Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess
Public Class BL_NombreArticulo
    Public Function F_insertarNombreArticulo(ByVal oBE_nombreArticulo As BE_nombreArticulo) As List(Of String)
        Try
            Return New DA_nombreArticulo().F_insertarNombreArticulo(oBE_nombreArticulo)
        Catch ex As Exception
        Finally

        End Try
    End Function
End Class
