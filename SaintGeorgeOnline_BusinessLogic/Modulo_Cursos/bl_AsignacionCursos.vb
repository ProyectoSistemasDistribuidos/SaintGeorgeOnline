Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos
Imports SaintGeorgeOnline_DataAccess.ModuloCursos

Namespace ModuloCursos

    Public Class bl_AsignacionCursos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionCursos As da_AsignacionCursos

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
            obj_da_AsignacionCursos = New da_AsignacionCursos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionCursos(ByVal objAsignacionCursos As be_AsignacionCursos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionCursos.FUN_INS_AsignacionCursos(objAsignacionCursos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_AsignacionCursos(ByVal objAsignacionCursos As be_AsignacionCursos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionCursos.FUN_UPD_AsignacionCursos(objAsignacionCursos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_AsignacionCursosOrden(ByVal objAsignacionCursos As be_AsignacionCursos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionCursos.FUN_UPD_AsignacionCursosOrden(objAsignacionCursos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_AsignacionCursos(ByVal objAsignacionCursos As be_AsignacionCursos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionCursos.FUN_DEL_AsignacionCursos(objAsignacionCursos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionCursos(ByVal int_CodigoGrado As Integer, ByVal int_CodigoAnioAcademico As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursos(int_CodigoGrado, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 30/03/2012
        Public Function FUN_GET_CursosAulasXCodigoAsignacionGrupo(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_GET_CursosAulasXCodigoAsignacionGrupo(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_AsignacionCursos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_GET_AsignacionCursos(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_CantidadCursosInternos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionCursos.FUN_GET_CantidadCursosInternos(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Lista de Plan Curricular por Anio Academico
        Public Function FUN_LIS_PlanCurricular(ByVal int_CodigoAnioAcademico As Integer, _
                                                 ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_PlanCurricular(int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PlanesCurricularesAsignacionCursos(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, _
               ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_PlanesCurricularesAsignacionCursos(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionCursosMidTermReport(ByVal int_CodigoGrado As Integer, _
                                       ByVal int_CodigoAnioAcademico As Integer, _
                                       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursosMidTermReport(int_CodigoGrado, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionCursosTalleresExtraCurriculares(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursosTalleresExtraCurriculares(int_CodigoAnioAcademico, int_CodigoCurso, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionCursosAGradosPorTalleresExtraCurriculares( _
            ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoNombreGrupo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursosAGradosPorTalleresExtraCurriculares(int_CodigoAnioAcademico, int_CodigoNombreGrupo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Lista de Cursos para registro de Tareas
        'Public Function FUN_LIS_AsignacionCursosPorAsgignacionGrupo(ByVal int_CodigoAsignacionGrupo As Integer, _
        '    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursosPorAsgignacionGrupo(int_CodigoAsignacionGrupo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function


        'Lista de Cursos para registro de Tareas, por asignacion de aula
        Public Function FUN_LIS_AsignacionCursosPorAsgignacionAula(ByVal int_CodigoAsignacionAula As Integer, ByVal int_Usuario As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursosPorAsgignacionAula(int_CodigoAsignacionAula, int_Usuario, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_CursosAulasXAsignacionCurso(ByVal int_CodigoAsignacionCurso As Integer, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_CursosAulasXAsignacionCurso(int_CodigoAsignacionCurso, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        'Lista de Cursos para registro de Notas, por asignacion de aula
        Public Function FUN_LIS_AsignacionCursosParaNotasPorAsgignacionAula(ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursosParaNotasPorAsgignacionAula(int_CodigoTrabajador, int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionCursosParaNotasPorAsgignacionAulaAux(ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoAsignacionAula As Integer, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursosParaNotasPorAsgignacionAulaAux(int_CodigoTrabajador, int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        ' Consulta Historia de Curricula
        Public Function FUN_LIS_AsignacionCursosHistorico( _
            ByVal str_CodigoAlumno As String, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCursos.FUN_LIS_AsignacionCursosHistorico(str_CodigoAlumno, int_CodigoGrado, int_CodigoAula, int_CodigoAnioAcademico, _
                                                                             int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace






