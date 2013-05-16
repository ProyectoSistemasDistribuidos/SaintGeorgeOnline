Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros

Namespace ModuloBancoLibros

    Public Class bl_CopiaLibros
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_CopiaLibros As da_CopiaLibros

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
            obj_da_CopiaLibros = New da_CopiaLibros
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_CopiaLibros(ByVal objCopiaLibros As be_CopiaLibros, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CopiaLibros.FUN_INS_CopiaLibros(objCopiaLibros, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_CopiaLibros(ByVal objCopiaLibros As be_CopiaLibros, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CopiaLibros.FUN_UPD_CopiaLibros(objCopiaLibros, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_CopiaLibros(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CopiaLibros.FUN_DEL_CopiaLibros(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"
        'ok
        'Public Function FUN_LIS_CopiaLibros(ByVal int_AnioAcademico As Integer, ByVal str_Titulo As String, ByVal int_CodigoIdioma As Integer, ByVal str_ISBN As String, ByVal int_CodigoGrado As Integer, ByVal int_CodigoTipoReporte As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
        '    Return obj_da_Libros.FUN_LIS_CopiaLibros(int_AnioAcademico, str_Titulo, int_CodigoIdioma, str_ISBN, int_CodigoGrado, int_CodigoTipoReporte, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        'End Function

        Public Function FUN_GET_CopiaLibros(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CopiaLibros.FUN_GET_CopiaLibros(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
