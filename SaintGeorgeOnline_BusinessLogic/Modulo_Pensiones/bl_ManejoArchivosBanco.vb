Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_ManejoArchivosBanco

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_ManejoArchivosBanco As da_ManejoArchivosBanco

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
            obj_da_ManejoArchivosBanco = New da_ManejoArchivosBanco
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_EstructuraArchivo(ByVal int_CodigoBanco As Integer, ByVal int_CodigoTipoOperacion As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ManejoArchivosBanco.FUN_GET_EstructuraArchivo(int_CodigoBanco, int_CodigoTipoOperacion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
