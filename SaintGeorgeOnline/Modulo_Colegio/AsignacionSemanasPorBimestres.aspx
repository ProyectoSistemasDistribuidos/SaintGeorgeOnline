﻿<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="AsignacionSemanasPorBimestres.aspx.vb" Inherits="Modulo_Colegio_AsignacionSemanasPorBimestres" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
    .style1
    {
        width: 150px;
        height: 25px;
    }
    .style2
    {
        height: 25px;
    }
    
</style>

<script type="text/javascript" >

    function abrirPopupParams(url, tipo, tbPadre) {

        var urlaux = url + '?tipo=' + tipo + '&Padre=' + tbPadre;
        window.showModalDialog(urlaux, "#1", "dialogHeight: 485px ; dialogWidth: 759px; center: Yes; help: No; resizable: No; status: No; scroll: No");

    }

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
      
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional" >
    
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
                <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 860px;">
                
                    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 820px;">
                               
                                <tr>
                                    <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                        <span>Periodo Académico</span>
                                    </td>
                                    <td style="width: 300px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarAnioAcademico" runat="server" Width="200px">
                                        </asp:DropDownList>  
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />                               
                                    </td>
                                     <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                      
                                    </td>
                                    <td style="width: 260px; height: 25px;" align="left" valign="middle">
                                                                                                                            
                                    </td>
                                
                                    <td style="width: 100px; height: 25px;" align="right" valign="middle">
                                        <asp:ImageButton ID="btnNuevo" runat="server" Width="74px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
                                            onclick="btnNuevo_Click" 
                                            ToolTip="Agregar Registro"/> 
                                    </td>
                                </tr>  
                                
                                <tr>
                                   
                                    <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                         <span>Bimestre</span>
                                    </td>                                    
                                    <td style="width: 300px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarBimestre" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    
                                      <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                        <span></span>
                                    </td>
                                     <td style="width: 260px; height: 25px;" align="left" valign="middle">
                                       
                                     </td>
                                    <td style="width: 100px; height: 25px;" align="right" valign="middle">                                    
                                        <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                            onclick="btnBuscar_Click" ToolTip="Buscar Registros"/>    
                                    </td>
                                </tr>
                                                                
                            </table>
                        </fieldset>
                    </div>
                         
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miBusquedaActualizacion_Ficha">
                        <fieldset>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 800px;">
                                <tr>
                                    <td style="width: 100px; height: 21px;" align="left" valign="middle">
                                        <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                            ToolTip="Exportar"
                                            OnClick="btnExportar_Click" />
                                    </td>
                                    <td style="width: 600px; height: 21px;" align="left" valign="bottom">                                  
                                    <asp:RadioButtonList ID="rbExportar" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Text="Word"/>
                                        <asp:ListItem Value="1" Text="Excel" Selected="True"/>
                                        <asp:ListItem Value="2" Text="Pdf"/>
                                        <asp:ListItem Value="3" Text="Html"/>
                                    </asp:RadioButtonList>                                    
                                </td>                                
                                <td style="width: 100px; height: 21px;" align="right" valign="middle">
                                    
                                 </td>                                                                     
                                </tr>
                            </table>
                           
                        </fieldset>                    
                    </div>    
                                    
                    <div class="miEspacio">
                    </div>    
                                    
                    <div id="miGridviewMantActualizacion_Ficha">
                        <asp:GridView ID="GV_AsignacionSemanaPorBimestre" runat="server" 
                            Width="840px" 
                            CssClass="miGridviewBusqueda" 
                            GridLines="None" 
                            AutoGenerateColumns="False"
                            AllowSorting="True"
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GV_AsignacionSemanaPorBimestre_RowDataBound"
                            OnRowCommand="GV_AsignacionSemanaPorBimestre_RowCommand" 
                            OnRowCreated="GV_AsignacionSemanaPorBimestre_RowCreated"
                            OnSorting="GV_AsignacionSemanaPorBimestre_Sorting" >
                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />  
                            <Columns>           
                                                                                            
                                <asp:TemplateField HeaderText="Año">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:50px;" align="right" valign="middle">Año&nbsp;</td>
                                            <td style="width:40px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_DescAnioAcademico" runat="server" 
                                                ToolTip="Descendente" ImageUrl="~/App_Themes/Imagenes/DOWN.png" CommandName="Sort" 
                                                CommandArgument="DescAnioAcademico"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescAnioAcademico" runat="server" Text='<%# Bind("DescAnioAcademico") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="90px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>     
                                
                                <asp:TemplateField HeaderText="Bimestre">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:70px;" align="right" valign="middle">Bimestre &nbsp;</td>
                                            <td style="width:30px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_DescBimestre" runat="server" 
                                                ToolTip="Descendente" ImageUrl="~/App_Themes/Imagenes/DOWN.png" CommandName="Sort" 
                                                CommandArgument="DescBimestre"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescBimestre" runat="server" Text='<%# Bind("DescBimestre") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>     
                                
                                <asp:TemplateField HeaderText="Semana Académica">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:120px;" align="right" valign="middle">Semana Académica&nbsp;</td>
                                            <td style="width:30px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_CodigoSemanaAcademica" runat="server" 
                                                ToolTip="Descendente" ImageUrl="~/App_Themes/Imagenes/DOWN.png" CommandName="Sort" 
                                                CommandArgument="CodigoSemanaAcademica"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescSemanaAcademica" runat="server" Text='<%# Bind("DescSemanaAcademica") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>    
                                
                                <asp:TemplateField HeaderText="Fecha de Inicio">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:90px;" align="right" valign="middle">Fecha de Inicio&nbsp;</td>
                                            <td style="width:30px;" align="left" valign="middle"></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaInicio" runat="server" Text='<%# Bind("FechaInicio") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="90px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>  
                                
                                <asp:TemplateField HeaderText="Fecha de Fin">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:60px;" align="right" valign="middle">Fecha de Fin&nbsp;</td>
                                            <td style="width:30px;" align="left" valign="middle"></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaFin" runat="server" Text='<%# Bind("FechaFin") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="90px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>                                  
                               </Columns>
                            
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 300px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a página   </span>                                         
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                CssClass="letranormal" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;
                                            de
                                            <asp:Label ID="lblNumPaginas" runat="server" />                                         
                                        </td>                                        
                                        <td style="height: 20px; width: 240px;" align="center" valign="middle">                                           
                                            <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                                                CssClass="pagfirst" />
                                            <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                                CommandArgument="Prev" CssClass="pagprev" />
                                            <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Sig. página"
                                                CommandArgument="Next" CssClass="pagnext" />
                                            <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                                                CssClass="paglast" />
                                        </td>                                        
                                        <td style="height: 20px; width: 300px;" align="right" valign="middle">
                                            <asp:Label ID="lblRegistrosActuales" runat="server" CssClass="miFooterMantLabelRight" />
                                        </td>                                        
                                    </tr>
                                </table>
                            </PagerTemplate>
                                
                        </asp:GridView>
                    </div>
                    
                    <div class="miEspacio">
                    </div>
                    
                      <atk:ModalPopupExtender ID="pnModalAgregarRegistro" runat="server"
                        TargetControlID="VerAgregarRegistro"
                        PopupControlID="pnlAgregarRegistro"
                        BackgroundCssClass="MiModalBackground" 
                        DropShadow="true" 
                        OkControlID="OKAgregarRegistro" 
                        CancelControlID="CancelAgregarRegistro"
                        Drag="true" 
                        PopupDragHandleControlID="AgregarRegistroHeader" />           

                        <asp:panel id="pnlAgregarRegistro" BackColor="White" BorderColor="Black" runat="server">
                          <table cellpadding="0" cellspacing="0" border="0" width="430px" id="panelRegistro">    
                            <tr  id="AgregarRegistroHeader" style ="cursor :pointer; ">
                                <td style="width: 410px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="4">                
                                    <asp:Label ID="lblTitulo" runat ="server"> <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;"></span></asp:Label>

                                </td>
                                <td style="width: 20px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                                    <asp:ImageButton ID="btnCerraAgregarRegistro" runat="server" Width="16" Height="15"
                                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                        onclick="btnCerrarModal_Click" ToolTip="Cerrar Panel"/>
                                </td>
                            </tr>
                            <tr>
                               <td colspan="5" style="height: 15px;" align="right">
                                        <%--<em style ="font-size:small;">Campos Obligatorios (*)</em>--%>
                               </td>
                            </tr>   
                                     
                            <tr>
                                <td style="width: 20px; " rowspan="4"></td>
                                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                    <span>Periodo :&nbsp;</span><span class="camposObligatorios">(*)</span>        
                                </td>
                                <td style="width: 190px; height: 25px;" align="left" valign="bottom">
                                     <asp:DropDownList ID="ddlAnioAcademico" runat="server" Width="140px">
                                    </asp:DropDownList>   
                                    <asp:HiddenField ID="hd_Codigo" runat="server" />    
                                    <asp:HiddenField ID="hiddenCodigoAnioAcademico" runat="server" />  
                                </td>
                               <td style ="width: 100px; height: 25px; " align="right" valign="bottom"> 
                                   <asp:ImageButton ID="btnGrabar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                            onclick="btnGrabar_Click" />
                                </td>
                                <td style="width: 20px; " rowspan="4"></td>
                            </tr>           
                            <tr>
                                <td  style="width: 100px; height: 25px; " align="left" valign="middle">
                                    <span>Bimestre :&nbsp;</span><span class="camposObligatorios">(*)</span> 
                                </td>
                                <td style="width: 190px; height: 25px;" align="left" valign="bottom">
                                    <asp:DropDownList ID="ddlBimestre" runat="server" Width="140px" OnSelectedIndexChanged="ddlBimestre_SelectedIndexChanged" AutoPostBack ="true">
                                    </asp:DropDownList>           
                                </td>
                                <td style ="width: 100px; height: 25px; " align="right" valign="bottom"> 
                                  <asp:ImageButton ID="btnCancelar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                                            onclick="btnCancelar_Click" CausesValidation="false"/>
                                </td>
                            </tr>   
                            <tr><td colspan="3"><br /></td></tr>    
                            <tr>
                               <td colspan="3" align="center" valign="top" style="width:390px">
                                 <div style="border: solid 1px #a6a3a3; width:390px">                    
                                        <asp:GridView ID="GVListaSemanas" runat="server"
                                            CssClass="miGridviewBusqueda"
                                            Width="390"
                                            GridLines="None" 
                                            AutoGenerateColumns="False"
                                            ShowHeader="true"
                                            ShowFooter="false"                                            
                                            AllowSorting="false" 
                                               
                                            EmptyDataText=" - No se encontraron resultados - "
                                           >
                                        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                        
                                        <Columns>                                          
                                      
                                         <asp:TemplateField ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center">    
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server"  />                                    
                                        </HeaderTemplate>                                      
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                        </ItemTemplate>                                   
                                        </asp:TemplateField>  
                                        
                                        <asp:BoundField DataField="IdFila" HeaderText="IdFila" ItemStyle-Width="0" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" />                                
                                        <asp:BoundField DataField="CodigoSemanaAcademica" HeaderText="CodigoSemanaAcademica" ItemStyle-Width="0" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" />                                
                                        <asp:BoundField DataField="CodigoAsignacionSemana" HeaderText="CodigoAsignacionSemana" ItemStyle-Width="0" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" />                                
                                        <asp:BoundField DataField="Semana" HeaderText="Semanas" ItemStyle-Width="90" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" />                                
                                        
                                        <asp:TemplateField ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" 
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>               
                                            <asp:Label ID="lblOrden" runat ="server" Text="Orden"></asp:Label>
                                            </HeaderTemplate>  
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbOrden" runat="server" Text='<%# Bind("Orden") %>' Width="20" />
                                           </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField ItemStyle-Width="120" ItemStyle-CssClass="miGridviewBusqueda_Rows" 
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>               
                                            <asp:Label ID="lblFechaInicio" runat ="server" Text="Fecha Inicio"></asp:Label>
                                            </HeaderTemplate>  
                                        <ItemTemplate>

                                        <table cellpadding="0" cellspacing="0" border="0" width="120px">
                                          <tr>
                                                                              
                                        <td valign="middle" align="right" style="width: 90px; height: 25px;">
                                            <asp:TextBox ID="tbFechaInicio" runat="server" Text='<%# Bind("FechaInicio") %>' CssClass="miTextBoxCalendar" Width="80" />
                                            <asp:HiddenField  ID="hd_FechaInicio"  Value ="0" runat="server" />  
                                            <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                TargetControlID="tbFechaInicio"
                                                UserDateFormat="DayMonthYear"                                                                    
                                                Mask="99/99/9999" 
                                                MaskType="Date" 
                                                PromptCharacter="-">
                                            </atk:MaskedEditExtender>                                                                                                                          
                                        </td>
                                        <td valign="middle" align="left" style="width: 30px; height: 25px;">
                                            <asp:ImageButton runat="server" ID="Image1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                            <atk:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                TargetControlID="tbFechaInicio"
                                                PopupButtonID="Image1" 
                                                Format="dd/MM/yyyy" 
                                                CssClass="MyCalendar" Enabled="True" />       
                                        </td>
                                    </tr>
                                        </table>         
                                        
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField ItemStyle-Width="120" ItemStyle-CssClass="miGridviewBusqueda_Rows" 
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Fecha Vencimiento">
                                            <HeaderTemplate>
                                           <asp:Label ID="lblFechaFin" runat =server Text="Fecha Fin"></asp:Label>
                                            </HeaderTemplate>  
                                        <ItemTemplate>

					                      <table cellpadding="0" cellspacing="0" border="0" width="120px">
                                    <tr>
                                       
                                        <td valign="middle" align="right" style="width: 90px; height: 25px;">
                                            <asp:TextBox ID="tbFechaFin" runat="server" Text='<%# Bind("FechaFin") %>' CssClass="miTextBoxCalendar" Width="80" />
                                             <asp:HiddenField  ID="hd_FechaFin" Value ="0" runat="server" />  
                                            <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                TargetControlID="tbFechaFin"
                                                UserDateFormat="DayMonthYear"                                                                    
                                                Mask="99/99/9999" 
                                                MaskType="Date" 
                                                PromptCharacter="-">
                                            </atk:MaskedEditExtender>                                                                                                                          
                                        </td>
                                        <td valign="middle" align="left" style="width: 30px; height: 25px;">
                                            <asp:ImageButton runat="server" ID="Image2" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                            <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                TargetControlID="tbFechaFin"
                                                PopupButtonID="Image2" 
                                                Format="dd/MM/yyyy" 
                                                CssClass="MyCalendar" Enabled="True" />       
                                        </td>
                                    </tr>
                                    </table>
                                        
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        </Columns>
                                        </asp:GridView>   
                                </div>                            
                           
                                </td>
                            </tr>
                                                      
                            <tr><td colspan="5"><br /></td></tr>          
                        </table>  
                          <div id="controlAgregarRegistro" style="display:none">
            <input type="button" id="VerAgregarRegistro" runat="server" />
            <input type="button" id="OKAgregarRegistro" />
            <input type="button" id="CancelAgregarRegistro" />
        </div>       
                        </asp:panel>                     
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
                                        DropShadow="True"
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

