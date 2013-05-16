Namespace ModuloPensiones

    Public Class be_Moneda

#Region "Atributos"

        Private int_CodigoMoneda As Integer
        Private str_Descripcion As String
        Private str_Simbolo As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoMoneda() As Integer
            Get
                Return int_CodigoMoneda
            End Get
            Set(ByVal value As Integer)
                int_CodigoMoneda = value
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

        Public Property Simbolo() As String
            Get
                Return str_Simbolo
            End Get
            Set(ByVal value As String)
                str_Simbolo = value
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
        Sub New(ByVal CodigoMoneda As Integer, _
                ByVal Descripcion As String, _
                ByVal Simbolo As String, _
                ByVal Estado As Integer)

            int_CodigoMoneda = CodigoMoneda
            str_Descripcion = Descripcion
            str_Simbolo = Simbolo
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace