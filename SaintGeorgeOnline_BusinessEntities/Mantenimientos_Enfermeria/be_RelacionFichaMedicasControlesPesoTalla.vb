Namespace ModuloEnfermeria

    Public Class be_RelacionFichaMedicasControlesPesoTalla

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoAlumno As Integer
        Private dec_Talla As Decimal
        Private dec_Peso As Decimal
        Private dt_FechaControl As Date
        Private str_Observaciones As String

#End Region

#Region "Propiedades"

        Public Property CodigoRelacion() As Integer
            Get
                Return int_CodigoRelacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacion = value
            End Set
        End Property

        Public Property CodigoAlumno() As Integer
            Get
                Return int_CodigoAlumno
            End Get
            Set(ByVal value As Integer)
                int_CodigoAlumno = value
            End Set
        End Property

        Public Property Talla() As Decimal
            Get
                Return dec_Talla
            End Get
            Set(ByVal value As Decimal)
                dec_Talla = value
            End Set
        End Property

        Public Property Peso() As Decimal
            Get
                Return dec_Peso
            End Get
            Set(ByVal value As Decimal)
                dec_Peso = value
            End Set
        End Property

        Public Property FechaControl() As Date
            Get
                Return dt_FechaControl
            End Get
            Set(ByVal value As Date)
                dt_FechaControl = value
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

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal Talla As Decimal, _
                ByVal Peso As Decimal, _
                ByVal FechaControl As Date, _
                ByVal Observaciones As String)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoAlumno = CodigoAlumno
            dec_Talla = Talla
            dec_Peso = Peso
            dt_FechaControl = FechaControl
            str_Observaciones = Observaciones

        End Sub
#End Region

    End Class

End Namespace
