Namespace ModuloPensiones

    Public Class be_ConceptosServicios

#Region "Atributos"

        Private int_CodigoConceptoServicio As Integer
        Private int_CodigoConceptoCobro As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoConceptoServicio() As Integer
            Get
                Return int_CodigoConceptoServicio
            End Get
            Set(ByVal value As Integer)
                int_CodigoConceptoServicio = value
            End Set
        End Property

        Public Property CodigoConceptoCobro() As Integer
            Get
                Return int_CodigoConceptoCobro
            End Get
            Set(ByVal value As Integer)
                int_CodigoConceptoCobro = value
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
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoConceptoServicio As Integer, _
                ByVal CodigoConceptoCobro As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)

            int_CodigoConceptoServicio = CodigoConceptoServicio
            int_CodigoConceptoCobro = CodigoConceptoCobro
            str_Descripcion = Descripcion
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace