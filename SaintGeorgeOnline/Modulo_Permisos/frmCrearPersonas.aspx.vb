
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Data
Imports System.IO
Imports System.Security.Cryptography
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_Utilities

Partial Class Modulo_Permisos_frmCrearPersonas
    Inherits System.Web.UI.Page
    Public dtPerfil As DataTable
    Public dtTipoPerfil As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            F_cargarCombo()
            F_cargarComboTipoPerfil()
        End If
    End Sub





#Region "Metodos"

#End Region
#Region "delegados"
    Delegate Function crearObjetoFilas(ByVal filas As DataRow, ByVal index As Integer) As Object
    Shared Function obtenerObjetosXfilas(ByVal filas As DataRow, ByVal index As Integer) As Dictionary(Of String, Object)
        Try
            Dim dc As New Dictionary(Of String, Object)

            dc("nombreCompleto") = filas("nombreCompleto")
            dc("estadopAcceso") = filas("estadopAcceso")

            dc("nombrePerfil") = filas("nombrePerfil")
            dc("ensenia") = filas("ensenia")

            dc("codTrab") = filas("codTrab")
            dc("codPersona") = filas("codPersona")

            dc("codPerfil") = filas("codPerfil")
            dc("apellidoPAterno") = filas("apellidoPAterno")

            dc("apellidoMAterno") = filas("apellidoMAterno")
            dc("nombre") = filas("nombre")

            dc("codEnsenia") = filas("codEnsenia")
            dc("codAcceso") = filas("codAcceso")

            dc("correoCorporativo") = filas("correoCorporativo")
            dc("correoSkype") = filas("correoSkype")
            dc("codRelacionPerfil") = filas("codRelacionPerfil")
            ''---------------------------
            dc("usuario") = filas("usuario")
            dc("pass") = filas("pass")

            dc("TJ_CodigoTrabajadoreAsistencia") = filas("TJ_CodigoTrabajadoreAsistencia")

            dc("esAsistente") = filas("esAsistente")


            Return dc

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
#Region "Combos"

    Private Sub F_cargarCombo()
        Try
            Dim nParam As String = "USP_LisCF_Perfiles"
            Dim dc As New Dictionary(Of String, Object)
            dtPerfil = New DataTable
            dtPerfil = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
        Catch ex As Exception


        End Try

    End Sub
    Private Sub F_cargarComboTipoPerfil()
        Try


            Dim nParam As String = "USP_LisTipoPerfil"
            Dim dc As New Dictionary(Of String, Object)
            dtTipoPerfil = New DataTable
            dtTipoPerfil = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)


        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Web Method"
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_listarPersonas(ByVal ApellidoPaterno As String, _
                                        ByVal apellidoMaterno As String, _
                                        ByVal nombrePersona As String, _
                                        ByVal codPerfil As Integer, _
                                        ByVal pagina As Integer, _
                                        ByVal soloPAginas As Integer, ByVal estado As Integer) As Object
        Try
            If soloPAginas = 1 Then
                Dim limInf As Integer = 0
                Dim lismSup As Integer = 0
                lismSup = pagina + 2 * (pagina)

                limInf = pagina + ((10 - 1) * (pagina - 1))
                lismSup = limInf + (10 - 1)

                Dim listaPaginas As New List(Of Integer)

                For indice As Integer = limInf To lismSup
                    listaPaginas.Add(indice)
                Next


                Dim dtPersonas As New DataTable
                Dim nParam As String = "USP_LISPerfilesPersonas"
                Dim dc As New Dictionary(Of String, Object)

                dc.Add("apelldioPaterno", ApellidoPaterno)
                dc.Add("apellidoMaterno", apellidoMaterno)
                dc.Add("nombre", nombrePersona)
                dc.Add("codPerfil", codPerfil)



                dc.Add("paginas", soloPAginas)
                dc.Add("limInf", listaPaginas(0))
                dc.Add("limSup", listaPaginas(listaPaginas.Count - 1))
                dc.Add("estado", estado)



                dtPersonas = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
                Dim sqlObject As List(Of Dictionary(Of String, Object))
                sqlObject = dtPersonas.AsEnumerable().Select(AddressOf obtenerObjetosXfilas).ToList

                '              <div style= " width:45px;height:35px; float:left  ">

                '</div>
                ' <div style= " width:300px;height:35px; ; float:left ">

                '</div>
                ' <div style= " width:150px;height:35px; ; float:left ">

                '</div>
                '    <div style= " width:150px;height:35px; ; float:left ">

                '</div>
                '  <div style= " width:45px;height:35px; float:left  ">

                '</div>

                ' cellpadding="0" cellspacing="0" border="0"
                Dim ui As New XElement("table", New XAttribute("cellpadding", "0"), _
                                          New XAttribute("cellspacing", "0"), _
                                            New XAttribute("border", "0"), New XAttribute("id", "TiposControlesActual"), New XAttribute("style", "width:690px"))
                ui.Add(New XElement("thead", New XAttribute("class", "miGridviewBusquedaActualizacion_Ficha_Header"), _
                                     New XElement("tr", _
                                               New XElement("th", New XElement("div", New XAttribute("style", "width:45px;height:28px;; line-height:33px;; text-align:center"), "Editar")), _
                                                New XElement("th", New XElement("div", New XAttribute("style", "width:300px;height:28px;; line-height:33px ;; text-align:center"), "Nombre Completo")), _
                                                 New XElement("th", New XElement("div", New XAttribute("style", "width:150px;height:28px;; line-height:33px;; text-align:center"), "Perfil")), _
                                                  New XElement("th", New XElement("div", New XAttribute("style", "width:150px;height:28px;; line-height:33px; text-align:center"), "Estado Acceso")), _
                                                   New XElement("th", New XElement("div", New XAttribute("style", "width:45px;height:28px; line-height:33px; text-align:center"), "Enseña")) _
                                                ) _
                                                  ) _
                                                   )
                ui.Add(New XElement("tbody"))
                Dim indice1 As Integer = 0
                For Each o In dtPersonas.Rows
                    ui.Element("tbody").Add(New XElement("tr", New XAttribute("onmouseover", "TiposControlesActualOver(this)"), _
                                                                 New XAttribute("onmouseout", "TiposControlesActualOut(this)"), _
                                                                   New XAttribute("id", indice1), _
                                                           New XElement("td", _
                                                             New XElement("img", _
                                                                            New XAttribute("style", "cursor:pointer;height:25px ;width:25px;"), _
                                                                             New XAttribute("title", "Editar"), _
                                                                              New XAttribute("onclick", "ObtieneFilas(" + CStr(indice1) + ")"), _
                                                                                New XAttribute("src", "../App_Themes/Imagenes/btnEditarRegistroDetalle_0.png")) _
                                                               ), _
                         New XElement("td", o("nombreCompleto")), _
                                                       New XElement("td", o("nombrePerfil")), _
                                                         New XElement("td", o("estadopAcceso")), _
                                                           New XElement("td", o("ensenia")) _
                                                                 ))


                    indice1 += 1
                Next
                Dim cadenas As String = ui.ToString()



                Return New With {.ui = cadenas, .data = sqlObject}

            End If

            If soloPAginas = 0 Then
                ''------------------------------------------------
                Dim dtPersonas As New DataTable
                Dim nParam As String = "USP_LISPerfilesPersonas"
                Dim dc As New Dictionary(Of String, Object)

                dc.Add("apelldioPaterno", ApellidoPaterno)
                dc.Add("apellidoMaterno", apellidoMaterno)
                dc.Add("nombre", nombrePersona)
                dc.Add("codPerfil", codPerfil)

                dc.Add("paginas", soloPAginas)
                dc.Add("limInf", 0)
                dc.Add("limSup", 0)
                dc.Add("estado", estado)


                dtPersonas = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)



                Dim paginasSobra As Integer = 0
                paginasSobra = CInt(dtPersonas.Rows(0)("cantidad")) Mod 10

                Dim numeroPaginas As Integer

                If dtPersonas.Rows(0)("cantidad") > 10 Then
                    numeroPaginas = dtPersonas.Rows(0)("cantidad") / 10
                Else
                    numeroPaginas = 1
                End If
                If dtPersonas.Rows(0)("cantidad") > 10 Then
                    If numeroPaginas Mod 10 <> 0 Then
                        numeroPaginas += 1
                    Else
                    End If
                End If

                Return New With {.count = numeroPaginas, .cantidad = CInt(dtPersonas.Rows(0)("cantidad"))}

                ''-------------------------------------------------
            End If

        Catch ex As Exception

        End Try

    End Function


    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_insertarPersona(ByVal dcPersona As Dictionary(Of String, Object)) As Object
        Dim dcResultado As New Dictionary(Of String, String)
        Try

            dcPersona("apellidoPAterno") = dcPersona("apellidoPAterno").ToString().Trim
            dcPersona("apellidoMAterno") = dcPersona("apellidoMAterno").ToString().Trim
            dcPersona("nombre") = dcPersona("nombre").ToString().Trim()

            Dim dtCoincidencias As DataTable
            Dim nParam As String = "USP_LisCoincidencia"
            Dim dc As New Dictionary(Of String, Object)


            dc.Add("nombre", dcPersona("nombre").ToString().Trim())
            dc.Add("apellidoPaterno", dcPersona("apellidoPAterno").ToString().Trim())
            dc.Add("apellidoMaterno", dcPersona("apellidoMAterno").ToString().Trim())

            If dcPersona("nombre").ToString().Trim().Length = 0 Or dcPersona("apellidoPAterno").ToString().Trim().Length = 0 Or _
                 CInt(dcPersona("codPerfil")) = 0 Then
                Return New With {.codOperacion = 0, .mensaje = "Ingrese los nombre y apellido paterno y el  perfil "}
            End If
            dtCoincidencias = New DataTable
            dtCoincidencias = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim sqlObject As Integer = 0

            If dtCoincidencias.Rows.Count > 0 And CInt(dcPersona("codTrab")) = 0 And CInt(dcPersona("codPersona")) = 0 Then

                sqlObject = (From sh In dtCoincidencias.AsEnumerable() Where sh("TJ_Usuario").ToString().Trim().Length > 0 _
                               Select CInt(obtenerSoloNumeros(sh("TJ_Usuario").ToString()))).ToList().DefaultIfEmpty(0).Max() + 1


            Else
                sqlObject = 1
            End If
            Dim rn As New Random
            ''
            ''
            Dim inicial As Integer = rn.Next(1, 7)
            Dim lista = Enumerable.Range(inicial, 3)
            Dim numeros As String = ""
            numeros = lista.Aggregate(Function(a, b) a.ToString() & "" & b.ToString())



            Dim apellidoPatNormal As String = ""
            Dim nombreNor As String = ""
            apellidoPatNormal = dcPersona("apellidoPAterno").ToString() _
            .ToLower().Trim().Replace("'", "") _
            .Replace("á", "a").Replace("é", "e") _
            .Replace("í", "i").Replace("ó", "o") _
            .Replace("ú", "u").Replace(" ", "").Replace("´", "")




            nombreNor = dcPersona("nombre").ToString().ToLower().Trim(). _
                         Replace("'", "") _
                        .Replace("á", "a") _
                        .Replace("é", "e") _
                        .Replace("í", "i") _
                        .Replace("ó", "o") _
                        .Replace("ú", "u") _
                        .Replace(" ", "") _
                        .Replace("´", "")

            Dim usuario As String = ""
            usuario &= "U"
            usuario &= apellidoPatNormal.Substring(0, 2).ToLower
            usuario &= nombreNor.Substring(0, 2).ToLower
            Dim soloLetras As String = usuario
            usuario &= (sqlObject).ToString().PadLeft(3, "0").ToLower





            Dim total As String = ""
            Dim pass As String = ""
            total = dcPersona("apellidoPAterno").ToString().Trim() & dcPersona("apellidoMAterno").ToString().Trim().ToLower() & nombreNor
            Dim totalNorm As String = ""

            totalNorm = total.ToLower().Replace("'", "").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace(" ", "").Replace("´", "")
            Dim cr As New Cripto
            Dim ids As Integer = 0




            pass = totalNorm.Substring(0, 5) & numeros & "$"
            If (CInt(dcPersona("codTrab")) = 0 And CInt(dcPersona("codPersona")) = 0) Then
                dcPersona("pass") = cr.Encriptar(New RC2CryptoServiceProvider, pass)
                dcPersona("usuario") = usuario
                dc.Clear()
                nParam = "USP_lisVerificaUsuario"
                dc.Add("TJ_Usuario", soloLetras)
                Dim dtUsuario As New DataTable
                dtUsuario = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
                If dtUsuario.Rows.Count > 1 Then
                    sqlObject = (From sh In dtUsuario.AsEnumerable() Where sh("TJ_Usuario").ToString().Trim().Length > 0 _
                                Select CInt(obtenerSoloNumeros(sh("TJ_Usuario").ToString()))).ToList().DefaultIfEmpty(0).Max() + 1


                    usuario = ""
                    usuario &= "U"
                    usuario &= apellidoPatNormal.Substring(0, 2).ToLower
                    usuario &= nombreNor.Substring(0, 2).ToLower
                    usuario &= (sqlObject).ToString().PadLeft(3, "0").ToLower
                    dcPersona("usuario") = usuario
                End If
                dcPersona("usuario") = usuario
            End If

            Dim j As Integer = 9

            '48  0
            '49  1
            '50  2
            '51  3
            '52  4
            '53  5
            '54  6
            '55  7
            '56  8
            '57  9

            'If TypeOf (j).ToString Is Integer Then

            'End If


            dcResultado = New bl_Persona().F_insertarPersona(dcPersona)

            Return dcResultado

        Catch ex As Exception
            Return dcResultado

        End Try
    End Function
    Shared Function obtenerSoloNumeros(ByVal letra As String) As Integer
        Try
            Dim car As New List(Of Integer)
            car.Add(48)
            car.Add(49)
            car.Add(50)
            car.Add(51)
            car.Add(52)
            car.Add(53)
            car.Add(54)
            car.Add(55)
            car.Add(56)
            car.Add(57)
            Dim cadena, split As String
            For indice As Integer = 0 To letra.Length - 1
                split = letra.Substring(indice, 1)
                If car.Contains(Asc(split)) Then
                    cadena &= split
                End If
            Next
            Return cadena
        Catch ex As Exception

        End Try
    End Function





    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_insertarPefil(ByVal dcPerfil As Dictionary(Of String, Object)) As Object
        Try
            Dim ids As Integer = 0

            If dcPerfil("nombrePerfil").ToString().Trim().Length = 0 Then
                Return New With {.codOperacion = ids, .mensaje = "Por favor ponga el nombre del perfil"}
            End If

            ids = New bl_Persona().F_insetarPerfil(dcPerfil)
            If ids > 0 Then
                Return New With {.codOperacion = ids, .mensaje = "Operacion Realizada Correctamente"}
            Else
                Return New With {.codOperacion = ids, .mensaje = "Operacion Erronea"}
            End If

        Catch ex As Exception
            Return New With {.codOperacion = 0, .mensaje = "Operacion Erronea,error del sistema " & ex.Message.ToString()}
        End Try
    End Function



    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_listarPefil(ByVal codPerfil As Integer) As Object
        Dim dtPerfilW As DataTable
        Try
            Dim cadenas As String = ""
            Dim nParam As String = "USP_LisCF_Perfiles"
            Dim dc As New Dictionary(Of String, Object)
            dtPerfilW = New DataTable
            dtPerfilW = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim xUIPerfil As New XElement("select", New XAttribute("id", "cmbPerfil"), New XAttribute("style", "width:192px"))
            xUIPerfil.Add(New XElement("option", New XAttribute("codTipoPerfil", "0"), New XAttribute("value", "0"), "--------------Seleccione-------------"))
            Dim ind As Integer = 0
            For Each filaPerfil As DataRow In dtPerfilW.Rows
                If filaPerfil("PS_CodigoPerfil") = codPerfil Then
                    xUIPerfil.Add(New XElement("option", _
                                               New XAttribute("id", ind), _
                                                 New XAttribute("selected", "selected"), _
                                  New XAttribute("codTipoPerfil", CInt(filaPerfil("codTipoPerfil"))), _
                                  New XAttribute("value", filaPerfil("PS_CodigoPerfil")), _
                                  filaPerfil("PS_Descripcion")))
                Else
                    xUIPerfil.Add(New XElement("option", _
                                             New XAttribute("id", ind), _
                                New XAttribute("codTipoPerfil", CInt(filaPerfil("codTipoPerfil"))), _
                                New XAttribute("value", filaPerfil("PS_CodigoPerfil")), _
                                filaPerfil("PS_Descripcion")))
                End If
                ind += 1
            Next

            Dim cadenasII = xUIPerfil.ToString()



            Return New With {.html = cadenasII}

        Catch ex As Exception

        End Try
    End Function

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_listarCoincidencia(ByVal nombre As String, ByVal apPaterno As String, ByVal apMaterno As String, ByVal codTrab As Integer) As Object
        Dim dtCoincidencias As New DataTable
        Try
            Dim cadenas As String = ""
            Dim nParam As String = "USP_LisCoincidencia"

            Dim dc As New Dictionary(Of String, Object)
            dc.Add("nombre", nombre)
            dc.Add("apellidoPaterno", apPaterno)
            dc.Add("apellidoMaterno", apMaterno)

            dtCoincidencias = New DataTable
            dtCoincidencias = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)


            Dim UsuariosPass = From sql In dtCoincidencias.AsEnumerable() _
                              Select New With {.corr = sql("").ToString().Substring(4, sql("TJ_Usuario").ToString().Length - 1)}

        Catch ex As Exception

        End Try
    End Function

    ''resetar pass
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_ResetPassword(ByVal dcPersona As Dictionary(Of String, Object)) As Object
        Try
            Dim dtCoincidencias As DataTable
            Dim nParam As String = "USP_LisCoincidencia"
            Dim dc As New Dictionary(Of String, Object)
            dc.Add("nombre", dcPersona("nombre").ToString().Trim())
            dc.Add("apellidoPaterno", dcPersona("apellidoPAterno").ToString().Trim())
            dc.Add("apellidoMaterno", dcPersona("apellidoMAterno").ToString().Trim())
            If dcPersona("nombre").ToString().Trim().Length = 0 Or dcPersona("apellidoPAterno").ToString().Trim().Length = 0 Then 'Or _
                '  dcPersona("apellidoMAterno").ToString().Trim().Length = 0 Then
                Return New With {.codOperacion = 0, .mensaje = "Ingrese los nombre y apellido paterno y materno "}
            End If
            dtCoincidencias = New DataTable
            dtCoincidencias = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim sqlObject As Integer = 0

            If dtCoincidencias.Rows.Count > 0 Then

                sqlObject = (From sh In dtCoincidencias.AsEnumerable() Where sh("TJ_Usuario").ToString().Trim().Length > 0 _
                                Select CInt(obtenerSoloNumeros(sh("TJ_Usuario").ToString()))).ToList().DefaultIfEmpty(0).Max() + 1

            Else
                sqlObject = 1
            End If
            Dim rn As New Random
            ''
            ''

            Dim inicial As Integer = rn.Next(1, 7)
            Dim lista = Enumerable.Range(inicial, 3)
            Dim numeros As String = ""
            numeros = lista.Aggregate(Function(a, b) a.ToString() & "" & b.ToString())


            Dim apellidoPatNormal As String = ""
            Dim nombreNor As String = ""

            apellidoPatNormal = dcPersona("apellidoPAterno").ToString() _
            .ToLower().Trim().Replace("'", "") _
            .Replace("á", "a").Replace("é", "e") _
            .Replace("í", "i").Replace("ó", "o") _
            .Replace("ú", "u").Replace(" ", "").Replace("´", "")


            nombreNor = dcPersona("nombre").ToString().ToLower().Trim(). _
                         Replace("'", "") _
                        .Replace("á", "a") _
                        .Replace("é", "e") _
                        .Replace("í", "i") _
                        .Replace("ó", "o") _
                        .Replace("ú", "u") _
                        .Replace(" ", "") _
                        .Replace("´", "")

            Dim usuario As String = ""
            usuario &= "U"
            usuario &= apellidoPatNormal.Substring(0, 2).ToLower
            usuario &= nombreNor.Substring(0, 2).ToLower
            usuario &= (sqlObject).ToString().PadLeft(3, "0").ToLower



            Dim total As String = ""
            Dim pass As String = ""
            total = dcPersona("apellidoPAterno").ToString().Trim() & dcPersona("apellidoMAterno").ToString().Trim().ToLower() & nombreNor
            Dim totalNorm As String = ""

            totalNorm = total.ToLower().Replace("'", "").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace(" ", "").Replace("´", "")

            Dim cr As New Cripto
            Dim ids As Integer = 0
            pass = totalNorm.Substring(0, 5) & numeros & "$"



            ' If CInt(dcPersona("codTrab")) = 0 And CInt(dcPersona("codPersona")) = 0 Then


            ', "@TJ_CodigoTrabajador", DbType.Int32, dcUsuarioPass("codTrab"))
            'dbBase.AddInParameter(dbCommand, "@TJ_Usuario", DbType.String, dcUsuarioPass("usuario"))
            'dbBase.AddInParameter(dbCommand, "@TJ_Contrasenia", DbType.String, dcUsuarioPass("pass"))



            dcPersona("pass") = cr.Encriptar(New RC2CryptoServiceProvider, pass)



            '  dcPersona("usuario") = usuario

            '   End If


            Dim dcRes As New Dictionary(Of String, String)
            Dim codOperacion As Integer = 0
            ' dcRes = New bl_Persona().F_ActualizarUsuarioPass(dcPersona)


            codOperacion = New bl_Persona().F_ActualizarUsuarioPass(dcPersona)

            If codOperacion > 0 Then
                dcRes("codigo") = codOperacion
                dcRes("mensaje") = "se cambio de pass correctamente"
            Else
                dcRes("codigo") = codOperacion
                dcRes("mensaje") = "Error intente de nuevo "
            End If
            Return dcRes

            'If ids > 0 Then
            '    Return New With {.codOperacion = ids, .mensaje = "Se cambio correctamente el password"}
            'ElseIf (ids = 0) Then
            '    Return New With {.codOperacion = ids, .mensaje = "Operacion Erronea"}

            'End If

        Catch ex As Exception
            Return New With {.codigo = 0, .mensaje = "Operacion Erronea,error del sistema " & ex.Message.ToString()}
        End Try
    End Function
    ''
#End Region
#Region "funciones"

    Private Function autogenerarPassword(ByVal str_ApellidoPaterno As String) As String
        Randomize()

        Dim int_Aleatorio As Integer = CInt(Int((999 * Rnd()) + 100))

        Dim int_PwLength As Integer = str_ApellidoPaterno.Length
        Dim str_Password As String

        If int_PwLength >= 5 Then ' Si el apellido paterno tiene más de 5 caracteres
            str_Password = str_ApellidoPaterno.Substring(0, 5)
        Else ' si tiene menos de 5 caracteres
            str_Password = str_ApellidoPaterno.Substring(0, int_PwLength)
        End If

        str_Password = str_Password & int_Aleatorio.ToString

        Dim cr As New Cripto
        str_Password = cr.Encriptar(New RC2CryptoServiceProvider, str_Password)
        Return str_Password
    End Function


#End Region
End Class
