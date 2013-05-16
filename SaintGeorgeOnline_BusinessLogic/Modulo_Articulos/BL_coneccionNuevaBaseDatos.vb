
Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess
Public Class BL_coneccionNuevaBaseDatos
    Public Function listarProveedores() As DataSet
        Try
            Return New DA_articuloNewbaseDatos().listarProveedores()

        Catch ex As Exception

        End Try

    End Function
    Public Function BuscarOrdenCompra(ByVal fechaInicio As String, ByVal fechaFin As String, ByVal codProveedor As Integer, ByVal LimInf As Integer, ByVal LimSup As Integer, ByVal soloBuscar As Integer) As DataSet
        Try
            Return New DA_articuloNewbaseDatos().BuscarOrdenCompra(fechaInicio, fechaFin, codProveedor, LimInf, LimSup, soloBuscar)
        Catch ex As Exception

        End Try

    End Function

    Public Function F_insertarProveedor(ByVal descripcionProveedor As String) As List(Of String)
        Try
            Return New DA_articuloNewbaseDatos().F_insertarProveedor(descripcionProveedor)
        Catch ex As Exception

        End Try
    End Function



End Class
