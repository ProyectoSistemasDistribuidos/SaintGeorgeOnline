Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess


Public Class bl_seguimiento
#Region "Atributos"

    Private str_Mensaje As String
    Private obj_da_ProgramacionWeekly As da_ProgramacionWeekly

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
        obj_da_ProgramacionWeekly = New da_ProgramacionWeekly
    End Sub

#End Region

#Region "Metodos Transacciones"


#End Region

#Region "Metodos No Transaccionales"

    Public Function FUN_LIS_PerformanceGroup(ByVal pdescripcion As String, ByVal pestado As Boolean, ByVal codigoUsuario As Integer, ByVal codigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

       
        Try
            Return New da_seguimiento().FUN_LIS_PerformanceGroup(pdescripcion, pestado, codigoUsuario, codigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Function FUN_LIS_EffortGrade(ByVal pdescripcion As String, ByVal pestado As Boolean, ByVal codigoUsuario As Integer, ByVal codigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        Try
            Return New da_seguimiento().FUN_LIS_EffortGrade(pdescripcion, pestado, codigoUsuario, codigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        Catch ex As Exception
        Finally

        End Try
    End Function

#End Region
End Class
