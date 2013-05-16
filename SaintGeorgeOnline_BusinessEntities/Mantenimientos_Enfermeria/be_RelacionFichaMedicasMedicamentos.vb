Namespace ModuloEnfermeria

    Public Class be_RelacionFichaMedicasMedicamentos

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoMedicamento As Integer
        Private dt_FechaRegistro As Date
        Private int_CodigoPresentacion As Integer
        Private str_CantidadPresentacion As String
        Private str_DosisMedicamento As String
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

        Public Property CodigoMedicamento() As Integer
            Get
                Return int_CodigoMedicamento
            End Get
            Set(ByVal value As Integer)
                int_CodigoMedicamento = value
            End Set
        End Property

        Public Property CodigoPresentacion() As Integer
            Get
                Return int_CodigoPresentacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoPresentacion = value
            End Set
        End Property

        Public Property FechaRegistro() As Date
            Get
                Return dt_FechaRegistro
            End Get
            Set(ByVal value As Date)
                dt_FechaRegistro = value
            End Set
        End Property

        Public Property CantidadPresentacion() As String
            Get
                Return str_CantidadPresentacion
            End Get
            Set(ByVal value As String)
                str_CantidadPresentacion = value
            End Set
        End Property

        Public Property DosisMedicamento() As String
            Get
                Return str_DosisMedicamento
            End Get
            Set(ByVal value As String)
                str_DosisMedicamento = value
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
                ByVal CodigoMedicamento As Integer, _
                ByVal CodigoPresentacion As Integer, _
                ByVal FechaRegistro As Date, _
                ByVal CantidadPresentacion As String, _
                ByVal DosisMedicamento As String, _
                ByVal Observaciones As String)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoAlumno = CodigoAlumno
            int_CodigoMedicamento = CodigoMedicamento
            dt_FechaRegistro = FechaRegistro
            int_CodigoPresentacion = CodigoPresentacion
            str_CantidadPresentacion = CantidadPresentacion
            str_DosisMedicamento = DosisMedicamento
            str_Observaciones = Observaciones


        End Sub
#End Region

    End Class

End Namespace
