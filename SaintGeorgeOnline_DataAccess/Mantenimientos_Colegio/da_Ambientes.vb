Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio

Namespace ModuloColegio

    Public Class da_Ambientes
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

        Public Function FUN_INS_Ambientes(ByVal objAmbientes As be_Ambientes, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_INS_Ambientes")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, objAmbientes.CodigoSede)
            dbBase.AddInParameter(dbCommand, "@p_NombreAmbiente", DbType.String, objAmbientes.NombreAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoAmbiente", DbType.Int16, objAmbientes.CodigoTipoAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBloque", DbType.Int16, objAmbientes.CodigoPabellon)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPiso", DbType.Int16, objAmbientes.CodigoPiso)
            dbBase.AddInParameter(dbCommand, "@p_Referencia", DbType.String, objAmbientes.Referencia)
            dbBase.AddInParameter(dbCommand, "@p_Capacidad", DbType.Int16, objAmbientes.Capacidad)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoAmbienteProyecto", DbType.Int16, objAmbientes.CodigoTipoAmbienteProyecto)
            dbBase.AddInParameter(dbCommand, "@p_Reservable", DbType.Int16, objAmbientes.Reservable)
            dbBase.AddInParameter(dbCommand, "@p_Multimedia", DbType.Int16, objAmbientes.Multimedia)
            dbBase.AddInParameter(dbCommand, "@p_AreaAmbiente", DbType.String, objAmbientes.AreaAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlfanumerico", DbType.String, objAmbientes.CodigoAlfanumerico)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_Ambientes(ByVal objAmbientes As be_Ambientes, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_UPD_Ambientes")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objAmbientes.CodigoAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, objAmbientes.CodigoSede)
            dbBase.AddInParameter(dbCommand, "@p_NombreAmbiente", DbType.String, objAmbientes.NombreAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoAmbiente", DbType.Int16, objAmbientes.CodigoTipoAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBloque", DbType.Int16, objAmbientes.CodigoPabellon)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPiso", DbType.Int16, objAmbientes.CodigoPiso)
            dbBase.AddInParameter(dbCommand, "@p_Referencia", DbType.String, objAmbientes.Referencia)
            dbBase.AddInParameter(dbCommand, "@p_Capacidad", DbType.Int16, objAmbientes.Capacidad)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoAmbienteProyecto", DbType.Int16, objAmbientes.CodigoTipoAmbienteProyecto)
            dbBase.AddInParameter(dbCommand, "@p_Reservable", DbType.Int16, objAmbientes.Reservable)
            dbBase.AddInParameter(dbCommand, "@p_Multimedia", DbType.Int16, objAmbientes.Multimedia)
            dbBase.AddInParameter(dbCommand, "@p_AreaAmbiente", DbType.String, objAmbientes.AreaAmbiente)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlfanumerico", DbType.String, objAmbientes.CodigoAlfanumerico)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_DEL_Ambientes(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_DEL_Ambientes")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

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

        Public Function FUN_LIS_Ambientes(ByVal int_CodigoSede As Integer, ByVal str_Nombre As String, ByVal int_CodigoTipoAmbiente As Integer, ByVal int_CodigoPabellon As Integer, ByVal int_CodigoPiso As Integer, ByVal int_Reservable As Integer, ByVal int_Multimedia As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_Ambientes")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int16, int_CodigoSede)
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, str_Nombre)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoAmbiente", DbType.Int16, int_CodigoTipoAmbiente)
            dbBase.AddInParameter(cmd, "@p_CodigoPabellon", DbType.Int16, int_CodigoPabellon)
            dbBase.AddInParameter(cmd, "@p_CodigoPiso", DbType.Int16, int_CodigoPiso)
            dbBase.AddInParameter(cmd, "@p_Reservable", DbType.Int16, int_Reservable)
            dbBase.AddInParameter(cmd, "@p_Multimedia", DbType.Int16, int_Multimedia)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_Ambientes(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_GET_Ambientes")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

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