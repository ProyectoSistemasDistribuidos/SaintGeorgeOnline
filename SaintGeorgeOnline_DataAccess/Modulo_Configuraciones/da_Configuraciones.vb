Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Public Class da_Configuraciones
    Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

    Private dbBase As SqlDatabase 'ExecuteDataSet
    Private dbCommand As DbCommand 'ExecuteScalar

#End Region

#Region "Constructor"

    Public Sub New()
        dbBase = New SqlDatabase(Me.SqlConexionDB)
    End Sub

#End Region




#Region "transaccional"

#End Region

#Region "no transaccional"

    Public Function Lis_clasesPresupuestales(ByVal int_codigoTrabajador As Integer, ByVal int_Periodo As Integer, ByVal intCodigoSede As Integer, ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer) As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("listarClasePresupuesto")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, int_codigoTrabajador)
                dbBase.AddInParameter(cmd, "@PP_CodigoPeriodo", DbType.Int32, int_Periodo)
                dbBase.AddInParameter(cmd, "@SD_CodigoSede", DbType.Int32, intCodigoSede)
                dbBase.AddInParameter(cmd, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int32, ASSSCC_CodigoAsignacionSSSCentroCosto)

                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using


        Catch ex As Exception
        Finally

        End Try
    End Function
#End Region


End Class
