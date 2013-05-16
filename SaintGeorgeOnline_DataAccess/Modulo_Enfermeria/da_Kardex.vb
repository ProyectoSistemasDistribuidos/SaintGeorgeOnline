Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class da_Kardex
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

        Public Function FUN_UPD_KardexStocksMinimos(ByVal objKardex As be_Kardex, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_UPD_KardexStocksMinimos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoKardex", DbType.Int16, objKardex.CodigoKardex)
            dbBase.AddInParameter(dbCommand, "@p_StockMinimo", DbType.Int16, objKardex.StockMinimo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_INS_DetalleKardex(ByVal dtKardex As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 1

            Try
                'Inicio la transaccion
                BeginTransaction()

                For Each dr As DataRow In dtKardex.Rows

                    Dim objKardex As New be_Kardex

                    objKardex.CodigoMedicamento = dr.Item("CodigoMedicamento")
                    objKardex.Cantidad = dr.Item("Cantidad")
                    objKardex.TipoAccion = dr.Item("CodigoTipoAccion")
                    objKardex.CodigoSede = dr.Item("CodigoSede")
                    objKardex.CodigoMotivoSalida = dr.Item("CodigoMotivoSalida")
                    objKardex.Observacion = dr.Item("Observaciones")
                    objKardex.UsuarioRegistro = int_CodigoUsuario

                    FUN_INS_Kardex(objKardex, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                Next

                Commit()

                str_Mensaje = "Grabación exitosa."

                Return int_Valor
            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0
            Finally
                Conexion.Close()
            End Try


        End Function

        Private Sub FUN_INS_Kardex(ByVal objKardex As be_Kardex, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_Kardex")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoMedicamento", DbType.Int16, objKardex.CodigoMedicamento)
            dbBase.AddInParameter(dbCommand, "@p_Cantidad", DbType.Int16, objKardex.Cantidad)
            dbBase.AddInParameter(dbCommand, "@p_TipoAccion", DbType.Int16, objKardex.TipoAccion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, objKardex.CodigoSede)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMotivoSalida", DbType.Int16, objKardex.CodigoMotivoSalida)
            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objKardex.Observacion)
            dbBase.AddInParameter(dbCommand, "@p_UsuarioRegistro", DbType.String, objKardex.UsuarioRegistro)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
        End Sub

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Kardex(ByVal int_CodigoSede As Integer, ByVal str_medicamento As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_KardexMedicamentos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int16, int_CodigoSede)
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_medicamento)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_Kardex(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_GET_KardexMedicamentos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoKardex", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_KardexStockMedicamento(ByVal int_CodigoSede As Integer, ByVal int_CodigoMedicamento As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_GET_KardexStockMedicamentos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int16, int_CodigoSede)
            dbBase.AddInParameter(cmd, "@p_CodigoMedicamento", DbType.Int16, int_CodigoMedicamento)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_KardexMovimientos(ByVal int_CodigoSede As Integer, ByVal int_CodigoMedicamento As Integer, ByVal date_FechaInicio As Date, ByVal date_FechaFin As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_KardexMovimientos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int16, int_CodigoSede)
            dbBase.AddInParameter(cmd, "@p_CodigoMedicamento", DbType.Int16, int_CodigoMedicamento)
            dbBase.AddInParameter(cmd, "@p_FechaInicio", DbType.Date, date_FechaInicio)
            dbBase.AddInParameter(cmd, "@p_FechaFin", DbType.Date, date_FechaFin)

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
