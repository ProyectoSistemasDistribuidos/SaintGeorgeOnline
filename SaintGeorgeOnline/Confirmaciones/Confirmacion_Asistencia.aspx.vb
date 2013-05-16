Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloEntrevistas
Imports SaintGeorgeOnline_BusinessEntities.ModuloEntrevistas

Partial Class Confirmaciones_Confirmacion_Asistencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Try

                Dim arrayfechas() As String
                Dim cod As Integer = 0
                arrayfechas = Request.QueryString("CES").ToString.Split(",")
                cod = Val(arrayfechas(0))

                If cod > 0 Then
                    Confirmar_Asistencia(cod)
                    Enviar_Notificacion_Confirmacion(cod)
                Else
                    'mensaje error
                    Response.Redirect("/SaintGeorgeOnline/Confirmaciones/Confirmacion_Asistencia_Mensaje.aspx")
                End If

            Catch ex As Exception
                Response.Redirect("/SaintGeorgeOnline/Confirmaciones/Confirmacion_Asistencia_Mensaje.aspx")
            End Try
        End If

    End Sub

    Private Sub Confirmar_Asistencia(ByVal CodEntrevista As Integer)

        Dim result As Integer = -1
        Dim usp_mensaje As String = ""
        Dim obl_ProgramacionEntrevistas As New bl_ProgramacionEntrevistas
        Dim obe_ProgramacionEntrevistaCabecera As New be_ProgramacionEntrevistaCabecera
        obe_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera = CodEntrevista
        result = obl_ProgramacionEntrevistas.FUN_UPD_ProgramacionEntrevistasWeb(obe_ProgramacionEntrevistaCabecera, usp_mensaje, 0, 0, 0, 0)

        If result > 0 Then

        Else
            'mensaje error
        End If
    End Sub

    Private Sub Enviar_Notificacion_Confirmacion(ByVal CodEntrevista As Integer)
        Try

            Dim obl_ProgramacionEntrevistas As New bl_ProgramacionEntrevistas
            Dim obe_ProgramacionEntrevistaCabecera As New be_ProgramacionEntrevistaCabecera
            obe_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera = CodEntrevista

            Dim ds_Resultado As New DataSet
            Dim rutamadre As String = Server.MapPath(".")
            Dim ArchLecturaEstructura As String = rutamadre
            Dim fileReaderPlantilla As String = ""
            Dim dtCabecera As DataTable
            Dim dtCopiado As DataTable
            Dim dtEmails As DataTable
            Dim cont As Integer = 0

            ds_Resultado = obl_ProgramacionEntrevistas.FUN_GET_ProgramacionEntrevistasWeb(obe_ProgramacionEntrevistaCabecera, 0, 0, 0, 0)

            dtCabecera = ds_Resultado.Tables(0)
            dtEmails = ds_Resultado.Tables(1)
            dtCopiado = ds_Resultado.Tables(2)

            ArchLecturaEstructura = ArchLecturaEstructura & "\Aviso_Confirmacion_.html"
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)

            If dtCabecera.Rows.Count > 0 Then
                fileReaderPlantilla = fileReaderPlantilla.Replace("[DIA]", dtCabecera.Rows(cont).Item("Fecha"))
                fileReaderPlantilla = fileReaderPlantilla.Replace("[ESTADO]", "<SPAN style='font-size:10px;" & dtCabecera.Rows(cont).Item("Color") & "'>" & dtCabecera.Rows(cont).Item("Estado") & "</SPAN>")
                fileReaderPlantilla = fileReaderPlantilla.Replace("[HORA]", dtCabecera.Rows(cont).Item("HoraIni"))
                fileReaderPlantilla = fileReaderPlantilla.Replace("[ENTREVISTADOR]", dtCabecera.Rows(cont).Item("Entrevistador"))
                fileReaderPlantilla = fileReaderPlantilla.Replace("[FAMILIA]", dtCabecera.Rows(cont).Item("Familia"))
                fileReaderPlantilla = fileReaderPlantilla.Replace("[MOTIVO]", dtCabecera.Rows(cont).Item("Motivo"))
                fileReaderPlantilla = fileReaderPlantilla.Replace("[GRADO]", dtCabecera.Rows(cont).Item("Grado"))
                fileReaderPlantilla = fileReaderPlantilla.Replace("[ALUMNO]", dtCabecera.Rows(cont).Item("Alumno"))

                For Each dr As DataRow In dtEmails.Rows
                    fileReaderPlantilla = fileReaderPlantilla.Replace("[PARTICIPANTES]", "<li>" & dr.Item("nPersona") & "</li>" & " [PARTICIPANTES]")
                Next

                fileReaderPlantilla = fileReaderPlantilla.Replace("[PARTICIPANTES]", "")

                Dim obj_EnvioEmail As New EnvioEmail_pres
                Dim int_ExitoEnvio As Integer = 0

                Dim arr_Emails As New ArrayList
                Dim arr_EmailCopia As New ArrayList

                For Each dr As DataRow In dtEmails.Rows
                    arr_Emails.Add(dr.Item("Email"))
                Next

                For Each dr As DataRow In dtCopiado.Rows
                    arr_EmailCopia.Add(dr.Item("Email"))
                Next

                int_ExitoEnvio = obj_EnvioEmail.SendEmail_EnvioPresupuesto(arr_Emails, fileReaderPlantilla, "Confirmación de Entrevista - INTRANET", arr_EmailCopia)

            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
