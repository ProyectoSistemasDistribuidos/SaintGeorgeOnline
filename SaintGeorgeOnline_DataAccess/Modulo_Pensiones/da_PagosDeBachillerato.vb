﻿Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

Namespace ModuloPensiones

    Public Class da_PagosDeBachillerato
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

        'Pagos por Descarga de Banco
        Public Function FUN_UPD_PagosDeBachillerato(ByVal int_CodigoDeuda As String, _
                                                ByVal dt_FechaPago As Date, _
                                                ByVal de_MontoPago As Decimal, _
                                                ByVal dt_FechaEmision As Date, _
                                                ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_PagosDeBachillerato")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDeuda", DbType.Int32, int_CodigoDeuda)
            dbBase.AddInParameter(dbCommand, "@p_FechaPago", DbType.Date, dt_FechaPago)
            dbBase.AddInParameter(dbCommand, "@p_MontoPago", DbType.Decimal, de_MontoPago)
            dbBase.AddInParameter(dbCommand, "@p_FechaEmision", DbType.Date, dt_FechaEmision)

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

        Public Function FUN_LIS_InformacionPagosBachillerato(ByVal str_CodigoAlumno As String, _
                                                             ByVal int_CodigoMoneda As Integer, _
                                                             ByVal dt_FechaVencimiento As Date, _
                                                             ByVal de_Monto As Decimal, _
                                                             ByVal str_descripcion As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_InformacionPagosDeBachillerato")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoMoneda", DbType.Int32, int_CodigoMoneda)
            dbBase.AddInParameter(cmd, "@p_FechaVencimiento", DbType.String, dt_FechaVencimiento)
            dbBase.AddInParameter(cmd, "@p_Monto", DbType.Decimal, de_Monto)
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_descripcion)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_DinamicoInformacionPagosBachillerato(ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_REP_InformacionPagosBachillerato")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_PeriodoAcademico", DbType.Int32, int_CodigoPeriodoAcademico)

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