Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroNotasAnualCuantitativo

#Region "Atributos"

        Private obj_da_RegistroNotasAnualCuantitativo As da_RegistroNotasAnualCuantitativo

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroNotasAnualCuantitativo = New da_RegistroNotasAnualCuantitativo

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RegistroNotasAnualCuantitativo( _
            ByVal obj_RegistroNotasAnualCualitativo As be_RegistroNotasAnualCuantitativo, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroNotasAnualCuantitativo.FUN_INS_RegistroNotasAnualCuantitativo(obj_RegistroNotasAnualCualitativo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_RegistroNotasAnualCuantitativoHistorica( _
            ByVal lstNotas As List(Of be_RegistroNotasAnualCuantitativo), ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_RegistroNotasAnualCuantitativo.FUN_INS_RegistroNotasAnualCuantitativoHistorica( _
            lstNotas, str_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

#End Region

    End Class

End Namespace