Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos
Imports SaintGeorgeOnline_DataAccess.ModuloCursos

Namespace ModuloCursos

    Public Class bl_FormasCalificacion

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_FormasCalificacion As da_FormasCalificacion

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
            obj_da_FormasCalificacion = New da_FormasCalificacion
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        'update
        Public Function FUN_LIS_FormasCalificacion(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_FormasCalificacion.FUN_LIS_FormasCalificacion(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace





