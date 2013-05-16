Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos

Namespace ModuloPermisos

    Public Class da_BloquesInformacion
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

        Public Function FUN_INS_BloquesInformaciones(ByVal objBloquesInformaciones As be_BloquesInformaciones, _
                                                     ByVal objDetalle As DataSet, _
                                                     ByRef str_Mensaje As String, _
                                                     ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Try

                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_BloquesInformaciones")
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@BI_Descripcion", DbType.String, objBloquesInformaciones.Descripcion)
                dbBase.AddInParameter(dbCommand, "@BI_CodigoGrupoProgramacion", DbType.String, objBloquesInformaciones.CodigoGrupoProgramacion)
                dbBase.AddInParameter(dbCommand, "@BI_Tipo", DbType.Int16, objBloquesInformaciones.Tipo)
                dbBase.AddInParameter(dbCommand, "@BI_Entidad", DbType.String, objBloquesInformaciones.Entidad)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = dbBase.GetParameterValue(dbCommand, "@p_Valor")

                'Detalle Campos Informacion
                If objDetalle.Tables(0) IsNot Nothing Then
                    If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objAsignacionCamposInformacion As be_AsignacionCamposInformacion
                        For Each dr As DataRow In objDetalle.Tables(0).Rows
                            objAsignacionCamposInformacion = New be_AsignacionCamposInformacion
                            objAsignacionCamposInformacion.CodigoBloqueInformacion = int_Valor
                            objAsignacionCamposInformacion.CodigoCampoInformacion = dr.Item("CodigoCampoInformacion")
                            FUN_INS_AsignacionCamposInformacion(objAsignacionCamposInformacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        Next

                    End If
                End If

                Commit()
                Return int_Valor

            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0
            Finally
                Conexion.Close()
            End Try

        End Function

        Public Function FUN_UPD_BloquesInformaciones(ByVal objBloquesInformaciones As be_BloquesInformaciones, _
                                                     ByVal objDetalle As DataSet, _
                                                     ByRef str_Mensaje As String, _
                                                     ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Dim int_Valor As Integer = 0
            Try

                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_UPD_BloquesInformaciones")
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objBloquesInformaciones.CodigoBloqueInformacion)
                dbBase.AddInParameter(dbCommand, "@BI_Descripcion", DbType.String, objBloquesInformaciones.Descripcion)
                dbBase.AddInParameter(dbCommand, "@BI_CodigoGrupoProgramacion", DbType.String, objBloquesInformaciones.CodigoGrupoProgramacion)
                dbBase.AddInParameter(dbCommand, "@BI_Tipo", DbType.Int16, objBloquesInformaciones.Tipo)
                dbBase.AddInParameter(dbCommand, "@BI_Entidad", DbType.String, objBloquesInformaciones.Entidad)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = dbBase.GetParameterValue(dbCommand, "@p_Valor")

                If int_Valor > 0 Then 'Si actualizo la cabecera

                    'Elimino todos los detalles
                    FUN_DEL_AsignacionCamposInformacion(objBloquesInformaciones.CodigoBloqueInformacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                    'Detalle Campos Informacion
                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objAsignacionCamposInformacion As be_AsignacionCamposInformacion
                            For Each dr As DataRow In objDetalle.Tables(0).Rows
                                objAsignacionCamposInformacion = New be_AsignacionCamposInformacion
                                objAsignacionCamposInformacion.CodigoBloqueInformacion = int_Valor
                                objAsignacionCamposInformacion.CodigoCampoInformacion = dr.Item("CodigoCampoInformacion")
                                FUN_INS_AsignacionCamposInformacion(objAsignacionCamposInformacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            Next

                        End If
                    End If

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

        Public Function FUN_DEL_BloquesInformaciones(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_DEL_BloquesInformaciones")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        'CAMPOS INFORMACION - BLOQUES DE INFORMACION
        Private Sub FUN_INS_AsignacionCamposInformacion(ByVal objAsignacionCamposInformacion As be_AsignacionCamposInformacion, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_AsignacionCamposInformacion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoCampoInformacion", DbType.Int16, objAsignacionCamposInformacion.CodigoCampoInformacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBloqueInformacion", DbType.Int16, objAsignacionCamposInformacion.CodigoBloqueInformacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_AsignacionCamposInformacion(ByVal int_CodigoBloqueInformacion As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_DEL_AsignacionCamposInformacion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoBloqueInformacion", DbType.Int16, int_CodigoBloqueInformacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        'ACCIONES DE ACCESO - BLOQUES DE INFORMACION
        Private Sub FUN_INS_AccionesAcceso(ByVal objAccionesAcceso As be_AccionesAcceso, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_AccionesAcceso")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombreAccion", DbType.Int32, objAccionesAcceso.CodigoNombreAccion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBloqueInformacion", DbType.Int32, objAccionesAcceso.CodigoBloqueInformacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_AccionesAcceso(ByVal int_CodigoRelacion As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_DEL_AccionesAcceso")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoRelacion", DbType.Int16, int_CodigoRelacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_BloquesInformacion(ByVal str_Descripcion As String, _
                                                   ByVal int_Estado As Integer, _
                                                   ByVal int_Tipo As Integer, _
                                                   ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CF_USP_LIS_BloquesInformacion")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int16, int_Tipo)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_BloquesInformacion(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CF_USP_GET_BloquesInformacion")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

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
