Namespace ModuloColegio

    Public Class be_Precedencias

#Region "Atributos"
        Private int_CodigoPrecedencia As Integer
        Private int_CodigoGradoALlevar As String
        Private int_CodigoGradoRequisito As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoPrecedencia() As Integer
            Get
                Return int_CodigoPrecedencia
            End Get
            Set(ByVal value As Integer)
                int_CodigoPrecedencia = value
            End Set
        End Property

        Public Property CodigoGradoALlevar() As Integer
            Get
                Return int_CodigoGradoALlevar
            End Get
            Set(ByVal value As Integer)
                int_CodigoGradoALlevar = value
            End Set
        End Property

        Public Property CodigoGradoRequisito() As Integer
            Get
                Return int_CodigoGradoRequisito
            End Get
            Set(ByVal value As Integer)
                int_CodigoGradoRequisito = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoPrecedencia As Integer, _
                ByVal CodigoGradoALlevar As Integer, _
                ByVal CodigoGradoRequisito As Integer)
            int_CodigoPrecedencia = CodigoPrecedencia
            int_CodigoGradoALlevar = CodigoGradoALlevar
            int_CodigoGradoRequisito = CodigoGradoRequisito
        End Sub

#End Region

    End Class

End Namespace