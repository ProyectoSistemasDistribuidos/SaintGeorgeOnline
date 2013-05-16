Namespace ModuloMatricula

    Public Class be_MotivoRestriccionMatricula
#Region "Atributos"
        Private int_CodigoMotivoRestriccionMatricula As Integer
        Private str_Descripcion As String
        Private str_MensajeAlerta As String
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoMotivoRestriccionMatricula() As Integer
            Get
                Return int_CodigoMotivoRestriccionMatricula
            End Get
            Set(ByVal value As Integer)
                int_CodigoMotivoRestriccionMatricula = value
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

        Public Property MensajeAlerta() As String
            Get
                Return str_MensajeAlerta
            End Get
            Set(ByVal value As String)
                str_MensajeAlerta = value
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

        Sub New(ByVal CodigoMotivoRestriccionMatricula As Integer, _
                ByVal Descripcion As String, _
                ByVal MensajeAlerta As String, _
                ByVal Estado As Integer)
            int_CodigoMotivoRestriccionMatricula = CodigoMotivoRestriccionMatricula
            str_Descripcion = Descripcion
            str_MensajeAlerta = MensajeAlerta
            int_Estado = Estado
        End Sub

#End Region
    End Class

End Namespace