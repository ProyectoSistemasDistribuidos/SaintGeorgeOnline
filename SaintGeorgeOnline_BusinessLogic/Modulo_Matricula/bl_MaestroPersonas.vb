Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula

    Public Class bl_MaestroPersonas

#Region "Atributos"

        Private str_Mensaje As String
        Private int_CodigoPersona As Integer
        Private int_CodigoOtros As Integer
        Private obj_da_MaestroPersonas As da_MaestroPersonas

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_MaestroPersonas = New da_MaestroPersonas
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_Otros(ByVal objOtros As be_Otros, ByVal int_CodigoOtros As Integer, ByRef int_CodigoPersona As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_MaestroPersonas.FUN_INS_Otros(objOtros, int_CodigoOtros, int_CodigoPersona, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Personas(ByVal objMaestroPersona As be_MaestroPersonas, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MaestroPersonas.FUN_LIS_Personas(objMaestroPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_AlumnoPorCodigoPersona(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MaestroPersonas.FUN_GET_AlumnoPorCodigoPersona(int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_CON_PersonaDisponibilidad(ByVal objPersona As be_Personas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_MaestroPersonas.FUN_CON_PersonaDisponibilidad(objPersona, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PersonaDisponibilidad(ByVal objPersona As be_Personas, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MaestroPersonas.FUN_LIS_PersonaDisponibilidad(objPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PersonasPorTipo(ByVal int_CodigoTipoPersona As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer) As DataSet

            Return obj_da_MaestroPersonas.FUN_LIS_PersonasPorTipo(int_CodigoTipoPersona, int_CodigoModulo, int_CodigoOpcion, int_CodigoUsuario, int_CodigoTipoUsuario)

        End Function

        Public Function FUN_LIS_PersonasProfesores(ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer) As DataSet

            Return obj_da_MaestroPersonas.FUN_LIS_PersonasProfesores(int_CodigoModulo, int_CodigoOpcion, int_CodigoUsuario, int_CodigoTipoUsuario)

        End Function

        Public Function FUN_LIS_PersonasProfesoresPorTipoPeriodoGradoAula( _
            ByVal int_Tipo As Integer, ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MaestroPersonas.FUN_LIS_PersonasProfesoresPorTipoPeriodoGradoAula( _
                int_Tipo, int_CodigoPeriodo, int_CodigoGrado, int_CodigoAula, _
                int_CodigoModulo, int_CodigoOpcion, int_CodigoUsuario, int_CodigoTipoUsuario)

        End Function

#End Region

    End Class

End Namespace




