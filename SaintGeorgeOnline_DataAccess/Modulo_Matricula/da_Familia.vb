Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula

Namespace ModuloMatricula

    Public Class da_Familia
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

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Familias(ByVal str_Descripcion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_Familia")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_Familia(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_Familia")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.String, str_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        ' updated 24/01/12
        Public Function FUN_GET_DatosFamilia(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_DatosFamilia")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.String, str_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_VAL_EliminarIntegranteFamilia(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_VAL_EliminarIntegranteFamilia")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoIntegranteFamiliar", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_FamiliaHijos(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_FamiliaHijos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.String, str_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' update
        Public Function FUN_GET_FamiliaresConSolicitudActualizacion(ByVal str_Codigo As String, ByVal int_AnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_FamiliaresConSolicitudActualizacion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.String, str_Codigo)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' entrevista
        Public Function FUN_LIS_FamiliasPorPeriodoGradoYAula(ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
                                                             ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_FamiliaPorPeriodoGradoYAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

#Region "Metodos Transaccionales"

        'update
        Public Function FUN_INS_Familia(ByVal str_NombreFamilia As String, ByVal str_AnioAcademico As String, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_Familias")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_NombreFamilia", DbType.String, str_NombreFamilia)
            dbBase.AddInParameter(dbCommand, "@p_AnioAcademico", DbType.String, str_AnioAcademico)

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

        Private Function FUN_UPD_Familia(ByVal int_CodigoFamilia As Integer, ByVal str_NombreFamilia As String, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_Familias")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(dbCommand, "@p_NombreFamilia", DbType.String, str_NombreFamilia)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Return int_Valor

        End Function

        'update
        Public Function FUN_UPD_FamiliaIntegrantesYAlumnos(ByVal int_CodigoFamilia As Integer, _
                                                           ByVal str_NombreFamilia As String, _
                                                           ByVal dt_Familiares As DataTable, _
                                                           ByVal dt_Alumnos As DataTable, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0
            Dim int_CodIntegranteFamilia As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Actualizo el nombre de la familia
                int_Valor = FUN_UPD_Familia(int_CodigoFamilia, str_NombreFamilia, tran, str_MiMensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)


                For Each drF As DataRow In dt_Familiares.Rows
                    If drF.Item("Estado") = 1 Then ' Registro Activo
                        'update/insert - integrante familia
                        Dim obj_BE_IntegrantesFamilia As New be_IntegrantesFamilia
                        obj_BE_IntegrantesFamilia.CodigoFamilia = int_CodigoFamilia
                        obj_BE_IntegrantesFamilia.CodigoIntegranteFamilia = drF.Item("CodigoIntegranteFamilia")
                        obj_BE_IntegrantesFamilia.CodigoFamiliar = drF.Item("CodigoFamiliar")
                        obj_BE_IntegrantesFamilia.CodigoParentesco = drF.Item("CodigoParentesco")

                        int_CodIntegranteFamilia = FUN_INS_IntegranteFamilia(obj_BE_IntegrantesFamilia, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        For Each drA As DataRow In dt_Alumnos.Rows
                            If drA.Item("Estado") = 1 Then ' Registro Activo
                                'update/insert - relacion alumno con integrante de familia
                                Dim obj_BE_RelacionAlumnoIntegranteFamilia As New be_DetalleIntegrantesFamilia
                                obj_BE_RelacionAlumnoIntegranteFamilia.CodigoIntegranteFamilia = int_CodIntegranteFamilia
                                obj_BE_RelacionAlumnoIntegranteFamilia.CodigoAlumno = drA.Item("CodigoAlumno")
                                obj_BE_RelacionAlumnoIntegranteFamilia.CodigoFamiliarResponsablePago = drA.Item("CodigoFamiliarResponsablePago")
                                FUN_INS_RelacionAlumnosIntegranteFamilia(obj_BE_RelacionAlumnoIntegranteFamilia, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            Else
                                If drA.Item("Tipo") = "R" Then ' Solo a los registros reales
                                    'delete - vinculos de alumno con el integrante de familia en la tabla de relacion alumno e integrante familia
                                    Dim obj_BE_RelacionAlumnoIntegranteFamilia As New be_DetalleIntegrantesFamilia
                                    obj_BE_RelacionAlumnoIntegranteFamilia.CodigoIntegranteFamilia = int_CodIntegranteFamilia
                                    obj_BE_RelacionAlumnoIntegranteFamilia.CodigoAlumno = drA.Item("CodigoAlumno")
                                    FUN_DEL_RelacionAlumnosIntegranteFamilia(obj_BE_RelacionAlumnoIntegranteFamilia, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                End If
                            End If
                        Next

                    Else ' drF.Item("Estado") = 0 : ' Registro Inactivo
                        If drF.Item("Tipo") = "R" Then ' Solo a los registros reales
                            'delete - integrante familia y a todos los alumnos vinculados al codigo de integrante familia del familiar
                            Dim obj_BE_IntegrantesFamilia As New be_IntegrantesFamilia
                            obj_BE_IntegrantesFamilia.CodigoIntegranteFamilia = drF.Item("CodigoIntegranteFamilia")
                            FUN_DEL_IntegranteFamiliaYAlumnos(obj_BE_IntegrantesFamilia, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        End If
                    End If
                Next

                Commit()
                str_Mensaje = "Registro exitoso."
                int_Valor = int_CodigoFamilia
                Return int_Valor

            Catch ex As Exception
                str_Mensaje = ex.Message
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try
        End Function

        'update - Registro de Integrantes de la Familia
        Private Function FUN_INS_IntegranteFamilia(ByVal obj_BE_IntegrantesFamilia As be_IntegrantesFamilia, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_IntegrantesFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoIntegranteFamilia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamilia", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoFamilia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_CodigoParentesco", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoParentesco)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
            int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Return int_Valor

        End Function

        'update - Eliminacion de Integrantes de la Familia y Alumnos vinculados a los Familiares
        Public Sub FUN_DEL_IntegranteFamiliaYAlumnos(ByVal obj_BE_IntegrantesFamilia As be_IntegrantesFamilia, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_IntegrantesFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoIntegranteFamilia)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        'update - Registro de Alumnos vinculados a los Familiares
        Private Sub FUN_INS_RelacionAlumnosIntegranteFamilia(ByVal obj_BE_DetalleIntegrantesFamilia As be_DetalleIntegrantesFamilia, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_RelacionAlumnoIntegrantesFamilia")

            'Parámetros de entrada
            'dbBase.AddInParameter(dbCommand, "@p_CodigoRelAlumnosFamiliares", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoRelAlumnosFamiliares)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoIntegranteFamilia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliarResponsablePago", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoFamiliarResponsablePago)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
        End Sub

        'update - Eliminacion del vinculo alumno con el integrantes de la familia
        Public Sub FUN_DEL_RelacionAlumnosIntegranteFamilia(ByVal obj_BE_DetalleIntegrantesFamilia As be_DetalleIntegrantesFamilia, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_RelacionAlumnoIntegrantesFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoIntegranteFamilia)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub




        Public Function FUN_UPD_IntegrantesFamilia(ByVal dt_Familiares As DataTable, ByVal dt_DetalleFamiliares As DataTable, ByVal dt_Detalle_eliminar As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0
            Dim int_CodigoIntegrante As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                'PASO 1: Eliminar FAMILIARES
                For Each dr As DataRow In dt_Detalle_eliminar.Rows
                    Dim obj_BE_IntegrantesFamilia As New be_IntegrantesFamilia
                    obj_BE_IntegrantesFamilia.CodigoIntegranteFamilia = dr.Item("CodigoIntegranteFamilia")
                    FUN_DEL_IntegrantesFamilia(obj_BE_IntegrantesFamilia, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                Next

                'PASO 2: Agregar y Actualizar FAMILIARES
                For Each dr As DataRow In dt_Familiares.Rows

                    Dim obj_BE_IntegrantesFamilia As New be_IntegrantesFamilia
                    Dim IdTabla As Integer = 0

                    IdTabla = dr.Item("ID_TABLA")
                    obj_BE_IntegrantesFamilia.CodigoIntegranteFamilia = dr.Item("CodigoIntegranteFamilia")
                    obj_BE_IntegrantesFamilia.CodigoFamilia = dr.Item("CodigoFamilia")
                    obj_BE_IntegrantesFamilia.CodigoFamiliar = dr.Item("CodigoFamiliar")
                    obj_BE_IntegrantesFamilia.CodigoParentesco = dr.Item("CodigoParentesco")
                    int_CodigoIntegrante = FUN_INS_Integrante(obj_BE_IntegrantesFamilia, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                    For Each dr_det As DataRow In dt_DetalleFamiliares.Rows
                        If dr_det.Item("ID_TABLA") = IdTabla Then
                            Dim obj_BE_DetalleIntegrantesFamilia As New be_DetalleIntegrantesFamilia
                            obj_BE_DetalleIntegrantesFamilia.CodigoAlumno = dr_det.Item("CodigoAlumno")
                            obj_BE_DetalleIntegrantesFamilia.CodigoIntegranteFamilia = int_CodigoIntegrante
                            obj_BE_DetalleIntegrantesFamilia.CodigoViveConAlumno = dr_det.Item("CodigoViveConAlumno")
                            obj_BE_DetalleIntegrantesFamilia.CodigoRelAlumnosFamiliares = dr_det.Item("CodigoRelAlumnosFamiliares")

                            FUN_INS_DetalleIntegrantes(obj_BE_DetalleIntegrantesFamilia, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        End If
                    Next

                Next

                Commit()
                str_Mensaje = "Registro exitoso."
                Return int_Valor

            Catch ex As Exception
                str_Mensaje = ex.Message
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try
        End Function

        Public Sub FUN_DEL_IntegrantesFamilia(ByVal obj_BE_IntegrantesFamilia As be_IntegrantesFamilia, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)
            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_IntegrantesFamilia")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoIntegranteFamilia)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
        End Sub

        Private Function FUN_INS_Integrante(ByVal obj_BE_IntegrantesFamilia As be_IntegrantesFamilia, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_IntegrantesFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoIntegranteFamilia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamilia", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoFamilia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_CodigoParentesco", DbType.Int32, obj_BE_IntegrantesFamilia.CodigoParentesco)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
            int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Return int_Valor

        End Function

        Private Sub FUN_INS_DetalleIntegrantes(ByVal obj_BE_DetalleIntegrantesFamilia As be_DetalleIntegrantesFamilia, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_DetalleIntegrantesFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoRelAlumnosFamiliares", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoRelAlumnosFamiliares)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoIntegranteFamilia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoViveConAlumno", DbType.Int32, obj_BE_DetalleIntegrantesFamilia.CodigoViveConAlumno)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
        End Sub

#End Region

    End Class

End Namespace
