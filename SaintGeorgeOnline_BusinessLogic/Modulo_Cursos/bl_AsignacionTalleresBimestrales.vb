Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos
Imports SaintGeorgeOnline_DataAccess.ModuloCursos

Namespace ModuloCursos

    Public Class bl_AsignacionTalleresBimestrales

#Region "Atributos"

        Private obj_da_AsignacionTalleresBimestrales As da_AsignacionTalleresBimestrales

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_AsignacionTalleresBimestrales = New da_AsignacionTalleresBimestrales

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_UPD_AsignacionTalleresBimestralesApertura(ByVal int_Codigo As Integer, ByVal int_EstadoApertura As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionTalleresBimestrales.FUN_UPD_AsignacionTalleresBimestralesApertura(int_Codigo, int_EstadoApertura, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionTalleresBimestrales(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionTalleresBimestrales.FUN_LIS_AsignacionTalleresBimestrales(int_CodigoAnioAcademico, int_CodigoBimestre, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionTalleresBimestralesPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionTalleresBimestrales.FUN_LIS_AsignacionTalleresBimestralesPorAlumno(str_CodigoAlumno, int_CodigoPeriodoAcademico, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        ' Actualizacion 06/07/2012
        Public Function FUN_LIS_TalleresBimestralesPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionTalleresBimestrales.FUN_LIS_TalleresBimestralesPorAlumno(str_CodigoAlumno, int_CodigoPeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Actualizacion 09/03/2012
        Public Function FUN_LIS_AsignacionTalleresBimestralesPorAlumnoRegistroManual( _
            ByVal str_CodigoAlumno As String, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionTalleresBimestrales.FUN_LIS_AsignacionTalleresBimestralesPorAlumnoRegistroManual( _
                str_CodigoAlumno, int_CodigoPeriodoAcademico, int_CodigoBimestre, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_ReportePorTaller(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoTaller As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionTalleresBimestrales.FUN_LIS_ReportePorTaller(int_CodigoAnioAcademico, int_CodigoTaller, int_CodigoGrado, int_CodigoAula, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ReportePorAlumnosSinTaller( _
            ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionTalleresBimestrales.FUN_LIS_ReportePorAlumnosSinTaller(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

    End Class

End Namespace
