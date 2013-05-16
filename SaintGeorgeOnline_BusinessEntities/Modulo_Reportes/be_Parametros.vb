Namespace ModuloReportes

    Public Class be_Parametros

#Region "Atributos"

        Private str_Descripcion As String
        Private str_Valor As String

#End Region

#Region "Propiedades"

        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
            End Set
        End Property

        Public Property Valor() As String
            Get
                Return str_Valor
            End Get
            Set(ByVal value As String)
                str_Valor = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal Descripcion As String, _
                ByVal Valor As String)

            str_Descripcion = Descripcion
            str_Valor = Valor

        End Sub

#End Region

    End Class

End Namespace
