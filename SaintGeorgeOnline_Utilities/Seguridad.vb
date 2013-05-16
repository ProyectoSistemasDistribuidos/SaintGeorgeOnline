Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Public Class Seguridad

    'Objeto Encriptador
    Private G_sEncriptador As SymmetricAlgorithm
    'KeyGen Generado con Algoritmo TripleDES
    Private ReadOnly G_sKEY() As Byte = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, _
                            15, 16, 17, 18, 19, 20, 21, 22, 23, 24}

    'IV Generado con Algoritmo TripleDES
    Private ReadOnly G_sIV() As Byte = {8, 7, 6, 5, 4, 3, 2, 1}

    ''' <summary>
    ''' Private: Desencriptar()
    ''' </summary>
    ''' <param name="sCadena_TEXT">Cadena de conexión Encritada</param>
    ''' <returns>Cadena de Conexión Desencriptada UTF-8</returns>
    Private Function Desencriptar(ByVal sCadena_TEXT As String) As String

        Dim mTransformacion As ICryptoTransform
        Dim mMemoryStream As MemoryStream
        Dim mCriptografia As CryptoStream
        Dim mCadenaBytes() As Byte
        Dim mDesencriptado As String

        'Cargar G_sEncriptador de Algoritmo Simetrico
        G_sEncriptador = New TripleDESCryptoServiceProvider

        'Caso clave bytes sea mayor que cero
        If ((G_sKEY.Length > 0) And (G_sIV.Length > 0)) Then

            Try

                'Desencriptar Valor
                mTransformacion = G_sEncriptador.CreateDecryptor(G_sKEY, G_sIV)
                'Recupear Cadena
                mCadenaBytes = Convert.FromBase64String(sCadena_TEXT)
                'Almacenar Valor Desencriptado
                mMemoryStream = New MemoryStream
                mCriptografia = New CryptoStream(mMemoryStream, mTransformacion, CryptoStreamMode.Write)
                mCriptografia.Write(mCadenaBytes, 0, mCadenaBytes.Length)
                mCriptografia.FlushFinalBlock()
                'Cerrar
                mCriptografia.Close()
                'Recuperar valor en Formato Texto
                mDesencriptado = Encoding.UTF8.GetString(mMemoryStream.ToArray)

            Catch ex As CryptographicException
                'Mensaje Error
                Return ("Error: " + ex.Message)
            End Try
        Else
            'Mandar Mensaje
            Return "Error: No se pudo conseguir la Clave y el IV"
        End If

        Return mDesencriptado

    End Function

    ''' <summary>
    ''' Public: DesencriptarOnlyText()
    ''' ------------------------------
    ''' Desencripta una cadena de conexión o texto cualquiera con Algoritmo de encriptación
    ''' Triple DES a formato UTF-8
    ''' </summary>
    ''' <param name="sCadenaEncrypt_TEXT">Cadena o Texto a desencriptar</param>
    ''' <returns>Cadena o Texto desencriptado (UTF - 8)</returns>
    Public Function DesencriptarOnlyText(ByVal sCadenaEncrypt_TEXT As String) As String

        Try
            Return Desencriptar(sCadenaEncrypt_TEXT)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ConvertirFechaDecimal(ByVal fecha As String) As Decimal

        If fecha.Length = 0 Then Return 0
        Return Convert.ToDecimal(fecha.Substring(6, 4) + fecha.Substring(3, 2) + fecha.Substring(0, 2))

    End Function

End Class
