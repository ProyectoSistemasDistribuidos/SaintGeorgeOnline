Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

Imports System.Data

Partial Class Interfaz_Familia_Modulo_DatosAlumnos_InformacionAlumno
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Master.MostrarTitulo("Información de Hijo")

            If Not Page.IsPostBack Then

                ObtenerListaAlumnos()
                ddl_Alumno_SelectedIndexChanged()

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try    
    End Sub

    Protected Sub btn_SolicitarActualizarDatos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_SolicitarActualizarDatos.Click
        Enviar_ActualizarDatos()
    End Sub

    Protected Sub ddl_Alumno_SelectedIndexChanged()

        Try
            Obtener_FichaAlumno(ddl_Alumno.SelectedValue)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

#End Region

#Region "Metodos"

    Private Sub ObtenerListaAlumnos()

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim ds_DatosFamilia As New DataSet
        Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva

        If int_CodigoTipoUsuario = 1 Then ' Alumnos

            ds_DatosFamilia = obj_BL_Alumnos.FUN_GET_AlumnosPorCodigoAlumno(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 65)
            ddl_Alumno.Enabled = False

        ElseIf int_CodigoTipoUsuario = 3 Then ' Familiares

            ds_DatosFamilia = obj_BL_Alumnos.FUN_LIS_AlumnosPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 65)
            ddl_Alumno.Enabled = True

        End If

        Dim ds_DatosAlumnos As New DataSet
        Dim dt_Familia As New DataTable
        Dim dt_Alumnos As New DataTable
        Dim dt_copia As New DataTable

        dt_Familia = ds_DatosFamilia.Tables(0)
        dt_Alumnos = ds_DatosFamilia.Tables(1)
        dt_copia = dt_Alumnos.Copy
        ds_DatosAlumnos.Tables.Add(dt_copia)

        'lbl_NombreFamilia.Text = "Familia: " & dt_Familia.Rows(0).Item("Descripcion")
        Controles.llenarCombo(ddl_Alumno, ds_DatosAlumnos, "CodigoAlumno", "NombreCompleto", False, False)

    End Sub

    Private Sub Enviar_ActualizarDatos()
        'Dim Context As HttpContext
        'Context = HttpContext.Current
        'Context.Items.Add("CodigoFamiliar", "2")
        'Server.Transfer("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaFamiliar.aspx")
        'Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaAlumno.aspx")SolicitudActualizacionInformacionHijos.aspx
        Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionInformacionHijos.aspx?codigoAlumno=" & ddl_Alumno.SelectedValue)
    End Sub

    Private Sub Obtener_FichaAlumno(ByVal int_CodigoAlumno As Integer)
        Try

            Dim obj_BL_Alumnos As New bl_Alumnos
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado
            'Verificar Codigo Modulo y Codigo de Opcion 
            Dim ds_Lista As DataSet = obj_BL_Alumnos.FUN_GET_Alumnos(int_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 51)

            lbl_NombreCompleto.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString & " " & _
                                        ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString & _
                                        ", " & ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString
            '1...DATOS PERSONALES
            lbl_CodigoAlumno.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoAlumno").ToString
            lbl_CodigoEducando.Text = ds_Lista.Tables(0).Rows(0).Item("CodigoEducando").ToString
            lbl_ApePaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoPaterno").ToString
            lbl_ApeMaterno.Text = ds_Lista.Tables(0).Rows(0).Item("ApellidoMaterno").ToString
            lbl_Nombres.Text = ds_Lista.Tables(0).Rows(0).Item("Nombre").ToString
            lbl_Sexo.Text = ds_Lista.Tables(0).Rows(0).Item("Sexo").ToString
            lbl_TipoDocIdentidad.Text = ds_Lista.Tables(0).Rows(0).Item("TipoDocIdentidad").ToString
            lbl_NumeroDocIdentidad.Text = ds_Lista.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString

            '2...DATOS NACIMIENTO
            lbl_NacimientoRegistrado.Text = ds_Lista.Tables(0).Rows(0).Item("NacimientoRegistrado").ToString
            lbl_FechaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("FechaNacimiento").ToString
            lbl_PaisNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("Pais").ToString
            lbl_DepartamentoNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("Departamento").ToString
            lbl_ProvinciaNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("Provincia").ToString
            lbl_DistritoNacimiento.Text = ds_Lista.Tables(0).Rows(0).Item("Distrito").ToString

            ' Nacionalidad
            If ds_Lista.Tables(1).Rows.Count > 0 Then

                If ds_Lista.Tables(1).Rows(0).Item("CodigoRelacion") > 0 Then
                    lbl_Nacionalidad_1.Text = ds_Lista.Tables(1).Rows(0).Item("Nacionalidad")
                Else
                    lbl_Nacionalidad_1.Text = "-"
                End If

                If ds_Lista.Tables(1).Rows.Count > 1 Then
                    If ds_Lista.Tables(1).Rows(1).Item("CodigoRelacion") > 0 Then
                        lbl_Nacionalidad_2.Text = ds_Lista.Tables(1).Rows(1).Item("Nacionalidad")
                    Else
                        lbl_Nacionalidad_2.Text = "-"
                    End If
                Else
                    lbl_Nacionalidad_2.Text = "-"
                End If

            Else
                lbl_Nacionalidad_1.Text = "-"
                lbl_Nacionalidad_2.Text = "-"
            End If

            '3...DATOS DOMICILIO
            lbl_DepartamentoDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DomicilioDepartamento").ToString
            lbl_ProvinciaDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DomicilioProvincia").ToString
            lbl_DistritoDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DomicilioDistrito").ToString
            lbl_UrbanizacionDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("Urbanizacion").ToString
            lbl_DireccionDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("Direccion").ToString
            lbl_ReferenciaDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("ReferenciaDomiciliaria").ToString
            lbl_TelefonoDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("TelefonoCasa").ToString
            lbl_AccesoInternetDomicilio.Text = ds_Lista.Tables(0).Rows(0).Item("DescAccesoInternet").ToString

            '4...DATOS DE SEGURO
            If ds_Lista.Tables(5).Rows.Count > 0 Then
                If ds_Lista.Tables(5).Rows(0).Item("CodigoRelacion") > 0 Then
                    lbl_AnioMatriculaSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("AnioMatricula")
                    lbl_TipoSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("Tipo")
                    lbl_CompañiaSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("Compania")
                    lbl_NumeroPolizaSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("NumeroPoliza")
                    lbl_VigenciaSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("VigenciaTime")
                    lbl_FechaVigenciaSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("FechaInicio") & " - " & ds_Lista.Tables(5).Rows(0).Item("FechaFin")
                    lbl_ClinicaSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("Clinica")
                    lbl_AmbulanciaSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("AmbulanciaCompania")
                    lbl_TelefonoAmbulanciaSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("TelefonoAmbulancia")
                    lbl_CopiaCarnetSeguro.Text = ds_Lista.Tables(5).Rows(0).Item("CopiaCarnetSeguro")
                Else
                    lbl_AnioMatriculaSeguro.Text = "-"
                    lbl_TipoSeguro.Text = "-"
                    lbl_CompañiaSeguro.Text = "-"
                    lbl_NumeroPolizaSeguro.Text = "-"
                    lbl_VigenciaSeguro.Text = "-"
                    lbl_FechaVigenciaSeguro.Text = "-"
                    lbl_ClinicaSeguro.Text = "-"
                    lbl_AmbulanciaSeguro.Text = "-"
                    lbl_TelefonoAmbulanciaSeguro.Text = "-"
                    lbl_CopiaCarnetSeguro.Text = "-"
                End If
            End If




            '....DATOS DE EMERGENCIA
            lbl_ContactoEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("NombreContactoAvisoEmergencia").ToString
            lbl_TelefonosEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("TelfCasaContactoAvisoEmergencia").ToString
            lbl_CelularEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("CellContactoAvisoEmergencia").ToString
            lbl_TelfOficinaEmergencia.Text = ds_Lista.Tables(0).Rows(0).Item("TelfOficinaContactoAvisoEmergencia").ToString


            '5...DATOS RELIGIOSOS
            lbl_ProfesaReligion.Text = ds_Lista.Tables(0).Rows(0).Item("DescProfesaReligion").ToString
            lbl_Religion.Text = ds_Lista.Tables(0).Rows(0).Item("Religion").ToString
            lbl_SeBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("DescBautizo").ToString
            lbl_LugarBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoLugar").ToString
            lbl_AnioBautizo.Text = ds_Lista.Tables(0).Rows(0).Item("BautizoAnio").ToString
            lbl_SePrimeraComunion.Text = ds_Lista.Tables(0).Rows(0).Item("DescPrimeraComunion").ToString
            lbl_PrimeraComunionLugar.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionLugar").ToString
            lbl_PrimeraComunionAnio.Text = ds_Lista.Tables(0).Rows(0).Item("PrimeraComunionAnio").ToString
            lbl_SeConfirmo.Text = ds_Lista.Tables(0).Rows(0).Item("DescConfirmacion").ToString
            lbl_LugarConfirmacion.Text = ds_Lista.Tables(0).Rows(0).Item("ConfirmacionLugar").ToString
            lbl_ConfirmacionAnio.Text = ds_Lista.Tables(0).Rows(0).Item("ComfirmacionAnio").ToString

            '....DATOS OTROS
            lbl_EstadoCivil.Text = ds_Lista.Tables(0).Rows(0).Item("Estadocivil").ToString
            lbl_CantidadHermanos.Text = ds_Lista.Tables(0).Rows(0).Item("CantidadHermanos").ToString
            lbl_PosicionEntreHermanos.Text = ds_Lista.Tables(0).Rows(0).Item("PosicionEntreHermanos").ToString
            lbl_EmailPersonal.Text = ds_Lista.Tables(0).Rows(0).Item("EmailPersonal").ToString
            lbl_CelularPersonal.Text = ds_Lista.Tables(0).Rows(0).Item("Celular").ToString


            ''cargar grilla informacion adicional
            grwInformacionAdicional.DataSource = ds_Lista.Tables(8)
            grwInformacionAdicional.DataBind()
            ''grwInformacionAdicional.


            Dim dtTienePendiente As New Data.DataTable
            dtTienePendiente = ds_Lista.Tables(9)
            Dim tienePendiente As Boolean = False
            tienePendiente = Convert.ToBoolean(dtTienePendiente.Rows(0)("tienePendiente").ToString())

            If tienePendiente = True Then
                btn_SolicitarActualizarDatos.Enabled = False
                btn_SolicitarActualizarDatos.ToolTip = "Usted tiene una solicitud pendiente de aprobacion"
            ElseIf tienePendiente = False Then
                btn_SolicitarActualizarDatos.Enabled = True
                btn_SolicitarActualizarDatos.ToolTip = "Enviar solicitud de aprobacion "
            End If


            Dim dtDatosSolicitante As New DataTable
            '' cargar informacion del  ultimo familiar q actualizo la informacion del ulumno 
            dtDatosSolicitante = ds_Lista.Tables(10)


            If dtDatosSolicitante.Rows.Count > 0 Then
                lblFechaActualizacion.Text = dtDatosSolicitante.Rows(0)("FechaRegistroSolicitud").ToString()
                lblNombreActualizacion.Text = dtDatosSolicitante.Rows(0)("NombreCompleto_FamiliarSolicitante").ToString()
            Else
                lblFechaActualizacion.Text = "--"
                lblNombreActualizacion.Text = "--"
            End If

            'DescParentesco_FamiliarSolicitante	NombreCompleto_AlumnoActualizar	FechaRegistroSolicitud
            'Papá	MACEDO GRADOS, Mario Santiago	25/06/2012


            ''
            ' lengua
            If ds_Lista.Tables(2).Rows.Count > 0 Then

                If ds_Lista.Tables(2).Rows(0).Item("CodigoRelacion") > 0 Then
                    lbl_LenguaMaterna.Text = ds_Lista.Tables(2).Rows(0).Item("idioma")
                Else
                    lbl_LenguaMaterna.Text = "-"
                End If
                If ds_Lista.Tables(2).Rows.Count > 1 Then
                    If ds_Lista.Tables(2).Rows(1).Item("CodigoRelacion") > 0 Then
                        lbl_SegundaLengua.Text = ds_Lista.Tables(2).Rows(1).Item("idioma")
                    Else
                        lbl_SegundaLengua.Text = "-"
                    End If
                Else
                    lbl_SegundaLengua.Text = "-"
                End If

            Else
                lbl_LenguaMaterna.Text = "-"
                lbl_SegundaLengua.Text = "-"
            End If

            '....DATOS SITUACION ACTUAL
            lbl_EstadoActual.Text = ds_Lista.Tables(0).Rows(0).Item("EstadoActualAlumno").ToString
            lbl_AnioAcademicoActual.Text = ds_Lista.Tables(0).Rows(0).Item("AnioActualAlumno").ToString
            lbl_NivelGradoSeccionActual.Text = ds_Lista.Tables(0).Rows(0).Item("ENSnGS").ToString
            lbl_house.Text = ds_Lista.Tables(0).Rows(0).Item("House").ToString

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
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
