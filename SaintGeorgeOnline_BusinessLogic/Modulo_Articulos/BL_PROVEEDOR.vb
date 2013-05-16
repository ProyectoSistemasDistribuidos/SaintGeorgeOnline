
Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess
Public Class BL_PROVEEDOR
    Public Function F_insertarProveedor(ByVal oBE_PROVEEDOR As BE_PROVEEDOR) As List(Of String)
        Try
            Return New DA_PROVEEDOR().F_insertarProveedor(oBE_PROVEEDOR)
        Catch ex As Exception

        End Try
    End Function

End Class
