﻿Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros

Namespace ModuloBancoLibros

    Public Class da_CopiaLibros

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

        Public Function FUN_INS_CopiaLibros(ByVal objCopiaLibros As be_CopiaLibros, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_INS_CopiaLibros")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoLibro", DbType.Int32, objCopiaLibros.CodigoLibro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBarra", DbType.String, objCopiaLibros.CodigoBarra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEjemplar", DbType.String, objCopiaLibros.CodigoEjemplar)

            dbBase.AddInParameter(dbCommand, "@p_NumeroPago", DbType.String, objCopiaLibros.NumeroPago)
            dbBase.AddInParameter(dbCommand, "@p_FechaCompra", DbType.Date, objCopiaLibros.FechaCompraDt)

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

        Public Function FUN_UPD_CopiaLibros(ByVal objCopiaLibros As be_CopiaLibros, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_UPD_CopiaLibros")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, objCopiaLibros.CodigoCopiaLibro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoLibro", DbType.Int32, objCopiaLibros.CodigoLibro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBarra", DbType.String, objCopiaLibros.CodigoBarra)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEjemplar", DbType.String, objCopiaLibros.CodigoEjemplar)

            dbBase.AddInParameter(dbCommand, "@p_NumeroPago", DbType.String, objCopiaLibros.NumeroPago)
            dbBase.AddInParameter(dbCommand, "@p_FechaCompra", DbType.Date, objCopiaLibros.FechaCompraDt)

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

        Public Function FUN_DEL_CopiaLibros(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_DEL_CopiaLibros")
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
        'ok
        'Public Function FUN_LIS_Libros(ByVal int_AnioAcademico As Integer, ByVal str_Titulo As String, ByVal int_CodigoIdioma As Integer, ByVal str_ISBN As String, _
        '                               ByVal int_CodigoGrado As Integer, ByVal int_CodigoTipoReporte As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_LIS_Libros")
        '    'Parámetros de entrada
        '    dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
        '    dbBase.AddInParameter(cmd, "@p_Titulo", DbType.String, str_Titulo)
        '    dbBase.AddInParameter(cmd, "@p_CodigoIdioma", DbType.Int16, int_CodigoIdioma)
        '    dbBase.AddInParameter(cmd, "@p_ISBN", DbType.String, str_ISBN)
        '    dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTipoReporte", DbType.Int16, int_CodigoTipoReporte)

        '    dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
        '    dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
        '    'Ejecucion del Store Procedure
        '    Return dbBase.ExecuteDataSet(cmd)

        'End Function

        Public Function FUN_GET_CopiaLibros(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_GET_CopiaLibros")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoLibro", DbType.Int16, int_Codigo)

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