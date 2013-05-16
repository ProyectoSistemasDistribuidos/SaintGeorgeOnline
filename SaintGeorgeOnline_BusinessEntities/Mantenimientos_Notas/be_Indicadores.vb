Namespace ModuloNotas

    Public Class be_Indicadores

#Region "Atributos"

        Private int_CodigoIndicador As Integer
        Private int_CodigoIndicadorSIE As Integer
        Private int_CodigoComponente As Integer
        Private str_Descripcion As String

#End Region

#Region "Propiedades"

        Public Property CodigoIndicador() As Integer
            Get
                Return int_CodigoIndicador
            End Get
            Set(ByVal value As Integer)
                int_CodigoIndicador = value
            End Set
        End Property

        Public Property CodigoIndicadorSIE() As Integer
            Get
                Return int_CodigoIndicadorSIE
            End Get
            Set(ByVal value As Integer)
                int_CodigoIndicadorSIE = value
            End Set
        End Property

        Public Property CodigoComponente() As Integer
            Get
                Return int_CodigoComponente
            End Get
            Set(ByVal value As Integer)
                int_CodigoComponente = value
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

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoIndicador As Integer, _
                ByVal CodigoIndicadorSIE As Integer, _
                ByVal CodigoComponente As Integer, _
                ByVal Descripcion As Integer)

            int_CodigoIndicador = CodigoIndicador
            int_CodigoIndicadorSIE = CodigoIndicadorSIE
            int_CodigoComponente = CodigoComponente
            str_Descripcion = Descripcion

        End Sub

#End Region

    End Class

End Namespace
