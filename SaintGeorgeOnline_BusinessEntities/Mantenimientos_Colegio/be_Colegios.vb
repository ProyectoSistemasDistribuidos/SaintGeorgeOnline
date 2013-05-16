Namespace ModuloColegio

    Public Class be_Colegios

#Region "Atributos"

        Private int_CodigoColegio As Integer
        Private str_Nombre As String
        Private str_PaginaWeb As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoColegio() As Integer
            Get
                Return int_CodigoColegio
            End Get
            Set(ByVal value As Integer)
                int_CodigoColegio = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return str_Nombre
            End Get
            Set(ByVal value As String)
                str_Nombre = value
            End Set
        End Property

        Public Property PaginaWeb() As String
            Get
                Return str_PaginaWeb
            End Get
            Set(ByVal value As String)
                str_PaginaWeb = value
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
        Sub New(ByVal CodigoColegio As Integer, _
                ByVal Nombre As String, _
                ByVal PaginaWeb As String, _
                ByVal Estado As Integer)

            int_CodigoColegio = CodigoColegio
            str_Nombre = Nombre
            str_PaginaWeb = PaginaWeb
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
