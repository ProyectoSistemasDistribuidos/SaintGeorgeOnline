Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Runtime.InteropServices.Marshal
Imports System.Threading
Imports System.Web
Imports System.Web.HttpContext
Imports System.Configuration
Imports System.IO
Imports System.Diagnostics
Imports System.Web.HttpServerUtility
Imports System.Web.UI.Page
Imports ClosedXML
Imports ClosedXML.Excel

Public Class Exportacion_pres

#Region "Atributos"

    Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    ''' <summary>
    ''' Obtiene una cadena de caracteres aleatorio (de tipo numero) que será el nombre del documento.
    ''' </summary>
    ''' <returns>Retorna descripcion de nombre de documento a generar</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function GetNewName() As String
        Dim sName As String = Convert.ToString(DateTime.Now.Ticks)
        Return sName
    End Function

#End Region


#Region "Exportar : Excel"

#Region "Variables de Posiciones en Excel"

    Private Shared int_HA_Left As Integer = 2 ' Alineación Horizontal Izquierda
    Private Shared int_HA_Center As Integer = 3 ' Alineación Horizontal Centrada
    Private Shared int_HA_Right As Integer = 4 ' Alineación Horizontal Derecha

    Private Shared int_VA_Top As Integer = 1 ' Alineación Vertical Superior
    Private Shared int_VA_Middle As Integer = 2 ' Alineación Vertical Media
    Private Shared int_VA_Bottom As Integer = 3 ' Alineación Vertical Inferior

#End Region

#Region "Metodos General"

    ''' <summary>
    ''' Insertar los bordes de las celdas según el rango indicado.
    ''' </summary>
    ''' <param name="mexcel">Instancia de documento excel</param>
    ''' <param name="objRango">Rando de celdas a insertar sus bordes</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub cuadradoCompleto(ByVal mexcel As Microsoft.Office.Interop.Excel.Application, _
                         ByVal objRango As Microsoft.Office.Interop.Excel.Range)
        Try


            objRango.Select()
            With mexcel
                '.Range(Rango).Select()
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
                With .Selection.Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                End With
            End With
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Obtiene la letra del abecedario segun su posicion en el alfabeto
    ''' </summary>
    ''' <param name="idColumna">Posicion de columna</param>
    ''' <returns>Letra que hace referencia a la columan en EXCEL</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function DevLetraColumna(ByVal idColumna As Integer) As String
        Dim letra As String = ""

        If idColumna = 1 Then
            letra = "A"
        ElseIf idColumna = 2 Then
            letra = "B"
        ElseIf idColumna = 3 Then
            letra = "C"
        ElseIf idColumna = 4 Then
            letra = "D"
        ElseIf idColumna = 5 Then
            letra = "E"
        ElseIf idColumna = 6 Then
            letra = "F"
        ElseIf idColumna = 7 Then
            letra = "G"
        ElseIf idColumna = 8 Then
            letra = "H"
        ElseIf idColumna = 9 Then
            letra = "I"
        ElseIf idColumna = 10 Then
            letra = "J"
        ElseIf idColumna = 11 Then
            letra = "K"
        ElseIf idColumna = 12 Then
            letra = "L"
        ElseIf idColumna = 13 Then
            letra = "M"
        ElseIf idColumna = 14 Then
            letra = "N"
        ElseIf idColumna = 15 Then
            letra = "O"
        ElseIf idColumna = 16 Then
            letra = "P"
        ElseIf idColumna = 17 Then
            letra = "Q"
        ElseIf idColumna = 18 Then
            letra = "R"
        ElseIf idColumna = 19 Then
            letra = "S"
        ElseIf idColumna = 20 Then
            letra = "T"
        ElseIf idColumna = 21 Then
            letra = "U"
        ElseIf idColumna = 22 Then
            letra = "V"
        ElseIf idColumna = 23 Then
            letra = "W"
        ElseIf idColumna = 24 Then
            letra = "X"
        ElseIf idColumna = 25 Then
            letra = "Y"
        ElseIf idColumna = 26 Then
            letra = "Z"
        End If

        Return letra
    End Function

    ''' <summary>
    ''' Obtiene el id de la letra
    ''' </summary>
    ''' <param name="strLetra">Letra a consultar ID</param>
    ''' <returns>Letra que hace referencia a la columan en EXCEL</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     12/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function DevIDColumna(ByVal strLetra As String) As Integer
        Dim idx As Integer = 0

        If strLetra = "A" Then
            idx = 1
        ElseIf strLetra = "B" Then
            idx = 2
        ElseIf strLetra = "C" Then
            idx = 3
        ElseIf strLetra = "D" Then
            idx = 4
        ElseIf strLetra = "E" Then
            idx = 5
        ElseIf strLetra = "F" Then
            idx = 6
        ElseIf strLetra = "G" Then
            idx = 7
        ElseIf strLetra = "H" Then
            idx = 8
        ElseIf strLetra = "I" Then
            idx = 9
        ElseIf strLetra = "J" Then
            idx = 10
        ElseIf strLetra = "K" Then
            idx = 11
        ElseIf strLetra = "L" Then
            idx = 12
        ElseIf strLetra = "M" Then
            idx = 13
        ElseIf strLetra = "N" Then
            idx = 14
        ElseIf strLetra = "O" Then
            idx = 15
        ElseIf strLetra = "P" Then
            idx = 16
        ElseIf strLetra = "Q" Then
            idx = 17
        ElseIf strLetra = "R" Then
            idx = 18
        ElseIf strLetra = "S" Then
            idx = 19
        ElseIf strLetra = "T" Then
            idx = 20
        ElseIf strLetra = "U" Then
            idx = 21
        ElseIf strLetra = "V" Then
            idx = 22
        ElseIf strLetra = "W" Then
            idx = 23
        ElseIf strLetra = "X" Then
            idx = 24
        ElseIf strLetra = "Y" Then
            idx = 25
        ElseIf strLetra = "Z" Then
            idx = 26
        End If

        Return idx
    End Function

    'Reporte Prestamos: Años Utlisados
    Public Shared Function existePeriodoLibro(ByVal dv As DataView, ByVal str_Periodo As String) As Boolean

        Dim bool_Existe As Boolean = False
        For Each dvr As DataRowView In dv
            If dvr("AC_Descripcion") = str_Periodo Then
                bool_Existe = True
                Exit For
            End If
        Next
        Return bool_Existe

    End Function

#End Region

    '/////////////////////
    'Reportes por SolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases/
    '/////////////////////

#Region "Modulo de SolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases"

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     10/11/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarSolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases(ByVal dtReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Try




            'Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            'Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
            'Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
            'Dim oCells As Microsoft.Office.Interop.Excel.Range
            Dim sFile As String, sTemplate As String
            Dim nombreRep As String

            nombreRep = GetNewName()

            sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xlsx"
            sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteSolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases").ToString()

            ' oExcel.Visible = False : oExcel.DisplayAlerts = False

            ''Start a new workbook 
            'oBooks = oExcel.Workbooks
            'oBooks.Open(sTemplate) 'Load colorful template with graph
            'oBook = oBooks.Item(1)
            'oSheets = oBook.Worksheets
            'oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            'oSheet.Name = str_NombreEntidadReporte
            'oCells = oSheet.Cells
            'oSheet.Activate()
            Dim rutaTemporal As String = ""
            LlenarPlantillaSolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases(dtReporte, str_NombreEntidadReporte, sTemplate, rutaTemporal)

            'oSheet.SaveAs(sFile)
            'oBook.Close()

            'Quit Excel and thoroughly deallocate everything
            'oExcel.Quit()
            'ReleaseComObject(oCells)
            'ReleaseComObject(oSheet)
            'ReleaseComObject(oSheets)
            'ReleaseComObject(oBook)
            'ReleaseComObject(oBooks)
            'ReleaseComObject(oExcel)
            'oExcel = Nothing
            'oBooks = Nothing
            'oBook = Nothing
            'oSheets = Nothing
            'oSheet = Nothing
            'oCells = Nothing
            'System.GC.Collect()
            Return rutaTemporal
        Catch ex As Exception

        End Try



    End Function


    'Reporte Devoluciones: Consolidado de Devoluciones

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     04/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>

    Public Shared Sub LlenarPlantillaSolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases(ByVal dtReporte As System.Data.DataSet, _
                                ByVal str_NombreEntidadReporte As String, ByVal strNombreArchivo As String, ByRef rutaTempDest As String)

        'Dim fila As Integer = 7
        'Dim columna As Integer = 4
        'Dim cont_columnas As Integer = 0
        'Dim cont_filas As Integer = 0
        'Dim int_contGrado As Integer = 0

        'columna = 8

        'Pintado de Titulo

        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("plantillaReporteConsolidadoSecundariaTipoCurso")
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"
        File.Copy(strNombreArchivo, rutaREpositorioTemporales)


        Try
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)

            ' workbook.Worksheets.Add("holaundo")

            Dim ws = workbook.Worksheet(1)




            'ws.Range(ws.Cell(2, limInf), ws.Cell(2, limSup))
            '.Merge()
            ''  ws.Cell(2, 3).Value = "Casting";


            'ws.Range(ws.Cell(2, 1).Address, ws.Cell(2, 2).Address);

            ws.Range(ws.Cell(3, 5), ws.Cell(3, 10)).Merge()
            '  oExcel.Range(oCells(3, 5), ws.Cell(3, 10)).HorizontalAlignment = 3

            ws.Range(ws.Cell(3, 5), ws.Cell(3, 10)).Value = "PRESUPUESTO - AÑO ACADÉMICO " & dtReporte.Tables(0).Rows(0).Item("Anio") & " - " & dtReporte.Tables(0).Rows(0).Item("Sede")

            ' oExcel.Range(oCells(3, 5), oCells(3, 10)).Font.Bold = True

            'Pintado de Fecha 
            ws.Range(ws.Cell(4, 5), ws.Cell(4, 10)).Merge()
            'oExcel.Range(oCells(4, 5), oCells(4, 10)).HorizontalAlignment = 3
            ws.Range(ws.Cell(4, 5), ws.Cell(4, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute & " " & Now.ToString("tt").ToLower()
            '  oExcel.Range(oCells(4, 5), oCells(4, 10)).Font.Bold = True

            'Pintado de Centros de Costos,SubCentro de costos 
            'oExcel.Range(oCells(6, 2), oCells(6, 5)).Merge()
            ws.Range(ws.Cell(6, 1), ws.Cell(6, 1)).Value = "Centro de Costos:"
            ws.Range(ws.Cell(7, 1), ws.Cell(7, 1)).Value = "Sub Centro de Costos:" & dtReporte.Tables(0).Rows(0).Item("SubCentroCostos")
            ws.Range(ws.Cell(8, 1), ws.Cell(8, 1)).Value = "Sub Sub Centro de Costos:" & dtReporte.Tables(0).Rows(0).Item("SubSubCentroCostos")
            ws.Range(ws.Cell(9, 1), ws.Cell(9, 1)).Value = "Sub Sub Sub Centro de Costos:" & dtReporte.Tables(0).Rows(0).Item("SubSubSubCentroCostos")
            ''oExcel.Range(oCells(6, 1), oCells(9, 1)).Font.Bold = True
            'oExcel.Range(oCells(6, 2), oCells(6, 3)).Interior.Color() = RGB(153, 153, 204)
            'oExcel.Range(oCells(7, 2), oCells(8, 3)).Interior.Color() = RGB(255, 250, 240)

            ws.Range(ws.Cell(6, 2), ws.Cell(6, 3)).Merge()
            ws.Range(ws.Cell(7, 2), ws.Cell(7, 3)).Merge()
            ws.Range(ws.Cell(8, 2), ws.Cell(8, 3)).Merge()
            ws.Range(ws.Cell(9, 2), ws.Cell(9, 3)).Merge()
            'oExcel.Range(oCells(6, 1), oCells(9, 3)).HorizontalAlignment = 3

            ws.Range(ws.Cell(6, 2), ws.Cell(6, 3)).Value = dtReporte.Tables(0).Rows(0).Item("CentroCostos")
            ws.Range(ws.Cell(7, 2), ws.Cell(7, 3)).Value = dtReporte.Tables(0).Rows(0).Item("SubCentroCostos")
            ws.Range(ws.Cell(8, 2), ws.Cell(8, 3)).Value = dtReporte.Tables(0).Rows(0).Item("SubSubCentroCostos")
            ws.Range(ws.Cell(9, 2), ws.Cell(9, 3)).Value = dtReporte.Tables(0).Rows(0).Item("SubSubSubCentroCostos")

            'Tipo de Cambio
            ws.Cell(6, 4).Value = "Tipo de Cambio:"


            ws.Cell(7, 4).Value = "S/." & dtReporte.Tables(0).Rows(0).Item("TipoCambio")

            With ws.Cell(6, 4)
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FF0000")
            End With
            With ws.Cell(7, 4)
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
            End With

            ''oExcel.Cells(6, 4).Interior.Color() = RGB(255, 0, 0)
            ''oExcel.Cells(7, 4).Interior.Color() = RGB(255, 255, 204)


            'Pintado del cuadrado de la leyenda


            '' cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(6, 4), oExcel.Cells(7, 4)))


            With ws.Range(ws.Cell(6, 4), ws.Cell(7, 4))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin

            End With


            ''Pintado del cuadrado de la leyenda
            'cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(6, 2), oExcel.Cells(8, 3)))

            ''Pintado del cuadrado de Cabecera estática 
            'cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(10, 2), oExcel.Cells(11, 8)))
            'Dim int_cantidadLibros As Integer = 0

            'int_cantidadLibros = dtLibro.Rows.Count

            ' Detalle Dinámica
            Dim FilaCab As Integer = 11
            Dim FilaDet As Integer = 11
            Dim ColumnaCab As Integer = 1
            Dim cont As Integer = 0
            Dim dt_Clases As DataTable
            Dim dt_Detalle As DataTable
            dt_Clases = dtReporte.Tables(0)
            dt_Detalle = dtReporte.Tables(1)
            Dim contDv As Integer = 0
            Dim int_cantidadTotal As Integer = 0
            Dim dec_CantPrecioTot As Decimal = 0
            Dim int_CantEne As Integer = 0
            Dim int_CantFeb As Integer = 0
            Dim int_CantMar As Integer = 0
            Dim int_CantAbr As Integer = 0
            Dim int_CantMay As Integer = 0
            Dim int_CantJun As Integer = 0
            Dim int_CantJul As Integer = 0
            Dim int_CantAgo As Integer = 0
            Dim int_CantSep As Integer = 0
            Dim int_CantOct As Integer = 0
            Dim int_CantNov As Integer = 0
            Dim int_CantDic As Integer = 0
            Dim int_CantTot As Integer = 0

            Dim dec_CantTotalPrecioTot As Decimal = 0
            Dim int_CantTotalEne As Integer = 0
            Dim int_CantTotalFeb As Integer = 0
            Dim int_CantTotalMar As Integer = 0
            Dim int_CantTotalAbr As Integer = 0
            Dim int_CantTotalMay As Integer = 0
            Dim int_CantTotalJun As Integer = 0
            Dim int_CantTotalJul As Integer = 0
            Dim int_CantTotalAgo As Integer = 0
            Dim int_CantTotalSep As Integer = 0
            Dim int_CantTotalOct As Integer = 0
            Dim int_CantTotalNov As Integer = 0
            Dim int_CantTotalDic As Integer = 0
            Dim int_CantTotalTot As Integer = 0

            While cont <= dt_Clases.Rows.Count - 1
                '' oExcel.Cells(FilaCab, ColumnaCab).Interior.Color() = RGB(0, 204, 255) ' ObtenerColor(cont) 'RGB(153, 153, 204)
                'oExcel.Range(oExcel.Cells(FilaCab, ColumnaCab), oExcel.Cells(FilaCab, ColumnaCab + 1)).Merge()
                'oExcel.Range(oExcel.Cells(FilaCab, ColumnaCab), oExcel.Cells(FilaCab, ColumnaCab)).HorizontalAlignment = 3

                ws.Cell(FilaCab, ColumnaCab).Value = "Clase :"

                With ws.Cell(FilaCab, ColumnaCab)
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                End With

                ws.Cell(FilaCab, ColumnaCab + 1).Value = dt_Clases.Rows(cont).Item("ClasePresupuestal").ToString

                Dim int_CodigoSSSCentroCostoClase As Integer = 0
                Dim int_CodigoSSSCentroCostoClaseDetalle As Integer = 0
                Dim int_CodigoEstructuraArticulo As Integer = 0
                Dim int_contColDetalle As Integer = 0
                Dim int_contFilaDetalle As Integer = 0

                int_CodigoSSSCentroCostoClase = dt_Clases.Rows(cont).Item("CodigoSSSCentroCostoClase").ToString
                ''oExcel.Range(oCells(FilaCab + 2, ColumnaCab), oCells(FilaCab + 2, ColumnaCab + 20)).Interior.Color() = RGB(0, 204, 255) ' ObtenerColor(cont)
                Dim sumFilas As Integer = FilaCab + 2
                ws.Cell(sumFilas, 1).Value = "Categoria :"
                ws.Cell(sumFilas, 2).Value = "Sub Categoria :"
                ws.Cell(sumFilas, 3).Value = "Artículo :"
                ws.Cell(sumFilas, 4).Value = "Observación :"

                '' tipo de distribucion-tipo validacion
                '  ws.Cell(sumFilas, 5).Value = "Tipo validacion :"
                '    ws.Cell(sumFilas, 5).Value = "Tipo distribucion :"


                ws.Cell(sumFilas, 5).Value = "Cantidad :"
                ws.Cell(sumFilas, 6).Value = "Unidad de Medida :"
                ws.Cell(sumFilas, 7).Value = "Precio :"
                ws.Cell(sumFilas, 8).Value = "Precio Total :"
                 

                ws.Cell(sumFilas, 9).Value = "Tipo Distribucion :"

                ws.Cell(sumFilas, 10).Value = "Ene :"
                ws.Cell(sumFilas, 11).Value = "Feb :"
                ws.Cell(sumFilas, 12).Value = "Mar :"
                ws.Cell(sumFilas, 13).Value = "Abr :"
                ws.Cell(sumFilas, 14).Value = "May :"
                ws.Cell(sumFilas, 15).Value = "Jun :"
                ws.Cell(sumFilas, 16).Value = "Jul :"
                ws.Cell(sumFilas, 17).Value = "Ago :"
                ws.Cell(sumFilas, 18).Value = "Sep :"
                ws.Cell(sumFilas, 19).Value = "Oct :"
                ws.Cell(sumFilas, 20).Value = "Nov :"
                ws.Cell(sumFilas, 21).Value = "Dic :"
                ws.Cell(sumFilas, 22).Value = "TOTAL :"


                With ws.Range(ws.Cell(sumFilas, 1), ws.Cell(sumFilas, 22))
                    .Style.Fill.BackgroundColor = XLColor.FromHtml("#00AEEF")
                End With
                '.EntireColumn.AutoFit()

                FilaDet = FilaCab + 3

                Dim dv As DataView = dt_Detalle.DefaultView

                dv.RowFilter = "1=1 and CodigoSSSCentroCostoClase =" & int_CodigoSSSCentroCostoClase.ToString

                If dv.Count > 0 Then 'Existe Detalle 
                    While contDv <= dv.Count - 1
                        If int_CodigoSSSCentroCostoClase = dv.Item(contDv).Item("CodigoSSSCentroCostoClase") Then

                            ws.Cell(FilaDet, 1).Value = (dv.Item(contDv).Item("Categoria").ToString).Trim
                            ws.Cell(FilaDet, 2).Value = (dv.Item(contDv).Item("SubCategoria").ToString).Trim
                            ws.Cell(FilaDet, 3).Value = (dv.Item(contDv).Item("Item").ToString).Trim




                            ws.Cell(FilaDet, 4).Value = (dv.Item(contDv).Item("Observacion").ToString).Trim


                            ws.Cell(FilaDet, 4).Style.Alignment.WrapText = True
                            ' Format(cantidad, “##,##0.00″)


                            ws.Cell(FilaDet, 5).Value = (dv.Item(contDv).Item("Cantidad").ToString).Trim
                            ws.Cell(FilaDet, 6).Value = (dv.Item(contDv).Item("Unidad").ToString).Trim
                            ws.Cell(FilaDet, 7).Value = "S/." & CStr(Format(CDec(dv.Item(contDv).Item("Precio").ToString), "##,##0.00"))
                            ws.Cell(FilaDet, 8).Value = "S/." & CStr(Format(CDec(dv.Item(contDv).Item("Precio").ToString) * CDec(dv.Item(contDv).Item("Cantidad").ToString), "##,##0.00")) 'dt_Detalle.Rows(contDv).Item("Item").ToString
                            dec_CantPrecioTot = CDec(dec_CantPrecioTot) + (CDec(dv.Item(contDv).Item("Precio").ToString) * CDec(dv.Item(contDv).Item("Cantidad").ToString))

                            ''DSPA_TipoDistribucion
                            ''CANTIDAD()
                            ws.Cell(FilaDet, 9).Value = dv.Item(contDv).Item("DSPA_TipoDistribucion").ToString

                            ws.Cell(FilaDet, 10).Value = dv.Item(contDv).Item("CantENE").ToString

                            int_CantEne = int_CantEne + dv.Item(contDv).Item("CantENE").ToString
                            ws.Cell(FilaDet, 11).Value = dv.Item(contDv).Item("CantFEB").ToString
                            int_CantFeb = int_CantFeb + dv.Item(contDv).Item("CantFEB").ToString
                            ws.Cell(FilaDet, 12).Value = dv.Item(contDv).Item("CantMAR").ToString
                            int_CantMar = int_CantMar + dv.Item(contDv).Item("CantMAR").ToString
                            ws.Cell(FilaDet, 13).Value = dv.Item(contDv).Item("CantABR").ToString
                            int_CantAbr = int_CantAbr + dv.Item(contDv).Item("CantABR").ToString
                            ws.Cell(FilaDet, 14).Value = dv.Item(contDv).Item("CantMAY").ToString
                            int_CantMay = int_CantMay + dv.Item(contDv).Item("CantMAY").ToString
                            ws.Cell(FilaDet, 15).Value = dv.Item(contDv).Item("CantJUN").ToString
                            int_CantJun = int_CantJun + dv.Item(contDv).Item("CantJUN").ToString
                            ws.Cell(FilaDet, 16).Value = dv.Item(contDv).Item("CantJUL").ToString
                            int_CantJul = int_CantJul + dv.Item(contDv).Item("CantJUL").ToString
                            ws.Cell(FilaDet, 17).Value = dv.Item(contDv).Item("CantAGO").ToString
                            int_CantAgo = int_CantAgo + dv.Item(contDv).Item("CantAGO").ToString
                            ws.Cell(FilaDet, 18).Value = dv.Item(contDv).Item("CantSET").ToString
                            int_CantSep = int_CantSep + dv.Item(contDv).Item("CantSET").ToString
                            ws.Cell(FilaDet, 19).Value = dv.Item(contDv).Item("CantOCT").ToString
                            int_CantOct = int_CantOct + dv.Item(contDv).Item("CantOCT").ToString
                            ws.Cell(FilaDet, 20).Value = dv.Item(contDv).Item("CantNOV").ToString
                            int_CantNov = int_CantNov + dv.Item(contDv).Item("CantNOV").ToString
                            ws.Cell(FilaDet, 21).Value = dv.Item(contDv).Item("CantDIC").ToString
                            int_CantDic = int_CantDic + dv.Item(contDv).Item("CantDIC").ToString

                            int_cantidadTotal = CDec(dv.Item(contDv).Item("CantENE").ToString) + CDec(dv.Item(contDv).Item("CantFEB").ToString) + CDec(dv.Item(contDv).Item("CantMAR").ToString) + _
                            CDec(dv.Item(contDv).Item("CantABR").ToString) + CDec(dv.Item(contDv).Item("CantMAY").ToString) + CDec(dv.Item(contDv).Item("CantJUN").ToString) + CDec(dv.Item(contDv).Item("CantJUL").ToString) + _
                            CDec(dv.Item(contDv).Item("CantAGO").ToString) + CDec(dv.Item(contDv).Item("CantSET").ToString) + CDec(dv.Item(contDv).Item("CantOCT").ToString) + CDec(dv.Item(contDv).Item("CantNOV").ToString) + _
                            CDec(dv.Item(contDv).Item("CantDIC").ToString)

                            ws.Cell(FilaDet, 22).Value = int_cantidadTotal
                            int_CantTot = int_CantTot + int_cantidadTotal

                        End If
                        ''Pintado del cuadrado de la cabecera

                        ''.Merge()


                        With ws.Range(ws.Cell(FilaCab + 2, ColumnaCab), ws.Cell(FilaDet, ColumnaCab + 21))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin

                        End With


                        ' .Merge()

                        With ws.Range(ws.Cell(6, 4), ws.Cell(7, 4))
                            .Style.Border.RightBorder = XLBorderStyleValues.Thin
                            .Style.Border.TopBorder = XLBorderStyleValues.Thin
                            .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                            .Style.Border.LeftBorder = XLBorderStyleValues.Thin

                        End With


                        contDv = contDv + 1
                        FilaDet = FilaDet + 1
                    End While
                    ws.Cell(FilaDet, 7).Value = "Total"

                    With ws.Cell(FilaDet, 7)
                        .Style.Fill.BackgroundColor = XLColor.FromHtml("#CCC0DA")
                    End With

                    '' ws.Cell(FilaDet, ColumnaCab + 6).Interior.Color() = RGB(153, 153, 204)
                    ws.Cell(FilaDet, 8).Value = "S/." & CStr(Format(CDec(dec_CantPrecioTot), "##,##0.00"))
                    ws.Cell(FilaDet, 10).Value = int_CantEne
                    ws.Cell(FilaDet, 11).Value = int_CantFeb
                    ws.Cell(FilaDet, 12).Value = int_CantMar
                    ws.Cell(FilaDet, 13).Value = int_CantAbr
                    ws.Cell(FilaDet, 14).Value = int_CantMay
                    ws.Cell(FilaDet, 15).Value = int_CantJun
                    ws.Cell(FilaDet, 16).Value = int_CantJul
                    ws.Cell(FilaDet, 17).Value = int_CantAgo
                    ws.Cell(FilaDet, 18).Value = int_CantSep
                    ws.Cell(FilaDet, 19).Value = int_CantOct
                    ws.Cell(FilaDet, 20).Value = int_CantNov
                    ws.Cell(FilaDet, 21).Value = int_CantDic
                    ws.Cell(FilaDet, 22).Value = int_CantTot

                    ''Pintado del cuadrado de Total
                    ' .Merge()
                    With ws.Range(ws.Cell(FilaDet, ColumnaCab + 6), ws.Cell(FilaDet, ColumnaCab + 21))
                        .Style.Border.RightBorder = XLBorderStyleValues.Thin
                        .Style.Border.TopBorder = XLBorderStyleValues.Thin
                        .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                        .Style.Border.LeftBorder = XLBorderStyleValues.Thin

                    End With

                    dec_CantTotalPrecioTot = dec_CantTotalPrecioTot + dec_CantPrecioTot
                    int_CantTotalEne = int_CantTotalEne + int_CantEne
                    int_CantTotalFeb = int_CantTotalFeb + int_CantFeb
                    int_CantTotalMar = int_CantTotalMar + int_CantMar
                    int_CantTotalAbr = int_CantTotalAbr + int_CantAbr
                    int_CantTotalMay = int_CantTotalMay + int_CantMay
                    int_CantTotalJun = int_CantTotalJun + int_CantJun
                    int_CantTotalJul = int_CantTotalJul + int_CantJul
                    int_CantTotalAgo = int_CantTotalAgo + int_CantAgo
                    int_CantTotalSep = int_CantTotalSep + int_CantSep
                    int_CantTotalOct = int_CantTotalOct + int_CantOct
                    int_CantTotalNov = int_CantTotalNov + int_CantNov
                    int_CantTotalDic = int_CantTotalDic + int_CantDic
                    int_CantTotalTot = int_CantTotalTot + int_CantTot

                    contDv = 0
                    dec_CantPrecioTot = 0
                    int_CantEne = 0
                    int_CantFeb = 0
                    int_CantMar = 0
                    int_CantAbr = 0
                    int_CantMay = 0
                    int_CantJun = 0
                    int_CantJul = 0
                    int_CantAgo = 0
                    int_CantSep = 0
                    int_CantOct = 0
                    int_CantNov = 0
                    int_CantDic = 0
                    int_CantTot = 0

                End If
                'ColumnaCab = ColumnaCab + 2
                'int_contColDetalle = int_contColDetalle + 1
                cont = cont + 1
                FilaCab = FilaDet + 3
            End While

            ws.Range(ws.Cell(FilaCab, 1), ws.Cell(FilaCab, 7)).Merge()



            ws.Range(ws.Cell(FilaCab, 1), ws.Cell(FilaCab, 7)).Value = "TOTAL GASTOS   S/."

            With ws.Range(ws.Cell(FilaCab, 1), ws.Cell(FilaCab, 7))
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#FFFF00")
            End With

            'CStr(Format(CDec(dec_CantPrecioTot), "##,##0.00"))
            ''oExcel.Range(oCells(FilaCab, 1), oCells(FilaCab, 7)).Interior.Color() = RGB(255, 255, 153)
            ws.Cell(FilaCab, 8).Value = "S/." & CStr(Format(CDec(dec_CantTotalPrecioTot), "##,##0.00"))
            ws.Cell(FilaCab, 10).Value = int_CantTotalEne
            ws.Cell(FilaCab, 11).Value = int_CantTotalFeb
            ws.Cell(FilaCab, 12).Value = int_CantTotalMar
            ws.Cell(FilaCab, 13).Value = int_CantTotalAbr
            ws.Cell(FilaCab, 14).Value = int_CantTotalMay
            ws.Cell(FilaCab, 15).Value = int_CantTotalJun
            ws.Cell(FilaCab, 16).Value = int_CantTotalJul
            ws.Cell(FilaCab, 17).Value = int_CantTotalAgo
            ws.Cell(FilaCab, 18).Value = int_CantTotalSep
            ws.Cell(FilaCab, 19).Value = int_CantTotalOct
            ws.Cell(FilaCab, 20).Value = int_CantTotalNov
            ws.Cell(FilaCab, 21).Value = int_CantTotalDic
            ws.Cell(FilaCab, 22).Value = int_CantTotalTot

            ' oExcel.Range(oCells(14, 1), oCells(FilaCab, 21)).WrapText = True
            ''Pintado del cuadrado de Total



            With ws.Range(ws.Cell(FilaCab, 1), ws.Cell(FilaCab, 22))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin

            End With
            'Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("B" & 12 & ":" & DevLetraColumna((dtLibro.Rows.Count * 2) + 8) & (int_cantidadAlumnos + 11))
            'cuadradoCompleto(oExcel, objCelda)

            ''oExcel.ActiveWindow.Zoom = 75

            ws.Columns().AdjustToContents()

            ws.Columns(4).Width = 42
            ws.Rows().AdjustToContents()



            ws.PageSetup.AdjustTo(60)
            'ws.PageSetup.FitToPages(1, 0)
            'ws.PageSetup.PagesTall = 1
            ws.PageSetup.PagesWide = 1


            ws.PageSetup.PageOrientation = ClosedXML.Excel.XLPageOrientation.Landscape
            ws.PageSetup.Margins.Top = 0.5 '1.9
            ws.PageSetup.Margins.Bottom = 0.5 '1.9
            ws.PageSetup.Margins.Left = 0 '0.6
            ws.PageSetup.Margins.Right = 0 '0.6
            ws.PageSetup.Margins.Header = 0 '0.8
            ws.PageSetup.Margins.Footer = 0 '0.8
            ''


            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales


        Catch ex As Exception

        End Try
    End Sub




    Public Shared Sub LLenarReporteErroresValidacion(ByVal dtReporte As System.Data.DataTable, _
                                 ByVal strNombreArchivo As String, ByRef rutaTempDest As String)

        Dim rutaPlantillas As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteSolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases")
        Dim rutaTemp As String = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/", "")
        ''value="\Plantillas\presupuestos\libretaPrimaria.xlsx"/>
        Try
            Dim rutaREpositorioTemporales As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) + "\Reportes\" & rutaTemp & ".xlsx"

            File.Copy(rutaPlantillas, rutaREpositorioTemporales)
            Dim workbook As New XLWorkbook(rutaREpositorioTemporales)
            Dim ws = workbook.Worksheet(1)
             

            Dim contadorFilas As Integer = 2
            Dim contadorColumnas As Integer = 1

            ws.Cell(contadorFilas, 1).Value = "Clase "
            ws.Cell(contadorFilas, 2).Value = "Categoria "
            ws.Cell(contadorFilas, 3).Value = "Subcategoria "
            ws.Cell(contadorFilas, 4).Value = "Articulo "
            ws.Cell(contadorFilas, 5).Value = "Descripcion del error "


            With ws.Cell(contadorFilas, 1)
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#6DDCF9")
            End With
            With ws.Cell(contadorFilas, 2)
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#6DDCF9")
            End With
            With ws.Cell(contadorFilas, 3)
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#6DDCF9")
            End With
            With ws.Cell(contadorFilas, 4)
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#6DDCF9")
            End With

            With ws.Cell(contadorFilas, 5)
                .Style.Fill.BackgroundColor = XLColor.FromHtml("#6DDCF9")
            End With

            

            ''
            ' detSolArt.DSPA_CheckEne, 
            '          detSolArt.DSPA_CantEne, 
            '          detSolArt.DSPA_CheckFeb,
            '           detSolArt.DSPA_CantFeb,
            '            detSolArt.DSPA_CheckMar,
            '             detSolArt.DSPA_CantMar,
            '              detSolArt.DSPA_CheckAbr, 
            '          detSolArt.DSPA_CantAbr,
            '           detSolArt.DSPA_CheckMay,
            '            detSolArt.DSPA_CantMay,
            '             detSolArt.DSPA_CheckJun,
            '              detSolArt.DSPA_CantJun,
            '               detSolArt.DSPA_CheckJul, 
            '          detSolArt.DSPA_CantJul,
            '           detSolArt.DSPA_CheckAgo,
            '            detSolArt.DSPA_CantAgo, 
            '            detSolArt.DSPA_CheckSet,
            '             detSolArt.DSPA_CantSet,
            '              detSolArt.DSPA_CheckOct, 
            '          detSolArt.DSPA_CantOct, 
            '          detSolArt.DSPA_CheckNov,
            '           detSolArt.DSPA_CantNov,
            '            detSolArt.DSPA_CheckDic,
            '             detSolArt.DSPA_CantDic, 
            '             detSolArt.DSPA_TipoDistribucion, 
            '          detSolArt.DSPA_TipoValidacion,
            '           detSolArt.DSPA_Precio, 
            'detSolArt.DSPA_Cantidad()
            ''
            For Each filas As DataRow In dtReporte.Rows
                contadorFilas += 1
                ws.Cell(contadorFilas, 1).Value = filas("CS_Descripcion").ToString()
                ws.Cell(contadorFilas, 2).Value = filas("CT_Descripcion").ToString()
                ws.Cell(contadorFilas, 3).Value = filas("SC_Descripcion").ToString()
                ws.Cell(contadorFilas, 4).Value = filas("AT_Descripcion").ToString()

                ws.Cell(contadorFilas, 5).Value = obtenerDescripcionError(filas)

            Next


 

            ws.Columns(1).Width = 85
            ws.Columns(2).Width = 85
            ws.Columns(3).Width = 85
            ws.Columns(4).Width = 85
            ws.Columns(5).Width = 90

            With ws.Range(ws.Cell(1, 1), ws.Cell(contadorFilas, 5))
                .Style.Border.RightBorder = XLBorderStyleValues.Thin
                .Style.Border.TopBorder = XLBorderStyleValues.Thin
                .Style.Border.BottomBorder = XLBorderStyleValues.Thin
                .Style.Border.LeftBorder = XLBorderStyleValues.Thin

            End With



            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales



        Catch ex As Exception

        End Try

    End Sub


    Private Shared Function obtenerDescripcionError(ByVal filaArticulo As DataRow) As String

        'bdJson[filaActual].tipoValidacion=(($("#rbtPro").attr("checked")=="checked" )?1:0)
        'var estadoPro=(($("#rbtPro").attr("checked")=="checked" )?1:0)
        'bdJson[filaActual].tipoDistribucion=(($("#cskCantidad").attr("checked")=="checked")?true:false)
        'var estadoDis=(($("#cskCantidad").attr("checked")=="checked")?true:false)
        Dim descripcionError As String = ""
        Try

            Dim tipoDistribucion As Boolean = False
            Dim tipoValidacion As Integer = 0

            Dim cantidad As Integer = 0
            Dim precio As Double = 0.0

            Dim precioTotal As Double = 0.0

            cantidad = CInt(filaArticulo("DSPA_Cantidad").ToString())
            precio = CDbl(filaArticulo("DSPA_Precio").ToString())

            precioTotal = cantidad * precio

            Dim contadorCantidad As Integer = 0
            Dim acPrecioTotal As Double = 0.0

            If CInt(filaArticulo("DSPA_TipoValidacion").ToString()) = 1 Then ''tipo validacion 1 :prorrateado

            Else ''tipo validacion 0 :normal

            End If


            If CBool(filaArticulo("DSPA_TipoDistribucion").ToString()) = True Then ''tipo validacion true :por cantidad
                contadorCantidad += CInt(filaArticulo("DSPA_CantEne").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantFeb").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantMar").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantAbr").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantMay").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantJun").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantJul").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantAgo").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantSet").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantOct").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantNov").ToString())
                contadorCantidad += CInt(filaArticulo("DSPA_CantDic").ToString())

                If contadorCantidad < cantidad Then
                    descripcionError = "No se ha distribuido toda la cantidad total solicitada "
                    Return descripcionError
                End If

            Else ''tipo validacion false :por precio

                acPrecioTotal += CDbl(filaArticulo("DSPA_CantEne").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantFeb").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantMar").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantAbr").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantMay").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantJun").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantJul").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantAgo").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantSet").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantOct").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantNov").ToString())
                acPrecioTotal += CDbl(filaArticulo("DSPA_CantDic").ToString())

                If acPrecioTotal < precioTotal Then
                    descripcionError = "la suma de las precios es menor al precio total  "
                    Return descripcionError

                Else
                    descripcionError = "La suma de los precios excede al precio total  "
                    Return descripcionError
                End If


            End If




            'For Each filas As DataRow In dtArticulo.Rows

            'Next

        Catch ex As Exception


        Finally
        End Try
        ''
        ' detSolArt.DSPA_CheckEne, 
        '          detSolArt.DSPA_CantEne, 
        '          detSolArt.DSPA_CheckFeb,
        '           detSolArt.DSPA_CantFeb,
        '            detSolArt.DSPA_CheckMar,
        '             detSolArt.DSPA_CantMar,
        '              detSolArt.DSPA_CheckAbr, 
        '          detSolArt.DSPA_CantAbr,
        '           detSolArt.DSPA_CheckMay,
        '            detSolArt.DSPA_CantMay,
        '             detSolArt.DSPA_CheckJun,
        '              detSolArt.DSPA_CantJun,
        '               detSolArt.DSPA_CheckJul, 
        '          detSolArt.DSPA_CantJul,
        '           detSolArt.DSPA_CheckAgo,
        '            detSolArt.DSPA_CantAgo, 
        '            detSolArt.DSPA_CheckSet,
        '             detSolArt.DSPA_CantSet,
        '              detSolArt.DSPA_CheckOct, 
        '          detSolArt.DSPA_CantOct, 
        '          detSolArt.DSPA_CheckNov,
        '           detSolArt.DSPA_CantNov,
        '            detSolArt.DSPA_CheckDic,
        '             detSolArt.DSPA_CantDic, 
        '             detSolArt.DSPA_TipoDistribucion, 
        '          detSolArt.DSPA_TipoValidacion,
        '           detSolArt.DSPA_Precio, 
        'detSolArt.DSPA_Cantidad()
        ''
    End Function
    'Obtiene el color a pintar de la clase
    Public Shared Function ObtenerColor(ByVal int_numClase As Integer) As String

        Dim str_Color As String = ""

        If int_numClase = 0 Then
            str_Color = RGB(0, 204, 255)
        ElseIf int_numClase = 1 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 2 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 3 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 4 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 5 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 6 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 7 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 8 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 9 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 10 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 11 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 12 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 13 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 14 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 15 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 16 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 17 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 18 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 19 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 20 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 21 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 22 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 23 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 24 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 25 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 26 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 27 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 28 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 29 Then
            str_Color = RGB(153, 153, 204)
        ElseIf int_numClase = 30 Then
            str_Color = RGB(153, 153, 204)
        Else
            str_Color = RGB(153, 153, 204)
        End If

        Return str_Color

    End Function


#End Region

#End Region

End Class
