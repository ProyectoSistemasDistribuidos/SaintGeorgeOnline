Namespace ModuloMatricula

    Public Class be_RelacionAlumnosFamiliares

#Region "Atributos"

        Private int_CodigoRelacion As Integer
        Private str_CodigoAlumno As String
        Private int_CodigoFamiliar As Integer
        Private int_CodigoParentesco As Integer
        Private int_Apoderado As Integer
        Private int_ResponsablePago As Integer
        Private int_ViveConAlumno As Integer
        Private int_EmitirFactura As Integer
        Private str_FacturaRazonSocial As String
        Private str_FacturaRUC As String
        Private str_FacturaDireccionEmpresa As String

#End Region

#Region "Propiedades"

        Public Property CodigoRelacion() As Integer
            Get
                Return int_CodigoRelacion
            End Get
            Set(ByVal value As Integer)
                int_CodigoRelacion = value
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

        Public Property Apoderado() As Integer
            Get
                Return int_Apoderado
            End Get
            Set(ByVal value As Integer)
                int_Apoderado = value
            End Set
        End Property

        Public Property ResponsablePago() As Integer
            Get
                Return int_ResponsablePago
            End Get
            Set(ByVal value As Integer)
                int_ResponsablePago = value
            End Set
        End Property

        Public Property ViveConAlumno() As Integer
            Get
                Return int_ViveConAlumno
            End Get
            Set(ByVal value As Integer)
                int_ViveConAlumno = value
            End Set
        End Property

        Public Property EmitirFactura() As Integer
            Get
                Return int_EmitirFactura
            End Get
            Set(ByVal value As Integer)
                int_EmitirFactura = value
            End Set
        End Property

        Public Property FacturaRazonSocial() As String
            Get
                Return str_FacturaRazonSocial
            End Get
            Set(ByVal value As String)
                str_FacturaRazonSocial = value
            End Set
        End Property

        Public Property FacturaRUC() As String
            Get
                Return str_FacturaRUC
            End Get
            Set(ByVal value As String)
                str_FacturaRUC = value
            End Set
        End Property

        Public Property FacturaDireccionEmpresa() As String
            Get
                Return str_FacturaDireccionEmpresa
            End Get
            Set(ByVal value As String)
                str_FacturaDireccionEmpresa = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()

            MyBase.New()

        End Sub

        Sub New(ByVal CodigoRelacion As Integer, _
                ByVal CodigoAlumno As String, _
                ByVal CodigoFamiliar As Integer, _
                ByVal CodigoParentesco As Integer, _
                ByVal Apoderado As Integer, _
                ByVal ResponsablePago As Integer, _
                ByVal ViveConAlumno As Integer, _
                ByVal EmitirFactura As Integer, _
                ByVal FacturaRazonSocial As String, _
                ByVal FacturaRUC As String, _
                ByVal FacturaDireccionEmpresa As String)

            int_CodigoRelacion = CodigoRelacion
            str_CodigoAlumno = CodigoAlumno
            int_CodigoFamiliar = CodigoFamiliar
            int_CodigoParentesco = CodigoParentesco
            int_Apoderado = Apoderado
            int_ResponsablePago = ResponsablePago
            int_ViveConAlumno = ViveConAlumno
            int_EmitirFactura = EmitirFactura
            str_FacturaRazonSocial = FacturaRazonSocial
            str_FacturaRUC = FacturaRUC
            str_FacturaDireccionEmpresa = FacturaDireccionEmpresa
        End Sub

#End Region
    End Class

End Namespace
