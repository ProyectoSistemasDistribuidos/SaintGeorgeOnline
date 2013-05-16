<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="RegistroInformeActividad.aspx.vb" Inherits="Modulo_Actividades_RegistroInformeActividad" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style type="text/css">

.miboton{
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
.miboton:hover{
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
fieldset
{
    width: 980px; 
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
    border: solid 1px #3079ed;
    background-color: #4b8efa; 
    color: #fff;
    display: block;
    line-height: 20px;
    width: 250px;
    }
#cabecera span, #detalle span
{
    padding-left: 10px;
    font-family: Arial;
    font-size: 11px;
    } 
#cabecera span.datos, #detalle span.datos
{
    font-weight: bold;
    padding: 0;
    font-family: Arial;
    font-size: 11px;
    }     

#cabecera tr
{
    height: 60px;    
    }   
#cabecera INPUT[type="text"]
{
    font-family: Arial;
    font-size: 10px;
    width: 600px;
    }            
#cabecera textarea /*INPUT[type="text"]*/
{
    font-family: Arial;
    font-size: 11px;
    height: 50px;
    width: 600px;
    text-align: left;
    vertical-align: top;
    }    
#detalle tr 
{
    height: 90px;    
    }    
#detalle textarea /*INPUT[type="text"]*/
{
    font-family: Arial;
    font-size: 11px;
    height: 80px;
    width: 830px;
    text-align: left;
    vertical-align: top;
    } 
#detalle ul li 
{
    margin: 0 0 0 20px;
    padding: 0;
    }        
    
</style>


<div id="miContainerMantenimiento">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">      
    <ContentTemplate> 
<div style="border: solid 0px blue; width: 980px; font-family: Arial, Helvetica, sans-serif; font-size: 10px;">
<fieldset>
    <legend>Datos de la Actividad</legend>
<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 980px;" id="cabecera">
<tr style="height: 10px;"><td colspan="2"></td></tr>
<tr  style="height: 25px;">
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Año:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:label ID="lblanio" runat="server" CssClass="datos" />
    </td>
</tr>
<tr  style="height: 25px;">
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Título de la Actividad:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:label ID="lbltitulo" runat="server" CssClass="datos" />
    </td>
</tr>
<tr>
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Grados:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">    
<asp:BulletedList ID="lstGrados" runat="server"></asp:BulletedList>    
    </td>
</tr>
<tr  style="height: 25px;">
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Responsable:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:label ID="lblresponsable" runat="server" CssClass="datos" />
    </td>
</tr>
<tr  style="height: 25px;">
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Día Actividad:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:label ID="lbldia" runat="server" CssClass="datos" />
    </td>
</tr>
<tr  style="height: 25px;">
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Lugar:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:label ID="lbllugar" runat="server" CssClass="datos" />
    </td>
</tr>
<tr style="height: 10px;"><td colspan="2"></td></tr>
</table>    
</fieldset>

<br />

<fieldset style="width: 980px; margin:0; padding: 0;">
    <legend>Datos del Reporte</legend>
<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 980px;" id="detalle">
<tr style="height: 10px;"><td colspan="2"></td></tr>
<tr>
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Objetivo:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:TextBox ID="tbObjetivo" runat="server" Rows="4" TextMode="MultiLine" />
    </td>
</tr>
<tr>
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Logros:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:TextBox ID="tblogros" runat="server" Rows="4" TextMode="MultiLine" />
    </td>
</tr>
<tr>
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Dificultades:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:TextBox ID="tbdificultades" runat="server" Rows="4" TextMode="MultiLine" />
    </td>
</tr>
<tr>
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Momentos Importantes:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:TextBox ID="tbmimportantes" runat="server" Rows="4" TextMode="MultiLine" />
    </td>
</tr>
<tr>
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Conclusiones:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:TextBox ID="tbconclusiones" runat="server" Rows="4" TextMode="MultiLine" />
    </td>
</tr>
<tr>
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Recomendaciones:</span>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:TextBox ID="tbrecomendaciones" runat="server" Rows="4" TextMode="MultiLine" />
    </td>
</tr>
<tr>
    <td style="width: 140px; height: 25px;" align="left" valign="middle">
<span>Información para Boletín (Área de Imagen):</span>
<br />
<span>Responder a las siguientes preguntas:</span>
<br />
<ul>
<li>¿Cúando?</li>
<li>¿Dónde?</li>
<li>¿Quienes?</li>
<li>¿Qué se aprendio?</li>
<li>¿Qué se vio?</li>
<li>Impresiones</li>
</ul>
    </td>
    <td style="width: 840px; height: 25px;" align="left" valign="middle">
<asp:TextBox ID="tbinformacionimagen" runat="server" Rows="7" TextMode="MultiLine" style="height: 150px;" />
    </td>
</tr>
<tr style="height: 10px;"><td colspan="2"></td></tr>
</table>    
</fieldset>

<br />
<div style="width: 980px; margin: auto; padding: 0;">    

<asp:LinkButton ID="btnGrabar" runat="server" ToolTip="Grabar" Text="Grabar" class="miboton" OnClick="btnGrabar_Click" />    
&nbsp;
<asp:LinkButton ID="btnCancelar" runat="server" ToolTip="Cancelar" Text="Cancelar" class="miboton" OnClick="btnCancelar_Click" />    
<asp:HiddenField ID="hiddenCodigoActividad" runat="server" value="0" />
</div>


    
</div>
    </ContentTemplate>
</asp:UpdatePanel>  
</div>

<script type="text/javascript" src="../App_Themes/Javascript/jquery-1.4.1.min.js" ></script> 
<script type="text/javascript">
    $(document).ready(function() {
        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);
    });

    function Cancelar() {
        if (confirm('¿Está seguro que desea salir sin guardar los cambios? Se perderás las modificaciones no grabadas.') == true)
            return true;
        else
            return false;
    }

</script>

</asp:Content>

