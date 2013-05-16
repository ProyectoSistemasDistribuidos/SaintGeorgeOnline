Namespace ModuloPensiones

    Public Class be_AsignacionBecas
 
#Region "Atributos"

        Private int_CodigoAsignacionBeca As Integer
        Private int_CodigoAlumno As Integer
        Private int_CodigoMotivoBeca As Integer
        Private int_CodigoTipoBeca As Integer
        Private int_CodigoAnioAcademico As Integer
        Private int_MesIni As Integer
        Private int_MesFin As Integer
        Private int_NumExp As Integer
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoAsignacionBeca() As Integer
            Get
                Return int_CodigoAsignacionBeca
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionBeca = value
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

        Public Property CodigoMotivoBeca() As Integer
            Get
                Return int_CodigoMotivoBeca
            End Get
            Set(ByVal value As Integer)
                int_CodigoMotivoBeca = value
            End Set
        End Property

        Public Property CodigoTipoBeca() As Integer
            Get
                Return int_CodigoTipoBeca
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoBeca = value
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

        Public Property MesIni() As Integer
            Get
                Return int_MesIni
            End Get
            Set(ByVal value As Integer)
                int_MesIni = value
            End Set
        End Property

        Public Property MesFin() As Integer
            Get
                Return int_MesFin
            End Get
            Set(ByVal value As Integer)
                int_MesFin = value
            End Set
        End Property

        Public Property NumExp() As Integer
            Get
                Return int_NumExp
            End Get
            Set(ByVal value As Integer)
                int_NumExp = value
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

        Sub New(ByVal CodigoAsignacionBeca As Integer, _
                ByVal CodigoAlumno As Integer, _
                ByVal CodigoMotivoBeca As Integer, _
                ByVal CodigoTipoBeca As Integer, _
                ByVal CodigoAnioAcademico As Integer, _
                ByVal MesIni As Integer, _
                ByVal MesFin As Integer, _
                ByVal NumExp As Integer, _
                ByVal Estado As Integer)

            int_CodigoAsignacionBeca = CodigoAsignacionBeca
            int_CodigoAlumno = CodigoAlumno
            int_CodigoMotivoBeca = CodigoMotivoBeca
            int_CodigoTipoBeca = CodigoTipoBeca
            int_CodigoAnioAcademico = CodigoAnioAcademico
            int_MesIni = MesIni
            int_MesFin = MesFin
            int_NumExp = NumExp
            int_Estado = Estado
        End Sub

#End Region

    End Class

End Namespace