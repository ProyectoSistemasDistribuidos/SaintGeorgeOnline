Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography

''' <summary>
''' Modulo de Ingreso de Alergias - PopUp
''' </summary>
''' <remarks>
''' Eventos:
''' -------
''' 1. Page_Load
''' 2. btnGrabar_IngresarAlergias_Click
''' 3. btnCerrar_IngresarAlergias_Click
''' 
''' Metodos Públicos:
''' ----------------
''' 1. setearParametros
''' 2. ocultarModal
''' 3. mostrarModal
''' 4. mostrarModalPadre
''' 5. Grabar
''' 6. cargarComboTipoAlergia
''' 
''' Metodos Privados:
''' ----------------
''' 1. limpiarFormulario
''' 2. validarGrabar
''' 3. actualizarComboPadre
''' 
''' Metodos Generales:
''' -----------------
''' 1. MostrarMensaje
''' 2. MostrarSexyAlertBox
''' 3. Obtener_CodigoTipoUsuarioLogueado
''' 4. Obtener_CodigoUsuarioLogueado
''' 5. EnvioEmailError
''' </remarks>
Partial Class Controles_IngresarAlergia
    Inherits System.Web.UI.UserControl

    Private int_CodigoModulo As Integer = 0
    Private int_CodigoOpcion As Integer = 9

#Region "Propiedades"
    Public Property ComboPadreID() As String
        Get
            Return hiddenComboPadreID.Value
        End Get
        Set(ByVal value As String)
            hiddenComboPadreID.Value = value
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
        Try
            Me.tbDescripcion.Attributes.Add("onkeypress", "return ValidarLength(this, 100);")
            Me.tbDescripcion.Attributes.Add("onkeyup", "return ValidarLength(this, 100);")
            Me.FilteredTextBoxExtender1.ValidChars = FilteredTextBoxExtender1.ValidChars & vbCrLf
            If Not Page.IsPostBack Then
                cargarComboTipoAlergia()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_IngresarAlergias_Click()
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
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCerrar_IngresarAlergias_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If
        Me.ocultarModal()

    End Sub

#End Region

#Region "Métodos Públicos"

    Public Sub setearParametros(ByVal str_ComboPadreID As String, ByVal str_ComboPadreTipo As String, ByVal bool_TieneModalPadre As Boolean, ByVal str_ModalPadreID As String)

        Me.ComboPadreID = str_ComboPadreID
        Me.tieneModalPadre = bool_TieneModalPadre
        Me.ModalPadreID = str_ModalPadreID

    End Sub

    Public Sub ocultarModal()

        Me.ModalPopupExtender_IngresarAlergias.Hide()
        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

    End Sub

    Public Sub mostrarModal()

        Me.limpiarFormulario()
        Me.ModalPopupExtender_IngresarAlergias.Show()
        If Me.tieneModalPadre Then
            Me.mostrarModalPadre()
        End If

    End Sub

    Private Sub mostrarModalPadre()
        Dim str_NombreModalPadre As String = ModalPadreID
        Dim modal As AjaxControlToolkit.ModalPopupExtender = CType(Page.FindControl(str_NombreModalPadre), AjaxControlToolkit.ModalPopupExtender)
        modal.Show()
    End Sub

    Private Sub Grabar()

        Dim obj_BE_Alergia As New be_Alergias
        Dim obj_BL_Alergia As New bl_Alergias
        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer
        Dim int_CodigoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Obtener_CodigoTipoUsuarioLogueado
        obj_BE_Alergia.CodigoTipoAlergia = CInt(ddlTipo.SelectedValue)
        obj_BE_Alergia.Descripcion = Me.tbDescripcion.Text.Trim
        usp_valor = obj_BL_Alergia.FUN_INS_Alergia(obj_BE_Alergia, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 51)
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
  
    ''' <summary>
    ''' Carga el listado del combo de tipo de Alergia
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang 
    ''' Fecha de Creación:     08/008/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoAlergia()
        Dim obj_BL_TipoAlergia As New bl_TiposAlergias
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_TipoAlergia.FUN_LIS_TipoAlergia("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 51)

        Controles.llenarCombo(ddlTipo, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

#End Region

#Region "Métodos Privados"

    Private Sub limpiarFormulario()

        Me.tbDescripcion.Text = ""
        ddlTipo.SelectedValue = 0
        tbDescripcion.Focus()

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
        Dim obj_BL_Alergias As New bl_Alergias
        Dim obj_BE_Alergias As New be_Alergias
        Dim int_CodigoTipoAlergia As Integer = obj_BE_Alergias.CodigoTipoAlergia
        Dim ds_Lista As DataSet = obj_BL_Alergias.FUN_LIS_Alergia(str_Descripcion, int_CodigoTipoAlergia, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Dim str_NombreComboPadre As String = ComboPadreID
        Dim combo As DropDownList = CType(Page.FindControl(str_NombreComboPadre), DropDownList)
        Controles.llenarCombo(combo, ds_Lista, "Codigo", "Descripcion", False, True)
        combo.SelectedValue = Me.CodigoRegistro

    End Sub

#End Region

#Region "Metodos Generales"

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

    Private Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Me.MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

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
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(int_CodigoModulo, int_CodigoOpcion, int_CodigoAccion, str_DetalleError, "", 0)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region

End Class
