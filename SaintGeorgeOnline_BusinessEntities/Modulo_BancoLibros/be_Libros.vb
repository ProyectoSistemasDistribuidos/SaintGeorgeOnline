Namespace ModuloBancoLibros

    Public Class be_Libros

#Region "Atributos"
        'ok
        Private int_CodigoLibro As Integer
        Private int_CodigoIdioma As Integer
        Private str_Titulo As String
        Private str_Editorial As String
        Private str_Coleccion As String
        Private str_Nivel As String
        Private str_Autor As String
        Private str_ISBN As String
        Private int_NumeroPaginas As Integer
        Private int_NumeroCopia As Integer
        Private dc_PrecioLibro As Decimal
        Private int_CodigoMonedaPrecioLibro As Integer
        Private dc_PrecioPrestamo As Decimal
        Private int_CodigoMonedaPrecioPrestamo As Integer
        Private dc_PrecioReposicion As Decimal
        Private int_CodigoMonedaPrecioReposicion As Integer
        Private str_RutaTapa As String
        Private int_Estado As Integer
        Private dc_Largo As Decimal
        Private dc_Ancho As Decimal
        Private dc_Grosor As Decimal
        Private int_CodigoTipoLibro As Integer
        Private int_CodigoCurso As Integer
        Private str_Edicion As String
        Private int_Sede As Integer
#End Region

#Region "Propiedades"
        Public Property Sede() As Integer
            Get
                Return int_Sede
            End Get
            Set(ByVal value As Integer)
                int_Sede = value
            End Set
        End Property

        Public Property CodigoLibro() As Integer
            Get
                Return int_CodigoLibro
            End Get
            Set(ByVal value As Integer)
                int_CodigoLibro = value
            End Set
        End Property

        Public Property CodigoIdioma() As Integer
            Get
                Return int_CodigoIdioma
            End Get
            Set(ByVal value As Integer)
                int_CodigoIdioma = value
            End Set
        End Property

        Public Property Titulo() As String
            Get
                Return str_Titulo
            End Get
            Set(ByVal value As String)
                str_Titulo = value
            End Set
        End Property

        Public Property Editorial() As String
            Get
                Return str_Editorial
            End Get
            Set(ByVal value As String)
                str_Editorial = value
            End Set
        End Property

        Public Property Coleccion() As String
            Get
                Return str_Coleccion
            End Get
            Set(ByVal value As String)
                str_Coleccion = value
            End Set
        End Property

        Public Property Nivel() As String
            Get
                Return str_Nivel
            End Get
            Set(ByVal value As String)
                str_Nivel = value
            End Set
        End Property

        Public Property Autor() As String
            Get
                Return str_Autor
            End Get
            Set(ByVal value As String)
                str_Autor = value
            End Set
        End Property

        Public Property ISBN() As String
            Get
                Return str_ISBN
            End Get
            Set(ByVal value As String)
                str_ISBN = value
            End Set
        End Property

        Public Property NumeroPaginas() As Integer
            Get
                Return int_NumeroPaginas
            End Get
            Set(ByVal value As Integer)
                int_NumeroPaginas = value
            End Set
        End Property

        Public Property NumeroCopia() As Integer
            Get
                Return int_NumeroCopia
            End Get
            Set(ByVal value As Integer)
                int_NumeroCopia = value
            End Set
        End Property

        Public Property PrecioLibro() As Decimal
            Get
                Return dc_PrecioLibro
            End Get
            Set(ByVal value As Decimal)
                dc_PrecioLibro = value
            End Set
        End Property

        Public Property CodigoMonedaPrecioLibro() As Integer
            Get
                Return int_CodigoMonedaPrecioLibro
            End Get
            Set(ByVal value As Integer)
                int_CodigoMonedaPrecioLibro = value
            End Set
        End Property

        Public Property PrecioPrestamo() As Decimal
            Get
                Return dc_PrecioPrestamo
            End Get
            Set(ByVal value As Decimal)
                dc_PrecioPrestamo = value
            End Set
        End Property

        Public Property CodigoMonedaPrecioPrestamo() As Integer
            Get
                Return int_CodigoMonedaPrecioPrestamo
            End Get
            Set(ByVal value As Integer)
                int_CodigoMonedaPrecioPrestamo = value
            End Set
        End Property

        Public Property PrecioReposicion() As Decimal
            Get
                Return dc_PrecioReposicion
            End Get
            Set(ByVal value As Decimal)
                dc_PrecioReposicion = value
            End Set
        End Property

        Public Property CodigoMonedaPrecioReposicion() As Integer
            Get
                Return int_CodigoMonedaPrecioReposicion
            End Get
            Set(ByVal value As Integer)
                int_CodigoMonedaPrecioReposicion = value
            End Set
        End Property

        Public Property RutaTapa() As String
            Get
                Return str_RutaTapa
            End Get
            Set(ByVal value As String)
                str_RutaTapa = value
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

        Public Property Largo() As Decimal
            Get
                Return dc_Largo
            End Get
            Set(ByVal value As Decimal)
                dc_Largo = value
            End Set
        End Property

        Public Property Ancho() As Decimal
            Get
                Return dc_Ancho
            End Get
            Set(ByVal value As Decimal)
                dc_Ancho = value
            End Set
        End Property

        Public Property Grosor() As Decimal
            Get
                Return dc_Grosor
            End Get
            Set(ByVal value As Decimal)
                dc_Grosor = value
            End Set
        End Property

        Public Property CodigoTipoLibro() As Integer
            Get
                Return int_CodigoTipoLibro
            End Get
            Set(ByVal value As Integer)
                int_CodigoTipoLibro = value
            End Set
        End Property

        Public Property CodigoCurso() As Integer
            Get
                Return int_CodigoCurso
            End Get
            Set(ByVal value As Integer)
                int_CodigoCurso = value
            End Set
        End Property

        Public Property Edicion() As String
            Get
                Return str_Edicion
            End Get
            Set(ByVal value As String)
                str_Edicion = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoLibro As Integer, _
                ByVal CodigoIdioma As Integer, _
                ByVal Titulo As String, _
                ByVal Editorial As Integer, _
                ByVal Autor As Integer, _
                ByVal Coleccion As String, _
                ByVal Nivel As String, _
                ByVal ISBN As String, _
                ByVal NumeroPaginas As Integer, _
                ByVal NumeroCopia As Integer, _
                ByVal PrecioLibro As Decimal, _
                ByVal CodigoMonedaPrecioLibro As Integer, _
                ByVal PrecioPrestamo As Decimal, _
                ByVal CodigoMonedaPrecioPrestamo As Integer, _
                ByVal PrecioReposicion As Decimal, _
                ByVal CodigoMonedaPrecioReposicion As Integer, _
                ByVal RutaTapa As String, _
                ByVal Estado As Integer, _
                ByVal Largo As Decimal, _
                ByVal Ancho As Decimal, _
                ByVal Grosor As Decimal, _
                ByVal CodigoTipoLibro As Integer, _
                ByVal CodigoCurso As Integer, _
                ByVal Edicion As String, _
                ByVal Sede As Integer)


            int_CodigoLibro = CodigoLibro
            int_CodigoIdioma = CodigoIdioma
            str_Titulo = Titulo
            str_Editorial = Editorial
            str_Coleccion = Coleccion
            str_Nivel = Nivel
            str_Autor = Autor
            str_ISBN = ISBN
            int_NumeroPaginas = NumeroPaginas
            int_NumeroCopia = NumeroCopia
            dc_PrecioLibro = PrecioLibro
            int_CodigoMonedaPrecioLibro = CodigoMonedaPrecioLibro
            dc_PrecioPrestamo = PrecioPrestamo
            int_CodigoMonedaPrecioPrestamo = CodigoMonedaPrecioPrestamo
            dc_PrecioReposicion = PrecioReposicion
            int_CodigoMonedaPrecioReposicion = CodigoMonedaPrecioReposicion
            str_RutaTapa = RutaTapa
            int_Estado = Estado
            dc_Largo = Largo
            dc_Ancho = Ancho
            dc_Grosor = Grosor
            int_CodigoTipoLibro = CodigoTipoLibro
            int_CodigoCurso = CodigoCurso
            str_Edicion = Edicion
            int_Sede = Sede
        End Sub

#End Region


    End Class

End Namespace