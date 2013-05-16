Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_RegistroEvaluacionesCuantitativos
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

#Region "Metodos Transaccionales"

        Public Function FUN_INS_RegistroEvaluacionesCuantitativosIndividual(ByVal obj_be_RegistroEvaluacion As be_RegistroEvaluacionesCuantitativos, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0

                'Inicio la transaccion
                BeginTransaction()
                ' insert
                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_RegistroEvaluacionesCuantitativos")
                dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroCuantitativo", DbType.Int16, obj_be_RegistroEvaluacion.CodigoRegistroCriterio)
                dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, obj_be_RegistroEvaluacion.Descriptor)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                p_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not p_Valor > 0 Then
                    Rollback()
                End If

                Commit()

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado

            Catch ex As Exception

                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                Rollback()
                Return lstResultado

            Finally

                Conexion.Close()
            End Try

        End Function

        Public Function FUN_INS_RegistroEvaluacionesCuantitativos(ByVal arr_Descriptores As List(Of be_RegistroEvaluacionesCuantitativos), _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0

                'Inicio la transaccion
                BeginTransaction()

                For Each be_obj_RegistroEvaluacion As be_RegistroEvaluacionesCuantitativos In arr_Descriptores

                    If be_obj_RegistroEvaluacion.CodigoRegistroEvaluacion > 0 Then ' update
                        dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroEvaluacionesCuantitativos")
                        dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroEvaluacion", DbType.Int16, be_obj_RegistroEvaluacion.CodigoRegistroEvaluacion)
                    Else ' insert
                        dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_RegistroEvaluacionesCuantitativos")
                        dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroCuantitativo", DbType.Int16, be_obj_RegistroEvaluacion.CodigoRegistroCriterio)
                    End If

                    dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, be_obj_RegistroEvaluacion.Descriptor)

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                    dbBase.ExecuteScalar(dbCommand, tran)

                    str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    p_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                    If Not p_Valor > 0 Then
                        Rollback()
                    End If
                Next

                Commit()

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado

            Catch ex As Exception

                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                Rollback()
                Return lstResultado

            Finally

                Conexion.Close()
            End Try

        End Function

        Public Function FUN_DEL_RegistroEvaluacionesCuantitativos(ByVal int_CodigoRegistroEvaluacion As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0

                'Inicio la transaccion
                BeginTransaction()

                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_DEL_RegistroEvaluacionesCuantitativos")
                dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroEvaluacion", DbType.Int16, int_CodigoRegistroEvaluacion)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                p_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not p_Valor > 0 Then
                    Rollback()
                Else
                    Commit()
                End If

                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado

            Catch ex As Exception

                lstResultado.Add("Ocurrio un error durante el registro.")
                lstResultado.Add(0)
                Rollback()
                Return lstResultado

            Finally
                Conexion.Close()
            End Try

        End Function

        ' update 15/03/2013
        Public Function FUN_INS_RegistroEvaluacionesCuantitativosPlantilla(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim p_Valor As Integer = 0
            Try
                'Dim str_Mensaje As String = ""

                'Inicio la transaccion
                BeginTransaction()

                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_RegistroEvaluacionesCuantitativosPlantilla")
                dbBase.AddInParameter(dbCommand, "@P_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)
                dbBase.AddInParameter(dbCommand, "@P_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                str_mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                p_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If p_Valor < 0 Then
                    Rollback()
                    Return p_Valor
                End If

                Commit()
                Return p_Valor

            Catch ex As Exception
                str_mensaje = "Ocurrio un error durante el registro."
                Return p_Valor
            Finally
                Conexion.Close()
            End Try

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_RegistroEvaluacionesCuantitativos(ByVal int_CodigoRegistroCuantitativo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_RegistroEvaluacionesCuantitativos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoRegistroCuantitativo", DbType.Int16, int_CodigoRegistroCuantitativo)

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