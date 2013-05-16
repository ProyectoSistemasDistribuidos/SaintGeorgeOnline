Imports System.Data

Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

''' <summary>
''' Modulo de Mantenimiento de Enfermedades
''' </summary>
''' <remarks>
''' Código del Modulo:    0
''' Código de la Opción:  8
''' </remarks>
Partial Class Interfaz_Familia_Principal
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Home")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                CargarFormulario()
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub dgv_Familia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

    End Sub

    Protected Sub dgv_DatosHijos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

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
        Me.Master.RegistrarAccesoPagina(0, 8)

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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(0, 8, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Carga la información de los integrantes de la familia seleccionada y de los alumnos a la cual pertenece.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub CargarFormulario()

        Try
            'PARA FAMILIARES
            Dim obj_BL_Familia As New bl_Familiares
            Dim ds_DatosFamilia As New DataSet
            Dim dt_Familia As New DataTable
            Dim dt_Familiares As New DataTable
            Dim int_CodigoFamilia As Integer = 0

            'PARA HIJOS
            Dim obj_BL_Alumnos As New bl_Alumnos
            Dim ds_DatosFamilia2 As New DataSet
            Dim int_contAlumnos As Integer = 0
            Dim str_DirectorioFoto As String = System.Configuration.ConfigurationManager.AppSettings.Item("RutaFotosUsuarios_Web_Alumn").ToString()
            Dim dt_Alumnos As New DataTable

            int_CodigoFamilia = Me.Master.Obtener_CodigoFamiliaActiva

            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoFamiliarLogueado

            'OBTENER FAMILIARES
            ds_DatosFamilia = obj_BL_Familia.FUN_LIS_FamiliaresPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 8)
            dt_Familia = ds_DatosFamilia.Tables(0)
            dt_Familiares = ds_DatosFamilia.Tables(1)

            'OBTENER HIJOS
            ds_DatosFamilia2 = obj_BL_Alumnos.FUN_LIS_AlumnosPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, 0, 8)
            dt_Alumnos = ds_DatosFamilia2.Tables(1)


            lbl_NombreFamilia_Principal.Text = Me.Master.Obtener_NombreFamiliaActiva

            gv_Familia.DataSource = dt_Familiares
            gv_Familia.DataBind()

            gv_Familia.DataSource = dt_Familiares
            gv_Familia.DataBind()

            While int_contAlumnos <= dt_Alumnos.Rows.Count - 1
                dt_Alumnos.Rows(int_contAlumnos).Item("RutaFoto") = str_DirectorioFoto & dt_Alumnos.Rows(int_contAlumnos).Item("RutaFoto")

                int_contAlumnos = int_contAlumnos + 1
            End While

            dgv_Hijos.DataSource = dt_Alumnos
            dgv_Hijos.DataBind()

        Catch ex As Exception

        End Try

    End Sub

#End Region

End Class
