Imports System.Security.Cryptography
Imports System.Data
Imports System.Data.SqlClient

Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo


Partial Class Interfaz_Familia_Plantilla_Principal
    Inherits System.Web.UI.MasterPage

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CargarMenuPrincipal()
        MostrarDatosUsuario()
        MostrarUsuariosOnline()

    End Sub

#End Region

#Region "Metodos Privados"

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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(0, 6, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)

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
    ''' Devuelve la cantidad de usuarios en linea en el sitio Web
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarDatosUsuario()

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim str_ArrayDatos() As String
            Dim dt_Familias As New DataTable
            Dim int_cont As Integer = 0

            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            lbl_NombreUsuario.Text = str_ArrayDatos(2)
            lbl_Familia_general.Text = str_ArrayDatos(9)
            fh_principal_CodigoFamilia.Value = str_ArrayDatos(8)
            lbl_AnioGeneral.Text = str_ArrayDatos(10)

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Construye el Menú de opciones del Usuario de tipo Familiares.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Private Sub CargarMenuPrincipal()
    '    Dim obj_BL_Logueo As New bl_Logueo
    '    Dim ds_PermisosAccesos As New DataSet
    '    Dim dt_Modulos As New DataView
    '    Dim dt_Grupos As New DataTable

    '    Try

    '        mn_General.Width = 200
    '        mn_General.CellPadding = 0
    '        mn_General.CellSpacing = 0
    '        tbMenuDinamico.BorderWidth = 0
    '        tbMenuDinamico.BorderStyle = BorderStyle.None
    '        tbMenuDinamico.GridLines = GridLines.Both
    '        tbMenuDinamico.HorizontalAlign = HorizontalAlign.Left
    '        tbMenuDinamico.ID = "MenuPrincipal_"

    '        Dim cont_BloqueMenu As Integer = 0
    '        Dim cont_SubBloque As Integer = 0
    '        Dim TipoSubBloque As Integer = 0
    '        Dim Ruta_Madre_Imagenes_Menu As String = ""

    '        Ruta_Madre_Imagenes_Menu = System.Configuration.ConfigurationManager.AppSettings.Item("RutaImagenMenu_Web_Externa")

    '        'Agregar cabecera por defecto - Home
    '        Dim newRow_C = New TableRow
    '        Dim newCell_C = New TableCell
    '        Dim imgMenu_C As New System.Web.UI.WebControls.Image

    '        newCell_C.BorderStyle = BorderStyle.Solid
    '        newCell_C.BorderWidth = 1
    '        newCell_C.BorderColor = Drawing.Color.White
    '        newCell_C.Height = 20

    '        imgMenu_C.ImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_home_f.jpg"
    '        imgMenu_C.Style.Value = "cursor:pointer"
    '        newCell_C.Controls.Add(imgMenu_C)
    '        newCell_C.Attributes.Add("onclick", "document.location.href = '" & "/SaintGeorgeOnline/Interfaz_Familia/Principal.aspx/" & "'")
    '        newRow_C.Cells.Add(newCell_C)
    '        mn_General.Rows.Add(newRow_C)

    '        'Agregar cabecera por defecto - Salir
    '        Dim newRow_S = New TableRow
    '        Dim newCell_S = New TableCell
    '        Dim imgMenu_S As New System.Web.UI.WebControls.Image
    '        Dim cont_opciones As Integer = 0

    '        newCell_S.BorderStyle = BorderStyle.Solid
    '        newCell_S.BorderWidth = 1
    '        newCell_S.BorderColor = Drawing.Color.White
    '        newCell_S.Height = 20

    '        imgMenu_S.ImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_salir_f.jpg"
    '        imgMenu_S.Style.Value = "cursor:pointer"
    '        newCell_S.Controls.Add(imgMenu_S)
    '        newCell_S.Attributes.Add("onclick", "document.location.href = '" & "/SaintGeorgeOnline/LogOut.aspx/" & "'")
    '        newRow_S.Cells.Add(newCell_S)
    '        tbMenuDinamico.Rows.Add(newRow_S)

    '        If Session("Ac_Pe_Us") Is Nothing Then
    '            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
    '            Dim ticket As FormsAuthenticationTicket = identity.Ticket
    '            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
    '            Dim str_Info As String = ""
    '            Dim str_ArrayDatos() As String
    '            Dim str_Usuario As String = ""
    '            Dim int_TipoUsuario As Integer = 0

    '            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
    '            str_ArrayDatos = str_Info.Split(";")
    '            int_TipoUsuario = str_ArrayDatos(1)
    '            str_Usuario = str_ArrayDatos(0)

    '            ds_PermisosAccesos = obj_BL_Logueo.FUN_GET_PermisosUsuario(str_Usuario, int_TipoUsuario, 0, 6)
    '            Session("Ac_Pe_Us") = ds_PermisosAccesos
    '        End If

    '        ds_PermisosAccesos = Session("Ac_Pe_Us")

    '        dt_Grupos = ds_PermisosAccesos.Tables(0)
    '        dt_Modulos = ds_PermisosAccesos.Tables(1).DefaultView

    '        While cont_BloqueMenu <= dt_Grupos.Rows.Count - 1
    '            Dim newRow = New TableRow
    '            Dim newCell = New TableCell
    '            Dim imgMenu As New System.Web.UI.WebControls.Image
    '            Dim hyperLink As New System.Web.UI.WebControls.HyperLink

    '            newCell.BorderStyle = BorderStyle.Solid
    '            newCell.BorderWidth = 1
    '            newCell.BorderColor = Drawing.Color.White
    '            newCell.Height = 20

    '            imgMenu.ImageUrl = Ruta_Madre_Imagenes_Menu & dt_Grupos.Rows(cont_BloqueMenu).Item("BM_RutaIcono").ToString
    '            imgMenu.Style.Value = "cursor:pointer"

    '            hyperLink.CssClass = "desplegable"
    '            hyperLink.Controls.Add(imgMenu)

    '            newCell.Controls.Add(hyperLink)

    '            cont_opciones = 0
    '            If dt_Grupos.Rows(cont_BloqueMenu).Item("BM_TipoBloque") = True Then
    '                dt_Modulos.RowFilter = " 1=1 and BM_CodigoBloque = " & dt_Grupos.Rows(cont_BloqueMenu).Item("BM_CodigoBloque").ToString


    '                Dim itemslista As New System.Web.UI.WebControls.BulletedList
    '                itemslista.Attributes.Add("Class", "ListaMenu")

    '                While cont_SubBloque <= dt_Modulos.Count - 1

    '                    TipoSubBloque = dt_Modulos.Item(cont_SubBloque).Item("SBM_TipoSubBloque").ToString

    '                    If TipoSubBloque = 1 Or TipoSubBloque = 2 Then

    '                        itemslista.Items.Add(dt_Modulos.Item(cont_SubBloque).Item("SBM_Descripcion").ToString)
    '                        itemslista.Items(cont_opciones).Attributes.Add("Class", "ItemMenuFondo")
    '                        itemslista.Items(cont_opciones).Attributes.Add("onclick", "document.location.href = '" & dt_Modulos.Item(cont_SubBloque).Item("SBM_Link").ToString & "'")
    '                        newCell.Controls.Add(itemslista)
    '                        cont_opciones = cont_opciones + 1
    '                    End If

    '                    cont_SubBloque = cont_SubBloque + 1
    '                End While
    '            Else
    '                newCell.Attributes.Add("onclick", "document.location.href = '" & dt_Grupos.Rows(cont_BloqueMenu).Item("BM_Link") & "'")
    '            End If

    '            newCell.HorizontalAlign = HorizontalAlign.Left
    '            newCell.Font.Name = "Arial"
    '            newCell.ForeColor = Drawing.Color.White
    '            newCell.Font.Size = 8
    '            newCell.Font.Bold = True
    '            newRow.Cells.Add(newCell)

    '            tbMenuDinamico.Rows.Add(newRow)
    '            cont_SubBloque = 0
    '            cont_BloqueMenu = cont_BloqueMenu + 1
    '        End While
    '    Catch ex As Exception
    '        EnvioEmailError(0, ex.ToString)
    '    End Try

    'End Sub
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

            If Obtener_CodigoTipoUsuarioLogueado() = 1 Then 'Alumnos
                Dim tbSubMenu_Home As New WebControls.MenuItem
                tbSubMenu_Home.ImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_home.jpg"
                tbSubMenu_Home.NavigateUrl = "/SaintGeorgeOnline/Interfaz_Familia/HomeAlumnos.aspx/"
                tbSubMenu_Home.SeparatorImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_separador.jpg"
                mn_General.Items.Add(tbSubMenu_Home)
            Else
                Dim tbSubMenu_Home As New WebControls.MenuItem
                tbSubMenu_Home.ImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_home.jpg"
                tbSubMenu_Home.NavigateUrl = "/SaintGeorgeOnline/Interfaz_Familia/Principal.aspx/"
                tbSubMenu_Home.SeparatorImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_separador.jpg"
                mn_General.Items.Add(tbSubMenu_Home)
            End If

            


            Dim tbSubMenu_Salir As New WebControls.MenuItem
            tbSubMenu_Salir.ImageUrl = Ruta_Madre_Imagenes_Menu & "menu_barra_salir.jpg"
            tbSubMenu_Salir.NavigateUrl = "/SaintGeorgeOnline/LogOut.aspx/"
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

#Region "Metodos Publicos del Master Page"

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
    ''' Obtener el código de la familia activa o seleccionada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_CodigoFamiliaActiva() As Integer

        Dim int_codigoFamiliaActiva As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_codigoFamiliaActiva = str_ArrayDatos(8)

        Catch ex As Exception
            int_codigoFamiliaActiva = -1
        End Try

        Return int_codigoFamiliaActiva

    End Function

    ''' <summary>
    ''' Obtener la descripción de la familia activa o seleccionada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Obtener_NombreFamiliaActiva() As String

        Dim str_NombreFamiliaActiva As String = ""

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            str_NombreFamiliaActiva = str_ArrayDatos(9)

        Catch ex As Exception
            str_NombreFamiliaActiva = ""
        End Try

        Return str_NombreFamiliaActiva

    End Function

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
    Public Function Obtener_CodigoFamiliarLogueado() As Integer

        Dim int_CodigoFamiliarLogueado As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim str_Info As String = ""
            Dim encript As New SaintGeorgeOnline_Utilities.Cripto
            Dim ticket As FormsAuthenticationTicket = identity.Ticket
            Dim str_ArrayDatos() As String


            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")

            int_CodigoFamiliarLogueado = str_ArrayDatos(0)

        Catch ex As Exception
            int_CodigoFamiliarLogueado = -1
        End Try

        Return int_CodigoFamiliarLogueado

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
        Dim Info_Acceso As String = ""
        Dim encript As New SaintGeorgeOnline_Utilities.Cripto
        Dim int_CodigoSession As Integer = 0
        Dim str_Info As String = ""
        Dim str_ArrayDatos() As String
        Dim int_CodigoUsuario As Integer = 0
        Dim int_CodigoTipoUsuario As Integer = 0

        Try
            Dim identity As FormsIdentity = HttpContext.Current.User.Identity
            Dim ticket As FormsAuthenticationTicket = identity.Ticket

            str_Info = encript.Desencriptar(New RC2CryptoServiceProvider, ticket.UserData)
            str_ArrayDatos = str_Info.Split(";")
            int_CodigoSession = str_ArrayDatos(5)
            int_CodigoUsuario = str_ArrayDatos(0)
            int_CodigoTipoUsuario = str_ArrayDatos(1)

            obj_BL_Usuario.FUN_INS_AccesoUsuarioDetalle(int_CodigoSession, int_CodigoModulo, int_CodigoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario)
        Catch ex As Exception

        End Try
    End Sub

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
    Public Function Obtener_DescripcionPeriodoEscolar() As Integer

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

#End Region

End Class

