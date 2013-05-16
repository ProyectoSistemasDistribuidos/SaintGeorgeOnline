Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_DataAccess.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class bl_FichaAtencion


#Region "Atributos"

        Private str_Mensaje As String
        Private obj_da_FichaAtencion As da_FichaAtencion

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
            obj_da_FichaAtencion = New da_FichaAtencion
        End Sub

#End Region

#Region "Metodos Transacciones"

        Public Function FUN_INS_FichaAtencion(ByVal objFichaAtencion As be_FichaAtencion, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_FichaAtencion.FUN_INS_FichaAtencion(objFichaAtencion, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_UPD_FichaAtencion(ByVal objFichaAtencion As be_FichaAtencion, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_FichaAtencion.FUN_UPD_FichaAtencion(objFichaAtencion, objDetalle, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_DEL_FichaAtencion(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_FichaAtencion.FUN_DEL_FichaAtencion(int_Codigo, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_UPD_EstadoFichaAtencion(ByVal int_CodigoFichaAtencion As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Return obj_da_FichaAtencion.FUN_UPD_EstadoFichaAtencion(int_CodigoFichaAtencion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_FichaAtencion( _
                ByVal int_CodigoTipoPaciente As Integer, _
                ByVal str_Nombre As String, _
                ByVal str_ApellidoPaterno As String, _
                ByVal str_ApellidoMaterno As String, _
                ByVal int_AlumnoNivel As Integer, _
                ByVal int_AlumnoSubNivel As Integer, _
                ByVal int_AlumnoGrado As Integer, _
                ByVal int_AlumnoAula As Integer, _
                ByVal str_FamiliarNombre As String, _
                ByVal str_FamiliarApellidoPaterno As String, _
                ByVal str_FamiliarApellidoMaterno As String, _
                ByVal int_FamiliarAlumnoNivel As Integer, _
                ByVal int_FamiliarAlumnoSubNivel As Integer, _
                ByVal int_FamiliarAlumnoGrado As Integer, _
                ByVal int_FamiliarAlumnoAula As Integer, _
                ByVal dt_FechaRangoInicial As Date, _
                ByVal dt_FechaRangoFinal As Date, _
                ByVal int_Sede As Integer, _
                ByVal int_Estado As Integer, _
                ByVal int_EstadoRegistro As Integer, _
                ByVal int_CodigoUsuario As Integer, _
                ByVal int_CodigoTipoUsuario As Integer, _
                ByVal int_CodigoModulo As Integer, _
                ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaAtencion.FUN_LIS_FichaAtencion( _
                int_CodigoTipoPaciente, str_Nombre, str_ApellidoPaterno, str_ApellidoMaterno, _
                int_AlumnoNivel, int_AlumnoSubNivel, int_AlumnoGrado, int_AlumnoAula, _
                str_FamiliarNombre, str_FamiliarApellidoPaterno, str_FamiliarApellidoMaterno, _
                int_FamiliarAlumnoNivel, int_FamiliarAlumnoSubNivel, int_FamiliarAlumnoGrado, int_FamiliarAlumnoAula, _
                dt_FechaRangoInicial, dt_FechaRangoFinal, int_Sede, int_Estado, int_EstadoRegistro, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_GET_FichaAtencion(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaAtencion.FUN_GET_FichaAtencion(int_Codigo, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DatosRelevantesFichaAtencion(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaAtencion.FUN_LIS_DatosRelevantesFichaAtencion(int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DatosSeguroFichaAtencion(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaAtencion.FUN_LIS_DatosSeguroFichaAtencion(int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_DatosFichaAtencion(ByVal str_CodigoAlumno As String, ByVal str_AnioAcademico As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaAtencion.FUN_LIS_DatosFichaAtencion(str_CodigoAlumno, str_AnioAcademico, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

        Public Function FUN_LIS_ContactosFichaAtencion(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Return obj_da_FichaAtencion.FUN_LIS_ContactosFichaAtencion(int_CodigoPersona, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

        End Function

#End Region

    End Class

End Namespace
