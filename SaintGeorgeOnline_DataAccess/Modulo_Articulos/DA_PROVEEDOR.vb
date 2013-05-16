Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class DA_PROVEEDOR
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

#Region "transaccional "

    Public Function F_insertarProveedor(ByVal oBE_PROVEEDOR As BE_PROVEEDOR) As List(Of String)
        Dim str_Mensaje As String = ""
        Dim p_Valor As Integer = 0
        Dim lstIds As New List(Of String)
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_InsertarProveedor")


                dbBase.AddInParameter(cmd, "@PV_CodigoProveedor", DbType.Int32, oBE_PROVEEDOR.PV_CodigoProveedor)

                dbBase.AddInParameter(cmd, "@PV_Descripcion", DbType.String, oBE_PROVEEDOR.PV_CodigoProveedor)
                dbBase.AddInParameter(cmd, "@PV_CodigoProveedorAntiguo", DbType.Int32, oBE_PROVEEDOR.PV_CodigoProveedorAntiguo)


                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 100)
 


                'alter procedure USP_InsertarProveedor
                '@PV_CodigoProveedor int ,
                '@PV_Descripcion varchar( max),
                '@PV_CodigoProveedorAntiguo int ,
                '@codigo int out,
                '@mensaje varchar(max) out 
                'as
                dbBase.ExecuteScalar(cmd)

                str_Mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@codigo")))

                lstIds.Add(str_Mensaje)
                lstIds.Add(p_Valor.ToString())

                '                ()
                '@PV_Descripcion varchar( max)
                Return lstIds
            End Using


        Catch ex As Exception

        End Try
    End Function
#End Region


#Region "no transaccional"

#End Region
End Class
