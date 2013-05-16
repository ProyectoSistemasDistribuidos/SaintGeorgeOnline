Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento

Namespace ModuloSeguimiento

    Public Class bl_AsignacionCalificativosCriterios

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionCalificativosCriterios As da_AsignacionCalificativosCriterios

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
            obj_da_AsignacionCalificativosCriterios = New da_AsignacionCalificativosCriterios
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionCalificativosCriterios(ByVal objAsignacionCC As be_AsignacionCalificativosCriterios, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionCalificativosCriterios.FUN_INS_AsignacionCalificativosCriterios(objAsignacionCC, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_AsignacionCalificativosCriterios(ByVal objAsignacionCC As be_AsignacionCalificativosCriterios, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionCalificativosCriterios.FUN_DEL_AsignacionCalificativosCriterios(objAsignacionCC, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionCalificativosCriterios(ByVal int_CodigoPeriodoAcademico As String, ByVal int_CodigoGrado As Integer, ByVal int_CodigoTipoDocumento As Integer, ByVal int_CodigoCriterio As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCalificativosCriterios.FUN_LIS_AsignacionCalificativosCriterios(int_CodigoPeriodoAcademico, int_CodigoGrado, int_CodigoTipoDocumento, int_CodigoCriterio, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        ' Notas Cuantitativas
        Public Function FUN_LIS_AsignacionCalificativosCriteriosPorAlumnoyCodigoAsignacionGrupo( _
            ByVal str_CodigoAlumno As String, ByVal int_CodigoAsignacionGrupo As String, _
            ByVal int_CodigoBimestre As Integer, ByVal int_CodigoTipoDocumento As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCalificativosCriterios.FUN_LIS_AsignacionCalificativosCriteriosPorAlumnoyCodigoAsignacionGrupo( _
            str_CodigoAlumno, int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoTipoDocumento, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        'Perfil Estudiante
        Public Function FUN_LIS_AsignacionCalificativosCriteriosPorCodigoAsignacionAula( _
            ByVal int_CodigoAsignacionAula As String, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoTipoDocumento As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCalificativosCriterios.FUN_LIS_AsignacionCalificativosCriteriosPorCodigoAsignacionAula( _
                int_CodigoAsignacionAula, int_CodigoBimestre, int_CodigoTipoDocumento, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Tutor Report
        Public Function FUN_LIS_AsignacionCalificativosCriteriosPorCodigoAsignacionAulaTutorReport( _
           ByVal int_CodigoAsignacionAula As String, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoTipoDocumento As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionCalificativosCriterios.FUN_LIS_AsignacionCalificativosCriteriosPorCodigoAsignacionAulaTutorReport( _
                int_CodigoAsignacionAula, int_CodigoBimestre, int_CodigoTipoDocumento, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



#End Region

    End Class

End Namespace
