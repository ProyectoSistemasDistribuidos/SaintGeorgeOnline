
Partial Class Plantillas_Exportaciones_Plantilla_Rep_PlanCurricular
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("ReportePlanCurricular_RepHtml"))

        Session("ReportePlanCurricular_RepHtml") = Nothing
        Session.Remove("ReportePlanCurricular_RepHtml")

    End Sub

End Class
