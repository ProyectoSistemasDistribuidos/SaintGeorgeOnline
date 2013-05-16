Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class DA_CU_HomologacionCursosMinisterio
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


#Region "Transaccional "
   
    Public Function F_InsertarCU_HomologacionCursosMinisterio(ByVal oCU_HomologacionCursosMinisterio As BE_CU_HomologacionCursosMinisterio) As List(Of String)
        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_insHomologacionCursos")

                dbBase.AddInParameter(cmd, "@HCM_CodigoHomologacionCursos", DbType.Int32, oCU_HomologacionCursosMinisterio.HCM_CodigoHomologacionCursos)
                dbBase.AddInParameter(cmd, "@CM_CodigoCursoMinisterio", DbType.Int32, oCU_HomologacionCursosMinisterio.CM_CodigoCursoMinisterio)
                dbBase.AddInParameter(cmd, "@ACA_CodigoAsignacionCurso", DbType.Int32, oCU_HomologacionCursosMinisterio.ACA_CodigoAsignacionCurso)
                dbBase.AddInParameter(cmd, "@HCM_CantidadDescriptores", DbType.Int32, oCU_HomologacionCursosMinisterio.HCM_CantidadDescriptores)



                dbBase.AddInParameter(cmd, "@HCM_Estado", DbType.Boolean, oCU_HomologacionCursosMinisterio.HCM_Estado)
                dbBase.AddInParameter(cmd, "@HCM_OrdenActa", DbType.Int32, oCU_HomologacionCursosMinisterio.HCM_OrdenActa)


                dbBase.AddInParameter(cmd, "@ACAN_CodigoAsignacionCursoActaNiveles", DbType.Int32, oCU_HomologacionCursosMinisterio.ACAN_CodigoAsignacionCursoActaNiveles)


                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int32, 255)

                dbBase.ExecuteScalar(cmd)


                'ACAN_CodigoAsignacionCursoActaNiveles
                str_Mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@codigo")))
                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado
            End Using

          

        Catch ex As Exception
        Finally

        End Try
    End Function


#End Region
#Region "No transaccional"



#End Region
End Class
