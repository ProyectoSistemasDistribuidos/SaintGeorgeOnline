Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloAcademico
Imports SaintGeorgeOnline_BusinessEntities.ModuloAcademico
Namespace ModuloAcademico
    Public Class bl_ProgramacionExamen

#Region "Atributos"
        Private obj_da_ProgramacionExamen As da_ProgramacionExamen
#End Region
#Region "Constructor"
        Public Sub New()
            obj_da_ProgramacionExamen = New da_ProgramacionExamen
        End Sub
#End Region
#Region "Métodos Transaccionales"

        Public Function FUN_INS_ProgramacionExamenes( _
            ByVal obe_RegistroNotasCargo As be_RegistroNotasCargo, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ProgramacionExamen.FUN_INS_ProgramacionExamenes(obe_RegistroNotasCargo, str_Mensaje, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_ProgramacionExamenesNota( _
            ByVal obe_RegistroNotasCargo As be_RegistroNotasCargo, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ProgramacionExamen.FUN_UPD_ProgramacionExamenesNota(obe_RegistroNotasCargo, str_Mensaje, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_ProgramacionExamenes( _
            ByVal obe_RegistroNotasCargo As be_RegistroNotasCargo, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ProgramacionExamen.FUN_DEL_ProgramacionExamenes(obe_RegistroNotasCargo, str_Mensaje, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region
#Region "Método No Transaccionales"

        Public Function FUN_LIS_ProgramacionExamenes( _
        ByVal int_CodigoAnioAcademico As Integer, _
        ByVal str_CodigoAlumno As String, _
        ByVal int_CodigoGrado As Integer, _
        ByVal int_CodigoAula As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ProgramacionExamen.FUN_LIS_ProgramacionExamenes( _
                int_CodigoAnioAcademico, str_CodigoAlumno, int_CodigoGrado, int_CodigoAula, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_ProgramacionExamenes( _
        ByVal int_Tipo As Integer, ByVal int_CodigoRegistroCargo As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ProgramacionExamen.FUN_GET_ProgramacionExamenes(int_Tipo, int_CodigoRegistroCargo, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_ProgramacionExamenesPorProfesor( _
        ByVal int_CodigoAnioAcademico As Integer, _
        ByVal int_CodigoProfesor As Integer, _
        ByVal int_CodigoGrado As Integer, _
        ByVal int_CodigoAula As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ProgramacionExamen.FUN_LIS_ProgramacionExamenesPorProfesor( _
                int_CodigoAnioAcademico, int_CodigoProfesor, int_CodigoGrado, int_CodigoAula, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_DetalleProgramacionExamenes( _
        ByVal int_Tipo As Integer, _
        ByVal int_CodigoRegistroAnual As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ProgramacionExamen.FUN_LIS_DetalleProgramacionExamenes(int_Tipo, int_CodigoRegistroAnual, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region
    End Class
End Namespace