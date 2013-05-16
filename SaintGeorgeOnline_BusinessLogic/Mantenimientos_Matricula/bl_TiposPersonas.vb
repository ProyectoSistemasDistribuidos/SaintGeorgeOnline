Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula

    Public Class bl_TiposPersonas

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_TiposPersonas As da_TiposPersonas

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

            obj_da_TiposPersonas = New da_TiposPersonas

        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_TiposPersonas(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_TiposPersonas.FUN_LIS_TiposPersonas(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 18/01/2012
        Public Function FUN_LIS_TiposPersonasPorModulo(ByVal int_Modulo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_TiposPersonas.FUN_LIS_TiposPersonasPorModulo(int_Modulo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
