Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Namespace ModuloPensiones

    Public Class da_Empresas

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

        Public Function FUN_INS_Empresas(ByVal objEmpresas As be_Empresas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_INS_Empresas")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeo", DbType.String, objEmpresas.CodigoUbigeo)
            dbBase.AddInParameter(dbCommand, "@p_RazonSocial", DbType.String, objEmpresas.RazonSocial)
            dbBase.AddInParameter(dbCommand, "@p_NombreComercial", DbType.String, objEmpresas.NombreComercial)
            dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, objEmpresas.Direccion)
            dbBase.AddInParameter(dbCommand, "@p_Ruc", DbType.String, objEmpresas.Ruc)
            dbBase.AddInParameter(dbCommand, "@p_Telefono", DbType.String, objEmpresas.Telefono)
            dbBase.AddInParameter(dbCommand, "@p_Celular", DbType.String, objEmpresas.Celular)
            dbBase.AddInParameter(dbCommand, "@p_Fax", DbType.String, objEmpresas.Fax)
            dbBase.AddInParameter(dbCommand, "@p_Email", DbType.String, objEmpresas.Email)

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

        Public Function FUN_UPD_Empresas(ByVal objEmpresas As be_Empresas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_UPD_Empresas")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objEmpresas.CodigoEmpresa)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeo", DbType.String, objEmpresas.CodigoUbigeo)
            dbBase.AddInParameter(dbCommand, "@p_RazonSocial", DbType.String, objEmpresas.RazonSocial)
            dbBase.AddInParameter(dbCommand, "@p_NombreComercial", DbType.String, objEmpresas.NombreComercial)
            dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, objEmpresas.Direccion)
            dbBase.AddInParameter(dbCommand, "@p_Ruc", DbType.String, objEmpresas.Ruc)
            dbBase.AddInParameter(dbCommand, "@p_Telefono", DbType.String, objEmpresas.Telefono)
            dbBase.AddInParameter(dbCommand, "@p_Celular", DbType.String, objEmpresas.Celular)
            dbBase.AddInParameter(dbCommand, "@p_Fax", DbType.String, objEmpresas.Fax)
            dbBase.AddInParameter(dbCommand, "@p_Email", DbType.String, objEmpresas.Email)

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

        Public Function FUN_DEL_Empresas(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PG_USP_DEL_Empresas")
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

        Public Function FUN_LIS_Empresas(ByVal str_RazonSocial As String, ByVal str_NombreComercial As String, ByVal str_Ruc As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_Empresas")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_RazonSocial", DbType.String, str_RazonSocial)
            dbBase.AddInParameter(cmd, "@p_NombreComercial", DbType.String, str_NombreComercial)
            dbBase.AddInParameter(cmd, "@p_Ruc", DbType.String, str_Ruc)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)


        End Function

        Public Function FUN_GET_Empresas(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_GET_Empresas")
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

