Namespace ModuloEnfermeria

    Public Class be_RelacionFichaMedicasOperaciones

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoTipoOperaciones As Integer
        Private dt_FechaOperacion As Date

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

        Public Property CodigoTipoOperaciones() As Integer
            Get
                Return int_CodigoTipoOperaciones
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoOperaciones = value
            End Set
        End Property

        Public Property FechaOperacion() As Date
            Get
                Return dt_FechaOperacion
            End Get
            Set(ByVal value As Date)
                dt_FechaOperacion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoTipoOperaciones As Integer, _
                ByVal FechaOperacion As Date)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoAlumno = CodigoAlumno
            int_CodigoTipoOperaciones = CodigoTipoOperaciones
            dt_FechaOperacion = FechaOperacion

        End Sub
#End Region

    End Class

End Namespace