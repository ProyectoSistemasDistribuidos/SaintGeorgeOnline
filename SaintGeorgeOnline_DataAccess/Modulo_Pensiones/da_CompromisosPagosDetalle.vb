Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

'j

Namespace ModuloPensiones

    Public Class da_CompromisosPagosDetalle

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

        Public Function FUN_INS_DetalleCompromisoPago(ByVal CompromisosPagosDetalle As be_CompromisosPagosDetalle, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try

                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_DetalleCompromisoPago")
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_codigoCompromisoPago", DbType.Int32, CompromisosPagosDetalle.CodigoCompromisoPago)
                dbBase.AddInParameter(dbCommand, "@p_codigoAlumno", DbType.String, CompromisosPagosDetalle.CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_codigoDeuda", DbType.Int32, CompromisosPagosDetalle.CodigoDeuda)
                dbBase.AddInParameter(dbCommand, "@p_FechaPagoDeuda", DbType.DateTime, CompromisosPagosDetalle.FechaPagoDeuda)
                dbBase.AddInParameter(dbCommand, "@p_Estado", DbType.Int32, CompromisosPagosDetalle.Estado)

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


        Public Function FUN_UPD_DetalleCompromisoPago(ByVal CompromisosPagosDetalle As be_CompromisosPagosDetalle, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try

                dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_DetalleCompromisoPago")
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_codigoDetalleCompromisoPago", DbType.Int32, CompromisosPagosDetalle.CodigoDetalleCompromisoPago)
                dbBase.AddInParameter(dbCommand, "@p_codigoCompromisoPago", DbType.Int32, CompromisosPagosDetalle.CodigoCompromisoPago)
                dbBase.AddInParameter(dbCommand, "@p_codigoAlumno", DbType.String, CompromisosPagosDetalle.CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_codigoDeuda", DbType.Int32, CompromisosPagosDetalle.CodigoDeuda)
                dbBase.AddInParameter(dbCommand, "@p_FechaPagoDeuda", DbType.DateTime, CompromisosPagosDetalle.FechaPagoDeuda)
                dbBase.AddInParameter(dbCommand, "@p_Estado", DbType.Int32, CompromisosPagosDetalle.Estado)

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

        Public Function FUN_DEL_DetalleCompromisoPago(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_DEL_DetalleCompromisoPago")
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

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_DetalleCompromisos(ByVal int_CodigoFamiliar As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_FamiliaresXAlumnosDetalleCompromisoPago")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int32, int_CodigoFamiliar)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DetalleCompromisosPagoXCP(ByVal int_CodigoCP As Integer, _
                                               ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_DetalleCompromisosPagoXCP")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoCompromisoPago", DbType.Int32, int_CodigoCP)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_GET_ConceptosDetalleCompromisoPago(ByVal int_AnioAcademico As Integer, ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_ConceptosDetalleCompromisoPago")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.String, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_Codigo)

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