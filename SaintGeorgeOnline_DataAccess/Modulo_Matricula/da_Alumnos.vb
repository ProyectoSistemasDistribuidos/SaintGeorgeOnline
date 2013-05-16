Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula

Namespace ModuloMatricula

    Public Class da_Alumnos
        Inherits InstanciaConexion.ManejadorConexion

        'Actualizado 30-05-2012

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

        Public Function FUN_INS_ExoneracionesCursos(ByVal int_codigoAnioAcademico As Integer, ByVal str_codigoAlumno As String, ByVal int_codigoCurso As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer
            dbCommand = Me.dbBase.GetStoredProcCommand("CU_USP_INS_ExoneracionesCursos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, int_codigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_codigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCurso", DbType.Int32, int_codigoCurso)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))
        End Function

        'update
        Public Function FUN_INS_Alumno(ByVal bool_FichaCompleta As Boolean, ByVal int_CodigoAnioIngresa As Integer, ByVal int_CodigoGradoActual As Integer, _
            ByVal objAlumno As be_Alumnos, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera
                dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_Alumnos")

                'Datos Generales de la cabecera
                dbBase.AddInParameter(dbCommand, "@p_EstadoFicha", DbType.Int32, Convert.ToInt32(bool_FichaCompleta))
                dbBase.AddInParameter(dbCommand, "@p_CodigoAnioIngresa", DbType.Int16, int_CodigoAnioIngresa)
                dbBase.AddInParameter(dbCommand, "@p_CodigoGradoActual", DbType.Int16, int_CodigoGradoActual)

                'Persona 
                dbBase.AddInParameter(dbCommand, "@p_Contrasenia", DbType.String, objAlumno.Contrasenia) 'Encriptada

                dbBase.AddInParameter(dbCommand, "@p_ApellidoPaterno", DbType.String, objAlumno.ApellidoPaterno)
                dbBase.AddInParameter(dbCommand, "@p_ApellidoMaterno", DbType.String, objAlumno.ApellidoMaterno)
                dbBase.AddInParameter(dbCommand, "@p_Nombre", DbType.String, objAlumno.Nombre)
                dbBase.AddInParameter(dbCommand, "@p_Sexo", DbType.Int16, objAlumno.CodigoSexo)
                dbBase.AddInParameter(dbCommand, "@p_TipoDocIdentidad", DbType.Int16, IIf(objAlumno.CodigoTipoDocIdentidad = 0, DBNull.Value, objAlumno.CodigoTipoDocIdentidad))
                dbBase.AddInParameter(dbCommand, "@p_NumeroDocIdentidad", DbType.String, IIf(objAlumno.NumeroDocIdentidad.Trim.Length = 0, DBNull.Value, objAlumno.NumeroDocIdentidad))
                dbBase.AddInParameter(dbCommand, "@p_ProfesaReligion", DbType.Int16, IIf(objAlumno.ProfesaReligion = -1, DBNull.Value, objAlumno.ProfesaReligion))
                dbBase.AddInParameter(dbCommand, "@p_CodigoReligion", DbType.Int16, IIf(objAlumno.CodigoReligion = 0, DBNull.Value, objAlumno.CodigoReligion))
                dbBase.AddInParameter(dbCommand, "@p_CodigoPais", DbType.Int16, IIf(objAlumno.CodigoPais = 0, DBNull.Value, objAlumno.CodigoPais))
                dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeoDomicilio", DbType.String, IIf(objAlumno.CodigoUbigeo = "000000", DBNull.Value, objAlumno.CodigoUbigeo))
                dbBase.AddInParameter(dbCommand, "@p_FechaNacimiento", DbType.DateTime, IIf(objAlumno.FechaNacimiento Is Nothing Or objAlumno.FechaNacimiento = "", DBNull.Value, objAlumno.FechaNacimiento))

                dbBase.AddInParameter(dbCommand, "@p_EmailPersonal", DbType.String, objAlumno.EmailPersonal)
                dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, objAlumno.Direccion)
                dbBase.AddInParameter(dbCommand, "@p_Urbanizacion", DbType.String, objAlumno.Urbanizacion)
                dbBase.AddInParameter(dbCommand, "@p_ReferenciaDomiciliaria", DbType.String, objAlumno.ReferenciaDomiciliaria)
                dbBase.AddInParameter(dbCommand, "@p_TelefonoCasa", DbType.String, objAlumno.TelefonoCasa)
                dbBase.AddInParameter(dbCommand, "@p_Celular", DbType.String, objAlumno.Celular)

                'Alumno
                dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeoNacimiento", DbType.String, IIf(objAlumno.CodigoNacimientoUbigeo = "000", DBNull.Value, objAlumno.CodigoNacimientoUbigeo))
                dbBase.AddInParameter(dbCommand, "@p_CantidadHermanos", DbType.Int16, objAlumno.CantidadHermanos)
                dbBase.AddInParameter(dbCommand, "@p_PosicionEntreHermanos", DbType.Int16, objAlumno.PosicionEntreHermanos)
                dbBase.AddInParameter(dbCommand, "@p_CorreoInstitucional", DbType.String, IIf(objAlumno.CorreoInstitucional.Trim.Length = 0, DBNull.Value, objAlumno.CorreoInstitucional))
                dbBase.AddInParameter(dbCommand, "@p_Bautizo", DbType.Int16, IIf(objAlumno.Bautizo = -1, DBNull.Value, objAlumno.Bautizo))
                dbBase.AddInParameter(dbCommand, "@p_BautizoLugar", DbType.String, IIf(objAlumno.BautizoLugar.Trim.Length = 0, DBNull.Value, objAlumno.BautizoLugar))
                dbBase.AddInParameter(dbCommand, "@p_BautizoAnio", DbType.Int16, IIf(objAlumno.BautizoAnio = -1, DBNull.Value, objAlumno.BautizoAnio))
                dbBase.AddInParameter(dbCommand, "@p_PrimeraComunion", DbType.Int16, IIf(objAlumno.PrimeraComunion = -1, DBNull.Value, objAlumno.PrimeraComunion))
                dbBase.AddInParameter(dbCommand, "@p_PrimeraComunionLugar", DbType.String, IIf(objAlumno.PrimeraComunionLugar.Trim.Length = 0, DBNull.Value, objAlumno.PrimeraComunionLugar))
                dbBase.AddInParameter(dbCommand, "@p_PrimeraComunionAnio", DbType.Int16, IIf(objAlumno.PrimeraComunionAnio = -1, DBNull.Value, objAlumno.PrimeraComunionAnio))
                dbBase.AddInParameter(dbCommand, "@p_Confirmacion", DbType.Int16, IIf(objAlumno.Confirmacion = -1, DBNull.Value, objAlumno.Confirmacion))
                dbBase.AddInParameter(dbCommand, "@p_ConfirmacionLugar", DbType.String, IIf(objAlumno.ConfirmacionLugar.Trim.Length = 0, DBNull.Value, objAlumno.ConfirmacionLugar))
                dbBase.AddInParameter(dbCommand, "@p_ComfirmacionAnio", DbType.Int16, IIf(objAlumno.ConfirmacionAnio = -1, DBNull.Value, objAlumno.ConfirmacionAnio))
                dbBase.AddInParameter(dbCommand, "@p_NacimientoRegistrado", DbType.Int16, IIf(objAlumno.NacimientoRegistrado = -1, DBNull.Value, objAlumno.NacimientoRegistrado))
                dbBase.AddInParameter(dbCommand, "@p_AccesoInternet", DbType.Int16, IIf(objAlumno.AccesoInternet = -1, DBNull.Value, objAlumno.AccesoInternet))
                dbBase.AddInParameter(dbCommand, "@p_ExperienciasTraumaticasDescripcion", DbType.String, objAlumno.ExperienciasTraumaticasDescripcion)
                dbBase.AddInParameter(dbCommand, "@p_NombreContactoAvisoEmergencia", DbType.String, objAlumno.NombreContactoAvisoEmergencia)
                dbBase.AddInParameter(dbCommand, "@p_TelfCasaContactoAvisoEmergencia", DbType.String, objAlumno.TelfCasaContactoAvisoEmergencia)
                dbBase.AddInParameter(dbCommand, "@p_TelfOficinaContactoAvisoEmergencia", DbType.String, objAlumno.TelfOficinaContactoAvisoEmergencia)
                dbBase.AddInParameter(dbCommand, "@p_CellContactoAvisoEmergencia", DbType.String, objAlumno.CellContactoAvisoEmergencia)

                dbBase.AddInParameter(dbCommand, "@p_CodigoIdioma1", DbType.Int16, IIf(objAlumno.CodigoIdioma1 = 0, DBNull.Value, objAlumno.CodigoIdioma1))
                dbBase.AddInParameter(dbCommand, "@p_CodigoIdioma2", DbType.Int16, IIf(objAlumno.CodigoIdioma2 = 0, DBNull.Value, objAlumno.CodigoIdioma2))
                dbBase.AddInParameter(dbCommand, "@p_CodigoNacionalidad1", DbType.Int16, IIf(objAlumno.CodigoNacionalidades1 = 0, DBNull.Value, objAlumno.CodigoNacionalidades1))
                dbBase.AddInParameter(dbCommand, "@p_CodigoNacionalidad2", DbType.Int16, IIf(objAlumno.CodigoNacionalidades2 = 0, DBNull.Value, objAlumno.CodigoNacionalidades2))

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

                If int_Valor > 0 Then

                    Commit()

                Else

                    Rollback()

                End If

                Return int_Valor

            Catch ex As Exception

                str_Mensaje = ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

       

        'Ficha Alumno (Módulo Matrícula)
        Public Function FUN_UPD_Alumno(ByVal bool_FichaCompleta As Boolean, _
                                       ByVal objAlumno As be_Alumnos, _
                                       ByVal objDetalle As DataSet, _
                                       ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera
                dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_Alumnos")

                'Datos Generales de la cabecera
                'Persona 
                dbBase.AddInParameter(dbCommand, "@p_EstadoFicha", DbType.Int32, Convert.ToInt32(bool_FichaCompleta))
                dbBase.AddInParameter(dbCommand, "@p_ApellidoPaterno", DbType.String, objAlumno.ApellidoPaterno)
                dbBase.AddInParameter(dbCommand, "@p_ApellidoMaterno", DbType.String, objAlumno.ApellidoMaterno)
                dbBase.AddInParameter(dbCommand, "@p_Nombre", DbType.String, objAlumno.Nombre)
                dbBase.AddInParameter(dbCommand, "@p_Sexo", DbType.Int16, objAlumno.CodigoSexo)
                dbBase.AddInParameter(dbCommand, "@p_TipoDocIdentidad", DbType.Int16, IIf(objAlumno.CodigoTipoDocIdentidad = 0, DBNull.Value, objAlumno.CodigoTipoDocIdentidad))
                dbBase.AddInParameter(dbCommand, "@p_NumeroDocIdentidad", DbType.String, objAlumno.NumeroDocIdentidad)
                dbBase.AddInParameter(dbCommand, "@p_FechaNacimiento", DbType.DateTime, IIf(objAlumno.FechaNacimiento.ToString.Length = 0, DBNull.Value, objAlumno.FechaNacimiento))
                dbBase.AddInParameter(dbCommand, "@p_ProfesaReligion", DbType.Int16, IIf(objAlumno.ProfesaReligion = -1, DBNull.Value, objAlumno.ProfesaReligion))

                dbBase.AddInParameter(dbCommand, "@p_CodigoReligion", DbType.String, IIf(objAlumno.CodigoReligion = 0, DBNull.Value, objAlumno.CodigoReligion))
                dbBase.AddInParameter(dbCommand, "@p_CodigoRelacionIdiomasPersonas1", DbType.Int16, IIf(objAlumno.CodigoRelacionIdiomasPersonas1 = 0, 0, objAlumno.CodigoRelacionIdiomasPersonas1))
                dbBase.AddInParameter(dbCommand, "@p_CodigoRelacionIdiomasPersonas2", DbType.Int16, IIf(objAlumno.CodigoRelacionIdiomasPersonas2 = 0, 0, objAlumno.CodigoRelacionIdiomasPersonas2))
                dbBase.AddInParameter(dbCommand, "@p_CodigoIdioma1", DbType.Int16, IIf(objAlumno.CodigoIdioma1 = 0, DBNull.Value, objAlumno.CodigoIdioma1))
                dbBase.AddInParameter(dbCommand, "@p_CodigoIdioma2", DbType.Int16, IIf(objAlumno.CodigoIdioma2 = 0, DBNull.Value, objAlumno.CodigoIdioma2))
                dbBase.AddInParameter(dbCommand, "@p_CodigoRelacionNacionalidadPersonas1", DbType.Int16, IIf(objAlumno.CodigoRelacionNacionalidadesPersonas1 = 0, DBNull.Value, objAlumno.CodigoRelacionNacionalidadesPersonas1))
                dbBase.AddInParameter(dbCommand, "@p_CodigoRelacionNacionalidadPersonas2", DbType.Int16, IIf(objAlumno.CodigoRelacionNacionalidadesPersonas2 = 0, DBNull.Value, objAlumno.CodigoRelacionNacionalidadesPersonas2))
                dbBase.AddInParameter(dbCommand, "@p_CodigoNacionalidad1", DbType.Int16, IIf(objAlumno.CodigoNacionalidades1 = 0, 0, objAlumno.CodigoNacionalidades1))
                dbBase.AddInParameter(dbCommand, "@p_CodigoNacionalidad2", DbType.Int16, IIf(objAlumno.CodigoNacionalidades2 = 0, 0, objAlumno.CodigoNacionalidades2))

                'Alumno
                dbBase.AddInParameter(dbCommand, "@p_CodigoEducando", DbType.String, objAlumno.CodigoEducando)
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objAlumno.CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, objAlumno.CodigoPersona)
                'dbBase.AddInParameter(dbCommand, "@p_CodigoHouse", DbType.Int16, objAlumno.CodigoHouse)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPais", DbType.Int16, IIf(objAlumno.CodigoPais = 0, DBNull.Value, objAlumno.CodigoPais))
                dbBase.AddInParameter(dbCommand, "@p_CantidadHermanos", DbType.Int16, objAlumno.CantidadHermanos)
                dbBase.AddInParameter(dbCommand, "@p_PosicionEntreHermanos", DbType.Int16, objAlumno.PosicionEntreHermanos)
                dbBase.AddInParameter(dbCommand, "@p_CorreoInstitucional", DbType.String, objAlumno.CorreoInstitucional)


                'dbBase.AddInParameter(dbCommand, "@p_ProfesaReligion", DbType.Int16, IIf(objAlumno.ProfesaReligion = -1, DBNull.Value, objAlumno.ProfesaReligion))
                dbBase.AddInParameter(dbCommand, "@p_Bautizo", DbType.Int16, IIf(objAlumno.Bautizo = -1, DBNull.Value, objAlumno.Bautizo))
                dbBase.AddInParameter(dbCommand, "@p_BautizoLugar", DbType.String, objAlumno.BautizoLugar)
                dbBase.AddInParameter(dbCommand, "@p_BautizoAnio", DbType.Int16, IIf(objAlumno.BautizoAnio = -1, DBNull.Value, objAlumno.BautizoAnio))
                dbBase.AddInParameter(dbCommand, "@p_PrimeraComunion", DbType.Int16, IIf(objAlumno.PrimeraComunion = -1, DBNull.Value, objAlumno.PrimeraComunion))
                dbBase.AddInParameter(dbCommand, "@p_PrimeraComunionLugar", DbType.String, objAlumno.PrimeraComunionLugar)
                dbBase.AddInParameter(dbCommand, "@p_PrimeraComunionAnio", DbType.Int16, IIf(objAlumno.PrimeraComunionAnio = -1, DBNull.Value, objAlumno.PrimeraComunionAnio))
                dbBase.AddInParameter(dbCommand, "@p_Confirmacion", DbType.Int16, IIf(objAlumno.Confirmacion = -1, DBNull.Value, objAlumno.Confirmacion))
                dbBase.AddInParameter(dbCommand, "@p_ConfirmacionLugar", DbType.String, objAlumno.ConfirmacionLugar)
                dbBase.AddInParameter(dbCommand, "@p_ComfirmacionAnio", DbType.Int16, IIf(objAlumno.ConfirmacionAnio = -1, DBNull.Value, objAlumno.ConfirmacionAnio))

                dbBase.AddInParameter(dbCommand, "@p_NacimientoRegistrado", DbType.Int16, IIf(objAlumno.NacimientoRegistrado = -1, DBNull.Value, objAlumno.NacimientoRegistrado))
                'dbBase.AddInParameter(dbCommand, "@p_RutaFoto", DbType.Int16, objAlumno.RutaFoto)
                dbBase.AddInParameter(dbCommand, "@p_AccesoInternet", DbType.Int16, IIf(objAlumno.AccesoInternet = -1, DBNull.Value, objAlumno.AccesoInternet))
                dbBase.AddInParameter(dbCommand, "@p_ExperienciasTraumaticasDescripcion", DbType.String, objAlumno.AccesoInternet)
                dbBase.AddInParameter(dbCommand, "@p_NombreContactoAvisoEmergencia", DbType.String, objAlumno.NombreContactoAvisoEmergencia)
                dbBase.AddInParameter(dbCommand, "@p_TelfCasaContactoAvisoEmergencia", DbType.String, objAlumno.TelfCasaContactoAvisoEmergencia)
                dbBase.AddInParameter(dbCommand, "@p_TelfOficinaContactoAvisoEmergencia", DbType.String, objAlumno.TelfOficinaContactoAvisoEmergencia)
                dbBase.AddInParameter(dbCommand, "@p_CellContactoAvisoEmergencia", DbType.String, objAlumno.CellContactoAvisoEmergencia)
                dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeoNacimiento", DbType.String, IIf(objAlumno.CodigoNacimientoUbigeo = "000", DBNull.Value, objAlumno.CodigoNacimientoUbigeo))

                dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeoDomicilio", DbType.String, IIf(objAlumno.CodigoUbigeo = "000000", DBNull.Value, objAlumno.CodigoUbigeo))
                dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, objAlumno.Direccion)
                dbBase.AddInParameter(dbCommand, "@p_Urbanizacion", DbType.String, objAlumno.Urbanizacion)
                dbBase.AddInParameter(dbCommand, "@p_ReferenciaDomiciliaria", DbType.String, objAlumno.ReferenciaDomiciliaria)
                dbBase.AddInParameter(dbCommand, "@p_TelefonoCasa", DbType.String, objAlumno.TelefonoCasa)

                dbBase.AddInParameter(dbCommand, "@p_EmitirFactura", DbType.Int16, objAlumno.EmitirFactura)
                dbBase.AddInParameter(dbCommand, "@p_CodigoEmpresa", DbType.Int16, IIf(objAlumno.CodigoEmpresa = 0, DBNull.Value, objAlumno.CodigoEmpresa))

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

                Dim str_codigoAlumno As String
                str_codigoAlumno = objAlumno.CodigoAlumno

                If int_Valor > 0 Then

                    'Elimino todos los detalles

                    FUN_DEL_TiposDiscapacidadesAlumnos(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                    FUN_DEL_RetiroAlumnos(str_codigoAlumno, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                    FUN_DEL_ProcedenciaAlumnos(str_codigoAlumno, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                    'Agrego los nuevos detalles de la Ficha Medica 
                    'DS.Tables(0) : Discapacidad

                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objTiposDiscapacidadAlumnos As be_RelacionTiposDiscapacidadesAlumnos

                            For Each dr As DataRow In objDetalle.Tables(0).Rows

                                objTiposDiscapacidadAlumnos = New be_RelacionTiposDiscapacidadesAlumnos
                                objTiposDiscapacidadAlumnos.CodigoAlumno = int_Valor
                                objTiposDiscapacidadAlumnos.CodigoTipoDiscapacidad = dr.Item("CodigoTipoDiscapacidad")
                                objTiposDiscapacidadAlumnos.Descripcion = dr.Item("DescripcionDiscapacidad")
                                FUN_INS_TiposDiscapacidadAlumnos(objTiposDiscapacidadAlumnos, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If

                    If objDetalle.Tables(1) IsNot Nothing Then
                        If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objRetiroAlumnos As be_Retiros

                            For Each dr As DataRow In objDetalle.Tables(1).Rows

                                objRetiroAlumnos = New be_Retiros
                                objRetiroAlumnos.CodigoAlumno = str_codigoAlumno
                                objRetiroAlumnos.FechaRetiro = dr.Item("FechaRegistroRetiro")
                                objRetiroAlumnos.CodigoMotivoRetiro = dr.Item("CodigoMotivo")
                                objRetiroAlumnos.CodigoAnioAcademico = dr.Item("CodigoAnio")
                                objRetiroAlumnos.CodigoColegio = dr.Item("CodigoColegioTraslado")
                                objRetiroAlumnos.Observacion = dr.Item("Observacion")
                                'objRetiroAlumnos.Descripcion = dr.Item("DescripcionDiscapacidad")
                                FUN_INS_RetiroAlumnos(objRetiroAlumnos, str_codigoAlumno, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If

                    If objDetalle.Tables(2) IsNot Nothing Then
                        If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objProcedenciaAlumnos As be_Procedencia

                            For Each dr As DataRow In objDetalle.Tables(2).Rows

                                objProcedenciaAlumnos = New be_Procedencia
                                objProcedenciaAlumnos.CodigoAlumno = str_codigoAlumno
                                'objRetiroAlumnos.FechaRetiro = dr.Item("FechaRegistroRetiro")
                                'objRetiroAlumnos.CodigoMotivoRetiro = dr.Item("CodigoMotivo")
                                objProcedenciaAlumnos.CodigoAnioAcademico = dr.Item("CodigoAnio")
                                objProcedenciaAlumnos.CodigoColegioProcedencia = dr.Item("CodigoColegioProcedencia")
                                'objRetiroAlumnos.Observacion = dr.Item("Observacion")
                                'objRetiroAlumnos.Descripcion = dr.Item("DescripcionDiscapacidad")
                                FUN_INS_ProcedenciaAlumnos(objProcedenciaAlumnos, str_codigoAlumno, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If


                    'DS.Tables(0) : Familia

                    'If objDetalle.Tables(1) IsNot Nothing Then
                    '    If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                    '        Dim objTiposAlumnosFamiliares As be_RelacionAlumnosFamiliares

                    '        For Each dr As DataRow In objDetalle.Tables(1).Rows

                    '            objTiposAlumnosFamiliares = New be_RelacionAlumnosFamiliares
                    '            objTiposAlumnosFamiliares.CodigoAlumno = int_Valor
                    '            objTiposAlumnosFamiliares.CodigoFamiliar = dr.Item("CodigoFamiliar")
                    '            objTiposAlumnosFamiliares.CodigoParentesco = dr.Item("CodigoParentesco")
                    '            If dr.Item("ViveConAlumno") = "No" Then
                    '                objTiposAlumnosFamiliares.ViveConAlumno = 0
                    '            Else
                    '                objTiposAlumnosFamiliares.ViveConAlumno = 1
                    '            End If
                    '            'objTiposAlumnosFamiliares.ViveConAlumno = dr.Item("CodigoViveConAlumno")
                    '            FUN_UPD_RelacionAlumnosFamiliares(objTiposAlumnosFamiliares, Transaccion)

                    '        Next

                    '    End If
                    'End If

                    Commit()

                Else

                    Rollback()

                End If

                Return int_Valor 'Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Catch ex As Exception

                str_Mensaje = ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        'modulo asignacionaula alumnos
        Public Function FUN_UPD_AsignacionAulaAlumno(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal str_CodigoAlumno As String, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_AsignacionAulaAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        Public Function FUN_UPD_AsignacionHouseAlumno(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoHouse As Integer, ByVal str_CodigoAlumno As String, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_HousesAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoHouse", DbType.Int32, int_CodigoHouse)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function
        'Ficha Alumno - Detalle -> Procedure con errores
        Public Sub FUN_UPD_RelacionAlumnosFamiliares(ByVal objRelacionAlumnosFamiliares As be_RelacionAlumnosFamiliares, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_RelacionAlumnosFamiliares")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int16, objRelacionAlumnosFamiliares.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objRelacionAlumnosFamiliares.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_CodigoParentesco", DbType.Int16, objRelacionAlumnosFamiliares.CodigoParentesco)
            dbBase.AddInParameter(dbCommand, "@p_ViveConAlumno", DbType.Int16, objRelacionAlumnosFamiliares.ViveConAlumno)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Public Sub FUN_INS_TiposDiscapacidadAlumnos(ByVal objTiposDiscapacidadAlumnos As be_RelacionTiposDiscapacidadesAlumnos, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_TiposDiscapacidadAlumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int16, objTiposDiscapacidadAlumnos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoDiscapacidad", DbType.Int16, objTiposDiscapacidadAlumnos.CodigoTipoDiscapacidad)
            dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objTiposDiscapacidadAlumnos.Descripcion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Public Sub FUN_INS_RetiroAlumnos(ByVal objRetiroAlumnos As be_Retiros, ByVal str_codigoAlumno As String, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_Retiros")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_FechaRetiro", DbType.Date, objRetiroAlumnos.FechaRetiro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMotivoRetiro", DbType.Int32, objRetiroAlumnos.CodigoMotivoRetiro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int16, objRetiroAlumnos.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_codigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoColegio", DbType.Int16, objRetiroAlumnos.CodigoColegio)
            dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objRetiroAlumnos.Observacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Public Sub FUN_INS_ProcedenciaAlumnos(ByVal objProcedenciaAlumnos As be_Procedencia, ByVal str_codigoAlumno As String, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_Procedencia")

            'Parámetros de entrada
            'dbBase.AddInParameter(dbCommand, "@p_FechaRetiro", DbType.Date, objRetiroAlumnos.FechaRetiro)
            'dbBase.AddInParameter(dbCommand, "@p_CodigoMotivoRetiro", DbType.Int32, objRetiroAlumnos.CodigoMotivoRetiro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objProcedenciaAlumnos.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, str_codigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoColegioProcedencia", DbType.Int32, objProcedenciaAlumnos.CodigoColegioProcedencia)
            'dbBase.AddInParameter(dbCommand, "@p_Observacion", DbType.String, objRetiroAlumnos.Observacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub


        Private Sub FUN_DEL_TiposDiscapacidadesAlumnos(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_TiposDiscapacidadesAlumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int16, int_CodigoFichaMedica)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_RetiroAlumnos(ByVal int_Codigo As String, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_Retiro")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_ProcedenciaAlumnos(ByVal int_Codigo As String, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_Procedencia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, int_Codigo)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_INS_NacionalidadPersona(ByVal objNacionalidadPersona As be_RelacionNacionalidadPersona, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_NacionalidadPersona")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, objNacionalidadPersona.CodigoPersona)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNacionalidad", DbType.Int16, objNacionalidadPersona.CodigoNacionalidad)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_NacionalidadPersona(ByVal int_CodigoPersona As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_NacionalidadPersona")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, int_CodigoPersona)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_INS_IdiomaPersona(ByVal objIdiomaPersona As be_RelacionIdiomaPersona, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_IdiomaPersona")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, objIdiomaPersona.CodigoPersona)
            dbBase.AddInParameter(dbCommand, "@p_CodigoIdioma", DbType.Int16, objIdiomaPersona.CodigoIdioma)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_IdiomaPersona(ByVal int_CodigoPersona As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_IdiomaPersona")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, int_CodigoPersona)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_INS_ClinicaSeguro(ByVal objClinicaSeguro As be_RelacionClinicasSeguro, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_ClinicaSeguro")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaSeguro", DbType.Int16, objClinicaSeguro.CodigoFichaSeguro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoClinica", DbType.Int16, objClinicaSeguro.CodigoClinica)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub


        Private Sub FUN_DEL_ClinicaSeguro(ByVal int_CodigoFichaSeguro As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_ClinicaSeguro")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaSeguro", DbType.Int16, int_CodigoFichaSeguro)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        'Validacion de Datos (Módulo Matrícula)
        Public Function FUN_UPD_AlumnosActualizacion( _
            ByVal intCodigoAlumno As Integer, _
            ByVal intCodigoPersona As Integer, _
            ByVal intCodigoSolicitud As Integer, _
            ByVal intCodigoPerfil As Integer, _
            ByVal objDT_Cabecera As DataSet, _
            ByVal arrStrCodigossNacionalidad() As String, _
            ByVal arrStrCodigosIdioma() As String, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer


            Dim int_Valor As Integer = 0
            Dim detNacionalidad As Integer = 0
            Dim detIdioma As Integer = 0
            Dim detClinica As Integer = 0
            Dim detDiscapacidad As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()




                For Each dr As DataRow In objDT_Cabecera.Tables(0).Rows

                    dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_AlumnosActualizacion")

                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, intCodigoSolicitud)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, intCodigoAlumno)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, intCodigoPersona)
                    'dbBase.AddInParameter(dbCommand, "@p_CodigoFichaSeguro", DbType.Int16, intCodigoFichaSeguro)
                    dbBase.AddInParameter(dbCommand, "@p_Indice", DbType.Int16, dr.Item("Indice"))
                    dbBase.AddInParameter(dbCommand, "@p_ValorActualizar", DbType.String, dr.Item("CodigoCampo"))

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                    'Ejecucion del Store Procedure
                    dbBase.ExecuteScalar(dbCommand, tran)

                Next


                'dcRelacionColumnas = New DataColumn("codFamilia", GetType(Integer))
                'dtRelacionFamilia.Columns.Add(dcRelacionColumnas)
                'dcRelacionColumnas = New DataColumn("pagante", GetType(Boolean))
                'dtRelacionFamilia.Columns.Add(dcRelacionColumnas)
                'dcRelacionColumnas = New DataColumn("viveCon", GetType(Boolean))
                For Each filasRelacionFamilia As DataRow In objDT_Cabecera.Tables(1).Rows
                    dbCommand = Me.dbBase.GetStoredProcCommand("UDP_MARelacionAlumno")

                    dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, CInt(filasRelacionFamilia("codFamilia")))
                    dbBase.AddInParameter(dbCommand, "@p_RAF_ResponsablePago", DbType.Boolean, Convert.ToBoolean(filasRelacionFamilia("pagante")))
                    dbBase.AddInParameter(dbCommand, "@p_ViveConAlumno", DbType.Boolean, Convert.ToBoolean(filasRelacionFamilia("viveCon")))


                    dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                    dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)

                    dbBase.ExecuteScalar(dbCommand, tran)

                    Dim cod As Integer = CInt(dbBase.GetParameterValue(dbCommand, "@codigo").ToString())
                    Dim mensaje As String = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()



                    '@codigo int out ,
                    '@mensaje varchar(max) out 

                Next

                'Detalle Alumno
                'Nacionalidad
                If arrStrCodigossNacionalidad IsNot Nothing Then
                    If arrStrCodigossNacionalidad.Length > 0 Then
                        FUN_DEL_NacionalidadPersona(intCodigoPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objNacionalidadPersona As New be_RelacionNacionalidadPersona
                        For Each item As String In arrStrCodigossNacionalidad
                            objNacionalidadPersona = New be_RelacionNacionalidadPersona
                            objNacionalidadPersona.CodigoPersona = intCodigoPersona
                            objNacionalidadPersona.CodigoNacionalidad = CInt(item)
                            FUN_INS_NacionalidadPersona(objNacionalidadPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        Next
                        detNacionalidad = 1

                    End If
                End If

                'Idioma
                If arrStrCodigosIdioma IsNot Nothing Then
                    If arrStrCodigosIdioma.Length > 0 Then
                        FUN_DEL_IdiomaPersona(intCodigoPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objIdiomaPersona As be_RelacionIdiomaPersona
                        For Each item As String In arrStrCodigosIdioma
                            objIdiomaPersona = New be_RelacionIdiomaPersona
                            objIdiomaPersona.CodigoIdioma = CInt(item)
                            objIdiomaPersona.CodigoPersona = intCodigoPersona
                            FUN_INS_IdiomaPersona(objIdiomaPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        Next
                        detIdioma = 1

                    End If
                End If

                'Clinicas
                ' ''If arrStrCodigosClinica IsNot Nothing Then
                ' ''    If arrStrCodigosClinica.Length > 0 Then
                ' ''        FUN_DEL_ClinicaSeguro(intCodigoFichaSeguro, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                ' ''        Dim objClinicaSeguro As be_RelacionClinicasSeguro
                ' ''        For Each item As String In arrStrCodigosClinica
                ' ''            objClinicaSeguro = New be_RelacionClinicasSeguro
                ' ''            objClinicaSeguro.CodigoFichaSeguro = intCodigoFichaSeguro
                ' ''            objClinicaSeguro.CodigoClinica = CInt(item)
                ' ''            FUN_INS_ClinicaSeguro(objClinicaSeguro, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                ' ''        Next
                ' ''        detClinica = 1

                ' ''    End If
                ' ''End If

                'Discapacidades
                ' ''If objDT_Discapacidad IsNot Nothing Then
                ' ''    If objDT_Discapacidad.Rows.Count > 0 Then
                ' ''        FUN_DEL_TiposDiscapacidadesAlumnos(intCodigoAlumno, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                ' ''        Dim objTiposDiscapacidadAlumnos As be_RelacionTiposDiscapacidadesAlumnos
                ' ''        For Each dr As DataRow In objDT_Discapacidad.Rows
                ' ''            objTiposDiscapacidadAlumnos = New be_RelacionTiposDiscapacidadesAlumnos
                ' ''            objTiposDiscapacidadAlumnos.CodigoAlumno = intCodigoAlumno
                ' ''            objTiposDiscapacidadAlumnos.CodigoTipoDiscapacidad = CInt(dr.Item("Codigo"))
                ' ''            objTiposDiscapacidadAlumnos.Descripcion = dr.Item("Detalle")
                ' ''            FUN_INS_TiposDiscapacidadAlumnos(objTiposDiscapacidadAlumnos, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                ' ''        Next
                ' ''        detDiscapacidad = 1

                ' ''    End If
                ' ''End If

                'Actualizo el estado de la solicitud de actualizacion
                int_Valor = FUN_UPD_EstadoSolicitudActualizacion(intCodigoSolicitud, intCodigoPerfil, detNacionalidad, detIdioma, Transaccion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                If int_Valor > 0 Then
                    Commit()
                Else
                    intCodigoAlumno = int_Valor
                    Rollback()
                End If

                Return intCodigoAlumno

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro." 'ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Private Function FUN_UPD_EstadoSolicitudActualizacion(ByVal CodigoSolicitud As Integer, ByVal CodigoPerfil As Integer, _
            ByVal detNacionalidad As Integer, ByVal detIdioma As Integer, _
            ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_SolicitudActualizacionFichaAlumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPerfil", DbType.Int16, CodigoPerfil)
            dbBase.AddInParameter(dbCommand, "@p_detNacionalidad", DbType.Int16, detNacionalidad)
            dbBase.AddInParameter(dbCommand, "@p_detIdioma", DbType.Int16, detIdioma)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            Return CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

        End Function

        '-> Procedure con errores
        Public Sub FUN_UPD_RelacionAlumnosFamiliaresActualizacion(ByVal objRelacionAlumnosFamiliares As be_RelacionAlumnosFamiliares, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_RelacionAlumnosFamiliaresActualizacion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int16, objRelacionAlumnosFamiliares.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objRelacionAlumnosFamiliares.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_CodigoParentesco", DbType.Int16, objRelacionAlumnosFamiliares.CodigoParentesco)
            dbBase.AddInParameter(dbCommand, "@p_Apoderado", DbType.Int16, objRelacionAlumnosFamiliares.Apoderado)
            dbBase.AddInParameter(dbCommand, "@p_ResponsablePago", DbType.Int16, objRelacionAlumnosFamiliares.ResponsablePago)
            dbBase.AddInParameter(dbCommand, "@p_ViveConAlumno", DbType.Int16, objRelacionAlumnosFamiliares.ViveConAlumno)
            dbBase.AddInParameter(dbCommand, "@p_EmitirFactura", DbType.Int16, objRelacionAlumnosFamiliares.EmitirFactura)
            dbBase.AddInParameter(dbCommand, "@p_FacturaRazonSocial", DbType.String, objRelacionAlumnosFamiliares.FacturaRazonSocial)
            dbBase.AddInParameter(dbCommand, "@p_FacturaRuc", DbType.String, objRelacionAlumnosFamiliares.FacturaRUC)
            dbBase.AddInParameter(dbCommand, "@p_FacturaDireccionEmpresa", DbType.String, objRelacionAlumnosFamiliares.FacturaDireccionEmpresa)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        'Ficha Alumno Temporal (Interfaz Familia - Módulo Solicitudes Actualización Información)


        Public Function FUN_INS_AlumnoTemp(ByVal objSolicitud As be_SolicitudActualizacionFichaAlumnos, _
                  ByVal objAlumno As be_Alumnos, _
                  ByVal objFichaSeguro As be_FichaSeguroAlumno, _
                  ByVal str_CadenaCodigoPerfil As String, _
                  ByVal objDetalle As DataSet, _
                  ByRef str_Mensaje As String, _
                  ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer, ByVal fechaNacimiento As DateTime?, ByVal esRegistrado As Boolean?) As Integer

            Dim int_Valor As Integer = 0
            Dim int_CodigoSolicitud As Integer = 0

            Dim strUbigeoNacimiento As String = ""

            Dim codPais As Integer = 0
            If objDetalle.Tables("ubigeoNacimiento").Rows(0)("codPais").ToString() = 0 Then
                codPais = 0
            Else
                codPais = CInt(objDetalle.Tables("ubigeoNacimiento").Rows(0)("codPais").ToString())
            End If
            strUbigeoNacimiento &= objDetalle.Tables("ubigeoNacimiento").Rows(0)("codDepartamento").ToString()
            strUbigeoNacimiento &= objDetalle.Tables("ubigeoNacimiento").Rows(0)("codProvincia").ToString()
            strUbigeoNacimiento &= objDetalle.Tables("ubigeoNacimiento").Rows(0)("codDistrito").ToString()
            Try
                'Inicio la transaccion
                BeginTransaction()

                int_CodigoSolicitud = FUN_INS_Solicitud(objSolicitud.CodigoPeronsaSolicitante, _
                    objAlumno.CodigoPersona, str_CadenaCodigoPerfil, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                If int_CodigoSolicitud > 0 Then


                    '@p_CodigoSolicitud INT,                          
                    '@p_CodigoAlumno varchar(8),                                       
                    '@p_CodigoPersona INT,      

                    ' @p_codUbigeoNacimiento varchar(6),--parametro aumentado po  salcedo vila gaylussac   
                    ' @codPais int , --parametro aumentado po  salcedo vila gaylussac   

                    '  @fechaNacimineto datetime ,
                    '  @esRegistrado bit,
                    dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_Alumnos_Temp")

                    dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objAlumno.CodigoAlumno)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, objAlumno.CodigoPersona)



                    dbBase.AddInParameter(dbCommand, "@p_codUbigeoNacimiento", DbType.String, strUbigeoNacimiento)

                    If codPais = 0 Then
                        dbBase.AddInParameter(dbCommand, "@codPais", DbType.Int16, DBNull.Value)
                    Else
                        dbBase.AddInParameter(dbCommand, "@codPais", DbType.Int16, codPais)
                    End If

                    If fechaNacimiento = Nothing Then
                        dbBase.AddInParameter(dbCommand, "@fechaNacimineto", DbType.DateTime, DBNull.Value)
                    Else
                        dbBase.AddInParameter(dbCommand, "@fechaNacimineto", DbType.DateTime, fechaNacimiento)
                    End If
                    If esRegistrado = Nothing Then
                        dbBase.AddInParameter(dbCommand, "@esRegistrado", DbType.Boolean, DBNull.Value)
                    Else
                        dbBase.AddInParameter(dbCommand, "@esRegistrado", DbType.Boolean, esRegistrado)
                    End If



                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoDocIdentidad", DbType.Int16, IIf(objAlumno.CodigoTipoDocIdentidad = -1, DBNull.Value, objAlumno.CodigoTipoDocIdentidad))
                    dbBase.AddInParameter(dbCommand, "@p_NumeroDocIdentidad", DbType.String, IIf(objAlumno.NumeroDocIdentidad Is Nothing, DBNull.Value, objAlumno.NumeroDocIdentidad))

                    dbBase.AddInParameter(dbCommand, "@p_CantidadHermanos", DbType.Int16, IIf(objAlumno.CantidadHermanos = -1, DBNull.Value, objAlumno.CantidadHermanos))
                    dbBase.AddInParameter(dbCommand, "@p_PosicionEntreHermanos", DbType.Int16, IIf(objAlumno.PosicionEntreHermanos = -1, DBNull.Value, objAlumno.PosicionEntreHermanos))
                    dbBase.AddInParameter(dbCommand, "@p_EmailPersonal", DbType.String, IIf(objAlumno.EmailPersonal Is Nothing, DBNull.Value, objAlumno.EmailPersonal))
                    dbBase.AddInParameter(dbCommand, "@p_Celular", DbType.String, IIf(objAlumno.Celular Is Nothing, DBNull.Value, objAlumno.Celular))
                    dbBase.AddInParameter(dbCommand, "@p_ProfesaReligion", DbType.String, IIf(objAlumno.ProfesaReligion = -1, DBNull.Value, objAlumno.ProfesaReligion))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoReligion", DbType.Int16, IIf(objAlumno.CodigoReligion = -1, DBNull.Value, objAlumno.CodigoReligion))
                    dbBase.AddInParameter(dbCommand, "@p_Bautizo", DbType.Int16, IIf(objAlumno.Bautizo = -1, DBNull.Value, objAlumno.Bautizo))
                    dbBase.AddInParameter(dbCommand, "@p_BautizoLugar", DbType.String, IIf(objAlumno.BautizoLugar Is Nothing, DBNull.Value, objAlumno.BautizoLugar))
                    dbBase.AddInParameter(dbCommand, "@p_BautizoAnio", DbType.Int16, IIf(objAlumno.BautizoAnio = -1, DBNull.Value, objAlumno.BautizoAnio))
                    dbBase.AddInParameter(dbCommand, "@p_PrimeraComunion", DbType.Int16, IIf(objAlumno.PrimeraComunion = -1, DBNull.Value, objAlumno.PrimeraComunion))
                    dbBase.AddInParameter(dbCommand, "@p_PrimeraComunionLugar", DbType.String, IIf(objAlumno.PrimeraComunionLugar Is Nothing, DBNull.Value, objAlumno.PrimeraComunionLugar))
                    dbBase.AddInParameter(dbCommand, "@p_PrimeraComunionAnio", DbType.Int16, IIf(objAlumno.PrimeraComunionAnio = -1, DBNull.Value, objAlumno.PrimeraComunionAnio))
                    dbBase.AddInParameter(dbCommand, "@p_Confirmacion", DbType.Int16, IIf(objAlumno.Confirmacion = -1, DBNull.Value, objAlumno.Confirmacion))
                    dbBase.AddInParameter(dbCommand, "@p_ConfirmacionLugar", DbType.String, IIf(objAlumno.ConfirmacionLugar Is Nothing, DBNull.Value, objAlumno.ConfirmacionLugar))
                    dbBase.AddInParameter(dbCommand, "@p_ComfirmacionAnio", DbType.Int16, IIf(objAlumno.ConfirmacionAnio = -1, DBNull.Value, objAlumno.ConfirmacionAnio))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeo", DbType.String, IIf(objAlumno.CodigoUbigeo Is Nothing, DBNull.Value, objAlumno.CodigoUbigeo))
                    dbBase.AddInParameter(dbCommand, "@p_Urbanizacion", DbType.String, IIf(objAlumno.Urbanizacion Is Nothing, DBNull.Value, objAlumno.Urbanizacion))
                    dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, IIf(objAlumno.Direccion Is Nothing, DBNull.Value, objAlumno.Direccion))
                    dbBase.AddInParameter(dbCommand, "@p_ReferenciaDomiciliaria", DbType.String, IIf(objAlumno.ReferenciaDomiciliaria Is Nothing, DBNull.Value, objAlumno.ReferenciaDomiciliaria))
                    dbBase.AddInParameter(dbCommand, "@p_TelefonoCasa", DbType.String, IIf(objAlumno.TelefonoCasa Is Nothing, DBNull.Value, objAlumno.TelefonoCasa))
                    dbBase.AddInParameter(dbCommand, "@p_AccesoInternet", DbType.Int16, IIf(objAlumno.AccesoInternet = -1, DBNull.Value, objAlumno.AccesoInternet))
                    dbBase.AddInParameter(dbCommand, "@p_NombreContactoAvisoEmergencia", DbType.String, IIf(objAlumno.NombreContactoAvisoEmergencia Is Nothing, DBNull.Value, objAlumno.NombreContactoAvisoEmergencia))
                    dbBase.AddInParameter(dbCommand, "@p_TelfCasaContactoAvisoEmergencia", DbType.String, IIf(objAlumno.TelfCasaContactoAvisoEmergencia Is Nothing, DBNull.Value, objAlumno.TelfCasaContactoAvisoEmergencia))
                    dbBase.AddInParameter(dbCommand, "@p_TelfOficinaContactoAvisoEmergencia", DbType.String, IIf(objAlumno.TelfOficinaContactoAvisoEmergencia Is Nothing, DBNull.Value, objAlumno.TelfOficinaContactoAvisoEmergencia))
                    dbBase.AddInParameter(dbCommand, "@p_CellContactoAvisoEmergencia", DbType.String, IIf(objAlumno.CellContactoAvisoEmergencia Is Nothing, DBNull.Value, objAlumno.CellContactoAvisoEmergencia))

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                    'Parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                    'Ejecucion del Store Procedure
                    dbBase.ExecuteScalar(dbCommand, tran)
                    str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))




                    If Not int_Valor > 0 Then
                        str_Mensaje = "Ocurrio un error durante el registro."
                        Rollback()
                        Return int_Valor
                    Else

                        'Detalle Familiar
                        'DS.Tables(0) : Nacionalidades


                        FUN_INS_RelacionAlumno(objDetalle.Tables("relacionAlumno"), Transaccion, objAlumno.CodigoAlumno, int_CodigoSolicitud)

                        If objDetalle.Tables(0) IsNot Nothing Then
                            If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                                Dim objNacionalidadPersona As be_RelacionNacionalidadPersona
                                For Each dr As DataRow In objDetalle.Tables(0).Rows
                                    objNacionalidadPersona = New be_RelacionNacionalidadPersona
                                    objNacionalidadPersona.CodigoPersona = objAlumno.CodigoPersona
                                    objNacionalidadPersona.CodigoNacionalidad = dr.Item("CodigoNacionalidad")
                                    FUN_INS_NacionalidadPersonaTemp(int_CodigoSolicitud, objNacionalidadPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                Next
                            End If
                        End If

                        'DS.Tables(1) : Idiomas
                        If objDetalle.Tables(1) IsNot Nothing Then
                            If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                                Dim objIdiomaPersona As be_RelacionIdiomaPersona
                                For Each dr As DataRow In objDetalle.Tables(1).Rows
                                    objIdiomaPersona = New be_RelacionIdiomaPersona
                                    objIdiomaPersona.CodigoIdioma = dr.Item("CodigoIdioma")
                                    objIdiomaPersona.CodigoPersona = objAlumno.CodigoPersona
                                    FUN_INS_IdiomaPersonaTemp(int_CodigoSolicitud, objIdiomaPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                Next

                            End If
                        End If

                        Commit()
                        Return int_Valor

                    End If

                    'If int_Valor > 0 Then
                    '    Commit()
                    'Else
                    '    Rollback()
                    'End If

                    'Return int_Valor

                Else

                    str_Mensaje = "Ocurrio un error durante el registro."
                    Rollback()
                    Return 0

                End If


            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro." 'ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()
            End Try
        End Function

        Private Function FUN_INS_RelacionAlumno(ByVal dtRelacionAlumno As DataTable, ByVal objSqlTransaction As SqlTransaction, ByVal codAlumno As Integer, ByVal codSolicitud As Integer)
            Try
                'alter procedure USP_RelacionAlumnosFamiliares_TempAlum
                ' @p_CodigoAlumno int ,
                ' @p_CodigoSolicitud int,
                ' @p_CodigoIntegranteFamilia int ,
                ' @p_ResponsablePago bit,
                ' @p_ViveConAlumno bit
                'Column("viveConElla", GetType(Boolean))
                'dtDetalleRelacionFamiliaAlumno.Columns.Add(colRelacionAlumno)
                'colRelacionAlumno = New DataColumn("esPagante", GetType(Boolean))
                'dtDetalleRelacionFamiliaAlumno.Columns.Add(colRelacionAlumno)
                'colRelacionAlumno = New DataColumn("codigoIntegranteFamilia", GetType(Integer))
                Dim cod As Integer = 0
                Dim mensaje As String = ""
                For Each filasEntidadRelacion As DataRow In dtRelacionAlumno.Rows
                    dbCommand = Me.dbBase.GetStoredProcCommand("USP_RelacionAlumnosFamiliares_TempAlum")
                    ''dbCommand.Parameters.Clear()
                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, codAlumno)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int32, codSolicitud)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoIntegranteFamilia", DbType.Int32, Convert.ToInt32(filasEntidadRelacion("codigoIntegranteFamilia").ToString()))
                    dbBase.AddInParameter(dbCommand, "@p_ResponsablePago", DbType.Boolean, Convert.ToBoolean(filasEntidadRelacion("esPagante")))
                    dbBase.AddInParameter(dbCommand, "@p_ViveConAlumno", DbType.Boolean, Convert.ToBoolean(filasEntidadRelacion("viveConElla")))
                    dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 50)
                    dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)

                    dbBase.ExecuteScalar(dbCommand, objSqlTransaction)
                    cod = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                    mensaje = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                Next
            Catch ex As Exception

            End Try
        End Function

        Private Function FUN_INS_Solicitud(ByVal int_CodigoPersonaSolicitante As Integer, _
            ByVal int_CodigoPersonaActualizar As Integer, _
            ByVal str_CadenaCodigoPerfil As String, _
            ByVal objSqlTransaction As SqlTransaction, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_Mensaje As String = ""
            Dim int_Valor As Integer
            Dim int_CodigoSolicitud As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_SolicitudActualizacionFichaAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaSolicitante", DbType.Int16, int_CodigoPersonaSolicitante)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaActualizar", DbType.Int16, int_CodigoPersonaActualizar)
            dbBase.AddInParameter(dbCommand, "@p_CadenaCodigoPerfil", DbType.String, str_CadenaCodigoPerfil)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Parámetros de salida
            dbBase.AddOutParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
            dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

            'Ejecucion del Store Procedure
            dbBase.ExecuteScalar(dbCommand, objSqlTransaction)
            int_CodigoSolicitud = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_CodigoSolicitud")))
            str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
            int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Return int_CodigoSolicitud

        End Function

        'Ficha Alumno Temporal - Detalle
        Private Sub FUN_INS_NacionalidadPersonaTemp(ByVal int_CodigoSolicitud As Integer, ByVal objNacionalidadPersona As be_RelacionNacionalidadPersona, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_NacionalidadPersona_TempAlum")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, objNacionalidadPersona.CodigoPersona)
            dbBase.AddInParameter(dbCommand, "@p_CodigoNacionalidad", DbType.Int16, objNacionalidadPersona.CodigoNacionalidad)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_INS_IdiomaPersonaTemp(ByVal int_CodigoSolicitud As Integer, ByVal objIdiomaPersona As be_RelacionIdiomaPersona, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_IdiomaPersona_TempAlum")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, objIdiomaPersona.CodigoPersona)
            dbBase.AddInParameter(dbCommand, "@p_CodigoIdioma", DbType.Int16, objIdiomaPersona.CodigoIdioma)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Public Sub FUN_INS_TiposDiscapacidadAlumnosTemp(ByVal int_CodigoSolicitud As Integer, ByVal objTiposDiscapacidadAlumnos As be_RelacionTiposDiscapacidadesAlumnos, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_TiposDiscapacidadAlumnos_TempAlum")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objTiposDiscapacidadAlumnos.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoDiscapacidad", DbType.Int16, objTiposDiscapacidadAlumnos.CodigoTipoDiscapacidad)
            dbBase.AddInParameter(dbCommand, "@p_Descripcion", DbType.String, objTiposDiscapacidadAlumnos.Descripcion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Public Sub FUN_INS_ClinicasSeguroTemp(ByVal int_CodigoSolicitud As Integer, ByVal objClinicasSeguro As be_RelacionClinicasSeguro, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_ClinicasSeguro_TempAlum")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaSeguro", DbType.Int16, objClinicasSeguro.CodigoFichaSeguro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoClinica", DbType.Int16, objClinicasSeguro.CodigoClinica)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

#End Region

#Region "Metodos No Transaccionales"
        'Ficha de Alumno (Módulo Matricula)      
        ''' <summary>
        ''' Lista los datos de la Ficha del Alumno
        ''' </summary>
        ''' <param name="str_ApellidoPaterno">Apellido Paterno del alumno</param>
        ''' <param name="str_ApellidoMaterno">Apellido Materno del alumno</param>
        ''' <param name="str_Nombre">Nombre del alumno</param>
        ''' <param name="int_estadoAlumno">Estado del alumno</param>
        ''' <param name="int_Nivel">Nivel en que se encuentra el alumno</param>
        ''' <param name="int_SubNivel">SubNivel en que se encuentra el alumno</param>
        ''' <param name="int_Grado">Grado en que se encuentra el alumno</param>
        ''' <param name="int_Aula">Aula en que se encuentra el alumno</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Function FUN_LIS_FichaAlumno(ByVal str_Codigo As String, ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_estadoAlumno As Integer, ByVal int_Nivel As Integer, ByVal int_SubNivel As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, ByVal int_PeriodoInicio As Integer, ByVal int_PeriodoFin As Integer, ByVal int_Sede As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_Alumnos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.String, str_Codigo)
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, str_ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, str_ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombre", DbType.String, str_Nombre)
            dbBase.AddInParameter(cmd, "@p_SituacionAlumno", DbType.Int32, int_estadoAlumno)
            dbBase.AddInParameter(cmd, "@p_Nivel", DbType.Int32, int_Nivel)
            dbBase.AddInParameter(cmd, "@p_Subnivel", DbType.Int32, int_SubNivel)
            dbBase.AddInParameter(cmd, "@p_Grado", DbType.Int32, int_Grado)
            dbBase.AddInParameter(cmd, "@p_Aula", DbType.Int32, int_Aula)
            dbBase.AddInParameter(cmd, "@p_PeriodoInicio", DbType.Int32, int_PeriodoInicio)
            dbBase.AddInParameter(cmd, "@p_PeriodoFin", DbType.Int32, int_PeriodoFin)
            dbBase.AddInParameter(cmd, "@p_Sede", DbType.Int32, int_Sede)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Ficha Alumno (Módulo Matrícula)           
        Public Function FUN_GET_Alumno(ByVal int_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_Alumno")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.String, int_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        '-> Procedure con errores
        Public Function FUN_LIS_BuscarFamilias(ByVal apellidoPaterno As String, ByVal apellidoMaterno As String, ByVal nombre As String, ByVal tipoDocumento As Integer, ByVal numDoc As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_FA_BusquedaFamiliares")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, apellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, apellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, nombre)
            dbBase.AddInParameter(cmd, "@p_TipoDocumento", DbType.Int16, tipoDocumento)
            dbBase.AddInParameter(cmd, "@p_NumeroDocumento", DbType.String, numDoc)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function
        'alumnos que tienen atencion medica
        Public Function FUN_LIS_AlumnosFichaAtencion(ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_FAM_Alumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Validacion de Datos (Módulo Matrícula)
        Public Function FUN_GET_AlumnoActualizacion(ByVal int_Codigo As Integer, ByVal int_CodigoSolicitud As Integer, ByVal int_CodigoPerfil As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_AlumnosActualizacionPorSoliciutd")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_Codigo)
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(cmd, "@p_CodigoPerfil", DbType.Int16, int_CodigoPerfil)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_AlumnoActualizacion(ByVal objMaestroPersona As be_MaestroPersonas, _
            ByVal dtInicial As Date, ByVal dtFinal As Date, ByVal intEstado As Integer, ByVal int_CodigoPerfil As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosActualizacion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_FechaRangoInicial", DbType.DateTime, dtInicial)
            dbBase.AddInParameter(cmd, "@p_FechaRangoFinal", DbType.DateTime, dtFinal)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, intEstado)
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, objMaestroPersona.ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, objMaestroPersona.ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, objMaestroPersona.Nombre)
            dbBase.AddInParameter(cmd, "@p_AlumnoNivel", DbType.Int16, objMaestroPersona.AlumnoNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoSubnivel", DbType.Int16, objMaestroPersona.AlumnoSubnivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoGrado", DbType.Int16, objMaestroPersona.AlumnoGrado)
            dbBase.AddInParameter(cmd, "@p_AlumnoAula", DbType.Int16, objMaestroPersona.AlumnoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoPerfil", DbType.Int16, int_CodigoPerfil)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Datos para visualizacion del Familiar (Interfaz Familia - Módulo Solicitudes Actualización Información)
        Public Function FUN_GET_AlumnoVisualizacionActualizacionFamiliar(ByVal str_Codigo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            'Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_AlumnoDatosSolicitudesFamiliar")
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_AlumnoPlantillaActualizacion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_Codigo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Modulo de - "DATOS DE ALUMNOS"
        Public Function FUN_LIS_AlumnosPorCodigoFamilia(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int32, int_CodigoFamiliar)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Modulo de - "DATOS DE ALUMNOS"
        Public Function FUN_GET_AlumnosPorCodigoAlumno(ByVal int_CodigoFamilia As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_AlumnoPorCodigo")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_AlumnosPorCodigoFamiliaMatricula(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorFamiliaMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int32, int_CodigoFamiliar)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_CompanierosAlumnos(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoAnioAcademico As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_CompanierosAlumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.Int32, int_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_Grado", DbType.Int32, int_Grado)
            dbBase.AddInParameter(cmd, "@p_Aula", DbType.Int32, int_Aula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function


        Public Function FUN_GET_AlumnosPorCodigoAlumnoYPeriodo(ByVal int_CodigoFamilia As Integer, ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_AlumnosPorCodigoYPeriodo")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'MODULO DE ALUMNOS
        Public Function FUN_GET_FichaUnicaMatriculaAlumno(ByVal str_CodigoAlumno As String, ByVal int_PeriodoAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_FichaUnicaMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_PeriodoAcademico", DbType.Int16, int_PeriodoAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Lista Alumnos por Talonario - Ingresos Varios
        Public Function FUN_LIS_AlumnosPorTalonario(ByVal int_CodigoTalonario As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_AlumnosPorTalonario")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoTalonario", DbType.Int16, int_CodigoTalonario)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'Lista de Alumnos para Mid Term Report
        Public Function FUN_LIS_AlumnosMidTermReport(ByVal int_CodigoAula As Integer, ByVal int_CodigoPeriodoAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosMidTermReport")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_PeriodoAcademico", DbType.Int16, int_CodigoPeriodoAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AlumnosMidTermReportPorGradoYAula(ByVal int_CodigoAnioAcademico As Integer, _
                                                                  ByVal int_CodigoGrado As Integer, _
                                                                  ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosMidTermReportPorGradoYAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AlumnosWeeklyReportPorGradoYAula(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoBimestre As Integer, ByVal int_CodigoSemana As Integer, ByVal int_CodigoAula As Integer, _
           ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosWeeklyReportPorGradoYAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoBimestre", DbType.Int16, int_CodigoBimestre)
            dbBase.AddInParameter(cmd, "@p_CodigoSemana", DbType.Int16, int_CodigoSemana)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'Lista de Alumnos para Reportes
        Public Function FUN_LIS_AlumnosPorNivelSubNivelGradoAula(ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoNivel As Integer, ByVal int_CodigoSubnivel As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorNivelSubNivelGradoAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoNivel", DbType.Int16, int_CodigoNivel)
            dbBase.AddInParameter(cmd, "@p_CodigoSubnivel", DbType.Int16, int_CodigoSubnivel)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ''MODULO DE PROFESORES DE ALUMNOS
        Public Function FUN_LIS_ProfesoresPorAlumno(ByVal int_CodigoAlumno As Integer, ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_ProfesoresPorAlumnos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.Int32, int_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_AsignacionAulasAlumnos(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AsignacionAulaAlumno")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_HousesAlumno(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_HousesAlumno")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_LIS_ExoneradosPorCurso(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoAula As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_ExoneradosPorCurso")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        Public Function FUN_REP_ExoneradosPorCurso(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_REP_ExoneradosPorCurso")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        'Modulo Registro Notas Grado Pronosticos
        Public Function FUN_LIS_AlumnoPorAulasyAnioAcademico(ByVal int_CodigoAula As Integer, ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnoPorAulasyAnioAcademico")

            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function


        Public Function FUN_REP_AlumnosRetirados(ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosRetirados")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' Impresion Libretas 04/07/2012
        Public Function FUN_LIS_AlumnosPorAulayAnioAcademicoLibreta(ByVal int_CodigoAsignacionAula As Integer, ByVal int_CodigoAnioAcademico As Integer, _
          ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorAulayAnioAcademicoLibreta")

            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionAula", DbType.Int32, int_CodigoAsignacionAula)
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function

        ' 06/09/2012
        ' Registro deudas por servicios
        Public Function FUN_LIS_AlumnosPorAulaGradoyAnioAcademico( _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoAsignacionAula As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorGradoAulaAnioAcademico")

            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoAsignacionAula", DbType.Int16, int_CodigoAsignacionAula)
            'dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)

        End Function
        ''
        'listado x Año, grado,aula, apellido paterno materno y nombre
        Public Function FUN_LIS_FotosAlumnos( _
            ByVal int_CodigoAnioAcademico As Integer, _
            ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, ByVal str_CodigoAlumno As String, ByVal str_ApellidoPaterno As String, _
            ByVal str_apellidoMaterno As String, ByVal str_nombre As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_FotosAlumnos")

            'Parámetros de entrada

            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int16, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int16, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int16, int_CodigoAula)
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.String, str_CodigoAlumno)
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, str_ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, str_apellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, str_nombre)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure



            Return dbBase.ExecuteDataSet(cmd)





        End Function

        ' 20/11/2012
        ' Busqueda de alumnos para ingreso de notas
        Public Function FUN_LIS_AlumnosPorPeriodoYGrado( _
            ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorPeriodoYGrado")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function


        ' Busqueda de alumnos para registro de pre-matricula
        Public Function FUN_LIS_AlumnosPorPeriodoYGradoPreMatricula( _
            ByVal int_CodigoAnioAcademico As Integer, ByVal int_CodigoGrado As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorPeriodoYGradoPreMatricula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAnioAcademico", DbType.Int32, int_CodigoAnioAcademico)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function


        ' Entrevistas
        Public Function FUN_LIS_AlumnosPorFamiliaPeriodoGradoAula(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoPeriodo As Integer, ByVal int_CodigoGrado As Integer, ByVal int_CodigoAula As Integer, _
                                                                  ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_AlumnosPorFamiliaPeriodoGradoAula")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)
            dbBase.AddInParameter(cmd, "@p_CodigoPeriodo", DbType.Int32, int_CodigoPeriodo)
            dbBase.AddInParameter(cmd, "@p_CodigoGrado", DbType.Int32, int_CodigoGrado)
            dbBase.AddInParameter(cmd, "@p_CodigoAula", DbType.Int32, int_CodigoAula)

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





