Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class bl_CategoriaAtencion

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_CategoriaAtencion As da_CategoriaAtencion

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
            obj_da_CategoriaAtencion = New da_CategoriaAtencion
        End Sub

#End Region

#Region "Metodos Transacciones"

      
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_CategoriaAtencion(ByVal str_Descripcion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CategoriaAtencion.FUN_LIS_CategoriaAtencion(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

     
#End Region

    End Class

End Namespace