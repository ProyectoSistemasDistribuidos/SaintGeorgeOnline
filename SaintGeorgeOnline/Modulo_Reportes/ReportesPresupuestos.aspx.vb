Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices.Marshal
Imports Microsoft.Office.Interop.Excel
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessLogic
Imports Data = System.Data.SqlClient
Imports comon = System.Data
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports ClosedXML.Excel


Partial Class Modulo_Reportes_ReportesPresupuestos
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"
    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Reportes de Presupuestos")

            btnReporteExportar.Attributes.Add("onclick", "ShowMyModalPopup()")

            If Not Page.IsPostBack Then
                cargarAniopresupuestal()
                cargarListaReportes()
                cargarListaPresentacion()
                pnlReporte1.Visible = True



                F_cargarCombo(ddwAnioPresupuestal.SelectedValue)

                mostrarPanelParametros()
            End If

        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub lstReportes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstReportes.SelectedIndexChanged
        Try
            cargarListaPresentacion()
            mostrarPanelParametros()
        Catch ex As Exception
            ' EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub lstPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPresentacion.SelectedIndexChanged

        mostrarPanelParametros()

    End Sub

    Protected Sub btnReporteExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim usp_mensaje As String = ""
            Exportar()
            'If validarCombos() Then
            '    Exportar()
            'Else
            '    MostrarAlertas(usp_mensaje)
            'End If




        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub


#End Region

#Region "Metodos"

    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    Private Sub MostrarAlertas(ByVal str_alertas As String)
        MostrarSexyAlertBox(str_alertas, "Alert")
    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    Sub mostrarSoloEstePanel(ByVal str_nombrePanel As String)
        Try
            For Each ctr As Control In Form.Controls
                If TypeOf (ctr) Is Panel Then
                    If ctr.ID <> str_nombrePanel Then
                        ctr.Visible = False
                    End If
                End If
            Next
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Sub ExportarExcel()

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim dt_Lista As New System.Data.DataTable
        Dim ds_Lista As New DataSet
        Dim obj_bl_ModuloReporte As New bl_RelacionAlumnos


        ''lstPresentacion
        Dim int_TipoReporte As Integer = lstReportes.SelectedValue 'Tipo reporte
        Dim int_PresentacionReporte As Integer = lstPresentacion.SelectedValue 'Tipo reporte

        Dim str_TituloReporte As String = "" 'Titulo reporte
        Dim int_CodigoGrado As Integer
        Dim int_CodigoAula As Integer

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim rutaArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()

    End Sub

    Private Sub cargarListaReportes()

        Dim int_CodigoTipoReporte As Integer = 8 ' Reportes de Presupuestos
        Dim obj_BL_Reportes As New bl_Reportes
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Reportes.FUN_LIS_Reportes(int_CodigoTipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaReportes") = ds_Lista

        lstReportes.DataSource = ds_Lista.Tables(0)
        lstReportes.DataTextField = "Nombre"
        lstReportes.DataValueField = "Codigo"
        lstReportes.DataBind()

        lstReportes.SelectedIndex = 0

    End Sub

    Private Sub cargarListaPresentacion()

        Dim dt As System.Data.DataTable = CType(ViewState("ListaReportes"), DataSet).Tables(1)
        Dim int_CodigoReporte As Integer = lstReportes.SelectedValue

        Dim dv As DataView = dt.DefaultView

        With dv
            .RowFilter = "1=1 and CodigoReporte = " & int_CodigoReporte
        End With

        lstPresentacion.DataSource = dv
        lstPresentacion.DataTextField = "Descripcion"
        lstPresentacion.DataValueField = "CodigoDetalle"
        lstPresentacion.DataBind()

        lstPresentacion.SelectedIndex = 0

    End Sub

    Private Sub mostrarPanelParametros()

        pnlReporte1.Visible = True
        'pnlReporte2.Visible = False
        'pnlReporte3.Visible = False

        estadoVisibilidadReportes(False)
        If lstPresentacion.SelectedValue = 99 Then
            estadoVisibilidadReportes(True)
        End If
    End Sub

#End Region

#Region "Exportar cubos reprote presupuesto"
    ''
    ''' <summary>
    ''' Exporta los datos del gridView en formato HTML
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     07/12/2011
    ''' Modificado por:        __________
    ''' Fecha de modificación: 07/12/2011
    ''' </remarks>
    Private Sub Exportar()
        If lstPresentacion.SelectedValue = 99 Then
            F_ReporteGastos(cmbPresupuesto.SelectedValue)
            Exit Sub
        End If


        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""

        Dim obj_BL_SolicitudDePresupuesto As New bl_SolicitudDePresupuesto

        Dim str_TituloReporte As String = ""
        Dim int_CodigoPeriodo As Integer = ddwAnioPresupuestal.SelectedValue
        Dim int_CodigoTipoUsuario As Integer = 2
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado


        ''listar el detalle de los gastos 
        ''--------------------------------------------------------
        Dim oDA_reporteGastos As New DA_reporteGastos
        Dim dtGastos As New System.Data.DataTable
        ''--------------------------------------------------------
        Dim ds_Lista As DataSet = obj_BL_SolicitudDePresupuesto.FUN_REP_SolicitudPresupuestosGerencia(int_CodigoPeriodo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)




        ''


        dtGastos = oDA_reporteGastos.F_ListarGastosDetalleCubo(0, int_CodigoPeriodo)

        Dim dt As comon.DataTable = New comon.DataTable("ListaExportar")



        dt = Datos.agregarColumna(dt, "Periodo", "integer")
        dt = Datos.agregarColumna(dt, "Sede", "string")
        dt = Datos.agregarColumna(dt, "CentroCosto", "string")
        dt = Datos.agregarColumna(dt, "SubCentroCosto", "string")
        dt = Datos.agregarColumna(dt, "SubSubCentroCosto", "string")
        dt = Datos.agregarColumna(dt, "Presupuesto", "string")
        dt = Datos.agregarColumna(dt, "Situacion", "string")
        dt = Datos.agregarColumna(dt, "Estado", "string")
        dt = Datos.agregarColumna(dt, "Clase", "string")
        dt = Datos.agregarColumna(dt, "Actividades", "string")
        dt = Datos.agregarColumna(dt, "Categoria", "string")
        dt = Datos.agregarColumna(dt, "SubCategoria", "string")
        dt = Datos.agregarColumna(dt, "Artículo", "string")
        dt = Datos.agregarColumna(dt, "Moneda", "string")
        dt = Datos.agregarColumna(dt, "Precio", "decimal")
        dt = Datos.agregarColumna(dt, "Cantidad", "decimal")
        dt = Datos.agregarColumna(dt, "Total", "decimal")
        dt = Datos.agregarColumna(dt, "Observación", "string")

        dt = Datos.agregarColumna(dt, "Gastado", "decimal")
        dt = Datos.agregarColumna(dt, "Saldo", "decimal")






        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("Periodo") = dr.Item("Periodo")
            auxDR.Item("Sede") = dr.Item("Sede")
            auxDR.Item("CentroCosto") = dr.Item("CentroCosto")
            auxDR.Item("SubCentroCosto") = dr.Item("SubCentroCosto")
            auxDR.Item("SubSubCentroCosto") = dr.Item("SubSubCentroCosto")
            auxDR.Item("Presupuesto") = dr.Item("Presupuesto")
            auxDR.Item("Estado") = dr.Item("Estado")
            auxDR.Item("Clase") = dr.Item("Clase")
            auxDR.Item("Actividades") = dr.Item("Actividades")
            auxDR.Item("Categoria") = dr.Item("Categoria")
            auxDR.Item("SubCategoria") = dr.Item("SubCategoria")
            auxDR.Item("Artículo") = dr.Item("Item")
            auxDR.Item("Moneda") = dr.Item("MonedaFinal")
            auxDR.Item("Precio") = dr.Item("PrecioFinal")
            auxDR.Item("Cantidad") = dr.Item("Cantidad")
            auxDR.Item("Total") = dr.Item("Total")
            auxDR.Item("Observación") = dr.Item("Observacion")
            auxDR.Item("Situacion") = dr.Item("Situacion")

            Dim montoGastadoL = (From gt In dtGastos.AsEnumerable() Where gt("codPres") = dr("cSPre") And gt("CodigoItem") = dr("cAr") _
                               Select New With {.gasto = CDec(gt("nPreMontoGastar"))})

            Dim montoGastadoAc = 0.0
            If montoGastadoL.Count > 0 Then
                montoGastadoAc = montoGastadoL.Aggregate(Function(prv, curr) New With {.gasto = prv.gasto + curr.gasto}).gasto
            End If


            Dim saldo As Decimal = 0.0


            saldo = CDec(dr.Item("Total")) - montoGastadoAc

            auxDR.Item("Gastado") = Format(montoGastadoAc, "##,##0.00")
            auxDR.Item("Saldo") = Format(saldo, "##,##0.00")

            'cSPre  codPres
            'cAr    CodigoItem
            '
            ' nPreMontoGastar()
            '285.86
            ' CodigoItem()
            '186:
            ' codPres()


            dt.Rows.Add(auxDR)
        Next

        str_TituloReporte = "Reporte de Presupuestos"
        'NombreArchivo = ExportarReporteDinamicoPresupuestosGerencia(dt, str_TituloReporte)

        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

        If lstPresentacion.SelectedValue = 82 Then

            NombreArchivo = ExportarReporteDinamicoPresupuestoOperativo(dt, str_TituloReporte)
        ElseIf lstPresentacion.SelectedValue = 84 Then
            NombreArchivo = ExportarReporteDinamicoPresupuestoCuboSubcategioriaArticulo(dt, str_TituloReporte)
        End If





        ''NombreArchivo = ExportarReporteDinamicoPresupuestosGerencia(dt, str_TituloReporte)

        'NombreArchivo = NombreArchivo & ".xls"
        'rutamadre = Server.MapPath(".")
        'rutamadre = Replace(rutamadre, "\Modulo_Presupuestos", "") + "\Reportes\"

        downloadBytes = File.ReadAllBytes(NombreArchivo)

        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + rutaTemp & ".xls" + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

    End Sub
    ''
#End Region
#Region "Procedimiento de exportacion de cubos "
    ''

    ''exportar Reporte preupueto Operativo
    ''' <summary>
    ''' Reporte de 
    ''' </summary>
    ''' <param name="dtReporte"></param>
    ''' <param name="str_NombreEntidadReporte"></param>
    ''' <returns>nombre del archivo </returns>
    ''' <remarks></remarks>
    Public Shared Function ExportarReporteDinamicoPresupuestoOperativo(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        '' fecha de modificacion 22/10/2012
        Try
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim oCells As Microsoft.Office.Interop.Excel.Range
            Dim sFile As String, sTemplate As String
            Dim nombreRep As String
            Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
            Dim fila As String = ""
            nombreRep = GetNewName()
            Dim oPivotField As Microsoft.Office.Interop.Excel.PivotField


            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteDinamico_1")


            'sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
            'sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteDinamico_1").ToString()


            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xls"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            oExcel.Visible = False : oExcel.DisplayAlerts = False
            ''Start a new workbook 
            oBooks = oExcel.Workbooks
            oBooks.Open(rutaREpositorioTemporales) 'Load colorful template with graph

            oBook = oBooks.Item(1)

            oSheets = oBook.Worksheets

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = str_NombreEntidadReporte
            oCells = oSheet.Cells

            fila = LlenarPlantillaReporteDinamicoPresupuestosGerencia(dtReporte, oCells, oExcel, str_NombreEntidadReporte)



            ''----
            oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Reporte presupuesto"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de Solicitud de Presupuestos"
            End With

            With oExcel.Range(oCells(1, 2), oCells(1, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                ' .Value = "Reporte Presupuesto Operativo"
            End With


            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            End With

            Dim int_cont As Integer = 0
            Dim str_DescTipo As String = ""

            ''
            objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
            oSheet.Activate()

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            objTablaDinamica.PivotCache.SourceData = str_NombreEntidadReporte & "!F5C2:F" & fila & "C21"


            objTablaDinamica.PivotCache.Refresh()



            ''---------------------------------------------------------------------------------------

            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            With oSheet.PivotTables("Tabla dinámica1").PivotFields("Artículo")
                .Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField
            End With



            With oSheet.PivotTables("Tabla dinámica1")
                .PivotCache.Refresh()
                .ManualUpdate = False
                .RefreshTable()
            End With
            ''





            oSheet = CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "ReporteOperativo"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de Solicitud de Presupuestos"
            End With

            With oExcel.Range(oCells(1, 2), oCells(1, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte Presupuesto Operativo"
            End With


            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            End With

            int_cont = 0
            str_DescTipo = ""

            ''
            objTablaDinamica = oSheet.PivotTables("tbl1")
            oSheet.Activate()

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()
            'Hoja1!$B$5:$Q$1809


            objTablaDinamica.PivotCache.SourceData = str_NombreEntidadReporte & "!F5C2:F" & fila & "C21"


            objTablaDinamica.PivotCache.Refresh()

            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()



            'Artículo
            With oSheet.PivotTables("tbl1").PivotFields("Artículo")
                .Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField
            End With

            oPivotField = oSheet.PivotTables("tbl1").PivotFields("Artículo")
            For Each oPivotItem As Microsoft.Office.Interop.Excel.PivotItem In oPivotField.PivotItems

                If oPivotItem.Value = "Intereses hipotecarios" Or oPivotItem.Value = "Obras Constructivas 2013" Then '' Or oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" Then

                    With oPivotItem

                        If oPivotItem.RecordCount > 0 Then
                            .Visible = False
                        End If



                    End With

                End If

            Next
            CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl1").PivotCache.Refresh()
            CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl1").ManualUpdate = False
            CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl1").RefreshTable()
            oExcel.ActiveWindow.Zoom = 75
            ''
            ''-------------------''
            oSheet = CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "ReporteNoOperativo"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte Presupuesto No operativo"
            End With

            With oExcel.Range(oCells(1, 2), oCells(1, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de solicitud de presupuesto"
            End With


            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            End With

            int_cont = 0
            str_DescTipo = ""

            ''
            objTablaDinamica = oSheet.PivotTables("tbl4")
            oSheet.Activate()

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()
            'Hoja1!$B$5:$Q$1809

            objTablaDinamica.PivotCache.SourceData = str_NombreEntidadReporte & "!F5C2:F" & fila & "C21"
            objTablaDinamica.PivotCache.Refresh()
            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            oSheet.PivotTables("tbl4").PivotFields("Artículo").DrillTo("Artículo")
            'Artículo
            With oSheet.PivotTables("tbl4").PivotFields("Artículo")
                .Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField
            End With




            oPivotField = oSheet.PivotTables("tbl4").PivotFields("Artículo")


            For Each oPivotItem As Microsoft.Office.Interop.Excel.PivotItem In oPivotField.PivotItems

                If oPivotItem.Value = "Intereses hipotecarios" Or oPivotItem.Value = "Obras Constructivas 2013" Then 'Or oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" Then
                    If oPivotItem.RecordCount > 0 Then
                        With oPivotItem
                            .Visible = True
                        End With
                    End If


                Else
                    If oPivotItem.RecordCount > 0 Then
                        With oPivotItem
                            .Visible = False
                        End With
                    End If

                End If

            Next
            CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl4").PivotCache.Refresh()
            CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl4").ManualUpdate = False
            CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl4").RefreshTable()
            oExcel.ActiveWindow.Zoom = 75

            ''-------------------''
            ''
            ''-------------------''
            ''-------------------''

            ''==================



            ''-------------------''
            oSheet = CType(oSheets.Item(5), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Reporte x CC y (G e I) Budget"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte x CC y (G e I) Budget"
            End With

            With oExcel.Range(oCells(1, 2), oCells(1, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de solicitud de presupuesto"
            End With


            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            End With




            '' ''---------------------------------------------------------------------------------
            '' ''---------------------------------------------------------------------------------------------------------

            'oExcel.ScreenUpdating = True
            ' ''--------------------------------------
            ''Hoja1!$B$5:$Q$1809
            ' ''----------------
            'oBook.Save()
            'oBook.Close()
            ''Quit Excel and thoroughly deallocate everything
            'oExcel.Quit()
            'ReleaseComObject(oCells)
            'ReleaseComObject(oSheet)
            'ReleaseComObject(oSheets)
            'ReleaseComObject(oBook)
            'ReleaseComObject(oBooks)
            'ReleaseComObject(oExcel)
            'oExcel = Nothing
            'oBooks = Nothing
            'oBook = Nothing
            'oSheets = Nothing
            'oSheet = Nothing
            'oCells = Nothing
            'System.GC.Collect()
            'Return rutaREpositorioTemporales

            '' ''---------------------------------------------------------------------------------------------------------
            ' ''---------------------------------------------------------------------------------




            int_cont = 0
            str_DescTipo = ""

            ''
            objTablaDinamica = oSheet.PivotTables("tblNuevo")
            oSheet.Activate()




            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()


            objTablaDinamica.PivotCache.SourceData = str_NombreEntidadReporte & "!F5C2:F" & fila & "C19"
            objTablaDinamica.PivotCache.Refresh()
            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(5), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()
            oPivotField = Nothing
            oPivotField = oSheet.PivotTables("tblNuevo").PivotFields("Artículo")

            ''''''''''''''''''''''''''''

            For Each oPivotItem As Microsoft.Office.Interop.Excel.PivotItem In oPivotField.PivotItems
                If oPivotItem.Value = "Intereses hipotecarios" Or oPivotItem.Value = "Obras Constructivas 2013" Then 'Or oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" 'Then

                    With oPivotItem
                        If oPivotItem.RecordCount > 0 Then
                            .Visible = False
                        End If




                    End With


                ElseIf oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" Then
                    With oPivotItem
                        If oPivotItem.RecordCount > 0 Then
                            If .Visible = False Then
                                .Visible = True
                            End If
                        End If

                    End With
                End If
            Next
            ' Next

            CType(oSheets.Item(5), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tblNuevo").PivotCache.Refresh()
            CType(oSheets.Item(5), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tblNuevo").ManualUpdate = False
            CType(oSheets.Item(5), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tblNuevo").RefreshTable()

            ''/////////////////////////////////////////////

            'oExcel.ScreenUpdating = True
            ' ''--------------------------------------
            ''Hoja1!$B$5:$Q$1809
            ' ''----------------
            'oBook.Save()
            'oBook.Close()
            ''Quit Excel and thoroughly deallocate everything
            'oExcel.Quit()
            'ReleaseComObject(oCells)
            'ReleaseComObject(oSheet)
            'ReleaseComObject(oSheets)
            'ReleaseComObject(oBook)
            'ReleaseComObject(oBooks)
            'ReleaseComObject(oExcel)
            'oExcel = Nothing
            'oBooks = Nothing
            'oBook = Nothing
            'oSheets = Nothing
            'oSheet = Nothing
            'oCells = Nothing
            'System.GC.Collect()
            'Return rutaREpositorioTemporales

            ''==================
            ''-------------------''
            oSheet = CType(oSheets.Item(6), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Reporte x CC y Ppto (G e I)"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte x CC y Ppto (G e I)"
            End With

            With oExcel.Range(oCells(1, 2), oCells(1, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de solicitud de presupuesto"
            End With


            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            End With

            int_cont = 0
            str_DescTipo = ""

            ''
            objTablaDinamica = oSheet.PivotTables("tblNuevaII")
            oSheet.Activate()

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()


            objTablaDinamica.PivotCache.SourceData = str_NombreEntidadReporte & "!F5C2:F" & fila & "C19"
            objTablaDinamica.PivotCache.Refresh()
            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(6), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            oPivotField = Nothing

            oPivotField = oSheet.PivotTables("tblNuevaII").PivotFields("Artículo")


            For Each oPivotItem As Microsoft.Office.Interop.Excel.PivotItem In oPivotField.PivotItems

                If oPivotItem.Value = "Intereses hipotecarios" Or oPivotItem.Value = "Obras Constructivas 2013" Then 'Or oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" 'Then


                    With oPivotItem

                        If oPivotItem.RecordCount > 0 Then
                            .Visible = False

                        End If
                    End With

                ElseIf oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" Then
                    With oPivotItem
                        If oPivotItem.RecordCount > 0 Then
                            If .Visible = False Then
                                .Visible = True
                            End If
                        End If

                    End With
                End If

            Next

            CType(oSheets.Item(6), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tblNuevaII").PivotCache.Refresh()
            CType(oSheets.Item(6), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tblNuevaII").ManualUpdate = False
            CType(oSheets.Item(6), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tblNuevaII").RefreshTable()
            ''==================





            oExcel.ScreenUpdating = True



            oBook.Save()
            oBook.Close()

            'Quit Excel and thoroughly deallocate everything
            oExcel.Quit()
            ReleaseComObject(oCells)
            ReleaseComObject(oSheet)
            ReleaseComObject(oSheets)
            ReleaseComObject(oBook)
            ReleaseComObject(oBooks)
            ReleaseComObject(oExcel)
            oExcel = Nothing
            oBooks = Nothing
            oBook = Nothing
            oSheets = Nothing
            oSheet = Nothing
            oCells = Nothing
            System.GC.Collect()





            ''---------------------------------------------------------------------------------------------------------
            'oExcel.ScreenUpdating = True
            ' ''--------------------------------------
            ''Hoja1!$B$5:$Q$1809
            ' ''----------------

            'oBook.Save()
            'oBook.Close()

            ''Quit Excel and thoroughly deallocate everything
            'oExcel.Quit()
            'ReleaseComObject(oCells)
            'ReleaseComObject(oSheet)
            'ReleaseComObject(oSheets)
            'ReleaseComObject(oBook)
            'ReleaseComObject(oBooks)
            'ReleaseComObject(oExcel)
            'oExcel = Nothing
            'oBooks = Nothing
            'oBook = Nothing
            'oSheets = Nothing
            'oSheet = Nothing
            'oCells = Nothing
            'System.GC.Collect()


            'Return rutaREpositorioTemporales
            'Exit Function
            ''---------------------------------------------------------------------------------------------------------









            Return rutaREpositorioTemporales

        Catch ex As Exception

        End Try
    End Function
    ''



#End Region


#Region "Exportacion de reporte: Centro de costos,Direccion,Administracion,MArketing,Servicios generales  "
    Public Shared Function ExportarReporteDinamicoPresupuestoCuboSubcategioriaArticulo(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String
        Try
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim oCells As Microsoft.Office.Interop.Excel.Range
            Dim sFile As String, sTemplate As String
            Dim nombreRep As String
            Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
            Dim fila As String = ""
            nombreRep = GetNewName()
            Dim oPivotField As Microsoft.Office.Interop.Excel.PivotField


            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteDinamico_2copiaII")


            'sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
            'sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteDinamico_1").ToString()


            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xls"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)


            oExcel.Visible = False : oExcel.DisplayAlerts = False
            ''Start a new workbook 
            oBooks = oExcel.Workbooks
            oBooks.Open(rutaREpositorioTemporales) 'Load colorful template with graph

            oBook = oBooks.Item(1)

            oSheets = oBook.Worksheets

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = str_NombreEntidadReporte
            oCells = oSheet.Cells

            fila = LlenarPlantillaReporteDinamicoPresupuestosGerencia(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

            ''-----------------------------------------------------------------------------------------

            'oBook.Save()
            'oBook.Close()

            ''Quit Excel and thoroughly deallocate everything
            'oExcel.Quit()
            'ReleaseComObject(oCells)
            'ReleaseComObject(oSheet)
            'ReleaseComObject(oSheets)
            'ReleaseComObject(oBook)
            'ReleaseComObject(oBooks)
            'ReleaseComObject(oExcel)
            'oExcel = Nothing
            'oBooks = Nothing
            'oBook = Nothing
            'oSheets = Nothing
            'oSheet = Nothing
            'oCells = Nothing
            'System.GC.Collect()
            'Return rutaREpositorioTemporales
            ''-----------------------------------------------------------------------------------------

            ''----
            oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Reporte x CC y Ppto y Subcatego"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de Solicitud de Presupuestos"
            End With

            With oExcel.Range(oCells(1, 2), oCells(1, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                ' .Value = "Reporte Presupuesto Operativo"
            End With


            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            End With

            Dim int_cont As Integer = 0
            Dim str_DescTipo As String = ""


            'Hoja1!$B$5:$Q$1809
            ''
            ''-----------------------------------------------------
            ''-----------------------------------------------------''
            objTablaDinamica = oSheet.PivotTables("tbl1")
            oSheet.Activate()

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()
            'Hoja1!$B$5:$Q$1809


            objTablaDinamica.PivotCache.SourceData = str_NombreEntidadReporte & "!F5C2:F" & fila & "C21"




            ''--------------------------------------------
            objTablaDinamica.AddDataField(objTablaDinamica.PivotFields("Gastado"), "Suma de Gastado", Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum)
            objTablaDinamica.AddDataField(objTablaDinamica.PivotFields("Saldo"), "Suma de Saldo", Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum)
            ''--------------------------------------------



            objTablaDinamica.PivotCache.Refresh()


            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            'Artículo

            ''--
            oPivotField = oSheet.PivotTables("tbl1").PivotFields("Artículo")

            For Each oPivotItem As Microsoft.Office.Interop.Excel.PivotItem In oPivotField.PivotItems
                If oPivotItem.Value = "Intereses hipotecarios" Or oPivotItem.Value = "Obras Constructivas 2013" Then 'Or oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" 'Then
                    With oPivotItem
                        If oPivotItem.RecordCount > 0 Then

                            .Visible = False
                        End If
                    End With
                Else

                    If oPivotItem.RecordCount > 0 Then
                        With oPivotItem

                            If oPivotItem.RecordCount > 0 Then
                                .Visible = True
                            End If
                        End With
                    End If




                End If
            Next
            ''--


            'With oSheet.PivotTables("tbl1")
            '    .PivotCache.Refresh()
            '    .ManualUpdate = False
            '    .RefreshTable()
            'End With

            CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl1").PivotCache.Refresh()
            CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl1").ManualUpdate = False
            CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tbl1").RefreshTable()
            oExcel.ActiveWindow.Zoom = 75

            ''-----------------------------------------------------''
            ''-----------------------------------------------------
            ''


            ''=================================================================================
            oSheet = CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Report x categoría y art x sede"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de Solicitud de Presupuestos"
            End With

            With oExcel.Range(oCells(1, 2), oCells(1, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                ' .Value = "Reporte Presupuesto Operativo"
            End With


            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            End With



            'Hoja1!$B$5:$Q$1809
            ''
            ''-----------------------------------------------------
            ''-----------------------------------------------------''
            objTablaDinamica = oSheet.PivotTables("tb2")
            oSheet.Activate()

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()
            'Hoja1!$B$5:$Q$1809


            objTablaDinamica.PivotCache.SourceData = str_NombreEntidadReporte & "!F5C2:F" & fila & "C21"




            ''--------------------------------------------
            objTablaDinamica.AddDataField(objTablaDinamica.PivotFields("Gastado"), "Suma de Gastado", Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum)
            objTablaDinamica.AddDataField(objTablaDinamica.PivotFields("Saldo"), "Suma de Saldo", Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum)
            ''--------------------------------------------

            objTablaDinamica.PivotCache.Refresh()


            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            'Artículo

            ''--
            oPivotField = Nothing
            oPivotField = oSheet.PivotTables("tb2").PivotFields("Artículo")

            For Each oPivotItem As Microsoft.Office.Interop.Excel.PivotItem In oPivotField.PivotItems
                If oPivotItem.Value = "Intereses hipotecarios" Or oPivotItem.Value = "Obras Constructivas 2013" Then 'Or oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" 'Then
                    With oPivotItem
                        If oPivotItem.RecordCount > 0 Then
                            .Visible = False
                        End If
                    End With
                Else
                    With oPivotItem
                        If oPivotItem.RecordCount > 0 Then

                            If .Visible = False Then


                                If oPivotItem.RecordCount > 0 Then
                                    .Visible = True
                                End If

                            End If
                        End If



                    End With
                End If
            Next
            ''--


            'With oSheet.PivotTables("tb2")
            '    .PivotCache.Refresh()
            '    .ManualUpdate = False
            '    .RefreshTable()
            'End With

            CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tb2").PivotCache.Refresh()
            CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tb2").ManualUpdate = False
            CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tb2").RefreshTable()
            oExcel.ActiveWindow.Zoom = 75

            ''-----------------------------------------------------''
            ''-----------------------------------------------------
            ''

            ''=================================================================================


            ''=================================================================================
            oSheet = CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Anális Gasto e Inv x art x sede"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de Solicitud de Presupuestos"
            End With

            With oExcel.Range(oCells(1, 2), oCells(1, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                ' .Value = "Reporte Presupuesto Operativo"
            End With


            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            End With







            'Hoja1!$B$5:$Q$1809
            ''
            ''-----------------------------------------------------
            ''-----------------------------------------------------''
            objTablaDinamica = oSheet.PivotTables("tb3")
            oSheet.Activate()

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()
            'Hoja1!$B$5:$Q$1809


            objTablaDinamica.PivotCache.SourceData = str_NombreEntidadReporte & "!F5C2:F" & fila & "C21"

            ''--------------------------------------------
            objTablaDinamica.AddDataField(objTablaDinamica.PivotFields("Gastado"), "Suma de Gastado", Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum)
            objTablaDinamica.AddDataField(objTablaDinamica.PivotFields("Saldo"), "Suma de Saldo", Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum)
            ''--------------------------------------------



            objTablaDinamica.PivotCache.Refresh()


            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            'Artículo

            ''--
            oPivotField = oSheet.PivotTables("tb3").PivotFields("Artículo")

            For Each oPivotItem As Microsoft.Office.Interop.Excel.PivotItem In oPivotField.PivotItems
                If oPivotItem.Value = "Intereses hipotecarios" Or oPivotItem.Value = "Obras Constructivas 2013" Then 'Or oPivotItem.Value = "Obras Constructivas 2013 - Instalaciones Adicionales" 'Then
                    With oPivotItem
                        If oPivotItem.RecordCount > 0 Then
                            .Visible = False
                        End If

                    End With
                Else
                    With oPivotItem
                        If oPivotItem.RecordCount > 0 Then
                            If .Visible = False Then
                                If oPivotItem.RecordCount > 0 Then
                                    .Visible = True

                                End If
                            End If
                        End If



                    End With
                End If
            Next
            ''--


            'With oSheet.PivotTables("tb3")
            '    .PivotCache.Refresh()
            '    .ManualUpdate = False
            '    .RefreshTable()
            'End With

            CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tb3").PivotCache.Refresh()
            CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tb3").ManualUpdate = False
            CType(oSheets.Item(4), Microsoft.Office.Interop.Excel.Worksheet).PivotTables("tb3").RefreshTable()
            oExcel.ActiveWindow.Zoom = 75

            ''-----------------------------------------------------''
            ''-----------------------------------------------------
            ''

            ''=================================================================================



            oExcel.ScreenUpdating = True



            oBook.Save()
            oBook.Close()

            'Quit Excel and thoroughly deallocate everything
            oExcel.Quit()
            ReleaseComObject(oCells)
            ReleaseComObject(oSheet)
            ReleaseComObject(oSheets)
            ReleaseComObject(oBook)
            ReleaseComObject(oBooks)
            ReleaseComObject(oExcel)
            oExcel = Nothing
            oBooks = Nothing
            oBook = Nothing
            oSheets = Nothing
            oSheet = Nothing
            oCells = Nothing
            System.GC.Collect()

            Return rutaREpositorioTemporales

        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Cargar combo de anio presupuestal"
    Sub cargarAniopresupuestal()
        Dim dtPresupuestos As New comon.DataTable

        Dim obl_combos As New bl_combos
        dtPresupuestos = obl_combos.listarAnioPresupuestal()
        ddwAnioPresupuestal.DataSource = dtPresupuestos
        ddwAnioPresupuestal.DataTextField = "PP_Descripcion"
        ddwAnioPresupuestal.DataValueField = "PP_CodigoPeriodo"
        ddwAnioPresupuestal.DataBind()
        Try
            obl_combos = New bl_combos

        Catch ex As Exception
        Finally

        End Try
    End Sub

#End Region

#Region "Llenar fuente de data "
    Private Shared Function LlenarPlantillaReporteDinamicoPresupuestosGerencia( _
      ByVal dtReporte As System.Data.DataTable, _
      ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
      ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
      ByVal str_NombreEntidadReporte As String) As String



        Dim fila As Integer = 5
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        'Pintado de Título
        With oExcel.Range(oCells(2, 3), oCells(2, 3))
            '.Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = str_NombreEntidadReporte
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 3), oCells(3, 3))
            '.Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        End With

        While cont_columnas <= dtReporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dtReporte.Columns(cont_columnas).ColumnName()
            With oExcel.Range(oCells(fila, columna + cont_columnas), oCells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .HorizontalAlignment = 3
            End With
            cont_columnas += 1
        End While

        cont_columnas = 0
        cont_filas = 0
        fila += 1

        While cont_columnas <= dtReporte.Columns.Count - 1
            While cont_filas <= dtReporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dtReporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas += 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        fila = fila + dtReporte.Rows.Count
        str_Fila = (fila - 1).ToString

        oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(5, 2), oCells(fila - 1, columna + cont_columnas - 1)))
        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function
#End Region


    Private Shared Sub cuadradoCompleto(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range)
        Try

            objRango.Select()
            With mexcel
                '.Range(Rango).Select()
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function




#Region "Cargar combo de presupuestos"


    Private Sub F_cargarCombo(ByVal codAnio As Integer)
        Try

            Dim dc As New Dictionary(Of String, Object)
            dc("periodo") = codAnio

            Dim dtPresuspuesto As New System.Data.DataTable
            Dim nParam As String = "USP_ListadoPresupuestoAnioPresupestal"
            dtPresuspuesto = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
            cmbPresupuesto.DataSource = dtPresuspuesto
            cmbPresupuesto.DataValueField = "codPresupuesto"
            cmbPresupuesto.DataTextField = "subSubSubCentroCosto"
            cmbPresupuesto.DataBind()
            cmbPresupuesto.Items.Insert(0, New ListItem("------------Seleccionar------------", "0"))

        Catch ex As Exception

        End Try
    End Sub
#End Region

    Protected Sub ddwAnioPresupuestal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddwAnioPresupuestal.SelectedIndexChanged


        F_cargarCombo(ddwAnioPresupuestal.SelectedValue)


    End Sub


#Region "Reporte  presupuesto  gastos "
    ''' <summary>
    ''' procedimiento  para exportar el presupuesto con los gastos realizados 
    ''' desarrollado por salcedo vila gaylussac  Win64gsv@hotmail.com
    ''' </summary>
    ''' <param name="codPresupuesto"></param>
    ''' <remarks></remarks>

    Public Sub F_ReporteGastos(ByVal codPresupuesto As Integer)
        Dim dtDetallePresupuesto As New System.Data.DataSet
        Dim dtDetallegastosPresupuesto As New System.Data.DataSet

        Try

            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
            ''
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            Dim filas As Integer = 4
            Dim filasTemp As Integer = 0
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)



            Dim ws = workbook.Worksheet(1) 'la primera hoja 
            Dim ws2 = workbook.Worksheet(2) 'la segunda  hoja 


            Dim oDA_reporteGastos As New DA_reporteGastos

            Dim dtPresupuestos As New System.Data.DataTable
            Dim dtGastos As New System.Data.DataTable

            Dim dtGastosDetalle As New System.Data.DataTable

            dtDetallePresupuesto = oDA_reporteGastos.F_listarPresupuestoServidor13Presupuesto(codPresupuesto)
            dtPresupuestos = dtDetallePresupuesto.Tables(0)
            dtDetallegastosPresupuesto = oDA_reporteGastos.F_listarPresupuestoServidor18Gastos(codPresupuesto)
            dtGastos = dtDetallegastosPresupuesto.Tables(0)


            dtGastosDetalle = oDA_reporteGastos.F_listarPresupuestoServidor18GastosDetalle(codPresupuesto)


            Dim sqlObject = (From pres In dtPresupuestos.AsEnumerable() Group pres By _
                                        anioPresu = pres("anioPresupuestal"), _
                                        nombreSede = pres("nombreSede"), _
                                        centroCosto = pres("centroCosto"), _
                                        tipoCambio = pres("tipoCambio"), _
                                        subCentroCosto = pres("subCentroCosto"), _
                                        subSubCentroCosto = pres("subSubCentroCosto"), _
                                        subSubSubCentroCosto = pres("subSubSubCentroCosto") _
                               Into detalle = Group _
                        Select New With { _
                                        .anioPresu = anioPresu, _
                                        .tipoCambio = tipoCambio, _
                                        .nombreSede = nombreSede, _
                                        .centroCosto = centroCosto, _
                                        .subCentroCosto = subCentroCosto, _
                                        .subSubCentroCosto = subSubCentroCosto, _
                                        .subSubSubCentroCosto = subSubSubCentroCosto, _
                                                .clases = (From cl In detalle.AsEnumerable() Group cl By nombreClase = cl("nombreClase") _
                                                          Into detalleClase = Group Select New _
                                                          With { _
                                                              .nombreClase = nombreClase, _
                                                              .totalGastado = (From dtClase In detalleClase.AsEnumerable() From gastos In dtGastos.AsEnumerable() Where gastos("codItem") = dtClase("codItem") Select New With {.gastos = gastos("montoGastado")}), _
                                                              .sumaTotalArticulos = (From arti In detalleClase.AsEnumerable() Select New With {.suma = arti("precioTotal")}), _
                                                              .articulos = (From art In detalleClase.AsEnumerable() _
                                                                             Select New With { _
                                                                                     .categoria = art("nombreCategorias"), _
                                                                                     .subCategoria = art("nombreSubcategoria"), _
                                                                                     .nombreArticulo = art("nombreArticulo"), _
                                                                                     .precioUnitario = art("precioUnitario"), _
                                                                                     .precioTotal = art("precioTotal"), _
                                                                                     .cantidad = art("cantidad"), _
                                                                                     .unidad = art("unidad"), _
                                                                                     .detalleGastos = (From dtGasto In dtGastosDetalle.AsEnumerable() _
                                                                                                         Where art("codItem") = dtGasto("CodigoItem") _
                                                                                                      Select New With { _
                                                                                                      .gasto = dtGasto("nPreMontoGastar"), .orden = dtGasto("IdOrden"), .nombreArticulo = dtGasto("Descripcion")}), _
                                                                                     .observacion = art("observacion"), _
                                                                                     .cantidadGastado = (From gastos In dtGastos.AsEnumerable() _
                                                                                                Where gastos("codItem") = _
                                                                                                art("codItem") Select gastos("montoGastado")).DefaultIfEmpty(0).First})})})









            '.Where(Function(obj) obj.detalleGastos.Count > 0)

            If sqlObject.Count = 0 Then
                Exit Sub
            End If

            Dim filaEmpieza As Integer = 3

            With ws.Range(ws.Cell(filaEmpieza, 2), ws.Cell(filaEmpieza, 7))
                .Merge()
                .Value = "PRESUPUESTO - AÑO ACADÉMICO  " & sqlObject(0).anioPresu & " - " & sqlObject(0).nombreSede
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With

            filaEmpieza += 1
            With ws.Range(ws.Cell(filaEmpieza, 2), ws.Cell(filaEmpieza, 7))
                .Merge()
                .Value = "Fecha de Reporte:  " & Date.Now.Day.ToString.PadLeft(2, "0") & "/" & Date.Now.Month.ToString.PadLeft(2, "0") & "/" & Date.Now.Year.ToString
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With
            filaEmpieza = 6


            With ws.Cell(filaEmpieza, 4)
                .Value = "Tipo de cambio "
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FF0000")
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With

            With ws.Cell(filaEmpieza + 1, 4)
                .Value = "S/." & sqlObject(0).tipoCambio
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With


            With ws.Cell(filaEmpieza, 1)
                .Value = "Centro de Costos:"
            End With
            With ws.Cell(filaEmpieza, 2)
                .Value = sqlObject(0).centroCosto
            End With
            filaEmpieza += 1
            With ws.Cell(filaEmpieza, 1)
                .Value = "Sub Centro de Costos:"
            End With
            With ws.Cell(filaEmpieza, 2)
                .Value = sqlObject(0).subCentroCosto
            End With
            filaEmpieza += 1

            With ws.Cell(filaEmpieza, 1)
                .Value = "Sub Sub Centro de Costos:"
            End With
            With ws.Cell(filaEmpieza, 2)
                .Value = sqlObject(0).subSubCentroCosto
            End With

            filaEmpieza += 1


            With ws.Cell(filaEmpieza, 1)
                .Value = "Sub Sub Sub Centro de Costos:"
            End With
            With ws.Cell(filaEmpieza, 2)
                .Value = sqlObject(0).subSubSubCentroCosto
            End With

            filaEmpieza += 1




            For Each Oclases In sqlObject(0).clases

                filaEmpieza += 2

                With ws.Cell(filaEmpieza, 1)
                    .Value = "Clase:"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                End With
                With ws.Cell(filaEmpieza, 2)
                    .Value = Oclases.nombreClase
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                End With
                filaEmpieza += 2


                Dim tempFilas As Integer = filaEmpieza
                With ws.Cell(filaEmpieza, 1)
                    .Value = "Categoria:"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 2)
                    .Value = "Sub Categoria :"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 3)
                    .Value = "Artículo :"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 4)
                    .Value = "Observación :"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 5)
                    .Value = "Cantidad :"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 6)
                    .Value = "Unidad de Medida :"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 7)
                    .Value = "Precio :"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 8)
                    .Value = "Precio Total :"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 9)
                    .Value = "Saldo Gastado"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With
                With ws.Cell(filaEmpieza, 10)
                    .Value = "Saldo a Favor"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                    .Style.Font.FontSize = 10
                End With

                For Each oarticulos In Oclases.articulos
                    filaEmpieza += 1
                    With ws.Cell(filaEmpieza, 1)
                        .Value = oarticulos.categoria
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(filaEmpieza, 2)
                        .Value = oarticulos.subCategoria
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(filaEmpieza, 3)
                        .Value = oarticulos.nombreArticulo
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(filaEmpieza, 4)
                        .Value = oarticulos.observacion
                        .Style.Font.FontSize = 8
                        '.Style.Alignment.WrapText = True
                    End With
                    With ws.Cell(filaEmpieza, 5)
                        .Value = oarticulos.cantidad
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(filaEmpieza, 6)
                        .Value = oarticulos.unidad
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(filaEmpieza, 7)
                        .Value = "S/." & Format(oarticulos.precioUnitario, "##,##0.00")
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(filaEmpieza, 8)
                        .Value = "S/." & Format(oarticulos.precioTotal, "##,##0.00")
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(filaEmpieza, 9)
                        .Value = "S/." & Format(oarticulos.cantidadGastado, "##,##0.00")
                        .Style.Font.FontSize = 8
                    End With

                    With ws.Cell(filaEmpieza, 10)
                        .Value = "S/." & Format(CDec(oarticulos.precioTotal) - CDec(oarticulos.cantidadGastado), "##,##0.00")
                        .Style.Font.FontSize = 8
                    End With


                Next



                With ws.Range(ws.Cell(tempFilas, 1), ws.Cell(filaEmpieza, 10))
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

                filaEmpieza += 1
                With ws.Cell(filaEmpieza, 7)
                    .Value = "Total :"
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#CCC0DA")
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin

                End With
                With ws.Cell(filaEmpieza, 8)
                    .Value = "S/." & Format(Oclases.sumaTotalArticulos.Sum(Function(os) CDec(os.suma)), "##,##0.00")
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With



                With ws.Cell(filaEmpieza, 9)
                    .Value = "S/." & Format(Oclases.totalGastado.Sum(Function(ogastos) CDec(ogastos.gastos)), "##,##0.00")
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With


                With ws.Cell(filaEmpieza, 10)
                    .Value = "S/." & Format(Oclases.sumaTotalArticulos.Sum(Function(os) CDec(os.suma)) - Oclases.totalGastado.Sum(Function(ogastos) CDec(ogastos.gastos)), "##,##0.00")  '("N2")
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                End With

            Next

            ''pintar el detalle de   los gastos de cada articulo en la segunda hoja 
            ''------------------------------------------------------------------------------------------------------------''

            Dim filaSegundaHoja As Integer = 3

            Dim filaTempSegundo As Integer = filaSegundaHoja

            With ws2.Range(ws2.Cell(filaSegundaHoja, 2), ws2.Cell(filaSegundaHoja, 7))
                .Merge()
                .Value = "PRESUPUESTO - AÑO ACADÉMICO  " & sqlObject(0).anioPresu & " - " & sqlObject(0).nombreSede
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With

            filaSegundaHoja += 1
            With ws2.Range(ws2.Cell(filaSegundaHoja, 2), ws2.Cell(filaSegundaHoja, 7))
                .Merge()
                .Value = "Fecha de Reporte:  " & Date.Now.Day.ToString.PadLeft(2, "0") & "/" & Date.Now.Month.ToString.PadLeft(2, "0") & "/" & Date.Now.Year.ToString
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With
            filaSegundaHoja = 6


            With ws2.Cell(filaSegundaHoja, 4)
                .Value = "Tipo de cambio "
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FF0000")
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With

            With ws2.Cell(filaSegundaHoja + 1, 4)
                .Value = "S/." & sqlObject(0).tipoCambio
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With

            With ws2.Cell(filaSegundaHoja, 1)
                .Value = "Centro de Costos:"
            End With
            With ws2.Cell(filaSegundaHoja, 2)
                .Value = sqlObject(0).centroCosto
            End With
            filaSegundaHoja += 1
            With ws2.Cell(filaSegundaHoja, 1)
                .Value = "Sub Centro de Costos:"
            End With
            With ws2.Cell(filaSegundaHoja, 2)
                .Value = sqlObject(0).subCentroCosto
            End With
            filaSegundaHoja += 1
            With ws2.Cell(filaSegundaHoja, 1)
                .Value = "Sub Sub Centro de Costos:"
            End With
            With ws2.Cell(filaSegundaHoja, 2)
                .Value = sqlObject(0).subSubCentroCosto
            End With
            filaSegundaHoja += 1
            With ws2.Cell(filaSegundaHoja, 1)
                .Value = "Sub Sub Sub Centro de Costos:"
            End With
            With ws2.Cell(filaSegundaHoja, 2)
                .Value = sqlObject(0).subSubSubCentroCosto
            End With


            filaSegundaHoja += 2

            filaTempSegundo = filaSegundaHoja

            With ws2.Range(ws2.Cell(filaSegundaHoja, 1), ws2.Cell(filaSegundaHoja, 3))
                .Merge()
                .Value = "Datos de Presupuesto"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#16365C")
                .Style.Font.FontColor = XLColor.White



                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With
            With ws2.Range(ws2.Cell(filaSegundaHoja, 4), ws2.Cell(filaSegundaHoja, 6))
                .Merge()
                .Value = "Datos de las compras"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#00B050")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With

            filaSegundaHoja += 1

            With ws2.Cell(filaSegundaHoja, 1)
                .Value = "Sub Categoría"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
            End With
            With ws2.Cell(filaSegundaHoja, 2)
                .Value = "Artículo"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
            End With
            With ws2.Cell(filaSegundaHoja, 3)
                .Value = "Monto"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
            End With
            With ws2.Cell(filaSegundaHoja, 4)
                .Value = "N° de Orden"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92D050")
            End With

            With ws2.Cell(filaSegundaHoja, 5)
                .Value = "Monto Gastado"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92D050")
            End With
            With ws2.Cell(filaSegundaHoja, 6)
                .Value = "Artículo de Compra"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92D050")
            End With


            With ws2.Range(ws2.Cell(filaSegundaHoja - 1, 7), ws2.Cell(filaSegundaHoja, 7))
                .Merge()
                .Value = "Saldo"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#92D050")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
            End With



            filaSegundaHoja += 1
            For Each Oclases In sqlObject(0).clases

                For Each articulos In Oclases.articulos.Where(Function(obj) obj.detalleGastos.Count > 0)

                    With ws2.Cell(filaSegundaHoja, 1)
                        .Value = articulos.subCategoria
                        .Style.Font.FontSize = 8
                    End With
                    With ws2.Cell(filaSegundaHoja, 2)
                        .Value = articulos.nombreArticulo
                        .Style.Font.FontSize = 8
                    End With
                    With ws2.Cell(filaSegundaHoja, 3)
                        .Value = "S/." & Format(articulos.precioTotal, "##,##0.00")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Font.FontSize = 8
                    End With

                    For Each ogastos In articulos.detalleGastos

                        With ws2.Cell(filaSegundaHoja, 5)
                            .Value = "S/." & Format(ogastos.gasto, "##,##0.00")
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Font.FontSize = 8
                        End With
                        With ws2.Cell(filaSegundaHoja, 4)
                            .Value = ogastos.orden
                            .Style.Font.FontSize = 8
                        End With
                        With ws2.Cell(filaSegundaHoja, 6)
                            .Value = ogastos.nombreArticulo
                            .Style.Font.FontSize = 8
                        End With
                        filaSegundaHoja += 1
                    Next
                    With ws2.Cell(filaSegundaHoja, 5)
                        .Value = "S/." & Format(articulos.detalleGastos.Sum(Function(og) CDec(og.gasto)), "##,##0.00")
                        .Style.Font.Bold = True
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Font.FontSize = 8
                    End With

                    With ws2.Cell(filaSegundaHoja, 2)
                        .Value = "Total:"
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                        .Style.Font.Bold = True
                        .Style.Font.FontSize = 8
                    End With

                    ''
                    With ws2.Cell(filaSegundaHoja, 7)
                        .Value = "S/." & Format(CDec(articulos.precioTotal) - articulos.detalleGastos.Sum(Function(og) CDec(og.gasto)), "##,##0.00")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Font.Bold = True
                        .Style.Font.FontSize = 8
                    End With
                    ''
                    With ws2.Cell(filaSegundaHoja, 3)
                        .Value = "S/." & Format(articulos.precioTotal, "##,##0.00")
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Font.Bold = True
                        .Style.Font.FontSize = 8
                    End With


                    With ws2.Range(ws2.Cell(filaSegundaHoja, 2), ws2.Cell(filaSegundaHoja, 5))
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#DCE6F1")
                        .Style.Font.FontSize = 8
                    End With




                    filaSegundaHoja += 1

                Next
            Next


            'filaTempSegundo

            With ws2.Range(ws2.Cell(filaTempSegundo, 1), ws2.Cell(filaSegundaHoja - 1, 7))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With



            ''------------------------------------------------------------------------------------------------------------''



            ws.PageSetup.AdjustTo(60)
            ws.PageSetup.Margins.Bottom = 0.75 '1.9
            ws.PageSetup.Margins.Left = 0.7 '0.6
            ws.PageSetup.Margins.Right = 0.7 '0.6
            ws.PageSetup.Margins.Header = 0.3 '0.8
            ws.PageSetup.Margins.Footer = 0.3 '0.8


            ws.Column(1).Width = 31
            ws.Column(2).Width = 31
            ws.Column(3).Width = 40
            ws.Column(4).Width = 45
            ws.Column(5).Width = 9
            ws.Column(6).Width = 20
            ws.Column(7).Width = 9
            ws.Column(8).Width = 11
            ws.Column(9).Width = 12
            ws.Column(10).Width = 11



            ws.Column(4).Hide()
            ws.Column(5).Hide()
            ws.Column(6).Hide()
            ws.Column(7).Hide()

            ws2.Column(1).Width = 34
            ws2.Column(2).Width = 44
            ws2.Column(3).Width = 44
            ws2.Column(4).Width = 13

            ws2.Column(5).Width = 17
            ws2.Column(6).Width = 45
            ws2.Column(7).Width = 17






            ws.PageSetup.PagesWide = 1
            ws.Name = "Reporte Gastos"
            ws2.Name = "Reporte detalle gastos "

            workbook.Save()
            Dim downloadBytes1 As Byte()
            downloadBytes1 = File.ReadAllBytes(rutaREpositorioTemporales)


            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "ReporteGastos" & rutaTemp.ToString() & ".xlsx" + "; size=" + downloadBytes1.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes1)
            Response.End()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " Estados reporrtes "
    Private Sub estadoVisibilidadReportes(ByVal estado As Boolean)
        Try
            cmbPresupuesto.Visible = estado
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class