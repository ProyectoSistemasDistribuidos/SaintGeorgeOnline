Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class da_color
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

#Region "transaccional"

#End Region

#Region "no transaccional"
    Public Function F_insertarColor(ByVal obe_color As be_color)

        Dim lstIds As New List(Of String)
        Dim str_Mensaje As String = ""
        Dim p_Valor As Integer = 0
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_InsAF_Color")
                dbBase.AddInParameter(cmd, "@CL_CodigoColor", DbType.Int32, obe_color.CL_CodigoColor)
                dbBase.AddInParameter(cmd, "@CL_Descripcion", DbType.String, obe_color.CL_Descripcion)
                dbBase.AddInParameter(cmd, "@CL_Estado", DbType.Boolean, obe_color.CL_Estado)

                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 100)

                dbBase.ExecuteScalar(cmd)

                str_Mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@codigo")))

                lstIds.Add(str_Mensaje)
                lstIds.Add(p_Valor.ToString())


                '@CL_CodigoColor int,
                '@CL_Descripcion varchar(max),
                '@CL_Estado bit,
                '@codigo int out ,
                '@mensaje varchar(max) out 
            End Using


        Catch ex As Exception
        Finally

        End Try
    End Function
#End Region
End Class
