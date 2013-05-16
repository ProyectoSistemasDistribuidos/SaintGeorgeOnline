Imports System.Data
Imports System.Web
Imports System.Web.Security
Imports SaintGeorgeOnline_Utilities
Imports System.Security.Cryptography

Public Class frmLogin

#Region "Atributos"

    Private int_CodigoModulo As Integer = 0
    Private int_CodigoOpcion As Integer = 0
    Dim objfrmPrincipal As frmPrincipal

#End Region

#Region "Evitar múltiples instancias"

    Private Shared Instancia As frmLogin = Nothing

    Public Shared Function Instance() As frmLogin
        If Instancia Is Nothing OrElse Instancia.IsDisposed = True Then
            Instancia = New frmLogin
        End If
        Instancia.BringToFront()
        Return Instancia
    End Function

#End Region

#Region "Eventos"

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2

        Me.tbUsuario.Text = ""
        Me.tbPassword.Text = ""

        Me.tbUsuario.Focus()

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        logear()
    End Sub

    Private Sub tbUsuario_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbUsuario.KeyPress

        Dim KeyAscii As Integer = Asc(e.KeyChar)
        If KeyAscii = 13 Then
            logear()
        End If

    End Sub

    Private Sub tbPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbPassword.KeyPress

        Dim KeyAscii As Integer = Asc(e.KeyChar)
        If KeyAscii = 13 Then
            logear()
        End If

    End Sub

    Private Sub logear()
        Dim usp_mensaje As String = ""
        If validarLogin(usp_mensaje) Then
            VerificarAcceso()
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If
    End Sub

#End Region

#Region "Metodos"

    Private Function validarLogin(ByRef str_Mensaje As String) As Boolean
        Dim result As Boolean = True
        Dim str_alertas As String = ""

        str_alertas = "Los siguientes campos presentan errores:"

        If Not tbUsuario.Text.Length > 0 Then
            str_alertas += vbCrLf & "Usuario requiere información. No puede estar vacia."
            result = False
        End If

        If Not tbPassword.Text.Length > 0 Then
            str_alertas += vbCrLf & "Password requiere información. No puede estar vacia."
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    Private Sub VerificarAcceso()

        Dim obj_BL_Logueo As New SaintGeorgeOnline_BusinessLogic.ModuloLogueo.bl_Logueo

        Dim ds_PermisosAccesos As New DataSet
        Dim ds_DatosUsuario As New DataSet

        Dim dt_datosUsuario As New DataTable
        Dim dt_Familias As New DataTable

        Dim str_Usuario As String = ""
        Dim str_Password As String = ""
        Dim str_Ip As String = ""
        Dim str_mensaje As String = ""
        Dim str_usuarioEncriptado As String = ""
        Dim encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim str_PasswordEncript As String = ""
        Dim str_HostName As String = ""

        str_Usuario = tbUsuario.Text.Trim
        str_Password = tbPassword.Text.Trim

        str_HostName = System.Net.Dns.GetHostName()
        str_Ip = System.Net.Dns.GetHostEntry(str_HostName).AddressList(0).ToString()

        'PASO 0: Encriptar Contraseña Ingresada
        str_PasswordEncript = encript.Encriptar(New RC2CryptoServiceProvider, str_Password)

        'PASO 1: Validacion de Acceso a la Web (Obtiene Datos de Usuario)
        ds_DatosUsuario = obj_BL_Logueo.FUN_VAL_PermisosUsuario(str_Usuario, str_PasswordEncript, 0, 0)
        dt_datosUsuario = ds_DatosUsuario.Tables(0)
        dt_Familias = ds_DatosUsuario.Tables(1)

        If dt_datosUsuario.Rows.Count = 0 Then
            MostrarSexyAlertBox("Usuario y/o contraseña incorrectos", "Alert")
        Else
            Dim SuperUsuario As Boolean = False
            Dim int_TipoUsuario As Integer = 0

            'PASO 2: Verifica si es super usuario
            SuperUsuario = dt_datosUsuario.Rows(0).Item("SuperUsuario")
            int_TipoUsuario = dt_datosUsuario.Rows(0).Item("TipoUsuario")

            'PASO 3: Registro de Acceso a la Web en el Log
            Dim obj_BE_Usuario As New SaintGeorgeOnline_BusinessEntities.ModuloLogueo.be_Usuario
            Dim int_CodigoSession As Integer = 0

            obj_BE_Usuario.CodigoUsuario = dt_datosUsuario.Rows(0).Item("Codigo")
            obj_BE_Usuario.TipoUsuario = dt_datosUsuario.Rows(0).Item("TipoUsuario")
            obj_BE_Usuario.IpUsuario = str_Ip
            obj_BE_Usuario.HostUsuario = str_HostName
            int_CodigoSession = obj_BL_Logueo.FUN_INS_AccesoUsuario(obj_BE_Usuario, str_mensaje, 0, 0)

            Dim int_CodigoPerfil As Integer = dt_datosUsuario.Rows(0).Item("CodigoPerfil")
            CargarForm(int_CodigoPerfil)

        End If

    End Sub

    Private Function RegistrarUsuarioLogueado(ByVal str_Usuario As String, ByVal dt_InformacionUsuario As DataTable, ByVal int_SessionUsuario As Integer) As String

        Dim obj_ticket As FormsAuthenticationTicket
        Dim obj_cookie As HttpCookie
        Dim obj_encript As New SaintGeorgeOnline_Utilities.Cripto

        Dim sb_datausuario As New System.Text.StringBuilder
        Dim str_hash As String
        Dim dt_DatosUsuario As New DataTable

        dt_DatosUsuario = dt_InformacionUsuario

        With sb_datausuario
            .Append(dt_DatosUsuario.Rows(0)("Codigo")) 'Codigo Usuario - indice 0
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("TipoUsuario")) 'Tipo Usuario - indice 1
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("Nombres")) 'Nombre Usuario - indice 2   
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("RutaFoto")) 'Ruta de Foto - indice 3  
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("SuperUsuario")) 'Super Usuario - indice 4
            .Append(";")
            .Append(int_SessionUsuario) 'Codigo de Session del Usuario - indice 5
            .Append(";")
            .Append(0) 'Usuario de Referencia - indice 6 
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("EmailUsuario")) 'Email de Usuario del tipo Trabajador - indide 7 
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("CodigoFamiliaActiva")) 'Codigo de Familia Activa del familiar - indice 8
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("DescripcionFamilia")) 'Nombre de Familia Activa del familiar - indice 9
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("PeriodoActivo")) 'Periodo Activo - indice 10
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("CodigoPeriodoActivo")) 'Codigo de Periodo Activo - indice 11
            .Append(";")
            .Append(dt_DatosUsuario.Rows(0)("CodigoPerfil")) 'Codigo de Perfil  - indice 12
        End With

        'Creamos el ticket de autenticacion,
        'ademas encriptamos la data del usuario

        'Le damos un primer nivel de seguridad,
        'encriptando el nombre del usuario

        str_Usuario = obj_encript.Encriptar(New RC2CryptoServiceProvider, str_Usuario)

        obj_ticket = New FormsAuthenticationTicket _
                    (0, str_Usuario, _
                    Date.Now, _
                    Date.Now.AddMinutes(60), False, _
                    obj_encript.Encriptar(New RC2CryptoServiceProvider, sb_datausuario.ToString), _
                    FormsAuthentication.FormsCookiePath)


        str_hash = FormsAuthentication.Encrypt(obj_ticket)

        obj_cookie = New HttpCookie(FormsAuthentication.FormsCookieName, str_hash)

        HttpContext.Current.Response.Cookies.Add(obj_cookie)

        Return str_Usuario
    End Function

    Protected Sub MostrarSexyAlertBox(ByVal str_mensaje As String, ByVal str_tipoMensaje As String)
        Dim SexyAlertScript As String = ""
        Dim str_title As String = "Saint George Online's Message"
        Select Case str_tipoMensaje
            Case "Alert"
                MsgBox(str_mensaje, MsgBoxStyle.Exclamation, str_title)
            Case "Info"
                MsgBox(str_mensaje, MsgBoxStyle.Information, str_title)
            Case "Error"
                MsgBox(str_mensaje, MsgBoxStyle.Critical, str_title)
        End Select

    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(int_CodigoModulo, int_CodigoOpcion, int_CodigoAccion, str_DetalleError, "", 0)
        MsgBox(str_MensajeUsuario)
    End Sub

    Private Sub CargarForm(ByRef int_CodigoPerfil As Integer)
        objfrmPrincipal = New frmPrincipal
        objfrmPrincipal.CodigoPerfil = int_CodigoPerfil
        objfrmPrincipal.Show()
        Me.Hide()
    End Sub

#End Region

End Class