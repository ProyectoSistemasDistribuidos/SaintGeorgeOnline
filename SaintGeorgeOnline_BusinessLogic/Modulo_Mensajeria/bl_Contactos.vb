Imports System.Data
Imports SaintGeorgeOnline_DataAccess.ModuloMensajeria
Imports SaintGeorgeOnline_BusinessEntities.ModuloMensajeria

Namespace ModuloMensajeria

    Public Class bl_Contactos

#Region "Atributos"

        Private obj_da_Contactos As da_Contactos

#End Region

#Region "Constructor"

        Public Sub New()

            obj_da_Contactos = New da_Contactos

        End Sub

#End Region

#Region "Metodos Transaccionales"



#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Contactos(ByVal str_Usuario As String, ByVal int_TipoUsuario As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Contactos.FUN_LIS_Contactos(str_Usuario, int_TipoUsuario, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ContactosPorProfesor(ByVal int_CodigoProfesor As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Contactos.FUN_LIS_ContactosPorProfesor(int_CodigoProfesor, int_CodigoAula, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ContactosPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Contactos.FUN_LIS_ContactosPorAlumno(str_CodigoAlumno, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        'Lista de contactos para tesoreria
        Public Function FUN_LIS_ContactosPorNumDeudasGradoAula(ByVal int_NumDeudas As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_Contactos.FUN_LIS_ContactosPorNumDeudasGradoAula(int_NumDeudas, int_CodigoGrado, int_CodigoAnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace