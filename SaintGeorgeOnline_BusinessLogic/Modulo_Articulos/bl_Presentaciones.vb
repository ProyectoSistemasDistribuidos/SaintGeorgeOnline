Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class bl_Presentaciones
    Public Function F_insertarPresentaciones(ByVal obe_Presentaciones As be_Presentaciones) As List(Of String)

        Try

            Return New da_Presentaciones().F_insertarPresentaciones(obe_Presentaciones)
        Catch ex As Exception
        Finally

        End Try
    End Function

    ''eliminar articulo
    Public Function F_EliminarArticulo(ByVal codArticulo As Integer) As List(Of String)

        Try

            Return New da_Presentaciones().F_EliminarArticulo(codArticulo)
        Catch ex As Exception
        Finally

        End Try
    End Function


End Class
