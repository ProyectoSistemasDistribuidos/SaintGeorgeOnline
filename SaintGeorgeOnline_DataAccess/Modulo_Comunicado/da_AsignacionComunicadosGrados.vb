﻿Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloComunicado

Namespace ModuloComunicado

    Public Class da_AsignacionComunicadosGrados

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

        Public Function FUN_INS_AsignacionComunicadosGrados(ByVal objAsignacionComunicadosGrados As be_AsignacionComunicadosGrados, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CM_USP_INS_AsignacionComunicadosGrados")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoComunicado", DbType.Int32, objAsignacionComunicadosGrados.CodigoComunicado)
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, objAsignacionComunicadosGrados.CodigoGrado)
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

        Public Function FUN_UPD_AsignacionComunicadosGrados(ByVal objAsignacionComunicadosGrados As be_AsignacionComunicadosGrados, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CM_USP_UPD_AsignacionComunicadosGrados")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, objAsignacionComunicadosGrados.CodigoAsignacionComunicadoGrados)
            dbBase.AddInParameter(dbCommand, "@p_CodigoComunicado", DbType.Int32, objAsignacionComunicadosGrados.CodigoComunicado)
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, objAsignacionComunicadosGrados.CodigoGrado)

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

        Public Function FUN_DEL_AsignacionComunicadosGrados(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CM_USP_DEL_AsignacionComunicadosGrados")
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

#End Region

    End Class

End Namespace

