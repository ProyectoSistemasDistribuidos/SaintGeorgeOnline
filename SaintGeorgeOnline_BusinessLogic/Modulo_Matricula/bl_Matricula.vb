Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula
    Public Class bl_Matricula

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Matricula As da_Matricula

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
            obj_da_Matricula = New da_Matricula
        End Sub

#End Region

#Region "Metodos No Transaccionales"
        ' updated 15/11/2012
        Public Function FUN_LIS_NotasPlantillaSIAGIE(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_LIS_NotasPlantillaSIAGIE(int_CodigoAnioAcademico, int_CodigoGrado, int_CodigoAula, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_DatosEmergenciaAlumno(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_GET_DatosEmergenciaAlumno(int_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_NominaMatricula(ByVal int_CodigoSede As Integer, ByVal int_AnioAcademico As Integer, ByVal int_CodGrado As Integer, ByVal int_Aula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_LIS_NominaMatricula(int_CodigoSede, int_AnioAcademico, int_CodGrado, int_Aula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 
        Public Function FUN_LIS_RecepcionDocumentosMatricula(ByVal int_TipoFiltro As Integer, ByVal str_Filtro As String, ByVal int_CodigoAnioMatricula As Integer, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_LIS_RecepcionDocumentosMatricula(int_TipoFiltro, str_Filtro, int_CodigoAnioMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' updated 03/08/2012
        Public Function FUN_REP_RecepcionDocumentosMatricula(ByVal int_TipoFiltro As Integer, ByVal str_Filtro As String, ByVal int_CodigoAnioMatricula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_REP_RecepcionDocumentosMatricula(int_TipoFiltro, str_Filtro, int_CodigoAnioMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_RetencionMatricula(ByVal int_TipoFiltro As Integer, ByVal str_Filtro As String, ByVal int_CodigoAnioMatricula As Integer, _
       ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_LIS_RetencionMatricula(int_TipoFiltro, str_Filtro, int_CodigoAnioMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 
        Public Function FUN_VAL_DeudasYPagosMatricula(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_VAL_DeudasYPagosMatricula(str_CodigoAlumno, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_VAL_RequisitosMatricula(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_VAL_RequisitosMatricula(str_CodigoAlumno, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_VAL_AceptacionDocumentoDireccion(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_VAL_AceptacionDocumentoDireccion(int_CodigoFamilia, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update
        Public Function FUN_LIS_RegistroMatriculaXalumno(ByVal int_CodigoAnioMatricula As Integer, ByVal str_CodigoAlumno As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_LIS_RegistroMatriculaXalumno(int_CodigoAnioMatricula, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DocumentosPasosMatricula(ByVal int_CodigoAnioMatricula As Integer, ByVal str_CodigoAlumno As String, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_LIS_DocumentosPasosMatricula(int_CodigoAnioMatricula, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_UltimoPasoMatriculaAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoEtapa As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_LIS_UltimoPasoMatriculaAlumno(str_CodigoAlumno, int_CodigoEtapa, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Reportes 02/01/2012
        Public Function FUN_REP_AlumnosMatriculadosXGradoFechas( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_REP_AlumnosMatriculadosXGradoFechas(int_CodigoPeriodoAcademico, _
                dt_FechaRangoInicial, dt_FechaRangoFinal, int_CodigoGrado, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Reportes 27/01/2012
        Public Function FUN_REP_AlumnosMatriculadosXGrado( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_REP_AlumnosMatriculadosXGrado(int_CodigoPeriodoAcademico, _
                dt_FechaRangoInicial, dt_FechaRangoFinal, int_CodigoGrado, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Reportes 05/01/2012 ok
        Public Function FUN_REP_AlumnosPasosMatricula( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_REP_AlumnosPasosMatricula(int_CodigoPeriodoAcademico, _
                dt_FechaRangoInicial, dt_FechaRangoFinal, int_CodigoGrado, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'No Matriculados
        Public Function FUN_REP_AlumnosNoMatriculadosXGrado(ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Matricula.FUN_REP_AlumnosNoMatriculadosXGrado(int_CodigoPeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_PasoMatricula(ByVal objMatricula As be_Matricula, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Matricula.FUN_INS_PasoMatricula(objMatricula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_Matricula(ByVal objMatricula As be_Matricula, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Matricula.FUN_INS_Matricula(objMatricula, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_MatriculaDeudas(ByVal objMatricula As be_Matricula, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Matricula.FUN_INS_MatriculaDeudas(objMatricula, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 
        Public Function FUN_INS_RecepcionDocumentosMatricula(ByVal dtLista As DataTable, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Matricula.FUN_INS_RecepcionDocumentosMatricula(dtLista, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_RegistroRestriccionMatricula(ByVal dtLista As DataTable, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Matricula.FUN_INS_RegistroRestriccionMatricula(dtLista, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class
End Namespace