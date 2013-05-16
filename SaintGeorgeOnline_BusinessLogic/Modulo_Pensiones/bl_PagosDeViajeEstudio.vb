Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_PagosDeViajeEstudio

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_PagosDeViajeEstudio As da_PagosDeViajeEstudio

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

            obj_da_PagosDeViajeEstudio = New da_PagosDeViajeEstudio

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_PagosDeViajeEstudio(ByVal str_CodigoAlumno As String, ByVal dt_FechaPago As Date, ByVal de_MontoPago As Decimal, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_PagosDeViajeEstudio.FUN_INS_PagosDeViajeEstudio(str_CodigoAlumno, dt_FechaPago, de_MontoPago, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_PagosDeViajeEstudioGeneral(ByVal int_CodigoDeuda As String, _
                                                 ByVal dt_FechaPago As Date, _
                                                 ByVal de_MontoPago As Decimal, _
                                                 ByVal dt_FechaEmision As Date, _
                                                 ByRef str_Mensaje As String, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_PagosDeViajeEstudio.FUN_UPD_PagosDeViajeEstudioGeneral( _
                int_CodigoDeuda, dt_FechaPago, de_MontoPago, dt_FechaEmision, str_Mensaje, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_InformacionPagosViajeAlumnos(ByVal str_CodigoAlumno As String, ByVal de_Monto As Decimal, ByVal dt_FechaPago As DateTime, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_PagosDeViajeEstudio.FUN_LIS_InformacionPagosViajeAlumnos(str_CodigoAlumno, de_Monto, dt_FechaPago, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_InformacionPagosViajesAlumnosGeneral(ByVal str_CodigoAlumno As String, _
                                                                     ByVal int_CodigoMoneda As Integer, _
                                                                     ByVal dt_FechaVencimiento As Date, _
                                                                     ByVal de_Monto As Decimal, _
                                                                     ByVal str_descripcion As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_PagosDeViajeEstudio.FUN_LIS_InformacionPagosViajesAlumnosGeneral(str_CodigoAlumno, int_CodigoMoneda, dt_FechaVencimiento, de_Monto, str_descripcion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_DinamicoInformacionPagosViajeAlumnos( _
              ByVal int_CodigoPeriodoAcademico As Integer, _
              ByVal dt_FechaRangoInicial As Date, _
              ByVal dt_FechaRangoFinal As Date, _
              ByVal int_CodigoNivel As Integer, _
              ByVal int_CodigoSubnivel As Integer, _
              ByVal int_CodigoGrado As Integer, _
              ByVal int_CodigoAula As Integer, _
              ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_PagosDeViajeEstudio.FUN_REP_DinamicoInformacionPagosViajeAlumnos( _
               int_CodigoPeriodoAcademico, dt_FechaRangoInicial, dt_FechaRangoFinal, _
               int_CodigoNivel, int_CodigoSubnivel, int_CodigoGrado, int_CodigoAula, _
               int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_REP_DinamicoInformacionPagosViajeGenerales(ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_PagosDeViajeEstudio.FUN_REP_DinamicoInformacionPagosViajeGenerales(int_CodigoPeriodoAcademico, _
             int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace