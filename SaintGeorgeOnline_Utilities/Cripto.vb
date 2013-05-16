Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class Cripto

    'Will return and use hexadecimal values. Default is False
    Private Shared _Hexa As Boolean = False

    Private Shared Property Hexa() As Boolean
        Get
            Return _Hexa
        End Get
        Set(ByVal value As Boolean)
            _Hexa = value
        End Set
    End Property

    'Encryption key
    Private Shared _Key As String = "SANJORGE"

    Private Shared Property Key() As String
        Get
            Return _Key
        End Get
        Set(ByVal value As String)
            _Key = value
        End Set
    End Property

    'Initialization vector. 
    Private Shared _IV() As Byte = {1, 3, 10, 55, 70, 90, 240, 2}

    Private Shared Property IV() As Byte()
        Get
            Return _IV
        End Get
        Set(ByVal value() As Byte)
            _IV = value
        End Set
    End Property

    Private Shared _Encoding As Text.Encoding = Text.Encoding.UTF8

    Private Shared Property Encoding() As Text.Encoding
        Get
            Return _Encoding
        End Get
        Set(ByVal value As Text.Encoding)
            _Encoding = value
        End Set
    End Property

    'Will auto adjust the key value if length is invalid. Defaut is True.
    Private Shared _AutoAdjustKeyLength As Boolean = True

    Private Shared Property AutoAdjustKeyLength() As Boolean
        Get
            Return _AutoAdjustKeyLength
        End Get
        Set(ByVal value As Boolean)
            _AutoAdjustKeyLength = value
        End Set
    End Property

    Public Function Encriptar(ByVal objCrypto As SymmetricAlgorithm, ByVal strEncriptar As String) As String
        Return Encrypt(objCrypto, strEncriptar, Key, IVHandler(IV, 8), Hexa)
    End Function

    Private Function Encrypt(ByVal objCrypto As SymmetricAlgorithm, ByVal value As String, ByVal encKey As String, ByVal arrIV() As Byte, ByVal blnHexa As Boolean) As String

        Dim strReturn As String = String.Empty
        Dim objStringBuilder As New StringBuilder
        Dim objMS As New MemoryStream
        Dim objCS As CryptoStream
        Dim objSW As StreamWriter
        Dim arrHexa() As Byte
        Dim arrKey() As Byte
        Dim intLength As Integer
        Dim i As Integer

        Try
            If value.Length > 0 Then

                arrKey = KeyHandler(encKey, objCrypto.LegalKeySizes(0).MinSize \ 8)

                objCS = New CryptoStream(objMS, objCrypto.CreateEncryptor(arrKey, arrIV), CryptoStreamMode.Write)
                objSW = New StreamWriter(objCS)
                objSW.Write(value)
                objSW.Flush()
                objCS.FlushFinalBlock()
                objMS.Flush()

                strReturn = Convert.ToBase64String(objMS.GetBuffer(), 0, Convert.ToInt32(objMS.Length))

                If blnHexa Then
                    arrHexa = Encoding.GetBytes(strReturn)
                    intLength = arrHexa.Length - 1
                    For i = 0 To intLength
                        objStringBuilder.Append(arrHexa(i).ToString("x"))
                    Next i
                    strReturn = objStringBuilder.ToString()
                End If

                objSW.Close()
                objCS.Close()
                objMS.Close()
                objCrypto.Clear()

            End If
        Catch ex As Exception
            strReturn = ""
        End Try


        Return strReturn

    End Function

    Public Function Desencriptar(ByVal objCrypto As SymmetricAlgorithm, ByVal strDesencriptar As String) As String
        Return Decrypt(objCrypto, strDesencriptar, Key, IVHandler(IV, 8), Hexa)
    End Function

    Private Function Decrypt(ByVal objCrypto As SymmetricAlgorithm, ByVal value As String, ByVal encKey As String, ByVal arrIV() As Byte, ByVal blnHexa As Boolean) As String

        Dim strReturn As String = String.Empty
        Dim objStringBuilder As New StringBuilder
        Dim objMS As MemoryStream
        Dim objCS As CryptoStream
        Dim objSR As StreamReader
        Dim arrHexa() As Byte
        Dim arrKey() As Byte
        Dim intLength As Integer
        Dim i As Integer
        Try
            If value.Length > 0 Then

                If blnHexa Then
                    intLength = Convert.ToInt32((value.Length / 2) - 1)
                    ReDim arrHexa(intLength)
                    For i = 0 To intLength
                        arrHexa(i) = Byte.Parse(value.Substring(i * 2, 2), Globalization.NumberStyles.HexNumber)
                    Next i
                    value = Encoding.GetString(arrHexa)
                End If

                arrKey = KeyHandler(encKey, objCrypto.LegalKeySizes(0).MinSize \ 8)

                objMS = New MemoryStream(Convert.FromBase64String(value))
                objCS = New CryptoStream(objMS, objCrypto.CreateDecryptor(arrKey, arrIV), CryptoStreamMode.Read)
                objSR = New StreamReader(objCS)

                strReturn = objSR.ReadToEnd()

                objSR.Close()
                objCS.Close()
                objMS.Close()
                objCrypto.Clear()

            End If
        Catch ex As Exception
            strReturn = ""
        End Try


        Return strReturn

    End Function

    Private Shared Function IVHandler(ByVal arrIV() As Byte, ByVal intSize As Integer) As Byte()

        Dim arrReturn(intSize) As Byte
        Dim i As Integer
        Dim z As Integer = 0

        If arrIV Is Nothing Then
            arrIV = IV
        End If

        If Not AutoAdjustKeyLength And arrIV.Length <> intSize Then
            MsgBox("Error")
            'Throw (New CryptoException(GetType(Cripto), My.Resources.Messages.ErrorIVLength.Replace("#1", arrIV.Length.ToString()).Replace("#2", intSize.ToString())))
        End If

        If intSize = arrIV.Length Then
            Return arrIV
        ElseIf intSize < arrIV.Length Then
            For i = 0 To intSize - 1
                arrReturn(i) = arrIV(i)
            Next
        ElseIf intSize > arrIV.Length Then
            For i = 0 To intSize - 1
                arrReturn(i) = arrIV(z)
                z += 1
                If z > arrIV.Length - 1 Then
                    z = 0
                End If
            Next
        End If

        Return arrReturn

    End Function

    Private Function KeyHandler(ByVal encKey As String, ByVal intSize As Integer) As Byte()

        Dim strNewKey As String
        Dim i As Integer
        Dim z As Integer = 0

        If Not AutoAdjustKeyLength And encKey.Length <> intSize Then
            MsgBox("Error")
            'Throw (New CryptoException(GetType(Cripto), My.Resources.Messages.ErrorKeyLength.Replace("#1", encKey.Length.ToString()).Replace("#2", intSize.ToString())))
        End If

        strNewKey = encKey

        If encKey.Length < intSize Then
            For i = encKey.Length + 1 To intSize
                strNewKey &= encKey.Substring(z, 1)
                z += 1
                If z > encKey.Length - 1 Then
                    z = 0
                End If
            Next
        ElseIf encKey.Length > intSize Then
            strNewKey = encKey.Substring(0, intSize)
        End If

        Return Encoding.GetBytes(strNewKey)

    End Function

End Class
