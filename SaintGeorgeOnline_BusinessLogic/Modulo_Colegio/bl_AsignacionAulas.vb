Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio

Namespace ModuloColegio

    Public Class bl_AsignacionAulas

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionAulas As da_AsignacionAulas

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
            obj_da_AsignacionAulas = New da_AsignacionAulas
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionAulas(ByVal objAsignacionAulas As be_AsignacionAulas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionAulas.FUN_INS_AsignacionAulas(objAsignacionAulas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_AsignacionAulas(ByVal objAsignacionAulas As be_AsignacionAulas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionAulas.FUN_UPD_AsignacionAulas(objAsignacionAulas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_AsignacionAulas(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionAulas.FUN_DEL_AsignacionAulas(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


        Public Function FUN_UPD_CierreAulas(ByVal dt_Lista As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionAulas.FUN_UPD_CierreAulas(dt_Lista, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionAulas(ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Aula As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulas(int_AnioAcademico, int_Sede, int_Aula, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionAulasParaGrupos(ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasParaGrupos(int_AnioAcademico, int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_AsignacionAulasParaGruposXNivel( _
           ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoNivel As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasParaGruposXNivel(int_AnioAcademico, int_CodigoGrado, int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_LIS_AsignacionAulasParaGruposXNivelMinisterio( _
           ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoNivel As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasParaGruposXNivelMinisterio(int_AnioAcademico, int_CodigoGrado, int_CodigoNivel, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_GET_AsignacionAulas(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_GET_AsignacionAulas(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update
        Public Function FUN_LIS_AsignacionAulasPorAnioAcademico(ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademico(int_AnioAcademico, int_Sede, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota(ByVal int_CodigoTrabajador As Integer, ByVal int_TipoNota As Integer, ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasPorAnioAcademicoTipoNota(int_CodigoTrabajador, int_TipoNota, int_AnioAcademico, int_Sede, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 28/02/2012
        Public Function FUN_LIS_AsignacionAulasPorProfesoryAnioAcademico(ByVal str_Usuario As String, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasPorProfesoryAnioAcademico(str_Usuario, int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 12-07-2012
        Public Function FUN_LIS_GradosPorProfesoryAnioAcademico(ByVal str_Usuario As String, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_GradosPorProfesoryAnioAcademico(str_Usuario, int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 19/03/2012
        Public Function FUN_LIS_AsignacionAulasParaCursosPorProfesoryAnioAcademico(ByVal str_Usuario As String, ByVal int_AnioAcademico As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasParaCursosPorProfesoryAnioAcademico(str_Usuario, int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 03/04/2012
        Public Function FUN_LIS_AsignacionAulasGradoPorAnioAcademico(ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasGradoPorAnioAcademico(int_AnioAcademico, int_Sede, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionAulasPorTutor( _
            ByVal int_CodigoTrabajador As Integer, ByVal int_AnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionAulas.FUN_LIS_AsignacionAulasPorTutor(int_CodigoTrabajador, int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

