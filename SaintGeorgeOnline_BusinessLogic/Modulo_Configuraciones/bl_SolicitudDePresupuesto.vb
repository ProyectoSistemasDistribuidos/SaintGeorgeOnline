Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess

Namespace ModuloConfiguraciones

    Public Class bl_SolicitudDePresupuesto

#Region "Atributos"

        Private obj_da_SolicitudDePresupuesto As da_SolicitudDePresupuesto

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_SolicitudDePresupuesto = New da_SolicitudDePresupuesto
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_SolicitudDePresupuesto(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_INS_SolicitudDePresupuesto(int_CodigoAsignacionSSSCentroCosto, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_EstadoSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, _
          ByRef str_Mensaje As String, ByRef strNombrePresupuesto As String, ByRef str_nombrePersona As String, ByRef nombreArea As String, ByRef nombreRemitente As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_EstadoSolicitudPresupuesto(int_CodigoSolicitudPresupuesto, str_Mensaje, strNombrePresupuesto, str_nombrePersona, nombreArea, nombreRemitente, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_ValidacionSolicitudDePresupuesto(ByVal objDetalle As DataTable, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_ValidacionSolicitudDePresupuesto(objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_CorreccionSolicitudDePresupuesto(ByVal objDetalle As DataTable, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_CorreccionSolicitudDePresupuesto(objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_EstadoValidacionSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, _
               ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_EstadoValidacionSolicitudPresupuesto(int_CodigoSolicitudPresupuesto, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_SolicitudDePresupuestoGerencia(ByVal objDetalle As DataTable, ByRef str_Mensaje As String, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_SolicitudDePresupuestoGerencia(objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_ValidacionReasignacionSolicitudDePresupuesto( _
           ByVal int_CodigoSolicitudPresupuesto As Integer, _
           ByVal int_CodigoPresupuesto As Integer, _
           ByVal int_CodigoTrabajadorReasignador As Integer, _
           ByVal int_TipoReasignacion As Integer, _
           ByVal objDetalle As DataSet, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_ValidacionReasignacionSolicitudDePresupuesto( _
                int_CodigoSolicitudPresupuesto, int_CodigoPresupuesto, int_CodigoTrabajadorReasignador, int_TipoReasignacion, _
                objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_EstadoValidacionSolicitudPresupuestoJefes( _
                  ByVal int_CodigoSolicitudPresupuesto As Integer, _
                  ByVal int_CodigoTrabajador As Integer, _
                  ByRef int_EnviarEmail As Integer, _
                  ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_EstadoValidacionSolicitudPresupuestoJefes(int_CodigoSolicitudPresupuesto, int_CodigoTrabajador, int_EnviarEmail, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_EstadoSolicitudPresupuestoGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_EstadoSolicitudPresupuestoGerencia(int_CodigoSolicitudPresupuesto, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_AprobarSolicitudPresupuestoGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_SolicitudDePresupuesto.FUN_UPD_AprobarSolicitudPresupuestoGerencia(int_CodigoSolicitudPresupuesto, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SolicitudPresupuestoYEstructuraSSSCentroCostoClases(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_LIS_SolicitudPresupuestoYEstructuraSSSCentroCostoClases(int_CodigoAsignacionSSSCentroCosto, int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoAValidar(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_LIS_SolicitudPresupuestoAValidar(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_SolicitudPresupuestoYEstructuraSSSCentroCostoClases(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_REP_SolicitudPresupuestoYEstructuraSSSCentroCostoClases(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_VAL_EnvioSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_VAL_EnvioSolicitudPresupuesto(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoACorregir(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_LIS_SolicitudPresupuestoACorregir(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_VAL_EnvioCorreccionSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_VAL_EnvioCorreccionSolicitudPresupuesto(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_VAL_AprobacionSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_VAL_AprobacionSolicitudPresupuesto(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoAValidarGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_LIS_SolicitudPresupuestoAValidarGerencia(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoAValidarSistemas(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_LIS_SolicitudPresupuestoAValidarSistemas(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoYReasignacionEstructuraSSSCentroCostoClasesSistemas(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_LIS_SolicitudPresupuestoYReasignacionEstructuraSSSCentroCostoClasesSistemas(int_CodigoAsignacionSSSCentroCosto, int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_VAL_AprobacionSolicitudPresupuestoJefes(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_VAL_AprobacionSolicitudPresupuestoJefes(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DetalleSolicitudPresupuestoObservacionesGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_LIS_DetalleSolicitudPresupuestoObservacionesGerencia(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_VAL_AprobacionSolicitudPresupuestoGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_VAL_AprobacionSolicitudPresupuestoGerencia(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DetalleSolicitudPresupuestoAprobadoGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_LIS_DetalleSolicitudPresupuestoAprobadoGerencia(int_CodigoSolicitudPresupuesto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_SolicitudPresupuestosGerencia(ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_SolicitudDePresupuesto.FUN_REP_SolicitudPresupuestosGerencia(int_CodigoPeriodo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        ''' <summary>
        '''   funcion que  sirven para actualizar el estado del presupuesto
        ''' </summary>
        ''' <param name="ASSSCC_CodigoAsignacionSSSCentroCosto"></param>
        ''' <param name="EP_CodigoEstadoPresupuesto"></param>
        ''' <param name="mensaje"></param>
        ''' <param name="nombrePresupuesto"></param>
        ''' <param name="nombrePersona"></param>
        ''' <param name="nombreArea"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function fValidarPresupuestoSiguienteValidador(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal EP_CodigoEstadoPresupuesto As Integer, _
                                                       ByRef mensaje As String, ByRef nombrePresupuesto As String, ByRef nombrePersona As String, ByRef nombreArea As String, ByRef ARVP_CodigoResponsableValidarPresupuesto As Integer) As Integer
            Try
                Return New da_SolicitudPresupuesto().fValidarPresupuestoSiguienteValidador(ASSSCC_CodigoAsignacionSSSCentroCosto, EP_CodigoEstadoPresupuesto, mensaje, nombrePresupuesto, nombrePersona, nombreArea, ARVP_CodigoResponsableValidarPresupuesto)


            Catch ex As Exception

            End Try

        End Function
        ''' <summary>
        ''' funcion para listar todos los clases y categorias y subcategorias 
        ''' </summary>
        ''' <param name="SP_CodigoSolicitudPresupuesto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function fListarClaseCategoriaSubcategoriaObservadosPresupuesto(ByVal SP_CodigoSolicitudPresupuesto As Integer, ByVal ARVP_CodigoResponsableValidarPresupuesto As Integer) As DataTable


            Try
                Return New da_SolicitudPresupuesto().fListarClaseCategoriaSubcategoriaObservadosPresupuesto(SP_CodigoSolicitudPresupuesto, ARVP_CodigoResponsableValidarPresupuesto)

            Catch ex As Exception

            End Try
        End Function



#End Region

    End Class

End Namespace

