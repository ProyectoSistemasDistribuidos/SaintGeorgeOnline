Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas
Imports SaintGeorgeOnline_DataAccess

Namespace ModuloNotas

    Public Class bl_rep_libretaNotas

        Private obj_da_rep_libretaNotas As da_rep_libretaNotas
        Public Sub New()
            obj_da_rep_libretaNotas = New da_rep_libretaNotas
        End Sub

        Public Function FUN_LIS_REP_ListarLibretaNotasPrimariaInicial(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, ByVal int_anioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Try

                Return New da_rep_libretaNotas().FUN_LIS_REP_ListarLibretaNotasPrimariaInicial(int_codigoAlumno, int_bimestre, int_anioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

            Catch ex As Exception
            Finally

            End Try
        End Function


        Public Function FUN_LIS_REP_AlumnosLibreta( _
         ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoBimestre As Integer, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_AlumnosLibreta(int_CodigoAsignacionAula, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_REP_LibretaNotasSecundaria(ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundaria(str_CodigoAlumno, int_CodigoBimestre, int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' update 11/07/2012
        Public Function FUN_LIS_REP_LibretaNotasSecundariaImp(ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_AnioAcademico As Integer, ByVal int_idioma As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundariaImp(str_CodigoAlumno, int_CodigoBimestre, int_AnioAcademico, int_idioma, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



        Public Function FUN_LIS_REP_ConsolidadoNotasSecundaria( _
            ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_TipoRep As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_ConsolidadoNotasSecundaria(int_CodigoAsignacionAula, int_CodigoBimestre, int_TipoRep, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_LIS_REP_ConsolidadoNotasSecundariaAnual( _
           ByVal int_CodigoAsignacionAula As Integer, ByVal int_TipoRep As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_ConsolidadoNotasSecundariaAnual(int_CodigoAsignacionAula, int_TipoRep, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_LIS_REP_ConsolidadoNotasExamenesSecundaria( _
          ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_TipoRep As Integer, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_ConsolidadoNotasExamenesSecundaria(int_CodigoAsignacionAula, int_CodigoBimestre, int_TipoRep, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



        'update 14/05/2012
        Public Function FUN_LIS_REP_LibretaNotasSecundariaResumidaPorAlumno( _
            ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_LibretaNotasSecundariaResumidaPorAlumno(str_CodigoAlumno, int_CodigoBimestre, int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_REP_ConsolidadoNotasPrimaria( _
         ByVal int_AsignascionAula As Integer, ByVal int_CodigoBimestre As Integer, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet


            Return New da_rep_libretaNotas().FUN_LIS_REP_ConsolidadoNotasPrimaria(int_AsignascionAula, int_CodigoBimestre, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_REP_ReporteLibretaPrimaria(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaPrimaria(int_codigoAlumno, int_bimestre, _
           int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)


        End Function

        Public Function FUN_LIS_REP_ReporteLibretaPrimaria_1(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, ByVal codAlumnos As String, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaPrimaria_1(int_codigoAlumno, int_bimestre, codAlumnos, _
           int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)


        End Function


        Public Function FUN_LIS_REP_ReporteLibretaInicial(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicial(int_codigoAlumno, int_bimestre, _
           int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)


        End Function



        Public Function FUN_LIS_REP_ReporteLibretaInicial_1(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, ByVal codAlumnos As String, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicial_1(int_codigoAlumno, int_bimestre, codAlumnos, _
           int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)


        End Function

        Public Function FUN_LIS_REP_ReporteLibretaInicialSegundoBimestre(ByVal int_codigoAlumno As Integer, ByVal int_bimestre As Integer, ByVal codAlumnos As String, _
      ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaInicialSegundoBimestre(int_codigoAlumno, int_bimestre, codAlumnos, _
          int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function






        Public Function FUN_lIS_CU_ListarLibretaReportePadre(ByVal int_codigo_curso As Integer, ByVal int_codigo_alumno As String, ByVal int_anio_academico As Integer, ByVal codBimestre As Integer)
            Try


                Return New da_rep_libretaNotas().FUN_lIS_CU_ListarLibretaReportePadre(int_codigo_curso, int_codigo_alumno, int_anio_academico, codBimestre)


            Catch ex As Exception

            End Try
        End Function


        Public Function FUN_LIS_REP_ReporteIncialRevision(ByVal int_codigoAsignacionAula As Integer, ByVal int_bimestre As Integer, ByVal int_codCurso As Integer, _
       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Try
                Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaRevision(int_codigoAsignacionAula, int_bimestre, int_codCurso, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

            Catch ex As Exception
            Finally

            End Try

        End Function

        Public Function FUN_LIS_REP_ReporteLibretaComentarioPrimaria(ByVal int_codigoAsignacionAula As Integer, ByVal int_bimestre As Integer) As DataSet
            Try
                Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaComentarioPrimaria(int_codigoAsignacionAula, int_bimestre)

            Catch ex As Exception
            Finally

            End Try
        End Function

        Public Function FUN_LIS_REP_ReporteLibretaComentarioSecundaria(ByVal int_codigoAsignacionAula As Integer, ByVal int_bimestre As Integer) As DataSet
            Try
                Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteLibretaComentarioSecundaria(int_codigoAsignacionAula, int_bimestre)

            Catch ex As Exception
            Finally

            End Try
        End Function



        Public Function FUN_LIS_REP_ReporteConsolidadoEvaluacionSecundaria(ByVal codBimestre As Integer, ByVal codAsignacionAula As Integer, ByVal codCurso As Integer) As DataSet
            Try
                Return New da_rep_libretaNotas().FUN_LIS_REP_ReporteConsolidadoEvaluacionSecundaria(codBimestre, codAsignacionAula, codCurso)
            Catch ex As Exception
            Finally
            End Try
        End Function
        Public Function FUN_LIS_REP_ReporteConsolidadoEvaluacionSecundariaAnual( _
            ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoTipoCurso As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_ReporteConsolidadoEvaluacionSecundariaAnual(int_CodigoAsignacionAula, int_CodigoTipoCurso)

        End Function



        Public Function FUN_LIS_REP_ReportePronosticoGrado(ByVal codGrado As Integer) As DataSet

            Try
                Return New da_rep_libretaNotas().FUN_LIS_REP_ReportePronosticoGrado(codGrado)
            Catch ex As Exception
            Finally

            End Try
        End Function

        Public Function FUN_LIS_REP_ReportePronosticoGradoPorPeriodo(ByVal codGrado As Integer, ByVal codPeriodo As Integer) As DataSet

            Try
                Return New da_rep_libretaNotas().FUN_LIS_REP_ReportePronosticoGradoPorPeriodo(codGrado, codPeriodo)
            Catch ex As Exception
            Finally

            End Try
        End Function

        Public Function FListarReporteComparacionBimestre(ByVal dcParametros As Dictionary(Of String, Object), ByVal nombreProcedure As String) As DataSet
            Try
                Return New da_rep_libretaNotas().FListarReporteComparacionBimestre(dcParametros, nombreProcedure)
            Catch ex As Exception
            Finally

            End Try
        End Function



        Public Function listarGradosPrimaria() As DataSet
            Try





                '' ´'' oppo

                Return New da_rep_libretaNotas().listarGradosPrimaria()

            Catch ex As Exception

            End Try
        End Function


        ' exportacion notas ministerio
        Public Function FUN_LIS_REP_RegistroNotasFinalesEquivalentes( _
            ByVal int_codigoGrado As Integer, ByVal int_codigoAulaMinisterio As Integer, ByVal int_desAnio As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_RegistroNotasFinalesEquivalentes( _
                int_codigoGrado, int_codigoAulaMinisterio, int_desAnio, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_REP_RegistroNotasFinalesMinisterio( _
            ByVal int_codAnio As Integer, ByVal int_codigoGrado As Integer, ByVal int_codigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_rep_libretaNotas.FUN_LIS_REP_RegistroNotasFinalesMinisterio( _
                int_codAnio, int_codigoGrado, int_codigoAula, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



    End Class


End Namespace