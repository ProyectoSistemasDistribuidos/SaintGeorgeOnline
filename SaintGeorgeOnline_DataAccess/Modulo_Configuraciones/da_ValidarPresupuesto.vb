Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessEntities
Public Class da_ValidarPresupuesto
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


#Region "no  transaccional"

    ''' <summary>
    ''' funcion para listar estructura de un presupuesto 
    ''' </summary>
    ''' <param name="ASSSCC_CodigoAsignacionSSSCentroCosto">codigo del presupuesto</param>
    ''' <param name="codTrabajador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fListarEstructuraPresupuestoValidar(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal codTrabajador As Integer) As DataSet
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_LisPresupuestoValidacion")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int32, ASSSCC_CodigoAsignacionSSSCentroCosto)
                dbBase.AddInParameter(cmd, "@codTrabajador", DbType.String, codTrabajador)

                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)



                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)

            End Using
        Catch ex As Exception

        End Try
    End Function


    Public Function fListarEstructuraPresupuestoValidarPorUsuario(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal codTrabajador As Integer) As DataSet

        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_LisPresupuestoValidacionPorUsuario2")
                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@codSolicitudPresupuesto", DbType.Int32, ASSSCC_CodigoAsignacionSSSCentroCosto)
                dbBase.AddInParameter(cmd, "@codTrabajador", DbType.String, codTrabajador)
                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)
            End Using
        Catch ex As Exception

        End Try
    End Function


    Public Function Fun_Lis_Validadores(ByVal CodigoAsignacionSSSCentroCosto As Integer, _
                                        ByVal CodigoResponsableValidarPresupuesto As Integer) As DataSet
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_ValidadoresPorPresupuesto")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@CodigoAsignacionSSSCentroCosto", DbType.Int32, CodigoAsignacionSSSCentroCosto)
                dbBase.AddInParameter(cmd, "@CodigoResponsableValidarPresupuesto", DbType.Int32, CodigoResponsableValidarPresupuesto)

                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)

            End Using
        Catch ex As Exception

        End Try
    End Function


#End Region
#Region "transaccional"
    Public Function fInsertarValidarPresupuesto(ByVal lstBE_PS_RegistroValidacionesPresupuestos As List(Of BE_PS_RegistroValidacionesPresupuestos), ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer)

        Dim lstIds As New List(Of Integer)
        Dim mensaje As String = ""
        Dim codigo As Integer = 0

        Dim cmd As DbCommand = dbBase.GetStoredProcCommand("USP_insertarRegistroValidacionesPresupuestos")
        Try
            BeginTransaction()
            For Each oBE_PS_RegistroValidacionesPresupuestos As BE_PS_RegistroValidacionesPresupuestos In lstBE_PS_RegistroValidacionesPresupuestos
                cmd.Parameters.Clear()
                dbBase.AddInParameter(cmd, "@RVP_CodigoRegistroValidacionesPresupuesto", DbType.Int16, oBE_PS_RegistroValidacionesPresupuestos.RVP_CodigoRegistroValidacionesPresupuesto)
                dbBase.AddInParameter(cmd, "@ARVP_CodigoResponsableValidarPresupuesto", DbType.Int16, oBE_PS_RegistroValidacionesPresupuestos.ARVP_CodigoResponsableValidarPresupuesto)
                dbBase.AddInParameter(cmd, "@DSPS_CodigoDetSolicitudPresupuestoSubCategoria", DbType.Int16, oBE_PS_RegistroValidacionesPresupuestos.DSPS_CodigoDetSolicitudPresupuestoSubCategoria)
                dbBase.AddInParameter(cmd, "@RVP_Observacion", DbType.String, oBE_PS_RegistroValidacionesPresupuestos.RVP_Observacion)
                dbBase.AddInParameter(cmd, "@RVP_MontoAprobado", DbType.Double, oBE_PS_RegistroValidacionesPresupuestos.RVP_MontoAprobado)
                dbBase.AddInParameter(cmd, "@RVP_Estado", DbType.Boolean, oBE_PS_RegistroValidacionesPresupuestos.RVP_Estado)


                dbBase.AddInParameter(cmd, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int16, ASSSCC_CodigoAsignacionSSSCentroCosto)

                dbBase.AddInParameter(cmd, "@ESC_CodigoEstadoSubCategoria", DbType.Int16, oBE_PS_RegistroValidacionesPresupuestos.estadoAprobado)

                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int16, 10)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 500)

                dbBase.ExecuteScalar(cmd, tran)

                mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                codigo = CInt(CStr(dbBase.GetParameterValue(cmd, "@codigo")))

                lstIds.Add(codigo)
            Next


            If lstIds.Contains(0) Then
                Rollback()
            End If

            Commit()
            Return lstIds
        Catch ex As Exception
            Rollback()
        Finally

        End Try
    End Function

#End Region
End Class
