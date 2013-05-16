Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloSeguimiento
Imports SaintGeorgeOnline_DataAccess.ModuloSeguimiento

Namespace ModuloSeguimiento

    Public Class bl_GrupoCriterioEvaluacion

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_GrupoCriterioEvaluacion As da_GrupoCriterioEvaluacion

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
            obj_da_GrupoCriterioEvaluacion = New da_GrupoCriterioEvaluacion
        End Sub

#End Region

#Region "Metodos Transacciones"

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_GrupoCriterioEvaluacion(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_GrupoCriterioEvaluacion.FUN_LIS_GrupoCriterioEvaluacion(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
