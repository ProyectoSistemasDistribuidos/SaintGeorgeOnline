Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo

Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

Partial Class Popups_agregarEmpresa
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 2
    Private cod_Opcion As Integer = 42

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then

                SetearAccionesAcceso()
                CargarCombos()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim usp_mensaje As String = ""
            If validar(usp_mensaje) Then
                Grabar()
            Else
                MostrarSexyAlertBox(usp_mensaje, "Alert")
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub SetearAccionesAcceso()
        RegistrarAccesoPagina(2, 42)

        'CONTROLES DEL FORMULARIO
        'Master.BloqueoControles(btnBuscar, 1)
        'Master.BloqueoControles(btnGrabar, 1)

        'Master.SeteoPermisosAcciones(btnBuscar, 47)
        'Master.SeteoPermisosAcciones(btnGrabar, 47)

    End Sub

    Private Sub CargarCombos()

        cargarComboDepartamentos()
        cargarComboProvincia()
        cargarComboDistrito()

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Departamento()
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDepartamentos()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Departamentos(int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Controles.llenarCombo(ddlDepartamento, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Provincia
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboProvincia()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Provincias(ddlDepartamento.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlProvincia, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Carga el combo Ubigeo Distrito
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboDistrito()

        Dim obj_BL_Ubigeo As New bl_Ubigeo
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()

        Dim ds_Lista As DataSet = obj_BL_Ubigeo.FUN_LIS_Distritos(ddlDepartamento.SelectedValue, ddlProvincia.SelectedValue, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlDistrito, ds_Lista, "Codigo", "Descripcion", False, True)

    End Sub

    ''' <summary>
    ''' Valida el campo de ingreso
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento 
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function validar(ByRef str_Mensaje As String) As Boolean

        Dim result As Boolean = True
        Dim str_alertas As String = ""

        If tbRazonSocial.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Razón Social")
            result = False
        End If
        If Validacion.ValidarCamposIngreso(tbRazonSocial) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Razón Social")
            result = False
        End If

        If tbNombreComercial.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Nombre Comercial")
            result = False
        End If
        If Validacion.ValidarCamposIngreso(tbNombreComercial) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "Nombre Comercial")
            result = False
        End If

        If tbRUC.Text.Trim.Length = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "RUC")
            result = False
        End If
        If Validacion.ValidarCamposIngreso(tbRUC) = False Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 2, "RUC")
            result = False
        End If

        If ddlDepartamento.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Departamento ")
            result = False
        End If
        If ddlProvincia.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Provincia ")
            result = False
        End If
        If ddlDistrito.SelectedValue = 0 Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Distrito ")
            result = False
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Limpia los campos del formulario de registro.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCampos()

        tbRazonSocial.Text = ""
        tbNombreComercial.Text = ""
        tbRUC.Text = ""
        tbDireccion.Text = ""
        tbTelefono.Text = ""
        tbCelular.Text = ""
        tbFax.Text = ""
        tbEmail.Text = ""
        ddlDepartamento.SelectedValue = 0
        ddlProvincia.SelectedValue = 0
        ddlDistrito.SelectedValue = 0

    End Sub

    ''' <summary>
    ''' Graba o Actualiza el registro indicado.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub Grabar()

        Dim obj_BE_Empresas As New be_Empresas
        Dim obj_BL_Empresas As New bl_Empresas

        Dim usp_mensaje As String = ""
        Dim usp_valor As Integer

        Dim int_CodigoUsuario As Integer = Obtener_CodigoUsuarioLogueado()
        Dim int_CodigoTipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()

        obj_BE_Empresas.CodigoUbigeo = ddlDepartamento.SelectedValue.ToString & _
                                        ddlProvincia.SelectedValue.ToString & _
                                        ddlDistrito.SelectedValue.ToString
        obj_BE_Empresas.RazonSocial = tbRazonSocial.Text.Trim
        obj_BE_Empresas.NombreComercial = tbNombreComercial.Text.Trim
        obj_BE_Empresas.Direccion = tbDireccion.Text.Trim
        obj_BE_Empresas.Ruc = tbRUC.Text.Trim
        obj_BE_Empresas.Telefono = tbTelefono.Text.Trim
        obj_BE_Empresas.Celular = tbCelular.Text.Trim
        obj_BE_Empresas.Fax = tbFax.Text.Trim
        obj_BE_Empresas.Email = tbRazonSocial.Text.Trim

        usp_valor = obj_BL_Empresas.FUN_INS_Empresas(obj_BE_Empresas, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            'MostrarSexyAlertBox(usp_mensaje, "Info")

            Session("CodigoEmpresaRegistrado") = usp_valor
            'limpiarCampos()
            Cerrar()

        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    Private Sub Cerrar()

        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "alert_cerrar", "window.close();", True)

    End Sub

    Protected Sub ddlDepartamento_SelectedIndexChanged()
        Try
            limpiarCombos(ddlDistrito)
            cargarComboProvincia()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub ddlProvincia_SelectedIndexChanged()
        Try
            cargarComboDistrito()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina los elementos de la lista
    ''' </summary>
    ''' <param name="combo">Nombre del combobox</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub limpiarCombos(ByVal combo As DropDownList)
        Controles.limpiarCombo(combo, False, True)
    End Sub

#End Region

#Region "Manejo de Alertas - Emails"

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarAlertas(ByVal str_alertas As String)

        MostrarSexyAlertBox(str_alertas, "Alert")

    End Sub

    ''' <summary>
    ''' Muestra un mensaje usando la animación de JQuery
    ''' </summary>
    ''' <param name="str_Mensaje">Mensaje que se quiere visualizar</param>
    ''' <param name="str_TipoMensaje">Tipo de Mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        MostrarMensaje(str_Mensaje, str_TipoMensaje)

    End Sub

    ''' <summary>
    ''' Envía Email de Error de cualquier metodo que lo invoque.
    ''' </summary>
    ''' <param name="int_CodigoAccion">Codigo que hace referencia al tipo de Acción</param>
    ''' <param name="str_DetalleError">Descripción del error</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_CodigoUsuario As String = Obtener_CodigoUsuarioLogueado()
        Dim int_TipoUsuario As Integer = Obtener_CodigoTipoUsuarioLogueado()
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(5, 79, int_CodigoAccion, str_DetalleError, int_CodigoUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

#End Region


#Region "Métodos Master Page"

    ''' <summary>
    ''' Obtiene el código del usuario logueado al sistema
    ''' </summary>
    ''' <returns>código de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
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
    ''' Registra el acceso al formulario. (Log de Accesos)
    ''' </summary>
    ''' <param name="int_CodigoSubBloque">Codigo del SubBloque de Menú.</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub RegistrarAccesoPagina(ByVal int_CodigoModulo As Integer, ByVal int_CodigoSubBloque As Integer)
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
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub MostrarMensaje(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)

        Dim str_Script As String = ""
        str_Script = SaintGeorgeOnline_Utilities.Alertas.ObtenerMensaje(str_Mensaje, str_TipoMensaje)
        ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "", str_Script, True)

    End Sub

    ''' <summary>
    ''' Obtiene el código del tipo de usuario logueado al sistema
    ''' </summary>
    ''' <returns>código de tipo de usuario logueado</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
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
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     15/02/2012
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