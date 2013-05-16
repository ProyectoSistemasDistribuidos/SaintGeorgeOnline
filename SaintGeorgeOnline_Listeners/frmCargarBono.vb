Imports System.Data
Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones

Public Class frmCargarBono

    Private dt_Excel As New DataTable
    Private dt_DB As New DataTable

    Private Sub frmCargarBono_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarComboAnioAcademico()
    End Sub

    Private Sub btnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargar.Click

        dt_Excel.Clear()
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

                    Dim connectionStringTemplate As String = _
                    "Provider=Microsoft.ACE.OLEDB.12.0;" + _
                    "Data Source={0};" + _
                    "Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"""

                    Dim connectionString As String = String.Format(connectionStringTemplate, XLSPath)

                    Dim sqlSelect_Bono As String = "SELECT * FROM [Por Distritos$];"

                    ' Load the Excel worksheet into a DataTable
                    Dim workbook_Bono As DataSet = New DataSet() ' libro
                    Dim worksheet_Bono As DataTable ' hoja

                    Try

                        ' Deudas 
                        Dim excelAdapter As System.Data.Common.DataAdapter = New System.Data.OleDb.OleDbDataAdapter(sqlSelect_Bono, connectionString)
                        excelAdapter.Fill(workbook_Bono)
                        worksheet_Bono = workbook_Bono.Tables(0) ' Bono

                        dt_Excel = worksheet_Bono

                        DataGridView1.DataSource = worksheet_Bono
                        DataGridView1.AutoResizeColumns()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                Else
                    MsgBox("El archvio no existe.")
                End If
            End If
        End With

    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        'MsgBox(dt_Excel.Rows.Count)

        Dim dtBono As New DataTable
        dtBono = Datos.agregarColumna(dtBono, "nperiodo", "integer")
        dtBono = Datos.agregarColumna(dtBono, "ngrado", "string")
        dtBono = Datos.agregarColumna(dtBono, "calumno", "string")
        dtBono = Datos.agregarColumna(dtBono, "nalumno", "string")
        dtBono = Datos.agregarColumna(dtBono, "ndistrito", "string")
        dtBono = Datos.agregarColumna(dtBono, "bono", "decimal")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In dt_Excel.Rows
            auxDR = dtBono.NewRow
            auxDR.Item("nperiodo") = dr.Item("nperiodo")
            auxDR.Item("ngrado") = dr.Item("ngrado")
            auxDR.Item("calumno") = dr.Item("calumno")
            auxDR.Item("nalumno") = dr.Item("nalumno")
            auxDR.Item("ndistrito") = dr.Item("ndistrito")
            auxDR.Item("bono") = dr.Item("bono")
            dtBono.Rows.Add(auxDR)
        Next

        Dim int_CodigoUsuario As Integer = 1
        Dim int_CodigoTipoUsuario As Integer = 1
        Dim cod_Modulo As Integer = 1
        Dim cod_Opcion As Integer = 1

        Dim obj_bl_deduas As New bl_Deudas
        Dim usp_mensaje As String = ""
        Dim lstRegistro As New List(Of String)

        lstRegistro = obj_bl_deduas.fun_ins_exportacionbonos(dtBono, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        MsgBox(usp_mensaje)



    End Sub

    Private Sub cargarComboAnioAcademico()

        Dim str_Descripcion As String = ""
        Dim obj_BL_AniosAcademicos As New bl_AniosAcademicos
        Dim int_CodigoTipoUsuario As Integer = 1
        Dim int_CodigoUsuario As Integer = 1
        Dim cod_Modulo As Integer = 1
        Dim cod_Opcion As Integer = 1

        Dim ds_Lista As DataSet = obj_BL_AniosAcademicos.FUN_LIS_AniosAcademicos(str_Descripcion, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        cboAnioAcademico.DataSource = ds_Lista.Tables(0)
        cboAnioAcademico.ValueMember = "Codigo"
        cboAnioAcademico.DisplayMember = "Descripcion"

    End Sub

End Class