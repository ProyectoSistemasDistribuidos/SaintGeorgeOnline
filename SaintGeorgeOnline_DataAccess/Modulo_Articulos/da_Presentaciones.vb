﻿Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class da_Presentaciones
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
    Public Function F_insertarPresentaciones(ByVal obe_Presentaciones As be_Presentaciones) As List(Of String)
        Dim lstIds As New List(Of String)
        Dim str_Mensaje As String = ""
        Dim p_Valor As Integer = 0
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_InsAF_Presentaciones")

                dbBase.AddInParameter(cmd, "@PS_CodigoPresentacion", DbType.Int32, obe_Presentaciones.PS_CodigoPresentacion)
                dbBase.AddInParameter(cmd, "@PS_Descripcion", DbType.String, obe_Presentaciones.PS_Descripcion)
                dbBase.AddInParameter(cmd, "@PS_Estado", DbType.Boolean, obe_Presentaciones.PS_Estado)

                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 100)

                dbBase.ExecuteScalar(cmd)
                str_Mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@codigo")))

                lstIds.Add(str_Mensaje)
                lstIds.Add(p_Valor.ToString())

                '@PS_CodigoPresentacion	int,
                '@PS_Descripcion  varchar(max),
                '@PS_Estado bit
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Function F_EliminarArticulo(ByVal pcodArticulo As Integer)

        Dim lstIds As New List(Of String)
        Dim str_Mensaje As String = ""
        Dim p_Valor As Integer = 0
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_EliminarArticulo")

            End Using

        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "no transaccional"

#End Region
End Class
