<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportacionCartas
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportacionCartas))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lstCartas = New System.Windows.Forms.ListBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cboCantDeudas2 = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtpFechaSuspension = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dtpHoraCitacion2 = New System.Windows.Forms.DateTimePicker
        Me.cboCantDeudas1 = New System.Windows.Forms.ComboBox
        Me.dtpHoraCitacion1 = New System.Windows.Forms.DateTimePicker
        Me.dtpFechaCitacion = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboAnioAcademico = New System.Windows.Forms.ComboBox
        Me.dtpFechaVcto = New System.Windows.Forms.DateTimePicker
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rbFormatoSalon = New System.Windows.Forms.RadioButton
        Me.rbFormatoFamilia = New System.Windows.Forms.RadioButton
        Me.PanelExportar = New System.Windows.Forms.Panel
        Me.lblMensajePanelDeudas = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblMensajeExportacion = New System.Windows.Forms.Label
        Me.btnExportar = New System.Windows.Forms.Button
        Me.lblRuta = New System.Windows.Forms.Label
        Me.cboGradoIni = New System.Windows.Forms.ComboBox
        Me.cboGradoFin = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.PanelExportar.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstCartas)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 284)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tipo de Reporte"
        '
        'lstCartas
        '
        Me.lstCartas.FormattingEnabled = True
        Me.lstCartas.Location = New System.Drawing.Point(6, 19)
        Me.lstCartas.Name = "lstCartas"
        Me.lstCartas.Size = New System.Drawing.Size(188, 251)
        Me.lstCartas.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.cboGradoFin)
        Me.GroupBox2.Controls.Add(Me.cboGradoIni)
        Me.GroupBox2.Controls.Add(Me.Panel2)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cboAnioAcademico)
        Me.GroupBox2.Controls.Add(Me.dtpFechaVcto)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(218, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(393, 284)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Parámetros"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cboCantDeudas2)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.dtpFechaSuspension)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Location = New System.Drawing.Point(6, 212)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(344, 52)
        Me.Panel2.TabIndex = 33
        '
        'cboCantDeudas2
        '
        Me.cboCantDeudas2.FormattingEnabled = True
        Me.cboCantDeudas2.Location = New System.Drawing.Point(114, 27)
        Me.cboCantDeudas2.Name = "cboCantDeudas2"
        Me.cboCantDeudas2.Size = New System.Drawing.Size(103, 21)
        Me.cboCantDeudas2.TabIndex = 34
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 13)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Cantidad Deudas: "
        '
        'dtpFechaSuspension
        '
        Me.dtpFechaSuspension.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaSuspension.Location = New System.Drawing.Point(114, 1)
        Me.dtpFechaSuspension.Name = "dtpFechaSuspension"
        Me.dtpFechaSuspension.Size = New System.Drawing.Size(103, 20)
        Me.dtpFechaSuspension.TabIndex = 31
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Fecha Suspensión:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dtpHoraCitacion2)
        Me.Panel1.Controls.Add(Me.cboCantDeudas1)
        Me.Panel1.Controls.Add(Me.dtpHoraCitacion1)
        Me.Panel1.Controls.Add(Me.dtpFechaCitacion)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(6, 125)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(344, 81)
        Me.Panel1.TabIndex = 0
        '
        'dtpHoraCitacion2
        '
        Me.dtpHoraCitacion2.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraCitacion2.Location = New System.Drawing.Point(224, 31)
        Me.dtpHoraCitacion2.Name = "dtpHoraCitacion2"
        Me.dtpHoraCitacion2.Size = New System.Drawing.Size(103, 20)
        Me.dtpHoraCitacion2.TabIndex = 33
        '
        'cboCantDeudas1
        '
        Me.cboCantDeudas1.FormattingEnabled = True
        Me.cboCantDeudas1.Location = New System.Drawing.Point(114, 57)
        Me.cboCantDeudas1.Name = "cboCantDeudas1"
        Me.cboCantDeudas1.Size = New System.Drawing.Size(103, 21)
        Me.cboCantDeudas1.TabIndex = 32
        '
        'dtpHoraCitacion1
        '
        Me.dtpHoraCitacion1.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraCitacion1.Location = New System.Drawing.Point(115, 31)
        Me.dtpHoraCitacion1.Name = "dtpHoraCitacion1"
        Me.dtpHoraCitacion1.Size = New System.Drawing.Size(103, 20)
        Me.dtpHoraCitacion1.TabIndex = 32
        '
        'dtpFechaCitacion
        '
        Me.dtpFechaCitacion.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaCitacion.Location = New System.Drawing.Point(114, 5)
        Me.dtpFechaCitacion.Name = "dtpFechaCitacion"
        Me.dtpFechaCitacion.Size = New System.Drawing.Size(103, 20)
        Me.dtpFechaCitacion.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Intervalo de Hora: "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Cantidad Deudas: "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Fecha Citación:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Fecha de Vcto:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Año Academico:"
        '
        'cboAnioAcademico
        '
        Me.cboAnioAcademico.FormattingEnabled = True
        Me.cboAnioAcademico.Location = New System.Drawing.Point(115, 19)
        Me.cboAnioAcademico.Name = "cboAnioAcademico"
        Me.cboAnioAcademico.Size = New System.Drawing.Size(103, 21)
        Me.cboAnioAcademico.TabIndex = 0
        '
        'dtpFechaVcto
        '
        Me.dtpFechaVcto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaVcto.Location = New System.Drawing.Point(115, 99)
        Me.dtpFechaVcto.Name = "dtpFechaVcto"
        Me.dtpFechaVcto.Size = New System.Drawing.Size(103, 20)
        Me.dtpFechaVcto.TabIndex = 30
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbFormatoSalon)
        Me.GroupBox3.Controls.Add(Me.rbFormatoFamilia)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 46)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(212, 45)
        Me.GroupBox3.TabIndex = 29
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Formato"
        '
        'rbFormatoSalon
        '
        Me.rbFormatoSalon.AutoSize = True
        Me.rbFormatoSalon.Location = New System.Drawing.Point(88, 19)
        Me.rbFormatoSalon.Name = "rbFormatoSalon"
        Me.rbFormatoSalon.Size = New System.Drawing.Size(71, 17)
        Me.rbFormatoSalon.TabIndex = 4
        Me.rbFormatoSalon.TabStop = True
        Me.rbFormatoSalon.Text = "Por Salón"
        Me.rbFormatoSalon.UseVisualStyleBackColor = True
        '
        'rbFormatoFamilia
        '
        Me.rbFormatoFamilia.AutoSize = True
        Me.rbFormatoFamilia.Location = New System.Drawing.Point(6, 19)
        Me.rbFormatoFamilia.Name = "rbFormatoFamilia"
        Me.rbFormatoFamilia.Size = New System.Drawing.Size(76, 17)
        Me.rbFormatoFamilia.TabIndex = 3
        Me.rbFormatoFamilia.TabStop = True
        Me.rbFormatoFamilia.Text = "Por Familia"
        Me.rbFormatoFamilia.UseVisualStyleBackColor = True
        '
        'PanelExportar
        '
        Me.PanelExportar.BackColor = System.Drawing.Color.White
        Me.PanelExportar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelExportar.Controls.Add(Me.lblMensajePanelDeudas)
        Me.PanelExportar.Controls.Add(Me.PictureBox2)
        Me.PanelExportar.Location = New System.Drawing.Point(620, 59)
        Me.PanelExportar.Name = "PanelExportar"
        Me.PanelExportar.Size = New System.Drawing.Size(348, 35)
        Me.PanelExportar.TabIndex = 35
        '
        'lblMensajePanelDeudas
        '
        Me.lblMensajePanelDeudas.AutoSize = True
        Me.lblMensajePanelDeudas.Location = New System.Drawing.Point(38, 10)
        Me.lblMensajePanelDeudas.Name = "lblMensajePanelDeudas"
        Me.lblMensajePanelDeudas.Size = New System.Drawing.Size(64, 13)
        Me.lblMensajePanelDeudas.TabIndex = 1
        Me.lblMensajePanelDeudas.Text = "Procesando"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.SaintGeorgeOnline_Listeners.My.Resources.Resources.ajax_loading
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'lblMensajeExportacion
        '
        Me.lblMensajeExportacion.AutoSize = True
        Me.lblMensajeExportacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensajeExportacion.Location = New System.Drawing.Point(620, 97)
        Me.lblMensajeExportacion.Name = "lblMensajeExportacion"
        Me.lblMensajeExportacion.Size = New System.Drawing.Size(53, 13)
        Me.lblMensajeExportacion.TabIndex = 34
        Me.lblMensajeExportacion.Text = "Mensaje: "
        '
        'btnExportar
        '
        Me.btnExportar.BackgroundImage = CType(resources.GetObject("btnExportar.BackgroundImage"), System.Drawing.Image)
        Me.btnExportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnExportar.Location = New System.Drawing.Point(620, 19)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(85, 33)
        Me.btnExportar.TabIndex = 28
        Me.btnExportar.Text = "Exportar"
        Me.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExportar.UseVisualStyleBackColor = True
        '
        'lblRuta
        '
        Me.lblRuta.AutoSize = True
        Me.lblRuta.Location = New System.Drawing.Point(620, 118)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(25, 13)
        Me.lblRuta.TabIndex = 36
        Me.lblRuta.Text = "ruta"
        '
        'cboGradoIni
        '
        Me.cboGradoIni.FormattingEnabled = True
        Me.cboGradoIni.Location = New System.Drawing.Point(286, 19)
        Me.cboGradoIni.Name = "cboGradoIni"
        Me.cboGradoIni.Size = New System.Drawing.Size(99, 21)
        Me.cboGradoIni.TabIndex = 34
        '
        'cboGradoFin
        '
        Me.cboGradoFin.FormattingEnabled = True
        Me.cboGradoFin.Location = New System.Drawing.Point(286, 61)
        Me.cboGradoFin.Name = "cboGradoFin"
        Me.cboGradoFin.Size = New System.Drawing.Size(99, 21)
        Me.cboGradoFin.TabIndex = 35
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(227, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Grado Ini:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(227, 64)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 13)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Grado Fin:"
        '
        'frmExportacionCartas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(977, 305)
        Me.Controls.Add(Me.lblRuta)
        Me.Controls.Add(Me.PanelExportar)
        Me.Controls.Add(Me.lblMensajeExportacion)
        Me.Controls.Add(Me.btnExportar)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmExportacionCartas"
        Me.Text = "Exportación de Cartas de Morosidad"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.PanelExportar.ResumeLayout(False)
        Me.PanelExportar.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstCartas As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExportar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboAnioAcademico As System.Windows.Forms.ComboBox
    Friend WithEvents rbFormatoSalon As System.Windows.Forms.RadioButton
    Friend WithEvents rbFormatoFamilia As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaVcto As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboCantDeudas1 As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFechaCitacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpHoraCitacion2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHoraCitacion1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dtpFechaSuspension As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboCantDeudas2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PanelExportar As System.Windows.Forms.Panel
    Friend WithEvents lblMensajePanelDeudas As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblMensajeExportacion As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboGradoFin As System.Windows.Forms.ComboBox
    Friend WithEvents cboGradoIni As System.Windows.Forms.ComboBox
End Class
