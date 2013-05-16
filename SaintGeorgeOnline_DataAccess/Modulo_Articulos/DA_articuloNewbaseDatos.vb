Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class DA_articuloNewbaseDatos
    Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

    Private dbBase As SqlDatabase 'ExecuteDataSet
    Private dbCommand As DbCommand 'ExecuteScalar

#End Region

#Region "Constructor"

    Public Sub New()
        dbBase = New SqlDatabase(Me.SqlConexionArticulos)
    End Sub

    Public Function listarProveedores() As DataSet
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_lisPROVEEDOR")

                Return dbBase.ExecuteDataSet(cmd)

            End Using

        Catch ex As Exception

        End Try
    End Function

    Public Function BuscarOrdenCompra(ByVal fechaInicio As String, ByVal fechaFin As String, ByVal codProveedor As Integer, ByVal LimIn As Integer, ByVal LimSup As Integer, ByVal soloBuscar As Integer) As DataSet
        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("sp_get_OrdenCompraCabeceraModuloArticulo")

                dbBase.AddInParameter(cmd, "@fecini", DbType.String, fechaInicio)
                dbBase.AddInParameter(cmd, "@fecfin", DbType.String, fechaFin)
                dbBase.AddInParameter(cmd, "@IdProveedor", DbType.Int32, codProveedor)
                dbBase.AddInParameter(cmd, "@LimIn", DbType.Int32, LimIn)
                dbBase.AddInParameter(cmd, "@LimSup", DbType.Int32, LimSup)

                dbBase.AddInParameter(cmd, "@soloBuscar", DbType.Int32, soloBuscar)





                Return dbBase.ExecuteDataSet(cmd)

            End Using

        Catch ex As Exception

        End Try
    End Function
    Public Function F_insertarProveedor(ByVal descripcionProveedor As String) As List(Of String)
        Dim lstIds As New List(Of String)
        Dim str_Mensaje As String = ""
        Dim p_Valor As Integer = 0
        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_insProveedor")

                dbBase.AddInParameter(cmd, "@desProveedor", DbType.Int32, descripcionProveedor)

                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 100)


                str_Mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@codigo")))

                lstIds.Add(str_Mensaje)
                lstIds.Add(p_Valor.ToString())
            End Using


            Return lstIds
        Catch ex As Exception

        End Try

        '      create procedure USP_insProveedor
        '@desProveedor varchar(max)

        '@codigo int out ,
        '@mensaje varchar(max) out
    End Function

#End Region

End Class
