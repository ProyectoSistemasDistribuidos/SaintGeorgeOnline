Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloFotocopia
Imports SaintGeorgeOnline_DataAccess.ModuloFotocopias

Namespace ModuloMatricula

    Public Class bl_SolicitudFotocopias

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_SolicitudFotocopias As da_SolicitudFotocopias

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
            obj_da_SolicitudFotocopias = New da_SolicitudFotocopias
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_SolicitudFotocopias(ByVal objSolicitudFotocopias As be_SolicitudFotocopias, _
                                                    ByVal objDetalle As DataTable, _
                                                    ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SolicitudFotocopias.FUN_INS_SolicitudFotocopias(objSolicitudFotocopias, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_SolicitudFotocopias(ByVal objSolicitudFotocopias As be_SolicitudFotocopias, _
                                                    ByVal objDetalle As DataTable, _
                                                    ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SolicitudFotocopias.FUN_UPD_SolicitudFotocopias(objSolicitudFotocopias, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_SolicitudFotocopias(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SolicitudFotocopias.FUN_DEL_SolicitudFotocopias(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_ENV_SolicitudFotocopias(ByVal int_Codigo As Integer, ByVal int_CodigoEstado As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SolicitudFotocopias.FUN_ENV_SolicitudFotocopias(int_Codigo, int_CodigoEstado, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_SolicitudFotocopiasEstadoDetalle(ByVal objSolicitudFotocopias As be_SolicitudFotocopias, _
                                             ByVal objDetalle As DataTable, _
                                             ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_SolicitudFotocopias.FUN_UPD_SolicitudFotocopiasEstadoDetalle(objSolicitudFotocopias, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SolicitudFotocopias(ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_SolicitudFotocopias.FUN_LIS_SolicitudFotocopias(dt_FechaInicio, dt_FechaFin, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_SolicitudFotocopias(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_SolicitudFotocopias.FUN_GET_SolicitudFotocopias(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_SolicitudFotocopiasValidacionImpresion(ByVal int_CodigoEstadoProceso As Integer, ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_SolicitudFotocopias.FUN_LIS_SolicitudFotocopiasValidacionImpresion(int_CodigoEstadoProceso, dt_FechaInicio, dt_FechaFin, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace