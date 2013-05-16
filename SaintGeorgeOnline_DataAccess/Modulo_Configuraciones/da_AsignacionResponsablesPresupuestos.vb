Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class da_AsignacionResponsablesPresupuestos

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

        Public Function FUN_UPD_AsignacionResponsablesPresupuestos(ByVal objAsistenciaAlumnos As be_AsignacionResponsablesPresupuestos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_AsignacionResponsablesPresupuestos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, objAsistenciaAlumnos.CodigoAsignacionResponsable)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajor", DbType.Int32, objAsistenciaAlumnos.CodigoTrabajador)
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

        Public Function FUN_LIS_AsignacionResponsablesPresupuestos(ByVal int_Sede As Integer, ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoCentrosCostos As Integer, ByVal int_CodigoSubCentrosCostos As Integer, ByVal int_CodigoSubSubCentrosCostos As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_AsignacionResponsablesPresupuestos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int32, int_Sede)
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)
            dbBase.AddInParameter(cmd, "@p_CodigoCentrosCostos", DbType.Int32, int_CodigoCentrosCostos)
            dbBase.AddInParameter(cmd, "@p_CodigoSubCentrosCostos", DbType.Int32, int_CodigoSubCentrosCostos)
            dbBase.AddInParameter(cmd, "@p_CodigoSubSubCentrosCostos", DbType.Int32, int_CodigoSubSubCentrosCostos)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function


        Public Function FUN_LIS_Trabajadores(ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_Trabajadores")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, str_ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, str_ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, str_Nombre)
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

