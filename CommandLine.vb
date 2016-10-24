Module CommandLine

    Public bolDeveloperMode As Boolean

    Public Sub ParseCommand()
        Dim strCommand() As String

        strCommand = Microsoft.VisualBasic.Strings.Split(Microsoft.VisualBasic.Command, "-")

        'ExtractFromDB()

        'Process command line args
        If InArray("dev", strCommand) <> -1 Then
            'Developer mode
            bolDeveloperMode = True
            frmMain.mnuDev.Visible = True
        End If
    End Sub

    Private Function InArray(ByVal strSearch As String, ByRef strArray() As String) As Integer
        Dim intCount As Integer

        For intCount = 0 To (strArray.Length - 1)
            If Microsoft.VisualBasic.Strings.Trim(strSearch) = Microsoft.VisualBasic.Strings.Trim(strArray(intCount)) Then
                Return intCount
                Exit Function
            End If
        Next

        Return -1
    End Function
End Module