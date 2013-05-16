Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos

    Public Class bl_SubbloquesMenus

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_SubbloqueMenu As da_SubbloquesMenus

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
            obj_da_SubbloqueMenu = New da_SubbloquesMenus
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_SubbloqueMenu(ByVal objSubbloqueMenu As be_SubbloquesMenus, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SubbloqueMenu.FUN_INS_SubbloqueMenu(objSubbloqueMenu, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_SubbloqueMenu(ByVal objSubbloqueMenu As be_SubbloquesMenus, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SubbloqueMenu.FUN_UPD_SubbloqueMenu(objSubbloqueMenu, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_SubbloqueMenu(ByVal objSubbloqueMenu As be_SubbloquesMenus, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SubbloqueMenu.FUN_DEL_SubbloqueMenu(objSubbloqueMenu, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SubbloqueMenu(ByVal int_SubBloque As Integer, ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_EstadoProceso As Integer, ByVal int_TipoSubBloque As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_SubbloqueMenu.FUN_LIS_SubbloqueMenu(int_SubBloque, str_Descripcion, int_Estado, int_EstadoProceso, int_TipoSubBloque, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_SubbloqueMenu(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_SubbloqueMenu.FUN_GET_SubbloqueMenu(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_ValidarSubbloqueMenu(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_SubbloqueMenu.FUN_DEL_ValidarSubbloqueMenu(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace

