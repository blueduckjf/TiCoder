Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmToken
    Private bytByte1, bytByte2 As Byte

    Private _texts(255, 255) As String
    Private _paths(255, 255) As String
    Private _bytes As Hashtable
    Private _sig As String


    Private Sub LoadTokens()
        frmMain.FillCommands("\catalog")

        lstTokens.Items.Clear()
        lstTokens.Items.AddRange(frmMain.lstItems.Items)

        With Tokens
            _texts = .TextList
            _paths = .PathList
            _bytes = .ByteList
            _sig = .Signature
        End With

        frmMain.FillCommands(CStr(frmMain.tvwTokens.SelectedNode.Tag))
    End Sub

    Private Sub frmToken_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadTokens()

        If bolDeveloperMode = True Then
            If File.Exists(AppDomain.CurrentDomain.BaseDirectory & "tokens.mdb") = True Then
                cmdLoad.Visible = True
            End If
        End If
    End Sub

    Private Sub lstTokens_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTokens.SelectedIndexChanged
        Dim strText As String
        Dim strBytes As String

        If lstTokens.SelectedItem Is Nothing Then Exit Sub
        strText = lstTokens.SelectedItem.ToString

        txtText.Enabled = True
        'txtBytes.Enabled = True
        txtPath.Enabled = True

        strBytes = CStr(_bytes(strText))
        bytByte1 = CByte("&H" & Microsoft.VisualBasic.Strings.Left(strBytes, 2))

        If Is2Byte(bytByte1) = True Then
            bytByte2 = CByte("&H" & Microsoft.VisualBasic.Strings.Right(strBytes, 2))
        Else
            bytByte2 = 0
        End If


        txtText.Text = strText
        txtPath.Text = _paths(bytByte1, bytByte2)

        If Is2Byte(bytByte1) = False Then
            txtBytes.Text = Microsoft.VisualBasic.Strings.Left(strBytes, 2)
        Else
            txtBytes.Text = Microsoft.VisualBasic.Strings.Left(strBytes, 2)
            txtBytes.Text &= Microsoft.VisualBasic.Strings.Right(strBytes, 2)
        End If



    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim fs As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\TI83.tkn", FileMode.Create)
        Dim bf As New BinaryFormatter


        Tokens = Nothing
        Tokens = New TokenList(_texts, _paths, _bytes, _sig)

        bf.Serialize(fs, Tokens)

        fs.Close()
        LoadTokens()

        frmMain.Reload()

        Me.Close()
    End Sub

    Private Sub txtText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtText.TextChanged
        Dim strOldText As String

        strOldText = _texts(bytByte1, bytByte2)

        If strOldText = "" Then Exit Sub

        _texts(bytByte1, bytByte2) = txtText.Text
        _bytes(strOldText) = Microsoft.VisualBasic.Hex(bytByte1) & Microsoft.VisualBasic.Hex(bytByte2)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub txtPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPath.TextChanged
        _paths(bytByte1, bytByte2) = txtPath.Text
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim strText, strBytes, strPath As String
        Dim bytByte1, bytByte2 As Byte

        strText = Microsoft.VisualBasic.InputBox("Text of Token?", "Add Token")
        strBytes = Microsoft.VisualBasic.InputBox("Bytes of Token?", "Add Token", "00")
        strPath = Microsoft.VisualBasic.InputBox("Path of Token?", "Add Token", "\")

        If strText = "" Then Exit Sub
        If strBytes = "" Then Exit Sub
        If strPath = "" Then Exit Sub

        bytByte1 = CByte("&H" & Microsoft.VisualBasic.Strings.Left(strBytes, 2))
        bytByte2 = CByte("&H" & Microsoft.VisualBasic.Strings.Right(strBytes, 2))

        _texts(bytByte1, bytByte2) = strText
        _paths(bytByte1, bytByte2) = strPath
        _bytes.Add(strText, strBytes)

        LoadTokens()
    End Sub


    Private Sub cmdByte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdByte.Click
        Dim bytByte1, bytByte2 As Byte
        Dim strBytes, strText As String
        Dim intIndex As Integer

        strBytes = Microsoft.VisualBasic.InputBox("Search for?", "Search by Bytes", "0000")

        If strBytes = "" Then Exit Sub

        lstTokens.SelectedItem = Nothing

        If Microsoft.VisualBasic.Strings.Len(strBytes) <> 4 Then Exit Sub

        bytByte1 = CByte("&H" & Microsoft.VisualBasic.Strings.Left(strBytes, 2))
        bytByte2 = CByte("&H" & Microsoft.VisualBasic.Strings.Right(strBytes, 2))
        strText = _texts(bytByte1, bytByte2)

        If strText = "" Then
            MessageBox.Show("No matching tokens found.", "Search by Byte", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        intIndex = lstTokens.Items.IndexOf(strText)
        lstTokens.SelectedItem = lstTokens.Items.Item(intIndex)
    End Sub

    Private Sub cmdText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdText.Click
        Dim strSearch As String
        Dim intIndex As Integer

        strSearch = Microsoft.VisualBasic.InputBox("Token string to search for?", "Search by Text")

        If strSearch = "" Then Exit Sub

        intIndex = lstTokens.Items.IndexOf(strSearch)

        If intIndex = -1 Then
            MessageBox.Show("No matching tokens found.", "Search by Text", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If



        lstTokens.SelectedItem = lstTokens.Items.Item(intIndex)

    End Sub

    Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        Dim tklNew As TokenList

        tklNew = LoadFromDB()

        Dim fs As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\TI83.tkn", FileMode.Create)
        Dim bf As New BinaryFormatter


        Tokens = tklNew

        bf.Serialize(fs, Tokens)

        fs.Close()
        LoadTokens()

        frmMain.Reload()

        Me.Close()
    End Sub
End Class