Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Namespace ModuloNotas

    Public Class bl_RegistroNotasBimestralesCuantitativas

#Region "Atributos"

        Private obj_da_RegistroNotasBimestralesCuantitativas As da_RegistroNotasBimestralesCuantitativas

#End Region

#Region "Propiedades"

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_RegistroNotasBimestralesCuantitativas = New da_RegistroNotasBimestralesCuantitativas

        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_UPD_ObservacionNotasBimestralesCuantitativas( _
            ByVal int_CodigoRegistroBimestral As Integer, ByVal str_descripcionObservacion As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Return obj_da_RegistroNotasBimestralesCuantitativas.FUN_UPD_ObservacionNotasBimestralesCuantitativas( _
                int_CodigoRegistroBimestral, str_descripcionObservacion, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_NotasBimestralesCuantitativas(ByVal int_CodigoRegistroBimestral As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RegistroNotasBimestralesCuantitativas.FUN_LIS_NotasBimestralesCuantitativas( _
              int_CodigoRegistroBimestral, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_RegistroNotasBimestralesCuantitativas(ByVal int_CodigoAsignacionGrupo As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_RegistroNotasBimestralesCuantitativas.FUN_LIS_RegistroNotasBimestralesCuantitativas( _
              int_CodigoAsignacionGrupo, int_CodigoBimestre, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace