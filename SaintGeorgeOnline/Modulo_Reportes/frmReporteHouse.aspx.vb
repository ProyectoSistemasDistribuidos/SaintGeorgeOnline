Imports ClosedXML.Excel
Imports System.Data
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports System.IO
Imports System.Linq
Imports Microsoft.Office.Interop.Excel

Partial Class Modulo_Reportes_frmReporteHouse
    Inherits System.Web.UI.Page




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'F_reporteHouse(0, 7)
        F_reporteEnfermeria("", "")

    End Sub

    Public Sub F_reporteHouse(ByVal pcodGrado As Integer, ByVal pcodAnio As Integer)
        ''----------Configuracion requerida---------------------
        ''<!--plantilla reporte House-->
        ''<add key="reporteHouse" value="\Plantillas\ExportacionLibreta\reporteHouse.xlsx"/>
        ''-----------------------------

        Try
            Dim dc As New Dictionary(Of String, Object)
            dc("codGrado") = pcodGrado
            dc("codAnio") = pcodAnio
            Dim dtHouse As New System.Data.DataTable
            Dim nParam As String = "USP_RepHouse"
            Dim indicadorEsPrimeraFila As Integer = 0
            Dim limInf As Integer = 0
            Dim limSup As Integer = 0
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
            ''
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")


            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            Dim filas As Integer = 4
            Dim filasTemp As Integer = 0
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)

            dtHouse = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim sqlObject = From grados In dtHouse.AsEnumerable() _
                            Group grados By codGrado = grados("codGrado"), _
                            nombreGrado = grados("nombreGrado") Into detalle = Group _
                              Select New With { _
                            .cantidad = detalle.Count, _
                            .nombreGrado = nombreGrado, _
                            .house = (From hs In detalle.AsEnumerable() _
                                      Group hs By _
                                   nombreHouse = hs("nombreHouse") Into houses = Group _
            Where nombreHouse <> "" Select New With {.nombreHouse = nombreHouse, .detalle = (From cantidad In houses.AsEnumerable() Select New With _
                                        { _
                             .cantidad = cantidad("cantidad"), _
                             .pos = cantidad("pos")})})}


            Dim ws = workbook.Worksheet(1)

            With ws.Range(ws.Cell(1, 2), ws.Cell(1, 7))
                .Merge()
                .Value = "Reporte de cantidad de alumnos por houses"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With
            With ws.Range(ws.Cell(2, 2), ws.Cell(2, 7))
                .Merge()
                .Value = "Fecha del reporte: " & Date.Now.Date.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With

            With ws.Range(ws.Cell(4, 1), ws.Cell(4, 4))
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#A7B7C8")
            End With


            With ws.Cell(4, 1)
                .Value = "Grado"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With

            With ws.Cell(4, 2)
                .Value = "Houses"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With

            With ws.Cell(4, 3)
                .Value = "Hombres"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With

            With ws.Cell(4, 4)
                .Value = "Mujeres"
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Font.Bold = True
            End With



            For Each oGrados In sqlObject
                indicadorEsPrimeraFila += 1

                If indicadorEsPrimeraFila = 1 Then
                    limInf = 5
                    limSup = limInf + IIf(oGrados.house.Count <= 1, 3, oGrados.house.Count - 1)
                Else
                    limInf = limSup + 1
                    limSup = limInf + IIf(oGrados.house.Count <= 1, 3, oGrados.house.Count - 1)
                End If


                With ws.Range(ws.Cell(limInf, 1), ws.Cell(limSup, 1))
                    .Merge()
                    .Value = oGrados.nombreGrado
                    .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Style.Border.RightBorder = XLBorderStyleValues.Thin
                    .Style.Border.TopBorder = XLBorderStyleValues.Thin
                    .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                    .Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                End With

                ''---------------------
                If oGrados.house.Count <= 1 Then
                    filas += 1
                    ws.Cell(filas, 2).Value = "Blue"
                    ws.Cell(filas, 3).Value = "0"
                    ws.Cell(filas, 4).Value = "0"
                    filas += 1
                    ws.Cell(filas, 2).Value = "Green"
                    ws.Cell(filas, 3).Value = "0"
                    ws.Cell(filas, 4).Value = "0"
                    filas += 1
                    ws.Cell(filas, 2).Value = "Red"
                    ws.Cell(filas, 3).Value = "0"
                    ws.Cell(filas, 4).Value = "0"
                    filas += 1
                    ws.Cell(filas, 2).Value = "Yellow"
                    ws.Cell(filas, 3).Value = "0"
                    ws.Cell(filas, 4).Value = "0"
                Else
                    ''---
                    For Each house In oGrados.house
                        filas += 1
                        ws.Cell(filas, 2).Value = house.nombreHouse
                        ''---
                        For Each oalumno In house.detalle
                            With (ws.Cell(filas, CInt(oalumno.pos)))
                                .Value = oalumno.cantidad
                            End With
                        Next
                        ''---
                    Next
                    ''---
                End If
                ''------------------
            Next




            With ws.Range(ws.Cell(4, 1), ws.Cell(limSup, 4))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin
            End With

            ws.PageSetup.AdjustTo(60)
            ws.PageSetup.Margins.Bottom = 0.75 '1.9
            ws.PageSetup.Margins.Left = 0.7 '0.6
            ws.PageSetup.Margins.Right = 0.7 '0.6
            ws.PageSetup.Margins.Header = 0.3 '0.8
            ws.PageSetup.Margins.Footer = 0.3 '0.8

            ws.PageSetup.PagesWide = 1

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

        Finally

        End Try
    End Sub


    Public Sub F_reporteEnfermeria(ByVal fechaInicio As String, ByVal fechaFin As String)
        Try
            Dim currentContext As System.Web.HttpContext = System.Web.HttpContext.Current
            '@fechaInicio datetime='',
            '@fechaFin datetime ='',
            '@codSede int =0,
            '@codTipo int =0,
            '@codNivel int =0,
            '@codSubNivel int =0,
            '@codGrado int =0,
            '@codAula int =0,
            '@codPersona int =0 


            Dim dc As New Dictionary(Of String, Object)
            dc("fechaInicio") = ""
            dc("fechaFin") = ""
            dc("codSede") = 0
            dc("codTipo") = 0
            dc("codNivel") = 0
            dc("codSubNivel") = 0
            dc("codGrado") = 0
            dc("codAula") = 0
            dc("codPersona") = 0

            Dim dtEnfermeria As New System.Data.DataTable
            Dim nParam As String = "USP_LisConsolidadoEnfermeria"
            dtEnfermeria = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            ''--------------------------------------------------------------------------------''
            'agrupar alumnos por grado  aula 


            Dim grupoGradoAulaAlumnos = _
            From grados In dtEnfermeria.AsEnumerable() Group grados By _
                                        codGrado = grados("codGrado"), nombreGrado = grados("nombreGrado") Into grados = Group _
                                Select New With { _
                                    .nombreGrado = nombreGrado, _
                                    .aulas = (From aulas In grados.AsEnumerable() Group aulas By codAula = aulas("codAula"), _
                                              nombreAula = aulas("nombreAula") Into GrpAulas = Group _
                                                      Select New With { _
                                                            .nombreAula = nombreAula, _
                                                            .nombreGradoPa = nombreGrado, _
                                                            .Alumnos = (From alumnos In GrpAulas.AsEnumerable() Group alumnos By codALumno = alumnos("codAlumno"), _
                                                                        nombreAlumno = alumnos("nombreALumno") Into grpAlumnos = Group _
                                                                                Select New With {.nombreAlumnos = nombreAlumno, .codAlmuno = codALumno, _
                                                                                         .cantidad = grpAlumnos.Count})})}

            
            Dim grupoCategoria = From cat In dtEnfermeria.AsEnumerable() Group cat By categoria = cat("categoria") Into categorias = Group _
                                 Select New With {.categoria = categoria, .cantidad = (categorias.Count)}


            Dim grupoTipoAtencion = From tip In dtEnfermeria.AsEnumerable() Group tip By tipo = tip("tipoAtencion") Into Detalle = Group _
                                    Select New With {.tipo = tipo, .cantidad = Detalle.Count}
            Dim grupoClase = From curso In dtEnfermeria.AsEnumerable() Group curso By nombreCurso = curso("nombreCurso") Into detalle = Group _
                            Select New With {.curso = nombreCurso, .cantidad = detalle.Count}


            Dim grupoDiagnostico = From diag In dtEnfermeria.AsEnumerable() Group diag By nombreDiag = diag("nombreDiag") Into detalle = Group _
                                   Select New With {.nombreDiagnostico = nombreDiag, .cantidad = detalle.Count}




            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
            Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim oCells As Microsoft.Office.Interop.Excel.Range
            Dim sFile As String, sTemplate As String
            Dim nombreRep As String
            Dim fila As String = ""
            ''--------------------------------------------------
            Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("reporteCargoEntrega")
            Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            oBooks = oExcel.Workbooks
            oBooks.Open(rutaREpositorioTemporales) 'Load colorful template with graph
            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets
            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "prueba"
            oSheet.Activate()
            oExcel.Visible = False
            oExcel.DisplayAlerts = False


         






            ''----------------------------------------------------------------------------------------------------
            '' pintado de alunmos  por grado  y aula  desde la fila  14 
            '' incializacion  de  variables
            Dim filaInicial As Integer = 3 ''fila inicial 
            Dim totalAtencionesPorAula As Integer = 0
            ''----------------------------------------------------------------------------------------------------


            '' pintado de la cabezera 

            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 6), Range))
                .Merge(True)
                .Value = "Consolidado de Cantidades"
                .Font.Bold = True
                .Font.Size = 16
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            End With
            filaInicial += 1

            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 6), Range))
                .Merge(True)
                .Value = "Fecha del reporte: " _
                & Date.Now.Day.ToString.PadLeft(2, "0") _
                & "/" & Date.Now.Month.ToString.PadLeft(2, "0") _
                & Date.Now.Year.ToString & " " _
                & Date.Now.Hour.ToString.PadLeft(2, "0") & ":" _
                & Date.Now.Minute.ToString.PadLeft(2, "0") & ":" _
                & Date.Now.Second.ToString.PadLeft(2, "0") & " "
                .Font.Bold = True
                .Font.Size = 9
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            End With
            filaInicial += 2


            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Range))
                .Merge(True)
                .Value = "Rango de Fecha de atención de : " & fechaInicio & " al " & fechaFin
                .Font.Size = 9
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            End With
            filaInicial += 1
            filaInicial += 1
            Dim filaTemp As Integer = filaInicial
          
            filaInicial += 4

            
            '
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 4), Range))
                .Merge(True)
                .Value = "NUMERO DE ATENCIONES POR ALUMNO"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            End With


            ''---------------------------------------------------------------''
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicial, 6), Range), CType(oExcel.ActiveSheet.Cells(filaInicial, 7), Range))
                .Merge(True)
                .Value = "CATEGORIA"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                .Font.Bold = True
            End With
            ''---------------------------------------------------------------'

            filaInicial += 2
            Dim filasCount As Integer = filaInicial ''pocicion de filas para los demas cuadros  

            With CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Range)
                .Value = "Codigo"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            '-------------------------------------------------------
            With CType(oExcel.ActiveSheet.Cells(filaInicial, 2), Range)
                .Value = "Grado/Aula"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            '-------------------------------------------------------
            With CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Range)
                .Value = "ALUMNOS"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            '-------------------------------------------------------

            With CType(oExcel.ActiveSheet.Cells(filaInicial, 4), Range)
                .Value = "ATENCIONES"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With

            '.Interior.Color() = RGB(149, 179, 215)
            '-------------------------------------------------------
            filaInicial += 1


            ''-----------------------------------------------------------------------------'
            '' pintar el cuadro de categoria                                               '
            ''-----------------------------------------------------------------------------'
            Dim filaInicioCategoria As Integer = filasCount
            With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range)
                .Value = "CATEGORIA"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range)
                .Value = "CANTIDAD"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With


            filasCount += 1
            For Each categorias In grupoCategoria
                With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range)
                    .NumberFormat = "@"
                    .Value = categorias.categoria
                    .Font.Size = 9
                End With
                With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range)
                    .NumberFormat = "@"
                    .Value = categorias.cantidad
                    .Font.Size = 9
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With
                filasCount += 1
            Next
            '' poner borde al cuadro de categoria 
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicioCategoria, 6), Range), CType(oExcel.ActiveSheet.Cells(filasCount - 1, 7), Range))
                .Borders.LineStyle = XlLineStyle.xlContinuous
            End With



            filasCount += 2


            ''-----------------------------------------------------------------------------'

            ''-----------------------------------------------------------------------------'
            '' pintar el cuadro de tipo de atencion                                        '
            ''-----------------------------------------------------------------------------'
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range), CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range))
                .Merge(True)
                .Value = "TIPO DE ATENCION"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                .Font.Bold = True
            End With

            filasCount += 2

            filaInicioCategoria = filasCount
            With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range)
                .Value = "ATENCION"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range)
                .Value = "CANTIDAD"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            filasCount += 1

            For Each ogrupoTipoAtencion In grupoTipoAtencion
                With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range)
                    .NumberFormat = "@"
                    .Value = ogrupoTipoAtencion.tipo
                    .Font.Size = 9
                End With
                With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range)
                    .NumberFormat = "@"
                    .Value = ogrupoTipoAtencion.cantidad
                    .Font.Size = 9
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With
                filasCount += 1
            Next
            '' poner borde del cuadro de tipo de atencion 
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicioCategoria, 6), Range), CType(oExcel.ActiveSheet.Cells(filasCount - 1, 7), Range))
                .Borders.LineStyle = XlLineStyle.xlContinuous
            End With


            filasCount += 2
            ''-----------------------------------------------------------------------------'




            ''-----------------------------------------------------------------------------'
            '' pintar el cuadro de clases                                      '
            ''-----------------------------------------------------------------------------'
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range), CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range))
                .Merge(True)
                .Value = "CLASES"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                .Font.Bold = True
            End With

            filasCount += 2

            filaInicioCategoria = filasCount
            With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range)
                .Value = "CLASES"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range)
                .Value = "CANTIDAD"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            filasCount += 1

            For Each ogrupoClase In grupoClase
                With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range)
                    .NumberFormat = "@"
                    .Value = ogrupoClase.curso
                    .Font.Size = 9
                End With
                With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range)
                    .NumberFormat = "@"
                    .Value = ogrupoClase.cantidad
                    .Font.Size = 9
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With
                filasCount += 1
            Next
            '' poner borde del cuadro de clases de atencion 
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicioCategoria, 6), Range), CType(oExcel.ActiveSheet.Cells(filasCount - 1, 7), Range))
                .Borders.LineStyle = XlLineStyle.xlContinuous
            End With



            filasCount += 2
            ''-----------------------------------------------------------------------------'
            ''-----------------------------------------------------------------------------'
            '' pintar el cuadro de clases                                      '
            ''-----------------------------------------------------------------------------'
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range), CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range))
                .Merge(True)
                .Value = "MOTIVO DE ATENCION"
                .Font.Size = 16
                .Interior.Color() = RGB(255, 0, 0)
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                .Font.Bold = True
            End With

            filasCount += 2
            filaInicioCategoria = filasCount
            With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range)
                .Value = "DIAGNOSTICO"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range)
                .Value = "CANTIDAD"
                .Font.Size = 11
                .Interior.Color() = RGB(196, 215, 155)
                .Font.Bold = True
            End With
            filasCount += 1

            For Each ogrupoDiagnostico In grupoDiagnostico
                With CType(oExcel.ActiveSheet.Cells(filasCount, 6), Range)
                    .NumberFormat = "@"
                    .Value = ogrupoDiagnostico.nombreDiagnostico
                    .Font.Size = 9
                End With
                With CType(oExcel.ActiveSheet.Cells(filasCount, 7), Range)
                    .NumberFormat = "@"
                    .Value = ogrupoDiagnostico.cantidad
                    .Font.Size = 9
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With
                filasCount += 1
            Next
            '' poner borde del cuadro de clases de atencion 
            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicioCategoria, 6), Range), CType(oExcel.ActiveSheet.Cells(filasCount - 1, 7), Range))
                .Borders.LineStyle = XlLineStyle.xlContinuous
            End With



            filasCount += 2
            '
            ''-----------------------------------------------------------------------------'


            Dim filaInicio As Integer = filaInicial - 1
            For Each ogrado In grupoGradoAulaAlumnos

                For Each aulas In ogrado.aulas

                    For Each oalumno In aulas.Alumnos


                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Range)
                            .NumberFormat = "@"
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 1), Range)
                            .Value = oalumno.codAlmuno.ToString
                            .Font.Size = 9
                        End With


                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 2), Range)
                            .NumberFormat = "@"
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 2), Range)
                            .Value = ogrado.nombreGrado & "/" & aulas.nombreAula.ToString
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Range)
                            .NumberFormat = "@"
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 3), Range)
                            .Value = oalumno.nombreAlumnos.ToString.Trim
                            .Font.Size = 9
                        End With
                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 4), Range)
                            .NumberFormat = "@"
                        End With

                        With CType(oExcel.ActiveSheet.Cells(filaInicial, 4), Range)
                            .Value = oalumno.cantidad.ToString
                            .Font.Size = 9
                            .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        End With

                        totalAtencionesPorAula += CInt(oalumno.cantidad)
                        filaInicial += 1
                    Next

                Next

            Next




            'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
            '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            '    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            'End With
            'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
            '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            '    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            'End With
            'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
            '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            '    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            'End With
            'With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
            '    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
            '    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
            'End With

            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaInicio, 1), Range), CType(oExcel.ActiveSheet.Cells(filaInicial - 1, 4), Range))
                .Borders.LineStyle = XlLineStyle.xlContinuous
            End With




            With oExcel.Application.Range(CType(oExcel.ActiveSheet.Cells(filaTemp, 1), Range), CType(oExcel.ActiveSheet.Cells(filaTemp, 3), Range))
                .Merge(True)
                .Value = "TOTAL DE ATENCIONES               " & totalAtencionesPorAula.ToString
                .Font.Size = 12
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            End With



            oExcel.ActiveSheet.Cells.Columns(1).ColumnWidth = 8
            oExcel.ActiveSheet.Cells.Columns(2).ColumnWidth = 22
            oExcel.ActiveSheet.Cells.Columns(3).ColumnWidth = 33
            oExcel.ActiveSheet.Cells.Columns(4).ColumnWidth = 11


            oExcel.ActiveSheet.Cells.Columns(6).ColumnWidth = 31


            oSheet.SaveAs(rutaREpositorioTemporales)

            oBook.Close()

            'Quit Excel and thoroughly deallocate everything


            EiminaReferencias(oSheet)
            EiminaReferencias(oBook)
            oExcel.Quit()
            EiminaReferencias(oExcel)
            System.GC.Collect()

            ''-------------------------
            ''
            Dim downloadBytes1 As Byte()
            downloadBytes1 = File.ReadAllBytes(rutaREpositorioTemporales)

            ' Response.AddHeader("content-disposition", "attachment;filename=" & NombreArchivo)
            Response.Charset = ""
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "reporteHouse.xlsx" + "; size=" + downloadBytes1.Length.ToString())
            Response.Flush()
            Response.BinaryWrite(downloadBytes1)
            Response.End()

            ''


        Catch ex As Exception

        End Try
    End Sub

    Shared Sub EiminaReferencias(ByRef Referencias As Object)
        Try
            'Bucle de eliminacion
            Do Until _
                      System.Runtime.InteropServices.Marshal.ReleaseComObject(Referencias) <= 0
            Loop
        Catch
        Finally
            Referencias = Nothing
        End Try
    End Sub
#Region "Funciones "

    Public Function crearDiccionarios(ByVal rwFila As DataRow, ByVal index As Integer) As Dictionary(Of Object, Object)
        Dim dcFilas As New Dictionary(Of Object, Object)
        Try

            dcFilas("") = rwFila("")
            dcFilas("") = rwFila("")
            dcFilas("") = rwFila("")
            dcFilas("") = rwFila("")


            Return dcFilas("") = rwFila("")


        Catch ex As Exception

        End Try

    End Function

#End Region
End Class
