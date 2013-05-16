Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess
Public Class bl_categoria
    Public Function F_insertarCategoria(ByVal oBE_categoria As BE_categoria) As List(Of String)
        Try
            Return New da_categoria().F_insertarCategoria(oBE_categoria)
        Catch ex As Exception

        End Try
    End Function
End Class
