Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloAcademico

Namespace ModuloAcademico

    Public Class da_CierrePeriodo
        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"
        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar
        Private cnn As DbConnection
        Private tran As DbTransaction
#End Region

#Region "Constructor"
        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
            cnn = Me.dbBase.CreateConnection()
        End Sub
#End Region

#Region "Propiedades"
        Public ReadOnly Property BaseDatos() As SqlDatabase
            Get
                Return Me.dbBase
            End Get
        End Property
        Public ReadOnly Property Transaccion() As DbTransaction
            Get
                Return Me.tran
            End Get
        End Property
        Public ReadOnly Property Conexion() As DbConnection
            Get
                Return Me.cnn
            End Get
        End Property
#End Region

#Region "Metodos"
        Public Sub BeginTransaction()
            If Not (cnn.State = ConnectionState.Open) Then
                cnn.Open()
            End If
            tran = cnn.BeginTransaction(IsolationLevel.Serializable)
        End Sub
        Public Sub Rollback()
            tran.Rollback()
        End Sub
        Public Sub Commit()
            tran.Commit()
        End Sub
#End Region

#Region "Región Transaccional"

        'multiple
        Public Function FUN_UPD_NotaFinal( _
            ByVal lst_CierrePeriodo As List(Of be_CierrePeriodo), _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim int_total As Integer = 0
            Dim str_mensajeAux As String = ""
            Dim int_valorAux As Integer = 0
            Dim lstResultado As New List(Of String)

            For Each item As be_CierrePeriodo In lst_CierrePeriodo

                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_NotaFinalCuantitativa")
                dbCommand.Parameters.Clear()

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int32, item.CodigoAsignacionAula)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, item.CodigoPeriodo)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand)
                str_mensajeAux = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_valorAux = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Agrego mi lista de retorno
                lstResultado.Add(str_mensajeAux)
                'lstResultado.Add(int_valorAux.ToString())

            Next

            Return lstResultado

        End Function

        'x aula
        Public Function FUN_UPD_NotaFinal( _
            ByVal obe_CierrePeriodo As be_CierrePeriodo, ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try
                BeginTransaction()
                Dim int_valorAux As Integer
                Dim dbCommand As DbCommand

                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_NotaFinalCuantitativa")
                dbCommand.Parameters.Clear()
                dbCommand.CommandTimeout = 600 '10mins

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int32, obe_CierrePeriodo.CodigoAsignacionAula)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, obe_CierrePeriodo.CodigoPeriodo)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_valorAux = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not int_valorAux > 0 Then
                    Rollback()
                End If

                Commit()
                Return int_valorAux

            Catch ex As Exception
                Rollback()
                str_mensaje = "Ocurrio un error durante el registro."
                Return -1
            Finally
                Conexion.Close()
            End Try

        End Function

        'cursos oficiales c/ interno
        Public Function FUN_UPD_NotaFinalcOficialcInterno( _
            ByVal obe_CierrePeriodo As be_CierrePeriodo, ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try
                BeginTransaction()
                Dim int_valorAux As Integer
                Dim dbCommand As DbCommand

                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_NotaFinalCuantitativaCursosOficialesCInternos")
                dbCommand.Parameters.Clear()
                dbCommand.CommandTimeout = 600 '10mins

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int32, obe_CierrePeriodo.CodigoAsignacionAula)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, obe_CierrePeriodo.CodigoPeriodo)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_valorAux = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not int_valorAux > 0 Then
                    Rollback()
                End If

                Commit()
                Return int_valorAux

            Catch ex As Exception
                Rollback()
                str_mensaje = "Ocurrio un error durante el registro."
                Return -1
            Finally
                Conexion.Close()
            End Try

        End Function

        'cursos oficiales c/ interno x bimestre
        Public Function FUN_UPD_NotaFinalcOficialcInternoXBimestre( _
            ByVal obe_CierrePeriodo As be_CierrePeriodo, ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Try
                BeginTransaction()
                Dim int_valorAux As Integer
                Dim dbCommand As DbCommand

                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_NotaFinalCuantitativaCursosOficialesCInternosXBimestre")
                dbCommand.Parameters.Clear()
                dbCommand.CommandTimeout = 600 '10mins

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int32, obe_CierrePeriodo.CodigoAsignacionAula)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, obe_CierrePeriodo.CodigoPeriodo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int32, obe_CierrePeriodo.CodigoBimestre)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_valorAux = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not int_valorAux > 0 Then
                    Rollback()
                End If

                Commit()
                Return int_valorAux

            Catch ex As Exception
                Rollback()
                str_mensaje = "Ocurrio un error durante el registro."
                Return -1
            Finally
                Conexion.Close()
            End Try

        End Function

#End Region

#Region "Región No Transaccional"

#End Region

    End Class

End Namespace