<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ValidarFichaFamiliar.aspx.vb" Inherits="Modulo_Matricula_ValidarFichaFamiliar" title="Página sin título" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
    .noList{
        list-style: none;
    }    
    .miPaddingRow{
        padding-left:10px;
        /*border-bottom: solid 1px black;*/
        display: block;    
        margin: 5px 0px 5px 0px;  
        list-style: none; 
    }
    .miLegendRow{
        padding: 4px 0px 0px 10px;
        text-align:left;
        font-size: 11px;
        font-family: Arial;
        font-weight: bold;
        height: 21px;
        background: url(../App_Themes/Imagenes/legend_header.gif) repeat-x;
        border: solid 1px #707070;
    }    
    .miRowBorder
    {
        border-bottom: solid 1px black;    
        border-top: solid 1px black;    
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
<div id="miPaginaActualizacionBusqueda">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div id="miContainerActualizacion_Ficha">
     
    <atk:TabContainer ID="TabContainer1" runat="server" Width="881px" ActiveTabIndex="0"
        AutoPostBack="false" ScrollBars="None" >    
                             
        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 650px;">
                
                    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 1x red; width: 800px;">
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Fecha Solicitud</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                    <table border="0" cellpadding="0" cellspacing="0" width="360px">
                                        <tr>
                                            <td align="left" valign="middle" style="width: 110px; height: 25px;">
                                                <asp:TextBox ID="tbFechaSolicitudInicial" runat="server" CssClass="miTextBoxCalendar" Height="18px" />    
                                                <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                    TargetControlID="tbFechaSolicitudInicial"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-">
                                                </atk:MaskedEditExtender>                                        
                                            </td>
                                            <td align="left" valign="middle" style="width: 30px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="image1" 
                                                    ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de solicitud inicial." />
                                                <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                    TargetControlID="tbFechaSolicitudInicial"
                                                    PopupButtonID="image1" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" />
                                            </td>
                                            <td align="left" valign="middle" style="width: 40px; height: 25px;">
                                                <span>hasta</span>
                                            </td>
                                            <td align="left" valign="middle" style="width: 110px; height: 25px;">
                                                <asp:TextBox ID="tbFechaSolicitudFinal" runat="server" CssClass="miTextBoxCalendar" Height="18px" />    
                                                <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                    TargetControlID="tbFechaSolicitudFinal"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-">
                                                </atk:MaskedEditExtender>                                        
                                            </td>
                                            <td align="left" valign="middle" style="width: 70px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="image2" 
                                                    ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de solicitud final." />
                                                <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                    TargetControlID="tbFechaSolicitudFinal"
                                                    PopupButtonID="image2" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" />
                                            </td>
                                        </tr>
                                    </table> 
                                    </td>  
                                    <td style="width: 100px;" align="right" valign="top">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Estado</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="bottom">
                                        <asp:RadioButtonList ID="rbEstados" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="-1" Text="Todos" />                                            
                                            <asp:ListItem Value="0" Text="Eliminado" />  
                                            <asp:ListItem Value="1" Text="Pendiente" Selected="True" />
                                            <asp:ListItem Value="2" Text="Validado" />                                       
                                        </asp:RadioButtonList>                                        
                                    </td>
                                    <td style="width: 100px;" align="right" valign="top">
                                    </td>
                                </tr>                                
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Apellido Paterno</span>
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbBuscarApellidoPaterno" runat="server" CssClass="miTextBox" 
                                            Width="320px" MaxLength="100" Height="18px" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarApellidoPaterno" 
                                            ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>                                     
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                         <asp:ImageButton ID="btnBuscar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/> 
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Apellido Materno</span>
                                    </td>
                                    <td style="min-width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbBuscarApellidoMaterno" runat="server" CssClass="miTextBox" 
                                            Width="320px" MaxLength="100" Height="18px" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarApellidoMaterno" 
                                            ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>                                     
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                         <asp:ImageButton ID="btnLimpiar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>     
                                    </td>
                                </tr> 
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre</span>
                                    </td>
                                    <td style="min-width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="tbBuscarNombre" runat="server" CssClass="miTextBox" 
                                            Width="320px" MaxLength="100" Height="18px" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarNombre" 
                                            ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>                                     
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                    </td>
                                </tr>  
                                                   
                            </table>
                        </fieldset>
                    </div>
                              
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miGridviewMantActualizacion_Ficha">                    
                        <asp:GridView ID="GVListaFamiliar" runat="server"                 
                            Width="840" 
                            CssClass="miGridviewBusquedaPersona" 
                            GridLines="none" 
                            AutoGenerateColumns="False"                      
                            ShowFooter="false"
                            ShowHeader="true"  
                            AllowPaging="True" 
                            AllowSorting="true"
                            PageSize="10"                   
                            EmptyDataText=" - No se encontraron resultados - "
                            EmptyDataRowStyle-ForeColor="#a51515"
                            EmptyDataRowStyle-HorizontalAlign="Center"
                            OnRowDataBound="GVListaFamiliar_RowDataBound"
                            OnRowCommand="GVListaFamiliar_RowCommand"
                            OnPageIndexChanging="GVListaFamiliar_PageIndexChanging"                             
                            OnSorting="GVListaFamiliar_Sorting" 
                            OnRowCreated="GVListaFamiliar_RowCreated">
                            <HeaderStyle CssClass="miGridviewBusquedaActualizacion_Ficha_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                           
                            <Columns>            
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                            CommandName="Seleccionar" CommandArgument='<%# Bind("CodigoFamiliar_FamiliarActualizar") %>' ToolTip="Seleccionar Familiar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnVerFamiliares" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver_familiares.png" Width="20" Height="20"
                                            CommandName="VerFamiliares" CommandArgument='<%# Bind("CodigoFamiliar_FamiliarActualizar") %>' ToolTip="Ver Familiares" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="NombreCompletoFamiliar">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:170px;" align="right" valign="middle">Nombre Completo Familiar&nbsp;</td>
                                            <td style="width:75px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto_FamiliarActualizar" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="NombreCompleto_FamiliarActualizar"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreCompleto_FamiliarActualizar") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="245px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="245px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="FechaSolicitud">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:100px;" align="right" valign="middle">Fecha Solicitud&nbsp;</td>
                                            <td style="width:20px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_FechaRegistroSolicitud" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="FechaRegistroSolicitud"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FechaRegistroSolicitud") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="120px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="NombreCompleto_FamiliarSolicitante" HeaderText="Nombre Completo Solicitante" >
                                    <HeaderStyle HorizontalAlign="Center" Width="225px" />
                                    <ItemStyle HorizontalAlign="left" Width="225px" CssClass="miGridviewBusqueda_Rows" />
                                </asp:BoundField>
                                
                                <asp:TemplateField HeaderText="Estado">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:70px;" align="right" valign="middle">Estado&nbsp;</td>
                                            <td style="width:20px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_EstadoSolicitud" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="EstadoSolicitud"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EstadoSolicitud") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="90px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="center" Width="90px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="CodigoSolicitud" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle">                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoSolicitud" runat="server" Text='<%# Bind("CodigoSolicitud") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>  
                            
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 628px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 200px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a página   </span>                                         
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                CssClass="letranormal" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;
                                            de
                                            <asp:Label ID="lblNumPaginas" runat="server" />                                         
                                        </td>                                        
                                        <td style="height: 20px; width: 228px;" align="center" valign="middle">                                           
                                            <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                                                CssClass="pagfirst" />
                                            <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                CommandArgument="Prev" CssClass="pagprev" />
                                            <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                CommandArgument="Next" CssClass="pagnext" />
                                            <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                                CssClass="paglast" />
                                        </td>                                        
                                        <td style="height: 20px; width: 200px;" align="right" valign="middle">
                                            <asp:Label ID="lblRegistrosActuales" runat="server" CssClass="miFooterMantLabelRight" />
                                        </td>                                        
                                    </tr>
                                </table>
                            </PagerTemplate>
                                                   
                        </asp:GridView>                                                
                    </div>
                    
                    <div class="miEspacio">
                    </div>
                    
                    <div id="GVLegenda">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
                            <tr>
                            
                                <td style="width: 5px; height: 26px;" align="center" valign="middle">
                                    </td>                              
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <img alt="Actualizar Registro" src="../App_Themes/Imagenes/opc_seleccionar.png"/></td>                                    
                                <td style="width: 110px; height: 26px;" align="left" valign="middle">
                                    <span>Seleccionar Registro</span></td>  
                                     
                                <td style="width: 10px; height: 26px;" align="center" valign="middle">
                                    <span></span></td>                                     
                                <td style="width: 695px; height: 26px;" align="center" valign="middle">
                                    </td>                                                                                            
                            </tr>                        
                        </table>
                    </div> 
                      
                </div>
            </ContentTemplate>
    </atk:TabPanel>      
    
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="false">        
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Inserción" />
            </HeaderTemplate>            
            <ContentTemplate> 

    <div id="miPaginaActualizacion_Verificar">    
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>      
        
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="width: 870px;" align="center" valign="top" colspan="2">
                                                    
            <table cellpadding="0" cellspacing="0" border="0" width="870px">   
            
            <tr>
                <td style="width: 100%;" align="left" colspan="4">
                    <div id="miCabeceraFicha">                    
                    
                        <table cellpadding="0" cellspacing="0" border="0" width="870px" style="margin: 0;">
                        
                            <tr>
                            
                                <td style="width: 755px;" align="left" valign="middle">

                        <fieldset style="width:755px; margin: 0;">
                            <legend style="width:550px">Datos del Solicitante</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="754px">
                           
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre Completo&nbsp;</span>
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="3">
                                    </td>
                                    <td style="width: 614px; height: 25px;" align="left" valign="middle">                                          
                                        <asp:Label ID="lblVerNombreSolicitante" runat="server" />
                                        <asp:HiddenField ID="hidenCodigoPerfil" runat="server" />
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Fecha&nbsp;</span> 
                                    </td>
                                     <td style="width: 614px; height: 25px;" align="left" valign="middle">                                     
                                        <asp:Label ID="lblVerFechaSolicitud" runat="server" />
                                    </td>                                   
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Estado&nbsp;</span>
                                    </td>
                                    <td style="width: 614px; height: 25px;" align="left" valign="middle">                                       
                                        <asp:Label ID="lblVerEstadoSolicitud" runat="server" />   
                                    </td>                                   
                                </tr> 
                                                                   
                            </table>
                        </fieldset>
                                
                                </td>
                                <td style="width: 115px;" align="center" valign="top" rowspan="2">   
                                    <br />                             
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin: 0;">
                                        <tr>
                                            <td style="width: 100%" align="center" valign="middle">
                                                 <asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" 
                                                    ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'" 
                                                    ToolTip="Grabar"
                                                    onclick="btnGrabar_click"/>    
                                            </td>
                                        </tr>
                                        <tr><td style="height:10px;"></td></tr>
                                        <tr>
                                            <td style="width: 100%;" align="center" valign="middle">
                                                <asp:ImageButton ID="btnFichaCancelar" runat="server" Width="84" Height="19"
                                                    ImageUrl="~/App_Themes/Imagenes/btnCancelarV2_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelarV2_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelarV2_1.png'" 
                                                    ToolTip="Cancelar"
                                                    onclick="btnFichaCancelar_Click" 
                                                    CausesValidation="false"/>          
                                            </td>
                                        </tr>
                                    </table>        
                                </td>
                            <tr>
                                <td style="width: 755px;" align="left" valign="middle">
                                <br />
                        <fieldset style="width:755px; margin: 0;">
                            <legend style="width:550px">Datos Personales</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="754px">
                            
                                <tr>
                                    <td style="width: 74px; height: 100px; background: #FFFFFF url(../App_Themes/Imagenes/img_bg.gif) no-repeat; background-position: center center;"
                                        align="center" valign="middle" rowspan="4">
                                        <asp:Image ID="imgFotoPaciente" runat="server" Width="54" Height="64" Style="border: #7f9db9 1px solid"
                                            ImageUrl="~/Fotos/noPhoto.gif" />
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="5">
                                    </td>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Apellido Paterno&nbsp;</span>
                                        <asp:HiddenField ID="hidenCodigoFamiliar" runat="server" />                                           
                                        <asp:HiddenField ID="hidenCodigoPersona" runat="server" />  
                                        <asp:HiddenField ID="hidenCodigoSolicitud" runat="server" />  
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="5">
                                    </td>
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                                          
                                        <asp:Label ID="lblVerApellidoPaterno" runat="server" />
                                    </td>
                                    
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="5">
                                    </td>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Tipo Documento&nbsp;</span> 
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="5">
                                    </td> 
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                                          
                                        <asp:Label ID="lblVerTipoDocumento" runat="server" /> 
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Apellido Materno&nbsp;</span> 
                                    </td>
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                                        
                                        <asp:Label ID="lblVerApellidoMaterno" runat="server" />
                                    </td>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Nro. Documento&nbsp;</span>
                                    </td>
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                                   
                                        <asp:Label ID="lblVerNumDocumento" runat="server" />    
                                    </td> 
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Nombre&nbsp;</span>
                                    </td>
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                                       
                                        <asp:Label ID="lblVerNombre" runat="server" />   
                                    </td>  
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Vive&nbsp;</span>
                                    </td>
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                                         
                                        <asp:Label ID="lblVerVive" runat="server" />   
                                    </td>                                   
                                </tr>                                
                               
                                <tr>                                   
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                       
                                        <span>Sexo&nbsp;</span>
                                    </td>
                                    <td style="width: 190px;" align="left" valign="middle">
                                        <asp:Label ID="lblVerSexo" runat="server" />  
                                    </td>     
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Fecha defunción&nbsp;</span>
                                    </td>
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                                        
                                        <asp:Label ID="lblVerFechaDefuncion" runat="server" />                                                           
                                    </td>                                
                                </tr>
                                
                                <tr>    
                                    <td style="width: 74px;"></td>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Estado Civil&nbsp;</span>
                                    </td>
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                  
                                        <asp:Label ID="lblVerEstadoCivil" runat="server" />    
                                    </td>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">  
                                        <asp:ImageButton ID="btnVerFamiliares" runat="server" Width="124" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnVerFamiliares_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnVerFamiliares_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnVerFamiliares_1.png'" 
                                            ToolTip="Ver Familiares"
                                            onclick="btnVerFamiliares_click"/>  
                                    </td>
                                    <td style="width: 190px; height: 25px;" align="left" valign="middle">                                                          
                                    </td>  
                                </tr>                                     
                            </table>
                        </fieldset>
                                
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                </td>   
            </tr>       
            
            <tr>
                <td style="width: 100%;" align="right" colspan="4">
                    <br />                    
                </td>
            </tr>  
                                                           
            <tr>                                                            
                <td style="width: 200px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                    Campos                                                                
                </td>
                <td style="width: 310px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                    Datos Actuales                                                                
                </td>
                <td style="width: 310px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                    Datos modificados por Familiar                                                                
                </td>
                <td style="width: 50px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                    <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true"/>                                                      
                </td>
            </tr> 
                                                          
            <tr>
                <td style="width: 870px; height: 25px" align="center" valign="top" colspan="4">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="center" valign="top" style="border: solid 1px #707070; width:870px">
                                   
                        <asp:GridView ID="GVActualizarFamiliar" runat="server"                 
                            Width="870px" 
                            CssClass="miGridviewBusquedaPersona" 
                            GridLines="none" 
                            ShowHeader="false"
                            ShowFooter="false"
                            AutoGenerateColumns="False" 
                            OnRowDataBound="GVActualizarFamiliar_RowDataBound">                            
                            <Columns>     
                                        
                                <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndiceCampo" runat="server" Text='<%# Bind("Indice")%>' />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>   
                                <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoOriginal" runat="server" Text='<%# Bind("CodigoCampoOriginal")%>' />
                                    </ItemTemplate>                                   
                                </asp:TemplateField> 
                                <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoActualizado" runat="server" Text='<%# Bind("CodigoCampoActualizar")%>' />
                                    </ItemTemplate>                                   
                                </asp:TemplateField> 
                                  
                                <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow" />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>  
                                   
                                <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="ValorOriginal" runat="server" Text='<%# Bind("ValorCampoOriginal")%>' CssClass="miPaddingRow" />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>   
                                  
                                <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="ValorActualizado" runat="server" Text='<%# Bind("ValorCampoActualizar")%>' CssClass="miPaddingRow" />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>  
                                
                                <asp:TemplateField ItemStyle-Width="50">   
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActualizar" runat="server" OnCheckedChanged="chkActualizar_CheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>               
                                </asp:TemplateField>    
                            </Columns>  
                                                                               
                        </asp:GridView>        
    
                            </td>
                        </tr>
                    </table>                                 
                </td>                                                        
            </tr>  
            
            <tr>                                                            
                <td style="width: 100%;" align="center" colspan="4">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaNacionalidad" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Nacionalidad                                                   
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVNacionalidad" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="miGridview_RowDataBound">                            
                                        <Columns>   
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoOriginal" runat="server" Text='<%# Bind("ArrCodigoOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoActualizado" runat="server" Text='<%# Bind("ArrCodigosActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescOriginal" runat="server" Text='<%# Bind("ArrDescOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescActualizado" runat="server" Text='<%# Bind("ArrDescActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorActualizado" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>      
                                            <asp:TemplateField ItemStyle-Width="50">                                                                      
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkActualizar" runat="server" />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>                           
                                        </Columns>                                                                                             
                                    </asp:GridView>        
                
                                        </td>
                                    </tr>
                                </table>                                 
                            </td>                                                        
                        </tr>                          
                    </table>                                           
                </td>
            </tr>
            
            <tr>                                                            
                <td style="width: 100%;" align="center" colspan="4">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaIdiomas" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Idiomas                                                                
                            </td>
                        </tr>  
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVIdiomas" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="miGridview_RowDataBound">                            
                                        <Columns>   
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoOriginal" runat="server" Text='<%# Bind("ArrCodigoOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoActualizado" runat="server" Text='<%# Bind("ArrCodigosActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescOriginal" runat="server" Text='<%# Bind("ArrDescOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescActualizado" runat="server" Text='<%# Bind("ArrDescActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorActualizado" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>      
                                            <asp:TemplateField ItemStyle-Width="50">                                                                      
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkActualizar" runat="server" />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>                           
                                        </Columns>                                                                                             
                                    </asp:GridView>        
                
                                        </td>
                                    </tr>
                                </table>                                 
                            </td>                                                        
                        </tr>   
                    </table>                                           
                </td>
            </tr>         
                    
            <tr>                                                            
                <td style="width: 100%;" align="center" colspan="4">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaProfesiones" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Profesiones                                                                
                            </td>
                        </tr>  
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVProfesiones" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="miGridview_RowDataBound">                            
                                        <Columns>   
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoOriginal" runat="server" Text='<%# Bind("ArrCodigoOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoActualizado" runat="server" Text='<%# Bind("ArrCodigosActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescOriginal" runat="server" Text='<%# Bind("ArrDescOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescActualizado" runat="server" Text='<%# Bind("ArrDescActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorActualizado" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>      
                                            <asp:TemplateField ItemStyle-Width="50">                                                                      
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkActualizar" runat="server" />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>                           
                                        </Columns>                                                                                             
                                    </asp:GridView>        
                
                                        </td>
                                    </tr>
                                </table>                                 
                            </td>                                                        
                        </tr> 
                    </table>                                           
                </td>
            </tr>  
                        
                        
                        

            <tr>                                                            
                <td style="width: 100%;" align="center" colspan="4">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaAutos" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Ficha Auto                                                                
                            </td>
                        </tr>  
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVAutos" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="miGridview_RowDataBound">                            
                                        <Columns>   
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoOriginal" runat="server" Text='<%# Bind("ArrCodigoOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoActualizado" runat="server" Text='<%# Bind("ArrCodigosActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescOriginal" runat="server" Text='<%# Bind("ArrDescOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescActualizado" runat="server" Text='<%# Bind("ArrDescActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorActualizado" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>      
                                            <asp:TemplateField ItemStyle-Width="50">                                                                      
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkActualizar" runat="server" />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>                           
                                        </Columns>                                                                                             
                                    </asp:GridView>        
                
                                        </td>
                                    </tr>
                                </table>                                 
                            </td>                                                        
                        </tr> 
                    </table>                                           
                </td>
            </tr>                          
                        
                        
                        
                                                          
            </table>  
                                                    
            </td>
        </tr> 
        </table>
        
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
            </ContentTemplate> 
        </atk:TabPanel>  
              
    </atk:TabContainer>
    
    </div>
    
    <div id="miListaFamiliares">
    <atk:ModalPopupExtender ID="pnModalListaFamiliares" runat="server"
        TargetControlID="VerListaFamiliares"
        PopupControlID="pnlListaFamiliares"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="true" 
        OkControlID="OKListaFamiliares" 
        CancelControlID="CancelListaFamiliares"
        Drag="true" 
        PopupDragHandleControlID="ListaFamiliaresHeader" />           

    <asp:panel id="pnlListaFamiliares" BackColor="White" BorderColor="Black" runat="server">  
        <table cellpadding="0" cellspacing="0" border="0" width="540px">    
        <tr>
            <td style="width: 510px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2">                
                <span id="FichasTemporalesHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Lista de Familiares</span>
            </td>
            <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                <asp:ImageButton ID="btnCerraListaFamiliares" runat="server" Width="16" Height="15"
                    ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                    onclick="btnCerraListaFamiliares_Click" ToolTip="Cerrar Panel"/>
            </td>
        </tr>
        <tr><td colspan="2"><br /></td></tr>
        <tr>
            <td colspan="2" align="center" valign="middle">        
<div style="border: solid 1px #a6a3a3; margin:auto; width:500px">
                    <asp:GridView ID="GVListaListaFamiliares" runat="server"
                            CssClass="miGridviewBusqueda"
                            Width="500"
                            GridLines="None" 
                            AutoGenerateColumns="False"
                            ShowHeader="true"
                            ShowFooter="false"
                            AllowPaging="false" 
                            AllowSorting="false"    
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GVListaListaFamiliares_RowDataBound">
                        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                           
                        <Columns>                          
                        <asp:BoundField DataField="CodigoPersona" HeaderText="CodigoPersona" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" />                                
                        <asp:BoundField DataField="CodigoFamiliar" HeaderText="CodigoFamiliar" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" />                                
                        <asp:BoundField DataField="CodigoFamilia" HeaderText="CodigoFamilia" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-CssClass="miHiddenStyle" />                               
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre del Familiar" ItemStyle-HorizontalAlign="left" ItemStyle-Width="300" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                     
                        <asp:BoundField DataField="NombreFamilia" HeaderText="Nombre de la Familia" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                     
                        </Columns>                         
                    </asp:GridView>   
</div>   
            </td>
        </tr> 
        <tr><td colspan="2"><br /></td></tr>              
    </table>     
    <div id="controlListaFamiliares" style="display:none">
            <input type="button" id="VerListaFamiliares" runat="server" />
            <input type="button" id="OKListaFamiliares" />
            <input type="button" id="CancelListaFamiliares" />
    </div>       
    </asp:panel>       
    </div>    
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
</div>          
    <script type="text/javascript">

    $(document).ready(function() {

        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);
            
    });

</script>
</asp:Content>

