Imports SaintGeorgeOnline_DataAccess

Public Class bl_da_admision

    Public Function FUN_reporteAlumnosBDSaint(ByVal codAnio As Integer) As DataSet
        Try
            Return New da_admision().FUN_reporteAlumnosBDSaint(codAnio)
        Catch ex As Exception

        End Try
    End Function

End Class
