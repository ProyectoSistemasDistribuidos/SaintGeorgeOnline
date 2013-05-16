Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class DA_AF_MovimientoArticulos_Cabecera
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


#Region "transaccional "
    Public Function F_InsertarCabezeraMovimiento(ByVal oBE_AF_MovimientoArticulos_Cabecera As BE_AF_MovimientoArticulos_Cabecera, ByVal LstBE_AF_MovimientoArticulos_Detalle As List(Of BE_AF_MovimientoArticulos_Detalle)) As Integer
        Dim codigo As Integer = 0
        Dim codigoPrincipal As Integer = 0
        Dim mensajePrincipal As String = ""
        Dim mensaje As String = ""
        Try
            BeginTransaction()


            dbCommand = Me.dbBase.GetStoredProcCommand("USP_IsnAF_MovimientoArticulos_Cabecera")
            dbCommand.Parameters.Clear()
            dbBase.AddInParameter(dbCommand, "@MA_CodigoMovimientoArticulos", DbType.Int32, oBE_AF_MovimientoArticulos_Cabecera.MA_CodigoMovimientoArticulos)
            dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajadorRegistrador", DbType.Int32, oBE_AF_MovimientoArticulos_Cabecera.TJ_CodigoTrabajadorRegistrador)
            dbBase.AddInParameter(dbCommand, "@AF_CodigoTipoMovimiento", DbType.Int32, oBE_AF_MovimientoArticulos_Cabecera.AF_CodigoTipoMovimiento)
            dbBase.AddInParameter(dbCommand, "@MA_FechaRegistroMovimiento", DbType.DateTime, Now.Date)
            dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajadorAsignado", DbType.Int32, oBE_AF_MovimientoArticulos_Cabecera.TJ_CodigoTrabajadorAsignado)

            dbBase.AddInParameter(dbCommand, "@LC_CodigoSede", DbType.Int32, oBE_AF_MovimientoArticulos_Cabecera.LC_CodigoSede)

            dbBase.AddInParameter(dbCommand, "@AMB_CodigoAmbiente", DbType.Int32, oBE_AF_MovimientoArticulos_Cabecera.AMB_CodigoAmbiente)



            'LC_CodigoSede
            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)



            dbBase.ExecuteScalar(dbCommand, tran)
            codigoPrincipal = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            mensajePrincipal = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))


            For Each oDetalle As BE_AF_MovimientoArticulos_Detalle In LstBE_AF_MovimientoArticulos_Detalle
                ''
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsAF_MovimientoArticulos_Detalle")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@DMA_CodigoDetalleMovimientoArticulo", DbType.Int32, oDetalle.DMA_CodigoDetalleMovimientoArticulo)
                dbBase.AddInParameter(dbCommand, "@MA_CodigoMovimientoArticulos", DbType.Int32, codigoPrincipal)
                dbBase.AddInParameter(dbCommand, "@AT_CodigoArticulo", DbType.Int32, oDetalle.AT_CodigoArticulo)


                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)



                dbBase.ExecuteScalar(dbCommand, tran)
                codigo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))


                ''
            Next

            If codigo = 0 Or codigoPrincipal = 0 Then
                Rollback()
            Else
                Commit()
            End If
            'ALTER procedure [dbo].[USP_InsAF_MovimientoArticulos_Detalle]
            '@DMA_CodigoDetalleMovimientoArticulo int ,
            '@MA_CodigoMovimientoArticulos int ,
            '@AT_CodigoArticulo int,
            '@codigo int out ,
            '@mensaje varchar(max) out
        Catch ex As Exception
            Rollback()
        End Try

        Return codigo

    End Function
#End Region

#Region "No transaccional "

#End Region

End Class
