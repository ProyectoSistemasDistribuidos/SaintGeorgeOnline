Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones

Namespace ModuloPensiones

    Public Class da_ReportesWord

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

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ReporteWord(ByVal int_CodigoAnio As Integer, ByVal str_CodigoAlumno As String, ByVal str_CodigoFamilia As String, _
                                                  ByVal dt_Fecha As Date, ByVal int_TipoReporte As Integer, ByVal int_CantidadDeudas As Integer, _
                                                 ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_CartaMorosidadXMes")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAnio", DbType.Int16, int_CodigoAnio)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.String, str_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_Fecha", DbType.String, dt_Fecha.ToShortDateString)
            dbBase.AddInParameter(cmd, "@p_TipoReporte", DbType.Int16, int_TipoReporte)
            dbBase.AddInParameter(cmd, "@p_CantidadDeudas", DbType.Int16, int_CantidadDeudas)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_ReportesWordPorGrados( _
            ByVal int_CodigoAnio As Integer, ByVal str_CodigoAlumno As String, ByVal str_CodigoFamilia As String, _
            ByVal int_CodigoGradoIni As Integer, ByVal int_CodigoGradoFin As Integer, _
            ByVal dt_Fecha As Date, ByVal int_TipoReporte As Integer, ByVal int_CantidadDeudas As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_CartaMorosidadXMesYGrado")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAnio", DbType.Int16, int_CodigoAnio)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.String, str_CodigoFamilia)


            dbBase.AddInParameter(cmd, "@p_CodigoGradoIni", DbType.Int16, int_CodigoGradoIni)
            dbBase.AddInParameter(cmd, "@p_CodigoGradoFin", DbType.Int16, int_CodigoGradoFin)


            dbBase.AddInParameter(cmd, "@p_Fecha", DbType.String, dt_Fecha.ToShortDateString)
            dbBase.AddInParameter(cmd, "@p_TipoReporte", DbType.Int16, int_TipoReporte)
            dbBase.AddInParameter(cmd, "@p_CantidadDeudas", DbType.Int16, int_CantidadDeudas)

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