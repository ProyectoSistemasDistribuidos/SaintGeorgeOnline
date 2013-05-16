Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos

Namespace ModuloCursos

    'update

    Public Class da_AsignacionGrupos
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

        Public Function FUN_INS_AsignacionGrupos(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_AsignacionGrupos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCurso", DbType.Int16, objAsignacionGrupos.CodigoAsignacionCurso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombreGrupo", DbType.Int16, objAsignacionGrupos.CodigoNombreGrupo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor1", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor1 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor1))
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor2", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor2 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor2))
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor3", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor3 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor3))
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor4", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor4 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor4))
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int16, objAsignacionGrupos.CodigoAsignacionAula)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure : Registro Cabecera
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
     
        End Function

        Public Function FUN_INS_AsignacionGruposPorDefecto(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_AsignacionGruposPorDefecto")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCurso", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure : Registro Cabecera
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_AsignacionGrupos(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_AsignacionGrupos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objAsignacionGrupos.CodigoAsignacionGrupo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCurso", DbType.Int16, objAsignacionGrupos.CodigoAsignacionCurso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombreGrupo", DbType.Int16, objAsignacionGrupos.CodigoNombreGrupo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor1", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor1 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor1))
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor2", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor2 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor2))
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor3", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor3 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor3))
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor4", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor4 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor4))
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int16, objAsignacionGrupos.CodigoAsignacionAula)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure : Registro Cabecera
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_DEL_AsignacionGrupos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_DEL_AsignacionGrupos")
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
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_DEL_AsignacionGruposTodos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_DEL_AsignacionGruposTodos")
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
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        ' Asignación de Grupos Extracurriculares
        Public Function FUN_INS_AsignacionGruposExtracurriculares(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAnioAcademico As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_AsignacionGruposExtracurriculares")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionCurso", DbType.Int16, objAsignacionGrupos.CodigoAsignacionCurso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombreGrupo", DbType.Int16, objAsignacionGrupos.CodigoNombreGrupo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure : Registro Cabecera
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_AsignacionGruposExtracurriculares(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_AsignacionGruposExtracurriculares")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objAsignacionGrupos.CodigoAsignacionGrupo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoProfesor1", DbType.Int16, IIf(objAsignacionGrupos.CodigoPersonaProfesor1 = 0, DBNull.Value, objAsignacionGrupos.CodigoPersonaProfesor1))
            dbBase.AddInParameter(dbCommand, "@p_NumeroVacantes", DbType.Int16, objAsignacionGrupos.NumeroVacantesTaller)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure : Registro Cabecera
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_DEL_AsignacionGruposExtracurriculares(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByVal int_CodigoGrado As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoAnioAcademico As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_DEL_AsignacionGruposExtracurriculares")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int16, int_CodigoCurso)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombreGrupo", DbType.Int16, objAsignacionGrupos.CodigoNombreGrupo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure : Registro Cabecera
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionGrupos(ByVal int_CodigoAsignacionCurso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionGruposCursos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionCurso", DbType.Int16, int_CodigoAsignacionCurso)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' Listado de Asignación de Grupos Extracurriculares
        Public Function FUN_LIS_AsignacionGruposExtracurriculares(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoCurso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionGruposCursosExtracurriculares")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoCurso", DbType.Int16, int_CodigoCurso)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AsignacionGruposPorAsignacionAula(ByVal int_CodigoAsignacionCurso As Integer, ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionGruposCursosPorAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionCurso", DbType.Int16, int_CodigoAsignacionCurso)
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionAula", DbType.Int16, int_CodigoAsignacionAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'update
        Public Function FUN_LIS_AlumnosPorAsignacionGrupos(ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AlumnosPorAsignacionGrupos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        ' Notas
        Public Function FUN_LIS_AlumnosPorAsignacionGruposParaNotas(ByVal int_TipoNota As Integer, ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AlumnosPorAsignacionGruposParaNotas")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_TipoNota", DbType.Int16, int_TipoNota)
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionGrupo", DbType.Int32, int_CodigoAsignacionGrupo)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_CodigoBimestre)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AsignacionGruposPorAsignacionAulaParaNotas( _
            ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoAsignacionCurso As Integer, ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AsignacionGruposCursosPorAulaParaNotas")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionCurso", DbType.Int16, int_CodigoAsignacionCurso)
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionAula", DbType.Int16, int_CodigoAsignacionAula)

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