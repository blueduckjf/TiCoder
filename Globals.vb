Module Globals
    Public Tokens As TokenList

    Public Sub LoadTokenList(ByVal strPath As String)
        Dim fs As New System.IO.FileStream(strPath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        LoadTokenList(fs)
    End Sub
    Public Sub LoadTokenList(ByVal fs As System.IO.Stream)
        Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter

        Tokens = Nothing
        Tokens = CType(bf.Deserialize(fs), TokenList)
        fs.Close()
    End Sub

    Public Function Is2Byte(ByVal intByte1 As Byte) As Boolean
        Select Case intByte1
            Case 92, 93, 94, 96, 97, 98, 99, 126, 170, 187
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Function BuildFolderList() As ArrayList
        Dim strFolders As New ArrayList

        Dim bolMatch As Boolean
        Dim intCount, intCount2, intSearch As Integer
        Dim strFolder As String

        For intCount = 0 To 255
            For intCount2 = 0 To 255
                bolMatch = False
                strFolder = Tokens.PathList(intCount, intCount2)

                If strFolder <> "" Then

                    For intSearch = 0 To (strFolders.Count - 1)
                        If strFolder.Equals(strFolders(intSearch)) Then bolMatch = True
                    Next

                    If bolMatch = False Then
                        strFolders.Add(strFolder)
                    End If
                End If

            Next
        Next



        For intCount = 0 To (strFolders.Count - 1)
            AddParent(CStr(strFolders(intCount)), strFolders)
        Next

        strFolders.Sort()

        Return strFolders
    End Function

    Public Sub AddParent(ByVal strCurrent As String, ByRef strFolderList As ArrayList)
        Dim strParent As String = ""
        Dim intLoc As Integer


        For intLoc = 1 To strCurrent.Length
            strParent = Microsoft.VisualBasic.Strings.Right(strCurrent, intLoc)

            If Microsoft.VisualBasic.Strings.Left(strParent, 1) = "\" Then
                strParent = Microsoft.VisualBasic.Strings.Left(strCurrent, strCurrent.Length - strParent.Length)
                Exit For
            End If
        Next


        If strParent = "\" Then Exit Sub
        If strParent = "" Then Exit Sub

        If strFolderList.LastIndexOf(strParent) = -1 Then
            strFolderList.Add(strParent)
        End If



        'add the parent above this
        AddParent(strParent, strFolderList)

    End Sub

    Public Function GetDeepestFolder(ByVal strPath As String) As String
        Dim strTemp, strReturn As String
        Dim intCount, intTemp As Integer

        For intCount = 0 To strPath.Length
            intTemp = strPath.Length - intCount
            strTemp = Microsoft.VisualBasic.Strings.Mid(strPath, intTemp, 1)

            If strTemp = "\" Then
                strReturn = Microsoft.VisualBasic.Strings.Mid(strPath, intTemp + 1, strPath.Length - intTemp)
                Return strReturn
            End If
        Next

        Return strPath
    End Function


End Module
