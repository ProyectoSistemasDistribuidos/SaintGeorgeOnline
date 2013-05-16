Namespace ModuloEntrevistas

    Public Class be_ProgramacionEntrevistaDetalleAcuerdos

#Region "Atributos"

        Private int_CodigoProgramacionEntrevistaCabecera As Integer
        Private int_CodigoRegistroAcuerdosEntrevista As Integer
        Private str_Acuerdo As String
        Private dt_FechaAcuerdo As Date

#End Region

#Region "Propiedades"

        Public Property CodigoProgramacionEntrevistaCabecera() As Integer
            Get
                Return int_CodigoProgramacionEntrevistaCabecera
            End Get
            Set(ByVal value As Integer)
                int_CodigoProgramacionEntrevistaCabecera = value
            End Set
        End Property
        Public Property CodigoRegistroAcuerdosEntrevista() As Integer
            Get
                Return int_CodigoRegistroAcuerdosEntrevista
            End Get
            Set(ByVal value As Integer)
                int_CodigoRegistroAcuerdosEntrevista = value
            End Set
        End Property
        Public Property Acuerdo() As String
            Get
                Return str_Acuerdo
            End Get
            Set(ByVal value As String)
                str_Acuerdo = value
            End Set
        End Property
        Public Property FechaAcuerdo() As Date
            Get
                Return dt_FechaAcuerdo
            End Get
            Set(ByVal value As Date)
                dt_FechaAcuerdo = value
            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub

        Sub New(ByVal CodigoProgramacionEntrevistaCabecera As Integer, _
                ByVal CodigoRegistroAcuerdosEntrevista As Integer, _
                ByVal Acuerdo As String, _
                ByVal FechaAcuerdo As Date)

            int_CodigoProgramacionEntrevistaCabecera = CodigoProgramacionEntrevistaCabecera
            int_CodigoRegistroAcuerdosEntrevista = CodigoRegistroAcuerdosEntrevista
            str_Acuerdo = Acuerdo
            dt_FechaAcuerdo = dt_FechaAcuerdo

        End Sub

#End Region

    End Class

End Namespace