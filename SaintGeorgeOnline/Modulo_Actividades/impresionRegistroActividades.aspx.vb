
Partial Class Modulo_Actividades_impresionRegistroActividades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.Response.Write(Session("SS_ImpresionFichaEntrevista"))
        If Not Session("SS_ImpresionRegistroActividades") Is Nothing Then
            miDiv.InnerHtml = Session("SS_ImpresionRegistroActividades").ToString
            Session("SS_ImpresionRegistroActividades") = Nothing
            Session.Remove("SS_ImpresionRegistroActividades")
        End If
    End Sub

End Class
