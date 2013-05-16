Public Class Validacion

    ''' <summary>
    ''' Valida la cantidad de caracteres de las descripciones enviadas.
    ''' </summary>
    ''' <param name="txt_CampoIngreso">Cadena de caracteres enviada</param>
    ''' <returns>Indicador sobre la valides del campo recibido</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ValidarCamposIngreso(ByVal txt_CampoIngreso As System.Web.UI.WebControls.TextBox) As Boolean
        Dim texto As String = txt_CampoIngreso.Text.Trim
        Dim cont As Integer = 0
        Dim alert As Boolean = True
        Dim cont_palabra As Integer = 0
        Dim palabra As String = ""

        While cont <= texto.Length - 1

            palabra = texto.Substring(cont, 1)


            If palabra = " " Then
                cont_palabra = 0
            Else
                cont_palabra = cont_palabra + 1
            End If

            If cont_palabra > 50 Then
                alert = False
                Exit While
            End If
            cont = cont + 1
        End While

        Return alert
    End Function

    ''' <summary>
    ''' Valida la estructura del email enviado.
    ''' </summary>
    ''' <param name="Email">Email enviado</param>
    ''' <returns>Indicador sobre la valides del email recibido.</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function Validar_Email(ByVal Email As String) As Boolean

        Dim i As Integer, iLen As Integer, caracter As String
        Dim bp As Boolean, iPos As Integer, iPos2 As Integer

        ' On Local Error GoTo Err_Sub

        Email = Trim$(Email)

        If Email = vbNullString Then
            Exit Function
        End If

        Email = LCase$(Email)
        iLen = Len(Email)


        For i = 1 To iLen
            caracter = Mid(Email, i, 1)

            If (Not (caracter Like "[a-z]")) And (Not (caracter Like "[0-9]")) Then

                If InStr(1, "_-" & "." & "@", caracter) > 0 Then
                    If bp = True Then
                        Exit Function
                    Else
                        bp = True

                        If i = 1 Or i = iLen Then
                            Exit Function
                        End If

                        If caracter = "@" Then
                            If iPos = 0 Then
                                iPos = i
                            Else

                                Exit Function
                            End If
                        End If
                        If caracter = "." Then
                            iPos2 = i
                        End If

                    End If
                Else

                    Exit Function
                End If
            Else
                bp = False
            End If
        Next i
        If iPos = 0 Or iPos2 = 0 Then
            Exit Function
        End If

        If iPos2 < iPos Then
            Exit Function
        End If


        Validar_Email = True

        Exit Function
        'Err_Sub:
        '    On Local Error Resume Next

        Validar_Email = False
    End Function

End Class
