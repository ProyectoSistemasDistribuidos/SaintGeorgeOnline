<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudFichaAlumno.aspx.vb" Inherits="Interfaz_Familia_Modulo_SolicitudesActualizacionInformacion_SolicitudFichaAlumno" %>

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
.miHiddenStyle{display:none;}
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
</style>    
<script type="text/javascript">

    function confirm_delete() {
        if (confirm('¿Esta seguro que desea eliminar el registro seleccionado?') == true)
            return true;
        else
            return false;
    }

    function confirm_cancelar() {
        if (confirm('¿Está seguro que desea salir del registro sin guardar sus cambios?') == true)
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

<div id="page_effect" style="display:none;">
    
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
                        <td style="width: 370px" align="left" valign="middle">
<asp:Label ID="lblNombreCompletoAlumno" runat="server" style="padding-left: 20px; font-size:12px; font-family:Arial; font-weight: bold;" />                        
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td style="width: 100px;" align="right" valign="middle">                       
<asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_1.png"
    onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_2.png'"
    onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnAceptarEnvioCorreo_1.png'"
    ToolTip="Enviar" OnClick="btnFichaGrabar_click" />                       
                        </td>
                        <td style="width: 40px">
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
                                        <asp:Label ID="lbl_Bloque_DatosPersonales" runat="server" Text="Personales" />
                                    </span>
                                    
<asp:HiddenField ID="hd_CodigoPersonaSolicitante" runat="server" />
<asp:HiddenField ID="hd_Codigo" runat="server" />
<asp:HiddenField ID="hd_CodigoPersona" runat="server" />
<asp:HiddenField ID="hd_CodigoRelacionIdiomasPersonas1" runat="server" />
<asp:HiddenField ID="hd_CodigoRelacionIdiomasPersonas2" runat="server" />
<asp:HiddenField ID="hd_CodigoRelacionNacionalidadesPersonas1" runat="server" />
<asp:HiddenField ID="hd_CodigoRelacionNacionalidadesPersonas2" runat="server" />                                    

                                    <div style="BORDER-TOP: #000000 0px solid;width:400px"></div>
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
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Código de Alumno:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">                                           
                                            <span class="respuesta_datos">
                                            <asp:Label ID="lblCodigo" runat="server" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Código del Educando:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                            <span class="respuesta_datos" >
                                            <asp:Label ID="lblCodigoEducando" runat="server" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Apellido Paterno:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                            <span class="respuesta_datos" >
                                            <asp:Label  ID="lblApellidoPaterno" runat="server" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Apellido Materno:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                            <span class="respuesta_datos" >
                                            <asp:Label  ID="lblApellidoMaterno" runat="server" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nombres:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                            <span class="respuesta_datos" >
                                            <asp:Label  ID="lblNombre" runat="server" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Sexo:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                            <span class="respuesta_datos" >
                                            <asp:Label  ID="lblSexo" runat="server" /></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Tipo de Documento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                            <span class="respuesta_datos" >
                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="253px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nro. Documento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">   
                                            <span class="respuesta_datos" >
                                            <asp:TextBox ID="tbNumDocumento" runat="server" Width="250px" MaxLength="12" Height="18px" CssClass="respuesta_input"/>  
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
                                        <asp:Label ID="lbl_Bloque_DatosNacimiento"  runat="server" Text="Nacimiento" />
                                    </span>
                                    <div style="BORDER-TOP: #000000 0px solid;width:400px"></div>
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
                                        <td style="width:150px; height :15px"  align="left" valign="bottom" class="titulo_datos_contenedor">
                                            <span class="titulo_datos" >Nacimiento Registrado:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                            <%--<asp:Label ID="lblNacRegistrado" runat="server" />--%>
                                             <asp:RadioButtonList CssClass="respuesta_input" ID="rblEsRegistrado" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Si</asp:ListItem>
                                                <asp:ListItem Value="1">No</asp:ListItem>
                                            </asp:RadioButtonList></span>
                                        </td>
                                    </tr>
                                 <%--   <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos" >Fecha de Nacimiento:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                            <asp:Label ID="lblFechaNacimiento" runat="server" /></span>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos" >Fecha de Nacimiento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <asp:TextBox CssClass="respuesta_input"   ID="txtFechaNacimiento" runat="server"></asp:TextBox>
                                            <asp:ImageButton runat="server" ID="imgFechaNacimiento" Height="15px"
                                                    ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de nacimiento" />
                                             <atk:CalendarExtender ID="txtFechaNacimiento_CalendarExtender" runat="server" 
                                                Enabled="True"
                                                Format="dd/MM/yyyy" 
                                                PopupButtonID="imgFechaNacimiento" 
                                                TargetControlID="txtFechaNacimiento">
                                            </atk:CalendarExtender>
                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Pais:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                            <%--<asp:Label ID="lblPais" runat="server" />--%>
                                             <asp:DropDownList ID="ddlPaisNacimiento" runat="server" 
                                                AutoPostBack="true" CssClass="respuesta_input" 
                                                Width="255px" Height="16px">
                                            </asp:DropDownList></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Departamento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                            <%--<asp:Label ID="lblDepartamento" runat="server" />--%>
                                            <asp:DropDownList ID="ddlDepartamentoNacimiento" runat="server" 
                                                AutoPostBack="true" CssClass="respuesta_input" 
                                               
                                                Width="255px">
                                            </asp:DropDownList></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Provincia:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                            <%--<asp:Label ID="lblProvincia" runat="server" />--%>
                                             <asp:DropDownList ID="ddlProvinciaNacimiento" runat="server" 
                                                AutoPostBack="true" CssClass="respuesta_input" 
                                                Width="255px">
                                            </asp:DropDownList></span>
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Distrito:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                            <%--<asp:Label ID="lblDistrito" runat="server" />--%></span>
                                                 <asp:DropDownList ID="ddlDistritoNacimiento" runat="server" 
                                                CssClass="respuesta_input" Width="255px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">1° Nacionalidad:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos" >
                                            <asp:DropDownList ID="ddlNacionalidad1" runat="server" Width="180px" CssClass="respuesta_input">
                                            </asp:DropDownList> 
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerNacionalidad1" runat="server">
                                                <asp:Image ID="image3" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">2° Nacionalidad:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos" >
                                            <asp:DropDownList ID="ddlNacionalidad2" runat="server" Width="180px" CssClass="respuesta_input">
                                            </asp:DropDownList> 
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerNacionalidad2" runat="server">
                                                <asp:Image ID="image1" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
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
                                        <asp:Label ID="lbl_Bloque_DatosDomicilio" runat="server" Text="Domicilio" />
                                    </span>
                                    <div style="BORDER-TOP: #000000 0px solid;width:400px"></div>
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
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Departamento:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                            <asp:DropDownList ID="ddlDomicilioDepartamento" runat="server" Width="255px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlDomicilioDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioUbigeo" runat="server">
                                                <asp:Image ID="image20" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Provincia:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                            <asp:DropDownList ID="ddlDomicilioProvincia" runat="server" Width="255px" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlDomicilioProvincia_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            </span>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Distrito:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                            <asp:DropDownList ID="ddlDomicilioDistrito" runat="server" Width="255px" CssClass="respuesta_input">
                                            </asp:DropDownList>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Urbanización:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbDomicilioUrbanizacion" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>     
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioUrbanizacion" runat="server">
                                                <asp:Image ID="image21" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Dirección:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbDomicilioDireccion" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioDireccion" runat="server">
                                                <asp:Image ID="image22" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Referencia domiciliaria:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbDomicilioReferencia" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>  
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioReferencia" runat="server">
                                                <asp:Image ID="image23" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Teléfono:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbDomicilioTelefono" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>     
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioTelefono" runat="server">
                                                <asp:Image ID="image24" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Tiene acceso a internet?:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                            
                                                    <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                        <tr>
                                                            <td style="width: 60px;" align="left" valign="middle"> 
                                                    <asp:RadioButtonList ID="rbDomicilioAccesoInternet" runat="server" RepeatDirection="Horizontal" CssClass="respuesta_input"> 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>    
                                                            </td>
                                                            <td style="width: 250px;" align="left" valign="middle"> 
                                                    <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerDomicilioAccesoInternet" runat="server">
                                                        <asp:Image ID="image25" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                    </a>  
                                                            </td>
                                                        </tr>
                                                    </table>   
                                            
                                            </span>
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
                                        <asp:Label ID="lbl_Bloque_DatosReligiosos" runat="server" Text="Religioso" />
                                    </span>
                                    <div style="BORDER-TOP: #000000 0px solid;width:400px"></div>
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
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos" >¿Profesa alguna religión?:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos" >
                                          
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width: 60px;" align="left" valign="middle">                                                  
                                                        <asp:RadioButtonList ID="rbReligion" runat="server" RepeatDirection="Horizontal" style="margin:0;" CssClass="respuesta_input"
                                                            AutoPostBack="true" OnSelectedIndexChanged="rbReligion_SelectedIndexChanged"> 
                                                            <asp:ListItem Value="0" >No</asp:ListItem>                                                                             
                                                            <asp:ListItem Value="1" Selected="True">Si</asp:ListItem> 
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="width: 250px;" align="left" valign="middle">       
                                                        <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerProfesaReligion" runat="server">
                                                            <asp:Image ID="image9" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                        </a> 
                                                    </td>
                                                </tr>
                                            </table>                                          
                                          
                                             </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Religión que profesa:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                            <asp:DropDownList ID="ddlReligion" runat="server" Width="180px" AutoPostBack="true" CssClass="respuesta_input"
                                                OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged">
                                            </asp:DropDownList>                                                   
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerReligion" runat="server">
                                                <asp:Image ID="image10" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Ha sido bautizado?:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                           
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width: 60px;" align="left" valign="middle">                                                  
                                                        <asp:RadioButtonList ID="rbBautizo" runat="server" RepeatDirection="Horizontal" style="margin:0;" CssClass="respuesta_input"
                                                            OnSelectedIndexChanged="rbBautizo_SelectedIndexChanged" AutoPostBack="true">   
                                                            <asp:ListItem Value="-1" style="display:none;" >Null</asp:ListItem>         
                                                            <asp:ListItem Value="0" >No</asp:ListItem>                                                                             
                                                            <asp:ListItem Value="1" Selected="True">Si</asp:ListItem> 
                                                        </asp:RadioButtonList>    
                                                    </td>
                                                    <td style="width: 250px;" align="left" valign="middle">                                                 
                                                        <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerBautizo" runat="server">
                                                            <asp:Image ID="image11" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                        </a>  
                                                    </td>
                                                </tr>
                                            </table>                                                
                                                
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Lugar de Bautizo:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbLugarBautizo" runat="server" Width="250px" MaxLength="100" CssClass="respuesta_input"/>    
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerLugarBautizo" runat="server">
                                                <asp:Image ID="image12" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Año de Bautizo:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbAnioBautizo" runat="server" Width="80px" MaxLength="4" CssClass="respuesta_input"/>  
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAnioBautizo" runat="server">
                                                <asp:Image ID="image13" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>     
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="tbAnioBautizo" Enabled="True">
                                            </atk:FilteredTextBoxExtender>                                              
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Ha dado la primera comunión?:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos" >
                                           
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width: 60px;" align="left" valign="middle">   
                                                        <asp:RadioButtonList ID="rbPriComunion" runat="server" RepeatDirection="Horizontal" style="margin:0;" CssClass="respuesta_input"
                                                             OnSelectedIndexChanged="rbPriComunion_SelectedIndexChanged" AutoPostBack="true">  
                                                            <asp:ListItem Value="-1" style="display:none;" >Null</asp:ListItem>         
                                                            <asp:ListItem Value="0" >No</asp:ListItem>                                                                             
                                                            <asp:ListItem Value="1" Selected="True">Si</asp:ListItem> 
                                                        </asp:RadioButtonList>                                                    
                                                    </td>
                                                    <td style="width: 250px;" align="left" valign="middle">  
                                                        <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerPriComunion" runat="server">
                                                            <asp:Image ID="image14" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                        </a>  
                                                    </td>
                                                </tr>
                                            </table>  
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Lugar de Primera Comunión:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos" >
                                            <asp:TextBox ID="tbLugarPriComunion" runat="server" Width="250px" MaxLength="100" CssClass="respuesta_input"/>  
                                             </span>   
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerLugarPriComunion" runat="server">
                                                <asp:Image ID="image15" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Año de Primera Comunión:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos" >
                                            <asp:TextBox ID="tbAnioPriComunion" runat="server" Width="80px" MaxLength="4" CssClass="respuesta_input"/> 
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAnioPriComunion" runat="server">
                                                <asp:Image ID="image16" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>            
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="tbAnioPriComunion" Enabled="True">
                                            </atk:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">¿Se ha confirmado?:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos" >
                                           
                                            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; margin:0; padding:0;">
                                                <tr>
                                                    <td style="width: 60px;" align="left" valign="middle"> 
                                                        <asp:RadioButtonList ID="rbConfirmado" runat="server" RepeatDirection="Horizontal" style="margin:0;" CssClass="respuesta_input" 
                                                            OnSelectedIndexChanged="rbConfirmado_SelectedIndexChanged" AutoPostBack="true">   
                                                            <asp:ListItem Value="-1" style="display:none;" >Null</asp:ListItem>         
                                                            <asp:ListItem Value="0" >No</asp:ListItem>                                                                             
                                                            <asp:ListItem Value="1" Selected="True">Si</asp:ListItem> 
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="width: 250px;" align="left" valign="middle"> 
                                                        <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerConfirmado" runat="server">
                                                            <asp:Image ID="image17" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                                        </a> 
                                                    </td>
                                                </tr>
                                            </table>   
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Lugar de Confirmación:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos" >
                                                <asp:TextBox ID="tbLugarConfirmado" runat="server" Width="250px" MaxLength="100" CssClass="respuesta_input"/>                                               
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerLugarConfirmado" runat="server">
                                                <asp:Image ID="image18" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Año de Confirmación:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle"> 
                                            <span class="respuesta_datos" >
                                                <asp:TextBox ID="tbAnioConfirmado" runat="server" Width="80px" MaxLength="4" CssClass="respuesta_input"/>  
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerAnioConfirmado" runat="server">
                                                <asp:Image ID="image19" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>             
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="tbAnioConfirmado" Enabled="True">
                                            </atk:FilteredTextBoxExtender>
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
                                        <asp:Label ID="lbl_Bloque_DatosEmergencia" runat="server" Text="Emergencia" />
                                    </span>
                                    <div style="BORDER-TOP: #000000 0px solid;width:400px"></div>
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
                                        <td style="width:460px; height: 20px;" align="left" valign="middle" class="titulo_datos_contenedor" colspan="2">
                                            <span class="titulo_datos" style="font-size: 12px; font-weight: bold;">En caso de emergencia llamar a:</span>                                            
                                        </td>
                                    </tr>                                    
                                    
                                    <tr>
                                        <td style="width:460px; height: 20px;" align="left" valign="middle" class="titulo_datos_contenedor" colspan="2">                                    
                                            <br />
                                        </td>
                                    </tr>                                          
                                    
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Nombres y Apellidos:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbNombreCompletoEmergencia" runat="server" Width="250px" MaxLength="100" CssClass="respuesta_input"/>      
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerNombreCompletoEmergencia" runat="server">
                                                <asp:Image ID="image26" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Teléfonos Casa:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbTelfCasaEmergencia" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>  
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTelfCasaEmergencia" runat="server">
                                                <asp:Image ID="image27" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>                                             
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Teléfonos Oficina:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbTelfOficinaEmergencia" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>   
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTelfOficinaEmergencia" runat="server">
                                                <asp:Image ID="image28" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>                                             
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Teléfonos Movil:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle">
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbTelfMovilEmergencia" runat="server" Width="250px" MaxLength="100" Height="18px" CssClass="respuesta_input"/>    
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerTelfMovilEmergencia" runat="server">
                                                <asp:Image ID="image29" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
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
                                        <asp:Label ID="lbl_Bloque_DatosOtros"  runat="server" Text="Otros" />
                                    </span>
                                    <div style="BORDER-TOP: #000000 0px solid;width:400px"></div>
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
                                            <span class="camposObligatorios"><i>Campos Obligatorios: (*) <br /> En caso de poner varios correos agregar "  ; " como separador </i></span>
                                        </td>
                                    </tr>    
                                     <tr>
                                     <td height="5px" colspan="2">
                                      &nbsp;&nbsp;</td>
                                     </tr>                               
                                    <tr>
                                        <td style="width:170px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Lengua Materna:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:290px" align="left" valign="middle"> 
                                            <span class="respuesta_datos">                                            
                                                <asp:DropDownList ID="ddlLenguaMaterna1" runat="server" Width="254px" CssClass="respuesta_input">
                                                </asp:DropDownList> 
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerLenguaMaterna1" runat="server">
                                                <asp:Image ID="image2" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Segunda Lengua:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="respuesta_datos" >
                                                <asp:DropDownList ID="ddlLenguaMaterna2" runat="server" Width="255px" CssClass="respuesta_input">
                                                </asp:DropDownList>
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerLenguaMaterna2" runat="server">
                                                <asp:Image ID="image4" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Cantidad de Hermanos:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbCantidadHermanos" runat="server" Width="250px" MaxLength="2" CssClass="respuesta_input"/>    
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerCantidadHermanos" runat="server">
                                                <asp:Image ID="image5" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="tbCantidadHermanos"  Enabled="True">
                                            </atk:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Posición entre los Hermanos:</span><span class="camposObligatorios">(*)</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbPosicionHermanos" runat="server" Width="250px" MaxLength="2" CssClass="respuesta_input"/>     
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerPosicionHermanos" runat="server">
                                                <asp:Image ID="image6" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>  
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="tbPosicionHermanos"  Enabled="True">
                                            </atk:FilteredTextBoxExtender>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Correo electónico Personal:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbCorreoElectronico" runat="server" Width="250px" MaxLength="100" CssClass="respuesta_input"/>   
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerCorreoElectronico" runat="server">
                                                <asp:Image ID="image7" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="titulo_datos">Telefono Móvil:</span>
                                        </td>
                                        <td style="width:310px" align="left" valign="middle" class="titulo_datos_contenedor">
                                            <span class="respuesta_datos">
                                                <asp:TextBox ID="tbCelular" runat="server" Width="250px" MaxLength="9" CssClass="respuesta_input"/>     
                                            </span>
                                            <a href="#" onclick="return false;" class="tooltip" visible="false" id="toolTipVerCelular" runat="server">
                                                <asp:Image ID="image8" runat="server" ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/AlertIcon.gif" Height="16" Width="16" />
                                            </a> 
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="tbCelular" Enabled="True">
                                            </atk:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="15px" colspan ="2">
                                         &nbsp;&nbsp;
                                        </td>
                                   </tr>  
                                    <tr>
                                       <td colspan ="2">
                                        <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                        
                                         <tr>                               
                                                    <td style="width: 80px; height: 26px; border-left: solid 1px black;" align="center" valign="middle" class="gridview_header">
                                                        Familiar
                                                    </td>
                                                    <td style="width: 190px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Nombre
                                                    </td>
                                                    <td style="width: 100px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Vive con el alumno(a)&nbsp; <span class="camposObligatorios">(*)</span>
                                                    </td>
                                                    <td style="width: 90px; height: 26px;" align="center" valign="middle" class="gridview_header">
                                                        Encargado Pagar &nbsp; <span class="camposObligatorios">(*)</span>
                                                    </td>
                                        </tr>
                                            <td style="width: 460px; height: 25px" align="center" valign="top" colspan="5">
                                               <asp:GridView ID="grwInformacionAdicional" runat="server" 
                                                 CssClass="gridview_body"
                                                 GridLines="None" 
                                                 width="460px"
                                                 ShowHeader="False" 
                                                 AutoGenerateColumns="False" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Familiar">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("PT_Descripcion") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="190px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vive Con ella">
                                            <ItemTemplate >
                                                <asp:RadioButtonList CssClass="respuesta_input" ID="rblViveConElla" runat="server" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1"> Si &nbsp; </asp:ListItem>
                                                    <asp:ListItem Value="0"> No </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Responsable de Pago">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlEncargadoPagar" class="respuesta_input"  runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlEncargadoPagar_SelectedIndexChanged">
                                                    <asp:ListItem   Value="1" >Si </asp:ListItem>
                                                    <asp:ListItem    Value="0" >No </asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="gridview_row" HorizontalAlign="Center" Width="90px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="codigos" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Bind("RAF_ViveConAlumno") %>' ID="lblViveConElla" runat="server" ></asp:Label>
                                                <asp:Label Text='<%# Bind("RAF_ResponsablePago") %>' ID="lblEsPagante" runat="server" ></asp:Label>
                                                <asp:Label ID="lblCodigoFamilia" Text='<%# Bind("IF_CodigoIntegranteFamilia") %>'  runat="server" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                            </td>
                                        </table> 
                                        </td>
                                      <%--  <td>
                                        </td>--%>
                                    </tr>
                                   </table>
                               </td>
                            </tr>                            
                         </table>
                         
            </td>
            <!--MENU DE GRUPOS DE INFORMACION -->
            <td style="border: solid 0px red; vertical-align:top;width:200px;text-align:right; font-family:Arial;font-size:10px;">
                
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
