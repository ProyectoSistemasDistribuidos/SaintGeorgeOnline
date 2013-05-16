Namespace ModuloPensiones

    Public Class be_CuentasBancarias

#Region "Atributos"

        Private int_CodigoCuentaBancaria As Integer
        Private int_CodigoBanco As Integer
        Private int_CodigoMoneda As Integer
        Private str_NumeroCuenta As String
        Private str_Descripcion As String
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoCuentaBancaria() As Integer
            Get
                Return int_CodigoCuentaBancaria
            End Get
            Set(ByVal value As Integer)
                int_CodigoCuentaBancaria = value
            End Set
        End Property

        Public Property CodigoBanco() As Integer
            Get
                Return int_CodigoBanco
            End Get
            Set(ByVal value As Integer)
                int_CodigoBanco = value
            End Set
        End Property

        Public Property CodigoMoneda() As Integer
            Get
                Return int_CodigoMoneda
            End Get
            Set(ByVal value As Integer)
                int_CodigoMoneda = value
            End Set
        End Property
        
        Public Property NumeroCuenta() As String
            Get
                Return str_NumeroCuenta
            End Get
            Set(ByVal value As String)
                str_NumeroCuenta = value
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
        Sub New(ByVal CodigoCuentaBancaria As Integer, _
                ByVal CodigoBanco As Integer, _
                ByVal CodigoMoneda As Integer, _
                ByVal NumeroCuenta As String, _
                ByVal Estado As Integer)

            int_CodigoCuentaBancaria = CodigoCuentaBancaria
            int_CodigoBanco = CodigoBanco
            int_CodigoMoneda = CodigoMoneda
            str_NumeroCuenta = NumeroCuenta
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
