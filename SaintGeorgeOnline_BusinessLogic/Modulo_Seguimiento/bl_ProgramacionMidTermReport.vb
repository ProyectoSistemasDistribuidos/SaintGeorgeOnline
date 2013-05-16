Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento

Namespace ModuloSeguimiento

    Public Class bl_ProgramacionMidTermReport

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_ProgramacionMidTermReport As da_ProgramacionMidTermReport

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
            obj_da_ProgramacionMidTermReport = New da_ProgramacionMidTermReport
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ProgramacionMidTermReport(ByVal objProgramacionMTR As be_ProgramacionMidTermReport, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionMidTermReport.FUN_INS_ProgramacionMidTermReport(objProgramacionMTR, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_ProgramacionMidTermReport(ByVal objProgramacionMTR As be_ProgramacionMidTermReport, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionMidTermReport.FUN_UPD_ProgramacionMidTermReport(objProgramacionMTR, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_ProgramacionMidTermReportEstado(ByVal int_Codigo As Integer, ByVal int_Estado As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionMidTermReport.FUN_UPD_ProgramacionMidTermReportEstado(int_Codigo, int_Estado, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ProgramacionMidTermReport(ByVal int_CodigoPeriodoAcademico As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionMidTermReport.FUN_LIS_ProgramacionMidTermReport(int_CodigoPeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_MidTermReportFamilia(ByVal int_CodigoPeriodoAcademico As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoGrado As Integer, ByVal int_CodigoSeccion As Integer, ByVal int_CodigoProgramacion As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionMidTermReport.FUN_LIS_MidTermReportFamilia(int_CodigoPeriodoAcademico, str_CodigoAlumno, int_CodigoGrado, int_CodigoSeccion, int_CodigoProgramacion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_MidTermFamilia(ByVal int_CodigoPeriodoAcademico As Integer, ByVal str_CodigoAlumno As String,  ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionMidTermReport.FUN_LIS_MidTermFamilia(int_CodigoPeriodoAcademico, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ProgramacionMidTermReportPlantilla(ByVal int_CodigoPeriodoAcademico As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionMidTermReport.FUN_LIS_ProgramacionMidTermReportPlantilla(int_CodigoPeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
