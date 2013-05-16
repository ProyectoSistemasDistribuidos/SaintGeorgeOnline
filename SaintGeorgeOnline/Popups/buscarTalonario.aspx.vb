Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_Utilities

Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Web.Services
Imports System.Configuration.ConfigurationManager

''' <summary>
''' Modulo de Consulta de Documentos de Tesorería
''' </summary>
''' <remarks>
''' Código del Modulo:    
''' Código de la Opción:  
''' </remarks>

Partial Class Popups_buscarTalonario
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                btnCerrar.Attributes.Add("onclick", "return cerrar();")

                cargarComboTalonarios()

                tbFechaInicio.Text = Today.ToShortDateString
                tbFechaFin.Text = Today.ToShortDateString

                ViewState("SortExpression") = "NombreCompletoAlumno"
                ViewState("Direccion") = "ASC"

                'Tipo Persona - Todos : 0 / Alumno : 1 / Personal : 2 / Familiar : 3 / Otros : 4
                If Request.QueryString("Tipo") IsNot Nothing Then

                    Dim tipo As Integer = CInt(Request.QueryString("tipo").ToString)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim usp_mensaje As String = ""
            If validarConsulta(usp_mensaje) Then
                Listar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
    End Sub

    ''' <summary>
    ''' Valida los campos del formulario antes de proceder a "Buscar"
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     28/05/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarConsulta(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If ddlFecha.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Fecha")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

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
        RegistrarAccesoPagina(0, 7)

    End Sub

    ''' <summary>
    ''' Limpia los parametros de busqueda de fichas de atención
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     03/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        ddlTalonario.SelectedValue = 0
        tbApellidoPaterno.Text = ""
        tbApellidoMaterno.Text = ""
        tbNombre.Text = ""

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
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim str_NombreUsuario As String = Obtener_NombreUsuarioLogueado()

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(0, 7, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Envio los mensajes de Alerta
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_Mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_TipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub MostrarMensaje(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Dim str_Script As String = ""
        str_Script = SaintGeorgeOnline_Utilities.Alertas.ObtenerMensaje(str_Mensaje, str_TipoMensaje)
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", str_Script, True)

    End Sub

    ''' <summary>
    ''' Cierra el PopUp de busqueda de personas.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Cerrar()

        Dim sb As New StringBuilder
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "alert_cerrar", "window.close();", True)

    End Sub

    ''' <summary>
    ''' Busca las personas según los criterios que se hayan seleccionado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Listar()

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        hfTotalRegsGVTodos.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        'If hfTotalRegsGVTodos.Value > 0 Then

        GVListaTodos.DataSource = ds_Lista.Tables(0)
        GVListaTodos.DataBind()

        'End If


        Dim sender As New Object
        sender = GVListaTodos

        SortGridView(CType(sender, GridView), ViewState("SortExpression"), ViewState("Direccion"))

        If hfTotalRegsGVTodos.Value > 0 Then
            ImagenSorting(CType(sender, GridView), ViewState("SortExpression"))
        End If

    End Sub

    ''' <summary>
    ''' Retorna el DataSet de la busqueda según los filtros indicados en el formulario.
    ''' </summary>
    ''' <returns>DataSet de resultados de busqueda</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     02/02/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
    
        Dim int_CodigoTalonario As Integer
        Dim str_NumeroPago As String = ""
        Dim str_ApellidoPaterno As String = ""
        Dim str_ApellidoMaterno As String = ""
        Dim str_Nombre As String = ""
        Dim int_TipoFecha As Integer
        Dim dt_FechaInicial As Date
        Dim dt_FechaFin As Date

        int_CodigoTalonario = ddlTalonario.SelectedValue
        str_NumeroPago = ""
        str_ApellidoPaterno = tbApellidoPaterno.Text.Trim
        str_ApellidoMaterno = tbApellidoMaterno.Text.Trim
        str_Nombre = tbNombre.Text.Trim
        int_TipoFecha = ddlFecha.SelectedValue
        dt_FechaInicial = tbFechaInicio.Text
        dt_FechaFin = tbFechaFin.Text

        Dim obj_BL_Pagos As New bl_Pagos
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            ds_Lista = obj_BL_Pagos.FUN_LIS_PagosGeneral(int_CodigoTalonario, str_NumeroPago, str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, _
                       int_TipoFecha, dt_FechaInicial, dt_FechaFin, _
                       int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista

        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                ds_Lista = obj_BL_Pagos.FUN_LIS_PagosGeneral(int_CodigoTalonario, str_NumeroPago, str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, _
            int_TipoFecha, dt_FechaInicial, dt_FechaFin, _
            int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista

            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

    ''' <summary>
    ''' Carga la lista de Talonarios
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     30/05/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTalonarios()

        Dim int_Modulo As Integer = 1
        Dim int_Estado As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim obj_BL_Talonarios As New bl_Talonarios

        Dim ds_Lista As DataSet = obj_BL_Talonarios.FUN_LIS_TalonariosPorModulo(int_Modulo, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlTalonario, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Setea los seleccionables a sus valores por defecto.
    ''' </summary>
    ''' <param name="combo">Control de tipo ComboBox</param>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)

        Controles.limpiarCombo(combo, True, False)

    End Sub

#End Region

#Region "Métodos de Auditoria"

    ''' <summary>
    ''' Registra el acceso al formulario. (Log de Accesos)
    ''' </summary>
    ''' <param name="int_CodigoSubBloque">Codigo del SubBloque de Menú.</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub RegistrarAccesoPagina(ByVal int_CodigoModulo As Integer, ByVal int_CodigoSubBloque As Integer)
        Dim obj_BL_Usuario As New bl_Logueo
        Dim str_Acceso As String = ""
        Dim obj_encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim int_CodigoSession As Integer = 0
        Dim str_Info As String = ""
        Dim astr_ArrayDatos() As String
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim ticket As FormsAuthenticationTicket = identity.Ticket

            str_Info = obj_encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            astr_ArrayDatos = str_Info.Split(";")
            int_CodigoSession = astr_ArrayDatos(5)
            int_CodigoUsuario = astr_ArrayDatos(0)
            int_CodigoTipoUsuario = astr_ArrayDatos(1)

            obj_BL_Usuario.FUN_INS_AccesoUsuarioDetalle(int_CodigoSession, int_CodigoModulo, int_CodigoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario)
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Obtiene el código del usuario logueado al sistema
    ''' </summary>
    ''' <returns>código de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoUsuarioLogueado() As Integer

        Dim int_CodigoUsuarioLogueado As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoUsuarioLogueado = str_ArrayDatos(0)

        Catch ex As Exception
            'EnvioEmailError(-1, ex.ToString)
            int_CodigoUsuarioLogueado = -1
        End Try

        Return 1 'int_CodigoUsuarioLogueado

    End Function

    ''' <summary>
    ''' Obtiene el código del tipo de usuario logueado al sistema
    ''' </summary>
    ''' <returns>código de tipo de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoTipoUsuarioLogueado() As Integer

        Dim int_CodigoTipoUsuarioLogueado As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoTipoUsuarioLogueado = str_ArrayDatos(1)

        Catch ex As Exception
            'EnvioEmailError(-1, ex.ToString)
            int_CodigoTipoUsuarioLogueado = -1
        End Try

        Return 1 'int_CodigoTipoUsuarioLogueado

    End Function

    ''' <summary>
    ''' Obtiene el nombre del usuario logueado al sistema.
    ''' </summary>
    ''' <returns>Nombre de Usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_NombreUsuarioLogueado() As String

        Dim str_NombreUsuarioLogueado As String = ""

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            str_NombreUsuarioLogueado = str_ArrayDatos(2)

        Catch ex As Exception
            'EnvioEmailError("Obtener Nombre de Trabajador logueado", ex.ToString)
            str_NombreUsuarioLogueado = ""
        End Try

        Return str_NombreUsuarioLogueado

    End Function

#End Region

#Region "Eventos del Gridview"

    Protected Sub GVListaTodos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Then

                int_CodigoAccion = 5
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                Dim obj_be_Pagos As New be_Pagos

                obj_be_Pagos.CodigoPago = codigo
                obj_be_Pagos.CodigoAlumno = CType(row.FindControl("lblCodigoAlumno"), Label).Text
                obj_be_Pagos.NumeroPago = CType(row.FindControl("lblNumeroPago"), Label).Text
                obj_be_Pagos.CodigoTalonario = CType(row.FindControl("lbCodigoTalonario"), Label).Text

                Session("PopupPago") = obj_be_Pagos
                Page.Session("ResetearPadre") = True
                Cerrar()

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaTodos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            AgregarAtributos(CType(sender, GridView), e)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaTodos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                CType(sender, GridView).PageIndex = e.NewPageIndex
            End If

            SortGridView(CType(sender, GridView), ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(CType(sender, GridView), ViewState("SortExpression"))

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaTodos_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager(CType(sender, GridView), e.Row, Me)
        End If

    End Sub

    Protected Sub GVListaTodos_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortExpression As String = e.SortExpression

            ViewState("SortExpression") = sortExpression

            If GridViewSortDirection = SortDirection.Ascending Then
                GridViewSortDirection = SortDirection.Descending
                SortGridView(CType(sender, GridView), sortExpression, "DESC")
                ViewState("Direccion") = "DESC"
            Else
                GridViewSortDirection = SortDirection.Ascending
                SortGridView(CType(sender, GridView), sortExpression, "ASC")
                ViewState("Direccion") = "ASC"
            End If

            ImagenSorting(CType(sender, GridView), e.SortExpression)
        Catch ex As Exception
            EnvioEmailError(112, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _gridview As GridView = CType(_DropDownList.Parent.Parent.Parent.Parent, GridView)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= _gridview.PageCount Then
                _gridview.PageIndex = _NumPag - 1
            Else
                _gridview.PageIndex = 0
            End If

            _gridview.SelectedIndex = -1
            'Listar()
            SortGridView(_gridview, ViewState("SortExpression"), ViewState("Direccion"))
            ImagenSorting(_gridview, ViewState("SortExpression"))

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Métodos del Gridview"

    ''' <summary>
    ''' Lista las fichas de atención ordenadas por un campo especifico
    ''' </summary>
    ''' <param name="sortExpression">Campo por el cual se realiza el ordenamiento.</param>
    ''' <param name="direction">Dirección ascendente o descendente la cual se usará en el ordenamiento </param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SortGridView(ByVal gridView As GridView, ByVal sortExpression As String, ByVal direction As String)

        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(2)

        hfTotalRegsGVTodos.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_Lista.Tables(0))
        dv.Sort = sortExpression + " " + direction

        gridView.DataSource = dv
        gridView.DataBind()
        gridView.Visible = True

    End Sub

    ''' <summary>
    ''' Cambia la dirección de ordenamiento del GridView
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
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
    ''' Cambia la imagen dependiendo el campo y dirección de ordenamiento del gridView.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal gridView As GridView, ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(gridView.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(gridView.HeaderRow.FindControl("btnSorting_NombreCompletoAlumno"), ImageButton)
        'Dim _btnSorting_d2 As ImageButton = CType(GVListaFichas.HeaderRow.FindControl("btnSorting_DescTipoPaciente"), ImageButton)
        'Dim _btnSorting_d3 As ImageButton = CType(GVListaFichas.HeaderRow.FindControl("btnSorting_DescSede"), ImageButton)
        'Dim _btnSorting_d4 As ImageButton = CType(GVListaFichas.HeaderRow.FindControl("btnSorting_FechaHoraAtencionDt"), ImageButton)

        'If _btnSorting.ID = _btnSorting_d1.ID Then

        '    _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d2.ToolTip = "Descendente"
        '    _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d3.ToolTip = "Descendente"
        '    _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d4.ToolTip = "Descendente"

        'ElseIf _btnSorting.ID = _btnSorting_d2.ID Then

        '    _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d1.ToolTip = "Descendente"
        '    _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d3.ToolTip = "Descendente"
        '    _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d4.ToolTip = "Descendente"

        'ElseIf _btnSorting.ID = _btnSorting_d3.ID Then

        '    _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d1.ToolTip = "Descendente"
        '    _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d2.ToolTip = "Descendente"
        '    _btnSorting_d4.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d4.ToolTip = "Descendente"

        'Else

        '    _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d1.ToolTip = "Descendente"
        '    _btnSorting_d2.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d2.ToolTip = "Descendente"
        '    _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
        '    _btnSorting_d3.ToolTip = "Descendente"

        'End If

        If ViewState("Direccion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

    ''' <summary>
    ''' Agrega el índice de páginas al combo de páginación. 
    ''' </summary>
    ''' <param name="gridView">GridView del formulario</param>
    ''' <param name="gvPagerRow">Fila del Gridview </param>
    ''' <param name="page">Página actual del formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
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
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     25/01/2011
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

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegsGVTodos.Value)

    End Function

    ''' <summary>
    ''' Agrega atributos a la grilla que lo invoque. (crea el paginado y diferentes estilos)
    ''' </summary>
    ''' <param name="gridview">control de tipo grilla</param>
    ''' <param name="e">evento de tipo grilla</param>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub AgregarAtributos(ByVal gridview As GridView, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        'Dim btnVerFoto As HtmlAnchor = e.Row.FindControl("btnLinkVerFoto")
        'Dim lblTipoPersona As Label = e.Row.FindControl("lbCodigoTipoPaciente")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = gridview.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(gridview, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            'btnVerFoto.Attributes.Add("rel", "sexylightbox")

            'If lblTipoPersona.Text = 1 Then
            '    btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & e.Row.DataItem("RutaFoto")
            'ElseIf lblTipoPersona.Text = 2 Then
            '    btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & e.Row.DataItem("RutaFoto")
            'ElseIf lblTipoPersona.Text = 3 Then
            '    btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & e.Row.DataItem("RutaFoto")
            'Else
            '    btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & e.Row.DataItem("RutaFoto")
            'End If


            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

End Class
