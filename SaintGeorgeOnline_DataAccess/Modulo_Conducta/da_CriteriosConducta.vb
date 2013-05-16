Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Namespace ModuloConductaAlumnos
    Public Class da_CriteriosConducta
        Inherits InstanciaConexion.ManejadorConexion
#Region "Atributos"
        Private dbBase As SqlDatabase
        Private dbCommand As DbCommand
#End Region
#Region "Constructor"
        Public Sub New()
            dbBase = New SqlDatabase(SqlConexionDB)
        End Sub
#End Region
#Region "Transaccionales"
        Public Function FUN_DEL_CriteriosConducta(ByVal CodCritCond As Integer, ByRef str_Mensaje As String, _
                                                 ByVal CodigoUsuario As Integer, ByVal CodigoTipoUsuario As Integer, _
                                                 ByVal CodigoModulo As Integer, ByVal CodigoOpcion As Integer) As Integer
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_DEL_CriteriosConducta")
            'Parámetros de Entrada
            dbBase.AddInParameter(cmd, "@p_CodCritCond", DbType.Int16, CodCritCond)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, CodigoOpcion)
            'Parámetros de Salida
            dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 255)
            'Ejecución del Store Procedure
            dbBase.ExecuteScalar(cmd)
            str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString
            Return Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
        End Function
        Public Function FUN_INS_CriterioConducta(ByVal str_Descripcion As String, ByVal int_CodTipoCritCond As Integer, ByVal int_Puntaje As Integer, _
                                                 ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
                                                 ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByRef str_Mensaje As String) As Integer
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_INS_CriteriosConducta")
            'Parámetro de Entrada
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_CodTipoCritCond", DbType.Int32, int_CodTipoCritCond)
            dbBase.AddInParameter(cmd, "@p_Puntaje", DbType.Int32, int_Puntaje)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parámetro de Salida
            dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 255)
            'Ejecución del Store Procedure
            dbBase.ExecuteScalar(cmd)
            str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString
            Return Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
        End Function
        Public Function FUN_UPD_CriteriosConducta(ByVal int_CodCritCond As Integer, ByVal str_Descripcion As String, _
                                                  ByVal int_CodTipoCritCond As Integer, ByVal int_Puntaje As Integer, ByVal int_CodigoUsuario As Integer, _
                                                  ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, _
                                                  ByVal int_CodigoOpcion As Integer, ByRef str_Mensaje As String) As Integer
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_UPD_CriteriosConducta")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodCritCond", DbType.Int32, int_CodCritCond)
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_CodTipoCritCond", DbType.Int32, int_CodTipoCritCond)
            dbBase.AddInParameter(cmd, "@p_Puntaje", DbType.Int32, int_Puntaje)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parametros de Salida
            dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 255)
            dbBase.AddInParameter(cmd, "@p_Valor", DbType.Int32, 10)
            'Ejecución del Store Procedure
            dbBase.ExecuteScalar(cmd)
            str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString
            Return Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
        End Function
#End Region
#Region "No Transaccionales"
        Public Function FUN_LIS_CriterioXtipoCriterio(ByVal CodTipoCriterio As Integer, ByVal CodigoUsuario As Integer, _
                                                       ByVal CodigoTipoUsuario As Integer, ByVal CodigoModulo As Integer, _
                                                       ByVal CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_LIS_CriterioXtipoCriterio")
            'Parametros de Entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTipoCriterio", DbType.Int16, CodTipoCriterio)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, CodigoOpcion)
            Return dbBase.ExecuteDataSet(cmd)
        End Function
        Public Function FUN_GET_CriterioXtipoCriterioByVal(ByVal CodigoCriterioConducta As Integer, ByVal CodigoUsuario As Integer, _
                                                       ByVal CodigoTipoUsuario As Integer, ByVal CodigoModulo As Integer, _
                                                       ByVal CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CT_USP_GET_CriterioXtipoCriterio")
            dbBase.AddInParameter(cmd, "@p_CodigoCriterioConducta", DbType.Int16, CodigoCriterioConducta)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, CodigoOpcion)
            Return dbBase.ExecuteDataSet(cmd)
        End Function
#End Region
    End Class
End Namespace

