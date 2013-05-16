Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessEntities

Public Class Da_AsignarTrabajadoresCentroCosto

    Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

    Private dbBase As SqlDatabase 'ExecuteDataSet
    Private dbCommand As DbCommand 'ExecuteScalar

    Private cnn As DbConnection
    Private tran As DbTransaction

#End Region

#Region "Constructor"

    Public Sub New()
        dbBase = New SqlDatabase(Me.SqlConexionDB)
        cnn = Me.dbBase.CreateConnection()
    End Sub

#End Region

#Region "Propiedades"

    Public ReadOnly Property BaseDatos() As SqlDatabase
        Get
            Return Me.dbBase
        End Get
    End Property

    Public ReadOnly Property Transaccion() As DbTransaction
        Get
            Return Me.tran
        End Get
    End Property

    Public ReadOnly Property Conexion() As DbConnection
        Get
            Return Me.cnn
        End Get
    End Property

#End Region

#Region "Metodos"

    Public Sub BeginTransaction()

        If Not (cnn.State = ConnectionState.Open) Then
            cnn.Open()
        End If

        tran = cnn.BeginTransaction(IsolationLevel.Serializable)

    End Sub

    Public Sub Rollback()

        tran.Rollback()

    End Sub

    Public Sub Commit()

        tran.Commit()

    End Sub

#End Region

#Region "transaccional"
    Public Function LstarEstructuraTrabajadorSubSubSubCentroCosto(ByVal PP_CodigoPeriodo As Integer, ByVal SD_CodigoSede As Integer) As DataSet
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("Usp_LisEsquemaPresupuestos")
                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@PP_CodigoPeriodo", DbType.Int32, PP_CodigoPeriodo)
                dbBase.AddInParameter(cmd, "@SD_CodigoSede", DbType.String, SD_CodigoSede)

                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)



                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)

            End Using
        Catch ex As Exception

        End Try
    End Function


    Public Function LstarEstructuraSubSubSubSubAprobador(ByVal PP_CodigoPeriodo As Integer, ByVal SD_CodigoSede As Integer, ByVal ACSD_CodigoAsignacionCentroCostoSede As Integer) As DataSet
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("ListarTrabajadoresSSSCC")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@PP_CodigoPeriodo", DbType.Int32, PP_CodigoPeriodo)
                dbBase.AddInParameter(cmd, "@SD_CodigoSede", DbType.String, SD_CodigoSede)
                dbBase.AddInParameter(cmd, "@ACSD_CodigoAsignacionCentroCostoSede", DbType.String, ACSD_CodigoAsignacionCentroCostoSede)
                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)



                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)

            End Using
        Catch ex As Exception

        End Try
    End Function


    Function insertarValidadoresTrabajador(ByVal lstBE_PS_AsignacionResponsablesValidaciones As List(Of BE_PS_AsignacionResponsablesValidaciones)) As List(Of Integer)
        Dim codigo As Integer = 0
        Dim mensaje As String = ""
        Dim lstIds As New List(Of Integer)
        Dim cmd As DbCommand
        Try
            BeginTransaction()
            For Each oBE_PS_AsignacionResponsablesValidaciones As BE_PS_AsignacionResponsablesValidaciones In lstBE_PS_AsignacionResponsablesValidaciones
                If oBE_PS_AsignacionResponsablesValidaciones.ARVP_CodigoResponsableValidarPresupuesto = 0 Then
                    cmd = dbBase.GetStoredProcCommand("USP_insertarAsignacionValidador")
                Else
                    cmd = dbBase.GetStoredProcCommand("USP_Udp_AsignacionValidador")
                End If
                dbBase.AddInParameter(cmd, "@ARVP_CodigoResponsableValidarPresupuesto", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.ARVP_CodigoResponsableValidarPresupuesto)
                dbBase.AddInParameter(cmd, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.ASSSCC_CodigoAsignacionSSSCentroCosto)
                dbBase.AddInParameter(cmd, "@TJ_CodigoTrabajador", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.TJ_CodigoTrabajador)
                dbBase.AddInParameter(cmd, "@TV_CodigoTipoValidacion", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.TV_CodigoTipoValidacion)
                dbBase.AddInParameter(cmd, "@ARVP_Estado", DbType.Boolean, oBE_PS_AsignacionResponsablesValidaciones.ARVP_Estado)
                dbBase.AddInParameter(cmd, "@ARVP_OrdenValidacion", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.ARVP_OrdenValidacion)
                dbBase.AddInParameter(cmd, "@ARVP_Activo", DbType.Boolean, oBE_PS_AsignacionResponsablesValidaciones.ARVP_Activo)
                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int16, 10)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 255)
                codigo = dbBase.ExecuteScalar(cmd, tran)
                mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                codigo = dbBase.GetParameterValue(cmd, "@codigo").ToString()
                lstIds.Add(codigo)
            Next
            Commit()
            Return lstIds
        Catch ex As Exception
            Rollback()
        Finally

        End Try
    End Function

    Function FeliminarResponsable(ByVal oBE_PS_AsignacionResponsablesValidaciones As BE_PS_AsignacionResponsablesValidaciones) As List(Of Integer)
        Dim codigo As Integer = 0
        Dim mensaje As String = ""
        Dim lstIds As New List(Of Integer)
        Dim cmd As DbCommand
        Try
            BeginTransaction()
            ' For Each oBE_PS_AsignacionResponsablesValidaciones As BE_PS_AsignacionResponsablesValidaciones In lstBE_PS_AsignacionResponsablesValidaciones
            If oBE_PS_AsignacionResponsablesValidaciones.ARVP_CodigoResponsableValidarPresupuesto = 0 Then
                cmd = dbBase.GetStoredProcCommand("USP_insertarAsignacionValidador")
            Else
                cmd = dbBase.GetStoredProcCommand("USP_Udp_AsignacionValidador")
            End If
            dbBase.AddInParameter(cmd, "@ARVP_CodigoResponsableValidarPresupuesto", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.ARVP_CodigoResponsableValidarPresupuesto)
            dbBase.AddInParameter(cmd, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.ASSSCC_CodigoAsignacionSSSCentroCosto)
            dbBase.AddInParameter(cmd, "@TJ_CodigoTrabajador", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.TJ_CodigoTrabajador)
            dbBase.AddInParameter(cmd, "@TV_CodigoTipoValidacion", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.TV_CodigoTipoValidacion)
            dbBase.AddInParameter(cmd, "@ARVP_Estado", DbType.Boolean, oBE_PS_AsignacionResponsablesValidaciones.ARVP_Estado)
            dbBase.AddInParameter(cmd, "@ARVP_OrdenValidacion", DbType.Int16, oBE_PS_AsignacionResponsablesValidaciones.ARVP_OrdenValidacion)
            dbBase.AddInParameter(cmd, "@ARVP_Activo", DbType.Boolean, oBE_PS_AsignacionResponsablesValidaciones.ARVP_Activo)
            dbBase.AddOutParameter(cmd, "@codigo", DbType.Int16, 10)
            dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 255)
            codigo = dbBase.ExecuteScalar(cmd, tran)
            mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
            codigo = dbBase.GetParameterValue(cmd, "@codigo").ToString()
            lstIds.Add(codigo)
            '  Next
            Commit()
            Return lstIds
        Catch ex As Exception
            Rollback()
        Finally

        End Try
    End Function


#End Region
#Region "no transaccional"

#End Region
End Class
