Imports SaintGeorgeOnline_BusinessEntities
Imports SaintGeorgeOnline_DataAccess

Public Class BL_CrearEstructuraNotasOficiales
    Function insertarAsignacionGrupo(ByVal dtListas As DataTable, ByVal codBimestre As Integer, ByVal codAula As Integer, ByVal codAnio As Integer) As Integer

        Try
            Dim oDA_CrearEstructuraNotasOficiales As New DA_CrearEstructuraNotasOficiales


            Return oDA_CrearEstructuraNotasOficiales.insertarAsignacionGrupo(dtListas, codBimestre, codAula, codAnio)
        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Function FInsertarMatrizGradoSexotGenerarNotasFinal(ByVal dtMatriz As DataTable, ByVal codBimestre As Integer, ByVal codAnioAcademico As Integer) As Boolean
        Try
            Return New DA_CrearEstructuraNotasOficiales().FInsertarMatrizGradoSexotGenerarNotasFinal(dtMatriz, codBimestre, codAnioAcademico)
        Catch ex As Exception

        End Try

    End Function

    Public Function insertarAsignacionGrupoConductaPrimaria(ByVal dtListas As DataTable, ByVal codAnio As Integer) As Integer
        Try
            Return New DA_CrearEstructuraNotasOficiales().insertarAsignacionGrupoConductaPrimaria(dtListas, codAnio)
        Catch ex As Exception

        End Try

    End Function

    Public Function insertarAsignacionGrupoConductaSecundaria(ByVal dtListas As DataTable, ByVal codAnio As Integer) As Integer
        Try
            Return New DA_CrearEstructuraNotasOficiales().insertarAsignacionGrupoConductaSecundaria(dtListas, codAnio)
        Catch ex As Exception

        End Try

    End Function

    Public Function insertarAsignacionGrupoConductaSecundariaSexto(ByVal dtListas As DataTable, ByVal codAnio As Integer) As Integer
        Try
            Return New DA_CrearEstructuraNotasOficiales().insertarAsignacionGrupoConductaSecundariaSexto(dtListas, codAnio)
        Catch ex As Exception

        End Try

    End Function
    ''
    ''
    Public Function f_actualizarNotaFinalPrimaria(ByVal dtNotaFinal As DataTable)
        Try
            Return New DA_CrearEstructuraNotasOficiales().f_actualizarNotaFinalPrimaria(dtNotaFinal)

        Catch ex As Exception

        End Try
    End Function

End Class