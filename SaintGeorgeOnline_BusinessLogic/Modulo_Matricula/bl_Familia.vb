Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula

    Public Class bl_Familia

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Familia As da_Familia

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
            obj_da_Familia = New da_Familia
        End Sub

#End Region

#Region "Metodos Transacciones"

        'update
        Public Function FUN_INS_Familia(ByVal str_NombreFamilia As String, ByVal str_AnioAcademico As String, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Familia.FUN_INS_Familia(str_NombreFamilia, str_AnioAcademico, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update
        Public Function FUN_UPD_FamiliaIntegrantesYAlumnos(ByVal int_CodigoFamilia As Integer, _
                                                          ByVal str_NombreFamilia As String, _
                                                          ByVal dt_Familiares As DataTable, _
                                                          ByVal dt_Alumnos As DataTable, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Familia.FUN_UPD_FamiliaIntegrantesYAlumnos(int_CodigoFamilia, str_NombreFamilia, dt_Familiares, dt_Alumnos, _
                str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_UPD_IntegranteFamilia(ByVal objFamiliares As DataTable, ByVal objDetalleFamiliares As DataTable, ByVal objDetalle_eliminar As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Familia.FUN_UPD_IntegrantesFamilia(objFamiliares, objDetalleFamiliares, objDetalle_eliminar, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_VAL_EliminarIntegranteFamilia(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Familia.FUN_VAL_EliminarIntegranteFamilia(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Familias(ByVal str_Descripcion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familia.FUN_LIS_Familias(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_Familia(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familia.FUN_GET_Familia(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' updated 24/01/12
        Public Function FUN_GET_DatosFamilia(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familia.FUN_GET_DatosFamilia(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_FamiliaHijos(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familia.FUN_GET_FamiliaHijos(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' update
        Public Function FUN_GET_FamiliaresConSolicitudActualizacion(ByVal str_Codigo As String, ByVal int_AnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familia.FUN_GET_FamiliaresConSolicitudActualizacion(str_Codigo, int_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' entrevista
        Public Function FUN_LIS_FamiliasPorPeriodoGradoYAula(ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
                                                             ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familia.FUN_LIS_FamiliasPorPeriodoGradoYAula(int_CodigoPeriodo, int_CodigoGrado, int_CodigoAula, _
                                                                       int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace