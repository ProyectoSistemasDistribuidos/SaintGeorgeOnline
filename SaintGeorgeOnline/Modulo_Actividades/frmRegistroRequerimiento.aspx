<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="frmRegistroRequerimiento.aspx.vb" Inherits="Modulo_Actividades_frmRegistroRequerimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



    <style type="text/css">
        

.miboton{
    text-decoration: none; 
    background-color: #4b8efa; 
    color: #ffffff; 
    border: 0;
    font-size: 12px;  
    display: block;
     line-height: 20px; float: left;
    padding: 0 10px 0 10px;
    margin: 0 5px 0 0;
    /*width: 80px;*/
    vertical-align: middle; text-align: center;   
    border: solid 1px #3079ed;  
    cursor: pointer; 
    }
.miboton:hover{
    text-decoration: none; 
    background-color: #0072bb;
    color: #ffffff; 
    border: 0; 
    /*font-size: 12px;*/  
    display: block; line-height: 20px; float: left; 
    padding: 0 10px 0 10px;
    margin: 0 5px 0 0;
    /*width: 80px;*/ 
    vertical-align: middle; text-align: center;   
    border: solid 1px #2f5bb7; 
    cursor: pointer; 
    } 
    
.filas
{ 
	height:25px;
    width:900px;
    margin-top:5px;
}
    .nombreEtiqueta
    {
    height:25px;
    width:120px;
    float:left;
    padding-left:5px;
    text-align:left;
    line-height:25px;
    color: #0000CC;
    font-family:@GulimChe;
    font-size:9pt;
    }
    .filasGrande
    { 
	    height:120px;
        width:750px;
    }
    .nombreEtiquetaGrande
    {
    height:120px;
  width:120px;
    float:left;
    padding-left:5px;
    text-align:left;
    line-height:75px;
    color: #0000CC;
    font-family:@GulimChe;
    font-size:10pt;
    	
    }
    
     .celdaDerechaGrande
    {
    	
     height:120px;
     width:590px; 
     float:left;
     line-height:120px;
    }
    .contenedor
    {
     height:auto;
     width:900px;
     overflow:hidden	;
     margin-top:25px;
    }
    .celdaDerecha
    {
     height:25px;
     width:250px; 
     float:left;
     line-height:25px;
    }
    .celdaFecha
    {
      float:left;
      width:180px;
      height:25px;
      line-height:25px;
    	
	}
	
	    .celdaFechaGrande
    {
      float:left;
      width:250px;
      height:25px;
      line-height:25px;
    	
	}
	.celdaHora
    {
      float:left;
      width:auto;
      height:25px;
      margin-left:10px;
      line-height:25px
    }
	
    .milegend
    {
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
    margin-left:85px;
    margin-bottom:10px
    }      
        .agrupador
    {
	 margin-left:15px;
	  height: auto;
	   margin-bottom:10px;
	    
	  
	} 
	.cajasSmallLeft
	{
        float:left;
        width:65px;
        height:25px;
        line-height:25px;
    } 
    .cajasSmallRight
	{
	 float:left;
	 width:65px;
	 margin-left:15px;
	 height:25px;
    line-height:25px;
    color: #0000CC;
    font-family:@GulimChe;
    font-size:9pt;
    } 
    
   
   .filasCheck
    {
    height:auto;
    width:700px;
    line-height:25px;
    margin-top:5px;
    overflow :hidden;
    
    }
    .labelCheck
    {
    float:left;
    width:95px;
    height:85px;
    color: #0000CC;
    font-family:@GulimChe;
    font-size:9pt;
    padding-left:5px;
    }
    
    .celdaChecks
    {
    width: auto;
    height:auto;
    float:left;
    text-align:left;
    padding-left:5px;
    overflow :hidden;

    }
    
    .botonGrabar
    {
     height:25px;
     float:right;
     width:180px;	
     text-align:center;
     
    }
    
    #Text1
    {
        width: 83px;
    }
    #txtNombreActividad
    {
        width: 244px;
    }
    #txtLugar
    {
        width: 246px;
    }
    #cmbOrganizador
    {
        width: 245px;
    }
    #TextArea1
    {
        height: 62px;
        width: 244px;
    }
    #Text2
    {
        width: 60px;
    }
    #Text3
    {
        width: 60px;
    }
    #Text4
    {
        width: 60px;
    }
    #txtNumeroPadres
    {
        width: 59px;
    }
    #txtNumeroAlumnos
    {
        width: 57px;
    }
    #txtNumeroAsistentes
    {
        width: 58px;
    }
    #txtNumeroDocentes
    {
        width: 56px;
    }
    #txtObjetivo
    {
        height: 110px; /* 61px; */
        width: 590px; /* 246px; */ 
    }
   
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


.contenedorBusqueda
{
 
   width:auto;
   height:auto;
   overflow :hidden;
}      
.filaBusqueda
{
	
	 height:25px;
	 width:800px;
	 line-height:25px;
	 margin: 5px 0 8px 0;
	 padding:0;
	
} 

.grilla
    {
    height:auto;
    width:851px;
    /*overflow:hidden;*/ 
    border: solid 1px #a6a3a3;
    /*
    border-left: solid 1px #a6a3a3;
    border-bottom: solid 1px #a6a3a3;
    border-right: solid 1px #a6a3a3;
    */
    }
.etiquetaFilaBusqueda
{
        width:75px;
        height:25px;	
        float:left;
        line-height:25px;
        color: #0000CC;
        font-family:@GulimChe;
        font-size:9pt;
        padding-right:5px;
        text-align :right ;
         
	}
	.etiquetaFilaBusquedaCentrado
{
        width:100px;
        height:25px;	
        float:left;
        line-height:25px;
        color: #0000CC;
        font-family:@GulimChe;
        font-size:9pt;
        padding-left:5px;
        text-align:left;
	}
    .ControlFilaBusqueda
        {
                width:125px;
                height:25px;	
                float:left;
                line-height:25px;
        }
        
        .filasOpcionesAnioMes 
        {
         height:auto;	
         width:800px;
         margin-top:5px;
        	
         }
          .filasOpcionesFechas
        {
         height:auto;	
         width:800px;
         margin-top:5px;
        	
         }
	
    #Text2
    {
        width: 77px;
    }
    	
    #Text3
    {
        width: 33px;
    }
    #Text4
    {
        width: 33px;
    }
        	
        #txtHoraInicio
        {
            width: 83px;
        }
        #txtHoraFin
        {
            width: 81px;
        }
        	
        #tabs
        {
            width: 888px;
        }
        	
    #Text2
    {
        width: 83px;
    }
        
    #Text3
    {
        width: 83px;
    }
            	
    </style>
    
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <link href="../App_Themes/Estilos/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
<script>

   
   
      
      
     </script>
<div>

 


<%--prueba --%>

<%--fin --%>

<div id="tabs">
    <ul>
        <li><a href="#fragment-2"><span >Busqueda</span></a></li>
        <li><a href="#fragment-3"><span>Registro Requerimiento</span></a></li>

    </ul>
     <div id="fragment-2" style="height: auto; overflow :hidden ">
<fieldset class="agrupador">

<legend class="milegend">

   Busqueda de actividades </legend>
   
   <div class="filas">
<div class="nombreEtiqueta">Year:</div>
<div class="celdaFecha">
 
    <select id="cmbYear" name="D1">
     
         <option value="0">---------Seleccione---------</option>
        <%For Each anio In lstYear%>
          <option value="<%=anio.Key %>"><%=anio.Value%></option>
        <%Next%>
        
        
    </select>
    
    </div>


<div class="nombreEtiqueta">Month:</div>
<div class="celdaFecha">
 
    <select id="cmbMeses" name="D2">
        <option value="0">---------Seleccione---------</option>
        <%For Each ms In lstMeses%>
        
          <option value="<%=ms.Key %>"><%=ms.Value%></option>
        <%Next%>
    </select>
    
    </div>


<div class="botonGrabar">

    <input id="btnGrabar" onclick="F_listarActividad()" type="button"  value="Buscar" class="miboton" />
    
    </div>

</div>
   <div class="filas">
<div class="nombreEtiqueta">Estado:</div>
<div class="celdaFecha">
 
    <select id="cmbEstado" name="D3">
           <option value="0">---------Seleccione---------</option>
        <%For Each estado As Data.DataRow In dtEstado.Rows%>
          <option value="<%=estado("codigo") %>"><%=estado("Descripcion")%></option>
        <%Next%>
    </select></div>



</div>
    
    
    
    <%--inicio--%>
    
    <div style="border: solid 1px #a6a3a3; margin: 0; padding:0; height:25px;width:850px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;margin-top:5px">

<div style="height:25px;width:245px; float:left; line-height:25px; text-align:center">Activity</div>
<div style="height:25px;width: 70px; float:left; line-height:25px; text-align:center">Date</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">
    AJ</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">
    AD</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">
    RA</div>
    
    <div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">
    Edit</div>
    <div style="height:25px;width: 70px; float:left; line-height:25px; text-align:center">Print</div>
    
    
</div>
    
    <div id="grillaBusqueda" style="border-bottom:solid 1px #a6a3a3; margin:0; padding:0; height:auto; width:850px;">
</div>
    <%--fin--%>
    
    </fieldset>
     </div>
<div id="fragment-3" style="height: auto; overflow :hidden ">

<div class="filas">
<div class="nombreEtiqueta">Actividad:</div>
<div class="celdaFecha" id="nombreActividad">
 
     </div>

<div class="botonGrabar">

    <input id="" onclick="factualizarActividad()" type="button"  value="Guardar" class="miboton" />
    
    </div>
</div>

<div class="filasGrande">
<div class="nombreEtiquetaGrande">

    Objetivo:</div>
<div class="celdaDerechaGrande" id="celdaObjetivo">

     &nbsp;</div>
</div>

<div class="filas">
<div class="nombreEtiqueta">Grados:</div>
<div class="celdaFechaGrande" id="infoGrados">
 
     </div>


</div>

<div class="filas">
<div class="nombreEtiqueta">Req.Logisticos:</div>
<div class="celdaFechaGrande">
 
     <input style="  width:244px"  id="txtReqLog" type="text" />
     </div>


</div>
<div class="filas">
<div class="nombreEtiqueta">Req. Infraestrucura:</div>
<div class="celdaFechaGrande">
  
     <input  style="  width:244px" id="txtReqInf" type="text" />
     </div>


</div>
<div class="filas">
<div class="nombreEtiqueta">Req. Sistemas</div>
<div class="celdaFechaGrande">
 
     <input  style="  width:244px" id="txtReqSist" type="text" /></div>


</div>

<div class="filasGrande">
<div class="nombreEtiquetaGrande">

    Comentario:</div>
<div class="celdaDerechaGrande">

     <textarea style=" height: 110px;width: 590px; /* 246px; */" id="txtComentario" name="S1"></textarea> 
    
  
    
    </div>
</div>
<div class="filas"></div>

</div>
</div>

</div>

  <script>
       /***********  variables globales *********************************/
      /*************************************************************/
      var codActividadEdicion = 0;
      /*******************************************************************/
      
      $(document).ready(function() {

      $("#tabs").tabs({ collapsible: true }); ;
      $("#tabs").tabs("disable", 1);
      
   
      });
      
      
      function F_listarActividad() {


          $.ajax({
              url: "frmRegistroRequerimiento.aspx/F_listarActividadesValidacion",
              async: false,
              cache: false,
              type: "post",
              data: JSON.stringify({
                  /*
                  ByVal anio As Integer, ByVal mes As Integer, ByVal estado As Integer
                  */
                  anio: $("#cmbYear option:selected").val(),
                  mes: $("#cmbMeses option:selected").val(),
                  estado: $("#cmbEstado option:selected").val()
              }),
              dataType: "json",
              contentType: "application/json; charset=utf-8",
              success: function(res, textStatus, jqXHR) {

                  /*  if (res.d.codigo > 0) {
                  Sexy.info('<br>' + res.d.mensaje)


                        
                  }
                  else {
                  Sexy.alert('<br>' + res.d.mensaje)
                  $.unblockUI();
                  }
                  */

                  $("#grillaBusqueda").html(res.d.html)

              }

      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
          });
      }
      //-----------------------------------------------------------------------------------------------//

      /*******************************************************************************************************************/
      /**************************************efecto de hover , out de la tabla de resultados de la grilla de busquedas *****/

      function TiposControlesActualOver(control) {
          $(control).css("backgroundColor", "#DEE8F5")
      }
      function TiposControlesActualOut(control) {
          $(control).css("backgroundColor", "")
      }

      /******************************************************/
      /******************************************************/
      function factualizarActividad() {

          /*
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
Public Shared Function F_actualizarActividad(ByVal dcActividad As Dictionary(Of String, Object)) As Object
          */
          
     /*   $("#tabs").tabs("enable", 0);
        $("#tabs").tabs("select", 0);
        $("#tabs").tabs("disable", 1);
        */  

         var oActividad ={
            codACtividad:codActividadEdicion,
            requerimientoLog: $("#txtReqLog").val(),
            requerimientoInf: $("#txtReqInf").val(),
            requerimientoSis: $("#txtReqSist").val(),
            comentario: $("#txtComentario").val()
          }  
          
 

          $.ajax({
          url: "frmRegistroRequerimiento.aspx/F_actualizarActividad",
              async: false,
              cache: false,
              type: "post",
              data: JSON.stringify({
                  dcActividad :oActividad
                
              }),
              dataType: "json",
              contentType: "application/json; charset=utf-8",
              success: function(res, textStatus, jqXHR) {
              $("#tabs").tabs("enable", 0);
              $("#tabs").tabs("select", 0);
              $("#tabs").tabs("disable", 1);  

              }

      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
          });


      }


       /*********************Funcion para mostrar el tab de edicion***********************************/
      /*********************************************************************************************/

      function fMostrarEdicion(PcodActividad) {
          codActividadEdicion = PcodActividad;
          $("#tabs").tabs("enable", 1);
          $("#tabs").tabs("select", 1);
          $("#tabs").tabs("disable", 0);

          F_listarActivdad(PcodActividad)


      }
      
      
      /******************************************* funcion para listar el detalle de la actividad*/
      /*****************************************************************************************/

      function F_listarActivdad(pcodActividad ) {
          /////////////////////////////////////////////////////////
          $.ajax({
          url: "frmRegistroRequerimiento.aspx/F_listarACtividad",
              async: false,
              cache: false,
              type: "post",
              data: JSON.stringify({
              codActividad: pcodActividad
              }),
              dataType: "json",
              contentType: "application/json; charset=utf-8",
              success: function(res, textStatus, jqXHR) {


FCargarInformacion(res.d)

              }

      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
          });
      ////////////////////////////////////////////////////////////

      }

      /********************************************************************/
      /*******************funcion parap cargar informacion adicional de la actividad*****/
      function FCargarInformacion(objtActividad) {
          $("#infoGrados").text(objtActividad.detalle )
           $("#celdaObjetivo").text(objtActividad.objetivo )
           $("#nombreActividad").text(objtActividad.nombreActividad)
      $("#txtReqLog").val(objtActividad.ReqLog),
          $("#txtReqInf").val(objtActividad.ReqInf),
             $("#txtReqSist").val(objtActividad.ReqTec),
            $("#txtComentario").val(objtActividad.comentarios)

     
     // $("#tabs").tabs("disable", 1);
      }
    </script>
</asp:Content>

