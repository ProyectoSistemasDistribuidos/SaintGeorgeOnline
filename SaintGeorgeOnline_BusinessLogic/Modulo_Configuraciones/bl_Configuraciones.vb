Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess

Public Class bl_Configuraciones
#Region "Atributos"

    Private str_Mensaje As String
    Private obj_da_Piso As da_CentrosCostos

#End Region

#Region "Propiedades"



#End Region

#Region "transaccional"

#End Region
#Region "no transaccional"
    Public Function Lis_clasesPresupuestales(ByVal int_codigoTrabajador As Integer, ByVal int_Periodo As Integer, ByVal intCodigoSede As Integer, ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer) As DataTable
        Try
            Dim oda_Configuraciones As New da_Configuraciones
            Return New da_Configuraciones().Lis_clasesPresupuestales(int_codigoTrabajador, int_Periodo, intCodigoSede, ASSSCC_CodigoAsignacionSSSCentroCosto)

        Catch ex As Exception

        End Try
    End Function

#End Region

End Class
