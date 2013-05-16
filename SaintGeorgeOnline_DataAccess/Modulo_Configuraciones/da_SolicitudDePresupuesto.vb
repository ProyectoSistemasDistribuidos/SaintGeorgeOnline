Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones

Namespace ModuloConfiguraciones

    Public Class da_SolicitudDePresupuesto
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

        Public Function FUN_INS_SolicitudDePresupuesto(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_ValorDetalleSubCategoria As Integer = 0
            Dim int_ValorDetalleArticulo As Integer = 0

            Dim str_MensajeDetalleSubCategoria As String = ""
            Dim str_MensajeDetallearticulo As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()
                dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_INS_SolicitudPresupuesto")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_CodigoAsignacionSSSCentroCosto", DbType.Int32, int_CodigoAsignacionSSSCentroCosto)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor"))) ' SP_CodigoSolicitudPresupuesto

                'Detalle Subcategoria y Articulos
                If int_Valor > 0 Then

                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables.Count > 1 Then ' Si presenta los 2 detalles : "SubCategoria" y "Artículos"

                            If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "SubCategoria", lo grabo
                                For Each drC As DataRow In objDetalle.Tables(0).Rows
                                    ' Registro de las SubCategorias
                                    dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_INS_DetalleSolicitudSubCategoria")

                                    'Parámetros de entrada
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_Valor)
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoSSCentroCostoSubCategoria", DbType.Int32, drC.Item("CodigoSSSCentroCostoSubCategoria"))

                                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                    'Parámetros de salida
                                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                    'Ejecucion del Store Procedure
                                    dbBase.ExecuteScalar(dbCommand, tran)
                                    str_MensajeDetalleSubCategoria = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                    int_ValorDetalleSubCategoria = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                    If int_ValorDetalleSubCategoria > 0 Then

                                        If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "Artículos", lo grabo
                                            For Each drA As DataRow In objDetalle.Tables(1).Rows

                                                If drC.Item("CodigoSSSCentroCostoSubCategoria") = drA.Item("CodigoSSSCentroCostoSubCategoria") Then


                                                    ' CASO : INSERT
                                                    If drA.Item("EnDetalleArticulos") = "Si" Then

                                                        If drA.Item("CodigoDetalleSolicitudArticulo") > 0 Then ' UPDATE
                                                            ' Registro de los Artículos
                                                            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_DetalleSolicitudArticulos")

                                                            'Parámetros de entrada
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, drA.Item("CodigoDetalleSolicitudArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraArticulo", DbType.Int32, drA.Item("CodigoEstructuraArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuesto", DbType.Int32, int_ValorDetalleSubCategoria)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoPrioridad", DbType.Int16, drA.Item("CodigoPrioridad"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Cantidad", DbType.Decimal, drA.Item("Cantidad"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, drA.Item("Observacion"))
                                                            dbBase.AddInParameter(dbCommand, "@p_UnidadMedida", DbType.String, drA.Item("UnidadMedida"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Precio", DbType.Decimal, drA.Item("PrecioDetalle"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int16, drA.Item("CodigoMonedaDetalle"))

                                                            dbBase.AddInParameter(dbCommand, "@p_CheckEne", DbType.Int16, drA.Item("checkENE"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantEne", DbType.Int16, drA.Item("CantENE"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckFeb", DbType.Int16, drA.Item("checkFEB"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantFeb", DbType.Int16, drA.Item("CantFEB"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckMar", DbType.Int16, drA.Item("checkMAR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantMar", DbType.Int16, drA.Item("CantMAR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckAbr", DbType.Int16, drA.Item("checkABR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantAbr", DbType.Int16, drA.Item("CantABR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckMay", DbType.Int16, drA.Item("checkMAY"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantMay", DbType.Int16, drA.Item("CantMAY"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckJun", DbType.Int16, drA.Item("checkJUN"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantJun", DbType.Int16, drA.Item("CantJUN"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckJul", DbType.Int16, drA.Item("checkJUL"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantJul", DbType.Int16, drA.Item("CantJUL"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckAgo", DbType.Int16, drA.Item("checkAGO"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantAgo", DbType.Int16, drA.Item("CantAGO"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckSet", DbType.Int16, drA.Item("checkSET"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantSet", DbType.Int16, drA.Item("CantSET"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckOct", DbType.Int16, drA.Item("checkOCT"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantOct", DbType.Int16, drA.Item("CantOCT"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckNov", DbType.Int16, drA.Item("checkNOV"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantNov", DbType.Int16, drA.Item("CantNOV"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckDic", DbType.Int16, drA.Item("checkDIC"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantDic", DbType.Int16, drA.Item("CantDIC"))


                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                                            'Parámetros de salida
                                                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                                            'Ejecucion del Store Procedure
                                                            dbBase.ExecuteScalar(dbCommand, tran)
                                                            str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                                            int_ValorDetalleArticulo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                                            If Not int_ValorDetalleArticulo > 0 Then
                                                                Rollback()
                                                                str_Mensaje = str_MensajeDetalleSubCategoria
                                                                Return int_ValorDetalleSubCategoria
                                                            End If

                                                        Else ' INSERT

                                                            ' Registro de los Artículos
                                                            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_INS_DetalleSolicitudArticulos")

                                                            'Parámetros de entrada
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraArticulo", DbType.Int32, drA.Item("CodigoEstructuraArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuesto", DbType.Int32, int_ValorDetalleSubCategoria)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoPrioridad", DbType.Int16, drA.Item("CodigoPrioridad"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Cantidad", DbType.Decimal, drA.Item("Cantidad"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, drA.Item("Observacion"))
                                                            dbBase.AddInParameter(dbCommand, "@p_UnidadMedida", DbType.String, drA.Item("UnidadMedida"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Precio", DbType.Decimal, drA.Item("PrecioDetalle"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int16, drA.Item("CodigoMonedaDetalle"))

                                                            dbBase.AddInParameter(dbCommand, "@p_CheckEne", DbType.Int16, drA.Item("checkENE"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantEne", DbType.Int16, drA.Item("CantENE"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckFeb", DbType.Int16, drA.Item("checkFEB"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantFeb", DbType.Int16, drA.Item("CantFEB"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckMar", DbType.Int16, drA.Item("checkMAR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantMar", DbType.Int16, drA.Item("CantMAR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckAbr", DbType.Int16, drA.Item("checkABR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantAbr", DbType.Int16, drA.Item("CantABR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckMay", DbType.Int16, drA.Item("checkMAY"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantMay", DbType.Int16, drA.Item("CantMAY"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckJun", DbType.Int16, drA.Item("checkJUN"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantJun", DbType.Int16, drA.Item("CantJUN"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckJul", DbType.Int16, drA.Item("checkJUL"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantJul", DbType.Int16, drA.Item("CantJUL"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckAgo", DbType.Int16, drA.Item("checkAGO"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantAgo", DbType.Int16, drA.Item("CantAGO"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckSet", DbType.Int16, drA.Item("checkSET"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantSet", DbType.Int16, drA.Item("CantSET"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckOct", DbType.Int16, drA.Item("checkOCT"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantOct", DbType.Int16, drA.Item("CantOCT"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckNov", DbType.Int16, drA.Item("checkNOV"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantNov", DbType.Int16, drA.Item("CantNOV"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckDic", DbType.Int16, drA.Item("checkDIC"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantDic", DbType.Int16, drA.Item("CantDIC"))


                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                                            'Parámetros de salida
                                                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                                            'Ejecucion del Store Procedure
                                                            dbBase.ExecuteScalar(dbCommand, tran)
                                                            str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                                            int_ValorDetalleArticulo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                                            If Not int_ValorDetalleArticulo > 0 Then
                                                                Rollback()
                                                                str_Mensaje = str_MensajeDetalleSubCategoria
                                                                Return int_ValorDetalleSubCategoria
                                                            End If

                                                        End If

                                                    Else ' drA.Item("EnDetalleArticulos") = "No" Then

                                                        If drA.Item("CodigoDetalleSolicitudArticulo") > 0 Then

                                                            ' Registro de los Artículos
                                                            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_DEL_DetalleSolicitudArticulos")

                                                            'Parámetros de entrada
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, drA.Item("CodigoDetalleSolicitudArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraArticulo", DbType.Int32, drA.Item("CodigoEstructuraArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuesto", DbType.Int32, int_ValorDetalleSubCategoria)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                                            'Parámetros de salida
                                                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                                            'Ejecucion del Store Procedure
                                                            dbBase.ExecuteScalar(dbCommand, tran)
                                                            str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                                            int_ValorDetalleArticulo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                                            If Not int_ValorDetalleArticulo > 0 Then
                                                                Rollback()
                                                                str_Mensaje = str_MensajeDetalleSubCategoria
                                                                Return int_ValorDetalleSubCategoria
                                                            End If

                                                        End If


                                                    End If

                                                End If

                                            Next
                                        End If

                                    Else
                                        Rollback()
                                        str_Mensaje = str_MensajeDetalleSubCategoria
                                        Return int_ValorDetalleSubCategoria
                                    End If
                                Next
                            Else ' rollback
                                Rollback()
                                Return int_Valor
                            End If

                        Else ' rollback
                            Rollback()
                            Return int_Valor
                        End If
                    Else 'rollback
                        Rollback()
                        Return int_Valor
                    End If

                Else 'rollback
                    Rollback()
                    Return int_Valor
                End If

                Commit()
                Return int_Valor

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_UPD_EstadoSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByRef str_Mensaje As String, ByRef strNombrePresupuesto As String, ByRef str_nombrePersona As String, ByRef nombreArea As String, ByRef nombreRemitente As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("USP_UdpEstadoReponsableValidacion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@SP_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            ''Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@nombrePresupuesto", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@nombrePersona", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@nombreArea", DbType.String, 255)

            dbBase.AddOutParameter(dbCommand, "@nombreRemitente", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
            strNombrePresupuesto = dbBase.GetParameterValue(dbCommand, "@nombrePresupuesto").ToString()
            str_nombrePersona = dbBase.GetParameterValue(dbCommand, "@nombrePersona").ToString()
            nombreArea = dbBase.GetParameterValue(dbCommand, "@nombreArea").ToString()
            nombreRemitente = dbBase.GetParameterValue(dbCommand, "@nombreRemitente").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@valor")))

        End Function

        Public Function FUN_UPD_ValidacionSolicitudDePresupuesto(ByVal objDetalle As DataTable, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MensajeDetallearticulo As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                If objDetalle.Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "Artículos", lo grabo
                    For Each drA As DataRow In objDetalle.Rows

                        ' Registro de los Artículos
                        dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_ValidarDetalleSolicitudArticulos")

                        'Parámetros de entrada
                        dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, drA.Item("CodigoDetalleSolicitudArticulo"))
                       
                        dbBase.AddInParameter(dbCommand, "@p_CodigoEstadoValidaciones", DbType.Int16, drA.Item("CodigoEstadoValidacion"))
                        dbBase.AddInParameter(dbCommand, "@p_CantidadArticuloValidado", DbType.Decimal, drA.Item("CantidadValidado"))
                        dbBase.AddInParameter(dbCommand, "@p_ObservacionValidado", DbType.String, drA.Item("ObservacionValidado"))

                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                        'Parámetros de salida
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                        'Ejecucion del Store Procedure
                        dbBase.ExecuteScalar(dbCommand, tran)
                        str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                        int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                        If Not int_Valor > 0 Then
                            Rollback()
                            str_Mensaje = str_MensajeDetallearticulo
                            Return int_Valor
                        End If

                    Next

                    Commit()
                    str_Mensaje = str_MensajeDetallearticulo
                    Return int_Valor

                Else ' Rollback

                    Rollback()
                    Return int_Valor

                End If

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_UPD_CorreccionSolicitudDePresupuesto(ByVal objDetalle As DataTable, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MensajeDetallearticulo As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                If objDetalle.Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "Artículos", lo grabo
                    For Each drA As DataRow In objDetalle.Rows

                        If drA.Item("CodigoEstadoValidacion") = 3 Then ' Estado pendiente de corrección

                            ' Registro de los Artículos
                            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_CorreccionDetalleSolicitudArticulos")

                            'Parámetros de entrada
                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, drA.Item("CodigoDetalleSolicitudArticulo"))
                            dbBase.AddInParameter(dbCommand, "@p_CantidadArticuloCorrejido", DbType.Decimal, drA.Item("Cantidad"))


                            dbBase.AddInParameter(dbCommand, "@p_CheckEne", DbType.Int16, drA.Item("checkENE"))
                            dbBase.AddInParameter(dbCommand, "@p_CantEne", DbType.Int16, drA.Item("CantENE"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckFeb", DbType.Int16, drA.Item("checkFEB"))
                            dbBase.AddInParameter(dbCommand, "@p_CantFeb", DbType.Int16, drA.Item("CantFEB"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckMar", DbType.Int16, drA.Item("checkMAR"))
                            dbBase.AddInParameter(dbCommand, "@p_CantMar", DbType.Int16, drA.Item("CantMAR"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckAbr", DbType.Int16, drA.Item("checkABR"))
                            dbBase.AddInParameter(dbCommand, "@p_CantAbr", DbType.Int16, drA.Item("CantABR"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckMay", DbType.Int16, drA.Item("checkMAY"))
                            dbBase.AddInParameter(dbCommand, "@p_CantMay", DbType.Int16, drA.Item("CantMAY"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckJun", DbType.Int16, drA.Item("checkJUN"))
                            dbBase.AddInParameter(dbCommand, "@p_CantJun", DbType.Int16, drA.Item("CantJUN"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckJul", DbType.Int16, drA.Item("checkJUL"))
                            dbBase.AddInParameter(dbCommand, "@p_CantJul", DbType.Int16, drA.Item("CantJUL"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckAgo", DbType.Int16, drA.Item("checkAGO"))
                            dbBase.AddInParameter(dbCommand, "@p_CantAgo", DbType.Int16, drA.Item("CantAGO"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckSet", DbType.Int16, drA.Item("checkSET"))
                            dbBase.AddInParameter(dbCommand, "@p_CantSet", DbType.Int16, drA.Item("CantSET"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckOct", DbType.Int16, drA.Item("checkOCT"))
                            dbBase.AddInParameter(dbCommand, "@p_CantOct", DbType.Int16, drA.Item("CantOCT"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckNov", DbType.Int16, drA.Item("checkNOV"))
                            dbBase.AddInParameter(dbCommand, "@p_CantNov", DbType.Int16, drA.Item("CantNOV"))
                            dbBase.AddInParameter(dbCommand, "@p_CheckDic", DbType.Int16, drA.Item("checkDIC"))
                            dbBase.AddInParameter(dbCommand, "@p_CantDic", DbType.Int16, drA.Item("CantDIC"))


                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                            'Parámetros de salida
                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                            'Ejecucion del Store Procedure
                            dbBase.ExecuteScalar(dbCommand, tran)
                            str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                            int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                            If Not int_Valor > 0 Then
                                Rollback()
                                str_Mensaje = str_MensajeDetallearticulo
                                Return int_Valor
                            End If

                        End If

                    Next

                    Commit()
                    str_Mensaje = str_MensajeDetallearticulo
                    Return int_Valor

                Else ' Rollback

                    Rollback()
                    Return int_Valor

                End If

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_UPD_EstadoValidacionSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_EstadoValidacionSolicitudPresupuesto")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_CodigoSolicitudPresupuesto)

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

        Public Function FUN_UPD_SolicitudDePresupuestoGerencia(ByVal objDetalle As DataTable, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim str_MensajeDetallearticulo As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                If objDetalle.Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "Artículos", lo grabo
                    For Each drA As DataRow In objDetalle.Rows

                        ' Registro de los Artículos                 
                        dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_ValidarDetalleSolicitudSubCategoriaGerencia")

                        'Parámetros de entrada
                        dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitudPresupuesto", DbType.Int32, drA.Item("CodigoSolicitudPresupuesto"))
                        dbBase.AddInParameter(dbCommand, "@p_CodigoSSCentroCostoSubCategoria", DbType.Int32, drA.Item("CodigoSSSCentroCostoSubCategoria"))

                        dbBase.AddInParameter(dbCommand, "@p_CodigoEstadoValidacion", DbType.Int16, drA.Item("CodigoEstadoValidacionFinal"))
                        dbBase.AddInParameter(dbCommand, "@p_MontoValidacionFinal", DbType.Decimal, drA.Item("MontoValidacionFinal"))
                        dbBase.AddInParameter(dbCommand, "@p_ObservacionValidacionFinal", DbType.String, drA.Item("ObservacionValidacionFinal"))

                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                        'Parámetros de salida
                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                        'Ejecucion del Store Procedure
                        dbBase.ExecuteScalar(dbCommand, tran)
                        str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                        int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                        If Not int_Valor > 0 Then
                            Rollback()
                            str_Mensaje = str_MensajeDetallearticulo
                            Return int_Valor
                        End If

                    Next

                    Commit()
                    str_Mensaje = str_MensajeDetallearticulo
                    Return int_Valor

                Else ' Rollback

                    Rollback()
                    Return int_Valor

                End If

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_UPD_ValidacionReasignacionSolicitudDePresupuesto( _
            ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByVal int_CodigoPresupuesto As Integer, _
            ByVal int_CodigoTrabajadorReasignador As Integer, _
            ByVal int_TipoReasignacion As Integer, _
            ByVal objDetalle As DataSet, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_ValorDetalleSubCategoria As Integer = 0
            Dim int_ValorDetalleArticulo As Integer = 0

            Dim str_MensajeDetalleSubCategoria As String = ""
            Dim str_MensajeDetallearticulo As String = ""

            Try

                'Inicio la transaccion
                BeginTransaction()

                If objDetalle.Tables(0) IsNot Nothing Then
                    If objDetalle.Tables.Count > 1 Then ' Si presenta los 2 detalles : "SubCategoria" y "Artículos"
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "SubCategoria", lo grabo
                            For Each drC As DataRow In objDetalle.Tables(0).Rows
                                ' Registro de las SubCategorias             
                                dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_INS_DetalleSolicitudSubCategoriaReasignacion")

                                'Parámetros de entrada
                                dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoSSCentroCostoSubCategoria", DbType.Int32, drC.Item("CodigoSSSCentroCostoSubCategoria"))
                                dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorReasignador", DbType.Int32, int_CodigoTrabajadorReasignador)
                                dbBase.AddInParameter(dbCommand, "@p_TipoReasignacion", DbType.Int32, int_TipoReasignacion)

                                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                                'Parámetros de salida
                                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                'Ejecucion del Store Procedure
                                dbBase.ExecuteScalar(dbCommand, tran)
                                str_MensajeDetalleSubCategoria = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                int_ValorDetalleSubCategoria = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                str_Mensaje = str_MensajeDetalleSubCategoria

                                If int_ValorDetalleSubCategoria > 0 Then
                                    If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro en el detalle "Artículos", lo grabo
                                        For Each drA As DataRow In objDetalle.Tables(1).Rows
                                            If drC.Item("CodigoSSSCentroCostoSubCategoria") = drA.Item("CodigoSSSCentroCostoSubCategoria") Then

                                                'Caso : Validación 
                                                If drA.Item("Reasignado") = "No" Then

                                                    If drA.Item("Tipo") = "R" And drA.Item("EnDetalleArticulos") = "Si" Then ' Update en los campos de validación de sistemas

                                                        ' Registro de los Artículos                 
                                                        dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_ValidarDetalleSolicitudArticulosSistemas")

                                                        'Parámetros de entrada
                                                        dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, drA.Item("CodigoDetalleSolicitudArticulo"))
                                                        dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorReasignador", DbType.Int32, int_CodigoTrabajadorReasignador)
                                                        dbBase.AddInParameter(dbCommand, "@p_TipoValidacion", DbType.Int32, int_TipoReasignacion)
                                                        dbBase.AddInParameter(dbCommand, "@p_CodigoEstadoValidacionesSistemas", DbType.Int16, drA.Item("CodigoEstadoValidacion"))
                                                        dbBase.AddInParameter(dbCommand, "@p_CantidadSistemas", DbType.Decimal, drA.Item("CantidadValidado"))
                                                        dbBase.AddInParameter(dbCommand, "@p_ObservacionSistemas", DbType.String, drA.Item("ObservacionValidado"))

                                                        dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                                        dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                                        dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                                        dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                                                        'Parámetros de salida
                                                        dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                                        dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                                        'Ejecucion del Store Procedure
                                                        dbBase.ExecuteScalar(dbCommand, tran)
                                                        str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                                        int_ValorDetalleArticulo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                                        If Not int_ValorDetalleArticulo > 0 Then
                                                            Rollback()
                                                            str_Mensaje = str_MensajeDetallearticulo
                                                            Return int_ValorDetalleArticulo
                                                        End If

                                                        str_Mensaje = str_MensajeDetallearticulo

                                                    End If

                                                    'Caso : Reasignación 
                                                ElseIf drA.Item("Reasignado") = "Si" Then

                                                    ' CASO : INSERT
                                                    If drA.Item("EnDetalleArticulos") = "Si" Then
                                                        If drA.Item("CodigoDetalleSolicitudArticulo") > 0 Then ' UPDATE

                                                            ' Registro de los Artículos                 
                                                            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_DetalleSolicitudArticulosSistemas")

                                                            'Parámetros de entrada
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, drA.Item("CodigoDetalleSolicitudArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraArticulo", DbType.Int32, drA.Item("CodigoEstructuraArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuesto", DbType.Int32, int_ValorDetalleSubCategoria)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorReasignador", DbType.Int32, int_CodigoTrabajadorReasignador)
                                                            dbBase.AddInParameter(dbCommand, "@p_TipoValidacion", DbType.Int32, int_TipoReasignacion)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoPrioridad", DbType.Int16, drA.Item("CodigoPrioridad"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Cantidad", DbType.Decimal, drA.Item("Cantidad"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, drA.Item("Observacion"))
                                                            dbBase.AddInParameter(dbCommand, "@p_UnidadMedida", DbType.String, drA.Item("UnidadMedida"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Precio", DbType.Decimal, drA.Item("PrecioDetalle"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int16, drA.Item("CodigoMonedaDetalle"))

                                                            dbBase.AddInParameter(dbCommand, "@p_CheckEne", DbType.Int16, drA.Item("checkENE"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantEne", DbType.Int16, drA.Item("CantENE"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckFeb", DbType.Int16, drA.Item("checkFEB"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantFeb", DbType.Int16, drA.Item("CantFEB"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckMar", DbType.Int16, drA.Item("checkMAR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantMar", DbType.Int16, drA.Item("CantMAR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckAbr", DbType.Int16, drA.Item("checkABR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantAbr", DbType.Int16, drA.Item("CantABR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckMay", DbType.Int16, drA.Item("checkMAY"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantMay", DbType.Int16, drA.Item("CantMAY"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckJun", DbType.Int16, drA.Item("checkJUN"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantJun", DbType.Int16, drA.Item("CantJUN"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckJul", DbType.Int16, drA.Item("checkJUL"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantJul", DbType.Int16, drA.Item("CantJUL"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckAgo", DbType.Int16, drA.Item("checkAGO"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantAgo", DbType.Int16, drA.Item("CantAGO"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckSet", DbType.Int16, drA.Item("checkSET"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantSet", DbType.Int16, drA.Item("CantSET"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckOct", DbType.Int16, drA.Item("checkOCT"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantOct", DbType.Int16, drA.Item("CantOCT"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckNov", DbType.Int16, drA.Item("checkNOV"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantNov", DbType.Int16, drA.Item("CantNOV"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckDic", DbType.Int16, drA.Item("checkDIC"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantDic", DbType.Int16, drA.Item("CantDIC"))


                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                                            'Parámetros de salida
                                                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                                            'Ejecucion del Store Procedure
                                                            dbBase.ExecuteScalar(dbCommand, tran)
                                                            str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                                            int_ValorDetalleArticulo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                                            If Not int_ValorDetalleArticulo > 0 Then
                                                                Rollback()
                                                                str_Mensaje = str_MensajeDetallearticulo
                                                                Return int_ValorDetalleSubCategoria
                                                            End If

                                                            str_Mensaje = str_MensajeDetallearticulo

                                                        Else ' INSERT

                                                            ' Registro de los Artículos            
                                                            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_INS_DetalleSolicitudArticulosSistemas")

                                                            'Parámetros de entrada
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraArticulo", DbType.Int32, drA.Item("CodigoEstructuraArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuesto", DbType.Int32, int_ValorDetalleSubCategoria)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorReasignador", DbType.Int32, int_CodigoTrabajadorReasignador)
                                                            dbBase.AddInParameter(dbCommand, "@p_TipoValidacion", DbType.Int32, int_TipoReasignacion)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoPrioridad", DbType.Int16, drA.Item("CodigoPrioridad"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Cantidad", DbType.Decimal, drA.Item("Cantidad"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, drA.Item("Observacion"))
                                                            dbBase.AddInParameter(dbCommand, "@p_UnidadMedida", DbType.String, drA.Item("UnidadMedida"))
                                                            dbBase.AddInParameter(dbCommand, "@p_Precio", DbType.Decimal, drA.Item("PrecioDetalle"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoMoneda", DbType.Int16, drA.Item("CodigoMonedaDetalle"))

                                                            dbBase.AddInParameter(dbCommand, "@p_CheckEne", DbType.Int16, drA.Item("checkENE"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantEne", DbType.Int16, drA.Item("CantENE"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckFeb", DbType.Int16, drA.Item("checkFEB"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantFeb", DbType.Int16, drA.Item("CantFEB"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckMar", DbType.Int16, drA.Item("checkMAR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantMar", DbType.Int16, drA.Item("CantMAR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckAbr", DbType.Int16, drA.Item("checkABR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantAbr", DbType.Int16, drA.Item("CantABR"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckMay", DbType.Int16, drA.Item("checkMAY"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantMay", DbType.Int16, drA.Item("CantMAY"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckJun", DbType.Int16, drA.Item("checkJUN"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantJun", DbType.Int16, drA.Item("CantJUN"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckJul", DbType.Int16, drA.Item("checkJUL"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantJul", DbType.Int16, drA.Item("CantJUL"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckAgo", DbType.Int16, drA.Item("checkAGO"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantAgo", DbType.Int16, drA.Item("CantAGO"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckSet", DbType.Int16, drA.Item("checkSET"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantSet", DbType.Int16, drA.Item("CantSET"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckOct", DbType.Int16, drA.Item("checkOCT"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantOct", DbType.Int16, drA.Item("CantOCT"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckNov", DbType.Int16, drA.Item("checkNOV"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantNov", DbType.Int16, drA.Item("CantNOV"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CheckDic", DbType.Int16, drA.Item("checkDIC"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CantDic", DbType.Int16, drA.Item("CantDIC"))


                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                                                            'Parámetros de salida
                                                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                                            'Ejecucion del Store Procedure
                                                            dbBase.ExecuteScalar(dbCommand, tran)
                                                            str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                                            int_ValorDetalleArticulo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                                            If Not int_ValorDetalleArticulo > 0 Then
                                                                Rollback()
                                                                str_Mensaje = str_MensajeDetallearticulo
                                                                Return int_ValorDetalleSubCategoria
                                                            End If

                                                            str_Mensaje = str_MensajeDetallearticulo

                                                        End If
                                                    Else ' drA.Item("EnDetalleArticulos") = "No" Then
                                                        If drA.Item("CodigoDetalleSolicitudArticulo") > 0 Then

                                                            ' Registro de los Artículos                 
                                                            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_DEL_DetalleSolicitudArticulosSistemas")

                                                            'Parámetros de entrada
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, drA.Item("CodigoDetalleSolicitudArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoEstructuraArticulo", DbType.Int32, drA.Item("CodigoEstructuraArticulo"))
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoDetalleSolicitudPresupuesto", DbType.Int32, int_ValorDetalleSubCategoria)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajadorReasignador", DbType.Int32, int_CodigoTrabajadorReasignador)
                                                            dbBase.AddInParameter(dbCommand, "@p_TipoValidacion", DbType.Int32, int_TipoReasignacion)

                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                                                            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                                                            'Parámetros de salida
                                                            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                                                            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                                                            'Ejecucion del Store Procedure
                                                            dbBase.ExecuteScalar(dbCommand, tran)
                                                            str_MensajeDetallearticulo = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                                                            int_ValorDetalleArticulo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                                                            If Not int_ValorDetalleArticulo > 0 Then
                                                                Rollback()
                                                                str_Mensaje = str_MensajeDetallearticulo
                                                                Return int_ValorDetalleSubCategoria
                                                            End If

                                                            str_Mensaje = str_MensajeDetallearticulo

                                                        End If
                                                    End If


                                                End If

                                            End If
                                        Next
                                    Else ' rollback
                                        Rollback()
                                        str_Mensaje = str_MensajeDetalleSubCategoria
                                        Return int_ValorDetalleSubCategoria
                                    End If
                                Else ' rollback
                                    Rollback()
                                    str_Mensaje = str_MensajeDetalleSubCategoria
                                    Return int_ValorDetalleSubCategoria
                                End If
                            Next

                        Else 'rollback
                            Rollback()
                            Return int_ValorDetalleSubCategoria
                        End If
                    End If
                Else
                    Rollback()
                    Return int_ValorDetalleSubCategoria
                End If

                Commit()
                Return int_ValorDetalleSubCategoria

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro."
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_UPD_EstadoValidacionSolicitudPresupuestoJefes( _
            ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByVal int_CodigoTrabajador As Integer, _
            ByRef int_EnviarEmail As Integer, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_EstadoValidacionSolicitudPresupuestoJefes")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_CodigoSolicitudPresupuesto)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTrabajador", DbType.Int32, int_CodigoTrabajador)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_EnviarEmail", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_EnviarEmail = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_EnviarEmail")))
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_EstadoSolicitudPresupuestoGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_EstadoSolicitudPresupuestoObservacionesGerencia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_CodigoSolicitudPresupuesto)

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

        Public Function FUN_UPD_AprobarSolicitudPresupuestoGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("PS_USP_UPD_AprobarSolicitudPresupuestoGerencia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int32, int_CodigoSolicitudPresupuesto)

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

        Public Function FUN_LIS_SolicitudPresupuestoYEstructuraSSSCentroCostoClases(ByVal int_CodigoAsignacionSSSCentroCosto As Integer, ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_SolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionSSSCentroCosto", DbType.Int32, int_CodigoAsignacionSSSCentroCosto)
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoAValidar(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_SolicitudPresupuestoAValidar")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_SolicitudPresupuestoYEstructuraSSSCentroCostoClases(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_REP_SolicitudPresupuestoYAsignacionEstructuraSSSCentroCostoClases")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_VAL_EnvioSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_VAL_EnvioSolicitudPresupuesto")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoACorregir(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_SolicitudPresupuestoACorregir")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_VAL_EnvioCorreccionSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_VAL_EnvioCorreccionSolicitudPresupuesto")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_VAL_AprobacionSolicitudPresupuesto(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_VAL_AprobacionSolicitudPresupuesto")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoAValidarGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_SolicitudPresupuestoAValidarGerencia")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoAValidarSistemas(ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_SolicitudPresupuestoValidacionSistemas")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_SolicitudPresupuestoYReasignacionEstructuraSSSCentroCostoClasesSistemas( _
            ByVal int_CodigoAsignacionSSSCentroCosto As Integer, _
            ByVal int_CodigoSolicitudPresupuesto As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_SolicitudPresupuestoYReasignacionEstructuraSSSCentroCostoClasesSistemas")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionSSSCentroCosto", DbType.Int32, int_CodigoAsignacionSSSCentroCosto)
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)
            'dbBase.AddInParameter(cmd, "@p_TipoReasignacion", DbType.Int32, int_TipoReasignacion)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_VAL_AprobacionSolicitudPresupuestoJefes(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_VAL_AprobacionSolicitudPresupuestoJefes")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DetalleSolicitudPresupuestoObservacionesGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_DetalleSolicitudPresupuestoObservacionesGerencia")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_VAL_AprobacionSolicitudPresupuestoGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_VAL_AprobacionSolicitudPresupuestoGerencia")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_DetalleSolicitudPresupuestoAprobadoGerencia(ByVal int_CodigoSolicitudPresupuesto As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_LIS_DetalleSolicitudPresupuestoAprobadoGerencia")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitudPresupuesto", DbType.Int32, int_CodigoSolicitudPresupuesto)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_SolicitudPresupuestosGerencia(ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("PS_USP_REP_SolicitudPresupuestosGerencia")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

#End Region

    End Class

End Namespace