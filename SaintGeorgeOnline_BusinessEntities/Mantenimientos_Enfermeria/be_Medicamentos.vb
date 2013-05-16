Namespace ModuloEnfermeria

    Public Class be_Medicamentos

#Region "Atributos"

        Private int_CodigoMedicamento As Integer
        Private int_CodigoNombre As Integer
        Private int_CodigoRelacion As Integer
        Private int_CodigoPresentacion As Integer
        Private int_CodigoUnidadMedida As Integer
        Private int_Cantidad As Integer
        Private str_Concentracion As String
        Private int_Control As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoMedicamento() As Integer
            Get
                Return int_CodigoMedicamento
            End Get
            Set(ByVal value As Integer)
                int_CodigoMedicamento = value
            End Set
        End Property
        Public Property CodigoNombre() As Integer
            Get
                Return int_CodigoNombre
            End Get
            Set(ByVal value As Integer)
                int_CodigoNombre = value
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
        Public Property CodigoRelacion() As Integer
            Get
                Return int_CodigoRelacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacion = value
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
        Public Property Cantidad() As Integer
            Get
                Return int_Cantidad
            End Get
            Set(ByVal value As Integer)
                int_Cantidad = value
            End Set
        End Property
        Public Property Concentracion() As String
            Get
                Return str_Concentracion
            End Get
            Set(ByVal value As String)
                str_Concentracion = value
            End Set
        End Property
        Public Property Control() As Integer
            Get
                Return int_Control
            End Get
            Set(ByVal value As Integer)
                int_Control = value
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
        Sub New(ByVal CodigoMedicamento As Integer, _
                ByVal CodigoNombre As Integer, _
                ByVal CodigoRelacion As Integer, _
                ByVal CodigoPresentacion As Integer, _
                ByVal CodigoUnidadMedida As Integer, _
                ByVal Cantidad As Integer, _
                ByVal Concentracion As String, _
                ByVal Control As Integer, _
                ByVal Estado As Integer)
            int_CodigoMedicamento = CodigoMedicamento
            int_CodigoNombre = CodigoNombre
            int_CodigoRelacion = CodigoRelacion
            int_CodigoPresentacion = CodigoPresentacion
            int_CodigoUnidadMedida = CodigoUnidadMedida
            int_Cantidad = Cantidad
            str_Concentracion = Concentracion
            int_Control = Control
            int_Estado = Estado
        End Sub
#End Region

    End Class

End Namespace