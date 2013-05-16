Public Class Datos

#Region "DataTable"

    ''' <summary>
    ''' Agrega columnas a la tabla temporal que se reciba.
    ''' </summary>
    ''' <param name="Table">Tabla temporal enviada</param>
    ''' <param name="nomCampo">Nombre de columna</param>
    ''' <param name="tipoDato">Tipo de dato de la columna</param>
    ''' <returns>Tabla temporal con columna agregada</returns>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function agregarColumna(ByRef Table As DataTable, ByVal nomCampo As String, ByVal tipoDato As String)

        Try
            Dim Name As DataColumn = New DataColumn(nomCampo)
            Select Case tipoDato
                Case "char", "string"
                    Name.DataType = System.Type.GetType("System.String")
                Case "numeric"
                    Name.DataType = System.Type.GetType("System.String")
                Case "decimal"
                    Name.DataType = System.Type.GetType("System.Decimal")
                Case "image"
                    Name.DataType = System.Type.GetType("System.Object")
                Case "Integer"
                    Name.DataType = System.Type.GetType("System.Int32")
                Case "Datetime"
                    Name.DataType = System.Type.GetType("System.DateTime")
            End Select
            Table.Columns.Add(Name)
            Return Table
        Catch
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Atogenera codigo correlativo maximo que identificara a una fila de una tabla temporal.
    ''' </summary>
    ''' <param name="dt_Temp_Auto">Tabla temporal</param>
    ''' <param name="str_NombreAutogenerado">Nombre de columna donde se autogenerará el identificador</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function AutogenerarCodigoTemporal(ByVal dt_Temp_Auto As DataTable, ByVal str_NombreAutogenerado As String) As Integer

        Dim cont As Integer = 0
        Dim max As Integer = 0

        If dt_Temp_Auto.Rows.Count = 0 Then
            max = 1
        End If

        While cont <= dt_Temp_Auto.Rows.Count - 1

            If max <= dt_Temp_Auto.Rows(cont).Item(str_NombreAutogenerado).ToString Then
                max = dt_Temp_Auto.Rows(cont).Item(str_NombreAutogenerado).ToString + 1
            End If

            cont = cont + 1
        End While

        Return max
    End Function

#End Region

#Region "Conversión"

    Public Shared Function Num2Text(ByVal value As Double) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

#End Region

#Region "Tipos de Datos"

    Public Shared Function isDecimal(ByVal s As String) As Boolean

        Dim bool_Return As Boolean = False
        Try

            Dim i, j As Integer
            i = InStr(1, s, ".00", CompareMethod.Text)
            j = InStr(1, s, ".50", CompareMethod.Text)

            If i > 0 Or j > 0 Then ' Si tiene el formato decimal
                Convert.ToDouble(s)
                bool_Return = True
            Else
                bool_Return = False
            End If

        Catch ex As Exception
            bool_Return = False
        End Try
        Return bool_Return

    End Function

#End Region

End Class
