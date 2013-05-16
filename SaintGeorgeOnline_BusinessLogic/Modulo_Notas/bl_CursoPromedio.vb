Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas
Imports SaintGeorgeOnline_DataAccess

Public Class bl_CursoPromedio

#Region "Atributos"

    Private obj_da_ProgramacionRegistroGradosPronosticos As da_ProgramacionRegistroGradosPronosticos

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

    Public Sub New()
        obj_da_ProgramacionRegistroGradosPronosticos = New da_ProgramacionRegistroGradosPronosticos
    End Sub

#End Region

    Public Function fnListarCursoPromedio(ByVal int_AnioAcademico As Integer, ByVal int_codAlumno As Integer) As DataSet

        Try
            Return New da_CursoPromedios().fnListarCursoPromedio(int_AnioAcademico, int_codAlumno)

        Catch ex As Exception

        End Try

    End Function

End Class
