Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos
Imports SaintGeorgeOnline_DataAccess.ModuloPermisos

Namespace ModuloPermisos

    Public Class bl_AsignacionAccionesBloquesInformacion

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionAccionesBloquesInformacion As da_AsignacionAccionesBloquesInformacion

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
            obj_da_AsignacionAccionesBloquesInformacion = New da_AsignacionAccionesBloquesInformacion
        End Sub

#End Region

    End Class

End Namespace
