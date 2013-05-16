Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloReportes

Namespace ModuloReportes

    Public Class bl_Enfermeria

        'updated 31/08/2011

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Enfermeria As da_Enfermeria

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
            obj_da_Enfermeria = New da_Enfermeria
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region




#Region "Metodos No Transaccionales"

        Public Function FUN_REP_AtencionesMedicasTotales(ByVal codSede As Integer, _
            ByVal int_TipoPaciente As Integer, ByVal dt_FechaRangoInicial As Date, ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoNivel As Integer, ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoPersona As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_AtencionesMedicasTotales(codSede, int_TipoPaciente, dt_FechaRangoInicial, dt_FechaRangoFinal, _
               int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoPersona, _
               int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_AtencionesMedicasExpandida( _
            ByVal int_TipoPaciente As Integer, ByVal dt_FechaRangoInicial As Date, ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoNivel As Integer, ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoPersona As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_AtencionesMedicasExpandida(int_TipoPaciente, dt_FechaRangoInicial, dt_FechaRangoFinal, _
                int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoPersona, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_AtencionesAcumuladoXHora( _
            ByVal int_TipoPaciente As Integer, ByVal dt_FechaRangoInicial As Date, ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoNivel As Integer, ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoSede As Integer, ByVal int_NumeroPintar As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_AtencionesAcumuladoXHora(int_TipoPaciente, dt_FechaRangoInicial, dt_FechaRangoFinal, _
                int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoSede, int_NumeroPintar, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_REP_MedicamentosPorFechas( _
            ByVal int_TipoPaciente As Integer, ByVal dt_FechaRangoInicial As Date, ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_MedicamentosPorFechas(int_TipoPaciente, dt_FechaRangoInicial, dt_FechaRangoFinal, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_FichaSeguroAlumno( _
            ByVal int_Nivel As Integer, ByVal int_Subnivel As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_FichaSeguroAlumno(int_Nivel, int_Subnivel, int_Grado, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_AteMedHistorialClinico( _
           ByVal int_CodigoAula As Integer, ByVal str_CodigoAlumno As String, ByVal int_TipoFecha As Integer, ByVal dt_FechaRangoInicial As Date, ByVal dt_FechaRangoFinal As Date, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_AteMedHistorialClinico(int_CodigoAula, str_CodigoAlumno, int_TipoFecha, dt_FechaRangoInicial, dt_FechaRangoFinal, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_FichaMedicaAlumno( _
            ByVal int_Nivel As Integer, ByVal int_Subnivel As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_FichaMedicaAlumno(int_Nivel, int_Subnivel, int_Grado, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_DiagnosticoPorFechas( _
            ByVal int_TipoPaciente As Integer, ByVal dt_FechaRangoInicial As Date, ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_DiagnosticoPorFechas(int_TipoPaciente, dt_FechaRangoInicial, dt_FechaRangoFinal, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_Alergias( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoNivel As Integer, ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_Alergias(int_CodigoPeriodoAcademico, int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_ProcedimientosRealizadosPorFechas(ByVal codSede As Integer, _
            ByVal int_TipoPaciente As Integer, ByVal dt_FechaRangoInicial As Date, ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_ProcedimientosRealizadosPorFechas(codSede, int_TipoPaciente, dt_FechaRangoInicial, dt_FechaRangoFinal, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_Emergencia(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoNivel As Integer, _
         ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
         ByVal int_CodigoAlumno As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
         ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_Emergencia(int_CodigoPeriodoAcademico, int_CodigoNivel, int_CodigoSubnivel, _
                                                        int_CodigoGrado, int_CodigoAula, int_CodigoAlumno, int_CodigoUsuario, _
                                                        int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_REP_Seguro(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoNivel As Integer, _
       ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
       ByVal int_CodigoAlumno As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
       ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_Seguro(int_CodigoPeriodoAcademico, int_CodigoNivel, int_CodigoSubnivel, _
                                                        int_CodigoGrado, int_CodigoAula, int_CodigoAlumno, int_CodigoUsuario, _
                                                        int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_REP_EnfermedadAlumno(ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoNivel As Integer, _
     ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
     ByVal int_CodigoAlumno As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
     ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Enfermeria.FUN_REP_EnfermedadAlumno(int_CodigoPeriodoAcademico, int_CodigoNivel, int_CodigoSubnivel, _
                                                        int_CodigoGrado, int_CodigoAula, int_CodigoAlumno, int_CodigoUsuario, _
                                                        int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
#End Region

    End Class

End Namespace