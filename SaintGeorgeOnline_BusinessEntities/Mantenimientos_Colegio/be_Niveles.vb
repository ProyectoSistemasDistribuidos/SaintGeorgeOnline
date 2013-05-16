﻿Namespace ModuloColegio

    Public Class be_Niveles

#Region "Atributos"

        Private int_CodigoNivel As Integer
        Private str_Descripcion As String

#End Region

#Region "Propiedades"

        Public Property CodigoNivel() As Integer
            Get
                Return int_CodigoNivel
            End Get
            Set(ByVal value As Integer)
                int_CodigoNivel = value
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


#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoNivel As Integer, _
                ByVal Descripcion As String)
            int_CodigoNivel = CodigoNivel
            str_Descripcion = Descripcion

        End Sub

#End Region

    End Class

End Namespace