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
Partial Class Modulo_Permisos_AsignacionPermisosPerfiles
    Inherits System.Web.UI.Page

#Region "Eventos de Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Asignación de Permisos a Perfiles")
            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                cargarPerfiles()
                ddl_Perfiles.Attributes.Add("onchange", "ShowMyModalPopup()")
                cargarConfiguracion()
            End If

            If ViewState("Fila_Check") Is Nothing Then
                Dim int_FilaCheck As Integer = ViewState("Fila_Check")
                Dim chkHabilitar As System.Web.UI.WebControls.CheckBox = gv_ConfigPermisosPerfil.Rows(int_FilaCheck).Cells(8).FindControl("chk_AccesoTotal_grilla")
                chkHabilitar.Focus()
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

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

    Protected Sub ddl_Perfiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarConfiguracion()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

#End Region

#Region "Eventos de Grilla"

    Protected Sub gv_ConfigPermisosPerfil_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Dim chkAcciones As System.Web.UI.WebControls.CheckBoxList = e.Row.FindControl("chk_AccionesAcceso_grilla")
        Dim chkST As System.Web.UI.WebControls.CheckBox = e.Row.FindControl("chk_AccesoTotal_grilla")

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dv_Acciones As New DataView
            Dim dt_Acciones_obtener As New DataTable
            Dim dt_Acciones_vacia As New DataTable
            Dim dt_PlantillaAcciones As New DataTable
            Dim codigo_Asignacion As Integer = 0

            dt_Acciones_vacia = ObtenerTablaTemporalAccesos()
            dt_Acciones_obtener = ViewState("TablaConfiguracion")
            dt_PlantillaAcciones = ViewState("TablaPlantillaAcciones")
            dv_Acciones = dt_Acciones_obtener.DefaultView
            codigo_Asignacion = e.Row.DataItem("CodigoAsignacion")
            dv_Acciones.RowFilter = "1=1 and ABIS_CodigoAsignacion = " & codigo_Asignacion
            dt_Acciones_vacia = dv_Acciones.ToTable

            If dv_Acciones.Count > 0 Then

                chkAcciones.DataSource = dt_PlantillaAcciones
                chkAcciones.DataTextField = "NombreBlanco"
                chkAcciones.DataValueField = "NAA_CodigoNombreAccion"
                chkAcciones.DataBind()

                EliminarAccionesTabla(dt_Acciones_vacia, dt_PlantillaAcciones, chkAcciones)
                SetearDetallesTabla(dt_Acciones_vacia, chkAcciones, chkST)
            Else
                chkST.Visible = False
            End If

            If e.Row.DataItem("Descripcion_BloqueInformacion").ToString.Length > 0 Then
                e.Row.Cells(6).Style.Value = "background-color:#DBE7FB"
                e.Row.Cells(7).Style.Value = "background-color:#DBE7FB"
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            If e.Row.DataItem("SBM_CodigoSubBloque") = -1 Then
                chkST.Visible = False
                e.Row.Cells(9).Controls.Clear()
                e.Row.Style.Value = "background-color:#BEFC96"
            End If

            If e.Row.DataItem("SBM_CodigoSubBloque_Hijo") = -1 And e.Row.DataItem("SBM_CodigoSubBloque") > -1 Then
                chkST.Visible = False
                e.Row.Cells(9).Controls.Clear()
                e.Row.Style.Value = "background-color:#FFFFCC"
            End If
        End If

    End Sub

    Protected Sub chk_Habilitar_grilla_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim cont As Integer = 0
        Dim int_CodigoAsignacion As Integer = -2
        Dim chkAcciones_Focus As System.Web.UI.WebControls.CheckBoxList

        int_CodigoAsignacion = sender.ValidationGroup.ToString

        While cont <= gv_ConfigPermisosPerfil.Rows.Count - 1

            Dim lblCodigoAsignacion As System.Web.UI.WebControls.Label = gv_ConfigPermisosPerfil.Rows(cont).Cells(10).FindControl("lbl_CodigoAsignacion_grilla")
            Dim chkAcciones As System.Web.UI.WebControls.CheckBoxList = gv_ConfigPermisosPerfil.Rows(cont).Cells(9).FindControl("chk_AccionesAcceso_grilla")
            chkAcciones_Focus = chkAcciones
            If lblCodigoAsignacion.Text = -1 Then
                gv_ConfigPermisosPerfil.Rows(cont).Cells(8).Controls.Clear()
            End If

            If lblCodigoAsignacion.Text = int_CodigoAsignacion Then
                Dim chkHabilitar As System.Web.UI.WebControls.CheckBox = gv_ConfigPermisosPerfil.Rows(cont).Cells(8).FindControl("chk_AccesoTotal_grilla")
                ViewState("Fila_Check") = cont
                Dim contObj As Integer = 0

                If chkHabilitar.Checked = True Then
                    While contObj <= chkAcciones.Items.Count - 1
                        If chkAcciones.Items(contObj).Enabled = True Then
                            chkAcciones.Items(contObj).Selected = True
                        Else
                            chkAcciones.Items(contObj).Attributes.Add("style", "visibility:hidden;")

                        End If
                        contObj = contObj + 1
                    End While
                Else
                    While contObj <= chkAcciones.Items.Count - 1
                        If chkAcciones.Items(contObj).Enabled = True Then
                            chkAcciones.Items(contObj).Selected = False
                        Else
                            chkAcciones.Items(contObj).Attributes.Add("style", "visibility:hidden;")
                        End If
                        contObj = contObj + 1
                    End While
                End If

                chkHabilitar.Focus()
            End If

            Dim contObj2 As Integer = 0
            While contObj2 <= chkAcciones.Items.Count - 1
                If chkAcciones.Items(contObj2).Enabled = False Then
                    chkAcciones.Items(contObj2).Attributes.Add("style", "visibility:hidden;")
                End If
                contObj2 = contObj2 + 1
            End While

            cont = cont + 1
        End While

    End Sub

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
    ''' Setea las acciones configuradas por cada bloque de informacion, subbloque de menu y bloque de menu.
    ''' </summary>
    ''' <param name="dt">Tabla con las acciones permitidas por el perfil seleccionado</param>
    ''' <param name="listBox">control de tipo listbox para setear las acciones de acceso</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearDetallesTabla(ByVal dt As DataTable, ByVal listBox As WebControls.CheckBoxList, ByVal checkTodos As WebControls.CheckBox)
        Dim contObj As Integer = 0
        Dim contTabla As Integer = 0
        Dim boo_TodosAccesos As Boolean = True
        Dim int_CodigoComparacionNombreAccion As Integer = 0

        While contObj <= listBox.Items.Count - 1
            int_CodigoComparacionNombreAccion = listBox.Items(contObj).Value
            While contTabla <= dt.Rows.Count - 1
                If listBox.Items(contObj).Value = dt.Rows(contTabla).Item("NAA_CodigoNombreAccion").ToString And dt.Rows(contTabla).Item("Habilitado").ToString = True Then
                    listBox.Items(contObj).Selected = True
                End If

                If listBox.Items(contObj).Value = dt.Rows(contTabla).Item("NAA_CodigoNombreAccion").ToString Then
                    listBox.Items(contObj).Attributes.Add("title", dt.Rows(contTabla).Item("NAA_Descripcion").ToString)
                End If

                contTabla = contTabla + 1
            End While
            contObj = contObj + 1
            contTabla = 0
        End While

        contObj = 0
        While contObj <= listBox.Items.Count - 1

            If listBox.Items(contObj).Selected = False And listBox.Items(contObj).Enabled = True Then
                boo_TodosAccesos = False
            End If
            contObj = contObj + 1
        End While

        If boo_TodosAccesos = True Then
            checkTodos.Checked = True
        End If

    End Sub

    Private Sub EliminarAccionesTabla(ByVal dt As DataTable, ByVal dtTodas As DataTable, ByVal listBox As WebControls.CheckBoxList)
        Dim contObj As Integer = 0
        Dim contTodas As Integer = 0
        Dim contUna As Integer = 0
        Dim boo_Encontro As Boolean = False
        Dim Int_CodigoNombre As Integer = -1

        While contTodas <= dtTodas.Rows.Count - 1

            Int_CodigoNombre = dtTodas.Rows(contTodas).Item("NAA_CodigoNombreAccion")
            While contUna <= dt.Rows.Count - 1

                If dt.Rows(contUna).Item("NAA_CodigoNombreAccion") = Int_CodigoNombre Then
                    boo_Encontro = True
                End If
                contUna = contUna + 1
            End While

            If boo_Encontro = False Then
                While contObj <= listBox.Items.Count - 1

                    If listBox.Items(contObj).Value = Int_CodigoNombre = True Then
                        listBox.Items(contObj).Enabled = False
                        listBox.Items(contObj).Attributes.Add("style", "visibility:hidden;")

                    End If
                    contObj = contObj + 1
                End While

                contObj = 0
            End If

            boo_Encontro = False
            contUna = 0
            contTodas = contTodas + 1
        End While

    End Sub

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

        Dim obj_BL_Perfil As New bl_Perfiles
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_Perfil.FUN_LIS_PerfilModulos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 56)

        ddl_Perfiles.DataSource = ds_Lista.Tables(0)
        ddl_Perfiles.DataTextField = "Descripcion"
        ddl_Perfiles.DataValueField = "Codigo"
        ddl_Perfiles.DataBind()
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

        Dim obj_BL_AsignacionPermisosPerfiles As New bl_AsignacionPermisosPerfiles
        Dim int_Perfil As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        int_Perfil = ddl_Perfiles.SelectedValue

        Dim ds_Lista As DataSet = obj_BL_AsignacionPermisosPerfiles.FUN_GET_ConfiguracionPerfil(int_Perfil, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 56)
        Dim dtCabecera As DataTable
        Dim dtDetalle As DataTable
        Dim dtAcciones As DataTable

        dtCabecera = ds_Lista.Tables(0)
        dtDetalle = ds_Lista.Tables(1)
        dtAcciones = ds_Lista.Tables(2)

        ViewState("TablaConfiguracion") = dtDetalle
        ViewState("TablaPlantillaAcciones") = dtAcciones
        gv_ConfigPermisosPerfil.DataSource = dtCabecera
        gv_ConfigPermisosPerfil.DataBind()

        dl_NombreAcciones.DataSource = dtAcciones
        dl_NombreAcciones.DataBind()

    End Sub

    ''' <summary>
    ''' Obtiene la estructura de la tabla temporal de la lista de permisos sobre los bloques de informacion. (estructura vacía)
    ''' </summary>
    ''' <returns>tabla temporal vacia con la estructura de permisos</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerTablaTemporalAccesos() As DataTable
        Dim dt As DataTable = New DataTable("ListaPermisos")

        dt = Datos.agregarColumna(dt, "BI_CodigoBloqueInformacion", "String")
        dt = Datos.agregarColumna(dt, "NAA_Descripcion", "String")

        Return dt
    End Function

    ''' <summary>
    ''' Construye la estructura de la tabla temporal de permisos donde se almacenara la información registrada para su manipulación.
    ''' </summary>
    ''' <returns>estructura de tabla temporal de permisos</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ObtenerTablaPermisos() As DataTable
        Dim dt As DataTable = New DataTable("TablaPermisos")

        dt = Datos.agregarColumna(dt, "CodigoAsignacion", "Int32")
        dt = Datos.agregarColumna(dt, "CodigoPerfil", "Int32")
        dt = Datos.agregarColumna(dt, "Habilitado", "Int32")
        dt = Datos.agregarColumna(dt, "CodigoNombreAccion", "Int32")

        Return dt
    End Function

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
        Dim cont As Integer = 0
        Dim codigo_BloqueInformacion As Integer = 0
        Dim dtPermisos As New DataTable
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        dtPermisos = ObtenerTablaPermisos()

        While cont <= gv_ConfigPermisosPerfil.Rows.Count - 1

            Dim lblCodigoAsignacion As System.Web.UI.WebControls.Label = gv_ConfigPermisosPerfil.Rows(cont).FindControl("lbl_CodigoAsignacion_grilla")
            Dim chkAcciones As System.Web.UI.WebControls.CheckBoxList = gv_ConfigPermisosPerfil.Rows(cont).FindControl("chk_AccionesAcceso_grilla")
            Dim int_ContAcciones As Integer = 0

            While int_ContAcciones <= chkAcciones.Items.Count - 1

                If chkAcciones.Items(int_ContAcciones).Enabled = False Then
                    chkAcciones.Items(int_ContAcciones).Attributes.Add("style", "visibility:hidden;")
                Else
                    Dim dr As DataRow
                    dr = dtPermisos.NewRow

                    dr.Item("CodigoAsignacion") = lblCodigoAsignacion.Text
                    dr.Item("CodigoPerfil") = ddl_Perfiles.SelectedValue

                    If chkAcciones.Items(int_ContAcciones).Selected = True Then
                        dr.Item("CodigoNombreAccion") = chkAcciones.Items(int_ContAcciones).Value
                        dr.Item("Habilitado") = 1
                    Else
                        dr.Item("CodigoNombreAccion") = chkAcciones.Items(int_ContAcciones).Value
                        dr.Item("Habilitado") = 0
                    End If

                    dtPermisos.Rows.Add(dr)
                End If

                int_ContAcciones = int_ContAcciones + 1
            End While

            cont = cont + 1
        End While

        'ENVIAR TABLA TEMPORAL A GRABAR
        Dim bl_BL_AsignacionPermisosPerfiles As New bl_AsignacionPermisosPerfiles
        usp_valor = bl_BL_AsignacionPermisosPerfiles.FUN_INS_DetalleAsignacionPermisosPerfiles(dtPermisos, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 56)

        If usp_valor >= 0 Then
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
