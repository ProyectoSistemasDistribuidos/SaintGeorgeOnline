
Imports SaintGeorgeOnline_DataAccess

Partial Class ModuloMigracion_frmMigracion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Dim oDA_ALL_DataBase As New DA_ALL_DataBase

        Dim cod As Integer = 0
        cod = oDA_ALL_DataBase.FInsertarEntrevista()

    End Sub
End Class
