Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic
Imports ClosedXML.Excel
Imports System.IO
Partial Class ModuloAdmision_frmReportesAdmision
    Inherits System.Web.UI.Page
#Region "Metodos"
    Private Sub cargarComboGrados()
        Dim obj_BL_Grados As New bl_Grados
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_GradosXCodigoNivelMinisterio(0, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlGrado, ds_Lista, "Codigo", "DescripcionEspaniol", True, False)
    End Sub
    Private Sub cargarComboAnioAcademico()
        Dim int_CodigoAnioTop As Integer = Me.Master.Obtener_CodigoPeriodoEscolar
        Dim obj_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
        Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
        Dim ds_Lista As DataSet = obj_AniosAcademicos.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
        Controles.llenarCombo(ddlPeriodo, ds_Lista, "Codigo", "Descripcion", False, True)
        Dim codAnioActual As Integer = 0
        codAnioActual = Master.Obtener_CodigoPeriodoEscolar()
        ddlPeriodo.SelectedValue = codAnioActual
    End Sub
   
#End Region
#Region "Eventos "

    Protected Sub ddlNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cargarComboGrados()

        Catch ex As Exception


        End Try
    End Sub

#End Region

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            CrearReporte()
        Catch ex As Exception

        End Try
    End Sub

#Region "Reportes funciones "
    Private Sub CrearReporte()

        Try

            Dim dsTeso As New DataSet
            Dim blTesoreris As New bl_admision2
            dsTeso = blTesoreris.FUN_reporteAlumnosBDTesoreria(IIf(ddlPeriodo.SelectedValue = "0", 0, ddlPeriodo.SelectedItem.Text), ddlGrado.SelectedValue)
            Dim dstSain As New DataSet
            Dim dstSaint As New bl_da_admision
            dstSain = dstSaint.FUN_reporteAlumnosBDSaint(ddlPeriodo.SelectedValue)
            Dim left As IEnumerable(Of left)


            left = dsTeso.Tables(0).AsEnumerable().Select(Function(fila) New left With {.idPostulante = CInt(fila("IdPostulante")), .nombre = fila("apepaterno") & " " & fila("apematerno") & " " & fila("nombres"), _
                                                                                        .grado = fila("descripcion")})



            Dim listaNombre As List(Of String)
            listaNombre = (left.Select(Function(obj) obj.grado)).Distinct().ToList

            Dim nombres = listaNombre.Aggregate(Function(curr, nxt) curr & " ," & nxt)


            Dim right As IEnumerable(Of right)
            right = dstSain.Tables(0).AsEnumerable().Select(Function(fila, index) New right With {.porcentaje = CInt(fila("EstadoPorcentaje")), .CodigoAlumno = CInt(fila("CodigoAlumno")), .idPostulante = CInt(fila("IdPostulante")), .cancelo = fila("Observacion")})

            '        EstadoPorcentaje()
            '0:


            Dim left1 = From s In left Group Join d In right On s.idPostulante Equals d.idPostulante Into det = Group _
                        From f In det.DefaultIfEmpty(New right With {.cancelo = "No pago", .idPostulante = 0, .CodigoAlumno = 0, .porcentaje = 0}) _
                                Select New With {.porc = f.porcentaje, .idPostulanteIz = s.idPostulante, .idPostulanteDer = f.idPostulante, .nombre = IIf(s Is Nothing, "", s.nombre), .grado = IIf(s Is Nothing, "", s.grado), .cancelo = IIf(f Is Nothing, "", f.cancelo), _
                                                 .detalle = (From dt In dstSain.Tables(1).AsEnumerable() Where dt("CodigoAlumno") = f.CodigoAlumno _
                                                             Select New detalle With {.fechaPago = dt("FechaPago").ToString, _
                                                                                      .fechaVen = dt("FechaVencimiento").ToString, _
                                                                                      .letra = dt("DescripcionLetra").ToString, _
                                                                                      .montoInicial = CDbl(dt("MontoInicial").ToString), _
                                                                                      .montoPagar = CDbl(dt("MontoPagar").ToString), _
                                                                                      .montoRestamte = CDbl(dt("MontoRestante").ToString)}).ToList}

          
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current


            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"

            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)
            Dim ws = workbook.Worksheet(1)
            '  
            Dim filas As Integer = 2
            With ws.Range(ws.Cell(filas, 1), ws.Cell(filas, 4))
                .Merge()
                .Value = "Reporte de Alumnos Admitidos  -  Cancelación de cuota de ingreso del año " & ddlPeriodo.SelectedItem.Text
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.FontSize = 12
                .Style.Font.Bold = True
            End With
            filas += 1
            With ws.Range(ws.Cell(filas, 1), ws.Cell(filas, 4))
                .Merge()
                .Value = "Fecha : " & Day(Date.Now) & "/" & Month(Date.Now) & "/" & Year(Date.Now)
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.FontSize = 10
                .Style.Font.Bold = True
            End With
            filas += 1
            With ws.Range(ws.Cell(filas, 1), ws.Cell(filas, 6))
                .Merge()
                .Value = "Grados : " & nombres
                .Style.Font.FontSize = 7
            End With
            ''
            filas += 1
            Dim indexFilasPintar As Integer = filas
            ws.Cell(filas, 1).Value = "Nro."
            ws.Cell(filas, 2).Value = "NOMBRE"
            ws.Cell(filas, 3).Value = "GRADO"
            ws.Cell(filas, 4).Value = "ESTADO"
            ws.Cell(filas, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            ''
            ws.Cell(filas, 5).Value = "Monto Inicial"
            ws.Cell(filas, 6).Value = "Monto Restante"
            ws.Cell(filas, 7).Value = "Letra"
            ws.Cell(filas, 8).Value = "Monto a pagar "
            ''
            ws.Cell(filas, 9).Value = "Fecha pago  "
            ws.Cell(filas, 10).Value = "Fecha Vencimiento  "
            ''
            ''
            filas += 1
            Dim indexFilas As Integer = 0
            Dim filasDet As Integer = 0
            Dim contFilasDet As Integer = 0
            Dim acFilasDet As Integer = 0
            Dim cont As Integer = 0
            Dim limInf As Integer = 0
            Dim linfSup As Integer = 0
            Dim count As Integer = 0

            For Each o In left1
                indexFilas += 1
                ''
                If o.detalle.Count = 0 Then
                    count = 1
                Else
                    count = o.detalle.Count
                End If
                If indexFilas = 1 Then
                    limInf = 6
                    'filas -= 1
                Else
                    limInf += count
                End If
                linfSup = limInf + count - 1
                If o.porc > 0 And o.detalle.Count > 0 Then
                    ''

                    'filas += o.detalle.Count
                    With ws.Range(ws.Cell(limInf, 1), ws.Cell(linfSup, 1))
                        .Merge()
                        .Value = indexFilas
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    End With

                    With ws.Range(ws.Cell(limInf, 2), ws.Cell(linfSup, 2))
                        .Merge()
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = o.nombre
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    End With
                    With ws.Range(ws.Cell(limInf, 3), ws.Cell(linfSup, 3))
                        .Merge()
                        .Value = o.grado

                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    End With
                    With ws.Range(ws.Cell(limInf, 4), ws.Cell(linfSup, 4))
                        .Merge()
                        .Value = o.cancelo
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#FCD5B4")
                        .Style.Alignment.WrapText = True
                    End With
                    With ws.Range(ws.Cell(limInf, 5), ws.Cell(linfSup, 5))
                        .Merge()
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = "S./" & o.detalle(0).montoInicial.ToString("N2")
                    End With
                    '
                    With ws.Range(ws.Cell(limInf, 6), ws.Cell(linfSup, 6))
                        .Merge()
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Value = "S./" & o.detalle(0).montoRestamte.ToString("N2")
                    End With
                    '



                    Dim SubIndex As Integer = -1
                    For ind As Integer = limInf To linfSup
                        SubIndex += 1
                        ws.Cell(ind, 7).Value = o.detalle(SubIndex).letra
                        ws.Cell(ind, 8).Value = "S./" & o.detalle(SubIndex).montoPagar.ToString("N2")

                        ws.Cell(ind, 9).Value = o.detalle(SubIndex).fechaPago

                        ws.Cell(ind, 10).Value = o.detalle(SubIndex).fechaVen
                    Next
                    filas += o.detalle.Count
                Else
                    filas += 1
                    ws.Cell(filas, 2).Value = o.nombre
                    ws.Cell(filas, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    ws.Cell(filas, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Cell(filas, 3).Value = o.grado
                    ws.Cell(filas, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    ws.Cell(filas, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                    ws.Cell(filas, 4).Value = o.cancelo
                    ws.Cell(filas, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    ws.Cell(filas, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center



                    ws.Cell(filas, 1).Value = indexFilas
                    ws.Cell(filas, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    ws.Cell(filas, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    ws.Cell(filas, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                    ws.Cell(filas, 4).Style.Alignment.WrapText = True
                    ws.Cell(filas, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    ws.Cell(filas, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center


                    If o.cancelo = "Canceló" Then
                        ws.Cell(filas, 4).Style.Fill.BackgroundColor = XLColor.FromHtml("#92D050")
                    End If



                End If
            Next
            Dim ultimasFilas As Integer = 0
            If filas < linfSup Then
                ultimasFilas = linfSup
            Else
                ultimasFilas = filas
            End If
            With ws.Range(indexFilasPintar, 1, ultimasFilas, 10)
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With
            With ws.Range(indexFilasPintar, 1, indexFilasPintar, 10)
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#F6F8AC")
                .Style.Font.Bold = True
            End With
            ws.Column(1).Width = 4
            ws.Column(2).Width = 43
            ws.Column(3).Width = 14
            ws.Column(4).Width = 22
            '
            ws.Column(5).Width = 12
            ws.Column(6).Width = 15

            ws.Column(7).Width = 7

            ws.Column(8).Width = 15

            ws.Column(9).Width = 11

            ws.Column(10).Width = 18


            ws.PageSetup.PagesWide = 0


            ws.PageSetup.AdjustTo(60)
            ws.PageSetup.PageOrientation = ClosedXML.Excel.XLPageOrientation.Landscape
            ws.PageSetup.Margins.Top = 0.5 '1.9
            ws.PageSetup.Margins.Bottom = 0.5 '1.9
            ws.PageSetup.Margins.Left = 0 '0.6
            ws.PageSetup.Margins.Right = 0 '0.6
            ws.PageSetup.Margins.Header = 0 '0.8s
            ws.PageSetup.Margins.Footer = 0 '0.8

            

         

            ws.Row(6).Hide()
            ''
            workbook.Save()

       
            ''
            Dim downloadBytes As Byte()
            downloadBytes = File.ReadAllBytes(rutaREpositorioTemporales)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "ReporteEntregaCargo.xlsx" + "; size=" + downloadBytes.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes)
            Response.End()
            Response.Close()
            Response.End()
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "clase"

    Public Class left
        Public idPostulante As Integer
        Public nombre As String
        Public grado As String
        Public cancelo As String
    End Class
    Public Class right
        Public cancelo As String
        Public idPostulante As Integer
        Public CodigoAlumno As Integer
        Public lstDetalle As IEnumerable(Of detalle)
        Public porcentaje As Integer
    End Class

    Public Class detalle
        Public montoInicial As Double
        Public montoRestamte As Double
        Public letra As String
        Public montoPagar As Double


        Public fechaPago As String
        Public fechaVen As String

    End Class
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cargarComboAnioAcademico()
            cargarComboGrados()
        End If
    End Sub
End Class
