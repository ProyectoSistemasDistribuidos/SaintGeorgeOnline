
Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloActividades
Imports SaintGeorgeOnline_DataAccess.ModuloActividades

Namespace ModuloActividades

    Public Class bl_EstadosActividades

#Region "Atributos"

        Private obj_da_EstadosActividades As da_EstadosActividades

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_EstadosActividades = New da_EstadosActividades
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_EstadosActividades(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_EstadosActividades.FUN_LIS_EstadosActividades(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
