Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Modulo de Mantenimiento de Relacion Anios AcademicosAulas
''' </summary>
''' <remarks>
''' Código del Modulo:   
''' Código de la Opción: 
''' </remarks>
Partial Class Modulo_Colegio_AsignacionSemanasPorBimestres
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Asignación de Semanas por Bimestre")

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                ViewState("SortExpression") = "DescAnioAcademico"
                ViewState("Direccion") = "ASC"
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")

                cargarComboBuscarAnioAcademico()
                cargarComboBuscarBimestre()
                'cargarComboBuscarSemanaAcademico()

                listar()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Try
            Dim usp_mensaje As String = ""
            hd_Codigo.Value = 0
            'tbAnioAcademico.Text = ddlBuscarAnioAcademico.SelectedItem.ToString
            'hiddenCodigoAnioAcademico.Value = ddlBuscarAnioAcademico.SelectedValue
            VerRegistro("Nuevo Registro")
            limpiarCampos()

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btnCancelar_Click()

        miTab1.Enabled = True
        TabContainer1.ActiveTabIndex = 0
        'hd_Codigo.Value = 0
        'pnModalAgregarRegistro.Hide()

    End Sub

    Protected Sub btnCerrarModal_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'hd_Codigo.Value = 0
        'pnModalAgregarRegistro.Hide()

    End Sub


    ''' <summary>
    ''' Habilita el TabPanel del formulario
    ''' </summary>
    ''' <param name="str_Modo">Nombre del label del tabPanel</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        lblTitulo.Text = str_Modo
        ddlAnioAcademico.Focus()
        cargarComboAnioAcademico()
        cargarComboBimestre()
        pnModalAgregarRegistro.Show()
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then
                Grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
                pnModalAgregarRegistro.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportar.Click
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        limpiarFiltros()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        Try
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlBimestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        cargarGrillaSemanas()
        pnModalAgregarRegistro.Show()
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(cod_Modulo, cod_Opcion)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    '''  <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    '''     Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Limpia los filtros de busqueda.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        ddlBuscarAnioAcademico.SelectedIndex = 0
        ddlBuscarBimestre.SelectedValue = 0
        ddlBuscarAnioAcademico.Focus()

    End Sub

    ''' <summary>
    ''' Exporta los datos del gridView en formato WORD,EXCEL,HTML,PDF,HTML.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Exportar()

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim stream As Stream
        Dim writer As StreamWriter
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        Dim dt As DataTable = New DataTable("ListaExportar")

        dt = Datos.agregarColumna(dt, "N°", "String")
        dt = Datos.agregarColumna(dt, "Año Académico", "String")
        dt = Datos.agregarColumna(dt, "Bimestre", "String")
        dt = Datos.agregarColumna(dt, "Semana Académica", "String")
        dt = Datos.agregarColumna(dt, "Fecha de Inicio", "String")
        dt = Datos.agregarColumna(dt, "Fecha de Fin", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Año Académico") = dr.Item("DescAnioAcademico").ToString
            auxDR.Item("Bimestre") = dr.Item("DescBimestre").ToString
            auxDR.Item("Semana Académica") = dr.Item("DescSemanaAcademica").ToString
            auxDR.Item("Fecha de Inicio") = dr.Item("FechaInicio").ToString
            auxDR.Item("Fecha de Fin") = dr.Item("FechaFin").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Semanas por Bimestres")
            reporte_html = Arreglo_Datos(0)
            NombreArchivo = Arreglo_Datos(1)
            NombreArchivo = NombreArchivo & ".doc"

            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Colegio", "\Reportes\")


            stream = File.OpenWrite(rutamadre & "\" & NombreArchivo)
            writer = New StreamWriter(stream, System.Text.Encoding.UTF8)

            Using (writer)
                writer.Write(reporte_html)
                writer.Flush()
            End Using

            writer.Close()
            downloadBytes = File.ReadAllBytes(rutamadre & "\" & NombreArchivo)

            Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            response.Clear()
            response.AddHeader("Content-Type", "binary/octet-stream")
            response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            response.Flush()
            response.BinaryWrite(downloadBytes)
            response.Flush()
            response.End()

        ElseIf rbExportar.SelectedValue = 1 Then 'EXCEL

            NombreArchivo = Exportacion.ExportarReporte(dt, "Semanas por Bimestres")
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Colegio", "\Reportes\")

            downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

            Response.AddHeader("content-disposition", "attachment;filename=test1.xls")
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()

        ElseIf rbExportar.SelectedValue = 2 Then 'PDF
            Dim m As System.IO.MemoryStream = New System.IO.MemoryStream

            m = Exportacion.ExportarReporte_Pdf(dt, "Semanas por Bimestres")

            'Exportar
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.pdf")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.OutputStream.Close()
            Response.End()

        ElseIf rbExportar.SelectedValue = 3 Then 'HTML
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Semanas por Bimestres")
            reporte_html = Arreglo_Datos(0)
            Session("Exportaciones_RepHtml") = reporte_html
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
        End If

    End Sub

    ''' <summary>
    ''' Carga la información de los Años Academicos activos
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBuscarAnioAcademico()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    ''' <summary>
    ''' Carga la información de las monedas
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboBuscarBimestre()
        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres("", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarBimestre, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    '''' <summary>
    '''' Carga la información de las Semanas Academicas activos
    '''' </summary>
    '''' <remarks>
    '''' Creador:               Fanny Salinas
    '''' Fecha de Creación:     26/07/2011
    '''' Modificado por:        _____________
    '''' Fecha de modificación: _____________ 
    '''' </remarks>
    'Private Sub cargarComboBuscarSemanaAcademico()
    '    Dim obj_AniosAcademicos As New bl_
    '    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
    '    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

    '    Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
    '    Controles.llenarCombo(ddlBuscarAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
    'End Sub


    Private Sub cargarComboAnioAcademico()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAnioAcademico, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    Private Sub cargarComboBimestre()
        Dim obj_BL_Bimestres As New bl_Bimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Bimestres.FUN_LIS_Bimestres("", int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBimestre, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub


    ''' <summary>
    ''' Graba los datos del formulario 
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_AsignacionSemanasPorBimestres As New be_AsignacionSemanasPorBimestres
        Dim obj_BL_AsignacionSemanasPorBimestres As New bl_AsignacionSemanasPorBimestres
        Dim bool_Grabar As Boolean = False
        Dim int_CodigoAsignacionSemana As Integer = 0
        Dim usp_mensaje As String = ""
        Dim usp_mensajeDetalle As String = ""
        Dim usp_mensajeFinal As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_AsignacionSemanasPorBimestres.CodigoAnioAcademico = ddlAnioAcademico.SelectedValue
        obj_BE_AsignacionSemanasPorBimestres.CodigoBimestre = ddlBimestre.SelectedValue

        Dim dt_NoRegistrados As New DataTable("ListaNoRegistrados")
        dt_NoRegistrados = Datos.agregarColumna(dt_NoRegistrados, "Idx", "Integer")
        dt_NoRegistrados = Datos.agregarColumna(dt_NoRegistrados, "Semana", "String")
        dt_NoRegistrados = Datos.agregarColumna(dt_NoRegistrados, "FechaInicio", "String")
        dt_NoRegistrados = Datos.agregarColumna(dt_NoRegistrados, "FechaFin", "String")

        Dim cont_NoRegistrados As Integer = 1

        For Each gvrS As GridViewRow In GVListaSemanas.Rows
            int_CodigoAsignacionSemana = gvrS.Cells(3).Text
            obj_BE_AsignacionSemanasPorBimestres.CodigoSemanaAcademica = gvrS.Cells(2).Text
           
            If int_CodigoAsignacionSemana > 0 Then 'existe el registro
                obj_BE_AsignacionSemanasPorBimestres.CodigoAsignacionSemana = int_CodigoAsignacionSemana
                obj_BE_AsignacionSemanasPorBimestres.FechaInicio = CType(gvrS.FindControl("tbFechaInicio"), TextBox).Text.Trim
                obj_BE_AsignacionSemanasPorBimestres.FechaFinal = CType(gvrS.FindControl("tbFechaFin"), TextBox).Text.Trim
                obj_BE_AsignacionSemanasPorBimestres.Orden = CType(gvrS.FindControl("tbOrden"), TextBox).Text.Trim

                If CType(gvrS.FindControl("chkSeleccionar"), CheckBox).Checked Then 'Si esta seleccionado el check
                    usp_valor = obj_BL_AsignacionSemanasPorBimestres.FUN_UPD_AsignacionSemanasPorBimestres(obj_BE_AsignacionSemanasPorBimestres, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                Else 'No esta seleccionado el check
                    usp_valor = obj_BL_AsignacionSemanasPorBimestres.FUN_DEL_AsignacionSemanasPorBimestres(int_CodigoAsignacionSemana, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                End If
            Else 'No existe el registro
                If CType(gvrS.FindControl("chkSeleccionar"), CheckBox).Checked Then 'Si esta seleccionado el check
                    obj_BE_AsignacionSemanasPorBimestres.FechaInicio = CType(gvrS.FindControl("tbFechaInicio"), TextBox).Text.Trim
                    obj_BE_AsignacionSemanasPorBimestres.FechaFinal = CType(gvrS.FindControl("tbFechaFin"), TextBox).Text.Trim
                    obj_BE_AsignacionSemanasPorBimestres.Orden = CType(gvrS.FindControl("tbOrden"), TextBox).Text.Trim

                    usp_valor = obj_BL_AsignacionSemanasPorBimestres.FUN_INS_AsignacionSemanasPorBimestres(obj_BE_AsignacionSemanasPorBimestres, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                Else
                    usp_valor = 1
                End If
            End If

            If usp_valor = 0 Then
                Dim dr As DataRow
                dr = dt_NoRegistrados.NewRow
                dr.Item("Idx") = cont_NoRegistrados
                dr.Item("Semana") = gvrS.Cells(4).Text
                dr.Item("FechaInicio") = CType(gvrS.FindControl("tbFechaInicio"), TextBox).Text.Trim
                dr.Item("FechaFin") = CType(gvrS.FindControl("tbFechaFin"), TextBox).Text.Trim
                dt_NoRegistrados.Rows.Add(dr)
                cont_NoRegistrados += 1
            ElseIf usp_valor > 0 Then
                bool_Grabar = True
            End If

        Next

        If bool_Grabar = True Then
            usp_mensaje = "Operacion exitosa."
        Else
            usp_mensaje = "No se grabo ningún registro."
        End If

        If dt_NoRegistrados.Rows.Count > 0 Then
            usp_mensajeDetalle = "No se grabaron los siguientes registros :<br />"
            usp_mensajeDetalle += "<ol>"
            For Each dr As DataRow In dt_NoRegistrados.Rows
                usp_mensajeDetalle += "<li><em>" & dr.Item("Semana").ToString & "</em> - <em>" & dr.Item("FechaInicio").ToString & "</em> - <em>" & dr.Item("FechaFin").ToString & "</em>.</li>"
            Next
            usp_mensajeDetalle += "</ol>"

            usp_mensajeFinal = usp_mensaje & "<br />" & usp_mensajeDetalle
        Else
            usp_mensajeFinal = usp_mensaje
        End If

        MostrarSexyAlertBox(usp_mensajeFinal, "Info")

        listar()
        btnCancelar_Click()
        limpiarCampos()
        'If usp_valor > 0 Then
        '    MostrarSexyAlertBox(usp_mensaje, "Info")
        '    btnCancelar_Click()
        '    limpiarCampos()
        '    listar()
        'Else
        '    MostrarSexyAlertBox(usp_mensaje, "Alert")
        'End If

    End Sub


    ''' <summary>
    ''' Obtiene y setea los datos en el Formulario.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de diágnostico</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_AsignacionSemanasPorBimestres As New bl_AsignacionSemanasPorBimestres
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_AsignacionSemanasPorBimestres.FUN_GET_AsignacionSemanasPorBimestres(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 7)
        VerRegistro("Actualización Registro")

        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("Codigo").ToString)
        ddlAnioAcademico.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoAnioAcademico").ToString
        ddlBimestre.SelectedValue = ds_Lista.Tables(0).Rows(0).Item("CodigoBimestre").ToString
       

    End Sub

    ''' <summary>
    ''' Valida los campos del formulario antes de proceder a "Grabar"
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""
        Dim int_cantFI As Integer = 0
        Dim int_cantFF As Integer = 0
        Dim int_cantDifFech As Integer = 0
        Dim int_cantFechExiste As Integer = 0

        If ddlAnioAcademico.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "año académico")
            result = False
        End If

        If ddlBimestre.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Bimestre")
            result = False
        End If

        Dim idFila As Integer = 0

        For Each gvrS As GridViewRow In GVListaSemanas.Rows
            If CType(gvrS.FindControl("chkSeleccionar"), CheckBox).Checked Then 'Si esta seleccionado el check
                Dim tbFechaInicio As TextBox = CType(gvrS.FindControl("tbFechaInicio"), TextBox)
                Dim tbFechaFin As TextBox = CType(gvrS.FindControl("tbFechaFin"), TextBox)

                If IsDate(tbFechaInicio.Text.Trim) = False Then
                    int_cantFI = int_cantFI + 1
                End If
                If IsDate(tbFechaFin.Text.Trim) = False Then
                    int_cantFF = int_cantFF + 1
                End If
                If IsDate(tbFechaInicio.Text.Trim) And IsDate(tbFechaFin.Text.Trim) Then
                    If (CType(tbFechaInicio.Text, Date) > CType(tbFechaFin.Text, Date)) Then
                        int_cantDifFech = int_cantDifFech + 1
                    End If

                    If validarFechasExiste((CType(tbFechaInicio.Text, Date)), (CType(tbFechaFin.Text, Date)), idFila) Then
                        int_cantFechExiste = int_cantFechExiste + 1
                    End If

                End If

            End If
            idFila = idFila + 1
        Next

        If int_cantFI > 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Inicio")
            result = False
        End If

        If int_cantFF > 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 6, "Fecha Fin")
            result = False
        End If

        If int_cantDifFech > 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 7, "Fecha Inicio")
            result = False
        End If

        If int_cantFechExiste > 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 37, "Fecha")
            result = False
        End If

       

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Function validarFechasExiste(ByRef tbFechaInicio As Date, ByVal tbFechaFin As Date, ByVal idFila As Integer) As Boolean
        Dim bool_Existe As Boolean = False
        Dim int_cont As Integer = 0

        For Each gvrS As GridViewRow In GVListaSemanas.Rows
            If CType(gvrS.FindControl("chkSeleccionar"), CheckBox).Checked Then 'Si esta seleccionado el check
                Dim tbFechaI As TextBox = CType(gvrS.FindControl("tbFechaInicio"), TextBox)
                Dim tbFechaF As TextBox = CType(gvrS.FindControl("tbFechaFin"), TextBox)

                If idFila = int_cont Then
                Else
                    If ((CType(tbFechaI.Text, Date)) <= tbFechaInicio And tbFechaInicio <= (CType(tbFechaF.Text, Date))) Or _
                       ((CType(tbFechaI.Text, Date)) <= tbFechaFin And tbFechaFin <= (CType(tbFechaF.Text, Date))) Then
                        bool_Existe = True
                        Exit For
                    End If
                End If

            End If
            int_cont = int_cont + 1
            bool_Existe = False
        Next

        Return bool_Existe
    End Function

    'Private Function validarFechasNoPase7dias(ByRef tbFechaInicio As Date, ByVal tbFechaFin As Date, ByVal idFila As Integer) As Boolean
    '    Dim bool_Existe As Boolean = False
    '    Dim int_cont As Integer = 0

    '    If tbFechaFin > tbFechaInicio Then

    '    End If

    '    Return bool_Existe
    'End Function

    ''' <summary>
    ''' Limpia los campos de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()

        hd_Codigo.Value = 0
        ddlAnioAcademico.SelectedValue = 0
        ddlBimestre.SelectedValue = 0

        Dim dt As New DataTable("ListaSemanas")
        dt = Datos.agregarColumna(dt, "IdFila", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoSemanaAcademica", "Integer")
        dt = Datos.agregarColumna(dt, "Semana", "String")
        dt = Datos.agregarColumna(dt, "CodigoAsignacionSemana", "Integer")
        dt = Datos.agregarColumna(dt, "FechaInicio", "String")
        dt = Datos.agregarColumna(dt, "FechaFin", "String")

        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("IdFila") = 0
        dr.Item("CodigoSemanaAcademica") = 0
        dr.Item("Semana") = ""
        dr.Item("CodigoAsignacionSemana") = 0
        dr.Item("FechaInicio") = ""
        dr.Item("FechaFin") = ""
        dt.Rows.Add(dr)

        dt.Clear()

        GVListaSemanas.DataSource = dt
        GVListaSemanas.DataBind()

    End Sub

    ''' <summary>
    ''' Lista los datos      
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GV_AsignacionSemanaPorBimestre.DataSource = ds_Lista.Tables(0)
        GV_AsignacionSemanaPorBimestre.DataBind()

        If ds_Lista.Tables(0).Rows.Count = 0 Then
            btnExportar.Enabled = False
            rbExportar.Enabled = False
        Else
            btnExportar.Enabled = True
            rbExportar.Enabled = True

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        End If

    End Sub

    Private Sub cargarGrillaSemanas()
       
        Dim obj_BL_AsignacionSemanasPorBimestres As New bl_AsignacionSemanasPorBimestres
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_AsignacionSemanasPorBimestres.FUN_LIS_AsignacionSemanasPorBimestreAgregar(ddlBimestre.SelectedValue, ddlAnioAcademico.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        GVListaSemanas.DataSource = ds_Lista.Tables(0)
        GVListaSemanas.DataBind()

    End Sub
    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim int_CodigoAnioAcademico As Integer = ddlBuscarAnioAcademico.SelectedValue
        Dim int_CodigoBimestre As Integer = ddlBuscarBimestre.SelectedValue

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_AsignacionSemanasPorBimestres As New bl_AsignacionSemanasPorBimestres
            ds_Lista = obj_BL_AsignacionSemanasPorBimestres.FUN_LIS_AsignacionSemanasPorBimestres( _
          int_CodigoBimestre, int_CodigoAnioAcademico, _
            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_AsignacionSemanasPorBimestres As New bl_AsignacionSemanasPorBimestres
                ds_Lista = obj_BL_AsignacionSemanasPorBimestres.FUN_LIS_AsignacionSemanasPorBimestres( _
                 int_CodigoBimestre, int_CodigoAnioAcademico, _
                int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function



    ''' <summary>
    ''' Cambia el estado de la información.     
    ''' </summary>
    ''' <param name="int_Codigo">Código de Motivo de Beca</param>
    '''  <param name="str_accion">nombre de la acción</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_AsignacionSemanasPorBimestres As New bl_AsignacionSemanasPorBimestres
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado


        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_AsignacionSemanasPorBimestres.FUN_DEL_AsignacionSemanasPorBimestres(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listar()

    End Sub

 
    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Elimina los elementos de la lista
    ''' </summary>
    ''' <param name="combo">Nombre del combobox</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     28/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, False, True)

    End Sub

#End Region

#Region "Metodos del Gridview"

    ''' <summary>
    ''' Agrega el índice de páginas al combo de páginación. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CrearBotonesPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlPageSelector"), DropDownList)
        ddlPageSelector.Items.Clear()

        For i As Integer = 1 To gridView.PageCount
            ddlPageSelector.Items.Add(i.ToString())
        Next

        ddlPageSelector.SelectedIndex = pageIndex

    End Sub

    ''' <summary>
    ''' Muestra la numeración de registros por página y cantidad total de registros del listado actual. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function InformacionPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegs.Value)

    End Function

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Property GridViewSortDirection() As SortDirection

        Get
            If ViewState("sortDirection") Is Nothing Then
                ViewState("sortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection") = value
        End Set

    End Property

    ''' <summary>
    ''' Lista los datos de procedimientos realizados ordenados por Descripción.
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)

        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GV_AsignacionSemanaPorBimestre.DataSource = dv
        GV_AsignacionSemanaPorBimestre.DataBind()

    End Sub

    ''' <summary>
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     26/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GV_AsignacionSemanaPorBimestre.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GV_AsignacionSemanaPorBimestre.HeaderRow.FindControl("btnSorting_DescAnioAcademico"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GV_AsignacionSemanaPorBimestre.HeaderRow.FindControl("btnSorting_DescBimestre"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GV_AsignacionSemanaPorBimestre.HeaderRow.FindControl("btnSorting_CodigoSemanaAcademica"), ImageButton)

        If _btnSorting.ID = _btnSorting_d1.ID Then

            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d2.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"

        Else

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"

        End If

        If ViewState("Direccion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GV_AsignacionSemanaPorBimestre.PageCount Then
                Me.GV_AsignacionSemanaPorBimestre.PageIndex = _NumPag - 1
            Else
                Me.GV_AsignacionSemanaPorBimestre.PageIndex = 0
            End If

            Me.GV_AsignacionSemanaPorBimestre.SelectedIndex = -1

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GV_AsignacionSemanaPorBimestre_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            'If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
            '    Dim codigo As Integer = CInt(e.CommandArgument.ToString)
            '    Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
            '    Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

            '    If e.CommandName = "Actualizar" Then
            '        int_CodigoAccion = 6
            '        obtener(codigo)
            '    ElseIf e.CommandName = "Eliminar" Then
            '        int_CodigoAccion = 3
            '        cambiarEstado(codigo, "Eliminar")
            '    End If

            'End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaSemanas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVListaSemanas.RowDataBound

        Dim chk_Sel As CheckBox = CType(e.Row.FindControl("chkSeleccionar"), CheckBox)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim int_CodigoAsignacionSemana As Integer = e.Row.Cells(3).Text

            If int_CodigoAsignacionSemana > 0 Then
                chk_Sel.Checked = True
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

    Protected Sub GV_AsignacionSemanaPorBimestre_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")
        End If
        'Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

        'If e.Row.RowType = DataControlRowType.Pager Then

        '    Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
        '    _TotalPags.Text = GV_AsignacionSemanaPorBimestre.PageCount.ToString

        '    Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
        '    _Registros.Text = InformacionPager(GV_AsignacionSemanaPorBimestre, e.Row, Me)

        'ElseIf e.Row.RowType = DataControlRowType.DataRow Then

        '    If e.Row.DataItem("Estado") = "Activo" Then
        '        btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
        '        btnEliminar.Visible = True
        '    End If

        '    e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
        '    e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        'End If

    End Sub

    Protected Sub GV_AsignacionSemanaPorBimestre_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GV_AsignacionSemanaPorBimestre.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GV_AsignacionSemanaPorBimestre_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression

            ViewState("SortExpression") = sortExpression

            If GridViewSortDirection = SortDirection.Ascending Then
                GridViewSortDirection = SortDirection.Descending
                SortGridView(sortExpression, "DESC")
                ViewState("Direccion") = "DESC"
            Else
                GridViewSortDirection = SortDirection.Ascending
                SortGridView(sortExpression, "ASC")
                ViewState("Direccion") = "ASC"
            End If

            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub GV_AsignacionSemanaPorBimestre_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager(GV_AsignacionSemanaPorBimestre, e.Row, Me)
        End If

    End Sub

#End Region



    
End Class
