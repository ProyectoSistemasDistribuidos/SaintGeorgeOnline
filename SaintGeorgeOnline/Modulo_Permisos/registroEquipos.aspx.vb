Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_BusinessLogic.ModuloPermisos
Imports SaintGeorgeOnline_Utilities
Imports System.Data

Partial Class Modulo_Permisos_registroEquipos
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Registro de Equipos")
            If Not Page.IsPostBack Then

                cargarTipoDispositivos()

            Else

                If Not Session("PersonaPopup") Is Nothing AndAlso Page.Session("ResetearPadre") = True Then

                    Dim objMaestroPersona As be_MaestroPersonas = Session("PersonaPopup")
                    Dim int_CodigoPersona As Integer = objMaestroPersona.CodigoPersona
                    Dim int_CodigoTipoPersona As Integer = objMaestroPersona.CodigoTipoPersona

                    hiddenCodigoPersona.Value = int_CodigoPersona
                    hiddenCodigoTipoPersona.Value = int_CodigoTipoPersona
                    tbNombre.Text = objMaestroPersona.NombreCompleto

                    Session.Remove("PersonaPopup")
                    cargarRegistros(int_CodigoPersona, int_CodigoTipoPersona)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Grabar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            AgregarFila()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            EliminarFilas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub


#End Region

#Region "Metodos"

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    Private Sub cargarTipoDispositivos()
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_descripcion As String = ""
        Dim ds_Lista As DataSet
        Dim obl_TipoDispositivo As New bl_TipoDispositivo
        ds_Lista = obl_TipoDispositivo.FUN_LIS_TipoDispositivo(str_descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        ViewState("ListaTiposDispositivos") = ds_Lista
    End Sub

    Private Sub cargarRegistros(ByVal int_CodigoPersona As Integer, ByVal int_CodigoTipoPersona As Integer)

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim ds_Lista As DataSet
        Dim obl_RegistroMAC As New bl_RegistroMAC
        ds_Lista = obl_RegistroMAC.FUN_LIS_RegistroMAC(int_CodigoPersona, int_CodigoTipoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If ds_Lista.Tables(0).Rows.Count > 0 Then
            hiddenCodigoRegistro.Value = ds_Lista.Tables(0).Rows(0).Item("cRegistro")
        Else
            hiddenCodigoRegistro.Value = 0
        End If

        Dim lstEliminados As New List(Of Integer)
        ViewState("lstEliminados") = lstEliminados

        ViewState("ListaDetalle") = ds_Lista.Tables(1)

        GridView1.DataSource = ds_Lista.Tables(1)
        GridView1.DataBind()
    End Sub

    Private Sub Grabar()

        Dim obl_RegistroMAC As New bl_RegistroMAC
        Dim obe_RegistroMAC As New be_RegistroMAC

        If hiddenCodigoPersona.Value = 0 Then
            MostrarSexyAlertBox("Debe buscar a una persona primero.", "Alert")
            Exit Sub
        End If

        If ViewState("ListaDetalle") Is Nothing Then
            MostrarSexyAlertBox("Debe agregar algun dispositivo.", "Alert")
            Exit Sub
        End If

        'If CType(ViewState("ListaDetalle"), DataTable).Rows.Count = 0 Then
        '    MostrarSexyAlertBox("Debe agregar algun dispositivo.", "Alert")
        '    Exit Sub
        'End If

        obe_RegistroMAC.CodigoCabecera = hiddenCodigoRegistro.Value
        obe_RegistroMAC.CodigoPersona = hiddenCodigoPersona.Value
        obe_RegistroMAC.CodigoTipoPersona = hiddenCodigoTipoPersona.Value

        Dim lstEliminados As List(Of Integer)
        lstEliminados = ViewState("lstEliminados")

        Dim dtDetalle As DataTable = ViewState("ListaDetalle")

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        usp_valor = obl_RegistroMAC.FUN_INS_RegistroMAC(obe_RegistroMAC, lstEliminados, dtDetalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")

            Dim int_CodigoPersona As Integer = hiddenCodigoPersona.Value
            Dim int_CodigoTipoPersona As Integer = hiddenCodigoTipoPersona.Value
            cargarRegistros(int_CodigoPersona, int_CodigoTipoPersona)

            'miTab1.Enabled = True
            'miTab2.Enabled = False
            'TabContainer1.ActiveTabIndex = 0
            'Buscar()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If



    End Sub

#End Region

#Region "Eventos del Gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ds_lista As DataSet
            If ViewState("ListaTiposDispositivos") Is Nothing Then
                cargarTipoDispositivos()
            End If
            ds_lista = ViewState("ListaTiposDispositivos")

            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlTipoDispositivo"), DropDownList)
            Controles.llenarCombo(ddl, ds_lista, "cTipoDispositivo", "Descripcion", False, True)
            ddl.SelectedValue = e.Row.DataItem("cTipoDispositivo")

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0
        Try
            If e.CommandName = "Agregar" Or e.CommandName = "Eliminar" Then
                If e.CommandName = "Agregar" Then
                    AgregarFila()
                ElseIf e.CommandName = "Eliminar" Then
                    Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                    Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
                    EliminarFila(row)
                End If
            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs)
        For index As Integer = 0 To GridView1.Rows.Count - 2
            CType(GridView1.Rows(index).FindControl("btnAgregarFila"), ImageButton).Visible = False
        Next
    End Sub

    Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs)
        Try
            GridView1.EditIndex = -1
            Dim dt As DataTable
            dt = ViewState("ListaDetalle")
            GridView1.DataSource = dt
            GridView1.DataBind()
            'Activo el boton "Agregar Nueva Fila"  y "Eliminar Todo"
            btnAgregar.Enabled = True
            btnEliminar.Enabled = True
            btnAgregar.ImageUrl = "~/App_Themes/Imagenes/btnAgregar_1.png"
            btnEliminar.ImageUrl = "~/App_Themes/Imagenes/btnEliminar_1.png"
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
        Try
            GridView1.EditIndex = e.NewEditIndex
            Dim dt As DataTable
            dt = ViewState("ListaDetalle")
            GridView1.DataSource = dt
            GridView1.DataBind()
            'Desactivo el boton "Agregar Nueva Fila" y "Eliminar Todo"
            btnAgregar.Enabled = False
            btnEliminar.Enabled = False
            btnAgregar.ImageUrl = "~/App_Themes/Imagenes/btnAgregar_0.png"
            btnEliminar.ImageUrl = "~/App_Themes/Imagenes/btnEliminar_0.png"
            CType(GridView1.Rows(e.NewEditIndex).FindControl("ddlTipoDispositivo"), DropDownList).Enabled = True
            For Each gvr As GridViewRow In GridView1.Rows
                CType(gvr.FindControl("btnAgregarFila"), ImageButton).Visible = False
                CType(gvr.FindControl("btnEliminarFila"), ImageButton).Visible = False
            Next
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
        Try

            Dim tbDireccion As TextBox = CType(GridView1.Rows(e.RowIndex).FindControl("tbDireccionMAC"), TextBox)
            Dim str_Direccion As String = tbDireccion.Text.Trim

            Dim row As GridViewRow = CType(tbDireccion.NamingContainer, GridViewRow)
            Dim int_CodigoTipoEquipo As Integer = CInt(CType(row.FindControl("ddlTipoDispositivo"), DropDownList).SelectedValue)
            Dim int_CodigoDetalle As Integer = CInt(CType(GridView1.Rows(e.RowIndex).FindControl("lblcDetalle"), Label).Text.Trim)

            'Actualizar el Dt
            Dim dt As New DataTable
            dt = CType(ViewState("ListaDetalle"), DataTable).Copy

            If int_CodigoTipoEquipo > 0 Then
                For Each dr As DataRow In dt.Rows
                    If dr.Item("cDetalle") = int_CodigoDetalle Then
                        dr.Item("cTipoDispositivo") = int_CodigoTipoEquipo
                        dr.Item("DireccionMAC") = str_Direccion
                        Exit For
                    End If
                Next
                ViewState("ListaDetalle") = dt
            Else
                Exit Sub
            End If

            GridView1.EditIndex = -1

            'Pinto el nuevo Dt
            GridView1.DataSource = dt
            GridView1.DataBind()

            'Activo el boton "Agregar Nueva Fila"  y "Eliminar Todo"
            btnAgregar.Enabled = True
            btnEliminar.Enabled = True
            btnAgregar.ImageUrl = "~/App_Themes/Imagenes/btnAgregar_1.png"
            btnEliminar.ImageUrl = "~/App_Themes/Imagenes/btnEliminar_1.png"

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "eEtodos del Gridview"

    Private Sub AgregarFila()

        Dim dt As DataTable
        If ViewState("ListaDetalle") Is Nothing Then
            dt = New DataTable
            dt = Datos.agregarColumna(dt, "cRegistro", "Integer")
            dt = Datos.agregarColumna(dt, "cDetalle", "Integer")
            dt = Datos.agregarColumna(dt, "cTipoDispositivo", "Integer")
            dt = Datos.agregarColumna(dt, "DireccionMAC", "string")
            dt = Datos.agregarColumna(dt, "TD", "string")
        Else
            dt = ViewState("ListaDetalle")
        End If

        Dim max_Registros As Integer = hiddenCodigoTipoPersona.Value
        If max_Registros = 0 Then
            max_Registros = 1
        End If

        If dt.Rows.Count = max_Registros Then
            MostrarSexyAlertBox("No se pueden agregar más registros al detalle.", "Alert")
            Exit Sub
        End If

        Dim int_NuevoIndice As Integer

        If dt.Rows.Count > 0 Then
            int_NuevoIndice = dt.Compute("Max(cDetalle)", "")
        Else
            int_NuevoIndice = 0
        End If

        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("cRegistro") = 0
        dr.Item("cDetalle") = int_NuevoIndice + 1
        dr.Item("cTipoDispositivo") = 0
        dr.Item("DireccionMAC") = ""
        dr.Item("TD") = "T"
        dt.Rows.Add(dr)

        ViewState("ListaDetalle") = dt

        GridView1.DataSource = dt
        GridView1.DataBind()

    End Sub

    Private Sub EliminarFila(ByVal row As GridViewRow)

        Dim dt As DataTable
        If ViewState("ListaDetalle") Is Nothing Then
            dt = New DataTable
            dt = Datos.agregarColumna(dt, "cRegistro", "Integer")
            dt = Datos.agregarColumna(dt, "cDetalle", "Integer")
            dt = Datos.agregarColumna(dt, "cTipoDispositivo", "Integer")
            dt = Datos.agregarColumna(dt, "DireccionMAC", "string")
            dt = Datos.agregarColumna(dt, "TD", "string")
        Else
            dt = ViewState("ListaDetalle")
        End If

        Dim lstEliminados As List(Of Integer)
        lstEliminados = ViewState("lstEliminados")

        For Each dr As DataRow In dt.Rows
            If dr.Item("cDetalle") = CType(row.FindControl("lblcDetalle"), Label).Text Then
                If dr.Item("TD") = "R" Then
                    lstEliminados.Add(dr.Item("cDetalle"))
                End If
                dr.Delete()
                Exit For
            End If
        Next
        dt.AcceptChanges()

        ViewState("ListaDetalle") = dt
        GridView1.DataSource = dt
        GridView1.DataBind()

        ViewState("lstEliminados") = lstEliminados

    End Sub

    Private Sub EliminarFilas()

        Dim dtAux As DataTable
        If ViewState("ListaDetalle") Is Nothing Then
            dtAux = New DataTable
            dtAux = Datos.agregarColumna(dtAux, "cRegistro", "Integer")
            dtAux = Datos.agregarColumna(dtAux, "cDetalle", "Integer")
            dtAux = Datos.agregarColumna(dtAux, "cTipoDispositivo", "Integer")
            dtAux = Datos.agregarColumna(dtAux, "DireccionMAC", "string")
            dtAux = Datos.agregarColumna(dtAux, "TD", "string")
        Else
            dtAux = ViewState("ListaDetalle")
        End If

        Dim lstEliminados As List(Of Integer)
        lstEliminados = ViewState("lstEliminados")

        For Each dr As DataRow In dtAux.Rows
            If dr.Item("TD") = "R" Then
                lstEliminados.Add(dr.Item("cDetalle"))
            End If
        Next

        Dim dt = New DataTable
        dt = Datos.agregarColumna(dt, "cRegistro", "Integer")
        dt = Datos.agregarColumna(dt, "cDetalle", "Integer")
        dt = Datos.agregarColumna(dt, "cTipoDispositivo", "Integer")
        dt = Datos.agregarColumna(dt, "DireccionMAC", "string")
        dt = Datos.agregarColumna(dt, "TD", "string")

        ViewState("ListaDetalle") = dt
        GridView1.DataSource = dt
        GridView1.DataBind()

    End Sub

#End Region


End Class
