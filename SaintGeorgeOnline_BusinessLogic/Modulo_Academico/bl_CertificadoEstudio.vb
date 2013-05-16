Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloAcademico
Namespace ModuloAcademico
    Public Class bl_CertificadoEstudio

#Region "Atributos"
        Private obj_da_CertificadoEstudio As da_CertificadoEstudio
#End Region
#Region "Constructor"
        Public Sub New()
            obj_da_CertificadoEstudio = New da_CertificadoEstudio
        End Sub
#End Region
#Region "Métodos Transaccionales"

#End Region
#Region "Método No Transaccionales"

        Public Function FUN_LIS_CertificadoEstudio(ByVal int_CodigoAnioAcademico As Integer, ByVal str_CodigoAlumno As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_CertificadoEstudio.FUN_LIS_CertificadoEstudio(int_CodigoAnioAcademico, str_CodigoAlumno, _
            int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region
    End Class
End Namespace
