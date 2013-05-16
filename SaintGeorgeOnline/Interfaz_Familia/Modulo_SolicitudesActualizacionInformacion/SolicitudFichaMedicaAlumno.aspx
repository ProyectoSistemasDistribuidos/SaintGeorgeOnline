<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudFichaMedicaAlumno.aspx.vb" Inherits="Interfaz_Familia_Modulo_SolicitudesActualizacionInformacion_SolicitudFichaMedicaAlumno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    
    <!--Archivos de Javascripts -->
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/sexyalertbox.v1.2.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.blockUI.js"></script>       
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jcarousellite_1.0.1c4.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.sexylightbox.v2.3.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.colorbox.js"></script>
    
	<!--Archivos de Estilos -->
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/styleAlertas.css" />   
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/sexyalertbox.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/misEstilos.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/sexylightbox.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/colorbox.css" />
    <link rel="stylesheet" type="text/css" href="/SaintGeorgeOnline/App_Themes/Estilos/styles_master.css"  />     
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/miCalendario.css" />  
    
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

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <atk:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
    </atk:ToolkitScriptManager>

<div id="page_effect"  >

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<asp:HiddenField ID="hiddenCodigoFamilia" runat="server" />
<asp:HiddenField ID="hiddenCodigoAlumno" runat="server" Value="" />                                  
<asp:HiddenField ID="hiddenCodigoAnioAcademico" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoFamiliar" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoNivel" runat="server" Value="0" />
<asp:HiddenField ID="hiddenCodigoGrado" runat="server" Value="0" />
<asp:HiddenField ID="hiddenNivel" runat="server" Value="0" />
<asp:HiddenField ID="hiddenGrado" runat="server" Value="0" />       
<asp:HiddenField ID="hiddenNombreCompleto" runat="server" Value="0" />      
<asp:HiddenField ID="hiddenFoto" runat="server" Value="0" />      
    
    <table style="width:720px;height:100%; border: solid 0px red;" border="0" cellpadding="0" cellspacing="0">
        <!--FONDO CABECERA -->           
        <tr>
            <td style="width:520px;height:45px ;background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorcabV2.jpg');background-repeat:no-repeat; ">
               
                <table cellpadding="0" cellspacing="0" border="0" width="520px" style="margin: 0;padding-top:8px; border: solid 0px red">
                    <tr>
                        <td style="width: 300px" align="left" valign="middle">
<asp:Label ID="lblNombreCompletoAlumno" runat="server" style="padding-left: 20px; font-size:12px; font-family:Arial; font-weight: bold;" />                          
                        </td>
                        <td style="width: 100px;" align="right" valign="middle">
                            <asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_1.png"
                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_2.png'"
                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_1.png'"
                                ToolTip="Enviar" OnClick="btnFichaGrabar_click" />
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td style="width: 100px;" align="left" valign="middle">
                           
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
                                        <asp:Label ID="lbl_Bloque_DatosAlumno" runat="server" Text="Generales" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td align="left" valign="top" style="width: 100%">
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0" style="width:460px;">
                                    
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Nombre Completo:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">  
                                            <span class="respuesta_datos">                                                    
                                                <asp:Label  Text="" ID="lblNombreAlumno" runat="server" />
                                            </span>
                                            <asp:HiddenField ID="hd_Codigo" runat="server" />
                                            <asp:HiddenField ID="hd_CodigoPersona" runat="server" Value="0" />  
                                            <asp:HiddenField ID="hd_CodigoPersonaSolicitante" runat="server" /> 
                                            <asp:HiddenField ID="hd_GradoActual" runat="server" />  
                                        </td>
                                    </tr>  
                                 <%--   <tr>
                                        <td style="width: 150px;" align="left">
                                            <span class="titulo_datos">Sede:</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                                <asp:Label ID="lblSede" runat="server" Text="-" />   
                                            </span>                                                         
                                        </td>
                                    </tr>   --%> 
                                    <tr>
                                        <td style="width: 150px;" align="left">
                                            <span class="titulo_datos">Estado/Año Académico:</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                                <asp:Label ID="lblSituacionAnio" runat="server" />                                                        
                                            </span>
                                        </td>
                                    </tr>    
                                    <tr>
                                        <td style="width: 150px;" align="left">
                                            <span class="titulo_datos">Nivel/SubNivel/Grado/Aula:</span>
                                        </td>
                                        <td style="width: 310px;" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                                <asp:Label ID="lblENSnGS"  runat="server" />                                                    
                                            </span>
                                        </td>
                                    </tr>                        
                                   </table>
                               </td>
                            </tr>  
                                   
                                   
                            <!--CABECERA -->
                            <tr>
                                <td style="width:100%;" align="left" valign="middle">
                                    <span class="titulo_Bloques">
                                        <asp:Label ID="lbl_Bloque_DesarrolloInfantil" runat="server" Text="Desarrollo Infantil" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:460px" align="left" valign="middle">
                                    <span class="camposObligatorios"><i>Para los alumnos de inicial es obligatorio ingresar los <b>Datos de Desarrollo Infantil</b> </i></span>
                                </td>
                            </tr> 
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td align="left" valign="top" style="width: 100%">
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">
                                   
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Tipo de Nacimiento:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlTipoNacimiento" runat="server" Width="150px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTipoNacimiento" runat="server">
                                                <asp:Image ID="image3" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>                                              
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Observaciones:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="tbObservaciones" runat="server" Width="280px" CssClass="respuesta_input"
                                                Height="35px" Rows="3" TextMode="MultiLine" />                                            
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerObservaciones" runat="server">
                                                <asp:Image ID="image1" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>      
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="tbObservaciones" Enabled="True"
                                                ValidChars="' ','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿A qué edad levantó la cabeza?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:310px; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width:45px;" align="left" valign="middle">
                                                        <asp:DropDownList ID="ddlEdadLevCabeza" runat="server" Width="45px" CssClass="respuesta_input">                                            
                                                        </asp:DropDownList>  
                                                    </td>
                                                    <td style="width:40px;" align="left" valign="middle">
                                                        <asp:Label ID="Label1" Text="Año(s)" runat="server" CssClass="titulo_datos" />
                                                    </td>
                                                    <td style="width:20px;" align="left" valign="middle">
                                                        <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEdadLevCabeza" runat="server">
                                                            <asp:Image ID="image2" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                        </a>  
                                                    </td>
                                                    <td style="width:45px;" align="left" valign="middle">
                                                        <asp:DropDownList ID="ddlMesesLevCabeza" runat="server" Width="45px" CssClass="respuesta_input">
                                                        </asp:DropDownList>
                                                    </td> 
                                                    <td style="width:45px;" align="left" valign="middle">
                                                        <asp:Label ID="Label2" Text ="Mes(es)" runat="server" CssClass="titulo_datos" />
                                                    </td>
                                                    <td style="width:115px;" align="left" valign="middle">
                                                        <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerMesesLevCabeza" runat="server">
                                                            <asp:Image ID="image8" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        
                                        </td>
                                    </tr>     
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿A qué edad se sentó?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                        
                                         <table cellpadding="0" cellspacing="0" border="0" style="width:310px; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width:45px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlEdadSento" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList>       
                                                    </td>
                                                    <td style="width:40px;" align="left" valign="middle">                                                     
                                            <asp:Label ID="lblAñoSento" Text="Año(s)" runat="server" CssClass="titulo_datos" />
                                                    </td>
                                                    <td style="width:20px;" align="left" valign="middle">                                                    
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEdadSento" runat="server">
                                                <asp:Image ID="image18" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                                    </td>
                                                    <td style="width:45px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlMesesSento" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList>   
                                                    </td> 
                                                    <td style="width:45px;" align="left" valign="middle">                                                         
                                            <asp:Label ID="lblMesesSento" Text="  Mes(es) " runat="server" CssClass="titulo_datos" />
                                                    </td>
                                                    <td style="width:115px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerMesesSento" runat="server">
                                                <asp:Image ID="image19" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                                    </td>
                                                </tr>
                                            </table>                                    
                                    
                                        </td>
                                    </tr> 
                                      
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿A qué edad se paró?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                        
                                        <table cellpadding="0" cellspacing="0" border="0" style="width:310px; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width:45px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlEdadParo" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList> 
                                                    </td>
                                                    <td style="width:40px;" align="left" valign="middle">                                                                                              
                                            <asp:Label ID="lblAñoParo" Text="Año(s)" runat="server" CssClass="titulo_datos" />
                                                    </td>
                                                    <td style="width:20px;" align="left" valign="middle">     
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEdadParo" runat="server">
                                                <asp:Image ID="image22" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                    </td>
                                                    <td style="width:45px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlMesesParo" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                                    </td> 
                                                    <td style="width:45px;" align="left" valign="middle">   
                                            <asp:Label ID="lblMesesParo" Text="Mes(es)" runat="server" CssClass="titulo_datos" />
                                                    </td>
                                                    <td style="width:115px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerMesesParo" runat="server">
                                                <asp:Image ID="image23" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>    
                                                    </td>
                                                </tr>
                                            </table>  
                                    
                                        </td>
                                    </tr>                                  
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿A qué edad caminó?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                        
                                        <table cellpadding="0" cellspacing="0" border="0" style="width:310px; margin:0; padding:0;">
                                            <tr>
                                                <td style="width:45px;" align="left" valign="middle">      
                                            <asp:DropDownList ID="ddlEdadCamino" runat="server" Width="45px" CssClass="respuesta_input">                                            
                                            </asp:DropDownList>
                                                </td>
                                                <td style="width:40px;" align="left" valign="middle">                                                     
                                            <asp:Label ID="lblAñoCamino" Text="Año(s)" runat="server" CssClass="titulo_datos" />
                                                </td>
                                                <td style="width:20px;" align="left" valign="middle"> 
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEdadCamino" runat="server">
                                                <asp:Image ID="image9" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                </td>
                                                <td style="width:45px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlMesesCamino" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                                </td> 
                                                <td style="width:45px;" align="left" valign="middle">
                                            <asp:Label ID="lblMesesCamino" Text="Mes(es)" runat="server" CssClass="titulo_datos" />
                                                </td>
                                                <td style="width:115px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerMesesCamino" runat="server">
                                                <asp:Image ID="image10" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                </td>
                                            </tr>
                                        </table> 
                                        
                                        </td>
                                    </tr>
                                                                            
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿A qué edad controló sus esfínteres?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width:310px; margin:0; padding:0;">
                                            <tr>
                                                <td style="width:45px;" align="left" valign="middle"> 
                                            <asp:DropDownList ID="ddlEdadControloEsfinteres" runat="server" Width="45px" CssClass="respuesta_input">                                                            
                                            </asp:DropDownList>
                                                </td>
                                                <td style="width:40px;" align="left" valign="middle">
                                            <asp:Label ID="lblAñoControloEsfinteres" Text="Año(s)" runat="server" CssClass="titulo_datos" />
                                                </td>
                                                <td style="width:20px;" align="left" valign="middle"> 
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEdadControloEsfinteres" runat="server">
                                                <asp:Image ID="image11" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                </td>
                                                <td style="width:45px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlMesesControloEsfinteres" runat="server" Width="45px" CssClass="respuesta_input">                                                     
                                            </asp:DropDownList>
                                                </td> 
                                                <td style="width:45px;" align="left" valign="middle">
                                            <asp:Label ID="lblMesesControloEsfinteres" Text="Mes(es)" runat="server" CssClass="titulo_datos" />
                                                </td>
                                                <td style="width:115px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerMesesControloEsfinteres" runat="server">
                                                <asp:Image ID="image12" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                </td>
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>  
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿A que edad pronunció las primeras palabras?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                        
                                        <table cellpadding="0" cellspacing="0" border="0" style="width:310px; margin:0; padding:0;">                                        
                                            <tr>
                                                <td style="width:45px;" align="left" valign="middle"> 
                                            <asp:DropDownList ID="ddlEdadHabloPrimerasPalabras" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                                </td>
                                                <td style="width:40px;" align="left" valign="middle">
                                            <asp:Label ID="lblAñoHabloPrimerasPalabras" Text="Año(s)" runat="server" CssClass="titulo_datos" />
                                                </td>
                                                <td style="width:20px;" align="left" valign="middle">  
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEdadHabloPrimerasPalabras" runat="server">
                                                <asp:Image ID="image13" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                </td>
                                                <td style="width:45px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlMesesHabloPrimerasPalabras" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                                </td> 
                                                <td style="width:45px;" align="left" valign="middle"> 
                                            <asp:Label ID="lblMesesHabloPrimerasPalabras" Text ="Mes(es)" runat="server" CssClass="titulo_datos" />
                                                </td>
                                                <td style="width:115px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerMesesHabloPrimerasPalabras" runat="server">
                                                <asp:Image ID="image14" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                </td>
                                            </tr>
                                        </table> 
                                            
                                        </td>
                                    </tr>  
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿A que edad se comunicó con fluídez?:</span>
                                        </td>
                                        <td align="left" valign="middle">  
                                        
                                        <table cellpadding="0" cellspacing="0" border="0" style="width:310px; margin:0; padding:0;">                                        
                                            <tr>
                                                <td style="width:45px;" align="left" valign="middle"> 
                                            <asp:DropDownList ID="ddlEdadHabloFluidez" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                                </td>
                                                <td style="width:40px;" align="left" valign="middle">
                                            <asp:Label ID="lblAñoHabloFluidez" Text="Año(s)" runat="server" CssClass="titulo_datos" />
                                                </td>
                                                    <td style="width:20px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerEdadHabloFluidez" runat="server">
                                                <asp:Image ID="image15" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                </td>
                                                <td style="width:45px;" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlMesesHabloFluidez" runat="server" Width="45px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                                </td> 
                                                <td style="width:45px;" align="left" valign="middle">
                                            <asp:Label ID="lblMesesHabloFluidez" Text="Mes(es)" runat="server" CssClass="titulo_datos" />
                                                </td>
                                                <td style="width:115px;" align="left" valign="middle">
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerMesesHabloFluidez" runat="server">
                                                <asp:Image ID="image16" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
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
                                        <asp:Label ID="lbl_Bloque_EstadoSalud" runat="server" Text="Estado Salud" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td align="left" valign="top" style="width: 100%">
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">
                                     <tr>
                                        <td style="width:460px" align="right" valign="middle" colspan="2">
                                            <span class="camposObligatorios"><i>Campos Obligatorios: (*)</i></span>
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">Tipo de Sangre:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:DropDownList ID="ddlTipoSangre" runat="server" Width="121px" Height="18px"  CssClass="respuesta_input">
                                            </asp:DropDownList>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTipoSangre" runat="server">
                                                <asp:Image ID="image17" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Enfermedades:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">

                                            <atk:ModalPopupExtender ID="pnModalEnfermedad" runat="server" 
                                                TargetControlID="btnMostrarEnfermedad"
                                                PopupControlID="pnl_PopUp_Enfermedad" 
                                                BackgroundCssClass="MiModalBackground"
                                                OkControlID="OKVacuna" 
                                                CancelControlID="CancelVacuna" 
                                                DynamicServicePath=""
                                                Enabled="True">
                                            </atk:ModalPopupExtender>
                                            <asp:Panel ID="pnl_PopUp_Enfermedad" BackColor="White" BorderColor="Black" runat="server" style="display:none">
                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                    <tr>
                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                                                            <span id="EnfermedadHeader">Agregar Enfermedad</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Enfermedad&nbsp;</span>
                                                            <asp:HiddenField ID="hidencodigoEnfermedad" runat="server" />
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlEnfermedad" runat="server" Width="202px" CssClass="modal_inputs">
                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Edad&nbsp;</span>
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:TextBox ID="tbEdad" runat="server" CssClass="modal_inputs" Text="0" Width="50px" />
                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers"
                                                                TargetControlID="tbEdad" Enabled="True">
                                                            </atk:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                            <asp:ImageButton ID="popup_btnAgregar_Enfermedad" runat="server" Width="84px" Height="19px"
                                                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'"
                                                                OnClick="popup_btnAgregar_Enfermedad_Click" ToolTip="Aceptar" />&nbsp;
                                                            <asp:ImageButton ID="popup_btnCancelar_Enfermedad" runat="server" Width="84px" Height="19px"
                                                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                                                                OnClick="popup_btnCancelar_Enfermedad_Click" ToolTip="Cancelar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="controlEnfermedad" style="display: none">
                                                    <input type="button" id="okEnfermedad" />
                                                    <input type="button" id="CancelEnfermedad" />
                                                </div>
                                            </asp:Panel>
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>                               
                                                    <td style="width: 60px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                    </td>
                                                    <td style="width: 185px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Enfermedad
                                                    </td>
                                                    <td style="width: 185px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Edad
                                                    </td>
                                                    <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_Enfermedad" runat="server" Width="20px" Height="20px"
                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btn_Add_Enfermedad_Click"
                                                            ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="4">
                                                        <asp:UpdatePanel ID="upEnfermedad" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                
                                                                    <asp:GridView ID="gvDetalleEnfermedad" runat="server" 
                                                                        CssClass="gridview_body"
                                                                        Width="460px"                                                                      
                                                                        GridLines="None" 
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False" 
                                                                        AllowSorting="False"                                                                        
                                                                        ShowHeader="False" 
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleEnfermedad_RowDataBound"
                                                                        OnRowCommand="gvDetalleEnfermedad_RowCommand">
                                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                        Visible="true" CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>'
                                                                                        ToolTip="Actualizar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                        Visible="true" CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>'
                                                                                        ToolTip="Eliminar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CodigoEnfermedad">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoEnfermedad" runat="server" Text='<%# Bind("CodigoEnfermedad") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEnfermedad" runat="server" Text='<%# Bind("Enfermedad") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" Width="200px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEdadEnfermedad_grilla" runat="server" Text='<%# Bind("Edad") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" Width="200px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                               
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnMostrarEnfermedad" runat="server" Text="Button" />
                                            </div>
                                         
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Vacunas:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                            
                                            <atk:ModalPopupExtender ID="pnModalVacuna" runat="server" TargetControlID="btnMostrarVacuna"
                                                PopupControlID="pnl_PopUp_Vacuna" BackgroundCssClass="MiModalBackground" 
                                                 OkControlID="OKVacuna" CancelControlID="CancelVacuna" DynamicServicePath=""
                                                Enabled="True">
                                            </atk:ModalPopupExtender>
                                            <asp:Panel ID="pnl_PopUp_Vacuna" BackColor="White" BorderColor="Black" runat="server" style="display:none">
                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                    <tr>
                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                                                            <span id="VacunaHeader">Agregar Vacuna</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Fecha de Vacunación :&nbsp;</span>
                                                            <asp:HiddenField ID="hidencodigoVacuna" runat="server" />
                                                        </td>
                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                <tr>
                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                        <asp:TextBox ID="tbFechaVacunacion" runat="server" CssClass="modal_inputs"/>
                                                                    </td>
                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                        <asp:ImageButton runat="server" ID="imageBF" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/calendar_icon.png"
                                                                            AlternateText="Elija una fecha del calendario" />
                                                                        <atk:CalendarExtender ID="calendario" runat="server" TargetControlID="tbFechaVacunacion"
                                                                            PopupButtonID="imageBF" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Vacuna : &nbsp;</span>
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlTipoVacuna" runat="server" Width="200px" CssClass="modal_inputs">
                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Dosis :&nbsp;</span>
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlDosis" runat="server" Width="200px" CssClass="modal_inputs">
                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                            <asp:ImageButton ID="popup_btnAgregar_Vacuna" runat="server" Width="84px" Height="19px"
                                                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'"
                                                                OnClick="popup_btnAgregar_Vacuna_Click" ToolTip="Aceptar" />&nbsp;
                                                            <asp:ImageButton ID="popup_btnCancelar_Vacuna" runat="server" Width="84px" Height="19px"
                                                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                                                                OnClick="popup_btnCancelar_Vacuna_Click" ToolTip="Cancelar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="controlVacuna" style="display: none">
                                                    <input type="button" id="OKVacuna" />
                                                    <input type="button" id="CancelVacuna" />
                                                </div>
                                            </asp:Panel>
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>
                                                    <td style="width: 60px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                    </td>
                                                    <td style="width: 100px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Fecha de Vacunación
                                                    </td>
                                                    <td style="width: 140px; height: 26px;" align="center" valign="middle" class="gridview_header">                                                       
                                                        Vacuna
                                                    </td>
                                                     <td style="width: 130px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Dosis
                                                    </td>
                                                    <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_Vacuna" runat="server" Width="20px" Height="20px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                            OnClick="btn_Add_Vacuna_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="5">
                                                        <asp:UpdatePanel ID="upVacuna" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                              
                                                                    <asp:GridView ID="gvDetalleVacuna" runat="server" 
                                                                        CssClass="gridview_body"
                                                                        GridLines="None" 
                                                                        Width="460px"
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False" 
                                                                        AllowSorting="False"                                                                         
                                                                        ShowHeader="False" 
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleVacuna_RowDataBound"
                                                                        OnRowCommand="gvDetalleVacuna_RowCommand">
                                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_actualizar.png"
                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Actualizar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png"
                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Eliminar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CodigoVacuna">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoVacuna" runat="server" Text='<%# Bind("CodigoVacuna") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoDosis" runat="server" Text='<%# Bind("CodigoDosis") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFechaVacunacion" runat="server" Text='<%# Bind("FechaVacunacion") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" Width="100px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVacuna" runat="server" Text='<%# Bind("Vacuna") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" Width="150px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDosis" runat="server" Text='<%# Bind("Dosis") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" Width="150px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                               
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnMostrarVacuna" runat="server" Text="Button" />
                                            </div>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Alergias:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                            
                                            <atk:ModalPopupExtender ID="pnModalAlergia" runat="server" TargetControlID="btnMostrarAlergia"
                                                PopupControlID="pnl_PopUp_Alergia" BackgroundCssClass="MiModalBackground" 
                                                OkControlID="OKAlergia" CancelControlID="CancelAlergia" DynamicServicePath=""
                                                Enabled="True">
                                            </atk:ModalPopupExtender>
                                            <asp:Panel ID="pnl_PopUp_Alergia" BackColor="White" BorderColor="Black" runat="server" style="display:none">
                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                    <tr>
                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                                                            <span id="AlergiaHeader">Agregar Alergia</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Tipo de Alergía :  &nbsp;</span>
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlTipoAlergia" runat="server" Width="200px" OnSelectedIndexChanged="ddlTipoAlergia_SelectedIndexChanged"
                                                                AutoPostBack="true" CssClass="modal_inputs">
                                                            </asp:DropDownList>                                                                                                            
                                                        </td>                                                                                    
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Alergías : &nbsp;</span>
                                                            <asp:HiddenField ID="hidencodigoAlergia" runat="server" />
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlAlergia" runat="server" Width="200px" CssClass="modal_inputs">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                            <asp:ImageButton ID="popup_btnAgregar_Alergia" runat="server" Width="84px" Height="19px"
                                                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'"
                                                                OnClick="popup_btnAgregar_Alergia_Click" ToolTip="Aceptar" />&nbsp;
                                                            <asp:ImageButton ID="popup_btnCancelar_Alergia" runat="server" Width="84px" Height="19px"
                                                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                                                                OnClick="popup_btnCancelar_Alergia_Click" ToolTip="Cancelar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="controlAlergia" style="display: none">
                                                    <input type="button" id="OKAlergia" />
                                                    <input type="button" id="CancelAlergia" />
                                                </div>
                                            </asp:Panel>
                                                                                        
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>                               
                                                    <td style="width: 60px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                    </td>
                                                    <td style="width: 185px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Alergías
                                                    </td>
                                                    <td style="width: 185px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Tipo de Alergías
                                                   </td>
                                                   <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_Alergia" runat="server" Width="20px" Height="20px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                            OnClick="btn_Add_Alergia_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="4">
                                                        <asp:UpdatePanel ID="upAlergia" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                
                                                                    <asp:GridView ID="gvDetalleAlergia" runat="server" 
                                                                        CssClass="gridview_body"
                                                                        GridLines="None" 
                                                                        width="460px"
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False" 
                                                                        AllowSorting="False" 
                                                                        ShowHeader="False"                                                                         
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleAlergia_RowDataBound"
                                                                        OnRowCommand="gvDetalleAlergia_RowCommand">
                                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_actualizar.png"
                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Actualizar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png"
                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Eliminar Registro" />
                                                                                </ItemTemplate>
                                                                               <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CodigoAlergia">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoAlergia" runat="server" Text='<%# Bind("CodigoAlergia") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoTipoAlergia" runat="server" Text='<%# Bind("CodigoTipoAlergia") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAlergia" runat="server" Text='<%# Bind("Alergia") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" Width="200px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTipoAlergia" runat="server" Text='<%# Bind("TipoAlergia") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" Width="200px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnMostrarAlergia" runat="server" Text="Button" />
                                            </div>
                                            
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Caracteristicas de la Piel:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                        
                                            <atk:ModalPopupExtender ID="pnModalCaracteristicasPiel" runat="server" TargetControlID="btnMostrarCaracteristicaPiel"
                                                PopupControlID="pnl_PopUp_CaracteristicasPiel" BackgroundCssClass="MiModalBackground"
                                               OkControlID="OKCaracteristicaPiel" CancelControlID="CancelCaracteristicaPiel"
                                                DynamicServicePath="" Enabled="True">
                                            </atk:ModalPopupExtender>
                                            <asp:Panel ID="pnl_PopUp_CaracteristicasPiel" BackColor="White" BorderColor="Black" runat="server" style="display:none">
                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                    <tr>
                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                                                            <span id="Span1">Agregar Caracteristicas de la Piel</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Tipo de carácteristicas :</span>
                                                            <asp:HiddenField ID="hdCodigoCaracteristicasPiel" runat="server" />
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlCaracteristicaPiel" runat="server" Width="200px" CssClass="modal_inputs">
                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                            <asp:ImageButton ID="popup_btnAgregar_CaracteristicaPiel" runat="server" Width="84px"
                                                                Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png"
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'"
                                                                OnClick="popup_btnAgregar_CaracteristicaPiel_Click" ToolTip="Aceptar" />&nbsp;
                                                            <asp:ImageButton ID="popup_btnCancelar_CaracteristicaPiel" runat="server" Width="84px"
                                                                Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png"
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                                                                OnClick="popup_btnCancelar_CaracteristicaPiel_Click" ToolTip="Cancelar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="Div2" style="display: none">
                                                    <input type="button" id="OKCaracteristicaPiel" />
                                                    <input type="button" id="CancelCaracteristicaPiel" />
                                                </div>
                                            </asp:Panel>
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>                               
                                                    <td style="width: 60px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                    </td>
                                                    <td style="width: 370px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Tipo de carácteristicas en la Piel
                                                    </td>
                                                    <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_CaracteristicasPiel" runat="server" Width="20px" Height="20px"
                                                            ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                            OnClick="btn_Add_CaracteristicasPiel_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="3">
                                                        <asp:UpdatePanel ID="upCaracteristicaPiel" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            
                                                                    <asp:GridView ID="gvDetalleCaracteristicaPiel" runat="server" 
                                                                        CssClass="gridview_body"
                                                                        GridLines="None" 
                                                                        width="460px"
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False" 
                                                                        AllowSorting="False"                                                                        
                                                                        ShowHeader="False" 
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleCaracteristicaPiel_RowDataBound"
                                                                        OnRowCommand="gvDetalleCaracteristicaPiel_RowCommand">
                                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_actualizar.png"
                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Actualizar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png"
                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Eliminar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CodigoCaracteristicapiel">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoCaracteristicapiel" runat="server" Text='<%# Bind("CodigoCaracteristicapiel") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCaracteristicaPiel" runat="server" Text='<%# Bind("CaracteristicaPiel") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="400px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnMostrarCaracteristicaPiel" runat="server" Text="Button" />
                                            </div>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Medicamentos:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                              <atk:ModalPopupExtender ID="pnModalMedicamentos" runat="server" TargetControlID="btnMostrarMedicamentos"
                                                    PopupControlID="pnl_PopUp_Medicamentos" BackgroundCssClass="MiModalBackground"
                                                     Drag="True" OkControlID="OKMedicamentos" CancelControlID="CancelMedicamentos"
                                                    DynamicServicePath="" Enabled="True" PopupDragHandleControlID="MedicamentosHeader" />
                                              <asp:Panel ID="pnl_PopUp_Medicamentos" BackColor="White" BorderColor="Black" runat="server">
                                                 <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                                <tr id="MedicamentosHeader" style="cursor: pointer;">
                                                                    <td style="width: 460px; height: 26px" colspan="4" align="center" class="miGVBusquedaFicha_Header">
                                                                        <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                            Agregar Medicamentos</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" height="10px" width="460px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 80px; height: 26px" align="left" valign="middle">
                                                                        <span style="padding-left: 10px">Medicamento :&nbsp;</span>
                                                                        <asp:HiddenField ID="hidencodigoMedicamento" runat="server" />
                                                                    </td>
                                                                    <td colspan="3" style="width: 380px; height: 26px;" align="left" valign="middle">
                                                                        <asp:DropDownList ID="ddlMedicamento" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 80px; height: 26px" align="left" valign="middle">
                                                                        <span style="padding-left: 10px">Presentación :&nbsp;</span>
                                                                    </td>
                                                                    <td style="width: 180px; height: 26px" align="left" valign="middle">
                                                                        <asp:DropDownList ID="ddlPresentacion" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td colspan="2" style="width: 200px; height: 26px" align="left" valign="middle">
                                                                        <table cellpadding="0" cellspacing="0" border="0" width="300px">
                                                                            <tr>
                                                                                <td style="width: 200px; height: 26px" align="left" valign="middle">
                                                                                    <span style="padding-left: 10px">Cantidad de la presentación : &nbsp;</span>
                                                                                </td>
                                                                                <td style="width:100px; height: 26px" align="left" valign="middle">
                                                                                    <asp:TextBox ID="tbCantidadPres" runat="server" CssClass="miTextBox" Width="80px"
                                                                                        MaxLength="150" Height="18px" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 80px; height: 26px" align="left" valign="middle">
                                                                        <span style="padding-left: 10px">Dosis :&nbsp;</span>
                                                                    </td>
                                                                    <td style="width: 180px; height: 51px" align="left" valign="middle">
                                                                        <asp:TextBox ID="tbDosisMedi" runat="server" CssClass="miTextBox" Width="170px" Height="35px"
                                                                            Rows="2" TextMode="MultiLine" />
                                                                    </td>
                                                                    <td style="width: 80px; height: 26px" align="left" valign="middle">
                                                                        <span style="padding-left: 10px">Observaciones :&nbsp;</span>
                                                                    </td>
                                                                    <td style="padding-right: 10px; width: 120px; height: 51px" align="left" valign="middle">
                                                                        <asp:TextBox ID="tbObservacionMedi" runat="server" CssClass="miTextBox" Width="170px"
                                                                            Height="35px" Rows="2" TextMode="MultiLine" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 460px; height: 26px" align="center" valign="middle" colspan="4">
                                                                        <asp:ImageButton ID="popup_btnAgregar_Medicamentos" runat="server" Width="84px" Height="19px"
                                                                            ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_Medicamentos_Click"
                                                                            ToolTip="Aceptar" />&nbsp;
                                                                        <asp:ImageButton ID="popup_btnCancelar_Medicamentos" runat="server" Width="84px"
                                                                            Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_Medicamentos_Click"
                                                                            ToolTip="Cancelar" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" height="5px" width=" 460px">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                <div id="controlMedicamentos" style="display: none">
                                                    <input type="button" id="OKMedicamentos" />
                                                    <input type="button" id="CancelMedicamentos" />
                                                </div>
                                            </asp:Panel>
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>                               
                                                    <td style="width: 160px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                    Medicamento</td>
                                                    <td style="width: 80px; height: 26px;" align="left" valign="middle" class="gridview_header">
                                                        Presentación / Cantidad
                                                    </td>
                                                    <td style="width: 90px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Dosis
                                                    </td>
                                                    <td style="width: 100px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Observaciones
                                                    </td>
                                                    <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_Medicamentos" runat="server" Width="20px" Height="20px"
                                                            ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                            OnClick="btn_Add_Medicamentos_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="5">
                                                        <asp:UpdatePanel ID="upMedicamentos" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                
                                                                    <asp:GridView ID="gvDetalleMedicamento" runat="server" 
                                                                        CssClass="gridview_body"
                                                                        GridLines="Both" 
                                                                        width="460px"
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False" 
                                                                        AllowSorting="False"                                                                         
                                                                        ShowHeader="False" 
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleMedicamento_RowDataBound"
                                                                        OnRowCommand="gvDetalleMedicamento_RowCommand">
                                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>'
                                                                                        ToolTip="Actualizar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="20px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>'
                                                                                        ToolTip="Eliminar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="20px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CodigoMedicamento">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoMedicamento" runat="server" Text='<%# Bind("CodigoMedicamento") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoPresentacion" runat="server" Text='<%# Bind("CodigoPresentacion") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCantidadPresentacion" runat="server" Text='<%# Bind("CantidadPresentacion") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMedicamento" runat="server" Text='<%# Bind("Medicamento") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="120px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPresentCant" runat="server" Text='<%# Bind("PresentCant") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="80px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDosisMedicamento" runat="server" Text='<%# Bind("DosisMedicamento") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="80px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblObservaciones" runat="server" Text='<%# Bind("Observaciones") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="140px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnMostrarMedicamentos" runat="server" Text="Button" />
                                            </div>
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
                                        <asp:Label ID="lbl_Bloque_OtrosDatosMedicos" runat="server" Text="Datos Médicos" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td align="left" valign="top" style="width: 100%">
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">
                                   
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle">
                                            <span class="titulo_datos">¿Tiene el Tabique desviado?:</span>
                                        </td>
                                       <td style="width:310px" align="left" valign="middle">
                                             
                                                    <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                        <tr>
                                                            <td style="width: 60px;" align="left" valign="middle"> 
                                                                                                            
                                                            <asp:RadioButtonList ID="rbTabiqueDesviado" runat="server" RepeatDirection="Horizontal" CssClass="respuesta_input">
                                                                <asp:ListItem Value="1" Text="Si" />
                                                                <asp:ListItem Value="0" Text="No" Selected="True" />                                                                              
                                                            </asp:RadioButtonList> 
                                                    
                                                            </td>
                                                            <td style="width: 250px;" align="left" valign="middle"> 
                                                            
                                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTabiqueDesviado" runat="server">
                                                                <asp:Image ID="image5" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                            </a>   
                                                    
                                                            </td>
                                                        </tr>
                                                    </table>                                                                                                             
                                               
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿Ha presentado Sangrado Nasal?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            
                                             <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                        <tr>
                                                            <td style="width: 60px;" align="left" valign="middle"> 
                                                                                                            
                                                    <asp:RadioButtonList ID="rbSangradoNasal" runat="server" RepeatDirection="Horizontal" CssClass="respuesta_input">
                                                <asp:ListItem Value="1" Text="Si" />
                                                <asp:ListItem Value="0" Text="No" Selected="True" />                                                                              
                                            </asp:RadioButtonList> 
                                                    
                                                            </td>
                                                            <td style="width: 250px;" align="left" valign="middle"> 
                                                            
                                                    <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerSangradoNasal" runat="server">
                                                <asp:Image ID="image4" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                                    
                                                            </td>
                                                        </tr>
                                                    </table>                                              
                                            
                                        </td>
                                    </tr>                                   
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿Usa lentes?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                        <tr>
                                                            <td style="width: 60px;" align="left" valign="middle"> 
                                                                                                            
                                                            <asp:RadioButtonList ID="rbUsaLentes" runat="server" RepeatDirection="Horizontal" CssClass="respuesta_input">
                                                                <asp:ListItem Value="1" Text="Si" />
                                                                <asp:ListItem Value="0" Text="No" Selected="True" />                                                                         
                                                             </asp:RadioButtonList> 
                                                    
                                                            </td>
                                                            <td style="width: 250px;" align="left" valign="middle"> 
                                                            
                                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerUsaLentes" runat="server">
                                                                <asp:Image ID="image21" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                            </a>  
                                                    
                                                            </td>
                                                        </tr>
                                                    </table>
                                            
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Descripción:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="tbObservacionesOftalmologicas" runat="server"  CssClass="respuesta_input"
                                                Enabled="true" Height="42px" Rows="3" 
                                                TextMode="MultiLine" Width="250px" />
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerObservacionesOftalmologicas" runat="server">
                                                <asp:Image ID="image20" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">¿Usa Aparatos de Ortodoncia?:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                        <tr>
                                                            <td style="width: 60px;" align="left" valign="middle"> 
                                                                                                            
                                                            <asp:RadioButtonList ID="rbUsaOrtodoncia" runat="server" RepeatDirection="Horizontal" CssClass="respuesta_input">
                                                                 <asp:ListItem Value="1" Text="Si" />
                                                                <asp:ListItem Value="0" Text="No" Selected="True" />                                                                          
                                                            </asp:RadioButtonList>  
                                                    
                                                            </td>
                                                            <td style="width: 250px;" align="left" valign="middle"> 
                                                            
                                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerUsaOrtodoncia" runat="server">
                                                                <asp:Image ID="image7" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                            </a> 
                                                    
                                                            </td>
                                                        </tr>
                                                    </table>
                                            
                                        </td>
                                    </tr>    
                                    
                                    <tr>
                                        <td align="left" valign="middle">
                                            <span class="titulo_datos">Descripción:</span>
                                        </td>
                                        <td align="left" valign="middle">
                                           <asp:TextBox ID="tbObservacionesDental" runat="server" CssClass="respuesta_input" 
                                                Enabled="true" Height="42px" Rows="3" 
                                                TextMode="MultiLine" Width="250px"></asp:TextBox>                                        
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerObservacionesDental" runat="server">
                                                <asp:Image ID="image6" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>  
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Hospitalizaciones:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                        
                                            <atk:ModalPopupExtender ID="pnModalHospitalizacion" runat="server" TargetControlID="btnmostrarHospitalizacion"
                                                PopupControlID="pnl_PopUp_Hospitalizacion" BackgroundCssClass="MiModalBackground"
                                                Drag="True" OkControlID="OKHospitalizacion" CancelControlID="CancelHospitalizacion"
                                                DynamicServicePath="" Enabled="True">
                                            </atk:ModalPopupExtender>
                                            <asp:Panel ID="pnl_PopUp_Hospitalizacion" BackColor="White" BorderColor="Black" runat="server" style="display:none">
                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                    <tr>
                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                                                            <span id="Span2">Agregar Hospitalizacion</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Fecha de Registro&nbsp;</span>
                                                            <asp:HiddenField ID="hidencodigoHospitalizacion" runat="server" />
                                                        </td>
                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                <tr>
                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                        <asp:TextBox ID="tbFechaHospitalizacion" runat="server" CssClass="modal_inputs"/>
                                                                    </td>
                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                        <asp:ImageButton runat="server" ID="imageBF5" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                            AlternateText="Elija una fecha del calendario" />
                                                                        <atk:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbFechaHospitalizacion"
                                                                            PopupButtonID="imageBF5" Format="dd/MM/yyyy" CssClass="MyCalendar" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Hospitalización :</span>
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlHospitalizacion" runat="server" Width="200px" CssClass="modal_inputs">
                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                            <asp:ImageButton ID="popup_btnAgregar_Hospitalizacion" runat="server" Width="84px"
                                                                Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png"
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'"
                                                                OnClick="popup_btnAgregar_Hospitalizacion_Click" ToolTip="Aceptar" />&nbsp;
                                                            <asp:ImageButton ID="popup_btnCancelar_Hospitalizacion" runat="server" Width="84px"
                                                                Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png"
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                                                                OnClick="popup_btnCancelar_Hospitalizacion_Click" ToolTip="Cancelar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="controlHospitalizacion" style="display: none">
                                                    <input type="button" id="OKHospitalizacion" />
                                                    <input type="button" id="CancelHospitalizacion" />
                                                </div>
                                            </asp:Panel>
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>                               
                                                    <td style="width: 60px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                    </td>
                                                    <td style="width: 185px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Fecha de Registro
                                                    </td>
                                                    <td style="width: 185px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Motivo de Hospitalización
                                                    </td>
                                                    <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_Hospitalizacion" runat="server" Width="20px" Height="20px"
                                                            ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                            OnClick="btn_Add_Hospitalizacion_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="4">
                                                        <asp:UpdatePanel ID="upHospitalizacion" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                
                                                                    <asp:GridView ID="gvDetalleHospitalizacion" runat="server" 
                                                                        CssClass="gridview_body"
                                                                        GridLines="None" 
                                                                        width="460px"
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False"
                                                                        AllowSorting="False" 
                                                                        ShowHeader="False" 
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleHospitalizacion_RowDataBound"
                                                                        OnRowCommand="gvDetalleHospitalizacion_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_actualizar.png"
                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Actualizar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png"
                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Eliminar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CodigoMotivoHospitalizacion">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoMotivoHospitalizacion" runat="server" Text='<%# Bind("CodigoMotivoHospitalizacion") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFechaHospitalizacion" runat="server" Text='<%# Bind("FechaHospitalizacion") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="200px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHospitalizacion" runat="server" Text='<%# Bind("Hospitalizacion") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="200px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnmostrarHospitalizacion" runat="server" Text="Button" />
                                            </div>
                                                            
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Operaciones:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                        
                                            <atk:ModalPopupExtender ID="pnModalOperacion" runat="server" TargetControlID="btnmostrarOperacion"
                                                PopupControlID="pnl_PopUp_Operaciones" BackgroundCssClass="MiModalBackground"
                                                Drag="True" OkControlID="OKOperacion" CancelControlID="CancelOperacion"
                                                DynamicServicePath="" Enabled="True">
                                            </atk:ModalPopupExtender>
                                            <asp:Panel ID="pnl_PopUp_Operaciones" BackColor="White" BorderColor="Black" runat="server" style="display:none">
                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                    <tr>
                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                                                            <span id="Span3">Agregar Operaciones</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Fecha de Registro&nbsp;</span>
                                                            <asp:HiddenField ID="hidencodigoOperacion" runat="server" />
                                                        </td>
                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                <tr>
                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                        <asp:TextBox ID="tbFechaOperacion" runat="server"  CssClass="modal_inputs"/>
                                                                    </td>
                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                        <asp:ImageButton runat="server" ID="imageBF6" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                            AlternateText="Elija una fecha del calendario" />
                                                                        <atk:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="tbFechaOperacion"
                                                                            PopupButtonID="imageBF6" Format="dd/MM/yyyy" CssClass="MyCalendar" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Operación :</span>
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlOperacion" runat="server" Width="200px" CssClass="modal_inputs">
                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                            <asp:ImageButton ID="popup_btnAgregar_Operacion" runat="server" Width="84px" Height="19px"
                                                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'"
                                                                OnClick="popup_btnAgregar_Operacion_Click" ToolTip="Aceptar" />&nbsp;
                                                            <asp:ImageButton ID="popup_btnCancelar_Operacion" runat="server" Width="84px" Height="19px"
                                                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                                                                OnClick="popup_btnCancelar_Operacion_Click" ToolTip="Cancelar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="controlOperacion" style="display: none">
                                                    <input type="button" id="OKOperacion" />
                                                    <input type="button" id="CancelOperacion" />
                                                </div>
                                            </asp:Panel>       
                                                                                 
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>                               
                                                    <td style="width: 60px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                    </td>
                                                    <td style="width: 185px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Fecha de Registro
                                                    </td>
                                                    <td style="width: 185px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Operación
                                                    </td>
                                                    <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_Operacion" runat="server" Width="20px" Height="20px"
                                                            ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                            OnClick="btn_Add_Operacion_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="4">
                                                        <asp:UpdatePanel ID="upOperacion" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                
                                                                    <asp:GridView ID="gvDetalleOperacion" runat="server" 
                                                                        CssClass="gridview_body"
                                                                        GridLines="None" 
                                                                        width="460px"
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False" 
                                                                        AllowSorting="False" 
                                                                        ShowHeader="False" 
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleOperacion_RowDataBound"
                                                                        OnRowCommand="gvDetalleOperacion_RowCommand">
                                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_actualizar.png"
                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Actualizar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png"
                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Eliminar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CodigoTipoOperaciones">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoTipoOperaciones" runat="server" Text='<%# Bind("CodigoTipoOperaciones") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFechaOperacion" runat="server" Text='<%# Bind("FechaOperacion") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="200px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOperacion" runat="server" Text='<%# Bind("Operacion") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="200px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnmostrarOperacion" runat="server" Text="Button" />
                                            </div>
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
                                        <asp:Label ID="lbl_Bloque_ControlSalud" runat="server" Text="Controles" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <!--FORMULARIO -->
                            <tr>
                               <td align="left" valign="top" style="width: 100%">
                                   <!--CONTENIDO DEL FORMULARIO: TABLA - DIV - ETC --> 
                                   <table border="0" cellpadding="0" cellspacing="0">
                                   
                                    <tr>
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Control de Peso - Talla:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                        
                                           <atk:ModalPopupExtender ID="pnModalControlPesoTalla" runat="server" TargetControlID="btnMostrarControlPesoTalla"
                                                                                                PopupControlID="pnl_PopUp_ControlPesoTalla" BackgroundCssClass="MiModalBackground"
                                                                                                  Drag="True" OkControlID="OKControlPesoTalla" CancelControlID="CancelControlPesoTalla"
                                                                                                DynamicServicePath="" Enabled="True" PopupDragHandleControlID="ControlPesoTallaHeader" />
                                                                                            <asp:Panel ID="pnl_PopUp_ControlPesoTalla" BackColor="White" BorderColor="Black"
                                                                                                runat="server" style="display:none">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                                                                    <tr id="ControlPesoTallaHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Control de Peso y Talla</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px" class="modal_titulos">Fecha de Registro&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hidenCodigoControlPesoTalla" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaControlPesoTalla" runat="server" CssClass="miTextBoxCalendar"
                                                                                                                            Enabled="true" />
                                                                                                                    </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton Enabled="False" runat="server" ID="imageBF7" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="tbFechaControlPesoTalla"
                                                                                                                            PopupButtonID="imageBF7" Format="dd/MM/yyyy" CssClass="MyCalendar" 
                                                                                                                            Enabled="True" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px" class="modal_titulos">Talla : &nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:TextBox ID="tbTalla" runat="server" CssClass="miTextBox" Text="" Width="50px"
                                                                                                                MaxLength="4" />
                                                                                                            0.00 cm.
                                                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="tbTalla" ValidChars="'.'" Enabled="True">
                                                                                                            </atk:FilteredTextBoxExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px"  class="modal_titulos">Peso :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:TextBox ID="tbPeso" runat="server" CssClass="miTextBox" Text="" Width="50px"
                                                                                                                MaxLength="6" />
                                                                                                            00.00 kg.
                                                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="tbPeso" Enabled="True" ValidChars="'.'">
                                                                                                            </atk:FilteredTextBoxExtender>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px;" align="left">
                                                                                                            <span style="padding-left: 10px"  class="modal_titulos">Observaciones :&nbsp;</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px;" align="left" valign="middle">
                                                                                                            <asp:TextBox ID="tbObservacionTallaPeso" runat="server" 
                                                                                                                CssClass="miTextBoxMultiLine" Height="42px" Rows="3" TextMode="MultiLine" 
                                                                                                                Width="200px" MaxLength="600"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_ControlPesoTalla" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'" OnClick="popup_btnAgregar_ControlPesoTalla_Click"
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_ControlPesoTalla" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'" OnClick="popup_btnCancelar_ControlPesoTalla_Click"
                                                                                                                ToolTip="Cancelar" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="2" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="ControlPesoTalla" style="display: none">
                                                                                                    <input type="button" id="OKControlPesoTalla" />
                                                                                                    <input type="button" id="CancelControlPesoTalla" />
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                            
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>                               
                                                    <td style="width: 100px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        Fecha de Registro
                                                    </td>
                                                    <td style="width: 80px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Talla
                                                    </td>
                                                    <td style="width: 80px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Peso
                                                    </td>
                                                    <td style="width: 170px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Observaciones
                                                    </td>
                                                    <td style="width: 30px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_ControlPesoTalla" runat="server" Width="20px" Height="20px"
                                                            ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                            OnClick="btn_Add_ControlPesoTalla_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="5">
                                                        <asp:UpdatePanel ID="upControlPesoTalla" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                
                                                                    <%--<asp:GridView ID="gvDetalleControlPesoTalla" runat="server"
                                                                        CssClass="gridview_body"
                                                                        GridLines="None" 
                                                                        width="460px"
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False" 
                                                                        AllowSorting="False" 
                                                                        ShowHeader="False" 
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleControlPesoTalla_RowDataBound"
                                                                        OnRowCommand="gvDetalleControlPesoTalla_RowCommand">
                                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                        <Columns>                                                                            
                                                                            <asp:TemplateField HeaderText="CodigoAlumno">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFechaControlPesoTalla" runat="server" Text='<%# Bind("FechaControl") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="100px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTalla" runat="server" Text='<%# Bind("Talla") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="80px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPeso" runat="server" Text='<%# Bind("Peso") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="80px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblObservacionesPesoTalla" runat="server" Text='<%# Bind("Observaciones") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="200px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>--%>
                                                                <asp:GridView ID="gvDetalleControlPesoTalla" runat="server" CssClass="gridview_body"
                                                                width="460px" GridLines="None" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False"
                                                                                                                        OnRowDataBound="gvDetalleControlPesoTalla_RowDataBound" OnRowCommand="gvDetalleControlPesoTalla_RowCommand"
                                                                                                                        ShowHeader="False" ShowFooter="False">
                                                                                                                        <Columns>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoControlPesoTalla") %>'
                                                                                                                                        ToolTip="Actualizar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoControlPesoTalla") %>'
                                                                                                                                        ToolTip="Eliminar Registro" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblFechaControlPesoTalla" runat="server" Text='<%# Bind("FechaControl") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="150px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblTalla" runat="server" Text='<%# Bind("Talla") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblPeso" runat="server" Text='<%# Bind("Peso") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="100px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField>
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:Label ID="lblObservacionesPesoTalla" runat="server" Text='<%# Bind("Observaciones") %>' />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Left" Width="380px" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                    </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnMostrarControlPesoTalla" runat="server" Text="Button" />
                                            </div>
                                            
                                        </td>
                                    </tr>     
                                    <tr>
                                        <td style="width:460px" align="left" valign="middle" colspan="2">
                                            <span class="titulo_datos">Otros Controles:</span>
                                        </td>
                                    </tr>
                                    <tr>    
                                        <td style="width:460px" align="left" valign="middle" colspan="2">

                                            <atk:ModalPopupExtender ID="pnModalOtrosControles" runat="server" TargetControlID="btnMostrarTipoControl"
                                                PopupControlID="pnl_PopUp_OtrosControles" BackgroundCssClass="MiModalBackground"
                                                OkControlID="OKTipoControl" CancelControlID="CancelTipoControl"
                                                DynamicServicePath="" Enabled="True">
                                            </atk:ModalPopupExtender>
                                            <asp:Panel ID="pnl_PopUp_OtrosControles" BackColor="White" BorderColor="Black" runat="server" style="display:none">
                                                <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                    <tr>
                                                        <td style="width: 360px; height: 26px" colspan="2" align="center" class="modal_header">
                                                            <span id="OtrosControlesHeader">Agregar Otros Controles</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Fecha de Registro&nbsp;</span>
                                                            <asp:HiddenField ID="hidenCodigoTipoControl" runat="server" />
                                                        </td>
                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                <tr>
                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                        <asp:TextBox ID="tbFechaTipoControl" runat="server"  CssClass="modal_inputs"/>
                                                                    </td>
                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                        <asp:ImageButton runat="server" ID="imageBF0" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/calendar_icon.png"
                                                                            AlternateText="Elija una fecha del calendario" />
                                                                        <atk:CalendarExtender ID="calendario2" runat="server" TargetControlID="tbFechaTipoControl"
                                                                            PopupButtonID="imageBF0" Format="dd/MM/yyyy" CssClass="MyCalendar" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Tipo de Control : &nbsp;</span>
                                                        </td>
                                                        <td style="width: 230px; height: 25px" align="left">
                                                            <asp:DropDownList ID="ddlTipoControl" runat="server" Width="202px" CssClass="modal_inputs">
                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                            <span class="modal_titulos">Resultado :&nbsp;</span>
                                                        </td>
                                                        <td style="width: 230px;" align="left" valign="middle">
                                                            <asp:TextBox ID="tbResultadoTipoControl" runat="server" CssClass="modal_inputs"
                                                                Enabled="true" Height="42px" Rows="3" TextMode="MultiLine" Width="202px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                            <asp:ImageButton ID="popup_btnAgregar_OtrosControles" runat="server" Width="84px"
                                                                Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png"
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptar_1.png'"
                                                                OnClick="popup_btnAgregar_OtrosControles_Click" ToolTip="Aceptar" />&nbsp;
                                                            <asp:ImageButton ID="popup_btnCancelar_OtrosControles" runat="server" Width="84px"
                                                                Height="19px" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png"
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                                                                OnClick="popup_btnCancelar_OtrosControles_Click" ToolTip="Cancelar" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="controlTipoControl" style="display: none">
                                                    <input type="button" id="OKTipoControl" />
                                                    <input type="button" id="CancelTipoControl" />
                                                </div>
                                            </asp:Panel>
                                            
                                            <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                <tr>                               
                                                    <td style="width: 60px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                    </td>
                                                    <td style="width: 100px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Fecha de Registro
                                                    </td>
                                                    <td style="width: 135px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Tipo de Control
                                                    </td>
                                                    <td style="width: 135px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Resultado
                                                    </td>
                                                    <td style="width: 30px; height: 26px; border-right: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        <asp:ImageButton ID="btn_Add_OtrosControles" runat="server" Width="20px" Height="20px"
                                                            ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"
                                                            OnClick="btn_Add_OtrosControles_Click" ToolTip="Agregar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 460px; height: 25px" align="center" valign="top" colspan="5">
                                                        <asp:UpdatePanel ID="upOtrosControles" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                              
                                                                    <asp:GridView ID="gvDetalleTipoControl" runat="server" 
                                                                        CssClass="gridview_body"
                                                                        GridLines="None" 
                                                                        AutoGenerateColumns="False" 
                                                                        AllowPaging="False" 
                                                                        AllowSorting="False" 
                                                                        ShowHeader="False" 
                                                                        ShowFooter="False"
                                                                        EmptyDataText=" - No se encontraron resultados - " 
                                                                        OnRowDataBound="gvDetalleTipoControl_RowDataBound"
                                                                        OnRowCommand="gvDetalleTipoControl_RowCommand">
                                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_actualizar.png"
                                                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Actualizar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/opc_eliminar.png"
                                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoRelacion") %>' ToolTip="Eliminar Registro" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CodigoTipoControl">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCodigoTipoControl" runat="server" Text='<%# Bind("CodigoTipoControl") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFechaControl" runat="server" Text='<%# Bind("FechaControl") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="100px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTipoControl" runat="server" Text='<%# Bind("TipoControl") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="135px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblResultado" runat="server" Text='<%# Bind("Resultado") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="165px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                              
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <div style="display: none">
                                                <asp:Button ID="btnMostrarTipoControl" runat="server" Text="Button" />
                                            </div>
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
            <td style="vertical-align:top;width:200px;text-align:right; font-family:Arial;font-size:10px;">
                                
            </td>
             
        </tr>  
        <!--FONDO PIE -->                  
        <tr>
            <td style="width: 520px;height:24px; text-align: top; background-image: url('/SaintGeorgeOnline/App_Themes/Imagenes/Familia/contenedores/grupoInformacion_contenedorfondo_pieV2.jpg'); background-repeat: no-repeat ">
                &nbsp;</td>
            <td style="width: 200px;height:24px; text-align: right; font-family: Arial; font-size: 10px; ">
                &nbsp;</td>
        </tr>
        
    </table>    
    </ContentTemplate>
    </asp:UpdatePanel>

</div>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
    <ContentTemplate>
    
        <atk:ModalPopupExtender id="ModalPopupExtender1" runat="server" 
            TargetControlID="lblAccionExportar" 
            PopupControlID="Panel1" 
            BackgroundCssClass="MiModalBackground" 
             />
         
        <asp:Panel ID="Panel1" runat="server" style="background-color:#FFFFFF;display:none;width:250px">
            <div style="margin: auto;">
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <span style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">Procesando...<br />Espere un momento.</span>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                        
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>                              
            </div>
        </asp:Panel>        
    
        <asp:Label ID="lblAccionExportar" runat="server" Text="MiModalHandler" style="display:none;"/> 
    </ContentTemplate>    
    </asp:UpdatePanel>  
    
 <script type="text/javascript">

     $(document).ready(function() {
         $(window).load(function() {
             $('#page_effect').fadeIn(2000);
         });
     });
    
 </script>    
    
    </div>
    </form>
</body>
</html>
