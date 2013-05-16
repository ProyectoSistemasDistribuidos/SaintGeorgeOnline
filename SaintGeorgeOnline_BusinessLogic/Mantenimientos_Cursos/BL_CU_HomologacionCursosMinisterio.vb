Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess


Public Class BL_CU_HomologacionCursosMinisterio
    Public Function F_InsertarCU_HomologacionCursosMinisterio(ByVal oCU_HomologacionCursosMinisterio As BE_CU_HomologacionCursosMinisterio) As List(Of String)

        Try
            Return New DA_CU_HomologacionCursosMinisterio().F_InsertarCU_HomologacionCursosMinisterio(oCU_HomologacionCursosMinisterio)
        Catch ex As Exception

        End Try
    End Function

End Class
