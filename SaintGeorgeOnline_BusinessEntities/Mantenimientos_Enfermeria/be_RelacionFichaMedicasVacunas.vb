Namespace ModuloEnfermeria

    Public Class be_RelacionFichaMedicasVacunas

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoVacuna As Integer
        Private int_CodigoDosis As Integer
        Private int_Edad As Integer
        Private dt_FechaVacunacion As Date

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

        Public Property CodigoVacuna() As Integer
            Get
                Return int_CodigoVacuna
            End Get
            Set(ByVal value As Integer)
                int_CodigoVacuna = value
            End Set
        End Property

        Public Property CodigoDosis() As Integer
            Get
                Return int_CodigoDosis
            End Get
            Set(ByVal value As Integer)
                int_CodigoDosis = value
            End Set
        End Property

        Public Property Edad() As Integer
            Get
                Return int_Edad
            End Get
            Set(ByVal value As Integer)
                int_Edad = value
            End Set
        End Property

        Public Property FechaVacunacion() As Date
            Get
                Return dt_FechaVacunacion
            End Get
            Set(ByVal value As Date)
                dt_FechaVacunacion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoCodigoVacuna As Integer, _
                ByVal CodigoDosis As Integer, _
                ByVal FechaVacunacion As Date)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoAlumno = CodigoAlumno
            int_CodigoVacuna = CodigoVacuna
            int_CodigoDosis = CodigoDosis
            dt_FechaVacunacion = FechaVacunacion

        End Sub
#End Region

    End Class

End Namespace
