
Partial Class Plantillas_Exportaciones_Plantilla_Rep_FichaAtencionFamilia_html
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("Exportaciones_RepFichaAtencionFamiliaHtml"))

        Session("Exportaciones_RepFichaAtencionFamiliaHtml") = Nothing
        Session.Remove("Exportaciones_RepFichaAtencionFamiliaHtml")

    End Sub
End Class
