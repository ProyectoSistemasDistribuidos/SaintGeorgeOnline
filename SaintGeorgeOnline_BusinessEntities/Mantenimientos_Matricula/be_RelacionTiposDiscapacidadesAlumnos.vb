Namespace ModuloMatricula

    Public Class be_RelacionTiposDiscapacidadesAlumnos
#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoTipoDiscapacidad As Integer
        Private int_CodigoAlumno As Integer
        Private str_Descripcion As String

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

        Public Property CodigoTipoDiscapacidad() As Integer
            Get
                Return int_CodigoTipoDiscapacidad
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoDiscapacidad = value
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

        Public Property Descripcion() As String
            Get
                Return str_Descripcion
            End Get
            Set(ByVal value As String)
                str_Descripcion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoTipoDiscapacidad As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal Descripcion As String)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoTipoDiscapacidad = CodigoTipoDiscapacidad
            int_CodigoAlumno = CodigoAlumno
            str_Descripcion = Descripcion

        End Sub

#End Region
    End Class

End Namespace
