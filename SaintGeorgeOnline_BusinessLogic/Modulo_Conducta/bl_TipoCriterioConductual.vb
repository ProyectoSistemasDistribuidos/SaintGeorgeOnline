Imports SaintGeorgeOnline_DataAccess.ModuloConductaAlumnos
Namespace ModuloConductaAlumnos
    Public Class bl_TipoCriterioConductual
#Region "Atributos"
        Dim str_Mensaje As String
        Dim obj_da_TipoCriterioConductual As da_TipoCriterioConductual
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
            obj_da_TipoCriterioConductual = New da_TipoCriterioConductual
        End Sub
#End Region
#Region "Región Transaccionales"

#End Region
#Region "Región No Transaccionales"
        Public Function FUN_LIS_TipoCriterioConductual(ByVal CodigoUsuario As Integer, ByVal CodigoTipoUsuario As Integer, _
                                                        ByVal CodigoModulo As Integer, ByVal CodigoOpcion As Integer) As DataSet
            Return obj_da_TipoCriterioConductual.FUN_LIS_TipoCriterioConductual(CodigoUsuario, CodigoTipoUsuario, CodigoModulo, CodigoOpcion)
        End Function

        Public Function FUN_LIS_TipoCriterioConductualSinBM(ByVal CodigoUsuario As Integer, ByVal CodigoTipoUsuario As Integer, _
                                                     ByVal CodigoModulo As Integer, ByVal CodigoOpcion As Integer) As DataSet
            Return obj_da_TipoCriterioConductual.FUN_LIS_TipoCriterioConductualSinBM(CodigoUsuario, CodigoTipoUsuario, CodigoModulo, CodigoOpcion)
        End Function
#End Region
    End Class
End Namespace

