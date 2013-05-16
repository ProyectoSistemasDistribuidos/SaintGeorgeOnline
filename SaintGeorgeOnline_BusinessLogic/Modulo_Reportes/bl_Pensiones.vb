Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloReportes

Namespace ModuloReportes

    Public Class bl_Pensiones

        'Actualizado

#Region "Atributos"

        Private obj_da_Pensiones As da_Pensiones

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_Pensiones = New da_Pensiones
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_REP_PagosDiariosPorConceptos( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal str_CodigoConceptoCobro As String, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_PagosDiariosPorConceptos(int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, _
                dt_FechaRangoInicial, dt_FechaRangoFinal, int_CodigoGrado, int_CodigoAula, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_PagosDiariosPorConceptosYAula( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal str_CodigoConceptoCobro As String, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_PagosDiariosPorConceptosYAula(int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, _
                dt_FechaRangoInicial, dt_FechaRangoFinal, int_CodigoGrado, int_CodigoAula, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_DeudasPorConceptosYAula( _
         ByVal int_CodigoPeriodoAcademico As Integer, _
         ByVal str_CodigoConceptoCobro As String, _
         ByVal dt_FechaRangoFinal As Date, _
         ByVal int_CodigoGrado As Integer, _
         ByVal int_CodigoAula As Integer, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_DeudasPorConceptosYAula(int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, _
              dt_FechaRangoFinal, int_CodigoGrado, int_CodigoAula, _
              int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_ResumenBecasXPeriodo( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoNivelMinisterio As Integer, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoMotivo As Integer, _
            ByVal int_CodigoTipo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_ResumenBecasXPeriodo(int_CodigoPeriodoAcademico, int_CodigoNivelMinisterio, _
              int_CodigoGrado, int_CodigoMotivo, int_CodigoTipo, _
              int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_PagosRealizadosPorPeriodo( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_PagosRealizadosPorPeriodo(int_CodigoPeriodoAcademico, dt_FechaRangoInicial, dt_FechaRangoFinal, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_REP_DeudasGeneradas( _
         ByVal int_CodigoPeriodoAcademico As Integer, _
         ByVal str_CodigoConceptoCobro As String, _
         ByVal dt_FechaRangoFinal As Date, _
         ByVal int_CodigoGrado As Integer, _
         ByVal int_CodigoAula As Integer, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_DeudasGeneradas(int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, _
              dt_FechaRangoFinal, int_CodigoGrado, int_CodigoAula, _
              int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



        Public Function FUN_REP_PagosRealizadosExportacionSIE_General( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_PagosRealizadosExportacionSIE_General(int_CodigoPeriodoAcademico, dt_FechaRangoInicial, dt_FechaRangoFinal, _
             int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_PagosRealizadosExportacionSIE_Cancelados( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return Nothing '' obj_da_Pensiones.FUN_REP_PagosRealizadosExportacionSIE_Cancelados(int_CodigoPeriodoAcademico, dt_FechaRangoInicial, dt_FechaRangoFinal, _
            ''int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_PagosRealizadosExportacionSIE_Emitidos( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return Nothing ''obj_da_Pensiones.FUN_REP_PagosRealizadosExportacionSIE_Emitidos(int_CodigoPeriodoAcademico, dt_FechaRangoInicial, dt_FechaRangoFinal, _
            '' int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



        Public Function FUN_REP_BecaXmes( _
 ByVal int_CodigoAnioAcademico As Integer, _
 ByVal int_Mes As Integer, _
 ByVal int_Nivel As Integer, _
 ByVal int_SubNivel As Integer, _
 ByVal int_Grado As Integer, _
 ByVal int_Aula As Integer, _
 ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_BecaXmes(int_CodigoAnioAcademico, int_Mes, int_Nivel, int_SubNivel, _
              int_Grado, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_REP_TipoBecaNivelGradoAula( _
ByVal int_CodigoAnioAcademico As Integer, _
ByVal int_Mes As Integer, _
ByVal int_Nivel As Integer, _
ByVal int_SubNivel As Integer, _
ByVal int_Grado As Integer, _
ByVal int_Aula As Integer, _
ByVal int_CodigoTipoBeca As Integer, _
ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_TipoBecaNivelGradoAula(int_CodigoAnioAcademico, int_Mes, int_Nivel, int_SubNivel, _
              int_Grado, int_Aula, int_CodigoTipoBeca, _
              int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_TipoBecaOtorgada( _
ByVal int_CodigoAnioAcademico As Integer, _
ByVal int_CodigoTipoBeca As Integer, _
ByVal int_Nivel As Integer, _
ByVal int_SubNivel As Integer, _
ByVal int_Grado As Integer, _
ByVal int_Aula As Integer, _
ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_TipoBecaOtorgada(int_CodigoAnioAcademico, int_CodigoTipoBeca, int_Nivel, int_SubNivel, _
              int_Grado, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



        ' Proyecciones 
        ' Ingresos Generales
        Public Function FUN_REP_ProyeccionIngresos( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicio As Date, _
            ByVal dt_FechaRangoFin As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_ProyeccionIngresos(int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Cuotas de Ingreso
        Public Function FUN_REP_ProyeccionCuotasDeIngreso( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicio As Date, _
            ByVal dt_FechaRangoFin As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_ProyeccionCuotasDeIngreso(int_CodigoPeriodoAcademico, dt_FechaRangoInicio, dt_FechaRangoFin, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Morosidad
        Public Function FUN_REP_Morosidad(ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_Morosidad(int_CodigoPeriodoAcademico, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_MorosidadPorCorte( _
            ByVal dt_fechaCorte1 As Date, _
            ByVal dt_fechaCorte2 As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_MorosidadPorCorte(dt_fechaCorte1, dt_fechaCorte2, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_MorosidadHistoricoPorCorte( _
           ByVal dt_fechaCorte As Date, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_MorosidadHistoricoPorCorte(dt_fechaCorte, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_DeudasPorConceptosYAulaTotales( _
           ByVal int_CodigoPeriodoAcademico As Integer, _
           ByVal str_CodigoConceptoCobro As String, _
           ByVal dt_FechaRangoFinal As Date, _
           ByVal int_CodigoGrado As Integer, _
           ByVal int_CodigoAula As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_DeudasPorConceptosYAulaTotales(int_CodigoPeriodoAcademico, str_CodigoConceptoCobro, _
                dt_FechaRangoFinal, int_CodigoGrado, int_CodigoAula, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Seguro
        Public Function FUN_REP_Seguro( _
            ByVal int_PeriodoAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pensiones.FUN_REP_Seguro(int_PeriodoAcademico, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace