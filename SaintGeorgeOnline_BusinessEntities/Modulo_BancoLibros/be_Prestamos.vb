Namespace ModuloBancoLibros

    Public Class be_Prestamos

#Region "Atributos"

        Private int_CodigoPrestamo As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoAnio As Integer
        Private dt_FechaRegistro As Date
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoPrestamo() As Integer
            Get
                Return int_CodigoPrestamo
            End Get
            Set(ByVal value As Integer)
                int_CodigoPrestamo = value
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

        Public Property CodigoAnio() As Integer
            Get
                Return int_CodigoAnio
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnio = value
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
        Sub New(ByVal CodigoPrestamo As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoAnio As Integer, _
                ByVal FechaRegistro As Date, _
                ByVal Estado As Integer)

            int_CodigoPrestamo = CodigoPrestamo
            int_CodigoAlumno = CodigoAlumno
            int_CodigoAnio = CodigoAnio
            dt_FechaRegistro = FechaRegistro
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
