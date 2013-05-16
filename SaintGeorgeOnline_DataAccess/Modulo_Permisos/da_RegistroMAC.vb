Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos

Namespace ModuloPermisos

    Public Class da_RegistoMAC
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

        Public Function FUN_INS_RegistroMAC( _
            ByVal obe_RegistroMAC As be_RegistroMAC, _
            ByVal lstEliminados As List(Of Integer), _
            ByVal dt_Detalle As DataTable, _
            ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim lstResultado As New List(Of String)
            Try
                BeginTransaction()
                Dim dbCommand As DbCommand
                Dim usp_mensaje1 = "", usp_mensaje2 = "", usp_mensaje3 As String = ""
                Dim usp_Valor1 = 0, usp_Valor2 = 0, usp_Valor3 As Integer = 0

                dbCommand = dbBase.GetStoredProcCommand("MA_USP_INS_RegistroMAC")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@p_CodigoRegistro", DbType.Int32, obe_RegistroMAC.CodigoCabecera)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int32, obe_RegistroMAC.CodigoPersona)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersona", DbType.Int32, obe_RegistroMAC.CodigoTipoPersona)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(dbCommand, tran)
                usp_mensaje1 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                usp_Valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                If Not usp_Valor1 > 0 Then
                    Rollback()
                    str_mensaje = usp_mensaje1
                    Return 0
                End If

                For Each item As Integer In lstEliminados
                    dbCommand = dbBase.GetStoredProcCommand("MA_USP_DEL_DetalleRegistrosMAC")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoRegistro", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleRegistroMAC", DbType.Int32, item)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje2 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor2 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje2
                        Return 0
                    End If
                Next

                For Each dr As DataRow In dt_Detalle.Rows
                    dbCommand = dbBase.GetStoredProcCommand("MA_USP_INS_DetalleRegistrosMAC")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleRegistroMAC", DbType.Int32, dr.Item("cDetalle"))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroMAC", DbType.Int32, usp_Valor1)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoDispositivo", DbType.Int32, dr.Item("cTipoDispositivo"))
                    dbBase.AddInParameter(dbCommand, "@p_DireccionMAC", DbType.String, dr.Item("DireccionMAC"))
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 100)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.ExecuteScalar(dbCommand, tran)
                    usp_mensaje3 = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    usp_Valor3 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
                    If Not usp_Valor3 > 0 Then
                        Rollback()
                        str_mensaje = usp_mensaje3
                        Return 0
                    End If

                Next

                Commit()
                str_mensaje = usp_mensaje1
                Return usp_Valor1

            Catch ex As Exception
                Rollback()
                str_mensaje = ex.Message
                Return 0
            Finally
                Conexion.Close()
            End Try
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_RegistroMAC(ByVal int_CodigoPersona As Integer, ByVal int_CodigoTipoPersona As Integer, _
                                            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_RegistroMAC")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPersona", DbType.Int32, int_CodigoPersona)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoPersona", DbType.Int32, int_CodigoTipoPersona)

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
