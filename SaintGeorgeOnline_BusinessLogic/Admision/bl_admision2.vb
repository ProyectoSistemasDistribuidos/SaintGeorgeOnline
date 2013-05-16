Imports SaintGeorgeOnline_DataAccess

Public Class bl_admision2


    Public Function FUN_reporteAlumnosBDTesoreria(ByVal codAnio As Integer, ByVal codGrado As Integer) As DataSet
        Try
            Return New da_admision2().FUN_reporteAlumnosBDTesoreria(codAnio, codGrado)
        Catch ex As Exception

        End Try
    End Function

End Class
