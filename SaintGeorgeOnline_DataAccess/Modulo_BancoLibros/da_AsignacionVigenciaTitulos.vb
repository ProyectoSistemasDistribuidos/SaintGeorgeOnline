Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Namespace ModuloBancoLibros
    Public Class da_AsignacionVigenciaTitulos
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
        Public Function FUN_REP_DinamicoAniosUtilidad(ByVal int_PeriodoInicio As Integer, ByVal int_PeriodoFin As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_REP_AniosUtilidad")
            'Parametros de Entrada
            dbBase.AddInParameter(cmd, "@p_PeriodoInicio", DbType.Int16, int_PeriodoInicio)
            dbBase.AddInParameter(cmd, "@p_PeriodoFin", DbType.Int16, int_PeriodoFin)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecución del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function
#End Region
    End Class
End Namespace

