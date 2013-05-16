Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroNotasDescriptores

#Region "Atributos"

        Private obj_da_registroNotasDescriptores As da_RegistroNotasDescriptores

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_registroNotasDescriptores = New da_RegistroNotasDescriptores

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RegistroNotasDescriptores( _
            ByVal arr_RegistroNotasDescriptores As List(Of be_RegistroNotasDescriptores), _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoAsignacionGrupo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_registroNotasDescriptores.FUN_INS_RegistroNotasDescriptores( _
              arr_RegistroNotasDescriptores, int_CodigoAnioAcademico, int_CodigoBimestre, int_CodigoAsignacionGrupo, _
              int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"


#End Region

    End Class

End Namespace