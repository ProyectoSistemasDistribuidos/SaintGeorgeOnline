Imports SaintGeorgeOnline_DataAccess.ModuloConductaAlumnos
Imports SaintGeorgeOnline_DataAccess

Public Class bl_meritoDemerito
#Region "Atributos"
    Private str_Mensaje As String
    Private obj_da_CriterioConducta As da_CriteriosConducta
#End Region
#Region "Propiedades"
    Public ReadOnly Property Mensajea() As String
        Get
            Return str_Mensaje
        End Get
    End Property
#End Region
#Region "Constructor"
    Public Sub New()
        obj_da_CriterioConducta = New da_CriteriosConducta
    End Sub
#End Region
#Region "Metodos No Transaccionales"
    Public Function F_ListarReporteMeritosDemeritos(ByVal codAula As Integer, ByVal codAlumno As Integer, ByVal codAnioAcademico As Integer, ByVal codBimestre As Integer, ByVal tipoCiterio As Integer)
        Try

            Return New da_meritoDemerito().F_ListarReporteMeritosDemeritos(codAula, codAlumno, codAnioAcademico, codBimestre, tipoCiterio)
        Catch ex As Exception

        End Try
    End Function

    Public Function F_litarAlumnosMeritoDemerito(ByVal p_CodigoAnioAcademico As Integer, _
                                                ByVal p_CodigoAsignacionAula As Integer, ByVal codAlumno As Integer, ByVal p_CodigoUsuario As Integer, ByVal p_CodigoTipoUsuario As Integer, _
                                               ByVal p_CodigoModulo As Integer, ByVal p_CodigoOpcion As Integer)
        Try
            Return New da_meritoDemerito().F_litarAlumnosMeritoDemerito(p_CodigoAnioAcademico, p_CodigoAsignacionAula, codAlumno, p_CodigoUsuario, p_CodigoTipoUsuario, p_CodigoModulo, p_CodigoOpcion)
        Catch ex As Exception

        End Try
    End Function

#End Region


End Class
