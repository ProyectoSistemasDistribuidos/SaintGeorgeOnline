
Partial Class Plantillas_Exportaciones_Plantilla_Rep_MidTermReport_html
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("Exportaciones_RepMidTermReportHtml"))

        Session("Exportaciones_RepMidTermReportHtml") = Nothing
        Session.Remove("Exportaciones_RepMidTermReportHtml")
    End Sub
End Class
