﻿Imports System.Data
Imports System.Data.SqlClient

Partial Class Modulo_Alertas_AlertaSolicitudesActualizacionPendientesFichaFamiliar
    Inherits System.Web.UI.UserControl

#Region "Data Access"
    Private cn As New SqlConnection("data source=192.168.1.18;initial catalog=BD_SanGeorgeOnline; uid=integracion; pwd=sanjorge123")

    Private ReadOnly Property getConexion() As SqlConnection
        Get
            Return cn
        End Get
    End Property

    Private Function listaAlertaSolicitudesActualizacionFichaFamiliar() As DataTable
        Dim da As New SqlDataAdapter("AL_USP_LIS_SolicitudesActualizacionFichaFamiliar", getConexion)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable("ListaFicha")
        da.Fill(dt)
        Return dt
    End Function

#End Region

#Region "Propiedades"

    Private strTitulo As String
    Private intCoorTop As Integer
    Private intCoorLeft As Integer
    Private intIndex As Integer
    Private boolDisplay As Boolean

    Public WriteOnly Property Titulo() As String
        Set(ByVal value As String)
            lbltitulo.Text = value
        End Set
    End Property

    Public WriteOnly Property CoorTop() As Integer
        Set(ByVal value As Integer)
            divSolicitudesActualizacionPendientesFichaFamiliar.Style.Add("Top", value.ToString & "px")
        End Set
    End Property

    Public WriteOnly Property CoorLeft() As Integer
        Set(ByVal value As Integer)
            divSolicitudesActualizacionPendientesFichaFamiliar.Style.Add("Left", value.ToString & "px")
        End Set
    End Property

    Public WriteOnly Property Index() As Integer
        Set(ByVal value As Integer)
            divSolicitudesActualizacionPendientesFichaFamiliar.Style.Add("z-index", (100 + value).ToString)
        End Set
    End Property

    Public WriteOnly Property Display() As Boolean
        Set(ByVal value As Boolean)
            If value = False Then
                divSolicitudesActualizacionPendientesFichaFamiliar.Style.Add("display", "none")
            End If
        End Set
    End Property

#End Region

#Region "Metodos Web"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AlertaSolicitudesActualizacionFichaFamiliar()
    End Sub

    Private Sub AlertaSolicitudesActualizacionFichaFamiliar()

        Dim dt_Lista As DataTable = listaAlertaSolicitudesActualizacionFichaFamiliar()
        lblCantidadFichas.Text = dt_Lista.Rows.Count

    End Sub

    Protected Sub btnCerraListaAlertaSolicitudesActualizacionPendientesFichaFamiliar_Click()
        pnModalListaAlertaSolicitudesActualizacionPendientesFichaFamiliar.Hide()
    End Sub

    Protected Sub btnVerMas_Click()
        miGridviewSolicitudesActualizacionPendientesFichaFamiliar.DataSource = listaAlertaSolicitudesActualizacionFichaFamiliar()
        miGridviewSolicitudesActualizacionPendientesFichaFamiliar.DataBind()
        pnModalListaAlertaSolicitudesActualizacionPendientesFichaFamiliar.Show()
    End Sub

    Protected Sub miGridviewSolicitudesActualizacionPendientesFichaFamiliar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

End Class
