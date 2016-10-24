

Public Class frmMain
    Private CurrentFile As TiFile
    Public bolChanged As Boolean
    Private strOpenFile As String

    Private WithEvents ClipMon As New ClipboardMonitor

#Region "Tree Operations"

    Private Sub BuildTree()
        Dim strFolders As ArrayList = BuildFolderList()
        Dim nodRoot As New TreeNode

        With nodRoot
            .ImageIndex = 3
            .SelectedImageIndex = 3
            .Text = "Categorized Commands"
            .Tag = "\"
        End With

        AddChildren("\", nodRoot, strFolders)

        With tvwTokens
            .Nodes.Add(nodRoot)
            .Nodes(0).Expand()
            .SelectedNode = tvwTokens.Nodes(0)
        End With


        nodRoot = New TreeNode
        With nodRoot
            .ImageIndex = 3
            .SelectedImageIndex = 3
            .Text = "Catalog"
            .Tag = "\catalog"
        End With

        tvwTokens.Nodes.Add(nodRoot)

    End Sub


    Private Sub AddChildren(ByVal strPath As String, ByRef nodNode As TreeNode, ByRef strFolders As ArrayList)
        Dim intCount As Integer
        Dim strTemp As String


        For intCount = 0 To (strFolders.Count - 1)
            strTemp = CStr(strFolders.Item(intCount))

            If Microsoft.VisualBasic.Strings.Left(strTemp, strPath.Length) = strPath Then
                If strTemp <> strPath Then
                    'node in strtemp is a child of this node.
                    Dim nodChild As New TreeNode
                    Dim strSplit() As String
                    Dim strSplit2() As String

                    strSplit = Microsoft.VisualBasic.Strings.Split(strTemp, "\")

                    With nodChild
                        .Text = FilterTreeNames(strSplit(strSplit.Length - 1))
                        .ImageIndex = 1
                        .SelectedImageIndex = 1
                        .Tag = strTemp
                    End With

                    strSplit2 = Microsoft.VisualBasic.Strings.Split(strPath, "\")

                    If strSplit2(strSplit2.Length - 1) = strSplit(strSplit.Length - 2) Then
                        nodNode.Nodes.Add(nodChild)
                        'now add the children of this node.
                        AddChildren(strTemp, nodChild, strFolders)
                    End If


                End If

            End If
        Next

    End Sub
    Private Function FilterTreeNames(ByVal strName As String) As String
        If strName = "k" Then strName = "Calculator Keys"

        strName = Capitalize(strName)

        Return strName
    End Function
    Private Function Capitalize(ByVal strString As String) As String
        Dim strBegin, strEnd As String
        strBegin = Microsoft.VisualBasic.Strings.Left(strString, 1)
        strEnd = Microsoft.VisualBasic.Strings.Right(strString, strString.Length - 1)

        strBegin = Microsoft.VisualBasic.Strings.UCase(strBegin)

        strString = strBegin & strEnd

        Return strString
    End Function


    Public Sub FillCommands(ByVal strPath As String, Optional ByVal bolClear As Boolean = True)
        Dim intCount, intCount2 As Integer
        Dim strCommand As String

        If bolClear = True Then lstItems.Items.Clear()

        If strPath = "\catalog" Then
            For intCount = 0 To 255
                For intCount2 = 0 To 255
                    strCommand = Tokens.TextList(intCount, intCount2)
                    Select Case strCommand
                        Case "[o]", "[e]", ""
                            'do nothing
                        Case Else
                            lstItems.Items.Add(strCommand)
                    End Select
                Next
            Next

            lstItems.Sorted = True
            lstItems.Sorted = False
            Exit Sub
        End If

        For intCount = 0 To 255
            For intCount2 = 0 To 255
                If strPath.Equals(Tokens.PathList(intCount, intCount2)) Then
                    strCommand = Tokens.TextList(intCount, intCount2)

                    If strCommand <> "[o]" Then
                        lstItems.Items.Add(strCommand)
                    End If
                End If
            Next
        Next

        Dim aryChildren, aryFolders As New ArrayList



        If lstItems.Items.Count = 0 Then
            If strPath <> "\" Then
                aryFolders = BuildFolderList()

                For intCount = 0 To (aryFolders.Count - 1)
                    If Microsoft.VisualBasic.Strings.Left(CStr(aryFolders.Item(intCount)), strPath.Length) = strPath Then
                        If CStr(aryFolders.Item(intCount)) <> strPath Then
                            aryChildren.Add(CStr(aryFolders.Item(intCount)))
                        End If
                    End If

                Next
            End If
        End If

        For intCount = 0 To (aryChildren.Count - 1)
            FillCommands(CStr(aryChildren.Item(intCount)), False)
        Next
    End Sub
#End Region
#Region "File Ops"
    Public Sub NewFile()
        CurrentFile = Nothing
        CurrentFile = New TiFile("NEWPROG", "Created with TI-Coder", False)

        strOpenFile = ""

        UpdateGUI()

        txtProg.Clear()
        bolChanged = False
    End Sub

    Private Sub Save(ByVal strFilename As String)
        If Microsoft.VisualBasic.Strings.UCase(Microsoft.VisualBasic.Strings.Right(strFilename, 3)) = "8XP" Then
            CurrentFile.Lines = txtProg.Lines
            CurrentFile.Save(strFilename, IO.FileMode.Create)

        Else
            Dim fs As New System.IO.FileStream(strFilename, IO.FileMode.Create)
            Dim sw As New System.IO.StreamWriter(fs)

            Dim intCount As Integer

            For intCount = 0 To (txtProg.Lines.Length - 1)
                sw.WriteLine(txtProg.Lines(intCount))
            Next
        End If

        strOpenFile = strFilename
        bolChanged = False
    End Sub
    Private Sub Save()
        If strOpenFile = "" Then
            PromptSave()
        Else
            Save(strOpenFile)
        End If
    End Sub
    Private Sub PromptSave()
        Dim strOpenFile As String

        Select Case dlgSave.ShowDialog
            Case Windows.Forms.DialogResult.OK
                strOpenFile = dlgSave.FileName
                Save(strOpenFile)
            Case Windows.Forms.DialogResult.Cancel
                Exit Sub
        End Select


    End Sub
    Private Function QuerySave() As DialogResult
        If bolChanged = True Then
            Select Case MessageBox.Show(txtName.Text & " has not been saved.  Would you like to save the changes?", "TI-Coder", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Case Windows.Forms.DialogResult.Yes
                    Save()
                    Return Windows.Forms.DialogResult.Yes
                Case Windows.Forms.DialogResult.No
                    Return Windows.Forms.DialogResult.No
                Case Windows.Forms.DialogResult.Cancel
                    Return Windows.Forms.DialogResult.Cancel
            End Select
        End If
    End Function
    Private Sub Open()
        Dim dlgResult As DialogResult
        dlgResult = QuerySave()

        If dlgResult = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        Else
            Select Case dlgOpen.ShowDialog
                Case Windows.Forms.DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        strOpenFile = dlgOpen.FileName

        Open(strOpenFile)
    End Sub
    Public Sub Open(ByVal strFilename As String)
        Select Case Microsoft.VisualBasic.Strings.UCase(Microsoft.VisualBasic.Strings.Right(strFilename, 3))
            Case "8XP"
                CurrentFile = Nothing
                CurrentFile = New TiFile(strFilename)

                UpdateGUI()

                txtProg.Clear()

                txtProg.Lines = CurrentFile.Lines
            Case "TIG"
                Dim fGroup As New frmGroup

                With fGroup
                    .strGroupFile = strFilename
                    .Text = "TI Group:  " & GetDeepestFolder(strFilename) 'not the intended purpose for this function, but it does the job perfectly.
                    .ShowDialog()
                End With

                Exit Sub
            Case Else
                Dim fs As New System.IO.FileStream(strFilename, IO.FileMode.Open)
                Dim sr As New System.IO.StreamReader(fs)

                txtProg.Clear()
                txtProg.Text = sr.ReadToEnd

                sr.Close()
                fs.Close()
        End Select





        bolChanged = False

    End Sub
#End Region

    Public Sub Reload()
        tvwTokens.Nodes.Clear()
        lstItems.Items.Clear()
        BuildTree()
    End Sub

    Private Sub UpdateGUI()
        txtName.Text = CurrentFile.ProgramName
        txtComment.Text = CurrentFile.Comment
        chkProtected.Checked = CurrentFile.Protect
    End Sub





    Public Sub InsertText(ByVal strText As String)
        Dim strBefore, strAfter As String
        Dim intTextPos As Integer

        intTextPos = txtProg.SelectionStart
        strBefore = Microsoft.VisualBasic.Strings.Left(txtProg.Text, intTextPos)
        strAfter = Microsoft.VisualBasic.Strings.Mid(txtProg.Text, intTextPos + 1, Microsoft.VisualBasic.Strings.Len(txtProg.Text) - intTextPos)

        txtProg.Text = strBefore & strText & strAfter
        txtProg.SelectionStart = intTextPos + Microsoft.VisualBasic.Strings.Len(strText)
        txtProg.Focus()
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim dlgResult As DialogResult = QuerySave()

        If dlgResult = Windows.Forms.DialogResult.Cancel Then e.Cancel = True
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ParseCommand()

        frmMain_Resize(sender, e)

        'LoadTokenList(System.AppDomain.CurrentDomain.BaseDirectory & "\TI83.tkn")
        LoadTokenList(New System.IO.MemoryStream(My.Resources.TI83))
        BuildTree()

        NewFile()
    End Sub
    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim intTotal As Integer

        pnlLeft.Top = mnuMenus.Height + tbrToolbar.Height

        tabCommand.Height = pnlLeft.Height - fraInfo.Height - 8 - tbrToolbar.Height


        intTotal = tabLists.Height - 16 '- tbrToolbar.Height

        tvwTokens.Height = CInt(intTotal / 2) - 8 '- tbrToolbar.Height

        lstItems.Top = tvwTokens.Top + tvwTokens.Height + 8
        lstItems.Height = CInt(intTotal / 2)

        With txtProg
            .Top = mnuMenus.Height + tbrToolbar.Height
            .Left = pnlLeft.Width
            .Height = Me.Height - 35 - mnuMenus.Height - tbrToolbar.Height
            .Width = Me.Width - pnlLeft.Width - 10
        End With
    End Sub



    Private Sub tvwTokens_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwTokens.AfterSelect
        FillCommands(CStr(tvwTokens.SelectedNode.Tag))
    End Sub

    Private Sub lstItems_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstItems.DoubleClick
        InsertText(lstItems.SelectedItem.ToString)
    End Sub


    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Dim dlgResult As DialogResult
        dlgResult = QuerySave()

        If dlgResult = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        Else
            End
        End If
    End Sub

    Private Sub mnuSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveAs.Click
        PromptSave()
    End Sub

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click, tbtSave.Click
        Save()
    End Sub

    Private Sub mnuOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpen.Click, tbtOpen.Click
        Open()
    End Sub

    Private Sub mnuNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNew.Click, tbtNew.Click
        Dim dlgResult As DialogResult
        dlgResult = QuerySave()

        If dlgResult = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        Else
            NewFile()
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If Not Microsoft.VisualBasic.IsNothing(CurrentFile) Then CurrentFile.ProgramName = txtName.Text
        bolChanged = True
    End Sub

    Private Sub txtComment_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComment.TextChanged
        If Not Microsoft.VisualBasic.IsNothing(CurrentFile) Then CurrentFile.Comment = txtComment.Text
        bolChanged = True
    End Sub

    Private Sub chkProtected_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProtected.CheckedChanged
        If Not Microsoft.VisualBasic.IsNothing(CurrentFile) Then CurrentFile.Protect = chkProtected.Checked
        bolChanged = True
    End Sub

    Private Sub txtProg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProg.Click
        ButtonList.Visible = False
    End Sub



    Private Sub SelectionChanged()
        If txtProg.SelectedText.Length > 0 Then
            tbtCut.Enabled = True
            tbtCopy.Enabled = True
            mnuCut.Enabled = True
            mnuCopy.Enabled = True
        Else
            tbtCut.Enabled = False
            tbtCopy.Enabled = False
            mnuCut.Enabled = False
            mnuCopy.Enabled = False
        End If
    End Sub

    Private Sub txtProg_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProg.KeyUp
        SelectionChanged()
    End Sub

    Private Sub txtProg_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtProg.MouseUp
        SelectionChanged()
    End Sub




    Private Sub txtProg_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProg.TextChanged
        'If Not Microsoft.VisualBasic.IsNothing(CurrentFile) Then CurrentFile.Lines = txtProg.Lines
        bolChanged = True


    End Sub

    Private Sub mnuDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDesign.Click, tbtMenuD.Click
        Dim fMenu As New frmMenu
        fMenu.ShowDialog()
    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub mnuTokenEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTokenEdit.Click
        frmToken.ShowDialog()
    End Sub

    Private Sub tabCommand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCommand.Click
        frmMain_Resize(sender, e)
        ButtonList.Visible = False
    End Sub


    Public Sub ButtonPush(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _
  cbt0.MouseDown, cbt1.MouseDown, cbt2.MouseDown, cbt3.MouseDown, cbt4.MouseDown, _
 cbt5.MouseDown, cbt6.MouseDown, cbt7.MouseDown, cbt8.MouseDown, cbt9.MouseDown, cbtApps.MouseDown, _
 cbtClear.MouseDown, cbtComma.MouseDown, cbtCos.MouseDown, cbtDel.MouseDown, cbtDiv.MouseDown, _
 cbtDot.MouseDown, cbtEnter.MouseDown, cbtExp.MouseDown, cbtGraph.MouseDown, _
 cbtLeftPar.MouseDown, cbtLn.MouseDown, cbtLog.MouseDown, cbtMath.MouseDown, cbtMinus.MouseDown, cbtMode.MouseDown, _
 cbtNeg.MouseDown, cbtOn.MouseDown, cbtPlus.MouseDown, cbtPrgm.MouseDown, _
 cbtRightPar.MouseDown, cbtSin.MouseDown, cbtStat.MouseDown, cbtSto.MouseDown, cbtTan.MouseDown, _
 cbtTimes.MouseDown, cbtTrace.MouseDown, cbtVars.MouseDown, cbtWindow.MouseDown, _
 cbtXinv.MouseDown, cbtXsq.MouseDown, cbtXT0n.MouseDown, cbtYeq.MouseDown, cbtZoom.MouseDown

        ButtonList.Visible = False

        If e.Button = Windows.Forms.MouseButtons.Left Then
            If sender Is cbtTrace Then InsertText("Trace")
            If sender Is cbtXT0n Then InsertText("X")
            If sender Is cbtXinv Then InsertText("ñ")
            If sender Is cbtSin Then InsertText("sin(")
            If sender Is cbtCos Then InsertText("cos(")
            If sender Is cbtTan Then InsertText("tan(")
            If sender Is cbtExp Then InsertText("^")
            If sender Is cbtXsq Then InsertText("Ü")
            If sender Is cbtComma Then InsertText(",")
            If sender Is cbtRightPar Then InsertText("(")
            If sender Is cbtLeftPar Then InsertText(")")
            If sender Is cbtLog Then InsertText("log(")
            If sender Is cbtLn Then InsertText("ln(")
            If sender Is cbtSto Then InsertText("ü")
            If sender Is cbt0 Then InsertText("0")
            If sender Is cbt1 Then InsertText("1")
            If sender Is cbt2 Then InsertText("2")
            If sender Is cbt3 Then InsertText("3")
            If sender Is cbt4 Then InsertText("4")
            If sender Is cbt5 Then InsertText("5")
            If sender Is cbt6 Then InsertText("6")
            If sender Is cbt7 Then InsertText("7")
            If sender Is cbt8 Then InsertText("8")
            If sender Is cbt9 Then InsertText("9")
            If sender Is cbtDot Then InsertText(".")
            If sender Is cbtNeg Then InsertText("ú")
            If sender Is cbtPlus Then InsertText("+")
            If sender Is cbtMinus Then InsertText("-")
            If sender Is cbtTimes Then InsertText("*")
            If sender Is cbtDiv Then InsertText("/")

            If sender Is cbtStat Then GenerateMenu("\stat", sender, e)
            If sender Is cbtMath Then GenerateMenu("\math", sender, e)
            If sender Is cbtApps Then GenerateMenu("\finance", sender, e)
            If sender Is cbtPrgm Then GenerateMenu("\prgm", sender, e)
            If sender Is cbtVars Then GenerateMenu("\vars", sender, e)
            If sender Is cbtMode Then GenerateMenu("\mode", sender, e)

        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            'right click = 2nd
            If sender Is cbtYeq Then GenerateMenu("\statplot", sender, e)
            If sender Is cbtWindow Then GenerateMenu("\tblset", sender, e)
            If sender Is cbtZoom Then GenerateMenu("\format", sender, e)
            If sender Is cbtZoom Then GenerateMenu("\format", sender, e)
            If sender Is cbtStat Then GenerateMenu("\list", sender, e)
            If sender Is cbtMath Then GenerateMenu("\test", sender, e)
            If sender Is cbtApps Then GenerateMenu("\angle", sender, e)
            If sender Is cbtPrgm Then GenerateMenu("\draw", sender, e)
            If sender Is cbtVars Then GenerateMenu("\distr", sender, e)
            If sender Is cbtXinv Then GenerateMenu("\matrx", sender, e)
            If sender Is cbtSin Then InsertText("sinñ(")
            If sender Is cbtCos Then InsertText("cosñ(")
            If sender Is cbtTan Then InsertText("tanñ(")
            If sender Is cbtExp Then InsertText("Ä")
            If sender Is cbtXsq Then InsertText("ð(")
            If sender Is cbtComma Then InsertText("û")
            If sender Is cbtLeftPar Then InsertText("{")
            If sender Is cbtRightPar Then InsertText("}")
            If sender Is cbtDiv Then InsertText("ë")
            If sender Is cbtLog Then InsertText("ý^(")
            If sender Is cbt7 Then InsertText("u")
            If sender Is cbt8 Then InsertText("v")
            If sender Is cbt9 Then InsertText("w")
            If sender Is cbtTimes Then InsertText("[")
            If sender Is cbtLn Then InsertText("ë^(")
            If sender Is cbt1 Then InsertText("L")
            If sender Is cbt2 Then InsertText("L‚")
            If sender Is cbt3 Then InsertText("Lƒ")
            If sender Is cbt4 Then InsertText("L„")
            If sender Is cbt5 Then InsertText("L…")
            If sender Is cbt6 Then InsertText("L†")
            If sender Is cbtMinus Then InsertText("]")
            If sender Is cbtPlus Then GenerateMenu("\mem", sender, e)
            If sender Is cbtDot Then InsertText("à")
            If sender Is cbtNeg Then InsertText("Ans")
        ElseIf e.Button = Windows.Forms.MouseButtons.Middle Then
            'middle click = alpha
            If sender Is cbtMath Then InsertText("A")
            If sender Is cbtApps Then InsertText("B")
            If sender Is cbtPrgm Then InsertText("C")
            If sender Is cbtXinv Then InsertText("D")
            If sender Is cbtSin Then InsertText("E")
            If sender Is cbtCos Then InsertText("F")
            If sender Is cbtTan Then InsertText("G")
            If sender Is cbtExp Then InsertText("H")
            If sender Is cbtXsq Then InsertText("I")
            If sender Is cbtComma Then InsertText("J")
            If sender Is cbtLeftPar Then InsertText("K")
            If sender Is cbtRightPar Then InsertText("L")
            If sender Is cbtDiv Then InsertText("M")
            If sender Is cbtLog Then InsertText("N")
            If sender Is cbt7 Then InsertText("O")
            If sender Is cbt8 Then InsertText("P")
            If sender Is cbt9 Then InsertText("Q")
            If sender Is cbtTimes Then InsertText("R")
            If sender Is cbtLn Then InsertText("S")
            If sender Is cbt4 Then InsertText("T")
            If sender Is cbt5 Then InsertText("U")
            If sender Is cbt6 Then InsertText("V")
            If sender Is cbtMinus Then InsertText("W")
            If sender Is cbtSto Then InsertText("X")
            If sender Is cbt1 Then InsertText("Y")
            If sender Is cbt2 Then InsertText("Z")
            If sender Is cbt3 Then InsertText("Á")
            If sender Is cbtPlus Then InsertText("""")
            If sender Is cbt0 Then InsertText(" ")
            If sender Is cbtDot Then InsertText(":")
            If sender Is cbtNeg Then InsertText("?")

        End If


        'CType(sender, AxMSForms.AxCommandButton).Enabled = False
        txtProg.Focus()
        'CType(sender, AxMSForms.AxCommandButton).Enabled = True
    End Sub

    Private Sub GenerateMenu(ByVal strPath As String, ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim ptMouse As New Point

        ptMouse = CType(sender, Control).Location
        ptMouse.X += tabCommand.Location.X
        ptMouse.Y += tabCommand.Location.Y
        ptMouse.X += CInt(e.X)
        ptMouse.Y += CInt(e.Y)
        ptMouse.X += 16
        ptMouse.Y += 48

        If ptMouse.Y + ButtonList.Height > Me.Height Then
            'is off screen.  line up bottom left corner instead
            ptMouse.Y = ptMouse.Y - ButtonList.Height + 24
        End If

        ButtonList.LoadPath(strPath)

        ButtonList.Location = ptMouse
        ButtonList.Visible = True
    End Sub

    Private Sub ButtonList_CommandSelected(ByVal strCommand As String) Handles ButtonList.CommandSelected
        InsertText(strCommand)
        ButtonList.Visible = False
    End Sub


    Private Sub cbtAlpha_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbtAlpha.Click
        MessageBox.Show("To select ALPHA characters, click the desired calculator key with the mouse wheel.", "ALPHA Button", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub cbt2nd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbt2nd.Click
        MessageBox.Show("To select 2nd characters, click the desired calculator key with the right mouse button.", "2nd Button", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub tvwTokens_BeforeCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwTokens.BeforeCollapse
        'If Not CType(sender, TreeView).SelectedNode Is Nothing Then
        ' If CType(sender, TreeView).SelectedNode.ImageIndex = 0 Then CType(sender, TreeView).SelectedNode.ImageIndex = 1
        'End If
    End Sub

    Private Sub tvwTokens_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwTokens.BeforeExpand
        'If Not CType(sender, TreeView).SelectedNode Is Nothing Then
        ' If CType(sender, TreeView).SelectedNode.ImageIndex = 1 Then CType(sender, TreeView).SelectedNode.ImageIndex = 0
        'End If
    End Sub



    Private Sub mnuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectAll.Click
        txtProg.SelectAll()
    End Sub

    Private Sub ClipMon_ClipboardChanged() Handles ClipMon.ClipboardChanged
        If Clipboard.ContainsText = True Then
            mnuPaste.Enabled = True
            tbtPaste.Enabled = True
        Else
            mnuPaste.Enabled = False
            tbtPaste.Enabled = False
        End If
    End Sub

    Private Sub mnuCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCut.Click, tbtCut.Click
        txtProg.Cut()
    End Sub

    Private Sub mnuCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCopy.Click, tbtCopy.Click
        txtProg.Copy()
    End Sub

    Private Sub mnuPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPaste.Click, tbtPaste.Click
        txtProg.Paste()
    End Sub

    Private Sub mnuUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUndo.Click
        txtProg.Undo()
    End Sub


    Private Sub tabKeypad_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tabKeypad.MouseDown
        Dim rtRect As Rectangle
        Dim bolIn As Boolean

        Dim ctlCheck As Control
        Dim ctlMatch As New Control

        For Each ctlCheck In tabKeypad.Controls
            With ctlCheck
                rtRect = New Rectangle(.Location.X, .Location.Y, .Width, .Height)

                If rtRect.Contains(e.X, e.Y) Then
                    ctlMatch = ctlCheck
                    bolIn = True
                    Exit For
                End If
            End With
        Next

        If bolIn = True Then
            Dim eArgs As New System.Windows.Forms.MouseEventArgs(e.Button, e.Clicks, (e.X - ctlMatch.Left), (e.Y - ctlMatch.Top), e.Delta)
            ButtonPush(ctlMatch, eArgs)
        End If


    End Sub


    Public Sub ChangeTip(ByVal sender As Object, ByVal e As MouseEventArgs) Handles _
        cbt0.MouseMove, cbt1.MouseMove, cbt2.MouseMove, cbt3.MouseMove, cbt4.MouseMove, _
       cbt5.MouseMove, cbt6.MouseMove, cbt7.MouseMove, cbt8.MouseMove, cbt9.MouseMove, cbtApps.MouseMove, _
       cbtClear.MouseMove, cbtComma.MouseMove, cbtCos.MouseMove, cbtDel.MouseMove, cbtDiv.MouseMove, _
       cbtDot.MouseMove, cbtEnter.MouseMove, cbtExp.MouseMove, cbtGraph.MouseMove, _
       cbtLeftPar.MouseMove, cbtLn.MouseMove, cbtLog.MouseMove, cbtMath.MouseMove, cbtMinus.MouseMove, cbtMode.MouseMove, _
       cbtNeg.MouseMove, cbtOn.MouseMove, cbtPlus.MouseMove, cbtPrgm.MouseMove, _
       cbtRightPar.MouseMove, cbtSin.MouseMove, cbtStat.MouseMove, cbtSto.MouseMove, cbtTan.MouseMove, _
       cbtTimes.MouseMove, cbtTrace.MouseMove, cbtVars.MouseMove, cbtWindow.MouseMove, _
       cbtXinv.MouseMove, cbtXsq.MouseMove, cbtXT0n.MouseMove, cbtYeq.MouseMove, cbtZoom.MouseMove

        Dim intKeyCode As Integer

        If sender Is cbtTrace Then intKeyCode = 14
        If sender Is cbtXT0n Then intKeyCode = 32
        If sender Is cbtXinv Then intKeyCode = 51
        If sender Is cbtSin Then intKeyCode = 52
        If sender Is cbtCos Then intKeyCode = 53
        If sender Is cbtTan Then intKeyCode = 54
        If sender Is cbtExp Then intKeyCode = 55
        If sender Is cbtXsq Then intKeyCode = 61
        If sender Is cbtComma Then intKeyCode = 62
        If sender Is cbtRightPar Then intKeyCode = 63
        If sender Is cbtLeftPar Then intKeyCode = 64
        If sender Is cbtLog Then intKeyCode = 71
        If sender Is cbtLn Then intKeyCode = 81
        If sender Is cbtSto Then intKeyCode = 91
        If sender Is cbt0 Then intKeyCode = 102
        If sender Is cbt1 Then intKeyCode = 92
        If sender Is cbt2 Then intKeyCode = 93
        If sender Is cbt3 Then intKeyCode = 94
        If sender Is cbt4 Then intKeyCode = 82
        If sender Is cbt5 Then intKeyCode = 83
        If sender Is cbt6 Then intKeyCode = 84
        If sender Is cbt7 Then intKeyCode = 72
        If sender Is cbt8 Then intKeyCode = 73
        If sender Is cbt9 Then intKeyCode = 74
        If sender Is cbtDot Then intKeyCode = 103
        If sender Is cbtNeg Then intKeyCode = 104
        If sender Is cbtEnter Then intKeyCode = 105
        If sender Is cbtPlus Then intKeyCode = 95
        If sender Is cbtMinus Then intKeyCode = 85
        If sender Is cbtTimes Then intKeyCode = 75
        If sender Is cbtDiv Then intKeyCode = 65

        If sender Is cbtStat Then intKeyCode = 33
        If sender Is cbtMath Then intKeyCode = 41
        If sender Is cbtApps Then intKeyCode = 42
        If sender Is cbtPrgm Then intKeyCode = 43
        If sender Is cbtVars Then intKeyCode = 44
        If sender Is cbtMode Then intKeyCode = 22

        If sender Is cbtYeq Then intKeyCode = 11
        If sender Is cbtWindow Then intKeyCode = 12
        If sender Is cbtZoom Then intKeyCode = 13
        If sender Is cbtGraph Then intKeyCode = 14
        If sender Is cbt2nd Then intKeyCode = 21
        If sender Is cbtAlpha Then intKeyCode = 31
        If sender Is cbtDel Then intKeyCode = 23
        If sender Is cbtClear Then intKeyCode = 45



        KeyCode(intKeyCode)

    End Sub

    Private Sub KeyCode(ByVal intKeyCode As Integer)
        txtCode.Text = intKeyCode.ToString
    End Sub

    Private Sub tabKeypad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabKeypad.Click

    End Sub
End Class
