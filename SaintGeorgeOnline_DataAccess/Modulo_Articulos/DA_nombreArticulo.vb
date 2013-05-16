Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class DA_nombreArticulo
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
#Region "  transaccional"
    Public Function F_insertarNombreArticulo(ByVal oBE_nombreArticulo As BE_nombreArticulo)

        Dim lstIds As New List(Of String)
        Dim str_Mensaje As String = ""
        Dim p_Valor As Integer = 0
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_InsAF_NombreArticulosInventarios")
                dbBase.AddInParameter(cmd, "@NA_CodigoNombreArticuloInventario", DbType.Int32, oBE_nombreArticulo.NA_CodigoNombreArticuloInventario)
                dbBase.AddInParameter(cmd, "@NA_Descripcion", DbType.String, oBE_nombreArticulo.NA_Descripcion)
                dbBase.AddInParameter(cmd, "@NA_Estado", DbType.Boolean, oBE_nombreArticulo.NA_Estado)

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
