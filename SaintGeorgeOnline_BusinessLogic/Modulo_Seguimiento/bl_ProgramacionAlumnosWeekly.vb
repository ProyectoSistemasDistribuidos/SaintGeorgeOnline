Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento

Namespace ModuloSeguimiento

    Public Class bl_ProgramacionAlumnosWeekly
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_ProgramacionAlumnosWeekly As da_ProgramacionAlumnosWeekly

#End Region

#Region "Propiedades"

        Public ReadOnly Property Mensaje() As String
            Get
                Return str_Mensaje
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_ProgramacionAlumnosWeekly = New da_ProgramacionAlumnosWeekly
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ProgramacionAlumnosWeekly(ByVal objProgramacionPAW As be_ProgramacionAlumnosWeekly, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionAlumnosWeekly.FUN_INS_ProgramacionAlumnosWeekly(objProgramacionPAW, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_ProgramacionAlumnosWeekly(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionAlumnosWeekly.FUN_DEL_ProgramacionAlumnosWeekly(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Public Function FUN_UPD_ProgramacionAlumnosWeeklyEstado(ByVal int_Codigo As Integer, ByVal int_Estado As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

        '    Return obj_da_ProgramacionAlumnosWeekly.FUN_UPD_ProgramacionAlumnosWeeklyEstado(int_Codigo, int_Estado, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_ProgramacionAlumnosWeekly(ByVal int_CodigoProgramacionWeekly As Integer, ByVal int_CodigoSemanaAcademica As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionAlumnosWeekly.FUN_GET_ProgramacionAlumnosWeekly(int_CodigoProgramacionWeekly, int_CodigoSemanaAcademica, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ProgramacionAlumnosWeeklyPorBimestre(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionAlumnosWeekly.FUN_LIS_ProgramacionAlumnosWeeklyPorBimestre(int_CodigoPeriodoAcademico, int_CodigoAula, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class
End Namespace