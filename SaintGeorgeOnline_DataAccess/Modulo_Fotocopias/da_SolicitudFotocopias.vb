Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloFotocopia

Namespace ModuloFotocopias

    Public Class da_SolicitudFotocopias
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

        Public Function FUN_INS_SolicitudFotocopias(ByVal objSolicitudFotocopias As be_SolicitudFotocopias, _
                                                    ByVal objDetalle As DataTable, _
                                                    ByRef str_Mensaje As String, _
                                                    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim str_MensajeDetalle As String = ""
            Dim int_totalregistros As Integer = 0

            Try
                'inicio la transaccion
                BeginTransaction()

                dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_INS_SolicitudFotocopias")
                dbBase.AddInParameter(dbCommand, "@p_FechaRegistro", DbType.Date, objSolicitudFotocopias.FechaRegistro)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If Not int_valor > 0 Then ' sino registro
                    Rollback()
                    Return int_valor
                Else

                    If objDetalle IsNot Nothing Then
                        If objDetalle.Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                            Dim objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias
                            For Each dr As DataRow In objDetalle.Rows
                                If dr.Item("Estado") = 1 Then
                                    objDetalleSolicitudFotocopias = New be_DetalleSolicitudFotocopias
                                    objDetalleSolicitudFotocopias.CodigoSolicitudFotocopia = int_valor
                                    objDetalleSolicitudFotocopias.CodigoAsignacionGrupo = dr.Item("CodigoAsignacionGrupo")
                                    objDetalleSolicitudFotocopias.NumeroCopias = dr.Item("NumCopias")
                                    objDetalleSolicitudFotocopias.Tema = dr.Item("Tema")
                                    objDetalleSolicitudFotocopias.FechaImpresion = dr.Item("FechaImpre")
                                    objDetalleSolicitudFotocopias.Observacion = dr.Item("Observacion")
                                    int_ValorDetalle = FUN_INS_DetalleSolicitudFotocopias(objDetalleSolicitudFotocopias, tran, _
                                                                                          str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                    If Not int_ValorDetalle > 0 Then
                                        Rollback()
                                        str_Mensaje = str_MensajeDetalle
                                        Return int_ValorDetalle
                                    End If
                                End If
                            Next
                        End If
                    End If

                End If

                Commit()
                Return int_valor

            Catch ex As Exception
                str_Mensaje = "ocurrio un error durante el registro."
                Rollback()
                Return int_valor
            Finally
                Conexion.Close()
            End Try

        End Function

        Public Function FUN_UPD_SolicitudFotocopias(ByVal objSolicitudFotocopias As be_SolicitudFotocopias, _
                                                    ByVal objDetalle As DataTable, _
                                                    ByRef str_Mensaje As String, _
                                                    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim str_MensajeDetalle As String = ""
            Dim int_totalregistros As Integer = 0

            Try
                'inicio la transaccion
                BeginTransaction()
                int_valor = objSolicitudFotocopias.CodigoSolicitudFotocopia

                If objDetalle.Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo actualizo
                    Dim objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias
                    For Each dr As DataRow In objDetalle.Rows
                        If dr.Item("Estado") = 1 Then ' update o insert

                            If dr.Item("Tipo") = "T" Then ' insert 

                                objDetalleSolicitudFotocopias = New be_DetalleSolicitudFotocopias
                                objDetalleSolicitudFotocopias.CodigoSolicitudFotocopia = int_valor
                                objDetalleSolicitudFotocopias.CodigoAsignacionGrupo = dr.Item("CodigoAsignacionGrupo")
                                objDetalleSolicitudFotocopias.NumeroCopias = dr.Item("NumCopias")
                                objDetalleSolicitudFotocopias.Tema = dr.Item("Tema")
                                objDetalleSolicitudFotocopias.FechaImpresion = dr.Item("FechaImpre")
                                objDetalleSolicitudFotocopias.Observacion = dr.Item("Observacion")
                                int_ValorDetalle = FUN_INS_DetalleSolicitudFotocopias(objDetalleSolicitudFotocopias, tran, _
                                                                                      str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                str_Mensaje = str_MensajeDetalle
                                If Not int_ValorDetalle > 0 Then
                                    Rollback()
                                    Return int_ValorDetalle
                                End If

                            ElseIf dr.Item("Tipo") = "R" Then ' update

                                objDetalleSolicitudFotocopias = New be_DetalleSolicitudFotocopias
                                objDetalleSolicitudFotocopias.CodigoSolicitudFotocopia = int_valor
                                objDetalleSolicitudFotocopias.CodigoDetalleSolicitudFotocopia = dr.Item("CodigoDetalleSolicitudFotocopias")
                                objDetalleSolicitudFotocopias.CodigoAsignacionGrupo = dr.Item("CodigoAsignacionGrupo")
                                objDetalleSolicitudFotocopias.NumeroCopias = dr.Item("NumCopias")
                                objDetalleSolicitudFotocopias.Tema = dr.Item("Tema")
                                objDetalleSolicitudFotocopias.FechaImpresion = dr.Item("FechaImpre")
                                objDetalleSolicitudFotocopias.Observacion = dr.Item("Observacion")
                                int_ValorDetalle = FUN_UPD_DetalleSolicitudFotocopias(objDetalleSolicitudFotocopias, tran, _
                                                                                      str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                str_Mensaje = str_MensajeDetalle
                                If Not int_ValorDetalle > 0 Then
                                    Rollback()
                                    Return int_ValorDetalle
                                End If
                            End If

                        ElseIf dr.Item("Estado") = 0 Then ' delete

                            If dr.Item("Tipo") = "T" Then ' nada pasa
                            ElseIf dr.Item("Tipo") = "R" Then ' actualizo el estado a eliminado
                                objDetalleSolicitudFotocopias = New be_DetalleSolicitudFotocopias
                                objDetalleSolicitudFotocopias.CodigoDetalleSolicitudFotocopia = dr.Item("CodigoDetalleSolicitudFotocopias")
                                int_ValorDetalle = FUN_DEL_DetalleSolicitudFotocopias(objDetalleSolicitudFotocopias, tran, _
                                                                                 str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                str_Mensaje = str_MensajeDetalle
                                If Not int_ValorDetalle > 0 Then
                                    Rollback()
                                    Return int_ValorDetalle
                                End If
                            End If

                        End If
                    Next
                End If

                Commit()
                Return int_valor

            Catch ex As Exception
                str_Mensaje = "ocurrio un error durante el registro."
                Rollback()
                Return int_valor
            Finally
                Conexion.Close()
            End Try

        End Function

        Public Function FUN_DEL_SolicitudFotocopias(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_DEL_SolicitudFotocopias")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_ENV_SolicitudFotocopias(ByVal int_Codigo As Integer, ByVal int_CodigoEstado As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_ENV_SolicitudFotocopias")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Codigo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEstado", DbType.Int32, int_CodigoEstado)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_SolicitudFotocopiasEstadoDetalle(ByVal objSolicitudFotocopias As be_SolicitudFotocopias, _
                                                    ByVal objDetalle As DataTable, _
                                                    ByRef str_Mensaje As String, _
                                                    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim str_MensajeDetalle As String = ""
            Dim int_totalregistros As Integer = 0

            Try
                'inicio la transaccion
                BeginTransaction()
                int_valor = objSolicitudFotocopias.CodigoSolicitudFotocopia

                If objDetalle.Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo actualizo
                    Dim objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias
                    For Each dr As DataRow In objDetalle.Rows
                        If dr.Item("Estado") = 1 Then ' update o insert

                            objDetalleSolicitudFotocopias = New be_DetalleSolicitudFotocopias
                            objDetalleSolicitudFotocopias.CodigoSolicitudFotocopia = int_valor
                            objDetalleSolicitudFotocopias.CodigoDetalleSolicitudFotocopia = dr.Item("CodigoDetalleSolicitudFotocopias")
                            objDetalleSolicitudFotocopias.CodigoEstadoProceso = dr.Item("EstadoProceso")

                            int_ValorDetalle = FUN_UPD_DetalleSolicitudFotocopiasEstadoProceso(objDetalleSolicitudFotocopias, tran, _
                                                                                  str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            str_Mensaje = str_MensajeDetalle
                            If Not int_ValorDetalle > 0 Then
                                Rollback()
                                Return int_ValorDetalle
                            End If

                        End If
                    Next
                End If

                Commit()
                Return int_valor

            Catch ex As Exception
                str_Mensaje = "ocurrio un error durante el registro."
                Rollback()
                Return int_valor
            Finally
                Conexion.Close()
            End Try

        End Function


        Public Function FUN_INS_DetalleSolicitudFotocopias(ByVal objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias, _
                                                           ByVal objSqlTransaction As SqlTransaction, _
                                                           ByRef str_Mensaje As String, _
                                                           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_INS_DetalleSolicitudFotocopias")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitudFotocopia", DbType.Int32, objDetalleSolicitudFotocopias.CodigoSolicitudFotocopia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupo", DbType.Int32, objDetalleSolicitudFotocopias.CodigoAsignacionGrupo)

            dbBase.AddInParameter(dbCommand, "@p_NumeroCopias", DbType.Int32, objDetalleSolicitudFotocopias.NumeroCopias)
            dbBase.AddInParameter(dbCommand, "@p_Tema", DbType.String, objDetalleSolicitudFotocopias.Tema)
            dbBase.AddInParameter(dbCommand, "@p_FechaImpresion", DbType.Date, objDetalleSolicitudFotocopias.FechaImpresion)
            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objDetalleSolicitudFotocopias.Observacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_DetalleSolicitudFotocopias(ByVal objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias, _
                                                           ByVal objSqlTransaction As SqlTransaction, _
                                                           ByRef str_Mensaje As String, _
                                                           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_UPD_DetalleSolicitudFotocopias")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudFotocopia", DbType.Int32, objDetalleSolicitudFotocopias.CodigoDetalleSolicitudFotocopia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitudFotocopia", DbType.Int32, objDetalleSolicitudFotocopias.CodigoSolicitudFotocopia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionGrupo", DbType.Int32, objDetalleSolicitudFotocopias.CodigoAsignacionGrupo)

            dbBase.AddInParameter(dbCommand, "@p_NumeroCopias", DbType.Int32, objDetalleSolicitudFotocopias.NumeroCopias)
            dbBase.AddInParameter(dbCommand, "@p_Tema", DbType.String, objDetalleSolicitudFotocopias.Tema)
            dbBase.AddInParameter(dbCommand, "@p_FechaImpresion", DbType.Date, objDetalleSolicitudFotocopias.FechaImpresion)
            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objDetalleSolicitudFotocopias.Observacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_DEL_DetalleSolicitudFotocopias(ByVal objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias, _
                                                           ByVal objSqlTransaction As SqlTransaction, _
                                                           ByRef str_Mensaje As String, _
                                                           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_DEL_DetalleSolicitudFotocopias")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudFotocopia", DbType.Int32, objDetalleSolicitudFotocopias.CodigoDetalleSolicitudFotocopia)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_DetalleSolicitudFotocopiasEstadoProceso( _
            ByVal objDetalleSolicitudFotocopias As be_DetalleSolicitudFotocopias, _
            ByVal objSqlTransaction As SqlTransaction, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("FC_USP_UPD_DetalleSolicitudFotocopiasEstadoProceso")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudFotocopia", DbType.Int32, objDetalleSolicitudFotocopias.CodigoDetalleSolicitudFotocopia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleEstadoProceso", DbType.Int32, objDetalleSolicitudFotocopias.CodigoEstadoProceso)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SolicitudFotocopias(ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("FC_USP_LIS_SolicitudFotocopias")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_FechaInicio", DbType.Date, dt_FechaInicio)
            dbBase.AddInParameter(cmd, "@p_FechaFin", DbType.Date, dt_FechaFin)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_SolicitudFotocopias(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("FC_USP_GET_SolicitudFotocopias")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_SolicitudFotocopiasValidacionImpresion(ByVal int_CodigoEstadoProceso As Integer, ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("FC_USP_LIS_SolicitudFotocopiasValidacion")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoEstadoProceso", DbType.Int16, int_CodigoEstadoProceso)
            dbBase.AddInParameter(cmd, "@p_FechaInicio", DbType.Date, dt_FechaInicio)
            dbBase.AddInParameter(cmd, "@p_FechaFin", DbType.Date, dt_FechaFin)

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