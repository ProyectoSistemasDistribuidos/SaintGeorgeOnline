<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AgregarCaracteristicaPiel.aspx.vb" Inherits="Popups_AgregarCaracteristicaPiel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnl_PopUp_CaracteristicasPiel" BackColor="White" BorderColor="Black"
                                                                                                runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" border="0" width="460px">
                                                                                                    <tr id="CaracteristicasPielHeader" style="cursor: pointer;">
                                                                                                        <td style="width: 360px; height: 26px" colspan="4" align="center" class="miGVBusquedaFicha_Header">
                                                                                                            <span style="padding-left: 20px; font-weight: bold; font-size: 11px; font-family: Arial">
                                                                                                                Agregar Caracteristicas de la Piel</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Fecha de Registro&nbsp;</span>
                                                                                                            <asp:HiddenField ID="hdCodigoCaracteristicasPiel" runat="server" />
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px;" align="left" valign="middle">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                                                                                                <tr>
                                                                                                                    <td valign="middle" align="left" style="width: 110px; height: 25px;">
                                                                                                                        <asp:TextBox ID="tbFechaRegistroCaracteristicasPiel" runat="server" CssClass="miTextBoxCalendar"
                                                                                                                            Enabled="false" />
                                                                                                                             <atk:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                                                                                                                TargetControlID="tbFechaRegistroCaracteristicasPiel"
                                                                                                                                UserDateFormat="DayMonthYear"                                                                    
                                                                                                                                Mask="99/99/9999" 
                                                                                                                                MaskType="Date" 
                                                                                                                                PromptCharacter="-">
                                                                                                                            </atk:MaskedEditExtender> 
                                                                                                                                                                                                </td>
                                                                                                                    <td valign="middle" align="left" style="width: 250px; height: 25px;">
                                                                                                                        <asp:ImageButton Enabled="false" runat="server" ID="imageBF3" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
                                                                                                                            AlternateText="Elija una fecha del calendario" />
                                                                                                                        <atk:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbFechaRegistroCaracteristicasPiel"
                                                                                                                            PopupButtonID="imageBF3" Format="dd/MM/yyyy" CssClass="MyCalendar" Enabled="True" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 130px; height: 25px" align="left" valign="middle">
                                                                                                            <span style="padding-left: 10px">Tipo de carácteristicas :</span>
                                                                                                        </td>
                                                                                                        <td style="width: 230px; height: 25px" align="left">
                                                                                                            <asp:DropDownList ID="ddlCaracteristicaPiel" runat="server" Width="200px">
                                                                                                                <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 100px; height: 25px" align="left" valign="middle">
                                                                                                            <asp:ImageButton ID="btnAgregarTipoCaracteristica" runat="server" Width="84px" Height="19px"
                                                                                                                ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" 
                                                                                                                ToolTip="Agregar TipoCaracteristicas" />
                                                                                                                
                                                                                                               <%-- OnClick="btnAgregarTipoCaracteristica_Click"--%>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 360px; height: 25px" align="center" valign="middle" colspan="4">
                                                                                                            <asp:ImageButton ID="popup_btnAgregar_CaracteristicaPiel" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAceptar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAceptar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnAceptar_1.png'" 
                                                                                                                ToolTip="Aceptar" />&nbsp;
                                                                                                                
                                                                                                               <%-- OnClick="popup_btnAgregar_CaracteristicaPiel_Click"--%>
                                                                                                                
                                                                                                            <asp:ImageButton ID="popup_btnCancelar_CaracteristicaPiel" runat="server" Width="84px"
                                                                                                                Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnCancelar_2.png'"
                                                                                                                onmouseout="this.src = '../App_Themes/Imagenes/btnCancelar_1.png'" 
                                                                                                                ToolTip="Cancelar" />
                                                                                                                
                                                                                                                <%--OnClick="popup_btnCancelar_CaracteristicaPiel_Click"--%>
                                                                                                                
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="3" height="10px">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div id="Div1" style="display: none">
                                                                                                    <input type="button" id="OKCaracteristicaPiel" />
                                                                                                    <input type="button" id="CancelCaracteristicaPiel" />
                                                                                                </div>
                                                                                            </asp:Panel>
    </form>
</body>
</html>
