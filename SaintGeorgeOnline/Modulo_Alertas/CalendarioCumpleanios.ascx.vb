Imports System.Data
Imports System.Data.SqlClient

Partial Class Modulo_Alertas_CalendarioCumpleanios
    Inherits System.Web.UI.UserControl

#Region "Data Access"
    Private cn As New SqlConnection("data source=192.168.1.18;initial catalog=BD_SanGeorgeOnline; uid=integracion; pwd=sanjorge123")

    Private ReadOnly Property getConexion() As SqlConnection
        Get
            Return cn
        End Get
    End Property

    Private Function listaCumpleaniosDelDia() As DataTable
        Dim da As New SqlDataAdapter("AL_USP_LIS_CumpleaniosDelDia", getConexion)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable("ListaBirthday")
        da.Fill(dt)
        Return dt
    End Function

    Private Function listaCumpleaniosPorCodigoTrabajador(ByVal codigo As Integer) As DataTable
        Dim da As New SqlDataAdapter("AL_USP_GET_CumpleaniosTrabajador", getConexion)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        da.SelectCommand.Parameters.Add("@p_CodigoTrabajador", SqlDbType.Int).Value = codigo
        Dim dt As New DataTable("ListaBirthday")
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
            divCumpleanios.Style.Add("Top", value.ToString & "px")
        End Set
    End Property

    Public WriteOnly Property CoorLeft() As Integer
        Set(ByVal value As Integer)
            divCumpleanios.Style.Add("Left", value.ToString & "px")
        End Set
    End Property

    Public WriteOnly Property Index() As Integer
        Set(ByVal value As Integer)
            divCumpleanios.Style.Add("z-index", (100 + value).ToString)
        End Set
    End Property

    Public WriteOnly Property Display() As Boolean
        Set(ByVal value As Boolean)
            If value = False Then
                divCumpleanios.Style.Add("display", "none")
            End If
        End Set
    End Property

#End Region

#Region "Metodos Web"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            listarCumpleaniosDelDia()
        End If
    End Sub

    Private Sub listarCumpleaniosDelDia()
        miRepeater.DataSource = listaCumpleaniosDelDia()
        miRepeater.DataBind()
    End Sub

    Protected Sub btnCerraListaFamiliares_Click()
        pnModalListaFamiliares.Hide()
    End Sub

    Private Sub mostrarDetalleNota(ByVal codigo As String)

        Dim dt As DataTable = listaCumpleaniosPorCodigoTrabajador(codigo)

        imgFoto.ImageUrl = "/SaintGeorgeOnline/Fotos/" & dt.Rows(0).Item("RutaFoto")
        lblModalNombre.Text = dt.Rows(0).Item("NombreCompleto")
        lblModalBirthday.Text = dt.Rows(0).Item("Birthday")
        lblModalCorreo.Text = dt.Rows(0).Item("Correo")
        lblModalSkype.Text = dt.Rows(0).Item("Skype")

        pnModalListaFamiliares.Show()

    End Sub

    Protected Sub miRepeater_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)

        If e.CommandName = "VerMas" Then
            Dim codigo As Integer = CInt(e.CommandArgument.ToString)
            mostrarDetalleNota(codigo)
        End If

    End Sub

#End Region

End Class
