<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmToken
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdSave = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstTokens = New System.Windows.Forms.ListBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtText = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBytes = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.cmdByte = New System.Windows.Forms.Button
        Me.cmdText = New System.Windows.Forms.Button
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(248, 344)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(96, 24)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "tokens.tkn"
        '
        'lstTokens
        '
        Me.lstTokens.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTokens.FormattingEnabled = True
        Me.lstTokens.Location = New System.Drawing.Point(16, 24)
        Me.lstTokens.Name = "lstTokens"
        Me.lstTokens.Size = New System.Drawing.Size(224, 342)
        Me.lstTokens.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(248, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Token Text:"
        '
        'txtText
        '
        Me.txtText.Enabled = False
        Me.txtText.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtText.Location = New System.Drawing.Point(256, 40)
        Me.txtText.Name = "txtText"
        Me.txtText.Size = New System.Drawing.Size(192, 20)
        Me.txtText.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(248, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Token Bytes:"
        '
        'txtBytes
        '
        Me.txtBytes.Enabled = False
        Me.txtBytes.Location = New System.Drawing.Point(256, 80)
        Me.txtBytes.MaxLength = 4
        Me.txtBytes.Name = "txtBytes"
        Me.txtBytes.Size = New System.Drawing.Size(32, 21)
        Me.txtBytes.TabIndex = 8
        Me.txtBytes.Text = "FFFF"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(248, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Path in Tree:"
        '
        'txtPath
        '
        Me.txtPath.Enabled = False
        Me.txtPath.Location = New System.Drawing.Point(256, 128)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(192, 21)
        Me.txtPath.TabIndex = 10
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(352, 344)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(96, 24)
        Me.cmdCancel.TabIndex = 11
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(248, 192)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(96, 24)
        Me.cmdAdd.TabIndex = 12
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Enabled = False
        Me.cmdRemove.Location = New System.Drawing.Point(352, 192)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(96, 24)
        Me.cmdRemove.TabIndex = 13
        Me.cmdRemove.Text = "Remove"
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdByte
        '
        Me.cmdByte.Location = New System.Drawing.Point(248, 160)
        Me.cmdByte.Name = "cmdByte"
        Me.cmdByte.Size = New System.Drawing.Size(96, 24)
        Me.cmdByte.TabIndex = 14
        Me.cmdByte.Text = "Search - Byte"
        Me.cmdByte.UseVisualStyleBackColor = True
        '
        'cmdText
        '
        Me.cmdText.Location = New System.Drawing.Point(352, 160)
        Me.cmdText.Name = "cmdText"
        Me.cmdText.Size = New System.Drawing.Size(96, 24)
        Me.cmdText.TabIndex = 15
        Me.cmdText.Text = "Search - Text"
        Me.cmdText.UseVisualStyleBackColor = True
        '
        'cmdLoad
        '
        Me.cmdLoad.Location = New System.Drawing.Point(248, 312)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(96, 24)
        Me.cmdLoad.TabIndex = 16
        Me.cmdLoad.Text = "Load from DB"
        Me.cmdLoad.UseVisualStyleBackColor = True
        Me.cmdLoad.Visible = False
        '
        'frmToken
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(457, 378)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.cmdText)
        Me.Controls.Add(Me.cmdByte)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtBytes)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtText)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lstTokens)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdSave)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmToken"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Token File Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstTokens As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBytes As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdByte As System.Windows.Forms.Button
    Friend WithEvents cmdText As System.Windows.Forms.Button
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
End Class
