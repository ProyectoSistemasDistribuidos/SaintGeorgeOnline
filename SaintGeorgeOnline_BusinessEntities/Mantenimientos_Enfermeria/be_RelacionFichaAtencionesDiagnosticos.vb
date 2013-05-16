Namespace ModuloEnfermeria

    Public Class be_RelacionFichaAtencionesDiagnosticos

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoFichaAtencion As Integer
        Private int_CodigoDiagnostico As Integer
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

        Public Property CodigoDiagnostico() As Integer
            Get
                Return int_CodigoDiagnostico
            End Get
            Set(ByVal value As Integer)
                int_CodigoDiagnostico = value
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
                ByVal CodigoDiagnostico As Integer, _
                ByVal Estado As Integer)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoFichaAtencion = CodigoFichaAtencion
            int_CodigoDiagnostico = CodigoDiagnostico
            int_Estado = Estado

        End Sub
#End Region

    End Class

End Namespace


