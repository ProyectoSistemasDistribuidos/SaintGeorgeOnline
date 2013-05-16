Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula

Namespace ModuloMatricula
    Public Class da_Matricula
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
        ' updated 15/11/2012
        Public Function FUN_LIS_NotasPlantillaSIAGIE(ByVal int_CodigoSede As Integer, ByVal int_AnioAcademico As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_NotasFinalesBimestralesXSalon") 'MA_USP_LIS_NominaMatricula

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoSede)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_AnioAcademico)
            'dbBase.AddInParameter(cmd, "@p_CodNivelMin", DbType.Int32, int_Nivel)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_Grado)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int32, int_Aula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_DatosEmergenciaAlumno(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_DatosEmergenciaAlumno")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.Int32, int_CodigoAlumno)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_NominaMatricula(ByVal int_CodigoSede As Integer, ByVal int_AnioAcademico As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_NominaMatriculaMinisterio") 'MA_USP_LIS_NominaMatricula

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSede", DbType.Int32, int_CodigoSede)
            dbBase.AddInParameter(cmd, "@p_CodAnioAcad", DbType.Int32, int_AnioAcademico)
            'dbBase.AddInParameter(cmd, "@p_CodNivelMin", DbType.Int32, int_Nivel)
            dbBase.AddInParameter(cmd, "@p_CodGrado", DbType.Int32, int_Grado)
            dbBase.AddInParameter(cmd, "@p_CodSeccion", DbType.Int32, int_Aula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'update 
        Public Function FUN_LIS_RecepcionDocumentosMatricula(ByVal int_TipoFiltro As Integer, ByVal str_Filtro As String, ByVal int_CodigoAnioMatricula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_RecepcionDocumentosMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_TipoFiltro", DbType.Int16, int_TipoFiltro)
            dbBase.AddInParameter(cmd, "@p_Filtro", DbType.String, str_Filtro)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioMatricula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' updated 03/08/2012
        Public Function FUN_REP_RecepcionDocumentosMatricula(ByVal int_TipoFiltro As Integer, ByVal str_Filtro As String, ByVal int_CodigoAnioMatricula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_REP_RecepcionDocumentosMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_TipoFiltro", DbType.Int16, int_TipoFiltro)
            dbBase.AddInParameter(cmd, "@p_Filtro", DbType.String, str_Filtro)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioMatricula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_RetencionMatricula(ByVal int_TipoFiltro As Integer, ByVal str_Filtro As String, ByVal int_CodigoAnioMatricula As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_RetencionMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_TipoFiltro", DbType.Int16, int_TipoFiltro)
            dbBase.AddInParameter(cmd, "@p_Filtro", DbType.String, str_Filtro)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioMatricula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'update 
        Public Function FUN_VAL_DeudasYPagosMatricula(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PG_USP_VAL_DeudasYPagosMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_VAL_RequisitosMatricula(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_VAL_RequisitosMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_VAL_AceptacionDocumentoDireccion(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoAnioAcademico As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_VAL_AceptacionDocumentoDireccion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        'update 
        Public Function FUN_LIS_RegistroMatriculaXalumno(ByVal int_CodigoAnioMatricula As Integer, ByVal str_CodigoAlumno As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_RegistroMatriculaXalumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioMatricula)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DocumentosPasosMatricula(ByVal int_CodigoAnioMatricula As Integer, ByVal str_CodigoAlumno As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_DocumentosPasosMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioMatricula)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        '[MA_USP_LIS_PasosMatriculaPorAlumno]
        Public Function FUN_LIS_UltimoPasoMatriculaAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoEtapa As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_PasosMatriculaPorAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoEtapa", DbType.Int16, int_CodigoEtapa)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'Reportes
        'ok
        Public Function FUN_REP_AlumnosMatriculadosXGradoFechas( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_REP_AlumnosMatriculadosXGradoFechas")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodoAcademico", DbType.Int16, int_CodigoPeriodoAcademico)
            dbBase.AddInParameter(cmd, "@p_FechaInicial", DbType.DateTime, dt_FechaRangoInicial)
            dbBase.AddInParameter(cmd, "@p_FechaFinal", DbType.DateTime, dt_FechaRangoFinal)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_AlumnosMatriculadosXGrado( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_REP_AlumnosMatriculadosXGrado")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodoAcademico", DbType.Int16, int_CodigoPeriodoAcademico)
            dbBase.AddInParameter(cmd, "@p_FechaInicial", DbType.DateTime, dt_FechaRangoInicial)
            dbBase.AddInParameter(cmd, "@p_FechaFinal", DbType.DateTime, dt_FechaRangoFinal)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_REP_AlumnosPasosMatricula( _
           ByVal int_CodigoPeriodoAcademico As Integer, _
           ByVal dt_FechaRangoInicial As Date, _
           ByVal dt_FechaRangoFinal As Date, _
           ByVal int_CodigoGrado As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_REP_AlumnosMatriculados")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodoAcademico", DbType.Int16, int_CodigoPeriodoAcademico)
            dbBase.AddInParameter(cmd, "@p_FechaInicial", DbType.DateTime, dt_FechaRangoInicial)
            dbBase.AddInParameter(cmd, "@p_FechaFinal", DbType.DateTime, dt_FechaRangoFinal)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'No Matriculados
        Public Function FUN_REP_AlumnosNoMatriculadosXGrado(ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_REP_AlumnosNoMatriculadosXGrado")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodoAcademico", DbType.Int16, int_CodigoPeriodoAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_PasoMatricula(ByVal objMatricula As be_Matricula, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_PasosMatricula")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objMatricula.PeriodoAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPasoMatricula", DbType.Int32, objMatricula.CodigoPasoMatricula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objMatricula.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int32, objMatricula.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_AceptacionEtapa", DbType.Int32, objMatricula.AceptacionEtapa)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteScalar(dbCommand)
        End Function

        Public Function FUN_INS_Matricula(ByVal objMatricula As be_Matricula, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MiMensaje As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera
                dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_Matricula")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, objMatricula.CodigoGrado)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, objMatricula.PeriodoAcademico)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objMatricula.CodigoAlumno)

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

                If int_Valor > 0 Then
                    Commit()
                Else
                    Rollback()
                End If

                Return int_Valor
            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try

        End Function

        Public Function FUN_INS_MatriculaDeudas(ByVal objMatricula As be_Matricula, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MiMensaje As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera
                dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_MatriculaDeudas")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoPeriodo", DbType.Int32, objMatricula.PeriodoAcademico)
                dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, objMatricula.CodigoGrado)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objMatricula.CodigoAlumno)

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

                If int_Valor > 0 Then
                    Commit()
                Else
                    Rollback()
                End If

                Return int_Valor
            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return -1
            Finally
                Conexion.Close()
            End Try

        End Function

        'update 
        Public Function FUN_INS_RecepcionDocumentosMatricula(ByVal dtLista As DataTable, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MiMensaje As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                If dtLista.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLista.Rows

                        dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_DocumentosMatricula")

                        'Parámetros de entrada
                        dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, dr.Item("CodigoAlumno"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacion", DbType.Int32, dr.Item("CodigoAsignacion"))
                        dbBase.AddInParameter(dbCommand, "@p_Check", DbType.Int32, dr.Item("Check"))

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

        Public Function FUN_INS_RegistroRestriccionMatricula(ByVal dtLista As DataTable, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MiMensaje As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                If dtLista.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLista.Rows

                        dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_RegistroRestriccionMatricula")

                        'Parámetros de entrada
                        dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, dr.Item("CodigoAlumno"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoMotivoRestriccion", DbType.Int32, dr.Item("CodigoMotivoRestriccionMatricula"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, dr.Item("CodigoAnioAcademico"))
                        dbBase.AddInParameter(dbCommand, "@p_Check", DbType.Int32, dr.Item("Check"))

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

    End Class
End Namespace


