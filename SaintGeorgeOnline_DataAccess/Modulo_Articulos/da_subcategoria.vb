Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class da_subcategoria
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
    Public Function F_insertarSubcategoria(ByVal oBE_Subcategoria As BE_Subcategoria) As List(Of String)
        Dim lstIds As New List(Of String)
        Dim str_Mensaje As String = ""
        Dim p_Valor As Integer = 0
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_insSubacategoria")


                '                @SCA_CodigoSubCategoriaArticulo int ,
                '@CA_CodigoCategoriaArticulo int ,	
                '@SCA_Descripcion	 varchar(max ),
                '@SCA_Estado bit 

                dbBase.AddInParameter(cmd, "@SCA_CodigoSubCategoriaArticulo", DbType.Int32, oBE_Subcategoria.SCA_CodigoSubCategoriaArticulo)
                dbBase.AddInParameter(cmd, "@CA_CodigoCategoriaArticulo", DbType.Int32, oBE_Subcategoria.CA_CodigoCategoriaArticulo)
                dbBase.AddInParameter(cmd, "@SCA_Descripcion", DbType.String, oBE_Subcategoria.SCA_Descripcion)
                dbBase.AddInParameter(cmd, "@SCA_Estado", DbType.Boolean, oBE_Subcategoria.SCA_Estado)

                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 100)

                dbBase.ExecuteScalar(cmd)
                str_Mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@codigo")))

                lstIds.Add(str_Mensaje)
                lstIds.Add(p_Valor.ToString())
                Return lstIds
            End Using
        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "no transaccional"

#End Region
End Class
