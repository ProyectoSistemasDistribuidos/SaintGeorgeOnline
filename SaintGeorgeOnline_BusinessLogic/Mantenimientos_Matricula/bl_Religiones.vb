﻿Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_DataAccess.ModuloMatricula

Namespace ModuloMatricula
    Public Class bl_Religiones

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_Religiones As da_Religiones

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
            obj_da_Religiones = New da_Religiones
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_Religiones(ByVal objReligiones As be_Religiones, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Religiones.FUN_INS_Religion(objReligiones, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_Religiones(ByVal objReligiones As be_Religiones, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Religiones.FUN_UPD_Religion(objReligiones, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_Religiones(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_Religiones.FUN_DEL_Religion(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Religiones(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Religiones.FUN_LIS_Religion(str_Descripcion, int_Estado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_Religiones(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_Religiones.FUN_GET_Religion(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


#End Region

    End Class
End Namespace

