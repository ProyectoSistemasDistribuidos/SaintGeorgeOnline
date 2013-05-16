Namespace ModuloEnfermeria

    Public Class be_ProcedimientosRealizados

#Region "Atributos"

        Private int_CodigoProcedimientoRealizado As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoProcedimientoRealizado() As Integer
            Get
                Return int_CodigoProcedimientoRealizado
            End Get
            Set(ByVal value As Integer)
                int_CodigoProcedimientoRealizado = value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
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
            MyBase.new()
        End Sub
        Sub New(ByVal CodigoProcedimientoRealizado As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)
            int_CodigoProcedimientoRealizado = CodigoProcedimientoRealizado
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
