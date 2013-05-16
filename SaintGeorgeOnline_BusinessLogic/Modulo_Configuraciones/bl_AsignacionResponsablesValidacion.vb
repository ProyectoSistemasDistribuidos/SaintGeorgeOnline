Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_AsignacionResponsablesValidacion

#Region "Atributos"

        Private obj_da_AsignacionResponsablesValidacion As da_AsignacionResponsablesValidacion

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_AsignacionResponsablesValidacion = New da_AsignacionResponsablesValidacion
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionResponsablesValidacion(ByVal objAsignacionResponsablesValidacion As be_AsignacionResponsablesValidacion, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionResponsablesValidacion.FUN_INS_AsignacionResponsablesValidacion(objAsignacionResponsablesValidacion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_AsignacionResponsablesValidacion(ByVal objAsignacionResponsablesValidacion As be_AsignacionResponsablesValidacion, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionResponsablesValidacion.FUN_DEL_AsignacionResponsablesValidacion(objAsignacionResponsablesValidacion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionResponsablesValidacion(ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoSede As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionResponsablesValidacion.FUN_LIS_AsignacionResponsablesValidacion(int_CodigoPeriodo, int_CodigoSede, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_AsignacionResponsablesValidacionCompleta(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionResponsablesValidacion.FUN_LIS_AsignacionResponsablesValidacionCompleta(int_CodigoAsignacionSSSCentroCosto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionResponsablesValidacionDetalle(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionResponsablesValidacion.FUN_LIS_AsignacionResponsablesValidacionDetalle(int_CodigoAsignacionSSSCentroCosto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_AsignacionResponsablePresupuesto(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionResponsablesValidacion.FUN_LIS_AsignacionResponsablePresupuesto(int_CodigoAsignacionSSSCentroCosto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionResponsablesValidacionSistemas(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionResponsablesValidacion.FUN_LIS_AsignacionResponsablesValidacionSistemas(int_CodigoAsignacionSSSCentroCosto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_AsignacionResponsablesValidacionGerencia(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionResponsablesValidacion.FUN_LIS_AsignacionResponsablesValidacionGerencia(int_CodigoAsignacionSSSCentroCosto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_AsignacionResponsablesPresupuestoConValidadores(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionResponsablesValidacion.FUN_LIS_AsignacionResponsablesPresupuestoConValidadores(int_CodigoAsignacionSSSCentroCosto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

    End Class

End Namespace