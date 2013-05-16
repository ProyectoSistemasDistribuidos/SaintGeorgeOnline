Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo

'28-06-2011
Namespace ModuloLogueo

    Public Class da_Logueo
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

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_PermisosUsuario(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("LG_USP_GET_PermisosAccesoUsuario")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_VAL_PermisosUsuario(ByVal str_Usuario As String, ByVal str_Contrasenia As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("LG_USP_VAL_PermisosAccesoUsuario")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_Usuario)
            dbBase.AddInParameter(cmd, "@p_Contrasenia", DbType.String, str_Contrasenia)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_EmailUsuario(ByVal str_Usuario As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataTable

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("LG_USP_GET_EmailUsuario")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_Usuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd).Tables(0)

        End Function

        Public Function FUN_GET_EmailDNI(ByVal str_Usuario As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataTable

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("LG_USP_GET_EmailDNI")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_DNI", DbType.String, str_Usuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd).Tables(0)

        End Function

        Public Function FUN_VAL_PermisosSuperUsuario(ByVal int_CodigoUsuario As String, ByVal str_ContraseniaSuperUsuario As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("LG_USP_VAL_PermisosAccesoSuperUsuario")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_Contrasenia", DbType.String, str_ContraseniaSuperUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_DatosUsuarioReferencia(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("LG_USP_GET_DatosUsuarioReferencia")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_DinamicoUsuariosEnElSistema(ByVal str_FechaInicio As String, ByVal str_FechaFin As String, ByVal int_TipoPersona As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AU_USP_REP_UsuariosEnElSistema")
            'Parametros de Entrada
            dbBase.AddInParameter(cmd, "@p_FechaInicio", DbType.String, str_FechaInicio)
            dbBase.AddInParameter(cmd, "@p_FechaFin", DbType.String, str_FechaFin)
            dbBase.AddInParameter(cmd, "@p_CodTipoUsuario", DbType.Int16, int_TipoPersona)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecución del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_PresupuestosUsuario(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal codAnioPeridico As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("LG_USP_GET_PresupuestosUsuario")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            dbBase.AddInParameter(cmd, "@PP_CodigoPeriodo", DbType.Int32, codAnioPeridico)
            '@PP_CodigoPeriodo
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_AccesoUsuario(ByVal objUsuario As be_Usuario, ByRef str_Mensaje As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("LG_USP_INS_Acceso")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, objUsuario.CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_TipoUsuario", DbType.Int32, objUsuario.TipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_IpUsuario", DbType.String, objUsuario.IpUsuario)
            dbBase.AddInParameter(dbCommand, "@p_HostUsuario", DbType.String, objUsuario.HostUsuario)
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

        Public Function FUN_INS_AccesoUsuarioDetalle(ByVal int_CodigoSession As Integer, ByVal int_CodigoModulo As Integer, ByRef int_CodigoSubBloque As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer) As Integer
            Dim result As Integer = 0

            dbCommand = Me.dbBase.GetStoredProcCommand("LG_USP_INS_AccesoDetalle")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAcceso", DbType.Int32, int_CodigoSession)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSubBloque", DbType.Int32, int_CodigoSubBloque)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)


            'Ejecucion del Store Procedure
            result = dbBase.ExecuteScalar(dbCommand)
            Return result
        End Function

        Public Function FUN_INS_ClaveSuperUsuario(ByVal int_CodigoUsuario As Integer, ByVal str_Contrasenia As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Dim int_resultado As Integer = -1

            dbCommand = Me.dbBase.GetStoredProcCommand("LG_USP_INS_ClaveSuperUsuario")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_Contrasenia", DbType.String, str_Contrasenia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            int_resultado = dbBase.ExecuteScalar(dbCommand)

            Return int_resultado
        End Function

#End Region

    End Class

End Namespace

