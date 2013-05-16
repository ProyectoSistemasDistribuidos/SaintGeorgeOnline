Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloTareas
Imports SaintGeorgeOnline_DataAccess.ModuloTareas

'd

Namespace ModuloTareas

    Public Class bl_TareaAcademica

#Region "Atributos"

        Private obj_da_TareaAcademica As da_TareaAcademica

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_TareaAcademica = New da_TareaAcademica
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_TareaAcademica(ByVal objTareaAcademica As be_TareaAcademica, ByVal dt_RutaAdjuntos As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_TareaAcademica.FUN_INS_TareaAcademica(objTareaAcademica, dt_RutaAdjuntos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_TareaAcademica(ByVal int_CodigoTareaAcademica As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_TareaAcademica.FUN_DEL_TareaAcademica(int_CodigoTareaAcademica, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_TareaAcademica(ByVal int_CodigoAsignacionAula As Integer, _
                                       ByVal int_CodigoAsignacionCurso As Integer, _
                                       ByVal int_CodigoProfesor As Integer, _
                                        ByVal dt_FechaVencimientoInicio As Date, _
                                        ByVal dt_FechaVencimientoFin As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_TareaAcademica.FUN_LIS_TareaAcademica(int_CodigoAsignacionAula, int_CodigoAsignacionCurso, int_CodigoProfesor, dt_FechaVencimientoInicio, dt_FechaVencimientoFin, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_TareaAcademicaFamiliar(ByVal int_CodigoAnio As Integer, _
                                              ByVal int_CodigoMes As Integer, _
                                               ByVal str_CodigoAlumno As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_TareaAcademica.FUN_LIS_TareaAcademicaFamiliar(int_CodigoAnio, int_CodigoMes, str_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DescripcionTareas(ByVal int_CodigoTarea As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_TareaAcademica.FUN_LIS_DescripcionTareas(int_CodigoTarea, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_TareaAcademicaSalon(ByVal int_CodigoAnio As Integer, _
                                              ByVal int_CodigoMes As Integer, _
                                              ByVal int_CodigoSalon As Integer, _
                                              ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_TareaAcademica.FUN_LIS_TareaAcademicaSalon(int_CodigoAnio, int_CodigoMes, int_CodigoSalon, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace