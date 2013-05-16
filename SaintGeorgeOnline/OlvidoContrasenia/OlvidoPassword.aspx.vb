Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_Utilities

Imports System.Data
Imports System.Security.Cryptography

''' <summary>
''' Modulo de Acceso - Olvido la contraseña
''' </summary>
''' <remarks>
''' Eventos:
''' -------
''' 1. Page_Load
''' 2. btnEnviar_Click
''' 
''' Metodos: 
''' -------
''' 1. EnvioEmailError
''' 2. EnviarContraseñas
''' 3. MostrarSexyAlertBox
''' 
''' </remarks>
Partial Class Usuarios_OlvidoPassword
    Inherits System.Web.UI.Page

    Private int_CodigoModulo As Integer = 0
    Private int_CodigoOpcion As Integer = 4

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsuario.Focus()
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            EnviarContraseñas()
        Catch ex As Exception
            EnvioEmailError(115, ex.ToString)
        End Try
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
        Dim int_TipoUsuario As Integer = 0
        Dim str_NombreUsuario As String = ""

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(int_CodigoModulo, int_CodigoOpcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Envía las credenciales de acceso a los emails registrados en el sistema para el usuario quien lo solicito.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnviarContraseñas()
        Dim obj_BL_Usuario As New bl_Logueo
        Dim dt_Emails As New DataTable
        Dim obj_UT_Email As New EnvioEmail
        Dim encript As New SaintGeorgeOnline_Utilities.Cripto

        If txtUsuario.Text.Length > 7 And txtUsuario.Text.Length < 15 Then

            dt_Emails = obj_BL_Usuario.FUN_GET_EmailDNI(txtUsuario.Text, 0, 4)

            If dt_Emails.Rows.Count > 0 Then

                Dim rutamadre As String = Server.MapPath(".")
                Dim fileReaderPlantilla As String = ""
                Dim ArchLecturaEstructura As String = ""
                Dim cont As Integer = 0
                Dim str_Email As String = ""
                Dim str_EmailCopia As String = ""
                Dim int_TipoUsuario As Integer = 0
                Dim str_PasswordEncript As String = ""
                Dim str_Password As String = ""
                Dim str_Usuario As String = ""

                str_Email = dt_Emails.Rows(0).Item("Email")
                str_EmailCopia = dt_Emails.Rows(0).Item("EmailCopiado")
                int_TipoUsuario = dt_Emails.Rows(0).Item("TipoUsuario")
                str_PasswordEncript = dt_Emails.Rows(0).Item("Contrasenia")
                str_Usuario = dt_Emails.Rows(0).Item("Usuario")

                rutamadre = rutamadre & "\"
                ArchLecturaEstructura = rutamadre

                If int_TipoUsuario = 1 Then
                    ArchLecturaEstructura = ArchLecturaEstructura & ConfigurationManager.AppSettings.Item("Recuperacontrasenia_Alumno").ToString()
                ElseIf int_TipoUsuario = 2 Then
                    ArchLecturaEstructura = ArchLecturaEstructura & ConfigurationManager.AppSettings.Item("Recuperacontrasenia_Trabajador").ToString()
                ElseIf int_TipoUsuario = 3 Then
                    ArchLecturaEstructura = ArchLecturaEstructura & ConfigurationManager.AppSettings.Item("Recuperacontrasenia_Familia").ToString()
                Else
                    MostrarSexyAlertBox("Error de Sistema. Por favor comunicarse con el área de Sistema.!", "Alert")
                    Exit Sub
                End If

                str_Password = encript.Desencriptar(New RC2CryptoServiceProvider, str_PasswordEncript)
                fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)

                fileReaderPlantilla = fileReaderPlantilla.Replace("[Fecha_Carta]", Now.Date.Day & "/" & Now.Date.Month & "/" & Now.Date.Year)
                fileReaderPlantilla = fileReaderPlantilla.Replace("[TABLA_USUARIO_CONTRASENIA]", "Usuario: <b>" & str_Usuario & "</b><BR>Contraseña: <b>" & str_Password & "</b>")
                fileReaderPlantilla = fileReaderPlantilla.Replace("[Logo_Documento]", "<img alt=''  width='156px' height='50px' src='" & ConfigurationManager.AppSettings.Item("RutaImagenEscudoLogoConTexto_Web").ToString() & "' />")

                obj_UT_Email.SendEmail_OlvidoContrasenia(str_Email, fileReaderPlantilla, "Recordatorio de Contraseña", str_EmailCopia)

                txtUsuario.Text = ""
                MostrarSexyAlertBox("Su contraseña ha sido enviada con exito a su correo.!!", "Info")
            Else
                MostrarSexyAlertBox("El Usuario no se encuentra registrado o es incorrecto, comuniquese con el área de soporte para más información.!!", "Alert")
            End If

        Else
            MostrarSexyAlertBox("Debe ingresar un Usuario válido.!!", "Alert")
        End If
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
    Protected Sub MostrarSexyAlertBox(ByVal str_mensaje As String, ByVal str_tipoMensaje As String)
        Dim SexyAlertScript As String = ""
        Select Case str_tipoMensaje
            Case "Alert"
                SexyAlertScript = "Sexy.alert('" & str_mensaje & "');"
            Case "Info"
                SexyAlertScript = "Sexy.info('" & str_mensaje & "');"
            Case "Error"
                SexyAlertScript = "Sexy.error('" & str_mensaje & "');"
        End Select
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", SexyAlertScript, True)
    End Sub

#End Region

   
End Class
