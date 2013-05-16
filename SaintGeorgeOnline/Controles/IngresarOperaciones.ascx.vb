Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Partial Class Controles_IngresarOperaciones
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
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.tbDescripcion.Attributes.Add("onkeypress", "return ValidarLength(this, 100);")
        Me.tbDescripcion.Attributes.Add("onkeyup", "return ValidarLength(this, 100);")
        Me.FilteredTextBoxExtender1.ValidChars = FilteredTextBoxExtender1.ValidChars & vbCrLf

    End Sub

    Protected Sub btnGrabar_IngresarOperaciones_Click()
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

    Protected Sub btnCerrar_IngresarOperaciones_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If
        Me.ocultarModal()

    End Sub

#End Region

#Region "Métodos Públicos"

    Public Sub setearParametros(ByVal str_ComboPadreID As String, ByVal bool_TieneModalPadre As Boolean, ByVal str_ModalPadreID As String)

        Me.ComboPadreID = str_ComboPadreID
        Me.tieneModalPadre = bool_TieneModalPadre
        Me.ModalPadreID = str_ModalPadreID

    End Sub

    Public Sub ocultarModal()

        Me.ModalPopupExtender_IngresarOperaciones.Hide()
        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

    End Sub

    Public Sub mostrarModal()

        Me.limpiarFormulario()
        Me.ModalPopupExtender_IngresarOperaciones.Show()
        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

    End Sub

    Private Sub mostrarModalPadre()
        Dim str_NombreModalPadre As String = ModalPadreID
        Dim modal As AjaxControlToolkit.ModalPopupExtender = CType(Page.FindControl(str_NombreModalPadre), AjaxControlToolkit.ModalPopupExtender)
        modal.Show()
    End Sub

    Public Sub Grabar()

        Dim obj_BE_Operaciones As New be_TiposOperacionesMedicas
        Dim obj_BL_Operaciones As New bl_TiposOperacionesMedicas
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado

        obj_BE_Operaciones.Descripcion = Me.tbDescripcion.Text.Trim
        usp_valor = obj_BL_Operaciones.FUN_INS_TipoOperacionMedica(obj_BE_Operaciones, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

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

    Private Sub limpiarFormulario()

        Me.tbDescripcion.Text = ""

    End Sub

    Private Function validarGrabar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbDescripcion.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Descripcion")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub actualizarComboPadre()

        Dim str_Descripcion As String = ""
        Dim int_Estado As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim obj_BL_Operaciones As New bl_TiposOperacionesMedicas
        Dim ds_Lista As DataSet = obj_BL_Operaciones.FUN_LIS_TipoOperacionMedica(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim str_NombreComboPadre As String = ComboPadreID
        Dim combo As DropDownList = CType(Page.FindControl(str_NombreComboPadre), DropDownList)
        Controles.llenarCombo(combo, ds_Lista, "Codigo", "Descripcion", False, True)
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
End Class
