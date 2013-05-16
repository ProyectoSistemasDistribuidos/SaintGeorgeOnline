Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio
Imports SaintGeorgeOnline_DataAccess.ModuloColegio

Namespace ModuloColegio

    Public Class bl_AsignacionIniciosBimestres

#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_AsignacionIniciosBimestres As da_AsignacionIniciosBimestres

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
            obj_da_AsignacionIniciosBimestres = New da_AsignacionIniciosBimestres
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_AsignacionIniciosBimestres(ByVal objAsignacionIniciosBimestres As be_AsignacionIniciosBimestres, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionIniciosBimestres.FUN_INS_AsignacionIniciosBimestres(objAsignacionIniciosBimestres, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

        Public Function FUN_DEL_AsignacionIniciosBimestres(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            Return obj_da_AsignacionIniciosBimestres.FUN_DEL_AsignacionIniciosBimestres(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function


#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_AsignacionIniciosBimestres(ByVal int_CodigoAnioAcademico As Integer, _
                                                ByVal int_CodigoNivel As Integer, _
                                                ByVal int_CodigoSubNiveles As Integer, _
                                                ByVal int_CodigoGrado As Integer, _
                                                ByVal int_CodigoBimestre As Integer, _
                                                ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_AsignacionIniciosBimestres.FUN_LIS_AsignacionIniciosBimestres( _
                int_CodigoAnioAcademico, int_CodigoNivel, int_CodigoSubNiveles, int_CodigoGrado, int_CodigoBimestre, _
                int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        End Function

#End Region

    End Class

End Namespace