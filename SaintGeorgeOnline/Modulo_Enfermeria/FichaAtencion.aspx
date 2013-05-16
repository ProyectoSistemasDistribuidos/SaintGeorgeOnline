<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false"
    CodeFile="FichaAtencion.aspx.vb" Inherits="Modulo_Enfermeria_FichaAtencion" Title="Página sin título" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<%@ Register src="../Controles/ingresarDiagnostico.ascx" tagname="ingresarDiagnostico" tagprefix="uc1" %>
<%@ Register src="../Controles/ingresarProcedimiento.ascx" tagname="ingresarProcedimiento" tagprefix="uc2" %>
<%@ Register src="../Controles/ingresarMedicamento.ascx" tagname="ingresarMedicamento" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<script type="text/javascript">

    function abrirPopupParams(url, tipo, tbPadre) {

        var urlaux = url + '?tipo=' + tipo + '&Padre=' + tbPadre;
        window.showModalDialog(urlaux, "#1", "dialogHeight: 485px ; dialogWidth: 759px; center: Yes; help: No; resizable: No; status: No; scroll: No");

    }

    function doCheck() {
        var keyCode = (event.which) ? event.which : event.keyCode;
        if ((keyCode == 8) || (keyCode == 46) || (keyCode > 31 && (keyCode < 48 || keyCode > 57)))
            event.returnValue = false;
        return true;
    }

    function MostrarImpresionFichaAtencion_html() {

        window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FichaAtencion_html.aspx', '_blank', '');
    }

    function MostrarImpresionFichaMedica_html() {

        window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FichaMedica_html.aspx', '_blank', '');
    }

    /*
    function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
    return false;

        return true;
    }
    */

</script>

    <style type="text/css">
      
        
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

     <div id="miContainerMantenimiento">
     
    <atk:TabContainer ID="TabContainer1" runat="server" Width="881px" ActiveTabIndex="1"
        AutoPostBack="false" ScrollBars="None" >
        
    <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" 
        Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 650px;">
                
                    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 800px;">
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Tipo de Paciente</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarTipoPaciente" runat="server" AutoPostBack="true" 
                                            Width="250px" OnSelectedIndexChanged="ddlBuscarTipoPaciente_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Todos--</asp:ListItem>
                                            <asp:ListItem Value="1">Alumno</asp:ListItem>
                                            <asp:ListItem Value="2">Personal</asp:ListItem>
                                            <asp:ListItem Value="3">Familiar</asp:ListItem>
                                            <asp:ListItem Value="4">Otros</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                    </td>
                                    <td style="width: 290px; height: 25px;" align="right" valign="top">                                        
                                        <asp:ImageButton ID="btnBuscar" runat="server" Width="74" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                            onclick="btnBuscar_Click" ToolTip="Buscar Registros"/>
                                    </td>        
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Apellido Paterno</span>
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
                                        <asp:ImageButton ID="btnLimpiar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>    
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
                                        
                                        <asp:ImageButton ID="btnVerFichaTemporal" runat="server" Width="176" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnListarFichaTemporal_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnListarFichaTemporal_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnListarFichaTemporal_1.png'"
                                            onclick="btnVerFichaTemporal_Click" ToolTip="Listar Fichas Temporales"/> 
                                        
    <atk:ModalPopupExtender ID="pnModalFichasTemporales" runat="server"
        TargetControlID="VerFichaTemporal"
        PopupControlID="pnlFichasTemporales"
        BackgroundCssClass="MiModalBackground" 
        OkControlID="OKFichaTemporal" 
        CancelControlID="CancelFichaTemporal"
        Drag="true" 
        PopupDragHandleControlID="FichasTemporalesHeader" />           

    <asp:panel id="pnlFichasTemporales" BackColor="White" BorderColor="Black" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" width="540px">    
        <tr>
                <td style="width: 510px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2">                
                    <span id="FichasTemporalesHeader" style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Lista de Fichas Temporales</span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                    <asp:ImageButton ID="btnCerraFichaTemporal" runat="server" Width="16" Height="15"
                        ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                        onclick="btnCerraFichaTemporal_Click" ToolTip="Cerrar Panel"/>
                </td>
            </tr>
            <tr><td colspan="2"><br /></td></tr>
            <tr>
                <td colspan="2" align="center" valign="middle">   
                
                      <div style="border: solid 1px #a6a3a3; width:500px">
            <asp:GridView ID="GVListaFichasTemporales" runat="server"
                            CssClass="miGridviewBusqueda"
                            Width="500"
                            GridLines="None" 
                            AutoGenerateColumns="False"
                            ShowHeader="true"
                            ShowFooter="false"
                            AllowPaging="false" 
                            AllowSorting="false"    
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GVListaFichasTemporales_RowDataBound"
                            OnRowCommand="GVListaFichasTemporales_RowCommand">
                        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                        <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />   
                                                        
                        <Columns>       
                                    
                        <asp:TemplateField>
                            <ItemTemplate>                                    
                                <asp:ImageButton ID="btnSeleccionar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_seleccionar.png" 
                                    CommandName="Seleccionar" CommandArgument='<%# Bind("CodigoFichaAtencion") %>' ToolTip="Seleccionar Ficha de Atención" />
                                                
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                             
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                    CommandName="Eliminar" CommandArgument='<%# Bind("CodigoFichaAtencion") %>' ToolTip="Eliminar Ficha de Atención" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="CodigoFichaAtencion" HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                                
                        <asp:BoundField DataField="DescTipoPaciente" HeaderText="Tipo Paciente" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" ItemStyle-CssClass="miGridviewBusqueda_Rows" />                               
                        <asp:BoundField DataField="FechaHoraAtencion" HeaderText="Fecha Atencion" ItemStyle-HorizontalAlign="left" ItemStyle-Width="130" ItemStyle-CssClass="miGridviewBusqueda_Rows" />    
                 
                        </Columns> 
                        
                        </asp:GridView>   
                    </div>        
                              
                </td>
            </tr> 
            <tr><td colspan="2"><br /></td></tr>              
        </table>  
        <div id="controlFichasTemporales" style="display:none">
            <input type="button" id="VerFichaTemporal" runat="server" />
            <input type="button" id="OKFichaTemporal" />
            <input type="button" id="CancelFichaTemporal" />
        </div>       
    </asp:panel>                                        
                                    
                                    
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
                                
                                <tr>
                                    <td style="width: 800px;" align="left" valign="top" colspan="3">        

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
<fieldset id="FSParametrosAlumno" runat="server" visible="false" style="width: 790px;">      
    <legend id="LinkPanelParametrosAlumno" runat="server" style="cursor: pointer;">
        <a onclick="return false;">Filtro Alumnos</a> 
    </legend>   
    <asp:Panel ID="pnlParametrosAlumno" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width: 760px;">
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Nivel</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlBuscarNivel" runat="server" Width="250px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBuscarNivel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>SubNivel</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlBuscarSubNivel" runat="server" Width="250px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBuscarSubNivel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Grado</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlBuscarGrado" runat="server" Width="250px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBuscarGrado_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Aula</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlBuscarAula" runat="server" Width="250px" >
                    </asp:DropDownList>
                </td>
            </tr>
        </table>          
    </asp:Panel>                                         
</fieldset>         
                                                                
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 800px" align="left" valign="top" colspan="3">   

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
<fieldset id="FSParametrosFamiliar" runat="server" visible="false" style="width: 790px;">
    <legend id="LinkPanelParametrosFamiliar" runat="server" style="cursor: pointer;">
        <a onclick="return false;">Filtro Alumnos del Familiar</a> 
    </legend>   
    <asp:Panel ID="pnlParametrosFamiliar" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 760px;">
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Apellido Paterno</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:TextBox ID="tbBuscarFamiliarApellidoPaterno" runat="server" CssClass="miTextBox"
                        Width="320px" MaxLength="100" Height="18px" />
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Apellido Materno</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:TextBox ID="tbBuscarFamiliarApellidoMaterno" runat="server" CssClass="miTextBox"
                        Width="320px" MaxLength="100" Height="18px" />
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Nombre</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:TextBox ID="tbBuscarFamiliarNombre" runat="server" CssClass="miTextBox" Width="320px"
                        MaxLength="100" Height="18px" />
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Nivel</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlBuscarFamiliarNivel" runat="server" Width="250px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBuscarFamiliarNivel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>SubNivel</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlBuscarFamiliarSubNivel" runat="server" Width="250px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBuscarFamiliarSubNivel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Grado</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlBuscarFamiliarGrado" runat="server" Width="250px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBuscarFamiliarGrado_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 140px; height: 25px;" align="left" valign="middle">
                    <span>Aula</span>
                </td>
                <td style="width: 620px; height: 25px;" align="left" valign="middle">
                    <asp:DropDownList ID="ddlBuscarFamiliarAula" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>                        
</fieldset>  
                                                                      
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Fecha de Atención</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                        <table cellpadding="0" cellspacing="0" border="0" width="360px">
                                        <tr>
                                            <td valign="middle" align="left" style="width: 35px; height: 25px;">
                                                <span>Desde</span>
                                            </td>  
                                            <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                <asp:TextBox ID="tbBuscarFechaAtencionInicial" runat="server" CssClass="miTextBoxCalendar" />
                                                <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                                    TargetControlID="tbBuscarFechaAtencionInicial"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-">
                                                </atk:MaskedEditExtender> 
                                            </td>
                                            <td valign="middle" align="left" style="width: 35px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="imageBF1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                <atk:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                    TargetControlID="tbBuscarFechaAtencionInicial"
                                                    PopupButtonID="imageBF1" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" />
                                            </td>
                                            <td valign="middle" align="left" style="width: 35px; height: 25px;">
                                                <span>Hasta</span>
                                            </td>  
                                            <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                <asp:TextBox ID="tbBuscarFechaAtencionFinal" runat="server" CssClass="miTextBoxCalendar" />
                                                <atk:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                                    TargetControlID="tbBuscarFechaAtencionFinal"
                                                    UserDateFormat="DayMonthYear"                                                                    
                                                    Mask="99/99/9999" 
                                                    MaskType="Date" 
                                                    PromptCharacter="-">
                                                </atk:MaskedEditExtender> 
                                            </td>
                                            <td valign="middle" align="left" style="width: 35px; height: 25px;">
                                                <asp:ImageButton runat="server" ID="imageBF2" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                <atk:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                    TargetControlID="tbBuscarFechaAtencionfinal"
                                                    PopupButtonID="imageBF2" 
                                                    Format="dd/MM/yyyy" 
                                                    CssClass="MyCalendar" />
                                            </td>
                                            
                                        </tr>
                                        </table>
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Sede</span>
                                    </td>
                                    <td style="width: 360px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarSede" runat="server" Width="250px">
                                        </asp:DropDownList>                                       
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                    </td>
                                </tr>  
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Estado de Registro</span>
                                    </td>
                                    <td style="min-width: 360px; height: 25px;" align="left" valign="bottom">
                                        <asp:RadioButtonList ID="rbEstadosRegistros" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="-1" Text="Todos" Selected="True"/>
                                            <asp:ListItem Value="1" Text="Terminada"  />
                                            <asp:ListItem Value="0" Text="Pendiente" />                                        
                                        </asp:RadioButtonList>                                        
                                    </td>
                                    <td style="width: 290px;" align="right" valign="top">
                                    </td>
                                </tr>
                                
                            </table>
                        </fieldset>
                    </div>
                    
                    <div class="miEspacio">
                    </div>      
                                   
                    <div id="misRegistrosEncontrados" style="width: 840px;">
                        <fieldset style="width: 840px;">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 800px;">
                                <tr>
                                    <td style="width: 100px; height: 21px;" align="left" valign="middle">
                                         
                                    </td>
                                    <td style="width: 600px; height: 21px;" align="left" valign="bottom">                                 
                                                                
                                    </td>                                
                                    <td style="width: 100px; height: 21px;" align="right" valign="middle">
                                        <asp:ImageButton ID="btnNuevo" runat="server" Width="74px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnNuevo_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnNuevo_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnNuevo_1.png'" 
                                            onclick="btnNuevo_Click" 
                                            ToolTip="Nuevo Registro"/>
                                     </td>                                                                     
                                </tr>
                            </table>
                           
                        </fieldset>                    
                    </div>   
                                     
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miGridviewMantActualizacion_Ficha">
                        <asp:GridView ID="GVListaFichas" runat="server" 
                            Width="840" 
                            CssClass="miGridviewBusqueda" 
                            GridLines="None" 
                            AutoGenerateColumns="false"
                            AllowPaging="True" 
                            AllowSorting="true"
                            PageSize="10"
                            ShowFooter="false"
                            ShowHeader="true"
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GVListaFichas_RowDataBound"
                            OnRowCommand="GVListaFichas_RowCommand"   
                            OnPageIndexChanging="GVListaFichas_PageIndexChanging" 
                            OnRowCreated="GVListaFichas_RowCreated"
                            OnSorting="GVListaFichas_Sorting" >
                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />    
                                                                                                         
                            <Columns>            
                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                            CommandName="Actualizar" CommandArgument='<%# Bind("CodigoFichaAtencion") %>' ToolTip="Actualizar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                            CommandName="Eliminar" CommandArgument='<%# Bind("CodigoFichaAtencion") %>' ToolTip="Eliminar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnActivar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_activar.png" 
                                            CommandName="Activar" CommandArgument='<%# Bind("CodigoFichaAtencion") %>' ToolTip="Activar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnVisualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_ver.png" 
                                            CommandName="Ver" CommandArgument='<%# Bind("CodigoFichaAtencion") %>' ToolTip="Ver Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_printer.png" 
                                            CommandName="Imprimir" CommandArgument='<%# Bind("CodigoFichaAtencion") %>' ToolTip="Imprimir Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="CodigoFichaAtencion" HeaderText="#" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40" />
                                
                                <asp:TemplateField HeaderText="Nombre Completo">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:130px;" align="right" valign="middle">Nombre Completo&nbsp;</td>
                                            <td style="width:70px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_NombreCompleto" runat="server" 
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
                                    <HeaderStyle HorizontalAlign="Center" Width="200px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="200px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Tipo Paciente">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:115px;" align="right" valign="middle">Tipo Paciente&nbsp;</td>
                                            <td style="width:35px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_DescTipoPaciente" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="DescTipoPaciente"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DescTipoPaciente") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>                                
                                
                                <asp:TemplateField HeaderText="Sede">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:85px;" align="right" valign="middle">Sede&nbsp;</td>
                                            <td style="width:65px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_DescSede" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="DescSede"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("DescSede") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Fecha Atencion">  
                                    <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="width:115px;" align="right" valign="middle">Fecha Atencion&nbsp;</td>
                                            <td style="width:35px;" align="left" valign="middle"><asp:ImageButton ID="btnSorting_FechaHoraAtencionDt" runat="server" 
                                                ToolTip="Descendente"    
                                                ImageUrl="~/App_Themes/Imagenes/DOWN.png"                             
                                                CommandName="Sort" 
                                                CommandArgument="FechaHoraAtencionDt"/></td>
                                        </tr>
                                    </table>                                    
                                    </HeaderTemplate>                                                                      
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("FechaHoraAtencion") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>                                
                                
                                <asp:BoundField DataField="Estado" >
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle"/>
                                    <ItemStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle" />
                                </asp:BoundField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="imgFichaPendiente" runat="server" Width="18px" Height="18px"   ImageUrl="~/App_Themes/Imagenes/AlertIcon.gif"  />                                        
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
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
                    
                    <div id="GVLegenda">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 840px;">
                            <tr>
                            
                                <td style="width: 5px; height: 26px;" align="center" valign="middle">
                                    </td>  
                            
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <img alt="Actualizar Registro" src="../App_Themes/Imagenes/opc_actualizar.png"/></td>                                    
                                <td style="width: 150px; height: 26px;" align="left" valign="middle">
                                    <span>Actualizar Registro</span></td>  
                                     
                                <td style="width: 10px; height: 26px;" align="center" valign="middle">
                                    <span>&#124;</span></td>    
                                     
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_eliminar.png"/></td>
                                <td style="width: 150px; height: 26px;" align="left" valign="middle">
                                    <span>Eliminar Registro</span></td>  
                                    
                                <td style="width: 10px; height: 26px;" align="center" valign="middle">
                                    <span>&#124;</span></td>    
                                                                    
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <img alt="Activar Registro" src="../App_Themes/Imagenes/opc_activar.png"/></td>
                                <td style="width: 150px; height: 26px;" align="left" valign="middle">
                                    <span>Activar Registro</span></td>  
                                                                        
                                <td style="width: 10px; height: 26px;" align="center" valign="middle">
                                    <span>&#124;</span></td>   
                                    
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <img alt="Ver Registro" src="../App_Themes/Imagenes/opc_ver.png"/></td>
                                <td style="width: 120px; height: 26px;" align="left" valign="middle">
                                    <span>Ver Registro</span></td>  
                                                                        
                                 <td style="width: 10px; height: 26px;" align="center" valign="middle">
                                    <span>&#124;</span></td>      
                                
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <img alt="Imprimir Registro" src="../App_Themes/Imagenes/opc_printer.png"/>
                                </td>
                                <td style="width: 150px; height: 26px;" align="left" valign="middle">
                                    <span>Imprimir Registro</span>
                                </td>                                      
                                <td style="width: 10px; height: 26px;" align="center" valign="middle">
                                    <span>&#124;</span>
                                </td>  
                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                    <img alt="Imprimir Registro" width="15px" height="15px" src="../App_Themes/Imagenes/AlertIcon.gif"/>
                                </td>    
                                <td style="width: 150px; height: 26px;" align="left" valign="middle">
                                    <span>Registro Pendiente</span>
                                </td>     
                                  
                                <td style="width: 195px; height: 26px;" align="center" valign="middle">
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

    <div id="miPaginaFicha">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            
                <div id="miContenidoFicha" style="border: solid 0px red">
                
                    <div id="miCabeceraFicha" style="border: solid 0px orange">
                    
                      <table cellpadding="0" cellspacing="0" border="0" width="840px" style="margin: 0;">
                            <tr>
                                <td style="width: 640px;" align="left" valign="middle">
                    
                        <fieldset style="width:640px; margin: 0;" id="Bloque_DatosPersonales" runat="server">                        
                            <legend style="width:400px">Datos del Paciente</legend>
                            <table cellpadding="0" cellspacing="0" border="0" width="640px">
                            
                                <tr>
                                    <td style="background: #FFFFFF url('../App_Themes/Imagenes/img_bg.gif') no-repeat center; text-align:right"
                                        align="center" valign="middle" rowspan="4">
                                        <asp:Image ID="imgFotoPaciente" runat="server" Width="54" Height="64" Style="border: #7f9db9 1px solid"
                                            ImageUrl="~/Fotos/noPhoto.gif" />
                                    </td>
                                    <td style="width: 10px; height: 25px;" align="left" valign="middle" rowspan="4">
                                    </td>
                                    <td style="width: 556px; height: 25px;" align="left" valign="middle">
                                        <span>Nombre Completo&nbsp;:&nbsp;</span>
                                        <asp:HiddenField ID="hidenCodigoFichaAtencion" runat="server" />                                       
                                        <asp:HiddenField ID="hidenCodigoPaciente" runat="server" />                                       
                                        <asp:HiddenField ID="hidenCodigoPersona" runat="server" />   
                                        <asp:HiddenField ID="hidenCodigoTipoPaciente" runat="server" />
                                        <asp:HiddenField ID="hidenCodigoTipoSangre" runat="server" />
                                        <asp:HiddenField ID="hidenCodigoSedePaciente" runat="server" />
                                                                        
                                        <asp:Label ID="lbNombrePaciente" runat="server" CssClass="misLabels" />&nbsp;<asp:Label ID="lbEdadPaciente" runat="server" CssClass="misLabels" />                                    
                                    </td>
                                    
                                </tr>
                                
                                <tr>
                                    <td style="width: 556px; height: 25px;" align="left" valign="middle">
                                        <span>Tipo de Paciente&nbsp;:&nbsp;</span>
                                        <asp:Label ID="lbTipoPaciente" runat="server" CssClass="misLabels"/>  
                                    </td>
                                    
                                </tr>
                                
                                
                                <%--inicio --%>  
                               <tr>
                                    <td style="width: 556px; height: 25px;" align="left" valign="middle">                                        
                                        <span><asp:Label ID="Label5" runat="server" Text = "Cantidad atenciones del  Alumno:"/></span>                                    
                                        <asp:Label ID="lblCantidadAtencionAlumno" runat="server" CssClass="misLabels"/>  
                                    </td>
                                    
                                    
                                </tr>
                                <%--fin--%>
                               <tr>
                                    <td style="width: 556px; height: 25px;" align="left" valign="middle">                                        
                                        <span><asp:Label ID="spanNSnGA" runat="server" Text = "Nivel/Subnivel/Grado/Aula :&nbsp;"/></span>                                    
                                        <asp:Label ID="lbNSnGS" runat="server" CssClass="misLabels"/>  
                                    </td>
                                    
                                </tr>
                                
                               
                                
                                
                                <tr>
                                     <td valign="middle" align="center"  colspan="3">
                                        <asp:ImageButton ID="btnVerDatosRelevantes" runat="server" Width="129" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnVerDatosRelevantes_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnVerDatosRelevantes_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnVerDatosRelevantes_1.png'" 
                                            ToolTip="Ver Datos Relevantes"
                                            onclick="btnVerDatosRelevantes_Click"/>&nbsp;
                                        <asp:ImageButton ID="btnVerSeguro" runat="server" Width="74" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnVerSeguro_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnVerSeguro_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnVerSeguro_1.png'" 
                                            ToolTip="Ver Seguro Médico"
                                            onclick="btnVerDatosSeguro_Click"/>&nbsp;
                                        <asp:ImageButton ID="btnVerContactos" runat="server" Width="89" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnVerContactos_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnVerContactos_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnVerContactos_1.png'" 
                                            ToolTip="Ver Contactos"
                                            onclick="btnVerContactos_Click"/>&nbsp;
                                        <asp:ImageButton ID="btnVerFichaMedica" runat="server" Width="109" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnVerFichaMedica_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnVerFichaMedica_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnVerFichaMedica_1.png'" 
                                            ToolTip="Ver Ficha Médica"
                                            onclick="btnVerFichaMedica_Click"/>                                                          
                                    </td>
                                    
                                </tr>
                                
                                <tr>
                                    <td colspan="3">
                                        
                                    </td>                                        
                                </tr>
                                
                                <tr><td style="height:5px" colspan="3"></td></tr>
                                
                            </table>
                        </fieldset>
                        
                                </td>
                                <td style="width: 200px;" align="center" valign="middle">
                                
                                    <table cellpadding="0" cellspacing="0" border="0" width="200px" style="margin: 0;">
                                        <tr>
                                            <td style="width: 200px;" align="right" valign="middle">
<asp:ImageButton ID="btnFichaBuscar" runat="server" Width="84" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnBuscarPersonaV2_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnBuscarPersonaV2_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnBuscarPersonaV2_1.png'"
                                            ToolTip="Buscar Paciente"
                                            OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','0','paciente');" />                                            
                                            </td>
                                        </tr>
                                        <tr><td style="height:10px;"></td></tr>
                                        <tr>
                                            <td style="width: 200px;" align="right" valign="middle">
 <asp:ImageButton ID="btnGrabar" runat="server" Width="84" Height="19" 
                                            ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarV2_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarV2_1.png'" 
                                            ToolTip="Grabar"
                                            onclick="btnFichaGrabar_click"/>                                            
                                            </td>
                                        </tr>
                                        <tr><td style="height:10px;"></td></tr>
                                        <tr>
                                            <td style="width: 200px;" align="right" valign="middle">
<asp:ImageButton ID="btnFichaCancelar" runat="server" Width="84" Height="19"
                                            ImageUrl="~/App_Themes/Imagenes/btnCancelarV2_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnCancelarV2_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnCancelarV2_1.png'" 
                                            ToolTip="Cancelar"
                                            onclick="btnFichaCancelar_Click" 
                                            CausesValidation="false"/>                                              
                                            </td>
                                        </tr>
                                        <tr><td style="height:10px;"></td></tr>
                                        <tr>
                                            <td style="width: 200px;" align="right" valign="middle">
<asp:ImageButton ID="btnAgregarFichaTemporal" runat="server" Width="171" Height="19"
                                            ImageUrl="~/App_Themes/Imagenes/btnGrabarFichaTemporal_0.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabarFichaTemporal_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabarFichaTemporal_1.png'" 
                                            ToolTip="Agregar a Fichas Temporales"
                                            onclick="btnAgregarFichaTemporal_Click" 
                                            CausesValidation="false"/>                                           
                                                                                     
                                            </td>
                                        </tr>
                                    </table>    
                                                                  
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                    
                    <div class="miEspacio">
                    </div>
                    
                    <div id="miContainerFicha">
                        <atk:TabContainer ID="TabContainer2" runat="server" Width="850px" ActiveTabIndex="1"
                            AutoPostBack="false" ScrollBars="Vertical">
                            
                            <atk:TabPanel ID="miFichaTab1" runat="server" HeaderText="Tab1">
                                <HeaderTemplate>
                                    Datos Iniciales de Atención
                                </HeaderTemplate>
                                <ContentTemplate>
                                
                                <div id="Bloque_DatosIniciales" runat="server" style="border:0; margin:0;">
                                    <fieldset>
                                        <legend>Recepción</legend>
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Sede&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <asp:DropDownList ID="ddlSede" runat="server" Width="200px" OnSelectedIndexChanged="ddlSede_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerSede" runat="server" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Fecha de Atención&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="390px">
                                                        <tr>
                                                            <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                <asp:TextBox ID="tbFechaAtencion" runat="server" CssClass="miTextBoxCalendar" />
                                                                <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                                                    TargetControlID="tbFechaAtencion"
                                                                    UserDateFormat="DayMonthYear"                                                                    
                                                                    Mask="99/99/9999" 
                                                                    MaskType="Date" 
                                                                    PromptCharacter="-">
                                                                </atk:MaskedEditExtender>  
                                                                <asp:Label ID="lblVerFechaAtencion" runat="server" />                                                                                                                            
                                                            </td>
                                                            <td valign="middle" align="left" style="width: 280px; height: 25px;">
                                                                <asp:ImageButton runat="server" ID="Image1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"  AlternateText="Elija una fecha del calendario" />
                                                                <atk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbFechaAtencion"
                                                                    PopupButtonID="Image1" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />       
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Hora de Ingreso&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">                                                    
                                                    <asp:Image ID="imgTimepicker1" runat="server" ImageUrl="~/App_Themes/Imagenes/ima_timepicker.gif" />                                                                                                                                        
                                                    <MKB:TimeSelector ID="TimeSelector1" runat="server"
                                                        SelectedTimeFormat="TwentyFour" MinuteIncrement="1" DisplaySeconds="false">                                                        
                                                    </MKB:TimeSelector>
                                                    <asp:Label ID="lblVerHoraIngreso" runat="server" />     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span><asp:Label ID="spanResponsable" runat="server" Text="Persona que acompaña ó envía" /></span>
                                                </td>
                                                <td style="width: 385px; height: 25px;" valign="middle" align="left">
                                                    <asp:TextBox ID="tbResponsable" runat="server" CssClass="miTextBox" Width="370px" Enabled="false" />
                                                    <asp:HiddenField ID="hidenCodigoPersonalEnvia" runat="server" />  
                                                    <asp:HiddenField ID="hidenCodigoPersonaEnvia" runat="server" />
                                                    <asp:HiddenField ID="hidenCodigoTipoPersonaEnvia" runat="server" /> 
                                                    <asp:Label ID="lblVerResponsable" runat="server" /> 
                                                </td>
                                                <td style="width: 235px; height: 25px;" valign="middle" align="left">
                                                    <asp:ImageButton ID="btnBuscarResponsable" runat="server" Width="74px" Height="19px"
                                                        ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png"                                                        
                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                        Tooltip="Buscar Responsable"
                                                        OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','2','envia');" />
                                                </td>
                                            </tr>
                                            <tr>
                                              <td style="width: 170px; height: 25px" align="left">
                                                    <span><asp:Label ID="spanTipoProcAtencion" runat="server" Text="Tipo de Procedencia" /></span>
                                                </td>
                                                <td colspan =2 style="width: 790px; height: 25px" align="left">
                                                  
                                                    <asp:RadioButtonList Width="300px" ID="rbTipoProcAtencion" runat="server" 
                                                    RepeatDirection="horizontal" OnSelectedIndexChanged= "rbTipoProcAtencion_SelectedIndexChanged" 
                                                    AutoPostBack ="True">
                                                        <asp:ListItem Value="1" Text="Curso" Selected="True"/>
                                                        <asp:ListItem Value="2" Text="Taller"      />
                                                        <asp:ListItem Value="3" Text="Recreo"/>
                                                        <asp:ListItem Value="4" Text="Otros"/> 
                                                    </asp:RadioButtonList> 
                                                    <asp:Label ID="lblVerTipoProcAtencion" runat="server" />     
                                                </td>
                                            </tr> 
                                            <tr>
                                              <td style="width: 170px; height: 25px" align="left">
                                                    <span><asp:Label ID="spanProcAtencion" runat="server" Text="Procedencia" /><span class="camposObligatorios">(*)</span></span>
                                                </td>
                                                <td colspan =2 style="width: 790px; height: 25px" align="left">
                                                  <asp:Panel runat ="server" ID ="pnlTipoProcAtencionCurso">
                                                     <asp:DropDownList ID="ddlProcCurso" runat="server" Width="300px" >
                                                    </asp:DropDownList>    
                                                  </asp:Panel>
                                                    <asp:Panel runat ="server" ID ="pnlTipoProcAtencionTaller">
                                                    <asp:DropDownList ID="ddlProcTaller" runat="server" Width="300px" >
                                                    </asp:DropDownList>   
                                                  </asp:Panel>
                                                    <asp:Panel runat ="server" ID ="pnlTipoProcAtencionOtro">
                                                    <asp:TextBox ID="tbProcOtro" runat="server" CssClass="miTextBox" Width="300px"  />      
                                                  </asp:Panel>
                                                  <asp:Label ID="lblVerNombreprocedencia" runat="server" /> 
                                                 </td>
                                            </tr> 
                                            
                                             <%-- <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span><asp:Label ID="lblCodigoCurso" runat="server" Text="Curso" /></span>
                                                </td>
                                                <td style="width: 385px; height: 25px;" valign="middle" align="left">
                                                    <asp:DropDownList id="ddlCodigoCurso" runat ="server" >
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerCodigoCurso" runat="server" /> 
                                                </td>
                                                <td style="width: 235px; height: 25px;" valign="middle" align="left">
                                                </td>
                                            </tr>
                                            
                                              <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span><asp:Label ID="lblCodigoTaller" runat="server" Text="Taller" /></span>
                                                </td>
                                                <td style="width: 385px; height: 25px;" valign="middle" align="left">
                                                    <asp:DropDownList id="ddlCodigoTaller" runat ="server" >
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerCodigoTaller" runat="server" /> 
                                                </td>
                                                <td style="width: 235px; height: 25px;" valign="middle" align="left">
                                                </td>
                                            </tr>
                                            
                                              <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span><asp:Label ID="lblOtro" runat="server" Text="Otro" /></span>
                                                </td>
                                                <td style="width: 385px; height: 25px;" valign="middle" align="left">
                                                    <asp:DropDownList id="ddlOtro" runat ="server" >
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerCodigoOtro" runat="server" /> 
                                                </td>
                                                <td style="width: 235px; height: 25px;" valign="middle" align="left">
                                                </td>
                                            </tr>--%>
                                            
                                        </table>
                                    </fieldset>
                                </div>
                                    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                            <atk:TabPanel ID="miFichaTab2" runat="server" HeaderText="Tab2">
                                <HeaderTemplate>
                                    Detalle de Atención
                                </HeaderTemplate>
                                <ContentTemplate>
                                
                                <div id="Bloque_DatosDetalle" runat="server" style="border:0; margin:0;">                                
                                    <fieldset>
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 80px" align="left" valign="middle">
                                                    <span>Sintomas&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbSintomas" 
                                                        ValidChars="' ','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'" 
                                                        Enabled="True">
                                                    </atk:FilteredTextBoxExtender>
                                                </td>
                                                <td style="width: 620px; height: 80px" align="left" valign="middle">
                                                    <asp:TextBox ID="tbSintomas" runat="server" CssClass="miTextBoxMultiLine" Width="460px"
                                                        Height="75px" Rows="6" TextMode="MultiLine" />
                                                    <asp:Label ID="lblVerSintomas" runat="server" />     
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 50px" align="left" valign="middle">
                                                    <span>¿Descanso en enfermería?&nbsp;</span><span class="camposObligatorios">(*)</span>                                                    
                                                </td>
                                                <td style="width: 620px; height: 50px" align="left">
                                                    <asp:RadioButtonList ID="rbControl" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Si" />
                                                        <asp:ListItem Value="0" Text="No" Selected="True" />                                        
                                                    </asp:RadioButtonList>   
                                                    <asp:Label ID="lblVerControl" runat="server" />  
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 80px" align="left" valign="middle">
                                                    <span>Observaciones&nbsp;</span>
                                                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="tbObservaciones" 
                                                        ValidChars="' ','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'" 
                                                        Enabled="True">
                                                    </atk:FilteredTextBoxExtender>
                                                </td>
                                                <td style="width: 620px; height: 80px" align="left" valign="middle">
                                                    <asp:TextBox ID="tbObservaciones" runat="server" CssClass="miTextBoxMultiLine" Width="460px" 
                                                        Height="75px" Rows="6" TextMode="MultiLine" />
                                                    <asp:Label ID="lblVerObservaciones" runat="server" />         
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 170px; height: 80px" align="left" valign="middle">
                                                    <span>Categoría&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 80px" align="left" valign="middle">
                                                    <asp:DropDownList ID="ddlCategoria" runat="server" Width="200px" >
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerCategoria" runat="server" />         
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 80px" align="left" valign="middle">
                                                    <span>Tipo atencion </span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 80px" align="left" valign="middle">
                                                    <asp:DropDownList ID="cmbTipoAtencion" runat="server" Width="200px" >
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblTipoAtencion" runat="server" />         
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                    
                                    <div class="miEspacio">
                                    </div>
                                    
                                    <fieldset>
                                        <legend>Diagnósticos de Enfermería&nbsp;<span class="camposObligatorios">(*)</span></legend>                                        
                                        <table cellpadding="0" cellspacing="0" border="0" width="590px">  
                                            <tr><td colspan="2" height="10px">
    <atk:ModalPopupExtender ID="pnModalDiagnostico" runat="server"
        TargetControlID="btnAgregarDetalleDiagnostico"
        PopupControlID="pnlDiagnostico"
        BackgroundCssClass="MiModalBackground" 
        OkControlID="OKDiagnostico" 
        CancelControlID="CancelDiagnostico"
        Drag="True" 
        PopupDragHandleControlID="DiagnosticoHeader" DynamicServicePath="" Enabled="True" />   
    <asp:panel id="pnlDiagnostico" BackColor="White" BorderColor="Black" BorderWidth="1px" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" width="460px">    
            <tr>
                <td style="width: 460px; height: 26px; cursor: pointer;" colspan="3" align="center" class="miGVBusquedaFicha_Header" id="DiagnosticoHeader">
                    <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial;">Agregar Diagnóstico de Enfermería</span>
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    <span style="padding-left:10px">Diagnóstico&nbsp;</span>
                </td>
                <td style="width: 260px; height: 25px" align="left">
                    <asp:DropDownList ID="ddlDiagnostico" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    <asp:ImageButton ID="btnAgregarRegistroDiagnostico" runat="server" Width="84px" Height="19px" 
                        ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
                        onclick="btnAgregarRegistroDiagnostico_Click" 
                        ToolTip="Agregar Nuevo Diagnóstico"/>
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td style="width: 460px; height: 25px" align="center" valign="middle" colspan="3">
                    <asp:ImageButton ID="btnModalAceptarDiagnostico" runat="server" Width="84px" Height="19px"
                        ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                        OnClick="btnModalAceptarDiagnostico_Click"
                        ToolTip="Aceptar" />&nbsp;
                    <asp:ImageButton ID="btnModalCancelarDiagnostico" runat="server" Width="84px" Height="19px"
                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                        OnClick="btnModalCancelarDiagnostico_Click"
                        ToolTip="Cancelar" />
                </td>
            </tr>      
            <tr><td colspan="3"><br /></td></tr>           
        </table>
        <div id="controlDiagnostico" style="display:none">
            <input type="button" id="OKDiagnostico" />
            <input type="button" id="CancelDiagnostico" />
        </div>
       
    </asp:panel>
                                            </td></tr>
                                            <tr>
                                                <td style="width: 590px;" align="left" valign="top" colspan="2">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="300px">
                                                        <tr>                                                            
                                                            <td style="width: 270px; height: 26px; text-align:center; color:White; font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                Diagnóstico de Enfermería                                                                 
                                                            </td>
                                                            <td style="width: 30px; height: 26px;" align="right" class="miGVBusquedaFicha_Header">
                                                                <asp:ImageButton ID="btnAgregarDetalleDiagnostico" runat="server" Width="24px" Height="24px"
                                                                    ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                                                                    OnClick="btnAgregarDetalleDiagnostico_Click"                                                    
                                                                    ToolTip="Agregar"/>                                                                      
                                                            </td>
                                                        </tr> 
                                                          
                                                        <tr>
                                                            <td style="width: 300px; height: 25px" align="center" valign="top" colspan="2">
                                                            
                                                            <asp:UpdatePanel ID="upDiagnostico" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>                                        
                                                                <div id="miGVMantFichaRegitros" style="width: 300px;">
                                                                <asp:GridView ID="GVListaDiagnosticos" runat="server" 
                                                                    CssClass="miGVBusquedaFicha"
                                                                    Width="300px"
                                                                    GridLines="None" 
                                                                    AutoGenerateColumns="False"
                                                                    ShowHeader="false"
                                                                    ShowFooter="false"
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false"    
                                                                    OnRowDataBound="GVListaDiagnosticos_RowDataBound"
                                                                    OnRowCommand="GVListaDiagnosticos_RowCommand">                           
                                                                    
                                                                    <Columns>         
                                                                        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>    
                                                                        
                                                                        <asp:TemplateField HeaderText="Codigo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                        </asp:TemplateField>   
                                                                                                 
                                                                        <asp:TemplateField HeaderText="Descripción">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="270px"/>
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="270px" />
                                                                        </asp:TemplateField> 
                                                                                                       
                                                                    </Columns>
                                                                </asp:GridView>                
                                                                </div>
                                                                <div class="miEspacio"></div>                                            
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            
                                                            </td>                                                        
                                                        </tr>
                                                         
                                                    </table>  
                                                </td>
                                            </tr>  
                                            
                                        </table>   
                                                                                                       
                                    </fieldset>
                                    
                                    <div class="miEspacio">
                                    </div>
                                    
                                    <fieldset>
                                        <legend>Procedimiento de Enfermería</legend>                                        
                                        <table cellpadding="0" cellspacing="0" border="0" width="590px"> 
                                            <tr><td colspan="2" height="10px">
    <atk:ModalPopupExtender ID="pnModalProcedimiento" runat="server"
        TargetControlID="btnAgregarDetalleProcedimiento"
        PopupControlID="pnlProcedimiento"
        BackgroundCssClass="MiModalBackground" 
        OkControlID="OKProcedimiento" 
        CancelControlID="CancelProcedimiento"
        Drag="True" 
        PopupDragHandleControlID="ProcedimientoHeader" DynamicServicePath="" Enabled="True" />  
    <asp:panel id="pnlProcedimiento" BackColor="White" BorderColor="Black" BorderWidth="1px" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" width="460px">    
            <tr>
                <td style="width: 460px; height: 26px; cursor: pointer;" colspan="3" align="center" class="miGVBusquedaFicha_Header" id="ProcedimientoHeader">
                    <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial;">Agregar Procedimiento de Enfermería</span>
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    <span style="padding-left:10px">Procedimiento&nbsp;</span>
                </td>
                <td style="width: 260px; height: 25px" align="left">
                    <asp:DropDownList ID="ddlProcedimiento" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    <asp:ImageButton ID="btnAgregarRegistroProcedimiento" runat="server" Width="84px" Height="19px" 
                        ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
                        onclick="btnAgregarRegistroProcedimiento_Click" 
                        ToolTip="Agregar Nuevo Procedimiento de Enfermería"/>
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td style="width: 460px; height: 25px" align="center" valign="middle" colspan="3">
                    <asp:ImageButton ID="btnModalAceptarProcedimiento" runat="server" Width="84px" Height="19px"
                        ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                        OnClick="btnModalAceptarProcedimiento_Click"
                        ToolTip="Aceptar" />&nbsp;
                    <asp:ImageButton ID="btnModalCancelarProcedimiento" runat="server" Width="84px" Height="19px"
                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                        OnClick="btnModalCancelarProcedimiento_Click"
                        ToolTip="Cancelar" />
                </td>
            </tr>      
            <tr><td colspan="3"><br /></td></tr>              
        </table>
        <div id="controlProcedimiento" style="display:none">
            <input type="button" id="OKProcedimiento" />
            <input type="button" id="CancelProcedimiento" />
        </div>
       
    </asp:panel>
                                            </td></tr>
                                            
                                            <tr>
                                                <td style="width: 590px;" align="left" valign="top" colspan="2">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="300px">
                                                    
                                                        <tr>                                                            
                                                            <td style="width: 270px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                Procedimiento de Enfermería                                                                  
                                                            </td>
                                                            <td style="width: 30px; height: 26px;" align="right" class="miGVBusquedaFicha_Header">
                                                                <asp:ImageButton ID="btnAgregarDetalleProcedimiento" runat="server" 
                                                                    Width="24px" Height="24px"
                                                                    ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                                                                    OnClick="btnAgregarDetalleProcedimiento_Click"                                                    
                                                                    ToolTip="Agregar"/>                                                                      
                                                            </td>
                                                        </tr> 
                                                          
                                                        <tr>
                                                            <td style="width: 300px; height: 25px" align="center" valign="top" colspan="2">
                                                            
                                                            <asp:UpdatePanel ID="upProcedimiento" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>                                        
                                                                <div id="miGVMantFichaRegitros" style="width: 300px;">
                                                                <asp:GridView ID="GVListaProcedimientos" runat="server" 
                                                                    CssClass="miGVBusquedaFicha"
                                                                    Width="300px"
                                                                    GridLines="None" 
                                                                    AutoGenerateColumns="False"
                                                                    ShowHeader="false"
                                                                    ShowFooter="false"
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false"    
                                                                    OnRowDataBound="GVListaProcedimientos_RowDataBound"
                                                                    OnRowCommand="GVListaProcedimientos_RowCommand">                           
                                                                    
                                                                    <Columns>         
                                                                        
                                                                        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>    
                                                                        
                                                                        <asp:TemplateField HeaderText="Codigo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                        </asp:TemplateField>   
                                                                                                 
                                                                        <asp:TemplateField HeaderText="Descripción">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="270px"/>
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="270px" />
                                                                        </asp:TemplateField> 
                                                                                                       
                                                                    </Columns>
                                                                </asp:GridView>                
                                                                </div>
                                                                <div class="miEspacio"></div>                                            
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            
                                                            </td>                                                        
                                                        </tr>
                                                         
                                                    </table>  
                                                </td>
                                            </tr>  
                                            
                                        </table>   
                                                                                                       
                                    </fieldset>
                                    
                                    <div class="miEspacio">
                                    </div>
                                    
                                    <fieldset>
                                        <legend>Medicamentos de Enfermería</legend>
                                         <table cellpadding="0" cellspacing="0" border="0" width="590px">                                          
                                            
                                            <tr><td colspan="2" height="10px">
                                            
    <atk:ModalPopupExtender ID="pnModalMedicamentos" runat="server"
        TargetControlID="btnMostrarMedicamento"
        PopupControlID="pnlMedicamento"
        BackgroundCssClass="MiModalBackground" 
        OkControlID="OKMedicamento" 
        CancelControlID="CancelMedicamento"
        Drag="True" 
        PopupDragHandleControlID="MedicamentoHeader" DynamicServicePath="" Enabled="True" />    
    <asp:panel id="pnlMedicamento" BackColor="White" BorderColor="Black" BorderWidth="1px" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" width="460px">    
            <tr>
                <td style="width: 460px; height: 26px; cursor: pointer;" colspan="3" align="center" class="miGVBusquedaFicha_Header" id="MedicamentoHeader">
                    <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial;">Agregar Medicamento</span>
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    <span style="padding-left:10px">Medicamento&nbsp;</span><asp:HiddenField ID="hidencodigoMedicamento" runat="server" />
                </td>
                <td style="width: 260px; height: 25px" align="left">
                    <asp:DropDownList ID="ddlMedicamento" runat="server" Width="250px" 
                        OnSelectedIndexChanged="ddlMedicamento_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    &nbsp;</td>
            <tr>
                <td style="width: 100px; height: 25px" align="left" valign="middle">
                    <span style="padding-left:10px"><asp:label ID="lblCantidadModalMedicamento" runat="server" Text="Cantidad"/></span>
                </td>
                <td style="width: 260px; height: 25px" align="left">
                    <asp:TextBox ID="tbCantidadMedicamento" runat="server" CssClass="miTextBox" 
                        Width="50px" />
                    <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" 
                        FilterType="Numbers" TargetControlID="tbCantidadMedicamento" Enabled="True">
                    </atk:FilteredTextBoxExtender>           
                </td>   
                <td style="width: 100px; height: 25px" align="left" valign="middle">                    
                </td>         
            </tr>  
            <tr><td colspan="3" class="style1"><br /></td></tr>              
            <tr>
                <td style="width: 460px; height: 25px" align="center" valign="middle" colspan="3">
                    <asp:ImageButton ID="btnModalAceptarMedicamento" runat="server" Width="84px" Height="19px"
                        ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                        OnClick="btnModalAceptarMedicamento_Click"
                        ToolTip="Aceptar" />&nbsp;
                    <asp:ImageButton ID="btnModalCancelarMedicamento" runat="server" Width="84px" Height="19px"
                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" 
                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                        OnClick="btnModalCancelarMedicamento_Click"
                        ToolTip="Cancelar" />
                </td>
            </tr>      
            <tr><td colspan="3"><br /></td></tr>             
        </table>
        <div id="controlMedicamento" style="display:none">
            <input type="button" id="OKMedicamento" />
            <input type="button" id="CancelMedicamento" />
        </div>
       
    </asp:panel>  
                                              
                                            </td></tr>
                                            
                                            <tr>
                                                <td style="width: 590px;" align="left" valign="top" colspan="2">
                                                
                                                    <table cellpadding="0" cellspacing="0" border="0" width="450px">
                                                        <tr>
                                                            <td style="width: 320px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                Medicamentos de Enfermería                                                                    
                                                            </td>
                                                            <td style="width: 100px; height: 26px; text-align:center; color:White;font-size:10px;" align="center" class="miGVBusquedaFicha_Header">
                                                                Cantidad                                                                  
                                                            </td>
                                                            <td style="width: 30px; height: 26px;" align="right" class="miGVBusquedaFicha_Header">
                                                                <asp:ImageButton ID="btnAgregarDetalleMedicamento" runat="server" Width="24px" Height="24px"
                                                                    ImageUrl="~/App_Themes/Imagenes/btnAgregarRegistroDetalle_1.png"   
                                                                    OnClick="btnAgregarDetalleMedicamento_Click"                                                    
                                                                    ToolTip="Agregar"/>                                                                      
                                                            </td>
                                                        </tr> 
                                                          
                                                        <tr>
                                                            <td style="width: 450px; height: 25px" align="center" valign="top" colspan="3">
                                                                            
                                                            <asp:UpdatePanel ID="upMedicamento" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>                                        
                                                                <div id="miGVMantFichaRegitros" style="width: 450px;">
                                                                <asp:GridView ID="GVListaMedicamentos" runat="server" 
                                                                    CssClass="miGVBusquedaFicha"
                                                                    GridLines="None" 
                                                                    Width="450px"
                                                                    AutoGenerateColumns="False"
                                                                    ShowHeader="false"
                                                                    ShowFooter="false"
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false" 
                                                                    OnRowDataBound="GVListaMedicamentos_RowDataBound"
                                                                    OnRowCommand="GVListaMedicamentos_RowCommand">                           
                                                                    
                                                                    <Columns>         
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                                                                    CommandName="Actualizar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Editar Registro" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                                                                    CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Quitar Registro" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Center" Width="30px" />
                                                                        </asp:TemplateField>    
                                                                        
                                                                        <asp:TemplateField HeaderText="Codigo">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Codigo") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="0"/>
                                                                            <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0" />
                                                                        </asp:TemplateField>   
                                                                                                 
                                                                        <asp:TemplateField HeaderText="Descripción">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="290px"/>
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="Left" Width="290px" />
                                                                        </asp:TemplateField> 
                                                                        
                                                                        <asp:TemplateField HeaderText="Cantidad">                                                                      
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Cantidad") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="130px"/>
                                                                            <ItemStyle CssClass="miGVBusquedaFicha_Rows" HorizontalAlign="left" Width="100px" />
                                                                        </asp:TemplateField> 
                                                                                                       
                                                                    </Columns>
                                                                </asp:GridView>                
                                                                </div>
                                                                <div class="miEspacio"></div>                                            
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>  
                                                            
                                                            </td>                                                        
                                                        </tr>
                                                         
                                                    </table> 
                                                    <div style="display:none">
                                                     <asp:Button ID="btnMostrarMedicamento" runat="server" Text="Button" />      
                                                    </div>
                                                
                                                </td>
                                            </tr> 
                                            
                                        </table>   
                                                                                                        
                                        
                                    </fieldset>
                                    
                                    <div class="miEspacio">
                                    </div>
                                </div>
                                      
                                </ContentTemplate>
                            </atk:TabPanel>                            
                            
                            <atk:TabPanel ID="miFichaTab3" runat="server" HeaderText="Tab3">
                                <HeaderTemplate>
                                    Datos Finales de Atención
                                </HeaderTemplate>
                                <ContentTemplate>
                                
                                <div id="Bloque_DatosFinales" runat="server" style="border:0; margin:0;">
                                    <fieldset>
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px">
                                        
                                            <tr>
                                                <td colspan="3" style="height: 15px;" align="right">
                                                    <em>Campos Obligatorios (*)</em>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left" valign="middle">
                                                    <span>Destino final del paciente&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" valign="middle">
                                                    <asp:DropDownList ID="ddlDestino" runat="server" Width="200" >
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVerDestino" runat="server" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left">
                                                    <span>Hora de Salida&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                </td>
                                                <td style="width: 620px; height: 25px" align="left" colspan="2" valign="middle">                                                    
                                                    <asp:Image ID="imgTimepicker2" runat="server" ImageUrl="~/App_Themes/Imagenes/ima_timepicker.gif" />                                                                                                                                        
                                                    <MKB:TimeSelector ID="TimeSelector2" runat="server"
                                                        SelectedTimeFormat="TwentyFour" MinuteIncrement="1" DisplaySeconds="false">                                                        
                                                    </MKB:TimeSelector>
                                                    <asp:Label ID="lblVerHoraSalida" runat="server" />
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </fieldset>
                                    <div class="miEspacio">
                                    </div>
                                    
                                    <asp:Panel ID="pnlAcompañante" runat="server">
                                                                        
                                    <fieldset>
                                        <legend>Datos del Acompañante</legend>
                                        <table cellpadding="0" cellspacing="0" border="0" width="790px" style="border: solid 0px red">
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left" valign="middle">
                                                    <span>Nombre Completo&nbsp;</span>
                                                </td>
                                                <td style="width: 385px; height: 25px" align="left" valign="middle">
                                                    <asp:TextBox ID="tbAcompanante" runat="server" CssClass="miTextBox" Width="370" Enabled="false" />     
                                                    <asp:HiddenField ID="hidenCodigoPersonaRecoge" runat="server" />  
                                                    <asp:HiddenField ID="hidenCodigoTipoPersonaRecoge" runat="server" />  
                                                    <asp:Label ID="lblVerAcompañante" runat="server" />                                            
                                                </td>
                                                 <td style="width: 235px; height: 25px;" valign="middle" align="left">
                                                    <asp:ImageButton ID="btnBuscarAcompananteSalida" runat="server" Width="74" Height="19"
                                                        ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png" 
                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                        Tooltip="Buscar Acompañante" 
                                                        OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','0','recoje');" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 170px; height: 25px" align="left" valign="middle">
                                                    <span>Tipo de Persona&nbsp;</span>  
                                                </td>
                                                <td style="width: 620px; height: 25px;"  colspan="2" align="left" valign="middle">
                                                    <asp:Label ID="lblTipoAcompanante" runat="server" />            
                                                </td>
                                               
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                                    
                                    <div class="miEspacio">
                                    </div>
                                </div>
                                    
                                </ContentTemplate>
                            </atk:TabPanel>
                            
                        </atk:TabContainer>
                    </div>
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>
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


    function detalle(obj) {
        var hwd = "frmDetalle.aspx?query=" + obj.codPersona
        window.open(hwd, "ventana1", "width=450,height=400,scrollbars=NO")
    }
</script>

    <uc1:ingresarDiagnostico id="ucIngresarDiagnostico" runat="server" />   
    <uc2:ingresarProcedimiento id="ucIngresarProcedimiento" runat="server" />  
    <uc3:ingresarMedicamento id="ucIngresarMedicamento" runat="server" />   
    
</div>    
    
</asp:Content>
