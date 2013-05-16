
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities

Public Class da_RegistroNotasAnualCualitativo
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

    Public Function FUN_INS_RegistroNotasAnualCualitativo(ByVal obe_RegistroNotasAnualCualitativo As be_RegistroNotasAnualCualitativo, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal bimestre As Integer) As List(Of String)
        Dim lstResultado As New List(Of String)
        Try
            BeginTransaction()
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            'ocon = dbBase.CreateConnection()
            dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasAnualCualitativo")
            'otr = cmd.Transaction
            dbBase.AddInParameter(dbCommand, "@P_CodigoAsignacionGrupo", DbType.Int32, obe_RegistroNotasAnualCualitativo.int_CodigoAsignacionGrupo)
            dbBase.AddInParameter(dbCommand, "@P_CodigoAnioAcademico", DbType.Int32, obe_RegistroNotasAnualCualitativo.int_P_CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@P_CodigoAlumno", DbType.String, obe_RegistroNotasAnualCualitativo.str_CodigoAlumno)
            '@P_CodigoAsignacionGrupo INT,
            '@P_CodigoAnioAcademico INT,
            '@P_CodigoAlumno varchar(8),
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            dbBase.ExecuteScalar(dbCommand, tran)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
            lstResultado.Add(str_Mensaje)
            lstResultado.Add(p_Valor.ToString())
            Dim lsta As New List(Of String)
            For i = 1 To 4
                lsta.Clear()
                dbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegNotasBimeCua")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@P_CodigoBimestre", DbType.Int32, i)
                dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroAnualL", DbType.Int32, Convert.ToInt32(lstResultado(1)))
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(dbCommand, tran)
                lsta.Add(str_Mensaje)
                lsta.Add(p_Valor.ToString())
                dbCommand.Parameters.Clear()
            Next
            Dim p_Valor1 As Integer = 0
            Dim str_Mensaje1 As String = ""

            'str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
            'p_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))

            'If p_Valor1 > 0 Then
            If lsta(1) > 0 Then
                Commit()
            Else
                Rollback()
            End If

            'Else
            'Rollback()
            'End If
            '

            Return lstResultado
            'otr.Rollback()


        Catch ex As Exception
            Rollback()
        End Try
    End Function
#End Region
End Class


