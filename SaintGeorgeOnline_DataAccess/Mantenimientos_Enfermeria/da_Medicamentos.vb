Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria

Namespace ModuloEnfermeria

    Public Class da_Medicamentos
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

        Public Function FUN_INS_Medicamento(ByVal objMedicamento As be_Medicamentos, _
                                            ByRef str_Mensaje As String, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_Medicamentos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombre", DbType.Int16, objMedicamento.CodigoNombre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPresentacion", DbType.Int16, objMedicamento.CodigoPresentacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUnidad", DbType.Int16, objMedicamento.CodigoUnidadMedida)
            dbBase.AddInParameter(dbCommand, "@p_Cantidad", DbType.Int16, objMedicamento.Cantidad)
            dbBase.AddInParameter(dbCommand, "@p_Concentracion", DbType.String, objMedicamento.Concentracion)
            dbBase.AddInParameter(dbCommand, "@p_Control", DbType.Int16, objMedicamento.Control)

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
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_Medicamento(ByVal objMedicamento As be_Medicamentos, _
                                            ByRef str_Mensaje As String, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_UPD_Medicamentos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objMedicamento.CodigoMedicamento)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombre", DbType.Int16, objMedicamento.CodigoNombre)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPresentacion", DbType.Int16, objMedicamento.CodigoPresentacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUnidad", DbType.Int16, objMedicamento.CodigoUnidadMedida)
            dbBase.AddInParameter(dbCommand, "@p_Cantidad", DbType.Int16, objMedicamento.Cantidad)
            dbBase.AddInParameter(dbCommand, "@p_Concentracion", DbType.String, objMedicamento.Concentracion)
            dbBase.AddInParameter(dbCommand, "@p_Control", DbType.Int16, objMedicamento.Control)

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
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_DEL_Medicamento(ByVal int_Codigo As Integer, _
                                            ByRef str_Mensaje As String, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_Medicamentos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_Codigo)

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
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_Medicamento(ByVal int_CodigoNombre As Integer, _
                                            ByVal int_CodigoPresentacion As Integer, _
                                            ByVal int_CodigoUnidadMedida As Integer, _
                                            ByVal int_Estado As Integer, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_Medicamentos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoNombre", DbType.Int16, int_CodigoNombre)
            dbBase.AddInParameter(cmd, "@p_CodigoPresentacion", DbType.Int16, int_CodigoPresentacion)
            dbBase.AddInParameter(cmd, "@p_CodigoUnidad", DbType.Int16, int_CodigoUnidadMedida)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_Medicamento(ByVal int_Codigo As Integer, _
                                            ByVal int_CodigoUsuario As Integer, _
                                            ByVal int_CodigoTipoUsuario As Integer, _
                                            ByVal int_CodigoModulo As Integer, _
                                            ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_GET_Medicamentos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

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