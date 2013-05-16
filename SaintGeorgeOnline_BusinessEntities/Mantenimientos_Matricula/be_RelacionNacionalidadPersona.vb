Namespace ModuloMatricula

    Public Class be_RelacionNacionalidadPersona

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoPersona As Integer
        Private int_CodigoNacionalidad As Integer
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

        Public Property CodigoPersona() As Integer
            Get
                Return int_CodigoPersona
            End Get
            Set(ByVal value As Integer)
                int_CodigoPersona = value
            End Set
        End Property

        Public Property CodigoNacionalidad() As Integer
            Get
                Return int_CodigoNacionalidad
            End Get
            Set(ByVal value As Integer)
                int_CodigoNacionalidad = value
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
                ByVal CodigoPersona As Integer, _
                ByVal CodigoNacionalidad As Integer, _
                ByVal Estado As Integer)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoPersona = CodigoPersona
            int_CodigoNacionalidad = CodigoNacionalidad
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace

