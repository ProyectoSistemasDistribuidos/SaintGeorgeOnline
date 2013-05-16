<%@ Page Language="VB" AutoEventWireup="false" CodeFile="buscarTalonario.aspx.vb" Inherits="Popups_buscarTalonario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Busqueda de Documentos</title>    
    
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery-1.4.1.min.js"></script>   
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/sexyalertbox.v1.2.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.sexylightbox.v2.3.js"></script>    
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.colorbox.js"></script>  
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/ToolTipPreview.js"></script>    
    
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/misEstilos.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/sexyalertbox.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/sexylightbox.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/colorbox.css" />
    
    <style type="text/css">

	    body, html{
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            background-color: black         
	    }	 
    	
	    em{
            font-size: 10px;
            font-family: Arial;
            color:Red;
            font-weight:bold; 
            margin-right: 7px;
            padding: 0;   
        }   
    	
    </style>   

</head>


<body>
    <form id="form1" runat="server">
 
    <atk:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </atk:ToolkitScriptManager>
    
    <asp:UpdatePanel ID="updPanel_BusquedaTalonarios" runat="server">
        <ContentTemplate>
        
        <div id="miModalWindow">        
    
        <table width="740px" border="0" cellpadding="0" cellspacing="0" style="border: solid 1px black" id="MiTablaParam">            
            <tr>                
                <td style="width: 640px; height: 25px;" colspan="6" valign="middle" align="left" class="TitlebarLeft">
                    <span>Busqueda de Documentos</span>
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="right" class="TitlebarLeft">
                    <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        Width="16" Height="15" CssClass="TitlebarLeft_Button" />&nbsp;
                </td>
            </tr>  

            <tr><td colspan="7" height="11px"></td></tr>
            
            <tr>
                <td style="width: 170px; height: 25px;" valign="middle" align="left">
                    <span>Talonario</span>
                </td>
                <td style="width: 470px; height: 25px;" valign="middle" align="left" colspan="5">
                    <asp:DropDownList ID="ddlTalonario" runat="server" Width="305">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hiddenCodigoAlumno" runat="server" />
                    <asp:HiddenField ID="hiddenPadre" runat="server" />                    
                    <asp:HiddenField ID="hfTotalRegsGVTodos" runat="server" Value="0" />
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="left">
                    <asp:ImageButton ID="btnBuscar" runat="server" Width="94" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnBuscarV2_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnBuscarV2_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnBuscarV2_1.png'"
                        OnClick="btnBuscar_Click"/>
                </td>
            </tr>            
            
            <tr>
                <td style="width: 170px; height: 25px;" valign="middle" align="left">
                    <span>Apellido Paterno</span>
                </td>
                <td style="width: 470px; height: 25px;" valign="middle" align="left" colspan="5">
                    <asp:TextBox ID="tbApellidoPaterno" runat="server" TabIndex="1" MaxLength="50" Width="300" CssClass="miTextBox" />
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="left">                
                    <asp:ImageButton ID="btnLimpiar" runat="server" Width="74" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                        onclick="btnLimpiar_Click" 
                        ToolTip="Limpiar Filtros" CausesValidation="false"/>                            
                </td> 
            </tr>
            
            <tr>
                <td style="width: 170px; height: 25px;" valign="middle" align="left">
                    <span>Apellido Materno</span>  
                </td>
                <td style="width: 470px; height: 25px;" valign="middle" align="left" colspan="5">
                    <asp:TextBox ID="tbApellidoMaterno" runat="server" TabIndex="2" MaxLength="50" Width="300" CssClass="miTextBox" />   
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="left">  
                </td> 
            </tr>
            
            <tr>
                <td style="width: 170px; height: 25px;" valign="middle" align="left">
                    <span>Nombre</span>
                </td>
                <td style="width: 470px; height: 25px;" valign="middle" align="left" colspan="5">
                    <asp:TextBox ID="tbNombre" runat="server" TabIndex="3" MaxLength="50" Width="300" CssClass="miTextBox" />
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="left">  
                </td>
            </tr>
            
            <tr>
                <td style="width: 170px; height: 25px;" align="left" valign="middle"> 
                    <span>Tipo de Fecha :</span>
                </td>            
                <td style="width: 120px; height: 25px;" align="left" valign="middle"> 
                    <asp:DropDownList ID="ddlFecha" runat="server" style="width: 110px;">
                        <asp:ListItem Value="0" Text="--Seleccione--" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Emisión"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Pago"></asp:ListItem>
                    </asp:DropDownList>    
                </td>
                <td style="width: 60px; height: 25px;" align="left" valign="middle"> 
                    <span>Inicio :</span>
                </td> 
                <td style="width: 100px; height: 25px;" align="left" valign="middle"> 
                    <asp:TextBox ID="tbFechaInicio" runat="server" style="width: 70px;" />   
                    <atk:MaskedEditExtender ID="ME_tbFechaInicio" runat="server" 
                        TargetControlID="tbFechaInicio"
                        UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
                    </atk:MaskedEditExtender>                 
                </td> 
                <td style="width: 60px; height: 25px;" align="left" valign="middle"> 
                    <span>Fin :</span>
                </td> 
                <td style="width: 130px; height: 25px;" align="left" valign="middle"> 
                    <asp:TextBox ID="tbFechaFin" runat="server" style="width: 70px;" />  
                    <atk:MaskedEditExtender ID="ME_tbFechaFin" runat="server" 
                        TargetControlID="tbFechaFin"
                        UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
                    </atk:MaskedEditExtender>                  
                </td> 
                <td style="width: 100px; height: 25px;" align="left" valign="middle"> 
                </td> 
            </tr>
           
            <tr><td style="width: 740px; height:10px;" valign="top" align="left" colspan="7"></td></tr>             
        </table>  
        
        <table width="740px" border="0" cellpadding="0" cellspacing="0" style="border: solid 1px black">                    
            <tr>
                <td style="width: 740px; margin: auto;" align="left" valign="top">       
                
                    <div class="miGVLista" style="border: solid 0px red;">
                    
    <asp:GridView ID="GVListaTodos" runat="server" Width="738" CssClass="miGridviewBusquedaPersona"
        GridLines="None" AutoGenerateColumns="False" ShowFooter="false" ShowHeader="true"
        AllowPaging="True" AllowSorting="true" PageSize="10" EmptyDataText=" - No se encontraron resultados - "
        EmptyDataRowStyle-ForeColor="#a51515" EmptyDataRowStyle-HorizontalAlign="Center"
        OnRowDataBound="GVListaTodos_RowDataBound" 
        OnRowCommand="GVListaTodos_RowCommand"
        OnPageIndexChanging="GVListaTodos_PageIndexChanging" 
        OnSorting="GVListaTodos_Sorting"
        OnRowCreated="GVListaTodos_RowCreated">
    <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
    <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
        <Columns>            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png"
                        CommandName="Seleccionar" CommandArgument='<%# Bind("CodigoPago") %>' ToolTip="Seleccionar Persona" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
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
               
            <asp:TemplateField HeaderText="Código" ItemStyle-CssClass="miGridviewBusqueda_Rows" HeaderStyle-Width="70px" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                </ItemTemplate>
            </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Nombre Completo">
                <HeaderTemplate>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr><td style="width: 150px;" align="right" valign="middle">Nombre Completo&nbsp;</td>
        <td style="width: 100px;" align="left" valign="middle">
            <asp:ImageButton ID="btnSorting_NombreCompletoAlumno" runat="server" ToolTip="Descendente"
                ImageUrl="~/App_Themes/Imagenes/DOWN_A.png" CommandName="Sort" CommandArgument="NombreCompletoAlumno" />
        </td>
    </tr>
    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreCompletoAlumno") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="300px" />
                <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="left" Width="300px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Fec. Emi." ItemStyle-Width="70px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblFechaVencimiento" runat="server" Text='<%# Bind("FechaEmision") %>' />
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:TemplateField HeaderText="Fec. Pago" ItemStyle-Width="70px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblFechaPago" runat="server" Text='<%# Bind("FechaPago") %>' />
                </ItemTemplate>
            </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Mon" ItemStyle-Width="30px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblMoneda" runat="server" Text='<%# Bind("SimboloMoneda") %>' />
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:TemplateField HeaderText="Origen" ItemStyle-Width="48px" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
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
        </table>
        </div>                            
        
        </ContentTemplate>
    </asp:UpdatePanel>           

    </form>
</body>
</html>
