Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloPermisos

Namespace ModuloPermisos

    Public Class da_SubbloquesMenus
        Inherits InstanciaConexion.ManejadorConexion

#Region "Atributos"

        Private dbBase As SqlDatabase 'ExecuteDataSet
        Private dbCommand As DbCommand 'ExecuteScalar

        Private cnn As DbConnection
        Private tran As DbTransaction

#End Region

#Region "Constructor"

        Public Sub New()
            dbBase = New SqlDatabase(Me.SqlConexionDB)
            cnn = Me.dbBase.CreateConnection()
        End Sub

#End Region

#Region "Propiedades"

        Public ReadOnly Property BaseDatos() As SqlDatabase
            Get
                Return Me.dbBase
            End Get
        End Property

        Public ReadOnly Property Transaccion() As DbTransaction
            Get
                Return Me.tran
            End Get
        End Property

        Public ReadOnly Property Conexion() As DbConnection
            Get
                Return Me.cnn
            End Get
        End Property

#End Region

#Region "Metodos"

        Public Sub BeginTransaction()

            If Not (cnn.State = ConnectionState.Open) Then
                cnn.Open()
            End If

            tran = cnn.BeginTransaction(IsolationLevel.Serializable)

        End Sub

        Public Sub Rollback()

            tran.Rollback()

        End Sub

        Public Sub Commit()

            tran.Commit()

        End Sub

#End Region

#Region "Metodos Transaccionales"

        'update 04/04/2011
        Public Function FUN_INS_SubbloqueMenu(ByVal objSubbloqueMenu As be_SubbloquesMenus, _
                                              ByVal objDetalle As DataSet, _
                                              ByRef str_Mensaje As String, _
                                              ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_CodigoBloqueInformacion As Integer = 0
            Dim int_Valor As Integer = 0
            Dim int_ValorAsignacion As Integer = 0
            Dim int_ValorAccion As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera
                dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_SubbloquesMenus")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoBloque", DbType.Int32, objSubbloqueMenu.CodigoBloque)
                dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objSubbloqueMenu.Descripcion)
                dbBase.AddInParameter(dbCommand, "@p_Link", DbType.String, objSubbloqueMenu.Link)
                dbBase.AddInParameter(dbCommand, "@p_RutaDocumentacion", DbType.String, objSubbloqueMenu.RutaDocumentacion)
                dbBase.AddInParameter(dbCommand, "@p_EstadoProceso", DbType.Int32, objSubbloqueMenu.EstadoProceso)
                dbBase.AddInParameter(dbCommand, "@p_TipoSubBloque", DbType.Int32, objSubbloqueMenu.TipoSubBloque)
                dbBase.AddInParameter(dbCommand, "@p_CodigoSubBloquePadre", DbType.Int32, objSubbloqueMenu.CodigoSubBloquePadre)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If int_Valor > 0 Then 'Registro de Cabecera : OK

                    'Detalle Bloque Informacion - Nuevos
                    If objDetalle.Tables(0) IsNot Nothing Then

                        'Si tiene almenos 1 registro y no es el registro CodigoRelacion = -1 entonces grabo 
                        If objDetalle.Tables(0).Rows.Count > 0 Then

                            If objDetalle.Tables(0).Rows(0).Item("CodigoRelacion") <> -1 Then

                                Dim objAsignacionBloquesInformacion As be_AsignacionBloquesInformacion
                                For Each dr As DataRow In objDetalle.Tables(0).Rows
                                    objAsignacionBloquesInformacion = New be_AsignacionBloquesInformacion
                                    objAsignacionBloquesInformacion.CodigoSubBloque = int_Valor
                                    objAsignacionBloquesInformacion.CodigoBloqueInformacion = dr.Item("CodigoBloqueInformacion")
                                    int_ValorAsignacion = FUN_INS_AsignacionBloquesInformacion(objAsignacionBloquesInformacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                    If int_ValorAsignacion > 0 Then

                                        int_CodigoBloqueInformacion = dr.Item("CodigoRelacion")
                                        Dim objAccionesAcceso As be_AccionesAcceso
                                        For Each drA As DataRow In objDetalle.Tables(1).Rows

                                            If drA.Item("CodigoBloqueInformacion") = int_CodigoBloqueInformacion Then
                                                objAccionesAcceso = New be_AccionesAcceso
                                                objAccionesAcceso.CodigoAsignacion = int_ValorAsignacion
                                                objAccionesAcceso.CodigoNombreAccion = drA.Item("CodigoAccion")
                                                int_ValorAccion = FUN_INS_AccionesAcceso(objAccionesAcceso, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                                If int_ValorAccion < 0 Then 'Error en registro de AccionesAcceso
                                                    str_Mensaje = "Ocurrio un error durante el registro."
                                                    Rollback()
                                                    Return int_ValorAccion
                                                End If
                                            End If

                                        Next

                                    Else 'Error en registro de Asignacion Bloque
                                        str_Mensaje = "Ocurrio un error durante el registro."
                                        Rollback()
                                        Return int_ValorAsignacion
                                    End If

                                Next

                            End If

                        End If
                    End If

                    Commit()
                    Return int_Valor

                Else

                    str_Mensaje = "Ocurrio un error durante el registro."
                    Rollback()
                    Return int_ValorAsignacion

                End If

            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante el registro." 'ex.Message
                Rollback()
                Return 0
            Finally
                Conexion.Close()
            End Try

        End Function

        Public Function FUN_UPD_SubbloqueMenu(ByVal objSubbloqueMenu As be_SubbloquesMenus, _
                                              ByVal objDetalle As DataSet, _
                                              ByRef str_Mensaje As String, _
                                              ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_CodigoBloqueInformacion As Integer = 0
            Dim int_CodigoAsignacion As Integer = 0
            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0
            Dim int_ValorAsignacion As Integer = 0
            Dim int_ValorAccion As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera
                dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_UPD_SubbloquesMenus")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, objSubbloqueMenu.CodigoSubBloque)
                dbBase.AddInParameter(dbCommand, "@p_CodigoBloque", DbType.Int32, objSubbloqueMenu.CodigoBloque)
                dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objSubbloqueMenu.Descripcion)
                dbBase.AddInParameter(dbCommand, "@p_Link", DbType.String, objSubbloqueMenu.Link)
                dbBase.AddInParameter(dbCommand, "@p_RutaDocumentacion", DbType.String, objSubbloqueMenu.RutaDocumentacion)
                dbBase.AddInParameter(dbCommand, "@p_EstadoProceso", DbType.Int32, objSubbloqueMenu.EstadoProceso)
                dbBase.AddInParameter(dbCommand, "@p_TipoSubBloque", DbType.Int32, objSubbloqueMenu.TipoSubBloque)
                dbBase.AddInParameter(dbCommand, "@p_CodigoSubBloquePadre", DbType.Int32, objSubbloqueMenu.CodigoSubBloquePadre)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure : Registro Cabecera
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If int_Valor > 0 Then 'Si actualizo la cabecera

                    'DataTable.Tables(0) - Detalle de Bloque de Informacion : Registros Nuevos
                    'DataTable.Tables(1) - Detalle de Bloque de Informacion : Registros Eliminados
                    'DataTable.Tables(2) - Detalle de Acciones : Registros Nuevos
                    'DataTable.Tables(3) - Detalle de Acciones : Registros Eliminados
                    'DataTable.Tables(4) - Detalle de Bloque de Informacion : Registros Constantes

                    'Si tiene almenos 1 registro y no es el registro CodigoRelacion = -1 entonces grabo 
                    If objDetalle.Tables(0).Rows.Count > 0 Then
                        If objDetalle.Tables(0).Rows(0).Item("CodigoRelacion") <> -1 Then

                            Dim objAsignacionBloquesInformacion As be_AsignacionBloquesInformacion
                            'DataTable.Tables(0) - Detalle de Bloque de Informacion : Registros Nuevos
                            For Each dr As DataRow In objDetalle.Tables(0).Rows
                                objAsignacionBloquesInformacion = New be_AsignacionBloquesInformacion
                                objAsignacionBloquesInformacion.CodigoSubBloque = int_Valor
                                objAsignacionBloquesInformacion.CodigoBloqueInformacion = dr.Item("CodigoBloqueInformacion")
                                int_ValorAsignacion = FUN_INS_AsignacionBloquesInformacion(objAsignacionBloquesInformacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                If int_ValorAsignacion > 0 Then

                                    int_CodigoBloqueInformacion = dr.Item("CodigoRelacion") 'objAsignacionBloquesInformacion.CodigoBloqueInformacion
                                    Dim objAccionesAcceso As be_AccionesAcceso

                                    'DataTable.Tables(2) - Detalle de Acciones : Registros Nuevos
                                    For Each drA As DataRow In objDetalle.Tables(2).Rows

                                        If drA.Item("CodigoBloqueInformacion") = int_CodigoBloqueInformacion Then
                                            objAccionesAcceso = New be_AccionesAcceso
                                            objAccionesAcceso.CodigoAsignacion = int_ValorAsignacion
                                            objAccionesAcceso.CodigoNombreAccion = drA.Item("CodigoAccion")
                                            int_ValorAccion = FUN_INS_AccionesAcceso(objAccionesAcceso, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                            If int_ValorAccion < 0 Then 'Error en registro de AccionesAcceso
                                                str_Mensaje = "Ocurrio un error durante el registro."
                                                Rollback()
                                                Return int_ValorAccion
                                            End If
                                        End If

                                    Next

                                Else 'Error en registro de Asignacion Bloque
                                    str_Mensaje = "Ocurrio un error durante el registro."
                                    Rollback()
                                    Return int_ValorAsignacion
                                End If

                            Next

                        End If
                    Else ' Sino agrego ningun nuevo Bloque de Informacion pero actualizo las acciones
                        If objDetalle.Tables(4).Rows.Count > 0 Then
                            If objDetalle.Tables(4).Rows(0).Item("CodigoRelacion") <> -1 Then

                                Dim objAsignacionBloquesInformacion As be_AsignacionBloquesInformacion
                                'DataTable.Tables(4) - Detalle de Bloque de Informacion : Registros Constantes
                                For Each dr As DataRow In objDetalle.Tables(4).Rows

                                    objAsignacionBloquesInformacion = New be_AsignacionBloquesInformacion
                                    objAsignacionBloquesInformacion.CodigoAsignacion = dr.Item("CodigoRelacion")

                                    int_ValorAsignacion = objAsignacionBloquesInformacion.CodigoAsignacion

                                    If int_ValorAsignacion > 0 Then

                                        int_CodigoBloqueInformacion = objAsignacionBloquesInformacion.CodigoBloqueInformacion
                                        Dim objAccionesAcceso As be_AccionesAcceso

                                        'DataTable.Tables(2) - Detalle de Acciones : Registros Nuevos
                                        For Each drA As DataRow In objDetalle.Tables(2).Rows

                                            If drA.Item("CodigoBloqueInformacion") = int_ValorAsignacion Then
                                                objAccionesAcceso = New be_AccionesAcceso
                                                objAccionesAcceso.CodigoAsignacion = int_ValorAsignacion
                                                objAccionesAcceso.CodigoNombreAccion = drA.Item("CodigoAccion")
                                                int_ValorAccion = FUN_INS_AccionesAcceso(objAccionesAcceso, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                                If int_ValorAccion < 0 Then 'Error en registro de AccionesAcceso
                                                    str_Mensaje = "Ocurrio un error durante el registro."
                                                    Rollback()
                                                    Return int_ValorAccion
                                                End If
                                            End If

                                        Next

                                    Else 'Error en registro de Asignacion Bloque
                                        str_Mensaje = "Ocurrio un error durante el registro."
                                        Rollback()
                                        Return int_ValorAsignacion
                                    End If

                                Next

                            End If
                        End If
                    End If

                    
                    'Si tiene almenos 1 registro y no es el registro CodigoRelacion = -1 entonces grabo 
                    If objDetalle.Tables(1).Rows.Count > 0 Then
                        If objDetalle.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then

                            Dim objAsignacionBloquesInformacion As be_AsignacionBloquesInformacion
                            'DataTable.Tables(1) - Detalle de Bloque de Informacion : Registros Eliminados
                            For Each dr As DataRow In objDetalle.Tables(1).Rows
                                objAsignacionBloquesInformacion = New be_AsignacionBloquesInformacion
                                objAsignacionBloquesInformacion.CodigoAsignacion = dr.Item("CodigoRelacion")

                                If objAsignacionBloquesInformacion.CodigoAsignacion > 0 Then 'Elimino todas las acciones dependientes

                                    int_CodigoAsignacion = objAsignacionBloquesInformacion.CodigoAsignacion
                                    Dim objAccionesAcceso As be_AccionesAcceso

                                    'DataTable.Tables(3) - Detalle de Acciones : Registros Eliminados
                                    For Each drA As DataRow In objDetalle.Tables(3).Rows

                                        If drA.Item("CodigoBloqueInformacion") = int_CodigoAsignacion Then
                                            objAccionesAcceso = New be_AccionesAcceso
                                            objAccionesAcceso.CodigoAccion = drA.Item("CodigoRelacion")
                                            int_ValorAccion = FUN_DEL_AccionesAcceso(objAccionesAcceso.CodigoAccion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                            If int_ValorAccion < 1 Then 'Error en eliminacion de AccionesAcceso
                                                str_Mensaje = "Ocurrio un error durante el registro."
                                                Rollback()
                                                Return int_ValorAccion
                                            End If
                                        End If

                                    Next

                                End If

                                int_ValorAsignacion = FUN_DEL_AsignacionBloquesInformacion(objAsignacionBloquesInformacion.CodigoAsignacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                If int_ValorAsignacion < 1 Then 'Error en la eliminacion de Bloques de Informacion
                                    str_Mensaje = "Ocurrio un error durante el registro."
                                    Rollback()
                                    Return int_ValorAsignacion
                                End If

                            Next

                        End If
                    Else ' Sino elimino ningun Bloque de Informacion reviso para las acciones
                        If objDetalle.Tables(4).Rows.Count > 0 Then
                            If objDetalle.Tables(4).Rows(0).Item("CodigoRelacion") <> -1 Then

                                Dim objAsignacionBloquesInformacion As be_AsignacionBloquesInformacion
                                'DataTable.Tables(4) - Detalle de Bloque de Informacion : Registros Constantes
                                For Each dr As DataRow In objDetalle.Tables(4).Rows

                                    objAsignacionBloquesInformacion = New be_AsignacionBloquesInformacion
                                    objAsignacionBloquesInformacion.CodigoAsignacion = dr.Item("CodigoRelacion")

                                    int_ValorAsignacion = objAsignacionBloquesInformacion.CodigoAsignacion

                                    If int_ValorAsignacion > 0 Then

                                        int_CodigoBloqueInformacion = int_ValorAsignacion
                                        Dim objAccionesAcceso As be_AccionesAcceso

                                        'DataTable.Tables(3) - Detalle de Acciones : Registros Eliminados
                                        For Each drA As DataRow In objDetalle.Tables(3).Rows

                                            If drA.Item("CodigoBloqueInformacion") = int_CodigoBloqueInformacion Then
                                                objAccionesAcceso = New be_AccionesAcceso
                                                objAccionesAcceso.CodigoAccion = drA.Item("CodigoRelacion")
                                                int_ValorAccion = FUN_DEL_AccionesAcceso(objAccionesAcceso.CodigoAccion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                                If int_ValorAccion < 1 Then 'Error en eliminacion de AccionesAcceso
                                                    str_Mensaje = "Ocurrio un error durante el registro."
                                                    Rollback()
                                                    Return int_ValorAccion
                                                End If
                                            End If

                                        Next

                                    Else 'Error en registro de Asignacion Bloque
                                        str_Mensaje = "Ocurrio un error durante el registro."
                                        Rollback()
                                        Return int_ValorAsignacion
                                    End If

                                Next

                            End If
                        End If
                    End If

                    Commit()
                    Return int_Valor

                Else
                    'str_Mensaje = "Ocurrio un error durante la actualizacion del registro."
                    Rollback()
                    Return int_Valor
                End If

            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante la actualizacion del registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()
            End Try

        End Function

        Public Function FUN_DEL_SubbloqueMenu(ByVal objSubbloqueMenu As be_SubbloquesMenus, _
                                              ByVal objDetalle As DataSet, _
                                              ByRef str_Mensaje As String, _
                                              ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_CodigoBloqueInformacion As Integer = 0
            Dim int_CodigoAsignacion As Integer = 0
            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0
            Dim int_ValorAsignacion As Integer = 0
            Dim int_ValorAccion As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()

                int_Valor = objSubbloqueMenu.CodigoSubBloque

                If int_Valor > 0 Then 'Si actualizo la cabecera

                    'DataTable.Tables(0) - Detalle de Bloque de Informacion : Registros Nuevos
                    'DataTable.Tables(1) - Detalle de Bloque de Informacion : Registros Eliminados
                    'DataTable.Tables(2) - Detalle de Acciones : Registros Nuevos
                    'DataTable.Tables(3) - Detalle de Acciones : Registros Eliminados
                    'DataTable.Tables(4) - Detalle de Bloque de Informacion : Registros Constantes

                    'Si tiene almenos 1 registro y no es el registro CodigoRelacion = -1 entonces grabo 
                    If objDetalle.Tables(1).Rows.Count > 0 Then
                        If objDetalle.Tables(1).Rows(0).Item("CodigoRelacion") <> -1 Then

                            Dim objAsignacionBloquesInformacion As be_AsignacionBloquesInformacion
                            'DataTable.Tables(1) - Detalle de Bloque de Informacion : Registros Eliminados
                            For Each dr As DataRow In objDetalle.Tables(1).Rows
                                objAsignacionBloquesInformacion = New be_AsignacionBloquesInformacion
                                objAsignacionBloquesInformacion.CodigoAsignacion = dr.Item("CodigoRelacion")

                                If objAsignacionBloquesInformacion.CodigoAsignacion > 0 Then 'Elimino todas las acciones dependientes

                                    int_CodigoAsignacion = objAsignacionBloquesInformacion.CodigoAsignacion
                                    Dim objAccionesAcceso As be_AccionesAcceso

                                    'DataTable.Tables(3) - Detalle de Acciones : Registros Eliminados
                                    For Each drA As DataRow In objDetalle.Tables(3).Rows

                                        If drA.Item("CodigoBloqueInformacion") = int_CodigoAsignacion Then
                                            objAccionesAcceso = New be_AccionesAcceso
                                            objAccionesAcceso.CodigoAccion = drA.Item("CodigoRelacion")
                                            int_ValorAccion = FUN_DEL_AccionesAcceso(objAccionesAcceso.CodigoAccion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                            If int_ValorAccion < 1 Then 'Error en eliminacion de AccionesAcceso
                                                str_Mensaje = "Ocurrio un error durante la eliminación del registro."
                                                Rollback()
                                                Return int_ValorAccion
                                            End If
                                        End If

                                    Next

                                End If

                                int_ValorAsignacion = FUN_DEL_AsignacionBloquesInformacion(objAsignacionBloquesInformacion.CodigoAsignacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                                If int_ValorAsignacion < 1 Then 'Error en la eliminacion de Bloques de Informacion
                                    str_Mensaje = "Ocurrio un error durante la eliminación del registro."
                                    Rollback()
                                    Return int_ValorAsignacion
                                End If

                            Next

                        End If
                    End If

                    dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_DEL_SubbloquesMenus")
                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_Valor)

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    'Parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)
                    'Ejecucion del Store Procedure
                    dbBase.ExecuteScalar(dbCommand, tran)

                    str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    int_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                    If int_Valor > 0 Then
                        Commit()
                        Return int_Valor
                    Else
                        str_Mensaje = "Ocurrio un error durante la eliminación del registro."
                        Rollback()
                        Return int_Valor
                    End If

                Else
                    str_Mensaje = "Ocurrio un error durante la eliminación del registro."
                    Rollback()
                    Return int_Valor
                End If

            Catch ex As Exception
                str_Mensaje = "Ocurrio un error durante la eliminación del registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()
            End Try
        End Function

        'Bloques de Informacion
        Private Function FUN_INS_AsignacionBloquesInformacion(ByVal objAsignacionBloquesInformacion As be_AsignacionBloquesInformacion, ByVal objSqlTransaction As SqlTransaction, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_Mensaje As String = ""

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_AsignacionesBloquesInformacion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoBloqueInformacion", DbType.Int32, objAsignacionBloquesInformacion.CodigoBloqueInformacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoSubBloque", DbType.Int32, objAsignacionBloquesInformacion.CodigoSubBloque)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)

            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_DEL_AsignacionBloquesInformacion(ByVal int_CodigoRelacion As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_Mensaje As String = ""

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_DEL_AsignacionesBloquesInformacion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoRelacion", DbType.Int16, int_CodigoRelacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)

            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        'Acciones Acceso
        Private Function FUN_INS_AccionesAcceso(ByVal objAccionesAcceso As be_AccionesAcceso, ByVal objSqlTransaction As SqlTransaction, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_Mensaje As String = ""

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_INS_AccionesAcceso")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoNombreAccion", DbType.Int32, objAccionesAcceso.CodigoNombreAccion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacion", DbType.Int32, objAccionesAcceso.CodigoAsignacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)

            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Private Function FUN_DEL_AccionesAcceso(ByVal int_CodigoRelacion As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_Mensaje As String = ""

            dbCommand = Me.dbBase.GetStoredProcCommand("CF_USP_DEL_AccionesAcceso")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoRelacion", DbType.Int16, int_CodigoRelacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int32, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)

            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_SubbloqueMenu(ByVal int_SubBloque As Integer, ByVal str_Descripcion As String, ByVal int_Estado As Integer, ByVal int_EstadoProceso As Integer, ByVal int_TipoSubBloque As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CF_USP_LIS_SubbloquesMenus")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoBloque", DbType.Int32, int_SubBloque)
            dbBase.AddInParameter(cmd, "@p_Descripcion", DbType.String, str_Descripcion)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int32, int_Estado)
            dbBase.AddInParameter(cmd, "@p_EstadoProceo", DbType.Int32, int_EstadoProceso)
            dbBase.AddInParameter(cmd, "@p_TipoSubBloque", DbType.Int32, int_TipoSubBloque)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_SubbloqueMenu(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CF_USP_GET_SubbloquesMenus")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_DEL_ValidarSubbloqueMenu(ByVal int_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("CF_USP_DEL_ValidarSubbloquesMenus")
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
