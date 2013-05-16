<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="frmActualizacionEnfermeria.aspx.vb" Inherits="Modulo_Enfermeria_frmActualizacionEnfermeria" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .milegend{
        margin: 0px 20px 0px 20px;  
        padding: 4px 0px 0px 10px;
        text-align:left;
        font-size: 11px;
        font-family: Arial;
        font-weight: bold;
        width: 470px;
        height: 21px;
        background: url(../App_Themes/Imagenes/legend_header.gif) repeat-x;
        border-left: solid 1px #707070;
        border-right: solid 1px #707070;
}        
        
        
       
       
        
        	  .pagination {
	FONT-SIZE: 80%;
	height:25px;
	float:left;
}
body
{
	background-color:#FFFFFF
	}
.pagination A {
	BORDER-BOTTOM: #aae 1px solid; BORDER-LEFT: #aae 1px solid; COLOR: #15b; BORDER-TOP: #aae 1px solid; BORDER-RIGHT: #aae 1px solid; TEXT-DECORATION: none
}
.pagination A {
	PADDING-BOTTOM: 0.3em; PADDING-LEFT: 0.5em; PADDING-RIGHT: 0.5em; DISPLAY: block; MARGIN-BOTTOM: 5px; FLOAT: left; MARGIN-RIGHT: 5px; PADDING-TOP: 0.3em
}
.pagination SPAN {
	PADDING-BOTTOM: 0.3em; PADDING-LEFT: 0.5em; PADDING-RIGHT: 0.5em; DISPLAY: block; MARGIN-BOTTOM: 5px; FLOAT: left; MARGIN-RIGHT: 5px; PADDING-TOP: 0.3em
}
.pagination .current {
	BORDER-BOTTOM: #aae 1px solid; BORDER-LEFT: #aae 1px solid; BACKGROUND: #26b; COLOR: #fff; BORDER-TOP: #aae 1px solid; BORDER-RIGHT: #aae 1px solid
}
.pagination .prev.current {
	BORDER-BOTTOM-COLOR: #999; BORDER-TOP-COLOR: #999; BACKGROUND: #fff; COLOR: #999; BORDER-RIGHT-COLOR: #999; BORDER-LEFT-COLOR: #999
}
.pagination .next.current {
	BORDER-BOTTOM-COLOR: #999; BORDER-TOP-COLOR: #999; BACKGROUND: #fff; COLOR: #999; BORDER-RIGHT-COLOR: #999; BORDER-LEFT-COLOR: #999
}


/**/
.jPaginate {
	POSITION: relative; WIDTH: 100%; HEIGHT: 34px; COLOR: #a5a5a5; FONT-SIZE: small
}
.jPaginate A {
	PADDING-BOTTOM: 2px; LINE-HEIGHT: 15px; MARGIN: 2px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; FLOAT: left; HEIGHT: 18px; CURSOR: pointer; PADDING-TOP: 2px
}
.jPag-control-back {
	POSITION: absolute; LEFT: 0px
}
.jPag-control-front {
	POSITION: absolute; TOP: 0px
}
.jPaginate SPAN {
	CURSOR: pointer
}
UL.jPag-pages {
	PADDING-BOTTOM: 0px; LIST-STYLE-TYPE: none; MARGIN: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; FLOAT: left; PADDING-TOP: 0px
}
UL.jPag-pages LI {
	PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; DISPLAY: inline; FLOAT: left; PADDING-TOP: 0px
}
UL.jPag-pages LI A {
	PADDING-BOTTOM: 2px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; FLOAT: left; PADDING-TOP: 2px
}
SPAN.jPag-current {
	PADDING-BOTTOM: 2px; LINE-HEIGHT: 15px; MARGIN: 2px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; FLOAT: left; HEIGHT: 18px; CURSOR: default; FONT-WEIGHT: normal; PADDING-TOP: 2px
}
UL.jPag-pages LI SPAN.jPag-previous {
	LINE-HEIGHT: 18px; MARGIN: 2px; FLOAT: left; HEIGHT: 22px
}
UL.jPag-pages LI SPAN.jPag-next {
	LINE-HEIGHT: 18px; MARGIN: 2px; FLOAT: left; HEIGHT: 22px
}
SPAN.jPag-sprevious {
	LINE-HEIGHT: 18px; MARGIN: 2px; FLOAT: left; HEIGHT: 22px
}
SPAN.jPag-snext {
	LINE-HEIGHT: 18px; MARGIN: 2px; FLOAT: left; HEIGHT: 22px
}
UL.jPag-pages LI SPAN.jPag-previous-img {
	LINE-HEIGHT: 18px; MARGIN: 2px; FLOAT: left; HEIGHT: 22px
}
UL.jPag-pages LI SPAN.jPag-next-img {
	LINE-HEIGHT: 18px; MARGIN: 2px; FLOAT: left; HEIGHT: 22px
}
SPAN.jPag-sprevious-img {
	LINE-HEIGHT: 18px; MARGIN: 2px; FLOAT: left; HEIGHT: 22px
}
SPAN.jPag-snext-img {
	LINE-HEIGHT: 18px; MARGIN: 2px; FLOAT: left; HEIGHT: 22px
}
UL.jPag-pages LI SPAN.jPag-previous {
	MARGIN: 2px 0px 2px 2px; WIDTH: 10px; FONT-SIZE: 12px; FONT-WEIGHT: bold
}
UL.jPag-pages LI SPAN.jPag-previous-img {
	MARGIN: 2px 0px 2px 2px; WIDTH: 10px; FONT-SIZE: 12px; FONT-WEIGHT: bold
}
UL.jPag-pages LI SPAN.jPag-next {
	MARGIN: 2px 2px 2px 0px; WIDTH: 10px; FONT-SIZE: 12px; FONT-WEIGHT: bold
}
UL.jPag-pages LI SPAN.jPag-next-img {
	MARGIN: 2px 2px 2px 0px; WIDTH: 10px; FONT-SIZE: 12px; FONT-WEIGHT: bold
}
SPAN.jPag-sprevious {
	TEXT-ALIGN: right; MARGIN: 2px 0px 2px 2px; WIDTH: 15px; FONT-SIZE: 18px
}
SPAN.jPag-sprevious-img {
	TEXT-ALIGN: right; MARGIN: 2px 0px 2px 2px; WIDTH: 15px; FONT-SIZE: 18px
}
SPAN.jPag-snext {
	TEXT-ALIGN: right; MARGIN: 2px 2px 2px 0px; WIDTH: 15px; FONT-SIZE: 18px
}
SPAN.jPag-snext-img {
	TEXT-ALIGN: right; MARGIN: 2px 2px 2px 0px; WIDTH: 15px; FONT-SIZE: 18px
}
UL.jPag-pages LI SPAN.jPag-previous-img {
	BACKGROUND: url(../images/previous.png) no-repeat right center
}
UL.jPag-pages LI SPAN.jPag-next-img {
	BACKGROUND: url(../images/next.png) no-repeat left center
}
SPAN.jPag-sprevious-img {
	BACKGROUND: url(../images/sprevious.png) no-repeat right center
}
SPAN.jPag-snext-img {
	BACKGROUND: url(../images/snext.png) no-repeat left center
}
        
 html, body, div, span, applet, object, iframe,
h1, h2, h3, h4, h5, h6, p,
blockquote, pre, a, abbr, acronym, address, big,
cite, code, del, dfn, em, font, img,
ins, kbd, q, s, samp, small, strike,
strong, sub, sup, tt, var, dl, dt, dd, ol, ul, li
, form, label,
table, caption, tbody, tfoot, thead, tr, th, td,
center, u, b, i {
     margin: 0;
     padding: 0;
     border: 0;
     outline: 0;
     font-weight: normal;
     font-style: normal;
     font-size: 100%;
     font-family: inherit;
     vertical-align: baseline
}

body {
     line-height: 1
}

:focus {
     outline: 0
}

ol, ul {
     list-style: none
}

table {
     border-collapse: collapse;
     border-spacing: 0
}

blockquote:before, blockquote:after, q:before, q:after {
     content: ""
}

blockquote, q {
     quotes: "" ""
}

input, textarea 
{
     margin: 0;
     padding: 0
}

hr {
     margin: 0;
     padding: 0;
     border: 0;
     color: #000;
     background-color: #000;
     height: 1px
}
 
        .style1
        {
            width: 124px;
        }
        .style2
        {
            width: 132px;
        }
        .style3
        {
            width: 137px;
        }
        .style4
        {
            width: 138px;
        }
        .style5
        {
            width: 136px;
        }
        .style6
        {
            width: 135px;
        }
 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
   
   
   
 
 
 <div id="tabs">
    <ul>
        <li><a href="#fragment-2"><span>Consulta</span></a></li>
        <li><a href="#fragment-3"><span>Registro</span></a></li>
    </ul>
     
    <div id="fragment-2" style="height: auto; overflow :hidden ">
       <div style="height: auto; overflow:hidden; width: 944px">
    
    <div>
    
   <fieldset>
   <legend class ="milegend">
    Criterios de busqueda
   </legend>
   
  
        <table style="width:100%; font-size:8pt;">
                        
           
            <tr>
                <td class="style1">
                    fecha de solicitud</td>
                <td class="style12">
                Desde &nbsp;&nbsp;
                   <input value="<%= fechaDesde%>" type="text" id="txtFechaInicio" />&nbsp;&nbsp; al&nbsp;
                    <input value="<%= fechaHasta%>" id="txtFechaFin" type="text" /></td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            
           <%-- 
               fechaDesde = Fecha2.ToString.Substring(0, 10)
        fechaHasta = Date.Now.ToString.Substring(0, 10)--%>
            
            <tr>
                <td class="style1">
                    Estado</td>
                <td class="style12">
                    <div id="estados">
                    
                 <table>
                 <tr>
                 <td>
                     Todos<input  value="0" name="estados" id="rdbEliminado"  type="radio" />
                     Pendiente <input value="1" checked="checked" name="estados" id="rdbPendiente" type="radio" />
                     Validado  <input value="2" name="estados" id="rdbValidado" type="radio" />
                 </td>
                 <td>
                 </td>
                 </tr>
                 </table>
                 
                    </div>
                    </td>
                <td>
                    
                    <img   onclick=" F_buscarSolicitud()" title="Buscar" 
                        src="../App_Themes/Imagenes/btnBuscar_1.png" 
                        style="cursor:pointer;height :25px; width:72px" />
                    <img onclick="limpiar()" title="Limpiar" 
                        src="../App_Themes/Imagenes/btnLimpiar_1.png" 
                        style="cursor:pointer;height :25px; width:72px" />&nbsp; </td>
            </tr>
            <tr>
                <td class="style1">
                    Apellido paterno </td>
                <td class="style2" colspan="2">
                    <input id="txtApellidoPaterno" type="text" /></td>
            </tr>
            <tr>
                <td class="style3">
                    Apellido materno </td>
                <td class="style4" colspan="2">
                    <input id="txtApellidoMaterno" type="text" />
                    <input type="button" onclick="ordenar()" value="ordenar grilla" />
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    Nombre</td>
                <td class="style2" colspan="2">
                    <input id="txtNombre" type="text"   /></td>
            </tr>
            </table>
       
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         
   <%--<table style="height: 73px; width: 920px; font-size:8pt;">
   <tr>
   <td class="style6">
                                        <span>Nivel</span>
                                    </td>
   <td>
   
   
   
                                        <asp:DropDownList runat="server" AutoPostBack="True" Width="250px" ID="ddlBuscarNivel" style="font-size: 8pt; font-family: Arial;" OnSelectedIndexChanged="ddlBuscarNivel_SelectedIndexChanged"></asp:DropDownList>
                                             
                                    </td>
   <td></td>
   </tr>
    <tr>
   <td class="style6">
                                        <span>SubNivel</span>
                                    </td>
   <td>
                                        <asp:DropDownList runat="server" AutoPostBack="True" Width="250px" ID="ddlBuscarSubNivel" style="font-size: 8pt; font-family: Arial;" OnSelectedIndexChanged="ddlBuscarSubNivel_SelectedIndexChanged"></asp:DropDownList>
                                             
                                    </td>
   <td></td>
   </tr>
    <tr>
   <td class="style6">
                                        <span>Grado</span>
                                    </td>
   <td>
                                        <asp:DropDownList runat="server" AutoPostBack="True" Width="250px" ID="ddlBuscarGrado" style="font-size: 8pt; font-family: Arial;" OnSelectedIndexChanged="ddlBuscarGrado_SelectedIndexChanged"></asp:DropDownList>
                                             
                                    </td>
   <td></td>
   </tr>
    <tr>
   <td class="style6">
                                        <span>Aula</span>
                                    </td>
   <td>
                                        <asp:DropDownList runat="server" Width="250px" ID="ddlBuscarAula" style="font-size: 8pt; font-family: Arial;"></asp:DropDownList>
                                             
                                    </td>
   <td>&nbsp;</td>
   </tr>
   </table>--%>
   </ContentTemplate>
   </asp:UpdatePanel>
    </fieldset>
    </div>
    <div style="height:auto ;overflow:hidden" id="pnlResultado">
    
    </div>
        <div style="height:auto;overflow:hidden; width:943px; height:auto;overflow:hidden" 
               id="detalles">
    
    
    </div>
   
    <div id="paginas">
    
    </div>
     <div id="Div1">
     Cantidad de registros :<strong id="numero"></strong>
    </div>
    </div>
</div>
	  <div id="fragment-3">
	   <div id="modalFicha" style="height:1262px; width:995px;background-color:#FFFFFF">
    
    <table style="height: 109px; width: 834px">
    <tr>
    <td class="style11">
    <fieldset style="height: 93px; width: 645px">
    <legend class="milegend">
    
        Datos solicitante</legend>
        <table style="height: 61px; width: 350px; font-size:8pt;">
        <tr>
        <td style="width:85px"  >Nombre Completo</td>
            <td>
            </td>
        <td id="nombreSol" class="style10"></td>
        <td id="nombreSol" class="style8">&nbsp;</td>
        </tr>
         <tr>
        <td class="style9">Fecha</td>
        <td id="fechaSol" class="style10"></td>
        <td id="fechaSol" class="style8">&nbsp;</td>
        </tr>
         <tr>
        <td class="style9">Estado</td>
        <td id="estadoSol" class="style10"></td>
        <td id="estadoSol" class="style8">&nbsp;</td>
        </tr>
        </table>
        
    </fieldset>
    </td>
    <td style="text-align:center; vertical-align:top">
    <table style="height: 49px; width: 75px">
    <tr>
    <td>
    <img src="../App_Themes/Imagenes/btnGrabarV2_2.png" onclick="actualizarFicha()" title="Grabar"  style="cursor:pointer"/>
    </td>
    </tr>
      <tr>
    <td>
     <img src="../App_Themes/Imagenes/btnCancelarV2_1.png" title="Cancelar" onclick="cancelar()" style="cursor:pointer" />
    </td>
    </tr>
    </table>
    </td>
    </tr>
    </table>
    <fieldset style="height: 93px; width: 642px">
    <legend class="milegend">
    
        Situacion actual del alumno</legend>
        
          <table style="height: 73px; width: 522px; font-size:8pt">
        <tr>
        <td style="width:65px" rowspan="3">
        <img id="nombreFoto"  style ="height :45px; width:45px"/>
         </td>
        <td style="width:85px">Nombre Completo</td>
        <td id="nombreAlumno"></td>
        </tr>
         <tr>
        <td class="style5">estado/año academico</td>
        <td id="estadoAnio"></td>
        </tr>
         <tr>
        <td class="style5">&nbsp;</td>
        <td></td>
        </tr>
        </table>
    </fieldset>
    
     <div id="edicionFicha" style="width: 600px">
     
     </div> 
      
</div>
	   </div>
	   
	   </div>
 
 
 <script>

     function limpiar() {

         $(":text").val("")
     $("option[value=0]").attr("selected",true )
     }
     $(document).ready(function() {


     $("#tabs").tabs({ collapsible: true });

     $("#tabs").tabs("disable", 1);
     F_buscarSolicitud();
         
//         $('#modalFicha').dialog({
//             autoOpen: false,
//             modal: true,
//             width: 1200,
//             height: 700,
//             title: "Edicion ficha ",
//             buttons: {
//                 "Cerrar": function() {
//                 $(this).dialog("close");
//                 },
//                 "Aceptar": function() {
//                     

//                     if (confirm("Seguro que  desea confirmar los cambios")) {
//                         actualizarFicha()
//                     }



//                 }

//             }
//         });

     });
   

     function F_buscarSolicitud() {
         $.blockUI({
             message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Realizando busqueda...</h4>'
         });
         $.ajax({
             url: "frmActualizacionEnfermeria.aspx/F_buscarSolicitud",
             cache: false,
             type: "post",
             data: JSON.stringify({
                 fechaIni: $("#txtFechaInicio").val(),
                 fechaFin: $("#txtFechaFin").val(),
                 RSAFM_EstadoSolicitud: parseInt($("input[name='estados']:checked").val()),
                 PE_ApellidoPaterno: $("#txtApellidoPaterno").val(),
                 PE_ApellidoMaterno: $("#txtApellidoMaterno").val(),
                 PE_Nombre: $("#txtNombre").val(),
                 GD_CodigoGrado: 0,  
                 AAP_CodigoAsignacionAula: 0,
                 AC_CodigoAnioAcademico: 0
                 , pagina: 0, soloPaginas: 1,


                 p_AlumnoNivel: 0, 
                 p_AlumnoSubnivel: 0,  
                 p_AlumnoAula: 0 

             }),
             dataType: "json",
             contentType: "application/json; charset=utf-8",
             success: function(res, textStatus, jqXHR) {


             $.unblockUI();

             $("#paginas").html("");

             $("#numero").text(res.d.cantidad)
             
                 $("#paginas").paginate({
                     count: res.d.count,
                     start: 1,
                     display: 10,
                     border: true,
                     border_color: '#fff',
                     text_color: '#fff',
                     background_color: 'black',
                     border_hover_color: '#ccc',
                     text_hover_color: '#000',
                     background_hover_color: '#fff',
                     images: false,
                     mouse: 'press',
                     onChange: function(page) {
                         paginarLista(page, 56)
                         //$('._current','#paginationdemo').removeClass('_current').hide();
                         //$('#p'+page).addClass('_current').show();
                     }
                 });
                 
                 
                 //l pagina As Integer, ByVal soloPaginas
                 paginarLista(1, 3);
             }
      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
         });



 }

 function paginarLista(p, jq) {
     $.blockUI({
         message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> cambiando de pagina...</h4>'
     });
  $.ajax({
             url: "frmActualizacionEnfermeria.aspx/F_buscarSolicitud",
             cache: false,
             type: "post",
             data: JSON.stringify({
                     fechaIni: $("#txtFechaInicio").val(),
                     fechaFin: $("#txtFechaFin").val(),
                     RSAFM_EstadoSolicitud:parseInt( $("input[name='estados']:checked").val()),
                     PE_ApellidoPaterno: $("#txtApellidoPaterno").val(),
                     PE_ApellidoMaterno: $("#txtApellidoMaterno").val(),
                     PE_Nombre: $("#txtNombre").val(),
                     GD_CodigoGrado: 0,  
                     AAP_CodigoAsignacionAula: 0,
                     AC_CodigoAnioAcademico:0, pagina: p, soloPaginas: 0,
                     //


                     p_AlumnoNivel: 0, 
                     p_AlumnoSubnivel: 0,  
                     p_AlumnoAula:0 
                     //
             }),
             dataType: "json",
             contentType: "application/json; charset=utf-8",
             success: function(res, textStatus, jqXHR) {
             crearGrilla(res.d)
             //crearDetalle(res,res)
             $.unblockUI();
         }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
     });
 
 }

     var lstAlergiasActual=[];
     var lstAlergiasNuevo = [];

     function crearFilasAlergia(res) {
     
     lstAlergiasActual = res.d.lstBE_EN_RelacionFichaMedicasAlergias
     lstAlergiasNuevo = res.d.lstBE_EN_RelacionFichaMedicasAlergias_temp

     var mayor = (lstAlergiasActual.length >= lstAlergiasNuevo.length) ? lstAlergiasActual.length : lstAlergiasNuevo.length;
//     var estado = lstAlergiasNuevo[].
     var html = "";

     if ( lstAlergiasNuevo.length == 0)
     {
         return html;
     }
//     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"//style=";background-color:#DCE6F4"
//     html += "<td style='text-align:center;background-color:#DCE6F4' colspan='4'>"
//            html += "Alergias"
//            html += "</td>"
//            html += "</tr>"
//            html += "<tr>"
//            //
//            html += "<tr>"
//            html += "<td style='text-align:center' colspan='4'>"
     //            html += "<div style='font-size:8pt;width:590px; border: 1px solid #54709B;text-align:right '>"

          
//            html += " <input onclick='FactualizarAlergia(this)' id='Checkbox1' type='checkbox' />"
//            
//             html += "</div>"
//            html += "</td>"
//            html += "</tr>"
//            html += "<tr>"


             
             
            html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
            html += "<td style='text-align:center;width:600px;' colspan='4'>"
            html += "<div style='font-size:8pt;width:525px;text-align:center ;float:left'>"
            html += "Alergias</div>"
            html += "<div style='font-size:8pt;width:112px;  text-align:right ;float:left; '>"
           html += "<input onclick='FactualizarAlergia(this)' id='Checkbox1' type='checkbox' />"
            html += "</div>"
            html += "</td>"
            html += "</tr>"
            html += "<tr>"
             
            
            
            //
            html += "<td colspan='2' style='text-align:center' >"
            html += "<table style=' ;width:300px' cellspacing='0' cellpadding='0' border='0' id='Alergia1'>"
            for (var indice = 0; indice < lstAlergiasActual.length; indice++) {
                html += "<tr onmouseover='AlergiasActualOver(this)' onmouseout='AlergiasActualOut(this)' id='" + indice + "'>"
                html += "<td colspan='2'><div style='font-size:8pt;width:300px; text-align:center '>" + lstAlergiasActual[indice].AG_Descripcion + "</div></td>"
                html += "</tr>"
 
            }
            html += "</table>"
            html += "</td>"

            html += "<td colspan='2' style='width:300px;text-align:center' >"
            html += "<table sytle='width:300px' id='Alergia2'>"
            for (var indice = 0; indice < lstAlergiasNuevo.length; indice++) {
                html += "<tr onmouseover='AlergiasNuevoOver(this)' onmouseout='AlergiasNuevoOut(this)' id='" + indice + "'>"
                html += "<td colspan='2'><div style='font-size:8pt;width:300px; text-align:center '>" + lstAlergiasNuevo[indice].AG_Descripcion + "</div></td>"
                html += "</tr>"
            }
            html += "</table>"
            html += "</td>"
            html += "</tr>"
            //
            return html

        }
        
        function AlergiasActualOver(control) {
            $("#Alergia1 tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
        }

        function AlergiasActualOut(control) {
            $("#Alergia1 tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
        }
        function AlergiasNuevoOver(control) {
            $("#Alergia2 tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
        }

        function AlergiasNuevoOut(control) {
            $("#Alergia2 tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
        }
 
 
 
 function FactualizarAlergia(control, index) {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }
    // lstAlergiasNuevo[index].RFAG_Check = estadoMarcado
     for (var indice = 0; indice < lstAlergiasNuevo.length; indice++) {
        // alert(lstAlergiasNuevo[indice].RFAG_Check)
         lstAlergiasNuevo[indice].RFAG_Check = estadoMarcado
     }
                
 }
 
 
 
 var lstFichasMedicaCarecteristicasPielActual = [];
 var lstFichasMedicaCarecteristicasPielNuevo = [];
      
 function crearFilasFichaMedicaCarecteristicasPiel(res) { 
     lstFichasMedicaCarecteristicasPielActual = res.d.LStBE_EN_RelacionFichaMedicasCarecteristicasPiel
     lstFichasMedicaCarecteristicasPielNuevo = res.d.LStBE_EN_RelacionFichaMedicasCarecteristicasPiel_temp


     var html = "";

     if (  lstFichasMedicaCarecteristicasPielNuevo.length == 0) {
         return html;
     }


     /*
        html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
            html += "<td style='text-align:center;width:600px;' colspan='4'>"
            html += "<div style='font-size:8pt;width:525px;text-align:right ;float:left'>"
            html += "Alergias</div>"
            html += "<div style='font-size:8pt;width:75px;  text-align:right ;float:right; '>"
           html += "<input onclick='FactualizarAlergia(this)' id='Checkbox1' type='checkbox' />"
            html += "</div>"
            html += "</td>"
            html += "</tr>"
            html += "<tr>"
            //
            html += "<td colspan='2' style='text-align:center' >"
            html += "<table style='border-style: solid; border-width: 1px;width:300px' cellspacing='0' cellpadding='0' border='0' id='Alergia1'>"
            for (var indice = 0; indice < lstAlergiasActual.length; indice++) {
                html += "<tr onmouseover='AlergiasActualOver(this)' onmouseout='AlergiasActualOut(this)' id='" + indice + "'>"
                html += "<td colspan='2'><div style='font-size:8pt;width:150px; text-align:center '>" + lstAlergiasActual[indice].AG_Descripcion + "</div></td>"
                html += "</tr>"
            }
            html += "</table>"
            html += "</td>"
            html += "<td colspan='2' style='width:300px;text-align:center' >"
            html += "<table sytle='width:300px' id='Alergia2'>"
            for (var indice = 0; indice < lstAlergiasNuevo.length; indice++) {
                html += "<tr onmouseover='AlergiasNuevoOver(this)' onmouseout='AlergiasNuevoOut(this)' id='" + indice + "'>"
                html += "<td colspan='2'><div style='font-size:8pt;width:150px; text-align:center '>" + lstAlergiasNuevo[indice].AG_Descripcion + "</div></td>"
                html += "</tr>"
            }
            html += "</table>"
            html += "</td>"
            html += "</tr>"
     */
     
     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center;width:600px;' colspan='4'>"
     html += "<div style='font-size:8pt;width:525px;  text-align:center ;float:left'>"
     html += "Caracteristicas Piel</div>"
     html += "<div style='font-size:8pt;width:75px;  text-align:right ;float:right'>"
     html += " <input onclick='FactualizarCarecteristicasPiel(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"
     html += "<td colspan='2' style='text-align:center;width:300px;' >"
     html += "<table width=300px   cellspacing='0' cellpadding='0' border='0' id='CarecteristicasPielActual'>"
     for (var indice = 0; indice < lstFichasMedicaCarecteristicasPielActual.length; indice++) {
         html += "<tr  onmouseover='CarecteristicasPielActualOver(this)' onmouseout='CarecteristicasPielActualOut(this)' id='" + indice + "'>"
         html += "<td style='width:300px;' colspan='2' ><div style='font-size:8pt;width:300px;  text-align:center '>" + lstFichasMedicaCarecteristicasPielActual[indice].TCP_Descripcion + "</div></td>"
         html += "</tr>"
     }
     html += "</table>"
     html += "</td>"

     html += "<td colspan='2' style='text-align:center;width:300px' >"
     html += "<table width=300px   id='CarecteristicasPielNuevo'>"
      for (var indice = 0; indice < lstFichasMedicaCarecteristicasPielNuevo.length; indice++) {
          html += "<tr  onmouseover='CarecteristicasPielNuevoOver(this)' onmouseout='CarecteristicasPielNuevoOut(this)' id='" + indice + "'>"
          html += "<td style='width:300px;' colspan='2'><div style='font-size:8pt;width:300px; text-align:center '>" + lstFichasMedicaCarecteristicasPielNuevo[indice].TCP_Descripcion + "</div></td>"
         html += "</tr>"
     }
     html += "</table>"
     html += "</td>"
     html += "</tr>"
     //
     
     
     return html;
 }

 ////////////////
 
 //onmouseover='CarecteristicasPielNuevoOver(this)' onmouseout='CarecteristicasPielNuevoOut(this)'
 function CarecteristicasPielActualOver(control) {
     $("#CarecteristicasPielActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }

 function CarecteristicasPielActualOut(control) {
     $("#CarecteristicasPielActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 function CarecteristicasPielNuevoOver(control) {
     $("#CarecteristicasPielNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }

 function CarecteristicasPielNuevoOut(control) {
     $("#CarecteristicasPielNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 
 ///////////////
 function FactualizarCarecteristicasPiel(control, index) {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
  //   lstFichasMedicaCarecteristicasPielNuevo[index].RFCP_Check = estadoMarcado
     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }
     for (var indice = 0; indice < lstFichasMedicaCarecteristicasPielNuevo.length; indice++) {
         lstFichasMedicaCarecteristicasPielNuevo[indice].RFCP_Check = estadoMarcado
     }
 }
 ///////////////////////////////////////////////////////////////////////////////
 
 
 var ListaFichaMedicaHospitalizacionActual = []
 var ListaFichaMedicaHospitalizacionNuevo = []
 function crearFilasHospitalizacion(res) {
     ListaFichaMedicaHospitalizacionActual = res.d.LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion
     ListaFichaMedicaHospitalizacionNuevo = res.d.LstBE_EN_RelacionFichaMedicasMotivoHospitalizacion_temp
     var html = "";
     if (  ListaFichaMedicaHospitalizacionNuevo.length == 0) {
         return html;
     }
//     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center;' colspan='4'>"
//     html += "Hospitalizacion"
//     html += "</td>"
//     html += "</tr>"
     
     
     
//     for (var indice = 0; indice < ListaFichaMedicaHospitalizacionActual.length; indice++) {
//         html += "<tr>"
//         html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '>Motivo</td>"
//         html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '>" + ListaFichaMedicaHospitalizacionActual[indice].MH_Descripcion + "</td>"
//         html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '>" + ListaFichaMedicaHospitalizacionNuevo[indice].MH_Descripcion + "</td>"
//         if (ListaFichaMedicaHospitalizacionNuevo[indice].RFMH_Check == true) {
//             html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '><input onclick='FactualizarHospitalizacion(this," + indice + ")'checked='checked' id='Checkbox1' type='checkbox' />"
//         }
//         else {
//             html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '><input onclick='FactualizarHospitalizacion(this," + indice + ")' id='Checkbox1' type='checkbox' />"
//         }
//         html += "</tr>"
//     }
//     return html;


     /**/
//     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center' colspan='4'>Hospitalizacion"
//     html += "<div style='font-size:8pt;width:590px; text-align:right '>"
//     html += " <input onclick='FactualizarHospitalizacion(this)' id='Checkbox1' type='checkbox' />"
//     html += "</div>"
//     html += "</td>"
//     html += "</tr>"
//     html += "<tr>"
     //

     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center;width:600px' colspan='4'>"
     html += "<div style='font-size:8pt;width:525px;  text-align:center ;float:left'>"
     html += "Hospitalizacion</div>"
     html += "<div style='font-size:8pt;width:111px;  text-align:right ;float:left'>"
     html += " <input onclick='FactualizarHospitalizacion(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"




     /**/
     //
     html += "<td colspan='2' style='text-align:center' >"
     html += "<table width=300px   cellspacing='0' cellpadding='0' border='0' id='HospitalizacionActual'>"
     for (var indice = 0; indice < ListaFichaMedicaHospitalizacionActual.length; indice++) {
         html += "<tr onmouseover='HospitalizacionActualOver(this)' onmouseout='HospitalizacionActualOut(this)' id='" + indice + "'>"
         html += "<td colspan='2'><div style='font-size:8pt;width:300px; text-align:center '>" + ListaFichaMedicaHospitalizacionActual[indice].MH_Descripcion + "</div></td>"
         html += "</tr>"

     }
     html += "</table>"
     html += "</td>"

     html += "<td colspan='2' style='text-align:center;width:300px' >"
     html += "<table width=300px   id='HospitalizacionNuevo'>"
     for (var indice = 0; indice < ListaFichaMedicaHospitalizacionNuevo.length; indice++) {
         html += "<tr onmouseover='HospitalizacionNuevoOver(this)' onmouseout='HospitalizacionNuevoOut(this)' id='" + indice + "'>"
         html += "<td colspan='2'><div style='font-size:8pt;width:300px;  text-align:center '>" + ListaFichaMedicaHospitalizacionNuevo[indice].MH_Descripcion + "</div></td>"
         html += "</tr>"
     }
     html += "</table>"
     html += "</td>"
     html += "</tr>"
     return html;
     /**/


 }

 //onmouseover='CarecteristicasPielNuevoOver(this)' onmouseout='CarecteristicasPielNuevoOut(this)'
 function HospitalizacionActualOver(control) {
     $("#HospitalizacionActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }

 function HospitalizacionActualOut(control) {
     $("#HospitalizacionActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 function HospitalizacionNuevoOver(control) {
     $("#HospitalizacionNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }

 function HospitalizacionNuevoOut(control) {
     $("#HospitalizacionNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 
 
 function FactualizarHospitalizacion(control) {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
   //  alert(estadoMarcado)
 //    ListaFichaMedicaHospitalizacionNuevo[index].RFMH_Check = estadoMarcado

     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }
     for (var indice = 0; indice < ListaFichaMedicaHospitalizacionNuevo.length; indice++) {
         ListaFichaMedicaHospitalizacionNuevo[indice].RFMH_Check = estadoMarcado
       //  alert(ListaFichaMedicaHospitalizacionNuevo[indice].RFMH_Check)
     }
 }
 
 ///////////////////////////////////////////////
 var ListaFichaMedicaEnfermedadesActual = []
 var ListaFichaMedicaEnfermedadesNuevo = []
 function crearFilasEnfermedades(res) {
     ListaFichaMedicaEnfermedadesActual = res.d.LstBE_EN_RelacionFichaMedicasEnfermedades
     ListaFichaMedicaEnfermedadesNuevo = res.d.LstBE_EN_RelacionFichaMedicasEnfermedades_temp
     var html = "";
     if (  ListaFichaMedicaEnfermedadesNuevo.length == 0) {
         return html;
     }

//     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center;' colspan='3'>"
//     html += "Enfermedades"
//     html += "</td>"

//     html += "</tr>"
     
     
//     for (var indice = 0; indice < ListaFichaMedicaEnfermedadesActual.length; indice++) {
//         html += "<tr>"
//         html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '>Enfermedades</td>"
//         html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '>" + ListaFichaMedicaEnfermedadesActual[indice].EF_Descripcion + "</td>"
//         html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '>" + ListaFichaMedicaEnfermedadesNuevo[indice].EF_Descripcion + "</td>"
//         if (ListaFichaMedicaEnfermedadesNuevo[indice].RFEF_Check == true)
//          {
//             html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '><input onclick='FactualizarEnfermedades(this," + indice + ")'checked='checked' id='Checkbox1' type='checkbox' />"
//          }
//         else {
//             html += "<td><div style='font-size:8pt;width:125px; border: 1px solid #54709B;text-align:center '><input onclick='FactualizarEnfermedades(this," + indice + ")' id='Checkbox1' type='checkbox' />"
//         }
//         html += "</tr>"
//     }


     /**/

//     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center;width:' colspan='4'>"
//     html += "<div style='font-size:8pt;width:525px;;text-align:right;float:right '>Enfermedades"
//     html += "</div>"

//     html += "<div style='font-size:8pt;width:75px;;text-align:right;float:right '>"
//     html += "<input onclick='FactualizarEnfermedades(this)' id='Checkbox1' type='checkbox' />"
//     html += "</div>"
//     
//     
//     html += "</td>"
//     html += "</tr>"
//     html += "<tr>"
     //
     
     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center;width:600px' colspan='4'>"
     html += "<div style='font-size:8pt;width:525px;  text-align:center ;float:left'>"
     html += "Enfermedades</div>"
     html += "<div style='font-size:8pt;width:111px;  text-align:right ;float:left'>"
     html += " <input onclick='FactualizarEnfermedades(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"
     /**/
     //
     html += "<td colspan='2' style='text-align:center;' >"
     html += "<table  style='border-style: ;width;300px' cellspacing='0' cellpadding='0' border='0' id='EnfermedadesActual'>"
     for (var indice = 0; indice < ListaFichaMedicaEnfermedadesActual.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='EnfermedadesActualOver(this)' onmouseout='EnfermedadesActualOut(this)'>"
         html += "<td style='font-size:8pt;' ><div style='font-size:8pt;width:300px; text-align:center '>" + ListaFichaMedicaEnfermedadesActual[indice].EF_Descripcion + "</div></td>"
         html += "</tr>"
     }
     html += "</table>"
     html += "</td>"

     html += "<td colspan='2' style='text-align:center;width:300px' >"
     html += "<table  style=';width:300px' id='MedicaEnfermedadesNuevo'>"
     for (var indice = 0; indice < ListaFichaMedicaEnfermedadesNuevo.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='MedicaEnfermedadesNuevoOver(this)' onmouseout='MedicaEnfermedadesNuevoOut(this)'>"
         html += "<td><div style='font-size:8pt;width:300px; text-align:center '>" + ListaFichaMedicaEnfermedadesNuevo[indice].EF_Descripcion + "</div></td>"
         html += "</tr>"
     }
     html += "</table>"
     html += "</td>"
     html += "</tr>"
     return html;
     /**/
 }
 //onmouseover='CarecteristicasPielNuevoOver(this)' onmouseout='CarecteristicasPielNuevoOut(this)'
 function EnfermedadesActualOver(control) {
     $("#EnfermedadesActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }

 function EnfermedadesActualOut(control) {
     $("#EnfermedadesActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 function MedicaEnfermedadesNuevoOver(control) {
     $("#MedicaEnfermedadesNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }

 function MedicaEnfermedadesNuevoOut(control) {
     $("#MedicaEnfermedadesNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 
 
 function FactualizarEnfermedades(control) {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }
     for (var indice = 0; indice < ListaFichaMedicaEnfermedadesNuevo.length; indice++) {
         ListaFichaMedicaEnfermedadesNuevo[indice].RFEF_Check = estadoMarcado
     }
  
 }
 
 /////////////////////////////////////////////
 var ListaFichaMedicaOperacionesActual = []
 var ListaFichaMedicaOperacionesNuevo = []
 function crearFilasOperaciones(res) {
     ListaFichaMedicaOperacionesActual = res.d.LstBE_EN_RelacionFichaMedicasOperaciones
     ListaFichaMedicaOperacionesNuevo = res.d.LstBE_EN_RelacionFichaMedicasOperaciones_temp
     var html = "";


     if (  ListaFichaMedicaOperacionesNuevo.length == 0) {
         return html;
     }


     //----------------
//     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center' colspan='4'>Operaciones"
//     html += "<div style='font-size:8pt;width:590px;  text-align:right '>"
//     html += " <input onclick='FactualizarOperaciones(this)' id='Checkbox1' type='checkbox' />"
//     html += "</div>"
//     html += "</td>"
//     html += "</tr>"
//     html += "<tr>"


     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center' colspan='4'>"
     html += "<div style='font-size:8pt;width:525px;  text-align:center ;float:left'>"
     html += "Operaciones</div>"
     html += "<div style='font-size:8pt;width:112px;  text-align:right ;float:left'>"
     html += " <input onclick='FactualizarOperaciones(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"

//------------------------
//     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center;background-color:#DCE6F4' colspan='4'>"
//     html += "Operaciones"
//     html += "</td>"
//     html += "</tr>"

     /**/
     
//     html += "<tr>"
//     html += "<td style='text-align:center' colspan='4'>"
//     html += "<div style='font-size:8pt;width:590px; border: 1px solid #54709B;text-align:right '>"
//     html += " <input onclick='FactualizarOperaciones(this)' id='Checkbox1' type='checkbox' />"
//     html += "</div>"
//     html += "</td>"
//     html += "</tr>"
     //     html += "<tr>"

     //


     /**/
     //
     html += "<td colspan='2' style='text-align:center' >"
     html += "<table width=300px  style=' ' cellspacing='0' cellpadding='0' border='0' id='FichaMedicaOperacionesActual'>"
     for (var indice = 0; indice < ListaFichaMedicaOperacionesActual.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='MedicaOperacionesActualOver(this)' onmouseout='MedicaOperacionesActualOut(this)'>"
         html += "<td colspan='2'><div style='font-size:8pt;width:150px;  ;text-align:center '>" + ListaFichaMedicaOperacionesActual[indice].TOM_Descripcion + "</div></td>"
         html += "</tr>"

     }
     html += "</table>"
     html += "</td>"

     html += "<td colspan='2' style='text-align:center;width:300px' >"
     html += "<table width=300px style='border-style: solid; border-width: 1px' id='FichaMedicaOperacionesNuevo'>"
     for (var indice = 0; indice < ListaFichaMedicaOperacionesNuevo.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='FichaMedicaOperacionesNuevoOver(this)' onmouseout='FichaMedicaOperacionesNuevoOut(this)'>"
         html += "<td colspan='2'><div style='font-size:8pt;width:150px; text-align:center '>" + ListaFichaMedicaOperacionesNuevo[indice].TOM_Descripcion + "</div></td>"
         html += "</tr>"
     }
     html += "</table>"
     html += "</td>"
     html += "</tr>"
     return html;
     /**/
 }
 //onmouseover='CarecteristicasPielNuevoOver(this)' onmouseout='CarecteristicasPielNuevoOut(this)'
 function MedicaOperacionesActualOver(control) {
     $("#FichaMedicaOperacionesActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function MedicaOperacionesActualOut(control) {
     $("#FichaMedicaOperacionesActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 function FichaMedicaOperacionesNuevoOver(control) {
     $("#FichaMedicaOperacionesNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function FichaMedicaOperacionesNuevoOut(control) {
     $("#FichaMedicaOperacionesNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 
 
 function FactualizarOperaciones(control) {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }
    //  ListaFichaMedicaOperacionesNuevo[index].RFOM_Check = estadoMarcado
     for (var indice = 0; indice < ListaFichaMedicaOperacionesNuevo.length; indice++) {
         ListaFichaMedicaOperacionesNuevo[indice].RFOM_Check = estadoMarcado
     }
     
 }
/////////////////////////////////////////////



 var ListaFichaMedicaTiposControlesActual = []
 var ListaFichaMedicaTiposControlesNuevo = []

 function crearFilasTiposControles(res) {
     ListaFichaMedicaTiposControlesActual = res.d.LstBE_EN_RelacionFichaMedicasTiposControles
     ListaFichaMedicaTiposControlesNuevo = res.d.LstBE_EN_RelacionFichaMedicasTiposControles_temp
     var html = "";
    
     if ( ListaFichaMedicaTiposControlesNuevo.length == 0) {
         return html;
     }


  /*   html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center;background-color:#DCE6F4' colspan='4'>"
     html += "Tipo controles"
     html += "</td>"
     html += "</tr>"*/
     /**/
     
     
   /*  html += "<tr>"
     html += "<td style='text-align:center' colspan='4'>"
     html += "<div style='font-size:8pt;width:590px; border: 1px solid #54709B;text-align:right '>"
     html += " <input onclick='FactualizarControles(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"*/
     
     
     
//      html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center' colspan='4'>Tipo controles"
//     html += "<div style='font-size:8pt;width:590px;  text-align:right '>"
//     html += " <input onclick='FactualizarControles(this)' id='Checkbox1' type='checkbox' />"
//     html += "</div>"
//     html += "</td>"
//     html += "</tr>"
//     html += "<tr>"


     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center' colspan='4'>"
     html += "<div style='font-size:8pt;width:525px;  text-align:center ;float:left'>"
     html += "Tipo controles</div>"
     html += "<div style='font-size:8pt;width:112px;  text-align:right ;float:left'>"
     html += " <input onclick='FactualizarControles(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"


     html += "<td colspan='2' style='text-align:center' >"
     html += "<table width=300px style=' ' cellspacing='0' cellpadding='0' border='0' id='TiposControlesActual'>"
     for (var indice = 0; indice < ListaFichaMedicaTiposControlesActual.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='TiposControlesActualOver(this)' onmouseout='TiposControlesActualOut(this)'>"
         html += "<td colspan='2'><div style='font-size:8pt;width:150px; ;text-align:center '>" + ListaFichaMedicaTiposControlesActual[indice].TC_Descripcion + "</div></td>"
         html += "</tr>"

     }
     html += "</table>"
     html += "</td>"

     html += "<td colspan='2' style='text-align:center;width:300px' >"
     html += "<table width=300px style='border-style: solid; border-width: 1px' id='FichaMedicaTiposControlesNuevo'>"
     for (var indice = 0; indice < ListaFichaMedicaTiposControlesNuevo.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='FichaMedicaTiposControlesNuevoOver(this)' onmouseout='FichaMedicaTiposControlesNuevoOut(this)'>"
         html += "<td colspan='2'><div style='font-size:8pt;width:150px;  text-align:center '>" + ListaFichaMedicaTiposControlesNuevo[indice].TC_Descripcion + "</div></td>"
         html += "</tr>"
     }
     html += "</table>"
     html += "</td>"
     html += "</tr>"
     return html;
  /**/
 }

 //onmouseover='CarecteristicasPielNuevoOver(this)' onmouseout='CarecteristicasPielNuevoOut(this)'
 function TiposControlesActualOver(control) {
     $("#TiposControlesActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function TiposControlesActualOut(control) {
     $("#TiposControlesActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 function FichaMedicaTiposControlesNuevoOver(control) {
     $("#FichaMedicaTiposControlesNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function FichaMedicaTiposControlesNuevoOut(control) {
     $("#FichaMedicaTiposControlesNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }


 function FactualizarControles(control) {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
    // ListaFichaMedicaTiposControlesNuevo[index].RFTC_Check = estadoMarcado

     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }
     for (var indice = 0; indice < ListaFichaMedicaTiposControlesNuevo.length; indice++) {
         ListaFichaMedicaTiposControlesNuevo[indice].RFTC_Check = estadoMarcado
     }
 }
 /////////////////////////////////////////////

 var ListaFichaMedicaVacunasActual = []
 var ListaFichaMedicaVacunasNuevo = []
 function crearFilasVacunas(res) {
     ListaFichaMedicaVacunasActual = res.d.LStBE_EN_RelacionFichaMedicasVacunas
     ListaFichaMedicaVacunasNuevo = res.d.LStBE_EN_RelacionFichaMedicasVacunas_temp
     var html = "";

     if (  ListaFichaMedicaVacunasNuevo.length == 0) {
         return html;
     }

//     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center' colspan='4'>"
//     html += "<div style='font-size:8pt;width:525px;  text-align:right ;float:left'>"
//     html += "Lista Medicamentos</div>"
//     html += "<div style='font-size:8pt;width:75px;  text-align:right ;float:left'>"
//     html += " <input onclick='FactualizarMedicamentos(this)' id='Checkbox1' type='checkbox' />"
//     html += "</div>"
//     html += "</td>"
//     html += "</tr>"
//     html += "<tr>"

     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center;width:600px' colspan='4'>"
     html += "<div style='font-size:8pt;width:525px;  text-align:center ;float:left'>"
     html += "Vacunas</div>"
     html += "<div style='font-size:8pt;width:112px;  text-align:right ;float:left'>"
     html += " <input onclick='FactualizarVacunas(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"





     html += "<td colspan='2' style='text-align:center;' >"
     html += "<table width=350px style=' ' id='MedicaVacunasActual' >"
     //

     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td><div style='font-size:8pt;width:150px;  ;text-align:center ; '>Dosis</div></td>"
     html += "<td><div style='font-size:8pt;width:150px; ;text-align:center;  '>Vacuna</div></td>"
     html += "</tr>"
     //
     for (var indice = 0; indice < ListaFichaMedicaVacunasActual.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='MedicaVacunasActualOver(this)' onmouseout='MedicaVacunasActualOut(this)'>"
         html += "<td><div style='font-size:8pt;width:150px;  text-align:center '>" + ListaFichaMedicaVacunasActual[indice].DV_Descripcion +"</div></td>"
         html += "<td><div style='font-size:8pt;width:150px;  text-align:center '> " + ListaFichaMedicaVacunasActual[indice].VC_Descripcion + "</div></td>"
         
         html += "</tr>"

     }
     html += "</table>"
     html += "</td>"

     html += "<td colspan='2' style='text-align:center;' >"
     html += "<table width=300px style=' ' cellspacing='0' cellpadding='0' border='0' id='MedicaVacunasNuevo' >"
     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td><div style='font-size:8pt;width:150px;  ;text-align:center '>Dosis</div></td>"
     html += "<td><div style='font-size:8pt;width:150px;   ;;text-align:center '>Vacuna</div></td>"
     html += "</tr>"

     for (var indice = 0; indice < ListaFichaMedicaVacunasNuevo.length; indice++) {

         html += "<tr id='" + indice + "' onmouseover='MedicaVacunasNuevoOver(this)' onmouseout='MedicaVacunasNuevoOut(this)' >"
         html += "<td><div style='font-size:8pt;width:150px;  ;text-align:center '>" + ListaFichaMedicaVacunasNuevo[indice].DV_Descripcion + "</div></td>"
         html += "<td><div style='font-size:8pt;width:150px; ;text-align:center '> " + ListaFichaMedicaVacunasNuevo[indice].VC_Descripcion + "</div></td>"

         html += "</tr>"
      
     
     }
     html += "</table>"
     html += "</td>"
     html += "</tr>"
     return html;
     /**/
 }

 //onmouseover='CarecteristicasPielNuevoOver(this)' onmouseout='CarecteristicasPielNuevoOut(this)'
 function MedicaVacunasActualOver(control) {
     $("#MedicaVacunasActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function MedicaVacunasActualOut(control) {
     $("#MedicaVacunasActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 function MedicaVacunasNuevoOver(control) {
     $("#MedicaVacunasNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function MedicaVacunasNuevoOut(control) {
     $("#MedicaVacunasNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 
 
 function FactualizarVacunas(control) {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
     //ListaFichaMedicaVacunasNuevo[index].RFVC_Check = estadoMarcado
     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }

     for (var indice = 0; indice < ListaFichaMedicaVacunasNuevo.length; indice++) {
         ListaFichaMedicaVacunasNuevo[indice].RFVC_Check = estadoMarcado
     }
 }
 //------------------------------------------------------------------------------
 var ListaFichaMedicaMedicamentosActual = []
 var ListaFichaMedicaMedicamentosNuevo = []
 function crearFilasMedicamentos(res) {
     ListaFichaMedicaMedicamentosActual = res.d.LStBE_EN_RelacionFichaMedicasMedicamentos
     ListaFichaMedicaMedicamentosNuevo = res.d.LStBE_EN_RelacionFichaMedicasMedicamentos_temp
     var html = "";


     if (  ListaFichaMedicaMedicamentosNuevo.length == 0) {
         return html;
     }

/*
     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center;background-color:#DCE6F4' colspan='4'>"
     html += "Lista Medicamentos"
     html += "</td>"
     html += "</tr>"
     */
     /**/

    /* html += "<tr>"
     html += "<td style='text-align:center' colspan='4'>"
     html += "<div style='font-size:8pt;width:590px; border: 1px solid #54709B;text-align:right '>"
     html += " <input onclick='FactualizarMedicamentos(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"*/

    
//        html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
//     html += "<td style='text-align:center' colspan='4'>Lista Medicamentos"
//     html += "<div style='font-size:8pt;width:590px;  ;text-align:right '>"
//     html += " <input onclick='FactualizarMedicamentos(this)' id='Checkbox1' type='checkbox' />"
//     html += "</div>"
//     html += "</td>"
//     html += "</tr>"
//     html += "<tr>"


     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td style='text-align:center' colspan='4'>"
     html += "<div style='font-size:8pt;width:525px;  text-align:center ;float:left'>"
     html += "Lista Medicamentos</div>"
     html += "<div style='font-size:8pt;width:112px;  text-align:right ;float:left'>"
     html += " <input onclick='FactualizarMedicamentos(this)' id='Checkbox1' type='checkbox' />"
     html += "</div>"
     html += "</td>"
     html += "</tr>"
     html += "<tr>"
      
      

     html += "<td colspan='2' style='text-align:center' >"
     html += "<table width=300px  style=' ' cellspacing='0' cellpadding='0' border='0' id='MedicaMedicamentosActual'>"
     for (var indice = 0; indice < ListaFichaMedicaMedicamentosActual.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='MedicamentosActualOver(this)' onmouseout='MedicamentosActualOut(this)'>"
         html += "<td colspan='2'><div style='font-size:8pt;width:300px;  ;text-align:center '>" +ListaFichaMedicaMedicamentosActual[indice].MA_Descripcion + " - " + ListaFichaMedicaMedicamentosActual[indice].PM_Descripcion + "</div></td>"
         html += "</tr>"

     }
     html += "</table>"
     html += "</td>"

     html += "<td colspan='2' style='text-align:center' >"
     html += "<table width=300px style='' id='MedicamentosNuevo'>"
     for (var indice = 0; indice < ListaFichaMedicaMedicamentosNuevo.length; indice++) {
         html += "<tr id='" + indice + "' onmouseover='MedicamentosNuevoOver(this)' onmouseout='MedicamentosNuevoOut(this)'>"
         html += "<td colspan='2'><div style='font-size:8pt;width:300px; ;text-align:center '>" + ListaFichaMedicaMedicamentosNuevo[indice].MA_Descripcion + " - " + ListaFichaMedicaMedicamentosNuevo[indice].PM_Descripcion + "</div></td>"
         html += "</tr>"
     }
     html += "</table>"
     html += "</td>"
     html += "</tr>"
     return html;
     /**/
 }
 //onmouseover='CarecteristicasPielNuevoOver(this)' onmouseout='CarecteristicasPielNuevoOut(this)'
 function MedicamentosActualOver(control) {
     $("#MedicaMedicamentosActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function MedicamentosActualOut(control) {
     $("#MedicaMedicamentosActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 function MedicamentosNuevoOver(control) {
     $("#MedicamentosNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function MedicamentosNuevoOut(control) {
     $("#MedicamentosNuevo tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 
 
 
 
 function FactualizarMedicamentos(control) {
  
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
     // ListaFichaMedicaMedicamentosNuevo[index].RFMD_Check = estadoMarcado

     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }
     for (var indice = 0; indice < ListaFichaMedicaMedicamentosNuevo.length; indice++) {
         ListaFichaMedicaMedicamentosNuevo[indice].RFMD_Check = estadoMarcado
     }

 }
 var fuenteDatosCliente = [];

 function crearGrilla(Tsource) {
     $("#pnlResultado").html("")
   
     fuenteDatosCliente = Tsource;
     var html = "";
     /**/
/**/
    
     html = "<table cellspacing='0' cellpadding='0' border='0' id='grilla' style='width:100%'>"
     html += "<thead>"
     html += "<tr class='miGridviewBusquedaActualizacion_Ficha_Header'>"
     html += "<td align='center' style= 'height:25px; border: 1px solid  ' ><div style='font-size:8pt;width:45px'>Seleccionar</div></td>"
     html += "<td align='center' style= 'height:25px; border: 1px solid ' ><div style='font-size:8pt;width:125px'>Nombre alumno</div></td>"

//     html += "<td align='center' style= 'height:25px; border: 1px solid' ><div style='font-size:8pt;width:150px'>Estado Alumno</div></td>"
     
     html += "<td align='center' style= 'height:25px; border: 1px solid  ' ><div style='font-size:8pt;width:85px'>Fecha Solicitud</div></td>"
     html += "<td align='center' style= 'height:25px; border: 1px solid  ' ><div style='font-size:8pt;width:125px'>Nombre Solicictante</div></td>"
     html += "<td align='center' style= 'height:25px; border: 1px solid' ><div style='font-size:8pt;width:85px'>Parentesco</div></td>"
     html += "<td align='center' style= 'height:25px; border: 1px solid ' ><div style='font-size:8pt;width:85px'>Estado Solicitud</div></td>"

    

     
     html += "</tr>"
     html += "</thead>"
     for (var indice = 0; indice < Tsource.length; indice++) {
         html += "<tr id='" + indice + "' onmousemove='over(this)' onmouseout='out(this)'>"




         if (Tsource[indice].estadoSolicitud == "Eliminado" || Tsource[indice].estadoSolicitud == "Validado" )
                {
                    html += "<td align='center'><div style='font-size:8pt;height:25px;width:45px; border-top-style: solid;border-bottom-style: solid;border-bottom-color:#DCE6F4';text-align:center '><div></td>"
                }
        else
               {
                   html += "<td align='center'><div style='font-size:8pt;width:45px;height:25px;;border-top-style: solid;border-bottom-style: solid;border-bottom-color:#DCE6F4';text-align:center '><img onclick='edicionGrilla(" + Tsource[indice].SAFM_CodigoSolicitud + "," + Tsource[indice].AL_CodigoAlumno + "," + indice + ")' title='Seleccionar' style='cursor:pointer;width: 12px; height: 12px'  src='../App_Themes/Imagenes/opc_seleccionar.png' /></div></td>"
               }
              

 

                   html += "<td align='center' style= '  border-top-style: solid;border-bottom-style: solid;border-bottom-color:#DCE6F4' ><div style='font-size:8pt;width:125px'>" + Tsource[indice].nombreSol + "</div></td>"
      
      
//      html += "<td align='center' style= '  border-top-style: solid;border-bottom-style: solid;border-bottom-color:#DCE6F4' ><div style='font-size:8pt;width:150px'>" + Tsource[indice].AL_EstadoActualAlumno + "</div></td>"
      html += "<td align='center' style= '  border-top-style: solid;border-bottom-style: solid;border-bottom-color:#DCE6F4' ><div style='font-size:8pt;width:125px'>" + Tsource[indice].SAFM_FechaHoraSolicitud + "</div></td>"

      html += "<td align='center' style= ' border-top-style: solid;border-bottom-style: solid;border-bottom-color:#DCE6F4' ><div style='font-size:8pt;width:125px'>" + Tsource[indice].nombreAlumno  + "</div></td>"
      
      html += "<td align='center' style= '  border-top-style: solid;border-bottom-style: solid;border-bottom-color:#DCE6F4' ><div style='width:125px'>" + Tsource[indice].PT_Descripcion + "</div></td>"

      html += "<td align='center' style= '  border-top-style: solid;border-bottom-style: solid;border-bottom-color:#DCE6F4' ><div style=font-size:8pt;'width:85px;font-size:8pt'>" + Tsource[indice].estadoSolicitud + "</div></td>"

           
            
            html += "</tr>"
    }
    
    


     html += "</table>"

     $("#pnlResultado").html(html)





 }
 function over(control) {

     $("#grilla tr[id='"+$(control).attr("id")+"']").css("backgroundColor", "#DEE8F5")
 }
 function out(control) {

     $("#grilla tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#EEEEEE")
 }

 function overEdicion(control) {

     $("#edicion tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
 }
 function outEdicion(control) {

     $("#edicion tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
 }
 
  
 
 
     $(document).ready(
 function() {
     $("#menu").hide('fast');
     /**/
     $("#txtFechaInicio").datepicker({ dateFormat: 'dd/mm/yy' });

     $("#txtFechaFin").datepicker({ dateFormat: 'dd/mm/yy' });
     
     
 });
 var listCliente = [];
 var codSolicitud = 0;
 var codAlumno = 0

 function edicionGrilla(codSol, pcodAlumno,index) {
     $("#tabs").tabs("enable", 1);
     $("#tabs").tabs("select", 1);

     $("#tabs").tabs("disable", 0);
   
     
   //  alert(fuenteDatosCliente[index].nombreAlumno)
     

$("#nombreSol").text(fuenteDatosCliente[index].nombreSol)
$("#fechaSol").text(fuenteDatosCliente[index].SAFM_FechaHoraSolicitud)
$("#estadoSol").text(fuenteDatosCliente[index].estadoSolicitud)

$("#nombreAlumno").text(fuenteDatosCliente[index].nombreAlumno)
$("#estadoAnio").text(fuenteDatosCliente[index].AL_EstadoActualAlumno +" / "+fuenteDatosCliente[index].AC_Descripcion)
$("#imgFotoGrande").attr("src", fuenteDatosCliente[index].AL_RutaFoto)
/**/


//.AC_Descripcion = sql.Field(Of Object)("AC_Descripcion"), _
//.AL_EstadoActualAlumno
/**/
    // $('#modalFicha').dialog("open");
     $("#edicionFicha").html("")
     codSolicitud = codSol
     codAlumno = pcodAlumno

     $.ajax({
         url: "frmActualizacionEnfermeria.aspx/F_listarFichaAlumnos",
         cache: false,
         type: "post",
         data: JSON.stringify({
             codSol: codSol,
             codAlumno: codAlumno
         }),
         dataType: "json",
         contentType: "application/json; charset=utf-8",
         success: function(res, textStatus, jqXHR) {

             /*
             Return New With {.actual = lsFichaActual, .actualizar = lsActualiza}
             */
             var listaActual = res.d.actual
             var listaActualizar = res.d.actualizar
             listCliente = listaActualizar;
             var html = "";
             html += "<table  cellspacing='0' cellpadding='0' border='0' id='edicion' width='600px'>";
             html += "<tr  class='miGridviewBusquedaActualizacion_Ficha_Header'>"
             html += "<td style='font-size:8pt;width:175px;'><div style='font-size:8pt;width:175px;  text-align:center '>Nombre campo </div></td>"
             
             html += "<td style='font-size:8pt;width:175px;'><div style='font-size:8pt;width:175px;  text-align:center '>Actual</div></td>"
             
             html += "<td style='font-size:8pt;width:175px;'><div style='font-size:8pt;width:175px;  text-align:center '>Datos actualizados por lal familia</div></td>"
             
             html += "<td style='width:75px;'><div style='float:left;font-size:8pt;width:75px;  text-align:right '><input id='chkAll' onclick='todos(this)' id='Checkbox1' type='checkbox' /></div></td>"
             html += "</tr>"

             for (var indice = 0; indice < listaActual.length; indice++) {


                 if (listaActualizar[indice].descripcion == "--" || listaActualizar[indice].descripcion == "0") {
                     continue;
                 }
                 
                 //
                 html += "<tr onmousemove='overEdicion(this)' onmouseout='outEdicion(this)' id='" + indice + "'>"
                 html += "<td style='font-size:8pt;width:175px;border-bottom-color:Black;border-bottom-style:solid'><div style='font-size:8pt;width:175px; ;text-align:left '>" + listaActualizar[indice].etiqueta + "</td>"
                 html += "<td style='font-size:8pt;width:175px;border-bottom-color:Black;border-bottom-style:solid'><div style='font-size:8pt;width:175px; ;text-align:left '>" + listaActual[indice].descripcion + "</td>"
                 html += "<td style='font-size:8pt;width:175px;border-bottom-color:Black;border-bottom-style:solid'><div style='font-size:8pt;width:175px;text-align:left '>" + listaActualizar[indice].descripcion + "</td>"

                 if (listaActualizar[indice].check == true) {
                     html += "<td style='font-size:8pt;width:75px; border-bottom-color:Black;border-bottom-style:solid'><div style='font-size:8pt;width:75px;text-align:right '><input id='Checkbox1' onclick='marcar(this," + indice + ")' checked='checked' type='checkbox' /></div>"
                 }
                 else {
                     html += "<td style='font-size:8pt;width:75px; border-bottom-color:Black;border-bottom-style:solid'><div style='font-size:8pt;width:75px;text-align:right '><input onclick='marcar(this," + indice + ")' id='Checkbox1' type='checkbox' /></div>"

                 }
                 html += "</tr>"
                 //<input id="Checkbox1" checked="checked" type="checkbox" />
             }
             //  alert(crearFilasAlergia(res))



                    html += crearFilasAlergia(res)

                    html += crearFilasFichaMedicaCarecteristicasPiel(res)
                    html += crearFilasEnfermedades(res)

                    html += crearFilasHospitalizacion(res)
                    html += crearFilasOperaciones(res)
                    html += crearFilasTiposControles(res)
                    html += crearFilasVacunas(res)
                    html += crearFilasMedicamentos(res)

             html += "</table>";

             $("#edicionFicha").html(html)
         }
         /*
         F_listarFichaAlumnos(ByVal codSol As Integer, ByVal codAlumno As Integer)
         */

      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
     });

 }
 function todos(control) 
 {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
     $(":checkbox[id!=chkAll]").attr('checked', estadoMarcado);


     for (var indice = 0; indice < listCliente.length; indice++) {
         listCliente[indice].check = estadoMarcado
     }
     for (var indice = 0; indice < ListaFichaMedicaMedicamentosNuevo.length; indice++) {
         ListaFichaMedicaMedicamentosNuevo[indice].RFMD_Check = estadoMarcado
     }

     for (var indice = 0; indice < ListaFichaMedicaVacunasNuevo.length; indice++) {
         ListaFichaMedicaVacunasNuevo[indice].RFVC_Check = estadoMarcado
     }
     for (var indice = 0; indice < ListaFichaMedicaTiposControlesNuevo.length; indice++) {
         ListaFichaMedicaTiposControlesNuevo[indice].RFTC_Check = estadoMarcado
     }
     for (var indice = 0; indice < ListaFichaMedicaOperacionesNuevo.length; indice++) {
         ListaFichaMedicaOperacionesNuevo[indice].RFOM_Check = estadoMarcado
     }
     for (var indice = 0; indice < ListaFichaMedicaEnfermedadesNuevo.length; indice++) {
         ListaFichaMedicaEnfermedadesNuevo[indice].RFEF_Check = estadoMarcado
     }
     for (var indice = 0; indice < ListaFichaMedicaHospitalizacionNuevo.length; indice++) {
         ListaFichaMedicaHospitalizacionNuevo[indice].RFMH_Check = estadoMarcado
         //  alert(ListaFichaMedicaHospitalizacionNuevo[indice].RFMH_Check)
     }
     for (var indice = 0; indice < lstFichasMedicaCarecteristicasPielNuevo.length; indice++) {
         lstFichasMedicaCarecteristicasPielNuevo[indice].RFCP_Check = estadoMarcado
     }
     for (var indice = 0; indice < lstAlergiasNuevo.length; indice++) {
         // alert(lstAlergiasNuevo[indice].RFAG_Check)
         lstAlergiasNuevo[indice].RFAG_Check = estadoMarcado
     }
      
 
 }

 function cancelar() {
     if (confirm("Seguro que  desea cancelar ")) {

        
       
         $("#tabs").tabs("enable", 0);
         $("#tabs").tabs("select", 0);
         $("#tabs").tabs("disable", 1);
     }
    
 
 }
 function actualizarFicha() 
 {

     if ( ! confirm("Seguro que  desea confirmar los cambios")) {
         return false; 
        
     }
      
 
 //F_ActualizarEstado(ByVal lisTsqlObjectActualiza As List(Of sqlObjectActualiza))
     $.ajax({
         url: "frmActualizacionEnfermeria.aspx/F_ActualizarEstado",
         cache: false,
         type: "post",
         data: JSON.stringify({
             lisTsqlObjectActualiza: listCliente,
             codSol: codSolicitud,
             codAlumno: codAlumno,
             lstMedicasAlergias_temp: lstAlergiasNuevo,
             lstCarecteristicasPiel: lstFichasMedicaCarecteristicasPielNuevo,
             lstEnfermedades: ListaFichaMedicaEnfermedadesNuevo,
             lstHospitalizacion: ListaFichaMedicaHospitalizacionNuevo,
             lstOperaciones: ListaFichaMedicaOperacionesNuevo,
             lstTiposControles: ListaFichaMedicaTiposControlesNuevo,
             lstVacunas: ListaFichaMedicaVacunasNuevo,
             lstMedicamentos: ListaFichaMedicaMedicamentosNuevo
              

         }),
         dataType: "json",
         contentType: "application/json; charset=utf-8",
         success: function(res, textStatus, jqXHR) {
         if (res.d == 0) {
             
                 alert("Error intente de nuevo ")
                 
             }
             else {

                 alert("Se guardo correctamente")
                 $('#modalFicha').dialog("close");

                 $("#tabs").tabs("enable", 0);
                 $("#tabs").tabs("select", 0);
                 $("#tabs").tabs("disable", 1);
                 
                 F_buscarSolicitud();
                 
             }
             
         }
          , error: function(xhr, ajaxOptions, thrownError) {
              alert(xhr.status); alert(thrownError);
          }
     });


 }

// $(document).ready(function() {

// $(":checkbox[id!=chkAll]").each(function(index, ctr) {
//     
//         $(this).click(function() {
//         var estadoMarcado = false;
//             
//             estadoMarcado = ($(this).attr("checked") == "checked") ? true : false
//             if (!estadoMarcado) {
//                 $(":checkbox[id!=chkAll]").attr('checked', estadoMarcado);
//             }
//         });

//     });


// });
 function marcar(control, index) 
 {
     var estadoMarcado = false;
     estadoMarcado = ($(control).attr("checked") == "checked") ? true : false
     listCliente[index].check = estadoMarcado
     if (!estadoMarcado) {
         $("#chkAll").attr('checked', estadoMarcado);
     }

 }

 /**/

 function ordenar(){
     $.ajax({
         url: "frmActualizacionEnfermeria.aspx/F_OrdenarGrilla",
         cache: false,
         type: "post",
         dataType: "json",
         contentType: "application/json; charset=utf-8",
         data: JSON.stringify({ lst: fuenteDatosCliente }),
          success: function(res, textStatus, jqXHR) {
         
         }
          , error: function(xhr, ajaxOptions, thrownError) {
              alert(xhr.status); alert(thrownError);
          }
     });
         
 }
 </script>
    <link href="../App_Themes/Estilos/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
  

</asp:Content>

