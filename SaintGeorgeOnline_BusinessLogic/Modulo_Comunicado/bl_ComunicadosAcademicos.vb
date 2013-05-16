Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloComunicado
Imports SaintGeorgeOnline_DataAccess.ModuloComunicado

Namespace ModuloComunicado

    Public Class bl_ComunicadosAcademicos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_ComunicadosAcademicos As da_ComunicadosAcademicos

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
            obj_da_ComunicadosAcademicos = New da_ComunicadosAcademicos
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ComunicadosAcademicos(ByVal objComunicadosAcademicos As be_ComunicadosAcademicos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ComunicadosAcademicos.FUN_INS_ComunicadosAcademicos(objComunicadosAcademicos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_DocumentoComunicadosAcademicos(ByVal objComunicadosAcademicos As be_ComunicadosAcademicos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ComunicadosAcademicos.FUN_UPD_DocumentoComunicadosAcademicos(objComunicadosAcademicos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_ComunicadosAcademicos(ByVal objComunicadosAcademicos As be_ComunicadosAcademicos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ComunicadosAcademicos.FUN_UPD_ComunicadosAcademicos(objComunicadosAcademicos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_ComunicadosAcademicos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ComunicadosAcademicos.FUN_DEL_ComunicadosAcademicos(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_INS_ConfirmacionLecturaComunicado(ByVal int_CodigoComunicadoAcademico As Integer, ByRef int_CodigoUsuario As String, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_ComunicadosAcademicos.FUN_INS_ConfirmacionLecturaComunicado(int_CodigoComunicadoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ComunicadosAcademicos(ByVal str_Descripcion As String, ByVal int_CodigoTipoDocumento As Integer, ByVal dt_FechaRegistroIni As Date, ByVal dt_FechaRegistroFin As Date, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ComunicadosAcademicos.FUN_LIS_ComunicadosAcademicos(str_Descripcion, int_CodigoTipoDocumento, dt_FechaRegistroIni, dt_FechaRegistroFin, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_ConsultaComunicadosAcademicos(ByVal int_AnioAcademico As Integer, ByVal int_CodigoMes As Integer, ByVal int_CodigoTipoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ComunicadosAcademicos.FUN_LIS_ConsultaComunicadosAcademicos(int_AnioAcademico, int_CodigoMes, int_CodigoTipoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_ConsultaComunicadosAcademicosUltimos(ByVal int_AnioAcademico As Integer, ByVal int_CodigoTipoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ComunicadosAcademicos.FUN_LIS_ConsultaComunicadosAcademicosUltimos(int_AnioAcademico, int_CodigoTipoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_ComunicadosAcademicos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ComunicadosAcademicos.FUN_GET_ComunicadosAcademicos(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_ComunicadosAcademicosXTipos( _
            ByVal int_codUsuario As Integer, _
            ByVal int_codTipoUsuario As Integer, _
            ByVal int_codTipoDocumento As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ComunicadosAcademicos.FUN_LIS_ComunicadosAcademicosXTipos(int_codUsuario, int_codTipoUsuario, int_codTipoDocumento, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace

