
Partial Class Plantillas_Reportes_Plantilla_Rep_Enfermeria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("ReporteEnfermeria_RepHtml"))

        Session("ReporteEnfermeria_RepHtml") = Nothing
        Session.Remove("ReporteEnfermeria_RepHtml")

    End Sub

End Class
