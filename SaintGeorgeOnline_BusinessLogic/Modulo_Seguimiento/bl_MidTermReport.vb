Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento

Namespace ModuloSeguimiento

    Public Class bl_MidTermReport

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_MidTermReport As da_MidTermReport

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
            obj_da_MidTermReport = New da_MidTermReport
        End Sub

#End Region

#Region "Metodos Transacciones"


        Public Function FUN_INS_MidTermReport(ByVal obj_MidTermReport As be_MidTermReport, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_MidTermReport.FUN_INS_MidTermReport(obj_MidTermReport, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_MidTermReportObservacionTutor(ByVal objMidTerReport As be_MidTermReport, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_MidTermReport.FUN_UPD_MidTermReportObservacionTutor(objMidTerReport, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



        Public Function FUN_INS_ComentarioMidTermReport( _
            ByVal obj_MidTermReport As be_MidTermReport, _
            ByVal int_CodigoDetalleComentario As Integer, ByVal int_CodigoCurso As Integer, ByVal str_Comentario As String, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, _
            ByVal int_CodigoTipoUsuario As Integer, _
            ByVal int_CodigoModulo As Integer, _
            ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_MidTermReport.FUN_INS_ComentarioMidTermReport( _
                obj_MidTermReport, int_CodigoDetalleComentario, int_CodigoCurso, str_Comentario, _
                str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_MidTermReportPorCurso(ByVal int_CodigoAula As Integer, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoTipoDocumento As Integer, ByVal int_CodigoProgramacion As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MidTermReport.FUN_LIS_MidTermReportPorCurso(int_CodigoAula, int_CodigoPeriodoAcademico, int_CodigoGrado, int_CodigoTipoDocumento, int_CodigoProgramacion, int_CodigoCurso, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_MidTermReportPorAlumno(ByVal int_CodigoAula As Integer, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoTipoDocumento As Integer, ByVal int_CodigoProgramacion As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MidTermReport.FUN_LIS_MidTermReportPorAlumno(int_CodigoAula, int_CodigoPeriodoAcademico, int_CodigoGrado, int_CodigoTipoDocumento, int_CodigoProgramacion, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_MidTermReportPorGradoAulaAlumno(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoAula As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoTipoDocumento As Integer, ByVal int_CodigoProgramacion As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MidTermReport.FUN_LIS_MidTermReportPorGradoAulaAlumno(int_CodigoPeriodoAcademico, int_CodigoGrado, int_CodigoAula, str_CodigoAlumno, int_CodigoTipoDocumento, int_CodigoProgramacion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ObservacionMidTermReportPorCurso( _
            ByVal str_CodigoAlumno As String, ByVal int_CodigoProgramacion As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoCurso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MidTermReport.FUN_LIS_ObservacionMidTermReportPorCurso(str_CodigoAlumno, int_CodigoProgramacion, int_CodigoAula, int_CodigoCurso, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

    End Class

End Namespace
