Namespace ModuloEnfermeria

    Public Class be_RelacionPresentacionesUnidadesMedidasMedicamentos

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoPresentacion As Integer
        Private int_CodigoUnidadMedida As Integer
        Private str_DescripcionPresentacion As String
        Private str_DescripcionUnidadMedida As String
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
        Public Property CodigoPresentacion() As Integer
            Get
                Return int_CodigoPresentacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoPresentacion = value
            End Set
        End Property
        Public Property CodigoUnidadMedida() As Integer
            Get
                Return int_CodigoUnidadMedida
            End Get
            Set(ByVal value As Integer)
                int_CodigoUnidadMedida = value
            End Set
        End Property
        Public Property DescripcionPresentacion() As String
            Get
                Return str_DescripcionPresentacion
            End Get
            Set(ByVal value As String)
                str_DescripcionPresentacion = value
            End Set
        End Property
        Public Property DescripcionUnidadMedida() As String
            Get
                Return str_DescripcionUnidadMedida
            End Get
            Set(ByVal value As String)
                str_DescripcionUnidadMedida = value
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
                ByVal CodigoPresentacion As Integer, _
                ByVal CodigoUnidadMedida As Integer, _
                ByVal DescripcionPresentacion As String, _
                ByVal DescripcionUnidadMedida As String, _
                ByVal Estado As Integer)
            int_CodigoRelacion = CodigoRelacion
            int_CodigoPresentacion = CodigoPresentacion
            int_CodigoUnidadMedida = CodigoUnidadMedida
            str_DescripcionPresentacion = DescripcionPresentacion
            str_DescripcionUnidadMedida = DescripcionUnidadMedida
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace