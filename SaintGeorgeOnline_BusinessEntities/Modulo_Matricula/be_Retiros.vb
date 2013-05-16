Namespace ModuloMatricula

    Public Class be_Retiros

#Region "Atributos"

        Private int_CodigoRetiro As Integer
        Private dt_FechaRetiro As Date 'String
        Private int_CodigoMotivoRetiro As Integer
        Private int_CodigoAnioAcademico As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoColegio As Integer
        Private str_Observacion As String

#End Region

#Region "Propiedades"

        Public Property CodigoRetiro() As Integer
            Get
                Return int_CodigoRetiro
            End Get
            Set(ByVal value As Integer)
                int_CodigoRetiro = value
            End Set
        End Property

        'Public Property FechaRetiro() As Date
        '    Get
        '        Return dt_FechaRetiro
        '    End Get
        '    Set(ByVal value As Date)
        '        dt_FechaRetiro = value
        '    End Set
        'End Property
        Public Property FechaRetiro() As Date
            Get
                Return dt_FechaRetiro
            End Get
            Set(ByVal value As Date)
                dt_FechaRetiro = value
            End Set
        End Property

        Public Property CodigoAnioAcademico() As Integer
            Get
                Return int_CodigoAnioAcademico
            End Get
            Set(ByVal value As Integer)
                int_CodigoAnioAcademico = value
            End Set
        End Property

        Public Property CodigoMotivoRetiro() As Integer
            Get
                Return int_CodigoMotivoRetiro
            End Get
            Set(ByVal value As Integer)
                int_CodigoMotivoRetiro = value
            End Set
        End Property

        Public Property CodigoAlumno() As String
            Get
                Return str_CodigoAlumno
            End Get
            Set(ByVal value As String)
                str_CodigoAlumno = value
            End Set
        End Property

        Public Property CodigoColegio() As Integer
            Get
                Return int_CodigoColegio
            End Get
            Set(ByVal value As Integer)
                int_CodigoColegio = value
            End Set
        End Property

        Public Property Observacion() As String
            Get
                Return str_Observacion
            End Get
            Set(ByVal value As String)
                str_Observacion = value
            End Set
        End Property

       
#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoRetiro As Integer, _
                ByVal FechaRetiro As Date, _
                ByVal CodigoMotivoRetiro As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoColegio As Integer, _
                ByVal Observacion As String)

            int_CodigoRetiro = CodigoRetiro
            dt_FechaRetiro = FechaRetiro
            int_CodigoMotivoRetiro = CodigoMotivoRetiro
            int_CodigoAnioAcademico = CodigoAnioAcademico
            str_CodigoAlumno = CodigoAlumno
            int_CodigoColegio = CodigoColegio
            str_Observacion = Observacion

        End Sub

#End Region

    End Class

End Namespace