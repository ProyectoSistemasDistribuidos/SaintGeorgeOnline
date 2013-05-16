Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports SaintGeorgeOnline_BusinessEntities.ModuloNotas
Imports SaintGeorgeOnline_BusinessEntities
Public Class DA_CrearEstructuraNotasOficiales
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


    Public Function insertarAsignacionGrupo(ByVal dtListas As DataTable, ByVal codBimestre As Integer, ByVal pcodAula As Integer, ByVal pcodAnio As Integer) As Integer
        ''


        Dim codigo As Integer = 0
        Dim codigo1 As Integer = 0
        Dim codigo2 As Integer = 0

        'Dim queryObject = From aulas In dtListas.AsEnumerable() Group aulas By _
        '                  codAulas = aulas("AAP_CodigoAsignacionAula") _
        '                  Into Aulas = Group _
        '                    Select New With {.codAula = codAulas, _
        '              .grupos = (From grupos In Aulas Group grupos By codGrupos = grupos("AGC_CodigoAsignacionGrupo") _
        '                         Into grupos = Group _
        '                    Select New With _
        '                    {.codGrupo = codGrupos, _
        '                 .alumnos = (From alumnos In grupos Group alumnos By codAlumno = alumnos("AL_CodigoAlumno") _
        '                             Into _
        '        alumnos = Group Select New With _
        '        {.codAlumno = codAlumno, .gruposCursoPadre = (From cursos In alumnos Group cursos By _
        '                                                      notas = cursos("ACA_CodigoAsignacionCursoPadre") _
        '             Into cursosLista = Group _
        '                    Select New With {.codCursoPadre = notas, .notasFinal = (From notasBim In cursosLista Select New With _
        '        {.notaBim = notasBim("RNBL_NotaFinalBimestre")})})})})}



        Dim queryObject = From aulas In dtListas.AsEnumerable() Group aulas By _
                            codAulas = aulas("AAP_CodigoAsignacionAula") _
                            Into Aulas = Group _
                            Select New With {.codAula = codAulas, _
                            .alumnos = (From alumnos In Aulas Group alumnos By codAlumno = alumnos("AL_CodigoAlumno") _
                            Into _
                            alumnos = Group Select New With {.codAlumno = codAlumno, _
                            .gruposCursoPadre = (From cursos In alumnos Group cursos By _
                            notas = cursos("ACA_CodigoAsignacionCursoPadre") _
                            Into cursosLista = Group Select New With { _
                            .codCursoPadre = notas, _
                            .notasFinal = (From notasBim In cursosLista Select New With _
                            {.notaBim = notasBim("RNBL_NotaFinalBimestre")}) _
                })})}


        ' Exit Function
        ''-------------------------------------------------------------------------
        Try
            BeginTransaction()


            For Each oaulas In queryObject

                ' For Each ogrupo In oaulas.grupos

                For Each oalumno In oaulas.alumnos

                    For Each ogrupoCursoPadre In oalumno.gruposCursoPadre

                        ''
                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsAsignacionGrupo")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@ACA_CodigoAsignacionCurso", DbType.Int32, ogrupoCursoPadre.codCursoPadre)
                        dbBase.AddInParameter(dbCommand, "@AAP_CodigoAsignacionAula", DbType.Int32, oaulas.codAula)
                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)
                        dbBase.ExecuteScalar(dbCommand, tran)


                        codigo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeI As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                        ''

                        dbCommand = dbBase.GetStoredProcCommand("USP_InsRegistroNotaAnualCualitativa")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@AGC_CodigoAsignacionGrupo", DbType.Int32, codigo)
                        dbBase.AddInParameter(dbCommand, "@AC_CodigoAnioAcademico", DbType.Int16, pcodAnio)
                        dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.Int32, oalumno.codAlumno)

                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)

                        dbBase.ExecuteScalar(dbCommand, tran)
                        codigo1 = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensaje As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                        Dim contadoPuntajes As Integer = 0

                        Dim cantidadNotas As Integer = 0
                        Dim notaLetras As String = ""

                        Dim esNull As Boolean = False

                        For Each notas In ogrupoCursoPadre.notasFinal
                            cantidadNotas = ogrupoCursoPadre.notasFinal.Where(Function(nota) nota.notaBim.ToString <> "").Count

                            'If ogrupoCursoPadre.notasFinal.Contains(
                            'If cantidadNotas = 3 Then
                            'If notas.notaBim = "" Then
                            '    esNull = True
                            '    Exit For
                            'End If
                            If notas.notaBim.ToUpper() = "AD" Then
                                contadoPuntajes += 4
                            ElseIf notas.notaBim.ToUpper() = "A" Then
                                contadoPuntajes += 3
                            ElseIf notas.notaBim.ToUpper() = "B" Then
                                contadoPuntajes += 2
                            ElseIf notas.notaBim.ToUpper() = "C" Then
                                contadoPuntajes += 1
                            End If
                            'ElseIf cantidadNotas = 3 Then
                            'End If
                        Next
                        ''
                        If cantidadNotas = 3 Then
                            If contadoPuntajes >= 11 And contadoPuntajes <= 12 Then
                                notaLetras = "AD"
                            ElseIf contadoPuntajes >= 8 And contadoPuntajes <= 10 Then
                                notaLetras = "A"
                            ElseIf contadoPuntajes >= 5 And contadoPuntajes <= 7 Then
                                notaLetras = "B"
                            ElseIf contadoPuntajes >= 0 And contadoPuntajes <= 4 Then
                                notaLetras = "C"
                            End If
                        ElseIf cantidadNotas = 2 Then
                            ''
                            If contadoPuntajes >= 7 And contadoPuntajes <= 8 Then
                                notaLetras = "AD"
                            ElseIf contadoPuntajes >= 5 And contadoPuntajes <= 6 Then
                                notaLetras = "A"
                            ElseIf contadoPuntajes >= 3 And contadoPuntajes <= 4 Then
                                notaLetras = "B"
                            ElseIf contadoPuntajes >= 0 And contadoPuntajes <= 2 Then
                                notaLetras = "C"
                            End If
                        ElseIf cantidadNotas = 1 Then
                            notaLetras = ogrupoCursoPadre.notasFinal(0).notaBim.ToString()
                            ''
                        End If



                        ''------------------------------------------
                        dbCommand = dbBase.GetStoredProcCommand("USP_InsNotaBimestral")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@BM_CodigoBimestre", DbType.Int32, codBimestre)
                        dbBase.AddInParameter(dbCommand, "@RNAL_CodigoRegistroAnualL", DbType.Int32, codigo1)

                        If cantidadNotas = 0 Then
                            dbBase.AddInParameter(dbCommand, "@RNBL_NotaFinalBimestre", DbType.String, "")
                        Else
                            dbBase.AddInParameter(dbCommand, "@RNBL_NotaFinalBimestre", DbType.String, notaLetras)
                        End If

                       





                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)

                        dbBase.ExecuteScalar(dbCommand, tran)
                        codigo2 = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeII As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))

                        ''


                    Next

                Next

            Next

            

            If codigo = 0 Then
                Rollback()
            Else
                Commit()
            End If
            Return codigo
            ''
        Catch ex As Exception
            Rollback()
            Return 0
        End Try
    End Function
    
#Region "generar conducta Primaria"
    Public Function insertarAsignacionGrupoConductaPrimaria(ByVal dtListas As DataTable, ByVal codAnio As Integer) As Integer
        ''


        Dim codigo1 As Integer = 0
        Dim codigo2 As Integer = 0
        Dim codigo3 As Integer = 0



        Dim query = From sql In dtListas.AsEnumerable() _
                    Group sql By CodGrupo = sql("ACA_CodigoAsignacionCurso") Into Grupos = Group _
                    Select New With {.codGrupo = CodGrupo, _
                                     .aulas = (From aula In Grupos.AsEnumerable() Group aula By codAula = aula("AAP_CodigoAsignacionAula") _
                                              Into Aulas = Group _
                                              Select New With { _
                                              .codAulas = codAula, _
                                              .alumnos = (From alumno In Aulas.AsEnumerable() Group alumno By CodAlumno = alumno("AL_CodigoAlumno") _
                                                         Into alumnos = Group _
                                                         Select New With { _
                                                         .CodAlumno = CodAlumno, _
                                                         .notas = (From nota In alumnos.AsEnumerable() Select New With { _
                                                                  .nota = nota("RCB_NotaBimestralCualitativa"), _
                                                                  .codBimestre = nota("BM_CodigoBimestre")})})})}




        Try
            BeginTransaction()
            For Each grupo In query
                For Each aula In grupo.aulas
                    For Each oalumno In aula.alumnos
                        ''
                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsAsignacionGrupo")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@ACA_CodigoAsignacionCurso", DbType.Int32, grupo.codGrupo)
                        dbBase.AddInParameter(dbCommand, "@AAP_CodigoAsignacionAula", DbType.Int32, aula.codAulas)
                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        codigo1 = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeI As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                        ''
                        dbCommand = dbBase.GetStoredProcCommand("USP_InsRegistroNotaAnualCualitativa")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@AGC_CodigoAsignacionGrupo", DbType.Int32, codigo1)
                        dbBase.AddInParameter(dbCommand, "@AC_CodigoAnioAcademico", DbType.Int16, codAnio)
                        dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.Int32, oalumno.CodAlumno)

                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int16, 10)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)

                        dbBase.ExecuteScalar(dbCommand, tran)
                        codigo2 = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensaje As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                        Dim contadoPuntajes As Integer = 0
                        Dim cantidadNotas As Integer = 0
                        Dim notaLetras As String = ""
                        ''------------------------------------------
                        For Each notas In oalumno.notas
                            dbCommand = dbBase.GetStoredProcCommand("USP_InsNotaBimestral")
                            dbCommand.Parameters.Clear()
                            dbBase.AddInParameter(dbCommand, "@BM_CodigoBimestre", DbType.Int32, notas.codBimestre)
                            dbBase.AddInParameter(dbCommand, "@RNAL_CodigoRegistroAnualL", DbType.Int32, codigo2)

                            dbBase.AddInParameter(dbCommand, "@RNBL_NotaFinalBimestre", DbType.String, notas.nota)

                            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)

                            dbBase.ExecuteScalar(dbCommand, tran)
                            codigo3 = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                            Dim mensajeII As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                        Next
                        ''
                    Next

                Next

            Next




            If codigo3 = 0 Then
                Rollback()
            Else
                Commit()
            End If
            ''
        Catch ex As Exception
            Rollback()
        End Try
        Return codigo3
    End Function
#End Region

#Region "Generar conduncta secudaria"
    Public Function insertarAsignacionGrupoConductaSecundaria(ByVal dtListas As DataTable, ByVal codAnio As Integer) As Integer
        ''


        Dim codigoGrupo As Integer = 0
        Dim codGrupoAnual As Integer = 0
        Dim codBimestre As Integer = 0



        Dim query = From sql In dtListas.AsEnumerable() _
                    Group sql By CodGrupo = sql("ACA_CodigoAsignacionCurso") Into Grupos = Group _
                    Select New With {.codGrupo = CodGrupo, _
                                     .aulas = (From aula In Grupos.AsEnumerable() Group aula By codAula = aula("AAP_CodigoAsignacionAula") _
                                              Into Aulas = Group _
                                              Select New With { _
                                              .codAulas = codAula, _
                                              .alumnos = (From alumno In Aulas.AsEnumerable() Group alumno By CodAlumno = alumno("AL_CodigoAlumno") _
                                                         Into alumnos = Group _
                                                         Select New With { _
                                                         .CodAlumno = CodAlumno, _
                                                         .notas = (From nota In alumnos.AsEnumerable() Select New With { _
                                                                  .nota = nota("RCB_NotaBimestralCualitativa"), _
                                                                  .codBimestre = nota("BM_CodigoBimestre")})})})}




        Try
            BeginTransaction()
            For Each grupo In query
                For Each aula In grupo.aulas
                    For Each oalumno In aula.alumnos
                        ''

                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsCU_AsignacionGruposCursos")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@ACA_CodigoAsignacionCurso", DbType.Int32, grupo.codGrupo)
                        dbBase.AddInParameter(dbCommand, "@AAP_CodigoAsignacionAula", DbType.Int32, aula.codAulas)

                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        codigoGrupo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeI As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))


                        ''
                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_insCU_RegistroNotasAnualCuantitativo")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@AGC_CodigoAsignacionGrupo", DbType.Int32, codigoGrupo)
                        dbBase.AddInParameter(dbCommand, "@AC_CodigoAnioAcademico", DbType.Int32, codAnio)
                        dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.Int32, oalumno.CodAlumno)


                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        codGrupoAnual = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeII As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))

                        Dim contadoPuntajes As Integer = 0
                        Dim cantidadNotas As Integer = 0
                        Dim notaLetras As String = ""
                        ''------------------------------------------
                        For Each notas In oalumno.notas


                            dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsCU_RegistroNotasBimestralesCuantitativas")
                            dbCommand.Parameters.Clear()
                            dbBase.AddInParameter(dbCommand, "@BM_CodigoBimestre", DbType.Int32, notas.codBimestre)
                            dbBase.AddInParameter(dbCommand, "@RNAT_CodigoRegistroAnualT", DbType.Int32, codGrupoAnual)
                            'If CInt(notas.nota) <> 0 Then
                            '    If CInt(notas.nota) >= 17 And CInt(notas.nota) <= 20 Then
                            '        dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "AD")
                            '    ElseIf CInt(notas.nota) >= 13 And CInt(notas.nota) <= 16 Then
                            '        dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "A")
                            '    ElseIf CInt(notas.nota) >= 11 And CInt(notas.nota) <= 12 Then
                            '        dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "B")
                            '    ElseIf CInt(notas.nota) >= 0 And CInt(notas.nota) <= 10 Then
                            '        dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "C")
                            '    End If
                            'Else
                            dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "")
                            'End If
                            dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestre", DbType.Decimal, notas.nota)
                            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                                dbBase.ExecuteScalar(dbCommand, tran)
                                codBimestre = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                                Dim mensajeBimestreNota As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))



                        Next
                        ''
                    Next

                Next

            Next




            If codBimestre = 0 Then
                Rollback()
            Else
                Commit()
            End If
            ''
        Catch ex As Exception
            Rollback()
        End Try
        Return codBimestre
    End Function


#End Region
#Region "Generar conduncta Sexto primaria"
    Public Function insertarAsignacionGrupoConductaSecundariaSexto(ByVal dtListas As DataTable, ByVal codAnio As Integer) As Integer
        ''


        Dim codigoGrupo As Integer = 0
        Dim codGrupoAnual As Integer = 0
        Dim codBimestre As Integer = 0



        Dim query = From sql In dtListas.AsEnumerable() _
                    Group sql By CodGrupo = sql("ACA_CodigoAsignacionCurso") Into Grupos = Group _
                    Select New With {.codGrupo = CodGrupo, _
                                     .aulas = (From aula In Grupos.AsEnumerable() Group aula By codAula = aula("AAP_CodigoAsignacionAula") _
                                              Into Aulas = Group _
                                              Select New With { _
                                              .codAulas = codAula, _
                                              .alumnos = (From alumno In Aulas.AsEnumerable() Group alumno By CodAlumno = alumno("AL_CodigoAlumno") _
                                                         Into alumnos = Group _
                                                         Select New With { _
                                                         .CodAlumno = CodAlumno, _
                                                         .notas = (From nota In alumnos.AsEnumerable() Select New With { _
                                                                  .nota = nota("RCB_NotaBimestralCualitativa"), _
                                                                  .codBimestre = nota("BM_CodigoBimestre")})})})}




        Try
            BeginTransaction()
            For Each grupo In query
                For Each aula In grupo.aulas
                    For Each oalumno In aula.alumnos
                        ''

                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsCU_AsignacionGruposCursos")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@ACA_CodigoAsignacionCurso", DbType.Int32, grupo.codGrupo)
                        dbBase.AddInParameter(dbCommand, "@AAP_CodigoAsignacionAula", DbType.Int32, aula.codAulas)

                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        codigoGrupo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeI As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))


                        ''
                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_insCU_RegistroNotasAnualCuantitativo")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@AGC_CodigoAsignacionGrupo", DbType.Int32, codigoGrupo)
                        dbBase.AddInParameter(dbCommand, "@AC_CodigoAnioAcademico", DbType.Int32, codAnio)
                        dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.Int32, oalumno.CodAlumno)


                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        codGrupoAnual = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeII As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))

                        Dim contadoPuntajes As Integer = 0
                        Dim cantidadNotas As Integer = 0
                        Dim notaLetras As String = ""
                        ''------------------------------------------
                        For Each notas In oalumno.notas


                            dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsCU_RegistroNotasBimestralesCuantitativas")
                            dbCommand.Parameters.Clear()
                            dbBase.AddInParameter(dbCommand, "@BM_CodigoBimestre", DbType.Int32, notas.codBimestre)
                            dbBase.AddInParameter(dbCommand, "@RNAT_CodigoRegistroAnualT", DbType.Int32, codGrupoAnual)


                            If CInt(notas.nota) <> 0 Then
                                If CInt(notas.nota) >= 17 And CInt(notas.nota) <= 20 Then
                                    dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "AD")
                                ElseIf CInt(notas.nota) >= 13 And CInt(notas.nota) <= 16 Then
                                    dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "A")
                                ElseIf CInt(notas.nota) >= 11 And CInt(notas.nota) <= 12 Then
                                    dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "B")
                                ElseIf CInt(notas.nota) >= 0 And CInt(notas.nota) <= 10 Then
                                    dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "C")
                                End If
                            Else
                                dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "")
                            End If


                            dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestre", DbType.Decimal, 0)
                            dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 100)
                            dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 100)
                            dbBase.ExecuteScalar(dbCommand, tran)
                            codBimestre = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                            Dim mensajeBimestreNota As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))



                        Next
                        ''
                    Next

                Next

            Next




            If codBimestre = 0 Then
                Rollback()
            Else
                Commit()
            End If
            ''
        Catch ex As Exception
            Rollback()
        End Try
        Return codBimestre
    End Function


#End Region
    Public Function FInsertarMatrizGradoSexotGenerarNotasFinal(ByVal dtMatriz As DataTable, ByVal codBimestre As Integer, ByVal codAnioAcademico As Integer) As Boolean
        Try
            Dim codigoGrupo As Integer = 0
            Dim codGrupoAnual As Integer = 0
            Dim codBimestreNotas As Integer = 0
            Dim queryObject = From ctx In dtMatriz.AsEnumerable() Group ctx By CodAulas = ctx("AAP_CodigoAsignacionAula") _
                          Into lstAulas = Group _
                          Select New With { _
                          .codAula = CodAulas, _
        .lstAlumnos = (From alumnos In lstAulas Group alumnos By codAlumno = alumnos("AL_CodigoAlumno") _
                       Into lstAlumnos = Group _
                       Select New With {.codAlumno = codAlumno, _
                                  .lstCursos = (From cursos In lstAlumnos Group cursos By codPadre = cursos("codOficialCuro") _
                                              Into lstCursos = Group _
                                              Select New With { _
      .codOficial = codPadre, .notas = (From nts In lstCursos Select New _
                                      With { _
                                      .notaFinal = nts("RNBT_NotaFinalBimestre")})})}) _
                          }
            BeginTransaction()
            For Each oaula In queryObject
                For Each alumno In oaula.lstAlumnos
                    For Each ocurso In alumno.lstCursos
                        ''
                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsCU_AsignacionGruposCursos")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@ACA_CodigoAsignacionCurso", DbType.Int32, ocurso.codOficial)
                        dbBase.AddInParameter(dbCommand, "@AAP_CodigoAsignacionAula", DbType.Int32, oaula.codAula)

                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        codigoGrupo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeI As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))


                        ''
                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_insCU_RegistroNotasAnualCuantitativo")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@AGC_CodigoAsignacionGrupo", DbType.Int32, codigoGrupo)
                        dbBase.AddInParameter(dbCommand, "@AC_CodigoAnioAcademico", DbType.Int32, codAnioAcademico)
                        dbBase.AddInParameter(dbCommand, "@AL_CodigoAlumno", DbType.Int32, alumno.codAlumno)


                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        codGrupoAnual = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeII As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                        ''
                        Dim acNotas As Integer = 0
                        Dim notasLetras As String = ""
                        Dim estaVacio As Boolean = False

                        Dim cantidadNotas As Integer = 0
                        For Each onotas In ocurso.notas
                            cantidadNotas = ocurso.notas.Count
                            If onotas.notaFinal = 0 Then
                                estaVacio = True
                                Exit For
                            End If
                            acNotas += onotas.notaFinal

                        Next

                        Dim promedioFinalOficial As Decimal = 0.0
                        promedioFinalOficial = acNotas / cantidadNotas
                        acNotas = promedioFinalOficial


                        If acNotas >= 0 And acNotas <= 8 Then
                            notasLetras = "C"
                        ElseIf acNotas >= 9 And acNotas <= 10 Then
                            notasLetras = "B"
                        ElseIf acNotas >= 11 And acNotas <= 16 Then
                            notasLetras = "A"
                        ElseIf acNotas >= 17 And acNotas <= 20 Then
                            notasLetras = "AD"
                        End If



                        dbCommand = Me.dbBase.GetStoredProcCommand("USP_InsCU_RegistroNotasBimestralesCuantitativas")
                        dbCommand.Parameters.Clear()
                        dbBase.AddInParameter(dbCommand, "@BM_CodigoBimestre", DbType.Int32, codBimestre)
                        dbBase.AddInParameter(dbCommand, "@RNAT_CodigoRegistroAnualT", DbType.Int32, codGrupoAnual)

                        If estaVacio Then
                            dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, "")
                        Else
                            dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestreLetra", DbType.String, notasLetras)

                        End If
                        dbBase.AddInParameter(dbCommand, "@RNBT_NotaFinalBimestre", DbType.Decimal, 0)

                        dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                        dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)
                        dbBase.ExecuteScalar(dbCommand, tran)
                        codBimestreNotas = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                        Dim mensajeBimestreNota As String = CStr(dbBase.GetParameterValue(dbCommand, "@mensaje"))
                        ''
                        ''----------------------------


                        'create procedure 
                        '@BM_CodigoBimestre int ,
                        '@RNAT_CodigoRegistroAnualT int ,
                        '@RNBT_NotaFinalBimestreLetra varchar(max),
                        '@codigo int out ,  
                        '@mensaje varchar(max) out  



                    Next

                Next
            Next


            If codigoGrupo = 0 Or codGrupoAnual = 0 Or codBimestreNotas = 0 Then
                Rollback()
                Return False
            Else
                Commit()
                Return True
            End If



        Catch ex As Exception
            Rollback()
        End Try
    End Function



#End Region

#Region "funciones calculo de notas "
    Public Function f_actualizarNotaFinalPrimaria(ByVal dtNotaFinal As DataTable)
        Try

            Dim codigo As Integer = 0
            Dim mensaje As String = ""

            Dim sqlObject = From sql In dtNotaFinal.AsEnumerable() Group sql By codNotaAnual = sql("RNAL_CodigoRegistroAnualL") Into lstNotas = Group _
                            Select New With {.codNotaFinal = codNotaAnual, _
                                             .notas = (From sqlNotas In lstNotas.AsEnumerable() Where sqlNotas("RNBL_NotaFinalBimestre") <> "" Order By sqlNotas("BM_CodigoBimestre") Descending _
                                                       Select New With {.codBime = sqlNotas("BM_CodigoBimestre"), .nota = sqlNotas("RNBL_NotaFinalBimestre")}).Take(1)}

            BeginTransaction()
            For Each codNotaFinal In sqlObject


                Dim contadoPuntajes As Integer = 0
                Dim cantidadNotas As Integer = 0
                Dim notaLetras As String = ""


                For Each notasBimestre In codNotaFinal.notas
                    'If notasBimestre.nota.ToUpper() = "AD" Then
                    '    contadoPuntajes += 4
                    'ElseIf notasBimestre.nota.ToUpper() = "A" Then
                    '    contadoPuntajes += 3
                    'ElseIf notasBimestre.nota.ToUpper() = "B" Then
                    '    contadoPuntajes += 2
                    'ElseIf notasBimestre.nota.ToUpper() = "C" Then
                    '    contadoPuntajes += 1
                    'End If
                    dbCommand = Me.dbBase.GetStoredProcCommand("USP_ActualizarNotaFinalPrimaria")
                    dbCommand.Parameters.Clear()
                    dbBase.AddInParameter(dbCommand, "@RNAL_CodigoRegistroAnualL", DbType.Int32, codNotaFinal.codNotaFinal)
                    dbBase.AddInParameter(dbCommand, "@RNAL_NotaAnual", DbType.String, notasBimestre.nota)

                    dbBase.AddOutParameter(dbCommand, "@codigo", DbType.Int32, 10)
                    dbBase.AddOutParameter(dbCommand, "@mensaje", DbType.String, 10)

                    dbBase.ExecuteScalar(dbCommand, tran)
                    codigo = CInt(CStr(dbBase.GetParameterValue(dbCommand, "@codigo")))
                    mensaje = (CStr(dbBase.GetParameterValue(dbCommand, "@mensaje")))
                    If codigo = 0 Then
                        Dim err As String = "errror"
                    End If
                Next

                'If contadoPuntajes >= 11 And contadoPuntajes <= 12 Then
                '    notaLetras = "AD"
                'ElseIf contadoPuntajes >= 8 And contadoPuntajes <= 10 Then
                '    notaLetras = "A"
                'ElseIf contadoPuntajes >= 5 And contadoPuntajes <= 7 Then
                '    notaLetras = "B"
                'ElseIf contadoPuntajes >= 0 And contadoPuntajes <= 4 Then
                '    notaLetras = "C"
                'End If


              
            Next

            If codigo = 0 Then
                Rollback()

            Else
                Commit()
            End If
            Return codigo

        Catch ex As Exception
            Rollback()
        End Try
    End Function
#End Region

#Region "no transaccional"





#End Region
End Class
