Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities

Public Class DA_PS_AsignacionArticulosPeriodos
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

    Public Function InsertarPS_AsignacionArticulosPeriodos(ByVal lstBE_PS_AsignacionArticulosPeriodos As List(Of BE_PS_AsignacionArticulosPeriodos)) As List(Of Integer)
        Dim lstIds As New List(Of Integer)

        Try
            BeginTransaction()
            Using dbCommand = Me.dbBase.GetStoredProcCommand("Usp_InsPS_AsignacionArticulosPeriodos")
                For Each oBE_PS_AsignacionArticulosPeriodos As BE_PS_AsignacionArticulosPeriodos In lstBE_PS_AsignacionArticulosPeriodos
                    dbCommand.Parameters.Clear()

                    dbBase.AddInParameter(dbCommand, "@AAP_CodigoEstructuraArticulo", DbType.Int32, oBE_PS_AsignacionArticulosPeriodos.AAP_CodigoEstructuraArticulo)
                    dbBase.AddInParameter(dbCommand, "@ASP_CodigoEstructuraSubCategoria", DbType.Int16, oBE_PS_AsignacionArticulosPeriodos.ASP_CodigoEstructuraSubCategoria)
                    dbBase.AddInParameter(dbCommand, "@AT_CodigoItem", DbType.Int16, oBE_PS_AsignacionArticulosPeriodos.AT_CodigoItem)
                    dbBase.AddInParameter(dbCommand, "@AAP_PrecioArticulo", DbType.Decimal, oBE_PS_AsignacionArticulosPeriodos.AAP_PrecioArticulo)
                    dbBase.AddInParameter(dbCommand, "@MD_CodigoMoneda", DbType.Int16, oBE_PS_AsignacionArticulosPeriodos.MD_CodigoMoneda)
                    dbBase.AddInParameter(dbCommand, "@AAP_UnidadMedida", DbType.String, oBE_PS_AsignacionArticulosPeriodos.AAP_UnidadMedida)

                    dbBase.AddInParameter(dbCommand, "@SI_CodigoSituacionItem", DbType.Int32, oBE_PS_AsignacionArticulosPeriodos.SI_CodigoSituacionItem)


                    'Parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)

                    dbBase.ExecuteScalar(dbCommand, tran)



                    Dim strMensaje As String = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
                    Dim intCodigo As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                    lstIds.Add(intCodigo)


                    '@AAP_CodigoEstructuraArticulo  int ,
                    '@ASP_CodigoEstructuraSubCategoria   int ,
                    '@AT_CodigoItem  int   ,
                    '@AAP_PrecioArticulo  decimal(8, 2) ,
                    '@MD_CodigoMoneda  int ,
                    '@AAP_UnidadMedida   varchar(50) ,
                    '@mensaje varchar(50) out ,
                    '@codigo int out
                    dbCommand.Parameters.Clear()

                Next
                Commit()
                Return lstIds




            End Using
        Catch ex As Exception
            Rollback()
        End Try
    End Function

#End Region

#Region "no transaccional"


    Public Function listarArticulosAsignacionSubcategoria(ByVal ASP_CodigoEstructuraSubCategoria As Integer) As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_LisArticulosSubcategoria")
                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@ASP_CodigoEstructuraSubCategoria", DbType.Int32, ASP_CodigoEstructuraSubCategoria)
                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd).Tables(0)

            End Using
        Catch ex As Exception

        End Try
    End Function

#End Region
End Class
