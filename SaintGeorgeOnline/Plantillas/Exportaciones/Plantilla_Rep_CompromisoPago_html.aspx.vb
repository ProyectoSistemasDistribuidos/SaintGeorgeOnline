Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class Plantillas_Exportaciones_Plantilla_Rep_CompromisoPago_html
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("Exportaciones_RepCompromisoPagoHtml"))

        Session("Exportaciones_RepCompromisoPagoHtml") = Nothing
        Session.Remove("Exportaciones_RepCompromisoPagoHtml")
        '    Dim ds As DataSet

        '    ds = Session("Exportaciones_RepCompromisoPagoHtml")

        '    Dim rutamadre As String = ""
        '    Dim downloadBytes As Byte()
        '    Dim stream As Stream
        '    Dim writer As StreamWriter
        '    'Dim contenido_exportar As String = ""
        '    Dim NombreArchivo As String = ""

        '    Dim reporte_html As String = ""
        '    Dim Arreglo_Datos As String()

        '    Arreglo_Datos = Exportacion.ExportarReporteCompromisoPago_Html(ds, "")
        '    reporte_html = Arreglo_Datos(0)
        '    NombreArchivo = Arreglo_Datos(1)
        '    NombreArchivo = NombreArchivo & ".doc"

        '    rutamadre = Server.MapPath(".")
        '    rutamadre = rutamadre.Replace("\Plantillas\Exportaciones", "\Reportes\")

        '    stream = File.OpenWrite(rutamadre & "\" & NombreArchivo)
        '    writer = New StreamWriter(stream, System.Text.Encoding.UTF8)

        '    Using (writer)
        '        writer.Write(reporte_html)
        '        writer.Flush()
        '    End Using

        '    writer.Close()
        '    downloadBytes = File.ReadAllBytes(rutamadre & "\" & NombreArchivo)

        '    Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
        '    response.Clear()
        '    response.AddHeader("Content-Type", "binary/octet-stream")
        '    response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo + "; size=" + downloadBytes.Length.ToString())
        '    response.Flush()
        '    response.BinaryWrite(downloadBytes)
        '    response.Flush()
        '    response.End()

    End Sub

End Class
