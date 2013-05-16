Imports System.Data
Imports System.Globalization
Imports SaintGeorgeOnline_BusinessLogic
Imports SaintGeorgeOnline_BusinessLogic.ModuloNotas
Imports SaintGeorgeOnline_BusinessLogic.ModuloColegio
Imports SaintGeorgeOnline_Utilities
Imports System.Web.Script.Services
Imports System.Web.Services

Partial Class Modulo_BancoLibros_frmPrestamoLibro
    Inherits System.Web.UI.Page
    Public dtAnio As DataTable
    Public dtGrado As DataTable
    Public fechaActual As String
#Region "cargar combos "
    Private Sub cargarComboAniosAcademico()
        Try
            dtAnio = New DataTable
            Dim obj_BL_Grados As New bl_AniosAcademicos
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_AniosAcademicos("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
            dtAnio = ds_Lista.Tables(0)
        Catch ex As Exception

        End Try



    End Sub


    Private Sub cargarComboGrado()

        Try
            Dim obj_BL_Grados As New bl_Grados
            Dim int_CodigoTipoUsuario As Integer = Me.Master.Obtener_CodigoTipoUsuarioLogueado
            Dim int_CodigoUsuario As Integer = Me.Master.Obtener_CodigoUsuarioLogueado
            Dim ds_Lista As DataSet = obj_BL_Grados.FUN_LIS_Grados("", 1, int_CodigoUsuario, int_CodigoTipoUsuario, 1, 1)
            dtGrado = New DataTable
            dtGrado = ds_Lista.Tables(0)



        Catch ex As Exception

        End Try
    End Sub
#End Region




#Region "cargar combos dependientes"


#Region "web services "

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function FListarAulasXGrado(ByVal codGrado As Integer, ByVal codAnio As Integer) As Object
        Dim dtAulas As DataTable
        Dim xel As XElement
        Try
            dtAulas = New DataTable

            Dim dc As New Dictionary(Of String, Object)

            Dim nParam As String = "USP_LisAulasGrado"

            dc("pCodGrado") = codGrado
            dc("pCodAnioAcademico") = codAnio

            dtAulas = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)


            xel = New XElement("select", New XAttribute("id", "cmbAula"), _
                                New XAttribute("class", "combo"), _
                               (From x In dtAulas.AsEnumerable() Select New XElement("option", _
                                                                                                New XAttribute("value", x("AU_CodigoAula")), x("AU_Descripcion"))))
            If dtAulas.Rows.Count > 0 Then
                Return New With {.html = xel.ToString()}
            Else
                Return New With {.html = f_crearComboDefault("cmbAula", "-----------------Todos----------------------", "0", "combo").ToString()}
            End If



        Catch ex As Exception

        End Try
    End Function


    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_listarAlumnosXAula(ByVal codAula As Integer, ByVal codAnio As Integer, ByVal codGrado As Integer) As Object
        Dim dtAlumnos As DataTable
        Dim xel As XElement
        Try
            dtAlumnos = New DataTable
            Dim nParam As String = "USP_LisAlumnosAulaNuevo"
            Dim dc As New Dictionary(Of String, Object)
            dc("codAula") = codAula
            dc("codAnio") = codAnio
            dc("codGrado") = codGrado

            dtAlumnos = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)


           
            xel = New XElement("table", New XAttribute("cellpadding", "0"), _
                        New XAttribute("cellspacing", "0"), _
                        New XAttribute("border", "0"), New XAttribute("id", "tblAlumnos"), (From Al In dtAlumnos.AsEnumerable() Select New XElement("tr", New XAttribute("id", Al("codAlumno").ToString()), New XAttribute("evntArgs", New System.Web.Script.Serialization.JavaScriptSerializer().Serialize( _
                                                                                                                        New With {.codALummo = Al("codAlumno"), .codAnio = Al("codAnio")})), New XAttribute("onmouseover", "Over(this)"), New XAttribute("onmouseout", "Out(this)"), _
                                                                                                                                      New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:250px; float:left; text-align: left; line-height:25px;") _
                                                                                                       , Al("nombreCompleto"))), _
                                                                                                          New XElement("td", New XAttribute("id", Al("codAlumno").ToString()), _
                                                                                           New XElement("div", New XAttribute("id", Al("codAlumno").ToString()), New XAttribute("style", "; font-size:8pt;height:auto; width:75px; float:left; text-align: center; line-height:25px;") _
                                                                                                       , Al("cantidadPrestamo"))), _
                                                                                                       New XElement("td", New XElement("div", _
                                                New XAttribute("style", " height:auto;width:45px;font-size:8pt;text-align:center"), New XElement("img", _
                                                                            New XAttribute("style", "cursor:pointer;height:18px ;width:18px;"), _
                                                                              New XAttribute("title", "Editar"), _
                                                                                New XAttribute("onclick", "F_AbrirVentanaEdicionPrestamos(" & New System.Web.Script.Serialization.JavaScriptSerializer().Serialize( _
                                                                                                New With {.nombrePersona = Al("nombreCompleto"), .codALummo = Al("codAlumno"), .codAnio = Al("codAnio")}) & " )"), _
                                                                                  New XAttribute("src", "../App_Themes/Imagenes/opc_actualizar.png")))))))




            If dtAlumnos.Rows.Count > 0 Then

                Return New With {.html = xel.ToString()}
            Else
                Return New With {.html = (New XElement("table", New XElement("tr", New XElement("td", New XAttribute("colspan", "3"), _
                                                                                                 New XElement("div", New XAttribute("style", "; height:25px; width:775px; text-align:center; color:Red"), "--No hay resultados--"))))).ToString()}

            End If

            '<tr>
            '    <td colspan="3">

            '    </td>
            '</tr>


        Catch ex As Exception

        End Try
    End Function


    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_listarAlumnosXAulaBuscar(ByVal codAula As Integer, ByVal codAnio As Integer, ByVal codGrado As Integer) As Object
        Dim dtAlumnos As DataTable
        Dim xel As XElement
        Try
            dtAlumnos = New DataTable
            Dim nParam As String = "USP_LisAlumnosAula"
            Dim dc As New Dictionary(Of String, Object)
            dc("codAula") = codAula
            dc("codAnio") = codAnio
            dc("codGrado") = codGrado

            dtAlumnos = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)


            xel = New XElement("select", New XAttribute("id", "cmbAula"), _
                           New XAttribute("class", "combo"), New XElement("option", New XAttribute("value", "0"), "-----------------Todos----------------------"), _
                          (From x In dtAlumnos.AsEnumerable() Select New XElement("option", _
                                                                                           New XAttribute("value", x("codAlumno")), x("nombreCompleto"))))

            If dtAlumnos.Rows.Count > 0 Then

                Return New With {.html = xel.ToString()}
            Else
                Return New With {.html = f_crearComboDefault("cmbAula", "-----------------Todos----------------------", "0", "combo").ToString()}

            End If




        Catch ex As Exception

        End Try
    End Function

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_VerificarDisponibilidadLibro(ByVal codBarra As String, ByVal listaCliente As Dictionary(Of String, String)) As Object

        Try


        Catch ex As Exception

        End Try
    End Function


    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_operacionAgregarElemento(ByVal nombreAlumno As String, ByVal codAnio As Integer, ByVal codAlumno As Integer, ByVal codBarra As String, ByVal listaActual As List(Of Dictionary(Of String, Object))) As Object
        Dim dtCodigoBarras As DataTable
        Dim dtPrestado As New DataTable
        Try

            'Dim funcion As Func(Of DataRow, Integer, Dictionary(Of String, Object))
            'funcion = AddressOf obtenerObjetosXfilas

            
            dtCodigoBarras = New DataTable
            Dim nParam As String = "USP_LisLibroBanco"
            Dim dc As New Dictionary(Of String, Object)
            dc("codBarra") = codBarra.Trim()
            Dim existe As Boolean = False

            existe = listaActual.Exists(Function(dicLibros) dicLibros("codigoBarra") = codBarra)
            dtCodigoBarras = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)
            dtPrestado = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(1)

            If dtPrestado.Rows.Count > 0 Then
                Return New With {.mensaje = "El libro por devolver fecha prestado: " & dtPrestado.Rows(0)("fechaSacoLibro").ToString() & " nombre persona que se presto  :" & dtPrestado.Rows(0)("nombrePersona").ToString(), .codOperacion = 0}
            End If

            If dtCodigoBarras.Rows.Count = 0 Then
                Return New With {.mensaje = "EL codigo de barras ingresado no existe ", .codOperacion = 0}
            End If
            Dim dcElementoNuevo As New Dictionary(Of String, Object)
            dcElementoNuevo = dtCodigoBarras.AsEnumerable().Select(AddressOf obtenerObjetosXfilas).First()
            dcElementoNuevo("codALumno") = codAlumno
            dcElementoNuevo("codAnio") = codAnio
            dcElementoNuevo("nombreAlumno") = nombreAlumno
            dcElementoNuevo("Estado") = True

            If Not existe Then
                listaActual.Add(dcElementoNuevo)
            Else
                Return New With {.mensaje = "El item que ingreso ya  esta asignado", .codOperacion = 0}
            End If
            Dim xmlUiLista As New XElement("table", _
                                              New XAttribute("id", "grillaLibros"), _
                                              New XAttribute("cellpadding", "0"), _
                                              New XAttribute("cellspacing", "0"), _
                                              New XAttribute("border", "0"), (listaActual.AsEnumerable().Select(Function(fil, index) _
                                                       New XElement("tr", New XAttribute("onmouseover", "Over(this)") _
                                            , New XAttribute("onmouseout", "Out(this)"), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:75px; float:left; text-align:left; line-height:25px;") _
                                                                                                       , fil("codigoBarra"))), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:85px; float:left; text-align: center; line-height:25px;") _
                                                                                                        , "Estado")), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:250px; float:left; text-align: center; line-height:25px;") _
                                                                                                       , fil("tituloLibro"))), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:125px; float:left; text-align:center; line-height:25px;") _
                                                                                                       , fil("autor"))), _
                                                                                                        New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:175px; float:left; text-align:center; line-height:25px;") _
                                                                                                       , fil("nombreAlumno"))), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:75px; float:left; text-align:center; line-height:25px;") _
                                                                                                       , _
                                                                                                        New XElement("img", _
                                                                                                                      New XAttribute("title", "Eliminar"), _
                                                                                                            New XAttribute("onclick", "fEliminar(" & index.ToString() & ")"), _
                                                                                                            New XAttribute("src", "../App_Themes/Imagenes/opc_eliminar.png"), _
                                                                                                            New XAttribute("style", " cursor:pointer;height:18px; width:18px;") _
                                                                                                                     )))))))
            Return New With {.mensaje = "Se agrego corretamente ", .codOperacion = 1, .ListaACtualizada = listaActual, .html = xmlUiLista.ToString}
        Catch ex As Exception
            Return New With {.mensaje = "Error de sistema " & ex.Message.ToString(), .codOperacion = -1, .ListaACtualizada = Nothing, .html = ""}
        End Try
    End Function

   


   
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_actualizarFechaPrestamoDetalleLibro(ByVal codPrestamo As Integer, ByVal fechaPrestamo As String) As Object
        Dim fech As DateTime
        Dim formatoCorrecto As Boolean = False
        Try
            Dim enUS As New CultureInfo("en-US")

            Dim dcRresultado As New Dictionary(Of Object, Object)

          
            formatoCorrecto = Date.TryParseExact(fechaPrestamo, "dd/MM/yyyy", enUS, _
                      DateTimeStyles.AllowLeadingWhite, fech)


            If formatoCorrecto Then
                dcRresultado = New BL_GSV_RegistroPrestamoLibros().F_actualizarFechaPrestamoDetalleLibro(codPrestamo, fechaPrestamo)
                Return dcRresultado
            Else
                Return New With {.codigo = 0, .mensaje = "El formato  de la fecha debe ser dd/mm/yyyy,Ejm. 12/02/2013"}
            End If



            Return dcRresultado
        Catch ex As Exception

        End Try
    End Function




    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_insertarPrestamoLibros(ByVal listaActual As List(Of Dictionary(Of Object, Object))) As Object
        Dim dcRresultado As New Dictionary(Of Object, Object)
        dcRresultado = New BL_GSV_RegistroPrestamoLibros().F_insertarPrestamosLIbros(listaActual)

        'dcResultado("mensaje") = mensaje1
        'dcResultado("codigo") = cod1


        Try
            If dcRresultado("codigo") > 0 Then

                For Each dcLibros In listaActual
                    dcLibros("Estado") = False
                Next

            End If
            Return New With {.cantidadDatos = listaActual.Count, .fuenteActualizado = listaActual, .mensaje = dcRresultado("mensaje"), .codigo = CInt(dcRresultado("codigo"))}
        Catch ex As Exception

            Return New With {.fuenteActualizado = listaActual, .mensaje = "Error de sistema " & ex.Message.ToString(), .codigo = -1, .ListaACtualizada = Nothing, .html = ""}

        End Try
    End Function


    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_ListarPrstamos(ByVal codAnio As Integer, ByVal codGrado As Integer, ByVal codAula As Integer, _
                                        ByVal codAlumno As Integer, ByVal codBarra As String) As Object

        Dim dtBusqueda As DataTable
        Try
            dtBusqueda = New DataTable
            Dim nParam As String = "USP_LisPrestamos"
            Dim dc As New Dictionary(Of String, Object)
            dc("codAnio") = codAnio
            dc("codGrado") = codGrado
            dc("codAula") = codAula
            dc("codALumno") = codAlumno
            dc("codigoBarra") = codBarra.Trim()
            dc("estadoPrestamo") = 0

            '' 0 todos
            '' 1 por devolver 
            '' 2 devueltos 

            dtBusqueda = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            'codEstado
            Dim xmlTabla As New XElement("table", New XAttribute("id", "grillaBusquedaLibros"), _
                                              New XAttribute("cellpadding", "0"), _
                                              New XAttribute("cellspacing", "0"), _
                                              New XAttribute("border", "0"), (From dt In dtBusqueda.AsEnumerable() _
                                         Select New XElement("tr", New XAttribute("onmouseover", "Over(this)"), New XAttribute("onmouseout", "Out(this)"), New XElement("td", _
                                               New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:200px;float:left; text-align:left; line-height:25px;") _
                                                           , dt("nombrePersona"))), _
                                                           New XElement("td", _
                                               New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:75px;float:left; text-align:left; line-height:25px;") _
                                                           , dt("codigoBarra"))) _
                                                            , New XElement("td", _
                                               New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:300px;float:left; text-align:left; line-height:25px;") _
                                                           , dt("titulo"))) _
                                                            , New XElement("td", crearFilasEdicionFecha(dt("codEstado"), dt("fechaPrestamo"), dt("codReg")), _
                                                             New XElement("td", _
                                               New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:85px;float:left; text-align:left; line-height:25px;") _
                                                                                   , dt("estado")))))))



            Return New With {.html = xmlTabla.ToString()}




        Catch ex As Exception

        End Try
    End Function



    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_listarDetalleLibrosPrestamo(ByVal codAnio As Integer, ByVal codAlumno As Integer) As Object
        Dim dtBusqueda As DataTable
        Try
            dtBusqueda = New DataTable
            Dim nParam As String = "USP_LisPrestamos"
            Dim dc As New Dictionary(Of String, Object)
            dc("codAnio") = codAnio
            dc("codGrado") = 0
            dc("codAula") = 0
            dc("codALumno") = codAlumno
            dc("codigoBarra") = ""
            dc("estadoPrestamo") = 1
            '' 0 todos
            '' 1 por devolver 
            '' 2 devueltos 

            dtBusqueda = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam).Tables(0)

            Dim xUi As New XElement("table", New XAttribute("cellpadding", "0"), _
                                              New XAttribute("cellspacing", "0"), _
                                              New XAttribute("border", "0"), (dtBusqueda.AsEnumerable().Select(Function(fila, index) _
                New XElement("tr", New XAttribute("onmouseover", "Over(this)"), New XAttribute("onmouseout", "Out(this)"), _
                          New XElement("td", _
                                   New XElement("div", New XAttribute("style", "height:auto;width:85px; float:left; line-height:25px;"), fila("codigoBarra"))), _
                          New XElement("td", _
                                   New XElement("div", New XAttribute("style", "height:auto;width:200px; ; float:left; line-height:25px;"), fila("titulo"))), _
                          New XElement("td", _
                                   New XElement("div", New XAttribute("style", "height:auto;width:85px; float:left; line-height:25px;"), fila("nombreTipoLibro"))), _
                          New XElement("td", _
                                   New XElement("div", New XAttribute("style", "height:auto;width:85px; float:left; line-height:25px;"), fila("fechaPrestamo"))), _
                         elemento(New XElement("img", _
                                                                                                             New XAttribute("title", "Eliminar"), _
                                                                                                             New XAttribute("onclick", "fEliminar(" & index.ToString() & ")"), _
                                                                                                             New XAttribute("src", "../App_Themes/Imagenes/opc_eliminar.png"), _
                                                                                                             New XAttribute("style", " cursor:pointer;height:18px; width:18px;") _
                                  ) _
                                  , False)))))
            Return New With {.codigo = 1, .mensaje = "", .html = xUi.ToString(), .listaNueva = (From al In dtBusqueda.AsEnumerable() _
                                                                       Select New With { _
                                                                              .codigoBarra = al("codigoBarra"), _
                                                                              .titulo = al("titulo"), _
                                                                              .nombreTipoLibro = al("nombreTipoLibro"), _
                                                                              .fechaPrestamo = al("fechaPrestamo"), _
                                                                              .codCopiaLibro = al("codCopiaLibro"), _
                                                                              .Estado = False, _
                                                                              .codAlumno = codAlumno, _
                                                                              .codAnio = codAnio, _
                                                                              .codBlLibro = al("codBlLibro"), _
                                                                              .tipoLibro = al("tipoLibro") _
                                                                                        })}
            'codAlumno(") = codAlumno")
            'dcResultadoLibros("codAnio"





        Catch ex As Exception

        End Try
    End Function


    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_listarDisponibilidadLibro(ByVal fecha As String, ByVal codBarra As String, _
                                                   ByVal codAlumno As Integer, _
                                                   ByVal codAnio As Integer, _
                                                   ByVal listaActual As List(Of Dictionary(Of String, Object))) As Object

        Dim dstResultado As New DataSet

        Dim dtCodigoBarras As DataTable
        Dim dtPrestado As New DataTable
        Try
            dtCodigoBarras = New DataTable
            Dim nParam As String = "USP_LisLibroBanco"
            Dim dc As New Dictionary(Of String, Object) '' diccionario para buscar libros 
            dc("codBarra") = codBarra.Trim()
            Dim existe As Boolean = False

            Dim dcResultadoLibros As New Dictionary(Of String, Object) ''diccionario para devolver libros  por codigo de barras

            ''
            existe = listaActual.Exists(Function(dicLibros) dicLibros("codigoBarra") = codBarra)

            If existe Then
                Return New With {.mensaje = "Ya esta asignado a una persona ", .codigo = 0}

            End If

            '------------------------------------------------------------------------------------------------------
            dstResultado = New bl_rep_libretaNotas().FListarReporteComparacionBimestre(dc, nParam)  'Listar la informacion del libro con el codigo barra 

            dtCodigoBarras = dstResultado.Tables(0)

            dtPrestado = dstResultado.Tables(1)

            If dtPrestado.Rows.Count > 0 Then
                Return New With {.mensaje = "El libro por devolver fecha prestado: " & dtPrestado.Rows(0)("fechaSacoLibro").ToString() & " nombre persona que se presto  :" & dtPrestado.Rows(0)("nombrePersona").ToString(), .codigo = 0}
            End If

            If dtCodigoBarras.Rows.Count = 0 Then
                Return New With {.mensaje = "EL codigo de barras ingresado no existe ", .codigo = 0}
            End If


            dcResultadoLibros = dtCodigoBarras.AsEnumerable().Select(AddressOf obtenerObjetosXfilas).First()
            dcResultadoLibros("fechaPrestamo") = fecha ' Date.Now.Day().ToString() & "/" & Date.Now.Month().ToString() & "/" & Date.Now.Year().ToString()
            dcResultadoLibros("codAlumno") = codAlumno
            dcResultadoLibros("codAnio") = codAnio
            dcResultadoLibros("Estado") = True
            '



            listaActual.Add(dcResultadoLibros)


            Dim xUi As New XElement("table", New XAttribute("cellpadding", "0"), _
                                             New XAttribute("cellspacing", "0"), _
                                             New XAttribute("border", "0"), (listaActual.Select(Function(dcListaActual, index) _
             New XElement("tr", New XAttribute("onmouseover", "Over(this)"), New XAttribute("onmouseout", "Out(this)"), _
                       New XElement("td", _
                                New XElement("div", New XAttribute("style", "height:auto;width:75px; float:left; line-height:25px;"), dcListaActual("codigoBarra"))), _
                       New XElement("td", _
                                New XElement("div", New XAttribute("style", "height:auto;width:200px; ; float:left; line-height:25px;"), dcListaActual("titulo"))), _
                       New XElement("td", _
                                New XElement("div", New XAttribute("style", "height:auto;width:85px; float:left; line-height:25px;"), dcListaActual("nombreTipoLibro"))), _
                       New XElement("td", _
                                New XElement("div", New XAttribute("style", "height:auto;width:85px; float:left; line-height:25px;"), dcListaActual("fechaPrestamo"))), _
                      New XElement("td", _
                                                                                        New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:75px; float:left; text-align:center; line-height:25px;") _
                                                                                                    , elemento(New XElement("img", _
                                                                                                             New XAttribute("title", "Eliminar"), _
                                                                                                             New XAttribute("onclick", "fEliminar(" & index.ToString() & ")"), _
                                                                                                             New XAttribute("src", "../App_Themes/Imagenes/opc_eliminar.png"), _
                                                                                                             New XAttribute("style", " cursor:pointer;height:18px; width:18px;") _
                                  ) _
                                  , CBool(dcListaActual("Estado")))))))))


            Return New With {.html = xUi.ToString(), .listaNueva = listaActual, .codigo = 1}
        Catch ex As Exception
        End Try
    End Function



    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_eliminarItem(ByVal indexDetalle As Integer, ByVal listaActual As List(Of Dictionary(Of String, Object))) As Object
        Try

            Dim xml As New XElement("table")



            listaActual.RemoveAt(indexDetalle)
            Dim xUi As New XElement("table", New XAttribute("cellpadding", "0"), _
                                                 New XAttribute("cellspacing", "0"), _
                                                 New XAttribute("border", "0"), (listaActual.Select(Function(dcListaActual, index) _
                New XElement("tr", New XAttribute("onmouseover", "Over(this)"), New XAttribute("onmouseout", "Out(this)"), _
                          New XElement("td", _
                                   New XElement("div", New XAttribute("style", "height:auto;width:75px; float:left; line-height:25px;"), dcListaActual("codigoBarra"))), _
                          New XElement("td", _
                                   New XElement("div", New XAttribute("style", "height:auto;width:200px; ; float:left; line-height:25px;"), dcListaActual("titulo"))), _
                          New XElement("td", _
                                   New XElement("div", New XAttribute("style", "height:auto;width:85px; float:left; line-height:25px;"), dcListaActual("nombreTipoLibro"))), _
                          New XElement("td", _
                                   New XElement("div", New XAttribute("style", "height:auto;width:85px; float:left; line-height:25px;"), dcListaActual("fechaPrestamo"))), _
                         New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:75px; float:left; text-align:center; line-height:25px;") _
                                                                                                       , elemento(New XElement("img", _
                                                                                                                New XAttribute("title", "Eliminar"), _
                                                                                                                New XAttribute("onclick", "fEliminar(" & index.ToString() & ")"), _
                                                                                                                New XAttribute("src", "../App_Themes/Imagenes/opc_eliminar.png"), _
                                                                                                                New XAttribute("style", " cursor:pointer;height:18px; width:18px;") _
                                     ) _
                                     , CBool(dcListaActual("Estado")))))))))

            Return New With {.ListaACtualizada = listaActual, .html = xUi.ToString, .mensaje = "El Elimino Corretamente ", .codOperacion = 1}


        Catch ex As Exception
            Return New With {.mensaje = "Error de sistema " & ex.Message.ToString(), .codOperacion = -1, .ListaACtualizada = Nothing, .html = ""}
        End Try
    End Function
#End Region
#End Region
#Region "Funciones "

#Region "Funciones  para crear diccionarios"

    Shared Function elemento(ByVal el As XElement, ByVal si As Boolean) As XElement
        Try
            If si Then
                Return el
            Else
                'el.Attribute("onclick").Value = ""
                'Return el
            End If

        Catch ex As Exception

        End Try
    End Function

    Shared Function crearFilasEdicionFecha(ByVal estado As Boolean, ByVal fechaPrestamo As String, ByVal codReg As Integer) As XElement
        Try
            If estado Then
                Return New XElement("div", New XAttribute("style", "; font-size:8pt;height:auto; width:100px;float:left; text-align:left; line-height:25px;") _
                                                           , fechaPrestamo)
            Else
                Return New XElement("div", New XAttribute("style", "; font-size:8pt;height:25px; width:120px;line-height:25px;") _
                        , (New XElement("table", New XAttribute("style", "width:120px"), New XAttribute("border", "0"), _
                                    New XAttribute("cellspacing", "0"), _
                                    New XAttribute("cellpadding", "0"), _
                        New XElement("tr", _
                                     New XElement("td", New XElement("div", _
                                                                     New XAttribute("style", "height:25px;width:25px"), New XElement("img", _
                                                                           New XAttribute("style", "cursor:pointer;height:18px ;width:18px;"), _
                                                                             New XAttribute("title", "Actualizar  fecha "), _
                                                                               New XAttribute("onclick", "EditarFilas(this," & codReg.ToString() & " )"), _
                                                                                 New XAttribute("src", "../App_Themes/Imagenes/actualizarFecha.png")))), _
                        New XElement("td", New XElement("div", _
                                                         New XAttribute("style", "height:25px;width:75px"), _
                        New XElement("input", New XAttribute("style", "width:75px"), New XAttribute("type", "text"), New XAttribute("id", "fecha" & codReg.ToString), New XAttribute("value", fechaPrestamo))))))))

            End If


        Catch ex As Exception

        End Try
    End Function

    Shared Function obtenerObjetosXfilas(ByVal filas As DataRow, ByVal index As Integer) As Dictionary(Of String, Object)
        Try
            Dim dc As New Dictionary(Of String, Object)
            dc("codCopiaLibro") = filas("codCopiaLibro")
            dc("codBlLibro") = filas("codBlLibro")
            dc("codigoBarra") = filas("codigoBarra")
            dc("titulo") = filas("tituloLibro")
            dc("autor") = filas("autor")
            dc("nombreTipoLibro") = filas("nombreTipoLibro")
            dc("tipoLibro") = filas("tipoLibro")

            Return dc

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Shared Function f_crearComboDefault(ByVal idHtml As String, ByVal nombreDefecto As String, ByVal value As String, ByVal clas As String) As XElement
        Try
            Dim xel = New XElement("select", New XAttribute("id", idHtml), _
                                  New XAttribute("class", clas), New XElement("option", New XAttribute("value", value), nombreDefecto))
            Return xel
        Catch ex As Exception

        End Try
    End Function


#End Region

#Region "crear XML desed diccionario "


    Shared Function creaXml(ByVal listaActual As List(Of Dictionary(Of Object, Object))) As XElement
        Try


            ''
            Dim xmlUiLista As New XElement("table", _
                                              New XAttribute("id", "grillaLibros"), _
                                              New XAttribute("cellpadding", "0"), _
                                              New XAttribute("cellspacing", "0"), _
                                              New XAttribute("border", "0"), (listaActual.AsEnumerable().Select(Function(fil, index) _
                                                       New XElement("tr", New XAttribute("onmouseover", "Over(this)") _
                                            , New XAttribute("onmouseout", "Out(this)"), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:25px; width:75px; float:left; text-align:left; line-height:25px;") _
                                                                                                       , fil("codigoBarra"))), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:25px; width:85px; float:left; text-align: center; line-height:25px;") _
                                                                                                        , "Estado")), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:25px; width:250px; float:left; text-align: center; line-height:25px;") _
                                                                                                       , fil("tituloLibro"))), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:25px; width:125px; float:left; text-align:center; line-height:25px;") _
                                                                                                       , fil("autor"))), _
                                                                                                        New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:25px; width:175px; float:left; text-align:center; line-height:25px;") _
                                                                                                       , fil("nombreAlumno"))), _
                                                                            New XElement("td", _
                                                                                           New XElement("div", New XAttribute("style", "; font-size:8pt;height:25px; width:75px; float:left; text-align:center; line-height:25px;") _
                                                                                                       , _
                                                                                                        New XElement("img", _
                                                                                                                      New XAttribute("title", "Eliminar"), _
                                                                                                            New XAttribute("onclick", "fEliminar(" & index.ToString() & ")"), _
                                                                                                            New XAttribute("src", "../App_Themes/Imagenes/opc_eliminar.png"), _
                                                                                                            New XAttribute("style", " cursor:pointer;height:18px; width:18px;") _
                                                                                                                     )))))))
            ''

            Return xmlUiLista
        Catch ex As Exception

        End Try
    End Function
#End Region

#End Region

#Region "eventos pagina "
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Master.MostrarTitulo("Préstamo de Libros")
            If Not Page.IsPostBack Then
                cargarComboAniosAcademico()
                cargarComboGrado()
                SetearFechaDefault()
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region


#Region " procedimientos Rutinas "
    Private Sub SetearFechaDefault()
        Try
            Dim _fechaActual As String = ""
            _fechaActual = Date.Now.Day.ToString() & "/" & Date.Now.Month & "/" & Date.Now.Year.ToString
            txtFechaInicio.Text = _fechaActual
            fechaActual = New System.Web.Script.Serialization.JavaScriptSerializer().Serialize(New With {.fecha = _fechaActual.ToString()})
        Catch ex As Exception

        End Try
    End Sub

#End Region
End Class
