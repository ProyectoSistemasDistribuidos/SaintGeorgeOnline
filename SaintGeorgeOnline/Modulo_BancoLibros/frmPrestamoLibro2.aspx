<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="frmPrestamoLibro2.aspx.vb" Inherits="Modulo_BancoLibros_frmPrestamoLibroNew" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   <style>
       
    .miboton
    {
        text-decoration: none; 
        background-color: #4b8efa; 
        color: #ffffff; 
        border: 0;
        font-size: 12px;  
        display: block; line-height: 20px; float: left;
        padding: 0 5px; 
        margin: 0 5px 0 0;
        width: 80px;
        vertical-align: middle; text-align: center;   
        border: solid 1px #3079ed;   
    }
    .miboton:hover
    {
        text-decoration: none; 
        background-color: #0072bb;
        color: #ffffff; 
        border: 0;font-size: 12px; 
        display: block; line-height: 20px; float: left; 
        padding: 0 5px; 
        margin: 0 5px 0 0;
        width: 80px;
        vertical-align: middle; text-align: center;   
        border: solid 1px #2f5bb7; 
    } 
       
       
.filas
{ 
	height:25px;
    width:700px;
    margin-top:5px;
    line-height:25px;
}
    .nombreEtiqueta
    {
    height:25px;
    width:97px;
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
	    height:75px;
        width:700px;
    }
    .nombreEtiquetaGrande
    {
    height:75px;
    width:92px;
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
    	
     height:75px;
     width:250px; 
     float:left;
     line-height:75px;
    }
    .contenedor
    {
     height:968px;
     width:890px;
     overflow:hidden;
     margin-top:25px;
    }
     .celdaDerecha
      {
            height:25px;
            float:left;
            line-height:25px;
            width: 280px;
            font-size:8pt;
            font-family:@Arial Unicode MS;
       }
       .celdaDerechaMedio
      {
            height:25px;
            float:left;
            line-height:25px;
            width: 140px;
            font-size:8pt;
            font-family:@Arial Unicode MS;
       }
    .celdaFecha
    {
      float:left;
      width:150px;
      height:25px;
      line-height:25px;
    	
	}
	.celdaHora
    {
      float:left;
      width:100px;
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
        height:  auto;
        overflow :hidden;
        width: auto;
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
         height:45px;
         float:right;
         width:180px;	
         text-align:center;
         line-height:45px;
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
        height: 61px;
        width: 246px;
    }
    #Button1
    {
        height: 26px;
        width: 68px;
    }
    
    .milegend{
        margin: 0px 20px 20px 20px;  
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
	 margin-top:5px;
	 line-height:25px;
	 margin-bottom:8px;
	
} 

.grilla
    {
 height :auto;
 width:800px;
  overflow:hidden;
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
        	
        	
        	
        	.combo
        	{
        		 width:280px;
        		 height:25px;
        		 font-size:8pt;
        		 font-style: italic;
        		 
        		
        	}
        	
        .texto
        {
        	 width:125px;
        	 height:23px;
        	 font-size:8pt;
        	 
        	}
        	.botonesDerecha
        	{
        	 height:25px;
        	 width:125px;
        	 float:right;
        	 margin-right:20px;	
        	 text-align:right;
        	 line-height:25px;
            }	
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../App_Themes/Estilos/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
<div class="contenedor">
<div id="tabs">
    <ul>
    <li><a href="#fragment-2"><span >Busqueda</span></a></li>
        <li><a href="#fragment-1"><span>Registro</span></a></li>
        
    </ul>
     <div id="fragment-1" style="height: auto; overflow :hidden; width:auto">
     <fieldset class="agrupador">

<legend class="milegend">Datos del registro</legend>
    
<div class="filas">
<div class="nombreEtiqueta">Periodo:</div>
<div class="celdaDerecha">
    <select id="cmbPeriodo" name="D1"  onchange="F_cagarAulasXGrado()"  class="combo">
        <option value="0">-----------------Seleccione----------------------</option>
        <%For Each flAnioAcademico As Data.DataRow In dtAnio.Rows%>
        <%If Date.Now.Year = flAnioAcademico("Descripcion") Then%>
        <option selected=selected value="<%=flAnioAcademico("Codigo") %>"><%=flAnioAcademico("Descripcion")%></option>
        <%Else%>
        <option value="<%=flAnioAcademico("Codigo") %>"><%=flAnioAcademico("Descripcion")%></option>
        <%End If%>
        <%Next%>
    </select>
</div>
</div>

<div class="filas">
<div class="nombreEtiqueta">Grado:</div>
<div class="celdaDerecha">
    <select onchange="F_cagarAulasXGrado()" id="cmbGrado" name="D2"   class="combo">
        <option selected="selected" value="0">---------------Seleccione------------------</option>
        <%For Each rwGrado As Data.DataRow In dtGrado.Rows%>
        <option value="<%=rwGrado("Codigo") %>"><%=rwGrado("Descripcion")%></option>
        <%Next%>
    </select>
</div>
<div class="botonesDerecha">
    <a  class="miboton" style="color: #ffffff;"  onclick="F_cambiarPanelCancelar()" href="#">Cancelar</a>                 
</div>
</div>

<div class="filas">
<div class="nombreEtiqueta">Aula:</div>
<div class="celdaDerecha">
    <select onchange="F_AlumnosXaula()" id="cmbAula" name="D3"   class="combo">
       <option selected="selected"  value="0">-----------------Todos----------------------</option>
    </select>
</div>
    <div class="botonesDerecha">
        <a class="miboton" style="color: #ffffff;"  onclick="f_limpiarInput()" href="#">Buscar</a>   
    </div>
</div>

<%--<div class="filas">
<div class="nombreEtiqueta">

    Alumno:</div>
<div class="celdaDerecha">


    
    <select id="cmbAlumno" name="D4"   class="combo">
       <option selected="selected" value="0">-----------------Todos----------------------</option>
    </select></div>
    
</div>--%>


<div style="height:25px; width:800px; margin-top:10px; " class="miGridviewBusquedaActualizacion_Ficha_Header">
 <%--<div style="height:25px; width:750px; font-size:10pt; ">--%>


 <div style="height:25px; width:250px; font-size:10pt;  float:left; text-align:center; line-height:25px;">
 Nombre Alumno 
 </div>
 <div style="height:25px; width:75px; ; font-size:10pt;  float:left; text-align:left; line-height:25px;">
Nro. libros 
 </div>
 <div style="height:25px; width:45px; ; font-size:10pt;  float:left; text-align:left; line-height:25px;">
Editar
 </div>
</div>
<div style="width:800px;overflow-y : scroll;overflow-x:hidden  ; height:200px; ">


<div style="width:800px; height:200px; ; " id="listaLibros">

</div>
</div>
 
    </fieldset>
     
     
     
     </div>
      <div id="fragment-2" style="height: auto; overflow :hidden; width: auto;">
      
      <fieldset class="agrupador">
      <legend class=" milegend">
      Consultar
      </legend>
      <div class="filas">
<div class="nombreEtiqueta">

          Periodo:</div>
    <div class="celdaDerecha">
    <select id="cmbPeriodoBusqueda" name="D1"  onchange="F_cagarAulasXGradoBusqueda()"  class="combo">
        <option selected=selected value="0">-----------------Todos----------------------</option>
        <%For Each flAnioAcademico As Data.DataRow In dtAnio.Rows%>
        
        <%If Date.Now.Year = flAnioAcademico("Descripcion") Then%>
        <option selected=selected value="<%=flAnioAcademico("Codigo") %>"><%=flAnioAcademico("Descripcion")%></option>
       <%Else%>
        <option value="<%=flAnioAcademico("Codigo") %>"><%=flAnioAcademico("Descripcion")%></option>
        <%End If%>
         
      
      
        <%Next%>
    </select>
    </div>
      </div>
            <div class="filas">
<div class="nombreEtiqueta">

                Grado:</div>
                <div class="celdaDerecha">
                <select onchange="F_cagarAulasXGradoBusqueda()" id="cmbGradoBusqueda" name="D2"   class="combo">
      <option selected="selected" value="0">-----------------Todos----------------------</option>
        <%For Each rwGrado As Data.DataRow In dtGrado.Rows%>
          <option value="<%=rwGrado("Codigo") %>"><%=rwGrado("Descripcion")%></option>
        <%Next%>
    </select>
    </div>
    <div class="botonesDerecha">
        <a class="miboton" style="color: #ffffff;"  onclick="F_cambiarPanel()" href="#">Nuevo</a>   
    
    </div>
      </div>
    
    
      <div class="filas">
<div class="nombreEtiqueta">

                Aula:</div>
                <div class="celdaDerecha">
                <select onchange="F_AlumnosXaulaBusqueda()" id="cmbAulaBusqueda" name="D3"   class="combo">
       <option selected="selected"  value="0">-----------------Todos----------------------</option>
    </select>
    </div>
    <div class="botonesDerecha">
    
    
        <a  class="miboton" style="color: #ffffff;"  onclick="f_limpiarInputBusqueda()" href="#">Limpiar</a>   
            
            
            
    </div>
      </div>
      
      <div class="filas">
<div class="nombreEtiqueta">

                Alumno:</div>
                <div class="celdaDerecha">
                <select id="cmbAlumnoBusqueda" name="D4"   class="combo">
       <option selected="selected" value="0">-----------------Todos----------------------</option>
    </select>
    </div>
    <div class="botonesDerecha">
    
    
    <a class="miboton" style="color: #ffffff;"   onclick="F_BuscarPrestamos()" href="#">Buscar</a>   
            
            
    </div>
      </div>
      <div class="filas">
<div class="nombreEtiqueta">

                Codigo de barra:</div>
                <div class="celdaDerecha">
                &nbsp;<input id="txtCodigoBarrasBusqueda"  type="text" class="texto" /></div>
      </div>
      
      <div class="miGridviewBusquedaActualizacion_Ficha_Header" style=" height:25px; width:800px; margin-top:10px;; font-size:10pt;  float:left; text-align:center; line-height:25px;  ">
      <div style=" height: 25px; width:200px;; float:left;   ">
      Nombre Persona 
      </div>
      <div style=" height: 25px; width:75px;; float:left ">
      Cod.&nbsp; Barra  
      </div>
      
       <div style=" height: 25px; width:300px;; float:left ">
     Titulo
      </div>
       <div style=" height: 25px; width:100px;; float:left ">
    Fecha
      </div>
      <div style=" height: 25px; width:85px;; float:left ">
    Estado
      </div>
      
      </div>
      <div style="overflow-y : scroll;overflow-x:hidden; height:213px;  width:824px;; margin-top:10px;  ">
      <div  id="pnlResultado" style="height:auto; overflow:hidden; width:800px;; margin-top:10px;  ">
      
      </div>
      </div>
      
      </fieldset>
     </div>
     </div>


</div>
<%--modales para edicion de presatamos de libros --%>

<div style="; height:350px;width:660px;" id="pnlPrestamos">
<fieldset>
<legend class="milegend">
Libros asignados
</legend>

<div  style="; height:25px;width:650px; margin-top:10px;">
<div class="nombreEtiqueta">Grado:</div>
<div id="lblgrado" class="celdaDerechaMedio">
</div>
<div class="nombreEtiqueta"> Anio :</div>
<div id="lblAnio" class="celdaDerechaMedio"></div>
</div>
<div style="; height:25px;width:650px; margin-top:10px;">
<div class="nombreEtiqueta">Nombre alumno :</div>
<div id="lblNombreAlumno" class="celdaDerecha">
</div>
</div>

<div  style="; height:25px;width:650px;">
<div class="nombreEtiqueta">Fecha Prestamo :</div>
<div class="celdaDerecha">
                <table>
<tr>
                        <td valign="middle" align="left" style="width: 90px; height: 25px;">
                            <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="miTextBoxCalendar" Width="70px" 
                                style="font-size: 8pt; font-family: Arial; text-align: left" ></asp:TextBox>
                            <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                TargetControlID="txtFechaInicio"
                                UserDateFormat="DayMonthYear"                                                                    
                                Mask="99/99/9999" 
                                MaskType="Date" 
                                PromptCharacter="-">
                            </atk:MaskedEditExtender>                                                                                                                          
                        </td>
                        <td valign="middle" align="left" style="width: 30px; height: 25px;">
                            <asp:ImageButton runat="server" ID="Image2" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                            <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                TargetControlID="txtFechaInicio"
                                PopupButtonID="Image2" 
                                Format="dd/MM/yyyy" 
                                CssClass="MyCalendar" Enabled="True" />       
                        </td>
                    </tr>
					
					</table>
</div>
               
<%--    <input id="Button1" type="button" onclick="" value="button" />
--%>    
    
</div>
<div  style="; height:25px;width:650px; margin-top:10px;">
<div class="nombreEtiqueta">Codigo de barra :</div>
<div class=celdaDerecha>
    <input  maxlength="20"  class="texto"  id="txtCodigoBarra" type="text" />
</div>
<div class="botonesDerecha">
    <a class="miboton" style="color: #ffffff;"   onclick="F_insertarBD()" href="#">Guardar</a>
</div>
</div>

 <div class="miGridviewBusquedaActualizacion_Ficha_Header" style="height:25px;width:650px; font-size:10pt; margin-top:10px; ">
     <div style="height:25px;width:85px; float:left; line-height:25px; ">
     Cod. Barra
     </div>
     <div style="height:25px;width:200px; ; float:left; line-height:25px;">
     Titulo
     </div>
     <div style="height:25px;width:85px; ; float:left; line-height:25px;">
     Tipo
     </div>
      <div style="height:25px;width:85px;; float:left; line-height:25px; ">
     Fecha Prestamo
     </div>
      <div style="height:25px;width:85px;; float:left; line-height:25px; ">
     Eliminar
     </div>
 </div>
 <div style="height:200px;width:650px; font-size:10pt; overflow-y : scroll;overflow-x:hidden  " >
 <div id="pnlDetallePrestamo" style="height:auto;width:650px;">
 
 </div>
 </div>
 
 </fieldset>
</div>



<script type="text/javascript" src="../App_Themes/Javascript/jquery.textchange.min.js" ></script>


<script src="../App_Themes/Javascript/calendar/jquery.calendars.js" type="text/javascript"></script>
<script src="../App_Themes/Javascript/calendar/jquery.calendars.plus.js" type="text/javascript"></script>
<link href="../App_Themes/Javascript/calendar/jquery.calendars.picker.css" rel="stylesheet" type="text/css" />
<script src="../App_Themes/Javascript/calendar/jquery.calendars.picker.js" type="text/javascript"></script>


    
<script>
    /****************************************************************************************/
    /****** variables globales **************************************************************/
    var listaPrestamos = [];
    

    /****************************************************************************************/
    
    
    

    /****************************************************************************************/
   /****** inicializar UI Tabs**************************************************************/
    $(document).ready(function() {


        $("#aspnetForm").submit(function() {
            return false;
        });


        /*******************************************/
        /* $('#pnlResultado').validate({
        errorPlacement: $.calendars.picker.errorPlacement,
        rules: {
        validFormatPicker: {
        required: true,
        cpDate: true
        },
        validBeforePicker: {
        cpCompareDate: ['before', '#validAfterPicker']
        },
        validAfterPicker: {
        cpCompareDate: { after: '#validBeforePicker' }
        },
        validTodayPicker: {
        cpCompareDate: 'ne today'
        },
        validSpecificPicker: {
        cpCompareDate: 'notBefore 01/01/2012'
        }
        },
        messages: {
        validFormatPicker: 'Please enter a valid date (yyyy-mm-dd)',
        validRangePicker: 'Please enter a valid date range',
        validMultiPicker: 'Please enter at most three valid dates',
        validAfterPicker: 'Please enter a date after the previous value'
        }
        });*/
        /******************************************************/


        $("#tabs").tabs({ collapsible: true });

        $("#tabs").tabs("disable", 1);


        $('#txtCodigoBarra').bind('textchange', function(event, previousText) {
            //console.log($(this).val());

            //consultarPrestamo();
            if ($.trim($('#txtCodigoBarra').val()).length >= 11) {
                F_AgregarLibro()
            }

        });



    });

    /****************************************************************************************/
   /****** inicializar UI Tabs**************************************************************/
  /****************************************************************************************/
 /* eventos cargar combos */
/*******************cargar aulas por grado ******************************/
    function F_cagarAulasXGrado() {

        $("#listaLibros").html("");
        var codGrado = 0
        var codAnio = 0

        codGrado = parseInt($("#cmbGrado option:selected").val())
        if (isNaN(codGrado))
        {
            codGrado = 0;
            $("#cmbAula").html("<option value='0'>-----------------Todos----------------------<option>")
            return false;
        }
if(codGrado==0)
{
    $("#cmbAula").html("<option value='0'>-----------------Todos----------------------<option>")
    return false;
}
        codAnio = isNaN(parseInt($("#cmbPeriodo option:selected").val())) ? 0 : parseInt($("#cmbPeriodo option:selected").val())

       


 
        
        $.ajax({
            url: "frmPrestamoLibro2.aspx/FListarAulasXGrado",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                codGrado: codGrado,
                codAnio: codAnio
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {
                $("#cmbAula").html(res.d.html)

                F_AlumnosXaula();
            }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
        });
    }

    /*******************cargar alumnmos  por aula segun el anio academico y grado  ******************************/
    function F_AlumnosXaula() {

        var codAula = 0
        var codAnio = 0
        codAula =codAula =isNaN( parseInt($("#cmbAula option:selected").val()))?0: parseInt($("#cmbAula option:selected").val())
        
       
        codAnio =isNaN( parseInt($("#cmbPeriodo option:selected").val()))?0: parseInt($("#cmbPeriodo option:selected").val())


        var codGrado = isNaN(parseInt($("#cmbGrado option:selected").val())) ? 0 : parseInt($("#cmbGrado option:selected").val())

        if (codGrado==0) {
            return false;
        }

        $.blockUI({
            message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Listando alumnos...</h4>'
        });
        
        
        $.ajax({
        url: "frmPrestamoLibro2.aspx/F_listarAlumnosXAula",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                codAula: codAula,
                codAnio: codAnio,
                codGrado: codGrado
                
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {


            $("#listaLibros").html(res.d.html)
            $.unblockUI();

            }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
        });
    }
    /*******************funcion efectos grilla filas    ******************************/




    function Over(ctr) {

        $(ctr).css("backgroundColor", "#DEE8F5")

    }

    function Out(ctr) {
        $(ctr).css("backgroundColor", "#FFFFFF")


    }
    /*******************funcion agregar elementos  a la grilla   ******************************/

    function AddItemGrilla() {
       
   
    var codBarra="";
        codBarra=$("#txtCodigoBarras").val();

        var codAlumno = 0;
        codAlumno = parseInt( $("#cmbAlumno option:selected").val())
        var codAnio = 0;
        codAnio =  parseInt( $("#cmbPeriodo option:selected").val())


        if ( $.trim(codBarra) =="") {
            Sexy.alert("Ingrese el codigo de barra ")

            return false;
        }
                 if (codAlumno == 0   )
                       {
                        Sexy.alert("Seleccione el alumno")
                        return false;
                       }

                if (codAnio == 0) {
                    Sexy.alert("Seleccione UN año academico")
                    return false;
                }

                $.blockUI({
                    message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Agregando elemento...</h4>'
                });
        var nombreAlumno = "";
        nombreAlumno = $("#cmbAlumno option:selected").text()
        $.ajax({
            url: "frmPrestamoLibro2.aspx/F_operacionAgregarElemento",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                nombreAlumno: nombreAlumno,
                codAnio: codAnio,
                codAlumno: codAlumno,
                codBarra: codBarra,
                listaActual: listaPrestamos
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {


            if (res.d.codOperacion > 0) {
                    Sexy.info(res.d.mensaje)
                    listaPrestamos = res.d.ListaACtualizada
                    $("#listaLibros").html(res.d.html)
                }
                else {
                    Sexy.alert(res.d.mensaje)
                 }

                 /*
                
                 */
              
                $.unblockUI();


            }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
        });
    
    }

  

/*******************funcion Insertar base de datos    ******************************/
function F_insertarBD() {
/*-----------------Validar si esta cargado */
    if (listaActualAlumno.length == 0)
        {
            Sexy.alert("Ingrese registros para guardar ")
            return false;
        }
if(!confirm("Seguro que desea guardar "))
{
    return false;
}


$.blockUI({
    message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Insertando elemento...</h4>'
});

$.ajax({
    url: "frmPrestamoLibro2.aspx/F_insertarPrestamoLibros",
    async: false,
    cache: false,
    type: "post",
    data: JSON.stringify({
         listaActual: listaActualAlumno
    }),
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    success: function(res, textStatus, jqXHR) {

    if (res.d.codigo > 0)
         {
            Sexy.info(res.d.mensaje)
            listaActualAlumno = res.d.fuenteActualizado;
            $("#tblAlumnos tr[id=" + filaEdicionActual.codALummo + "] td:eq(1) div").text(res.d.cantidadDatos)

          

        }
        else {
            Sexy.alert(res.d.mensaje)
        }

        $.unblockUI();
    }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
});

}


/*******************funcion Eliminar item prestamo detalle     ******************************/

function f_limpiarInput() {
    $("#txtCodigoBarras").val("")
    
    //$("#cmbPeriodo option").removeAttr("selected")
    //$("#cmbPeriodo[option=0]").attr("selected", true)
    
    $("#cmbGrado option").removeAttr("selected")
    $("#cmbGrado option[value=0]").attr("selected", true)
    
    
    
    F_cagarAulasXGrado();
    listaActualAlumno = [];
    $("#listaLibros").html("")
}


  /*************************************************************************************************************/
 /*******************filtro  para cambiar  los filtros de busqueda               ******************************/
/*************************************************************************************************************/
/*******************cargar aulas por grado busqueda  ******************************/


/*******************limpiar filtro de busqueuda                ******************************/
function f_limpiarInputBusqueda() {
    $("#txtCodigoBarrasBusqueda").val("")
    $("#cmbPeriodoBusqueda option").removeAttr("selected")
    $("#cmbPeriodoBusqueda option[value=0]").attr("selected", true)
    $("#cmbGradoBusqueda option").removeAttr("selected")
    $("#cmbGradoBusqueda option[value=0]").attr("selected", true)
    
    F_cagarAulasXGradoBusqueda();


}


function F_cagarAulasXGradoBusqueda() {

    var codGrado = 0
    var codAnio = 0



    codGrado = isNaN(parseInt($("#cmbGradoBusqueda option:selected").val())) ? 0 : parseInt($("#cmbGradoBusqueda option:selected").val())
    codAnio = isNaN(parseInt($("#cmbPeriodoBusqueda option:selected").val())) ? 0 : parseInt($("#cmbPeriodoBusqueda option:selected").val())

 
    $.ajax({
        url: "frmPrestamoLibro2.aspx/FListarAulasXGrado",
        async: false,
        cache: false,
        type: "post",
        data: JSON.stringify({
            codGrado: codGrado,
            codAnio: codAnio
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(res, textStatus, jqXHR) {
        $("#cmbAulaBusqueda").html(res.d.html)
        F_AlumnosXaulaBusqueda();
       
        }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
    });
}

function F_AlumnosXaulaBusqueda() {

    var codAula = 0
    var codAnio = 0

    
    codAula = isNaN(parseInt($("#cmbAulaBusqueda option:selected").val())) ? 0 : parseInt($("#cmbAulaBusqueda option:selected").val())

    
    codAnio = isNaN(parseInt($("#cmbPeriodoBusqueda option:selected").val())) ? 0 : parseInt($("#cmbPeriodoBusqueda option:selected").val())

    var codGrado = isNaN(parseInt($("#cmbGradoBusqueda option:selected").val())) ? 0 : parseInt($("#cmbGradoBusqueda option:selected").val())



    $.ajax({
    url: "frmPrestamoLibro2.aspx/F_listarAlumnosXAulaBuscar",
        async: false,
        cache: false,
        type: "post",
        data: JSON.stringify({
            codAula: codAula,
            codAnio: codAnio,
            codGrado:codGrado
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(res, textStatus, jqXHR) {
        $("#cmbAlumnoBusqueda").html(res.d.html)


        }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
    });
}
/*******************funcion para buscar      ******************************/
function F_BuscarPrestamos()  {
    $.blockUI({
        message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Buscando  ...</h4>'
    });
    
var pCodAnio =0;
pCodAnio = parseInt( $("#cmbPeriodoBusqueda option:selected").val())
var pCodGrado =0;
pCodGrado = parseInt( $("#cmbGradoBusqueda option:selected").val())
var pCodAula =0;
pCodAula = parseInt( $("#cmbAulaBusqueda option:selected").val())
var pCodAlumno =0;
pCodAlumno = parseInt( $("#cmbAlumnoBusqueda option:selected").val())
var pCodBarra ="";
pCodBarra=$.trim($("#txtCodigoBarrasBusqueda").val())
 
    $.ajax({
        url: "frmPrestamoLibro2.aspx/F_ListarPrstamos",
        async: false,
        cache: false,
        type: "post",
        data: JSON.stringify({
            codAnio: pCodAnio,
            codGrado: pCodGrado,
            codAula: pCodAula,
            codAlumno: pCodAlumno,
            codBarra: pCodBarra
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(res, textStatus, jqXHR) {
        $("#pnlResultado").html(res.d.html)
        
        $('input[id^=fecha]').calendarsPicker({
        showTrigger: '#calImg',
               dateFormat: 'dd/mm/yyyy',
               rangeSelect: false,
               selectDefaultDate: true,
               defaultDate: '-1w',
              onChangeMonthYear: function(year, month) { 
             //$(this).val($(this).val())

            // alert($(this).calendarsPicker('getDate'))
             //alert('Moving to month ' + month + '/' + year);


          //   $(this).calendarsPicker('setDate', $(this).val()); 
             
             }
         });

         /***********************************/

      
        /****************************************/
        
            $.unblockUI();

        }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
    });

}


/*******************funcion para edicion de registros******************************/
function EditarFilas(ctr, codReg) {
    var fechaNueva  =$.trim( $(ctr).parents("td").next("td").find("input").val())
    var codRegistro = codReg;
    
        $(ctr).attr("src", "../App_Themes/Imagenes/bigrotation2.gif")
        $(ctr).attr("title", "Actualizando");

        $.ajax({
            url: "frmPrestamoLibro2.aspx/F_actualizarFechaPrestamoDetalleLibro",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                codPrestamo: codRegistro,
                fechaPrestamo: fechaNueva
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {

            
                if (parseInt( res.d.codigo) > 0) {

                   
                   // Sexy.info(res.d.mensaje)
                }
                else {
                    Sexy.alert(res.d.mensaje)
                 }
               
               
                $(ctr).attr("src", "../App_Themes/Imagenes/actualizarFecha.png")
                $(ctr).attr("title", "Actualizar");

            }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
        });


    }

/*******************funcion   cambiar de panel  agregar nuevo******************************/
function F_cambiarPanel() {
    $("#tabs").tabs("enable", 1)
    $("#tabs").tabs("select", 1);
    $("#tabs").tabs("disable", 0);
    $("#txtCodigoBarras").val("")
    listaActualAlumno = [];
    $("#listaLibros").html("")
    f_limpiarInput();
}
/*******************funcion   cambiar de panel   cancelar regresar consultar******************************/
function F_cambiarPanelCancelar() {
    $("#tabs").tabs("enable", 0)
    $("#tabs").tabs("select", 0);
    $("#tabs").tabs("disable", 1);
    $("#txtCodigoBarrasBusqueda").val("")
    listaActualAlumno = [];
    $("#listaLibros").html("")
    f_limpiarInput();
}


/*******************************************************************************************************/


</script>


  <script type="text/javascript">

      /*******************************************************************************************************/
      /*Variables globales         */
      var fechaActua=<%=fechaActual %>
      var filaEdicionActual = {};
      var listaActualAlumno = [];
 /*****funciones para dialogos***************************************************************************/
/*******************************************************************************************************/

      $(document).ready(function() {
      
       
      $("#menu").hide('fast');
      
      $('#pnlPrestamos').dialog({
          autoOpen: false,
          modal: true,
          width: 700,
          height: 560,
          center: true,
          title: "Prestamos",
          buttons: {
              "Cerrar": function() {
                  $(this).dialog("close");
              }
          }
      });


      $(".ui-dialog-titlebar .ui-widget-header .ui-corner-all .ui-helper-clearfix").remove();
   
      });


/*********************************funciones abrir dialogo edicion de prestamos ***************************************************************************/
      function F_AbrirVentanaEdicionPrestamos(oEvent) {
      
   
      
        filaEdicionActual = oEvent;
        $("#pnlPrestamos").dialog("open");
        $("#txtCodigoBarra").val("")
        $("#ctl00_ContentPlaceHolder1_txtFechaInicio").val(fechaActua.fecha)
        $("#lblAnio").text($("#cmbPeriodo option:selected").text())
        $("#lblgrado").text($("#cmbGrado option:selected").text())
         
         
         
          $("#lblNombreAlumno").text(filaEdicionActual.nombrePersona)
          $.ajax({
              url: "frmPrestamoLibro2.aspx/F_listarDetalleLibrosPrestamo",
              async: false,
              cache: false,
              type: "post",
              data: JSON.stringify({
                  codAnio: filaEdicionActual.codAnio,
                  codAlumno: filaEdicionActual.codALummo

              }),
              dataType: "json",
              contentType: "application/json; charset=utf-8",
              success: function(res, textStatus, jqXHR) {

               

                  if (res.d.codigo > 0) {
                      $("#pnlDetallePrestamo").html(res.d.html);
                      listaActualAlumno = res.d.listaNueva;

                  }
                  else {
                      Sexy.alert(res.d.mensaje)
                  }


              }
              /*
              F_listarDetalleLibrosPrestamo(ByVal codAnio As Integer, ByVal codAlumno As Integer) As Object
              */

      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
          });

      }




      /*********************************Agregar libro***************************************************************************/

      function F_AgregarLibro() {
var codBarra= $.trim($("#txtCodigoBarra").val());
var fecha =$("#ctl00_ContentPlaceHolder1_txtFechaInicio").val();


if ( codBarra.length<=10)
{
return  false;
}

$.ajax({
    url: "frmPrestamoLibro2.aspx/F_listarDisponibilidadLibro",
    async: false,
    cache: false,
    type: "post",
    data: JSON.stringify({
        fecha: fecha,
        codBarra: codBarra,
        codAlumno: filaEdicionActual.codALummo,
        codAnio: filaEdicionActual.codAnio,
        listaActual: listaActualAlumno

    }),
    dataType: "json",
    contentType: "application/json; charset=utf-8",

    success: function(res, textStatus, jqXHR) {

        if (res.d.codigo > 0) {
            $("#pnlDetallePrestamo").html(res.d.html);
            listaActualAlumno = res.d.listaNueva;
        }
        else {
            Sexy.alert(res.d.mensaje)
        }
       
       
       



    }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
});




}

/*******************funcion Eliminar item prestamo detalle     ******************************/
function fEliminar(index) {

    if (!confirm("Seguro que desea Eliminar ")) {
        return false;
    }

    $.blockUI({
        message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Eliminado item ...</h4>'
    });

    $.ajax({
        url: "frmPrestamoLibro2.aspx/F_eliminarItem",
        async: false,
        cache: false,
        type: "post",
        data: JSON.stringify({
            indexDetalle: index,
            listaActual: listaActualAlumno
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(res, textStatus, jqXHR) {

            if (res.d.codOperacion > 0) {
                Sexy.info(res.d.mensaje)
                listaActualAlumno = res.d.ListaACtualizada
                $("#pnlDetallePrestamo").html(res.d.html);
            }
            else {
                Sexy.alert(res.d.mensaje)
            }



            $.unblockUI();

        }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
    });

}
  
    </script>
</asp:Content>

