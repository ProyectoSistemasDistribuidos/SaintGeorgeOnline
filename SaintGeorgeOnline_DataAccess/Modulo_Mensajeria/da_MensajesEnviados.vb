Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMensajeria

Namespace ModuloMensajeria

    Public Class da_MensajesEnviados
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

        Public Function FUN_INS_MensajesEnviados(ByVal objMensajesEnviados As be_MensajesEnviados, _
                                         ByVal str_ListaCodigosyTipoRecibe As String, _
                                         ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MS_USP_INS_MensajeEnviados")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Asunto", DbType.String, objMensajesEnviados.Asunto)
            dbBase.AddInParameter(dbCommand, "@p_CuerpoCorreo", DbType.String, objMensajesEnviados.CuerpoCorreo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoMensaje", DbType.Int16, objMensajesEnviados.CodigoTipoMensaje)

            dbBase.AddInParameter(dbCommand, "@p_Usuario", DbType.String, objMensajesEnviados.CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersonaEnvio", DbType.Int32, objMensajesEnviados.CodigoTipoPersonaEnvio)

            dbBase.AddInParameter(dbCommand, "@p_ListaCodigosyTipoRecibe", DbType.String, str_ListaCodigosyTipoRecibe)
            dbBase.AddInParameter(dbCommand, "@p_ConfirmacionLectura", DbType.Int16, objMensajesEnviados.ConfirmacionLectura)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaRecibida", DbType.String, str_CodigoPersonaRecibe)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersonaRecepcion", DbType.String, str_CodigoTipoPersonaRecibe)

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

        'Envio de mensajes con archivos adjuntos
        Public Function FUN_INS_MensajesEnviadosConAdjuntos(ByVal objMensajesEnviados As be_MensajesEnviados, _
                                         ByVal str_ListaCodigosyTipoRecibe As String, _
                                         ByVal dt_ListaAdjuntos As DataTable, _
                                         ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_ValorDetalle As Integer = 0
            Dim str_MensajeDetalle As String = ""

            Try
                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("MS_USP_INS_MensajeEnviadosConAdjuntos")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_Asunto", DbType.String, objMensajesEnviados.Asunto)
                dbBase.AddInParameter(dbCommand, "@p_CuerpoCorreo", DbType.String, objMensajesEnviados.CuerpoCorreo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoMensaje", DbType.Int16, objMensajesEnviados.CodigoTipoMensaje)

                dbBase.AddInParameter(dbCommand, "@p_Usuario", DbType.String, objMensajesEnviados.CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersonaEnvio", DbType.Int32, objMensajesEnviados.CodigoTipoPersonaEnvio)

                dbBase.AddInParameter(dbCommand, "@p_ListaCodigosyTipoRecibe", DbType.String, str_ListaCodigosyTipoRecibe)
                dbBase.AddInParameter(dbCommand, "@p_ConfirmacionLectura", DbType.Int16, objMensajesEnviados.ConfirmacionLectura)

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
                int_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                ' Si tiene adjuntos, los registro
                If dt_ListaAdjuntos.Rows.Count > 0 Then
                    Dim objAdjuntosEnviados As be_AdjuntosEnviados
                    For Each dr As DataRow In dt_ListaAdjuntos.Rows
                        objAdjuntosEnviados = New be_AdjuntosEnviados
                        objAdjuntosEnviados.CodigoMensajeEnviado = int_Valor
                        objAdjuntosEnviados.RutaAdjunto = dr.Item("RutaArchivo")
                        objAdjuntosEnviados.NombreArchivo = dr.Item("NombreArchivoReal")
                        objAdjuntosEnviados.Extension = dr.Item("Extension")
                        objAdjuntosEnviados.TamanioArchivo = dr.Item("Tamaño")
                        int_ValorDetalle = FUN_INS_AdjuntosAMensajeEnviado(objAdjuntosEnviados, tran, str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        If Not int_ValorDetalle > 0 Then
                            Rollback()
                            Return int_ValorDetalle
                        End If
                    Next
                End If

                If int_Valor > 0 Then
                    Commit()
                Else
                    Rollback()
                End If

                Return int_Valor

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()
            End Try

        End Function

        Private Function FUN_INS_AdjuntosAMensajeEnviado(ByVal objAdjuntoEnviado As be_AdjuntosEnviados, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MS_USP_INS_AdjuntosEnviados")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoMensajeEnviado", DbType.Int32, objAdjuntoEnviado.CodigoMensajeEnviado)
            dbBase.AddInParameter(dbCommand, "@p_RutaAdjunto", DbType.String, objAdjuntoEnviado.RutaAdjunto)
            dbBase.AddInParameter(dbCommand, "@p_NombreArchivo", DbType.String, objAdjuntoEnviado.NombreArchivo)
            dbBase.AddInParameter(dbCommand, "@p_Extension", DbType.String, objAdjuntoEnviado.Extension)
            dbBase.AddInParameter(dbCommand, "@p_tamanio", DbType.Int32, objAdjuntoEnviado.TamanioArchivo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_MensajeEnviado(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MS_USP_GET_MensajeEnviado")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoMensajeEnviado", DbType.Int32, int_Codigo)

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