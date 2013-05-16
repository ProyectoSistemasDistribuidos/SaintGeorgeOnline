Imports System.Data
Imports System.Web
Imports System.Web.Security
Imports SaintGeorgeOnline_Utilities
Imports System.Security.Cryptography

''' <summary>
''' Modulo de Acceso - Login
''' </summary>
''' <remarks>
''' Eventos:
''' -------
''' 1. Page_Load
''' 2. btn_Ingresar_Click
''' 
''' Metodos: 
''' -------
''' 1. VerificarAcceso
''' 2. RegistrarUsuarioLogueado
''' 
''' Metodos Generales:
''' -----------------
''' 1. MostrarSexyAlertBox
''' 2. EnvioEmailError
''' </remarks>
Partial Class Acceso_Login
    Inherits System.Web.UI.Page

    Private int_CodigoModulo As Integer = 0
    Private int_CodigoOpcion As Integer = 0

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsuario.Focus()
    End Sub

    Protected Sub btn_Ingresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Ingresar.Click
        Try
            Dim usp_mensaje As String = ""
            If validarLogin(usp_mensaje) Then
                VerificarAcceso()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If

        Catch ex As Exception
            EnvioEmailError(10, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Function validarLogin(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If txtUsuario.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Usuario")
            result = False
        End If

        If txtContrasenia.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Contraseña")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Verifica las credenciales de Acceso al Sistema.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerificarAcceso()

        Me.Validate()
        If IsValid Then

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

            str_Usuario = txtUsuario.Text.Trim
            str_Password = txtContrasenia.Text.Trim
            str_Ip = Request.ServerVariables.Item("REMOTE_ADDR")

            Try
                str_HostName = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
            Catch ex As Exception
                str_HostName = ""
            End Try

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

                'PASO 4: Encriptacion de Arry de Datos de la Session a la Web
                str_usuarioEncriptado = RegistrarUsuarioLogueado(str_Usuario, dt_datosUsuario, int_CodigoSession)

                If SuperUsuario = True And int_TipoUsuario = 2 Then
                    Response.Redirect("/SaintGeorgeOnline/Interfaz_Logueo.aspx", False)
                Else
                    'PASO 5: Obtencion de Permisos a Usuario Validado
                    ds_PermisosAccesos = obj_BL_Logueo.FUN_GET_PermisosUsuario(obj_BE_Usuario.CodigoUsuario, obj_BE_Usuario.TipoUsuario, 0, 0)
                    Session("Ac_Pe_Us") = ds_PermisosAccesos

                    'PASO 6: Direccionar a Interfaz correspondiente
                    If int_TipoUsuario = 1 Then 'Alumnos
                        Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/HomeAlumnos.aspx", False)
                    ElseIf int_TipoUsuario = 2 Then 'Trabajadores
                        Response.Redirect("/SaintGeorgeOnline/Principal.aspx", False)
                    ElseIf int_TipoUsuario = 3 Then 'Familiar
                        Session("Ac_Pe_Us_Familias") = dt_Familias

                        If dt_Familias.Rows.Count > 1 Then
                            Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Interfaz_LogueoFamilia.aspx", False)
                        Else
                            Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Principal.aspx", False)
                        End If

                    End If
                End If

            End If

        End If
    End Sub

    ''' <summary>
    ''' Registra Datos del Usuario logueado a la cookie de la sesión.
    ''' </summary>
    ''' <param name="str_Usuario">Nombre de Usuario.</param>
    ''' <param name="dt_InformacionUsuario">Tabla Temporal con datos del Usuario.</param>
    ''' <param name="int_SessionUsuario">Codigo de Sesión del Usuario.</param>
    ''' <returns>Nombre del Usuario encriptado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
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

#End Region

#Region "Metodos Generales"

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
