Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_matrizAsignacionCriterios
#Region "Atributos"

    Private str_Mensaje As String

#Region "no transaccional"
    Public Function FUN_LIS_MidTermReportPorCurso(ByVal codBimestre As Integer, ByVal int_codigoAnioAcademico As Integer, ByVal int_codAsignacionGrupo As Integer, ByVal codTipoDocumentoSeguimiento As Integer) As DataSet

        Try
            Return New da_matrizAsignacionCriterios().FUN_LIS_matrizAsignacionCriterios(codBimestre, int_codigoAnioAcademico, int_codAsignacionGrupo, codTipoDocumentoSeguimiento)


        Catch ex As Exception
        Finally

        End Try
    End Function
    Public Function FUN_LIS_matrizCabezeraDetalle(ByVal codBimestre As Integer, ByVal int_codAsignacionGrupo As Integer, ByVal int_tipoDocumento As Integer) As DataSet
        Try
            Return New da_matrizAsignacionCriterios().FUN_LIS_matrizCabezeraDetalle(codBimestre, int_codAsignacionGrupo, int_tipoDocumento)


        Catch ex As Exception
        Finally

        End Try
    End Function



#End Region



#Region "transaccional"

    Function insercionCascada(ByVal ocontexto As contexto, ByVal codBimestre As Integer, ByVal codAsignacionGrupo As Integer, ByVal codPerm As Integer, ByVal codEffort As Integer, ByVal courseCovered As String) As List(Of String)

        Try
            Return New da_matrizAsignacionCriterios().insercionCascada(ocontexto, codBimestre, codAsignacionGrupo, codPerm, codEffort, courseCovered)

        Catch ex As Exception
        Finally

        End Try

    End Function

    Function fActualizacionCazcada(ByVal ocontexto As contexto, ByVal codPerform As Integer, ByVal codEffort As Integer, ByVal courseCovered As Integer, ByVal codCAbezera As Integer, ByVal str_dif As String, ByVal str_rec As String, ByVal str_arc As String) As List(Of Integer)
        Try
            Return New da_matrizAsignacionCriterios().fActualizacionCazcada(ocontexto, codPerform, codEffort, courseCovered, codCAbezera, str_dif, str_rec, str_arc)

        Catch ex As Exception

        End Try


    End Function



#End Region

#End Region
End Class
