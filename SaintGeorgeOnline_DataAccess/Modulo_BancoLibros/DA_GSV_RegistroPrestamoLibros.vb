
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros
Public Class DA_GSV_RegistroPrestamoLibros

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

#Region " no transaccional "


#End Region

#Region " transaccional "


    Public Function F_insertarPrestamosLIbros(ByVal dcPrestamos As List(Of Dictionary(Of Object, Object))) As Dictionary(Of Object, Object)


        Dim grupoAlumno = From grpAlumno In dcPrestamos Group grpAlumno By codAlumno = grpAlumno("codAlumno"), _
                      codAnio = grpAlumno("codAnio") Into alumno = Group _
                    Select New With {.coAlumno = codAlumno, .codAnio = codAnio, .libros = (From lb In alumno.AsEnumerable() Where lb("Estado") = True Select New With _
                                                                      {.codCopiaLibro = lb("codCopiaLibro"), _
                                                                       .codBlLibro = lb("codBlLibro"), _
                                                                       .fecha = lb("fechaPrestamo"), _
                                                                       .tipolibro = lb("tipoLibro")})}

        Dim dcResultado As New Dictionary(Of Object, Object)
        Dim cod1 As Integer = 0
        Dim cod2 As Integer = 0

        Dim mensaje1 As String = ""
        Dim mensaje2 As String = ""





        Try
            BeginTransaction()
            For Each oalumno In grupoAlumno
                dbCommand = dbBase.GetStoredProcCommand("USP_GSV_InsBL_Prestamos")
                dbBase.AddInParameter(dbCommand, "@PM_CodigoPrestamo", DbType.Int32, 0)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.Int32, oalumno.coAlumno)
                dbBase.AddInParameter(dbCommand, "@AC_CodigoAnioAcademico", DbType.Int32, oalumno.codAnio)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)

                dbBase.ExecuteScalar(dbCommand, tran)

                cod1 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                mensaje1 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()

                dcResultado("mensaje") = mensaje1
                dcResultado("codigo") = cod1

                For Each detallePrestamo In oalumno.libros
                    dbCommand = dbBase.GetStoredProcCommand("USP_GSV_InsBL_PrestamoDetalle")
                    dbBase.AddInParameter(dbCommand, "@PMD_CodigoDetallePrestamo", DbType.Int32, 0)
                    dbBase.AddInParameter(dbCommand, "@PM_CodigoPrestamo", DbType.Int32, cod1)

                    dbBase.AddInParameter(dbCommand, "@LB_CodigoLibro", DbType.Int32, detallePrestamo.codBlLibro)
                    dbBase.AddInParameter(dbCommand, "@CLB_CodigoCopiaLibro", DbType.Int32, detallePrestamo.codCopiaLibro)
                    dbBase.AddInParameter(dbCommand, "@PMD_FechaPrestamo", DbType.DateTime, Convert.ToDateTime(detallePrestamo.fecha.ToString()))

                    dbBase.AddInParameter(dbCommand, "@PMD_FechaDevolucion", DbType.DateTime, DBNull.Value)


                    ''--0 = prestado ; 1=devuelto     
                    '                       TL_CodigoTipoLibro	TL_Descripcion	TL_Abrev
                    '                       Workbook                 WB 
                    If detallePrestamo.tipolibro = 4 Then
                        dbBase.AddInParameter(dbCommand, "@PMD_EstadoPrestamo", DbType.Boolean, True)
                    Else
                        dbBase.AddInParameter(dbCommand, "@PMD_EstadoPrestamo", DbType.Boolean, False)
                    End If



                    dbBase.AddInParameter(dbCommand, "@PMD_Estado", DbType.Boolean, True)

                    dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
                    dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)


                    dbBase.ExecuteScalar(dbCommand, tran)

                    cod2 = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                    mensaje2 = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
                Next

            Next
            Commit()


           


            Return dcResultado

        Catch ex As Exception
            Rollback()
            dbCommand.Connection.Close()
            dbCommand.Dispose()
        Finally
            dbCommand.Connection.Close()
            dbCommand.Dispose()

        End Try
    End Function


    Public Function F_actualizarFechaPrestamoDetalleLibro(ByVal codPrestamo As Integer, ByVal fechaPrestamo As String) As Dictionary(Of Object, Object)
        Dim mensaje As String = ""
        Dim codigo As Integer = 0
        Try
            dbCommand = dbBase.GetStoredProcCommand("GSV_UDP_BL_ActualizarBL_PrestamoDetalle")
            dbBase.AddInParameter(dbCommand, "@PMD_CodigoDetallePrestamo", DbType.Int32, codPrestamo)
            dbBase.AddInParameter(dbCommand, "@PMD_FechaPrestamo", DbType.DateTime, Convert.ToDateTime(fechaPrestamo))
            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 255)
            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)



            dbBase.ExecuteScalar(dbCommand)

            codigo = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            mensaje = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
            Dim dc As New Dictionary(Of Object, Object)
            dc("mensaje") = mensaje
            dc("codigo") = codigo
            Return dc
        Catch ex As Exception

        End Try
    End Function
#End Region
End Class
