Namespace ModuloPensiones

    Public Class be_Talonarios

#Region "Atributos"

        Private int_CodigoTalonario As Integer
        Private str_Serie As String
        Private str_Correlativo As String
        Private str_Descripcion As String
        Private int_ImprRecibo As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoTalonario() As Integer
            Get
                Return int_CodigoTalonario
            End Get
            Set(ByVal value As Integer)
                int_CodigoTalonario = value
            End Set
        End Property

        Public Property Serie() As String
            Get
                Return str_Serie
            End Get
            Set(ByVal value As String)
                str_Serie = value
            End Set
        End Property

        Public Property Correlativo() As String
            Get
                Return str_Correlativo
            End Get
            Set(ByVal value As String)
                str_Correlativo = value
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

        Public Property ImprRecibo() As Integer
            Get
                Return int_ImprRecibo
            End Get
            Set(ByVal value As Integer)
                int_ImprRecibo = value
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
        Sub New(ByVal CodigoTalonario As Integer, _
                ByVal Serie As String, _
                ByVal Correlativo As String, _
                ByVal Descripcion As String, _
                ByVal int_ImprRecibo As Integer, _
                ByVal Estado As Integer)

            int_CodigoTalonario = CodigoTalonario
            str_Serie = Serie
            str_Correlativo = Correlativo
            str_Descripcion = Descripcion
            int_ImprRecibo = ImprRecibo
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace