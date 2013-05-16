Namespace ModuloNotas

    Public Class be_RegistroSubIndicadores

#Region "Atributos"

        Private int_CodigoRegistroSubIndicadores As Integer
        Private int_CodigoSubIndicador As Integer
        Private int_CodigoRegistroIndicadores As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRegistroSubIndicadores() As Integer
            Get
                Return int_CodigoRegistroSubIndicadores
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroSubIndicadores = value
            End Set
        End Property

        Public Property CodigoSubIndicador() As Integer
            Get
                Return int_CodigoSubIndicador
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubIndicador = value
            End Set
        End Property

        Public Property CodigoRegistroIndicadores() As Integer
            Get
                Return int_CodigoRegistroIndicadores
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroIndicadores = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoRegistroSubIndicadores As Integer, _
                ByVal CodigoSubIndicador As Integer, _
                ByVal CodigoRegistroIndicadores As Integer)

            int_CodigoRegistroSubIndicadores = CodigoRegistroSubIndicadores
            int_CodigoSubIndicador = CodigoSubIndicador
            int_CodigoRegistroIndicadores = CodigoRegistroIndicadores

        End Sub

#End Region

    End Class

End Namespace