Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloAsistenciaAlumnos
Imports SaintGeorgeOnline_DataAccess.ModuloAsistenciaAlumnos
'ok 06/06/2012
Namespace ModuloAsistenciaAlumnos

    Public Class bl_Motivo
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Motivo As da_Motivo

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
            obj_da_Motivo = New da_Motivo
        End Sub

#End Region

#Region "Metodos Transacciones"
        Public Function FUN_INS_Motivo(ByVal objDiagnostico As be_Motivo, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Motivo.FUN_INS_Motivo(objDiagnostico, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Motivo(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Motivo.FUN_LIS_Motivo(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace
