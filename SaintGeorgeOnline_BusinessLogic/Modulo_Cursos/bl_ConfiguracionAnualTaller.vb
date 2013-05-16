Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos
Imports SaintGeorgeOnline_DataAccess.ModuloCursos

Namespace ModuloCursos

    Public Class bl_ConfiguracionAnualTaller

#Region "Atributos"

        Private obj_da_ConfiguracionAnualTaller As da_ConfiguracionAnualTaller

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_ConfiguracionAnualTaller = New da_ConfiguracionAnualTaller

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ConfiguracionAnualTaller(ByVal objConfiguracionAnualTaller As be_ConfiguracionAnualTaller, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ConfiguracionAnualTaller.FUN_INS_ConfiguracionAnualTaller(objConfiguracionAnualTaller, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_ConfiguracionAnualTallerApertura(ByVal int_Codigo As Integer, ByVal int_EstadoApertura As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ConfiguracionAnualTaller.FUN_UPD_ConfiguracionAnualTallerApertura(int_Codigo, int_EstadoApertura, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ConfiguracionAnualTaller(ByVal int_CodigoAnioAcademico As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ConfiguracionAnualTaller.FUN_LIS_ConfiguracionAnualTaller(int_CodigoAnioAcademico, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace