Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities

Imports CrystalDecisions
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports System.Text
Imports System.Windows.Forms
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices.Marshal

Public Class frmImpresionTalonarios

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
    Private int_CodigoUsuario As Integer = 1
    Private int_CodigoTipoUsuario As Integer = 1

    Private int_CodigoTalonario As Integer
    Private int_TipoConsulta As Integer

    Private str_Error_ImpresorasInstaladas As String = "Detectar impresoras instaladas."
    Private str_Error_ImpresoraFinal As String = "Buscar la impresora matricial."
    Private str_Error_Hojas As String = "Detectar hojas de la impresora."
    Private str_Error_HojaFinal As String = "Buscar la hoja del documento a imprimir."


#Region "Evitar múltiples instancias"

    Private Shared Instancia As frmImpresionTalonarios = Nothing

    Public Shared Function Instance() As frmImpresionTalonarios
        If Instancia Is Nothing OrElse Instancia.IsDisposed = True Then
            Instancia = New frmImpresionTalonarios
        End If
        Instancia.BringToFront()
        Return Instancia
    End Function

#End Region

#Region "Eventos"

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim str_Error As String = ""

        Try

            Dim nomServidor As String = ""
            Dim DatosServer As String = ""
            Dim arrayDatos As String()

            nomServidor = System.Configuration.ConfigurationManager.AppSettings.Item("servidorActual").ToString.Trim
            DatosServer = System.Configuration.ConfigurationManager.AppSettings.Item(nomServidor).ToString.Trim
            arrayDatos = DatosServer.Split("*")
            StatusStrip1.Text = "Nombre Servidor: " & arrayDatos(0)

            gbImpresion.Enabled = False
            gbImpresion.Visible = True

            ' Posible error : "Detectar impresoras instaladas."
            str_Error = str_Error_ImpresorasInstaladas

            ' Selección de impresora
            cargarImpresoras()

            ' Posible error : "Buscar la impresora matricial."
            str_Error = str_Error_ImpresoraFinal

            Dim str_NombreImpresora As String = System.Configuration.ConfigurationManager.AppSettings("nombreImpresora").ToString.Trim

            For i = 0 To cboImpresora.Items.Count - 1

                If cboImpresora.Items(i).ToString.ToLower = str_NombreImpresora.ToLower Then
                    cboImpresora.SelectedIndex = i
                End If
            Next i

            ' Posible error : "Detectar hojas de la impresora."
            str_Error = str_Error_Hojas

            ' Selección de hoja
            cargarHojas()

            ' Posible error : "Buscar la hoja del documento a imprimir."
            str_Error = str_Error_HojaFinal

            Dim str_NombreHoja As String = System.Configuration.ConfigurationManager.AppSettings("hojaBoleta").ToString.Trim
            For i = 0 To cboHojas.Items.Count - 1
                If cboHojas.Items(i).ToString = str_NombreHoja Then
                    cboHojas.SelectedIndex = i
                End If
            Next i

            'rbPagosVarios.Checked = True
            'tbRangoInicial.Text = "0000001"
            'tbRangoFinal.Text = "0005000"

            tbRangoInicial.Enabled = chkFiltrarPago.Checked
            tbRangoFinal.Enabled = chkFiltrarPago.Checked

            'rbTipoFechaEmision.Enabled = chkFiltrarPorFecha.Checked
            'rbTipoFechaPago.Enabled = chkFiltrarPorFecha.Checked
            'dtpFechaInicio.Enabled = chkFiltrarPorFecha.Checked
            'dtpFechaFin.Enabled = chkFiltrarPorFecha.Checked

            rbTipoFechaPago.Checked = True
            dtpFechaInicio.Value = Today.AddDays(-7).ToShortDateString
            dtpFechaFin.Value = Today.ToShortDateString

            cargarComboTalonarios()

        Catch ex As Exception

            ' Bloqueo de los controles del formulario
            chkAll.Enabled = False
            gbEstado.Enabled = False
            gbTipo.Enabled = False
            btnBuscar.Enabled = False
            btnImprimir.Enabled = False
            MsgBox("Error : " & str_Error)

        End Try

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        VisualizarPagos()
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If DataGridView1.Rows.Count > 0 Then
            ImprimirPagos()
        Else
            MsgBox("No ha seleccionado ningún documento.")
            Exit Sub
        End If
    End Sub

    Private Sub btnEmitir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmitir.Click

        If DataGridView1.Rows.Count > 0 Then

            EmitirPagos()

        Else
            MsgBox("No ha seleccionado ningún documento.")
            Exit Sub
        End If
    End Sub

    Private Sub cboImpresora_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboImpresora.SelectedIndexChanged
        Dim str_Error As String = ""

        Try

            ' Posible error : "Detectar hojas de la impresora."
            str_Error = str_Error_Hojas

            cargarHojas()

        Catch ex As Exception
            MsgBox("Error : " & str_Error)
        End Try

    End Sub

    Private Sub chkFiltrarPago_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFiltrarPago.CheckedChanged

        tbRangoInicial.Enabled = chkFiltrarPago.Checked
        tbRangoFinal.Enabled = chkFiltrarPago.Checked

    End Sub

#Region "radio buttons"
    'Private Sub rbTipoBoleta_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim str_Error As String = ""

    '    Try

    '        TipoDocumentos()
    '        Dim int_CodigoTalonario As Integer = 1

    '        ' Posible error : "Buscar la hoja del documento a imprimir."
    '        str_Error = str_Error_HojaFinal

    '        seleccionarHojaImpresora(int_CodigoTalonario)

    '    Catch ex As Exception
    '        MsgBox("Error : " & str_Error)
    '    End Try

    'End Sub

    'Private Sub rbTipoFactura_CheckedChanged1(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim str_Error As String = ""

    '    Try

    '        TipoDocumentos()
    '        Dim int_CodigoTalonario As Integer = 2

    '        ' Posible error : "Buscar la hoja del documento a imprimir."
    '        str_Error = str_Error_HojaFinal

    '        seleccionarHojaImpresora(int_CodigoTalonario)

    '    Catch ex As Exception
    '        MsgBox("Error : " & str_Error)
    '    End Try

    'End Sub

    'Private Sub rbTipoNotaCredito_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim str_Error As String = ""

    '    Try

    '        TipoDocumentos()
    '        Dim int_CodigoTalonario As Integer = 3

    '        ' Posible error : "Buscar la hoja del documento a imprimir."
    '        str_Error = str_Error_HojaFinal

    '        seleccionarHojaImpresora(int_CodigoTalonario)

    '    Catch ex As Exception
    '        MsgBox("Error : " & str_Error)
    '    End Try

    'End Sub

    'Private Sub rbTipoNotaDebito_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim str_Error As String = ""

    '    Try

    '        TipoDocumentos()
    '        Dim int_CodigoTalonario As Integer = 6

    '        ' Posible error : "Buscar la hoja del documento a imprimir."
    '        str_Error = str_Error_HojaFinal

    '        seleccionarHojaImpresora(int_CodigoTalonario)

    '    Catch ex As Exception
    '        MsgBox("Error : " & str_Error)
    '    End Try

    'End Sub

    'Private Sub rbTipoTodas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'TodosDocumentos()
    'End Sub
#End Region

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged

        If DataGridView1.Rows.Count > 0 Then
            Dim dt As DataTable = DataGridView1.DataSource
            For Each dr As DataRow In dt.Rows
                dr.Item("Check") = Convert.ToInt32(chkAll.Checked)
            Next
            DataGridView1.DataSource = dt
        Else
            Exit Sub
        End If

    End Sub

    Private Sub cboTalonario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTalonario.SelectedIndexChanged
        Dim str_Error As String = ""
        Try
            If cboTalonario.SelectedValue.ToString = "System.Data.DataRowView" Then
                Exit Sub
            Else
                If cboTalonario.SelectedValue > 0 Then
                    TipoDocumentos()
                    Dim int_CodigoTalonario As Integer = cboTalonario.SelectedValue
                    ' Posible error : "Buscar la hoja del documento a imprimir."
                    str_Error = str_Error_HojaFinal
                    seleccionarHojaImpresora(int_CodigoTalonario)
                End If
            End If
        Catch ex As Exception
            MsgBox("Error : " & str_Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub seleccionarHojaImpresora(ByVal int_CodigoTalonario As Integer)

        Dim str_NombreHoja As String = ""

        Select Case int_CodigoTalonario
            Case 1 : str_NombreHoja = System.Configuration.ConfigurationManager.AppSettings("hojaBoleta").ToString.Trim ' boleta
            Case 2 : str_NombreHoja = System.Configuration.ConfigurationManager.AppSettings("hojaFactura").ToString.Trim ' factura
            Case 3 : str_NombreHoja = System.Configuration.ConfigurationManager.AppSettings("hojaFactura").ToString.Trim ' credito
            Case 6 : str_NombreHoja = System.Configuration.ConfigurationManager.AppSettings("hojaFactura").ToString.Trim ' debito
            Case 7 : str_NombreHoja = System.Configuration.ConfigurationManager.AppSettings("hojaBoleta").ToString.Trim ' boleta
            Case 10 : str_NombreHoja = System.Configuration.ConfigurationManager.AppSettings("hojaFactura").ToString.Trim ' factura
            Case 11 : str_NombreHoja = System.Configuration.ConfigurationManager.AppSettings("hojaFactura").ToString.Trim ' credito
            Case 9 : str_NombreHoja = System.Configuration.ConfigurationManager.AppSettings("hojaFactura").ToString.Trim ' debito
        End Select

        For i = 0 To cboHojas.Items.Count - 1
            If cboHojas.Items(i).ToString = str_NombreHoja Then
                cboHojas.SelectedIndex = i
            End If
        Next i

    End Sub

    Private Sub TipoDocumentos()

        btnBuscar.Enabled = True
        btnImprimir.Enabled = True

    End Sub

    Private Sub cargarComboTalonarios()

        Dim int_CodigoPagina As Integer = System.Configuration.ConfigurationManager.AppSettings.Item("CodigoPagina_ImpresionTalonarios").ToString.Trim
        Dim int_Estado As Integer = 1
        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1
        Dim obj_BL_Talonarios As New bl_Talonarios
        Dim ds_Lista As DataSet = obj_BL_Talonarios.FUN_LIS_TalonariosPorModulo(int_CodigoPagina, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Try
            cboTalonario.DataSource = ds_Lista.Tables(0)
            cboTalonario.DisplayMember = "DescripcionCompleta2"
            cboTalonario.ValueMember = "Codigo"
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub

    Private Sub cargarImpresoras()
        Dim i As Integer
        Dim pkInstalledPrinters As String

        For i = 0 To System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count - 1
            pkInstalledPrinters = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Item(i)
            cboImpresora.Items.Add(pkInstalledPrinters)
        Next
    End Sub

    Private Sub cargarHojas()
        cboHojas.Items.Clear()
        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        doctoprint.PrinterSettings.PrinterName = cboImpresora.SelectedItem.ToString

        For i As Integer = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            cboHojas.Items.Add(doctoprint.PrinterSettings.PaperSizes(i).PaperName)
        Next
    End Sub

    ' Obtiene una lista de pagos para visualizarla en el gridview
    Private Sub VisualizarPagos()

        Dim int_Talonario As Integer
        int_Talonario = cboTalonario.SelectedValue

        Dim int_EstadoEmision As Integer
        If rbEstadoPendientes.Checked Then : int_EstadoEmision = 0
        Else : int_EstadoEmision = 1 : End If

        Dim bool_FiltrarPago As Boolean = chkFiltrarPago.Checked
        Dim str_NumeroPagoInicial As String = ""
        Dim str_NumeroPagoFinal As String = ""

        If bool_FiltrarPago Then
            str_NumeroPagoInicial = tbRangoInicial.Text.Trim
            str_NumeroPagoFinal = tbRangoFinal.Text.Trim
        Else
            str_NumeroPagoInicial = ""
            str_NumeroPagoFinal = ""
        End If

        Dim int_TipoFecha As Integer
        If rbTipoFechaEmision.Checked Then
            int_TipoFecha = 1
        ElseIf rbTipoFechaPago.Checked Then
            int_TipoFecha = 2
        End If

        Dim dt_FechaIni As Date = dtpFechaInicio.Value.ToShortDateString
        Dim dt_FechaFin As Date = dtpFechaFin.Value.ToShortDateString

        Dim ds_Lista As DataSet
        Dim obj_BL_Pagos As New bl_Pagos

        ds_Lista = obj_BL_Pagos.FUN_LIS_PagosModuloImpresion( _
        int_EstadoEmision, int_Talonario, str_NumeroPagoInicial, str_NumeroPagoFinal, int_TipoFecha, dt_FechaIni, dt_FechaFin, _
        int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        DataGridView1.DataSource = ds_Lista.Tables(0)

    End Sub

    ' Imprime la lista de pagos seleccionados
    Private Sub ImprimirPagos()

        Dim int_Talonario As Integer
        int_Talonario = cboTalonario.SelectedValue

        Dim sb_CodigoPago As New StringBuilder
        Dim str_CodigoPago As String = ""

        For Each r As DataGridViewRow In DataGridView1.Rows
            If Convert.ToBoolean(r.DataGridView.Item("Check", r.Index).Value) = True Then
                sb_CodigoPago.Append(r.DataGridView.Item("CodigoPago", r.Index).Value.ToString & ",")
            End If
        Next

        str_CodigoPago = sb_CodigoPago.ToString()

        If str_CodigoPago.Trim.Length = 0 Then
            MsgBox("No ha seleccionado ningún documento.")
            Exit Sub
        End If

        str_CodigoPago = str_CodigoPago.Substring(0, str_CodigoPago.Length - 1)

        Dim ds_Lista As DataSet
        Dim obj_BL_Pagos As New bl_Pagos

        ds_Lista = obj_BL_Pagos.FUN_LIS_PagosModuloImpresionVarios(str_CodigoPago, int_Talonario, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim dt As DataTable
        dt = ds_Lista.Tables(0).Copy

        Dim monto As String = ""
        Dim soles As Double = 0
        Dim centimos As Double = 0
        Dim str_centimos As String = ""

        For Each dr As DataRow In dt.Rows
            monto = dr.Item("MontoTotalPago").ToString
            str_centimos = monto.Substring(monto.LastIndexOf(".") + 1, 2)
            soles = monto.Substring(0, monto.Length - str_centimos.Length - 1) 'CInt(dr.Item("MontoTotalPago"))
            dr.Item("MontoTotalTexto") = Datos.Num2Text(soles) & " Y " & str_centimos & "/100 " & " " & dr.Item("DescMoneda")
        Next

        Dim ds_Pagos As New DataSet
        ds_Pagos = SeparacionPagos(dt)

        ImprimirPagosMultiplesDetalles(int_Talonario, ds_Pagos)

    End Sub

    Private Sub ImprimirPagosIndividual(ByVal str_CodigoPago As String, ByVal int_Talonario As Integer)

        Dim ds_Lista As DataSet
        Dim obj_BL_Pagos As New bl_Pagos

        ds_Lista = obj_BL_Pagos.FUN_LIS_PagosModuloImpresionVarios(str_CodigoPago, int_Talonario, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim dt As DataTable
        dt = ds_Lista.Tables(0).Copy

        Dim monto As String = ""
        Dim soles As Double = 0
        Dim centimos As Double = 0
        Dim str_centimos As String = ""

        For Each dr As DataRow In dt.Rows
            monto = dr.Item("MontoTotalPago").ToString
            str_centimos = monto.Substring(monto.LastIndexOf(".") + 1, 2)
            soles = monto.Substring(0, monto.Length - str_centimos.Length - 1) 'CInt(dr.Item("MontoTotalPago"))
            dr.Item("MontoTotalTexto") = Datos.Num2Text(soles) & " Y " & str_centimos & "/100 " & " " & dr.Item("DescMoneda")
        Next

        Dim ds_Pagos As New DataSet
        ds_Pagos = SeparacionPagos(dt)

        ImprimirPagosMultiplesDetalles(int_Talonario, ds_Pagos)

    End Sub

    ' Impresion de Boletas, Facturas, Nota Débito y Nota de Crédito
    Private Sub ImprimirPagosMultiplesDetalles(ByVal int_CodigoTalonario As Integer, ByVal ds_Pagos As DataSet)

        ' Maximo 10 registros en el detalle
        Dim int_MaxRowFactura As Integer = 10
        Dim int_MaxRowNotaCredito As Integer = 3

        Dim int_CodigoPago As Integer
        Dim obj_BL_Pagos As New bl_Pagos
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        If (int_CodigoTalonario = 1 Or int_CodigoTalonario = 7 Or int_CodigoTalonario = 8) Then ' Boleta 

            ' Seteo de cada Pago en una hoja de reporte
            For Each dtRep As DataTable In ds_Pagos.Tables

                Dim rptB As New RptImpresionBoletaConDetalle
                rptB.SetDataSource(dtRep)
                rptB.GroupFooterSection1.Height = 240 * (int_MaxRowFactura - dtRep.Rows.Count) ' - (10 * 240) = 2400
                rptB.PrintOptions.PrinterName = cboImpresora.SelectedItem.ToString

                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = cboImpresora.SelectedItem.ToString

                Dim rawKind As Integer
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = cboHojas.SelectedItem.ToString Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        rptB.PrintOptions.PaperSize = rawKind
                        Exit For
                    End If
                Next
                rptB.PrintToPrinter(1, False, 0, 0)

                ' Actualización del estado de emisión
                int_CodigoPago = CInt(dtRep.Rows(0).Item("CodigoPago").ToString)
                usp_valor = obj_BL_Pagos.FUN_UPD_PagosEstadoEmision(int_CodigoPago, int_CodigoTalonario, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            Next

        ElseIf (int_CodigoTalonario = 2 Or int_CodigoTalonario = 10) Then ' Factura 

            ' Seteo de cada Pago en una hoja de reporte
            For Each dtRep As DataTable In ds_Pagos.Tables

                Dim rptF As New RptImpresionFacturaConDetalle
                rptF.SetDataSource(dtRep)
                rptF.GroupFooterSection1.Height = 240 * (int_MaxRowFactura - dtRep.Rows.Count) ' - (10 * 240) = 2400
                rptF.PrintOptions.PrinterName = cboImpresora.SelectedItem.ToString

                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = cboImpresora.SelectedItem.ToString

                Dim rawKind As Integer
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = cboHojas.SelectedItem.ToString Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        rptF.PrintOptions.PaperSize = rawKind
                        Exit For
                    End If
                Next
                rptF.PrintToPrinter(1, False, 0, 0)

                ' Actualización del estado de emisión
                int_CodigoPago = CInt(dtRep.Rows(0).Item("CodigoPago").ToString)
                usp_valor = obj_BL_Pagos.FUN_UPD_PagosEstadoEmision(int_CodigoPago, int_CodigoTalonario, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            Next

        ElseIf (int_CodigoTalonario = 3 Or int_CodigoTalonario = 11) Then ' Nota Crédito 

            ' Seteo de cada Pago en una hoja de reporte
            For Each dtRep As DataTable In ds_Pagos.Tables

                Dim rptNC As New RptImpresionNotaCreditoConDetalle
                rptNC.SetDataSource(dtRep)
                rptNC.GroupFooterSection1.Height = 240 * (int_MaxRowNotaCredito - dtRep.Rows.Count) ' - (3 * 240) = 720
                rptNC.PrintOptions.PrinterName = cboImpresora.SelectedItem.ToString

                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = cboImpresora.SelectedItem.ToString

                Dim rawKind As Integer
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = cboHojas.SelectedItem.ToString Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        rptNC.PrintOptions.PaperSize = rawKind
                        Exit For
                    End If
                Next
                rptNC.PrintToPrinter(1, False, 0, 0)

                ' Actualización del estado de emisión
                int_CodigoPago = CInt(dtRep.Rows(0).Item("CodigoNota").ToString)
                usp_valor = obj_BL_Pagos.FUN_UPD_PagosEstadoEmision(int_CodigoPago, int_CodigoTalonario, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            Next

        ElseIf (int_CodigoTalonario = 6 Or int_CodigoTalonario = 9) Then ' Nota Débito 

            ' Seteo de cada Pago en una hoja de reporte
            For Each dtRep As DataTable In ds_Pagos.Tables

                Dim rptND As New RptImpresionNotaDebitoConDetalle
                rptND.SetDataSource(dtRep)
                rptND.GroupFooterSection1.Height = 240 * (int_MaxRowFactura - dtRep.Rows.Count) ' - (10 * 240) = 2400
                rptND.PrintOptions.PrinterName = cboImpresora.SelectedItem.ToString

                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = cboImpresora.SelectedItem.ToString

                Dim rawKind As Integer
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = cboHojas.SelectedItem.ToString Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        rptND.PrintOptions.PaperSize = rawKind
                        Exit For
                    End If
                Next
                rptND.PrintToPrinter(1, False, 0, 0)

                ' Actualización del estado de emisión
                int_CodigoPago = CInt(dtRep.Rows(0).Item("CodigoNota").ToString)
                usp_valor = obj_BL_Pagos.FUN_UPD_PagosEstadoEmision(int_CodigoPago, int_CodigoTalonario, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            Next

        End If

    End Sub

    ' Validacion de Dt y Dr de Pagos
    Private Function SeparacionPagos(ByVal dt As DataTable) As DataSet

        ' Separación de los Pagos en DataTables
        Dim int_CodigoPago, int_Indice As Integer
        Dim dsRepAux As New DataSet

        For Each dr As DataRow In dt.Rows
            int_CodigoPago = dr.Item("CodigoPago")
            int_Indice = dr.Item("idx")
            Dim dtRepAux As DataTable

            If ExisteDTPago(dsRepAux, "Pago-" & int_CodigoPago.ToString) = False Then
                dtRepAux = dt.Clone
                dtRepAux.TableName = "Pago-" & int_CodigoPago.ToString
                dsRepAux.Tables.Add(dtRepAux)
            Else
                dtRepAux = dsRepAux.Tables("Pago-" & int_CodigoPago.ToString)
            End If

            For Each drAux As DataRow In dt.Rows
                If int_CodigoPago = drAux.Item("CodigoPago") Then
                    If ExisteRowPago(dtRepAux, int_CodigoPago, int_Indice) = False Then
                        dtRepAux.ImportRow(dr)
                        'Exit For
                        Continue For
                    End If
                End If
            Next
        Next

        Return dsRepAux

    End Function

    Private Function ExisteDTPago(ByVal ds As DataSet, ByVal dtName As String) As Boolean

        Dim bool_Existe As Boolean = False

        If ds.Tables.Count > 0 Then
            For Each dt As DataTable In ds.Tables
                If dt.TableName = dtName Then
                    bool_Existe = True
                    Exit For
                End If
            Next
        Else
            bool_Existe = False
        End If

        Return bool_Existe

    End Function

    Private Function ExisteRowPago(ByVal dt As DataTable, ByVal int_CodigoPago As Integer, ByVal int_idx As Integer) As Boolean

        Dim bool_Existe As Boolean = False

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If dr.Item("CodigoPago") = int_CodigoPago And dr.Item("idx") = int_idx Then
                    bool_Existe = True
                    Exit For
                End If
            Next
        Else
            bool_Existe = False
        End If

        Return bool_Existe

    End Function

    'Emitir Pagos
    Private Sub EmitirPagos()

        Dim int_Talonario As Integer
        int_Talonario = cboTalonario.SelectedValue

        'If rbTipoBoleta.Checked Then : int_Talonario = 1
        'ElseIf rbTipoFactura.Checked Then : int_Talonario = 2
        'ElseIf rbTipoNotaCredito.Checked Then : int_Talonario = 3
        'ElseIf rbTipoNotaDebito.Checked Then : int_Talonario = 6 : End If

        Dim sb_CodigoPago As New StringBuilder
        Dim str_CodigoPago As String = ""

        For Each r As DataGridViewRow In DataGridView1.Rows
            If Convert.ToBoolean(r.DataGridView.Item("Check", r.Index).Value) = True Then
                sb_CodigoPago.Append(r.DataGridView.Item("CodigoPago", r.Index).Value.ToString & ",")
            End If
        Next

        str_CodigoPago = sb_CodigoPago.ToString()

        If str_CodigoPago.Trim.Length = 0 Then
            MsgBox("No ha seleccionado ningún documento.")
            Exit Sub
        End If

        str_CodigoPago = str_CodigoPago.Substring(0, str_CodigoPago.Length - 1)


        Dim obj_BL_Pagos As New bl_Pagos

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        usp_valor = obj_BL_Pagos.FUN_UPD_PagosEstadoEmision(str_CodigoPago, int_Talonario, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            VisualizarPagos()
        End If

        MsgBox(usp_mensaje)

    End Sub

#End Region

#Region "Eventos DataGridView"

    Private Sub DataGridView1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            Dim TipoAccion As Integer = 0

            If DataGridView1.CurrentCell Is Nothing Then
                Exit Sub
            End If

            If e.ColumnIndex = 0 Or e.ColumnIndex = 1 Or e.ColumnIndex = 2 Then
                If e.RowIndex < 0 Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            If e.ColumnIndex = 0 Then     ' Vista Previa
                TipoAccion = 1
            ElseIf e.ColumnIndex = 1 Then ' Seleccionar
                TipoAccion = 2
            ElseIf e.ColumnIndex = 2 Then ' Imprimir
                TipoAccion = 3
            End If

            Dim int_CodigoDocumento As Integer = DataGridView1.Item("CodigoPago", DataGridView1.CurrentRow.Index).Value
            Dim int_CodigoTalonario As Integer = DataGridView1.Item("CodigoTalonario", DataGridView1.CurrentRow.Index).Value

            If TipoAccion = 1 Then

                Dim objFrm As New frmVistaPrevia
                objFrm.codigoDocumento = int_CodigoDocumento
                objFrm.codigoTalonario = int_CodigoTalonario
                objFrm.ShowDialog()

            ElseIf TipoAccion = 2 Then

            ElseIf TipoAccion = 3 Then

                'seleccionarHojaImpresora(int_CodigoTalonario)
                ImprimirPagosIndividual(int_CodigoDocumento.ToString, int_CodigoTalonario)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos DataGridView"

#End Region

End Class