<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgregarRetiro.aspx.vb" Inherits="Popups_AgregarRetiro" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="*" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Retiros</title>

    <!--Archivos de Javascripts -->
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/sexyalertbox.v1.2.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.blockUI.js"></script>       
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jcarousellite_1.0.1c4.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.sexylightbox.v2.3.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.colorbox.js"></script>
    
	<!--Archivos de Estilos -->
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/styleAlertas.css" />   
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/sexyalertbox.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/misEstilos.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/sexylightbox.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/colorbox.css" />
    <link rel="stylesheet" type="text/css" href="/SaintGeorgeOnline/App_Themes/Estilos/styles_master.css"  />  
    <link rel="stylesheet" type="text/css" media="all" href="/SaintGeorgeOnline/App_Themes/Estilos/miCalendario.css" />       
    
</head>
<body>
    <form id="form1" runat="server">
    
    <atk:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </atk:ToolkitScriptManager>    

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>

                <div style="border: solid 0px blue; width: 650px;">
                    <div id="miDetalleMant">
                        <fieldset>
                            <legend>Datos del Registro</legend>
                            <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; min-width: 610px;">                                
                                <tr>
                                    <td colspan="2" style="height: 15px;" align="right">
                                        <em>Campos Obligatorios (*)</em>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Año Académico :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbAnioAcadenmico" runat="server" CssClass="miTextBox" Width="450px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Fecha de Registro :&nbsp;</span><span class="camposObligatorios">(*)</span>                                      
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbFecha" runat="server" CssClass="miTextBox" Width="450px" MaxLength="300" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Situación :&nbsp;</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbSituacion" runat="server" CssClass="miTextBox" Width="450px" MaxLength="300" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width: 150px; height: 25px;" align="left" valign="middle">
                                        <span>Motivo :</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="min-width: 460px; height: 25px;" align="left" valign="bottom">
                                        <asp:TextBox ID="tbMotivo" runat="server" CssClass="miTextBox" Width="450px"  MaxLength="300" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px; height: 25px" align="left">
                                        <span>Colegio Procedencia :</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="width: 460px; height: 25px" align="left" colspan="2" valign="middle">
                                        <asp:DropDownList ID="ddlProcedencia" runat="server" Width="255px"
                                            >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                            
                                <tr>
                                    <td style="width: 150px; height: 25px" align="left">
                                        <span> Código Modular :</span><span class="camposObligatorios">(*)</span>
                                    </td>
                                    <td style="width: 460px; height: 25px" align="left" colspan="2" valign="middle">
                                        <asp:DropDownList ID="ddlCodigoModular" runat="server" Width="255px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                    
                            </table>
                        </fieldset>
                    </div>    
                    <div class="miEspacio"></div>            
                    <div id="miFooterDetalleMant">
                        <asp:ImageButton ID="btnGrabar" runat="server" Width="74" Height="19" ImageUrl="~/App_Themes/Imagenes/btnGrabar_1.png"
                            onmouseover="this.src = '../App_Themes/Imagenes/btnGrabar_2.png'" 
                            onmouseout="this.src = '../App_Themes/Imagenes/btnGrabar_1.png'" ToolTip="Grabar"
                            onclick="btnGrabar_Click" />
                    </div>          
                </div>     
                
            </ContentTemplate>
        </asp:UpdatePanel>
            
    </form>
</body>
</html>
