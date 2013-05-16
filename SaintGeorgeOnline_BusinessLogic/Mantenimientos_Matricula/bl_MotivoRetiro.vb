Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula

    Public Class bl_MotivoRetiro

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_MotivoRetiro As da_MotivoRetiro

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
            obj_da_MotivoRetiro = New da_MotivoRetiro
        End Sub

#End Region

#Region "Metodos Transacciones"


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_MotivoRetiro(ByVal str_Descripcion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_MotivoRetiro.FUN_LIS_MotivoRetiro(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


#End Region

    End Class

End Namespace