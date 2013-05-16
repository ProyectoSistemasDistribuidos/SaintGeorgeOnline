Namespace ModuloEnfermeria

    Public Class be_RelacionFichaMedicasCarecteristicasPiel

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoCaracteristicapiel As Integer
        Private dt_FechaRegistro As Date

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

        Public Property CodigoCaracteristicapiel() As Integer
            Get
                Return int_CodigoCaracteristicapiel
            End Get
            Set(ByVal value As Integer)
                int_CodigoCaracteristicapiel = value
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

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoCodigoVacuna As Integer, _
                ByVal FechaVacunacion As Date)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoAlumno = CodigoAlumno
            int_CodigoCaracteristicapiel = CodigoCaracteristicapiel
            dt_FechaRegistro = FechaRegistro

        End Sub
#End Region

    End Class

End Namespace
