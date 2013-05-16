Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros
Namespace ModuloBancoLibros
    Public Class bl_CursosLibros
#Region "Atributos"
        Private str_Mensaje As String
        Private obj_da_CursosLibros As da_CursosLibros
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
            obj_da_CursosLibros = New da_CursosLibros
        End Sub
#End Region
#Region "Metodos No Transaccionales"
        Public Function FUN_LIST_CursosLibros(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CursosLibros.FUN_LIST_CursosLibros(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region
    End Class
End Namespace

