Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

Imports System.Data

Partial Class Interfaz_Familia_Modulo_DatosFamilia_InformacionFamiliares
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Master.MostrarTitulo("Información de Familiar")

        Try
            If Not Page.IsPostBack Then

                'SetearAccionesAcceso()
                ObtenerListaFamiliares()
                ddl_Familiar_SelectedIndexChanged()

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btn_SolicitarActualizarDatos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_SolicitarActualizarDatos.Click
        Enviar_ActualizarDatos()
    End Sub

    Protected Sub ddl_Familiar_SelectedIndexChanged()
        Try
            Obtener_FichaFamiliar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene la relación de Familiares según el usuario en el sistema.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerListaFamiliares()

        Dim obj_BL_Familia As New bl_Familiares
        Dim ds_DatosFamilia As New DataSet
        Dim dt_Familia As New DataTable
        Dim dt_Familiares As New DataTable
        Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim ds_Familiares As New DataSet
        Dim dt_copia As New DataTable

        ds_DatosFamilia = obj_BL_Familia.FUN_LIS_FamiliaresPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 64)

        dt_Familia = ds_DatosFamilia.Tables(0)
        dt_Familiares = ds_DatosFamilia.Tables(1)
        dt_copia = dt_Familiares.Copy
        ds_Familiares.Tables.Add(dt_copia)

        lbl_NombreFamilia.Text = "Familia: " & dt_Familia.Rows(0).Item("Descripcion")

        Controles.llenarCombo(ddl_Familiar, ds_Familiares, "CodigoFamiliar", "NombreCompleto", False, False)

    End Sub

    Public Sub Obtener_FichaFamiliar()

        Try

            Dim int_CodigoFamiliar As Integer = ddl_Familiar.SelectedValue
            Dim obj_BL_Familiares As New bl_Familiares

            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

            Dim ds_Lista As DataSet = obj_BL_Familiares.FUN_GET_Familiar(int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 64)

            lbl_NombreCompleto.Text = ddl_Familiar.SelectedItem.Text

            '1...DATOS PERSONALES
            lbl_ApePaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString
            lbl_ApeMaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString
            lbl_Nombres.Text = ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString

            lbl_NombreCompleto.Text = lbl_ApePaterno.Text & " " & lbl_ApeMaterno.Text & ", " & lbl_Nombres.Text

            lbl_Sexo.Text = ds_Lista.Tables(0).Rows(0).Item("DescSexo").ToString
            lbl_TipoDocIdentidad.Text = ds_Lista.Tables(0).Rows(0).Item("DescTipoDocIdentidad").ToString
            lbl_NumeroDocIdentidad.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString
            lbl_EstadoCivil.Text = ds_Lista.Tables(0).Rows(0).Item("DescEstadoCivil").ToString
            lbl_Vive.Text = ds_Lista.Tables(0).Rows(0).Item("DescVive").ToString
            lbl_FechaDefuncion.Text = ds_Lista.Tables(0).Rows(0).Item("FechaDefuncionStr").ToString

            '2...DATOS DE NACIMIENTO
            lbl_FechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
            Dim str_Nacionalidaddes As String = ""
            Dim int_Nacionalidades_cont As Integer = 0

            If ds_Lista.Tables(1).Rows.Count > 0 And ds_Lista.Tables(1).Rows(0).Item(0).ToString = -1 Then
                lbl_Nacionalidad.Text = "--"
            Else
                While int_Nacionalidades_cont <= ds_Lista.Tables(1).Rows.Count - 1
                    str_Nacionalidaddes = ds_Lista.Tables(1).Rows(int_Nacionalidades_cont).Item("Descripcion").ToString & "<BR>" & str_Nacionalidaddes
                    int_Nacionalidades_cont = int_Nacionalidades_cont + 1
                End While

                lbl_Nacionalidad.Text = str_Nacionalidaddes
            End If
           

            '3...DATOS DE DOMICILIO
            lbl_PaisDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DescPaisDomicilio").ToString
            lbl_DepartamentoDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioDepartamento").ToString
            lbl_ProvinciaDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioProvincia").ToString
            lbl_DistritoDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoDomicilioDistrito").ToString
            lbl_UrbanizacionDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("UrbanizacionDomicilio").ToString
            lbl_DireccionDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DireccionDomicilio").ToString
            lbl_ReferenciaDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString
            lbl_TelefonoDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoDomicilio").ToString
            lbl_AccesoInternetDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DescAccesoInternetDomicilio").ToString

            '4...DATOS LABORALES
            lbl_SituacionLaboral.Text = ds_Lista.Tables(0).Rows(0).Item("DescSituacionLaboral").ToString
            lbl_OpcupacionCargo.Text = ds_Lista.Tables(0).Rows(0).Item("Ocupacion").ToString
            lbl_CentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("CentroTrabajo").ToString
            lbl_DireccionTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("DireccionTrabajo").ToString
            lbl_PaisCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("DescPaisTrabajo").ToString
            lbl_DepartamentoCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoDepartamento").ToString
            lbl_ProvinciaCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoProvincia").ToString
            lbl_DistritoCentroTrabajos.Text = ds_Lista.Tables(0).Rows(0).Item("DescUbigeoTrabajoDistrito").ToString
            lbl_TelefonoCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoTrabajo").ToString
            lbl_CelularCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("CelularTrabajo").ToString
            lbl_ServicioRadioCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("DescServicioRadioTrabajo").ToString
            lbl_NumeroRadioCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroRadioTrabajo").ToString
            lbl_EmailCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("EmailTrabajo").ToString
            lbl_TieneInternetCentroTrabajo.Text = ds_Lista.Tables(0).Rows(0).Item("DescAccesoInternetTrabajo").ToString

            '5...DATOS DE ESTUDIO
            lbl_EsExalumno.Text = ds_Lista.Tables(0).Rows(0).Item("DescExAlumno").ToString
            lbl_ColegioEgreso.Text = ds_Lista.Tables(0).Rows(0).Item("ColegioEgreso").ToString
            lbl_AnioEgreso.Text = ds_Lista.Tables(0).Rows(0).Item("AnioEgreso").ToString
            lbl_ContinuoEstudios.Text = ds_Lista.Tables(0).Rows(0).Item("ContinuoEstudios").ToString
            lbl_NivelInstruccion.Text = ds_Lista.Tables(0).Rows(0).Item("DescNivelInstruccion").ToString
            lbl_EscolaridadMinisterio.Text = ds_Lista.Tables(0).Rows(0).Item("DescEscolaridadMinisterio").ToString

            Dim str_Profesiones As String = ""
            Dim int_Profesiones_cont As Integer = 0

            If ds_Lista.Tables(3).Rows.Count > 0 And ds_Lista.Tables(3).Rows(0).Item(0).ToString = -1 Then
                lbl_Profesiones.Text = "--"
            Else
                While int_Profesiones_cont <= ds_Lista.Tables(3).Rows.Count - 1
                    str_Profesiones = ds_Lista.Tables(3).Rows(int_Profesiones_cont).Item("Descripcion").ToString & "<BR>" & str_Profesiones
                    int_Profesiones_cont = int_Profesiones_cont + 1
                End While

                lbl_Profesiones.Text = str_Profesiones
            End If

            '6...DATOS ADICIONALES
            lbl_ProfesaReligion.Text = ds_Lista.Tables(0).Rows(0).Item("DescProfesaReligion").ToString
            lbl_Religion.Text = ds_Lista.Tables(0).Rows(0).Item("DescReligion").ToString
            lbl_NombreIglesia.Text = ds_Lista.Tables(0).Rows(0).Item("NombreIglesia").ToString
            lbl_Celular.Text = ds_Lista.Tables(0).Rows(0).Item("CelularPersonal").ToString
            lbl_ServicioRadioPersonal.Text = ds_Lista.Tables(0).Rows(0).Item("DescServicioRadioDomicilio").ToString
            lbl_NumeroRadioPersonal.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroRadioPersonal").ToString
            lbl_EmailPersonal.Text = ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString

            Dim str_Idiomas As String = ""
            Dim int_Idiomas_cont As Integer = 0

            If ds_Lista.Tables(2).Rows.Count > 0 And ds_Lista.Tables(2).Rows(0).Item(0).ToString = -1 Then
                lbl_IdiomasPersonal.Text = "--"
            Else
                While int_Idiomas_cont <= ds_Lista.Tables(2).Rows.Count - 1
                    str_Idiomas = ds_Lista.Tables(2).Rows(int_Idiomas_cont).Item("Descripcion").ToString & "<BR>" & str_Idiomas
                    int_Idiomas_cont = int_Idiomas_cont + 1
                End While

                lbl_IdiomasPersonal.Text = str_Idiomas
            End If



            Dim dtTienePendientes As New DataTable

            dtTienePendientes = ds_Lista.Tables(6)
            Dim tienePend As Boolean = False
            tienePend = Convert.ToBoolean(dtTienePendientes.Rows(0)("tienePendiente"))
            If tienePend = True Then
                btn_SolicitarActualizarDatos.Enabled = False
                btn_SolicitarActualizarDatos.ToolTip = "Tiene una solicitud pendiente de aprobacion"
            ElseIf tienePend = False Then
                btn_SolicitarActualizarDatos.Enabled = True
                btn_SolicitarActualizarDatos.ToolTip = ""
            End If
            'NombreCompleto_FamiliarSolicitante	CodigoSolicitud	CodigoFamiliar_FamiliarActualizar	CodigoPersona_FamiliarActualizar	NombreCompleto_FamiliarActualizar	FechaRegistroSolicitud
            'URTEAGA DONGO, Alfredo	1754	715	1978	URTEAGA DONGO, Alfredo	01/03/2012


            Dim DtinformacionUltimaActualizacion As New DataTable
            DtinformacionUltimaActualizacion = ds_Lista.Tables(7)
            If DtinformacionUltimaActualizacion.Rows.Count > 0 Then

                lblFechaModificacion.Text = DtinformacionUltimaActualizacion.Rows(0)("FechaRegistroSolicitud").ToString()
                lblNombreFamiliarModificacion.Text = DtinformacionUltimaActualizacion.Rows(0)("NombreCompleto_FamiliarSolicitante").ToString()

            Else
                lblFechaModificacion.Text = ""
                lblNombreFamiliarModificacion.Text = ""
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub


    Private Sub Enviar_ActualizarDatos()
        'Dim Context As HttpContext
        'Context = HttpContext.Current
        'Context.Items.Add("CodigoFamiliar", "2")
        'Server.Transfer("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaFamiliar.aspx")
        'Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaFamiliar.aspx")
        Session("ActualizacionDatosInformacionFamiliares") = ddl_Familiar.SelectedValue & "," & _
                                                            ddl_Familiar.SelectedItem.ToString & "," & _
                                                           Me.Master.Obtener_CodigoPeriodoEscolar
        Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionInformacionFamiliares.aspx?codigoFamiliar=" & ddl_Familiar.SelectedValue)
    End Sub

#End Region


#Region "Manejo de Alertas - Emails"

    ''' <summary>
    ''' Recibe mensajes y los deriva a otro metodo que los visualizara cno animación de JQuery
    ''' </summary>
    ''' <param name="str_alertas">Mensaje que se quiere visualizar</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     25/01/2011
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
    ''' Fecha de Creación:     25/01/2011
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
    ''' Fecha de Creación:     18/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_CodigoUsuario As String = Obtener_CodigoFamiliarLogueado()
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
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Function Obtener_CodigoFamiliarLogueado() As Integer

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
    ''' Registra el acceso al formulario. (Log de Accesos)
    ''' </summary>
    ''' <param name="int_CodigoSubBloque">Codigo del SubBloque de Menú.</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
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
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
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

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     03/01/2011
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
