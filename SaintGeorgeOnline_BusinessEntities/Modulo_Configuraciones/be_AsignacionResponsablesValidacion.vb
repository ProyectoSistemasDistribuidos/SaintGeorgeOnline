Namespace ModuloConfiguraciones

    Public Class be_AsignacionResponsablesValidacion

#Region "Atributos"

        Private int_CodigoResponsableValidarPresupuesto As Integer
        Private int_CodigoAsignacionSSSCentroCosto As Integer
        Private int_CodigoTrabajador As Integer
        Private int_CodigoTipoValidacion As Integer
        Private int_OrdenValidacion As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoResponsableValidarPresupuesto() As Integer
            Get
                Return int_CodigoResponsableValidarPresupuesto
            End Get
            Set(ByVal value As Integer)
                int_CodigoResponsableValidarPresupuesto = value
            End Set
        End Property

        Public Property CodigoAsignacionSSSCentroCosto() As Integer
            Get
                Return int_CodigoAsignacionSSSCentroCosto
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionSSSCentroCosto = value
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

        Public Property CodigoTipoValidacion() As Integer
            Get
                Return int_CodigoTipoValidacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoValidacion = value
            End Set
        End Property

        Public Property OrdenValidacion() As Integer
            Get
                Return int_OrdenValidacion
            End Get
            Set(ByVal value As Integer)
                int_OrdenValidacion = value
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

        Sub New(ByVal CodigoResponsableValidarPresupuesto As Integer, _
                ByVal CodigoAsignacionSSSCentroCosto As Integer, _
                ByVal CodigoTrabajador As Integer, _
                ByVal CodigoTipoValidacion As Integer, _
                ByVal Estado As Integer)

            int_CodigoResponsableValidarPresupuesto = CodigoResponsableValidarPresupuesto
            int_CodigoAsignacionSSSCentroCosto = CodigoAsignacionSSSCentroCosto
            int_CodigoTrabajador = CodigoTrabajador
            int_CodigoTipoValidacion = CodigoTipoValidacion
            int_OrdenValidacion = OrdenValidacion
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace