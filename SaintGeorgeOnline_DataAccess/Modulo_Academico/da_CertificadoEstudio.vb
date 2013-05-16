Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common

Namespace ModuloAcademico

    Public Class da_CertificadoEstudio
        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"
        Private dbBase As SqlDatabase
        Private dbCommand As DbCommand
#End Region

#Region "Contructor"
        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
        End Sub
#End Region

#Region "Región Transaccional"

#End Region

#Region "Región No Transaccional"

        Public Function FUN_LIS_CertificadoEstudio(ByVal int_CodigoAnioAcademico As Integer, ByVal str_CodigoAlumno As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_NotasCertificado")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_codigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_codigoAlumno", DbType.String, str_CodigoAlumno)

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