Public Class GeneracionContrasenias

    ''' <summary>
    ''' Retorna un (1) número aleatorio.
    ''' </summary>
    ''' <param name="Minimo">Intervalo Inicial del número aleatorio</param>
    ''' <param name="Maximo">Intervalo Final del número aleatorio</param>
    ''' <returns>Número aleatorio</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function AleatorioNumeros(ByVal Minimo As Long, ByVal Maximo As Long) As Long
        Randomize() ' inicializar la semilla   
        AleatorioNumeros = CLng((Minimo - Maximo) * Rnd() + Maximo)
    End Function

    ''' <summary>
    ''' Retorna una (1) letra aleatoria.
    ''' </summary>
    ''' <param name="Minimo">Intervalo inicial de la posición de la letra</param>
    ''' <param name="Maximo">Intervalo final de la posición de la letra</param>
    ''' <returns>Letra aleatoria</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function AleatorioLetras(ByVal Minimo As Long, ByVal Maximo As Long) As String
        Randomize() ' inicializar la semilla   
        Dim numeroLetras As String = ""
        Dim letrasAleatorias As String = ""
        Dim int_contCaracter As Integer = 1

        While int_contCaracter <= 4

            numeroLetras = CLng((Minimo - Maximo) * Rnd() + Maximo)

            letrasAleatorias = letrasAleatorias & HomologarLetrasAbecedario(numeroLetras)
            int_contCaracter = int_contCaracter + 1
        End While

        Return letrasAleatorias
    End Function

    ''' <summary>
    ''' Genera contraseña de super usuario (9 caracteres)
    ''' </summary>
    ''' <returns>Retorna contraseña autogenerada de super usuario</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function ContraseniaSuperUsuario() As String
        Dim cadena_numeros As String = ""
        Dim cadena_letras As String = ""
        Dim cadena_contrasenia As String = ""

        cadena_numeros = AleatorioNumeros(1, 99999)
        cadena_letras = AleatorioLetras(1, 26)
        cadena_contrasenia = AleatorioPosicionCaracteres(cadena_numeros, cadena_letras)

        Return cadena_contrasenia
    End Function

    ''' <summary>
    ''' Obtiene una letra del abecedario segun su posicion
    ''' </summary>
    ''' <param name="int_PosicionLetra">Numero que representa una posicion en el abecedario</param>
    ''' <returns>Retorna letra del abecedario</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function HomologarLetrasAbecedario(ByVal int_PosicionLetra As Integer) As String
        Dim resultado As String = ""

        If int_PosicionLetra = 1 Then
            resultado = "a"
        ElseIf int_PosicionLetra = 2 Then
            resultado = "b"
        ElseIf int_PosicionLetra = 3 Then
            resultado = "c"
        ElseIf int_PosicionLetra = 4 Then
            resultado = "d"
        ElseIf int_PosicionLetra = 5 Then
            resultado = "e"
        ElseIf int_PosicionLetra = 6 Then
            resultado = "f"
        ElseIf int_PosicionLetra = 7 Then
            resultado = "g"
        ElseIf int_PosicionLetra = 8 Then
            resultado = "h"
        ElseIf int_PosicionLetra = 9 Then
            resultado = "i"
        ElseIf int_PosicionLetra = 10 Then
            resultado = "j"
        ElseIf int_PosicionLetra = 11 Then
            resultado = "k"
        ElseIf int_PosicionLetra = 12 Then
            resultado = "l"
        ElseIf int_PosicionLetra = 13 Then
            resultado = "m"
        ElseIf int_PosicionLetra = 14 Then
            resultado = "n"
        ElseIf int_PosicionLetra = 15 Then
            resultado = "o"
        ElseIf int_PosicionLetra = 16 Then
            resultado = "p"
        ElseIf int_PosicionLetra = 17 Then
            resultado = "q"
        ElseIf int_PosicionLetra = 18 Then
            resultado = "r"
        ElseIf int_PosicionLetra = 19 Then
            resultado = "s"
        ElseIf int_PosicionLetra = 20 Then
            resultado = "t"
        ElseIf int_PosicionLetra = 21 Then
            resultado = "u"
        ElseIf int_PosicionLetra = 22 Then
            resultado = "v"
        ElseIf int_PosicionLetra = 23 Then
            resultado = "w"
        ElseIf int_PosicionLetra = 24 Then
            resultado = "x"
        ElseIf int_PosicionLetra = 25 Then
            resultado = "y"
        ElseIf int_PosicionLetra = 26 Then
            resultado = "z"
        End If

        Return resultado
    End Function

    ''' <summary>
    ''' Setea los caracteres alfanumericos de la contraseña del super usuario en una posicion aleatoria
    ''' </summary>
    ''' <param name="str_Numeros">Numeros generados aleatoriamente (5)</param>
    ''' <param name="str_Letras">Letras generadas aleatoriamente (4)</param>
    ''' <returns>Retorna caracteres de contraseña autogenerada</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function AleatorioPosicionCaracteres(ByVal str_Numeros As String, ByVal str_Letras As String) As String
        Dim cadena_posiciones As String = ""
        Dim str_contrasenia As String = ""
        Dim str_Caracter As String = ""

        Dim int_contCaracter As Integer = 0
        Dim int_contNumeros As Integer = 0
        Dim int_contLetras As Integer = 0
       

        cadena_posiciones = AleatorioNumeros(1, 999999999)

        While int_contCaracter <= cadena_posiciones.Length - 1

            str_Caracter = cadena_posiciones.Chars(int_contCaracter).ToString

            If str_Caracter = "1" Or str_Caracter = "3" Or str_Caracter = "5" Or str_Caracter = "7" Or str_Caracter = "9" Then
                If str_Letras.Length > int_contLetras Then
                    str_contrasenia = str_contrasenia + str_Letras.Chars(int_contLetras).ToString
                    int_contLetras = int_contLetras + 1
                Else
                    str_contrasenia = str_contrasenia + str_Numeros.Chars(int_contNumeros).ToString
                    int_contNumeros = int_contNumeros + 1
                End If
            Else
                If str_Numeros.Length > int_contNumeros Then
                    str_contrasenia = str_contrasenia + str_Numeros.Chars(int_contNumeros).ToString
                    int_contNumeros = int_contNumeros + 1
                Else
                    str_contrasenia = str_contrasenia + str_Letras.Chars(int_contLetras).ToString
                    int_contLetras = int_contLetras + 1
                End If
                
            End If

            int_contCaracter = int_contCaracter + 1
        End While

        Return str_contrasenia
    End Function

End Class
