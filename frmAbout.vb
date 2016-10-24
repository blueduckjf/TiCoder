Public Class frmAbout

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub


    Private Sub lnkEmail_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkEmail.LinkClicked
        lnkEmail.LinkVisited = True
        'System.Diagnostics.Process.Start("IExplore", "mailto:blueduck577@gmail.com")
        System.Diagnostics.Process.Start("mailto:blueduck577@gmail.com")
    End Sub

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        picImage.Image = imlIcon.Images(0)
    End Sub
End Class