Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities

Public Class da_RegistroNotasBimestralesCualitativas
    Inherits InstanciaConexion.ManejadorConexion
    Private dbBase As SqlDatabase 'ExecuteDataSet
    Private dbCommand As DbCommand 'ExecuteScalar

    Public Sub New()
        dbBase = New SqlDatabase(Me.SqlConexionDB)
    End Sub
#Region "Metodos Transaccionales"

    Public Function FUN_INS_RegistroNotasBimestralesCualitativas(ByVal obe_RegistroNotasBimestralesCualitativas As be_RegistroNotasBimestralesCualitativas, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroNotasBimestralesCualitativas")
            dbBase.AddInParameter(dbCommand, "@P_CodigoBimestre", DbType.Int32, obe_RegistroNotasBimestralesCualitativas.int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@P_CodigoRegistroAnualL", DbType.Int32, obe_RegistroNotasBimestralesCualitativas.int_CodigoRegistroAnualL)

            '@P_CodigoBimestre INT,
            '@P_CodigoRegistroAnualL INT,

            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
            lstResultado.Add(str_Mensaje)
            lstResultado.Add(p_Valor.ToString())
            Return lstResultado



        Catch ex As Exception

        End Try
    End Function

    Public Function FUN_LIS_RegistroNotasBimestralesCualitativas(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_CU_RegistroNotasBiCual")
            dbBase.AddInParameter(dbCommand, "@P_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)
            dbBase.AddInParameter(dbCommand, "@P_CodigoBimestre", DbType.Int32, int_CodigoBimestre)
            '@P_CodigoAsignacionGrupo int,
            '@P_CodigoBimestre int,
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            Return dbBase.ExecuteDataSet(dbCommand)
        Catch ex As Exception

        End Try
    End Function


    '    LTER  PROCEDURE [dbo].[CU_USP_UPD_CU_RegistroNotasBimestralesCualitativas]
    '@P_RNBL_CodigoRegistroBimestralL int ,
    '@p_ObservacionCurso VARCHAR(MAX),

    '@p_Mensaje VARCHAR(255) OUTPUT,  
    '@p_Valor INT OUTPUT  ,

    '@p_CodigoUsuario  INT ,  
    '@p_CodigoTipoUsuario  INT,  
    '@p_CodigoModulo  INT,  
    '@p_CodigoOpcion  INT  


    Public Function FUN_UPD_RegistroNotasBimestralesCualitativas(ByVal int_idBimestre As Integer, ByVal str_Observacion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)
        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_CU_RegistroNotasBimestralesCualitativas")

            dbBase.AddInParameter(dbCommand, "@P_RNBL_CodigoRegistroBimestralL", DbType.Int32, int_idBimestre)
            dbBase.AddInParameter(dbCommand, "@p_ObservacionCurso", DbType.String, str_Observacion)

            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)


            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            dbBase.ExecuteScalar(dbCommand)

            p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            str_Mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@p_Mensaje"))
            ' 
            lstResultado.Add(p_Valor)
            lstResultado.Add(str_Mensaje)

            Return lstResultado


        Catch ex As Exception
            lstResultado.Add("-1")
            lstResultado.Add("Error")

            Return lstResultado
        Finally

        End Try
    End Function
#End Region
End Class


