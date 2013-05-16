Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess

Public Class BL_StuacionMatricula
    Public Function F_funcionActualizarSituacionMAtricula(ByVal dc As Dictionary(Of Integer, Integer))
        Try

            Return New DA_StuacionMatricula().F_funcionActualizarSituacionMAtricula(dc)

        Catch ex As Exception

        End Try
    End Function


End Class
