Namespace ModuloPermisos
    Public Class be_BloquesInformaciones

#Region "Atributos"

        Private int_CodigoBloqueInformacion As Integer
        Private str_Descripcion As String
        Private str_CodigoGrupoProgramacion As String
        Private int_Tipo As Integer
        Private str_Entidad As String

#End Region

#Region "Propiedades"

        Public Property CodigoBloqueInformacion() As Integer
            Get
                Return int_CodigoBloqueInformacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoBloqueInformacion = value
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

        Public Property CodigoGrupoProgramacion() As String
            Get
                Return str_CodigoGrupoProgramacion
            End Get
            Set(ByVal value As String)
                str_CodigoGrupoProgramacion = value
            End Set
        End Property

        Public Property Tipo() As Integer
            Get
                Return int_Tipo
            End Get
            Set(ByVal value As Integer)
                int_Tipo = value
            End Set

        End Property

        Public Property Entidad() As String
            Get
                Return str_Entidad
            End Get
            Set(ByVal value As String)
                str_Entidad = value
            End Set

        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(CodigoBloqueInformacion As integer, _
                Descripcion As String, _
                CodigoGrupoProgramacion As String, _
                Tipo As Integer, _
                Entidad As String)

            int_CodigoBloqueInformacion = CodigoBloqueInformacion
            str_Descripcion = Descripcion
            str_CodigoGrupoProgramacion = CodigoGrupoProgramacion
            int_Tipo = Tipo
            str_Entidad = Entidad

        End Sub

#End Region

    End Class
End Namespace

