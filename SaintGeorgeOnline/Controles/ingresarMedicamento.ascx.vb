Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Class Controles_ingresarMedicamento
    Inherits System.Web.UI.UserControl

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Propiedades"

    Public Property ComboPadreID() As String
        Get
            Return hiddencomboPadreID.Value
        End Get
        Set(ByVal value As String)
            hiddencomboPadreID.Value = value
        End Set
    End Property

    Public Property ModalPadreID() As String
        Get
            Return hiddenModalPadreID.Value
        End Get
        Set(ByVal value As String)
            hiddenModalPadreID.Value = value
        End Set
    End Property

    Public Property tieneModalPadre() As Boolean
        Get
            Return hiddenTieneModalPadreID.Value
        End Get
        Set(ByVal value As Boolean)
            hiddenTieneModalPadreID.Value = value
        End Set
    End Property

    Public Property CodigoRegistro() As Integer
        Get
            Return hiddenCodigoRegistro.Value
        End Get
        Set(ByVal value As Integer)
            hiddenCodigoRegistro.Value = value
        End Set
    End Property

    Public Property CodigoNombreMedicamento() As Integer
        Get
            Return hiddenCodigoNombreMedicamento.Value
        End Get
        Set(ByVal value As Integer)
            hiddenCodigoNombreMedicamento.Value = value
        End Set
    End Property

    Public Property CodigoPresentacion() As Integer
        Get
            Return hiddenCodigoPresentacion.Value
        End Get
        Set(ByVal value As Integer)
            hiddenCodigoPresentacion.Value = value
        End Set
    End Property

    Public Property CodigoUnidad() As Integer
        Get
            Return hiddenCodigoUnidad.Value
        End Get
        Set(ByVal value As Integer)
            hiddenCodigoUnidad.Value = value
        End Set
    End Property

    Public Property tieneControlesAuxiliares() As Boolean
        Get
            Return hiddenTieneControlesAuxiliares.Value
        End Get
        Set(ByVal value As Boolean)
            hiddenTieneControlesAuxiliares.Value = value
        End Set
    End Property

    Public Property ControlAuxiliarLabel() As String
        Get
            Return hiddenControlAuxiliarLabelID.Value
        End Get
        Set(ByVal value As String)
            hiddenControlAuxiliarLabelID.Value = value
        End Set
    End Property

    Public Property ControlAuxiliarTextbox() As String
        Get
            Return hiddenControlAuxiliarTextBoxID.Value
        End Get
        Set(ByVal value As String)
            hiddenControlAuxiliarTextBoxID.Value = value
        End Set
    End Property

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Me.cargarComboNombres()
            Me.cargarComboPresentacion()
            Controles.limpiarCombo(ddlUnidadMedida, False, True)
            Me.ddlUnidadMedida.Enabled = False
        End If
    End Sub

    Protected Sub btnGrabar_IngresarMedicamento_Click()
        Try
            Dim usp_mensaje As String = ""
            If Me.validarGrabar(usp_mensaje) Then
                Me.Grabar()
            Else
                Me.MostrarSexyAlertBox(usp_mensaje, "Alert")
                If Me.tieneModalPadre Then
                    Me.mostrarModalPadre()
                End If
                Me.mostrarModal()
            End If
        Catch ex As Exception
            'EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCerrar_IngresarMedicamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If
        Me.ocultarModal()
    End Sub


    Protected Sub btnAgregarRegistroNombreMedicamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

        Me.mostrarModal_NombreMedicamento()

    End Sub

    Protected Sub btnAgregarRegistroPresentacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

        Me.mostrarModal_Presentacion()

    End Sub

    Protected Sub ddlPresentacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            actualizarModal()
            Controles.limpiarCombo(ddlUnidadMedida, False, True)
            cargarComboUnidad()

        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Métodos Públicos"

    Public Sub setearParametros(ByVal str_ComboPadreID As String, ByVal bool_TieneModalPadre As Boolean, ByVal str_ModalPadreID As String)
        Me.ComboPadreID = str_ComboPadreID
        Me.tieneModalPadre = bool_TieneModalPadre
        Me.ModalPadreID = str_ModalPadreID
        Me.limpiarFormulario()
    End Sub

    Public Sub ocultarModal()

        Me.ModalPopupExtender_IngresarMedicamento.Hide()
        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

    End Sub

    Public Sub mostrarModal()

        Me.ModalPopupExtender_IngresarMedicamento.Show()
        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

    End Sub

    Private Sub actualizarModal()

        Me.ModalPopupExtender_IngresarMedicamento.Show()
        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

    End Sub

    Public Sub Grabar()

        Dim obj_BE_Medicamento As New be_Medicamentos
        Dim obj_BL_Medicamento As New bl_Medicamentos
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_Medicamento.CodigoNombre = Me.ddlNombre.SelectedValue
        obj_BE_Medicamento.CodigoPresentacion = Me.ddlPresentacion.SelectedValue
        obj_BE_Medicamento.CodigoUnidadMedida = Me.ddlUnidadMedida.SelectedValue
        obj_BE_Medicamento.Cantidad = IIf(Me.tbCantidad.Text.Trim.Length = 0, 0, Me.tbCantidad.Text.Trim)
        obj_BE_Medicamento.Concentracion = IIf(Me.tbCantidad.Text.Trim.Length = 0, "", Me.tbConcentracion.Text.Trim)

        obj_BE_Medicamento.Control = rbControl.SelectedValue

        usp_valor = obj_BL_Medicamento.FUN_INS_Medicamento(obj_BE_Medicamento, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 26)

        If usp_valor > 0 Then
            Me.CodigoRegistro = usp_valor
            Me.MostrarSexyAlertBox(usp_mensaje, "Info")
            Me.actualizarComboPadre()
        Else
            Me.MostrarSexyAlertBox(usp_mensaje, "Alert")
            Me.mostrarModal()
        End If

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

    End Sub

#End Region

#Region "Métodos Privados"

    Private Sub mostrarModalPadre()
        Dim str_NombreModalPadre As String = ModalPadreID
        Dim modal As AjaxControlToolkit.ModalPopupExtender = CType(Page.FindControl(str_NombreModalPadre), AjaxControlToolkit.ModalPopupExtender)
        modal.Show()

    End Sub

    Private Sub cargarComboNombres()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim obj_BL_NombreMedicamento As New bl_NombresMedicamentos
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_NombreMedicamento.FUN_LIS_NombreMedicamento(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlNombre, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub cargarComboPresentacion()

        Dim obj_BL_RelacionPresentacionUnidadMedidaMedicamento As New bl_RelacionPresentacionesUnidadesMedidasMedicamentos
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_RelacionPresentacionUnidadMedidaMedicamento.FUN_LIS_PresentacionMedicaDisponible(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPresentacion, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    Private Sub cargarComboUnidad()

        Dim int_CodigoPresetacion As Integer = ddlPresentacion.SelectedValue
        Dim obj_BL_RelacionPresentacionUnidadMedidaMedicamento As New bl_RelacionPresentacionesUnidadesMedidasMedicamentos
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoUsuarioLogueado

        Dim ds_Lista As DataSet = obj_BL_RelacionPresentacionUnidadMedidaMedicamento.FUN_LIS_UnidadMedidaxPresentacionMedicaDisponible(int_CodigoPresetacion, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlUnidadMedida, ds_Lista, "Codigo", "Descripcion", False, True)

        If ds_Lista.Tables(0).Rows.Count > 0 Then
            ddlUnidadMedida.Enabled = True
        Else
            ddlUnidadMedida.Enabled = False
        End If

    End Sub

    Private Sub limpiarFormulario()

        Me.ddlNombre.SelectedValue = 0
        Me.tbConcentracion.Text = ""
        Me.ddlPresentacion.SelectedValue = 0
        Me.tbCantidad.Text = ""
        Me.ddlPresentacion.SelectedValue = 0
        Me.rbControl.SelectedValue = 0
        Me.ddlUnidadMedida.SelectedValue = 0
    End Sub

    Private Function validarGrabar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If Me.ddlNombre.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Nombre")
            result = False
        End If

        If Me.ddlPresentacion.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Presentación")
            result = False
        End If

        If Me.ddlUnidadMedida.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Unidad de Medida")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub actualizarComboPadre()

        Dim obj_BL_Medicamentos As New bl_Medicamentos
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoUsuarioLogueado

        Dim int_CodigoSede As Integer = 1
        Dim str_Descripcion As String = ""
        Dim ds_Lista As DataSet = obj_BL_Medicamentos.FUN_LIS_Medicamento(0, 0, 0, 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 46)

        Dim str_NombreComboPadre As String = ComboPadreID
        Dim combo As DropDownList = CType(Page.FindControl(str_NombreComboPadre), DropDownList)

        Controles.llenarCombo(combo, ds_Lista, "Codigo", "NombreCompleto", False, True)
        combo.SelectedValue = Me.CodigoRegistro
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

#End Region

#Region "Seguridad"

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

#End Region

#Region "Registro de Nombres de Medicamento"

#Region "Eventos"

    Protected Sub btnGrabar_IngresarNombresMedicamento_Click()
        Try
            Dim usp_mensaje As String = ""
            If Me.validarGrabar_NombreMedicamento(usp_mensaje) Then
                Me.Grabar_NombreMedicamento()
            Else
                Me.MostrarSexyAlertBox(usp_mensaje, "Alert")
                Me.mostrarModal_NombreMedicamento()
            End If
        Catch ex As Exception
            'EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCerrar_IngresarNombresMedicamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Me.ocultarModal_NombreMedicamento()

    End Sub

#End Region

#Region "Métodos"

    Private Sub limpiarFormulario_NombreMedicamento()

        Me.tbDescripcion_NombreMedicamento.Text = ""

    End Sub

    Private Sub mostrarModal_NombreMedicamento()

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If
        Me.ModalPopupExtender_IngresarMedicamento.Show()
        Me.ModalPopupExtender_IngresarNombresMedicamento.Show()
        'Me.limpiarFormulario()
    End Sub

    Private Sub ocultarModal_NombreMedicamento()

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If
        Me.ModalPopupExtender_IngresarMedicamento.Show()
        Me.ModalPopupExtender_IngresarNombresMedicamento.Hide()

    End Sub

    Private Function validarGrabar_NombreMedicamento(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If Me.tbDescripcion_NombreMedicamento.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripcion")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub Grabar_NombreMedicamento()

        Dim obj_BE_NombreMedicamento As New be_NombresMedicamentos
        Dim obj_BL_NombreMedicamento As New bl_NombresMedicamentos
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_NombreMedicamento.Descripcion = Me.tbDescripcion_NombreMedicamento.Text.Trim
        usp_valor = obj_BL_NombreMedicamento.FUN_INS_NombreMedicamento(obj_BE_NombreMedicamento, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            Me.actualizarCombo_NombreMedicamento(usp_valor)
            Me.MostrarSexyAlertBox(usp_mensaje, "Info")
            Me.ocultarModal_NombreMedicamento()
        Else
            Me.MostrarSexyAlertBox(usp_mensaje, "Alert")
            Me.mostrarModal_NombreMedicamento()
        End If

    End Sub

    Private Sub actualizarCombo_NombreMedicamento(ByVal int_Codigo As Integer)

        cargarComboNombres()
        ddlNombre.SelectedValue = int_Codigo

    End Sub

#End Region

#End Region

#Region "Registro de Presentación"

#Region "Eventos"

    Protected Sub btnGrabar_IngresarPresentacion_Click()
        Try
            Dim usp_mensaje As String = ""
            If Me.validarGrabar_Presentacion(usp_mensaje) Then
                Me.Grabar_Presentacion()
            Else
                Me.MostrarSexyAlertBox(usp_mensaje, "Alert")
                Me.mostrarModal_Presentacion()
            End If
        Catch ex As Exception
            'EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCerrar_IngresarPresentacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Me.ocultarModal_Presentacion()

    End Sub

#End Region

#Region "Métodos"

    Private Sub limpiarFormulario_Presentacion()

        Me.tbDescripcion_Presentacion.Text = ""

    End Sub

    Private Sub mostrarModal_Presentacion()

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If
        Me.ModalPopupExtender_IngresarMedicamento.Show()
        Me.ModalPopupExtender_IngresarPresentacion.Show()
        Me.limpiarFormulario_Presentacion()

    End Sub

    Private Sub ocultarModal_Presentacion()

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If
        Me.ModalPopupExtender_IngresarMedicamento.Show()
        Me.ModalPopupExtender_IngresarPresentacion.Hide()

    End Sub

    Private Function validarGrabar_Presentacion(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If Me.tbDescripcion_Presentacion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripcion")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub Grabar_Presentacion()

        Dim obj_BE_PresentacionMedicamento As New be_PresentacionesMedicamentos
        Dim obj_BL_PresentacionMedicamento As New bl_PresentacionesMedicamentos
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_PresentacionMedicamento.Descripcion = Me.tbDescripcion_Presentacion.Text.Trim
        usp_valor = obj_BL_PresentacionMedicamento.FUN_INS_PresentacionMedicamento(obj_BE_PresentacionMedicamento, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            Me.actualizarCombo_Presentacion(usp_valor)
            Me.actualizarCombo_PresentacionUnidadMedida()
            Me.MostrarSexyAlertBox(usp_mensaje, "Info")
            Me.ocultarModal_Presentacion()
        Else
            Me.MostrarSexyAlertBox(usp_mensaje, "Alert")
            Me.mostrarModal_Presentacion()
        End If

    End Sub

    Private Sub actualizarCombo_Presentacion(ByVal int_Codigo As Integer)

        cargarComboPresentacion()
        Me.ddlPresentacion.SelectedValue = int_Codigo

    End Sub

    Private Sub actualizarCombo_PresentacionUnidadMedida()

        Controles.limpiarCombo(ddlUnidadMedida, False, True)
        Me.cargarComboUnidad()
        Me.ddlUnidadMedida.SelectedValue = 5

    End Sub

#End Region

#End Region

End Class
