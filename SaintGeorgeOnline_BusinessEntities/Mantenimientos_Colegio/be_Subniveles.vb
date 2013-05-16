Namespace ModuloColegio

    Public Class be_Subniveles

#Region "Atributos"

        Private int_CodigoSubnivel As Integer
        Private int_CodigoNivel As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoSubnivel() As Integer
            Get
                Return int_CodigoSubnivel
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubnivel = value
            End Set
        End Property

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
        Sub New(ByVal CodigoSubnivel As Integer, _
                ByVal CodigoNivel As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)

            int_CodigoSubnivel = CodigoSubnivel
            int_CodigoNivel = CodigoNivel
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

