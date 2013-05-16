Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloColegio

Namespace ModuloColegio

    Public Class da_SedesColegio
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

        Public Function FUN_INS_SedesColegio(ByVal objSedesColegio As be_SedesColegio, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_INS_SedesColegio")
            'Parámetros de entrada

            dbBase.AddInParameter(dbCommand, "@p_NombreSede", DbType.String, objSedesColegio.NombreSede)
            dbBase.AddInParameter(dbCommand, "@p_CodigoColegio", DbType.Int16, objSedesColegio.CodigoColegio)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeo", DbType.String, objSedesColegio.CodigoUbigeo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaResponsableMatricula", DbType.Int16, objSedesColegio.CodigoPersonaResponsableMatricula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaDirectorGeneral", DbType.Int16, objSedesColegio.CodigoPersonaDirectorGeneral)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaDirectorNacional", DbType.Int16, objSedesColegio.CodigoPersonaDirectorNacional)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaSubDirector", DbType.Int16, objSedesColegio.CodigoPersonaSubDirector)
            dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, objSedesColegio.Direccion)
            dbBase.AddInParameter(dbCommand, "@p_NombreUgel", DbType.String, objSedesColegio.NombreUgel)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUgel", DbType.String, objSedesColegio.CodigoUgel)
            dbBase.AddInParameter(dbCommand, "@p_NumeroUgel", DbType.Int16, objSedesColegio.NumeroUgel)
            dbBase.AddInParameter(dbCommand, "@p_Urbanizacion", DbType.String, objSedesColegio.Urbanizacion)
            dbBase.AddInParameter(dbCommand, "@p_NumeroResolucion", DbType.String, objSedesColegio.NumeroResolucion)
            dbBase.AddInParameter(dbCommand, "@p_Gestion", DbType.String, objSedesColegio.Gestion)
            dbBase.AddInParameter(dbCommand, "@p_Forma", DbType.String, objSedesColegio.Forma)
            dbBase.AddInParameter(dbCommand, "@p_Modalidad", DbType.String, objSedesColegio.Modalidad)
            dbBase.AddInParameter(dbCommand, "@p_Programa", DbType.String, objSedesColegio.Programa)
            dbBase.AddInParameter(dbCommand, "@p_Turno", DbType.String, objSedesColegio.Turno)
            dbBase.AddInParameter(dbCommand, "@p_GestionAbrv", DbType.String, objSedesColegio.GestionAbrv)
            dbBase.AddInParameter(dbCommand, "@p_FormaAbrv", DbType.String, objSedesColegio.FormaAbrv)
            dbBase.AddInParameter(dbCommand, "@p_ModalidadAbrv", DbType.String, objSedesColegio.ModalidadAbrv)
            dbBase.AddInParameter(dbCommand, "@p_ProgramaAbrv", DbType.String, objSedesColegio.ProgramaAbrv)
            dbBase.AddInParameter(dbCommand, "@p_TurnoAbrv", DbType.String, objSedesColegio.TurnoAbrv)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_UPD_SedesColegio(ByVal objSedesColegio As be_SedesColegio, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_UPD_SedesColegio")
            'Parámetros de entrada

            dbBase.AddInParameter(dbCommand, "@p_CodigoSede", DbType.Int16, objSedesColegio.CodigoSede)
            dbBase.AddInParameter(dbCommand, "@p_NombreSede", DbType.String, objSedesColegio.NombreSede)
            dbBase.AddInParameter(dbCommand, "@p_CodigoColegio", DbType.Int16, objSedesColegio.CodigoColegio)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeo", DbType.String, objSedesColegio.CodigoUbigeo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaResponsableMatricula", DbType.Int16, objSedesColegio.CodigoPersonaResponsableMatricula)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaDirectorGeneral", DbType.Int16, objSedesColegio.CodigoPersonaDirectorGeneral)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaDirectorNacional", DbType.Int16, objSedesColegio.CodigoPersonaDirectorNacional)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaSubDirector", DbType.Int16, objSedesColegio.CodigoPersonaSubDirector)
            dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, objSedesColegio.Direccion)
            dbBase.AddInParameter(dbCommand, "@p_NombreUgel", DbType.String, objSedesColegio.NombreUgel)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUgel", DbType.String, objSedesColegio.CodigoUgel)
            dbBase.AddInParameter(dbCommand, "@p_NumeroUgel", DbType.Int16, objSedesColegio.NumeroUgel)
            dbBase.AddInParameter(dbCommand, "@p_Urbanizacion", DbType.String, objSedesColegio.Urbanizacion)
            dbBase.AddInParameter(dbCommand, "@p_NumeroResolucion", DbType.String, objSedesColegio.NumeroResolucion)
            dbBase.AddInParameter(dbCommand, "@p_Gestion", DbType.String, objSedesColegio.Gestion)
            dbBase.AddInParameter(dbCommand, "@p_Forma", DbType.String, objSedesColegio.Forma)
            dbBase.AddInParameter(dbCommand, "@p_Modalidad", DbType.String, objSedesColegio.Modalidad)
            dbBase.AddInParameter(dbCommand, "@p_Programa", DbType.String, objSedesColegio.Programa)
            dbBase.AddInParameter(dbCommand, "@p_Turno", DbType.String, objSedesColegio.Turno)
            dbBase.AddInParameter(dbCommand, "@p_GestionAbrv", DbType.String, objSedesColegio.GestionAbrv)
            dbBase.AddInParameter(dbCommand, "@p_FormaAbrv", DbType.String, objSedesColegio.FormaAbrv)
            dbBase.AddInParameter(dbCommand, "@p_ModalidadAbrv", DbType.String, objSedesColegio.ModalidadAbrv)
            dbBase.AddInParameter(dbCommand, "@p_ProgramaAbrv", DbType.String, objSedesColegio.ProgramaAbrv)
            dbBase.AddInParameter(dbCommand, "@p_TurnoAbrv", DbType.String, objSedesColegio.TurnoAbrv)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        Public Function FUN_DEL_SedesColegio(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CO_USP_DEL_SedesColegio")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
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

        Public Function FUN_LIS_SedesColegio(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_LIS_SedesColegio")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, str_Descripcion)

            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)


        End Function

        Public Function FUN_GET_SedesColegio(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CO_USP_GET_SedesColegio")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)

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

