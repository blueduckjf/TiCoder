Imports System.IO

Public Class frmGroup

    Public strGroupFile As String

    Private zipUtil As New ZipUtility

    Private Sub frmGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strDir As String = System.AppDomain.CurrentDomain.BaseDirectory & "group\"
        Dim dirInfo As DirectoryInfo
        Dim filInfo() As FileInfo


        If Directory.Exists(strDir) = True Then Directory.Delete(strDir, True)
        Directory.CreateDirectory(strDir)
        zipUtil.Decompress(strGroupFile, strDir)

        dirInfo = New DirectoryInfo(strDir)
        filInfo = dirInfo.GetFiles("*.8xp")

        lstFiles.Items.AddRange(filInfo)

    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click, lstFiles.DoubleClick
        If sender Is lstFiles Then
            If lstFiles.Text = "" Then
                cmdOK.Enabled = False
                Exit Sub
            End If

        End If


        Dim filInfo As FileInfo = CType(lstFiles.SelectedItem, FileInfo)
        Dim strFilename As String = filInfo.FullName
        Dim strDir As String = System.AppDomain.CurrentDomain.BaseDirectory & "group\"

        Dim tiFile As New TiFile(strFilename)

        Directory.Delete(strDir, True)

        With frmMain
            .NewFile()

            .txtProg.Lines = tiFile.Lines
            .txtName.Text = tiFile.ProgramName
            .txtComment.Text = tiFile.Comment
            .chkProtected.Checked = tiFile.Protect

            .bolChanged = True
        End With

        If sender Is lstFiles Then Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub lstFiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        cmdOK.Enabled = True
    End Sub
End Class