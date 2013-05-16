Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento

Namespace ModuloSeguimiento

    Public Class bl_ProgramacionWeekly
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_ProgramacionWeekly As da_ProgramacionWeekly

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
            obj_da_ProgramacionWeekly = New da_ProgramacionWeekly
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ProgramacionWeekly(ByVal objProgramacionPAW As be_ProgramacionWeekly, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionWeekly.FUN_INS_ProgramacionWeekly(objProgramacionPAW, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_WeeklyReport(ByVal objWeeklyReport As be_WeeklyReport, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionWeekly.FUN_INS_WeeklyReport(objWeeklyReport, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_UPD_WeeklyReport(ByVal objWeeklyReport As be_WeeklyReport, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionWeekly.FUN_UPD_WeeklyReport(objWeeklyReport, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Public Function FUN_UPD_ProgramacionWeeklyEstado(ByVal int_Codigo As Integer, ByVal int_Estado As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

        '    Return obj_da_ProgramacionWeekly.FUN_UPD_ProgramacionWeeklyEstado(int_Codigo, int_Estado, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ProgramacionWeekly(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoBimestre As Integer, _
                                                   ByVal int_CodigoCurso As Integer, ByVal int_CodigoSemana As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionWeekly.FUN_LIS_ProgramacionWeekly(int_CodigoPeriodoAcademico, int_CodigoAula, int_CodigoGrado, int_CodigoBimestre, int_CodigoCurso, int_CodigoSemana, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_WeeklyFamilia(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoAlumno As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionWeekly.FUN_LIS_WeeklyFamilia(int_CodigoPeriodoAcademico, int_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_WeeklyReportFamilia(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoSemana As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionWeekly.FUN_LIS_WeeklyReportFamilia(int_CodigoPeriodoAcademico, int_CodigoGrado, int_CodigoAula, str_CodigoAlumno, int_CodigoBimestre, int_CodigoSemana, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_WeeklyReportPorGradoAulaAlumno(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoGrado As Integer, _
          ByVal int_CodigoAula As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoSemana As Integer, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionWeekly.FUN_LIS_WeeklyReportPorGradoAulaAlumno(int_CodigoPeriodoAcademico, int_CodigoGrado, int_CodigoAula, str_CodigoAlumno, int_CodigoBimestre, int_CodigoSemana, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
#End Region
    End Class

End Namespace