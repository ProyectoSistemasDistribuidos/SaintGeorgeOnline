Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloReportes

Namespace ModuloReportes

    Public Class bl_Asistencia
        'ok
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Asistencia As da_Asistencia

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
            obj_da_Asistencia = New da_Asistencia
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_REP_IncidenciasAsistencia(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoAula As Integer, _
                     ByVal int_CodigoBimestre As Integer, ByVal str_CodigoAlumno As Integer, _
                     ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
                     ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Asistencia.FUN_REP_IncidenciasAsistencia(int_CodigoPeriodoAcademico, _
                                                        int_CodigoAula, int_CodigoBimestre, str_CodigoAlumno, _
                                                        int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_AsistenciaXBimestreMeses(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoAula As Integer, _
                    ByVal int_CodigoBimestre As Integer, ByVal str_CodigoAlumno As Integer, _
                    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
                    ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Asistencia.FUN_REP_AsistenciaXBimestreMeses(int_CodigoPeriodoAcademico, _
                                                        int_CodigoAula, int_CodigoBimestre, str_CodigoAlumno, _
                                                        int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_ControlXBimestre(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoAula As Integer, _
                   ByVal int_CodigoBimestre As Integer, ByVal str_CodigoAlumno As Integer, _
                   ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
                   ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Asistencia.FUN_REP_ControlXBimestre(int_CodigoPeriodoAcademico, _
                                                        int_CodigoAula, int_CodigoBimestre, str_CodigoAlumno, _
                                                        int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
#End Region

    End Class

End Namespace