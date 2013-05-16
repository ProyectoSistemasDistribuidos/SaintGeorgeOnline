Namespace ModuloEnfermeria

    Public Class be_RelacionFichaMedicasMotivoHospitalizacion

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoMotivoHospitalizacion As Integer
        Private dt_FechaHospitalizacion As Date

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

        Public Property CodigoMotivoHospitalizacion() As Integer
            Get
                Return int_CodigoMotivoHospitalizacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoMotivoHospitalizacion = value
            End Set
        End Property

        Public Property FechaHospitalizacion() As Date
            Get
                Return dt_FechaHospitalizacion
            End Get
            Set(ByVal value As Date)
                dt_FechaHospitalizacion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoMotivoHospitalizacion As Integer, _
                ByVal FechaHospitalizacion As Date)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoAlumno = CodigoAlumno
            int_CodigoMotivoHospitalizacion = CodigoMotivoHospitalizacion
            dt_FechaHospitalizacion = FechaHospitalizacion

        End Sub
#End Region

    End Class

End Namespace
