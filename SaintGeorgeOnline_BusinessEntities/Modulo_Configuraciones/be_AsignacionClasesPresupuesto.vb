Namespace ModuloConfiguraciones

    Public Class be_AsignacionClasesPresupuesto
#Region "Atributos"
        Private int_CodigoEstructuraPresupuesto As Integer
        Private int_CodigoPeriodo As Integer
        Private int_CodigoSubSubSubCentroCosto As Integer
        Private int_CodigoEstructuraClase As Integer
        Private int_CodigoEstructuraCategoria As Integer
        Private int_CodigoEstructuraSubCategoria As Integer
        Private int_CodigoSSSCentroCostoClase As Integer
        Private int_CodigoSSCentroCostoCategoria As Integer
        Private int_CodigoSSCentroCostoSubCategoria As Integer
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoEstructuraPresupuesto() As Integer
            Get
                Return int_CodigoEstructuraPresupuesto
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstructuraPresupuesto = value
            End Set
        End Property
        Public Property CodigoPeriodo() As Integer
            Get
                Return int_CodigoPeriodo
            End Get
            Set(ByVal value As Integer)
                int_CodigoPeriodo = value
            End Set
        End Property
        Public Property CodigoSubSubSubCentroCosto() As Integer
            Get
                Return int_CodigoSubSubSubCentroCosto
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubSubSubCentroCosto = value
            End Set
        End Property
        Public Property CodigoSSSCentroCostoClase() As Integer
            Get
                Return int_CodigoSSSCentroCostoClase
            End Get
            Set(ByVal value As Integer)
                int_CodigoSSSCentroCostoClase = value
            End Set
        End Property
        Public Property CodigoSSCentroCostoCategoria() As Integer
            Get
                Return int_CodigoSSCentroCostoCategoria
            End Get
            Set(ByVal value As Integer)
                int_CodigoSSCentroCostoCategoria = value
            End Set
        End Property
        Public Property CodigoSSCentroCostoSubCategoria() As Integer
            Get
                Return int_CodigoSSCentroCostoSubCategoria
            End Get
            Set(ByVal value As Integer)
                int_CodigoSSCentroCostoSubCategoria = value
            End Set
        End Property

        Public Property CodigoEstructuraClase() As Integer
            Get
                Return int_CodigoEstructuraClase
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstructuraClase = value
            End Set
        End Property
        Public Property CodigoEstructuraCategoria() As Integer
            Get
                Return int_CodigoEstructuraCategoria
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstructuraCategoria = value
            End Set
        End Property
        Public Property CodigoEstructuraSubCategoria() As Integer
            Get
                Return int_CodigoEstructuraSubCategoria
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstructuraSubCategoria = value
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

        Sub New(ByVal CodigoEstructuraPresupuesto As Integer, _
                ByVal CodigoPeriodo As Integer, _
                ByVal CodigoSubSubSubCentroCosto As Integer, _
                ByVal CodigoEstructuraClase As Integer, _
                ByVal CodigoEstructuraCategoria As Integer, _
                ByVal CodigoEstructuraSubCategoria As Integer, _
                ByVal CodigoSSSCentroCostoClase As Integer, _
                ByVal CodigoSSCentroCostoCategoria As Integer, _
                ByVal CodigoSSCentroCostoSubCategoria As Integer, _
                ByVal Estado As Integer)
            int_CodigoEstructuraPresupuesto = CodigoEstructuraPresupuesto
            int_CodigoPeriodo = CodigoPeriodo
            int_CodigoSubSubSubCentroCosto = CodigoSubSubSubCentroCosto
            int_CodigoEstructuraClase = CodigoEstructuraClase
            int_CodigoEstructuraCategoria = CodigoEstructuraCategoria
            int_CodigoEstructuraSubCategoria = CodigoEstructuraSubCategoria
            int_CodigoSSSCentroCostoClase = CodigoSSSCentroCostoClase
            int_CodigoSSCentroCostoCategoria = CodigoSSCentroCostoCategoria
            int_CodigoSSCentroCostoSubCategoria = CodigoSSCentroCostoSubCategoria
            int_Estado = Estado
        End Sub

#End Region
    End Class

End Namespace

