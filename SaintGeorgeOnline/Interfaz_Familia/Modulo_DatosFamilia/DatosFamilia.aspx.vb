Imports System.Data

Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

''' <summary>
''' Modulo de Mantenimiento de Enfermedades
''' </summary>
''' <remarks>
''' Código del Modulo:    4
''' Código de la Opción:  64
''' </remarks>
Partial Class Interfaz_Familia_Modulo_DatosFamilia_DatosFamilia
    Inherits System.Web.UI.Page

#Region "Eventos"

#Region "Eventos generales"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Datos de la Familia")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                ObtenerDatosFamilia()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btn_Consultar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Session("int_CodigoFamiliar_pe") = sender.ValidationGroup
        Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_DatosFamilia/InformacionFamiliares.aspx")

    End Sub

    Protected Sub btn_SolicitarActualizarDatos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Session("int_CodigoFamiliar_pe") = sender.ValidationGroup
        Response.Redirect("/SaintGeorgeOnline/Interfaz_Familia/Modulo_SolicitudesActualizacionInformacion/SolicitudActualizacionFichaFamiliar.aspx")

    End Sub

#End Region

#Region "Eventos de Grilla"

    Protected Sub dgv_DatosFamilia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim str_CodigoFamilia As String = ""
                Dim vst As New DataView(ViewState("Datos_Familiares"))
                Dim gdv As GridView = e.Row.Cells(0).FindControl("dgv_Familiares")

                str_CodigoFamilia = DataBinder.Eval(e.Row.DataItem, "CodigoFamilia")
                vst.RowFilter = "1=1 and CodigoFamilia = " + str_CodigoFamilia

                gdv.DataSource = vst
                gdv.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

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
        Me.Master.RegistrarAccesoPagina(4, 64)

    End Sub

    ''' <summary>
    ''' Obtiene la relación de Familiares según el usuario en el sistema.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ObtenerDatosFamilia()

        Dim obj_BL_Familia As New bl_Familiares
        Dim ds_DatosFamilia As New DataSet
        Dim dt_Familia As New DataTable
        Dim dt_Familiares As New DataTable
        Dim int_CodigoFamilia As Integer = Me.Master.Obtener_CodigoFamiliaActiva
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

        int_CodigoFamilia = Me.Master.Obtener_CodigoFamiliaActiva

        ds_DatosFamilia = obj_BL_Familia.FUN_LIS_FamiliaresPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoTipoUsuario, 4, 64)

        dt_Familia = ds_DatosFamilia.Tables(0)
        dt_Familiares = ds_DatosFamilia.Tables(1)

        ViewState("Datos_Familiares") = dt_Familiares

        dgv_DatosFamilia.DataSource = dt_Familia
        dgv_DatosFamilia.DataBind()

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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(4, 64, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
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

End Class
