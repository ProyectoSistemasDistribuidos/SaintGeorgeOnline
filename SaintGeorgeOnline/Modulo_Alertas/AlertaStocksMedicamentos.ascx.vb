Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.DataVisualization.Charting

Public Class Modulo_Alertas_AlertaStocksMedicamentos
    Inherits System.Web.UI.UserControl

#Region "Data Access"
    Private cn As New SqlConnection("data source=192.168.1.18;initial catalog=BD_SanGeorgeOnline; uid=integracion; pwd=sanjorge123")

    Private ReadOnly Property getConexion() As SqlConnection
        Get
            Return cn
        End Get
    End Property

    Private Function listaStockChart() As DataTable
        Dim da As New SqlDataAdapter("AL_USP_LIS_KardexChart", getConexion)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable("ListaStock")
        da.Fill(dt)
        Return dt
    End Function

    Private Function listaStockTotal() As DataTable
        Dim da As New SqlDataAdapter("AL_USP_LIS_Kardex", getConexion)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable("ListaStock")
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
            divMedicamento.Style.Add("Top", value.ToString & "px")
        End Set
    End Property

    Public WriteOnly Property CoorLeft() As Integer
        Set(ByVal value As Integer)
            divMedicamento.Style.Add("Left", value.ToString & "px")
        End Set
    End Property

    Public WriteOnly Property Index() As Integer
        Set(ByVal value As Integer)
            divMedicamento.Style.Add("z-index", (100 + value).ToString)
        End Set
    End Property

    Public WriteOnly Property Display() As Boolean
        Set(ByVal value As Boolean)
            If value = False Then
                divMedicamento.Style.Add("display", "none")
            End If
        End Set
    End Property

#End Region

#Region "Metodos Web"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        listarAlertasStockChart()
    End Sub

    Private Sub listarAlertasStockChart()

        Chart1.DataSource = listaStockChart()

        Chart1.Series("Series1").XValueMember = "DescMedicamento"
        Chart1.Series("Series1").YValueMembers = "Stock"
        Chart1.Series("Series2").YValueMembers = "StockMinimo"

        Chart1.Series("Series1").ChartType = SeriesChartType.Bar
        Chart1.Series("Series2").ChartType = SeriesChartType.Bar

        Chart1.Series("Series1").IsValueShownAsLabel = True
        Chart1.Series("Series2").IsValueShownAsLabel = True

        'Chart1.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        'Chart1.ChartAreas("ChartArea1").Area3DStyle.Inclination = 20
        'Chart1.ChartAreas("ChartArea1").Area3DStyle.Rotation = 30

        Chart1.Series("Series1")("PointWidth") = 0.8
        Chart1.Series("Series2")("PointWidth") = 0.8

        'Chart1.Series("Series1")("DrawingStyle") = "LightToDark"
        'Chart1.Series("Series2")("DrawingStyle") = "LightToDark"

        Chart1.ChartAreas("ChartArea1").AxisX.IsMarginVisible = True

        'Chart1.Legends("Default").Enabled = True
        Chart1.DataBind()

    End Sub

    Protected Sub btnCerraListaAlertaStock_Click()
        pnModalListaAlertaStock.Hide()
    End Sub

    Protected Sub btnVerMas_Click()
        miGridviewStockTotal.DataSource = listaStockTotal()
        miGridviewStockTotal.DataBind()
        pnModalListaAlertaStock.Show()
    End Sub

    Protected Sub miGridviewStockTotal_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onMouseOver", "this.bgColor='#d3eefa'")
            e.Row.Attributes.Add("onMouseOut", "this.bgColor=''")

        End If

    End Sub

#End Region

End Class

