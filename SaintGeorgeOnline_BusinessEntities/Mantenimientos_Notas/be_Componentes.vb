Namespace ModuloNotas

    Public Class be_Componentes

#Region "Atributos"

        Private int_CodigoComponente As Integer
        Private int_CodigoComponenteSIE As Integer
        Private str_Descripcion As String

#End Region

#Region "Propiedades"

        Public Property CodigoComponente() As Integer
            Get
                Return int_CodigoComponente
            End Get
            Set(ByVal value As Integer)
                int_CodigoComponente = value
            End Set
        End Property

        Public Property CodigoComponenteSIE() As Integer
            Get
                Return int_CodigoComponenteSIE
            End Get
            Set(ByVal value As Integer)
                int_CodigoComponenteSIE = value
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

        Sub New(ByVal CodigoComponente As Integer, _
                ByVal CodigoComponenteSIE As Integer, _
                ByVal Descripcion As Integer)

            int_CodigoComponente = CodigoComponente
            int_CodigoComponenteSIE = CodigoComponenteSIE
            str_Descripcion = Descripcion

        End Sub

#End Region

    End Class

End Namespace
