Namespace ModuloMatricula

    Public Class be_PaisesMinisterio

#Region "Atributos"
        Private int_CodigoPaisMinisterio As Integer
        Private str_Descripcion As String
        Private str_Abreviatura As String
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoPaisMinisterio() As Integer
            Get
                Return int_CodigoPaisMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoPaisMinisterio = value
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

        Public Property Abreviatura() As String
            Get
                Return str_Abreviatura
            End Get
            Set(ByVal value As String)
                str_Abreviatura = value
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

        Sub New(ByVal CodigoPaisMinisterio As Integer, _
                ByVal Descripcion As String, _
                ByVal Abreviatura As String, _
                ByVal Estado As Integer)
            int_CodigoPaisMinisterio = CodigoPaisMinisterio
            str_Descripcion = Descripcion
            str_Abreviatura = Abreviatura
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace