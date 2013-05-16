
Imports System.Data
Imports System.Data.SqlClient
Imports SaintGeorgeOnline_Utilities

Public Class frmCargaTipoCambio

    Private dt_Excel As New DataTable
    Private dt_DB As New DataTable

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

                    Dim sqlSelect_Bono As String = "SELECT * FROM [Hoja1$];"

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

        Dim dtBono As New DataTable
        dtBono = Datos.agregarColumna(dtBono, "codigo", "integer")
        dtBono = Datos.agregarColumna(dtBono, "fecha", "datetime")
        dtBono = Datos.agregarColumna(dtBono, "compra", "decimal")
        dtBono = Datos.agregarColumna(dtBono, "venta", "decimal")
        dtBono = Datos.agregarColumna(dtBono, "estado", "integer")
        dtBono = Datos.agregarColumna(dtBono, "moneda", "integer")

        Dim cont As Integer = 1
        Dim auxDR As DataRow

        For Each dr As DataRow In dt_Excel.Rows
            auxDR = dtBono.NewRow
            auxDR.Item("codigo") = dr.Item("codigo")
            auxDR.Item("fecha") = dr.Item("fecha")
            auxDR.Item("compra") = dr.Item("compra")
            auxDR.Item("venta") = dr.Item("venta")
            auxDR.Item("estado") = dr.Item("estado")
            auxDR.Item("moneda") = dr.Item("moneda")
            dtBono.Rows.Add(auxDR)
        Next

        Dim conn As String = "data source=192.168.1.13; initial catalog=BD_SanGeorgeOnline; uid=SanJorgeOnlineSG; pwd=5g0nl1y% ;Language=spanish"
        Using cnx As New SqlConnection(conn)
            cnx.Open()

            For Each dr As DataRow In dtBono.Rows
                Using cmd As New SqlCommand("sp_tipocambio", cnx)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Clear()
                    With cmd.Parameters
                        .Add("@p1", SqlDbType.Int).Value = dr.Item("codigo")
                        .Add("@p2", SqlDbType.DateTime).Value = dr.Item("fecha")
                        .Add("@p3", SqlDbType.Decimal).Value = dr.Item("compra")
                        .Add("@p4", SqlDbType.Decimal).Value = dr.Item("venta")
                        .Add("@p5", SqlDbType.Int).Value = dr.Item("estado")
                        .Add("@p6", SqlDbType.Int).Value = dr.Item("moneda")
                    End With
                    cmd.ExecuteNonQuery()
                End Using
            Next
        End Using

    End Sub


End Class