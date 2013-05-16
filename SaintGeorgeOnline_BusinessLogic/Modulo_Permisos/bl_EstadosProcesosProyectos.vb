Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos

    Public Class bl_EstadosProcesosProyectos

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_EstadoProcesoProyecto As da_EstadosProcesosProyectos

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
            obj_da_EstadoProcesoProyecto = New da_EstadosProcesosProyectos
        End Sub

#End Region


#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_EstadoProcesoProyecto(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_EstadoProcesoProyecto.FUN_LIS_EstadoProcesoProyecto(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
       
#End Region

    End Class

End Namespace
