<%@ Page Language="VB" MasterPageFile="~/Interfaz_Familia/Plantilla_Principal.master" AutoEventWireup="false" CodeFile="CompanierosHijo.aspx.vb" Inherits="Interfaz_Familia_Modulo_DatosHijos_CompanierosHijo" title="Página sin título" %>

<%@ MasterType VirtualPath="/SaintGeorgeOnline/Interfaz_Familia/Plantilla_Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery-1.4.1.js"></script>      
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.pimg.js"></script>    

<style type="text/css">

	#pimg 
	{
	    z-index: 999999;
	    display: none;
	    position: absolute;
	    background-color: #FFFFFF;
	    font-family: Arial;
	    font-size: 12px;
	    font-weight: bold; 
	    border: solid 10px black;
	    padding: 10px;
    }
    .contenedor
    {
        border: solid 1px #41576f; 
        width: 720px; 
        height:auto; 
        background: url(/SaintGeorgeOnline/App_Themes/imagenes/bgtest.gif) no-repeat        
    }
    .tablita
    {
        border: solid 1px black; 
        /*background-color: #FFFFFF;*/ 
        font-family: Arial; 
        font-size: 10px; 
        font-weight: bold;               
    }   
    .datos
    {            
        background-color: #FFFFFF;
        filter: alpha(opacity=75);
        opacity: 0.8;    
        font-family: Arial; 
        font-size: 10px; 
        font-weight: bold;
    }

</style>	

<script type="text/javascript">
    $(document).ready(function() {
        pimg();
    });
</script>    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="contenedor">

        <asp:DataList ID="DataList1" runat="server" 
            RepeatColumns="4" 
            RepeatDirection="Horizontal"             
            RepeatLayout="Table"
            CellPadding="0"
            CellSpacing="5">  
       
        <ItemTemplate>
            <table style="width: 170px; height: 70px;" class="tablita"  border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50px; height: 70px; border-right: solid 1px black; background-color: #FFFFFF;" 
                        align="center" valign="top" rowspan="3">
                        <img id="foto" runat="server" style="border:0 ; width:50px; height: 70px;" />
                    </td>
                    <td style="width: 120px; height: 30px;" align="left" valign="middle" class="datos">
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("nombre") %>' style="padding-left:5px;" />
                    </td>
                </tr>   
                <tr>
                    <td style="width: 120px; height: 20px;" align="left" valign="middle">
                        
                        <table style="width: 120px; height: 20px;" border="0" cellpadding="0" cellspacing="0" class="datos">
                            <tr>
                                <td style="width:30px; height:20px;" align="center" valign="middle">
<img alt="Cumpleanios" src="http://www.astrosafari.com/styles/astrosafari/imageset/icon_birthday.gif" style="border:0; width:20px; height:20px" />                               
                                </td>
                                <td style="width:90px; height:20px;" align="left" valign="middle">
<asp:Label ID="Label2" runat="server" Text='<%# Bind("cumpleanios") %>' />                                
                                </td>
                            </tr>
                        </table>
                    
                    </td>
                </tr>    
                <tr>
                    <td style="width: 120px; height: 20px;" align="left" valign="middle">

                        <table style="width: 120px; height: 20px;" border="0" cellpadding="0" cellspacing="0" class="datos">
                            <tr>
                                <td style="width:30px; height:20px;" align="center" valign="middle">
<img alt="Telefono" src="http://labacana.mobi/iphone%20icon_256.png" style="border:0; width:20px; height:20px" />                               
                                </td>
                                <td style="width:90px; height:20px;" align="left" valign="middle">
<asp:Label ID="Label3" runat="server" Text='<%# Bind("telefono") %>' />                                
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>          
            </table>
        </ItemTemplate>      
        </asp:DataList>

    </div>
    
</asp:Content>

