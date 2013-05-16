<%@ Page Language="VB" AutoEventWireup="false" CodeFile="formatoRegistroActividades.aspx.vb" Inherits="Modulo_Actividades_formatoRegistroActividades" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
body{
    padding:0; 
    margin:10;
    font-family: Arial, Helvetica, sans-serif;
    }        
h1{text-align: right}
       
.miboton{
    text-decoration: none; 
    background-color: #4b8efa; 
    color: #ffffff; 
    border: 0;
    font-size: 12px;  
    display: block; line-height: 20px; float: left;
    padding: 0 5px; 
    margin: 0 5px 0 0;
    width: 80px;
    vertical-align: middle; text-align: center;   
    border: solid 1px #3079ed;   
    }
.miboton:hover{
    text-decoration: none; 
    background-color: #0072bb;
    color: #ffffff; 
    border: 0;font-size: 12px; 
    display: block; line-height: 20px; float: left; 
    padding: 0 5px; 
    margin: 0 5px 0 0;
    width: 80px;
    vertical-align: middle; text-align: center;   
    border: solid 1px #2f5bb7; 
    } 
    </style>
    
    <script type="text/javascript">
        function ImpresionFicha() {
            window.open('/SaintGeorgeOnline/Modulo_Actividades/impresionRegistroActividades.aspx', '_blank', 'width=750,height=800');
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <h1>Document Preview</h1>
    <h5>Type of Document:</h5>
    
    <asp:DropDownList ID="ddlFormato" runat="server">
        <asp:ListItem Text="Coordinación de actividades" Value="1"></asp:ListItem>
        <asp:ListItem Text="Informe de actividades" Value="2"></asp:ListItem>
    </asp:DropDownList>
    
    <asp:HiddenField ID="hiddenActividad" runat="server" Value="0" />
    <br />
<asp:LinkButton ID="btnWord" runat="server" ToolTip="Word" Text="Word" class="miboton" OnClick="btnWord_Click" />      
&nbsp;&nbsp;&nbsp;
<asp:LinkButton ID="btnHTML" runat="server" ToolTip="HTML" Text="HTML" class="miboton" OnClick="btnHTML_Click" />  
    
    </div>
    </form>
</body>
</html>
