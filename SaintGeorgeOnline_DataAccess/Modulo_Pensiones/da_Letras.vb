Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

Namespace ModuloPensiones

    Public Class da_Letras
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

        Public Function FUN_INS_Letras(ByVal objLetras As be_Letras, ByVal objDetalle As DataSet, _
            ByVal obj_be_NotaCreditoDebito As be_NotaCreditoDebito, ByVal de_MontoDebito As Decimal, _
            ByRef str_ValorNumeroOperacion As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim int_ValorNotaDebito As Integer = 0
            Dim str_MensajeDetalle As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_Letras")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objLetras.CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int32, objLetras.CodigoFamiliar)
                dbBase.AddInParameter(dbCommand, "@p_NumeroOperacion", DbType.String, objLetras.NumeroOperacion)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Detalle Pagos
                If int_Valor > 0 Then

                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables.Count > 1 Then ' Si presenta los 2 detalles : "Letras" y "Deudas Vencidas"

                            If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "Letras", lo grabo
                                Dim objLetraDetalle As be_LetrasDetalle
                                For Each dr As DataRow In objDetalle.Tables(0).Rows
                                    objLetraDetalle = New be_LetrasDetalle
                                    objLetraDetalle.CodigoLetra = int_Valor
                                    objLetraDetalle.CodigoMoneda = dr.Item("CodigoMoneda")
                                    objLetraDetalle.FechaEmision = dr.Item("FechaEmision")
                                    objLetraDetalle.FechaVencimiento = dr.Item("FechaVencimiento")
                                    objLetraDetalle.MontoPagar = dr.Item("MontoPagar")
                                    objLetraDetalle.NumeroLetra = dr.Item("NumeroLetra")
                                    objLetraDetalle.DescripcionLetra = dr.Item("DescripcionLetra")
                                    objLetraDetalle.Orden = dr.Item("Orden")
                                    int_ValorDetalle = FUN_INS_LetrasDetalle(objLetraDetalle, tran, str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                    If Not int_ValorDetalle > 0 Then
                                        Rollback()
                                        str_Mensaje = str_MensajeDetalle
                                        Return int_ValorDetalle
                                    End If
                                Next
                            End If

                            ' Registro de la Nota de Débito
                            int_ValorNotaDebito = FUN_INS_NotaDebito(obj_be_NotaCreditoDebito, de_MontoDebito, tran, str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            If Not int_ValorNotaDebito > 0 Then
                                Rollback()
                                str_Mensaje = str_MensajeDetalle
                                Return int_ValorDetalle
                            End If

                            int_ValorDetalle = 0
                            str_MensajeDetalle = ""

                            If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "Deudas Vencidas", lo grabo
                                Dim int_CodigoPago As Integer = 0
                                For Each dr As DataRow In objDetalle.Tables(1).Rows
                                    int_CodigoPago = dr.Item("CodigoPago")
                                    int_ValorDetalle = FUN_INS_LetrasDeudasVencidas(int_CodigoPago, int_Valor, int_ValorNotaDebito, tran, str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                    If Not int_ValorDetalle > 0 Then
                                        Rollback()
                                        str_Mensaje = str_MensajeDetalle
                                        Return int_ValorDetalle
                                    End If
                                Next
                            End If

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

        Private Function FUN_INS_LetrasDetalle(ByVal objLetraDetalle As be_LetrasDetalle, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_LetrasDetalle")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoLetra", DbType.Int32, objLetraDetalle.CodigoLetra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objLetraDetalle.CodigoMoneda)
            dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.DateTime, objLetraDetalle.FechaEmision)
            dbBase.AddInParameter(dbCommand, "@p_FechaVencimiento", DbType.DateTime, objLetraDetalle.FechaVencimiento)
            dbBase.AddInParameter(dbCommand, "@p_MontoPagar", DbType.Decimal, objLetraDetalle.MontoPagar)
            dbBase.AddInParameter(dbCommand, "@p_NumeroLetra", DbType.String, objLetraDetalle.NumeroLetra)
            dbBase.AddInParameter(dbCommand, "@p_DescripcionLetra", DbType.String, objLetraDetalle.DescripcionLetra)
            dbBase.AddInParameter(dbCommand, "@p_Orden", DbType.String, objLetraDetalle.Orden)

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

        Private Function FUN_INS_NotaDebito(ByVal objNotaCreditoDebito As be_NotaCreditoDebito, ByVal de_Monto As Decimal, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_NotaDebitoParaLetras")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, objNotaCreditoDebito.CodigoPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objNotaCreditoDebito.CodigoMoneda)
            dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.DateTime, objNotaCreditoDebito.FechaEmision)
            dbBase.AddInParameter(dbCommand, "@p_Monto", DbType.Decimal, de_Monto)

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

        Private Function FUN_INS_LetrasDeudasVencidas(ByVal int_CodigoPago As Integer, ByVal int_CodigoLetra As Integer, ByVal int_CodigoNotaDebito As Integer, _
            ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_LetrasDeudasVencidas")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, int_CodigoPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoLetra", DbType.Int32, int_CodigoLetra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNotaDebito", DbType.Int32, int_CodigoNotaDebito)

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

        Public Function FUN_UPD_LetrasPago( _
            ByVal int_CodigoLetra As Integer, ByVal int_CodigoDetalleLetra As Integer, _
            ByVal str_CodigoAlumno As String, ByVal int_CodigoDeuda As Integer, _
            ByVal dt_FechaPagoLetra As Date, _
            ByVal int_OrigenPagoLetra As Integer, ByVal int_CodigoCuentaBancaria As Integer, ByVal int_FormaPagoLetra As Integer, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_LetrasPago")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoLetra", DbType.Int32, int_CodigoLetra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleLetra", DbType.Int32, int_CodigoDetalleLetra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, int_CodigoDeuda)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.DateTime, dt_FechaPagoLetra)

            dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, int_OrigenPagoLetra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, int_FormaPagoLetra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, IIf(int_CodigoCuentaBancaria = 0, DBNull.Value, int_CodigoCuentaBancaria))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        'Eliminar Pagos
        Public Function FUN_UPD_LetrasEliminar(ByVal int_CodigoDetalleLetra As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoTalonario As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_LetrasEliminar")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleLetra", DbType.Int32, int_CodigoDetalleLetra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        'suspender letra
        Public Function FUN_UPD_LetrasSuspender(ByVal int_CodigoDetalleLetra As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoTalonario As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_LetrasSuspender")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleLetra", DbType.Int32, int_CodigoDetalleLetra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
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

        Public Function FUN_LIS_LetrasPorCodigo(ByVal int_CodigoOperacion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_LetrasPorCodigo")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_CodigoOperacion)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_Letras(ByVal str_Nombre As String, ByVal int_CodigoTalonario As Integer, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicial As String, ByVal dt_FechaFinal As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_Letras")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, str_Nombre)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)
            dbBase.AddInParameter(cmd, "@p_TipoFecha", DbType.Int32, int_TipoFecha)
            dbBase.AddInParameter(cmd, "@p_FechaInicial", DbType.Date, dt_FechaInicial)
            dbBase.AddInParameter(cmd, "@p_FechaFinal", DbType.Date, dt_FechaFinal)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_OperacionLetras(ByVal str_Nombre As String, ByVal dt_FechaInicial As String, ByVal dt_FechaFinal As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_OperacionLetras")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, str_Nombre)
            dbBase.AddInParameter(cmd, "@p_FechaInicial", DbType.Date, dt_FechaInicial)
            dbBase.AddInParameter(cmd, "@p_FechaFinal", DbType.Date, dt_FechaFinal)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_OperacionLetras(ByVal int_CodigoLetra As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_OperacionLetra")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoLetra", DbType.Int32, int_CodigoLetra)

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