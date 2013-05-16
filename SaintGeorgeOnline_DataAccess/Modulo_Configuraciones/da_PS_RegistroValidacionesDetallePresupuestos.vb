Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessEntities

Public Class da_PS_RegistroValidacionesDetallePresupuestos
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

#Region "Metodos Transaccionales"

    Public Function FinsercionPS_RegistroValidacionesDetallePresupuestos(ByVal lstBE_PS_RegistroValidacionesDetallePresupuestos As List(Of BE_PS_RegistroValidacionesDetallePresupuestos)) As List(Of Integer)
        Try
            BeginTransaction()
            dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsPS_RegistroValidacionesDetallePresupuestos")

            Dim lstId As New List(Of Integer)

            For Each oBE_PS_RegistroValidacionesDetallePresupuestos As BE_PS_RegistroValidacionesDetallePresupuestos In lstBE_PS_RegistroValidacionesDetallePresupuestos
                'Parámetros de entrada
                dbCommand.Parameters.Clear()


                dbBase.AddInParameter(dbCommand, "@RVDP_CodigoRegistroValidacionesDetalle", DbType.Int32, oBE_PS_RegistroValidacionesDetallePresupuestos.RVDP_CodigoRegistroValidacionesDetalle)
                dbBase.AddInParameter(dbCommand, "@RVP_CodigoRegistroValidacionesPresupuesto", DbType.Int16, oBE_PS_RegistroValidacionesDetallePresupuestos.RVP_CodigoRegistroValidacionesPresupuesto)
                dbBase.AddInParameter(dbCommand, "@DSPA_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int16, oBE_PS_RegistroValidacionesDetallePresupuestos.DSPA_CodigoDetalleSolicitudPresupuestoArticulo)
                dbBase.AddInParameter(dbCommand, "@RVDP_Observacion", DbType.String, oBE_PS_RegistroValidacionesDetallePresupuestos.RVDP_Observacion)
                dbBase.AddInParameter(dbCommand, "@RVDP_Estado", DbType.Int16, oBE_PS_RegistroValidacionesDetallePresupuestos.RVDP_Estado)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
                dbBase.ExecuteScalar(dbCommand, tran)
                Dim int_ValorDetalleArticulo As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstId.Add(int_ValorDetalleArticulo)
                dbCommand.Parameters.Clear()
            Next

            Commit()
            Return lstId
            'Ejecucion del Store Procedure

        Catch ex As Exception
            Rollback()
        End Try
    End Function
#End Region

End Class
