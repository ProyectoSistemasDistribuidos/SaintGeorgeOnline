Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports DocumentFormat.OpenXml.Wordprocessing

Partial Class Modulo_Actividades_frmRegistroRequerimiento
    Inherits System.Web.UI.Page
#Region "variables globlaes "
    Public lstMeses As Dictionary(Of Integer, String)
    Public lstYear As Dictionary(Of Integer, String)
    Public dtEstado As Data.DataTable
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cargarComboYear()
            cargarComboMeses()
            cargarComboEstados()
        End If
    End Sub

#Region "Cargar  combos filtros apra la busqueda "

    Private Sub cargarComboYear()
        Try
            lstYear = New Dictionary(Of Integer, String)

            For anio As Integer = Date.Now.Year - 2 To Date.Now.Year + 2
                lstYear(anio) = anio.ToString
            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub cargarComboMeses()
        Try
            lstMeses = New Dictionary(Of Integer, String)
            lstMeses(1) = "Enero"
            lstMeses(2) = "Febrero"
            lstMeses(3) = "Marzo"
            lstMeses(4) = "Abril"
            lstMeses(5) = "Mayo"
            lstMeses(6) = "Junio"
            lstMeses(7) = "Julio"
            lstMeses(8) = "Agosto"
            lstMeses(9) = "Setiembre"
            lstMeses(10) = "Octubre"
            lstMeses(11) = "Nomviembre"
            lstMeses(12) = "Diciembre"


        Catch ex As Exception

        End Try
    End Sub

    Private Sub cargarComboEstados()
        Try
            dtEstado = New Data.DataTable
            Dim dc As New Dictionary(Of String, Object)
            Dim nParam As String = "USSP_lisEstadosActividad"

            dtEstado = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Listar actividades"
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_listarActividadesValidacion(ByVal anio As Integer, ByVal mes As Integer, ByVal estado As Integer) As Object
        Dim dtActividad As New Data.DataTable
        Try
            Dim dc As New Dictionary(Of String, Object)
            Dim nParam As String = "USP_lisActividadesRequerimiento"
            dc("year") = anio
            dc("mes") = mes
            dc("codEstado") = estado
            dc("codTipoApr") = 2

            dtActividad = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim xml As New XElement("table", New XAttribute("id", "tablaBusqueda"), New XAttribute("cellpadding", "0"), New XAttribute("cellspacing", "0"), New XAttribute("border", "0"), _
 _
                   dtActividad.AsEnumerable().Select(Function(fila, index) _
                            New XElement("tr", New XAttribute("onmouseover", "TiposControlesActualOver(this)"), _
                                                New XAttribute("onmouseout", "TiposControlesActualOut(this)"), New XElement("td", _
                            New XElement("div", _
                            New XAttribute("style", "height:25px;width:245px; float:left; line-height:25px; text-align:center"), fila("nombreActividad"))), _
                            New XElement("td", _
                            New XElement("div", _
                            New XAttribute("style", "height:25px;width: 70px; float:left; line-height:25px; text-align:center"), fila("fechaInicio"))), _
                            New XElement("td", _
                            New XElement("div", _
                            New XAttribute("style", "height:25px;width: 25px; float:left; line-height:25px; text-align:center"), "")), _
                            New XElement("td", _
                            New XElement("div", _
                            New XAttribute("style", "height:25px;width: 25px; float:left; line-height:25px; text-align:center"), "")), _
                              New XElement("td", _
                            New XElement("div", _
                            New XAttribute("style", "height:25px;width: 25px; float:left; line-height:25px; text-align:center"), "")), _
                            New XElement("td", New XElement("div", New XAttribute("style", " height:25px;width:25px;font-size:8pt;text-align:center"), _
                                          New XElement("img", New XAttribute("style", "cursor:pointer;height:18px ;width:18px;"), _
                                                              New XAttribute("title", "Edit"), _
                                                              New XAttribute("onclick", "fMostrarEdicion(" & fila("codActividad").ToString() & " )"), _
                                                              New XAttribute("src", "../App_Themes/Imagenes/opc_actualizar.png")))))))


            Return New With {.html = xml.ToString}
            'codActividad	codAprobaciones	codTioApr	nombreTipoAprobador	estadoActividad	fechaInicio	nombreActividad
            '8	1	1	Jefatura de Nivel	Approved	17/04/2013	prueba 04

        Catch ex As Exception

        End Try
    End Function



#End Region

#Region "actualizar la actividad "

    <WebMethod(EnableSession:=True)> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
   Public Shared Function F_actualizarActividad(ByVal dcActividad As Dictionary(Of String, Object)) As Object
        Try
            Dim dcMensajes As New Dictionary(Of String, Object)
            dcMensajes = New BL_Actividad().F_actualizarReq(dcActividad)
        Catch ex As Exception

        End Try


    End Function
#End Region

#Region "Listar actividad "

    <WebMethod(EnableSession:=True)> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
   Public Shared Function F_listarACtividad(ByVal codActividad As Integer) As Object
        Try
            Dim dtActividad As New Data.DataTable
            Dim dc As New Dictionary(Of String, Object)
            Dim nParam As String = "USP_lisDetalleActividad"
            dc("p_CodigoProgramacionActividad") = codActividad

            'ReqTec	ReqLog	ReqInf	comentarios



            dtActividad = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
            Dim sqlObject = From act In dtActividad.AsEnumerable() Group act By _
                            ReqTec = act("ReqTec"), _
                            nombreActividad = act("actividad"), _
                            ReqLog = act("ReqLog"), _
                            objetivo = act("objtetivo"), _
                            ReqInf = act("ReqInf"), _
                            comentarios = act("comentarios") _
                            Into detalle = Group _
                            Select New With { _
                                    .nombreActividad = nombreActividad, _
                                    .ReqTec = ReqTec, _
                                    .ReqLog = ReqLog, _
                                    .ReqInf = ReqInf, _
                                    .comentarios = comentarios, _
                                    .objetivo = objetivo, _
                                             .detalle = (From dt In detalle.AsEnumerable() _
                                                        Select New actividad With {.nombreActividad = dt("nombreGrado")}). _
                                                        Aggregate(Function(prev, curr) _
                                                                      New actividad With {.nombreActividad = prev.nombreActividad & "," & curr.nombreActividad}).nombreActividad.ToUpper}


            Return sqlObject.First
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "clases"

    Public Class actividad
        Public nombreActividad As String
    End Class
#End Region
End Class
