Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessEntities

Public Class da_AsignacionClasesPresupuesto_gsv
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
    Public Function insertarEstructuraCentro(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal lstbe_PS_AsignacionSSSCentroCostoSubCategoria As List(Of be_PS_AsignacionSSSCentroCostoSubCategoria))
        Dim codigo As Integer = 0
        Dim mensaje As String = ""

        Try
            BeginTransaction()
            Dim oBE_PS_AsignacionEstructuraSSSCentroCostoClases As BE_PS_AsignacionEstructuraSSSCentroCostoClases
            Dim obe_PS_AsignacionSSSCentroCostoCategoria As be_PS_AsignacionSSSCentroCostoCategoria


            For Each obe_PS_AsignacionSSSCentroCostoSubCategoria As be_PS_AsignacionSSSCentroCostoSubCategoria In lstbe_PS_AsignacionSSSCentroCostoSubCategoria
                oBE_PS_AsignacionEstructuraSSSCentroCostoClases = New BE_PS_AsignacionEstructuraSSSCentroCostoClases

                oBE_PS_AsignacionEstructuraSSSCentroCostoClases.ASP_CodigoEstructuraSubCategoria = obe_PS_AsignacionSSSCentroCostoSubCategoria.ASP_CodigoEstructuraSubCategoria
                oBE_PS_AsignacionEstructuraSSSCentroCostoClases.ASSSCC_CodigoAsignacionSSSCentroCosto = ASSSCC_CodigoAsignacionSSSCentroCosto

                dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsPS_AsignacionEstructuraSSSCentroCostoClases")
                dbBase.AddInParameter(dbCommand, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int16, oBE_PS_AsignacionEstructuraSSSCentroCostoClases.ASSSCC_CodigoAsignacionSSSCentroCosto)
                dbBase.AddInParameter(dbCommand, "@ASP_CodigoEstructuraSubCategoria", DbType.Int16, oBE_PS_AsignacionEstructuraSSSCentroCostoClases.ASP_CodigoEstructuraSubCategoria)
                dbBase.AddInParameter(dbCommand, "@ASL_CodigoSSSCentroCostoClase", DbType.Int16, oBE_PS_AsignacionEstructuraSSSCentroCostoClases.ASL_CodigoSSSCentroCostoClase)


                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                mensaje = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString
                codigo = dbBase.GetParameterValue(dbCommand, "@codigo").ToString

                obe_PS_AsignacionSSSCentroCostoCategoria = New be_PS_AsignacionSSSCentroCostoCategoria

                obe_PS_AsignacionSSSCentroCostoCategoria.ASL_CodigoSSSCentroCostoClase = codigo
                obe_PS_AsignacionSSSCentroCostoCategoria.ASP_CodigoEstructuraSubCategoria = obe_PS_AsignacionSSSCentroCostoSubCategoria.ASP_CodigoEstructuraSubCategoria


                dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsPS_AsignacionSSSCentroCostoCategoria")

                dbBase.AddInParameter(dbCommand, "@ASC_CodigoSSCentroCostoCategoria", DbType.Int16, obe_PS_AsignacionSSSCentroCostoCategoria.ASC_CodigoSSCentroCostoCategoria)
                dbBase.AddInParameter(dbCommand, "@ASL_CodigoSSSCentroCostoClase", DbType.Int16, obe_PS_AsignacionSSSCentroCostoCategoria.ASL_CodigoSSSCentroCostoClase)
                dbBase.AddInParameter(dbCommand, "@ASP_CodigoEstructuraSubCategoria", DbType.Int16, obe_PS_AsignacionSSSCentroCostoSubCategoria.ASP_CodigoEstructuraSubCategoria)


                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)


                mensaje = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString
                codigo = dbBase.GetParameterValue(dbCommand, "@codigo").ToString

                dbCommand = Me.dbBase.GetStoredProcCommand("Usp_InsPS_AsignacionSSSCentroCostoSubCategoria")





                dbBase.AddInParameter(dbCommand, "@ASS_CodigoSSCentroCostoSubCategoria", DbType.Int16, obe_PS_AsignacionSSSCentroCostoSubCategoria.ASS_CodigoSSCentroCostoSubCategoria)
                dbBase.AddInParameter(dbCommand, "@ASP_CodigoEstructuraSubCategoria", DbType.Int16, obe_PS_AsignacionSSSCentroCostoSubCategoria.ASP_CodigoEstructuraSubCategoria)
                dbBase.AddInParameter(dbCommand, "@ASC_CodigoSSCentroCostoCategoria", DbType.Int16, codigo)

                dbBase.AddInParameter(dbCommand, "@ASS_Estado", DbType.Boolean, obe_PS_AsignacionSSSCentroCostoSubCategoria.ASS_Estado)


                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)
                dbBase.AddInParameter(dbCommand, "@ASS_PermitirEditar", DbType.Boolean, obe_PS_AsignacionSSSCentroCostoSubCategoria.ASS_PermitirEditar)


                dbBase.ExecuteScalar(dbCommand, tran)


                mensaje = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString
                codigo = dbBase.GetParameterValue(dbCommand, "@codigo").ToString


                '         @ASS_CodigoSSCentroCostoSubCategoria int,
                '               @ASP_CodigoEstructuraSubCategoria int,
                '               @ASC_CodigoSSCentroCostoCategoria int,
                '               @ASS_Estado bit ,
                '@codigo int out ,
                '@mensaje varchar(max) out

            Next

            Commit()

        Catch ex As Exception
            Rollback()

        End Try

    End Function

#End Region



End Class
