Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloFotocopia

Namespace ModuloFotocopias

    Public Class da_DetalleSolicitudFotocopias
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

        Public Function FUN_INS_DetalleSolicitudFotocopias(ByVal objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_INS_DetalleSolicitudFotocopias")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitudFotocopia", DbType.Int32, objDetalleSolicitudFotocopias.CodigoSolicitudFotocopia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, objDetalleSolicitudFotocopias.CodigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_Estado", DbType.Int32, objDetalleSolicitudFotocopias.Estado)
            dbBase.AddInParameter(dbCommand, "@p_NumeroCopias", DbType.Int32, objDetalleSolicitudFotocopias.NumeroCopias)
            dbBase.AddInParameter(dbCommand, "@p_Tema", DbType.String, objDetalleSolicitudFotocopias.Tema)
            dbBase.AddInParameter(dbCommand, "@p_FechaImpresion", DbType.Date, objDetalleSolicitudFotocopias.FechaImpresion)
            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objDetalleSolicitudFotocopias.Observacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int32, objDetalleSolicitudFotocopias.CodigoCurso)
           
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

        Public Function FUN_UPD_DetalleSolicitudFotocopias(ByVal objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_UPD_DetalleSolicitudFotocopias")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudFotocopia", DbType.Int32, objDetalleSolicitudFotocopias.CodigoDetalleSolicitudFotocopia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitudFotocopia", DbType.Int32, objDetalleSolicitudFotocopias.CodigoSolicitudFotocopia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, objDetalleSolicitudFotocopias.CodigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_Estado", DbType.Int32, objDetalleSolicitudFotocopias.Estado)
            dbBase.AddInParameter(dbCommand, "@p_NumeroCopias", DbType.Int32, objDetalleSolicitudFotocopias.NumeroCopias)
            dbBase.AddInParameter(dbCommand, "@p_Tema", DbType.String, objDetalleSolicitudFotocopias.Tema)
            dbBase.AddInParameter(dbCommand, "@p_FechaImpresion", DbType.Date, objDetalleSolicitudFotocopias.FechaImpresion)
            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objDetalleSolicitudFotocopias.Observacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int32, objDetalleSolicitudFotocopias.CodigoCurso)

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

        Public Function FUN_DEL_DetalleSolicitudFotocopias(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_DEL_DetalleSolicitudFotocopias")
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

        Public Function FUN_LIS_DetalleSolicitudFotocopias(ByVal str_Fecha As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("FC_USP_LIS_DetalleSolicitudFotocopias")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Fecha", DbType.Date, str_Fecha)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Public Function FUN_GET_Ambientes(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
        'Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_GET_Ambientes")
        ''Parámetros de entrada
        '    dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

        '    dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
        '    dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
        ''Ejecucion del Store Procedure
        '    Return dbBase.ExecuteDataSet(cmd)
        'End Function

#End Region


    End Class

End Namespace