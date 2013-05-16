Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros
'ok

Namespace ModuloBancoLibros

    Public Class bl_AsignacionVigenciaLibros
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionVigenciaLibros As da_AsignacionVigenciaLibros

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
            obj_da_AsignacionVigenciaLibros = New da_AsignacionVigenciaLibros
        End Sub

#End Region

#Region "Metodos Transacciones"

     

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionVigenciaLibros(ByVal int_AnioAcademico1 As Integer, ByVal int_AnioAcademico2 As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsignacionVigenciaLibros.FUN_LIS_AsignacionVigenciaLibros(int_AnioAcademico1, int_AnioAcademico2, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
