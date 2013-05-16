Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

Namespace ModuloPensiones

    Public Class da_NotaCreditoDebito
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

        Public Function FUN_INS_NotaCredito(ByVal objNotaCredito As be_NotaCreditoDebito, ByVal objDetalle As DataSet, ByRef str_ValorNumeroNotaCredito As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim str_MensajeDetalle As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_NotaCredito")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, objNotaCredito.CodigoPago)
                dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objNotaCredito.CodigoMoneda)
                dbBase.AddInParameter(dbCommand, "@p_CodigoConceptoCobro", DbType.Int32, objNotaCredito.CodigoConceptoCobro)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objNotaCredito.CodigoTalonario)
                dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.Date, objNotaCredito.FechaEmision)
                dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objNotaCredito.Observacion)
                dbBase.AddInParameter(dbCommand, "@p_CodigoDocumento", DbType.Int32, objNotaCredito.CodigoDocumento)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroNotaCredito", DbType.String, 11)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                str_ValorNumeroNotaCredito = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroNotaCredito").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Detalle Nota Credito
                If int_Valor > 0 Then

                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objNotaCreditoDetalle As be_NotaCreditoDebitoDetalle

                            For Each dr As DataRow In objDetalle.Tables(0).Rows

                                objNotaCreditoDetalle = New be_NotaCreditoDebitoDetalle
                                objNotaCreditoDetalle.CodigoNotaCredito = int_Valor
                                objNotaCreditoDetalle.CodigoDetallePago = dr.Item("CodigoDetallePago")
                                objNotaCreditoDetalle.Monto = dr.Item("Monto")

                                int_ValorDetalle = FUN_INS_NotaCreditoDetalle(objNotaCreditoDetalle, tran, str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                If Not int_ValorDetalle > 0 Then
                                    Rollback()
                                    Return int_ValorDetalle
                                End If

                            Next

                        End If
                    End If

                Else
                    Rollback()
                    Return int_Valor
                End If

                Commit()
                Return int_Valor

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Private Function FUN_INS_NotaCreditoDetalle(ByVal objNotaCreditoDetalle As be_NotaCreditoDebitoDetalle, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
                   ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_NotaCreditoDetalle")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoNotaCredito", DbType.Int32, objNotaCreditoDetalle.CodigoNotaCredito)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetallePago", DbType.Int32, objNotaCreditoDetalle.CodigoDetallePago)
            dbBase.AddInParameter(dbCommand, "@p_Monto", DbType.Decimal, objNotaCreditoDetalle.Monto)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)

            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function


        Public Function FUN_INS_NotaDebito(ByVal objNotaDebito As be_NotaCreditoDebito, ByVal objDetalle As DataSet, ByRef str_ValorNumeroNotaDebito As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim str_MensajeDetalle As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_NotaDebito")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, IIf(objNotaDebito.CodigoPago = 0, DBNull.Value, objNotaDebito.CodigoPago))
                dbBase.AddInParameter(dbCommand, "@p_CodigoLetra", DbType.Int32, IIf(objNotaDebito.CodigoLetra = 0, DBNull.Value, objNotaDebito.CodigoLetra))
                dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objNotaDebito.CodigoMoneda)
                dbBase.AddInParameter(dbCommand, "@p_CodigoConceptoCobro", DbType.Int32, objNotaDebito.CodigoConceptoCobro)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objNotaDebito.CodigoTalonario)
                dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.Date, objNotaDebito.FechaEmision)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroNotaDebito", DbType.String, 11)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                str_ValorNumeroNotaDebito = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroNotaDebito").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Detalle Nota Credito
                If int_Valor > 0 Then

                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objNotaDebitoDetalle As be_NotaCreditoDebitoDetalle

                            For Each dr As DataRow In objDetalle.Tables(0).Rows

                                objNotaDebitoDetalle = New be_NotaCreditoDebitoDetalle
                                objNotaDebitoDetalle.CodigoNotaCredito = int_Valor
                                objNotaDebitoDetalle.CodigoDetallePago = dr.Item("CodigoDetallePago")
                                objNotaDebitoDetalle.CodigoDetalleLetra = dr.Item("CodigoDetalleLetra")
                                objNotaDebitoDetalle.Monto = dr.Item("Monto")

                                int_ValorDetalle = FUN_INS_NotaDebitoDetalle(objNotaDebitoDetalle, tran, str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                If Not int_ValorDetalle > 0 Then
                                    Rollback()
                                    Return int_ValorDetalle
                                End If

                            Next

                        End If
                    End If

                Else
                    Rollback()
                    Return int_Valor
                End If

                Commit()
                Return int_Valor

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Private Function FUN_INS_NotaDebitoDetalle(ByVal objNotaDebitoDetalle As be_NotaCreditoDebitoDetalle, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
                   ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_NotaDebitoDetalle")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoNotaDebito", DbType.Int32, objNotaDebitoDetalle.CodigoNotaCredito)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetallePago", DbType.Int32, IIf(objNotaDebitoDetalle.CodigoDetallePago = 0, DBNull.Value, objNotaDebitoDetalle.CodigoDetallePago))
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleLetra", DbType.Int32, IIf(objNotaDebitoDetalle.CodigoDetalleLetra = 0, DBNull.Value, objNotaDebitoDetalle.CodigoDetalleLetra))
            dbBase.AddInParameter(dbCommand, "@p_Monto", DbType.Decimal, objNotaDebitoDetalle.Monto)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)

            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function


        Public Function FUN_DEL_NotaDebitoLetra(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_DEL_NotaDebitoLetra")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_NotasLetra(ByVal str_NumeroIni As String, ByVal str_NumeroFin As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_NotaDebitoLetra")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_NumeroIni", DbType.String, str_NumeroIni)
            dbBase.AddInParameter(cmd, "@p_NumeroFin", DbType.String, str_NumeroFin)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace
