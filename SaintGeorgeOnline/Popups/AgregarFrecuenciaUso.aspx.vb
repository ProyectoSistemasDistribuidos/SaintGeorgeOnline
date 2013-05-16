Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessLogic.ModuloEnfermeria
Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient

Partial Class Popups_AgregarFrecuenciaUso
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then


        End If

    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgregar.Click

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancelar.Click

    End Sub
#Region "Eventos de los Combos"


#End Region

#End Region

#Region "Metodos"

#Region "Carga Combos"



#End Region


#End Region

#Region "Eventos del Gridview"



#End Region

End Class
