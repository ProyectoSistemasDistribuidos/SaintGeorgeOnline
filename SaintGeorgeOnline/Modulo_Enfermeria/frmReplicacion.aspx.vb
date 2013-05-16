Imports System.Web.Services
Imports System.Web.Script.Services
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports System.Data
Imports SaintGeorgeOnline_BusinessEntities
Imports System.Reflection
Imports SaintGeorgeOnline_BusinessLogic

Partial Class Modulo_Enfermeria_frmReplicacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub


    Public Function F_listarFichaAlumnos(ByVal codSol As Integer, ByVal codAlumno As Integer) As Integer
        Try
            Dim nParam As String = "USP_lisFIchaEnfermeriaAlumno"
            Dim dc As New Dictionary(Of String, Object)
            dc.Add("AL_CodigoAlumno", codAlumno)
            dc.Add("SAFM_CodigoSolicitud", codSol)
            Dim dtc As New DataSet
            dtc = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam)
            Dim dtActual As New DataTable
            dtActual = dtc.Tables(0)
            Dim dtNuevo As New DataTable
            dtNuevo = dtc.Tables(1)

            Dim lsActualiza As New List(Of sqlObjectActualiza)

            Dim lsFichaActual As New List(Of sqlObjectActualiza)
            lsActualiza = creaListaActualizar(dtNuevo)
            lsFichaActual = crearListaActual(dtActual)

            ''----------------------------
            Dim nParamII As String = "USP_lisDetalleFichaEnfermedad"

            Dim dcII As New Dictionary(Of String, Object)
            dcII.Add("SAFM_CodigoSolicitud", codSol)
            dcII.Add("AL_CodigoAlumno", codAlumno)
            Dim dtcII As New DataSet
            dtcII = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dcII, nParamII)

            Dim dtFichaMapping As New DataTable

            dtFichaMapping = dtcII.Tables(0)

            Dim dtActualMapping As New DataTable

            dtActualMapping = dtcII.Tables(1)

            Dim obe_Consulta As be_Consulta
            Dim tipoConsultaBE As Type
            tipoConsultaBE = GetType(be_Consulta)
            Dim infoObject As FieldInfo
            Dim nombreTipoClase As String = ""

            Dim arrInfoFIeld As FieldInfo() = tipoConsultaBE.GetFields()





            Dim lstbe_Consulta As New List(Of be_Consulta)
            For Each filas As DataRow In dtFichaMapping.Rows
                obe_Consulta = New be_Consulta
                For Each columna As DataColumn In dtFichaMapping.Columns
                    Dim lstNnombres = From h In tipoConsultaBE.GetFields() Select h.Name

                    If Not lstNnombres.Contains(columna.ColumnName.ToString) Then
                        Continue For
                    End If

                    infoObject = tipoConsultaBE.GetField(columna.ColumnName.ToString)
                    nombreTipoClase = infoObject.FieldType.Name()
                    If nombreTipoClase = "Int32" Then
                        infoObject.SetValue(obe_Consulta, CInt(filas(columna.ColumnName.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        infoObject.SetValue(obe_Consulta, CStr(filas(columna.ColumnName.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        infoObject.SetValue(obe_Consulta, CBool(filas(columna.ColumnName.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        infoObject.SetValue(obe_Consulta, CDate(filas(columna.ColumnName.ToString)))
                    End If

                Next
                lstbe_Consulta.Add(obe_Consulta)
            Next

            Dim TYP_BE_EN_RelacionFichaMedicasAlergias As Type = GetType(BE_EN_RelacionFichaMedicasAlergias)
            Dim oBE_EN_RelacionFichaMedicasAlergias As BE_EN_RelacionFichaMedicasAlergias
            Dim lstBE_EN_RelacionFichaMedicasAlergias As New List(Of BE_EN_RelacionFichaMedicasAlergias)

            Dim TYP_BE_EN_RelacionFichaMedicasCarecteristicasPiel As Type = GetType(BE_EN_RelacionFichaMedicasCarecteristicasPiel)
            Dim LStBE_EN_RelacionFichaMedicasCarecteristicasPiel As New List(Of BE_EN_RelacionFichaMedicasCarecteristicasPiel)
            Dim oBE_EN_RelacionFichaMedicasCarecteristicasPiel As BE_EN_RelacionFichaMedicasCarecteristicasPiel

            Dim TYP_BE_EN_RelacionFichaMedicasEnfermedades As Type = GetType(BE_EN_RelacionFichaMedicasEnfermedades)
            Dim LstBE_EN_RelacionFichaMedicasEnfermedades As New List(Of BE_EN_RelacionFichaMedicasEnfermedades)
            Dim oBE_EN_RelacionFichaMedicasEnfermedades As BE_EN_RelacionFichaMedicasEnfermedades

            Dim TYP_BE_EN_RelacionFichaMedicasMotivoHospitalizacion As Type = GetType(BE_EN_RelacionFichaMedicasMotivoHospitalizacion)
            Dim LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion As New List(Of BE_EN_RelacionFichaMedicasMotivoHospitalizacion)
            Dim oBE_EN_RelacionFichaMedicasMotivoHospitalizacion As BE_EN_RelacionFichaMedicasMotivoHospitalizacion

            Dim TYP_BE_EN_RelacionFichaMedicasOperaciones As Type = GetType(BE_EN_RelacionFichaMedicasOperaciones)
            Dim LstBE_EN_RelacionFichaMedicasOperaciones As New List(Of BE_EN_RelacionFichaMedicasOperaciones)
            Dim oBE_EN_RelacionFichaMedicasOperaciones As BE_EN_RelacionFichaMedicasOperaciones

            Dim TYP_BE_EN_RelacionFichaMedicasTiposControles As Type = GetType(BE_EN_RelacionFichaMedicasTiposControles)
            Dim LstBE_EN_RelacionFichaMedicasTiposControles As New List(Of BE_EN_RelacionFichaMedicasTiposControles)
            Dim oBE_EN_RelacionFichaMedicasTiposControles As BE_EN_RelacionFichaMedicasTiposControles

            Dim TYP_BE_EN_RelacionFichaMedicasVacunas As Type = GetType(BE_EN_RelacionFichaMedicasVacunas)
            Dim LStBE_EN_RelacionFichaMedicasVacunas As New List(Of BE_EN_RelacionFichaMedicasVacunas)
            Dim oBE_EN_RelacionFichaMedicasVacunas As BE_EN_RelacionFichaMedicasVacunas


            Dim TYP_BE_EN_RelacionFichaMedicasMedicamentos As Type = GetType(BE_EN_RelacionFichaMedicasMedicamentos)
            Dim LStBE_EN_RelacionFichaMedicasMedicamentos As New List(Of BE_EN_RelacionFichaMedicasMedicamentos)
            Dim oBE_EN_RelacionFichaMedicasMedicamentos As BE_EN_RelacionFichaMedicasMedicamentos



            Dim oFieldInfo As FieldInfo()
            Dim nombreTipo As String = ""

            For Each filasMaping As DataRow In dtActualMapping.Rows
                oBE_EN_RelacionFichaMedicasAlergias = New BE_EN_RelacionFichaMedicasAlergias
                oBE_EN_RelacionFichaMedicasCarecteristicasPiel = New BE_EN_RelacionFichaMedicasCarecteristicasPiel
                oBE_EN_RelacionFichaMedicasEnfermedades = New BE_EN_RelacionFichaMedicasEnfermedades
                oBE_EN_RelacionFichaMedicasMotivoHospitalizacion = New BE_EN_RelacionFichaMedicasMotivoHospitalizacion
                oBE_EN_RelacionFichaMedicasOperaciones = New BE_EN_RelacionFichaMedicasOperaciones
                oBE_EN_RelacionFichaMedicasTiposControles = New BE_EN_RelacionFichaMedicasTiposControles
                oBE_EN_RelacionFichaMedicasVacunas = New BE_EN_RelacionFichaMedicasVacunas
                oBE_EN_RelacionFichaMedicasMedicamentos = New BE_EN_RelacionFichaMedicasMedicamentos

                ''---------------------------------------------------------------------------------------------------
                oFieldInfo = TYP_BE_EN_RelacionFichaMedicasMedicamentos.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo
                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMedicamentos, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMedicamentos, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMedicamentos, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMedicamentos, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    ''
                Next

                If Not (LStBE_EN_RelacionFichaMedicasMedicamentos.Select((Function(o) o.RFMD_CodigoRelFichaMedMedicamentos)).Contains(oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_CodigoRelFichaMedMedicamentos)) Then

                    If oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_CodigoRelFichaMedMedicamentos <> 0 Then
                        LStBE_EN_RelacionFichaMedicasMedicamentos.Add(oBE_EN_RelacionFichaMedicasMedicamentos)
                    End If



                End If




                ''---------------------------------------------------------------------------------------------------


                ''---------------------------------------------------------------------------------------------------
                oFieldInfo = TYP_BE_EN_RelacionFichaMedicasAlergias.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo
                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasAlergias, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasAlergias, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasAlergias, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasAlergias, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    ''
                Next


                If Not (lstBE_EN_RelacionFichaMedicasAlergias.Select(Function(o) o.RFAG_CodigoRelFichaMedAlergias).Contains(oBE_EN_RelacionFichaMedicasAlergias.RFAG_CodigoRelFichaMedAlergias)) Then
                    If oBE_EN_RelacionFichaMedicasAlergias.RFAG_CodigoRelFichaMedAlergias <> 0 Then
                        lstBE_EN_RelacionFichaMedicasAlergias.Add(oBE_EN_RelacionFichaMedicasAlergias)
                    End If


                End If
                ''---------------------------------------------------------------------------------------------------
                oFieldInfo = TYP_BE_EN_RelacionFichaMedicasCarecteristicasPiel.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasCarecteristicasPiel, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasCarecteristicasPiel, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasCarecteristicasPiel, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasCarecteristicasPiel, CDate(filasMaping(nombreField.Name.ToString)))
                    End If
                    ''
                Next
                If Not (LStBE_EN_RelacionFichaMedicasCarecteristicasPiel.Select(Function(o) o.RFCP_CodigoRelFichaMedCaractPiel)).Contains(oBE_EN_RelacionFichaMedicasCarecteristicasPiel.RFCP_CodigoRelFichaMedCaractPiel) Then
                    If oBE_EN_RelacionFichaMedicasCarecteristicasPiel.RFCP_CodigoRelFichaMedCaractPiel <> 0 Then
                        LStBE_EN_RelacionFichaMedicasCarecteristicasPiel.Add(oBE_EN_RelacionFichaMedicasCarecteristicasPiel)
                    End If




                End If



                ''---------------------------------------------------------------------------------------------------
                oFieldInfo = TYP_BE_EN_RelacionFichaMedicasEnfermedades.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo
                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasEnfermedades, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasEnfermedades, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasEnfermedades, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasEnfermedades, CDate(filasMaping(nombreField.Name.ToString)))
                    End If
                Next

                If Not (LstBE_EN_RelacionFichaMedicasEnfermedades.Select(Function(o) o.RFEF_CodigoRelFichaMedEnEnfermedades)).Contains(oBE_EN_RelacionFichaMedicasEnfermedades.RFEF_CodigoRelFichaMedEnEnfermedades) Then
                    If oBE_EN_RelacionFichaMedicasEnfermedades.RFEF_CodigoRelFichaMedEnEnfermedades <> 0 Then
                        LstBE_EN_RelacionFichaMedicasEnfermedades.Add(oBE_EN_RelacionFichaMedicasEnfermedades)
                    End If




                End If

                ''---------------------------------------------------------------------------------------------------
                oFieldInfo = TYP_BE_EN_RelacionFichaMedicasMotivoHospitalizacion.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion, CDate(filasMaping(nombreField.Name.ToString)))
                    End If
                Next

                If Not (LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion.Select(Function(o) o.RFMH_CodigoRelFichaMedMotivoHosp)).Contains(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion.RFMH_CodigoRelFichaMedMotivoHosp) Then

                    If oBE_EN_RelacionFichaMedicasMotivoHospitalizacion.RFMH_CodigoRelFichaMedMotivoHosp <> 0 Then
                        LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion.Add(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion)
                    End If


                End If



                ''---------------------------------------------------------------------------------------------------
                oFieldInfo = TYP_BE_EN_RelacionFichaMedicasOperaciones.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasOperaciones, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasOperaciones, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasOperaciones, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasOperaciones, CDate(filasMaping(nombreField.Name.ToString)))
                    End If
                Next




                If Not (LstBE_EN_RelacionFichaMedicasOperaciones.Select(Function(o) o.RFOM_CodigoRelFichaMedOperaciones).Contains(oBE_EN_RelacionFichaMedicasOperaciones.RFOM_CodigoRelFichaMedOperaciones)) Then


                    If oBE_EN_RelacionFichaMedicasOperaciones.RFOM_CodigoRelFichaMedOperaciones <> 0 Then
                        LstBE_EN_RelacionFichaMedicasOperaciones.Add(oBE_EN_RelacionFichaMedicasOperaciones)
                    End If

                End If
                ''---------------------------------------------------------------------------------------------------
                oFieldInfo = TYP_BE_EN_RelacionFichaMedicasTiposControles.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasTiposControles, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasTiposControles, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasTiposControles, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasTiposControles, CDate(filasMaping(nombreField.Name.ToString)))
                    End If
                Next


                If Not (LstBE_EN_RelacionFichaMedicasTiposControles.Select(Function(o) o.RFTC_CodigoRelFichaMedTiposControles).Contains(oBE_EN_RelacionFichaMedicasTiposControles.RFTC_CodigoRelFichaMedTiposControles)) Then

                    If oBE_EN_RelacionFichaMedicasTiposControles.RFTC_CodigoRelFichaMedTiposControles <> 0 Then
                        LstBE_EN_RelacionFichaMedicasTiposControles.Add(oBE_EN_RelacionFichaMedicasTiposControles)
                    End If



                End If

                ''---------------------------------------------------------------------------------------------------
                oFieldInfo = TYP_BE_EN_RelacionFichaMedicasVacunas.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasVacunas, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasVacunas, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasVacunas, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasVacunas, CDate(filasMaping(nombreField.Name.ToString)))
                    End If
                Next


                If Not (LStBE_EN_RelacionFichaMedicasVacunas.Select(Function(o) o.RFVC_CodigoRelVacunasFichaMed).Contains(oBE_EN_RelacionFichaMedicasVacunas.RFVC_CodigoRelVacunasFichaMed)) Then

                    If oBE_EN_RelacionFichaMedicasVacunas.RFVC_CodigoRelVacunasFichaMed <> 0 Then
                        LStBE_EN_RelacionFichaMedicasVacunas.Add(oBE_EN_RelacionFichaMedicasVacunas)
                    End If



                End If
                ''---------------------------------------------------------------------------------------------------

            Next


            ''llllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllll

            Dim TYP_BE_EN_RelacionFichaMedicasAlergias_temp As Type = GetType(BE_EN_RelacionFichaMedicasAlergias_Temp)
            Dim oBE_EN_RelacionFichaMedicasAlergias_temp As BE_EN_RelacionFichaMedicasAlergias_Temp
            Dim lstBE_EN_RelacionFichaMedicasAlergias_temp As New List(Of BE_EN_RelacionFichaMedicasAlergias_Temp)

            Dim TYP_BE_EN_RelacionFichaMedicasCarecteristicasPiel_temp As Type = GetType(BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp)
            Dim LStBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp As New List(Of BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp)
            Dim oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp As BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp

            Dim TYP_BE_EN_RelacionFichaMedicasEnfermedades_temp As Type = GetType(BE_EN_RelacionFichaMedicasEnfermedades_Temp)
            Dim LstBE_EN_RelacionFichaMedicasEnfermedades_temp As New List(Of BE_EN_RelacionFichaMedicasEnfermedades_Temp)
            Dim oBE_EN_RelacionFichaMedicasEnfermedades_temp As BE_EN_RelacionFichaMedicasEnfermedades_Temp

            Dim TYP_BE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp As Type = GetType(BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp)
            Dim LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp As New List(Of BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp)
            Dim oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp As BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp

            Dim TYP_BE_EN_RelacionFichaMedicasOperaciones_temp As Type = GetType(BE_EN_RelacionFichaMedicasOperaciones_Temp)
            Dim LstBE_EN_RelacionFichaMedicasOperaciones_temp As New List(Of BE_EN_RelacionFichaMedicasOperaciones_Temp)
            Dim oBE_EN_RelacionFichaMedicasOperaciones_temp As BE_EN_RelacionFichaMedicasOperaciones_Temp

            Dim TYP_BE_EN_RelacionFichaMedicasTiposControles_temp As Type = GetType(BE_EN_RelacionFichaMedicasTiposControles_Temp)
            Dim LstBE_EN_RelacionFichaMedicasTiposControles_temp As New List(Of BE_EN_RelacionFichaMedicasTiposControles_Temp)
            Dim oBE_EN_RelacionFichaMedicasTiposControles_temp As BE_EN_RelacionFichaMedicasTiposControles_Temp

            Dim TYP_BE_EN_RelacionFichaMedicasVacunas_temp As Type = GetType(BE_EN_RelacionFichaMedicasVacunas_Temp)
            Dim LStBE_EN_RelacionFichaMedicasVacunas_temp As New List(Of BE_EN_RelacionFichaMedicasVacunas_Temp)
            Dim oBE_EN_RelacionFichaMedicasVacunas_temp As BE_EN_RelacionFichaMedicasVacunas_Temp


            Dim TYP_BE_EN_RelacionFichaMedicasMedicamentos_temp As Type = GetType(BE_EN_RelacionFichaMedicasMedicamentos_Temp)
            Dim LStBE_EN_RelacionFichaMedicasMedicamentos_temp As New List(Of BE_EN_RelacionFichaMedicasMedicamentos_Temp)
            Dim oBE_EN_RelacionFichaMedicasMedicamentos_temp As BE_EN_RelacionFichaMedicasMedicamentos_Temp



            Dim oFieldInfo_temp As FieldInfo()
            Dim nombreTipo_temp As String = ""

            For Each filasMaping As DataRow In dtcII.Tables(0).Rows
                oBE_EN_RelacionFichaMedicasAlergias_temp = New BE_EN_RelacionFichaMedicasAlergias_Temp
                oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp = New BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp
                oBE_EN_RelacionFichaMedicasEnfermedades_temp = New BE_EN_RelacionFichaMedicasEnfermedades_Temp
                oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp = New BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp
                oBE_EN_RelacionFichaMedicasOperaciones_temp = New BE_EN_RelacionFichaMedicasOperaciones_Temp
                oBE_EN_RelacionFichaMedicasTiposControles_temp = New BE_EN_RelacionFichaMedicasTiposControles_Temp
                oBE_EN_RelacionFichaMedicasVacunas_temp = New BE_EN_RelacionFichaMedicasVacunas_Temp
                oBE_EN_RelacionFichaMedicasMedicamentos_temp = New BE_EN_RelacionFichaMedicasMedicamentos_Temp

                ''......................................................................
                ''---------------------------------------------------------------------------------------------------
                oFieldInfo_temp = TYP_BE_EN_RelacionFichaMedicasMedicamentos_temp.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo_temp
                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMedicamentos_temp, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMedicamentos_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMedicamentos_temp, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMedicamentos_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    ''
                Next

                If Not (LStBE_EN_RelacionFichaMedicasMedicamentos_temp.Select(Function(o) o.RFMD_CodigoRelFichaMedMedicamentos).Contains(oBE_EN_RelacionFichaMedicasMedicamentos_temp.RFMD_CodigoRelFichaMedMedicamentos)) Then

                    If oBE_EN_RelacionFichaMedicasMedicamentos_temp.RFMD_CodigoRelFichaMedMedicamentos <> 0 Then
                        LStBE_EN_RelacionFichaMedicasMedicamentos_temp.Add(oBE_EN_RelacionFichaMedicasMedicamentos_temp)
                    End If


                End If
                ''......................................................................


                ''---------------------------------------------------------------------------------------------------
                oFieldInfo_temp = TYP_BE_EN_RelacionFichaMedicasAlergias_temp.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo_temp
                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasAlergias_temp, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasAlergias_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasAlergias_temp, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasAlergias_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    ''
                Next

                If Not (lstBE_EN_RelacionFichaMedicasAlergias_temp.Select(Function(o) o.RFAG_CodigoRelFichaMedAlergias).Contains(oBE_EN_RelacionFichaMedicasAlergias_temp.RFAG_CodigoRelFichaMedAlergias)) Then
                    If oBE_EN_RelacionFichaMedicasAlergias_temp.RFAG_CodigoRelFichaMedAlergias <> 0 Then
                        lstBE_EN_RelacionFichaMedicasAlergias_temp.Add(oBE_EN_RelacionFichaMedicasAlergias_temp)
                    End If




                End If

                ''---------------------------------------------------------------------------------------------------
                oFieldInfo_temp = TYP_BE_EN_RelacionFichaMedicasCarecteristicasPiel_temp.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo_temp


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    ''
                Next


                If Not (LStBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp.Select(Function(o) o.RFCP_CodigoRelFichaMedCaractPiel).Contains(oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp.RFCP_CodigoRelFichaMedCaractPiel)) Then


                    If oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp.RFCP_CodigoRelFichaMedCaractPiel <> 0 Then
                        LStBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp.Add(oBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp)
                    End If

                End If

                ''---------------------------------------------------------------------------------------------------
                oFieldInfo_temp = TYP_BE_EN_RelacionFichaMedicasEnfermedades_temp.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo_temp
                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasEnfermedades_temp, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasEnfermedades_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasEnfermedades_temp, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasEnfermedades_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                Next


                If Not (LstBE_EN_RelacionFichaMedicasEnfermedades_temp.Select(Function(o) o.RFEF_CodigoRelFichaMedEnEnfermedades).Contains(oBE_EN_RelacionFichaMedicasEnfermedades_temp.RFEF_CodigoRelFichaMedEnEnfermedades)) Then

                    If oBE_EN_RelacionFichaMedicasEnfermedades_temp.RFEF_CodigoRelFichaMedEnEnfermedades <> 0 Then
                        LstBE_EN_RelacionFichaMedicasEnfermedades_temp.Add(oBE_EN_RelacionFichaMedicasEnfermedades_temp)
                    End If
                End If
                ''---------------------------------------------------------------------------------------------------
                oFieldInfo_temp = TYP_BE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo_temp


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                Next

                If Not (LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp.Select(Function(o) o.RFMH_CodigoRelFichaMedMotivoHosp).Contains(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp.RFMH_CodigoRelFichaMedMotivoHosp)) Then

                    If oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp.RFMH_CodigoRelFichaMedMotivoHosp <> 0 Then
                        LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp.Add(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp)
                    End If


                End If


                ''---------------------------------------------------------------------------------------------------
                oFieldInfo_temp = TYP_BE_EN_RelacionFichaMedicasOperaciones_temp.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo_temp
                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasOperaciones_temp, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasOperaciones_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasOperaciones_temp, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasOperaciones_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                Next


                If Not (LstBE_EN_RelacionFichaMedicasOperaciones_temp.Select(Function(o) o.RFOM_CodigoRelFichaMedOperaciones).Contains(oBE_EN_RelacionFichaMedicasOperaciones_temp.RFOM_CodigoRelFichaMedOperaciones)) Then
                    If oBE_EN_RelacionFichaMedicasOperaciones_temp.RFOM_CodigoRelFichaMedOperaciones <> 0 Then
                        LstBE_EN_RelacionFichaMedicasOperaciones_temp.Add(oBE_EN_RelacionFichaMedicasOperaciones_temp)
                    End If


                End If
                ''---------------------------------------------------------------------------------------------------
                oFieldInfo_temp = TYP_BE_EN_RelacionFichaMedicasTiposControles_temp.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo_temp


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasTiposControles_temp, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasTiposControles_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasTiposControles_temp, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasTiposControles_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                Next

                'oBE_EN_RelacionFichaMedicasTiposControles_temp.RFTC_CodigoRelFichaMedTiposControles
                If Not (LstBE_EN_RelacionFichaMedicasTiposControles_temp.Select(Function(o) o.RFTC_CodigoRelFichaMedTiposControles).Contains(oBE_EN_RelacionFichaMedicasTiposControles_temp.RFTC_CodigoRelFichaMedTiposControles)) Then


                    If oBE_EN_RelacionFichaMedicasTiposControles_temp.RFTC_CodigoRelFichaMedTiposControles <> 0 Then
                        LstBE_EN_RelacionFichaMedicasTiposControles_temp.Add(oBE_EN_RelacionFichaMedicasTiposControles_temp)
                    End If
                End If

                ''---------------------------------------------------------------------------------------------------
                oFieldInfo_temp = TYP_BE_EN_RelacionFichaMedicasVacunas_temp.GetFields()
                For Each nombreField As FieldInfo In oFieldInfo_temp


                    nombreTipoClase = nombreField.FieldType.Name.ToString
                    ''
                    If nombreTipoClase = "Int32" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasVacunas_temp, CInt(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "String" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasVacunas_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Boolean" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasVacunas_temp, CBool(filasMaping(nombreField.Name.ToString)))
                    End If
                    If nombreTipoClase = "Date" Then
                        nombreField.SetValue(oBE_EN_RelacionFichaMedicasVacunas_temp, CStr(filasMaping(nombreField.Name.ToString)))
                    End If
                Next

                If Not (LStBE_EN_RelacionFichaMedicasVacunas_temp.Select(Function(o) o.RFVC_CodigoRelVacunasFichaMed).Contains(oBE_EN_RelacionFichaMedicasVacunas_temp.RFVC_CodigoRelVacunasFichaMed)) Then

                    If oBE_EN_RelacionFichaMedicasVacunas_temp.RFVC_CodigoRelVacunasFichaMed <> 0 Then
                        LStBE_EN_RelacionFichaMedicasVacunas_temp.Add(oBE_EN_RelacionFichaMedicasVacunas_temp)
                    End If
                End If


                ''---------------------------------------------------------------------------------------------------
                ' 
            Next
            ''llllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllll
            ''ooooooooooooooo
            Dim idResultado As Integer = 0

            idResultado = F_ActualizarEstado(lsActualiza, codSol, codAlumno, lstBE_EN_RelacionFichaMedicasAlergias_temp, LStBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp, _
                                                        LstBE_EN_RelacionFichaMedicasEnfermedades_temp, LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp, _
                                                        LstBE_EN_RelacionFichaMedicasOperaciones_temp, LstBE_EN_RelacionFichaMedicasTiposControles_temp, LStBE_EN_RelacionFichaMedicasVacunas_temp, _
                                                        LStBE_EN_RelacionFichaMedicasMedicamentos_temp)




            'Return New With { _
            '        .actual = lsFichaActual, .actualizar = lsActualiza, _
            '        .lstBE_EN_RelacionFichaMedicasAlergias = lstBE_EN_RelacionFichaMedicasAlergias, _
            '        .LStBE_EN_RelacionFichaMedicasCarecteristicasPiel = LStBE_EN_RelacionFichaMedicasCarecteristicasPiel, _
            '        .LstBE_EN_RelacionFichaMedicasEnfermedades = LstBE_EN_RelacionFichaMedicasEnfermedades, _
            '        .LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion = LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion, _
            '        .LstBE_EN_RelacionFichaMedicasOperaciones = LstBE_EN_RelacionFichaMedicasOperaciones, _
            '        .LstBE_EN_RelacionFichaMedicasTiposControles = LstBE_EN_RelacionFichaMedicasTiposControles, _
            '        .LStBE_EN_RelacionFichaMedicasVacunas = LStBE_EN_RelacionFichaMedicasVacunas, _
            '        .lstBE_EN_RelacionFichaMedicasAlergias_temp = lstBE_EN_RelacionFichaMedicasAlergias_temp, _
            '        .LStBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp = LStBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp, _
            '        .LstBE_EN_RelacionFichaMedicasEnfermedades_temp = LstBE_EN_RelacionFichaMedicasEnfermedades_temp, _
            '        .LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp = LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp, _
            '        .LstBE_EN_RelacionFichaMedicasOperaciones_temp = LstBE_EN_RelacionFichaMedicasOperaciones_temp, _
            '        .LstBE_EN_RelacionFichaMedicasTiposControles_temp = LstBE_EN_RelacionFichaMedicasTiposControles_temp, _
            '        .LStBE_EN_RelacionFichaMedicasVacunas_temp = LStBE_EN_RelacionFichaMedicasVacunas_temp, _
            '        .LStBE_EN_RelacionFichaMedicasMedicamentos_temp = LStBE_EN_RelacionFichaMedicasMedicamentos_temp, _
            '        .LStBE_EN_RelacionFichaMedicasMedicamentos = LStBE_EN_RelacionFichaMedicasMedicamentos}

            Return idResultado
        Catch ex As Exception

        End Try
    End Function



    Public Function F_ActualizarEstado(ByVal lisTsqlObjectActualiza As List(Of sqlObjectActualiza), ByVal codSol As Integer, ByVal codAlumno As Integer, _
             ByVal lstMedicasAlergias_temp As List(Of BE_EN_RelacionFichaMedicasAlergias_Temp), _
             ByVal lstCarecteristicasPiel As List(Of BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp), _
             ByVal lstEnfermedades As List(Of BE_EN_RelacionFichaMedicasEnfermedades_Temp), _
             ByVal lstHospitalizacion As List(Of BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp), _
             ByVal lstOperaciones As List(Of BE_EN_RelacionFichaMedicasOperaciones_Temp), _
             ByVal lstTiposControles As List(Of BE_EN_RelacionFichaMedicasTiposControles_Temp), _
             ByVal lstVacunas As List(Of BE_EN_RelacionFichaMedicasVacunas_Temp), _
             ByVal lstMedicamentos As List(Of BE_EN_RelacionFichaMedicasMedicamentos_Temp)) As Object
        Try
            Dim ofichaMedica As New BE_FichaEnfermeria
            Dim ofichaMedicaTemp As New BE_FichaEnfermeriaTemp
            Dim otFichaTemp As Type
            otFichaTemp = GetType(BE_FichaEnfermeriaTemp)
            Dim otFicha As Type
            otFicha = GetType(BE_FichaEnfermeria)
            Dim arrFieldInfo As FieldInfo()
            Dim fInfoFichaTemp As FieldInfo
            arrFieldInfo = otFicha.GetFields()
            Dim nombreTipoClase As String
            For Each oField As FieldInfo In arrFieldInfo
                nombreTipoClase = oField.FieldType.Name
                If nombreTipoClase = "Int32" Then
                    oField.SetValue(ofichaMedica, 0)
                End If
                If nombreTipoClase = "String" Then
                    oField.SetValue(ofichaMedica, "")
                End If
                If nombreTipoClase = "Boolean" Then
                    oField.SetValue(ofichaMedica, False)
                End If
            Next
            ofichaMedicaTemp.AL_CodigoAlumno = codAlumno
            ofichaMedicaTemp.SAFM_CodigoSolicitud = codSol
            ofichaMedica.AL_CodigoAlumno = codAlumno

            For Each osqlObjectActualiza As sqlObjectActualiza In lisTsqlObjectActualiza
                osqlObjectActualiza.check = True
                If osqlObjectActualiza.check = True Then
                    fInfoFichaTemp = otFichaTemp.GetField(osqlObjectActualiza.nombreCheck)
                    fInfoFichaTemp.SetValue(ofichaMedicaTemp, osqlObjectActualiza.check)

                    If osqlObjectActualiza.codTipo = -1 Then

                        Dim nombreTipo As String
                        fInfoFichaTemp = otFicha.GetField(osqlObjectActualiza.nombreCampo)
                        nombreTipo = fInfoFichaTemp.FieldType.Name


                        If nombreTipo = "Int32" Then
                            fInfoFichaTemp.SetValue(ofichaMedica, CInt(osqlObjectActualiza.descripcion))
                        ElseIf nombreTipo = "String" Then
                            fInfoFichaTemp.SetValue(ofichaMedica, osqlObjectActualiza.descripcion)
                        ElseIf nombreTipo = "Nullable`1" Then
                            Dim nombre As String = osqlObjectActualiza.descripcion.ToUpper()
                            fInfoFichaTemp.SetValue(ofichaMedica, estadoBool(osqlObjectActualiza.descripcion))
                        End If

                    Else
                        Dim nombreTipo As String
                        fInfoFichaTemp = otFicha.GetField(osqlObjectActualiza.nombreTipo)
                        nombreTipo = fInfoFichaTemp.FieldType.Name
                        fInfoFichaTemp.SetValue(ofichaMedica, osqlObjectActualiza.codTipo)
                    End If
                End If
            Next

            Dim lstMedicasAlergiasIns As New List(Of BE_EN_RelacionFichaMedicasAlergias)
            Dim oBE_EN_RelacionFichaMedicasAlergias As BE_EN_RelacionFichaMedicasAlergias

            Dim lstCarecteristicasPielIns As New List(Of BE_EN_RelacionFichaMedicasCarecteristicasPiel)
            Dim oBE_EN_RelacionFichaMedicasCarecteristicasPiel As BE_EN_RelacionFichaMedicasCarecteristicasPiel

            Dim lstEnfermedadesIns As New List(Of BE_EN_RelacionFichaMedicasEnfermedades)
            Dim oBE_EN_RelacionFichaMedicasEnfermedades As BE_EN_RelacionFichaMedicasEnfermedades

            Dim lstHospitalizacionIns As New List(Of BE_EN_RelacionFichaMedicasMotivoHospitalizacion)
            Dim oBE_EN_RelacionFichaMedicasMotivoHospitalizacion As BE_EN_RelacionFichaMedicasMotivoHospitalizacion

            Dim lstOperacionesIns As New List(Of BE_EN_RelacionFichaMedicasOperaciones)
            Dim oBE_EN_RelacionFichaMedicasOperaciones As BE_EN_RelacionFichaMedicasOperaciones

            Dim lstTiposControlesIns As New List(Of BE_EN_RelacionFichaMedicasTiposControles)
            Dim oBE_EN_RelacionFichaMedicasTiposControles As BE_EN_RelacionFichaMedicasTiposControles

            Dim lstVacunasIns As New List(Of BE_EN_RelacionFichaMedicasVacunas)
            Dim oBE_EN_RelacionFichaMedicasVacunas As BE_EN_RelacionFichaMedicasVacunas

            ' ByVal lstMedicamentos As List(Of BE_EN_RelacionFichaMedicasMedicamentos_Temp))
            Dim oBE_EN_RelacionFichaMedicasMedicamentos As BE_EN_RelacionFichaMedicasMedicamentos
            Dim lstBE_EN_RelacionFichaMedicasMedicamentos As New List(Of BE_EN_RelacionFichaMedicasMedicamentos)


            For Each oBE_EN_RelacionFichaMedicasMedicamentos_Temp As BE_EN_RelacionFichaMedicasMedicamentos_Temp In lstMedicamentos
                oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_Check = True
                If oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_Check Then
                    oBE_EN_RelacionFichaMedicasMedicamentos = New BE_EN_RelacionFichaMedicasMedicamentos
                    oBE_EN_RelacionFichaMedicasMedicamentos.MA_CodigoMedicamento = oBE_EN_RelacionFichaMedicasMedicamentos_Temp.MA_CodigoMedicamento
                    oBE_EN_RelacionFichaMedicasMedicamentos.PM_CodigoPresentacion = oBE_EN_RelacionFichaMedicasMedicamentos_Temp.PM_CodigoPresentacion
                    oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_CantidadPresentacion = oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_CantidadPresentacion
                    oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_DosisMedicamento = oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_DosisMedicamento
                    oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_Observaciones = oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_Observaciones
                    oBE_EN_RelacionFichaMedicasMedicamentos.RFMD_FechaRegistro = CDate(oBE_EN_RelacionFichaMedicasMedicamentos_Temp.RFMD_FechaRegistro)
                    lstBE_EN_RelacionFichaMedicasMedicamentos.Add(oBE_EN_RelacionFichaMedicasMedicamentos)
                End If
            Next


            For Each oBE_EN_RelacionFichaMedicasAlergias_Temp As BE_EN_RelacionFichaMedicasAlergias_Temp In lstMedicasAlergias_temp
                oBE_EN_RelacionFichaMedicasAlergias_Temp.RFAG_Check = True
                If oBE_EN_RelacionFichaMedicasAlergias_Temp.RFAG_Check Then
                    oBE_EN_RelacionFichaMedicasAlergias = New BE_EN_RelacionFichaMedicasAlergias
                    oBE_EN_RelacionFichaMedicasAlergias.AG_CodigoAlergia = oBE_EN_RelacionFichaMedicasAlergias_Temp.AG_CodigoAlergia
                    oBE_EN_RelacionFichaMedicasAlergias.RFAG_FechaRegistro = CDate(oBE_EN_RelacionFichaMedicasAlergias_Temp.RFAG_FechaRegistro)
                    lstMedicasAlergiasIns.Add(oBE_EN_RelacionFichaMedicasAlergias)
                End If
            Next
            ''
            For Each oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp As BE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp In lstCarecteristicasPiel
                oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp.RFCP_Check = True
                If oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp.RFCP_Check Then
                    oBE_EN_RelacionFichaMedicasCarecteristicasPiel = New BE_EN_RelacionFichaMedicasCarecteristicasPiel
                    oBE_EN_RelacionFichaMedicasCarecteristicasPiel.TCP_CodigoCaracteristicapiel = oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp.TCP_CodigoCaracteristicapiel
                    oBE_EN_RelacionFichaMedicasCarecteristicasPiel.RFCP_FechaRegistro = CDate(oBE_EN_RelacionFichaMedicasCarecteristicasPiel_Temp.RFCP_FechaRegistro)
                    lstCarecteristicasPielIns.Add(oBE_EN_RelacionFichaMedicasCarecteristicasPiel)
                End If
            Next
            ''
            For Each oBE_EN_RelacionFichaMedicasEnfermedades_Temp As BE_EN_RelacionFichaMedicasEnfermedades_Temp In lstEnfermedades
                oBE_EN_RelacionFichaMedicasEnfermedades_Temp.RFEF_Check = True
                If oBE_EN_RelacionFichaMedicasEnfermedades_Temp.RFEF_Check Then
                    oBE_EN_RelacionFichaMedicasEnfermedades = New BE_EN_RelacionFichaMedicasEnfermedades
                    oBE_EN_RelacionFichaMedicasEnfermedades.EF_CodigoEnfermedad = oBE_EN_RelacionFichaMedicasEnfermedades_Temp.EF_CodigoEnfermedad
                    oBE_EN_RelacionFichaMedicasEnfermedades.RFEF_Edad = oBE_EN_RelacionFichaMedicasEnfermedades_Temp.RFEF_Edad

                    lstEnfermedadesIns.Add(oBE_EN_RelacionFichaMedicasEnfermedades)
                End If
            Next
            ''
            For Each oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp As BE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp In lstHospitalizacion
                oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp.RFMH_Check = True
                If oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp.RFMH_Check Then
                    oBE_EN_RelacionFichaMedicasMotivoHospitalizacion = New BE_EN_RelacionFichaMedicasMotivoHospitalizacion
                    oBE_EN_RelacionFichaMedicasMotivoHospitalizacion.MH_CodigoMotivoHospitalizacion = oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp.MH_CodigoMotivoHospitalizacion
                    oBE_EN_RelacionFichaMedicasMotivoHospitalizacion.RFMH_FechaHospitalizacion = CDate(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion_Temp.RFMH_FechaHospitalizacion)
                    lstHospitalizacionIns.Add(oBE_EN_RelacionFichaMedicasMotivoHospitalizacion)
                End If
            Next
            ''
            For Each oBE_EN_RelacionFichaMedicasOperaciones_Temp As BE_EN_RelacionFichaMedicasOperaciones_Temp In lstOperaciones
                oBE_EN_RelacionFichaMedicasOperaciones_Temp.RFOM_Check = True
                If oBE_EN_RelacionFichaMedicasOperaciones_Temp.RFOM_Check Then
                    oBE_EN_RelacionFichaMedicasOperaciones = New BE_EN_RelacionFichaMedicasOperaciones
                    oBE_EN_RelacionFichaMedicasOperaciones.RFOM_FechaOperacion = CDate(oBE_EN_RelacionFichaMedicasOperaciones_Temp.RFOM_FechaOperacion)
                    oBE_EN_RelacionFichaMedicasOperaciones.TOM_CodigoTipoOperaciones = oBE_EN_RelacionFichaMedicasOperaciones_Temp.TOM_CodigoTipoOperaciones
                    lstOperacionesIns.Add(oBE_EN_RelacionFichaMedicasOperaciones)
                End If
            Next
            ''
            For Each oBE_EN_RelacionFichaMedicasTiposControles_Temp As BE_EN_RelacionFichaMedicasTiposControles_Temp In lstTiposControles
                oBE_EN_RelacionFichaMedicasTiposControles_Temp.RFTC_Check = True
                If oBE_EN_RelacionFichaMedicasTiposControles_Temp.RFTC_Check Then
                    oBE_EN_RelacionFichaMedicasTiposControles = New BE_EN_RelacionFichaMedicasTiposControles
                    oBE_EN_RelacionFichaMedicasTiposControles.RFTC_FechaControl = CDate(oBE_EN_RelacionFichaMedicasTiposControles_Temp.RFTC_FechaControl)
                    oBE_EN_RelacionFichaMedicasTiposControles.RFTC_Resultado = oBE_EN_RelacionFichaMedicasTiposControles_Temp.RFTC_Resultado
                    oBE_EN_RelacionFichaMedicasTiposControles.TC_CodigoTipoControl = oBE_EN_RelacionFichaMedicasTiposControles_Temp.TC_CodigoTipoControl
                    lstTiposControlesIns.Add(oBE_EN_RelacionFichaMedicasTiposControles)

                End If
            Next
            ''
            For Each oBE_EN_RelacionFichaMedicasVacunas_Temp As BE_EN_RelacionFichaMedicasVacunas_Temp In lstVacunas
                oBE_EN_RelacionFichaMedicasVacunas_Temp.RFVC_Check = True
                If oBE_EN_RelacionFichaMedicasVacunas_Temp.RFVC_Check Then
                    oBE_EN_RelacionFichaMedicasVacunas = New BE_EN_RelacionFichaMedicasVacunas

                    oBE_EN_RelacionFichaMedicasVacunas.DV_CodigoDosis = oBE_EN_RelacionFichaMedicasVacunas_Temp.DV_CodigoDosis
                    oBE_EN_RelacionFichaMedicasVacunas.VC_CodigoVacuna = oBE_EN_RelacionFichaMedicasVacunas_Temp.VC_CodigoVacuna
                    oBE_EN_RelacionFichaMedicasVacunas.RFVC_FechaVacunacion = CDate(oBE_EN_RelacionFichaMedicasVacunas_Temp.RFVC_FechaVacunacion)
                    oBE_EN_RelacionFichaMedicasVacunas.RFVC_Edad = oBE_EN_RelacionFichaMedicasVacunas_Temp.RFVC_Edad
                    lstVacunasIns.Add(oBE_EN_RelacionFichaMedicasVacunas)
                End If
            Next



            Dim oBL_FichaEnfermeriaTemp As New BL_FichaEnfermeriaTemp
            Dim cod As Integer = 0




            cod = oBL_FichaEnfermeriaTemp.F_ActualizarEnfermeriaTmpEstado(ofichaMedicaTemp, ofichaMedica, _
                    lstMedicasAlergiasIns, _
                    lstMedicasAlergias_temp, _
                    lstCarecteristicasPielIns, _
                    lstCarecteristicasPiel, _
                    lstEnfermedades, _
                    lstEnfermedadesIns, _
                    lstBE_EN_RelacionFichaMedicasMedicamentos, _
                    lstMedicamentos, _
                    lstHospitalizacionIns, _
                    lstHospitalizacion, _
                    lstOperacionesIns, _
                    lstOperaciones, _
                    lstTiposControlesIns, _
                    lstTiposControles, _
                    lstVacunasIns, _
                    lstVacunas)


            Return cod

        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function creaListaActualizar(ByVal dtNuevo As DataTable) As List(Of sqlObjectActualiza)
        Try


            Dim lsActualiza As New List(Of sqlObjectActualiza)


            Dim osqlObjectActualiza As sqlObjectActualiza

            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo(0)("TN_CodigoTipoNacimiento_Check"))
            osqlObjectActualiza.codTipo = CInt(dtNuevo(0)("TN_CodigoTipoNacimiento"))
            osqlObjectActualiza.descripcion = dtNuevo(0)("TN_Descripcion").ToString()
            osqlObjectActualiza.nombreCampo = "TN_CodigoTipoNacimiento"
            osqlObjectActualiza.etiqueta = "Nacimiento"
            osqlObjectActualiza.nombreCheck = "TN_CodigoTipoNacimiento_Check"
            osqlObjectActualiza.nombreTipo = "TN_CodigoTipoNacimiento"



            lsActualiza.Add(osqlObjectActualiza)


            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("TSA_CodigoTipoSangre_Check"))
            osqlObjectActualiza.codTipo = CInt(dtNuevo.Rows(0)("TSA_CodigoTipoSangre"))
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("TSA_Descripcion").ToString()
            osqlObjectActualiza.nombreCampo = "TN_CodigoTipoNacimiento"
            osqlObjectActualiza.etiqueta = "Tipo Sangre"
            osqlObjectActualiza.nombreCheck = "TSA_CodigoTipoSangre_Check"
            osqlObjectActualiza.nombreTipo = "TSA_CodigoTipoSangre"

            lsActualiza.Add(osqlObjectActualiza)

            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_TipoNacimientoObservaciones_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_TipoNacimientoObservaciones").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_TipoNacimientoObservaciones"
            osqlObjectActualiza.etiqueta = "Observacion Nacimiento"
            osqlObjectActualiza.nombreCheck = "FMA_TipoNacimientoObservaciones_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_EdadLevantoCabeza_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_EdadLevantoCabeza").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_EdadLevantoCabeza"
            osqlObjectActualiza.etiqueta = "Edad levanto cabeza"
            osqlObjectActualiza.nombreCheck = "FMA_EdadLevantoCabeza_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_MesesLevantoCabeza_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_MesesLevantoCabeza").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_MesesLevantoCabeza"
            osqlObjectActualiza.etiqueta = "Meses levanto cabeza"
            osqlObjectActualiza.nombreCheck = "FMA_MesesLevantoCabeza_Check"

            lsActualiza.Add(osqlObjectActualiza)
            ''--
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_EdadSento_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_EdadSento").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_EdadSento"
            osqlObjectActualiza.etiqueta = "Edad sento "
            osqlObjectActualiza.nombreCheck = "FMA_EdadSento_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_MesesSento_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_MesesSento").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_MesesSento"
            osqlObjectActualiza.etiqueta = "Meses sento  "
            osqlObjectActualiza.nombreCheck = "FMA_MesesSento_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_EdadParo_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_EdadParo").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_EdadParo"
            osqlObjectActualiza.etiqueta = "Edad paro  "
            osqlObjectActualiza.nombreCheck = "FMA_EdadParo_Check"
            lsActualiza.Add(osqlObjectActualiza)

            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_MesesParo_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_MesesParo").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_MesesParo"
            osqlObjectActualiza.etiqueta = "Meses paro  "
            osqlObjectActualiza.nombreCheck = "FMA_MesesParo_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_EdadCamino_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_EdadCamino").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_EdadCamino"
            osqlObjectActualiza.etiqueta = "Edad camino  "
            osqlObjectActualiza.nombreCheck = "FMA_EdadCamino_Check"
            lsActualiza.Add(osqlObjectActualiza)

            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_MesesCamino_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_MesesCamino").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_MesesCamino"
            osqlObjectActualiza.etiqueta = "Meses camino  "
            osqlObjectActualiza.nombreCheck = "FMA_MesesCamino_Check"
            lsActualiza.Add(osqlObjectActualiza)

            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_EdadControloEsfinteres_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_EdadControloEsfinteres").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_EdadControloEsfinteres"
            osqlObjectActualiza.etiqueta = "Edad controlo  "
            osqlObjectActualiza.nombreCheck = "FMA_EdadControloEsfinteres_Check"
            lsActualiza.Add(osqlObjectActualiza)

            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_MesesControloEsfinteres_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_MesesControloEsfinteres").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_MesesControloEsfinteres"
            osqlObjectActualiza.etiqueta = "Meses controlo  "
            osqlObjectActualiza.nombreCheck = "FMA_MesesControloEsfinteres_Check"
            lsActualiza.Add(osqlObjectActualiza)

            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_EdadHabloPrimerasPalabras_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_EdadHabloPrimerasPalabras").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_EdadHabloPrimerasPalabras"
            osqlObjectActualiza.etiqueta = "Edad primera palabra  "
            osqlObjectActualiza.nombreCheck = "FMA_EdadHabloPrimerasPalabras_Check"
            lsActualiza.Add(osqlObjectActualiza)

            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_MesesHabloPrimerasPalabras_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_MesesHabloPrimerasPalabras").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_MesesHabloPrimerasPalabras"
            osqlObjectActualiza.etiqueta = "Meses hablo primera palabra   "
            osqlObjectActualiza.nombreCheck = "FMA_MesesHabloPrimerasPalabras_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_EdadHabloFluidez_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_EdadHabloFluidez").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_EdadHabloFluidez"
            osqlObjectActualiza.nombreCheck = "FMA_EdadHabloFluidez_Check"
            osqlObjectActualiza.etiqueta = "Edad Fluidez"
            lsActualiza.Add(osqlObjectActualiza)

            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_MesesHabloFluidez_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo(0)("FMA_MesesHabloFluidez").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_MesesHabloFluidez"
            osqlObjectActualiza.etiqueta = "Meses hablo Fluidez"
            osqlObjectActualiza.nombreCheck = "FMA_MesesHabloFluidez_Check"
            lsActualiza.Add(osqlObjectActualiza)

            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_TabiqueDesviado_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_TabiqueDesviado").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_TabiqueDesviado"
            osqlObjectActualiza.etiqueta = "Tabique desviado"
            osqlObjectActualiza.nombreCheck = "FMA_TabiqueDesviado_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_SangradoNasal_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_SangradoNasal").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_SangradoNasal"
            osqlObjectActualiza.etiqueta = "Sangrado Nasal"
            osqlObjectActualiza.nombreCheck = "FMA_SangradoNasal_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_UsaLentes_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo(0)("FMA_UsaLentes").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_UsaLentes"
            osqlObjectActualiza.etiqueta = "Usa lentes"
            osqlObjectActualiza.nombreCheck = "FMA_UsaLentes_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_ObservacionesOftalmologicas_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = IIf(dtNuevo.Rows(0)("FMA_ObservacionesOftalmologicas").ToString() = "", "--", dtNuevo.Rows(0)("FMA_ObservacionesOftalmologicas").ToString())

            osqlObjectActualiza.nombreCampo = "FMA_ObservacionesOftalmologicas"
            osqlObjectActualiza.etiqueta = "Observaciones Oftalmologicas"
            osqlObjectActualiza.nombreCheck = "FMA_ObservacionesOftalmologicas_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_UsaOrtodoncia_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = dtNuevo.Rows(0)("FMA_UsaOrtodoncia").ToString()
            osqlObjectActualiza.nombreCampo = "FMA_UsaOrtodoncia"
            osqlObjectActualiza.etiqueta = "Usa Ortodoncia"
            osqlObjectActualiza.nombreCheck = "FMA_UsaOrtodoncia_Check"
            lsActualiza.Add(osqlObjectActualiza)
            ''
            osqlObjectActualiza = New sqlObjectActualiza
            osqlObjectActualiza.check = CBool(dtNuevo.Rows(0)("FMA_ObservacionesDental_Check"))
            osqlObjectActualiza.codTipo = -1
            osqlObjectActualiza.descripcion = IIf(dtNuevo.Rows(0)("FMA_ObservacionesDental").ToString() = "", "--", dtNuevo.Rows(0)("FMA_ObservacionesDental").ToString())
            osqlObjectActualiza.nombreCampo = "FMA_ObservacionesDental"
            osqlObjectActualiza.etiqueta = "Obserbacion Dental"
            osqlObjectActualiza.nombreCheck = "FMA_ObservacionesDental_Check"
            lsActualiza.Add(osqlObjectActualiza)
            Return lsActualiza
        Catch ex As Exception

        End Try
    End Function

    Public Function crearListaActual(ByVal dtActual As DataTable) As List(Of sqlObjectActualiza)
        Dim osqlObjectActualiza As sqlObjectActualiza
        ''-----------------------------------------------------------------------
        Dim lsFichaActual As New List(Of sqlObjectActualiza)
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual(0)("TN_CodigoTipoNacimiento_Check"))
        osqlObjectActualiza.codTipo = CInt(dtActual(0)("TN_CodigoTipoNacimiento"))
        osqlObjectActualiza.descripcion = dtActual(0)("TN_Descripcion").ToString()
        osqlObjectActualiza.nombreCampo = "TN_CodigoTipoNacimiento"
        lsFichaActual.Add(osqlObjectActualiza)


        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("TSA_CodigoTipoSangre_Check"))
        osqlObjectActualiza.codTipo = CInt(dtActual.Rows(0)("TSA_CodigoTipoSangre"))
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("TSA_Descripcion").ToString()
        osqlObjectActualiza.nombreCampo = "TN_CodigoTipoNacimiento"
        lsFichaActual.Add(osqlObjectActualiza)

        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_TipoNacimientoObservaciones_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = IIf(dtActual.Rows(0)("FMA_TipoNacimientoObservaciones").ToString() = "", "-", dtActual.Rows(0)("FMA_TipoNacimientoObservaciones").ToString())
        osqlObjectActualiza.nombreCampo = "FMA_TipoNacimientoObservaciones"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_EdadLevantoCabeza_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_EdadLevantoCabeza").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_EdadLevantoCabeza"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_MesesLevantoCabeza_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_MesesLevantoCabeza").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_MesesLevantoCabeza"
        lsFichaActual.Add(osqlObjectActualiza)
        ''--
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_EdadSento_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_EdadSento").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_EdadSento"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_MesesSento_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_MesesSento").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_MesesSento"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_EdadParo_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_EdadParo").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_EdadParo"
        lsFichaActual.Add(osqlObjectActualiza)

        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_MesesParo_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_MesesParo").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_MesesParo"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_EdadCamino_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_EdadCamino").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_EdadCamino"
        lsFichaActual.Add(osqlObjectActualiza)

        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_MesesCamino_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_MesesCamino").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_MesesCamino"
        lsFichaActual.Add(osqlObjectActualiza)

        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_EdadControloEsfinteres_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_EdadControloEsfinteres").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_EdadControloEsfinteres"
        lsFichaActual.Add(osqlObjectActualiza)

        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_MesesControloEsfinteres_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_MesesControloEsfinteres").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_MesesControloEsfinteres"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_EdadHabloPrimerasPalabras_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_EdadHabloPrimerasPalabras").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_EdadHabloPrimerasPalabras"
        lsFichaActual.Add(osqlObjectActualiza)

        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_MesesHabloPrimerasPalabras_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_MesesHabloPrimerasPalabras").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_MesesHabloPrimerasPalabras"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_EdadHabloFluidez_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_EdadHabloFluidez").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_EdadHabloFluidez"
        lsFichaActual.Add(osqlObjectActualiza)

        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_MesesHabloFluidez_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual(0)("FMA_MesesHabloFluidez").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_MesesHabloFluidez"
        lsFichaActual.Add(osqlObjectActualiza)

        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_TabiqueDesviado_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_TabiqueDesviado").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_TabiqueDesviado"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_SangradoNasal_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_SangradoNasal").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_SangradoNasal"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_UsaLentes_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual(0)("FMA_UsaLentes").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_UsaLentes"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_ObservacionesOftalmologicas_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = IIf(dtActual.Rows(0)("FMA_ObservacionesOftalmologicas").ToString() = "", "--", dtActual.Rows(0)("FMA_ObservacionesOftalmologicas").ToString())

        osqlObjectActualiza.nombreCampo = "FMA_ObservacionesOftalmologicas"
        lsFichaActual.Add(osqlObjectActualiza)
        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_UsaOrtodoncia_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = dtActual.Rows(0)("FMA_UsaOrtodoncia").ToString()
        osqlObjectActualiza.nombreCampo = "FMA_UsaOrtodoncia"
        lsFichaActual.Add(osqlObjectActualiza)

        ''
        osqlObjectActualiza = New sqlObjectActualiza
        'osqlObjectActualiza.check = CBool(dtActual.Rows(0)("FMA_ObservacionesDental_Check"))
        osqlObjectActualiza.codTipo = -1
        osqlObjectActualiza.descripcion = IIf(dtActual.Rows(0)("FMA_ObservacionesDental").ToString() = "", "--", dtActual.Rows(0)("FMA_ObservacionesDental").ToString())
        osqlObjectActualiza.nombreCampo = "FMA_ObservacionesDental"
        lsFichaActual.Add(osqlObjectActualiza)
        Return lsFichaActual
    End Function

    Public Class sqlObjectActualiza
        Public codTipo As Integer
        Public descripcion As String
        Public nombreCampo As String
        Public nombreCheck As String
        Public check As Boolean
        Public etiqueta As String
        Public tipoControl As Integer
        Public nombreTipo As String
    End Class

    Shared Function estadoBool(ByVal cad As String) As Boolean?
        If cad.ToUpper.Trim = "NO" Then
            Return False
        End If
        If cad.ToUpper.Trim = "SI" Then
            Return True
        End If
        If cad.ToUpper.Trim = "--" Then
            Return Nothing
        End If
    End Function
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            Dim entero As Integer = 0
            Dim nParam As String = "USP_lisEstructuraSolicitud"
            Dim dc As New Dictionary(Of String, Object)

            dc.Add("AC_CodigoAnioAcademico", 2)
            dc.Add("RSAFM_EstadoSolicitud", 1)

            '@AC_CodigoAnioAcademico

            Dim dtc As New DataSet
            dtc = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam)
            Dim dtSol As New DataTable
            dtSol = dtc.Tables(0)

            For Each filas As DataRow In dtSol.Rows
                entero = F_listarFichaAlumnos(CInt(filas("SAFM_CodigoSolicitud").ToString()), CInt(filas("AL_CodigoAlumno").ToString()))
            Next


        Catch ex As Exception

        End Try


    End Sub
End Class
