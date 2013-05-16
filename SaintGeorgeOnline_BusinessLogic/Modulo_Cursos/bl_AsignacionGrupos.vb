Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos
Imports SaintGeorgeOnline_DataAccess.ModuloCursos

Namespace ModuloCursos

    Public Class bl_AsignacionGrupos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionGrupos As da_AsignacionGrupos

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
            obj_da_AsignacionGrupos = New da_AsignacionGrupos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionGrupos(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionGrupos.FUN_INS_AsignacionGrupos(objAsignacionGrupos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_AsignacionGruposPorDefecto(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionGrupos.FUN_INS_AsignacionGruposPorDefecto(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_AsignacionGrupos(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionGrupos.FUN_UPD_AsignacionGrupos(objAsignacionGrupos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_AsignacionGrupos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionGrupos.FUN_DEL_AsignacionGrupos(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_AsignacionGruposTodos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionGrupos.FUN_DEL_AsignacionGruposTodos(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Asignación de Grupos Extracurriculares
        Public Function FUN_INS_AsignacionGruposExtracurriculares(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAnioAcademico As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionGrupos.FUN_INS_AsignacionGruposExtracurriculares(objAsignacionGrupos, int_CodigoGrado, int_CodigoAnioAcademico, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_AsignacionGruposExtracurriculares(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionGrupos.FUN_UPD_AsignacionGruposExtracurriculares(objAsignacionGrupos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_AsignacionGruposExtracurriculares(ByVal objAsignacionGrupos As be_AsignacionGrupos, ByVal int_CodigoGrado As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoAnioAcademico As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionGrupos.FUN_DEL_AsignacionGruposExtracurriculares(objAsignacionGrupos, int_CodigoGrado, int_CodigoCurso, int_CodigoAnioAcademico, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionGrupos(ByVal int_CodigoAsignacionCurso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionGrupos.FUN_LIS_AsignacionGrupos(int_CodigoAsignacionCurso, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionGruposExtracurriculares(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoCurso As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionGrupos.FUN_LIS_AsignacionGruposExtracurriculares(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoCurso, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionGruposPorAsignacionAula(ByVal int_CodigoAsignacionCurso As Integer, ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionGrupos.FUN_LIS_AsignacionGruposPorAsignacionAula(int_CodigoAsignacionCurso, int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update

        Public Function FUN_LIS_AlumnosPorAsignacionGrupos(ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionGrupos.FUN_LIS_AlumnosPorAsignacionGrupos(int_CodigoAsignacionGrupo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Notas
        Public Function FUN_LIS_AlumnosPorAsignacionGruposParaNotas(ByVal int_TipoNota As Integer, ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionGrupos.FUN_LIS_AlumnosPorAsignacionGruposParaNotas(int_TipoNota, int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionGruposPorAsignacionAulaParaNotas( _
            ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoAsignacionCurso As Integer, ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionGrupos.FUN_LIS_AsignacionGruposPorAsignacionAulaParaNotas(int_CodigoTrabajador, int_CodigoAsignacionCurso, int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

    End Class

End Namespace
