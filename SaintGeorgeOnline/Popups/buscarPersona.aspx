<%@ Page Language="VB" AutoEventWireup="false" CodeFile="buscarPersona.aspx.vb" Inherits="Popups_buscarPersona" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Busqueda de Personas</title>    
    
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

    #preview{
	    position:absolute;
	    border:1px solid #ccc;
	    background:#333;
	    padding:5px;
	    display:none;
	    color:#fff;
	}	
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
        color: #a51515;
        margin-right: 7px;
        padding: 0;   
    }   
	
</style>    

<script type="text/javascript">

    $(document).ready(function() {

        SexyLightbox.initialize({ color: 'black', dir: '../App_Themes/ImagenesSLB' });
       
    });

    function pageLoad(sender, args) {  
     
        if (args.get_isPartialLoad()){
            imagePreview();
            SexyLightbox.initialize({ color: 'black', dir: '../App_Themes/ImagenesSLB' });
        }
        
    }

    function cerrar() {
    
        window.close();

    }	
    
</script>  
    
</head>
<body>
    <form id="form1" runat="server">
 
    <atk:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </atk:ToolkitScriptManager>
    
    <asp:UpdatePanel ID="updPanel_BusquedaPersona" runat="server">
        <ContentTemplate>
        
        <div id="miModalWindow">        
    
        <table width="740px" border="0" cellpadding="0" cellspacing="0" style="border: solid 1px black" id="MiTablaParam">            
            <tr>                
                <td style="width: 640px; height: 25px;" colspan="2" valign="middle" align="left" class="TitlebarLeft">
                    <span>Busqueda de Personas</span>
                </td>
                <td style="width: 100px; height: 25px;" valign="middle" align="right" class="TitlebarLeft">
                    <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        Width="16" Height="15" CssClass="TitlebarLeft_Button" />&nbsp;
                </td>
            </tr>  

            <tr><td colspan="3" height="11px"></td></tr>
                                
            <tr>
                <td style="width: 170px; height: 25px;" valign="middle" align="left">
                    <span>Tipo Persona</span>
                    <asp:HiddenField ID="hiddenTipoPersona" runat="server" />
                    <asp:HiddenField ID="hiddenPadre" runat="server" />
                    <asp:HiddenField ID="hfTotalRegsGVTodos" runat="server" Value="0" />
                </td>
                <td style="width: 470px; height: 25px;" valign="middle" align="left">
                    <asp:DropDownList ID="ddlBuscarTipoPersona" runat="server" 
                        AutoPostBack="true" Width="305px"
                        OnSelectedIndexChanged="ddlBuscarTipoPersona_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px; height: 25px; padding-top: 6px;" valign="top" align="left">
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
                <td style="width: 470px; height: 25px;" valign="middle" align="left">
                    <asp:TextBox ID="tbApellidoPaterno" runat="server" TabIndex="1" MaxLength="50" Width="300" CssClass="miTextBox" />
                </td>
                <td style="width: 100px; height: 25px; padding-top: 6px;" valign="top" align="left">                
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
                <td style="width: 470px; height: 25px;" valign="middle" align="left">
                    <asp:TextBox ID="tbApellidoMaterno" runat="server" TabIndex="2" MaxLength="50" Width="300" CssClass="miTextBox" />
                </td>
                <td style="width: 100px; height: 25px; padding-top: 6px;" valign="top" align="left" rowspan="3">                    
                    <asp:ImageButton ID="btnRegistrar" runat="server" Width="94" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnRegistrarV2_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnRegistrarV2_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnRegistrarV2_1.png'"
                        OnClick="btnRegistrar_Click" Visible="false"/>      
                </td>                
            </tr>
            
            <tr>
                <td style="width: 170px; height: 25px;" valign="middle" align="left">
                    <span>Nombre</span>
                </td>
                <td style="width: 470px; height: 25px;" valign="middle" align="left">
                    <asp:TextBox ID="tbNombre" runat="server" TabIndex="3" MaxLength="50" Width="300" CssClass="miTextBox" />
                </td>
            </tr>
            
            <tr>
                <td style="width: 170px; height: 25px;" valign="middle" align="left">
                    <span>Sede</span>
                </td>
                <td style="width: 470px; height: 25px;" valign="middle" align="left">
                     <asp:DropDownList ID="ddlBuscarSede" runat="server" Width="305">
                     </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td style="width: 740px;" valign="top" align="left" colspan="3">                        
<atk:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"
    TargetControlID="pnlParametrosAlumno"
    CollapsedSize="0"
    Collapsed="True"
    ExpandControlID="LinkPanelParametrosAlumno"
    CollapseControlID="LinkPanelParametrosAlumno"
    AutoCollapse="False"
    AutoExpand="False"
    ScrollContents="false"
    ExpandDirection="Vertical" />                
<fieldset id="FSParametrosAlumno" runat="server" visible="false" style="width: 700px;">                                
    <legend id="LinkPanelParametrosAlumno" runat="server" style="cursor: pointer;">
        <a onclick="return false;">Filtro Alumnos</a> 
    </legend>   
    <asp:Panel ID="pnlParametrosAlumno" runat="server">
<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 645px;">
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Nivel</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle" colspan="2">
                                <asp:DropDownList ID="ddlAlumnoNiveles" runat="server" AutoPostBack="true" Width="305" 
                                OnSelectedIndexChanged="ddlAlumnoNiveles_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>            
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>SubNivel</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle" colspan="2">
                                <asp:DropDownList ID="ddlAlumnoSubniveles" runat="server" AutoPostBack="true" Width="305"
                                OnSelectedIndexChanged="ddlAlumnoSubniveles_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>            
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Grado</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle" colspan="2">
                                <asp:DropDownList ID="ddlAlumnoGrados" runat="server" AutoPostBack="true" Width="305"
                                OnSelectedIndexChanged="ddlAlumnoGrados_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>            
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Aula</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                <asp:DropDownList ID="ddlAlumnoAulas" runat="server" Width="305">
                                </asp:DropDownList>
                            </td>
                        </tr>
</table>                                
    </asp:Panel>    
</fieldset>                 
                </td>
            </tr>
            
            <tr>
                <td style="width: 740px;" valign="top" align="left" colspan="3">   
<atk:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="Server"
    TargetControlID="pnlParametrosFamiliar"
    CollapsedSize="0"
    Collapsed="True"
    ExpandControlID="LinkPanelParametrosFamiliar"
    CollapseControlID="LinkPanelParametrosFamiliar"
    AutoCollapse="False"
    AutoExpand="False"
    ScrollContents="false"
    ExpandDirection="Vertical" />                                                 
<fieldset id="FSParametrosFamiliar" runat="server" visible="false" style="width: 700px;">
    <legend id="LinkPanelParametrosFamiliar" runat="server" style="cursor: pointer;">
        <a onclick="return false;">Filtro Alumnos del Familiar</a> 
    </legend>
    <asp:Panel ID="pnlParametrosFamiliar" runat="server">   
<table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 645px;">                    
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Apellido Paterno</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                <asp:TextBox ID="tbFamiliarAlumnoApellidoPaterno" runat="server" MaxLength="50" Width="300" CssClass="miTextBox" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Apellido Materno</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                <asp:TextBox ID="tbFamiliarAlumnoApellidoMaterno" runat="server" MaxLength="50" Width="300" CssClass="miTextBox" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Nombre</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                <asp:TextBox ID="tbFamiliarAlumnoNombre" runat="server" MaxLength="20" Width="300" CssClass="miTextBox" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Nivel</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                <asp:DropDownList ID="ddlFamiliarAlumnoNiveles" runat="server" AutoPostBack="true" Width="305" 
                                OnSelectedIndexChanged="ddlFamiliarAlumnoNiveles_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>  
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>SubNivel</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                <asp:DropDownList ID="ddlFamiliarAlumnoSubniveles" runat="server" AutoPostBack="true" Width="305"
                                OnSelectedIndexChanged="ddlFamiliarAlumnoSubniveles_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>  
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Grado</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                <asp:DropDownList ID="ddlFamiliarAlumnoGrados" runat="server" AutoPostBack="true" Width="305"
                                OnSelectedIndexChanged="ddlFamiliarAlumnoGrados_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>   
                        <tr>
                            <td style="width: 145px; height: 25px;" align="left" valign="middle">
                                <span>Aula</span>
                            </td>
                            <td style="width: 500px; height: 25px;" align="left" valign="middle">
                                <asp:DropDownList ID="ddlFamiliarAlumnoAulas" runat="server" Width="305">
                                </asp:DropDownList>
                            </td>
                        </tr>                        
                    </table>                 
    </asp:Panel>     
</fieldset>             
                </td>
            </tr>     
            <tr>
                <td style="width: 740px; height:10px;" valign="top" align="left" colspan="3">       
                </td>
            </tr>    
        </table>     
           
        <table width="740px" border="0" cellpadding="0" cellspacing="0" style="border: solid 1px black">                    
            <tr>
                <td style="width: 740px; margin: auto;" align="left" valign="top" colspan="3">
                
                    <div class="miGVLista">
                    
                        <asp:GridView ID="GVListaTodos" runat="server"                 
                            Width="740" 
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
                                            CommandName="Seleccionar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Seleccionar Persona" />
                                            
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <a class="preview" id="btnLinkVerFoto" runat="server">
                                            <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" />
                                        </a>
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Nombre Completo">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:250px;" align="right" valign="middle">Nombre Completo&nbsp;</td>
                                            <td style="width:220px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" 
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
                                    <HeaderStyle HorizontalAlign="Center" Width="570px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="left" Width="570px" />
                                </asp:TemplateField>
                                
                                
                                <asp:BoundField DataField="Edad" HeaderText="Edad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" />
                                <asp:BoundField DataField="DescTipoPaciente" HeaderText="Tipo Persona" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                                
                                 <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoPersona" runat="server" Text='<%# Bind("CodigoPersona") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoTipoPaciente" runat="server" Text='<%# Bind("CodigoTipoPaciente") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbRutaFoto" runat="server" Text='<%# Bind("RutaFoto") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbNSnGS" runat="server" Text='<%# Bind("NSnGS") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoGrado" runat="server" Text='<%# Bind("CodigoGrado") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns> 
                            
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 740px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 240px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a página</span>                                     
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                CssClass="letranormal" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;de&nbsp;
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
                    
                        <asp:GridView ID="GVListaAlumnos" runat="server"                 
                            Width="740" 
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
                            OnRowDataBound="GVListaTodos_RowDataBound"
                            OnRowCommand="GVListaAlumnos_RowCommand"
                            OnPageIndexChanging="GVListaTodos_PageIndexChanging"                             
                            OnSorting="GVListaTodos_Sorting" 
                            OnRowCreated="GVListaTodos_RowCreated">
                            
                            <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                            
                            <Columns>
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                            CommandName="Seleccionar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Seleccionar Persona" />
                                            
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <a class="preview" id="btnLinkVerFoto" runat="server" >
                                            <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Codigo Alumno" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" ItemStyle-CssClass="miGridviewBusqueda_Rows" >
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoAlumno" runat="server" Text='<%# Bind("Codigo") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Nombre Completo">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:250px;" align="right" valign="middle">Nombre Completo&nbsp;</td>
                                            <td style="width:220px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" 
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
                                    <HeaderStyle HorizontalAlign="Center" Width="470px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="left" Width="470px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Edad" HeaderText="Edad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" />
                                <asp:BoundField DataField="DescTipoPaciente" HeaderText="Tipo Persona" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" ItemStyle-CssClass="miGridviewBusqueda_Rows" />
                                <asp:BoundField DataField="NSnGS" HeaderText="Nivel / SubNivel / Grado / Aula" ItemStyle-HorizontalAlign="left" ItemStyle-Width="280" ItemStyle-CssClass="miGridviewBusqueda_Rows" />      
                                
                                 <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoPersona" runat="server" Text='<%# Bind("CodigoPersona") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoTipoPaciente" runat="server" Text='<%# Bind("CodigoTipoPaciente") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoTipoSangre" runat="server" Text='<%# Bind("CodigoTipoSangre") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbDescTipoSangre" runat="server" Text='<%# Bind("DescTipoSangre") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbRutaFoto" runat="server" Text='<%# Bind("RutaFoto") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoGrado" runat="server" Text='<%# Bind("CodigoGrado") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>   
                            
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 740px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 240px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a página</span>                                     
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                CssClass="letranormal" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;de&nbsp;
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
                        
                        <asp:GridView ID="GVListaPersonal" runat="server"                 
                            Width="740" 
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
                            OnRowDataBound="GVListaTodos_RowDataBound"
                            OnRowCommand="GVListaPersonal_RowCommand"
                            OnPageIndexChanging="GVListaTodos_PageIndexChanging"                             
                            OnSorting="GVListaTodos_Sorting" 
                            OnRowCreated="GVListaTodos_RowCreated">
                            <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                            
                            <Columns>
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                            CommandName="Seleccionar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Seleccionar Persona" />
                                            
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <a class="preview" id="btnLinkVerFoto" runat="server" >
                                            <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Nombre Completo">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:250px;" align="right" valign="middle">Nombre Completo&nbsp;</td>
                                            <td style="width:220px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" 
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
                                    <HeaderStyle HorizontalAlign="Center" Width="570px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="left" Width="570px" />
                                </asp:TemplateField>
                                
                                
                                <asp:BoundField DataField="Edad" HeaderText="Edad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" />
                                <asp:BoundField DataField="DescTipoPaciente" HeaderText="Tipo Persona" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                                
                                 <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoPersona" runat="server" Text='<%# Bind("CodigoPersona") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoTipoPaciente" runat="server" Text='<%# Bind("CodigoTipoPaciente") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbRutaFoto" runat="server" Text='<%# Bind("RutaFoto") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>  
                            
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 700px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 240px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a página</span>                                     
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                CssClass="letranormal" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;de&nbsp;
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
                        
                        <asp:GridView ID="GVListaFamiliar" runat="server"                 
                            Width="740" 
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
                            OnRowDataBound="GVListaTodos_RowDataBound"
                            OnRowCommand="GVListaFamiliar_RowCommand"
                            OnPageIndexChanging="GVListaTodos_PageIndexChanging"                             
                            OnSorting="GVListaTodos_Sorting" 
                            OnRowCreated="GVListaTodos_RowCreated">
                            <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                           
                            <Columns>
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                            CommandName="Seleccionar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Seleccionar Persona" />
                                            
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <a class="preview" id="btnLinkVerFoto" runat="server" >
                                            <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Nombre Completo">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:250px;" align="right" valign="middle">Nombre Completo&nbsp;</td>
                                            <td style="width:220px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" 
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
                                    <HeaderStyle HorizontalAlign="Center" Width="570px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="left" Width="570px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Edad" HeaderText="Edad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" />
                                <asp:BoundField DataField="DescTipoPaciente" HeaderText="Tipo Persona" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                                
                                 <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoPersona" runat="server" Text='<%# Bind("CodigoPersona") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoTipoPaciente" runat="server" Text='<%# Bind("CodigoTipoPaciente") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbRutaFoto" runat="server" Text='<%# Bind("RutaFoto") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>     
                            
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 740px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 240px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a página</span>                                     
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                CssClass="letranormal" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;de&nbsp;
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
                        
                        <asp:GridView ID="GVListaOtros" runat="server"                 
                            Width="740" 
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
                            OnRowDataBound="GVListaTodos_RowDataBound"
                            OnRowCommand="GVListaOtros_RowCommand"
                            OnPageIndexChanging="GVListaTodos_PageIndexChanging"                             
                            OnSorting="GVListaTodos_Sorting" 
                            OnRowCreated="GVListaTodos_RowCreated">
                            <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />
                            
                            <Columns>
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                            CommandName="Seleccionar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Seleccionar Persona" />
                                            
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <a class="preview" id="btnLinkVerFoto" runat="server" >
                                            <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                
                                
                                
                                <asp:TemplateField HeaderText="Nombre Completo">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:250px;" align="right" valign="middle">Nombre Completo&nbsp;</td>
                                            <td style="width:220px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" 
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
                                    <HeaderStyle HorizontalAlign="Center" Width="570px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="left" Width="570px" />
                                </asp:TemplateField>
                                
                                
                                
                                
                                <asp:BoundField DataField="Edad" HeaderText="Edad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" />
                                <asp:BoundField DataField="DescTipoPaciente" HeaderText="Tipo Persona" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                                
                                 <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoPersona" runat="server" Text='<%# Bind("CodigoPersona") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbCodigoTipoPaciente" runat="server" Text='<%# Bind("CodigoTipoPaciente") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" HeaderStyle-Width="0" ItemStyle-Width="0">
                                    <ItemTemplate>                                    
                                        <asp:Label ID="lbRutaFoto" runat="server" Text='<%# Bind("RutaFoto") %>' />                                                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>  
                            
                            <PagerTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 740px;">
                                    <tr>                                        
                                        <td style="height: 20px; width: 240px;" align="left" valign="middle">
                                            <span class="miFooterMantLabelLeft">Ir a página</span>                                     
                                            <asp:DropDownList ID="ddlPageSelector" runat="server" 
                                                CssClass="letranormal" 
                                                AutoPostBack="true" 
                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;de&nbsp;
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
