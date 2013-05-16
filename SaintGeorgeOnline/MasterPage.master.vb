Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo
Imports SaintGeorgeOnline_DataAccess.ModuloLogueo
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_Utilities
Imports System.Security.Cryptography
Imports System.Web.Services
Imports System.Configuration.ConfigurationManager
Imports SaintGeorgeOnline_BusinessLogic

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage



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
    Public Sub MostrarMensaje(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Dim str_Script As String = ""
        str_Script = SaintGeorgeOnline_Utilities.Alertas.ObtenerMensaje(str_Mensaje, str_TipoMensaje)

        'str_Script = " $(document).ready(function() { " & str_Script & " });"

        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", str_Script, True)

    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub MostrarMensajeAlert(ByVal str_Mensaje As String)

        Dim str_AlertMsn As String = str_Mensaje

        str_AlertMsn = str_AlertMsn.Replace("<ul>", "\n")
        str_AlertMsn = str_AlertMsn.Replace("<li>", "\n")
        str_AlertMsn = str_AlertMsn.Replace("</li>", "")
        str_AlertMsn = str_AlertMsn.Replace("<em>", "")
        str_AlertMsn = str_AlertMsn.Replace("</em>", "")
        str_AlertMsn = str_AlertMsn.Replace("</ul>", "\n")

        Dim str_Script As String = ""
        str_Script = "alert(' " & str_AlertMsn & " ');"
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", str_Script, True)

    End Sub

    ''' <summary>
    ''' Muestra el Título del formularío invocador en la cabecera de la Master Page.
    ''' </summary>
    ''' <param name="str_Titulo">Título del formulario</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub MostrarTitulo(ByVal str_Titulo As String)

        lblModulo.Text = str_Titulo
        Me.Page.Title = str_Titulo

    End Sub

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
        Dim obj_encript As New Cripto
        Dim int_CodigoSession As Integer = 0
        Dim str_Info As String = ""
        Dim astr_ArrayDatos() As String
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0

        Try

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim ticket As FormsAuthenticationTicket = identity.Ticket
                str_Info = obj_encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            astr_ArrayDatos = str_Info.Split(";")
            int_CodigoSession = astr_ArrayDatos(5)
            int_CodigoUsuario = astr_ArrayDatos(0)
            int_CodigoTipoUsuario = astr_ArrayDatos(1)

            'obj_BL_Usuario.FUN_INS_AccesoUsuarioDetalle(int_CodigoSession, int_CodigoModulo, int_CodigoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario)
        Catch ex As Exception
            EnvioEmailError(1, "<B>Registro de Acceso a Página</B> <BR>" & ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene el codigo del año academico actual del usuario logueado al sistema.
    ''' </summary>
    ''' <returns>Codigo de Periodo Academico Actual</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoPeriodoPresupuesto() As Integer

        Dim int_CodigoPeriodoEscolar As Integer

        Try

            Dim str_Info As String = ""
            Dim str_ArrayDatos() As String

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim encript As New Cripto
                Dim ticket As FormsAuthenticationTicket = identity.Ticket

                str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            str_ArrayDatos = str_Info.Split(";")
            int_CodigoPeriodoEscolar = str_ArrayDatos(11)

            'Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            'Dim str_Info As String = ""
            'Dim encript As New Cripto
            'Dim ticket As FormsAuthenticationTicket = identity.Ticket
            'Dim str_ArrayDatos() As String

            'str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            'str_ArrayDatos = str_Info.Split(";")

            'int_CodigoPeriodoEscolar = str_ArrayDatos(11)

        Catch ex As Exception
            int_CodigoPeriodoEscolar = 1
        End Try

        Return int_CodigoPeriodoEscolar

    End Function

    ''' <summary>
    ''' Obtiene la descripcion del año academico actual del usuario logueado al sistema.
    ''' </summary>
    ''' <returns>Descripcion del Periodo Academico Actual</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_DescripcionPeriodoPresupuesto() As Integer

        Dim int_PeriodoEscolar As Integer

        Try

            Dim str_Info As String = ""
            Dim str_ArrayDatos() As String

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim encript As New Cripto
                Dim ticket As FormsAuthenticationTicket = identity.Ticket

                str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            str_ArrayDatos = str_Info.Split(";")
            int_PeriodoEscolar = str_ArrayDatos(10)

            'Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            'Dim str_Info As String = ""
            'Dim encript As New Cripto
            'Dim ticket As FormsAuthenticationTicket = identity.Ticket
            'Dim str_ArrayDatos() As String

            'str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            'str_ArrayDatos = str_Info.Split(";")

            'int_PeriodoEscolar = str_ArrayDatos(10)

        Catch ex As Exception
            int_PeriodoEscolar = -1
        End Try

        Return int_PeriodoEscolar

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

            Dim str_Info As String = ""
            Dim str_ArrayDatos() As String

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim encript As New Cripto
                Dim ticket As FormsAuthenticationTicket = identity.Ticket

                str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            str_ArrayDatos = str_Info.Split(";")
            str_NombreUsuarioLogueado = str_ArrayDatos(2)

            'Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            'Dim str_Info As String = ""
            'Dim encript As New Cripto
            'Dim ticket As FormsAuthenticationTicket = identity.Ticket
            'Dim str_ArrayDatos() As String

            'str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            'str_ArrayDatos = str_Info.Split(";")

            'str_NombreUsuarioLogueado = str_ArrayDatos(2)

        Catch ex As Exception
            str_NombreUsuarioLogueado = ""
        End Try

        Return str_NombreUsuarioLogueado

    End Function





    ''
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

            Dim str_Info As String = ""
            Dim str_ArrayDatos() As String

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim encript As New Cripto
                Dim ticket As FormsAuthenticationTicket = identity.Ticket

                str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            str_ArrayDatos = str_Info.Split(";")
            int_CodigoUsuarioLogueado = str_ArrayDatos(0)

            'Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            'Dim str_Info As String = ""
            'Dim encript As New Cripto
            'Dim ticket As FormsAuthenticationTicket = identity.Ticket
            'Dim str_ArrayDatos() As String

            'str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            'str_ArrayDatos = str_Info.Split(";")

            'int_CodigoUsuarioLogueado = str_ArrayDatos(0)

        Catch ex As Exception
            int_CodigoUsuarioLogueado = -1
        End Try

        Return int_CodigoUsuarioLogueado

    End Function

    ''' <summary>
    ''' Obtiene el código del presupuesto actual 
    ''' </summary>
    ''' <returns>código de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoPresupuesto() As Integer

        Dim int_CodigoPresupuesto As Integer = 0

        Try

            Dim str_Info As String = ""
            Dim str_ArrayDatos() As String

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim encript As New Cripto
                Dim ticket As FormsAuthenticationTicket = identity.Ticket

                str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            str_ArrayDatos = str_Info.Split(";")
            int_CodigoPresupuesto = str_ArrayDatos(7)

            'Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            'Dim str_Info As String = ""
            'Dim encript As New Cripto
            'Dim ticket As FormsAuthenticationTicket = identity.Ticket
            'Dim str_ArrayDatos() As String


            'str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            'str_ArrayDatos = str_Info.Split(";")

            'int_CodigoPresupuesto = str_ArrayDatos(7)

        Catch ex As Exception
            int_CodigoPresupuesto = -1
        End Try

        Return int_CodigoPresupuesto

    End Function

    Public Function Obtener_CodigoSolicitudPresupuesto() As Integer

        Dim int_CodigoSolicitudPresupuesto As Integer = 0

        Try

            Dim str_Info As String = ""
            Dim str_ArrayDatos() As String

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim encript As New Cripto
                Dim ticket As FormsAuthenticationTicket = identity.Ticket

                str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            str_ArrayDatos = str_Info.Split(";")
            int_CodigoSolicitudPresupuesto = str_ArrayDatos(8)

            'Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            'Dim str_Info As String = ""
            'Dim encript As New Cripto
            'Dim ticket As FormsAuthenticationTicket = identity.Ticket
            'Dim str_ArrayDatos() As String


            'str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            'str_ArrayDatos = str_Info.Split(";")

            'int_CodigoSolicitudPresupuesto = str_ArrayDatos(8)

        Catch ex As Exception
            int_CodigoSolicitudPresupuesto = -1
        End Try

        Return int_CodigoSolicitudPresupuesto

    End Function

    Public Function Obtener_NombrePresupuesto() As String

        Dim str_NombrePresupuesto As String = ""

        Try

            Dim str_Info As String = ""
            Dim str_ArrayDatos() As String

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim encript As New Cripto
                Dim ticket As FormsAuthenticationTicket = identity.Ticket

                str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            str_ArrayDatos = str_Info.Split(";")
            str_NombrePresupuesto = str_ArrayDatos(9)

            'Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            'Dim str_Info As String = ""
            'Dim encript As New Cripto
            'Dim ticket As FormsAuthenticationTicket = identity.Ticket
            'Dim str_ArrayDatos() As String


            'str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            'str_ArrayDatos = str_Info.Split(";")

            'str_NombrePresupuesto = str_ArrayDatos(9)

        Catch ex As Exception
            str_NombrePresupuesto = -1
        End Try

        Return str_NombrePresupuesto

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Not Page.IsPostBack Then
        CargarMenuPrincipal()
        MostrarDatosUsuario()


        'End If

    End Sub

    Private Sub CargarMenuPrincipal()
        Dim obj_BL_Logueo As New bl_Logueo
        Dim ds_PermisosAccesos As New DataSet
        Dim dt_Copia As New DataTable
        Dim dt_Copia1 As New DataTable
        Dim dv_Modulos As New DataView
        Dim dt_Grupos As New DataTable
        Dim dv_SubModulos As New DataView
        Dim Ruta_Madre_Imagenes_Menu As String = ""

        Ruta_Madre_Imagenes_Menu = System.Configuration.ConfigurationManager.AppSettings.Item("RutaImagenMenu_Web_Externa")

        Try

            mn_General.Items.Clear()
            mn_General.Width = 185
            mn_General.Height = 20
            mn_General.Style.Value = "text-align:left;"
            Dim tbSubMenu_Home As New WebControls.MenuItem
            tbSubMenu_Home.ImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_home.jpg"
            tbSubMenu_Home.NavigateUrl = "/Presupuestos/Principal.aspx"
            tbSubMenu_Home.SeparatorImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_separador.jpg"
            mn_General.Items.Add(tbSubMenu_Home)


            Dim tbSubMenu_Salir As New WebControls.MenuItem
            tbSubMenu_Salir.ImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_salir.jpg"
            tbSubMenu_Salir.NavigateUrl = "/Presupuestos/LogOut.aspx"
            tbSubMenu_Salir.SeparatorImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_separador.jpg"
            mn_General.Items.Add(tbSubMenu_Salir)

            'If Session("Ac_Pe_Us") Is Nothing Then
            '    Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            '    Dim ticket As FormsAuthenticationTicket = identity.Ticket
            '    Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            '    Dim str_Info As String = ""
            '    Dim str_ArrayDatos() As String
            '    Dim str_Usuario As String = ""
            '    Dim int_TipoUsuario As Integer = 0

            '    str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            '    str_ArrayDatos = str_Info.Split(";")
            '    int_TipoUsuario = str_ArrayDatos(1)
            '    str_Usuario = str_ArrayDatos(0)

            '    ds_PermisosAccesos = obj_BL_Logueo.FUN_GET_PermisosUsuario(str_Usuario, int_TipoUsuario, 0, 5)
            '    Session("Ac_Pe_Us") = ds_PermisosAccesos
            'End If

            'ds_PermisosAccesos = Session("Ac_Pe_Us")

            'dt_Grupos = ds_PermisosAccesos.Tables(0)

            'dt_Copia1 = ds_PermisosAccesos.Tables(1)
            'dt_Copia = dt_Copia1.Copy

            'dv_Modulos = dt_Copia1.DefaultView
            'dv_SubModulos = dt_Copia.DefaultView

            'Dim int_contMenus As Integer = 0

            'While int_contMenus <= dt_Grupos.Rows.Count - 1

            '    Dim tbSubMenu As New WebControls.MenuItem
            '    tbSubMenu.ImageUrl = Ruta_Madre_Imagenes_Menu & dt_Grupos.Rows(int_contMenus).Item("BM_RutaIcono")
            '    tbSubMenu.SeparatorImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_separador.jpg"

            '    dv_Modulos.RowFilter = "1=1 AND BM_CodigoBloque =" & dt_Grupos.Rows(int_contMenus).Item("BM_CodigoBloque") & " AND SBM_TipoSubBloque IN(1,2)"
            '    Dim int_ContHijos As Integer = 0

            '    While int_ContHijos <= dv_Modulos.Count - 1
            '        Dim tbMenu_Hijo As New WebControls.MenuItem
            '        tbMenu_Hijo.Text = dv_Modulos.Item(int_ContHijos).Item("SBM_Descripcion").ToString
            '        tbMenu_Hijo.NavigateUrl = dv_Modulos.Item(int_ContHijos).Item("SBM_Link").ToString

            '        dv_SubModulos.RowFilter = "1=1 AND SBM_TipoSubBloque = 3 AND SBM_CodigoSubBloquePadre=" & dv_Modulos.Item(int_ContHijos).Item("SBM_CodigoSubBloque")
            '        Dim int_ContHijosDeHijos As Integer = 0

            '        While int_ContHijosDeHijos <= dv_SubModulos.Count - 1
            '            Dim tbMenu_HijoDeHijo As New WebControls.MenuItem
            '            tbMenu_HijoDeHijo.Text = dv_SubModulos.Item(int_ContHijosDeHijos).Item("SBM_Descripcion").ToString
            '            tbMenu_HijoDeHijo.NavigateUrl = dv_SubModulos.Item(int_ContHijosDeHijos).Item("SBM_Link").ToString

            '            tbMenu_Hijo.ChildItems.Add(tbMenu_HijoDeHijo)
            '            int_ContHijosDeHijos = int_ContHijosDeHijos + 1
            '        End While

            '        tbSubMenu.ChildItems.Add(tbMenu_Hijo)
            '        int_ContHijos = int_ContHijos + 1
            '    End While

            'mn_General.Items.Add(tbSubMenu)
            'int_contMenus = int_contMenus + 1
            'End While

        Catch ex As Exception
            EnvioEmailError(0, "<b>Cargar Menú Principal:</b>  <BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub MostrarDatosUsuario()

        Try

            Dim str_Info As String = ""
            Dim astr_ArrayDatos() As String
            Dim obj_encript As New Cripto

            ' Si mi sesión caduco, obtengo los datos de la cookie y los cargo en la sesión
            If Session("MiUsuario") Is Nothing Then

                Dim obj_identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim obj_ticket As FormsAuthenticationTicket = obj_identity.Ticket

                str_Info = obj_encript.Desencriptar(New RC2CryptoServiceProvider, obj_ticket.UserData)
                Session("MiUsuario") = str_Info

            Else ' Obtengo los datos de la sesión

                str_Info = Session("MiUsuario")

            End If

            astr_ArrayDatos = str_Info.Split(";")
            lbl_NombreUsuario.Text = astr_ArrayDatos(2)
            hd_Usuario_General.Value = obj_encript.Encriptar(New RC2CryptoServiceProvider, (astr_ArrayDatos(0) & "," & astr_ArrayDatos(1) & "," & astr_ArrayDatos(5)))
            Img_FotoUsuario.ImageUrl = IIf(astr_ArrayDatos(3).ToString = "", "/Presupuestos/Fotos/noPhoto.gif", System.Configuration.ConfigurationManager.AppSettings.Item("RutaFotosUsuarios_Web_Admin").ToString() & astr_ArrayDatos(3))
            lbl_AnioGeneral.Text = astr_ArrayDatos(6)



            'Dim obj_identity As FormsIdentity = HttpContext.Current.User.Identity
            'Dim obj_ticket As FormsAuthenticationTicket = obj_identity.Ticket
            'Dim str_Info As String = ""
            'Dim obj_encript As New Cripto
            'Dim astr_ArrayDatos() As String

            'str_Info = obj_encript.Desencriptar(New RC2CryptoServiceProvider, obj_ticket.UserData)
            'astr_ArrayDatos = str_Info.Split(";")

            'lbl_NombreUsuario.Text = astr_ArrayDatos(2)
            'hd_Usuario_General.Value = obj_encript.Encriptar(New RC2CryptoServiceProvider, (astr_ArrayDatos(0) & "," & astr_ArrayDatos(1) & "," & astr_ArrayDatos(5)))
            'Img_FotoUsuario.ImageUrl = IIf(astr_ArrayDatos(3).ToString = "", "/Presupuestos/Fotos/noPhoto.gif", System.Configuration.ConfigurationManager.AppSettings.Item("RutaFotosUsuarios_Web_Admin").ToString() & astr_ArrayDatos(3))
            'lbl_AnioGeneral.Text = astr_ArrayDatos(6)

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As Integer, ByVal str_DetalleError As String)

        Dim int_TipoUsuario As Integer = 2
        Dim str_NombreUsuario As String = Obtener_NombreUsuarioLogueado()

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(0, 0, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)

    End Sub

End Class

