Namespace ModuloColegio

    Public Class be_EstadosAniosAcademicos

#Region "Atributos"
        Private int_CodigoEstadosAniosAcademicos As Integer
        Private str_Descripcion As String
#End Region

#Region "Propiedades"

        Public Property CodigoEstadosAniosAcademicos() As Integer
            Get
                Return int_CodigoEstadosAniosAcademicos
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstadosAniosAcademicos = value
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

        Sub New(ByVal CodigoEstadosAniosAcademicos As Integer, _
                ByVal Descripcion As String)
            int_CodigoEstadosAniosAcademicos = CodigoEstadosAniosAcademicos
            str_Descripcion = Descripcion

        End Sub

#End Region

    End Class

End Namespace

