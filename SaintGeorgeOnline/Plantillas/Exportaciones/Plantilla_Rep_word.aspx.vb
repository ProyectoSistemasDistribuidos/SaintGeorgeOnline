
Partial Class Plantillas_Exportaciones_Plantilla_Rep_word
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim filename As String = "tunombredefichero"
        Dim url As String = "rutacompleta del fichero"

        Response.Buffer = False

        Response.Clear()

        Response.ClearContent()

        Response.ClearHeaders()

        Response.ContentType = "unknown"

        '    Response.AddHeader("Content-Disposition", String.Format("attachment; filename=\"{0}\"", filename))

        'Response.AddHeader("Content-Disposition", "attachment; filename=" + downloadName + "; size=" + downloadBytes.Length.ToString())
        Response.Flush()

        Response.TransmitFile(url)

        Response.End()


    End Sub

End Class
