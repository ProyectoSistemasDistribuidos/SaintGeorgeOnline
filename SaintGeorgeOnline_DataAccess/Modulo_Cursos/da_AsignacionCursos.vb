Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos

Namespace ModuloCursos

    Public Class da_AsignacionCursos
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

        Public Function FUN_INS_AsignacionCursos(ByVal objAsignacionCursos As be_AsignacionCursos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_AsignacionCursos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int32, objAsignacionCursos.CodigoCurso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaCalificacion", DbType.Int32, objAsignacionCursos.CodigoFormaCalificacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, objAsignacionCursos.CodigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objAsignacionCursos.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCursoPadre", DbType.Int32, objAsignacionCursos.CodigoAsignacionCursoPadre)
            dbBase.AddInParameter(dbCommand, "@p_OrdenActa", DbType.Int32, IIf(objAsignacionCursos.OrdenActa = 0, DBNull.Value, objAsignacionCursos.OrdenActa))
            dbBase.AddInParameter(dbCommand, "@p_OrdenReporte", DbType.Int32, IIf(objAsignacionCursos.OrdenReporte = 0, DBNull.Value, objAsignacionCursos.OrdenReporte))
            dbBase.AddInParameter(dbCommand, "@p_NotaAprobatoria", DbType.Int32, IIf(objAsignacionCursos.NotaAprobatoria = 0, DBNull.Value, objAsignacionCursos.NotaAprobatoria))
            dbBase.AddInParameter(dbCommand, "@p_MaxComponentes", DbType.Int32, IIf(objAsignacionCursos.MaxComponentes = 0, DBNull.Value, objAsignacionCursos.MaxComponentes))
            dbBase.AddInParameter(dbCommand, "@p_MaxIndicadores", DbType.Int32, IIf(objAsignacionCursos.MaxIndicadores = 0, DBNull.Value, objAsignacionCursos.MaxIndicadores))
            dbBase.AddInParameter(dbCommand, "@p_MaxSubIndicadores", DbType.Int32, IIf(objAsignacionCursos.MaxSubIndicadores = 0, DBNull.Value, objAsignacionCursos.MaxSubIndicadores))
            dbBase.AddInParameter(dbCommand, "@p_MaxCriterios", DbType.Int32, IIf(objAsignacionCursos.MaxCriterios = 0, DBNull.Value, objAsignacionCursos.MaxCriterios))
            dbBase.AddInParameter(dbCommand, "@p_MaxEvaluaciones", DbType.Int32, IIf(objAsignacionCursos.MaxEvaluaciones = 0, DBNull.Value, objAsignacionCursos.MaxEvaluaciones))
            dbBase.AddInParameter(dbCommand, "@p_AutogenerarGrupo", DbType.Int32, objAsignacionCursos.AutogenerarGrupo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Private Function FUN_INS_AsignacionCursosInterno(ByVal objAsignacionCursosInternos As be_AsignacionCursos, ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_AsignacionCursos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int32, objAsignacionCursosInternos.CodigoCurso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaCalificacion", DbType.Int32, objAsignacionCursosInternos.CodigoFormaCalificacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, objAsignacionCursosInternos.CodigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objAsignacionCursosInternos.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCursoPadre", DbType.Int32, objAsignacionCursosInternos.CodigoAsignacionCursoPadre)
            dbBase.AddInParameter(dbCommand, "@p_OrdenActa", DbType.Int32, IIf(objAsignacionCursosInternos.OrdenActa = 0, DBNull.Value, objAsignacionCursosInternos.OrdenActa))
            dbBase.AddInParameter(dbCommand, "@p_OrdenReporte", DbType.Int32, IIf(objAsignacionCursosInternos.OrdenReporte = 0, DBNull.Value, objAsignacionCursosInternos.OrdenReporte))
            dbBase.AddInParameter(dbCommand, "@p_NotaAprobatoria", DbType.Int32, IIf(objAsignacionCursosInternos.NotaAprobatoria = 0, DBNull.Value, objAsignacionCursosInternos.NotaAprobatoria))
            dbBase.AddInParameter(dbCommand, "@p_MaxComponentes", DbType.Int32, IIf(objAsignacionCursosInternos.MaxComponentes = 0, DBNull.Value, objAsignacionCursosInternos.MaxComponentes))
            dbBase.AddInParameter(dbCommand, "@p_MaxIndicadores", DbType.Int32, IIf(objAsignacionCursosInternos.MaxIndicadores = 0, DBNull.Value, objAsignacionCursosInternos.MaxIndicadores))
            dbBase.AddInParameter(dbCommand, "@p_MaxSubIndicadores", DbType.Int32, IIf(objAsignacionCursosInternos.MaxSubIndicadores = 0, DBNull.Value, objAsignacionCursosInternos.MaxSubIndicadores))
            dbBase.AddInParameter(dbCommand, "@p_MaxCriterios", DbType.Int32, IIf(objAsignacionCursosInternos.MaxCriterios = 0, DBNull.Value, objAsignacionCursosInternos.MaxCriterios))
            dbBase.AddInParameter(dbCommand, "@p_MaxEvaluaciones", DbType.Int32, IIf(objAsignacionCursosInternos.MaxEvaluaciones = 0, DBNull.Value, objAsignacionCursosInternos.MaxEvaluaciones))
            dbBase.AddInParameter(dbCommand, "@p_AutogenerarGrupo", DbType.Int32, objAsignacionCursosInternos.AutogenerarGrupo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Return int_Valor

        End Function

        Public Function FUN_UPD_AsignacionCursos(ByVal objAsignacionCursos As be_AsignacionCursos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_AsignacionCursos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCurso", DbType.Int32, objAsignacionCursos.CodigoAsignacionCurso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFormaCalificacion", DbType.Int32, objAsignacionCursos.CodigoFormaCalificacion)
            dbBase.AddInParameter(dbCommand, "@p_NotaAprobatoria", DbType.Int32, IIf(objAsignacionCursos.NotaAprobatoria = 0, DBNull.Value, objAsignacionCursos.NotaAprobatoria))
            dbBase.AddInParameter(dbCommand, "@p_MaxComponentes", DbType.Int32, IIf(objAsignacionCursos.MaxComponentes = 0, DBNull.Value, objAsignacionCursos.MaxComponentes))
            dbBase.AddInParameter(dbCommand, "@p_MaxIndicadores", DbType.Int32, IIf(objAsignacionCursos.MaxIndicadores = 0, DBNull.Value, objAsignacionCursos.MaxIndicadores))
            dbBase.AddInParameter(dbCommand, "@p_MaxSubIndicadores", DbType.Int32, IIf(objAsignacionCursos.MaxSubIndicadores = 0, DBNull.Value, objAsignacionCursos.MaxSubIndicadores))
            dbBase.AddInParameter(dbCommand, "@p_MaxCriterios", DbType.Int32, IIf(objAsignacionCursos.MaxCriterios = 0, DBNull.Value, objAsignacionCursos.MaxCriterios))
            dbBase.AddInParameter(dbCommand, "@p_MaxEvaluaciones", DbType.Int32, IIf(objAsignacionCursos.MaxEvaluaciones = 0, DBNull.Value, objAsignacionCursos.MaxEvaluaciones))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure : Registro Cabecera
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_AsignacionCursosOrden(ByVal objAsignacionCursos As be_AsignacionCursos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_AsignacionCursosOrden")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCurso", DbType.Int32, objAsignacionCursos.CodigoAsignacionCurso)
            dbBase.AddInParameter(dbCommand, "@p_OrdenActa", DbType.Int32, objAsignacionCursos.OrdenActa) 'IIf(objAsignacionCursos.OrdenActa = 0, DBNull.Value, objAsignacionCursos.OrdenActa))
            dbBase.AddInParameter(dbCommand, "@p_OrdenReporte", DbType.Int32, IIf(objAsignacionCursos.OrdenReporte = 0, DBNull.Value, objAsignacionCursos.OrdenReporte))

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure : Registro Cabecera
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_DEL_AsignacionCursos(ByVal objAsignacionCursos As be_AsignacionCursos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_DEL_AsignacionCursos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCurso", DbType.Int32, objAsignacionCursos.CodigoAsignacionCurso)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure 
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionCursos(ByVal int_CodigoGrado As Integer, _
                                       ByVal int_CodigoAnioAcademico As Integer, _
                                       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionCursos")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)


        End Function

        Public Function FUN_LIS_CursosAulasXAsignacionCurso(ByVal int_CodigoAsignacionCurso As Integer, _
                                     ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_SalonPorAsignacionCurso")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionCurso", DbType.Int32, int_CodigoAsignacionCurso)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)


        End Function
        Public Function FUN_GET_CursosAulasXCodigoAsignacionGrupo(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_GET_CursosAulasXCodigoAsignacionGrupo")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_AsignacionCursos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_GET_AsignacionCursos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_CantidadCursosInternos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_GET_AsignacionCursosCantidadCursosInternos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCurso", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            dbBase.AddOutParameter(dbCommand, "@p_ValorCantidadInternos", DbType.Int32, 10)

            dbBase.ExecuteScalar(dbCommand)
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_ValorCantidadInternos")))

        End Function

        'Lista de Plan Curricular por Anio Academico
        Public Function FUN_LIS_PlanCurricular(ByVal int_CodigoAnioAcademico As Integer, _
                                                 ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_PlanCurricular")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'Lista de Plan Curricular con Cursos Asignados
        Public Function FUN_LIS_PlanesCurricularesAsignacionCursos(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, _
                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_PlanesCurricularesAsignacionCursos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'Lista de Cursos para Mid Term Report
        Public Function FUN_LIS_AsignacionCursosMidTermReport(ByVal int_CodigoGrado As Integer, _
                                       ByVal int_CodigoAnioAcademico As Integer, _
                                       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionCursosMidTermReport")
            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)


        End Function

        'Lista de Cursos Actividades Adicionales
        Public Function FUN_LIS_AsignacionCursosTalleresExtraCurriculares(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("FUN_LIS_AsignacionCursosTalleresExtraCurriculares")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoCurso", DbType.Int32, int_CodigoCurso)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AsignacionCursosAGradosPorTalleresExtraCurriculares( _
            ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoNombreGrupo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("FUN_LIS_AsignacionCursosAGradosPorTalleresExtraCurriculares")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoNombreGrupo", DbType.Int32, int_CodigoNombreGrupo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        'Lista de Cursos para registro de Tareas
        'Public Function FUN_LIS_AsignacionCursosPorAsgignacionGrupo(ByVal int_CodigoAsignacionGrupo As Integer, _
        '    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionCursosPorAsignacionGrupo")

        '    'Parámetros de entrada
        '    dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)

        '    dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
        '    dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
        '    dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

        '    'Ejecucion del Store Procedure
        '    Return dbBase.ExecuteDataSet(cmd)

        'End Function


        'Lista de Cursos para registro de Tareas, por asignacion de aula
        Public Function FUN_LIS_AsignacionCursosPorAsgignacionAula(ByVal int_CodigoAsignacionAula As Integer, ByVal int_Usuario As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionCursosPorAsignacionAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionAula", DbType.Int32, int_CodigoAsignacionAula)
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.Int32, int_Usuario)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'Lista de Cursos para registro de Notas, por asignacion de aula
        Public Function FUN_LIS_AsignacionCursosParaNotasPorAsgignacionAula(ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionCursosParaNotasPorAsignacionAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionAula", DbType.Int32, int_CodigoAsignacionAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'Lista de Cursos para registro de Notas, por asignacion de aula
        Public Function FUN_LIS_AsignacionCursosParaNotasPorAsgignacionAulaAux(ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionCursosParaNotasPorAsignacionAulaAux")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionAula", DbType.Int32, int_CodigoAsignacionAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        ' Consulta Historia de Curricula
        Public Function FUN_LIS_AsignacionCursosHistorico( _
            ByVal str_CodigoAlumno As String, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionCursosHistorico")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace




