Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos
    Public Class bl_AsignacionPermisosPerfiles

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionPermisosPerfiles As da_AsignacionPermisosPerfiles

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
            obj_da_AsignacionPermisosPerfiles = New da_AsignacionPermisosPerfiles
        End Sub

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_DetalleAsignacionPermisosPerfiles(ByVal objDetalle As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionPermisosPerfiles.FUN_INS_DetalleAsignacionPermisosPerfiles(objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Agregar permisos por usuarios
        Public Function FUN_INS_AsignacionPermisosParaUsuarios(ByVal int_CodigoTrabajador As Integer, ByVal int_TipoAccion As Integer, ByVal objDetalle As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_AsignacionPermisosPerfiles.FUN_INS_AsignacionPermisosParaUsuarios(int_CodigoTrabajador, int_TipoAccion, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_ConfiguracionPerfil(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionPermisosPerfiles.FUN_GET_ConfiguracionPerfil(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PlantillaPerfilPorTrabajador(ByVal int_CodigoPerfil As Integer, ByVal int_CodigoTrabajador As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionPermisosPerfiles.FUN_LIS_PlantillaPerfilPorTrabajador(int_CodigoPerfil, int_CodigoTrabajador, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

