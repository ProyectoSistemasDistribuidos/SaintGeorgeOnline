<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="frmReportesAdmision.aspx.vb" Inherits="ModuloAdmision_frmReportesAdmision" %>
<%@ OutputCache Location ="None" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 115px;
        }
        .milegend{
        margin: 0px 20px 0px 20px;  
        padding: 4px 0px 0px 10px;
        text-align:left;
        font-size: 11px;
        font-family: Arial;
        font-weight: bold;
        width: 470px;
        height: 21px;
        background: url(../App_Themes/Imagenes/legend_header.gif) repeat-x;
        border-left: solid 1px #707070;
        border-right: solid 1px #707070;
} 
        .style2
        {
            width: 128px;
        }
        .style3
        {
            width: 180px;
        }
        .style4
        {
            width: 67px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="height: 466px; width: 819px">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
    <fieldset>
    <legend class="milegend">
    Reportes admision
    </legend>
    
    
    <table style="width:100%;">
        <tr>
            <td class="style2">
                Anio academico</td>
            <td class="style1">
                <asp:DropDownList ID="ddlPeriodo" runat="server"
                     
                    style="width: 100px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td class="style4">
                Grado</td>
            <td class="style3">
<asp:DropDownList ID="ddlGrado" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;"
      >
</asp:DropDownList>  
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Exportar" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1">
               
                 &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</fieldset>
</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Button1" />
                </Triggers>
                </asp:UpdatePanel>
</div>

 <script>
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function beginReq(sender, args) {
            // muestra el popup
            $.blockUI({
                message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Editando...</h4>'
            });

            // $find(ModalProgress).show();
        }$(document).ready(function() {
            $("#menu").hide('fast');
        });

        function endReq(sender, args) {
            //  esconde el popup
           // $find(ModalProgress).hide();
            $.unblockUI();
        } 
    </script>
</asp:Content>

