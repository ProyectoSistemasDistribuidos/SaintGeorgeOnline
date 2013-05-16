Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class da_CursoPromedios
    Inherits InstanciaConexion.ManejadorConexion
    Private dbBase As SqlDatabase 'ExecuteDataSet
    Private dbCommand As DbCommand 'ExecuteScalar

    Public Sub New()
        dbBase = New SqlDatabase(Me.SqlConexionDB)
    End Sub


#Region "Metodos No  Transaccionales"

    Function fnListarCursoPromedio(ByVal int_AnioAcademico As Integer, ByVal int_codAlumno As Integer) As DataSet
        Try

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_LIS_UPS_CursoPromedios")
            dbBase.AddInParameter(dbCommand, "@p_codAlumno", DbType.Int32, int_codAlumno)
            dbBase.AddInParameter(dbCommand, "@p_codAnioAcademico", DbType.Int32, int_AnioAcademico)
            Return dbBase.ExecuteDataSet(dbCommand)

        Catch ex As Exception
        Finally


        End Try
    End Function

#End Region




End Class
