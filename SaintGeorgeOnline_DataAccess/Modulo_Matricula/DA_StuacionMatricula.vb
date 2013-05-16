Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula

Public Class DA_StuacionMatricula
    Inherits InstanciaConexion.ManejadorConexion

    'Actualizado 30-05-2012

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
    Public Function F_funcionActualizarSituacionMAtricula(ByVal dc As Dictionary(Of Integer, Integer))

        Dim cod As Integer = 0
        Try
            'BeginTransaction()

            'Registro la cabecera
            Using dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_ActualizarSituacionALumnoFinal")
                'Datos Generales de la cabecera
                For Each f As KeyValuePair(Of Integer, Integer) In dc
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@codMatricula", DbType.Int32, f.Key)
                    dbBase.AddInParameter(dbCommand, "@codSituacion", DbType.Int16, f.Value)
                    cod = CType(dbBase.ExecuteScalar(dbCommand), Integer)
                Next
            End Using
            Return cod
        Catch ex As Exception

        End Try
    End Function
#End Region
End Class
