<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="AsignacionAulas.aspx.vb" Inherits="Mantenimientos_Colegio_AsignacionAulas" title="Página sin título" %>

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
    
    #preview{
	    position:absolute;
	    border:1px solid #ccc;
	    background:#333;
	    padding:5px;
	    display:none;
	    color:#fff;
	}    
</style>

<script type="text/javascript">
    

    function pageLoad(sender, args) {

        if (args.get_isPartialLoad()) {
            imagePreview();
            
        }

    }

    function cerrar() {

        window.close();

    }	
    
</script> 

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional"  >
    
     <Triggers>
        <asp:PostBackTrigger ControlID="TabContainer1$miTab1$btnExportar" />
    </Triggers>
    
    <ContentTemplate>
    
 <div id="miContainerMantenimiento">
    <atk:TabContainer ID="TabContainer1" runat="server" Width="881px" ActiveTabIndex="0"
        AutoPostBack="false" ScrollBars="None" >
        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Busqueda" />
            </HeaderTemplate>
            <ContentTemplate> 
                <div style="border: solid 0px blue; width: 650px;">
                    <div id="miBusquedaMant"><!-- 650px -->
                        <fieldset>
                            <legend>Criterios de busqueda</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red;
                                min-width: 800px;">
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="bottom">
                                        <span>Año Académico</span>   
                                    </td>
                                    <td style="width: 550px; height: 25px; padding-left:10px" align="left" valign="bottom">
                                        <asp:DropDownList  ID="ddlBuscarAnioAcademico" runat="server" Width="255px">
                                        </asp:DropDownList>   
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                        
                                    </td>
                                    <td style="width: 100px; padding-top:6px" align="right" valign="top" rowspan="5">
                                        
                                                <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                    onclick="btnBuscar_Click" ToolTip="Buscar Registros"/><br /><br />
                                                <asp:ImageButton ID="btnLimpiar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>     
                                            
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="bottom">                                        
                                        <span>Sede</span> 
                                    </td>
                                    <td style="width: 650px; height: 25px; padding-left:10px" align="left" valign="bottom">
                                        <asp:DropDownList  ID="ddlBuscarSede" runat="server" Width="255px">
                                        </asp:DropDownList>  
                                    </td>
                                </tr> 
                                 <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="bottom">     
                                        <span>Grado </span>
                                    </td>
                                    <td style="width: 650px; height: 25px; padding-left:10px" align="left" valign="bottom">
                                        <asp:DropDownList  ID="ddlBuscarGrados" runat="server" Width="255px"  
                                            OnSelectedIndexChanged="ddlBuscarGrados_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>   
                                    </td>
                                </tr>    
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="bottom">
                                        <span>Aula</span>
                                    </td>
                                    <td style="width: 650px; height: 25px; padding-left:10px" align="left" valign="bottom">
                                        <asp:DropDownList  ID="ddlBuscarAulas" runat="server" Width="255px">
                                        </asp:DropDownList>  
                                    </td>
                                </tr>  
                            </table>
                        </fieldset>
                    </div>
                    <div class="miEspacio">
                    </div>                    
                    <div id="misRegistrosEncontrados"  style="width: 840px;">
                        <fieldset style="width: 840px;">
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width:820px;">
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
                                    <asp:RadioButtonList ID="rbExportar" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Text="Word"/>
                                        <asp:ListItem Value="1" Text="Excel" Selected="True"/>
                                        <asp:ListItem Value="2" Text="Pdf"/>
                                        <asp:ListItem Value="3" Text="Html"/>
                                    </asp:RadioButtonList>                                    
                                </td>                                
                                <td style="padding-right :20px; width : 300px; height: 21px;" align="right" valign="middle">
                                    <asp:ImageButton ID="btnNuevo" runat="server" Width="74px" Height="19px" 
                                        ImageUrl="~/App_Themes/Imagenes/btnNuevo_1.png"
                                        onmouseover="this.src = '../App_Themes/Imagenes/btnNuevo_2.png'" 
                                        onmouseout="this.src = '../App_Themes/Imagenes/btnNuevo_1.png'" 
                                        onclick="btnNuevo_Click" 
                                        ToolTip="Nuevo Registro"/>
&nbsp;&nbsp;                                        
<asp:Button ID="btnEstadoBimestre" runat="server" OnClick="btnEstadoBimestre_Click" text="Grabar Estado Bimestres" />
                                        
                                        
                                </td>                                                                     
                               </tr>
                            </table>
                           
                        </fieldset>                    
                    </div>                    
                    <div class="miEspacio">
                    </div>
                    <div id="miGridviewMantActualizacion_Ficha">
                        <asp:GridView ID="GridView1" runat="server" 
                            Width="840px" 
                            CssClass="miGridviewBusqueda" 
                            GridLines="None" 
                            AutoGenerateColumns="False"
                            EmptyDataText=" - No se encontraron resultados - "
                            OnRowDataBound="GridView1_RowDataBound"
                            OnRowCommand="GridView1_RowCommand">
                            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                            <Columns>                            
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                            CommandName="Actualizar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Actualizar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_eliminar.png" 
                                            CommandName="Eliminar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Eliminar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnActivar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_activar.png" 
                                            CommandName="Activar" CommandArgument='<%# Bind("Codigo") %>' ToolTip="Activar Registro" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="0px" />
                                </asp:TemplateField>
                                                                
                                
                                <asp:TemplateField HeaderText="Grado"> 
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescGrado" runat="server" Text='<%# Bind("DescGrado") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="70px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Aula">                                                                     
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DescAula" runat="server" Text='<%# Bind("DescAula") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="70px" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Aula SIAGIE">                                                                     
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DescAulaMinisterio" runat="server" Text='<%# Bind("DescAulaMinisterio") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"/>
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" >
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="60px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="DescAnioAcademico" HeaderText="Año" >
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="DescSede" HeaderText="Sede" >
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="60px" />
                                </asp:BoundField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    
                                        <a class="preview" id="btnLinkVerFotoTutor" runat="server" >
                                            <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Tutor">
                                    <ItemTemplate>                     
                                        <asp:Label ID="lblTutor" runat="server" Text='<%# Bind("NombrePersona") %>' />                                                                                    
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a class="preview" id="btnLinkVerFotoRespSalon" runat="server" style="display: none;" >
                                            <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                                              
                                 <asp:BoundField DataField="NombrePersonaResponsableSalon" HeaderText="Responsable de Salón" >
                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle  CssClass="miHiddenStyle" HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                  
                                  <asp:TemplateField>
                                    <ItemTemplate>
                                        <a class="preview" id="btnLinkVerFotoRespActa" runat="server" style="display: none;" >
                                            <img alt="" src="/SaintGeorgeOnline/App_Themes/Imagenes/opc_foto.png" style="border:0" /></a>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="20px" />
                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                 
                                 <asp:BoundField DataField="NombrePersonaResponsableActa" HeaderText="Responsable de Acta" >
                                    <HeaderStyle CssClass="miHiddenStyle" HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Estado" >
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle"/>
                                    <ItemStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="RutaFotoTutora" >
                                    <HeaderStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle"/>
                                    <ItemStyle HorizontalAlign="Center" Width="0px" CssClass="miHiddenStyle" />
                                </asp:BoundField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:label ID="lblCodigoAsignacionAula" runat="server" Text='<%# Bind("Codigo") %>' />
    </ItemTemplate>
    <HeaderStyle CssClass="miHiddenStyle" Width="0px" />
    <ItemStyle CssClass="miHiddenStyle" Width="0px" />
</asp:TemplateField>    
                                
<asp:TemplateField HeaderText="No 1">
    <ItemTemplate>
        <asp:CheckBox ID="chkNotaBim1"  runat="server" />
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="38px" />
    <ItemStyle HorizontalAlign="Center" Width="38px" />
</asp:TemplateField>           
<asp:TemplateField HeaderText="No 2">
    <ItemTemplate>
        <asp:CheckBox ID="chkNotaBim2"  runat="server" />
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="38px" />
    <ItemStyle HorizontalAlign="Center" Width="38px" />
</asp:TemplateField>        
<asp:TemplateField HeaderText="No 3">
    <ItemTemplate>
        <asp:CheckBox ID="chkNotaBim3"  runat="server" />
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="38px" />
    <ItemStyle HorizontalAlign="Center" Width="38px" />
</asp:TemplateField>        
<asp:TemplateField HeaderText="No 4">
    <ItemTemplate>
        <asp:CheckBox ID="chkNotaBim4"  runat="server" />
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="38px" />
    <ItemStyle HorizontalAlign="Center" Width="38px" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Co 1">
    <ItemTemplate>
        <asp:CheckBox ID="chkconductaBim1"  runat="server" />
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="38px" />
    <ItemStyle HorizontalAlign="Center" Width="38px" />
</asp:TemplateField>           
<asp:TemplateField HeaderText="Co 2">
    <ItemTemplate>
        <asp:CheckBox ID="chkconductaBim2"  runat="server" />
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="38px" />
    <ItemStyle HorizontalAlign="Center" Width="38px" />
</asp:TemplateField>        
<asp:TemplateField HeaderText="Co 3">
    <ItemTemplate>
        <asp:CheckBox ID="chkconductaBim3"  runat="server" />
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="38px" />
    <ItemStyle HorizontalAlign="Center" Width="38px" />
</asp:TemplateField>        
<asp:TemplateField HeaderText="Co 4">
    <ItemTemplate>
        <asp:CheckBox ID="chkconductaBim4"  runat="server" />
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="38px" />
    <ItemStyle HorizontalAlign="Center" Width="38px" />
</asp:TemplateField>                                
                                
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="miEspacio">
                    </div>
                </div>
            </ContentTemplate>
        </atk:TabPanel>  
       
    </atk:TabContainer>     
    
    <asp:Panel ID="pnlAgregar" runat="server" Width="560px" style="border: solid 1px black; background: #FFFFFF; display:none; margin: 0; padding: 0; font-size: 10px; font-family: Arial;">
                                     <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 560px;">                                
                                        <tr style="cursor :pointer ;" id="miDragControl" >
                                            <td style="width: 545px; height: 26px" colspan="2" align="center" class="miGVBusquedaFicha_Header">
                                                <span style="padding-left: 20px; font-weight: bold; font-size: 11px;
                                                    font-family: Arial; cursor: pointer">Agregar Datos del Registro </span>
                                            </td>
                                            <td style="width: 15px; height: 26px;" valign="middle" align="right" class="miGVBusquedaFicha_Header">
                                                <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                                    Width="16" Height="15" CssClass="TitlebarLeft_Button" />&nbsp;
                                            </td>
                                       </tr>
                                       
                                        <tr>
                                            <td colspan="2" style=" padding-right:5px; height: 15px;" align="right">
                                                <em>Campos Obligatorios (*)</em>
                                            </td>
                                        </tr>
                                        
                                           <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Anio Académico :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                                 <asp:HiddenField ID="hd_Codigo" runat="server" /> 
                                            </td>
                                            <td style="min-width: 380px;" align="left" valign="middle">
                                                <asp:DropDownList  ID="ddlAnioAcademico" runat="server" Width="255px">
                                                </asp:DropDownList> 
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Sede :&nbsp;</span><span class="camposObligatorios">(*)</span>                                        
                                            </td>
                                            <td style="min-width: 380px;" align="left" valign="middle">
                                                <asp:DropDownList  ID="ddlSede" runat="server" Width="255px" OnSelectedIndexChanged="ddlSede_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList> 
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Grado :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                            </td>
                                            <td style="min-width: 380px; " align="left" valign="middle">
                                                <asp:DropDownList  ID="ddlGrados" runat="server" Width="255px"  OnSelectedIndexChanged="ddlGrados_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>   
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Aula :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                              </td>
                                            <td style="min-width: 380px; " align="left" valign="middle">
                                                <asp:DropDownList  ID="ddlAulas" runat="server" Width="255px">
                                                </asp:DropDownList>   
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Aula Ministerio :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                              </td>
                                            <td style="min-width: 380px; " align="left" valign="middle">
                                                <asp:DropDownList  ID="ddlAulaMinisterio" runat="server" Width="255px">
                                                </asp:DropDownList>   
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Ambiente :&nbsp;</span>                                        
                                            </td>
                                            <td style="min-width: 380px; " align="left" valign="middle">
                                                <asp:DropDownList  ID="ddlAmbiente" runat="server" Width="255px">
                                                </asp:DropDownList> 
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Tutor :&nbsp;</span><span class="camposObligatorios">(*)</span>           
                                                <asp:HiddenField ID="hidenCodigoPersonaTutor" runat="server" />                               
                                            </td>
                                            <td style="min-width: 380px; " align="left" valign="middle">
                                                <table style="width:380px;" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td align="left" valign="middle" style="width:180px;">
                                                            <asp:TextBox ID="tbPersonaTutor" runat="server" 
                                                                CssClass="miTextBox" Width="250px" MaxLength="100" Enabled="False" />                                                
                                                        </td>
                                                        <td align="left" valign="middle" style="width:200px;">
                                                            <asp:ImageButton ID="btnBuscarTutor" runat="server" Width="74px" Height="19px"
                                                                ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png"                                                       
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                                Tooltip="Buscar Tutor"
                                                                OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','2','tutor');" />                                                
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Capacidad :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                                <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" 
                                                    FilterType="Numbers" TargetControlID="tbCapacidad" Enabled="True">
                                                </atk:FilteredTextBoxExtender>
                                            </td>
                                            <td style="min-width: 380px; " align="left" valign="middle">
                                                <asp:TextBox ID="tbCapacidad" runat="server" CssClass="miTextBox" Width="250px" MaxLength="10"/>
                                            </td>
                                        </tr> 
                                        
                                        <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Responsable de Acta  :&nbsp;</span><span class="camposObligatorios">(*)</span>           
                                                <asp:HiddenField ID="hidenCodigoPersonaResponsableActa" runat="server" />                               
                                            </td>
                                            <td style="min-width: 380px; " align="left" valign="middle">
                                                <table style="width:380px;" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td align="left" valign="middle" style="width:180px;">
                                                            <asp:TextBox ID="tbPersonaResponsableActa" runat="server" 
                                                                CssClass="miTextBox" Width="250px" MaxLength="100" Enabled="False" />                                                
                                                        </td>
                                                        <td align="left" valign="middle" style="width:200px;">
                                                            <asp:ImageButton ID="btnBuscarResponsableActa" runat="server" Width="74px" Height="19px"
                                                                ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png"                                                       
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                                Tooltip="Buscar Responsable de Acta"
                                                                OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','2','ResponsableActa');" />                                                
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width: 180px;  padding :10px;" align="left" valign="middle">
                                                <span>Responsable de Salon  :&nbsp;</span><span class="camposObligatorios">(*)</span>           
                                                <asp:HiddenField ID="hidenCodigoPersonaResponsableSalon" runat="server" />                               
                                            </td>
                                            <td style="min-width: 380px;" align="left" valign="middle">
                                                <table style="width:380px;" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td align="left" valign="middle" style="width:180px;">
                                                            <asp:TextBox ID="tbPersonaResponsableSalon" runat="server" 
                                                                CssClass="miTextBox" Width="250px" MaxLength="100" Enabled="False" />                                                
                                                        </td>
                                                        <td align="left" valign="middle" style="width:200px;">
                                                            <asp:ImageButton ID="btnBuscarResponsableSalon" runat="server" Width="74px" Height="19px"
                                                                ImageUrl="~/App_Themes/Imagenes/btnBuscarPersona_1.png"                                                       
                                                                onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_2.png'"
                                                                onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnBuscarPersona_1.png'" 
                                                                Tooltip="Buscar Responsable de Salon"
                                                                OnClientClick="abrirPopupParams('/SaintGeorgeOnline/Popups/buscarPersona.aspx','2','ResponsableSalon');" />                                                
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="padding-left :10px; height : 30px;" colspan ="2" valign="bottom" align="left">
                                                 <asp:ImageButton ID="btnGrabar" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                                    onclick="btnGrabar_Click" />&nbsp;
                                                <asp:ImageButton ID="btnCancelar" runat="server" Width="84px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                                                    onclick="btnCancelar_Click" CausesValidation="False"/>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="2" style="height:10px;">
                                            
                                                                    <div id="controlCampoInformacion" style="display: none">
                                                                        <input type="button" id="OKAgregarCurso" />
                                                                        <input type="button" id="CancelAgregarCurso" />                                                
                                                                        <asp:Button ID="btnAgregarDetalleAgregarCurso" runat="server" />      
                                                                    </div>                    
                                            
                                            </td>
                                        </tr>
                                        
                                    </table>
                                 </asp:Panel>
    
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
      
    <atk:ModalPopupExtender ID="ModalPopupExtender2" runat="server"        
                                            TargetControlID="lblAccionAgregar"
                                            PopupControlID="pnlAgregar" 
                                            BackgroundCssClass="MiModalBackground"
                                            OkControlID="OKAgregarCurso" 
                                            CancelControlID="CancelAgregarCurso"
                                            Drag="true"  
                                            PopupDragHandleControlID="miDragControl" DynamicServicePath="" 
                                            Enabled="true"
                                            />  
                            
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
         
     <asp:Label ID="lblAccionAgregar" runat="server" ForeColor="White" Text="..."></asp:Label>
                 
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



