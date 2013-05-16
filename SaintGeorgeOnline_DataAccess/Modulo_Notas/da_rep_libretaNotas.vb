Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities

Namespace ModuloNotas

    Public Class da_rep_libretaNotas
        Inherits InstanciaConexion.ManejadorConexion
        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar




        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
        End Sub
#Region "Metodos NO Transaccionales"

        Public Function FUN_LIS_REP_ListarLibretaNotasPrimariaInicial(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, ByVal int_anioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_REP_libretaPrimariaInicial")
                dbBase.AddInParameter(dbCommand, "@p_codigoAlumno", DbType.Int32, int_codigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_bimestre", DbType.Int32, int_bimestre)
                dbBase.AddInParameter(dbCommand, "@p_anioAcademico", DbType.Int32, int_anioAcademico)
                Return dbBase.ExecuteDataSet(dbCommand)


            Catch ex As Exception

            End Try
        End Function

        Public Function FUN_LIS_REP_AlumnosLibreta( _
            ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_AlumnosLibreta")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int16, int_CodigoAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_LibretaNotasSecundaria( _
            ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_LibretaSecundaria")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        ' update 11/07/2012
        Public Function FUN_LIS_REP_LibretaNotasSecundariaImp( _
            ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_AnioAcademico As Integer, ByVal int_idioma As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_LibretaSecundaria")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_idioma", DbType.Int16, int_idioma)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_ConsolidadoNotasSecundaria( _
            ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_TipoRep As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_ConsolidadoNotasSecundaria")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int16, int_CodigoAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoReporte", DbType.Int32, int_TipoRep)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function
        Public Function FUN_LIS_REP_ConsolidadoNotasSecundariaAnual( _
            ByVal int_CodigoAsignacionAula As Integer, ByVal int_TipoRep As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_ConsolidadoNotasSecundariaAnual")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int32, int_CodigoAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoReporte", DbType.Int32, int_TipoRep)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function
        Public Function FUN_LIS_REP_ConsolidadoNotasExamenesSecundaria( _
            ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_TipoRep As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_ConsolidadoNotasExamenesSecundaria")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int16, int_CodigoAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoReporte", DbType.Int32, int_TipoRep)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        'update 14/05/2012
        Public Function FUN_LIS_REP_LibretaNotasSecundariaResumidaPorAlumno( _
            ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_LibretaSecundariaResumida")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_ConsolidadoNotasPrimaria( _
            ByVal int_AsignascionAula As Integer, ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_REP_ConsolidadoEvaluacionPrimaria")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_codigoAsignacionAula", DbType.Int16, int_AsignascionAula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_ReporteLibretaPrimaria(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet


            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_REP_LIBRETAS")
            dbCommand.CommandTimeout = 360 ' 6 mins

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_cod_alumno", DbType.Int32, int_codigoAlumno)
            dbBase.AddInParameter(dbCommand, "@bimestre", DbType.Int32, int_bimestre)
            '' dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_ReporteLibretaPrimaria_1(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, ByVal codAlumnos As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet


            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_REP_LIBRETAS_1")
            dbCommand.CommandTimeout = 360 ' 6 mins

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_cod_alumno", DbType.Int32, int_codigoAlumno)
            dbBase.AddInParameter(dbCommand, "@bimestre", DbType.Int32, int_bimestre)
            dbBase.AddInParameter(dbCommand, "@codAlumnos", DbType.String, codAlumnos)


            '' dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_ReporteLibretaInicial(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, _
                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet


            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_REP_LIBRETAS_INICIAL_1")

            dbCommand.CommandTimeout = 360 ' 6 mins

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_cod_alumno", DbType.Int32, int_codigoAlumno)
            dbBase.AddInParameter(dbCommand, "@bimestre", DbType.Int32, int_bimestre)
            dbBase.AddInParameter(dbCommand, "@codAlumnos", DbType.String, "")




            '' dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_ReporteLibretaInicial_1(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, ByVal codAlumnos As String, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet


            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_REP_LIBRETAS_INICIAL_1")

            dbCommand.CommandTimeout = 360 ' 6 mins

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_cod_alumno", DbType.Int32, int_codigoAlumno)
            dbBase.AddInParameter(dbCommand, "@bimestre", DbType.Int32, int_bimestre)
            dbBase.AddInParameter(dbCommand, "@codAlumnos", DbType.String, codAlumnos)


            '' dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function


        Public Function FUN_LIS_REP_ReporteLibretaInicialSegundoBimestre(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, ByVal codAlumnos As String, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet


            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_REP_LIBRETAS_INICIAL2BIMESTRE")
            dbCommand.CommandTimeout = 360 ' 6 mins

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_cod_alumno", DbType.Int32, int_codigoAlumno)
            dbBase.AddInParameter(dbCommand, "@bimestre", DbType.Int32, int_bimestre)
            dbBase.AddInParameter(dbCommand, "@codAlumnos", DbType.String, codAlumnos)

            '' dbBase.AddInParameter(dbCommand, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function


        Public Function FUN_lIS_CU_ListarLibretaReportePadre(ByVal int_codigo_curso As Integer, ByVal int_codigo_alumno As String, ByVal int_anio_academico As Integer, ByVal codBimestre As Integer)
            Try
                Dim str_Mensaje As String = ""
                Dim p_Valor As Integer = 0
                Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_Rep_NotaRevision")

                '@p_codigo_curso int ,      
                '@p_codigo_anio_academico int ,    
                '@p_cod_bimestre int      


                dbBase.AddInParameter(dbCommand, "@p_codigo_curso", DbType.Int32, int_codigo_curso)
                dbBase.AddInParameter(dbCommand, "@p_codigo_anio_academico", DbType.Int32, int_anio_academico)
                dbBase.AddInParameter(dbCommand, "@p_cod_bimestre", DbType.Int32, codBimestre)

                dbBase.AddInParameter(dbCommand, "@cod_alumno", DbType.String, int_codigo_alumno)

                '@p_codigo_curso int ,        
                '@p_codigo_anio_academico int ,      
                '@p_cod_bimestre int   ,
                '@cod_alumno varchar(8)    


                Return dbBase.ExecuteDataSet(dbCommand)
            Catch ex As Exception
            Finally

            End Try

        End Function

        Public Function FUN_LIS_REP_ReporteLibretaRevision(ByVal int_codigoAsignacionAula As Integer, ByVal int_bimestre As Integer, ByVal int_codCurso As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet


            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_RegistrosNotasXCurso")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_codigoBimestre", DbType.Int32, int_bimestre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionAula", DbType.Int32, int_codigoAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@codCurso", DbType.Int32, int_codCurso)

            '@p_codigoBimestre int,                            
            '@p_CodigoAsignacionAula int  ,  
            '@codCurso int  

            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_ReporteLibretaComentarioPrimaria(ByVal int_codigoAsignacionAula As Integer, ByVal int_bimestre As Integer) As DataSet


            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("REP_USP_Comentarios")

            'Parámetros de entrada

            dbBase.AddInParameter(dbCommand, "@p_codAsignacionAula", DbType.Int32, int_codigoAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@codBimestre", DbType.Int32, int_bimestre)





            '@p_codigoBimestre int,                            
            '@p_CodigoAsignacionAula int  ,  
            '@codCurso int  

            Return dbBase.ExecuteDataSet(dbCommand)

        End Function
        Public Function FUN_LIS_REP_ReporteLibretaComentarioSecundaria(ByVal int_codigoAsignacionAula As Integer, ByVal int_bimestre As Integer) As DataSet
            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("REP_USP_ComentariosSecundaria")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_codAsignacionAula", DbType.Int32, int_codigoAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@codBimestre", DbType.Int32, int_bimestre)
            '@p_codigoBimestre int,                            
            '@p_CodigoAsignacionAula int  ,  
            '@codCurso int  

            Return dbBase.ExecuteDataSet(dbCommand)
        End Function

        ''reporte consolidado evaluacion secudaria

        Public Function FUN_LIS_REP_ReporteConsolidadoEvaluacionSecundaria(ByVal codBimestre As Integer, ByVal codAsignacionAula As Integer, ByVal codCurso As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("REP_USP_RpConsolidadoEvaluacion")

            '@codBimestre int ,
            '@codAula int
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@codBimestre", DbType.Int32, codBimestre)
            dbBase.AddInParameter(dbCommand, "@codAula", DbType.Int32, codAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@codTipoCurso", DbType.Int32, codCurso)

            '@p_codigoBimestre int,                            
            '@p_CodigoAsignacionAula int  ,  
            '@codCurso int  
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function
        Public Function FUN_LIS_REP_ReporteConsolidadoEvaluacionSecundariaAnual( _
                  ByVal int_CodigoAsignacionAula As Integer, _
                  ByVal int_CodigoTipoCurso As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("REP_USP_RpConsolidadoEvaluacionAnual")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@codAula", DbType.Int32, int_CodigoAsignacionAula)
            dbBase.AddInParameter(dbCommand, "@codTipoCurso", DbType.Int32, int_CodigoTipoCurso)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_ReportePronosticoGrado(ByVal codGrado As Integer) As DataSet
            Using dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_REP_ReporterGradoPronostico")
                '@codBimestre int ,
                '@codAula int
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@grado", DbType.Int32, codGrado)
                '@p_codigoBimestre int,                            
                '@p_CodigoAsignacionAula int  ,  
                '@codCurso int  
                Return dbBase.ExecuteDataSet(dbCommand)
            End Using
        End Function
        Public Function FUN_LIS_REP_ReportePronosticoGradoPorPeriodo(ByVal codGrado As Integer, ByVal codPeriodo As Integer) As DataSet
            Using dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_REP_ReporterGradoPronosticoPorPeriodo")
                '@codBimestre int ,
                '@codAula int
                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@grado", DbType.Int32, codGrado)
                dbBase.AddInParameter(dbCommand, "@periodo", DbType.Int32, codPeriodo)
                '@p_codigoBimestre int,                            
                '@p_CodigoAsignacionAula int  ,  
                '@codCurso int  
                Return dbBase.ExecuteDataSet(dbCommand)
            End Using
        End Function



        Public Function listarGradosPrimaria() As DataSet
            ' 
            Using dbCommand As DbCommand = dbBase.GetStoredProcCommand("USP_listarGradosPrimariaComparar")
                Return dbBase.ExecuteDataSet(dbCommand)
            End Using
        End Function

        Public Function FListarReporteComparacionBimestre(ByVal dcParametros As Dictionary(Of String, Object), ByVal nombreProcedure As String) As DataSet
            Try
                Using dbCommand As DbCommand = dbBase.GetStoredProcCommand(nombreProcedure)
                    For Each ClaveValor As KeyValuePair(Of String, Object) In dcParametros
                        dbBase.AddInParameter(dbCommand, "@" & ClaveValor.Key, _TipoDato(dcParametros.Item(ClaveValor.Key)), ClaveValor.Value)
                    Next
                    Return dbBase.ExecuteDataSet(dbCommand)
                End Using


            Catch ex As Exception
            Finally

            End Try
        End Function
        Private Function _TipoDato(ByVal oTtype As Object) As DbType
            Dim ot As Type = oTtype.GetType
            If ot.Name = "Int32" Then
                Return DbType.Int32
            ElseIf ot.Name = "Decimal" Then
                Return DbType.Decimal
            ElseIf ot.Name = "Double" Then
                Return DbType.Double
            ElseIf ot.Name = "DateTime" Then
                Return DbType.DateTime
            ElseIf ot.Name = "Boolean" Then
                Return DbType.Boolean
            ElseIf ot.Name = "Date" Then
                Return DbType.Date
            ElseIf ot.Name = "Int16" Then
                Return DbType.Int16
            ElseIf ot.Name = "String" Then
                Return DbType.String
            End If




        End Function


        Public Function FUN_LIS_REP_RegistroNotasFinalesEquivalentes( _
            ByVal int_codigoGrado As Integer, ByVal int_codigoAulaMinisterio As Integer, ByVal int_desAnio As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_REP_RegistroNotasFinalesEquivalentes")
            dbBase.AddInParameter(dbCommand, "@p_codGrado", DbType.Int32, int_codigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_codAulaMinisterio", DbType.Int32, int_codigoAulaMinisterio)
            dbBase.AddInParameter(dbCommand, "@p_desAnio", DbType.Int32, int_desAnio)
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

        Public Function FUN_LIS_REP_RegistroNotasFinalesMinisterio( _
            ByVal int_codAnio As Integer, ByVal int_codigoGrado As Integer, ByVal int_codigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim dbCommand As DbCommand = dbBase.GetStoredProcCommand("CU_USP_REP_RegistroNotasFinalesSecundaria")
            dbBase.AddInParameter(dbCommand, "@p_codAnio", DbType.Int32, int_codAnio)
            dbBase.AddInParameter(dbCommand, "@p_codGrado", DbType.Int32, int_codigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_codAula", DbType.Int32, int_codigoAula)
            Return dbBase.ExecuteDataSet(dbCommand)

        End Function

#End Region




    End Class

End Namespace