Namespace ModuloEnfermeria

    Public Class be_RelacionFichaAtencionesProcedimientosRealizados

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoFichaAtencion As Integer
        Private int_CodigoProcedimientoRealizado As Integer
        Private str_DescripcionProcedimientoRealizado As String
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

        Public Property CodigoProcedimientoRealizado() As Integer
            Get
                Return int_CodigoProcedimientoRealizado
            End Get
            Set(ByVal value As Integer)
                int_CodigoProcedimientoRealizado = value
            End Set
        End Property

        Public Property DescripcionProcedimientoRealizado() As String
            Get
                Return str_DescripcionProcedimientoRealizado
            End Get
            Set(ByVal value As String)
                str_DescripcionProcedimientoRealizado = value
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
                ByVal CodigoProcedimientoRealizado As Integer, _
                ByVal DescripcionProcedimientoRealizado As String, _
                ByVal Estado As Integer)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoFichaAtencion = CodigoFichaAtencion
            int_CodigoProcedimientoRealizado = CodigoProcedimientoRealizado
            str_DescripcionProcedimientoRealizado = DescripcionProcedimientoRealizado
            int_Estado = Estado

        End Sub
#End Region

    End Class

End Namespace
