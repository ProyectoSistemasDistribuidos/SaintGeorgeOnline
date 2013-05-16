Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio

''' <summary>
''' Modulo de Mantenimiento de Enfermedades
''' </summary>
''' <remarks>
''' Código del Modulo:    2
''' Código de la Opción:  63
''' </remarks>
Partial Class Modulo_Matricula_FichaFamilia
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 2
    Private cod_Opcion As Integer = 63

#Region "Eventos Generales"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Ficha de Familia")
            popup_btnAgregar_NuevoFamiliar.Attributes.Add("onclick", "abrirPopupFichaFamiliar('/SaintGeorgeOnline/Modulo_Matricula/frameFichaFamiliar.aspx');")
            popup_btnAgregar_NuevoAlumno.Attributes.Add("onclick", "abrirPopupFichaAlumno('/SaintGeorgeOnline/Modulo_Matricula/frameFichaAlumno.aspx');")

            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                ViewState("SortExpression") = "Descripcion"
                ViewState("Direccion") = "ASC"

                'popup_btnCancelar_IntegranteFamilia.Attributes.Add("OnClick", "return confirm_cancelar();")
                btnCancelar.Attributes.Add("OnClick", "return confirm_cancelar();")

                Listar()
                Listar_Familiares()
                Listar_Parentescos()
                Listar_Alumnos()
                cargarComboAnioAcademico()

            Else

                If Session("CodigoFamiliarRegistrado") IsNot Nothing Then

                    Dim int_CodigoFamiliar As Integer = Session("CodigoFamiliarRegistrado")

                    If int_CodigoFamiliar > 0 Then
                        Listar_Familiares()
                        ddl_Familiar_Popup.SelectedValue = int_CodigoFamiliar
                    End If

                    Session.Remove("CodigoFamiliarRegistrado")
                    Session("CodigoFamiliarRegistrado") = Nothing
                    ModalPopupExtender_IntegrantesFamilia.Show()

                End If

                If Session("CodigoAlumnoRegistrado") IsNot Nothing Then

                    Dim int_Codigoalumno As Integer = Session("CodigoAlumnoRegistrado")

                    If int_Codigoalumno > 0 Then
                        Listar_Alumnos()
                        ddl_Alumno_Popup.SelectedValue = int_Codigoalumno
                    End If

                    Session.Remove("CodigoAlumnoRegistrado")
                    Session("CodigoAlumnoRegistrado") = Nothing
                    ModalPopupExtender_AlumnosFamilia.Show()

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
        
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Listar()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelar_Click()

        miTab1.Enabled = True
        miTab2.Enabled = False
        lbTab2.Text = "Inserción"
        TabContainer1.ActiveTabIndex = 0
        tbBuscarDescripcion.Focus()

        hiddenCodigoFamilia.Value = 0

    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Grabar()
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            NuevaFamilia()
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Eventos de Grilla"

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
            ImagenSorting()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Then
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    obtener(codigo)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            'SETEO DE PERMISOS DE ACCIONES---------------

            '---------------------------------------------

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
            ImagenSorting()
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

            ImagenSorting()
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

#Region "Metodos Generales"

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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 63, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

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
        Me.Master.RegistrarAccesoPagina(2, 63)

        'CONTROLES DEL FORMULARIO
        Master.BloqueoControles(btnBuscar, 1)
        Master.BloqueoControles(btnGrabar, 1)

        Master.SeteoPermisosAcciones(btnBuscar, 63)
        Master.SeteoPermisosAcciones(btnGrabar, 63)

    End Sub

    ''' <summary>
    ''' Exporte el listado de la información filtradaen los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Exportar()

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

    ''' <summary>
    ''' Lista la relación de subbloques de menus.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Listar()

        Dim ds_lista As DataSet = ObtenerResultadoBusqueda(1)
        hfTotalRegs.Value = CInt(ds_lista.Tables(0).Rows.Count.ToString)

        GridView1.DataSource = ds_lista.Tables(0)
        GridView1.DataBind()

        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
        ImagenSorting()

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

        Dim str_Descripcion As String = tbBuscarDescripcion.Text.Trim()
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim bl_obj_Familia As New bl_Familia
            ds_Lista = bl_obj_Familia.FUN_LIS_Familias(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 63)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim bl_obj_Familia As New bl_Familia
                ds_Lista = bl_obj_Familia.FUN_LIS_Familias(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 63)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_lista
    End Function

    ''' <summary>
    ''' Carga la informacion en el seleccionable de familiares.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Listar_Familiares()

        Dim bl_obj_Familiares As New bl_Familiares
        Dim be_obj_MaestroPersona As New be_MaestroPersonas
        Dim ds_lista As DataSet
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        be_obj_MaestroPersona.EstadoPersona = 1
        be_obj_MaestroPersona.ApellidoPaterno = ""
        be_obj_MaestroPersona.ApellidoMaterno = ""
        be_obj_MaestroPersona.Nombre = ""

        be_obj_MaestroPersona.AlumnoFamiliarApellidoMaterno = ""
        be_obj_MaestroPersona.AlumnoFamiliarApellidoPaterno = ""
        be_obj_MaestroPersona.AlumnoFamiliarNombres = ""

        ds_lista = bl_obj_Familiares.FUN_LIS_Familiar(be_obj_MaestroPersona, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 63)
        Controles.llenarCombo(ddl_Familiar_Popup, ds_lista, "CodigoFamiliar", "NombreCompleto", False, True)

    End Sub

    ''' <summary>
    ''' Carga la informacion en el seleccionable de parentescos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Listar_Parentescos()
        Dim bl_obj_Parentesco As New bl_Parentescos
        Dim ds_lista As DataSet
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        ds_lista = bl_obj_Parentesco.FUN_LIS_Parentesco("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 63)
        Controles.llenarCombo(ddl_Parentesco_Popup, ds_lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga la informacion en el seleccionable de alumnos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    ''' 
    Private Sub Listar_Alumnos()

        Dim bl_obj_alumnos As New bl_Alumnos
        Dim ds_lista As DataSet
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim str_ApellidoPaterno As String = ""
        Dim str_ApellidoMaterno As String = ""
        Dim str_Nombre As String = ""
        Dim int_estadoAlumno As Integer = 0
        Dim int_Nivel As Integer = 0
        Dim int_SubNivel As Integer = 0
        Dim int_Grado As Integer = 0
        Dim int_Aula As Integer = 0
        Dim int_PeriodoInicio As Integer = 0
        Dim int_PeriodoFin As Integer = 0
        Dim int_Sede As Integer = 0

        ds_lista = bl_obj_alumnos.FUN_LIS_FichaAlumno("", str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_estadoAlumno, _
            int_Nivel, int_SubNivel, int_Grado, int_Aula, int_PeriodoInicio, int_PeriodoFin, int_Sede, _
            int_CodigoUsuario, int_CodigoTipoUsuario, 2, 63)
        Controles.llenarCombo(ddl_Alumno_Popup, ds_lista, "CodigoAlumno", "NombreCompleto", False, True)

    End Sub

    ''' <summary>
    ''' Obtiene la información sobre un registro y lo muestra en el formulario.
    ''' </summary>
    ''' <param name="int_Codigo">Codigo de familia</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub obtener(ByVal int_Codigo As String)

        Dim obj_BL_Familia As New bl_Familia
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Familia.FUN_GET_DatosFamilia(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        hiddenCodigoFamilia.Value = CInt(ds_Lista.Tables(0).Rows(0).Item("CodigoFamilia").ToString)
        lblNombreFamilia_Cab.Text = ds_Lista.Tables(0).Rows(0).Item("NombreFamilia").ToString
        tbNombreFamilia_Cab.Text = ds_Lista.Tables(0).Rows(0).Item("NombreFamilia").ToString

        VerRegistro("Actualización")

        ViewState("ListaFamiliares") = ds_Lista.Tables(1)
        ViewState("ListaAlumnos") = ds_Lista.Tables(2)
        'ViewState("HijosFamilia") = ds_Lista.Tables(3)

        gvListaFamiliares.DataSource = ds_Lista.Tables(1)
        gvListaFamiliares.DataBind()

        gvListaAlumnos.DataSource = ds_Lista.Tables(2)
        gvListaAlumnos.DataBind()

    End Sub

    ''' <summary>
    ''' Bloquea el formulario de busqueda cuando se selecciona la opción de Nuevo Registro.
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
        miTab2.Enabled = True
        lbTab2.Text = str_Modo
        TabContainer1.ActiveTabIndex = 1

    End Sub

    ''' <summary>
    ''' Registra los familiares a la familia seleccionada
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BL_Familia As New bl_Familia
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        Dim int_CodigoFamilia As Integer = hiddenCodigoFamilia.Value
        Dim str_NombreFamilia As String = tbNombreFamilia_Cab.Text.Trim

        Dim dtF, dtA As DataTable
        dtF = ViewState("ListaFamiliares")
        dtA = ViewState("ListaAlumnos")


        usp_valor = obj_BL_Familia.FUN_UPD_FamiliaIntegrantesYAlumnos(int_CodigoFamilia, str_NombreFamilia, dtF, dtA, _
            usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 63)

        If usp_valor >= 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

        btnCancelar_Click()
        Listar()

    End Sub

#End Region

#Region "Metodos del GriedView"

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

        Dim ds_lista As DataSet = ObtenerResultadoBusqueda(2)
        hfTotalRegs.Value = CInt(ds_lista.Tables(0).Rows.Count.ToString)

        Dim dv As New Data.DataView(ds_lista.Tables(0))
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
    Private Sub ImagenSorting()

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting"), ImageButton)

        If ViewState("Direccion") = "ASC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/DOWN_A.png"
            _btnSorting.ToolTip = "Descendente"
        ElseIf ViewState("Direccion") = "DESC" Then
            _btnSorting.ImageUrl = "~/App_Themes/Imagenes/UP_A.png"
            _btnSorting.ToolTip = "Ascendente"
        End If

    End Sub

#End Region

    'MANTENIMIENTO - INTEGRANTES DE LA FAMILIA
#Region "Mantenimiento - Integrantes de Familia"

#Region "Eventos"

    Protected Sub btn_Add_Integrante_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        ViewState("NuevoIntegrante") = True
        lbl_FamiliaPopUp.Text = lblNombreFamilia_Cab.Text
        ModalPopupExtender_IntegrantesFamilia.Show()

    End Sub

    Protected Sub popup_btnCancelar_IntegranteFamilia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        cerrarModalIntegranteFamilia()

    End Sub

    Protected Sub popup_btnAgregar_IntegranteFamilia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If ViewState("NuevoIntegrante") = False Then
            editarIntegranteFamilia()
        ElseIf ViewState("NuevoIntegrante") = True Then
            agregarIntegranteFamilia()
        End If

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Cierra el PopUp de Registro de Integrantes de la Familia
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalIntegranteFamilia()

        ModalPopupExtender_IntegrantesFamilia.Hide()

    End Sub

    ''' <summary>
    ''' Agrega un (1) familiar a la relación de integrantes de la familia seleccionada.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarIntegranteFamilia()

        If ddl_Familiar_Popup.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe ingresar un familiar.", "Alert")
            ModalPopupExtender_IntegrantesFamilia.Show()
            Exit Sub
        End If

        If ddl_Parentesco_Popup.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe ingresar un parentesco.", "Alert")
            ModalPopupExtender_IntegrantesFamilia.Show()
            Exit Sub
        End If

        Dim dtF As DataTable
        dtF = ViewState("ListaFamiliares")

        Dim int_CodFamiliar As Integer = ddl_Familiar_Popup.SelectedValue

        For Each dr As DataRow In dtF.Rows

            If dr.Item("CodigoFamiliar") = ddl_Familiar_Popup.SelectedValue And dr.Item("CodigoParentesco") = ddl_Parentesco_Popup.SelectedValue And dr.Item("Estado") = 1 Then

                MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                ModalPopupExtender_IntegrantesFamilia.Show()
                Exit Sub

            End If

            If dr.Item("CodigoFamiliar") = int_CodFamiliar And dr.Item("Estado") = 1 Then

                MostrarSexyAlertBox("El familiar seleccionado, ya se encuentra registrado.", "Alert")
                ModalPopupExtender_IntegrantesFamilia.Show()
                Exit Sub

            End If

            If dr.Item("CodigoParentesco") = ddl_Parentesco_Popup.SelectedValue And _
                (ddl_Parentesco_Popup.SelectedValue = 1 Or ddl_Parentesco_Popup.SelectedValue = 2) And dr.Item("Estado") = 1 Then

                MostrarSexyAlertBox("Ya se ha ingresado a una madre o padre en la familia", "Alert")
                ModalPopupExtender_IntegrantesFamilia.Show()
                Exit Sub

            End If

        Next

        Dim bool_Registrar As Boolean = True

        For Each dr As DataRow In dtF.Rows
            If dr.Item("CodigoFamiliar") = int_CodFamiliar And dr.Item("Estado") = 0 Then ' si existe el familiar y esta inactivo, lo activo
                dr.Item("CodigoParentesco") = ddl_Parentesco_Popup.SelectedValue
                dr.Item("Parentesco") = ddl_Parentesco_Popup.SelectedItem.Text
                dr.Item("Estado") = 1
                bool_Registrar = False
                Exit Sub
            End If
        Next

        If bool_Registrar Then
            Dim drF As DataRow
            drF = dtF.NewRow
            drF.Item("idx") = 0
            drF.Item("CodigoIntegranteFamilia") = 0
            drF.Item("CodigoFamilia") = hiddenCodigoFamilia.Value
            drF.Item("CodigoFamiliar") = ddl_Familiar_Popup.SelectedValue
            drF.Item("CodigoParentesco") = ddl_Parentesco_Popup.SelectedValue
            drF.Item("DescripcionFamilia") = lblNombreFamilia_Cab.Text
            drF.Item("NombreFamiliar") = ddl_Familiar_Popup.SelectedItem.Text
            drF.Item("Parentesco") = ddl_Parentesco_Popup.SelectedItem.Text
            drF.Item("Tipo") = "T"
            drF.Item("Estado") = 1
            dtF.Rows.Add(drF)
        End If

        ViewState("ListaFamiliares") = dtF

        Dim dv As DataView = dtF.DefaultView
        dv.RowFilter = "1=1 and Estado = " & 1 ' Solo activos

        gvListaFamiliares.DataSource = dv
        gvListaFamiliares.DataBind()
        UpdatePanel_ListaFamiliares.Update()

    End Sub

    ''' <summary>
    ''' Modifica la información de la vinculacion del familiar con la familia seleccionada.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarIntegranteFamilia()

        If ddl_Familiar_Popup.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe ingresar un familiar.", "Alert")
            ModalPopupExtender_IntegrantesFamilia.Show()
            Exit Sub
        End If

        If ddl_Parentesco_Popup.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe ingresar un parentesco.", "Alert")
            ModalPopupExtender_IntegrantesFamilia.Show()
            Exit Sub
        End If

        Dim dtF As DataTable
        dtF = ViewState("ListaFamiliares")

        Dim int_CodFamiliar As Integer = hiddenCodigoFamiliar.Value

        For Each dr As DataRow In dtF.Rows

            If dr.Item("CodigoFamiliar") <> int_CodFamiliar Then

                If dr.Item("CodigoFamiliar") = ddl_Familiar_Popup.SelectedValue And dr.Item("CodigoParentesco") = ddl_Parentesco_Popup.SelectedValue Then

                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    ModalPopupExtender_IntegrantesFamilia.Show()
                    Exit Sub

                End If

                If dr.Item("CodigoFamiliar") = int_CodFamiliar Then

                    MostrarSexyAlertBox("El familiar seleccionado, ya se encuentra registrado.", "Alert")
                    ModalPopupExtender_IntegrantesFamilia.Show()
                    Exit Sub

                End If

                If dr.Item("CodigoParentesco") = ddl_Parentesco_Popup.SelectedValue And _
                    (ddl_Parentesco_Popup.SelectedValue = 1 Or ddl_Parentesco_Popup.SelectedValue = 2) Then

                    MostrarSexyAlertBox("Ya se ha ingresado a una madre o padre en la familia", "Alert")
                    ModalPopupExtender_IntegrantesFamilia.Show()
                    Exit Sub

                End If

            End If

        Next

        For Each dr As DataRow In dtF.Rows

            If dr.Item("CodigoFamiliar") = int_CodFamiliar Then

                dr.Item("CodigoFamiliar") = ddl_Familiar_Popup.SelectedValue
                dr.Item("CodigoParentesco") = ddl_Parentesco_Popup.SelectedValue
                dr.Item("NombreFamiliar") = ddl_Familiar_Popup.SelectedItem.Text
                dr.Item("Parentesco") = ddl_Parentesco_Popup.SelectedItem.Text

            End If

        Next

        ViewState("ListaIntegrantes") = dtF

        Dim dv As DataView = dtF.DefaultView
        dv.RowFilter = "1=1 and Estado = " & 1 ' Solo activos

        gvListaFamiliares.DataSource = dv
        gvListaFamiliares.DataBind()
        UpdatePanel_ListaFamiliares.Update()

    End Sub

    ''' <summary>
    ''' Obtiene la información de un integrante de la familia y la muestra en el PopUp de Integrantes de la familia.
    ''' </summary>
    ''' <param name="int_CodigoIntegranteFamilia"></param>
    ''' <param name="int_CodigoFamilia">Codigo del familia</param>
    ''' <param name="int_CodigoFamiliar">Codigo del familiar</param>
    ''' <param name="int_CodigoParentesco">Codigo del parentesco</param>
    ''' <remarks></remarks>
    Private Sub mostrarIntegranteFamilia(ByVal int_CodigoIntegranteFamilia As Integer, _
                                        ByVal int_CodigoFamilia As Integer, _
                                        ByVal int_CodigoFamiliar As Integer, _
                                        ByVal int_CodigoParentesco As Integer)

        hiddenCodigoFamiliar.Value = int_CodigoFamiliar

        lbl_FamiliaPopUp.Text = lblNombreFamilia_Cab.Text
        ddl_Familiar_Popup.SelectedValue = int_CodigoFamiliar
        ddl_Parentesco_Popup.SelectedValue = int_CodigoParentesco
        ModalPopupExtender_IntegrantesFamilia.Show()

    End Sub

    Private Sub EliminarIntegranteFamilia(ByVal int_CodigoIntegranteFamilia As Integer, _
                                        ByVal int_CodigoFamilia As Integer, _
                                        ByVal int_CodigoFamiliar As Integer, _
                                        ByVal int_CodigoParentesco As Integer)

        'consulto que ningun alumno tenga como responsable de pago al familiar que deseo eliminar       
        Dim dtA As DataTable
        dtA = ViewState("ListaAlumnos")

        Dim str_NombreAlumno, str_NombreFamiliar As String


        For Each drA As DataRow In dtA.Rows
            If drA.Item("CodigoFamiliarResponsablePago") = int_CodigoFamiliar Then

                str_NombreAlumno = drA.Item("NombreAlumno")
                str_NombreFamiliar = drA.Item("NombreFamiliarResponsablePago")
                MostrarSexyAlertBox("No se puede eliminar al familiar <em>" & str_NombreFamiliar & "</em>, ya que es responsable de pago del alumno(a) <em>" & str_NombreAlumno & "</em>.", "Alert")
                Exit Sub

            End If
        Next



        Dim dtF As DataTable
        dtF = ViewState("ListaFamiliares")

        For Each drF As DataRow In dtF.Rows

            If drF.Item("CodigoFamiliar") = int_CodigoFamiliar Then
                drF.Item("Estado") = 0
                Exit For
            End If

        Next

        Dim dv As DataView = dtF.DefaultView
        dv.RowFilter = "1=1 and Estado = " & 1 ' Solo activos

        ViewState("ListaFamiliares") = dtF
        gvListaFamiliares.DataSource = dv
        gvListaFamiliares.DataBind()

    End Sub

#End Region

#Region "Eventos de Grilla"

    Protected Sub gvListaFamiliares_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim int_CodigoIntegranteFamilia As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then

                    int_CodigoAccion = 6
                    ViewState("NuevoIntegrante") = False
                    mostrarIntegranteFamilia(CType(row.FindControl("lblCodigoIntegranteFamilia"), Label).Text, _
                                            CType(row.FindControl("lblCodigoFamilia"), Label).Text, _
                                            CType(row.FindControl("lblCodigoFamiliar"), Label).Text, _
                                            CType(row.FindControl("lblCodigoParentesco"), Label).Text)

                ElseIf e.CommandName = "Eliminar" Then

                    int_CodigoAccion = 3
                    'EliminarIntegrante(CType(row.FindControl("lblID_TABLA_grilla"), Label).Text)
                    EliminarIntegranteFamilia(CType(row.FindControl("lblCodigoIntegranteFamilia"), Label).Text, _
                                            CType(row.FindControl("lblCodigoFamilia"), Label).Text, _
                                            CType(row.FindControl("lblCodigoFamiliar"), Label).Text, _
                                            CType(row.FindControl("lblCodigoParentesco"), Label).Text)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub gvListaFamiliares_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
       
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

        End If

    End Sub

#End Region

#End Region

    'MANTENIMIENTO - ALUMNOS DE LA FAMILIA
#Region "Mantenimiento - Alumnos de Familia"

#Region "Eventos"

    Protected Sub btn_Add_Alumno_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        ViewState("NuevoAlumno") = True
        lbl_FamiliaPopUp2.Text = lblNombreFamilia_Cab.Text

        Dim dt_Familiares As DataTable = CType(ViewState("ListaFamiliares"), DataTable)
        Dim ds_Familiares As New DataSet
        ds_Familiares.Tables.Add(dt_Familiares)
        Controles.llenarCombo(ddl_FamiliarParentesco_Popup, ds_Familiares, "CodigoFamiliar", "NombreFamiliar", False, True)

        ModalPopupExtender_AlumnosFamilia.Show()

    End Sub

    Protected Sub popup_btnCancelar_AlumnoFamilia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        cerrarModalAlumnoFamilia()

    End Sub

    Protected Sub popup_btnAgregar_AlumnoFamilia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If ViewState("NuevoAlumno") = False Then
            editarAlumnoFamilia()
        ElseIf ViewState("NuevoAlumno") = True Then
            agregarAlumnoFamilia()
        End If

    End Sub

#End Region

#Region "Metodos"

    Private Sub cerrarModalAlumnoFamilia()

        ModalPopupExtender_AlumnosFamilia.Hide()

    End Sub

    Private Sub agregarAlumnoFamilia()

        If ddl_Alumno_Popup.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un alumno.", "Alert")
            ModalPopupExtender_AlumnosFamilia.Show()
            Exit Sub
        End If

        If ddl_FamiliarParentesco_Popup.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un familiar responsable de pago.", "Alert")
            ModalPopupExtender_AlumnosFamilia.Show()
            Exit Sub
        End If

        Dim dtA As DataTable
        dtA = ViewState("ListaAlumnos")

        Dim int_CodAlumno As Integer = ddl_Alumno_Popup.SelectedValue

        For Each dr As DataRow In dtA.Rows

            If dr.Item("CodigoAlumno") = ddl_Alumno_Popup.SelectedValue And dr.Item("Estado") = 1 Then

                MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                ModalPopupExtender_AlumnosFamilia.Show()
                Exit Sub

            End If

        Next

        Dim bool_Registrar As Boolean = True

        For Each dr As DataRow In dtA.Rows
            If dr.Item("CodigoAlumno") = int_CodAlumno And dr.Item("Estado") = 0 Then ' si existe el alumno y esta inactivo, lo activo                
                dr.Item("Estado") = 1
                bool_Registrar = False
                Exit Sub
            End If
        Next

        If bool_Registrar Then
            Dim drA As DataRow
            drA = dtA.NewRow
            drA.Item("idx") = 0
            drA.Item("CodigoAlumno") = ddl_Alumno_Popup.SelectedValue
            drA.Item("NombreAlumno") = ddl_Alumno_Popup.SelectedItem.Text

            drA.Item("CodigoFamiliarResponsablePago") = ddl_FamiliarParentesco_Popup.SelectedValue
            drA.Item("NombreFamiliarResponsablePago") = ddl_FamiliarParentesco_Popup.SelectedItem.Text

            drA.Item("Tipo") = "T"
            drA.Item("Estado") = 1
            dtA.Rows.Add(drA)
        End If

        ViewState("ListaAlumnos") = dtA

        Dim dv As DataView = dtA.DefaultView
        dv.RowFilter = "1=1 and Estado = " & 1 ' Solo activos

        gvListaAlumnos.DataSource = dv
        gvListaAlumnos.DataBind()
        UpdatePanel_ListaAlumnos.Update()

    End Sub

    Private Sub editarAlumnoFamilia()

        If ddl_Alumno_Popup.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe ingresar un alumno.", "Alert")
            ModalPopupExtender_AlumnosFamilia.Show()
            Exit Sub
        End If

        If ddl_FamiliarParentesco_Popup.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un familiar responsable de pago.", "Alert")
            ModalPopupExtender_AlumnosFamilia.Show()
            Exit Sub
        End If

        Dim dtA As DataTable
        dtA = ViewState("ListaAlumnos")

        Dim int_CodAlumno As Integer = hiddenCodigoAlumno.Value

        For Each dr As DataRow In dtA.Rows

            If dr.Item("CodigoAlumno") <> int_CodAlumno Then

                If dr.Item("CodigoAlumno") = ddl_Alumno_Popup.SelectedValue Then

                    MostrarSexyAlertBox("Este registro ya existe.", "Alert")
                    ModalPopupExtender_IntegrantesFamilia.Show()
                    Exit Sub

                End If

            End If

        Next

        For Each dr As DataRow In dtA.Rows

            If dr.Item("CodigoAlumno") = int_CodAlumno Then

                dr.Item("CodigoAlumno") = ddl_Alumno_Popup.SelectedValue
                dr.Item("NombreAlumno") = ddl_Alumno_Popup.SelectedItem.Text

                dr.Item("CodigoFamiliarResponsablePago") = ddl_FamiliarParentesco_Popup.SelectedValue
                dr.Item("NombreFamiliarResponsablePago") = ddl_FamiliarParentesco_Popup.SelectedItem.Text

            End If

        Next

        ViewState("ListaAlumnos") = dtA

        Dim dv As DataView = dtA.DefaultView
        dv.RowFilter = "1=1 and Estado = " & 1 ' Solo activos

        gvListaAlumnos.DataSource = dv
        gvListaAlumnos.DataBind()
        UpdatePanel_ListaAlumnos.Update()

    End Sub

    Private Sub mostrarAlumnoFamilia(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoApoderadoFamiliar As Integer)

        Dim dt_Familiares As DataTable = CType(ViewState("ListaFamiliares"), DataTable)
        Dim ds_Familiares As New DataSet
        ds_Familiares.Tables.Add(dt_Familiares)
        Controles.llenarCombo(ddl_FamiliarParentesco_Popup, ds_Familiares, "CodigoFamiliar", "NombreFamiliar", False, True)

        hiddenCodigoAlumno.Value = int_CodigoAlumno
        lbl_FamiliaPopUp2.Text = lblNombreFamilia_Cab.Text
        ddl_Alumno_Popup.SelectedValue = int_CodigoAlumno
        ddl_FamiliarParentesco_Popup.SelectedValue = int_CodigoApoderadoFamiliar
        ModalPopupExtender_AlumnosFamilia.Show()

    End Sub

    Private Sub EliminarAlumnoFamilia(ByVal int_CodigoAlumno As Integer)

        Dim dtA As DataTable
        dtA = ViewState("ListaAlumnos")

        For Each drA As DataRow In dtA.Rows

            If drA.Item("CodigoAlumno") = int_CodigoAlumno Then
                drA.Item("Estado") = 0
                Exit For
            End If

        Next

        Dim dv As DataView = dtA.DefaultView
        dv.RowFilter = "1=1 and Estado = " & 1 ' Solo activos

        ViewState("ListaAlumnos") = dtA
        gvListaAlumnos.DataSource = dv
        gvListaAlumnos.DataBind()

    End Sub

#End Region

#Region "Eventos de Grilla"

    Protected Sub gvListaAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim int_CodigoAlumnoFamilia As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then

                    int_CodigoAccion = 6

                    Dim lblCodigoFamiliarResponsablePago As Label = CType(row.FindControl("lblCodigoFamiliarResponsablePago"), Label)
                    Dim lblCodigoAlumno As Label = CType(row.FindControl("lblCodigoAlumno"), Label)

                    mostrarAlumnoFamilia(Val(lblCodigoAlumno.Text), _
                                         Val(lblCodigoFamiliarResponsablePago.Text))

                    ViewState("NuevoAlumno") = False

                ElseIf e.CommandName = "Eliminar" Then

                    int_CodigoAccion = 3
                    EliminarAlumnoFamilia(int_CodigoAlumnoFamilia)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub gvListaAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")
            btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")


        End If

    End Sub

#End Region

#End Region

    'REGISTRO - FAMILIA
#Region "Registro de Familia"

#Region "Eventos"

    Protected Sub btnVerificar_IngresarFamilia_Click()
        Try
            Dim usp_mensaje As String = ""
            If validarGrabarFamiliaNueva(usp_mensaje) Then
                VerificarFamiliaNueva()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
            ModalPopupExtender_IngresarFamilia.Show()
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_IngresarFamilia_Click()
        Try
            Dim usp_mensaje As String = ""
            If validarGrabarFamiliaNueva(usp_mensaje) Then
                GrabarFamiliaNueva()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
                ModalPopupExtender_IngresarFamilia.Show()
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCerrar_IngresarFamilia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        ModalPopupExtender_IngresarFamilia.Hide()

    End Sub

    Protected Sub popup_btnAgregar_NuevoFamiliar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        ModalPopupExtender_IntegrantesFamilia.Show()
        Session("CodigoFamiliarRegistrado") = 0


        Dim str_Script As String = ""
        str_Script = "window.showModalDialog('/SaintGeorgeOnline/Modulo_Matricula/frameFichaFamiliar.aspx', '#1', " _
                    + "'dialogHeight: 500px ; dialogWidth: 870px; center: Yes; help: No; resizable: No; status: No; scroll: Yes;');"
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", str_Script, True)


        'ModalPopupExtender_IngresarFamiliar.Show()
        'Iframe1.Attributes.Add("src", "frameFichaFamiliar.aspx")

    End Sub

    Protected Sub btnCerrar_IngresarFamiliar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        ModalPopupExtender_IntegrantesFamilia.Show()

    End Sub


#End Region

#Region "Metodos"

    Private Sub cargarComboAnioAcademico()

        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlAnioAcademicoIngreso, ds_Lista, "Codigo", "Descripcion", False, False)

        ddlAnioAcademicoIngreso.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar

    End Sub

    Private Sub NuevaFamilia()

        ModalPopupExtender_IngresarFamilia.Show()
        btnGrabar_IngresarFamilia.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_0.png"
        btnGrabar_IngresarFamilia.Enabled = False
        limpiarModal()

    End Sub

    Private Function validarGrabarFamiliaNueva(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbNombreFamilia.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Nombre de Familia")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub GrabarFamiliaNueva()

        Dim obj_BL_Familia As New bl_Familia
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim str_NombreFamilia As String = ""
        str_NombreFamilia = tbNombreFamilia.Text.Trim

        Dim str_AnioAcademico As String = ""
        str_AnioAcademico = ddlAnioAcademicoIngreso.SelectedItem.ToString

        usp_valor = obj_BL_Familia.FUN_INS_Familia(str_NombreFamilia, str_AnioAcademico, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor >= 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            Listar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
            ModalPopupExtender_IngresarFamilia.Show()
        End If

    End Sub

    Private Sub VerificarFamiliaNueva()

        Dim obj_BL_Familia As New bl_Familia
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim str_NombreFamilia As String = ""
        str_NombreFamilia = tbNombreFamilia.Text.Trim

        Dim ds_Lista As DataSet = obj_BL_Familia.FUN_LIS_Familias(str_NombreFamilia, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        GridView2.DataSource = ds_Lista.Tables(0)
        GridView2.DataBind()

        btnGrabar_IngresarFamilia.Enabled = True
        btnGrabar_IngresarFamilia.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_1.png"

    End Sub

    Private Sub limpiarModal()

        tbNombreFamilia.Text = ""
        GridView2.DataBind()

    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Seleccionar" Then

                Dim CodigoFamilia As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                int_CodigoAccion = 6
                obtener(CodigoFamilia)

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

#End Region

End Class

