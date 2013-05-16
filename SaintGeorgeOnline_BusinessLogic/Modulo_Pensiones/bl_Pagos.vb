Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_Pagos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Pagos As da_Pagos

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
            obj_da_Pagos = New da_Pagos
        End Sub

#End Region

#Region "Metodos Transacciones"

        'Pagos por Descarga de Banco
        Public Function FUN_INS_PagosPorDescargaBanco(ByVal objPagos As be_Pagos, ByVal dt_FechaVencimiento As Date, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByRef str_DescTalonario As String, ByRef str_DocumentoReferencia As String, ByRef str_DescripcionDeuda As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_INS_PagosPorDescargaBanco(objPagos, dt_FechaVencimiento, str_Mensaje, int_CodigoPago, int_CodigoTalonario, str_NumeroPago, str_DescTalonario, str_DocumentoReferencia, str_DescripcionDeuda, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update
        Public Function FUN_INS_PagosAnualesPorDescargaBanco(ByVal objPagos As be_Pagos, ByVal dt_FechaVencimiento As Date, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByRef str_DescTalonario As String, ByRef str_DocumentoReferencia As String, ByRef str_DescripcionDeuda As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_INS_PagosAnualesPorDescargaBanco(objPagos, dt_FechaVencimiento, str_Mensaje, int_CodigoPago, int_CodigoTalonario, str_NumeroPago, str_DescTalonario, str_DocumentoReferencia, str_DescripcionDeuda, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Pagos por Caja 
        Public Function FUN_INS_PagosPorCajaSinDetalle(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
           ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_INS_PagosPorCajaSinDetalle(objPagos, int_CodigoDeuda, str_Mensaje, int_CodigoPago, int_CodigoTalonario, str_NumeroPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_PagosPorCajaSinDetalleConMora(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_INS_PagosPorCajaSinDetalleConMora(objPagos, int_CodigoDeuda, str_Mensaje, int_CodigoPago, int_CodigoTalonario, str_NumeroPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_PagosPorCajaSinDetalleConMora(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_UPD_PagosPorCajaSinDetalleConMora(objPagos, int_CodigoDeuda, str_Mensaje, int_CodigoPago, int_CodigoTalonario, str_NumeroPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_PagosPorCaja(ByVal objPagos As be_Pagos, ByVal objDetalle As DataSet, ByRef str_ValorNumeroPago As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_INS_PagosPorCaja(objPagos, objDetalle, str_ValorNumeroPago, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 06/08/2012
        Public Function FUN_INS_PagosPorCajaSinDetalleDeudasAux(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
           ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_INS_PagosPorCajaSinDetalleDeudasAux(objPagos, int_CodigoDeuda, str_Mensaje, int_CodigoPago, int_CodigoTalonario, str_NumeroPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Pagos - Deudas vencidas
        Public Function FUN_INS_PagosDeudasVencidas(ByVal int_CodigoDeuda As Integer, ByVal objPagos As be_Pagos, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_INS_PagosDeudasVencidas(int_CodigoDeuda, objPagos, str_Mensaje, int_CodigoPago, int_CodigoTalonario, str_NumeroPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_PagosDeudasVencidas(ByVal objPagos As be_Pagos, ByVal int_CodigoDeuda As Integer, _
            ByRef str_Mensaje As String, ByRef int_CodigoPago As Integer, ByRef int_CodigoTalonario As Integer, ByRef str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_UPD_PagosDeudasVencidas(objPagos, int_CodigoDeuda, str_Mensaje, int_CodigoPago, int_CodigoTalonario, str_NumeroPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Anular Pagos
        Public Function FUN_UPD_PagosAnular(ByVal objPagos As be_Pagos, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_UPD_PagosAnular(objPagos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Eliminar Pagos
        Public Function FUN_UPD_PagosEliminar(ByVal objPagos As be_Pagos, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_UPD_PagosEliminar(objPagos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Pagos de Ingresos Varios
        Public Function FUN_INS_PagosIngresosVarios(ByVal objPagos As be_Pagos, ByVal objDetalle As DataSet, _
            ByRef bool_Admision As Boolean, ByRef de_montoAdmision As Decimal, ByRef str_ValorNumeroPago As String, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_INS_PagosIngresosVarios(objPagos, objDetalle, bool_Admision, de_montoAdmision, str_ValorNumeroPago, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#Region "Interfaz Admision"

        Public Function FUN_INS_PagosIngresosAdmision(ByVal str_NumDocumento As String, ByVal int_TipoDocumento As Integer, _
                                                  ByVal str_IdPostulante As String, ByVal dt_FechaRegistro As Date, _
                                                  ByVal int_Moneda As Integer, ByVal de_Monto As Decimal, ByRef str_Mensaje As String) As Integer

            Return obj_da_Pagos.FUN_INS_PagosIngresosAdmision(str_NumDocumento, int_TipoDocumento, str_IdPostulante, dt_FechaRegistro, int_Moneda, de_Monto, str_Mensaje)

        End Function

        Public Function FUN_UPD_EstadoPostulanteAdmision(ByVal int_CodigoPostulante As Integer, _
                                                 ByVal int_EstadoSubProceso As Integer, _
                                                 ByRef str_Mensaje As String, _
                                                 ByVal int_OrdenPagoAdmision As Integer, _
                                                 ByVal str_DocumentoPagoAdmision As String, _
                                                 ByVal de_MontoPagoAdmision As Decimal) As Integer

            Return obj_da_Pagos.FUN_UPD_EstadoPostulanteAdmision(int_CodigoPostulante, int_EstadoSubProceso, str_Mensaje, _
                                                                 int_OrdenPagoAdmision, str_DocumentoPagoAdmision, de_MontoPagoAdmision)

        End Function

#End Region


        'Actualizacion de Datos en Pagos Registrados
        Public Function FUN_UPD_PagosRegistrados(ByVal objPagos As be_Pagos, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_UPD_PagosRegistrados(objPagos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#Region "Impresion"

        Public Function FUN_UPD_PagosEstadoEmision(ByVal int_CodigoDocumento As Integer, ByVal int_CodigoTalonario As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_UPD_PagosEstadoEmision(int_CodigoDocumento, int_CodigoTalonario, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_PagosEstadoEmision(ByVal str_CodigoDocumento As String, ByVal int_CodigoTalonario As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Pagos.FUN_UPD_PagosEstadoEmision(str_CodigoDocumento, int_CodigoTalonario, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region


#Region "Importacion Pagos"

        Public Function fun_ins_exportacionpagos(ByVal objdetalle As DataSet, ByRef str_mensaje As String, _
            ByVal int_codigousuario As Integer, ByVal int_codigotipousuario As Integer, ByVal int_codigomodulo As Integer, ByVal int_codigoopcion As Integer) As List(Of String)

            Return obj_da_Pagos.fun_ins_exportacionpagos(objdetalle, str_mensaje, int_codigousuario, int_codigotipousuario, int_codigomodulo, int_codigoopcion)

        End Function

#End Region

#End Region

#Region "Metodos No Transaccionales"

#Region "Impresion"

        'Public Function FUN_LIS_PagosImprimir(ByVal int_CodigoTalonario As Integer, ByVal int_TipoConsulta As Integer, ByVal str_NumeroPagoInicial As String, ByVal str_NumeroPagoFinal As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Return obj_da_Pagos.FUN_LIS_PagosImprimir(int_CodigoTalonario, int_TipoConsulta, str_NumeroPagoInicial, str_NumeroPagoFinal, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

        ' Lista de busqueda
        Public Function FUN_LIS_PagosModuloImpresion(ByVal int_EstadoEmision As Integer, ByVal int_CodigoTalonario As Integer, _
                                                     ByVal str_NumeroPagoInicial As String, ByVal str_NumeroPagoFinal As String, _
                                                     ByVal int_TipoFecha As Integer, ByVal dt_FechaInicial As Date, ByVal dt_FechaFinal As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosModuloImpresion(int_EstadoEmision, int_CodigoTalonario, str_NumeroPagoInicial, str_NumeroPagoFinal, int_TipoFecha, dt_FechaInicial, dt_FechaFinal, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Public Function FUN_GET_PagoModuloImpresion(ByVal int_CodigoDocumento As Integer, ByVal int_CodigoTalonario As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Return obj_da_Pagos.FUN_GET_PagoModuloImpresion(int_CodigoDocumento, int_CodigoTalonario, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

        'Impresion Factura
        'Public Function FUN_GET_PagoModuloImpresionFactura(ByVal int_CodigoDocumento As Integer, ByVal int_CodigoTalonario As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Return obj_da_Pagos.FUN_GET_PagoModuloImpresionFactura(int_CodigoDocumento, int_CodigoTalonario, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

        'Impresion Factura
        Public Function FUN_LIS_PagosModuloImpresionVarios(ByVal str_CodigoDocumento As String, ByVal int_CodigoTalonario As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosModuloImpresionVarios(str_CodigoDocumento, int_CodigoTalonario, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

        Public Function FUN_LIS_Pagos(ByVal str_Nombre As String, ByVal int_CodigoTalonario As Integer, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicial As String, ByVal dt_FechaFinal As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_Pagos(str_Nombre, int_CodigoTalonario, int_TipoFecha, dt_FechaInicial, dt_FechaFinal, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_Pagos(ByVal str_Nombre As String, _
            ByVal int_CodigoTalonario As Integer, ByVal str_NumeroRangoInicial As String, ByVal str_NumeroRangoFinal As String, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicial As String, ByVal dt_FechaFinal As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_Pagos(str_Nombre, int_CodigoTalonario, str_NumeroRangoInicial, str_NumeroRangoFinal, int_TipoFecha, dt_FechaInicial, dt_FechaFinal, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PagosParaNotaCredito(ByVal int_TipoNota As Integer, _
                                                     ByVal int_CodigoTalonario As Integer, _
                                                     ByVal str_NumeroPago As String, _
                                                     ByVal int_CodigoTalonarioNota As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosParaNotaCredito(int_TipoNota, int_CodigoTalonario, str_NumeroPago, int_CodigoTalonarioNota, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PagosParaNotaDebito(ByVal int_TipoNota As Integer, ByVal int_CodigoTalonario As Integer, ByVal str_NumeroPago As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosParaNotaDebito(int_TipoNota, int_CodigoTalonario, str_NumeroPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_PagosGeneral(ByVal int_CodigoTalonario As Integer, ByVal str_NumeroPago As String, _
            ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombres As String, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosGeneral(int_CodigoTalonario, str_NumeroPago, _
                str_ApellidoPaterno, str_ApellidoMaterno, str_Nombres, _
                int_TipoFecha, dt_FechaInicio, dt_FechaFin, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PagosGeneralParaNotas(ByVal int_CodigoConceptoAux As Integer, ByVal int_CodigoTalonario As Integer, ByVal str_NumeroPago As String, _
            ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombres As String, _
            ByVal int_TipoFecha As Integer, ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosGeneralParaNotas(int_CodigoConceptoAux, int_CodigoTalonario, str_NumeroPago, _
                str_ApellidoPaterno, str_ApellidoMaterno, str_Nombres, _
                int_TipoFecha, dt_FechaInicio, dt_FechaFin, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



        Public Function FUN_LIS_PagosEmitidosPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_Modulo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosEmitidosPorAlumno(str_CodigoAlumno, int_Modulo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_LIS_PagosEmitidosPorFamilia(ByVal str_CodigoFamilia As String, ByVal int_Modulo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosEmitidosPorFamilia(str_CodigoFamilia, int_Modulo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        Public Function FUN_LIS_PagosEmitidosPorFamiliaFiltroAlumno(ByVal str_CodigoAlumno As String, ByVal int_Modulo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosEmitidosPorFamiliaFiltroAlumno(str_CodigoAlumno, int_Modulo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function



        Public Function FUN_GET_Pago(ByVal int_CodigoPago As Integer, ByVal int_CodigoTalonario As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_GET_Pago(int_CodigoPago, int_CodigoTalonario, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PagosPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, ByVal int_TipoBusqueda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosPorAlumno(str_CodigoAlumno, int_CodigoAnioAcademico, int_TipoBusqueda, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_PagosPorAlumnoV2(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, ByVal int_TipoBusqueda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Pagos.FUN_LIS_PagosPorAlumnoV2(str_CodigoAlumno, int_CodigoAnioAcademico, int_TipoBusqueda, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#End Region

    End Class

End Namespace
