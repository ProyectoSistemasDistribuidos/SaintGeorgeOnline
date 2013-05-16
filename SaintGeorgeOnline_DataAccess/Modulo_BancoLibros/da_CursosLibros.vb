Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Namespace ModuloBancoLibros
    Public Class da_CursosLibros
        Inherits InstanciaConexion.ManejadorConexion
#Region "Atributos"
        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar
#End Region
#Region "Constructor"
        Public Sub New()
            dbBase = New SqlDatabase(SqlConexionDB)
        End Sub
#End Region
#Region "Metodo no Transaccionales"
        Public Function FUN_LIST_CursosLibros(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUusario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_LIS_CursosLibros")
            'Parametros de Entrada
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUusario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecución del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function
#End Region
    End Class
End Namespace

