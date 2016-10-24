<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ButtonList
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.lblFont = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Tabs
        '
        Me.Tabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.Tabs.Location = New System.Drawing.Point(0, 0)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(168, 168)
        Me.Tabs.TabIndex = 0
        '
        'lblFont
        '
        Me.lblFont.AutoSize = True
        Me.lblFont.Font = New System.Drawing.Font("Ti83Pluspc", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFont.Location = New System.Drawing.Point(8, 232)
        Me.lblFont.Name = "lblFont"
        Me.lblFont.Size = New System.Drawing.Size(33, 11)
        Me.lblFont.TabIndex = 1
        Me.lblFont.Text = "Font"
        Me.lblFont.Visible = False
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(120, 168)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(48, 20)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.BackColor = System.Drawing.SystemColors.Control
        Me.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPath.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtPath.Location = New System.Drawing.Point(8, 170)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(104, 13)
        Me.txtPath.TabIndex = 3
        '
        'ButtonList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.lblFont)
        Me.Controls.Add(Me.Tabs)
        Me.Name = "ButtonList"
        Me.Size = New System.Drawing.Size(172, 191)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents lblFont As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox

End Class
