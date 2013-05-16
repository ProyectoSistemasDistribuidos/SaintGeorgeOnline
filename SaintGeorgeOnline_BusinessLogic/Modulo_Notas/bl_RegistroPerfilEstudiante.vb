Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroPerfilEstudiante

#Region "Atributos"

        Private obj_da_RegistroPerfilEstudiante As da_RegistroPerfilEstudiante

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroPerfilEstudiante = New da_RegistroPerfilEstudiante

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_RegistroNotasDescriptores( _
            ByVal arr_RegistroPerfilEstudiante As List(Of be_RegistroPerfilEstudiante), _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoBimestre As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroPerfilEstudiante.FUN_INS_RegistroPerfilEstudiante( _
                arr_RegistroPerfilEstudiante, int_CodigoAnioAcademico, int_CodigoBimestre, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"


#End Region

    End Class

End Namespace