Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula
Imports SaintGeorgeOnline_BusinessLogic.ModuloEntrevistas
Imports SaintGeorgeOnline_BusinessEntities.ModuloEntrevistas
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports System.Runtime.CompilerServices

Partial Class Modulo_Actividades_formatoRegistroActividades
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If Not Session("SS_CodigoProgramacionActividad") Is Nothing Then
                hiddenActividad.Value = Session("SS_CodigoProgramacionActividad")
            Else
                hiddenActividad.Value = 0
            End If

        End If
    End Sub

    Protected Sub btnWord_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim str_Contenido As String = ""

            If ddlFormato.SelectedValue = 1 Then
                str_Contenido = formatoActividades()
            ElseIf ddlFormato.SelectedValue = 2 Then
                str_Contenido = formatoInformeActividades()
            End If

            formatoWord(str_Contenido)

        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub btnHTML_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim str_Contenido As String = ""

            If ddlFormato.SelectedValue = 1 Then
                str_Contenido = formatoActividades()
            ElseIf ddlFormato.SelectedValue = 2 Then
                str_Contenido = formatoInformeActividades()
            End If

            formatoHTML(str_Contenido)

        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub
#End Region

#Region "Metodos"
    Private Sub formatoWord(ByVal contenido As String)

        Dim sb_page As New StringBuilder

        Dim html1 As String = _
"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" + _
"<html xmlns='http://www.w3.org/1999/xhtml'>" + _
"<head><title></title>" + _
"<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" + _
"<style type='text/css'>" + _
        "body, html{padding:0; margin:auto; font-family: Arial; font-size: 10px; font-family: Arial}" + _
        "ul{ margin: 0; padding: 0;}" + _
        "li{ margin: 0; padding: 0;}" + _
        "h1,h2,h3{ margin: 0; padding: 0; text-decoration: underline;}" + _
    "</style>" + _
"</head>" + _
"<body>" + _
    "<div style='margin: auto; padding: 0; width:620px; border: 0'>" + _
    "<div id='miDiv'>"

        Dim html2 As String = _
    "</div>" + _
    "</div>" + _
"</body>" + _
"</html>"

        sb_page.Append(html1)
        sb_page.Append(contenido.ToString)
        sb_page.Append(html2)

        Dim reporte As String = ""
        If ddlFormato.SelectedValue = 1 Then
            reporte = "Actividad"
        ElseIf ddlFormato.SelectedValue = 2 Then
            reporte = "InformeActividad"
        End If

        Dim byteArray As Byte() = StringToArrByte(sb_page.ToString)
        Response.BufferOutput = True
        Response.Clear()
        Response.ClearHeaders()
        Dim timeStamp As String = Convert.ToString(DateTime.Now.ToString("MMddyyyy_HHmmss"))
        Response.AddHeader("Content-Type", "binary/octet-stream; charset=UTF-8")
        Response.Charset = "UTF-8"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & reporte & "_" + timeStamp + ".doc; size=" + byteArray.Length.ToString())
        Response.BinaryWrite(byteArray)
        Response.End()

    End Sub

    Private Sub formatoHTML(ByVal contenido As String)
        Session("SS_ImpresionRegistroActividades") = contenido.ToString
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "imp", "<script language='JavaScript' type='text/javascript'>ImpresionFicha();</script>", False)
    End Sub

    Private Function formatoPadre() As String

        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1

        Dim obl_ProgramacionEntrevistas As New bl_ProgramacionEntrevistas
        Dim obl_ProgramacionEntrevistaCabecera As New be_ProgramacionEntrevistaCabecera

        obl_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera = hiddenActividad.Value

        Dim ds_lista As DataSet = obl_ProgramacionEntrevistas.FUN_GET_ProgramacionEntrevistasConFicha(obl_ProgramacionEntrevistaCabecera, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim sb_HTML As New StringBuilder
        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 650px;'")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='center' valign='middle' colspan='2'><br /><h2>Registro de Entrevista a Padres de Familia</h2><br /></td></tr>")

        ' 1
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>1. Datos de la Entrevista</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Fecha de Entrevista:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Fecha") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Familia:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Familia") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Alumno:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Alumno") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Periodo / Grado / Aula:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Periodo") & " / " _
                                                                                               & ds_lista.Tables(0).Rows(0).Item("Grado") & " / " _
                                                                                               & ds_lista.Tables(0).Rows(0).Item("Aula") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Entrevistador:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Entrevistador") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Participantes:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(1).Rows
            sb_HTML.Append("<li>" & dr.Item("nPersona") & "</li>")
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Asistentes:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(6).Rows
            If dr.Item("chk") = 1 Then
                sb_HTML.Append("<li>" & dr.Item("Descripcion") & "</li>")
            End If
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")


        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Solicitada por:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("TipoSolicitante") & "</td>")
        sb_HTML.Append("</tr>")

        ' 2
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>2. Motivos</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        Dim lstMotivosCab = _
        From sql1 In ds_lista.Tables(5).AsEnumerable _
        Select CodigoMotivoEntrevista = CInt(sql1("CodigoMotivoEntrevista").ToString), _
              MotivoEntrevista = sql1("MotivoEntrevista").ToString _
        Distinct

        Dim bool_it As Boolean = False
        For Each item In lstMotivosCab
            bool_it = False
            For Each dr As DataRow In ds_lista.Tables(5).Rows
                If dr.Item("chk") = 1 And dr.Item("CodigoMotivoEntrevista") = item.CodigoMotivoEntrevista Then
                    If bool_it = False Then
                        sb_HTML.Append("<tr><td style='width: 150px; height: 15px;' align='left' valign='middle'><span style='padding-left:10px'>" & dr.Item("MotivoEntrevista") & "</span></td>")
                        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'></td></tr>")
                    End If
                    bool_it = True
                    sb_HTML.Append("<tr><td style='width: 150px; height: 15px;' align='left' valign='middle'></td>")
                    sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'><ul><li><span>" & dr.Item("SubMotivoEntrevista") & "</span></li></ul></td></tr>")
                End If
            Next
        Next

        ' 3
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>3. Aspectos Tratados</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px; text-align: justify;' align='left' valign='middle' colspan='2'>" & Replace(ds_lista.Tables(0).Rows(0).Item("AspectosTratados"), vbLf, "<br>") & "</td></tr>")

        ' 4
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>4. Acuerdos</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")

        For Each dr As DataRow In ds_lista.Tables(4).Rows
            sb_HTML.Append("<tr><td style='width: 150px; height: 15px;' align='center' valign='middle'><span>" & dr.Item("fecha") & "</span></td>")
            sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'><span>" & dr.Item("acuerdo") & "</span></td></tr>")
        Next

        ' 5
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>Acuerdos Específicos:</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /><br /><br /><br />")

        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 650px;'><tr/>")
        sb_HTML.Append("<td style='text-align: center; width: 150px;'>______________________<br /><span>Nombre:</span></td>")
        sb_HTML.Append("<td style='text-align: center; width: 150px;'>______________________<br /><span>Nombre:</span></td>")
        sb_HTML.Append("<td style='text-align: center; width: 150px;'>______________________<br /><span>Nombre:</span></td>")
        sb_HTML.Append("<td style='text-align: center; width: 200px;'></div>")
        sb_HTML.Append("</tr></table>")

        sb_HTML.Append("</td></tr>")
        sb_HTML.Append("</table>")

        Return sb_HTML.ToString

    End Function

    Private Function formatoColegio() As String

        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1

        Dim obl_ProgramacionEntrevistas As New bl_ProgramacionEntrevistas
        Dim obl_ProgramacionEntrevistaCabecera As New be_ProgramacionEntrevistaCabecera

        obl_ProgramacionEntrevistaCabecera.CodigoProgramacionEntrevistaCabecera = hiddenActividad.Value

        Dim ds_lista As DataSet = obl_ProgramacionEntrevistas.FUN_GET_ProgramacionEntrevistasConFicha(obl_ProgramacionEntrevistaCabecera, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim sb_HTML As New StringBuilder
        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 650px;'")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='center' valign='middle' colspan='2'><br /><h2>Registro de Entrevista a Padres de Familia - Formato Interno</h2><br /></td></tr>")

        ' 1
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>1. Datos de la Entrevista</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Fecha de Entrevista:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Fecha") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Familia:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Familia") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Alumno:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Alumno") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Periodo / Grado / Aula:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Periodo") & " / " _
                                                                                               & ds_lista.Tables(0).Rows(0).Item("Grado") & " / " _
                                                                                               & ds_lista.Tables(0).Rows(0).Item("Aula") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Entrevistador:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Entrevistador") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Participantes:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(1).Rows
            sb_HTML.Append("<li>" & dr.Item("nPersona") & "</li>")
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Asistentes:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(6).Rows
            If dr.Item("chk") = 1 Then
                sb_HTML.Append("<li>" & dr.Item("Descripcion") & "</li>")
            End If
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 150px; height: 15px;' align='left' valign='middle'><span>Solicitada por:</span></td>")
        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("TipoSolicitante") & "</td>")
        sb_HTML.Append("</tr>")

        ' 2 
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>2. Comentarios de Profesores</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br />")
        If ds_lista.Tables(1).Rows.Count > 0 Then
            sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: solid 1px #000000; width: 650px;'>")
            sb_HTML.Append("<tr>")
            sb_HTML.Append("<th style='width: 120px; height: 15px; border-right: solid 1px #000000;' align='center' valign='middle'>Curso</th>")
            sb_HTML.Append("<th style='width: 200px; height: 15px; border-right: solid 1px #000000;' align='center' valign='middle'>Profesor</th>")
            sb_HTML.Append("<th style='width: 330px; height: 15px;' align='center' valign='middle'>Comentario</th>")
            sb_HTML.Append("</tr>")
            For Each dr As DataRow In ds_lista.Tables(2).Rows
                sb_HTML.Append("<tr>")
                sb_HTML.Append("<td style='width: 120px; height: 15px; border-right: solid 1px #000000; border-top: solid 1px #000000;' align='left' valign='middle'>" & dr.Item("Curso") & "</td>")
                sb_HTML.Append("<td style='width: 200px; height: 15px; border-right: solid 1px #000000; border-top: solid 1px #000000;' align='left' valign='middle'>" & dr.Item("nPersona") & "</td>")
                sb_HTML.Append("<td style='width: 330px; height: 15px; border-top: solid 1px #000000;' align='left' valign='middle'>" & dr.Item("Comentario") & "</td>")
                sb_HTML.Append("</tr>")
            Next
            sb_HTML.Append("</table>")
        End If
        sb_HTML.Append("</td></tr>")

        ' 3
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>3. Motivos</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        Dim lstMotivosCab = _
        From sql1 In ds_lista.Tables(5).AsEnumerable _
        Select CodigoMotivoEntrevista = CInt(sql1("CodigoMotivoEntrevista").ToString), _
              MotivoEntrevista = sql1("MotivoEntrevista").ToString _
        Distinct

        Dim bool_it As Boolean = False
        For Each item In lstMotivosCab
            bool_it = False
            For Each dr As DataRow In ds_lista.Tables(5).Rows
                If dr.Item("chk") = 1 And dr.Item("CodigoMotivoEntrevista") = item.CodigoMotivoEntrevista Then
                    If bool_it = False Then
                        sb_HTML.Append("<tr><td style='width: 150px; height: 15px;' align='left' valign='middle'><span style='padding-left:10px'>" & dr.Item("MotivoEntrevista") & "</span></td>")
                        sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'></td></tr>")
                    End If
                    bool_it = True
                    sb_HTML.Append("<tr><td style='width: 150px; height: 15px;' align='left' valign='middle'></td>")
                    sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'><ul><li><span>" & dr.Item("SubMotivoEntrevista") & "</span></li></ul></td></tr>")
                End If
            Next
        Next

        ' 4
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>4. Aspectos Tratados</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px; text-align: justify;' align='left' valign='middle' colspan='2'>" & Replace(ds_lista.Tables(0).Rows(0).Item("AspectosTratados"), vbLf, "<br>") & "</td></tr>")

        ' 5
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>5. Acuerdos</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        For Each dr As DataRow In ds_lista.Tables(4).Rows
            sb_HTML.Append("<tr><td style='width: 150px; height: 15px;' align='center' valign='middle'><span>" & dr.Item("fecha") & "</span></td>")
            sb_HTML.Append("<td style='width: 500px; height: 15px;' align='left' valign='middle'><span>" & dr.Item("acuerdo") & "</span></td></tr>")
        Next

        ' 5.1
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>Acuerdos Específicos:</h3><br /><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")


        ' 6
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><h3>6. Comentarios / Seguimiento:</h3></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 650px; height: 15px; text-align: justify;' align='left' valign='middle' colspan='2'>" & Replace(ds_lista.Tables(0).Rows(0).Item("Comentario"), vbLf, "<br>") & "</td></tr>")


        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'><br /><br /><br /><br /><br /></td></tr>")

        sb_HTML.Append("<tr><td style='width: 650px; height: 15px;' align='left' valign='middle' colspan='2'>")
        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 650px;'><tr/>")
        sb_HTML.Append("<td style='text-align: center; width: 150px;'>______________________<br /><span>Nombre:</span></td>")
        sb_HTML.Append("<td style='text-align: center; width: 150px;'>______________________<br /><span>Nombre:</span></td>")
        sb_HTML.Append("<td style='text-align: center; width: 150px;'>______________________<br /><span>Nombre:</span></td>")
        sb_HTML.Append("<td style='text-align: center; width: 200px;'></div>")
        sb_HTML.Append("</tr></table>")
        sb_HTML.Append("</td></tr>")

        sb_HTML.Append("</table>")

        Return sb_HTML.ToString

    End Function



    Private Function formatoActividades() As String

        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1

        Dim obl_actividad As New BL_Actividad
        Dim int_CodigoActividad As Integer = hiddenActividad.Value

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

    Private Function formatoInformeActividades() As String

        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1

        Dim obl_actividad As New BL_Actividad
        Dim int_CodigoActividad As Integer = hiddenActividad.Value

        Dim ds_lista As DataSet = obl_actividad.FUN_GET_ActividadImp(int_CodigoActividad, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)

        Dim sb_HTML As New StringBuilder
        sb_HTML.Append("<table cellpadding='0' cellspacing='0' border='0' style='border: 0; width: 600px;'")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='center' valign='middle' colspan='2'><br /><h2>INFORME DE ACTIVIDADES</h2><br /><br /></td></tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Título de la actividad</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Actividad") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Grados</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(1).Rows
            sb_HTML.Append("<li>" & dr.Item("dGrado") & "</li>")
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Participantes</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'><ul>")
        For Each dr As DataRow In ds_lista.Tables(2).Rows
            sb_HTML.Append("<li>" & dr.Item("dTipo") & "</li>")
        Next
        sb_HTML.Append("</ul></td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Persona Responsable</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("Organizador") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Día de la actividad</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>Inicio: " & ds_lista.Tables(0).Rows(0).Item("FechaInicio") & " " & ds_lista.Tables(0).Rows(0).Item("HoraInicio") & "</td>")
        sb_HTML.Append("</tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>Fin: " & ds_lista.Tables(0).Rows(0).Item("FechaInicio") & " " & ds_lista.Tables(0).Rows(0).Item("HoraFin") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Lugar</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & ds_lista.Tables(0).Rows(0).Item("lugar") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Objetivo del evento o actividad</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & Replace(ds_lista.Tables(3).Rows(0).Item("Objetivo"), vbLf, "<br>") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Momentos importantes</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & Replace(ds_lista.Tables(3).Rows(0).Item("MomentosImportantes"), vbLf, "<br>") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Conclusiones</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & Replace(ds_lista.Tables(3).Rows(0).Item("Conclusiones"), vbLf, "<br>") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Recomendaciones para el próximo evento del mismo tipo</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & Replace(ds_lista.Tables(3).Rows(0).Item("Recomendaciones"), vbLf, "<br>") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("<tr><td style='width: 600px; height: 15px;' align='left' valign='middle' colspan='2'><br /></td></tr>")
        sb_HTML.Append("<tr><td style='width: 600px; height: 15px; border-bottom: solid 1px #6fa4d4;' align='left' valign='middle' colspan='2'><b>Informe para Boletin</b></td></tr>")
        sb_HTML.Append("<tr>")
        sb_HTML.Append("<td style='width: 170px; height: 15px;' align='left' valign='middle'></td>")
        sb_HTML.Append("<td style='width: 430px; height: 15px;' align='left' valign='middle'>" & Replace(ds_lista.Tables(3).Rows(0).Item("InformacionImagen"), vbLf, "<br>") & "</td>")
        sb_HTML.Append("</tr>")

        sb_HTML.Append("</table>")

        Return sb_HTML.ToString
    End Function


    Public Function StringToArrByte(ByVal str_Data As String) As Byte()
        'Dim arr_Data() As Byte = System.Text.UTF8Encoding.ASCII.GetBytes(str_Data)
        Dim arr_Data() As Byte = System.Text.UTF8Encoding.UTF8.GetBytes(str_Data)
        Return arr_Data
    End Function
#End Region

End Class
