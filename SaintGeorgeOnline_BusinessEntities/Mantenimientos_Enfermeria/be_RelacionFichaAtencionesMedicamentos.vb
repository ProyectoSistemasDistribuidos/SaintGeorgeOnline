Namespace ModuloEnfermeria

    Public Class BE_RelacionFichaAtencionesMedicamentos

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoFichaAtencion As Integer
        Private int_CodigoMedicamento As Integer
        Private dou_CantidadSuministrada As Double
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

        Public Property CodigoFichaAtencion() As Integer
            Get
                Return int_CodigoFichaAtencion
            End Get
            Set(ByVal value As Integer)
                int_CodigoFichaAtencion = value
            End Set
        End Property

        Public Property CodigoMedicamento() As Integer
            Get
                Return int_CodigoMedicamento
            End Get
            Set(ByVal value As Integer)
                int_CodigoMedicamento = value
            End Set
        End Property

        Public Property CantidadSuministrada() As Double
            Get
                Return dou_CantidadSuministrada
            End Get
            Set(ByVal value As Double)
                dou_CantidadSuministrada = value
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
                ByVal CodigoFichaAtencion As Integer, _
                ByVal CodigoMedicamento As Integer, _
                ByVal dou_CantidadSuministrada As Double, _
                ByVal Estado As Integer)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoFichaAtencion = CodigoFichaAtencion
            int_CodigoMedicamento = CodigoMedicamento
            dou_CantidadSuministrada = CantidadSuministrada
            int_Estado = Estado

        End Sub
#End Region

    End Class

End Namespace

