Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_RegistroNotasBimestralesCuantitativas
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

        Public Function FUN_UPD_ObservacionNotasBimestralesCuantitativas( _
            ByVal int_CodigoRegistroBimestral As Integer, ByVal str_descripcionObservacion As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0

                'Inicio la transaccion
                BeginTransaction()
                ' insert
                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_ObservacionNotasBimestralesCuantitativas")

                dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroBimestre", DbType.Int16, int_CodigoRegistroBimestral)
                dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, str_descripcionObservacion)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                p_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not p_Valor > 0 Then
                    Rollback()
                End If

                Commit()

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado

            Catch ex As Exception

                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                Rollback()
                Return lstResultado

            Finally

                Conexion.Close()
            End Try

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_NotasBimestralesCuantitativas(ByVal int_CodigoRegistroBimestral As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_NotasBimestralesCuantitativas")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoRegistroBimestre", DbType.Int16, int_CodigoRegistroBimestral)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_RegistroNotasBimestralesCuantitativas(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_CU_RegistroNotasBimestralesCuantitativas")
                dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                Return dbBase.ExecuteDataSet(dbCommand)
            Catch ex As Exception

            End Try
        End Function

#End Region

    End Class

End Namespace
