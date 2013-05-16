Namespace ModuloMatricula
    Public Class be_DetalleIntegrantesFamilia

        'Actualizado 16/02/2012

#Region "Atributos"

        Private int_CodigoRelAlumnosFamiliares As Integer
        Private int_CodigoIntegranteFamilia As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoViveConAlumno As Integer
        Private int_CodigoFamiliarResponsablePago As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoRelAlumnosFamiliares() As Integer
            Get
                Return int_CodigoRelAlumnosFamiliares
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelAlumnosFamiliares = value
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

        Public Property CodigoFamiliarResponsablePago() As Integer
            Get
                Return int_CodigoFamiliarResponsablePago
            End Get
            Set(ByVal value As Integer)
                int_CodigoFamiliarResponsablePago = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.new()
        End Sub

        Sub New(ByVal CodigoRelAlumnosFamiliares As Integer, _
                ByVal CodigoIntegranteFamilia As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoViveConAlumno As Integer, _
                ByVal CodigoFamiliarResponsablePago As Integer)

            int_CodigoRelAlumnosFamiliares = CodigoRelAlumnosFamiliares
            int_CodigoIntegranteFamilia = CodigoIntegranteFamilia
            int_CodigoAlumno = CodigoAlumno
            int_CodigoViveConAlumno = CodigoViveConAlumno
            int_CodigoFamiliarResponsablePago = CodigoFamiliarResponsablePago

        End Sub

#End Region

    End Class
End Namespace

