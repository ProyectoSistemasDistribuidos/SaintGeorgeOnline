
Partial Class Plantillas_Exportaciones_Plantilla_Rep_FichaAlumno_html
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("Exportaciones_RepFichaAlumnoHtml"))

        Session("Exportaciones_RepFichaAlumnoHtml") = Nothing
        Session.Remove("Exportaciones_RepFichaAlumnoHtml")

    End Sub

End Class
