Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_Articulos

#Region "Atributos"

        Private obj_da_Articulos As da_Articulos

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_Articulos = New da_Articulos
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Articulos(ByVal str_Descripcion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Articulos.FUN_LIS_Articulos(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

