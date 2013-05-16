<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ExoneracionCursos.aspx.vb" Inherits="Modulo_Matricula_ExoneracionCursos" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<style type="text/css">
               
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
</style>

<script type="text/javascript" >

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
      
</script>

<div id="miPaginaMantenimiento">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional"  >
    
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
                                    <td  style="padding-left :5px; width: 62px; height: 25px;" align="left" valign="middle">
                                        <span>Año :</span>
                                    </td>
                                    <td  style=" width: 50px; height: 25px;" align="left" valign="middle">
                                        <asp:DropDownList ID="ddlBuscarAnioAcademico" runat="server" Width="100px" OnSelectedIndexChanged="ddlBuscarAnioAcademico_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>  
                                        <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />                               
                                    </td>
                                     <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                              
                                     </td>
                                     <td colspan =6  style="width: 388px; height: 25px;" align="right" valign="middle">
                                            <%-- <asp:ImageButton ID="btnGrabar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                                onclick="btnGrabar_Click" />&nbsp;--%>
                                           <asp:ImageButton ID="btnCancelar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" ToolTip="Cancelar"
                                                onclick="btnCancelar_Click" CausesValidation="false"/>
                                           <asp:ImageButton ID="btnExportar" runat="server" Width="84" Height="19" ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                                                onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                                                onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'" ToolTip="Exportar"
                                                onclick="btnExportar_Click" CausesValidation="false"/>   
                                     </td>

                                   <%-- <td  style="width: 100px;  height: 25px;" align="right" valign="middle">
                                        
                                    </td>--%>
                                </tr>  
                               <tr>
                                              <td style="padding-left :5px; width: 62px; height: 25px;" align="left" valign="middle">
                                                  <span>Aulas :&nbsp;</span>
                                              </td>
                                              <td style="width: 320px; height: 25px;" align="left" valign="middle">
                                                <asp:DropDownList ID="ddlAulas" runat="server" Width="313px" style="font-size: 8pt; font-family: Courier New;" OnSelectedIndexChanged="ddlAulas_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList> 
                                                <asp:HiddenField ID="hiddenCodigoAnioAcademico" runat="server" Value="0" />
                                                <asp:HiddenField ID="hiddenCodigoGrado" runat="server" Value="0" />                                    
                                                <asp:HiddenField ID="hd_CodigoAlumno_pnl1" runat="server" Value="0" />                                    
                                             </td>
                                              <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                                <span>Nivel :&nbsp;</span>
                                             </td>
                                              <td style="width: 80px; height: 25px;" align="left" valign="middle">
                                                <asp:TextBox ID="tbNivel" runat="server" Width="70px" style="font-size: 8pt; font-family: Arial;" Enabled="false" />
                                             </td>         
                                              <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                                <span>Grado :&nbsp;</span>
                                             </td>                                                  
                                              <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                                <asp:TextBox ID="tbGrado" runat="server" Width="40px" style="font-size: 8pt; font-family: Arial;" Enabled="false"/>
                                             </td>
                                              <td style="width: 48px; height: 25px;" align="left" valign="middle">
                                                <span>Sección :&nbsp;</span>
                                              </td>
                                              <td style="width: 60px; height: 25px;" align="left" valign="middle">
                                                <asp:TextBox ID="tbSeccion" runat="server" Width="40px" style="font-size: 8pt; font-family: Arial;" Enabled="false"/>
                                              </td> 
                                               <td  style="width: 100px;  height: 25px;" align="right" valign="middle">
                                        
                                            </td>                             
                                          </tr>
                                          
                            </table>
                        </fieldset>
                    </div>
                         
                    <div class="miEspacio">
                    </div>
                          
                   <div id="">
                      <table  cellpadding="0" cellspacing="0" border="0" style="padding-left :10px; border: solid 0x red; width: 850px;">
                            <tr>
                                       <td align="left" valign="top">
                                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width:840px;">
                                                <tr>
                                                   <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid;    border-width:1px;  border-color:#a6a3a3;  width: 20px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    Order</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width: 100px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    Codigo Alumno</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width: 240px; height: 26px; text-align:left; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    Nombre Completo</td>
                                                    <td id="miHeaderDetCPago2" style="border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width: 70px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    Sección</td>
                                                    <td id="miHeaderDetCPago2" style=" border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width:310px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    Cursos Exonerados</td>
                                                    <td id="miHeaderDetCPago2" style=" border-bottom:solid; border-top:solid;   border-width:1px;  border-color:#a6a3a3;  width: 100px; height: 26px; text-align:center; color:White;font-size:10px; background-color:Teal; " align="center" >
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan ="6" style="border-bottom:solid; border-left:solid; border-right:solid;   border-width:1px;  border-color:#a6a3a3; ">
                                                   
                                                      <asp:GridView ID="GridView1" runat="server" 
                                                            Width="840px" 
                                                            CssClass="miGridviewBusqueda" 
                                                            GridLines="None" 
                                                            AutoGenerateColumns="False" 
                                                            AllowSorting="True"
                                                            EmptyDataText=" - No se encontraron resultados - "
                                                           OnRowDataBound="GridView1_RowDataBound"
                                                           OnRowCommand="GridView1_RowCommand"
                                                            ShowHeader="False"  >
                                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />   <%-- OnRowDataBound="GridView1_RowDataBound" --%>                                                                             
                                                            <Columns>     
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoMatricula" runat="server" Text='<%# Bind("CodigoMatricula") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>5
                                                                        <asp:Label ID="lblCodigoAnioAcademico" runat="server" Text='<%# Bind("CodigoAnioAcademico") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoGrado" runat="server" Text='<%# Bind("CodigoGrado") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("IdFila") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="30px" />
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField >                                                                      
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNombreAlumno" runat="server" Text='<%# Bind("NombreAlumno") %>' />
                                                                    </ItemTemplate>
                                                                   <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="230px" />
                                                                </asp:TemplateField>  
                                                                  <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAula" runat="server" Text='<%# Bind("Aula") %>' />
                                                                    </ItemTemplate>
                                                                   <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="120px" />
                                                                </asp:TemplateField>  
                                                                 <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/App_Themes/Imagenes/opc_agregar.png" 
                                                                            CommandName="AgregarRegistro" CommandArgument='<%# Bind("CodigoAlumno") %>' ToolTip="Agregar Registro" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                    <ItemStyle CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Center" Width="50px" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                       <asp:Label ID="lblCursosExonerados" runat="server" Text='<%# Bind("CursosExonerados") %>' />
                                                                    </ItemTemplate>
                                                                   <ItemStyle  CssClass="miGridviewBusqueda_Rows" HorizontalAlign="Left" Width="310px" />
                                                                </asp:TemplateField>  
                                                                  
                                                                <%--<asp:TemplateField>  
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlAula"  runat="server">
                                                                        </asp:DropDownList>                                                            
                                                                     </ItemTemplate>
                                                                   <ItemStyle CssClass="miGridviewBusqueda_Rows"  HorizontalAlign="Center" Width="120px" />
                                                                </asp:TemplateField>
                                                              <asp:BoundField DataField="CodigoAula" HeaderText="CodigoAula" ItemStyle-Width="0" HeaderStyle-CssClass="miHiddenStyle" ItemStyle-CssClass="miHiddenStyle" />                                
                       --%>
                                                              </Columns>  
                                                              
                                                        </asp:GridView>
                                                   
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                      </table>
                   </div>                 
                   
                                        
                                          
                </div>
            </ContentTemplate>
    </atk:TabPanel> 
        
    </atk:TabContainer>
      <atk:ModalPopupExtender ID="pnModalAgregarRegistro" runat="server"
                        TargetControlID="VerAgregarRegistro"
                        PopupControlID="pnlAgregarRegistro"
                        BackgroundCssClass="MiModalBackground" 
                        OkControlID="OKAgregarRegistro" 
                        CancelControlID="CancelAgregarRegistro"
                        Drag="True" 
                        PopupDragHandleControlID="AgregarRegistroHeader" DynamicServicePath="" 
                        Enabled="True" />           

                     <asp:panel id="pnlAgregarRegistro"  style="z-index:100" BackColor="White" BorderColor="Black" runat="server">
                         <table cellpadding="0" cellspacing="0" border="0" width="680px" id="panelRegistro">    
                                    <tr  id="AgregarRegistroHeader" style ="cursor :pointer; ">
                                        <td style="width: 560px; height: 26px" align="left" valign="middle" class="miGVBusquedaFicha_Header_V2" colspan="5">                
                                            <span style="padding-left:20px; font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">Agregar Registro</span>

                                        </td>
                                        <td style="width: 20px; height: 26px" align="right" valign="middle" class="miGVBusquedaFicha_Header_V2">
                                            <asp:ImageButton ID="btnCerraAgregarRegistro" runat="server" Width="16px" Height="15px"
                                                ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"
                                                onclick="btnCerraAgregarRegistro_Click" ToolTip="Cerrar Panel"/>
                                        </td>
                                    </tr>
                                    <tr>
                                       <td colspan="6" style="height: 15px; width:580px;" align="right">
                                       </td>
                                    </tr>
                                     <tr>
                                       <td style="width: 20px; " rowspan="10"></td>
                                         <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                            <span>Año :</span>
                                        </td>
                                        <td colspan="3" style="width:540px; height: 25px;" align="left" valign="middle">   
                                              <asp:Label ID="lblAnio_pnlReg" runat="server"  /> 
                                         </td>
                                      <%--<td style="width: 20px; " rowspan="10"></td>--%>
                                   </tr>
                                    <tr>
                                        
                                         <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                            <span>Aula :</span>
                                        </td>
                                        <td colspan="3" style="width:540px; height: 25px;" align="left" valign="middle">   
                                              <asp:Label ID="lblAula_pnlReg" runat="server" />
                                              <asp:Label ID="lblCodigoRegistroMeritosDemeritos_pnlReg" runat="server"  style="display:none " />  
                                         </td>
                                      <td style="width: 20px; " rowspan="10"></td>
                                   </tr> 
                                   <tr>
                                        <%--<td style="width: 20px; " rowspan="10"></td>--%>
                                         <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                            <span>Alumno :</span>
                                        </td>
                                        <td colspan="3" style="width:540px; height: 25px;" align="left" valign="middle">   
                                              <asp:Label ID="lblAlumno_pnlReg" runat="server"  /> 
                                         </td>
                                      <%--<td style="width: 20px; " rowspan="10"></td>--%>
                                   </tr>
                                    <tr>
                                         <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                            <span>Curso :</span>
                                        </td>
                                        <td colspan="3" style="width:540px; height: 25px;" align="left" valign="middle">   
                                           <asp:DropDownList ID="ddlCurso_pnlReg" runat="server" Width="313px" style="font-size: 8pt; font-family: Courier New;">
                                    </asp:DropDownList>
                                         </td>
                                   </tr>
                                    <tr>
                                      <td style="width: 100px; height: 25px;" align="left" valign="middle">
                                           
                                        </td>
                                       <td colspan ="3" style="width: 540px; height: 25px;" align="left" valign="middle">
                                              <asp:ImageButton ID="btn_GrabarExoneracion" runat="server" Width="74px" Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                                                    onclick="btn_GrabarExoneracion_Click" />
                                        </td>
                                   </tr>  
                                     <tr>
                                       <td colspan ="4" style="width: 100px; height: 25px;" align="left" valign="middle">
                                          
                                        </td>
                                   </tr>    
                                     
                                    <%--<tr><td colspan ="2"  style ="width: 640px; height: 25px; "></td>
                                    </tr>--%>      
                                </table>
                        
                          <div id="controlAgregarRegistro" style="display:none">
                                <input type="button" id="VerAgregarRegistro" runat="server" />
                                <input type="button" id="OKAgregarRegistro" />
                                <input type="button" id="CancelAgregarRegistro" />
                            </div>       
                        </asp:panel>    
    
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

