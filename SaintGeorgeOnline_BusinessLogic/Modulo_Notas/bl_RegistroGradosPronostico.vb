Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroGradosPronostico


#Region "Atributos"

        Private obj_da_RegistroGradosPronostico As da_RegistroGradosPronostico

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_RegistroGradosPronostico = New da_RegistroGradosPronostico
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RegistroGradosPronostico(ByVal objRegistroGradosPronostico As be_RegistroGradosPronostico, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_RegistroGradosPronostico.FUN_INS_RegistroGradosPronostico(objRegistroGradosPronostico, str_Mensaje, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_GruposIBPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoProfesor As Integer, _
            ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoProgramacionGrupo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RegistroGradosPronostico.FUN_LIS_GruposIBPorAlumno(str_CodigoAlumno, int_CodigoProfesor, int_CodigoAnioAcademico, int_CodigoProgramacionGrupo, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

    End Class

End Namespace