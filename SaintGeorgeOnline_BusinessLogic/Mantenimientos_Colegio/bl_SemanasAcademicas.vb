Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio
Namespace ModuloColegio
    Public Class bl_SemanasAcademicas
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_SemanasAcademicas As da_SemanasAcademicas
        
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
            obj_da_SemanasAcademicas = New da_SemanasAcademicas
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_SemanasAcademicas(ByVal objSemanasAcademicas As be_SemanasAcademicas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SemanasAcademicas.FUN_INS_SemanasAcademicas(objSemanasAcademicas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_UPD_SemanasAcademicas(ByVal objSemanasAcademica As be_SemanasAcademicas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SemanasAcademicas.FUN_UPD_SemanasAcademicas(objSemanasAcademica, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_SemanasAcademicas(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SemanasAcademicas.FUN_DEL_SemanasAcademicas(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SemanasAcademicas(ByVal str_Descripcion As String, ByVal str_Abreviatura As String, ByVal str_DescripcionIngles As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_SemanasAcademicas.FUN_LIS_SemanasAcademicas(str_Descripcion, str_Abreviatura, str_DescripcionIngles, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_SemasAcademicas(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_SemanasAcademicas.FUN_GET_SemasAcademicas(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region
    End Class
End Namespace

