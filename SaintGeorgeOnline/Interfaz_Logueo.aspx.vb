Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

Imports System.Data
Imports System.Security.Cryptography

''' <summary>
''' Modulo de Interfaz de Logueo
''' </summary>
''' <remarks>
''' Eventos: 
''' -------
''' 1. Page_Load
''' 2. chk_UsuarioReferencia_CheckedChanged
''' 3. btnContinuar_Click
''' 4. ddl_TipoPersona_SelectedIndexChanged
''' 5. btn_GenerarContraseña_Click
''' 
''' Metodos:
''' -------
''' 1. EnvioEmailError
''' 2. VerificacionLogueo
''' 3. cargarComboTipoPersona
''' 4. CargarComboPersonas
''' 5. ContinuarProceso
''' 6. MostrarSexyAlertBox
''' 7. GenerarClaveSuperUsuario
''' 8. Obtener_CodigoUsuarioLogueado
''' 9. Obtener_CodigoTipoUsuarioLogueado
''' 10. Obtener_NombreUsuarioLogueado
''' 11. RegistrarAccesoPagina
''' </remarks>
Partial Class Interfaz_Logueo
    Inherits System.Web.UI.Page

    Private int_CodigoModulo As Integer = 0
    Private int_CodigoOpcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                RegistrarAccesoPagina(int_CodigoModulo, int_CodigoOpcion)
                cargarComboTipoPersona()
                CargarComboPersonas()
                VerificacionLogueo()
            End If
            
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub chk_UsuarioReferencia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_UsuarioReferencia.CheckedChanged
        If chk_UsuarioReferencia.Checked = True Then
            pnl_SuplantarUsuario.Visible = True
        Else
            pnl_SuplantarUsuario.Visible = False
        End If
    End Sub

    Protected Sub btnContinuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinuar.Click
        Try
            If chk_UsuarioReferencia.Checked = True Then
                If ddl_TipoPersona.SelectedValue = 0 Or ddl_Persona.SelectedValue = 0 Then
                    MostrarSexyAlertBox("Debe seleccionar una persona de referencia de la lista", "Alert")
                Else
                    ContinuarProceso()
                End If
            Else
                ContinuarProceso()
            End If
        Catch ex As Exception
            EnvioEmailError(10, ex.ToString)
        End Try
    End Sub

    Protected Sub ddl_TipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            CargarComboPersonas()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btn_GenerarContraseña_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            GenerarClaveSuperUsuario()
        Catch ex As Exception
            EnvioEmailError(114, ex.ToString)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Response.Redirect("/SaintGeorgeOnline/LogOut.aspx/")
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
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim str_NombreUsuario As String = Obtener_NombreUsuarioLogueado()

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(int_CodigoModulo, int_CodigoOpcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
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

        Dim obj_identity As FormsIdentity = HttpContext.Current.User.Identity
        Dim obj_ticket As FormsAuthenticationTicket = obj_identity.Ticket
        Dim str_Info As String = ""
        Dim obj_encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim astr_ArrayDatos() As String
        Dim boo_SuperUsuario As Boolean = False
        Dim int_TipoUsuario As Integer = 0
        Dim str_NombreUsuario As String = ""

        str_Info = obj_encript.Desencriptar(New RC2CryptoServiceProvider, obj_ticket.UserData)
        astr_ArrayDatos = str_Info.Split(";")

        boo_SuperUsuario = astr_ArrayDatos(4)
        int_TipoUsuario = astr_ArrayDatos(1)
        str_NombreUsuario = astr_ArrayDatos(2)
        Img_FotoUsuario.ImageUrl = IIf(astr_ArrayDatos(3).ToString = "", "/SaintGeorgeOnline/Fotos/noPhoto.gif", System.Configuration.ConfigurationManager.AppSettings.Item("RutaFotosUsuarios_Web_Admin").ToString() & astr_ArrayDatos(3))

        lbl_UsuarioLogueado.Text = str_NombreUsuario

    End Sub

    ''' <summary>
    ''' Carga el Combo de Tipo de Personas.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboTipoPersona()

        Dim obj_BL_TiposPersonas As New bl_TiposPersonas
        Dim ds_Lista As DataSet = obj_BL_TiposPersonas.FUN_LIS_TiposPersonas("", -1, 0, 1, 0, 0)
        Controles.llenarCombo(ddl_TipoPersona, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el Combo de Personas según el tipo de persona seleccionada.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CargarComboPersonas()
        Dim obj_BL_Personas As New bl_MaestroPersonas
        Dim ds_Lista As DataSet = obj_BL_Personas.FUN_LIS_PersonasPorTipo(ddl_TipoPersona.SelectedValue, 0, 1, 0, 0)
        Controles.llenarCombo(ddl_Persona, ds_Lista, "Codigo", "Nombres", False, True)
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
    Private Sub ContinuarProceso()

        If chk_UsuarioReferencia.Checked = True And txt_Contrasenia.Text.Trim = "" Then
            MostrarSexyAlertBox("Debe ingresar una contraseña para seguir con el proceso.", "Alert")
            Exit Sub
        End If

        Dim obj_identity As FormsIdentity = HttpContext.Current.User.Identity
        Dim obj_ticket As FormsAuthenticationTicket = obj_identity.Ticket
        Dim obj_encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim obj_BL_Logueo As New SaintGeorgeOnline_BusinessLogic.ModuloLogueo.bl_Logueo
        Dim obj_cookie As HttpCookie

        Dim str_Info As String = ""
        Dim astr_ArrayDatos() As String
        Dim str_Usuario As String = ""
        Dim str_ContraseniaSuperUsuario As String = ""
        Dim str_hash As String
        Dim str_NombreUsuario As String = ""

        Dim ds_PermisosAccesos As New DataSet
        Dim dt_DatosUsuarioReemplazado As New DataTable

        Dim int_TipoUsuario As Integer = 0
        Dim int_CodigoFamiliaActiva As Integer = 0
        Dim str_NombreFamiliaActiva As String = ""
        Dim sb_datausuario As New System.Text.StringBuilder

        str_Info = obj_encript.Desencriptar(New RC2CryptoServiceProvider, obj_ticket.UserData)
        astr_ArrayDatos = str_Info.Split(";")
        int_TipoUsuario = astr_ArrayDatos(1)
        str_Usuario = astr_ArrayDatos(0)

        If chk_UsuarioReferencia.Checked = True Then
            str_Usuario = ddl_Persona.SelectedValue
            int_TipoUsuario = ddl_TipoPersona.SelectedValue
            str_ContraseniaSuperUsuario = txt_Contrasenia.Text.Trim
            str_ContraseniaSuperUsuario = obj_encript.Encriptar(New RC2CryptoServiceProvider, str_ContraseniaSuperUsuario)

            dt_DatosUsuarioReemplazado = obj_BL_Logueo.FUN_VAL_PermisosSuperUsuario(astr_ArrayDatos(0), str_ContraseniaSuperUsuario, 0, 1).Tables(0)

            If dt_DatosUsuarioReemplazado.Rows.Count > 0 Then

                'OBTENER MENU DE OPCIONES DE USUARIO DE REFERENCIA
                ds_PermisosAccesos = obj_BL_Logueo.FUN_GET_PermisosUsuario(str_Usuario, int_TipoUsuario, 0, 1)

                'OBTENER DATOS DE USUARIO DE REFERENCIA (SOLO EN CASO DE FAMILIARES)
                If int_TipoUsuario = 3 Then
                    Dim ds_DatosUsuario As New DataSet
                    Dim dt_Familias As New DataTable
                    Dim dt_DatoUsuarioReemplazo As New DataTable

                    ds_DatosUsuario = obj_BL_Logueo.FUN_GET_DatosUsuarioReferencia(str_Usuario, int_TipoUsuario, 0, 1)
                    dt_DatoUsuarioReemplazo = ds_DatosUsuario.Tables(0)
                    dt_Familias = ds_DatosUsuario.Tables(1)
                    Session("Ac_Pe_Us_Familias") = dt_Familias

                    int_CodigoFamiliaActiva = dt_DatoUsuarioReemplazo.Rows(0).Item("CodigoFamiliaActiva")
                    str_NombreFamiliaActiva = dt_DatoUsuarioReemplazo.Rows(0).Item("DescripcionFamilia")
                End If
            Else
                MostrarSexyAlertBox("Usuario y/o contraseña incorrectos", "Alert")
                Exit Sub
            End If

        Else
            ds_PermisosAccesos = obj_BL_Logueo.FUN_GET_PermisosUsuario(str_Usuario, int_TipoUsuario, 0, 1)
        End If

        Session("Ac_Pe_Us") = ds_PermisosAccesos

        str_NombreUsuario = obj_ticket.Name

        With sb_datausuario
            .Append(astr_ArrayDatos(0)) 'Codigo Usuario - indice 0
            .Append(";")
            .Append(astr_ArrayDatos(1)) 'Tipo Usuario - indice 1
            .Append(";")
            .Append(astr_ArrayDatos(2)) 'Nombre Usuario - indice 2   
            .Append(";")
            .Append(astr_ArrayDatos(3)) 'Ruta de Foto - indice 3  
            .Append(";")
            .Append(astr_ArrayDatos(4)) 'Super Usuario - indice 4
            .Append(";")
            .Append(astr_ArrayDatos(5)) 'Codigo de Session del Usuario - indice 5
            .Append(";")
            .Append(str_Usuario) 'Usuario de Referencia - indice 6 
            .Append(";")
            .Append(astr_ArrayDatos(7)) 'Email de Usuario del tipo Trabajador - indide 7 
            .Append(";")
            .Append(int_CodigoFamiliaActiva) 'Codigo de Familia Activa del familiar - indice 8
            .Append(";")
            .Append(str_NombreFamiliaActiva) 'Nombre de Familia Activa del familiar - indice 9
            .Append(";")
            .Append(astr_ArrayDatos(10)) 'Periodo Activo - indice 10
            .Append(";")
            .Append(astr_ArrayDatos(11)) 'Codigo de Periodo Activo - indice 11
            .Append(";")
            .Append(astr_ArrayDatos(12)) 'Codigo de Perfil  - indice 12
        End With

        'Actualizar Ticket de Sesion
        obj_ticket = New FormsAuthenticationTicket _
                    (0, str_NombreUsuario, _
                    Date.Now, _
                    Date.Now.AddMinutes(60), False, _
                    obj_encript.Encriptar(New RC2CryptoServiceProvider, sb_datausuario.ToString), _
                    FormsAuthentication.FormsCookiePath)

        str_hash = FormsAuthentication.Encrypt(obj_ticket)

        obj_cookie = New HttpCookie(FormsAuthentication.FormsCookieName, str_hash)

        HttpContext.Current.Response.Cookies.Add(obj_cookie)

        'Direccionar a Interfaz correspondiente
        If int_TipoUsuario = 1 Then 'Alumnos
            Response.Redirect("", False)
        ElseIf int_TipoUsuario = 2 Then 'Trabajadores
            Response.Redirect("/SaintGeorgeOnline/Principal.aspx", False)
        ElseIf int_TipoUsuario = 3 Then 'Familiar
            Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Principal.aspx", False)
        Else
            Response.Redirect("/SaintGeorgeOnline/Acceso_Error.aspx", False)
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
    ''' Genera clave de super usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub GenerarClaveSuperUsuario()

        Dim obj_identity As FormsIdentity = HttpContext.Current.User.Identity
        Dim obj_ticket As FormsAuthenticationTicket = obj_identity.Ticket

        Dim str_Info As String = ""
        Dim str_Contrasenia As String = ""
        Dim str_ContraseniaEncriptada As String = ""
        Dim astr_ArrayDatos As String()
        Dim str_EmailUsuario As String = ""

        Dim obj_EnvioEmail As New EnvioEmail
        Dim encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim obj_BL_Logueo As New bl_Logueo

        Dim int_Result As Integer = -1

        str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, obj_ticket.UserData)
        astr_ArrayDatos = str_Info.Split(";")
        str_EmailUsuario = astr_ArrayDatos(7)

        If str_EmailUsuario.Length > 4 Then
            '5 números y 4 letras en posiciones aleatorias
            str_Contrasenia = GeneracionContrasenias.ContraseniaSuperUsuario()
            str_ContraseniaEncriptada = encript.Encriptar(New RC2CryptoServiceProvider, str_Contrasenia)
            int_Result = obj_BL_Logueo.FUN_INS_ClaveSuperUsuario(astr_ArrayDatos(0), str_ContraseniaEncriptada, 0, 1)
            obj_EnvioEmail.SendEmail(str_EmailUsuario, "Se ha generado la contraseña: <b>" & str_Contrasenia & "</b>, esta estará vigente sólo para la sesión que acaba de iniciar en el sistema, en el siguiente ingreso deberá de generar de nuevo la contraseña.", "Contraseña Autogenerada de Super Usuario")

            MostrarSexyAlertBox("Se ha generado y enviado a su email la contraseña satisfactoriamente.", "Info")
        Else
            MostrarSexyAlertBox("El sistema no tiene registrado un email para el usuario logueado. Por favor comunicarse con el área de sistemas.", "Info")
            Exit Sub
        End If

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
            EnvioEmailError(1, "<B>Registro de Acceso a Página</B> <BR>" & ex.ToString)
        End Try
    End Sub

#End Region

   
End Class
