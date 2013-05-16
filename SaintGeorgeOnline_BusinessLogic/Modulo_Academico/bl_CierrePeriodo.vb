Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloAcademico
Imports SaintGeorgeOnline_BusinessEntities.ModuloAcademico

Namespace ModuloAcademico
    Public Class bl_CierrePeriodo

#Region "Atributos"
        Private obj_da_CierrePeriodo As da_CierrePeriodo
#End Region
#Region "Constructor"
        Public Sub New()
            obj_da_CierrePeriodo = New da_CierrePeriodo
        End Sub
#End Region
#Region "Métodos Transaccionales"

#End Region
#Region "Método No Transaccionales"

        'multiple
        Public Function FUN_UPD_NotaFinal( _
          ByVal lst_CierrePeriodo As List(Of be_CierrePeriodo), _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_CierrePeriodo.FUN_UPD_NotaFinal(lst_CierrePeriodo, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        'x aula
        Public Function FUN_UPD_NotaFinal( _
          ByVal obe_CierrePeriodo As be_CierrePeriodo, ByRef str_mensaje As String, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_CierrePeriodo.FUN_UPD_NotaFinal(obe_CierrePeriodo, str_mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        'cursos oficiales c/ interno
        Public Function FUN_UPD_NotaFinalcOficialcInterno( _
           ByVal obe_CierrePeriodo As be_CierrePeriodo, ByRef str_mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_CierrePeriodo.FUN_UPD_NotaFinalcOficialcInterno(obe_CierrePeriodo, str_mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        'cursos oficiales c/ interno x bimestre
        Public Function FUN_UPD_NotaFinalcOficialcInternoXBimestre( _
           ByVal obe_CierrePeriodo As be_CierrePeriodo, ByRef str_mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_CierrePeriodo.FUN_UPD_NotaFinalcOficialcInternoXBimestre(obe_CierrePeriodo, str_mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region
    End Class
End Namespace