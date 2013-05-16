Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros
Namespace ModuloBancoLibros
    Public Class bl_TipoLibro
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_TipoLibro As da_TipoLibro
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
            obj_da_TipoLibro = New da_TipoLibro
        End Sub

#End Region

#Region "Metodos No Transaccionales"
        Public Function FUN_LIS_TipoLibro(ByVal str_Descripcion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_TipoLibro.FUN_LIS_TipoLibro(str_Descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region
    End Class
End Namespace

