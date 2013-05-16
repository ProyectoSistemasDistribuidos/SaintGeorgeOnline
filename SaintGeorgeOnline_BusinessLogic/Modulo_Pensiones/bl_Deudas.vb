Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_Deudas

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Deudas As da_Deudas

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
            obj_da_Deudas = New da_Deudas
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_Deudas(ByVal objDeudas As be_Deudas, ByVal int_CodigoPostulante As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Deudas.FUN_INS_Deudas(objDeudas, int_CodigoPostulante, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_Deudas(ByVal objDeudas As be_Deudas, ByRef str_Mensaje As String, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Deudas.FUN_UPD_Deudas(objDeudas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_Deudas(ByVal objDeudas As be_Deudas, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Deudas.FUN_DEL_Deudas(objDeudas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_ListaDeudas(ByVal str_CodigoDeudas As String, ByVal str_CodigoAlumno As String, ByRef str_Mensaje As String, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Deudas.FUN_DEL_ListaDeudas(str_CodigoDeudas, str_CodigoAlumno, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_DeudasDeCalendarioPorAlumno(ByVal objDetalle As DataSet, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Deudas.FUN_INS_DeudasDeCalendarioPorAlumno(objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_INS_DeudasPorServicio(ByVal objDetalle As DataSet, ByRef str_Mensaje As String, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Deudas.FUN_INS_DeudasPorServicio(objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


#Region "Bonos"

        Public Function fun_ins_exportacionbonos(ByVal objdetalle As DataTable, ByRef str_mensaje As String, _
            ByVal int_codigousuario As Integer, ByVal int_codigotipousuario As Integer, ByVal int_codigomodulo As Integer, ByVal int_codigoopcion As Integer) As List(Of String)

            Return obj_da_Deudas.fun_ins_exportacionbonos(objdetalle, str_mensaje, int_codigousuario, int_codigotipousuario, int_codigomodulo, int_codigoopcion)

        End Function

#End Region

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_DeudasAlumnos(ByVal int_CodigoBanco As Integer, ByVal int_CodigoMoneda As Integer, ByVal int_CodigoTipo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_DeudasAlumnos(int_CodigoBanco, int_CodigoMoneda, int_CodigoTipo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DeudasAlumnosPorServicio(ByVal int_CodigoServicio As Integer, _
                                                         ByVal int_CodigoBanco As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_DeudasAlumnosPorServicio(int_CodigoServicio, int_CodigoBanco, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 07/08/2012
        Public Function FUN_LIS_DeudasAlumnosPorServicioGenerales( _
           ByVal int_CodigoServicio As Integer, _
           ByVal int_CodigoBanco As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_DeudasAlumnosPorServicioGenerales(int_CodigoServicio, int_CodigoBanco, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'update 13/08/2012
        Public Function FUN_LIS_DeudasAlumnosPorServicioGeneralesCuotas( _
            ByVal int_CodigoServicio As Integer, _
            ByVal int_CodigoBanco As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_DeudasAlumnosPorServicioGeneralesCuotas(int_CodigoServicio, int_CodigoBanco, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_InformacionDeudasAlumnos(ByVal str_CodigoAlumno As String, ByVal str_FechaVencimiento As String, ByVal int_CodigoMoneda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_InformacionDeudasAlumnos(str_CodigoAlumno, str_FechaVencimiento, int_CodigoMoneda, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' update
        Public Function FUN_LIS_InformacionDeudasAnualesAlumnos(ByVal str_CodigoAlumno As String, ByVal str_FechaVencimiento As String, ByVal int_CodigoMoneda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_InformacionDeudasAnualesAlumnos(str_CodigoAlumno, str_FechaVencimiento, int_CodigoMoneda, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DeudasPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_DeudasPorAlumno(str_CodigoAlumno, int_CodigoPeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DeudasVencidas(ByVal int_CodigoTalonario As Integer, ByVal dt_FechaVencimiento As Date, ByVal int_CodigoConcepto As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_DeudasVencidas(int_CodigoTalonario, dt_FechaVencimiento, int_CodigoConcepto, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DeudasFechasVencimiento(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_DeudasFechasVencimiento(int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function


        Public Function FUN_LIS_GeneracionDeudas(ByVal str_ApellidoPaterno As String, ByVal int_CodigoAnioAcademico As Integer, ByVal int_TipoBusqueda As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_GeneracionDeudas(str_ApellidoPaterno, int_CodigoAnioAcademico, int_TipoBusqueda, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DeudasGeneradasPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoPeriodoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_LIS_DeudasGeneradasPorAlumno(str_CodigoAlumno, int_CodigoPeriodoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        ' updated 31/10/2012
        Public Function FUN_GET_Deuda(ByVal int_Codigo As Integer, ByVal int_TipoPagoAdmision As Integer, ByVal int_CodigoPagoAuxAdmision As Integer, _
                                      ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Deudas.FUN_GET_Deuda(int_Codigo, int_TipoPagoAdmision, int_CodigoPagoAuxAdmision, _
                                               int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

