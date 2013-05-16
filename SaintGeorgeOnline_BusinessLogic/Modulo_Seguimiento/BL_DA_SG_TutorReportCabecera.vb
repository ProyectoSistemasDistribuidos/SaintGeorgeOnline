Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class BL_DA_SG_TutorReportCabecera
    Public Function F_insertarTutorReportCabecera(ByVal oBE_TutorReportCabecera As BE_TutorReportCabecera, ByVal lst As List(Of BE_TutorReportDetalle)) As Integer
        Try
            Dim oDA_SG_TutorReportCabecera As New DA_SG_TutorReportCabecera

            Return oDA_SG_TutorReportCabecera.F_insertarTutorReportCabecera(oBE_TutorReportCabecera, lst)

        Catch ex As Exception

        End Try
    End Function


End Class
