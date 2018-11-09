<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class personal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(personal))
        Me.LBLTITULO = New System.Windows.Forms.Label()
        Me.txtdui = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnSi = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnsalir = New System.Windows.Forms.Button()
        Me.btnbuscar = New System.Windows.Forms.Button()
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.Lbl1 = New System.Windows.Forms.Label()
        Me.txtnombre1 = New System.Windows.Forms.TextBox()
        Me.txtape1 = New System.Windows.Forms.TextBox()
        Me.txtjrv = New System.Windows.Forms.TextBox()
        Me.cmbcargo = New System.Windows.Forms.ComboBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LOGO = New System.Windows.Forms.PictureBox()
        Me.btningresar = New System.Windows.Forms.Button()
        Me.cbProcedencia = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MENSAJESALIR = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LOGO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LBLTITULO
        '
        Me.LBLTITULO.AutoSize = True
        Me.LBLTITULO.Font = New System.Drawing.Font("Book Antiqua", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTITULO.ForeColor = System.Drawing.Color.Navy
        Me.LBLTITULO.Location = New System.Drawing.Point(315, 9)
        Me.LBLTITULO.Name = "LBLTITULO"
        Me.LBLTITULO.Size = New System.Drawing.Size(594, 63)
        Me.LBLTITULO.TabIndex = 2
        Me.LBLTITULO.Text = "SISTEMA PAGOS JRV" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ELECCIONES 2018" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DIPUTACIONES A ASAMBLEA LEGISLATIVA Y CONSEJO" & _
            "S MUNICIPALES"
        Me.LBLTITULO.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtdui
        '
        Me.txtdui.Location = New System.Drawing.Point(60, 107)
        Me.txtdui.MaxLength = 10
        Me.txtdui.Name = "txtdui"
        Me.txtdui.Size = New System.Drawing.Size(221, 20)
        Me.txtdui.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial Rounded MT Bold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(118, 533)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(165, 22)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Datos Correctos:"
        '
        'btnSi
        '
        Me.btnSi.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSi.ForeColor = System.Drawing.Color.Green
        Me.btnSi.Location = New System.Drawing.Point(349, 533)
        Me.btnSi.Name = "btnSi"
        Me.btnSi.Size = New System.Drawing.Size(58, 33)
        Me.btnSi.TabIndex = 20
        Me.btnSi.Text = "Si"
        Me.btnSi.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(479, 533)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(58, 33)
        Me.Button2.TabIndex = 21
        Me.Button2.Text = "No"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Font = New System.Drawing.Font("Bookman Old Style", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Location = New System.Drawing.Point(270, 586)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(101, 35)
        Me.btnCancelar.TabIndex = 22
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnsalir
        '
        Me.btnsalir.Font = New System.Drawing.Font("Bookman Old Style", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsalir.Location = New System.Drawing.Point(598, 586)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(101, 36)
        Me.btnsalir.TabIndex = 23
        Me.btnsalir.Text = "Salir"
        Me.btnsalir.UseVisualStyleBackColor = True
        '
        'btnbuscar
        '
        Me.btnbuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbuscar.Location = New System.Drawing.Point(298, 98)
        Me.btnbuscar.Name = "btnbuscar"
        Me.btnbuscar.Size = New System.Drawing.Size(96, 38)
        Me.btnbuscar.TabIndex = 24
        Me.btnbuscar.Text = "Buscar"
        Me.btnbuscar.UseVisualStyleBackColor = True
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToDeleteRows = False
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Location = New System.Drawing.Point(12, 254)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.ReadOnly = True
        Me.dgvData.Size = New System.Drawing.Size(687, 239)
        Me.dgvData.TabIndex = 26
        '
        'Lbl1
        '
        Me.Lbl1.AutoSize = True
        Me.Lbl1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl1.Location = New System.Drawing.Point(12, 109)
        Me.Lbl1.Name = "Lbl1"
        Me.Lbl1.Size = New System.Drawing.Size(42, 18)
        Me.Lbl1.TabIndex = 27
        Me.Lbl1.Text = "DUI:"
        '
        'txtnombre1
        '
        Me.txtnombre1.Location = New System.Drawing.Point(12, 152)
        Me.txtnombre1.Name = "txtnombre1"
        Me.txtnombre1.Size = New System.Drawing.Size(206, 20)
        Me.txtnombre1.TabIndex = 31
        '
        'txtape1
        '
        Me.txtape1.Location = New System.Drawing.Point(224, 152)
        Me.txtape1.Name = "txtape1"
        Me.txtape1.Size = New System.Drawing.Size(204, 20)
        Me.txtape1.TabIndex = 33
        '
        'txtjrv
        '
        Me.txtjrv.Location = New System.Drawing.Point(583, 110)
        Me.txtjrv.Name = "txtjrv"
        Me.txtjrv.Size = New System.Drawing.Size(51, 20)
        Me.txtjrv.TabIndex = 37
        '
        'cmbcargo
        '
        Me.cmbcargo.FormattingEnabled = True
        Me.cmbcargo.Items.AddRange(New Object() {"Presidente", "Secretario", "Vocal1", "Vocal2", "Vocal3", "Vigilante"})
        Me.cmbcargo.Location = New System.Drawing.Point(434, 152)
        Me.cmbcargo.Name = "cmbcargo"
        Me.cmbcargo.Size = New System.Drawing.Size(121, 21)
        Me.cmbcargo.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.planilla.My.Resources.Resources.ESCUDO
        Me.PictureBox2.Location = New System.Drawing.Point(1258, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 86)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 39
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.planilla.My.Resources.Resources.LOGO_TSE
        Me.PictureBox1.Location = New System.Drawing.Point(15, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(146, 66)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 25
        Me.PictureBox1.TabStop = False
        '
        'LOGO
        '
        Me.LOGO.Location = New System.Drawing.Point(37, 16)
        Me.LOGO.Name = "LOGO"
        Me.LOGO.Size = New System.Drawing.Size(530, 350)
        Me.LOGO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LOGO.TabIndex = 4
        Me.LOGO.TabStop = False
        '
        'btningresar
        '
        Me.btningresar.Location = New System.Drawing.Point(588, 216)
        Me.btningresar.Name = "btningresar"
        Me.btningresar.Size = New System.Drawing.Size(75, 23)
        Me.btningresar.TabIndex = 42
        Me.btningresar.Text = "INGRESAR"
        Me.btningresar.UseVisualStyleBackColor = True
        '
        'cbProcedencia
        '
        Me.cbProcedencia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProcedencia.FormattingEnabled = True
        Me.cbProcedencia.Items.AddRange(New Object() {"ARENA", "FMLN", "GANA", "PCN", "PDC", "CD", "DS", "PSD", "PSP", "FPS"})
        Me.cbProcedencia.Location = New System.Drawing.Point(570, 152)
        Me.cbProcedencia.Name = "cbProcedencia"
        Me.cbProcedencia.Size = New System.Drawing.Size(129, 23)
        Me.cbProcedencia.TabIndex = 45
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 175)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Nombres"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(221, 175)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Apellidos"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(434, 175)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Cargo en JRV"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(567, 176)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Procedencia"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.LOGO)
        Me.Panel1.Location = New System.Drawing.Point(705, 176)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(663, 414)
        Me.Panel1.TabIndex = 50
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(874, 596)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(68, 57)
        Me.Button1.TabIndex = 51
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.Location = New System.Drawing.Point(1204, 596)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(68, 57)
        Me.Button6.TabIndex = 52
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(981, 604)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(188, 40)
        Me.Button7.TabIndex = 53
        Me.Button7.Text = "Cambiar Imagen Asamblea"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Modern No. 20", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(499, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 21)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "JRV No."
        '
        'MENSAJESALIR
        '
        Me.MENSAJESALIR.AutoSize = True
        Me.MENSAJESALIR.Location = New System.Drawing.Point(476, 604)
        Me.MENSAJESALIR.Name = "MENSAJESALIR"
        Me.MENSAJESALIR.Size = New System.Drawing.Size(39, 13)
        Me.MENSAJESALIR.TabIndex = 55
        Me.MENSAJESALIR.Text = "Label6"
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(981, 112)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(58, 60)
        Me.Button3.TabIndex = 56
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(1062, 110)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(56, 62)
        Me.Button4.TabIndex = 57
        Me.Button4.UseVisualStyleBackColor = False
        '
        'personal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 665)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.MENSAJESALIR)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbProcedencia)
        Me.Controls.Add(Me.btningresar)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.cmbcargo)
        Me.Controls.Add(Me.txtjrv)
        Me.Controls.Add(Me.txtape1)
        Me.Controls.Add(Me.txtnombre1)
        Me.Controls.Add(Me.Lbl1)
        Me.Controls.Add(Me.dgvData)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnbuscar)
        Me.Controls.Add(Me.btnsalir)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnSi)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtdui)
        Me.Controls.Add(Me.LBLTITULO)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "personal"
        Me.Text = "                                                   "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LOGO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LBLTITULO As System.Windows.Forms.Label
    Friend WithEvents LOGO As System.Windows.Forms.PictureBox
    Friend WithEvents txtdui As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSi As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnsalir As System.Windows.Forms.Button
    Friend WithEvents btnbuscar As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
    Friend WithEvents Lbl1 As System.Windows.Forms.Label
    Friend WithEvents txtnombre1 As System.Windows.Forms.TextBox
    Friend WithEvents txtape1 As System.Windows.Forms.TextBox
    Friend WithEvents txtjrv As System.Windows.Forms.TextBox
    Friend WithEvents cmbcargo As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btningresar As System.Windows.Forms.Button
    Friend WithEvents cbProcedencia As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents MENSAJESALIR As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
End Class
