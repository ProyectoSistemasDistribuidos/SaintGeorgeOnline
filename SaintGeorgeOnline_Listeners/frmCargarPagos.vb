Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones

Public Class frmCargarPagos

    Private dt_Excel As New DataTable
    Private dt_Conceptos As New DataTable
    Private dt_DB As New DataTable

    Private Sub frmCargarPagos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnCargarConcepto.Enabled = False
        Me.Left = 0
        Me.Top = 0

    End Sub

    Private Sub btnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargar.Click

        Dim XLSPath As String = ""

        With OpenFileDialog1
            .Title = "Excel Spreadsheet"
            .FileName = ""
            .DefaultExt = ".xls"
            .AddExtension = True
            .Filter = "Excel Worksheets|*.xls; *.xlsx"

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                XLSPath = (CType(.FileName, String))
                If (XLSPath.Length) <> 0 Then

                    tbRuta.Text = XLSPath

                    Try
                        Dim sheet As String = "sistemas - enero"
                        Dim worksheet As DataTable = obtenerDatosHojaExcel(sheet, XLSPath)
                        DataGridView1.DataSource = worksheet
                        DataGridView1.AutoResizeColumns()

                        dt_Excel = worksheet
                        dt_Conceptos = crearTablaConceptos(dt_Conceptos)

                        Dim dr_Aux As DataRow

                        Dim str_Concepto As String = ""
                        Dim str_IndiceCelda As String = ""
                        Dim int_Idx As Integer = 0

                        For Each dr As DataRow In worksheet.Rows
                            str_IndiceCelda = dr.Item(0).ToString
                            If str_IndiceCelda.Length = 0 Then
                                Continue For
                            Else
                                If str_IndiceCelda.Length > 5 Then
                                    If str_IndiceCelda.ToUpper.Substring(0, 5) = "TOTAL" Then
                                        Continue For
                                    End If
                                    If str_IndiceCelda.ToUpper.Substring(2, 1) <> "-" Then ' Concepto
                                        str_Concepto = str_IndiceCelda.ToUpper
                                        int_Idx += 1
                                        dr_Aux = dt_Conceptos.NewRow
                                        dr_Aux.Item("idx") = int_Idx
                                        dr_Aux.Item("concepto") = str_Concepto
                                        dt_Conceptos.Rows.Add(dr_Aux)
                                        Continue For
                                    End If
                                End If
                            End If
                        Next

                        cboConceptos.DataSource = dt_Conceptos
                        cboConceptos.ValueMember = "idx"
                        cboConceptos.DisplayMember = "concepto"

                        btnCargarConcepto.Enabled = True

                    Catch ex As Exception
                        MsgBox("Error: " & ex.Message)
                    End Try

                Else
                    MsgBox("El archvio no existe.")
                End If
            End If
        End With

    End Sub

    Private Sub btnCargarConcepto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargarConcepto.Click

        Dim int_RowIdx As Integer = 0

        Try

            Dim dt_Aux As New DataTable
            dt_Aux = crearTablaConsolidado(dt_Aux)
            Dim dr_Aux As DataRow

            Dim str_Concepto As String = ""
            Dim str_ConceptoSeleccionado As String = cboConceptos.Text

            Dim str_IndiceCelda As String = ""
            Dim int_Idx As Integer = 0

            For Each dr As DataRow In dt_Excel.Rows
                int_RowIdx += 1
                str_IndiceCelda = dr.Item(0).ToString
                If str_IndiceCelda.Length = 0 Then
                    Continue For
                Else
                    If str_IndiceCelda.Length > 5 Then
                        If str_IndiceCelda.ToUpper.Substring(0, 5) = "TOTAL" Then
                            Continue For
                        End If
                        If str_IndiceCelda.ToUpper.Substring(2, 1) <> "-" Then ' Concepto
                            str_Concepto = str_IndiceCelda.ToUpper
                            Continue For
                        Else ' Registro
                            If str_Concepto <> str_ConceptoSeleccionado Then
                                Continue For
                            Else
                                int_Idx += 1
                                dr_Aux = dt_Aux.NewRow
                                dr_Aux.Item("idx") = int_Idx
                                dr_Aux.Item("concepto") = str_Concepto
                                'dr_Aux.Item("comprobante") = dr.Item(0)
                                dr_Aux.Item("tal") = dr.Item(0).ToString.Substring(0, 2)
                                dr_Aux.Item("numero") = dr.Item(0).ToString.Substring(3, dr.Item(0).ToString.Length - 3)
                                dr_Aux.Item("periodo") = dr.Item(1)
                                'dr_Aux.Item("anio") = dr.Item(1).ToString.Substring(0, 4)
                                'dr_Aux.Item("mes") = dr.Item(1).ToString.Substring(5, 3)
                                dr_Aux.Item("origen") = dr.Item(2)
                                dr_Aux.Item("codigo") = dr.Item(4)

                                If dr.Item(5).ToString.Substring(0, 1) = "A" Then ' Anulado
                                    dr_Aux.Item("fechapago") = DBNull.Value
                                    dr_Aux.Item("anulado") = "SI"
                                Else ' Fecha
                                    dr_Aux.Item("fechapago") = dr.Item(5)
                                    dr_Aux.Item("anulado") = "NO"
                                End If

                                dr_Aux.Item("alumno") = dr.Item(6)
                                dr_Aux.Item("monto") = dr.Item(7)
                                dr_Aux.Item("mora") = dr.Item(8)
                                dr_Aux.Item("total") = dr.Item(9)
                                dt_Aux.Rows.Add(dr_Aux)
                            End If
                        End If
                    End If
                End If
            Next

            dt_DB = dt_Aux.Copy

            Dim drTotal As DataRow
            drTotal = dt_Aux.NewRow

            drTotal.Item("idx") = 0
            drTotal.Item("concepto") = str_ConceptoSeleccionado
            drTotal.Item("tal") = DBNull.Value
            drTotal.Item("numero") = DBNull.Value
            drTotal.Item("periodo") = DBNull.Value
            drTotal.Item("origen") = DBNull.Value
            drTotal.Item("codigo") = DBNull.Value
            drTotal.Item("fechapago") = DBNull.Value
            drTotal.Item("anulado") = DBNull.Value
            drTotal.Item("alumno") = DBNull.Value
            drTotal.Item("monto") = dt_DB.Compute("Sum(monto)", "")
            drTotal.Item("mora") = dt_DB.Compute("Sum(mora)", "")
            drTotal.Item("total") = dt_DB.Compute("Sum(total)", "")
            dt_Aux.Rows.Add(drTotal)



            DataGridView2.DataSource = dt_Aux
            DataGridView2.AutoResizeColumns()

        Catch ex As Exception
            MsgBox("Error: " & ex.Message & ", en fila " & int_RowIdx.ToString)
        End Try

    End Sub


    Private Sub btnRegistrarPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistrarPago.Click

        Dim obj_bl_pagos As New bl_Pagos
        Dim usp_valor As Integer
        Dim usp_mensaje As String = ""

        Dim ds_DB As New DataSet
        Dim dt_dbaux As New DataTable
        dt_dbaux = dt_DB.Copy
        ds_DB.Tables.Add(dt_dbaux)

        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1
        Dim cod_Modulo As Integer = 1
        Dim cod_Opcion As Integer = 1

        Dim lstRegistro As New List(Of String)

        lstRegistro = obj_bl_pagos.fun_ins_exportacionpagos(ds_DB, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        MsgBox(usp_mensaje)

    End Sub

    Public Function obtenerDatosHojaExcel(ByVal sheet As String, ByVal path As String) As DataTable

        Dim connectionStringTemplate As String = _
                  "Provider=Microsoft.ACE.OLEDB.12.0;" + _
                  "Data Source={0};" + _
                  "Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"""

        Dim connectionString As String = String.Format(connectionStringTemplate, path)
        Dim sqlSelect As String = "SELECT * FROM [" & sheet.Trim() & "$];"
        Dim workbook As DataSet = New DataSet()
        Dim worksheet As DataTable

        Dim excelAdapter As System.Data.Common.DataAdapter = New System.Data.OleDb.OleDbDataAdapter(sqlSelect, connectionString)
        excelAdapter.Fill(workbook)
        worksheet = workbook.Tables(0)
        Return worksheet

    End Function

    Private Function crearTablaConsolidado(ByVal dt As DataTable)
        dt = Datos.agregarColumna(dt, "idx", "integer")
        dt = Datos.agregarColumna(dt, "concepto", "string")
        'dt = Datos.agregarColumna(dt, "comprobante", "string")
        dt = Datos.agregarColumna(dt, "tal", "string")
        dt = Datos.agregarColumna(dt, "numero", "string")
        dt = Datos.agregarColumna(dt, "periodo", "string")
        'dt = Datos.agregarColumna(dt, "anio", "string")
        'dt = Datos.agregarColumna(dt, "mes", "string")
        dt = Datos.agregarColumna(dt, "origen", "string")
        dt = Datos.agregarColumna(dt, "codigo", "string")
        dt = Datos.agregarColumna(dt, "fechapago", "string")
        dt = Datos.agregarColumna(dt, "alumno", "string")
        dt = Datos.agregarColumna(dt, "monto", "decimal")
        dt = Datos.agregarColumna(dt, "mora", "decimal")
        dt = Datos.agregarColumna(dt, "total", "decimal")
        dt = Datos.agregarColumna(dt, "anulado", "string")
        Return dt

    End Function

    Private Function crearTablaConceptos(ByVal dt As DataTable)
        dt = Datos.agregarColumna(dt, "idx", "integer")
        dt = Datos.agregarColumna(dt, "concepto", "string")
        Return dt

    End Function

End Class