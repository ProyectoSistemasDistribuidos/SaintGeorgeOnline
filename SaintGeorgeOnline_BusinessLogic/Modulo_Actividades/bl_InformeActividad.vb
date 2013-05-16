Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloActividades
Imports SaintGeorgeOnline_DataAccess.ModuloActividades

Namespace ModuloActividades

    Public Class bl_InformeActividad
#Region "Atributos"

        Private obj_da_InformeActividad As da_InformeActividad

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_InformeActividad = New da_InformeActividad
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_InformeActividad( _
            ByVal obe_InformeActividad As be_InformeActividad, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_InformeActividad.FUN_INS_InformeActividades(obe_InformeActividad, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"


#End Region

    End Class

End Namespace