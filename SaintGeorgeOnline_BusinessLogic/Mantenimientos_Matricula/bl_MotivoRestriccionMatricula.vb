Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula

    Public Class bl_MotivoRestriccionMatricula
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_MotivoRestriccionMatricula As da_MotivoRestriccionMatricula

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
            obj_da_MotivoRestriccionMatricula = New da_MotivoRestriccionMatricula
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_MotivoRestriccionMatricula(ByVal objMotivoRestriccionMatricula As be_MotivoRestriccionMatricula, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_MotivoRestriccionMatricula.FUN_INS_MotivoRestriccionMatricula(objMotivoRestriccionMatricula, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_MotivoRestriccionMatricula(ByVal objMotivoRestriccionMatricula As be_MotivoRestriccionMatricula, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_MotivoRestriccionMatricula.FUN_UPD_MotivoRestriccionMatricula(objMotivoRestriccionMatricula, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_MotivoRestriccionMatricula(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_MotivoRestriccionMatricula.FUN_DEL_MotivoRestriccionMatricula(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_RelacionMotivoRestriccionMatricula(ByVal codigo As Integer, ByVal nombreCompleto As String, ByVal opcion As Integer, ByVal observacion As String) As Integer
            Return obj_da_MotivoRestriccionMatricula.FUN_ACT_RelacionMotivoRestriccionMatricula(codigo, nombreCompleto, opcion, observacion)
        End Function
#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_MotivoRestriccionMatricula(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_MotivoRestriccionMatricula.FUN_LIS_MotivoRestriccionMatricula(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_LIS_MotivoRestriccionMatriculaPorMotivoRestriccion() As DataSet
            Return obj_da_MotivoRestriccionMatricula.FUN_LIS_MotivoRestriccionMatriculaPorMotivoRestriccion()
        End Function
        Public Function FUN_LIS_AlumnosPorMotivoRestriccion(ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_Grado As Integer) As DataSet
            Return obj_da_MotivoRestriccionMatricula.FUN_LIS_AlumnosPorMotivoRestriccion(str_ApellidoPaterno, str_ApellidoMaterno, str_Nombre, int_Grado)
        End Function

        Public Function FUN_GET_MotivoRestriccionMatricula(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_MotivoRestriccionMatricula.FUN_GET_MotivoRestriccionMatricula(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region
    End Class

End Namespace
