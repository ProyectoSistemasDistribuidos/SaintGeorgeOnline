Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula

Namespace ModuloMatricula

    Public Class da_MaestroPersonas
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

        Public Function FUN_INS_Otros(ByVal objOtros As be_Otros, ByVal int_CodigoOtros As Integer, ByRef int_CodigoPersona As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_Otros")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_ApellidoPaterno", DbType.String, objOtros.ApellidoPaterno)
            dbBase.AddInParameter(dbCommand, "@p_ApellidoMaterno", DbType.String, objOtros.ApellidoMaterno)
            dbBase.AddInParameter(dbCommand, "@p_Nombre", DbType.String, objOtros.Nombre)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_CodigoOtros", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            int_CodigoOtros = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_CodigoOtros")))
            int_CodigoPersona = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_CodigoPersona")))
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Personas(ByVal objMaestroPersona As be_MaestroPersonas, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_Personas")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_TipoPersona", DbType.Int16, objMaestroPersona.CodigoTipoPersona)
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, objMaestroPersona.ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, objMaestroPersona.ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, objMaestroPersona.Nombre)

            dbBase.AddInParameter(cmd, "@p_AlumnoNivel", DbType.Int16, objMaestroPersona.AlumnoNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoSubnivel", DbType.Int16, objMaestroPersona.AlumnoSubnivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoGrado", DbType.Int16, objMaestroPersona.AlumnoGrado)
            dbBase.AddInParameter(cmd, "@p_AlumnoAula", DbType.Int16, objMaestroPersona.AlumnoAula)

            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarApellidoPaterno", DbType.String, objMaestroPersona.AlumnoFamiliarApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarApellidoMaterno", DbType.String, objMaestroPersona.AlumnoFamiliarApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarNombres", DbType.String, objMaestroPersona.AlumnoFamiliarNombres)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarNivel", DbType.Int16, objMaestroPersona.AlumnoFamiliarNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarSubnivel", DbType.Int16, objMaestroPersona.AlumnoFamiliarSubnivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarGrado", DbType.Int16, objMaestroPersona.AlumnoFamiliarGrado)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarAula", DbType.Int16, objMaestroPersona.AlumnoFamiliarAula)


            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_AlumnoPorCodigoPersona(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_AlumnoPorCodigoPersona")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPersona", DbType.Int16, int_CodigoPersona)


            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_CON_PersonaDisponibilidad(ByVal objPersona As be_Personas, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_CON_PersonaDisponibilidad")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, objPersona.ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, objPersona.ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, objPersona.Nombre)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoDocIdentidad", DbType.Int16, objPersona.CodigoTipoDocIdentidad)
            dbBase.AddInParameter(cmd, "@p_NumeroDocIdentidad", DbType.String, objPersona.NumeroDocIdentidad)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(cmd, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(cmd, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(cmd)
            str_Mensaje = dbBase.GetParameterValue(cmd, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(cmd, "@p_Valor")))

        End Function

        Public Function FUN_LIS_PersonaDisponibilidad(ByVal objPersona As be_Personas, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_PersonaDisponibilidad")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, objPersona.ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, objPersona.ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, objPersona.Nombre)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_PersonasPorTipo(ByVal int_CodigoTipoPersona As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_PersonasPorTipo")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTipoPersona", DbType.Int16, int_CodigoTipoPersona)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.String, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_PersonasProfesores(ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_PersonasProfesores")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.String, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_LIS_PersonasProfesoresPorTipoPeriodoGradoAula( _
            ByVal int_Tipo As Integer, ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_PersonasProfesoresPorTipoPeriodoGradoAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int32, int_Tipo)
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

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






