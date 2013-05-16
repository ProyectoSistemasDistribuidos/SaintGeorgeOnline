Imports System.Xml.Serialization
Imports System.Xml
Imports System.IO
Imports System.Text

Public Class Deserializer
    ''' <summary>
    ''' funcion para deserializar objetos desde la base de datos
    ''' </summary>
    ''' <typeparam name="T"> el tipo al cual se va castear </typeparam>
    ''' <param name="CadenaSerializado">cadena del objeto serializado</param>
    ''' <returns>retorna la lista correspondiente</returns>
    ''' <remarks> si es una lista debe tener como root ArrayOf Tipo de objeto por ejemplo :ArrayOfSituacion un array de objeto :Situacion</remarks>
    Public Shared Function Deserialize(Of T)(ByVal CadenaSerializado As String) As T

        Dim serializer As New XmlSerializer(GetType(T))

        Dim settings As New XmlReaderSettings()
        Using textReader As New StringReader(CadenaSerializado)

            Dim xmlReader As XmlReader = xmlReader.Create(textReader, settings)




            Return CType(serializer.Deserialize(xmlReader), T)


        End Using

    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <typeparam name="T">tipo del cual se va serializar </typeparam>
    ''' <param name="value">el objeto a serializar</param>
    ''' <returns>la cedna serializada</returns>
    ''' <remarks></remarks>
    Private Shared Function Serialize(Of T)(ByVal value As T) As String



        Dim serializer As New XmlSerializer(GetType(T))
        Dim settings As New XmlWriterSettings


        settings.Encoding = New UnicodeEncoding(False, False)
        settings.Indent = False
        settings.OmitXmlDeclaration = False


        Dim textWriter As New StringWriter()

        Dim xmlWriter As XmlWriter = xmlWriter.Create(textWriter, settings)



        serializer.Serialize(xmlWriter, value)

        Return textWriter.ToString()
    End Function
End Class
