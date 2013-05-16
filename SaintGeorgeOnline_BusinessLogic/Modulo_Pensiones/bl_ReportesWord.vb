Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones

Namespace ModuloPensiones

    Public Class bl_ReportesWord

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_ReportesWord As da_ReportesWord

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
            obj_da_ReportesWord = New da_ReportesWord
        End Sub

#End Region

#Region "Metodos Transacciones"

    


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_ReportesWord(ByVal int_CodigoAnio As Integer, ByVal str_CodigoAlumno As String, ByVal str_CodigoFamilia As String, _
                                             ByVal dt_Fecha As Date, ByVal int_TipoReporte As Integer, ByVal int_CantidadDeudas As Integer, _
                                             ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ReportesWord.FUN_LIS_ReporteWord(int_CodigoAnio, str_CodigoAlumno, str_CodigoFamilia, _
                dt_Fecha, int_TipoReporte, int_CantidadDeudas, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


        Public Function FUN_LIS_ReportesWordPorGrados( _
            ByVal int_CodigoAnio As Integer, ByVal str_CodigoAlumno As String, ByVal str_CodigoFamilia As String, _
            ByVal int_CodigoGradoIni As Integer, ByVal int_CodigoGradoFin As Integer, _
            ByVal dt_Fecha As Date, ByVal int_TipoReporte As Integer, ByVal int_CantidadDeudas As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_ReportesWord.FUN_LIS_ReportesWordPorGrados(int_CodigoAnio, str_CodigoAlumno, str_CodigoFamilia, _
                int_CodigoGradoIni, int_CodigoGradoFin, _
                dt_Fecha, int_TipoReporte, int_CantidadDeudas, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace