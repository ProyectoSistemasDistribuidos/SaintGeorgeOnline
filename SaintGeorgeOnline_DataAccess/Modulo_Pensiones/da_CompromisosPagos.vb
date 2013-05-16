Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

Namespace ModuloPensiones
    Public Class da_CompromisosPagos
        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar

#End Region

#Region "Constructor"

        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
        End Sub

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_CompromisoPago(ByVal objCompromisosPagos As be_CompromisosPagos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try

                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_CompromisoPago")
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_codigoFamilia", DbType.String, objCompromisosPagos.CodigoFamilia)
                dbBase.AddInParameter(dbCommand, "@p_codigoFamiliar", DbType.Int32, objCompromisosPagos.CodigoFamiliar)
                dbBase.AddInParameter(dbCommand, "@p_FechaEmisionCompromisoPago", DbType.DateTime, objCompromisosPagos.FechaEmisionCompromisoPago)

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

            Catch ex As Exception
                str_Mensaje = "Ocurrió un error, intente su operación otra vez."
                Return -1
            End Try

        End Function

        Public Function FUN_UPD_CompromisoPago(ByVal objCompromisosPagos As be_CompromisosPagos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_CompromisoPago")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_codigoCompromisoPago", DbType.String, objCompromisosPagos.CodigoCompromisoPago)
            dbBase.AddInParameter(dbCommand, "@p_codigoFamilia", DbType.String, objCompromisosPagos.CodigoFamilia)
            dbBase.AddInParameter(dbCommand, "@p_codigoFamiliar", DbType.Int32, objCompromisosPagos.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_FechaEmisionCompromisoPago", DbType.DateTime, objCompromisosPagos.FechaEmisionCompromisoPago)

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

        Public Function FUN_DEL_CompromisosPagos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_DEL_CompromisoPago")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Codigo)

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
        'ok
#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_CompromisoPagoXFamiliar(ByVal int_CodigoFamiliar As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_CompromisosPagoFamiliarXAlumno")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int32, int_CodigoFamiliar)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_CompromisoPago(ByVal int_CodigoFamilia As Integer, _
                                                ByVal int_CodigoFamiliar As Integer, _
                                                ByVal dt_FechaRegistroCPIni As Date, _
                                                ByVal dt_FechaRegistroCPFin As Date, _
                                               ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_CompromisosPago")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int32, int_CodigoFamiliar)
            dbBase.AddInParameter(cmd, "@p_FechaRegistroCPIni", DbType.DateTime, dt_FechaRegistroCPIni)
            dbBase.AddInParameter(cmd, "@p_FechaRegistroCPFin", DbType.DateTime, dt_FechaRegistroCPFin)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_CompromisosPagos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_CompromisoPago")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_CompromisoPagoXFamiliaFamiliar(ByVal int_CodigoFamilia As Integer, _
                                                               ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, _
                                                               ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, _
                                                               ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_CompromisoPagoXCodFamiliaFamiliar")
            'Parámetro de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int32, int_CodigoFamiliar)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecución del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function
#End Region

    End Class
End Namespace

