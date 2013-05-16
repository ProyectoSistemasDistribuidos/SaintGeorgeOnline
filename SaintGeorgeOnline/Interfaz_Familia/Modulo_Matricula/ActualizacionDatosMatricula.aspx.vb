Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula

Partial Class Interfaz_Familia_Modulo_Matricula_ActualizacionDatosMatricula
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 4
    Private cod_Opcion As Integer = 74

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Me.Master.MostrarTitulo("Actualización de Datos")

            If Not Page.IsPostBack Then

                'Session("ActualizacionDatosMatricula") = "20050019,20050019"

                If Session("ActualizacionDatosMatricula") IsNot Nothing Then


                    ' Guardo los datos referente a la matrícula del alumno
                    Dim arr_Datos As String() = Session("ActualizacionDatosMatricula").ToString.Split(",")

                    hiddenCodigoAlumno.Value = arr_Datos(0).ToString
                    hiddenCodigoAnioAcademico.Value = arr_Datos(1).ToString
                    hiddenCodigoFamiliar.Value = arr_Datos(2).ToString
                    hiddenCodigoNivel.Value = arr_Datos(3).ToString
                    hiddenCodigoGrado.Value = arr_Datos(4).ToString
                    hiddenNivel.Value = arr_Datos(5).ToString
                    hiddenGrado.Value = arr_Datos(6).ToString
                    hiddenNombreCompleto.Value = arr_Datos(7).ToString
                    hiddenFoto.Value = arr_Datos(8).ToString
                    hiddenCodigoFamilia.Value = arr_Datos(9).ToString

                    'Session.Remove("ActualizacionDatosMatricula")
                    'Session("ActualizacionDatosMatricula") = Nothing

                    'Iframe1.Attributes.Add("src", "../Modulo_SolicitudesActualizacionInformacion/SolicitudFichaFamiliar.aspx")
                    'Iframe2.Attributes.Add("src", "../Modulo_SolicitudesActualizacionInformacion/SolicitudFichaAlumno.aspx")
                    'Iframe3.Attributes.Add("src", "../Modulo_SolicitudesActualizacionInformacion/SolicitudFichaMedicaAlumno.aspx")

                    Dim int_CodigoFamilia As Integer = hiddenCodigoFamilia.Value
                    Dim int_CodigoalumnoMatricula As String = hiddenCodigoAlumno.Value

                    Dim int_CodigoEtapa As Integer = 1

                    Dim obj_BL_Matricula As New bl_Matricula
                    Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
                    Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

                    Dim ds_Lista As DataSet = obj_BL_Matricula.FUN_LIS_UltimoPasoMatriculaAlumno(int_CodigoalumnoMatricula, int_CodigoEtapa, int_CodigoUsuario, int_CodigoTipoUsuario, 5, 79)

                    Dim int_CodigoPaso As Integer
                    int_CodigoPaso = ds_Lista.Tables(0).Rows(0).Item("CodigoPasoMatricula")

                    Dim int_CantFam As Integer = ds_Lista.Tables(0).Rows(0).Item("cant")

                    ' Mostrar Panel Cargando
                    pnlLoadFrame1.Visible = True
                    pnlLoadFrame2.Visible = True
                    pnlLoadFrame3.Visible = True
                    pnlFrame1.Visible = False
                    pnlFrame2.Visible = False
                    pnlFrame3.Visible = False

                    If int_CodigoPaso = 0 Then ' si no ha realizado ninguna actualización

                        pnlActualizacionDatos.Visible = True
                        pnlFinActualizacion.Visible = False

                        TabContainer1.ActiveTabIndex = 0
                        miTab1.Enabled = True
                        miTab2.Enabled = False
                        miTab3.Enabled = False

                        obtenerDatosFamilia(int_CodigoFamilia)

                        pnlLoadFrame1.Visible = False
                        pnlFrame1.Visible = True

                    ElseIf int_CodigoPaso = 6 Then ' si ya realizo la primera actualización : Datos Familiares

                        ' '' si ya actualizo a los 2 padres
                        ''If int_CantFam >= 2 Then

                        ''    pnlActualizacionDatos.Visible = True
                        ''    pnlFinActualizacion.Visible = False

                        ''    TabContainer1.ActiveTabIndex = 1
                        ''    miTab1.Enabled = False 'True 'False
                        ''    miTab2.Enabled = True
                        ''    miTab3.Enabled = False

                        ''    obtenerDatosFamilia(int_CodigoFamilia)
                        ''    obtenerDatosAlumnoMatricula(int_CodigoalumnoMatricula)

                        ''    pnlLoadFrame1.Visible = False
                        ''    pnlFrame1.Visible = False 'True
                        ''    pnlLoadFrame2.Visible = False
                        ''    pnlFrame2.Visible = True

                        ''Else

                        ''    pnlActualizacionDatos.Visible = True
                        ''    pnlFinActualizacion.Visible = False

                        ''    TabContainer1.ActiveTabIndex = 0
                        ''    miTab1.Enabled = True
                        ''    miTab2.Enabled = False
                        ''    miTab3.Enabled = False

                        ''    obtenerDatosFamilia(int_CodigoFamilia)

                        ''    pnlLoadFrame1.Visible = False
                        ''    pnlFrame1.Visible = True

                        ''End If

                        pnlActualizacionDatos.Visible = True
                        pnlFinActualizacion.Visible = False

                        TabContainer1.ActiveTabIndex = 1
                        miTab1.Enabled = False 'True 'False
                        miTab2.Enabled = True
                        miTab3.Enabled = False

                        'obtenerDatosFamilia(int_CodigoFamilia)
                        obtenerDatosAlumnoMatricula(int_CodigoalumnoMatricula)

                        pnlLoadFrame1.Visible = False
                        pnlFrame1.Visible = False 'True
                        pnlLoadFrame2.Visible = False
                        pnlFrame2.Visible = True

                    ElseIf int_CodigoPaso = 7 Then ' si ya realizo la segunda actualización : Datos Familiares y Datos Alumno

                        pnlActualizacionDatos.Visible = True
                        pnlFinActualizacion.Visible = False

                        TabContainer1.ActiveTabIndex = 2
                        miTab1.Enabled = False 'True 'False
                        miTab2.Enabled = False
                        miTab3.Enabled = True

                        'obtenerDatosFamilia(int_CodigoFamilia)
                        obtenerDatosFichaMedicaAlumno(int_CodigoalumnoMatricula)

                        pnlLoadFrame1.Visible = False
                        pnlFrame1.Visible = False 'True
                        pnlLoadFrame3.Visible = False
                        pnlFrame3.Visible = True

                    ElseIf int_CodigoPaso = 8 Then ' si ya realizo la tercera actualización : Datos Familiares, Datos Alumno y Datos de la Ficha Médica

                        pnlActualizacionDatos.Visible = False
                        pnlFinActualizacion.Visible = True

                        miTab1.Enabled = False
                        miTab2.Enabled = False
                        miTab3.Enabled = False

                        'Muestro mensaje 
                        Dim str_Mensaje As New StringBuilder

                        str_Mensaje.Append("Operación Exitosa.\n")
                        str_Mensaje.Append("<em>Los datos enviados serán verificados por el colegio y se le enviará un e-mail de confirmación")
                        str_Mensaje.Append("en un lapso de 48 horas.\nSi tuviera algún inconveniente por favor comuníquese al teléfono: 4458147.")

                        Me.Master.MostrarMensajeAlert(str_Mensaje.ToString)

                    Else
                        pnlActualizacionDatos.Visible = False
                        pnlFinActualizacion.Visible = True

                        miTab1.Enabled = False
                        miTab2.Enabled = False
                        miTab3.Enabled = False
                    End If

                    'obtenerDatosFamilia(int_CodigoFamilia)
                    'obtenerDatosAlumnoMatricula(int_CodigoalumnoMatricula)
                    'obtenerDatosFichaMedicaAlumno(int_CodigoalumnoMatricula)

                End If

            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Matricula.aspx")
    End Sub

#End Region

#Region "Metodos"

    Private Sub obtenerDatosFamilia(ByVal int_CodigoFamilia As Integer)

        Iframe1.Attributes.Add("src", "../Modulo_SolicitudesActualizacionInformacion/SolicitudFichaFamiliar.aspx?codigoFamilia=" & int_CodigoFamilia.ToString)

    End Sub

    Private Sub obtenerDatosAlumnoMatricula(ByVal int_CodigoAlumnoMatricula As Integer)

        Iframe2.Attributes.Add("src", "../Modulo_SolicitudesActualizacionInformacion/SolicitudFichaAlumno.aspx?codigoAlumno=" & int_CodigoAlumnoMatricula.ToString)

    End Sub

    Private Sub obtenerDatosFichaMedicaAlumno(ByVal int_CodigoAlumnoMatricula As Integer)

        Iframe3.Attributes.Add("src", "../Modulo_SolicitudesActualizacionInformacion/SolicitudFichaMedicaAlumno.aspx?codigoAlumno=" & int_CodigoAlumnoMatricula.ToString)

    End Sub


    ''' <summary>
    ''' Registra el Log de pasos de matricula.
    ''' </summary>
    ''' <param name="int_CodigoPasoMatricula">Codigo del paso de la matricula</param>
    ''' <param name="int_PeriodoAcademico">Periodo academico de la matricula</param>
    ''' <param name="int_CodigoAlumno">Codigo del alumno a matricular</param>
    ''' <param name="int_CodigoFamiliar">Codigo del familiar que esta matriculando</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub RegistrarPasoMatricula(ByVal int_CodigoPasoMatricula As Integer, ByVal int_PeriodoAcademico As Integer, ByVal int_CodigoAlumno As Integer, ByVal int_CodigoFamiliar As Integer, ByVal int_AceptacionEtapa As Integer)

        Dim obj_BE_Matricula As New be_Matricula
        Dim obj_BL_Matricula As New bl_Matricula
        Dim int_Resultado As Integer = -1
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

        obj_BE_Matricula.PeriodoAcademico = int_PeriodoAcademico
        obj_BE_Matricula.CodigoPasoMatricula = int_CodigoPasoMatricula
        obj_BE_Matricula.CodigoAlumno = int_CodigoAlumno
        obj_BE_Matricula.CodigoFamiliar = int_CodigoFamiliar
        obj_BE_Matricula.AceptacionEtapa = int_AceptacionEtapa

        int_Resultado = obj_BL_Matricula.FUN_INS_PasoMatricula(obj_BE_Matricula, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 74)

    End Sub


    ''' <summary>
    ''' Setear permisos de acciones sobre el formulario según la configuración del usuario.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(4, 74)
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
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(4, 74, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
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

#Region "Eventos de Grilla"

    Protected Sub gvDetalleIntegrantesFamilia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim int_CodigoAccion As Integer = 0

        Try
            If e.CommandName = "Seleccionar" Then
                Dim int_codigo As Integer = CInt(e.CommandArgument.ToString)
                Dim btn As ImageButton = CType(e.CommandSource, ImageButton)
                Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

                int_CodigoAccion = 3
                obtenerDatosFamilia(int_codigo)



            End If
        Catch ex As Exception
            EnvioEmailError(int_CodigoAccion, ex.ToString)
        End Try

    End Sub

    Protected Sub gvDetalleIntegrantesFamilia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

End Class