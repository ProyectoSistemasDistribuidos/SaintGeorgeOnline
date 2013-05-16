Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_SubIndicadores
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

        Public Function FUN_INS_Subindicadores(ByVal str_DescripcionSubIndicador As String, ByVal int_CodigoIndicador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim lstRes As New List(Of String)
            Try

                '@P_CodigoIndicador INT ,
                '@p_Descripcion varchar(200),
                '@p_Mensaje VARCHAR(255) OUTPUT,          
                '@p_Valor INT OUTPUT  ,
                '@p_CodigoUsuario  INT ,
                '@p_CodigoTipoUsuario  INT,
                '@p_CodigoModulo  INT,
                '@p_CodigoOpcion  INT
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_SubIndicadores")
                dbBase.AddInParameter(cmd, "@P_CodigoIndicador", DbType.Int32, int_CodigoIndicador)
                dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_DescripcionSubIndicador)
                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd)
                lstRes.Add(dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString())
                lstRes.Add(Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor"))).ToString())
                Return lstRes
            Catch ex As Exception

            End Try
        End Function

        Public Function FUN_UDP_Subindicadores(ByVal str_DescripcionSubIndicador As String, ByVal int_CodigoSubIndicador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
            Dim lstRes As New List(Of String)
            Try

                '@P_CodigoSubIndicador INT ,
                '@p_Descripcion varchar(200),

                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UDP_SubIndicadores")
                dbBase.AddInParameter(cmd, "@P_CodigoSubIndicador", DbType.Int32, int_CodigoSubIndicador)
                dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_DescripcionSubIndicador)


                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd)
                lstRes.Add(dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString())
                lstRes.Add(Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor"))).ToString())
                Return lstRes
            Catch ex As Exception

            End Try
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SubIndicadores(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_SubIndicadores")

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

        Public Function FUN_LIS_SubIndicadoresPorIndicador(ByVal int_CodigoIndicador As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_SubIndicadoresPorIndicador")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoIndicador", DbType.Int16, int_CodigoIndicador)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)

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