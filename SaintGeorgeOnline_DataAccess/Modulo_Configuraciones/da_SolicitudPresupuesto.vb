Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessEntities


Public Class da_SolicitudPresupuesto

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




#Region "transaccional"

    Public Function FUN_INS_SolicitudPresupuesto(ByVal oBE_SolicitudPresupuesto As BE_SolicitudPresupuesto, ByVal obe_DetalleSolicitudSubCategoria As List(Of be_DetalleSolicitudSubCategoria), ByVal ListBE_PS_DetalleSolicitudArticulos As List(Of BE_PS_DetalleSolicitudArticulos), ByVal TJ_CodigoTrabajador As Integer) As List(Of Integer)

        Dim mensaje As String = ""
        Dim mensaje1 As String = ""
        Dim mensaje2 As String = ""
        Dim p_valor1 As Integer = 0
        Dim p_valor2 As Integer = 0
        Dim p_valor3 As Integer = 0
        Dim listasIds As New List(Of Integer)

        Try
            BeginTransaction()

            For indice As Integer = 0 To ListBE_PS_DetalleSolicitudArticulos.Count - 1


                dbCommand = dbBase.GetStoredProcCommand("insertarSolicitudPresupuesto")
                dbCommand.Parameters.Clear()

                dbBase.AddInParameter(dbCommand, "@CodigoSolicitudPresupuesto", DbType.Int32, oBE_SolicitudPresupuesto.CodigoSolicitudPresupuesto)
                dbBase.AddInParameter(dbCommand, "@CodigoTipoPresupuesto", DbType.Int32, oBE_SolicitudPresupuesto.CodigoTipoPresupuesto)

                dbBase.AddInParameter(dbCommand, "@CodigoEstadoPresupuesto", DbType.Int32, oBE_SolicitudPresupuesto.CodigoEstadoPresupuesto)
                dbBase.AddInParameter(dbCommand, "@CodigoAsignacionSSSCentroCosto", DbType.Int32, oBE_SolicitudPresupuesto.CodigoAsignacionSSSCentroCosto)

                dbBase.AddInParameter(dbCommand, "@FechaRegistro", DbType.DateTime, oBE_SolicitudPresupuesto.FechaRegistro)
                dbBase.AddInParameter(dbCommand, "@Estado", DbType.Boolean, oBE_SolicitudPresupuesto.Estado)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                p_valor1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))


                dbCommand = dbBase.GetStoredProcCommand("USP_INS_DetalleSolicitudSubCategoria")
                dbCommand.Parameters.Clear()
                ''
                dbBase.AddInParameter(dbCommand, "@DSPS_CodigoDetSolicitudPresupuestoSubCategoria", DbType.Int32, obe_DetalleSolicitudSubCategoria(indice).DSPS_CodigoDetSolicitudPresupuestoSubCategoria)
                dbBase.AddInParameter(dbCommand, "@SP_CodigoSolicitudPresupuesto", DbType.Int32, p_valor1)

                dbBase.AddInParameter(dbCommand, "@ASS_CodigoSSCentroCostoSubCategoria", DbType.Int32, obe_DetalleSolicitudSubCategoria(indice).ASS_CodigoSSCentroCostoSubCategoria)
                dbBase.AddInParameter(dbCommand, "@DSPS_Estado", DbType.Boolean, obe_DetalleSolicitudSubCategoria(indice).DSPS_Estado)

                dbBase.AddInParameter(dbCommand, "@ESA_CodigoEstadoValidaciones", DbType.Int32, obe_DetalleSolicitudSubCategoria(indice).ESA_CodigoEstadoValidaciones)
                dbBase.AddInParameter(dbCommand, "@DSPS_MontoValidacionFinal", DbType.Decimal, obe_DetalleSolicitudSubCategoria(indice).DSPS_MontoValidacionFinal)

                dbBase.AddInParameter(dbCommand, "@DSPS_FechaRegistroValidacion", DbType.DateTime, obe_DetalleSolicitudSubCategoria(indice).DSPS_FechaRegistroValidacion)
                dbBase.AddInParameter(dbCommand, "@DSPS_ObservacionValidacionFinal", DbType.String, obe_DetalleSolicitudSubCategoria(indice).DSPS_ObservacionValidacionFinal)

                dbBase.AddInParameter(dbCommand, "@DSPS_UsuarioReasignador", DbType.Int32, obe_DetalleSolicitudSubCategoria(indice).DSPS_UsuarioReasignador)
                dbBase.AddInParameter(dbCommand, "@DSPS_TipoReasignacion", DbType.Int32, obe_DetalleSolicitudSubCategoria(indice).DSPS_TipoReasignacion)


                dbBase.AddInParameter(dbCommand, "@ATP_CodigoEstructuraCategoria", DbType.Int32, obe_DetalleSolicitudSubCategoria(indice).codEstructuraCategoria)
                dbBase.AddInParameter(dbCommand, "@ASP_CodigoEstructuraSubCategoria", DbType.Int32, obe_DetalleSolicitudSubCategoria(indice).codEstrcuturaSubCat)
                dbBase.AddInParameter(dbCommand, "@ACP_CodigoEstructuraClase", DbType.Int32, obe_DetalleSolicitudSubCategoria(indice).codEstructuraClase)
                dbBase.AddInParameter(dbCommand, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int32, oBE_SolicitudPresupuesto.CodigoAsignacionSSSCentroCosto)


                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)



                '@ATP_CodigoEstructuraCategoria int ,
                '@ASP_CodigoEstructuraSubCategoria int ,
                '@ACP_CodigoEstructuraClase int ,
                '@ASSSCC_CodigoAsignacionSSSCentroCosto int ,



                dbBase.ExecuteScalar(dbCommand, tran)

                p_valor2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                mensaje2 = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))

                ''USP_UDP_DetalleSolicitudArticulos


                dbCommand.Parameters.Clear()
                If ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CodigoDetalleSolicitudPresupuestoArticulo = 0 Then
                    dbCommand = dbBase.GetStoredProcCommand("USP_INS_PS_DetalleSolicitudArticulos")
                    dbCommand.Parameters.Clear()
                ElseIf ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CodigoDetalleSolicitudPresupuestoArticulo <> 0 Then
                    dbCommand = dbBase.GetStoredProcCommand("USP_UDP_DetalleSolicitudArticulos")
                    dbCommand.Parameters.Clear()
                End If
                ''
                dbBase.AddInParameter(dbCommand, "@DSPA_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CodigoDetalleSolicitudPresupuestoArticulo)
                dbBase.AddInParameter(dbCommand, "@AAP_CodigoEstructuraArticulo", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).AAP_CodigoEstructuraArticulo)
                dbBase.AddInParameter(dbCommand, "@DSP_CodigoDetalleSolicitudPresupuesto", DbType.Int32, p_valor2)

                dbBase.AddInParameter(dbCommand, "@PD_CodigoPrioridad", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).PD_CodigoPrioridad)
                dbBase.AddInParameter(dbCommand, "@DSPA_Cantidad", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_Cantidad)
                dbBase.AddInParameter(dbCommand, "@DSPA_UnidadMedida", DbType.String, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_UnidadMedida)
                dbBase.AddInParameter(dbCommand, "@DSPA_Observacion", DbType.String, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_Observacion)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckEne", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckEne)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantEne", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantEne)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckFeb", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckFeb)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantFeb", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantFeb)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckMar", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckMar)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantMar", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantMar)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckAbr", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckAbr)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantAbr", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantAbr)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckMay", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckMay)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantMay", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantMay)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckJun", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckJun)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantJun", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantJun)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckJul", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckJul)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantJul", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantJul)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckAgo", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckAgo)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantAgo", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantAgo)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckSet", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckSet)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantSet", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantSet)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckOct", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckOct)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantOct", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantOct)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckNov", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckNov)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantNov", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantNov)
                dbBase.AddInParameter(dbCommand, "@DSPA_CheckDic", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CheckDic)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantDic", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantDic)
                dbBase.AddInParameter(dbCommand, "@DSPA_Estado", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_Estado)
                dbBase.AddInParameter(dbCommand, "@ESA_CodigoEstadoValidaciones", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).ESA_CodigoEstadoValidaciones)
                dbBase.AddInParameter(dbCommand, "@ESA_CantidadArticuloValidado", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).ESA_CantidadArticuloValidado)
                dbBase.AddInParameter(dbCommand, "@ESA_ObservacionArticuloValidado", DbType.String, ListBE_PS_DetalleSolicitudArticulos(indice).ESA_ObservacionArticuloValidado)
                dbBase.AddInParameter(dbCommand, "@ESA_FechaRegistroValidacion", DbType.DateTime, ListBE_PS_DetalleSolicitudArticulos(indice).ESA_FechaRegistroValidacion)
                dbBase.AddInParameter(dbCommand, "@DSPA_Precio", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_Precio)
                dbBase.AddInParameter(dbCommand, "@DSPA_PrecioValidado", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_PrecioValidado)
                dbBase.AddInParameter(dbCommand, "@MD_CodigoMoneda", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).MD_CodigoMoneda)
                dbBase.AddInParameter(dbCommand, "@DSPA_UsuarioReasignador", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_UsuarioReasignador)
                dbBase.AddInParameter(dbCommand, "@DSPA_ObservacionSistemas", DbType.String, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_ObservacionSistemas)
                dbBase.AddInParameter(dbCommand, "@DSPA_CantidadSistemas", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CantidadSistemas)
                dbBase.AddInParameter(dbCommand, "@DSPA_CodigoEstadoValidacionSistemas", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_CodigoEstadoValidacionSistemas)
                dbBase.AddInParameter(dbCommand, "@DSPA_PrecioSistemas", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_PrecioSistemas)
                dbBase.AddInParameter(dbCommand, "@DSPA_MonedaSistemas", DbType.Decimal, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_MonedaSistemas)
                dbBase.AddInParameter(dbCommand, "@DSPA_TipoReasignacion", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_TipoReasignacion)
                dbBase.AddInParameter(dbCommand, "@DSPA_UnidadSistemas", DbType.String, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_UnidadSistemas)
                dbBase.AddInParameter(dbCommand, "@DSPA_TipoValidacion", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_TipoValidacion)
                dbBase.AddInParameter(dbCommand, "@DSPA_TipoDistribucion", DbType.Boolean, ListBE_PS_DetalleSolicitudArticulos(indice).DSPA_TipoDistribucion)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)

                dbBase.AddInParameter(dbCommand, "@TJ_CodigoTrabajador", DbType.Int32, TJ_CodigoTrabajador)
                dbBase.AddInParameter(dbCommand, "@DSPA_InformacionCompleta", DbType.Int32, ListBE_PS_DetalleSolicitudArticulos(indice).listoEnviar)



                dbBase.ExecuteScalar(dbCommand, tran)
                p_valor3 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                mensaje2 = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                listasIds.Add(p_valor3)
                ''

            Next

            ''Rollback()
            Commit()
            Return listasIds
        Catch ex As Exception
            Rollback()
        Finally
            Conexion.Close()
            Conexion.Dispose()
            dbCommand.Dispose()

        End Try

    End Function

    Function fEliminarFilas(ByVal DSPA_CodigoDetalleSolicitudPresupuestoArticulo As Integer, ByVal codTrabajador As Integer) As Object
        Dim lstRes As New List(Of String)

        Try

            dbCommand = dbBase.GetStoredProcCommand("USP_Del_PS_DetalleSolicitudArticulos")
            dbCommand.Parameters.Clear()
            dbBase.AddInParameter(dbCommand, "@DSPA_CodigoDetalleSolicitudPresupuestoArticulo", DbType.Int32, DSPA_CodigoDetalleSolicitudPresupuestoArticulo)

            dbBase.AddInParameter(dbCommand, "@codTrabajador", DbType.Int32, codTrabajador)

            dbBase.AddOutParameter(dbCommand, "@mesanje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@valor", DbType.Int32, 100)
            dbBase.ExecuteScalar(dbCommand)
            Dim p_valor3 As Integer = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@valor")))

            Dim mensaje As String = CStr(dbBase.GetParameterValue(dbCommand, "@mesanje"))

 



            dbBase.ExecuteScalar(dbCommand)
            Return New With {.codigo = p_valor3, .mensaje = mensaje}
        Catch ex As Exception
        Finally


        End Try
    End Function

    ''' <summary>
    ''' funcion par validar el prespouesto 
    ''' 
    ''' </summary>
    ''' <param name="ASSSCC_CodigoAsignacionSSSCentroCosto"></param>
    ''' <param name="EP_CodigoEstadoPresupuesto"></param>
    ''' <param name="mensaje"></param>
    ''' <param name="nombrePresupuesto"></param>
    ''' <param name="nombrePersona"></param>
    ''' <param name="nombreArea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fValidarPresupuestoSiguienteValidador(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal EP_CodigoEstadoPresupuesto As Integer, _
                                                       ByRef mensaje As String, ByRef nombrePresupuesto As String, ByRef nombrePersona As String, ByRef nombreArea As String, ByRef ARVP_CodigoResponsableValidarPresupuesto As Integer) As Integer

        Try
            '  ASSSCC_CodigoAsignacionSSSCentroCosto INT ,
            'EP_CodigoEstadoPresupuesto int ,
            'mensaje varchar(max) out ,        
            'nombrePresupuesto  varchar(max) out   ,  
            'nombrePersona varchar(max) out    ,  
            'nombreArea varchar(max) out 
            Dim codigo As Integer = 0

            Try
                dbCommand = dbBase.GetStoredProcCommand("USP_ActEstadoPrespuesto_1")

                dbBase.AddInParameter(dbCommand, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int32, ASSSCC_CodigoAsignacionSSSCentroCosto)
                dbBase.AddInParameter(dbCommand, "@EP_CodigoEstadoPresupuesto", DbType.Int32, EP_CodigoEstadoPresupuesto)

                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@nombrePresupuesto", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@nombrePersona", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@nombreArea", DbType.String, 255)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                dbBase.AddOutParameter(dbCommand, "@ARVP_CodigoResponsableValidarPresupuesto", DbType.Int32, 100)

                dbBase.ExecuteScalar(dbCommand)
                mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                nombrePresupuesto = CStr(dbBase.GetParameterValue(dbCommand, "@nombrePresupuesto"))
                nombrePersona = CStr(dbBase.GetParameterValue(dbCommand, "@nombrePersona"))
                nombreArea = CStr(dbBase.GetParameterValue(dbCommand, "@nombreArea"))

                ARVP_CodigoResponsableValidarPresupuesto = CInt(dbBase.GetParameterValue(dbCommand, "@ARVP_CodigoResponsableValidarPresupuesto"))

                Return CInt(dbBase.GetParameterValue(dbCommand, "@codigo"))

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Function
#End Region


#Region "no transaccional"

    Function listarSolicitudPresupuesto(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal ACP_CodigoEstructuraClase As Integer, ByVal DSPS_CodigoDetSolicitudPresupuestoSubCategoria As Integer) As DataTable

        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("listarSolicitudes")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int32, ASSSCC_CodigoAsignacionSSSCentroCosto)
                dbBase.AddInParameter(cmd, "@ACP_CodigoEstructuraClase", DbType.Int32, ACP_CodigoEstructuraClase)
                dbBase.AddInParameter(cmd, "@DSPS_CodigoDetSolicitudPresupuestoSubCategoria", DbType.Int32, DSPS_CodigoDetSolicitudPresupuestoSubCategoria)

                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function

    Function listarSolicitudPresupuestoValidarArticulo(ByVal ASSSCC_CodigoAsignacionSSSCentroCosto As Integer, ByVal ACP_CodigoEstructuraClase As Integer, ByVal DSPS_CodigoDetSolicitudPresupuestoSubCategoria As Integer, ByVal RVP_CodigoRegistroValidacionesPresupuesto As Integer) As DataTable

        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("listarSolicitudesValidarArticulo")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int32, ASSSCC_CodigoAsignacionSSSCentroCosto)
                dbBase.AddInParameter(cmd, "@ACP_CodigoEstructuraClase", DbType.Int32, ACP_CodigoEstructuraClase)
                dbBase.AddInParameter(cmd, "@DSPS_CodigoDetSolicitudPresupuestoSubCategoria", DbType.Int32, DSPS_CodigoDetSolicitudPresupuestoSubCategoria)

                dbBase.AddInParameter(cmd, "@RVP_CodigoRegistroValidacionesPresupuesto", DbType.Int32, RVP_CodigoRegistroValidacionesPresupuesto)
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function

    Function fListarClaseCategoriaSubcategoriaObservadosPresupuesto(ByVal SP_CodigoSolicitudPresupuesto As Integer, ByVal ARVP_CodigoResponsableValidarPresupuesto As Integer) As DataTable

        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_ListarObservaciones_1")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@SP_CodigoSolicitudPresupuesto", DbType.Int32, SP_CodigoSolicitudPresupuesto)
                dbBase.AddInParameter(cmd, "@ARVP_CodigoResponsableValidarPresupuesto", DbType.Int32, ARVP_CodigoResponsableValidarPresupuesto)










                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function

#End Region
End Class
