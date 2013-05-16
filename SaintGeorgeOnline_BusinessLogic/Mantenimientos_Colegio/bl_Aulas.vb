Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio

Namespace ModuloColegio

    Public Class bl_Aulas

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Aulas As da_Aulas

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
            obj_da_Aulas = New da_Aulas
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_Aulas(ByVal objAulas As be_Aulas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Aulas.FUN_INS_Aulas(objAulas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_Aulas(ByVal objAulas As be_Aulas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Aulas.FUN_UPD_Aulas(objAulas, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_Aulas(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Aulas.FUN_DEL_Aulas(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Aulas(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Aulas.FUN_LIS_Aulas(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_Aulas(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Aulas.FUN_LIS_Aulas(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_AulasXAnio(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Aulas.FUN_LIS_AulasXAnio(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_LIS_Aulas(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Aulas.FUN_LIS_Aulas(int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_AulasXTutor(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Aulas.FUN_LIS_AulasXTutor(int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_AulasXTutorJefeNivel(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Aulas.FUN_LIS_AulasXTutorJefeNivel(int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_AulasXcodGradoXcodAulMin(ByVal int_CodigoGrado As Integer, ByVal int_CodigoAulaMinisterio As Integer, _
                                                         ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, _
                                                         ByVal int_CodigoModulo As Integer, ByVal CodigoOpcion As Integer) As String
            Return obj_da_Aulas.FUN_LIS_AulasXcodGradoXcodAulMin(int_CodigoGrado, int_CodigoAulaMinisterio, int_CodigoUsuario, _
                                                                 int_CodigoTipoUsuario, int_CodigoModulo, CodigoOpcion)
        End Function

        Public Function FUN_GET_Aulas(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Aulas.FUN_GET_Aulas(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace

