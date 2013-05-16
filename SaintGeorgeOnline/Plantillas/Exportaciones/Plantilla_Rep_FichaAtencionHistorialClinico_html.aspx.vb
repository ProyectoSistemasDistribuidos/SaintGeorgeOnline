
Partial Class Plantillas_Exportaciones_Plantilla_Rep_FichaAtencionHistorialClinico_html
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("Exportaciones_RepFichaAtencionHistorialClinicohtml"))

        Session("Exportaciones_RepFichaAtencionHistorialClinicohtml") = Nothing
        Session.Remove("Exportaciones_RepFichaAtencionHistorialClinicohtml")

    End Sub
End Class
