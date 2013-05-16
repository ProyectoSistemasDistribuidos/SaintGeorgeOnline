Imports System.Data
Imports System.IO
Imports SaintGeorgeOnline_BusinessLogic.ModuloReportes
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloLogueo
Imports SaintGeorgeOnline_Utilities
Partial Class Modulo_Reportes_ReportesPermisos
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 0
    Private cod_Opcion As Integer = 14

#Region "Evento"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.Master.MostrarTitulo("Reportes Usuarios en el Sistema")
            btnReporteExportar.Attributes.Add("onclick", "ShowMyModalPopup()")
            cargarListaReportes()
            cargarListaPresentacion()
            pnlReporte1.Visible = True
            cargarCombos()
            tbFechaInicio.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy")
            tbFechaFin.Text = Today
        End If
    End Sub

    Protected Sub lstReportes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarListaPresentacion()
            mostrarPanelParametros()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnReporteExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim usp_mensaje As String = ""
        Try
            If validar(usp_mensaje) Then
                If lstReportes.SelectedValue = 8 Then
                    ExportarUsuariosEnElSistema()
                End If
            Else
                MostrarAlertBox(usp_mensaje)
            End If
        Catch ex As Exception
            EnvioEmailError(1, ex.ToString)
        End Try
    End Sub
#End Region

#Region "Metodo"

    Private Sub cargarListaReportes()

        Dim int_CodigoTipoReporte As Integer = 2 ' Reportes de Acceso al Sistema
        Dim obj_BL_Reportes As New bl_Reportes
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Reportes.FUN_LIS_Reportes(int_CodigoTipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("ListaReportes") = ds_Lista

        lstReportes.DataSource = ds_Lista.Tables(0)
        lstReportes.DataTextField = "Nombre"
        lstReportes.DataValueField = "Codigo"
        lstReportes.DataBind()

        lstReportes.SelectedIndex = 0

    End Sub

    Private Sub cargarListaPresentacion()

        Dim dt As DataTable = CType(ViewState("ListaReportes"), DataSet).Tables(1)
        Dim int_CodigoReporte As Integer = lstReportes.SelectedValue

        Dim dv As DataView = dt.DefaultView

        With dv
            .RowFilter = "1=1 and CodigoReporte = " & int_CodigoReporte
        End With

        lstPresentacion.DataSource = dt
        lstPresentacion.DataTextField = "Descripcion"
        lstPresentacion.DataValueField = "CodigoDetalle"
        lstPresentacion.DataBind()


        lstPresentacion.SelectedIndex = 0

    End Sub

    Private Sub mostrarPanelParametros()
        pnlReporte1.Visible = True
    End Sub

    ''' <summary>
    ''' Muestra mensajes de alerta sobre las acciones que se realizan en los distintos formularios.    
    ''' </summary>
    ''' <param name="str_mensaje">Descripción del mensaje que se mostrará en el formulario</param>
    ''' <param name="str_tipoMensaje">Definición de Tipo de Icono que se mostrará en el mensaje</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     29/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado

        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    Private Sub cargarCombos()
        cargarComboUsuario()
    End Sub

    ''' <summary>
    ''' Carga el combo con la lista de Indicaciones de Tipo de Personas en estado activo
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     20/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub cargarComboUsuario()
        Dim obj_BL_TiposPersonas As New bl_TiposPersonas
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_TiposPersonas.FUN_LIS_TiposPersonas("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 2)
        Controles.llenarCombo(ddlTipoUsuario, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Function validar(ByRef str_Mensaje As String) As Boolean
        Dim result As Boolean = True
        Dim str_alertas As String = ""
        Dim int_check As Integer = 0
        Dim boolFecha As Boolean = True
        Dim boolFechaCompromiso1 As Boolean = True
        Dim boolFechaVencimiento As Boolean = True

        Dim c As Integer = 0

        If lstReportes.SelectedValue = "" Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 3, "Tipo de Reporte")
            result = False
        End If

        If tbFechaInicio.Text = "" Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Fecha Inicio")
            result = False
        End If
        If tbFechaFin.Text = "" Then
            str_alertas = Alertas.ObtenerAlerta(str_alertas, 1, "Fecha Fin")
            result = False
        End If
        If tbFechaInicio.Text <> "" And tbFechaFin.Text <> "" Then
            Dim vFechaInicio As Date = tbFechaInicio.Text
            Dim vFechaFin As Date = tbFechaFin.Text
            If vFechaInicio > vFechaFin Then
                str_alertas = Alertas.ObtenerAlerta(str_alertas, 32, "Fecha Inico y Fecha Fin es")
                result = False
            End If
        End If

        str_Mensaje = str_alertas
        Return result

    End Function

    ''' <summary>
    ''' Exporte el listado de la información filtrada en los diferentes formatos indicados.
    ''' </summary>
    ''' <remarks>
    ''' Creador:               EDGAR CHANG
    ''' Fecha de Creación:     01/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Sub ExportarUsuariosEnElSistema()
        Dim str_FechaInicio As String = CStr(tbFechaInicio.Text)
        Dim str_PeriodoFin As String = CStr(tbFechaFin.Text)
        Dim int_CodTipoPersonas As Integer = CInt(ddlTipoUsuario.SelectedValue)
        Dim int_CodigoUsuario As Integer = 1 'Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = 1 'Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim obj_BL_Logeo As New bl_Logueo

        Dim ds_Lista As DataSet = obj_BL_Logeo.FUN_REP_DinamicoUsuariosEnElSistema(str_FechaInicio, str_PeriodoFin, int_CodTipoPersonas, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        If ds_Lista.Tables(0).Rows.Count > 0 Then
            Dim rutamadre As String = ""
            Dim downloadBytes As Byte()
            Dim contenido_exportar As String = ""
            Dim NombreArchivo As String = ""
            If lstPresentacion.SelectedValue = 9 Then
                NombreArchivo = Exportacion.ExportarReporteUsuariosEnElSistemaGeneral(ds_Lista, str_FechaInicio, str_PeriodoFin, "Usuarios en el Sistema")
            ElseIf lstPresentacion.SelectedValue = 10 Then
                NombreArchivo = Exportacion.ExportarReporteUsuariosEnElSistemaDetalle(ds_Lista, str_FechaInicio, str_PeriodoFin, "Usuarios en el Sistema")
            End If
            NombreArchivo = NombreArchivo & ".xls"
            rutamadre = Server.MapPath(".")
            rutamadre = rutamadre.Replace("\Modulo_Reportes", "\Reportes\")

            downloadBytes = File.ReadAllBytes(rutamadre & NombreArchivo)

            Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
        Else
            MsgBox("No se encontraron registros.", MsgBoxStyle.Information, "Usuarios en el sistema.")
        End If

    End Sub

    Protected Sub MostrarAlertBox(ByVal str_Mensaje As String)
        Me.Master.MostrarMensajeAlert(str_Mensaje)
    End Sub

#End Region

End Class