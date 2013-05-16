Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio

Namespace ModuloColegio

    Public Class bl_CierreBimestre

#Region "Atributos"

        '  Private obj_da_CierreBimestre As da_CierreBimestre

#End Region

#Region "Constructor"

        Public Sub New()
            'obj_da_CierreBimestre = New da_CierreBimestre
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_UPD_CierreBimestre(ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_Estado As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            ' Return obj_da_CierreBimestre.FUN_UPD_CierreBimestre(int_CodigoAsignacionAula, int_CodigoBimestre, int_Estado, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        'Public Function FUN_LIS_AsignacionAulas(ByVal int_AnioAcademico As Integer, ByVal int_Sede As Integer, ByVal int_Aula As Integer, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

        '    Return obj_da_CierreBimestre.FUN_LIS_AsignacionAulas(int_AnioAcademico, int_Sede, int_Aula, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        'End Function

#End Region

    End Class

End Namespace

