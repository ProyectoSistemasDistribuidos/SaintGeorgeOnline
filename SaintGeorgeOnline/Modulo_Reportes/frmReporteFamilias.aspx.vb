Imports System.Linq
Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports ClosedXML.Excel
Imports System.IO

Partial Class Modulo_Reportes_frmReporteFamilias
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        reporteCertificados()

    End Sub



    Private Sub reporteCertificados()
        Try
            ''------------------------------------------------------
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim dc As New Dictionary(Of String, Object)
            Dim dtCertificado As New System.Data.DataTable
            Dim nParam As String = "USP_ReporteCertificadoPrimaria"
            dc("codAsignAula") = 42
            dc("codALumno") = 20050119
            dc("codGrado") = 8

            dtCertificado = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("certificadoPrimaria")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")

            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            Dim filas As Integer = 4
            Dim filasTemp As Integer = 0
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)




            ''------------------------------------------------------------------------------------------------------------''
            Dim sqlALumnos = From alumnos In dtCertificado.AsEnumerable() Group alumnos By _
                             codAlumno = alumnos("codAlumno"), _
                             nombreAlumno = alumnos("nombreAlumno") Into detalle = Group _
                             Select New With { _
                             .nombreAlumno = nombreAlumno, .listaGrados = (From notas In detalle.AsEnumerable() Group notas By codGrado = notas("codGrado") Into listaGrados = Group Select New With {.grado = codGrado}), _
                             .anios = (From notas In detalle.AsEnumerable() Group notas By anio = notas("anio"), codGrado = notas("codGrado") _
                                       Into grados = Group _
                                       Select New With { _
                                       .codGrado = codGrado, .anio = anio, _
                                       .notas = (From nots In grados.AsEnumerable() Order By nots("orden") Ascending Select New With { _
                                                .nota = nots("notaAnual"), _
                                                .orden = nots("orden")})})}



            ''------------------------------------------------------------------------------------------------------------''


            Dim listaGrado As New List(Of String)



            Dim contadorHojas As Integer = 1



            For Each osql In sqlALumnos
                'Dim ws = workbook.Worksheet(contadorHojas)

                Dim ws = workbook.Worksheets.Add("Hoja " & contadorHojas.ToString)



                Dim filasInicia As Integer = 17
                With ws.Range(ws.Cell(4, 4), ws.Cell(4, 10))
                    .Merge()
                    .Value = "      LIMA     METROPOLITANA"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Style.Font.FontSize = 19
                End With



                For columnas As Integer = 6 To 11
                    ws.Column(columnas).Width = 5.14
                Next
                ''-----
                With ws.Cell(15, 6)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 7)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 8)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 9)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 10)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                With ws.Cell(15, 11)
                    .Value = "--"
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With

                ''-----
                For filasDefault As Integer = 17 To 23
                    For columnas As Integer = 6 To 11
                        With ws.Cell(filasDefault, columnas)
                            .Value = "--"
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        End With
                    Next
                Next

                ''

                Dim listaGrados As String = ""
                Dim esPrimero As Integer = 0

                For Each oGra In osql.listaGrados
                    esPrimero += 1
                    If esPrimero = osql.listaGrados.Count Then
                        If osql.listaGrados.Count > 1 Then
                            listaGrados &= oGra.grado.ToString & "° y "
                        Else
                            If osql.listaGrados.Count = 1 Then
                                listaGrados &= oGra.grado.ToString & "° "
                            Else
                                listaGrados &= oGra.grado.ToString & "°, "
                            End If
                        End If
                    Else
                        If osql.listaGrados.Count = 1 Then
                            listaGrados &= oGra.grado.ToString & "° "
                        Else
                            listaGrados &= oGra.grado.ToString & "°, "
                        End If
                    End If
                Next


                With ws.Range(ws.Cell(12, 11), ws.Cell(12, 13))
                    .Merge()
                    .Value = listaGrados
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    '.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With



                ''
                With ws.Range(ws.Cell(12, 2), ws.Cell(12, 8))
                    .Merge()
                    .Value = osql.nombreAlumno
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    '.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                End With
                ''
                For Each oAnios In osql.anios

                    With ws.Cell(15, CInt(oAnios.codGrado) + 5)
                        .Value = oAnios.anio
                        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    End With

                    For Each oNtas In oAnios.notas
                        ws.Row(filasInicia).Height = 13
                        With ws.Cell(filasInicia, CInt(oAnios.codGrado) + 5)
                            .Value = oNtas.nota
                            .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        End With
                        filasInicia += 1

                    Next


                Next



                ''ancho de  columnas 
                With ws.Column(5)
                    .Width = 3.25
                End With
                With ws.Column(4)
                    .Width = 5.43
                End With

                '' configuracion de impresion 
                ''-------------------------------------------
                ws.Row(1).Height = 15
                ws.Row(2).Height = 12
                ws.Row(3).Height = 61.5

                ws.Row(4).Height = 29.25
                For fila As Integer = 5 To 10
                    ws.Row(fila).Height = 15
                Next
                ws.Row(11).Height = 26.25
                ws.Row(12).Height = 24
                ws.Row(13).Height = 15
                ws.Row(14).Height = 12.75
                ws.Row(15).Height = 25.5
                ws.Row(16).Height = 11.25
                ''-------------------------------------------
                ws.PageSetup.PaperSize = XLPaperSize.LetterPaper
                ws.PageSetup.PageOrientation = XLPageOrientation.Portrait
                ws.PageSetup.AdjustTo(100)
                ws.PageSetup.FitToPages(1, 1)
                ws.PageSetup.Margins.Top = 0 '1.9
                ws.PageSetup.Margins.Bottom = 0 '1.9
                ws.PageSetup.Margins.Left = 0 '0.6
                ws.PageSetup.Margins.Right = 0 '0.6
                ws.PageSetup.Margins.Header = 0 '0.8
                ws.PageSetup.Margins.Footer = 0 '0.8
                '''''''''''''''''''''''''''''''''''''''

                ws.PageSetup.PrintAreas.Add("A1:M45")

                contadorHojas += 1
            Next


            'ws.Column(13).Width = 3.29'
            'ALTO 





            ''-------------------------------------------




            workbook.Save()
            Dim downloadBytes1 As Byte()
            downloadBytes1 = File.ReadAllBytes(rutaREpositorioTemporales)

            ' Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteHouse.xlsx" + "; size=" + downloadBytes1.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes1)
            Response.End()


        Catch ex As Exception

        End Try
    End Sub


    Private Sub reporteFamilias()
        Try
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current



            Dim dc As New Dictionary(Of String, Object)


            Dim dtMatricula As New System.Data.DataTable
            Dim nParam As String = "REP_familiasAlumno"
            dtMatricula = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")



            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            Dim filas As Integer = 4
            Dim filasTemp As Integer = 0
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)


            Dim ws = workbook.Worksheet(1)
            ''-------------------------------------------------------

            Dim sqlObject = From alumno In dtMatricula.AsEnumerable() Group alumno By codAlumno = alumno("codAlumno"), _
                            nombreAlumno = alumno("nombreHijo"), _
                            fechaMatricula = alumno("fechaMatricula"), _
                            grado = alumno("grado") Into alumnos = Group _
                             Select New With { _
                           .nombreAlumno = nombreAlumno, _
                           .fechaMatricula = fechaMatricula, _
                           .grado = grado, _
                           .familiares = (From fam In alumnos.AsEnumerable() Order By fam("tipoParetezco") Ascending Select New With { _
                                         .nombreFamilia = fam("nombrePadre"), .codParentezco = fam("tipoParetezco"), _
                                         .parentezco = fam("parentezco"), _
                                         .cellOficina = fam("cellOficina").ToString(), _
                                         .telefonoOficina = fam("telefonoOficina"), _
                                         .telefonoCasa = fam("telefonoCasa"), _
                                         .celular = fam("celular")}).Take(1)} '.Where(Function(obj) obj.codParentezco = 1)}

            ''-------------------------------------------------------


            'cellOficina	telefonoOficina	telefonoCasa	celular
            '964338777	2054320	964338777	

            Dim filasInicial As Integer = 4

            Dim tempFilas As Integer = filasInicial
            With ws.Cell(filasInicial, 1)
                .Value = "Fecha de matrícula"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With

            With ws.Cell(filasInicial, 2)
                .Value = "NombreCompleto del Alumno"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With

            With ws.Cell(filasInicial, 3)
                .Value = "Grado"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With

            With ws.Cell(filasInicial, 4)
                .Value = "Nombre completo de la familia"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With

            With ws.Cell(filasInicial, 5)
                .Value = "Parentesco"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With

            With ws.Cell(filasInicial, 6)
                .Value = "Telefonos del contacto"
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End With
            filasInicial += 1
            Dim indicadorEsPrimeraFila As Integer = 0
            Dim limInf As Integer = 0
            Dim limSup As Integer = 0
            '
            '========================================================
            '

          
            '========================================================

            Dim sIndice As Integer = 0
            For Each oalumno In sqlObject
                indicadorEsPrimeraFila += 1
                If indicadorEsPrimeraFila = 1 Then
                    limInf = 5
                    limSup = limInf + oalumno.familiares.Count - 1
                Else
                    limInf = limSup + 1
                    limSup = limInf + oalumno.familiares.Count - 1
                End If
                With ws.Range(ws.Cell(limInf, 1), ws.Cell(limSup, 1))
                    .Merge()
                    .Value = oalumno.fechaMatricula
                    .Style.Font.FontSize = 8
                End With
                With ws.Range(ws.Cell(limInf, 2), ws.Cell(limSup, 2))
                    .Merge()
                    .Value = oalumno.nombreAlumno
                    .Style.Font.FontSize = 8

                End With
                With ws.Range(ws.Cell(limInf, 3), ws.Cell(limSup, 3))
                    .Value = oalumno.grado
                    .Style.Font.FontSize = 8
                    .Merge()
                End With
                sIndice = 0


                For indice As Integer = limInf To limSup

                    With ws.Cell(indice, 4)
                        .Value = oalumno.familiares(sIndice).nombreFamilia
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(indice, 5)
                        .Value = oalumno.familiares(sIndice).parentezco
                        .Style.Font.FontSize = 8
                    End With
                    With ws.Cell(indice, 6)
                        .Value = "Celular : " & oalumno.familiares(sIndice).celular & " /cel Ofic. :" & oalumno.familiares(sIndice).cellOficina & " / Telef Casa: " & oalumno.familiares(sIndice).telefonoCasa _
                        & " / Telef Oficina :" & oalumno.familiares(sIndice).telefonoOficina
                        .Style.Font.FontSize = 8
                    End With

                    sIndice += 1
                Next





            Next


            ws.PageSetup.PagesWide = 1


            ws.Column(1).Width = 16
            ws.Column(2).Width = 25
            ws.Column(3).Width = 11
            ws.Column(4).Width = 25

            ws.Column(5).Width = 11

            ws.Column(6).Width = 33

            With ws.Range(ws.Cell(tempFilas, 1), ws.Cell(limSup, 6))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With




            workbook.Save()
            Dim downloadBytes1 As Byte()
            downloadBytes1 = File.ReadAllBytes(rutaREpositorioTemporales)

            ' Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteHouse.xlsx" + "; size=" + downloadBytes1.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes1)
            Response.End()


        Catch ex As Exception

        End Try
    End Sub
End Class
