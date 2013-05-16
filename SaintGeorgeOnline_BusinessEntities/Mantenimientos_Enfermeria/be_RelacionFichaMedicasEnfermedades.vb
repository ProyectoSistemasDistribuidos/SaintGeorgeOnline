Namespace ModuloEnfermeria

    Public Class be_RelacionFichaMedicasEnfermedades

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoEnfermedad As Integer
        'Private str_Enfermedad As String
        Private int_Edad As Integer
        'Private int_Estado As Integer

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

        Public Property CodigoEnfermedad() As Integer
            Get
                Return int_CodigoEnfermedad
            End Get
            Set(ByVal value As Integer)
                int_CodigoEnfermedad = value
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

        'Public Property Enfermedad() As String
        '    Get
        '        Return str_Enfermedad
        '    End Get
        '    Set(ByVal value As String)
        '        str_Enfermedad = value
        '    End Set
        'End Property

        'Public Property Estado() As Integer
        '    Get
        '        Return int_Estado
        '    End Get
        '    Set(ByVal value As Integer)
        '        int_Estado = value
        '    End Set
        'End Property

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoEnfermedad As Integer, _
                ByVal Edad As Integer)

            int_CodigoRelacion = CodigoRelacion
            int_CodigoAlumno = CodigoAlumno
            int_CodigoEnfermedad = CodigoEnfermedad
            int_Edad = Edad

        End Sub
#End Region

    End Class

End Namespace
