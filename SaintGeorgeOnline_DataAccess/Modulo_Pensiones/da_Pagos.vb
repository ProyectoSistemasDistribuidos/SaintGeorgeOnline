Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

Namespace ModuloPensiones

    Public Class da_Pagos
        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbBaseAdm As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar

        Private dbCommandAdm As DbCommand 'ExecuteScalar

        Private cnn As DbConnection
        Private cnnAdm As DbConnection
        Private tran As DbTransaction

#End Region

#Region "Constructor"

        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
            cnn = Me.dbBase.CreateConnection()
        End Sub

        Public Sub cnxAdmision()
            dbBaseAdm = New SqlDatabase(Me.SqlConexionAdmisionDB)
            cnnAdm = Me.dbBaseAdm.CreateConnection()
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

        'Pagos por Descarga de Banco
        Public Function FUN_INS_PagosPorDescargaBanco(ByVal objPagos As be_Pagos, ByVal dt_FechaVencimiento As Date, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByRef str_DescTalonario As String, ByRef str_DocumentoReferencia As String, ByRef str_DescripcionDeuda As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosPorDescargaBanco")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.Date, objPagos.FechaEmision)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)
            dbBase.AddInParameter(dbCommand, "@p_FechaVencimiento", DbType.Date, dt_FechaVencimiento)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)

            dbBase.AddInParameter(dbCommand, "@p_MontoCobro", DbType.Decimal, objPagos.MontoTotalCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoMora", DbType.Decimal, objPagos.MontoTotalMora)
            dbBase.AddInParameter(dbCommand, "@p_MontoTotal", DbType.Decimal, objPagos.MontoTotalPago)

            dbBase.AddInParameter(dbCommand, "@p_CodigoBanco", DbType.Int32, IIf(objPagos.CodigoBanco = 0, DBNull.Value, objPagos.CodigoBanco))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoPago", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoTalonario", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)

            dbBase.AddOutParameter(dbCommand, "@p_ValorTipoTalonario", DbType.String, 50)
            dbBase.AddOutParameter(dbCommand, "@p_ValorDocumentoDeReferencia", DbType.String, 50)
            dbBase.AddOutParameter(dbCommand, "@p_ValorDescripcionDeuda", DbType.String, 50)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_CodigoPago = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoPago")))
            int_CodigoTalonario = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoTalonario")))
            str_NumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()

            str_DescTalonario = dbBase.GetParameterValue(dbCommand, "@p_ValorTipoTalonario").ToString()
            str_DocumentoReferencia = dbBase.GetParameterValue(dbCommand, "@p_ValorDocumentoDeReferencia").ToString()
            str_DescripcionDeuda = dbBase.GetParameterValue(dbCommand, "@p_ValorDescripcionDeuda").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        'update
        Public Function FUN_INS_PagosAnualesPorDescargaBanco(ByVal objPagos As be_Pagos, ByVal dt_FechaVencimiento As Date, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByRef str_DescTalonario As String, ByRef str_DocumentoReferencia As String, ByRef str_DescripcionDeuda As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosAnualesPorDescargaBanco")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.Date, objPagos.FechaEmision)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)
            dbBase.AddInParameter(dbCommand, "@p_FechaVencimiento", DbType.Date, dt_FechaVencimiento)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)

            dbBase.AddInParameter(dbCommand, "@p_MontoCobro", DbType.Decimal, objPagos.MontoTotalCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoMora", DbType.Decimal, objPagos.MontoTotalMora)
            dbBase.AddInParameter(dbCommand, "@p_MontoTotal", DbType.Decimal, objPagos.MontoTotalPago)

            dbBase.AddInParameter(dbCommand, "@p_CodigoBanco", DbType.Int32, IIf(objPagos.CodigoBanco = 0, DBNull.Value, objPagos.CodigoBanco))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoPago", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoTalonario", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)

            dbBase.AddOutParameter(dbCommand, "@p_ValorTipoTalonario", DbType.String, 50)
            dbBase.AddOutParameter(dbCommand, "@p_ValorDocumentoDeReferencia", DbType.String, 50)
            dbBase.AddOutParameter(dbCommand, "@p_ValorDescripcionDeuda", DbType.String, 50)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_CodigoPago = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoPago")))
            int_CodigoTalonario = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoTalonario")))
            str_NumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()

            str_DescTalonario = dbBase.GetParameterValue(dbCommand, "@p_ValorTipoTalonario").ToString()
            str_DocumentoReferencia = dbBase.GetParameterValue(dbCommand, "@p_ValorDocumentoDeReferencia").ToString()
            str_DescripcionDeuda = dbBase.GetParameterValue(dbCommand, "@p_ValorDescripcionDeuda").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        'Pagos por Caja 
        Public Function FUN_INS_PagosPorCajaSinDetalle(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosPorCajaSinDetalle")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEmpresa", DbType.Int32, IIf(objPagos.CodigoEmpresa = 0, DBNull.Value, objPagos.CodigoEmpresa))
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, IIf(int_CodigoDeuda = 0, DBNull.Value, int_CodigoDeuda))

            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, objPagos.CodigoFormaPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, objPagos.CodigoOrigenPago)

            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)
            dbBase.AddInParameter(dbCommand, "@p_MontoCobro", DbType.Decimal, objPagos.MontoTotalCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoDescuento", DbType.Decimal, objPagos.MontoTotalDescuento)
            dbBase.AddInParameter(dbCommand, "@p_MontoMora", DbType.Decimal, objPagos.MontoTotalMora)
            dbBase.AddInParameter(dbCommand, "@p_MontoTotal", DbType.Decimal, objPagos.MontoTotalPago)

            If objPagos.CodigoFormaPago = 2 Then 'Cheque
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, objPagos.NumeroCheque)
            ElseIf objPagos.CodigoFormaPago = 3 Then 'Banco
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, objPagos.CodigoCuentaBancaria)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            Else ' Otra : Efectivo o planilla
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            End If

            'dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, IIf(objPagos.CodigoCuentaBancaria = 0, DBNull.Value, objPagos.CodigoCuentaBancaria))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoPago", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoTalonario", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_CodigoPago = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoPago")))
            int_CodigoTalonario = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoTalonario")))
            str_NumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_INS_PagosPorCajaSinDetalleConMora(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosConMora")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEmpresa", DbType.Int32, IIf(objPagos.CodigoEmpresa = 0, DBNull.Value, objPagos.CodigoEmpresa))
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, int_CodigoDeuda)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, objPagos.CodigoFormaPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, objPagos.CodigoOrigenPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)
            dbBase.AddInParameter(dbCommand, "@p_MontoCobro", DbType.Decimal, objPagos.MontoTotalCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoDescuento", DbType.Decimal, objPagos.MontoTotalDescuento)
            dbBase.AddInParameter(dbCommand, "@p_MontoMora", DbType.Decimal, objPagos.MontoTotalMora)
            dbBase.AddInParameter(dbCommand, "@p_MontoTotal", DbType.Decimal, objPagos.MontoTotalPago)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoBanco", DbType.Int32, IIf(objPagos.CodigoBanco = 0, DBNull.Value, objPagos.CodigoBanco))
            'dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, IIf(objPagos.CodigoCuentaBancaria = 0, DBNull.Value, objPagos.CodigoCuentaBancaria))

            If objPagos.CodigoFormaPago = 2 Then 'Cheque
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, objPagos.NumeroCheque)
            ElseIf objPagos.CodigoFormaPago = 3 Then 'Banco
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, objPagos.CodigoCuentaBancaria)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            Else ' Otra : Efectivo o planilla
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            End If

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoPago", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoTalonario", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_CodigoPago = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoPago")))
            int_CodigoTalonario = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoTalonario")))
            str_NumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_PagosPorCajaSinDetalleConMora(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_PagosConMora")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, int_CodigoDeuda)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, objPagos.CodigoFormaPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, objPagos.CodigoOrigenPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)
            dbBase.AddInParameter(dbCommand, "@p_MontoMora", DbType.Decimal, objPagos.MontoTotalPago)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoBanco", DbType.Int32, IIf(objPagos.CodigoBanco = 0, DBNull.Value, objPagos.CodigoBanco))
            'dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, IIf(objPagos.CodigoCuentaBancaria = 0, DBNull.Value, objPagos.CodigoCuentaBancaria))

            If objPagos.CodigoFormaPago = 2 Then 'Cheque
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, objPagos.NumeroCheque)
            ElseIf objPagos.CodigoFormaPago = 3 Then 'Banco
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, objPagos.CodigoCuentaBancaria)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            Else ' Otra : Efectivo o Planilla
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            End If



            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoPago", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoTalonario", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_CodigoPago = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoPago")))
            int_CodigoTalonario = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoTalonario")))
            str_NumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_INS_PagosPorCaja(ByVal objPagos As be_Pagos, ByVal objDetalle As DataSet, ByRef str_ValorNumeroPago As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim str_MensajeDetalle As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosPorCaja")

                'Parámetros de entrada                
                dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, objPagos.CodigoFormaPago)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, objPagos.CodigoOrigenPago)

                dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_CodigoEmpresa", DbType.Int32, IIf(objPagos.CodigoEmpresa = 0, DBNull.Value, objPagos.CodigoEmpresa))
                dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)
                'dbBase.AddInParameter(dbCommand, "@p_NumeroPago", DbType.String, objPagos.NumeroPago)
                dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)
                dbBase.AddInParameter(dbCommand, "@p_MontoTotalCobro", DbType.Decimal, objPagos.MontoTotalCobro)
                dbBase.AddInParameter(dbCommand, "@p_MontoTotalDescuento", DbType.Decimal, objPagos.MontoTotalDescuento)
                dbBase.AddInParameter(dbCommand, "@p_MontoTotalMora", DbType.Decimal, objPagos.MontoTotalMora)
                dbBase.AddInParameter(dbCommand, "@p_MontoTotalPago", DbType.Decimal, objPagos.MontoTotalPago)

                If objPagos.CodigoFormaPago = 2 Then 'Cheque
                    dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                    dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, objPagos.NumeroCheque)
                ElseIf objPagos.CodigoFormaPago = 3 Then 'Banco
                    dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, objPagos.CodigoCuentaBancaria)
                    dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
                Else ' Otra : Efectivo o planilla
                    dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                    dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
                End If

                'dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, IIf(objPagos.CodigoCuentaBancaria = 0, DBNull.Value, objPagos.CodigoCuentaBancaria))
                'dbBase.AddInParameter(dbCommand, "@p_CodigoBanco", DbType.Int32, IIf(objPagos.CodigoBanco = 0, DBNull.Value, objPagos.CodigoBanco))

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                str_ValorNumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Detalle Pagos
                If int_Valor > 0 Then

                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objPagosDetalle As be_PagosDetalle

                            For Each dr As DataRow In objDetalle.Tables(0).Rows

                                objPagosDetalle = New be_PagosDetalle
                                objPagosDetalle.CodigoPago = int_Valor
                                objPagosDetalle.CodigoConceptoCobro = dr.Item("CodigoConceptoCobro")
                                objPagosDetalle.CodigoDeuda = dr.Item("CodigoDeuda")
                                objPagosDetalle.MontoCobro = dr.Item("MontoCobro")
                                objPagosDetalle.MontoDescuento = dr.Item("MontoDescuentoBeca") + dr.Item("MontoDescuentoOtros")
                                objPagosDetalle.MontoMora = dr.Item("MontoMora")
                                objPagosDetalle.MontoPago = dr.Item("MontoPago")

                                int_ValorDetalle = FUN_INS_PagosPorCajaDetalle(objPagosDetalle, tran, str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

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

        'update 06/08/2012
        Public Function FUN_INS_PagosPorCajaSinDetalleDeudasAux(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosPorCajaSinDetalleDeudasAux")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEmpresa", DbType.Int32, IIf(objPagos.CodigoEmpresa = 0, DBNull.Value, objPagos.CodigoEmpresa))
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, IIf(int_CodigoDeuda = 0, DBNull.Value, int_CodigoDeuda))

            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, objPagos.CodigoFormaPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, objPagos.CodigoOrigenPago)

            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)
            dbBase.AddInParameter(dbCommand, "@p_MontoCobro", DbType.Decimal, objPagos.MontoTotalCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoDescuento", DbType.Decimal, objPagos.MontoTotalDescuento)
            dbBase.AddInParameter(dbCommand, "@p_MontoMora", DbType.Decimal, objPagos.MontoTotalMora)
            dbBase.AddInParameter(dbCommand, "@p_MontoTotal", DbType.Decimal, objPagos.MontoTotalPago)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoBanco", DbType.Int32, IIf(objPagos.CodigoBanco = 0, DBNull.Value, objPagos.CodigoBanco))
            'dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, IIf(objPagos.CodigoCuentaBancaria = 0, DBNull.Value, objPagos.CodigoCuentaBancaria))

            If objPagos.CodigoFormaPago = 2 Then 'Cheque
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, objPagos.NumeroCheque)
            ElseIf objPagos.CodigoFormaPago = 3 Then 'Banco
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, objPagos.CodigoCuentaBancaria)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            Else ' Otra : Efectivo o planilla
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            End If

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoPago", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoTalonario", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_CodigoPago = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoPago")))
            int_CodigoTalonario = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoTalonario")))
            str_NumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        'update
        Private Function FUN_INS_PagosPorCajaDetalle(ByVal objPagosDetalle As be_PagosDetalle, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosPorCajaDetalle")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, objPagosDetalle.CodigoPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoConceptoCobro", DbType.Int32, objPagosDetalle.CodigoConceptoCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoCobro", DbType.Decimal, objPagosDetalle.MontoCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoDescuento", DbType.Decimal, objPagosDetalle.MontoDescuento)
            dbBase.AddInParameter(dbCommand, "@p_MontoMora", DbType.Decimal, objPagosDetalle.MontoMora)
            dbBase.AddInParameter(dbCommand, "@p_MontoPago", DbType.Decimal, objPagosDetalle.MontoPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, IIf(objPagosDetalle.CodigoDeuda = 0, DBNull.Value, objPagosDetalle.CodigoDeuda))

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

        'Pagos - Deudas vencidas
        Public Function FUN_INS_PagosDeudasVencidas(ByVal int_CodigoDeuda As Integer, ByVal objPagos As be_Pagos, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosDeudasVencidas")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, int_CodigoDeuda)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEmpresa", DbType.Int32, IIf(objPagos.CodigoEmpresa = 0, DBNull.Value, objPagos.CodigoEmpresa))
            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)
            dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.Date, objPagos.FechaEmision)

            dbBase.AddInParameter(dbCommand, "@p_CodigoBanco", DbType.Int32, IIf(objPagos.CodigoBanco = 0, DBNull.Value, objPagos.CodigoBanco))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoPago", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoTalonario", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_CodigoPago = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoPago")))
            int_CodigoTalonario = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoTalonario")))
            str_NumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_PagosDeudasVencidas(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("FUN_UPD_PagosDeudasVencidas")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, int_CodigoDeuda)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, objPagos.CodigoFormaPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, objPagos.CodigoOrigenPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, IIf(objPagos.CodigoCuentaBancaria = 0, DBNull.Value, objPagos.CodigoCuentaBancaria))

            If objPagos.CodigoFormaPago = 2 Then 'Cheque
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, objPagos.NumeroCheque)
            ElseIf objPagos.CodigoFormaPago = 3 Then 'Banco
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, objPagos.CodigoCuentaBancaria)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            Else ' Otra : Efectivo o Planilla
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            End If

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoPago", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorCodigoTalonario", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_CodigoPago = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoPago")))
            int_CodigoTalonario = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCodigoTalonario")))
            str_NumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()

            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        'Anular Pagos
        Public Function FUN_UPD_PagosAnular(ByVal objPagos As be_Pagos, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_PagosAnular")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, objPagos.CodigoPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)

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
        Public Function FUN_UPD_PagosEliminar(ByVal objPagos As be_Pagos, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_PagosEliminar")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, objPagos.CodigoPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPagos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)

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

        'Pagos de Ingresos Varios
        Public Function FUN_INS_PagosIngresosVarios(ByVal objPagos As be_Pagos, ByVal objDetalle As DataSet, _
            ByRef bool_Admision As Boolean, ByRef de_montoAdmision As Decimal, ByRef str_ValorNumeroPago As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim str_MensajeDetalle As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosIngresosVarios")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoTalonario", DbType.Int32, objPagos.CodigoTalonario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, IIf(objPagos.CodigoAlumno = 0, DBNull.Value, objPagos.CodigoAlumno))
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersona", DbType.String, IIf(objPagos.CodigoTipoPersona = 0, DBNull.Value, objPagos.CodigoTipoPersona))
                dbBase.AddInParameter(dbCommand, "@p_DescripcionOtraPersona", DbType.String, IIf(objPagos.DescripcionOtraPersona Is Nothing, DBNull.Value, objPagos.DescripcionOtraPersona))
                dbBase.AddInParameter(dbCommand, "@p_CodigoEmpresa", DbType.Int32, IIf(objPagos.CodigoEmpresa = 0, DBNull.Value, objPagos.CodigoEmpresa))
                dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, objPagos.CodigoOrigenPago)
                dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, objPagos.CodigoFormaPago)

                If objPagos.CodigoFormaPago = 2 Then 'Cheque
                    dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                    dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, objPagos.NumeroCheque)
                ElseIf objPagos.CodigoFormaPago = 3 Then 'Banco
                    dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, objPagos.CodigoCuentaBancaria)
                    dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
                Else ' Otra : Efectivo
                    dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                    dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
                End If

                dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, objPagos.CodigoMoneda)
                dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objPagos.Observacion)
                dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.Date, objPagos.FechaEmision)
                dbBase.AddInParameter(dbCommand, "@p_MontoTotalCobro", DbType.Decimal, objPagos.MontoTotalCobro)
                dbBase.AddInParameter(dbCommand, "@p_MontoTotalDescuento", DbType.Decimal, objPagos.MontoTotalDescuento)
                dbBase.AddInParameter(dbCommand, "@p_MontoTotalPago", DbType.Decimal, objPagos.MontoTotalPago)

                dbBase.AddInParameter(dbCommand, "@p_CodigoBanco", DbType.Int32, IIf(objPagos.CodigoBanco = 0, DBNull.Value, objPagos.CodigoBanco))

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_ValorNumeroPago", DbType.String, 11)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                str_ValorNumeroPago = dbBase.GetParameterValue(dbCommand, "@p_ValorNumeroPago").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Detalle Pagos
                If int_Valor > 0 Then

                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objPagosDetalle As be_PagosDetalle

                            For Each dr As DataRow In objDetalle.Tables(0).Rows

                                objPagosDetalle = New be_PagosDetalle
                                objPagosDetalle.CodigoPago = int_Valor
                                objPagosDetalle.CodigoConceptoCobro = dr.Item("CodigoConceptoCobro")
                                objPagosDetalle.MontoCobro = dr.Item("MontoCobro")
                                objPagosDetalle.MontoDescuento = dr.Item("MontoDescuento")
                                objPagosDetalle.MontoPago = dr.Item("MontoPago")
                                objPagosDetalle.Cantidad = dr.Item("Cantidad")

                                int_ValorDetalle = FUN_INS_PagosIngresosVariosDetalle(objPagosDetalle, tran, str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                If Not int_ValorDetalle > 0 Then
                                    Rollback()
                                    Return int_ValorDetalle
                                End If

                                ' si el tipo de concepto es "Evaluación de Ingreso" : 23
                                ' si se registra el detalle

                                ' update 08/05/2013
                                'If objPagosDetalle.CodigoConceptoCobro = 23 Then
                                '    bool_Admision = True
                                '    de_montoAdmision = objPagosDetalle.MontoPago
                                'End If

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

        Private Function FUN_INS_PagosIngresosVariosDetalle(ByVal objPagosDetalle As be_PagosDetalle, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_PagosIngresosVariosDetalle")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, objPagosDetalle.CodigoPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoConceptoCobro", DbType.Int32, objPagosDetalle.CodigoConceptoCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoCobro", DbType.Decimal, objPagosDetalle.MontoCobro)
            dbBase.AddInParameter(dbCommand, "@p_MontoDescuento", DbType.Decimal, objPagosDetalle.MontoDescuento)
            dbBase.AddInParameter(dbCommand, "@p_MontoPago", DbType.Decimal, objPagosDetalle.MontoPago)
            dbBase.AddInParameter(dbCommand, "@p_Cantidad", DbType.Int32, objPagosDetalle.Cantidad)

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


#Region "Interfaz Admision"

        Public Function FUN_INS_PagosIngresosAdmision(ByVal str_NumDocumento As String, ByVal int_TipoDocumento As Integer, _
                                                  ByVal str_IdPostulante As String, ByVal dt_FechaRegistro As Date, _
                                                  ByVal int_Moneda As Integer, ByVal de_Monto As Decimal, ByRef str_Mensaje As String) As Integer
            Dim int_Valor As Integer = 0

            cnxAdmision()
            dbCommandAdm = Me.dbBaseAdm.GetStoredProcCommand("PG_USP_INS_PagosCarpeta")

            'Parámetros de entrada
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_NroDocumento", DbType.String, str_NumDocumento)
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_TipoDocumento", DbType.Int32, int_TipoDocumento)
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_IdPostulante", DbType.Decimal, str_IdPostulante)
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_FechaRegistro", DbType.DateTime, dt_FechaRegistro)
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_Moneda", DbType.Int32, int_Moneda)
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_Monto", DbType.Decimal, de_Monto)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommandAdm, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommandAdm, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBaseAdm.ExecuteScalar(dbCommandAdm)

            str_Mensaje = dbBaseAdm.GetParameterValue(dbCommandAdm, "@p_Mensaje").ToString()
            int_Valor = CInt(CStr(dbBaseAdm.GetParameterValue(dbCommandAdm, "@p_Valor")))

            Return int_Valor

        End Function

        Public Function FUN_UPD_EstadoPostulanteAdmision(ByVal int_CodigoPostulante As Integer, _
                                                         ByVal int_EstadoSubProceso As Integer, _
                                                         ByRef str_Mensaje As String, _
                                                         ByVal int_OrdenPagoAdmision As Integer, _
                                                         ByVal str_DocumentoPagoAdmision As String, _
                                                         ByVal de_MontoPagoAdmision As Decimal) As Integer
            Dim int_Valor As Integer = 0

            cnxAdmision()
            dbCommandAdm = Me.dbBaseAdm.GetStoredProcCommand("PG_USP_UPD_PostulanteSubProceso")

            'Parámetros de entrada
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_CodigoPostulante", DbType.Int32, int_CodigoPostulante)
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_EstadoSubProceso", DbType.Int32, int_EstadoSubProceso)

            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_OrdenPago", DbType.Int32, int_OrdenPagoAdmision)
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_DocumentoPago", DbType.String, str_DocumentoPagoAdmision)
            dbBaseAdm.AddInParameter(dbCommandAdm, "@p_MontoPago", DbType.Decimal, de_MontoPagoAdmision)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommandAdm, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommandAdm, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBaseAdm.ExecuteScalar(dbCommandAdm)

            str_Mensaje = dbBaseAdm.GetParameterValue(dbCommandAdm, "@p_Mensaje").ToString()
            int_Valor = CInt(CStr(dbBaseAdm.GetParameterValue(dbCommandAdm, "@p_Valor")))

            Return int_Valor

        End Function

#End Region

        'Actualizacion de Datos en Pagos Registrados
        Public Function FUN_UPD_PagosRegistrados(ByVal objPagos As be_Pagos, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_PagosRegistrados")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, objPagos.CodigoPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOrigenPago", DbType.Int32, objPagos.CodigoOrigenPago)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaPago", DbType.Int32, objPagos.CodigoFormaPago)

            If objPagos.CodigoFormaPago = 2 Then 'Cheque
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, objPagos.NumeroCheque)
            ElseIf objPagos.CodigoFormaPago = 3 Then 'Banco
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, objPagos.CodigoCuentaBancaria)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            Else ' Otra : Efectivo
                dbBase.AddInParameter(dbCommand, "@p_CodigoCuentaBancaria", DbType.Int32, DBNull.Value)
                dbBase.AddInParameter(dbCommand, "@p_NumeroCheque", DbType.String, DBNull.Value)
            End If

            dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.Date, objPagos.FechaEmision)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, objPagos.FechaPago)

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

#Region "Impresion"

        Public Function FUN_UPD_PagosEstadoEmision(ByVal int_CodigoDocumento As Integer, ByVal int_CodigoTalonario As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_PagosEstadoEmision")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.Int32, int_CodigoDocumento)
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

        Public Function FUN_UPD_PagosEstadoEmision(ByVal str_CodigoDocumento As String, ByVal int_CodigoTalonario As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_ListaPagosEstadoEmision")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPago", DbType.String, str_CodigoDocumento)
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

#Region "Importacion Pagos"

        Public Function fun_ins_exportacionpagos(ByVal objdetalle As DataSet, ByRef str_mensaje As String, _
            ByVal int_codigousuario As Integer, ByVal int_codigotipousuario As Integer, ByVal int_codigomodulo As Integer, ByVal int_codigoopcion As Integer) As List(Of String)

            Dim int_valor As Integer = 0
            Dim int_totalregistros As Integer = 0

            Dim lstRegistro As New List(Of String)

            Try
                'inicio la transaccion
                BeginTransaction()
                For Each dr As DataRow In objdetalle.Tables(0).Rows
                    dbCommand = Me.dbBase.GetStoredProcCommand("pg_usp_upd_deudaspagosimportados")

                    'parámetros de entrada        
                    dbBase.AddInParameter(dbCommand, "@p_concepto", DbType.String, dr.Item("concepto"))
                    dbBase.AddInParameter(dbCommand, "@p_talonario", DbType.String, dr.Item("tal"))
                    dbBase.AddInParameter(dbCommand, "@p_numero", DbType.String, dr.Item("numero"))
                    dbBase.AddInParameter(dbCommand, "@p_periodo", DbType.String, dr.Item("periodo"))
                    dbBase.AddInParameter(dbCommand, "@p_origen", DbType.String, dr.Item("origen"))
                    dbBase.AddInParameter(dbCommand, "@p_codigoalumno", DbType.String, dr.Item("codigo"))

                    If dr.Item("anulado") = "SI" Then
                        dbBase.AddInParameter(dbCommand, "@p_fechapago", DbType.Date, DBNull.Value)
                    Else
                        dbBase.AddInParameter(dbCommand, "@p_fechapago", DbType.Date, dr.Item("fechapago"))
                    End If

                    dbBase.AddInParameter(dbCommand, "@p_anulado", DbType.String, dr.Item("anulado"))
                    dbBase.AddInParameter(dbCommand, "@p_nombrealumno", DbType.String, dr.Item("alumno"))
                    dbBase.AddInParameter(dbCommand, "@p_monto", DbType.Decimal, dr.Item("monto"))
                    dbBase.AddInParameter(dbCommand, "@p_mora", DbType.Decimal, dr.Item("mora"))
                    dbBase.AddInParameter(dbCommand, "@p_total", DbType.Decimal, dr.Item("total"))

                    'dbbase.addinparameter(dbcommand, "@p_codigousuario", dbtype.int32, int_codigousuario)
                    'dbbase.addinparameter(dbcommand, "@p_codigotipousuario", dbtype.int32, int_codigotipousuario)
                    'dbbase.addinparameter(dbcommand, "@p_codigomodulo", dbtype.string, int_codigomodulo)
                    'dbbase.addinparameter(dbcommand, "@p_codigoopcion", dbtype.int32, int_codigoopcion)

                    'parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@p_valor", DbType.Int32, 10)
                    dbBase.AddOutParameter(dbCommand, "@p_mensaje", DbType.String, 255)

                    'ejecucion del store procedure
                    dbBase.ExecuteScalar(dbCommand, tran)
                    str_mensaje = dbBase.GetParameterValue(dbCommand, "@p_mensaje").ToString()
                    int_valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_valor")))

                    If Not int_valor >= 0 Then ' si ocurre algun error : int_valor (-1)
                        Rollback()
                        lstRegistro.Add("Error en el registro: " & dr.Item("numero") & " - Codigo pago: " & int_valor.ToString)
                        'Return int_valor
                        Return lstRegistro
                    Else
                        If int_valor = 0 Then ' no se actualizo el registro
                            lstRegistro.Add("No se actualizo el registro: " & dr.Item("numero") & " - Codigo pago: " & int_valor.ToString)
                        Else ' se actualizo el registro
                            lstRegistro.Add("Se actualizo registro: " & dr.Item("numero") & " - Codigo pago: " & int_valor.ToString)
                        End If
                        int_totalregistros += 1
                    End If

                Next
                Commit()
                'Return int_totalregistros 'int_valor
                Return lstRegistro 'int_valor

            Catch ex As Exception
                str_mensaje = "ocurrio un error durante el registro."
                lstRegistro.Add(str_mensaje)
                Rollback()
                Return lstRegistro
            Finally
                Conexion.Close()
            End Try

        End Function

#End Region

#End Region

#Region "Metodos No Transaccionales"

#Region "Impresion"

        '' Impresiones
        'Public Function FUN_LIS_PagosImprimir(ByVal int_CodigoTalonario As Integer, ByVal int_TipoConsulta As Integer, ByVal str_NumeroPagoInicial As String, ByVal str_NumeroPagoFinal As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Dim cmd As DbCommand

        '    '@p_TipoConsulta : 0 Pendiente de Pago / 1 Pago en Colegio / 2 Pago en Banco    
        '    If int_TipoConsulta > 0 Then

        '        If int_CodigoTalonario = 1 Then 'Boleta : 1
        '            cmd = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosImprimirBoleta")
        '        Else 'Factura : 2
        '            cmd = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosImprimirFactura")
        '        End If

        '        dbBase.AddInParameter(cmd, "@p_TipoConsulta", DbType.Int32, int_TipoConsulta)

        '    Else ' int_TipoConsulta = 0, Pagos generados por vencimiento de las deudas

        '        If int_CodigoTalonario = 1 Then 'Boleta : 1
        '            cmd = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosVencidosImprimirBoleta")
        '        Else 'Factura : 2
        '            cmd = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosVencidosImprimirFactura")
        '        End If

        '    End If

        '    'Parámetros de entrada
        '    dbBase.AddInParameter(cmd, "@p_NumeroPagoInicial", DbType.String, str_NumeroPagoInicial)
        '    dbBase.AddInParameter(cmd, "@p_NumeroPagoFinal", DbType.String, str_NumeroPagoFinal)

        '    dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
        '    dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

        '    'Ejecucion del Store Procedure
        '    Return dbBase.ExecuteDataSet(cmd)

        'End Function


        ' Lista de busqueda

        Public Function FUN_LIS_PagosModuloImpresion(ByVal int_EstadoEmision As Integer, ByVal int_CodigoTalonario As Integer, _
                                                     ByVal str_NumeroPagoInicial As String, ByVal str_NumeroPagoFinal As String, _
                                                     ByVal int_TipoFecha As Integer, ByVal dt_FechaInicial As Date, ByVal dt_FechaFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosModuloImpresionFiltrado")

            'Parámetros de entrada  
            dbBase.AddInParameter(cmd, "@p_EstadoEmision", DbType.Int32, int_EstadoEmision)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)
            dbBase.AddInParameter(cmd, "@p_NumeroPagoInicial", DbType.String, str_NumeroPagoInicial)
            dbBase.AddInParameter(cmd, "@p_NumeroPagoFinal", DbType.String, str_NumeroPagoFinal)

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

        'Public Function FUN_GET_PagoModuloImpresion(ByVal int_CodigoDocumento As Integer, ByVal int_CodigoTalonario As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_PagosModuloImpresion")

        '    'Parámetros de entrada
        '    dbBase.AddInParameter(cmd, "@p_CodigoDocumento", DbType.Int32, int_CodigoDocumento)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)

        '    dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
        '    dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

        '    'Ejecucion del Store Procedure
        '    Return dbBase.ExecuteDataSet(cmd)

        'End Function

        '' Impresion Factura
        'Public Function FUN_GET_PagoModuloImpresionFactura(ByVal int_CodigoDocumento As Integer, ByVal int_CodigoTalonario As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_PagosModuloImpresionFactura")

        '    'Parámetros de entrada
        '    dbBase.AddInParameter(cmd, "@p_CodigoDocumento", DbType.Int32, int_CodigoDocumento)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)

        '    dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
        '    dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

        '    'Ejecucion del Store Procedure
        '    Return dbBase.ExecuteDataSet(cmd)

        'End Function

        Public Function FUN_LIS_PagosModuloImpresionVarios(ByVal str_CodigoDocumento As String, ByVal int_CodigoTalonario As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_PagosModuloImpresionVarios")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoDocumento", DbType.String, str_CodigoDocumento)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

        Public Function FUN_LIS_Pagos(ByVal str_Nombre As String, ByVal int_CodigoTalonario As Integer, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicial As String, ByVal dt_FechaFinal As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_Pagos")

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

        Public Function FUN_LIS_Pagos(ByVal str_Nombre As String, _
            ByVal int_CodigoTalonario As Integer, ByVal str_NumeroRangoInicial As String, ByVal str_NumeroRangoFinal As String, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicial As String, ByVal dt_FechaFinal As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosGenerales")
            cmd.CommandTimeout = 360 ' 4 mins

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, str_Nombre)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)
            dbBase.AddInParameter(cmd, "@p_NumeroPagoInicial", DbType.String, str_NumeroRangoInicial)
            dbBase.AddInParameter(cmd, "@p_NumeroPagoFinal", DbType.String, str_NumeroRangoFinal)
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


        ' Listar Pagos para Nota de Crédito
        Public Function FUN_LIS_PagosParaNotaCredito( _
            ByVal int_TipoNota As Integer, ByVal int_CodigoTalonario As Integer, ByVal str_NumeroPago As String, ByVal int_CodigoTalonarioNota As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosParaNotaDeCredito")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_TipoNota", DbType.Int32, int_TipoNota)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)
            dbBase.AddInParameter(cmd, "@p_NumeroPago", DbType.String, str_NumeroPago)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonarioNota", DbType.Int32, int_CodigoTalonarioNota)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' Listar Pagos para Nota de Debito
        Public Function FUN_LIS_PagosParaNotaDebito(ByVal int_TipoNota As Integer, ByVal int_CodigoTalonario As Integer, ByVal str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosParaNotaDeDebito")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_TipoNota", DbType.Int32, int_TipoNota)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)
            dbBase.AddInParameter(cmd, "@p_NumeroPago", DbType.String, str_NumeroPago)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_LIS_PagosGeneral(ByVal int_CodigoTalonario As Integer, ByVal str_NumeroPago As String, _
            ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombres As String, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosGeneral")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)
            dbBase.AddInParameter(cmd, "@p_NumeroTalonario", DbType.String, str_NumeroPago)
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, str_ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, str_ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, str_Nombres)
            dbBase.AddInParameter(cmd, "@p_TipoFecha", DbType.Int32, int_TipoFecha)
            dbBase.AddInParameter(cmd, "@p_FechaInicial", DbType.Date, dt_FechaInicio)
            dbBase.AddInParameter(cmd, "@p_FechaFinal", DbType.Date, dt_FechaFin)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' User Control : Buscar Talonarios
        Public Function FUN_LIS_PagosGeneralParaNotas(ByVal int_CodigoConceptoAux As Integer, ByVal int_CodigoTalonario As Integer, ByVal str_NumeroPago As String, _
            ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombres As String, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosGeneral")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoConceptoAux", DbType.Int32, int_CodigoConceptoAux)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)
            dbBase.AddInParameter(cmd, "@p_NumeroTalonario", DbType.String, str_NumeroPago)
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, str_ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, str_ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, str_Nombres)
            dbBase.AddInParameter(cmd, "@p_TipoFecha", DbType.Int32, int_TipoFecha)
            dbBase.AddInParameter(cmd, "@p_FechaInicial", DbType.Date, dt_FechaInicio)
            dbBase.AddInParameter(cmd, "@p_FechaFinal", DbType.Date, dt_FechaFin)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function




        Public Function FUN_LIS_PagosEmitidosPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_Modulo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosEmitidos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_Modulo", DbType.Int32, int_Modulo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
        Public Function FUN_LIS_PagosEmitidosPorFamilia(ByVal str_CodigoFamilia As String, ByVal int_Modulo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosEmitidosPorFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.String, str_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_Modulo", DbType.Int32, int_Modulo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
        Public Function FUN_LIS_PagosEmitidosPorFamiliaFiltroAlumno(ByVal str_CodigoAlumno As String, ByVal int_Modulo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosEmitidosPorFamiliaFiltroAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_Modulo", DbType.Int32, int_Modulo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function



        Public Function FUN_GET_Pago(ByVal int_CodigoPago As Integer, ByVal int_CodigoTalonario As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_Pago")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPago", DbType.Int32, int_CodigoPago)
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int32, int_CodigoTalonario)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_PagosPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, ByVal int_TipoBusqueda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosPorAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_TipoBusqueda", DbType.Int32, int_TipoBusqueda)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_LIS_PagosPorAlumnoV2(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, ByVal int_TipoBusqueda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_PagosPorAlumno2")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_TipoBusqueda", DbType.Int32, int_TipoBusqueda)

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