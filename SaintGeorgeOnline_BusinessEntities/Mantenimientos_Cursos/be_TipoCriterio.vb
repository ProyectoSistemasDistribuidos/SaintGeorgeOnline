Namespace ModuloCursos

    Public Class be_TipoCriterio
#Region "Atributos"

        Private int_CodigoTipoCriterio As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoTipoCriterio() As Integer
            Get
                Return int_CodigoTipoCriterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoCriterio = value
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
        Sub New(ByVal CodigoTipoCriterio As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer)

            int_CodigoTipoCriterio = CodigoTipoCriterio
            str_Descripcion = Descripcion
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace
