Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class bl_color

    Public Function F_insertarColor(ByVal obe_color As be_color)
        Try

            Return New da_color().F_insertarColor(obe_color)
        Catch ex As Exception
        Finally

        End Try
    End Function

End Class
