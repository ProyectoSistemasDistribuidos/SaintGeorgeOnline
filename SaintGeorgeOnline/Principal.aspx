<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="Principal.aspx.vb" Inherits="Principal" title="Página sin título"   %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<%@ OutputCache Location ="None" %>

<%@ Register src="Modulo_Alertas/CalendarioCumpleanios.ascx" tagname="CalendarioCumpleanios" tagprefix="uc1" %>
<%@ Register src="Modulo_Alertas/AlertaStocksMedicamentos.ascx" tagname="AlertaStocksMedicamentos" tagprefix="uc2" %>
<%@ Register src="Modulo_Alertas/AlertaFichasAtencionPendientes.ascx" tagname="AlertaFichasAtencionPendientes" tagprefix="uc3" %>
<%@ Register src="Modulo_Alertas/AlertaSolicitudesActualizacionPendientesFichaFamiliar.ascx" tagname="AlertaSolicitudesActualizacionPendientesFichaFamiliar" tagprefix="uc4" %>
<%@ Register src="Modulo_Alertas/AlertaSolicitudesActualizacionPendientesFichaAlumno.ascx" tagname="AlertaSolicitudesActualizacionPendientesFichaAlumno" tagprefix="uc5" %>
<%@ Register src="Modulo_Alertas/AlertaSolicitudesActualizacionPendientesFichaMedica.ascx" tagname="AlertaSolicitudesActualizacionPendientesFichaMedica" tagprefix="uc6" %>
<%@ Register src="Controles/CalendarioEntrevistas.ascx" tagname="CalendarioEntrevistas" tagprefix="uc7" %>
<%@ Register src="Controles/ComunicadosHome.ascx" tagname="ComunicadosHome" tagprefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <link href="App_Themes/Estilos/cupertino/jquery-ui-1.7.3.custom.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <script src="App_Themes/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="App_Themes/jquery/jquery-ui-1.7.3.custom.min.js" type="text/javascript"></script>    
    <script src="App_Themes/jquery/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>
    <script src="App_Themes/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="App_Themes/Javascript/calendarscript-month.js" type="text/javascript"></script>
    <script src="App_Themes/jquery/jquery-ui-timepicker-addon-0.6.2.min.js" type="text/javascript"></script>

<style type="text/css">
    
        body
        {
            /*margin-top: 40px;
            text-align: center;*/ 
            margin: 0;
            padding: 0;
            /*
            font-size: 14px; 
            font-family: "Lucida Grande" ,Helvetica,Arial,Verdana,sans-serif;
            */
        }
        #calendar
        {
            width: 760px;
            margin: 0 10px 10px 10px;
            padding: 0;
            float: left;
        }        
        /* css for timepicker */
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            height: 25px;
        }
        .ui-timepicker-div dl dd
        {
            margin: -25px 0 10px 65px;
        }
        .style1
        {
            width: 100%;
        }        
        /* table fields alignment*/
        .alignRight
        {
        	text-align:right;
        	padding-right:10px;
        	padding-bottom:10px;
        }
        .alignLeft
        {
        	text-align:left;
        	padding-bottom:10px;
        }   
            
        fieldset
        {
            /*width: 800px;*/ 
            margin: 0; 
            padding: 0;
            border: solid 1px #a6a3a3;
            }    
        legend
        {
            margin: 0 0 0 20px; 
            padding: 0 0 0 10px;
            font-family: Arial;
            font-size: 12px;  
            border: solid 1px #375f90; /*#3079ed; */
            background-color: #375f92; /*#4b8efa; */
            color: #fff;
            display: block;
            line-height: 20px;
            width: 150px;
            }
    
</style>


<div>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
         
         <table cellpadding="0" cellspacing="0" border="0" style="width:1240px; height:400px; border: solid 0px red; text-align:right; vertical-align:top;">
            <tr>
                <td style="width:215px">
                    &nbsp;
                </td>
                
                <td style="width:240px;" align="left" valign="top">

    <div style="border: solid 0px blue; width: 240px; height: 600px; font-family: Arial, Helvetica, sans-serif; font-size: 10px;">    
        <fieldset style="height: 283px; width: 240px;">
            <legend>Favoritos</legend>
            <br />
        </fieldset> 
        <br />
        <fieldset style="height: auto; width: 240px;">
            <legend>Comunicados / Boletines</legend>
            <br />
            <div id="DetalleEventos" style="height: 200px; width:240px; border: solid 0px red;">
                <marquee id="iescroller" direction="up" width="240px" height="204px" scrollamount="2" scrolldelay="80" aling="center" onmouseout="this.start()" onmouseover="this.stop()"
                         style="border:0 solid #787878; background-color:#FFFFFF; margin: 0; padding:0;">                                                                  
                    <uc8:ComunicadosHome onmouseup="stop()" onmouseout="start()" ID="ComunicadosHome1" runat="server" /></marquee>                        
            </div>  
            <br />
        </fieldset>         
    </div>         
     
                </td>
                
                <td style="width:4px;">
                    &nbsp;
                </td>
                
                <td style="width:780px;" align="left" valign="top">

    <div style="border: solid 0px blue; width: 780px; height: 600px; font-family: Arial, Helvetica, sans-serif; font-size: 10px;">                 
        <fieldset style="width: 780px;">
            <legend>Calendario de Actividades</legend>
            <br />
            
            <table border="0" cellpadding="0" cellspacing="0" style="width: 780px; height: 25px; border: solid 0px red; display: none;" id="filtros">
                <tr>    
                    <td align="left" valign="middle" style="width: 50px;">
                        <span style="padding-left: 10px;">Filtros:</span>
                    </td>
                    <td align="left" valign="middle" style="width: 200px;">
                        <input type="checkbox" value="1" /><span style="padding-left: 5px; font-weight: bold;">Actividades</span>
                        &nbsp;
                        <input type="checkbox" value="2" /><span style="padding-left: 5px; font-weight: bold;">Entrevistas</span> 
                    </td>
                    <td align="left" valign="middle" style="width: 530px;">
                    </td>
                </tr>
            </table>
            <br />           
            
            <div id="calendar">
            </div>
            
            <br />
        </fieldset>              
    </div> 

    <div id="updatedialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 20px;" title="Consulta de Actividades">
        <table cellpadding="0" cellspacing="0" border="0" class="style1">
            <tr>
                <td class="alignRight">name:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" readonly="readonly" style="width: 400px;" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">description:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="30" rows="5" readonly="readonly" style="width: 400px;" ></textarea>
                </td>
            </tr>
            <tr>
                <td class="alignRight">start:</td>
                <td class="alignLeft">
                    <span id="eventStart"></span>
                </td>
            </tr>
            <tr>
                <td class="alignRight">end: </td>
                <td class="alignLeft">
                    <span id="eventEnd"></span>
                    <input type="hidden" id="eventId" />
                </td>
            </tr>
            <tr>
                <td class="alignRight">files: </td>
                <td class="alignLeft">
                    <div id="eventFile"></div>
                    <a id="eventFile2"></a> 
                </td>
            </tr>
        </table>
    </div>
    
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
                  
                </td>
                
                <td style="width:1px;">
                    &nbsp;
                </td>                
            </tr>
         </table>       
      
<%--   </ContentTemplate>
</asp:UpdatePanel> --%> 

</div>

</asp:Content>

