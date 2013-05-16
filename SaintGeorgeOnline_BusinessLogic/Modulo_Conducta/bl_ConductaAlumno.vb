Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConductaAlumnos
Imports SaintGeorgeOnline_DataAccess.ModuloConductaAlumnos
'ok
Namespace ModuloConductaAlumnos

    Public Class bl_ConductaAlumno

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_ConductaAlumno As da_ConductaAlumno

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
            obj_da_ConductaAlumno = New da_ConductaAlumno
        End Sub

#End Region

#Region "Metodos Transacciones"
        Public Function FUN_INS_RegistroMeritosDemeritos(ByVal obj_RegistroMeritosDemeritos As be_RegistroMeritosDemeritos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ConductaAlumno.FUN_INS_RegistroMeritosDemeritos(obj_RegistroMeritosDemeritos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_Registro3BlackMarkDemeritos(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoBimestre As Integer, ByVal str_CodigoAlumno As String, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ConductaAlumno.FUN_INS_Registro3BlackMarkDemeritos(int_CodigoAnioAcademico, int_CodigoBimestre, str_CodigoAlumno, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_RegistroBlackMark(ByVal obj_RegistroBlackMark As be_RegistroBlackMark, ByVal int_Bimestre As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoAnioAcademico As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ConductaAlumno.FUN_INS_RegistroBlackMark(obj_RegistroBlackMark, int_Bimestre, int_CodigoAula, int_CodigoAnioAcademico, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_ConductaPrimaria(ByVal int_CodigoAnioAcademico As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal str_NotaBimestralLetras As String, ByVal int_CodigoAula As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ConductaAlumno.FUN_UPD_ConductaPrimaria(int_CodigoAnioAcademico, str_CodigoAlumno, int_CodigoBimestre, str_NotaBimestralLetras, int_CodigoAula, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_AprobacionDeMeritosTotales(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoTipo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ConductaAlumno.FUN_UPD_AprobacionDeMeritosTotales(int_CodigoAnioAcademico, int_CodigoAula, int_CodigoBimestre, int_CodigoCurso, int_CodigoTipo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_RegistroAprobacionDeMeritos(ByVal int_CodigoRegistroMeritosDemeritos As Integer, ByVal int_CodigoEstadoAprobacion As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ConductaAlumno.FUN_UPD_RegistroAprobacionDeMeritos(int_CodigoRegistroMeritosDemeritos, int_CodigoEstadoAprobacion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_DetalleConducta(ByVal str_CodigoAlumno As String, ByVal int_CodigoBimestre As Integer, ByVal int_Anio As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ConductaAlumno.FUN_GET_DetalleConducta(str_CodigoAlumno, int_CodigoBimestre, int_Anio, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_Detalle3BlackMark(ByVal int_CodigoRegistroConductual As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ConductaAlumno.FUN_GET_Detalle3BlackMark(int_CodigoRegistroConductual, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AlumnosXSalonConducta(ByVal int_AnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConductaAlumno.FUN_LIS_AlumnosXSalonConducta(int_AnioAcademico, int_CodigoAula, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_AprobacionDEMeritos(ByVal int_AnioAcademico As Integer, ByVal int_Aula As Integer, ByVal int_Bimestre As Integer, ByVal int_Curso As Integer, ByVal str_CodigoAlumno As String, ByVal int_Codigotipo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConductaAlumno.FUN_LIS_AprobacionDEMeritos(int_AnioAcademico, int_Aula, int_Bimestre, int_Curso, str_CodigoAlumno, int_Codigotipo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_ConductaPrimaria(ByVal int_AnioAcademico As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConductaAlumno.FUN_LIS_ConductaPrimaria(int_AnioAcademico, int_CodigoBimestre, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_AsignacionGruposCursos(ByVal int_AnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConductaAlumno.FUN_LIS_AsignacionGruposCursos(int_AnioAcademico, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_RegistroConductaBimestral_NotaBimestralCualitativa(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoTipo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConductaAlumno.FUN_LIS_RegistroConductaBimestral_NotaBimestralCualitativa(int_CodigoAnioAcademico, int_CodigoAula, int_CodigoBimestre, int_CodigoCurso, int_CodigoTipo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_REP_RegistroConductaBimestral_NotaBimestralCualitativa(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoCurso As Integer, ByVal int_CodigoTipo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConductaAlumno.FUN_REP_RegistroConductaBimestral_NotaBimestralCualitativa(int_CodigoAnioAcademico, int_CodigoAula, int_CodigoBimestre, int_CodigoCurso, int_CodigoTipo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_REP_MeritDemeritoXAlumnoCurso(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConductaAlumno.FUN_REP_MeritDemeritoXAlumnoCurso(int_CodigoAnioAcademico, int_CodigoAula, int_CodigoBimestre, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

    End Class

End Namespace

