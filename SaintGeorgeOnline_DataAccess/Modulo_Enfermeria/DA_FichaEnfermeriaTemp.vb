Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloEnfermeria
Imports SaintGeorgeOnline_BusinessEntities

Public Class DA_FichaEnfermeriaTemp
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

    Public Function F_ActualizarEnfermeriaTmpEstado(ByVal oBE_FichaEnfermeriaTemp As BE_FichaEnfermeriaTemp, ByVal oBE_FichaEnfermeria As BE_FichaEnfermeria, _
                            ByVal lstBE_EN_RelacionFichaMedicasAlergias As List(Of BE_EN_RelacionFichaMedicasAlergias), _
                            ByVal lstBE_EN_RelacionFichaMedicasAlergias_Temp As List(Of BE_EN_RelacionFichaMedicasAlergias_Temp), _
                            ByVal lstBE_EN_RelacionFichaMedicasCarecteristicasPiel As List(Of BE_EN_RelacionFichaMedicasCarecteristicasPiel), _
                            ByVal lstBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp As List(Of BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp), _
                            ByVal lstBE_EN_RelacionFichaMedicasEnfermedades_Temp As List(Of BE_EN_RelacionFichaMedicasEnfermedades_Temp), _
                            ByVal lstBE_EN_RelacionFichaMedicasEnfermedades As List(Of BE_EN_RelacionFichaMedicasEnfermedades), _
                            ByVal lstBE_EN_RelacionFichaMedicasMedicamentos As List(Of BE_EN_RelacionFichaMedicasMedicamentos), _
                            ByVal lstBE_EN_RelacionFichaMedicasMedicamentos_Temp As List(Of BE_EN_RelacionFichaMedicasMedicamentos_Temp), _
                            ByVal lstBE_EN_RelacionFichaMedicasMotivoHospitalizacion As List(Of BE_EN_RelacionFichaMedicasMotivoHospitalizacion), _
                            ByVal lstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp As List(Of BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp), _
                            ByVal lstBE_EN_RelacionFichaMedicasOperaciones As List(Of BE_EN_RelacionFichaMedicasOperaciones), _
                            ByVal lstBE_EN_RelacionFichaMedicasOperaciones_Temp As List(Of BE_EN_RelacionFichaMedicasOperaciones_Temp), _
                            ByVal lstBE_EN_RelacionFichaMedicasTiposControles As List(Of BE_EN_RelacionFichaMedicasTiposControles), _
                            ByVal lstBE_EN_RelacionFichaMedicasTiposControles_Temp As List(Of BE_EN_RelacionFichaMedicasTiposControles_Temp), _
                            ByVal lstBE_EN_RelacionFichaMedicasVacunas As List(Of BE_EN_RelacionFichaMedicasVacunas), _
                            ByVal lstBE_EN_RelacionFichaMedicasVacunas_Temp As List(Of BE_EN_RelacionFichaMedicasVacunas_Temp)) As Integer

        Try
            'Inicio la transaccion

            Dim lstIdsGenerados As New List(Of Integer)
            BeginTransaction()
            dbCommand = Me.dbBase.GetStoredProcCommand("USP_UDP_EN_FichaMedicasAlumnos_Temp")
            'Datos Generales de la cabecera
            dbBase.AddInParameter(dbCommand, "@SAFM_CodigoSolicitud", DbType.Int32, oBE_FichaEnfermeriaTemp.SAFM_CodigoSolicitud)
            dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeriaTemp.AL_CodigoAlumno)

            dbBase.AddInParameter(dbCommand, "@TN_CodigoTipoNacimiento_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.TN_CodigoTipoNacimiento_Check)
            dbBase.AddInParameter(dbCommand, "@TSA_CodigoTipoSangre_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.TSA_CodigoTipoSangre_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_TipoNacimientoObservaciones_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_TipoNacimientoObservaciones_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_EdadLevantoCabeza_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_EdadLevantoCabeza_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_MesesLevantoCabeza_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_MesesLevantoCabeza_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_EdadSento_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_EdadSento_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_MesesSento_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_MesesSento_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_EdadParo_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_EdadParo_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_MesesParo_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_MesesParo_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_EdadCamino_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_EdadCamino_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_MesesCamino_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_MesesCamino_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_EdadControloEsfinteres_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_EdadControloEsfinteres_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_MesesControloEsfinteres_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_MesesControloEsfinteres_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_EdadHabloPrimerasPalabras_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_EdadHabloPrimerasPalabras_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_MesesHabloPrimerasPalabras_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_MesesHabloPrimerasPalabras_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_EdadHabloFluidez_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_EdadHabloFluidez_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_MesesHabloFluidez_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_MesesHabloFluidez_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_TabiqueDesviado_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_TabiqueDesviado_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_SangradoNasal_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_SangradoNasal_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_UsaLentes_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_UsaLentes_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_ObservacionesOftalmologicas_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_ObservacionesOftalmologicas_Check)
            dbBase.AddInParameter(dbCommand, "@FMA_UsaOrtodoncia_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_UsaOrtodoncia_Check)

            dbBase.AddInParameter(dbCommand, "@FMA_ObservacionesDental_Check", DbType.Boolean, oBE_FichaEnfermeriaTemp.FMA_ObservacionesDental_Check)

            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)

            dbBase.ExecuteScalar(dbCommand, tran)
            Dim str_Mensaje As String = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
            Dim int_Valor As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_FichaMedicasAlumnos")
            dbCommand.Parameters.Clear()
            lstIdsGenerados.Add(int_Valor)
            dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
            dbBase.AddInParameter(dbCommand, "@TN_CodigoTipoNacimiento", DbType.Int32, oBE_FichaEnfermeria.TN_CodigoTipoNacimiento)

            dbBase.AddInParameter(dbCommand, "@TSA_CodigoTipoSangre", DbType.Int32, oBE_FichaEnfermeria.TSA_CodigoTipoSangre)
            dbBase.AddInParameter(dbCommand, "@FMA_TipoNacimientoObservaciones", DbType.String, oBE_FichaEnfermeria.FMA_TipoNacimientoObservaciones)

            dbBase.AddInParameter(dbCommand, "@FMA_EdadLevantoCabeza", DbType.Int32, oBE_FichaEnfermeria.FMA_EdadLevantoCabeza)
            dbBase.AddInParameter(dbCommand, "@FMA_MesesLevantoCabeza", DbType.Int32, oBE_FichaEnfermeria.FMA_MesesLevantoCabeza)

            dbBase.AddInParameter(dbCommand, "@FMA_EdadSento", DbType.Int32, oBE_FichaEnfermeria.FMA_EdadSento)
            dbBase.AddInParameter(dbCommand, "@FMA_MesesSento", DbType.Int32, oBE_FichaEnfermeria.FMA_MesesSento)

            dbBase.AddInParameter(dbCommand, "@FMA_EdadParo", DbType.Int32, oBE_FichaEnfermeria.FMA_EdadParo)
            dbBase.AddInParameter(dbCommand, "@FMA_MesesParo", DbType.Int32, oBE_FichaEnfermeria.FMA_MesesParo)

            dbBase.AddInParameter(dbCommand, "@FMA_EdadCamino", DbType.Int32, oBE_FichaEnfermeria.FMA_EdadCamino)
            dbBase.AddInParameter(dbCommand, "@FMA_MesesCamino", DbType.Int32, oBE_FichaEnfermeria.FMA_MesesCamino)

            dbBase.AddInParameter(dbCommand, "@FMA_EdadControloEsfinteres", DbType.Int32, oBE_FichaEnfermeria.FMA_EdadControloEsfinteres)
            dbBase.AddInParameter(dbCommand, "@FMA_MesesControloEsfinteres", DbType.Int32, oBE_FichaEnfermeria.FMA_MesesControloEsfinteres)

            dbBase.AddInParameter(dbCommand, "@FMA_EdadHabloPrimerasPalabras", DbType.Int32, oBE_FichaEnfermeria.FMA_EdadHabloPrimerasPalabras)
            dbBase.AddInParameter(dbCommand, "@FMA_MesesHabloPrimerasPalabras", DbType.Int32, oBE_FichaEnfermeria.FMA_MesesHabloPrimerasPalabras)

            dbBase.AddInParameter(dbCommand, "@FMA_EdadHabloFluidez", DbType.Int32, oBE_FichaEnfermeria.FMA_EdadHabloFluidez)
            dbBase.AddInParameter(dbCommand, "@FMA_MesesHabloFluidez", DbType.Int32, oBE_FichaEnfermeria.FMA_MesesHabloFluidez)

            dbBase.AddInParameter(dbCommand, "@FMA_TabiqueDesviado", DbType.Boolean, IIf(oBE_FichaEnfermeria.FMA_TabiqueDesviado Is Nothing, DBNull.Value, oBE_FichaEnfermeria.FMA_TabiqueDesviado))
            dbBase.AddInParameter(dbCommand, "@FMA_SangradoNasal", DbType.Boolean, IIf(oBE_FichaEnfermeria.FMA_SangradoNasal Is Nothing, DBNull.Value, oBE_FichaEnfermeria.FMA_SangradoNasal))


            dbBase.AddInParameter(dbCommand, "@FMA_UsaLentes", DbType.Boolean, IIf(oBE_FichaEnfermeria.FMA_UsaLentes Is Nothing, DBNull.Value, oBE_FichaEnfermeria.FMA_UsaLentes))
            dbBase.AddInParameter(dbCommand, "@FMA_ObservacionesOftalmologicas", DbType.String, oBE_FichaEnfermeria.FMA_ObservacionesOftalmologicas)



            dbBase.AddInParameter(dbCommand, "@FMA_UsaOrtodoncia", DbType.Boolean, IIf(oBE_FichaEnfermeria.FMA_UsaOrtodoncia Is Nothing, DBNull.Value, oBE_FichaEnfermeria.FMA_UsaOrtodoncia))
            dbBase.AddInParameter(dbCommand, "@FMA_ObservacionesDental", DbType.String, oBE_FichaEnfermeria.FMA_ObservacionesDental)

            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 255)
            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
            ''

            dbBase.ExecuteScalar(dbCommand, tran)

            ''


            Dim str_Mensaje1 As String = dbBase.GetParameterValue(dbCommand, "@mensaje").ToString()
            Dim int_Valor1 As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
            lstIdsGenerados.Add(int_Valor1)

            ''--------------------------------------------------------------------------------------
            For Each oBE_EN_RelacionFichaMedicasAlergias_temp As BE_EN_RelacionFichaMedicasAlergias_Temp In lstBE_EN_RelacionFichaMedicasAlergias_Temp

                If Not oBE_EN_RelacionFichaMedicasAlergias_temp.RFAG_Check Then
                    Continue For
                End If
               
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_RelacionFichaMedicasAlergias_Temp")
                dbBase.AddInParameter(dbCommand, "@RFAG_CodigoRelFichaMedAlergias", DbType.Int32, oBE_EN_RelacionFichaMedicasAlergias_temp.RFAG_CodigoRelFichaMedAlergias)
                dbBase.AddInParameter(dbCommand, "@RFAG_Check", DbType.Boolean, oBE_EN_RelacionFichaMedicasAlergias_temp.RFAG_Check)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorA As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorA)
            Next


            For Each oBE_EN_RelacionFichaMedicasAlergias As BE_EN_RelacionFichaMedicasAlergias In lstBE_EN_RelacionFichaMedicasAlergias
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_INS_EN_RelacionFichaMedicasAlergias")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@RFAG_CodigoRelFichaMedAlergias", DbType.Int32, oBE_EN_RelacionFichaMedicasAlergias.RFAG_CodigoRelFichaMedAlergias)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@AG_CodigoAlergia", DbType.Int32, oBE_EN_RelacionFichaMedicasAlergias.AG_CodigoAlergia)
                dbBase.AddInParameter(dbCommand, "@RFAG_FechaRegistro", DbType.DateTime, oBE_EN_RelacionFichaMedicasAlergias.RFAG_FechaRegistro)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorB As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorB)
            Next
            ''--------------------------------------------------------------------------------------


            ''--------------------------------------------------------------------------------------

            For Each oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp As BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp In lstBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp
                If Not oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp.RFCP_Check Then
                    Continue For
                End If
                
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_RelacionFichaMedicasCarecteristicasPiel_Temp")
                dbBase.AddInParameter(dbCommand, "@RFCP_CodigoRelFichaMedCaractPiel", DbType.Int32, oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp.RFCP_CodigoRelFichaMedCaractPiel)
                dbBase.AddInParameter(dbCommand, "@RFCP_Check", DbType.Boolean, oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp.RFCP_Check)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorD As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorD)
            Next

            For Each oBE_EN_RelacionFichaMedicasCarecteristicasPiel As BE_EN_RelacionFichaMedicasCarecteristicasPiel In lstBE_EN_RelacionFichaMedicasCarecteristicasPiel
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_INS_EN_RelacionFichaMedicasCarecteristicasPiel")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@RFCP_CodigoRelFichaMedCaractPiel", DbType.Int32, oBE_EN_RelacionFichaMedicasCarecteristicasPiel.RFCP_CodigoRelFichaMedCaractPiel)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@TCP_CodigoCaracteristicapiel", DbType.Int32, oBE_EN_RelacionFichaMedicasCarecteristicasPiel.TCP_CodigoCaracteristicapiel)
                dbBase.AddInParameter(dbCommand, "@RFCP_FechaRegistro", DbType.DateTime, oBE_EN_RelacionFichaMedicasCarecteristicasPiel.RFCP_FechaRegistro)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorC As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorC)
            Next

          
            ''--------------------------------------------------------------------------------------
            ''

            For Each oBE_EN_RelacionFichaMedicasMedicamentos_Temp As BE_EN_RelacionFichaMedicasMedicamentos_Temp In lstBE_EN_RelacionFichaMedicasMedicamentos_Temp
                If Not oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_Check Then
                    Continue For
                End If
                
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_RelacionFichaMedicasMedicamentos_Temp")
                dbBase.AddInParameter(dbCommand, "@RFMD_CodigoRelFichaMedMedicamentos", DbType.Int32, oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_CodigoRelFichaMedMedicamentos)
                dbBase.AddInParameter(dbCommand, "@RFMD_Check", DbType.Boolean, oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_Check)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorF As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorF)
            Next
            For Each oBE_EN_RelacionFichaMedicasMedicamentos As BE_EN_RelacionFichaMedicasMedicamentos In lstBE_EN_RelacionFichaMedicasMedicamentos
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_INS_EN_RelacionFichaMedicasMedicamentos")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@RFMD_CodigoRelFichaMedMedicamentos", DbType.Int32, oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_CodigoRelFichaMedMedicamentos)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)

                dbBase.AddInParameter(dbCommand, "@MA_CodigoMedicamento", DbType.Int32, oBE_EN_RelacionFichaMedicasMedicamentos.MA_CodigoMedicamento)
                dbBase.AddInParameter(dbCommand, "@RFMD_FechaRegistro", DbType.DateTime, oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_FechaRegistro)

                dbBase.AddInParameter(dbCommand, "@PM_CodigoPresentacion", DbType.Int32, oBE_EN_RelacionFichaMedicasMedicamentos.PM_CodigoPresentacion)
                dbBase.AddInParameter(dbCommand, "@RFMD_CantidadPresentacion", DbType.String, oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_CantidadPresentacion)
                dbBase.AddInParameter(dbCommand, "@RFMD_DosisMedicamento", DbType.String, oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_DosisMedicamento)

                dbBase.AddInParameter(dbCommand, "@RFMD_Observaciones", DbType.String, oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_Observaciones)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorE As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorE)
            Next


            
            ''--------------------------------------------------------------------------------------
            For Each oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp As BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp In lstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp
                If Not oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp.RFMH_Check Then
                    Continue For
                End If

                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp")
                dbBase.AddInParameter(dbCommand, "@RFMH_CodigoRelFichaMedMotivoHosp", DbType.Int32, oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp.RFMH_CodigoRelFichaMedMotivoHosp)
                dbBase.AddInParameter(dbCommand, "@RFMH_Check", DbType.Boolean, oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp.RFMH_Check)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorH As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorH)
            Next
            For Each oBE_EN_RelacionFichaMedicasMotivoHospitalizacion As BE_EN_RelacionFichaMedicasMotivoHospitalizacion In lstBE_EN_RelacionFichaMedicasMotivoHospitalizacion
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_INS_EN_RelacionFichaMedicasMotivoHospitalizacion")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@RFMH_CodigoRelFichaMedMotivoHosp", DbType.Int32, oBE_EN_RelacionFichaMedicasMotivoHospitalizacion.RFMH_CodigoRelFichaMedMotivoHosp)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@MH_CodigoMotivoHospitalizacion", DbType.Int32, oBE_EN_RelacionFichaMedicasMotivoHospitalizacion.MH_CodigoMotivoHospitalizacion)
                dbBase.AddInParameter(dbCommand, "@RFMH_FechaHospitalizacion", DbType.DateTime, oBE_EN_RelacionFichaMedicasMotivoHospitalizacion.RFMH_FechaHospitalizacion)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorG As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorG)
            Next


           
            ''--------------------------------------------------------------------------------------

            For Each oBE_EN_RelacionFichaMedicasOperaciones_Temp As BE_EN_RelacionFichaMedicasOperaciones_Temp In lstBE_EN_RelacionFichaMedicasOperaciones_Temp
                If Not oBE_EN_RelacionFichaMedicasOperaciones_Temp.RFOM_Check Then
                    Continue For
                End If

                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_RelacionFichaMedicasOperaciones_Temp")
                dbBase.AddInParameter(dbCommand, "@RFOM_CodigoRelFichaMedOperaciones", DbType.Int32, oBE_EN_RelacionFichaMedicasOperaciones_Temp.RFOM_CodigoRelFichaMedOperaciones)
                dbBase.AddInParameter(dbCommand, "@RFOM_Check", DbType.Boolean, oBE_EN_RelacionFichaMedicasOperaciones_Temp.RFOM_Check)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorJ As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorJ)
            Next
            For Each oBE_EN_RelacionFichaMedicasOperaciones As BE_EN_RelacionFichaMedicasOperaciones In lstBE_EN_RelacionFichaMedicasOperaciones
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_INS_EN_RelacionFichaMedicasOperaciones")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@RFOM_CodigoRelFichaMedOperaciones", DbType.Int32, oBE_EN_RelacionFichaMedicasOperaciones.RFOM_CodigoRelFichaMedOperaciones)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddInParameter(dbCommand, "@TOM_CodigoTipoOperaciones", DbType.Int32, oBE_EN_RelacionFichaMedicasOperaciones.TOM_CodigoTipoOperaciones)
                dbBase.AddInParameter(dbCommand, "@RFOM_FechaOperacion", DbType.DateTime, oBE_EN_RelacionFichaMedicasOperaciones.RFOM_FechaOperacion)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorI As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorI)
            Next
           
            ''--------------------------------------------------------------------------------------

            For Each oBE_EN_RelacionFichaMedicasTiposControles_Temp As BE_EN_RelacionFichaMedicasTiposControles_Temp In lstBE_EN_RelacionFichaMedicasTiposControles_Temp
                If Not oBE_EN_RelacionFichaMedicasTiposControles_Temp.RFTC_Check Then
                    Continue For
                End If

                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_RelacionFichaMedicasTiposControles_Temp")
                dbBase.AddInParameter(dbCommand, "@RFTC_CodigoRelFichaMedTiposControles", DbType.Int32, oBE_EN_RelacionFichaMedicasTiposControles_Temp.RFTC_CodigoRelFichaMedTiposControles)
                dbBase.AddInParameter(dbCommand, "@RFTC_Check", DbType.Boolean, oBE_EN_RelacionFichaMedicasTiposControles_Temp.RFTC_Check)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorL As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorL)
            Next

            For Each oBE_EN_RelacionFichaMedicasTiposControles As BE_EN_RelacionFichaMedicasTiposControles In lstBE_EN_RelacionFichaMedicasTiposControles

                dbCommand = Me.dbBase.GetStoredProcCommand("USP_INS_EN_RelacionFichaMedicasTiposControles")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@RFTC_CodigoRelFichaMedTiposControles", DbType.Int32, oBE_EN_RelacionFichaMedicasTiposControles.RFTC_CodigoRelFichaMedTiposControles)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)

                dbBase.AddInParameter(dbCommand, "@TC_CodigoTipoControl", DbType.Int32, oBE_EN_RelacionFichaMedicasTiposControles.TC_CodigoTipoControl)
                dbBase.AddInParameter(dbCommand, "@RFTC_FechaControl", DbType.DateTime, oBE_EN_RelacionFichaMedicasTiposControles.RFTC_FechaControl)

                dbBase.AddInParameter(dbCommand, "@RFTC_Resultado", DbType.String, oBE_EN_RelacionFichaMedicasTiposControles.RFTC_Resultado)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorK As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorK)

            Next

           
            ''--------------------------------------------------------------------------------------

            For Each oBE_EN_RelacionFichaMedicasVacunas_Temp As BE_EN_RelacionFichaMedicasVacunas_Temp In lstBE_EN_RelacionFichaMedicasVacunas_Temp
                If Not oBE_EN_RelacionFichaMedicasVacunas_Temp.RFVC_Check Then
                    Continue For
                End If
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_RelacionFichaMedicasVacunas_Temp")
                dbBase.AddInParameter(dbCommand, "@RFVC_CodigoRelVacunasFichaMed", DbType.Int32, IIf(oBE_EN_RelacionFichaMedicasVacunas_Temp.RFVC_CodigoRelVacunasFichaMed = 0, DBNull.Value, oBE_EN_RelacionFichaMedicasVacunas_Temp.RFVC_CodigoRelVacunasFichaMed))
                dbBase.AddInParameter(dbCommand, "@RFVC_Check", DbType.Boolean, oBE_EN_RelacionFichaMedicasVacunas_Temp.RFVC_Check)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorN As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorN)
            Next
            For Each oBE_EN_RelacionFichaMedicasVacunas As BE_EN_RelacionFichaMedicasVacunas In lstBE_EN_RelacionFichaMedicasVacunas

                dbCommand = Me.dbBase.GetStoredProcCommand("USP_INS_EN_RelacionFichaMedicasVacunas")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@RFVC_CodigoRelVacunasFichaMed", DbType.Int32, oBE_EN_RelacionFichaMedicasVacunas.RFVC_CodigoRelVacunasFichaMed)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)

                dbBase.AddInParameter(dbCommand, "@VC_CodigoVacuna", DbType.Int32, oBE_EN_RelacionFichaMedicasVacunas.VC_CodigoVacuna)
                dbBase.AddInParameter(dbCommand, "@DV_CodigoDosis", DbType.Int32, oBE_EN_RelacionFichaMedicasVacunas.DV_CodigoDosis)



                dbBase.AddInParameter(dbCommand, "@RFVC_FechaVacunacion", DbType.DateTime, oBE_EN_RelacionFichaMedicasVacunas.RFVC_FechaVacunacion)
                dbBase.AddInParameter(dbCommand, "@RFVC_Edad", DbType.Int32, oBE_EN_RelacionFichaMedicasVacunas.RFVC_Edad)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorM As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorM)
            Next

            

            '--------------------------------------------------------------------------------------------------
            For Each oBE_EN_RelacionFichaMedicasEnfermedades_Temp As BE_EN_RelacionFichaMedicasEnfermedades_Temp In lstBE_EN_RelacionFichaMedicasEnfermedades_Temp
                If Not oBE_EN_RelacionFichaMedicasEnfermedades_Temp.RFEF_Check Then
                    Continue For
                End If
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_RelacionFichaMedicasEnfermedades_Temp")
                dbBase.AddInParameter(dbCommand, "@RFEF_CodigoRelFichaMedEnEnfermedades", DbType.Int32, oBE_EN_RelacionFichaMedicasEnfermedades_Temp.RFEF_CodigoRelFichaMedEnEnfermedades)
                dbBase.AddInParameter(dbCommand, "@RFEF_Check", DbType.Boolean, oBE_EN_RelacionFichaMedicasEnfermedades_Temp.RFEF_Check)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)
                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorO As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorO)
            Next

            For Each oBE_EN_RelacionFichaMedicasEnfermedades As BE_EN_RelacionFichaMedicasEnfermedades In lstBE_EN_RelacionFichaMedicasEnfermedades
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_INS_EN_RelacionFichaMedicasEnfermedades")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@RFEF_CodigoRelFichaMedEnEnfermedades", DbType.Int32, oBE_EN_RelacionFichaMedicasEnfermedades.RFEF_CodigoRelFichaMedEnEnfermedades)
                dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.String, oBE_FichaEnfermeria.AL_CodigoAlumno)

                dbBase.AddInParameter(dbCommand, "@EF_CodigoEnfermedad", DbType.Int32, oBE_EN_RelacionFichaMedicasEnfermedades.EF_CodigoEnfermedad)
                dbBase.AddInParameter(dbCommand, "@RFEF_Edad", DbType.Int32, oBE_EN_RelacionFichaMedicasEnfermedades.RFEF_Edad)

                dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)



                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
                Dim int_ValorP As Integer = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                lstIdsGenerados.Add(int_ValorP)

            Next



            If Not lstIdsGenerados.Contains(0) Then
                dbCommand = Me.dbBase.GetStoredProcCommand("USP_UPD_EN_EstadoSolicitudActualizacionEnfermeria")
                dbCommand.Parameters.Clear()
                dbBase.AddInParameter(dbCommand, "@SAFM_CodigoSolicitud", DbType.Int32, oBE_FichaEnfermeriaTemp.SAFM_CodigoSolicitud)
                dbBase.ExecuteScalar(lPadCommand(dbCommand), tran)
            End If

            Dim indicadorOPeracion As Integer = 0
           

            'oBE_FichaEnfermeriaTemp.SAFM_CodigoSolicitud

            ''------------------------------------------------------------------------------------------
            If lstIdsGenerados.Contains(0) Then
                Rollback()
                indicadorOPeracion = 0
            Else
                Commit()
                indicadorOPeracion = 1

            End If


            Return indicadorOPeracion
        Catch ex As Exception
            Rollback()
            Return 0
        End Try
    End Function
    Function lPadCommand(ByRef ocom As DbCommand) As DbCommand

        For Each op As DbParameter In ocom.Parameters
            
            If op.DbType = DbType.Int32 And op.Direction = ParameterDirection.Input Then
                If Convert.ToInt32(op.Value) = 0 Then
                    op.Value = DBNull.Value
                End If
            End If

            If op.DbType = DbType.Int16 And op.Direction = ParameterDirection.Input Then
                If Convert.ToInt16(op.Value) = 0 Then
                    op.Value = DBNull.Value
                End If
            End If

            If op.DbType = DbType.String And op.Direction = ParameterDirection.Input Then
                If op.Value.ToString.Trim() = "" Then
                    op.Value = DBNull.Value
                End If
            End If
        Next

        Return ocom

    End Function


#End Region
End Class
