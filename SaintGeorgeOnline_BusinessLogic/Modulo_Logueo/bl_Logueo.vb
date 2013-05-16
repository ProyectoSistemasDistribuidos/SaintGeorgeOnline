Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloLogueo
Imports SaintGeorgeOnline_BusinessEntities.ModuloLogueo

Namespace ModuloLogueo

    Public Class bl_Logueo

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Logueo As da_Logueo
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
            obj_da_Logueo = New da_Logueo
        End Sub

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_PermisosUsuario(ByVal str_CodigoUsuario As String, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Logueo.FUN_GET_PermisosUsuario(str_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_VAL_PermisosUsuario(ByVal str_Usuario As String, ByVal Contrasenia As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Logueo.FUN_VAL_PermisosUsuario(str_Usuario, Contrasenia, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_EmailUsuario(ByVal str_Usuario As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataTable
            Return obj_da_Logueo.FUN_GET_EmailUsuario(str_Usuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_EmailDNI(ByVal str_Usuario As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataTable
            Return obj_da_Logueo.FUN_GET_EmailDNI(str_Usuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_VAL_PermisosSuperUsuario(ByVal int_CodigoUsuario As String, ByVal str_ContraseniaSuperUsuario As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Logueo.FUN_VAL_PermisosSuperUsuario(int_CodigoUsuario, str_ContraseniaSuperUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_DatosUsuarioReferencia(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Logueo.FUN_GET_DatosUsuarioReferencia(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_REP_DinamicoUsuariosEnElSistema(ByVal str_FechaInicio As String, ByVal str_FechaFin As String, ByVal int_TipoPersona As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Logueo.FUN_REP_DinamicoUsuariosEnElSistema(str_FechaInicio, str_FechaFin, int_TipoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_GET_PresupuestosUsuario(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal codAnioPeridico As Integer) As DataSet

            Return obj_da_Logueo.FUN_GET_PresupuestosUsuario(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, codAnioPeridico)

        End Function
#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_AccesoUsuario(ByVal objUsuario As be_Usuario, ByRef str_Mensaje As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Logueo.FUN_INS_AccesoUsuario(objUsuario, str_Mensaje, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_INS_AccesoUsuarioDetalle(ByVal int_CodigoSession As Integer, ByVal int_CodigoModulo As Integer, ByRef int_CodigoSubBloque As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer) As Integer
            Return obj_da_Logueo.FUN_INS_AccesoUsuarioDetalle(int_CodigoSession, int_CodigoModulo, int_CodigoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario)
        End Function

        Public Function FUN_INS_ClaveSuperUsuario(ByVal int_CodigoUsuario As Integer, ByVal str_Contrasenia As String, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Logueo.FUN_INS_ClaveSuperUsuario(int_CodigoUsuario, str_Contrasenia, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace

