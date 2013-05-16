Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class da_AsignacionArticulosPeriodo
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

        Public Function FUN_INS_AsignacionArticulosPeriodo(ByVal objAsignacionArticulosPeriodo As be_AsignacionArticulosPeriodo, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_INS_AsignacionArticulosPeriodo")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraSubCategoria", DbType.Int32, objAsignacionArticulosPeriodo.CodigoEstructuraSubCategoria)
            dbBase.AddInParameter(dbCommand, "@p_CodigoItem", DbType.Int32, objAsignacionArticulosPeriodo.CodigoItem)
            dbBase.AddInParameter(dbCommand, "@p_Precio", DbType.Decimal, IIf(objAsignacionArticulosPeriodo.PrecioArticulo = 0, DBNull.Value, objAsignacionArticulosPeriodo.PrecioArticulo))
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, IIf(objAsignacionArticulosPeriodo.CodigoMoneda = 0, DBNull.Value, objAsignacionArticulosPeriodo.CodigoMoneda))
            dbBase.AddInParameter(dbCommand, "@p_Unidad", DbType.String, IIf(objAsignacionArticulosPeriodo.Unidad.Length = 0, DBNull.Value, objAsignacionArticulosPeriodo.Unidad))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_AsignacionArticulosPeriodo(ByVal objAsignacionArticulosPeriodo As be_AsignacionArticulosPeriodo, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_AsignacionArticulosPeriodo")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraArticulo", DbType.Int32, objAsignacionArticulosPeriodo.CodigoEstructuraArticulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraSubCategoria", DbType.Int32, objAsignacionArticulosPeriodo.CodigoEstructuraSubCategoria)
            dbBase.AddInParameter(dbCommand, "@p_CodigoItem", DbType.Int32, objAsignacionArticulosPeriodo.CodigoItem)
            dbBase.AddInParameter(dbCommand, "@p_Precio", DbType.Decimal, IIf(objAsignacionArticulosPeriodo.PrecioArticulo = 0, DBNull.Value, objAsignacionArticulosPeriodo.PrecioArticulo))
            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int32, IIf(objAsignacionArticulosPeriodo.CodigoMoneda = 0, DBNull.Value, objAsignacionArticulosPeriodo.CodigoMoneda))
            dbBase.AddInParameter(dbCommand, "@p_Unidad", DbType.String, IIf(objAsignacionArticulosPeriodo.Unidad.Length = 0, DBNull.Value, objAsignacionArticulosPeriodo.Unidad))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_DEL_AsignacionArticulosPeriodo(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_DEL_AsignacionArticulosPeriodo")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionArticulosPeriodoPorSubCategoria(ByVal int_CodigoAsignacionSubCategoria As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_AsignacionArticulosPeriodoPorSubCategoria")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionSubCategoria", DbType.Int32, int_CodigoAsignacionSubCategoria)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace
