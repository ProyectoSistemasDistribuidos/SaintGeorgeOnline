Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloActividades
Imports SaintGeorgeOnline_DataAccess.ModuloActividades
'ok
Namespace ModuloActividades

    Public Class bl_ConfirmacionParticipantes

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_ConfirmacionParticipantes As da_ConfirmacionParticipantes

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
            obj_da_ConfirmacionParticipantes = New da_ConfirmacionParticipantes
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ConfirmacionParticipantes(ByVal dtLista As DataTable, ByRef str_Mensaje As String, _
         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ConfirmacionParticipantes.FUN_INS_ConfirmacionParticipantes(dtLista, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_INS_RegAsistentes(ByVal dtLista As DataTable, ByRef str_Mensaje As String, _
        ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ConfirmacionParticipantes.FUN_INS_RegAsistentes(dtLista, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ConfirmacionParticipantes(ByVal int_CodigoActividad As Integer, ByVal int_Tipo As Integer, ByVal str_Familia As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConfirmacionParticipantes.FUN_LIS_ConfirmacionParticipantes(int_CodigoActividad, int_Tipo, str_Familia, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_RegAsistentes(ByVal int_CodigoActividad As Integer, ByVal int_Tipo As Integer, ByVal str_Familia As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConfirmacionParticipantes.FUN_LIS_RegAsistentes(int_CodigoActividad, int_Tipo, str_Familia, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_Actividades(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConfirmacionParticipantes.FUN_LIS_Actividades(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_CantConfirmacionParticipantes(ByVal int_CodigoActividad As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConfirmacionParticipantes.FUN_LIS_CantConfirmacionParticipantes(int_CodigoActividad, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_CantRegAsistentes(ByVal int_CodigoActividad As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConfirmacionParticipantes.FUN_LIS_CantRegAsistentes(int_CodigoActividad, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_REP_ConfirmacionParticipantes(ByVal int_CodigoActividad As Integer, ByVal int_Tipo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_ConfirmacionParticipantes.FUN_REP_ConfirmacionParticipantes(int_CodigoActividad, int_Tipo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace