Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMensajeria

Namespace ModuloMensajeria

    Public Class da_MensajesRecibidos
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

        Public Function FUN_DEL_MensajesRecibidos(ByVal objMensajesRecibidos As be_MensajesRecibidos, ByVal str_ListaCodigosMensajesRecibidos As String, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MS_USP_DEL_MensajeRecibidos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Usuario", DbType.String, objMensajesRecibidos.CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoPersonaRecibe", DbType.Int32, objMensajesRecibidos.CodigoTipoPersonaRecepcion)
            dbBase.AddInParameter(dbCommand, "@p_ListaCodigosMensajesRecibidos", DbType.String, str_ListaCodigosMensajesRecibidos)

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

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_MensajesRecibidos(ByVal str_CodigoUsuario As String, _
                                                  ByVal int_TipoUsuario As Integer, _
                                                  ByVal int_TipoMensaje As Integer, _
                                                  ByVal int_EstadoMensaje As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MS_USP_LIS_MensajesRecibidos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_TipoUsuario", DbType.Int32, int_TipoUsuario)
            dbBase.AddInParameter(cmd, "@p_TipoMensaje", DbType.Int32, int_TipoMensaje)
            dbBase.AddInParameter(cmd, "@p_EstadoMensaje", DbType.Int32, int_EstadoMensaje)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        '24/10/2012
        Public Function FUN_LIS_MensajesRecibidosNew( _
            ByVal str_CodigoUsuario As String, ByVal int_TipoUsuario As Integer, _
            ByVal int_limInf As Integer, ByVal int_limSup As Integer, ByVal int_pagina As Integer, _
            ByVal int_TipoMensaje As Integer, ByVal int_EstadoMensaje As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MS_USP_LIS_MensajesRecibidosNew")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_TipoUsuario", DbType.Int32, int_TipoUsuario)

            dbBase.AddInParameter(cmd, "@p_pagina", DbType.Int32, int_pagina)
            dbBase.AddInParameter(cmd, "@p_limInf", DbType.Int32, int_limInf)
            dbBase.AddInParameter(cmd, "@p_limSup", DbType.Int32, int_limSup)

            dbBase.AddInParameter(cmd, "@p_TipoMensaje", DbType.Int32, int_TipoMensaje)
            dbBase.AddInParameter(cmd, "@p_EstadoMensaje", DbType.Int32, int_EstadoMensaje)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
        Public Function FUN_LIS_PaginadoMensajes( _
            ByVal str_CodigoUsuario As String, ByVal int_TipoUsuario As Integer, _
            ByVal int_limInf As Integer, ByVal int_limSup As Integer, ByVal int_pagina As Integer, _
            ByVal int_TipoMensaje As Integer, ByVal int_EstadoMensaje As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MS_USP_LIS_PaginadoMensajes")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_TipoUsuario", DbType.Int32, int_TipoUsuario)

            dbBase.AddInParameter(cmd, "@p_pagina", DbType.Int32, int_pagina)
            dbBase.AddInParameter(cmd, "@p_limInf", DbType.Int32, int_limInf)
            dbBase.AddInParameter(cmd, "@p_limSup", DbType.Int32, int_limSup)

            dbBase.AddInParameter(cmd, "@p_TipoMensaje", DbType.Int32, int_TipoMensaje)
            dbBase.AddInParameter(cmd, "@p_EstadoMensaje", DbType.Int32, int_EstadoMensaje)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_MensajesRecibidosPorTipoYCodigo( _
            ByVal str_CodigoUsuario As String, ByVal int_TipoUsuario As Integer, _
            ByVal int_limInf As Integer, ByVal int_limSup As Integer, ByVal int_pagina As Integer, _
            ByVal int_TipoMensaje As Integer, ByVal int_EstadoMensaje As Integer, _
            ByVal int_Carpeta As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MS_USP_LIS_MensajesRecibidosPorTipoYCodigo")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_TipoUsuario", DbType.Int32, int_TipoUsuario)

            dbBase.AddInParameter(cmd, "@p_pagina", DbType.Int32, int_pagina)
            dbBase.AddInParameter(cmd, "@p_limInf", DbType.Int32, int_limInf)
            dbBase.AddInParameter(cmd, "@p_limSup", DbType.Int32, int_limSup)

            dbBase.AddInParameter(cmd, "@p_TipoMensaje", DbType.Int32, int_TipoMensaje)
            dbBase.AddInParameter(cmd, "@p_EstadoMensaje", DbType.Int32, int_EstadoMensaje)

            dbBase.AddInParameter(cmd, "@p_Carpeta", DbType.Int32, int_Carpeta)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
        Public Function FUN_LIS_PaginadoMensajesPorTipoYCodigo( _
            ByVal str_CodigoUsuario As String, ByVal int_TipoUsuario As Integer, _
            ByVal int_limInf As Integer, ByVal int_limSup As Integer, ByVal int_pagina As Integer, _
            ByVal int_TipoMensaje As Integer, ByVal int_EstadoMensaje As Integer, _
            ByVal int_Carpeta As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MS_USP_LIS_PaginadoMensajesPorTipoYCodigo")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Usuario", DbType.String, str_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_TipoUsuario", DbType.Int32, int_TipoUsuario)

            dbBase.AddInParameter(cmd, "@p_pagina", DbType.Int32, int_pagina)
            dbBase.AddInParameter(cmd, "@p_limInf", DbType.Int32, int_limInf)
            dbBase.AddInParameter(cmd, "@p_limSup", DbType.Int32, int_limSup)

            dbBase.AddInParameter(cmd, "@p_TipoMensaje", DbType.Int32, int_TipoMensaje)
            dbBase.AddInParameter(cmd, "@p_EstadoMensaje", DbType.Int32, int_EstadoMensaje)

            dbBase.AddInParameter(cmd, "@p_Carpeta", DbType.Int32, int_Carpeta)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_MensajesRecibidos(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MS_USP_GET_MensajesRecibidos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoMensajeRecibido", DbType.Int32, int_Codigo)

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