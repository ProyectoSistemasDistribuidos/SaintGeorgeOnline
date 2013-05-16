Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloAsistenciaAlumnos
Imports SaintGeorgeOnline_DataAccess.ModuloAsistenciaAlumnos
'ok
Namespace ModuloAsistenciaAlumnos

    Public Class bl_MedioUso

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_MedioUso As da_MedioUso

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
            obj_da_MedioUso = New da_MedioUso
        End Sub

#End Region

#Region "Metodos Transacciones"
     
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_MedioUso(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_MedioUso.FUN_LIS_MedioUso(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
