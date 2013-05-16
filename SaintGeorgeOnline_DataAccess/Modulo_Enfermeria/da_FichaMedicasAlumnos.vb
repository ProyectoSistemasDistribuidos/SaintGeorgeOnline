Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Namespace ModuloEnfermeria

    Public Class da_FichaMedicasAlumnos
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

        ''' <summary>
        ''' Actualiza los datos de la Ficha Medica del Alumno
        ''' </summary>
        ''' <param name="objFichaMedica">Datos de la entidad Ficha Medica</param>
        ''' <param name="objDetalle">Data set que almacena los datos de los detalles de la Ficha médica de alumno</param>
        ''' <param name="str_Mensaje"></param>
        ''' <returns>Código del alumno</returns>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Function FUN_UPD_FichaMedicaAlumno(ByVal objFichaMedica As be_FichaMedica, ByVal objDetalle As DataSet, ByRef str_Mensaje As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_MiMensaje As String = ""
            Dim int_Valor As Integer = 0

            Try

                'Inicio la transaccion
                BeginTransaction()

                'Registro la cabecera
                dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_UPD_FichaMedicasAlumnos")

                'Datos Generales de la cabecera
                dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedica.CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoSangre", DbType.Int16, IIf(objFichaMedica.CodigoTipoSangre = 0, DBNull.Value, objFichaMedica.CodigoTipoSangre))
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoNacimiento", DbType.Int16, IIf(objFichaMedica.CodigoTipoNacimiento = 0, DBNull.Value, objFichaMedica.CodigoTipoNacimiento))
                dbBase.AddInParameter(dbCommand, "@p_TipoNacimientoObservaciones", DbType.String, objFichaMedica.TipoNacimientoObservaciones)
                dbBase.AddInParameter(dbCommand, "@p_EdadLevantoCabeza", DbType.Int16, objFichaMedica.EdadLevantoCabeza)
                dbBase.AddInParameter(dbCommand, "@p_MesesLevantoCabeza", DbType.Int16, objFichaMedica.MesesLevantoCabeza)
                dbBase.AddInParameter(dbCommand, "@p_EdadSento", DbType.Int16, objFichaMedica.EdadSento)
                dbBase.AddInParameter(dbCommand, "@p_MesesSento", DbType.Int16, objFichaMedica.MesesSento)
                dbBase.AddInParameter(dbCommand, "@p_EdadParo", DbType.Int16, objFichaMedica.EdadParo)
                dbBase.AddInParameter(dbCommand, "@p_MesesParo", DbType.Int16, objFichaMedica.MesesParo)
                dbBase.AddInParameter(dbCommand, "@p_EdadCamino", DbType.Int16, objFichaMedica.EdadCamino)
                dbBase.AddInParameter(dbCommand, "@p_MesesCamino", DbType.Int16, objFichaMedica.MesesCamino)
                dbBase.AddInParameter(dbCommand, "@p_EdadControloEsfinteres", DbType.Int16, objFichaMedica.EdadControloEsfinteres)
                dbBase.AddInParameter(dbCommand, "@p_MesesControloEsfinteres", DbType.Int16, objFichaMedica.MesesControloEsfinteres)
                dbBase.AddInParameter(dbCommand, "@p_EdadHabloPrimerasPalabras", DbType.Int16, objFichaMedica.EdadHabloPrimerasPalabras)
                dbBase.AddInParameter(dbCommand, "@p_MesesHabloPrimerasPalabras", DbType.Int16, objFichaMedica.MesesHabloPrimerasPalabras)
                dbBase.AddInParameter(dbCommand, "@p_EdadHabloFluidez", DbType.Int16, objFichaMedica.EdadHabloFluidez)
                dbBase.AddInParameter(dbCommand, "@p_MesesHabloFluidez", DbType.Int16, objFichaMedica.MesesHabloFluidez)

                dbBase.AddInParameter(dbCommand, "@p_TabiqueDesviado", DbType.Int16, IIf(objFichaMedica.TabiqueDesviado = -1, DBNull.Value, objFichaMedica.TabiqueDesviado))
                dbBase.AddInParameter(dbCommand, "@p_SangradoNasal", DbType.Int16, IIf(objFichaMedica.SangradoNasal = -1, DBNull.Value, objFichaMedica.SangradoNasal))
                dbBase.AddInParameter(dbCommand, "@p_UsaLentes", DbType.Int16, IIf(objFichaMedica.UsaLentes = -1, DBNull.Value, objFichaMedica.UsaLentes))
                dbBase.AddInParameter(dbCommand, "@p_ObservacionesOftalmologicas", DbType.String, objFichaMedica.ObservacionesOftalmologicas)
                dbBase.AddInParameter(dbCommand, "@p_ObservacionesDental", DbType.String, objFichaMedica.ObservacionesDental)
                dbBase.AddInParameter(dbCommand, "@p_UsaOrtodoncia", DbType.Int16, IIf(objFichaMedica.UsaOrtodoncia = -1, DBNull.Value, objFichaMedica.UsaOrtodoncia))

                dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
                dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

                'Parámetros de salida
                dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.String, 10)
                dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                'Ejecucion del Store Procedure : Registro Cabecera
                dbBase.ExecuteScalar(dbCommand, tran)
                str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                int_Valor = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

                'Elimino todos los detalles

                FUN_DEL_FichaMedicaEnfermedad(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaVacuna(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaCaracteristicasPiel(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaMedicamento(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaHospitalizacion(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaOperacion(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaControlPesoTalla(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaTipoControl(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaAlergia(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaDatosSeguro(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                FUN_DEL_FichaMedicaRentaEstudiantil(int_Valor, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                'Agrego los nuevos detalles de la Ficha Medica 
                'DS.Tables(0) : Enfermedad

                If objDetalle.Tables(0) IsNot Nothing Then
                    If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaEnfermedad As be_RelacionFichaMedicasEnfermedades

                        For Each dr As DataRow In objDetalle.Tables(0).Rows

                            objFichaMedicaEnfermedad = New be_RelacionFichaMedicasEnfermedades
                            objFichaMedicaEnfermedad.CodigoAlumno = int_Valor
                            objFichaMedicaEnfermedad.CodigoEnfermedad = dr.Item("CodigoEnfermedad")
                            objFichaMedicaEnfermedad.Edad = IIf(dr.Item("Edad").ToString = "", 0, dr.Item("Edad"))
                            FUN_INS_FichaMedicaEnfermedad(objFichaMedicaEnfermedad, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(1) : Vacuna
                If objDetalle.Tables(1) IsNot Nothing Then
                    If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaVacuna As be_RelacionFichaMedicasVacunas

                        For Each dr As DataRow In objDetalle.Tables(1).Rows

                            objFichaMedicaVacuna = New be_RelacionFichaMedicasVacunas
                            objFichaMedicaVacuna.CodigoAlumno = int_Valor
                            objFichaMedicaVacuna.CodigoVacuna = dr.Item("CodigoVacuna")
                            objFichaMedicaVacuna.CodigoDosis = dr.Item("CodigoDosis")
                            objFichaMedicaVacuna.Edad = IIf(dr.Item("Edad").ToString = "", 0, dr.Item("Edad"))
                            objFichaMedicaVacuna.FechaVacunacion = dr.Item("FechaVacunacion")
                            FUN_INS_FichaMedicaVacuna(objFichaMedicaVacuna, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(2) : Caracteristicas de la piel
                If objDetalle.Tables(2) IsNot Nothing Then
                    If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaCaracteristicasPiel As be_RelacionFichaMedicasCarecteristicasPiel

                        For Each dr As DataRow In objDetalle.Tables(2).Rows

                            objFichaMedicaCaracteristicasPiel = New be_RelacionFichaMedicasCarecteristicasPiel
                            objFichaMedicaCaracteristicasPiel.CodigoAlumno = int_Valor
                            objFichaMedicaCaracteristicasPiel.CodigoCaracteristicapiel = dr.Item("CodigoCaracteristicapiel")
                            objFichaMedicaCaracteristicasPiel.FechaRegistro = dr.Item("FechaRegistro")
                            FUN_INS_FichaMedicaCaracteristicasPiel(objFichaMedicaCaracteristicasPiel, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(3) : Medicamento
                If objDetalle.Tables(3) IsNot Nothing Then
                    If objDetalle.Tables(3).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaMedicamento As be_RelacionFichaMedicasMedicamentos

                        For Each dr As DataRow In objDetalle.Tables(3).Rows

                            objFichaMedicaMedicamento = New be_RelacionFichaMedicasMedicamentos
                            objFichaMedicaMedicamento.CodigoAlumno = int_Valor
                            objFichaMedicaMedicamento.CodigoMedicamento = dr.Item("CodigoMedicamento")
                            objFichaMedicaMedicamento.CodigoPresentacion = dr.Item("CodigoPresentacion")
                            objFichaMedicaMedicamento.CantidadPresentacion = dr.Item("CantidadPresentacion")
                            objFichaMedicaMedicamento.DosisMedicamento = dr.Item("DosisMedicamento")
                            objFichaMedicaMedicamento.Observaciones = dr.Item("Observaciones")
                            FUN_INS_FichaMedicaMedicamento(objFichaMedicaMedicamento, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(4) : Hospitalizacion
                If objDetalle.Tables(4) IsNot Nothing Then
                    If objDetalle.Tables(4).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaMotivoHospitalizacion As be_RelacionFichaMedicasMotivoHospitalizacion

                        For Each dr As DataRow In objDetalle.Tables(4).Rows

                            objFichaMedicaMotivoHospitalizacion = New be_RelacionFichaMedicasMotivoHospitalizacion
                            objFichaMedicaMotivoHospitalizacion.CodigoAlumno = int_Valor
                            objFichaMedicaMotivoHospitalizacion.CodigoMotivoHospitalizacion = dr.Item("CodigoMotivoHospitalizacion")
                            objFichaMedicaMotivoHospitalizacion.FechaHospitalizacion = dr.Item("FechaHospitalizacion")
                            FUN_INS_FichaMedicaHospitalizacion(objFichaMedicaMotivoHospitalizacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(5) : Operacion
                If objDetalle.Tables(5) IsNot Nothing Then
                    If objDetalle.Tables(5).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaOperacion As be_RelacionFichaMedicasOperaciones

                        For Each dr As DataRow In objDetalle.Tables(5).Rows

                            objFichaMedicaOperacion = New be_RelacionFichaMedicasOperaciones
                            objFichaMedicaOperacion.CodigoAlumno = int_Valor
                            objFichaMedicaOperacion.CodigoTipoOperaciones = dr.Item("CodigoTipoOperaciones")
                            objFichaMedicaOperacion.FechaOperacion = dr.Item("FechaOperacion")
                            FUN_INS_FichaMedicaOperacion(objFichaMedicaOperacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(6) : Otros Controles 
                If objDetalle.Tables(6) IsNot Nothing Then
                    If objDetalle.Tables(6).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaTipoControl As be_RelacionFichaMedicasTiposControles

                        For Each dr As DataRow In objDetalle.Tables(6).Rows

                            objFichaMedicaTipoControl = New be_RelacionFichaMedicasTiposControles
                            objFichaMedicaTipoControl.CodigoAlumno = int_Valor
                            objFichaMedicaTipoControl.CodigoTipoControl = dr.Item("CodigoTipoControl")
                            objFichaMedicaTipoControl.FechaControl = dr.Item("FechaControl")
                            objFichaMedicaTipoControl.Resultado = dr.Item("Resultado")
                            FUN_INS_FichaMedicaTipoControl(objFichaMedicaTipoControl, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If


                'DS.Tables(7) : ControlPesoTalla
                If objDetalle.Tables(7) IsNot Nothing Then
                    If objDetalle.Tables(7).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaControlPesoTalla As be_RelacionFichaMedicasControlesPesoTalla

                        For Each dr As DataRow In objDetalle.Tables(7).Rows

                            objFichaMedicaControlPesoTalla = New be_RelacionFichaMedicasControlesPesoTalla
                            objFichaMedicaControlPesoTalla.CodigoAlumno = int_Valor
                            objFichaMedicaControlPesoTalla.Talla = CDec(dr.Item("Talla"))
                            objFichaMedicaControlPesoTalla.Peso = CDec(dr.Item("Peso"))
                            objFichaMedicaControlPesoTalla.FechaControl = dr.Item("FechaControl")
                            objFichaMedicaControlPesoTalla.Observaciones = dr.Item("Observaciones")
                            FUN_INS_FichaMedicaControlPesoTalla(objFichaMedicaControlPesoTalla, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(8) : Alergia
                If objDetalle.Tables(8) IsNot Nothing Then
                    If objDetalle.Tables(8).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaAlergia As be_RelacionFichaMedicasAlergias

                        For Each dr As DataRow In objDetalle.Tables(8).Rows

                            objFichaMedicaAlergia = New be_RelacionFichaMedicasAlergias
                            objFichaMedicaAlergia.CodigoAlumno = int_Valor
                            objFichaMedicaAlergia.CodigoAlergia = dr.Item("CodigoAlergia")
                            objFichaMedicaAlergia.FechaRegistro = dr.Item("FechaRegistro")

                            FUN_INS_FichaMedicaAlergia(objFichaMedicaAlergia, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If

                'DS.Tables(9) : Datos Seguro
                If objDetalle.Tables(9) IsNot Nothing Then
                    If objDetalle.Tables(9).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaSeguro As be_FichaSeguro

                        For Each dr As DataRow In objDetalle.Tables(9).Rows

                            objFichaMedicaSeguro = New be_FichaSeguro
                            objFichaMedicaSeguro.CodigoAlumno = int_Valor
                            objFichaMedicaSeguro.CodigoAnioAcademico = dr.Item("CodigoAnio")
                            objFichaMedicaSeguro.CodigoTipoSeguro = dr.Item("CodigoTipoSeguro")
                            objFichaMedicaSeguro.CodigoCompaniaSeguro = dr.Item("CodigoCompania")
                            objFichaMedicaSeguro.NumeroPoliza = dr.Item("NumeroPoliza")
                            objFichaMedicaSeguro.VigenciaIndefinida = dr.Item("Vigencia")
                            objFichaMedicaSeguro.FechaInicio = dr.Item("FechaInicio")
                            objFichaMedicaSeguro.FechaFin = dr.Item("FechaFin")
                            objFichaMedicaSeguro.Compania = dr.Item("AmbulanciaCompania")
                            objFichaMedicaSeguro.Telefono = dr.Item("TelefonoAmbulancia")
                            objFichaMedicaSeguro.CopiaCarnetSeguro = dr.Item("CopiaCarnetSeguro")
                            objFichaMedicaSeguro.CodigoClinica = dr.Item("CodigoClinica")

                            FUN_INS_FichaMedicaSeguro(objFichaMedicaSeguro, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If


                'DS.Tables(10) : Renta Estudiantil
                If objDetalle.Tables(10) IsNot Nothing Then
                    If objDetalle.Tables(10).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                        Dim objFichaMedicaRentaEstudiantil As be_FichaSeguroRentaEstudiantil

                        For Each dr As DataRow In objDetalle.Tables(10).Rows

                            objFichaMedicaRentaEstudiantil = New be_FichaSeguroRentaEstudiantil
                            objFichaMedicaRentaEstudiantil.CodigoAlumno = int_Valor
                            objFichaMedicaRentaEstudiantil.CodigoAnioAcademico = dr.Item("CodigoAnioAcademico")
                            objFichaMedicaRentaEstudiantil.CodigoFamiliarPrimerTitular = dr.Item("CodigoFamiliarPrimerTitular")
                            objFichaMedicaRentaEstudiantil.CodigoFamiliarSegundoTitular = dr.Item("CodigoFamiliarSegundoTitular")

                            FUN_INS_FichaMedicaRentaEstudiantil(objFichaMedicaRentaEstudiantil, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next

                    End If
                End If
                Commit()

                Return int_Valor 'Integer.Parse(CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor")))

            Catch ex As Exception

                str_Mensaje = ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        ''' <summary>
        ''' Inserta los datos en el detalle de las enfermedades  
        ''' </summary>
        ''' <param name="objFichaMedicaEnfermedad">Datos de la entidad Relacion de Ficha Medica de enfermedades </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaEnfermedad(ByVal objFichaMedicaEnfermedad As be_RelacionFichaMedicasEnfermedades, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaEnfermedad")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaEnfermedad.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEnfermedad", DbType.Int16, objFichaMedicaEnfermedad.CodigoEnfermedad)
            dbBase.AddInParameter(dbCommand, "@p_Edad", DbType.Int16, objFichaMedicaEnfermedad.Edad)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Vacunas.  
        ''' </summary>
        ''' <param name="objFichaMedicaVacuna">Datos de la entidad Relacion de Ficha Medica de Vacuna </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaVacuna(ByVal objFichaMedicaVacuna As be_RelacionFichaMedicasVacunas, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaVacuna")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaVacuna.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoVacuna", DbType.Int16, objFichaMedicaVacuna.CodigoVacuna)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDosis", DbType.Int16, objFichaMedicaVacuna.CodigoDosis)
            dbBase.AddInParameter(dbCommand, "@p_Edad", DbType.Int16, objFichaMedicaVacuna.Edad)
            dbBase.AddInParameter(dbCommand, "@p_FechaVacunacion", DbType.Date, objFichaMedicaVacuna.FechaVacunacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Alergias.  
        ''' </summary>
        ''' <param name="objFichaMedicaAlergia">Datos de la entidad Relacion de Ficha Medica de Alergias </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaAlergia(ByVal objFichaMedicaAlergia As be_RelacionFichaMedicasAlergias, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaAlergia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaAlergia.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlergia", DbType.Int16, objFichaMedicaAlergia.CodigoAlergia)
            dbBase.AddInParameter(dbCommand, "@p_FechaRegistro", DbType.Date, objFichaMedicaAlergia.FechaRegistro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Alergias.  
        ''' </summary>
        ''' <param name="objFichaMedicaSeguro">Datos de la entidad Relacion de Ficha Medica de Alergias </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     24/01/2012
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaSeguro(ByVal objFichaMedicaSeguro As be_FichaSeguro, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_FichaMedicaSeguro")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, objFichaMedicaSeguro.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnio", DbType.Int32, objFichaMedicaSeguro.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipo", DbType.Int32, objFichaMedicaSeguro.CodigoTipoSeguro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCompania", DbType.Int32, objFichaMedicaSeguro.CodigoCompaniaSeguro)
            dbBase.AddInParameter(dbCommand, "@p_NumeroPoliza", DbType.String, objFichaMedicaSeguro.NumeroPoliza)
            dbBase.AddInParameter(dbCommand, "@p_Vigencia", DbType.Int32, objFichaMedicaSeguro.VigenciaIndefinida)
            dbBase.AddInParameter(dbCommand, "@p_FechaInicio", DbType.DateTime, IIf(objFichaMedicaSeguro.FechaInicio = Nothing, DBNull.Value, objFichaMedicaSeguro.FechaInicio))
            dbBase.AddInParameter(dbCommand, "@p_FechaFin", DbType.DateTime, IIf(objFichaMedicaSeguro.FechaFin = Nothing, DBNull.Value, objFichaMedicaSeguro.FechaFin))
            dbBase.AddInParameter(dbCommand, "@p_Compania", DbType.String, objFichaMedicaSeguro.Compania)
            dbBase.AddInParameter(dbCommand, "@p_Telefono", DbType.String, objFichaMedicaSeguro.Telefono)
            dbBase.AddInParameter(dbCommand, "@p_CopiaCarnetSeguro", DbType.Int32, objFichaMedicaSeguro.CopiaCarnetSeguro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoClinica", DbType.String, objFichaMedicaSeguro.CodigoClinica)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Caracteristicas de la piel.  
        ''' </summary>
        ''' <param name="objFichaMedicaRentaEstudiantil">Datos de la entidad Relacion de Ficha Medica de Caracteristicas de la piel </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     20/02/2012
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaRentaEstudiantil(ByVal objFichaMedicaRentaEstudiantil As be_FichaSeguroRentaEstudiantil, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_INS_FichaMedicaRentaEstudiantil")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, objFichaMedicaRentaEstudiantil.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAnioAcademico", DbType.Int32, objFichaMedicaRentaEstudiantil.CodigoAnioAcademico)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliarPrimerTitular", DbType.Int32, objFichaMedicaRentaEstudiantil.CodigoFamiliarPrimerTitular)
            dbBase.AddInParameter(dbCommand, "@p_CodigoFamiliarSegundoTitular", DbType.Int32, objFichaMedicaRentaEstudiantil.CodigoFamiliarSegundoTitular)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Caracteristicas de la piel.  
        ''' </summary>
        ''' <param name="objFichaMedicaCaracteristicasPiel">Datos de la entidad Relacion de Ficha Medica de Caracteristicas de la piel </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaCaracteristicasPiel(ByVal objFichaMedicaCaracteristicasPiel As be_RelacionFichaMedicasCarecteristicasPiel, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaCaracteristicasPiel")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaCaracteristicasPiel.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCaracteristicapiel", DbType.Int16, objFichaMedicaCaracteristicasPiel.CodigoCaracteristicapiel)
            dbBase.AddInParameter(dbCommand, "@p_FechaRegistro", DbType.Date, objFichaMedicaCaracteristicasPiel.FechaRegistro)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de los medicamentos que usa el alumno.  
        ''' </summary>
        ''' <param name="objFichaMedicaMedicamento">Datos de la entidad Relacion de Ficha Medica de medicamentos </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaMedicamento(ByVal objFichaMedicaMedicamento As be_RelacionFichaMedicasMedicamentos, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasMedicamentos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaMedicamento.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMedicamento", DbType.Int16, objFichaMedicaMedicamento.CodigoMedicamento)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPresentacion", DbType.Int16, objFichaMedicaMedicamento.CodigoPresentacion)
            dbBase.AddInParameter(dbCommand, "@p_CantidadPresentacion", DbType.String, objFichaMedicaMedicamento.CantidadPresentacion)
            dbBase.AddInParameter(dbCommand, "@p_DosisMedicamento", DbType.String, objFichaMedicaMedicamento.DosisMedicamento)
            dbBase.AddInParameter(dbCommand, "@p_Observaciones", DbType.String, objFichaMedicaMedicamento.Observaciones)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las hospitalizaciones que haya tenido el alumno.  
        ''' </summary>
        ''' <param name="objFichaMedicaHospitalizacion">Datos de la entidad Relacion de Ficha Medica de Hospitalización </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaHospitalizacion(ByVal objFichaMedicaHospitalizacion As be_RelacionFichaMedicasMotivoHospitalizacion, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasMotivoHospitalizacion")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaHospitalizacion.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMotivoHospitalizacion", DbType.Int16, objFichaMedicaHospitalizacion.CodigoMotivoHospitalizacion)
            dbBase.AddInParameter(dbCommand, "@p_FechaHospitalizacion", DbType.Date, objFichaMedicaHospitalizacion.FechaHospitalizacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Operaciones que haya tenido el alumno.  
        ''' </summary>
        ''' <param name="objFichaMedicaOperacion">Datos de la entidad Relacion de Ficha Medica de Operación. </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaOperacion(ByVal objFichaMedicaOperacion As be_RelacionFichaMedicasOperaciones, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasOperaciones")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaOperacion.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoOperaciones", DbType.Int16, objFichaMedicaOperacion.CodigoTipoOperaciones)
            dbBase.AddInParameter(dbCommand, "@p_FechaOperacion", DbType.Date, objFichaMedicaOperacion.FechaOperacion)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de los tipos de control que haya tenido el alumno.  
        ''' </summary>
        ''' <param name="objFichaMedicaTipoControl">Datos de la entidad Relacion de Ficha Medica de Tipos de control. </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaTipoControl(ByVal objFichaMedicaTipoControl As be_RelacionFichaMedicasTiposControles, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasTiposControles")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaTipoControl.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoControl", DbType.Int16, objFichaMedicaTipoControl.CodigoTipoControl)
            dbBase.AddInParameter(dbCommand, "@p_FechaControl", DbType.Date, objFichaMedicaTipoControl.FechaControl)
            dbBase.AddInParameter(dbCommand, "@p_Resultado", DbType.String, objFichaMedicaTipoControl.Resultado)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle del conrol de peso y talla  que haya tenido el alumno.  
        ''' </summary>
        ''' <param name="objFichaMedicaControlPesoTalla">Datos de la entidad Relacion de Ficha Medica de control de peso y talla. </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaControlPesoTalla(ByVal objFichaMedicaControlPesoTalla As be_RelacionFichaMedicasControlesPesoTalla, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasControlPesoTalla")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, objFichaMedicaControlPesoTalla.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_Talla", DbType.Decimal, objFichaMedicaControlPesoTalla.Talla)
            dbBase.AddInParameter(dbCommand, "@p_Peso", DbType.Decimal, objFichaMedicaControlPesoTalla.Peso)
            dbBase.AddInParameter(dbCommand, "@p_FechaControl", DbType.Date, objFichaMedicaControlPesoTalla.FechaControl)
            dbBase.AddInParameter(dbCommand, "@p_Observaciones", DbType.String, objFichaMedicaControlPesoTalla.Observaciones)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de enfermedad.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Codigo de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaEnfermedad(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaEnfermedad")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Vacuna.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Codigo de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaVacuna(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaVacuna")
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Alergia.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Codigo de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaAlergia(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaAlergia")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Alergia.
        ''' </summary>
        ''' <param name="int_CodigoAlumno">Codigo de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaDatosSeguro(ByVal int_CodigoAlumno As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_FichaMedicaSeguro")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, int_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Renta estudiantil.
        ''' </summary>
        ''' <param name="int_CodigoAlumno">Codigo de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     20/02/2012
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaRentaEstudiantil(ByVal int_CodigoAlumno As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_DEL_FichaMedicaRentaEstudiantil")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.Int32, int_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub
        ''' <summary>
        ''' Eliminación del detalle de Caracteristicas de la Piel.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Código de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaCaracteristicasPiel(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaCaracteristicasPiel")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Medicamentos.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Código de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaMedicamento(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaMedicamentos")
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Hospitalización.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Código de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaHospitalizacion(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaMotivoHospitalizacion")
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Operación de la Ficha médica.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Código de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaOperacion(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaOperaciones")
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Contrl de Peso y Talla de la Ficha médica.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Código de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Sub FUN_DEL_FichaMedicaControlPesoTalla(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaControlPesoTalla")
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Eliminación del detalle de Tipo de Control de la Ficha médica.
        ''' </summary>
        ''' <param name="int_CodigoFichaMedica">Código de Alumno</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        ''' 
        Private Sub FUN_DEL_FichaMedicaTipoControl(ByVal int_CodigoFichaMedica As Integer, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_DEL_FichaMedicaTiposControles")
            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.Int32, int_CodigoFichaMedica)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Actualiza los datos de la Ficha Medica del Alumno
        ''' </summary>
        ''' <param name="intCodigoFichaMedica">Codigo del alumno</param>
        ''' <param name="intCodigoPersona">Codigo de la persona </param>
        ''' <param name="intCodigoSolicitud">Codigo de la solicitud</param>
        ''' <param name="intCodigoPerfil">Codigo del Perfil</param>
        ''' <param name="objDT_Cabecera">Tabla que almacena los datos de la Ficha médica de alumno</param>
        ''' <param name="objDetalle">Data set,almacena los datos de los detalles de la Ficha médica de alumno</param>
        ''' <param name="str_Mensaje">Mensaje que se muestra</param>
        ''' <returns>Código del alumno</returns>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Function FUN_UPD_FichaMedicaActualizacion( _
            ByVal intCodigoFichaMedica As String, _
            ByVal intCodigoPersona As Integer, _
            ByVal intCodigoSolicitud As Integer, _
            ByVal intCodigoPerfil As Integer, _
            ByVal objDT_Cabecera As DataTable, _
            ByVal objDetalle As DataSet, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, _
            ByVal int_CodigoTipoUsuario As Integer, _
            ByVal int_CodigoModulo As Integer, _
            ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As Integer = 0
            Dim detEnfermedad As Integer = 0
            Dim detVacuna As Integer = 0
            Dim detcaracteristicasPiel As Integer = 0
            Dim detMedicamentos As Integer = 0
            Dim detHospitalizacion As Integer = 0
            Dim detOperacion As Integer = 0
            Dim detTipoControl As Integer = 0
            Dim detAlergia As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()

                For Each dr As DataRow In objDT_Cabecera.Rows

                    dbCommand = Me.dbBase.GetStoredProcCommand("MA_USP_UPD_FichaMedicaActualizacion")

                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, intCodigoSolicitud)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoFichaMedica", DbType.String, intCodigoFichaMedica)
                    dbBase.AddInParameter(dbCommand, "@p_Indice", DbType.Int16, dr.Item("Indice"))
                    dbBase.AddInParameter(dbCommand, "@p_ValorActualizar", DbType.String, dr.Item("CodigoCampo"))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.String, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
                    'Ejecucion del Store Procedure
                    dbBase.ExecuteScalar(dbCommand, tran)

                Next

                'Agrego los nuevos detalles de la Ficha Medica 
                'DS.Tables(0) : Enfermedad

                If objDetalle.Tables(0) IsNot Nothing Then

                    If objDetalle.Tables(0).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                        'Elimino todos los detalles
                        FUN_DEL_FichaMedicaEnfermedad(intCodigoFichaMedica, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaMedicaEnfermedad As be_RelacionFichaMedicasEnfermedades

                        For Each dr As DataRow In objDetalle.Tables(0).Rows

                            objFichaMedicaEnfermedad = New be_RelacionFichaMedicasEnfermedades
                            objFichaMedicaEnfermedad.CodigoAlumno = intCodigoFichaMedica
                            objFichaMedicaEnfermedad.CodigoEnfermedad = dr.Item("CodigoEnfermedad")
                            objFichaMedicaEnfermedad.Edad = CInt(dr.Item("Edad"))
                            FUN_INS_FichaMedicaEnfermedad(objFichaMedicaEnfermedad, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next
                        detEnfermedad = 1
                    End If
                End If

                'DS.Tables(1) : Vacuna
                If objDetalle.Tables(1) IsNot Nothing Then

                    If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                        'Elimino todos los detalles
                        FUN_DEL_FichaMedicaVacuna(intCodigoFichaMedica, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaMedicaVacuna As be_RelacionFichaMedicasVacunas

                        For Each dr As DataRow In objDetalle.Tables(1).Rows

                            objFichaMedicaVacuna = New be_RelacionFichaMedicasVacunas
                            objFichaMedicaVacuna.CodigoAlumno = intCodigoFichaMedica
                            objFichaMedicaVacuna.CodigoVacuna = dr.Item("CodigoVacuna")
                            objFichaMedicaVacuna.CodigoDosis = dr.Item("CodigoDosis")
                            objFichaMedicaVacuna.FechaVacunacion = dr.Item("FechaVacunacion")
                            FUN_INS_FichaMedicaVacuna(objFichaMedicaVacuna, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next
                        detVacuna = 1
                    End If
                End If

                'DS.Tables(2) : Caracteristicas de la piel
                If objDetalle.Tables(2) IsNot Nothing Then

                    If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                        'Elimino todos los detalles
                        FUN_DEL_FichaMedicaCaracteristicasPiel(intCodigoFichaMedica, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaMedicaCaracteristicasPiel As be_RelacionFichaMedicasCarecteristicasPiel

                        For Each dr As DataRow In objDetalle.Tables(2).Rows

                            objFichaMedicaCaracteristicasPiel = New be_RelacionFichaMedicasCarecteristicasPiel
                            objFichaMedicaCaracteristicasPiel.CodigoAlumno = intCodigoFichaMedica
                            objFichaMedicaCaracteristicasPiel.CodigoCaracteristicapiel = dr.Item("CodigoCaractPiel")
                            objFichaMedicaCaracteristicasPiel.FechaRegistro = dr.Item("FechaRegistro")
                            FUN_INS_FichaMedicaCaracteristicasPiel(objFichaMedicaCaracteristicasPiel, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next
                        detcaracteristicasPiel = 1
                    End If
                End If

                'DS.Tables(3) : Medicamento()
                If objDetalle.Tables(3) IsNot Nothing Then

                    If objDetalle.Tables(3).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                        'Elimino todos los detalles
                        FUN_DEL_FichaMedicaMedicamento(intCodigoFichaMedica, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaMedicaMedicamento As be_RelacionFichaMedicasMedicamentos

                        For Each dr As DataRow In objDetalle.Tables(3).Rows

                            objFichaMedicaMedicamento = New be_RelacionFichaMedicasMedicamentos
                            objFichaMedicaMedicamento.CodigoAlumno = intCodigoFichaMedica
                            objFichaMedicaMedicamento.CodigoMedicamento = dr.Item("CodigoMedicamento")
                            objFichaMedicaMedicamento.CodigoPresentacion = dr.Item("CodigoFrecuenciaUso")
                            objFichaMedicaMedicamento.FechaRegistro = dr.Item("FechaRegistro")
                            FUN_INS_FichaMedicaMedicamento(objFichaMedicaMedicamento, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next
                        detMedicamentos = 1
                    End If
                End If

                'DS.Tables(4) : Hospitalizacion
                If objDetalle.Tables(4) IsNot Nothing Then

                    If objDetalle.Tables(4).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                        'Elimino todos los detalles
                        FUN_DEL_FichaMedicaHospitalizacion(intCodigoFichaMedica, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaMedicaMotivoHospitalizacion As be_RelacionFichaMedicasMotivoHospitalizacion

                        For Each dr As DataRow In objDetalle.Tables(4).Rows

                            objFichaMedicaMotivoHospitalizacion = New be_RelacionFichaMedicasMotivoHospitalizacion
                            objFichaMedicaMotivoHospitalizacion.CodigoAlumno = intCodigoFichaMedica
                            objFichaMedicaMotivoHospitalizacion.CodigoMotivoHospitalizacion = dr.Item("CodigoMotivoHospitalizacion")
                            objFichaMedicaMotivoHospitalizacion.FechaHospitalizacion = dr.Item("FechaHospitalizacion")
                            FUN_INS_FichaMedicaHospitalizacion(objFichaMedicaMotivoHospitalizacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next
                        detHospitalizacion = 1
                    End If
                End If

                'DS.Tables(5) : Operacion
                If objDetalle.Tables(5) IsNot Nothing Then

                    If objDetalle.Tables(5).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                        'Elimino todos los detalles
                        FUN_DEL_FichaMedicaOperacion(intCodigoFichaMedica, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaMedicaOperacion As be_RelacionFichaMedicasOperaciones

                        For Each dr As DataRow In objDetalle.Tables(5).Rows

                            objFichaMedicaOperacion = New be_RelacionFichaMedicasOperaciones
                            objFichaMedicaOperacion.CodigoAlumno = intCodigoFichaMedica
                            objFichaMedicaOperacion.CodigoTipoOperaciones = dr.Item("CodigoTipoOperaciones")
                            objFichaMedicaOperacion.FechaOperacion = dr.Item("FechaOperacion")
                            FUN_INS_FichaMedicaOperacion(objFichaMedicaOperacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next
                        detOperacion = 1
                    End If
                End If

                'DS.Tables(6) : Otros Controles 
                If objDetalle.Tables(6) IsNot Nothing Then

                    If objDetalle.Tables(6).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                        'Elimino todos los detalles
                        FUN_DEL_FichaMedicaTipoControl(intCodigoFichaMedica, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaMedicaTipoControl As be_RelacionFichaMedicasTiposControles

                        For Each dr As DataRow In objDetalle.Tables(6).Rows

                            objFichaMedicaTipoControl = New be_RelacionFichaMedicasTiposControles
                            objFichaMedicaTipoControl.CodigoAlumno = intCodigoFichaMedica
                            objFichaMedicaTipoControl.CodigoTipoControl = dr.Item("CodigoTipoControl")
                            objFichaMedicaTipoControl.FechaControl = dr.Item("FechaControl")
                            objFichaMedicaTipoControl.Resultado = dr.Item("Resultado")
                            FUN_INS_FichaMedicaTipoControl(objFichaMedicaTipoControl, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next
                        detTipoControl = 1
                    End If
                End If


                'DS.Tables(7) : Alergia
                If objDetalle.Tables(7) IsNot Nothing Then

                    If objDetalle.Tables(7).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo
                        'Elimino todos los detalles
                        FUN_DEL_FichaMedicaAlergia(intCodigoFichaMedica, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Dim objFichaMedicaAlergia As be_RelacionFichaMedicasAlergias

                        For Each dr As DataRow In objDetalle.Tables(7).Rows

                            objFichaMedicaAlergia = New be_RelacionFichaMedicasAlergias
                            objFichaMedicaAlergia.CodigoAlumno = intCodigoFichaMedica
                            objFichaMedicaAlergia.CodigoAlergia = dr.Item("CodigoAlergia")
                            objFichaMedicaAlergia.FechaRegistro = dr.Item("FechaRegistro")

                            FUN_INS_FichaMedicaAlergia(objFichaMedicaAlergia, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                        Next
                        detAlergia = 1
                    End If
                End If

                'Actualizo el estado de la solicitud de actualizacion
                int_Valor = FUN_UPD_EstadoSolicitudActualizacion(intCodigoSolicitud, intCodigoPerfil, detEnfermedad, detVacuna, detcaracteristicasPiel, detMedicamentos, detHospitalizacion, detOperacion, detTipoControl, detAlergia, Transaccion, str_Mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                If int_Valor > 0 Then
                    Commit()
                    'str_Mensaje = "Transacción exitosa."
                Else
                    intCodigoFichaMedica = int_Valor
                    Rollback()
                End If

                Return CInt(intCodigoFichaMedica)

            Catch ex As Exception

                str_Mensaje = "Ocurrio un error durante el registro." 'ex.Message
                Rollback()
                Return 0

            Finally

                Conexion.Close()

            End Try

        End Function

        ''' <summary>
        ''' Actualiza el estado de solicitud de la Ficha Medica del Alumno
        ''' </summary>
        ''' <param name="CodigoSolicitud">Codigo de solicitud</param>
        ''' <param name="CodigoPerfil">Codigo de perfil </param>
        ''' <param name="detEnfermedad">Nos indica si tiene al menos un registro donde el valor=1;sino tiene registros el valor=0</param>
        ''' <param name="detVacuna">Nos indica si tiene al menos un registro donde el valor=1;sino tiene registros el valor=0</param>
        ''' <param name="detcaracteristicasPiel">Nos indica si tiene al menos un registro donde el valor=1;sino tiene registros el valor=0</param>
        ''' <param name="detMedicamentos">Nos indica si tiene al menos un registro donde el valor=1;sino tiene registros el valor=0</param>
        ''' <param name="detHospitalizacion">Nos indica si tiene al menos un registro donde el valor=1;sino tiene registros el valor=0</param>
        ''' <param name="detOperacion">Nos indica si tiene al menos un registro donde el valor=1;sino tiene registros el valor=0</param>
        ''' <param name="detTipoControl">Nos indica si tiene al menos un registro donde el valor=1;sino tiene registros el valor=0</param>
        ''' <param name="detAlergia">Nos indica si tiene al menos un registro donde el valor=1;sino tiene registros el valor=0</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <param name="str_Mensaje">Mensaje que se muestra</param>
        ''' <returns>Código del alumno</returns>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Function FUN_UPD_EstadoSolicitudActualizacion(ByVal CodigoSolicitud As Integer, ByVal CodigoPerfil As Integer, _
            ByVal detEnfermedad As Integer, ByVal detVacuna As Integer, ByVal detcaracteristicasPiel As Integer, _
            ByVal detMedicamentos As Integer, ByVal detHospitalizacion As Integer, ByVal detOperacion As Integer, _
            ByVal detTipoControl As Integer, ByVal detAlergia As Integer, _
            ByVal objSqlTransaction As SqlTransaction, ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_UPD_SolicitudActualizacionFichaMedicaAlumnos")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPerfil", DbType.Int16, CodigoPerfil)
            dbBase.AddInParameter(dbCommand, "@p_detEnfermedad", DbType.Int16, detEnfermedad)
            dbBase.AddInParameter(dbCommand, "@p_detVacuna", DbType.Int16, detVacuna)
            dbBase.AddInParameter(dbCommand, "@p_detcaracteristicasPiel", DbType.Int16, detcaracteristicasPiel)
            dbBase.AddInParameter(dbCommand, "@p_detMedicamentos", DbType.Int16, detMedicamentos)
            dbBase.AddInParameter(dbCommand, "@p_detHospitalizacion", DbType.Int16, detHospitalizacion)
            dbBase.AddInParameter(dbCommand, "@p_detOperacion", DbType.Int16, detOperacion)
            dbBase.AddInParameter(dbCommand, "@p_detTipoControl", DbType.Int16, detTipoControl)
            dbBase.AddInParameter(dbCommand, "@p_detAlergia", DbType.Int16, detAlergia)

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

        ''' <summary>
        ''' Inserta los datos en la Ficha medica de alumnos temporal
        ''' </summary>
        ''' <param name="objSolicitud">Datos de la entidad Solicitud de actualizacion de Ficha médica</param>
        ''' <param name="objFichaMedicaAlumno">Datos de la entidad Ficha Medica</param>
        ''' <param name="str_CadenaCodigoPerfil">Codigo de perfil</param>
        ''' <param name="objDetalle">Data set que almacena los datos de los detalles de la Ficha médica de alumno</param>
        ''' <param name="str_Mensaje">Mensaje que se muestra</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Function FUN_INS_FichaMedicasAlumnosTemp(ByVal objSolicitud As be_SolicitudActualizacionFichaMedicaAlumno, _
            ByVal objFichaMedicaAlumno As be_FichaMedica, _
            ByVal str_CadenaCodigoPerfil As String, _
            ByVal objDetalle As DataSet, _
            ByRef str_Mensaje As String, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim int_Valor As String = 0
            Dim int_CodigoSolicitud As Integer = 0

            Try
                'Inicio la transaccion
                BeginTransaction()

                int_CodigoSolicitud = FUN_INS_Solicitud(objSolicitud.CodigoPeronsaSolicitante, _
                    objFichaMedicaAlumno.CodigoAlumno, _
                    str_CadenaCodigoPerfil, _
                    Transaccion, _
                    int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                If int_CodigoSolicitud > 0 Then

                    dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaAlumnos_Temp")

                    dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaAlumno.CodigoAlumno)

                    'Parámetros de entrada
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoNacimiento", DbType.Int16, IIf(objFichaMedicaAlumno.CodigoTipoNacimiento = -1, DBNull.Value, objFichaMedicaAlumno.CodigoTipoNacimiento))
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoSangre", DbType.Int16, IIf(objFichaMedicaAlumno.CodigoTipoSangre = -1, DBNull.Value, objFichaMedicaAlumno.CodigoTipoSangre))
                    dbBase.AddInParameter(dbCommand, "@p_TipoNacimientoObservaciones", DbType.String, IIf(objFichaMedicaAlumno.TipoNacimientoObservaciones Is Nothing, DBNull.Value, objFichaMedicaAlumno.TipoNacimientoObservaciones))
                    dbBase.AddInParameter(dbCommand, "@p_EdadLevantoCabeza", DbType.Int16, IIf(objFichaMedicaAlumno.EdadLevantoCabeza = -1, DBNull.Value, objFichaMedicaAlumno.EdadLevantoCabeza))
                    dbBase.AddInParameter(dbCommand, "@p_MesesLevantoCabeza", DbType.Int16, IIf(objFichaMedicaAlumno.MesesLevantoCabeza = -1, DBNull.Value, objFichaMedicaAlumno.MesesLevantoCabeza))
                    dbBase.AddInParameter(dbCommand, "@p_EdadSento", DbType.Int16, IIf(objFichaMedicaAlumno.EdadSento = -1, DBNull.Value, objFichaMedicaAlumno.EdadSento))
                    dbBase.AddInParameter(dbCommand, "@p_MesesSento", DbType.Int16, IIf(objFichaMedicaAlumno.MesesSento = -1, DBNull.Value, objFichaMedicaAlumno.MesesSento))
                    dbBase.AddInParameter(dbCommand, "@p_EdadParo", DbType.Int16, IIf(objFichaMedicaAlumno.EdadParo = -1, DBNull.Value, objFichaMedicaAlumno.EdadParo))
                    dbBase.AddInParameter(dbCommand, "@p_MesesParo", DbType.Int16, IIf(objFichaMedicaAlumno.MesesParo = -1, DBNull.Value, objFichaMedicaAlumno.MesesParo))
                    dbBase.AddInParameter(dbCommand, "@p_EdadCamino", DbType.Int16, IIf(objFichaMedicaAlumno.EdadCamino = -1, DBNull.Value, objFichaMedicaAlumno.EdadCamino))
                    dbBase.AddInParameter(dbCommand, "@p_MesesCamino", DbType.Int16, IIf(objFichaMedicaAlumno.MesesCamino = -1, DBNull.Value, objFichaMedicaAlumno.MesesCamino))
                    dbBase.AddInParameter(dbCommand, "@p_EdadControloEsfinteres", DbType.Int16, IIf(objFichaMedicaAlumno.EdadControloEsfinteres = -1, DBNull.Value, objFichaMedicaAlumno.EdadControloEsfinteres))
                    dbBase.AddInParameter(dbCommand, "@p_MesesControloEsfinteres", DbType.Int16, IIf(objFichaMedicaAlumno.MesesControloEsfinteres = -1, DBNull.Value, objFichaMedicaAlumno.MesesControloEsfinteres))
                    dbBase.AddInParameter(dbCommand, "@p_EdadHabloPrimerasPalabras", DbType.Int16, IIf(objFichaMedicaAlumno.EdadHabloPrimerasPalabras = -1, DBNull.Value, objFichaMedicaAlumno.EdadHabloPrimerasPalabras))
                    dbBase.AddInParameter(dbCommand, "@p_MesesHabloPrimerasPalabras", DbType.Int16, IIf(objFichaMedicaAlumno.MesesHabloPrimerasPalabras = -1, DBNull.Value, objFichaMedicaAlumno.MesesHabloPrimerasPalabras))
                    dbBase.AddInParameter(dbCommand, "@p_EdadHabloFluidez", DbType.Int16, IIf(objFichaMedicaAlumno.EdadHabloFluidez = -1, DBNull.Value, objFichaMedicaAlumno.EdadHabloFluidez))
                    dbBase.AddInParameter(dbCommand, "@p_MesesHabloFluidez", DbType.Int16, IIf(objFichaMedicaAlumno.MesesHabloFluidez = -1, DBNull.Value, objFichaMedicaAlumno.MesesHabloFluidez))
                    dbBase.AddInParameter(dbCommand, "@p_TabiqueDesviado", DbType.Int16, IIf(objFichaMedicaAlumno.TabiqueDesviado = -1, DBNull.Value, objFichaMedicaAlumno.TabiqueDesviado))
                    dbBase.AddInParameter(dbCommand, "@p_SangradoNasal", DbType.Int16, IIf(objFichaMedicaAlumno.SangradoNasal = -1, DBNull.Value, objFichaMedicaAlumno.SangradoNasal))
                    dbBase.AddInParameter(dbCommand, "@p_UsaLentes", DbType.Int16, IIf(objFichaMedicaAlumno.UsaLentes = -1, DBNull.Value, objFichaMedicaAlumno.UsaLentes))
                    dbBase.AddInParameter(dbCommand, "@p_ObservacionesOftalmologicas", DbType.String, IIf(objFichaMedicaAlumno.ObservacionesOftalmologicas Is Nothing, DBNull.Value, objFichaMedicaAlumno.ObservacionesOftalmologicas))
                    dbBase.AddInParameter(dbCommand, "@p_ObservacionesDental", DbType.String, IIf(objFichaMedicaAlumno.ObservacionesDental Is Nothing, DBNull.Value, objFichaMedicaAlumno.ObservacionesDental))
                    dbBase.AddInParameter(dbCommand, "@p_UsaOrtodoncia", DbType.Int16, IIf(objFichaMedicaAlumno.UsaOrtodoncia = -1, DBNull.Value, objFichaMedicaAlumno.UsaOrtodoncia))

                    dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
                    dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

                    'Parámetros de salida
                    dbBase.AddOutParameter(dbCommand, "@p_Valor", DbType.String, 8)
                    dbBase.AddOutParameter(dbCommand, "@p_Mensaje", DbType.String, 255)

                    'Ejecucion del Store Procedure
                    dbBase.ExecuteScalar(dbCommand, tran)
                    str_Mensaje = dbBase.GetParameterValue(dbCommand, "@p_Mensaje").ToString()
                    int_Valor = CStr(dbBase.GetParameterValue(dbCommand, "@p_Valor"))

                    'Detalle Ficha medica de alumno
                    'DS.Tables(0) : Enfermedad
                    If objDetalle.Tables(0) IsNot Nothing Then
                        If objDetalle.Tables(0).Rows.Count > 0 Then 'Si tiene almenos 1 registro, lo grabo

                            Dim objFichaMedicaEnfermedad As be_RelacionFichaMedicasEnfermedades
                            For Each dr As DataRow In objDetalle.Tables(0).Rows
                                objFichaMedicaEnfermedad = New be_RelacionFichaMedicasEnfermedades
                                objFichaMedicaEnfermedad.CodigoAlumno = objFichaMedicaAlumno.CodigoAlumno
                                objFichaMedicaEnfermedad.CodigoEnfermedad = dr.Item("CodigoEnfermedad")
                                objFichaMedicaEnfermedad.Edad = dr.Item("Edad")
                                FUN_INS_FichaMedicaEnfermedadTemp(int_CodigoSolicitud, objFichaMedicaEnfermedad, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)
                            Next

                        End If
                    End If

                    'DS.Tables(1) : Vacuna
                    If objDetalle.Tables(1) IsNot Nothing Then
                        If objDetalle.Tables(1).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFichaMedicaVacuna As be_RelacionFichaMedicasVacunas

                            For Each dr As DataRow In objDetalle.Tables(1).Rows

                                objFichaMedicaVacuna = New be_RelacionFichaMedicasVacunas
                                objFichaMedicaVacuna.CodigoAlumno = objFichaMedicaAlumno.CodigoAlumno
                                objFichaMedicaVacuna.CodigoVacuna = dr.Item("CodigoVacuna")
                                objFichaMedicaVacuna.CodigoDosis = dr.Item("CodigoDosis")
                                objFichaMedicaVacuna.FechaVacunacion = dr.Item("FechaVacunacion")
                                FUN_INS_FichaMedicaVacunaTemp(int_CodigoSolicitud, objFichaMedicaVacuna, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If


                    'DS.Tables(2) : Alergia
                    If objDetalle.Tables(2) IsNot Nothing Then
                        If objDetalle.Tables(2).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFichaMedicaAlergia As be_RelacionFichaMedicasAlergias

                            For Each dr As DataRow In objDetalle.Tables(2).Rows

                                objFichaMedicaAlergia = New be_RelacionFichaMedicasAlergias
                                objFichaMedicaAlergia.CodigoAlumno = objFichaMedicaAlumno.CodigoAlumno
                                objFichaMedicaAlergia.CodigoAlergia = dr.Item("CodigoAlergia")
                                'objFichaMedicaAlergia.FechaRegistro = dr.Item("FechaRegistro")
                                FUN_INS_FichaMedicaAlergiaTemp(int_CodigoSolicitud, objFichaMedicaAlergia, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If

                    'DS.Tables(3) : Caracteristicas de la piel
                    If objDetalle.Tables(3) IsNot Nothing Then
                        If objDetalle.Tables(3).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFichaMedicaCaracteristicasPiel As be_RelacionFichaMedicasCarecteristicasPiel

                            For Each dr As DataRow In objDetalle.Tables(3).Rows

                                objFichaMedicaCaracteristicasPiel = New be_RelacionFichaMedicasCarecteristicasPiel
                                objFichaMedicaCaracteristicasPiel.CodigoAlumno = objFichaMedicaAlumno.CodigoAlumno
                                objFichaMedicaCaracteristicasPiel.CodigoCaracteristicapiel = dr.Item("CodigoCaracteristicapiel")
                                'objFichaMedicaCaracteristicasPiel.FechaRegistro = dr.Item("FechaRegistro")
                                FUN_INS_FichaMedicaCaracteristicasPielTemp(int_CodigoSolicitud, objFichaMedicaCaracteristicasPiel, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If


                    'DS.Tables(4) : Medicamento
                    If objDetalle.Tables(4) IsNot Nothing Then
                        If objDetalle.Tables(4).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFichaMedicaMedicamento As be_RelacionFichaMedicasMedicamentos

                            For Each dr As DataRow In objDetalle.Tables(4).Rows

                                objFichaMedicaMedicamento = New be_RelacionFichaMedicasMedicamentos
                                objFichaMedicaMedicamento.CodigoAlumno = objFichaMedicaAlumno.CodigoAlumno
                                objFichaMedicaMedicamento.CodigoMedicamento = dr.Item("CodigoMedicamento")
                                objFichaMedicaMedicamento.CodigoPresentacion = dr.Item("CodigoPresentacion")
                                objFichaMedicaMedicamento.CantidadPresentacion = dr.Item("CantidadPresentacion")
                                objFichaMedicaMedicamento.DosisMedicamento = dr.Item("DosisMedicamento")
                                objFichaMedicaMedicamento.Observaciones = dr.Item("Observaciones")
                                'objFichaMedicaMedicamento.FechaRegistro = dr.Item("FechaRegistro")
                                FUN_INS_FichaMedicaMedicamentoTemp(int_CodigoSolicitud, objFichaMedicaMedicamento, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If

                    'DS.Tables(5) : Hospitalizacion
                    If objDetalle.Tables(5) IsNot Nothing Then
                        If objDetalle.Tables(5).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFichaMedicaMotivoHospitalizacion As be_RelacionFichaMedicasMotivoHospitalizacion

                            For Each dr As DataRow In objDetalle.Tables(5).Rows

                                objFichaMedicaMotivoHospitalizacion = New be_RelacionFichaMedicasMotivoHospitalizacion
                                objFichaMedicaMotivoHospitalizacion.CodigoAlumno = objFichaMedicaAlumno.CodigoAlumno
                                objFichaMedicaMotivoHospitalizacion.CodigoMotivoHospitalizacion = dr.Item("CodigoMotivoHospitalizacion")
                                objFichaMedicaMotivoHospitalizacion.FechaHospitalizacion = dr.Item("FechaHospitalizacion")
                                FUN_INS_FichaMedicaHospitalizacionTemp(int_CodigoSolicitud, objFichaMedicaMotivoHospitalizacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If

                    'DS.Tables(6) : Operacion
                    If objDetalle.Tables(6) IsNot Nothing Then
                        If objDetalle.Tables(6).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFichaMedicaOperacion As be_RelacionFichaMedicasOperaciones

                            For Each dr As DataRow In objDetalle.Tables(6).Rows

                                objFichaMedicaOperacion = New be_RelacionFichaMedicasOperaciones
                                objFichaMedicaOperacion.CodigoAlumno = objFichaMedicaAlumno.CodigoAlumno
                                objFichaMedicaOperacion.CodigoTipoOperaciones = dr.Item("CodigoTipoOperaciones")
                                objFichaMedicaOperacion.FechaOperacion = dr.Item("FechaOperacion")
                                FUN_INS_FichaMedicaOperacionTemp(int_CodigoSolicitud, objFichaMedicaOperacion, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If

                    'DS.Tables(7) : Otros Controles 
                    If objDetalle.Tables(7) IsNot Nothing Then
                        If objDetalle.Tables(7).Rows.Count > 0 Then ' Si tiene almenos 1 registro, lo grabo

                            Dim objFichaMedicaTipoControl As be_RelacionFichaMedicasTiposControles

                            For Each dr As DataRow In objDetalle.Tables(7).Rows

                                objFichaMedicaTipoControl = New be_RelacionFichaMedicasTiposControles
                                objFichaMedicaTipoControl.CodigoAlumno = objFichaMedicaAlumno.CodigoAlumno
                                objFichaMedicaTipoControl.CodigoTipoControl = dr.Item("CodigoTipoControl")
                                objFichaMedicaTipoControl.FechaControl = dr.Item("FechaControl")
                                objFichaMedicaTipoControl.Resultado = dr.Item("Resultado")
                                FUN_INS_FichaMedicaTipoControlTemp(int_CodigoSolicitud, objFichaMedicaTipoControl, Transaccion, int_CodigoUsuario, int_CodigoTipoUsuario, int_CodigoModulo, int_CodigoOpcion)

                            Next

                        End If
                    End If

                    Commit()
                    Return int_Valor

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

        ''' <summary>
        ''' Inserta Solicitud de Actualización de Ficha Médica de alumno.
        ''' </summary>
        ''' <param name="int_CodigoPersonaSolicitante">Codigo de persona que solicita la actualizacion de la ficha médica</param>
        ''' <param name="int_CodigoAlumnoActualizar">Codigo de alumno a actualizar</param>
        ''' <param name="str_CadenaCodigoPerfil">Codigo de perfil</param>
        ''' <param name="objSqlTransaction">Cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Private Function FUN_INS_Solicitud(ByVal int_CodigoPersonaSolicitante As Integer, _
            ByVal str_CodigoAlumnoActualizar As String, _
            ByVal str_CadenaCodigoPerfil As String, _
            ByVal objSqlTransaction As SqlTransaction, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As Integer

            Dim str_Mensaje As String = ""
            Dim int_Valor As Integer
            Dim int_CodigoSolicitud As Integer

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_SolicitudActualizacionFichaMedicaAlumno")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoPersonaSolicitante", DbType.Int16, int_CodigoPersonaSolicitante)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumnoActualizar", DbType.String, str_CodigoAlumnoActualizar)
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

        ''' <summary>
        ''' Inserta los datos en el detalle de las enfermedades temporales 
        ''' </summary>
        ''' <param name="int_CodigoSolicitud">Codigo de Solicitud</param>
        ''' <param name="objFichaMedicaEnfermedad">Datos de la entidad Relacion de Ficha Medica de enfermedades temporales </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaEnfermedadTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaMedicaEnfermedad As be_RelacionFichaMedicasEnfermedades, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaEnfermedad_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaEnfermedad.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoEnfermedad", DbType.Int16, objFichaMedicaEnfermedad.CodigoEnfermedad)
            dbBase.AddInParameter(dbCommand, "@p_Edad", DbType.Int16, objFichaMedicaEnfermedad.Edad)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Vacunas temporales.  
        ''' </summary>
        ''' <param name="int_CodigoSolicitud">Codigo de solicitud</param>
        ''' <param name="objFichaMedicaVacuna">Datos de la entidad Relacion de Ficha Medica de Vacuna Temporales </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaVacunaTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaMedicaVacuna As be_RelacionFichaMedicasVacunas, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaVacuna_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaVacuna.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoVacuna", DbType.Int16, objFichaMedicaVacuna.CodigoVacuna)
            dbBase.AddInParameter(dbCommand, "@p_CodigoDosis", DbType.Int16, objFichaMedicaVacuna.CodigoDosis)
            dbBase.AddInParameter(dbCommand, "@p_FechaVacunacion", DbType.Date, objFichaMedicaVacuna.FechaVacunacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Alergias temporales.  
        ''' </summary>
        ''' <param name="int_CodigoSolicitud">Codigo de solicitud</param>
        ''' <param name="objFichaMedicaAlergia">Datos de la entidad Relacion de Ficha Medica de Alergias temporales </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaAlergiaTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaMedicaAlergia As be_RelacionFichaMedicasAlergias, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaAlergia_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaAlergia.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlergia", DbType.Int16, objFichaMedicaAlergia.CodigoAlergia)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Caracteristicas de la piel temporal.  
        ''' </summary>
        ''' <param name="int_CodigoSolicitud">Codigo de solicitud</param>
        ''' <param name="objFichaMedicaCaracteristicasPiel">Datos de la entidad Relacion de Ficha Medica de Caracteristicas de la piel temporal</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaCaracteristicasPielTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaMedicaCaracteristicasPiel As be_RelacionFichaMedicasCarecteristicasPiel, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicaCaracteristicasPiel_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaCaracteristicasPiel.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoCaracteristicapiel", DbType.Int16, objFichaMedicaCaracteristicasPiel.CodigoCaracteristicapiel)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de los medicamentos de la tabla temporal.  
        ''' </summary>
        ''' <param name="int_CodigoSolicitud">Codigo de solicitud</param>
        ''' <param name="objFichaMedicaMedicamento">Datos de la entidad Relacion de Ficha Medica de medicamentos de la tabla temporal </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaMedicamentoTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaMedicaMedicamento As be_RelacionFichaMedicasMedicamentos, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasMedicamentos_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaMedicamento.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMedicamento", DbType.Int16, objFichaMedicaMedicamento.CodigoMedicamento)
            dbBase.AddInParameter(dbCommand, "@p_CodigoPresentacion", DbType.Int16, objFichaMedicaMedicamento.CodigoPresentacion)
            dbBase.AddInParameter(dbCommand, "@p_CantidadPresentacion", DbType.String, objFichaMedicaMedicamento.CantidadPresentacion)
            dbBase.AddInParameter(dbCommand, "@p_DosisMedicamento", DbType.String, objFichaMedicaMedicamento.DosisMedicamento)
            dbBase.AddInParameter(dbCommand, "@p_Observaciones", DbType.String, objFichaMedicaMedicamento.Observaciones)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las hospitalizaciones de la tabla temporal.
        ''' </summary>
        ''' <param name="int_CodigoSolicitud">Codigo de solicitud</param>
        ''' <param name="objFichaMedicaHospitalizacion">Datos de la entidad Relacion de Ficha Medica de Hospitalización de la tabla temporal.</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaHospitalizacionTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaMedicaHospitalizacion As be_RelacionFichaMedicasMotivoHospitalizacion, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasMotivoHospitalizacion_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaHospitalizacion.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoMotivoHospitalizacion", DbType.Int16, objFichaMedicaHospitalizacion.CodigoMotivoHospitalizacion)
            dbBase.AddInParameter(dbCommand, "@p_FechaHospitalizacion", DbType.Date, objFichaMedicaHospitalizacion.FechaHospitalizacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de las Operaciones de la tabla temporal.  
        ''' </summary>
        ''' <param name="int_CodigoSolicitud">Codigo de solicitud</param>
        ''' <param name="objFichaMedicaOperacion">Datos de la entidad Relacion de Ficha Medica de Operación de la tabla temporal. </param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaOperacionTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaMedicaOperacion As be_RelacionFichaMedicasOperaciones, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasOperaciones_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaOperacion.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoOperaciones", DbType.Int16, objFichaMedicaOperacion.CodigoTipoOperaciones)
            dbBase.AddInParameter(dbCommand, "@p_FechaOperacion", DbType.Date, objFichaMedicaOperacion.FechaOperacion)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

        ''' <summary>
        ''' Inserta los datos en el detalle de los tipos de control de la tabla temporal.
        ''' </summary>
        ''' <param name="int_CodigoSolicitud">Codigo de solicitud</param>
        ''' <param name="objFichaMedicaTipoControl">Datos de la entidad Relacion de Ficha Medica de Tipos de control de la tabla temporal.</param>
        ''' <param name="objSqlTransaction">cadena de conexion</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Sub FUN_INS_FichaMedicaTipoControlTemp(ByVal int_CodigoSolicitud As Integer, ByVal objFichaMedicaTipoControl As be_RelacionFichaMedicasTiposControles, ByVal objSqlTransaction As SqlTransaction, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer)

            dbCommand = Me.dbBase.GetStoredProcCommand("EN_USP_INS_FichaMedicasTiposControles_Temp")

            'Parámetros de entrada
            dbBase.AddInParameter(dbCommand, "@p_CodigoSolicitud", DbType.Int16, int_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@p_CodigoAlumno", DbType.String, objFichaMedicaTipoControl.CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoControl", DbType.Int16, objFichaMedicaTipoControl.CodigoTipoControl)
            dbBase.AddInParameter(dbCommand, "@p_FechaControl", DbType.Date, objFichaMedicaTipoControl.FechaControl)
            dbBase.AddInParameter(dbCommand, "@p_Resultado", DbType.String, objFichaMedicaTipoControl.Resultado)

            dbBase.AddInParameter(dbCommand, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(dbCommand, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(dbCommand, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            dbBase.ExecuteNonQuery(dbCommand, objSqlTransaction)

        End Sub

#End Region

#Region "Metodos No Transaccionales"

        'Ficha Médica de Alumno (Módulo Enfermería)      
        ''' <summary>
        ''' Lista los datos de la Ficha Medica del Alumno
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
        Public Function FUN_LIS_FichaMedicaAlumno(ByVal str_ApellidoPaterno As String, ByVal str_ApellidoMaterno As String, ByVal str_Nombre As String, ByVal int_estadoAlumno As Integer, ByVal int_Nivel As Integer, ByVal int_SubNivel As Integer, ByVal int_Grado As Integer, ByVal int_Aula As Integer, ByVal int_PeriodoInicio As Integer, ByVal int_PeriodoFin As Integer, ByVal int_Sede As Integer, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_FichaMedicaAlumno")
            'Parámetros de entrada
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

        ''' <summary>
        ''' Obtiene los datos de la Ficha Médica del alumno
        ''' </summary>
        ''' <param name="int_Codigo">Codigo del alumno</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Function FUN_GET_FichaMedicaAlumno(ByVal str_Codigo As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_GET_FichaMedicasAlumnos")
            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.String, str_Codigo)
            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int16, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int16, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int16, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int16, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Validacion de Datos (Módulo Enfermería)
        ''' <summary>
        ''' Obtiene los datos de la solicitud de actualizacion de Ficha Médica del alumno
        ''' </summary>
        ''' <param name="int_Codigo">Codigo del alumno</param>
        ''' <param name="int_CodigoSolicitud">Codigo de la solicitud</param>
        ''' <param name="int_CodigoPerfil">Codigo del perfil</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Function FUN_GET_FichaMedicaActualizacion(ByVal int_Codigo As Integer, _
            ByVal int_CodigoSolicitud As Integer, _
            ByVal int_CodigoPerfil As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_GET_FichaMedicaAlumnoActualizacionPorSoliciutd")

            'Parámetros de entrada
            dbBase.AddInParameter(cmd, "@p_Codigo", DbType.Int32, int_Codigo)
            dbBase.AddInParameter(cmd, "@p_CodigoSolicitud", DbType.Int32, int_CodigoSolicitud)
            dbBase.AddInParameter(cmd, "@p_CodigoPerfil", DbType.Int32, int_CodigoPerfil)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)
            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        ''' <summary>
        ''' Lista los datos de la solicitud de actualizacion de Ficha Médica del alumno
        ''' </summary>
        ''' <param name="objMaestroPersona">Contiene los datos de la entidad Maestro Persona</param>
        ''' <param name="dt_FechaRangoInicial">Fecha en que se inicia la busqueda de la ficha médica</param>
        ''' <param name="dt_FechaRangoFinal">Fecha en que se finaliza la busqueda de la ficha médica</param>
        ''' <param name="int_CodigoPerfil">Codigo del perfil</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Function FUN_LIS_FichaMedicaActualizacion(ByVal objMaestroPersona As be_MaestroPersonas, _
            ByVal dt_FechaRangoInicial As Date, _
            ByVal dt_FechaRangoFinal As Date, _
            ByVal int_CodigoPerfil As Integer, _
            ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet

            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_LIS_FichaMedicaAlumnoActualizacion")

            'Parámetros de entrada        
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarApellidoPaterno", DbType.String, objMaestroPersona.AlumnoFamiliarApellidoPaterno)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarApellidoMaterno", DbType.String, objMaestroPersona.AlumnoFamiliarApellidoMaterno)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarNombres", DbType.String, objMaestroPersona.AlumnoFamiliarNombres)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarNivel", DbType.Int16, objMaestroPersona.AlumnoFamiliarNivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarSubnivel", DbType.Int16, objMaestroPersona.AlumnoFamiliarSubnivel)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarGrado", DbType.Int16, objMaestroPersona.AlumnoFamiliarGrado)
            dbBase.AddInParameter(cmd, "@p_AlumnoFamiliarAula", DbType.Int16, objMaestroPersona.AlumnoFamiliarAula)
            dbBase.AddInParameter(cmd, "@p_Estado", DbType.Int16, objMaestroPersona.EstadoPersona)
            dbBase.AddInParameter(cmd, "@p_FechaRangoInicial", DbType.DateTime, dt_FechaRangoInicial)
            dbBase.AddInParameter(cmd, "@p_FechaRangoFinal", DbType.DateTime, dt_FechaRangoFinal)
            dbBase.AddInParameter(cmd, "@p_CodigoPerfil", DbType.Int16, int_CodigoPerfil)

            dbBase.AddInParameter(cmd, "@p_CodigoUsuario", DbType.Int32, int_CodigoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoTipoUsuario", DbType.Int32, int_CodigoTipoUsuario)
            dbBase.AddInParameter(cmd, "@p_CodigoModulo", DbType.Int32, int_CodigoModulo)
            dbBase.AddInParameter(cmd, "@p_CodigoOpcion", DbType.Int32, int_CodigoOpcion)

            'Ejecucion del Store Procedure
            Return dbBase.ExecuteDataSet(cmd)
        End Function

        'Datos para visualizacion de la Ficha Médica de Alumno (Interfaz Familia - Módulo Solicitudes Actualización Información)
        ''' <summary>
        ''' Obtiene los datos para la visualizacion del Familiar
        ''' </summary>
        ''' <param name="str_CodigoAlumno">Codigo de Alumno</param>
        ''' <remarks>
        ''' Creador:               Fanny Salinas 
        ''' Fecha de Creación:     07/01/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        Public Function FUN_GET_FichaMedicaAlumnoVisualizacionActualizacionFamiliar(ByVal str_CodigoAlumno As String, ByVal int_CodigoUsuario As Integer, ByVal int_CodigoTipoUsuario As Integer, ByVal int_CodigoModulo As Integer, ByVal int_CodigoOpcion As Integer) As DataSet
            Dim cmd As DbCommand = dbBase.GetStoredProcCommand("EN_USP_GET_FichaMedicasAlumnosDatosSolicitudesFamiliar")

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

