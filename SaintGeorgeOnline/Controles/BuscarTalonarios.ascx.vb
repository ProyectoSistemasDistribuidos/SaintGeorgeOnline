Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Class Controles_BuscarTalonarios
    Inherits System.Web.UI.UserControl

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Propiedades"

    Public Property FiltroTalonario() As Integer
        Get
            Return hiddenFiltroTalonario.Value
        End Get
        Set(ByVal value As Integer)
            hiddenFiltroTalonario.Value = value
        End Set
    End Property

    Public Property FiltroConcepto() As Integer
        Get
            Return hiddenFiltroConcepto.Value
        End Get
        Set(ByVal value As Integer)
            hiddenFiltroConcepto.Value = value
        End Set
    End Property

    Public Property RealizarConsultaID() As String
        Get
            Return hiddenRealizarConsultaID.Value
        End Get
        Set(ByVal value As String)
            hiddenRealizarConsultaID.Value = value
        End Set
    End Property

    Public Property CodigoPagoID() As String
        Get
            Return hiddenCodigoPagoID.Value
        End Get
        Set(ByVal value As String)
            hiddenCodigoPagoID.Value = value
        End Set
    End Property

    Public Property CodigoAlumnoID() As String
        Get
            Return hiddenCodigoAlumnoID.Value
        End Get
        Set(ByVal value As String)
            hiddenCodigoAlumnoID.Value = value
        End Set
    End Property

    Public Property NumeroPagoID() As String
        Get
            Return hiddenNumeroPagoID.Value
        End Get
        Set(ByVal value As String)
            hiddenNumeroPagoID.Value = value
        End Set
    End Property

    Public Property CodigoTalonarioID() As String
        Get
            Return hiddenCodigoTalonarioID.Value
        End Get
        Set(ByVal value As String)
            hiddenCodigoTalonarioID.Value = value
        End Set
    End Property

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                Me.tbFechaInicio.Text = Today.AddDays(-7) '"01/01/2010" 'Today.ToShortDateString
                Me.tbFechaFin.Text = Today.ToShortDateString

                ViewState("SortExpression") = "NombreCompletoAlumno"
                ViewState("Direccion") = "ASC"

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btnCerrar_BuscarTalonarios_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim hidden As HiddenField = CType(Page.FindControl(RealizarConsultaID), HiddenField)
        hidden.Value = "False"
        Me.ocultarModal()

    End Sub

    Protected Sub btnBuscar_BuscarTalonarios_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validarConsulta(usp_mensaje) Then
                Me.Listar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
            Me.mostrarModal()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnLimpiar_BuscarTalonarios_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        limpiarFormulario()
        Me.mostrarModal()

    End Sub

#End Region

#Region "Métodos Públicos"

    Public Sub setearParametros(ByVal int_FiltroTalonario As Integer, ByVal int_FiltroConcepto As Integer, ByVal str_CodigoPagoID As String, ByVal str_CodigoAlumnoID As String, ByVal str_NumeroPagoID As String, ByVal str_CodigoTalonarioID As String, ByVal str_HiddenRealizarConsulta As String)

        Me.FiltroTalonario = int_FiltroTalonario
        Me.FiltroConcepto = int_FiltroConcepto
        Me.CodigoPagoID = str_CodigoPagoID
        Me.CodigoAlumnoID = str_CodigoAlumnoID
        Me.NumeroPagoID = str_NumeroPagoID
        Me.CodigoTalonarioID = str_CodigoTalonarioID
        Me.RealizarConsultaID = str_HiddenRealizarConsulta

    End Sub

    Public Sub ocultarModal()

        Me.ModalPopupExtender_BuscarTalonarios.Hide()

    End Sub

    Public Sub mostrarModal()

        Me.ModalPopupExtender_BuscarTalonarios.Show()

    End Sub

    Public Sub Listar()

        Dim ds_Lista As DataSet = Me.ObtenerResultadoBusqueda(1)

        hfTotalRegsGVTodos.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        If hfTotalRegsGVTodos.Value > 0 Then
            If ds_Lista.Tables(0).Rows(0).Item("CodigoPago") <> -1 Then
                GVListaTodos.DataSource = ds_Lista.Tables(0)
                GVListaTodos.DataBind()

                'Dim sender As New Object
                'sender = GVListaTodos

                'SortGridView(CType(sender, GridView), ViewState("SortExpression"), ViewState("Direccion"))

                'If hfTotalRegsGVTodos.Value > 0 Then
                '    ImagenSorting(CType(sender, GridView), ViewState("SortExpression"))
                'End If

            Else
                GVListaTodos.DataBind()
            End If
        Else
            GVListaTodos.DataBind()
        End If

    End Sub

    Private Function ObtenerResultadoBusqueda(ByVal int_Modo As Integer) As DataSet

        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        Dim int_CodigoTalonario As Integer
        Dim int_CodigoConceptoAuxiliar As Integer

        If FiltroConcepto = 11 Then ' Si el mótivo de la nota de Débito es "Aumento de Cuota de Ingreso"
            int_CodigoConceptoAuxiliar = 3 ' el filtro de búsqueda de pagos es por concepto de cuota de ingreso
        Else
            int_CodigoConceptoAuxiliar = 0
        End If

        Dim str_NumeroPago As String = ""
        Dim str_ApellidoPaterno As String = ""
        Dim str_ApellidoMaterno As String = ""
        Dim str_Nombre As String = ""
        Dim int_TipoFecha As Integer
        Dim dt_FechaInicial As Date
        Dim dt_FechaFin As Date

        int_CodigoTalonario = Me.ddlTalonario.SelectedValue
        str_NumeroPago = ""
        str_ApellidoPaterno = Me.tbApellidoPaterno.Text.Trim
        str_ApellidoMaterno = Me.tbApellidoMaterno.Text.Trim
        str_Nombre = Me.tbNombre.Text.Trim
        int_TipoFecha = Me.ddlFecha.SelectedValue
        dt_FechaInicial = Me.tbFechaInicio.Text
        dt_FechaFin = Me.tbFechaFin.Text

        Dim obj_BL_Pagos As New bl_Pagos
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            ds_Lista = obj_BL_Pagos.FUN_LIS_PagosGeneralParaNotas(int_CodigoConceptoAuxiliar, int_CodigoTalonario, str_NumeroPago, _
                        str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, _
                        int_TipoFecha, dt_FechaInicial, dt_FechaFin, _
                        int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            ViewState("Listado_Datos") = ds_Lista

        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                ds_Lista = obj_BL_Pagos.FUN_LIS_PagosGeneralParaNotas(int_CodigoConceptoAuxiliar, int_CodigoTalonario, str_NumeroPago, _
                        str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, _
                        int_TipoFecha, dt_FechaInicial, dt_FechaFin, _
                        int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
                ViewState("Listado_Datos") = ds_Lista

            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

#End Region

#Region "Métodos Privados"

    Public Sub limpiarFormulario()

        Me.ddlTalonario.SelectedValue = 0
        Me.tbApellidoPaterno.Text = ""
        Me.tbApellidoMaterno.Text = ""
        Me.tbNombre.Text = ""
        Me.ddlFecha.SelectedValue = 1

        Me.tbFechaInicio.Text = Today.AddDays(-7) '"01/01/2010" 'Today.ToShortDateString
        Me.tbFechaFin.Text = Today.ToShortDateString

        Me.GVListaTodos.DataBind()

    End Sub

    Public Sub cargarComboTalonarios()

        Dim int_CodigoPagina As Integer = FiltroTalonario
        Dim int_FiltroConcepto As Integer = FiltroConcepto
        Dim int_Estado As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim obj_BL_Talonarios As New bl_Talonarios

        Dim ds_Lista As DataSet = obj_BL_Talonarios.FUN_LIS_TalonariosPorModulo(int_CodigoPagina, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If int_FiltroConcepto > 0 Then
            Dim dt As DataTable = ds_Lista.Tables(0).Clone
            Dim ds_Aux As New DataSet
            For Each dr As DataRow In ds_Lista.Tables(0).Rows
                If int_FiltroConcepto = 11 Then ' Aumento de Cuota de Ingreso - Codigo : 11 
                    If dr.Item("Codigo") = 1 Or dr.Item("Codigo") = 2 Then ' Importo los registros de boleta o factura
                        dt.ImportRow(dr)
                    End If
                ElseIf int_FiltroConcepto = 10 Then ' Protesta de Letra - Codigo : 10 
                    If dr.Item("Codigo") = 5 Then ' Importo los registros de letra
                        dt.ImportRow(dr)
                    End If
                End If
            Next
            ds_Aux.Tables.Add(dt)
            Controles.llenarCombo(Me.ddlTalonario, ds_Aux, "Codigo", "DescripcionCompleta", False, True)
        Else
            Controles.llenarCombo(Me.ddlTalonario, ds_Lista, "Codigo", "DescripcionCompleta", False, True)
        End If

    End Sub

    Private Function validarConsulta(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If Me.ddlTalonario.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Talonario")
            result = False
        End If

        If Me.ddlFecha.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Fecha")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub actualizarComboPadre()

        'Dim str_Descripcion As String = ""
        'Dim int_Estado As Integer = 1
        'Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        'Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        'Dim obj_BL_Diagnostico As New bl_Diagnosticos
        'Dim ds_Lista As DataSet = obj_BL_Diagnostico.FUN_LIS_Diagnostico(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        'Dim str_NombreComboPadre As String = ComboPadreID
        'Dim combo As DropDownList = CType(Page.FindControl(str_NombreComboPadre), DropDownList)
        'Controles.llenarCombo(combo, ds_Lista, "Codigo", "Descripcion", False, True)
        'combo.SelectedValue = Me.CodigoRegistro

    End Sub

    Private Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    Private Sub MostrarMensaje(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Dim str_Script As String = ""
        Select Case str_TipoMensaje
            Case "Alert"
                str_Script = "Sexy.alert('<br />" & str_Mensaje & "');"
            Case "Info"
                str_Script = "Sexy.info('<br />" & str_Mensaje & "');"
            Case "Error"
                str_Script = "Sexy.error('<br />" & str_Mensaje & "');"
        End Select
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", str_Script, True)

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

                Dim str_CodigoAlumno As String = CType(row.FindControl("lblCodigoAlumno"), Label).Text
                Dim str_NumeroPago As String = CType(row.FindControl("lblNumeroPago"), Label).Text
                Dim str_CodigoTalonario As String = CType(row.FindControl("lbCodigoTalonario"), Label).Text

                Dim miDDL As DropDownList = CType(Page.FindControl(CodigoTalonarioID), DropDownList)
                Dim miTB As TextBox = CType(Page.FindControl(NumeroPagoID), TextBox)
                Dim mihidden As HiddenField = CType(Page.FindControl(RealizarConsultaID), HiddenField)

                miDDL.SelectedValue = str_CodigoTalonario
                miTB.Text = str_NumeroPago
                mihidden.Value = "True"
                Me.ocultarModal()

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

            Me.mostrarModal()

        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaTodos_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then
            CrearBotonesPager(CType(sender, GridView), e.Row, Me.Page)
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

            Me.mostrarModal()

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

            Me.mostrarModal()

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

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = gridview.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(gridview, e.Row, Me.Page)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

#Region "Seguridad"

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

    Private Function Obtener_CodigoTipoUsuarioLogueado() As Integer

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
            int_CodigoTipoUsuarioLogueado = -1
        End Try

        Return int_CodigoTipoUsuarioLogueado

    End Function

    Private Function Obtener_CodigoUsuarioLogueado() As Integer

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
            int_CodigoUsuarioLogueado = -1
        End Try

        Return int_CodigoUsuarioLogueado

    End Function

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

    Private Sub SetearAccionesAcceso()

        RegistrarAccesoPagina(0, 7)

    End Sub

#End Region

End Class