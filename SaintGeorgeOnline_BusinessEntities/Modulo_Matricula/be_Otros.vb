Namespace ModuloMatricula

    Public Class be_Otros
        Inherits be_Personas

#Region "Atributos"

        Private int_CodigoOtros As Integer
        Private str_Observaciones As String
        Private int_EstadoAlumno As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoOtros() As Integer
            Get
                Return int_CodigoOtros
            End Get
            Set(ByVal value As Integer)
                int_CodigoOtros = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return str_Observaciones
            End Get
            Set(ByVal value As String)
                str_Observaciones = value
            End Set
        End Property

        Public Property EstadoAlumno() As Integer
            Get
                Return int_EstadoAlumno
            End Get
            Set(ByVal value As Integer)
                int_EstadoAlumno = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoOtros As Integer, _
                ByVal Observaciones As String, _
                ByVal EstadoAlumno As Integer)

            int_CodigoOtros = CodigoOtros
            str_Observaciones = Observaciones
            int_EstadoAlumno = Estado

        End Sub

#End Region

    End Class

End Namespace

