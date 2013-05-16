Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula

    Public Class bl_Alumnos

        'Actualizado 30-05-2012

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Alumnos As da_Alumnos


#End Region

#Region "Propiedades"

        Public ReadOnly Property Mensaje() As String
            Get
                Return str_Mensaje
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_Alumnos = New da_Alumnos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ExoneracionesCursos(ByVal int_codigoAnioAcademico As Integer, ByVal str_codigoAlumno As String, ByVal int_codigoCurso As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Alumnos.FUN_INS_ExoneracionesCursos(int_codigoAnioAcademico, str_codigoAlumno, int_codigoCurso, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update
        Public Function FUN_INS_Alumno(ByVal bool_FichaCompleta As Boolean, ByVal int_CodigoAnioIngresa As Integer, ByVal int_CodigoGradoActual As Integer, _
            ByVal objAlumno As be_Alumnos, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Alumnos.FUN_INS_Alumno(bool_FichaCompleta, int_CodigoAnioIngresa, int_CodigoGradoActual, objAlumno, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_UPD_Alumno(ByVal bool_FichaCompleta As Boolean, ByVal objFichaMedicaAlumno As be_Alumnos, _
            ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Alumnos.FUN_UPD_Alumno(bool_FichaCompleta, objFichaMedicaAlumno, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'modulo asignacion aula alumnos

        Public Function FUN_UPD_AsignacionAulaAlumno(ByVal int_codigoAnioAcademico As Integer, ByVal int_codigoAula As Integer, ByVal str_CodigoAlumno As String, _
           ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Alumnos.FUN_UPD_AsignacionAulaAlumno(int_codigoAnioAcademico, int_codigoAula, str_CodigoAlumno, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_AsignacionHouseAlumno(ByVal int_codigoAnioAcademico As Integer, ByVal int_codigoHouse As Integer, ByVal str_CodigoAlumno As String, _
         ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Alumnos.FUN_UPD_AsignacionHouseAlumno(int_codigoAnioAcademico, int_codigoHouse, str_CodigoAlumno, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Validacion de Datos
        Public Function FUN_UPD_AlumnosActualizacion( _
            ByVal intCodigoAlumno As Integer, ByVal intCodigoPersona As Integer, _
            ByVal intCodigoSolicitud As Integer, ByVal intCodigoPerfil As Integer, _
            ByVal objDT_Cabecera As DataSet, _
            ByVal arrStrCodigossNacionalidad() As String, _
            ByVal arrStrCodigosIdioma() As String, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Alumnos.FUN_UPD_AlumnosActualizacion( _
                intCodigoAlumno, intCodigoPersona, intCodigoSolicitud, intCodigoPerfil, objDT_Cabecera, _
                arrStrCodigossNacionalidad, arrStrCodigosIdioma, str_Mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Registro de Fichas de Alumno Temporal
        Public Function FUN_INS_AlumnoTemp(ByVal objSolicitud As be_SolicitudActualizacionFichaAlumnos, _
            ByVal objAlumno As be_Alumnos, _
            ByVal objFichaSeguro As be_FichaSeguroAlumno, _
            ByVal str_CadenaCodigoPerfil As String, _
            ByVal objDetalle As DataSet, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal fechaNacimiento As DateTime?, ByVal esRegistradoNacimiento As Boolean?) As Integer

            Return obj_da_Alumnos.FUN_INS_AlumnoTemp(objSolicitud, objAlumno, objFichaSeguro, str_CadenaCodigoPerfil, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, fechaNacimiento, esRegistradoNacimiento)

        End Function


#End Region

#Region "Metodos No Transaccionales"

        '11/10/2012 last version
        Public Function FUN_LIS_FotosAlumnos(ByVal int_CodigoAnioAcademico As Integer, ByVal int_Codigogrado As Integer, ByVal int_CodigoAula As Integer, ByVal str_Codigo As String, ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_FotosAlumnos(int_CodigoAnioAcademico, int_Codigogrado, int_CodigoAula, str_Codigo, str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_FichaAlumno(ByVal str_Codigo As String, ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_estadoAlumno As Integer, ByVal int_Nivel As Integer, ByVal int_SubNivel As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, ByVal int_PeriodoInicio As Integer, ByVal int_PeriodoFin As Integer, ByVal int_Sede As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_FichaAlumno(str_Codigo, str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_estadoAlumno, int_Nivel, int_SubNivel, int_Grado, int_Aula, int_PeriodoInicio, int_PeriodoFin, int_Sede, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_Alumnos(ByVal int_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_GET_Alumno(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_BuscarFamilias(ByVal apellidoPaterno As String, ByVal apellidoMaterno As String, ByVal nombre As String, ByVal tipoDocumento As Integer, ByVal numeroDoc As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_BuscarFamilias(apellidoPaterno, apellidoMaterno, nombre, tipoDocumento, numeroDoc, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        'obtener alumnos Ficha Atencion medica
        Public Function FUN_LIS_AlumnosFichaAtencion(ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosFichaAtencion(int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        'Validacion de Datos
        Public Function FUN_GET_FamiliarActualizacion(ByVal int_Codigo As Integer, ByVal int_CodigoSolicitud As Integer, ByVal int_CodigoPerfil As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_GET_AlumnoActualizacion(int_Codigo, int_CodigoSolicitud, int_CodigoPerfil, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AlumnoActualizacion(ByVal objMaestroPersona As be_MaestroPersonas, _
                ByVal dtInicial As Date, ByVal dtFinal As Date, ByVal intEstado As Integer, ByVal int_CodigoPerfil As Integer, _
                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnoActualizacion(objMaestroPersona, dtInicial, dtFinal, intEstado, int_CodigoPerfil, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Datos para visualizacion del Familiar
        Public Function FUN_GET_AlumnoVisualizacionActualizacionFamiliar(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_GET_AlumnoVisualizacionActualizacionFamiliar(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Modulo de - "DATOS DE ALUMNOS"
        Public Function FUN_LIS_AlumnosPorCodigoFamilia(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Alumnos.FUN_LIS_AlumnosPorCodigoFamilia(int_CodigoFamilia, int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_AlumnosPorCodigoFamiliaMatricula(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Alumnos.FUN_LIS_AlumnosPorCodigoFamiliaMatricula(int_CodigoFamilia, int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_AlumnosPorCodigoAlumno(ByVal int_CodigoFamilia As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Alumnos.FUN_GET_AlumnosPorCodigoAlumno(int_CodigoFamilia, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_CompanierosAlumnos(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoAnioAcademico As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Alumnos.FUN_LIS_CompanierosAlumnos(int_CodigoAlumno, int_CodigoAnioAcademico, int_Grado, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


        Public Function FUN_GET_AlumnosPorCodigoAlumnoYPeriodo(ByVal int_CodigoFamilia As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_GET_AlumnosPorCodigoAlumnoYPeriodo(int_CodigoFamilia, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        'MODULO DE ALUMNOS
        Public Function FUN_GET_FichaUnicaMatriculaAlumno(ByVal str_CodigoAlumno As String, ByVal int_PeriodoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)
            Return obj_da_Alumnos.FUN_GET_FichaUnicaMatriculaAlumno(str_CodigoAlumno, int_PeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


        'Lista Alumnos por Talonario - Ingresos Varios
        Public Function FUN_LIS_AlumnosPorTalonario(ByVal int_CodigoTalonario As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosPorTalonario(int_CodigoTalonario, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Lista de Alumnos para Mid Term Report
        Public Function FUN_LIS_AlumnosMidTermReport(ByVal int_CodigoAula As Integer, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosMidTermReport(int_CodigoAula, int_CodigoPeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AlumnosMidTermReportPorGradoYAula(ByVal int_CodigoAnioAcademico As Integer, _
                                                                  ByVal int_CodigoGrado As Integer, _
                                                                  ByVal int_CodigoAula As Integer, _
                                                                  ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosMidTermReportPorGradoYAula(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, _
                                                                            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AlumnosWeeklyReportPorGradoYAula(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoSemana As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosWeeklyReportPorGradoYAula(int_CodigoAnioAcademico, int_CodigoBimestre, int_CodigoSemana, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Lista de Alumnos para Reportes
        Public Function FUN_LIS_AlumnosPorNivelSubNivelGradoAula(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoNivel As Integer, ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosPorNivelSubNivelGradoAula(int_CodigoAnioAcademico, int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Modulo de Profesores pro Alumno
        Public Function FUN_LIS_ProfesoresPorAlumno(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_ProfesoresPorAlumno(int_CodigoAlumno, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'modulo asignacion de aulas a alumnos

        Public Function FUN_LIS_AsignacionAulasAlumnos(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AsignacionAulasAlumnos(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_HousesAlumno(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_HousesAlumno(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ExoneradosPorCurso(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_ExoneradosPorCurso(int_CodigoAnioAcademico, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_ExoneradosPorCurso(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_REP_ExoneradosPorCurso(int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        'Modulo Registro Notas Grado Pronosticos
        Public Function FUN_LIS_AlumnoPorAulasyAnioAcademico(ByVal int_CodigoAula As Integer, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnoPorAulasyAnioAcademico(int_CodigoAula, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_AlumnosRetirados(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_REP_AlumnosRetirados(int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Impresion Libretas 04/07/2012
        Public Function FUN_LIS_AlumnosPorAulayAnioAcademicoLibreta(ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosPorAulayAnioAcademicoLibreta(int_CodigoAsignacionAula, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' 06/09/2012
        ' Registro deudas por servicios
        Public Function FUN_LIS_AlumnosPorAulaGradoyAnioAcademico( _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosPorAulaGradoyAnioAcademico(int_CodigoAnioAcademico, int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        ' 20/11/2012
        ' Busqueda de alumnos para ingreso de notas
        Public Function FUN_LIS_AlumnosPorPeriodoYGrado( _
            ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosPorPeriodoYGrado( _
                int_CodigoAnioAcademico, int_CodigoGrado, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Busqueda de alumnos para registro de pre-matricula
        Public Function FUN_LIS_AlumnosPorPeriodoYGradoPreMatricula( _
            ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosPorPeriodoYGradoPreMatricula( _
                int_CodigoAnioAcademico, int_CodigoGrado, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        ' Entrevistas
        Public Function FUN_LIS_AlumnosPorFamiliaPeriodoGradoAula(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
                                                                  ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Alumnos.FUN_LIS_AlumnosPorFamiliaPeriodoGradoAula(int_CodigoFamilia, int_CodigoPeriodo, int_CodigoGrado, int_CodigoAula, _
                                                                            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

    End Class

End Namespace

