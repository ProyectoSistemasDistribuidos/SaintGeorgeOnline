Imports Microsoft.Office.Interop.Excel
Imports ClosedXML.Excel

Partial Class subirArchivo_frmSubirArchivoBiblioteca
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        subirArchivo()
    End Sub

    Dim excel As New ApplicationClass
    Dim wbkWorkbook As Workbook
    Dim wshWorksheet As Worksheet
    Dim rng As Range

    Public Class fileUploadExcelBiblioteca
        Public codUsuari As String
        Public tipousuario As String
        Public nombre As String
        Public dni As String
        Public codigoBarras As String
        Public ubicacion As String
        Public titulo As String
        Public autor As String
        Public anio As String
        Public fechaPrestamo As String
        Public fechaDevolucion As String
        Public diasAtrazo As String
        Public estado As String
    End Class
    Private Sub subirArchivo()
        Try

            'Dim workbook As New XLWorkbook("D:\archivos de envio\Prestamos-2012-11-07.xlsx")


            'Dim ws = workbook.Worksheet(1)
            'Dim demo As String = ws.Cell(1, 1).Value


            'codigo de usuario	Tipo de usuario	Apellidos	Nombres	DNI	Código de barras	Ubicación	Título	Autor	Año de publicación	Fecha de prestamo	Fecha de Devolución	Días de Atrazo	Estado


            'Exit Sub


            wbkWorkbook = excel.Workbooks.Open("D:\archivos de envio\Prestamos-2012-11-07.csv")

            wshWorksheet = wbkWorkbook.Worksheets(1)
            wshWorksheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            wshWorksheet.Activate()
            wshWorksheet.Visible = True


            Dim cod As String = CType(excel.ActiveSheet.Cells(1, 1), Range).Value

            Dim lstExportacion As New List(Of fileUploadExcelBiblioteca)
            Dim ofileUploadExcelBiblioteca As fileUploadExcelBiblioteca

            Dim indiceFilas As Integer = 1
            Dim cantidadFilasVacias As Integer = 0
            Dim cntProfesor As String = "Profesor"
            Dim cntAlumno As String = "Alumno"
            Do
                indiceFilas += 1
                ofileUploadExcelBiblioteca = New fileUploadExcelBiblioteca
                ofileUploadExcelBiblioteca.codUsuari = CType(excel.ActiveSheet.Cells(indiceFilas, 1), Range).Value
                ofileUploadExcelBiblioteca.tipousuario = CType(excel.ActiveSheet.Cells(indiceFilas, 2), Range).Value
                ofileUploadExcelBiblioteca.dni = CType(excel.ActiveSheet.Cells(indiceFilas, 5), Range).Value
                ofileUploadExcelBiblioteca.codigoBarras = CType(excel.ActiveSheet.Cells(indiceFilas, 6), Range).Value
                ofileUploadExcelBiblioteca.titulo = CType(excel.ActiveSheet.Cells(indiceFilas, 8), Range).Value
                ofileUploadExcelBiblioteca.autor = CType(excel.ActiveSheet.Cells(indiceFilas, 9), Range).Value
                ofileUploadExcelBiblioteca.fechaPrestamo = CType(excel.ActiveSheet.Cells(indiceFilas, 11), Range).Value
                ofileUploadExcelBiblioteca.fechaDevolucion = CType(excel.ActiveSheet.Cells(indiceFilas, 12), Range).Value
                ofileUploadExcelBiblioteca.diasAtrazo = CType(excel.ActiveSheet.Cells(indiceFilas, 13), Range).Value
                ofileUploadExcelBiblioteca.estado = CType(excel.ActiveSheet.Cells(indiceFilas, 14), Range).Value


                If ofileUploadExcelBiblioteca.tipousuario = Nothing Then
                    cantidadFilasVacias += 1
                    If CType(excel.ActiveSheet.Cells(indiceFilas + 1, 2), Range).Value = Nothing Then
                        cantidadFilasVacias += 1
                    Else
                        cantidadFilasVacias = 0
                    End If

                End If
                If ofileUploadExcelBiblioteca.tipousuario = cntAlumno And cantidadFilasVacias <> 2 Then
                    lstExportacion.Add(ofileUploadExcelBiblioteca)
                End If

            Loop While cantidadFilasVacias <> 2

           

            wbkWorkbook.Save()
            EiminaReferencias(wshWorksheet)
            EiminaReferencias(wbkWorkbook)
            excel.Quit()
            EiminaReferencias(excel)
            System.GC.Collect()

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

End Class
