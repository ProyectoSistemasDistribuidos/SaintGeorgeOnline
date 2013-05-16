Imports System.Net.Mail
Imports System.Configuration.ConfigurationManager
Imports System
Imports System.IO
Imports System.Security.Cryptography

Public Class EnvioEmail

    ''' <summary>
    ''' Envia emails a los usuarios especificados (reciba un (1) email )
    ''' </summary>
    ''' <param name="Email">Email de usuario a enviar el email</param>
    ''' <param name="cuerpo">Cuerpo del email a enviar</param>
    ''' <param name="Asunto">Asunto del email a enviar</param>
    ''' <returns>Indicador del resultado del envío del email</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function SendEmail(ByVal Email As String, ByVal cuerpo As String, ByVal Asunto As String) As Integer

        Dim MailMsg As New MailMessage()
        Dim encript As New Cripto
        Dim str_SMTPServer As String = ""
        Dim str_EmailSaliente As String = ""
        Dim str_UserSaliente As String = ""
        Dim str_PasswordSaliente As String = ""

        str_SMTPServer = Configuration.ConfigurationManager.AppSettings.Item("SMTP_SERVER").ToString()
        str_UserSaliente = Configuration.ConfigurationManager.AppSettings.Item("USER_SAL").ToString()
        str_PasswordSaliente = Configuration.ConfigurationManager.AppSettings.Item("PASSWORD_SAL").ToString()
        str_EmailSaliente = Configuration.ConfigurationManager.AppSettings.Item("EMAIL_FROM_SAL").ToString()

        str_SMTPServer = encript.Desencriptar(New RC2CryptoServiceProvider, str_SMTPServer)
        str_UserSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_UserSaliente)
        str_PasswordSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_PasswordSaliente)
        str_EmailSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_EmailSaliente)

        With MailMsg
            .From = New MailAddress(str_EmailSaliente, "Soporte - Colegio San Jorge - Miraflores")
            .Subject = Asunto
            .BodyEncoding = System.Text.Encoding.UTF8
            .Body = cuerpo
            .IsBodyHtml = True
            .To.Add(New MailAddress(Email, "", System.Text.Encoding.UTF8))
        End With

        Try
            Dim SmtpMail As New SmtpClient()
            Dim mCredential As New Net.NetworkCredential(str_UserSaliente, str_PasswordSaliente)
            SmtpMail.Credentials = mCredential
            SmtpMail.Host = str_SMTPServer
            'SmtpMail.Send(MailMsg)

            Return 1

        Catch ex As Exception

            Return 0

        End Try

    End Function


    ''' <summary>
    ''' Envia emails a los usuarios especificados (se envia un arreglo de emails )
    ''' </summary>
    ''' <param name="Emails">Email de usuario a enviar el email</param>
    ''' <param name="cuerpo">Cuerpo del email a enviar</param>
    ''' <param name="Asunto">Asunto del email a enviar</param>
    '''  <param name="AttachmentFile">Nombre del Archivo a adjuntar</param>
    ''' <returns>Indicador del resultado del envío del email</returns>
    ''' <remarks>
    ''' Creador:               Fanny Salinas
    ''' Fecha de Creación:     29/12/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function SendEmail(ByVal Emails As ArrayList, ByVal cuerpo As String, ByVal Asunto As String, ByVal AttachmentFile As String) As Integer

        Dim MailMsg As New MailMessage()
        Dim encript As New Cripto
        Dim str_SMTPServer As String = ""
        Dim str_EmailSaliente As String = ""
        Dim str_UserSaliente As String = ""
        Dim str_PasswordSaliente As String = ""
        Dim MailAttachment As Attachment = New Attachment(AttachmentFile)

        str_SMTPServer = Configuration.ConfigurationManager.AppSettings.Item("SMTP_SERVER").ToString()
        str_UserSaliente = Configuration.ConfigurationManager.AppSettings.Item("USER_SAL").ToString()
        str_PasswordSaliente = Configuration.ConfigurationManager.AppSettings.Item("PASSWORD_SAL").ToString()
        str_EmailSaliente = Configuration.ConfigurationManager.AppSettings.Item("EMAIL_FROM_SAL").ToString()

        str_SMTPServer = encript.Desencriptar(New RC2CryptoServiceProvider, str_SMTPServer)
        str_UserSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_UserSaliente)
        str_PasswordSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_PasswordSaliente)
        str_EmailSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_EmailSaliente)

        With MailMsg
            .From = New MailAddress(str_EmailSaliente, "Soporte - Colegio San Jorge - Miraflores") 'Cambiar por el correo del usuario logeado
            .Subject = Asunto
            .BodyEncoding = System.Text.Encoding.UTF8
            .Body = cuerpo
            .IsBodyHtml = True
            .Attachments.Add(MailAttachment)

        End With

        Dim SmtpMail As New SmtpClient()
        Dim mCredential As New Net.NetworkCredential(str_UserSaliente, str_PasswordSaliente)
        SmtpMail.Credentials = mCredential
        SmtpMail.Host = str_SMTPServer

        Try

            'For i As Integer = 0 To Emails.Count - 1
            '    MailMsg.To.Clear()
            '    MailMsg.To.Add(New MailAddress(Emails(i), "", System.Text.Encoding.UTF8))
            '    SmtpMail.Send(MailMsg)
            'Next

            'For Each email As String In Emails
            '    MailMsg.To.Add(New MailAddress(email, "", System.Text.Encoding.UTF8))
            '    SmtpMail.Send(MailMsg)
            'Next

            Return 1

        Catch ex As Exception

            Return 0

        End Try

    End Function


    ''' <summary>
    ''' Envia emails a los usuarios especificados (se envia un arreglo de emails )
    ''' </summary>
    ''' <param name="Emails">Email de usuario a enviar el email</param>
    ''' <param name="cuerpo">Cuerpo del email a enviar</param>
    ''' <param name="Asunto">Asunto del email a enviar</param>
    ''' <returns>Indicador del resultado del envío del email</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     12/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Shared Function SendEmail(ByVal Emails As ArrayList, ByVal cuerpo As String, ByVal Asunto As String) As Integer

        Dim MailMsg As New MailMessage()
        Dim encript As New Cripto
        Dim str_SMTPServer As String = ""
        Dim str_EmailSaliente As String = ""
        Dim str_UserSaliente As String = ""
        Dim str_PasswordSaliente As String = ""

        str_SMTPServer = Configuration.ConfigurationManager.AppSettings.Item("SMTP_SERVER").ToString()
        str_UserSaliente = Configuration.ConfigurationManager.AppSettings.Item("USER_SAL").ToString()
        str_PasswordSaliente = Configuration.ConfigurationManager.AppSettings.Item("PASSWORD_SAL").ToString()
        str_EmailSaliente = Configuration.ConfigurationManager.AppSettings.Item("EMAIL_FROM_SAL").ToString()

        str_SMTPServer = encript.Desencriptar(New RC2CryptoServiceProvider, str_SMTPServer)
        str_UserSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_UserSaliente)
        str_PasswordSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_PasswordSaliente)
        str_EmailSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_EmailSaliente)

        With MailMsg
            .From = New MailAddress(str_EmailSaliente, "Soporte - Colegio San Jorge - Miraflores") 'Cambiar por el correo del usuario logeado
            .Subject = Asunto
            .BodyEncoding = System.Text.Encoding.UTF8
            .Body = cuerpo
            .IsBodyHtml = True
        End With

        Dim SmtpMail As New SmtpClient()
        Dim mCredential As New Net.NetworkCredential(str_UserSaliente, str_PasswordSaliente)
        SmtpMail.Credentials = mCredential
        SmtpMail.Host = str_SMTPServer

        Try
            'For Each email As String In Emails
            '    MailMsg.To.Add(New MailAddress(email, "", System.Text.Encoding.UTF8))
            '    'SmtpMail.Send(MailMsg)
            'Next

            For i As Integer = 0 To Emails.Count - 1
                MailMsg.To.Clear()
                MailMsg.To.Add(New MailAddress(Emails(i), "", System.Text.Encoding.UTF8))
                SmtpMail.Send(MailMsg)
            Next

            Return 1

        Catch ex As Exception

            Return 0

        End Try

    End Function

    ''' <summary>
    ''' Envia emails a los usuarios especificados (se envia un arreglo de emails )
    ''' </summary>
    ''' <param name="Emails">Email de usuario a enviar el email</param>
    ''' <param name="cuerpo">Cuerpo del email a enviar</param>
    ''' <param name="Asunto">Asunto del email a enviar</param>
    ''' <returns>Indicador del resultado del envío del email</returns>
    ''' <remarks>
    ''' Creador:               Juan Vento
    ''' Fecha de Creación:     12/04/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function SendEmail(ByVal Emails As String(), ByVal cuerpo As String, ByVal Asunto As String) As Integer

        Dim MailMsg As New MailMessage()
        Dim encript As New Cripto
        Dim str_SMTPServer As String = ""
        Dim str_EmailSaliente As String = ""
        Dim str_UserSaliente As String = ""
        Dim str_PasswordSaliente As String = ""

        str_SMTPServer = Configuration.ConfigurationManager.AppSettings.Item("SMTP_SERVER").ToString()
        str_UserSaliente = Configuration.ConfigurationManager.AppSettings.Item("USER_SAL").ToString()
        str_PasswordSaliente = Configuration.ConfigurationManager.AppSettings.Item("PASSWORD_SAL").ToString()
        str_EmailSaliente = Configuration.ConfigurationManager.AppSettings.Item("EMAIL_FROM_SAL").ToString()

        str_SMTPServer = encript.Desencriptar(New RC2CryptoServiceProvider, str_SMTPServer)
        str_UserSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_UserSaliente)
        str_PasswordSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_PasswordSaliente)
        str_EmailSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_EmailSaliente)

        With MailMsg
            .From = New MailAddress(str_EmailSaliente, "Soporte - Colegio San Jorge - Miraflores") 'Cambiar por el correo del usuario logeado
            .Subject = Asunto
            .BodyEncoding = System.Text.Encoding.UTF8
            .Body = cuerpo
            .IsBodyHtml = True
        End With

        Dim SmtpMail As New SmtpClient()
        Dim mCredential As New Net.NetworkCredential(str_UserSaliente, str_PasswordSaliente)
        SmtpMail.Credentials = mCredential
        SmtpMail.Host = str_SMTPServer

        Try
            For Each email As String In Emails
                MailMsg.To.Add(New MailAddress(email, "", System.Text.Encoding.UTF8))
                SmtpMail.Send(MailMsg)
            Next

            Return 1

        Catch ex As Exception

            Return 0

        End Try

    End Function

    ''' <summary>
    ''' Envia emails a los usuarios especificados (reciba un (1) email y un email de copiado)
    ''' </summary>
    ''' <param name="str_email">Email de usuario a enviar el email</param>
    ''' <param name="str_cuerpo">Cuerpo del email a enviar</param>
    ''' <param name="str_Asunto">Asunto del email a enviar</param>
    ''' <param name="str_emailCopia">Email de usuario a copiar el email</param>
    ''' <returns>Indicador del resultado del envío del email</returns>
    ''' <remarks>
    ''' Creador:               Johnatan Matta
    ''' Fecha de Creación:     06/01/2011
    ''' Modificado por:        _____________
    ''' Fecha de modificación: _____________ 
    ''' </remarks>
    Public Function SendEmail_OlvidoContrasenia(ByVal str_email As String, ByVal str_cuerpo As String, ByVal str_Asunto As String, ByVal str_emailCopia As String) As Integer

        Dim MailMsg As New MailMessage()
        Dim contEmail As Integer = 0
        Dim encript As New Cripto
        Dim str_SMTPServer As String = ""
        Dim str_EmailSaliente As String = ""
        Dim str_UserSaliente As String = ""
        Dim str_PasswordSaliente As String = ""
        'Dim MsgAttach As New Attachment("D:\Proyecto\SaintGeorgeOnline\SaintGeorgeOnline\App_Themes\Imagenes\Logos\St Georges logo 2.jpg")

        str_SMTPServer = Configuration.ConfigurationManager.AppSettings.Item("SMTP_SERVER").ToString()
        str_UserSaliente = Configuration.ConfigurationManager.AppSettings.Item("USER_SAL").ToString()
        str_PasswordSaliente = Configuration.ConfigurationManager.AppSettings.Item("PASSWORD_SAL").ToString()
        str_EmailSaliente = Configuration.ConfigurationManager.AppSettings.Item("EMAIL_FROM_SAL").ToString()

        str_SMTPServer = encript.Desencriptar(New RC2CryptoServiceProvider, str_SMTPServer)
        str_UserSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_UserSaliente)
        str_PasswordSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_PasswordSaliente)
        str_EmailSaliente = encript.Desencriptar(New RC2CryptoServiceProvider, str_EmailSaliente)

        With MailMsg
            .From = New MailAddress(str_EmailSaliente, "Colegio San Jorge - Miraflores")
            .Subject = str_Asunto
            .BodyEncoding = System.Text.Encoding.UTF8
            .Body = str_cuerpo
            .IsBodyHtml = True
            .To.Add(New MailAddress(str_email, "", System.Text.Encoding.UTF8))

            If str_emailCopia.Length > 4 Then
                .CC.Add(New MailAddress(str_emailCopia, "", System.Text.Encoding.UTF8))
            End If

            '.Attachments.Add(MsgAttach)
        End With

        Try
            Dim SmtpMail As New SmtpClient()
            Dim mCredential As New Net.NetworkCredential(str_UserSaliente, str_PasswordSaliente)
            SmtpMail.Credentials = mCredential
            SmtpMail.Host = str_SMTPServer
            SmtpMail.Send(MailMsg)

            Return 1

        Catch ex As Exception
            Return 0
        End Try
    End Function

End Class