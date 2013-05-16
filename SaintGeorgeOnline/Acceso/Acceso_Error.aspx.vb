
''' <summary>
''' Modulo de Error de Acceso
''' </summary>
''' <remarks>
''' Eventos:
''' -------
''' 1. Page_Load
''' 
''' Metodos: 
''' -------
''' 1. LimpiarSesion
''' </remarks>
Partial Class Acceso_Acceso_Error
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
    End Sub

#End Region

End Class
