﻿
Partial Class Plantillas_Exportaciones_Plantilla_Rep_FichaMedica_html
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Response.Write(Session("Exportaciones_RepFichaMedicaHtml"))

        Session("Exportaciones_RepFichaMedicaHtml") = Nothing
        Session.Remove("Exportaciones_RepFichaMedicaHtml")

    End Sub

End Class
