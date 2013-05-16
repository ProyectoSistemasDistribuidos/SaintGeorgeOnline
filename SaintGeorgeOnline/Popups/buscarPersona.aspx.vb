Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloLogueo
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_Utilities

Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Web.Services
Imports System.Configuration.ConfigurationManager

''' <summary>
''' Modulo de Mantenimiento de Enfermedades
''' </summary>
''' <remarks>
''' Código del Modulo:    
''' Código de la Opción:  
''' </remarks>
Partial Class Popups_buscarPersona
    Inherits System.Web.UI.Page

#Region "Eventos"

#Region "Eventos Generales"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                btnCerrar.Attributes.Add("onclick", "return cerrar();")
                cargarComboTipoPersona()
                cargarComboSede()

                ViewState("SortExpression") = "NombreCompleto"
                ViewState("Direccion") = "ASC"

                'Tipo Persona - Todos : 0 / Alumno : 1 / Personal : 2 / Familiar : 3 / Otros : 4
                If Request.QueryString("Tipo") IsNot Nothing Then

                    Dim tipo As Integer = CInt(Request.QueryString("tipo").ToString)
                    ddlBuscarTipoPersona.SelectedValue = tipo
                    hiddenTipoPersona.Value = tipo

                    If tipo = 1 Then
                        ddlBuscarTipoPersona.Enabled = False
                        tipoBusqueda()
                        FSParametrosAlumno.Visible = True
                        FSParametrosFamiliar.Visible = True
                    ElseIf tipo = 2 Then
                        ddlBuscarTipoPersona.Enabled = False
                    ElseIf tipo = 3 Then
                        ddlBuscarTipoPersona.Enabled = False
                        tipoBusqueda()
                    End If

                End If

                'Ficha Atencion 
                'tbPadre - Paciente : 1 / Envia : 2 / Recoje : 3 (Ficha Atencion)
                If Request.QueryString("Padre") IsNot Nothing Then

                    Dim Padre As String = CStr(Request.QueryString("Padre").ToString)
                    hiddenPadre.Value = Padre

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

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""

            If validarRegistro(usp_mensaje) Then
                Registrar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
    End Sub

#End Region

#Region "Eventos de los Combos"

    Protected Sub ddlBuscarTipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            tipoBusqueda()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAlumnoNiveles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlAlumnoSubniveles)
            limpiarCombos(ddlAlumnoGrados)
            limpiarCombos(ddlAlumnoAulas)
            cargarComboAlumnoSubNivel()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAlumnoSubniveles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlAlumnoGrados)
            limpiarCombos(ddlAlumnoAulas)
            cargarComboAlumnoGrado()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlAlumnoGrados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlAlumnoAulas)
            cargarComboAlumnoAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlFamiliarAlumnoNiveles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlFamiliarAlumnoSubniveles)
            limpiarCombos(ddlFamiliarAlumnoGrados)
            limpiarCombos(ddlFamiliarAlumnoAulas)
            cargarComboFamiliarAlumnoSubNivel()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlFamiliarAlumnoSubniveles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlFamiliarAlumnoGrados)
            limpiarCombos(ddlFamiliarAlumnoAulas)
            cargarComboFamiliarAlumnoGrado()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlFamiliarAlumnoGrados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            limpiarCombos(ddlFamiliarAlumnoAulas)
            cargarComboFamiliarAlumnoAulas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
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

                Dim objMaestroPersona As New be_MaestroPersonas

                objMaestroPersona.CodigoAlumno = codigo
                'objMaestroPersona.NombreCompleto = 'row.Cells(2).Text
                objMaestroPersona.NombreCompleto = CType(row.FindControl("Label1"), Label).Text

                objMaestroPersona.Edad = row.Cells(3).Text
                objMaestroPersona.DescTipoPersona = row.Cells(4).Text

                objMaestroPersona.CodigoPersona = CType(row.FindControl("lbCodigoPersona"), Label).Text
                objMaestroPersona.CodigoTipoPersona = CType(row.FindControl("lbCodigoTipoPaciente"), Label).Text
                objMaestroPersona.RutaFoto = CType(row.FindControl("lbRutaFoto"), Label).Text
                objMaestroPersona.NSnGS = CType(row.FindControl("lbNSnGS"), Label).Text
                objMaestroPersona.CodigoGrado = CType(row.FindControl("lbCodigoGrado"), Label).Text

                Session("PersonaPopup") = objMaestroPersona
                Page.Session("ResetearPadre") = True

                'Dim str_PaginaRetorno As String = ""
                'str_PaginaRetorno = CStr(hiddenPadre.Value)

                'If str_PaginaRetorno = "paciente" Or str_PaginaRetorno = "envia" Or str_PaginaRetorno = "recoje" Then ' Ficha Atencion
                '    Session("FichaAtencionTipobusqueda") = str_PaginaRetorno
                'ElseIf str_PaginaRetorno = "matricula" Or str_PaginaRetorno = "director" Then ' Sede Colegios
                '    Session("SedeColegioTipoBusqueda") = str_PaginaRetorno
                'End If

                Session("FichaAtencionTipobusqueda") = CStr(hiddenPadre.Value)
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

#Region "Genericos"

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
        Dim _btnSorting_d1 As ImageButton = CType(gridView.HeaderRow.FindControl("btnSorting_NombreCompleto"), ImageButton)
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

#End Region

#Region "Personalizados"

    Protected Sub GVListaAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Then
                int_CodigoAccion = 5
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                Dim objMaestroPersona As New be_MaestroPersonas

                objMaestroPersona.CodigoAlumno = codigo
                'objMaestroPersona.NombreCompleto = row.Cells(2).Text
                objMaestroPersona.NombreCompleto = CType(row.FindControl("Label1"), Label).Text
                objMaestroPersona.Edad = row.Cells(4).Text
                objMaestroPersona.DescTipoPersona = row.Cells(5).Text
                objMaestroPersona.NSnGS = row.Cells(6).Text

                objMaestroPersona.CodigoPersona = CType(row.FindControl("lbCodigoPersona"), Label).Text
                objMaestroPersona.CodigoTipoPersona = CType(row.FindControl("lbCodigoTipoPaciente"), Label).Text
                objMaestroPersona.CodigoTipoSangre = CType(row.FindControl("lbCodigoTipoSangre"), Label).Text
                objMaestroPersona.DescTipoSangre = CType(row.FindControl("lbDescTipoSangre"), Label).Text
                objMaestroPersona.RutaFoto = CType(row.FindControl("lbRutaFoto"), Label).Text
                objMaestroPersona.CodigoGrado = CType(row.FindControl("lbCodigoGrado"), Label).Text

                Session("PersonaPopup") = objMaestroPersona
                Page.Session("ResetearPadre") = True
                Session("FichaAtencionTipobusqueda") = CStr(hiddenPadre.Value)
                Cerrar()

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaPersonal_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Then
                int_CodigoAccion = 5
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                Dim objMaestroPersona As New be_MaestroPersonas

                'objMaestroPersona.CodigoTrabajador = codigo
                'objMaestroPersona.NombreCompleto = row.Cells(2).Text
                objMaestroPersona.NombreCompleto = CType(row.FindControl("Label1"), Label).Text
                objMaestroPersona.Edad = row.Cells(3).Text
                objMaestroPersona.DescTipoPersona = row.Cells(4).Text

                objMaestroPersona.CodigoPersona = CType(row.FindControl("lbCodigoPersona"), Label).Text
                objMaestroPersona.CodigoTipoPersona = CType(row.FindControl("lbCodigoTipoPaciente"), Label).Text
                objMaestroPersona.RutaFoto = CType(row.FindControl("lbRutaFoto"), Label).Text

                Session("PersonaPopup") = objMaestroPersona
                Page.Session("ResetearPadre") = True

                Dim str_PaginaRetorno As String = ""
                str_PaginaRetorno = hiddenPadre.Value

                If str_PaginaRetorno = "paciente" Or str_PaginaRetorno = "envia" Or str_PaginaRetorno = "recoje" Then ' Ficha Atencion
                    Session("FichaAtencionTipobusqueda") = str_PaginaRetorno
                ElseIf str_PaginaRetorno = "matricula" Or str_PaginaRetorno = "directorGeneral" Or str_PaginaRetorno = "directorNacional" Or str_PaginaRetorno = "subdirector" Then ' Sede Colegios
                    Session("SedeColegioTipoBusqueda") = str_PaginaRetorno
                ElseIf str_PaginaRetorno = "tutor" Or str_PaginaRetorno = "ResponsableActa" Or str_PaginaRetorno = "ResponsableSalon" Then ' Relacion AnioAcademico - Aulas
                    Session("TutorAulaTipoBusqueda") = str_PaginaRetorno
                ElseIf str_PaginaRetorno = "JefeDepartamento" Then ' Mant. Departamento Academicos
                    Session("JefeDepartamentoTipoBusqueda") = str_PaginaRetorno
                End If

                Cerrar()

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaFamiliar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Then
                int_CodigoAccion = 5
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                Dim objMaestroPersona As New be_MaestroPersonas

                objMaestroPersona.NombreCompleto = CType(row.FindControl("Label1"), Label).Text
                objMaestroPersona.Edad = row.Cells(3).Text
                objMaestroPersona.DescTipoPersona = row.Cells(4).Text

                objMaestroPersona.CodigoPersona = CType(row.FindControl("lbCodigoPersona"), Label).Text
                objMaestroPersona.CodigoTipoPersona = CType(row.FindControl("lbCodigoTipoPaciente"), Label).Text
                objMaestroPersona.RutaFoto = CType(row.FindControl("lbRutaFoto"), Label).Text

                Session("PersonaPopup") = objMaestroPersona
                Page.Session("ResetearPadre") = True

                Dim str_PaginaRetorno As String = ""
                str_PaginaRetorno = hiddenPadre.Value

                If str_PaginaRetorno = "paciente" Or str_PaginaRetorno = "envia" Or str_PaginaRetorno = "recoje" Then ' Ficha Atencion
                    Session("FichaAtencionTipoBusqueda") = CStr(hiddenPadre.Value)
                ElseIf str_PaginaRetorno = "personacompromisopago" Then
                    Session("ResponsablePagoTipoBusqueda") = str_PaginaRetorno
                End If

                Cerrar()

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GVListaOtros_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Seleccionar" Then
                int_CodigoAccion = 5
                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                Dim objMaestroPersona As New be_MaestroPersonas

                'objMaestroPersona.CodigoOtros = codigo
                'objMaestroPersona.NombreCompleto = row.Cells(2).Text
                objMaestroPersona.NombreCompleto = CType(row.FindControl("Label1"), Label).Text
                objMaestroPersona.Edad = row.Cells(3).Text
                objMaestroPersona.DescTipoPersona = row.Cells(4).Text

                objMaestroPersona.CodigoPersona = CType(row.FindControl("lbCodigoPersona"), Label).Text
                objMaestroPersona.CodigoTipoPersona = CType(row.FindControl("lbCodigoTipoPaciente"), Label).Text
                objMaestroPersona.RutaFoto = CType(row.FindControl("lbRutaFoto"), Label).Text

                Session("PersonaPopup") = objMaestroPersona
                Page.Session("ResetearPadre") = True
                Session("FichaAtencionTipoBusqueda") = CStr(hiddenPadre.Value)
                Cerrar()

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub
#End Region

#End Region

#End Region

#Region "Metodos"

#Region "Metodos Generales"

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

        ddlBuscarTipoPersona.SelectedValue = 0
        tbApellidoPaterno.Text = ""
        tbApellidoMaterno.Text = ""
        tbNombre.Text = ""
        ddlAlumnoNiveles.SelectedValue = 0
        ddlAlumnoSubniveles.SelectedValue = 0
        ddlAlumnoGrados.SelectedValue = 0
        ddlAlumnoAulas.SelectedValue = 0
        tbFamiliarAlumnoApellidoPaterno.Text = ""
        tbFamiliarAlumnoApellidoMaterno.Text = ""
        tbFamiliarAlumnoNombre.Text = ""
        ddlFamiliarAlumnoNiveles.SelectedValue = 0
        ddlFamiliarAlumnoSubniveles.SelectedValue = 0
        ddlFamiliarAlumnoGrados.SelectedValue = 0
        ddlFamiliarAlumnoAulas.SelectedValue = 0
        ddlBuscarSede.SelectedValue = 0

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
    ''' Muestra los diferentes filtros de búsqueda según el tipo de persona a encontrar.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub tipoBusqueda()

        If ddlBuscarTipoPersona.SelectedValue = 1 Then
            FSParametrosAlumno.Visible = True
            cargarCombosAlumno()
        Else
            FSParametrosAlumno.Visible = False
            limpiarCombosAlumno()
        End If

        If ddlBuscarTipoPersona.SelectedValue = 3 Then
            FSParametrosFamiliar.Visible = True
            cargarCombosFamiliarAlumno()
        Else
            FSParametrosFamiliar.Visible = False
            limpiarCombosFamiliarAlumno()
        End If

        If ddlBuscarTipoPersona.SelectedValue = 4 Then
            btnRegistrar.Visible = True
        Else
            btnRegistrar.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Realiza las validaciones del registro de usuarios del tipo OTROS.
    ''' </summary>
    ''' <param name="str_Mensaje"></param>
    ''' <returns>Retorna el indicador si el registro posse validaciones a realizarse.</returns>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validarRegistro(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbNombre.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Nombre")
            result = False
        End If

        If tbApellidoPaterno.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Apellido Paterno")
            result = False
        End If

        str_Mensaje = str_alertas

        Return result

    End Function

    ''' <summary>
    ''' Registra el tipo de personas OTROS.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Registrar()

        Dim obj_BL_MaestroPersonas As New bl_MaestroPersonas
        Dim obj_BE_Otros As New be_Otros

        obj_BE_Otros.Nombre = tbNombre.Text.Trim
        obj_BE_Otros.ApellidoPaterno = tbApellidoPaterno.Text.Trim
        obj_BE_Otros.ApellidoMaterno = tbApellidoMaterno.Text.Trim

        Dim usp_valor As Integer
        Dim usp_mensaje As String = ""
        Dim usp_CodigoOtros As Integer
        Dim usp_CodigoPersona As Integer

        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        usp_valor = obj_BL_MaestroPersonas.FUN_INS_Otros(obj_BE_Otros, usp_CodigoOtros, usp_CodigoPersona, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)

        If usp_valor > 0 Then

            'MostrarSexyAlertBox(usp_mensaje, "Info")

            Dim objMaestroPersona As New be_MaestroPersonas

            objMaestroPersona.CodigoOtros = usp_CodigoOtros
            objMaestroPersona.NombreCompleto = obj_BE_Otros.ApellidoPaterno & _
                                                IIf(obj_BE_Otros.ApellidoMaterno.Length = 0, "", " " & obj_BE_Otros.ApellidoMaterno) & _
                                                " , " & obj_BE_Otros.Nombre
            objMaestroPersona.Edad = 0
            objMaestroPersona.DescTipoPersona = "Otros"

            objMaestroPersona.CodigoPersona = usp_CodigoPersona
            objMaestroPersona.CodigoTipoPersona = 4
            objMaestroPersona.RutaFoto = "/SaintGeorgeOnline/Fotos/noPhotoMsg.gif"

            Session("PersonaPopup") = objMaestroPersona
            Page.Session("ResetearPadre") = True
            Session("FichaAtencionTipoBusqueda") = CStr(hiddenPadre.Value)
            Cerrar()

            'btnCancelar_Click()
            'limpiarCampos()
            'Listar()
        Else

            MostrarSexyAlertBox(usp_mensaje, "Alert")

        End If

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

        Dim sender As New Object

        If ddlBuscarTipoPersona.SelectedValue = 0 Then 'busqueda de todos            
            GVListaTodos.DataSource = ds_Lista.Tables(0)
            GVListaTodos.DataBind()
            GVListaTodos.Visible = True

            sender = GVListaTodos

        ElseIf ddlBuscarTipoPersona.SelectedValue = 1 Then 'busqueda de alumnos
            GVListaAlumnos.DataSource = ds_Lista.Tables(0)
            GVListaAlumnos.DataBind()
            GVListaAlumnos.Visible = True

            sender = GVListaAlumnos

        ElseIf ddlBuscarTipoPersona.SelectedValue = 2 Then ' busqueda de personal
            GVListaPersonal.DataSource = ds_Lista.Tables(0)
            GVListaPersonal.DataBind()
            GVListaPersonal.Visible = True

            sender = GVListaPersonal

        ElseIf ddlBuscarTipoPersona.SelectedValue = 3 Then ' busqueda de familia
            GVListaFamiliar.DataSource = ds_Lista.Tables(0)
            GVListaFamiliar.DataBind()
            GVListaFamiliar.Visible = True

            sender = GVListaFamiliar

        ElseIf ddlBuscarTipoPersona.SelectedValue = 4 Then ' busqueda de otros
            GVListaOtros.DataSource = ds_Lista.Tables(0)
            GVListaOtros.DataBind()
            GVListaOtros.Visible = True

            sender = GVListaOtros

        End If

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

        Dim obj_BE_MaestroPersonas As New be_MaestroPersonas
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        GVListaTodos.Visible = False
        GVListaAlumnos.Visible = False
        GVListaPersonal.Visible = False
        GVListaFamiliar.Visible = False
        GVListaOtros.Visible = False

        obj_BE_MaestroPersonas.CodigoTipoPersona = ddlBuscarTipoPersona.SelectedValue
        obj_BE_MaestroPersonas.ApellidoPaterno = tbApellidoPaterno.Text.Trim
        obj_BE_MaestroPersonas.ApellidoMaterno = tbApellidoMaterno.Text.Trim
        obj_BE_MaestroPersonas.Nombre = tbNombre.Text.Trim

        If ddlBuscarTipoPersona.SelectedValue = 1 Then 'busqueda de alumnos

            obj_BE_MaestroPersonas.AlumnoNivel = ddlAlumnoNiveles.SelectedValue
            obj_BE_MaestroPersonas.AlumnoSubnivel = ddlAlumnoSubniveles.SelectedValue
            obj_BE_MaestroPersonas.AlumnoGrado = ddlAlumnoGrados.SelectedValue
            obj_BE_MaestroPersonas.AlumnoAula = ddlAlumnoAulas.SelectedValue

        ElseIf ddlBuscarTipoPersona.SelectedValue = 3 Then ' busqueda de familia

            obj_BE_MaestroPersonas.AlumnoFamiliarApellidoPaterno = tbFamiliarAlumnoApellidoPaterno.Text.Trim
            obj_BE_MaestroPersonas.AlumnoFamiliarApellidoMaterno = tbFamiliarAlumnoApellidoMaterno.Text.Trim
            obj_BE_MaestroPersonas.AlumnoFamiliarNombres = tbFamiliarAlumnoNombre.Text.Trim
            obj_BE_MaestroPersonas.AlumnoFamiliarNivel = IIf(ddlFamiliarAlumnoNiveles.SelectedValue = "", 0, ddlFamiliarAlumnoNiveles.SelectedValue)
            obj_BE_MaestroPersonas.AlumnoFamiliarSubnivel = ddlFamiliarAlumnoSubniveles.SelectedValue
            obj_BE_MaestroPersonas.AlumnoFamiliarGrado = ddlFamiliarAlumnoGrados.SelectedValue
            obj_BE_MaestroPersonas.AlumnoFamiliarAula = ddlFamiliarAlumnoAulas.SelectedValue

        End If

        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_MaestroPersonas As New bl_MaestroPersonas
            ds_Lista = obj_BL_MaestroPersonas.FUN_LIS_Personas(obj_BE_MaestroPersonas, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_MaestroPersonas As New bl_MaestroPersonas
                ds_Lista = obj_BL_MaestroPersonas.FUN_LIS_Personas(obj_BE_MaestroPersonas, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

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

#End Region

#Region "Metodos de Combos"

#Region "Generales"

    ''' <summary>
    ''' Carga la información al seleccionable de Tipos de Personas.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoPersona()

        Dim obj_BL_TiposPersonas As New bl_TiposPersonas
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_TiposPersonas.FUN_LIS_TiposPersonas("", -1, 0, 7, int_CodigoUsuario, int_CodigoTipoUsuario)
        Controles.llenarCombo(ddlBuscarTipoPersona, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga la información al seleccionable de Sedes de Colegio.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSede()

        Dim obj_BL_SedesColegio As New bl_SedesColegio
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_SedesColegio.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlBuscarSede, ds_Lista, "Codigo", "NombreSede", True, False)

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

#Region "Alumnos"

    ''' <summary>
    ''' Limpia los selecionables referidos a la busqueda de alumnos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombosAlumno()

        limpiarCombos(ddlAlumnoNiveles)
        limpiarCombos(ddlAlumnoSubniveles)
        limpiarCombos(ddlAlumnoGrados)
        limpiarCombos(ddlAlumnoAulas)

    End Sub

    ''' <summary>
    ''' Carga los seleccionables referidos al Alumno.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosAlumno()

        cargarComboAlumnoNivel()
        limpiarCombos(ddlAlumnoSubniveles)
        limpiarCombos(ddlAlumnoGrados)
        limpiarCombos(ddlAlumnoAulas)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable de Nivel.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 25/01/2011 
    ''' </remarks>
    Private Sub cargarComboAlumnoNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlAlumnoNiveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable de SubNivel.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 25/01/2011 
    ''' </remarks>
    Private Sub cargarComboAlumnoSubNivel()

        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlAlumnoNiveles.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlAlumnoSubniveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable de Grado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 25/01/2011 
    ''' </remarks>
    Private Sub cargarComboAlumnoGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlAlumnoSubniveles.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlAlumnoGrados, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable de Aulas.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 25/01/2011 
    ''' </remarks>
    Private Sub cargarComboAlumnoAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlAlumnoGrados.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlAlumnoAulas, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

#End Region

#Region "Alumno Familiar"

    ''' <summary>
    ''' Limpia los seleccionables referidos al familiar.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombosFamiliarAlumno()

        limpiarCombos(ddlFamiliarAlumnoNiveles)
        limpiarCombos(ddlFamiliarAlumnoSubniveles)
        limpiarCombos(ddlFamiliarAlumnoGrados)
        limpiarCombos(ddlFamiliarAlumnoAulas)

    End Sub

    ''' <summary>
    ''' Carga los seleccionables referidos al familiar.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarCombosFamiliarAlumno()

        cargarComboFamiliarAlumnoNivel()
        limpiarCombos(ddlFamiliarAlumnoSubniveles)
        limpiarCombos(ddlFamiliarAlumnoGrados)
        limpiarCombos(ddlFamiliarAlumnoAulas)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable de Nivel del Hijo del Familiar.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 25/01/2011 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoNivel()

        Dim obj_BL_Niveles As New bl_Niveles
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Niveles.FUN_LIS_Niveles("", -1, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)
        Controles.llenarCombo(ddlFamiliarAlumnoNiveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable del SubNivel del Hijo del Familiar.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 25/01/2011 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoSubNivel()

        Dim obj_BL_SubNiveles As New bl_Subniveles
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_SubNiveles.FUN_LIS_Subniveles(CInt(ddlFamiliarAlumnoNiveles.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)

        Controles.llenarCombo(ddlFamiliarAlumnoSubniveles, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable del grado del Hijo del Familiar.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 25/01/2011 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoGrado()

        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados(CInt(ddlFamiliarAlumnoSubniveles.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)

        Controles.llenarCombo(ddlFamiliarAlumnoGrados, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable del aula del Hijo del Familiar.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan José
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: 25/01/2011 
    ''' </remarks>
    Private Sub cargarComboFamiliarAlumnoAulas()

        Dim obj_BL_Aulas As New bl_Aulas
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim ds_Lista As DataSet = obj_BL_Aulas.FUN_LIS_Aulas(CInt(ddlFamiliarAlumnoGrados.SelectedValue), int_CodigoUsuario, int_CodigoTipoUsuario, 0, 7)

        Controles.llenarCombo(ddlFamiliarAlumnoAulas, ds_Lista, "Codigo", "Descripcion", True, False)

    End Sub

#End Region

#End Region

#Region "Metodos del Gridview"

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

        Dim btnVerFoto As HtmlAnchor = e.Row.FindControl("btnLinkVerFoto")
        Dim lblTipoPersona As Label = e.Row.FindControl("lbCodigoTipoPaciente")

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = gridview.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(gridview, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            btnVerFoto.Attributes.Add("rel", "sexylightbox")

            If lblTipoPersona.Text = 1 Then
                btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & e.Row.DataItem("RutaFoto")
            ElseIf lblTipoPersona.Text = 2 Then
                btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Admin").ToString() & e.Row.DataItem("RutaFoto")
            ElseIf lblTipoPersona.Text = 3 Then
                btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & e.Row.DataItem("RutaFoto")
            Else
                btnVerFoto.HRef = ConfigurationManager.AppSettings("RutaFotosUsuarios_Web").ToString() & e.Row.DataItem("RutaFoto")
            End If


            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

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

#End Region

End Class
