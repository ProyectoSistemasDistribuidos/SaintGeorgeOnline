Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_CalendarioPagos

        'update

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_CalendarioPagos As da_CalendarioPagos
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
            obj_da_CalendarioPagos = New da_CalendarioPagos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_CalendarioPagos(ByVal objCalendarioPagos As be_CalendarioPagos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CalendarioPagos.FUN_INS_CalendarioPagos(objCalendarioPagos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_CalendarioPagos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CalendarioPagos.FUN_DEL_CalendarioPagos(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_ACT_CalendarioPagos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CalendarioPagos.FUN_ACT_CalendarioPagos(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_INS_CalendarioPagoAnual(ByVal objCalendarioPagos As be_CalendarioPagos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_CalendarioPagos.FUN_INS_CalendarioPagoAnual(objCalendarioPagos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_CalendarioPagos(ByVal int_CodigoAnioAcademico As Integer, _
                                                ByVal int_Mes As Integer, _
                                                ByVal int_CodigoConceptoCobro As Integer, _
                                                ByVal int_CodigoGrado As Integer, _
                                                ByVal int_CodigoMoneda As Integer, _
                                                ByVal int_Estado As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_CalendarioPagos.FUN_LIS_CalendarioPagos( _
                int_CodigoAnioAcademico, int_Mes, int_CodigoConceptoCobro, int_CodigoGrado, int_CodigoMoneda, int_Estado, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_CalendarioPagosFechasVencimiento(ByVal int_CodigoAnioAcademico As Integer, ByVal int_Estado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_CalendarioPagos.FUN_LIS_CalendarioPagosFechasVencimiento( _
               int_CodigoAnioAcademico, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_CronogramaPagos(ByVal int_CodigoAlumno As Integer, _
                                                ByVal int_CodigoAnioAcademico As Integer, _
                                                ByVal int_CodigoUsuario As Integer, _
                                                ByVal int_CodigoTipoUsuario As Integer, _
                                                ByVal int_CodigoModulo As Integer, _
                                                ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_CalendarioPagos.FUN_LIS_CronogramaPagos( _
                   int_CodigoAlumno, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_CalendarioPagosAnuales(ByVal int_CodigoAnioAcademico As Integer, _
                 ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_CalendarioPagos.FUN_LIS_CalendarioPagosAnuales( _
                int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class


End Namespace
