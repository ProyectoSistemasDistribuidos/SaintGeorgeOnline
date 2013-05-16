<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="PrestamoLibros.aspx.vb" Inherits="Modulo_BancoLibros_PrestamoLibros" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
     
.verticaltext {
    writing-mode: tb-rl;
    filter: flipv fliph;
}

.miClaseCheckBox td{ 
    width: 25px;
    height :26px;
    text-align: center;
}

#mask {
  position:absolute;
  left:0;
  top:0;
  z-index:9000;
  background-color:#000;
  display:none;
}
  
#boxes .window {
  position:absolute;
  left:0;
  top:0;
  width:440px;
  height:200px;
  display:none;
  z-index:9999;
  padding:20px;
}

#boxes #dialog {
  width:400px; 
  height:200px;
  padding:0;
  background-color:#FFFFFF; 
  border: 0;
}

</style> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.mb.flipText.min.js"></script> 

 <div id="miPaginaMantenimiento">
  <asp:UpdatePanel ID="Udp_PaginaMantenimiento" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div id="miContainerMantenimiento">
            <atk:TabContainer ID="Tc_PaginaMantenimiento" 
                              runat="server" 
                              Width="881px" 
                              ActiveTabIndex="0"
                              AutoPostBack="false" 
                              ScrollBars="None" >
                <atk:TabPanel ID="Tp_Registro" runat="server" HeaderText="Tab1" Enabled="true">
                    <HeaderTemplate>
                        <asp:Label ID="lbl_TituloPanel" runat="server" Text="Registro" />
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="border: solid 0px blue; width: 870px;">
                            <div id="miContenidoFicha" style="border: solid 0px red">
                                <div id="miCabeceraFicha" style="border: solid 0px orange">
                                   <table cellpadding="0" cellspacing="0" border="0" width="840px" style="margin: 0;">
                                       <tr>
                                          <td style="width: 545px;" align="left" valign="middle">                    
                                            <fieldset style="width:840px;margin: 0;" id="Bloque_DatosPersonales" runat="server">                        
                                                <legend style="width:400px">Criterios de Busqueda</legend>
                                                <table cellpadding="0" cellspacing="0" border="0" width="840px">                            
                                                    <tr>
                                                       <td style="width: 80px; height: 25px;" align="left" valign="middle" >
                                                            <span style="padding-left:10px;">Año:&nbsp;</span>
                                                        </td>
                                                        <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlBuscarAnio" runat="server" Width="100px" style="font-size: 8pt; font-family: Arial;" AutoPostBack="true" OnSelectedIndexChanged="ddlBuscarAnio_SelectedIndexChanged">
                                                            </asp:DropDownList>  
                                                       </td> 
                                                       <td style="width: 80px; height: 25px;" align="left" valign="middle" >
                                                            <span style="padding-left:10px;">Grado:&nbsp;</span>
                                                        </td>
                                                        <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlBuscarGrado" runat="server" Width="100px" style="font-size: 8pt; font-family: Arial;" AutoPostBack="true" OnSelectedIndexChanged="ddlBuscarGrado_SelectedIndexChanged">
                                                            </asp:DropDownList>  
                                                       </td> 
                                                       
                                                       <td style="width: 80px; height: 25px;" align="left" valign="middle" >
                                                            <span style="padding-left:10px;">Aula:&nbsp;</span>
                                                        </td>
                                                        <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlBuscarAula" runat="server" Width="100px" style="font-size: 8pt; font-family: Arial;" AutoPostBack="true" OnSelectedIndexChanged="ddlBuscarAula_SelectedIndexChanged">
                                                            </asp:DropDownList>  
                                                       </td> 
                                                       
                                                       
                                                       <td style="width: 80px; height: 25px;" align="left" valign="middle" >
                                                            <span style="padding-left:10px;">Idioma:&nbsp;</span>
                                                        </td>
                                                        <td style="width: 120px; height: 25px;" align="left" valign="middle">
                                                            <asp:DropDownList ID="ddlBuscarIdioma" runat="server" Width="100px" style="font-size: 8pt; font-family: Arial;" AutoPostBack="true" OnSelectedIndexChanged="ddlBuscarIdioma_SelectedIndexChanged">
                                                            </asp:DropDownList>  
                                                       </td> 
                                                       <td style="width: 240px;" align="center" valign="middle">                                                          
                                                       </td>                                                                                          
                                                    </tr>
                                                </table>
                                             </fieldset>
                                         </td>
                                       </tr>
                                       <tr>
                                          <td></td>
                                       </tr>
                                   </table>
                                </div>
                                <div class="miEspacio"></div>  
                                <div id="miCabeceraFicha" style="border: solid 0px orange">
                                   <table cellpadding="0" cellspacing="0" border="0" width="840px" style="margin: 0;">
                                       <tr>
                                          <td style="width: 545px;" align="left" valign="middle">
                                            <fieldset style="width:840px; margin: 0;" id="Fieldset1" runat="server">                        
                                            <legend style="width:400px;">Relación de Alumnos</legend>
                                                <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width:820px;">                                
                                                <tr>
                                                    <td valign ="top" align ="left" style ="width :820px; ">
                                                    <div id="miGridviewMantActualizacion_Ficha" style="width:815px;" >  
                                                        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0px red; width:815px;">                                
                                                            <tr>
                                                                <td style="border: 0; width: 302px;  text-align:left ; color: #FFFFFF; font-size:10px; background-color:#0a0f14;" align="left" valign="bottom">                                                                
                                                                    <div style="width: 302px; height: 140px;" >                                                                      
                                                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 302px;">
                                                                        <tr>
                                                                            <td colspan="3" style="border-bottom:solid 1px #a6a3a3; width:302px; height: 15px;">
                                                                                <b><span>Total de Libros</span></b>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" style="border-bottom:solid 1px #a6a3a3; width:302px; height: 15px;">
                                                                                <b><span>Total de Prestamos</span></b>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width:25px; height: 110px;" align="center" valign="bottom">
                                                                                <b><span>N°</span></b>
                                                                            </td>
                                                                            <td style="width: 205px; height: 110px;" align="left" valign="bottom">
                                                                                <b><span> Alumnos</span></b>
                                                                            </td>
                                                                            <td style="width: 32px; height: 110px;" align="center" valign="bottom">
                                                                                <b><span class="bt">Todos</span></b>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    </div>
                                                                </td>
                                                                <td style="border: 0; width: 513px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;" align="left">
                                                                    <asp:HiddenField ID="hd_activado" runat="server" /> 
                                                                    <div style="width: 513px; height: 140px; border: solid 0px red;">
                                                                    <asp:DataList ID="dl_NombreLibros" runat="server" RepeatDirection="Horizontal">    
                                                                        <ItemStyle Width="25px" Height="140px" />                                                                                                                                       
                                                                        <ItemTemplate> 
                                                                            <div style="width: 25px; height: 140px; border: solid 0px #0a0f14;">  
                                                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 25px; height: 140px;">
                                                                            <tr>
                                                                                <td style="border-bottom:solid 1px #a6a3a3; width:25px; height: 15px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;" align="left" valign="bottom">
                                                                                    <div style="width: 25px; height: 15px;">
                                                                                        <asp:Label ID="Label2" ForeColor="White"  runat="server" Font-Bold="true" Text='<%# Eval("TotalCopias") %>' /> 
                                                                                    </div>                                                                                     
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border-bottom:solid 1px #a6a3a3; width:25px; height: 15px; text-align:left ; color:White; font-size:10px; background-color:#0a0f14;" align="left" valign="bottom">                                                                                    
                                                                                    <div style="width: 25px; height: 15px;">
                                                                                        <asp:Label ID="Label1" ForeColor="White"  runat="server" Font-Bold="true" Text='<%# Eval("Prestado1") %>' />                                                                                       
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="border-bottom:solid 0px #a6a3a3; width: 25px; height: 110px;" align="left" valign="bottom">      
                                                                                    <div style="width: 25px; height: 110px; float:left">
                                                                                    <table cellspacing="0" cellpadding="0" border="0" style="width: 25px; height: 110px">
                                                                                    <tr>
                                                                                        <td style="width: 25px; height: 110px;" align="left" valign="bottom">                                                                                        
                                                                                            <a id="linkModalLibro" runat="server" href="#dialog" style="text-decoration: none; font-weight: bold; color: #FFFFFF; cursor: pointer;">                                                                                                                                                  
                                                                                                <asp:Label ID="lblAbreviatura" runat="server" Text='<%# Eval("Abrev") %>' CssClass="bt" />  
                                                                                                <asp:Label ID="lblTitulo" runat="server" Text='<%# Eval("Titulo") %>' style="display: none;" />  
                                                                                                <asp:Label ID="lblImgURL" runat="server" Text='<%# Eval("RutaPortada") %>' style="display: none;" />  
                                                                                                <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("PrecioPrestamo") %>' style="display: none;" />  
                                                                                            </a>                                                                                    
                                                                                        </td>
                                                                                    </tr>
                                                                                    </table>
                                                                                    </div>                                                                              
                                                                                </td>
                                                                            </tr>
                                                                            </table> 
                                                                            </div>                                                                                                                                                       
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                    </div>
                                                                </td>                                    
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="height: 10px; width: 815px; background-color: #0a0f14">
                                                                <div style="width: 815px; height: 10px; border: solid 0px red; display: none">
                                                                &nbsp;
                                                                </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left" valign="middle">                                                                        
                                                                        <asp:HiddenField ID="hiddenLibrosCheckAll" runat="server" />   
                                                                        <asp:HiddenField ID="hiddenLibrosPrestados" runat="server" />                                                                                                                                         
                                                                        <asp:HiddenField ID="hiddenLibrosEstadoActual" runat="server" />  
                                                                                                                                                                                                         
                                                                        <asp:HiddenField ID="hiddenMiGridviewIndex" runat="server" Value="" /> 
                                                                        <asp:HiddenField ID="hiddenMiEstadoGrabar" runat="server" Value="1" /> 
                                                                        
                                                                        <div id="miPanelGridview" style="width: 815px; height: 267px; overflow-y: scroll; overflow-x: hidden;">
                                                                        <asp:GridView ID="GridView1" runat="server" 
                                                                            Width="815px" 
                                                                            CssClass="miGridviewBusqueda" 
                                                                            GridLines="None" 
                                                                            AutoGenerateColumns="False"
                                                                            AllowSorting="True"
                                                                            ShowHeader="false" 
                                                                            EmptyDataText=" - No se encontraron resultados - "
                                                                            OnRowDataBound="GridView1_RowDataBound"
                                                                            OnRowCommand="GridView1_RowCommand">
                                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                                                                            <Columns>                                                                                                                      
                                                                                <asp:TemplateField>                                                                      
                                                                                    <ItemTemplate>
                                                                                        <div style="width: 0; border: solid 0px red;">
                                                                                            <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                                </asp:TemplateField>
                                                                                                                                                                         
                                                                                <asp:TemplateField>  
                                                                                    <ItemTemplate>
                                                                                        <div style="width: 25px; border: solid 0px red;">
                                                                                            <asp:Label ID="lblIdFila" runat="server" Text='<%# Bind("IdFila") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="25px" />
                                                                                </asp:TemplateField>
                                                                                                                                                                   
                                                                                <asp:TemplateField>  
                                                                                    <ItemTemplate>
                                                                                        <div style="width: 200px; border: solid 0px red;">
                                                                                            <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' style="font-size: 7pt;" />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="200px" />
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField> 
                                                                                    <ItemTemplate> 
                                                                                        <div style="width: 45px; border: solid 0px red;">  
                                                                                            &nbsp;                                                                          
                                                                                            <img id="btnEditar" runat="server" alt="" src="~/App_Themes/Imagenes/opc_actualizar.png" style="width: 16px; height: 16px; cursor: pointer;" />  
                                                                                            <asp:ImageButton id="btnGrabar" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_grabar.png" style="width: 16px; height: 16px; cursor: pointer; display: none;" CommandName="Grabar" CommandArgument='<%# Container.DataItemIndex %>'/>                                                                                                                                                                                 
                                                                                            <img id="btnCancelar" runat="server" alt="" src="~/App_Themes/Imagenes/opc_eliminar.png" style="width: 16px; height: 16px; cursor: pointer; display: none;" />                                                                                          
                                                                                        </div>                                             
                                                                                    </ItemTemplate> 
                                                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="45px" />  
                                                                                </asp:TemplateField>  
                                                                                
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <div style="width: 25px; border: solid 0px red;">  
                                                                                            <asp:CheckBox ID="chkAll" runat="server" style="width: 25px;" />  
                                                                                            &nbsp;                                
                                                                                        </div>                                                                                        
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Right" Width="25px" BackColor="#ffd426" />  
                                                                                </asp:TemplateField>
                                                                                                            
                                                                                <asp:TemplateField> 
                                                                                    <ItemTemplate>
                                                                                        <div style="width: 520px; border: solid 0px red;">
                                                                                            <asp:CheckBoxList ID="chk_Libros_grilla" runat="server" Enabled="true" RepeatDirection="Horizontal" CssClass="miClaseCheckBox">
                                                                                            </asp:CheckBoxList> 
                                                                                            <span style="display: none;">&nbsp;</span>    
                                                                                        </div>                                                         
                                                                                    </ItemTemplate> 
                                                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="520px" />  
                                                                                </asp:TemplateField> 
                                                                                
                                                                                <asp:TemplateField>                                                                      
                                                                                    <ItemTemplate>
                                                                                        <div style="width: 0; border: solid 0px red;">
                                                                                            <asp:Label ID="lblCodigoPrestamo" runat="server" Text='<%# Bind("CodigoPrestamo") %>' />
                                                                                        </div>    
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="miHiddenStyle" Width="0px" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView> 
                                                                       </div>                                                                                                                                            
                                                                </td>
                                                            </tr>
                                                        </table> 
                                                    </div>
                                                    </td>
                                                </tr>
                                                </table>  
                                            </fieldset>
                                         </td>
                                       </tr>
                                   </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </atk:TabPanel>                              
            </atk:TabContainer>
    </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>  
    
        <atk:ModalPopupExtender id="ModalPopupExtender1" runat="server" 
            TargetControlID="lblModalHandler" 
            PopupControlID="Panel1" 
            BackgroundCssClass="MiModalBackground" 
            DropShadow="false" />
            
        <asp:Panel ID="Panel1" runat="server" style="background-color:#FFFFFF;display:none;width:250px">
            <div style="margin: auto;">
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" OnClick="btnVolver_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <div id="miContenidoModal" runat="server" style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">
                        </div>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                        
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>                              
            </div>
        </asp:Panel>     
        <asp:Label ID="lblModalHandler" runat="server" Text="MiModalHandler" style="display:none;"/> 
        
</ContentTemplate>
</asp:UpdatePanel>  

<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
<ContentTemplate>

        <atk:ModalPopupExtender id="ModalPopupExtender2" runat="server" 
            TargetControlID="lblAccionCargando" 
            PopupControlID="Panel2" 
            BackgroundCssClass="MiModalBackground" 
            DropShadow="false" />
         
        <asp:Panel ID="Panel2" runat="server" style="background-color:#FFFFFF;display:none;width:250px">
            <div style="margin: auto;">
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <span style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">Procesando...<br />Espere un momento.</span>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                        
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>                              
            </div>
        </asp:Panel>        
    
        <asp:Label ID="lblAccionCargando" runat="server" Text="MiModalHandler" style="display:none;"/> 
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
  
<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Tc_PaginaMantenimiento$Tp_Registro$ddlBuscarAnio" />
        <asp:AsyncPostBackTrigger ControlID="Tc_PaginaMantenimiento$Tp_Registro$ddlBuscarGrado" />        
        <asp:AsyncPostBackTrigger ControlID="Tc_PaginaMantenimiento$Tp_Registro$ddlBuscarIdioma" />
    </Triggers>           
</asp:UpdatePanel>        

<div id="boxes">
<div id="dialog" class="window">
<table cellpadding="0" cellspacing="0" border="0" style="width: 400px; height: 200px; border: solid 2px #000000;">
    <tr>
        <td style="width:  30px; height: 26px;" class="miGVBusquedaFicha_Header_V2">
            <span>&nbsp;</span>
        </td>
        <td style="width: 340px; height: 26px;" class="miGVBusquedaFicha_Header_V2">
            <span id="miTituloLibroModal" style="font-weight:bold; font-size:11px; font-family:Arial;"></span>
        </td>
        <td style="width:  30px; height: 26px;" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
            <a href="#" class="close" />
                <img alt="Cerrar" src="../App_Themes/Imagenes/cross_icon_normal.png" style="border: 0; text-decoration: none;"/>
            </a> 
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 10px;"></td>               
    </tr>
    <tr>
        <td style="width:  30px;"></td>
        <td style="width: 340px; height: 162px;" align="left" valign="top">
            <table cellpadding="0" cellspacing="0" border="0" style="width: 340px; font-size: 11px; font-family: Arial; color: #000000;">
                <tr>
                    <td style="width: 80px; height: 162px;" rowspan="3" valign="top" align="center">
                        <img alt="Libro" src="../Imagenes_BancoLibro/aa.gif" 
                            style="border: solid 1px #000000; text-decoration: none; width: 80px; height: 120px;"/>
                    </td>
                    <td style="width: 20px; height: 25px;" rowspan="3" >
                    </td> 
                    <td style="width: 240px; height: 25px;">
                        <span id="miTituloLibro"></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 240px; height: 25px;">
                        <span id="miPrecioLibro"></span>    
                    </td>
                </tr>
                <tr>
                    <td style="width: 240px; height: 112px;">
                        <span id="Span2"></span>   
                    </td>
                </tr>                    
            </table>
        </td>
        <td style="width:  30px;"></td>
    </tr>    
</table> 
</div>  
<!-- Mask to cover the whole screen -->
<div id="mask"></div>
</div>


<script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.scrollTo.js"></script>
<script type="text/javascript">


    $(document).ready(function() {

        $(".bt").mbFlipText(false);

        var dataList = document.getElementById("<%= dl_NombreLibros.ClientID %>");
        var grid = document.getElementById("<%= GridView1.ClientID %>");
        var CboAnio = document.getElementById("<%= ddlBuscarAnio.ClientID %>");
        var CboGrado = document.getElementById("<%= ddlBuscarGrado.ClientID %>");
        var CboAula = document.getElementById("<%= ddlBuscarAula.ClientID %>");
        var CboIdioma = document.getElementById("<%= ddlBuscarIdioma.ClientID %>");

        function miFocus(index) {
            if (grid.rows.length > 0) {
                row = grid.rows[index];
                $("input:hidden[id$=hiddenMiGridviewIndex]").val(index); // Seteo el valor del indice de la fila en el gridview
                $('#miPanelGridview').scrollTo($(row), 500);
            }
        }

        //Evento del checkbox "All"
        $('input:checkbox[id$=chkAll]', grid).click(function(e) {
            if (grid.rows.length > 0) {
                var estadoCheck = $(this).attr('checked');
                var rowIndex = $(this).closest('tr').prevAll().length;
                var cell = grid.rows[rowIndex].cells[5];
                var miDiv = $(cell).find('div')[0];
                var child = $(miDiv).find('table')[0]; // CheckBoxList
                var ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

                if (estadoCheck == true) {
                    ChecksList.attr('checked', true);
                } else {
                    ChecksList.removeAttr('checked');
                }
            }
        });

        //Evento del DataList en sus "a href"
        $('a:[id$=linkModalLibro]', dataList).click(function(e) {

            //Cancel the link behavior
            e.preventDefault();

            //Get the A tag
            var Modal = $(this).attr('href');
            var LinkID = $(this).attr('id');

            var strTituloLibro = $('#' + LinkID + ' span:eq(1)').text();
            var strPrecioLibro = $('#' + LinkID + ' span:eq(3)').text();

            //Get the screen height and width
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();

            //Set heigth and width to mask to fill up the whole screen
            $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

            //transition effect
            $('#mask').fadeIn("fast");
            $('#mask').fadeTo("fast", 0.9);

            //Get the window height and width
            var winH = $(window).height();
            var winW = $(window).width();

            //Set the popup window to center
            $(Modal).css('top', winH / 2 - $(Modal).height() / 2);
            $(Modal).css('left', winW / 2 - $(Modal).width() / 2);

            //$('#miTexto').text($(this).attr('id'));
            $('#miTituloLibroModal').text('Libro : ' + strTituloLibro);
            $('#miTituloLibro').text('Título : ' + strTituloLibro);
            $('#miPrecioLibro').text('Precio : ' + strPrecioLibro);

            //transition effect
            $(Modal).fadeIn("fast");

        });

        //if close button is clicked
        $('.window .close').click(function(e) {
            //Cancel the link behavior
            e.preventDefault();

            $('#mask').hide();
            $('.window').hide();
        });

        //Evento del boton "Editar"
        $('image:[id$=btnEditar]', grid).click(function(e) {

            if (grid.rows.length > 0) {

                var rowIndex = $(this).closest('tr').prevAll().length;
                var row;
                var cellCheckAll;
                var childCheckAll;
                var SpanCheckAll;
                var CheckAll;
                var cell;
                var miDiv;
                var child;
                var table;
                var SpansList;
                var ChecksList;
                var cellButtons;
                var miDivButtons;
                var childEditar;
                var childGrabar;
                var childCancelar;
                var btnEditar;
                var btnGrabar;
                var btnCancelar;

                for (i = 0; i < grid.rows.length; i++) {

                    row = grid.rows[i];
                    cellCheckAll = grid.rows[i].cells[4];
                    SpanCheckAll = $(cellCheckAll).find('span');
                    childCheckAll = $(cellCheckAll).find('input:checkbox');

                    cell = grid.rows[i].cells[5];
                    miDiv = $(cell).find('div')[0];
                    child = $(miDiv).find('table')[0]; // CheckBoxList

                    if (child != undefined) {

                        table = $('table#' + $(child).attr('id'));
                        SpansList = $('table#' + $(child).attr('id')).find('span');
                        ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

                        // Botones
                        cellButtons = grid.rows[i].cells[3];
                        miDivButtons = $(cellButtons).find('div')[0];
                        childEditar = $(miDivButtons).children(0)[0];
                        childGrabar = $(miDivButtons).children(0)[1];
                        childCancelar = $(miDivButtons).children(0)[2];

                        btnEditar = childEditar.id;
                        btnGrabar = childGrabar.id;
                        btnCancelar = childCancelar.id;

                        if (i == rowIndex) {

                            $(row).css({ background: "#dcff7d" });

                            // Grabo los valores actuales del CheckBoxList previos a Editar
                            var cadena = '';
                            var miLength = 0;
                            var miIterador = 0;
                            miLength = ChecksList.length;
                            var miArray = new Array(miLength);

                            $.each(ChecksList, function(e, checkbox) {
                                if (checkbox.checked) {
                                    miArray[miIterador] = "True";
                                } else {
                                    miArray[miIterador] = "False";
                                }
                                miIterador += 1;
                            });

                            $("input:hidden[id$=hiddenLibrosEstadoActual]").val(miArray);

                            if (childCheckAll[0].checked) { $("input:hidden[id$=hiddenLibrosCheckAll]").val("True"); }
                            else { $("input:hidden[id$=hiddenLibrosCheckAll]").val("False"); }

                            // Activo el check "CheckAll" y los botones "Grabar" y "Cancelar" en la fila actual
                            SpanCheckAll.removeAttr('disabled');
                            childCheckAll.removeAttr('disabled');

                            table.removeAttr('disabled');
                            SpansList.removeAttr('disabled');
                            ChecksList.removeAttr('disabled');

                            $(this).hide();
                            $('#' + btnGrabar + '').show();
                            $('#' + btnCancelar + '').show();
                        } else {
                            // Oculto el check "CheckAll" y los botones "Editar","Grabar" y "Cancelar" para las otras filas
                            SpanCheckAll.attr('disabled', 'disabled');
                            childCheckAll.attr('disabled', 'disabled');

                            table.attr('disabled', 'disabled');
                            SpansList.attr('disabled', 'disabled');
                            ChecksList.attr('disabled', 'disabled');
                            $('#' + btnEditar + '').hide();
                            $('#' + btnGrabar + '').hide();
                            $('#' + btnCancelar + '').hide();
                        }
                        $(CboAnio).attr('disabled', 'disabled');
                        $(CboGrado).attr('disabled', 'disabled');
                        $(CboAula).attr('disabled', 'disabled');  
                        $(CboIdioma).attr('disabled', 'disabled');
                    }
                }
                miFocus(rowIndex);
                e.preventDefault();
            }
        });

        //Evento del boton "Grabar"
        $('input:image[id$=btnGrabar]', grid).click(function(e) {

            var estadoCheck = $(this).attr('checked');
            var rowIndex = $(this).closest('tr').prevAll().length;
            var cell = grid.rows[rowIndex].cells[5];
            var miDiv = $(cell).find('div')[0];
            var child = $(miDiv).find('table')[0]; // CheckBoxList
            var ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

            var cadena = '';
            var miLength = 0;
            var miIterador = 0;
            miLength = ChecksList.length;
            var miArray = new Array(miLength);

            $.each(ChecksList, function(e, checkbox) {
                if (checkbox.checked) {
                    miArray[miIterador] = "True";
                } else {
                    miArray[miIterador] = "False";
                }
                miIterador += 1;
            });

            $("input:hidden[id$=hiddenLibrosPrestados]").val(miArray);
        });

        //Evento del boton "Cancelar"
        $('image:[id$=btnCancelar]', grid).click(function(e) {
            if (grid.rows.length > 0) {

                var rowIndex = $(this).closest('tr').prevAll().length;
                var row;
                var cellCheckAll;
                var childCheckAll;
                var SpanCheckAll;
                var CheckAll;
                var cell;
                var miDiv;
                var child;
                var table;
                var SpansList;
                var ChecksList;
                var cellButtons;
                var miDivButtons;
                var childEditar;
                var childGrabar;
                var childCancelar;
                var btnEditar;
                var btnGrabar;
                var btnCancelar;

                for (i = 0; i < grid.rows.length; i++) {

                    row = grid.rows[i];
                    cellCheckAll = grid.rows[i].cells[4];
                    SpanCheckAll = $(cellCheckAll).find('span');
                    childCheckAll = $(cellCheckAll).find('input:checkbox');

                    cell = grid.rows[i].cells[5];
                    miDiv = $(cell).find('div')[0];
                    child = $(miDiv).find('table')[0]; // CheckBoxList

                    table = $('table#' + $(child).attr('id'));
                    SpansList = $('table#' + $(child).attr('id')).find('span');
                    ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

                    // Botones
                    cellButtons = grid.rows[i].cells[3];
                    miDivButtons = $(cellButtons).find('div')[0];
                    childEditar = $(miDivButtons).children(0)[0];
                    childGrabar = $(miDivButtons).children(0)[1];
                    childCancelar = $(miDivButtons).children(0)[2];

                    btnEditar = childEditar.id;
                    btnGrabar = childGrabar.id;
                    btnCancelar = childCancelar.id;

                    if (i == rowIndex) {

                        $(row).css({ background: "#d3eefa" });
                        $(row).hover(
                                function() { $(this).css({ background: "#d3eefa" }); },
                                function() { $(this).css({ background: "#ffffff" }); }
                            );

                        // Reseteo los valores de los CheckBoxList
                        var miIterador = 0;
                        var cadena = '';
                        cadena = $("input:hidden[id$=hiddenLibrosEstadoActual]").val();
                        var miArray = cadena.split(",");

                        $.each(ChecksList, function(e, checkbox) {
                            if (miArray[miIterador] == "True") { checkbox.checked = true; }
                            else { checkbox.checked = false; }
                            miIterador += 1;
                        });

                        var valHiddenCheckAll = $("input:hidden[id$=hiddenLibrosCheckAll]").val();
                        if (valHiddenCheckAll == "True") { childCheckAll[0].checked = true; }
                        else { childCheckAll[0].checked = false; }

                    }

                    // Desactivo el check "CheckAll" y los botones "Grabar" y "Cancelar"
                    SpanCheckAll.attr('disabled', 'disabled');
                    childCheckAll.attr('disabled', 'disabled');

                    table.attr('disabled', 'disabled');
                    SpansList.attr('disabled', 'disabled');
                    ChecksList.attr('disabled', 'disabled');

                    $('#' + btnEditar + '').show();
                    $('#' + btnGrabar + '').hide();
                    $('#' + btnCancelar + '').hide();

                }

                $(CboAnio).removeAttr('disabled');
                $(CboGrado).removeAttr('disabled');
                $(CboAula).removeAttr('disabled');  
                $(CboIdioma).removeAttr('disabled');

                e.preventDefault();
            }
        });

    });

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            $(".bt").mbFlipText(false);            
         
            var dataList = document.getElementById("<%= dl_NombreLibros.ClientID %>");
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var CboAnio = document.getElementById("<%= ddlBuscarAnio.ClientID %>");
            var CboGrado = document.getElementById("<%= ddlBuscarGrado.ClientID %>");
            var CboAula = document.getElementById("<%= ddlBuscarAula.ClientID %>");
            var CboIdioma = document.getElementById("<%= ddlBuscarIdioma.ClientID %>");

            function miFocus(index) {
                if (grid.rows.length > 0) {
                    row = grid.rows[index];
                    $("input:hidden[id$=hiddenMiGridviewIndex]").val(index); // Seteo el valor del indice de la fila en el gridview
                    $('#miPanelGridview').scrollTo($(row), 500);
                }
            }

            //Evento del checkbox "All"
            $('input:checkbox[id$=chkAll]', grid).click(function(e) {
                if (grid.rows.length > 0) {
                    var estadoCheck = $(this).attr('checked');
                    var rowIndex = $(this).closest('tr').prevAll().length;
                    var cell = grid.rows[rowIndex].cells[5];
                    var miDiv = $(cell).find('div')[0];
                    var child = $(miDiv).find('table')[0]; // CheckBoxList
                    var ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

                    if (estadoCheck == true) {
                        ChecksList.attr('checked', true);
                    } else {
                        ChecksList.removeAttr('checked');
                    }
                }
            });

            //Evento del DataList en sus "a href"
            $('a:[id$=linkModalLibro]', dataList).click(function(e) {

                //Cancel the link behavior
                e.preventDefault();

                //Get the A tag
                var Modal = $(this).attr('href');
                var LinkID = $(this).attr('id');

                var strTituloLibro = $('#' + LinkID + ' span:eq(1)').text();
                var strPrecioLibro = $('#' + LinkID + ' span:eq(3)').text();

                //Get the screen height and width
                var maskHeight = $(document).height();
                var maskWidth = $(window).width();

                //Set heigth and width to mask to fill up the whole screen
                $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

                //transition effect
                $('#mask').fadeIn("fast");
                $('#mask').fadeTo("fast", 0.9);

                //Get the window height and width
                var winH = $(window).height();
                var winW = $(window).width();

                //Set the popup window to center
                $(Modal).css('top', winH / 2 - $(Modal).height() / 2);
                $(Modal).css('left', winW / 2 - $(Modal).width() / 2);

                //$('#miTexto').text($(this).attr('id'));
                $('#miTituloLibroModal').text('Libro : ' + strTituloLibro);
                $('#miTituloLibro').text('Título : ' + strTituloLibro);
                $('#miPrecioLibro').text('Precio : ' + strPrecioLibro);

                //transition effect
                $(Modal).fadeIn("fast");

            });

            //if close button is clicked
            $('.window .close').click(function(e) {
                //Cancel the link behavior
                e.preventDefault();

                $('#mask').hide();
                $('.window').hide();
            });

            //Evento del boton "Editar"
            $('image:[id$=btnEditar]', grid).click(function(e) {
                if (grid.rows.length > 0) {

                    var rowIndex = $(this).closest('tr').prevAll().length;
                    var row;
                    var cellCheckAll;
                    var childCheckAll;
                    var SpanCheckAll;
                    var CheckAll;
                    var cell;
                    var miDiv;
                    var child;
                    var table;
                    var SpansList;
                    var ChecksList;
                    var cellButtons;
                    var miDivButtons;
                    var childEditar;
                    var childGrabar;
                    var childCancelar;
                    var btnEditar;
                    var btnGrabar;
                    var btnCancelar;

                    for (i = 0; i < grid.rows.length; i++) {

                        row = grid.rows[i];
                        cellCheckAll = grid.rows[i].cells[4];
                        SpanCheckAll = $(cellCheckAll).find('span');
                        childCheckAll = $(cellCheckAll).find('input:checkbox');

                        cell = grid.rows[i].cells[5];
                        miDiv = $(cell).find('div')[0];
                        child = $(miDiv).find('table')[0]; // CheckBoxList

                        if (child != undefined) {

                            table = $('table#' + $(child).attr('id'));
                            SpansList = $('table#' + $(child).attr('id')).find('span');
                            ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

                            // Botones
                            cellButtons = grid.rows[i].cells[3];
                            miDivButtons = $(cellButtons).find('div')[0];
                            childEditar = $(miDivButtons).children(0)[0];
                            childGrabar = $(miDivButtons).children(0)[1];
                            childCancelar = $(miDivButtons).children(0)[2];

                            btnEditar = childEditar.id;
                            btnGrabar = childGrabar.id;
                            btnCancelar = childCancelar.id;

                            if (i == rowIndex) {

                                $(row).css({ background: "#dcff7d" });

                                // Grabo los valores actuales del CheckBoxList previos a Editar
                                var cadena = '';
                                var miLength = 0;
                                var miIterador = 0;
                                miLength = ChecksList.length;
                                var miArray = new Array(miLength);

                                $.each(ChecksList, function(e, checkbox) {
                                    if (checkbox.checked) {
                                        miArray[miIterador] = "True";
                                    } else {
                                        miArray[miIterador] = "False";
                                    }
                                    miIterador += 1;
                                });

                                $("input:hidden[id$=hiddenLibrosEstadoActual]").val(miArray);

                                if (childCheckAll[0].checked) { $("input:hidden[id$=hiddenLibrosCheckAll]").val("True"); }
                                else { $("input:hidden[id$=hiddenLibrosCheckAll]").val("False"); }

                                // Activo el check "CheckAll" y los botones "Grabar" y "Cancelar" en la fila actual
                                SpanCheckAll.removeAttr('disabled');
                                childCheckAll.removeAttr('disabled');

                                table.removeAttr('disabled');
                                SpansList.removeAttr('disabled');
                                ChecksList.removeAttr('disabled');

                                $(this).hide();
                                $('#' + btnGrabar + '').show();
                                $('#' + btnCancelar + '').show();
                            } else {
                                // Oculto el check "CheckAll" y los botones "Editar","Grabar" y "Cancelar" para las otras filas
                                SpanCheckAll.attr('disabled', 'disabled');
                                childCheckAll.attr('disabled', 'disabled');

                                table.attr('disabled', 'disabled');
                                SpansList.attr('disabled', 'disabled');
                                ChecksList.attr('disabled', 'disabled');
                                $('#' + btnEditar + '').hide();
                                $('#' + btnGrabar + '').hide();
                                $('#' + btnCancelar + '').hide();
                            }
                            $(CboAnio).attr('disabled', 'disabled');
                            $(CboGrado).attr('disabled', 'disabled');
                            $(CboAula).attr('disabled', 'disabled');
                            $(CboIdioma).attr('disabled', 'disabled');
                        }
                    }
                    miFocus(rowIndex);
                    e.preventDefault();
                }
            });

            //Evento del boton "Grabar"
            $('input:image[id$=btnGrabar]', grid).click(function(e) {

                var estadoCheck = $(this).attr('checked');
                var rowIndex = $(this).closest('tr').prevAll().length;
                var cell = grid.rows[rowIndex].cells[5];
                var miDiv = $(cell).find('div')[0];
                var child = $(miDiv).find('table')[0]; // CheckBoxList
                var ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');

                var cadena = '';
                var miLength = 0;
                var miIterador = 0;
                miLength = ChecksList.length;
                var miArray = new Array(miLength);

                $.each(ChecksList, function(e, checkbox) {
                    if (checkbox.checked) {
                        miArray[miIterador] = "True";
                    } else {
                        miArray[miIterador] = "False";
                    }
                    miIterador += 1;
                });

                $("input:hidden[id$=hiddenLibrosPrestados]").val(miArray);
            });

            //Evento del boton "Cancelar"
            $('image:[id$=btnCancelar]', grid).click(function(e) {
                if (grid.rows.length > 0) {

                    var rowIndex = $(this).closest('tr').prevAll().length;
                    var row;
                    var cellCheckAll;
                    var childCheckAll;
                    var SpanCheckAll;
                    var CheckAll;
                    var cell;
                    var miDiv;
                    var child;
                    var table;
                    var SpansList;
                    var ChecksList;
                    var cellButtons;
                    var miDivButtons;
                    var childEditar;
                    var childGrabar;
                    var childCancelar;
                    var btnEditar;
                    var btnGrabar;
                    var btnCancelar;

                    for (i = 0; i < grid.rows.length; i++) {

                        row = grid.rows[i];
                        cellCheckAll = grid.rows[i].cells[4];
                        SpanCheckAll = $(cellCheckAll).find('span');
                        childCheckAll = $(cellCheckAll).find('input:checkbox');

                        cell = grid.rows[i].cells[5];
                        miDiv = $(cell).find('div')[0];
                        child = $(miDiv).find('table')[0]; // CheckBoxList

                        table = $('table#' + $(child).attr('id'));
                        SpansList = $('table#' + $(child).attr('id')).find('span');
                        ChecksList = $('table#' + $(child).attr('id')).find('input:checkbox');


                        // Botones
                        cellButtons = grid.rows[i].cells[3];
                        miDivButtons = $(cellButtons).find('div')[0];
                        childEditar = $(miDivButtons).children(0)[0];
                        childGrabar = $(miDivButtons).children(0)[1];
                        childCancelar = $(miDivButtons).children(0)[2];

                        btnEditar = childEditar.id;
                        btnGrabar = childGrabar.id;
                        btnCancelar = childCancelar.id;

                        if (i == rowIndex) {

                            $(row).css({ background: "#d3eefa" });
                            $(row).hover(
                                function() { $(this).css({ background: "#d3eefa" }); },
                                function() { $(this).css({ background: "#ffffff" }); }
                            );

                            // Reseteo los valores de los CheckBoxList
                            var miIterador = 0;
                            var cadena = '';
                            cadena = $("input:hidden[id$=hiddenLibrosEstadoActual]").val();
                            var miArray = cadena.split(",");

                            $.each(ChecksList, function(e, checkbox) {
                                if (miArray[miIterador] == "True") { checkbox.checked = true; }
                                else { checkbox.checked = false; }
                                miIterador += 1;
                            });

                            var valHiddenCheckAll = $("input:hidden[id$=hiddenLibrosCheckAll]").val();
                            if (valHiddenCheckAll == "True") { childCheckAll[0].checked = true; }
                            else { childCheckAll[0].checked = false; }

                        }

                        // Desactivo el check "CheckAll" y los botones "Grabar" y "Cancelar"
                        SpanCheckAll.attr('disabled', 'disabled');
                        childCheckAll.attr('disabled', 'disabled');

                        table.attr('disabled', 'disabled');
                        SpansList.attr('disabled', 'disabled');
                        ChecksList.attr('disabled', 'disabled');

                        $('#' + btnEditar + '').show();
                        $('#' + btnGrabar + '').hide();
                        $('#' + btnCancelar + '').hide();

                    }

                    $(CboAnio).removeAttr('disabled');
                    $(CboGrado).removeAttr('disabled');
                    $(CboAula).removeAttr('disabled');   
                    $(CboIdioma).removeAttr('disabled');

                    e.preventDefault();
                }
            });

        }
    }




    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_initializeRequest(initializeRequest);
    prm.add_endRequest(endRequest);

    var _postBackElement;

    function initializeRequest(sender, e) {

        if (prm.get_isInAsyncPostBack()) {
            e.set_cancel(true);
        }
        _postBackElement = e.get_postBackElement();

        if (_postBackElement.id.indexOf('ddlBuscarAnio') > -1 || _postBackElement.id.indexOf('ddlBuscarGrado') > -1 || _postBackElement.id.indexOf('ddlBuscarAula') > -1 || _postBackElement.id.indexOf('ddlBuscarIdioma') > -1) {

            var ddlGrado = document.getElementById("<%=ddlBuscarGrado.ClientID%>");
            var codGrado = ddlGrado.options[ddlGrado.selectedIndex].value;            
            if (codGrado > 0) {
                $find('ctl00_ContentPlaceHolder1_ModalPopupExtender2').show();
            }
        }
    }

    function endRequest(sender, e) {
        if (_postBackElement.id.indexOf('ddlBuscarAnio') > -1 || _postBackElement.id.indexOf('ddlBuscarGrado') > -1 || _postBackElement.id.indexOf('ddlBuscarAula') > -1 || _postBackElement.id.indexOf('ddlBuscarIdioma') > -1) {
            $find('ctl00_ContentPlaceHolder1_ModalPopupExtender2').hide();
        }
    }

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender2');
        modal.show();
    }

</script>     
  
</div>     

</asp:Content>

