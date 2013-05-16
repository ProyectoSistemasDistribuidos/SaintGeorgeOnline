Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos
    Public Class bl_BloqueoFichasAtenciones

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_BloqueosFichasAtenciones As da_BloqueoFichasAtenciones

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
            obj_da_BloqueosFichasAtenciones = New da_BloqueoFichasAtenciones
        End Sub

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Excepciones(ByVal int_Vigente As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_BloqueosFichasAtenciones.FUN_LIS_Excepciones(int_Vigente, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_BloqueosGenerales(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_BloqueosFichasAtenciones.FUN_LIS_BloqueosGenerales(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_VAL_CodigoFichaExcepciones(ByVal int_CodigoFicha As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_BloqueosFichasAtenciones.FUN_VAL_CodigoFichaExcepciones(int_CodigoFicha, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

#Region "Metodos Transaccionales"
        Public Function FUN_INS_BloqueoGeneral(ByVal objBloqueoFichaAtencion As be_BloqueoFichasAtenciones, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_BloqueosFichasAtenciones.FUN_INS_BloqueoGeneral(objBloqueoFichaAtencion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_INS_DtalleExcepciones(ByVal objDetalle As DataTable, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_BloqueosFichasAtenciones.FUN_INS_DetalleExcepciones(objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

    End Class
End Namespace

