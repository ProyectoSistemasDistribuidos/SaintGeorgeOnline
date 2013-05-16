Namespace ModuloPermisos
    Public Class be_BloqueoFichasAtenciones

#Region "Atributos"

        Private int_CodigoBloqueo As Integer
        Private int_DiasModificacion As Integer
        Private int_OmisionFichaPendiente As Integer
        Private int_CodigoExclusion As Integer
        Private int_CodigoFichaMedicaAtencion As Integer
        Private int_DiasExclusion As Integer
        Private int_Estado As Integer
        Private int_Terminado As Integer
        Private date_FechaRegistroExcepcion As Date

#End Region

#Region "Propiedades"

        Public Property CodigoBloqueo() As Integer
            Get
                Return int_CodigoBloqueo
            End Get
            Set(ByVal value As Integer)
                int_CodigoBloqueo = value
            End Set
        End Property

        Public Property DiasModificacion() As Integer
            Get
                Return int_DiasModificacion
            End Get
            Set(ByVal value As Integer)
                int_DiasModificacion = value
            End Set
        End Property

        Public Property OmisionFichaPendiente() As Integer
            Get
                Return int_OmisionFichaPendiente
            End Get
            Set(ByVal value As Integer)
                int_OmisionFichaPendiente = value
            End Set
        End Property

        Public Property CodigoExclusion() As Integer
            Get
                Return int_CodigoExclusion
            End Get
            Set(ByVal value As Integer)
                int_CodigoExclusion = value
            End Set
        End Property

        Public Property CodigoFichaMedicaAtencion() As Integer
            Get
                Return int_CodigoFichaMedicaAtencion
            End Get
            Set(ByVal value As Integer)
                int_CodigoFichaMedicaAtencion = value
            End Set
        End Property

        Public Property DiasExclusion() As Integer
            Get
                Return int_DiasExclusion
            End Get
            Set(ByVal value As Integer)
                int_DiasExclusion = value
            End Set
        End Property

        Public Property Terminado() As Integer
            Get
                Return int_Terminado
            End Get
            Set(ByVal value As Integer)
                int_Terminado = value
            End Set
        End Property

        Public Property FechaRegistroExcepcion() As Date
            Get
                Return date_FechaRegistroExcepcion
            End Get
            Set(ByVal value As Date)
                date_FechaRegistroExcepcion = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

#End Region

    End Class
End Namespace

