Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros
Namespace ModuloBancoLibros
    Public Class bl_AsignacionVigenciaTitulos
#Region "Atributos"
        Private str_Mensaje As String
        Private obj_da_AsignacionVigenciaTitulos As da_AsignacionVigenciaTitulos
#End Region
#Region "propiedades"
        Public ReadOnly Property Mensaje() As String
            Get
                Return str_Mensaje
            End Get
        End Property
#End Region
#Region "Constructor"
        Sub New()
            obj_da_AsignacionVigenciaTitulos = New da_AsignacionVigenciaTitulos
        End Sub
#End Region
#Region "Métodos no Transaccionales"
        Public Function FUN_REP_DinamicoAniosUtilidad(ByVal int_PeriodoInicio As Integer, ByVal int_PeriodoFin As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsignacionVigenciaTitulos.FUN_REP_DinamicoAniosUtilidad(int_PeriodoInicio, int_PeriodoFin, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

    End Class
End Namespace

