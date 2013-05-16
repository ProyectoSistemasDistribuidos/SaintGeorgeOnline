Namespace ModuloConfiguraciones

    Public Class be_AsignacionResponsablesPresupuestos

#Region "Atributos"
        Private int_CodigoAsignacionResponsable As Integer
        Private int_CodigoSubSubCentroCosto As Integer
        Private int_CodigoPeriodo As Integer
        Private int_CodigoTrabajador As Integer
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionResponsable() As Integer
            Get
                Return int_CodigoAsignacionResponsable
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionResponsable = value
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
        Public Property CodigoSubSubCentroCosto() As Integer
            Get
                Return int_CodigoSubSubCentroCosto
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubSubCentroCosto = value
            End Set
        End Property
        Public Property CodigoTrabajador() As Integer
            Get
                Return int_CodigoTrabajador
            End Get
            Set(ByVal value As Integer)
                int_CodigoTrabajador = value
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

        Sub New(ByVal CodigoAsignacionResponsable As Integer, _
                ByVal CodigoPeriodo As Integer, _
                ByVal CodigoSubSubCentroCosto As Integer, _
                ByVal CodigoTrabajador As Integer, _
                ByVal Estado As Integer)
            int_CodigoAsignacionResponsable = CodigoAsignacionResponsable
            int_CodigoPeriodo = CodigoPeriodo
            int_CodigoSubSubCentroCosto = CodigoSubSubCentroCosto
            int_CodigoTrabajador = CodigoTrabajador
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace

