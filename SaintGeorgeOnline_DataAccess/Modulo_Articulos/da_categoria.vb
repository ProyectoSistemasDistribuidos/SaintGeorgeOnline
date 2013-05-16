Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class da_categoria

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
    Public Function F_insertarCategoria(ByVal oBE_categoria As BE_categoria) As List(Of String)
        Dim lstIds As New List(Of String)
        Dim str_Mensaje As String = ""
        Dim p_Valor As Integer = 0
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_insCategoriaArticulo")

                '@CA_CodigoCategoriaArticulo int ,
                '		@TA_CodigoTipoArticulo int ,
                '		@CA_Descripcion varchar(max),
                '		@CA_Estado bit ,

                dbBase.AddInParameter(cmd, "@CA_CodigoCategoriaArticulo", DbType.Int32, oBE_categoria.CA_CodigoCategoriaArticulo)
                dbBase.AddInParameter(cmd, "@TA_CodigoTipoArticulo", DbType.Int32, oBE_categoria.TA_CodigoTipoArticulo)
                dbBase.AddInParameter(cmd, "@CA_Descripcion", DbType.String, oBE_categoria.CA_Descripcion)
                dbBase.AddInParameter(cmd, "@CA_Estado", DbType.Boolean, oBE_categoria.CA_Estado)

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
