<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ValidarFichaAlumnos.aspx.vb" Inherits="Modulo_Matricula_ValidarFichaAlumnos" title="Página sin título" %>
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
                                                <asp:TextBox ID="tbFechaSolicitudInicial" runat="server" CssClass="miTextBoxCalendar" Height="18px" 
                                                    style="font-size: 8pt; font-family: Arial;" />    
                                                <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                    TargetControlID="tbFechaSolicitudInicial"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                </atk:MaskedEditExtender>                                        
                                            </td>
                                            <td align="left" valign="middle" style="width: 30px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="image1" 
                                                    ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de solicitud inicial." />
                                                <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                    TargetControlID="tbFechaSolicitudInicial"
                                                    PopupButtonID="image1" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" Enabled="True" />
                                            </td>
                                            <td align="left" valign="middle" style="width: 40px; height: 25px;">
                                                <span>hasta</span>
                                            </td>
                                            <td align="left" valign="middle" style="width: 110px; height: 25px;">
                                                <asp:TextBox ID="tbFechaSolicitudFinal" runat="server" CssClass="miTextBoxCalendar" Height="18px"  
                                                    style="font-size: 8pt; font-family: Arial;" />    
                                                <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                    TargetControlID="tbFechaSolicitudFinal"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-" CultureAMPMPlaceholder="" 
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                                </atk:MaskedEditExtender>                                        
                                            </td>
                                            <td align="left" valign="middle" style="width: 70px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="image2" 
                                                    ImageUrl="~/App_Themes/Imagenes/calendar_icon.png" ToolTip="Fecha de solicitud final." />
                                               
                                                <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                    TargetControlID="tbFechaSolicitudFinal"
                                                    PopupButtonID="image2" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" Enabled="True" />
                                                    
                                            </td>
                                        </tr>
                                    </table> 
                                    </td>  
                                    <td style="width: 100px;" align="right" valign="top">
                                    </td>
                                </tr>
                                                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Estado Solicitud</span>
                                    </td>
                                    <td style="min-width: 360px; height: 25px;" align="left" valign="bottom">
                                        <asp:RadioButtonList ID="rbEstados" runat="server" RepeatDirection="Horizontal"  
                                            style="font-size: 8pt; font-family: Arial;" >
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
                                            Width="320px" MaxLength="100" Height="18px" 
                                            style="font-size: 8pt; font-family: Arial;" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarApellidoPaterno" 
                                            ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>                                     
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                         <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
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
                                            style="font-size: 8pt; font-family: Arial;"  
                                            Width="320px" MaxLength="100" Height="18px" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" 
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarApellidoMaterno" 
                                            ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>                                     
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                         <asp:ImageButton ID="btnLimpiar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
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
                                            style="font-size: 8pt; font-family: Arial;"  
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
                                                                                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Nivel</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarNivel" runat="server" Width="250px" 
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBuscarNivel_SelectedIndexChanged"
                                            style="font-size: 8pt; font-family: Arial;"  >
                                        </asp:DropDownList>                                             
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>SubNivel</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarSubNivel" runat="server" Width="250px" 
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBuscarSubNivel_SelectedIndexChanged"
                                            style="font-size: 8pt; font-family: Arial;"  >
                                        </asp:DropDownList>                                             
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Grado</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarGrado" runat="server" Width="250px" 
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBuscarGrado_SelectedIndexChanged"
                                            style="font-size: 8pt; font-family: Arial;"  >
                                        </asp:DropDownList>                                             
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Aula</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarAula" runat="server" Width="250px"
                                            style="font-size: 8pt; font-family: Arial;"  >
                                        </asp:DropDownList>                                             
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
                        
                        <asp:GridView ID="GVListaAlumno" runat="server"                 
                            Width="840px" 
                            CssClass="miGridviewBusquedaPersona" 
                            GridLines="None" 
                            AutoGenerateColumns="False"  
                            AllowPaging="True" 
                            AllowSorting="True"                   
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GVListaAlumno_RowDataBound"
                            OnRowCommand="GVListaAlumno_RowCommand"
                            OnPageIndexChanging="GVListaAlumno_PageIndexChanging"                             
                            OnSorting="GVListaAlumno_Sorting" 
                            OnRowCreated="GVListaAlumno_RowCreated">
                            <HeaderStyle CssClass="miGridviewBusquedaActualizacion_Ficha_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                           
                            <Columns>            
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                            CommandName="Seleccionar" CommandArgument='<%# Bind("CodigoAlumno_AlumnoActualizar") %>' ToolTip="Seleccionar Alumno" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="NombreCompletoAlumno">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:150px;" align="right" valign="middle">Nombre del Alumno&nbsp;</td>
                                            <td style="width:50px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto_AlumnoActualizar" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="NombreCompleto_AlumnoActualizar"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreCompleto_AlumnoActualizar") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="200px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="200px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="EstadoAlumno_AlumnoActualizar" HeaderText="Estado Alumno" >
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" Width="200px" 
                                        CssClass="miGridviewBusqueda_Rows" />
                                </asp:BoundField>
                                
                                <asp:TemplateField HeaderText="FechaSolicitud">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:60px;" align="center" valign="middle">Fecha Solicitud&nbsp;</td>
                                            <td style="width:20px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_FechaRegistroSolicitud" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="FechaRegistroSolicitud"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FechaRegistroSolicitud") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="80px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="NombreCompleto_FamiliarSolicitante" HeaderText="Nombre del Solicitante" >
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" Width="200px" 
                                        CssClass="miGridviewBusqueda_Rows" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="DescParentesco_FamiliarSolicitante" HeaderText="Parentesco" >
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" CssClass="miGridviewBusqueda_Rows" />
                                </asp:BoundField>
                                
                                <asp:TemplateField HeaderText="Estado">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:60px;" align="center" valign="middle">Estado Solicitud</td>
                                            <td style="width:20px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_EstadoSolicitud" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="EstadoSolicitud"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("EstadoSolicitud") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" 
                                        Width="80px" />
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
                                
                            </tr>
                            
                            <tr>
                                <td style="width: 755px;" align="left" valign="middle">
                                 <br />     
                        <fieldset style="width:755px; margin: 0;">
                            <legend style="width:550px">Situación Actual del Alumno</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="754px">
                           
                                <tr>
                                    <td style="width: 74px; height: 100px; background: #FFFFFF url(../App_Themes/Imagenes/img_bg.gif) no-repeat; background-position: center center;"
                                        align="center" valign="middle" rowspan="4">
                                        <asp:Image ID="imgFotoPaciente" runat="server" Width="54" Height="64" Style="border: #7f9db9 1px solid"
                                            ImageUrl="~/Fotos/noPhoto.gif" />
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="4">
                                    </td>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre Completo&nbsp;</span>
                                        <asp:HiddenField ID="hidenCodigoAlumno" runat="server" />                                           
                                        <asp:HiddenField ID="hidenCodigoPersona" runat="server" />  
                                        <asp:HiddenField ID="hidenCodigoSolicitud" runat="server" />  
                                        <asp:HiddenField ID="hidenCodigoFichaSeguro" runat="server" />  
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="4">
                                    </td>
                                    <td style="width: 530px; height: 25px;" align="left" valign="middle">                                          
                                        <asp:Label ID="lblVerNombreCompleto" runat="server" />
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                                        <span>Estado/Año Académico&nbsp;</span> 
                                    </td>
                                     <td style="width: 530px; height: 25px;" align="left" valign="middle">                                     
                                        <asp:Label ID="lblVerEstadoAnioAcademico" runat="server" />
                                    </td>                                   
                                </tr>
                                
                                <tr>
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                        
                                        <span>Nivel/SubNivel/Grado/Aula&nbsp;</span>
                                    </td>
                                    <td style="width: 530px; height: 25px;" align="left" valign="middle">                                       
                                        <asp:Label ID="lblVerNSnGS" runat="server" />   
                                    </td>                                   
                                </tr>                                
                               
                                <tr>                                   
                                    <td style="width: 130px; height: 25px;" align="left" valign="middle">                                       
                                        <span>House &nbsp;</span>
                                    </td>
                                    <td style="width: 530px;" align="left" valign="middle">
                                        <asp:Label ID="lblVerHouse" runat="server" />  
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
                                   
                        <asp:GridView ID="GVActualizarAlumno" runat="server"                 
                            Width="870px" 
                            CssClass="miGridviewBusquedaPersona" 
                            GridLines="none" 
                            ShowHeader="false"
                            ShowFooter="false"
                            AutoGenerateColumns="False" 
                            OnRowDataBound="GVActualizarAlumno_RowDataBound">                            
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
                                       <%-- hh--%>
                                        
                                        
                                 <%--       
                        nombre	
                        parienteTipoDescripcion
                        responsablePago	
                        viveConAlumno	
                        codintegranteFamilia	

                        nombreDer	
                        parienteTipoDescripcionDer	
                        responsablePagoDer	
                        viveConAlumnoDer	
                        codintegranteFamiliaDer
	--%>
                                        
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
                                            
    
                                        
                                        <%-- hh--%>
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="Table1" runat="server">
                        <tr>                                                            
                            <td style="width: 870px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                Información otros                                                        
                            </td>
                        </tr> 
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center" valign="top" style="border: solid 1px #707070; width:100%">
                                              
                        <asp:GridView ID="GridView1" runat="server"    
                                        Width="870px" 
                                        CssClass="miGridviewBusquedaPersona" 
                                        GridLines="Both" 
                                        ShowFooter="false"
                                        ShowHeader="true"
                                        AutoGenerateColumns="False" >                            
                          
                          <HeaderStyle BackColor="#bfe4f5" HorizontalAlign="Center" VerticalAlign="Middle" />
                          <RowStyle Height="26px" />
                            <Columns>   
                                <asp:TemplateField ItemStyle-Width="340" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"  HeaderText="Nombre Completo del Familiar">                                                                      
                                    <ItemTemplate>
                                    <asp:Label ID="lblIndiceCampo1" runat="server" Text='<%# Bind("nombre")%>' />
                                    &nbsp;(
                                    <asp:Label ID="lblIndiceCampo2" runat="server" Text='<%# Bind("parienteTipoDescripcion")%>' />
                                    &nbsp;)
                                    </ItemTemplate>                                   
                                </asp:TemplateField> 
                                
                                <asp:TemplateField ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  HeaderText="Responsable Pago/Actual">                                                                      
                                    <ItemTemplate><asp:Label ID="lblIndiceCampo" runat="server" Text='<%# Bind("responsablePago")%>' /></ItemTemplate>                                   
                                </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  HeaderText="Vive con alumno/Actual ">                                                                      
                                    <ItemTemplate><asp:Label ID="lblIndiceCampo3" runat="server" Text='<%# Bind("viveConAlumno")%>' /></ItemTemplate>                                   
                                </asp:TemplateField>
                                 
                                <asp:TemplateField ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  HeaderText="Responsable Pago/Modificado">                                                                      
                                    <ItemTemplate><asp:Label ID="lblIndiceCampo6" runat="server" Text='<%# Bind("responsablePagoDer")%>' /></ItemTemplate>                                   
                                </asp:TemplateField> 
                                <asp:TemplateField ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  HeaderText="Vive con alumno/Modificado">                                                                      
                                    <ItemTemplate><asp:Label ID="lblIndiceCampo5" runat="server" Text='<%# Bind("viveConAlumnoDer")%>' /></ItemTemplate>                                   
                                </asp:TemplateField> 
                              
                               <asp:TemplateField ItemStyle-Width="50">   
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActualizar" runat="server" AutoPostBack="true" />
                                    </ItemTemplate>               
                                </asp:TemplateField>  
                                 <asp:TemplateField ItemStyle-Width="0" Visible="false">   
                                    <ItemTemplate>
                                       <asp:Label ID="lblIndiceCampo7" runat="server" Text='<%# Bind("codintegranteFamilia")%>' />
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

