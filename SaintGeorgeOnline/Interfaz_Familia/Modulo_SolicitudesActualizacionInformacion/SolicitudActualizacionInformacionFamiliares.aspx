﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master"  AutoEventWireup="false" CodeFile="SolicitudActualizacionInformacionFamiliares.aspx.vb" Inherits="Interfaz_Familia_Modulo_SolicitudesActualizacionInformacion_SolicitudActualizacionInformacionFamiliares" %>

<%@ MasterType VirtualPath="~/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css" >

.respuesta_datos
{
	font-weight:bold;  
	font-size:11px; 
	font-family:Arial;  
}
.respuesta_input
{
    font-size: 11px;	
    font-family: Arial;  
    margin: 1px 0 1px 0;   
}
.titulo_Bloques
{
    font-size:13px;	
    font-family:Arial;
    font-weight: bold;
    color: White;
    height: 20px;
    width: 440px;
    margin-top: 10px;
    padding: 3px 0 0 20px;
    vertical-align: middle;
    text-align: left;
    display: block;
    background-color: #41576f;    
    border: solid 1px black;
}
.titulo_datos
{
    font-size:11px;	
    font-family:Arial;
    
}
.titulo_grupoInfo
{
    font-weight:bold;
    font-family:Arial;
    font-size:13px; 
}
.titulo_datos_contenedor
{
	vertical-align:top; 
	font-family:Arial;
}
.miHiddenStyle
{
    display:none
}
/* GRIDVIEW */
.gridview_header
{
    background-color: #41576f;    
    border-top: solid 1px black;             
    font-size:11px;	
    font-family:Arial;
    font-weight: bold;
    color: White;
    height: 20px;
}
.gridview_body
{
    border: solid 1px black; 
    background-color: #FFFFFF;
}
.gridview_body_Temporal
{
    border: solid 1px black; 
    background-color: #dcff7d;
}
.gridview_row
{      
    border-bottom: solid 1px black;       
    font-size:10px;	
    font-family:Arial;
    color: black;
    min-height: 20px;
}
/*MODAL POPUP*/
.MiModalBackground{
	background-color: black;    
	filter:alpha(opacity=70);
	opacity:0.7;
}
.modal_header
{
    background-color: #41576f;    
    height: 25px;  
    color: #FFFFFF;
    font-size: 12px; 
    font-family: Arial;
    font-weight: bold; 
    border-bottom: solid 1px black; 
}
.modal_titulos
{
    font-size: 11px;	
    font-family: Arial;  
    margin-left: 10px;          
}
.modal_inputs
{
    font-size: 11px;	
    font-family: Arial;       
}
em {
    color:Red;
    font-weight:bold
}
i {   
    color:#b30000; 
	font-size:11px; 
	font-family:Arial;  
}
</style>   
<script type="text/javascript">

    function confirm_delete() {
        if (confirm('¿Esta seguro que desea eliminar el registro seleccionado?') == true)
            return true;
        else
            return false;
    }

    function confirm_cancelar() {
        if (confirm('¿Esta seguro que desea salir del registro sin guardar sus cambios?') == true)
            return true;
        else
            return false;
    }

    function confirm_grabar() {
        if (confirm('¿Está seguro que desea enviar la Solicitud de Ficha de Actualización de datos?\n Una vez enviada no podrá realizar otra solicitud hasta que esta sea validada.') == true)
            return true;
        else
            return false;
    }


    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

      <asp:Panel ID="miPanelFamiliar" runat="server" style="margin: 0; padding: 0; border: 0;">
    <table style="width:720px;height:100%; border: solid 0px red;" border="0" cellpadding="0" cellspacing="0">
        <!--FONDO CABECERA -->           
        <tr>
            <td style="width:520px;height:45px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorcabV2.jpg');background-repeat:no-repeat; ">
            
                <table cellpadding="0" cellspacing="0" border="0" width="520px" style="margin: 0;
                    padding-top: 8px; border: solid 0px red">
                    <tr>
                        <td style="width: 300px" align="left" valign="middle">
<asp:Label ID="lblNombreCompletoFamiliar" runat="server" style="padding-left: 20px; font-size:12px; font-family:Arial; font-weight: bold;" />
                        </td>
                        <td style="width: 100px;" align="right" valign="middle">
                            <asp:ImageButton ID="btnFichaGrabar" runat="server" Width="84" Height="19" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_1.png"
                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_2.png'"
                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_1.png'"
                                ToolTip="Enviar" OnClick="btnFichaGrabar_click" />
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td style="width: 100px;" align="left" valign="middle">
                            <asp:ImageButton ID="btnFichaCancelar" runat="server" Width="84" Height="19" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelarV2_1.png"
                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelarV2_2.png'"
                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelarV2_1.png'"
                                ToolTip="Cancelar" OnClick="btnFichaCancelar_Click" CausesValidation="false" />
                        </td>
                        <td style="width: 10px">
                        </td>
                    </tr>
                </table>
                
            </td>
             <td style="width:200px;height:45px ;">
                &nbsp;
            </td>
        </tr>
        <!--FONDO CUERPO (Aqui se encuentra el menu de opciones y la coleccion de vistas) -->           
        <tr>
            <!--COLECCION DE VIEWS CON LOS GRUPOS DE INFORMACION -->
            <td style="height:100%;width:520px;vertical-align:top;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorfondoV2.jpg');background-repeat:repeat-y;">


                         <table style="height:100%;vertical-align:top;text-align:left;padding-left:20px;width:460px;" border="0" cellpadding="0" cellspacing="0">
                            
                            <!--CABECERA -->
                            <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosPersonales" runat="server" Text="Personales"/>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                &nbsp;&nbsp;
                                </td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:460px;">
                                   
                                    <tr>
                                        <td style="width:460px" align="right" valign="middle" colspan="2">
                                            <span class="camposObligatorios"><i>Campos Obligatorios: (*)</i></span>
                                        </td>
                                    </tr>                                    
                                    
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos" >Apellido Paterno:</span><span class="camposObligatorios">(*)</span>
                                            <asp:HiddenField ID="hidenCodigoFamiliar" runat="server" Value="0" />                                           
                                            <asp:HiddenField ID="hidenCodigoPersona" runat="server" Value="0" />  
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbApellidoPaterno" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerApellidoPaterno" runat="server">
                                                <asp:Image ID="image3" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" align="left" valign="middle">
                                            <span class="titulo_datos">Apellido Materno:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <asp:TextBox ID="tbApellidoMaterno" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>    
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerApellidoMaterno" runat="server">
                                                <asp:Image ID="image4" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" align="left" valign="middle">
                                            <span class="titulo_datos">Nombres:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <asp:TextBox ID="tbNombre" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/> 
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerNombre" runat="server">
                                                <asp:Image ID="image5" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" align="left" valign="middle">
                                            <span class="titulo_datos">Sexo:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width: 140px;" align="left" valign="middle">
                                            <asp:RadioButtonList ID="rbSexo" runat="server" RepeatDirection="Horizontal" style="margin:0;" CssClass="respuesta_input">   
                                                <asp:ListItem Value="2" Selected="True">Masculino</asp:ListItem>                                                                             
                                                <asp:ListItem Value="1">Femenino</asp:ListItem> 
                                            </asp:RadioButtonList>                                                      
                                                    </td>
                                                    <td style="width: 170px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerSexo" runat="server">
                                                <asp:Image ID="image6" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>           
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" align="left" valign="middle">
                                            <span class="titulo_datos">Tipo Documento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>         
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTipoDocumento" runat="server">
                                                <asp:Image ID="image7" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" align="left" valign="middle">
                                            <span class="titulo_datos">Nro. Documento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <asp:TextBox ID="tbNumDocumento" runat="server" Width="250px" MaxLength="12" Height="18px" CssClass="respuesta_input"/>  
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerNumDocumento" runat="server">
                                                <asp:Image ID="image8" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" align="left" valign="middle">
                                            <span class="titulo_datos">Estado Civil:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlEstadoCivil" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>   
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEstadoCivil" runat="server">
                                                <asp:Image ID="image9" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" align="left" valign="middle">
                                            <span class="titulo_datos">Vive:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width: 60px;" align="left" valign="middle">    
                                            <asp:RadioButtonList ID="rbVive" runat="server" RepeatDirection="Horizontal" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="rbVive_SelectedIndexChanged" AutoPostBack="true" style="margin: 0"> 
                                                <asp:ListItem Value="0">No</asp:ListItem>       
                                                <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                            </asp:RadioButtonList>                          
                                                    </td>
                                                    <td style="width: 250px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerVive" runat="server">
                                                <asp:Image ID="image10" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;" align="left" valign="middle">
                                            <span class="titulo_datos">Fecha de defunción:</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                <tr>
                                                    <td align="left" valign="middle" style="width: 110px; height: 25px;">
                                                        <asp:TextBox ID="tbFechaDefuncion" runat="server" Height="18px" CssClass="respuesta_input"/>    
                                                        <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                            TargetControlID="tbFechaDefuncion"
                                                            UserDateFormat="DayMonthYear"                                                                    
                                                            Mask="99/99/9999" 
                                                            MaskType="Date" 
                                                            PromptCharacter="-">
                                                        </atk:MaskedEditExtender>  
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 30px; height: 25px;">
                                                        <asp:ImageButton runat="server" ID="image1" 
                                                            ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de defuncioón." />
                                                        <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                            TargetControlID="tbFechaDefuncion"
                                                            PopupButtonID="image1" 
                                                            Format="dd/MM/yyyy" 
                                                            CssClass="MyCalendar" />
                                                       <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerFechaDefuncion" runat="server">
                                                            <asp:Image ID="image11" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                        </a> 
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 170px; height: 25px;">
                                            <span class="titulo_datos">Formato: 01/12/1980</span>
                                                    </td>
                                                </tr>                                                    
                                                </table>  
                                            
                                        </td>
                                    </tr>                        
                                   </table>
                               </td>
                            </tr>   
                                                     
               
                            <!--CABECERA -->
                            <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosNacimiento" runat="server" Text="Nacimiento" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:460px;">

                                    <tr>
                                        <td style="width:460px" align="right" valign="middle" colspan="2">
                                            <span class="camposObligatorios"><i>Campos Obligatorios: (*)</i></span>
                                        </td>
                                    </tr>  
                                    
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Fecha de Nacimiento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                    <tr>
                                                        <td align="left" valign="middle" style="width: 110px; height: 25px;">
                                                            <asp:TextBox ID="tbFechaNacimiento" runat="server" 
                                                                CssClass="respuesta_input" Height="18px" />    
                                                            <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                                TargetControlID="tbFechaNacimiento"
                                                                UserDateFormat="DayMonthYear"                                                                    
                                                                Mask="99/99/9999" 
                                                                MaskType="Date" 
                                                                PromptCharacter="-">
                                                            </atk:MaskedEditExtender>
                                                        </td>
                                                        <td align="left" valign="middle" style="width: 30px; height: 25px;">
                                                            <asp:ImageButton runat="server" ID="image2" 
                                                                ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de nacimiento." />
                                                            <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                TargetControlID="tbFechaNacimiento"
                                                                PopupButtonID="image2" 
                                                                Format="dd/MM/yyyy" 
                                                                CssClass="MyCalendar" />
                                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerFechaNacimiento" runat="server">
                                                                <asp:Image ID="image12" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                            </a> 
                                                        </td>
                                                        
                                                        <td align="left" valign="middle" style="width: 170px; height: 25px;">
                                                <span class="titulo_datos">Formato: 01/12/1980</span>
                                                        </td>
                                                    </tr>                                                    
                                                </table>   
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Nacionalidad:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlNacionalidad" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>  
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerNacionalidad" runat="server">
                                                <asp:Image ID="image13" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                   </table>
                               </td>
                            </tr>   
                                        
                 
                            <!--CABECERA -->
                            <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosDomicilio"  runat="server" Text= "Domicilio" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:460px;">
                                    <tr>
                                        <td style="width:460px" align="right" valign="middle" colspan="2">
                                            <span class="camposObligatorios"><i>Campos Obligatorios: (*)</i></span>
                                        </td>
                                    </tr>                                      
                                    
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">País:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlDomicilioPais" runat="server" Width="253px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlDomicilioPais_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioPais" runat="server">
                                                <asp:Image ID="image21" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Departamento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlDomicilioDepartamento" runat="server" Width="253px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlDomicilioDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioUbigeo" runat="server">
                                                <asp:Image ID="image44" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Provincia:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlDomicilioProvincia" runat="server" Width="253px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlDomicilioProvincia_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Distrito:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlDomicilioDistrito" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Urbanización:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbDomicilioUrbanizacion" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>                                    
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioUrbanizacion" runat="server">
                                                <asp:Image ID="image25" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Dirección:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbDomicilioDireccion" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioDireccion" runat="server">
                                                <asp:Image ID="image26" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Referencia domiciliaria:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbDomicilioReferencia" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>   
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioReferencia" runat="server">
                                                <asp:Image ID="image27" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Teléfono:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbDomicilioTelefono" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>     
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioTelefono" runat="server">
                                                <asp:Image ID="image28" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">¿Tiene acceso a internet?:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; margin: 0; padding: 0;">
                                                <tr>
                                                    <td style="width: 60px;" align="left" valign="middle">
                                                        <asp:RadioButtonList ID="rbDomicilioAccesoInternet" runat="server" RepeatDirection="Horizontal" CssClass="respuesta_input">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="width: 250px;" align="left" valign="middle">
                                                        <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioAccesoInternet" runat="server">
                                                            <asp:Image ID="image29" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                   </table>
                               </td>
                            </tr>  
                      
                      
                            <!--CABECERA -->
                            <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosLaborales" runat="server" Text="Laborales" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                    <table border="0" cellpadding="0" cellspacing="0" style="width:460px;">
                                    
                                    <tr>
                                        <td style="width:460px" align="right" valign="middle" colspan="2">
                                            <span class="camposObligatorios"><i>Campos Obligatorios: (*)</i></span>
                                        </td>
                                    </tr>                                      
                                    
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos" >Situación Laboral:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlSituacionLaboral" runat="server" Width="253px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlSituacionLaboral_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerSituacionLaboral" runat="server">
                                                <asp:Image ID="image30" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Ocupación / cargo:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbOcupacion" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>                                                    
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerOcupacion" runat="server">
                                                <asp:Image ID="image31" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Centro de Trabajo:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbCentroTrabajo" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerCentroTrabajo" runat="server">
                                                <asp:Image ID="image32" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Dirección de Trabajo:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbTrabajoDireccion" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoDireccion" runat="server">
                                                <asp:Image ID="image33" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">País:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlTrabajoPais" runat="server" Width="253px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlTrabajoPais_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoPais" runat="server">
                                                <asp:Image ID="image34" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Departamento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlTrabajoDepartamento" runat="server" Width="253px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlTrabajoDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoUbigeo" runat="server">
                                                <asp:Image ID="image45" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Provincia:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlTrabajoProvincia" runat="server" Width="253px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlTrabajoProvincia_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Distrito:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlTrabajoDistrito" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Teléfono:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbTrabajoTelefono" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>     
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoTelefono" runat="server">
                                                <asp:Image ID="image22" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Celular:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbTrabajoCelular" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>   
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoCelular" runat="server">
                                                <asp:Image ID="image23" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Servicio Radio:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlTrabajoRadio" runat="server" Width="253px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlTrabajoRadio_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoRadio" runat="server">
                                                <asp:Image ID="image24" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Número Radio:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbTrabajoNumeroRadio" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/> 
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoNumeroRadio" runat="server">
                                                <asp:Image ID="image35" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Correo electrónico:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox ID="tbTrabajoEmail" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>    
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoEmail" runat="server">
                                                <asp:Image ID="image36" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">¿Tiene acceso a internet?:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                        <tr>
                                                            <td style="width: 60px;" align="left" valign="middle">                                                   
                                                    <asp:RadioButtonList ID="rbTrabajoAccesoInternet" runat="server" RepeatDirection="Horizontal" CssClass="respuesta_input"> 
                                                        <asp:ListItem Value="-1" style="display:none;" >Null</asp:ListItem> 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>                                                      
                                                            </td>
                                                            <td style="width: 250px;" align="left" valign="middle">                                                      
                                                    <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTrabajoAccesoInternet" runat="server">
                                                        <asp:Image ID="image37" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                    </a>                                                      
                                                            </td>
                                                        </tr>
                                                    </table>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                   </table>
                               </td>
                            </tr>    
                                     
                                     
                            <!--CABECERA -->
                            <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosEstudios" runat="server" Text="Estudio" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">

                                    <tr>
                                        <td style="width:460px" align="right" valign="middle" colspan="2">
                                            <span class="camposObligatorios"><i>Campos Obligatorios: (*)</i></span>
                                        </td>
                                    </tr>                                     
                                   
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos" >¿Es un Ex-Alumno?:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                        <tr>
                                                            <td style="width: 60px;" align="left" valign="middle">                                                
                                                    <asp:RadioButtonList ID="rbEstudiosExAlumno" runat="server" CssClass="respuesta_input"
                                                        RepeatDirection="Horizontal" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbEstudiosExAlumno_SelectedIndexChanged"> 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>    
                                                            </td>
                                                            <td style="width: 250px;" align="left" valign="middle">                                                     
                                                    <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEstudiosExAlumno" runat="server">
                                                        <asp:Image ID="image38" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                    </a>
                                                            </td>
                                                        </tr>
                                                    </table> 
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Colegio de egreso:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="tbEstudiosColegioEgreso" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEstudiosColegioEgreso" runat="server">
                                                <asp:Image ID="image39" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Año que egreso:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlEstudiosAnioEgreso" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="tbEstudiosAnioEgreso" runat="server" Width="250px" MaxLength="100" Height="18px" Visible="false" />                                                    
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEstudiosAnioEgreso" runat="server">
                                                <asp:Image ID="image40" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Donde continuo estudios:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="tbEstudiosContinuo" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEstudiosContinuo" runat="server">
                                                <asp:Image ID="image41" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Nivel de Instrucción:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlEstudiosNivelInstruccion" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEstudiosNivelInstruccion" runat="server">
                                                <asp:Image ID="image42" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Escolaridad Ministerio:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlEstudiosEscolaridadMinisterio" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEstudiosEscolaridadMinisterio" runat="server">
                                                <asp:Image ID="image43" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:100%;height:3px;" align="left" valign="middle" colspan="2">
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Profesion(es):</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">    
                                        

  <atk:ModalPopupExtender ID="pnModalProfesion" runat="server"
        TargetControlID="btnAgregarDetalleProfesion"
        PopupControlID="pnlProfesion"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="true" 
        OkControlID="OKProfesion" 
        CancelControlID="CancelProfesion"
        Drag="true" 
        PopupDragHandleControlID="ProfesionHeader" />           

    <asp:panel id="pnlProfesion" BackColor="White" BorderColor="Black" runat="server" 
        Style="display: none; border: solid 1px black;">
    
        <table cellpadding="0" cellspacing="0" border="0" width="360px">    
            <tr>
                <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                    <span id="ProfesionHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor:pointer">Agregar Profesión</span>
                </td>
            </tr>
            <tr><td colspan="2" height="10px"></td></tr>
            <tr>
                <td style="width: 130px; height: 25px" align="left" valign="middle">
                    <span class="modal_titulos">Profesión&nbsp;</span>
                </td>
                <td style="width: 230px; height: 25px" align="left">
                    <asp:DropDownList ID="ddlProfesion" runat="server" Width="202px" CssClass="modal_inputs">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                    <asp:ImageButton ID="btnModalAceptarProfesion" runat="server" Width="84" Height="19"
                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" 
                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'" 
                        OnClick="btnModalAceptarProfesion_Click"
                        ToolTip="Aceptar" />&nbsp;
                    <asp:ImageButton ID="btnModalCancelarProfesion" runat="server" Width="84" Height="19"
                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png" 
                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'" 
                        OnClick="btnModalCancelarProfesion_Click"
                        ToolTip="Cancelar" />
                </td>
            </tr>      
            <tr><td colspan="2" height="10px"></td></tr>              
        </table>
        
        <div id="controlProfesion" style="display:none">
            <input type="button" id="OKProfesion" />
            <input type="button" id="CancelProfesion" />
        </div>
       
    </asp:panel>                                            
                                            
                                                     <table cellpadding="0" cellspacing="0" border="0" width="310px">                                                    
                                                        <tr>    
                                                            <td style="width: 30px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                            </td>                                                        
                                                            <td style="width: 250px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                                Profesión                                                                
                                                            </td>
                                                            <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                                <asp:ImageButton ID="btnAgregarDetalleProfesion" runat="server" Width="24" Height="24"
                                                                    ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                                                                    OnClick="btnAgregarDetalleProfesion_Click"                                                    
                                                                    ToolTip="Agregar" Enabled="true"/>                                                                      
                                                            </td>
                                                        </tr> 
                                                          
                                                        <tr>
                                                            <td style="width: 310px; height: 25px" align="center" valign="top" colspan="3">
                                                            
                                                            <asp:UpdatePanel ID="upProfesion" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>                                        
                                                                <div id="miGridviewMantDetalle_Familia">
                                                                <asp:GridView ID="GVListaProfesiones" runat="server" 
                                                                    CssClass="gridview_body"
                                                                    Width="310px"
                                                                    GridLines="None" 
                                                                    AutoGenerateColumns="False"
                                                                    ShowHeader="false"
                                                                    ShowFooter="false"
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false"    
                                                                    EmptyDataText=" - No se encontraron resultados - "
                                                                    OnRowDataBound="GVListaProfesiones_RowDataBound"
                                                                    OnRowCommand="GVListaProfesiones_RowCommand">                          
                                                                    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" /> 
                                                                    <Columns>        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>    
                                                                        
                                                                        <asp:TemplateField HeaderText="Codigo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                        </asp:TemplateField>   
                                                                                                 
                                                                        <asp:TemplateField HeaderText="Descripción">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Left" Width="280px" />
                                                                        </asp:TemplateField> 
                                                                                                       
                                                                    </Columns>
                                                                    
                                                                </asp:GridView>                
                                                                </div>
                                                                <div class="miEspacio"></div>                                            
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            
                                                            </td>                                                        
                                                        </tr>
                                                         
                                                    </table>                                              
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                   </table>
                               </td>
                            </tr>   
                                      
                                      
                            <!--CABECERA -->
                            <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DatosAdicionales" runat="server" Text="Adicionales" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            &nbsp;&nbsp;
                            </td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td>
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0" style="width: 460px;">
                                   
                                    <tr>
                                        <td style="width:460px" align="right" valign="middle" colspan="2">
                                            <span class="camposObligatorios"><i>Campos Obligatorios: (*)</i></span>
                                        </td>
                                    </tr>                                     
                                   
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos" >¿Profesa alguna religión?:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                        <tr>
                                                            <td style="width: 60px;" align="left" valign="middle">   
                                                
                                                    <asp:RadioButtonList ID="rbAdicionalesProfesaReligion" runat="server" CssClass="respuesta_input"
                                                        RepeatDirection="Horizontal" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbAdicionalesProfesaReligion_SelectedIndexChanged" > 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>    
                                                            </td>
                                                            <td style="width: 250px;" align="left" valign="middle">                                                    
                                                    <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAdicionalesProfesaReligion" runat="server">
                                                        <asp:Image ID="image14" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                    </a> 
                                                            </td>
                                                        </tr>
                                                    </table>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Religión:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlAdicionalesReligion" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAdicionalesReligion" runat="server">
                                                <asp:Image ID="image15" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Nombre de la Iglesia:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="tbAdicionalesNombreIglesia" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>                                                         
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAdicionalesNombreIglesia" runat="server">
                                                <asp:Image ID="image16" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>                                    
<tr>
    <td style="width:460px; height: 15px;" valign="middle" colspan="2">
<div style="border-bottom: solid 2px #41576f; width: 460px;"></div>    
    </td>
</tr>                                                                        
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Celular:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="tbAdicionalesCelular" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>     
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAdicionalesCelular" runat="server">
                                                <asp:Image ID="image17" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Servicio Radio:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlAdicionalesRadio" runat="server" Width="253px" CssClass="respuesta_input" 
                                                OnSelectedIndexChanged="ddlAdicionalesRadio_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAdicionalesRadio" runat="server">
                                                <asp:Image ID="image18" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Número Radio:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="tbAdicionalesNumeroRadio" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>  
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAdicionalesNumeroRadio" runat="server">
                                                <asp:Image ID="image19" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
<tr>
    <td style="width:460px; height: 15px;" valign="middle" colspan="2">
<div style="border-bottom: solid 2px #41576f; width: 460px;"></div>    
    </td>
</tr>                                                                            
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Correo electrónico:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="tbAdicionalesEmail" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>  
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAdicionalesEmail" runat="server">
                                                <asp:Image ID="image20" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
<tr>
    <td style="width:460px; height: 15px;" valign="middle" colspan="2">
<div style="border-bottom: solid 2px #41576f; width: 460px;"></div>    
    </td>
</tr>                                                                            
                                    <tr>
                                        <td style="width:100%;height:3px;" align="left" valign="middle" colspan="2">
                                        </td>
                                    </tr>    
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Idiomas:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                            
   <atk:ModalPopupExtender ID="pnModalIdioma" runat="server"
        TargetControlID="btnAgregarDetalleIdioma"
        PopupControlID="pnlIdioma"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="true" 
        OkControlID="OKIdioma" 
        CancelControlID="CancelIdioma"
        Drag="true" 
        PopupDragHandleControlID="IdiomaHeader" />           

    <asp:panel id="pnlIdioma" BackColor="White" BorderColor="Black" runat="server" Style="display: none; border: solid 1px black;">
    
        <table cellpadding="0" cellspacing="0" border="0" width="360px"> 
            <tr>
                <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                    <span id="IdiomaHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor:pointer">Agregar Idioma</span>
                </td>
            </tr>
            <tr><td colspan="2" height="10px"></td></tr>
            <tr>
                <td style="width: 130px; height: 25px" align="left" valign="middle">
                    <span class="titulo_datos" style="padding-left:10px">Idioma&nbsp;</span>
                </td>
                <td style="width: 230px; height: 25px" align="left" valign="middle">
                    <asp:DropDownList ID="ddlIdioma" runat="server" Width="202px" CssClass="modal_inputs">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                    <asp:ImageButton ID="btnModalAceptarIdioma" runat="server" Width="84" Height="19"
                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" 
                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'" 
                        OnClick="btnModalAceptarIdioma_Click"
                        ToolTip="Aceptar" />&nbsp;
                    <asp:ImageButton ID="btnModalCancelarIdioma" runat="server" Width="84" Height="19"
                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png" 
                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'" 
                        OnClick="btnModalCancelarIdioma_Click"
                        ToolTip="Cancelar" />
                </td>
            </tr>      
            <tr><td colspan="2" height="10px"></td></tr>              
        </table>
        <div id="controlIdioma" style="display:none">
            <input type="button" id="OKIdioma" />
            <input type="button" id="CancelIdioma" />
        </div>
       
    </asp:panel>                                            
                                            
                                            
                                                    <table cellpadding="0" cellspacing="0" border="0" width="310px">
                                                    
                                                        <tr> 
                                                            <td style="width: 30px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                            </td>                                                           
                                                            <td style="width: 250px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                                Idioma                                                             
                                                            </td>
                                                            <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                                <asp:ImageButton ID="btnAgregarDetalleIdioma" runat="server" Width="24" Height="24"
                                                                    ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                                                                    OnClick="btnAgregarDetalleIdioma_Click"                                                    
                                                                    ToolTip="Agregar" Enabled="true"/>                                                                      
                                                            </td>
                                                        </tr> 
                                                          
                                                        <tr>
                                                            <td style="width: 310px; height: 25px" align="center" valign="top" colspan="3">
                                                            
                                                            <asp:UpdatePanel ID="upIdioma" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>  
                                                                          
                                                                <div id="miGVMantFichaRegitros" style="width: 310px;">
                                                                <asp:GridView ID="GVListaIdiomas" runat="server" 
                                                                    CssClass="gridview_body"
                                                                    Width="310px"
                                                                    GridLines="None" 
                                                                    AutoGenerateColumns="False"
                                                                    ShowHeader="false"
                                                                    ShowFooter="false"
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false"    
                                                                    EmptyDataText=" - No se encontraron resultados - "
                                                                    OnRowDataBound="GVListaIdiomas_RowDataBound"
                                                                    OnRowCommand="GVListaIdiomas_RowCommand">                           
                                                                    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />    
                                                                    <Columns>         
                                                                        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                            </ItemTemplate>                                                                            
                                                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>    
                                                                        
                                                                        <asp:TemplateField HeaderText="Codigo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                        </asp:TemplateField>   
                                                                                                 
                                                                        <asp:TemplateField HeaderText="Descripción">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="250px"/>
                                                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Left" Width="280px" />
                                                                        </asp:TemplateField> 
                                                                                                       
                                                                    </Columns>
                                                                </asp:GridView>                
                                                                </div>
                                                                
                                                                <div class="miEspacio"></div>                                            
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            
                                                            </td>                                                        
                                                        </tr>
                                                         
                                                    </table>                                              
                                            
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Ficha Autos:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                    
   <atk:ModalPopupExtender ID="pnModalAuto" runat="server"
        TargetControlID="btnAgregarDetalleAuto"
        PopupControlID="pnlAuto"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="true" 
        OkControlID="OKAuto" 
        CancelControlID="CancelAuto"
        Drag="true" 
        PopupDragHandleControlID="AutoHeader" />           

    <asp:panel id="pnlAuto" BackColor="White" BorderColor="Black" runat="server" Style="display: none; border: solid 1px black;">
    
        <table cellpadding="0" cellspacing="0" border="0" width="360px"> 
            <tr>
                <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                    <span id="AutoHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor:pointer">Agregar Auto</span>
                </td>
            </tr>
            <tr><td colspan="2" height="10px"></td></tr>
            <tr>
                <td style="width: 130px; height: 25px" align="left" valign="middle">
                    <span class="titulo_datos" style="padding-left:10px">Placa&nbsp;</span>
                </td>
                <td style="width: 230px; height: 25px" align="left" valign="middle">
                    <asp:TextBox ID="tbPlaca" runat="server" CssClass="modal_inputs" Width="200px" />
                </td>
            </tr>
            <tr>
                <td style="width: 130px; height: 25px" align="left" valign="middle">
                    <span class="titulo_datos" style="padding-left:10px">Marca&nbsp;</span>
                </td>
                <td style="width: 230px; height: 25px" align="left" valign="middle">
                    <asp:TextBox ID="tbMarca" runat="server" CssClass="modal_inputs" Width="200px" />
                </td>
            </tr>   
            <tr>
                <td style="width: 130px; height: 25px" align="left" valign="middle">
                    <span class="titulo_datos" style="padding-left:10px">Módelo&nbsp;</span>
                </td>
                <td style="width: 230px; height: 25px" align="left" valign="middle">
                    <asp:TextBox ID="tbModelo" runat="server" CssClass="modal_inputs" Width="200px" />
                </td>
            </tr>             
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                    <asp:ImageButton ID="btnModalAceptarAuto" runat="server" Width="84" Height="19"
                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" 
                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'" 
                        OnClick="btnModalAceptarAuto_Click"
                        ToolTip="Aceptar" />&nbsp;
                    <asp:ImageButton ID="btnModalCancelarAuto" runat="server" Width="84" Height="19"
                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png" 
                        onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                        onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'" 
                        OnClick="btnModalCancelarAuto_Click"
                        ToolTip="Cancelar" />
                </td>
            </tr>      
            <tr><td colspan="2" height="10px"></td></tr>              
        </table>
        <div id="controlAuto"  style="display:none">
            <input type="button" id="OKAuto" />
            <input type="button" id="CancelAuto" />
        </div>
       
    </asp:panel>                                         
                                    
                                    
                                    
                                                    <table cellpadding="0" cellspacing="0" border="0" width="310px">
                                                    
                                                        <tr> 
                                                            <td style="width: 30px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                            </td>                                                           
                                                            <td style="width: 80px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                                Placa                                                             
                                                            </td>                                                           
                                                            <td style="width: 90px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                                Marca                                                             
                                                            </td>                                                           
                                                            <td style="width: 80px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                                Módelo                                                             
                                                            </td>
                                                            <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                                <asp:ImageButton ID="btnAgregarDetalleAuto" runat="server" Width="24" Height="24"
                                                                    ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                                                                    OnClick="btnAgregarDetalleAuto_Click"                                                    
                                                                    ToolTip="Agregar" Enabled="true"/>                                                                      
                                                            </td>
                                                        </tr> 
                                                          
                                                        <tr>
                                                            <td style="width: 310px; height: 25px" align="center" valign="top" colspan="5">
                                                            
                                                            <asp:UpdatePanel ID="upAuto" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>  
                                                                          
                                                                <div id="miGVMantFichaRegitros" style="width: 310px;">
                                                                <asp:GridView ID="GVListaAutos" runat="server" 
                                                                    CssClass="gridview_body"
                                                                    Width="310px"
                                                                    GridLines="None" 
                                                                    AutoGenerateColumns="False"
                                                                    ShowHeader="false"
                                                                    ShowFooter="false"
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false"    
                                                                    EmptyDataText=" - No se encontraron resultados - "
                                                                    OnRowDataBound="GVListaAutos_RowDataBound"
                                                                    OnRowCommand="GVListaAutos_RowCommand">                           
                                                                    <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />    
                                                                    <Columns>         
                                                                        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                            </ItemTemplate>                                                                            
                                                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>    
                                                                        
                                                                        <asp:TemplateField HeaderText="Codigo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                        </asp:TemplateField>   
                                                                                                 
                                                                        <asp:TemplateField HeaderText="Marca">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Marca") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                                                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Left" Width="80px" />
                                                                        </asp:TemplateField>   
                                                                         
                                                                        <asp:TemplateField HeaderText="Modelo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Modelo") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="90px"/>
                                                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Left" Width="90px" />
                                                                        </asp:TemplateField> 
                                                                        
                                                                        <asp:TemplateField HeaderText="Placa">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Placa") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                                                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Left" Width="80px" />
                                                                        </asp:TemplateField> 
                                                                                                       
                                                                    </Columns>
                                                                </asp:GridView>                
                                                                </div>
                                                                
                                                                <div class="miEspacio"></div>                                            
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            
                                                            </td>                                                        
                                                        </tr>
                                                         
                                                    </table>                                      
                                    
                                        </td>
                                    </tr>                                    
                                    
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                   </table>
                               </td>
                            </tr>    
                                                    
                         </table>
                         
            </td>
            <!--MENU DE GRUPOS DE INFORMACION -->
            <td style="border: solid 0px red; vertical-align:top;width:200px;text-align:center; font-family:Arial;font-size:10px;">
                
            </td>
             
        </tr>  
        <!--FONDO PIE -->                  
        <tr>
            <td style="width: 520px;height:24px; text-align: top; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorfondo_pieV2.jpg'); background-repeat: no-repeat ">
                &nbsp;</td>
            <td style="width: 200px;height:24px; text-align: right; font-family: Arial; font-size: 10px;">
                &nbsp;</td>
        </tr>        
    </table>   
    </asp:Panel>    

</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
