Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess

Public Class bl_Seguimiento_Rep
    Public Function FUN_REP_Seguimiento(ByVal int_codigoAsignacionAula As Integer, ByVal int_codigoCurso As Integer, ByVal int_bimestre As Integer) As DataSet
        Try
            Return New da_Seguimiento_Rep().FUN_REP_Seguimiento(int_codigoAsignacionAula, int_codigoCurso, int_bimestre)
        Catch ex As Exception
        Finally

        End Try

    End Function
    Function fRepClassSumaryReport(ByVal int_codAsignacionAula As Integer, ByVal cod_asignacionGrupo As Integer, ByVal codTipoDocumento As Integer, ByVal codBimestre As Integer) As DataSet
        Try
            Return New da_Seguimiento_Rep().fRepClassSumaryReport(int_codAsignacionAula, cod_asignacionGrupo, codTipoDocumento, codBimestre)
        Catch ex As Exception
        Finally

        End Try
    End Function

End Class
