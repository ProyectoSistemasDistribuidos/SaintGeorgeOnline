Imports System.Web.UI.WebControls
Imports System.Windows.Forms


Public Class Controles_pres

#Region "utiles"

    ''' <summary>
    ''' Inserta información (solo númerica) en el seleccionable recibido.
    ''' </summary>
    ''' <param name="combo">Control de tipo combobox</param>
    ''' <param name="numInicio">Numero Inicial del rango</param>
    ''' <param name="numFin">Numero Final del rango</param>
    ''' <param name="includeTodos">Indica si se agregara un registro indicando -TODOS-</param>
    ''' <param name="includeSeleccion">Indica si se agrega un registro indicando -SELECCIONE-</param>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Sub llenarComboNumerico(ByVal combo As DropDownList, _
                    ByVal numInicio As Integer, _
                    ByVal numFin As Integer, _
                    ByVal includeTodos As Boolean, _
                    ByVal includeSeleccion As Boolean)
        With combo

            Dim cont As Integer = numFin - numInicio
            For i As Integer = 0 To cont
                .Items.Insert(i, New ListItem(numFin - i))
                .Items(i).Value = numFin - i
            Next

            If includeTodos Then
                .Items.Insert(0, New ListItem("--Todos--"))
                .Items(0).Value = "0"
            ElseIf includeSeleccion Then
                .Items.Insert(0, New ListItem("--Seleccione--"))
                .Items(0).Value = "0"
            End If

            .SelectedIndex = 0
        End With
    End Sub

    ''' <summary>
    ''' Retorna 1 DataSet con la lista de meses del año
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     02/03/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ListaMeses() As DataSet

        Dim ds_Lista As New DataSet
        Dim objDT_Meses As New DataTable
        objDT_Meses = New DataTable("ListaMeses")
        objDT_Meses = Datos.agregarColumna(objDT_Meses, "Codigo", "Integer")
        objDT_Meses = Datos.agregarColumna(objDT_Meses, "Descripcion", "String")

        Dim str_Mes As String = ""
        Dim r As DataRow
        For i As Integer = 1 To 12
            r = objDT_Meses.NewRow
            r.Item("Codigo") = i
            Select Case i
                Case 1 : str_Mes = "Enero"
                Case 2 : str_Mes = "Febrero"
                Case 3 : str_Mes = "Marzo"
                Case 4 : str_Mes = "Abril"
                Case 5 : str_Mes = "Mayo"
                Case 6 : str_Mes = "Junio"
                Case 7 : str_Mes = "Julio"
                Case 8 : str_Mes = "Agosto"
                Case 9 : str_Mes = "Setiembre"
                Case 10 : str_Mes = "Octubre"
                Case 11 : str_Mes = "Noviembre"
                Case 12 : str_Mes = "Diciembre"
            End Select
            r.Item("Descripcion") = str_Mes
            objDT_Meses.Rows.Add(r)
        Next
        ds_Lista.Tables.Add(objDT_Meses)
        Return ds_Lista

    End Function

    ''' <summary>
    ''' Retorna 1 DataSet con la lista de meses del año en formato númerico
    ''' </summary>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     21/06/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ListaNumerica(ByVal int_Inicio As Integer, ByVal int_Fin As Integer) As DataSet

        Dim ds_Lista As New DataSet
        Dim objDT As New DataTable
        objDT = New DataTable("Lista")
        objDT = Datos.agregarColumna(objDT, "Codigo", "Integer")
        objDT = Datos.agregarColumna(objDT, "Descripcion", "String")

        Dim str_Mes As String = ""
        Dim r As DataRow
        For i As Integer = int_Inicio To int_Fin
            r = objDT.NewRow
            r.Item("Codigo") = i
            str_Mes = i.ToString
            r.Item("Descripcion") = str_Mes
            objDT.Rows.Add(r)
        Next
        ds_Lista.Tables.Add(objDT)
        Return ds_Lista

    End Function

    ''' <summary>
    ''' Inserta información en el seleccionable (de tipo combobox) recibido.
    ''' </summary>
    ''' <param name="combo">Control de tipo combobox</param>
    ''' <param name="dataSource">Tabla temporal con los datos a insertar</param>
    ''' <param name="ValueField">Nombre de columna que se referira a los valores internos del seleccionable</param>
    ''' <param name="TextField">Nombre de columna que se referira a las descripciones mostradas del seleccionable</param>
    ''' <param name="includeTodos">Indica si se agregara un registro indicando -TODOS-</param>
    ''' <param name="includeSeleccion">Indica si se agrega un registro indicando -SELECCIONE-</param>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Sub llenarCombo(ByVal combo As DropDownList, _
                        ByVal dataSource As DataSet, _
                        ByVal ValueField As String, _
                        ByVal TextField As String, _
                        ByVal includeTodos As Boolean, _
                        ByVal includeSeleccion As Boolean)
        With combo
            .DataSource = dataSource
            .DataValueField = ValueField
            .DataTextField = TextField
            .DataBind()

            If includeTodos Then
                .Items.Insert(0, New ListItem("--Todos--"))
                .Items(0).Value = "0"
            ElseIf includeSeleccion Then
                .Items.Insert(0, New ListItem("--Seleccione--"))
                .Items(0).Value = "0"
            End If

            .SelectedIndex = 0
        End With
    End Sub

    Public Shared Sub llenarCombo(ByVal combo As DropDownList, _
                    ByVal dataSource As DataTable, _
                    ByVal ValueField As String, _
                    ByVal TextField As String, _
                    ByVal includeTodos As Boolean, _
                    ByVal includeSeleccion As Boolean)
        With combo
            .DataSource = dataSource
            .DataValueField = ValueField
            .DataTextField = TextField
            .DataBind()

            If includeTodos Then
                .Items.Insert(0, New ListItem("--Todos--"))
                .Items(0).Value = "0"
            ElseIf includeSeleccion Then
                .Items.Insert(0, New ListItem("--Seleccione--"))
                .Items(0).Value = "0"
            End If

            .SelectedIndex = 0
        End With
    End Sub

    Public Shared Sub llenarCombo(ByVal combo As DropDownList, _
                 ByVal dataSource As DataView, _
                 ByVal ValueField As String, _
                 ByVal TextField As String, _
                 ByVal includeTodos As Boolean, _
                 ByVal includeSeleccion As Boolean)
        With combo
            .DataSource = dataSource
            .DataValueField = ValueField
            .DataTextField = TextField
            .DataBind()

            If includeTodos Then
                .Items.Insert(0, New ListItem("--Todos--"))
                .Items(0).Value = "0"
            ElseIf includeSeleccion Then
                .Items.Insert(0, New ListItem("--Seleccione--"))
                .Items(0).Value = "0"
            End If

            .SelectedIndex = 0
        End With
    End Sub

    ''' <summary>
    ''' Inserta información en el seleccionable (de tipo AjaxControlToolkit.ComboBox) recibido.
    ''' </summary>
    ''' <param name="combo">Control de tipo AjaxControlToolkit.ComboBox</param>
    ''' <param name="dataSource">Tabla temporal con los datos a insertar</param>
    ''' <param name="ValueField">Nombre de columna que se referira a los valores internos del seleccionable</param>
    ''' <param name="TextField">Nombre de columna que se referira a las descripciones mostradas del seleccionable</param>
    ''' <param name="includeTodos">Indica si se agregara un registro indicando -TODOS-</param>
    ''' <param name="includeSeleccion">Indica si se agrega un registro indicando -SELECCIONE-</param>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Public Shared Sub llenarCombo(ByVal combo As AjaxControlToolkit.ComboBox, _
    '                ByVal dataSource As DataSet, _
    '                ByVal ValueField As String, _
    '                ByVal TextField As String, _
    '                ByVal includeTodos As Boolean, _
    '                ByVal includeSeleccion As Boolean)
    '    With combo
    '        .DataSource = dataSource
    '        .DataValueField = ValueField
    '        .DataTextField = TextField
    '        .DataBind()

    '        If includeTodos Then
    '            .Items.Insert(0, New ListItem("--Todos--"))
    '            .Items(0).Value = "0"
    '        ElseIf includeSeleccion Then
    '            .Items.Insert(0, New ListItem("--Seleccione--"))
    '            .Items(0).Value = "0"
    '        End If

    '        .SelectedIndex = 0

    '    End With
    'End Sub

    ''' <summary>
    ''' Inserta información en el seleccionable (de tipo combobox - Windows Form) recibido.
    ''' </summary>
    ''' <param name="combo">Control de tipo combobox</param>
    ''' <param name="dataSource">Tabla temporal con los datos a insertar</param>
    ''' <param name="ValueField">Nombre de columna que se referira a los valores internos del seleccionable</param>
    ''' <param name="TextField">Nombre de columna que se referira a las descripciones mostradas del seleccionable</param>
    ''' <param name="includeTodos">Indica si se agregara un registro indicando -TODOS-</param>
    ''' <param name="includeSeleccion">Indica si se agrega un registro indicando -SELECCIONE-</param>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     27/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Sub llenarCombo(ByVal combo As ComboBox, _
                        ByVal dataSource As DataSet, _
                        ByVal ValueField As String, _
                        ByVal TextField As String, _
                        ByVal includeTodos As Boolean, _
                        ByVal includeSeleccion As Boolean)
        With combo
            .DataSource = dataSource.Tables(0)
            .ValueMember = ValueField
            .DisplayMember = TextField
            .SelectedIndex = 0
        End With
    End Sub

    ''' <summary>
    ''' Limpia la información ingresada en el seleccionable (de tipo combobox) recibido.
    ''' </summary>
    ''' <param name="combo">Control de tipo combobox</param>
    ''' <param name="includeTodos">Indica si se agregara un registro indicando -TODOS-</param>
    ''' <param name="includeSeleccion">Indica si se agrega un registro indicando -SELECCIONE-</param>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Sub limpiarCombo(ByVal combo As DropDownList, _
                                   ByVal includeTodos As Boolean, _
                                   ByVal includeSeleccion As Boolean)
        With combo
            .Items.Clear()
            If includeTodos Then
                .Items.Insert(0, New ListItem("--Todos--"))
            ElseIf includeSeleccion Then
                .Items.Insert(0, New ListItem("--Seleccione--"))
            End If
            .Items(0).Value = "0"

            .SelectedIndex = 0
            .DataBind()
        End With
    End Sub

    ''' <summary>
    ''' Limpia la información ingresada en el seleccionable (de tipo AjaxControlToolkit.ComboBox) recibido.
    ''' </summary>
    ''' <param name="combo">Control de tipo AjaxControlToolkit.ComboBox</param>
    ''' <param name="includeTodos">Indica si se agregara un registro indicando -TODOS-</param>
    ''' <param name="includeSeleccion">Indica si se agrega un registro indicando -SELECCIONE-</param>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    'Public Shared Sub limpiarCombo(ByVal combo As AjaxControlToolkit.ComboBox, _
    '                         ByVal includeTodos As Boolean, _
    '                         ByVal includeSeleccion As Boolean)
    '    With combo
    '        .Items.Clear()
    '        If includeTodos Then
    '            .Items.Insert(0, New ListItem("--Todos--"))
    '        ElseIf includeSeleccion Then
    '            .Items.Insert(0, New ListItem("--Seleccione--"))
    '        End If
    '        .Items(0).Value = "0"

    '        .SelectedIndex = 0
    '        .DataBind()
    '    End With
    'End Sub

    ''' <summary>
    ''' Limpia la información ingresada en el seleccionable (de tipo combobox - Windows Form) recibido.
    ''' </summary>
    ''' <param name="combo">Control de tipo combobox</param>
    ''' <param name="includeTodos">Indica si se agregara un registro indicando -TODOS-</param>
    ''' <param name="includeSeleccion">Indica si se agrega un registro indicando -SELECCIONE-</param>
    ''' <remarks>
    ''' Creador:               Juan Jose
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Sub limpiarCombo(ByVal combo As ComboBox, _
                                   ByVal includeTodos As Boolean, _
                                   ByVal includeSeleccion As Boolean)
        With combo
            .Items.Clear()
            If includeTodos Then
                .Items.Insert(0, New ListItem("--Todos--"))
            ElseIf includeSeleccion Then
                .Items.Insert(0, New ListItem("--Seleccione--"))
            End If
            .Items(0).Value = "0"

            .SelectedIndex = 0
        End With
    End Sub

#End Region

    Public Class CaracteresEspeciales

#Region "Filtro caracteres validos"

        ''' <summary>
        ''' Inserta información en el seleccionable (de tipo AjaxControlToolkit.ComboBox) recibido.
        ''' </summary>
        ''' <param name="FilterTextBox">Control de tipo AjaxControlToolkit.FilteredTextBoxExtender</param>
        ''' <param name="int_TipoGrupoValido">Tipo de control de caracteres</param>
        ''' <remarks>
        ''' Creador:               Juan Jose
        ''' Fecha de Creación:     30/03/2011
        ''' Modificado por:        _____________
        ''' Fecha de modificación: _____________ 
        ''' </remarks>
        'Public Shared Sub agregarGrupo(ByVal FilterTextBox As AjaxControlToolkit.FilteredTextBoxExtender, ByVal int_Grupo As Integer)

        '    Dim arrayChar As New ArrayList
        '    If int_Grupo = 1 Then 'Tipo : Descripcion
        '        arrayChar.Add("á")
        '        arrayChar.Add("é")
        '        arrayChar.Add("í")
        '        arrayChar.Add("ó")
        '        arrayChar.Add("ú")
        '        arrayChar.Add("Á")
        '        arrayChar.Add("É")
        '        arrayChar.Add("Í")
        '        arrayChar.Add("Ó")
        '        arrayChar.Add("Ú")
        '        arrayChar.Add("(")
        '        arrayChar.Add(")")
        '        arrayChar.Add(" ")
        '        arrayChar.Add("_")
        '        arrayChar.Add("-")
        '        arrayChar.Add("@")
        '        arrayChar.Add("$")
        '        arrayChar.Add(",")
        '        arrayChar.Add(".")
        '        arrayChar.Add("´")
        '    End If

        '    Dim strBuilder As New System.Text.StringBuilder
        '    For i As Integer = 0 To arrayChar.Count - 1
        '        strBuilder.Append("'" & arrayChar.Item(i).ToString & "'")
        '        If i < arrayChar.Count - 1 Then
        '            strBuilder.Append(",")
        '        End If
        '    Next

        '    FilterTextBox.ValidChars = strBuilder.ToString

        'End Sub

#End Region

    End Class

End Class
