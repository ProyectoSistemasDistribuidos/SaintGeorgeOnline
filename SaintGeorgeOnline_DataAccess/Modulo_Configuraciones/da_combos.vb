Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_BusinessEntities
Imports System.Xml.Serialization
Imports System.IO

Public Class da_combos
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

#End Region
#Region "no transaccional"
    Public Function Lis_categorias(ByVal int_codigoEstructuraClase As Integer, ByVal CentroCosto As Integer) As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("listarCategoriasXClasePresupuesto")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@codigoEstructuraClase", DbType.Int32, int_codigoEstructuraClase)
                dbBase.AddInParameter(cmd, "@CentroCosto", DbType.Int32, CentroCosto)


                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using


        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Function Lis_ArticuloTipoArticulo(ByVal int_tipoArticulo As Integer) As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("listarItemTipoItem")
                dbBase.AddInParameter(cmd, "@tipoItem", DbType.Int32, int_tipoArticulo)
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using



        Catch ex As Exception

        End Try

    End Function
    Public Function Lis_TipoArticulo() As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USp_lisTipoItem")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using



        Catch ex As Exception

        End Try

    End Function
    Public Function Lis_SubcategoriasCategorias(ByVal int_CodigoEstructuraCategoria As Integer, ByVal centroCosto As Integer, ByVal codTrabajador As Integer) As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("ListarSubCategoria")

                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@CodigoEstructuraCategoria", DbType.Int32, int_CodigoEstructuraCategoria)
                dbBase.AddInParameter(cmd, "@centroCosto", DbType.Int32, centroCosto)
                dbBase.AddInParameter(cmd, "@codTrabajador", DbType.Int32, codTrabajador)

                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using


        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Function Lis_ArticulosSubcategorias(ByVal int_CodigoSubCategoria As Integer, ByVal centroCosto As Integer) As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("listarArticulosSubcategoria")
                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@CodigoEstructuraSubCategoria", DbType.Int32, int_CodigoSubCategoria)
                dbBase.AddInParameter(cmd, "@centroCosto", DbType.Int32, centroCosto)

                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using


        Catch ex As Exception
        Finally

        End Try
    End Function


    Public Function Lis_prioridadArticulos() As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("listarPrioridad")
                'Parámetros de entrada


                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using


        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Function Lis_Monedas() As DataTable
        Try
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("listar_monedas")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using



        Catch ex As Exception

        End Try

    End Function

    Public Function listarTrabajadores()
        Try


            Using cmd As DbCommand = dbBase.GetStoredProcCommand("Usp_ListarLT_Trabajadores")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using
        Catch ex As Exception

        End Try
    End Function
    Public Function listarTipoValidacion()
        Try


            Using cmd As DbCommand = dbBase.GetStoredProcCommand("Usp_LisTipoPS_TiposValidaciones")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using
        Catch ex As Exception

        End Try
    End Function
    ''
    ''' <summary>
    ''' listar el anio presupuestal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function listarAnioPresupuestal() As DataTable

        ''  ListarAnioPresupuestal
        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("ListarAnioPresupuestal")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function

    ''
    ''' <summary>
    ''' listar el estado de presupuestos 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fListarEstadosPresupuesto() As DataTable

        ''  ListarAnioPresupuestal
        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_lisEstadosPresupuesto")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function
    Public Function listarSubcategorias() As DataTable


        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("Usp_lisPS_SubCategorias")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Function listarCategorias() As DataTable


        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_Lis_PS_Categorias")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function


    Public Function listarActividades() As DataTable


        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_Lis_PS_Actividades")
                'Parámetros de entrada
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function


    Public Function VerificarTipoUsuario(ByVal p_CodigoTrabajador As Integer) As DataTable


        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_lisTipoUsuario")
                'Parámetros de entrada
                dbBase.AddInParameter(cmd, "@p_CodigoTrabajador", DbType.Int32, p_CodigoTrabajador)
                Return dbBase.ExecuteDataSet(cmd).Tables(0)
            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function


    Public Function fListarSituacionArticulo() As List(Of BE_PS_SituacionItem)
        Dim listBE_PS_SituacionItem As New List(Of BE_PS_SituacionItem)
        Dim oBE_PS_SituacionItem As BE_PS_SituacionItem
        Dim oIDataReader As IDataReader
        Try

            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_LisPS_SituacionItem")
                'Parámetros de entrada


                oIDataReader = dbBase.ExecuteReader(cmd)

                Dim dataSerializado As String = ""


                While oIDataReader.Read

                    listBE_PS_SituacionItem.Add(New BE_PS_SituacionItem With {.SI_CodigoSituacionItem = oIDataReader.GetInt32(0), .SI_Descripcion = oIDataReader.GetString(1)})
                End While

                oIDataReader.Close()


                Return listBE_PS_SituacionItem



            End Using

        Catch ex As Exception
        Finally

        End Try
    End Function

#End Region

#Region "transaccional"

    Public Function insertarEstructuraCentro(ByVal ATP_CodigoEstructuraCategoria As Integer, ByVal SC_CodigoSubCategoria As Integer)

        Dim codigo As Integer = 0
        Dim mensaje As String = ""
        Dim cmd As DbCommand = dbBase.GetStoredProcCommand("USP_insPS_AsignacionSubCategoriasPeriodos")
        Try
            BeginTransaction()

            dbBase.AddInParameter(cmd, "@ASP_CodigoEstructuraSubCategoria", DbType.Int16, 10)
            dbBase.AddInParameter(cmd, "@ATP_CodigoEstructuraCategoria", DbType.Int16, ATP_CodigoEstructuraCategoria)
            dbBase.AddInParameter(cmd, "@SC_CodigoSubCategoria", DbType.Int16, SC_CodigoSubCategoria)
            codigo = dbBase.ExecuteScalar(cmd, tran)

            Commit()
            Return codigo
        Catch ex As Exception
            Rollback()
        Finally

        End Try
    End Function


    Public Function insertarPS_AsignacionCategoriasPeriodos(ByVal ACP_CodigoEstructuraClase As Integer, ByVal CT_CodigoCategoria As Integer, ByVal AT_CodigoActividad As Integer)

        Dim codigo As Integer = 0
        Dim mensaje As String = ""
        Dim cmd As DbCommand = dbBase.GetStoredProcCommand("USP_PS_AsignacionCategoriasPeriodos")
        Try
            BeginTransaction()

            dbBase.AddInParameter(cmd, "@ATP_CodigoEstructuraCategoria", DbType.Int16, 10)
            dbBase.AddInParameter(cmd, "@ACP_CodigoEstructuraClase", DbType.Int16, ACP_CodigoEstructuraClase)
            dbBase.AddInParameter(cmd, "@CT_CodigoCategoria", DbType.Int16, CT_CodigoCategoria)
            dbBase.AddInParameter(cmd, "@AT_CodigoActividad", DbType.Int16, AT_CodigoActividad)
            codigo = dbBase.ExecuteScalar(cmd, tran)

            Commit()
            Return codigo
        Catch ex As Exception
            Rollback()
        Finally

        End Try
    End Function

    Public Function insertarEstructuraCentro(ByVal AT_Descripcion As String, ByVal TA_CodigoTipoItem As Integer)

        Dim codigo As Integer = 0
        Dim mensaje As String = ""
        Dim cmd As DbCommand = dbBase.GetStoredProcCommand("USP_ItemPS_Item")
        Try
            BeginTransaction()

            dbBase.AddInParameter(cmd, "@AT_CodigoItem", DbType.Int16, 10)
            dbBase.AddInParameter(cmd, "@AT_Descripcion", DbType.String, AT_Descripcion)
            dbBase.AddInParameter(cmd, "@AT_Estado", DbType.Boolean, True)
            dbBase.AddInParameter(cmd, "@TA_CodigoTipoItem", DbType.Int16, TA_CodigoTipoItem)


            codigo = dbBase.ExecuteScalar(cmd, tran)

            Commit()
            Return codigo
        Catch ex As Exception
            Rollback()
        Finally

        End Try
    End Function


    Public Function fActualizarCodigoTrabajador(ByVal lstCondCentro As List(Of Integer), ByVal lstTrabajador As List(Of Integer))
        Dim codigo As Integer

        Try
            BeginTransaction()
            For indice As Integer = 0 To lstCondCentro.Count - 1
                Dim cmd As DbCommand = dbBase.GetStoredProcCommand("Usp_UpdPS_AsignacionSSSCentroCostos")
                dbBase.AddInParameter(cmd, "@ASSSCC_CodigoAsignacionSSSCentroCosto", DbType.Int16, lstCondCentro(indice))
                dbBase.AddInParameter(cmd, "@TJ_CodigoTrabajador", DbType.Int16, lstTrabajador(indice))
                codigo = dbBase.ExecuteScalar(cmd, tran)

            Next
            Commit()
            Return codigo

        Catch ex As Exception
        Finally

        End Try
    End Function
#End Region


End Class
