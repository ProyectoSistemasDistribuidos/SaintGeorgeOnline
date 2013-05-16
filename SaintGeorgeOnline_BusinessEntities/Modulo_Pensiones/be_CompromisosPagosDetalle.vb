
Namespace ModuloPensiones

    Public Class be_CompromisosPagosDetalle
#Region "Atributos"
        Private int_CodigoDetalleCompromisoPago As Integer
        Private int_CodigoCompromisoPago As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoDeuda As Integer
        Private dt_FechaPagoDeuda As Date
        Private int_estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoDetalleCompromisoPago() As Integer
            Get
                Return int_CodigoDetalleCompromisoPago
            End Get
            Set(ByVal value As Integer)
                int_CodigoDetalleCompromisoPago = value
            End Set
        End Property

        Public Property CodigoCompromisoPago() As Integer
            Get
                Return int_CodigoCompromisoPago
            End Get
            Set(ByVal value As Integer)
                int_CodigoCompromisoPago = value
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

        Public Property CodigoDeuda() As Integer
            Get
                Return int_CodigoDeuda
            End Get
            Set(ByVal value As Integer)
                int_CodigoDeuda = value
            End Set
        End Property

        Public Property FechaPagoDeuda() As Date
            Get
                Return dt_FechaPagoDeuda
            End Get
            Set(ByVal value As Date)
                dt_FechaPagoDeuda = value
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
        Sub New(ByVal CodigoDetalleCompromisoPago As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoCompromisoPago As String, _
                ByVal CodigoDeuda As Integer, _
                ByVal FechaPagoDeuda As Date, _
                ByVal Estado As Integer)

            int_CodigoDetalleCompromisoPago = CodigoDetalleCompromisoPago
            int_CodigoCompromisoPago = CodigoCompromisoPago
            str_CodigoAlumno = CodigoAlumno
            int_CodigoDeuda = CodigoDeuda
            dt_FechaPagoDeuda = FechaPagoDeuda
            int_estado = Estado
        End Sub

#End Region
    End Class

End Namespace