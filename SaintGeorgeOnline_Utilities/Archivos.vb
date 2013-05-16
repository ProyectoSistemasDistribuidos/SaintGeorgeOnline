Imports System.IO
Imports System.Web
Imports System.Text
Imports Ionic.Zip

Public Class Archivos

#Region "MemoryStream"

    Private Shared Function StreamToMemory(ByVal input As Stream) As System.IO.MemoryStream

        Dim buffer(1023) As Byte
        Dim count As Integer = 1024
        Dim output As MemoryStream

        ' Construir el nuevo Stream
        If input.CanSeek Then
            output = New MemoryStream(input.Length)
        Else
            output = New MemoryStream
        End If

        ' Iterar el Stream y transferir los bytes al MemoryStream 
        Do
            count = input.Read(buffer, 0, count)
            If count = 0 Then Exit Do
            output.Write(buffer, 0, count)
        Loop

        ' Setear la posicion del stream a 0
        output.Position = 0

        Return output

    End Function

    Public Shared Function FileStreamToMemory(ByVal str_RutaArchivo As String) As System.IO.MemoryStream

        Dim FStream As FileStream
        Dim MStream As MemoryStream
        FStream = New FileStream(str_RutaArchivo, FileMode.Open, FileAccess.Read, FileShare.Read)
        MStream = StreamToMemory(FStream)
        FStream.Close()
        Return MStream

    End Function

    Public Shared Function StringToMemoryStream(ByVal str_Cadena As String) As System.IO.MemoryStream

        Dim MStream As New System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(str_Cadena))
        Return MStream

    End Function

    Public Shared Function MemoryStreamToString(ByVal ms_Data As System.IO.MemoryStream) As String

        Return System.Text.Encoding.ASCII.GetString(ms_Data.ToArray)

    End Function

    Public Shared Function StringToArrByte(ByVal str_Data As String) As Byte()

        Dim arr_Data() As Byte = System.Text.UTF8Encoding.ASCII.GetBytes(str_Data)
        Return arr_Data

    End Function

#End Region

    Class Subir

        Public Overloads Shared Function Grabar(ByVal arrBytData() As Byte, ByVal str_Ruta As String, ByVal str_FileName As String) As Boolean

            If My.Computer.FileSystem.DirectoryExists(str_Ruta) Then
                Dim newFile As New FileStream(str_Ruta & "\" & str_FileName, FileMode.Create)
                newFile.Write(arrBytData, 0, arrBytData.Length)
                newFile.Close()
                Return True
            Else
                Return False
            End If

        End Function

    End Class

    Class Descargar

#Region "Zipeo (Sobrecargado)"

        Public Overloads Shared Sub Comprimir(ByVal ms_Data As System.IO.MemoryStream, ByVal str_FileName As String, ByVal str_ZipFileName As String)

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            Response.Clear()
            Response.BufferOutput = False
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/zip"
            Response.AddHeader("content-disposition", "filename=" & str_ZipFileName & ";")
            Using zip As New ZipFile()
                zip.AddEntry(str_FileName, ms_Data)
                zip.Save(Response.OutputStream)
            End Using
            Response.Close()

            Response.End()

        End Sub

        Public Overloads Shared Sub Comprimir(ByVal arr_Data() As Byte, ByVal str_FileName As String, ByVal str_ZipFileName As String)

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            Response.Clear()
            Response.BufferOutput = False
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/zip"
            Response.AddHeader("content-disposition", "filename=" & str_ZipFileName & ";")
            Using zip As New ZipFile()
                zip.AddEntry(str_FileName, arr_Data)
                zip.Save(Response.OutputStream)
            End Using
            Response.Close()

            Response.End()

        End Sub

        Public Overloads Shared Sub Comprimir(ByVal str_Data As String, ByVal str_FileName As String, ByVal str_ZipFileName As String)

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            Response.Clear()
            Response.BufferOutput = False
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/zip"
            Response.AddHeader("content-disposition", "filename=" & str_ZipFileName & ";")
            Using zip As New ZipFile()
                zip.AddEntry(str_FileName, str_Data)
                zip.Save(Response.OutputStream)
            End Using
            Response.Close()

            Response.End()

        End Sub

#End Region

#Region "Archivos Planos (Sobrecargado)"

        Public Overloads Shared Sub Plano(ByVal str_Data As String, ByVal str_FileName As String)

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            Response.Clear()
            Response.BufferOutput = False
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & str_FileName & ";")
            Response.Write(str_Data)
            Response.Flush()
            Response.Close()

            Response.End()

        End Sub

        Public Overloads Shared Sub Plano(ByVal arr_Data() As Byte, ByVal str_FileName As String)

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            Response.Clear()
            Response.BufferOutput = False
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & str_FileName & ";")
            Response.BinaryWrite(arr_Data)
            Response.Flush()
            Response.Close()

            Response.End()

        End Sub

#End Region

#Region "Word / Excel / PDF"

        Public Overloads Shared Sub Word(ByVal arr_Data() As Byte, ByVal str_FileName As String)

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            Response.Clear()
            Response.BufferOutput = False
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & str_FileName & ";")
            Response.BinaryWrite(arr_Data)
            Response.Flush()
            Response.Close()

        End Sub

        Public Overloads Shared Sub Excel(ByVal arr_Data() As Byte, ByVal str_FileName As String)

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            Response.Clear()
            Response.BufferOutput = False
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "binary/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & str_FileName & ";")
            Response.BinaryWrite(arr_Data)
            Response.Flush()
            Response.Close()

        End Sub

        Public Overloads Shared Sub PDF(ByVal ms_Data As System.IO.MemoryStream, ByVal str_FileName As String)

            Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & str_FileName & ";")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.OutputStream.Write(ms_Data.GetBuffer(), 0, ms_Data.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.OutputStream.Close()
            Response.End()

        End Sub

#End Region

    End Class

End Class
