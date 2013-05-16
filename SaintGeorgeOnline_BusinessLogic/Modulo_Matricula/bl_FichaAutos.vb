Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula
Namespace ModuloMatricula
    Public Class bl_FichaAutos
#Region "Atributos"
        Private str_Mensaje As String
        Private obj_da_FichaAutos As da_FichaAutos
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
            obj_da_FichaAutos = New da_FichaAutos
        End Sub

#End Region
#Region "Método Transaccionales"
        Public Function FUN_INS_FichaAutos(ByVal objFichaAutos As be_FichaAutos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_FichaAutos.FUN_INS_FichaAutos(objFichaAutos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_UPD_FichaAutos(ByVal objFichaAuto As be_FichaAutos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_FichaAutos.FUN_UPD_FichaAutos(objFichaAuto, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_DEL_FichaAutos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_FichaAutos.FUN_DEL_FichaAutos(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region
#Region "Metodos No Transaccionales"
        Public Function FUN_LIS_FichaAutos(ByVal obj_be_FichaAutos As be_FichaAutos, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_FichaAutos.FUN_LIS_FichaAutos(obj_be_FichaAutos, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_GET_FichaAulas(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_FichaAutos.FUN_GET_FichaAutos(str_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region
    End Class
End Namespace

