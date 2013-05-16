Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos
Imports SaintGeorgeOnline_DataAccess.ModuloCursos

Namespace ModuloCursos

    Public Class bl_AsignacionAlumnosTalleres

#Region "Atributos"

        Private obj_da_AsignacionAlumnosTalleres As da_AsignacionAlumnosTalleres

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_AsignacionAlumnosTalleres = New da_AsignacionAlumnosTalleres

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionAlumnosTalleres(ByVal str_CodigoAlumno As String, ByVal int_CodigoAsignacionTallerBimestre As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionAlumnosTalleres.FUN_INS_AsignacionAlumnosTalleres(str_CodigoAlumno, int_CodigoAsignacionTallerBimestre, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        'Public Function FUN_LIS_AsignacionAlumnosTalleres(ByVal int_CodigoAnioAcademico As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Return obj_da_AsignacionAlumnosTalleres.FUN_LIS_AsignacionAlumnosTalleres(int_CodigoAnioAcademico, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

#End Region

    End Class

End Namespace