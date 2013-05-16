Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_ProgramacionRegistroGradosPronosticoss

#Region "Atributos"

        Private obj_da_ProgramacionRegistroGradosPronosticos As da_ProgramacionRegistroGradosPronosticos

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_ProgramacionRegistroGradosPronosticos = New da_ProgramacionRegistroGradosPronosticos
        End Sub

#End Region

#Region "Metodos Transacciones"



#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ProgramacionRegistroGradosPronosticos(ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionRegistroGradosPronosticos.FUN_LIS_ProgramacionRegistroGradosPronosticos(int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace