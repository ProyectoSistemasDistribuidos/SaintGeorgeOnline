Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloEntrevistas
Imports SaintGeorgeOnline_BusinessEntities.ModuloEntrevistas

Namespace ModuloEntrevistas

    Public Class bl_ProgramacionEntrevistas

#Region "Atributos"
        Private str_Mensaje As String
        Private obj_da_ProgramacionEntrevistas As da_ProgramacionEntrevistas
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
            obj_da_ProgramacionEntrevistas = New da_ProgramacionEntrevistas
        End Sub
#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ProgramacionEntrevistas( _
            ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal lsDetalle As List(Of be_ProgramacionEntrevistaDetalle), ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, _
            ByVal lstEliminar As List(Of be_ProgramacionEntrevistaDetalle), ByVal bool_EnviarEmail As Boolean) As Integer

            Return obj_da_ProgramacionEntrevistas.FUN_INS_ProgramacionEntrevistas(obe_ProgramacionEntrevistaCabecera, lsDetalle, str_mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, lstEliminar, bool_EnviarEmail)

        End Function

        Public Function F_ElminarEntrevista(ByVal codProg As Integer) As Integer
            Try
                Return obj_da_ProgramacionEntrevistas.F_ElminarEntrevista(codProg)
            Catch ex As Exception
            End Try
        End Function

        Public Function F_AactualizarListaEntevista(ByVal lstEntrevista As List(Of Object)) As Integer
            Try
                Return obj_da_ProgramacionEntrevistas.F_AactualizarListaEntevista(lstEntrevista)
            Catch ex As Exception

            End Try
        End Function

        Public Function FUN_UPD_ProgramacionEntrevistasWeb( _
           ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, ByRef str_mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionEntrevistas.FUN_UPD_ProgramacionEntrevistasWeb(obe_ProgramacionEntrevistaCabecera, str_mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_UPD_ProgramacionEntrevistasFicha( _
            ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal lstDetalleParticipantesEliminar As List(Of be_ProgramacionEntrevistaDetalle), _
            ByVal lstDetalleParticipantes As List(Of be_ProgramacionEntrevistaDetalle), _
            ByVal lstDetalleAcuerdos As List(Of be_ProgramacionEntrevistaDetalleAcuerdos), _
            ByVal lstDetalleMotivosEliminar As List(Of be_ProgramacionEntrevistaDetalleMotivo), _
            ByVal lstDetalleMotivos As List(Of be_ProgramacionEntrevistaDetalleMotivo), _
            ByVal lstDetalleAsistentesEliminar As List(Of be_ProgramacionEntrevistaDetalleAsistentes), _
            ByVal lstDetalleAsistentes As List(Of be_ProgramacionEntrevistaDetalleAsistentes), _
            ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionEntrevistas.FUN_UPD_ProgramacionEntrevistasFicha( _
                obe_ProgramacionEntrevistaCabecera, _
                lstDetalleParticipantesEliminar, lstDetalleParticipantes, _
                lstDetalleAcuerdos, _
                lstDetalleMotivosEliminar, lstDetalleMotivos, _
                lstDetalleAsistentesEliminar, lstDetalleAsistentes, _
                str_mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_ProgramacionEntrevistasFicha( _
            ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByRef str_mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ProgramacionEntrevistas.FUN_DEL_ProgramacionEntrevistasFicha( _
                obe_ProgramacionEntrevistaCabecera, str_mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transacciones"

        Public Function FUN_LIS_ProgramacionEntrevistas( _
            ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoMes As Integer, ByVal int_CodigoGrado As Integer, ByVal int_EstadoEntrevista As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal RPE_CodigoProgramacionEntrevista As Integer) As DataSet

            Return obj_da_ProgramacionEntrevistas.FUN_LIS_ProgramacionEntrevistas(int_CodigoPeriodo, int_CodigoMes, int_CodigoGrado, int_EstadoEntrevista, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion, RPE_CodigoProgramacionEntrevista)

        End Function

        Public Function FUN_GET_ProgramacionEntrevistasWeb(ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionEntrevistas.FUN_GET_ProgramacionEntrevistasWeb(obe_ProgramacionEntrevistaCabecera, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ProgramacionEntrevistasSinFichaPorTrabajador( _
            ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionEntrevistas.FUN_LIS_ProgramacionEntrevistasSinFichaPorTrabajador(obe_ProgramacionEntrevistaCabecera, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_ProgramacionEntrevistas(ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionEntrevistas.FUN_GET_ProgramacionEntrevistas(obe_ProgramacionEntrevistaCabecera, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ProgramacionEntrevistasConFichaPorTrabajador( _
        ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoMes As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoTrabajadorEntrevistador As Integer, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionEntrevistas.FUN_LIS_ProgramacionEntrevistasConFichaPorTrabajador(int_CodigoPeriodo, int_CodigoMes, int_CodigoGrado, int_CodigoTrabajadorEntrevistador, _
                                                                                                       int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_ProgramacionEntrevistasConFicha(ByVal obe_ProgramacionEntrevistaCabecera As be_ProgramacionEntrevistaCabecera, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ProgramacionEntrevistas.FUN_GET_ProgramacionEntrevistasConFicha(obe_ProgramacionEntrevistaCabecera, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
