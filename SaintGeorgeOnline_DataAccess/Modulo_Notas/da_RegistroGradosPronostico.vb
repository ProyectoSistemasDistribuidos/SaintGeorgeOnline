Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_RegistroGradosPronostico
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

        Public Function FUN_INS_RegistroGradosPronostico(ByVal objRegistroGradosPronostico As be_RegistroGradosPronostico, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_RegistroGradosPronostico")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroNotaGradoPronostico", DbType.Int32, objRegistroGradosPronostico.CodigoRegistroNotaGradoPronostico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionGradoPronostico", DbType.Int32, objRegistroGradosPronostico.CodigoProgramacionGradoPronostico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupoCurso", DbType.Int32, objRegistroGradosPronostico.CodigoAsignacionGrupoCurso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objRegistroGradosPronostico.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_Nota", DbType.Int16, objRegistroGradosPronostico.Nota)
            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objRegistroGradosPronostico.Observacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_GruposIBPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoProfesor As Integer, ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoProgramacionGrupo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_GruposIBPorAlumno")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoProfesor", DbType.Int16, int_CodigoProfesor)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoProgramacionGradoPronostico", DbType.Int16, int_CodigoProgramacionGrupo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace