Imports SaintGeorgeOnline_Utilities
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_BusinessLogic.ModuloPensiones

Public Class frmCargarDeudas


    'Private dt_Viajes As DataTable
    'Private dt_Destino As DataTable

    Private dt_Deudas As DataTable
    Private dt_Cuotas As DataTable

    Private Sub frmCargarDeudas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarComboAnioAcademico()
        cargarConceptos()
        cargarMoneda()
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

                    Dim connectionStringTemplate As String = _
                    "Provider=Microsoft.ACE.OLEDB.12.0;" + _
                    "Data Source={0};" + _
                    "Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"""

                    Dim connectionString As String = String.Format(connectionStringTemplate, XLSPath)

                    Dim sqlSelect_Deudas As String = "SELECT * FROM [Hoja1$];"
                    Dim sqlSelect_Cuotas As String = "SELECT * FROM [Hoja2$];"

                    ' Load the Excel worksheet into a DataTable
                    Dim workbook_Deudas As DataSet = New DataSet()
                    Dim workbook_Cuotas As DataSet = New DataSet()

                    Dim worksheet_Deudas As DataTable
                    Dim worksheet_Cuotas As DataTable

                    Try

                        ' Deudas 
                        Dim excelAdapter As System.Data.Common.DataAdapter = New System.Data.OleDb.OleDbDataAdapter(sqlSelect_Deudas, connectionString)
                        excelAdapter.Fill(workbook_Deudas)
                        worksheet_Deudas = workbook_Deudas.Tables(0) ' Deudas
                        DataGridView2.DataSource = worksheet_Deudas
                        DataGridView2.AutoResizeColumns()

                        ' Cuotas
                        excelAdapter = New System.Data.OleDb.OleDbDataAdapter(sqlSelect_Cuotas, connectionString)
                        excelAdapter.Fill(workbook_Cuotas)
                        worksheet_Cuotas = workbook_Cuotas.Tables(0) ' Cuotas
                        DataGridView1.DataSource = worksheet_Cuotas
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

        If Not tbRuta.Text.Length > 0 Then
            MsgBox("Debe seleccionar un archivo.")
        Else
            dt_Deudas = DataGridView2.DataSource
            dt_Cuotas = DataGridView1.DataSource
            'MsgBox("Destino: " & dt_Destino.Rows.Count.ToString & " - Viajes: " & dt_Viajes.Rows.Count.ToString)

            Dim int_CodigoConcepto As Integer = cboConceptoCobro.SelectedValue
            Dim int_CodigoMoneda As Integer = cboMoneda.SelectedValue

            Dim dt_Registros As New DataTable
            dt_Registros = crearTablaConsolidado(dt_Registros)
            Dim dr As DataRow

            Dim dt_Fecha As Date

            For i As Integer = 0 To dt_Deudas.Rows.Count - 1
                For j As Integer = 0 To dt_Deudas.Columns.Count - 1
                    If j > 1 Then
                        If dt_Deudas.Rows(i).Item(j).ToString.Length > 0 Then
                            dr = dt_Registros.NewRow
                            dr.Item("codigoalumno") = dt_Deudas.Rows(i).Item("codigo").ToString
                            dr.Item("nombre") = dt_Deudas.Rows(i).Item("nombre").ToString
                            dr.Item("cuota") = dt_Deudas.Columns(j).ColumnName.ToString
                            dr.Item("monto") = dt_Deudas.Rows(i).Item(j).ToString
                            dt_Fecha = obtenerFechaVencimiento(dt_Deudas.Columns(j).ColumnName.ToString, dt_Cuotas)
                            dr.Item("fecven") = dt_Fecha
                            dr.Item("codigomes") = dt_Fecha.Month
                            dr.Item("codigoanio") = cboAnioAcademico.SelectedValue
                            dr.Item("codigoconcepto") = int_CodigoConcepto
                            dr.Item("codigomoneda") = int_CodigoMoneda
                            dt_Registros.Rows.Add(dr)
                        End If
                    End If
                Next
            Next

            MsgBox("Total Registros: " & dt_Registros.Rows.Count.ToString)

            Dim ds_Detalle As New DataSet
            ds_Detalle.Tables.Add(dt_Registros)

            Dim usp_valor As Integer = 0
            Dim usp_mensaje As String = ""

            Dim int_CodigoTipoUsuario As Integer = 1
            Dim int_CodigoUsuario As Integer = 1
            Dim cod_Modulo As Integer = 1
            Dim cod_Opcion As Integer = 1

            Dim obj_BL_Deudas As New bl_Deudas
            usp_valor = obj_BL_Deudas.FUN_INS_DeudasPorServicio(ds_Detalle, usp_mensaje, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
            MsgBox(usp_mensaje)

        End If

    End Sub

    Private Function crearTablaConsolidado(ByVal dt As DataTable)

        dt = Datos.agregarColumna(dt, "codigoalumno", "string")
        dt = Datos.agregarColumna(dt, "nombre", "string")
        dt = Datos.agregarColumna(dt, "cuota", "string")
        dt = Datos.agregarColumna(dt, "monto", "decimal")
        dt = Datos.agregarColumna(dt, "fecven", "Datetime")
        dt = Datos.agregarColumna(dt, "codigomes", "integer")
        dt = Datos.agregarColumna(dt, "codigoanio", "integer")
        dt = Datos.agregarColumna(dt, "codigoconcepto", "integer")
        dt = Datos.agregarColumna(dt, "codigomoneda", "integer")
        Return dt

    End Function

    Private Function obtenerFechaVencimiento(ByVal str_cuota As String, ByVal dt As DataTable) As DateTime
        Dim str_Fecha As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item(0) = str_cuota Then
                str_Fecha = dt.Rows(i).Item("Fecha")
                Exit For
            End If
        Next
        Return Convert.ToDateTime(str_Fecha)
    End Function

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

    Private Sub cargarConceptos()

        Dim int_CodigoPagina As Integer = System.Configuration.ConfigurationManager.AppSettings("CodigoPagina_RegistroDeudasGenerales").ToString.Trim
        Dim int_Estado As Integer = 1

        Dim obj_BL_ConceptosCobros As New bl_ConceptosCobros
        Dim int_CodigoTipoUsuario As Integer = 1
        Dim int_CodigoUsuario As Integer = 1
        Dim cod_Modulo As Integer = 1
        Dim cod_Opcion As Integer = 1

        Dim ds_Lista As DataSet = obj_BL_ConceptosCobros.FUN_LIS_ConceptosCobrosPorModulo(int_CodigoPagina, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        cboConceptoCobro.DataSource = ds_Lista.Tables(0)
        cboConceptoCobro.ValueMember = "Codigo"
        cboConceptoCobro.DisplayMember = "Descripcion"

    End Sub

    Private Sub cargarMoneda()

        Dim str_Descripcion As String = ""
        Dim obj_BL_Moneda As New bl_Moneda
        Dim int_CodigoTipoUsuario As Integer = 1
        Dim int_CodigoUsuario As Integer = 1
        Dim cod_Modulo As Integer = 1
        Dim cod_Opcion As Integer = 1

        Dim ds_Lista As DataSet = obj_BL_Moneda.FUN_LIS_Moneda(str_Descripcion, 1, int_CodigoUsuario, int_CodigoTipoUsuario, cod_Modulo, cod_Opcion)
        cboMoneda.DataSource = ds_Lista.Tables(0)
        cboMoneda.ValueMember = "Codigo"
        cboMoneda.DisplayMember = "Descripcion"

    End Sub

End Class