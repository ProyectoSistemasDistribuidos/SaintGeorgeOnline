Namespace ModuloNotas

    Public Class be_RegistroIndicadores

#Region "Atributos"

        Private int_CodigoRegistroIndicadores As Integer
        Private int_CodigoIndicador As Integer
        Private int_CodigoRegistroComponentes As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroIndicadores() As Integer
            Get
                Return int_CodigoRegistroIndicadores
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroIndicadores = value
            End Set
        End Property

        Public Property CodigoIndicador() As Integer
            Get
                Return int_CodigoIndicador
            End Get
            Set(ByVal value As Integer)
                int_CodigoIndicador = value
            End Set
        End Property

        Public Property CodigoRegistroComponentes() As Integer
            Get
                Return int_CodigoRegistroComponentes
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroComponentes = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroIndicadores As Integer, _
                ByVal CodigoIndicador As Integer, _
                ByVal CodigoRegistroComponentes As Integer)

            int_CodigoRegistroIndicadores = CodigoRegistroIndicadores
            int_CodigoIndicador = CodigoIndicador
            int_CodigoRegistroComponentes = CodigoRegistroComponentes

        End Sub

#End Region

    End Class

End Namespace