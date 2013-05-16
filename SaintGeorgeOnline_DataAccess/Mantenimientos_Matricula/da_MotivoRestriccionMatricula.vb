Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula

Namespace ModuloMatricula

    Public Class da_MotivoRestriccionMatricula
        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar

#End Region

#Region "Constructor"

        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
        End Sub

#End Region

#Region "Metodos Transaccionales"

        Public Function FUN_INS_MotivoRestriccionMatricula(ByVal objMotivoRestriccionMatricula As be_MotivoRestriccionMatricula, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_MotivosRestriccionMatricula")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objMotivoRestriccionMatricula.Descripcion)
            dbBase.AddInParameter(dbCommand, "@p_MensajeAlerta", DbType.String, objMotivoRestriccionMatricula.MensajeAlerta)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_MotivoRestriccionMatricula(ByVal objMotivoRestriccionMatricula As be_MotivoRestriccionMatricula, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_MotivosRestriccionMatricula")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, objMotivoRestriccionMatricula.CodigoMotivoRestriccionMatricula)
            dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objMotivoRestriccionMatricula.Descripcion)
            dbBase.AddInParameter(dbCommand, "@p_MensajeAlerta", DbType.String, objMotivoRestriccionMatricula.MensajeAlerta)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_DEL_MotivoRestriccionMatricula(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_MotivosRestriccionMatricula")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_ACT_RelacionMotivoRestriccionMatricula(ByVal codigo As Integer, ByVal nombreCompleto As String, ByVal opcion As Integer, ByVal observacion As String) As Integer
    
            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_RelacionMotivoRestriccionMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, codigo)
            dbBase.AddInParameter(dbCommand, "@p_NombreCompleto", DbType.String, nombreCompleto)
            dbBase.AddInParameter(dbCommand, "@p_Opcion", DbType.Int16, opcion)
            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, observacion)
            'dbBase.AddInParameter(dbCommand, "@p_Indice", DbType.Int16, dr.Item("Indice"))
            'dbBase.AddInParameter(dbCommand, "@p_ValorActualizar", DbType.String, dr.Item("CodigoCampo"))

            'Ejecucion del Store Procedure
            'dbBase.ExecuteScalar(dbCommand, tran)
            dbBase.ExecuteScalar(dbCommand)
            'str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return codigo
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_MotivoRestriccionMatricula(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_MotivosRestriccionMatricula")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int32, int_Estado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_MotivoRestriccionMatriculaPorMotivoRestriccion() As DataSet
            Try
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_MotivosRestriccionMatriculaPorColumna")
                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)

            Catch ex As Exception

                'System.Web.HttpContext.Current.Server.ClearError()
                'System.Web.HttpContext.Current.Session("Error") = "Error"
                'System.Web.HttpContext.Current.Response.Redirect("../Errores/PaginaDeError.aspx", True)

                Return Nothing

            End Try
        End Function

        Public Function FUN_LIS_AlumnosPorMotivoRestriccion(ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_Grado As Integer) As DataSet
            Try
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorMotivoRestriccion")
                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, str_ApellidoPaterno)
                dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, str_ApellidoMaterno)
                dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, str_Nombre)
                dbBase.AddInParameter(cmd, "@p_Grado", DbType.String, int_Grado)
                'Ejecucion del Store Procedure
                Return dbBase.ExecuteDataSet(cmd)

            Catch ex As Exception

                'System.Web.HttpContext.Current.Server.ClearError()
                'System.Web.HttpContext.Current.Session("Error") = "Error"
                'System.Web.HttpContext.Current.Response.Redirect("../Errores/PaginaDeError.aspx", True)

                Return Nothing

            End Try
        End Function

        Public Function FUN_GET_MotivoRestriccionMatricula(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_MotivosRestriccionMatricula")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

#End Region
    End Class

End Namespace
