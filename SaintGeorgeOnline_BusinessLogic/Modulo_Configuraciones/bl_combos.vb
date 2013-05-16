Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess.ModuloConfiguraciones
Imports SaintGeorgeOnline_DataAccess
Imports SaintGeorgeOnline_BusinessEntities

Public Class bl_combos
#Region "Atributos"

    Private str_Mensaje As String
    Private obj_da_Piso As da_CentrosCostos

#End Region

#Region "Propiedades"



#End Region

#Region "transaccional"
    Public Function insertarEstructuraCentro(ByVal ATP_CodigoEstructuraCategoria As Integer, ByVal SC_CodigoSubCategoria As Integer)
        Try
            Return New da_combos().insertarEstructuraCentro(ATP_CodigoEstructuraCategoria, SC_CodigoSubCategoria)
        Catch ex As Exception

        End Try
    End Function


    Public Function insertarEstructuraCentro(ByVal AT_Descripcion As String, ByVal TA_CodigoTipoItem As Integer) As Integer
        Try
            Return New da_combos().insertarEstructuraCentro(AT_Descripcion, TA_CodigoTipoItem)
        Catch ex As Exception

        End Try
    End Function

    Public Function fActualizarCodigoTrabajador(ByVal lstCondCentro As List(Of Integer), ByVal lstTrabajador As List(Of Integer))
        Try
            Return New da_combos().fActualizarCodigoTrabajador(lstCondCentro, lstTrabajador)
        Catch ex As Exception

        End Try


    End Function

#End Region

#Region "no transaccional"
    Public Function fListarEstadosPresupuesto() As DataTable
        Try
            Return New da_combos().fListarEstadosPresupuesto()
        Catch ex As Exception

        End Try
    End Function

    Public Function Lis_categorias(ByVal int_codigoEstructuraClase As Integer, ByVal centroCosto As Integer) As DataTable
        Try
            Return New da_combos().Lis_categorias(int_codigoEstructuraClase, centroCosto)
        Catch ex As Exception

        End Try
    End Function

    Public Function Lis_SubcategoriasCategorias(ByVal int_CodigoEstructuraCategoria As Integer, ByVal centroCosto As Integer, ByVal codTrabajador As Integer) As DataTable
        Try
            Return New da_combos().Lis_SubcategoriasCategorias(int_CodigoEstructuraCategoria, centroCosto, codTrabajador)
        Catch ex As Exception

        End Try
    End Function

    Public Function Lis_ArticulosSubcategorias(ByVal int_CodigoSubCategoria As Integer, ByVal centroCosto As Integer) As DataTable
        Try
            Return New da_combos().Lis_ArticulosSubcategorias(int_CodigoSubCategoria, centroCosto)
        Catch ex As Exception

        End Try
    End Function



    Public Function Lis_prioridadArticulos() As DataTable
        Try
            Return New da_combos().Lis_prioridadArticulos()
        Catch ex As Exception

        End Try
    End Function
    Public Function Lis_Monedas() As DataTable
        Try
            Return New da_combos().Lis_Monedas()
        Catch ex As Exception

        End Try
    End Function
    Public Function listarAnioPresupuestal() As DataTable
        Try
            Return New da_combos().listarAnioPresupuestal()
        Catch ex As Exception
        Finally

        End Try
    End Function
    Public Function listarSubcategorias() As DataTable
        Try
            Return New da_combos().listarSubcategorias()
        Catch ex As Exception

        End Try
    End Function

    Public Function listarCategorias() As DataTable


        Try
            Return New da_combos().listarCategorias()
        Catch ex As Exception

        End Try

    End Function

    Public Function listarActividades() As DataTable


        Try
            Return New da_combos().listarActividades()
        Catch ex As Exception

        End Try

    End Function

    Public Function insertarPS_AsignacionCategoriasPeriodos(ByVal ACP_CodigoEstructuraClase As Integer, ByVal CT_CodigoCategoria As Integer, ByVal AT_CodigoActividad As Integer)

        Try
            Return New da_combos().insertarPS_AsignacionCategoriasPeriodos(ACP_CodigoEstructuraClase, CT_CodigoCategoria, AT_CodigoActividad)

        Catch ex As Exception
        Finally

        End Try
    End Function

    Public Function Lis_TipoArticulo() As DataTable
        Try
            Return New da_combos().Lis_TipoArticulo()
        Catch ex As Exception

        End Try
    End Function
    Public Function Lis_ArticuloTipoArticulo(ByVal int_tipoArticulo As Integer) As DataTable
        Try
            Return New da_combos().Lis_ArticuloTipoArticulo(int_tipoArticulo)
        Catch ex As Exception

        End Try
    End Function

    Public Function listarTrabajadores()
        Try
            Return New da_combos().listarTrabajadores()
        Catch ex As Exception

        End Try
    End Function

    Public Function listarTipoValidacion()
        Try
            Return New da_combos().listarTipoValidacion()
        Catch ex As Exception

        End Try
    End Function

    Public Function VerificarTipoUsuario(ByVal p_CodigoTrabajador As Integer) As DataTable
        Return New da_combos().VerificarTipoUsuario(p_CodigoTrabajador)
    End Function

    Public Function fListarSituacionArticulo() As List(Of BE_PS_SituacionItem)
        Return New da_combos().fListarSituacionArticulo()
    End Function




#End Region
End Class
