Namespace ModuloNotas

    Public Class be_SubIndicadores

#Region "Atributos"


        Private int_CodigoSubIndicador As Integer
        Private int_CodigoSubIndicadorSIE As Integer
        Private int_CodigoIndicador As Integer
        Private str_Descripcion As String

#End Region

#Region "Propiedades"

        Public Property CodigoSubIndicador() As Integer
            Get
                Return int_CodigoSubIndicador
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubIndicador = value
            End Set
        End Property

        Public Property CodigoSubIndicadorSIE() As Integer
            Get
                Return int_CodigoSubIndicadorSIE
            End Get
            Set(ByVal value As Integer)
                int_CodigoSubIndicadorSIE = value
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

        Sub New(ByVal CodigoSubIndicador As Integer, _
                ByVal CodigoSubIndicadorSIE As Integer, _
                ByVal CodigoIndicador As Integer, _
                ByVal Descripcion As Integer)

            int_CodigoSubIndicador = CodigoSubIndicador
            int_CodigoSubIndicadorSIE = CodigoSubIndicadorSIE
            int_CodigoIndicador = CodigoIndicador
            str_Descripcion = Descripcion

        End Sub

#End Region

    End Class

End Namespace