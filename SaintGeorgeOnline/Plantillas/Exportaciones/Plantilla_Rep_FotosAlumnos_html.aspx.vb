
Partial Class Plantillas_Exportaciones_Plantilla_Rep_FotosAlumnos_html
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("Exportaciones_FotosAlumnosHtml"))

        Session("Exportaciones_FotosAlumnosHtml") = Nothing
        Session.Remove("Exportaciones_FotosAlumnosHtml")

    End Sub

End Class
