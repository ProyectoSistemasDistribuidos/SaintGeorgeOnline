Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloActividades

Namespace ModuloActividades

    Public Class da_AprobacionActividades
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

        Public Function FUN_INS_AprobacionActividades( _
            ByVal obe_AprobacionActividades As be_AprobacionActividades, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MiMensaje As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("AC_USP_UPD_AprobacionActividades")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoActividad", DbType.Int32, obe_AprobacionActividades.CodigoActividad)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAprobacion", DbType.Int32, obe_AprobacionActividades.CodigoAprobacion)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajador", DbType.Int32, obe_AprobacionActividades.CodigoTrabajador)
                dbBase.AddInParameter(dbCommand, "@p_CodigoEstado", DbType.Int32, obe_AprobacionActividades.CodigoEstado)
                dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, obe_AprobacionActividades.Observacion)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not int_Valor > 0 Then
                    Rollback()
                    Return int_Valor
                End If

                Commit()
                Return int_Valor

            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AprobacionActividadesCoordinacion( _
            ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoMes As Integer, ByVal int_Estado As Integer, ByVal int_CodigoTrabajador As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AC_USP_LIS_AprobacionActividadesCoordinacion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)
            dbBase.AddInParameter(cmd, "@p_CodigoMes", DbType.String, int_CodigoMes)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int32, int_Estado)
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace