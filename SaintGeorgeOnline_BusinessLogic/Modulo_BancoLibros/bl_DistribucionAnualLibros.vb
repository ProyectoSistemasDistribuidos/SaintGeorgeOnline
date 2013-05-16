Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Imports SaintGeorgeOnline_DataAccess.ModuloBancoLibros

Namespace ModuloBancoLibros
    Public Class bl_DistribucionAnualLibros

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_DistribucionAnualLibros As da_DistribucionAnualLibros

#End Region

#Region "Propiedades"

        Public ReadOnly Property Mensaje() As String
            Get
                Return str_Mensaje
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            obj_da_DistribucionAnualLibros = New da_DistribucionAnualLibros
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_DetalleDistribucionLibrosAnual(ByVal objDistribucionAnualLibros As be_DistribucionAnualLibros, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_DistribucionAnualLibros.FUN_INS_DetalleDistribucionLibrosAnual(objDistribucionAnualLibros, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_DistribucionAnualLibros(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_DistribucionAnualLibros.FUN_DEL_DistribucionAnualLibros(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionAnualLibros(ByVal int_CodigoAnio As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoIdioma As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_DistribucionAnualLibros.FUN_LIS_AsignacionAnualLibros(int_CodigoAnio, int_CodigoGrado, int_CodigoIdioma, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_LIS_DetalleDistribucionLibrosAnual(ByVal int_CodigoAnio As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_DistribucionAnualLibros.FUN_LIS_DetalleDistribucionLibrosAnual(int_CodigoAnio, int_CodigoGrado, int_CodigoAula, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        'Public Function FUN_LIS_ValidaFechaBimestre(ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAnio As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
        '    Return obj_da_DistribucionAnualLibros.FUN_LIS_ValidaFechaBimestre(dt_FechaInicio, dt_FechaFin, int_CodigoGrado, int_CodigoAnio, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        'End Function
        Public Function FUN_LIS_ValidaFechaBimestre(ByVal dt_FechaInicio As Date, ByVal dt_FechaFin As Date, ByVal int_CodigoGrado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_DistribucionAnualLibros.FUN_LIS_ValidaFechaBimestre(dt_FechaInicio, dt_FechaFin, int_CodigoGrado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region

    End Class
End Namespace

