Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo

''' <summary>
''' Modulo de Mantenimiento de Enfermedades
''' </summary>
''' <remarks>
''' Código del Modulo:    0
''' Código de la Opción:  2
''' </remarks>
Partial Class Interfaz_Familia_Interfaz_LogueoFamilia
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                CargarFamilias()
                VerificacionLogueo()
            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnContinuar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ContinuarProceso()
        Catch ex As Exception
            EnvioEmailError(10, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub SetearAccionesAcceso()
        RegistrarAccesoPagina(0, 2)
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
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(0, 2, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Obtiene las Familias relacionadas al usuario logueado en el sistema. (Para la selección de familia con quien entrar al sistema)
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CargarFamilias()

        Dim dt_Familias As New DataTable
        dt_Familias = Session("Ac_Pe_Us_Familias")
        rbl_Familias.DataSource = dt_Familias
        rbl_Familias.DataTextField = "Descripcion"
        rbl_Familias.DataValueField = "Codigo"
        rbl_Familias.DataBind()
        rbl_Familias.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Verifica y obtiene el usuario logueado al sistema.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub VerificacionLogueo()

        Dim identity As FormsIdentity = HttpContext.Current.User.Identity
        Dim ticket As FormsAuthenticationTicket = identity.Ticket
        Dim str_Info As String = ""
        Dim encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim str_ArrayDatos() As String
        Dim SuperUsuario As Boolean = False
        Dim TipoUsuario As Integer = 0
        Dim NombreUsuario As String = ""

        str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
        str_ArrayDatos = str_Info.Split(";")

        SuperUsuario = str_ArrayDatos(4)
        TipoUsuario = str_ArrayDatos(1)
        NombreUsuario = str_ArrayDatos(2)

        lbl_UsuarioLogueado.Text = NombreUsuario

    End Sub

    ''' <summary>
    ''' Actualiza la sesión del Usuario en la Cookie creada anteriormente.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub ContinuarProceso()

        If Not Session("Ac_Pe_Us_Familias") Is Nothing Then
            Dim dt_Familias As New DataTable
            Dim int_cont As Integer = 0
            Dim str_CodigoFamiliaActiva As String = ""
            Dim str_NombreFamiliaActiva As String = ""

            dt_Familias = Session("Ac_Pe_Us_Familias")

            While int_cont <= dt_Familias.Rows.Count - 1

                If dt_Familias.Rows(int_cont).Item("Codigo") = rbl_Familias.SelectedValue Then
                    dt_Familias(int_cont).Item("Seleccion") = 1

                    str_CodigoFamiliaActiva = dt_Familias.Rows(int_cont).Item("Codigo")
                    str_NombreFamiliaActiva = dt_Familias.Rows(int_cont).Item("Descripcion")

                    'Actualizar la familia activa
                    Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                    Dim ticket As FormsAuthenticationTicket = identity.Ticket
                    Dim encript As New SaintGeorgeOnline_Utilities.Cripto
                    Dim cookie As HttpCookie
                    Dim datausuario As New System.Text.StringBuilder

                    Dim hash As String = ""
                    Dim NombreUsuario As String = ""
                    Dim str_Info As String = ""
                    Dim str_ArrayDatos() As String


                    str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                    str_ArrayDatos = str_Info.Split(";")

                    NombreUsuario = ticket.Name

                    With datausuario
                        .Append(str_ArrayDatos(0)) 'Codigo Usuario - indice 0
                        .Append(";")
                        .Append(str_ArrayDatos(1)) 'Tipo Usuario - indice 1
                        .Append(";")
                        .Append(str_ArrayDatos(2)) 'Nombre Usuario - indice 2   
                        .Append(";")
                        .Append(str_ArrayDatos(3)) 'Ruta de Foto - indice 3  
                        .Append(";")
                        .Append(str_ArrayDatos(4)) 'Super Usuario - indice 4
                        .Append(";")
                        .Append(str_ArrayDatos(5)) 'Codigo de Session del Usuario - indice 5
                        .Append(";")
                        .Append(0) 'Usuario de Referencia - indice 6 
                        .Append(";")
                        .Append(str_ArrayDatos(7)) 'Email de Usuario del tipo Trabajador - indide 7 
                        .Append(";")
                        .Append(str_CodigoFamiliaActiva) 'Codigo de Familia Activa del familiar - indice 8
                        .Append(";")
                        .Append(str_NombreFamiliaActiva) 'Nombre de Familia Activa del familiar - indice 9
                        .Append(";")
                        .Append(str_ArrayDatos(10)) 'Periodo Activo - indice 10
                        .Append(";")
                        .Append(str_ArrayDatos(11)) 'Codigo de Periodo Activo - indice 11
                    End With

                    'Actualizar Ticket de Sesion
                    ticket = New FormsAuthenticationTicket _
                    (0, NombreUsuario, _
                    Date.Now, _
                    Date.Now.AddMinutes(60), False, _
                    encript.Encriptar(New RC2CryptoServiceProvider, datausuario.ToString), _
                    FormsAuthentication.FormsCookiePath)

                    hash = FormsAuthentication.Encrypt(ticket)

                    cookie = New HttpCookie(FormsAuthentication.FormsCookieName, hash)

                    HttpContext.Current.Response.Cookies.Add(cookie)

                Else
                    dt_Familias(int_cont).Item("Seleccion") = 0
                End If

                int_cont = int_cont + 1
            End While

            Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Principal.aspx", False)
        Else
            Response.Redirect("/SaintGeorgeOnline/Acceso/Acceso_Error.aspx", False)
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

    ''' <summary>
    ''' Obtiene el código del usuario logueado al sistema
    ''' </summary>
    ''' <returns>código de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoUsuarioLogueado() As Integer

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
            EnvioEmailError("Obtener Codigo de Trabajador logueado", ex.ToString)
            int_CodigoUsuarioLogueado = -1
        End Try

        Return int_CodigoUsuarioLogueado

    End Function

    ''' <summary>
    ''' Obtiene el código del tipo de usuario logueado al sistema
    ''' </summary>
    ''' <returns>código de tipo de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoTipoUsuarioLogueado() As Integer

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
            EnvioEmailError("Obtener Codigo de Trabajador logueado", ex.ToString)
            int_CodigoTipoUsuarioLogueado = -1
        End Try

        Return int_CodigoTipoUsuarioLogueado

    End Function

    ''' <summary>
    ''' Obtiene el nombre del usuario logueado al sistema.
    ''' </summary>
    ''' <returns>Nombre de Usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_NombreUsuarioLogueado() As String

        Dim str_NombreUsuarioLogueado As String = ""

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            str_NombreUsuarioLogueado = str_ArrayDatos(2)

        Catch ex As Exception
            EnvioEmailError("Obtener Nombre de Trabajador logueado", ex.ToString)
            str_NombreUsuarioLogueado = ""
        End Try

        Return str_NombreUsuarioLogueado

    End Function

    ''' <summary>
    ''' Registra el acceso al formulario. (Log de Accesos)
    ''' </summary>
    ''' <param name="int_CodigoSubBloque">Codigo del SubBloque de Menú.</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub RegistrarAccesoPagina(ByVal int_CodigoModulo As Integer, ByVal int_CodigoSubBloque As Integer)
        Dim obj_BL_Usuario As New bl_Logueo
        Dim str_Acceso As String = ""
        Dim obj_encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim int_CodigoSession As Integer = 0
        Dim str_Info As String = ""
        Dim astr_ArrayDatos() As String
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim ticket As FormsAuthenticationTicket = identity.Ticket

            str_Info = obj_encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            astr_ArrayDatos = str_Info.Split(";")
            int_CodigoSession = astr_ArrayDatos(5)
            int_CodigoUsuario = astr_ArrayDatos(0)
            int_CodigoTipoUsuario = astr_ArrayDatos(1)

            obj_BL_Usuario.FUN_INS_AccesoUsuarioDetalle(int_CodigoSession, int_CodigoModulo, int_CodigoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario)
        Catch ex As Exception

        End Try
    End Sub

#End Region

    
End Class
