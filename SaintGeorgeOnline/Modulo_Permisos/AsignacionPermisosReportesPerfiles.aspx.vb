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

Partial Class Modulo_Permisos_AsignacionPermisosReportesPerfiles
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos de Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Asignación de Permisos a Reportes por Perfil")

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                cargarPerfiles()
                ddlPerfiles.Attributes.Add("onchange", "ShowMyModalPopup()")
                cargarConfiguracion()
            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Grabar()
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlPerfiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarConfiguracion()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Eventos de Grilla"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

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

    'Protected Sub chk_Habilitar_grilla_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim cont As Integer = 0
    '    Dim int_CodigoAsignacion As Integer = -2
    '    Dim chkAcciones_Focus As System.Web.UI.WebControls.CheckBoxList

    '    int_CodigoAsignacion = sender.ValidationGroup.ToString

    '    While cont <= gv_ConfigPermisosPerfil.Rows.Count - 1

    '        Dim lblCodigoAsignacion As System.Web.UI.WebControls.Label = gv_ConfigPermisosPerfil.Rows(cont).Cells(10).FindControl("lbl_CodigoAsignacion_grilla")
    '        Dim chkAcciones As System.Web.UI.WebControls.CheckBoxList = gv_ConfigPermisosPerfil.Rows(cont).Cells(9).FindControl("chk_AccionesAcceso_grilla")
    '        chkAcciones_Focus = chkAcciones
    '        If lblCodigoAsignacion.Text = -1 Then
    '            gv_ConfigPermisosPerfil.Rows(cont).Cells(8).Controls.Clear()
    '        End If

    '        If lblCodigoAsignacion.Text = int_CodigoAsignacion Then
    '            Dim chkHabilitar As System.Web.UI.WebControls.CheckBox = gv_ConfigPermisosPerfil.Rows(cont).Cells(8).FindControl("chk_AccesoTotal_grilla")
    '            ViewState("Fila_Check") = cont
    '            Dim contObj As Integer = 0

    '            If chkHabilitar.Checked = True Then
    '                While contObj <= chkAcciones.Items.Count - 1
    '                    If chkAcciones.Items(contObj).Enabled = True Then
    '                        chkAcciones.Items(contObj).Selected = True
    '                    Else
    '                        chkAcciones.Items(contObj).Attributes.Add("style", "visibility:hidden;")

    '                    End If
    '                    contObj = contObj + 1
    '                End While
    '            Else
    '                While contObj <= chkAcciones.Items.Count - 1
    '                    If chkAcciones.Items(contObj).Enabled = True Then
    '                        chkAcciones.Items(contObj).Selected = False
    '                    Else
    '                        chkAcciones.Items(contObj).Attributes.Add("style", "visibility:hidden;")
    '                    End If
    '                    contObj = contObj + 1
    '                End While
    '            End If

    '            chkHabilitar.Focus()
    '        End If

    '        Dim contObj2 As Integer = 0
    '        While contObj2 <= chkAcciones.Items.Count - 1
    '            If chkAcciones.Items(contObj2).Enabled = False Then
    '                chkAcciones.Items(contObj2).Attributes.Add("style", "visibility:hidden;")
    '            End If
    '            contObj2 = contObj2 + 1
    '        End While

    '        cont = cont + 1
    '    End While

    'End Sub

#End Region

#Region "Metodos"

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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(3, 56, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Setea los permisos de Acciones sobre el formulario según el usuario logueado
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(3, 56)
    End Sub

    ''' <summary>
    ''' Obtener las Acciones de Acceso seleccionadas
    ''' </summary>
    ''' <param name="int_TipoDev">Tipo de dato a devolver</param>
    ''' <param name="obj_chkObjeto">Objeto de tipo control (Lista de checks)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerAccionesAcceso(ByVal int_TipoDev As Integer, ByVal obj_chkObjeto As Web.UI.WebControls.CheckBoxList) As String

        Dim contM As Integer = 0
        Dim CadenaDev_Cod As String = ""
        Dim CadenaDev_Des As String = ""

        While contM <= obj_chkObjeto.Items.Count - 1
            If obj_chkObjeto.Items(contM).Selected = True Then

                CadenaDev_Cod = CadenaDev_Cod + "," + obj_chkObjeto.Items(contM).Value
                CadenaDev_Des = CadenaDev_Des + ", " + obj_chkObjeto.Items(contM).Text
            End If
            contM = contM + 1
        End While

        If CadenaDev_Des.Length > 4 Then
            CadenaDev_Des = CadenaDev_Des.Substring(2, CadenaDev_Des.Length - 2)
        End If

        If CadenaDev_Cod.Length > 1 Then
            CadenaDev_Cod = CadenaDev_Cod.Substring(1, CadenaDev_Cod.Length - 1)
        End If

        If int_TipoDev = 1 Then
            'CODIGO
            Return CadenaDev_Cod
        Else
            'DESCRIPCION
            Return CadenaDev_Des
        End If
    End Function

    ''' <summary>
    ''' Carga información en el seleccionable de perfiles.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarPerfiles()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_Perfil As New bl_Perfiles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Perfil.FUN_LIS_PerfilModulos(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 56)
        Controles.llenarCombo(ddlPerfiles, ds_Lista, "Codigo", "Descripcion", False, False)

    End Sub

    ''' <summary>
    ''' Muestra la información de la configuración de un perfil seleccionado en el formulario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarConfiguracion()

        Dim obj_BL_AsignacionPermisosReportesPerfiles As New bl_AsignacionPermisosReportesPerfiles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_Perfil As Integer = ddlPerfiles.SelectedValue

        Dim ds_Lista As DataSet = obj_BL_AsignacionPermisosReportesPerfiles.FUN_GET_ConfiguracionPerfil(int_Perfil, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaPlantillaPermisos") = ds_Lista.Tables(0)

        GridView1.DataSource = ds_Lista.Tables(0)
        GridView1.DataBind()

    End Sub

    ''' <summary>
    ''' Registra los accesos y permisos de acciones de todas las opciones marcadas según el perfil seleccionado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim dtPermisos As New DataTable
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoPerfil As Integer = ddlPerfiles.SelectedValue
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        Dim dt As DataTable
        dt = CType(ViewState("ListaPlantillaPermisos"), DataTable).Clone

        Dim dr As DataRow
        For Each gvr As GridViewRow In GridView1.Rows

            If Convert.ToInt32(CType(gvr.FindControl("lblCodigoPresentacionReporte"), Label).Text) > 0 Then 'CodigoPresentacionReporte               
                dr = dt.NewRow
                dr.Item("CodigoTipoReporte") = CType(gvr.FindControl("lblCodigoTipoReporte"), Label).Text
                dr.Item("CodigoReporte") = CType(gvr.FindControl("lblCodigoReporte"), Label).Text
                dr.Item("CodigoPresentacionReporte") = CType(gvr.FindControl("lblCodigoPresentacionReporte"), Label).Text
                dr.Item("CodigoAsignacionPermisoReporte") = CType(gvr.FindControl("lblCodigoAsignacionPermisoReporte"), Label).Text
                dr.Item("TipoAccion") = Convert.ToInt32(CType(gvr.FindControl("chk"), CheckBox).Checked)
                dt.Rows.Add(dr)
            End If
        Next

        Dim obj_BL_AsignacionPermisosReportesPerfiles As New bl_AsignacionPermisosReportesPerfiles
        usp_valor = obj_BL_AsignacionPermisosReportesPerfiles.FUN_INS_AsignacionPermisosReportesPerfil(int_CodigoPerfil, dt, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 56)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
            cargarConfiguracion()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    ''' <summary>
    ''' Envío el mensaje y tipo de mensaje a ser mostrado por el Master Page.
    ''' </summary>
    ''' <param name="str_Mensaje">Descripción del mensaje</param>
    ''' <param name="str_TipoMensaje">Descripción del tipo de mensaje</param>
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

End Class