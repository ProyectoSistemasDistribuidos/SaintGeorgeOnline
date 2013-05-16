<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReporteLibretaPrimaria
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
        Me.components = New System.ComponentModel.Container
        Me.lstPresentacion = New System.Windows.Forms.ListBox
        Me.lstReportes = New System.Windows.Forms.ListBox
        Me.SaveFile = New System.Windows.Forms.SaveFileDialog
        Me.btnExportar = New System.Windows.Forms.Button
        Me.cmbSalon = New System.Windows.Forms.ComboBox
        Me.cmbBimestre = New System.Windows.Forms.ComboBox
        Me.lblSalon = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.tmrControlador = New System.Windows.Forms.Timer(Me.components)
        Me.pcbLoading = New System.Windows.Forms.PictureBox
        Me.lblMensajePros = New System.Windows.Forms.Label
        Me.pgbEstadoProceso = New System.Windows.Forms.ProgressBar
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnDetenerProceso = New System.Windows.Forms.Button
        Me.lblNumeroHoja = New System.Windows.Forms.Label
        Me.lblHoja = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.CodigoAlumno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NombreCompleto = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Chk = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblTotalChk = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbidiomaES = New System.Windows.Forms.RadioButton
        Me.rbidiomaEN = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbPeriodo = New System.Windows.Forms.ComboBox
        Me.cmbTipoReporte = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtFechaImpresion = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        CType(Me.pcbLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstPresentacion
        '
        Me.lstPresentacion.FormattingEnabled = True
        Me.lstPresentacion.Location = New System.Drawing.Point(201, 76)
        Me.lstPresentacion.Name = "lstPresentacion"
        Me.lstPresentacion.Size = New System.Drawing.Size(140, 160)
        Me.lstPresentacion.TabIndex = 0
        '
        'lstReportes
        '
        Me.lstReportes.FormattingEnabled = True
        Me.lstReportes.Location = New System.Drawing.Point(46, 76)
        Me.lstReportes.Name = "lstReportes"
        Me.lstReportes.Size = New System.Drawing.Size(139, 160)
        Me.lstReportes.TabIndex = 1
        '
        'btnExportar
        '
        Me.btnExportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnExportar.Location = New System.Drawing.Point(758, 148)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(109, 75)
        Me.btnExportar.TabIndex = 2
        Me.btnExportar.Text = "Exportar Libreta"
        Me.btnExportar.UseVisualStyleBackColor = True
        '
        'cmbSalon
        '
        Me.cmbSalon.FormattingEnabled = True
        Me.cmbSalon.Location = New System.Drawing.Point(444, 270)
        Me.cmbSalon.Name = "cmbSalon"
        Me.cmbSalon.Size = New System.Drawing.Size(308, 21)
        Me.cmbSalon.TabIndex = 3
        '
        'cmbBimestre
        '
        Me.cmbBimestre.FormattingEnabled = True
        Me.cmbBimestre.Location = New System.Drawing.Point(444, 175)
        Me.cmbBimestre.Name = "cmbBimestre"
        Me.cmbBimestre.Size = New System.Drawing.Size(308, 21)
        Me.cmbBimestre.TabIndex = 4
        '
        'lblSalon
        '
        Me.lblSalon.AutoSize = True
        Me.lblSalon.Location = New System.Drawing.Point(381, 273)
        Me.lblSalon.Name = "lblSalon"
        Me.lblSalon.Size = New System.Drawing.Size(34, 13)
        Me.lblSalon.TabIndex = 5
        Me.lblSalon.Text = "Salon"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(381, 178)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Bimestre"
        '
        'tmrControlador
        '
        Me.tmrControlador.Interval = 1
        '
        'pcbLoading
        '
        Me.pcbLoading.Image = Global.SaintGeorgeOnline_Listeners.My.Resources.Resources.ajax_loading
        Me.pcbLoading.Location = New System.Drawing.Point(288, 436)
        Me.pcbLoading.Name = "pcbLoading"
        Me.pcbLoading.Size = New System.Drawing.Size(53, 53)
        Me.pcbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pcbLoading.TabIndex = 7
        Me.pcbLoading.TabStop = False
        '
        'lblMensajePros
        '
        Me.lblMensajePros.AutoSize = True
        Me.lblMensajePros.Font = New System.Drawing.Font("Miriam", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensajePros.Location = New System.Drawing.Point(42, 325)
        Me.lblMensajePros.Name = "lblMensajePros"
        Me.lblMensajePros.Size = New System.Drawing.Size(28, 23)
        Me.lblMensajePros.TabIndex = 8
        Me.lblMensajePros.Text = "..."
        '
        'pgbEstadoProceso
        '
        Me.pgbEstadoProceso.Location = New System.Drawing.Point(46, 263)
        Me.pgbEstadoProceso.Name = "pgbEstadoProceso"
        Me.pgbEstadoProceso.Size = New System.Drawing.Size(295, 22)
        Me.pgbEstadoProceso.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(43, 247)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Estado proceso"
        '
        'btnDetenerProceso
        '
        Me.btnDetenerProceso.Location = New System.Drawing.Point(46, 291)
        Me.btnDetenerProceso.Name = "btnDetenerProceso"
        Me.btnDetenerProceso.Size = New System.Drawing.Size(230, 25)
        Me.btnDetenerProceso.TabIndex = 11
        Me.btnDetenerProceso.Text = "Detener exportación de Libretas"
        Me.btnDetenerProceso.UseVisualStyleBackColor = True
        '
        'lblNumeroHoja
        '
        Me.lblNumeroHoja.AutoSize = True
        Me.lblNumeroHoja.Location = New System.Drawing.Point(282, 297)
        Me.lblNumeroHoja.Name = "lblNumeroHoja"
        Me.lblNumeroHoja.Size = New System.Drawing.Size(52, 13)
        Me.lblNumeroHoja.TabIndex = 12
        Me.lblNumeroHoja.Text = "Nro. Hoja"
        '
        'lblHoja
        '
        Me.lblHoja.AutoSize = True
        Me.lblHoja.Location = New System.Drawing.Point(340, 297)
        Me.lblHoja.Name = "lblHoja"
        Me.lblHoja.Size = New System.Drawing.Size(13, 13)
        Me.lblHoja.TabIndex = 13
        Me.lblHoja.Text = "0"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CodigoAlumno, Me.NombreCompleto, Me.Chk})
        Me.DataGridView1.Location = New System.Drawing.Point(444, 297)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(423, 192)
        Me.DataGridView1.TabIndex = 14
        '
        'CodigoAlumno
        '
        Me.CodigoAlumno.DataPropertyName = "CodigoAlumno"
        Me.CodigoAlumno.Frozen = True
        Me.CodigoAlumno.HeaderText = "Codigo"
        Me.CodigoAlumno.Name = "CodigoAlumno"
        Me.CodigoAlumno.Width = 70
        '
        'NombreCompleto
        '
        Me.NombreCompleto.DataPropertyName = "NombreCompleto"
        Me.NombreCompleto.Frozen = True
        Me.NombreCompleto.HeaderText = "NombreCompleto"
        Me.NombreCompleto.Name = "NombreCompleto"
        Me.NombreCompleto.Width = 250
        '
        'Chk
        '
        Me.Chk.DataPropertyName = "Chk"
        Me.Chk.FalseValue = "false"
        Me.Chk.Frozen = True
        Me.Chk.HeaderText = ""
        Me.Chk.Name = "Chk"
        Me.Chk.TrueValue = "true"
        Me.Chk.Width = 20
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(381, 306)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Alumnos"
        '
        'lblTotalChk
        '
        Me.lblTotalChk.AutoSize = True
        Me.lblTotalChk.Location = New System.Drawing.Point(808, 496)
        Me.lblTotalChk.Name = "lblTotalChk"
        Me.lblTotalChk.Size = New System.Drawing.Size(13, 13)
        Me.lblTotalChk.TabIndex = 36
        Me.lblTotalChk.Text = "0"
        Me.lblTotalChk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(702, 496)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Total Seleccionados:"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(809, 301)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 37
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbidiomaES)
        Me.GroupBox1.Controls.Add(Me.rbidiomaEN)
        Me.GroupBox1.Location = New System.Drawing.Point(46, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(295, 50)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Configuracion de Excel"
        '
        'rbidiomaES
        '
        Me.rbidiomaES.AutoSize = True
        Me.rbidiomaES.Location = New System.Drawing.Point(36, 19)
        Me.rbidiomaES.Name = "rbidiomaES"
        Me.rbidiomaES.Size = New System.Drawing.Size(63, 17)
        Me.rbidiomaES.TabIndex = 1
        Me.rbidiomaES.TabStop = True
        Me.rbidiomaES.Text = "Español"
        Me.rbidiomaES.UseVisualStyleBackColor = True
        '
        'rbidiomaEN
        '
        Me.rbidiomaEN.AutoSize = True
        Me.rbidiomaEN.Location = New System.Drawing.Point(105, 19)
        Me.rbidiomaEN.Name = "rbidiomaEN"
        Me.rbidiomaEN.Size = New System.Drawing.Size(53, 17)
        Me.rbidiomaEN.TabIndex = 0
        Me.rbidiomaEN.TabStop = True
        Me.rbidiomaEN.Text = "Ingles"
        Me.rbidiomaEN.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(381, 156)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Periodo"
        '
        'cmbPeriodo
        '
        Me.cmbPeriodo.FormattingEnabled = True
        Me.cmbPeriodo.Location = New System.Drawing.Point(444, 148)
        Me.cmbPeriodo.Name = "cmbPeriodo"
        Me.cmbPeriodo.Size = New System.Drawing.Size(121, 21)
        Me.cmbPeriodo.TabIndex = 40
        '
        'cmbTipoReporte
        '
        Me.cmbTipoReporte.FormattingEnabled = True
        Me.cmbTipoReporte.Location = New System.Drawing.Point(444, 97)
        Me.cmbTipoReporte.Name = "cmbTipoReporte"
        Me.cmbTipoReporte.Size = New System.Drawing.Size(155, 21)
        Me.cmbTipoReporte.TabIndex = 41
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(373, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Tipo reporte"
        '
        'txtFechaImpresion
        '
        Me.txtFechaImpresion.Location = New System.Drawing.Point(754, 93)
        Me.txtFechaImpresion.Name = "txtFechaImpresion"
        Me.txtFechaImpresion.Size = New System.Drawing.Size(113, 20)
        Me.txtFechaImpresion.TabIndex = 43
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(657, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 13)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Fecha Impresion:"
        '
        'frmReporteLibretaPrimaria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1061, 761)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtFechaImpresion)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbTipoReporte)
        Me.Controls.Add(Me.cmbPeriodo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.lblTotalChk)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.lblHoja)
        Me.Controls.Add(Me.lblNumeroHoja)
        Me.Controls.Add(Me.btnDetenerProceso)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pgbEstadoProceso)
        Me.Controls.Add(Me.lblMensajePros)
        Me.Controls.Add(Me.pcbLoading)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSalon)
        Me.Controls.Add(Me.cmbBimestre)
        Me.Controls.Add(Me.cmbSalon)
        Me.Controls.Add(Me.btnExportar)
        Me.Controls.Add(Me.lstReportes)
        Me.Controls.Add(Me.lstPresentacion)
        Me.Name = "frmReporteLibretaPrimaria"
        Me.Text = "Generación de Libretas"
        CType(Me.pcbLoading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstPresentacion As System.Windows.Forms.ListBox
    Friend WithEvents lstReportes As System.Windows.Forms.ListBox
    Friend WithEvents SaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnExportar As System.Windows.Forms.Button
    Friend WithEvents cmbSalon As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBimestre As System.Windows.Forms.ComboBox
    Friend WithEvents lblSalon As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmrControlador As System.Windows.Forms.Timer
    Friend WithEvents pcbLoading As System.Windows.Forms.PictureBox
    Friend WithEvents lblMensajePros As System.Windows.Forms.Label
    Friend WithEvents pgbEstadoProceso As System.Windows.Forms.ProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDetenerProceso As System.Windows.Forms.Button
    Friend WithEvents lblNumeroHoja As System.Windows.Forms.Label
    Friend WithEvents lblHoja As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTotalChk As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CodigoAlumno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NombreCompleto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Chk As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbidiomaES As System.Windows.Forms.RadioButton
    Friend WithEvents rbidiomaEN As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriodo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTipoReporte As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFechaImpresion As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
