Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos

Namespace ModuloPermisos
    Public Class da_AsignacionPermisosPerfiles
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

        Public Function FUN_INS_DetalleAsignacionPermisosPerfiles(ByVal objDetalle As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 1

            Try
                BeginTransaction()

                For Each dr As DataRow In objDetalle.Rows

                    Dim obj_be_AsignacionPermisosPerfiles As be_AsignacionPermisosPerfiles

                    obj_be_AsignacionPermisosPerfiles = New be_AsignacionPermisosPerfiles
                    obj_be_AsignacionPermisosPerfiles.CodigoAsignacion = dr.Item("CodigoAsignacion").ToString
                    obj_be_AsignacionPermisosPerfiles.CodigoPerfil = dr.Item("CodigoPerfil").ToString
                    obj_be_AsignacionPermisosPerfiles.CodigoNombreAccion = dr.Item("CodigoNombreAccion").ToString
                    obj_be_AsignacionPermisosPerfiles.Habilitado = dr.Item("Habilitado").ToString

                    FUN_INS_AsignacionPermisosPerfiles(obj_be_AsignacionPermisosPerfiles, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                Next

                Commit()
                str_Mensaje = "Operación exitosa."
                Return int_Valor
            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try
        End Function

        Private Function FUN_INS_AsignacionPermisosPerfiles(ByVal obj_be_AsignacionPermisosPerfiles As be_AsignacionPermisosPerfiles, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_AsignacionPermisosPerfiles")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacion", DbType.Int16, obj_be_AsignacionPermisosPerfiles.CodigoAsignacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPerfil", DbType.Int16, obj_be_AsignacionPermisosPerfiles.CodigoPerfil)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombreAccion", DbType.Int16, obj_be_AsignacionPermisosPerfiles.CodigoNombreAccion)
            dbBase.AddInParameter(dbCommand, "@p_Habilitado", DbType.Int16, obj_be_AsignacionPermisosPerfiles.Habilitado)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
            int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Return int_Valor
        End Function

        'Agregar permisos por usuarios
        Public Function FUN_INS_AsignacionPermisosParaUsuarios(ByVal int_CodigoTrabajador As Integer, ByVal int_TipoAccion As Integer, ByVal objDetalle As DataTable, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 1

            Try
                BeginTransaction()

                'Registro la cabecera
                dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_PerfilesPorTipo")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)
                dbBase.AddInParameter(dbCommand, "@p_TipoAccion", DbType.Int32, int_TipoAccion)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure : Registro Cabecera
                dbBase.ExecuteScalar(dbCommand, tran)

                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor"))) ' Código del nuevo perfil registrado

                If int_Valor > 0 Then ' Si se registro el nuevo perfil
                    If objDetalle IsNot Nothing Then
                        If objDetalle.Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim obj_be_AsignacionPermisosPerfiles As be_AsignacionPermisosPerfiles

                            For Each dr As DataRow In objDetalle.Rows

                                obj_be_AsignacionPermisosPerfiles = New be_AsignacionPermisosPerfiles
                                obj_be_AsignacionPermisosPerfiles.CodigoAccion = dr.Item("CodigoAccion")
                                obj_be_AsignacionPermisosPerfiles.CodigoPerfil = int_Valor
                                FUN_INS_DetalleAsignacionPermisosParaUsuarios(obj_be_AsignacionPermisosPerfiles, int_CodigoTrabajador, int_TipoAccion, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If
                End If

                Commit()
                str_Mensaje = "Operación exitosa."

                Return int_Valor
            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try
        End Function

        Private Sub FUN_INS_DetalleAsignacionPermisosParaUsuarios(ByVal obj_be_AsignacionPermisosPerfiles As be_AsignacionPermisosPerfiles, ByVal int_CodigoTrabajador As Integer, ByVal int_TipoAccion As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_AsignacionPermisosPorUsuarios")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAccion", DbType.Int16, obj_be_AsignacionPermisosPerfiles.CodigoAccion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPerfil", DbType.Int16, obj_be_AsignacionPermisosPerfiles.CodigoPerfil)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)
            dbBase.AddInParameter(dbCommand, "@p_TipoAccion", DbType.Int32, int_TipoAccion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_ConfiguracionPerfil(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CF_USP_GET_ConfiguracionPerfil")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_PlantillaPerfilPorTrabajador(ByVal int_CodigoPerfil As Integer, ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CF_USP_LIS_PlantillaConfiguracionPerfiles")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPerfil", DbType.Int16, int_CodigoPerfil)
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int16, int_CodigoTrabajador)

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

