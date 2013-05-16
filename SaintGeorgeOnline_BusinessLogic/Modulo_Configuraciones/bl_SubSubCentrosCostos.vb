Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class bl_SubSubCentrosCostos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Piso As da_SubSubCentrosCostos

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
            obj_da_Piso = New da_SubSubCentrosCostos
        End Sub

#End Region

#Region "Metodos Transacciones"


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SubSubCentrosCostos(ByVal int_CodigoSubCentrosCostos As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Piso.FUN_LIS_SubSubCentrosCostos(int_CodigoSubCentrosCostos, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace


