Imports System.Windows.Forms

Public Class frmMenu
    Private Structure MenuItem
        Public strText As String
        Public strLabel As String
    End Structure



    Private Sub GenerateMenu()
        Dim intCount As Integer
        Dim strOut As String

        Dim mniTemp As MenuItem
        Dim mniItems As New ArrayList

        If chk1.Checked = True Then
            mniTemp.strLabel = txtlbl1.Text
            mniTemp.strText = txt1.Text
            mniItems.Add(mniTemp)
        End If

        If chk2.Checked = True Then
            mniTemp.strLabel = txtlbl2.Text
            mniTemp.strText = txt2.Text
            mniItems.Add(mniTemp)
        End If

        If chk3.Checked = True Then
            mniTemp.strLabel = txtlbl3.Text
            mniTemp.strText = txt3.Text
            mniItems.Add(mniTemp)
        End If

        If chk4.Checked = True Then
            mniTemp.strLabel = txtlbl4.Text
            mniTemp.strText = txt4.Text
            mniItems.Add(mniTemp)
        End If

        If chk5.Checked = True Then
            mniTemp.strLabel = txtlbl5.Text
            mniTemp.strText = txt5.Text
            mniItems.Add(mniTemp)
        End If

        If chk6.Checked = True Then
            mniTemp.strLabel = txtlbl6.Text
            mniTemp.strText = txt6.Text
            mniItems.Add(mniTemp)
        End If

        If chk7.Checked = True Then
            mniTemp.strLabel = txtlbl7.Text
            mniTemp.strText = txt7.Text
            mniItems.Add(mniTemp)
        End If

        strOut = "Menu(""" & txtTitle.Text & """"

        For intCount = 0 To (mniItems.Count - 1)
            With CType(mniItems.Item(intCount), MenuItem)
                strOut &= ",""" & .strText & """," & .strLabel
            End With
        Next

        strOut &= ")"

        frmMain.InsertText(strOut)


    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        GenerateMenu()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtTitle.Focus()
    End Sub
End Class
