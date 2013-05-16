<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Interfaz_Familia_CompromisoPagos_Default" title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="PopupModal" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    GridLines="Horizontal" Width="845px" OnRowDataBound="GridView1_RowDataBound" OnRowCommand ="GridView1_RowCommand" >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnActualizar" runat="server" 
                                    CommandArgument='<%# Bind("CodigoAlumno") %>' CommandName="Actualizar" 
                                    ImageUrl="~/App_Themes/Imagenes/opc_actualizar.png" 
                                    ToolTip="Actualizar Registro" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnCancelar" runat="server" 
                                    CommandArgument='<%# Bind("CodigoAlumno") %>' CommandName="Cancelar" 
                                    ImageUrl="~/App_Themes/Imagenes/opc_cancelar.png" ToolTip="Cancelar Registro" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnActivar" runat="server" 
                                    CommandArgument='<%# Bind("CodigoAlumno") %>' CommandName="Activar" 
                                    ImageUrl="~/App_Themes/Imagenes/opc_activar.png" ToolTip="Activar Registro" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CodigoAlumno">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CodigoAlumno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DescConceptoCobro">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("DescConceptoCobro") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monto">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FechaEmisionStr">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("FechaEmisionStr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FechaVencimientoStr">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("FechaVencimientoStr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alumno">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlAlumno" runat="server" AutoPostBack="True" >
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                
                <atk:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                TargetControlId = "VerRegistroAlumno"
                PopupControlId = "pnlRegistroAlumno"
                BackgroundCssClass ="FondoAplicacion"
                DropShadow ="True"
                OkControlId ="OkRegistroAlumno"
                CancelControlId ="CancelRegistroAlumno"
                Drag ="True"
                PopupDragHandleControlId ="RegistroAlumnoHeader"
                DynamicServicePath =""
                Enabled ="True"
                >
                </atk:ModalPopupExtender>
                
                    <asp:Panel ID="pnlRegistroAlumno" BackColor="White" BorderColor ="Black" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <span id="RegistroAlumnoHeader" style="padding-left: 20px; font-weight: bold; font-size: 11px;
                                        font-family: Arial; cursor: pointer;">Nuevo Registro</span>
                                </td>                                                             
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCodAlumno" runat="server" Text="Codigo Alumno"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodAlumno" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblDescConceptoCobro" runat="server" Text="Concepto Cobro"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescConcepCobro" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblMonto" runat="server" Text="Monto"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblFechaEmision" runat="server" Text="FechaEmisión"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaEmision" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVencimiento" runat="server" Text="FechaVencimiento"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVencimiento" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMonto1" runat="server" Text="cboMonto"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonto1" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                 <td>
                                     <asp:Button ID="Button2" runat="server" Text="Salir" />
                                </td>
                            </tr>
                        </table>
                        <div id="ControlRegistroAlumno">
                            <input type="button" id="VerRegistroAlumno" runat ="server" />
                            <input type="button" id="OkRegistroAlumno" />
                            <input type="button" id="CancelRegistroAlumno" />
                        </div>
                    </asp:Panel>
               
             
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

