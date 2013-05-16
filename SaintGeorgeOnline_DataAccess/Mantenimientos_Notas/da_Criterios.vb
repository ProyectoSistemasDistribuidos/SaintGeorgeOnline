﻿Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_Criterios
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

#Region "Metodos Transaccionales"

        Public Function FUN_INS_Criterios(ByVal str_Descripcion As String, ByVal int_codigoAsignacionGrupo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_Criterios")

                dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
                dbBase.AddInParameter(cmd, "@p_codigoAsignacionGrupo", DbType.Int16, int_codigoAsignacionGrupo)

                dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 100)
                dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int32, 255)
                dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.ExecuteScalar(cmd)

                str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))
                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado

            Catch ex As Exception

            End Try
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Criterios(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_codigoAsignacionGrupo As Integer, _
                                          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_Criterios")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)
            dbBase.AddInParameter(cmd, "@p_codigoAsignacionGrupo", DbType.Int16, int_codigoAsignacionGrupo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace