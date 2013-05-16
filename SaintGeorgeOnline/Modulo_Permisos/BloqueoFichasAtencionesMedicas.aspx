<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="BloqueoFichasAtencionesMedicas.aspx.vb" Inherits="Modulo_Permisos_BloqueoFichasAtencionesMedicas" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 21px;
            }
        .style2
        {
            height: 25px;
            width: 159px;
        }
    </style>
    
    <style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
        .style3
        {
            height: 28px;
            width: 159px;
        }
        .style4
        {
            width: 260px;
            height: 28px;
        }
    
        .style5
        {
            height: 21px;
            width: 269px;
        }
    
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <div class="miEspacio">
    </div>  
                        
    <div id="miContainerMantenimiento">
        <atk:TabContainer ID="TabContainer1" runat="server" Width="670px" ActiveTabIndex="0"
        AutoPostBack="false" ScrollBars="None" >
            <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Configuración" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 650px;">
                    <div id="miBusquedaMant">
                        <fieldset>
                            <legend>Bloqueos Generales</legend>
                            <table cellpadding="0" cellspacing="0" border="0" 
                                style="min-width: 610px; width: 606px;">
                                <tr>
                                    <td colspan="2" style="height:10px">
                                        &nbsp;
                                        <asp:HiddenField ID="hidencodigoBloqueo" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style5">
                                        Intervalo de días de plazo de modificación
                                    </td>
                                    <td style="min-width: 300px; height: 21px;" align="left">
                                        <asp:TextBox ID="tbDiasPlazo" runat="server" CssClass="miTextBox" 
                                            Width="40px" Height="20px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style5">
                                        Omitir Fichas Pendientes
                                    </td>
                                    <td style="min-width: 300px; height: 21px;" align="left">
                                        <asp:RadioButtonList ID="rbOmision" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="Sí" Selected="True" />
                                            <asp:ListItem Value="0" Text="No" />
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style1" colspan="2">
                                        
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <div class="miEspacio">
                        </div>  
                        <fieldset>
                            <legend>Listado de Excepciones</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="590" >
                                <tr>
                                    <td height="10px">
                                       &nbsp;
                                        <asp:HiddenField ID="hidencodigoExcepcion" runat="server" />
                                    </td> 
                                </tr> 
                                <tr>
                                    <td style="width: 590px;" align="center" valign="top">
                                                
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 585px">
                                            <tr>
                                                 <td style="width: 200px; height: 26px; text-align:right  ; color:White;font-size:10px;padding-right:20px " 
                                                     align="left" class="miGVBusquedaFicha_Header">
                                                     Codigo de Ficha
                                                 </td>
                                                 <td style="height: 26px; width: 100px; text-align:left; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                     Fecha de Excepción
                                                 </td>
                                                 <td style="height: 26px; width: 50px; text-align:left; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                     Días de Excepción
                                                 </td>  
                                                 <td style="height: 26px; width: 100px; text-align:left; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                     Vigencia
                                                 </td>              
                                                 <td style="width: 30px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                     <asp:ImageButton ID="btn_Add_Excepcion" runat="server" Width="20px" Height="20px"
                                                                      ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                                                                      OnClick="btn_Add_Excepcion_Click"                                                    
                                                                      ToolTip="Agregar"/> 
                                                 </td>
                                                              
                                            </tr>
                                                            
                                            <tr>  
                                                 <td style="width: 400px; height: 25px" align="center" valign="top" colspan="5">
                                                      <asp:UpdatePanel ID="upExcepciones" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>                                                                                 
                                                                  <div id="Div2">
                                                                        <asp:GridView ID="gvDetalleExcepciones" runat="server" 
                                                                                    CssClass="miGVBusquedaFicha" 
                                                                                    GridLines="None" 
                                                                                    AutoGenerateColumns="False"
                                                                                    AllowPaging="False" 
                                                                                    AllowSorting="False"
                                                                                    EmptyDataText=" - No se encontraron resultados - "
                                                                                    OnRowCommand="gvDetalleExcepciones_RowCommand"
                                                                                    OnRowDataBound="gvDetalleExcepciones_RowDataBound"
                                                                                    ShowHeader="False"
                                                                                    ShowFooter="False" Width="573px"
                                                                                    >
                                                                                    <Columns>     
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" Visible="true" 
                                                                                                    CommandName="Actualizar" CommandArgument='<%# Bind("CodigoExcepcion") %>' ToolTip="Actualizar Registro" />
                                                                                             </ItemTemplate>                                                                                           
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png"  Visible="true" 
                                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("CodigoExcepcion") %>' ToolTip="Eliminar Registro" />
                                                                                            </ItemTemplate>                                                                                           
                                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                                        </asp:TemplateField>
                                                                                       
                                                                                        <asp:TemplateField HeaderText="Correlativo">                                                                      
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblCorrelativo_grilla" runat="server" Text='<%# Bind("Correlativo") %>' />
                                                                                                       
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                        </asp:TemplateField>
                                                                                                
                                                                                                <asp:TemplateField HeaderText="CodigoExcepcion">                                                                      
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblCodigoExcepcion_grilla" runat="server" Text='<%# Bind("CodigoExcepcion") %>' />
                                                                                                       
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0" />
                                                                                                </asp:TemplateField>
                                                                                                  
                                                                                                <asp:TemplateField >                                     
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblCodigoFichaAtencion_grilla" runat="server" Text='<%# Bind("CodigoFichaAtencion") %>' />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="center" Width="100px" />
                                                                                            </asp:TemplateField>                                                                         
                                                                                                               
                                                                                            <asp:TemplateField  >                                                                                                         
                                                                                                <ItemTemplate  >
                                                                                                    <asp:Label  ID="lblFechaExclusion_grilla" runat="server" Text='<%# Bind("FechaExclusion") %>' />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle  CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="70px"  />
                                                                                            </asp:TemplateField>    
                                                                                            <asp:TemplateField  >                                                                                                         
                                                                                                <ItemTemplate  >
                                                                                                    <asp:Label  ID="lblDiasExclusion_grilla" runat="server" Text='<%# Bind("DiasExclusion") %>' />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle  CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="50px"  />
                                                                                            </asp:TemplateField>   
                                                                                            <asp:TemplateField  >                                                                                                         
                                                                                                <ItemTemplate  >
                                                                                                    <asp:Label  ID="lblVigenciaExclusion_grilla" runat="server" Text='<%# Bind("Vigencia") %>' />
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle  CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="100px"  />
                                                                                            </asp:TemplateField>                                                                                  
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                  </div>           
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>   
                                                 </td>                                                        
                                            </tr> 
                                            <tr>
                                                 <td colspan="5">
                                                     <asp:Panel ID="pnl_PopUp_Excepcion" BackColor="White" BorderColor="Black" runat="server">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                                            <tr>
                                                                <td style="width: 360px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                                    <span id="EnfermedadHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial">
                                                                    Agregar Excepción</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" height="10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" class="style3">
                                                                     <span style="padding-left:10px">Codigo de Ficha&nbsp;</span>
                                                                     
                                                                     </td>
                                                                <td align="left" class="style4">
                                                                     <asp:TextBox ID="tbCodigoFicha" runat="server" Height="22px" 
                                                                         style="margin-left: 0px" Width="50px" CssClass="miTextBox"></asp:TextBox>
                                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                                                                                  FilterType="Numbers" 
                                                                         TargetControlID="tbCodigoFicha" Enabled="True">
                                                                     </atk:FilteredTextBoxExtender>   
                                                                </td>
                                                            </tr> 
                                                            <tr>
                                                                <td align="left" class="style2" valign="middle">
                                                                    <span style="padding-left:10px">Fecha de Exclusión&nbsp;</span></td>
                                                                <td align="left" style="width: 260px; height: 25px">
                                                                    <asp:TextBox ID="tbFechaRegistroExcepcion" runat="server" CssClass="miTextBoxCalendar" />
                                                                    <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                                        TargetControlID="tbFechaRegistroExcepcion"
                                                                        UserDateFormat="DayMonthYear"                                                                    
                                                                        Mask="99/99/9999" 
                                                                        MaskType="Date" 
                                                                        PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                                    </atk:MaskedEditExtender> 
                                                                    <asp:ImageButton runat="server" ID="imageBF1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                                    <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                        TargetControlID="tbFechaRegistroExcepcion"
                                                                        PopupButtonID="imageBF1" 
                                                                        Format="dd/MM/yyyy" 
                                                                        CssClass="MyCalendar" Enabled="True" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle" class="style2">
                                                                     <span style="padding-left:10px">Días de exclusión&nbsp;</span>
                                                                </td>
                                                                <td style="width: 260px; height: 25px" align="left">
                                                                     <asp:TextBox ID="tbdiasexclusion" runat="server" CssClass="miTextBox" 
                                                                                  Width="50px" ></asp:TextBox>
                                                                     <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" 
                                                                                                  FilterType="Numbers" 
                                                                         TargetControlID="tbdiasexclusion" Enabled="True">
                                                                     </atk:FilteredTextBoxExtender>           
                                                                </td>            
                                                            </tr> 
                                                            <tr>
                                                                <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="2">
                                                                     <asp:ImageButton ID="popup_btnAgregar_Excepcion" runat="server" Width="84px" Height="19px"
                                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                                                                                                                            onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                                                                                                                            OnClick="popup_btnAgregar_Excepcion_Click"
                                                                                                                            ToolTip="Aceptar" />&nbsp;
                                                                     <asp:ImageButton ID="popup_btnCancelar_Excepcion" runat="server" Width="84px" Height="19px"
                                                                                                                            ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                                                                                                                            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                                                                                            OnClick="popup_btnCancelar_Excepcion_Click"
                                                                                                                            ToolTip="Cancelar" />
                                                                </td>
                                                            </tr>      
                                                            <tr>
                                                                <td colspan="2" height="10px"></td>
                                                            </tr>     
                                                                                                                                   
                                                        </table>  
                             <div id="controlEnfermedad" style="display:none">
                                <input type="button" id="okEnfermedad" />
                                <input type="button" id="CancelEnfermedad" />
                             </div>
                        </asp:Panel>  
                        
                                                     <atk:ModalPopupExtender 
                                                        ID="modal_xxx" 
                                                        runat="server"
                                                        DynamicServicePath="" 
                                                        Enabled="True" 
                                                        BackgroundCssClass="FondoAplicacion"
                                                        DropShadow="True"
                                                        PopupControlID="pnl_PopUp_Excepcion"                    
                                                        TargetControlID="lblAbrePopUp"
                                                                        
                                                        >
                                                     </atk:ModalPopupExtender>
                                                 </td>
                                            </tr>   
                                                                                      
                                        </table> 
                                                           
                                        <div style="display:none">
                                             <asp:Button ID="btnMostrarEnfermedad" runat="server" Text="Button" />      
                                        </div>
                                                
                                    </td>
                                </tr>                                             
                            </table>     
                            </fieldset>
                    </div>                      
                    <div class="miEspacio" style="text-align:right  " >
                        <asp:Label ID="lblAbrePopUp" runat="server"></asp:Label>
                    </div>                
                    <div id="miFooterDetalleMant">
                        <asp:ImageButton ID="btnGrabar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                            onclick="btnGrabar_Click" />
                    </div>  
                    <div class="miEspacio">
                        
                    </div>
                </div>
            </ContentTemplate>
        </atk:TabPanel>         
    </atk:TabContainer>     
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

