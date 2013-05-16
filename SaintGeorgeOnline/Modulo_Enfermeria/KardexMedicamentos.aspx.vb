Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Drawing
Imports System.Diagnostics
Imports System.Runtime.InteropServices.Marshal
Imports System.Threading

''' <summary>
''' Modulo de Ingresos y Salidas de medicamentos
''' </summary>
''' <remarks>
''' Código del Modulo:    1
''' Código de la Opción:  46
''' </remarks>
Partial Class Modulo_Enfermeria_KardexMedicamentos
    Inherits System.Web.UI.Page


#Region "Eventos Generales"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Master.MostrarTitulo("Ingresos y Salidas de Medicamentos")

        Try
            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                ViewState("SortExpression") = "DescripcionNombre"
                ViewState("Direccion") = "ASC"
                btnExportar.Attributes.Add("OnClick", "ShowMyModalPopup()")
                btn_Cancelar_Kardex_IS.Attributes.Add("OnClick", "return confirm_cancelar();")
                cargarComboSede()
                listar()

                'LOAD DE I/S
                cargarComboMedicamentoKardex()
                cargarComboMotivoSalida()
                pnl_MotivoSalida.Visible = False
                ViewState("NuevoEntradaSalida") = True
                cargarKardex_ES()
                pnl_Mant_Registro_Kardex_IS.Enabled = False
                btn_Cancelar_Kardex_IS.Enabled = False
                btn_Grabar_Kardex_IS.Enabled = False

                btn_Cancelar_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnCancelarV2_0.png"
                btn_Grabar_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_0.png"

                tbBuscarFechaHistFinal.Text = Now.Date
                tbBuscarFechaHistInicial.Text = DateAdd(DateInterval.Day, -6, Now.Date)

                lbl_Hist_CodigoMedicamento.Text = 0
                lbl_Hist_CodigoSede.Text = 0

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

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportar.Click
        Try
            Exportar()
        Catch ex As Exception
            EnvioEmailError(4, ex.ToString)
        End Try

    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        limpiarFiltros()
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Dispose()
    End Sub

    Protected Sub btnNuevo_Kardex_IS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Nuevo()
    End Sub

    Protected Sub btnCancelar_Kardex_IS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Cancelar()
    End Sub

    Protected Sub btn_Grabar_Kardex_IS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Grabar()
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Eventos de la Grilla"

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

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblNumPaginas")
            _TotalPags.Text = GridView1.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblRegistrosActuales")
            _Registros.Text = InformacionPager(GridView1, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

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
        If e.Row.RowType = System.Web.UI.WebControls.DataControlRowType.Pager Then
            CrearBotonesPager(GridView1, e.Row, Me)
        End If
    End Sub

    Protected Sub GriedView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        If e.CommandName = "VerHistorial" Then
            Dim CodigoMedicamento As Integer = CInt(e.CommandArgument.ToString)
            Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
            Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

            lbl_MedicamentoHistorial.Text = CType(row.FindControl("lblNombreMedicamento_grilla"), Label).Text
            lbl_SedeHistorial.Text = CType(row.FindControl("lblSede_grilla"), Label).Text

            lbl_Hist_CodigoMedicamento.Text = CType(row.FindControl("lblCodigoMedicamento_grilla"), Label).Text
            lbl_Hist_CodigoSede.Text = CType(row.FindControl("lblCodigoSede_grilla"), Label).Text

            ListarHistorial(CodigoMedicamento, CType(row.FindControl("lblCodigoSede_grilla"), Label).Text)
            mpe_Historico.Show()
        End If

    End Sub

#End Region

#Region "Eventos de PopUps de Ingresos y Salidas"

    Protected Sub btnAgregarDetalleIngresoSalida_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevoEntradaSalida") = True
        mpe_ModalIngresoSalida.Show()
    End Sub

    Protected Sub popup_btnAgregar_Medicamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If ViewState("NuevoEntradaSalida") = False Then

            editarMedicamento_IS()

        ElseIf ViewState("NuevoEntradaSalida") = True Then

            agregarMedicamento_IS()

        End If

    End Sub

    Protected Sub popup_btnCancelar_Medicamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalMedicamento()
    End Sub

    Protected Sub rblTipoAccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If rblTipoAccion_IS.SelectedValue = 1 Then
            'INGRESO
            pnl_MotivoSalida.Visible = False
        Else
            'SALIDA
            pnl_MotivoSalida.Visible = True
        End If

        mpe_ModalIngresoSalida.Show()
    End Sub

    Protected Sub ddlMedicamento_IS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            MostrarStockActual()
            mpe_ModalIngresoSalida.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlSede_IS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            MostrarStockActual()
            mpe_ModalIngresoSalida.Show()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub gvDetalleKardex_ES_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
            Dim CodigoRelFichaMedEnEnfermedades As Integer = CInt(e.CommandArgument.ToString)
            Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
            Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

            If e.CommandName = "Actualizar" Then

                ViewState("NuevoEntradaSalida") = False

                activarEditarMedicamento_IS(CType(row.FindControl("lblCorrelativo_grilla"), Label).Text, CType(row.FindControl("lblCodigoSede_grilla"), Label).Text, CType(row.FindControl("lblCodigoMedicamento_grilla"), Label).Text, CType(row.FindControl("lblCodigoTipoAccion_grilla"), Label).Text, CType(row.FindControl("lblCodigoMotivoSalida_grilla"), Label).Text, CType(row.FindControl("lblCantidad_grilla"), Label).Text, CType(row.FindControl("lblObservaciones_grilla"), Label).Text)

            ElseIf e.CommandName = "Eliminar" Then

                EliminarMedicamento_IS(CType(row.FindControl("lblCorrelativo_grilla"), Label).Text)

            End If

        End If

    End Sub

    Protected Sub gvDetalleKardex_ES_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

        End If

    End Sub
    Protected Sub btnAgregarRegistroMedicamento_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim str_miDDL As String = ddlMedicamento_IS.UniqueID.ToString
        Dim bool_miModal As Boolean = True
        Dim str_miModal As String = mpe_ModalIngresoSalida.UniqueID.ToString

        ucIngresarMedicamento.setearParametros(str_miDDL, bool_miModal, str_miModal)
        ucIngresarMedicamento.mostrarModal()

    End Sub
#End Region

#Region "Eventos de Historial"

    Protected Sub ddlHistPageSelector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _DropDownList As DropDownList = DirectCast(sender, DropDownList)
            Dim _NumPag As Integer

            If Integer.TryParse(_DropDownList.SelectedValue.ToString, _NumPag) AndAlso _NumPag > 0 AndAlso _NumPag <= Me.gv_Historico.PageCount Then
                Me.gv_Historico.PageIndex = _NumPag - 1
            Else
                Me.gv_Historico.PageIndex = 0
            End If

            Me.gv_Historico.SelectedIndex = -1

            Dim ds_Lista As DataSet = ObtenerResultadoBusquedaHist(2)
            hfTotalRegsHist.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

            gv_Historico.DataSource = ds_Lista.Tables(0)
            gv_Historico.DataBind()

            mpe_Historico.Show()

            mpe_Historico.Show()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try
    End Sub

    Protected Sub gv_Historico_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            If e.NewPageIndex >= 0 Then
                Me.gv_Historico.PageIndex = e.NewPageIndex
            End If

            Dim ds_Lista As DataSet = ObtenerResultadoBusquedaHist(2)
            hfTotalRegsHist.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

            gv_Historico.DataSource = ds_Lista.Tables(0)
            gv_Historico.DataBind()

            mpe_Historico.Show()
        Catch ex As Exception
            EnvioEmailError(111, ex.ToString)
        End Try

    End Sub

    Protected Sub gv_Historico_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.Pager Then

            Dim _TotalPags As Label = e.Row.FindControl("lblHistNumPaginas")
            _TotalPags.Text = gv_Historico.PageCount.ToString

            Dim _Registros As Label = e.Row.FindControl("lblHistRegistrosActuales")
            _Registros.Text = InformacionHistPager(gv_Historico, e.Row, Me)

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

    Protected Sub gv_Historico_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = System.Web.UI.WebControls.DataControlRowType.Pager Then
            CrearBotonesHistPager(gv_Historico, e.Row, Me)
        End If
    End Sub

    Protected Sub btnBuscarHistorial_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            ListarHistorial(1, 1)
            mpe_Historico.Show()
        Catch ex As Exception
            EnvioEmailError(8, ex.ToString)
        End Try

    End Sub

#End Region

#Region "Metodos de Historial"

    ''' <summary>
    ''' Crea los botones de siguientex, atraz, etc de la lista de información de los medicamentos.
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
    Private Sub CrearBotonesHistPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page)

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim ddlPageSelector As DropDownList = DirectCast(gvPagerRow.FindControl("ddlHistPageSelector"), DropDownList)
        ddlPageSelector.Items.Clear()

        For i As Integer = 1 To gridView.PageCount
            ddlPageSelector.Items.Add(i.ToString())
        Next

        ddlPageSelector.SelectedIndex = pageIndex

    End Sub

    Private Function InformacionHistPager(ByVal gridView As GridView, ByVal gvPagerRow As GridViewRow, ByVal page As Page) As String

        Dim pageIndex As Integer = gridView.PageIndex
        Dim pageCount As Integer = gridView.PageCount
        Dim pageSize As Integer = gridView.PageSize
        Dim rowCount As Integer = gridView.Rows.Count

        Dim currentPageFirstRow As Integer = ((pageIndex * pageSize) + 1)
        Dim currentPageLastRow As Integer = 0
        Dim lastPageRemainder As Integer = pageCount Mod pageSize

        currentPageLastRow = currentPageFirstRow + rowCount - 1

        Return [String].Format("Registro {0} al {1} de {2}", currentPageFirstRow, currentPageLastRow, hfTotalRegsHist.Value)

    End Function

    Private Sub ListarHistorial(ByVal int_codigoMedicamento As Integer, ByVal int_codigoSede As Integer)
        Dim ds_Lista As New DataSet
        
        If IsDate(tbBuscarFechaHistInicial.Text) = False Or IsDate(tbBuscarFechaHistFinal.Text) = False Then
            MostrarSexyAlertBox("Formato de Fecha de Inicio o Fin incorrecta", "Info")
            Exit Sub
        End If

        ds_Lista = ObtenerResultadoBusquedaHist(1)
        hfTotalRegsHist.Value = CInt(ds_Lista.Tables(0).Rows.Count.ToString)

        gv_Historico.DataSource = ds_Lista.Tables(0)
        gv_Historico.DataBind()

    End Sub

    Private Function ObtenerResultadoBusquedaHist(ByVal int_Modo As Integer) As DataSet

        Dim ds_Lista As New DataSet
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim date_FechaIni As Date = tbBuscarFechaHistInicial.Text
        Dim date_FechaFin As Date = tbBuscarFechaHistFinal.Text
        Dim int_CodigoMedicamento As Integer = lbl_Hist_CodigoMedicamento.Text
        Dim int_CodigoSede As Integer = lbl_Hist_CodigoSede.Text

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Kardex As New bl_Kardex
            ds_Lista = obj_BL_Kardex.FUN_LIS_KardexMovimientos(int_codigoSede, int_codigoMedicamento, date_FechaIni, date_FechaFin, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)
            ViewState("Listado_DatosHist") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_DatosHist") Is Nothing Then

                Dim obj_BL_Kardex As New bl_Kardex
                ds_Lista = obj_BL_Kardex.FUN_LIS_KardexMovimientos(int_codigoSede, int_codigoMedicamento, date_FechaIni, date_FechaFin, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)
                ViewState("Listado_DatosHist") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_DatosHist")
            End If
        End If

        Return ds_Lista
    End Function

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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(1, 46, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Habilita el registro de una nueva entrada y/o salida de medicamentos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Nuevo()
        pnl_Mant_Registro_Kardex_IS.Enabled = True
        btn_Cancelar_Kardex_IS.Enabled = True
        btn_Grabar_Kardex_IS.Enabled = True

        btn_Cancelar_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnCancelar_1.png"
        btn_Grabar_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnGrabar_1.png"
        btnNuevo_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnNuevoV2_0.png"

        btnNuevo_Kardex_IS.Enabled = False
    End Sub

    ''' <summary>
    ''' Registra el Ingreso y/o Salida de medicamentos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()
        Dim bl_Kardex As New bl_Kardex
        Dim mensaje As String = ""
        Dim dt_Detalle_IS As New DataTable
        Dim result As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        dt_Detalle_IS = ViewState("ListaKardex_ES")

        result = bl_Kardex.FUN_INS_DetalleKardex(dt_Detalle_IS, mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)

        MostrarSexyAlertBox(mensaje, "Info")

        If result > 0 Then
            pnl_Mant_Registro_Kardex_IS.Enabled = False
            btn_Cancelar_Kardex_IS.Enabled = False
            btn_Grabar_Kardex_IS.Enabled = False

            btn_Cancelar_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnCancelarV2_0.png"
            btn_Grabar_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_0.png"
            btnNuevo_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnNuevo_1.png"

            btnNuevo_Kardex_IS.Enabled = True

            BlanquearTablaMedicamentos_IS()
            listar()
        End If
    End Sub

    ''' <summary>
    ''' Cancelar Operación de Registro de Entrada yo Salida de medicamentos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Cancelar()

        pnl_Mant_Registro_Kardex_IS.Enabled = False
        btn_Cancelar_Kardex_IS.Enabled = False
        btn_Grabar_Kardex_IS.Enabled = False

        btn_Cancelar_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnCancelarV2_0.png"
        btn_Grabar_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnGrabarV2_0.png"
        btnNuevo_Kardex_IS.ImageUrl = "~/App_Themes/Imagenes/btnNuevo_1.png"

        btnNuevo_Kardex_IS.Enabled = True

        BlanquearTablaMedicamentos_IS()

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
        Me.Master.RegistrarAccesoPagina(1, 46)

        'CONTROLES DEL FORMULARIO



        'GRUPOS DE INFORMACION


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

        Dim rutamadre As String = ""
        Dim downloadBytes As Byte()
        Dim stream As Stream
        Dim writer As StreamWriter
        Dim contenido_exportar As String = ""
        Dim NombreArchivo As String = ""

        Dim obj_BL_Kardex As New bl_Kardex
        Dim ds_Lista As DataSet = ObtenerResultadoBusqueda(1)

        Dim dt As DataTable = New DataTable("ListaExportar")

        dt = Datos.agregarColumna(dt, "N°", "String")
        dt = Datos.agregarColumna(dt, "Medicamento", "String")
        dt = Datos.agregarColumna(dt, "Unidad Medica / Presentación", "String")
        dt = Datos.agregarColumna(dt, "Stock", "String")
        dt = Datos.agregarColumna(dt, "Stock Mínimo", "String")
        dt = Datos.agregarColumna(dt, "Sede", "String")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In ds_Lista.Tables(0).Rows
            auxDR = dt.NewRow
            auxDR.Item("N°") = cont
            auxDR.Item("Medicamento") = dr.Item("DescripcionNombre").ToString
            auxDR.Item("Unidad Medica / Presentación") = dr.Item("DescripcionRelacion").ToString
            auxDR.Item("Stock") = dr.Item("CantidadActual").ToString
            auxDR.Item("Stock Mínimo") = dr.Item("StockMinimo").ToString
            auxDR.Item("Sede") = dr.Item("Sede").ToString

            dt.Rows.Add(auxDR)
            cont += 1
        Next

        If rbExportar.SelectedValue = 0 Then 'WORD
            Dim reporte_html As String = ""
            Dim Arreglo_Datos As String()

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Kardex de Medicmantos")
            reporte_html = Arreglo_Datos(0)
            NombreArchivo = Arreglo_Datos(1)
            NombreArchivo = NombreArchivo & ".doc"

            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Enfermeria", "\Reportes\")


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
            NombreArchivo = Exportacion.ExportarReporte(dt, "Kardex de Medicmantos")
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Enfermeria", "\Reportes\")

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

            m = Exportacion.ExportarReporte_Pdf(dt, "Kardex de Medicmantos")


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

            Arreglo_Datos = Exportacion.ExportarReporte_Html(dt, "Kardex de Medicmantos")
            reporte_html = Arreglo_Datos(0)
            Session("Exportaciones_RepHtml") = reporte_html
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>MostrarImpresion_html();</script>", False)
        End If

    End Sub

    ''' <summary>
    ''' Carga la relación de entradas y salidas de medicamentos registrada.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarKardex_ES()
        If ViewState("ListaKardex_ES") Is Nothing Then
        Else
            gvDetalleKardex_IS.DataSource = ViewState("ListaKardex_ES")
            gvDetalleKardex_IS.DataBind()

        End If
    End Sub

    ''' <summary>
    ''' Limpia los filtros de búsqueda
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarFiltros()

        tbBuscarMedicamentoDescripcion.Text = ""
        ddlBuscarSede.SelectedValue = 0
        tbBuscarMedicamentoDescripcion.Focus()

    End Sub

    ''' <summary>
    ''' Carga el seleccionable de sedes.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboSede()
        Dim obj_BL_Sede As New bl_SedesColegio
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Sede.FUN_LIS_SedesColegio("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)

        Controles.llenarCombo(ddlBuscarSede, ds_Lista, "Codigo", "NombreSede", True, False)
        Controles.llenarCombo(ddlSede_IS, ds_Lista, "Codigo", "NombreSede", False, True)
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

    End Sub

    ''' <summary>
    ''' Lista la relación de medicamentos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
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
        End If

        SortGridView(ViewState("SortExpression"), ViewState("Direccion"))
        ImagenSorting(ViewState("SortExpression"))
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

        Dim str_Descripcion As String = tbBuscarMedicamentoDescripcion.Text.Trim()
        Dim int_CodigoSede As Integer = CInt(ddlBuscarSede.SelectedValue)
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As New DataSet

        If int_Modo = 1 Then 'LLAMAR A LA BASE DE DATOS

            Dim obj_BL_Kardex As New bl_Kardex
            ds_Lista = obj_BL_Kardex.FUN_LIS_Kardex(int_CodigoSede, str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)
            ViewState("Listado_Datos") = ds_Lista
        Else                 'LLAMAR EN MEMORIA
            If ViewState("Listado_Datos") Is Nothing Then

                Dim obj_BL_Kardex As New bl_Kardex
                ds_Lista = obj_BL_Kardex.FUN_LIS_Kardex(int_CodigoSede, str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)
                ViewState("Listado_Datos") = ds_Lista
            Else
                ds_Lista = ViewState("Listado_Datos")
            End If
        End If

        Return ds_Lista
    End Function

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
    ''' Carga el seleccionable de Motivos de Salidas de medicamentos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMotivoSalida()
        Dim obj_BL_MotivoSalidaMedicamento As New bl_MotivoSalidaMedicamento
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_MotivoSalidaMedicamento.FUN_LIS_MotivoSalidaMedicamento("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)

        Controles.llenarCombo(ddlMotivoSalidaMedicamento_IS, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el seleccionable de Medicamentos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboMedicamentoKardex()

        Dim obj_BL_Medicamentos As New bl_Medicamentos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Medicamentos.FUN_LIS_Medicamento(0, 0, 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)
        ViewState("DatosMedicamentos") = ds_Lista.Tables(0)
        Controles.llenarCombo(ddlMedicamento_IS, ds_Lista, "Codigo", "NombreCompleto", False, True)

    End Sub

#End Region

#Region "Metodos del Gridview"

    ''' <summary>
    ''' Crea los botones de siguientex, atraz, etc de la lista de información de los medicamentos.
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
    ''' Información referencial del listado de información de la grilla de medicamentos.
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
    ''' <param name="nombreBoton">Descripción del nombre del grafico</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ImagenSorting(ByVal nombreBoton As String)

        Dim _btnSorting As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_" & nombreBoton), ImageButton)
        Dim _btnSorting_d1 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_DescripcionNombre"), ImageButton)
        Dim _btnSorting_d2 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_DescripcionRelacion"), ImageButton)
        Dim _btnSorting_d3 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_CantidadActual"), ImageButton)
        Dim _btnSorting_d5 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_Sede"), ImageButton)
        Dim _btnSorting_d6 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_StockMinimo"), ImageButton)
        Dim _btnSorting_d7 As ImageButton = CType(GridView1.HeaderRow.FindControl("btnSorting_FechaActualizacionStockReal"), ImageButton)

        If _btnSorting.ID = _btnSorting_d1.ID Then
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

        ElseIf _btnSorting.ID = _btnSorting_d2.ID Then

            _btnSorting_d1.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d1.ToolTip = "Descendente"

            _btnSorting_d3.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d3.ToolTip = "Descendente"

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

            _btnSorting_d5.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d5.ToolTip = "Descendente"

            _btnSorting_d7.ImageUrl = "~/App_Themes/Imagenes/DOWN.png"
            _btnSorting_d7.ToolTip = "Descendente"

        ElseIf _btnSorting.ID = _btnSorting_d7.ID Then
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

#Region "Metodos de PopUp de Ingresos y Salidas"

    ''' <summary>
    ''' Cierra la ventana modal del Ingreso y/o salida de medicamentos.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalMedicamento()

        mpe_ModalIngresoSalida.Hide()
        'LIMPIAR CONTROLES DE POPUP
        ddlSede_IS.SelectedValue = 0
        ddlMedicamento_IS.SelectedValue = 0
        rblTipoAccion_IS.SelectedValue = 1
        ddlMotivoSalidaMedicamento_IS.SelectedValue = 0
        tb_Cantidad_IS.Text = 0
        tb_Observacion_IS.Text = ""

    End Sub

    ''' <summary>
    ''' Agrega el medicamento a la lista de Entradas y Salidas de medicamentos a registrar
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarMedicamento_IS()

        If ddlSede_IS.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar una sede.", "Alert")
            mpe_ModalIngresoSalida.Show()
            Exit Sub
        End If

        If ddlMedicamento_IS.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un medicamento.", "Alert")
            mpe_ModalIngresoSalida.Show()
            Exit Sub
        End If

        If tb_Cantidad_IS.Text.Length = 0 Then
            MostrarSexyAlertBox("Debe ingresar una cantidad.", "Alert")
            mpe_ModalIngresoSalida.Show()
            Exit Sub
        End If

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False
        Dim id_codigo_fila As Integer = 0

        If ViewState("ListaKardex_ES") Is Nothing Then

            dt = New DataTable("ListaKardex_ES")

            dt = Datos.agregarColumna(dt, "Correlativo", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoSede", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoMedicamento", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoTipoAccion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoMotivoSalida", "Integer")

            dt = Datos.agregarColumna(dt, "Medicamento", "String")
            dt = Datos.agregarColumna(dt, "Cantidad", "Integer")
            dt = Datos.agregarColumna(dt, "TipoAccion", "String")
            dt = Datos.agregarColumna(dt, "Sede", "String")
            dt = Datos.agregarColumna(dt, "MotivoSalida", "String")
            dt = Datos.agregarColumna(dt, "Observaciones", "String")
        Else

            dt = ViewState("ListaKardex_ES")

        End If

        'VALIDACION
        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoSede").ToString = ddlSede_IS.SelectedValue And auxdr.Item("CodigoMedicamento") = ddlMedicamento_IS.SelectedValue And auxdr.Item("CodigoTipoAccion").ToString = rblTipoAccion_IS.SelectedValue Then
                    MostrarSexyAlertBox("Ya se ha ingresado un medicamento para la sede seleccionada y para la acción elegida. Actualice la cantidad del medicamento ya ingresado", "Alert")
                    mpe_ModalIngresoSalida.Show()
                    Exit Sub
                End If

            Next

        End If

        If boolIncremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            id_codigo_fila = dt.Rows.Count

            dr.Item("Correlativo") = id_codigo_fila + 1
            dr.Item("CodigoSede") = ddlSede_IS.SelectedValue
            dr.Item("CodigoMedicamento") = ddlMedicamento_IS.SelectedValue
            dr.Item("CodigoTipoAccion") = rblTipoAccion_IS.SelectedValue
            dr.Item("CodigoMotivoSalida") = ddlMotivoSalidaMedicamento_IS.SelectedValue

            dr.Item("Medicamento") = ddlMedicamento_IS.SelectedItem.Text
            dr.Item("Cantidad") = tb_Cantidad_IS.Text
            dr.Item("TipoAccion") = rblTipoAccion_IS.SelectedItem.Text
            dr.Item("Sede") = ddlSede_IS.SelectedItem.Text

            If ddlMotivoSalidaMedicamento_IS.SelectedValue = 0 Then
                dr.Item("MotivoSalida") = ""
            Else
                dr.Item("MotivoSalida") = ddlMotivoSalidaMedicamento_IS.SelectedItem.Text
            End If

            dr.Item("Observaciones") = tb_Observacion_IS.Text.Trim
            dt.Rows.Add(dr)

        End If

        ViewState("ListaKardex_ES") = dt

        gvDetalleKardex_IS.DataSource = dt
        gvDetalleKardex_IS.DataBind()

        'LIMPIAR CONTROLES DE POPUP
        ddlSede_IS.SelectedValue = 0
        ddlMedicamento_IS.SelectedValue = 0
        rblTipoAccion_IS.SelectedValue = 1
        ddlMotivoSalidaMedicamento_IS.SelectedValue = 0
        tb_Cantidad_IS.Text = 0
        tb_Observacion_IS.Text = ""

        upKardex_IS.Update()

    End Sub

    ''' <summary>
    ''' Edita el medicamento de la lista de Entradas y Salidas de medicamentos a registrar.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarMedicamento_IS()

        If ddlSede_IS.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar una sede.", "Alert")
            mpe_ModalIngresoSalida.Show()
            Exit Sub
        End If

        If ddlMedicamento_IS.SelectedValue = 0 Then
            MostrarSexyAlertBox("Debe seleccionar un medicamento.", "Alert")
            mpe_ModalIngresoSalida.Show()
            Exit Sub
        End If

        If tb_Cantidad_IS.Text.Length = 0 Then
            MostrarSexyAlertBox("Debe ingresar una cantidad.", "Alert")
            mpe_ModalIngresoSalida.Show()
            Exit Sub
        End If

        Dim int_CodigoOriginal As Integer = hidencodigoIS_PopUp.Value

        Dim dt As DataTable
        Dim boolIncremento As Boolean = False

        dt = ViewState("ListaKardex_ES")

        For Each auxdr As DataRow In dt.Rows

            '        If auxdr.Item("CodigoEnfermedad").ToString = ddlEnfermedad.SelectedValue And auxdr.Item("Edad").ToString = Val(tbEdad.Text) Then

            '            MostrarSexyAlertBox("El registro ya se encuentra en la lista", "Alert")
            '            Exit Sub

            '        End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("Correlativo").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoSede") = ddlSede_IS.SelectedValue
                auxdr.Item("CodigoMedicamento") = ddlMedicamento_IS.SelectedValue
                auxdr.Item("CodigoTipoAccion") = rblTipoAccion_IS.SelectedValue
                auxdr.Item("CodigoMotivoSalida") = ddlMotivoSalidaMedicamento_IS.SelectedValue
                auxdr.Item("Medicamento") = ddlMedicamento_IS.SelectedItem.Text
                auxdr.Item("Cantidad") = tb_Cantidad_IS.Text
                auxdr.Item("TipoAccion") = rblTipoAccion_IS.SelectedItem.Text
                auxdr.Item("Sede") = ddlSede_IS.SelectedItem.Text

                If ddlMotivoSalidaMedicamento_IS.SelectedValue = 0 Then
                    auxdr.Item("MotivoSalida") = ""
                Else
                    auxdr.Item("MotivoSalida") = ddlMotivoSalidaMedicamento_IS.SelectedItem.Text
                End If

                auxdr.Item("Observaciones") = tb_Observacion_IS.Text.Trim
            End If

        Next

        ViewState("ListaKardex_ES") = dt

        gvDetalleKardex_IS.DataSource = dt
        gvDetalleKardex_IS.DataBind()

        'LIMPIAR CONTROLES DE POPUP
        ddlSede_IS.SelectedValue = 0
        ddlMedicamento_IS.SelectedValue = 0
        rblTipoAccion_IS.SelectedValue = 1
        ddlMotivoSalidaMedicamento_IS.SelectedValue = 0
        tb_Cantidad_IS.Text = 0
        tb_Observacion_IS.Text = ""

        upKardex_IS.Update()

    End Sub

    ''' <summary>
    ''' Invoca al PopUp de Entrada y/o Salida de medicamentos y setea de datos para su modificación por el usuario. 
    ''' </summary>
    ''' <param name="int_Correlativo">Codigo identificador de la fila a modificar</param>
    ''' <param name="int_CodigoSede">Codigo de la Sede</param>
    ''' <param name="int_CodigoMedicamento">Codigo del Medicamento</param>
    ''' <param name="int_CodigoTipoAccion">Codigo del tipo de acción (Ingreso / Salida)</param>
    ''' <param name="int_CodigoMotivoSalida">Codigo del motivo de salida</param>
    ''' <param name="Cantidad">Cantidad de medicamento a Ingresar y/o Salir</param>
    ''' <param name="str_Observaciones">Observación del Retiro de medicamento</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarMedicamento_IS(ByVal int_Correlativo As Integer, ByVal int_CodigoSede As Integer, ByVal int_CodigoMedicamento As Integer, ByVal int_CodigoTipoAccion As Integer, ByVal int_CodigoMotivoSalida As Integer, ByVal Cantidad As Integer, ByVal str_Observaciones As String)

        hidencodigoIS_PopUp.Value = int_Correlativo

        rblTipoAccion_IS.SelectedValue = int_CodigoTipoAccion
        ddlSede_IS.SelectedValue = int_CodigoSede
        ddlMedicamento_IS.SelectedValue = int_CodigoMedicamento
        ddlMotivoSalidaMedicamento_IS.SelectedValue = int_CodigoMotivoSalida
        tb_Cantidad_IS.Text = Cantidad
        tb_Observacion_IS.Text = str_Observaciones

        If rblTipoAccion_IS.SelectedValue = 1 Then
            'INGRESO
            pnl_MotivoSalida.Visible = False
        Else
            'SALIDA
            pnl_MotivoSalida.Visible = True
        End If

        UpdatePanel1.Update()
        mpe_ModalIngresoSalida.Show()

    End Sub

    ''' <summary>
    ''' Elimina el medicamento selecionado de la relación de medicamentos a registrar su entrada y/o salida.
    ''' </summary>
    ''' <param name="int_CodigoCorrelativo"> Codigo que identifica al medicamento para una determinada sede.</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EliminarMedicamento_IS(ByVal int_CodigoCorrelativo As Integer)

        Dim dt As DataTable

        dt = ViewState("ListaKardex_ES")

        For Each auxdr As DataRow In dt.Rows

            If Val(auxdr.Item("Correlativo").ToString) = int_CodigoCorrelativo Then
                auxdr.Delete()
                Exit For

            End If

        Next

        dt.AcceptChanges()

        ViewState("ListaKardex_ES") = dt

        gvDetalleKardex_IS.DataSource = dt
        gvDetalleKardex_IS.DataBind()
        upKardex_IS.Update()

    End Sub

    ''' <summary>
    ''' Crea y limpia la tabla temporal de medicamentos a registrar (Estructura de la tabla)
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub BlanquearTablaMedicamentos_IS()
        Dim dt As DataTable

        dt = New DataTable("ListaKardex_ES")

        dt = Datos.agregarColumna(dt, "Correlativo", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoSede", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoMedicamento", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoTipoAccion", "Integer")
        dt = Datos.agregarColumna(dt, "CodigoMotivoSalida", "Integer")

        dt = Datos.agregarColumna(dt, "Medicamento", "String")
        dt = Datos.agregarColumna(dt, "Cantidad", "Integer")
        dt = Datos.agregarColumna(dt, "TipoAccion", "String")
        dt = Datos.agregarColumna(dt, "Sede", "String")
        dt = Datos.agregarColumna(dt, "MotivoSalida", "String")
        dt = Datos.agregarColumna(dt, "Observaciones", "String")

        ViewState("ListaKardex_ES") = dt

        gvDetalleKardex_IS.DataSource = dt
        gvDetalleKardex_IS.DataBind()

        upKardex_IS.Update()
    End Sub

    ''' <summary>
    ''' Muestra el Stock actual para el medicamento y sede seleccionada.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarStockActual()

        If ddlSede_IS.SelectedValue > 0 Then
            Dim BL_kardex As New bl_Kardex
            Dim dt_Stock As New DataTable
            Dim codigo_sede As Integer = ddlSede_IS.SelectedValue
            Dim codigo_medicamento As Integer = ddlMedicamento_IS.SelectedValue
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

            dt_Stock = BL_kardex.FUN_GET_KardexStockMedicamento(codigo_sede, codigo_medicamento, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46).Tables(0)

            If dt_Stock.Rows.Count > 0 Then
                lbl_StockActual_IS.Text = dt_Stock.Rows(0).Item("Stock")
            Else
                lbl_StockActual_IS.Text = "No se ha podido obtener el stock del medicamento seleccionado."
            End If

        End If

    End Sub

#End Region



End Class
