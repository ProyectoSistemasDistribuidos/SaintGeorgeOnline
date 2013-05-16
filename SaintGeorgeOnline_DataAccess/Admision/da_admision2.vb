Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports SaintGeorgeOnline_DataAccess.InstanciaConexion
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Public Class da_admision2
    Inherits ManejadorConexion

#Region "Atributos"

    Private dbBase As SqlDatabase 'ExecuteDataSet
    Private dbCommand As DbCommand 'ExecuteScalar

    Private cnn As DbConnection
    Private tran As DbTransaction

#End Region

#Region "Constructor"

    Public Sub New()
        dbBase = New SqlDatabase(Me.SqlConexionAdmisionDB)
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

#Region "Metodos No Transaccionales"

    Public Function FUN_reporteAlumnosBDTesoreria(ByVal codAnio As Integer, ByVal codGrado As Integer) As DataSet

        Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AD_USP_LIS_listar_postulantesAdmitidos")

        'Parámetros de entrada
        dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, codAnio)
        dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, codGrado)
        

        'Ejecucion del Store Procedure
        Return dbBase.ExecuteDataSet(cmd)

    End Function


#End Region
End Class
