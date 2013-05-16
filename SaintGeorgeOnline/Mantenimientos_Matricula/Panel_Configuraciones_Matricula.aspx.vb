Imports System.Data
Imports SaintGeorgeOnline_Utilities

''' <summary>
''' Modulo de Configuración de Codificadores de Matrícula
''' </summary>
''' <remarks>
''' Código del Modulo:    2
''' Código de la Opción:  27
''' </remarks>
Partial Class Mantenimientos_Matricula_Panel_Configuraciones_Matricula
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Configuraciones de Matrícula")
            If Not Page.IsPostBack Then
                SetearAccionesAcceso()
                cargarSubMenusConfiguracion()
                bl_Config_fa.Style.Value = "cursor: pointer;list-style: none;"
            End If
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Setea las acciones de acceso del usuario
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     10/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub SetearAccionesAcceso()
        Me.Master.RegistrarAccesoPagina(2, 27)
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     10/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
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

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(2, 27, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    ''' <summary>
    ''' Obtiene y muestra todas las opciones a los diversos formularios de mantenimiento que tenga asignado el usuario en su perfil de acceso.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarSubMenusConfiguracion()

        Dim cont_grupos As Integer = 0
        Dim cont_opciones As Integer = 0
        Dim ds_AccesosPermisos As New DataSet
        Dim dt_Grupos As New DataTable
        Dim dt_Modulos As New DataView
        bl_Config_fa.Style.Value = "padding: -30;margin : -30;list-style: none;"

        Try
            ds_AccesosPermisos = Session("Ac_Pe_Us")
            dt_Grupos = ds_AccesosPermisos.Tables(0)
            dt_Modulos = ds_AccesosPermisos.Tables(1).DefaultView

            While cont_grupos <= dt_Grupos.Rows.Count - 1

                If dt_Grupos.Rows(cont_grupos).Item("BM_CodigoBloque").ToString = 2 Then

                    dt_Modulos.RowFilter = " 1=1 and SBM_CodigoSubBloquePadre = 27 and BM_CodigoBloque = " & dt_Grupos.Rows(cont_grupos).Item("BM_CodigoBloque").ToString

                    While cont_opciones <= dt_Modulos.Count - 1
                        bl_Config_fa.Items.Add(dt_Modulos.Item(cont_opciones).Item("SBM_Descripcion").ToString)
                        bl_Config_fa.Items(cont_opciones).Value = dt_Modulos.Item(cont_opciones).Item("SBM_Link").ToString
                        bl_Config_fa.Items(cont_opciones).Attributes.Add("Class", "ListaItems")
                        cont_opciones = cont_opciones + 1
                    End While

                    cont_opciones = 0

                End If

                cont_grupos = cont_grupos + 1
            End While

        Catch ex As Exception

        End Try

    End Sub

#End Region

End Class
