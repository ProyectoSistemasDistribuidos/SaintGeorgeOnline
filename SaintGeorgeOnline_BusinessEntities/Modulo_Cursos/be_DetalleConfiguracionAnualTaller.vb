Namespace ModuloCursos

    Public Class be_DetalleConfiguracionAnualTaller

#Region "Atributos"

        Private int_CodigoDetalle As Integer
        Private int_CodigoConfiguracionAnualTaller As Integer
        Private int_CodigoBimestre As Integer
        Private int_Estado As Integer
        Private int_EstadoConfiguracion As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoDetalle() As Integer
            Get
                Return int_CodigoDetalle
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetalle = value
            End Set
        End Property

        Public Property CodigoConfiguracionAnualTaller() As Integer
            Get
                Return int_CodigoConfiguracionAnualTaller
            End Get
            Set(ByVal value As Integer)
                int_CodigoConfiguracionAnualTaller = value
            End Set
        End Property

        Public Property CodigoBimestre() As Integer
            Get
                Return int_CodigoBimestre
            End Get
            Set(ByVal value As Integer)
                int_CodigoBimestre = value
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

        Public Property EstadoConfiguracion() As Integer
            Get
                Return int_EstadoConfiguracion
            End Get
            Set(ByVal value As Integer)
                int_EstadoConfiguracion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoDetalle As Integer, _
                ByVal CodigoConfiguracionAnualTaller As Integer, _
                ByVal CodigoBimestre As Integer, _
                ByVal Estado As Integer, _
                ByVal EstadoConfiguracion As Integer)

            int_CodigoDetalle = CodigoDetalle
            int_CodigoConfiguracionAnualTaller = CodigoConfiguracionAnualTaller
            int_CodigoBimestre = CodigoBimestre
            int_Estado = Estado
            int_EstadoConfiguracion = EstadoConfiguracion

        End Sub

#End Region

    End Class

End Namespace