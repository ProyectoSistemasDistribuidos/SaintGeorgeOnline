Imports System.Data

Partial Class Controles_CalendarioEntrevistas
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ViewState("MES") = Now.Month
        ViewState("ANIO") = Now.Year

        CargarCalendarioEntrevistas()
    End Sub

    Private Sub CargarCalendarioEntrevistas()

        Dim dtcalendario As DataTable
        'Dim objeActividades As New ClasesWeb.clsActCoorSheet
        Dim cantidadDias As Integer
        Dim numCasilla As Integer = 0
        Dim contFestividadTabla As Integer = 0

        TB_Reuniones.Rows.Clear()
        TB_Reuniones.HorizontalAlign = HorizontalAlign.Center
        TB_Reuniones.Width = 200
        TB_Reuniones.Height = 190
        TB_Reuniones.BorderStyle = BorderStyle.Solid
        TB_Reuniones.CellPadding = 0
        TB_Reuniones.CellSpacing = 0
        TB_Reuniones.BorderWidth = 1
        TB_Reuniones.BorderColor = Drawing.Color.Black
        TB_Reuniones.GridLines = GridLines.Both

        Dim addRow As Integer
        Dim addCol As Integer = 1

        Dim filas As Integer = 8
        Dim columnas As Integer = 7

        Dim mes As Integer
        Dim anio As Integer

        Dim contDias As Integer = 1
        Dim diaComienzo As Integer
        Dim diaActual As Integer = Now.Day
        Dim encontroActAceptadas As Integer = 0

        mes = ViewState("MES")
        anio = ViewState("ANIO")
        cantidadDias = obtenerCantDiasMes(mes, anio)


        'objeActividades.Mes = mes
        'objeActividades.Anio = anio
        'dtcalendario = objeActividades.ListarCalendarioActividades_FijasYAceptadas


        'diaComienzo = dtcalendario.Rows(0).Item("DiaComienzoMes")   'obtenerdiaComienzo(mes, anio)

        'If dtcalendario.Columns.Count > 1 Then
        '    encontroActAceptadas = 1
        'Else
        '    encontroActAceptadas = 0
        'End If


        '1...recorrer filas
        For addRow = 1 To filas
            Dim newRow = New TableRow
            newRow.Height = 20
            newRow.Width = 20

            '2...recorrer columnas
            For addCol = 1 To columnas
                Dim newCell2 = New TableCell
                newCell2.BorderColor = Drawing.Color.Black
                newCell2.BorderStyle = BorderStyle.Solid
                newCell2.BorderWidth = 1

                Dim lblDiaSemana As New System.Web.UI.WebControls.Label
                Dim cambioMes As New System.Web.UI.WebControls.DropDownList

                If addRow = 1 Then

                    If addCol = 1 Then
                        newCell2.ColumnSpan = 7
                        newCell2.HorizontalAlign = HorizontalAlign.Center
                        lblDiaSemana.Font.Bold = True
                        lblDiaSemana.Font.Size = 12
                        lblDiaSemana.Text = obtenerNombreMes(mes) & " " & anio
                        newCell2.Controls.Add(lblDiaSemana)
                        newRow.Cells.Add(newCell2)
                    End If

                End If

                If addRow = 2 Then

                    lblDiaSemana.ForeColor = Drawing.Color.White
                    lblDiaSemana.Font.Bold = True

                    newCell2.Style.Value = "background-color:#14c9f4;text-align:center"


                    If addCol = 1 Then
                        lblDiaSemana.Text = "MON"
                    End If

                    If addCol = 2 Then
                        lblDiaSemana.Text = "TUE"
                    End If

                    If addCol = 3 Then
                        lblDiaSemana.Text = "WED"
                    End If

                    If addCol = 4 Then
                        lblDiaSemana.Text = "THU"
                    End If

                    If addCol = 5 Then
                        lblDiaSemana.Text = "FRI"
                    End If

                    If addCol = 6 Then
                        lblDiaSemana.Text = "SAT"
                    End If

                    If addCol = 7 Then
                        lblDiaSemana.Text = "SUN"
                    End If
                    lblDiaSemana.Font.Size = 7
                    newCell2.Controls.Add(lblDiaSemana)
                    newRow.Cells.Add(newCell2)
                End If

                If addRow = 3 Or addRow = 4 Or addRow = 5 Or addRow = 6 Or addRow = 7 Or addRow = 8 Then

                    If addRow = 3 And addCol >= diaComienzo Then
                        lblDiaSemana.Text = contDias
                        numCasilla = addCol

                        If contDias = diaActual Then
                            newCell2.Font.Size = 16
                            newCell2.ForeColor = Drawing.Color.DarkBlue
                        End If

                        newCell2.Style.Value = "text-align:center"
                        lblDiaSemana.Font.Size = 7
                        newCell2.Controls.Add(lblDiaSemana)
                        newCell2.Font.Bold = True

                        contFestividadTabla = 0

                        'If dtcalendario.Columns.Count > 1 Then
                        '    While contFestividadTabla <= dtcalendario.Rows.Count - 1
                        '        If dtcalendario.Rows(contFestividadTabla).Item("NumCasilla") = numCasilla Then
                        '            newCell2.BackColor = Drawing.Color.LightGreen
                        '            newCell2.Style.Value = "cursor:pointer"
                        '            newCell2.Attributes.Add("onclick", "MostrardetalleCalendario('" & dtcalendario.Rows(contFestividadTabla).Item("DiaClonado") & "/" & mes & "/" & anio & "')")
                        '            newCell2.ToolTip = "Actividad: " & dtcalendario.Rows(contFestividadTabla).Item("Descrip_Actividad") & "...... Grado: " & dtcalendario.Rows(contFestividadTabla).Item("Grado") & "...... Hora: " & dtcalendario.Rows(contFestividadTabla).Item("HoraIni") & " - " & dtcalendario.Rows(contFestividadTabla).Item("HoraFin") & "...... Organizador: " & dtcalendario.Rows(contFestividadTabla).Item("Organizador") & "...... Tipo Actividad: " & dtcalendario.Rows(contFestividadTabla).Item("TipoActividad")

                        '        End If

                        '        contFestividadTabla = contFestividadTabla + 1
                        '    End While
                        'End If


                        newRow.Cells.Add(newCell2)
                        contDias = contDias + 1
                    ElseIf addRow > 3 Then

                        If addRow = 4 Then
                            numCasilla = 7 + addCol
                        ElseIf addRow = 5 Then
                            numCasilla = 14 + addCol
                        ElseIf addRow = 6 Then
                            numCasilla = 21 + addCol
                        ElseIf addRow = 7 Then
                            numCasilla = 28 + addCol
                        ElseIf addRow = 8 Then
                            numCasilla = 35 + addCol
                        End If

                        If contDias <= cantidadDias Then
                            lblDiaSemana.Text = contDias
                        Else
                            lblDiaSemana.Text = ""
                        End If
                        If contDias = diaActual Then
                            newCell2.Font.Size = 16
                            newCell2.ForeColor = Drawing.Color.DarkBlue
                        End If

                        newCell2.Style.Value = "text-align:center"
                        newCell2.Controls.Add(lblDiaSemana)
                        newCell2.Font.Bold = True

                        contFestividadTabla = 0

                        'If dtcalendario.Columns.Count > 1 Then
                        '    While contFestividadTabla <= dtcalendario.Rows.Count - 1
                        '        If dtcalendario.Rows(contFestividadTabla).Item("NumCasilla") = numCasilla Then
                        '            newCell2.BackColor = Drawing.Color.LightGreen
                        '            newCell2.Style.Value = "cursor:pointer"
                        '            newCell2.ToolTip = "Actividad: " & dtcalendario.Rows(contFestividadTabla).Item("Descrip_Actividad") & "...... Grado: " & dtcalendario.Rows(contFestividadTabla).Item("Grado") & "...... Hora: " & dtcalendario.Rows(contFestividadTabla).Item("HoraIni") & " - " & dtcalendario.Rows(contFestividadTabla).Item("HoraFin") & "...... Organizador: " & dtcalendario.Rows(contFestividadTabla).Item("Organizador") & "...... Tipo Actividad: " & dtcalendario.Rows(contFestividadTabla).Item("TipoActividad")
                        '            newCell2.Attributes.Add("onclick", "MostrardetalleCalendario('" & dtcalendario.Rows(contFestividadTabla).Item("DiaClonado") & "/" & mes & "/" & anio & "')")
                        '        End If

                        '        contFestividadTabla = contFestividadTabla + 1
                        '    End While

                        'End If


                        newRow.Cells.Add(newCell2)
                        contDias = contDias + 1
                    Else

                        lblDiaSemana.Text = ""

                        newCell2.Controls.Add(lblDiaSemana)
                        newRow.Cells.Add(newCell2)
                    End If

                End If

            Next
            TB_Reuniones.Rows.Add(newRow)
        Next

    End Sub

    Private Function obtenerCantDiasMes(ByVal Mes As Integer, ByVal anio As Integer) As Integer
        If Mes = 1 Then
            Return 31
        ElseIf Mes = 2 Then
            If anio = 2008 Or anio = 2011 Or anio = 2015 Or anio = 2019 Then
                Return 29
            Else
                Return 28
            End If
        ElseIf Mes = 3 Then
            Return 31
        ElseIf Mes = 4 Then
            Return 30
        ElseIf Mes = 5 Then
            Return 31
        ElseIf Mes = 6 Then
            Return 30
        ElseIf Mes = 7 Then
            Return 31
        ElseIf Mes = 8 Then
            Return 31
        ElseIf Mes = 9 Then
            Return 30
        ElseIf Mes = 10 Then
            Return 31
        ElseIf Mes = 11 Then
            Return 30
        ElseIf Mes = 12 Then
            Return 31
        End If
    End Function

    Private Function obtenerNombreMes(ByVal idmes As Integer) As String
        If idmes = 1 Then
            Return "January"
        ElseIf idmes = 2 Then
            Return "February"
        ElseIf idmes = 3 Then
            Return "March"
        ElseIf idmes = 4 Then
            Return "April"
        ElseIf idmes = 5 Then
            Return "May"
        ElseIf idmes = 6 Then
            Return "June"
        ElseIf idmes = 7 Then
            Return "July"
        ElseIf idmes = 8 Then
            Return "August"
        ElseIf idmes = 9 Then
            Return "September"
        ElseIf idmes = 10 Then
            Return "October"
        ElseIf idmes = 11 Then
            Return "November"
        ElseIf idmes = 12 Then
            Return "December"
        End If

    End Function

End Class
