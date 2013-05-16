Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
'ok
Namespace ModuloBancoLibros

    Public Class da_Libros

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

        Public Function FUN_INS_Libros(ByVal objLibros As be_Libros, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_INS_Libros")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoIdioma", DbType.Int16, objLibros.CodigoIdioma)
            dbBase.AddInParameter(dbCommand, "@p_Titulo", DbType.String, objLibros.Titulo)
            dbBase.AddInParameter(dbCommand, "@p_Editorial", DbType.String, objLibros.Editorial)
            dbBase.AddInParameter(dbCommand, "@p_Autor", DbType.String, objLibros.Autor)
            dbBase.AddInParameter(dbCommand, "@p_Coleccion", DbType.String, objLibros.Coleccion)
            dbBase.AddInParameter(dbCommand, "@p_Nivel", DbType.String, objLibros.Nivel)
            dbBase.AddInParameter(dbCommand, "@p_ISBN", DbType.String, objLibros.ISBN)
            dbBase.AddInParameter(dbCommand, "@p_NumeroPaginas", DbType.Int16, objLibros.NumeroPaginas)
            dbBase.AddInParameter(dbCommand, "@p_NumeroCopia", DbType.Int16, objLibros.NumeroCopia)
            dbBase.AddInParameter(dbCommand, "@p_PrecioLibro", DbType.Decimal, objLibros.PrecioLibro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMonedaPrecioLibro", DbType.Int16, objLibros.CodigoMonedaPrecioLibro)
            dbBase.AddInParameter(dbCommand, "@p_PrecioPrestamo", DbType.Decimal, objLibros.PrecioPrestamo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMonedaPrecioPrestamo", DbType.Int16, objLibros.CodigoMonedaPrecioPrestamo)
            dbBase.AddInParameter(dbCommand, "@p_PrecioReposicion", DbType.Decimal, objLibros.PrecioReposicion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMonedaPrecioReposicion", DbType.Int16, objLibros.CodigoMonedaPrecioReposicion)
            dbBase.AddInParameter(dbCommand, "@p_RutaTapa", DbType.String, objLibros.RutaTapa)
            dbBase.AddInParameter(dbCommand, "@P_Largo", DbType.Decimal, objLibros.Largo)
            dbBase.AddInParameter(dbCommand, "@p_Ancho", DbType.Decimal, objLibros.Ancho)
            dbBase.AddInParameter(dbCommand, "@P_Grosor", DbType.Decimal, objLibros.Grosor)
            dbBase.AddInParameter(dbCommand, "@p_TL_CodigoTipoLibro", DbType.Decimal, objLibros.CodigoTipoLibro)
            dbBase.AddInParameter(dbCommand, "@P_CL_CodigoCurso", DbType.Decimal, IIf(objLibros.CodigoCurso = 0, DBNull.Value, objLibros.CodigoCurso))
            dbBase.AddInParameter(dbCommand, "@P_Edicion", DbType.String, objLibros.ISBN)
            dbBase.AddInParameter(dbCommand, "@P_Sede", DbType.Int32, objLibros.Sede)

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

        Public Function FUN_UPD_Libros(ByVal objLibros As be_Libros, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_UPD_Libros")
            'Parámetros de entrada 
            'ok
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objLibros.CodigoLibro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoIdioma", DbType.Int16, objLibros.CodigoIdioma)
            dbBase.AddInParameter(dbCommand, "@p_Titulo", DbType.String, objLibros.Titulo)
            dbBase.AddInParameter(dbCommand, "@p_Editorial", DbType.String, objLibros.Editorial)
            dbBase.AddInParameter(dbCommand, "@p_Coleccion", DbType.String, objLibros.Coleccion)
            dbBase.AddInParameter(dbCommand, "@p_Nivel", DbType.String, objLibros.Nivel)
            dbBase.AddInParameter(dbCommand, "@p_Autor", DbType.String, objLibros.Autor)
            dbBase.AddInParameter(dbCommand, "@p_ISBN", DbType.String, objLibros.ISBN)
            dbBase.AddInParameter(dbCommand, "@p_NumeroPaginas", DbType.Int16, objLibros.NumeroPaginas)
            dbBase.AddInParameter(dbCommand, "@p_NumeroCopia", DbType.Int16, objLibros.NumeroCopia)
            dbBase.AddInParameter(dbCommand, "@p_PrecioLibro", DbType.Decimal, objLibros.PrecioLibro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMonedaPrecioLibro", DbType.Int16, objLibros.CodigoMonedaPrecioLibro)
            dbBase.AddInParameter(dbCommand, "@p_PrecioPrestamo", DbType.Decimal, objLibros.PrecioPrestamo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMonedaPrecioPrestamo", DbType.Int16, objLibros.CodigoMonedaPrecioPrestamo)
            dbBase.AddInParameter(dbCommand, "@p_PrecioReposicion", DbType.Decimal, objLibros.PrecioReposicion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMonedaPrecioReposicion", DbType.Int16, objLibros.CodigoMonedaPrecioReposicion)
            dbBase.AddInParameter(dbCommand, "@p_RutaTapa", DbType.String, objLibros.RutaTapa)
            dbBase.AddInParameter(dbCommand, "@P_Largo", DbType.Decimal, objLibros.Largo)
            dbBase.AddInParameter(dbCommand, "@p_Ancho", DbType.Decimal, objLibros.Ancho)
            dbBase.AddInParameter(dbCommand, "@P_Grosor", DbType.Decimal, objLibros.Grosor)
            dbBase.AddInParameter(dbCommand, "@p_TL_CodigoTipoLibro", DbType.Decimal, objLibros.CodigoTipoLibro)
            dbBase.AddInParameter(dbCommand, "@P_CL_CodigoCurso", DbType.Decimal, IIf(objLibros.CodigoCurso = 0, DBNull.Value, objLibros.CodigoCurso))
            dbBase.AddInParameter(dbCommand, "@P_Edicion", DbType.String, objLibros.Edicion)
            dbBase.AddInParameter(dbCommand, "@P_Sede", DbType.Int32, objLibros.Sede)

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

        Public Function FUN_UPD_ImagenLibro(ByVal objLibros As be_Libros, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_UPD_ImagenesLibros")
            'Parámetros de entrada 
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objLibros.CodigoLibro)
            dbBase.AddInParameter(dbCommand, "@p_RutaTapa", DbType.String, objLibros.RutaTapa)
           
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

        Public Function FUN_DEL_Libros(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_DEL_Libros")
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

        Public Function FUN_INS_AsignacionGradosLibros(ByVal int_CodigoLibro As Integer, ByVal int_CodigoGrado As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_INS_AsignacionGradosLibros")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(dbCommand, "@p_CodigoLibro", DbType.Int32, int_CodigoLibro)

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

        Public Function FUN_DEL_AsignacionGradosLibros(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_DEL_AsignacionGradosLibros")
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
        Public Function FUN_LIS_Libros(ByVal int_AnioAcademico As Integer, ByVal str_Titulo As String, ByVal int_CodigoIdioma As Integer, ByVal str_ISBN As String, _
                                       ByVal str_CodigoGrado As String, ByVal int_CodigoTipoReporte As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_LIS_Libros")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_Titulo", DbType.String, str_Titulo)
            dbBase.AddInParameter(cmd, "@p_CodigoIdioma", DbType.Int16, int_CodigoIdioma)
            dbBase.AddInParameter(cmd, "@p_ISBN", DbType.String, str_ISBN)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.String, str_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoReporte", DbType.Int16, int_CodigoTipoReporte)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_LIS_UltimoCodigoLibro() As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_LIS_UltimoCodigoLibro")
            'Parámetros de entrada        
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_GET_Libros(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_GET_Libros")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoLibro", DbType.Int16, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_Idiomas(ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_LIS_Idioma")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, int_Estado)

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