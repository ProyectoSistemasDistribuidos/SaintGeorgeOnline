Imports SaintGeorgeOnline_DataAccess.ModuloLogueo
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo

Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Web.Services
Imports System.Configuration.ConfigurationManager

''' <summary>
''' Master Page de Interfaz de Trabajadores
''' </summary>
''' <remarks>
''' Eventos:
''' -------
''' 1. Page_Load
''' 
''' Metodos:
''' -------
'''    Privados:
'''    --------
'''    1. EnvioEmailError
'''    2. CargarMenuPrincipal
'''    3. DevNombreDia
'''    4. DevNombreMes
'''    5. MostrarUsuariosOnline
'''    6. MostrarDatosUsuario
'''    7. MostrarSexyAlertBox
''' 
'''    Publicos:
'''    --------
'''    1. MostrarMensaje
'''    2. MostrarMensajeAlert
'''    3. MostrarTitulo
'''    4. OcultarMenu
'''    5. AgregarPostBackControles
'''    6. SeteoPermisosAcciones
'''    7. SeteoBloquesInformacion
'''    8. BloqueoControles
'''    9. Obtener_CodigoUsuarioLogueado
'''    10. Obtener_CodigoTipoUsuarioLogueado
'''    11. Obtener_NombreUsuarioLogueado
'''    12. Obtener_PeriodoEscolar
'''    13. Obtener_CodigoPeriodoEscolar
''' 
''' </remarks>
Partial Class PaginaPrincipal
    Inherits System.Web.UI.MasterPage

    Private int_CodigoModulo As Integer = 0
    Private int_CodigoOpcion As Integer = 5

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CargarMenuPrincipal()
        MostrarDatosUsuario()
        MostrarUsuariosOnline()

    End Sub

#End Region

#Region "Metodos"

#Region "Privados"

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
        Dim str_MensajeUsuario As String = ""

        int_TipoUsuario = Obtener_CodigoTipoUsuarioLogueado()
        str_NombreUsuario = Obtener_NombreUsuarioLogueado()

        str_MensajeUsuario = Alertas.EnviarMensajeErrorEmail(int_CodigoModulo, int_CodigoOpcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)

    End Sub

    ''' <summary>
    ''' Construye el Menú de opciones del Usuario de tipo Trabajadores.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    ''' 
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
            tbSubMenu_Home.NavigateUrl = "/SaintGeorgeOnline/Principal.aspx"
            tbSubMenu_Home.SeparatorImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_separador.jpg"
            mn_General.Items.Add(tbSubMenu_Home)


            Dim tbSubMenu_Salir As New WebControls.MenuItem
            tbSubMenu_Salir.ImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_salir.jpg"
            tbSubMenu_Salir.NavigateUrl = "/SaintGeorgeOnline/LogOut.aspx"
            tbSubMenu_Salir.SeparatorImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_separador.jpg"
            mn_General.Items.Add(tbSubMenu_Salir)

            If Session("Ac_Pe_Us") Is Nothing Then
                Dim identity As FormsIdentity = HttpContext.Current.User.Identity
                Dim ticket As FormsAuthenticationTicket = identity.Ticket
                Dim encript As New SaintGeorgeOnline_Utilities.Cripto
                Dim str_Info As String = ""
                Dim str_ArrayDatos() As String
                Dim str_Usuario As String = ""
                Dim int_TipoUsuario As Integer = 0

                str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
                str_ArrayDatos = str_Info.Split(";")
                int_TipoUsuario = str_ArrayDatos(1)
                str_Usuario = str_ArrayDatos(0)

                ds_PermisosAccesos = obj_BL_Logueo.FUN_GET_PermisosUsuario(str_Usuario, int_TipoUsuario, 0, 5)
                Session("Ac_Pe_Us") = ds_PermisosAccesos
            End If

            ds_PermisosAccesos = Session("Ac_Pe_Us")

            dt_Grupos = ds_PermisosAccesos.Tables(0)

            dt_Copia1 = ds_PermisosAccesos.Tables(1)
            dt_Copia = dt_Copia1.Copy

            dv_Modulos = dt_Copia1.DefaultView
            dv_SubModulos = dt_Copia.DefaultView

            Dim int_contMenus As Integer = 0

            While int_contMenus <= dt_Grupos.Rows.Count - 1

                Dim tbSubMenu As New WebControls.MenuItem
                tbSubMenu.ImageUrl = Ruta_Madre_Imagenes_Menu & dt_Grupos.Rows(int_contMenus).Item("BM_RutaIcono")
                tbSubMenu.SeparatorImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_separador.jpg"

                If dt_Grupos.Rows(int_contMenus).Item("BM_TipoLink") = True Then
                    tbSubMenu.Target = "blank_"
                End If

                If dt_Grupos.Rows(int_contMenus).Item("BM_TipoBloque") = True Then
                    dv_Modulos.RowFilter = "1=1 AND BM_CodigoBloque =" & dt_Grupos.Rows(int_contMenus).Item("BM_CodigoBloque") & " AND SBM_TipoSubBloque IN(1,2)"
                    Dim int_ContHijos As Integer = 0

                    While int_ContHijos <= dv_Modulos.Count - 1
                        Dim tbMenu_Hijo As New WebControls.MenuItem
                        tbMenu_Hijo.Text = dv_Modulos.Item(int_ContHijos).Item("SBM_Descripcion").ToString
                        tbMenu_Hijo.NavigateUrl = dv_Modulos.Item(int_ContHijos).Item("SBM_Link").ToString

                        dv_SubModulos.RowFilter = "1=1 AND SBM_TipoSubBloque = 3 AND SBM_CodigoSubBloquePadre=" & dv_Modulos.Item(int_ContHijos).Item("SBM_CodigoSubBloque")
                        Dim int_ContHijosDeHijos As Integer = 0

                        While int_ContHijosDeHijos <= dv_SubModulos.Count - 1
                            Dim tbMenu_HijoDeHijo As New WebControls.MenuItem
                            tbMenu_HijoDeHijo.Text = dv_SubModulos.Item(int_ContHijosDeHijos).Item("SBM_Descripcion").ToString
                            tbMenu_HijoDeHijo.NavigateUrl = dv_SubModulos.Item(int_ContHijosDeHijos).Item("SBM_Link").ToString

                            tbMenu_Hijo.ChildItems.Add(tbMenu_HijoDeHijo)
                            int_ContHijosDeHijos = int_ContHijosDeHijos + 1
                        End While

                        tbSubMenu.ChildItems.Add(tbMenu_Hijo)
                        int_ContHijos = int_ContHijos + 1
                    End While

                Else
                    tbSubMenu.NavigateUrl = dt_Grupos.Rows(int_contMenus).Item("BM_Link")
                End If

                mn_General.Items.Add(tbSubMenu)
                int_contMenus = int_contMenus + 1
            End While

        Catch ex As Exception
            EnvioEmailError(0, "<b>Cargar Menú Principal:</b>  <BR>" & ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Devuelve la descripción del día solicitado.
    ''' </summary>
    ''' <param name="int_dia">Numero de día</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function DevNombreDia(ByVal int_dia As Integer) As String
        Dim str_nomdia As String = ""

        If int_dia = 1 Then
            str_nomdia = "Lunes"
        ElseIf int_dia = 2 Then
            str_nomdia = "Martes"
        ElseIf int_dia = 3 Then
            str_nomdia = "Miércoles"
        ElseIf int_dia = 4 Then
            str_nomdia = "Jueves"
        ElseIf int_dia = 5 Then
            str_nomdia = "Viernes"
        ElseIf int_dia = 6 Then
            str_nomdia = "Sábado"
        ElseIf int_dia = 7 Then
            str_nomdia = "Domingo"
        Else
            str_nomdia = ""
        End If

        Return str_nomdia
    End Function

    ''' <summary>
    ''' Devuelve la descripción del mes solicitado.
    ''' </summary>
    ''' <param name="int_mes">Numero de mes</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function DevNombreMes(ByVal int_mes As Integer) As String
        Dim str_nommes As String = ""

        If int_mes = 1 Then
            str_nommes = "Enero"
        ElseIf int_mes = 2 Then
            str_nommes = "Febrero"
        ElseIf int_mes = 3 Then
            str_nommes = "Marzo"
        ElseIf int_mes = 4 Then
            str_nommes = "Abril"
        ElseIf int_mes = 5 Then
            str_nommes = "Mayo"
        ElseIf int_mes = 6 Then
            str_nommes = "Junio"
        ElseIf int_mes = 7 Then
            str_nommes = "Julio"
        ElseIf int_mes = 8 Then
            str_nommes = "Agosto"
        ElseIf int_mes = 9 Then
            str_nommes = "Septiembre"
        ElseIf int_mes = 10 Then
            str_nommes = "Octubre"
        ElseIf int_mes = 11 Then
            str_nommes = "Noviembre"
        ElseIf int_mes = 12 Then
            str_nommes = "Diciembre"
        Else
            str_nommes = ""
        End If

        Return str_nommes
    End Function

    ''' <summary>
    ''' Devuelve la cantidad de usuarios en linea en el sitio Web
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarUsuariosOnline()
        Try
            lbl_Cant_OnlineUsers.Text = Application("ActiveUsers") & " usuario(s) en linea."
        Catch ex As Exception
            lbl_Cant_OnlineUsers.Text = "obteniendo usuarios en linea ....."
        End Try
    End Sub

    ''' <summary>
    ''' Muestra los datos personales del Usuario Logueado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarDatosUsuario()

        Try
            Dim obj_identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim obj_ticket As FormsAuthenticationTicket = obj_identity.Ticket
            Dim str_Info As String = ""
            Dim obj_encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim astr_ArrayDatos() As String

            str_Info = obj_encript.Desencriptar(New RC2CryptoServiceProvider, obj_ticket.UserData)
            astr_ArrayDatos = str_Info.Split(";")

            lbl_NombreUsuario.Text = astr_ArrayDatos(2)
            hd_Usuario_General.Value = obj_encript.Encriptar(New RC2CryptoServiceProvider, (astr_ArrayDatos(0) & "," & astr_ArrayDatos(1) & "," & astr_ArrayDatos(5)))
            Img_FotoUsuario.ImageUrl = IIf(astr_ArrayDatos(3).ToString = "", "/SaintGeorgeOnline/Fotos/noPhoto.gif", System.Configuration.ConfigurationManager.AppSettings.Item("RutaFotosUsuarios_Web_Admin").ToString() & astr_ArrayDatos(3))
            lbl_AnioGeneral.Text = astr_ArrayDatos(10)

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

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

#Region "Publicos"

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

    ''' <summary>
    ''' Habilita los botones de las acciones que el usuario puede realizar durante toda la sesión.
    ''' </summary>
    ''' <param name="obj_BotonAcceso">Botón de Acción</param>
    ''' <param name="int_CodigoOpcion">Código de SubBloque de Menú</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub SeteoPermisosAcciones(ByVal obj_BotonAcceso As Object, ByVal int_CodigoOpcion As Integer)

        Dim int_cont As Integer = 0
        Dim ds_PermisosAccesos As New DataSet
        Dim dv_PermisosAccesos As New DataView

        Try
            '    ds_PermisosAccesos = Session("Ac_Pe_Us")
            '    dt_PermisosAccesos = ds_PermisosAccesos.Tables(3).DefaultView
            '    dt_PermisosAccesos.RowFilter = "1=1 AND SBM_CodigoSubBloque = " & codigoOpcion

            '    While int_cont <= dt_PermisosAccesos.Count - 1

            '        If dt_PermisosAccesos.Item(int_cont).Item("AA_CodigoProgramacion") = BotonAcceso.ID Then
            '            BotonAcceso.visible = True
            '        End If

            '        int_cont = int_cont + 1
            '    End While
        Catch ex As Exception

        End Try


    End Sub

    ''' <summary>
    ''' Habilita los bloques de Información que el usuario puede ver.
    ''' </summary>
    ''' <param name="obj_BloqueInformacion">Codigo de Bloque de Información</param>
    ''' <param name="int_codigoOpcion">Código de SubBloque de Menú</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub SeteoBloquesInformacion(ByVal obj_BloqueInformacion As Object, ByVal int_codigoOpcion As Integer)
        Dim int_cont As Integer = 0
        Dim ds_PermisosAccesos As New DataSet
        Dim dv_PermisosAccesos As New DataView

        Try
            '    ds_PermisosAccesos = Session("Ac_Pe_Us")
            '    dt_PermisosAccesos = ds_PermisosAccesos.Tables(2).DefaultView
            '    dt_PermisosAccesos.RowFilter = "1=1 AND SBM_CodigoSubBloque = " & codigoOpcion

            '    While int_cont <= dt_PermisosAccesos.Count - 1

            '        If dt_PermisosAccesos.Item(int_cont).Item("BI_CodigoGrupoProgramacion") = BloqueInformacion.ID Then
            '            BloqueInformacion.visible = True
            '        End If

            '        int_cont = int_cont + 1
            '    End While

        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Deshabilita los controles enviados del formulario. (botones, tabs, etc)
    ''' </summary>
    ''' <param name="obj_Objeto">Control de tipo boton, tab, etc</param>
    ''' <param name="int_tipo">Tipo de control</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Sub BloqueoControles(ByVal obj_Objeto As Object, ByVal int_tipo As Integer)

        'If int_tipo = 1 Then
        '    'PARA BOTONES
        '    obj_Objeto.visible = False
        'End If

        'If int_tipo = 2 Then
        '    'PARA GRUPOS DE INFORMACION
        '    obj_Objeto.visible = False
        'End If
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
    ''' Obtiene la descripcion del año academico actual del usuario logueado al sistema.
    ''' </summary>
    ''' <returns>Descripcion del Periodo Academico Actual</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_PeriodoEscolar() As Integer

        Dim int_PeriodoEscolar As Integer

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_PeriodoEscolar = str_ArrayDatos(10)

        Catch ex As Exception
            int_PeriodoEscolar = 2011
        End Try

        Return int_PeriodoEscolar

    End Function

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
    Public Function Obtener_CodigoPeriodoEscolar() As Integer

        Dim int_CodigoPeriodoEscolar As Integer

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoPeriodoEscolar = str_ArrayDatos(11)

        Catch ex As Exception
            int_CodigoPeriodoEscolar = 1
        End Try

        Return int_CodigoPeriodoEscolar

    End Function

    ''' <summary>
    ''' Obtiene el codigo del año academico actual del usuario logueado al sistema.
    ''' </summary>
    ''' <returns>Codigo de Perfill</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoPerfil() As Integer

        Dim int_CodigoPerfil As Integer

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoPerfil = str_ArrayDatos(12)

        Catch ex As Exception
            int_CodigoPerfil = 0
        End Try

        Return int_CodigoPerfil

    End Function

#End Region

#End Region

End Class

