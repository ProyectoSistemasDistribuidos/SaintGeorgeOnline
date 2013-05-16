Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities

Public Class da_articulo
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

#Region "Transaccional"
    Public Function FUN_INS_Articulo(ByVal obe_articulo As be_articulo, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_AF_ImsertarArticulo")

                dbBase.AddInParameter(cmd, "@AT_CodigoArticulo", DbType.Int32, obe_articulo.AT_CodigoArticulo)
                dbBase.AddInParameter(cmd, "@CL_CodigoColor", DbType.Int32, obe_articulo.CL_CodigoColor)
                dbBase.AddInParameter(cmd, "@MC_CodigoMarca", DbType.Int32, obe_articulo.MC_CodigoMarca)
                ' dbBase.AddInParameter(cmd, "@MD_CodigoModelo", DbType.Int32, obe_articulo.MD_CodigoModelo)
                dbBase.AddInParameter(cmd, "@UM_CodigoUnidadMedida", DbType.Int32, obe_articulo.UM_CodigoUnidadMedida)
                dbBase.AddInParameter(cmd, "@TA_CodigoTipoArticulo", DbType.Int32, obe_articulo.TA_CodigoTipoArticulo)
                dbBase.AddInParameter(cmd, "@PS_CodigoPresentacion", DbType.Int32, obe_articulo.PS_CodigoPresentacion)
                dbBase.AddInParameter(cmd, "@AT_Especificaciones", DbType.String, obe_articulo.AT_Especificaciones)

                ''
                dbBase.AddInParameter(cmd, "@AT_Foto", DbType.String, obe_articulo.AT_Foto)
                dbBase.AddInParameter(cmd, "@AT_StockActual", DbType.Int32, obe_articulo.AT_StockActual)
                dbBase.AddInParameter(cmd, "@AT_ActivoFijo", DbType.Boolean, obe_articulo.AT_ActivoFijo)
                dbBase.AddInParameter(cmd, "@AT_FechaCreacion", DbType.Date, obe_articulo.AT_FechaCreacion)
                'dbBase.AddInParameter(cmd, "@AT_StockMinimo", DbType.Int32, obe_articulo.AT_StockMinimo)
                dbBase.AddInParameter(cmd, "@AT_PrecioUnitario", DbType.Decimal, obe_articulo.AT_PrecioUnitario)
                dbBase.AddInParameter(cmd, "@AT_NumeroSerie", DbType.String, obe_articulo.AT_NumeroSerie)

                dbBase.AddInParameter(cmd, "@AT_CodigoBarras", DbType.String, obe_articulo.AT_CodigoBarras)
                dbBase.AddInParameter(cmd, "@AT_Modelo", DbType.String, obe_articulo.AT_Modelo)
                dbBase.AddInParameter(cmd, "@BJ_CodigoMotivoBaja", DbType.Int32, obe_articulo.BJ_CodigoMotivoBaja)
                dbBase.AddInParameter(cmd, "@ET_CodigoEstadoArticulo", DbType.Int32, obe_articulo.ET_CodigoEstadoArticulo)
                dbBase.AddInParameter(cmd, "@AT_Observacion", DbType.String, obe_articulo.AT_Observacion)
                ''
               
                dbBase.AddInParameter(cmd, "@AT_NumeroParte", DbType.String, obe_articulo.AT_NumeroParte)
                dbBase.AddInParameter(cmd, "@AT_Estado", DbType.Boolean, True)
                dbBase.AddInParameter(cmd, "@NA_CodigoNombreArticuloInventario", DbType.Int32, obe_articulo.NA_CodigoNombreArticuloInventario)


                dbBase.AddInParameter(cmd, "@CA_CodigoCategoriaArticulo", DbType.Int32, obe_articulo.CA_CodigoCategoriaArticulo)



                '' dbBase.AddInParameter(cmd, "@NA_CodigoNombreArticuloInventario", DbType.Int32, obe_articulo.NA_CodigoNombreArticuloInventario)

                dbBase.AddInParameter(cmd, "@AT_Cantidad", DbType.Int32, obe_articulo.AT_Cantidad)

                ''--------------------------------------------------------
                dbBase.AddInParameter(cmd, "@AT_ConGarantia", DbType.Boolean, obe_articulo.AT_ConGarantia)
                dbBase.AddInParameter(cmd, "@AT_FechaIngreso", DbType.DateTime, obe_articulo.AT_FechaIngreso)
                dbBase.AddInParameter(cmd, "@AT_CantidadMesesGarantia", DbType.Int32, obe_articulo.AT_CantidadMesesGarantia)
                dbBase.AddInParameter(cmd, "@SCA_CodigoSubCategoriaArticulo", DbType.Int32, obe_articulo.SCA_CodigoSubCategoriaArticulo)


                dbBase.AddInParameter(cmd, "@TJ_CodigoTrabajadorRegistrador", DbType.Int32, obe_articulo.TJ_CodigoTrabajadorRegistrador)
        
                ''---------------------------------------------------------

                dbBase.AddInParameter(cmd, "@AT_IdOrden", DbType.Int32, obe_articulo.AT_IdOrden)
                dbBase.AddInParameter(cmd, "@AT_NumOrden", DbType.String, obe_articulo.AT_NumOrden)
                dbBase.AddInParameter(cmd, "@AT_nFacID", DbType.Int32, obe_articulo.AT_nFacID)
                dbBase.AddInParameter(cmd, "@AT_Documento", DbType.String, obe_articulo.AT_Documento)
                dbBase.AddInParameter(cmd, "@AT_NumGuiaRemision", DbType.String, obe_articulo.AT_NumGuiaRemision)
                dbBase.AddInParameter(cmd, "@PV_CodigoProveedor", DbType.Int32, obe_articulo.PV_CodigoProveedor)





                '@AT_IdOrden int,
                '@AT_NumOrden varchar(max),
                '@AT_nFacID int,
                '@AT_Documento varchar(max),
                '@AT_NumGuiaRemision varchar(max),
                '@PV_CodigoProveedor int,
                'dbBase.AddInParameter(cmd, "@AT_ConGarantia", DbType.Boolean, obe_articulo.AT_ConGarantia)

                dbBase.AddInParameter(cmd, "@PV_Descripcion", DbType.String, obe_articulo.PV_Descripcion)


 
                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 100)



                dbBase.ExecuteScalar(cmd)

                'AT_CodigoArticulo ,
                'CL_CodigoColor ,
                'MC_CodigoMarca ,
                'MD_CodigoModelo ,
                'UM_CodigoUnidadMedida ,
                'TA_CodigoTipoArticulo ,
                'PS_CodigoPresentacion ,
                'AT_Especificaciones ,


                'AT_Foto  ,
                'AT_StockActual ,
                'AT_ActivoFijo,
                'AT_FechaCreacion ,
                'AT_StockMinimo ,
                'AT_PrecioUnitario ,
                'AT_NumeroSerie ,


                'AT_CodigoBarras ,
                'AT_Modelo ,
                'BJ_CodigoMotivoBaja ,
                '           ET_CodigoEstadoArticulo()


                str_Mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@codigo")))
                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado
            End Using
        Catch ex As Exception

        End Try
    End Function

    Public Function FUN_DEL_EliminarArtiuculo(ByVal codArticulo As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As List(Of String)

        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("USP_EliminarArticulo")

                dbBase.AddInParameter(cmd, "@AT_CodigoArticulo", DbType.Int32, codArticulo)
                dbBase.AddOutParameter(cmd, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(cmd, "@mensaje", DbType.String, 100)
                dbBase.ExecuteScalar(cmd)

                str_Mensaje = dbBase.GetParameterValue(cmd, "@mensaje").ToString()
                p_Valor = Integer.Parse(CStr(dbBase.GetParameterValue(cmd, "@codigo")))
                lstResultado.Add(str_Mensaje)
                lstResultado.Add(p_Valor.ToString())
                Return lstResultado

            End Using
        Catch ex As Exception

        End Try


    End Function


#End Region



#Region "No Transaccional"
    Public Function FUN_Lis_Articulo(ByVal obe_articulo As be_articulo, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet


        Dim lstResultado As New List(Of String)
        Try
            Dim str_Mensaje As String = ""
            Dim p_Valor As Integer = 0
            Using cmd As DbCommand = dbBase.GetStoredProcCommand("AF_USP_lisArticulo")

                dbBase.AddInParameter(cmd, "@AT_CodigoArticulo", DbType.Int32, obe_articulo.AT_CodigoArticulo)
                dbBase.AddInParameter(cmd, "@CL_CodigoColor", DbType.Int32, obe_articulo.CL_CodigoColor)
                dbBase.AddInParameter(cmd, "@MC_CodigoMarca", DbType.Int32, obe_articulo.MC_CodigoMarca)
                ' dbBase.AddInParameter(cmd, "@MD_CodigoModelo", DbType.Int32, obe_articulo.MD_CodigoModelo)
                dbBase.AddInParameter(cmd, "@UM_CodigoUnidadMedida", DbType.Int32, obe_articulo.UM_CodigoUnidadMedida)
                dbBase.AddInParameter(cmd, "@TA_CodigoTipoArticulo", DbType.Int32, obe_articulo.TA_CodigoTipoArticulo)
                dbBase.AddInParameter(cmd, "@PS_CodigoPresentacion", DbType.Int32, obe_articulo.PS_CodigoPresentacion)
                dbBase.AddInParameter(cmd, "@AT_Especificaciones", DbType.String, obe_articulo.AT_Especificaciones)

                ''
                dbBase.AddInParameter(cmd, "@AT_Foto", DbType.String, obe_articulo.AT_Foto)
                dbBase.AddInParameter(cmd, "@AT_StockActual", DbType.Int32, obe_articulo.AT_StockActual)
                dbBase.AddInParameter(cmd, "@AT_ActivoFijo", DbType.Boolean, obe_articulo.AT_ActivoFijo)
                dbBase.AddInParameter(cmd, "@AT_FechaCreacion", DbType.Date, obe_articulo.AT_FechaCreacion)
                '  dbBase.AddInParameter(cmd, "@AT_StockMinimo", DbType.Int32, obe_articulo.AT_StockMinimo)
                dbBase.AddInParameter(cmd, "@AT_PrecioUnitario", DbType.Decimal, obe_articulo.AT_PrecioUnitario)
                dbBase.AddInParameter(cmd, "@AT_NumeroSerie", DbType.String, obe_articulo.AT_NumeroSerie)

                dbBase.AddInParameter(cmd, "@AT_CodigoBarras", DbType.String, obe_articulo.AT_CodigoBarras)
                dbBase.AddInParameter(cmd, "@AT_Modelo", DbType.String, obe_articulo.AT_Modelo)
                dbBase.AddInParameter(cmd, "@BJ_CodigoMotivoBaja", DbType.Int32, obe_articulo.BJ_CodigoMotivoBaja)
                dbBase.AddInParameter(cmd, "@ET_CodigoEstadoArticulo", DbType.Int32, obe_articulo.ET_CodigoEstadoArticulo)
                dbBase.AddInParameter(cmd, "@AT_Observacion", DbType.String, obe_articulo.AT_Observacion)

                dbBase.AddInParameter(cmd, "@NA_CodigoNombreArticuloInventario", DbType.Int32, obe_articulo.NA_CodigoNombreArticuloInventario)

                dbBase.AddInParameter(cmd, "@CA_CodigoCategoriaArticulo", DbType.Int32, obe_articulo.CA_CodigoCategoriaArticulo)


                ' dbBase.AddInParameter(cmd, "@AT_NombreArticulo", DbType.String, obe_articulo.AT_NombreArticulo)

                ''






                Return dbBase.ExecuteDataSet(cmd)
            End Using
        Catch ex As Exception

        End Try
    End Function
#End Region
End Class
