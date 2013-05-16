<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ValidarDatosActualizadosFichaMedica.aspx.vb" 
Inherits="Modulo_Enfermeria_ValidarDatosActualizadosFichaMedica" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">

</script>
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

<div id="miPaginaActualizacionBusqueda">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>

     <div id="miPaginaActualizacion_Ficha">
     
     <atk:TabContainer ID="TabContainer1" runat="server" Width="890px" ActiveTabIndex="0"
        AutoPostBack="false" ScrollBars="None" >
                
    <atk:TabPanel ID="miTab1_1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1_1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 650px;">
                
                    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
                        <fieldset>
                            <legend>Criterios de Búsqueda del Alumno</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 1x red; width: 800px;">
                                  <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Fecha Solicitud</span>
                                    </td>
                                    <td style="min-width: 550px; height: 25px;" align="left" valign="bottom">
                                        <table cellpadding="0" cellspacing="0" border="0" width="320px" colspan="4">
                                            <tr>
                                                 <td valign="middle" align="left" style="width: 35px; height: 25px;">
                                                <span>Desde</span>
                                                </td> 
                                                <td valign="middle" align="left" style="width: 80px; height: 25px;">
                                                    <asp:TextBox ID="tbFechaSolicitudInicio" runat="server" CssClass="miTextBoxCalendar" />
                                                     <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                    TargetControlID="tbFechaSolicitudInicio"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                </atk:MaskedEditExtender> 
                                                </td>
                                                <td valign="middle" align="left" style="width: 25px; height: 25px;">
                                                    <asp:ImageButton runat="server" ID="imageBF" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                    <atk:CalendarExtender ID="calendario" runat="server" 
                                                        TargetControlID="tbFechaSolicitudInicio"
                                                        PopupButtonID="imageBF" 
                                                        Format="dd/MM/yyyy" 
                                                        CssClass="MyCalendar" Enabled="True" />
                                                </td>
                                                
                                                 <td valign="middle" align="center" style="width: 65px; height: 25px;">
                                                <span>Hasta</span>
                                                 </td> 
                                                 <td valign="middle" align="left" style="width: 80px; height: 25px;">
                                                    <asp:TextBox ID="tbFechaSolicitudFin" runat="server" CssClass="miTextBoxCalendar" />
                                                      <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                    TargetControlID="tbFechaSolicitudFin"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                         CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                         CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                         CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                </atk:MaskedEditExtender> 
                                                </td>
                                                <td valign="middle" align="left" style="width: 20px; height: 25px;">
                                                    <asp:ImageButton runat="server" ID="imageSolicitud" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                    <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                        TargetControlID="tbFechaSolicitudFin"
                                                        PopupButtonID="imageSolicitud" 
                                                        Format="dd/MM/yyyy" 
                                                        CssClass="MyCalendar" Enabled="True" />
                                                </td>
                                            </tr>
                                        </table>  
                                                                      
                                    </td>
                                     <td style="width: 100px;" align="right" valign="top">
                                                   <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/> 
                                    </td>
                                </tr>            
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Estado </span>
                                    </td>
                                    <td style="min-width: 360px; height: 25px;" align="left" valign="bottom">
                                        <asp:RadioButtonList ID="rbEstados" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="-1" Text="Todos" />                                            
                                            <asp:ListItem Value="0" Text="Eliminado" />  
                                            <asp:ListItem Value="1" Text="Pendiente" Selected="True" />
                                            <asp:ListItem Value="2" Text="Validado" />                                       
                                        </asp:RadioButtonList>                                        
                                    </td>
                                    <td style="width: 100px;" align="right" valign="top">
                                                   <asp:ImageButton ID="btnLimpiar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>     
                                    </td>
                                </tr>
                                 
                                 <tr>
                                                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                    <span>Apellido Paterno</span>
                                                     <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                                </td>
                                                <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                                    <asp:TextBox ID="tbBuscarFamiliarApellidoPaterno" runat="server" CssClass="miTextBox" Width="320px" MaxLength="100" Height="18px" />                                            
                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbBuscarFamiliarApellidoPaterno" 
                                                        ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>  
                                                </td>
                                                <%-- <td style="width: 200px;" align="right" valign="top">
                                                   <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/> 
                                    </td>--%>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                    <span>Apellido Materno</span>
                                                </td>
                                                <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                                    <asp:TextBox ID="tbBuscarFamiliarApellidoMaterno" runat="server" CssClass="miTextBox" Width="320px" MaxLength="100" Height="18px" /> 
                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbBuscarFamiliarApellidoMaterno" 
                                                        ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>                                           
                                                </td>
                                                <%--<td style="width: 200px;" align="right" valign="top">
                                                   <asp:ImageButton ID="btnLimpiar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>     
                                    </td>--%>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                    <span>Nombre</span>
                                                </td>
                                                <td style="width: 700px; height: 25px;" align="left" valign="middle" colspan ="2">
                                                    <asp:TextBox ID="tbBuscarFamiliarNombre" runat="server" CssClass="miTextBox" Width="320px" MaxLength="100" Height="18px" />                                            
                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbBuscarFamiliarNombre" 
                                                        ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                                    </atk:FilteredTextBoxExtender>  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                    <span>Nivel</span>
                                                </td>
                                                <td style="width: 700px; height: 25px;" align="left" valign="middle" colspan ="2" >
                                                    <asp:DropDownList ID="ddlBuscarFamiliarNivel" runat="server" Width="250px" 
                                                        AutoPostBack="True" 
                                                        OnSelectedIndexChanged="ddlBuscarFamiliarNivel_SelectedIndexChanged">
                                                    </asp:DropDownList>                                             
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                    <span>SubNivel</span>
                                                </td>
                                                <td style="width: 700px; height: 25px;" align="left" valign="middle" colspan ="2" >
                                                    <asp:DropDownList ID="ddlBuscarFamiliarSubNivel" runat="server" Width="250px" 
                                                        AutoPostBack="True" 
                                                        OnSelectedIndexChanged="ddlBuscarFamiliarSubNivel_SelectedIndexChanged">
                                                    </asp:DropDownList>                                             
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                    <span>Grado</span>
                                                </td>
                                                <td style="width: 700px; height: 25px;" align="left" valign="middle" colspan ="2">
                                                    <asp:DropDownList ID="ddlBuscarFamiliarGrado" runat="server" Width="250px" 
                                                        AutoPostBack="True" 
                                                        OnSelectedIndexChanged="ddlBuscarFamiliarGrado_SelectedIndexChanged">
                                                    </asp:DropDownList>                                             
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                                    <span>Aula</span>
                                                </td>
                                                <td style="width: 700px; height: 25px;" align="left" valign="middle" colspan ="2" >
                                                    <asp:DropDownList ID="ddlBuscarFamiliarAula" runat="server" Width="250px">
                                                    </asp:DropDownList>                                             
                                                </td>
                                            </tr>  
                            </table>
                        </fieldset>
                    </div>
                              
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miGridviewMantActualizacion_Ficha">                    
                        <asp:GridView ID="GVListaFamiliar" runat="server"                 
                            Width="840px" 
                            CssClass="miGridviewBusquedaPersona" 
                            GridLines="None" 
                            AutoGenerateColumns="False"  
                            AllowPaging="True" 
                            AllowSorting="True"                   
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GVListaFamiliar_RowDataBound"
                            OnRowCommand="GVListaFamiliar_RowCommand"
                            OnPageIndexChanging="GVListaFamiliar_PageIndexChanging"                             
                            OnSorting="GVListaFamiliar_Sorting" 
                            OnRowCreated="GVListaFamiliar_RowCreated">
                            <HeaderStyle CssClass="miGridviewBusquedaActualizacion_Ficha_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                           
                            <Columns>            
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                            CommandName="Seleccionar" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Seleccionar Alumno" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="NombreCompletoFamiliar">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:240px;" align="right" valign="middle">Nombre del Alumno&nbsp;</td>
                                            <td style="width:20px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="NombreCompleto"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreCompleto") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="260px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="260px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Estado Actual/Nivel/SubNivel/Grado/Aula">  
                                    <HeaderTemplate>
                                         <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:200px;" align="center" valign="middle">
                                               Estado/Nivel/SubNivel/Grado/Aula&nbsp;</td>
                                           
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblENSnGS" runat="server" Text='<%# Bind("ENSnGS") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="FechaSolicitud">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:110px;" align="right" valign="middle">Fecha Solicitud&nbsp;</td>
                                            <td style="width:20px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Fecha" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Fecha"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Fecha") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="130px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
                                        Width="130px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Solicitante" HeaderText="Nombre del Solicitante" >
                                    <HeaderStyle HorizontalAlign="Center" Width="215px" />
                                    <ItemStyle HorizontalAlign="Left" Width="215px" 
                                        CssClass="miGridviewBusqueda_Rows" />
                                </asp:BoundField>
                                
                                <asp:TemplateField HeaderText="Parentesco">  
                                    <HeaderTemplate>
                                         <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:60px;" align="center" valign="middle">
                                               Parentesco&nbsp;</td>
                                           
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("Parentesco") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
                                        Width="60px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Estado">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:45px;" align="right" valign="middle">Estado&nbsp;</td>
                                            <td style="width:30px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_Estado" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="Estado"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Estado") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="75px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
                                        Width="70px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="CodigoSolicitud">                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoSolicitud" runat="server" Text='<%# Bind("CodigoSolicitud") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="miHiddenStyle" />
                                    <ItemStyle CssClass="miHiddenStyle" />
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
                                    <span>&#124;</span></td>                                     
                                <td style="width: 695px; height: 26px;" align="center" valign="middle">
                                    </td>                                                                                            
                            </tr>                        
                        </table>
                    </div> 
                      
                </div>
            </ContentTemplate>
    </atk:TabPanel>
    
    <atk:TabPanel ID="miTab2_2" runat="server" HeaderText="Tab2_2" Enabled="false">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2_2" runat="server" Text="Actualización" />
            </HeaderTemplate>
            <ContentTemplate> 

              <div id="miPaginaActualizacion_Verificar">    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                <td style="width: 760px;" align="left" valign="middle">
                                
                                 <fieldset style="width:760px; margin: 0;" id="Fieldset1">
                                    <legend>Datos de la solicitud</legend>
                                    <table cellpadding="0" cellspacing="0" border="0" width="750px">
                                      <tr>
                                   
                                            <td style="width:70px; height: 25px; font-weight: bold;" align="center" valign="middle">
                                               <span>Fecha :&nbsp;</span>
                                            </td>
                                            
                                            <td style="width: 90px; height: 25px;" align="left" >
                                                <asp:Label ID="lblFechaSolicitud"  runat="server"></asp:Label>
                                            </td>
                                             
                                             <td style="width: 70px; height: 25px; font-weight: bold; font-family: Arial;" align="left" valign="middle"  >
                                                <span>Nombre :&nbsp;</span>
                                            </td>
                                                
                                            <td style="width: 250px; height: 25px; font-family: Arial;" align="left" valign="middle">
                                                <asp:Label  Text="" ID="lblNombreCompletoSolicitante" runat="server" ></asp:Label>
                                                <asp:HiddenField ID="hidenCodigoPerfil" runat="server" />
                                            </td>  
                                            <td style="width: 70px; height: 25px; font-weight: bold; font-family: Arial;" align="left" valign="middle"  >
                                                <span>Parentesco :&nbsp;</span>
                                            </td>
                                                
                                            <td style="width: 150px; height: 25px; font-family: Arial;" align="left" valign="middle">
                                                <asp:Label  Text="" ID="lblParentescoSolicitante" runat="server" ></asp:Label>
                                            </td>                      
                                        </tr>                              
                                    </table>
                                </fieldset>
                                <div class="miEspacio">
                                </div>
                                </td>
                                <td style="width: 100px;" align="left" valign="middle">
                                
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin: 0;">
                                        <tr>
                                            <td style="width: 100%" align="center" valign="middle">
                                                 <asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" 
                                                    ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'" 
                                                    ToolTip="Grabar"
                                                    onclick="btnGrabar_Click"/>    
                                            </td>
                                        </tr>
                                        <tr><td style="height:10px;"></td></tr>
                                        <tr>
                                            <td style="width: 100%;" align="center" valign="middle">
                                                <asp:ImageButton ID="btnCancelar" runat="server" Width="84" Height="19"
                                                    ImageUrl="~/App_Themes/Imagenes/btnCancelarV2_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelarV2_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelarV2_1.png'" 
                                                    ToolTip="Cancelar"
                                                    onclick="btnCancelarFicha_Click" 
                                                    CausesValidation="false"/>          
                                            </td>
                                        </tr>
                                    </table>    
                                                                  
                                </td>
                             </tr>
                             <tr>
                             
                             </tr>
                             
                        </table>
                        
                    </div>
                </td>   
            </tr>   
            <tr >
            
            <td style="width: 100%;" align="left" colspan="4">
                    <div id="miCabeceraFicha">                    
                    
                        <table cellpadding="0" cellspacing="0" border="0" width="870px" style="margin: 0;">
                            <tr>
                                <td style="width: 760px;" align="left" valign="middle">
                                
                              <fieldset style="width:760px; margin: 0;" id="FMA_DatosAlumno">
                                    <legend>Datos del Alumno</legend>
                                    <table  width="645px">
                                        <tr>
                                            <td style="width: 74px; height: 100px; background: #FFFFFF url(../App_Themes/Imagenes/img_bg.gif) no-repeat; background-position: center center;"
                                                align="center" valign="middle" rowspan="4">
                                                <asp:Image ID="img_FotoUsuario" runat="server" Width="54" Height="64" Style="border: #7f9db9 1px solid"
                                                    ImageUrl="~/Fotos/noPhoto.gif" />
                                            </td>
                                            <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="6">
                                            </td>
                                            <td style="width: 130px; height: 25px; font-weight: bold; font-family: Arial;" align="left" valign="middle"  >
                                                <span>Nombre Completo :&nbsp;</span>
                                            </td>
                                            <td style="width: 10px; height: 25px;" align="left">
                                            </td>    
                                            <td style="width: 421px; height: 25px; font-family: Arial;" align="left" valign="middle">
                                                <asp:Label  Text="" ID="lblNombreAlumno" runat="server" ></asp:Label>
                                                 <asp:HiddenField ID="hd_Codigo" runat="server" />
                                                 <asp:HiddenField ID="hidenCodigoPersona" runat="server" /> 
                                                 <asp:HiddenField ID="hidenCodigoSolicitud" runat="server" /> 
                                            </td>                                       
                                        </tr>
                                        <tr>
                                   
                                            <td style="width:130px; height: 25px; font-weight: bold;" align="left" valign="middle">
                                               <span>Sede:&nbsp;</span>
                                            </td>
                                            <td style="width: 10px; height: 25px;" align="left">
                                            </td> 
                                            <td style="width: 421px; height: 25px;" align="left" colspan ="3">
                                                <asp:Label ID="lblSede" Text="Miraflores"  runat="server"></asp:Label>
                                            </td>                      
                                        </tr>
                                        <tr>
                                        <td style="width: 130px; height: 25px; font-weight: bold; font-family: Arial;" align="left" valign="middle" >
                                               <span>Estado/Año Académico :&nbsp;</span>
                                            </td>
                                            <td style="width: 10px; height: 25px;" align="left">
                                            </td> 
                                            <td style="width: 421px; height: 25px; font-family: Arial;" align="left" colspan ="3">
                                                 <asp:Label ID="lblSituacionAnio" Text="Activo/2010"  runat="server"></asp:Label>
                                            </td>  
                                    
                                        </tr>                                        
                                        <tr>                                    
                                                                      
                                            <td style="width: 130px; height: 25px; font-weight: bold; font-family: Arial;" align="left" valign="middle">
                                                <span> Nivel/SubNivel/Grado/Aula :&nbsp;</span>
                                            </td>
                                            <td style="width: 10px; height: 25px;" align="left">
                                            </td> 
                                            <td style="width: 421px; height: 25px; font-family: Arial;" align="left" colspan ="3">
                                               <asp:Label ID="lblENSnGS"  Text=""  runat="server"></asp:Label>
                                            </td>
                                       </tr>
                                    </table>
                                </fieldset>
                               
                             </tr>
                             <tr>
                             
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
                <td style="width: 250px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                    Campos                                                                
                </td>
                <td style="width: 285px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                    Datos Actuales                                                                
                </td>
                <td style="width: 285px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                    Datos modificados por la Familia                                                                
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
                                   
                        <asp:GridView ID="GVActualizarFichaMedica" runat="server"                 
                            Width="870px" 
                            CssClass="miGridviewBusquedaPersona" 
                            GridLines="none" 
                            ShowFooter="false"
                            ShowHeader="false"
                            AutoGenerateColumns="False" 
                            OnRowDataBound="GVActualizarFichaMedica_RowDataBound">                            
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
                                 <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantRegistrosCampo" runat="server" Text='<%# Bind("CantRegistros")%>' />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>    
                                <asp:TemplateField ItemStyle-Width="250" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow" />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="285" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="ValorOriginal" runat="server" Text='<%# Bind("ValorCampoOriginal")%>' CssClass="miPaddingRow" />
                                    </ItemTemplate>                                   
                                </asp:TemplateField>     
                                <asp:TemplateField ItemStyle-Width="285" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="ValorActualizado" runat="server" Text='<%# Bind("ValorCampoActualizar")%>' CssClass="miPaddingRow" />
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
            
             <tr>                                                            
                <td style="width: 100%;" align="center" colspan="4">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaEnfermedad" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Enfermedades                                                   
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVEnfermedad" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="None" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="GVEnfermedad_RowDataBound">                            
                                        <Columns>   
                                         <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoRelacionEnf" runat="server" Text='<%# Bind("CodigoRelacion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
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
                                            
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaEdadOriginal" runat="server" Text='<%# Bind("ArrEdadOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaEdadActualizado" runat="server" Text='<%# Bind("ArrEdadActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaVacuna" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Vacunas                                                   
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVVacuna" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="GVVacuna_RowDataBound">                            
                                        <Columns>  
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoRelacionVac" runat="server" Text='<%# Bind("CodigoRelacion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>  
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoVacOriginal" runat="server" Text='<%# Bind("ArrCodigoVacOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoVacActualizado" runat="server" Text='<%# Bind("ArrCodigoVacActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescVacOriginal" runat="server" Text='<%# Bind("ArrDescVacOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDescVacActualizado" runat="server" Text='<%# Bind("ArrDescVacActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoDosisOriginal" runat="server" Text='<%# Bind("ArrCodigoDosisOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoDosisActualizado" runat="server" Text='<%# Bind("ArrCodigoDosisActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDosisOriginal" runat="server" Text='<%# Bind("ArrDosisOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaDosisActualizado" runat="server" Text='<%# Bind("ArrDosisActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaVacOriginal" runat="server" Text='<%# Bind("ArrFechaVacOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaVacActualizado" runat="server" Text='<%# Bind("ArrFechaVacActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                            
                                            <asp:TemplateField ItemStyle-Width="220" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="300" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="300" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaAlergia" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Alergia                                                   
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVAlergia" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="GVAlergia_RowDataBound">                            
                                        <Columns>   
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoRelacionAlerg" runat="server" Text='<%# Bind("CodigoRelacion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoAlergOriginal" runat="server" Text='<%# Bind("ArrCodigoAlergOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoAlergActualizado" runat="server" Text='<%# Bind("ArrCodigoAlergActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaAlergOriginal" runat="server" Text='<%# Bind("ArrAlergOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaAlergActualizado" runat="server" Text='<%# Bind("ArrAlergActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            
                                                 <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaAlergOriginal" runat="server" Text='<%# Bind("ArrFechaAlergOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaAlergActualizado" runat="server" Text='<%# Bind("ArrFechaAlergActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                                                                 
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaCaractPiel" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Caracteristicas de la Piel                                                   
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVCaracteristicaPiel" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="GVCaracteristicaPiel_RowDataBound">                            
                                        <Columns>   
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoRelacionCaractPiel" runat="server" Text='<%# Bind("CodigoRelacion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoCaractPielOriginal" runat="server" Text='<%# Bind("ArrCodigoCaractPielOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoCaractPielActualizado" runat="server" Text='<%# Bind("ArrCodigoCaractPielActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCaractPielOriginal" runat="server" Text='<%# Bind("ArrCaractPielOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCaractPielActualizado" runat="server" Text='<%# Bind("ArrCaractPielActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaCaractPielOriginal" runat="server" Text='<%# Bind("ArrFechaCaractPielOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaCaractPielActualizado" runat="server" Text='<%# Bind("ArrFechaCaractPielActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            
                                                                                        
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaMedic" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Medicamento que usa actualmente                                                
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVMedicamento" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="GVMedicamento_RowDataBound">                            
                                        <Columns>   
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoRelacionMed" runat="server" Text='<%# Bind("CodigoRelacion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoMedicOriginal" runat="server" Text='<%# Bind("ArrCodigoMedicOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoMedicActualizado" runat="server" Text='<%# Bind("ArrCodigoMedicActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaMedicOriginal" runat="server" Text='<%# Bind("ArrMedicOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaMedicActualizado" runat="server" Text='<%# Bind("ArrMedicActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            
                                                <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoFrecOriginal" runat="server" Text='<%# Bind("ArrCodigoFrecOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoFrecActualizado" runat="server" Text='<%# Bind("ArrCodigoFrecActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            
                                             <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFrecOriginal" runat="server" Text='<%# Bind("ArrFrecOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFrecActualizado" runat="server" Text='<%# Bind("ArrFrecActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>  
                                            
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaMedicOriginal" runat="server" Text='<%# Bind("ArrFechaMedicOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaMedicActualizado" runat="server" Text='<%# Bind("ArrFechaMedicActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                                                                        
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaHosp" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Motivo Hospitalización                                                   
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVMotivoHospitalizacion" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="GVMotivoHospitalizacion_RowDataBound">                            
                                        <Columns>   
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoRelacionHosp" runat="server" Text='<%# Bind("CodigoRelacion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoHospOriginal" runat="server" Text='<%# Bind("ArrCodigoHospOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoHospActualizado" runat="server" Text='<%# Bind("ArrCodigoHospActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaHospOriginal" runat="server" Text='<%# Bind("ArrHospOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaHospActualizado" runat="server" Text='<%# Bind("ArrHospActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            
                                           <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaHospOriginal" runat="server" Text='<%# Bind("ArrFechaHospOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaHospActualizado" runat="server" Text='<%# Bind("ArrFechaHospActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>
                                                                                        
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaOperac" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Datos Tipo Operación                                                  
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVTipoOperacion" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="GVTipoOperacion_RowDataBound">                            
                                        <Columns>   
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoRelacionOper" runat="server" Text='<%# Bind("CodigoRelacion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoOperacOriginal" runat="server" Text='<%# Bind("ArrCodigoOperacOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoOperacActualizado" runat="server" Text='<%# Bind("ArrCodigoOperacActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaOperacOriginal" runat="server" Text='<%# Bind("ArrOperacOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaOperacActualizado" runat="server" Text='<%# Bind("ArrOperacActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaOperacOriginal" runat="server" Text='<%# Bind("ArrFechaOperacOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaOperacActualizado" runat="server" Text='<%# Bind("ArrFechaOperacActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>
                                                                                        
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="miTablaTiposControles" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Tipos de Control                                                 
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                               
                                    <asp:GridView ID="GVTipoTiposControles" runat="server"                 
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="None" 
                                        ShowFooter="false"
                                        ShowHeader="false"
                                        AutoGenerateColumns="False" 
                                        OnRowDataBound="GVTipoTiposControles_RowDataBound">                            
                                        <Columns>   
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoRelacionTipCont" runat="server" Text='<%# Bind("CodigoRelacion")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoTipControlOriginal" runat="server" Text='<%# Bind("ArrCodigoTipControlOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaCodigoTipControlActualizado" runat="server" Text='<%# Bind("ArrCodigoTipControlActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>    
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaTipControlOriginal" runat="server" Text='<%# Bind("ArrTipControlOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaTipControlActualizado" runat="server" Text='<%# Bind("ArrTipControlActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            
                                              <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaResultadoOriginal" runat="server" Text='<%# Bind("ArrResultadoOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaResultadoActualizado" runat="server" Text='<%# Bind("ArrResultadoActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaTipControlOriginal" runat="server" Text='<%# Bind("ArrFechaTipControlOriginal")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>   
                                             
                                            <asp:TemplateField ItemStyle-CssClass="miHiddenStyle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblListaFechaTipControlActualizado" runat="server" Text='<%# Bind("ArrFechaTipControlActualizar")%>' />
                                                </ItemTemplate>                                   
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreCampo" runat="server" Text='<%# Bind("NombreCampo")%>' CssClass="miPaddingRow"/>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
                                                <ItemTemplate>
                                                    <asp:BulletedList ID="listValorOriginal" runat="server" CssClass="noList">
                                                    </asp:BulletedList>
                                                </ItemTemplate>                                   
                                            </asp:TemplateField>     
                                            <asp:TemplateField ItemStyle-Width="310" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">                                                                      
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

