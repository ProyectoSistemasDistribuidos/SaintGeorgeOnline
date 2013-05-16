<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImpresionTalonarios
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImpresionTalonarios))
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cboHojas = New System.Windows.Forms.ComboBox
        Me.cboImpresora = New System.Windows.Forms.ComboBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.tbRangoFinal = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.tbRangoInicial = New System.Windows.Forms.TextBox
        Me.rbEstadoImpresas = New System.Windows.Forms.RadioButton
        Me.rbEstadoPendientes = New System.Windows.Forms.RadioButton
        Me.gbEstado = New System.Windows.Forms.GroupBox
        Me.gbTipo = New System.Windows.Forms.GroupBox
        Me.cboTalonario = New System.Windows.Forms.ComboBox
        Me.gbFiltro = New System.Windows.Forms.GroupBox
        Me.chkFiltrarPago = New System.Windows.Forms.CheckBox
        Me.lblRangoInicial = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.gbImpresion = New System.Windows.Forms.GroupBox
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.gbFiltroFecha = New System.Windows.Forms.GroupBox
        Me.dtpFechaFin = New System.Windows.Forms.DateTimePicker
        Me.dtpFechaInicio = New System.Windows.Forms.DateTimePicker
        Me.rbTipoFechaPago = New System.Windows.Forms.RadioButton
        Me.rbTipoFechaEmision = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn
        Me.btnEmitir = New System.Windows.Forms.Button
        Me.btnImprimir = New System.Windows.Forms.Button
        Me.btnBuscar = New System.Windows.Forms.Button
        Me.Column1 = New System.Windows.Forms.DataGridViewImageColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewImageColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CodigoPago = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CodigoTalonario = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CodigoAlumno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Check = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbEstado.SuspendLayout()
        Me.gbTipo.SuspendLayout()
        Me.gbFiltro.SuspendLayout()
        Me.gbImpresion.SuspendLayout()
        Me.gbFiltroFecha.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 388)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1035, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.BalloonTipText = "Saint George Online's Listener"
        Me.NotifyIcon1.BalloonTipTitle = "Listener"
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Saint George Online's Listener"
        '
        'cboHojas
        '
        Me.cboHojas.FormattingEnabled = True
        Me.cboHojas.Location = New System.Drawing.Point(71, 52)
        Me.cboHojas.Name = "cboHojas"
        Me.cboHojas.Size = New System.Drawing.Size(226, 21)
        Me.cboHojas.TabIndex = 31
        '
        'cboImpresora
        '
        Me.cboImpresora.FormattingEnabled = True
        Me.cboImpresora.Location = New System.Drawing.Point(71, 22)
        Me.cboImpresora.Name = "cboImpresora"
        Me.cboImpresora.Size = New System.Drawing.Size(226, 21)
        Me.cboImpresora.TabIndex = 28
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.CodigoPago, Me.CodigoTalonario, Me.CodigoAlumno, Me.Check})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 151)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1005, 223)
        Me.DataGridView1.TabIndex = 26
        '
        'tbRangoFinal
        '
        Me.tbRangoFinal.Location = New System.Drawing.Point(190, 18)
        Me.tbRangoFinal.Name = "tbRangoFinal"
        Me.tbRangoFinal.Size = New System.Drawing.Size(70, 20)
        Me.tbRangoFinal.TabIndex = 25
        Me.tbRangoFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(142, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "N° Fin:"
        '
        'tbRangoInicial
        '
        Me.tbRangoInicial.Location = New System.Drawing.Point(66, 18)
        Me.tbRangoInicial.Name = "tbRangoInicial"
        Me.tbRangoInicial.Size = New System.Drawing.Size(70, 20)
        Me.tbRangoInicial.TabIndex = 23
        Me.tbRangoInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rbEstadoImpresas
        '
        Me.rbEstadoImpresas.AutoSize = True
        Me.rbEstadoImpresas.Location = New System.Drawing.Point(90, 19)
        Me.rbEstadoImpresas.Name = "rbEstadoImpresas"
        Me.rbEstadoImpresas.Size = New System.Drawing.Size(67, 17)
        Me.rbEstadoImpresas.TabIndex = 19
        Me.rbEstadoImpresas.TabStop = True
        Me.rbEstadoImpresas.Text = "Impresas"
        Me.rbEstadoImpresas.UseVisualStyleBackColor = True
        '
        'rbEstadoPendientes
        '
        Me.rbEstadoPendientes.AutoSize = True
        Me.rbEstadoPendientes.Checked = True
        Me.rbEstadoPendientes.Location = New System.Drawing.Point(6, 19)
        Me.rbEstadoPendientes.Name = "rbEstadoPendientes"
        Me.rbEstadoPendientes.Size = New System.Drawing.Size(78, 17)
        Me.rbEstadoPendientes.TabIndex = 18
        Me.rbEstadoPendientes.TabStop = True
        Me.rbEstadoPendientes.Text = "Pendientes"
        Me.rbEstadoPendientes.UseVisualStyleBackColor = True
        '
        'gbEstado
        '
        Me.gbEstado.Controls.Add(Me.rbEstadoPendientes)
        Me.gbEstado.Controls.Add(Me.rbEstadoImpresas)
        Me.gbEstado.Location = New System.Drawing.Point(12, 12)
        Me.gbEstado.Name = "gbEstado"
        Me.gbEstado.Size = New System.Drawing.Size(165, 50)
        Me.gbEstado.TabIndex = 37
        Me.gbEstado.TabStop = False
        Me.gbEstado.Text = "Estado de Pagos"
        '
        'gbTipo
        '
        Me.gbTipo.Controls.Add(Me.cboTalonario)
        Me.gbTipo.Location = New System.Drawing.Point(12, 72)
        Me.gbTipo.Name = "gbTipo"
        Me.gbTipo.Size = New System.Drawing.Size(441, 50)
        Me.gbTipo.TabIndex = 38
        Me.gbTipo.TabStop = False
        Me.gbTipo.Text = "Tipo de Comprobante"
        '
        'cboTalonario
        '
        Me.cboTalonario.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTalonario.FormattingEnabled = True
        Me.cboTalonario.Location = New System.Drawing.Point(6, 19)
        Me.cboTalonario.Name = "cboTalonario"
        Me.cboTalonario.Size = New System.Drawing.Size(425, 22)
        Me.cboTalonario.TabIndex = 37
        '
        'gbFiltro
        '
        Me.gbFiltro.Controls.Add(Me.chkFiltrarPago)
        Me.gbFiltro.Controls.Add(Me.lblRangoInicial)
        Me.gbFiltro.Controls.Add(Me.tbRangoInicial)
        Me.gbFiltro.Controls.Add(Me.Label1)
        Me.gbFiltro.Controls.Add(Me.tbRangoFinal)
        Me.gbFiltro.Location = New System.Drawing.Point(183, 12)
        Me.gbFiltro.Name = "gbFiltro"
        Me.gbFiltro.Size = New System.Drawing.Size(270, 50)
        Me.gbFiltro.TabIndex = 39
        Me.gbFiltro.TabStop = False
        '
        'chkFiltrarPago
        '
        Me.chkFiltrarPago.AutoSize = True
        Me.chkFiltrarPago.Location = New System.Drawing.Point(10, 0)
        Me.chkFiltrarPago.Name = "chkFiltrarPago"
        Me.chkFiltrarPago.Size = New System.Drawing.Size(94, 17)
        Me.chkFiltrarPago.TabIndex = 40
        Me.chkFiltrarPago.Text = "Filtrar N° Pago"
        Me.chkFiltrarPago.UseVisualStyleBackColor = True
        '
        'lblRangoInicial
        '
        Me.lblRangoInicial.AutoSize = True
        Me.lblRangoInicial.Location = New System.Drawing.Point(7, 21)
        Me.lblRangoInicial.Name = "lblRangoInicial"
        Me.lblRangoInicial.Size = New System.Drawing.Size(50, 13)
        Me.lblRangoInicial.TabIndex = 23
        Me.lblRangoInicial.Text = "N° Inicio:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Impresora :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Hoja :"
        '
        'gbImpresion
        '
        Me.gbImpresion.Controls.Add(Me.Label2)
        Me.gbImpresion.Controls.Add(Me.Label3)
        Me.gbImpresion.Controls.Add(Me.cboImpresora)
        Me.gbImpresion.Controls.Add(Me.cboHojas)
        Me.gbImpresion.Location = New System.Drawing.Point(720, 12)
        Me.gbImpresion.Name = "gbImpresion"
        Me.gbImpresion.Size = New System.Drawing.Size(303, 81)
        Me.gbImpresion.TabIndex = 42
        Me.gbImpresion.TabStop = False
        Me.gbImpresion.Text = "Configuracion de Impresión"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(93, 128)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(92, 17)
        Me.chkAll.TabIndex = 45
        Me.chkAll.Text = "Marcar Todos"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'gbFiltroFecha
        '
        Me.gbFiltroFecha.Controls.Add(Me.dtpFechaFin)
        Me.gbFiltroFecha.Controls.Add(Me.dtpFechaInicio)
        Me.gbFiltroFecha.Controls.Add(Me.rbTipoFechaPago)
        Me.gbFiltroFecha.Controls.Add(Me.rbTipoFechaEmision)
        Me.gbFiltroFecha.Controls.Add(Me.Label4)
        Me.gbFiltroFecha.Controls.Add(Me.Label5)
        Me.gbFiltroFecha.Location = New System.Drawing.Point(459, 12)
        Me.gbFiltroFecha.Name = "gbFiltroFecha"
        Me.gbFiltroFecha.Size = New System.Drawing.Size(173, 111)
        Me.gbFiltroFecha.TabIndex = 47
        Me.gbFiltroFecha.TabStop = False
        Me.gbFiltroFecha.Text = "Filtrar Por Fecha"
        '
        'dtpFechaFin
        '
        Me.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaFin.Location = New System.Drawing.Point(59, 79)
        Me.dtpFechaFin.Name = "dtpFechaFin"
        Me.dtpFechaFin.Size = New System.Drawing.Size(103, 20)
        Me.dtpFechaFin.TabIndex = 44
        '
        'dtpFechaInicio
        '
        Me.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaInicio.Location = New System.Drawing.Point(59, 54)
        Me.dtpFechaInicio.Name = "dtpFechaInicio"
        Me.dtpFechaInicio.Size = New System.Drawing.Size(103, 20)
        Me.dtpFechaInicio.TabIndex = 43
        '
        'rbTipoFechaPago
        '
        Me.rbTipoFechaPago.AutoSize = True
        Me.rbTipoFechaPago.Location = New System.Drawing.Point(77, 24)
        Me.rbTipoFechaPago.Name = "rbTipoFechaPago"
        Me.rbTipoFechaPago.Size = New System.Drawing.Size(50, 17)
        Me.rbTipoFechaPago.TabIndex = 42
        Me.rbTipoFechaPago.TabStop = True
        Me.rbTipoFechaPago.Text = "Pago"
        Me.rbTipoFechaPago.UseVisualStyleBackColor = True
        '
        'rbTipoFechaEmision
        '
        Me.rbTipoFechaEmision.AutoSize = True
        Me.rbTipoFechaEmision.Location = New System.Drawing.Point(10, 24)
        Me.rbTipoFechaEmision.Name = "rbTipoFechaEmision"
        Me.rbTipoFechaEmision.Size = New System.Drawing.Size(61, 17)
        Me.rbTipoFechaEmision.TabIndex = 41
        Me.rbTipoFechaEmision.TabStop = True
        Me.rbTipoFechaEmision.Text = "Emision"
        Me.rbTipoFechaEmision.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "F. Inicio:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "F. Fin:"
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.Frozen = True
        Me.DataGridViewImageColumn1.HeaderText = "VP"
        Me.DataGridViewImageColumn1.Image = Global.SaintGeorgeOnline_Listeners.My.Resources.Resources.opc_ver
        Me.DataGridViewImageColumn1.MinimumWidth = 30
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ToolTipText = "Vista Previa"
        Me.DataGridViewImageColumn1.Width = 30
        '
        'DataGridViewImageColumn2
        '
        Me.DataGridViewImageColumn2.Frozen = True
        Me.DataGridViewImageColumn2.HeaderText = "Imp"
        Me.DataGridViewImageColumn2.Image = Global.SaintGeorgeOnline_Listeners.My.Resources.Resources.opc_printer
        Me.DataGridViewImageColumn2.MinimumWidth = 30
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.ToolTipText = "Imprimir"
        Me.DataGridViewImageColumn2.Width = 30
        '
        'btnEmitir
        '
        Me.btnEmitir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnEmitir.Location = New System.Drawing.Point(638, 90)
        Me.btnEmitir.Name = "btnEmitir"
        Me.btnEmitir.Size = New System.Drawing.Size(76, 33)
        Me.btnEmitir.TabIndex = 46
        Me.btnEmitir.Text = "Emitir    "
        Me.btnEmitir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEmitir.UseVisualStyleBackColor = True
        '
        'btnImprimir
        '
        Me.btnImprimir.BackgroundImage = Global.SaintGeorgeOnline_Listeners.My.Resources.Resources.print_icon
        Me.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnImprimir.Location = New System.Drawing.Point(639, 51)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(76, 33)
        Me.btnImprimir.TabIndex = 43
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnBuscar
        '
        Me.btnBuscar.BackgroundImage = CType(resources.GetObject("btnBuscar.BackgroundImage"), System.Drawing.Image)
        Me.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnBuscar.Location = New System.Drawing.Point(639, 12)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(75, 33)
        Me.btnBuscar.TabIndex = 27
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'Column1
        '
        Me.Column1.Frozen = True
        Me.Column1.HeaderText = "VP"
        Me.Column1.Image = Global.SaintGeorgeOnline_Listeners.My.Resources.Resources.opc_ver
        Me.Column1.MinimumWidth = 30
        Me.Column1.Name = "Column1"
        Me.Column1.ToolTipText = "Vista Previa"
        Me.Column1.Width = 30
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "Check"
        Me.Column2.FalseValue = "False"
        Me.Column2.Frozen = True
        Me.Column2.HeaderText = ""
        Me.Column2.IndeterminateValue = "Null"
        Me.Column2.MinimumWidth = 30
        Me.Column2.Name = "Column2"
        Me.Column2.TrueValue = "True"
        Me.Column2.Width = 30
        '
        'Column3
        '
        Me.Column3.Frozen = True
        Me.Column3.HeaderText = "Imp"
        Me.Column3.Image = Global.SaintGeorgeOnline_Listeners.My.Resources.Resources.opc_printer
        Me.Column3.MinimumWidth = 30
        Me.Column3.Name = "Column3"
        Me.Column3.ToolTipText = "Imprimir"
        Me.Column3.Width = 30
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "AbreviaturaTalonario"
        DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle29
        Me.Column4.Frozen = True
        Me.Column4.HeaderText = "Tipo"
        Me.Column4.MinimumWidth = 40
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Width = 40
        '
        'Column5
        '
        Me.Column5.DataPropertyName = "NumeroPago"
        DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle30
        Me.Column5.Frozen = True
        Me.Column5.HeaderText = "N° Pago"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column5.Width = 120
        '
        'Column6
        '
        Me.Column6.DataPropertyName = "NombreCompleto"
        DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle31
        Me.Column6.Frozen = True
        Me.Column6.HeaderText = "Nombre Alumno"
        Me.Column6.MinimumWidth = 300
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column6.Width = 300
        '
        'Column7
        '
        Me.Column7.DataPropertyName = "SimboloMoneda"
        DataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column7.DefaultCellStyle = DataGridViewCellStyle32
        Me.Column7.Frozen = True
        Me.Column7.HeaderText = "Mon"
        Me.Column7.MinimumWidth = 40
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column7.Width = 40
        '
        'Column8
        '
        Me.Column8.DataPropertyName = "Total"
        DataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle33
        Me.Column8.Frozen = True
        Me.Column8.HeaderText = "Monto"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column8.Width = 110
        '
        'Column9
        '
        Me.Column9.DataPropertyName = "FechaEmision"
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column9.DefaultCellStyle = DataGridViewCellStyle34
        Me.Column9.Frozen = True
        Me.Column9.HeaderText = "F. Emisión"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column10
        '
        Me.Column10.DataPropertyName = "FechaPago"
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column10.DefaultCellStyle = DataGridViewCellStyle35
        Me.Column10.Frozen = True
        Me.Column10.HeaderText = "F. Pago"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CodigoPago
        '
        Me.CodigoPago.DataPropertyName = "CodigoPago"
        Me.CodigoPago.HeaderText = "CodigoPago"
        Me.CodigoPago.Name = "CodigoPago"
        Me.CodigoPago.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CodigoPago.Visible = False
        '
        'CodigoTalonario
        '
        Me.CodigoTalonario.DataPropertyName = "CodigoTalonario"
        Me.CodigoTalonario.HeaderText = "CodigoTalonario"
        Me.CodigoTalonario.Name = "CodigoTalonario"
        Me.CodigoTalonario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CodigoTalonario.Visible = False
        '
        'CodigoAlumno
        '
        Me.CodigoAlumno.DataPropertyName = "CodigoAlumno"
        Me.CodigoAlumno.HeaderText = "CodigoAlumno"
        Me.CodigoAlumno.Name = "CodigoAlumno"
        Me.CodigoAlumno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CodigoAlumno.Visible = False
        '
        'Check
        '
        Me.Check.DataPropertyName = "Check"
        Me.Check.Frozen = True
        Me.Check.HeaderText = "Check"
        Me.Check.Name = "Check"
        Me.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Check.Visible = False
        '
        'frmImpresionTalonarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 410)
        Me.Controls.Add(Me.gbFiltroFecha)
        Me.Controls.Add(Me.btnEmitir)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.gbImpresion)
        Me.Controls.Add(Me.gbFiltro)
        Me.Controls.Add(Me.gbTipo)
        Me.Controls.Add(Me.gbEstado)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmImpresionTalonarios"
        Me.Text = "Impresión de Talonarios"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbEstado.ResumeLayout(False)
        Me.gbEstado.PerformLayout()
        Me.gbTipo.ResumeLayout(False)
        Me.gbFiltro.ResumeLayout(False)
        Me.gbFiltro.PerformLayout()
        Me.gbImpresion.ResumeLayout(False)
        Me.gbImpresion.PerformLayout()
        Me.gbFiltroFecha.ResumeLayout(False)
        Me.gbFiltroFecha.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents cboHojas As System.Windows.Forms.ComboBox
    Friend WithEvents cboImpresora As System.Windows.Forms.ComboBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents tbRangoFinal As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbRangoInicial As System.Windows.Forms.TextBox
    Friend WithEvents rbEstadoImpresas As System.Windows.Forms.RadioButton
    Friend WithEvents rbEstadoPendientes As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn2 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents gbEstado As System.Windows.Forms.GroupBox
    Friend WithEvents gbTipo As System.Windows.Forms.GroupBox
    Friend WithEvents gbFiltro As System.Windows.Forms.GroupBox
    Friend WithEvents lblRangoInicial As System.Windows.Forms.Label
    Friend WithEvents chkFiltrarPago As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents gbImpresion As System.Windows.Forms.GroupBox
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnEmitir As System.Windows.Forms.Button
    Friend WithEvents gbFiltroFecha As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rbTipoFechaEmision As System.Windows.Forms.RadioButton
    Friend WithEvents rbTipoFechaPago As System.Windows.Forms.RadioButton
    Friend WithEvents dtpFechaInicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFechaFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboTalonario As System.Windows.Forms.ComboBox
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodigoPago As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodigoTalonario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodigoAlumno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Check As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
