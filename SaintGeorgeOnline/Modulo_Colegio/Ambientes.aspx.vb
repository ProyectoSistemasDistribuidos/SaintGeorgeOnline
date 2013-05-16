Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Modulo de Mantenimiento de Ambientes
''' </summary>
''' <remarks>
''' Código del Modulo:    2
''' Código de la Opción:  75
''' </remarks>
Partial Class Mantenimientos_Colegio_Ambientes
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 2
    Private cod_Opcion As Integer = 75

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Ambientes")
            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                ViewState("SortExpression") = "NombreAmbiente"
                ViewState("Direccion") = "ASC"
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")
                tbNombre.Attributes.Add("onkeypress", " ValidarLength(this, 100);")
                tbNombre.Attributes.Add("onkeyup", " ValidarLength(this, 100);")

                tbReferencia.Attributes.Add("onkeypress", " ValidarLength(this, 100);")
                tbReferencia.Attributes.Add("onkeyup", " ValidarLength(this, 100);")

                cargarCombo()
                listar()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        limpiarCampos()
        pnModalAmbientes.Show()

    End Sub

    Protected Sub btnCancelar_Click()

        miTab1.Enabled = True
        TabContainer1.ActiveTabIndex = 0
        ddlBuscarSede.Focus()
        hd_Codigo.Value = 0

    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try

    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then
                Grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
                pnModalAmbientes.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ModalPopupExtender1.Dispose()

    End Sub

    Protected Sub btnCerrarPanelAmbiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        miTab1.Enabled = True
        TabContainer1.ActiveTabIndex = 0
        ddlBuscarSede.Focus()
        hd_Codigo.Value = 0
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        limpiarFiltros()

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setear permisos de acciones sobre el formulario según la configuración del usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(cod_Modulo, cod_Opcion)
    End Sub

    ''' <summary>
    ''' Carga la información de todos los seleccionables del formulario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombo()
        cargarComboSedesColegio()
        cargarComboTipoAmbiente()
        cargarComboPabellon()
        cargarComboPiso()
    End Sub

    ''' <summary>
    ''' Carga la información del seleccionable de sedes.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSedesColegio()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarSede, ds_Lista, "Codigo", "NombreSede", True, False)
        Controles.llenarCombo(ddlSede, ds_Lista, "Codigo", "NombreSede", False, True)

    End Sub

    ''' <summary>
    ''' Carga la información del seleccionable de tipos de ambientes.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoAmbiente()

        Dim obj_BL_TipoAmbiente As New bl_TipoAmbientes
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_TipoAmbiente.FUN_LIS_TipoAmbientes("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarTipoAmbiente, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlTipoAmbiente, ds_Lista, "Codigo", "Descripcion", False, True)
        Controles.llenarCombo(ddlTipoAmbienteProyectado, ds_Lista, "Codigo", "Descripcion", False, True)
    End Sub

    ''' <summary>
    ''' Carga la información del seleccionable de pabellones.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboPabellon()
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_BL_Bloques As New bl_Bloques
        Dim ds_Lista As DataSet = obj_BL_Bloques.FUN_LIS_Bloques("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarPabellon, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlBloque, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga la información del seleccionable de pisos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboPiso()

        Dim obj_BL_Pisos As New bl_Pisos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Pisos.FUN_LIS_Pisos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlBuscarPiso, ds_Lista, "Codigo", "Descripcion", True, False)
        Controles.llenarCombo(ddlPiso, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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
    ''' Limpia los filtros de búsqueda del formulario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks> 
    Private Sub limpiarFiltros()
        ddlSede.SelectedValue = 0
        ddlTipoAmbiente.SelectedValue = 0
        ddlBuscarPabellon.SelectedValue = 0
        ddlBuscarPiso.SelectedValue = 0
        ddlBuscarTipoAmbiente.SelectedValue = 0

        rbBuscarReservaAmbiente.SelectedValue = -1
        rbBuscarMultimedia.SelectedValue = -1

        tbBuscarNombre.Text = ""
        tbBuscarNombre.Focus()
    End Sub

    ''' <summary>
    ''' Exporte el listado de la información filtrada en los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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
        dt = Datos.agregarColumna(dt, "NombreAmbiente", "String")
        dt = Datos.agregarColumna(dt, "TipoAmbiente", "String")
        dt = Datos.agregarColumna(dt, "Reservable", "String")
        dt = Datos.agregarColumna(dt, "Multimedia", "String")
        dt = Datos.agregarColumna(dt, "Bloque", "String")
        dt = Datos.agregarColumna(dt, "Piso", "String")
        dt = Datos.agregarColumna(dt, "Capacidad", "String")
        dt = Datos.agregarColumna(dt, "Referencia", "String")
        dt = Datos.agregarColumna(dt, "Sede", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("NombreAmbiente") = dr.Item("NombreAmbiente").ToString
            auxDR.Item("TipoAmbiente") = dr.Item("TipoAmbiente").ToString
            auxDR.Item("Bloque") = dr.Item("Bloque").ToString
            auxDR.Item("Reservable") = dr.Item("Reservable").ToString
            auxDR.Item("Multimedia") = dr.Item("Multimedia").ToString
            auxDR.Item("Piso") = dr.Item("Piso").ToString
            auxDR.Item("Capacidad") = dr.Item("Capacidad").ToString
            auxDR.Item("Referencia") = dr.Item("Referencia").ToString
            auxDR.Item("Sede") = dr.Item("Sede").ToString
            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Ambiente")
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

            NombreArchivo = Exportacion.ExportarReporte(dt, "Ambiente")
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

            m = Exportacion.ExportarReporte_Pdf(dt, "Ambiente")

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

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Ambiente")
            reporte_html = Arreglo_Datos(0)

            Session("Exportaciones_RepHtml") = reporte_html
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
        End If

    End Sub

    ''' <summary>
    ''' Bloquea el formulario de busqueda cuando se selecciona la opción de Nuevo y Modificación de Registro.
    ''' </summary>
    ''' <param name="str_Modo"></param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerRegistro(ByVal str_Modo As String)

        miTab1.Enabled = False
        TabContainer1.ActiveTabIndex = 1
        ddlBloque.Focus()

    End Sub

    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlSede.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Sede")
            result = False
        End If

        If tbNombre.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Nombre")
            result = False
        End If
        If Validacion.ValidarCamposIngreso(tbNombre) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Nombre")
            result = False
        End If

        If ddlTipoAmbiente.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Ambiente")
            result = False
        End If

        If ddlBloque.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Bloque")
            result = False
        End If

        If ddlPiso.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Piso")
            result = False
        End If

        If Validacion.ValidarCamposIngreso(tbReferencia) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Referencia")
            result = False
        End If

        If tbCapacidad.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Capacidad")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Limpia los campos del formulario de registro.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()

        hd_Codigo.Value = 0
        ddlBloque.SelectedValue = 0
        ddlPiso.SelectedValue = 0
        ddlSede.SelectedValue = 0
        ddlTipoAmbiente.SelectedValue = 0
        rbMultimedia.SelectedValue = 0
        rbReserva.SelectedValue = 0
        tbCapacidad.Text = 0
        tbArea.Text = ""
        tbNombre.Text = ""
        tbReferencia.Text = ""
        tbArea.Text = ""
        ddlTipoAmbienteProyectado.SelectedValue = 0
        tbCodigoAmbienteAlfanumerico.Text = ""

    End Sub

    ''' <summary>
    ''' Lista la relación de ambientes.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        Juan Vento
    ''' Fecha de modificación: 15/02/2011
    ''' </remarks>
    Private Sub listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

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

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet
        Dim int_CodigoSede As Integer = CInt(ddlBuscarSede.SelectedValue)
        Dim str_Nombre As String = tbBuscarNombre.Text.Trim()
        Dim int_CodigoTipoAmbiente As Integer = CInt(ddlBuscarTipoAmbiente.SelectedValue)
        Dim int_CodigoPabellon As Integer = CInt(ddlBuscarPabellon.SelectedValue)
        Dim int_CodigoPiso As Integer = CInt(ddlBuscarPiso.SelectedValue)
        Dim int_Reservable As Integer = CInt(rbBuscarReservaAmbiente.SelectedValue)
        Dim int_Multimedia As Integer = CInt(rbBuscarMultimedia.SelectedValue)
        Dim int_Estado As Integer = 1 ' CInt(rbEstados.SelectedValue)
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Ambiente As New bl_Ambientes
            ds_Lista = obj_BL_Ambiente.FUN_LIS_Ambientes(int_CodigoSede, str_Nombre, int_CodigoTipoAmbiente, int_CodigoPabellon, int_CodigoPiso, int_Reservable, int_Multimedia, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Ambiente As New bl_Ambientes
                ds_Lista = obj_BL_Ambiente.FUN_LIS_Ambientes(int_CodigoSede, str_Nombre, int_CodigoTipoAmbiente, int_CodigoPabellon, int_CodigoPiso, int_Reservable, int_Multimedia, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Obtiene la información sobre un registro y lo muestra en el formulario.
    ''' </summary>
    ''' <param name="int_Codigo">Código de ambiente</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As Integer)

        Dim obj_BL_bl_Ambientes As New bl_Ambientes
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_bl_Ambientes.FUN_GET_Ambientes(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        hd_Codigo.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("Codigo").ToString)
        ddlSede.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoSede").ToString)
        ddlTipoAmbiente.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoTipoAmbiente").ToString)
        ddlTipoAmbienteProyectado.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("codigoTipoAmbienteProyecto").ToString)
        tbNombre.Text = ds_Lista.Tables(0).Rows(0).Item("NombreAmbiente").ToString
        ddlBloque.SelectedValue = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoBloque").ToString)
        ddlPiso.Text = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoPiso").ToString)
        tbReferencia.Text = ds_Lista.Tables(0).Rows(0).Item("Referencia").ToString
        tbCapacidad.Text = CInt(ds_Lista.Tables(0).Rows(0).Item("Capacidad").ToString)
        rbReserva.SelectedValue = IIf(CBool(ds_Lista.Tables(0).Rows(0).Item("Reservable")), 1, 0)
        rbMultimedia.SelectedValue = IIf(CBool(ds_Lista.Tables(0).Rows(0).Item("Multimedia")), 1, 0)
        tbCodigoAmbienteAlfanumerico.Text = ds_Lista.Tables(0).Rows(0).Item("Codigoalfanumerico").ToString
        tbArea.Text = ds_Lista.Tables(0).Rows(0).Item("Area").ToString

        pnModalAmbientes.Show()

    End Sub

    ''' <summary>
    ''' Llama al metodo de Eliminar o Activar según la acción seleccionada.
    ''' </summary>
    ''' <param name="int_Codigo">codigo de ambiente</param>
    ''' <param name="str_accion">tipo de acción a realizar (Activar o Eliminar)</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub cambiarEstado(ByVal int_Codigo As Integer, ByVal str_accion As String)

        Dim obj_BL_Ambientes As New bl_Ambientes
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        If str_accion = "Eliminar" Then
            usp_valor = obj_BL_Ambientes.FUN_DEL_Ambientes(int_Codigo, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        listar()

    End Sub

    ''' <summary>
    ''' Graba o Actualiza el registro indicado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_Ambientes As New be_Ambientes
        Dim obj_BL_Ambientes As New bl_Ambientes
        Dim BoolGrabar As Integer = hd_Codigo.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        obj_BE_Ambientes.CodigoSede = CInt(ddlSede.SelectedValue)
        obj_BE_Ambientes.NombreAmbiente = tbNombre.Text.Trim()
        obj_BE_Ambientes.CodigoTipoAmbiente = CInt(ddlTipoAmbiente.SelectedValue)
        obj_BE_Ambientes.CodigoPabellon = CInt(ddlBloque.SelectedValue)
        obj_BE_Ambientes.CodigoPiso = CInt(ddlPiso.SelectedValue)
        obj_BE_Ambientes.Referencia = tbReferencia.Text.Trim()
        obj_BE_Ambientes.Capacidad = CInt(tbCapacidad.Text.Trim())
        obj_BE_Ambientes.CodigoTipoAmbienteProyecto = CInt(ddlTipoAmbienteProyectado.SelectedValue)
        obj_BE_Ambientes.Reservable = CInt(rbReserva.SelectedValue)
        obj_BE_Ambientes.Multimedia = CInt(rbMultimedia.SelectedValue)
        obj_BE_Ambientes.AreaAmbiente = tbArea.Text.Trim
        obj_BE_Ambientes.CodigoAlfanumerico = tbCodigoAmbienteAlfanumerico.Text.Trim

        If BoolGrabar = 0 Then
            usp_valor = obj_BL_Ambientes.FUN_INS_Ambientes(obj_BE_Ambientes, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Else
            obj_BE_Ambientes.CodigoAmbiente = CInt(BoolGrabar)
            usp_valor = obj_BL_Ambientes.FUN_UPD_Ambientes(obj_BE_Ambientes, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        End If

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            btnCancelar_Click()
            limpiarCampos()
            listar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
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

#End Region

#Region "Metodos del Gridview"

    ''' <summary>
    ''' Crea los botones de siguientex, atraz, etc de la lista de información.
    ''' </summary>
    ''' <param name="gridView">Control de tipo griedview</param>
    ''' <param name="gvPagerRow">Fila de la grilla</param>
    ''' <param name="page">Página</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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
    ''' Información referencial del listado de información de la grilla.
    ''' </summary>
    ''' <param name="gridView">control tipo grilla</param>
    ''' <param name="gvPagerRow">fila de la grilla</param>
    ''' <param name="page">página</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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
    ''' Setea los indicadores (ViewState) de la dirección del ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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
    ''' Ordena el listado de información de la grilla según la columa y dirección seleccionada.
    ''' </summary>
    ''' <param name="sortExpression">Columna por la cual se ordenará el listado</param>
    ''' <param name="direction">Dirección ASC o DESC por el cual se ordenará el listado</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)
        hfTotalRegs.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        GridView1.DataSource = dv
        GridView1.DataBind()

    End Sub

    ''' <summary>
    ''' Dirección del grafico indicador del ordenamiento basado en columnas.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_NombreAmbiente"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_TipoAmbiente"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Bloque"), ImageButton)
        Dim _btnSorting_d4 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Piso"), ImageButton)
        Dim _btnSorting_d5 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Reservable"), ImageButton)
        Dim _btnSorting_d6 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Multimedia"), ImageButton)
        Dim _btnSorting_d7 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Sede"), ImageButton)

        If _btnSorting.ID = _btnSorting_d1.ID Then
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"
            _btnSorting_d7.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d7.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d2.ID Then
            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"
            _btnSorting_d7.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d7.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d3.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"
            _btnSorting_d7.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d7.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d4.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"
            _btnSorting_d7.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d7.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d5.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"
            _btnSorting_d7.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d7.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d6.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d7.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d7.ToolTip = "Descendente"


        Else

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"
            _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d2.ToolTip = "Descendente"
            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"
            _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d4.ToolTip = "Descendente"
            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"
            _btnSorting_d6.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d6.ToolTip = "Descendente"
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

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.GridView1.PageCount Then
                Me.GridView1.PageIndex = _NumPag - 1
            Else
                Me.GridView1.PageIndex = 0
            End If

            Me.GridView1.SelectedIndex = -1

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Or e.CommandName = "Activar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    obtener(codigo)
                ElseIf e.CommandName = "Eliminar" And row.Cells(5).Text <> "Inactivo" Then
                    int_CodigoAccion = 3
                    cambiarEstado(codigo, "Eliminar")
                ElseIf e.CommandName = "Activar" And row.Cells(5).Text <> "Activo" Then
                    int_CodigoAccion = 2
                    cambiarEstado(codigo, "Activar")
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
        Dim btnActivar As ImageButton = e.Row.FindControl("btnActivar")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.DataItem("Estado") = "Activo" Then
                btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")
                btnActivar.Visible = False
                btnEliminar.Visible = True
            Else
                btnActivar.Attributes.Add("OnClick", "return confirm_activar();")
                btnActualizar.Visible = False
                btnEliminar.Visible = False
                e.Row.ForeColor = Drawing.Color.DarkRed

            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.GridView1.PageIndex = e.NewPageIndex
            End If

            SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(ViewState("SortExpression"))
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
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

            ImagenSorting(e.SortExpression)
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager(GridView1, e.Row, Me)
        End If

    End Sub

#End Region

End Class
