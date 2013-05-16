<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="Panel_Configuraciones_Enfermeria.aspx.vb" Inherits="Mantenimientos_Enfermeria_MantenimientoEnfermeria" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <style type="text/css">
        .ListaItems{
            
            margin : -30;
            padding: -30;
            list-style: none;
            cursor: pointer;
            color: black;
        }  
        
    </style>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"   >
        
    <ContentTemplate>
    
        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10px; color: #000080">
                    <div>
                        <asp:BulletedList ID="bl_Config_fa" runat="server" DisplayMode="HyperLink" 
                        style="margin-left: -30px">
                        </asp:BulletedList>                    
                    </div>
                    
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

