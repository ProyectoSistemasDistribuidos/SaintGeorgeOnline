'Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
'Imports SaintGeorgeOnline_BusinessLogic.Utilitario
'Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessLogic.ModuloActividades
Imports SaintGeorgeOnline_BusinessEntities.ModuloActividades
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

Partial Class Modulo_Actividades_RegistroInformeActividad
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1


#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Description of Activity and Report")

            If Not Page.IsPostBack Then

                btnCancelar.Attributes.Add("onclick", "return Cancelar();")

                If HttpContext.Current.Session("SS_CodigoActividad") IsNot Nothing Then

                    Dim int_codigoActividad As Integer = CInt(HttpContext.Current.Session("SS_CodigoActividad").ToString)
                    hiddenCodigoActividad.Value = int_codigoActividad

                    'HttpContext.Current.Session.Remove("SS_CodigoActividad")
                    'HttpContext.Current.Session("SS_CodigoActividad") = Nothing

                    cargarActividad()

                Else
                    Response.Redirect("frmActividad.aspx")
                End If

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Response.Redirect("frmActividad.aspx")
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Grabar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarActividad()

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obl_actividad As New BL_Actividad
        Dim int_CodigoActividad As Integer = hiddenCodigoActividad.Value
        Dim ds_lista As DataSet = obl_actividad.FUN_GET_ActividadImp(int_CodigoActividad, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        lblanio.Text = ds_lista.Tables(0).Rows(0).Item("Periodo")
        lbltitulo.Text = ds_lista.Tables(0).Rows(0).Item("Actividad")

        lstGrados.DataSource = ds_lista.Tables(1)
        lstGrados.DataValueField = "cGrado"
        lstGrados.DataTextField = "dGrado"
        lstGrados.DataBind()

        lblresponsable.Text = ds_lista.Tables(0).Rows(0).Item("Organizador")
        lbldia.Text = ds_lista.Tables(0).Rows(0).Item("FechaInicio") & " ( " & ds_lista.Tables(0).Rows(0).Item("HoraInicio") & " - " & ds_lista.Tables(0).Rows(0).Item("HoraFin") & " )"
        lbllugar.Text = ds_lista.Tables(0).Rows(0).Item("lugar")


        tbObjetivo.Text = ds_lista.Tables(3).Rows(0).Item("Objetivo")
        tblogros.Text = ds_lista.Tables(3).Rows(0).Item("Logros")
        tbdificultades.Text = ds_lista.Tables(3).Rows(0).Item("Dificultades")
        tbmimportantes.Text = ds_lista.Tables(3).Rows(0).Item("MomentosImportantes")
        tbconclusiones.Text = ds_lista.Tables(3).Rows(0).Item("Conclusiones")
        tbrecomendaciones.Text = ds_lista.Tables(3).Rows(0).Item("Recomendaciones")
        tbinformacionimagen.Text = ds_lista.Tables(3).Rows(0).Item("InformacionImagen")


    End Sub

    Private Sub Grabar() '(ByVal int_CodigoActividad As Integer, ByVal int_CodigoAprobacion As Integer, ByVal int_CodigoEstado As Integer, ByVal str_Observacion As String)

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        Dim obl_InformeActividad As New bl_InformeActividad
        Dim obe_InformeActividad As New be_InformeActividad

        obe_InformeActividad.CodigoActividad = hiddenCodigoActividad.Value
        obe_InformeActividad.Objetivo = tbObjetivo.Text.Trim
        obe_InformeActividad.Logros = tblogros.Text.Trim
        obe_InformeActividad.Dificultades = tbdificultades.Text.Trim
        obe_InformeActividad.MomentosImportantes = tbmimportantes.Text.Trim
        obe_InformeActividad.Conclusiones = tbconclusiones.Text.Trim
        obe_InformeActividad.Recomendaciones = tbrecomendaciones.Text.Trim
        obe_InformeActividad.InformacionImagen = tbinformacionimagen.Text.Trim

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        usp_valor = obl_InformeActividad.FUN_INS_InformeActividad(obe_InformeActividad, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If

    End Sub

    Private Sub EnvioEmailError(ByVal int_CodigoAccion As String, ByVal str_DetalleError As String)
        Dim int_TipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim str_NombreUsuario As String = Me.Master.Obtener_NombreUsuarioLogueado
        Dim str_MensajeUsuario As String = Alertas.EnviarMensajeErrorEmail(cod_Modulo, cod_Opcion, int_CodigoAccion, str_DetalleError, str_NombreUsuario, int_TipoUsuario)
        MostrarSexyAlertBox(str_MensajeUsuario, "Error")
    End Sub

    Protected Sub MostrarSexyAlertBox(ByVal str_Mensaje As String, ByVal str_TipoMensaje As String)
        Me.Master.MostrarMensaje(str_Mensaje, str_TipoMensaje)
    End Sub
#End Region

End Class
