Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_ConceptosServicios

#Region "Atributos"

        Private obj_da_ConceptosServicios As da_ConceptosServicios

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_ConceptosServicios = New da_ConceptosServicios
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_ConceptosServicios(ByVal objConceptosServicios As be_ConceptosServicios, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_ConceptosServicios.FUN_INS_ConceptosServicios(objConceptosServicios, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ConceptosServicios(ByVal str_Descripcion As String, ByVal int_CodigoConceptoCobro As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ConceptosServicios.FUN_LIS_ConceptosServicios(str_Descripcion, int_CodigoConceptoCobro, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace