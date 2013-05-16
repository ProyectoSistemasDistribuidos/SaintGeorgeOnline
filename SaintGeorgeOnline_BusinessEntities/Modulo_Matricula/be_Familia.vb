Namespace ModuloMatricula
    Public Class be_Familia

#Region "Atributos"

        Private int_CodigoFamilia As Integer
        Private int_CodigoIntegranteFamilia As Integer
        Private int_CodigoFamiliar As Integer
        Private int_CodigoParentesco As Integer
        Private int_CodigoRelAlumnosFamiliares As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoViveConAlumno As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoFamilia() As Integer
            Get
                Return int_CodigoFamilia
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamilia = value
            End Set
        End Property

        Public Property CodigoIntegranteFamilia() As Integer
            Get
                Return int_CodigoIntegranteFamilia
            End Get
            Set(ByVal value As Integer)
                int_CodigoIntegranteFamilia = value
            End Set
        End Property

        Public Property CodigoFamiliar() As Integer
            Get
                Return int_CodigoFamiliar
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliar = value
            End Set
        End Property

        Public Property CodigoParentesco() As Integer
            Get
                Return int_CodigoParentesco
            End Get
            Set(ByVal value As Integer)
                int_CodigoParentesco = value
            End Set
        End Property

        Public Property CodigoRelAlumnosFamiliares() As Integer
            Get
                Return int_CodigoRelAlumnosFamiliares
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelAlumnosFamiliares = value
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

        Public Property CodigoViveConAlumno() As Integer
            Get
                Return int_CodigoViveConAlumno
            End Get
            Set(ByVal value As Integer)
                int_CodigoViveConAlumno = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Sub New()
            MyBase.new()
        End Sub

#End Region

    End Class
End Namespace

