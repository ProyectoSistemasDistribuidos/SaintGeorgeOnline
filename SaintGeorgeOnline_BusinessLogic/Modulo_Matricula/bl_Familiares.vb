Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula

    Public Class bl_Familiares

#Region "Atributos"

        Private str_Mensaje As String
        Private int_CodigoPersona As Integer
        Private int_CodigoOtros As Integer
        Private obj_da_Familiares As da_Familiares

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_Familiares = New da_Familiares
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_Familiares(ByVal bool_FichaCompleta As Boolean, _
                                           ByVal objFamiliares As be_Familiares, _
                                           ByVal objDetalle As DataSet, _
                                           ByRef str_Mensaje As String, ByRef int_CodigoPersona As Integer, ByRef int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Familiares.FUN_INS_Familiares(bool_FichaCompleta, objFamiliares, objDetalle, str_Mensaje, int_CodigoPersona, int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_Familiares(ByVal bool_FichaCompleta As Boolean, _
                                           ByVal objFamiliares As be_Familiares, _
                                           ByVal objDetalle As DataSet, _
                                           ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Familiares.FUN_UPD_Familiares(bool_FichaCompleta, objFamiliares, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_Familiares(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Familiares.FUN_DEL_Familiares(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Validacion de Datos
        Public Function FUN_UPD_FamiliaresActualizacion( _
            ByVal intCodigoFamiliar As Integer, ByVal intCodigoPersona As Integer, ByVal intCodigoSolicitud As Integer, ByVal intCodigoPerfil As Integer, _
            ByVal objDT_Cabecera As DataTable, _
            ByVal arrStrCodigossNacionalidad() As String, _
            ByVal arrStrCodigosIdioma() As String, _
            ByVal arrStrCodigosProfesion() As String, _
            ByVal arrStrCodigosAuto() As String, ByVal arrDescAuto() As String, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Familiares.FUN_UPD_FamiliaresActualizacion( _
                                    intCodigoFamiliar, intCodigoPersona, intCodigoSolicitud, intCodigoPerfil, objDT_Cabecera, _
                                    arrStrCodigossNacionalidad, arrStrCodigosIdioma, arrStrCodigosProfesion, arrStrCodigosAuto, arrDescAuto, _
                                    str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Registro de Fichas de Familiar Temporal
        Public Function FUN_INS_FamiliaresTemp(ByVal objSolicitud As be_SolicitudActualizacionFichaFamiliares, _
            ByVal objFamiliares As be_Familiares, _
            ByVal str_CadenaCodigoPerfil As String, _
            ByVal objDetalle As DataSet, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_Familiares.FUN_INS_FamiliaresTemp(objSolicitud, objFamiliares, str_CadenaCodigoPerfil, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Familiar(ByVal objMaestroPersona As be_MaestroPersonas, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_LIS_Familiar(objMaestroPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_Familiar(ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_GET_Familiar(int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_FamiliarPorPersona(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_GET_FamiliarPorPersona(int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Validacion de Datos
        Public Function FUN_GET_FamiliarActualizacion(ByVal int_Codigo As Integer, ByVal int_CodigoSolicitud As Integer, ByVal int_CodigoPerfil As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_GET_FamiliarActualizacion(int_Codigo, int_CodigoSolicitud, int_CodigoPerfil, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_FamiliarActualizacion(ByVal objMaestroPersona As be_MaestroPersonas, _
                ByVal dtInicial As Date, ByVal dtFinal As Date, ByVal intEstado As Integer, ByVal int_CodigoPerfil As Integer, _
                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_LIS_FamiliarActualizacion(objMaestroPersona, dtInicial, dtFinal, intEstado, int_CodigoPerfil, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Lista de Familiares por Familiar
        Public Function FUN_LIS_FamiliaresPorCodigoFamiliar(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_LIS_FamiliaresPorCodigoFamiliar(int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Datos para visualizacion del Familiar
        Public Function FUN_GET_FamiliarVisualizacionActualizacionFamiliar(ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_GET_FamiliarVisualizacionActualizacionFamiliar(int_CodigoFamiliar, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Modulo de - "DATOS DE FAMILIA"
        Public Function FUN_LIS_FamiliaresPorCodigoFamilia(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_LIS_FamiliaresPorCodigoFamilia(int_CodigoFamilia, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
        'Modulo  - "Enfermeria"
        Public Function FUN_LIS_FamiliaresPorAlumno(ByVal int_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Familiares.FUN_LIS_FamiliaresPorAlumno(int_CodigoAlumno, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function
#End Region

    End Class

End Namespace





