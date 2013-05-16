﻿Namespace ModuloColegio

    Public Class be_Bloques

#Region "Atributos"
        Private int_CodigoBloque As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoBloque() As Integer
            Get
                Return int_CodigoBloque
            End Get
            Set(ByVal value As Integer)
                int_CodigoBloque = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
            End Set
        End Property

        Public Property Estado() As Integer
            Get
                Return int_Estado
            End Get
            Set(ByVal value As Integer)
                int_Estado = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoBloque As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)
            int_CodigoBloque = CodigoBloque
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace