Namespace ModuloPensiones

    Public Class be_Letras

#Region "Atributos"

        Private int_CodigoLetra As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoFamiliar As Integer
        Private str_NumeroOperacion As String

#End Region

#Region "Propiedades"

        Public Property CodigoLetra() As Integer
            Get
                Return int_CodigoLetra
            End Get
            Set(ByVal value As Integer)
                int_CodigoLetra = value
            End Set
        End Property

        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property

        Public Property CodigoFamiliar() As Integer
            Get
                Return int_CodigoFamiliar
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliar = value
            End Set
        End Property

        Public Property NumeroOperacion() As String
            Get
                Return str_NumeroOperacion
            End Get
            Set(ByVal value As String)
                str_NumeroOperacion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoLetra As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoFamiliar As Integer, _
                ByVal NumeroOperacion As String)

            int_CodigoLetra = CodigoLetra
            int_CodigoFamiliar = CodigoFamiliar
            str_CodigoAlumno = CodigoAlumno
            str_NumeroOperacion = NumeroOperacion

        End Sub

#End Region

    End Class

End Namespace
