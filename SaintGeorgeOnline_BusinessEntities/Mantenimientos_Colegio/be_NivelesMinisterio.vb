Namespace ModuloColegio

    Public Class be_NivelesMinisterio

#Region "Atributos"

        Private int_CodigoNivelesMinisterio As Integer
        Private str_Descripcion As String
        Private int_Estado As Integer
        Private str_CodigoModular As String
        Private str_Abreviatura As String
        Private str_CaracteristicaAbrev As String
        'Private dt_PeriodoElectivoIni As Date
        'Private dt_PeriodoElectivoFin As Date
#End Region

#Region "Propiedades"

        Public Property CodigoNivelesMinisterio() As Integer
            Get
                Return int_CodigoNivelesMinisterio
            End Get
            Set(ByVal value As Integer)
                int_CodigoNivelesMinisterio = value
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
        Public Property Estado() As Integer
            Get
                Return int_Estado
            End Get
            Set(ByVal value As Integer)
                int_Estado = value
            End Set
        End Property
        Public Property CodigoModular() As String
            Get
                Return str_CodigoModular
            End Get
            Set(ByVal value As String)
                str_CodigoModular = value
            End Set
        End Property
        Public Property Abreviatura() As String
            Get
                Return str_Abreviatura
            End Get
            Set(ByVal value As String)
                str_Abreviatura = value
            End Set
        End Property
        Public Property CaracteristicaAbrev() As String
            Get
                Return str_CaracteristicaAbrev
            End Get
            Set(ByVal value As String)
                str_CaracteristicaAbrev = value
            End Set
        End Property
        'Public Property PeriodoElectivoIni() As Date
        '    Get
        '        Return dt_PeriodoElectivoIni
        '    End Get
        '    Set(ByVal value As Date)
        '        dt_PeriodoElectivoIni = value
        '    End Set
        'End Property
        'Public Property PeriodoElectivoFin() As Date
        '    Get
        '        Return dt_PeriodoElectivoFin
        '    End Get
        '    Set(ByVal value As Date)
        '        dt_PeriodoElectivoFin = value
        '    End Set
        'End Property

#End Region

#Region "Constructor"

        Sub New()
            MyBase.New()
        End Sub
        Sub New(ByVal CodigoNivelesMinisterio As Integer, _
                ByVal Descripcion As String, _
                ByVal Estado As Integer, _
                ByVal CodigoModular As String, _
                ByVal Abreviatura As String, _
                ByVal CaracteristicaAbrev As String)
            int_CodigoNivelesMinisterio = CodigoNivelesMinisterio
            str_Descripcion = Descripcion
            int_Estado = Estado
            str_CodigoModular = CodigoModular
            str_Abreviatura = Abreviatura
            str_CaracteristicaAbrev = CaracteristicaAbrev
        End Sub

#End Region

    End Class

End Namespace