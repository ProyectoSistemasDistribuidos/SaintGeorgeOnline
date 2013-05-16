Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas

Namespace ModuloNotas

    Public Class da_RegistroCriteriosCuantitativos
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

        Public Function FUN_INS_RegistroCriteriosCuantitativos( _
            ByVal arr_Criterios As List(Of be_RegistroCriteriosCuantitativos), _
            ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0

                'Inicio la transaccion
                BeginTransaction()

                For Each be_obj_RegistroCriteriosCuantitativos As be_RegistroCriteriosCuantitativos In arr_Criterios

                    If be_obj_RegistroCriteriosCuantitativos.CodigoRegistro > 0 Then ' update
                        dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroCriteriosCuantitativos")
                        dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroCuantitativo", DbType.Int16, be_obj_RegistroCriteriosCuantitativos.CodigoRegistro)
                    Else ' insert
                        dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_RegistroCriteriosCuantitativos")
                    End If

                    dbBase.AddInParameter(dbCommand, "@p_CodigoCriterio", DbType.Int16, be_obj_RegistroCriteriosCuantitativos.CodigoCriterio)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupo", DbType.Int16, int_CodigoAsignacionGrupo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
                    dbBase.AddInParameter(dbCommand, "@p_Peso", DbType.Int16, be_obj_RegistroCriteriosCuantitativos.Peso)

                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 100)
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 255)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                    dbBase.ExecuteScalar(dbCommand, tran)

                    str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

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

        Public Function FUN_INS_RegistroCriteriosCuantitativosIndidual( _
            ByVal int_CodigoCriterio As Integer, _
            ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_Peso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_INS_RegistroCriteriosCuantitativos")

                dbBase.AddInParameter(cmd, "@p_CodigoCriterio", DbType.Int16, int_CodigoCriterio)
                dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int16, int_CodigoAsignacionGrupo)
                dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
                dbBase.AddInParameter(cmd, "@p_Peso", DbType.Int16, int_Peso)

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

        Public Function FUN_UPD_RegistroCriteriosCuantitativos( _
            ByVal int_CodigoRegistroCuantitativo As Integer, _
            ByVal int_CodigoCriterio As Integer, _
            ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_Peso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_UPD_RegistroCriteriosCuantitativos")

                dbBase.AddInParameter(cmd, "@p_CodigoRegistroCuantitativo", DbType.Int16, int_CodigoRegistroCuantitativo)
                dbBase.AddInParameter(cmd, "@p_CodigoCriterio", DbType.Int16, int_CodigoCriterio)
                dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int16, int_CodigoAsignacionGrupo)
                dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
                dbBase.AddInParameter(cmd, "@p_Peso", DbType.Int16, int_Peso)

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

        Public Function FUN_DEL_RegistroCriteriosCuantitativos(ByVal int_CodigoRegistroCuantitativo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0

                'Inicio la transaccion
                BeginTransaction()

                dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_DEL_RegistroCriteriosCuantitativos")
                dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroCuantitativo", DbType.Int16, int_CodigoRegistroCuantitativo)

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

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_RegistroCriteriosCuantitativos(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_RegistroCriteriosCuantitativos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int16, int_CodigoAsignacionGrupo)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_InformacionRegistroCriteriosCuantitativos(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_GET_RegistroCriteriosCuantitativos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int16, int_CodigoAsignacionGrupo)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_EstructuraRegistroCriteriosyEvaluaciones(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_EstructuraCriteriosyEvaluaciones")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int16, int_CodigoAsignacionGrupo)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

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
