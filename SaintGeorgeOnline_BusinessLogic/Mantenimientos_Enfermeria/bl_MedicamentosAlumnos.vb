Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class bl_MedicamentosAlumnos
#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_MedicamentosAlumnos As da_MedicamentosAlumnos

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
            obj_da_MedicamentosAlumnos = New da_MedicamentosAlumnos
        End Sub

#End Region

#Region "Metodos Transacciones"
        Public Function FUN_INS_MedicamentosAlumnos(ByVal objMedicamentosAlumnos As be_MedicamentosAlumnos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_MedicamentosAlumnos.FUN_INS_MedicamentosAlumnos(objMedicamentosAlumnos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_UPD_MedicamentosAlumnos(ByVal objMedicamentosAlumnos As be_MedicamentosAlumnos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_MedicamentosAlumnos.FUN_UPD_MedicamentosAlumnos(objMedicamentosAlumnos, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_DEL_MedicamentosAlumnos(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_MedicamentosAlumnos.FUN_DEL_MedicamentosAlumnos(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        
#End Region

#Region "Metodos No Transaccionales"
        Public Function FUN_LIS_MedicamentosAlumnos(ByVal str_Descripcion As String, ByVal int_Validado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_MedicamentosAlumnos.FUN_LIS_MedicamentosAlumnos(str_Descripcion, int_Validado, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
        Public Function FUN_GET_MedicamentosAlumnos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Return obj_da_MedicamentosAlumnos.FUN_GET_MedicamentosAlumnos(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function
#End Region
    End Class

End Namespace