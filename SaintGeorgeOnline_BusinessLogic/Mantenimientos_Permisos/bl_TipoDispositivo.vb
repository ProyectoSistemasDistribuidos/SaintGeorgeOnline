Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos

    Public Class bl_TipoDispositivo

#Region "Atributos"
        Private obj_da_TipoDispositivo As da_TipoDispositivo
#End Region

#Region "Constructor"
        Public Sub New()
            obj_da_TipoDispositivo = New da_TipoDispositivo
        End Sub
#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_TipoDispositivo(ByVal str_Descripcion As String, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_TipoDispositivo.FUN_LIS_TipoDispositivo(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

    End Class

End Namespace

