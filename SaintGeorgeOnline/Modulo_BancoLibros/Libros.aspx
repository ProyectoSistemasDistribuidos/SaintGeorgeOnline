<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="Libros.aspx.vb" Inherits="Modulo_BancoLibros_Libros" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--ok--%>
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
    #panelRegistro span{
        font-size: 11px;
        font-family: Arial;
    }
    #panelRegistro em{
        font-size: 10px;
        font-family: Arial;
        color: #a51515;
        margin-right: 7px;
        padding: 0;
    }    
   
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
    <Triggers>
      
        <asp:PostBackTrigger ControlID="TabContainer1$miTab2$btnSubir" />
        
    </Triggers>
   <ContentTemplate>
    
 <div id="miContainerMantenimiento">
   <atk:TabContainer ID="TabContainer1" runat="server" Width="881px"  ActiveTabIndex="0"
        AutoPostBack="false" ScrollBars="None" >
        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
            <ContentTemplate>
                <div style="border: solid 0px blue; width: 870px;">
                    <div id="miBusquedaActualizacion_Ficha">
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border ="0" style="border: solid 0px red; min-width: 820px;">                                
                               <tr>
                                    <td style="padding-left :15px; width: 100px; height: 25px;" align="left" valign="bottom">
                                        <span >Año:&nbsp;</span>
                                    </td>
                                    <td style=" width: 620px; height: 25px;" align="left" valign="bottom">
                                         <asp:DropDownList ID="ddlBuscarAnio" runat="server" Width="100px">
                                        </asp:DropDownList>  
                                         <asp:HiddenField ID="hfTotalRegs" runat="server" />   
                                    </td>
                                    <td style=" width: 100px; height: 25px;" align="right" valign="top">
                                         <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left :15px; width: 100px; height: 25px;" align="left" valign="bottom">
                                        <span>Titulo :&nbsp;</span>
                                    </td>
                                    <td style=" width: 620px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbBuscarTitulo" runat="server" CssClass="miTextBox" Width="400px"
                                            BackColor="#FFFFCC"/>
                                    </td>
                                    <td style=" width: 100px; height: 25px;" align="right" valign="top">
                                            <asp:ImageButton ID="btnLimpiar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>                                           
                                    </td>
                                    
                                </tr>
                                  <tr>
                                    <td style="padding-left :15px; width: 100px; height: 25px;" align="left" valign="bottom">
                                        <span>Idioma:</span>
                                    </td>
                                    <td align="left" style="width: 620px; height: 25px;" valign="bottom">
                                       <asp:RadioButtonList ID="rdBuscarIdioma"  runat="server" RepeatDirection="Horizontal" Width ="300px" Height="5px">
                                            <asp:ListItem Value="0" Text="Todos" Selected="True" />
                                            <asp:ListItem Value="1" Text="Ingles" />
                                            <asp:ListItem Value="2" Text="Español" />
                                            <asp:ListItem Value="3" Text="Frances" />                                        
                                            <asp:ListItem Value="4" Text="Otro" />  
                                        </asp:RadioButtonList>     
                                    </td>     
                                     <td style="width: 100px; height: 25px;" align="right" valign="top">
                                           <asp:ImageButton ID="ImageButton2" runat="server" Width="74px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnNuevo_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnNuevo_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnNuevo_1.png'" 
                                            onclick="btnNuevo_Click" 
                                            ToolTip="Nuevo Registro"/> 
                                    </td>                              
                                </tr>
                                <tr>
                                    <td style="padding-left :15px; width: 100px; height: 25px;" align="left" valign="bottom">
                                        <span>ISBN :&nbsp;</span>
                                       <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbBuscarISBN" 
                                            ValidChars="' ','Ñ','ñ','.','á','é','í','ó','ú','(',')'" Enabled="True"  >
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td style=" width: 620px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbBuscarISBN" runat="server" CssClass="miTextBox" Width="400px"
                                        BackColor="#FFFFCC" />
                                    </td>
                                    <td style=" width: 100px; height: 25px;" align="right" valign="top">
                                        
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-left :15px; width: 100px; height: 25px;" align="left" valign="middle">
                                        <span>Grado</span>
                                    </td>
                                    <td colspan="2" align="left" style="width: 720px; height: 25px;" valign="bottom">
                                        <table border ="0" style="width: 720px;">
                                            <td align="left" style="width: 50px; height: 25px; padding-bottom :3px;" valign="bottom">
                                                 <asp:CheckBox ID="chkAll" Width ="50px"  Text="Todos" style="font-size:12px;" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />  
                                                 </asp:CheckBox> 
                                            </td>
                                            <td align="left" style="width: 670px; height: 25px;" valign="bottom">
                                                <asp:CheckBoxList ID="chkBuscarGrados" Width ="500px"   runat="server" RepeatDirection="Horizontal" style=" padding-left:5px;  font-size:12px;"    >
                                                </asp:CheckBoxList>  
                                            </td>
                                        </table>
                                          
                                    </td>
                                   
                                </tr>
                            </table>
                        </fieldset>
                    </div>    
                    <div class="miEspacio"></div>  
                              
                    <div id="misRegistrosEncontrados" style =" visibility:collapse;  width :800px;" >
                        <fieldset style ="width :840px;" >
                            <table cellpadding="0" cellspacing="0" style="border: solid 0px red;">
                                <tr>
                                    <td style="width: 100px; height: 21px;" align="left" valign="middle">
                                        <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                            ToolTip="Exportar" 
                                            OnClick="btnExportar_Click" />
                                    </td>
                                    <td style="width: 410px; height: 21px;" align="left" valign="middle">   
                                        <div id="miBusquedaActualizacion_Ficha" style="width:690px;">                              
                                       <fieldset style="width:710px;">
                                       <legend style="width: 100px;">Tipo de Reporte</legend>
                                        <table >
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="rbTipoReporte" runat="server" RepeatDirection="Horizontal" width="350px">
                                                        <asp:ListItem Value="0" Text="Stock de Libros"  />
                                                        <asp:ListItem Value="1" Text="Resumen de Prestamos" Selected="True"/>
                                                        <asp:ListItem Value="2" Text="Deudas de Libros"/>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr> 
                                        </table>
                                        </fieldset>
                                        </div>
                                 </td>                                                                                                 
                                </tr>
                            </table>
                           
                        </fieldset>                    
                    </div> 
                    
                    <div class="miEspacio"></div>  
                     <div id="miGridviewMantActualizacion_Ficha">
                    
                        <table  cellpadding="0" cellspacing="0" style="border: solid 0px red; min-width: 820px;">                                
                             <tr>
                                <td rowspan="2" style="border-top:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:28px; height: 26px; text-align:center; color:White;font-size:10px; background-color:#0a0f14;" align="center">
                                </td>
                                <td rowspan="2" style="border-top:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:28px; height: 26px; text-align:center; color:White;font-size:10px; background-color:#0a0f14;" align="center">
                                </td>
                                <td rowspan="2" style="border-top:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:68px; height: 26px; text-align:center; color:White;font-size:10px; background-color:#0a0f14;" align="center">
                                Codigo
                                </td>
                                <td rowspan="2" style="border-top:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width: 150px; height: 26px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;"  align="left" >
                                    Titulo</td>
                                <td rowspan="2" style="border-top:solid; border-bottom:solid;  border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width:150px; height: 26px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;"  align="left" >
                                    Editorial</td>
                                <td rowspan="2" style="border-top:solid; border-bottom:solid;  border-width:1px;  border-color:#a6a3a3; width:100px; height: 26px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;"  align="left" >
                                    ISBN</td>
                                <td rowspan="2" style="border-top:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width:100px; height: 26px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;"  align="left" >
                                    Grado</td>                                
                               
                                <td colspan="4" style="border-style:solid;   border-width:1px;  border-color:#a6a3a3;  width:260px; height: 26px; text-align:center; color:White;font-size:10px; background-color:#0a0f14; " align="center" >
                                 Detalle de Libros
                                 </td>
                            </tr>
                            <tr>                                                                     
                             <td  style="border-left:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width: 65px; height: 26px; text-align:center; color:White;font-size:10px;background-color:#0a0f14;" align="center" >
                             Total
                             </td>
                             <td  style="border-left:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width: 65px; height: 26px; text-align:center; color:White;font-size:10px;background-color:#0a0f14;" align="center">
                             Prestados
                             </td>
                             <td  style="border-left:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width: 65px; height: 26px; text-align:center; color:White;font-size:10px;background-color:#0a0f14;" align="center">
                             Devueltos
                             </td>
                             <td style="border-left:solid; border-right:solid; border-bottom:solid; border-width:1px;  border-color:#a6a3a3; width: 65px; height: 26px; text-align:center; color:White;font-size:10px; background-color:#0a0f14;" align="center" >
                             Disponibles
                             </td>
                            </tr>
                             <tr>
                                <td colspan ="11" style="width: 840px;" align="left" valign="middle">  
                                    <asp:GridView ID="GridView1" runat="server" 
                                        Width="839px" 
                                        CssClass="miGridviewBusqueda" 
                                        GridLines="None" 
                                        AutoGenerateColumns="False"
                                        AllowPaging="True" 
                                        AllowSorting="True"
                                        EmptyDataText=" - No se encontraron resultados - "
                                        OnPageIndexChanging="GridView1_PageIndexChanging" 
                                        OnRowDataBound="GridView1_RowDataBound"
                                        OnRowCommand="GridView1_RowCommand"
                                        OnRowCreated="GridView1_RowCreated"
                                        OnSorting="GridView1_Sorting" ShowHeader="False" >
                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                                        <Columns>     
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a onclick ="return false;" class="preview" id="btnVerPortada" runat="server" >
                                                        <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                                
                                                </ItemTemplate>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>                                    
                                                    <asp:Label ID="lblRutaFoto" runat="server" Text='<%# Bind("RutaPortada") %>' />                                                                                    
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="miHiddenStyle" Width="0px" />
                                                <ItemStyle CssClass="miHiddenStyle" Width="0px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                                        CommandName="Actualizar" CommandArgument='<%# Bind("CodigoLibro") %>' ToolTip="Actualizar Registro" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoLibro") %>' ToolTip="Eliminar Registro" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField >                                                                      
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigoLibro" runat="server" Text='<%# Bind("CodigoLibro") %>' />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="41px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>  
                                                <ItemTemplate>
                                                    <asp:Label  ID="lblTitulo" runat="server" Text='<%# Bind("Titulo") %>' />
                                                </ItemTemplate>
                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="170px"  />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>  
                                               <ItemTemplate>
                                                    <asp:Label ID="lblEditorial" runat="server" Text='<%# Bind("Editorial") %>' />
                                               </ItemTemplate>
                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="140px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>  
                                                <ItemTemplate>
                                                    <asp:Label ID="lblISBN" runat="server" Text='<%# Bind("ISBN") %>' />
                                                </ItemTemplate>
                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="120px" />
                                            </asp:TemplateField>   
                                            
                                            <asp:TemplateField>  
                                               <ItemTemplate>
                                                    <asp:Label ID="lblGrado" runat="server" Text='<%# Bind("Grado") %>' />
                                               </ItemTemplate>
                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="100px" />
                                            </asp:TemplateField>    
                                                                                        
                                            <asp:TemplateField>  
                                               <ItemTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>' />
                                               </ItemTemplate>
                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>   
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnVerTotal" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_agregar.png" 
                                                        CommandName="Total" CommandArgument='<%# Bind("CodigoLibro") %>' ToolTip="Ver Total de libros" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>  
                                               <ItemTemplate>
                                                    <asp:Label ID="lblPrestado" runat="server" Text='<%# Bind("Prestado") %>' />
                                               </ItemTemplate>
                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnVerPrestado" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_agregar.png" 
                                                        CommandName="Prestado" CommandArgument='<%# Bind("CodigoLibro") %>' ToolTip="Ver libros prestados" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>  
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDevueltos" runat="server" Text='<%# Bind("Devueltos") %>' />
                                                </ItemTemplate>
                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>    
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnVerDevueltos" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_agregar.png" 
                                                        CommandName="Devueltos" CommandArgument='<%# Bind("CodigoLibro") %>' ToolTip="Ver libros Devueltos" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>  
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDisponible" runat="server" Text='<%# Bind("Disponible") %>' />
                                                </ItemTemplate>
                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>    
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnVerDisponible" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_agregar.png" 
                                                        CommandName="Disponible" CommandArgument='<%# Bind("CodigoLibro") %>' ToolTip="Ver libros Disponibles" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                            </asp:TemplateField>
                                                                                       
                                            <asp:BoundField DataField="Estado" >
                                                 <HeaderStyle CssClass="miHiddenStyle" />
                                                 <ItemStyle CssClass="miHiddenStyle" />
                                            </asp:BoundField>
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
                                                        <asp:Button ID="btnFirst" runat="server" CommandName="Page" ToolTip="Primera Pagina" CommandArgument="First"
                                                            CssClass="pagfirst" />
                                                        <asp:Button ID="btnPrevious" runat="server" CommandName="Page" ToolTip="Página anterior"
                                                            CommandArgument="Prev" CssClass="pagprev" />
                                                        <asp:Button ID="btnNext" runat="server" CommandName="Page" ToolTip="Página siguiente"
                                                            CommandArgument="Next" CssClass="pagnext" />
                                                        <asp:Button ID="btnLast" runat="server" CommandName="Page" ToolTip="Última Pagina" CommandArgument="Last"
                                                            CssClass="paglast" />
                                                    </td>                                        
                                                    <td style="height: 20px; width: 200px;" align="right" valign="middle">
                                                        <asp:Label ID="lblRegistrosActuales" runat="server" CssClass="miFooterMantLabelRight" />
                                                    </td>                                        
                                                </tr>
                                            </table>
                                        </PagerTemplate>
                                    </asp:GridView> 
                                </td>
                            </tr>
                             <tr>
                               
                                <td colspan ="11" style="width: 840px;" align="left" valign="middle">  
                                     <div class="miEspacio">
                                    </div>
                                    
                                      <table id="GVLegenda_BL" border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
                                         <tr>
                                            <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                <img alt="Actualizar Registro" src="../App_Themes/Imagenes/opc_actualizar.png"/></td>
                                            <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                <span id="GVLegendaSpan_BL">Actualizar Registro</span></td>                              
                                            <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                <span id="GVLegendaSpan_BL">&#124;</span></td>      
                                            <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_eliminar.png"/></td>
                                            <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                <span id="GVLegendaSpan_BL">Eliminar Registro</span></td>  
                                            <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                <span id="GVLegendaSpan_BL">&#124;</span></td>                                    
                                            <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                <img alt="Activar Registro" src="../App_Themes/Imagenes/opc_foto.png"/></td>
                                            <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                <span id="GVLegendaSpan_BL">Ver Portada del Libro</span></td>                                      
                                            <td style="width: 200px"></td>                                                                     
                                        </tr>                        
                                      </table> 
                                   
                                </td>
                            </tr>                                    
                         
                        </table> 
                          
                     </div>
                          
                </div>    
            </ContentTemplate>
        </atk:TabPanel>  
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="false">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Reporte" />
            </HeaderTemplate>
            <ContentTemplate> 
                 <div style="border: solid 0px blue; width: 860px;">
                    <div id="miBusquedaActualizacion_Ficha">
                        <fieldset>
                            <legend>Datos del Libro</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 820px;">
                                <tr>
                                    <td colspan="3" style="padding-right:8px; width: 550px ;height: 15px;" align="right">
                                        <em>Campos Obligatorios (*)</em>
                                    </td>
                                     <td colspan="2" style="height: 15px; width: 270px ;" align="right">
                                       <asp:Label ID="lblObtieneCodigoLibro"  runat ="server" Visible ="False"></asp:Label>
                                    </td>
                                </tr>  
                                <tr>
                                     <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Codigo : </span>
                                    </td>
                                     <td colspan="2" style="width:400px; height: 25px;"  align="left" valign="bottom">
                                       <asp:Label ID="lblCodigoLibroR"  runat ="server" Font-Bold="true" ForeColor="DarkRed" Font-Size="18px" ></asp:Label>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Título : </span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td colspan="2" style="width:400px; height: 25px;"  align="left" valign="bottom">
                                        <asp:TextBox ID="tbTitulo" runat="server" CssClass="miTextBox" Width="390px" MaxLength="100" BackColor="#FFFFCC" />
                                        <asp:HiddenField ID="hd_Codigo" runat="server" />
                                        <asp:HiddenField ID="hfTotalRegs1" runat="server" /> 
                                    </td>
                                    <td style="width: 180px; height: 25px; " align="center" valign="bottom" rowspan ="4">
                                        <asp:Image style="border:1px; border-style:solid; border-color:#a6a3a3; " ID="ImgFoto" runat="server" Width ="100px" 
                                            Height="90px" BorderColor="Red" />
                                         <asp:HiddenField ID="hd_rutaFotoImagen" runat="server" />                            
                                    </td>
                                    <td style="width: 90px; padding-top:6px" align="right" valign="top" rowspan="19">
                                                <asp:ImageButton ID="btnGrabar" runat="server" Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                                    onclick="btnGrabar_Click" /><br /><br />
                                                <asp:ImageButton ID="btnCancelar" runat="server" Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                                                    onclick="btnCancelar_Click" CausesValidation="False"/>                                              
                                    </td>
                                </tr>                                
                                <tr>
                                    <td align="left"  style="width:150px; height: 25px;"  valign="middle">
                                        <span>Idioma</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td align="left" valign="bottom" style="width:200px; height: 25px;"  >
                                        <asp:RadioButtonList ID="rbIdioma"  runat="server" RepeatDirection="Horizontal" Width="200px"  OnSelectedIndexChanged="rbIdioma_SelectedIndexChanged"   AutoPostBack="True"  >
                                            <asp:ListItem Value="1" Text="Ingles" />
                                            <asp:ListItem Value="2" Text="Español" Selected="True" />
                                            <asp:ListItem Value="3" Text="Frances" />                                        
                                            <asp:ListItem Value="4" Text="Otro" />  
                                        </asp:RadioButtonList>                                        
                                    </td>
                                    <td style="width: 200px; height: 25px;" align="left" valign="bottom">
                                       <asp:TextBox ID="tbIdiomaOtro" runat="server" CssClass="miTextBox" Width="187px" MaxLength="100" BackColor="#FFFFCC" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbIdiomaOtro" 
                                            ValidChars="' ','.','á','é','í','ó','ú','(',')'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                  
                                </tr>
                                <tr>
                                    <td align="left"  style="width:150px; height: 25px;"  valign="middle">
                                        <span>Autor : </span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="width: 400px; height: 25px;" align="left"  valign="middle" colspan="2">
                                        <asp:TextBox ID="tbAutor" runat="server" CssClass="miTextBox" Width="390px" MaxLength="100" BackColor="#FFFFCC" />                                                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Editorial : </span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="width: 400px; " align="left" valign="bottom" colspan="2">
                                        <asp:TextBox ID="tbEditorial" runat="server" CssClass="miTextBox" Width="390px" MaxLength="100" BackColor="#FFFFCC" />                                                                     
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Colección:</span>
                                    </td>
                                    <td style="width:400px; height: 25px;" align="left" valign="middle" colspan="2">
                                        <asp:TextBox ID="tbColeccion" runat="server" CssClass="miTextBox" Width="390px" MaxLength="100" BackColor="#FFFFCC" />
                                    </td>  
                                     <td  style="width: 180px; height: 25px; " align="center" valign="middle">
                                         <asp:ImageButton ID="btnVerImagenPoppup" runat="server" Width="90px" Height="20px" ImageUrl="~/App_Themes/Imagenes/btnVerImagenPoppup.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnVerImagenPoppup.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnVerImagenPoppup.png'" ToolTip="Ver la imagen en grande"
                                                    OnClick ="btnVerImagenPoppup_Click" CausesValidation="False"/>
                                    </td>                                       
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Nivel:</span>
                                    </td>
                                    <td style="width: 400px; height: 25px;"  align="left" valign="middle" colspan="2">
                                        <asp:TextBox ID="tbNivel" runat="server" CssClass="miTextBox" Width="390px" MaxLength="100" BackColor="#FFFFCC" />
                                    </td> 
                                    <td  style="width: 180px; height: 25px; " align="center" valign="bottom" >
                                    </td>                                       
                                </tr>
                                <tr>
                                    <td align="left"style="width:150px; height: 25px;"  valign="middle">
                                        <span>ISBN :</span>
                                    </td>
                                    <td style="width: 400px; height: 25px;" align="left" valign="middle" colspan="2">
                                        <asp:TextBox ID="tbISBN" runat="server" CssClass="miTextBox" Width="390px" MaxLength="100" BackColor="#FFFFCC" />
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="tbISBN" 
                                            ValidChars="' ','Ñ','ñ','-','á','é','í','ó','ú'" Enabled="True">
                                        </atk:FilteredTextBoxExtender>                                    
                                    </td>
                                    <td style="width: 180px; height: 25px;" align="left" valign="bottom">
                                        
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Precio de Libro : </span>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers" ValidChars="'.'" 
                                            TargetControlID="tbPrecioLibro" Enabled="True">
                                        </atk:FilteredTextBoxExtender>                            
                                    </td>
                                    <td align="left" valign="middle" style="width: 200px; height: 25px;">
                                         <asp:TextBox ID="tbPrecioLibro" runat="server" CssClass="miTextBox" Width="190px" MaxLength="100"  BackColor="#FFFFCC" ></asp:TextBox>
                                    </td>    
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 200px; height: 25px;">
                                            <tr>
                                                <td style="width: 60px; height: 25px;" align="left" valign="middle" >
                                                     <span>Moneda :</span>
                                                </td>
                                                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                                                      <asp:DropDownList ID="ddlMonedaPrecioLibro" runat="server" Width="100px">
                                                      </asp:DropDownList> 
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 180px; height: 25px;" align="center" valign="top" rowspan="12">
                                        <table style="border-style:solid; border-width:1px;  border-color:#a6a3a3; width: 150px;" cellpadding="0" cellspacing="0">   
                                            <tr>
                                                <td colspan="2" id="miHeaderDetCPago2"  style="border-style:solid; border-width:1px;  border-color:#a6a3a3; width: 70px; height: 25px; text-align:center ; color:White; font-size:10px; background-color:#0a0f14;"  align="left" >
                                                <b><span>Stocks de Libro</span></b></td>                                                
                                            </tr>
                                            <tr>
                                                <td style="padding-left:10px;  width:70px; height: 25px;" align="left" valign="middle" >
                                                  Total :  
                                                </td>
                                                 <td style="padding-left :20px; width: 80px; height: 25px;" align="left" valign="middle" >
                                                    <asp:TextBox ID="tbTotal" runat="server" CssClass="miTextBox" text="0"
                                                              Width="40px"  Height="18px" BackColor="#CCFF99" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left:10px; width:70px; height: 25px;" align="left" valign="middle" >
                                                  Disponible :  
                                                </td>
                                                 <td style="padding-left :20px; width: 80px; height: 25px;" align="left" valign="middle" >
                                                    <asp:TextBox ID="tbDisponible" runat="server" CssClass="miTextBox" text="0"
                                                              Width="40px"  Height="18px" BackColor="#CCFF99" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left:10px; width:70px; height: 25px;" align="left" valign="middle" >
                                                  Utilizado :  
                                                </td>
                                                 <td style="padding-left :20px; width: 80px; height: 25px;" align="left" valign="middle" >
                                                    <asp:TextBox ID="tbUtilizado" runat="server" CssClass="miTextBox" text="0"
                                                              Width="40px"  Height="18px" BackColor="#CCFF99" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding-left:10px; width:70px; height: 7px;" align="left" valign="bottom" >
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Precio de Prestamo:</span>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" ValidChars="'.'" 
                                            TargetControlID="tbPrecioPrestamo" 
                                            Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td align="left" valign="middle" style="width: 200px; height: 25px;" >
                                        <asp:TextBox ID="tbPrecioPrestamo" runat="server" CssClass="miTextBox" Width="190px" MaxLength="100" BackColor="#FFFFCC" ></asp:TextBox>
                                    </td>
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 200px; height: 25px;">
                                            <tr>
                                                <td style="width: 60px; height: 25px;" align="left" valign="middle">
                                                     <span>Moneda :</span>
                                                </td>
                                                <td style="min-width: 140px; height: 25px;" align="left" valign="middle">
                                                      <asp:DropDownList ID="ddlMonedaPrecioPrestamo" runat="server" Width="100px">
                                                      </asp:DropDownList> 
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Precio de Reposición:</span>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="'.'" TargetControlID="tbPrecioReposicion" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td align="left" valign="middle" style="width: 200px; height: 25px;">
                                        <asp:TextBox ID="tbPrecioReposicion" runat="server" CssClass="miTextBox" Width="190px"
                                            MaxLength="100" BackColor="#FFFFCC"></asp:TextBox>
                                    </td>
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 200px; height: 25px;">
                                           
                                            <tr>
                                                <td style="width: 60px; height: 25px;" align="left" valign="middle">
                                                    <span>Moneda :</span>
                                                </td>
                                                <td style="min-width: 140px; height: 25px;" align="left" valign="middle">
                                                    <asp:DropDownList ID="ddlMonedaPrecioReposicion" runat="server" Width="100px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Largo:</span>
                                        <atk:FilteredTextBoxExtender ID="FTBLargo" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="'.'" TargetControlID="tbLargo" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td  colspan ="2" align="left" valign="middle" style="width: 400px; height: 25px;">
                                        <asp:TextBox ID="tbLargo" runat="server" CssClass="miTextBox" Width="190px" MaxLength="100"
                                            BackColor="#FFFFCC"></asp:TextBox>
                                    </td>
                                </tr>                               
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Ancho:</span>
                                        <atk:FilteredTextBoxExtender ID="FTBAncho" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="'.'" TargetControlID="tbAncho" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td colspan ="2" align="left" valign="middle" style="width: 400px; height: 25px;">
                                        <asp:TextBox ID="tbAncho" runat="server" CssClass="miTextBox" Width="190px" MaxLength="100"
                                            BackColor="#FFFFCC"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"  style="width:150px; height: 25px;"  valign="middle">
                                        <span>Grosor:</span>
                                        <atk:FilteredTextBoxExtender ID="FTBGrosor" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="'.'" TargetControlID="tbGrosor" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td colspan ="2" align="left" valign="middle" style="width: 400px; height: 25px;">
                                        <asp:TextBox ID="tbGrosor" runat="server" CssClass="miTextBox" Width="190px" MaxLength="100"
                                            BackColor="#FFFFCC"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"  style="width:150px; height: 25px;"  valign="middle">
                                        <span>Edición:</span>
                                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="'.'" TargetControlID="tbGrosor" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                                    </td>
                                    <td colspan ="2" align="left" valign="middle" style="width: 400px; height: 25px;">
                                        <asp:TextBox ID="tbEdicion" runat="server" CssClass="miTextBox" Width="190px" MaxLength="100"
                                            BackColor="#FFFFCC"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                         <span>Sede:</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td colspan ="2" align="left" valign="middle" style="width: 400px; height: 25px;">
                                        <asp:DropDownList ID="ddlSede" runat="server" Width="190px" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left"  style="width:150px; height: 25px;"  valign="middle">
                                        <span>Grado:</span>
                                    </td>
                                    <td colspan ="2" align="left" valign="bottom" style="width: 400px; height: 25px;">
                                        <asp:CheckBoxList ID="chkGrados" runat="server" Width="400px" Height="25px" style=" font-size:10px; "
                                            RepeatDirection="Horizontal" >
                                           
                                        </asp:CheckBoxList>
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td align="left"  style="width:150px; height: 25px;"  valign="middle">
                                        <span>Tipo de Libro:</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td colspan ="2" align="left" valign="middle" style="width: 400px; height: 25px;">
                                        <asp:RadioButtonList ID="rbTipoLibro" runat="server" Width="280px" Height="5px" OnSelectedIndexChanged="rbTipoLibro_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                        </asp:RadioButtonList>
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <asp:Label ID="lbCurso" runat="server" Text="Curso:"></asp:Label>
                                    </td>
                                    <td colspan ="2" align="left" valign="middle" style="width: 400px; height: 25px;">
                                        <asp:DropDownList ID="ddlCursosLibros" runat="server" Width="190px" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        <span>Buscar Imagen:</span>
                                    </td>
                                     <td style="width: 200px; height: 25px;" align="left" valign="middle">                                             
                                             <asp:FileUpload ID="tbRutaImagenPortada" runat="server" Width="200px" BackColor="#FFFFCC" />                                                       
                                    </td>
                                     <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                        <asp:TextBox ID="TbRutaIcono" runat="server" CssClass="miTextBox" Height="20px" 
                                            Width="150px" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                    </td>                                    
                                 </tr>                                
                                <tr>
                                    <td align="left" style="width:150px; height: 25px;"  valign="middle">
                                        
                                    </td>
                                    <td align="right" valign="middle" style="width: 200px; height: 25px;">
                                       <asp:ImageButton ID="btnSubir" runat="server" ImageUrl="~/App_Themes/Imagenes/btnAdjuntarImagen.jpg"
                                        ToolTip="Clic para adjuntar imagen"
                                        onclick="btn_SubirImagen_Click" CausesValidation="False"/>
                                    </td>     
                                    <td style="width: 200px; height: 25px;" align="left" valign="middle">
                                    </td>   
                                    <%--<td style="width: 180px; height: 25px;" align="left" valign="middle">
                                    </td>  --%>                                
                                 </tr>                                
                            </table>
                        </fieldset>
                    </div>
                    <div class="miEspacio">
                    </div>                    
                    <div id="miBusquedaActualizacion_Ficha">
                        <fieldset>
                         <legend>Datos de Ejemplares</legend>
                         
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 610px;">                                
                                <tr>
                                    <td style="width: 610px; height: 10px;" align="left" valign="top" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 500px;" align="left" valign="middle">
                                        <table cellpadding="0" cellspacing="0" border="2" style="border: solid 0px red; width: 500px;">                                
                                             <tr>
                                                <td class="miGVBusquedaFicha_Header" style="border-top:solid; border-left:solid;  border-width:1px;  border-color:#999999; width: 30px; height: 26px; color:White; font-size:10px; background-color:#0a0f14;" align="center">
                                                </td>
                                              <%--  <td class="miGVBusquedaFicha_Header" style="border-top:solid; border-width:1px;  border-color:#999999; width: 100px; height: 26px; color:White; font-size:10px; background-color:#0a0f14; text-align:center"  align="left" >
                                                   <span>Código de Ejemplar</span>
                                                 </td>--%>
                                                 <td class="miGVBusquedaFicha_Header" style="border-top:solid; border-width:1px;  border-color:#999999; width: 160px; height: 26px; color:White; font-size:10px; background-color:#0a0f14; text-align:center"  align="left" >
                                                   <span>Código de Barra</span>
                                                 </td>
                                                <td class="miGVBusquedaFicha_Header" style="border-top:solid; border-width:1px;  border-color:#999999; width:  80px; height: 26px; color:White; font-size:10px; background-color:#0a0f14; text-align:center"  align="center" >
                                                   <span>Fec. Compra</span>
                                                </td>
                                                <td class="miGVBusquedaFicha_Header" style="border-top:solid; border-width:1px;  border-color:#999999; width: 150px; height: 26px; color:White; font-size:10px; background-color:#0a0f14; text-align:center"  align="center" >
                                                   <span>N° Comprobante</span>
                                                </td>
                                                <td class="miGVBusquedaFicha_Header" style="border-top:solid; border-right:solid;  border-width:1px;  border-color:#999999; width: 80px; height: 26px; color:White; font-size:10px; background-color:#0a0f14; text-align:center"  align="center" >
                                                    <span>Estado</span>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td colspan="5" style="width: 500px;" align="left" valign="middle">
                                                <div style="width: 500px; border: solid 1px #999999">                                                     
                                                    <asp:GridView ID="GV_DatosEjemplares" runat="server" 
                                                        Width="500px" 
                                                        CssClass="miGridviewBusqueda" 
                                                        AutoGenerateColumns="False"
                                                        GridLines="None" 
                                                        AllowPaging="True" 
                                                        AllowSorting="True"
                                                        EmptyDataText=" - No se encontraron resultados - "
                                                        OnRowDataBound="GV_DatosEjemplares_RowDataBound"
                                                        OnRowCommand="GV_DatosEjemplares_RowCommand"
                                                        OnRowCreated="GV_DatosEjemplares_RowCreated"
                                                        OnPageIndexChanging="GV_DatosEjemplares_PageIndexChanging" 
                                                        ShowHeader="False" >
                                                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                                                        <Columns>     
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                                        CommandName="Eliminar" CommandArgument='<%# Bind("CodigoCopiaLibro") %>' ToolTip="Eliminar Registro" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField>                                                                      
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoDato" runat="server" Text='<%# Bind("TipoDato") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                            </asp:TemplateField>
                                                            
                                                              <asp:TemplateField>                                                                      
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoLibro" runat="server" Text='<%# Bind("CodigoLibro") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField>                                                                      
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoCopiaLibro" runat="server" Text='<%# Bind("CodigoCopiaLibro") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                            </asp:TemplateField>
                                                                     
                                                            <asp:TemplateField>  
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNumeroEjemplar" runat="server" Text='<%# Bind("NumeroEjemplar") %>' />
                                                                </ItemTemplate>
                                                               <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                            </asp:TemplateField>
                                                            
                                                          <%--  <asp:TemplateField>  
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoBarra" runat="server" Text='<%# Bind("CodigoBarra") %>' />
                                                               </ItemTemplate>
                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                            </asp:TemplateField>--%>
                                                           
                                                            <asp:TemplateField>                                                                      
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField>  
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoEjemplar" runat="server" Text='<%# Bind("CodigoEjemplar") %>' />
                                                                </ItemTemplate>
                                                             <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField>  
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoBarra" runat="server" Text='<%# Bind("CodigoBarra") %>' />
                                                                </ItemTemplate>
                                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="160px" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField>  
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaCompraStr" runat="server" Text='<%# Bind("FechaCompraStr") %>' />
                                                                </ItemTemplate>
                                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="80px" />
                                                            </asp:TemplateField> 
                                                            
                                                            <asp:TemplateField>  
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaCompra" runat="server" Text='<%# Bind("FechaCompra") %>' />
                                                                </ItemTemplate>
                                                               <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0px" />
                                                            </asp:TemplateField> 
                                                            
                                                            <asp:TemplateField>  
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNumeroPago" runat="server" Text='<%# Bind("NumeroPago") %>' />
                                                                </ItemTemplate>
                                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
                                                            </asp:TemplateField> 
                                                            
                                                            <asp:TemplateField>  
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDisponible" runat="server" Text='<%# Bind("Disponible") %>' />
                                                                </ItemTemplate>
                                                               <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="80px" />
                                                            </asp:TemplateField>                                                            
                                                          </Columns>  
                                                          
                                                          <PagerTemplate>
                                                               <table border="0" cellpadding="0" cellspacing="0" style="width: 500px;">
                                                                  <tr>                                        
                                                                        <td style="height: 20px; width: 150px;" align="left" valign="middle">
                                                                            <span class="miFooterMantLabelLeft">Ir a página   </span>                                         
                                                                            <asp:DropDownList ID="ddlPageSelector1" runat="server" 
                                                                                CssClass="letranormal" 
                                                                                AutoPostBack="true" 
                                                                                OnSelectedIndexChanged="ddlPageSelector1_SelectedIndexChanged">
                                                                            </asp:DropDownList>&nbsp; de
                                                                            <asp:Label ID="lblNumPaginas1" runat="server" />                                         
                                                                        </td>                                        
                                                                        <td style="height: 20px; width: 180px;" align="center" valign="middle">                                           
                                                                            <asp:Button ID="btnFirst1" runat="server" CommandName="Page" ToolTip="Primera Pagina" CommandArgument="First"
                                                                                CssClass="pagfirst" />
                                                                            <asp:Button ID="btnPrevious1" runat="server" CommandName="Page" ToolTip="Página anterior"
                                                                                CommandArgument="Prev" CssClass="pagprev" />
                                                                            <asp:Button ID="btnNext1" runat="server" CommandName="Page" ToolTip="Página siguiente"
                                                                                CommandArgument="Next" CssClass="pagnext" />
                                                                            <asp:Button ID="btnLast1" runat="server" CommandName="Page" ToolTip="Última Pagina" CommandArgument="Last"
                                                                                CssClass="paglast" />
                                                                        </td>                                        
                                                                        <td style="height: 20px; width: 150px;" align="right" valign="middle">
                                                                            <asp:Label ID="lblRegistrosActuales1" runat="server" CssClass="miFooterMantLabelRight" />
                                                                        </td>                                        
                                                                  </tr>
                                                               </table>
                                                          </PagerTemplate>
                                                    </asp:GridView>                                                    
                                                </div>    
                                                </td>
                                            </tr>
                                            <tr>
                                               <td colspan="5" style="width:500px;" align="left" valign="middle">  
                                                    <div class="miEspacio">
                                                    </div>
                                                        <table id="GVLegenda_BL"  border="0" cellpadding="0" cellspacing="0" style="width: 502px;">
                                                         <tr>
                                                            <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                              <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_eliminar.png"/></td>
                                                            <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                                 <span id="GVLegendaSpan_BL">Eliminar Registro</span></td>  
                                                            <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                               </td>  
                                                            <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                              </td>  
                                                            <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                              </td>  
                                                            <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                                </td>  
                                                             <td style="width: 202px; height: 26px;" align="center" valign="middle">
                                                            </td>                
                                                        </tr>                        
                                                      </table> 
                                               </td>
                                             </tr>
                                         </table>                                    
                                    </td>
                                    <td style="width: 110px; height: 21px;" align="center" valign="top">
                                        <asp:ImageButton ID="btn_Add_CopiaLibro" runat="server" Width="74px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnNuevo_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnNuevo_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnNuevo_1.png'" 
                                            OnClick ="btn_Add_CopiaLibro_click"
                                            ToolTip="Nuevo Registro" />
                                    </td>
                                </tr>                                    
                            </table>       
                                       
                        </fieldset>                    
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
                                        
                                        
              <atk:ModalPopupExtender ID="pnModalAgregarRegistro" runat="server"
                                                    TargetControlID="VerAgregarRegistro"
                                                    PopupControlID="pnlAgregarRegistro"
                                                    BackgroundCssClass="MiModalBackground" 
                                                    DropShadow="true" 
                                                    OkControlID="OKAgregarRegistro" 
                                                    CancelControlID="CancelAgregarRegistro"
                                                    Drag="true" 
                                                    PopupDragHandleControlID="AgregarRegistroHeader" />   
                
              <atk:ModalPopupExtender ID="pnlModalVerImagen" runat="server"
                                                    TargetControlID="VerImagen"
                                                    PopupControlID="pnlVerImagen"
                                                    BackgroundCssClass="MiModalBackground" 
                                                    DropShadow="true" 
                                                    Drag="true" 
                                                    PopupDragHandleControlID="VerImagenHeader" />
                  
              <atk:ModalPopupExtender ID="pnlModalListadoCopiaLibros" runat="server"
                                                    TargetControlID="VerAgregarLista"
                                                    PopupControlID="pnlListadoCopiaLibros"
                                                    BackgroundCssClass="MiModalBackground" 
                                                    OkControlID="OKAgregarListadoCopiaLibros" 
                                                    CancelControlID="CancelAgregarListadoCopiaLibros"
                                                    Drag="true" 
                                                    PopupDragHandleControlID="AgregarListadoCopiaLibrosHeader" />
    
    <asp:panel id="pnlListadoCopiaLibros" BackColor="White" BorderColor="Black" runat="server" style ="height:600;">
        <table cellpadding="0" cellspacing="0" border="0"  width="480px" id="panelRegistro">    
            <tr>
                <td style="width: 470px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="5" id="AgregarListadoCopiaLibrosHeader" >                
                    <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Nuevo Registro</span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                    <asp:ImageButton ID="btnCerraListadoCopiaLibros" runat="server" Width="16" Height="15"
                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        onclick="btnCerraListadoCopiaLibros_Click" ToolTip="Cerrar Panel"/>
                </td>
            </tr>
            <tr><td height="5px" colspan="2"></td></tr>   
            <tr>
                <td colspan="2" align="left" valign="top" style="padding-left:10px; width:480px">   
                
                                     
                </td>
            </tr>
             <tr><td  height="5px" colspan="2"></td></tr>  
                
            
        </table>  
        <div id="controlAgregarRegistro" style="display:none">
            <input type="button" id="VerAgregarLista" runat="server"   />
            <input type="button" id="OKAgregarListadoCopiaLibros" />
            <input type="button" id="CancelAgregarListadoCopiaLibros" />
        </div>       
    </asp:panel>    
                 
    <asp:Panel ID="pnlAgregarRegistro" BackColor="White" BorderColor="Black" runat="server" style="display: none;">
         <table cellpadding="0" cellspacing="0" border="0" style="width: 500px; border: solid 1px #000000;" id="panelRegistro">
             <tr>
                 <td style="width: 470px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2"
                     colspan="2" id="AgregarRegistroHeader">
                     <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial;
                         cursor: pointer;">Nuevo Registro</span>
                 </td>
                 <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                     <asp:ImageButton ID="btnCerraAgregarRegistro" runat="server" Width="16" Height="15"
                         ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" OnClick="btnCerrarModal_Click"
                         ToolTip="Cerrar Panel" />
                 </td>
             </tr>
             <tr><td colspan="3"><br /></td></tr>
             <tr>
                 <td style="width: 30px; height: 25px;" rowspan="4">
                 </td>
                 <td align="left" valign="top" style="width: 440px">
                     <table border="0" cellpadding="0" cellspacing="0" style="width: 440px;">
                         <tr>
                             <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                 <span>Cantidad de Libros :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                 <asp:HiddenField ID="hd_CodigoDetalle" runat="server" />
                             </td>
                             <td style="width: 320px; height: 25px;" align="left" valign="middle">
                                 <asp:TextBox ID="tbCantidadLibros" runat="server" CssClass="miTextBox" Width="200px" MaxLength="100" style="font-size: 8pt; font-family: Arial; background-color: #FFFFCC" />
                                 <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers"
                                     TargetControlID="tbCantidadLibros" ValidChars="' ','.','á','é','í','ó','ú','(',')','_','-'" Enabled="True">
                                 </atk:FilteredTextBoxExtender>
                             </td>
                         </tr>
                         <tr>
                            <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                <span>Fec. Compra :&nbsp;</span><span class="camposObligatorios">(*)</span>    
                            </td>
                            <td style="width: 320px; height: 25px;" align="left" valign="middle">  
                                <asp:TextBox ID="tbFechaCompra" runat="server" CssClass="miTextBoxCalendar" Width="80px" style="font-size: 8pt; font-family: Arial; background-color: #FFFFCC"/>
                                <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                    TargetControlID="tbFechaCompra"
                                    UserDateFormat="DayMonthYear"                                                                    
                                    Mask="99/99/9999" 
                                    MaskType="Date" 
                                    PromptCharacter="-" Enabled="True">
                                </atk:MaskedEditExtender>
                            </td>             
                         </tr>
                        <tr>
                             <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                 <span>N° Comprobante :&nbsp;</span><span class="camposObligatorios">(*)</span>
                             </td>
                             <td style="width: 320px; height: 25px;" align="left" valign="middle">
                                 <asp:TextBox ID="tbNumeroPago" runat="server" CssClass="miTextBox" Width="200px" MaxLength="100" style="font-size: 8pt; font-family: Arial; background-color: #FFFFCC" />
                                 <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" 
                                     TargetControlID="tbNumeroPago" ValidChars="' ','.','(',')','_','-'" Enabled="True">
                                 </atk:FilteredTextBoxExtender>
                             </td>
                         </tr>       
                         <%--<tr>
                             <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                 <span>Código de Barra :&nbsp;</span><span class="camposObligatorios">(*)</span>
                             </td>
                             <td style="width: 320px; height: 25px;" align="left" valign="middle">
                                 <asp:TextBox ID="tbCodigoBarra" runat="server" CssClass="miTextBox" Width="200px" MaxLength="100" style="font-size: 8pt; font-family: Arial; background-color: #FFFFCC" />
                             </td>
                         </tr>       --%>                  
                     </table>
                 </td>
                 <td style="width: 30px; height: 25px;" rowspan="4">
                 </td>
             </tr>
             <tr><td><br /></td></tr>
             <tr>
                 <td align="left" valign="middle">
                     <asp:ImageButton ID="btnGrabarDetalle" runat="server" Width="74" Height="19"
                         ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'"
                         onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                         OnClick="btnGrabarDetalle_Click" />&nbsp;
                     <asp:ImageButton ID="btnCancelarDetalle" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                         onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'"
                         ToolTip="Cancelar" OnClick="btnCancelarDetalle_Click" CausesValidation="false" />
                 </td>
                 <asp:Label ID="lblCodigoCopiaLibro" runat="server" Value="0" Visible="false" />
             </tr>
             <tr><td><br /></td></tr>
         </table>
         <div id="Div2" style="display: none">
             <input type="button" id="VerAgregarRegistro" runat="server" />
             <input type="button" id="OKAgregarRegistro" />
             <input type="button" id="CancelAgregarRegistro" />
         </div>
     </asp:Panel>
    
    <asp:panel id="pnlVerImagen" BackColor="White" BorderColor="Black" runat="server" style ="height:400;">
        <table cellpadding="0" cellspacing="0" border="0"  width="400px" id="Table1">    
            <tr>
                <td style="width: 470px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="5" id="VerImagenHeader" >                
                    <span style="padding-left:5px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Nombre del Libro : &nbsp;<asp:Label ID="lblCabNombreTitulo" runat="server" /></span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                    <asp:ImageButton ID="btnCerrarVerImagen" runat="server" Width="16" Height="15"
                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        onclick="btnCerrarModal_Click" ToolTip="Cerrar Panel"/>
                </td>
            </tr>
            <tr><td height="5px" colspan="2"></td></tr>   
            <tr>
                <td colspan="2" align="left" valign="top" style="padding-left:10px; width:470px">   
                
                    <table border="0" cellpadding="0" cellspacing="0" style="width:460">
                        <tr>
                           
                            <td align="center" valign="top" style="width:470px">
                                 <asp:Image style="border:1px; border-style:solid; border-color:#a6a3a3; " ID="img_FotoGrande" runat="server" Width ="400px" 
                                            Height="350px" BorderColor="Red" 
                                             />                                                   
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 250px; height: 5px;" align="left" valign="bottom">
                            </td>
                        </tr>                   
                    </table>
                                    
                </td>
            </tr>
              
        </table>  
        <div id="Div1" style="display:none">
            <input type="button" id="VerImagen" runat="server" />
           
        </div>       
    </asp:panel>                           
                            
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

