Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class bl_marca
    Public Function F_insertarMarca(ByVal obe_marca As be_marca) As List(Of String)
        Try
            Return New da_marca().F_insertarMarca(obe_marca)
        Catch ex As Exception
        Finally

        End Try
    End Function

End Class
