Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloEntrevistas

Namespace ModuloEntrevistas

    Public Class bl_MedioSolicitudEntrevista

#Region "Atributos"
        Private obj_da_MedioSolicitudEntrevista As da_MedioSolicitudEntrevista
#End Region

#Region "Constructor"
        Public Sub New()
            obj_da_MedioSolicitudEntrevista = New da_MedioSolicitudEntrevista
        End Sub
#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transacciones"
        Public Function FUN_LIS_MedioSolicitudEntrevista( _
            ByVal str_Descripcion As String, ByVal int_Estado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_MedioSolicitudEntrevista.FUN_LIS_MedioSolicitudEntrevista(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
#End Region

    End Class

End Namespace
