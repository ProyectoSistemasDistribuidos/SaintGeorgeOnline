<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frameFichaFamiliar.aspx.vb" Inherits="Modulo_Matricula_frameFichaFamiliar" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="*" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" base target="_self">
<base target="_self">
    <title></title>
    
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
    
    <style type="text/css">
        em { color : Red; font-weight: bold; }
        body, html {margin: 0; padding: 0}
    </style>
    
    <!--Scripts Internos -->
    <script type="text/javascript">
    
    function confirm_cancelar() {
        if (confirm('¿Esta seguro que desea salir del registro sin guardar sus cambios?') == true)
            return true;
        else
            return false;
    }
    
    function cerrar() {

        window.close();
           
    }

    function cargar() {

        window.location = window.location;
    
    }

    function retonar() {
    
        window.returnValue = 1;
        
    }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <atk:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </atk:ToolkitScriptManager>
    

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            
                <div id="miContenidoFicha" style="width: 880px; border: solid 0px red; margin: 0; padding: 0;">
                
                    <div id="miCabeceraFicha" style="width: 860px; border: solid 0px red; margin: 0; padding: 0;">      
                    
                        <table cellpadding="0" cellspacing="0" border="0" width="649px" style=" margin: 0; padding: 0;">
                            <tr>
                                <td style="width: 505px;" align="left" valign="middle">
                                
                        <fieldset style="width:505px;margin: 0;" id="Bloque_DatosPersonales" runat="server">
                            <legend style="width:400px">Datos Personales</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="495px">
                            
                                <tr>
                                    <td colspan="5" style="height: 15px;" align="right">
                                        <em>Campos Obligatorios (*)</em>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 74px; height: 100px; background: #FFFFFF url(../App_Themes/Imagenes/img_bg.gif) no-repeat; background-position: center center;"
                                        align="center" valign="middle" rowspan="4">
                                        <asp:Image ID="imgFotoPaciente" runat="server" Width="54" Height="64" 
                                            Style="border: #7f9db9 1px solid"
                                            ImageUrl="~/Fotos/noPhoto.gif" />
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="10">
                                    </td>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Apellido Paterno&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                        <asp:HiddenField ID="hidenCodigoFamiliar" runat="server" Value="0" />                                           
                                        <asp:HiddenField ID="hidenCodigoPersona" runat="server" Value="0" />  
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="10">
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">  
                                        <asp:TextBox ID="tbApellidoPaterno" runat="server" CssClass="miTextBox" 
                                            style="width: 250px; font-size: 8pt; font-family: Arial;" MaxLength="100" />                                        
                                    </td>
                                   
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Apellido Materno&nbsp;</span> 
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbApellidoMaterno" runat="server" CssClass="miTextBox" 
                                            style="width: 250px; font-size: 8pt; font-family: Arial;" MaxLength="100" />
                                    </td>
                                    
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Nombre&nbsp;</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbNombre" runat="server" CssClass="miTextBox" 
                                            style="width: 250px; font-size: 8pt; font-family: Arial;" MaxLength="100" /> 
                                    </td>                                    
                                </tr>                                
                                
                                <tr>
                                    <td style="width: 130px;" align="left" valign="middle">
    <asp:ImageButton ID="btnDisponibilidad" runat="server" Width="94" Height="19" 
        ImageUrl="~/App_Themes/Imagenes/btnVerificar_1.png"
        onmouseover="this.src = '../App_Themes/Imagenes/btnVerificar_2.png'" 
        onmouseout="this.src = '../App_Themes/Imagenes/btnVerificar_1.png'" 
        ToolTip="Verificar disponibilidad de información personal."
        onclick="btnDisponibilidad_click"/>     
    <atk:ModalPopupExtender ID="pnModalFichaFamiliarDisponible" runat="server"
        TargetControlID="VerFichaFamiliarDisponible"
        PopupControlID="pnlFichaFamiliarDisponible"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="true" 
        OkControlID="OKFichaFamiliarDisponible" 
        CancelControlID="CancelFichaFamiliarDisponible"
        Drag="true" 
        PopupDragHandleControlID="FichaFamiliarDisponibleHeader" />                                                       
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">                                       
                                        <div id="DivDisponibilidad" runat="server">
                                        </div> 
                                    </td>                                    
                                </tr>
                                
                                <tr>
                                    <td style="width: 74px; height: 25px;" align="left" valign="middle" rowspan="6"></td>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                       
                                        <span>Sexo&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                    </td>
                                    <td style="width: 271px;" align="left" valign="middle">
                                        <asp:RadioButtonList ID="rbSexo" runat="server" RepeatDirection="Horizontal" 
                                            style="font-size: 8pt; font-family: Arial;">   
                                            <asp:ListItem Value="2" Selected="True">Masculino</asp:ListItem>                                                                             
                                            <asp:ListItem Value="1">Femenino</asp:ListItem> 
                                        </asp:RadioButtonList>   
                                    </td>                                    
                                </tr>
                                
                                
                                <tr>                                    
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Tipo Documento&nbsp;</span><span class="camposObligatorios">(*)</span>     
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">                                        
                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" 
                                            style="width: 255px; font-size: 8pt; font-family: Arial;">
                                        </asp:DropDownList>         
                                    </td>
                                    
                                </tr>                                
                                
                                <tr>                                    
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Nro. Documento&nbsp;</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbNumDocumento" runat="server" CssClass="miTextBox"
                                            style="width: 250px; font-size: 8pt; font-family: Arial;" MaxLength="12" />
                                    </td>
                                    
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Estado Civil&nbsp;</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlEstadoCivil" runat="server"
                                            style="width: 254px; font-size: 8pt; font-family: Arial;">
                                        </asp:DropDownList>   
                                    </td>
                                    
                                </tr>                                 
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Vive&nbsp;</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">
                                        <asp:RadioButtonList ID="rbVive" runat="server" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rbVive_SelectedIndexChanged" AutoPostBack="true" 
                                            style="font-size: 8pt; font-family: Arial;"> 
                                            <asp:ListItem Value="0">No</asp:ListItem>       
                                            <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                        </asp:RadioButtonList>        
                                    </td>                                    
                                </tr> 
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Fecha defunción&nbsp;</span><span class="camposObligatorios">(*)</span>  
                                    </td>
                                    <td style="width: 271px; height: 25px;" align="left" valign="middle">
                                        <table cellpadding="0" cellspacing="0" border="0" width="321px">
                                        <tr>
                                            <td align="left" valign="middle" style="width: 110px; height: 25px;">
                                                <asp:TextBox ID="tbFechaDefuncion" runat="server" 
                                                    CssClass="miTextBoxCalendar"
                                                    style="width: 100px; font-size: 8pt; font-family: Arial;" />    
                                                <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                    TargetControlID="tbFechaDefuncion"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-">
                                                </atk:MaskedEditExtender>
                                            </td>
                                            <td align="left" valign="middle" style="width: 181px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="image1" 
                                                    ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de defuncioón." />
                                                <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                    TargetControlID="tbFechaDefuncion"
                                                    PopupButtonID="image1" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" />
                                            </td>
                                        </tr>                                                    
                                        </table>                                  
                                    </td>                                    
                                </tr>                                   
                            </table>
                        </fieldset>
                        
                                </td>
                                <td style="width: 144px;" align="center" valign="middle">
                                
                                    <table cellpadding="0" cellspacing="0" border="0" width="144px" style="margin: 0;">
                                        <tr>
                                            <td style="width: 144px;" align="center" valign="middle">
                                                 <asp:ImageButton ID="btnFichaGrabar" runat="server" Width="84" Height="19" 
                                                    ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'" 
                                                    ToolTip="Grabar"
                                                    onclick="btnFichaGrabar_click"/>    
                                            </td>
                                        </tr>
                                        <tr><td style="height:10px;"></td></tr>
                                        <tr>
                                            <td style="width: 144px;" align="center" valign="middle">
                                                <asp:ImageButton ID="btnFichaCancelar" runat="server" Width="84" Height="19"
                                                    ImageUrl="~/App_Themes/Imagenes/btnCancelarV2_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelarV2_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelarV2_1.png'" 
                                                    ToolTip="Cancelar" Visible="false"
                                                    onclick="btnFichaCancelar_Click" 
                                                    CausesValidation="false"/>          
                                            </td>
                                        </tr>
                                    </table>    
                                                                  
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                    
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miContainerFicha" style="width: 860px; border: solid 0px red; margin: 0; padding: 0;">
                        <atk:TabContainer ID="TabContainer2" runat="server" Width="850px" ActiveTabIndex="0" AutoPostBack="false" ScrollBars="Vertical">
                            
                            <atk:TabPanel ID="miFichaTab1" runat="server" HeaderText="Tab2" >
                                <HeaderTemplate>
                                    Datos Nacimiento
                                </HeaderTemplate>
                                <ContentTemplate>
                                
                                <div id="Bloque_DatosNacimiento" runat="server" style="border:0; margin:0;">
                                 
                                    <fieldset>                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                                 
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Fecha Nacimiento&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="321px">
                                                    <tr>
                                                        <td align="left" valign="middle" style="width: 110px; height: 25px;">
                                                            <asp:TextBox ID="tbFechaNacimiento" runat="server" 
                                                                CssClass="miTextBoxCalendar" Height="18px" />    
                                                            <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                                TargetControlID="tbFechaNacimiento"
                                                                UserDateFormat="DayMonthYear"                                                                    
                                                                Mask="99/99/9999" 
                                                                MaskType="Date" 
                                                                PromptCharacter="-">
                                                            </atk:MaskedEditExtender>
                                                            <asp:Label ID="lblVerFechaNacimiento" runat="server" /> 
                                                        </td>
                                                        <td align="left" valign="middle" style="width: 211px; height: 25px;">
                                                            <asp:ImageButton runat="server" ID="image2" 
                                                                ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de nacimiento." />
                                                            <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                TargetControlID="tbFechaNacimiento"
                                                                PopupButtonID="image2" 
                                                                Format="dd/MM/yyyy" 
                                                                CssClass="MyCalendar" />
                                                        </td>
                                                    </tr>                                                    
                                                </table>     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Nacionalidad&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlNacionalidad" runat="server" Width="254px">
                                                    </asp:DropDownList> 
                                                    <asp:Label ID="lblVerNacionalidad" runat="server" />  
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                    
                                </div>    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab2" runat="server" HeaderText="Tab1">
                                <HeaderTemplate>
                                    Datos Adicionales
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <fieldset>                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                                 
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>¿Profesa alguna religión?&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbAdicionalesProfesaReligion" runat="server" 
                                                        RepeatDirection="Horizontal" AutoPostBack="True"
                                                        
                                                        OnSelectedIndexChanged="rbAdicionalesProfesaReligion_SelectedIndexChanged" > 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>   
                                                    <asp:Label ID="lblVerAdicionalesProfesaReligion" runat="server" />   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Religión&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlAdicionalesReligion" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerAdicionalesReligion" runat="server" /> 
                                                </td>
                                            </tr>
                                                    
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Nombre de la Iglesia&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbAdicionalesNombreIglesia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblVerAdicionalesNombreIglesia" runat="server" /> 
                                                </td>
                                            </tr>
                                                                                                                                                                               
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Celular&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbAdicionalesCelular" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblVerAdicionalesCelular" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Servicio Radio&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlAdicionalesRadio" runat="server" Width="255px" 
                                                        OnSelectedIndexChanged="ddlAdicionalesRadio_SelectedIndexChanged" 
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerAdicionalesRadio" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Numero Radio&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbAdicionalesNumeroRadio" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblVerAdicionalesNumeroRadio" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Correo electrónico&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbAdicionalesEmail" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblVerAdicionalesEmail" runat="server" /> 
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    
                                    <div class="miEspacio">
                                    </div>
                                    
                                    <fieldset>
                                        <legend>Detalle Idiomas&nbsp;<span class="camposObligatorios">(*)</span></legend>                                        
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">  
                                            <tr><td colspan="2" height="10px"></td></tr>
                                            <tr>
                                                <td style="width: 590px;" align="center" valign="top" colspan="2">                                                    
                                                    <table cellpadding="0" cellspacing="0" border="0" width="790px">                                                    
                                                        <tr>                                                            
                                                            <td style="width: 760px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                Idioma                                                             
                                                            </td>
                                                            <td style="width: 30px; height: 26px;" align="right" class="miGVBusquedaFicha_Header">
    <asp:ImageButton ID="btnAgregarDetalleIdioma" runat="server" Width="24px" Height="24px"
        ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
        OnClick="btnAgregarDetalleIdioma_Click"                                                    
        ToolTip="Agregar"/>    
    <atk:ModalPopupExtender ID="pnModalIdioma" runat="server"
        TargetControlID="btnAgregarDetalleIdioma"
        PopupControlID="pnlIdioma"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="True" 
        OkControlID="OKIdioma" 
        CancelControlID="CancelIdioma"
        Drag="True" 
        PopupDragHandleControlID="IdiomaHeader" Enabled="True" />                                                                   
                                                            </td>
                                                        </tr>                                                           
                                                        <tr>
                                                            <td style="width: 790px; height: 25px" align="center" valign="top" colspan="2">
                                                            
                                                            <asp:UpdatePanel ID="upIdioma" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>  
                                                                          
                                                                <div id="miGVMantFichaRegitros">
                                                                <asp:GridView ID="GVListaIdiomas" runat="server" 
                                                                    CssClass="miGVBusquedaFicha"
                                                                    Width="790px"
                                                                    GridLines="None" 
                                                                    AutoGenerateColumns="False"
                                                                    ShowHeader="false"
                                                                    ShowFooter="false"
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false"    
                                                                    OnRowDataBound="GVListaIdiomas_RowDataBound"
                                                                    OnRowCommand="GVListaIdiomas_RowCommand">                           
                                                                    
                                                                    <Columns>         
                                                                        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>    
                                                                        
                                                                        <asp:TemplateField HeaderText="Codigo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                        </asp:TemplateField>   
                                                                                                 
                                                                        <asp:TemplateField HeaderText="Descripción">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="760px"/>
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="760px" />
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
                                        </table>                                                       
                                    </fieldset>
                                    <div class="miEspacio">
                                    </div>
                                    
                                    <fieldset>
                                        <legend>Detalle FichaAutos&nbsp;<span class="camposObligatorios">(*)</span></legend>
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr><td colspan="4" height="10px"></td></tr>
                                            <tr>
                                                <td style="width: 100px;" align="center" valign="top" colspan="4">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="790px">                                                        
                                                        <tr>
                                                            <td style="width: 253px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                align="center" class="miGVBusquedaFicha_Header">
                                                                Placa
                                                            </td>
                                                            <td style="width: 253px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                align="center" class="miGVBusquedaFicha_Header">
                                                                Marca
                                                            </td>
                                                            <td style="width: 253px; height: 26px; text-align: center; color: White; font-size: 10px;"
                                                                align="center" class="miGVBusquedaFicha_Header">
                                                                Modelo
                                                            </td>
                                                            <td style="width: 30px; height: 26px;" align="right" class="miGVBusquedaFicha_Header">
    <asp:ImageButton ID="btnAgregarDetalleFichaAutos" runat="server" Width="24px" Height="24px"
        ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" OnClick="btnAgregarDetalleFichaAutos_Click"
        ToolTip="Agregar" />

    <atk:ModalPopupExtender ID="pnModalFichaAutos" runat="server" TargetControlID="btnVer_FichaAutos"
        PopupControlID="pnlFichaAutos" BackgroundCssClass="MiModalBackground" DropShadow="True"
        OkControlID="OKFichaAutos" CancelControlID="CancelFichaAutos" Drag="True" 
        PopupDragHandleControlID="FichaAutosHeader" DynamicServicePath="" 
        Enabled="True" />                                                                    
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="center" valign="top" colspan="4">
                                                                <asp:UpdatePanel ID="UpFichaAutos" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div id="miGVMantFichaRegitros">
                                                                            <asp:GridView ID="GVListaFichaAutos" runat="server" CssClass="miGVBusquedaFicha" Width="790px"
                                                                                GridLines="None" AutoGenerateColumns="False" ShowHeader="false" ShowFooter="false"
                                                                                AllowPaging="false" AllowSorting="false" OnRowDataBound="GVListaFichaAutos_RowDataBound"
                                                                                OnRowCommand="GVListaFichaAutos_RowCommand">
                                                                                <Columns> 
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png"
                                                                                                CommandName="Actualizar" CommandArgument='<%# Bind("Codigo") %>'
                                                                                                ToolTip="Actualizar Registro" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"
                                                                                                CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                                                        <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                    </asp:TemplateField>
                                                                               
                                                                                    <asp:TemplateField HeaderText="Placa">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbPlaca" runat="server" Text='<%# Bind("Placa") %>' />                                                                                            
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="360px" />
                                                                                        <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="253px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Marca">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbMarca" runat="server" Text='<%# Bind("Marca") %>' />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="760px" />
                                                                                        <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="253px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Modelo">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbModelo" runat="server" Text='<%# Bind("Modelo") %>' />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="760px" />
                                                                                        <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="253px" />
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                        <div class="miEspacio">
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab3" runat="server" HeaderText="Tab3">
                                <HeaderTemplate>
                                    Datos del Domicilio
                                </HeaderTemplate>
                                <ContentTemplate>
                                
                                    <fieldset>                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>País&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioPais" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlDomicilioPais_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerDomicilioPais" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Departamento&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioDepartamento" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlDomicilioDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerDomicilioDepartamento" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Provincia&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioProvincia" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlDomicilioProvincia_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerDomicilioProvincia" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Distrito&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioDistrito" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerDomicilioDistrito" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Urbanización&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioUrbanizacion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblVerDomicilioUrbanizacion" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Dirección&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioDireccion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                                                    <asp:Label ID="lblVerDomicilioDireccion" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Referencia domiciliaria&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioReferencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblVerDomicilioReferencia" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Teléfono&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioTelefono" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblVerDomicilioTelefono" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>¿Tiene acceso a internet?&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbDomicilioAccesoInternet" runat="server" RepeatDirection="Horizontal"> 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>    
                                                    <asp:Label ID="lblVerDomicilioAccesoInternet" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab4" runat="server" HeaderText="Tab4">
                                <HeaderTemplate>
                                    Datos Laborales
                                </HeaderTemplate>
                                <ContentTemplate>
                                
                                    <fieldset>                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Situación Laboral&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlSituacionLaboral" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlSituacionLaboral_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerSituacionLaboral" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Ocupación / cargo&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbOcupacion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                                                    <asp:Label ID="lblVerOcupacion" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Centro de Trabajo&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCentroTrabajo" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                                                    <asp:Label ID="lblVerCentroTrabajo" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Dirección de Trabajo&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTrabajoDireccion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                                                    <asp:Label ID="lblVerTrabajoDireccion" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>País&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlTrabajoPais" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlTrabajoPais_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerTrabajoPais" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Departamento&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlTrabajoDepartamento" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlTrabajoDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerTrabajoDepartamento" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Provincia&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlTrabajoProvincia" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlTrabajoProvincia_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerTrabajoProvincia" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Distrito&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlTrabajoDistrito" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerTrabajoDistrito" runat="server" /> 
                                                </td>
                                            </tr>   
                                            
                                         
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Teléfono / Anexo&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTrabajoTelefono" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                    <asp:Label ID="lblVerTrabajoTelefono" runat="server" /> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Celular&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTrabajoCelular" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />  
                                                    <asp:Label ID="lblVerTrabajoCelular" runat="server" />    
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Servicio Radio&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlTrabajoRadio" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlTrabajoRadio_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerTrabajoRadio" runat="server" />    
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Número Radio&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTrabajoNumeroRadio" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" /> 
                                                    <asp:Label ID="lblVerTrabajoNumeroRadio" runat="server" />        
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Correo electrónico&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTrabajoEmail" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                    <asp:Label ID="lblVerTrabajoEmail" runat="server" />   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>¿Tiene acceso a internet?&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbTrabajoAccesoInternet" runat="server" RepeatDirection="Horizontal"> 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>    
                                                    <asp:Label ID="lblVerTrabajoAccesoInternet" runat="server" />   
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                    
                                </ContentTemplate>
                            </atk:TabPanel>                            
                            
                            <atk:TabPanel ID="miFichaTab5" runat="server" HeaderText="Tab5">
                                <HeaderTemplate>
                                    Datos de los Estudios
                                </HeaderTemplate>
                                <ContentTemplate>
                                
                                    <fieldset>                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>¿Es un Ex-Alumno?&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbEstudiosExAlumno" runat="server" 
                                                        RepeatDirection="Horizontal" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbEstudiosExAlumno_SelectedIndexChanged"> 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>    
                                                    <asp:Label ID="lblVerEstudiosExAlumno" runat="server" />  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Colegio de egreso&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbEstudiosColegioEgreso" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                                                    <asp:Label ID="lblVerEstudiosColegioEgreso" runat="server" />  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Año que egreso&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                     <asp:DropDownList ID="ddlEstudiosAnioEgreso" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="tbEstudiosAnioEgreso" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" Visible="false" />
                                                    <asp:Label ID="lblVerEstudiosAnioEgreso" runat="server" />  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Donde continuo estudios&nbsp;</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbEstudiosContinuo" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                                                    <asp:Label ID="lblVerEstudiosContinuo" runat="server" />  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Nivel de Instrucción&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlEstudiosNivelInstruccion" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerEstudiosNivelInstruccion" runat="server" />  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Escolaridad Ministerio&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlEstudiosEscolaridadMinisterio" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerEstudiosEscolaridadMinisterio" runat="server" />  
                                                </td>
                                            </tr>                                            
                                        </table>
                                    </fieldset>
                                    
                                    <div class="miEspacio">
                                    </div>
                                    
                                    <fieldset>
                                        <legend>Detalle Profesiones</legend>                                        
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">                                          
                                            <tr><td colspan="2" height="10px"></td></tr>
                                            
                                            <tr>
                                                <td style="width: 790px;" align="center" valign="top" colspan="2">
                                                    
                                                    <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                    
                                                        <tr>                                                            
                                                            <td style="width: 760px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                Profesión                                                                
                                                            </td>
                                                            <td style="width: 30px; height: 26px;" align="right" class="miGVBusquedaFicha_Header">
    <asp:ImageButton ID="btnAgregarDetalleProfesion" runat="server" Width="24" Height="24"
        ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
        OnClick="btnAgregarDetalleProfesion_Click"                                                    
        ToolTip="Agregar" Enabled="true"/>  
    <atk:ModalPopupExtender ID="pnModalProfesion" runat="server"
        TargetControlID="btnAgregarDetalleProfesion"
        PopupControlID="pnlProfesion"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="true" 
        OkControlID="OKProfesion" 
        CancelControlID="CancelProfesion"
        Drag="true" 
        PopupDragHandleControlID="ProfesionHeader" />                                                                                                                                             
                                                            </td>
                                                        </tr> 
                                                          
                                                        <tr>
                                                            <td style="width: 790px; height: 25px" align="center" valign="top" colspan="2">
                                                            
                                                            <asp:UpdatePanel ID="upProfesion" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>                                        
                                                                <div id="miGVMantFichaRegitros">
                                                                <asp:GridView ID="GVListaProfesiones" runat="server" 
                                                                    CssClass="miGVBusquedaFicha"
                                                                    Width="790px"
                                                                    GridLines="None" 
                                                                    AutoGenerateColumns="False"
                                                                    ShowHeader="false"
                                                                    ShowFooter="false"
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false"    
                                                                    OnRowDataBound="GVListaProfesiones_RowDataBound"
                                                                    OnRowCommand="GVListaProfesiones_RowCommand">                          
                                                                    
                                                                    <Columns>        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>    
                                                                        
                                                                        <asp:TemplateField HeaderText="Codigo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                        </asp:TemplateField>   
                                                                                                 
                                                                        <asp:TemplateField HeaderText="Descripción">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="760px"/>
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="760px" />
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
                                            
                                        </table>   
                                                                                                       
                                    </fieldset>
                                    
                                </ContentTemplate>
                            </atk:TabPanel>                         
                            
                        </atk:TabContainer>
                    </div>
                    
                </div>
                  
    <asp:panel id="pnlFichaFamiliarDisponible" BackColor="White" BorderColor="Black" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" width="540px">    
        <tr>
                <td style="width: 510px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2">                
                    <span id="FichaFamiliarDisponibleHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Lista de Posibles Coincidencias</span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                    <asp:ImageButton ID="btnCerraFichaTemporal" runat="server" Width="16" Height="15"
                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        onclick="btnCerraFichaTemporal_Click" ToolTip="Cerrar Panel"/>
                </td>
            </tr>
            <tr><td colspan="2"><br /></td></tr>
            <tr>
                <td colspan="2" align="center" valign="middle">   
                
                      <div style="border: solid 1px #a6a3a3; width:500px">
            <asp:GridView ID="GVListaFichaFamiliarDisponible" runat="server"
                            CssClass="miGridviewBusqueda"
                            Width="500"
                            GridLines="None" 
                            AutoGenerateColumns="False"
                            ShowHeader="true"
                            ShowFooter="false"
                            AllowPaging="false" 
                            AllowSorting="false"    
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GVListaFichaFamiliarDisponible_RowDataBound"
                            OnRowCommand="GVListaFichaFamiliarDisponible_RowCommand">
                        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />   
                                                        
                        <Columns>       
                                    
                        <asp:TemplateField>
                            <ItemTemplate>                                    
                                <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                    CommandName="Seleccionar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Seleccionar Ficha de Atención" />
                                                
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>                            
                       
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" ItemStyle-HorizontalAlign="left" ItemStyle-Width="240" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                        <asp:BoundField DataField="DescTipoDocumentoIdentidad" HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="center" ItemStyle-Width="100" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                        <asp:BoundField DataField="NumeroDocumentoIdentidad" HeaderText="Número Documento" ItemStyle-HorizontalAlign="center" ItemStyle-Width="100" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                        
                        </Columns> 
                        
            </asp:GridView>
               
                    </div>        
                              
                </td>
            </tr> 
            <tr><td colspan="2"><br /></td></tr>              
        </table>  
        <div id="controlFichaFamiliarDisponible" style="display:none">
            <input type="button" id="VerFichaFamiliarDisponible" runat="server" />
            <input type="button" id="OKFichaFamiliarDisponible" />
            <input type="button" id="CancelFichaFamiliarDisponible" />
        </div>       
    </asp:panel>                   
                        
    <asp:panel id="pnlIdioma" BackColor="White" BorderColor="Black" runat="server">
    
        <table cellpadding="0" cellspacing="0" border="0" width="360px">    
            <tr>
                <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                    <span id="IdiomaHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor:pointer">Agregar Idioma</span>
                </td>
            </tr>
            <tr><td colspan="2" height="10px">&nbsp;</td></tr>
            <tr>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    <span style="padding-left:10px">Idioma&nbsp;</span>
                </td>
                <td style="width: 260px; height: 25px" align="left">
                    <asp:DropDownList ID="ddlIdioma" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                    <asp:ImageButton ID="btnModalAceptarIdioma" runat="server" Width="84px" Height="19px"
                        ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                        OnClick="btnModalAceptarIdioma_Click"
                        ToolTip="Aceptar" />&nbsp;
                    <asp:ImageButton ID="btnModalCancelarIdioma" runat="server" Width="84px" Height="19px"
                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                        OnClick="btnModalCancelarIdioma_Click"
                        ToolTip="Cancelar" />
                </td>
            </tr>      
            <tr><td colspan="3" height="10px"></td></tr>              
        </table>
        <div id="controlIdioma" style="display:none">
            <input type="button" id="OKIdioma" />
            <input type="button" id="CancelIdioma" />
        </div>
       
    </asp:panel>                
          
    <asp:Panel ID="pnlFichaAutos" BackColor="White" BorderColor="Black" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" width="360px">
        <tr>
            <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                <span id="Span1" style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial;
                    cursor: pointer">Agregar Ficha Autos</span>                                                                       
            </td>
        </tr>
        <tr>
            <td colspan="4" height="10px">
                <asp:HiddenField ID="hiddenCodigoPlaca" runat="server" Value="" />
            </td>
        </tr>
        <tr>
            <td style="width: 150px; height: 25px" align="left" valign="middle">
                                                                    <span style="padding-left:10px">Marca&nbsp;</span>                                                                  
            </td>
            <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                <asp:TextBox ID="tbMarca" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                    TargetControlID="tbMarca" ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                </atk:FilteredTextBoxExtender>
            </td>                                                              
        </tr>
        <tr>
            <td style="width: 100px; height: 25px" align="left" valign="middle">
                <span style="padding-left:10px">Modelo&nbsp;</span>                                                                  
            </td>
            <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                <asp:TextBox ID="tbModelo" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                    TargetControlID="tbModelo" ValidChars="' ','á','é','í','ó','ú','(',')'"
                    Enabled="True">
                </atk:FilteredTextBoxExtender>
            </td>                                                              
        </tr>
        <tr>
            <td style="width: 100px; height: 25px" align="left" valign="middle">                                                                    
                <span style="padding-left:10px">Placa&nbsp;</span>     
            </td>                                                                
            <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                <asp:TextBox ID="tbPlaca" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                    TargetControlID="tbPlaca" ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                </atk:FilteredTextBoxExtender>
            </td>
        </tr>                                        
                                                            
        <tr>
            <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                <asp:ImageButton ID="btnModalAceptarFichaAutos" runat="server" Width="84px" 
                    Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png"
                    onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                     onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'"
                    OnClick="btnModalAceptarFichaAutos_Click" ToolTip="Aceptar" />&nbsp;
                <asp:ImageButton ID="btnModalCancelarFichaAutos" runat="server" Width="84px" 
                    Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'"
                    OnClick="btnModalCancelarFichaAutos_Click" ToolTip="Cancelar" />
            </td>
        </tr>
        <tr>
            <td colspan="3" height="10px">
            </td>
        </tr>
        </table>
        <div id="Div1" style="display: none">
            <input type="button" id="OKFichaAutos" />
            <input type="button" id="CancelFichaAutos" />
            <input type="button" id="btnVer_FichaAutos" runat="server" />
        </div>
    </asp:Panel>                
                                                                 
    <asp:panel id="pnlProfesion" BackColor="White" BorderColor="Black" runat="server">
    
        <table cellpadding="0" cellspacing="0" border="0" width="360px">    
            <tr>
                <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                    <span id="ProfesionHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor:pointer">Agregar Profesión</span>
                </td>
            </tr>
            <tr><td colspan="2" height="10px"></td></tr>
            <tr>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    <span style="padding-left:10px">Profesión&nbsp;</span>
                </td>
                <td style="width: 260px; height: 25px" align="left">
                    <asp:DropDownList ID="ddlProfesion" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                    <asp:ImageButton ID="btnModalAceptarProfesion" runat="server" Width="84" Height="19"
                        ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                        OnClick="btnModalAceptarProfesion_Click"
                        ToolTip="Aceptar" />&nbsp;
                    <asp:ImageButton ID="btnModalCancelarProfesion" runat="server" Width="84" Height="19"
                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                        OnClick="btnModalCancelarProfesion_Click"
                        ToolTip="Cancelar" />
                </td>
            </tr>      
            <tr><td colspan="3" height="10px"></td></tr>              
        </table>
        
        <div id="controlProfesion" style="display:none">
            <input type="button" id="OKProfesion" />
            <input type="button" id="CancelProfesion" />
        </div>
       
    </asp:panel>
                                    
            </ContentTemplate>
        </asp:UpdatePanel>
         

    </form>
</body>
</html>
