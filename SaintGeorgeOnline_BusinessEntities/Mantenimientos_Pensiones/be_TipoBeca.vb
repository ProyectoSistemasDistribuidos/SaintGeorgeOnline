Namespace ModuloPensiones

    Public Class be_TipoBeca
#Region "Atributos"

        Private int_CodigoTipoBeca As Integer
        Private str_Descripcion As String
        Private str_Abrev As String
        Private do_PorcentajeDescuento As Double
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoTipoBeca() As Integer
            Get
                Return int_CodigoTipoBeca
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoBeca = value
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

        Public Property Abrev() As String
            Get
                Return str_Abrev
            End Get
            Set(ByVal value As String)
                str_Abrev = value
            End Set
        End Property

        Public Property PorcentajeDescuento() As Double
            Get
                Return do_PorcentajeDescuento
            End Get
            Set(ByVal value As Double)
                do_PorcentajeDescuento = value
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
        Sub New(ByVal CodigoTipoBeca As Integer, _
               ByVal Descripcion As String, _
               ByVal Abrev As String, _
               ByVal PorcentajeDescuento As Double, _
               ByVal Estado As Integer)

            int_CodigoTipoBeca = CodigoTipoBeca
            str_Descripcion = Descripcion
            str_Abrev = Abrev
            do_PorcentajeDescuento = PorcentajeDescuento
            int_Estado = Estado
        End Sub

#End Region
    End Class

End Namespace
