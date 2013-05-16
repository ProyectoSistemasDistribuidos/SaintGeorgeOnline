Namespace ModuloMatricula
    Public Class be_FichaAutos
#Region "Atributos"
        Private int_CodigoFichaAuto As Integer
        Private int_CodigoFamiliar As Integer
        Private str_Marca As String
        Private str_Modelo As String
        Private str_Placa As String
        Private int_Estado As Integer
#End Region
#Region "Propiedades"
        Public Property CodigoFichaAuto() As Integer
            Get
                Return int_CodigoFichaAuto
            End Get
            Set(ByVal value As Integer)
                int_CodigoFichaAuto = value
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
        Public Property Marca() As String
            Get
                Return str_Marca
            End Get
            Set(ByVal value As String)
                str_Marca = value
            End Set
        End Property
        Public Property Modelo() As String
            Get
                Return str_Modelo
            End Get
            Set(ByVal value As String)
                str_Modelo = value
            End Set
        End Property
        Public Property Placa() As String
            Get
                Return str_Placa
            End Get
            Set(ByVal value As String)
                str_Placa = value
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
        Sub New(ByVal CodigoFichaAuto As Integer, _
                ByVal CodigoFamiliar As Integer, _
                ByVal Marca As String, _
                ByVal Modelo As String, _
                ByVal Placa As String, _
                ByVal Estado As Integer)
            CodigoFichaAuto = int_CodigoFichaAuto
            CodigoFamiliar = int_CodigoFamiliar
            Marca = str_Marca
            Modelo = str_Modelo
            Placa = str_Placa
            Estado = int_Estado
        End Sub
#End Region
    End Class
End Namespace

