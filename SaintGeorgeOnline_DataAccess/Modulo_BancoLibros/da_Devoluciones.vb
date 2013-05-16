Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros

Namespace ModuloBancoLibros

    Public Class da_Devoluciones
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

        Public Function FUN_UPD_PrestamoDetalle(ByVal int_Codigo As Integer, ByVal int_Accion As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_UPD_PrestamoDetalle")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Codigo)
            dbBase.AddInParameter(dbCommand, "@p_Accion", DbType.Int32, int_Accion)

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


        Public Function FUN_UPD_DevolverLibro(ByVal int_CodigoDetalle As Integer, _
                                              ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

            Dim lstResultado As New List(Of String)
            Dim p_Mensaje As String = ""
            Dim p_Valor As Integer = 0

            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_UPD_DevolverLibro")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@codDetalle", DbType.Int32, int_CodigoDetalle)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            p_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            lstResultado.Add(p_Valor.ToString())
            lstResultado.Add(p_Mensaje)

            Return lstResultado

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_GET_PrestamoDetalle(ByVal int_CodigoPrestamo As Integer, ByVal int_CodigoLibro As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_GET_PrestamoDetalle")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPrestamo", DbType.Int32, int_CodigoPrestamo)
            dbBase.AddInParameter(cmd, "@p_CodigoLibro", DbType.Int32, int_CodigoLibro)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_PrestamoPorLibroGradoPeriodo( _
            ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_Idioma As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_LIS_PrestamoPorLibroGradoPeriodo")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoIdioma", DbType.Int16, int_Idioma)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace
