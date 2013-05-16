Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_Letras

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Letras As da_Letras

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
            obj_da_Letras = New da_Letras
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_Letras(ByVal objLetras As be_Letras, ByVal objDetalle As DataSet, _
            ByVal obj_be_NotaCreditoDebito As be_NotaCreditoDebito, ByVal de_MontoDebito As Decimal, _
            ByRef str_ValorNumeroOperacion As String, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Letras.FUN_INS_Letras(objLetras, objDetalle, obj_be_NotaCreditoDebito, de_MontoDebito, str_ValorNumeroOperacion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_LetrasPago( _
           ByVal int_CodigoLetra As Integer, ByVal int_CodigoDetalleLetra As Integer, _
           ByVal str_CodigoAlumno As String, ByVal int_CodigoDeuda As Integer, _
           ByVal dt_FechaPagoLetra As Date, _
           ByVal int_OrigenPagoLetra As Integer, ByVal int_CodigoCuentaBancaria As Integer, ByVal int_FormaPagoLetra As Integer, _
           ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Letras.FUN_UPD_LetrasPago(int_CodigoLetra, int_CodigoDetalleLetra, str_CodigoAlumno, int_CodigoDeuda, _
                                                    dt_FechaPagoLetra, _
                                                    int_OrigenPagoLetra, int_CodigoCuentaBancaria, int_FormaPagoLetra, _
                                                    str_Mensaje, _
                                                    int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        'Public Function FUN_UPD_LetrasPago( _
        '    ByVal int_CodigoLetra As Integer, ByVal int_CodigoDetalleLetra As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoDeuda As Integer, ByVal dt_FechaPagoLetra As Date, ByVal int_OrigenPagoLetra As Integer, ByRef str_Mensaje As String, _
        '    ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

        '    Return obj_da_Letras.FUN_UPD_LetrasPago(int_CodigoLetra, int_CodigoDetalleLetra, str_CodigoAlumno, int_CodigoDeuda, dt_FechaPagoLetra, int_OrigenPagoLetra, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

        'Eliminar Pagos
        Public Function FUN_UPD_LetrasEliminar(ByVal int_CodigoDetalleLetra As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoTalonario As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Letras.FUN_UPD_LetrasEliminar(int_CodigoDetalleLetra, str_CodigoAlumno, int_CodigoTalonario, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Suspender Pagos
        Public Function FUN_UPD_LetrasSuspender(ByVal int_CodigoDetalleLetra As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoTalonario As Integer, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Letras.FUN_UPD_LetrasSuspender(int_CodigoDetalleLetra, str_CodigoAlumno, int_CodigoTalonario, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_LetrasPorCodigo(ByVal int_CodigoOperacion As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Letras.FUN_LIS_LetrasPorCodigo(int_CodigoOperacion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_Letras(ByVal str_Nombre As String, ByVal int_CodigoTalonario As Integer, _
           ByVal int_TipoFecha As Integer, ByVal dt_FechaInicial As String, ByVal dt_FechaFinal As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Letras.FUN_LIS_Letras(str_Nombre, int_CodigoTalonario, int_TipoFecha, dt_FechaInicial, dt_FechaFinal, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_OperacionLetras(ByVal str_Nombre As String, ByVal dt_FechaInicial As String, ByVal dt_FechaFinal As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Letras.FUN_LIS_OperacionLetras(str_Nombre, dt_FechaInicial, dt_FechaFinal, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_OperacionLetras(ByVal int_CodigoLetra As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Letras.FUN_GET_OperacionLetras(int_CodigoLetra, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

