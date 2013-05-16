<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReplicacion.aspx.vb" Inherits="Modulo_Enfermeria_frmReplicacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script>
         Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
         function beginReq(sender, args) {
             // muestra el popup
             $.blockUI({
                 message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" /> Editando...</h4>'
             });

             // $find(ModalProgress).show();
         } $(document).ready(function() {
             $("#menu").hide('fast');
         });

         function endReq(sender, args) {
             //  esconde el popup
             // $find(ModalProgress).hide();
             $.unblockUI();
         } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="Button1" runat="server" Text="Button" />
            </ContentTemplate>
        </asp:UpdatePanel>
    
        
    
    </div>
    </form>
    
    
</body>
</html>
