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

Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html

Imports CrystalDecisions
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports SaintGeorgeOnline_BusinessEntities.ModuloReportes

Imports ClosedXML
Imports ClosedXML.Excel

'nnj
'jn
'mmm

Public Class Exportacion

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

#Region "Reportes Genericos - Listas"

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporte(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcelConfirmacionAsistencia").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells


        LlenarPlantilla(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     17/08/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteConfirmacionAsistencia(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String, ByVal str_cantidadConfirmacion As String, ByVal str_cantidadAsistentes As String, ByVal str_cantidadTotalInscritosConfirmacion As String, ByVal str_cantidadTotalInscritosAsistentes As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcelConfirmacionAsistencia").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells


        LlenarPlantillaConfirmacionAsistencia(dtReporte, oCells, oExcel, str_NombreEntidadReporte, str_cantidadConfirmacion, str_cantidadAsistentes, str_cantidadTotalInscritosConfirmacion, str_cantidadTotalInscritosAsistentes)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:              Fanny Salinas
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaConfirmacionAsistencia(ByVal dtReporte As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String, ByVal str_cantidadConfirmacion As String, ByVal str_cantidadAsistentes As String, ByVal str_cantidadTotalInscritosConfirmacion As String, ByVal str_cantidadTotalInscritosAsistentes As String)

        Dim dt_reporte As New DataTable
        Dim fila As Integer = 8
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_reporte = dtReporte

        'Pintado de cabecera
        While cont_columnas <= dt_reporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dt_reporte.Columns(cont_columnas).ColumnName
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Bold = True
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Interior.Color() = RGB(149, 179, 215)
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Color = RGB(0, 0, 0)
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).HorizontalAlignment = 3

            cont_columnas = cont_columnas + 1
        End While

        'Pintado de detalle
        cont_columnas = 0
        fila = 9

        While cont_columnas <= dt_reporte.Columns.Count - 1
            While cont_filas <= dt_reporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dt_reporte.Rows(cont_filas).Item(cont_columnas)
                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).WrapText = True
                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).Interior.Color() = RGB(242, 242, 242)
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While


        Dim int_cantidadDefilaEspacios As Integer = dt_reporte.Rows.Count + 8
        Dim str_letra As String = ""
        Dim int_fila As Integer

        For i As Integer = 0 To dt_reporte.Columns.Count
            str_letra = DevLetraColumna(i + 4)
            For j As Integer = 0 To dt_reporte.Rows.Count
                int_fila = 9 + j
                oCells.Range(str_letra & int_fila).RowHeight = 21
                oCells.Range(str_letra & int_fila).VerticalAlignment = 2 'VerticalAlign.Middle
            Next
            oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        Next

        oCells.Range("G9:G" & int_cantidadDefilaEspacios.ToString).HorizontalAlignment = 3
        oCells.Range("I9:I" & int_cantidadDefilaEspacios.ToString).HorizontalAlignment = 3
        oCells.Range("L9:L" & int_cantidadDefilaEspacios.ToString).HorizontalAlignment = 3

        Dim total_columnas As Integer = dt_reporte.Columns.Count
        Dim ultimaPosicion As Integer = DevIDColumna("E") + total_columnas - 2 ' (idx + E)

        oCells(3, 5) = "Relación de " & str_NombreEntidadReporte
        oCells.Range("E3:E3").Font.Size = 25
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = 3 'HorizontalAlign.Right

        oCells(5, 5) = "Cantidad de Confirmados = " & "   " & str_cantidadConfirmacion
        oCells.Range("E5:" & "G5").Merge()
        oCells.Range("E5:E5").Font.Size = 12

        oCells(6, 5) = "Cantidad de Asistentes = " & "   " & str_cantidadAsistentes
        oCells.Range("E6:" & "G6").Merge()
        oCells.Range("E6:E6").Font.Size = 12

        oCells(5, 8) = "Cantidad Total de Inscritos Confirmados = " & "   " & str_cantidadTotalInscritosConfirmacion
        oCells.Range("H5:" & "K5").Merge()
        oCells.Range("H5:H5").Font.Size = 12

        oCells(6, 8) = "Cantidad Total de Inscritos Asistentes = " & "   " & str_cantidadTotalInscritosAsistentes
        oCells.Range("H6:" & "K6").Merge()
        oCells.Range("H6:H6").Font.Size = 12

        Dim objCelda1 As Microsoft.Office.Interop.Excel.Range = oCells.Range("E5:K6")

        cuadradoCompleto(oExcel, objCelda1)


        Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("D8:" & "L" & int_cantidadDefilaEspacios.ToString)

        cuadradoCompleto(oExcel, objCelda)

        
        oExcel.ActiveWindow.Zoom = 75

    End Sub

    Public Shared Function ExportarReporteAlumnosRetirados(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteAlumnosRetirados").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells


        LlenarPlantillaAlumnosRetirados(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function


    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantilla(ByVal dtReporte As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim dt_reporte As New DataTable
        Dim fila As Integer = 8
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_reporte = dtReporte

        'Pintado de cabecera
        While cont_columnas <= dt_reporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dt_reporte.Columns(cont_columnas).ColumnName
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Bold = True
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Interior.Color() = RGB(149, 179, 215)
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Color = RGB(0, 0, 0)
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).HorizontalAlignment = 3

            cont_columnas = cont_columnas + 1
        End While

        'Pintado de detalle
        cont_columnas = 0
        fila = 9

        While cont_columnas <= dt_reporte.Columns.Count - 1
            While cont_filas <= dt_reporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dt_reporte.Rows(cont_filas).Item(cont_columnas)
                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).WrapText = True
                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).Interior.Color() = RGB(242, 242, 242)
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While



        Dim str_letra As String = ""
        Dim int_fila As Integer

        For i As Integer = 0 To dt_reporte.Columns.Count
            str_letra = DevLetraColumna(i + 4)
            For j As Integer = 0 To dt_reporte.Rows.Count
                int_fila = 9 + j
                oCells.Range(str_letra & int_fila).RowHeight = 21
                oCells.Range(str_letra & int_fila).VerticalAlignment = 2 'VerticalAlign.Middle
            Next
            oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        Next

        Dim total_columnas As Integer = dt_reporte.Columns.Count
        Dim ultimaPosicion As Integer = DevIDColumna("E") + total_columnas - 2 ' (idx + E)

        oCells(3, 5) = "Relación de " & str_NombreEntidadReporte
        oCells.Range("E3:E3").Font.Size = 25
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = 3 'HorizontalAlign.Right


        Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("D" & 8 & ":" & DevLetraColumna(dt_reporte.Columns.Count + 3) & (fila + dt_reporte.Rows.Count - 1))

        cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub

    Private Shared Sub LlenarPlantillaAlumnosRetirados(ByVal dtReporte As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim dt_reporte As New DataTable
        Dim fila As Integer = 8
        Dim columna As Integer = 1
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_reporte = dtReporte

        'Pintado de cabecera
        While cont_columnas <= dt_reporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dt_reporte.Columns(cont_columnas).ColumnName
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Bold = True
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Interior.Color() = RGB(149, 179, 215)
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Color = RGB(0, 0, 0)
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).HorizontalAlignment = 3

            cont_columnas = cont_columnas + 1
        End While

        oExcel.Range(oCells(8, 1), oCells(8, 1)).ColumnWidth = 5
        oExcel.Range(oCells(8, 2), oCells(8, 2)).ColumnWidth = 35
        oExcel.Range(oCells(8, 3), oCells(8, 3)).ColumnWidth = 20
        'oExcel.Range(oCells(8, 4), oCells(8, 4)).ColumnWidth = 25
        'oExcel.Range(oCells(8, 6), oCells(8, 6)).ColumnWidth = 25
        'oExcel.Range(oCells(8, 9), oCells(8, 9)).ColumnWidth = 25

        'Pintado de detalle
        cont_columnas = 0
        fila = 9

        While cont_columnas <= dt_reporte.Columns.Count - 1
            While cont_filas <= dt_reporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dt_reporte.Rows(cont_filas).Item(cont_columnas)
                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).WrapText = True
                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).Interior.Color() = RGB(242, 242, 242)
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

     

        Dim str_letra As String = ""
        Dim int_fila As Integer

        For i As Integer = 0 To dt_reporte.Columns.Count
            str_letra = DevLetraColumna(i + 4)
            For j As Integer = 0 To dt_reporte.Rows.Count
                int_fila = 9 + j
                oCells.Range(str_letra & int_fila).RowHeight = 21
                oCells.Range(str_letra & int_fila).VerticalAlignment = 2 'VerticalAlign.Middle
            Next
            oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        Next

        Dim total_columnas As Integer = dt_reporte.Columns.Count
        Dim ultimaPosicion As Integer = DevIDColumna("E") + total_columnas - 2 ' (idx + E)

        oCells(3, 3) = "Relación de " & str_NombreEntidadReporte
        oCells.Range("B3:B3").Font.Size = 25
        oCells.Range("B3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        oCells.Range("B3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = 3 'HorizontalAlign.Right

        oCells(4, 3) = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oCells.Range("B4:B4").Font.Size = 25
        oCells.Range("B4:" & DevLetraColumna(ultimaPosicion) & "4").Merge()
        oCells.Range("B4:" & DevLetraColumna(ultimaPosicion) & "4").HorizontalAlignment = 3 'HorizontalAlign.Right


        Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("A" & 8 & ":" & DevLetraColumna(dt_reporte.Columns.Count) & (fila + dt_reporte.Rows.Count - 1))

        cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub

#End Region




    '/////////////////////
    'Reportes por módulos/
    '/////////////////////

#Region "Modulo de Banco de Libros"

    'Reporte Prestamos:Consolidado de Prestamos

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     04/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteBancoLibros(ByVal dtReporte As System.Data.DataSet, ByVal int_CodigoGrado As Integer, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Dim dtAlumnos As DataTable
        Dim dtLibro As DataTable
        Dim dtLibrosPrestados As DataTable
        Dim dtGrado As DataTable
        dtAlumnos = dtReporte.Tables(0)
        dtLibro = dtReporte.Tables(1)
        dtLibrosPrestados = dtReporte.Tables(2)
        dtGrado = dtReporte.Tables(3)

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_BancoLibros").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        'oBook = oBooks.Item(1)
        'oSheets = oBook.Worksheets
        'oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        'oSheet.Name = str_NombreEntidadReporte
        'oCells = oSheet.Cells

        'LlenarPlantillaPrestamo(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        Dim int_contGrado As Integer = 0

        If int_CodigoGrado = 0 Then
            While int_contGrado <= dtGrado.Rows.Count - 1
                oBook = oBooks.Item(1)
                oSheets = oBook.Worksheets
                oSheet = CType(oSheets.Item(int_contGrado + 1), Microsoft.Office.Interop.Excel.Worksheet)
                oSheet.Name = dtGrado.Rows(int_contGrado).Item("GD_Descripcion")
                oCells = oSheet.Cells
                oSheet.Activate()
                Dim dt_Al As DataTable
                Dim dt_Lib As DataTable
                Dim dt_LibPrest As DataTable

                dt_Al = LlenarDataTable(dtAlumnos, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))
                dt_Lib = LlenarDataTable(dtLibro, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))
                dt_LibPrest = LlenarDataTable(dtLibrosPrestados, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))

                LlenarPlantillaPrestamo(dt_Al, dt_Lib, dt_LibPrest, oCells, oExcel, str_NombreEntidadReporte)

                int_contGrado = int_contGrado + 1
            End While
        Else
            Dim int_contHojaExc As Integer = 2

            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets

            While int_contHojaExc <= 14
                oSheet = CType(oSheets.Item(int_contHojaExc), Microsoft.Office.Interop.Excel.Worksheet)
                oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
                int_contHojaExc = int_contHojaExc + 1
            End While

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = str_NombreEntidadReporte
            oCells = oSheet.Cells
            'oExcel.Application.Sheets(str_NombreEntidadReporte).Select()
            oSheet.Activate()
            LlenarPlantillaPrestamo(dtAlumnos, dtLibro, dtLibrosPrestados, oCells, oExcel, str_NombreEntidadReporte)

        End If

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dt_Alumnos">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     04/07/2011
    ''' Modificado por:        Fanny Salinas
    ''' Fecha de modificación: 20/07/2011 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaPrestamo(ByVal dt_Alumnos As System.Data.DataTable, ByVal dtLibro As System.Data.DataTable, ByVal dtLibrosPrestados As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 7
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        columna = 9

        'Pintado de Titulo
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Value = "CONSOLIDADO DE PRESTAMOS POR ALUMNO - AÑO ACADÉMICO  " & dt_Alumnos.Rows(0).Item("AC_Descripcion")
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Merge()
        oExcel.Range(oCells(4, 5), oCells(4, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Font.Bold = True

        'Pintado de Cabecera estática 
        oExcel.Range(oCells(7, 2), oCells(8, 2)).Merge()
        oExcel.Range(oCells(7, 2), oCells(8, 2)).Value = "Año Académico"
        oExcel.Range(oCells(7, 3), oCells(8, 3)).Merge()
        oExcel.Range(oCells(7, 3), oCells(8, 3)).Value = "Grado"
        oExcel.Range(oCells(7, 4), oCells(8, 4)).Merge()
        oExcel.Range(oCells(7, 4), oCells(8, 4)).Value = "Seccion"
        oExcel.Range(oCells(7, 5), oCells(8, 5)).Merge()
        oExcel.Range(oCells(7, 5), oCells(8, 5)).Value = "Alumno"
        oExcel.Range(oCells(7, 6), oCells(8, 6)).Merge()
        oExcel.Range(oCells(7, 6), oCells(8, 6)).Value = "Estado actual"
        oExcel.Range(oCells(7, 7), oCells(8, 7)).Merge()
        oExcel.Range(oCells(7, 7), oCells(8, 7)).Value = "Cantidad"
        oExcel.Range(oCells(7, 8), oCells(8, 8)).Merge()
        oExcel.Range(oCells(7, 8), oCells(8, 8)).Value = "Pagos"

        oExcel.Range(oCells(7, 2), oCells(8, 8)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 2), oCells(8, 8)).WrapText = True
        oExcel.Range(oCells(7, 2), oCells(8, 8)).Font.Bold = True
        oExcel.Range(oCells(7, 2), oCells(8, 8)).Interior.Color() = RGB(204, 255, 204)

        'Pintado del cuadrado de Cabecera estática 
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(7, 2), oExcel.Cells(8, 8)))
        Dim int_cantidadLibros As Integer = 0

        int_cantidadLibros = dtLibro.Rows.Count

        'Pintado de Cabecera Dinámica de libros

        While cont_columnas <= dtLibro.Rows.Count - 1
            oCells(fila, columna + cont_columnas) = dtLibro.Rows(cont_columnas).Item("Titulo")
            cont_columnas = cont_columnas + 1
        End While
        oExcel.Range(oExcel.Cells(fila, 9), oExcel.Cells(fila, columna + cont_columnas - 1)).Select()
        oExcel.Range(oExcel.Cells(fila, 9), oExcel.Cells(fila, columna + cont_columnas - 1)).Orientation = 90
        oExcel.Range(oExcel.Cells(fila, 9), oExcel.Cells(fila, columna + cont_columnas - 1)).RowHeight = 150
        oExcel.Range(oExcel.Cells(fila, 9), oExcel.Cells(fila, columna + cont_columnas - 1)).WrapText = True

        'oExcel.Range(oCells(7, 8), oCells(6, columna + cont_columnas - 1)).HorizontalAlignment = 3
        'oExcel.Range(oCells(7, 8), oCells(6, columna + cont_columnas - 1)).Interior.Color() = RGB(153, 255, 153)
        'oExcel.Range(oCells(7, 8), oCells(6, columna + cont_columnas - 1)).Font.Bold = True


        oExcel.Range(oCells(6, 8), oCells(6, columna + cont_columnas - 1)).Merge()
        oExcel.Range(oCells(6, 8), oCells(6, columna + cont_columnas - 1)).Value = "Detalle de Libros del Grado"
        oExcel.Range(oCells(6, 8), oCells(6, columna + cont_columnas - 1)).HorizontalAlignment = 3
        oExcel.Range(oCells(6, 8), oCells(7, columna + cont_columnas - 1)).Interior.Color() = RGB(152, 251, 152)
        oExcel.Range(oCells(6, 8), oCells(7, columna + cont_columnas - 1)).Font.Bold = True

        cuadradoCompleto(oExcel, oExcel.Range(oCells(6, 8), oCells(8, columna + cont_columnas - 1)))

        cont_columnas = 0

        While cont_columnas <= dtLibro.Rows.Count - 1
            oCells(8, columna + cont_columnas) = dtLibro.Rows(cont_columnas).Item("Moneda") & " " & dtLibro.Rows(cont_columnas).Item("PrecioPrestamo")
            cont_columnas = cont_columnas + 1
        End While
        oExcel.Range(oCells(8, 8), oCells(6, columna + cont_columnas - 1)).HorizontalAlignment = 3
        oExcel.Range(oCells(8, 8), oCells(6, columna + cont_columnas - 1)).Interior.Color() = RGB(204, 255, 204)
        oExcel.Range(oCells(8, 8), oCells(6, columna + cont_columnas - 1)).Font.Bold = True

        'Pintado de detalle
        columna = 2
        cont_columnas = 0
        fila = 9

        While cont_columnas <= 4
            While cont_filas <= dt_Alumnos.Rows.Count - 1
                If cont_columnas = 0 Then
                    oCells(fila + cont_filas, columna + cont_columnas) = dt_Alumnos.Rows(cont_filas).Item("AC_Descripcion")
                ElseIf cont_columnas = 1 Then
                    oCells(fila + cont_filas, columna + cont_columnas) = dt_Alumnos.Rows(cont_filas).Item("GD_Descripcion")
                ElseIf cont_columnas = 2 Then
                    oCells(fila + cont_filas, columna + cont_columnas) = dt_Alumnos.Rows(cont_filas).Item("AU_Descripcion")
                ElseIf cont_columnas = 3 Then
                    oCells(fila + cont_filas, columna + cont_columnas) = dt_Alumnos.Rows(cont_filas).Item("NombreCompleto")
                ElseIf cont_columnas = 4 Then
                    oCells(fila + cont_filas, columna + cont_columnas) = dt_Alumnos.Rows(cont_filas).Item("DescEstadoActualAlumno")
                End If
                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).WrapText = True
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While

        'pintado de Prestamo por alumno
        columna = 9
        cont_columnas = 0
        cont_filas = 0
        Dim cont_Prestado As Integer = 0
        Dim str_codigoAlumno As String = 0
        Dim int_codigoLibro As Integer = 0
        Dim dvCantPrestados As DataView
        Dim dv As DataView
        dvCantPrestados = dtLibrosPrestados.DefaultView
        dv = dtLibrosPrestados.DefaultView
        Dim int_cantidad As Integer = 0
        Dim dc_Pagos As Decimal = 0.0
        Dim int_totalCantidad As Integer = 0
        Dim dc_totalPagos As Decimal = 0.0
        Dim dc_totalPrecioLibros As Decimal = 0.0
        Dim int_cantidadAlumnos As Integer = dt_Alumnos.Rows.Count
        While cont_filas <= dt_Alumnos.Rows.Count - 1
            str_codigoAlumno = dt_Alumnos.Rows(cont_filas).Item("AL_CodigoAlumno")
            While cont_columnas <= dtLibro.Rows.Count - 1
                int_codigoLibro = dtLibro.Rows(cont_columnas).Item("CodigoLibro")

                dv.RowFilter = "1=1 and CodigoAlumno=" & str_codigoAlumno & " and CodigoLibro =" & int_codigoLibro.ToString

                While cont_Prestado <= dv.Count - 1
                    If int_codigoLibro = dv.Item(cont_Prestado).Item("CodigoLibro") Then
                        oCells(fila + cont_filas, columna + cont_columnas) = "X"
                        oCells(fila + cont_filas, columna + cont_columnas).HorizontalAlignment = 3
                        oCells(fila + cont_filas, columna + cont_columnas).Interior.Color() = RGB(255, 255, 204)
                        oCells(fila + cont_filas, columna + cont_columnas).Font.Bold = True
                        int_cantidad = int_cantidad + 1
                        dc_Pagos = dc_Pagos + CDec(dtLibro.Rows(cont_columnas).Item("PrecioPrestamo"))
                    Else
                        oCells(fila + cont_filas, columna + cont_columnas) = " "
                    End If
                    cont_Prestado = cont_Prestado + 1
                End While
                'pintado Totales 
                dvCantPrestados.RowFilter = "1=1 and CodigoLibro =" & int_codigoLibro.ToString
                dc_totalPrecioLibros = dvCantPrestados.Count * CDec(dtLibro.Rows(cont_columnas).Item("PrecioPrestamo"))
                oCells(int_cantidadAlumnos + 9, columna + cont_columnas) = "S/." & dc_totalPrecioLibros.ToString
                oCells(int_cantidadAlumnos + 9, columna + cont_columnas).HorizontalAlignment = 3

                cont_Prestado = 0
                cont_columnas = cont_columnas + 1
                dc_totalPrecioLibros = 0

            End While
            int_totalCantidad = int_totalCantidad + int_cantidad
            dc_totalPagos = dc_totalPagos + dc_Pagos
            'Pintar la cantidad de libros
            oCells(fila + cont_filas, 7) = int_cantidad
            oCells(fila + cont_filas, 7).HorizontalAlignment = 3


            oCells(int_cantidadAlumnos + 9, 5) = "Total"
            'pintar  Total de cantidad de libros
            oCells(int_cantidadAlumnos + 9, 7) = int_totalCantidad
            oCells(int_cantidadAlumnos + 9, 7).HorizontalAlignment = 3

            'Pintar el total de pagos
            oCells(fila + cont_filas, 8) = "S/." & dc_Pagos.ToString
            oCells(fila + cont_filas, 8).HorizontalAlignment = 3

            'pintar  Total de cantidad de Pagos
            oCells(int_cantidadAlumnos + 9, 8) = "S/." & dc_totalPagos.ToString
            oCells(int_cantidadAlumnos + 9, 8).HorizontalAlignment = 3
            'Cuadrado de total
            cuadradoCompleto(oExcel, oExcel.Range(oCells(int_cantidadAlumnos + 9, 5), oCells(int_cantidadAlumnos + 9, columna + cont_columnas - 1)))
            oExcel.Range(oCells(int_cantidadAlumnos + 9, 5), oCells(int_cantidadAlumnos + 9, columna + cont_columnas - 1)).Interior.Color() = RGB(146, 208, 80)

            int_cantidad = 0
            dc_Pagos = 0.0
            cont_columnas = 0
            cont_filas = cont_filas + 1

        End While

        Dim str_letra As String = ""
        'Dim int_fila As Integer
        Dim int_totalColumnas As Integer = 8 + dtLibro.Rows.Count
        Dim int_totalcol As Integer = dtLibro.Rows.Count + 8

        oExcel.Range(oCells(9, 2), oCells((int_cantidadAlumnos + 8), int_totalcol)).EntireColumn.AutoFit()
        oExcel.Range(oCells(9, 2), oCells((int_cantidadAlumnos + 8), 4)).HorizontalAlignment = 3
        oExcel.Range(oCells(9, 6), oCells((int_cantidadAlumnos + 8), int_totalcol)).HorizontalAlignment = 3
        cuadradoCompleto(oExcel, oExcel.Range(oCells(9, 2), oCells((int_cantidadAlumnos + 8), int_totalcol)))

        oExcel.ActiveWindow.Zoom = 75

    End Sub

    'Reporte Devoluciones: Consolidado de Devoluciones

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     06/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteDevolucion(ByVal dtReporte As DataSet, ByVal int_CodigoGrado As Integer, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Dim dtAlumnos As DataTable
        Dim dtLibro As DataTable
        Dim dtLibrosPrestados As DataTable
        Dim dtGrado As DataTable
        dtAlumnos = dtReporte.Tables(0)
        dtLibro = dtReporte.Tables(1)
        dtLibrosPrestados = dtReporte.Tables(2)
        dtGrado = dtReporte.Tables(3)

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_DevolucionBancoLibros").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        Dim int_contGrado As Integer = 0

        If int_CodigoGrado = 0 Then
            While int_contGrado <= dtGrado.Rows.Count - 1
                oBook = oBooks.Item(1)
                oSheets = oBook.Worksheets
                oSheet = CType(oSheets.Item(int_contGrado + 1), Microsoft.Office.Interop.Excel.Worksheet)
                oSheet.Name = dtGrado.Rows(int_contGrado).Item("GD_Descripcion")
                oCells = oSheet.Cells
                'oSheet.(dtGrado.Rows(int_contGrado).Item("GD_Descripcion")).Select()
                oSheet.Activate()
                Dim dt_Al As DataTable
                Dim dt_Lib As DataTable
                Dim dt_LibPrest As DataTable

                dt_Al = LlenarDataTable(dtAlumnos, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))
                dt_Lib = LlenarDataTable(dtLibro, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))
                dt_LibPrest = LlenarDataTable(dtLibrosPrestados, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))

                LlenarPlantillaDevolucion(dt_Al, dt_Lib, dt_LibPrest, oCells, oExcel, str_NombreEntidadReporte)

                int_contGrado = int_contGrado + 1
            End While
        Else
            Dim int_contHojaExc As Integer = 2

            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets

            While int_contHojaExc <= 14
                oSheet = CType(oSheets.Item(int_contHojaExc), Microsoft.Office.Interop.Excel.Worksheet)
                oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
                int_contHojaExc = int_contHojaExc + 1
            End While

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = str_NombreEntidadReporte
            oCells = oSheet.Cells
            'oExcel.Application.Sheets(str_NombreEntidadReporte).Select()
            oSheet.Activate()
            LlenarPlantillaDevolucion(dtAlumnos, dtLibro, dtLibrosPrestados, oCells, oExcel, str_NombreEntidadReporte)
        End If


        'oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        'oSheet.Name = str_NombreEntidadReporte
        'oCells = oSheet.Cells

        'LlenarPlantillaDevolucion(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function


    'Reporte DeudoresBancoLibros: Consolidado de DeudoresBancoLibros

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     12/03/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteDeudoresBancoLibros(ByVal dtReporte As DataSet, ByVal int_CodigoGrado As Integer, ByVal str_CodigoAnio As String, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Dim dtAlumnos As DataTable
        Dim dtGrado As DataTable
        dtAlumnos = dtReporte.Tables(0)
        dtGrado = dtReporte.Tables(1)

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_DevolucionBancoLibros").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        Dim int_contGrado As Integer = 0

        If int_CodigoGrado = 0 Then
            While int_contGrado <= dtGrado.Rows.Count - 1
                oBook = oBooks.Item(1)
                oSheets = oBook.Worksheets
                oSheet = CType(oSheets.Item(int_contGrado + 1), Microsoft.Office.Interop.Excel.Worksheet)
                oSheet.Name = dtGrado.Rows(int_contGrado).Item("GD_Descripcion")
                oCells = oSheet.Cells
                oSheet.Activate()
                Dim dt_Al As DataTable

                dt_Al = LlenarDataTable(dtAlumnos, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))
                LlenarPlantillaDeudoresBancoLibros(dt_Al, str_CodigoAnio, oCells, oExcel, str_NombreEntidadReporte)

                int_contGrado = int_contGrado + 1
            End While
        Else
            Dim int_contHojaExc As Integer = 2

            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets

            While int_contHojaExc <= 14
                oSheet = CType(oSheets.Item(int_contHojaExc), Microsoft.Office.Interop.Excel.Worksheet)
                oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
                int_contHojaExc = int_contHojaExc + 1
            End While

            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = str_NombreEntidadReporte
            oCells = oSheet.Cells
            oSheet.Activate()
            LlenarPlantillaDeudoresBancoLibros(dtAlumnos, str_CodigoAnio, oCells, oExcel, str_NombreEntidadReporte)
        End If

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    Public Shared Function LlenarDataTable(ByVal dtReporte As System.Data.DataTable, ByVal int_CodigoGrado As Integer) As DataTable
        Dim int_fila As Integer = 0
        'Dim int_columna As Integer = 0
        Dim dt As DataTable
        Dim dv As DataView
        dt = dtReporte.Copy
        dt.Rows.Clear()
        dv = dtReporte.DefaultView
        Dim dr As DataRow

        dv.RowFilter = "1=1 and GD_CodigoGrado=" & int_CodigoGrado 'dtRepetidos.Rows(cont_1).Item("CodigoLibro")

        While int_fila <= dv.Count - 1
            dr = dt.NewRow
            If dtReporte.Columns.Count = 7 Then
                dr.Item(0) = dv.Item(int_fila).Item(0)
                dr.Item(1) = dv.Item(int_fila).Item(1)
                dr.Item(2) = dv.Item(int_fila).Item(2)
                dr.Item(3) = dv.Item(int_fila).Item(3)
                dr.Item(4) = dv.Item(int_fila).Item(4)
                dr.Item(5) = dv.Item(int_fila).Item(5)
                dr.Item(6) = dv.Item(int_fila).Item(6)
            ElseIf dtReporte.Columns.Count > 7 Then
                dr.Item(0) = dv.Item(int_fila).Item(0)
                dr.Item(1) = dv.Item(int_fila).Item(1)
                dr.Item(2) = dv.Item(int_fila).Item(2)
                dr.Item(3) = dv.Item(int_fila).Item(3)
                dr.Item(4) = dv.Item(int_fila).Item(4)
                dr.Item(5) = dv.Item(int_fila).Item(5)
                dr.Item(6) = dv.Item(int_fila).Item(6)
                dr.Item(7) = dv.Item(int_fila).Item(7)
                dr.Item(8) = dv.Item(int_fila).Item(8)
                dr.Item(9) = dv.Item(int_fila).Item(9)
                dr.Item(10) = dv.Item(int_fila).Item(10)
                dr.Item(11) = dv.Item(int_fila).Item(11)
                dr.Item(12) = dv.Item(int_fila).Item(12)
            End If
            dt.Rows.Add(dr)
            int_fila = int_fila + 1
        End While

        Return dt
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dt_Alumnos">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     12/03/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaDeudoresBancoLibros(ByVal dt_Alumnos As System.Data.DataTable, ByVal str_CodigoAnio As String, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 7
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim int_contGrado As Integer = 0

        columna = 8

        'Pintado de Titulo
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Value = "CONSOLIDADO DE DEUDORES POR ALUMNO - AÑO ACADÉMICO " & str_CodigoAnio
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Merge()
        oExcel.Range(oCells(4, 5), oCells(4, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Font.Bold = True

        'Pintado de Cabecera estática 

        oCells(7, 2) = "Año Académico"
        oCells(7, 3) = "Grado"
        oCells(7, 4) = "Seccion"
        oCells(7, 5) = "Alumno"
        oCells(7, 6) = "Estado actual"
        oCells(7, 7) = "Cantidad de Libros"
        oCells(7, 8) = "Libros Faltantes"

        oExcel.Range(oCells(7, 2), oCells(7, 8)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 2), oCells(7, 8)).WrapText = True
        oCells(7, 8).ColumnWidth = 35
        oExcel.Range(oCells(7, 2), oCells(7, 8)).Font.Bold = True
        oExcel.Range(oCells(7, 2), oCells(7, 8)).Interior.Color() = RGB(204, 255, 204)

        'Pintado del cuadrado de Cabecera estática 
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(7, 2), oExcel.Cells(7, 7)))
        Dim int_cantidadLibros As Integer = 0

        'int_cantidadLibros = dtLibro.Rows.Count

        'Pintado de detalle
        cont_columnas = 0
        fila = 8

        'While cont_columnas <= dt_Alumnos.Columns.Count - 1
        While cont_filas <= dt_Alumnos.Rows.Count - 1
            oCells(fila + cont_filas, 2) = dt_Alumnos.Rows(cont_filas).Item("AC_Descripcion")
            oCells(fila + cont_filas, 3) = dt_Alumnos.Rows(cont_filas).Item("GD_Descripcion")
            oCells(fila + cont_filas, 4) = dt_Alumnos.Rows(cont_filas).Item("AU_Descripcion")
            oCells(fila + cont_filas, 5) = dt_Alumnos.Rows(cont_filas).Item("NombreCompleto")
            oCells(fila + cont_filas, 6) = dt_Alumnos.Rows(cont_filas).Item("DescEstadoActualAlumno")
            oCells(fila + cont_filas, 7) = dt_Alumnos.Rows(cont_filas).Item("CantidadLibroNoDevueltos")
            oCells(fila + cont_filas, 8) = dt_Alumnos.Rows(cont_filas).Item("librosNoDevueltos")

            oCells(fila + cont_filas, 8).WrapText = True
            'oCells.Range(oCells.Cells(fila + cont_filas, 2), oCells.Cells(fila + cont_filas, 7)).Interior.Color() = RGB(242, 242, 242)
            cont_filas = cont_filas + 1
        End While
        
        oExcel.Range(oCells(7, 2), oCells((dt_Alumnos.Rows.Count + 7), 8)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(7, 2), oCells((cont_filas + 7), 8)))
        cont_filas = 0
        cont_columnas = cont_columnas + 1

        oExcel.ActiveWindow.Zoom = 75

    End Sub

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
    Private Shared Sub LlenarPlantillaDevolucion(ByVal dt_Alumnos As System.Data.DataTable, ByVal dtLibro As System.Data.DataTable, ByVal dtLibrosPrestados As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 7
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim int_contGrado As Integer = 0

        columna = 8

        'Pintado de Titulo
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Value = "CONSOLIDADO DE DEVOLUCIONES POR ALUMNO - AÑO ACADÉMICO " & dt_Alumnos.Rows(0).Item("AC_Descripcion")
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Merge()
        oExcel.Range(oCells(4, 5), oCells(4, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Font.Bold = True

        'Pintado de Leyenda 
        oExcel.Range(oCells(6, 2), oCells(6, 3)).Merge()
        oExcel.Range(oCells(6, 2), oCells(6, 3)).Value = "Leyenda"
        oExcel.Range(oCells(6, 5), oCells(6, 3)).Font.Bold = True
        oExcel.Range(oCells(6, 2), oCells(6, 3)).Interior.Color() = RGB(153, 153, 204)

        'Pintado Detalle de Leyenda 
        'oExcel.Range(oCells(7, 2), oCells(7, 3)).HorizontalAlignment = 3
        oExcel.Range(oCells(7, 2), oCells(7, 2)).Value = "P"
        oExcel.Range(oCells(7, 3), oCells(7, 3)).Value = "Prestado"
        oExcel.Range(oCells(8, 2), oCells(8, 2)).Value = "D"
        oExcel.Range(oCells(8, 3), oCells(8, 3)).Value = "Devuelto"

        oExcel.Range(oCells(7, 2), oCells(8, 3)).Interior.Color() = RGB(255, 250, 240)
        oExcel.Range(oCells(6, 2), oCells(8, 3)).HorizontalAlignment = 3

        'Pintado del cuadrado de la leyenda
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(6, 2), oExcel.Cells(8, 3)))

        'Pintado de Cabecera estática 
        oExcel.Range(oCells(10, 2), oCells(11, 2)).Merge()
        oExcel.Range(oCells(10, 2), oCells(11, 2)).Value = "Año Académico"
        oExcel.Range(oCells(10, 3), oCells(11, 3)).Merge()
        oExcel.Range(oCells(10, 3), oCells(11, 3)).Value = "Grado"
        oExcel.Range(oCells(10, 4), oCells(11, 4)).Merge()
        oExcel.Range(oCells(10, 4), oCells(11, 4)).Value = "Seccion"
        oExcel.Range(oCells(10, 5), oCells(11, 5)).Merge()
        oExcel.Range(oCells(10, 5), oCells(11, 5)).Value = "Alumno"
        oExcel.Range(oCells(10, 6), oCells(11, 6)).Merge()
        oExcel.Range(oCells(10, 6), oCells(11, 6)).Value = "Estado actual"
        oExcel.Range(oCells(10, 7), oCells(11, 7)).Merge()
        oExcel.Range(oCells(10, 7), oCells(11, 7)).Value = "Libros devueltos"
        oExcel.Range(oCells(10, 8), oCells(11, 8)).Merge()
        oExcel.Range(oCells(10, 8), oCells(11, 8)).Value = "Libros Faltantes"

        oExcel.Range(oCells(10, 2), oCells(11, 8)).HorizontalAlignment = 3
        oExcel.Range(oCells(10, 2), oCells(11, 8)).WrapText = True
        oExcel.Range(oCells(10, 2), oCells(11, 8)).Font.Bold = True
        oExcel.Range(oCells(10, 2), oCells(11, 8)).Interior.Color() = RGB(204, 255, 204)

        'Pintado del cuadrado de Cabecera estática 
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(10, 2), oExcel.Cells(11, 8)))
        Dim int_cantidadLibros As Integer = 0

        int_cantidadLibros = dtLibro.Rows.Count

        ' Cabecera Dinámica
        Dim FilaCab As Integer = 10
        Dim ColumnaCab As Integer = 9
        Dim cont As Integer = 0

        While cont <= dtLibro.Rows.Count - 1

            oExcel.Range(oExcel.Cells(FilaCab, ColumnaCab), oExcel.Cells(FilaCab, ColumnaCab + 1)).Merge()
            oExcel.Range(oExcel.Cells(FilaCab, ColumnaCab), oExcel.Cells(FilaCab, ColumnaCab + 1)).HorizontalAlignment = 3
            oExcel.Cells(FilaCab, ColumnaCab) = dtLibro.Rows(cont).Item("Titulo").ToString

            oExcel.Cells(FilaCab + 1, ColumnaCab) = "P"
            oExcel.Range(oExcel.Cells(FilaCab + 1, ColumnaCab), oExcel.Cells(FilaCab + 1, ColumnaCab)).HorizontalAlignment = 3
            oExcel.Range(oExcel.Cells(FilaCab + 1, ColumnaCab), oExcel.Cells(FilaCab + 1, ColumnaCab)).ColumnWidth = 9

            oExcel.Cells(FilaCab + 1, ColumnaCab + 1) = "D"
            oExcel.Range(oExcel.Cells(FilaCab + 1, ColumnaCab + 1), oExcel.Cells(FilaCab + 1, ColumnaCab + 1)).HorizontalAlignment = 3
            oExcel.Range(oExcel.Cells(FilaCab + 1, ColumnaCab + 1), oExcel.Cells(FilaCab + 1, ColumnaCab + 1)).ColumnWidth = 9

            cont = cont + 1
            ColumnaCab = ColumnaCab + 2

        End While

        ' ------------Detalle de Libros del Grado---------------------
        oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).Merge()
        oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).Value = "Detalle de Libros del Grado"
        oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).HorizontalAlignment = 3
        oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).Interior.Color() = RGB(204, 255, 204)
        oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).Font.Bold = True

        'cabecera de titulo
        oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab, ColumnaCab - 1)).Select()
        oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab, ColumnaCab - 1)).Orientation = 90
        oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab, ColumnaCab - 1)).RowHeight = 150
        oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab, ColumnaCab - 1)).WrapText = True
        oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab + 1, ColumnaCab - 1)).Font.Bold = True
        oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab + 1, ColumnaCab - 1)).Interior.Color() = RGB(204, 255, 204)

        'oExcel.Range(oExcel.Cells(FilaCab, 8), oExcel.Cells(FilaCab + 1, ColumnaCab - 1)).Font.Color = RGB(255, 255, 255)

        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(9, 9), oExcel.Cells(FilaCab + 1, ColumnaCab - 1)))

        cont = 0

        ' Detalle Alumnos
        fila = 12
        columna = 2

        ' Detalle de Devoluciones
        Dim filaDet As Integer = 12
        Dim columnnaDet As Integer = 9
        Dim contDv As Integer = 0

        'dv.RowFilter = "CodigoAlumno = '" & str_CodigoAlumno & "'"
        Dim cont_LibDevueltos As Integer = 0
        Dim cont_LibFaltantes As Integer = 0

        While cont <= dt_Alumnos.Rows.Count - 1

            oExcel.Range(oExcel.Cells(fila, columna + 0), oExcel.Cells(fila, columna + 0)).HorizontalAlignment = 3
            oExcel.Range(oExcel.Cells(fila, columna + 1), oExcel.Cells(fila, columna + 1)).HorizontalAlignment = 3

            oExcel.Cells(fila, columna + 0) = dt_Alumnos.Rows(cont).Item("AC_Descripcion").ToString
            oExcel.Cells(fila, columna + 1) = dt_Alumnos.Rows(cont).Item("GD_Descripcion").ToString
            oExcel.Cells(fila, columna + 2) = dt_Alumnos.Rows(cont).Item("AU_Descripcion").ToString
            oExcel.Cells(fila, columna + 3) = dt_Alumnos.Rows(cont).Item("NombreCompleto").ToString
            oExcel.Cells(fila, columna + 4) = dt_Alumnos.Rows(cont).Item("DescEstadoActualAlumno").ToString

            Dim str_CodigoAlumno As String = ""
            Dim int_codigoLibro As Integer = 0
            Dim cont_colLibros As Integer = 0
            str_CodigoAlumno = dt_Alumnos.Rows(cont).Item("AL_CodigoAlumno").ToString

            While cont_colLibros <= dtLibro.Rows.Count - 1 'Recorrido de Libros

                int_codigoLibro = dtLibro.Rows(cont_colLibros).Item("CodigoLibro")

                Dim dv As DataView = dtLibrosPrestados.DefaultView

                dv.RowFilter = "1=1 and CodigoAlumno=" & str_CodigoAlumno & " and CodigoLibro =" & int_codigoLibro.ToString

                If dv.Count > 0 Then 'Existe prestamo 
                    While contDv <= dv.Count - 1
                        If int_codigoLibro = dv.Item(contDv).Item("CodigoLibro") Then

                            If dv(contDv).Item("EstadoPrestamo") = 0 Then
                                oExcel.Cells(fila, columnnaDet) = "Si"
                                oExcel.Cells(fila, columnnaDet + 1) = "No"
                                oExcel.Cells(fila, columnnaDet + 1).Interior.Color() = RGB(255, 3, 13)
                                cont_LibFaltantes = cont_LibFaltantes + 1
                            Else
                                oExcel.Cells(fila, columnnaDet) = "Si"
                                oExcel.Cells(fila, columnnaDet + 1) = "Si"
                                oExcel.Cells(fila, columnnaDet + 1).Interior.Color() = RGB(146, 208, 80)
                                cont_LibDevueltos = cont_LibDevueltos + 1
                            End If
                            'Else
                            '    oExcel.Cells(fila, columnnaDet) = "No"
                            '    oExcel.Cells(fila, columnnaDet + 1) = "No"
                            '    oExcel.Cells(fila, columnnaDet).Interior.Color() = RGB(255, 255, 204)
                            '    oExcel.Cells(fila, columnnaDet + 1).Interior.Color() = RGB(152, 251, 152)
                        End If
                        contDv = contDv + 1
                    End While
                Else ' No hay un prestamo
                    oExcel.Cells(fila, columnnaDet) = "No"
                    oExcel.Cells(fila, columnnaDet + 1) = "No"
                    oExcel.Cells(fila, columnnaDet).Interior.Color() = RGB(255, 246, 143)
                    oExcel.Cells(fila, columnnaDet + 1).Interior.Color() = RGB(255, 246, 143)
                End If

                contDv = 0
                columnnaDet = columnnaDet + 2
                cont_colLibros = cont_colLibros + 1
            End While

            oExcel.Cells(fila, 7) = cont_LibDevueltos
            oExcel.Cells(fila, 8) = cont_LibFaltantes

            cont_LibDevueltos = 0
            cont_LibFaltantes = 0
            columnnaDet = 9
            fila = fila + 1
            cont = cont + 1
        End While

        Dim int_cantidadAlumnos As Integer = dt_Alumnos.Rows.Count

        Dim str_letra As String = ""
        Dim int_fila As Integer
        Dim int_totalColumnas As Integer = 8 + dtLibro.Rows.Count
        Dim int_totalfilas As Integer = int_cantidadAlumnos + 2
        'For i As Integer = 0 To int_totalColumnas  'cantidad de columnas
        '    str_letra = DevLetraColumna(i + 2)
        '    For j As Integer = 0 To int_totalfilas 'cantidad de alumnos
        '        int_fila = 7 + j
        '        oCells.Range(str_letra & int_fila).RowHeight = 21
        '        oCells.Range(str_letra & int_fila).VerticalAlignment = 2 'VerticalAlign.Middle
        '        oCells.Range(str_letra & int_fila).HorizontalAlignment = 3
        '    Next
        '    'oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        'Next
        'oCells.Range("B" & 12 & ":" & DevLetraColumna((dtLibro.Rows.Count * 2) + 8) & (int_cantidadAlumnos + 11)).EntireColumn.AutoFit()
        'oCells.Range("F" & 12 & ":" & "H" & (int_cantidadAlumnos + 11)).HorizontalAlignment = 3

        oExcel.Range(oCells(12, 2), oCells((int_cantidadAlumnos + 11), (dtLibro.Rows.Count * 2) + 8)).EntireColumn.AutoFit()
        oExcel.Range(oCells(12, 2), oCells((int_cantidadAlumnos + 11), 4)).HorizontalAlignment = 3
        oExcel.Range(oCells(12, 6), oCells((int_cantidadAlumnos + 11), (dtLibro.Rows.Count * 2) + 8)).HorizontalAlignment = 3
        cuadradoCompleto(oExcel, oExcel.Range(oCells(12, 2), oCells((int_cantidadAlumnos + 11), (dtLibro.Rows.Count * 2) + 8)))

        'Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("B" & 12 & ":" & DevLetraColumna((dtLibro.Rows.Count * 2) + 8) & (int_cantidadAlumnos + 11))
        'cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub

    'Reporte Dinamico: Cantidad de Libros Vendidos Por Aula   

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     04/07/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteDinamicoCantidadLibrosVendidosPorAula(ByVal dtReporte As System.Data.DataSet, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
        Dim fila As String = ""
        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_Blanco").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        fila = LlenarPlantillaDinamicoCantidadLibrosVendidosPorAula(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinamico"
        oCells = oSheet.Cells
        'Pintado de Fecha 
        oSheet.Range(oCells(4, 3), oCells(4, 7)).Merge()
        oSheet.Range(oCells(4, 3), oCells(4, 7)).HorizontalAlignment = 3
        oSheet.Range(oCells(4, 3), oCells(4, 7)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oSheet.Range(oCells(4, 3), oCells(4, 7)).Font.Bold = True
        oSheet.Range(oCells(10, 7), oCells(fila + 50, 7)).NumberFormat = "S/. #,##0.00"
        Dim int_cont As Integer = 0
        Dim str_DescGrado As String = ""
        'Dim str_DescMonto As String
        Dim dv_Grado As DataView
        dv_Grado = dtReporte.Tables(1).DefaultView

        'int_cont = 0

        'While int_cont <= dtReporte.Tables(0).Rows.Count - 1
        '    str_DescMonto = dtReporte.Tables(0).Rows(int_cont).Item("Monto")
        '    oSheet.PivotTables("Tabla dinámica1").PivotFields("Suma de Monto Total").PivotItems(str_DescMonto).NumberFormat = "$ #,##0.00"
        '    int_cont = int_cont + 1
        'End While

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")
        oSheet.Activate()

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica.PivotCache.SourceData = "PrestamoPorAlumno!F6C2:F" & fila & "C8"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        While int_cont <= dtReporte.Tables(1).Rows.Count - 1
            str_DescGrado = dtReporte.Tables(1).Rows(int_cont).Item("GD_Descripcion")
            oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescGrado).ShowDetail = False
            int_cont = int_cont + 1
        End While

        Dim str_TituloGrado As String = ""

        If int_CodigoGrado = 0 Then
            str_TituloGrado = "Todos los grados "
        Else
            str_TituloGrado = str_DescGrado
        End If

        'Pintado de Año, grado, seccion
        oExcel.Range(oCells(5, 3), oCells(5, 7)).Merge()
        oExcel.Range(oCells(5, 3), oCells(5, 7)).HorizontalAlignment = 3
        oExcel.Range(oCells(5, 3), oCells(5, 7)).Value = str_TituloGrado & "  -  " & dtReporte.Tables(0).Rows(0).Item("AC_Descripcion").ToString
        oExcel.Range(oCells(5, 3), oCells(5, 7)).Font.Bold = True
        'oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        'oSheet.Activate()

        'Dim int_cont As Integer = 0
        'Dim str_DescGrado As String
        'Dim dv_Grado As DataView
        'dv_Grado = dtReporte.Tables(1).DefaultView

        'If int_CodigoGrado = 0 Then
        '    str_DescGrado = "Todos"
        'Else
        '    str_DescGrado = (dv_Grado.RowFilter = "1=1 and GD_CodigoGrado=" & int_CodigoGrado.ToString)
        'End If

        'While int_cont <= dtReporte.Tables(1).Rows.Count - 1
        '    str_DescGrado = dtReporte.Tables(1).Rows(int_cont).Item("GD_Descripcion")
        '    oSheet.PivotTables("Tabla dinámica1").PivotFields("Grado").PivotItems(str_DescGrado).ShowDetail = False
        '    int_cont = int_cont + 1
        'End While

        'With xlSheet.PivotTables("PivotTable1").PivotFields("Convenio")
        '    .Orientation = Excel.XlPivotFieldOrientation.xlPageField
        '    .Position = 1
        'End With

        'objTablaDinamica.AddDataField("Precio Libro", "Cuenta de Precio Libro", Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField)
        'With oSheet.PivotTables("Tabla dinámica1").a
        '    .Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField
        '    .Position = 6
        'End With
        oSheet.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

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
    Private Shared Function LlenarPlantillaDinamicoCantidadLibrosVendidosPorAula(ByVal dtReporte As System.Data.DataSet, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String) As String

        Dim dt_GradoPorLibro As DataTable

        Dim fila As Integer = 7
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""

        dt_GradoPorLibro = dtReporte.Tables(0)
        'dtLibrosPrestados = dtReporte.Tables(1)

        'Pintado de cabecera estatica

        'Pintado de Titulo
        oExcel.Range(oCells(3, 3), oCells(3, 7)).Merge()
        oExcel.Range(oCells(3, 3), oCells(3, 7)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 3), oCells(3, 7)).Value = "ESTADISTICA DE PRESTAMOS DE LIBROS"
        oExcel.Range(oCells(3, 3), oCells(3, 7)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 3), oCells(4, 7)).Merge()
        oExcel.Range(oCells(4, 3), oCells(4, 7)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 3), oCells(4, 7)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(4, 3), oCells(4, 7)).Font.Bold = True

        oCells(6, 2) = "Año Académico"
        oCells(6, 3) = "Nivel"
        oCells(6, 4) = "Grado"
        oCells(6, 5) = "Sección"
        oCells(6, 6) = "Nombre de Libro (Precio)"
        'oCells(6, 7) = "Precio del Libro"
        oCells(6, 7) = "Libros vendidos"
        oCells(6, 8) = "Monto Total"

        oExcel.Range(oCells(6, 2), oCells(6, 8)).HorizontalAlignment = 3
        oExcel.Range(oCells(6, 2), oCells(6, 8)).Interior.Color() = RGB(153, 255, 153)
        oExcel.Range(oCells(6, 2), oCells(6, 8)).Font.Bold = True

        cont_filas = 0

        While cont_filas <= dt_GradoPorLibro.Rows.Count - 1
            oCells(fila, 2) = dt_GradoPorLibro.Rows(cont_filas).Item("AC_Descripcion")
            oCells(fila, 3) = dt_GradoPorLibro.Rows(cont_filas).Item("NV_Descripcion")
            oCells(fila, 4) = dt_GradoPorLibro.Rows(cont_filas).Item("GD_Descripcion")
            oCells(fila, 5) = dt_GradoPorLibro.Rows(cont_filas).Item("AU_Descripcion")
            oCells(fila, 6) = dt_GradoPorLibro.Rows(cont_filas).Item("Titulo")
            oCells(fila, 7) = dt_GradoPorLibro.Rows(cont_filas).Item("Cantidad")
            oCells(fila, 8) = "S/. " & dt_GradoPorLibro.Rows(cont_filas).Item("Monto")
            cont_filas = cont_filas + 1
            fila = fila + 1
        End While
        str_Fila = (fila - 1).ToString
        oExcel.Range(oCells(6, 2), oCells(fila - 1, 8)).EntireColumn.AutoFit()
        cuadradoCompleto(oExcel, oExcel.Range(oCells(6, 2), oCells(fila - 1, 8)))
        oExcel.Range(oCells(6, 2), oCells(fila - 1, 8)).HorizontalAlignment = 3
        oExcel.Range(oCells(6, 2), oCells(fila - 1, 2)).HorizontalAlignment = 2
        oExcel.ActiveWindow.Zoom = 75

        Return str_Fila
    End Function

    'Reporte de Años de Utilidad de Títulos

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Edgar Chang 
    ''' Fecha de Creación:     23/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ BL_USP_REP_AniosUtilidad
    ''' </remarks>
    Public Shared Function ExportarReporteAniosUtilidad(ByVal dtReporte As System.Data.DataSet, ByVal int_PeriodoInicio As Integer, ByVal int_PeriodoFin As Integer, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim dtAsignacionVigenciaTitulo As DataTable
        dtAsignacionVigenciaTitulo = dtReporte.Tables(0)

        Dim dtLibro As DataTable
        Dim dtAnioUtilidad As DataTable
        Dim dtPeriodoInicioFin As DataTable

        dtLibro = dtReporte.Tables(0)
        dtAnioUtilidad = dtReporte.Tables(1)
        dtPeriodoInicioFin = dtReporte.Tables(2)

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Consolidado de Títulos de Libro"
        oCells = oSheet.Cells
        oSheet.Activate()
        LlenarPlantillaAniosUtilidad(dtLibro, dtAnioUtilidad, dtPeriodoInicioFin, oCells, oExcel, str_NombreEntidadReporte)
        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="oCells">Instancia de rango de Periodo Inicio y Fin</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     18/08/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________
    ''' </remarks>
    Private Shared Sub LlenarPlantillaAniosUtilidad(ByVal dtLibro As System.Data.DataTable, ByVal dtAnioUtilidad As System.Data.DataTable, _
                                ByVal dtPeriodoInicioFin As System.Data.DataTable, ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 7
        Dim columna As Integer = 9
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        Dim int_ColumnaAniosUtilidad As Integer = 9
        Dim int_CantPeriodoInicioFin As Integer = dtPeriodoInicioFin.Rows.Count
        Dim int_CantAniosUtilidad As Integer = dtAnioUtilidad.Rows.Count
        Dim int_CeldaAniosUtilidad As Integer = 8

        'Pintado de Titulo
        oExcel.Range(oCells(3, 5), oCells(3, 11)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 11)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 11)).Value = "CONSOLIDADO DE TÍTULO DE LIBROS  "
        oExcel.Range(oCells(3, 5), oCells(3, 11)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(5, 5), oCells(4, 11)).Merge()
        oExcel.Range(oCells(5, 5), oCells(4, 11)).HorizontalAlignment = 3
        oExcel.Range(oCells(5, 5), oCells(4, 11)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(5, 5), oCells(4, 11)).Font.Bold = True

        With oExcel.Range("B7:I7")
            .HorizontalAlignment = 3
            .WrapText = True
            .Font.Bold = True
            .Interior.Color() = RGB(204, 255, 204)
        End With

        With oExcel.Range(oCells(7, 2), oCells(8, 2))
            .Merge()
            .Value = "Título"
            .ColumnWidth = 30
        End With

        oExcel.Range(oCells(7, 3), oCells(8, 3)).Merge()
        oExcel.Range(oCells(7, 3), oCells(8, 3)).Value = "Idioma"
        oExcel.Range(oCells(7, 4), oCells(8, 4)).Merge()
        oExcel.Range(oCells(7, 4), oCells(8, 4)).Value = "Editorial"
        oExcel.Range(oCells(7, 5), oCells(8, 5)).Merge()
        oExcel.Range(oCells(7, 5), oCells(8, 5)).Value = "Autor"
        oExcel.Range(oCells(7, 6), oCells(8, 6)).Merge()
        oExcel.Range(oCells(7, 6), oCells(8, 6)).Value = "Colección"
        oExcel.Range(oCells(7, 7), oCells(8, 7)).Merge()
        oExcel.Range(oCells(7, 7), oCells(8, 7)).Value = "Nivel del Libro"
        oExcel.Range(oCells(7, 8), oCells(8, 8)).Merge()
        oExcel.Range(oCells(7, 8), oCells(8, 8)).Value = "Cantidad de Ejemplares"
        oExcel.Range(oCells(7, 9), oCells(8, 9)).Merge()

        'Pintado del cuadrado de Cabecera estática 
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(7, 2), oExcel.Cells(7, 9)))

        While cont_columnas <= dtPeriodoInicioFin.Rows.Count - 1
            oCells(fila, int_ColumnaAniosUtilidad + cont_columnas) = dtPeriodoInicioFin.Rows(cont_columnas).Item("PeriodoInicioFin")
            With oExcel.Range(oCells(fila, int_ColumnaAniosUtilidad + cont_columnas), oCells(fila + 1, int_ColumnaAniosUtilidad + cont_columnas))
                .Merge()
                .HorizontalAlignment = 3
                .WrapText = True
                .Font.Bold = True
                .Interior.Color() = RGB(204, 255, 204)
            End With
            cont_columnas = cont_columnas + 1
        End While

        cont_columnas = 0

        'Pintado de detalle
        columna = 2
        cont_filas = 0
        cont_columnas = 0
        fila = 9

        Dim int_CodigoLibroDTLibro As Integer
        Dim cont_filasdtAnioUtilidad = 0
        Dim int_CeldaUltimoAnio As Integer = int_CeldaAniosUtilidad + int_CantPeriodoInicioFin

        'Pintado del cuadrado de Cabecera estática 
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(7, int_ColumnaAniosUtilidad), oExcel.Cells(7, int_CeldaUltimoAnio)))
        While cont_filas <= dtLibro.Rows.Count - 1
            While cont_columnas <= dtLibro.Columns.Count - 1
                If cont_columnas < dtLibro.Columns.Count - 1 Then
                    oCells(fila + cont_filas, columna + cont_columnas) = dtLibro.Rows(cont_filas).Item(cont_columnas)
                Else
                    int_CodigoLibroDTLibro = dtLibro.Rows(cont_filas).Item(cont_columnas)

                    Dim dv As DataView = dtAnioUtilidad.DefaultView
                    With dv
                        .RowFilter = "1=1 and LB_CodigoLibro = " & int_CodigoLibroDTLibro
                    End With

                    Dim str_Periodo As String = ""

                    For i As Integer = 0 To int_CantPeriodoInicioFin - 1
                        str_Periodo = oExcel.Range(oCells(7, int_ColumnaAniosUtilidad + i), oCells(7, int_ColumnaAniosUtilidad + i)).Value
                        If existePeriodoLibro(dv, str_Periodo) Then
                            oCells(fila + cont_filas, int_ColumnaAniosUtilidad + i) = "X"
                            With oExcel.Range(oCells(fila + cont_filas, int_ColumnaAniosUtilidad + i), oCells(fila + cont_filas, int_ColumnaAniosUtilidad + i))
                                .Interior.Color() = RGB(204, 255, 204)
                            End With
                            With oExcel.Range(oCells(fila + cont_filas, int_ColumnaAniosUtilidad + i), oCells(fila + cont_filas, int_ColumnaAniosUtilidad + i))
                                .HorizontalAlignment = 3
                                .VerticalAlignment = 2
                                .Font.Bold = True
                            End With
                        End If
                    Next
                End If
                cont_columnas = cont_columnas + 1
            End While
            cont_columnas = 0
            cont_filas = cont_filas + 1
        End While
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(9, 2), oExcel.Cells(9 + dtLibro.Rows.Count, int_CeldaUltimoAnio)))
        oExcel.ActiveWindow.Zoom = 75
    End Sub

#End Region

#Region "Fotos de alumnos"
    Public Shared Function ExportarReporteFotosAlumnos_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String
        ''
        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFotosAlumnosHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaFotosAlumnosHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla

    End Function

    Private Shared Function LlenarPlantillaFotosAlumnosHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""
        Dim plantillaColumna As String = ""
        Dim int_cont As Integer = 0

        plantillaFila = "<tr>" & _
                        "[ColumnaFoto]" & _
                        "</tr>" & _
                        "<tr>" & _
                            "<td colspan='3'><br /></td>" & _
                        "</tr>[ListaFotosAlumnos]"

        plantillaColumna = "<td  style='width :200px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>" & _
                        "<table cellpadding='0' cellspacing='0' border='0' style='width: 200px; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>" & _
                           " <tr><td colspan ='2' style='width :200px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='center' valign='top'>" & _
                                  "<img alt='' src='[RutaFoto]'  width ='100px' height='100px' /> </td></tr>" & _
                            "<tr><td style='padding-left :10px; width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>Codigo : </td>" & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[CodigoAlumno]</td></tr>" & _
                            "<tr><td style='padding-left :10px; width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>Apellidos:</td>" & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[NombreCompleto]</td></tr>" & _
                            "<tr><td style='padding-left :10px; width :20px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>Aula:</td> " & _
                                "<td style='width :180px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' align='left' valign='top'>[GradoAula]</td></tr>" & _
                        "</table> " & _
                     "</td>[ColumnaFoto]"

        'dsReporte.Tables(0).Rows(int_cont).Item("Titulo")
        'dsReporte.Tables(0).Rows(int_cont).Item("Editorial")
        'dsReporte.Tables(0).Rows(int_cont).Item("ISBN")

        If dsReporte.Tables(0).Rows.Count > 0 Then

            While cont_filas <= dsReporte.Tables(0).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaFotosAlumnos]", plantillaFila)

                While cont_columnas < 3

                    If cont_filas + cont_columnas <= dsReporte.Tables(0).Rows.Count - 1 Then
                        Plantilla = Plantilla.Replace("[ColumnaFoto]", plantillaColumna)
                        Plantilla = Plantilla.Replace("[RutaFoto]", ConfigurationManager.AppSettings("RutaFotosUsuarios_Web_Alumn").ToString() & dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("CodigoAlumno").ToString & ".jpg") '& "/" & dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("RutaPortada").ToString)
                        Plantilla = Plantilla.Replace("[CodigoAlumno]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("CodigoAlumno"))
                        Plantilla = Plantilla.Replace("[NombreCompleto]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("NombreCompleto"))
                        Plantilla = Plantilla.Replace("[GradoAula]", dsReporte.Tables(0).Rows(cont_filas + cont_columnas).Item("GradoAula"))

                    End If

                    cont_columnas = cont_columnas + 1
                End While

                Plantilla = Plantilla.Replace("[ColumnaFoto]", "")
                cont_columnas = 0
                cont_filas = cont_filas + 3
            End While
            Plantilla = Plantilla.Replace("[ListaFotosAlumnos]", "")

        Else
            Plantilla = Plantilla.Replace("[ListaFotosAlumnos]", "<tr><td colspan='3' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>")
        End If

        Return Plantilla
    End Function
#End Region

#Region "Incidencias de asistencia"

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     29/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteIncidenciasAsistencia(ByVal dtReporte As DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Dim dtAlumnos As DataTable
        Dim dtIncidencias As DataTable

        dtAlumnos = dtReporte.Tables(0)
        dtIncidencias = dtReporte.Tables(1)

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteIncidenciasAsistencia").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        Dim int_contGrado As Integer = 0

        'If int_CodigoGrado = 0 Then
        '    While int_contGrado <= dtGrado.Rows.Count - 1
        '        oBook = oBooks.Item(1)
        '        oSheets = oBook.Worksheets
        '        oSheet = CType(oSheets.Item(int_contGrado + 1), Microsoft.Office.Interop.Excel.Worksheet)
        '        oSheet.Name = dtGrado.Rows(int_contGrado).Item("GD_Descripcion")
        '        oCells = oSheet.Cells
        '        'oSheet.(dtGrado.Rows(int_contGrado).Item("GD_Descripcion")).Select()
        '        oSheet.Activate()
        '        Dim dt_Al As DataTable
        '        Dim dt_Lib As DataTable
        '        Dim dt_LibPrest As DataTable

        '        dt_Al = LlenarDataTable(dtAlumnos, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))
        '        dt_Lib = LlenarDataTable(dtLibro, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))
        '        dt_LibPrest = LlenarDataTable(dtLibrosPrestados, dtGrado.Rows(int_contGrado).Item("GD_CodigoGrado"))

        '        LlenarPlantillaDevolucion(dt_Al, dt_Lib, dt_LibPrest, oCells, oExcel, str_NombreEntidadReporte)

        '        int_contGrado = int_contGrado + 1
        '    End While
        'Else
        '    Dim int_contHojaExc As Integer = 2

        '    oBook = oBooks.Item(1)
        '    oSheets = oBook.Worksheets

        '    While int_contHojaExc <= 14
        '        oSheet = CType(oSheets.Item(int_contHojaExc), Microsoft.Office.Interop.Excel.Worksheet)
        '        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
        '        int_contHojaExc = int_contHojaExc + 1
        '    End While

        '    oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        '    oSheet.Name = str_NombreEntidadReporte
        '    oCells = oSheet.Cells
        '    'oExcel.Application.Sheets(str_NombreEntidadReporte).Select()
        '    oSheet.Activate()
        '    LlenarPlantillaDevolucion(dtAlumnos, dtLibro, dtLibrosPrestados, oCells, oExcel, str_NombreEntidadReporte)
        'End If

        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaIncidenciasAsistencia(dtAlumnos, dtIncidencias, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     29/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteAsistenciaXBimestreMeses(ByVal dtReporte As DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Dim dtBimestre As DataTable
        Dim dtMes As DataTable
        Dim dtAlumnosDet As DataTable

        dtBimestre = dtReporte.Tables(0)
        dtMes = dtReporte.Tables(1)
        dtAlumnosDet = dtReporte.Tables(2)

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteIncidenciasAsistencia").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        Dim int_contGrado As Integer = 0

        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaAsistenciaXBimestreMeses(dtBimestre, dtMes, dtAlumnosDet, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas 
    ''' Fecha de Creación:     01/06/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteControlAsistenciaXBimestre(ByVal dtReporte As DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Dim dtAlumnos As DataTable
        Dim dtBimestre As DataTable
        Dim dtFecha As DataTable
        Dim dtAlumnosDet As DataTable

        dtAlumnos = dtReporte.Tables(0)
        dtBimestre = dtReporte.Tables(1)
        dtFecha = dtReporte.Tables(2)
        dtAlumnosDet = dtReporte.Tables(3)

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteIncidenciasAsistencia").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        Dim int_contGrado As Integer = 0

        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets

        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaControlAsistenciaXBimestre(dtAlumnos, dtBimestre, dtFecha, dtAlumnosDet, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dt_Alumnos">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     29/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaControlAsistenciaXBimestre(ByVal dtAlumnos As System.Data.DataTable, ByVal dtBimestre As System.Data.DataTable, ByVal dtFecha As System.Data.DataTable, ByVal dtAlumnosDet As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        'Pintado de Titulo
        oExcel.Range(oCells(3, 2), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 2), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 2), oCells(3, 10)).Value = "CONTROL DE ASISTENCIA - AÑO ACADÉMICO " & dtBimestre.Rows(0).Item("Anio")
        oExcel.Range(oCells(3, 2), oCells(3, 10)).Font.Bold = True

        'Pintado de Bimestre 
        oExcel.Range(oCells(4, 2), oCells(4, 10)).Merge()
        oExcel.Range(oCells(4, 2), oCells(4, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 2), oCells(4, 10)).Value = dtBimestre.Rows(0).Item("Bimestre") & " ( " & dtBimestre.Rows(0).Item("FechaInicio") & " - " & dtBimestre.Rows(0).Item("FechaFin") & " ) "
        oExcel.Range(oCells(4, 2), oCells(4, 10)).Font.Bold = True

        'Pintado de Grado 
        oExcel.Range(oCells(5, 2), oCells(5, 10)).Merge()
        oExcel.Range(oCells(5, 2), oCells(5, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(5, 2), oCells(5, 10)).Value = dtBimestre.Rows(0).Item("Grado") & " - " & dtBimestre.Rows(0).Item("Aula")
        oExcel.Range(oCells(5, 2), oCells(5, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(6, 2), oCells(6, 10)).Merge()
        oExcel.Range(oCells(6, 2), oCells(6, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(6, 2), oCells(6, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(6, 2), oCells(6, 10)).Font.Bold = True

        'Pintado de Leyenda 
        oExcel.Range(oCells(8, 1), oCells(8, 2)).Merge()
        oExcel.Range(oCells(8, 1), oCells(8, 2)).Value = "leyenda"

        oExcel.Range(oCells(9, 1), oCells(9, 2)).Merge()
        oExcel.Range(oCells(9, 1), oCells(9, 2)).Value = "P = Present        L = Late       LJ = Late Justified"
        oExcel.Range(oCells(10, 1), oCells(10, 2)).Merge()
        oExcel.Range(oCells(10, 1), oCells(10, 2)).Value = "A = Absent       AJ = Absent Justified"

        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(8, 1), oExcel.Cells(10, 2)))

        'Pintado de Cabecera estática 

        oExcel.Range(oCells(12, 1), oCells(12, 1)).Value = "N°"
        oExcel.Range(oCells(12, 1), oCells(12, 1)).ColumnWidth = 3

        oExcel.Range(oCells(12, 2), oCells(12, 2)).Value = "Nombre Completo"
        oExcel.Range(oCells(12, 2), oCells(12, 2)).ColumnWidth = 35

        'Pintado de cabecera Dinamica
        Dim int_colFecha As Integer = 0
        Dim int_columna As Integer = 3

        While int_colFecha <= dtFecha.Rows.Count - 1

            oCells(12, int_columna + int_colFecha) = dtFecha.Rows(int_colFecha).Item("Strfecha")
            oExcel.Range(oCells(12, int_columna + int_colFecha), oCells(12, int_columna + int_colFecha)).ColumnWidth = 3
            oExcel.Range(oCells(12, int_columna + int_colFecha), oCells(12, int_columna + int_colFecha)).WrapText = True

            int_colFecha = int_colFecha + 1

        End While

        int_columna = 3
        Dim int_filaDet As Integer = 0
        Dim int_contColumDetAlum As Integer = 0
        Dim str_CodigoAlumno As String
        Dim str_fecha As String
        Dim contDv As Integer = 0
        Dim int_fila As Integer = 13
        Dim columna As Integer = 2
        'Dim cont_columnas As Integer = 0

        '    dv.RowFilter = "1=1 and CodigoAlumno=" & str_CodigoAlumno
        While int_filaDet <= dtAlumnos.Rows.Count - 1
            str_CodigoAlumno = dtAlumnos.Rows(int_filaDet).Item("CodigoAlumno").ToString
            oExcel.Cells(int_fila + int_filaDet, 1) = dtAlumnos.Rows(int_filaDet).Item("idx").ToString
            oExcel.Cells(int_fila + int_filaDet, 2) = dtAlumnos.Rows(int_filaDet).Item("NombreAlumno").ToString

            While int_contColumDetAlum <= dtFecha.Rows.Count - 1
                str_fecha = dtFecha.Rows(int_contColumDetAlum).Item("CadFecha").ToString

                Dim dv As DataView = dtAlumnosDet.DefaultView()

                dv.RowFilter = "1=1 and CodigoAlumno=" & str_CodigoAlumno.ToString & " and CadFecha='" & str_fecha.ToString & "'"

                If dv.Count > 0 Then 'Existe detalle 
                    While contDv <= dv.Count - 1
                        oExcel.Cells(int_fila + int_filaDet, int_columna + int_contColumDetAlum) = dv.Item(contDv).Item("TipoEvento")

                        If dv.Item(contDv).Item("TipoEvento") = "A" Then
                            oExcel.Range(oCells(int_fila + int_filaDet, int_columna + int_contColumDetAlum), oCells(int_fila + int_filaDet, int_columna + int_contColumDetAlum)).Interior.Color() = RGB(255, 0, 0)
                        ElseIf dv.Item(contDv).Item("TipoEvento") = "L" Then
                            oExcel.Range(oCells(int_fila + int_filaDet, int_columna + int_contColumDetAlum), oCells(int_fila + int_filaDet, int_columna + int_contColumDetAlum)).Interior.Color() = RGB(153, 153, 204)
                        ElseIf dv.Item(contDv).Item("TipoEvento") = "P" Then
                            oExcel.Range(oCells(int_fila + int_filaDet, int_columna + int_contColumDetAlum), oCells(int_fila + int_filaDet, int_columna + int_contColumDetAlum)).Interior.Color() = RGB(5, 121, 3)

                        End If
                        contDv = contDv + 1
                    End While
                Else 'No Existe detalle 
                    oExcel.Cells(int_fila + int_filaDet, int_columna + int_contColumDetAlum) = ""
                End If
                contDv = 0
                int_contColumDetAlum = int_contColumDetAlum + 1
            End While
            int_contColumDetAlum = 0
            int_filaDet = int_filaDet + 1
        End While
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(12, 1), oExcel.Cells(int_fila + int_filaDet - 1, int_columna + dtFecha.Rows.Count - 1)))
        oExcel.ActiveWindow.Zoom = 75

    End Sub

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     29/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaIncidenciasAsistencia(ByVal dt_Alumnos As System.Data.DataTable, ByVal dtIncidencias As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 10 '7
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0



        'Pintado de Titulo
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Value = "CONSOLIDADO DE INCIDENCIAS "
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Merge()
        oExcel.Range(oCells(4, 5), oCells(4, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Font.Bold = True

        'Pintado de Leyenda 
        oExcel.Range(oCells(6, 1), oCells(6, 2)).Merge()
        oExcel.Range(oCells(6, 1), oCells(6, 2)).Value = "leyenda"

        oExcel.Range(oCells(7, 1), oCells(7, 3)).Merge()
        oExcel.Range(oCells(7, 1), oCells(7, 3)).Value = "A = Absent            L= Late           AJ = Absent Justified            LJ = Late Justified   "

        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(6, 1), oExcel.Cells(7, 2)))

        'Pintado de Cabecera estática 

        oExcel.Range(oCells(9, 1), oCells(9, 1)).Value = "N°"

        oExcel.Range(oCells(9, 2), oCells(9, 2)).Value = "Nombre Completo"

        oExcel.Range(oCells(9, 3), oCells(9, 3)).Value = "Grado"

        oExcel.Range(oCells(9, 4), oCells(9, 4)).Value = "Aula"

        oExcel.Range(oCells(9, 5), oCells(9, 5)).Value = "Fecha de asistencia"

        oExcel.Range(oCells(9, 6), oCells(9, 6)).Value = "Tipo"

        oExcel.Range(oCells(9, 7), oCells(9, 7)).Value = "Motivo"

        oExcel.Range(oCells(9, 8), oCells(9, 8)).Value = "Medio Uso"

        oExcel.Range(oCells(9, 9), oCells(9, 9)).Value = "Observaciones"

        oExcel.Range(oCells(9, 1), oCells(9, 9)).HorizontalAlignment = 3
        'oExcel.Range(oCells(6, 2), oCells(6, 9)).WrapText = True
        oExcel.Range(oCells(9, 1), oCells(9, 9)).Font.Bold = True
        oExcel.Range(oCells(9, 1), oCells(9, 9)).Interior.Color() = RGB(204, 255, 204)
        oExcel.Range(oCells(9, 2), oCells(9, 2)).ColumnWidth = 35
        oExcel.Range(oCells(9, 5), oCells(9, 5)).ColumnWidth = 20
        oExcel.Range(oCells(9, 8), oCells(9, 8)).ColumnWidth = 25
        oExcel.Range(oCells(9, 9), oCells(9, 9)).ColumnWidth = 25

        Dim cont_filas As Integer = 0
        columna = 2
        ' Detalle de Devoluciones
        Dim filaDet As Integer = 15 '12
        Dim columnnaDet As Integer = 9
        Dim contDv As Integer = 0
        Dim cont As Integer = 0
        'dv.RowFilter = "CodigoAlumno = '" & str_CodigoAlumno & "'"
        Dim cont_LibDevueltos As Integer = 0
        Dim cont_LibFaltantes As Integer = 0

        While cont <= dt_Alumnos.Rows.Count - 1

            'oExcel.Range(oExcel.Cells(fila, columna + 0), oExcel.Cells(fila, columna + 0)).HorizontalAlignment = 3
            'oExcel.Range(oExcel.Cells(fila, columna + 1), oExcel.Cells(fila, columna + 1)).HorizontalAlignment = 3

            Dim str_CodigoAlumno As String = ""
            Dim int_codigoLibro As Integer = 0
            Dim cont_colLibros As Integer = 0
            str_CodigoAlumno = dt_Alumnos.Rows(cont).Item("CodigoAlumno").ToString

            Dim dv As DataView = dtIncidencias.DefaultView

            dv.RowFilter = "1=1 and CodigoAlumno=" & str_CodigoAlumno

            If dv.Count > 0 Then 'Existe prestamo 
                While contDv <= dv.Count - 1
                    oExcel.Cells(fila + contDv, columna + 3) = dv.Item(contDv).Item("FechaAsistencia") 'dt_Alumnos.Rows(cont).Item("FechaAsistencia").ToString
                    oExcel.Cells(fila + contDv, columna + 4) = dv.Item(contDv).Item("AbrevTipoEvento")  'dt_Alumnos.Rows(cont).Item("AbrevTipoEvento").ToString
                    oExcel.Cells(fila + contDv, columna + 5) = dv.Item(contDv).Item("Motivo") 'dt_Alumnos.Rows(cont).Item("Motivo").ToString
                    oExcel.Cells(fila + contDv, columna + 6) = dv.Item(contDv).Item("MedioUso") ' dt_Alumnos.Rows(cont).Item("MedioUso").ToString
                    oExcel.Cells(fila + contDv, columna + 7) = dv.Item(contDv).Item("Observacion")  'dt_Alumnos.Rows(cont).Item("Observacion").ToString

                    If dv.Item(contDv).Item("AbrevTipoEvento") = "A" Then
                        oExcel.Range(oCells(fila + contDv, columna + 4), oCells(fila + contDv, columna + 4)).Interior.Color() = RGB(255, 0, 0)
                    ElseIf dv.Item(contDv).Item("AbrevTipoEvento") = "L" Then
                        oExcel.Range(oCells(fila + contDv, columna + 4), oCells(fila + contDv, columna + 4)).Interior.Color() = RGB(153, 153, 204)
                    End If

                    cont_filas = contDv + 1
                    contDv = contDv + 1
                End While
            End If

            oExcel.Range(oExcel.Cells(fila, columna - 1), oExcel.Cells(fila + contDv - 1, columna - 1)).Merge()
            oExcel.Range(oExcel.Cells(fila, columna - 1), oExcel.Cells(fila + contDv - 1, columna - 1)).HorizontalAlignment = 3

            oExcel.Range(oExcel.Cells(fila, columna + 0), oExcel.Cells(fila + contDv - 1, columna + 0)).Merge()
            oExcel.Range(oExcel.Cells(fila, columna + 0), oExcel.Cells(fila + contDv - 1, columna + 0)).HorizontalAlignment = 3

            oExcel.Range(oExcel.Cells(fila, columna + 1), oExcel.Cells(fila + contDv - 1, columna + 1)).Merge()
            oExcel.Range(oExcel.Cells(fila, columna + 1), oExcel.Cells(fila + contDv - 1, columna + 1)).HorizontalAlignment = 3

            oExcel.Range(oExcel.Cells(fila, columna + 2), oExcel.Cells(fila + contDv - 1, columna + 2)).Merge()
            oExcel.Range(oExcel.Cells(fila, columna + 2), oExcel.Cells(fila + contDv - 1, columna + 2)).HorizontalAlignment = 3

            oExcel.Cells(fila, columna - 1) = dt_Alumnos.Rows(cont).Item("IdFila").ToString
            oExcel.Cells(fila, columna + 0) = dt_Alumnos.Rows(cont).Item("NombreCompleto").ToString
            oExcel.Cells(fila, columna + 1) = dt_Alumnos.Rows(cont).Item("Grado").ToString
            oExcel.Cells(fila, columna + 2) = dt_Alumnos.Rows(cont).Item("Aula").ToString

            fila = fila + contDv
            contDv = 0
            cont = cont + 1

        End While
        oExcel.Range(oExcel.Cells(9, 1), oExcel.Cells(fila - 1, 9)).WrapText = True
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(9, 1), oExcel.Cells(fila - 1, 9)))
        oExcel.ActiveWindow.Zoom = 75

    End Sub

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dt_Bimestre">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     29/05/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaAsistenciaXBimestreMeses(ByVal dt_Bimestre As System.Data.DataTable, ByVal dtMes As System.Data.DataTable, ByVal dtAlumnosDetalle As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 7
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0



        'Pintado de Titulo
        oExcel.Range(oCells(3, 2), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 2), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 2), oCells(3, 10)).Value = "CONSOLIDADO DE ATTENDANCE - AÑO ACADÉMICO " & dt_Bimestre.Rows(0).Item("Anio")
        oExcel.Range(oCells(3, 2), oCells(3, 10)).Font.Bold = True

        'Pintado de Bimestre 
        oExcel.Range(oCells(4, 2), oCells(4, 10)).Merge()
        oExcel.Range(oCells(4, 2), oCells(4, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 2), oCells(4, 10)).Value = dt_Bimestre.Rows(0).Item("Bimestre") & " ( " & dt_Bimestre.Rows(0).Item("FechaInicio") & " - " & dt_Bimestre.Rows(0).Item("FechaFin") & " ) "
        oExcel.Range(oCells(4, 2), oCells(4, 10)).Font.Bold = True

        'Pintado de Grado 
        oExcel.Range(oCells(5, 2), oCells(5, 10)).Merge()
        oExcel.Range(oCells(5, 2), oCells(5, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(5, 2), oCells(5, 10)).Value = dt_Bimestre.Rows(0).Item("Grado") & " - " & dt_Bimestre.Rows(0).Item("Aula")
        oExcel.Range(oCells(5, 2), oCells(5, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(6, 2), oCells(6, 10)).Merge()
        oExcel.Range(oCells(6, 2), oCells(6, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(6, 2), oCells(6, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(6, 2), oCells(6, 10)).Font.Bold = True

        'Pintado de Leyenda 
        oExcel.Range(oCells(8, 1), oCells(8, 2)).Merge()
        oExcel.Range(oCells(8, 1), oCells(8, 2)).Value = "leyenda"

        oExcel.Range(oCells(9, 1), oCells(9, 2)).Merge()
        oExcel.Range(oCells(9, 1), oCells(9, 2)).Value = "P = Present        L = Late       LJ = Late Justified"
        oExcel.Range(oCells(10, 1), oCells(10, 2)).Merge()
        oExcel.Range(oCells(10, 1), oCells(10, 2)).Value = "A = Absent       AJ = Absent Justified"

        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(8, 1), oExcel.Cells(10, 2)))

        'Pintado de Cabecera estática 
        oExcel.Range(oCells(12, 1), oCells(14, 1)).Merge()
        oExcel.Range(oCells(12, 1), oCells(14, 1)).Value = "N°"
        oExcel.Range(oCells(12, 1), oCells(14, 1)).ColumnWidth = 3

        oExcel.Range(oCells(12, 2), oCells(14, 2)).Merge()
        oExcel.Range(oCells(12, 2), oCells(14, 2)).Value = "Names"
        oExcel.Range(oCells(12, 2), oCells(14, 2)).ColumnWidth = 35

        oExcel.Range(oCells(12, 3), oCells(12, 22)).Merge()
        oExcel.Range(oCells(12, 3), oCells(12, 22)).Value = "Month"


        'oExcel.Range(oCells(13, 4), oCells(14, 4)).ColumnWidth = 5

        Dim int_contMes As Integer = 0
        Dim int_ColumnaMes As Integer = 3

        While int_contMes <= dtMes.Rows.Count - 1

            oExcel.Range(oCells(13, int_ColumnaMes), oCells(13, int_ColumnaMes + 4)).Merge()
            oExcel.Range(oCells(13, int_ColumnaMes), oCells(13, int_ColumnaMes + 4)).Value = dtMes.Rows(int_contMes).Item("Mes")

            oExcel.Range(oCells(14, int_ColumnaMes), oCells(14, int_ColumnaMes)).Value = "P"
            oExcel.Range(oCells(14, int_ColumnaMes + 1), oCells(14, int_ColumnaMes + 1)).Value = "L"
            oExcel.Range(oCells(14, int_ColumnaMes + 2), oCells(14, int_ColumnaMes + 2)).Value = "LJ"
            oExcel.Range(oCells(14, int_ColumnaMes + 3), oCells(14, int_ColumnaMes + 3)).Value = "A"
            oExcel.Range(oCells(14, int_ColumnaMes + 4), oCells(14, int_ColumnaMes + 4)).Value = "AJ"
            oExcel.Range(oCells(14, int_ColumnaMes), oCells(14, int_ColumnaMes + 4)).ColumnWidth = 3
            int_ColumnaMes = int_ColumnaMes + 5
            int_contMes = int_contMes + 1
        End While
        oExcel.Range(oCells(13, int_ColumnaMes), oCells(13, int_ColumnaMes + 4)).Merge()
        oExcel.Range(oCells(13, int_ColumnaMes), oCells(13, int_ColumnaMes + 4)).Value = "Total"
        oExcel.Range(oCells(14, int_ColumnaMes), oCells(14, int_ColumnaMes)).Value = "P"
        oExcel.Range(oCells(14, int_ColumnaMes + 1), oCells(14, int_ColumnaMes + 1)).Value = "L"
        oExcel.Range(oCells(14, int_ColumnaMes + 2), oCells(14, int_ColumnaMes + 2)).Value = "LJ"
        oExcel.Range(oCells(14, int_ColumnaMes + 3), oCells(14, int_ColumnaMes + 3)).Value = "A"
        oExcel.Range(oCells(14, int_ColumnaMes + 4), oCells(14, int_ColumnaMes + 4)).Value = "AJ"
        oExcel.Range(oCells(14, int_ColumnaMes), oCells(14, int_ColumnaMes + 4)).ColumnWidth = 3

        int_contMes = 0
        Dim int_filaDet As Integer = 15
        Dim int_columnaDet As Integer = 1

        While int_contMes <= dtAlumnosDetalle.Rows.Count - 1

            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet), oCells(int_filaDet + int_contMes, int_columnaDet)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("idx")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 1), oCells(int_filaDet + int_contMes, int_columnaDet + 1)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("NombreAlumno")

            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 2), oCells(int_filaDet + int_contMes, int_columnaDet + 2)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("P1")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 3), oCells(int_filaDet + int_contMes, int_columnaDet + 3)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("L1")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 4), oCells(int_filaDet + int_contMes, int_columnaDet + 4)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("LJ1")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 5), oCells(int_filaDet + int_contMes, int_columnaDet + 5)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("A1")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 6), oCells(int_filaDet + int_contMes, int_columnaDet + 6)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("AJ1")

            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 7), oCells(int_filaDet + int_contMes, int_columnaDet + 7)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("P2")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 8), oCells(int_filaDet + int_contMes, int_columnaDet + 8)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("L2")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 9), oCells(int_filaDet + int_contMes, int_columnaDet + 9)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("LJ2")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 10), oCells(int_filaDet + int_contMes, int_columnaDet + 10)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("A2")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 11), oCells(int_filaDet + int_contMes, int_columnaDet + 11)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("AJ2")

            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 12), oCells(int_filaDet + int_contMes, int_columnaDet + 12)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("P3")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 13), oCells(int_filaDet + int_contMes, int_columnaDet + 13)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("L3")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 14), oCells(int_filaDet + int_contMes, int_columnaDet + 14)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("LJ3")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 15), oCells(int_filaDet + int_contMes, int_columnaDet + 15)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("A3")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 16), oCells(int_filaDet + int_contMes, int_columnaDet + 16)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("AJ3")

            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 17), oCells(int_filaDet + int_contMes, int_columnaDet + 17)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("PTotal")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 18), oCells(int_filaDet + int_contMes, int_columnaDet + 18)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("LTotal")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 19), oCells(int_filaDet + int_contMes, int_columnaDet + 19)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("LJTotal")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 20), oCells(int_filaDet + int_contMes, int_columnaDet + 20)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("ATotal")
            oExcel.Range(oCells(int_filaDet + int_contMes, int_columnaDet + 21), oCells(int_filaDet + int_contMes, int_columnaDet + 21)).Value = dtAlumnosDetalle.Rows(int_contMes).Item("AJTotal")

            int_contMes = int_contMes + 1
        End While

        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(12, 1), oExcel.Cells(int_filaDet + int_contMes - 1, int_columnaDet + 21)))

        oExcel.ActiveWindow.Zoom = 75

    End Sub
#End Region


#Region "Meritos Demeritos"

    Public Shared Function ExportarReporteMeritDemeritoXAlumnoCurso(ByVal dtReporte As DataSet, ByVal int_CodigoAnio As Integer, ByVal str_NombreEntidadReporte As String) As String
        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        Dim dtAlumnos As DataTable
        Dim dtProfesores As DataTable
        Dim dtDetalle As DataTable

        dtAlumnos = dtReporte.Tables(0)
        dtDetalle = dtReporte.Tables(1)
        dtProfesores = dtReporte.Tables(2)

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteMeritDemeritoXAlumnoCurso").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        Dim int_contHojaExc As Integer = 0

        If dtAlumnos.Rows.Count > 1 Then
            While int_contHojaExc <= dtAlumnos.Rows.Count - 1
                oBook = oBooks.Item(1)
                oSheets = oBook.Worksheets
                oSheet = CType(oSheets.Item(int_contHojaExc + 1), Microsoft.Office.Interop.Excel.Worksheet)
                oSheet.Name = dtAlumnos.Rows(int_contHojaExc).Item("CodigoAlumno")
                oCells = oSheet.Cells
                'oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
                oSheet.Activate()

                LlenarPlantillaMeritDemerito(dtAlumnos.Rows(int_contHojaExc).Item("CodigoAlumno"), dtAlumnos.Rows(int_contHojaExc).Item("Alumno"), dtProfesores, dtDetalle, oCells, oExcel, str_NombreEntidadReporte)

                int_contHojaExc = int_contHojaExc + 1
            End While
        ElseIf dtAlumnos.Rows.Count = 1 Then
            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets
            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = dtAlumnos.Rows(0).Item("CodigoAlumno")
            oCells = oSheet.Cells
            'oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
            oSheet.Activate()

            LlenarPlantillaMeritDemerito(dtAlumnos.Rows(0).Item("CodigoAlumno"), dtAlumnos.Rows(0).Item("Alumno"), dtProfesores, dtDetalle, oCells, oExcel, str_NombreEntidadReporte)

        End If


        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    Private Shared Sub LlenarPlantillaMeritDemerito(ByVal str_CodigoAlumno As String, ByVal str_Alumno As String, ByVal dtCursos As System.Data.DataTable, ByVal dtDetalle As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim fila As Integer = 7
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim int_contGrado As Integer = 0

        columna = 8

        'Pintado de Titulo
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Value = "CONSOLIDADO DE DEVOLUCIONES POR ALUMNO - AÑO ACADÉMICO "
        oExcel.Range(oCells(3, 5), oCells(3, 10)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Merge()
        oExcel.Range(oCells(4, 5), oCells(4, 10)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(4, 5), oCells(4, 10)).Font.Bold = True

        'Pintado del nombre de alumno
        oExcel.Range(oCells(6, 2), oCells(6, 3)).Merge()
        oExcel.Range(oCells(6, 2), oCells(6, 3)).Value = "Alumno :   " + str_Alumno

        Dim int_cantCurso As Integer
        Dim int_cantDet As Integer
        Dim int_cantFilaDet As Integer = 1
        Dim str_Profesor As String
        Dim str_NombreCurso As String
        Dim str_Fecha As String
        Dim str_motivo As String
        Dim str_Puntaje As String
        Dim dv As DataView

        Dim int_filaCurso As Integer = 8
        dv = dtDetalle.DefaultView

        While int_cantCurso <= dtCursos.Rows.Count - 1

            dv.RowFilter = "1=1 and CodigoAsignacionGrupo= " & dtCursos.Rows(int_cantCurso).Item("CodigoAsignacionGrupo") & " and CodigoAlumno = " & str_CodigoAlumno

            While int_cantDet <= dv.Count - 1

                If int_cantDet = 0 Then
                    str_NombreCurso = dtCursos.Rows(int_cantCurso).Item("Curso").ToString
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet, 2), oCells(int_filaCurso + int_cantFilaDet, 3)).Merge()
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet, 2), oCells(int_filaCurso + int_cantFilaDet, 3)).Value = str_NombreCurso

                    str_Profesor = dv.Item(0).Item("Profesor")
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 1, 2), oCells(int_filaCurso + int_cantFilaDet + 1, 3)).Merge()
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 1, 2), oCells(int_filaCurso + int_cantFilaDet + 1, 3)).Value = str_Profesor

                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 2), oCells(int_filaCurso + int_cantFilaDet + 2, 2)).Merge()
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 2), oCells(int_filaCurso + int_cantFilaDet + 2, 2)).Value = "Fecha"

                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 3), oCells(int_filaCurso + int_cantFilaDet + 2, 3)).Merge()
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 3), oCells(int_filaCurso + int_cantFilaDet + 2, 3)).Value = "Motivo"

                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 4), oCells(int_filaCurso + int_cantFilaDet + 2, 4)).Merge()
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 4), oCells(int_filaCurso + int_cantFilaDet + 2, 4)).Value = "Puntaje"

                End If
                str_Fecha = dv.Item(int_cantDet).Item("FechaRegistro")
                oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 3, 2), oCells(int_filaCurso + int_cantFilaDet + 3, 2)).Merge()
                oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 3, 2), oCells(int_filaCurso + int_cantFilaDet + 3, 2)).Value = str_Fecha
                oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 3, 2), oCells(int_filaCurso + int_cantFilaDet + 3, 2)).HorizontalAlignment = 3

                str_motivo = dv.Item(int_cantDet).Item("Motivo")
                oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 3, 3), oCells(int_filaCurso + int_cantFilaDet + 3, 3)).Merge()
                oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 3, 3), oCells(int_filaCurso + int_cantFilaDet + 3, 3)).Value = str_motivo
                oExcel.Range(oExcel.Cells(int_filaCurso + int_cantFilaDet + 3, 3), oExcel.Cells(int_filaCurso + int_cantFilaDet + 3, 3)).WrapText = True

                str_Puntaje = dv.Item(int_cantDet).Item("Puntaje")
                oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 3, 4), oCells(int_filaCurso + int_cantFilaDet + 3, 4)).Merge()
                oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 3, 4), oCells(int_filaCurso + int_cantFilaDet + 3, 4)).Value = str_Puntaje

                If int_cantDet = 0 Then
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 2), oCells(int_filaCurso + int_cantFilaDet + 2, 4)).Font.Bold = True
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 2), oCells(int_filaCurso + int_cantFilaDet + 2, 4)).Interior.Color() = RGB(153, 153, 204) 'RGB(255, 250, 240)
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 2), oCells(int_filaCurso + int_cantFilaDet + 2, 4)).HorizontalAlignment = 3
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 3), oCells(int_filaCurso + int_cantFilaDet + 2, 3)).ColumnWidth = 60
                    oExcel.Range(oCells(int_filaCurso + int_cantFilaDet + 2, 2), oCells(int_filaCurso + int_cantFilaDet + 2, 2)).ColumnWidth = 13
                End If
                cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(int_filaCurso + int_cantFilaDet + 2, 2), oExcel.Cells(int_filaCurso + int_cantFilaDet + 3, 4)))

                int_cantDet = int_cantDet + 1
                int_cantFilaDet = int_cantFilaDet + 1


            End While
            If dv.Count > 0 Then
                int_cantFilaDet = int_cantFilaDet + 4
            End If

            'int_filaCurso = int_filaCurso + int_cantDet + 2
            int_cantDet = 0
            'int_filaCurso(+int_cantDet + 2)
            int_cantCurso = int_cantCurso + 1
        End While
        int_cantFilaDet = 0
        int_cantCurso = 0
        'Pintado de Leyenda 
        'oExcel.Range(oCells(6, 2), oCells(6, 3)).Merge()
        'oExcel.Range(oCells(6, 2), oCells(6, 3)).Value = "Leyenda"
        'oExcel.Range(oCells(6, 5), oCells(6, 3)).Font.Bold = True
        'oExcel.Range(oCells(6, 2), oCells(6, 3)).Interior.Color() = RGB(153, 153, 204)

        'Pintado Detalle de Leyenda 
        'oExcel.Range(oCells(7, 2), oCells(7, 3)).HorizontalAlignment = 3
        'oExcel.Range(oCells(7, 2), oCells(7, 2)).Value = "P"
        'oExcel.Range(oCells(7, 3), oCells(7, 3)).Value = "Prestado"
        'oExcel.Range(oCells(8, 2), oCells(8, 2)).Value = "D"
        'oExcel.Range(oCells(8, 3), oCells(8, 3)).Value = "Devuelto"

        'oExcel.Range(oCells(7, 2), oCells(8, 3)).Interior.Color() = RGB(255, 250, 240)
        'oExcel.Range(oCells(6, 2), oCells(8, 3)).HorizontalAlignment = 3

        ''Pintado del cuadrado de la leyenda
        'cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(6, 2), oExcel.Cells(8, 3)))

        'Pintado de Cabecera estática 
        'oExcel.Range(oCells(10, 2), oCells(11, 2)).Merge()
        'oExcel.Range(oCells(10, 2), oCells(11, 2)).Value = "Año Académico"
        'oExcel.Range(oCells(10, 3), oCells(11, 3)).Merge()
        'oExcel.Range(oCells(10, 3), oCells(11, 3)).Value = "Grado"
        'oExcel.Range(oCells(10, 4), oCells(11, 4)).Merge()
        'oExcel.Range(oCells(10, 4), oCells(11, 4)).Value = "Seccion"
        'oExcel.Range(oCells(10, 5), oCells(11, 5)).Merge()
        'oExcel.Range(oCells(10, 5), oCells(11, 5)).Value = "Alumno"
        'oExcel.Range(oCells(10, 6), oCells(11, 6)).Merge()
        'oExcel.Range(oCells(10, 6), oCells(11, 6)).Value = "Estado actual"
        'oExcel.Range(oCells(10, 7), oCells(11, 7)).Merge()
        'oExcel.Range(oCells(10, 7), oCells(11, 7)).Value = "Libros devueltos"
        'oExcel.Range(oCells(10, 8), oCells(11, 8)).Merge()
        'oExcel.Range(oCells(10, 8), oCells(11, 8)).Value = "Libros Faltantes"

        'oExcel.Range(oCells(10, 2), oCells(11, 8)).HorizontalAlignment = 3
        'oExcel.Range(oCells(10, 2), oCells(11, 8)).WrapText = True
        'oExcel.Range(oCells(10, 2), oCells(11, 8)).Font.Bold = True
        'oExcel.Range(oCells(10, 2), oCells(11, 8)).Interior.Color() = RGB(204, 255, 204)

        ''Pintado del cuadrado de Cabecera estática 
        'cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(10, 2), oExcel.Cells(11, 8)))
        'Dim int_cantidadLibros As Integer = 0

        'int_cantidadLibros = dtLibro.Rows.Count

        '' Cabecera Dinámica
        'Dim FilaCab As Integer = 10
        'Dim ColumnaCab As Integer = 9
        'Dim cont As Integer = 0

        'While cont <= dtLibro.Rows.Count - 1

        '    oExcel.Range(oExcel.Cells(FilaCab, ColumnaCab), oExcel.Cells(FilaCab, ColumnaCab + 1)).Merge()
        '    oExcel.Range(oExcel.Cells(FilaCab, ColumnaCab), oExcel.Cells(FilaCab, ColumnaCab + 1)).HorizontalAlignment = 3
        '    oExcel.Cells(FilaCab, ColumnaCab) = dtLibro.Rows(cont).Item("Titulo").ToString

        '    oExcel.Cells(FilaCab + 1, ColumnaCab) = "P"
        '    oExcel.Range(oExcel.Cells(FilaCab + 1, ColumnaCab), oExcel.Cells(FilaCab + 1, ColumnaCab)).HorizontalAlignment = 3
        '    oExcel.Range(oExcel.Cells(FilaCab + 1, ColumnaCab), oExcel.Cells(FilaCab + 1, ColumnaCab)).ColumnWidth = 9

        '    oExcel.Cells(FilaCab + 1, ColumnaCab + 1) = "D"
        '    oExcel.Range(oExcel.Cells(FilaCab + 1, ColumnaCab + 1), oExcel.Cells(FilaCab + 1, ColumnaCab + 1)).HorizontalAlignment = 3
        '    oExcel.Range(oExcel.Cells(FilaCab + 1, ColumnaCab + 1), oExcel.Cells(FilaCab + 1, ColumnaCab + 1)).ColumnWidth = 9

        '    cont = cont + 1
        '    ColumnaCab = ColumnaCab + 2

        'End While

        '' ------------Detalle de Libros del Grado---------------------
        'oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).Merge()
        'oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).Value = "Detalle de Libros del Grado"
        'oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).HorizontalAlignment = 3
        'oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).Interior.Color() = RGB(204, 255, 204)
        'oExcel.Range(oCells(9, 9), oCells(9, ColumnaCab - 1)).Font.Bold = True

        ''cabecera de titulo
        'oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab, ColumnaCab - 1)).Select()
        'oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab, ColumnaCab - 1)).Orientation = 90
        'oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab, ColumnaCab - 1)).RowHeight = 150
        'oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab, ColumnaCab - 1)).WrapText = True
        'oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab + 1, ColumnaCab - 1)).Font.Bold = True
        'oExcel.Range(oExcel.Cells(FilaCab, 9), oExcel.Cells(FilaCab + 1, ColumnaCab - 1)).Interior.Color() = RGB(204, 255, 204)

        ''oExcel.Range(oExcel.Cells(FilaCab, 8), oExcel.Cells(FilaCab + 1, ColumnaCab - 1)).Font.Color = RGB(255, 255, 255)

        'cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(9, 9), oExcel.Cells(FilaCab + 1, ColumnaCab - 1)))

        'cont = 0

        '' Detalle Alumnos
        'fila = 12
        'columna = 2

        '' Detalle de Devoluciones
        'Dim filaDet As Integer = 12
        'Dim columnnaDet As Integer = 9
        'Dim contDv As Integer = 0

        ''dv.RowFilter = "CodigoAlumno = '" & str_CodigoAlumno & "'"
        'Dim cont_LibDevueltos As Integer = 0
        'Dim cont_LibFaltantes As Integer = 0

        'While cont <= dt_Alumnos.Rows.Count - 1

        '    oExcel.Range(oExcel.Cells(fila, columna + 0), oExcel.Cells(fila, columna + 0)).HorizontalAlignment = 3
        '    oExcel.Range(oExcel.Cells(fila, columna + 1), oExcel.Cells(fila, columna + 1)).HorizontalAlignment = 3

        '    oExcel.Cells(fila, columna + 0) = dt_Alumnos.Rows(cont).Item("AC_Descripcion").ToString
        '    oExcel.Cells(fila, columna + 1) = dt_Alumnos.Rows(cont).Item("GD_Descripcion").ToString
        '    oExcel.Cells(fila, columna + 2) = dt_Alumnos.Rows(cont).Item("AU_Descripcion").ToString
        '    oExcel.Cells(fila, columna + 3) = dt_Alumnos.Rows(cont).Item("NombreCompleto").ToString
        '    oExcel.Cells(fila, columna + 4) = dt_Alumnos.Rows(cont).Item("DescEstadoActualAlumno").ToString

        '    Dim str_CodigoAlumno As String = ""
        '    Dim int_codigoLibro As Integer = 0
        '    Dim cont_colLibros As Integer = 0
        '    str_CodigoAlumno = dt_Alumnos.Rows(cont).Item("AL_CodigoAlumno").ToString

        '    While cont_colLibros <= dtLibro.Rows.Count - 1 'Recorrido de Libros

        '        int_codigoLibro = dtLibro.Rows(cont_colLibros).Item("CodigoLibro")

        '        Dim dv As DataView = dtLibrosPrestados.DefaultView

        '        dv.RowFilter = "1=1 and CodigoAlumno=" & str_CodigoAlumno & " and CodigoLibro =" & int_codigoLibro.ToString

        '        If dv.Count > 0 Then 'Existe prestamo 
        '            While contDv <= dv.Count - 1
        '                If int_codigoLibro = dv.Item(contDv).Item("CodigoLibro") Then

        '                    If dv(contDv).Item("EstadoPrestamo") = 0 Then
        '                        oExcel.Cells(fila, columnnaDet) = "Si"
        '                        oExcel.Cells(fila, columnnaDet + 1) = "No"
        '                        oExcel.Cells(fila, columnnaDet + 1).Interior.Color() = RGB(255, 3, 13)
        '                        cont_LibFaltantes = cont_LibFaltantes + 1
        '                    Else
        '                        oExcel.Cells(fila, columnnaDet) = "Si"
        '                        oExcel.Cells(fila, columnnaDet + 1) = "Si"
        '                        oExcel.Cells(fila, columnnaDet + 1).Interior.Color() = RGB(146, 208, 80)
        '                        cont_LibDevueltos = cont_LibDevueltos + 1
        '                    End If
        '                    'Else
        '                    '    oExcel.Cells(fila, columnnaDet) = "No"
        '                    '    oExcel.Cells(fila, columnnaDet + 1) = "No"
        '                    '    oExcel.Cells(fila, columnnaDet).Interior.Color() = RGB(255, 255, 204)
        '                    '    oExcel.Cells(fila, columnnaDet + 1).Interior.Color() = RGB(152, 251, 152)
        '                End If
        '                contDv = contDv + 1
        '            End While
        '        Else ' No hay un prestamo
        '            oExcel.Cells(fila, columnnaDet) = "No"
        '            oExcel.Cells(fila, columnnaDet + 1) = "No"
        '            oExcel.Cells(fila, columnnaDet).Interior.Color() = RGB(255, 246, 143)
        '            oExcel.Cells(fila, columnnaDet + 1).Interior.Color() = RGB(255, 246, 143)
        '        End If

        '        contDv = 0
        '        columnnaDet = columnnaDet + 2
        '        cont_colLibros = cont_colLibros + 1
        '    End While

        '    oExcel.Cells(fila, 7) = cont_LibDevueltos
        '    oExcel.Cells(fila, 8) = cont_LibFaltantes

        '    cont_LibDevueltos = 0
        '    cont_LibFaltantes = 0
        '    columnnaDet = 9
        '    fila = fila + 1
        '    cont = cont + 1
        'End While

        'Dim int_cantidadAlumnos As Integer = dt_Alumnos.Rows.Count

        'Dim str_letra As String = ""
        'Dim int_fila As Integer
        'Dim int_totalColumnas As Integer = 8 + dtLibro.Rows.Count
        'Dim int_totalfilas As Integer = int_cantidadAlumnos + 2
        ''For i As Integer = 0 To int_totalColumnas  'cantidad de columnas
        ''    str_letra = DevLetraColumna(i + 2)
        ''    For j As Integer = 0 To int_totalfilas 'cantidad de alumnos
        ''        int_fila = 7 + j
        ''        oCells.Range(str_letra & int_fila).RowHeight = 21
        ''        oCells.Range(str_letra & int_fila).VerticalAlignment = 2 'VerticalAlign.Middle
        ''        oCells.Range(str_letra & int_fila).HorizontalAlignment = 3
        ''    Next
        ''    'oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        ''Next
        ''oCells.Range("B" & 12 & ":" & DevLetraColumna((dtLibro.Rows.Count * 2) + 8) & (int_cantidadAlumnos + 11)).EntireColumn.AutoFit()
        ''oCells.Range("F" & 12 & ":" & "H" & (int_cantidadAlumnos + 11)).HorizontalAlignment = 3

        'oExcel.Range(oCells(12, 2), oCells((int_cantidadAlumnos + 11), (dtLibro.Rows.Count * 2) + 8)).EntireColumn.AutoFit()
        'oExcel.Range(oCells(12, 2), oCells((int_cantidadAlumnos + 11), 4)).HorizontalAlignment = 3
        'oExcel.Range(oCells(12, 6), oCells((int_cantidadAlumnos + 11), (dtLibro.Rows.Count * 2) + 8)).HorizontalAlignment = 3
        'cuadradoCompleto(oExcel, oExcel.Range(oCells(12, 2), oCells((int_cantidadAlumnos + 11), (dtLibro.Rows.Count * 2) + 8)))

        ''Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("B" & 12 & ":" & DevLetraColumna((dtLibro.Rows.Count * 2) + 8) & (int_cantidadAlumnos + 11))
        ''cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub
#End Region

#Region "Módulo de Pensiones"

#Region "Descarga de Pagos Por Banco"

    Public Shared Function ExportarReportePagosPorDescargaDeBanco(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application

        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks
        Dim oBook As Microsoft.Office.Interop.Excel.Workbook

        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets
        Dim oSheet1, oSheet2, oSheet3 As Microsoft.Office.Interop.Excel.Worksheet

        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        'Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets

        'Pagos Actualizados
        oSheet1 = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet1.Activate()
        oSheet1.Name = dsReporte.Tables(0).TableName
        oCells = oSheet1.Cells
        LlenarPlantillaPagosActualizadosPorDescargaBanco(dsReporte.Tables(0), oCells, oExcel, dsReporte.Tables(0).TableName) 'str_NombreEntidadReporte)

        'Pagos No Actualizados
        oSheet2 = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet2.Activate()
        oSheet2.Name = dsReporte.Tables(1).TableName
        oCells = oSheet2.Cells
        LlenarPlantillaPagosNoActualizadosPorDescargaBanco(dsReporte.Tables(1), oCells, oExcel, dsReporte.Tables(1).TableName) 'str_NombreEntidadReporte)


        oSheet3 = CType(oSheets.Item(3), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet3.Activate()
        oSheet3.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVeryHidden


        oSheet1.Activate()

        oSheet1.SaveAs(sFile)
        oSheet2.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet1)
        ReleaseComObject(oSheet2)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet1 = Nothing
        oSheet2 = Nothing
        oSheet3 = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    Private Shared Sub LlenarPlantillaPagosActualizadosPorDescargaBanco(ByVal dtReporte As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim dt_reporte As New DataTable
        Dim fila As Integer = 8
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_reporte = dtReporte

        'Pintado de cabecera
        While cont_columnas <= dt_reporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dt_reporte.Columns(cont_columnas).ColumnName

            With oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .HorizontalAlignment = 3

                If cont_columnas = 0 Then ' N°
                    .ColumnWidth = 8
                ElseIf cont_columnas = 1 Then ' Código
                    .ColumnWidth = 14
                ElseIf cont_columnas = 2 Then ' Nombre Completo
                    .ColumnWidth = 45
                ElseIf cont_columnas = 3 Then ' Concepto
                    .ColumnWidth = 30
                ElseIf cont_columnas = 4 Then ' Talonario
                    .ColumnWidth = 18
                ElseIf cont_columnas = 5 Then ' N° Pago
                    .ColumnWidth = 14
                ElseIf cont_columnas = 6 Or cont_columnas = 7 Then ' Fec Pago -	Fec Vnct
                    .ColumnWidth = 12
                ElseIf cont_columnas = 8 Then ' Moneda
                    .ColumnWidth = 8
                ElseIf cont_columnas = 9 Then ' Monto
                    .ColumnWidth = 14
                ElseIf cont_columnas = 10 Then ' Documento de Referencia
                    .ColumnWidth = 30
                End If

            End With

            cont_columnas = cont_columnas + 1
        End While

        'Pintado de detalle
        cont_columnas = 0
        fila = 9

        While cont_columnas <= dt_reporte.Columns.Count - 1
            While cont_filas <= dt_reporte.Rows.Count - 1

                oCells(fila + cont_filas, columna + cont_columnas) = dt_reporte.Rows(cont_filas).Item(cont_columnas)

                Dim s As String = dt_reporte.Rows(cont_filas).Item(cont_columnas)

                If Datos.isDecimal(s) Then
                    oCells(fila + cont_filas, columna + cont_columnas).numberformat = "###,##0.00"
                End If

                With oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas))

                    If cont_columnas = 0 Then ' N°
                        .ColumnWidth = 8
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 1 Then ' Código
                        .ColumnWidth = 14
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 2 Then ' Nombre Completo
                        .ColumnWidth = 45
                        .HorizontalAlignment = int_HA_Left
                    ElseIf cont_columnas = 3 Then ' Concepto
                        .ColumnWidth = 30
                        .HorizontalAlignment = int_HA_Left
                    ElseIf cont_columnas = 4 Then ' Talonario
                        .ColumnWidth = 18
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 5 Then ' N° Pago
                        .ColumnWidth = 14
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 6 Or cont_columnas = 7 Then ' Fec Pago -	Fec Vnct
                        .ColumnWidth = 12
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 8 Then ' Moneda
                        .ColumnWidth = 8
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 9 Then ' Monto
                        .ColumnWidth = 14
                        .HorizontalAlignment = int_HA_Right
                    ElseIf cont_columnas = 10 Then ' Documento de Referencia
                        .ColumnWidth = 40
                        .HorizontalAlignment = int_HA_Left
                    End If

                End With

                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).Interior.Color() = RGB(242, 242, 242)
                cont_filas = cont_filas + 1

            End While

            cont_filas = 0
            cont_columnas = cont_columnas + 1

        End While

        Dim str_letra As String = ""
        Dim int_fila As Integer

        For i As Integer = 0 To dt_reporte.Columns.Count
            str_letra = DevLetraColumna(i + 4)
            For j As Integer = 0 To dt_reporte.Rows.Count
                int_fila = 9 + j
                oCells.Range(str_letra & int_fila).RowHeight = 21
                oCells.Range(str_letra & int_fila).VerticalAlignment = int_VA_Middle
            Next
            'oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        Next

        Dim total_columnas As Integer = dt_reporte.Columns.Count
        Dim ultimaPosicion As Integer = DevIDColumna("E") + total_columnas - 2 ' (idx + E)

        oCells(3, 5) = "Relación de " & str_NombreEntidadReporte
        oCells.Range("E3:E3").Font.Size = 25
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = int_HA_Center


        Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("D" & 8 & ":" & DevLetraColumna(dt_reporte.Columns.Count + 3) & (fila + dt_reporte.Rows.Count - 1))
        cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub

    Private Shared Sub LlenarPlantillaPagosNoActualizadosPorDescargaBanco(ByVal dtReporte As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim dt_reporte As New DataTable
        Dim fila As Integer = 8
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_reporte = dtReporte

        'Pintado de cabecera
        While cont_columnas <= dt_reporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dt_reporte.Columns(cont_columnas).ColumnName

            With oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas))
                .Font.Bold = True
                .Interior.Color() = RGB(149, 179, 215)
                .Font.Color = RGB(0, 0, 0)
                .HorizontalAlignment = 3

                If cont_columnas = 0 Then ' N°
                    .ColumnWidth = 8
                ElseIf cont_columnas = 1 Then ' Código
                    .ColumnWidth = 14
                ElseIf cont_columnas = 2 Then ' Nombre Completo
                    .ColumnWidth = 45
                ElseIf cont_columnas = 3 Then ' Concepto
                    .ColumnWidth = 20
                ElseIf cont_columnas = 4 Or cont_columnas = 5 Then ' Fec Pago -	Fec Vnct
                    .ColumnWidth = 12
                ElseIf cont_columnas = 6 Then ' Moneda
                    .ColumnWidth = 8
                ElseIf cont_columnas = 7 Then ' Monto
                    .ColumnWidth = 14
                ElseIf cont_columnas = 8 Then ' Mora
                    .ColumnWidth = 14
                ElseIf cont_columnas = 9 Then ' Total
                    .ColumnWidth = 14
                ElseIf cont_columnas = 10 Then ' Motivo
                    .ColumnWidth = 30
                End If

            End With

            cont_columnas = cont_columnas + 1
        End While

        'Pintado de detalle
        cont_columnas = 0
        fila = 9

        While cont_columnas <= dt_reporte.Columns.Count - 1
            While cont_filas <= dt_reporte.Rows.Count - 1

                oCells(fila + cont_filas, columna + cont_columnas) = dt_reporte.Rows(cont_filas).Item(cont_columnas)

                Dim s As String = dt_reporte.Rows(cont_filas).Item(cont_columnas) '"ff.00"

                If Datos.isDecimal(s) Then
                    oCells(fila + cont_filas, columna + cont_columnas).numberformat = "###,##0.00"
                End If

                With oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas))

                    If cont_columnas = 0 Then ' N°
                        .ColumnWidth = 8
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 1 Then ' Código
                        .ColumnWidth = 14
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 2 Then ' Nombre Completo
                        .ColumnWidth = 45
                        .HorizontalAlignment = int_HA_Left
                    ElseIf cont_columnas = 3 Then ' Concepto
                        .ColumnWidth = 20
                        .HorizontalAlignment = int_HA_Left
                    ElseIf cont_columnas = 4 Or cont_columnas = 5 Then ' Fec Pago -	Fec Vnct
                        .ColumnWidth = 12
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 6 Then ' Moneda
                        .ColumnWidth = 8
                        .HorizontalAlignment = int_HA_Center
                    ElseIf cont_columnas = 7 Then ' Monto
                        .ColumnWidth = 14
                        .HorizontalAlignment = int_HA_Right
                    ElseIf cont_columnas = 8 Then ' Mora
                        .ColumnWidth = 14
                        .HorizontalAlignment = int_HA_Right
                    ElseIf cont_columnas = 9 Then ' Total
                        .ColumnWidth = 14
                        .HorizontalAlignment = int_HA_Right
                    ElseIf cont_columnas = 10 Then ' Motivo
                        .HorizontalAlignment = int_HA_Left
                        .EntireColumn.AutoFit()
                    End If

                End With

                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).Interior.Color() = RGB(242, 242, 242)
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While



        Dim str_letra As String = ""
        Dim int_fila As Integer

        For i As Integer = 0 To dt_reporte.Columns.Count
            str_letra = DevLetraColumna(i + 4)
            For j As Integer = 0 To dt_reporte.Rows.Count
                int_fila = 9 + j
                oCells.Range(str_letra & int_fila).RowHeight = 21
                oCells.Range(str_letra & int_fila).VerticalAlignment = int_VA_Middle
            Next
            'oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        Next

        Dim total_columnas As Integer = dt_reporte.Columns.Count
        Dim ultimaPosicion As Integer = DevIDColumna("E") + total_columnas - 2 ' (idx + E)

        oCells(3, 5) = "Relación de " & str_NombreEntidadReporte
        oCells.Range("E3:E3").Font.Size = 25
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = int_HA_Center


        Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("D" & 8 & ":" & DevLetraColumna(dt_reporte.Columns.Count + 3) & (fila + dt_reporte.Rows.Count - 1))
        cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub

#End Region

#Region "Generación de Letras"

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtLetras">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     04/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteLetras(ByVal dtLetras As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application

        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks
        Dim oBook As Microsoft.Office.Interop.Excel.Workbook

        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets
        Dim oSheet As Microsoft.Office.Interop.Excel.Worksheet

        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaPagoLetra").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        'Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets

        Dim str_NombreHoja As String = ""

        For i As Integer = 0 To dtLetras.Rows.Count - 1
            str_NombreHoja = dtLetras.Rows(i).Item("HojaLetra")
            oSheet = CType(oSheets.Item(i + 1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Activate()
            oSheet.Name = str_NombreHoja
            oCells = oSheet.Cells
            LlenarPlantillaLetras(dtLetras.Rows(i), oCells, oExcel, str_NombreHoja)

            oSheet.SaveAs(sFile)
            ReleaseComObject(oCells)
            ReleaseComObject(oSheet)
        Next

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        'ReleaseComObject(oCells)
        'ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dr">DataRow con los datos de la letra a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     04/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaLetras(ByVal dr As System.Data.DataRow, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        oCells(1, DevIDColumna("B")) = dr("NombreFamiliar")
        oCells(1, DevIDColumna("C")) = dr("DocumentoFamiliar")

        oCells.Range("D7").NumberFormat = "@"
        oCells(7, DevIDColumna("D")) = dr("NumeroLetra").ToString

        oCells(7, DevIDColumna("H")) = dr("FechaGiro")
        oCells(7, DevIDColumna("J")) = dr("LugarGiro") ' I
        oCells(7, DevIDColumna("K")) = dr("FechaVencimiento") ' J
        oCells(7, DevIDColumna("M")) = dr("MontoTotal") ' K
        oCells(10, DevIDColumna("E")) = dr("MontoTotalTexto")
        oCells(13, DevIDColumna("E")) = dr("NombreFamiliar")
        oCells(15, DevIDColumna("E")) = dr("DomicilioFamiliar") ' F
        oCells(17, DevIDColumna("H")) = dr("DistritoFamiliar") ' 16
        oCells(18, DevIDColumna("E")) = dr("DocumentoFamiliar") ' 17
        oCells(18, DevIDColumna("H")) = dr("TelefonoFamiliar") ' 17 G
        oCells(21, DevIDColumna("K")) = dr("Representante1")
        oCells(22, DevIDColumna("K")) = dr("DocumentoRepresentante1")
        oCells(21, DevIDColumna("M")) = dr("Representante2")
        oCells(22, DevIDColumna("M")) = dr("DocumentoRepresentante2")

        oExcel.ActiveWindow.Zoom = 75

    End Sub

#End Region

#Region "Cartas de Morosidad"

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     14/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteSinFormato(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcelSinFormato").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = str_NombreEntidadReporte
        oCells = oSheet.Cells

        LlenarPlantillaSinFormato(dtReporte, oCells, oExcel, str_NombreEntidadReporte)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     14/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaSinFormato(ByVal dtReporte As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim dt_reporte As New DataTable
        Dim fila As Integer = 8
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_reporte = dtReporte

        'Pintado de cabecera
        While cont_columnas <= dt_reporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dt_reporte.Columns(cont_columnas).ColumnName
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Bold = True
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).HorizontalAlignment = 3

            cont_columnas = cont_columnas + 1
        End While

        'Pintado de detalle
        cont_columnas = 0
        fila = 9

        While cont_columnas <= dt_reporte.Columns.Count - 1
            While cont_filas <= dt_reporte.Rows.Count - 1
                oCells(fila + cont_filas, columna + cont_columnas) = dt_reporte.Rows(cont_filas).Item(cont_columnas)
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While



        Dim str_letra As String = ""
        Dim int_fila As Integer

        For i As Integer = 0 To dt_reporte.Columns.Count
            str_letra = DevLetraColumna(i + 4)
            For j As Integer = 0 To dt_reporte.Rows.Count
                int_fila = 9 + j
                oCells.Range(str_letra & int_fila).RowHeight = 21
                oCells.Range(str_letra & int_fila).VerticalAlignment = 2 'VerticalAlign.Middle
            Next
            oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        Next

        Dim total_columnas As Integer = dt_reporte.Columns.Count
        Dim ultimaPosicion As Integer = DevIDColumna("E") + total_columnas - 2 ' (idx + E)

        oCells(3, 5) = "Relación de " & str_NombreEntidadReporte
        oCells.Range("E3:E3").Font.Size = 15
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = 3 'HorizontalAlign.Right


        Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("D" & 8 & ":" & DevLetraColumna(dt_reporte.Columns.Count + 3) & (fila + dt_reporte.Rows.Count - 1))
        cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub

#End Region

#Region "Emisión de Boletas en Lote"

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dsReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     20/05/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteMultiHojas(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application

        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks
        Dim oBook As Microsoft.Office.Interop.Excel.Workbook

        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets
        Dim oSheet1, oSheet2 As Microsoft.Office.Interop.Excel.Worksheet

        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        'Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets

        oSheet1 = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet1.Activate()
        oSheet1.Name = dsReporte.Tables(0).TableName
        oCells = oSheet1.Cells
        LlenarPlantillaPagos(dsReporte.Tables(0), oCells, oExcel, dsReporte.Tables(0).TableName) 'str_NombreEntidadReporte)

        oSheet2 = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet2.Activate()
        oSheet2.Name = dsReporte.Tables(1).TableName
        oCells = oSheet2.Cells
        LlenarPlantillaPagos(dsReporte.Tables(1), oCells, oExcel, dsReporte.Tables(1).TableName) 'str_NombreEntidadReporte)

        oSheet1.SaveAs(sFile)
        oSheet2.SaveAs(sFile)

        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet1)
        ReleaseComObject(oSheet2)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet1 = Nothing
        oSheet2 = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con los datos a exportar</param>
    ''' <param name="oCells">Instancia de rango de documento a setear datos</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     26/05/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Sub LlenarPlantillaPagos(ByVal dtReporte As System.Data.DataTable, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                ByVal str_NombreEntidadReporte As String)

        Dim dt_reporte As New DataTable
        Dim fila As Integer = 8
        Dim columna As Integer = 4
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_reporte = dtReporte

        'Pintado de cabecera
        While cont_columnas <= dt_reporte.Columns.Count - 1
            oCells(fila, columna + cont_columnas) = dt_reporte.Columns(cont_columnas).ColumnName
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Bold = True
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Interior.Color() = RGB(149, 179, 215)
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).Font.Color = RGB(0, 0, 0)
            oCells.Range(oCells.Cells(fila, columna + cont_columnas), oCells.Cells(fila, columna + cont_columnas)).HorizontalAlignment = 3

            cont_columnas = cont_columnas + 1
        End While

        'Pintado de detalle
        cont_columnas = 0
        fila = 9

        While cont_columnas <= dt_reporte.Columns.Count - 1
            While cont_filas <= dt_reporte.Rows.Count - 1

                oCells(fila + cont_filas, columna + cont_columnas) = dt_reporte.Rows(cont_filas).Item(cont_columnas)

                Dim s As String = dt_reporte.Rows(cont_filas).Item(cont_columnas) '"ff.00"

                If Datos.isDecimal(s) Then
                    oCells(fila + cont_filas, columna + cont_columnas).numberformat = "###,##0.00"
                End If

                If cont_filas = dt_reporte.Rows.Count - 1 Then
                    With oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas))
                        .EntireColumn.AutoFit()
                    End With
                End If

                'oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).WrapText = True
                oCells.Range(oCells.Cells(fila + cont_filas, columna + cont_columnas), oCells.Cells(fila + cont_filas, columna + cont_columnas)).Interior.Color() = RGB(242, 242, 242)
                cont_filas = cont_filas + 1
            End While
            cont_filas = 0
            cont_columnas = cont_columnas + 1
        End While



        Dim str_letra As String = ""
        Dim int_fila As Integer

        For i As Integer = 0 To dt_reporte.Columns.Count
            str_letra = DevLetraColumna(i + 4)
            For j As Integer = 0 To dt_reporte.Rows.Count
                int_fila = 9 + j
                oCells.Range(str_letra & int_fila).RowHeight = 21
                oCells.Range(str_letra & int_fila).VerticalAlignment = 2 'VerticalAlign.Middle
            Next
            oCells.Range(str_letra & 9.ToString).EntireColumn.AutoFit()
        Next

        Dim total_columnas As Integer = dt_reporte.Columns.Count
        Dim ultimaPosicion As Integer = DevIDColumna("E") + total_columnas - 2 ' (idx + E)

        oCells(3, 5) = "Relación de " & str_NombreEntidadReporte
        oCells.Range("E3:E3").Font.Size = 25
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").Merge()
        oCells.Range("E3:" & DevLetraColumna(ultimaPosicion) & "3").HorizontalAlignment = 3 'HorizontalAlign.Right


        Dim objCelda As Microsoft.Office.Interop.Excel.Range = oCells.Range("D" & 8 & ":" & DevLetraColumna(dt_reporte.Columns.Count + 3) & (fila + dt_reporte.Rows.Count - 1))
        cuadradoCompleto(oExcel, objCelda)

        oExcel.ActiveWindow.Zoom = 75

    End Sub

#End Region

#End Region

#Region "Módulo de Matrícula"
#Region "Ficha de Alumnos"

    'FICHA UNICA DE MATRICULA
    Public Shared Function Exportar_FichaUnicaMatricula(ByVal ds_Reporte As System.Data.DataSet) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim str_NombreEntidadReporte As String = ""

        nombreRep = GetNewName()

        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaFichaUnicaMatricula").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oCells = oSheet.Cells
        oSheet.Activate()

        LlenarPlantilla_FichaUnicaMatricula_Hoja1(ds_Reporte, oCells, oExcel)

        oSheet = oBook.Worksheets(2)
        oCells = oSheet.Cells
        oSheet.Activate()

        LlenarPlantilla_FichaUnicaMatricula_Hoja2(ds_Reporte, oCells, oExcel)

        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    Private Shared Sub LlenarPlantilla_FichaUnicaMatricula_Hoja1(ByVal ds_Reporte As System.Data.DataSet, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application)

        Dim dt_DatosGenerales As New DataTable
        Dim dt_Idiomas As New DataTable
        Dim dt_ControlesSalud As New DataTable
        Dim dt_OtrosControles As New DataTable
        Dim dt_Enfermedades As New DataTable
        Dim dt_Alergias As New DataTable
        Dim dt_Padres As New DataTable

        Dim fila As Integer = 10
        Dim columna As Integer = 45
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0

        dt_DatosGenerales = ds_Reporte.Tables(0)
        dt_Idiomas = ds_Reporte.Tables(1)
        dt_ControlesSalud = ds_Reporte.Tables(2)
        dt_OtrosControles = ds_Reporte.Tables(3)
        dt_Enfermedades = ds_Reporte.Tables(4)
        dt_Alergias = ds_Reporte.Tables(5)
        dt_Padres = ds_Reporte.Tables(6)
        '---------------------------------------------
        'PASO 1: CODIGO DEL EDUCANDO
        '---------------------------------------------
        Dim str_CodigoEducando As String = dt_DatosGenerales.Rows(0).Item("CodigoEducando")
        Dim int_ContCodigoEducando As Integer = 0

        While int_ContCodigoEducando <= str_CodigoEducando.Length - 1
            oCells(fila, columna + int_ContCodigoEducando) = str_CodigoEducando.Chars(int_ContCodigoEducando).ToString
            int_ContCodigoEducando = int_ContCodigoEducando + 1
        End While

        '---------------------------------------------
        'PASO 2: DATOS PERSONALES
        '---------------------------------------------
        fila = 13

        'a) Apellido Paterno
        columna = 1
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("ApePaterno")

        'b) Apellido Materno
        columna = 9
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("ApeMaterno")

        'c) Nombres
        columna = 18
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("Nombre")

        'd) Sexo
        Dim bool_Sexo As Boolean = dt_DatosGenerales.Rows(0).Item("CodigoSexo")
        If bool_Sexo = True Then
            columna = 30
            oCells(fila, columna) = "X"
        Else
            columna = 32
            oCells(fila, columna) = "X"
        End If

        'e) Nacimiento Registrado
        Dim bool_NacimientoReg As Boolean = dt_DatosGenerales.Rows(0).Item("CodigoNacimientoRegistrado")
        If bool_NacimientoReg = True Then
            columna = 40
            oCells(fila, columna) = "X"
        Else
            columna = 42
            oCells(fila, columna) = "X"
        End If

        'f) Fecha de Nacimiento
        fila = 17
        columna = 7
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DiaNacimiento")
        columna = 9
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("MesNacimiento")
        columna = 12
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("AnioNacimiento")

        'g) Pais Nacimiento
        fila = 18
        columna = 7
        Dim int_CodigoPais As Integer = dt_DatosGenerales.Rows(0).Item("CodigoPais")
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("Pais")

        If int_CodigoPais = 1 Then 'PAIS PERU
            'h) Departamento Nacimiento
            fila = 19
            oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DepartamentoNacimiento")

            'i) Provincia Nacimiento
            fila = 20
            oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("ProvinciaNacimiento")

            'j) Distrito Nacimiento
            fila = 21
            oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DistritoNacimiento")
        End If

        'k) Documento de Identidad
        Dim int_TipoDocIdentidad As Integer = 0
        int_TipoDocIdentidad = dt_DatosGenerales.Rows(0).Item("CodigoTipoDocIdentidad")
        fila = 22

        If int_TipoDocIdentidad = 1 Then 'DNI
            columna = 9
            oCells(fila, columna) = "X"
            columna = 21
            oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("NumeroDocIdentidad")
        End If

        If int_TipoDocIdentidad = 2 Then 'Carnet de Extrangeria
            columna = 14
            oCells(fila, columna) = "X"
            columna = 21
            oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("NumeroDocIdentidad")
        End If

        If int_TipoDocIdentidad = 5 Then 'Libreta Electoral
            columna = 12
            oCells(fila, columna) = "X"
            columna = 21
            oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("NumeroDocIdentidad")
        End If

        'l) Lengua Materna y Segunda Lengua
        Dim int_ContLenguas As Integer = 0
        columna = 21
        While int_ContLenguas <= dt_Idiomas.Rows.Count - 1

            If dt_Idiomas.Rows(int_ContLenguas).Item("LenguaMaterna") = True Then
                fila = 16
                oCells(fila, columna) = dt_Idiomas.Rows(int_ContLenguas).Item("idioma")
            Else
                fila = 18
                oCells(fila, columna) = dt_Idiomas.Rows(int_ContLenguas).Item("idioma")
            End If

            int_ContLenguas = int_ContLenguas + 1
        End While

        'm) Numero de hermanos y lugar que ocupa
        fila = 19
        columna = 21
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("CantidadHermanos")
        columna = 27
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("PosicionEntreHermanos")

        'n) Religión
        columna = 21
        fila = 20
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DecripReligion")

        '---------------------------------------------
        'PASO 3: DESARROLLO DEL ESTUDIANTE
        '---------------------------------------------

        'a) Tipo de Nacimiento
        Dim int_TipoNacimiento As Integer = 0
        int_TipoNacimiento = dt_DatosGenerales.Rows(0).Item("CodigoTipoNacimiento")
        fila = 17

        If int_TipoNacimiento = 1 Then 'NORMAL
            columna = 33
            oCells(fila, columna) = "X"
        Else                           'CON COMPLICACIONES
            columna = 41
            oCells(fila, columna) = "X"
        End If

        'b) Observaciones del tipo de nacimiento
        fila = 19
        columna = 29
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("ObservacionesTipoNacimiento")

        'c) Aspectos y Actividades (Edades)
        columna = 57

        'EDAD LEVANTO CABEZA
        fila = 17
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("EdadLevantoCabeza")

        'EDAD SE SENTO
        fila = 18
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("EdadSento")

        'EDAD SE PARO
        fila = 19
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("EdadParo")

        'EDAD CAMINO
        fila = 20
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("EdadCamino")

        'EDAD CONTROL DE ESFINTERES
        fila = 21
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("EdadControlEsfinteres")

        'EDAD HABLO PRIMERAS PALABRAS
        fila = 22
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("EdadHabloPrimerasPalabras")

        'EDAD HABLO FLUIDEZ
        fila = 23
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("EdadHabloFluidez")

        '---------------------------------------------
        'PASO 4: CONTROLES DE SALUD DEL ESTUDIANTE
        '---------------------------------------------

        'a) CONTROL DE PESO Y TALLA
        Dim int_contControlesSalud As Integer = 0
        Dim db_Peso As Double = 0.0
        Dim db_Talla As Double = 0.0
        fila = 28
        columna = 1

        While int_contControlesSalud <= dt_ControlesSalud.Rows.Count - 1
            fila = 28 + int_contControlesSalud

            'a) DIA DE CONTROL
            oCells.Range(oCells.Cells(fila, columna), oCells.Cells(fila, columna + 1)).Merge()
            oCells(fila, columna) = dt_ControlesSalud.Rows(int_contControlesSalud).Item("DiaControl")

            'b) MES DE CONTROL
            oCells.Range(oCells.Cells(fila, columna + 2), oCells.Cells(fila, columna + 3)).Merge()
            oCells(fila, columna + 2) = dt_ControlesSalud.Rows(int_contControlesSalud).Item("MesControl")

            'c) ANIO DE CONTROL
            oCells.Range(oCells.Cells(fila, columna + 4), oCells.Cells(fila, columna + 5)).Merge()
            oCells(fila, columna + 4) = dt_ControlesSalud.Rows(int_contControlesSalud).Item("AnioControl")

            'd) PESO DE CONTROL
            oCells.Range(oCells.Cells(fila, columna + 6), oCells.Cells(fila, columna + 7)).Merge()
            db_Peso = dt_ControlesSalud.Rows(int_contControlesSalud).Item("Peso")
            oCells(fila, columna + 6) = Format(db_Peso, "##,###.00")

            'e) TALLA DE CONTROL
            oCells.Range(oCells.Cells(fila, columna + 8), oCells.Cells(fila, columna + 9)).Merge()
            db_Talla = dt_ControlesSalud.Rows(int_contControlesSalud).Item("Talla")
            oCells(fila, columna + 8) = Format(db_Talla, "##,###.00")

            'f) OBSERVACIONES DE CONTROL
            oCells.Range(oCells.Cells(fila, columna + 10), oCells.Cells(fila, columna + 11)).Merge()
            oCells(fila, columna + 10) = dt_ControlesSalud.Rows(int_contControlesSalud).Item("Observaciones")

            db_Peso = 0.0
            db_Talla = 0.0
            int_contControlesSalud = int_contControlesSalud + 1
        End While

        'OTROS CONTROLES
        Dim int_ContOtrosControles As Integer = 0
        fila = 28
        columna = 23

        While int_ContOtrosControles <= dt_OtrosControles.Rows.Count - 1
            fila = 28 + int_ContOtrosControles

            'g) DIA DE CONTROL
            oCells(fila, columna) = dt_OtrosControles.Rows(int_ContOtrosControles).Item("DiaControlOtro")

            'h) MES DE CONTROL
            oCells(fila, columna + 2) = dt_OtrosControles.Rows(int_ContOtrosControles).Item("MesControlOtro")

            'i) ANIO DE CONTROL
            oCells(fila, columna + 4) = dt_OtrosControles.Rows(int_ContOtrosControles).Item("AnioControlOtro")

            'j) CONTROL
            oCells(fila, columna + 6) = dt_OtrosControles.Rows(int_ContOtrosControles).Item("DescripTipoControl")

            'k) RESULTADO
            oCells(fila, columna + 9) = dt_OtrosControles.Rows(int_ContOtrosControles).Item("DescripResultado")

            int_ContOtrosControles = int_ContOtrosControles + 1
        End While

        '---------------------------------------------
        'PASO 5: ESTADO DE SALUD DEL ESTUDIANTE
        '---------------------------------------------

        'a) ENFERMEDADES
        Dim int_ContEnfermedades As Integer = 0
        fila = 28
        columna = 36

        While int_ContEnfermedades <= dt_Enfermedades.Rows.Count - 1
            fila = 28 + int_ContEnfermedades

            oCells(fila, columna) = dt_Enfermedades.Rows(int_ContEnfermedades).Item("EdadEnfermedad")
            oCells(fila, columna + 2) = dt_Enfermedades.Rows(int_ContEnfermedades).Item("Enfermedad")

            int_ContEnfermedades = int_ContEnfermedades + 1
        End While

        'b) ALERGIAS
        Dim int_ContAlergias As Integer = 0
        Dim str_ConsolidadoAlergias As String = ""
        fila = 26
        columna = 49

        While int_ContAlergias <= dt_Alergias.Rows.Count - 1
            str_ConsolidadoAlergias = str_ConsolidadoAlergias + ", " + dt_Alergias.Rows(int_ContAlergias).Item("DescripAlergias")
            int_ContAlergias = int_ContAlergias + 1
        End While

        oCells(fila, columna) = str_ConsolidadoAlergias

        'c) Tipo de Sangre
        fila = 32
        columna = 54
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DescripTipoSangre")

        '---------------------------------------------
        'PASO 6: DATOS DEL DOMICILIO DEL ESTUDIANTE
        '---------------------------------------------
        fila = 36

        'a) Año del Domicilio
        columna = 1
        oCells(fila, columna) = 2011

        'b) Dirección del Domicilio
        columna = 3
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DireccionDomicilio")

        'b) Urbanizacion del Domicilio
        columna = 15
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DescripUrbanizacionDomicilio")

        'c) Departamento del Domicilio
        columna = 19
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DepartamentoDomicilio")

        'd) Provincia del Domicilio
        columna = 23
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("ProvinciaDomicilio")

        'e) Distrito del Domicilio
        columna = 28
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("DistritoDomicilio")

        'f) Telefono del Domicilio
        columna = 33
        oCells(fila, columna) = dt_DatosGenerales.Rows(0).Item("TelfCasa")

        '---------------------------------------------
        'PASO 7: DATOS DE LOS PADRES
        '---------------------------------------------
        Dim int_ContPadres As Integer = 0
        fila = 36

        While int_ContPadres <= dt_Padres.Rows.Count - 1

            If dt_Padres.Rows(int_ContPadres).Item("CodigoParentesco") = 1 Then 'PADRE

                'a) Apellido Paterno
                fila = 36
                columna = 43
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("ApePaterno")

                'b) Apellido Materno
                fila = 37
                columna = 43
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("ApeMaterno")

                'c) Nombres
                fila = 38
                columna = 43
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("Nombre")

                'd) Vive
                fila = 39

                If dt_Padres.Rows(int_ContPadres).Item("Vive") = True Then
                    columna = 45
                    oCells(fila, columna) = "X"
                Else
                    columna = 49
                    oCells(fila, columna) = "X"
                End If

                'e) Dia de Nacimiento
                fila = 41
                columna = 43
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("DiaNacimiento")

                'f) Mes de Nacimiento
                fila = 41
                columna = 45
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("MesNacimiento")

                'f) Año de Nacimiento
                fila = 41
                columna = 48
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("AnioNacimiento")

                'g) Grado de Instruccion
                fila = 42
                columna = 43
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("DescripGradoInstruccion")

                'h) Ocupacion
                fila = 43
                columna = 43
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("Ocupacion")

                'i) Vive con el Estudiante
                fila = 44

                If dt_Padres.Rows(int_ContPadres).Item("ViveConAlumno") = True Then
                    columna = 45
                    oCells(fila, columna) = "X"
                Else
                    columna = 49
                    oCells(fila, columna) = "X"
                End If


            ElseIf dt_Padres.Rows(int_ContPadres).Item("CodigoParentesco") = 2 Then 'MADRE

                'a) Apellido Paterno
                fila = 36
                columna = 51
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("ApePaterno")

                'b) Apellido Materno
                fila = 37
                columna = 51
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("ApeMaterno")

                'c) Nombres
                fila = 38
                columna = 51
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("Nombre")

                'd) Vive
                fila = 39

                If dt_Padres.Rows(int_ContPadres).Item("Vive") = True Then
                    columna = 53
                    oCells(fila, columna) = "X"
                Else
                    columna = 57
                    oCells(fila, columna) = "X"
                End If

                'e) Dia de Nacimiento
                fila = 41
                columna = 51
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("DiaNacimiento")

                'f) Mes de Nacimiento
                fila = 41
                columna = 53
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("MesNacimiento")

                'f) Año de Nacimiento
                fila = 41
                columna = 56
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("AnioNacimiento")

                'g) Grado de Instruccion
                fila = 42
                columna = 51
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("DescripGradoInstruccion")

                'h) Ocupacion
                fila = 43
                columna = 51
                oCells(fila, columna) = dt_Padres.Rows(int_ContPadres).Item("Ocupacion")

                'i) Vive con el Estudiante
                fila = 44

                If dt_Padres.Rows(int_ContPadres).Item("ViveConAlumno") = True Then
                    columna = 53
                    oCells(fila, columna) = "X"
                Else
                    columna = 57
                    oCells(fila, columna) = "X"
                End If

            End If

            int_ContPadres = int_ContPadres + 1
        End While

        '---------------------------------------------
        oExcel.ActiveWindow.Zoom = 90

    End Sub

    Private Shared Sub LlenarPlantilla_FichaUnicaMatricula_Hoja2(ByVal ds_Reporte As System.Data.DataSet, _
                                ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                ByVal oExcel As Microsoft.Office.Interop.Excel.Application)

        Dim dt_DatosMatriculas As New DataTable
        Dim dt_Padres As New DataTable
        Dim fila As Integer = 3
        Dim columna As Integer = 6

        dt_Padres = ds_Reporte.Tables(6)
        dt_DatosMatriculas = ds_Reporte.Tables(7)

        Dim int_ContMatriculas As Integer = 0
        Dim str_CodigoModular As String = ""
        Dim int_ContCodigoModular As Integer = 0
        Dim int_ContApoderados As Integer = 0
        Dim int_ContPadres As Integer = 0
        Dim int_PrimerPeriodoAlumno As Integer = 0
        Dim int_PeriodoAlumnoReferencia As Integer = 0
        Dim int_DesplazadorDeAnios As Integer = 1

        While int_ContMatriculas <= dt_DatosMatriculas.Rows.Count - 1

            'OBTENER EL PRIMER AÑO DEL ALUMNO EN EL COLEGIO
            If int_ContMatriculas = 0 Then
                int_PrimerPeriodoAlumno = dt_DatosMatriculas.Rows(0).Item("PeriodoAcademico")
            Else
                int_PeriodoAlumnoReferencia = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("PeriodoAcademico")
                int_DesplazadorDeAnios = int_PeriodoAlumnoReferencia - int_PrimerPeriodoAlumno

            End If

            '---------------------------------------------
            'PASO 1: MATRICULAS
            '---------------------------------------------

            'a) PERIODO ACADEMICO
            oCells(fila, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("PeriodoAcademico")

            'b) NOMBRE DE COLEGIO
            oCells(fila + 1, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("ColegioProcedencia")

            'c) CODIGO MODULAR
            str_CodigoModular = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("CodigoModularNivel")

            While int_ContCodigoModular <= str_CodigoModular.Length - 1
                oCells(fila + 3, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7) + int_ContCodigoModular) = str_CodigoModular.Chars(int_ContCodigoModular).ToString
                int_ContCodigoModular = int_ContCodigoModular + 1
            End While

            'd) DEPARTAMENTO
            oCells(fila + 4, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("Departamento")

            'e) PROVINCIA
            oCells(fila + 5, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("Provincia")

            'f) DISTRITO
            oCells(fila + 6, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("Distrito")

            'g) INSTANCIA DE GESTION
            oCells(fila + 7, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("InstanciaGestion")

            'h) NIVEL
            oCells(fila + 9, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("DescripNivel")

            'i) GRADO
            oCells(fila + 10, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("DescripGrado")

            'j) SECCION
            oCells(fila + 11, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("DescripSeccion")

            'k) SECCION
            oCells(fila + 12, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("Turno")

            '---------------------------------------------
            'PASO 2: RESPONSABLE DE MATRICULA
            '---------------------------------------------

            oCells(fila + 27, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("PeriodoAcademico")

            '---------------------------------------------
            'PASO 3: DATOS DEL APODERADO DE MATRICULA
            '---------------------------------------------

            oCells(fila + 34, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("PeriodoAcademico")

            While int_ContApoderados <= dt_Padres.Rows.Count - 1

                If dt_Padres.Rows(int_ContApoderados).Item("Apoderado") = True Then
                    oCells(fila + 35, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("ApePaterno")
                    oCells(fila + 36, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("ApeMaterno")
                    oCells(fila + 37, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("Nombre")
                    oCells(fila + 38, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("DescripParentesco")

                    oCells(fila + 41, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("DiaNacimiento")
                    oCells(fila + 41, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7) + 2) = dt_Padres.Rows(int_ContApoderados).Item("MesNacimiento")
                    oCells(fila + 41, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7) + 4) = dt_Padres.Rows(int_ContApoderados).Item("AnioNacimiento")

                    oCells(fila + 42, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("DescripGradoInstruccion")
                    oCells(fila + 43, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("Ocupacion")
                    oCells(fila + 44, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("Domicilio")
                    oCells(fila + 45, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_Padres.Rows(int_ContApoderados).Item("TelfCasa")
                    Exit While
                End If
                int_ContApoderados = int_ContApoderados + 1
            End While

            '---------------------------------------------
            'PASO 3: SUPERVIVENCIA DE LOS PADRES
            '---------------------------------------------

            oCells(fila + 47, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7)) = dt_DatosMatriculas.Rows(int_ContMatriculas).Item("PeriodoAcademico")

            While int_ContPadres <= dt_Padres.Rows.Count - 1

                If dt_Padres.Rows(int_ContPadres).Item("CodigoParentesco") = 1 Then 'PADRE

                    If dt_Padres.Rows(int_ContPadres).Item("Vive") = True Then
                        oCells(fila + 48, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7) + 2) = "X"
                    Else
                        oCells(fila + 48, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7) + 5) = "X"
                    End If
                Else            'MADRE

                    If dt_Padres.Rows(int_ContPadres).Item("Vive") = True Then
                        oCells(fila + 49, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7) + 2) = "X"
                    Else
                        oCells(fila + 49, columna + ((int_ContMatriculas * int_DesplazadorDeAnios) * 7) + 5) = "X"
                    End If
                End If
                int_ContPadres = int_ContPadres + 1
            End While


            int_ContPadres = 0
            int_ContApoderados = 0
            int_ContCodigoModular = 0
            str_CodigoModular = ""
            int_PeriodoAlumnoReferencia = 0
            int_DesplazadorDeAnios = 1
            int_ContMatriculas = int_ContMatriculas + 1
        End While

    End Sub

#End Region
#Region "Nomina Matricula"
    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="ds_Consulta">Tabla temporal de datos a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Edgar Chang 
    ''' Fecha de Creación:     01/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarLlenarPlantillaNominaSIAGE(ByVal ds_Consulta As System.Data.DataSet, ByVal sFullName As String) As String
        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim dt_EstudMatric As DataTable
        Dim Fila As String = ""
        dt_EstudMatric = ds_Consulta.Tables(0)
        nombreRep = GetNewName()
        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = sFullName

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph
        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        '**************************************************************
        '           HojaCalculo: ESTUDIANTES MATRICULADOS
        '**************************************************************
        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "EstudiantesMatriculados"
        oCells = oSheet.Cells
        oSheet.Activate()
        LlenarPlantillaNominaSIAGE_EstudiantesMatriculados(dt_EstudMatric, oCells, oExcel)

        'LlenarPlantillaNominaSIAGE
        oSheet.SaveAs(sFile)
        oBook.Close()

        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function
    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="oCells">Instancia de rango de Periodo Inicio y Fin</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     10/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________
    ''' </remarks>
    Private Shared Sub LlenarPlantillaNominaSIAGE_Generalidades(ByVal dtNominaSIAGE As System.Data.DataTable, _
                                                          ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                                          ByVal oExcel As Microsoft.Office.Interop.Excel.Application)
        oExcel.Range(oCells(5, 5), oCells(5, 6)).Value = dtNominaSIAGE.Rows(0).Item("CodigoModular")
        oExcel.Range(oCells(5, 8), oCells(5, 10)).Value = dtNominaSIAGE.Rows(0).Item("Nivel")
        oExcel.Range(oCells(6, 3), oCells(6, 10)).Value = dtNominaSIAGE.Rows(0).Item("Nombre")
        oExcel.Range(oCells(6, 4), oCells(6, 10)).Value = dtNominaSIAGE.Rows(0).Item("AnioAcademico")
        oExcel.Range(oCells(9, 3), oCells(9, 4)).Value = dtNominaSIAGE.Rows(0).Item("Grado")
        oExcel.Range(oCells(9, 6), oCells(9, 6)).Value = dtNominaSIAGE.Rows(0).Item("Seccion")

    End Sub
    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="oCells">Instancia de rango de Periodo Inicio y Fin</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     10/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________
    ''' </remarks>
    Private Shared Sub LlenarPlantillaNominaSIAGE_EstudiantesMatriculados(ByVal dtNominaSIAGE As System.Data.DataTable, _
                                                          ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                                          ByVal oExcel As Microsoft.Office.Interop.Excel.Application)
        Dim Int_Fila As Integer = 2
        Dim Int_Columna As Integer = 2
        Dim Int_ContColum As Integer = 0
        Dim Int_ContFilas As Integer = 0
        Dim str_Fila As String = ""
        While Int_ContFilas <= dtNominaSIAGE.Rows.Count - 1
            While Int_ContColum <= dtNominaSIAGE.Columns.Count - 1
                If Int_ContColum <= dtNominaSIAGE.Columns.Count - 1 Then
                    oCells(Int_Fila + Int_ContFilas, Int_Columna + Int_ContColum) = dtNominaSIAGE.Rows(Int_ContFilas).Item(Int_ContColum)
                End If
                Int_ContColum = Int_ContColum + 1
            End While
            Int_ContColum = 0
            oCells(Int_Fila + Int_ContFilas, 1) = 1 + Int_ContFilas
            Int_ContFilas = Int_ContFilas + 1
        End While
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(2, 1), oExcel.Cells(1 + dtNominaSIAGE.Rows.Count, 1 + dtNominaSIAGE.Columns.Count)))
        oExcel.ActiveWindow.Zoom = 75
    End Sub
#End Region
#End Region

#Region "Módulo de Seguridad"
    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Edgar Chang 
    ''' Fecha de Creación:     01/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteUsuariosEnElSistemaGeneral(ByVal dtReporte As System.Data.DataSet, ByVal str_FechaInicio As String, ByVal str_FechaFin As String, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim dtUsuariosEnElSistemas As DataTable
        Dim Fila As String = ""
        dtUsuariosEnElSistemas = dtReporte.Tables(0)
        nombreRep = GetNewName()
        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteAccesoSistema8_9").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "UsuariosSistema"
        oCells = oSheet.Cells
        oSheet.Activate()
        Fila = LlenarPlantillaUsuariosEnElSistemaGeneral(dtUsuariosEnElSistemas, str_FechaInicio, str_FechaFin, oCells, oExcel, str_NombreEntidadReporte)
        'LlenarPlantillaNominaSIAGE
        oSheet.SaveAs(sFile)
        oBook.Close()
        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Exporta reporte en formato EXCEL (listado de items)
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte a exportar</param>
    ''' <returns>Retorna nombre de reporte generado en el servidor a exportar</returns>
    ''' <remarks>
    ''' Creador:               Edgar Chang 
    ''' Fecha de Creación:     01/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteUsuariosEnElSistemaDetalle(ByVal dtReporte As System.Data.DataSet, ByVal str_FechaInicio As String, ByVal str_FechaFin As String, ByVal str_NombreEntidadReporte As String) As String

        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks, oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets, oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String, sTemplate As String
        Dim nombreRep As String
        Dim dtUsuariosEnElSistemas As DataTable
        Dim Fila As String = ""
        Dim objTablaDinamica As Microsoft.Office.Interop.Excel.PivotTable
        dtUsuariosEnElSistemas = dtReporte.Tables(0)
        nombreRep = GetNewName()
        sFile = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & nombreRep & ".xls"
        sTemplate = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaExcel_ReporteAccesoSistema8_10").ToString()

        oExcel.Visible = False : oExcel.DisplayAlerts = False

        ''Start a new workbook 
        oBooks = oExcel.Workbooks
        oBooks.Open(sTemplate) 'Load colorful template with graph

        oBook = oBooks.Item(1)
        oSheets = oBook.Worksheets
        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "UsuariosSistema"
        oCells = oSheet.Cells
        oSheet.Activate()
        Fila = LlenarPlantillaUsuariosEnElSistemaDetalle(dtUsuariosEnElSistemas, str_FechaInicio, str_FechaFin, oCells, oExcel, str_NombreEntidadReporte)

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Name = "Reporte Dinamico"
        oCells = oSheet.Cells
        'Pintado de Fecha 
        oExcel.Range(oCells(3, 1), oCells(3, 5)).Merge()
        oExcel.Range(oCells(3, 1), oCells(3, 5)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 1), oCells(3, 5)).Value = "USUARIOS EN EL SISTEMA"
        oExcel.Range(oCells(3, 1), oCells(3, 5)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 1), oCells(4, 5)).Merge()
        oExcel.Range(oCells(4, 1), oCells(4, 5)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 1), oCells(4, 5)).Value = "del  " & str_FechaInicio & "  " & "al  " & str_FechaFin
        oExcel.Range(oCells(4, 1), oCells(4, 5)).Font.Bold = True
        oExcel.Range(oCells(4, 1), oCells(4, 5)).RowHeight = 25

        'Pintado de Fecha 
        oExcel.Range(oCells(5, 1), oCells(5, 5)).Merge()
        oExcel.Range(oCells(5, 1), oCells(5, 5)).HorizontalAlignment = 3
        oExcel.Range(oCells(5, 1), oCells(5, 5)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(5, 1), oCells(5, 5)).Font.Bold = True
        oExcel.Range(oCells(5, 1), oCells(5, 5)).RowHeight = 20

        Dim int_cont As Integer = 0
        Dim str_DescGrado As String = ""
        'Dim str_DescMonto As String 
        Dim dv_UsuaSist As DataView
        dv_UsuaSist = dtReporte.Tables(0).DefaultView

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica = oSheet.PivotTables("Tabla dinámica1")


        oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()

        objTablaDinamica.PivotCache.SourceData = "UsuariosSistema!F9C2:F" & Fila & "C5" 'Usuarios en el Sistema!R9C2:R272C5 - "UsuariosSistema!F9C2:F" & Fila & "C5"
        objTablaDinamica.PivotCache.Refresh()

        oSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden

        oSheet = CType(oSheets.Item(2), Microsoft.Office.Interop.Excel.Worksheet)
        oSheet.Activate()
        oExcel.ActiveWindow.Zoom = 75
        oSheet.SaveAs(sFile)
        oBook.Close()
        'Quit Excel and thoroughly deallocate everything
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oExcel = Nothing
        oBooks = Nothing
        oBook = Nothing
        oSheets = Nothing
        oSheet = Nothing
        oCells = Nothing
        System.GC.Collect()

        Return nombreRep
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="oCells">Instancia de rango de Periodo Inicio y Fin</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     01/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________
    ''' </remarks>
    Private Shared Function LlenarPlantillaUsuariosEnElSistemaDetalle(ByVal dtUsuariosEnElSistema As System.Data.DataTable, _
                                                          ByVal str_FechaInicio As String, ByVal str_FechaFin As String, _
                                                          ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                                          ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                                          ByVal str_NombreEntidadReporte As String) As String
        Dim fila As Integer = 10
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""
        'Pintado de Titulo
        oExcel.Range(oCells(3, 5), oCells(3, 11)).Merge()
        oExcel.Range(oCells(3, 5), oCells(3, 11)).HorizontalAlignment = 3
        oExcel.Range(oCells(3, 5), oCells(3, 11)).Value = "USUARIOS EN EL SISTEMA"
        oExcel.Range(oCells(3, 5), oCells(3, 11)).Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range(oCells(4, 5), oCells(4, 11)).Merge()
        oExcel.Range(oCells(4, 5), oCells(4, 11)).HorizontalAlignment = 3
        oExcel.Range(oCells(4, 5), oCells(4, 11)).Value = "del  " & str_FechaInicio & "  " & "al  " & str_FechaFin
        oExcel.Range(oCells(4, 5), oCells(4, 11)).Font.Bold = True
        oExcel.Range(oCells(4, 5), oCells(4, 11)).RowHeight = 35

        'Pintado de Fecha 
        oExcel.Range(oCells(5, 5), oCells(5, 11)).Merge()
        oExcel.Range(oCells(5, 5), oCells(5, 11)).HorizontalAlignment = 3
        oExcel.Range(oCells(5, 5), oCells(5, 11)).Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range(oCells(5, 5), oCells(5, 11)).Font.Bold = True
        oExcel.Range(oCells(5, 5), oCells(5, 11)).RowHeight = 30
        With oExcel.Range("B9:E9")
            .HorizontalAlignment = 3
            .WrapText = True
            .Font.Bold = True
            .Interior.Color() = RGB(204, 255, 204)
        End With

        With oExcel.Range(oCells(9, 2), oCells(9, 2))
            .Merge()
            .Value = "Nombre Usuario"
            .ColumnWidth = 35

        End With

        oExcel.Range(oCells(9, 3), oCells(9, 3)).Merge()
        oExcel.Range(oCells(9, 3), oCells(9, 3)).ColumnWidth = 15
        oExcel.Range(oCells(9, 3), oCells(9, 3)).Value = "Tipo de Usuario"
        oExcel.Range(oCells(9, 4), oCells(9, 4)).Merge()
        oExcel.Range(oCells(9, 4), oCells(9, 4)).ColumnWidth = 15
        oExcel.Range(oCells(9, 4), oCells(9, 4)).Value = "Nro Accesos"
        oExcel.Range(oCells(9, 5), oCells(9, 5)).Merge()
        oExcel.Range(oCells(9, 5), oCells(9, 5)).ColumnWidth = 15
        oExcel.Range(oCells(9, 5), oCells(9, 5)).Value = "Fecha de Acceso"



        'Pintado del cuadrado de Cabecera estática 
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(9, 2), oExcel.Cells(9, 5)))

        While cont_filas <= dtUsuariosEnElSistema.Rows.Count - 1
            While cont_columnas <= dtUsuariosEnElSistema.Columns.Count - 1
                If cont_columnas <= dtUsuariosEnElSistema.Columns.Count - 1 Then
                    oCells(fila + cont_filas, columna + cont_columnas) = dtUsuariosEnElSistema.Rows(cont_filas).Item(cont_columnas)
                End If
                cont_columnas = cont_columnas + 1
            End While
            cont_columnas = 0
            cont_filas = cont_filas + 1
        End While
        str_Fila = (cont_filas + 9).ToString
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(9, 2), oExcel.Cells(9 + dtUsuariosEnElSistema.Rows.Count, 1 + dtUsuariosEnElSistema.Columns.Count)))
        oExcel.ActiveWindow.Zoom = 75
        Return str_Fila
    End Function

    ''' <summary>
    ''' Llena el documento EXCEL con la información que se envio para su exportación.
    ''' </summary>
    ''' <param name="oCells">Instancia de rango de Periodo Inicio y Fin</param>
    ''' <param name="oExcel">Instancia del libro de excel</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <remarks>
    ''' Creador:               Edgar Chang
    ''' Fecha de Creación:     01/09/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________
    ''' </remarks>
    Private Shared Function LlenarPlantillaUsuariosEnElSistemaGeneral(ByVal dtUsuariosEnElSistema As System.Data.DataTable, _
                                                          ByVal str_FechaInicio As String, ByVal str_FechaFin As String, _
                                                          ByVal oCells As Microsoft.Office.Interop.Excel.Range, _
                                                          ByVal oExcel As Microsoft.Office.Interop.Excel.Application, _
                                                          ByVal str_NombreEntidadReporte As String) As String
        Dim fila As Integer = 8
        Dim columna As Integer = 2
        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim str_Fila As String = ""
        'Pintado de Titulo
        oExcel.Range("A1:F1").Merge()
        oExcel.Range("A1:F1").HorizontalAlignment = 3
        oExcel.Range("A1:F1").Value = "USUARIOS EN EL SISTEMA"
        oExcel.Range("A1:F1").Font.Bold = True

        'Pintado de Fecha 
        oExcel.Range("A2:F2").Merge()
        oExcel.Range("A2:F2").HorizontalAlignment = 3
        oExcel.Range("A2:F2").Value = "del  " & str_FechaInicio & "  " & "al  " & str_FechaFin
        oExcel.Range("A2:F2").Font.Bold = True
        oExcel.Range("A2:F2").RowHeight = 35

        'Pintado de Fecha 
        oExcel.Range("A3:F3").Merge()
        oExcel.Range("A3:F3").HorizontalAlignment = 3
        oExcel.Range("A3:F3").Value = "Fecha de Reporte: " & Now.Date & "    " & Now.Hour & " : " & Now.Minute
        oExcel.Range("A3:F3").Font.Bold = True
        oExcel.Range("A3:F3").RowHeight = 30
        With oExcel.Range("B7:E7")
            .HorizontalAlignment = 3
            .WrapText = True
            .Font.Bold = True
            .Interior.Color() = RGB(204, 255, 204)
        End With

        With oExcel.Range(oCells(7, 2), oCells(7, 2))
            .Merge()
            .Value = "Nombre Usuario"
            .ColumnWidth = 35

        End With

        oExcel.Range(oCells(7, 3), oCells(7, 3)).Merge()
        oExcel.Range(oCells(7, 3), oCells(7, 3)).ColumnWidth = 15
        oExcel.Range(oCells(7, 3), oCells(7, 3)).Value = "Tipo de Usuario"
        oExcel.Range(oCells(7, 4), oCells(7, 4)).Merge()
        oExcel.Range(oCells(7, 4), oCells(7, 4)).ColumnWidth = 15
        oExcel.Range(oCells(7, 4), oCells(7, 4)).Value = "Nro Accesos"
        oExcel.Range(oCells(7, 5), oCells(7, 5)).Merge()
        oExcel.Range(oCells(7, 5), oCells(7, 5)).ColumnWidth = 15
        oExcel.Range(oCells(7, 5), oCells(7, 5)).Value = "Fecha de Acceso"



        'Pintado del cuadrado de Cabecera estática 
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(7, 2), oExcel.Cells(7, 5)))

        While cont_filas <= dtUsuariosEnElSistema.Rows.Count - 1
            While cont_columnas <= dtUsuariosEnElSistema.Columns.Count - 1
                If cont_columnas <= dtUsuariosEnElSistema.Columns.Count - 1 Then
                    oCells(fila + cont_filas, columna + cont_columnas) = dtUsuariosEnElSistema.Rows(cont_filas).Item(cont_columnas)
                End If
                cont_columnas = cont_columnas + 1
            End While
            cont_columnas = 0
            cont_filas = cont_filas + 1
        End While
        str_Fila = (cont_filas + 7).ToString
        cuadradoCompleto(oExcel, oExcel.Range(oExcel.Cells(7, 2), oExcel.Cells(7 + dtUsuariosEnElSistema.Rows.Count, 1 + dtUsuariosEnElSistema.Columns.Count)))
        'Margen()
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlPageBreakPreview
        oExcel.ActiveSheet.VPageBreaks(1).Location = oExcel.Range("F1")
        oExcel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=4, RegionIndex:=1)
        oExcel.ActiveWindow.View = Microsoft.Office.Interop.Excel.XlWindowView.xlNormalView
        oExcel.Range("A1").Select()
        oExcel.ActiveWindow.Zoom = 75
        Return str_Fila
    End Function


#End Region
#End Region


#Region "Exportar : Html"

    ''' <summary>
    ''' Devuelve la información enviada en codigo HTML para su exportación en el navegador o en documentos WORD
    ''' </summary>
    ''' <param name="dsReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteWeeklyReport_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String
        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaWeeklyReportHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaWeeklyReportHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla



    End Function


    ''' <summary>
    ''' Devuelve la información enviada en codigo HTML para su exportación en el navegador o en documentos WORD
    ''' </summary>
    ''' <param name="dsReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     04/04/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteMidTermReport_IF_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String, ByVal int_CodigoGrado As Integer) As String
        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            If int_CodigoGrado <= 8 Then
                ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaMidTermReportHtml_Junior").ToString()
            Else
                ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaMidTermReportHtml").ToString()
            End If

            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaMidTermReportHtml_IF(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla



    End Function

    ''' <summary>
    ''' Devuelve la información enviada en codigo HTML para su exportación en el navegador o en documentos WORD
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporte_Html(ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String()

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Dim DatosExportacionHtml(1) As String
        Dim nombreArchivo As String = ""

        Try
            nombreArchivo = GetNewName()
            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaHtml1").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaHtml(fileReaderPlantilla, dtReporte, str_NombreEntidadReporte)

            DatosExportacionHtml(0) = fileReaderPlantilla
            DatosExportacionHtml(1) = nombreArchivo

        Catch ex As Exception

        End Try

        Return DatosExportacionHtml

    End Function

    ''' <summary>
    ''' Devuleve la información de la Ficha de Atención Médica recibida en codigo HTML para su exportación en el navegador o en documentos WORD.
    ''' </summary>
    ''' <param name="dsReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteFichaAtencion_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFichaAtencionHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaFichaAtencionHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla

    End Function

    ''' <summary>
    ''' Devuleve la información de la Ficha de Atención Médica recibida en codigo HTML para su exportación en el navegador o en documentos WORD.
    ''' </summary>
    ''' <param name="dsReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteFichaAtencionFamilia_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFichaAtencionFamiliaHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaFichaAtencionFamiliaHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla

    End Function

    ''' <summary>
    ''' Devuleve la información de la Ficha Médica recibida en el codig HTML para su exportación en el navegador o en documentos WORD.
    ''' </summary>
    ''' <param name="dsReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteFichaMedica_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFichaMedicaHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaFichaMedicaHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla

    End Function

    ''' <summary>
    ''' Devuelve la información de la Ficha Familiar recibida en el código HTML para su exportación en el navegador o en un documento de tipo WORD.
    ''' </summary>
    ''' <param name="dsReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteFichaFamiliar_Html(ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Try

            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaFichaFamiliarHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaFichaFamiliarHtml(fileReaderPlantilla, dsReporte, str_NombreEntidadReporte)

        Catch ex As Exception
            fileReaderPlantilla = ""
        End Try

        Return fileReaderPlantilla

    End Function

    ''' <summary>
    ''' Devuleve la información de la Ficha Médica recibida en el codig HTML para su exportación en el navegador o en documentos WORD.
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteCompromisoPago_Html(ByVal dtReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String ' As String()

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Dim DatosExportacionHtml(1) As String
        Dim nombreArchivo As String = ""

        Try
            nombreArchivo = GetNewName()
            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaCompromisoPagoTesoreriaHtml").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaCompromisoPagoHtml(fileReaderPlantilla, dtReporte, str_NombreEntidadReporte)

            DatosExportacionHtml(0) = fileReaderPlantilla
            DatosExportacionHtml(1) = nombreArchivo

        Catch ex As Exception

        End Try

        Return fileReaderPlantilla ' DatosExportacionHtml

    End Function

    ''' <summary>
    ''' Llena el documento HTML con la información del CompromisoPago que se envió para su exportación.
    ''' </summary>
    ''' <param name="Plantilla">Cadena de caracteres con codigo HTML (Plantilla del documento a llenar)</param>
    ''' <param name="dsReporte">Conjunto de tablas temporales con la informacion a ingresar en el plantilla</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres de codigo HTML con la información a exportar en el navegador o documento de WORD</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function LlenarPlantillaCompromisoPagoHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""
        Dim nombreCompletoAlumno As String = ""
        Dim nombreAlumno As String = ""
        Dim codigoAlumno As String = ""
        Dim descGrado As String = ""
        Dim nombreAlumnoHermanos As String = ""
        Dim descGradoHermanos As String = ""
        '    
        'Detalle cabeceras
        Plantilla = Plantilla.Replace("[Nombre_CompletoFamiliar]", IIf(dsReporte.Tables(0).Rows(0).Item("Nombre_CompletoFamiliar").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Nombre_CompletoFamiliar")))
        Plantilla = Plantilla.Replace("[numeroDoc]", IIf(dsReporte.Tables(0).Rows(0).Item("numeroDoc").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("numeroDoc")))
        Plantilla = Plantilla.Replace("[parentesco]", IIf(dsReporte.Tables(0).Rows(0).Item("parentesco").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("parentesco")))
        Plantilla = Plantilla.Replace("[fechaemision]", IIf(dsReporte.Tables(0).Rows(0).Item("numeroDoc").ToString.Length = 0, "-", FormatDateTime(dsReporte.Tables(0).Rows(0).Item("fechaemision"), DateFormat.LongDate)))
        Plantilla = Plantilla.Replace("[Nombre_CompletoAlumno]", IIf(dsReporte.Tables(3).Rows(0).Item("Nombre_CompletoAlumno").ToString.Length = 0, "-", dsReporte.Tables(3).Rows(0).Item("Nombre_CompletoAlumno")))
        Plantilla = Plantilla.Replace("[Grado]", IIf(dsReporte.Tables(3).Rows(0).Item("descripcion").ToString.Length = 0, "-", dsReporte.Tables(3).Rows(0).Item("descripcion")))

        Plantilla = Plantilla.Replace("[listaConceptoCompromisoPago]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[listaConceptoCompromisoPago]")

        For i As Integer = 0 To dsReporte.Tables(2).Rows.Count - 1

            'Plantilla = Plantilla.Replace("[listaConceptoCompromisoPago]", "<tr><td colspan='2' align='left' valign='bottom' style='height:10px; font-family: Times New Roman; font-weight: normal; font-size: 10pt;'><u> Alumno: " & dsReporte.Tables(2).Rows(i).Item("nombreCompleto") & "</u></td></tr>[listaConceptoCompromisoPago]")

            Plantilla = Plantilla.Replace("[listaConceptoCompromisoPago]", "<tr><td  align='left' valign='top' style=' width:200px; height:20px; font-family: Times New Roman; font-size: 10pt;'> <b>Descripción</b> </td><td  align='left' valign='top' style='width:250px; font-family: Times New Roman; font-size: 10pt;'><b>Fecha de Pago</b> </td></tr>[listaConceptoCompromisoPago]")

            For c As Integer = 0 To dsReporte.Tables(1).Rows.Count - 1
                If dsReporte.Tables(2).Rows(i).Item("codigoAlumno") = dsReporte.Tables(1).Rows(c).Item("codigoAlumno") Then

                    Plantilla = Plantilla.Replace("[listaConceptoCompromisoPago]", "<tr><td align='left' valign='top' style='font-family: Times New Roman; font-size: 10pt; width:200px;'>" & dsReporte.Tables(1).Rows(c).Item("conceptos") & "</td><td align='left' valign='top' style='font-family: Times New Roman; font-size: 10pt; width:200px;'>" & dsReporte.Tables(1).Rows(c).Item("fechaPagoDeuda") & "</td></tr>[listaConceptoCompromisoPago]")

                End If
            Next
            Plantilla = Plantilla.Replace("[listaConceptoCompromisoPago]", "<tr><td colspan='2' align='left' valign='top' style='font-family: Times New Roman; font-size: 10pt;'> &nbsp;&nbsp;</td></tr>[listaConceptoCompromisoPago]")

        Next

        Plantilla = Plantilla.Replace("[listaConceptoCompromisoPago]", "</table>")

        Return Plantilla
    End Function

    ''' <summary>
    ''' Llena el documento HTML con la información que se envió para su exportación. (Sólo listados)
    ''' </summary>
    ''' <param name="Plantilla">Cadena de caracteres con codigo HTML (Plantilla del documento a llenar)</param>
    ''' <param name="dtReporte">Tabla temporal con la información a ingresar en la plantilla</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres de codigo HTML con la información a exportar en el navegador o documento de WORD</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function LlenarPlantillaHtml(ByVal Plantilla As String, ByVal dtReporte As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""

        'Titulo
        Plantilla = Plantilla.Replace("[nombre_reporte]", "</b>" & str_NombreEntidadReporte & "<b>")

        'Detalle cabeceras
        Plantilla = Plantilla.Replace("[detalle_reporte]", "<table border='1' cellpadding='0' cellspacing='0' style='border-color: #000000; border-collapse: collapse;font-family: Arial, Helvetica, sans-serif'><tr>[detalle_reporte]</tr>[detalle_detalle]</table>")

        While cont_columnas <= dtReporte.Columns.Count - 1

            Plantilla = Plantilla.Replace("[detalle_reporte]", "<td style='border-color: #000000;border-collapse: collapse;font-weight: bold;background-color:#ADD8E6;font-size: 12px; text-align: center;'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
            cont_columnas = cont_columnas + 1
        End While

        Plantilla = Plantilla.Replace("[detalle_reporte]", "")

        'Detalle detalle
        cont_columnas = 0
        Plantilla = Plantilla.Replace("[detalle_detalle]", "<tr>[detalle_contenido]</tr>[detalle_detalle]")

        While cont_filas <= dtReporte.Rows.Count - 1

            While cont_columnas <= dtReporte.Columns.Count - 1

                Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='border-color: #000000;border-collapse: collapse;text-align: center;font-size: 10px;'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                cont_columnas = cont_columnas + 1
            End While

            Plantilla = Plantilla.Replace("[detalle_contenido]", "")

            Plantilla = Plantilla.Replace("[detalle_detalle]", "<tr>[detalle_contenido]</tr>[detalle_detalle]")
            cont_columnas = 0
            cont_filas = cont_filas + 1
        End While

        Plantilla = Plantilla.Replace("[detalle_contenido]", "")
        Plantilla = Plantilla.Replace("[detalle_detalle]", "")

        Return Plantilla
    End Function

    Private Shared Function LlenarPlantillaFichaAtencionFamiliaHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""

        '    
        Plantilla = Plantilla.Replace("[Codigo_FichaAtencion]", IIf(dsReporte.Tables(0).Rows(0).Item("CodigoFichaAtencion").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("CodigoFichaAtencion")))

        'Detalle cabeceras
        Plantilla = Plantilla.Replace("[Nombre_Completo]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreCompleto").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreCompleto")))
        Plantilla = Plantilla.Replace("[Tipo_Paciente]", IIf(dsReporte.Tables(0).Rows(0).Item("DescTipoPersonaPaciente").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescTipoPersonaPaciente")))
        Plantilla = Plantilla.Replace("[Edad]", IIf(dsReporte.Tables(0).Rows(0).Item("Edad").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Edad")))

        If dsReporte.Tables(0).Rows(0).Item("CodigoTipoPersonaPaciente") = 1 Then

            Plantilla = Plantilla.Replace("[Label_NSnGA]", "Nivel / Subnivel / Grado / Aula")
            Plantilla = Plantilla.Replace("[NSnGA]", IIf(dsReporte.Tables(0).Rows(0).Item("NSnGA").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NSnGA")))

        Else

            Plantilla = Plantilla.Replace("[Label_NSnGA]", "")
            Plantilla = Plantilla.Replace("[NSnGA]", "")

        End If

        Plantilla = Plantilla.Replace("[Sede]", IIf(dsReporte.Tables(0).Rows(0).Item("DescSede").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescSede")))
        Plantilla = Plantilla.Replace("[Fecha_Atencion]", IIf(dsReporte.Tables(0).Rows(0).Item("FechaAtencionStr").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("FechaAtencionStr")))
        Plantilla = Plantilla.Replace("[Hora_Ingreso]", IIf(dsReporte.Tables(0).Rows(0).Item("HoraAtencionStr").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("HoraAtencionStr")))

        If dsReporte.Tables(0).Rows(0).Item("CodigoTipoPersonaPaciente") = 1 Then

            Plantilla = Plantilla.Replace("[Label_Responsable]", "Enviado por :")
            Plantilla = Plantilla.Replace("[Responsable]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreCompletoPersonaEnvia").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreCompletoPersonaEnvia")))

        Else

            Plantilla = Plantilla.Replace("[Label_Responsable]", "")
            Plantilla = Plantilla.Replace("[Responsable]", "")

        End If

        Plantilla = Plantilla.Replace("[Sintomas]", IIf(dsReporte.Tables(0).Rows(0).Item("Sintomas").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Sintomas")))
        Plantilla = Plantilla.Replace("[Observaciones]", IIf(dsReporte.Tables(0).Rows(0).Item("Observaciones").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Observaciones")))

        If (dsReporte.Tables(0).Rows(0).Item("DescansoEnfermeria")) Then

            Plantilla = Plantilla.Replace("[DescansoEnfermeria]", "Si")

        Else

            Plantilla = Plantilla.Replace("[DescansoEnfermeria]", "No")

        End If

        'ListaDiagnosticos

        Plantilla = Plantilla.Replace("[ListaDiagnosticos]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[ListaDiagnosticos]")

        If dsReporte.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(1).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaDiagnosticos]", "<tr><td align='left' valign='top' style='width:100%'>" & dsReporte.Tables(1).Rows(i).Item("Descripcion") & "<td></tr>[ListaDiagnosticos]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaDiagnosticos]", "<tr><td align='left' valign='top' style='width:100%'>-<td></tr>[ListaDiagnosticos]")

        End If

        Plantilla = Plantilla.Replace("[ListaDiagnosticos]", "</table>")

        'ListaProcedimientos
        Plantilla = Plantilla.Replace("[ListaProcedimientos]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[ListaProcedimientos]")

        If dsReporte.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(2).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaProcedimientos]", "<tr><td align='left' valign='top' style='width:100%'>" & dsReporte.Tables(2).Rows(i).Item("Descripcion") & "<td></tr>[ListaProcedimientos]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaProcedimientos]", "<tr><td align='left' valign='top' style='width:100%'>-<td></tr>[ListaProcedimientos]")

        End If

        Plantilla = Plantilla.Replace("[ListaProcedimientos]", "</table>")

        'ListaMedicamentos
        Plantilla = Plantilla.Replace("[ListaMedicamentos]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[ListaMedicamentos]")

        If dsReporte.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(3).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaMedicamentos]", "<tr><td align='left' valign='top' style='width:100%'>" & dsReporte.Tables(3).Rows(i).Item("Descripcion") & IIf(dsReporte.Tables(3).Rows(i).Item("Cantidad") = 0, "", " / " & dsReporte.Tables(3).Rows(i).Item("Cantidad")) & "<td></tr>[ListaMedicamentos]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaMedicamentos]", "<tr><td align='left' valign='top' style='width:100%'>-<td></tr>[ListaMedicamentos]")

        End If

        Plantilla = Plantilla.Replace("[ListaMedicamentos]", "</table>")

        Plantilla = Plantilla.Replace("[DestinoFinal]", IIf(dsReporte.Tables(0).Rows(0).Item("DescIndicacionMedica").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescIndicacionMedica")))
        Plantilla = Plantilla.Replace("[Hora_Salida]", IIf(dsReporte.Tables(0).Rows(0).Item("HoraSalidaStr").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("HoraSalidaStr")))

        If dsReporte.Tables(0).Rows(0).Item("CodigoTipoPersonaPaciente") = 1 Then

            Plantilla = Plantilla.Replace("[Label_Nombre_Completo_Acompañante]", "Nombre Completo del Acompañante :")
            Plantilla = Plantilla.Replace("[Nombre_Completo_Acompañante]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreCompletoPersonaRecoge").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreCompletoPersonaRecoge")))
            Plantilla = Plantilla.Replace("[Label_Tipo_Persona_Acompañante]", "Tipo de Persona del Acompañante :")
            Plantilla = Plantilla.Replace("[Tipo_Persona_Acompañante]", IIf(dsReporte.Tables(0).Rows(0).Item("DescTipoPersonaRecoge").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescTipoPersonaRecoge")))

        Else

            Plantilla = Plantilla.Replace("[Label_Nombre_Completo_Acompañante]", "")
            Plantilla = Plantilla.Replace("[Nombre_Completo_Acompañante]", "")
            Plantilla = Plantilla.Replace("[Label_Tipo_Persona_Acompañante]", "")
            Plantilla = Plantilla.Replace("[Tipo_Persona_Acompañante]", "")

        End If

        Return Plantilla
    End Function

    ''' <summary>
    ''' Llena el documento HTML con la información de la Ficha de Atención que se envió para su exportación.
    ''' </summary>
    ''' <param name="Plantilla">Cadena de caracteres con codigo HTML (Plantilla del documento a llenar)</param>
    ''' <param name="dsReporte">Conjunto de tablas temporales con la informacion a ingresar en el plantilla</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres de codigo HTML con la información a exportar en el navegador o documento de WORD</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function LlenarPlantillaFichaAtencionHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""

        '    
        Plantilla = Plantilla.Replace("[Codigo_FichaAtencion]", IIf(dsReporte.Tables(0).Rows(0).Item("CodigoFichaAtencion").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("CodigoFichaAtencion")))

        'Detalle cabeceras
        Plantilla = Plantilla.Replace("[Nombre_Completo]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreCompleto").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreCompleto")))
        Plantilla = Plantilla.Replace("[Tipo_Paciente]", IIf(dsReporte.Tables(0).Rows(0).Item("DescTipoPersonaPaciente").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescTipoPersonaPaciente")))
        Plantilla = Plantilla.Replace("[Edad]", IIf(dsReporte.Tables(0).Rows(0).Item("Edad").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Edad")))

        If dsReporte.Tables(0).Rows(0).Item("CodigoTipoPersonaPaciente") = 1 Then

            Plantilla = Plantilla.Replace("[Label_NSnGA]", "Nivel / Subnivel / Grado / Aula")
            Plantilla = Plantilla.Replace("[NSnGA]", IIf(dsReporte.Tables(0).Rows(0).Item("NSnGA").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NSnGA")))

        Else

            Plantilla = Plantilla.Replace("[Label_NSnGA]", "")
            Plantilla = Plantilla.Replace("[NSnGA]", "")

        End If

        Plantilla = Plantilla.Replace("[Sede]", IIf(dsReporte.Tables(0).Rows(0).Item("DescSede").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescSede")))
        Plantilla = Plantilla.Replace("[Fecha_Atencion]", IIf(dsReporte.Tables(0).Rows(0).Item("FechaAtencionStr").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("FechaAtencionStr")))
        Plantilla = Plantilla.Replace("[Hora_Ingreso]", IIf(dsReporte.Tables(0).Rows(0).Item("HoraAtencionStr").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("HoraAtencionStr")))

        If dsReporte.Tables(0).Rows(0).Item("CodigoTipoPersonaPaciente") = 1 Then

            Plantilla = Plantilla.Replace("[Label_Responsable]", "Enviado por :")
            Plantilla = Plantilla.Replace("[Responsable]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreCompletoPersonaEnvia").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreCompletoPersonaEnvia")))

        Else

            Plantilla = Plantilla.Replace("[Label_Responsable]", "")
            Plantilla = Plantilla.Replace("[Responsable]", "")

        End If

        Plantilla = Plantilla.Replace("[Sintomas]", IIf(dsReporte.Tables(0).Rows(0).Item("Sintomas").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Sintomas")))
        Plantilla = Plantilla.Replace("[Observaciones]", IIf(dsReporte.Tables(0).Rows(0).Item("Observaciones").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Observaciones")))

        If (dsReporte.Tables(0).Rows(0).Item("DescansoEnfermeria")) Then

            Plantilla = Plantilla.Replace("[DescansoEnfermeria]", "Si")

        Else

            Plantilla = Plantilla.Replace("[DescansoEnfermeria]", "No")

        End If

        'ListaDiagnosticos

        Plantilla = Plantilla.Replace("[ListaDiagnosticos]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[ListaDiagnosticos]")

        If dsReporte.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(1).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaDiagnosticos]", "<tr><td align='left' valign='top' style='width:100%'>" & dsReporte.Tables(1).Rows(i).Item("Descripcion") & "<td></tr>[ListaDiagnosticos]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaDiagnosticos]", "<tr><td align='left' valign='top' style='width:100%'>-<td></tr>[ListaDiagnosticos]")

        End If

        Plantilla = Plantilla.Replace("[ListaDiagnosticos]", "</table>")

        'ListaProcedimientos
        Plantilla = Plantilla.Replace("[ListaProcedimientos]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[ListaProcedimientos]")

        If dsReporte.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(2).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaProcedimientos]", "<tr><td align='left' valign='top' style='width:100%'>" & dsReporte.Tables(2).Rows(i).Item("Descripcion") & "<td></tr>[ListaProcedimientos]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaProcedimientos]", "<tr><td align='left' valign='top' style='width:100%'>-<td></tr>[ListaProcedimientos]")

        End If

        Plantilla = Plantilla.Replace("[ListaProcedimientos]", "</table>")

        'ListaMedicamentos
        Plantilla = Plantilla.Replace("[ListaMedicamentos]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[ListaMedicamentos]")

        If dsReporte.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(3).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaMedicamentos]", "<tr><td align='left' valign='top' style='width:100%'>" & dsReporte.Tables(3).Rows(i).Item("Descripcion") & IIf(dsReporte.Tables(3).Rows(i).Item("Cantidad") = 0, "", " / " & dsReporte.Tables(3).Rows(i).Item("Cantidad")) & "<td></tr>[ListaMedicamentos]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaMedicamentos]", "<tr><td align='left' valign='top' style='width:100%'>-<td></tr>[ListaMedicamentos]")

        End If

        Plantilla = Plantilla.Replace("[ListaMedicamentos]", "</table>")

        Plantilla = Plantilla.Replace("[DestinoFinal]", IIf(dsReporte.Tables(0).Rows(0).Item("DescIndicacionMedica").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescIndicacionMedica")))
        Plantilla = Plantilla.Replace("[Hora_Salida]", IIf(dsReporte.Tables(0).Rows(0).Item("HoraSalidaStr").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("HoraSalidaStr")))

        If dsReporte.Tables(0).Rows(0).Item("CodigoTipoPersonaPaciente") = 1 Then

            Plantilla = Plantilla.Replace("[Label_Nombre_Completo_Acompañante]", "Nombre Completo del Acompañante :")
            Plantilla = Plantilla.Replace("[Nombre_Completo_Acompañante]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreCompletoPersonaRecoge").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreCompletoPersonaRecoge")))
            Plantilla = Plantilla.Replace("[Label_Tipo_Persona_Acompañante]", "Tipo de Persona del Acompañante :")
            Plantilla = Plantilla.Replace("[Tipo_Persona_Acompañante]", IIf(dsReporte.Tables(0).Rows(0).Item("DescTipoPersonaRecoge").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescTipoPersonaRecoge")))

        Else

            Plantilla = Plantilla.Replace("[Label_Nombre_Completo_Acompañante]", "")
            Plantilla = Plantilla.Replace("[Nombre_Completo_Acompañante]", "")
            Plantilla = Plantilla.Replace("[Label_Tipo_Persona_Acompañante]", "")
            Plantilla = Plantilla.Replace("[Tipo_Persona_Acompañante]", "")

        End If

        Return Plantilla
    End Function

    ''' <summary>
    ''' Llena el documento HTML con la información de la Ficha Médica que se envió para su exportación.
    ''' </summary>
    ''' <param name="Plantilla">Cadena de caracteres con codigo HTML (Plantilla del documento a llenar)</param>
    ''' <param name="dsReporte">Conjunto de tablas temporales con la informacion a ingresar en el plantilla</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres de codigo HTML con la información a exportar en el navegador o documento de WORD</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function LlenarPlantillaFichaMedicaHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""

        '    
        'Plantilla = Plantilla.Replace("[Codigo_FichaAtencion]", IIf(dsReporte.Tables(0).Rows(0).Item("CodigoFichaAtencion").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("CodigoFichaAtencion")))

        'Detalle cabeceras
        Plantilla = Plantilla.Replace("[Nombre_Completo]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreCompleto").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreCompleto")))
        Plantilla = Plantilla.Replace("[FechaNacimiento]", IIf(dsReporte.Tables(0).Rows(0).Item("FechaNacimiento").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("FechaNacimiento")))
        Plantilla = Plantilla.Replace("[AnioAcademico]", " ")
        Plantilla = Plantilla.Replace("[SituacionActual]", " ")
        Plantilla = Plantilla.Replace("[Sede]", " ")
        Plantilla = Plantilla.Replace("[Nivel]", IIf(dsReporte.Tables(0).Rows(0).Item("Nivel").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("Nivel")))
        Plantilla = Plantilla.Replace("[SubNivel]", IIf(dsReporte.Tables(0).Rows(0).Item("SubNivel").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("SubNivel")))
        Plantilla = Plantilla.Replace("[Grado]", IIf(dsReporte.Tables(0).Rows(0).Item("Grado").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("Grado")))
        Plantilla = Plantilla.Replace("[Aula]", IIf(dsReporte.Tables(0).Rows(0).Item("Aula").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("Aula")))
        Plantilla = Plantilla.Replace("[TipoNacimiento]", IIf(dsReporte.Tables(0).Rows(0).Item("Nacimiento").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("Nacimiento")))
        Plantilla = Plantilla.Replace("[Observaciones]", IIf(dsReporte.Tables(0).Rows(0).Item("Observacion").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("Observacion")))
        Plantilla = Plantilla.Replace("[EdadLevantocabeza]", IIf(dsReporte.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 1, dsReporte.Tables(0).Rows(0).Item("EdadLevantocabeza") & " año ", dsReporte.Tables(0).Rows(0).Item("EdadLevantocabeza") & " años ")) & IIf(dsReporte.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadLevantocabeza").ToString = 0, IIf(dsReporte.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " meses "), " y " & IIf(dsReporte.Tables(0).Rows(0).Item("MesesLevantoCabeza").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesLevantoCabeza") & " meses "))))
        Plantilla = Plantilla.Replace("[EdadSento]", IIf(dsReporte.Tables(0).Rows(0).Item("EdadSento").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadSento").ToString = 1, dsReporte.Tables(0).Rows(0).Item("EdadSento") & " año ", dsReporte.Tables(0).Rows(0).Item("EdadSento") & " años ")) & IIf(dsReporte.Tables(0).Rows(0).Item("MesesSento").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadSento").ToString = 0, IIf(dsReporte.Tables(0).Rows(0).Item("MesesSento").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesSento") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesSento") & " meses "), " y " & IIf(dsReporte.Tables(0).Rows(0).Item("MesesSento").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesSento") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesSento") & " meses "))))
        Plantilla = Plantilla.Replace("[EdadParo]", IIf(dsReporte.Tables(0).Rows(0).Item("EdadParo").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadParo").ToString = 1, dsReporte.Tables(0).Rows(0).Item("EdadParo") & " año ", dsReporte.Tables(0).Rows(0).Item("EdadParo") & " años ")) & IIf(dsReporte.Tables(0).Rows(0).Item("MesesParo").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadParo").ToString = 0, IIf(dsReporte.Tables(0).Rows(0).Item("MesesParo").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesParo") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesParo") & " meses "), " y " & IIf(dsReporte.Tables(0).Rows(0).Item("MesesParo").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesParo") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesParo") & " meses "))))
        Plantilla = Plantilla.Replace("[EdadCamino]", IIf(dsReporte.Tables(0).Rows(0).Item("EdadCamino").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadCamino").ToString = 1, dsReporte.Tables(0).Rows(0).Item("EdadCamino") & " año ", dsReporte.Tables(0).Rows(0).Item("EdadCamino") & " años ")) & IIf(dsReporte.Tables(0).Rows(0).Item("MesesCamino").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadCamino").ToString = 0, IIf(dsReporte.Tables(0).Rows(0).Item("MesesCamino").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesCamino") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesCamino") & " meses "), " y " & IIf(dsReporte.Tables(0).Rows(0).Item("MesesCamino").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesCamino") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesCamino") & " meses "))))
        Plantilla = Plantilla.Replace("[EdadControloEsfinteres]", IIf(dsReporte.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 1, dsReporte.Tables(0).Rows(0).Item("EdadControloEsfinteres") & " año ", dsReporte.Tables(0).Rows(0).Item("EdadControloEsfinteres") & " años ")) & IIf(dsReporte.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadControloEsfinteres").ToString = 0, IIf(dsReporte.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " meses "), " y " & IIf(dsReporte.Tables(0).Rows(0).Item("MesesControloEsfinteres").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesControloEsfinteres") & " meses "))))
        Plantilla = Plantilla.Replace("[EdadPronuncioPrimeraPalabra]", IIf(dsReporte.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 1, dsReporte.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras") & " año ", dsReporte.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras") & " años ")) & IIf(dsReporte.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadHabloPrimerasPalabras").ToString = 0, IIf(dsReporte.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " meses "), " y " & IIf(dsReporte.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesHabloPrimerasPalabras") & " meses "))))
        Plantilla = Plantilla.Replace("[EdadComunicoFluidez]", IIf(dsReporte.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 1, dsReporte.Tables(0).Rows(0).Item("EdadHabloFluidez") & " año ", dsReporte.Tables(0).Rows(0).Item("EdadHabloFluidez") & " años ")) & IIf(dsReporte.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 0, " ", IIf(dsReporte.Tables(0).Rows(0).Item("EdadHabloFluidez").ToString = 0, IIf(dsReporte.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesHabloFluidez") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesHabloFluidez") & " meses "), " y " & IIf(dsReporte.Tables(0).Rows(0).Item("MesesHabloFluidez").ToString = 1, dsReporte.Tables(0).Rows(0).Item("MesesHabloFluidez") & " mes ", dsReporte.Tables(0).Rows(0).Item("MesesHabloFluidez") & " meses "))))
        Plantilla = Plantilla.Replace("[TipoSangre]", IIf(dsReporte.Tables(0).Rows(0).Item("TipoSangre").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("TipoSangre")))
        Plantilla = Plantilla.Replace("[DescripcionOftamologica]", IIf(dsReporte.Tables(0).Rows(0).Item("ObservacionesOftalmologicas").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("ObservacionesOftalmologicas")))
        Plantilla = Plantilla.Replace("[DescripcionOrtodoncia]", IIf(dsReporte.Tables(0).Rows(0).Item("ObservacionesDental").ToString.Length = 0, " ", dsReporte.Tables(0).Rows(0).Item("ObservacionesDental")))

        If (dsReporte.Tables(0).Rows(0).Item("TabiqueDesviado")) Then

            Plantilla = Plantilla.Replace("[tabiqueDesviado]", "Si")

        Else

            Plantilla = Plantilla.Replace("[tabiqueDesviado]", "No")

        End If

        If (dsReporte.Tables(0).Rows(0).Item("SangradoNasal")) Then

            Plantilla = Plantilla.Replace("[SangradoNasal]", "Si")

        Else

            Plantilla = Plantilla.Replace("[SangradoNasal]", "No")

        End If

        If (dsReporte.Tables(0).Rows(0).Item("UsaLentes")) Then

            Plantilla = Plantilla.Replace("[UsaLentes]", "Si")

        Else

            Plantilla = Plantilla.Replace("[UsaLentes]", "No")

        End If

        If (dsReporte.Tables(0).Rows(0).Item("UsaOrtodoncia")) Then

            Plantilla = Plantilla.Replace("[AparatosOrtodoncia]", "Si")

        Else

            Plantilla = Plantilla.Replace("[AparatosOrtodoncia]", "No")

        End If

        ''ListaEnfermedad

        Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<table cellpadding='0' cellspacing='0' style=' width: 450px;'>[ListaEnfermedad]")
        Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px; ' align='left' valign='bottom'><b>Enfermedad</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:193px; font-size:10px;' align='left' valign='bottom'><b>Edad</b></td></tr>[ListaEnfermedad]")

        If dsReporte.Tables(1).Rows(0).Item("CodigoRelFichaMedEnEnfermedades") <> -1 Then

            'Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<Table><tr><td align='left' valign='top' style='width:75%'>Enfermedad</td>[ListaEnfermedad]")
            For i As Integer = 0 To dsReporte.Tables(1).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px;'>" & dsReporte.Tables(1).Rows(i).Item("Enfermedad") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:193px;font-size:10px;'>" & IIf(dsReporte.Tables(1).Rows(i).Item("Edad") = 0, "", dsReporte.Tables(1).Rows(i).Item("Edad")) & " años " & "</td></tr>[ListaEnfermedad]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaEnfermedad]", "<tr><td colspan='2' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaEnfermedad]")

        End If

        Plantilla = Plantilla.Replace("[ListaEnfermedad]", "</table>")

        ''ListaVacuna
        Plantilla = Plantilla.Replace("[ListaVacuna]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaVacuna]")
        Plantilla = Plantilla.Replace("[ListaVacuna]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:210px;  font-size:10px; ' align='left' valign='bottom'><b>Vacuna</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:170px;  font-size:10px; ' align='left' valign='bottom'><b>Dosis</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:110px; font-size:10px; ' align='left' valign='bottom'><b>Edad</b></td></tr>[ListaVacuna]")

        If dsReporte.Tables(2).Rows(0).Item("CodigoRelVacunasFichaMed") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(2).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaVacuna]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:210px; font-size:10px;'>" & dsReporte.Tables(2).Rows(i).Item("Vacuna") & "</td><td align='left' valign='top' style='border:solid 1px #000000; width:170px; font-size:10px;'>" & dsReporte.Tables(2).Rows(i).Item("Dosis") & "</td><td align='left' valign='top' style='border:solid 1px #000000; width:110px; font-size:10px;'>" & IIf(dsReporte.Tables(2).Rows(i).Item("Edad") = 0, "", dsReporte.Tables(2).Rows(i).Item("Edad")) & " años " & "</td> </tr>[ListaVacuna]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaVacuna]", "<tr><td colspan='3' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaVacuna]")

        End If

        Plantilla = Plantilla.Replace("[ListaVacuna]", "</table>")

        ''ListaAlergia
        Plantilla = Plantilla.Replace("[ListaAlergia]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaAlergia]")
        Plantilla = Plantilla.Replace("[ListaAlergia]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px; ' align='left' valign='bottom'><b>Alergia</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:193px; font-size:10px; ' align='left' valign='bottom'><b>Tipo Alergia</b></td></tr>[ListaAlergia]")

        If dsReporte.Tables(3).Rows(0).Item("CodigoRelFichaMedAlergias") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(3).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaAlergia]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px;'>" & dsReporte.Tables(3).Rows(i).Item("Alergia") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:193px;font-size:10px;'>" & dsReporte.Tables(3).Rows(i).Item("TipoAlergia") & "</td></tr>[ListaAlergia]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaAlergia]", "<tr><td colspan='2' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaAlergia]")

        End If

        Plantilla = Plantilla.Replace("[ListaAlergia]", "</table>")


        ''ListaCaracteristicaPiel
        Plantilla = Plantilla.Replace("[ListaCaracteristicaPiel]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaCaracteristicaPiel]")
        Plantilla = Plantilla.Replace("[ListaCaracteristicaPiel]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:450px; font-size:10px;' align='left' valign='bottom'><b>Características de la Piel</b></td></tr>[ListaCaracteristicaPiel]")

        If dsReporte.Tables(4).Rows(0).Item("CodigoRelFichaMedCaractPiel") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(4).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaCaracteristicaPiel]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:450px; font-size:10px;'>" & dsReporte.Tables(4).Rows(i).Item("CaracteristicaPiel") & "</td></tr>[ListaCaracteristicaPiel]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaCaracteristicaPiel]", "<tr><td align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaCaracteristicaPiel]")

        End If

        Plantilla = Plantilla.Replace("[ListaCaracteristicaPiel]", "</table>")

        ''ListaMedicamento
        Plantilla = Plantilla.Replace("[ListaMedicamento]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaMedicamento]")
        Plantilla = Plantilla.Replace("[ListaMedicamento]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:130px;  font-size:10px; ' align='left' valign='bottom'><b>Medicamento</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:120px; font-size:10px; ' align='left' valign='bottom'><b>Presentación / Cantidad</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:100px; font-size:10px; ' align='left' valign='bottom'><b>Dosis</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:100px; font-size:10px; ' align='left' valign='bottom'>Observaciones</td></tr>[ListaMedicamento]")

        If dsReporte.Tables(5).Rows(0).Item("CodigoRelFichaAtenMedicamentos") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(5).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaMedicamento]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:130px; font-size:10px;'>" & dsReporte.Tables(5).Rows(i).Item("Medicamento") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:120px;font-size:10px;'>" & dsReporte.Tables(5).Rows(i).Item("PresentCant") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:100px;font-size:10px;'>" & dsReporte.Tables(5).Rows(i).Item("DosisMedicamento") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:100px;font-size:10px;'>" & dsReporte.Tables(5).Rows(i).Item("Observaciones") & "</td></tr>[ListaMedicamento]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaMedicamento]", "<tr><td colspan='4' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaMedicamento]")

        End If

        Plantilla = Plantilla.Replace("[ListaMedicamento]", "</table>")


        ''ListaHospitalizacion
        Plantilla = Plantilla.Replace("[ListaHospitalizacion]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaHospitalizacion]")
        Plantilla = Plantilla.Replace("[ListaHospitalizacion]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px; ' align='left' valign='bottom'><b>Fecha de Hospitalización</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:193px;  font-size:10px; ' align='left' valign='bottom'><b>Hospitalización</b></td></tr>[ListaHospitalizacion]")

        If dsReporte.Tables(6).Rows(0).Item("CodigoRelFichaMedMotivoHosp") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(6).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaHospitalizacion]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px;'>" & dsReporte.Tables(6).Rows(i).Item("FechaHospitalizacion") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:193px;font-size:10px;'>" & dsReporte.Tables(6).Rows(i).Item("Hospitalizacion") & "</td></tr>[ListaHospitalizacion]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaHospitalizacion]", "<tr><td colspan='2' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaHospitalizacion]")

        End If

        Plantilla = Plantilla.Replace("[ListaHospitalizacion]", "</table>")


        ''ListaOperacion
        Plantilla = Plantilla.Replace("[ListaOperacion]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaOperacion]")
        Plantilla = Plantilla.Replace("[ListaOperacion]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px; ' align='left' valign='bottom'><b>Fecha de Operación</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:193px; font-size:10px; ' align='left' valign='bottom'><b>Operación</b></td></tr>[ListaOperacion]")

        If dsReporte.Tables(7).Rows(0).Item("CodigoRelFichaMedOperaciones") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(7).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaOperacion]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:257px; font-size:10px;'>" & dsReporte.Tables(7).Rows(i).Item("FechaOperacion") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:193px;font-size:10px;'>" & dsReporte.Tables(7).Rows(i).Item("Operacion") & "</td></tr>[ListaOperacion]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaOperacion]", "<tr><td colspan='2' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaOperacion]")

        End If

        Plantilla = Plantilla.Replace("[ListaOperacion]", "</table>")

        ''ListaControlPesotalla
        Plantilla = Plantilla.Replace("[ListaControlPesotalla]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaControlPesotalla]")
        Plantilla = Plantilla.Replace("[ListaControlPesotalla]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:130px; font-size:10px; ' align='left' valign='bottom'><b>Fecha</b> </td><td align='left' valign='top' style='border:solid 1px #000000; width:120px; font-size:10px;' align='left' valign='bottom'><b>Talla</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:100px; font-size:10px;' align='left' valign='bottom'><b>Peso</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:100px; font-size:10px; ' align='left' valign='bottom'><b>Observaciones</b></td></tr>[ListaControlPesotalla]")

        If dsReporte.Tables(8).Rows(0).Item("CodigoControlPesoTalla") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(8).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaControlPesotalla]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:130px; font-size:10px;'>" & dsReporte.Tables(8).Rows(i).Item("FechaControl") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:120px;font-size:10px;'>" & dsReporte.Tables(8).Rows(i).Item("Talla") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:100px;font-size:10px;'>" & dsReporte.Tables(8).Rows(i).Item("Peso") & "</td><td align='left' valign='top' style='border:solid 1px #000000;width:100px;font-size:10px;'>" & IIf(dsReporte.Tables(8).Rows(i).Item("Observaciones").ToString.Length = 0, " ", dsReporte.Tables(8).Rows(i).Item("Observaciones")) & "</td></tr>[ListaControlPesotalla]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaControlPesotalla]", "<tr><td colspan='4' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaControlPesotalla]")

        End If

        Plantilla = Plantilla.Replace("[ListaControlPesotalla]", "</table>")

        ''ListaTipoControl
        Plantilla = Plantilla.Replace("[ListaTipoControl]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 450px;'>[ListaTipoControl]")
        Plantilla = Plantilla.Replace("[ListaTipoControl]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:210px; font-size:10px;' align='left' valign='bottom'><b>Fecha de Control</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:170px;font-size:10px;' align='left' valign='bottom'><b>Tipo de Control</b></td><td align='left' valign='top' style='border:solid 1px #000000; width:110px;font-size:10px;' align='left' valign='bottom'><b>Resultado</b></td></tr>[ListaTipoControl]")

        If dsReporte.Tables(9).Rows(0).Item("CodigoRelFichaMedTiposControles") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(9).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaTipoControl]", "<tr><td align='left' valign='top' style='border:solid 1px #000000; width:210px; font-size:10px;'>" & dsReporte.Tables(9).Rows(i).Item("FechaControl") & "</td><td align='left' valign='top' style='border:solid 1px #000000; width:170px; font-size:10px;'>" & dsReporte.Tables(9).Rows(i).Item("TipoControl") & "</td><td align='left' valign='top' style='border:solid 1px #000000; width:110px; font-size:10px;'>" & IIf(dsReporte.Tables(9).Rows(0).Item("Resultado").ToString.Length = 0, " ", dsReporte.Tables(9).Rows(0).Item("Resultado")) & "</td></tr>[ListaTipoControl]")

            Next

        Else

            Plantilla = Plantilla.Replace("[ListaTipoControl]", "<tr><td colspan='3' align='left' valign='top' style='width:100%'>&nbsp;</td></tr>[ListaTipoControl]")

        End If

        Plantilla = Plantilla.Replace("[ListaTipoControl]", "</table>")

        Return Plantilla
    End Function

    ''' <summary>
    ''' Llena el documento HTML con la información de la Ficha Familiar que se envió para su exportación.
    ''' </summary>
    ''' <param name="Plantilla">Cadena de caracteres con codigo HTML (Plantilla del documento a llenar)</param>
    ''' <param name="dsReporte">Conjunto de tablas temporales con la informacion a ingresar en el plantilla</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres de codigo HTML con la información a exportar en el navegador o documento de WORD</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function LlenarPlantillaFichaFamiliarHtml(ByVal Plantilla As String, ByVal dsReporte As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""

        'Datos Personales
        Plantilla = Plantilla.Replace("[Nombre_Completo]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreCompleto").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreCompleto")))
        Plantilla = Plantilla.Replace("[Sexo]", IIf(dsReporte.Tables(0).Rows(0).Item("DescSexo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescSexo")))
        Plantilla = Plantilla.Replace("[Edad]", IIf(dsReporte.Tables(0).Rows(0).Item("Edad").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Edad")))

        Plantilla = Plantilla.Replace("[Tipo_Documento]", IIf(dsReporte.Tables(0).Rows(0).Item("DescTipoDocIdentidad").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescTipoDocIdentidad")))
        Plantilla = Plantilla.Replace("[Numero_Documento]", IIf(dsReporte.Tables(0).Rows(0).Item("NumeroDocIdentidad").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NumeroDocIdentidad")))
        Plantilla = Plantilla.Replace("[Estado_Civil]", IIf(dsReporte.Tables(0).Rows(0).Item("DescEstadoCivil").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescEstadoCivil")))
        Plantilla = Plantilla.Replace("[Vive]", IIf(dsReporte.Tables(0).Rows(0).Item("DescVive").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescVive")))
        Plantilla = Plantilla.Replace("[Fecha_Defunción]", IIf(dsReporte.Tables(0).Rows(0).Item("FechaDefuncionStr").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("FechaDefuncionStr")))

        'Datos Nacimiento
        Plantilla = Plantilla.Replace("[Fecha_Nacimiento]", IIf(dsReporte.Tables(0).Rows(0).Item("FechaNacimiento").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("FechaNacimiento")))
        If dsReporte.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then
            Plantilla = Plantilla.Replace("[Nacionalidad]", IIf(dsReporte.Tables(1).Rows(0).Item("Descripcion").ToString.Length = 0, "-", dsReporte.Tables(1).Rows(0).Item("Descripcion")))
        End If
        'Datos Adicionales
        Plantilla = Plantilla.Replace("[Profesa_Religion]", IIf(dsReporte.Tables(0).Rows(0).Item("DescProfesaReligion").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescProfesaReligion")))
        Plantilla = Plantilla.Replace("[Religion]", IIf(dsReporte.Tables(0).Rows(0).Item("DescReligion").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescReligion")))
        Plantilla = Plantilla.Replace("[Nombre_Iglesia]", IIf(dsReporte.Tables(0).Rows(0).Item("NombreIglesia").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NombreIglesia")))
        Plantilla = Plantilla.Replace("[Celular]", IIf(dsReporte.Tables(0).Rows(0).Item("CelularPersonal").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("CelularPersonal")))
        Plantilla = Plantilla.Replace("[Servicio_Radio]", IIf(dsReporte.Tables(0).Rows(0).Item("DescServicioRadioDomicilio").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescServicioRadioDomicilio")))
        Plantilla = Plantilla.Replace("[Numero_Radio]", IIf(dsReporte.Tables(0).Rows(0).Item("NumeroRadioPersonal").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NumeroRadioPersonal")))
        Plantilla = Plantilla.Replace("[Correo_Personal]", IIf(dsReporte.Tables(0).Rows(0).Item("EmailPersonal").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("EmailPersonal")))

        'Lista Idiomas
        Plantilla = Plantilla.Replace("[ListaIdiomas]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[ListaIdiomas]")
        If dsReporte.Tables(2).Rows(0).Item("CodigoRelacion") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(2).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaIdiomas]", "<tr><td align='left' valign='top' style='width:100%'>" & dsReporte.Tables(2).Rows(i).Item("Descripcion") & "<td></tr>[ListaIdiomas]")

            Next

        Else
            Plantilla = Plantilla.Replace("[ListaIdiomas]", "<tr><td align='left' valign='top' style='width:100%'>-<td></tr>[ListaIdiomas]")
        End If
        Plantilla = Plantilla.Replace("[ListaIdiomas]", "</table>")

        'Datos Domicilio
        Plantilla = Plantilla.Replace("[Pais_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("DescPaisDomicilio").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescPaisDomicilio")))
        Plantilla = Plantilla.Replace("[Departamento_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("DescUbigeoDomicilioDepartamento").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescUbigeoDomicilioDepartamento")))
        Plantilla = Plantilla.Replace("[Provincia_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("DescUbigeoDomicilioProvincia").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescUbigeoDomicilioProvincia")))
        Plantilla = Plantilla.Replace("[Distrito_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("DescUbigeoDomicilioDistrito").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescUbigeoDomicilioDistrito")))
        Plantilla = Plantilla.Replace("[Urbanizacion_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("UrbanizacionDomicilio").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("UrbanizacionDomicilio")))
        Plantilla = Plantilla.Replace("[Direccion_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("DireccionDomicilio").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DireccionDomicilio")))
        Plantilla = Plantilla.Replace("[Referencia_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("ReferenciaDomicilio").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("ReferenciaDomicilio")))
        Plantilla = Plantilla.Replace("[Telefono_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("TelefonoDomicilio").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("TelefonoDomicilio")))
        Plantilla = Plantilla.Replace("[AccesoInternet_Domicilio]", IIf(dsReporte.Tables(0).Rows(0).Item("DescAccesoInternetDomicilio").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescAccesoInternetDomicilio")))

        Plantilla = Plantilla.Replace("[Situación_Laboral]", IIf(dsReporte.Tables(0).Rows(0).Item("DescSituacionLaboral").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescSituacionLaboral")))
        Plantilla = Plantilla.Replace("[Ocupacion_Cargo]", IIf(dsReporte.Tables(0).Rows(0).Item("Ocupacion").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("Ocupacion")))
        Plantilla = Plantilla.Replace("[Centro_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("CentroTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("CentroTrabajo")))
        Plantilla = Plantilla.Replace("[Direccion_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("DireccionTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DireccionTrabajo")))
        Plantilla = Plantilla.Replace("[Pais_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("DescPaisTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescPaisTrabajo")))
        Plantilla = Plantilla.Replace("[Departamento_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("DescUbigeoTrabajoDepartamento").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescUbigeoTrabajoDepartamento")))
        Plantilla = Plantilla.Replace("[Provincia_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("DescUbigeoTrabajoProvincia").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescUbigeoTrabajoProvincia")))
        Plantilla = Plantilla.Replace("[Distrito_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("DescUbigeoTrabajoDistrito").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescUbigeoTrabajoDistrito")))
        Plantilla = Plantilla.Replace("[Telefono_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("TelefonoTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("TelefonoTrabajo")))
        Plantilla = Plantilla.Replace("[Celular_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("CelularTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("CelularTrabajo")))
        Plantilla = Plantilla.Replace("[Servicio_Radio_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("DescServicioRadioTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescServicioRadioTrabajo")))
        Plantilla = Plantilla.Replace("[Número_Radio_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("NumeroRadioTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("NumeroRadioTrabajo")))
        Plantilla = Plantilla.Replace("[Correo_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("EmailTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("EmailTrabajo")))
        Plantilla = Plantilla.Replace("[AccesoInternet_Trabajo]", IIf(dsReporte.Tables(0).Rows(0).Item("DescAccesoInternetTrabajo").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescAccesoInternetTrabajo")))

        Plantilla = Plantilla.Replace("[ExAlumno]", IIf(dsReporte.Tables(0).Rows(0).Item("DescExAlumno").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescExAlumno")))
        Plantilla = Plantilla.Replace("[Colegio_Egreso]", IIf(dsReporte.Tables(0).Rows(0).Item("ColegioEgreso").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("ColegioEgreso")))
        Plantilla = Plantilla.Replace("[Anio_Egreso]", IIf(dsReporte.Tables(0).Rows(0).Item("AnioEgreso").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("AnioEgreso")))
        Plantilla = Plantilla.Replace("[Continuo_Estudios]", IIf(dsReporte.Tables(0).Rows(0).Item("ContinuoEstudios").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("ContinuoEstudios")))
        Plantilla = Plantilla.Replace("[Nivel_Instruccion]", IIf(dsReporte.Tables(0).Rows(0).Item("DescNivelInstruccion").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescNivelInstruccion")))
        Plantilla = Plantilla.Replace("[Escolaridad_Ministerio]", IIf(dsReporte.Tables(0).Rows(0).Item("DescEscolaridadMinisterio").ToString.Length = 0, "-", dsReporte.Tables(0).Rows(0).Item("DescEscolaridadMinisterio")))

        'Lista Profesiones
        Plantilla = Plantilla.Replace("[ListaProfesiones]", "<table cellpadding='0' cellspacing='0' border='0' style='width: 400px;'>[ListaProfesiones]")
        If dsReporte.Tables(3).Rows(0).Item("CodigoRelacion") <> -1 Then

            For i As Integer = 0 To dsReporte.Tables(3).Rows.Count - 1

                Plantilla = Plantilla.Replace("[ListaProfesiones]", "<tr><td align='left' valign='top' style='width:100%'>" & dsReporte.Tables(3).Rows(i).Item("Descripcion") & "<td></tr>[ListaProfesiones]")

            Next

        Else
            Plantilla = Plantilla.Replace("[ListaProfesiones]", "<tr><td align='left' valign='top' style='width:100%'>-<td></tr>[ListaProfesiones]")
        End If
        Plantilla = Plantilla.Replace("[ListaProfesiones]", "</table>")

        Return Plantilla
    End Function


    ''' <summary>
    ''' Llena el documento HTML con la información de la Ficha Familiar que se envió para su exportación.
    ''' </summary>
    ''' <param name="Plantilla">Cadena de caracteres con codigo HTML (Plantilla del documento a llenar)</param>
    ''' <param name="ds">Conjunto de tablas temporales con la informacion a ingresar en el plantilla</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres de codigo HTML con la información a exportar en el navegador o documento de WORD</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     04/04/2012
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function LlenarPlantillaMidTermReportHtml_IF(ByVal Plantilla As String, ByVal ds As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim dt_Alumnos As DataTable = ds.Tables(0)
        Dim dt_Cursos As DataTable = ds.Tables(1)
        Dim dt_Criterios As DataTable = ds.Tables(2)
        Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
        Dim dt_Notas As DataTable = ds.Tables(4)
        Dim dt_Observacion As DataTable = ds.Tables(5)
        Dim dt_ObservacionDetalle As DataTable = ds.Tables(6)
        Dim str_CodigoAlumno As String

        'For cont As Integer = 0 To dt_Alumnos.Rows.Count - 1

        str_CodigoAlumno = dt_Alumnos.Rows(0).Item("CodigoAlumno")

        Dim dv_Cursos As DataView = dt_Cursos.DefaultView
        dv_Cursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_Criterios As DataView = dt_Criterios.DefaultView
        dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_CriteriosYCalificativos As DataView = dt_CriteriosYCalificativos.DefaultView
        dv_CriteriosYCalificativos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_Notas As DataView = dt_Notas.DefaultView
        dv_Notas.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_Observacion As DataView = dt_Observacion.DefaultView
        dv_Observacion.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        'Datos Personales
        'plantilla padre
     
        Plantilla = Plantilla.Replace("[NombreCompleto]", dv_Observacion(0).Item("NombreCompleto").ToString)
        Plantilla = Plantilla.Replace("[Aula]", dv_Observacion(0).Item("DescGrado").ToString)
        Plantilla = Plantilla.Replace("[Bimestre]", dv_Observacion(0).Item("DescMesProgramacion").ToString)

        'Aqui va el detalle
        Plantilla = Plantilla.Replace("[ListadoDetalleNotasCabecera]", "<table cellpadding='0' cellspacing='0' border='0' style='border:solid 1px #000000; width:600px; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>" & _
                                                                              "<tr>" & _
                                                                              "<td align='center' valign='top' style='width:250px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' >" & _
                                                                              "<b><span>Subject</span></b> " & _
                                                                              "</td>[ListadoDetalleNotasCabecera]")

        For i As Integer = 0 To dv_Criterios.Count - 1
            Plantilla = Plantilla.Replace("[ListadoDetalleNotasCabecera]", "<td align='center' valign='top' style='border-left: solid 1px #000000; width:60px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' >" & dv_Criterios(i).Item("AbreviaturaCriterio").ToString & "</td>[ListadoDetalleNotasCabecera]")
        Next


        Dim int_cantCursos As Integer = CInt(dv_Cursos.Count)
        Dim int_ConCur As Integer = 0

        Dim int_CodigoCurso, int_CodigoCriterio, int_Filas, int_Columnas As Integer
        Dim str_DescCurso As String = ""

        int_Filas = 0
        int_Columnas = 0

        Plantilla = Plantilla.Replace("[ListadoDetalleNotasCabecera]", "<td " & _
                                     "align='center' valign='top' style='border-left: solid 1px #000000;  width:230px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' ><b><span>Tutor's Comment</span></b> </td></tr>[ListadoDetalleNotasCabecera]")

        Plantilla = Plantilla.Replace("[ListadoDetalleNotasCabecera]", "<tr><td align='center' valign='top' style='border-left: solid 1px #000000; width:60px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' colspan='3'>[DETALLE]</td><td align='left' valign='top' style='border-left: solid 1px #000000; width:60px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' >[COMENTARIO]</td></tr><table>")

        If dv_Cursos.Count > 0 Then
            Plantilla = Plantilla.Replace("[DETALLE]", "<table><tr>[ListadoDetalleNotasCabecera]</tr></table>")


            While int_Filas <= dv_Cursos.Count - 1 ' Filas Cursos
                int_CodigoCurso = dv_Cursos(int_Filas).Item("CodigoCurso").ToString
                str_DescCurso = dv_Cursos(int_Filas).Item("DescNombreCurso").ToString

                Plantilla = Plantilla.Replace("[ListadoDetalleNotasCabecera]", "<tr>" & _
                "<td align='left' valign='top' style=' border-top: solid 1px #000000; border-right: solid 1px #000000; width:250px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' >" & _
                "<b><span>" & str_DescCurso & "</span></b> </td>[ListadoDetalleNotasCabecera]")


                While int_Columnas <= dv_Criterios.Count - 1 ' Columnas Criterios
                    int_CodigoCriterio = dv_Criterios(int_Columnas).Item("CodigoCriterio")
                    Plantilla = Plantilla.Replace("[ListadoDetalleNotasCabecera]", "<td align='center' valign='top' style='border-top: solid 1px #000000; border-right: solid 1px #000000; width:60px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' >" & _
                    obtenerNota(dv_Notas, int_CodigoCurso, int_CodigoCriterio) & "</td>[ListadoDetalleNotasCabecera]")

                    int_Columnas += 1
                End While

                If int_Filas = 0 Then
                    Plantilla = Plantilla.Replace("[COMENTARIO]", dv_Observacion(0).Item("Observacion") & "[COMENTARIO]")

                    For dtObsDet As Integer = 0 To dt_ObservacionDetalle.Rows.Count - 1

                        Plantilla = Plantilla.Replace("[COMENTARIO]", "<b>" & dt_ObservacionDetalle.Rows(dtObsDet).Item("Curso").ToString & ": </b>" & _
                                                       dt_ObservacionDetalle.Rows(dtObsDet).Item("ObservacionProfesor").ToString & "<BR><BR>[COMENTARIO]")

                    Next
                    Plantilla = Plantilla.Replace("[COMENTARIO]", "")

                End If


                str_DescCurso = ""
                int_CodigoCriterio = 0
                int_Columnas = 0
                int_Filas += 1
            End While

        End If
       
        Plantilla = Plantilla.Replace("[ListadoDetalleNotasCabecera]", "")


        int_CodigoCriterio = 0

        ' Estructura y Cabecera de la Tabla Criterios y Calificativos
        Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<table cellpadding='0' cellspacing='0' border='0' style='width:600px; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'><tr>[ListadoDetalleNotas]")

        For ii As Integer = 0 To dv_Criterios.Count - 1
            Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<td " & _
                                    "align='center' valign='top' style='  width:300px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' ><b><u><span>" & _
                                    dv_Criterios(ii).Item("Criterio") & "</span></u></b></td>[ListadoDetalleNotas]")
        Next


        Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "</tr><tr>[ListadoDetalleNotas]")


        For i As Integer = 0 To dv_Criterios.Count - 1

            int_CodigoCriterio = dv_Criterios(i).Item("CodigoCriterio")

            Dim dtAux As New DataTable
            dtAux = dv_CriteriosYCalificativos.ToTable

            Dim dv As DataView = dtAux.DefaultView
            With dv
                .RowFilter = "1=1 and CodigoCriterio = '" & int_CodigoCriterio & "' and CodigoAlumno = '" & str_CodigoAlumno & "'"
                .Sort = "OrdenCalificativo ASC"
            End With

            Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<td align='left' valign='top' style='width:300px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' >" & _
                  "<table cellpadding='0' cellspacing='0' border='0' style='width:300px; text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 11px; vertical-align: top;'>[ListadoDetalleNotas]")


            For j As Integer = 0 To dv.Count - 1
                Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<tr><td " & _
                "align='left' valign='top' style='padding-right :30px; width:300px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' ><b><span>" & _
                                      dv(j).Item("Nota") & " - " & dv(j).Item("Calificativo") & " : " & dv(j).Item("LeyendaIngles") & " (" & dv(j).Item("LeyendaEspaniol") & " ) </span></b> </td></tr>[ListadoDetalleNotas]")

                Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<tr><td " & _
                "align='left' valign='top' style='width:300px; font-family: Arial, Helvetica, sans-serif; font-size: 11px' >" & _
                                      "&nbsp;&nbsp;&nbsp; </td></tr>[ListadoDetalleNotas]")

            Next

            Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "</table></td>[ListadoDetalleNotas]")
            int_CodigoCriterio = 0
        Next
        'Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "</tr></table>[ListadoDetalleNotas]")

        Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "")

        Return Plantilla
    End Function

    ''' <summary>
    ''' Llena el documento HTML con la información de la Ficha Familiar que se envió para su exportación.
    ''' </summary>
    ''' <param name="Plantilla">Cadena de caracteres con codigo HTML (Plantilla del documento a llenar)</param>
    ''' <param name="ds">Conjunto de tablas temporales con la informacion a ingresar en el plantilla</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres de codigo HTML con la información a exportar en el navegador o documento de WORD</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     27/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Private Shared Function LlenarPlantillaWeeklyReportHtml(ByVal Plantilla As String, ByVal ds As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As String

        Dim dt_Alumnos As DataTable = ds.Tables(0)
        Dim dt_Cursos As DataTable = ds.Tables(1)
        Dim dt_Criterios As DataTable = ds.Tables(2)
        Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
        Dim dt_Notas As DataTable = ds.Tables(4)
        Dim dt_CabeceraTutor As DataTable = ds.Tables(5)
        Dim dt_Leyenda As DataTable = ds.Tables(6)
        Dim dt_GrupoCriterioEvaluacion As DataTable = ds.Tables(7)
        Dim str_CodigoAlumno As String

        'For cont As Integer = 0 To dt_Alumnos.Rows.Count - 1

        str_CodigoAlumno = dt_Alumnos.Rows(0).Item("CodigoAlumno")

        Dim dv_Cursos As DataView = dt_Cursos.DefaultView
        dv_Cursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        'Dim dv_Criterios As DataView = dt_Criterios.DefaultView
        'dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_CriteriosYCalificativos As DataView = dt_CriteriosYCalificativos.DefaultView
        dv_CriteriosYCalificativos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_Notas As DataView = dt_Notas.DefaultView
        dv_Notas.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_CabeceraTutor As DataView = dt_CabeceraTutor.DefaultView
        dv_CabeceraTutor.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        'Datos Personales
        'plantilla padre
        Plantilla = Plantilla.Replace("[lblName]", dv_CabeceraTutor(0).Item("NombreCompleto").ToString)
        Plantilla = Plantilla.Replace("[lblClass]", dv_CabeceraTutor(0).Item("DescGrado").ToString & " " & dv_CabeceraTutor(0).Item("DescAula").ToString)
        Plantilla = Plantilla.Replace("[lblTutor]", dv_CabeceraTutor(0).Item("DescPersonaTutor").ToString)

        Plantilla = Plantilla.Replace("[lblCalificativo1]", dt_Leyenda.Rows(0).Item("DescCalificativoWeekly").ToString)
        Plantilla = Plantilla.Replace("[lblCalificativo2]", dt_Leyenda.Rows(1).Item("DescCalificativoWeekly").ToString)
        Plantilla = Plantilla.Replace("[lblCalificativo3]", dt_Leyenda.Rows(2).Item("DescCalificativoWeekly").ToString)

        Plantilla = Plantilla.Replace("[lblFecha]", dv_CabeceraTutor(0).Item("DescFecha").ToString)
        Plantilla = Plantilla.Replace("[lblBimestre]", dv_CabeceraTutor(0).Item("DescBimestre").ToString)
        Plantilla = Plantilla.Replace("[lblSemana]", dv_CabeceraTutor(0).Item("DesSemana").ToString)
        'Aqui va el detalle
        Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<tr>" & _
                                        "<td  width='190pt' height='25pt' style='text-align: center; font-size:10pt; font-family:Arial;'> <b><span>Subject</span></b> </td>[ListadoDetalleNotas]")

        For i As Integer = 0 To dv_Cursos.Count - 1
            Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<td  width='50pt' height='25pt' style='text-align: center; font-size:10pt; font-family:Arial;'>" & dv_Cursos(i).Item("DescNombreCurso").ToString & "</td>[ListadoDetalleNotas]")
        Next

        Dim int_cantCursos As Integer = 11 - CInt(dv_Cursos.Count)
        Dim int_ConCur As Integer = 0

        If int_cantCursos > 0 Then

            While int_ConCur <= int_cantCursos - 1
                Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<td  width='50pt' height='25pt' style='text-align: center; font-size:10pt; font-family:Arial;'>&nbsp;</td>[ListadoDetalleNotas]")
                int_ConCur = int_ConCur + 1
            End While
            int_ConCur = 0
        End If

        'Detalle Notas
        Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "</tr>[ListadoDetalleNotas]")

        Dim int_CodigoCriterio, int_CodigoCurso, int_Filas, int_Columnas As Integer
        Dim str_DescCriterio As String = ""

        int_Filas = 0
        int_Columnas = 0

        Dim int_CodigoGrupoCriterio As Integer = 0
        Dim int_CantidadCriterios As Integer = 0
        Dim str_CodigoGrupoCriterio As String = ""
        Dim int_cantAumFila = 3
        Dim dv_Criterios As DataView = dt_Criterios.DefaultView

        For i As Integer = 0 To dt_GrupoCriterioEvaluacion.Rows.Count - 1
            int_CodigoGrupoCriterio = dt_GrupoCriterioEvaluacion.Rows(i).Item("CodigoGrupoCriterio")
            str_CodigoGrupoCriterio = dt_GrupoCriterioEvaluacion.Rows(i).Item("GrupoCriterioEvaluacion")
            Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<tr>" & _
                                      "<td  width='190pt' height='20pt' style='text-align: left; font-size:10pt; font-family:Arial;'> <b><span>" & str_CodigoGrupoCriterio & "</span></b> </td>[ListadoDetalleNotas]")

            dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "' and CodigoGrupoCriterio = '" & CStr(int_CodigoGrupoCriterio) & "'"

            While int_ConCur <= 10
                Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<td  width='50pt' height='20pt' style='text-align: left; font-size:6pt; font-family:Arial;'>&nbsp;</td>[ListadoDetalleNotas]")
                int_ConCur = int_ConCur + 1
            End While
            int_ConCur = 0

            Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "</tr>[ListadoDetalleNotas]")

            While int_Filas <= dv_Criterios.Count - 1 ' Filas Criterios

                int_CodigoCriterio = dv_Criterios(int_Filas).Item("CodigoCriterio").ToString
                str_DescCriterio = dv_Criterios(int_Filas).Item("Criterio").ToString
                Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<tr>" & _
                                    "<td  width='190pt' height='15pt' style='text-align: left; font-size:6pt; font-family:Arial;'> <span>" & str_DescCriterio & "</span> </td>[ListadoDetalleNotas]")

                While int_Columnas <= dv_Cursos.Count - 1 ' Columnas cursos
                    int_CodigoCurso = dv_Cursos(int_Columnas).Item("CodigoCurso")
                    Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<td  width='50pt' height='15pt' style='text-align: center; font-size:6pt; font-family:Arial;'>" & obtenerNota(dv_Notas, int_CodigoCurso, int_CodigoCriterio) & "</td>[ListadoDetalleNotas]")
                    int_Columnas += 1
                End While

                If int_cantCursos > 0 Then

                    While int_ConCur <= int_cantCursos - 1
                        Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "<td  width='50pt' height='15pt' style='text-align: center; font-size:6pt; font-family:Arial;'>&nbsp;</td>[ListadoDetalleNotas]")
                        int_ConCur = int_ConCur + 1
                    End While
                    int_ConCur = 0
                End If

                int_CantidadCriterios = dv_Criterios.Count
                str_DescCriterio = ""
                int_CodigoCurso = 0
                int_Columnas = 0
                int_Filas += 1
            End While
            int_Filas = 0
            int_CantidadCriterios = int_CantidadCriterios + 1
            int_cantAumFila = int_cantAumFila + int_CantidadCriterios
            Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "</tr>[ListadoDetalleNotas]")
        Next
        Plantilla = Plantilla.Replace("[ListadoDetalleNotas]", "")

        'Plantilla Tutor
        Plantilla = Plantilla.Replace("[lblName2]", dv_CabeceraTutor(0).Item("NombreCompleto").ToString)
        Plantilla = Plantilla.Replace("[lblClass2]", dv_CabeceraTutor(0).Item("DescGrado").ToString & " " & dv_CabeceraTutor(0).Item("DescAula").ToString)
        Plantilla = Plantilla.Replace("[lblTutor2]", dv_CabeceraTutor(0).Item("DescPersonaTutor").ToString)

        Plantilla = Plantilla.Replace("[lblCalificativo1_2]", dt_Leyenda.Rows(0).Item("DescCalificativoWeekly").ToString)
        Plantilla = Plantilla.Replace("[lblCalificativo2_2]", dt_Leyenda.Rows(1).Item("DescCalificativoWeekly").ToString)
        Plantilla = Plantilla.Replace("[lblCalificativo3_2]", dt_Leyenda.Rows(2).Item("DescCalificativoWeekly").ToString)

        Plantilla = Plantilla.Replace("[lblFecha2]", dv_CabeceraTutor(0).Item("DescFecha").ToString)
        Plantilla = Plantilla.Replace("[lblBimestre2]", dv_CabeceraTutor(0).Item("DescBimestre").ToString)
        Plantilla = Plantilla.Replace("[lblSemana2]", dv_CabeceraTutor(0).Item("DesSemana").ToString)

        'Aqui va el detalle
        Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "<tr>" & _
                                        "<td  width='190pt' height='25pt' style='text-align: center; font-size:10pt; font-family:Arial;'> <b><span>Subject</span></b> </td>[ListadoDetalleNotas2]")

        For i As Integer = 0 To dv_Cursos.Count - 1
            Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "<td  width='50pt' height='25pt' style='text-align: center; font-size:10pt; font-family:Arial;'>" & dv_Cursos(i).Item("DescNombreCurso").ToString & "</td>[ListadoDetalleNotas2]")
        Next

        Dim int_cantCursos2 As Integer = 11 - CInt(dv_Cursos.Count)
        Dim int_ConCur2 As Integer = 0

        If int_cantCursos > 0 Then

            While int_ConCur2 <= int_cantCursos2 - 1
                Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "<td  width='50pt' height='25pt' style='text-align: center; font-size:10pt; font-family:Arial;'>&nbsp;</td>[ListadoDetalleNotas2]")
                int_ConCur2 = int_ConCur2 + 1
            End While
            int_ConCur2 = 0
        End If

        'Detalle Notas
        Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "</tr>[ListadoDetalleNotas2]")

        Dim int_CodigoCriterio2, int_CodigoCurso2, int_Filas2, int_Columnas2 As Integer
        Dim str_DescCriterio2 As String = ""

        int_Filas2 = 0
        int_Columnas2 = 0

        Dim int_CodigoGrupoCriterio2 As Integer = 0
        Dim int_CantidadCriterios2 As Integer = 0
        Dim str_CodigoGrupoCriterio2 As String = ""
        Dim int_cantAumFila2 = 3
        Dim dv_Criterios2 As DataView = dt_Criterios.DefaultView

        For i As Integer = 0 To dt_GrupoCriterioEvaluacion.Rows.Count - 1
            int_CodigoGrupoCriterio2 = dt_GrupoCriterioEvaluacion.Rows(i).Item("CodigoGrupoCriterio")
            str_CodigoGrupoCriterio2 = dt_GrupoCriterioEvaluacion.Rows(i).Item("GrupoCriterioEvaluacion")
            Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "<tr>" & _
                                      "<td  width='190pt' height='20pt' style='text-align: left; font-size:10pt; font-family:Arial;'> <b><span>" & str_CodigoGrupoCriterio2 & "</span></b> </td>[ListadoDetalleNotas2]")

            dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "' and CodigoGrupoCriterio = '" & CStr(int_CodigoGrupoCriterio2) & "'"

            While int_ConCur2 <= 10
                Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "<td  width='50pt' height='20pt' style='text-align: left; font-size:10pt; font-family:Arial;'>&nbsp;</td>[ListadoDetalleNotas2]")
                int_ConCur2 = int_ConCur2 + 1
            End While
            int_ConCur2 = 0

            Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "</tr>[ListadoDetalleNotas2]")

            While int_Filas2 <= dv_Criterios.Count - 1 ' Filas Criterios

                int_CodigoCriterio2 = dv_Criterios(int_Filas2).Item("CodigoCriterio").ToString
                str_DescCriterio2 = dv_Criterios(int_Filas2).Item("Criterio").ToString
                Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "<tr>" & _
                                    "<td  width='190pt' height='15pt' style='text-align: left; font-size:6pt; font-family:Arial;'> <span>" & str_DescCriterio2 & "</span> </td>[ListadoDetalleNotas2]")

                While int_Columnas2 <= dv_Cursos.Count - 1 ' Columnas cursos
                    int_CodigoCurso2 = dv_Cursos(int_Columnas2).Item("CodigoCurso")
                    Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "<td  width='50pt' height='15pt' style='text-align: center; font-size:6pt; font-family:Arial;'>" & obtenerNota(dv_Notas, int_CodigoCurso2, int_CodigoCriterio2) & "</td>[ListadoDetalleNotas2]")
                    int_Columnas2 += 1
                End While

                If int_cantCursos2 > 0 Then

                    While int_ConCur2 <= int_cantCursos2 - 1
                        Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "<td  width='50pt' height='15pt' style='text-align: center; font-size:6pt; font-family:Arial;'>&nbsp;</td>[ListadoDetalleNotas2]")
                        int_ConCur2 = int_ConCur2 + 1
                    End While
                    int_ConCur2 = 0
                End If

                int_CantidadCriterios2 = dv_Criterios.Count
                str_DescCriterio2 = ""
                int_CodigoCurso2 = 0
                int_Columnas2 = 0
                int_Filas2 += 1
            End While
            int_Filas2 = 0
            int_CantidadCriterios2 = int_CantidadCriterios2 + 1
            int_cantAumFila2 = int_cantAumFila2 + int_CantidadCriterios2
            Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "</tr>[ListadoDetalleNotas2]")
        Next
        Plantilla = Plantilla.Replace("[ListadoDetalleNotas2]", "")

        Return Plantilla
    End Function


#Region "Reportes HTML"
    'Reportes Enfermeria

    ''' <summary>
    ''' Devuelve la información enviada en codigo HTML para su exportación en el navegador o en documentos WORD
    ''' </summary>
    ''' <param name="dtReporte">Tabla temporal con la información a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres en codigo HTML a exportar</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     17/02/2011
    ''' Modificado por:        Juan Vento
    ''' Fecha de modificación: 21/02/2011 
    ''' </remarks>
    Public Shared Function ExportarReporteEnfermeria_Html(ByVal dtReporte As System.Data.DataTable, _
                                                          ByVal str_NombreEntidadReporte As String, _
                                                          ByVal bool_Parametros As Boolean, _
                                                          ByVal bool_MostrarTotales As Boolean, _
                                                          ByVal arr_Parametros As ArrayList, _
                                                          ByVal int_TipoReporte As Integer) As String()

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Dim DatosExportacionHtml(1) As String
        Dim nombreArchivo As String = ""

        Try
            nombreArchivo = GetNewName()
            If int_TipoReporte = 2 Or int_TipoReporte = 5 Then
                ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaEnfermeriaWideHtml").ToString()
            Else
                ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaEnfermeriaHtml").ToString()
            End If

            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)
            fileReaderPlantilla = LlenarPlantillaReporteEnfermeria_Html(fileReaderPlantilla, _
                                                                        dtReporte, _
                                                                        str_NombreEntidadReporte, _
                                                                        bool_Parametros, _
                                                                        bool_MostrarTotales, _
                                                                        arr_Parametros, _
                                                                        int_TipoReporte)

            DatosExportacionHtml(0) = fileReaderPlantilla
            DatosExportacionHtml(1) = nombreArchivo

        Catch ex As Exception

        End Try

        Return DatosExportacionHtml

    End Function

    ''' <summary>
    ''' Llena el documento HTML con la información que se envió para su exportación. (Sólo listados)
    ''' </summary>
    ''' <param name="Plantilla">Cadena de caracteres con codigo HTML (Plantilla del documento a llenar)</param>
    ''' <param name="dtReporte">Tabla temporal con la información a ingresar en la plantilla</param>
    ''' <param name="str_NombreEntidadReporte">Titulo del reporte</param>
    ''' <returns>Cadena de caracteres de codigo HTML con la información a exportar en el navegador o documento de WORD</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     17/02/2011
    ''' Modificado por:        Juan Vento
    ''' Fecha de modificación: 21/02/2011 
    ''' </remarks>
    Private Shared Function LlenarPlantillaReporteEnfermeria_Html(ByVal Plantilla As String, _
                                                                  ByVal dtReporte As System.Data.DataTable, _
                                                                  ByVal str_NombreEntidadReporte As String, _
                                                                  ByVal bool_Parametros As Boolean, _
                                                                  ByVal bool_MostrarTotales As Boolean, _
                                                                  ByVal arr_Parametros As ArrayList, _
                                                                  ByVal int_TipoReporte As Integer) As String

        Dim cont_columnas As Integer = 0
        Dim cont_filas As Integer = 0
        Dim plantillaFila As String = ""

        'Fecha Reporte        
        Plantilla = Plantilla.Replace("[fecha_reporte]", "<table border='0' cellpadding='0' cellspacing='0'>[fecha_reporte]</table>")
        Plantilla = Plantilla.Replace("[fecha_reporte]", "<tr><td style='width: 50px; height:17px;' align='left' valign='middle'>Fecha :</td>" + _
                                                             "<td style='width: 50px; height:17px;' align='right' valign='middle'>" & Now.ToShortDateString & "</td></tr>[fecha_reporte]")
        Plantilla = Plantilla.Replace("[fecha_reporte]", "<tr><td style='width: 50px; height:17px;' align='left' valign='middle'>Hora :</td>" + _
                                                             "<td style='width: 50px; height:17px;' align='right' valign='middle'>" & Now.ToShortTimeString & "</td></tr>")

        'Titulo
        Plantilla = Plantilla.Replace("[nombre_reporte]", "</b>" & str_NombreEntidadReporte & "<b>")

        'Pregunto si se visualizaran parametros
        If bool_Parametros Then
            'Parametros
            Plantilla = Plantilla.Replace("[parametros_reporte]", "<br /><table border='0' cellpadding='0' cellspacing='0'>[parametros_reporte]</table>")
            Plantilla = Plantilla.Replace("[parametros_reporte]", "<tr><td colspan='2' style='width: 200px; height:25px;' align='left' valign='middle'>Reporte filtrado por :</td></tr>[parametros_reporte]")

            For Each obj_be_Parametro As be_Parametros In arr_Parametros
                Plantilla = Plantilla.Replace("[parametros_reporte]", "<tr><td style='width: 100px; height:20px;' align='left' valign='middle'>" & obj_be_Parametro.Descripcion & "</td>[parametros_reporte]")
                Plantilla = Plantilla.Replace("[parametros_reporte]", "<td style='width: 100px; height:20px;' align='left' valign='middle'>" & obj_be_Parametro.Valor & "</td></tr>[parametros_reporte]")
            Next
            Plantilla = Plantilla.Replace("[parametros_reporte]", "")

        Else 'No se quieren visualizar los parametros
            Plantilla = Plantilla.Replace("[parametros_reporte]", "")
        End If

        'Detalle cabeceras
        Plantilla = Plantilla.Replace("[detalle_reporte]", "<table id='mytable' border='0' cellpadding='0' cellspacing='0' style='border: solid 1px #000000; font-family: Arial, Helvetica, sans-serif'><tr>[detalle_reporte]</tr>[detalle_detalle]</table>")

        While cont_columnas <= dtReporte.Columns.Count - 1

            If int_TipoReporte = 1 Then

                If cont_columnas = 0 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 1 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 260px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 2 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 200px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 3 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                End If

            ElseIf int_TipoReporte = 2 Then

                If cont_columnas = 0 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 60px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 1 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 220px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 2 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 3 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 60px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 4 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 60px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 5 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 6 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 7 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                End If

            ElseIf int_TipoReporte = 3 Then

                If cont_columnas = 0 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 1 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 260px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 2 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 200px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 3 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                End If

            ElseIf int_TipoReporte = 4 Then

                If cont_columnas = 0 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 40px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 1 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 250px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 2 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 60px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 3 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 80px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 4 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 120px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 5 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 50px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 6 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 60px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                End If

            ElseIf int_TipoReporte = 5 Then

                If cont_columnas = 0 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 200px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 1 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 2 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 50px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 3 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 4 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 5 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 6 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 7 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 50px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                End If

            ElseIf int_TipoReporte = 6 Then

                If cont_columnas = 0 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 1 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 260px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 2 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 150px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 3 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 150px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                End If

            ElseIf int_TipoReporte = 7 Then

                If cont_columnas = 0 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 100px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 1 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 260px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                ElseIf cont_columnas = 2 Then
                    Plantilla = Plantilla.Replace("[detalle_reporte]", "<th style='width: 300px; height:30px; border-bottom: solid 1px #000000; background-color:#ADD8E6; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & dtReporte.Columns(cont_columnas).ColumnName & "</td>[detalle_reporte]")
                End If

            End If

            cont_columnas = cont_columnas + 1

        End While
        Plantilla = Plantilla.Replace("[detalle_reporte]", "")

        'Detalle detalle
        cont_columnas = 0
        Plantilla = Plantilla.Replace("[detalle_detalle]", "<tr>[detalle_contenido]</tr>[detalle_detalle]")

        While cont_filas <= dtReporte.Rows.Count - 1

            If int_TipoReporte = 1 Or int_TipoReporte = 3 Or int_TipoReporte = 6 Then

                While cont_columnas <= dtReporte.Columns.Count - 1

                    If cont_columnas = 0 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 1 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 2 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 3 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    End If
                    cont_columnas = cont_columnas + 1

                End While

            ElseIf int_TipoReporte = 2 Then

                While cont_columnas <= dtReporte.Columns.Count - 1

                    If cont_columnas = 0 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 1 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 2 Then ' Nivel - Subnivel - Grado - Aula

                        Dim arrStr_NSnGA() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_NSnGA = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_NSnGA.Length - 1
                            listaHtml.Append("<li>" & arrStr_NSnGA(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")

                    ElseIf cont_columnas = 3 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 4 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 5 Then
                        'Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")

                        Dim arrStr_Diagnosticos() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_Diagnosticos = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_Diagnosticos.Length - 1
                            listaHtml.Append("<li>" & arrStr_Diagnosticos(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")



                    ElseIf cont_columnas = 6 Then
                        'Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")

                        Dim arrStr_Medicamentos() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_Medicamentos = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_Medicamentos.Length - 1
                            listaHtml.Append("<li>" & arrStr_Medicamentos(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")

                    ElseIf cont_columnas = 7 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    End If
                    cont_columnas = cont_columnas + 1

                End While

            ElseIf int_TipoReporte = 4 Then

                While cont_columnas <= dtReporte.Columns.Count - 1

                    If cont_columnas = 0 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 1 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 2 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 3 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 4 Then 'Detalle Clinicas

                        Dim arrStr_Clinicas() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_Clinicas = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_Clinicas.Length - 1
                            listaHtml.Append("<li>" & arrStr_Clinicas(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 5 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 6 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    End If
                    cont_columnas = cont_columnas + 1

                End While

            ElseIf int_TipoReporte = 5 Then

                While cont_columnas <= dtReporte.Columns.Count - 1

                    If cont_columnas = 0 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 1 Then ' Nivel - Subnivel - Grado - Aula

                        Dim arrStr_NSnGA() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_NSnGA = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_NSnGA.Length - 1
                            listaHtml.Append("<li>" & arrStr_NSnGA(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 2 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 3 Then

                        Dim arrStr_Alergias() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_Alergias = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_Alergias.Length - 1
                            listaHtml.Append("<li>" & arrStr_Alergias(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")

                    ElseIf cont_columnas = 4 Then

                        Dim arrStr_Medicamentos() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_Medicamentos = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_Medicamentos.Length - 1
                            listaHtml.Append("<li>" & arrStr_Medicamentos(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")

                    ElseIf cont_columnas = 5 Then

                        Dim arrStr_Enfermedades() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_Enfermedades = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_Enfermedades.Length - 1
                            listaHtml.Append("<li>" & arrStr_Enfermedades(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")

                    ElseIf cont_columnas = 6 Then

                        Dim arrStr_Familiares() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_Familiares = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, "*")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_Familiares.Length - 1
                            listaHtml.Append("<li>" & arrStr_Familiares(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")

                    ElseIf cont_columnas = 7 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    End If
                    cont_columnas = cont_columnas + 1

                End While


            ElseIf int_TipoReporte = 7 Then

                While cont_columnas <= dtReporte.Columns.Count - 1

                    If cont_columnas = 0 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='center' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 1 Then
                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" & dtReporte.Rows(cont_filas).Item(cont_columnas) & "</td>[detalle_contenido]")
                    ElseIf cont_columnas = 2 Then

                        'Detalle Alergias(Tipo)

                        Dim arrStr_Alergias() As String
                        Dim listaHtml As New System.Text.StringBuilder

                        arrStr_Alergias = Split(dtReporte.Rows(cont_filas).Item(cont_columnas).ToString, ",")
                        listaHtml.Append("<ul>")
                        For i As Integer = 0 To arrStr_Alergias.Length - 1
                            listaHtml.Append("<li>" & arrStr_Alergias(i) & "</li>")
                        Next
                        listaHtml.Append("</ul>")

                        Plantilla = Plantilla.Replace("[detalle_contenido]", "<td style='height:20px; border-color: #000000; border-collapse: collapse; font-size: 10px;' align='left' valign='middle'>" _
                                                      & listaHtml.ToString & _
                                                      "</td>[detalle_contenido]")

                    End If
                    cont_columnas = cont_columnas + 1

                End While

            End If

            Plantilla = Plantilla.Replace("[detalle_contenido]", "")
            Plantilla = Plantilla.Replace("[detalle_detalle]", "<tr>[detalle_contenido]</tr>[detalle_detalle]")

            cont_columnas = 0
            cont_filas = cont_filas + 1
        End While
        Plantilla = Plantilla.Replace("[detalle_contenido]", "")
        Plantilla = Plantilla.Replace("[detalle_detalle]", "[detalle_totales]")

        'Pregunto si se visualizaran los totales
        If bool_MostrarTotales Then
            'Fila Totales
            If int_TipoReporte = 1 Then

                Plantilla = Plantilla.Replace("[detalle_totales]", "<tr><td colspan='4' align='center'><table><tr>[detalle_totales]</tr></table></td></tr>")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 100px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>Total</td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 260px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & IIf(dtReporte.Rows.Count = 0, "0", dtReporte.Compute("COUNT(Codigo)", "").ToString) & "</td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 200px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'></td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 100px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & IIf(dtReporte.Rows.Count = 0, "0", dtReporte.Compute("SUM(Cantidad)", "").ToString) & "</td>")

            ElseIf int_TipoReporte = 3 Then

                Plantilla = Plantilla.Replace("[detalle_totales]", "<tr><td colspan='4' align='center'><table><tr>[detalle_totales]</tr></table></td></tr>")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 100px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>Total</td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 260px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & IIf(dtReporte.Rows.Count = 0, "0", dtReporte.Compute("COUNT(Codigo)", "").ToString) & "</td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 200px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'></td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 100px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & IIf(dtReporte.Rows.Count = 0, "0", dtReporte.Compute("SUM(Cantidad)", "").ToString) & "</td>")

            ElseIf int_TipoReporte = 6 Then

                Plantilla = Plantilla.Replace("[detalle_totales]", "<tr><td colspan='4' align='center'><table><tr>[detalle_totales]</tr></table></td></tr>")

                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 100px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>Total</td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 260px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & IIf(dtReporte.Rows.Count = 0, "0", dtReporte.Compute("COUNT(Codigo)", "").ToString) & "</td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 150px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>" & IIf(dtReporte.Rows.Count = 0, "0", dtReporte.Compute("SUM(Cantidad)", "").ToString) & "</td>[detalle_totales]")
                Plantilla = Plantilla.Replace("[detalle_totales]", "<td style='width: 150px; height:30px; border-top: solid 1px #000000; font-family: Arial, Helvetica, sans-serif; font-size: 12px;' align='center' valign='middle'>100%</td>")

            Else 'Si el reporte no tiene fila de totales
                Plantilla = Plantilla.Replace("[detalle_totales]", "<tr><td colspan='" & dtReporte.Columns.Count & "'></<td></tr>")
            End If
        Else 'No se quieren visualizar los totales
            Plantilla = Plantilla.Replace("[detalle_totales]", "<tr><td colspan='" & dtReporte.Columns.Count & "'></<td></tr>")
        End If

        Return Plantilla
    End Function

#End Region

#End Region


#Region "Exportar : Pdf"

    ''' <summary>
    ''' Exporta reportes de consolidado de matricula en PDF (Soló listados de información)
    ''' </summary>
    ''' <param name="ds">Tabla de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Nombre del reporte</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteConsolidadoMatricula_Pdf(ByVal ds As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As System.IO.MemoryStream

        ' step 1: creation of a document-object
        Dim m As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim document As New Document(PageSize.A4.Rotate)
        Dim font8 As Font = FontFactory.GetFont("ARIAL", 14, Font.BOLD)

        Dim dt_Alumnos As DataTable = ds.Tables(0)
        Dim dt_RespMatricula As DataTable = ds.Tables(1)

        Dim str_CodigoAlumno As String = ""
        Dim str_NombreCompleto As String = ""
        Dim str_Fecharegistro As String = ""
        Dim str_AnioMatricula As String = ""
        Dim str_Nivel As String = ""
        Dim str_Grado As String = ""
        Dim str_ResponsableMatricula As String = ""
        ' step 2:
        ' we create a writer that listens to the document
        ' and directs a PDF-stream to a file
        PdfWriter.GetInstance(document, m)

        ' step 3: we open the document
        document.Open()

        ' step 4: we add a paragraph to the document
        'Dim p1 As Phrase
        'p1 = New Phrase(str_NombreEntidadReporte)

        'Dim paragraph As Paragraph = New Paragraph(p1)
        'paragraph.Alignment = Element.ALIGN_CENTER
        'paragraph.Font = FontFactory.GetFont("ARIAL", 7)
        'document.Add(paragraph)

        'Cabecera
        'For cont As Integer = 0 To dt_Alumnos.Rows.Count - 1

        str_CodigoAlumno = dt_Alumnos.Rows(0).Item("CodigoAlumno")
        str_NombreCompleto = dt_Alumnos.Rows(0).Item("NombreCompleto")
        str_Fecharegistro = dt_Alumnos.Rows(0).Item("FechaRegistro")
        str_AnioMatricula = dt_Alumnos.Rows(0).Item("AnioMatricula")
        str_Nivel = dt_Alumnos.Rows(0).Item("NivelMatricula")
        str_Grado = dt_Alumnos.Rows(0).Item("GradoMatricula")
        str_ResponsableMatricula = dt_RespMatricula.Rows(0).Item("NombreCompletoFamiliar")

        'Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTablaTitulo As PdfPTable = New PdfPTable(3)
        Dim oTablaDetalle As PdfPTable = New PdfPTable(1)
        'Dim oTablaNotas As PdfPTable = New PdfPTable(12)
        'Dim oTablaComentarioCopiaPadreFamilia As PdfPTable = New PdfPTable(2)
        Dim oCell As PdfPCell = Nothing
        Dim oCellDetalle As PdfPCell = Nothing
        'Dim oCellComentario As PdfPCell = Nothing
        'Titulo
        'imagen del escudo
        Dim str_ImagenEscudoLogo As String = ConfigurationManager.AppSettings("RutaImagenEscudoLogoBlanco_Web_Local").ToString()  'currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReportesSeguimientoWeekly").ToString()
        Dim logo As iTextSharp.text.Image = Image.GetInstance(str_ImagenEscudoLogo)

        oCell = New PdfPCell(logo)
        oCell.HorizontalAlignment = 3
        oCell.BorderWidth = 0.0F
        oCell.Rowspan = 3
        oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 1
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 1
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        'oCell = New PdfPCell(New Phrase(New Chunk("", font8)))
        'oCell.HorizontalAlignment = 3
        'oCell.BorderWidth = 0.0F
        'oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(str_NombreEntidadReporte, FontFactory.GetFont("ARIAL", 18, Font.BOLD))))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidth = 0.0F
        oCell.Colspan = 2
        oTablaTitulo.AddCell(oCell)

        'oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        'oCell.HorizontalAlignment = 1
        ''oCell.BorderWidth = 0.0F
        'oTablaTitulo.AddCell(oCell)

        'oCell = New PdfPCell(New Phrase(New Chunk("", font8)))
        'oCell.HorizontalAlignment = 3
        'oCell.BorderWidth = 0.0F
        'oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        oTablaTitulo.SpacingBefore = 15.0F
        document.Add(oTablaTitulo)

        oCell = Nothing

        'Espacio
        Dim p2 As Phrase
        p2 = New Phrase("")

        Dim paragraph2 As Paragraph = New Paragraph(p2)
        paragraph2.Alignment = Element.ALIGN_CENTER
        document.Add(paragraph2)

        'cabecera
        '1ra fila

        oCellDetalle = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCellDetalle.HorizontalAlignment = 1
        oCellDetalle.BorderWidth = 0.0F
        oTablaDetalle.AddCell(oCellDetalle)

        oCellDetalle = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCellDetalle.HorizontalAlignment = 1
        oCellDetalle.BorderWidth = 0.0F
        oTablaDetalle.AddCell(oCellDetalle)

        oCellDetalle = New PdfPCell(New Phrase(New Chunk("El alumno(a) " & str_NombreCompleto & " ha sido matriculado en el año académico " & str_AnioMatricula & " por " & str_ResponsableMatricula & " en el Nivel:" & str_Nivel & " y Grado:" & str_Grado & " ,el día de " & str_Fecharegistro & ".", font8)))
        oCellDetalle.HorizontalAlignment = 1
        oCellDetalle.BorderWidth = 0.0F
        oTablaDetalle.AddCell(oCellDetalle)

        document.Add(oTablaDetalle)


        'Espacio
        Dim p3 As Phrase
        p3 = New Phrase("")

        Dim paragraph3 As Paragraph = New Paragraph(p3)
        paragraph3.Alignment = Element.ALIGN_CENTER
        document.Add(paragraph3)

        'Next
        ' step 5: we close the document

        document.Close()


        Return m
    End Function


    ''' <summary>
    ''' Exporta reportes en PDF (Soló listados de información)
    ''' </summary>
    ''' <param name="dt_Listado">Tabla de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Nombre del reporte</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporte_Pdf(ByVal dt_Listado As System.Data.DataTable, ByVal str_NombreEntidadReporte As String) As System.IO.MemoryStream

        ' step 1: creation of a document-object
        Dim m As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim document As New Document(PageSize.A4.Rotate)
        Dim font8 As Font = FontFactory.GetFont("ARIAL", 7)

        ' step 2:
        ' we create a writer that listens to the document
        ' and directs a PDF-stream to a file
        PdfWriter.GetInstance(document, m)

        ' step 3: we open the document
        document.Open()

        ' step 4: we add a paragraph to the document
        Dim p1 As Phrase
        p1 = New Phrase(str_NombreEntidadReporte)

        Dim paragraph As Paragraph = New Paragraph(p1)
        paragraph.Alignment = Element.ALIGN_CENTER
        document.Add(paragraph)

        Dim PdFTabla As PdfPTable = New PdfPTable(dt_Listado.Columns.Count)
        Dim PdfPCell As PdfPCell
        Dim cont_encabezado As Integer = 0

        While cont_encabezado <= dt_Listado.Columns.Count - 1

            PdfPCell = New PdfPCell(New Phrase(New Chunk(dt_Listado.Columns(cont_encabezado).ColumnName.ToString, font8)))
            PdfPCell.HorizontalAlignment = 1
            PdfPCell.BackgroundColor = New BaseColor(184, 204, 228)
            PdFTabla.AddCell(PdfPCell)

            cont_encabezado = cont_encabezado + 1
        End While

        Dim cont_rows As Integer = 0
        Dim cont_colum As Integer = 0

        While cont_rows <= dt_Listado.Rows.Count - 1

            While cont_colum <= dt_Listado.Columns.Count - 1

                PdfPCell = New PdfPCell(New Phrase(New Chunk(dt_Listado.Rows(cont_rows).Item(cont_colum).ToString, font8)))

                PdFTabla.AddCell(PdfPCell)

                cont_colum = cont_colum + 1
            End While

            cont_colum = 0
            cont_rows = cont_rows + 1
        End While

        PdFTabla.SpacingBefore = 15.0F

        document.Add(PdFTabla)

        ' step 5: we close the document
        document.Close()

        Return m
    End Function



    ''' <summary>
    ''' Exporta reportes de weekly en PDF (Soló listados de información)
    ''' </summary>
    ''' <param name="ds">Tabla de datos a exportar</param>
    ''' <param name="str_NombreEntidadReporte">Nombre del reporte</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     25/10/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ExportarReporteWeekly_Pdf(ByVal ds As System.Data.DataSet, ByVal str_NombreEntidadReporte As String) As System.IO.MemoryStream

        ' step 1: creation of a document-object
        Dim m As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim document As New Document(PageSize.A4.Rotate)
        Dim font8 As Font = FontFactory.GetFont("ARIAL", 14, Font.BOLD)

        Dim dt_Alumnos As DataTable = ds.Tables(0)
        Dim dt_Cursos As DataTable = ds.Tables(1)
        Dim dt_Criterios As DataTable = ds.Tables(2)
        Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
        Dim dt_Notas As DataTable = ds.Tables(4)
        Dim dt_CabeceraTutor As DataTable = ds.Tables(5)
        Dim dt_Leyenda As DataTable = ds.Tables(6)
        Dim dt_GrupoCriterioEvaluacion As DataTable = ds.Tables(7)

        Dim str_CodigoAlumno As String = ""

        ' step 2:
        ' we create a writer that listens to the document
        ' and directs a PDF-stream to a file
        PdfWriter.GetInstance(document, m)

        ' step 3: we open the document
        document.Open()

        ' step 4: we add a paragraph to the document
        'Dim p1 As Phrase
        'p1 = New Phrase(str_NombreEntidadReporte)

        'Dim paragraph As Paragraph = New Paragraph(p1)
        'paragraph.Alignment = Element.ALIGN_CENTER
        'paragraph.Font = FontFactory.GetFont("ARIAL", 7)
        'document.Add(paragraph)

        'Cabecera
        'For cont As Integer = 0 To dt_Alumnos.Rows.Count - 1

        str_CodigoAlumno = dt_Alumnos.Rows(0).Item("CodigoAlumno")

        Dim dv_Cursos As DataView = dt_Cursos.DefaultView
        dv_Cursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        'Dim dv_Criterios As DataView = dt_Criterios.DefaultView
        'dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_CriteriosYCalificativos As DataView = dt_CriteriosYCalificativos.DefaultView
        dv_CriteriosYCalificativos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_Notas As DataView = dt_Notas.DefaultView
        dv_Notas.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        Dim dv_CabeceraTutor As DataView = dt_CabeceraTutor.DefaultView
        dv_CabeceraTutor.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

        'Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTablaTitulo As PdfPTable = New PdfPTable(3)
        Dim oTablaCabecera As PdfPTable = New PdfPTable(6)
        Dim oTablaNotas As PdfPTable = New PdfPTable(12)
        Dim oTablaComentarioCopiaPadreFamilia As PdfPTable = New PdfPTable(2)
        Dim oCell As PdfPCell = Nothing
        Dim oCellNotas As PdfPCell = Nothing
        Dim oCellComentario As PdfPCell = Nothing
        'Titulo
        'imagen del escudo
        Dim str_ImagenEscudoLogo As String = ConfigurationManager.AppSettings("RutaImagenEscudoLogoBlanco_Web_Local").ToString()  'currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReportesSeguimientoWeekly").ToString()
        Dim logo As iTextSharp.text.Image = Image.GetInstance(str_ImagenEscudoLogo)

        oCell = New PdfPCell(logo)
        oCell.HorizontalAlignment = 3
        oCell.BorderWidth = 0.0F
        oCell.Rowspan = 3
        oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 1
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 1
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        'oCell = New PdfPCell(New Phrase(New Chunk("", font8)))
        'oCell.HorizontalAlignment = 3
        'oCell.BorderWidth = 0.0F
        'oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(str_NombreEntidadReporte, font8)))
        oCell.HorizontalAlignment = 1
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 1
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        'oCell = New PdfPCell(New Phrase(New Chunk("", font8)))
        'oCell.HorizontalAlignment = 3
        'oCell.BorderWidth = 0.0F
        'oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", font8)))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidth = 0.0F
        oTablaTitulo.AddCell(oCell)

        oTablaTitulo.SpacingBefore = 15.0F
        document.Add(oTablaTitulo)

        oCell = Nothing

        'cabecera
        '1ra fila

        oCell = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidthTop = 0.0F
        oCell.BorderWidthBottom = 0.0F
        oCell.BorderWidthLeft = 0.0F
        oCell.Rowspan = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk("Name : ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dv_CabeceraTutor(0).Item("NombreCompleto").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dt_Leyenda.Rows(0).Item("DescCalificativoWeekly").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidthTop = 0.0F
        oCell.BorderWidthBottom = 0.0F
        oCell.BorderWidthRight = 0.0F
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidthTop = 0.0F
        oCell.BorderWidthBottom = 0.0F
        oCell.BorderWidthLeft = 0.0F
        oCell.BorderWidthRight = 0.0F
        oTablaCabecera.AddCell(oCell)

        '2da fila

        oCell = New PdfPCell(New Phrase(New Chunk("Class: ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dv_CabeceraTutor(0).Item("DescGrado").ToString & " " & dv_CabeceraTutor(0).Item("DescAula").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dt_Leyenda.Rows(1).Item("DescCalificativoWeekly").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oCell.BorderWidthTop = 0.0F
        oCell.BorderWidthBottom = 0.0F
        oCell.BorderWidthRight = 0.0F
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dv_CabeceraTutor(0).Item("DescFecha").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 2
        oCell.BorderWidthTop = 0.0F
        oCell.BorderWidthBottom = 0.0F
        oCell.BorderWidthLeft = 0.0F
        oCell.BorderWidthRight = 0.0F
        oTablaCabecera.AddCell(oCell)

        '3ra fila

        oCell = New PdfPCell(New Phrase(New Chunk("Tutor: ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dv_CabeceraTutor(0).Item("DescPersonaTutor").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dt_Leyenda.Rows(2).Item("DescCalificativoWeekly").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 3
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dv_CabeceraTutor(0).Item("DescBimestre").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 2
        oCell.BorderWidthTop = 0.0F
        oCell.BorderWidthBottom = 0.0F
        oCell.BorderWidthRight = 0.0F
        oTablaCabecera.AddCell(oCell)

        oCell = New PdfPCell(New Phrase(New Chunk(dv_CabeceraTutor(0).Item("DesSemana").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCell.HorizontalAlignment = 2
        oCell.BorderWidthTop = 0.0F
        oCell.BorderWidthBottom = 0.0F
        oCell.BorderWidthLeft = 0.0F
        oCell.BorderWidthRight = 0.0F
        oTablaCabecera.AddCell(oCell)

        oTablaCabecera.SpacingBefore = 15.0F
        document.Add(oTablaCabecera)
        'Espacio
        Dim p2 As Phrase
        p2 = New Phrase("")

        Dim paragraph2 As Paragraph = New Paragraph(p2)
        paragraph2.Alignment = Element.ALIGN_CENTER
        document.Add(paragraph2)

        'Aqui va el detalle

        oCellNotas = New PdfPCell(New Phrase(New Chunk("Subject", FontFactory.GetFont("Verdana", 5, Font.BOLD))))
        oCellNotas.HorizontalAlignment = 1
        oTablaNotas.AddCell(oCellNotas)


        For i As Integer = 0 To dv_Cursos.Count - 1

            oCellNotas = New PdfPCell(New Phrase(New Chunk(dv_Cursos(i).Item("DescNombreCurso").ToString, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
            oCellNotas.HorizontalAlignment = 1
            oTablaNotas.AddCell(oCellNotas)

        Next
        Dim int_cantCursos As Integer = 11 - CInt(dv_Cursos.Count)
        Dim int_ConCur As Integer = 0

        If int_cantCursos > 0 Then

            While int_ConCur <= int_cantCursos - 1
                oCellNotas = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
                oCellNotas.HorizontalAlignment = 1
                oTablaNotas.AddCell(oCellNotas)
                int_ConCur = int_ConCur + 1
            End While
            int_ConCur = 0
        End If

        'Detalle Notas
        Dim int_CodigoCriterio, int_CodigoCurso, int_Filas, int_Columnas As Integer
        Dim str_DescCriterio As String = ""

        int_Filas = 0
        int_Columnas = 0

        Dim int_CodigoGrupoCriterio As Integer = 0
        Dim int_CantidadCriterios As Integer = 0
        Dim str_CodigoGrupoCriterio As String = ""
        Dim int_cantAumFila = 3
        Dim dv_Criterios As DataView = dt_Criterios.DefaultView

        For i As Integer = 0 To dt_GrupoCriterioEvaluacion.Rows.Count - 1
            int_CodigoGrupoCriterio = dt_GrupoCriterioEvaluacion.Rows(i).Item("CodigoGrupoCriterio")
            str_CodigoGrupoCriterio = dt_GrupoCriterioEvaluacion.Rows(i).Item("GrupoCriterioEvaluacion")

            oCellNotas = New PdfPCell(New Phrase(New Chunk(str_CodigoGrupoCriterio, FontFactory.GetFont("Verdana", 5, Font.BOLD))))
            oCellNotas.HorizontalAlignment = 1
            oTablaNotas.AddCell(oCellNotas)
            dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "' and CodigoGrupoCriterio = '" & CStr(int_CodigoGrupoCriterio) & "'"

            While int_ConCur <= 10
                oCellNotas = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
                oCellNotas.HorizontalAlignment = 1
                oTablaNotas.AddCell(oCellNotas)
                int_ConCur = int_ConCur + 1
            End While
            int_ConCur = 0

            While int_Filas <= dv_Criterios.Count - 1 ' Filas Criterios

                int_CodigoCriterio = dv_Criterios(int_Filas).Item("CodigoCriterio").ToString
                str_DescCriterio = dv_Criterios(int_Filas).Item("Criterio").ToString
                oCellNotas = New PdfPCell(New Phrase(New Chunk(str_DescCriterio, FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
                oCellNotas.HorizontalAlignment = 1
                oTablaNotas.AddCell(oCellNotas)

                While int_Columnas <= dv_Cursos.Count - 1 ' Columnas cursos
                    int_CodigoCurso = dv_Cursos(int_Columnas).Item("CodigoCurso")

                    oCellNotas = New PdfPCell(New Phrase(New Chunk(obtenerNota(dv_Notas, int_CodigoCurso, int_CodigoCriterio), FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
                    oCellNotas.HorizontalAlignment = 1
                    oTablaNotas.AddCell(oCellNotas)

                    int_Columnas += 1
                End While

                If int_cantCursos > 0 Then

                    While int_ConCur <= int_cantCursos - 1
                        oCellNotas = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
                        oCellNotas.HorizontalAlignment = 1
                        oTablaNotas.AddCell(oCellNotas)
                        int_ConCur = int_ConCur + 1
                    End While
                    int_ConCur = 0
                End If

                int_CantidadCriterios = dv_Criterios.Count
                str_DescCriterio = ""
                int_CodigoCurso = 0
                int_Columnas = 0
                int_Filas += 1
            End While
            int_Filas = 0
            int_CantidadCriterios = int_CantidadCriterios + 1
            int_cantAumFila = int_cantAumFila + int_CantidadCriterios
        Next

        oTablaNotas.SpacingBefore = 15.0F
        document.Add(oTablaNotas)
        'Aqui termina el detalle
        'Espacio
        Dim p3 As Phrase
        p3 = New Phrase("")

        Dim paragraph3 As Paragraph = New Paragraph(p3)
        paragraph3.Alignment = Element.ALIGN_CENTER
        document.Add(paragraph3)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 4
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        'oCellComentario.Rowspan = 5
        oCellComentario.BorderWidthTop = 0.0F
        oCellComentario.BorderWidthBottom = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        oCellComentario.BorderWidthTop = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oCellComentario = New PdfPCell(New Phrase(New Chunk(" ", FontFactory.GetFont("Verdana", 5, Font.NORMAL))))
        oCellComentario.HorizontalAlignment = 1
        oCellComentario.BorderWidthTop = 0.0F
        oTablaComentarioCopiaPadreFamilia.AddCell(oCellComentario)

        oTablaComentarioCopiaPadreFamilia.SpacingBefore = 15.0F
        document.Add(oTablaComentarioCopiaPadreFamilia)

        'Next
        ' step 5: we close the document

        document.Close()


        Return m
    End Function

    Public Shared Function obtenerNota(ByVal dv_Notas As DataView, ByVal int_CodigoCurso As Integer, ByVal int_CodigoCriterio As Integer) As String

        Dim str_Nota As String = ""

        For Each drv As DataRowView In dv_Notas
            If drv.Item("CodigoCurso") = int_CodigoCurso And drv.Item("CodigoCriterio") = int_CodigoCriterio Then
                str_Nota = drv.Item("Nota")
                Exit For
            End If
        Next

        Return str_Nota

    End Function
    
#End Region


#Region "Exportar WORD"

    Public Shared Function GenerarCarta_Suspension(ByVal ds_Data As DataSet, ByVal str_FechaSuspension As Date) As String

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara1 As Microsoft.Office.Interop.Word.Paragraph, oPara2 As Microsoft.Office.Interop.Word.Paragraph, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oPara9, oPara10, oPara11, oPara12, oPara13, oPara14, oPara15 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        Dim dt_Consolidado As DataTable
        Dim dt_Familias As DataTable
        Dim dt_DetalleDeudas As DataTable

        Dim dv_Alumnos As DataView
        Dim dv_DetalleDeudas As DataView
        Dim cont As Integer = 0
        Dim cont_Hermanos As Integer = 0

        dt_Consolidado = ds_Data.Tables(0)
        dt_Familias = ds_Data.Tables(1)
        dt_DetalleDeudas = ds_Data.Tables(2)

        dv_Alumnos = dt_Consolidado.DefaultView
        dv_DetalleDeudas = dt_DetalleDeudas.DefaultView

        For i As Integer = 0 To dt_Familias.Rows.Count - 1

            If i = 0 Then
                'Iniciamos el Word 
                Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaSuspension").ToString()
                oWord = New Microsoft.Office.Interop.Word.Application
                oWord.Visible = False
                oDoc = oWord.Documents.Add(saArchivo)
                oDoc.Content.Copy()

            End If

            If i > dt_Familias.Rows.Count - 1 Then
                Exit For
            End If

            If dt_Familias.Rows(i).Item("Exportar") = 1 Then

                dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")
                dv_DetalleDeudas.RowFilter = "1=1 AND CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")

                'COLUMNA "A"
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                oPara1.Range.Font.Name = "Arial Narrow"
                oPara1.Format.SpaceAfter = 6
                oPara1.Range.Font.Bold = True
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                oPara1.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = " "
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i).Item("NombreFamilia").ToString
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = dt_Familias.Rows(i).Item("NombreFamilia").ToString
                    .Replacement.Text = ""
                    .Forward = True
                    .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                    .Format = False
                    .MatchCase = False
                    .MatchWholeWord = False
                    .MatchWildcards = False
                    .MatchSoundsLike = False
                    .MatchAllWordForms = False
                End With
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Bold = True

                cont = 0
                cont_Hermanos = dv_DetalleDeudas.Count

                oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                oTable.Range.ParagraphFormat.SpaceAfter = 3
                oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"
                oTable.Cell(1, 1).Range.Font.Name = "Arial Narrow"
                oTable.Cell(1, 2).Range.Font.Name = "Arial Narrow"

                Dim int_contDetalleDeuda As Integer = 0

                Dim str_ListaHermanos As String = ""
                Dim str_NombreAlumno As String = ""
                Dim int_NombreLength As Integer = 0
                Dim int_IndexNombreAlumno As Integer

                While cont <= dv_Alumnos.Count - 1

                    oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                    oTable.Cell(cont + 2, 1).Range.Font.Bold = True

                    dv_DetalleDeudas.RowFilter = "1=1 AND CodigoAlumno = " & dv_Alumnos.Item(cont).Item("CodigoAlumno").ToString

                    While int_contDetalleDeuda <= dv_DetalleDeudas.Count - 1

                        oTable.Cell(int_contDetalleDeuda + 2, 2).Range.Text = dv_DetalleDeudas.Item(int_contDetalleDeuda).Item("armada").ToString & _
                        " " & dv_DetalleDeudas.Item(int_contDetalleDeuda).Item("FechaVencimiento").ToString
                        oTable.Cell(int_contDetalleDeuda + 2, 2).Range.Font.Bold = True

                        int_contDetalleDeuda = int_contDetalleDeuda + 1
                    End While

                    int_contDetalleDeuda = 0

                    str_NombreAlumno = dv_DetalleDeudas.Item(int_contDetalleDeuda).Item("NombreAlumno").ToString
                    int_NombreLength = str_NombreAlumno.Length
                    str_NombreAlumno = str_NombreAlumno.Substring(0, str_NombreAlumno.IndexOf(" - "))

                    int_IndexNombreAlumno = 0
                    int_IndexNombreAlumno = str_ListaHermanos.IndexOf(str_NombreAlumno)

                    If int_IndexNombreAlumno < 0 Then
                        str_ListaHermanos = str_ListaHermanos + str_NombreAlumno + ", "
                    End If

                    cont = cont + 1
                End While

                str_ListaHermanos = str_ListaHermanos.Substring(0, str_ListaHermanos.Length - 2)

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Range.Font.Bold = False
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara6.Range.Text = "Referencia: Incumplimiento de PAGO DE ARMADAS POR ENSEÑANZA."
                oPara6.Range.Font.Name = "Arial Narrow"
                oPara6.Format.SpaceAfter = 6
                oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara6.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = "Referencia"
                    .Replacement.Text = ""
                    .Forward = True
                    .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                    .Format = False
                    .MatchCase = False
                    .MatchWholeWord = False
                    .MatchWildcards = False
                    .MatchSoundsLike = False
                    .MatchAllWordForms = False
                End With
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = " "
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara7.Range.Text = "De nuestra consideración:"
                oPara7.Range.Font.Name = "Arial Narrow"
                oPara7.Format.SpaceAfter = 6
                oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara7.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = " "
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara13.Range.InsertParagraphAfter()

                oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara9.Range.Text = "Mediante la presente cumplimos con comunicarle lo siguiente:"
                oPara9.Range.Font.Name = "Arial Narrow"
                oPara9.Format.SpaceAfter = 6
                oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara9.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = " "
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara10.Range.Text = "1." & vbTab & "Según es de su conocimiento, la Cláusula 7ª del Contrato de Servicio Educativo, suscrito entre ustedes Y el Colegio, el pasado 15 de Febrero del 2010, el Centro Educativo tiene la facultad de suspender el servicio educativo de su hija, si ustedes incumplieran en el pago oportuno de dos armadas del servicio educativo anual."
                oPara10.Range.Font.Name = "Arial Narrow"
                oPara10.Format.SpaceAfter = 6
                oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara10.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = " "
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = "2." & vbTab & "En este caso en particular, el Colegio les ha enviado reiteradamente notas informativas para recordarles del incumplimiento en el pago aludido."
                oPara13.Range.Font.Name = "Arial Narrow"
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara13.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = " "
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = "3. " & vbTab & "Los días 18 y 25 de Junio, se envió una  invitación para encontrar una fecha propicia para que ustedes cumplieran con la obligación asumida con nosotros. Pero no se han acercado, ignorando sin justificación alguna nuestro ofrecimiento para acordar una fecha en la cual ustedes pudieran cumplir con sus obligaciones."
                oPara15.Range.Font.Name = "Arial Narrow"
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara15.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = " "
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                Dim str_AlumnosTexto As String = ""
                Dim str_HijosTexto As String = ""

                If str_ListaHermanos.IndexOf(",") > 0 Then
                    str_HijosTexto = "sus hijos " & str_ListaHermanos & ", "
                    str_AlumnosTexto = "los alumnos(as) " & str_ListaHermanos & ", "
                Else
                    str_HijosTexto = "su hijo(a) " & str_ListaHermanos & ", "
                    str_AlumnosTexto = "el/la alumno(a) " & str_ListaHermanos & ", "
                End If

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = "4." & vbTab & "En consecuencia, al haber  transcurrido con exceso el plazo en el que ustedes debieron pagar las armadas vencidas antes mencionadas correspondientes al Servicio Educativo de " + _
                                    str_HijosTexto + _
                                    "y en cumplimiento del artículo 7º del Contrato de Servicio Educativo, cumplimos con poner en su conocimiento la decisión de SUSPENDER EL SERVICIO EDUCATIVO a " + _
                                     str_AlumnosTexto + _
                                     "a partir del día " & Dev_NombreDia(str_FechaSuspension.DayOfWeek) & " " & str_FechaSuspension.Day & " de " & Dev_NombreMes(str_FechaSuspension.Month) & " en aplicación del Art. 3 del D.S. 005-2002-ED, en concordancia con la Ley de Protección de la Economía Familiar y el Reglamento de las Instituciones Privadas de Educación Básica y Técnico Productiva, sobre las condiciones económicas que rigen el servicio educativo particular."
                oPara15.Range.Font.Name = "Arial Narrow"
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara15.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = " "
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = "Sin otro particular,"
                oPara15.Range.Font.Name = "Arial Narrow"
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = "Atentamente,"
                oPara12.Range.Font.Name = "Arial Narrow"
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = "Janeth Velasco E."
                oPara15.Range.Font.Name = "Arial Narrow"
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = "Tesorería"
                oPara5.Range.Font.Name = "Arial Narrow"
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

            End If

            'i = i + 1
        Next

        Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
        Dim str_RutaGuardar As String = ""
        Dim str_nombreDoc As String = ""

        str_nombreDoc = "CartaSuspension_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
        str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()

        'Quit Word and thoroughly deallocate everything
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc

    End Function

    Public Shared Function GenerarCarta_Citacion(ByVal ds_Data As DataSet, ByVal str_FechaCitacion As Date, ByVal str_HoraInicio As String, ByVal str_HoraFin As String) As String

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara1 As Microsoft.Office.Interop.Word.Paragraph, oPara2 As Microsoft.Office.Interop.Word.Paragraph, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oPara9, oPara10, oPara11, oPara12, oPara13, oPara14, oPara15 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        Dim dt_Consolidado As DataTable
        Dim dt_Familias As DataTable
        Dim dv_Alumnos As DataView
        Dim cont As Integer = 0
        Dim cont_Hermanos As Integer = 0

        dt_Consolidado = ds_Data.Tables(0)
        dt_Familias = ds_Data.Tables(1)

        dv_Alumnos = dt_Consolidado.DefaultView

        For i As Integer = 0 To dt_Familias.Rows.Count - 1

            If i = 0 Then
                'Iniciamos el Word 
                Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaCitacion").ToString()
                oWord = New Microsoft.Office.Interop.Word.Application
                oWord.Visible = False
                oDoc = oWord.Documents.Add(saArchivo)
                oDoc.Content.Copy()

            End If

            If i > dt_Familias.Rows.Count - 1 Then
                Exit For
            End If

            If dt_Familias.Rows(i).Item("Exportar") = 1 Then

                dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")

                'COLUMNA "A"
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                oPara1.Range.Font.Name = "Arial Narrow"
                oPara1.Format.SpaceAfter = 6
                oPara1.Range.Font.Bold = True
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                oPara1.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = " "
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i).Item("NombreFamilia").ToString
                oPara2.Range.Font.Name = "Arial Narrow"
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = dt_Familias.Rows(i).Item("NombreFamilia").ToString
                    .Replacement.Text = ""
                    .Forward = True
                    .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                    .Format = False
                    .MatchCase = False
                    .MatchWholeWord = False
                    .MatchWildcards = False
                    .MatchSoundsLike = False
                    .MatchAllWordForms = False
                End With
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Bold = True

                cont = 0
                cont_Hermanos = dv_Alumnos.Count

                oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                oTable.Range.ParagraphFormat.SpaceAfter = 3
                oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"
                oTable.Cell(1, 1).Range.Font.Name = "Arial Narrow"
                oTable.Cell(1, 2).Range.Font.Name = "Arial Narrow"

                While cont <= dv_Alumnos.Count - 1

                    oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                    oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                    oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                    oTable.Cell(cont + 2, 2).Range.Font.Bold = True

                    cont = cont + 1
                End While

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Range.Font.Bold = False
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente de pago las armadas arriba mencionadas."
                oPara6.Range.Font.Name = "Arial Narrow"
                oPara6.Format.SpaceAfter = 6
                oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara6.Range.InsertParagraphAfter()

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = " "
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara7.Range.Text = "Es por ello que se cita a usted para el día " & Dev_NombreDia(str_FechaCitacion.DayOfWeek) & " " & str_FechaCitacion.Day & " de " & Dev_NombreMes(str_FechaCitacion.Month) & " del presente desde las " & str_HoraInicio & " hasta " & str_HoraFin & " en la oficina de tesorería del colegio"
                oPara7.Range.Font.Name = "Arial Narrow"
                oPara7.Format.SpaceAfter = 6
                oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara7.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = " "
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara13.Range.InsertParagraphAfter()

                oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara9.Range.Text = "Atentamente."
                oPara9.Range.Font.Name = "Arial Narrow"
                oPara9.Format.SpaceAfter = 6
                oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara9.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = " "
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara10.Range.Text = "Janeth Velasco"
                oPara10.Range.Font.Name = "Arial Narrow"
                oPara10.Format.SpaceAfter = 6
                oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara10.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = "Tesorería"
                oPara11.Range.Font.Name = "Arial Narrow"
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                If cont_Hermanos = 1 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()

                    oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara13.Range.Text = " "
                    oPara13.Format.SpaceAfter = 6
                    oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara13.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 2 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 3 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                End If

                If i + 1 <= dt_Familias.Rows.Count - 1 Then

                    If dt_Familias.Rows(i + 1).Item("Exportar") = 1 Then

                        dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i + 1).Item("CodigoFamilia")

                        'COLUMNA "B"
                        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                        oPara1.Range.Font.Bold = True
                        oPara1.Format.SpaceAfter = 6
                        oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                        oPara1.Range.InsertParagraphAfter()

                        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara2.Range.Text = " "
                        oPara2.Range.Font.Bold = False
                        oPara2.Format.SpaceAfter = 6
                        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara2.Range.InsertParagraphAfter()

                        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                        oPara2.Format.SpaceAfter = 6
                        oPara2.Range.Font.Bold = False
                        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara2.Range.InsertParagraphAfter()

                        oWord.Selection.Find.ClearFormatting()
                        With oWord.Selection.Find
                            .Text = dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                            .Replacement.Text = ""
                            .Forward = True
                            .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                            .Format = False
                            .MatchCase = False
                            .MatchWholeWord = False
                            .MatchWildcards = False
                            .MatchSoundsLike = False
                            .MatchAllWordForms = False
                        End With
                        oWord.Selection.Find.Execute()
                        oWord.Selection.Font.Bold = True

                        cont = 0
                        cont_Hermanos = dv_Alumnos.Count

                        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                        oTable.Range.ParagraphFormat.SpaceAfter = 3
                        oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                        oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"

                        While cont <= dv_Alumnos.Count - 1

                            oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                            oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                            oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                            oTable.Cell(cont + 2, 2).Range.Font.Bold = True

                            cont = cont + 1
                        End While

                        oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara5.Range.Text = " "
                        oPara5.Range.Font.Bold = False
                        oPara5.Format.SpaceAfter = 6
                        oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara5.Range.InsertParagraphAfter()

                        oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente de pago las armadas arriba mencionadas."
                        oPara6.Format.SpaceAfter = 6
                        oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara6.Range.InsertParagraphAfter()

                        oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara12.Range.Text = " "
                        oPara12.Format.SpaceAfter = 6
                        oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara12.Range.InsertParagraphAfter()

                        oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara7.Range.Text = "Es por ello que se cita a usted para el día " & Dev_NombreDia(str_FechaCitacion.DayOfWeek) & " " & str_FechaCitacion.Day & " de " & Dev_NombreMes(str_FechaCitacion.Month) & " del presente desde las " & str_HoraInicio & " hasta " & str_HoraFin & " en la oficina de tesorería del colegio"
                        oPara7.Format.SpaceAfter = 6
                        oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara7.Range.InsertParagraphAfter()

                        oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara13.Range.Text = " "
                        oPara13.Format.SpaceAfter = 6
                        oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara13.Range.InsertParagraphAfter()

                        oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara9.Range.Text = "Atentamente."
                        oPara9.Format.SpaceAfter = 6
                        oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara9.Range.InsertParagraphAfter()

                        oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara15.Range.Text = " "
                        oPara15.Format.SpaceAfter = 6
                        oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara15.Range.InsertParagraphAfter()

                        oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara10.Range.Text = "Janeth Velasco"
                        oPara10.Format.SpaceAfter = 6
                        oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara10.Range.InsertParagraphAfter()

                        oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara11.Range.Text = "Tesorería"
                        oPara11.Format.SpaceAfter = 6
                        oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara11.Range.InsertParagraphAfter()

                        If cont_Hermanos = 1 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()

                            oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara13.Range.Text = " "
                            oPara13.Format.SpaceAfter = 6
                            oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara13.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 2 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 3 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        End If

                    End If

                End If

            End If

            cont = 0
            i = i + 1
        Next

        Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
        Dim str_RutaGuardar As String = ""
        Dim str_nombreDoc As String = ""

        str_nombreDoc = "CartaCitacion_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
        str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()

        'Quit Word and thoroughly deallocate everything
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc

    End Function

    Public Shared Function GenerarEsquela_Formato2(ByVal ds_Data As DataSet) As String

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara1 As Microsoft.Office.Interop.Word.Paragraph, oPara2 As Microsoft.Office.Interop.Word.Paragraph, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oPara9, oPara10, oPara11, oPara12, oPara13, oPara14, oPara15 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        Dim dt_Consolidado As DataTable
        Dim dt_Familias As DataTable
        Dim dv_Alumnos As DataView
        Dim cont As Integer = 0
        Dim cont_Hermanos As Integer = 0

        dt_Consolidado = ds_Data.Tables(0)
        dt_Familias = ds_Data.Tables(1)

        dv_Alumnos = dt_Consolidado.DefaultView

        For i As Integer = 0 To dt_Familias.Rows.Count - 1

            If i = 0 Then
                'Iniciamos el Word 
                Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaEsquelas2").ToString()
                oWord = New Microsoft.Office.Interop.Word.Application
                oWord.Visible = False
                oDoc = oWord.Documents.Add(saArchivo)
                oDoc.Content.Copy()

            End If

            If i > dt_Familias.Rows.Count - 1 Then
                Exit For
            End If

            If dt_Familias.Rows(i).Item("Exportar") = 1 Then

                dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")

                'COLUMNA "A"
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                oPara1.Range.Font.Name = "Arial Narrow"
                oPara1.Format.SpaceAfter = 6
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                oPara1.Range.Font.Bold = True
                oPara1.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i).Item("NombreFamilia").ToString
                oPara2.Range.Font.Name = "Arial Narrow"
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = dt_Familias.Rows(i).Item("NombreFamilia").ToString
                    .Replacement.Text = ""
                    .Forward = True
                    .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                    .Format = False
                    .MatchCase = False
                    .MatchWholeWord = False
                    .MatchWildcards = False
                    .MatchSoundsLike = False
                    .MatchAllWordForms = False
                End With
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Bold = True

                cont = 0
                cont_Hermanos = dv_Alumnos.Count

                oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                oTable.Range.ParagraphFormat.SpaceAfter = 3
                oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"
                oTable.Cell(1, 1).Range.Font.Name = "Arial Narrow"
                oTable.Cell(1, 2).Range.Font.Name = "Arial Narrow"

                While cont <= dv_Alumnos.Count - 1

                    oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                    oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                    oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                    oTable.Cell(cont + 2, 2).Range.Font.Bold = True

                    cont = cont + 1
                End While

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Range.Font.Bold = False
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente de pago las armada arriba mencionadas."
                oPara6.Range.Font.Name = "Arial Narrow"
                oPara6.Range.Font.Bold = False
                oPara6.Format.SpaceAfter = 6
                oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara6.Range.InsertParagraphAfter()

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = " "
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara7.Range.Text = "Sírvase cancelar la deuda pendiente a la brevedad posible, de lo contrario nos veremos obligados a tomar medidas necesarias, en aplicación del Art. 3 del D.S. 05-2002-ED, en concordancia con la Ley de Protección de la Economía Familiar y el Reglamento de las Instituciones Privadas de la Educación Básica y Técnico Productiva, sobre las condiciones económicas que rigen el servicio, durante el presente año."
                oPara7.Range.Font.Name = "Arial Narrow"
                oPara7.Format.SpaceAfter = 6
                oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara7.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = " "
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara13.Range.InsertParagraphAfter()

                oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara8.Range.Text = "Si se encuentra al día en sus pagos, ignore esta deuda que figura como pendiente en el sistema hasta el día de hoy."
                oPara8.Range.Font.Name = "Arial Narrow"
                oPara8.Format.SpaceAfter = 6
                oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara8.Range.InsertParagraphAfter()

                oPara14 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara14.Range.Text = " "
                oPara14.Format.SpaceAfter = 6
                oPara14.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara14.Range.InsertParagraphAfter()

                oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara9.Range.Text = "Atentamente."
                oPara9.Range.Font.Name = "Arial Narrow"
                oPara9.Format.SpaceAfter = 6
                oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara9.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = " "
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara10.Range.Text = "Janeth Velasco"
                oPara10.Range.Font.Name = "Arial Narrow"
                oPara10.Format.SpaceAfter = 6
                oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara10.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = "Tesorería"
                oPara11.Range.Font.Name = "Arial Narrow"
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                If cont_Hermanos = 1 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 2 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 3 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()
                End If

                If i + 1 <= dt_Familias.Rows.Count - 1 Then

                    If dt_Familias.Rows(i + 1).Item("Exportar") = 1 Then

                        dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i + 1).Item("CodigoFamilia")

                        'COLUMNA "B"
                        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                        oPara1.Format.SpaceAfter = 6
                        oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                        oPara1.Range.Font.Bold = True
                        oPara1.Range.InsertParagraphAfter()

                        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                        oPara2.Range.Font.Bold = False
                        oPara2.Format.SpaceAfter = 6
                        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara2.Range.InsertParagraphAfter()

                        oWord.Selection.Find.ClearFormatting()
                        With oWord.Selection.Find
                            .Text = dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                            .Replacement.Text = ""
                            .Forward = True
                            .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                            .Format = False
                            .MatchCase = False
                            .MatchWholeWord = False
                            .MatchWildcards = False
                            .MatchSoundsLike = False
                            .MatchAllWordForms = False
                        End With
                        oWord.Selection.Find.Execute()
                        oWord.Selection.Font.Bold = True

                        cont = 0
                        cont_Hermanos = dv_Alumnos.Count

                        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                        oTable.Range.ParagraphFormat.SpaceAfter = 3
                        oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                        oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"

                        While cont <= dv_Alumnos.Count - 1

                            oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                            oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                            oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                            oTable.Cell(cont + 2, 2).Range.Font.Bold = True
                            cont = cont + 1
                        End While

                        oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara5.Range.Text = " "
                        oPara5.Range.Font.Bold = False
                        oPara5.Format.SpaceAfter = 6
                        oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara5.Range.InsertParagraphAfter()

                        oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente de pago las armada arriba mencionadas."
                        oPara6.Range.Font.Bold = False
                        oPara6.Format.SpaceAfter = 6
                        oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara6.Range.InsertParagraphAfter()

                        oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara12.Range.Text = " "
                        oPara12.Format.SpaceAfter = 6
                        oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara12.Range.InsertParagraphAfter()

                        oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara7.Range.Text = "Sírvase cancelar la deuda pendiente a la brevedad posible, de lo contrario nos veremos obligados a tomar medidas necesarias, en aplicación del Art. 3 del D.S. 05-2002-ED, en concordancia con la Ley de Protección de la Economía Familiar y el Reglamento de las Instituciones Privadas de la Educación Básica y Técnico Productiva, sobre las condiciones económicas que rigen el servicio, durante el presente año."
                        oPara7.Format.SpaceAfter = 6
                        oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara7.Range.InsertParagraphAfter()

                        oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara13.Range.Text = " "
                        oPara13.Format.SpaceAfter = 6
                        oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara13.Range.InsertParagraphAfter()

                        oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara8.Range.Text = "Si se encuentra al día en sus pagos, ignore esta deuda que figura como pendiente en el sistema hasta el día de hoy."
                        oPara8.Format.SpaceAfter = 6
                        oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara8.Range.InsertParagraphAfter()

                        oPara14 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara14.Range.Text = " "
                        oPara14.Format.SpaceAfter = 6
                        oPara14.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara14.Range.InsertParagraphAfter()

                        oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara9.Range.Text = "Atentamente."
                        oPara9.Format.SpaceAfter = 6
                        oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara9.Range.InsertParagraphAfter()

                        oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara15.Range.Text = " "
                        oPara15.Format.SpaceAfter = 6
                        oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara15.Range.InsertParagraphAfter()

                        oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara10.Range.Text = "Janeth Velasco"
                        oPara10.Format.SpaceAfter = 6
                        oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara10.Range.InsertParagraphAfter()

                        oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara11.Range.Text = "Tesorería"
                        oPara11.Format.SpaceAfter = 6
                        oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara11.Range.InsertParagraphAfter()

                        If cont_Hermanos = 1 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 2 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 3 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()
                        End If

                    End If

                End If

            End If

            cont = 0
            i = i + 1
        Next

        Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
        Dim str_RutaGuardar As String = ""
        Dim str_nombreDoc As String = ""

        str_nombreDoc = "Esquela2_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
        str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()

        'Quit Word and thoroughly deallocate everything
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc
    End Function

    Public Shared Function GenerarEsquela_Formato1(ByVal ds_Data As DataSet) As String

        Dim str_nombreDoc As String = ""

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara1 As Microsoft.Office.Interop.Word.Paragraph, oPara2 As Microsoft.Office.Interop.Word.Paragraph, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8, oPara9, oPara10, oPara11, oPara12, oPara13, oPara14, oPara15 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        Dim dt_Consolidado As DataTable
        Dim dt_Familias As DataTable
        Dim dv_Alumnos As DataView
        Dim cont As Integer = 0
        Dim cont_Hermanos As Integer = 0

        dt_Consolidado = ds_Data.Tables(0)
        dt_Familias = ds_Data.Tables(1)

        dv_Alumnos = dt_Consolidado.DefaultView

        For i As Integer = 0 To dt_Familias.Rows.Count - 1

            If i = 0 Then
                'Iniciamos el Word 
                Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaEsquelas1").ToString()
                oWord = New Microsoft.Office.Interop.Word.Application
                oWord.Visible = False
                oDoc = oWord.Documents.Add(saArchivo)
                oDoc.Content.Copy()

            End If

            If i > dt_Familias.Rows.Count - 1 Then
                Exit For
            End If

            If dt_Familias.Rows(i).Item("Exportar") = 1 Then

                dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i).Item("CodigoFamilia")

                'COLUMNA "A"
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                oPara1.Range.Font.Name = "Arial Narrow"
                oPara1.Format.SpaceAfter = 6
                oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                oPara1.Range.Font.Bold = True
                oPara1.Range.InsertParagraphAfter()

                oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i).Item("NombreFamilia").ToString
                oPara2.Range.Font.Name = "Arial Narrow"
                oPara2.Range.Font.Bold = False
                oPara2.Format.SpaceAfter = 6
                oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara2.Range.InsertParagraphAfter()

                oWord.Selection.Find.ClearFormatting()
                With oWord.Selection.Find
                    .Text = dt_Familias.Rows(i).Item("NombreFamilia").ToString
                    .Replacement.Text = ""
                    .Forward = True
                    .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                    .Format = False
                    .MatchCase = False
                    .MatchWholeWord = False
                    .MatchWildcards = False
                    .MatchSoundsLike = False
                    .MatchAllWordForms = False
                End With
                oWord.Selection.Find.Execute()
                oWord.Selection.Font.Bold = True

                cont = 0
                cont_Hermanos = dv_Alumnos.Count

                oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                oTable.Range.ParagraphFormat.SpaceAfter = 3
                oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"
                oTable.Cell(1, 1).Range.Font.Name = "Arial Narrow"
                oTable.Cell(1, 2).Range.Font.Name = "Arial Narrow"

                While cont <= dv_Alumnos.Count - 1

                    oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                    oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                    oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                    oTable.Cell(cont + 2, 2).Range.Font.Bold = True
                    cont = cont + 1
                End While

                oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara5.Range.Text = " "
                oPara5.Range.Font.Bold = False
                oPara5.Format.SpaceAfter = 6
                oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara5.Range.InsertParagraphAfter()

                oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente el pago de la armada arriba mencionada."
                oPara6.Range.Font.Name = "Arial Narrow"
                oPara6.Range.Font.Bold = False
                oPara6.Format.SpaceAfter = 6
                oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara6.Range.InsertParagraphAfter()

                oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara12.Range.Text = " "
                oPara12.Format.SpaceAfter = 6
                oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara12.Range.InsertParagraphAfter()

                oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara7.Range.Text = "Sírvase cancelar la deuda pendiente en las oficinas del Banco Interbank ó del Banco de Crédito, ya que la morosidad de las armadas afecta el normal desenvolvimiento de nuestra institución."
                oPara7.Range.Font.Name = "Arial Narrow"
                oPara7.Format.SpaceAfter = 6
                oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara7.Range.InsertParagraphAfter()

                oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara13.Range.Text = " "
                oPara13.Format.SpaceAfter = 6
                oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara13.Range.InsertParagraphAfter()

                oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara8.Range.Text = "Si se encuentra al día en sus pagos, ignore esta deuda que figura como pendiente en el sistema hasta el día de hoy."
                oPara8.Range.Font.Name = "Arial Narrow"
                oPara8.Format.SpaceAfter = 6
                oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
                oPara8.Range.InsertParagraphAfter()

                oPara14 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara14.Range.Text = " "
                oPara14.Format.SpaceAfter = 6
                oPara14.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara14.Range.InsertParagraphAfter()

                oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara9.Range.Text = "Atentamente."
                oPara9.Range.Font.Name = "Arial Narrow"
                oPara9.Format.SpaceAfter = 6
                oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara9.Range.InsertParagraphAfter()

                oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara15.Range.Text = " "
                oPara15.Format.SpaceAfter = 6
                oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara15.Range.InsertParagraphAfter()

                oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara10.Range.Text = "Janeth Velasco"
                oPara10.Range.Font.Name = "Arial Narrow"
                oPara10.Format.SpaceAfter = 6
                oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara10.Range.InsertParagraphAfter()

                oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara11.Range.Text = "Tesorería"
                oPara11.Range.Font.Name = "Arial Narrow"
                oPara11.Format.SpaceAfter = 6
                oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                oPara11.Range.InsertParagraphAfter()

                If cont_Hermanos = 1 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()

                    oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara15.Range.Text = " "
                    oPara15.Format.SpaceAfter = 6
                    oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara15.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 2 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()

                    oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara12.Range.Text = " "
                    oPara12.Format.SpaceAfter = 6
                    oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara12.Range.InsertParagraphAfter()
                ElseIf cont_Hermanos = 3 Then
                    oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara5.Range.Text = " "
                    oPara5.Format.SpaceAfter = 6
                    oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    oPara5.Range.InsertParagraphAfter()
                End If

                If i + 1 <= dt_Familias.Rows.Count - 1 Then

                    If dt_Familias.Rows(i + 1).Item("Exportar") = 1 Then

                        dv_Alumnos.RowFilter = "1=1 and CodigoFamilia =" & dt_Familias.Rows(i + 1).Item("CodigoFamilia")

                        'COLUMNA "B"
                        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara1.Range.Text = "Miraflores, " & Now.Day & " de " & Dev_NombreMes(Now.Month) & " de " & Now.Year
                        oPara1.Range.Font.Bold = True
                        oPara1.Format.SpaceAfter = 6
                        oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                        oPara1.Range.InsertParagraphAfter()

                        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara2.Range.Text = "Familia:" & dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                        oPara2.Range.Font.Bold = False
                        oPara2.Format.SpaceAfter = 6
                        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara2.Range.InsertParagraphAfter()

                        oWord.Selection.Find.ClearFormatting()
                        With oWord.Selection.Find
                            .Text = dt_Familias.Rows(i + 1).Item("NombreFamilia").ToString
                            .Replacement.Text = ""
                            .Forward = True
                            .Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
                            .Format = False
                            .MatchCase = False
                            .MatchWholeWord = False
                            .MatchWildcards = False
                            .MatchSoundsLike = False
                            .MatchAllWordForms = False
                        End With
                        oWord.Selection.Find.Execute()
                        oWord.Selection.Font.Bold = True

                        cont = 0
                        cont_Hermanos = dv_Alumnos.Count

                        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Hermanos + 1, 2)
                        oTable.Range.ParagraphFormat.SpaceAfter = 3
                        oTable.Cell(1, 1).Range.Text = "Alumno(a):"
                        oTable.Cell(1, 1).Range.Font.Bold = False
                        oTable.Cell(1, 2).Range.Text = "Armadas que adeuda:"
                        oTable.Cell(1, 2).Range.Font.Bold = False

                        While cont <= dv_Alumnos.Count - 1

                            oTable.Cell(cont + 2, 1).Range.Text = dv_Alumnos.Item(cont).Item("NombreAlumno").ToString
                            oTable.Cell(cont + 2, 2).Range.Text = dv_Alumnos.Item(cont).Item("deuda").ToString
                            oTable.Cell(cont + 2, 1).Range.Font.Bold = True
                            oTable.Cell(cont + 2, 2).Range.Font.Bold = True
                            cont = cont + 1
                        End While

                        oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara5.Range.Text = " "
                        oPara5.Range.Font.Bold = False
                        oPara5.Format.SpaceAfter = 6
                        oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara5.Range.InsertParagraphAfter()

                        oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara6.Range.Text = "Por medio de la presente le recordamos que tiene pendiente el pago de la armada arriba mencionada."
                        oPara6.Range.Font.Bold = False
                        oPara6.Format.SpaceAfter = 6
                        oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara6.Range.InsertParagraphAfter()

                        oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara12.Range.Text = " "
                        oPara12.Range.Font.Bold = False
                        oPara12.Format.SpaceAfter = 6
                        oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara12.Range.InsertParagraphAfter()

                        oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara7.Range.Text = "Sírvase cancelar la deuda pendiente en las oficinas del Banco Interbank ó del Banco de Crédito, ya que la morosidad de las armadas afecta el normal desenvolvimiento de nuestra institución."
                        oPara7.Range.Font.Bold = False
                        oPara7.Format.SpaceAfter = 6
                        oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara7.Range.InsertParagraphAfter()

                        oPara13 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara13.Range.Text = " "
                        oPara13.Range.Font.Bold = False
                        oPara13.Format.SpaceAfter = 6
                        oPara13.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara13.Range.InsertParagraphAfter()

                        oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara8.Range.Text = "Si se encuentra al día en sus pagos, ignore esta deuda que figura como pendiente en el sistema hasta el día de hoy."
                        oPara8.Range.Font.Bold = False
                        oPara8.Format.SpaceAfter = 6
                        oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara8.Range.InsertParagraphAfter()

                        oPara14 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara14.Range.Text = " "
                        oPara14.Range.Font.Bold = False
                        oPara14.Format.SpaceAfter = 6
                        oPara14.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara14.Range.InsertParagraphAfter()

                        oPara9 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara9.Range.Text = "Atentamente."
                        oPara9.Range.Font.Bold = False
                        oPara9.Format.SpaceAfter = 6
                        oPara9.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara9.Range.InsertParagraphAfter()

                        oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara15.Range.Text = " "
                        oPara15.Range.Font.Bold = False
                        oPara15.Format.SpaceAfter = 6
                        oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara15.Range.InsertParagraphAfter()

                        oPara10 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara10.Range.Font.Bold = False
                        oPara10.Range.Text = "Janeth Velasco"
                        oPara10.Format.SpaceAfter = 6
                        oPara10.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara10.Range.InsertParagraphAfter()

                        oPara11 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                        oPara11.Range.Font.Bold = False
                        oPara11.Range.Text = "Tesorería"
                        oPara11.Format.SpaceAfter = 6
                        oPara11.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                        oPara11.Range.InsertParagraphAfter()

                        If cont_Hermanos = 1 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()

                            oPara15 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara15.Range.Text = " "
                            oPara15.Format.SpaceAfter = 6
                            oPara15.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara15.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 2 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()

                            oPara12 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara12.Range.Text = " "
                            oPara12.Format.SpaceAfter = 6
                            oPara12.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara12.Range.InsertParagraphAfter()
                        ElseIf cont_Hermanos = 3 Then
                            oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                            oPara5.Range.Text = " "
                            oPara5.Format.SpaceAfter = 6
                            oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                            oPara5.Range.InsertParagraphAfter()
                        End If

                    End If

                End If

            End If

            cont = 0
            i = i + 1
        Next

        oWord.Selection.MoveUp(5, Count:=1)

        Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
        Dim str_RutaGuardar As String = ""


        str_nombreDoc = "Esquela1_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
        str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()

        'Quit Word and thoroughly deallocate everything
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc
    End Function

    Public Shared Function Dev_NombreMes(ByVal int_CodigoMes As Integer) As String
        Dim str_NombreMes As String = ""

        If int_CodigoMes = 1 Then
            str_NombreMes = "Enero"
        ElseIf int_CodigoMes = 2 Then
            str_NombreMes = "Febrero"
        ElseIf int_CodigoMes = 3 Then
            str_NombreMes = "Marzo"
        ElseIf int_CodigoMes = 4 Then
            str_NombreMes = "Abril"
        ElseIf int_CodigoMes = 5 Then
            str_NombreMes = "Mayo"
        ElseIf int_CodigoMes = 6 Then
            str_NombreMes = "Junio"
        ElseIf int_CodigoMes = 7 Then
            str_NombreMes = "Julio"
        ElseIf int_CodigoMes = 8 Then
            str_NombreMes = "Agosto"
        ElseIf int_CodigoMes = 9 Then
            str_NombreMes = "Septiembre"
        ElseIf int_CodigoMes = 10 Then
            str_NombreMes = "Octubre"
        ElseIf int_CodigoMes = 11 Then
            str_NombreMes = "Noviembre"
        ElseIf int_CodigoMes = 12 Then
            str_NombreMes = "Diciembre"
        End If

        Return str_NombreMes
    End Function

    Public Shared Function Dev_NombreDia(ByVal int_CodigoDia As Integer) As String
        Dim str_NombreDia As String = ""

        If int_CodigoDia = 1 Then
            str_NombreDia = "Lunes"
        ElseIf int_CodigoDia = 2 Then
            str_NombreDia = "Martes"
        ElseIf int_CodigoDia = 3 Then
            str_NombreDia = "Miercoles"
        ElseIf int_CodigoDia = 4 Then
            str_NombreDia = "Jueves"
        ElseIf int_CodigoDia = 5 Then
            str_NombreDia = "Viernes"
        ElseIf int_CodigoDia = 6 Then
            str_NombreDia = "Sábado"
        ElseIf int_CodigoDia = 7 Then
            str_NombreDia = "Domingo"
        End If

        Return str_NombreDia
    End Function

    'Modulo Interfaz Familia Weekly Report
    'Private Shared currentContext As System.Web.HttpContext = System.Web.HttpContext.Current

    'Public Shared Function GenerarReporte(ByVal ds As DataSet) As String

    '    Dim dt_Alumnos As DataTable = ds.Tables(0)
    '    Dim dt_Cursos As DataTable = ds.Tables(1)
    '    Dim dt_Criterios As DataTable = ds.Tables(2)
    '    Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
    '    Dim dt_Notas As DataTable = ds.Tables(4)
    '    Dim dt_CabeceraTutor As DataTable = ds.Tables(5)
    '    Dim dt_Leyenda As DataTable = ds.Tables(6)
    '    Dim dt_GrupoCriterioEvaluacion As DataTable = ds.Tables(7)

    '    Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
    '    Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing

    '    Dim str_CodigoAlumno As String = ""

    '    'Iniciamos el Word 
    '    Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaReportesSeguimientoWeekly").ToString()
    '    oWord = New Microsoft.Office.Interop.Word.Application
    '    oWord.Visible = False

    '    oDoc = oWord.Documents.Add(saArchivo)
    '    oDoc.Content.Copy()

    '    For cont As Integer = 0 To dt_Alumnos.Rows.Count - 1

    '        str_CodigoAlumno = dt_Alumnos.Rows(cont).Item("CodigoAlumno")

    '        Dim dv_Cursos As DataView = dt_Cursos.DefaultView
    '        dv_Cursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        Dim dv_Criterios As DataView = dt_Criterios.DefaultView
    '        dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        Dim dv_CriteriosYCalificativos As DataView = dt_CriteriosYCalificativos.DefaultView
    '        dv_CriteriosYCalificativos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        Dim dv_Notas As DataView = dt_Notas.DefaultView
    '        dv_Notas.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        Dim dv_CabeceraTutor As DataView = dt_CabeceraTutor.DefaultView
    '        dv_CabeceraTutor.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '        'Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
    '        Dim oTablaCabecera As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTablaPie As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTabla As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTablaNotas As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTablaComentarioCopiaPadreFamilia As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTablaComentarioTutor As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oTablaLinea As Microsoft.Office.Interop.Word.Table = Nothing
    '        Dim oPara1, oPara2, oPara3, oPara4, oParaVoid1, oParaVoid2, oParaVoid3, oParaPageBreak, oParaDocIni As Microsoft.Office.Interop.Word.Paragraph
    '        Dim sel As Microsoft.Office.Interop.Word.Selection

    '        If cont = 0 Then
    '            oParaDocIni = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '            oParaDocIni.Range.Text = " "
    '            oParaDocIni.Range.Font.Name = "Arial"
    '            oParaDocIni.Range.Font.Size = 10
    '            oParaDocIni.Range.Font.Bold = True
    '            With oParaDocIni.Range.ParagraphFormat
    '                .SpaceBefore = 1
    '                .SpaceBeforeAuto = False
    '                .SpaceAfter = 1
    '                .SpaceAfterAuto = False
    '                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '            End With
    '            oParaDocIni.Range.InsertParagraph()
    '        End If

    '        'Aqui va el detalle
    '        obtenerFormatoInicial(oDoc, oWord, ds, str_CodigoAlumno)
    '        'Aqui termina el detalle

    '        ' Tabla Comentario Copia para el Padre de Familia
    '        oTablaComentarioCopiaPadreFamilia = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 2, 3)
    '        oTablaComentarioCopiaPadreFamilia.Range.Font.Name = "Arial"
    '        oTablaComentarioCopiaPadreFamilia.Range.Font.Size = 7
    '        oTablaComentarioCopiaPadreFamilia.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '        oTablaComentarioCopiaPadreFamilia.Borders.Enable = True

    '        'Columna de la tabla
    '        oTablaComentarioCopiaPadreFamilia.Columns(1).SetWidth(298, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaComentarioCopiaPadreFamilia.Columns(2).SetWidth(18, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaComentarioCopiaPadreFamilia.Columns(3).SetWidth(241, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

    '        oTablaComentarioCopiaPadreFamilia.Cell(1, 2).Select()
    '        sel = oWord.Selection
    '        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '        sel.Rows.Height = oWord.CentimetersToPoints(1.3)

    '        oTablaComentarioCopiaPadreFamilia.Cell(2, 2).Select()
    '        sel = oWord.Selection
    '        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '        sel.Rows.Height = oWord.CentimetersToPoints(1.4)

    '        Dim str_Firma As String = ""
    '        For i As Integer = 0 To 97
    '            str_Firma = str_Firma + "_"
    '        Next

    '        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oPara1.Range.Text = ""
    '        oPara1.Range.InsertParagraphAfter()

    '        ' Tabla de Linea de separación
    '        oTablaLinea = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 1, 1)
    '        oTablaLinea.Range.Font.Name = "Arial"
    '        oTablaLinea.Range.Font.Size = 10
    '        oTablaLinea.Range.Font.Bold = False
    '        oTablaLinea.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '        oTablaLinea.Borders.Enable = False

    '        oTablaLinea.Columns(1).SetWidth(560, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

    '        oTablaLinea.Cell(1, 1).Range.Text = str_Firma


    '        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '        oPara2.Range.Text = ""
    '        oPara2.Range.InsertParagraphAfter()

    '        obtenerFormatoInicial(oDoc, oWord, ds, str_CodigoAlumno)

    '        ' Tabla Comentario Tutor
    '        oTablaComentarioTutor = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 1, 3)
    '        oTablaComentarioTutor.Range.Font.Name = "Arial"
    '        oTablaComentarioTutor.Range.Font.Size = 7
    '        oTablaComentarioTutor.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '        oTablaComentarioTutor.Borders.Enable = True

    '        'Columna de la tabla
    '        oTablaComentarioTutor.Columns(1).SetWidth(298, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaComentarioTutor.Columns(2).SetWidth(18, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaComentarioTutor.Columns(3).SetWidth(241, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

    '        oTablaComentarioTutor.Cell(1, 2).Select()
    '        sel = oWord.Selection
    '        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '        sel.Rows.Height = oWord.CentimetersToPoints(1.3)

    '        oTablaComentarioTutor.Cell(2, 2).Select()
    '        sel = oWord.Selection
    '        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '        sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '        sel.Rows.Height = oWord.CentimetersToPoints(1.4)

    '    Next

    '    ' Grabar el reporte Word()
    '    Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
    '    Dim str_RutaGuardar As String = ""
    '    Dim str_nombreDoc As String = ""

    '    str_nombreDoc = "WeeklyReport_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
    '    str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
    '    oDoc.SaveAs(str_RutaGuardar)
    '    oDoc.Close()

    '    'Quit Word and thoroughly deallocate everything
    '    oWord.Quit()
    '    System.GC.Collect()

    '    Return str_nombreDoc

    'End Function

    'Public Shared Sub obtenerFormatoInicial(ByVal oDoc As Microsoft.Office.Interop.Word.Document, ByVal oWord As Microsoft.Office.Interop.Word.Application, ByVal ds As DataSet, ByVal str_CodigoAlumno As String)

    '    Dim dt_Cursos As DataTable = ds.Tables(1)
    '    Dim dt_Criterios As DataTable = ds.Tables(2)
    '    Dim dt_CriteriosYCalificativos As DataTable = ds.Tables(3)
    '    Dim dt_Notas As DataTable = ds.Tables(4)
    '    Dim dt_CabeceraTutor As DataTable = ds.Tables(5)
    '    Dim dt_Leyenda As DataTable = ds.Tables(6)
    '    Dim dt_GrupoCriterioEvaluacion As DataTable = ds.Tables(7)

    '    Dim oTablaCabecera As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oTablaPie As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oTabla As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oTablaNotas As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oTablaComentarioCopiaPadreFamilia As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oTablaLinea As Microsoft.Office.Interop.Word.Table = Nothing
    '    Dim oPara1, oPara2, oPara3, oPara4, oParaVoid1, oParaVoid2, oParaVoid3, oParaPageBreak, oParaDocIni As Microsoft.Office.Interop.Word.Paragraph
    '    Dim sel As Microsoft.Office.Interop.Word.Selection
    '    Dim dv_Cursos As DataView = dt_Cursos.DefaultView
    '    dv_Cursos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '    'Dim dv_Criterios As DataView = dt_Criterios.DefaultView
    '    'dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '    Dim dv_CriteriosYCalificativos As DataView = dt_CriteriosYCalificativos.DefaultView
    '    dv_CriteriosYCalificativos.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '    Dim dv_Notas As DataView = dt_Notas.DefaultView
    '    dv_Notas.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '    Dim dv_CabeceraTutor As DataView = dt_CabeceraTutor.DefaultView
    '    dv_CabeceraTutor.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "'"

    '    ' Tabla datos del Alumno

    '    oTablaCabecera = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, 4, 5)
    '    oTablaCabecera.Range.Font.Name = "Arial"
    '    oTablaCabecera.Range.Font.Size = 9
    '    oTablaCabecera.Range.Font.Bold = False
    '    oTablaCabecera.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '    oTablaCabecera.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '    oTablaCabecera.Borders.Enable = True

    '    oTablaCabecera.Range.InsertParagraphBefore()

    '    oTablaCabecera.Columns(1).SetWidth(50, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaCabecera.Columns(2).SetWidth(40.8, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaCabecera.Columns(3).SetWidth(180.8, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaCabecera.Columns(4).SetWidth(130.8, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaCabecera.Columns(5).SetWidth(150.4, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

    '    oTablaCabecera.Cell(1, 2).Merge(oTablaCabecera.Cell(1, 4))
    '    oTablaCabecera.Cell(1, 2).Range.Text = "WEEKLY REPORT"
    '    oTablaCabecera.Cell(1, 2).Range.Font.Size = 16
    '    oTablaCabecera.Cell(1, 2).Range.Font.Bold = True
    '    oTablaCabecera.Cell(1, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '    oTablaCabecera.Cell(1, 2).Select()
    '    sel = oWord.Selection
    '    sel.Rows.Height = oWord.CentimetersToPoints(0.2)
    '    'sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

    '    'imagen del escudo
    '    oTablaCabecera.Cell(1, 1).Merge(oTablaCabecera.Cell(4, 1))
    '    Dim str_ImagenEscudoLogo As String = ConfigurationManager.AppSettings("RutaImagenEscudoLogoBlanco_Web").ToString() 'currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaImagenEscudoLogoBlanco_Web").ToString()

    '    oTablaCabecera.Cell(1, 1).Range.InlineShapes.AddPicture(FileName:=str_ImagenEscudoLogo, LinkToFile:=False, SaveWithDocument:=True)
    '    oTablaCabecera.Cell(1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '    oTablaCabecera.Cell(1, 1).Select()
    '    sel = oWord.Selection
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalBottom

    '    oParaVoid1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oParaVoid1.Range.Text = " "
    '    oParaVoid1.Range.Font.Name = "Arial"
    '    oParaVoid1.Range.Font.Size = 10
    '    oParaVoid1.Range.Font.Bold = True
    '    With oParaVoid1.Range.ParagraphFormat
    '        .SpaceBefore = 1
    '        .SpaceBeforeAuto = False
    '        .SpaceAfter = 1
    '        .SpaceAfterAuto = False
    '        .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '    End With
    '    oParaVoid1.Range.InsertParagraph()

    '    'medio
    '    oTablaCabecera.Cell(1, 3).Select()
    '    oTablaCabecera.Cell(2, 2).Range.Text = "Name: "
    '    oTablaCabecera.Cell(3, 2).Range.Text = "Class: "
    '    oTablaCabecera.Cell(4, 2).Range.Text = "Tutor: "
    '    oTablaCabecera.Cell(2, 3).Range.Text = dv_CabeceraTutor(0).Item("NombreCompleto").ToString
    '    oTablaCabecera.Cell(3, 3).Range.Text = dv_CabeceraTutor(0).Item("DescGrado").ToString & " " & dv_CabeceraTutor(0).Item("DescAula").ToString
    '    oTablaCabecera.Cell(4, 3).Range.Text = dv_CabeceraTutor(0).Item("DescPersonaTutor").ToString
    '    oTablaCabecera.Cell(2, 4).Range.Text = dt_Leyenda.Rows(0).Item("DescCalificativoWeekly").ToString
    '    oTablaCabecera.Cell(3, 4).Range.Text = dt_Leyenda.Rows(1).Item("DescCalificativoWeekly").ToString
    '    oTablaCabecera.Cell(4, 4).Range.Text = dt_Leyenda.Rows(2).Item("DescCalificativoWeekly").ToString

    '    'fecha

    '    oTablaCabecera.Cell(1, 3).Select()
    '    sel = oWord.Selection
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

    '    oTablaCabecera.Cell(2, 5).Select()
    '    sel = oWord.Selection
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

    '    oTablaCabecera.Cell(3, 5).Select()
    '    sel = oWord.Selection
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    oTablaCabecera.Cell(3, 5).Range.Text = dv_CabeceraTutor(0).Item("DescFecha").ToString
    '    oTablaCabecera.Cell(3, 5).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight

    '    oTablaCabecera.Cell(4, 5).Select()
    '    sel = oWord.Selection
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    sel.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    '    oTablaCabecera.Cell(4, 5).Range.Text = dv_CabeceraTutor(0).Item("DescBimestre").ToString & "        " & dv_CabeceraTutor(0).Item("DesSemana").ToString
    '    oTablaCabecera.Cell(4, 5).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
    '    oTablaCabecera.Cell(4, 5).Range.Font.Size = 10
    '    oTablaCabecera.Cell(4, 5).Range.Font.Bold = True
    '    'Selection.TypeText(Text:="        ")
    '    ' Tabla Notas
    '    oTablaNotas = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, dt_Criterios.Rows.Count + dt_GrupoCriterioEvaluacion.Rows.Count + 1, 12)
    '    oTablaNotas.Range.Font.Name = "Arial"
    '    oTablaNotas.Range.Font.Size = 6
    '    oTablaNotas.Range.Font.Bold = False
    '    oTablaNotas.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
    '    oTablaNotas.Borders.Enable = True

    '    oTablaNotas.Range.InsertParagraphBefore()

    '    'Estructura y Cabecera de la Tabla Notas
    '    oTablaNotas.Columns(1).SetWidth(150, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(2).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(3).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(4).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(5).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(6).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(7).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(8).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(9).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(10).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(11).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '    oTablaNotas.Columns(12).SetWidth(37, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)

    '    oTablaNotas.Cell(1, 1).Range.Text = "Subject"
    '    oTablaNotas.Cell(1, 1).Range.Font.Size = 10
    '    oTablaNotas.Cell(1, 1).Range.Font.Bold = True
    '    oTablaNotas.Cell(1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter

    '    oTablaNotas.Cell(1, 1).Select()
    '    sel = oWord.Selection
    '    sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
    '    sel.Rows.Height = oWord.CentimetersToPoints(0.9)
    '    sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

    '    For i As Integer = 0 To dv_Cursos.Count - 1
    '        'oTablaNotas.Columns(i + 2).SetWidth(50, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustSameWidth)
    '        oTablaNotas.Cell(1, i + 2).Range.Text = dv_Cursos(i).Item("DescNombreCurso").ToString
    '        'oTablaNotas.Cell(1, 1).Range.Font.Size = 8
    '        With oTablaNotas.Cell(1, i + 2).Range.ParagraphFormat
    '            .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '            .SpaceBefore = 1
    '            .SpaceBeforeAuto = False
    '            .SpaceAfter = 1
    '            .SpaceAfterAuto = False
    '            .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '        End With
    '    Next

    '    'Detalle Notas
    '    Dim int_CodigoCriterio, int_CodigoCurso, int_Filas, int_Columnas As Integer
    '    Dim str_DescCriterio As String = ""

    '    int_Filas = 0
    '    int_Columnas = 0

    '    Dim int_CodigoGrupoCriterio As Integer = 0
    '    Dim int_CantidadCriterios As Integer = 0
    '    Dim str_CodigoGrupoCriterio As String = ""
    '    Dim int_cantAumFila = 3
    '    Dim dv_Criterios As DataView = dt_Criterios.DefaultView

    '    For i As Integer = 0 To dt_GrupoCriterioEvaluacion.Rows.Count - 1
    '        int_CodigoGrupoCriterio = dt_GrupoCriterioEvaluacion.Rows(i).Item("CodigoGrupoCriterio")
    '        str_CodigoGrupoCriterio = dt_GrupoCriterioEvaluacion.Rows(i).Item("GrupoCriterioEvaluacion")

    '        oTablaNotas.Cell(2 + int_CantidadCriterios, 1).Range.Text = str_CodigoGrupoCriterio
    '        oTablaNotas.Cell(2 + int_CantidadCriterios, 1).Range.Font.Size = 10
    '        oTablaNotas.Cell(2 + int_CantidadCriterios, 1).Range.Font.Bold = True
    '        dv_Criterios.RowFilter = "1=1 and CodigoAlumno = '" & str_CodigoAlumno & "' and CodigoGrupoCriterio = '" & CStr(int_CodigoGrupoCriterio) & "'"

    '        While int_Filas <= dv_Criterios.Count - 1 ' Filas Criterios

    '            int_CodigoCriterio = dv_Criterios(int_Filas).Item("CodigoCriterio").ToString
    '            str_DescCriterio = dv_Criterios(int_Filas).Item("Criterio").ToString
    '            oTablaNotas.Cell(int_Filas + int_cantAumFila, 1).Range.Text = str_DescCriterio

    '            oTablaNotas.Cell(int_Filas + int_cantAumFila, 1).Select()
    '            sel = oWord.Selection
    '            sel.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly
    '            'sel.Rows.Height = oWord.CentimetersToPoints(0.5)
    '            sel.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

    '            With oTablaNotas.Cell(int_Filas + int_cantAumFila, 1).Range.ParagraphFormat
    '                .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
    '                .LeftIndent = oWord.CentimetersToPoints(0.2)
    '                .RightIndent = oWord.CentimetersToPoints(0)
    '                .SpaceBefore = 1
    '                .SpaceBeforeAuto = False
    '                .SpaceAfter = 1
    '                .SpaceAfterAuto = False
    '                .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '            End With

    '            While int_Columnas <= dv_Cursos.Count - 1 ' Columnas cursos
    '                int_CodigoCurso = dv_Cursos(int_Columnas).Item("CodigoCurso")
    '                oTablaNotas.Cell(int_Filas + int_cantAumFila, int_Columnas + 2).Range.Text = obtenerNota(dv_Notas, int_CodigoCurso, int_CodigoCriterio)
    '                With oTablaNotas.Cell(int_Filas + int_cantAumFila, int_Columnas + 2).Range.ParagraphFormat
    '                    .Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
    '                    .SpaceBefore = 1
    '                    .SpaceBeforeAuto = False
    '                    .SpaceAfter = 1
    '                    .SpaceAfterAuto = False
    '                    .LineSpacingRule = Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceSingle
    '                End With
    '                int_Columnas += 1
    '            End While

    '            int_CantidadCriterios = dv_Criterios.Count
    '            str_DescCriterio = ""
    '            int_CodigoCurso = 0
    '            int_Columnas = 0
    '            int_Filas += 1
    '        End While
    '        int_Filas = 0
    '        int_CantidadCriterios = int_CantidadCriterios + 1
    '        int_cantAumFila = int_cantAumFila + int_CantidadCriterios
    '    Next
    '    oPara3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
    '    oPara3.Range.Text = ""
    '    oPara3.Range.InsertParagraphAfter()

    '    'Return int_Fila

    'End Sub

#Region "Módulo Pensiones"

#Region "Generación de Compromiso Pago de Letras"

    ' WORD
    Public Shared Function GenerarCompromisoPago(ByVal dt As DataTable) As String

        Dim oWord As Microsoft.Office.Interop.Word.Application = Nothing
        Dim oDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Dim oHoja As Microsoft.Office.Interop.Word.Page = Nothing
        Dim oTable As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oTableCronograma As Microsoft.Office.Interop.Word.Table = Nothing
        Dim oRng As Microsoft.Office.Interop.Word.Range = Nothing
        Dim oShape As Microsoft.Office.Interop.Word.InlineShape = Nothing
        Dim oPara0, oPara1, oPara2, oPara3, oPara4, oPara5, oPara6, oPara7, oPara8 As Microsoft.Office.Interop.Word.Paragraph
        Dim oChart As Object = Nothing
        Dim Pos As Double = 0
        Dim numAlum As String = ""

        'Iniciamos el Word 
        Dim saArchivo As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaPlantillaCompromisoPagoLetra").ToString()
        oWord = New Microsoft.Office.Interop.Word.Application
        oWord.Visible = False
        oDoc = oWord.Documents.Add(saArchivo)
        oDoc.Content.Copy()

        oPara0 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara0.Range.Text = ""
        oPara0.Format.SpaceAfter = 6
        oPara0.Range.InsertParagraphAfter()
        oPara0.Range.InsertParagraphAfter()
        oPara0.Range.InsertParagraphAfter()

        oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara1.Range.Text = "COMPROMISO DE PAGO - CUOTA DE INGRESO"
        oPara1.Range.Font.Name = "Arial Narrow"
        oPara1.Format.SpaceAfter = 6
        oPara1.Range.Font.Bold = True
        oPara1.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle
        oPara1.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
        oPara1.Range.InsertParagraphAfter()

        oPara0.Range.InsertParagraphAfter()

        oPara2 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara2.Range.Text = "Yo, " & dt.Rows(0).Item("CompromisoPagoNombreFamiliar") & ", me comprometo en abonar el monto de la(s) siguiente(s) letra(s) " & _
                            "a favor del Colegio San Jorge de Miraflores, por el concepto del Saldo de la Cuota de Ingreso de mi hijo(a) " & _
                            dt.Rows(0).Item("CompromisoPagoNombreHijo") & " la(s) siguiente(s) fecha(s) de vencimiento:"
        oPara2.Range.Font.Name = "Arial Narrow"
        oPara2.Format.SpaceAfter = 6
        oPara2.Range.Font.Bold = False
        oPara2.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
        oPara1.Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone
        oPara2.Range.InsertParagraphAfter()

        Dim cont_Letras, cont As Integer
        cont_Letras = dt.Rows.Count
        cont = 0

        oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("\endofdoc").Range, cont_Letras + 1, 4)
        oTable.Range.ParagraphFormat.SpaceAfter = 3
        oTable.Cell(1, 1).Range.Text = "#"
        oTable.Cell(1, 2).Range.Text = "Fecha Emisión"
        oTable.Cell(1, 3).Range.Text = "Fecha Vcto."
        oTable.Cell(1, 4).Range.Text = "Monto"

        For i As Integer = 0 To 3
            oTable.Cell(1, i + 1).Range.Font.Name = "Arial Narrow"
            oTable.Cell(1, i + 1).Range.FormattedText.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
        Next

        While cont <= cont_Letras - 1
            oTable.Cell(cont + 2, 1).Range.Text = dt.Rows(cont).Item("NumeroLetra").ToString
            oTable.Cell(cont + 2, 2).Range.Text = dt.Rows(cont).Item("FechaGiro").ToString
            oTable.Cell(cont + 2, 3).Range.Text = dt.Rows(cont).Item("FechaVencimiento").ToString
            oTable.Cell(cont + 2, 4).Range.Text = dt.Rows(cont).Item("SimboloMoneda").ToString & " " & dt.Rows(cont).Item("MontoTotal").ToString

            For i As Integer = 0 To 3
                oTable.Cell(cont + 2, i + 1).Range.Font.Bold = False
                oTable.Cell(cont + 2, i + 1).Range.FormattedText.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
            Next

            cont = cont + 1
        End While

        oPara0.Range.InsertParagraphAfter()

        oPara3 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara3.Range.Text = "El pago se realizará solo en las Tiendas Financieras del Banco Interbank (este servicio no está disponible en los supermercados como Plaza Vea o Vivanda, ni en las agencias de banca personal)."
        oPara3.Range.Font.Name = "Arial Narrow"
        oPara3.Format.SpaceAfter = 6
        oPara3.Range.Font.Bold = False
        oPara3.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
        oPara3.Range.InsertParagraphAfter()

        oPara4 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara4.Range.Text = "De no haberse cancelado la letra a la fecha de vencimiento, usted tendrá como plazo máximo 8 días para cancelarla, de lo contrario la letra será protestada y los gastos serán asumidos por el aceptante de la letra."
        oPara4.Range.Font.Name = "Arial Narrow"
        oPara4.Format.SpaceAfter = 6
        oPara4.Range.Font.Bold = False
        oPara4.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
        oPara4.Range.InsertParagraphAfter()

        oPara5 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara5.Range.Text = "En caso se presentara algún inconveniente con el Pago de Letra o no le llegase el aviso de vencimiento, comunicarse con la Tesorería del Colegio al teléfono 445-8147 anexo 121."
        oPara5.Range.Font.Name = "Arial Narrow"
        oPara5.Format.SpaceAfter = 6
        oPara5.Range.Font.Bold = False
        oPara5.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
        oPara5.Range.InsertParagraphAfter()

        oPara6 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara6.Range.Text = "Así mismo, declaro tener conocimiento que en caso desista de tomar la vacante obtenida habiendo realizado ya un primer pago, el colegio retendrá el 25% del total de la cuota de ingreso por concepto de gastos administrativos."
        oPara6.Range.Font.Name = "Arial Narrow"
        oPara6.Format.SpaceAfter = 6
        oPara6.Range.Font.Bold = False
        oPara6.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
        oPara6.Range.InsertParagraphAfter()

        oPara0.Range.InsertParagraphAfter()

        oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara7.Range.Text = "Miraflores, ..... de .................... del 2,0...."
        oPara7.Range.Font.Name = "Arial Narrow"
        oPara7.Format.SpaceAfter = 6
        oPara7.Range.Font.Bold = False
        oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
        oPara7.Range.InsertParagraphAfter()

        oPara7 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara7.Range.Text = "______________________"
        oPara7.Range.Font.Name = "Arial Narrow"
        oPara7.Format.SpaceAfter = 6
        oPara7.Range.Font.Bold = False
        oPara7.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
        oPara7.Range.InsertParagraphAfter()

        oPara8 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
        oPara8.Range.Text = "DNI ........................................"
        oPara8.Range.Font.Name = "Arial Narrow"
        oPara8.Format.SpaceAfter = 6
        oPara8.Range.Font.Bold = False
        oPara8.Format.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify
        oPara8.Range.InsertParagraphAfter()

        Dim sTempFolderPath As String = System.IO.Path.GetTempPath()
        Dim str_RutaGuardar As String = ""
        Dim str_nombreDoc As String = ""

        str_nombreDoc = "CompromisoPago_" & Date.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "_") & ".doc"
        str_RutaGuardar = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & str_nombreDoc
        oDoc.SaveAs(str_RutaGuardar)
        oDoc.Close()

        'Quit Word and thoroughly deallocate everything
        oWord.Quit()
        System.GC.Collect()

        Return str_nombreDoc
    End Function

    ' HTML 
    Public Shared Function GenerarCompromisoPagoHtml(ByVal dt As DataTable) As String

        Dim rutamadre As String = HttpContext.Current.ApplicationInstance.Server.MapPath("/SaintGeorgeOnline")
        Dim ArchLecturaEstructura As String = rutamadre
        Dim fileReaderPlantilla As String = ""
        Dim DatosExportacionHtml(1) As String
        Dim nombreArchivo As String = ""

        Try
            nombreArchivo = GetNewName()
            ArchLecturaEstructura = rutamadre & ConfigurationManager.AppSettings.Item("RutaPlantillaCompromisoPago").ToString()
            fileReaderPlantilla = My.Computer.FileSystem.ReadAllText(ArchLecturaEstructura)

            Dim str_NombreFamiliar As String = dt.Rows(0).Item("CompromisoPagoNombreFamiliar")
            Dim str_NombreAlumno As String = dt.Rows(0).Item("CompromisoPagoNombreHijo")

            fileReaderPlantilla = fileReaderPlantilla.Replace("[Nombre_Familiar]", str_NombreFamiliar)
            fileReaderPlantilla = fileReaderPlantilla.Replace("[Nombre_Alumno]", str_NombreAlumno)

            Dim sb_ListaLetras As New StringBuilder
            sb_ListaLetras.Append("<table cellpadding='0' cellspacing='0' border='0' style='width: 500px; font-family:Arial;font-size:10pt;'><tr>")
            sb_ListaLetras.Append("<th style='width: 125px; height: 25; border-bottom: solid 1px #000000' align='center' valign='middle'>#</th>" & _
                                        "<th style='width: 125px; height: 25; border-bottom: solid 1px #000000' align='center' valign='middle'>Fecha Emisión</th>" & _
                                        "<th style='width: 125px; height: 25; border-bottom: solid 1px #000000' align='center' valign='middle'>Fecha Vcto.</th>" & _
                                        "<th style='width: 125px; height: 25; border-bottom: solid 1px #000000' align='center' valign='middle'>Monto</th><tr>")
            For Each dr As DataRow In dt.Rows
                sb_ListaLetras.Append("<tr>" & _
                                          "<td align='center' valign='middle'>" & dr.Item("NumeroLetra").ToString & "</td>" & _
                                          "<td align='center' valign='middle'>" & dr.Item("FechaGiro").ToString & "</td>" & _
                                          "<td align='center' valign='middle'>" & dr.Item("FechaVencimiento").ToString & "</td>" & _
                                          "<td align='center' valign='middle'>" & dr.Item("SimboloMoneda").ToString & " " & dr.Item("MontoTotal").ToString & "</td>" & _
                                          "</tr>")
            Next
            sb_ListaLetras.Append("</table>")
            fileReaderPlantilla = fileReaderPlantilla.Replace("[Tabla_Letras]", sb_ListaLetras.ToString)

            Dim str_RutaFile As String = currentContext.Server.MapPath(currentContext.Request.ApplicationPath) & _
                                        ConfigurationManager.AppSettings("RutaReportesExcel").ToString() & _
                                        nombreArchivo & ".doc"

            Dim sw As StreamWriter = New StreamWriter(str_RutaFile, True, System.Text.Encoding.UTF8)
            sw.Write(fileReaderPlantilla.ToString)
            sw.Close()

        Catch ex As Exception

        End Try

        Return nombreArchivo

    End Function

#End Region

#End Region

#End Region


#Region "Exportar : Impresion Boletas y Facturas"

    Enum FormatExport
        PDF
        WORD
        EXCEL
        RTF
    End Enum

    Public Shared Sub Export(ByVal cv As CrystalReportViewer, ByVal rpt As ReportDocument, ByVal FE As FormatExport, ByVal NombreArch As String, Optional ByVal Vertical As Boolean = True)
        Dim exp As ExportOptions
        Dim req As ExportRequestContext
        Dim st As System.IO.Stream
        Dim ContentTypeExport As String
        Dim b() As Byte
        Dim pg As Page
        Dim ext As String
        pg = cv.Page
        exp = New ExportOptions

        Select Case FE
            Case FormatExport.EXCEL
                Dim Oexcel As New ExportFormatType
                exp.ExportFormatType = ExportFormatType.Excel
                exp.FormatOptions = New ExcelFormatOptions
                ContentTypeExport = "application/vnd.ms-excel"
                ext = ".xls"
            Case FormatExport.PDF
                exp.ExportFormatType = ExportFormatType.PortableDocFormat
                exp.FormatOptions = New PdfRtfWordFormatOptions
                ContentTypeExport = "application/pdf"
                ext = ".pdf"
            Case FormatExport.WORD
                exp.ExportFormatType = ExportFormatType.WordForWindows
                exp.FormatOptions = New PdfRtfWordFormatOptions
                ContentTypeExport = "application/msword"
                ext = ".doc"

            Case FormatExport.RTF
                exp.ExportFormatType = ExportFormatType.RichText
                exp.FormatOptions = New PdfRtfWordFormatOptions
                ContentTypeExport = "application/rtf"
                ext = ".rtf"

        End Select
        req = New ExportRequestContext
        req.ExportInfo = exp
        With rpt.FormatEngine.PrintOptions
            .PaperSize = PaperSize.PaperA4
            If Vertical = True Then
                .PaperOrientation = PaperOrientation.Portrait
            Else
                .PaperOrientation = PaperOrientation.Landscape
            End If
        End With

        st = rpt.FormatEngine.ExportToStream(req)

        pg.Response.AddHeader("Content-Disposition", "attachment;filename=" & NombreArch & " " & DateTime.Now.Day & "-" & DateTime.Now.Month & "-" & DateTime.Now.Year & ext)
        pg.Response.ContentType = ContentTypeExport
        ReDim b(st.Length)
        st.Read(b, 0, CInt(st.Length))
        pg.Response.BinaryWrite(b)
        st.Flush()
        st.Close()
        pg.Response.End()
    End Sub

#End Region

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
    Private Shared Sub LlenarPlantillaSolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases(ByVal dtReporte As System.Data.DataSet, _
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
                ws.Cell(sumFilas, 5).Value = "Cantidad :"
                ws.Cell(sumFilas, 6).Value = "Unidad de Medida :"
                ws.Cell(sumFilas, 7).Value = "Precio :"
                ws.Cell(sumFilas, 8).Value = "Precio Total :"
                ws.Cell(sumFilas, 9).Value = "Ene :"
                ws.Cell(sumFilas, 10).Value = "Feb :"
                ws.Cell(sumFilas, 11).Value = "Mar :"
                ws.Cell(sumFilas, 12).Value = "Abr :"
                ws.Cell(sumFilas, 13).Value = "May :"
                ws.Cell(sumFilas, 14).Value = "Jun :"
                ws.Cell(sumFilas, 15).Value = "Jul :"
                ws.Cell(sumFilas, 16).Value = "Ago :"
                ws.Cell(sumFilas, 17).Value = "Sep :"
                ws.Cell(sumFilas, 18).Value = "Oct :"
                ws.Cell(sumFilas, 19).Value = "Nov :"
                ws.Cell(sumFilas, 20).Value = "Dic :"
                ws.Cell(sumFilas, 21).Value = "TOTAL :"


                With ws.Range(ws.Cell(sumFilas, 1), ws.Cell(sumFilas, 21))
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
                            ws.Cell(FilaDet, 9).Value = dv.Item(contDv).Item("CantENE").ToString
                            int_CantEne = int_CantEne + dv.Item(contDv).Item("CantENE").ToString
                            ws.Cell(FilaDet, 10).Value = dv.Item(contDv).Item("CantFEB").ToString
                            int_CantFeb = int_CantFeb + dv.Item(contDv).Item("CantFEB").ToString
                            ws.Cell(FilaDet, 11).Value = dv.Item(contDv).Item("CantMAR").ToString
                            int_CantMar = int_CantMar + dv.Item(contDv).Item("CantMAR").ToString
                            ws.Cell(FilaDet, 12).Value = dv.Item(contDv).Item("CantABR").ToString
                            int_CantAbr = int_CantAbr + dv.Item(contDv).Item("CantABR").ToString
                            ws.Cell(FilaDet, 13).Value = dv.Item(contDv).Item("CantMAY").ToString
                            int_CantMay = int_CantMay + dv.Item(contDv).Item("CantMAY").ToString
                            ws.Cell(FilaDet, 14).Value = dv.Item(contDv).Item("CantJUN").ToString
                            int_CantJun = int_CantJun + dv.Item(contDv).Item("CantJUN").ToString
                            ws.Cell(FilaDet, 15).Value = dv.Item(contDv).Item("CantJUL").ToString
                            int_CantJul = int_CantJul + dv.Item(contDv).Item("CantJUL").ToString
                            ws.Cell(FilaDet, 16).Value = dv.Item(contDv).Item("CantAGO").ToString
                            int_CantAgo = int_CantAgo + dv.Item(contDv).Item("CantAGO").ToString
                            ws.Cell(FilaDet, 17).Value = dv.Item(contDv).Item("CantSET").ToString
                            int_CantSep = int_CantSep + dv.Item(contDv).Item("CantSET").ToString
                            ws.Cell(FilaDet, 18).Value = dv.Item(contDv).Item("CantOCT").ToString
                            int_CantOct = int_CantOct + dv.Item(contDv).Item("CantOCT").ToString
                            ws.Cell(FilaDet, 19).Value = dv.Item(contDv).Item("CantNOV").ToString
                            int_CantNov = int_CantNov + dv.Item(contDv).Item("CantNOV").ToString
                            ws.Cell(FilaDet, 20).Value = dv.Item(contDv).Item("CantDIC").ToString
                            int_CantDic = int_CantDic + dv.Item(contDv).Item("CantDIC").ToString

                            int_cantidadTotal = CDec(dv.Item(contDv).Item("CantENE").ToString) + CDec(dv.Item(contDv).Item("CantFEB").ToString) + CDec(dv.Item(contDv).Item("CantMAR").ToString) + _
                            CDec(dv.Item(contDv).Item("CantABR").ToString) + CDec(dv.Item(contDv).Item("CantMAY").ToString) + CDec(dv.Item(contDv).Item("CantJUN").ToString) + CDec(dv.Item(contDv).Item("CantJUL").ToString) + _
                            CDec(dv.Item(contDv).Item("CantAGO").ToString) + CDec(dv.Item(contDv).Item("CantSET").ToString) + CDec(dv.Item(contDv).Item("CantOCT").ToString) + CDec(dv.Item(contDv).Item("CantNOV").ToString) + _
                            CDec(dv.Item(contDv).Item("CantDIC").ToString)

                            ws.Cell(FilaDet, 21).Value = int_cantidadTotal
                            int_CantTot = int_CantTot + int_cantidadTotal

                        End If
                        ''Pintado del cuadrado de la cabecera

                        ''.Merge()


                        With ws.Range(ws.Cell(FilaCab + 2, ColumnaCab), ws.Cell(FilaDet, ColumnaCab + 20))
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
                    ws.Cell(FilaDet, 9).Value = int_CantEne
                    ws.Cell(FilaDet, 10).Value = int_CantFeb
                    ws.Cell(FilaDet, 11).Value = int_CantMar
                    ws.Cell(FilaDet, 12).Value = int_CantAbr
                    ws.Cell(FilaDet, 13).Value = int_CantMay
                    ws.Cell(FilaDet, 14).Value = int_CantJun
                    ws.Cell(FilaDet, 15).Value = int_CantJul
                    ws.Cell(FilaDet, 16).Value = int_CantAgo
                    ws.Cell(FilaDet, 17).Value = int_CantSep
                    ws.Cell(FilaDet, 18).Value = int_CantOct
                    ws.Cell(FilaDet, 19).Value = int_CantNov
                    ws.Cell(FilaDet, 20).Value = int_CantDic
                    ws.Cell(FilaDet, 21).Value = int_CantTot

                    ''Pintado del cuadrado de Total
                    ' .Merge()
                    With ws.Range(ws.Cell(FilaDet, ColumnaCab + 6), ws.Cell(FilaDet, ColumnaCab + 20))
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
            ws.Cell(FilaCab, 9).Value = int_CantTotalEne
            ws.Cell(FilaCab, 10).Value = int_CantTotalFeb
            ws.Cell(FilaCab, 11).Value = int_CantTotalMar
            ws.Cell(FilaCab, 12).Value = int_CantTotalAbr
            ws.Cell(FilaCab, 13).Value = int_CantTotalMay
            ws.Cell(FilaCab, 14).Value = int_CantTotalJun
            ws.Cell(FilaCab, 15).Value = int_CantTotalJul
            ws.Cell(FilaCab, 16).Value = int_CantTotalAgo
            ws.Cell(FilaCab, 17).Value = int_CantTotalSep
            ws.Cell(FilaCab, 18).Value = int_CantTotalOct
            ws.Cell(FilaCab, 19).Value = int_CantTotalNov
            ws.Cell(FilaCab, 20).Value = int_CantTotalDic
            ws.Cell(FilaCab, 21).Value = int_CantTotalTot

            ' oExcel.Range(oCells(14, 1), oCells(FilaCab, 21)).WrapText = True
            ''Pintado del cuadrado de Total



            With ws.Range(ws.Cell(FilaCab, 1), ws.Cell(FilaCab, 21))
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

            workbook.Save()
            rutaTempDest = rutaREpositorioTemporales


        Catch ex As Exception

        End Try
    End Sub

End Class
