Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessEntities

Public Class DA_ActualizarDetaalleSolicitudArticuloPrecioCantidad
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
    Public Function fActualizarPrecioCantidadSolicitudArticulo(ByVal lstSolicitudArticulo As List(Of BE_ActualizarDetaalleSolicitudArticuloPrecioCantidad)) As List(Of Integer)
        Dim codigo As Integer = 0
        Dim mensaje As String = ""
        Dim lstCodigos As New List(Of Integer)
        Try
            BeginTransaction()

            For Each oBE_ActualizarDetaalleSolicitudArticuloPrecioCantidad As BE_ActualizarDetaalleSolicitudArticuloPrecioCantidad In lstSolicitudArticulo

                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UDP_ActualizarValidacionDetalleSolicitudArticulo")

                dbBase.AddInParameter(dbCommand, "@DSPA_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int16, oBE_ActualizarDetaalleSolicitudArticuloPrecioCantidad.DSPA_CodigoDetalleSolicitudPresupuestoArticulo)
                dbBase.AddInParameter(dbCommand, "@DSPA_Cantidad", DbType.Int16, oBE_ActualizarDetaalleSolicitudArticuloPrecioCantidad.DSPA_Cantidad)
                dbBase.AddInParameter(dbCommand, "@DSPA_Precio", DbType.Decimal, oBE_ActualizarDetaalleSolicitudArticuloPrecioCantidad.DSPA_Precio)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                mensaje = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
                codigo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))


                lstCodigos.Add(codigo)
            Next

            Commit()
            Return lstCodigos

        Catch ex As Exception
            Rollback()
        End Try

    End Function
#End Region


End Class
