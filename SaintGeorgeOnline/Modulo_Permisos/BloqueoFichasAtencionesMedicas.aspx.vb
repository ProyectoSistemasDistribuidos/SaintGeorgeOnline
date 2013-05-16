Imports System.Data

Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloPermisos
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos

''' <summary>
''' Modulo de Mantenimiento de Enfermedades
''' </summary>
''' <remarks>
''' Código del Modulo:    3
''' Código de la Opción:  45
''' </remarks>
Partial Class Modulo_Permisos_BloqueoFichasAtencionesMedicas
    Inherits System.Web.UI.Page

#Region "Eventos de Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Bloqueo de Modificación de Fichas de Atenciones Médicas")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                tbFechaRegistroExcepcion.Text = Now.Date
                ListarExcepciones()
                ListarBloqueosGenerales()
                ViewState("NuevaExcepcion") = True
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
        
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            GabrarBloqueosExcepciones()
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try

    End Sub

    Protected Sub popup_btnCancelar_Excepcion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        cerrarModalExcepcion()
    End Sub

    Protected Sub btn_Add_Excepcion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ViewState("NuevaExcepcion") = True
        tbCodigoFicha.Focus()
        modal_xxx.Show()
    End Sub

    Protected Sub popup_btnAgregar_Excepcion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If ViewState("NuevaExcepcion") = False Then
                editarExcepcion()
            ElseIf ViewState("NuevaExcepcion") = True Then
                agregarExcepcion()
            End If
        Catch ex As Exception
            EnvioEmailError(113, ex.ToString)
        End Try
        
    End Sub

#End Region

#Region "Eventos de Grilla"

    Protected Sub gvDetalleExcepciones_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Actualizar" Or e.CommandName = "Eliminar" Then
                Dim CodigoRelFichaMedEnEnfermedades As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                If e.CommandName = "Actualizar" Then
                    int_CodigoAccion = 6
                    ViewState("NuevaExcepcion") = False
                    activarEditarExcepcion(CType(row.FindControl("lblCorrelativo_grilla"), Label).Text, CType(row.FindControl("lblCodigoFichaAtencion_grilla"), Label).Text, CType(row.FindControl("lblFechaExclusion_grilla"), Label).Text, CType(row.FindControl("lblDiasExclusion_grilla"), Label).Text)

                ElseIf e.CommandName = "Eliminar" Then
                    int_CodigoAccion = 3
                    EliminarExcepcion(CType(row.FindControl("lblCorrelativo_grilla"), Label).Text)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub gvDetalleExcepciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim btnEliminar As ImageButton = e.Row.FindControl("btnEliminar")

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

            btnEliminar.Attributes.Add("OnClick", "return confirm_delete();")

        End If

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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(3, 45, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
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
        Me.Master.RegistrarAccesoPagina(3, 45)

        'CONTROLES DEL FORMULARIO


        'GRUPOS DE INFORMACION


    End Sub

    ''' <summary>
    ''' Graba las excepciones sobre la modificación de las Fichas de Atenciones Médicas
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub GabrarBloqueosExcepciones()

        Dim obj_BL_BloqueoFichasAtenciones As New bl_BloqueoFichasAtenciones
        Dim obj_BE_BloqueoFichasAtenciones As New be_BloqueoFichasAtenciones
        Dim str_mensaje As String = ""
        Dim dt_DetalleExcepciones As New DataTable
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        dt_DetalleExcepciones = ViewState("ListaExcepciones")

        'GRABAR BLOQUEOS GENERALES
        obj_BE_BloqueoFichasAtenciones.OmisionFichaPendiente = rbOmision.SelectedValue
        obj_BE_BloqueoFichasAtenciones.DiasModificacion = Val(tbDiasPlazo.Text)

        obj_BL_BloqueoFichasAtenciones.FUN_INS_BloqueoGeneral(obj_BE_BloqueoFichasAtenciones, str_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 45)
        'GRABAR EXCEPCIONES
        obj_BL_BloqueoFichasAtenciones.FUN_INS_DtalleExcepciones(dt_DetalleExcepciones, str_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 45)

        MostrarSexyAlertBox("Operación exitosa", "Info")

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

    ''' <summary>
    ''' Cierra el PopUp de Registro de Excepciones
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cerrarModalExcepcion()
        modal_xxx.Hide()
        tbdiasexclusion.Text = 0
    End Sub

    ''' <summary>
    ''' Lista todas las excepciones registradas en el sistema que aún esten vigentes.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ListarExcepciones()
        Dim int_Estado As Integer = CInt(rbOmision.SelectedValue)
        Dim int_Vigente As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim obj_BL_BloqueosFichaAtencion As New bl_BloqueoFichasAtenciones
        Dim ds_Lista As DataSet = obj_BL_BloqueosFichaAtencion.FUN_LIS_Excepciones(int_Vigente, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 45)

        ViewState("ListaExcepciones") = ds_Lista.Tables(0)
        gvDetalleExcepciones.DataSource = ds_Lista.Tables(0)
        gvDetalleExcepciones.DataBind()

    End Sub

    ''' <summary>
    ''' Valida la existencia del código de la Ficha de Atención Médica a registrar en la excepción.
    ''' </summary>
    ''' <param name="int_CodigoFicha">Código de Ficha de Atención Médica</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function ValidarExistenciaFichaAtencionMedica(ByVal int_CodigoFicha As Integer) As Integer
        Dim int_result As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Try
            Dim obj_BL_BloqueoFichasAtencionesMedicas As New bl_BloqueoFichasAtenciones
            int_result = obj_BL_BloqueoFichasAtencionesMedicas.FUN_VAL_CodigoFichaExcepciones(int_CodigoFicha, int_CodigoUsuario, int_CodigoTipoUsuario, 3, 45)

        Catch ex As Exception
            int_result = -1
        End Try
        
        Return int_result
    End Function

    ''' <summary>
    ''' Agrega una excepción al listado de excepciones. (Agrega la excepción a la Tabla Temporal que será almacenada despues de dar clic en grabar)
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub agregarExcepcion()

        If IsNumeric(tbCodigoFicha.Text) = False Then
            MostrarSexyAlertBox("El campo Código de Ficha debe tener un número ingresado.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        If tbCodigoFicha.Text = 0 Then
            MostrarSexyAlertBox("El campo Código de Ficha debe ser mayor a 0.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        If IsNumeric(tbdiasexclusion.Text) = False Then
            MostrarSexyAlertBox("El campo Días de exlusión debe tener un número ingresado.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        If tbdiasexclusion.Text = 0 Then
            MostrarSexyAlertBox("El campo Días de exlusión debe ser mayor a 0.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        'Verificar existencia del código de la ficha de Atención Médica
        If ValidarExistenciaFichaAtencionMedica(Val(tbCodigoFicha.Text)) <= 0 Then
            MostrarSexyAlertBox("Debe ingresar un código de Ficha de Atención existente.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        Dim dt As DataTable
        Dim boo_Incremento As Boolean = False
        Dim int_codigo_fila As Integer = 0

        If ViewState("ListaExcepciones") Is Nothing Then

            dt = New DataTable("ListaExcepciones")

            dt = Datos.agregarColumna(dt, "Correlativo", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoExcepcion", "Integer")
            dt = Datos.agregarColumna(dt, "CodigoFichaAtencion", "Integer")
            dt = Datos.agregarColumna(dt, "FechaExclusion", "String")
            dt = Datos.agregarColumna(dt, "DiasExclusion", "Integer")
            dt = Datos.agregarColumna(dt, "Vigencia", "String")
            dt = Datos.agregarColumna(dt, "Estado", "Integer")
        Else

            dt = ViewState("ListaExcepciones")

        End If

        'VALIDACION
        If dt.Rows.Count > 0 Then

            For Each auxdr As DataRow In dt.Rows

                If auxdr.Item("CodigoFichaAtencion").ToString = tbCodigoFicha.Text And auxdr.Item("Vigencia") = "Vigente" Then
                    MostrarSexyAlertBox("El código de Ficha de Atención seleccionado ya se encuentra en el listado de excepciones y aún está vigente.", "Alert")
                    modal_xxx.Show()
                    Exit Sub

                End If

            Next

        End If

        If boo_Incremento = False Then

            Dim dr As DataRow
            dr = dt.NewRow

            int_codigo_fila = dt.Rows.Count

            dr.Item("Correlativo") = Datos.AutogenerarCodigoTemporal(dt, "Correlativo")
            dr.Item("CodigoExcepcion") = 0
            dr.Item("CodigoFichaAtencion") = Val(tbCodigoFicha.Text)
            dr.Item("FechaExclusion") = tbFechaRegistroExcepcion.Text
            dr.Item("DiasExclusion") = Val(tbdiasexclusion.Text)
            dr.Item("Vigencia") = "Vigente"
            dr.Item("Estado") = 1
            dt.Rows.Add(dr)

        End If

        ViewState("ListaExcepciones") = dt

        gvDetalleExcepciones.DataSource = dt
        gvDetalleExcepciones.DataBind()

        tbCodigoFicha.Text = 0
        tbdiasexclusion.Text = 0
        tbFechaRegistroExcepcion.Text = Now.Date

        upExcepciones.Update()

    End Sub

    ''' <summary>
    ''' Editar excepción del listado de excepciones.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub editarExcepcion()

        If IsNumeric(tbCodigoFicha.Text) = False Then
            MostrarSexyAlertBox("El campo Código de Ficha debe tener un número ingresado.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        If tbCodigoFicha.Text = 0 Then
            MostrarSexyAlertBox("El campo Código de Ficha debe ser mayor a 0.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        If IsNumeric(tbdiasexclusion.Text) = False Then
            MostrarSexyAlertBox("El campo Días de exlusión debe tener un número ingresado.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        If tbdiasexclusion.Text = 0 Then
            MostrarSexyAlertBox("El campo Días de exlusión debe ser mayor a 0.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        'Verificar existencia del código de la ficha de Atención Médica
        If ValidarExistenciaFichaAtencionMedica(Val(tbCodigoFicha.Text)) <= 0 Then
            MostrarSexyAlertBox("Debe ingresar un código de Ficha de Atención existente.", "Alert")
            modal_xxx.Show()
            Exit Sub
        End If

        Dim int_CodigoOriginal As Integer = hidencodigoExcepcion.Value

        Dim dt As DataTable

        dt = ViewState("ListaExcepciones")

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("Correlativo").ToString <> int_CodigoOriginal And auxdr.Item("CodigoFichaAtencion").ToString = tbCodigoFicha.Text And auxdr.Item("Vigencia") = "Vigente" Then
                MostrarSexyAlertBox("El código de Ficha de Atención seleccionado ya se encuentra en el listado de excepciones y aún está vigente.", "Alert")
                modal_xxx.Show()
                Exit Sub

            End If

        Next

        For Each auxdr As DataRow In dt.Rows

            If auxdr.Item("Correlativo").ToString = int_CodigoOriginal Then

                auxdr.Item("CodigoFichaAtencion") = Val(tbCodigoFicha.Text)
                auxdr.Item("FechaExclusion") = tbFechaRegistroExcepcion.Text
                auxdr.Item("DiasExclusion") = Val(tbdiasexclusion.Text)

            End If

        Next

        ViewState("ListaExcepciones") = dt

        gvDetalleExcepciones.DataSource = dt
        gvDetalleExcepciones.DataBind()

        tbCodigoFicha.Text = 0
        tbdiasexclusion.Text = 0
        tbFechaRegistroExcepcion.Text = Now.Date

        upExcepciones.Update()

    End Sub

    ''' <summary>
    ''' Listar Configuración General de Bloques de Fichas de Atenciones Médicas.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ListarBloqueosGenerales()
        Dim obj_BL_BloqueosFichaAtencion As New bl_BloqueoFichasAtenciones
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado

        Dim dt_Lista As DataTable = obj_BL_BloqueosFichaAtencion.FUN_LIS_BloqueosGenerales(int_CodigoUsuario, int_CodigoTipoUsuario, 3, 45).Tables(0)

        If dt_Lista.Rows.Count > 0 Then
            tbDiasPlazo.Text = dt_Lista.Rows(0).Item("DiasModificacion")
            rbOmision.SelectedValue = IIf(dt_Lista.Rows(0).Item("OmitirPendientes") = True, 1, 0)
            hidencodigoBloqueo.Value = dt_Lista.Rows(0).Item("Codigo")
        Else
            hidencodigoBloqueo.Value = 0
        End If

    End Sub

    ''' <summary>
    ''' Invoca al PopUp de Modificación de Excepción.    '''  
    ''' </summary> 
    ''' <param name="int_Correlativo">Codigo de numero de registro de la DataTable</param>
    ''' <param name="int_CodigoFichaAtencion">Código de ficha de atención</param>
    ''' <param name="str_FechaExclusion">Fecha de Exclusión de bloqueo</param>
    ''' <param name="int_DiasExclusion">Cantidad de días de Exclusión de bloqueo</param>
    ''' <remarks>
    ''' Creador: Johnatan Matta
    ''' Fecha de Creación: 06/01/2011
    ''' Modificado por: _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub activarEditarExcepcion(ByVal int_Correlativo As Integer, ByVal int_CodigoFichaAtencion As Integer, ByVal str_FechaExclusion As String, ByVal int_DiasExclusion As Integer)

        hidencodigoExcepcion.Value = int_Correlativo
        tbCodigoFicha.Text = int_CodigoFichaAtencion
        tbdiasexclusion.Text = int_DiasExclusion
        tbFechaRegistroExcepcion.Text = str_FechaExclusion

        modal_xxx.Show()

    End Sub

    ''' <summary>
    ''' Eliminar Excepción de bloqueo de Ficha de Atención Médica.
    ''' </summary>
    ''' <param name="int_CodigoCorrelativo">Codigo de numero de registro de la DataTable</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EliminarExcepcion(ByVal int_CodigoCorrelativo As Integer)

        Dim dt As DataTable

        dt = ViewState("ListaExcepciones")

        For Each auxdr As DataRow In dt.Rows

            If Val(auxdr.Item("Correlativo").ToString) = int_CodigoCorrelativo Then
                auxdr.Delete()
                Exit For

            End If

        Next

        dt.AcceptChanges()

        ViewState("ListaEnfermedad") = dt

        gvDetalleExcepciones.DataSource = dt
        gvDetalleExcepciones.DataBind()
        upExcepciones.Update()

    End Sub

#End Region

End Class
