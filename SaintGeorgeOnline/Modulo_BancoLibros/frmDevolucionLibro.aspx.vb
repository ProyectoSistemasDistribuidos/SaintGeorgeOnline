Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloBancoLibros
Imports SaintGeorgeOnline_Utilities
Imports System.Web.Script.Services
Imports System.Web.Services

Partial Class Modulo_BancoLibros_frmDevolucionLibro
    Inherits System.Web.UI.Page

    Private cod_Modulo As Integer = 1
    Private cod_Opcion As Integer = 1
    Public Shared dtAnio As DataTable

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Devolución de Libros")

            If Not Page.IsPostBack Then
                cargarComboAniosAcademico()
            End If

        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarComboAniosAcademico()
        Try
            dtAnio = New DataTable
            Dim obj_BL_Grados As New bl_AniosAcademicos
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
            dtAnio = ds_Lista.Tables(0)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Web Methods"


    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function buscarPrestamo(ByVal codAnio As Integer, ByVal codBarra As String) As Object

        Dim dtBusqueda As DataTable
        Try
            dtBusqueda = New DataTable
            Dim nParam As String = "BL_USP_GET_PrestamoLibro"
            Dim dc As New Dictionary(Of String, Object)
            dc("codigoanio") = codAnio
            dc("codigobarra") = codBarra.Trim()
            dtBusqueda = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim xmlTabla As New XElement("table", New XAttribute("id", "lstPrestamo"), _
                                                  New XAttribute("cellpadding", "0"), _
                                                  New XAttribute("cellspacing", "0"), _
                                                  New XAttribute("style", "width: 1000px;"), _
                                                  New XAttribute("border", "0"), _
                            New XElement("tr", _
                            New XElement("th", New XAttribute("style", "display: none; width: 0")), _
                            New XElement("th", New XAttribute("style", "display: none; width: 0")), _
                            New XElement("th", New XAttribute("style", "width: 70px;"), "Código"), _
                            New XElement("th", New XAttribute("style", "width:410px;"), "Nombres y Apellidos"), _
                            New XElement("th", New XAttribute("style", "width: 90px;"), "Cod.Barra"), _
                            New XElement("th", New XAttribute("style", "width:350px;"), "Título"), _
                            New XElement("th", New XAttribute("style", "width:80px;"), "Fec. Pres.")) _
            )

            xmlTabla.Add((From dt In dtBusqueda.AsEnumerable() _
                            Select New XElement("tr", New XAttribute("onmouseover", "Over(this)"), _
                                                      New XAttribute("onmouseout", "Out(this)"), _
                                New XElement("td", New XAttribute("style", "display: none; width: 0"), dt("cPre")), _
                                New XElement("td", New XAttribute("style", "display: none; width: 0"), dt("cDet")), _
                                New XElement("td", New XAttribute("style", "width: 70px; text-align:center;"), dt("cAlumno")), _
                                New XElement("td", New XAttribute("style", "width:410px; text-align:left;"), New XElement("span", New XAttribute("style", "padding-left:10px;")), dt("nAlumno")), _
                                New XElement("td", New XAttribute("style", "width: 90px; text-align:center;"), dt("CodigoBarra")), _
                                New XElement("td", New XAttribute("style", "width:350px; text-align:left;"), New XElement("span", New XAttribute("style", "padding-left:10px;"), dt("titulo"))), _
                                New XElement("td", New XAttribute("style", "width:80px; text-align:center; border-right: solid 1px #a6a3a3;"), dt("fecPrestamo")) _
                            ) _
                        ))

            Return New With {.html = xmlTabla.ToString()}

        Catch ex As Exception

        End Try
    End Function


    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function devolverLibro(ByVal codDetalle As Integer) As Object
        Dim list As New List(Of String)
        Try
            Dim obl_Devoluciones As New bl_Devoluciones
            list = obl_Devoluciones.FUN_UPD_DevolverLibro(codDetalle, 1, 1, 1, 1)
            Return list
        Catch ex As Exception
            list.Add("-1")
            list.Add("Ocurrió un error, intente su operación otra vez.")
            Return list
        End Try
        'If list.Item(0).ToString = "" Then
        'End If
    End Function


#End Region


End Class
