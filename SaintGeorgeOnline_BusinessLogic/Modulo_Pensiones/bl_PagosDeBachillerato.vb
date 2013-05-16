Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_PagosDeBachillerato

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_PagosDeBachillerato As da_PagosDeBachillerato
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

            obj_da_PagosDeBachillerato = New da_PagosDeBachillerato

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_UPD_PagosDeBachillerato(ByVal int_CodigoDeuda As String, _
                                                ByVal dt_FechaPago As Date, _
                                                ByVal de_MontoPago As Decimal, _
                                                ByVal dt_FechaEmision As Date, _
                                                ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_PagosDeBachillerato.FUN_UPD_PagosDeBachillerato( _
                int_CodigoDeuda, dt_FechaPago, de_MontoPago, dt_FechaEmision, str_Mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_InformacionPagosBachillerato(ByVal str_CodigoAlumno As String, _
                                                             ByVal int_CodigoMoneda As Integer, _
                                                             ByVal dt_FechaVencimiento As Date, _
                                                             ByVal de_Monto As Decimal, _
                                                             ByVal str_descripcion As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_PagosDeBachillerato.FUN_LIS_InformacionPagosBachillerato( _
                str_CodigoAlumno, int_CodigoMoneda, dt_FechaVencimiento, de_Monto, str_descripcion, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_DinamicoInformacionPagosBachillerato( _
            ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_PagosDeBachillerato.FUN_REP_DinamicoInformacionPagosBachillerato(int_CodigoPeriodoAcademico, _
               int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace