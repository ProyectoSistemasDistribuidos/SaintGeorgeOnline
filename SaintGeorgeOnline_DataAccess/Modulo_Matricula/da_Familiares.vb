Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula

Namespace ModuloMatricula

    Public Class da_Familiares
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

        'Ficha Familiar (Módulo Matrícula)
        Public Function FUN_INS_Familiares(ByVal bool_FichaCompleta As Boolean, _
                                           ByVal objFamiliares As be_Familiares, _
                                           ByVal objDetalle As DataSet, _
            ByRef str_Mensaje As String, ByRef int_CodigoPersona As Integer, ByRef int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()

                dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_Familiares")

                dbBase.AddInParameter(dbCommand, "@p_EstadoFicha", DbType.Int32, Convert.ToInt32(bool_FichaCompleta))
                dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaINI", DbType.Int16, objFamiliares.CodigoPersona)

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_Contrasenia", DbType.String, objFamiliares.Contrasenia) 'Encriptada

                dbBase.AddInParameter(dbCommand, "@p_CodigoSexo", DbType.Int16, IIf(objFamiliares.CodigoSexo = -1, DBNull.Value, objFamiliares.CodigoSexo))
                dbBase.AddInParameter(dbCommand, "@p_CodigoEstadoCivil", DbType.Int16, IIf(objFamiliares.CodigoEstadoCivil = -1, DBNull.Value, objFamiliares.CodigoEstadoCivil))
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoDocIdentidad", DbType.Int16, IIf(objFamiliares.CodigoTipoDocIdentidad = -1, DBNull.Value, objFamiliares.CodigoTipoDocIdentidad))
                dbBase.AddInParameter(dbCommand, "@p_CodigoReligion", DbType.Int16, IIf(objFamiliares.CodigoReligion = -1, DBNull.Value, objFamiliares.CodigoReligion))
                dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeo", DbType.String, IIf(objFamiliares.CodigoUbigeo Is Nothing, DBNull.Value, objFamiliares.CodigoUbigeo))
                dbBase.AddInParameter(dbCommand, "@p_ApellidoPaterno", DbType.String, IIf(objFamiliares.ApellidoPaterno Is Nothing, DBNull.Value, objFamiliares.ApellidoPaterno))
                dbBase.AddInParameter(dbCommand, "@p_ApellidoMaterno", DbType.String, IIf(objFamiliares.ApellidoMaterno Is Nothing, DBNull.Value, objFamiliares.ApellidoMaterno))
                dbBase.AddInParameter(dbCommand, "@p_Nombre", DbType.String, IIf(objFamiliares.Nombre Is Nothing, DBNull.Value, objFamiliares.Nombre))
                dbBase.AddInParameter(dbCommand, "@p_FechaNacimiento", DbType.DateTime, IIf(objFamiliares.FechaNacimiento Is Nothing, DBNull.Value, objFamiliares.FechaNacimiento))
                dbBase.AddInParameter(dbCommand, "@p_NumeroDocIdentidad", DbType.String, IIf(objFamiliares.NumeroDocIdentidad Is Nothing, DBNull.Value, objFamiliares.NumeroDocIdentidad))
                dbBase.AddInParameter(dbCommand, "@p_EmailPersonal", DbType.String, IIf(objFamiliares.EmailPersonal Is Nothing, DBNull.Value, objFamiliares.EmailPersonal))
                dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, IIf(objFamiliares.Direccion Is Nothing, DBNull.Value, objFamiliares.Direccion))
                dbBase.AddInParameter(dbCommand, "@p_Urbanizacion", DbType.String, IIf(objFamiliares.Urbanizacion Is Nothing, DBNull.Value, objFamiliares.Urbanizacion))
                dbBase.AddInParameter(dbCommand, "@p_ReferenciaDomiciliaria", DbType.String, IIf(objFamiliares.ReferenciaDomiciliaria Is Nothing, DBNull.Value, objFamiliares.ReferenciaDomiciliaria))
                dbBase.AddInParameter(dbCommand, "@p_TelefonoCasa", DbType.String, IIf(objFamiliares.TelefonoCasa Is Nothing, DBNull.Value, objFamiliares.TelefonoCasa))
                dbBase.AddInParameter(dbCommand, "@p_Celular", DbType.String, IIf(objFamiliares.Celular Is Nothing, DBNull.Value, objFamiliares.Celular))
                dbBase.AddInParameter(dbCommand, "@p_ProfesaReligion", DbType.Int16, IIf(objFamiliares.ProfesaReligion = -1, DBNull.Value, objFamiliares.ProfesaReligion))

                dbBase.AddInParameter(dbCommand, "@p_CodigoServicioRadioDomicilio", DbType.Int16, IIf(objFamiliares.CodigoServicioRadioDomicilio = -1, DBNull.Value, objFamiliares.CodigoServicioRadioDomicilio))
                dbBase.AddInParameter(dbCommand, "@p_CodigoPais", DbType.Int16, IIf(objFamiliares.CodigoPaisDomicilio = -1, DBNull.Value, objFamiliares.CodigoPaisDomicilio))
                dbBase.AddInParameter(dbCommand, "@p_CodigoEscolaridadMinisterio", DbType.Int16, IIf(objFamiliares.CodigoEscolaridadMinisterio = -1, DBNull.Value, objFamiliares.CodigoEscolaridadMinisterio))
                dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeoCentroTrabajo", DbType.String, IIf(objFamiliares.CodigoUbigeoCentroTrabajo Is Nothing, DBNull.Value, objFamiliares.CodigoUbigeoCentroTrabajo))
                dbBase.AddInParameter(dbCommand, "@p_CodigoServicioRadioOficina", DbType.Int16, IIf(objFamiliares.CodigoServicioRadioOficina = -1, DBNull.Value, objFamiliares.CodigoServicioRadioOficina))
                dbBase.AddInParameter(dbCommand, "@p_CodigoNivelInstruccion", DbType.Int16, IIf(objFamiliares.CodigoNivelInstruccion = -1, DBNull.Value, objFamiliares.CodigoNivelInstruccion))
                dbBase.AddInParameter(dbCommand, "@p_codigosituacionlaboral", DbType.Int16, IIf(objFamiliares.codigosituacionlaboral = -1, DBNull.Value, objFamiliares.codigosituacionlaboral))
                dbBase.AddInParameter(dbCommand, "@p_Vive", DbType.Int16, IIf(objFamiliares.Vive = Nothing, DBNull.Value, objFamiliares.Vive))
                dbBase.AddInParameter(dbCommand, "@p_FechaDefuncion", DbType.DateTime, IIf(objFamiliares.FechaDefuncion = Nothing, DBNull.Value, objFamiliares.FechaDefuncion))
                dbBase.AddInParameter(dbCommand, "@p_ExAlumno", DbType.Int16, IIf(objFamiliares.ExAlumno = -1, DBNull.Value, objFamiliares.ExAlumno))
                dbBase.AddInParameter(dbCommand, "@p_ExAlumnoAnioEgreso", DbType.Int16, IIf(objFamiliares.ExAlumnoAnioEgreso = -1, DBNull.Value, objFamiliares.ExAlumnoAnioEgreso))

                'dbBase.AddInParameter(dbCommand, "@p_Usuario", DbType.String, IIf(objFamiliares.Usuario Is Nothing, DBNull.Value, objFamiliares.Usuario))
                'dbBase.AddInParameter(dbCommand, "@p_Contrasenia", DbType.String, IIf(objFamiliares.Contrasenia Is Nothing, DBNull.Value, objFamiliares.Contrasenia))

                dbBase.AddInParameter(dbCommand, "@p_OcupacionCargo", DbType.String, IIf(objFamiliares.OcupacionCargo Is Nothing, DBNull.Value, objFamiliares.OcupacionCargo))
                dbBase.AddInParameter(dbCommand, "@p_CentroTrabajo", DbType.String, IIf(objFamiliares.CentroTrabajo Is Nothing, DBNull.Value, objFamiliares.CentroTrabajo))
                dbBase.AddInParameter(dbCommand, "@p_CodigoPaisCentroTrabajo", DbType.Int16, IIf(objFamiliares.CodigoPaisCentroTrabajo = -1, DBNull.Value, objFamiliares.CodigoPaisCentroTrabajo))
                dbBase.AddInParameter(dbCommand, "@p_DireccionCentroTrabajo", DbType.String, IIf(objFamiliares.DireccionCentroTrabajo Is Nothing, DBNull.Value, objFamiliares.DireccionCentroTrabajo))
                dbBase.AddInParameter(dbCommand, "@p_TelefonoOficina", DbType.String, IIf(objFamiliares.TelefonoOficina Is Nothing, DBNull.Value, objFamiliares.TelefonoOficina))
                dbBase.AddInParameter(dbCommand, "@p_CelularOficina", DbType.String, IIf(objFamiliares.CelularOficina Is Nothing, DBNull.Value, objFamiliares.CelularOficina))
                dbBase.AddInParameter(dbCommand, "@p_NumeroServicioRadioOficina", DbType.String, IIf(objFamiliares.NumeroServicioRadioOficina Is Nothing, DBNull.Value, objFamiliares.NumeroServicioRadioOficina))
                dbBase.AddInParameter(dbCommand, "@p_EmailOficina", DbType.String, IIf(objFamiliares.EmailOficina Is Nothing, DBNull.Value, objFamiliares.EmailOficina))
                dbBase.AddInParameter(dbCommand, "@p_AccesoInternetOficina", DbType.Int16, IIf(objFamiliares.AccesoInternetOficina = -1, DBNull.Value, objFamiliares.AccesoInternetOficina))
                dbBase.AddInParameter(dbCommand, "@p_NombreIglesia", DbType.String, IIf(objFamiliares.NombreIglesia Is Nothing, DBNull.Value, objFamiliares.NombreIglesia))
                dbBase.AddInParameter(dbCommand, "@p_AccesoInternet", DbType.Int16, IIf(objFamiliares.AccesoInternet = -1, DBNull.Value, objFamiliares.AccesoInternet))
                dbBase.AddInParameter(dbCommand, "@p_NumeroServicioRadioPersonal", DbType.String, IIf(objFamiliares.NumeroServicioRadioPersonal Is Nothing, DBNull.Value, objFamiliares.NumeroServicioRadioPersonal))
                dbBase.AddInParameter(dbCommand, "@p_ColegioEgreso", DbType.String, IIf(objFamiliares.ColegioEgreso Is Nothing, DBNull.Value, objFamiliares.ColegioEgreso))
                dbBase.AddInParameter(dbCommand, "@p_ContinuaEstudios", DbType.String, IIf(objFamiliares.ContinuaEstudios Is Nothing, DBNull.Value, objFamiliares.ContinuaEstudios))

                dbBase.AddOutParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, 10)

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.Int16, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure
                dbBase.ExecuteScalar(dbCommand, tran)
                int_CodigoFamiliar = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_CodigoFamiliar")))
                int_CodigoPersona = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_CodigoPersona")))
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                If int_Valor > 0 Then 'Si actualizo la cabecera

                    'Detalle Familiar
                    'Nacionalidad
                    Dim objNacionalidadPersona As New be_RelacionNacionalidadPersona
                    objNacionalidadPersona.CodigoPersona = int_CodigoPersona
                    objNacionalidadPersona.CodigoNacionalidad = objFamiliares.CodigoNacionalidad
                    FUN_INS_NacionalidadPersona(objNacionalidadPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                    'DS.Tables(0) : Idiomas
                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objIdiomaPersona As be_RelacionIdiomaPersona
                            For Each dr As DataRow In objDetalle.Tables(0).Rows
                                objIdiomaPersona = New be_RelacionIdiomaPersona
                                objIdiomaPersona.CodigoIdioma = dr.Item("Codigo")
                                objIdiomaPersona.CodigoPersona = int_CodigoPersona
                                FUN_INS_IdiomaPersona(objIdiomaPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            Next

                        End If
                    End If

                    'DS.Tables(1) : Profesiones
                    If objDetalle.Tables(1) IsNot Nothing Then
                        If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFamiliarProfesion As be_RelacionFamiliarProfesion
                            For Each dr As DataRow In objDetalle.Tables(1).Rows
                                objFamiliarProfesion = New be_RelacionFamiliarProfesion
                                objFamiliarProfesion.CodigoProfesion = dr.Item("Codigo")
                                objFamiliarProfesion.CodigoFamiliar = int_Valor
                                FUN_INS_FamiliarProfesion(objFamiliarProfesion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            Next

                        End If
                    End If

                    'DS.Tables(2) : FichaAutos
                    If objDetalle.Tables(2) IsNot Nothing Then
                        If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                            Dim objFichaAutos As be_FichaAutos
                            For Each dr As DataRow In objDetalle.Tables(2).Rows
                                objFichaAutos = New be_FichaAutos
                                objFichaAutos.Placa = dr.Item("Placa")
                                objFichaAutos.Marca = dr.Item("Marca")
                                objFichaAutos.Modelo = dr.Item("Modelo")
                                objFichaAutos.CodigoFamiliar = int_Valor
                                FUN_INS_FichaAutos(objFichaAutos, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            Next

                        End If
                    End If

                    Commit()

                Else

                    Rollback()

                End If

                Return int_Valor

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro." 'ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_UPD_Familiares(ByVal bool_FichaCompleta As Boolean, _
                                           ByVal objFamiliares As be_Familiares, _
                                           ByVal objDetalle As DataSet, _
                                           ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()

                dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_Familiares")

                'Parámetros de entrada
                dbBase.AddInParameter(dbCommand, "@p_EstadoFicha", DbType.Int32, Convert.ToInt32(bool_FichaCompleta))
                dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objFamiliares.CodigoFamiliar)
                dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, objFamiliares.CodigoPersona)

                dbBase.AddInParameter(dbCommand, "@p_CodigoSexo", DbType.Int16, IIf(objFamiliares.CodigoSexo = -1, DBNull.Value, objFamiliares.CodigoSexo))
                dbBase.AddInParameter(dbCommand, "@p_CodigoEstadoCivil", DbType.Int16, IIf(objFamiliares.CodigoEstadoCivil = -1, DBNull.Value, objFamiliares.CodigoEstadoCivil))
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoDocIdentidad", DbType.Int16, IIf(objFamiliares.CodigoTipoDocIdentidad = -1, DBNull.Value, objFamiliares.CodigoTipoDocIdentidad))
                dbBase.AddInParameter(dbCommand, "@p_CodigoReligion", DbType.Int16, IIf(objFamiliares.CodigoReligion = -1, DBNull.Value, objFamiliares.CodigoReligion))
                dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeo", DbType.String, IIf(objFamiliares.CodigoUbigeo Is Nothing, DBNull.Value, objFamiliares.CodigoUbigeo))
                dbBase.AddInParameter(dbCommand, "@p_ApellidoPaterno", DbType.String, IIf(objFamiliares.ApellidoPaterno Is Nothing, DBNull.Value, objFamiliares.ApellidoPaterno))
                dbBase.AddInParameter(dbCommand, "@p_ApellidoMaterno", DbType.String, IIf(objFamiliares.ApellidoMaterno Is Nothing, DBNull.Value, objFamiliares.ApellidoMaterno))
                dbBase.AddInParameter(dbCommand, "@p_Nombre", DbType.String, IIf(objFamiliares.Nombre Is Nothing, DBNull.Value, objFamiliares.Nombre))
                dbBase.AddInParameter(dbCommand, "@p_FechaNacimiento", DbType.DateTime, IIf(objFamiliares.FechaNacimiento Is Nothing, DBNull.Value, objFamiliares.FechaNacimiento))
                dbBase.AddInParameter(dbCommand, "@p_NumeroDocIdentidad", DbType.String, IIf(objFamiliares.NumeroDocIdentidad Is Nothing, DBNull.Value, objFamiliares.NumeroDocIdentidad))
                dbBase.AddInParameter(dbCommand, "@p_EmailPersonal", DbType.String, IIf(objFamiliares.EmailPersonal Is Nothing, DBNull.Value, objFamiliares.EmailPersonal))
                dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, IIf(objFamiliares.Direccion Is Nothing, DBNull.Value, objFamiliares.Direccion))
                dbBase.AddInParameter(dbCommand, "@p_Urbanizacion", DbType.String, IIf(objFamiliares.Urbanizacion Is Nothing, DBNull.Value, objFamiliares.Urbanizacion))
                dbBase.AddInParameter(dbCommand, "@p_ReferenciaDomiciliaria", DbType.String, IIf(objFamiliares.ReferenciaDomiciliaria Is Nothing, DBNull.Value, objFamiliares.ReferenciaDomiciliaria))
                dbBase.AddInParameter(dbCommand, "@p_TelefonoCasa", DbType.String, IIf(objFamiliares.TelefonoCasa Is Nothing, DBNull.Value, objFamiliares.TelefonoCasa))
                dbBase.AddInParameter(dbCommand, "@p_Celular", DbType.String, IIf(objFamiliares.Celular Is Nothing, DBNull.Value, objFamiliares.Celular))
                dbBase.AddInParameter(dbCommand, "@p_ProfesaReligion", DbType.Int16, IIf(objFamiliares.ProfesaReligion = -1, DBNull.Value, objFamiliares.ProfesaReligion))

                dbBase.AddInParameter(dbCommand, "@p_CodigoServicioRadioDomicilio", DbType.Int16, IIf(objFamiliares.CodigoServicioRadioDomicilio = -1, DBNull.Value, objFamiliares.CodigoServicioRadioDomicilio))
                dbBase.AddInParameter(dbCommand, "@p_CodigoPais", DbType.Int16, IIf(objFamiliares.CodigoPaisDomicilio = -1, DBNull.Value, objFamiliares.CodigoPaisDomicilio))
                dbBase.AddInParameter(dbCommand, "@p_CodigoEscolaridadMinisterio", DbType.Int16, IIf(objFamiliares.CodigoEscolaridadMinisterio = -1, DBNull.Value, objFamiliares.CodigoEscolaridadMinisterio))
                dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeoCentroTrabajo", DbType.String, IIf(objFamiliares.CodigoUbigeoCentroTrabajo Is Nothing, DBNull.Value, objFamiliares.CodigoUbigeoCentroTrabajo))
                dbBase.AddInParameter(dbCommand, "@p_CodigoServicioRadioOficina", DbType.Int16, IIf(objFamiliares.CodigoServicioRadioOficina = -1, DBNull.Value, objFamiliares.CodigoServicioRadioOficina))
                dbBase.AddInParameter(dbCommand, "@p_CodigoNivelInstruccion", DbType.Int16, IIf(objFamiliares.CodigoNivelInstruccion = -1, DBNull.Value, objFamiliares.CodigoNivelInstruccion))
                dbBase.AddInParameter(dbCommand, "@p_codigosituacionlaboral", DbType.Int16, IIf(objFamiliares.codigosituacionlaboral = -1, DBNull.Value, objFamiliares.codigosituacionlaboral))
                dbBase.AddInParameter(dbCommand, "@p_Vive", DbType.Int16, IIf(objFamiliares.Vive = Nothing, DBNull.Value, objFamiliares.Vive))
                dbBase.AddInParameter(dbCommand, "@p_FechaDefuncion", DbType.DateTime, IIf(objFamiliares.FechaDefuncion = Nothing, DBNull.Value, objFamiliares.FechaDefuncion))
                dbBase.AddInParameter(dbCommand, "@p_ExAlumno", DbType.Int16, IIf(objFamiliares.ExAlumno = -1, DBNull.Value, objFamiliares.ExAlumno))
                dbBase.AddInParameter(dbCommand, "@p_ExAlumnoAnioEgreso", DbType.Int16, IIf(objFamiliares.ExAlumnoAnioEgreso = -1, DBNull.Value, objFamiliares.ExAlumnoAnioEgreso))
                dbBase.AddInParameter(dbCommand, "@p_Usuario", DbType.String, IIf(objFamiliares.Usuario Is Nothing, DBNull.Value, objFamiliares.Usuario))
                dbBase.AddInParameter(dbCommand, "@p_Contrasenia", DbType.String, IIf(objFamiliares.Contrasenia Is Nothing, DBNull.Value, objFamiliares.Contrasenia))
                dbBase.AddInParameter(dbCommand, "@p_OcupacionCargo", DbType.String, IIf(objFamiliares.OcupacionCargo Is Nothing, DBNull.Value, objFamiliares.OcupacionCargo))
                dbBase.AddInParameter(dbCommand, "@p_CentroTrabajo", DbType.String, IIf(objFamiliares.CentroTrabajo Is Nothing, DBNull.Value, objFamiliares.CentroTrabajo))
                dbBase.AddInParameter(dbCommand, "@p_CodigoPaisCentroTrabajo", DbType.Int16, IIf(objFamiliares.CodigoPaisCentroTrabajo = -1, DBNull.Value, objFamiliares.CodigoPaisCentroTrabajo))
                dbBase.AddInParameter(dbCommand, "@p_DireccionCentroTrabajo", DbType.String, IIf(objFamiliares.DireccionCentroTrabajo Is Nothing, DBNull.Value, objFamiliares.DireccionCentroTrabajo))
                dbBase.AddInParameter(dbCommand, "@p_TelefonoOficina", DbType.String, IIf(objFamiliares.TelefonoOficina Is Nothing, DBNull.Value, objFamiliares.TelefonoOficina))
                dbBase.AddInParameter(dbCommand, "@p_CelularOficina", DbType.String, IIf(objFamiliares.CelularOficina Is Nothing, DBNull.Value, objFamiliares.CelularOficina))
                dbBase.AddInParameter(dbCommand, "@p_NumeroServicioRadioOficina", DbType.String, IIf(objFamiliares.NumeroServicioRadioOficina Is Nothing, DBNull.Value, objFamiliares.NumeroServicioRadioOficina))
                dbBase.AddInParameter(dbCommand, "@p_EmailOficina", DbType.String, IIf(objFamiliares.EmailOficina Is Nothing, DBNull.Value, objFamiliares.EmailOficina))
                dbBase.AddInParameter(dbCommand, "@p_AccesoInternetOficina", DbType.Int16, IIf(objFamiliares.AccesoInternetOficina = -1, DBNull.Value, objFamiliares.AccesoInternetOficina))
                dbBase.AddInParameter(dbCommand, "@p_NombreIglesia", DbType.String, IIf(objFamiliares.NombreIglesia Is Nothing, DBNull.Value, objFamiliares.NombreIglesia))
                dbBase.AddInParameter(dbCommand, "@p_AccesoInternet", DbType.Int16, IIf(objFamiliares.AccesoInternet = -1, DBNull.Value, objFamiliares.AccesoInternet))
                dbBase.AddInParameter(dbCommand, "@p_NumeroServicioRadioPersonal", DbType.String, IIf(objFamiliares.NumeroServicioRadioPersonal Is Nothing, DBNull.Value, objFamiliares.NumeroServicioRadioPersonal))
                dbBase.AddInParameter(dbCommand, "@p_ColegioEgreso", DbType.String, IIf(objFamiliares.ColegioEgreso Is Nothing, DBNull.Value, objFamiliares.ColegioEgreso))
                dbBase.AddInParameter(dbCommand, "@p_ContinuaEstudios", DbType.String, IIf(objFamiliares.ContinuaEstudios Is Nothing, DBNull.Value, objFamiliares.ContinuaEstudios))

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

                If int_Valor > 0 Then 'Si actualizo la cabecera

                    'Elimino todos los detalles
                    FUN_DEL_NacionalidadPersona(objFamiliares.CodigoPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                    FUN_DEL_IdiomaPersona(objFamiliares.CodigoPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                    FUN_DEL_FamiliarProfesion(objFamiliares.CodigoFamiliar, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                    'FUN_DEL_FichaAutos(objFamiliares.CodigoFamiliar, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                    'Detalle Familiar
                    'Nacionalidad
                    Dim objNacionalidadPersona As New be_RelacionNacionalidadPersona
                    objNacionalidadPersona.CodigoPersona = objFamiliares.CodigoPersona
                    objNacionalidadPersona.CodigoNacionalidad = objFamiliares.CodigoNacionalidad
                    FUN_INS_NacionalidadPersona(objNacionalidadPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                    'DS.Tables(0) : Idiomas
                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objIdiomaPersona As be_RelacionIdiomaPersona
                            For Each dr As DataRow In objDetalle.Tables(0).Rows
                                objIdiomaPersona = New be_RelacionIdiomaPersona
                                objIdiomaPersona.CodigoIdioma = dr.Item("Codigo")
                                objIdiomaPersona.CodigoPersona = objFamiliares.CodigoPersona
                                FUN_INS_IdiomaPersona(objIdiomaPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            Next

                        End If
                    End If

                    'DS.Tables(1) : Profesiones
                    If objDetalle.Tables(1) IsNot Nothing Then
                        If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFamiliarProfesion As be_RelacionFamiliarProfesion
                            For Each dr As DataRow In objDetalle.Tables(1).Rows
                                objFamiliarProfesion = New be_RelacionFamiliarProfesion
                                objFamiliarProfesion.CodigoProfesion = dr.Item("Codigo")
                                objFamiliarProfesion.CodigoFamiliar = objFamiliares.CodigoFamiliar
                                FUN_INS_FamiliarProfesion(objFamiliarProfesion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            Next

                        End If
                    End If

                    'DS.Tables(2) : Ficha Autos
                    If objDetalle.Tables(2) IsNot Nothing Then
                        If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                            Dim objFichaAutos As be_FichaAutos
                            For Each dr As DataRow In objDetalle.Tables(2).Rows
                                objFichaAutos = New be_FichaAutos
                                If dr.Item("Tipo") = "T" Then
                                    objFichaAutos.CodigoFichaAuto = dr.Item("Codigo")
                                    objFichaAutos.Placa = dr.Item("Placa")
                                    objFichaAutos.Marca = dr.Item("Marca")
                                    objFichaAutos.Modelo = dr.Item("Modelo")
                                    objFichaAutos.CodigoFamiliar = int_Valor
                                    FUN_INS_FichaAutos(objFichaAutos, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                Else ' El tipo es Real
                                    If dr.Item("EstadoRegistro") = 1 Then
                                        objFichaAutos.CodigoFichaAuto = dr.Item("Codigo")
                                        objFichaAutos.Placa = dr.Item("Placa")
                                        objFichaAutos.Marca = dr.Item("Marca")
                                        objFichaAutos.Modelo = dr.Item("Modelo")
                                        objFichaAutos.CodigoFamiliar = int_Valor
                                        FUN_UPD_FichaAutos(objFichaAutos, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                    Else
                                        Dim int_Codigo As Integer = dr.Item("Codigo")
                                        FUN_DEL_FichaAutos(int_Codigo, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                    End If
                                End If

                            Next
                        End If
                    End If
                    Commit()

                Else

                    Rollback()

                End If

                Return int_Valor

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro." 'ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        Public Function FUN_DEL_Familiares(ByVal int_Codigo As Integer, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_Familiares")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_Codigo)

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

        'Ficha Familiar - Detalles
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

        Private Sub FUN_INS_IdiomaPersona(ByVal objIdiomaPersona As be_RelacionIdiomaPersona, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)
            'edgar
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

        Private Sub FUN_INS_FamiliarProfesion(ByVal objFamiliarProfesion As be_RelacionFamiliarProfesion, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_FamiliarProfesion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoProfesion", DbType.Int16, objFamiliarProfesion.CodigoProfesion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objFamiliarProfesion.CodigoFamiliar)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_INS_FichaAutos(ByVal objFichaAutos As be_FichaAutos, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_FichaAutos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objFichaAutos.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_Marca", DbType.String, objFichaAutos.Marca)
            dbBase.AddInParameter(dbCommand, "@p_Modelo", DbType.String, objFichaAutos.Modelo)
            dbBase.AddInParameter(dbCommand, "@p_Placa", DbType.String, objFichaAutos.Placa)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Public Sub FUN_UPD_FichaAutos(ByVal objFichaAutos As be_FichaAutos, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)
            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_FichaAutos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, objFichaAutos.CodigoFichaAuto)
            dbBase.AddInParameter(dbCommand, "@p_Marca", DbType.String, objFichaAutos.Marca)
            dbBase.AddInParameter(dbCommand, "@p_Modelo", DbType.String, objFichaAutos.Modelo)
            dbBase.AddInParameter(dbCommand, "@p_Placa", DbType.String, objFichaAutos.Placa)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
           dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)
        End Sub

        Private Sub FUN_INS_IntegranteFamilia(ByVal objIntegrantesFamilia As be_IntegrantesFamilia, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_IntegranteFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamilia", DbType.String, objIntegrantesFamilia.CodigoFamilia)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objIntegrantesFamilia.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_CodigoParentesco", DbType.Int16, objIntegrantesFamilia.CodigoParentesco)

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

        Private Sub FUN_DEL_FamiliarProfesion(ByVal int_CodigoFamiliar As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_FamiliarProfesion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, int_CodigoFamiliar)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_FichaAutos(ByVal int_CodigoFichaAutos As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_FichaAutos")
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_Codigo", DbType.Int16, int_CodigoFichaAutos)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_DEL_FamiliarFichaAuto(ByVal int_CodigoFamiliar As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_FamiliarFichaAuto")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, int_CodigoFamiliar)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        'Validacion de Datos (Módulo Matrícula)
        Public Function FUN_UPD_FamiliaresActualizacion( _
            ByVal intCodigoFamiliar As Integer, _
            ByVal intCodigoPersona As Integer, _
            ByVal intCodigoSolicitud As Integer, _
            ByVal intCodigoPerfil As Integer, _
            ByVal objDT_Cabecera As DataTable, _
            ByVal arrStrCodigossNacionalidad() As String, _
            ByVal arrStrCodigosIdioma() As String, _
            ByVal arrStrCodigosProfesion() As String, _
            ByVal arrStrCodigosAuto() As String, ByVal arrDescAuto() As String, _
            ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim detNacionalidad As Integer = 0
            Dim detIdioma As Integer = 0
            Dim detProfesion As Integer = 0
            Dim detAuto As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()

                For Each dr As DataRow In objDT_Cabecera.Rows

                    dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_FamiliaresActualizacion")

                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, intCodigoSolicitud)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, intCodigoFamiliar)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, intCodigoPersona)
                    dbBase.AddInParameter(dbCommand, "@p_Indice", DbType.Int16, dr.Item("Indice"))
                    dbBase.AddInParameter(dbCommand, "@p_ValorActualizar", DbType.String, dr.Item("CodigoCampo"))

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
                    'Ejecucion del Store Procedure
                    dbBase.ExecuteScalar(dbCommand, tran)

                Next

                'Detalle Familiar
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

                'Profesiones
                If arrStrCodigosProfesion IsNot Nothing Then
                    If arrStrCodigosProfesion.Length > 0 Then
                        FUN_DEL_FamiliarProfesion(intCodigoFamiliar, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFamiliarProfesion As be_RelacionFamiliarProfesion
                        For Each item As String In arrStrCodigosProfesion

                            If item = "-" Then : Exit For : End If

                            objFamiliarProfesion = New be_RelacionFamiliarProfesion
                            objFamiliarProfesion.CodigoProfesion = CInt(item)
                            objFamiliarProfesion.CodigoFamiliar = intCodigoFamiliar
                            FUN_INS_FamiliarProfesion(objFamiliarProfesion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        Next
                        detProfesion = 1

                    End If
                End If

                'Autos
                If arrStrCodigosAuto IsNot Nothing Then
                    If arrStrCodigosAuto.Length > 0 Then
                        FUN_DEL_FamiliarFichaAuto(intCodigoFamiliar, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaAuto As be_FichaAutos

                        For i As Integer = 0 To arrStrCodigosAuto.Length - 1
                            If arrStrCodigosAuto(i) = "-" Then : Exit For : End If
                            Dim arrItem As String() = arrDescAuto(i).Split("-")

                            objFichaAuto = New be_FichaAutos
                            objFichaAuto.Marca = arrItem(0)
                            objFichaAuto.Modelo = arrItem(1)
                            objFichaAuto.Placa = arrItem(2)
                            objFichaAuto.CodigoFamiliar = intCodigoFamiliar
                            FUN_INS_FichaAutos(objFichaAuto, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                        'For Each item As String In arrStrCodigosAuto

                        '    If item = "-" Then : Exit For : End If

                        '    Dim arrItem As String() = arrDescAuto.Split("-")

                        '    objFichaAuto = New be_FichaAutos
                        '    objFichaAuto.Marca = arrItem(0)
                        '    objFichaAuto.Modelo = arrItem(1)
                        '    objFichaAuto.Placa = arrItem(2)
                        '    objFichaAuto.CodigoFamiliar = intCodigoFamiliar
                        '    FUN_INS_FichaAutos(objFichaAuto, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        'Next
                        detAuto = 1

                    End If
                End If

                'Actualizo el estado de la solicitud de actualizacion
                int_Valor = FUN_UPD_EstadoSolicitudActualizacion(intCodigoSolicitud, intCodigoPerfil, detNacionalidad, detIdioma, detProfesion, Transaccion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                If int_Valor > 0 Then
                    Commit()
                Else
                    intCodigoFamiliar = int_Valor
                    Rollback()
                End If

                Return intCodigoFamiliar

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro." 'ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        ' update : retorno de datos del usuario que valida
        Private Function FUN_UPD_EstadoSolicitudActualizacion(ByVal CodigoSolicitud As Integer, ByVal CodigoPerfil As Integer, _
            ByVal detNacionalidad As Integer, ByVal detIdioma As Integer, ByVal detProfesion As Integer, _
            ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_SolicitudActualizacionFichaFamiliares")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPerfil", DbType.Int16, CodigoPerfil)
            dbBase.AddInParameter(dbCommand, "@p_detNacionalidad", DbType.Int16, detNacionalidad)
            dbBase.AddInParameter(dbCommand, "@p_detIdioma", DbType.Int16, detIdioma)
            dbBase.AddInParameter(dbCommand, "@p_detProfesion", DbType.Int16, detProfesion)

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

        'Ficha Familiar Temporal (Interfaz Familia - Módulo Solicitudes Actualización Información)
        Public Function FUN_INS_FamiliaresTemp(ByVal objSolicitud As be_SolicitudActualizacionFichaFamiliares, _
            ByVal objFamiliares As be_Familiares, _
            ByVal str_CadenaCodigoPerfil As String, _
            ByVal objDetalle As DataSet, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim int_CodigoSolicitud As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()

                int_CodigoSolicitud = FUN_INS_Solicitud(objSolicitud.CodigoPeronsaSolicitante, _
                    objFamiliares.CodigoPersona, _
                    str_CadenaCodigoPerfil, _
                    objSolicitud.TipoSolicitud, _
                    objSolicitud.CodigoAnioSolicitud, _
                    Transaccion, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                If int_CodigoSolicitud > 0 Then

                    dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_Familiares_Temp")

                    dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objFamiliares.CodigoFamiliar)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoPersona", DbType.Int16, objFamiliares.CodigoPersona)

                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_CodigoSexo", DbType.Int16, IIf(objFamiliares.CodigoSexo = -1, DBNull.Value, objFamiliares.CodigoSexo))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoEstadoCivil", DbType.Int16, IIf(objFamiliares.CodigoEstadoCivil = -1, DBNull.Value, objFamiliares.CodigoEstadoCivil))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoDocIdentidad", DbType.Int16, IIf(objFamiliares.CodigoTipoDocIdentidad = -1, DBNull.Value, objFamiliares.CodigoTipoDocIdentidad))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoReligion", DbType.Int16, IIf(objFamiliares.CodigoReligion = -1, DBNull.Value, objFamiliares.CodigoReligion))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeo", DbType.String, IIf(objFamiliares.CodigoUbigeo Is Nothing, DBNull.Value, objFamiliares.CodigoUbigeo))
                    dbBase.AddInParameter(dbCommand, "@p_ApellidoPaterno", DbType.String, IIf(objFamiliares.ApellidoPaterno Is Nothing, DBNull.Value, objFamiliares.ApellidoPaterno))
                    dbBase.AddInParameter(dbCommand, "@p_ApellidoMaterno", DbType.String, IIf(objFamiliares.ApellidoMaterno Is Nothing, DBNull.Value, objFamiliares.ApellidoMaterno))
                    dbBase.AddInParameter(dbCommand, "@p_Nombre", DbType.String, IIf(objFamiliares.Nombre Is Nothing, DBNull.Value, objFamiliares.Nombre))
                    dbBase.AddInParameter(dbCommand, "@p_FechaNacimiento", DbType.DateTime, IIf(objFamiliares.FechaNacimiento = "01/01/1753", DBNull.Value, objFamiliares.FechaNacimiento))
                    dbBase.AddInParameter(dbCommand, "@p_NumeroDocIdentidad", DbType.String, IIf(objFamiliares.NumeroDocIdentidad Is Nothing, DBNull.Value, objFamiliares.NumeroDocIdentidad))
                    dbBase.AddInParameter(dbCommand, "@p_EmailPersonal", DbType.String, IIf(objFamiliares.EmailPersonal Is Nothing, DBNull.Value, objFamiliares.EmailPersonal))
                    dbBase.AddInParameter(dbCommand, "@p_Direccion", DbType.String, IIf(objFamiliares.Direccion Is Nothing, DBNull.Value, objFamiliares.Direccion))
                    dbBase.AddInParameter(dbCommand, "@p_Urbanizacion", DbType.String, IIf(objFamiliares.Urbanizacion Is Nothing, DBNull.Value, objFamiliares.Urbanizacion))
                    dbBase.AddInParameter(dbCommand, "@p_ReferenciaDomiciliaria", DbType.String, IIf(objFamiliares.ReferenciaDomiciliaria Is Nothing, DBNull.Value, objFamiliares.ReferenciaDomiciliaria))
                    dbBase.AddInParameter(dbCommand, "@p_TelefonoCasa", DbType.String, IIf(objFamiliares.TelefonoCasa Is Nothing, DBNull.Value, objFamiliares.TelefonoCasa))
                    dbBase.AddInParameter(dbCommand, "@p_Celular", DbType.String, IIf(objFamiliares.Celular Is Nothing, DBNull.Value, objFamiliares.Celular))
                    dbBase.AddInParameter(dbCommand, "@p_ProfesaReligion", DbType.Int16, IIf(objFamiliares.ProfesaReligion = -1, DBNull.Value, objFamiliares.ProfesaReligion))

                    dbBase.AddInParameter(dbCommand, "@p_CodigoServicioRadioDomicilio", DbType.Int16, IIf(objFamiliares.CodigoServicioRadioDomicilio = -1, DBNull.Value, objFamiliares.CodigoServicioRadioDomicilio))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoPais", DbType.Int16, IIf(objFamiliares.CodigoPaisDomicilio = -1, DBNull.Value, objFamiliares.CodigoPaisDomicilio))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoEscolaridadMinisterio", DbType.Int16, IIf(objFamiliares.CodigoEscolaridadMinisterio = -1, DBNull.Value, objFamiliares.CodigoEscolaridadMinisterio))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUbigeoCentroTrabajo", DbType.String, IIf(objFamiliares.CodigoUbigeoCentroTrabajo Is Nothing, DBNull.Value, objFamiliares.CodigoUbigeoCentroTrabajo))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoServicioRadioOficina", DbType.Int16, IIf(objFamiliares.CodigoServicioRadioOficina = -1, DBNull.Value, objFamiliares.CodigoServicioRadioOficina))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoNivelInstruccion", DbType.Int16, IIf(objFamiliares.CodigoNivelInstruccion = -1, DBNull.Value, objFamiliares.CodigoNivelInstruccion))
                    dbBase.AddInParameter(dbCommand, "@p_codigosituacionlaboral", DbType.Int16, IIf(objFamiliares.codigosituacionlaboral = -1, DBNull.Value, objFamiliares.codigosituacionlaboral))
                    dbBase.AddInParameter(dbCommand, "@p_Vive", DbType.Int16, IIf(objFamiliares.Vive = -1, DBNull.Value, objFamiliares.Vive))
                    dbBase.AddInParameter(dbCommand, "@p_FechaDefuncion", DbType.DateTime, IIf(objFamiliares.FechaDefuncion = "01/01/1753", DBNull.Value, objFamiliares.FechaDefuncion))
                    dbBase.AddInParameter(dbCommand, "@p_ExAlumno", DbType.Int16, IIf(objFamiliares.ExAlumno = -1, DBNull.Value, objFamiliares.ExAlumno))
                    dbBase.AddInParameter(dbCommand, "@p_ExAlumnoAnioEgreso", DbType.Int16, IIf(objFamiliares.ExAlumnoAnioEgreso = -1, DBNull.Value, objFamiliares.ExAlumnoAnioEgreso))
                    dbBase.AddInParameter(dbCommand, "@p_Usuario", DbType.String, objFamiliares.Usuario)
                    dbBase.AddInParameter(dbCommand, "@p_Contrasenia", DbType.String, objFamiliares.Contrasenia)
                    dbBase.AddInParameter(dbCommand, "@p_OcupacionCargo", DbType.String, IIf(objFamiliares.OcupacionCargo Is Nothing, DBNull.Value, objFamiliares.OcupacionCargo))
                    dbBase.AddInParameter(dbCommand, "@p_CentroTrabajo", DbType.String, IIf(objFamiliares.CentroTrabajo Is Nothing, DBNull.Value, objFamiliares.CentroTrabajo))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoPaisCentroTrabajo", DbType.Int16, IIf(objFamiliares.CodigoPaisCentroTrabajo = -1, DBNull.Value, objFamiliares.CodigoPaisCentroTrabajo))
                    dbBase.AddInParameter(dbCommand, "@p_DireccionCentroTrabajo", DbType.String, IIf(objFamiliares.DireccionCentroTrabajo Is Nothing, DBNull.Value, objFamiliares.DireccionCentroTrabajo))
                    dbBase.AddInParameter(dbCommand, "@p_TelefonoOficina", DbType.String, IIf(objFamiliares.TelefonoOficina Is Nothing, DBNull.Value, objFamiliares.TelefonoOficina))
                    dbBase.AddInParameter(dbCommand, "@p_CelularOficina", DbType.String, IIf(objFamiliares.CelularOficina Is Nothing, DBNull.Value, objFamiliares.CelularOficina))
                    dbBase.AddInParameter(dbCommand, "@p_NumeroServicioRadioOficina", DbType.String, IIf(objFamiliares.NumeroServicioRadioOficina Is Nothing, DBNull.Value, objFamiliares.NumeroServicioRadioOficina))
                    dbBase.AddInParameter(dbCommand, "@p_EmailOficina", DbType.String, IIf(objFamiliares.EmailOficina Is Nothing, DBNull.Value, objFamiliares.EmailOficina))
                    dbBase.AddInParameter(dbCommand, "@p_AccesoInternetOficina", DbType.Int16, IIf(objFamiliares.AccesoInternetOficina = -1, DBNull.Value, objFamiliares.AccesoInternetOficina))
                    dbBase.AddInParameter(dbCommand, "@p_NombreIglesia", DbType.String, IIf(objFamiliares.NombreIglesia Is Nothing, DBNull.Value, objFamiliares.NombreIglesia))
                    dbBase.AddInParameter(dbCommand, "@p_AccesoInternet", DbType.Int16, IIf(objFamiliares.AccesoInternet = -1, DBNull.Value, objFamiliares.AccesoInternet))
                    dbBase.AddInParameter(dbCommand, "@p_NumeroServicioRadioPersonal", DbType.String, IIf(objFamiliares.NumeroServicioRadioPersonal Is Nothing, DBNull.Value, objFamiliares.NumeroServicioRadioPersonal))
                    dbBase.AddInParameter(dbCommand, "@p_ColegioEgreso", DbType.String, IIf(objFamiliares.ColegioEgreso Is Nothing, DBNull.Value, objFamiliares.ColegioEgreso))
                    dbBase.AddInParameter(dbCommand, "@p_ContinuaEstudios", DbType.String, IIf(objFamiliares.ContinuaEstudios Is Nothing, DBNull.Value, objFamiliares.ContinuaEstudios))

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
                        'Nacionalidad - Nacionalidad pertenece a una tabla Relacion
                        If objFamiliares.CodigoNacionalidad > 0 Then
                            Dim objNacionalidadPersona As New be_RelacionNacionalidadPersona
                            objNacionalidadPersona.CodigoPersona = objFamiliares.CodigoPersona
                            objNacionalidadPersona.CodigoNacionalidad = objFamiliares.CodigoNacionalidad
                            FUN_INS_NacionalidadPersonaTemp(int_CodigoSolicitud, objNacionalidadPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                        End If

                        'DS.Tables(0) : Idiomas
                        If objDetalle.Tables(0) IsNot Nothing Then
                            If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                                Dim objIdiomaPersona As be_RelacionIdiomaPersona
                                For Each dr As DataRow In objDetalle.Tables(0).Rows
                                    objIdiomaPersona = New be_RelacionIdiomaPersona
                                    objIdiomaPersona.CodigoIdioma = dr.Item("Codigo")
                                    objIdiomaPersona.CodigoPersona = objFamiliares.CodigoPersona
                                    FUN_INS_IdiomaPersonaTemp(int_CodigoSolicitud, objIdiomaPersona, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                Next

                            End If
                        End If

                        'DS.Tables(1) : Profesiones
                        If objDetalle.Tables(1) IsNot Nothing Then
                            If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                                Dim objFamiliarProfesion As be_RelacionFamiliarProfesion
                                For Each dr As DataRow In objDetalle.Tables(1).Rows
                                    objFamiliarProfesion = New be_RelacionFamiliarProfesion
                                    objFamiliarProfesion.CodigoProfesion = dr.Item("Codigo")
                                    objFamiliarProfesion.CodigoFamiliar = objFamiliares.CodigoFamiliar
                                    FUN_INS_FamiliarProfesionTemp(int_CodigoSolicitud, objFamiliarProfesion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                Next

                            End If
                        End If

                        'DS.Tables(2) : FichaAutos
                        If objDetalle.Tables(2) IsNot Nothing Then
                            If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                                Dim objFichaAutos As be_FichaAutos
                                For Each dr As DataRow In objDetalle.Tables(2).Rows
                                    objFichaAutos = New be_FichaAutos
                                    objFichaAutos.Placa = dr.Item("Placa")
                                    objFichaAutos.Marca = dr.Item("Marca")
                                    objFichaAutos.Modelo = dr.Item("Modelo")
                                    objFichaAutos.CodigoFamiliar = objFamiliares.CodigoFamiliar
                                    FUN_INS_FichaAutosTemp(int_CodigoSolicitud, objFichaAutos, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                                Next

                            End If
                        End If

                        Commit()
                        Return int_Valor

                    End If

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

        Private Function FUN_INS_Solicitud(ByVal int_CodigoPersonaSolicitante As Integer, _
            ByVal int_CodigoPersonaActualizar As Integer, _
            ByVal str_CadenaCodigoPerfil As String, _
            ByVal int_TipoSolicitud As Integer, _
            ByVal int_CodigoAnioSolicitud As Integer, _
            ByVal objSqlTransaction As SqlTransaction, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_Mensaje As String = ""
            Dim int_Valor As Integer
            Dim int_CodigoSolicitud As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_SolicitudActualizacionFichaFamiliares")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaSolicitante", DbType.Int16, int_CodigoPersonaSolicitante)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaActualizar", DbType.Int16, int_CodigoPersonaActualizar)
            dbBase.AddInParameter(dbCommand, "@p_CadenaCodigoPerfil", DbType.String, str_CadenaCodigoPerfil)
            dbBase.AddInParameter(dbCommand, "@p_TipoSolicitud", DbType.Int16, int_TipoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioSolicitud", DbType.Int16, int_CodigoAnioSolicitud)

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

        'Ficha Familiar Temporal - Detalle
        Private Sub FUN_INS_NacionalidadPersonaTemp(ByVal int_CodigoSolicitud As Integer, ByVal objNacionalidadPersona As be_RelacionNacionalidadPersona, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_NacionalidadPersona_Temp")

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

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_IdiomaPersona_Temp")

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

        Private Sub FUN_INS_FamiliarProfesionTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFamiliarProfesion As be_RelacionFamiliarProfesion, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_FamiliarProfesion_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoProfesion", DbType.Int16, objFamiliarProfesion.CodigoProfesion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objFamiliarProfesion.CodigoFamiliar)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        Private Sub FUN_INS_FichaAutosTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaAutos As be_FichaAutos, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_FichaAuto_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliar", DbType.Int16, objFichaAutos.CodigoFamiliar)
            dbBase.AddInParameter(dbCommand, "@p_Marca", DbType.String, objFichaAutos.Marca)
            dbBase.AddInParameter(dbCommand, "@p_Modelo", DbType.String, objFichaAutos.Modelo)
            dbBase.AddInParameter(dbCommand, "@p_Placa", DbType.String, objFichaAutos.Placa)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

#End Region

#Region "Metodos No Transaccionales"

        'Ficha Familiar (Módulo Matrícula)
        Public Function FUN_LIS_Familiar(ByVal objMaestroPersona As be_MaestroPersonas, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_Familiares")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, objMaestroPersona.ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, objMaestroPersona.ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, objMaestroPersona.Nombre)

            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarApellidoPaterno", DbType.String, objMaestroPersona.AlumnoFamiliarApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarApellidoMaterno", DbType.String, objMaestroPersona.AlumnoFamiliarApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarNombres", DbType.String, objMaestroPersona.AlumnoFamiliarNombres)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarNivel", DbType.Int16, objMaestroPersona.AlumnoFamiliarNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarSubnivel", DbType.Int16, objMaestroPersona.AlumnoFamiliarSubnivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarGrado", DbType.Int16, objMaestroPersona.AlumnoFamiliarGrado)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarAula", DbType.Int16, objMaestroPersona.AlumnoFamiliarAula)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, objMaestroPersona.EstadoPersona)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_Familiar(ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_Familiares")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int16, int_CodigoFamiliar)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_GET_FamiliarPorPersona(ByVal int_CodigoPersona As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_FamiliaresPorPersona")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoPersona", DbType.Int16, int_CodigoPersona)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Validacion de Datos (Módulo Matrícula)
        Public Function FUN_GET_FamiliarActualizacion(ByVal int_Codigo As Integer, ByVal int_CodigoSolicitud As Integer, ByVal int_CodigoPerfil As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_FamiliaresActualizacionPorSoliciutd")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int16, int_Codigo)
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(cmd, "@p_CodigoPerfil", DbType.Int16, int_CodigoPerfil)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        Public Function FUN_LIS_FamiliarActualizacion(ByVal objMaestroPersona As be_MaestroPersonas, _
            ByVal dtInicial As Date, ByVal dtFinal As Date, ByVal intEstado As Integer, ByVal int_CodigoPerfil As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_FamiliaresActualizacion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_FechaRangoInicial", DbType.DateTime, dtInicial)
            dbBase.AddInParameter(cmd, "@p_FechaRangoFinal", DbType.DateTime, dtFinal)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, intEstado)
            dbBase.AddInParameter(cmd, "@p_ApellidoPaterno", DbType.String, objMaestroPersona.ApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_ApellidoMaterno", DbType.String, objMaestroPersona.ApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_Nombres", DbType.String, objMaestroPersona.Nombre)
            dbBase.AddInParameter(cmd, "@p_CodigoPerfil", DbType.Int16, int_CodigoPerfil)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Lista de Familiares del Familiar
        Public Function FUN_LIS_FamiliaresPorCodigoFamiliar(ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_FamiliaresPorFamiliar")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int16, int_CodigoFamiliar)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Datos para visualizacion del Familiar (Interfaz Familia - Módulo Solicitudes Actualización Información)
        Public Function FUN_GET_FamiliarVisualizacionActualizacionFamiliar(ByVal int_CodigoFamiliar As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            'Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_FamiliaresDatosSolicitudesFamiliar")
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_GET_FamiliaresPlantillaActualizacion")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamiliar", DbType.Int16, int_CodigoFamiliar)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Modulo de - "DATOS DE FAMILIA"
        Public Function FUN_LIS_FamiliaresPorCodigoFamilia(ByVal int_CodigoFamilia As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_FamiliaresPorFamilia")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoFamilia", DbType.Int32, int_CodigoFamilia)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'listado de familiares por alumno
        Public Function FUN_LIS_FamiliaresPorAlumno(ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("MA_USP_LIS_FamiliaresPorAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_CodigoAlumno", DbType.String, str_CodigoAlumno)

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
