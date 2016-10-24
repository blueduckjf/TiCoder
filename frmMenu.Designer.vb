<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenu
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtlbl1 = New System.Windows.Forms.TextBox
        Me.txt1 = New System.Windows.Forms.TextBox
        Me.chk1 = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtlbl2 = New System.Windows.Forms.TextBox
        Me.txt2 = New System.Windows.Forms.TextBox
        Me.chk2 = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtlbl3 = New System.Windows.Forms.TextBox
        Me.txt3 = New System.Windows.Forms.TextBox
        Me.chk3 = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtlbl4 = New System.Windows.Forms.TextBox
        Me.txt4 = New System.Windows.Forms.TextBox
        Me.chk4 = New System.Windows.Forms.CheckBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.txtlbl5 = New System.Windows.Forms.TextBox
        Me.txt5 = New System.Windows.Forms.TextBox
        Me.chk5 = New System.Windows.Forms.CheckBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.txtlbl6 = New System.Windows.Forms.TextBox
        Me.txt6 = New System.Windows.Forms.TextBox
        Me.chk6 = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.txtlbl7 = New System.Windows.Forms.TextBox
        Me.txt7 = New System.Windows.Forms.TextBox
        Me.chk7 = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdOK, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdCancel, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(148, 371)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(207, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdOK.Location = New System.Drawing.Point(7, 3)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(89, 23)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "Insert"
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(106, 3)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(98, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Menu Title:"
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(16, 24)
        Me.txtTitle.MaxLength = 16
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(176, 20)
        Me.txtTitle.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Menu Items:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtlbl1)
        Me.GroupBox1.Controls.Add(Me.txt1)
        Me.GroupBox1.Controls.Add(Me.chk1)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'txtlbl1
        '
        Me.txtlbl1.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlbl1.Location = New System.Drawing.Point(184, 13)
        Me.txtlbl1.Name = "txtlbl1"
        Me.txtlbl1.Size = New System.Drawing.Size(144, 20)
        Me.txtlbl1.TabIndex = 2
        Me.txtlbl1.Text = "Destination Label"
        '
        'txt1
        '
        Me.txt1.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt1.Location = New System.Drawing.Point(32, 13)
        Me.txt1.MaxLength = 14
        Me.txt1.Name = "txt1"
        Me.txt1.Size = New System.Drawing.Size(144, 20)
        Me.txt1.TabIndex = 1
        Me.txt1.Text = "Menu Item"
        '
        'chk1
        '
        Me.chk1.AutoSize = True
        Me.chk1.Checked = True
        Me.chk1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk1.Location = New System.Drawing.Point(8, 17)
        Me.chk1.Name = "chk1"
        Me.chk1.Size = New System.Drawing.Size(15, 14)
        Me.chk1.TabIndex = 0
        Me.chk1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtlbl2)
        Me.GroupBox2.Controls.Add(Me.txt2)
        Me.GroupBox2.Controls.Add(Me.chk2)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 112)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'txtlbl2
        '
        Me.txtlbl2.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlbl2.Location = New System.Drawing.Point(184, 13)
        Me.txtlbl2.Name = "txtlbl2"
        Me.txtlbl2.Size = New System.Drawing.Size(144, 20)
        Me.txtlbl2.TabIndex = 2
        '
        'txt2
        '
        Me.txt2.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt2.Location = New System.Drawing.Point(32, 13)
        Me.txt2.MaxLength = 14
        Me.txt2.Name = "txt2"
        Me.txt2.Size = New System.Drawing.Size(144, 20)
        Me.txt2.TabIndex = 1
        '
        'chk2
        '
        Me.chk2.AutoSize = True
        Me.chk2.Location = New System.Drawing.Point(8, 17)
        Me.chk2.Name = "chk2"
        Me.chk2.Size = New System.Drawing.Size(15, 14)
        Me.chk2.TabIndex = 0
        Me.chk2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtlbl3)
        Me.GroupBox3.Controls.Add(Me.txt3)
        Me.GroupBox3.Controls.Add(Me.chk3)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 152)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        '
        'txtlbl3
        '
        Me.txtlbl3.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlbl3.Location = New System.Drawing.Point(184, 13)
        Me.txtlbl3.Name = "txtlbl3"
        Me.txtlbl3.Size = New System.Drawing.Size(144, 20)
        Me.txtlbl3.TabIndex = 2
        '
        'txt3
        '
        Me.txt3.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt3.Location = New System.Drawing.Point(32, 13)
        Me.txt3.MaxLength = 14
        Me.txt3.Name = "txt3"
        Me.txt3.Size = New System.Drawing.Size(144, 20)
        Me.txt3.TabIndex = 1
        '
        'chk3
        '
        Me.chk3.AutoSize = True
        Me.chk3.Location = New System.Drawing.Point(8, 17)
        Me.chk3.Name = "chk3"
        Me.chk3.Size = New System.Drawing.Size(15, 14)
        Me.chk3.TabIndex = 0
        Me.chk3.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtlbl4)
        Me.GroupBox4.Controls.Add(Me.txt4)
        Me.GroupBox4.Controls.Add(Me.chk4)
        Me.GroupBox4.Location = New System.Drawing.Point(16, 192)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        '
        'txtlbl4
        '
        Me.txtlbl4.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlbl4.Location = New System.Drawing.Point(184, 13)
        Me.txtlbl4.Name = "txtlbl4"
        Me.txtlbl4.Size = New System.Drawing.Size(144, 20)
        Me.txtlbl4.TabIndex = 2
        '
        'txt4
        '
        Me.txt4.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt4.Location = New System.Drawing.Point(32, 13)
        Me.txt4.MaxLength = 14
        Me.txt4.Name = "txt4"
        Me.txt4.Size = New System.Drawing.Size(144, 20)
        Me.txt4.TabIndex = 1
        '
        'chk4
        '
        Me.chk4.AutoSize = True
        Me.chk4.Location = New System.Drawing.Point(8, 17)
        Me.chk4.Name = "chk4"
        Me.chk4.Size = New System.Drawing.Size(15, 14)
        Me.chk4.TabIndex = 0
        Me.chk4.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtlbl5)
        Me.GroupBox5.Controls.Add(Me.txt5)
        Me.GroupBox5.Controls.Add(Me.chk5)
        Me.GroupBox5.Location = New System.Drawing.Point(16, 232)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox5.TabIndex = 8
        Me.GroupBox5.TabStop = False
        '
        'txtlbl5
        '
        Me.txtlbl5.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlbl5.Location = New System.Drawing.Point(184, 13)
        Me.txtlbl5.Name = "txtlbl5"
        Me.txtlbl5.Size = New System.Drawing.Size(144, 20)
        Me.txtlbl5.TabIndex = 2
        '
        'txt5
        '
        Me.txt5.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt5.Location = New System.Drawing.Point(32, 13)
        Me.txt5.MaxLength = 14
        Me.txt5.Name = "txt5"
        Me.txt5.Size = New System.Drawing.Size(144, 20)
        Me.txt5.TabIndex = 1
        '
        'chk5
        '
        Me.chk5.AutoSize = True
        Me.chk5.Location = New System.Drawing.Point(8, 17)
        Me.chk5.Name = "chk5"
        Me.chk5.Size = New System.Drawing.Size(15, 14)
        Me.chk5.TabIndex = 0
        Me.chk5.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtlbl6)
        Me.GroupBox6.Controls.Add(Me.txt6)
        Me.GroupBox6.Controls.Add(Me.chk6)
        Me.GroupBox6.Location = New System.Drawing.Point(16, 272)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox6.TabIndex = 9
        Me.GroupBox6.TabStop = False
        '
        'txtlbl6
        '
        Me.txtlbl6.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlbl6.Location = New System.Drawing.Point(184, 13)
        Me.txtlbl6.Name = "txtlbl6"
        Me.txtlbl6.Size = New System.Drawing.Size(144, 20)
        Me.txtlbl6.TabIndex = 2
        '
        'txt6
        '
        Me.txt6.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt6.Location = New System.Drawing.Point(32, 13)
        Me.txt6.MaxLength = 14
        Me.txt6.Name = "txt6"
        Me.txt6.Size = New System.Drawing.Size(144, 20)
        Me.txt6.TabIndex = 1
        '
        'chk6
        '
        Me.chk6.AutoSize = True
        Me.chk6.Location = New System.Drawing.Point(8, 17)
        Me.chk6.Name = "chk6"
        Me.chk6.Size = New System.Drawing.Size(15, 14)
        Me.chk6.TabIndex = 0
        Me.chk6.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtlbl7)
        Me.GroupBox7.Controls.Add(Me.txt7)
        Me.GroupBox7.Controls.Add(Me.chk7)
        Me.GroupBox7.Location = New System.Drawing.Point(16, 312)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox7.TabIndex = 10
        Me.GroupBox7.TabStop = False
        '
        'txtlbl7
        '
        Me.txtlbl7.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlbl7.Location = New System.Drawing.Point(184, 13)
        Me.txtlbl7.Name = "txtlbl7"
        Me.txtlbl7.Size = New System.Drawing.Size(144, 20)
        Me.txtlbl7.TabIndex = 2
        '
        'txt7
        '
        Me.txt7.Font = New System.Drawing.Font("Ti83Pluspc", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt7.Location = New System.Drawing.Point(32, 13)
        Me.txt7.MaxLength = 14
        Me.txt7.Name = "txt7"
        Me.txt7.Size = New System.Drawing.Size(144, 20)
        Me.txt7.TabIndex = 1
        '
        'chk7
        '
        Me.chk7.AutoSize = True
        Me.chk7.Location = New System.Drawing.Point(8, 17)
        Me.chk7.Name = "chk7"
        Me.chk7.Size = New System.Drawing.Size(15, 14)
        Me.chk7.TabIndex = 0
        Me.chk7.UseVisualStyleBackColor = True
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(367, 412)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMenu"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Menu Designer"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtlbl1 As System.Windows.Forms.TextBox
    Friend WithEvents txt1 As System.Windows.Forms.TextBox
    Friend WithEvents chk1 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtlbl2 As System.Windows.Forms.TextBox
    Friend WithEvents txt2 As System.Windows.Forms.TextBox
    Friend WithEvents chk2 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtlbl3 As System.Windows.Forms.TextBox
    Friend WithEvents txt3 As System.Windows.Forms.TextBox
    Friend WithEvents chk3 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtlbl4 As System.Windows.Forms.TextBox
    Friend WithEvents txt4 As System.Windows.Forms.TextBox
    Friend WithEvents chk4 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtlbl5 As System.Windows.Forms.TextBox
    Friend WithEvents txt5 As System.Windows.Forms.TextBox
    Friend WithEvents chk5 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtlbl6 As System.Windows.Forms.TextBox
    Friend WithEvents txt6 As System.Windows.Forms.TextBox
    Friend WithEvents chk6 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtlbl7 As System.Windows.Forms.TextBox
    Friend WithEvents txt7 As System.Windows.Forms.TextBox
    Friend WithEvents chk7 As System.Windows.Forms.CheckBox

End Class
