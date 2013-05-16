Imports SaintGeorgeOnline_DataAccess.ModuloConductaAlumnos
Namespace ModuloConductaAlumnos
    Public Class bl_CriteriosConducta

#Region "Atributos"
        Private str_Mensaje As String
        Private obj_da_CriterioConducta As da_CriteriosConducta
#End Region
#Region "Propiedades"
        Public ReadOnly Property Mensajea() As String
            Get
                Return str_Mensaje
            End Get
        End Property
#End Region
#Region "Constructor"
        Public Sub New()
            obj_da_CriterioConducta = New da_CriteriosConducta
        End Sub
#End Region
#Region "Metodos Transaccionales"
        Public Function FUN_DEL_CriteriosConducta(ByVal CodCritCond As Integer, ByRef str_Mensaje As String, _
                                                  ByVal CodigoUsuario As Integer, ByVal CodigoTipoUsuario As Integer, _
                                                  ByVal CodigoModulo As Integer, ByVal CodigoOpcion As Integer) As Integer
            Return obj_da_CriterioConducta.FUN_DEL_CriteriosConducta(CodCritCond, str_Mensaje, CodigoUsuario, _
                                                                     CodigoTipoUsuario, CodigoModulo, CodigoOpcion)
        End Function
        Public Function FUN_INS_CriterioConducta(ByVal str_Descripcion As String, ByVal int_CodTipoCritCond As Integer, ByVal int_Puntaje As Integer, _
                                                 ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
                                                 ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByRef str_Mensaje As String) As Integer
            Return obj_da_CriterioConducta.FUN_INS_CriterioConducta(str_Descripcion, int_CodTipoCritCond, int_Puntaje, int_CodigoUsuario, int_CodigoTipoUsuario, _
                                                                    int_CodigoModulo, int_CodigoOpcion, str_Mensaje)
        End Function
        Public Function FUN_UPD_CriteriosConducta(ByVal int_CodCritCond As Integer, ByVal str_Descripcion As String, _
                                                  ByVal int_CodTipoCritCond As Integer, ByVal int_Puntaje As Integer, ByVal int_CodigoUsuario As Integer, _
                                                  ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, _
                                                  ByVal int_CodigoOpcion As Integer, ByRef str_Mensaje As String) As Integer
            Return obj_da_CriterioConducta.FUN_UPD_CriteriosConducta(int_CodCritCond, str_Descripcion, int_CodTipoCritCond, int_Puntaje, _
                                                                     int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, _
                                                                     int_CodigoOpcion, str_Mensaje)
        End Function
#End Region
#Region "Metodos No Transaccionales"
        Public Function FUN_LIS_CriterioXtipoCriterio(ByVal CodTipoCriterio As Integer, ByVal CodigoUsuario As Integer, _
                                                       ByVal CodigoTipoUsuario As Integer, ByVal CodigoModulo As Integer, _
                                                       ByVal CodigoOpcion As Integer) As DataSet
            Return obj_da_CriterioConducta.FUN_LIS_CriterioXtipoCriterio(CodTipoCriterio, CodigoUsuario, CodigoTipoUsuario, _
                                                                         CodigoModulo, CodigoOpcion)
        End Function
        Public Function FUN_GET_CriterioXtipoCriterioByVal(ByVal CodigoCriterioConducta As Integer, ByVal CodigoUsuario As Integer, _
                                                       ByVal CodigoTipoUsuario As Integer, ByVal CodigoModulo As Integer, _
                                                       ByVal CodigoOpcion As Integer) As DataSet
            Return obj_da_CriterioConducta.FUN_GET_CriterioXtipoCriterioByVal(CodigoCriterioConducta, CodigoUsuario, _
                                                                              CodigoTipoUsuario, CodigoModulo, CodigoOpcion)
        End Function
#End Region
    End Class
End Namespace

