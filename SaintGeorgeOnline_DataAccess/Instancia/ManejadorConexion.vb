Namespace InstanciaConexion

    Public Class ManejadorConexion

        Private str_SqlConexionDB As String = Configuracion.CadenaConexionDB
        Private str_SqlConexionAdmisionDB As String = Configuracion.CadenaConexionAdmisionDB
        Private str_cadenaConeccionBDCSJ As String = Configuracion.CadenaConexionArticulos
        Public Sub New()
        End Sub





        Public ReadOnly Property SqlConexionDB() As String
            Get
                Return str_SqlConexionDB
            End Get
        End Property


        Public ReadOnly Property SqlConexionAdmisionDB() As String
            Get
                Return str_SqlConexionAdmisionDB
            End Get
        End Property


        Public ReadOnly Property SqlConexionArticulos() As String
            Get
                Return str_cadenaConeccionBDCSJ
            End Get
        End Property

    End Class

End Namespace