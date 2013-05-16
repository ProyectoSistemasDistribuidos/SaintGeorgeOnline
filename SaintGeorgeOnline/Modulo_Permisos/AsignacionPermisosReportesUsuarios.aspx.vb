Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloPermisos
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos

''' <summary>
''' Modulo de Asignación de Permisos a Perfiles
''' </summary>
''' <remarks>
''' Código del Modulo:    
''' Código de la Opción:  
''' </remarks>

Partial Class Modulo_Permisos_AsignacionPermisosReportesUsuarios
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos Generales"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Asignación de Permisos a Reportes por Usuarios")

            If Not Page.IsPostBack Then

                ViewState("ListaUsuario_ValidarBtnActualizar") = True
                ListarUsuarios()

                'cargarConfiguracion(1)

            End If

            btnVerPermisos.Attributes.Add("onclick", "ShowMyModalPopup()")
            btnAgregarPermisos.Attributes.Add("onclick", "ShowMyModalPopup()")
            btnQuitarPermisos.Attributes.Add("onclick", "ShowMyModalPopup()")

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Grabar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        lblNombreCompletoActual.Text = ""
        lblPerfilActual.Text = ""
        lblModoActual.Text = ""

        hiddenCodigoPerfilActual.Value = 0
        hiddenCodigoTrabajadorActual.Value = 0

        ViewState("ListaUsuario_ValidarBtnActualizar") = True
        ListarUsuarios()

        gv_ConfigPermisosPerfil.DataBind()
        btnGrabar.Visible = False
        btnCancelar.Visible = False

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

    Private Sub ListarUsuarios()

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim obj_Bl_Perfiles As New bl_Perfiles
        Dim ds_Lista As New DataSet
        ds_Lista = obj_Bl_Perfiles.FUN_LIS_UsuariosPerfil(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        gv_Usuarios.DataSource = ds_Lista.Tables(0)
        gv_Usuarios.DataBind()

    End Sub

    Private Sub cargarConfiguracion(ByVal int_CodigoPerfil As Integer, ByVal int_CodigoTrabajador As Integer)

        Dim obj_BL_AsignacionPermisosReportesPerfiles As New bl_AsignacionPermisosReportesPerfiles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_AsignacionPermisosReportesPerfiles.FUN_LIS_PlantillaPerfilPorTrabajador(int_CodigoPerfil, int_CodigoTrabajador, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaPlantillaPermisos") = ds_Lista.Tables(0)

        gv_ConfigPermisosPerfil.DataSource = ds_Lista.Tables(0)
        gv_ConfigPermisosPerfil.DataBind()

    End Sub

    Private Sub mostrarPanelConfiguracion(ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoPerfil As Integer, _
                                          ByVal str_NombreTrabajador As String, ByVal str_Perfil As String)

        hiddenCodigoPerfil.Value = int_CodigoPerfil
        hiddenCodigoTrabajador.Value = int_CodigoTrabajador

        lblNombreCompleto.Text = str_NombreTrabajador
        lblPerfil.Text = str_Perfil

        pnModalOpciones.Show()

    End Sub

    Private Sub Grabar()

        Dim obj_BL_AsignacionPermisosReportesPerfiles As New bl_AsignacionPermisosReportesPerfiles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_TipoAccion As Integer = hiddenCodigoAccionGrabar.Value
        ' Tipo Accion :
        ' -------------
        ' 4 : Agregar Permisos Reporte
        ' 5 : Quitar Permisos Reporte

        Dim int_CodigoTrabajador As Integer = hiddenCodigoTrabajadorActual.Value
        Dim int_CodigoPerfil As Integer = hiddenCodigoPerfilActual.Value
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        Dim dtPermisos As New DataTable("TablaPermisosReportes")
        dtPermisos = Datos.agregarColumna(dtPermisos, "CodigoPresentacionReporte", "integer")
        Dim dr As DataRow

        Dim str_Tipo As String = ""
        Dim str_CodigoPresentacionReporte As String = ""
        Dim chk As CheckBox

        For Each gvr As GridViewRow In gv_ConfigPermisosPerfil.Rows

            str_Tipo = CType(gvr.FindControl("lblTipo"), Label).Text

            If str_Tipo = "PR" Then

                chk = gvr.FindControl("chk")
                str_CodigoPresentacionReporte = CType(gvr.FindControl("lblCodigoPresentacionReporte"), Label).Text

                If int_TipoAccion = 4 Then ' Agregar Permiso Reporte

                    ' Si esta marcado, lo agrego a mi tabla de nuevos permisos
                    If chk.Enabled And chk.Checked Then
                        dr = dtPermisos.NewRow
                        dr.Item("CodigoPresentacionReporte") = str_CodigoPresentacionReporte
                        dtPermisos.Rows.Add(dr)
                    End If

                ElseIf int_TipoAccion = 5 Then ' Quitar Permiso Reporte

                    ' Si no esta marcado, lo agrego a mi tabla de quitar permisos
                    If chk.Enabled And chk.Checked = False Then
                        dr = dtPermisos.NewRow
                        dr.Item("CodigoPresentacionReporte") = str_CodigoPresentacionReporte
                        dtPermisos.Rows.Add(dr)
                    End If

                End If

            End If
        Next

        If Not dtPermisos.Rows.Count > 0 Then
            MostrarSexyAlertBox("No ha seleccionado ningún Reporte", "Alert")
            Exit Sub
        End If

        usp_valor = obj_BL_AsignacionPermisosReportesPerfiles.FUN_INS_AsignacionPermisosReportesParaUsuarios(int_CodigoTrabajador, int_TipoAccion, dtPermisos, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 56)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            cargarConfiguracion(int_CodigoPerfil, int_CodigoTrabajador)

            If int_TipoAccion = 4 Then ' Agregar Acciones
                modoAgregarPermisos()
            ElseIf int_TipoAccion = 5 Then ' Quitar Acciones
                modoQuitarPermisos()
            End If

        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

#End Region

#Region "Eventos de Grilla"

    Protected Sub gv_Usuarios_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Actualizar" Then

                Dim codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                Dim int_CodigoTrabajador As Integer = CType(row.FindControl("lblCodigoTrabajador"), Label).Text
                Dim int_CodigoPerfil As Integer = CType(row.FindControl("lblCodigoPerfil"), Label).Text
                Dim str_NombreTrabajador As String = CType(row.FindControl("lblNombreTrabajador"), Label).Text
                Dim str_Perfil As String = CType(row.FindControl("lblPerfil"), Label).Text

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    mostrarPanelConfiguracion(int_CodigoTrabajador, int_CodigoPerfil, str_NombreTrabajador, str_Perfil)
                    'cargarConfiguracion(codigo)
                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub gv_Usuarios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnActualizar As ImageButton = e.Row.FindControl("btnActualizar")
            Dim lblIdx As Label = e.Row.FindControl("lblIdx")
            lblIdx.Text = e.Row.RowIndex + 1

            Dim bool_ValidarBtnActualizar As Boolean = ViewState("ListaUsuario_ValidarBtnActualizar")

            If bool_ValidarBtnActualizar Then
                If e.Row.DataItem("CodigoPerfil") = 0 Then
                    btnActualizar.Visible = False
                Else
                    btnActualizar.Visible = True
                End If
            Else
                btnActualizar.Visible = False
            End If


            'btnActualizar.Attributes.Add("onclick", "ShowMyModalPopup()")

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

    Protected Sub gv_ConfigPermisosPerfil_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblTipo As Label = e.Row.FindControl("lblTipo")
            Dim lblDescTipoReporte As Label = e.Row.FindControl("lblDescTipoReporte")
            Dim lblDescReporte As Label = e.Row.FindControl("lblDescReporte")
            Dim lblDescPresentacionReporte As Label = e.Row.FindControl("lblDescPresentacionReporte")
            Dim chk As CheckBox = e.Row.FindControl("chk")

            If lblTipo.Text = "T" Then

                e.Row.Attributes.Add("style", "background-color: #BEFC96")
                chk.Attributes.Add("style", "display: none")

            ElseIf lblTipo.Text = "R" Then

                e.Row.Attributes.Add("style", "background-color: #FFFFCC")
                chk.Attributes.Add("style", "display: none")

            ElseIf lblTipo.Text = "PR" Then

                e.Row.Cells(3).Attributes.Add("style", "background-color: #DBE7FB")

                If e.Row.DataItem("TipoAccion") = 1 Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If

            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

#Region "Metodos del GriedView"

    Private Sub SetearDetallesTabla(ByVal dv As DataView, ByVal chkList As CheckBoxList, ByVal checkAll As CheckBox)

        chkList.DataSource = dv
        chkList.DataValueField = "CodigoAccionAcceso"
        chkList.DataTextField = "DescBlanco"
        chkList.DataBind()

        Dim int_TotalVisibles, int_TotalCheck, int_TotalRegistrosDV As Integer
        int_TotalRegistrosDV = dv.Count
        For Each dvr As DataRowView In dv
            If dvr("Visible") = 1 Then
                int_TotalVisibles += 1
                If dvr("Check") = 1 Then
                    int_TotalCheck += 1
                End If
            End If
        Next

        If int_TotalVisibles > 0 Then
            If int_TotalVisibles = int_TotalCheck Then
                checkAll.Checked = True
            End If
        Else
            checkAll.Visible = False
        End If

        Dim int_CAAP_Dvr As Integer ' CodigoAsignacionAccionesPerfil - DataViewRow
        Dim int_CAA_Dvr As Integer ' CodigoAccionAcceso - DataViewRow
        Dim int_VIS_Dvr As Integer ' Visible - DataViewRow
        Dim int_CHK_Dvr As Integer ' Check - DataViewRow

        Dim int_CAA_Chk As Integer ' CodigoAccionAcceso - CheckBox

        Dim contObj As Integer = 0

        While contObj <= chkList.Items.Count - 1
            int_CAA_Chk = chkList.Items(contObj).Value

            For Each dvr As DataRowView In dv
                int_CAAP_Dvr = dvr("CodigoAsignacionAccionesPerfil")
                int_CAA_Dvr = dvr("CodigoAccionAcceso")
                int_VIS_Dvr = dvr("Visible")
                int_CHK_Dvr = dvr("Check")

                If int_CAA_Chk = int_CAA_Dvr Then
                    chkList.Items(contObj).Attributes.Add("title", dvr("DescNombreAccion").ToString)
                    'chkList.Items(contObj).Attributes.Add("CodigoNombreAccion", dvr("CodigoNombreAccion").ToString)

                    If int_CHK_Dvr = 1 Then
                        chkList.Items(contObj).Selected = True
                    Else
                        chkList.Items(contObj).Selected = False
                    End If
                Else
                    If int_CAA_Chk = 0 Then
                        chkList.Items(contObj).Attributes.Add("style", "display: none;")
                        chkList.Items(contObj).Enabled = False
                    End If
                End If
            Next

            contObj = contObj + 1
        End While

    End Sub

#End Region

#Region "Modal Opciones"

#Region "Eventos"

    Protected Sub btnVerPermisos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim int_CodigoPerfil, int_CodigoTrabajador As Integer
            int_CodigoPerfil = hiddenCodigoPerfil.Value
            int_CodigoTrabajador = hiddenCodigoTrabajador.Value

            hiddenCodigoAccionGrabar.Value = 1 ' Visualizar Permisos

            lblNombreCompletoActual.Text = lblNombreCompleto.Text
            lblPerfilActual.Text = lblPerfil.Text
            lblModoActual.Text = "Visualizar"

            cargarConfiguracion(int_CodigoPerfil, int_CodigoTrabajador)
            modoVerPermisos()
            pnModalOpciones.Hide()

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnAgregarPermisos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim int_CodigoPerfil, int_CodigoTrabajador As Integer
            int_CodigoPerfil = hiddenCodigoPerfil.Value
            int_CodigoTrabajador = hiddenCodigoTrabajador.Value

            lblNombreCompletoActual.Text = lblNombreCompleto.Text
            lblPerfilActual.Text = lblPerfil.Text
            lblModoActual.Text = "Agregar"

            hiddenCodigoPerfilActual.Value = hiddenCodigoPerfil.Value
            hiddenCodigoTrabajadorActual.Value = hiddenCodigoTrabajador.Value
            hiddenCodigoAccionGrabar.Value = 4 ' Agregar Permisos

            ViewState("ListaUsuario_ValidarBtnActualizar") = False
            ListarUsuarios()

            cargarConfiguracion(int_CodigoPerfil, int_CodigoTrabajador)
            modoAgregarPermisos()
            pnModalOpciones.Hide()

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnQuitarPermisos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim int_CodigoPerfil, int_CodigoTrabajador As Integer
            int_CodigoPerfil = hiddenCodigoPerfil.Value
            int_CodigoTrabajador = hiddenCodigoTrabajador.Value

            lblNombreCompletoActual.Text = lblNombreCompleto.Text
            lblPerfilActual.Text = lblPerfil.Text
            lblModoActual.Text = "Quitar"

            hiddenCodigoPerfilActual.Value = hiddenCodigoPerfil.Value
            hiddenCodigoTrabajadorActual.Value = hiddenCodigoTrabajador.Value
            hiddenCodigoAccionGrabar.Value = 5 ' Quitar Permisos

            ViewState("ListaUsuario_ValidarBtnActualizar") = False
            ListarUsuarios()

            cargarConfiguracion(int_CodigoPerfil, int_CodigoTrabajador)
            modoQuitarPermisos()
            pnModalOpciones.Hide()

        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelarPermisos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        lblNombreCompletoActual.Text = ""
        lblPerfilActual.Text = ""
        lblModoActual.Text = ""

        hiddenCodigoPerfilActual.Value = 0
        hiddenCodigoTrabajadorActual.Value = 0
        hiddenCodigoAccionGrabar.Value = 1 ' Visualizar Permisos

        ViewState("ListaUsuario_ValidarBtnActualizar") = True

        gv_ConfigPermisosPerfil.DataBind()
        pnModalOpciones.Hide()

    End Sub

#End Region

#Region "Metodos"

    Private Sub modoAgregarPermisos()

        Dim lblTipo As Label
        Dim chk As CheckBox
        Dim lblCodigoAsignacionPermisoReporte As Label

        For Each gvr As GridViewRow In gv_ConfigPermisosPerfil.Rows

            lblTipo = gvr.FindControl("lblTipo")

            If lblTipo.Text = "PR" Then

                lblCodigoAsignacionPermisoReporte = gvr.FindControl("lblCodigoAsignacionPermisoReporte")
                chk = gvr.FindControl("chk")

                If chk.Checked Then
                    chk.Enabled = False
                Else
                    chk.Enabled = True
                End If

            End If
        Next

        btnGrabar.Visible = True
        btnCancelar.Visible = True

    End Sub

    Private Sub modoQuitarPermisos()

        Dim lblTipo As Label
        Dim chk As CheckBox
        Dim lblCodigoAsignacionPermisoReporte As Label

        For Each gvr As GridViewRow In gv_ConfigPermisosPerfil.Rows

            lblTipo = gvr.FindControl("lblTipo")

            If lblTipo.Text = "PR" Then

                lblCodigoAsignacionPermisoReporte = gvr.FindControl("lblCodigoAsignacionPermisoReporte")
                chk = gvr.FindControl("chk")

                If chk.Checked Then
                    chk.Enabled = True
                Else
                    chk.Enabled = False
                End If

            End If
        Next

        btnGrabar.Visible = True
        btnCancelar.Visible = True

    End Sub

    Private Sub modoVerPermisos()

        Dim lblTipo As Label
        Dim chk As CheckBox

        For Each gvr As GridViewRow In gv_ConfigPermisosPerfil.Rows

            lblTipo = gvr.FindControl("lblTipo")

            If lblTipo.Text = "PR" Then

                chk = gvr.FindControl("chk")
                chk.Enabled = False

            End If

        Next

        btnGrabar.Visible = False
        btnCancelar.Visible = False

    End Sub

#End Region

#End Region

End Class