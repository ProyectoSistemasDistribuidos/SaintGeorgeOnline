<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="RestriccionMatricula.aspx.vb" Inherits="Mantenimientos_Matricula_RestriccionMatricula" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style type="text/css">
        .style1
        {
            width: 220px;
            height: 26px;
        }
        .style2
        {
            width: 100px;
            height: 26px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
 <div id="miPaginaMantenimiento">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate> 
                <div id="miPaginaMantenimiento">
                    <div style="border: solid 0px blue; width: 650px;">
                       <div id="miBusquedaMant">
                            <fieldset>
                                <legend>Criterios de busqueda</legend>
                                   <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red;
                                    min-width: 550px;">
                                    <tr>
                                        <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                            <span>Apellido Paterno</span>
                                        </td>
                                        <td style="width: 400px; height: 25px;"  align="left" valign="middle">
                                            <asp:TextBox ID="tbBuscarApellidoPaterno" runat="server" CssClass="miTextBox" Width="320px" MaxLength="100" Height="18px" />
                                            <asp:HiddenField ID="hfTotalRegs" runat="server" Value="0" />
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="tbBuscarApellidoPaterno" 
                                                ValidChars="' ','.','á','é','í','ó','ú','(',')'" Enabled="True">
                                            </atk:FilteredTextBoxExtender>
                                        </td>
                                        <td style="width: 30px; padding-top:4px" align="right" valign="top" >
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="btnBuscar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnBuscar_1.png"
                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnBuscar_2.png'" 
                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnBuscar_1.png'"
                                                        onclick="btnBuscar_Click" ToolTip="Buscar Registros"/><br /><br />
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                            <span>Apellido Materno</span>
                                        </td>
                                        <td style="width: 400px; height: 25px;" align="left" valign="middle">
                                            <asp:TextBox ID="tbBuscarApellidoMaterno" runat="server" CssClass="miTextBox" 
                                                Width="320px" MaxLength="100" Height="18px" />
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                                                FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="tbBuscarApellidoMaterno" 
                                                ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                            </atk:FilteredTextBoxExtender>                                     
                                        </td>
                                         <td style="min-width: 30px; height: 25px;" align="right" valign="top"  >
                                          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                   <asp:ImageButton ID="btnLimpiar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnLimpiar_1.png"
                                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnLimpiar_2.png'" 
                                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnLimpiar_1.png'"
                                                                    onclick="btnLimpiar_Click" ToolTip="Limpiar Filtros"/>     
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                       </td> 
                                    </tr>
                                    <tr>
                                        <td style="width: 180px; height: 25px;" align="left" valign="middle">
                                            <span>Nombre</span>
                                        </td>
                                        <td  style="min-width: 400px; height: 25px;" align="left" valign="middle">
                                            <asp:TextBox ID="tbBuscarNombre" runat="server" CssClass="miTextBox" 
                                                Width="320px" MaxLength="100" Height="18px" />
                                            <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" 
                                                FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="tbBuscarNombre" 
                                                ValidChars="' ','á','é','í','ó','ú','(',')'" Enabled="True">
                                            </atk:FilteredTextBoxExtender>                                     
                                        </td>
                                          <td style="min-width: 30px; height: 25px;" align ="right"  >
                                             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                     <asp:ImageButton ID="btnGrabar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                                                                    onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                                                                    onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" 
                                                                    onclick="btnGrabar_Click" ToolTip="Grabar" />  
                                                 </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                       
                                    </tr> 
                                    <tr>
                                            <td style="width:180px; height: 25px;" align="left" valign="middle">
                                                <span>Grado</span>
                                            </td>
                                            <td style="width: 400px; height: 25px;" align="left" valign="middle">
                                                <asp:DropDownList ID="ddlGrados" runat="server" Width="100px" AutoPostBack="true" >
                                                </asp:DropDownList>                                             
                                            </td>
                                             <td style="min-width: 30px; height: 25px;" align ="right"  >
                                                 <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                         <asp:ImageButton ID="btnCancelar" runat="server" Width="74" Height="19" 
                                                                        ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png"
                                                                        onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'" 
                                                                        onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                                        onclick="btnCancelar_Click" ToolTip="Cancelar" />  
                                                     </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                   </tr>
                                                                                                              
                                </table>
                            </fieldset>
                        </div>
                        
                        <div class="miEspacio">
                        </div>                    
                                        
                        <div class="miEspacio">
                        </div>
                        
                        <div style=" margin: 0px 10px 0px 10px;  padding: 0; width: 630px; border: solid 1px #000000 ">
                         <table cellpadding="0" cellspacing="0"    width="630px">
                            <tr>
                                <td rowspan ="2" 
                                    style="width:290px; text-align:center; color:White;font-size:14px;font-weight: bold; background-color:#003955;border-right : solid 1px #000000"
                                    align="center "  >
                                    Nombre de Alumno</td>
                                <td colspan="2" style="width:180px; height: 26px;  color:White;font-size:14px; font-weight: bold; background-color:#003955;border-bottom: solid 1px #000000;"
                                 align="center" >
                                    Debe Libro a Biblioteca</td>
                            </tr>
                            <tr>    
                                <td style="width: 60px; height: 26px; color:White;font-size:10px; background-color:#003955; border-right :solid 1px #000000;" align="center"   >
                                    opcion</td>
                                <td style="width: 200px; height: 26px; color:White;font-size:10px;background-color:#003955;" align="center" >
                                    Descripcion</td>
                             
                            </tr>
                          </table> 
                        </div>
                        <div  style=" margin: 0px 10px 0px 10px;  padding: 0; width: 630px; border-bottom: solid 1px #000000;border-left : solid 1px #000000;border-right: solid 1px #000000 ">
                          
                                        <asp:gridView ID="GV_DinamicoAlumno" runat="server" 
                                            CssClass="miGridviewBusqueda" 
                                            GridLines="Both" 
                                            AutoGenerateColumns="False"
                                            AllowPaging="True" 
                                            AllowSorting="True"
                                            EmptyDataText=" - No se encontraron resultados - " ShowHeader="False">
                                           
                                            <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />
                                            <PagerStyle CssClass="miGridviewBusqueda_Footer" HorizontalAlign="Center" />                                                                                 
                                            <Columns>    
                                                 
                                                 <asp:TemplateField > 
                                                  <ItemTemplate>
                                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
                                                  </ItemTemplate>                                                    
                                                <ItemStyle CssClass="miHiddenStyle" HorizontalAlign="Left" Width="0px" />
                                                </asp:TemplateField>
                                                      
                                                <asp:TemplateField >  
                                                   <ItemTemplate>
                                                        <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' />
                                                    </ItemTemplate>
                                                  
                                                    <ItemStyle  CssClass="GridMotivoRestriccion_Rows" HorizontalAlign="Left" Width="400px" />
                                                </asp:TemplateField>
                                                
                                                  <asp:TemplateField >  
                                                                                                                     
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="lblOpcion" runat="server" Checked='<%# Bind("Opcion") %>' />
                                                    </ItemTemplate>
                                                  
                                                     <ItemStyle CssClass="GridMotivoRestriccion_Rows" HorizontalAlign="Center" Width="83px" />
                                                </asp:TemplateField>
                                                
                                                  <asp:TemplateField >  
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblObservacion1" runat="server" Text='<%# Bind("Observacion") %>' Width ="200" Height="30px" Rows="2"  TextMode="MultiLine" />
                                                    </ItemTemplate>
                                                   <ItemStyle CssClass="GridMotivoRestriccion_Rows" HorizontalAlign="Center" Width="234px" />
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        
                                        </asp:gridView>
                         </div>
                                    <div class="miEspacio">
                                    </div>
                                   <%-- <div id="GVLegenda_Alumno">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 830px;">
                                            <tr>
                                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                    <img alt="Actualizar Registro" src="../App_Themes/Imagenes/opc_actualizar.png"/></td>
                                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                    <span>Actualizar Registro</span></td>                              
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span>|</span></td>      
                                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                    <img alt="Eliminar Registro" src="../App_Themes/Imagenes/opc_ver.png"/></td>
                                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                    <span>Ver Registro</span></td>  
                                                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                                                    <span>|</span></td>                                    
                                                <td style="width: 30px; height: 26px;" align="center" valign="middle">
                                                    <img alt="Activar Registro" src="../App_Themes/Imagenes/opc_printer.png"/></td>
                                                <td style="width: 100px; height: 26px;" align="left" valign="middle">
                                                    <span>Imprimir Registro</span></td>                                      
                                                <td style="width: 200px"></td>                                                                     
                                            </tr>                        
                                        </table>
                                    </div>  --%>
                             
                            
                      
                  
                    </div>
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

