Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.Common
Namespace ModuloConductaAlumnos
    Public Class da_TipoCriterioConductual
        Inherits InstanciaConexion.ManejadorConexion
#Region "Atributos"
        Private dbBases As SqlDatabase
        Private dbCommand As DbCommand
#End Region
#Region "Constructor"
        Public Sub New()
            dbBases = New SqlDatabase(SqlConexionDB)
        End Sub
#End Region
#Region "Region Transaccionales"

#End Region
#Region "Region No Transaccionales"
        Public Function FUN_LIS_TipoCriterioConductual(ByVal CodigoUsuario As Integer, ByVal CodigoTipoUsuario As Integer, _
                                                        ByVal CodigoModulo As Integer, ByVal CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBases.GetStoredProcCommand("CT_USP_LIS_TipoCriterioConductual")
            dbBases.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, CodigoUsuario)
            dbBases.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, CodigoTipoUsuario)
            dbBases.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, CodigoModulo)
            dbBases.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, CodigoOpcion)
            Return dbBases.ExecuteDataSet(cmd)
        End Function
        Public Function FUN_LIS_TipoCriterioConductualSinBM(ByVal CodigoUsuario As Integer, ByVal CodigoTipoUsuario As Integer, _
                                                      ByVal CodigoModulo As Integer, ByVal CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBases.GetStoredProcCommand("CT_USP_LIS_TipoCriterioConductualSinBM")
            dbBases.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, CodigoUsuario)
            dbBases.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, CodigoTipoUsuario)
            dbBases.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, CodigoModulo)
            dbBases.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, CodigoOpcion)
            Return dbBases.ExecuteDataSet(cmd)
        End Function
#End Region
    End Class
End Namespace

