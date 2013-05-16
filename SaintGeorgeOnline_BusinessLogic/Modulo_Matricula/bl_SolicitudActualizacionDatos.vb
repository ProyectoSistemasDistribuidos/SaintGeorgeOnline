Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula

    Public Class bl_SolicitudActualizacionDatos

#Region "Atributos"

        Private obj_da_SolicitudActualizacionDatos As da_SolicitudActualizacionDatos

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_SolicitudActualizacionDatos = New da_SolicitudActualizacionDatos
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_DatosSolicitudes(ByVal int_CodigoSolicitud As Integer, ByVal int_TipoSolicitud As Integer, ByVal str_CadenaPerfil As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudActualizacionDatos.FUN_LIS_DatosSolicitudes(int_CodigoSolicitud, int_TipoSolicitud, str_CadenaPerfil, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        'Datos del familiar que solicito la actualizacion de datos
        Public Function FUN_LIS_DatosSolicitanteActualizacion(ByVal int_CodigoSolcitud As Integer, ByVal int_TipoSolicitud As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudActualizacionDatos.FUN_LIS_DatosSolicitanteActualizacion(int_CodigoSolcitud, int_TipoSolicitud, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace