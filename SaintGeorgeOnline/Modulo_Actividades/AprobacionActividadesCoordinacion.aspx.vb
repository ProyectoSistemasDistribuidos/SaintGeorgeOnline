Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessEntities.ModuloActividades
Imports SaintGeorgeOnline_BusinessLogic.ModuloActividades
Imports SaintGeorgeOnline_BusinessLogic.Utilitario
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_Utilities
Imports System.Net.Mail
Imports System.Configuration.ConfigurationManager
Imports System.Security.Cryptography
Imports System.Data
Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices

Partial Class Modulo_Actividades_AprobacionActividadesCoordinacion
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Approval Activities - Coordination Area")

            If Not Page.IsPostBack Then

                cargarComboAnioAcademico()
                cargarComboMeses()
                cargarComboEstados()

                ddlPeriodo.SelectedValue = Me.Master.Obtener_CodigoPeriodoEscolar
                ddlMes.SelectedValue = Today.Month

            End If

        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Buscar()
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try

    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim fila As Control = CType(sender, LinkButton).NamingContainer
            Dim int_CodigoActividad As Integer = CInt(CType(fila.FindControl("lblcProgAct"), Label).Text)
            Dim int_CodigoAprobacion As Integer = CInt(CType(fila.FindControl("lblcRegApro"), Label).Text)
            Dim int_CodigoEstado As Integer = CInt(CType(fila.FindControl("ddlEstado"), DropDownList).SelectedValue)
            Dim str_Observacion As String = CType(fila.FindControl("tbComentario"), TextBox).Text.Trim
            Grabar(int_CodigoActividad, int_CodigoAprobacion, int_CodigoEstado, str_Observacion)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim fila As Control = CType(sender, LinkButton).NamingContainer
            Dim int_codigo As Integer = CType(sender, LinkButton).CommandArgument
            Session("SS_CodigoProgramacionActividad") = int_codigo
            ScriptManager.RegisterStartupScript(UpdatePanel1, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>FormatoImpresion();</script>", False)
        Catch ex As Exception
            EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboAnioAcademico()
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, False)
    End Sub

    Private Sub cargarComboMeses()
        Dim ds_Lista As DataSet = Controles.ListaMeses
        Controles.llenarCombo(ddlMes, ds_Lista, "Codigo", "Descripcion", True, False)
    End Sub

    Private Sub cargarComboEstados()
        Dim obj_EstadosActividades As New bl_EstadosActividades
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_EstadosActividades.FUN_LIS_EstadosActividades("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        Controles.llenarCombo(ddlEstado, ds_Lista, "Codigo", "Descripcion", False, False)
        ViewState("VS_Estados") = ds_Lista.Tables(1)
    End Sub

    Private Sub Buscar()

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim obl_AprobacionActividades As New bl_AprobacionActividades

        Dim int_CodigoPeriodo As Integer = ddlPeriodo.SelectedValue
        Dim int_Mes As Integer = ddlMes.SelectedValue
        Dim int_Estado As Integer = ddlEstado.SelectedValue
        Dim int_CodigoTrabajador As Integer = int_CodigoUsuario ' 54 ' 38 'int_CodigoUsuario
        Dim int_NivelAprobacion As Integer = 1

        Dim dc As New Dictionary(Of String, Object)
        Dim ds_lista As DataSet
        Dim nParam As String = "AC_USP_LIS_AprobacionActividadesCoordinacion"
        dc("p_CodigoPeriodo") = int_CodigoPeriodo
        dc("p_CodigoMes") = int_Mes
        dc("p_Estado") = int_Estado
        dc("p_CodigoTrabajador") = int_CodigoTrabajador
        dc("p_NivelAprobacion") = int_NivelAprobacion

        'dc("p_CodigoUsuario") = int_CodigoUsuario
        'dc("p_CodigoTipoUsuario") = int_CodigoTipoUsuario
        'dc("p_CodigoModulo") = cod_Modulo
        'dc("p_CodigoOpcion") = cod_Opcion

        ds_lista = New bl_Generico().Fun_Lis_Generico(dc, nParam)

        'Dim ds_lista As DataSet = obl_AprobacionActividades.FUN_LIS_AprobacionActividadesCoordinacion( _
        '    int_CodigoPeriodo, int_Mes, int_Estado, int_CodigoTrabajador, _
        '    int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        ViewState("VS_Detalle") = ds_lista.Tables(1)
        GridView1.DataSource = ds_lista.Tables(0)
        GridView1.DataBind()

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

    Private Sub Grabar(ByVal int_CodigoActividad As Integer, ByVal int_CodigoAprobacion As Integer, ByVal int_CodigoEstado As Integer, ByVal str_Observacion As String)

        Dim obl_AprobacionActividades As New bl_AprobacionActividades
        Dim obe_AprobacionActividades As New be_AprobacionActividades

        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado

        obe_AprobacionActividades.CodigoAprobacion = int_CodigoAprobacion
        obe_AprobacionActividades.CodigoActividad = int_CodigoActividad
        obe_AprobacionActividades.CodigoTrabajador = int_CodigoUsuario
        obe_AprobacionActividades.CodigoEstado = int_CodigoEstado
        obe_AprobacionActividades.Observacion = str_Observacion

        Dim usp_valor As Integer = 0
        Dim usp_mensaje As String = ""

        usp_valor = obl_AprobacionActividades.FUN_INS_AprobacionActividades(obe_AprobacionActividades, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        If usp_valor > 0 Then

            If int_CodigoEstado = 2 Then ' aprobado

                ' envio de email
                Dim dcValPro As New Dictionary(Of String, Object)
                Dim ds_lista As DataSet
                Dim nParam As String = "USP_LisCorreosTrabajadorAprobar"
                dcValPro("NivelAprobacion") = 2
                dcValPro("codTrabajador") = int_CodigoUsuario
                dcValPro("codActividad") = 0
                ds_lista = New bl_Generico().Fun_Lis_Generico(dcValPro, nParam)

                Dim listaCorreo As New ArrayList()
                Dim listaCorreoCo As New ArrayList()

                Dim NombreDir As String = ""
                Dim NombreApr As String = ""

                For Each dr As DataRow In ds_lista.Tables(0).Rows
                    listaCorreo.Add(dr.Item("correo"))
                    NombreDir = NombreDir + dr.Item("nombreAprobar") + ", "
                Next
                NombreDir = NombreDir.Trim
                NombreDir = NombreDir.Substring(0, NombreDir.Length - 1)

                NombreApr = ds_lista.Tables(1).Rows(0).Item("nombreAprobar").ToString

                listaCorreoCo.Add(ds_lista.Tables(2).Rows(0).Item("correo").ToString)

                Dim estadoEnvio As Integer = 0
                Dim mensaje As String = ""
                Dim asunto As String = "Aprobación de Actividad"
                Dim cabecera As String = "Estimado(a) " & NombreDir & ". <br>" & _
                                         "Se informa que la siguiente actividad ha sido aprobada por Jefatura de Nivel " & NombreApr & " para su revisión.<br><br><br>"

                Dim cuerpo As String = formatoActividades(int_CodigoActividad)

                mensaje = cabecera + cuerpo

                'comentado para pruebas 06/03/2013
                estadoEnvio = SendEmail_EnvioPresupuesto(listaCorreo, mensaje, asunto, listaCorreoCo)

            End If

            Buscar()
            MostrarSexyAlertBox(usp_mensaje, "Info")
        Else
            MostrarSexyAlertBox(usp_mensaje, "Alert")
        End If


    End Sub

    Private Function formatoActividades(ByVal int_CodigoActividad As Integer) As String

        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1

        Dim obl_actividad As New BL_Actividad

        Dim ds_lista As DataSet = obl_actividad.FUN_GET_ActividadImp(int_CodigoActividad, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim sb_HTML As New StringBuilder
        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 600px;'")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='center' valign='middle' colspan='2'><br /><h2>ACTIVITIES COORDINATION SHEET</h2><br /></td></tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'>")

        ' fecha y hora
        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 600px;'>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 400px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 40px; height: 15px;' align='left' valign='middle'><b>DATE:</b></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000;' align='center' valign='middle'><b>FROM:</b></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000; border-right: solid 1px #000000;' align='center' valign='middle'><b>TO:</b></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 400px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 40px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000;' align='center' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("FechaInicio") & "</td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000; border-right: solid 1px #000000;' align='center' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("FechaFin") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 450px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 40px; height: 15px;' align='left' valign='middle'><b>HOUR:</b></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000;' align='center' valign='middle'><b>FROM:</b></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000; border-right: solid 1px #000000;' align='center' valign='middle'><b>TO:</b></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 400px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 40px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border-left: solid 1px #000000; border-top: solid 1px #000000; border-bottom: solid 1px #000000;' align='center' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("HoraInicio") & "</td>")
        sb_HTML.Append("<td style='width: 80px; height: 15px; border: solid 1px #000000;' align='center' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("HoraFin") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("</table>")

        sb_HTML.Append("</td></tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Activity</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Type of Activity:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("tipoAct") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Activity:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Actividad") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Place:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Lugar") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>For:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(2).Rows
            sb_HTML.Append("<li>" & dr.Item("dTipo") & "</li>")
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Organiser</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Organiser:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Organizador") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Subject Area:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(1).Rows
            sb_HTML.Append("<li>" & dr.Item("dGrado") & "</li>")
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Participants</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Number of Students:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("NumAlumnos") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Number of Teachers:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("NumProfesores") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Number of Parents:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("NumPadres") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'><span>Number of Classroom Assistants:</span></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("NumAsistentesAula") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Requirements</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("ReqTecnologicos") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("ReqLogistica") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("ReqInfraestructura") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("</td></tr>")
        sb_HTML.Append("</table>")

        Return sb_HTML.ToString
    End Function

#Region "Enviar correo "
    Public Function SendEmail_EnvioPresupuesto(ByVal Emails As ArrayList, ByVal str_Cuerpo As String, ByVal str_Asunto As String, ByVal EmailCopia As ArrayList) As Integer

        Dim MailMsg As New MailMessage()
        Dim contEmail As Integer = 0

        Dim encript As New Cripto
        Dim str_SMTPServer As String = ""
        Dim str_EmailSaliente As String = ""
        Dim str_UserSaliente As String = ""
        Dim str_PasswordSaliente As String = ""

        str_SMTPServer = System.Configuration.ConfigurationManager.AppSettings.Item("SMTP_SERVER").ToString()
        str_UserSaliente = System.Configuration.ConfigurationManager.AppSettings.Item("USER_SAL").ToString()
        str_PasswordSaliente = System.Configuration.ConfigurationManager.AppSettings.Item("PASSWORD_SAL").ToString()
        str_EmailSaliente = System.Configuration.ConfigurationManager.AppSettings.Item("EMAIL_FROM_SAL").ToString()

        str_SMTPServer = encript.Desencriptar(New RC2CryptoServiceProvider, str_SMTPServer)
        str_UserSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_UserSaliente)
        str_PasswordSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_PasswordSaliente)
        str_EmailSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_EmailSaliente)

        With MailMsg
            .From = New MailAddress(str_EmailSaliente, "Colegio San Jorge - Miraflores")
            .Subject = str_Asunto
            .BodyEncoding = System.Text.Encoding.UTF8
            .Body = str_Cuerpo
            .IsBodyHtml = True
        End With

        Dim SmtpMail As New SmtpClient()
        Dim mCredential As New Net.NetworkCredential(str_UserSaliente, str_PasswordSaliente)
        SmtpMail.Credentials = mCredential
        SmtpMail.Host = str_SMTPServer

        Try
            For Each email As String In Emails
                MailMsg.To.Add(New MailAddress(email, "", System.Text.Encoding.UTF8))
            Next

            For Each email As String In EmailCopia
                MailMsg.CC.Add(New MailAddress(email, "", System.Text.Encoding.UTF8))
            Next

            SmtpMail.Send(MailMsg)

            Return 1

        Catch ex As Exception

            Return 0

        End Try

    End Function
#End Region

#End Region

#Region "Eventos del gridview"

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblidx As Label = CType(e.Row.FindControl("lblidx"), Label)
            lblidx.Text = e.Row.RowIndex + 1

            Dim lblGrades As Label = CType(e.Row.FindControl("lblGrades"), Label)
            Dim int_CodProgAct As Integer = e.Row.DataItem("cProgAct")

            Dim dt As DataTable = ViewState("VS_Detalle")
            Dim dv As DataView = dt.DefaultView
            dv.RowFilter = "cProgAct=" & int_CodProgAct

            Dim sb_grados As New StringBuilder
            For Each item In dv
                sb_grados.Append(item("dGrado") & ", ")
            Next
            lblGrades.Text = sb_grados.ToString.Substring(0, sb_grados.ToString.Length - 2)

            Dim dt2 As DataTable = ViewState("VS_Estados")
            Dim ddlEstado As DropDownList = CType(e.Row.FindControl("ddlEstado"), DropDownList)
            With ddlEstado
                .DataSource = dt2
                .DataValueField = "Codigo"
                .DataTextField = "Descripcion"
                .DataBind()
            End With
            ddlEstado.SelectedValue = e.Row.DataItem("cEstado")

            Dim btnGrabar As LinkButton = CType(e.Row.FindControl("btnGrabar"), LinkButton)
            If e.Row.DataItem("esEditable") = 1 Then
                btnGrabar.Visible = True
            Else
                btnGrabar.Visible = False
            End If

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

End Class
