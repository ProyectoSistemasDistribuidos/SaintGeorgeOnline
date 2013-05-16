<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ingresarMedicamento.ascx.vb" Inherits="Controles_ingresarMedicamento" %>

<style type="text/css">
    .modalBackground
    {
        background-color: black;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    #panelRegistro span
    {
        font-size: 11px;
        font-family: Arial;
    }
    #panelRegistro em
    {
        font-size: 10px;
        font-family: Arial;
        color: #a51515;
        margin-right: 7px;
        padding: 0;
    }
    #panelRegistro .header
    {
        background: #0a0f14 url(/SaintGeorgeOnline/App_Themes/Imagenes/legend_header.gif) repeat-x;
        text-align: left;
        color: black;
        height: 26px;
        border-bottom: solid 1px black;
    }
</style>   


<script type="text/javascript">

    function ValidarLength(textareaControl, maxlength) {
        if (textareaControl.value.length > maxlength) {
            textareaControl.value = textareaControl.value.substring(0, maxlength);
        }
    }
    
</script>
    

<asp:UpdatePanel ID="UpdatePanel_IngresarMedicamento" runat="server">
    <ContentTemplate>
        <atk:ModalPopupExtender ID="ModalPopupExtender_IngresarMedicamento" runat="server"
            PopupControlID="Panel_IngresarMedicamento" TargetControlID="btnVer_IngresarMedicamento"
            OkControlID="btnOK_IngresarMedicamento" CancelControlID="btnCancel_IngresarMedicamento"
            BackgroundCssClass="modalBackground"  Drag="True" PopupDragHandleControlID="dragCtrl_IngresarMedicamento"
            Enabled="True" />
        <asp:Panel ID="Panel_IngresarMedicamento" BackColor="White" BorderColor="Black" BorderWidth="1"
            runat="server" Style="width: 550px; display: none;">
            <table cellpadding="0" cellspacing="0" border="0" id="panelRegistro" style="width: 550px;">
                <tr>
                    <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                        <span style="width: 30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td style="width: 490px; height: 26px; cursor: pointer;" align="left" valign="middle"
                        class="header" colspan="3" id="dragCtrl_IngresarMedicamento">
                        <span style="font-weight: bold; font-size: 11px; font-family: Arial; cursor: pointer;">
                            <asp:Label ID="lbltitulo" runat="server" Text="Registro de Medicamentos" />
                        </span>
                    </td>
                    <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                        <asp:ImageButton ID="btnCerrar_IngresarMedicamento" runat="server" Width="16px" Height="15px"
                            ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" OnClick="btnCerrar_IngresarMedicamento_Click"
                            ToolTip="Cerrar Panel" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:HiddenField ID="hiddenComboPadreID" runat="server" Value="" />
                        <asp:HiddenField ID="hiddenModalPadreID" runat="server" Value="" />
                        <asp:HiddenField ID="hiddenTieneModalPadreID" runat="server" Value="false" />
                        <asp:HiddenField ID="hiddenCodigoRegistro" runat="server" Value="0" />
                        <asp:HiddenField ID="hiddenCodigoNombreMedicamento" runat="server" Value="0" />
                        <asp:HiddenField ID="hiddenCodigoPresentacion" runat="server" Value="0" />
                        <asp:HiddenField ID="hiddenCodigoUnidad" runat="server" Value="0" />
                        <asp:HiddenField ID="hiddenTieneControlesAuxiliares" runat="server" Value="false" />
                        <asp:HiddenField ID="hiddenControlAuxiliarLabelID" runat="server" Value="" />
                        <asp:HiddenField ID="hiddenControlAuxiliarTextBoxID" runat="server" Value="" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px;" rowspan="7">
                    </td>
                    <td style="width: 490px; height: 25px;" valign="middle" align="right" colspan="3">
                        <em>Campos Obligatorios (*)</em>
                    </td>
                    <td style="width: 30px;" rowspan="7">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                        <span>Nombre&nbsp;</span><span class="camposObligatorios">(*)</span>
                    </td>
                    <td style="width: 250px; height: 25px;" align="left" valign="middle">
                        <asp:DropDownList ID="ddlNombre" runat="server" Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 110px; height: 25px;" align="left" valign="middle">
                        <asp:ImageButton ID="btnAgregarRegistroNombreMedicamento" runat="server" Width="84px"
                            Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                            onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnAgregarRegistroNombreMedicamento_Click"
                            ToolTip="Agregar Nuevo Nombre de Medicamento" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                        <span>Concentración&nbsp;</span>
                    </td>
                    <td style="width: 250px; height: 25px;" align="left" valign="middle">
                        <asp:TextBox ID="tbConcentracion" runat="server" CssClass="miTextBox" Width="150px"
                            MaxLength="100" />
                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                            TargetControlID="tbConcentracion" ValidChars="' ','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'">
                        </atk:FilteredTextBoxExtender>
                    </td>
                    <td style="width: 110px; height: 25px;" align="left" valign="middle">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                        <span>Presentación&nbsp;</span><span class="camposObligatorios">(*)</span>
                    </td>
                    <td style="width: 250px; height: 25px;" align="left" valign="middle">
                        <asp:DropDownList ID="ddlPresentacion" runat="server" Width="240px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPresentacion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 110px; height: 25px;" align="left" valign="middle">
                        <asp:ImageButton ID="btnAgregarRegistroPresentacion" runat="server" Width="84px"
                            Height="19px" ImageUrl="~/App_Themes/Imagenes/btnAgregar_1.png" onmouseover="this.src = '../App_Themes/Imagenes/btnAgregar_2.png'"
                            onmouseout="this.src = '../App_Themes/Imagenes/btnAgregar_1.png'" OnClick="btnAgregarRegistroPresentacion_Click"
                            ToolTip="Agregar Nueva Presentación de Medicamento" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                        <span>Cantidad&nbsp;</span>
                    </td>
                    <td style="width: 250px; height: 25px;" align="left" valign="middle">
                        <asp:TextBox ID="tbCantidad" runat="server" CssClass="miTextBox" Width="150px" MaxLength="10" />
                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                            TargetControlID="tbCantidad">
                        </atk:FilteredTextBoxExtender>
                    </td>
                    <td style="width: 110px; height: 25px;" align="left" valign="middle">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                        <span>Unidad de medida&nbsp;</span><span class="camposObligatorios">(*)</span>
                    </td>
                    <td style="width: 250px; height: 25px;" align="left" valign="middle">
                        <asp:DropDownList ID="ddlUnidadMedida" runat="server" Width="240px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 110px; height: 25px;" align="left" valign="middle">
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px; height: 25px;" align="left" valign="middle">
                        <span>¿Se controla stock?</span>
                    </td>
                    <td style="width: 250px; height: 25px;" align="left" valign="middle">
                        <asp:RadioButtonList ID="rbControl" runat="server" RepeatDirection="Horizontal" Enabled="false">
                            <asp:ListItem Value="1" Text="Si" />
                            <asp:ListItem Value="0" Text="No" Selected="True" />
                        </asp:RadioButtonList>
                    </td>
                    <td style="width: 110px; height: 25px;" align="left" valign="top">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" valign="middle">
                        <asp:ImageButton ID="btnGrabar_IngresarMedicamento" runat="server" Width="84px" Height="19px"
                            ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnGrabarV2_2.png'"
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnGrabarV2_1.png'"
                            ToolTip="Grabar" OnClick="btnGrabar_IngresarMedicamento_Click" />&nbsp;
                        <asp:ImageButton ID="btnCancelar_IngresarMedicamento" runat="server" Width="84px"
                            Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                            ToolTip="Cancelar" OnClick="btnCerrar_IngresarMedicamento_Click" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
            </table>
            <div id="controlIngresarMedicamento" style="display: none">
                <input type="button" id="btnVer_IngresarMedicamento" runat="server" />
                <input type="button" id="btnOK_IngresarMedicamento" />
                <input type="button" id="btnCancel_IngresarMedicamento" />
            </div>
        </asp:Panel>
        <atk:ModalPopupExtender ID="ModalPopupExtender_IngresarNombresMedicamento" runat="server"
            PopupControlID="Panel_IngresarNombresMedicamento" TargetControlID="btnVer_IngresarNombresMedicamento"
            OkControlID="btnOK_IngresarNombresMedicamento" CancelControlID="btnCancel_IngresarNombresMedicamento"
            BackgroundCssClass="modalBackground"  Drag="True" PopupDragHandleControlID="dragCtrl_IngresarNombresMedicamento"
            Enabled="True" />
        <asp:Panel ID="Panel_IngresarNombresMedicamento" BackColor="White" BorderColor="Black"
            BorderWidth="1" runat="server" Style="width: 500px; display: none;">
            <table cellpadding="0" cellspacing="0" border="0" id="panelRegistro" style="width: 500px;">
                <tr>
                    <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                        <span style="width: 30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td style="width: 440px; height: 26px; cursor: pointer;" align="left" valign="middle"
                        class="header" colspan="2" id="dragCtrl_IngresarNombresMedicamento">
                        <span style="font-weight: bold; font-size: 11px; font-family: Arial; cursor: pointer;">
                            Registro de Nombres de Medicamento</span>
                    </td>
                    <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                        <asp:ImageButton ID="btnCerrar_IngresarNombresMedicamento" runat="server" Width="16px"
                            Height="15px" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" OnClick="btnCerrar_IngresarNombresMedicamento_Click"
                            ToolTip="Cerrar Panel" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px;" rowspan="2">
                    </td>
                    <td style="width: 440px; height: 25px;" valign="middle" align="right" colspan="2">
                        <em>Campos Obligatorios (*)</em>
                    </td>
                    <td style="width: 30px;" rowspan="2">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; height: 60px;" align="left" valign="middle">
                        <span>Descripción&nbsp;</span><span class="camposObligatorios">(*)</span>
                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                            TargetControlID="tbDescripcion_NombreMedicamento" ValidChars="' ','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'">
                        </atk:FilteredTextBoxExtender>
                    </td>
                    <td style="width: 360px; height: 60px;" align="left" valign="top">
                        <asp:TextBox ID="tbDescripcion_NombreMedicamento" runat="server" CssClass="miTextBox"
                            Width="350px" Height="50px" Rows="3" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" valign="middle">
                        <asp:ImageButton ID="btnGrabar_IngresarNombresMedicamento" runat="server" Width="84px"
                            Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnGrabarV2_2.png'"
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnGrabarV2_1.png'"
                            ToolTip="Grabar" OnClick="btnGrabar_IngresarNombresMedicamento_Click" />&nbsp;
                        <asp:ImageButton ID="btnCancelar_IngresarNombresMedicamento" runat="server" Width="84px"
                            Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                            ToolTip="Cancelar" OnClick="btnCerrar_IngresarNombresMedicamento_Click" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
            </table>
            <div id="controlIngresarNombresMedicamento" style="display: none">
                <input type="button" id="btnVer_IngresarNombresMedicamento" runat="server" />
                <input type="button" id="btnOK_IngresarNombresMedicamento" />
                <input type="button" id="btnCancel_IngresarNombresMedicamento" />
            </div>
        </asp:Panel>
        <atk:ModalPopupExtender ID="ModalPopupExtender_IngresarPresentacion" runat="server"
            PopupControlID="Panel_IngresarPresentacion" TargetControlID="btnVer_IngresarPresentacion"
            OkControlID="btnOK_IngresarPresentacion" CancelControlID="btnCancel_IngresarPresentacion"
            BackgroundCssClass="modalBackground"  Drag="True" PopupDragHandleControlID="dragCtrl_IngresarPresentacion"
            Enabled="True" />
        <asp:Panel ID="Panel_IngresarPresentacion" BackColor="White" BorderColor="Black"
            BorderWidth="1" runat="server" Style="width: 500px; display: none;">
            <table cellpadding="0" cellspacing="0" border="0" id="panelRegistro" style="width: 500px;">
                <tr>
                    <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                        <span style="width: 30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td style="width: 440px; height: 26px; cursor: pointer;" align="left" valign="middle"
                        class="header" colspan="2" id="dragCtrl_IngresarPresentacion">
                        <span style="font-weight: bold; font-size: 11px; font-family: Arial;">Registro de Presentación</span>
                    </td>
                    <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                        <asp:ImageButton ID="btnCerrar_IngresarPresentacion" runat="server" Width="16px"
                            Height="15px" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" OnClick="btnCerrar_IngresarPresentacion_Click"
                            ToolTip="Cerrar Panel" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30px;" rowspan="2">
                    </td>
                    <td style="width: 440px; height: 25px;" valign="middle" align="right" colspan="2">
                        <em>Campos Obligatorios (*)</em>
                    </td>
                    <td style="width: 30px;" rowspan="2">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; height: 60px;" align="left" valign="middle">
                        <span>Descripción&nbsp;</span><span class="camposObligatorios">(*)</span>
                        <atk:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                            TargetControlID="tbDescripcion_Presentacion" ValidChars="' ','.','á','é','í','ó','ú','(',')','Á','É','Í','Ó','Ú'">
                        </atk:FilteredTextBoxExtender>
                    </td>
                    <td style="width: 360px; height: 60px;" align="left" valign="top">
                        <asp:TextBox ID="tbDescripcion_Presentacion" runat="server" CssClass="miTextBox"
                            Width="350px" Height="50px" Rows="3" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" valign="middle">
                        <asp:ImageButton ID="btnGrabar_IngresarPresentacion" runat="server" Width="84px"
                            Height="19px" ImageUrl="~/App_Themes/Imagenes/btnGrabarV2_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnGrabarV2_2.png'"
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnGrabarV2_1.png'"
                            ToolTip="Grabar" OnClick="btnGrabar_IngresarPresentacion_Click" />&nbsp;
                        <asp:ImageButton ID="btnCancelar_IngresarPresentacion" runat="server" Width="84px"
                            Height="19px" ImageUrl="~/App_Themes/Imagenes/btnCancelar_1.png" onmouseover="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_2.png'"
                            onmouseout="this.src = '/SaintGeorgeOnline/App_Themes/Imagenes/btnCancelar_1.png'"
                            ToolTip="Cancelar" OnClick="btnCerrar_IngresarPresentacion_Click" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
            </table>
            <div id="controlIngresarPresentacion" style="display: none">
                <input type="button" id="btnVer_IngresarPresentacion" runat="server" />
                <input type="button" id="btnOK_IngresarPresentacion" />
                <input type="button" id="btnCancel_IngresarPresentacion" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>   