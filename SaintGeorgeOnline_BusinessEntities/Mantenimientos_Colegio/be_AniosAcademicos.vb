Namespace ModuloColegio

    Public Class be_AniosAcademicos

#Region "Atributos"

        Private int_CodigoAnioAcademico As Integer
        Private int_Descripcion As Integer
        Private dt_FechaInicio As Date
        Private dt_FechaFin As Date
        Private int_Vigencia As Integer
        Private int_CodigoEstadoAnioAcademico As Integer
        Private int_Estado As Integer


#End Region

#Region "Propiedades"

        Public Property CodigoAnioAcademico() As Integer
            Get
                Return int_CodigoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnioAcademico = value
            End Set
        End Property

        Public Property CodigoEstadoAnioAcademico() As Integer
            Get
                Return int_CodigoEstadoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoEstadoAnioAcademico = value
            End Set
        End Property

        Public Property Descripcion() As Integer
            Get
                Return int_Descripcion
            End Get
            Set(ByVal value As Integer)
                int_Descripcion = value
            End Set
        End Property

        Public Property FechaInicio() As Date
            Get
                Return dt_FechaInicio
            End Get
            Set(ByVal value As Date)
                dt_FechaInicio = value
            End Set
        End Property

        Public Property FechaFin() As Date
            Get
                Return dt_FechaFin
            End Get
            Set(ByVal value As Date)
                dt_FechaFin = value
            End Set
        End Property

        Public Property Vigencia() As Integer
            Get
                Return int_Vigencia
            End Get
            Set(ByVal value As Integer)
                int_Vigencia = value
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
        Sub New(ByVal CodigoAnioAcademico As Integer, _
                ByVal Descripcion As Integer, _
                ByVal dt_FechaInicio As Date, _
                ByVal dt_FechaFin As Date, _
                ByVal Vigencia As Integer, _
                ByVal CodigoEstadoAnioAcademico As Integer, _
                ByVal Estado As Integer)

            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_Descripcion = Descripcion
            dt_FechaInicio = FechaInicio
            dt_FechaFin = FechaFin
            int_Vigencia = Vigencia
            int_CodigoEstadoAnioAcademico = CodigoEstadoAnioAcademico
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace
