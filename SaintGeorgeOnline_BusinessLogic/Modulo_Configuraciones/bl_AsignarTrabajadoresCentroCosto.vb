Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_AsignarTrabajadoresCentroCosto


#Region "transaccinal"
    Function insertarValidadoresTrabajador(ByVal lstBE_PS_AsignacionResponsablesValidaciones As List(Of BE_PS_AsignacionResponsablesValidaciones)) As List(Of Integer)
        Dim codigo As Integer = 0
        Dim mensaje As String = ""

        Try
            Return New Da_AsignarTrabajadoresCentroCosto().insertarValidadoresTrabajador(lstBE_PS_AsignacionResponsablesValidaciones)
        Catch ex As Exception

        End Try

    End Function

#End Region
#Region "no transaccinoal"
    Public Function LstarEstructuraTrabajadorSubSubSubCentroCosto(ByVal PP_CodigoPeriodo As Integer, ByVal SD_CodigoSede As Integer) As DataSet
        Try

            Return New Da_AsignarTrabajadoresCentroCosto().LstarEstructuraTrabajadorSubSubSubCentroCosto(PP_CodigoPeriodo, SD_CodigoSede)

        Catch ex As Exception

        End Try
    End Function

    Public Function LstarEstructuraSubSubSubSubAprobador(ByVal PP_CodigoPeriodo As Integer, ByVal SD_CodigoSede As Integer, ByVal ACSD_CodigoAsignacionCentroCostoSede As Integer) As DataSet
        Try

            Return New Da_AsignarTrabajadoresCentroCosto().LstarEstructuraSubSubSubSubAprobador(PP_CodigoPeriodo, SD_CodigoSede, ACSD_CodigoAsignacionCentroCostoSede)

        Catch ex As Exception

        End Try
    End Function
    Function FeliminarResponsable(ByVal oBE_PS_AsignacionResponsablesValidaciones As BE_PS_AsignacionResponsablesValidaciones) As List(Of Integer)
        Try
            Return New Da_AsignarTrabajadoresCentroCosto().FeliminarResponsable(oBE_PS_AsignacionResponsablesValidaciones)
        Catch ex As Exception
        Finally

        End Try
    End Function


#End Region
End Class
