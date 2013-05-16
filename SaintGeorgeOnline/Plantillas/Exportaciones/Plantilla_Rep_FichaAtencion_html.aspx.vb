
Partial Class Plantillas_Exportaciones_Plantilla_Rep_FichaAtencion_html
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("Exportaciones_RepFichaAtencionHtml"))

        Session("Exportaciones_RepFichaAtencionHtml") = Nothing
        Session.Remove("Exportaciones_RepFichaAtencionHtml")

    End Sub

End Class
