Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloBancoLibros

Namespace ModuloBancoLibros

    Public Class da_Prestamos
        Inherits InstanciaConexion.ManejadorConexion

        '#Region "Atributos"

        '        Private dbBase As SqlDatabase 'ExecuteDataSet
        '        Private dbCommand As DbCommand 'ExecuteScalar

        '#End Region

        '#Region "Constructor"

        '        Public Sub New()
        '            dbBase = New SqlDatabase(Me.SqlConexionDB)
        '        End Sub

        '#End Region


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

        Public Function FUN_INS_Prestamos(ByVal objPrestamos As be_Prestamos, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_INS_Prestamos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPrestamos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objPrestamos.CodigoAnio)

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

        'Public Function FUN_INS_Prestamos(ByVal objPrestamos As be_Prestamos, _
        '                                            ByVal objDetalle As DataTable, _
        '                                            ByRef str_Mensaje As String, _
        '                                            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

        '    Dim int_valor As Integer = 0
        '    Dim int_ValorDetalle As Integer = 0
        '    Dim str_MensajeDetalle As String = ""
        '    Dim int_totalregistros As Integer = 0

        '    Try
        '        'inicio la transaccion
        '        BeginTransaction()

        '        'dbCommand = Me.dbBase.GetStoredProcCommand("BL_USP_INS_Prestamos")
        '        ''Parámetros de entrada
        '        'dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objPrestamos.CodigoAlumno)
        '        'dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objPrestamos.CodigoAnio)

        '        'dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
        '        'dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
        '        'dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
        '        'dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)


        '        ''Parámetros de salida
        '        'dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
        '        'dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

        '        ''Ejecucion del Store Procedure
        '        'dbBase.ExecuteScalar(dbCommand, tran)
        '        'str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
        '        'int_valor = Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        '        'If Not int_valor > 0 Then ' sino registro
        '        '    Rollback()
        '        '    Return int_valor
        '        'Else

        '        If objDetalle IsNot Nothing Then
        '            If objDetalle.Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
        '                Dim objPrestamoDetalle As be_PrestamoDetalle
        '                For Each dr As DataRow In objDetalle.Rows

        '                    int_valor = FUN_INS_DetallePrestamo(objPrestamos, tran, _
        '                                                                             str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)


        '                    If dr.Item("Estado") = 1 Then
        '                        objPrestamoDetalle = New be_PrestamoDetalle
        '                        objPrestamoDetalle.CodigoPrestamo = int_valor
        '                        objPrestamoDetalle.CodigoBarra = dr.Item("CodigoBarra")
        '                        int_ValorDetalle = FUN_INS_DetallePrestamo(objPrestamoDetalle, tran, _
        '                                                                              str_MensajeDetalle, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
        '                        If Not int_ValorDetalle > 0 Then
        '                            Rollback()
        '                            str_Mensaje = str_MensajeDetalle
        '                            Return int_ValorDetalle
        '                        End If
        '                    End If
        '                Next
        '            End If
        '        End If

        '        'End If

        '        Commit()
        '        Return int_valor

        '    Catch ex As Exception
        '        str_Mensaje = "ocurrio un error durante el registro."
        '        Rollback()
        '        Return int_valor
        '    Finally
        '        Conexion.Close()
        '    End Try

        'End Function

        

#End Region

#Region "Metodos No Transaccionales"

        Public Function FUN_LIS_LibrosPrestadosAlumnos(ByVal str_CodigoAlumno As String, ByVal int_CodigoAnio As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_LIS_LibrosPrestadosAlumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoAnio", DbType.Int16, int_CodigoAnio)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_PrestamoPorAlumno(ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_REP_PrestamoPorAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_DevolucionPorAlumno(ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_REP_DevolucionPorAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_DeudoresBancoLibros(ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_REP_DeudoresBancoLibros")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'oooooooooooooooooooooooo
        Public Function FUN_REP_DinamicoCantidadLibrosVendidosPorAula(ByVal int_AnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("BL_USP_REP_DinamicoCantidadLibrosVendidosPorAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_AnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)

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