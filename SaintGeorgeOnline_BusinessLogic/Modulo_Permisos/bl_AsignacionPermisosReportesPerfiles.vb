Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos

    Public Class bl_AsignacionPermisosReportesPerfiles

#Region "Atributos"

        Private obj_da_AsignacionPermisosReportesPerfiles As da_AsignacionPermisosReportesPerfiles

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_AsignacionPermisosReportesPerfiles = New da_AsignacionPermisosReportesPerfiles

        End Sub

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_AsignacionPermisosReportesPerfil(ByVal int_CodigoPerfil As Integer, ByVal objDetalle As DataTable, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionPermisosReportesPerfiles.FUN_INS_AsignacionPermisosReportesPerfil(int_CodigoPerfil, objDetalle, str_Mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Agregar permisos por Usuario
        Public Function FUN_INS_AsignacionPermisosReportesParaUsuarios(ByVal int_CodigoTrabajador As Integer, ByVal int_TipoAccion As Integer, ByVal objDetalle As DataTable, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionPermisosReportesPerfiles.FUN_INS_AsignacionPermisosReportesParaUsuarios(int_CodigoTrabajador, int_TipoAccion, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_ConfiguracionPerfil(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionPermisosReportesPerfiles.FUN_GET_ConfiguracionPerfil(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PlantillaPerfilPorTrabajador(ByVal int_CodigoPerfil As Integer, ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionPermisosReportesPerfiles.FUN_LIS_PlantillaPerfilPorTrabajador(int_CodigoPerfil, int_CodigoTrabajador, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace