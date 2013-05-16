
Partial Class Modulo_Actividades_frmSession
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Request.QueryString("codActividad") <> String.Empty Then

            Session("SS_CodigoProgramacionActividad") = CInt(Request.QueryString("codActividad"))
            Response.Redirect("/SaintGeorgeOnline/Modulo_Actividades/formatoRegistroActividades.aspx")
            '         function FormatoImpresion() {
            '    window.open('/SaintGeorgeOnline/Modulo_Actividades/formatoRegistroActividades.aspx', '_blank', 'menubar=0,resizable=0,width=500,height=200');
            '}
        
        End If
    End Sub
End Class
