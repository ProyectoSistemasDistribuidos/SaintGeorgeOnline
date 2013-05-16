Imports SaintGeorgeOnline_DataAccess

Public Class BL_GSV_RegistroPrestamoLibros

    Public Function F_insertarPrestamosLIbros(ByVal dcPrestamos As List(Of Dictionary(Of Object, Object))) As Dictionary(Of Object, Object)
        Try
            Return New DA_GSV_RegistroPrestamoLibros().F_insertarPrestamosLIbros(dcPrestamos)
        Catch ex As Exception

        End Try
    End Function

    Public Function F_actualizarFechaPrestamoDetalleLibro(ByVal codPrestamo As Integer, ByVal fechaPrestamo As String) As Dictionary(Of Object, Object)

        Return New DA_GSV_RegistroPrestamoLibros().F_actualizarFechaPrestamoDetalleLibro(codPrestamo, fechaPrestamo)
    End Function


End Class
