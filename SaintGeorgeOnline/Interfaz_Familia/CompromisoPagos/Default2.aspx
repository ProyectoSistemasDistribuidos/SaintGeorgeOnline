<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Interfaz_Familia_CompromisoPagos_Default2" title="Página sin título" %>

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
    <table>
    <tr>
        <td>
            <asp:Button ID="Button1" runat="server" Text="AgregarPopupModal" Visible ="true"  />
        </td>
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
                    <asp:TextBox ID="txtMonto1" runat="server"></asp:TextBox>
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
    </table>
    
</asp:Content>

