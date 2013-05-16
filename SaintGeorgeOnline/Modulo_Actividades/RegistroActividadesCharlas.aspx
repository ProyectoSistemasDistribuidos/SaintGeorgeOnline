<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="RegistroActividadesCharlas.aspx.vb" Inherits="Modulo_Actividades_RegistroActividadesCharlas" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" >

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
      
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="miPaginaMantenimiento" style="margin-left: 10px;">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
  <Triggers>
        <asp:PostBackTrigger ControlID="TabContainer1$miTab1$btnExportar" />
    </Triggers>

    <ContentTemplate>    
    
    <atk:TabContainer ID="TabContainer1" runat="server" Width="900px" ActiveTabIndex="0" AutoPostBack="false" ScrollBars="None" >
        <atk:TabPanel ID="miTab1" runat="server" HeaderText="Tab1" Enabled="true">
            <HeaderTemplate>
                <asp:Label ID="lbTab1" runat="server" Text="Confirmación de asistentes" />
            </HeaderTemplate>
            <ContentTemplate>    
            
    <table cellpadding="0" cellspacing="0" border="0" style="width: 882px; border: solid 0px red; margin: 0; padding: 0; font-size: 11px; font-family: Arial;">
      <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Actividad :</span>
            </td>    
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
               <asp:DropDownList ID="ddlActividad" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"   AutoPostBack="True" OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged">
                </asp:DropDownList> 
            </td>
            <td style="width: 522px; height: 25px;" align="center" valign="middle" colspan="2">
                    <span>Cantidad de personas confirmadas :</span>    <asp:Label ID="lbl_cantRegistrados" runat="server" Text="0" /> 
                    &nbsp;&nbsp;  <asp:ImageButton ID="btnActualizar" runat="server" Width="24px" Height="19px" 
                    ImageUrl="~/App_Themes/Imagenes/Download-icon.png"
                    onclick="btnActualizar_Click" 
                    ToolTip="Actualizar la cantidad de registrados"/>  
            </td>
        </tr>  
        
        <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Tipo Persona :</span>
            </td>    
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
                  <asp:RadioButtonList ID="rbTipo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Text="Familias" Selected="True"/>
                    <asp:ListItem Value="2" Text="Personal de Colegio" />
                     <asp:ListItem Value="3" Text="Invitados" />
                </asp:RadioButtonList>       
            </td>
            <td style="width: 522px; height: 25px;" align="center" valign="middle" colspan="2">
                  
            </td>
        </tr>   
                  
        <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Apellidos :</span>
            </td>    
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
                <asp:TextBox ID="tbFamilia" runat="server" style="width:270px; font-size: 8pt; font-family: Arial;" />
            </td>            
            <td style="width: 300px; height: 25px; border: solid 1px #FFFFFF" align="left" valign="middle">
                <asp:ImageButton ID="btnBuscar" runat="server" Width="74px" Height="19px" 
                    ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'" 
                    onclick="btnBuscar_Click" 
                    ToolTip="Buscar"/>   
            </td>
            <td style="width: 222px; height: 25px; border: solid 1px #FFFFFF" align="right" valign="middle">    
                   <asp:ImageButton ID="btnExportar" runat="server" Width="84px" Height="19px" 
                                            ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                            onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                            onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'"
                                            ToolTip="Exportar"
                                            OnClick="btnExportar_Click" />
                <asp:ImageButton ID="btnGrabar" runat="server" Width="74px" Height="19px" 
                    ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" 
                    onclick="btnGrabar_Click" 
                    ToolTip="Grabar"/>                            
            </td>
        </tr>               
        <tr>
            <td style="width: 880px; height: 25px;" align="left" valign="middle" colspan="4">    
    <div id="miGridviewMantActualizacion_Ficha" style="width: 882px; height: 26px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 882px; height: 26px; color:White; background-color: #0a0f14; 
            font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;"
        <tr>
            <td style="width:  30px; height: 26px;" align="center" valign="middle">
                <span>#</span>                                                                 
            </td>
            <%--<td style="width:  70px; height: 26px;" align="center" valign="middle">
                <span>Código</span>                                                                 
            </td>--%>
            <td style="width: 220px; height: 26px;" align="center" valign="middle">
                <span>Nombre Completo</span>                                                                 
            </td>
            <td style="width:  130px; height: 26px;" align="center" valign="middle">  
                 <span>Cantidad</span>   
            </td>
              <td style="width:  300px; height: 26px;" align="left" valign="middle">  
                 <span>Observación</span>   
            </td>
            <td style="width:  102px; height: 26px;" align="center" valign="middle"> 
              <span>Confirmación</span>   
            </td>                      
        </tr>
    </table>      
    </div> 
     
    <div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 882px; height: 295px; margin: 0; padding: 0;">          
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="865px"
            GridLines="None" 
            AutoGenerateColumns="False"
            AllowPaging="False" 
            AllowSorting="False"
            ShowFooter="false"
            ShowHeader="false"
            EmptyDataText=" - No se encontraron resultados - "           
            OnRowDataBound="GridView1_RowDataBound">
            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
            <Columns>     
              <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoConfirmacionAsistencia" runat="server" Text='<%# Bind("CodigoConfirmacionAsistencia") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                                
                  <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoProgramacionActividad" runat="server" Text='<%# Bind("CodigoProgramacionActividad") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                 <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoFamilia" runat="server" Text='<%# Bind("CodigoFamilia") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                   <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoTrabajador" runat="server" Text='<%# Bind("CodigoTrabajador") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoInvitado" runat="server" Text='<%# Bind("CodigoInvitado") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                 <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("Tipo") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                <asp:TemplateField ItemStyle-Width="30px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Bind("IdFila") %>'  />
                    </ItemTemplate>
                </asp:TemplateField> 
                                 
             <%--  <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigoFamilia" runat="server" Text='<%# Bind("CodigoFamilia") %>' />
                    </ItemTemplate>
                </asp:TemplateField> --%>                           
                                 
                <asp:TemplateField ItemStyle-Width="400px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblFamilia" runat="server" Text='<%# Bind("Familia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="tbCantidad" Width ="30px"  runat="server" Text='<%# Bind("Cantidad") %>' />
                         <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                            TargetControlID="tbCantidad" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="400px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="tbObservacion" Width ="350px" runat="server" Text='<%# Bind("Observacion") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            
              <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:CheckBox Width="100px" ID="chk_Asistencia" runat="server" RepeatDirection="Horizontal" CssClass="miClaseCheckBox">
                    </asp:CheckBox>    
                    </ItemTemplate>
                </asp:TemplateField>  
            </Columns>
        </asp:GridView>
    </div>
            </td>
        </tr>                                 
    </table>                              

            </ContentTemplate> 
        </atk:TabPanel>
        
        <atk:TabPanel ID="miTab2" runat="server" HeaderText="Tab2" Enabled="true">
            <HeaderTemplate>
                 <asp:Label ID="lbTab2" runat="server" Text="Registro de Asistentes" />
            </HeaderTemplate>
            <ContentTemplate>
                
                <table cellpadding="0" cellspacing="0" border="0" style="width: 882px; border: solid 0px red; margin: 0; padding: 0; font-size: 11px; font-family: Arial;">
      <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Actividad :</span>
            </td>    
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
               <asp:DropDownList ID="ddlActividadRegAsistentes" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"   AutoPostBack="True" OnSelectedIndexChanged="ddlActividadRegAsistentes_SelectedIndexChanged">
                </asp:DropDownList> 
            </td>
            <td style="width: 522px; height: 25px;" align="center" valign="middle" colspan="2">
                    <span>Cantidad de personas asistentes :</span>    <asp:Label ID="lblCantidadRegAsistentes" runat="server" Text="0" /> 
            </td>
        </tr>  
        
        <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Tipo Persona :</span>
            </td>    
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
                  <asp:RadioButtonList ID="rbTipoRegAsistentes" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Text="Familias" Selected="True"/>
                    <asp:ListItem Value="2" Text="Personal de Colegio" />
                    <asp:ListItem Value="3" Text="Invitados" />
                </asp:RadioButtonList>       
            </td>
            <td style="width: 522px; height: 25px;" align="center" valign="middle" colspan="2">
                  
            </td>
        </tr>   
                  
        <tr>
            <td style="width: 80px; height: 25px;" align="left" valign="middle">
                <span>Apellidos :</span>
            </td>    
            <td style="width: 280px; height: 25px;" align="left" valign="middle">
                <asp:TextBox ID="tbApellidoRegAsistentes" runat="server" style="width:270px; font-size: 8pt; font-family: Arial;" />
            </td>            
            <td style="width: 300px; height: 25px; border: solid 1px #FFFFFF" align="left" valign="middle">
                <asp:ImageButton ID="btnBuscarRegAsistentes" runat="server" Width="74px" Height="19px" 
                    ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                    onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'" 
                    onclick="btnBuscarRegAsistentes_Click" 
                    ToolTip="Buscar"/>   
            </td>
            <td style="width: 222px; height: 25px; border: solid 1px #FFFFFF" align="right" valign="middle">    
               <asp:ImageButton ID="btnGrabarRegAsistentes" runat="server" Width="74px" Height="19px" 
                    ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" 
                    onclick="btnGrabarRegAsistentes_Click" 
                    ToolTip="Grabar"/>                            
            </td>
        </tr>               
        <tr>
            <td style="width: 880px; height: 25px;" align="left" valign="middle" colspan="4">    
    <div id="miGridviewMantActualizacion_Ficha" style="width: 882px; height: 26px; margin: 0; padding: 0; border-bottom: 0;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 882px; height: 26px; color:White; background-color: #0a0f14; 
            font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;"
        <tr>
            <td style="width:  30px; height: 26px;" align="center" valign="middle">
                <span>#</span>                                                                 
            </td>
            <%--<td style="width:  70px; height: 26px;" align="center" valign="middle">
                <span>Código</span>                                                                 
            </td>--%>
            <td style="width: 220px; height: 26px;" align="center" valign="middle">
                <span>Nombre Completo</span>                                                                 
            </td>
            <td style="width:  170px; height: 26px;" align="center" valign="middle">  
                 <span>Cantidad</span>   
            </td>
              <td style="width:  115px; height: 26px;" align="left" valign="middle">  
                 <span># Asistentes</span>   
            </td>
            <td style="width:  247px; height: 26px;" align="center" valign="middle"> 
              <span>Asistencia</span>   
            </td>                      
        </tr>
    </table>      
    </div> 
     
    <div id="miGridviewMant" style="overflow-y: scroll; overflow-x: hidden; width: 882px; height: 295px; margin: 0; padding: 0;">          
        <asp:GridView ID="GridView2" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="865px"
            GridLines="None" 
            AutoGenerateColumns="False"
            AllowPaging="False" 
            AllowSorting="False"
            ShowFooter="false"
            ShowHeader="false"
            EmptyDataText=" - No se encontraron resultados - "           
            OnRowDataBound="GridView2_RowDataBound">
            <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
            <Columns>     
              <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoConfirmacionAsistenciaRegAsistentes" runat="server" Text='<%# Bind("CodigoConfirmacionAsistencia") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                                
                  <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoProgramacionActividadRegAsistentes" runat="server" Text='<%# Bind("CodigoProgramacionActividad") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                 <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoFamiliaRegAsistentes" runat="server" Text='<%# Bind("CodigoFamilia") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                   <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCodigoTrabajadorRegAsistentes" runat="server" Text='<%# Bind("CodigoTrabajador") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                 <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblCheckAsistioRegAsistentes" runat="server" Text='<%# Bind("CheckAsistio") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                 <asp:TemplateField>                                                                      
                    <ItemTemplate>
                        <div style="width: 0px; border: solid 0px red;">
                            <asp:Label ID="lblTipoRegAsistentes" runat="server" Text='<%# Bind("Tipo") %>' />
                        </div>   
                    </ItemTemplate>
                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                </asp:TemplateField> 
                
                <asp:TemplateField ItemStyle-Width="40px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndexRegAsistentes" runat="server" Text='<%# Bind("IdFila") %>'  />
                    </ItemTemplate>
                </asp:TemplateField> 
                                 
             <%--  <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigoFamilia" runat="server" Text='<%# Bind("CodigoFamilia") %>' />
                    </ItemTemplate>
                </asp:TemplateField> --%>                           
                                 
                <asp:TemplateField ItemStyle-Width="390px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblFamiliaRegAsistentes" runat="server" Text='<%# Bind("Familia") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="80px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                         <asp:Label ID="lblCantidadRegAsistentes" runat="server" Text='<%# Bind("Cantidad") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="240px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="tbCantidadRegAsistentes" Width ="30px"  runat="server" Text='<%# Bind("CantidadAsistentes") %>' />
                         <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
                                            TargetControlID="tbCantidadRegAsistentes" Enabled="True">
                                        </atk:FilteredTextBoxExtender>
                     </ItemTemplate>
                </asp:TemplateField>
            
              <asp:TemplateField ItemStyle-Width="300px" ItemStyle-CssClass="miGridviewBusqueda_Rows"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:CheckBox Width="100px" ID="chk_AsistenciaRegAsistentes" runat="server" RepeatDirection="Horizontal" CssClass="miClaseCheckBox">
                    </asp:CheckBox>    
                    </ItemTemplate>
                </asp:TemplateField>  
            </Columns>
        </asp:GridView>
    </div>
            </td>
        </tr>                                 
    </table>
                    
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

