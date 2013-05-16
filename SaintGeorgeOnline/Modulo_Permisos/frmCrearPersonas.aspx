<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="frmCrearPersonas.aspx.vb" Inherits="Modulo_Permisos_frmCrearPersonas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .milegend
{
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
INPUT
{
	 font-size:8pt
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
    
        #txtPefil
        {
            width: 247px;
        }
    
        #txtCorreoSkype
        {
            width: 377px;
        }
        #txtCorrreoCorp
        {
            width: 375px;
        }
        #txtNombreEdicion
        {
            width: 375px;
        }
        #txtApPaternoEdicion
        {
            width: 373px;
        }
        #txtApMaterno
        {
            width: 374px;
            height: 20px;
        }
    
        #txtApellidoMaterno
        {
            width: 195px;
        }
        #txtApellidoPaterno
        {
            width: 197px;
        }
        #txtNombre
        {
            width: 198px;
        }
    
        #Text1
        {
            width: 197px;
        }
        #Text2
        {
            width: 198px;
        }
    
        #txtApPaternoCambiarPass
        {
            width: 193px;
        }
        #txtApMaternoCambiarPass
        {
            width: 191px;
        }
        #txtNombreCambiarPAss
        {
            width: 188px;
        }
    
        #txtCodigoTrabajador
        {
            width: 375px;
        }
    
       </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="height: 1253px">




    <div style="height: 715px; width: 924px">
<fieldset style="height: 550px; margin-left:10px; width: 689px; padding-left:5px">

<legend class="milegend">
Buscar persona 
</legend>
<div style=" height:auto; overflow:hidden; width:690px">
<div style=" height:25px ;width:690px; line-height:25px">
<div style=" height:25px ;width:85px; float:left">

</div>
<div style=" height:25px ;width:85px; float:left">

</div>
</div>
<div style =" height:auto; overflow:hidden;">
<div style=" height:45px ;width:690px; line-height:55px">
<div style=" height:45px ;width:100px; float:left; font-size:10pt; ">

    Apellido Paterno</div>
<div style=" height:25px ;width:200px; float:left; margin-top:15px ">

    <input id="txtApellidoPaterno" type="text" />
    
    </div>
    <div style=" float:right;height:45px ;width:100px; line-height:45px;  ; text-align:right; margin-right:20px">
    
    
    
    
    &nbsp;<img onclick="listarpersonasPerfil()" 
          title="Buscar"  style="  cursor:pointer;height:22px ; width:98px;" src="../App_Themes/Imagenes/botonesGSV/btnBuscarV2_1.png" 
       onmouseout="this.src = '../App_Themes/Imagenes/botonesGSV/btnBuscarV2_1.png'" 
            
            onmouseover="this.src = '../App_Themes/Imagenes/botonesGSV/btnBuscarV2_2.png'"/></div>
    
     <div style=" float:right;height:45px ;width:100px; line-height:45px; margin-right:15px ">
    &nbsp;</div>
   
</div>
<div style=" height:25px ;width:690px; line-height:25px">
<div style=" height:25px ;width:100px; float:left; font-size:9pt">

    Apellido Materno</div>
<div style=" height:25px ;width:200px; float:left">

    <input id="txtApellidoMaterno" type="text" /></div>
</div>
<div style=" height:25px ;width:690px; line-height:25px">
<div style=" height:25px ;width:100px; float:left; font-size:9pt">

Nombre</div>
<div style=" height:25px ;width:200px; float:left">

    <input id="txtNombre" type="text" /></div>
    <div style=" float:right;height:25px ;width:150px; margin-right:20PX; text-align:right">
    &nbsp;<img   title="Limpiar filtro" style="  cursor:pointer;height:21px; width:96px;" 
            src="../App_Themes/Imagenes/btnLimpiar_1.png"   
          onclick="borrar()"
            onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'" 
            onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'"/>
            </div>
</div>
<div style=" height:25px ;width:690px; line-height:25px">
<div style=" height:25px ;width:100px; float:left; font-size:9pt">

    Perfil</div>
<div style=" height:25px ;width:200px; float:left">

    <select id="cmbPerfil" name="D1" style=" width:192px" >
        <option codTipoPerfil="0" value="0">--------------Todos---------</option>
        <%For Each fiPerfil As System.Data.DataRow In dtPerfil.Rows%>
            <option codTipoPerfil="<%=fiPerfil("codTipoPerfil") %>" value="<%=fiPerfil("PS_CodigoPerfil") %>"><%=fiPerfil("PS_Descripcion")%></option>
        <%Next%>
    </select>
    
    </div>
    <div style=" float:left;height:25px ;width:150px; margin-left:75PX; float:right; text-align:right; margin-right:20px">
    &nbsp;<img   title="Nuevo" style="  cursor:pointer;height:21px; width:96px;" 
            src="../App_Themes/Imagenes/botonesGSV/btnCorreoNuevo_1.png"   
            onclick="NuevaPersona()"
            onmouseout="this.src = '../App_Themes/Imagenes/botonesGSV/btnCorreoNuevo_1.png'" 
            
             
            
            onmouseover="this.src = '../App_Themes/Imagenes/botonesGSV/btnCorreoNuevo_2.png'"/></div>
</div>
<div style=" height:25px ;width:690px; line-height:25px">
<div style=" height:25px ;width:100px; float:left; font-size:9pt">

    Estado</div>
<div style=" height:25px ;width:200px; float:left">

   <select id="cmbEstados">
<option value="-1">-----Todos-----</option>
<option value="1">Activos</option>
<option value="0">Inactivos</option>
   </select>
    
    </div>
    
</div>
</div>

<div style="height:auto ;width:690px; overflow:hidden ">
<div id="grilla" style="  ">

</div>
<div id="paginas">
</div>
<div >
<strong>Cantidad registros encontrados:</strong>
<strong id="cantidad"></strong>
</div>
</div>
</div>



</fieldset>
    

    
    </div>


 <%-------------------------------------------------------------------------------------------%>
 
 <div id="edicionPersona" style=" ; height:auto;width:673px; ">
 
 <fieldset style=" margin-left:10px; padding-left:5px; width: 614px; padding-top:5px;">
 <legend class=" milegend">
 Edicion Persona 
 </legend>
 
     <div style="height:25px; width:650px; margin-top:5px">
      <div style="height:25px; width:200px;float:left; line-height:25px ">
     
          Apellido Paterno</div>
     <div style="height:25px; width:380px;float:left ">
     <input type="text" id="txtApPaternoEdicion" />
    </div>
    
    <div style=" height:25px; width:50px;float:left; line-height:25px">
    
        *</div>
   <%--  <div style="height:25px; width:250px;float:left; padding-left:10PX ">
     <strong style="  "></strong>
     </div>--%>
    
    </div>
      <div style="height:25px; width:650px; margin-top:5px">
      <div style="height:25px; width:200px;float:left; line-height:25px ">
     
          Apellido Materno</div>
     <div style="height:25px; width:380px; float:left">
     <input type="text" id="txtApMaterno" />
    </div>
    </div>
      <div style="height:25px; width:650px ; margin-top:5px">
      <div style="height:25px; width:200px;float:left ; line-height:25px ">
     
          Nombre</div>
     <div style="height:25px; width:380px;float:left ">
     <input type="text" id="txtNombreEdicion" />
    </div>
    
    <%------%>
    <div style=" height:25px; width:50px;float:left; line-height:25px">
    
        *</div>
    <%------%>
    </div>
    
    <div style="height:25px; width:650px; margin-top:5px">
      <div style="height:25px; width:200px;float:left ; line-height:25px ">
     
          Correo coorporativo</div>
     <div style="height:25px; width:380px;float:left ">
     <input type="text" id="txtCorrreoCorp" />
    </div>
    </div>
    
    
   <%-- '' --%>
    <div style="height:25px; width:650px; margin-top:5px">
      <div style="height:25px; width:200px;float:left ; line-height:25px ">
     
          Codigo Asistencia </div>
     <div style="height:25px; width:380px;float:left ">
     <input type="text" id="txtCodigoTrabajador" />
    </div>
    </div>
    
  <%--  ''--%>
    
    
    <div style="height:25px; width:650px; margin-top:5px">
      <div style="height:25px; width:200px;float:left ; line-height:25px ">
     
          Correo Skype</div>
     <div style="height:25px; width:380px;float:left ">
     <input type="text" id="txtCorreoSkype" />
    </div>
    </div>
      <div style="height:25px; width:650px; margin-top:5px">
      <div style="height:25px; width:200px;float:left ; line-height:25px ">
     
          Enseña</div>
     <div style="height:25px; width:300px;float:left ">
     
     <input type="checkbox" id="chkEnsenia" /></div>
    </div>
     <div style="height:25px; width:650px ; margin-top:5px">
      <div style="height:25px; width:200px;float:left ; line-height:25px ">
     
          Estado</div>
     <div style="height:25px; width:380px;float:left ">
      <input type="checkbox" id="chkEstado" /></div>
    </div>
    
      <div style="height:25px; width:650px ; margin-top:5px">
      <div style="height:25px; width:200px;float:left ; line-height:25px ">
     
          Asistente</div>
     <div style="height:25px; width:380px;float:left ">
      <input type="checkbox" id="chkAsistente" /></div>
    </div>
     
      <div style="height:25px; width:650px ; margin-top:5px">
      <div style="height:25px; width:200px;float:left ; line-height:25px ">
     
          Perfil</div>
     <div style="height:25px; width:380px;float:left ">
     <select id="cmbPerfilEdicion" name="D1" style=" width:376px" >
        <option codTipoPerfil="1" value="0">--------------Selecione---------</option>
        
        <%For Each fiPerfil As System.Data.DataRow In dtPerfil.Rows%>
            <option codTipoPerfil="<%=fiPerfil("codTipoPerfil") %>" value="<%=fiPerfil("PS_CodigoPerfil") %>"><%=fiPerfil("PS_Descripcion")%></option>
        <%Next%>
    </select>
    </div>
    <div style=" float:left; width:58px; height:25px; margin-left:10px">
        
        <img title="Nuevo" onclick="abrirEdicionPerfil()" alt="" src="../App_Themes/Imagenes/Add-icon.png" style="width: 24px; height: 24px; margin-left:5px; cursor:pointer" />
    </div>
    
    </div>
    
    
    
    
    <div style="height:45px; width:650px; text-align:center; line-height:45px; margin-top:5px;">
    
    <img title="Grabar" id="btnGrabarNotas" alt="" 
            onclick="F_insertarPersona()"
            onmouseout="this.src = '../App_Themes/Imagenes/botonesGSV/btnGrabar_1.png'" 
            onmouseover="this.src = '../App_Themes/Imagenes/botonesGSV/btnGrabar_2.png'" 
            src="../App_Themes/Imagenes/botonesGSV/btnGrabar_1.png"
            
            style="border-style: none; border-color: inherit; border-width: 0; cursor: pointer; width: 80px; height: 26px; " />
            
            
             
    <img title="Grabar" id="Img2" alt="" 
            onclick="F_cancelar()"
            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelarV2_1.png'" 
            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelarV2_2.png'" 
            src="../App_Themes/Imagenes/btnCancelarV2_1.png"
            
            style="border-style: none; border-color: inherit; border-width: 0; cursor: pointer; width: 80px; height: 26px; " />
            
            
    </div>
    
    <div id="resetPass" style="height:25px; width:580px ; text-align :left; padding-left:20px; margin-top:5px">
    
    <input type="button" onclick="F_ResetearPass()"  value="Resetear  Pass" />
    
    </div>
    </fieldset>
</div>


 <%---------------------------------------------------------------------------------------------%>


<div id="edicionPerfil" style="height:auto ;width:600px;">
  
  <fieldset>
  
  <legend class=" milegend">
  Edicion perfil
  </legend>
 
  <div style="height:25px ;width:600px; margin-top:5px">
<div style="height:25px ;width:100px;; float:left">Tipo Perfil</div>
<div style="height:25px ;width:250px; float:left">

<select id="cmbTipoPerfil" name="D1" style=" width:246px" >
        <option value="0">--------------Selecione---------</option>
        <%For Each fiPerfil As System.Data.DataRow In dtTipoPerfil.Rows%>
           <% If fiPerfil("TP_CodigoTipoPerfil") = 1 Then%>
            <option selected="selected" value="<%=fiPerfil("TP_CodigoTipoPerfil") %>"><%=fiPerfil("TP_Descripcion")%></option>
         <%else %>
             <option  value="<%=fiPerfil("TP_CodigoTipoPerfil") %>"><%=fiPerfil("TP_Descripcion")%></option>
         <%End If%>
        <%Next%>
</select>

</div>
</div>
<div style="height:25px ;width:600px;">
<div style="height:25px ;width:100px;; float:left">Perfil</div>
<div style="height:25px ;width:250px; float:left"><input type= "text" id="txtPefil" /></div>

</div>
<div style="height:40px ;width:600px; line-height:40px; text-align:center">

<img title="Grabar" id="Img1" alt="" 
            onclick="F_crearPerfil()"
            onmouseout="this.src = '../App_Themes/Imagenes/botonesGSV/btnGrabar_1.png'" 
            onmouseover="this.src = '../App_Themes/Imagenes/botonesGSV/btnGrabar_2.png'" 
            src="../App_Themes/Imagenes/botonesGSV/btnGrabar_1.png" 
            
        style="border-style: none; border-color: inherit; border-width: 0; cursor: pointer; width: 83px; height: 22px; " />
        
        <img title="Grabar" id="Img3" alt="" 
            onclick="F_cancelarPerfil()"
            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelarV2_1.png'" 
            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelarV2_2.png'" 
            src="../App_Themes/Imagenes/btnCancelarV2_1.png"
            
            
        style="border-style: none; border-color: inherit; border-width: 0; cursor: pointer; width: 77px; height: 22px; " />
        
</div>

 </fieldset>
</div>




<div id="EdicionUsuarioPersona"  style="height:auto ;width:600px;">
<div style="height:25px ; width:600px; ">
<div style="height:25px ; width:125px; float:left ; font-size:8pt; line-height:25px">
    nombre:</div>

<div style="height:25px ; width:200px;; float:left;; line-height:25px ">
    <input id="txtNombreCambiarPAss" readonly="readonly" type="text" /></div>

</div>
<div style="height:25px ; width:600px; ">
<div style="height:25px ; width:125px; float:left ; font-size:8pt; line-height:25px">
    Apellido Paterno:</div>

<div style="height:25px ; width:200px;; float:left ">
    <input id="txtApPaternoCambiarPass" readonly="readonly" type="text" /></div>

</div>
<div style="height:25px ; width:600px; ">
<div style="height:25px ; width:125px; float:left; font-size:8pt ; line-height:25px">
    Apellido Materno:</div>

<div style="height:25px ; width:200px; float:left ">
    <input id="txtApMaternoCambiarPass" readonly="readonly" type="text" /></div>

</div>

<div style="height:45px ; width:200px; line-height:45px; text-align:center">
<input type="button" onclick="generarUsuarioPass() " value="Generar Usuario" />
</div>



</div>

</div>

<script>
    var indiceACtual = -1;
    var BdJson = [];
    function listarpersonasPerfil() {

       var  PApellidoPaterno=$("#txtApellidoPaterno").val();
       var PapellidoMaterno = $("#txtApellidoMaterno").val();
       var PnombrePersona = $("#txtNombre").val();
       var PcodPerfil = $("#cmbPerfil option:selected").val()
       var pestado = parseInt($("#cmbEstados option:selected").val())
       
       $.ajax({
           url: "frmCrearPersonas.aspx/F_listarPersonas",
           cache: false,
           async: false,
           type: "post",
           data: JSON.stringify({
               ApellidoPaterno: PApellidoPaterno,
               apellidoMaterno: PapellidoMaterno,
               nombrePersona: PnombrePersona,
               codPerfil: PcodPerfil,
               pagina:0,
               soloPAginas:0,
               estado: pestado
               /*
                        ByVal paginas As Integer, _
                        ByVal soloPAginas As
               */
           }),
           dataType: "json",
           contentType: "application/json; charset=utf-8",
           success: function(res, textStatus, jqXHR) {
//

           /**/
           $("#cantidad").text(res.d.cantidad)
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
               paginarLista(1, 3);
               /**/
               
               
               
           }
              ,
           error: function(xhr, ajaxOptions, thrownError) {
               alert(xhr.status); alert(thrownError);
           }
       });
      }



      /**/


      function paginarLista(p, jq) {
          var PApellidoPaterno = $("#txtApellidoPaterno").val();
          var PapellidoMaterno = $("#txtApellidoMaterno").val();
          var PnombrePersona = $("#txtNombre").val();
          var PcodPerfil = $("#cmbPerfil option:selected").val()
          var pestado = parseInt($("#cmbEstados option:selected").val())
  
          
          
          $.blockUI({
              message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> cambiando de pagina...</h4>'
          });
          $.ajax({
          url: "frmCrearPersonas.aspx/F_listarPersonas",
              cache: false,
              type: "post",
              async:false,
              data: JSON.stringify({
              //

              ApellidoPaterno: PApellidoPaterno,
              apellidoMaterno: PapellidoMaterno,
              nombrePersona: PnombrePersona,
              codPerfil: PcodPerfil,
              pagina: p,
              soloPAginas: 1,
              estado: pestado
                     //
                   
              }),
              dataType: "json",
              contentType: "application/json; charset=utf-8",
              success: function(res, textStatus, jqXHR) {


                    $("#grilla").html(res.d.ui)
                    BdJson = res.d.data;
                  
                  $.unblockUI();
              }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
          });

      }
/**/



      function ObtieneFilas(index)
       {
           indiceACtual = index;
           $("#resetPass").show();
          $("#edicionPersona").dialog("open");
          TipoOperacion = 1
          
          $("#txtApPaternoEdicion").val(BdJson[index].apellidoPAterno)
          $("#txtApMaterno").val(BdJson[index].apellidoMAterno)
          $("#txtNombreEdicion").val(BdJson[index].nombre)
          $("#cmbPerfilEdicion option[value=" + BdJson[index].codPerfil + "]").attr("selected", true)


          $("#txtCorrreoCorp").val(BdJson[index].correoCorporativo)
          $("#txtCorreoSkype").val(BdJson[index].correoSkype)

         // oPersona.TJ_CodigoTrabajadoreAsistencia = parseInt($("#txtCodigoTrabajador").val());

          $("#txtCodigoTrabajador").val(BdJson[index].TJ_CodigoTrabajadoreAsistencia)
        
          if (BdJson[index].codEnsenia==1)
            {
                  $("#chkEnsenia").attr("checked", true )
            }
          if (BdJson[index].codEnsenia == 0)
             {
                 $("#chkEnsenia").attr("checked", false)

             }
             if (BdJson[index].codAcceso == 1) {
                 $("#chkEstado").attr("checked", true)
             }
             if (BdJson[index].codAcceso == 0) {
                 $("#chkEstado").attr("checked", false)

             }
 
 /**********************************************/
 /***
  /**********************************************/
             if (BdJson[index].esAsistente == 1) {
                 $("#chkAsistente").attr("checked", true)
             }
             if (BdJson[index].esAsistente == 0) {
                 $("#chkAsistente").attr("checked", false)

             }
 
      }


      $(document).ready(function() {

      //EdicionUsuarioPersona
      $('#EdicionUsuarioPersona').dialog({
          autoOpen: false,
          modal: true,
          width: 900,
          height: 600,
          title: "Edicion Usuario Persona",
          buttons: {
              "Cerrar": function() {
                  $(this).dialog("close");
              },
              "Aceptar": function() {
                  $(this).dialog("close");

              }

          }

      });
      ////
      $("#menu").hide('fast');
  $('#edicionPersona').dialog({
      autoOpen: false,
      modal: true,
      width: 700,
      height: 435,
      title: "Edicion Persona"//,
//      buttons: {
//          "Cerrar": function() {
//              $(this).dialog("close");
//          }

//      }
  });
  //


  $('#edicionPerfil').dialog({
      autoOpen: false,
      modal: true,
      width: 650,
      title: "Edicion  articulo",
      buttons: {
//          "Cerrar": function() {
//              $(this).dialog("close");
//          },
//          "Aceptar": function() {
//              $(this).dialog("close");

//          }

      }
  });
  ///
});


function TiposControlesActualOver(control) {
    $("#TiposControlesActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#DEE8F5")
}
function TiposControlesActualOut(control) {
    $("#TiposControlesActual tr[id='" + $(control).attr("id") + "']").css("backgroundColor", "#FFFFFF")
}




function F_insertarPersona() {


//    $("#txtApPaternoEdicion").val();
//    oPersona.apellidoMAterno = $("#txtApMaterno").val();

    //    oPersona.nombre = $("#txtNombreEdicion").val();


        if(TipoOperacion==1)
                {
                    F_actualizarPersona();
                    listarpersonasPerfil();
                }
                else if (TipoOperacion == 0)
                {
                    F_nuevoPersona()
                    listarpersonasPerfil();

                }
               
        
        
    }

/*
nuevo persona

*/
var TipoOperacion = 0;

function NuevaPersona() {
    $("#edicionPersona").dialog("open");

    //
    $("#txtCodigoTrabajador").val("");
    $("#resetPass").hide();
//
    $("#txtApPaternoEdicion").val("")
    $("#txtApMaterno").val("")
    $("#txtNombreEdicion").val("")
    $("#cmbPerfilEdicion option[value=0]").attr("selected", true)
    $("#txtCorrreoCorp").val("")
    $("#txtCorreoSkype").val("")
    $("#chkEnsenia").attr("checked", false)
    $("#chkEstado").attr("checked", false)
    $("#chkAsistente").attr("checked", false)
    
    
    TipoOperacion=0
}

function F_actualizarPersona() {
    BdJson[indiceACtual].apellidoPAterno = $("#txtApPaternoEdicion").val();
    BdJson[indiceACtual].apellidoMAterno = $("#txtApMaterno").val();
    BdJson[indiceACtual].nombre = $("#txtNombreEdicion").val();
    BdJson[indiceACtual].codPerfil = parseInt($("#cmbPerfilEdicion option:selected").val());
    BdJson[indiceACtual].correoCorporativo = $("#txtCorrreoCorp").val();
    BdJson[indiceACtual].correoSkype = $("#txtCorreoSkype").val();

    BdJson[indiceACtual].TJ_CodigoTrabajadoreAsistencia = parseInt(($("#txtCodigoTrabajador").val() == "") ? 0 : $("#txtCodigoTrabajador").val());

    

    BdJson[indiceACtual].codEnsenia = ($("#chkEnsenia").attr("checked") == "checked") ? true : false;
    BdJson[indiceACtual].codAcceso = ($("#chkEstado").attr("checked") == "checked") ? true : false;

    BdJson[indiceACtual].esAsistente = ($("#chkAsistente").attr("checked") == "checked") ? true : false;

    $.ajax({
        async:false,
        url: "frmCrearPersonas.aspx/F_insertarPersona",
        cache: false,
        type: "post",
        data: JSON.stringify({
            dcPersona: BdJson[indiceACtual]
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(res, textStatus, jqXHR) {


        $.unblockUI();

        if (res.d.codigo > 0) {
                Sexy.info(res.d.mensaje)
            }
            else  {
                Sexy.alert(res.d.mensaje);
            }

            if (res.d.codigo > 0)
           
           {
               $("#edicionPersona").dialog("close");
           }
         


        }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
    });

}

/*resetar pass */


function F_ResetearPass() {

if(! confirm("Seguro que desea cambiar el password"))
{
    return false;
    
}
   /* BdJson[indiceACtual].apellidoPAterno = $("#txtApPaternoEdicion").val();
    BdJson[indiceACtual].apellidoMAterno = $("#txtApMaterno").val();
    BdJson[indiceACtual].nombre = $("#txtNombreEdicion").val();
    BdJson[indiceACtual].codPerfil = parseInt($("#cmbPerfilEdicion option:selected").val());
    BdJson[indiceACtual].correoCorporativo = $("#txtCorrreoCorp").val();
    BdJson[indiceACtual].correoSkype = $("#txtCorreoSkype").val();


    BdJson[indiceACtual].codEnsenia = ($("#chkEnsenia").attr("checked") == "checked") ? true : false;
    BdJson[indiceACtual].codAcceso = ($("#chkEstado").attr("checked") == "checked") ? true : false;*/

    $.ajax({
    async: false,
    url: "frmCrearPersonas.aspx/F_ResetPassword",
        cache: false,
        type: "post",
        data: JSON.stringify({
            dcPersona: BdJson[indiceACtual]
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(res, textStatus, jqXHR) {

////            if (res.d.codOperacion > 0) {
////                Sexy.info(res.d.mensaje)
////            }
////            else {
////                Sexy.alert(res.d.mensaje);
////            }
////            $.unblockUI();
        if (res.d.codigo > 0) 
        {
            Sexy.info(res.d.mensaje)
        }
        else {
            Sexy.alert(res.d.mensaje);
        }
        $.unblockUI();


        if (res.d.codigo > 0) {
            $("#edicionPersona").dialog("close");
            listarpersonasPerfil();
        }
         

        }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
    });

}


/**/
function F_nuevoPersona() {
    var oPersona = new Object();
    oPersona.apellidoPAterno = $("#txtApPaternoEdicion").val();
    oPersona.apellidoMAterno = $("#txtApMaterno").val();

    oPersona.nombre = $("#txtNombreEdicion").val();
    oPersona.codPerfil = parseInt($("#cmbPerfilEdicion option:selected").val());
    oPersona.correoCorporativo = $("#txtCorrreoCorp").val();
    oPersona.correoSkype = $("#txtCorreoSkype").val();

    oPersona.codEnsenia = ($("#chkEnsenia").attr("checked") == "checked") ? true : false;
    oPersona.codAcceso = ($("#chkEstado").attr("checked") == "checked") ? true : false;

    oPersona.esAsistente = ($("#chkAsistente").attr("checked") == "checked") ? true : false;
    
    
    
    

    oPersona.codPersona = 0
    oPersona.codTrab = 0
    oPersona.codRelacionPerfil = 0
    oPersona.TJ_CodigoTrabajadoreAsistencia = parseInt($("#txtCodigoTrabajador").val());


    $.ajax({
        async: false,
        url: "frmCrearPersonas.aspx/F_insertarPersona",
        cache: false,
        type: "post",
        data: JSON.stringify({
        dcPersona: oPersona
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(res, textStatus, jqXHR) {

        if (res.d.codigo > 0) {
                Sexy.info(res.d.mensaje)
            }
            else {
                Sexy.alert(res.d.mensaje);
            }
            $.unblockUI();
            
            if (res.d.codigo > 0) {
                $("#edicionPersona").dialog("close");
            }
         

        }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
    });

}



var codEdicionPerfilActual = 0;


function abrirEdicionPerfil() 
{
    codEdicionPerfilActual = parseInt($("#cmbPerfilEdicion option:selected").val())
    var codTipoPerfil = parseInt($("#cmbPerfilEdicion option:selected").attr("codTipoPerfil"))
        $("#txtPefil").val("");
    //    codTipoPerfil
        $("#edicionPerfil").dialog("open");
        if(codEdicionPerfilActual!=0) {
            $("#txtPefil").val($("#cmbPerfilEdicion option:selected").text())
        }
        $("#cmbTipoPerfil option[value=" + codTipoPerfil + "]").attr("selected", true)
    }


function F_crearPerfil() {

var codTipoPerfil=parseInt($("#cmbTipoPerfil option:selected").val())
var nombrePer=$("#txtPefil").val();
var oPerfil = { codRegPerfil: codEdicionPerfilActual, codTipoPerfil: codTipoPerfil, nombrePerfil: nombrePer };

$.ajax({
async: false,
    url: "frmCrearPersonas.aspx/F_insertarPefil",
    cache: false,
    type: "post",
    data: JSON.stringify({
        dcPerfil: oPerfil
    }),
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    success: function(res, textStatus, jqXHR) {

        if (res.d.codOperacion > 0) {
            Sexy.info(res.d.mensaje)
        }
        else {
            Sexy.alert(res.d.mensaje);
        }
        $.unblockUI();
        F_actualizarPerfil(res.d.codOperacion)

    }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
});

}


var edicionNombre = "";
var edicionApPaterno = "";
var edicionApMaterno = "";
var codTrabajadorEdicion = 0;


function F_actualizarPerfil(index) {

//    $("#EdicionUsuarioPersona").dialog("open");
//    $("#txtApPaternoCambiarPass").val(BdJson[index].apellidoPAterno)
//    $("#txtApMaternoCambiarPass").val(BdJson[index].apellidoMAterno)
//    $("#txtNombreCambiarPAss").val(BdJson[index].nombre)
//    
//    edicionNombre = BdJson[index].nombre;
//    edicionApPaterno = BdJson[index].apellidoPAterno;
//    edicionApMaterno = BdJson[index].apellidoMAterno;
//    codTrabajadorEdicion= BdJson[index].codTrab
//    txtNombreCambiarPAss
//    txtApPaternoCambiarPass
//    txtApMaternoCambiarPass

$.ajax({
url: "frmCrearPersonas.aspx/F_listarPefil",
async: false,
    cache: false,
    type: "post",
    data: JSON.stringify({ codPerfil: index }),
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    success: function(res, textStatus, jqXHR) {

    $("#cmbPerfilEdicion").html(res.d.html)
    $("#edicionPerfil").dialog("close");

            }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
  });


}
function F_cancelar() {
    $("#edicionPersona").dialog("close");
}
/**/

function EdicionPerfil(index) {
    $("#EdicionUsuarioPersona").dialog("open");
    $("#txtApPaternoCambiarPass").val(BdJson[index].apellidoPAterno)
    $("#txtApMaternoCambiarPass").val(BdJson[index].apellidoMAterno)
    $("#txtNombreCambiarPAss").val(BdJson[index].nombre)
    //
    
    edicionNombre = BdJson[index].nombre;
    edicionApPaterno = BdJson[index].apellidoPAterno;
    edicionApMaterno = BdJson[index].apellidoMAterno;
    codTrabajadorEdicion= BdJson[index].codTrab
}




/**/
function generarUsuarioPass() {

    $.ajax({
    async: false,
url: "frmCrearPersonas.aspx/F_listarCoincidencia",
    cache: false,
    type: "post",
    data: JSON.stringify({
        nombre:edicionNombre  , 
        apPaterno  :edicionApPaterno,
        apMaterno: edicionApMaterno ,
        codTrab: codTrabajadorEdicion
    }),
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    success: function(res, textStatus, jqXHR) {
                     
                     }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
  });



}

function borrar() {
$("#txtApellidoPaterno").val("");
$("#txtApellidoMaterno").val("");
$("#txtNombre").val("");
$("#cmbPerfil option[value=0]").attr("selected",true )
$("#cmbEstados option[value=-1]").attr("selected", true) 

//    cmbPerfil, cmbEstados
}

function F_cancelarPerfil() {
    $("#edicionPerfil").dialog("close");

}






</script>
 <link href="../App_Themes/Estilos/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
   
</asp:Content>

