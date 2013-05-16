Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloAcademico

Namespace ModuloAcademico

    Public Class da_ProgramacionExamen
        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"
        Private dbBase As SqlDatabase
        Private dbCommand As DbCommand
#End Region

#Region "Contructor"
        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
        End Sub
#End Region

#Region "Región Transaccional"

        Public Function FUN_INS_ProgramacionExamenes( _
            ByVal obe_RegistroNotasCargo As be_RegistroNotasCargo, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_ProgramacionExamenes")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Tipo", DbType.Int32, obe_RegistroNotasCargo.TipoNota)
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroCargo", DbType.Int32, obe_RegistroNotasCargo.CodigoRegistroCargo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroAnual", DbType.Int32, obe_RegistroNotasCargo.CodigoRegistroAnual)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaProfesor", DbType.Int32, obe_RegistroNotasCargo.CodigoPersonaProfesor)

            dbBase.AddInParameter(dbCommand, "@p_Nota", DbType.String, IIf(obe_RegistroNotasCargo.Nota.Length = 0, DBNull.Value, obe_RegistroNotasCargo.Nota))


            ''If obe_RegistroNotasCargo.TipoNota = 1 Then ' Cuantitativo: 1
            ''    dbBase.AddInParameter(dbCommand, "@p_NotaCt", DbType.Decimal, obe_RegistroNotasCargo.NotaCt)
            ''    dbBase.AddInParameter(dbCommand, "@p_NotaCl", DbType.String, DBNull.Value)
            ''Else ' Cualitativo: 2
            ''    dbBase.AddInParameter(dbCommand, "@p_NotaCt", DbType.Decimal, DBNull.Value)
            ''    dbBase.AddInParameter(dbCommand, "@p_NotaCl", DbType.String, obe_RegistroNotasCargo.NotaCl)
            ''End If    

            dbBase.AddInParameter(dbCommand, "@p_FechaExamen", DbType.Date, obe_RegistroNotasCargo.FechaExamen)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoExamen", DbType.Int32, obe_RegistroNotasCargo.CodigoTipoExamen)


            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_ProgramacionExamenesNota( _
            ByVal obe_RegistroNotasCargo As be_RegistroNotasCargo, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_UPD_ProgramacionExamenesNota")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Tipo", DbType.Int32, obe_RegistroNotasCargo.TipoNota)
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroCargo", DbType.Int32, obe_RegistroNotasCargo.CodigoRegistroCargo)
            dbBase.AddInParameter(dbCommand, "@p_Nota", DbType.String, IIf(obe_RegistroNotasCargo.Nota.Length = 0, DBNull.Value, obe_RegistroNotasCargo.Nota))

            'If obe_RegistroNotasCargo.TipoNota = 1 Then ' Cuantitativo: 1
            '    dbBase.AddInParameter(dbCommand, "@p_NotaCt", DbType.Decimal, obe_RegistroNotasCargo.NotaCt)
            '    dbBase.AddInParameter(dbCommand, "@p_NotaCl", DbType.String, DBNull.Value)
            'Else ' Cualitativo: 2
            '    dbBase.AddInParameter(dbCommand, "@p_NotaCt", DbType.Decimal, DBNull.Value)
            '    dbBase.AddInParameter(dbCommand, "@p_NotaCl", DbType.String, obe_RegistroNotasCargo.NotaCl)
            'End If


            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_DEL_ProgramacionExamenes( _
            ByVal obe_RegistroNotasCargo As be_RegistroNotasCargo, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_DEL_ProgramacionExamenes")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroCargo", DbType.Int32, obe_RegistroNotasCargo.CodigoRegistroCargo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoRegistroAnual", DbType.Int32, obe_RegistroNotasCargo.CodigoRegistroAnual)
            dbBase.AddInParameter(dbCommand, "@p_Tipo", DbType.Int32, obe_RegistroNotasCargo.TipoNota)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

#End Region

#Region "Región No Transaccional"

        Public Function FUN_LIS_ProgramacionExamenes( _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal str_CodigoAlumno As String, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_ProgramacionExamenes")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_codigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_codigoAlumno", DbType.String, str_CodigoAlumno)

            dbBase.AddInParameter(cmd, "@p_codGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_codAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_ProgramacionExamenes( _
            ByVal int_Tipo As Integer, _
            ByVal int_CodigoRegistroCargo As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_GET_ProgramacionExamenes")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int32, int_Tipo)
            dbBase.AddInParameter(cmd, "@p_codigoRegistroCargo", DbType.Int32, int_CodigoRegistroCargo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_ProgramacionExamenesPorProfesor( _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoProfesor As Integer, _
            ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_ProgramacionExamenesPorProfesor")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_codAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_codProfesor", DbType.Int32, int_CodigoProfesor)

            dbBase.AddInParameter(cmd, "@p_codGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_codAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DetalleProgramacionExamenes( _
            ByVal int_Tipo As Integer, _
            ByVal int_CodigoRegistroAnual As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CU_USP_LIS_DetalleProgramacionExamenes")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Tipo", DbType.Int32, int_Tipo)
            dbBase.AddInParameter(cmd, "@p_codigoRegistroAnual", DbType.Int32, int_CodigoRegistroAnual)

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