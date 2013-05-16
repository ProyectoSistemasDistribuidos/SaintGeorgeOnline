Imports SaintGeorgeOnline_BusinessEntities.ModuloMatricula
Imports SaintGeorgeOnline_BusinessEntities.ModuloPensiones
Imports SaintGeorgeOnline_BusinessEntities.ModuloCursos
Imports SaintGeorgeOnline_DataAccess.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloCursos
Imports SaintGeorgeOnline_BusinessLogic.ModuloMatricula

Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_DataAccess.ModuloNotas

Imports SaintGeorgeOnline_Utilities
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Script.Serialization
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessEntities
Imports System.Web.Services
Imports System.Web.Script.Services
Partial Class Modulo_Reportes_frmNotasAlumno
    Inherits System.Web.UI.Page

    Public listaJsonLibreta As String = ""
    Public jsAsistensias As String = ""


    Function crearListaComponente(ByVal tb_listaNotasIndicador As DataTable) As List(Of notaComponente)
        Dim lstnotaComponente As New List(Of notaComponente)
        Dim onotaComponente As notaComponente
        Dim codTemComp As String
        For Each fila In tb_listaNotasIndicador.Rows
            onotaComponente = New notaComponente

            onotaComponente.nomComponente = fila("CP_Descripcion").ToString()
            onotaComponente.codRegNotaComponenente = Convert.ToInt32(fila("RNC_CodigoRegistroNotaComponente").ToString())
            onotaComponente.codAlumnos = fila("AL_CodigoAlumno").ToString()
            onotaComponente.notaComponente = fila("RNC_NotaComponente").ToString()
            onotaComponente.codRegComponente = Convert.ToInt32(fila("RC_CodigoRegistroComponentes").ToString())
            If onotaComponente.codRegNotaComponenente = 0 Then
                Continue For
            End If

            If onotaComponente.codRegNotaComponenente <> codTemComp Then
                lstnotaComponente.Add(onotaComponente)
            End If


            'codRegNotaComponenente()
            'codRegNotaIndicador()
            'codRegSubindicador()


            '            CP_Descripcion

            '            RNC_CodigoRegistroNotaComponente

            '            AL_CodigoAlumno
            codTemComp = onotaComponente.codRegNotaComponenente

        Next
        Return lstnotaComponente
        Try

        Catch ex As Exception

        End Try
    End Function
    Function crearListaIndicador(ByVal tb_listaNotasIndicador As DataTable) As List(Of notaIndicador)
        Try
            Dim lstNotaIndicador As New List(Of notaIndicador)
            Dim onotaIndicador As notaIndicador
            Dim codTempIndicador As String
            '            ID_Descripcion

            '            RNI_CodigoRegistroNotaIndicador
            '11:
            'codRegNotaComponenente()
            'codRegNotaIndicador()
            'codRegSubindicador()


            For Each fila As DataRow In tb_listaNotasIndicador.Rows
                onotaIndicador = New notaIndicador
                onotaIndicador.codRegNotaComponenente = Convert.ToInt32(fila("RNC_CodigoRegistroNotaComponente").ToString())
                onotaIndicador.nomIndicador = fila("ID_Descripcion").ToString()
                onotaIndicador.codRegNotaIndicador = Convert.ToInt32(fila("RNI_CodigoRegistroNotaIndicador").ToString())
                onotaIndicador.notaIndicador = fila("RNI_NotaIndicador").ToString()

                onotaIndicador.codRegIndicador = Convert.ToInt32(fila("RI_CodigoRegistroIndicadores").ToString())

                If onotaIndicador.codRegNotaIndicador = 0 Then
                    Continue For
                End If

                If onotaIndicador.codRegNotaIndicador <> codTempIndicador Then
                    lstNotaIndicador.Add(onotaIndicador)
                End If
                codTempIndicador = onotaIndicador.codRegNotaIndicador
            Next
            Return lstNotaIndicador
        Catch ex As Exception

        End Try
    End Function

    Function crearListaSubIndicador(ByVal tb_listaNotasIndicador As DataTable) As List(Of notaSubIndicador)
        Dim lstNotaSubIndicador As New List(Of notaSubIndicador)
        Dim onotaSubIndicador As notaSubIndicador
        Dim tempSubIndicador As String
        Try
            For Each fila In tb_listaNotasIndicador.Rows
                onotaSubIndicador = New notaSubIndicador

                onotaSubIndicador.codRegNotaIndicador = fila("RNI_CodigoRegistroNotaIndicador").ToString()
                onotaSubIndicador.codRegSubindicador = fila("RNSI_CodigoRegistroNotaSubIndicador").ToString()
                onotaSubIndicador.nomRegSubindicador = fila("SI_Descripcion").ToString()
                onotaSubIndicador.notaSubIndicador = fila("RNSI_NotaSubIndicador").ToString()



                'codRegNotaComponenente()
                'codRegNotaIndicador()
                'codRegSubindicador()

                If onotaSubIndicador.codRegSubindicador = 0 Then
                    Continue For
                End If

                If onotaSubIndicador.nomRegSubindicador <> tempSubIndicador Then
                    lstNotaSubIndicador.Add(onotaSubIndicador)
                End If

                tempSubIndicador = onotaSubIndicador.codRegSubindicador
            Next
            Return lstNotaSubIndicador
        Catch ex As Exception

        End Try
    End Function

    Function listaNotasRegistros(ByVal tbl_NotasRegistro As DataTable) As List(Of persona)
        Dim lstPersona As New List(Of persona)
        Dim opersona As persona
        Dim onotaComponente As notaComponente
        Dim onotaIndicador As notaIndicador
        Dim onotaSubIndicador As notaSubIndicador
        Dim codAlumnoTemp As String = ""




        Dim codCompTemp As Integer
        Dim codIndTemp As Integer
        Dim codSubInd As Integer

        Dim lstPersona1 As New List(Of persona)
        Dim lstNotaComponente As New List(Of notaComponente)
        Dim lstNotaIndicador As New List(Of notaIndicador)
        Dim lstNotaSubIndicador As New List(Of notaSubIndicador)


        Dim onotaIndicadorNull As notaIndicador

        Dim nullnotaSubIndicador As notaSubIndicador

        lstPersona1 = crearListaPersona(tbl_NotasRegistro)
        lstNotaComponente = crearListaComponente(tbl_NotasRegistro)
        lstNotaIndicador = crearListaIndicador(tbl_NotasRegistro)
        lstNotaSubIndicador = crearListaSubIndicador(tbl_NotasRegistro)

        For Each op As persona In lstPersona1

            opersona = New persona
            opersona.codAlumnos = op.codAlumnos
            opersona.nombrepersona = op.nombrepersona
            opersona.bimestre = op.bimestre
            opersona.observacionCuro = op.observacionCuro
            opersona.foto = op.foto
            opersona.promedio = op.promedio
            For Each onotaCom As notaComponente In lstNotaComponente
                onotaComponente = New notaComponente
                onotaComponente.codAlumnos = onotaCom.codAlumnos
                onotaComponente.nomComponente = onotaCom.nomComponente
                onotaComponente.codRegNotaComponenente = onotaCom.codRegNotaComponenente
                onotaComponente.notaComponente = onotaCom.notaComponente
                onotaComponente.codRegComponente = onotaCom.codRegComponente

                If opersona.codAlumnos = onotaComponente.codAlumnos Then
                    For Each oNotaInd As notaIndicador In lstNotaIndicador
                        onotaIndicador = New notaIndicador
                        onotaIndicador.nomIndicador = oNotaInd.nomIndicador
                        onotaIndicador.codRegNotaComponenente = oNotaInd.codRegNotaComponenente
                        onotaIndicador.codRegNotaIndicador = oNotaInd.codRegNotaIndicador
                        onotaIndicador.notaIndicador = oNotaInd.notaIndicador

                        onotaIndicador.codRegIndicador = oNotaInd.codRegIndicador
                        If onotaIndicador.codRegNotaComponenente = onotaComponente.codRegNotaComponenente Then
                            For Each onotaSub As notaSubIndicador In lstNotaSubIndicador
                                onotaSubIndicador = New notaSubIndicador
                                onotaSubIndicador.codRegNotaIndicador = onotaSub.codRegNotaIndicador
                                onotaSubIndicador.nomRegSubindicador = onotaSub.nomRegSubindicador
                                onotaSubIndicador.codRegSubindicador = onotaSub.codRegSubindicador
                                onotaSubIndicador.notaSubIndicador = onotaSub.notaSubIndicador

                                onotaSubIndicador.codRegSubindicador = onotaSub.codRegSubindicador
                                If onotaSubIndicador.codRegNotaIndicador = onotaIndicador.codRegNotaIndicador Then
                                    onotaIndicador.lstNotaSubinidcador.Add(onotaSubIndicador)
                                End If


                            Next
                            '    If onotaIndicador.lstNotaSubinidcador.Count = 0 Then

                            '        nullnotaSubIndicador = New notaSubIndicador
                            '        nullnotaSubIndicador.codRegNotaIndicador = 0
                            '        nullnotaSubIndicador.codRegSubindicador = 0
                            '        nullnotaSubIndicador.nomRegSubindicador = ""
                            '        nullnotaSubIndicador.notaSubIndicador = ""
                            '        onotaIndicador.lstNotaSubinidcador.Add(nullnotaSubIndicador)
                            '        'Dim onotaIndicadorNull As notaIndicador

                            '        'Dim nullnotaSubIndicador As notaSubIndicador
                            '    End If

                            onotaComponente.lstNotaIndicador.Add(onotaIndicador)
                        End If


                    Next
                    'If onotaComponente.lstNotaIndicador.Count = 0 Then

                    '    nullnotaSubIndicador = New notaSubIndicador
                    '    nullnotaSubIndicador.codRegNotaIndicador = 0
                    '    nullnotaSubIndicador.codRegSubindicador = 0
                    '    nullnotaSubIndicador.nomRegSubindicador = ""
                    '    nullnotaSubIndicador.notaSubIndicador = ""

                    '    onotaIndicadorNull = New notaIndicador
                    '    onotaIndicadorNull.nomIndicador = ""
                    '    onotaIndicadorNull.notaIndicador = ""
                    '    onotaIndicadorNull.codRegNotaComponenente = 0
                    '    onotaIndicadorNull.codRegNotaIndicador = 0

                    '    onotaIndicadorNull.lstNotaSubinidcador.Add(nullnotaSubIndicador)
                    '    onotaComponente.lstNotaIndicador.Add(onotaIndicadorNull)
                    '    'Dim onotaIndicadorNull As notaIndicador

                    '    'Dim nullnotaSubIndicador As notaSubIndicador

                    'End If



                    opersona.lstNotaComponente.Add(onotaComponente)


                End If

            Next

            lstPersona.Add(opersona)

        Next




        Return lstPersona

    End Function
    Public Function crearListaPersona(ByVal tb_listaNotasIndicador As DataTable) As List(Of persona)
        Try
            Dim lstPersonas As New List(Of persona)

            '            nombreCompleto

            '            AL_CodigoAlumno
            Dim opersona As persona
            Dim codTempCodPersona As String

            For Each fila As DataRow In tb_listaNotasIndicador.Rows
                opersona = New persona
                opersona.promedio = fila("RNBL_NotaFinalBimestre").ToString()
                opersona.codAlumnos = fila("AL_CodigoAlumno").ToString()
                opersona.nombrepersona = fila("nombreCompleto").ToString()
                opersona.bimestre = fila("RNBL_CodigoRegistroBimestralL").ToString()
                opersona.observacionCuro = fila("RNBL_ObservacionCurso").ToString()
                opersona.foto = fila("AL_RutaFoto").ToString()


                If fila("AL_CodigoAlumno") <> codTempCodPersona Then



                    lstPersonas.Add(opersona)
                End If
                codTempCodPersona = opersona.codAlumnos

            Next

            Return lstPersonas
        Catch ex As Exception

        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        'Dim obl_RegistroNotasComponentes As New bl_RegistroNotasComponentes
        'Dim dtsRegistroNotasAlumnos As New DataSet
        'dtsRegistroNotasAlumnos = obl_RegistroNotasComponentes.FUN_lIS_CU_ListarNotasComponenteIndicadorSubindicador(int_CodigoBimestre, int_CodigoAsignacionGrupo)
        'Dim lPersona As New List(Of persona)
        'lPersona = listaNotasRegistros(dtsRegistroNotasAlumnos.Tables(0))

        Dim dtCursos As New DataTable
        Dim dtNotasCurso As New DataTable
        Dim dtAsistencias As New DataTable
        Dim obl_CursoPromedio As New bl_CursoPromedio
        Dim dstCursosNotas As DataSet = New bl_CursoPromedio().fnListarCursoPromedio(2, 20100141)
        Dim lstCadenaLinbreta As String = ""



        dtCursos = dstCursosNotas.Tables(0)
        dtNotasCurso = dstCursosNotas.Tables(1)
        dtAsistencias = dstCursosNotas.Tables(2)
        
        'Select _
        '           nombreCompleto = i.Field(Of String)("nombreCompleto"), _
        '           AL_RutaFoto = i.Field(Of String)("AL_RutaFoto") _
        '           , id = i.Field(Of String)("id")


        Dim lstcursoLibretaPadre As New List(Of cursoLibretaPadre)

        lstcursoLibretaPadre = crearListaCurso(dtCursos)

        Dim lstcursoLibretaPadreContexto As New List(Of cursoLibretaPadre)

        lstcursoLibretaPadreContexto = crearListaNotasBimestre(dtNotasCurso, lstcursoLibretaPadre)


        ''  Dim jsLstcursoLibretaPadreContexto = From j In lstcursoLibretaPadreContexto order j.lstNotaCurso. Select j

        listaJsonLibreta = New System.Web.Script.Serialization.JavaScriptSerializer().Serialize(lstcursoLibretaPadreContexto)




        Dim sql = From g In dtAsistencias.AsEnumerable() _
                  Select _
                    TardanzaJustificada1 = g.Field(Of Integer)("1TardanzaJustificada"), _
                    TardanzaSinJustificar1 = g.Field(Of Integer)("1TardanzaSinJustificar"), _
                    FaltaJustificada1 = g.Field(Of Integer)("1FaltaJustificada"), _
                    FaltaSinJustificar1 = g.Field(Of Integer)("1FaltaSinJustificar"), _
                    TardanzaJustificada2 = g.Field(Of Integer)("2TardanzaJustificada"), _
                    TardanzaSinJustificar2 = g.Field(Of Integer)("2TardanzaSinJustificar"), _
                    FaltaJustificada2 = g.Field(Of Integer)("2FaltaJustificada"), _
                    FaltaSinJustificar2 = g.Field(Of Integer)("2FaltaSinJustificar"), _
                    TardanzaJustificada3 = g.Field(Of Integer)("3TardanzaJustificada"), _
                    TardanzaSinJustificar3 = g.Field(Of Integer)("3TardanzaSinJustificar"), _
                    FaltaJustificada3 = g.Field(Of Integer)("3FaltaJustificada"), _
                    FaltaSinJustificar3 = g.Field(Of Integer)("3FaltaSinJustificar"), _
                    TardanzaJustificada4 = g.Field(Of Integer)("4TardanzaJustificada"), _
                    TardanzaSinJustificar4 = g.Field(Of Integer)("4TardanzaSinJustificar"), _
                    FaltaJustificada4 = g.Field(Of Integer)("4FaltaJustificada"), _
                    FaltaSinJustificar4 = g.Field(Of Integer)("4FaltaSinJustificar")


        jsAsistensias = New JavaScriptSerializer().Serialize(sql)



    End Sub

    Public Class asistencias




    End Class
    Function crearListaCurso(ByVal dt_curso As DataTable) As List(Of cursoLibretaPadre)
        Dim lstCursoLibreta As New List(Of cursoLibretaPadre)
        Dim ocursoLibretaPadre As cursoLibretaPadre
        Try


            'NC_Descripcion(CS_CodigoCurso)
            'Español(2)


            For Each filaCursoLibreta As DataRow In dt_curso.Rows
                ocursoLibretaPadre = New cursoLibretaPadre

                ocursoLibretaPadre.codCurso = filaCursoLibreta("CS_CodigoCurso").ToString()
                ocursoLibretaPadre.nombreCurso = filaCursoLibreta("NC_Descripcion").ToString()



                lstCursoLibreta.Add(ocursoLibretaPadre)
            Next
        Catch ex As Exception
        Finally

        End Try
        Return lstCursoLibreta
    End Function


    Function crearListaNotasBimestre(ByVal dt_notasBimestre As DataTable, ByVal lstCursoLibretaPadre As List(Of cursoLibretaPadre)) As List(Of cursoLibretaPadre)
        Dim lstnotaCurso As New List(Of notaCurso)
        Dim onotaCurso As notaCurso
        Dim lstcursoLibretaPadreRet As New List(Of cursoLibretaPadre)
        For Each ocursoLibretaPadre As cursoLibretaPadre In lstCursoLibretaPadre
            For Each filaNotas As DataRow In dt_notasBimestre.Rows
                onotaCurso = New notaCurso
                onotaCurso.codBimestre = filaNotas("BM_CodigoBimestre").ToString()
                onotaCurso.codCurso = filaNotas("CS_CodigoCurso").ToString()
                onotaCurso.notaCurso = filaNotas("RNBL_NotaFinalBimestre").ToString()
                ocursoLibretaPadre.notaAnual = filaNotas("RNAL_NotaAnual").ToString()
                If ocursoLibretaPadre.codCurso = onotaCurso.codCurso Then
                    ocursoLibretaPadre.lstNotaCurso.Add(onotaCurso)
                End If

            Next
            lstcursoLibretaPadreRet.Add(ocursoLibretaPadre)
        Next
        '        NC_Descripcion	RNBL_NotaFinalBimestre	BM_CodigoBimestre	AL_CodigoAlumno	CS_CodigoCurso	RNAL_NotaAnual	GD_CodigoGrado	AC_CodigoAnioAcademico
        'Español	A	1	20100141	2	NULL	5	2
        Return lstcursoLibretaPadreRet
    End Function
    Public Class cursoLibretaPadre
        Public nombreCurso As String
        Public codCurso As String
        Public notaAnual As String
        Public lstNotaCurso As New List(Of notaCurso)


    End Class

    Public Class notaCurso
        Public codCurso As String
        Public notaCurso As String
        Public codBimestre As String


    End Class
End Class


