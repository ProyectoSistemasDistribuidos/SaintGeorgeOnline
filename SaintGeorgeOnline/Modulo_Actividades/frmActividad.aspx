<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="frmActividad.aspx.vb" Inherits="Modulo_Actividad_frmActividad" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

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
    width:95px;
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
        width:700px;
    }
    .nombreEtiquetaGrande
    {
    height:120px;
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
      width:150px;
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
        	
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../App_Themes/Estilos/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
   
<div id="tabs">
    <ul>
        <li><a href="#fragment-3"><span >Busqueda</span></a></li>
        <li><a href="#fragment-2"><span>Registro</span></a></li>

    </ul>
     <div id="fragment-2" style="height: auto; overflow :hidden ">
<fieldset class="agrupador">

<legend class="milegend">

    Activities Registration</legend>

<div class="contenedor">
<div class="filas">
<div class="nombreEtiqueta">

    Actividad:</div>
<div class="celdaDerecha">

    <input id="txtNombreActividad" type="text" /></div>
<div class="botonGrabar">

    <input id="btnGrabar" onclick="f_insetarActividad()" type="button"  value="Grabar" class="miboton" />
    <input id="Button1" onclick="f_cancelar()" type="button"  value="Cancelar" class="miboton" />
    </div>
    
</div>


<div class="filas">
<div class="nombreEtiqueta">Fecha Inicio:</div>
<div class="celdaFecha">
    <table cellpadding="0" cellspacing="0" border="0" width="120px">
                    <tr>
                        <td valign="middle" align="left" style="width: 90px; height: 25px;">
                            <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="miTextBoxCalendar" Width="70px" 
                                style="font-size: 8pt; font-family: Arial; text-align: left" />
                            <atk:TextBoxWatermarkExtender ID="txtFechaInicio_TextBoxWatermarkExtender" 
                                runat="server" Enabled="True" TargetControlID="txtFechaInicio" 
                                WatermarkText="dd/mm/yyyy">
                            </atk:TextBoxWatermarkExtender>
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

<%--<div style="height: 25px; width:100px; float:left;color: Red; font-family: Arial; font-size: 9pt;">
Formato (dd/mm/yyy)</div>--%>
<div class="nombreEtiqueta">Hora de Inicio:</div>
<div class="celdaHora">
    <input id="txtHoraInicio" type="text" />    
   <%-- <span style="color: Red; font-family: Arial; font-size: 9pt;">(formato 24 horas: 15:00)</span>--%>
     
</div>
<%--
<div style="height: 25px; width:100px; float:left;color: Red; font-family: Arial; font-size: 9pt;">
Formato (hh/mm/ss)</div>--%>

</div>
<div class="filas">
<div class="nombreEtiqueta">Fecha Fin:</div>
<div class="celdaFecha">
    <table cellpadding="0" cellspacing="0" border="0" width="120px">
                    <tr>
                        <td valign="middle" align="left" style="width: 90px; height: 25px;">
                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="miTextBoxCalendar" Width="70px" 
                                style="font-size: 8pt; font-family: Arial; text-align: left" />
                            <atk:TextBoxWatermarkExtender ID="txtFechaFin_TextBoxWatermarkExtender" 
                                runat="server" Enabled="True" TargetControlID="txtFechaFin" 
                                WatermarkText="dd/mm/yyyy">
                            </atk:TextBoxWatermarkExtender>
                            <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                TargetControlID="txtFechaFin"
                                UserDateFormat="DayMonthYear"                                                                    
                                Mask="99/99/9999" 
                                MaskType="Date" 
                                PromptCharacter="-">
                            </atk:MaskedEditExtender>                                                                                                                          
                        </td>
                        <td valign="middle" align="left" style="width: 30px; height: 25px;">
                            <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                            <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                TargetControlID="txtFechaFin"
                                PopupButtonID="ImageButton1" 
                                Format="dd/MM/yyyy" 
                                CssClass="MyCalendar" Enabled="True" />       
                        </td>
                    </tr>
                    </table>
</div>

<%--<div style="height: 25px; width:100px; float:left;color: Red; font-family: Arial; font-size: 9pt;">
Formato  
    (dd/mm/yyy)</div>--%>
<div class="nombreEtiqueta">Hora de Fin:</div>
<div class="celdaHora">

    <input id="txtHoraFin" type="text" />
    
</div>

<%--<div style="height: 25px; width:100px; float:left;color: Red; font-family: Arial; font-size: 9pt;">
Formato (hh/mm/ss)</div>>--%>
</div>

<div class="filas">
<div class="nombreEtiqueta">

    Lugar:</div>
<div class="celdaDerecha">

    <input id="txtLugar" type="text" /></div>
</div>
<div class="filas">
<div class="nombreEtiqueta">

    Organizador:</div>
<div class="celdaDerecha">

    <select id="cmbOrganizador" name="D1">
    <option value="0">------------Seleccione------------</option> 
      <%For Each filaTrabajador As System.Data.DataRow In dtTrabajador.Rows%>
      
      
     <%If CInt (filaTrabajador("codigo").ToString())=codTrab  then %>
      
      <option selected="selected" value="<%=filaTrabajador("codigo") %>"><%=filaTrabajador("nombre")%></option>
     
     <%Else%>
  <option  value="<%=filaTrabajador("codigo") %>"><%=filaTrabajador("nombre")%></option>
      <%end if%>
      
      <%Next%>
    </select></div>
</div>


<div class="filasGrande">
<div class="nombreEtiquetaGrande">

    Objetivo:</div>
<div class="celdaDerechaGrande">

     <textarea id="txtObjetivo" name="S1"></textarea> 
    
  
    
    </div>
</div>

<div class="filas">
 <div class="nombreEtiqueta">

  
     Tipo actividad :</div>
    <div style="; height:25px; width:auto">
    
        <asp:RadioButtonList ID="rbtTipoActividad" runat="server" Font-Names="Andalus" 
            Font-Size="8pt"  ForeColor="#7B79B0" RepeatDirection="Horizontal">
        </asp:RadioButtonList>
    
    </div>
    
</div>


<div class="filas">
 <div class="nombreEtiqueta">
N° docentes
     :</div>
    <div class="cajasSmallLeft">
    
        <input id="txtNumeroDocentes" type="text" readonly="readonly" /></div>
     <div class="cajasSmallRight">
    
         <img  onclick="F_abrirDocentes()" title="agregar" alt="" style=" cursor:pointer;" src="../App_Themes/Imagenes/agregarNuevo.png"  /> (Add)</div>
     
</div>
<div class="filas">
 <div class="nombreEtiqueta">
     N° Asistentes
     :</div>
    <div class="cajasSmallLeft">
    
        <input id="txtNumeroAsistentes" type="text" readonly="readonly" /></div>
     <div class="cajasSmallRight">
    
         <img title="agregar" alt="" onclick="F_abrirAsistentes()" style="cursor:pointer;" src="../App_Themes/Imagenes/agregarNuevo.png"  /> (Add)</div>
    
</div>
<div class="filas">
 <div class="nombreEtiqueta">
     N° Alumnos
     :</div>
    <div class="cajasSmallLeft">
    
        <input id="txtNumeroAlumnos" type="text" /></div>
     <div class="cajasSmallRight">
    
    </div>
    
</div>
<div class="filas">
 <div class="nombreEtiqueta">
     N° Padres
     :</div>
    <div class="cajasSmallLeft">
    
        <input id="txtNumeroPadres" type="text" /></div>
     <div class="cajasSmallRight">
         &nbsp;</div>
    
</div>
<div class="filasCheck">
<div class="labelCheck">

    Para:</div>
    <div class="celdaChecks">
    
        <asp:CheckBoxList ID="chkListDestinatario" runat="server" BorderWidth="0px" 
            CellPadding="1" CellSpacing="2" Font-Names="Arial" Font-Size="Small" 
            ForeColor="#7B79B0" RepeatColumns="1" RepeatDirection="Horizontal">
        </asp:CheckBoxList> 
        
    
        </div>
</div>

<div class="filasCheck">
<div class="labelCheck">
    
    Grados:</div>
    <div class="celdaChecks">
    
        <asp:CheckBoxList ID="chkListGrados" runat="server" BorderWidth="0px" 
            CellPadding="1" CellSpacing="2" Font-Names="Arial" Font-Size="Small" 
            ForeColor="#7B79B0" RepeatColumns="3" RepeatDirection="Horizontal">
        </asp:CheckBoxList> 
        
    
    </div>
</div>

</div>
</fieldset>
</div>

<div id="fragment-3">
<div class="contenedorBusqueda">
<fieldset>

<legend class=" milegend ">
Search
</legend>
<div class=" filaBusqueda">
<div class="etiquetaFilaBusqueda">

    <input checked="checked" name="opciones" id="rbtAnioMes" onclick="mostrarAnioMes()" type="radio" /></div>
<div class="etiquetaFilaBusquedaCentrado">

    Año y mes </div>
    
    
<div class="etiquetaFilaBusquedaCentrado">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input type="radio" name="opciones" onclick="mostrarFechas()" id="rbtRangoFechas" />
    </div>
    <div class="etiquetaFilaBusquedaCentrado">

        Rango de fechas :</div>
    
</div>


<div class="filasOpcionesAnioMes">
<div class="filaBusqueda">
    <div class="etiquetaFilaBusquedaCentrado">

        Year:</div>
        <div class="celdaDerecha">
        <select id="cmbYear">
        <option value="0">-------Todos-------</option>
        <%For Each anio As Integer In lstAnios%>
      
      <%If anio = Year(Date.Now) Then%>
      
      
        <option  selected =selected  value="<%=anio%>"><%=anio%></option>
        
        <%Else%>
         <option value="<%=anio%>"><%=anio%></option>
       <%End If%> 
        
        
        
        <%
        Next%>
        </select>
        </div>
    
</div>
<div class="filaBusqueda">
    <div class="etiquetaFilaBusquedaCentrado">

        Month:</div>
        <div class="celdaDerecha">
          <select id="cmbMes">
        <option value="0">-------Todos-------</option>
     
     <%  Dim count As Integer = 0 : For Each strMes In lstMeses%>
     <%count += 1%>
     <%If Month(Date.Now) = count Then%>
       <option selected=selected value="<%= count%>"><%=strMes%></option>
     <%Else%>
       <option value="<%=count %>"><%=strMes%></option>
     <%End If%>
    
      
      
     <% Next%>
        </select>
        </div>
    
</div>
</div>

<div class="filasOpcionesFechas">
<div class=" filaBusqueda">
<div class="etiquetaFilaBusquedaCentrado">
    Del </div>
    <div class="etiquetaFilaBusquedaCentrado">
          <table cellpadding="0" cellspacing="0" border="0" width="120px">
                    <tr>
                        <td valign="middle" align="left" style="width: 90px; height: 25px;">
                            <asp:TextBox ID="txtDel" runat="server" CssClass="miTextBoxCalendar" Width="70px" 
                                style="font-size: 8pt; font-family: Arial; text-align: left" />
                            <atk:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                TargetControlID="txtDel"
                                UserDateFormat="DayMonthYear"                                                                    
                                Mask="99/99/9999" 
                                MaskType="Date" 
                                PromptCharacter="-">
                            </atk:MaskedEditExtender>                                                                                                                          
                            <atk:CalendarExtender ID="CalendarExtender3" runat="server" 
                                TargetControlID="txtDel"
                                PopupButtonID="imgDel" 
                                Format="dd/MM/yyyy" 
                                CssClass="MyCalendar" Enabled="True" />       
                        </td>
                        <td valign="middle" align="left" style="width: 30px; height: 25px;">
                            <asp:ImageButton runat="server" ID="imgDel" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                        </td>
                    </tr>
                    </table>
    </div>
    <div class="etiquetaFilaBusquedaCentrado">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Al </div>
    <div class="etiquetaFilaBusquedaCentrado">
          <table cellpadding="0" cellspacing="0" border="0" width="120px">
                    <tr>
                        <td valign="middle" align="left" style="width: 90px; height: 25px;">
                            <asp:TextBox ID="txtAl" runat="server" CssClass="miTextBoxCalendar" Width="70px" 
                                style="font-size: 8pt; font-family: Arial; text-align: left" />
                            <atk:MaskedEditExtender ID="MaskedEditExtender4" runat="server" 
                                TargetControlID="txtAl"
                                UserDateFormat="DayMonthYear"                                                                    
                                Mask="99/99/9999" 
                                MaskType="Date" 
                                PromptCharacter="-">
                            </atk:MaskedEditExtender>                                                                                                                          
                            <atk:CalendarExtender ID="CalendarExtender4" runat="server" 
                                TargetControlID="txtAl"
                                PopupButtonID="imgAl" 
                                Format="dd/MM/yyyy" 
                                CssClass="MyCalendar" Enabled="True" />       
                        </td>
                        <td valign="middle" align="left" style="width: 30px; height: 25px;">
                            <asp:ImageButton runat="server" ID="imgAl" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                        </td>
                    </tr>
                    </table>
    </div>
 
</div>
</div>
</fieldset>

<div style="margin:0;padding:0;height:auto;width:850px;">

<div class="filaBusqueda">
<div style="width: 200px; height: 25px; float:left; border: 0;">
<input type="button" value="Create Activity" onclick="f_cambiarPanel()" class="miboton" />
&nbsp;
<input type="button" value="Search" onclick="fBuscarActividad()" class="miboton" />
</div> 

<div style="width: 500px; height: 25px; float:left; border: 0;">
    <div style="height:25px; width:50px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <span>States:</span>
    </div>
    <div style="height:25px; width:25px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <img style="height:18px; width:18px; border: 0;" title="Pending" src="../App_Themes/Imagenes/icon-Yellow.png" />
    </div>
    <div style="height:25px; width:75px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <span>Pending</span>
    </div>
    <div style="height:25px; width:25px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <img style="height:18px; width:18px; border: 0;" title="Approved" src="../App_Themes/Imagenes/icon-Green.png" />
    </div>
    <div style="height:25px; width:75px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <span>Approved</span>
    </div>   
    <div style="height:25px; width:25px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <img style="height:18px; width:18px; border: 0;" title="Disapproved" src="../App_Themes/Imagenes/icon-Red.png" />
    </div>
    <div style="height:25px; width:75px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <span>Disapproved</span>
    </div>   
    <div style="height:25px; width:25px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <img style="height:18px; width:18px; border: 0;" title="Observed" src="../App_Themes/Imagenes/icon-Orange.png" />
    </div>
    <div style="height:25px; width:75px; font-size:8pt; text-align:left; float: left; vertical-align: middle;">
        <span>Observed</span>
    </div>     
</div>
</div>

<div style="border: solid 1px #a6a3a3; margin: 0; padding:0; height:25px;width:850px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Arial, Helvetica, sans-serif;">
<div style="height:25px;width:245px; float:left; line-height:25px; text-align:center">Activity</div>
<div style="height:25px;width:175px; float:left; line-height:25px; text-align:center">Organiser</div>
<div style="height:25px;width: 70px; float:left; line-height:25px; text-align:center">Type activity</div>
<div style="height:25px;width: 70px; float:left; line-height:25px; text-align:center">Date</div>
<div style="height:25px;width:140px; float:left; line-height:25px; text-align:center">Grade</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">E</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">D</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">P</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">JN</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">DIR</div>
<div style="height:25px;width: 25px; float:left; line-height:25px; text-align:center">INF</div>
</div>

<div id="grillaBusqueda" style="border-bottom:solid 1px #a6a3a3; margin:0; padding:0; height:auto; width:850px;">
</div>

</div>

</div>

</div>
 
<%--<!--------------------------Ventana de dialogos------------------------!>--%>


<div id="dlgDocentes" style="height:250px; width:650px;">
<%--<fieldset>
<legend class=" milegend">
    Seleccion de Profesores</legend>--%>    
<div style="border: solid 1px #a6a3a3; margin: 0; padding:0; width: 450px;">    
<table cellpadding="0" cellspacing="0" border="0" style="width: 450px; height: 26px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">    
<tr>
    <td style="width:  450px; height: 26px;" align="center" valign="middle">
        <span>Docentes</span>                                                                 
    </td>
</tr>
</table> 
</div>
<div style="height:125px; overflow-y:scroll; overflow-x: hidden; width:450px; border: solid 1px #a6a3a3;">
<asp:CheckBoxList ID="chkLDocentes" runat="server" BorderWidth="0px" Width="433px"
    CellPadding="2" CellSpacing="2" Font-Names="Arial" Font-Size="12px" 
    ForeColor="#7B79B0" RepeatColumns="1" RepeatDirection="Vertical">
</asp:CheckBoxList>
</div>
<br />
<div style="height:25px;  width:565px; float: right; border: solid 0px red;">
    <input onclick="f_obtenerListaDocentes()" type="button" value="Grabar" class="miboton" style="font-size: 12px; " />
</div>
<%--</fieldset>--%>
</div>

<div id="dlgAsistentes"  style="height:250px; width:650px;">
<%--<fieldset>
<legend class=" milegend">
    Seleccion de Asistentes</legend>--%>   
<div style="border: solid 1px #a6a3a3; margin: 0; padding:0; width: 450px;">    
<table cellpadding="0" cellspacing="0" border="0" style="width: 450px; height: 26px; color:White; background-color: #555555; font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">    
<tr>
    <td style="width:  450px; height: 26px;" align="center" valign="middle">
        <span>Asistentes</span>                                                                 
    </td>
</tr>
</table> 
</div>
<div style="height:125px; overflow-y:scroll; overflow-x: hidden; width:450px; border: solid 1px #a6a3a3;">
<asp:CheckBoxList ID="chklAsistentes" runat="server" BorderWidth="0px" 
    CellPadding="1" CellSpacing="2" Font-Names="Arial" Font-Size="Small" 
    ForeColor="#7B79B0" RepeatColumns="1" RepeatDirection="Vertical">
</asp:CheckBoxList>
</div>
<br />
<div style="height:25px;  width:565px; float: right; border: solid 0px red;">
    <input onclick="f_obtenerListaAsistentes()" type="button" value="Grabar" class="miboton" style="font-size: 12px; " />
</div>

<%--
</fieldset>--%>
</div>
<%--<!---------------------------------------------------------------------!>--%>



<script>



/****************************************************************************************************
//funciones para crear  water mark
//
//*********************************************/

(function($) {
	var map=new Array();
	$.Watermark = {
		ShowAll:function(){
			for (var i=0;i<map.length;i++){
				if(map[i].obj.val()==""){
					map[i].obj.val(map[i].text);					
					map[i].obj.css("color",map[i].WatermarkColor);
				}else{
				    map[i].obj.css("color",map[i].DefaultColor);
				}
			}
		},
		HideAll:function(){
			for (var i=0;i<map.length;i++){
				if(map[i].obj.val()==map[i].text)
					map[i].obj.val("");					
			}
		}
	}
	
	$.fn.Watermark = function(text,color) {
		if(!color)
			color="#aaa";
		return this.each(
			function(){		
				var input=$(this);
				var defaultColor=input.css("color");
				map[map.length]={text:text,obj:input,DefaultColor:defaultColor,WatermarkColor:color};
				function clearMessage(){
					if(input.val()==text)
						input.val("");
					input.css("color",defaultColor);
				}

				function insertMessage(){
					if(input.val().length==0 || input.val()==text){
						input.val(text);
						input.css("color",color);	
					}else
						input.css("color",defaultColor);				
				}

				input.focus(clearMessage);
				input.blur(insertMessage);								
				input.change(insertMessage);
				
				insertMessage();
			}
		);
	};
})(jQuery);
/***********************************************************************************************/
    //*******************************************************************************************//
    //
    //  VARIABLES GLOBALES
    //
    //*******************************************************************************************//
    var listaGrados = [];
    var listaDestinatarios = [];
    var listaAsistentes = [];
    var listaDocentes = [];
    var oActividad = null;
    var codActividad = 0;
    
    var codUsuario=<%=objServer %>
 


    //*******************************************************************************************//
    //
    // inicializar interfaz UI
    //
    //*******************************************************************************************//

    /*-----------------------------------------------------------
    // dialogo Docentes
    //-----------------------------------------------------------*/




    $(document).ready(function() {
    
    
    /*****************************************************/
$("#txtHoraInicio").Watermark("HH:MM");
$("#txtHoraFin").Watermark("HH:MM");
    
    $.Watermark.ShowAll();


//$("#txtHoraInicio,#txtHoraFin").click()


    /***************************************************/
        /*------------ Efectos filas-------------------*/
        mostrarAnioMes();
        $("#menu").hide('fast');
        $("#<%=chkLDocentes.ClientID%> tr").mouseover(function() {
            $(this).css("backgroundColor", "#DEE8F5")
        });
        $("#<%=chkLDocentes.ClientID%> tr").mouseout(function() {
            $(this).css("backgroundColor", "#FFFFFF")
        });

        $("#<%=chklAsistentes.ClientID%> tr").mouseover(function() {
            $(this).css("backgroundColor", "#DEE8F5")
        });
        $("#<%=chklAsistentes.ClientID%> tr").mouseout(function() {
            $(this).css("backgroundColor", "#FFFFFF")
        });


        $("#txtHoraFin").timeEntry({show24Hours: true});

        $("#txtHoraInicio").timeEntry({ show24Hours: true });

        /*------------------------------*/

        /******************Validacion de numeros **************************/
        $("#txtNumeroDocentes,#txtNumeroAsistentes,#txtNumeroAlumnos,#txtNumeroPadres").limitkeypress({ rexp: /^[0-9]{1,6}(([0-9]){0,4})?$/ });
        
        //$("#txtObjetivo").limitkeypress({ rexp: /^[Aa-zZ0-9]{1,10}$/ });

//        $("#Text1,#Text3").limitkeypress({ rexp: /^[0-2]{1}$/ });
//        $("#Text2,#Text4").limitkeypress({ rexp: /^[0-9]{1,2}$/ });
        
        /****************************************************************/


        $("#tabs").tabs({ collapsible: true });
        /*-----------------------------------------------------------
        // crear tabs
        //-----------------------------------------------------------*/
        $("#tabs").tabs("disable", 1);
        /*-----------------------------------------------------------
        // dialogo docentes
        //-----------------------------------------------------------*/
        $('#dlgDocentes').dialog({
            autoOpen: false,
            modal: true,
            width: 600,
            height: 350,
            title: "Docentes",
            buttons: {
                "Cerrar": function() {
                    $(this).dialog("close");
                }
            }
        });

        /*-----------------------------------------------------------
        // dialogo Asistentes
        //-----------------------------------------------------------*/
        $('#dlgAsistentes').dialog({
            autoOpen: false,
            modal: true,
            width: 600,
            height: 350,
            title: "Asistentes",
            buttons: {
                "Cerrar": function() {
                    $(this).dialog("close");
                }
            }
        });


    });


    //*******************************************************************************************//
    // funciones para guardar la actividad 
    //*******************************************************************************************//

    function f_insertarActividad() {
        f_obtenerListagrados();
        f_obtenerListaDestinatario();
        f_obtenerListaAsistentes()
        f_obtenerListaDocentes()
        f_obtenerACtividad();



    }


    //*******************************************************************************************//
    // funcion  obtener  actividad
    //*******************************************************************************************//
    function f_obtenerACtividad() {
        var horaInicio = "";
        horaInicio = $("#txtHoraInicio").val()


        var horaFin = "";
        horaFin = $("#txtHoraFin").val()
        

        oActividad = {
            tipoActividad: ($("#<%=rbtTipoActividad.ClientID%> tr td input:radio:checked").toArray().length == 0) ? 0 : $("#<%=rbtTipoActividad.ClientID%> tr td input:radio:checked").val(),
            PA_CodigoProgramacionActividad: codActividad,
            nombreActividad: $("#txtNombreActividad").val(),
            fechaInicio: $("#ctl00_ContentPlaceHolder1_txtFechaInicio").val(),
            hraInicio: horaInicio,
            fechaFin: $("#ctl00_ContentPlaceHolder1_txtFechaFin").val(),
            hraFin: horaFin,
            lugar: $("#txtLugar").val(),
            organizador: $("#cmbOrganizador option:selected").val(),
            objetivo: $("#txtObjetivo").val(),
            numeroDocente: ($("#txtNumeroDocentes").val() == "") ? 0 : $("#txtNumeroDocentes").val(),
            numeroAsistentes: ($("#txtNumeroAsistentes").val() == "") ? 0 : $("#txtNumeroAsistentes").val(),
            numeroPadres: ($("#txtNumeroPadres").val() == "") ? 0 : $("#txtNumeroPadres").val(),
            dirigido: listaDestinatarios,
            grados: listaGrados,
            listaAsistentes: listaAsistentes,
            ListaDocentes: listaDocentes,
            numeroAlumnos: ($("#txtNumeroAlumnos").val() == "") ? 0 : $("#txtNumeroAlumnos").val(),
            codUsuario:codUsuario.codUsuario
        }

    }

    //*******************************************************************************************//
    // funcion  obtener  lista grados  asociados la actividad
    //*******************************************************************************************//
    function f_obtenerListagrados() {
        listaGrados = [];
        $("#<%=chkListGrados.ClientID%> tr td input:checkbox:checked").each(function() {
          
            var codGrado = $(this).next("label").find("span").text()
            listaGrados.push(codGrado)
       
        });

    }



    //*******************************************************************************************//
    // funcion  obtener  lista tipo destinatario  asociados la actividad
    //*******************************************************************************************//
    function f_obtenerListaDestinatario() {
        listaDestinatarios = [];
        $("#<%=chkListDestinatario.ClientID%> tr td input:checkbox:checked").each(function() {
            var codDest = $(this).next("label").find("span").text()
            listaDestinatarios.push(codDest)
        });

    }

    //*******************************************************************************************//
    // funcion  obtener  lista asistentes
    //*******************************************************************************************//
    function f_obtenerListaAsistentes() {
        listaAsistentes = [];
        $("#<%=chklAsistentes.ClientID%> tr td input:checkbox:checked").each(function(){
            var codGrado = $(this).next("label").find("span").text()
            listaAsistentes.push(codGrado)
        });
        $('#dlgAsistentes').dialog("close")
        $("#txtNumeroAsistentes").val(listaAsistentes.length)
    }



    //*******************************************************************************************//
    // funcion  obtener  lista Docentes
    //*******************************************************************************************//
    function f_obtenerListaDocentes() {
        listaDocentes = [];
        $("#<%=chkLDocentes.ClientID%> tr td input:checkbox:checked").each(function() {
            var codDest = $(this).next("label").find("span").text()
            listaDocentes.push(codDest)
        });
        $('#dlgDocentes').dialog("close");
        $("#txtNumeroDocentes").val(listaDocentes.length)
    }



    //*******************************************************************************************//
    // funcion ABRIR MODALES 
    //*******************************************************************************************//
    // -------------------------// DOCENTE //-------------------------
    function F_abrirDocentes() {
        $("#dlgDocentes").dialog("open");

    }


    //-------------------------//  ASISTENTES  //-------------------------

    function F_abrirAsistentes() {
        $("#dlgAsistentes").dialog("open");

    }


    /*********************************************************************
    * funciones para la insercion de la actividad a las base de datos 
    **********************************************************************/
    function f_insetarActividad() {

        f_insertarActividad();
        $.blockUI({
            message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Realizando la inserción...</h4>'
        });

        $.ajax({
            url: "frmActividad.aspx/FInsertarActividad",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                dcACtividad: oActividad
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {

                if (res.d.codigo > 0) {
                    Sexy.info('<br>' + res.d.mensaje)

                    /**************************************/
                    $("#tabs").tabs("enable", 0);
                    $("#tabs").tabs("select", 0);

                    $("#tabs").tabs("disable", 1);
                    /**************************************/

                    fBuscarActividad()
                }
                else {
                    Sexy.alert('<br>' + res.d.mensaje)
                    $.unblockUI();
                }
            }

      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
        });
    }


    /*********************************************************************
    * funciones para mostrar los paneles de tipo de filtros para busqueda 
    **********************************************************************/
    function mostrarAnioMes() {

        $(".filasOpcionesAnioMes").slideDown();
        $("#ctl00_ContentPlaceHolder1_txtAl,#ctl00_ContentPlaceHolder1_txtDel").val("")
        $(".filasOpcionesFechas").slideUp(); ;

    }


    function mostrarFechas() {
        $(".filasOpcionesAnioMes").slideUp();
        $(".filasOpcionesFechas").slideDown();
        $("#cmbYear option[value=0]").attr("selected", true)
        $("#cmbMes option[value=0]").attr("selected", true)

    }


    /*****************************************************************************/
    //** funciones para busqueda de actividades 
    /*****************************************************************************/
    function fBuscarActividad() {
        $.blockUI({
            message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Actualizando la grilla busqueda...</h4>'
        });
        $.ajax({
            url: "frmActividad.aspx/fBuscarActividades",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                anio: $("#cmbYear option:selected").val(),
                mes: $("#cmbMes option:selected").val(),
                fecha1: $("#ctl00_ContentPlaceHolder1_txtDel").val(),
                fecha2: $("#ctl00_ContentPlaceHolder1_txtAl").val(),
                codRegistro:codUsuario.codUsuario
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {
                $("#grillaBusqueda").html(res.d.html)
                $.unblockUI();
            }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
        });

    }

    /*****************************************************************************/
    //** funciones para efecto filas color
    /*****************************************************************************/
    function TiposControlesActualOver(control) {
        $(control).css("backgroundColor", "#DEE8F5")
    }
    function TiposControlesActualOut(control) {
        $(control).css("backgroundColor", "")
    }



    /*****************************************************************************/
    //** funciones para edicion de actividades
    /*****************************************************************************/
    function EditarFilas(PcodActividad) {
        $.ajax({
            url: "frmActividad.aspx/F_CargarActividadRegistro",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                PcodActividad: PcodActividad
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {


                codActividad = res.d[0].codActividad;
                $("#ctl00_ContentPlaceHolder1_txtFechaFin").val(res.d[0].fechaFin)
                $("#ctl00_ContentPlaceHolder1_txtFechaInicio").val(res.d[0].fechaFin)
                $("#txtNombreActividad").val(res.d[0].nombreActividad)
                $("#cmbOrganizador option[value=" + res.d[0].CodOrganizador + "]").attr("selected", true)
                $("#txtObjetivo").val(res.d[0].objetivoActividad)
                $("#txtNumeroPadres").val(res.d[0].numeroPadres)
                $("#txtNumeroAlumnos").val(res.d[0].numeroAlumnos)
                
                $("#txtHoraInicio").val(res.d[0].hrInicio);
                $("#txtHoraFin").val(res.d[0].hrFin);
                $("#txtLugar").val(res.d[0].lugar)

                $("#tabs").tabs("enable", 1);
                $("#tabs").tabs("select", 1);
                $("#tabs").tabs("disable", 0);
                
                /******************************/
                /*descativar todos los checks**/
                /******************************/
                F_lispiarGradosChecks()
                F_limpiarDestinatariosChecks()
                f_LimpiarAsistenteCheck()
                f_LimpiarDocenteCheck()
                f_desmarcarTipoActividad()
                /*****************************/

                /******************************/
                /*marcar  los checks         **/
                /******************************/
                f_marcarDocente(res.d[0].docente)
                f_marcarAsistente(res.d[0].docente)
                f_marcarGrado(res.d[0].grados)
                f_marcarDestinatario(res.d[0].destinatarios)
                f_marcarTipoActividad(res.d[0].tipoActividad)
                /******************************/


                /********************************/
                /*obtener l cantidad  docentes **/
                /********************************/
                f_obtenerListaDocentes()
                f_obtenerListaAsistentes()




            }


      , error: function(xhr, ajaxOptions, thrownError) {
          alert(xhr.status); alert(thrownError);
      }
        });

    }

    //------------------------------------------------------------------------------------------------------------------------------------
    /*****************************************************************************/
    //** funciones para marcar los check  docentes 
    /*****************************************************************************/
    function f_marcarDocente(lista) {
        $("#<%=chkLDocentes.ClientID%> tr td input:checkbox").each(function() {
            var codDest = $(this).next("label").find("span").text()

            for (var indice = 0; indice < lista.length; indice++) {
                if (lista[indice] == parseInt($.trim(codDest))) {
                    $(this).attr("checked", "checked");
                }
            }

        });


    }
    /******************************************************************************/
    //** funciones para marcar los check  Asistentes 
    /******************************************************************************/
    function f_marcarAsistente(lista) {
        $("#<%=chklAsistentes.ClientID%> tr td input:checkbox").each(function() {
            var codDest = $(this).next("label").find("span").text()
            for (var indice = 0; indice < lista.length; indice++) {
                if (lista[indice] == parseInt($.trim(codDest))) {
                    $(this).attr("checked", "checked");
                }
            }
        });


    }
    /******************************************************************************/
    //** funciones para marcar los check  grado 
    /******************************************************************************/
    function f_marcarGrado(lista) {
        $("#<%=chkListGrados.ClientID%> tr td input:checkbox").each(function() {
            var codDest = $(this).next("label").find("span").text()
            for (var indice = 0; indice < lista.length; indice++) {
                if (lista[indice] == parseInt($.trim(codDest))) {
                    $(this).attr("checked", "checked");
                }
            }
        });


    }



    /******************************************************************************/
    //** funciones para marcar los check  destinatario 
    /******************************************************************************/
    function f_marcarDestinatario(lista) {
        $("#<%=chkListDestinatario.ClientID%> tr td input:checkbox").each(function() {
            var codDest = $(this).next("label").find("span").text()
            for (var indice = 0; indice < lista.length; indice++) {
                if (lista[indice] == parseInt($.trim(codDest))) {
                    $(this).attr("checked", "checked");
                }
            }
        });


    }

    //------------------------------------------------------------------------------------------------------------------------------------

    //------------------------------------------------------------------------------------------------------------------------------------
    /******************************************************************************/
    //** funcion para borrar limpiar checks de los grados  
    /******************************************************************************/
    function F_lispiarGradosChecks() {
        $("#<%=chkListGrados.ClientID%> tr td input:checkbox").each(function() {
            $(this).removeAttr("checked");
        });

    }

    /******************************************************************************/
    //** funciones para borrar limpiar checks de los Destinatarios  
    /******************************************************************************/
    function F_limpiarDestinatariosChecks() {
        $("#<%=chkListDestinatario.ClientID%> tr td input:checkbox").each(function() {

            $(this).removeAttr("checked");
        });

    }

    /*****************************************************************************/
    //** funciones para borrar limpiar checks de los Asistentes 
    /*****************************************************************************/
    function f_LimpiarAsistenteCheck() {
        $("#<%=chklAsistentes.ClientID%> tr td input:checkbox").each(function() {
            $(this).removeAttr("checked");
        });


    }
    /*****************************************************************************/
    //** funciones para borrar limpiar checks de los docentes 
    /*****************************************************************************/
    function f_LimpiarDocenteCheck() {
        $("#<%=chkLDocentes.ClientID%> tr td input:checkbox").each(function() {
            $(this).removeAttr("checked");
        });
    }
    /******************************************************************************/
    //** funciones para desmarcar el tipo de atividad 
    /******************************************************************************/
    function f_desmarcarTipoActividad() {
        $("#<%=rbtTipoActividad.ClientID%> tr td input:radio").each(function() {

            $(this).removeAttr("checked");

        });


    }
    //------------------------------------------------------------------------------------------------------------------------------------

    /******************************************************************************/
    //** funciones para marcar el tipo de atividad 
    /******************************************************************************/
    function f_marcarTipoActividad(cod) {
        $("#<%=rbtTipoActividad.ClientID%> tr td input:radio").each(function() {
            var codDest = $(this).val();

            if (codDest == cod) {
                $(this).attr("checked", "checked");
            }

        });


    }

    /******************************************************************************/
    //** funciones para cambiar de panel  
    /******************************************************************************/
    function f_cambiarPanel() {
        codActividad = 0;
        $("#tabs").tabs("enable", 1);
        $("#tabs").tabs("disable", 0);
        $("#tabs").tabs("select", 1);
        $("#ctl00_ContentPlaceHolder1_txtFechaFin").val("")
        $("#ctl00_ContentPlaceHolder1_txtFechaInicio").val("")
        $("#txtNombreActividad").val("")
        
        
       // $("#cmbOrganizador option[value=0]").attr("selected", true)
        
        
        
        $("#txtObjetivo").val("")
        $("#txtNumeroPadres").val("")
        $("#txtNumeroAlumnos").val("")

        $("#txtHoraInicio").val("")
        $("#txtHoraFin").val("")
        /******************************/
        /*descativar todos los checks**/
        /******************************/
        F_lispiarGradosChecks()
        F_limpiarDestinatariosChecks()
        f_LimpiarAsistenteCheck()
        f_LimpiarDocenteCheck()
        f_desmarcarTipoActividad()
        f_obtenerListaDocentes()
        f_obtenerListaAsistentes()

    }

    function f_cancelar() {
        codActividad = 0;
        $("#tabs").tabs("enable", 0)
        $("#tabs").tabs("select", 0);
        $("#tabs").tabs("disable", 1);
        ;
    }



    /******************************************************************************/
    //**Eliminar actividad 
    /******************************************************************************/

    function EliminarActividad(PcodActividad) {


        if (confirm("¿Está seguro que dese eliminar el registro seleccionado?")) {

            //////////////////////////////////////////////////////////////////////


            $.blockUI({
                message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Eliminando..</h4>'
            });

            $.ajax({
                url: "frmActividad.aspx/F_Eliminar",
                async: false,
                cache: false,
                type: "post",
                data: JSON.stringify({
                    codActividad: PcodActividad
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function(res, textStatus, jqXHR) {
                    $("#grillaBusqueda").html(res.d.html)

                    if (res.d.codigo > 0) {
                        Sexy.info(res.d.mensaje)
                        fBuscarActividad()
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
        //////////////////////////////////////////////////////////////////////


    }

    /******************************************************************************/
    //**Impresion de actividad  
    /******************************************************************************/
    function Imprimir(codActividad) {
        window.open('/SaintGeorgeOnline/Modulo_Actividades/frmSession.aspx?codActividad=' + codActividad, '_blank', 'menubar=0,resizable=0,width=500,height=200');
    }
    
    /*
        Generar Informe
    */
    function GenerarInforme(paramcActividad){    
        $.ajax({
            url: "frmActividad.aspx/fRegistrarReporte",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                codActividad: paramcActividad
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {                
                var pathname = window.location.pathname;
                var url= pathname.replace("frmActividad.aspx","RegistroInformeActividad.aspx" )     
                $(location).attr('href',url);
            },
            error: function(xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
            }
        });     
    }
    
    
</script>
</asp:Content>

