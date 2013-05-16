
Namespace ModuloPensiones
    '

    Public Class be_CompromisosPagos

#Region "Atributos"

        Private int_CodigoCompromisoPago As Integer
        Private int_CodigoFamiliar As Integer
        Private int_CodigoFamilia As Integer
        Private dt_FechaEmisionCompromisoPago As Date
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoCompromisoPago() As Integer
            Get
                Return int_CodigoCompromisoPago
            End Get
            Set(ByVal value As Integer)
                int_CodigoCompromisoPago = value
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

        Public Property CodigoFamilia() As Integer
            Get
                Return int_CodigoFamilia
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamilia = value
            End Set
        End Property

        Public Property FechaEmisionCompromisoPago() As Date
            Get
                Return dt_FechaEmisionCompromisoPago
            End Get
            Set(ByVal value As Date)
                dt_FechaEmisionCompromisoPago = value
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
        Sub New(ByVal CodigoCompromisoPago As Integer, _
                ByVal CodigoFamiliar As Integer, _
                ByVal CodigoFamilia As Integer, _
                ByVal FechaEmisionCompromisoPago As Date, _
                ByVal Estado As Integer)

            int_CodigoCompromisoPago = CodigoCompromisoPago
            int_CodigoFamiliar = CodigoFamiliar
            int_CodigoFamilia = CodigoFamilia
            dt_FechaEmisionCompromisoPago = FechaEmisionCompromisoPago
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace