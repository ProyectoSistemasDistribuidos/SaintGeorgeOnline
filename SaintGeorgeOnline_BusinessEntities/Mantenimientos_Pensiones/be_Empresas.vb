Namespace ModuloPensiones

    Public Class be_Empresas

#Region "Atributos"
        Private int_CodigoEmpresa As Integer
        Private str_CodigoUbigeo As String
        Private str_RazonSocial As String
        Private str_NombreComercial As String
        Private str_Direccion As String
        Private str_Ruc As String
        Private str_Telefono As String
        Private str_Celular As String
        Private str_Fax As String
        Private str_Email As String
        Private int_Estado As Integer
#End Region

#Region "Propiedades"

        Public Property CodigoEmpresa() As Integer
            Get
                Return int_CodigoEmpresa
            End Get
            Set(ByVal value As Integer)
                int_CodigoEmpresa = value
            End Set
        End Property

        Public Property CodigoUbigeo() As String
            Get
                Return str_CodigoUbigeo
            End Get
            Set(ByVal value As String)
                str_CodigoUbigeo = value
            End Set
        End Property

        Public Property RazonSocial() As String
            Get
                Return str_RazonSocial
            End Get
            Set(ByVal value As String)
                str_RazonSocial = value
            End Set
        End Property

        Public Property NombreComercial() As String
            Get
                Return str_NombreComercial
            End Get
            Set(ByVal value As String)
                str_NombreComercial = value
            End Set
        End Property

        Public Property Direccion() As String
            Get
                Return str_Direccion
            End Get
            Set(ByVal value As String)
                str_Direccion = value
            End Set
        End Property

        Public Property Ruc() As String
            Get
                Return str_Ruc
            End Get
            Set(ByVal value As String)
                str_Ruc = value
            End Set
        End Property

        Public Property Telefono() As String
            Get
                Return str_Telefono
            End Get
            Set(ByVal value As String)
                str_Telefono = value
            End Set
        End Property

        Public Property Celular() As String
            Get
                Return str_Celular
            End Get
            Set(ByVal value As String)
                str_Celular = value
            End Set
        End Property

        Public Property Fax() As String
            Get
                Return str_Fax
            End Get
            Set(ByVal value As String)
                str_Fax = value
            End Set
        End Property

        Public Property Email() As String
            Get
                Return str_Email
            End Get
            Set(ByVal value As String)
                str_Email = value
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

        Sub New(ByVal CodigoEmpresa As Integer, _
                ByVal CodigoUbigeo As String, _
                ByVal RazonSocial As String, _
                ByVal NombreComercial As String, _
                ByVal Direccion As String, _
                ByVal Ruc As String, _
                ByVal Telefono As String, _
                ByVal Celular As String, _
                ByVal Fax As String, _
                ByVal Email As String, _
                ByVal Estado As Integer)
            int_CodigoEmpresa = CodigoEmpresa
            str_CodigoUbigeo = CodigoUbigeo
            str_RazonSocial = RazonSocial
            str_NombreComercial = NombreComercial
            str_Direccion = Direccion
            str_Ruc = Ruc
            str_Telefono = Telefono
            str_Celular = Celular
            str_Fax = Fax
            str_Email = Email
            int_Estado = Estado
        End Sub

#End Region

    End Class
End Namespace

