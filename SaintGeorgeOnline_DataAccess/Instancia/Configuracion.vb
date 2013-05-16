Imports SaintGeorgeOnline_Utilities
Imports System.Configuration

Namespace InstanciaConexion

    Public Class Configuracion

#Region "Atributos"

        Private Shared str_CadenaConexionDB As String = New Seguridad().DesencriptarOnlyText(ConfigurationManager.AppSettings("BDInstance").ToString())
        Private Shared str_CadenaConexionAdmisionDB As String = New Seguridad().DesencriptarOnlyText(ConfigurationManager.AppSettings("BDInstanceAdmision").ToString())
        Private Shared str_CadenaConexionArticulos As String = New Seguridad().DesencriptarOnlyText(ConfigurationManager.AppSettings("BDArticulos").ToString())











#End Region

#Region "Propiedades"

        Public Shared ReadOnly Property CadenaConexionDB() As String
            Get
                Return str_CadenaConexionDB
            End Get
        End Property

        Public Shared ReadOnly Property CadenaConexionAdmisionDB() As String
            Get
                Return str_CadenaConexionAdmisionDB
            End Get
        End Property
        Public Shared ReadOnly Property CadenaConexionArticulos() As String
            Get
                Return str_CadenaConexionArticulos
            End Get
        End Property

#End Region

    End Class

End Namespace
