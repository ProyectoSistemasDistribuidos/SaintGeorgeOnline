Imports System.Web
Imports System

Partial Class LogOut
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LimpiarSesion()
    End Sub

#End Region
    
#Region "Metodos"

    ''' <summary>
    ''' Limpia los datos de la sesión del Usuario al Salir de la Web
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LimpiarSesion()
        Session.Clear()
        Session.RemoveAll()
        Session.Abandon()
        FormsAuthentication.SignOut()
        Me.Response.Redirect("/SaintGeorgeOnline/Acceso/Login.aspx")
    End Sub

#End Region
    

End Class
