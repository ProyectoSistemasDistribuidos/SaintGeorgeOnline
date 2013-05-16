Namespace ModuloPensiones

    Public Class be_ConceptosCobros

#Region "Atributos"

        Private int_CodigoConceptoCobro As Integer
        Private int_CodigoTalonario As Integer
        Private str_Descripcion As String
        Private do_Mora As Double
        Private int_AfectaBeca As Integer
        Private int_IGV As Integer
        Private do_Monto As Double
        Private str_Abrev As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoConceptoCobro() As Integer
            Get
                Return int_CodigoConceptoCobro
            End Get
            Set(ByVal value As Integer)
                int_CodigoConceptoCobro = value
            End Set
        End Property

        Public Property CodigoTalonario() As Integer
            Get
                Return int_CodigoTalonario
            End Get
            Set(ByVal value As Integer)
                int_CodigoTalonario = value
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

        Public Property Mora() As Double
            Get
                Return do_Mora
            End Get
            Set(ByVal value As Double)
                do_Mora = value
            End Set
        End Property

        Public Property AfectaBeca() As Integer
            Get
                Return int_AfectaBeca
            End Get
            Set(ByVal value As Integer)
                int_AfectaBeca = value
            End Set
        End Property

        Public Property IGV() As Integer
            Get
                Return int_IGV
            End Get
            Set(ByVal value As Integer)
                int_IGV = value
            End Set
        End Property

        Public Property Monto() As Double
            Get
                Return do_Monto
            End Get
            Set(ByVal value As Double)
                do_Monto = value
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
        Sub New(ByVal CodigoConceptoCobro As Integer, _
                ByVal CodigoTalonario As Integer, _
                ByVal Descripcion As String, _
                ByVal do_Mora As Double, _
                ByVal int_AfectaBeca As Integer, _
                ByVal int_IGV As Integer, _
                ByVal do_Monto As Integer, _
                ByVal Abrev As String, _
                ByVal Estado As Integer)

            int_CodigoConceptoCobro = CodigoConceptoCobro
            int_CodigoTalonario = CodigoTalonario
            str_Descripcion = Descripcion
            do_Mora = Mora
            int_AfectaBeca = AfectaBeca
            int_IGV = IGV
            do_Monto = Monto
            str_Abrev = Abrev
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace