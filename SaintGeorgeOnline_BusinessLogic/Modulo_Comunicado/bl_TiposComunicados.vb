Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloComunicado
Imports SaintGeorgeOnline_DataAccess.ModuloComunicado

Namespace ModuloComunicado

    Public Class bl_TiposComunicados

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_TiposComunicados As da_TiposComunicados

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
            obj_da_TiposComunicados = New da_TiposComunicados
        End Sub

#End Region

#Region "Metodos Transacciones"



#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_TiposComunicados(ByVal str_Descripcion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_TiposComunicados.FUN_LIS_TiposComunicados(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace

