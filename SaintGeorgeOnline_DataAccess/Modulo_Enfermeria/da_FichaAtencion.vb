Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class da_FichaAtencion
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

        Public Function FUN_UPD_EstadoFichaAtencion(ByVal int_CodigoFichaAtencion As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_UPD_EstadoFichaAtencion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaAtencion", DbType.Int16, int_CodigoFichaAtencion)
            dbBase.AddOutParameter(dbCommand, "@p_EstadoFichaAtencion", DbType.Int16, 10)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)

            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_EstadoFichaAtencion")))

        End Function

        Public Function FUN_INS_FichaAtencion(ByVal objFichaAtencion As be_FichaAtencion, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera

                '   dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaAtencion")


                dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaAtencion_nuevo")

                ''-----------------------------------------------
                ''modficacion : agregar un parametro  de  registro  para el tipo de atencion 
                ''modificado por salcedo vila gaylussac 
                ''parametro  agregado : @p_CodigoTipoAtencion << para agregar el tipo de atencion >>
                ''-----------------------------------------------------------------------------
                'Parámetros de entrada
                'Datos Generales


                dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaPaciente", DbType.Int32, objFichaAtencion.CodigoPersonaPaciente)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersonaPaciente", DbType.Int32, objFichaAtencion.CodigoTipoPersonaPaciente)
                dbBase.AddInParameter(dbCommand, "@p_UsuarioRegistro", DbType.Int32, objFichaAtencion.UsuarioRegistro)


                'Datos Iniciales
                dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, objFichaAtencion.CodigoSede)
                dbBase.AddInParameter(dbCommand, "@p_FechaAtencion", DbType.DateTime, objFichaAtencion.FechaAtencion)
                dbBase.AddInParameter(dbCommand, "@p_HoraIngreso", DbType.DateTime, objFichaAtencion.HoraIngreso)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaEnvia", DbType.Int32, objFichaAtencion.CodigoPersonaEnvia)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoProcedencia", DbType.Int32, objFichaAtencion.CodigoTipoProcedencia)
                dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int32, objFichaAtencion.CodigoCurso)
                dbBase.AddInParameter(dbCommand, "@p_CodigoNombreGrupo", DbType.Int32, objFichaAtencion.CodigoNombreGrupo)
                dbBase.AddInParameter(dbCommand, "@p_DescripcionOtros", DbType.String, objFichaAtencion.DescripcionOtros)

                'Detalle Atencion
                dbBase.AddInParameter(dbCommand, "@p_CodigoCategoriaAtencion", DbType.Int16, objFichaAtencion.CodigoCategoriaAtencion)
                '------------------------------------------------------------------------------------------------------------------
                ''-------campo agreagado por salcedo vila gaylussac --
                '' fecha de modificacion :06/03/2013
                ''------------------------------------------------------------------------------------------------------------------
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoAtencion", DbType.Int16, objFichaAtencion.CodigoTipoAtencion)
                ''------------------------------------------------------------------------------------------------------------------

                dbBase.AddInParameter(dbCommand, "@p_SintomasDescripcion", DbType.String, objFichaAtencion.SintomasDescripcion)
                dbBase.AddInParameter(dbCommand, "@p_Observaciones", DbType.String, objFichaAtencion.Observaciones)
                dbBase.AddInParameter(dbCommand, "@p_DescansarEnfermeria", DbType.Int16, objFichaAtencion.DescansarEnfermeria)

                'Datos Finales
                dbBase.AddInParameter(dbCommand, "@p_CodigoIndicacionMedica", DbType.Int16, objFichaAtencion.CodigoIndicacionMedica)
                dbBase.AddInParameter(dbCommand, "@p_HoraSalida", DbType.DateTime, objFichaAtencion.HoraSalida)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaRecoje", DbType.Int32, objFichaAtencion.CodigoPersonaRecoje)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersonaRecoge", DbType.Int32, objFichaAtencion.CodigoTipoPersonaRecoge)

                dbBase.AddInParameter(dbCommand, "@p_Completado", DbType.Int16, objFichaAtencion.Completado)
                dbBase.AddInParameter(dbCommand, "@p_TieneMatricula", DbType.Int16, objFichaAtencion.TieneMatricula)
                dbBase.AddInParameter(dbCommand, "@p_PermisoModificar", DbType.Int16, objFichaAtencion.PermisoModificar)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure : Registro Cabecera
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Detalle Ficha Atencion 
                'DS.Tables(0) : Diagnostico
                If objDetalle.Tables(0) IsNot Nothing Then
                    If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaAtencionDiagnostico As be_RelacionFichaAtencionesDiagnosticos

                        For Each dr As DataRow In objDetalle.Tables(0).Rows

                            objFichaAtencionDiagnostico = New be_RelacionFichaAtencionesDiagnosticos
                            objFichaAtencionDiagnostico.CodigoFichaAtencion = int_Valor
                            objFichaAtencionDiagnostico.CodigoDiagnostico = dr.Item("Codigo")
                            FUN_INS_FichaAtencionDiagnostico(objFichaAtencionDiagnostico, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(1) : Procedimiento
                If objDetalle.Tables(1) IsNot Nothing Then
                    If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaAtencionProcedimientoRealizado As be_RelacionFichaAtencionesProcedimientosRealizados

                        For Each dr As DataRow In objDetalle.Tables(1).Rows

                            objFichaAtencionProcedimientoRealizado = New be_RelacionFichaAtencionesProcedimientosRealizados
                            objFichaAtencionProcedimientoRealizado.CodigoFichaAtencion = int_Valor
                            objFichaAtencionProcedimientoRealizado.CodigoProcedimientoRealizado = dr.Item("Codigo")
                            FUN_INS_FichaAtencionProcedimientoRealizado(objFichaAtencionProcedimientoRealizado, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(2) : Medicamento
                If objDetalle.Tables(2) IsNot Nothing Then
                    If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaAtencionMedicamento As BE_RelacionFichaAtencionesMedicamentos

                        For Each dr As DataRow In objDetalle.Tables(2).Rows

                            objFichaAtencionMedicamento = New BE_RelacionFichaAtencionesMedicamentos
                            objFichaAtencionMedicamento.CodigoFichaAtencion = int_Valor
                            objFichaAtencionMedicamento.CodigoMedicamento = dr.Item("Codigo")
                            objFichaAtencionMedicamento.CantidadSuministrada = dr.Item("Cantidad")
                            FUN_INS_FichaAtencionMedicamento(objFichaAtencionMedicamento, objFichaAtencion.CodigoSede, tran, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                Commit()

                Return int_Valor 'Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro." 'ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_UPD_FichaAtencion(ByVal objFichaAtencion As be_FichaAtencion, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera
                ''dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_UPD_FichaAtencion")

                ''cambiar  por 
                ''cambiado por un nuvo procedure para probar el SP para no afectar al procedure de produccion 
                dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_UPD_FichaAtencion_nuevo")




                'Parámetros de entrada
                'Datos Generales

                'dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaPaciente", DbType.Int16, objFichaAtencion.CodigoPersonaPaciente)
                'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersonaPaciente", DbType.Int16, objFichaAtencion.CodigoTipoPersonaPaciente)

                'Datos Iniciales
                dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, objFichaAtencion.CodigoFichaAtencion)
                dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, objFichaAtencion.CodigoSede)
                dbBase.AddInParameter(dbCommand, "@p_FechaAtencion", DbType.DateTime, objFichaAtencion.FechaAtencion)
                dbBase.AddInParameter(dbCommand, "@p_HoraIngreso", DbType.DateTime, objFichaAtencion.HoraIngreso)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaEnvia", DbType.Int32, objFichaAtencion.CodigoPersonaEnvia)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoProcedencia", DbType.Int32, objFichaAtencion.CodigoTipoProcedencia)
                dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int32, objFichaAtencion.CodigoCurso)
                dbBase.AddInParameter(dbCommand, "@p_CodigoNombreGrupo", DbType.Int32, objFichaAtencion.CodigoNombreGrupo)
                dbBase.AddInParameter(dbCommand, "@p_DescripcionOtros", DbType.String, objFichaAtencion.DescripcionOtros)

                'Detalle Atencion
                dbBase.AddInParameter(dbCommand, "@p_CodigoCategoriaAtencion", DbType.Int16, objFichaAtencion.CodigoCategoriaAtencion)
                ''agregado  por salcedo vila gaylussac 
                ''fecha de modificacion 06/03/2013
                ''actualizar el campos de tipo atencion medica 
                ''----------------------------------------------------------------------------------------------------------------
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoAtencion", DbType.Int16, objFichaAtencion.CodigoTipoAtencion)
                ''----------------------------------------------------------------------------------------------------------------

                dbBase.AddInParameter(dbCommand, "@p_SintomasDescripcion", DbType.String, objFichaAtencion.SintomasDescripcion)
                dbBase.AddInParameter(dbCommand, "@p_Observaciones", DbType.String, objFichaAtencion.Observaciones)
                dbBase.AddInParameter(dbCommand, "@p_DescansarEnfermeria", DbType.Int16, objFichaAtencion.DescansarEnfermeria)

                'Datos Finales
                dbBase.AddInParameter(dbCommand, "@p_CodigoIndicacionMedica", DbType.Int16, objFichaAtencion.CodigoIndicacionMedica)
                dbBase.AddInParameter(dbCommand, "@p_HoraSalida", DbType.DateTime, objFichaAtencion.HoraSalida)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaRecoje", DbType.Int32, objFichaAtencion.CodigoPersonaRecoje)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersonaRecoge", DbType.Int32, objFichaAtencion.CodigoTipoPersonaRecoge)

                dbBase.AddInParameter(dbCommand, "@p_Completado", DbType.Int16, objFichaAtencion.Completado)
                dbBase.AddInParameter(dbCommand, "@p_TieneMatricula", DbType.Int16, objFichaAtencion.TieneMatricula)
                dbBase.AddInParameter(dbCommand, "@p_PermisoModificar", DbType.Int16, objFichaAtencion.PermisoModificar)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure : Registro Cabecera
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Elimino todos los detalles
                FUN_DEL_FichaAtencionDiagnostico(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaAtencionProcedimientoRealizado(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaAtencionMedicamento(int_Valor, objFichaAtencion.CodigoSede, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                'Agrego los nuevos detalles de la Ficha Atencion 
                'DS.Tables(0) : Diagnostico
                If objDetalle.Tables(0) IsNot Nothing Then
                    If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaAtencionDiagnostico As be_RelacionFichaAtencionesDiagnosticos

                        For Each dr As DataRow In objDetalle.Tables(0).Rows

                            objFichaAtencionDiagnostico = New be_RelacionFichaAtencionesDiagnosticos
                            objFichaAtencionDiagnostico.CodigoFichaAtencion = int_Valor
                            objFichaAtencionDiagnostico.CodigoDiagnostico = dr.Item("Codigo")
                            FUN_INS_FichaAtencionDiagnostico(objFichaAtencionDiagnostico, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(1) : Procedimiento
                If objDetalle.Tables(1) IsNot Nothing Then
                    If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaAtencionProcedimientoRealizado As be_RelacionFichaAtencionesProcedimientosRealizados

                        For Each dr As DataRow In objDetalle.Tables(1).Rows

                            objFichaAtencionProcedimientoRealizado = New be_RelacionFichaAtencionesProcedimientosRealizados
                            objFichaAtencionProcedimientoRealizado.CodigoFichaAtencion = int_Valor
                            objFichaAtencionProcedimientoRealizado.CodigoProcedimientoRealizado = dr.Item("Codigo")
                            FUN_INS_FichaAtencionProcedimientoRealizado(objFichaAtencionProcedimientoRealizado, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(2) : Medicamento
                If objDetalle.Tables(2) IsNot Nothing Then
                    If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaAtencionMedicamento As BE_RelacionFichaAtencionesMedicamentos

                        For Each dr As DataRow In objDetalle.Tables(2).Rows

                            objFichaAtencionMedicamento = New BE_RelacionFichaAtencionesMedicamentos
                            objFichaAtencionMedicamento.CodigoFichaAtencion = int_Valor
                            objFichaAtencionMedicamento.CodigoMedicamento = dr.Item("Codigo")
                            objFichaAtencionMedicamento.CantidadSuministrada = dr.Item("Cantidad")
                            FUN_INS_FichaAtencionMedicamento(objFichaAtencionMedicamento, objFichaAtencion.CodigoSede, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                Commit()

                Return int_Valor 'Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Catch ex As Exception

                str_Mensaje = ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_DEL_FichaAtencion(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaAtencion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Private Sub FUN_INS_FichaAtencionDiagnostico(ByVal objFichaAtencionDiagnostico As be_RelacionFichaAtencionesDiagnosticos, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaAtencionDiagnostico")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaAtencion", DbType.Int16, objFichaAtencionDiagnostico.CodigoFichaAtencion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDiagnostico", DbType.Int16, objFichaAtencionDiagnostico.CodigoDiagnostico)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Public Sub FUN_INS_FichaAtencionProcedimientoRealizado(ByVal objFichaAtencionProcedimientoRealizado As be_RelacionFichaAtencionesProcedimientosRealizados, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaAtencionProcedimiento")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaAtencion", DbType.Int16, objFichaAtencionProcedimientoRealizado.CodigoFichaAtencion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoProcedimientoRealizado", DbType.Int16, objFichaAtencionProcedimientoRealizado.CodigoProcedimientoRealizado)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Public Sub FUN_INS_FichaAtencionMedicamento(ByVal objFichaAtencionMedicamento As BE_RelacionFichaAtencionesMedicamentos, ByVal int_CodigoAlmacen As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaAtencionMedicamento")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaAtencion", DbType.Int16, objFichaAtencionMedicamento.CodigoFichaAtencion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMedicamento", DbType.Int16, objFichaAtencionMedicamento.CodigoMedicamento)
            dbBase.AddInParameter(dbCommand, "@p_CantidadSuministrada", DbType.Int16, objFichaAtencionMedicamento.CantidadSuministrada)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, int_CodigoAlmacen)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_FichaAtencionDiagnostico(ByVal int_CodigoFichaAtencion As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaAtencionDiagnostico")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaAtencion", DbType.Int16, int_CodigoFichaAtencion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_FichaAtencionProcedimientoRealizado(ByVal int_CodigoFichaAtencion As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaAtencionProcedimiento")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaAtencion", DbType.Int16, int_CodigoFichaAtencion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_FichaAtencionMedicamento(ByVal int_CodigoFichaAtencion As Integer, ByVal int_CodigoAlmacen As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaAtencionMedicamento")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaAtencion", DbType.Int16, int_CodigoFichaAtencion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, int_CodigoAlmacen)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_FichaAtencion( _
                ByVal int_CodigoTipoPaciente As Integer, _
                ByVal str_Nombre As String, _
                ByVal str_ApellidoPaterno As String, _
                ByVal str_ApellidoMaterno As String, _
                ByVal int_AlumnoNivel As Integer, _
                ByVal int_AlumnoSubNivel As Integer, _
                ByVal int_AlumnoGrado As Integer, _
                ByVal int_AlumnoAula As Integer, _
                ByVal str_FamiliarNombre As String, _
                ByVal str_FamiliarApellidoPaterno As String, _
                ByVal str_FamiliarApellidoMaterno As String, _
                ByVal int_FamiliarAlumnoNivel As Integer, _
                ByVal int_FamiliarAlumnoSubNivel As Integer, _
                ByVal int_FamiliarAlumnoGrado As Integer, _
                ByVal int_FamiliarAlumnoAula As Integer, _
                ByVal dt_FechaRangoInicial As Date, _
                ByVal dt_FechaRangoFinal As Date, _
                ByVal int_Sede As Integer, _
                ByVal int_Estado As Integer, _
                ByVal int_EstadoRegistro As Integer, _
                ByVal int_CodigoUsuario As Integer, _
                ByVal int_CodigoTipoUsuario As Integer, _
                ByVal int_CodigoModulo As Integer, _
                ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_FichasAtencion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_TipoPersona", DbType.Int16, int_CodigoTipoPaciente)
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, str_ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, str_ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, str_Nombre)
            dbBase.AddInParameter(cmd, "@p_AlumnoNivel", DbType.Int16, int_AlumnoNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoSubnivel", DbType.Int16, int_AlumnoSubNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoGrado", DbType.Int16, int_AlumnoGrado)
            dbBase.AddInParameter(cmd, "@p_AlumnoAula", DbType.Int16, int_AlumnoAula)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarApellidoPaterno", DbType.String, str_FamiliarApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarApellidoMaterno", DbType.String, str_FamiliarApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarNombres", DbType.String, str_FamiliarNombre)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarNivel", DbType.Int16, int_FamiliarAlumnoNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarSubnivel", DbType.Int16, int_FamiliarAlumnoSubNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarGrado", DbType.Int16, int_FamiliarAlumnoGrado)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarAula", DbType.Int16, int_FamiliarAlumnoAula)
            dbBase.AddInParameter(cmd, "@p_FechaRangoInicial", DbType.DateTime, dt_FechaRangoInicial)
            dbBase.AddInParameter(cmd, "@p_FechaRangoFinal", DbType.DateTime, dt_FechaRangoFinal)
            dbBase.AddInParameter(cmd, "@p_Sede", DbType.Int16, int_Sede)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)
            dbBase.AddInParameter(cmd, "@p_EstadoRegistro", DbType.Int16, int_EstadoRegistro)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_FichaAtencion(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_GET_FichaAtencion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DatosRelevantesFichaAtencion(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_DatosRelevantesAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_CodigoPersona)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DatosSeguroFichaAtencion(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_DatosSeguro")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_CodigoPersona)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
        Public Function FUN_LIS_DatosFichaAtencion(ByVal str_CodigoAlumno As String, ByVal str_AnioAcademico As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_DatosFichaAtencion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_AnioAcademico", DbType.String, str_AnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_ContactosFichaAtencion(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_ContactosEmergenciaAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_CodigoPersona)

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
