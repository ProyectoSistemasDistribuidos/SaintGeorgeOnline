Imports System.Data

Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

''' <summary>
''' Modulo de Consulta de datos de hijos
''' </summary>
''' <remarks>
''' Código del Modulo:    5
''' Código de la Opción:  65
''' </remarks>
Partial Class Interfaz_Familia_Modulo_DatosAlumnos_DatosAlumnos
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Datos de Hijos")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                ObtenerDatosFamilia()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
        
    End Sub

    Protected Sub btn_Consultar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Session("int_CodigoHijo_pe") = sender.ValidationGroup
        Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_DatosHijos/InformacionHijos.aspx")

    End Sub

    Protected Sub btn_SolicitarActualizarDatos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        
        Session("int_CodigoHijo_pe") = sender.ValidationGroup
        Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaAlumno.aspx")

    End Sub

#End Region

#Region "Metodos"

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
        Me.Master.RegistrarAccesoPagina(5, 65)

    End Sub

    ''' <summary>
    ''' Obtiene la relación de Alumnos relacionados al familia y a la familia seleccionada del logueo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerDatosFamilia()

        Dim obj_BL_Alumnos As New bl_Alumnos
        Dim ds_DatosFamilia As New DataSet
        Dim dt_Familia As New DataTable
        Dim dt_Alumnos As New DataTable
        Dim int_CodigoFamilia As Integer = 0
        Dim str_DirectorioFoto = System.Configuration.ConfigurationManager.AppSettings.Item("RutaFotosUsuarios_Web_Alumn").ToString()
        Dim int_contAlumnos As Integer = 0

        int_CodigoFamilia = Me.Master.Obtener_CodigoFamiliaActiva

        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

        ds_DatosFamilia = obj_BL_Alumnos.FUN_LIS_AlumnosPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 2, 65)

        dt_Familia = ds_DatosFamilia.Tables(0)
        dt_Alumnos = ds_DatosFamilia.Tables(1)

        While int_contAlumnos <= dt_Alumnos.Rows.Count - 1
            dt_Alumnos.Rows(int_contAlumnos).Item("RutaFoto") = str_DirectorioFoto & dt_Alumnos.Rows(int_contAlumnos).Item("RutaFoto")

            int_contAlumnos = int_contAlumnos + 1
        End While

        ViewState("Datos_Hijos") = dt_Alumnos

        dgv_DatosHijos.DataSource = dt_Familia
        dgv_DatosHijos.DataBind()
        
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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(5, 65, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
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

    Protected Sub dgv_DatosHijos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim str_CodigoFamilia As String = ""
                Dim vst As New DataView(ViewState("Datos_Hijos"))
                Dim gdv As GridView = e.Row.Cells(0).FindControl("dgv_Hijos")
                
                str_CodigoFamilia = DataBinder.Eval(e.Row.DataItem, "CodigoFamilia")
                vst.RowFilter = "1=1 and CodigoFamilia = " + str_CodigoFamilia

                gdv.DataSource = vst
                gdv.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

    
End Class
