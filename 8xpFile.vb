Option Compare Binary

Imports System.IO
Imports System.Text
Imports System.Runtime.InteropServices

Public Class TiFile
    Private _comment As String
    Private _progName As String
    Private _protect As Boolean

    Private bytHeader() As Byte = {42, 42, 84, 73, 56, 51, 70, 42, 26, 10, 0}

    Private ProgramData As MemoryStream

    Private Structure CheckedRange
        Public intLow As Integer
        Public intHigh As Integer
    End Structure
    Private Structure TokenLoc
        Public strText As String
        Public intStrLoc As Integer
    End Structure

    Private Class TokenLocComp
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            If CType(x, TokenLoc).intStrLoc > CType(y, TokenLoc).intStrLoc Then Return 1
            If CType(x, TokenLoc).intStrLoc = CType(y, TokenLoc).intStrLoc Then Return 0
            If CType(x, TokenLoc).intStrLoc < CType(y, TokenLoc).intStrLoc Then Return -1
        End Function
    End Class
    Private Class StringLenComp
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            If Microsoft.VisualBasic.Strings.Len(CStr(x)) > Microsoft.VisualBasic.Strings.Len(CStr(y)) Then Return -1
            If Microsoft.VisualBasic.Strings.Len(CStr(x)) = Microsoft.VisualBasic.Strings.Len(CStr(y)) Then Return 0
            If Microsoft.VisualBasic.Strings.Len(CStr(x)) < Microsoft.VisualBasic.Strings.Len(CStr(y)) Then Return 1

        End Function
    End Class


    Public Property Comment() As String
        Get
            Return _comment
        End Get
        Set(ByVal value As String)
            If Microsoft.VisualBasic.Strings.Len(value) > 42 Then
                Throw New Exception("Comments cannot be longer than 42 characters,")
            End If

            _comment = value
        End Set
    End Property
    Public Property ProgramName() As String
        Get
            Return _progName
        End Get
        Set(ByVal value As String)
            If Microsoft.VisualBasic.Strings.Len(value) > 8 Then
                Throw New Exception("Program names cannot be longer than 8 characters,")
            End If

            _progName = value
        End Set
    End Property
    Public Property Protect() As Boolean
        Get
            Return _protect
        End Get
        Set(ByVal value As Boolean)
            _protect = value
        End Set
    End Property

    Public Property Lines() As String()
        Get
            Dim bytTemp, bytTemp2 As Byte
            Dim bol2Byte As Boolean
            Dim strToken, strLine As String
            Dim strLines() As String

            ReDim strLines(0)
            strLine = ""
            ProgramData.Seek(0, SeekOrigin.Begin)

            Do Until ProgramData.Position = (ProgramData.Length - 1)
                bytTemp = CByte(ProgramData.ReadByte)

                bol2Byte = Is2Byte(bytTemp)

                If bol2Byte = False Then
                    bytTemp2 = 0
                Else
                    bytTemp2 = CByte(ProgramData.ReadByte)
                End If

                strToken = Tokens.TextList(bytTemp, bytTemp2)

                If strToken = "(crlf)" Then
                    strLines(strLines.Length - 1) = strLine
                    ReDim Preserve strLines(strLines.Length)
                    strLine = ""
                Else
                    strLine &= strToken
                End If

            Loop

            strLines(strLines.Length - 1) = strLine

            Return strLines
        End Get
        Set(ByVal value As String())
            Tokenize(value)
        End Set
    End Property

    Public Sub New(ByVal strProgramName As String, ByVal strComment As String, ByVal bolProtect As Boolean)
        Me.ProgramName = strProgramName
        Me.Comment = strComment
        _protect = bolProtect
        ProgramData = New MemoryStream
    End Sub
    Public Sub New(ByVal strFile As String)
        Load(strFile)
    End Sub

#Region "File Operations"

    Public Sub Load(ByRef fs As FileStream)
        Dim bytTemp() As Byte, bytChecksum(), bytProgTemp() As Byte
        Dim intDataLen As Integer

        'check the checksum before anything.
        ReDim bytTemp(1)
        fs.Seek(-2, SeekOrigin.End)
        fs.Read(bytTemp, 0, 2)

        ReDim bytProgTemp(CInt(fs.Length))
        fs.Seek(0, SeekOrigin.Begin)
        fs.Read(bytProgTemp, 0, CInt(fs.Length - 2))

        bytChecksum = Checksum(bytProgTemp)

        bytProgTemp = Nothing

        If bytChecksum(0) <> bytTemp(0) Then
            Throw New Exception("Checksum inconsistency; the file may be corrupt.")
        Else
            If bytChecksum(1) <> bytTemp(1) Then
                Throw New Exception("Checksum inconsistency; the file may be corrupt.")
            End If
        End If




        ReDim bytTemp(10)
        fs.Seek(0, SeekOrigin.Begin)
        fs.Read(bytTemp, 0, 11)

        If Microsoft.VisualBasic.Strings.Left(ByteToString(bytTemp), 8) <> "**TI83F*" Then
            Throw New Exception("Not a proper .8XP file.")
        End If

        fs.Seek(11, SeekOrigin.Begin)

        ReDim bytTemp(41)
        fs.Read(bytTemp, 0, 42)

        _comment = ByteToString(bytTemp)

        fs.Seek(59, SeekOrigin.Begin)
        ReDim bytTemp(0)
        fs.Read(bytTemp, 0, 1)

        If bytTemp(0) = 6 Then
            _protect = True
        Else
            _protect = False
        End If

        fs.Seek(60, SeekOrigin.Begin)
        ReDim bytTemp(7)
        fs.Read(bytTemp, 0, 8)

        _progName = ByteToString(bytTemp)


        fs.Seek(72, SeekOrigin.Begin)

        ReDim bytTemp(1)
        fs.Read(bytTemp, 0, 2)

        intDataLen = ByteToInt(bytTemp)

        ReDim bytTemp(intDataLen)
        fs.Read(bytTemp, 0, intDataLen)

        ProgramData = New MemoryStream(bytTemp)

        fs.Close()
    End Sub
    Public Sub Load(ByVal strFile As String)
        Dim fs As New FileStream(strFile, FileMode.Open)
        Load(fs)
    End Sub
    Public Sub Save(ByRef fs As FileStream)
        Dim bytChecksum(1) As Byte
        Dim ms As New MemoryStream

        'Header
        ms.Write(bytHeader, 0, bytHeader.Length)

        'Comment
        WritePad(_comment, 42, ms)

        'Length of Data section + 19
        ms.Write(IntToByte(CType(ProgramData.Length + 19, UShort)), 0, 2)

        'Random shit: 13, 0
        ms.WriteByte(13)
        ms.WriteByte(0)

        'Length of Data section + 2
        ms.Write(IntToByte(CType(ProgramData.Length + 2, UShort)), 0, 2)

        'Protected
        If _protect = True Then
            ms.WriteByte(6)
        Else
            ms.WriteByte(5)
        End If

        'Program name
        WritePad(_progName, 8, ms)

        'More Random bytes
        ms.WriteByte(0)
        ms.WriteByte(0)

        'Length of Data section + 2
        ms.Write(IntToByte(CType(ProgramData.Length + 2, UShort)), 0, 2)

        'Length of Data section
        ms.Write(IntToByte(CType(ProgramData.Length, UShort)), 0, 2)

        'Data section
        ProgramData.WriteTo(ms)

        'Checksum
        bytChecksum = Checksum(ms.ToArray)

        ms.WriteTo(fs)
        fs.Write(bytChecksum, 0, bytChecksum.Length)

        fs.Close()
        ms.Close()


    End Sub
    Public Sub Save(ByVal strFile As String, ByVal modMode As System.IO.FileMode)
        Dim fs As New FileStream(strFile, modMode)
        Save(fs)
    End Sub
#End Region

    Private Function SortTokens() As ArrayList
        Dim strSortedTokens As New ArrayList

        Dim intCount, intCount2 As Integer
        Dim strToken As String

        For intCount = 0 To 255
            For intCount2 = 0 To 255
                strToken = Tokens.TextList(intCount, intCount2)

                Select Case strToken
                    Case "[o]", "[e]", ""
                        'do nothing
                    Case Else
                        strSortedTokens.Add(strToken)
                End Select
            Next
        Next

        strSortedTokens.Sort(New StringLenComp)

        Return strSortedTokens
    End Function

    Private Sub Tokenize(ByRef strLines() As String)
        Dim mstLine As MemoryStream

        Dim strSorted As ArrayList

        Dim intCount As Integer

        ProgramData.Close()
        ProgramData = New MemoryStream

        strSorted = SortTokens()

        For intCount = 0 To (strLines.Length - 1)
            mstLine = TokenizeLine(strLines(intCount), strSorted)

            If intCount <> (strLines.Length - 1) Then
                mstLine.WriteByte(CByte("&H3F"))
            End If

            mstLine.WriteTo(ProgramData)



        Next


    End Sub
    Private Function TokenizeLine(ByVal strLine As String, ByRef strSorted As ArrayList) As MemoryStream
        Dim mstOut As New MemoryStream

        Dim bolInChecked As Boolean
        Dim intCount, intLoc, intCheckCount As Integer
        Dim strCompare, strBytes As String

        Dim bytByte1, bytByte2 As Byte


        Dim tklTemp As TokenLoc
        Dim tklLocs As New ArrayList

        Dim crgTemp As CheckedRange
        Dim crgChecked As New ArrayList


        strLine = Microsoft.VisualBasic.Strings.Replace(strLine, " ", Microsoft.VisualBasic.Chr(CInt("&H26")))
        strLine = Microsoft.VisualBasic.Strings.Replace(strLine, Microsoft.VisualBasic.vbCrLf, "(crlf)")

        For intCount = 0 To (strSorted.Count - 1)
            strCompare = CStr(strSorted.Item(intCount))

            If strCompare = " " Then
                strCompare = Microsoft.VisualBasic.Chr(CInt("&H26"))
            Else
                strCompare = Microsoft.VisualBasic.Strings.Replace(strCompare, " ", Microsoft.VisualBasic.Chr(CInt("&H26")))
            End If

            intLoc = Microsoft.VisualBasic.Strings.InStr(1, strLine, strCompare)

            Do Until intLoc = 0
                bolInChecked = False

                For intCheckCount = 0 To (crgChecked.Count - 1)
                    With CType(crgChecked.Item(intCheckCount), CheckedRange)
                        If .intLow <= intLoc Then
                            If intLoc <= (.intHigh - 1) Then bolInChecked = True
                        End If
                    End With
                Next

                If strCompare = Microsoft.VisualBasic.Chr(CInt("&H26")) Then
                    strCompare = " "
                Else
                    strCompare = Microsoft.VisualBasic.Strings.Replace(strCompare, Microsoft.VisualBasic.Chr(CInt("&H26")), " ")
                End If

                If bolInChecked = False Then
                    With tklTemp
                        .intStrLoc = intLoc
                        .strText = strCompare
                    End With

                    tklLocs.Add(tklTemp)

                    crgTemp.intLow = intLoc
                    crgTemp.intHigh = intLoc + Microsoft.VisualBasic.Strings.Len(strCompare)
                    crgChecked.Add(crgTemp)
                End If

                intLoc = Microsoft.VisualBasic.Strings.InStr(intLoc + 1, strLine, strCompare)
            Loop
        Next

        tklLocs.Sort(New TokenLocComp)

        For intCount = 0 To (tklLocs.Count - 1)
            strCompare = CType(tklLocs.Item(intCount), TokenLoc).strText




            strBytes = CStr(Tokens.ByteList(strCompare))


            bytByte1 = CByte("&H" & Microsoft.VisualBasic.Strings.Left(strBytes, 2))
            mstOut.WriteByte(bytByte1)

            If Is2Byte(bytByte1) Then
                bytByte2 = CByte("&H" & Microsoft.VisualBasic.Strings.Right(strBytes, 2))
                mstOut.WriteByte(bytByte2)
            End If

        Next

        Return mstOut
    End Function

    Public Shared Function Checksum(ByVal bytProgram As Byte()) As Byte()
        Dim intCount As Integer
        Dim intChecksum As UInt16
        Dim intAdd As UInteger
        Dim bytReturn(1) As Byte

        For intCount = 55 To (bytProgram.Length - 1)
            intAdd = intAdd + bytProgram(intCount)
        Next

        intChecksum = CType(intAdd Mod (2 ^ 16), UInt16)
        bytReturn = IntToByte(intChecksum)

        Return bytReturn
    End Function

    Private Shared Function IntToByte(ByVal intInteger As UInt16) As Byte()
        Dim bytReturn(1) As Byte

        Dim gcIHandle As GCHandle = GCHandle.Alloc(intInteger, Runtime.InteropServices.GCHandleType.Pinned)
        Dim ptrAdd As IntPtr = gcIHandle.AddrOfPinnedObject

        System.Runtime.InteropServices.Marshal.Copy(ptrAdd, bytReturn, 0, 2)

        Return bytReturn
    End Function

    Private Shared Function ByteToInt(ByVal bytByte() As Byte) As Integer
        Dim intReturn As Integer

        Dim gcIHandle As GCHandle = GCHandle.Alloc(intReturn, Runtime.InteropServices.GCHandleType.Pinned)
        Dim ptrAdd As IntPtr = gcIHandle.AddrOfPinnedObject

        System.Runtime.InteropServices.Marshal.Copy(bytByte, 0, ptrAdd, 2)

        intReturn = Marshal.ReadInt32(ptrAdd)

        Return intReturn
    End Function

    Private Function StringToByte(ByVal strString As String) As Byte()
        Dim bytReturn() As Byte

        bytReturn = Encoding.Default.GetBytes(strString)

        Return bytReturn
    End Function

    Private Function ByteToString(ByVal bytByte() As Byte) As String
        Dim strTemp As String

        strTemp = Encoding.Default.GetString(bytByte)

        Return strTemp
    End Function

    Private Function InStrSpaces(ByVal intStart As Integer, ByVal strString1 As String, ByVal strString2 As String, ByVal cmpCompare As Microsoft.VisualBasic.CompareMethod) As Integer
        If strString2 <> " " Then
            Return Microsoft.VisualBasic.Strings.InStr(intStart, strString1, strString2, cmpCompare)
        Else
            'instr is beat, it doesnt search for spaces.  custom instr right here
            Dim intCount As Integer

            For intCount = intStart To Microsoft.VisualBasic.Strings.Len(strString1)
                If strString2 = Microsoft.VisualBasic.Strings.Mid(strString1, intCount, Microsoft.VisualBasic.Strings.Len(strString2)) Then
                    Return intCount
                    Exit Function
                End If
            Next
        End If
    End Function

    Private Sub WritePad(ByVal strText As String, ByVal intMaxLen As Integer, ByRef fs As MemoryStream)
        Dim intPad, intCount As Integer

        intPad = intMaxLen - Microsoft.VisualBasic.Strings.Len(strText)
        fs.Write(StringToByte(strText), 0, Microsoft.VisualBasic.Strings.Len(strText))

        For intCount = 1 To intPad
            fs.WriteByte(0)
        Next
    End Sub


End Class
