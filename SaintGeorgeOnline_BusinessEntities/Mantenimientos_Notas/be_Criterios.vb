Namespace ModuloNotas

    Public Class be_Criterios

#Region "Atributos"

        Private int_CodigoCriterio As Integer
        Private str_Descripcion As String

#End Region

#Region "Propiedades"

        Public Property CodigoCriterio() As Integer
            Get
                Return int_CodigoCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoCriterio = value
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

        Sub New(ByVal CodigoCriterio As Integer, _
                ByVal Descripcion As Integer)

            int_CodigoCriterio = CodigoCriterio
            str_Descripcion = Descripcion

        End Sub

#End Region

    End Class

End Namespace