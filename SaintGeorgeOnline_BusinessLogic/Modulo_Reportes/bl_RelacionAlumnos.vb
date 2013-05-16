Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloReportes
Namespace ModuloReportes
    Public Class bl_RelacionAlumnos

#Region "Atributos"
        Private str_Mensaje As String
        Private obj_da_Reportes As da_RelacionAlumnos
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
            obj_da_Reportes = New da_RelacionAlumnos
        End Sub
#End Region
#Region "Métodos Transaccionales"

#End Region
#Region "Método No Transaccionales"

        Public Function FUN_REP_RelacionAlumnoXControl(ByVal codAnio As Integer, ByVal codGrado As Integer, ByVal codAula As Integer, _
                                            ByVal codUsuario As Integer, ByVal codTipoUsuario As Integer, ByVal codModulo As Integer _
                                           , ByVal codOpcion As Integer) As DataSet
            Return obj_da_Reportes.FUN_REP_RelacionAlumnoXControl(codAnio, codGrado, codAula, codUsuario, codTipoUsuario, codModulo, codOpcion)
        End Function
        Public Function FUN_REP_RelacionAlumnoXcumpleaniosMes(ByVal codAnio As Integer, ByVal codMes As Integer, _
                                                              ByVal codNiv As Integer, ByVal codGdo As Integer, _
                                                                ByVal codAul As Integer, ByVal codUsuario As Integer, ByVal codTipoUsuario As Integer, _
                                                                ByVal codModulo As Integer, ByVal codOpcion As Integer) As DataSet
            Return obj_da_Reportes.FUN_REP_RelacionAlumnoXcumpleaniosMes(codAnio, codMes, codNiv, codGdo, codAul, codUsuario, codTipoUsuario, codModulo, codOpcion)
        End Function
        Public Function FUN_REP_RelacionAlumnoXfirmasPadres(ByVal codAnio As Integer, ByVal codGrado As Integer, ByVal codAula As Integer, _
                                            ByVal codUsuario As Integer, ByVal codTipoUsuario As Integer, ByVal codModulo As Integer _
                                           , ByVal codOpcion As Integer) As DataSet
            Return obj_da_Reportes.FUN_REP_RelacionAlumnoXfirmasPadres(codAnio, codGrado, codAula, codUsuario, codTipoUsuario, codModulo, codOpcion)
        End Function
        Public Function FUN_REP_REP_RelacionAlumnoXsalon(ByVal codAnio As Integer, ByVal codNivel As Integer, ByVal codSubnivel As Integer, ByVal codGrado As Integer, ByVal codAula As Integer, _
                                            ByVal codUsuario As Integer, ByVal codTipoUsuario As Integer, ByVal codModulo As Integer _
                                           , ByVal codOpcion As Integer) As DataSet
            Return obj_da_Reportes.FUN_REP_RelacionAlumnoXsalon(codAnio, codNivel, codSubnivel, codGrado, codAula, codUsuario, codTipoUsuario, codModulo, codOpcion)
        End Function


        Public Function FUN_REP_REP_RelacionAlumnoXsexo(ByVal codAnio As Integer, ByVal codGrado As Integer, ByVal codAula As Integer, _
                                           ByVal codUsuario As Integer, ByVal codTipoUsuario As Integer, ByVal codModulo As Integer _
                                          , ByVal codOpcion As Integer) As DataSet
            Return obj_da_Reportes.FUN_REP_RelacionAlumnoXsexo(codAnio, codGrado, codAula, codUsuario, codTipoUsuario, codModulo, codOpcion)
        End Function


        Public Function FUN_REP_REP_RelacionAlumnoXtelefono(ByVal codAnio As Integer, ByVal codGrado As Integer, ByVal codAula As Integer, _
                                           ByVal codUsuario As Integer, ByVal codTipoUsuario As Integer, ByVal codModulo As Integer _
                                          , ByVal codOpcion As Integer) As DataSet
            Return obj_da_Reportes.FUN_REP_RelacionAlumnoXtelefono(codAnio, codGrado, codAula, codUsuario, codTipoUsuario, codModulo, codOpcion)
        End Function

        Public Function FUN_REP_RelacionAlumnosPorTipoMatricula(ByVal int_codigoAnio As Integer, ByVal int_codigoTipoMatricula As Integer, _
                                            ByVal int_codigoUsuario As Integer, ByVal int_codigoTipoUsuario As Integer, ByVal int_codigoModulo As Integer, ByVal int_codigoOpcion As Integer) As DataSet

            Return obj_da_Reportes.FUN_REP_RelacionAlumnosPorTipoMatricula(int_codigoAnio, int_codigoTipoMatricula, int_codigoUsuario, int_codigoTipoUsuario, int_codigoModulo, int_codigoOpcion)

        End Function

        ' Notas 06/11/2012
        Public Function FUN_REP_RegistroNotasCriteriosyEvaluaciones(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Reportes.FUN_REP_RegistroNotasCriteriosyEvaluaciones(int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Notas Consolidado 11/12/2012
        Public Function FUN_REP_RegistroNotasAcumulativas( _
            ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoTipoReporte As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Reportes.FUN_REP_RegistroNotasAcumulativas(int_CodigoAsignacionAula, int_CodigoTipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' Consolidado Por Tercio 03/01/2013
        Public Function FUN_REP_ConsolidadoPorTercios( _
            ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Reportes.FUN_REP_ConsolidadoPorTercios(int_CodigoPeriodo, int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        ' Notas Consolidado Actas Senior 08/01/2013
        Public Function FUN_REP_ConsolidadoNotasActasSenior( _
            ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Reportes.FUN_REP_ConsolidadoNotasActasSenior(int_CodigoAsignacionAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region
    End Class
End Namespace
