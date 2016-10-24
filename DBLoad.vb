Imports System.IO
Imports System.Data.OleDb

Module DBLoad
    Public Function LoadFromDB() As TokenList
        Dim tklReturn As TokenList

        Dim strTexts(255, 255), strPaths(255, 255) As String
        Dim htbBytes As New Hashtable

        Dim sBytes As MemoryStream
        Dim strText, strPath, strBytes As String
        Dim bytByte1, bytByte2 As Byte

        Dim strConnection As String = _
        "Provider=Microsoft.Jet.OLEDB.4.0;" _
        & "Data Source=" & System.AppDomain.CurrentDomain.BaseDirectory & "tokens.mdb"

        Dim odbCommand As New OleDbCommand
        Dim odbReader As OleDbDataReader

        With odbCommand
            .Connection = New OleDbConnection(strConnection)
            .Connection.Open()

            .CommandText = "SELECT * FROM Tokens"

            odbReader = .ExecuteReader
        End With

        With odbReader
            Do While .Read
                strBytes = .GetString(1)
                sBytes = StringToBytes(strBytes)
                strText = .GetString(2)
                strPath = .GetString(3)

                If sBytes.Length = 1 Then
                    bytByte1 = CByte(sBytes.ReadByte)
                    bytByte2 = 0
                Else
                    bytByte1 = CByte(sBytes.ReadByte)
                    bytByte2 = CByte(sBytes.ReadByte)
                End If

                strTexts(bytByte1, bytByte2) = strText
                strPaths(bytByte1, bytByte2) = strPath

                If htbBytes.Item(strText) Is Nothing Then
                    htbBytes.Add(strText, strBytes)
                Else
                    strText = "DUPe"
                End If
            Loop
        End With

        tklReturn = New TokenList(strTexts, strPaths, htbBytes, "TI83")

        Return tklReturn
    End Function

    Private Function StringToBytes(ByVal strBytes As String) As MemoryStream
        Dim msOut As New MemoryStream
        Dim str1, str2 As String

        If strBytes.Length = 4 Then
            str1 = Microsoft.VisualBasic.Strings.Left(strBytes, 2)
            str2 = Microsoft.VisualBasic.Strings.Right(strBytes, 2)
            msOut.WriteByte(CByte("&H" & str1))
            msOut.WriteByte(CByte("&H" & str2))
        Else
            msOut.WriteByte(CByte("&H" & strBytes))
        End If

        msOut.Seek(0, SeekOrigin.Begin)

        Return msOut
    End Function
End Module

