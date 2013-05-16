<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BuscarTalonarios.ascx.vb" Inherits="Controles_BuscarTalonarios" %>

<style type="text/css">   
    .miTextBox{
        border: solid 1px #a6a3a3;
        height: 15px;
    }
    .miTextBox:hover{
        border: solid 1px #7F9DB9;
        height: 15px;
    }    
    .modalBackground{
	    background-color: black;    
	    filter:alpha(opacity=70);
	    opacity:0.7;
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
    #panelRegistro .header{        
        background: #0a0f14 url(/SaintGeorgeOnline/App_Themes/Imagenes/legend_header.gif) repeat-x;
        text-align: left;
        color: black;
        height: 26px;        
        border-bottom: solid 1px black;
    }           
</style>  

    <asp:UpdatePanel ID="UpdatePanel_BuscarTalonarios" runat="server">
    <ContentTemplate>      
    <atk:ModalPopupExtender ID="ModalPopupExtender_BuscarTalonarios" runat="server"
        PopupControlID="Panel_BuscarTalonarios"
        TargetControlID="btnVer_BuscarTalonarios"
        OkControlID="btnOK_BuscarTalonarios" 
        CancelControlID="btnCancel_BuscarTalonarios"
        BackgroundCssClass="modalBackground" 
        Drag="True" PopupDragHandleControlID="dragCtrl_BuscarTalonarios" Enabled="True" />      
    <asp:panel id="Panel_BuscarTalonarios" BackColor="White" BorderColor="Black" BorderWidth="1" runat="server" style="width: 800px; display: none;">
        <table cellpadding="0" cellspacing="0" border="0" id="panelRegistro" style="width: 800px;">          
            <tr>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">                    
                    <span style="width:30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> 
                </td>
                <td style="width: 740px; height: 26px; cursor: pointer;" align="left" valign="middle" class="header" 
                    colspan="7" id="dragCtrl_BuscarTalonarios">                
                    <span style="font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">
                        <asp:Label ID="lbltitulo" runat="server" text="Búsqueda de Documentos" />
                    </span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                    <asp:ImageButton ID="btnCerrar_BuscarTalonarios" runat="server" Width="16px" Height="15px"
                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        onclick="btnCerrar_BuscarTalonarios_Click" ToolTip="Cerrar"/>                                        
                </td>
            </tr>
            
            <tr>
                <td style="width: 800px;" colspan="9">
                    <asp:HiddenField ID="hiddenFiltroTalonario" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenFiltroConcepto" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenRealizarConsultaID" runat="server" Value="" />
                    <asp:HiddenField ID="hiddenCodigoPagoID" runat="server" Value="" />
                    <asp:HiddenField ID="hiddenCodigoAlumnoID" runat="server" Value="" />
                    <asp:HiddenField ID="hiddenNumeroPagoID" runat="server" Value="" />
                    <asp:HiddenField ID="hiddenCodigoTalonarioID" runat="server" Value="" />
                <br />
                </td>
            </tr>  
            
            <tr>
                <td style="width: 30px; height: 25px" align="right" valign="middle" rowspan="8">  
                </td>
                <td style="width: 120px; height: 25px;" align="left" valign="middle">
                    <span>Talonario</span>
                </td>
                <td style="width: 520px; height: 25px;" align="left" valign="middle" colspan="5">
                    <asp:DropDownList ID="ddlTalonario" runat="server" style="width: 302px; font-size: 8pt; font-family: Arial;">
                    </asp:DropDownList>         
                    <asp:HiddenField ID="hfTotalRegsGVTodos" runat="server" Value="0" />
                </td>
                <td style="width: 100px; height: 25px;" align="left" valign="middle">
                    <asp:ImageButton ID="btnBuscar_BuscarTalonarios" runat="server" Width="94" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnBuscarV2_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnBuscarV2_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnBuscarV2_1.png'"
                        OnClick="btnBuscar_BuscarTalonarios_Click"/>
                </td>
                <td style="width: 30px; height: 25px" align="right" valign="middle" rowspan="8">  
                </td>
            </tr>    
            
            <tr>
                <td style="width: 120px; height: 25px;" valign="middle" align="left">
                    <span>Apellido Paterno</span>
                </td>
                <td style="width: 520px; height: 25px;" valign="middle" align="left" colspan="5">
                    <asp:TextBox ID="tbApellidoPaterno" runat="server" TabIndex="1" MaxLength="50" Width="300px" CssClass="miTextBox" />
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="left">                
                    <asp:ImageButton ID="btnLimpiar_BuscarTalonarios" runat="server" Width="74" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                        onclick="btnLimpiar_BuscarTalonarios_Click" 
                        ToolTip="Limpiar Filtros" CausesValidation="false"/>                            
                </td> 
            </tr>            
               
            <tr>
                <td style="width: 120px; height: 25px;" valign="middle" align="left">
                    <span>Apellido Materno</span>  
                </td>
                <td style="width: 520px; height: 25px;" valign="middle" align="left" colspan="5">
                    <asp:TextBox ID="tbApellidoMaterno" runat="server" TabIndex="2" MaxLength="50" Width="300px" CssClass="miTextBox" />   
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="left">  
                </td> 
            </tr>               
                
            <tr>
                <td style="width: 120px; height: 25px;" valign="middle" align="left">
                    <span>Nombre</span>
                </td>
                <td style="width: 520px; height: 25px;" valign="middle" align="left" colspan="5">
                    <asp:TextBox ID="tbNombre" runat="server" TabIndex="3" MaxLength="50" Width="300px" CssClass="miTextBox" />
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="left">  
                </td>
            </tr>                  
                   
            <tr>
                <td style="width: 120px; height: 25px;" align="left" valign="middle"> 
                    <span>Tipo de Fecha</span>
                </td>            
                <td style="width: 120px; height: 25px;" align="left" valign="middle"> 
                    <asp:DropDownList ID="ddlFecha" runat="server" style="width: 110px; font-size: 8pt; font-family: Arial;">
                        <asp:ListItem Value="1" Text="Emisión"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Pago"></asp:ListItem>
                    </asp:DropDownList>    
                </td>
                <td style="width: 50px; height: 25px;" align="left" valign="middle"> 
                    <span>Inicio&nbsp;</span>
                </td> 
                <td style="width: 120px; height: 25px;" align="left" valign="middle"> 
                    <asp:TextBox ID="tbFechaInicio" runat="server" style="width: 70px; font-size: 8pt; font-family: Arial;" />   
                    <atk:MaskedEditExtender ID="ME_tbFechaInicio" runat="server" 
                        TargetControlID="tbFechaInicio"
                        UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
                    </atk:MaskedEditExtender>                 
                </td> 
                <td style="width: 50px; height: 25px;" align="left" valign="middle"> 
                    <span>Fin&nbsp;</span>
                </td> 
                <td style="width: 180px; height: 25px;" align="left" valign="middle"> 
                    <asp:TextBox ID="tbFechaFin" runat="server" style="width: 70px; font-size: 8pt; font-family: Arial;" />  
                    <atk:MaskedEditExtender ID="ME_tbFechaFin" runat="server" 
                        TargetControlID="tbFechaFin"
                        UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
                    </atk:MaskedEditExtender>                  
                </td> 
                <td style="width: 100px; height: 25px;" align="left" valign="middle"> 
                </td> 
            </tr>                   
                         
            <tr>
                <td colspan="7">
                    <br />
                </td>
            </tr>
             
            <tr>
                <td colspan="7" style="width: 740px; height: 26px;" align="center" valign="top"> 
                
    <div id="miGridviewMantActualizacion_Ficha" style="width: 740px; height: 26px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 740px; height: 26px; color:White; background-color: #0a0f14; 
            font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_Header">
        <tr>
            <td style="width:  20px; height: 26px;" align="center" valign="middle"><span></span></td>
            <td style="width:  20px; height: 26px;" align="center" valign="middle"><span>Tipo</span></td>
            <td style="width:  30px; height: 26px;" align="center" valign="middle"><span>Serie</span></td>
            <td style="width:  60px; height: 26px;" align="center" valign="middle"><span># Pago</span></td>       
            <td style="width:  60px; height: 26px;" align="center" valign="middle"><span>Codigo</span></td>  
            <td style="width: 280px; height: 26px;" align="center" valign="middle"><span>Nombre Completo</span></td>
            <td style="width:  60px; height: 26px;" align="center" valign="middle"><span>Fec. Emi.</span></td>
            <td style="width:  60px; height: 26px;" align="center" valign="middle"><span>Fec. Pago</span></td>
            <td style="width:  20px; height: 26px;" align="center" valign="middle"><span>Mon</span></td>  
            <td style="width:  60px; height: 26px;" align="center" valign="middle"><span>Total</span></td>         
            <td style="width:  50px; height: 26px;" align="center" valign="middle"><span>Origen</span></td>    
            <td style="width:  20px; height: 26px;" align="center" valign="middle"><span></span></td>                    
        </tr>
    </table>      
    </div>                 
                
                
                </td>
            </tr>   
            
            <tr>
                <td colspan="7" style="width: 740px;" align="center" valign="top"> 
<div class="contenido" style="margin: 0; padding: 0; border: solid 1px #a6a3a3; width:740px; height: 262px; overflow-y: scroll; overflow-x: hidden;">   
                    
    <asp:GridView ID="GVListaTodos" runat="server"
        CssClass="miGridviewBusquedaPersona"
        GridLines="None" 
        Width="720px" 
        AutoGenerateColumns="False" 
        ShowFooter="false" 
        ShowHeader="false"
        AllowPaging="FALSE" 
        AllowSorting="true" 
        PageSize="10" 
        EmptyDataText=" - No se encontraron resultados - "
        EmptyDataRowStyle-ForeColor="#a51515" EmptyDataRowStyle-HorizontalAlign="Center"
        OnRowDataBound="GVListaTodos_RowDataBound" 
        OnRowCommand="GVListaTodos_RowCommand"
        
        OnPageIndexChanging="GVListaTodos_PageIndexChanging" 
        OnSorting="GVListaTodos_Sorting"
        OnRowCreated="GVListaTodos_RowCreated">
    <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
        <Columns>            
            <asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" HeaderStyle-Width="20px" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png"
                        CommandName="Seleccionar" CommandArgument='<%# Bind("CodigoPago") %>' ToolTip="Seleccionar Persona" />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tipo" ItemStyle-CssClass="miGridviewBusqueda_Rows" HeaderStyle-Width="20px" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblAbreviaturaTalonario" runat="server" Text='<%# Bind("AbreviaturaTalonario") %>' />
                </ItemTemplate>
            </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Serie" ItemStyle-CssClass="miGridviewBusqueda_Rows" HeaderStyle-Width="30px" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblSerie" runat="server" Text='<%# Bind("Serie") %>' />
                </ItemTemplate>
            </asp:TemplateField> 
             
            <asp:TemplateField HeaderText="N° Pago" ItemStyle-CssClass="miGridviewBusqueda_Rows" HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblNumeroPago" runat="server" Text='<%# Bind("NumeroDocumento") %>' />
                </ItemTemplate>
            </asp:TemplateField>
               
            <asp:TemplateField HeaderText="Código" ItemStyle-CssClass="miGridviewBusqueda_Rows" HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                </ItemTemplate>
            </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Nombre Completo">
                <HeaderTemplate>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr><td style="width: 170px;" align="right" valign="middle">Nombre Completo&nbsp;</td>
        <td style="width: 110px;" align="left" valign="middle">
            <asp:ImageButton ID="btnSorting_NombreCompletoAlumno" runat="server" ToolTip="Descendente"
                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png" CommandName="Sort" CommandArgument="NombreCompletoAlumno" />
        </td>
    </tr>
    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblNombreCompletoAlumno" runat="server" Text='<%# Bind("NombreCompletoAlumno") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="280px" />
                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="left" Width="280px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Fec. Emi." ItemStyle-Width="60px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblFechaVencimiento" runat="server" Text='<%# Bind("FechaEmision") %>' />
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:TemplateField HeaderText="Fec. Pago" ItemStyle-Width="60px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblFechaPago" runat="server" Text='<%# Bind("FechaPago") %>' />
                </ItemTemplate>
            </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Mon" ItemStyle-Width="20px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblMoneda" runat="server" Text='<%# Bind("SimboloMoneda") %>' />
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:TemplateField HeaderText="Total" ItemStyle-Width="60px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>' />
                </ItemTemplate>
            </asp:TemplateField>  
            
            <asp:TemplateField HeaderText="Origen" ItemStyle-Width="50px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblOrigen" runat="server" Text='<%# Bind("Origen") %>' />
                </ItemTemplate>
            </asp:TemplateField>     
            
            <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                <ItemTemplate>
                    <asp:Label ID="lbCodigoTalonario" runat="server" Text='<%# Bind("CodigoTalonario") %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>          
        <PagerTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 735px;">
                <tr>
                    <td style="height: 20px; width: 235px;" align="left" valign="middle">
                        <span class="miFooterMantLabelLeft">Ir a página</span>
                        <asp:DropDownList ID="ddlPageSelector" runat="server" CssClass="letranormal" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;de&nbsp;
                        <asp:Label ID="lblNumPaginas" runat="server" />
                    </td>
                    <td style="height: 20px; width: 300px;" align="center" valign="middle">
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
                </td>
            </tr>  
            
            <tr>
                <td colspan="7">
                    <br />
                </td>
            </tr>   
                   
        </table>  
        <div id="controlBuscarTalonarios" style="display:none">
            <input type="button" id="btnVer_BuscarTalonarios" runat="server" />
            <input type="button" id="btnOK_BuscarTalonarios" />
            <input type="button" id="btnCancel_BuscarTalonarios" />
        </div>       
    </asp:panel> 
    </ContentTemplate>        
    </asp:UpdatePanel>  