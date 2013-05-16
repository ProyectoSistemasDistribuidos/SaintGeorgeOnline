Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessEntities.ModuloAsistenciaAlumnos
Imports SaintGeorgeOnline_DataAccess.ModuloAsistenciaAlumnos
Imports SaintGeorgeOnline_BusinessLogic.ModuloAsistenciaAlumnos
Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing

Imports System.Security.Cryptography
Imports System.Web.Services
Imports System.Configuration.ConfigurationManager
'ok
Partial Class Modulo_AsistenciaAlumno_AsistenciaDiaria
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Asistencia Diaria")

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                tbFechaAsistencia.Text = Now.Date
                cargarCombos()
                listarBimestreXAño()
                'listar()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try

    End Sub

    Protected Sub btnAddReason_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            pnModalAgregarMotivo.Show()
            pnModalAgregarRegistro.Show()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try

    End Sub
    Protected Sub ddlBuscarSalon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If ddlBuscarSalon.SelectedValue > 0 Then
        listar()
        ddlBuscarAnioAcademico.Enabled = False
        tbFechaAsistencia.Enabled = False
        imageFrIni1.Enabled = False
        'Else
        '    MostrarSexyAlertBox("Debe seleccionar un item válido de salón.", "Alert")
        'End If
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""

            Grabar()

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub btnGrabarMotivo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""

            If validarMensajeMotivo(usp_mensaje) Then
                GrabarMotivo()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
                pnModalAgregarRegistro.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub btnAgregarMultiple_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""

            If validarAgregarMultiple(usp_mensaje) Then
                cargarDatosMultiple()
                pnModalAgregarRegistro.Show()
                'ActivarPanel(3)
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ModalPopupExtender1.Hide()

    End Sub

    Protected Sub btnCerrarAgregarMotivo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerrarAgregarMotivo.Click
        pnModalAgregarRegistro.Show()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()

    End Sub

    Protected Sub btnGrabarJustificacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validarMensajeJustificacion(usp_mensaje) Then
                GrabarJustificacion()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
                pnModalAgregarRegistro.Show()
            End If

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelarJustificacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'pnModalAgregarRegistro.Dispose()
    End Sub

    Protected Sub btnCancelarMotivo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        pnModalAgregarMotivo.Dispose()
        pnModalAgregarRegistro.Show()
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarCombos()
        cargarComboAniosAcademicos()
        cargarComboAulas()
        cargarMotivo()
        cargarMedioUso()
        cargarComboBimestres()
    End Sub

    Private Sub cargarComboBimestres()

        Dim str_Descripcion As String = ""
        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBimestre, ds_Lista, "Codigo", "Descripcion", False, False)

        ''
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     15/03/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarMotivo()

        Dim obj_BL_Motivo As New bl_Motivo
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Motivo.FUN_LIS_Motivo(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        rbMotivo.DataSource = ds_Lista
        rbMotivo.DataValueField = "Codigo"
        rbMotivo.DataTextField = "Descripcion"
        rbMotivo.DataBind()
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     15/03/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarMedioUso()

        Dim obj_BL_MedioUso As New bl_MedioUso
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_MedioUso.FUN_LIS_MedioUso(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        rbMedioUsado.DataSource = ds_Lista
        rbMedioUsado.DataValueField = "Codigo"
        rbMedioUsado.DataTextField = "Descripcion"
        rbMedioUsado.DataBind()
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Anos Academicos disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAniosAcademicos()

        Dim obj_BL_AnioAcademico As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AnioAcademico.FUN_LIS_AniosAcademicos("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
        ddlBuscarAnioAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar

    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Aulas disponibles en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarSalon, ds_Lista, "Codigo", "DescAulaCompuesta", False, True)

    End Sub

    Private Sub cargarDatosMultiple()
        Dim dt As DataTable
        dt = ViewState("ListadoConducta")

        If ViewState("ListadoConducta") IsNot Nothing Then
            'GV_Alumnos.DataSource = dt
            'GV_Alumnos.DataBind()
        End If
        'cargarComboCriteriosConductaM()
    End Sub

    ''' <summary>
    ''' Setear permisos de acciones sobre el formulario según la configuración del usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        'Me.Master.RegistrarAccesoPagina(cod_Modulo, cod_Opcion)
    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Exporte el listado de la información filtrada en los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Exportar()
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim NombreArchivo As String = ""
        Dim RutaMadre As String = ""
        Dim downloadBytes As Byte()

        If rbExportar.SelectedValue = 1 Then ' ATTENDANCE

            Dim obj_BL_Asistencia As New bl_Asistencia

            Dim ds As DataSet
            ds = obj_BL_Asistencia.FUN_REP_AsistenciaXBimestreMeses(ddlBuscarAnioAcademico.SelectedValue, _
                                                                 ddlBuscarSalon.SelectedValue, ddlBimestre.SelectedValue, _
                                                                  0, int_CodigoUsuario, _
                                                                  int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            NombreArchivo = Exportacion.ExportarReporteAsistenciaXBimestreMeses(ds, "Attendance")
        ElseIf rbExportar.SelectedValue = 2 Then ' Control de Asistencia

            Dim obj_BL_Asistencia As New bl_Asistencia
            Dim ds As DataSet
            ds = obj_BL_Asistencia.FUN_REP_ControlXBimestre(Me.Master.Obtener_CodigoPeriodoEscolar, _
                                                                 ddlBuscarSalon.SelectedValue, ddlBimestre.SelectedValue, _
                                                                 0, int_CodigoUsuario, _
                                                                  int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            NombreArchivo = Exportacion.ExportarReporteControlAsistenciaXBimestre(ds, "Control_Asistencia")
        ElseIf rbExportar.SelectedValue = 3 Then ' Consolidado de Incidencias

            Dim obj_BL_Asistencia As New bl_Asistencia

            Dim ds As DataSet
            ds = obj_BL_Asistencia.FUN_REP_IncidenciasAsistencia(Me.Master.Obtener_CodigoPeriodoEscolar, _
                                                                 ddlBuscarSalon.SelectedValue, ddlBimestre.SelectedValue, _
                                                                 0, int_CodigoUsuario, _
                                                                  int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            NombreArchivo = Exportacion.ExportarReporteIncidenciasAsistencia(ds, "Incidencias de Asistencia")

        End If

        NombreArchivo = NombreArchivo & ".xls"
        RutaMadre = Server.MapPath(".")
        RutaMadre = RutaMadre.Replace("\Modulo_AsistenciaAlumno", "\Reportes\")
        downloadBytes = File.ReadAllBytes(RutaMadre & "\" & NombreArchivo)
        Response.Charset = ""
        Response.ContentType = "binary/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()
        Response.BinaryWrite(downloadBytes)
        Response.End()
    End Sub
    'Private Sub Exportar()

    '    Dim rutamadre As String = ""
    '    Dim downloadBytes As Byte()
    '    Dim stream As Stream
    '    Dim writer As StreamWriter
    '    Dim contenido_exportar As String = ""
    '    Dim NombreArchivo As String = ""

    '    Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
    '    Dim dt As DataTable = New DataTable("ListaExportar")

    '    dt = Datos.agregarColumna(dt, "N°", "String")
    '    dt = Datos.agregarColumna(dt, "Student", "String")
    '    dt = Datos.agregarColumna(dt, "Evento", "String")
    '    dt = Datos.agregarColumna(dt, "TL", "String")
    '    dt = Datos.agregarColumna(dt, "TA", "String")
    '    dt = Datos.agregarColumna(dt, "TLJ", "String")
    '    dt = Datos.agregarColumna(dt, "TAJ", "String")

    '    Dim cont As Integer = 1
    '    Dim auxDR As DataRow

    '    For Each dr As DataRow In ds_Lista.Tables(0).Rows
    '        auxDR = dt.NewRow
    '        auxDR.Item("N°") = cont
    '        auxDR.Item("Student") = dr.Item("NombreAlumno").ToString
    '        auxDR.Item("Evento") = dr.Item("EventoAsistencia").ToString
    '        auxDR.Item("TL") = dr.Item("TotalTardanzas").ToString
    '        auxDR.Item("TA") = dr.Item("TotalFaltas").ToString
    '        auxDR.Item("TLJ") = dr.Item("TotalTardanzasJustificadas").ToString
    '        auxDR.Item("TAJ") = dr.Item("TotalFaltasJustificadas").ToString
    '        dt.Rows.Add(auxDR)
    '        cont += 1
    '    Next

    '    If rbExportar.SelectedValue = 0 Then 'WORD
    '        Dim reporte_html As String = ""
    '        Dim Arreglo_Datos As String()

    '        Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Asistencia Diaria")
    '        reporte_html = Arreglo_Datos(0)
    '        NombreArchivo = Arreglo_Datos(1)
    '        NombreArchivo = NombreArchivo & ".doc"

    '        rutamadre = Server.MapPath(".")
    '        rutamadre = rutamadre.Replace("\Modulo_AsistenciaAlumno", "\Reportes\")

    '        stream = File.OpenWrite(rutamadre & "\" & NombreArchivo)
    '        writer = New StreamWriter(stream, System.Text.Encoding.UTF8)

    '        Using (writer)
    '            writer.Write(reporte_html)
    '            writer.Flush()
    '        End Using

    '        writer.Close()
    '        downloadBytes = File.ReadAllBytes(rutamadre & "\" & NombreArchivo)

    '        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
    '        response.Clear()
    '        response.AddHeader("Content-Type", "binary/octet-stream")
    '        response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
    '        response.Flush()
    '        response.BinaryWrite(downloadBytes)
    '        response.Flush()
    '        response.End()

    '    ElseIf rbExportar.SelectedValue = 1 Then 'EXCEL

    '        NombreArchivo = Exportacion.ExportarReporte(dt, "Asistencia Diaria")
    '        NombreArchivo = NombreArchivo & ".xls"
    '        rutamadre = Server.MapPath(".")
    '        rutamadre = rutamadre.Replace("\Modulo_AsistenciaAlumno", "\Reportes\")

    '        downloadBytes = File.ReadAllBytes(rutamadre & "\" & NombreArchivo)

    '        Response.AddHeader("content-disposition", "attachment;filename=test1.xls")
    '        Response.Charset = ""
    '        Response.ContentType = "binary/octet-stream"
    '        Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
    '        Response.Flush()
    '        Response.BinaryWrite(downloadBytes)
    '        Response.End()

    '    ElseIf rbExportar.SelectedValue = 2 Then 'PDF
    '        Dim m As System.IO.MemoryStream = New System.IO.MemoryStream

    '        m = Exportacion.ExportarReporte_Pdf(dt, "Asistencia Diaria")

    '        'Exportar
    '        Response.Clear()
    '        Response.ContentType = "application/pdf"
    '        Response.AddHeader("content-disposition", "attachment;filename=Reporte.pdf")
    '        Response.Cache.SetCacheability(HttpCacheability.NoCache)

    '        Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length)
    '        Response.OutputStream.Flush()
    '        Response.OutputStream.Close()
    '        Response.End()
    '    ElseIf rbExportar.SelectedValue = 3 Then 'HTML
    '        Dim reporte_html As String = ""
    '        Dim Arreglo_Datos As String()

    '        Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Asistencia Diaria")
    '        reporte_html = Arreglo_Datos(0)

    '        Session("Exportaciones_RepHtml") = reporte_html
    '        ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
    '    End If

    'End Sub

    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarMensajeJustificacion(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If rbMedioUsado.SelectedValue = Nothing Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Medium used")
            result = False
        End If

        If rbMotivo.SelectedValue = Nothing Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Reason")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    06/06/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarMensajeMotivo(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""


        If tbDescMotivo.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Description")
            result = False
        End If

        If Validacion.ValidarCamposIngreso(tbDescMotivo) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Description")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Function validarAgregarMultiple(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        'If ddlBuscarBimestre.SelectedValue = 0 Then
        '    str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Bimestre")
        '    result = False
        'End If

        If ddlBuscarSalon.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Salón")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub listarBimestreXAño()
        Dim obj_BL_AsistenciaAlumnos As New bl_AsistenciaAlumnos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim dt_Plantilla As New DataTable
        Dim ds_ListaBi As DataSet

        ds_ListaBi = obj_BL_AsistenciaAlumnos.FUN_LIS_BimestreXAño(ddlBuscarAnioAcademico.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        lblFecInicio1.Text = ds_ListaBi.Tables(0).Rows(0).Item("FechaInicio")
        lblFecFin1.Text = ds_ListaBi.Tables(0).Rows(0).Item("FechaFin")
        lblFecInicio2.Text = ds_ListaBi.Tables(0).Rows(1).Item("FechaInicio")
        lblFecFin2.Text = ds_ListaBi.Tables(0).Rows(1).Item("FechaFin")
        lblFecInicio3.Text = ds_ListaBi.Tables(0).Rows(2).Item("FechaInicio")
        lblFecFin3.Text = ds_ListaBi.Tables(0).Rows(2).Item("FechaFin")
        lblFecInicio4.Text = ds_ListaBi.Tables(0).Rows(3).Item("FechaInicio")
        lblFecFin4.Text = ds_ListaBi.Tables(0).Rows(3).Item("FechaFin")

    End Sub
    ''' <summary>
    ''' Lista los datos de la busqueda .
    ''' </summary>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

        tbAsistentes.Text = ds_Lista.Tables(1).Rows(0).Item("cant_Asistentes")
        tbTardanzas.Text = ds_Lista.Tables(1).Rows(0).Item("cant_Tardanzas")
        tbfaltas.Text = ds_Lista.Tables(1).Rows(0).Item("cant_Faltas")
        tbFaltasJust.Text = ds_Lista.Tables(1).Rows(0).Item("cant_FaltasJustificadas")
        'tbFaltasInjust.Text = ds_Lista.Tables(1).Rows(0).Item("cant_Faltas")
        'tbTardanzasInjust.Text = ds_Lista.Tables(1).Rows(0).Item("cant_Tardanzas")
        tbTardanzasJust.Text = ds_Lista.Tables(1).Rows(0).Item("cant_TardanzasJustificadas")

        If ds_Lista.Tables(0).Rows.Count = 0 Then
            btnExportar.Enabled = False
            rbExportar.Enabled = False
        Else
            btnExportar.Enabled = True
            rbExportar.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:              Fanny Salinas 
    ''' Fecha de Creación:    31/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet
        Dim dt_FechaAsistencia As Date = tbFechaAsistencia.Text.Trim
        Dim int_AnioAcademico As Integer = ddlBuscarAnioAcademico.SelectedValue()
        Dim int_Aula As Integer = ddlBuscarSalon.SelectedValue()
        Dim int_Estado As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_AsistenciaAlumnos As New bl_AsistenciaAlumnos
            ds_Lista = obj_BL_AsistenciaAlumnos.FUN_LIS_AsistenciaAlumnos(int_AnioAcademico, int_Aula, dt_FechaAsistencia, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_AsistenciaAlumnos As New bl_AsistenciaAlumnos
                ds_Lista = obj_BL_AsistenciaAlumnos.FUN_LIS_AsistenciaAlumnos(int_AnioAcademico, int_Aula, dt_FechaAsistencia, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Graba o Actualiza el registro indicado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_AsistenciaAlumnos As New be_AsistenciaAlumnos
        Dim obj_BL_AsistenciaAlumnos As New bl_AsistenciaAlumnos
        'Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_EventoAsistencia As Integer = 0
        obj_BE_AsistenciaAlumnos.FechaAsistencia = tbFechaAsistencia.Text
        obj_BE_AsistenciaAlumnos.CodigoAula = ddlBuscarSalon.SelectedValue()

        If GridView1.Rows.Count > 0 Then

            For Each gvrM As GridViewRow In GridView1.Rows
                usp_valor = 0

                If CType(gvrM.FindControl("rdb_LisAsistentes"), RadioButton).Checked = True Then
                    int_EventoAsistencia = 1
                ElseIf CType(gvrM.FindControl("rdb_LisTardanzas"), RadioButton).Checked = True Then
                    int_EventoAsistencia = 3
                ElseIf CType(gvrM.FindControl("rdb_LisFaltas"), RadioButton).Checked = True Then
                    int_EventoAsistencia = 2
                ElseIf CType(gvrM.FindControl("lblTardanzasJust"), Label).Text = "Si" Then
                    int_EventoAsistencia = 5
                ElseIf CType(gvrM.FindControl("lblFaltasJust"), Label).Text = "Si" Then
                    int_EventoAsistencia = 4
                End If

                obj_BE_AsistenciaAlumnos.CodigoRegistroAsistencia = CType(gvrM.FindControl("lblCodigoRegistroAsistencia"), Label).Text.Trim
                obj_BE_AsistenciaAlumnos.CodigoAlumno = CType(gvrM.FindControl("lblCodigoAlumno"), Label).Text.Trim
                obj_BE_AsistenciaAlumnos.CodigoEventoAsistencia = int_EventoAsistencia
                obj_BE_AsistenciaAlumnos.ObservacionTutor = ""

                usp_valor = obj_BL_AsistenciaAlumnos.FUN_INS_AsistenciaAlumnos(obj_BE_AsistenciaAlumnos, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

            Next

            If usp_valor > 0 Then
                MostrarSexyAlertBox(usp_mensaje, "Info")
                'btnCancelar_Click()
                listar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If

        End If
    End Sub

    ''' <summary>
    ''' Graba o Actualiza el registro indicado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     16/03/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarJustificacion()

        Dim obj_BE_AsistenciaAlumnos As New be_AsistenciaAlumnos
        Dim obj_BL_AsistenciaAlumnos As New bl_AsistenciaAlumnos
        'Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_EventoAsistencia As Integer = 0
        obj_BE_AsistenciaAlumnos.FechaJustificacion = tbFechaJustificacionTA.Text
        obj_BE_AsistenciaAlumnos.CodigoAula = ddlBuscarSalon.SelectedValue()

        obj_BE_AsistenciaAlumnos.CodigoRegistroAsistencia = lblCodigoRegistroAsistenciaJust.Text
        obj_BE_AsistenciaAlumnos.CodigoAlumno = lblCodigoAlumno.Text
        obj_BE_AsistenciaAlumnos.CodigoEventoAsistencia = rbTipo.SelectedValue
        obj_BE_AsistenciaAlumnos.ObservacionTutor = tbObservacionJust.Text
        obj_BE_AsistenciaAlumnos.CodigoMotivo = rbMotivo.SelectedValue
        obj_BE_AsistenciaAlumnos.CodigoMedioUso = rbMedioUsado.SelectedValue

        usp_valor = obj_BL_AsistenciaAlumnos.FUN_INS_AsistenciaJustificacion(obj_BE_AsistenciaAlumnos, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            'btnCancelar_Click()
            listar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Graba o Actualiza el registro indicado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     06/06/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GrabarMotivo()

        Dim obj_BE_Motivo As New be_Motivo
        Dim obj_BL_Motivo As New bl_Motivo
        'Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_EventoAsistencia As Integer = 0
        obj_BE_Motivo.Descripcion = tbDescMotivo.Text

        usp_valor = obj_BL_Motivo.FUN_INS_Motivo(obj_BE_Motivo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            'btnCancelar_Click()
            cargarMotivo()
            pnModalAgregarRegistro.Show()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
            pnModalAgregarMotivo.Show()
            pnModalAgregarRegistro.Show()
        End If

    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    Protected Sub rdb_LisAsistentes_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As RadioButton = CType(sender, RadioButton)
        Dim grv As GridViewRow = CType(chk.NamingContainer, GridViewRow)
        Dim chk_Tardanza As RadioButton = CType(grv.FindControl("rdb_LisTardanzas"), RadioButton)
        Dim chk_Faltas As RadioButton = CType(grv.FindControl("rdb_LisFaltas"), RadioButton)
        chk.Checked = True
        chk.BackColor = Color.Green
        chk_Tardanza.BackColor = Color.White
        chk_Faltas.BackColor = Color.White

    End Sub

    Protected Sub rdb_LisTardanzas_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As RadioButton = CType(sender, RadioButton)
        Dim grv As GridViewRow = CType(chk.NamingContainer, GridViewRow)
        Dim chk_Asistencias As RadioButton = CType(grv.FindControl("rdb_LisAsistentes"), RadioButton)
        Dim chk_Faltas As RadioButton = CType(grv.FindControl("rdb_LisFaltas"), RadioButton)

        chk.Checked = True
        chk.BackColor = Color.Yellow
        chk_Asistencias.BackColor = Color.White
        chk_Faltas.BackColor = Color.White

    End Sub

    Protected Sub rdb_LisFaltas_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As RadioButton = CType(sender, RadioButton)
        Dim grv As GridViewRow = CType(chk.NamingContainer, GridViewRow)
        Dim chk_Asistencias As RadioButton = CType(grv.FindControl("rdb_LisAsistentes"), RadioButton)
        Dim chk_Tardanza As RadioButton = CType(grv.FindControl("rdb_LisTardanzas"), RadioButton)

        chk.Checked = True
        chk.BackColor = Color.Red
        chk_Asistencias.BackColor = Color.White
        chk_Tardanza.BackColor = Color.White

    End Sub


    ''' <summary>
    ''' Limpia los filtros de búsqueda del formulario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     13/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks> 
    Private Sub limpiarFiltros()

        ddlBuscarAnioAcademico.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
        tbFechaAsistencia.Text = Now.Date
        ddlBuscarSalon.SelectedValue = 0
        ddlBuscarAnioAcademico.Focus()
        ddlBuscarAnioAcademico.Enabled = True
        tbFechaAsistencia.Enabled = True
        imageFrIni1.Enabled = True
        listar()
    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim btnVerFoto As HtmlAnchor = e.Row.FindControl("btnVerPortada")
        Dim rdbAsistentes As RadioButton = e.Row.FindControl("rdb_LisAsistentes")
        Dim rdbTardanzas As RadioButton = e.Row.FindControl("rdb_LisTardanzas")
        Dim rdbFaltas As RadioButton = e.Row.FindControl("rdb_LisFaltas")
        Dim lblTardanzasJust As Label = e.Row.FindControl("lblTardanzasJust")
        Dim lblFaltasJust As Label = e.Row.FindControl("lblFaltasJust")
        Dim btnJustificacion As ImageButton = e.Row.FindControl("btnJustificacion")
        'Dim checkTardanzas As CheckBox = e.Row.FindControl("check_LisTardanzas")
        'Dim checkFaltas As CheckBox = e.Row.FindControl("check_LisFaltas")

        If e.Row.RowType = DataControlRowType.DataRow Then
            btnVerFoto.Attributes.Add("rel", "sexylightbox")

            btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & e.Row.DataItem("RutaFoto")

            If e.Row.DataItem("CodigoEventoAsistencia") = 1 Then
                'btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                'btnEliminar.Visible = True
                rdbAsistentes.Checked = True
                rdbAsistentes.BackColor = Color.Green
                lblTardanzasJust.Text = ""
                lblFaltasJust.Text = ""

                rdbTardanzas.BackColor = Color.White
                rdbFaltas.BackColor = Color.White
            ElseIf e.Row.DataItem("CodigoEventoAsistencia") = 3 Then
                rdbTardanzas.Checked = True
                rdbTardanzas.BackColor = Color.Yellow
                lblTardanzasJust.Text = "No"
                lblFaltasJust.Text = ""
                lblTardanzasJust.BackColor = Color.LightCoral

                rdbAsistentes.BackColor = Color.White
                rdbFaltas.BackColor = Color.White
            ElseIf e.Row.DataItem("CodigoEventoAsistencia") = 2 Then
                rdbFaltas.Checked = True
                rdbFaltas.BackColor = Color.Red
                lblTardanzasJust.Text = ""
                lblFaltasJust.Text = "No"
                lblFaltasJust.BackColor = Color.LightCoral

                rdbTardanzas.BackColor = Color.White
                rdbAsistentes.BackColor = Color.White

            ElseIf e.Row.DataItem("CodigoEventoAsistencia") = 4 Then 'faltas Justificada
                lblFaltasJust.Text = "Si"
                lblTardanzasJust.Text = ""
                lblFaltasJust.BackColor = Color.LightGreen

                rdbFaltas.BackColor = Color.White
                rdbTardanzas.BackColor = Color.White
                rdbAsistentes.BackColor = Color.White
            ElseIf e.Row.DataItem("CodigoEventoAsistencia") = 5 Then 'tardanza Justificada
                lblTardanzasJust.Text = "Si"
                lblFaltasJust.Text = ""
                lblTardanzasJust.BackColor = Color.LightGreen

                rdbFaltas.BackColor = Color.White
                rdbTardanzas.BackColor = Color.White
                rdbAsistentes.BackColor = Color.White
            End If



            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Justificacion" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Justificacion" Then
                    lblAnio.Text = ddlBuscarAnioAcademico.SelectedItem.ToString
                    'lblFecha.Text = tbFechaAsistencia.Text
                    tbFechaJustificacionTA.Text = tbFechaAsistencia.Text
                    lblSalon.Text = ddlBuscarSalon.SelectedItem.ToString
                    lblNombreCompleto.Text = CType(row.FindControl("lblNombreAlumno"), Label).Text
                    lblCodigoAlumno.Text = codigo
                    lblCodigoRegistroAsistenciaJust.Text = CType(row.FindControl("lblCodigoRegistroAsistencia"), Label).Text

                    Dim int_CodigoEvento As Integer = 0
                    int_CodigoEvento = CInt(CType(row.FindControl("lblCodigoEventoAsistenciaGV"), Label).Text)

                    If int_CodigoEvento = 4 Or int_CodigoEvento = 5 Then
                        rbTipo.SelectedValue = CInt(CType(row.FindControl("lblCodigoEventoAsistenciaGV"), Label).Text)
                        rbMedioUsado.SelectedValue = CInt(CType(row.FindControl("lblCodigoMedioUsoGV"), Label).Text)
                        rbMotivo.SelectedValue = CInt(CType(row.FindControl("lblCodigoMotivoGV"), Label).Text)
                        tbObservacionJust.Text = CType(row.FindControl("lblCodigoObservacionGV"), Label).Text
                    Else
                        rbTipo.SelectedValue = 5
                        rbMedioUsado.SelectedValue = Nothing
                        rbMotivo.SelectedValue = Nothing
                        tbObservacionJust.Text = ""
                    End If

                    pnModalAgregarRegistro.Show()
                End If

            End If

        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

#End Region

End Class
