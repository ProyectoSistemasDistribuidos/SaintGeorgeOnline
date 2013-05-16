Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio

Namespace ModuloColegio

    Public Class bl_AsignacionSemanasPorBimestres
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionSemanasPorBimestres As da_AsignacionSemanasPorBimestres

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
            obj_da_AsignacionSemanasPorBimestres = New da_AsignacionSemanasPorBimestres
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionSemanasPorBimestres(ByVal objAsignacionSemanasPorBimestres As be_AsignacionSemanasPorBimestres, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionSemanasPorBimestres.FUN_INS_AsignacionSemanasPorBimestres(objAsignacionSemanasPorBimestres, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_UPD_AsignacionSemanasPorBimestres(ByVal objAsignacionSemanasPorBimestres As be_AsignacionSemanasPorBimestres, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionSemanasPorBimestres.FUN_UPD_AsignacionSemanasPorBimestres(objAsignacionSemanasPorBimestres, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_AsignacionSemanasPorBimestres(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionSemanasPorBimestres.FUN_DEL_AsignacionSemanasPorBimestres(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionSemanasPorBimestres(ByVal int_CodigoBimestre As Integer, ByVal int_CodigoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsignacionSemanasPorBimestres.FUN_LIS_AsignacionSemanasPorBimestres(int_CodigoBimestre, int_CodigoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_LIS_AsignacionSemanasPorBimestreAgregar(ByVal int_CodigoBimestre As Integer, ByVal int_CodigoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsignacionSemanasPorBimestres.FUN_LIS_AsignacionSemanasPorBimestreAgregar(int_CodigoBimestre, int_CodigoAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_GET_AsignacionSemanasPorBimestres(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_AsignacionSemanasPorBimestres.FUN_GET_AsignacionSemanasPorBimestres(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


#End Region
    End Class

End Namespace