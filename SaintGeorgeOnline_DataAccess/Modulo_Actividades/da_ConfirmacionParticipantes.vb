Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
'ok
Namespace ModuloActividades

    Public Class da_ConfirmacionParticipantes

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

        Public Function FUN_INS_ConfirmacionParticipantes(ByVal dtLista As DataTable, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MiMensaje As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                If dtLista.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLista.Rows

                        dbCommand = Me.dbBase.GetStoredProcCommand("AC_USP_UPD_ConfirmacionParticipantes")

                        'Parámetros de entrada
                        dbBase.AddInParameter(dbCommand, "@p_CodigoConfirmacionAsistencia", DbType.Int32, dr.Item("CodigoConfirmacionAsistencia"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionActividad", DbType.Int32, dr.Item("CodigoProgramacionActividad"))
                        dbBase.AddInParameter(dbCommand, "@p_CantidadParticipantes", DbType.Int32, dr.Item("CantidadParticipantes"))
                        dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, dr.Item("Observacion"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoFamilia", DbType.String, dr.Item("CodigoFamilia"))
                        dbBase.AddInParameter(dbCommand, "@p_Check", DbType.Int32, dr.Item("Check"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorAsistente", DbType.Int32, dr.Item("CodigoTrabajadorAsistente"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoInvitado", DbType.Int32, dr.Item("CodigoInvitado"))
                        dbBase.AddInParameter(dbCommand, "@p_Tipo", DbType.Int32, dr.Item("Tipo"))

                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                        'Parámetros de salida
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                        'Ejecucion del Store Procedure
                        dbBase.ExecuteScalar(dbCommand, tran)
                        str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                        int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                        If Not int_Valor > 0 Then
                            Rollback()
                            Return int_Valor
                        End If

                    Next
                End If

                Commit()
                Return int_Valor

            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try

        End Function

        Public Function FUN_INS_RegAsistentes(ByVal dtLista As DataTable, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MiMensaje As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                If dtLista.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLista.Rows

                        dbCommand = Me.dbBase.GetStoredProcCommand("AC_USP_UPD_RegAsistentes")

                        'Parámetros de entrada
                        dbBase.AddInParameter(dbCommand, "@p_CodigoConfirmacionAsistencia", DbType.Int32, dr.Item("CodigoConfirmacionAsistencia"))
                        'dbBase.AddInParameter(dbCommand, "@p_CodigoProgramacionActividad", DbType.Int32, dr.Item("CodigoProgramacionActividad"))
                        dbBase.AddInParameter(dbCommand, "@p_CantidadAsistentes", DbType.Int32, dr.Item("CantidadAsistentes"))
                        'dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, dr.Item("Observacion"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoFamilia", DbType.String, dr.Item("CodigoFamilia"))
                        dbBase.AddInParameter(dbCommand, "@p_Check", DbType.Int32, dr.Item("Check"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorAsistente", DbType.Int32, dr.Item("CodigoTrabajadorAsistente"))
                        dbBase.AddInParameter(dbCommand, "@p_Tipo", DbType.Int32, dr.Item("Tipo"))

                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                        'Parámetros de salida
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                        'Ejecucion del Store Procedure
                        dbBase.ExecuteScalar(dbCommand, tran)
                        str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                        int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                        If Not int_Valor > 0 Then
                            Rollback()
                            Return int_Valor
                        End If

                    Next
                End If

                Commit()
                Return int_Valor

            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try

        End Function

#End Region

#Region "Metodos No Transaccionales"
        Public Function FUN_LIS_ConfirmacionParticipantes(ByVal int_CodigoActividad As Integer, ByVal int_Tipo As Integer, ByVal str_Familia As String, _
                                       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AC_USP_LIS_ConfirmacionParticipantes")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoActividad", DbType.Int32, int_CodigoActividad)
            dbBase.AddInParameter(cmd, "@p_Familia", DbType.String, str_Familia)
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int32, int_Tipo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_RegAsistentes(ByVal int_CodigoActividad As Integer, ByVal int_Tipo As Integer, ByVal str_Apellido As String, _
                                       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AC_USP_LIS_RegAsistentes")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoActividad", DbType.Int32, int_CodigoActividad)
            dbBase.AddInParameter(cmd, "@p_Familia", DbType.String, str_Apellido)
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int32, int_Tipo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_Actividades(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AC_USP_LIS_Actividades")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_CantConfirmacionParticipantes(ByVal int_CodigoActividad As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AC_USP_LIS_CantConfirmacionParticipantes")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoActividad", DbType.Int32, int_CodigoActividad)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_CantRegAsistentes(ByVal int_CodigoActividad As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AC_USP_LIS_CantRegAsistentes")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoActividad", DbType.Int32, int_CodigoActividad)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_ConfirmacionParticipantes(ByVal int_CodigoActividad As Integer, ByVal int_Tipo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("AC_USP_REP_ConfirmacionParticipantes")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoActividad", DbType.Int32, int_CodigoActividad)
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int32, int_Tipo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
#End Region

    End Class

End Namespace