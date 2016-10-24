Public Class ButtonList
    Public Sub LoadPath(ByVal strPath As String)
        txtPath.Text = strPath

        strPath &= "\"

        Tabs.TabPages.Clear()

        'Get a list of subfolders.
        Dim aryPaths, aryMatch As ArrayList
        Dim strTemp As String
        Dim intCount As Integer

        aryPaths = BuildFolderList()
        aryMatch = New ArrayList

        For intCount = 0 To (aryPaths.Count - 1)
            strTemp = CStr(aryPaths.Item(intCount))

            If Microsoft.VisualBasic.Strings.Left(strTemp, strPath.Length) = strPath Then
                If strTemp <> strPath Then
                    aryMatch.Add(strTemp)
                End If
            ElseIf strTemp = Microsoft.VisualBasic.Strings.Left(strPath, strPath.Length - 1) Then
                aryMatch.Add(strTemp)
            End If
        Next


        For intCount = 0 To (aryMatch.Count - 1)
            strTemp = CStr(aryMatch.Item(intCount))
            AddTab(strTemp)
        Next

        'Tabs.TabPages.Clear()
    End Sub

    Private Sub AddTab(ByVal strPath As String)
        Dim strDeep As String = GetDeepestFolder(strPath)
        Dim tbTab As New TabPage
        Dim lstItems As New ListBox
        Dim aryCommands As ArrayList
        Dim intCount As Integer

        tbTab.Text = strDeep
        Tabs.Font = lblFont.Font

        With lstItems
            .Font = lblFont.Font
            .Location = New Point(4, 4)
            .Width = Me.Width - 16
            .Height = Tabs.Height - 32
        End With

        aryCommands = ListCommands(strPath)

        If aryCommands.Count = 0 Then

        Else
            For intCount = 0 To (aryCommands.Count - 1)
                lstItems.Items.Add(aryCommands(intCount))
            Next

            tbTab.Controls.Add(lstItems)
            Tabs.TabPages.Add(tbTab)
        End If

        AddHandler lstItems.DoubleClick, AddressOf ListHandler

        Me.Height = Tabs.Height
    End Sub

    Private Function ListCommands(ByVal strPath As String) As ArrayList
        Dim intCount1, intCount2 As Integer
        Dim aryReturn As New ArrayList
        Dim strTemp, strCommand As String

        For intCount1 = 0 To Tokens.PathList.GetUpperBound(0)
            For intCount2 = 0 To Tokens.PathList.GetUpperBound(1)
                strTemp = Tokens.PathList(intCount1, intCount2)
                If strTemp = strPath Then
                    strCommand = Tokens.TextList(intCount1, intCount2)
                    aryReturn.Add(strCommand)
                End If
            Next
        Next

        Return aryReturn
    End Function

    Private Sub ListHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strCommand As String

        strCommand = CType(sender, ListBox).Text

        RaiseEvent CommandSelected(strCommand)
    End Sub


    Private Sub cmdX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Visible = False
    End Sub

    Private Sub ButtonList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Size = New System.Drawing.Size(172, 191)
    End Sub

    Public Event CommandSelected(ByVal strCommand As String)
End Class
