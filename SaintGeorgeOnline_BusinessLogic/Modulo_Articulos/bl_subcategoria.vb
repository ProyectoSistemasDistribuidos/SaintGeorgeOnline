Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess
Public Class bl_subcategoria
    Public Function F_insertarSubcategoria(ByVal oBE_Subcategoria As BE_Subcategoria) As List(Of String)

        Try
            Return New da_subcategoria().F_insertarSubcategoria(oBE_Subcategoria)
        Catch ex As Exception

        End Try

    End Function
End Class
