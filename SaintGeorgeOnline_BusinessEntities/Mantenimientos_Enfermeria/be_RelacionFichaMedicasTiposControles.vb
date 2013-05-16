Namespace ModuloEnfermeria

    Public Class be_RelacionFichaMedicasTiposControles

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoTipoControl As Integer
        Private dt_FechaControl As Date
        Private str_Resultado As String

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

        Public Property CodigoTipoControl() As Integer
            Get
                Return int_CodigoTipoControl
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoControl = value
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

        Public Property Resultado() As String
            Get
                Return str_Resultado
            End Get
            Set(ByVal value As String)
                str_Resultado = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoTipoControl As Integer, _
                ByVal FechaHospitalizacion As Date, _
                ByVal Resultado As String)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoAlumno = CodigoAlumno
            int_CodigoTipoControl = CodigoTipoControl
            dt_FechaControl = FechaControl
            str_Resultado = Resultado

        End Sub
#End Region

    End Class

End Namespace
