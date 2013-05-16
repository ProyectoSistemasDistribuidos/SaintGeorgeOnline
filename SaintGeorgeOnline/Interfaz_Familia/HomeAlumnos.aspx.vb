Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

Partial Class Interfaz_Familia_HomeAlumnos
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Home")
            If Not Page.IsPostBack Then
                'SetearAccionesAcceso()
                'CargarFormulario()
            End If
        Catch ex As Exception
            'EnvioEmailError(0, ex.ToString)
        End Try
    End Sub

    Protected Sub dgv_Familia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

    End Sub

    Protected Sub dgv_DatosHijos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

    End Sub

#End Region

End Class
