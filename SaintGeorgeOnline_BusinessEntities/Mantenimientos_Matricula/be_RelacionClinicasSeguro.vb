Namespace ModuloMatricula

    Public Class be_RelacionClinicasSeguro

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoFichaSeguro As Integer
        Private int_CodigoClinica As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRelacion() As Integer
            Get
                Return int_CodigoRelacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacion = value
            End Set
        End Property

        Public Property CodigoFichaSeguro() As Integer
            Get
                Return int_CodigoFichaSeguro
            End Get
            Set(ByVal value As Integer)
                int_CodigoFichaSeguro = value
            End Set
        End Property

        Public Property CodigoClinica() As Integer
            Get
                Return int_CodigoClinica
            End Get
            Set(ByVal value As Integer)
                int_CodigoClinica = value
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

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoFichaSeguro As Integer, _
                ByVal CodigoClinica As Integer, _
                ByVal Estado As Integer)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoFichaSeguro = CodigoFichaSeguro
            int_CodigoClinica = CodigoClinica
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace