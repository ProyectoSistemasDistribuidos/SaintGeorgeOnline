Imports SaintGeorgeOnline_DataAccess

Public Class BL_entrevistaComentario
    Public Function FInsertarComentarioEntrevista(ByVal lst As List(Of Dictionary(Of String, Object)))
        Try
            Return New DA_entrevistaComentario().FInsertarComentarioEntrevista(lst)
        Catch ex As Exception

        End Try
    End Function

End Class
