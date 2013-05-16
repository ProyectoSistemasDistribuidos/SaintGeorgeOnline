<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frameFichaAlumno.aspx.vb" Inherits="Modulo_Matricula_frameFichaAlumno" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="*" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            
                <div id="miContenidoFichaAlumno">
                
                    <div id="miCabeceraFichaAlumno">
                     <table width="800" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style ="width: 400px;" rowspan ="5">
                        <fieldset id="Bloque_SituacionActual" runat="server">
                            <legend>Situación Actual</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="600px">
                                 <tr>
                                    <td style="width: 110px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Año Ingresa :&nbsp;</span>
                                    </td>
                                    <td style="width: 321px; height: 25px;" align="left" valign="middle">
    <asp:DropDownList ID="ddlAnioAcademico" runat="server" Width="100px" style="font-size: 8pt; font-family: Arial;">
    </asp:DropDownList> 
                                    </td>
                                </tr>      
                                <tr>
                                    <td style="width: 110px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Grado Actual :&nbsp;</span>
                                    </td>
                                    <td style="width: 321px; height: 25px;" align="left" valign="middle">
    <asp:DropDownList ID="ddlGrados" runat="server" Width="300px" style="font-size: 8pt; font-family: Arial;">
    </asp:DropDownList> 
                                    </td>
                                    <td style="width: 84px;" align="right" valign="middle">
                                    </td>
                                </tr>

                            </table>
                        </fieldset>
                        </td>
                    </tr> 
                    <tr>
                        <td style ="width: 150px; height:25px; " align="right"  >
                        </td>    
                    </tr>
                    <tr>
                        <td align="right" style="width: 250px; height:20px;">
                            <asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" 
                                ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                                onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" 
                                onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'" 
                                ToolTip="Grabar"
                                onclick="btnGrabar_click"/>
                        </td>    
                    </tr>
                    <tr>
                        <td align="right" style="width: 250px; height:20px;">
                            <asp:ImageButton ID="btnCancelar" 
                                runat="server" Width="84" Height="19"
                                ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                ToolTip="Cancelar"
                                onclick="btnCancelar_Click" 
                                CausesValidation="false" Visible="false" />   
                        </td>
                    </tr>
                    <tr>
                        <td style ="width: 150px; height:20px;" align="right">
                         </td>
                    </tr>
                </table>
                </div>
                    
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miContainerFichaAlumno">
                        <atk:TabContainer ID="TabContainer2" runat="server" Width="850px" ActiveTabIndex="0"
                            AutoPostBack="false" ScrollBars="Vertical">
                            
                            <atk:TabPanel ID="miFichaTab1" runat="server" HeaderText="Tab1">
                                <HeaderTemplate>
                                    Datos Personales
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div id="Bloque_DatosPersonales" runat="server" style="border:0; margin:0;">
                                    <fieldset id="miFichaTab1_1" >                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Apellido Paterno :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbApellidoPaterno" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Apellido Materno :&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:TextBox ID="tbApellidoMaterno" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />  
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Nombre :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbNombre" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Sexo :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:RadioButtonList ID="rbSexo" runat="server" RepeatDirection="Horizontal">   
                                                        <asp:ListItem Value="2" >Masculino</asp:ListItem>                                                                             
                                                        <asp:ListItem Value="1" Selected="True">Femenino</asp:ListItem> 
                                                    </asp:RadioButtonList> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Tipo Documento :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="255px">
                                                   </asp:DropDownList> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Nro. Documento :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbNumDocumento" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />  
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                </div>    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab2" runat="server" HeaderText="Tab2">
                                <HeaderTemplate>
                                    Datos de Nacimiento
                                </HeaderTemplate>
                                <ContentTemplate>  
                                 <div id="Bloque_DatosNacimiento" runat="server" style="border:0; margin:0;">                              
                                    <fieldset id="miFichaTab2_1">                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Nacimiento Registrado :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbNacRegistrado" runat="server" RepeatDirection="Horizontal">   
                                                        <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                        <asp:ListItem Value="1">Si</asp:ListItem> 
                                                    </asp:RadioButtonList>   
                                                </td>
                                            </tr> 
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Fecha &nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td   valign="middle" align="left" style="width: 100px; height: 25px;">
                                                    <asp:TextBox ID="tbFechaNacimiento" runat="server" CssClass="miTextBoxCalendar" />   
                                                </td>
                                                <td valign="middle" align="left" style="width: 510px; height: 25px;">
                                                    <asp:ImageButton runat="server" ID="imageBF5" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                    <atk:CalendarExtender ID="CalendarExtender4" runat="server" 
                                                        TargetControlID="tbFechaNacimiento"
                                                        PopupButtonID="imageBF5" 
                                                        Format="dd/MM/yyyy" 
                                                        CssClass="MyCalendar" />
                                                </td>
                                               
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Pais&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td  colspan="2"  style="width: 610px; height: 25px" align="left"  valign="middle">
                                                    <asp:DropDownList ID="ddlPais" runat="server" Width="180px"  OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Departamento&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td  colspan="2"  style="width: 610px; height: 25px" align="left"  valign="middle">
                                                    <asp:DropDownList ID="ddlDepartamento" runat="server" Width="180px"
                                                        OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>  
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Provincia&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlProvincia" runat="server" Width="180px"
                                                        OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Distrito&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDistrito" runat="server" Width="180px">
                                                    </asp:DropDownList>  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>1era Nacionalidad&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlNacionalidad1" runat="server" Width="180px">
                                                    </asp:DropDownList>  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>2da Nacionalidad&nbsp;</span>
                                                </td>
                                                <td style="width: 610px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlNacionalidad2" runat="server" Width="180px">
                                                    </asp:DropDownList> 
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                 </div>   
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab3" runat="server" HeaderText="Tab3">
                                <HeaderTemplate>
                                    Datos Adicionales
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div id="Bloque_OtrosDatos" runat="server" style="border:0; margin:0;">                              
                                    <fieldset id ="Bloque_DatosAdicionales" runat="server"> 
                                        <legend>Otros Datos</legend> 
                                         <table cellpadding="0" cellspacing="0" border="0" width="790">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                                 
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>1era Lengua Materna&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlLenguaMaterna1" runat="server" Width="254px">
                                                    </asp:DropDownList> 
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>2da Lengua Materna&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlLenguaMaterna2" runat="server" Width="255px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                                    
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Estado Civil&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:DropDownList ID="ddlEstadocivil" runat="server" Width="255px">
                                                   </asp:DropDownList> 
                                                </td>
                                            </tr>
                                                                                                                                                                               
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Cantidad de Hermanos&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCantidadHermanos" runat="server" CssClass="miTextBox" Width="250px" MaxLength="2" Height="18px" />      
                                                </td>
                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbCantidadHermanos"  Enabled="True">
                                                    </atk:FilteredTextBoxExtender>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Posición entre los Hermanos&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbPosicionHermanos" runat="server" CssClass="miTextBox" Width="250px" MaxLength="2" Height="18px" />     
                                                </td>
                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbPosicionHermanos"  Enabled="True">
                                                    </atk:FilteredTextBoxExtender>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Correo electrónico&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCorreoElectronico" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />   
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Correo electrónico Institucional&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCorreoElectronicoInstitucional" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Celular&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbCelular" runat="server" CssClass="miTextBox" Width="250px" MaxLength="9" Height="18px" />     
                                                </td>
                                                 <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbCelular"  Enabled="True">
                                                 </atk:FilteredTextBoxExtender>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    
                                    <div class="miEspacio">
                                    </div>
                    
                                    <fieldset id ="Bloque_DatosReligiosos" runat="server" >  
                                        <legend>Datos Religiosos</legend>                                     
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                          <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Profesa alguna Religión?&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                   <asp:RadioButtonList ID="rbReligion" runat="server"     RepeatDirection="Horizontal" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbReligion_SelectedIndexChanged" > 
                                                    <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                    <asp:ListItem Value="1">Si</asp:ListItem> 
                                                    </asp:RadioButtonList>  
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Religión que profesa :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle" > 
                                                    <asp:DropDownList ID="ddlReligion" runat="server" Width="180px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged">
                                                    </asp:DropDownList>   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Ha sido bautizado? :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbBautizo" runat="server" RepeatDirection="Horizontal">   
                                                    <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                    <asp:ListItem Value="1">Si</asp:ListItem> 
                                                    </asp:RadioButtonList>   
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Lugar:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbLugarBautizo" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />          
                                                </td>
                                                <td style="width: 70px; height: 25px" align="center">
                                                    <span>Año:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left"  valign="middle">
                                                     <asp:TextBox ID="tbAnioBautizo" runat="server" CssClass="miTextBox" Width="80px" MaxLength="4" Height="18px" />           
                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbAnioBautizo" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>             
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Ha dado la primera comunión? :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbPriComunion" runat="server" RepeatDirection="Horizontal">   
                                                    <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                    <asp:ListItem Value="1">Si</asp:ListItem> 
                                                    </asp:RadioButtonList>   
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Lugar:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbLugarPriComunion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />      
                                                </td>
                                                <td style="width: 70px; height: 25px" align="center">
                                                    <span>Año:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left"  valign="middle">
                                                    <asp:TextBox ID="tbAnioPriComunion" runat="server" CssClass="miTextBox" Width="80px" MaxLength="4" Height="18px" />      
                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbAnioPriComunion" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>      
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Se ha confirmado? :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbConfirmado" runat="server" RepeatDirection="Horizontal">   
                                                    <asp:ListItem Value="0">No</asp:ListItem>                                                                             
                                                    <asp:ListItem Value="1">Si</asp:ListItem> </asp:RadioButtonList>   
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Lugar:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbLugarConfirmado" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                </td>
                                                <td style="width: 70px; height: 25px" align="center">
                                                    <span>Año:&nbsp;</span>
                                                </td>
                                                <td style="width: 300px; height: 25px" align="left"  valign="middle">
                                                    <asp:TextBox ID="tbAnioConfirmado" runat="server" CssClass="miTextBox" Width="80px" MaxLength="4" Height="18px" />            
                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="tbAnioConfirmado" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            
                                            
                                        </table>
                                    </fieldset>
                                </div>    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab4" runat="server" HeaderText="Tab4">
                                <HeaderTemplate>
                                    Datos Familiares
                                </HeaderTemplate>
                                <ContentTemplate>
                                     <div id="Bloque_DatosFamiliares" runat="server" style="border:0; margin:0;">                                
                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                               <tr>
                                                    <td style="width: 250px; height: 26px; text-align:center;  color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Nombre Completo</td>
                                                    <td  style="width: 80px; height: 26px;  text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Parentesco</td>
                                                    <td    style="width: 80px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Usuario</td>
                                                    <td style="width: 80px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Telf. Casa</td>
                                                    <td style="width: 80px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Telf. Celular</td>
                                                   <td style="width: 60px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Apoderado</td>
                                                   <td style="width: 140px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                        Email</td>
                                                   <td style="width: 20px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                    <%--   <asp:ImageButton ID="btn_Add_Familiar" runat="server" Width="20px" Height="20px"
                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png" 
                                                                                                            ToolTip="Agregar" />--%>
                                                  </td>
                                                </tr>
                                                            
                                               <tr>  
                                                   <td style="width: 790px; height: 25px" align="center" valign="top" colspan="8">
                                                      <div style="width: 790px;  border: solid 1px #a6a3a3; " >
                                                        <asp:GridView ID="gvFamiliares" runat="server"
                                                            CssClass="miGridviewBusqueda"
                                                            Width="790px" 
                                                            GridLines="None" 
                                                            AutoGenerateColumns="false" 
                                                            AllowPaging="false" 
                                                            AllowSorting="false"
                                                            ShowFooter="false" 
                                                            ShowHeader="false" 
                                                            EmptyDataText=" - No se encontraron resultados - "
                                                            OnRowDataBound="gvFamiliares_RowDataBound">
                                                        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                                                        <Columns>  
                                                          <asp:TemplateField HeaderText="CodigoFamiliar">                                                                      
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCodigoFamiliar" runat="server" Text='<%# Bind("CodigoFamiliar") %>' />
                                                               
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                        </asp:TemplateField>               
                                                        <asp:TemplateField >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="250px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("Parentesco") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="80px" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("Usuario") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="80px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTelfCasa" runat="server" Text='<%# Bind("TelfCasa") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="80px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCelular" runat="server" Text='<%# Bind("Celular") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="100px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApoderado" runat="server" Text="" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="60px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'  />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="110px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnVer" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png" 
                                                                    CommandName="Visualizar"
                                                                    CommandArgument='<%# Bind("CodigoFamiliar") %>' ToolTip="Ver Registro"
                                                                  /> 
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                        </asp:TemplateField>  
                                                         <asp:BoundField DataField="CodigoFamiliar" HeaderText="CodigoFamiliar" ItemStyle-Width="0" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" />    
                                                        </Columns>
                                                        </asp:GridView>
                                                       </div>
                                                    </td>                                                        
                                               </tr>    
                                               
                                               <tr>
                                                  <td height="10px">
                                                 </td>  
                                              </tr>     
                                           </table>
                                    </div>
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab5" runat="server" HeaderText="Tab5">
                                <HeaderTemplate>
                                    Datos de Domicilio
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div id="Bloque_DatosDomicilio" runat="server" style="border:0; margin:0;">                                
                                    <fieldset id="DatosDomicilio" runat="server">                                       
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>                                     
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Departamento&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioDepartamento" runat="server" Width="255px" 
                                                        OnSelectedIndexChanged="ddlDomicilioDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Provincia&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioProvincia" runat="server" Width="255px"
                                                        OnSelectedIndexChanged="ddlDomicilioProvincia_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Distrito&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlDomicilioDistrito" runat="server" Width="255px">
                                                    </asp:DropDownList>   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Urbanización&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioUrbanizacion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />         
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Dirección&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioDireccion" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Referencia domiciliaria&nbsp;</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioReferencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Teléfono&nbsp;</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbDomicilioTelefono" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>¿Tiene acceso a internet?&nbsp;</span>
                                                </td>
                                                <td style="width: 570px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:RadioButtonList ID="rbDomicilioAccesoInternet" runat="server" RepeatDirection="Horizontal"> 
                                                        <asp:ListItem Value="0">No</asp:ListItem>       
                                                        <asp:ListItem Value="1">Si</asp:ListItem>                                                                                                                                                       
                                                    </asp:RadioButtonList>     
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                </div>    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab6" runat="server" HeaderText="Tab6">
                                <HeaderTemplate>
                                   Datos Médicos
                                </HeaderTemplate>
                                <ContentTemplate>
                                <div id="Bloque_DatosMedico" runat="server" style="border:0; margin:0;">                                
                                     <fieldset id="Bloque_DatosSeguroMedico" runat="server" >
                                            <legend >Datos de Seguro Médico</legend>
                                                 <table cellpadding="0" cellspacing="0" border="0" width="590">
                                                   
                                                    <tr>
                                                        <td colspan="2" height="10px">
                                                        </td> 
                                                    </tr> 
                                                    
                                                    <tr>
                                                        <td style="width: 590px;" align="center" valign="top" colspan="8">
                                                
                                                            <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                  <tr>
                                                                    <td  style="width:100px; height: 26px; text-align: center; color: White;
                                                                        font-size: 10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                        Año Matrícula
                                                                    </td>
                                                                    <td style="width: 100px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Tipo 
                                                                    </td>
                                                                    <td style="width: 140px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        compañia
                                                                    </td>
                                                                     <td style="width: 100px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                        Numero Poliza
                                                                    </td>
                                                                     <td style="width: 200px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                       Clínicas
                                                                    </td>
                                                                      <td style="width: 150px; height: 26px; text-align: left; color: White; font-size: 10px;"
                                                                        align="left" class="miGVBusquedaFicha_Header">
                                                                       Vigencia
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                    <td style="width: 790px; height: 25px" align="center" valign="top" colspan="6">
                                                                     
                                                                    </td>
                                                                </tr>
                                                           </table> 
                                                
                                                        </td>
                                                    </tr>                                                                   
                                                  
                                                </table>
                                                
                                     </fieldset>
                                  
                                 <div class="miEspacio">
                                 </div>
                                  
                                     <fieldset id="Bloque_CasoEmergencia" runat="server">
                                         
                                                    <legend>Aviso en caso de emergencia y no poder ubicar a familiares registrados</legend>
                                                         <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                <tr>
                                                                    <td colspan="3" style="height: 15px;" align="right">
                                                                        <em>Campos Obligatorios (*)</em>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Nombre Completo :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbNombreCompletoEmergencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />   
                                                </td>
                                            </tr>
                                                                                        
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Telefono Casa :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTelfCasaEmergencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Telefono Oficina :&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTelfOficinaEmergencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 180px; height: 25px" align="left">
                                                    <span>Telefono Móvil :&nbsp;</span>
                                                </td>
                                                <td style="width: 670px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:TextBox ID="tbTelfMovilEmergencia" runat="server" CssClass="miTextBox" Width="250px" MaxLength="100" Height="18px" />   
                                                </td>
                                            </tr>
                                          </table>   
                                       </fieldset>
                                     
                                 <div class="miEspacio">
                                 </div>
                                       
                                       <fieldset id="Bloque_DatosEspeciales" runat="server">
                                             <legend >Datos Especiales</legend>
                                                    
                                                     <table cellpadding="0" cellspacing="0" border="0" width="590">
                                                      
                                                        <tr>
                                                            <td style="width: 590px;" align="center" valign="top" colspan="8">
                                                    
                                                                <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                                                     <tr>
                                                                        <td colspan="3" style="height: 10px;" align="right">
                                                                            
                                                                            </td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td style="width: 180px; height: 25px" align="left">
                                                                            <span>Experiencias Traumaticas&nbsp;</span>
                                                                        </td>
                                                                        <td  colspan ="2" style="width: 610px; height: 25px" align="left" valign="middle">
                                                                            <asp:TextBox ID="tbExperienciasTraumaticas" runat="server" CssClass="miTextBoxMultiLine" Width="360px" 
                                                                                Height="35px" Rows="3" TextMode="MultiLine" />
                                                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                                            TargetControlID="tbExperienciasTraumaticas" 
                                                                            ValidChars="' ','-','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'" 
                                                                            Enabled="True">
                                                                            </atk:FilteredTextBoxExtender>    
                                                                        </td>
                                                                    </tr>  
                                                                    
                                                                        
                                                               </table> 
                                                               
                                                                <div style="display:none">
                                                                 <asp:Button ID="Button3" runat="server" Text="Button" />      
                                                                </div>
                                                    
                                                            </td>
                                                        </tr>                                                                   
                                                      
                                                    </table>
                                                    
                                      
                                 
                                  <div class="miEspacio">
                                 </div>
                                                    
                                       </fieldset>
                                </div>  
                                </ContentTemplate>
                            </atk:TabPanel>  
                              
                        </atk:TabContainer>
                    </div>
                    
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
