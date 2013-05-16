<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="AsistenciaDiaria.aspx.vb" Inherits="Modulo_AsistenciaAlumno_AsistenciaDiaria" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" >

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
    
      function pageLoad(sender, args) {

        if (args.get_isPartialLoad()) {
            imagePreview();
          
        }
    }
      
</script>

<style type="text/css">
  
 #preview{
	    position:absolute;
	    border:1px solid #ccc;
	    background:#333;
	    padding:5px;
	    display:none;
	    color:#fff;
	}	
                      
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
</style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional"  >
    
     <Triggers>
        <asp:PostBackTrigger ControlID="TabContainer1$miTab1$btnExportar" />
    </Triggers>
    
    <ContentTemplate>
   
     <div id="miContainerMantenimiento">
     
    <atk:TabContainer ID="TabContainer1" runat="server" Width="881px" ActiveTabIndex="0"
        AutoPostBack="false" ScrollBars="None" >
        
    <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" 
        Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Search" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 860px;">
                
                    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
                        <fieldset>
                            <legend>Search criteria</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 820px;">
                               
                                <tr>
                                    <td  style="width: 100px; height: 25px;" align="left" valign="middle">
                                        <span>Academic Year </span>
                                    </td>
                                    <td  style="padding-left :8px; width: 200px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarAnioAcademico" runat="server" Width="100px">
                                        </asp:DropDownList>  
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />                               
                                    </td>
                                    <td style="width: 420px; height: 25px;" align="left" valign="middle">                                    
                                                                         
                                    </td>
                                    <td style="width: 100px; height: 25px;" align="right" valign="middle">
                                        
                                    </td>
                                </tr>  
                                
                                <tr>
                                    <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                          <span >Date </span>  
                                      </td>
                                    <td style="width:200px; height: 25px;" align="left" valign="middle">                           
                                         <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                            <tr>
                                            <td align="right" valign="middle" style="width: 110px; height: 25px;">
                                                <asp:TextBox ID="tbFechaAsistencia" runat="server" BackColor ="LightYellow" 
                                                    ForeColor ="Red" Font-Size ="17px" Font-Bold ="True"
                                                    CssClass="miTextBoxCalendar" Height="18px" />    
                                                <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                    TargetControlID="tbFechaAsistencia"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                </atk:MaskedEditExtender>
                                            </td>
                                            <td align="left" valign="middle" style="width: 90px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="imageFrIni1" 
                                                    ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de Registro de Inicio." />
                                                <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                    TargetControlID="tbFechaAsistencia"
                                                    PopupButtonID="imageFrIni1" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" Enabled="True" />
                                            </td>
                                        </tr>                                                    
                                        </table>                                                  
                                    </td>
                                    <td style="width: 420px; height: 25px;" align="left" valign="middle">                                    
                                                                    
                                    </td>
                                    <td style="width: 100px; height: 25px;" align="right" valign="middle">
                                                             
                                    </td>
                                </tr>
                                     <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                        <span>Classroom</span>
                                    </td>
                                    <td  style="padding-left :8px; width: 200px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarSalon" runat="server" Width="200px" OnSelectedIndexChanged="ddlBuscarSalon_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>                                                                                      
                                    </td>
                                     <td style="width: 420px; height: 25px;" align="center" valign="middle">                                    
                                          <table >
                                            <tr>
                                                
                                                <td>
                                                     <asp:ImageButton ID="btnCancelar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'"
                                                    onclick="btnCancelar_Click" ToolTip="Limpiar Filtros"/>       
                                                </td>
                                                 <td>
                                                     <asp:ImageButton ID="btnGrabar" runat="server" Width="74px" Height="19px" 
                                                        ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" 
                                                        onclick="btnGrabar_Click" 
                                                        ToolTip="Grabar Registro"/>   
                                                </td>
                                            </tr>
                                          </table>
                                                                   
                                    </td>
                                     <td style="width: 100px; height: 25px;" align="right" valign="middle">
                                                           
                                    </td>
                                   </tr>                                    
                                
                            </table>
                        </fieldset>
                    </div>
                         
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miBusquedaActualizacion_Ficha">
                        <fieldset>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 820px;">
                                
                                <tr>
                                   <%-- <td rowspan ="2" style="border-top:solid; border-bottom:solid; border-right:solid; border-left:solid;  border-width:1px;  border-color:#a6a3a3; background-color:#FAFAB3; width: 280px; height: 21px;" align="left" valign="middle">
                                        <span> Seleccionar el <em>Bimestre</em> para las siguientes opciones: <br />
                                        <em>*</em>  Visualizar las cantidades de <em>TL,TA,TLJ,TAJ </em> .</br>
                                       <em>**</em> Exportar Los reportes.</span><br />
                                    </td>  --%>
                                  
                                    <td style="padding-left :8px; width: 100px; height: 21px;" align="left" valign="top">
                                        <span>Bimestre : </span>
                                    </td>
                                    <td style=" width: 490px; height: 21px;" align="left" valign="top">                                  
                                        <asp:DropDownList ID="ddlBimestre" runat="server" Width="200px" >
                                        </asp:DropDownList>   <span><em>( Seleccionar el <b>Bimestre</b> solo para Exportar )</em></span>                           
                                    </td>
                                    <td rowspan="4" style=" width: 230px; height: 21px;" align="left" valign="top">     
                                        <table  cellpadding="0" cellspacing="0" border="0" style=" border: solid 0x red; width:230px; ">
                                                                <tr>
                                                                   <td id="miHeaderDetCPago1" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 70px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                                     Bimestres</td>
                                                                   <td id="miHeaderDetCPago1" style="border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width:80px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                                     Fecha de Inicio</td>
                                                                   <td id="miHeaderDetCPago1" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 80px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                                     Fecha de Fin</td>
                                                                </tr>
                                                                 <tr>
                                                                   <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 40px; font-family :Arial; font-size :12px; " align="center" valign="middle"><span> 1°</span></td>
                                                                     <td style="border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                          <asp:Label ID="lblFecInicio1" runat="server"></asp:Label>
                                                                     </td>
                                                                    <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                      <asp:Label ID="lblFecFin1"  runat="server"></asp:Label>
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                   <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 70px; font-family :Arial; font-size :12px; " align="center" valign="middle"><span> 2°</span></td>
                                                                     <td style="border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                          <asp:Label ID="lblFecInicio2"  runat="server"></asp:Label>
                                                                     </td>
                                                                    <td style="border-right:solid; border-left:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                      <asp:Label ID="lblFecFin2"  runat="server"></asp:Label>
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                   <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 70px; font-family :Arial; font-size :12px; " align="center" valign="middle"><span> 3°</span></td>
                                                                     <td style="border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                          <asp:Label ID="lblFecInicio3"  runat="server"></asp:Label>
                                                                     </td>
                                                                    <td style="border-left:solid; border-right:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                      <asp:Label ID="lblFecFin3"  runat="server"></asp:Label>
                                                                     </td>
                                                                </tr>
                                                                <tr>
                                                                   <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 70px; font-family :Arial; font-size :12px; " align="center" valign="middle"><span> 4°</span></td>
                                                                     <td style="border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                          <asp:Label ID="lblFecInicio4"  runat="server"></asp:Label>
                                                                     </td>
                                                                    <td style="border-bottom:solid; border-left:solid; border-right:solid;border-width:1px;  border-color:#a6a3a3; width:80px; height: 15px;" align="center" valign="middle">
                                                                      <asp:Label ID="lblFecFin4"  runat="server"></asp:Label>
                                                                     </td>
                                                                </tr>
                                                              
                                                            </table>
                                    </td>       
                                                                                     
                                </tr>
                               <%--
                                  <tr>
                                                  
                                <td colspan="3" style="width: 820px; height: 10px;" align="right" valign="middle">
                                   &nbsp;&nbsp
                                 </td>                                                                     
                                </tr>--%>
                                
                                <tr>
                                    <td style="padding-left :8px; width: 100px; height: 21px;" align="left" valign="middle">
                                        <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" Enabled ="false"
                                            ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                            ToolTip="Exportar"
                                            OnClick="btnExportar_Click" />
                                    </td>
                                    <td  style="width: 490px; height: 21px;" align="left" valign="bottom">                                  
                                    <asp:RadioButtonList ID="rbExportar" runat="server" RepeatDirection="Horizontal" Enabled ="false">
                                        <%--<asp:ListItem Value="0" Text="Word"/>--%>
                                        <asp:ListItem Value="1" Text="Attendance" />
                                        <asp:ListItem Value="2" Text="Control de Asistencia" Selected="True"/>
                                        <asp:ListItem Value="3" Text="Consolidado de Incidencias"/>
                                    </asp:RadioButtonList>                                    
                                </td>                         
                              <%--  <td style="width: 120px; height: 25px;" align="right" valign="middle">
                                   
                                 </td>--%>                                                                     
                                </tr>
                                <tr>
                                <td colspan =2 height="14px">
                                &nbsp;&nbsp
                                </td>
                                </tr>
                                 <tr>
                                <td colspan ="2" height="29px">
                                 <asp:ImageButton ID="btnAgregarMultiple" runat="server" Width="92px" Height="29px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnAgregarVarios_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnAgregarVarios_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnAgregarVarios_1.png'" 
                                            onclick="btnAgregarMultiple_Click"
                                            ToolTip="Agregar Varios Registros" Visible="false"  />
                                </td>
                                </tr>
                            </table>
                        </fieldset>                    
                    </div>    
                                    
                    <div class="miEspacio">
                    </div>  
                      
                   <div id="miGridviewMantActualizacion_Ficha">
                      
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 840px;">
                                <tr>
                                                                   
                                    <td colspan ="3" style="width: 510px; height: 21px;" align="right" valign="top">
                                        <table id="GVLegenda_BL"  border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
                                             <tr>
                                                <td style="width: 70px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">P: Present </span></td>   
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">&#124;</span></td> 
                                                <td style="width: 70px; height: 26px;" align="left" valign="middle">
                                                    <span id="GVLegendaSpan_BL">L: Late</span></td>                              
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">&#124;</span></td>      
                                                <td style="width: 70px; height: 26px;" align="center" valign="middle">
                                                   <span id="GVLegendaSpan_BL">A: Absent</span></td>  
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">&#124;</span></td>   
                                                 <td style="width: 120px; height: 26px;" align="left" valign="middle">
                                                    <span id="GVLegendaSpan_BL">TL: Total Late</span></td> 
                                                 <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">&#124;</span></td>   
                                                  <td style="width: 120px; height: 26px;" align="left" valign="middle">
                                                    <span id="GVLegendaSpan_BL">TA: Total Absent</span></td> 
                                                   <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">&#124;</span></td>   
                                                  <td style="width: 150px; height: 26px;" align="left" valign="middle">
                                                    <span id="GVLegendaSpan_BL">TLJ: Total Late Justified</span></td> 
                                                   <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">&#124;</span></td>   
                                                   <td style="width: 150px; height: 26px;" align="left" valign="middle">
                                                    <span id="GVLegendaSpan_BL">TAJ: Total Absent Justified</span></td> 
                                                     <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">&#124;</span></td>   
                                                 <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <asp:TextBox ID="tbRojo" runat="server" CssClass="miTextBox" Width="15px" 
                                                         Height="15px" BackColor ="Red" ReadOnly ="True" />
                                                </td>         
                                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                    <span id="GVLegendaSpan_BL">Absent</span></td>  
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <asp:TextBox ID="tbVerde" runat="server" CssClass="miTextBox" Width="15px" 
                                                        Height="15px" BackColor ="Green" ReadOnly ="True" />
                                                </td>  
                                                 <td style="width: 55px; height: 26px;" align="center" valign="middle">
                                                 <span id="GVLegendaSpan_BL">Present</span></td> 
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <asp:TextBox ID="tbAmarillo" runat="server" CssClass="miTextBox" Width="15px" 
                                                        Height="15px" BackColor ="Yellow" ReadOnly ="True" />
                                                </td>  
                                                 <td style="width: 50px; height: 26px;" align="center" valign="middle">
                                                   <span id="GVLegendaSpan_BL">Late</span></td>   
                                            </tr>                        
                                      </table> 
                                     </td>                                                                     
                                </tr>
                            </table>
                    </div>                              
                    <div class="miEspacio">
                    </div>         
                             
                   <div id="">
                      <table  cellpadding="0" cellspacing="0" border="0" style="padding-left :10px; border: solid 0x red; width: 850px;">
                            <tr>
                                       <td align="left" valign="top">
                                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width:650px;">
                                                <tr>
                                                   <td id="miHeaderDetCPago2" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 30px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                   Order</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width: 240px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    Student</td>
                                                    <td id="miHeaderDetCPago2" style="border-left:solid; border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    P</td>
                                                    <td id="miHeaderDetCPago2" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    L</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid; border-right:solid;  border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    A</td>
                                                    <td id="miHeaderDetCPago2" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    LJ</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid; border-right:solid;  border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    AJ</td>
                                                    <td id="miHeaderDetCPago2" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    TL</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid; border-right:solid;  border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    TA</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid; border-right:solid;  border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    TLJ</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid; border-right:solid;  border-width:1px;  border-color:#a6a3a3;  width: 40px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    TAJ</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid; border-right:solid;  border-width:1px;  border-color:#a6a3a3;  width: 20px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    J</td>
                                                </tr>
                                                <tr>
                                                    <td colspan ="12" style="border-bottom:solid; border-left:solid; border-right:solid;   border-width:1px;  border-color:#a6a3a3; ">
                                                   
                                                      <asp:GridView ID="GridView1" runat="server" 
                                                            Width="650px" 
                                                            CssClass="miGridviewBusqueda" 
                                                            GridLines="None" 
                                                            AutoGenerateColumns="False" 
                                                            AllowSorting="True"
                                                            EmptyDataText=" - No se encontraron resultados - "
                                                            OnRowDataBound="GridView1_RowDataBound"
                                                            OnRowCommand="GridView1_RowCommand"
                                                            ShowHeader="False"  >
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                                                            <Columns>     
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoRegistroAsistencia" runat="server" Text='<%# Bind("CodigoRegistroAsistencia") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>
                                                                                                                    
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRutaFoto" runat="server" Text='<%# Bind("RutaFoto") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID_TABLA" runat="server" Text='<%# Bind("ID_TABLA") %>' />
                                                                    </ItemTemplate>
                                                                   <ItemStyle   CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="35px" />
                                                                </asp:TemplateField>  
                                                                
                                                                <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNombreAlumno" runat="server" Text='<%# Bind("NombreAlumno") %>' />
                                                                    </ItemTemplate>
                                                                   <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="245px" />
                                                                </asp:TemplateField>  
                                                                
                                                                   <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                       <a onclick ="return false;" class="preview" id="btnVerPortada" runat="server" >
                                                                         <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                                </asp:TemplateField>                                                                                                                                   
                                                                                                                                       
                                                                <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                       <table >
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:RadioButton ID="rdb_LisAsistentes"  runat="server" GroupName="LisAsistentes" Width ="40px"  Height="15px" OnCheckedChanged ="rdb_LisAsistentes_OnCheckedChanged" AutoPostBack="true"/>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="rdb_LisTardanzas" runat="server" GroupName="LisAsistentes" Width ="40px" Height="15px" OnCheckedChanged ="rdb_LisTardanzas_OnCheckedChanged" AutoPostBack="true"/>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButton ID="rdb_LisFaltas" runat="server" GroupName="LisAsistentes" Width ="40px" Height="15px" OnCheckedChanged ="rdb_LisFaltas_OnCheckedChanged" AutoPostBack="true"/>
                                                                                </td>
                                                                            </tr>
                                                                       </table>                                                              
                                                                     </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="120px" />
                                                                </asp:TemplateField>
                                                                
                                                              <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTardanzasJust" runat="server" Text="0" /> 
                                                                      <%--  <asp:CheckBox ID="check_LisTardanzas" runat="server" Width="15px"  OnCheckedChanged="check_LisTardanzas_CheckedChanged" AutoPostBack="true"
                                                                            Height="15px" />--%>
                                                                    </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
                                                                </asp:TemplateField>  
                                                                
                                                               <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFaltasJust" runat="server" Text="0" /> 
                                                                      <%--  <asp:CheckBox ID="check_LisTardanzas" runat="server" Width="15px"  OnCheckedChanged="check_LisTardanzas_CheckedChanged" AutoPostBack="true"
                                                                            Height="15px" />--%>
                                                                    </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
                                                                </asp:TemplateField>  
                                                                     
                                                            <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTardanzas" runat="server" Text='<%# Bind("TotalTardanzas") %>' /> 
                                                                      <%--  <asp:CheckBox ID="check_LisTardanzas" runat="server" Width="15px"  OnCheckedChanged="check_LisTardanzas_CheckedChanged" AutoPostBack="true"
                                                                            Height="15px" />--%>
                                                                    </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="45px" />
                                                                </asp:TemplateField>  
                                                                                                                                                                                                             
                                                                <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFaltas" runat="server" Text='<%# Bind("TotalFaltas") %>' /> 
                                                                        <%-- <asp:CheckBox ID="check_LisFaltas" runat="server" Width="15px" 
                                                                            Height="15px"/>--%>
                                                                    </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="45px" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTardanzasJustificadas" runat="server" Text='<%# Bind("TotalTardanzasJustificadas") %>' /> 
                                                                    </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="45px" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFaltasJustificadas" runat="server" Text='<%# Bind("TotalFaltasJustificadas") %>' /> 
                                                                    </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="45px" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                          <asp:ImageButton ID="btnJustificacion" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_agregar.png" 
                                                                            CommandName="Justificacion" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Justificacion" />
                                                                    </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>  
                                                                  
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoMotivoGV" runat="server" Text='<%# Bind("CodigoMotivo") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>  
                                                                
                                                                 <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoMedioUsoGV" runat="server" Text='<%# Bind("CodigoMedioUso") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>  
                                                                   
                                                                 <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoObservacionGV" runat="server" Text='<%# Bind("Observacion") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>  
                                                                
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoEventoAsistenciaGV" runat="server" Text='<%# Bind("CodigoEventoAsistencia") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>  
                                                                   
                                                              </Columns>  
                                                              
                                                        </asp:GridView>
                                                   
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                             <atk:ModalPopupExtender ID="pnModalAgregarRegistro" runat="server"
                        TargetControlID="VerAgregarRegistro"
                        PopupControlID="pnlAgregarRegistro"
                        BackgroundCssClass="MiModalBackground" 
                        OkControlID="OKAgregarRegistro" 
                        CancelControlID="CancelAgregarRegistro"
                        Drag="true" 
                        PopupDragHandleControlID="AgregarRegistroHeader" />           

                                             <asp:panel id="pnlAgregarRegistro" BackColor="White" BorderColor="Black" runat="server">
                          <div id="miPanelGridview" style="width: 445px; height: 620px; overflow-y: scroll; overflow-x: hidden;">
                          <table cellpadding="0" cellspacing="0" border="0" width="430px" id="panelRegistro">    
                            <tr  id="AgregarRegistroHeader" style ="cursor :pointer; ">
                                <td style="width: 410px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="4">                
                                     <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Register of Latenesses and absences</span>

                                </td>
                                <td style="width: 20px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                                    <asp:ImageButton ID="btnCerraAgregarRegistro" runat="server" Width="16" Height="15"
                                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                        ToolTip="Cerrar Panel"/>
                                </td>
                            </tr>
                            <tr>
                               <td colspan="5" style="height: 15px;" align="right">
                                        <%-- onclick="btnCerrarModal_Click"<em style ="font-size:small;">Campos Obligatorios (*)</em>--%>
                               </td>
                            </tr>   
                            <tr>
                                    <td  style="padding-left :5px; width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Academic Year :</span>
                                    </td>
                                    <td style="padding-left :8px; width: 230px; height: 25px;" align="left" valign="middle">
                                       <asp:Label ID="lblAnio" runat="server" Width="200px" />    
                                    </td>
                                    <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                        
                                    </td>
                                </tr>  
                                <tr>
                                    <td style="padding-left :5px; width: 150px; height: 25px;" align="left" valign="middle">
                                          <span >Date :</span>  
                                      </td>
                                    <td style=" width:230px; height: 25px;" align="left" valign="middle">                           
                                         <%--<asp:Label ID="lblFecha" runat="server" Width="200px" />--%>   
                                         <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                            <tr>
                                            <td align="right" valign="middle" style="width: 110px; height: 25px;">
                                                <asp:TextBox ID="tbFechaJustificacionTA" runat="server" BackColor ="LightYellow" 
                                                    ForeColor ="Red" Font-Size ="17px" Font-Bold ="True"
                                                    CssClass="miTextBoxCalendar" Height="18px" />    
                                                <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                    TargetControlID="tbFechaJustificacionTA"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                </atk:MaskedEditExtender>
                                            </td>
                                            <td align="left" valign="middle" style="width: 90px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="imageFrIni2" 
                                                    ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de Registro de Inicio." />
                                                <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                    TargetControlID="tbFechaJustificacionTA"
                                                    PopupButtonID="imageFrIni2" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" Enabled="True" />
                                            </td>
                                        </tr>                                                    
                                        </table>
                                                                                             
                                    </td>
                                    <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                                             
                                    </td>
                                </tr>
                                <tr>
                                     <td style="padding-left :5px; width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Classroom :</span>
                                    </td>
                                    <td  style="padding-left :8px; width: 230px; height: 25px;" align="left" valign="middle">
                                          <asp:Label ID="lblSalon" runat="server" Width="200px" />                                                                                       
                                    </td>
                                     <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                                           
                                    </td>
                                 </tr>                                    
                                <tr>
                                     <td style="padding-left :5px; width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Student :</span>
                                    </td>
                                    <td colspan="2" style="padding-left :8px; width: 280px; height: 25px;" align="left" valign="middle">
                                          <asp:Label ID="lblNombreCompleto" runat="server" Width="280px" />  
                                          <asp:Label ID="lblCodigoAlumno" runat="server"  style="display:none " /> 
                                          <asp:Label ID="lblCodigoRegistroAsistenciaJust" runat="server" text="0" style="display:none " /> 
                                    </td>
                                    
                                   </tr>
                                <tr>
                                     <td style="padding-left :5px; width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Type :</span>
                                    </td>
                                    <td  style="padding-left :8px; width: 230px; height: 25px;" align="left" valign="middle">
                                         <asp:RadioButtonList ID="rbTipo" runat="server" Width="200px" RepeatDirection="Horizontal" >
                                            <asp:ListItem Value="5" Text="Late" Selected="True" />
                                            <asp:ListItem Value="4" Text="Absent" />
                                        </asp:RadioButtonList>                                                                                
                                    </td>
                                    <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                                           
                                    </td>
                                   </tr>
                                <tr>
                                     <td style="padding-left :5px; width: 150px; height: 25px;" align="left" valign="top">
                                        <span>Medium used :</span>
                                    </td>
                                    <td  style="padding-left :8px; width: 230px; height: 25px;" align="left" valign="middle">
                                        <div style ="border:solid 1px #a6a3a3;  width :240px; ">
                                            <asp:RadioButtonList ID="rbMedioUsado" runat="server" Width="250px" >
                                            </asp:RadioButtonList>     
                                        </div>                                                                                 
                                    </td>
                                  
                                     <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                       
                                   </td>
                                   </tr> 
                                <tr>   
                                    <td colspan ="3" style="width:430px; height: 10px;" align="right" valign="middle">
                                                           
                                    </td>
                                   </tr>     
                                <tr>
                                     <td style="padding-left :5px; width: 150px; height: 25px;" align="left" valign="top">
                                        <span>Reason :</span>  <asp:ImageButton ID="btnAddReason" 
                                        runat="server" ImageUrl="~/App_Themes/Imagenes/opc_agregar.png" 
                                        ToolTip ="Add Reason" 
                                        OnClick ="btnAddReason_Click"
                                        Visible="false"/>
                                    </td>
                                    <td  style="padding-left :8px; width: 230px; height: 25px;" align="left" valign="middle">
                                        <div style ="border:solid 1px #a6a3a3;  width :240px; ">
                                            <asp:RadioButtonList ID="rbMotivo" runat="server" Width="250px" >
                                            </asp:RadioButtonList>  
                                        </div>                                                                                    
                                    </td>
                                    
                                     <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                                        
                                    </td>
                              </tr>
                               <tr>   
                                    <td colspan ="3" style="width:430px; height: 10px;" align="right" valign="middle">
                                                           
                                    </td>
                                   </tr> 
                               <tr>
                                     <td style="padding-left :5px; width: 150px; height: 25px;" align="left" valign="top">
                                        <span>Observations :</span>
                                    </td>
                                    <td  style="padding-left :8px; width: 230px; height: 25px;" align="left" valign="middle">
                                        <div style ="border:solid 1px #a6a3a3;  width :240px; ">
                                            <asp:TextBox  ID ="tbObservacionJust" runat ="server"
                                             Width="240px" style="font-size: 8pt; font-family: Arial; 
                                              height:150px; "  MaxLength="400" Rows="4" TextMode="MultiLine"   />
                                        </div>                                                                                    
                                    </td>
                                    
                                     <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                                           
                                    </td>
                              </tr> 
                                <tr>   
                                    <td colspan ="3" style="width:430px; height: 10px;" align="right" valign="middle">
                                                           
                                    </td>
                                   </tr> 
                              <tr>
                                     <td style="padding-left :5px; width: 150px; height: 25px;" align="right" valign="top">
                                         <asp:ImageButton ID="btnGrabarJustificacion" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                            onclick="btnGrabarJustificacion_Click" />
                                    </td>
                                    <td  style="padding-left :8px; width: 230px; height: 25px;" align="center" valign="middle">
                                             <asp:ImageButton ID="btnCancelarJustificacion" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                                            onclick="btnCancelarJustificacion_Click" CausesValidation="false"/>                                                                              
                                    </td>
                                    
                                     <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                                           
                                    </td>
                              </tr> 
                                        
                            <tr><td colspan="5"><br /></td></tr>          
                        </table>  
                          </div> 
                          
                          <div id="controlAgregarRegistro" style="display:none">
                          
            <input type="button" id="VerAgregarRegistro" runat="server" />
            <input type="button" id="OKAgregarRegistro" />
            <input type="button" id="CancelAgregarRegistro" />
        </div>       
                        </asp:panel>  
                        
                                            <atk:ModalPopupExtender ID="pnModalAgregarMotivo" runat="server"
                                                TargetControlID="VerAgregarMotivo"
                                                PopupControlID="pnlAgregarMotivo"
                                                BackgroundCssClass="MiModalBackground" 
                                                OkControlID="OKAgregarMotivo" 
                                                CancelControlID="CancelAgregarMotivo"
                                                Drag="true" 
                                                PopupDragHandleControlID="AgregarMotivoHeader" />
                                           
                                           <asp:panel id="pnlAgregarMotivo" BackColor="White" BorderColor="Black" runat="server">
                          <table cellpadding="0" cellspacing="0" border="0" width="430px" id="Table1">    
                            <tr  id="AgregarMotivoHeader" style ="cursor :pointer; ">
                                <td style="width: 410px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="4">                
                                     <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Register of Reason</span>

                                </td>
                                <td style="width: 20px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                                    <asp:ImageButton ID="btnCerrarAgregarMotivo" runat="server" Width="16" Height="15"
                                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                        ToolTip="Cerrar Panel"/>
                                </td>
                            </tr>
                            <tr>
                               <td colspan="5" style="height: 15px;" align="right">
                                        <%-- onclick="btnCerrarModal_Click"<em style ="font-size:small;">Campos Obligatorios (*)</em>--%>
                               </td>
                            </tr>   
                            <tr>
                                    <td  style="padding-left :5px; width: 150px; height: 50px;" align="left" valign="middle">
                                        <span>Description :</span>
                                    </td>
                                    <td style="padding-left :8px; width: 230px; height: 50px;" align="left" valign="middle">
                                       <asp:TextBox ID="tbDescMotivo" runat="server" CssClass="miTextBox" Width="290px"
                                            Height="35px" Rows="2" TextMode="MultiLine"/>    
                                    </td>
                                    <td style="width: 50px; height: 50px;" align="right" valign="middle">
                                        
                                    </td>
                                </tr>  
                                <tr>   
                                    <td colspan ="3" style="width:430px; height: 10px;" align="right" valign="middle">
                                                           
                                    </td>
                                   </tr> 
                              <tr>
                                     <td style="padding-left :5px; width: 150px; height: 25px;" align="right" valign="top">
                                       
                                    </td>
                                    <td  style="padding-left :8px; width: 230px; height: 25px;" align="center" valign="middle">
                                              <asp:ImageButton ID="btnGrabarMotivo" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                            onclick="btnGrabarMotivo_Click" />
                                             <asp:ImageButton ID="btnCancelarMotivo" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                                            onclick="btnCancelarMotivo_Click" CausesValidation="false"/>                                                                              
                                    </td>
                                    
                                     <td style="width: 50px; height: 25px;" align="right" valign="middle">
                                                           
                                    </td>
                              </tr> 
                                        
                            <tr><td colspan="5"><br /></td></tr>          
                        </table>  
                          <div id="controlAgregarMotivo" style="display:none">
            <input type="button" id="VerAgregarMotivo" runat="server" />
            <input type="button" id="OKAgregarMotivo" />
            <input type="button" id="CancelAgregarMotivo" />
        </div>       
                        </asp:panel>
                        
                        
                        
                                        </td>
                                        <td width="130px">
                                                                                          
                                        </td>
                                        <td align="right" valign="top">
                                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width:140px;">
                                                <tr>
                                                   <td  colspan ="2" id="miHeaderDetCPago2" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width: 30px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                   Summary</td>
                                                </tr>
                                                 <tr>
                                                   <td style="border-left:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 80px; font-family :Arial; font-size :12px; " align="left" valign="middle"><span> Present:</span></td>
                                                     <td style="border-right:solid; border-width:1px;  border-color:#a6a3a3; width:60px; height: 32px;" align="center" valign="middle">
                                                     <asp:TextBox ID="tbAsistentes" runat="server" CssClass="miTextBox" Width="25px" 
                                                             Height="17px" BackColor ="LightYellow" ReadOnly ="True" />
                                                     </td>
                                                </tr>
                                                <tr>
                                                   <td style="border-left:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 80px; font-family :Arial; font-size :12px; " align="left" valign="middle"><span> Late:</span></td>
                                                     <td style="border-right:solid; border-width:1px;  border-color:#a6a3a3; width:60px; height: 32px;" align="center" valign="middle">
                                                     <asp:TextBox ID="tbTardanzas" runat="server" CssClass="miTextBox" Width="25px" 
                                                             Height="17px" BackColor ="LightYellow" ReadOnly ="True" />
                                                     </td>
                                                </tr>
                                                  <tr>
                                                   <td style="border-left:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 80px; font-family :Arial; font-size :12px; " align="left" valign="middle"><span> Absent:</span></td>
                                                     <td style="border-right:solid; border-width:1px;  border-color:#a6a3a3; width:60px; height: 32px;" align="center" valign="middle">
                                                     <asp:TextBox ID="tbfaltas" runat="server" CssClass="miTextBox" Width="25px" 
                                                             Height="17px" BackColor ="LightYellow" ReadOnly ="True" />
                                                     </td>
                                                </tr>
                                                 <tr>
                                                   <td style="border-left:solid; border-width:1px;  border-color:#a6a3a3; padding-left :5px; width: 80px; height: 32px; font-family :Arial; font-size :12px;" align="left" valign="middle"><span> Late Justified:</span></td>
                                                   <td style="border-right:solid; border-width:1px;  border-color:#a6a3a3; width: 60px; height: 32px;" align="center" valign="middle">
                                                     <asp:TextBox ID="tbTardanzasJust" runat="server" CssClass="miTextBox" 
                                                           Width="25px" Height="17px" BackColor ="LightYellow" ReadOnly ="True" />
                                                     </td>
                                                </tr>
                                                 <tr>
                                                   <td style="border-left:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3;   padding-left :5px; width: 80px; height: 32px;  font-family :Arial; font-size :12px;" align="left" valign="middle"><span> Absent Justified:</span> </td>
                                                   <td style="border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width: 60px; height: 32px;" align="center" valign="middle">
                                                   <asp:TextBox ID="tbFaltasJust" runat="server" CssClass="miTextBox" Width="25px" 
                                                           Height="17px" BackColor ="LightYellow" ReadOnly ="True" />
                                                   </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                      </table>
                   </div>                 
                                          
                </div>
            </ContentTemplate>
    </atk:TabPanel> 
        
    </atk:TabContainer>
    
        
     <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="71px" Width="388px"  style="display:none">
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="text-align:right; ">
                                                        <asp:ImageButton ID="btnVolver" runat="server" 
                                                            ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10px; color: #000080; text-align: center;">
                                                        
                                                            <img alt="" src="../App_Themes/Imagenes/bigrotation2.gif" 
                                                            style="width: 32px; height: 32px" />
                                                        
                                                            <br />
                                                    
                                                    Exportando, espere unos segundos ...
                                                                                                
                                                    </td>
                                                </tr>    
                                        
                                            </table>
                                        </asp:Panel>
                            
    <atk:ModalPopupExtender ID="ModalPopupExtender1" 
                                        runat="server"
                                        DynamicServicePath="" 
                                        Enabled="True" 
                                        BackgroundCssClass="FondoAplicacion"
                                        PopupControlID="pnlImpresion"                    
                                        TargetControlID="lblAccionExportar"
                                        >
                                        </atk:ModalPopupExtender>
                                        
    <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..."></asp:Label>    
    
    </div>        
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript">

    $(document).ready(function() {

        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);
            
    });

</script>

</div>

</asp:Content>

