Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_tutorReport
#Region "no transaccional"

    Public Function FUN_REP_SeguimientoTutorReport(ByVal codAsignacionAula As Integer, ByVal codTipoCocumento As Integer, ByVal codBimestre As Integer) As DataSet
        Try
            Return New da_tutorReport().FUN_REP_SeguimientoTutorReport(codAsignacionAula, codTipoCocumento, codBimestre)
        Catch ex As Exception
        End Try
    End Function
    Function insercionCascadaTutorReport(ByVal lstcriterioTutorReport As List(Of criterioTutorReport), ByVal codBimestre As Integer, ByVal codAsignacionAula As Integer) As List(Of Integer)

        Try
            Return New da_tutorReport().insercionCascadaTutorReport(lstcriterioTutorReport, codBimestre, codAsignacionAula)
        Catch ex As Exception


        End Try
    End Function

    Public Function FUN_REP_SeguimientoTutorReportDatos(ByVal codAsignacionAula As Integer, ByVal codBimestre As Integer) As DataSet
        Try
            Return New da_tutorReport().FUN_REP_SeguimientoTutorReportDatos(codAsignacionAula, codBimestre)
        Catch ex As Exception

        End Try
    End Function

    Function fActualizacionCazcadaTutorReport(ByVal lstReportDetalle As List(Of TutorReportDetalle), ByVal codTutorReportCabezera As Integer, ByVal codGradeRendimiento As Integer, ByVal codActitud As Integer, ByVal codEsfuerzo As Integer) As List(Of Integer)

        Try

            Return New da_tutorReport().fActualizacionCazcadaTutorReport(lstReportDetalle, codTutorReportCabezera, codGradeRendimiento, codActitud, codEsfuerzo)

        Catch ex As Exception



        End Try
    End Function

    Public Function FUN_REP_SeguimientoTutorReportDatosExcel(ByVal codAsignacionAula As Integer, ByVal codBimestre As Integer) As DataSet
        Try
            Return New da_tutorReport().FUN_REP_SeguimientoTutorReportDatosExcel(codAsignacionAula, codBimestre)

        Catch ex As Exception

        End Try
    End Function

#End Region
End Class
