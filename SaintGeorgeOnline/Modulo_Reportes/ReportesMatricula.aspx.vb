Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloReportes
Imports SaintGeorgeOnline_DataAccess.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices.Marshal
Imports ClosedXML.Excel
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas

''' <summary>
''' Modulo de Reportes de Matrícula   
''' </summary>
''' <remarks>
''' Código del Modulo:    X
''' Código de la Opción:  X
''' </remarks>

Partial Class Modulo_Reportes_ReportesMatricula
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Reportes de Matrícula")

            btnReporteExportar.Attributes.Add("onclick", "ShowMyModalPopup()")

            If Not Page.IsPostBack Then

                cargarListaReportes()
                cargarListaPresentacion()

                pnlReporte1.Visible = True
                pnlReporte2.Visible = False

                cargarComboAniosAcademicos()
                cargarComboGrado()

                ddlRep1_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                ddlRep2_Periodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar

                tbRep1_FechaInicio.Text = Today.AddDays(-7).ToShortDateString
                tbRep1_FechaFin.Text = Today

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub lstReportes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarListaPresentacion()
            mostrarPanelParametros()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnReporteExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If lstReportes.SelectedValue.ToString.Length > 0 And lstPresentacion.SelectedValue.ToString.Length > 0 Then
                If lstPresentacion.SelectedValue = 33 Or lstPresentacion.SelectedValue = 35 Or lstPresentacion.SelectedValue = 39 Or lstPresentacion.SelectedValue = 98 Then
                    Exportar()
                ElseIf lstPresentacion.SelectedValue = 34 Then
                    ExportarPasosMatricula()
                End If
            Else
                Me.Master.MostrarMensajeAlert("Debe seleccionar un Reporte y su Presentación.")
                Exit Sub
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarListaReportes()

        Dim int_CodigoTipoReporte As Integer = 6 ' Reportes de Matrícula
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

        Dim dt As DataTable = CType(ViewState("ListaReportes"), DataSet).Tables(1)
        Dim int_CodigoReporte As Integer = lstReportes.SelectedValue

        Dim dv As DataView = dt.DefaultView

        With dv
            .RowFilter = "1=1 and CodigoReporte = " & int_CodigoReporte
        End With

        lstPresentacion.DataSource = dv
        lstPresentacion.DataTextField = "Descripcion"
        lstPresentacion.DataValueField = "CodigoDetalle"
        lstPresentacion.DataBind()

        If dv.Count > 0 Then
            lstPresentacion.SelectedIndex = 0
        End If

    End Sub

    Private Sub mostrarPanelParametros()

        If lstReportes.SelectedValue = 17 Then ' Reporte : Alumnos Matriculados

            pnlReporte1.Visible = True
            pnlReporte2.Visible = False

        ElseIf lstReportes.SelectedValue = 18 Then ' Reporte : Alumnos No Matriculados

            pnlReporte1.Visible = False
            pnlReporte2.Visible = True

        Else

            pnlReporte1.Visible = False
            pnlReporte2.Visible = False

        End If

    End Sub

    Private Sub limpiarCombos(ByVal combo As DropDownList, ByVal bool_Todos As Boolean, ByVal bool_Seleccione As Boolean)

        Controles.limpiarCombo(combo, bool_Todos, bool_Seleccione)

    End Sub


    Private Sub cargarComboAniosAcademicos()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlRep1_Periodo, ds_Lista, "Codigo", "Descripcion", False, False)
        Controles.llenarCombo(ddlRep2_Periodo, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    Private Sub cargarComboGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlRep1_Grados, ds_Lista, "Codigo", "DescripcionEspaniol", True, False)

    End Sub


    ''' <summary>
    ''' Exporta los datos del gridView en formato HTML
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     30/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________
    ''' </remarks>
    Private Sub Exportar()

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet
        Dim obj_BL_Matricula As New bl_Matricula

        Dim bool_Parametros As Boolean = False
        Dim bool_MostrarTotales As Boolean = False


        Dim int_TipoReporte As Integer = lstReportes.SelectedValue 'Tipo reporte
        Dim int_PresentacionReporte As Integer = lstPresentacion.SelectedValue 'Tipo reporte


        Dim str_TituloReporte As String = "" 'Titulo reporte
        Dim Arreglo_Parametros As New ArrayList 'Arreglo de parametros para la visualizacion en el reporte

        Dim reporte_html As String = "" 'Contenido del reporte

        Dim dt As DataTable = New DataTable("ListaExportar")

        Dim int_CodigoPeriodoAcademico As Integer
        Dim dt_FechaRangoInicio As Date
        Dim dt_FechaRangoFin As Date
        Dim int_CodigoGrado As Integer
        Dim bool_Valido As Boolean = False

        If int_TipoReporte = 18 And int_PresentacionReporte = 98 Then

            reporteSituacionFinal(CInt(ddlRep2_Periodo.SelectedItem.Text))

        End If


        If int_TipoReporte = 17 Then ' Reporte : Reportes de Alumnos Matriculados

            int_CodigoPeriodoAcademico = ddlRep1_Periodo.SelectedValue
            dt_FechaRangoInicio = tbRep1_FechaInicio.Text
            dt_FechaRangoFin = tbRep1_FechaFin.Text
            int_CodigoGrado = ddlRep1_Grados.SelectedValue

            If int_PresentacionReporte = 33 Then  ' Presentación : Por Fecha y Grado

                str_TituloReporte = "MiReporte"
                ds_Lista = obj_BL_Matricula.FUN_REP_AlumnosMatriculadosXGradoFechas( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, int_CodigoGrado, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "#", "integer")
                dt = Datos.agregarColumna(dt, "Codigo", "string")
                dt = Datos.agregarColumna(dt, "Apellidos y Nombres", "string")
                dt = Datos.agregarColumna(dt, "Grado", "string")
                dt = Datos.agregarColumna(dt, "Fecha Registro", "string")
                dt = Datos.agregarColumna(dt, "Año", "integer")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("#") = dr.Item("codigoGrado")
                    auxDR.Item("Codigo") = dr.Item("codigoAlumno")
                    auxDR.Item("Apellidos y Nombres") = dr.Item("NombreCompleto")
                    auxDR.Item("Grado") = dr.Item("Grado")
                    auxDR.Item("Fecha Registro") = dr.Item("FechaMatricula")
                    auxDR.Item("Año") = dr.Item("AnioMatricula")
                    dt.Rows.Add(auxDR)
                Next

            End If

            If int_PresentacionReporte = 35 Then  ' Presentación : Por Fecha y Grado

                str_TituloReporte = "MiReporte"
                ds_Lista = obj_BL_Matricula.FUN_REP_AlumnosMatriculadosXGrado( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, int_CodigoGrado, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "#", "integer")
                dt = Datos.agregarColumna(dt, "Codigo", "string")
                dt = Datos.agregarColumna(dt, "Apellidos y Nombres", "string")
                dt = Datos.agregarColumna(dt, "Grado", "string")
                dt = Datos.agregarColumna(dt, "Año", "integer")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("#") = dr.Item("codigoGrado")
                    auxDR.Item("Codigo") = dr.Item("codigoAlumno")
                    auxDR.Item("Apellidos y Nombres") = dr.Item("NombreCompleto")
                    auxDR.Item("Grado") = dr.Item("Grado")
                    auxDR.Item("Año") = dr.Item("AnioMatricula")
                    dt.Rows.Add(auxDR)
                Next

            End If

        ElseIf int_TipoReporte = 18 Then ' Reporte : Reportes de Alumnos NO Matriculados

            int_CodigoPeriodoAcademico = ddlRep2_Periodo.SelectedValue

            If int_PresentacionReporte = 39 Then  ' Presentación : Grado

                str_TituloReporte = "MiReporte"
                ds_Lista = obj_BL_Matricula.FUN_REP_AlumnosNoMatriculadosXGrado( _
                    int_CodigoPeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

                dt = Datos.agregarColumna(dt, "#", "integer")
                dt = Datos.agregarColumna(dt, "Codigo", "string")
                dt = Datos.agregarColumna(dt, "Apellidos y Nombres", "string")
                dt = Datos.agregarColumna(dt, "Grado", "string")
                dt = Datos.agregarColumna(dt, "Año", "integer")

                Dim cont As Integer = 1
                Dim auxDR As DataRow

                For Each dr As DataRow In ds_Lista.Tables(0).Rows
                    auxDR = dt.NewRow
                    auxDR.Item("#") = dr.Item("codigoGrado")
                    auxDR.Item("Codigo") = dr.Item("codigoAlumno")
                    auxDR.Item("Apellidos y Nombres") = dr.Item("NombreCompleto")
                    auxDR.Item("Grado") = dr.Item("Grado")
                    auxDR.Item("Año") = dr.Item("AnioMatricula")
                    dt.Rows.Add(auxDR)
                Next

            End If

        End If

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()

        If Not dt.Rows.Count > 0 Then
            Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
            Exit Sub
        End If

        If int_TipoReporte = 17 Then ' Reporte : Reportes de Alumnos Matriculados

            If int_PresentacionReporte = 33 Then ' Presentación : Por Fecha y Grado

                NombreArchivo = ExportarReporteDinamicoMatriculadosPorFechayGrado(dt, str_TituloReporte)

            End If
            If int_PresentacionReporte = 35 Then ' Presentación : Por Grado

                NombreArchivo = ExportarReporteDinamicoMatriculadosPorGrado(dt, str_TituloReporte)

            End If

        ElseIf int_TipoReporte = 18 Then ' Reporte : Reportes de Alumnos NO Matriculados

            If int_PresentacionReporte = 39 Then ' Presentación : Por Grado

                NombreArchivo = ExportarReporteDinamicoNoMatriculadosPorGrado(dt, str_TituloReporte)

            End If

        End If

        NombreArchivo = NombreArchivo & ".xls"

        RutaMadre = Server.MapPath(".")
        RutaMadre = RutaMadre.Replace("\Modulo_Reportes", "\Reportes\")

        downloadBytes = File.ReadAllBytes(RutaMadre & NombreArchivo)

        'Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

    End Sub

    Private Sub ExportarPasosMatricula()

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet
        Dim obj_BL_Matricula As New bl_Matricula

        Dim bool_Parametros As Boolean = False
        Dim bool_MostrarTotales As Boolean = False


        Dim int_TipoReporte As Integer = lstReportes.SelectedValue 'Tipo reporte
        Dim int_PresentacionReporte As Integer = lstPresentacion.SelectedValue 'Tipo reporte


        Dim str_TituloReporte As String = "" 'Titulo reporte
        Dim Arreglo_Parametros As New ArrayList 'Arreglo de parametros para la visualizacion en el reporte

        Dim reporte_html As String = "" 'Contenido del reporte
        Dim Arreglo_Datos As String() 'Arreglo de datos del reporte (cabecera y detalle)

        Dim dt As DataTable = New DataTable("ListaExportar")

        Dim int_CodigoPeriodoAcademico As Integer
        Dim dt_FechaRangoInicio As Date
        Dim dt_FechaRangoFin As Date
        Dim int_CodigoGrado As Integer
        Dim bool_Valido As Boolean = False

        If int_TipoReporte = 17 Then ' Reporte : Reportes de Alumnos Matriculados

            int_CodigoPeriodoAcademico = ddlRep1_Periodo.SelectedValue
            dt_FechaRangoInicio = tbRep1_FechaInicio.Text
            dt_FechaRangoFin = tbRep1_FechaFin.Text
            int_CodigoGrado = ddlRep1_Grados.SelectedValue

            If int_PresentacionReporte = 34 Then  ' Presentación : Por Fecha y Grado

                str_TituloReporte = "MiReporte"
                ds_Lista = obj_BL_Matricula.FUN_REP_AlumnosPasosMatricula( _
                    int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, int_CodigoGrado, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            End If
        End If

        'LLenado de reporte
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()

        If Not ds_Lista.Tables(0).Rows.Count > 0 Then
            Me.Master.MostrarMensajeAlert("La consulta no encontro ningún registro.")
            Exit Sub
        End If

        If int_TipoReporte = 17 Then ' Reporte : Reportes de Alumnos Matriculados

            If int_PresentacionReporte = 34 Then ' Presentación : Por Fecha y Grado
                NombreArchivo = ExportarReporteDinamicoPasosMatricula(ds_Lista, str_TituloReporte)

            End If

        End If

        NombreArchivo = NombreArchivo & ".xls"

        RutaMadre = Server.MapPath(".")
        RutaMadre = RutaMadre.Replace("\Modulo_Reportes", "\Reportes\")

        downloadBytes = File.ReadAllBytes(RutaMadre & NombreArchivo)

        'Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     30/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)

        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")

    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     30/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

#End Region

#Region "Reportes 17"

#End Region

#Region "Reportes 18"

#End Region

#Region "Exportacion Reportes"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function

    'Reporte Codigo : 17 - 33
    Public Shared Function ExportarReporteDinamicoMatriculadosPorFechayGrado(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteMatricula17_33").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteMatriculadosPorFechayGrado(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells

        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Alumnos Matrículados por Fecha y Grado"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            'Now.ToString("dddd, MMMM d, yyyy h:mm") & " " & Now.ToString("tt").ToLower()

        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica.PivotCache.SourceData = "MiReporte!F5C2:F" & fila & "C7"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        While int_cont <= dtReporte.Rows.Count - 1
            str_DescTipo = dtReporte.Rows(int_cont).Item("Grado")
            oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = True
            int_cont = int_cont + 1
        End While

        'Margen
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("F1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        oExcel.Range("a1").Select()

        oSheet.SaveAs(sFile)

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

        Return nombreRep
    End Function

    'Reporte Codigo : 17 - 34
    Public Shared Function ExportarReporteDinamicoPasosMatricula(ByVal dtReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteMatricula17_34").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        Dim dtAlumno As DataTable
        Dim dtMatriculados As DataTable
        Dim dtPasosMatricula As DataTable
        Dim dtFechaMatricula As DataTable

        dtMatriculados = dtReporte.Tables(0)
        dtAlumno = dtReporte.Tables(1)
        dtPasosMatricula = dtReporte.Tables(2)
        dtFechaMatricula = dtReporte.Tables(3)

        LlenarPlantillaReportePasoMatricula(dtMatriculados, dtAlumno, dtPasosMatricula, dtFechaMatricula, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)

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

        Return nombreRep
    End Function

    'Reporte Codigo : 17 - 35
    Public Function ExportarReporteDinamicoMatriculadosPorGrado(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteMatricula17_35").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaReporteMatriculadosPorGrado(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinámico"
        oCells = oSheet.Cells

        'Pintado de Título
        With oExcel.Range(oCells(2, 2), oCells(2, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Reporte de Alumnos Matrículados por Grado"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 2), oCells(3, 2))
            .Merge()
            .HorizontalAlignment = 1
            .Font.Bold = True
            .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            'Now.ToString("dddd, MMMM d, yyyy h:mm") & " " & Now.ToString("tt").ToLower()

        End With

        Dim int_cont As Integer = 0
        Dim str_DescTipo As String = ""

        Dim dv_Grado As DataView
        dv_Grado = dtReporte.DefaultView

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica.PivotCache.SourceData = "MiReporte!F5C2:F" & fila & "C6"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        While int_cont <= dtReporte.Rows.Count - 1
            str_DescTipo = dtReporte.Rows(int_cont).Item("Grado")
            oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = True
            int_cont = int_cont + 1
        End While

        oSheet.SaveAs(sFile)

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

        Return nombreRep
    End Function

    'Reporte Codigo : 18 - 39
    Public Function ExportarReporteDinamicoNoMatriculadosPorGrado(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

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

            sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
            sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteMatricula18_39").ToString()

            oExcel.Visible = False : oExcel.DisplayAlerts = False

            ''Start a new workbook 
            oBooks = oExcel.Workbooks
            oBooks.Open(sTemplate) 'Load colorful template with graph
            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets
            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = str_NombreEntidadReporte
            oCells = oSheet.Cells

            fila = LlenarPlantillaReporteNoMatriculadosPorGrado(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

            oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Reporte Dinámico"
            oCells = oSheet.Cells

            'Pintado de Título
            With oExcel.Range(oCells(2, 2), oCells(2, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Reporte de Alumnos No Matrículados por Grado"
            End With

            'Pintado de Fecha 
            With oExcel.Range(oCells(3, 2), oCells(3, 2))
                .Merge()
                .HorizontalAlignment = 1
                .Font.Bold = True
                .Value = "Fecha de Reporte: " & Now.Date & " " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
                'Now.ToString("dddd, MMMM d, yyyy h:mm") & " " & Now.ToString("tt").ToLower()

            End With

            Dim int_cont As Integer = 0
            Dim str_DescTipo As String = ""

            Dim dv_Grado As DataView
            dv_Grado = dtReporte.DefaultView

            objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
            oSheet.Activate()

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            objTablaDinamica.PivotCache.SourceData = "MiReporte!F5C2:F" & fila & "C6"
            objTablaDinamica.PivotCache.Refresh()

            oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()

            Dim contador As Integer = 0
            While int_cont <= dtReporte.Rows.Count - 1
                str_DescTipo = dtReporte.Rows(int_cont).Item("Grado")

                ' If oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = False Then
                contador += 1

                'If Not oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado") Is Nothing Then
                '    If contador > 3 Then

                If str_DescTipo <> "" Then
                    oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescTipo).ShowDetail = True
                End If

                '    End If


                'End If

                ' End If


                int_cont = int_cont + 1
            End While



            oSheet.SaveAs(sFile)

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
            Return nombreRep
        Catch ex As Exception

        End Try

    End Function


    Private Shared Function LlenarPlantillaReporteMatriculadosPorFechayGrado( _
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
        With oExcel.Range(oCells(2, 3), oCells(2, 5))
            '.Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Reporte de Alumnos Matriculados"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 3), oCells(3, 5))
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

    Private Shared Sub LlenarPlantillaReportePasoMatricula( _
            ByVal dt_Matriculados As System.Data.DataTable, ByVal dtAlumnos As System.Data.DataTable, ByVal dtPasoMatricula As System.Data.DataTable, ByVal dtFechaMatricula As System.Data.DataTable, _
            ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
            ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
            ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 7
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim int_contGrado As Integer = 0

        columna = 8

        'Pintado de Titulo
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Value = "Reporte de Pasos de la Matrícula  "
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Merge()
        oExcel.Range(oCells(4, 5), oCells(4, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Font.Bold = True

        'Pintado de Cabecera estática 
        oExcel.Range(oCells(7, 2), oCells(8, 2)).Merge()
        oExcel.Range(oCells(7, 2), oCells(8, 2)).Value = "Alumno"
        oExcel.Range(oCells(7, 3), oCells(8, 3)).Merge()
        oExcel.Range(oCells(7, 3), oCells(8, 3)).Value = "Responsable de Matrícula"
        oExcel.Range(oCells(7, 4), oCells(8, 4)).Merge()
        oExcel.Range(oCells(7, 4), oCells(8, 4)).Value = "Parentesco"

        oExcel.Range(oCells(7, 2), oCells(8, 4)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 2), oCells(8, 4)).WrapText = True
        oExcel.Range(oCells(7, 2), oCells(8, 4)).Font.Bold = True
        oExcel.Range(oCells(7, 2), oCells(8, 4)).Interior.Color() = RGB(204, 255, 204)

        'Pintado del cuadrado de Cabecera estática 
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(7, 2), oExcel.Cells(8, 4)))

        'Pintado de Cabecera estática de los pasos de matricula
        oExcel.Range(oCells(7, 5), oCells(7, 7)).Merge()
        oExcel.Range(oCells(7, 5), oCells(7, 7)).Value = "Actualizaión de Datos"
        oExcel.Range(oCells(7, 8), oCells(7, 13)).Merge()
        oExcel.Range(oCells(7, 8), oCells(7, 13)).Value = "Matrícula"
        oExcel.Range(oCells(8, 13), oCells(8, 13)).Value = "Fecha de la Matrícula"

        'Inmovilizar paneles
        Dim objRangoajuste4 As Microsoft.Office.Interop.Excel.Range = oExcel.Range("A9")
        objRangoajuste4.Select()
        oExcel.ActiveWindow.FreezePanes = True

        ' Cabecera Dinámica
        Dim FilaCab As Integer = 8
        Dim ColumnaCab As Integer = 5
        Dim cont As Integer = 0

        While cont <= dtPasoMatricula.Rows.Count - 1

            oExcel.Range(oExcel.Cells(FilaCab, ColumnaCab), oExcel.Cells(FilaCab, ColumnaCab)).HorizontalAlignment = 3
            oExcel.Cells(FilaCab, ColumnaCab) = dtPasoMatricula.Rows(cont).Item("PasoMatricula").ToString
            cont = cont + 1
            ColumnaCab = ColumnaCab + 1

        End While

        oExcel.Range(oExcel.Cells(7, 5), oExcel.Cells(FilaCab, ColumnaCab)).Select()
        'oExcel.Range(oExcel.Cells(7, 5), oExcel.Cells(FilaCab, ColumnaCab - 1)).Orientation = 90
        'oExcel.Range(oExcel.Cells(FilaCab, 5), oExcel.Cells(FilaCab, ColumnaCab - 1)).RowHeight = 150
        oExcel.Range(oExcel.Cells(FilaCab, 5), oExcel.Cells(FilaCab, ColumnaCab)).WrapText = True
        oExcel.Range(oExcel.Cells(7, 5), oExcel.Cells(FilaCab, ColumnaCab)).Font.Bold = True
        oExcel.Range(oExcel.Cells(7, 5), oExcel.Cells(FilaCab, ColumnaCab)).Interior.Color() = RGB(204, 255, 204)

        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(7, 5), oExcel.Cells(FilaCab, ColumnaCab)))

        cont = 0

        ' Detalle Alumnos
        fila = 9
        columna = 2

        ' Detalle de Devoluciones
        Dim filaDet As Integer = 9
        Dim columnnaDet As Integer = 5
        Dim contDv As Integer = 0

        'dv.RowFilter = "CodigoAlumno = '" & str_CodigoAlumno & "'"
        Dim cont_LibDevueltos As Integer = 0
        Dim cont_LibFaltantes As Integer = 0

        While cont <= dtAlumnos.Rows.Count - 1

            oExcel.Range(oExcel.Cells(fila, columna + 0), oExcel.Cells(fila, columna + 0)).HorizontalAlignment = 1
            oExcel.Range(oExcel.Cells(fila, columna + 1), oExcel.Cells(fila, columna + 1)).HorizontalAlignment = 2
            oExcel.Range(oExcel.Cells(fila, columna + 2), oExcel.Cells(fila, columna + 2)).HorizontalAlignment = 3

            oExcel.Cells(fila, columna + 0) = dtAlumnos.Rows(cont).Item("NombreCompletoAlumno").ToString
            oExcel.Cells(fila, columna + 1) = dtAlumnos.Rows(cont).Item("NombreCompletoFamiliar").ToString
            oExcel.Cells(fila, columna + 2) = dtAlumnos.Rows(cont).Item("Parentesco").ToString

            Dim str_CodigoAlumno As String = ""
            Dim int_codigoLibro As Integer = 0
            Dim cont_colLibros As Integer = 0
            str_CodigoAlumno = dtAlumnos.Rows(cont).Item("codigoAlumno").ToString

            While cont_colLibros <= dtPasoMatricula.Rows.Count - 1 'Recorrido de Pasos de la Matricula

                int_codigoLibro = dtPasoMatricula.Rows(cont_colLibros).Item("CodigoPasoMatricula")

                Dim dv As DataView = dt_Matriculados.DefaultView

                dv.RowFilter = "1=1 and codigoAlumno=" & str_CodigoAlumno & " and CodigoPasoMatricula =" & int_codigoLibro.ToString

                If dv.Count > 0 Then 'Existe prestamo 
                    While contDv <= dv.Count - 1
                        If int_codigoLibro = dv.Item(contDv).Item("CodigoPasoMatricula") Then

                            oExcel.Cells(fila, columnnaDet) = "X"

                        End If
                        contDv = contDv + 1
                    End While
                Else ' No hay un prestamo
                    oExcel.Cells(fila, columnnaDet) = ""

                End If

                contDv = 0
                columnnaDet = columnnaDet + 1
                cont_colLibros = cont_colLibros + 1
            End While


            Dim dvf As DataView = dtFechaMatricula.DefaultView
            Dim contDvf As Integer = 0

            dvf.RowFilter = "1=1 and codigoAlumno=" & str_CodigoAlumno

            If dvf.Count > 0 Then '
                'While contDvf <= dvf.Count - 1
                '    If 5 = dvf.Item(contDvf).Item("CodigoPasoMatricula") Then

                oExcel.Cells(fila, columnnaDet) = dvf.Item(0).Item("FechaRegistroMatricula")

                '    End If
                '    contDvf = contDvf + 1
                'End While
            Else ' 
                oExcel.Cells(fila, columnnaDet) = ""

            End If


            columnnaDet = 5
            fila = fila + 1
            cont = cont + 1
        End While

        oExcel.Range(oExcel.Cells(9, 4), oExcel.Cells(fila - 1, 13)).HorizontalAlignment = 3
        oExcel.Range(oExcel.Cells(9, 5), oExcel.Cells(fila - 1, 7)).Interior.Color() = RGB(253, 233, 217)
        oExcel.Range(oExcel.Cells(9, 8), oExcel.Cells(fila - 1, 13)).Interior.Color() = RGB(228, 223, 236)
        oExcel.Range(oCells(9, 2), oCells(fila - 1, 4)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(9, 2), oCells(fila - 1, 13)))
        oExcel.ActiveWindow.Zoom = 75


    End Sub

    Private Shared Function LlenarPlantillaReporteMatriculadosPorGrado( _
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
        With oExcel.Range(oCells(2, 3), oCells(2, 4))
            '.Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Reporte de Alumnos Matriculados"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 3), oCells(3, 4))
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
        oExcel.Range("a3").Select()
        'Cerrar Ventana
        oExcel.ActiveWorkbook.ShowPivotTableFieldList = False
        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function

    Private Shared Function LlenarPlantillaReporteNoMatriculadosPorGrado( _
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
        With oExcel.Range(oCells(2, 3), oCells(2, 4))
            '.Merge()
            .HorizontalAlignment = 3
            .Font.Bold = True
            .Value = "Reporte de Alumnos No Matriculados"
        End With

        'Pintado de Fecha 
        With oExcel.Range(oCells(3, 3), oCells(3, 4))
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
        oExcel.Range("a3").Select()
        'Cerrar Ventana
        oExcel.ActiveWorkbook.ShowPivotTableFieldList = False
        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function


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




#End Region
    ''' <summary>
    ''' reporte presentacion reporte 98
    ''' </summary>
    ''' <param name="anio"> anio seleccionado para saber </param>
    ''' <remarks></remarks>
#Region "Reporte no matriculados  "
    Public Sub reporteSituacionFinal(ByVal anio As Integer)
        Try
            Dim dc As New Dictionary(Of String, Object)
            dc("anioActual") = anio
            Dim dtSituacionFinal As New System.Data.DataTable
            Dim nParam As String = "LIstarFany"
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
            ''
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)

            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)
            dtSituacionFinal = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
            Dim ws = workbook.Worksheet(1)



            Dim filaIncial As Integer = 1

            Dim amPm As String = ""
            amPm = IIf(Date.Now.Hour > 12, "Pm", "Am")
            Dim fecha As String = Date.Now.Day.ToString.PadLeft(2, "0") _
                & "/" & Date.Now.Month.ToString.PadLeft(2, "0") & "/" _
                & Date.Now.Year.ToString & " " _
                & Date.Now.Hour.ToString.PadLeft(2, "0") & ":" _
                & Date.Now.Minute.ToString.PadLeft(2, "0") & ":" _
                & Date.Now.Second.ToString.PadLeft(2, "0") & " " & amPm


            With ws.Range(ws.Cell(filaIncial, 1), ws.Cell(filaIncial, 4))
                .Merge()
                .Value = "Reporte de Alumnos no Matriculados"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With
            filaIncial += 1
            With ws.Range(ws.Cell(filaIncial, 1), ws.Cell(filaIncial, 4))
                .Merge()
                .Value = "Fecha del reporte: " & fecha
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With

            filaIncial += 1
            filaIncial += 1

            Dim empiezanFilas As Integer = filaIncial
            With ws.Cell(filaIncial, 1)
                .Value = "Código de Alumno"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#C5D9F1")
                .Style.Alignment.WrapText = True

            End With
            With ws.Cell(filaIncial, 2)
                .Value = "Nombre Completo del Alumno"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#C5D9F1")
                .Style.Alignment.WrapText = True
            End With
            With ws.Cell(filaIncial, 3)
                .Value = "Año"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#C5D9F1")
                .Style.Alignment.WrapText = True
            End With
            With ws.Cell(filaIncial, 4)
                .Value = "Grado"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#C5D9F1")
                .Style.Alignment.WrapText = True
            End With
            With ws.Cell(filaIncial, 5)
                .Value = "Situación Final"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#C5D9F1")
                .Style.Alignment.WrapText = True

            End With
            With ws.Cell(filaIncial, 6)
                .Value = "Debe Libros al Banco de Libros"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#C5D9F1")
                .Style.Alignment.WrapText = True
                .Style.Font.FontSize = 11
            End With

            With ws.Cell(filaIncial, 7)
                .Value = "Debe a Biblioteca"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#C5D9F1")
                .Style.Alignment.WrapText = True

            End With
            With ws.Cell(filaIncial, 8)
                .Value = "Pagó la Matrícula"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#C5D9F1")
                .Style.Alignment.WrapText = True

            End With

            With ws.Row(filaIncial)
                .Height = 31
            End With

            filaIncial += 1
            For Each filas As Data.DataRow In dtSituacionFinal.Rows

                With ws.Cell(filaIncial, 1)
                    .Value = filas("CodigoAlumno")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Font.FontSize = 9
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                With ws.Cell(filaIncial, 2)
                    .Value = filas("NombreAlumno")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    .Style.Font.FontSize = 9
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                With ws.Cell(filaIncial, 3)
                    .Value = filas("anio")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Font.FontSize = 9
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With
                With ws.Cell(filaIncial, 4)
                    .Value = filas("grado")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Font.FontSize = 9
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    'CodigoAlumno	NombreAlumno	anio	grado	SituacionFinal	DebeLibro	DebeBiblioteca	PagoMatricula
                    '20030002	ALMEIDA GALLEGOS Joaquin	2013	Tenth	Postegación de Evaluación	No 	No 	Si
                End With
                With ws.Cell(filaIncial, 5)
                    .Value = filas("SituacionFinal")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Font.FontSize = 1
                    .Style.Font.FontSize = 9
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.WrapText = True
                End With
                With ws.Cell(filaIncial, 6)
                    .Value = filas("DebeLibro")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Font.FontSize = 9
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                End With
                With ws.Cell(filaIncial, 7)
                    .Value = filas("DebeBiblioteca")
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Font.FontSize = 9
                End With
                With ws.Cell(filaIncial, 8)
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Value = filas("PagoMatricula")
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Font.FontSize = 9
                End With
                filaIncial += 1
            Next

            With ws.Range(ws.Cell(empiezanFilas, 1), ws.Cell(IIf(filaIncial <> 0, filaIncial - 1, empiezanFilas), 8))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With




            ws.Column(1).Width = 18
            ws.Column(2).Width = 41
            ws.Column(3).Width = 8
            ws.Column(4).Width = 12
            ws.Column(5).Width = 16
            ws.Column(6).Width = 15
            ws.Column(7).Width = 12
            ws.Column(8).Width = 10








            ws.PageSetup.AdjustTo(60)
            ws.PageSetup.Margins.Bottom = 0.75 '1.9
            ws.PageSetup.Margins.Left = 0.7 '0.6
            ws.PageSetup.Margins.Right = 0.7 '0.6
            ws.PageSetup.Margins.Header = 0.3 '0.8
            ws.PageSetup.Margins.Footer = 0.3 '0.8
            ws.PageSetup.PagesWide = 1


            'Dim wsFreeze = workbook.Worksheets.Add("Freeze View")


            'ws.SheetView.FreezeRows(3)

            workbook.Save()

            Dim downloadBytes1 As Byte()
            downloadBytes1 = File.ReadAllBytes(rutaREpositorioTemporales)


            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteHouse.xlsx" + "; size=" + downloadBytes1.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes1)
            Response.End()

        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class