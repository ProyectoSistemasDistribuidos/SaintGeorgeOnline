﻿Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class da_TiposControles
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

        Public Function FUN_INS_TipoControl(ByVal objTipoControl As be_TiposControles, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_TiposControles")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objTipoControl.Descripcion)
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

        Public Function FUN_UPD_TipoControl(ByVal objTipoControl As be_TiposControles, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_UPD_TiposControles")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objTipoControl.CodigoTipoControl)
            dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objTipoControl.Descripcion)
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

        Public Function FUN_DEL_TipoControl(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_TiposControles")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_Codigo)
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

        Public Function FUN_LIS_TipoControl(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_TiposControles")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_TipoControl(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_GET_TiposControles")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

#End Region

    End Class

End Namespace
