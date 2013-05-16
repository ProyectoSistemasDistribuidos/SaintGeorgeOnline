Namespace ModuloTareas

    Public Class be_TareaAcademica

#Region "Atributos"

        Private int_CodigoTareaAcademica As Integer
        Private int_CodigoAsignacionGrupo As Integer
        Private int_CodigoTipoTarea As Integer
        Private str_Descripcion As String
        Private dt_FechaPublicacion As Date
        Private dt_FechaVencimiento As Date
        Private int_Estado As Integer

#End Region

#Region "Propiedades"

        Public Property CodigoTareaAcademica() As Integer
            Get
                Return int_CodigoTareaAcademica
            End Get
            Set(ByVal value As Integer)
                int_CodigoTareaAcademica = value
            End Set
        End Property

        Public Property CodigoAsignacionGrupo() As Integer
            Get
                Return int_CodigoAsignacionGrupo
            End Get
            Set(ByVal value As Integer)
                int_CodigoAsignacionGrupo = value
            End Set
        End Property

        Public Property CodigoTipoTarea() As Integer
            Get
                Return int_CodigoTipoTarea
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoTarea = value
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

        Public Property FechaPublicacion() As Date
            Get
                Return dt_FechaPublicacion
            End Get
            Set(ByVal value As Date)
                dt_FechaPublicacion = value
            End Set
        End Property

        Public Property FechaVencimiento() As Date
            Get
                Return dt_FechaVencimiento
            End Get
            Set(ByVal value As Date)
                dt_FechaVencimiento = value
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

        Sub New(ByVal CodigoTareaAcademica As Integer, _
                ByVal CodigoAsignacionGrupo As Integer, _
                ByVal CodigoTipoTarea As Integer, _
                ByVal Descripcion As String, _
                ByVal FechaPublicacion As Date, _
                ByVal FechaVencimiento As Date, _
                ByVal Estado As Integer)

            int_CodigoTareaAcademica = CodigoTareaAcademica
            int_CodigoAsignacionGrupo = CodigoAsignacionGrupo
            int_CodigoTipoTarea = CodigoTipoTarea
            str_Descripcion = Descripcion
            dt_FechaPublicacion = FechaPublicacion
            dt_FechaVencimiento = FechaVencimiento
            int_Estado = Estado

        End Sub

#End Region

    End Class

End Namespace