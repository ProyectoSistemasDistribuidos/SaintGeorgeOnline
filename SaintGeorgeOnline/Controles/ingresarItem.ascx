<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ingresarItem.ascx.vb" Inherits="Controles_ingresarItem" %>

<style type="text/css">   
    .miTextBox{
        border: solid 1px #a6a3a3;
        height: 15px;
    }
    .miTextBox:hover{
        border: solid 1px #7F9DB9;
        height: 15px;
    }     
    .modalBackground{
	    background-color: black;    
	    filter:alpha(opacity=70);
	    opacity:0.7;
    }    
    #panelRegistro span{
        font-size: 11px;
        font-family: Arial;
    }
    #panelRegistro em{
        font-size: 10px;
        font-family: Arial;
        color: #a51515;
        margin-right: 7px;
        padding: 0;
    }   
    #panelRegistro .header{        
        background: #0a0f14 url(/Presupuestos/App_Themes/Imagenes/Estilos/legend_header.gif) repeat-x;
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
    
    <asp:UpdatePanel ID="UpdatePanel_IngresarItem" runat="server">
    <ContentTemplate>  
    <atk:ModalPopupExtender ID="ModalPopupExtender_IngresarItem" runat="server"
        PopupControlID="Panel_IngresarItem"
        TargetControlID="btnVer_IngresarItem"
        OkControlID="btnOK_IngresarItem" 
        CancelControlID="btnCancel_IngresarItem"
        BackgroundCssClass="modalBackground"  
        Drag="True" PopupDragHandleControlID="dragCtrl_IngresarItem" Enabled="True" />      
    <asp:panel id="Panel_IngresarItem" BackColor="White" BorderColor="Black" BorderWidth="1" runat="server" style="width: 500px; display: none;">
        <table cellpadding="0" cellspacing="0" border="0" id="panelRegistro" style="width: 500px;">          
            <tr>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">                    
                    <span style="width:30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> 
                </td>
                <td style="width: 440px; height: 26px; cursor: pointer;" align="left" valign="middle" class="header" colspan="2" id="dragCtrl_IngresarItem">                
                    <span style="font-weight:bold; font-size:11px; font-family:Arial; cursor: pointer;">
                        <asp:Label ID="lbltitulo" runat="server" text="Registro de Artículos" />
                    </span>
                </td>
                <td style="width: 30px; height: 26px" align="right" valign="middle" class="header">
                    <asp:ImageButton ID="btnCerrar_IngresarItem" runat="server" Width="16px" Height="15px"
                        ImageUrl="~/App_Themes/Imagenes/Botones/cross_icon_normal.png"
                        onclick="btnCerrar_IngresarItem_Click" ToolTip="Cerrar Panel"/>                                        
                </td>
            </tr>
            <tr><td colspan="4">
                <asp:HiddenField ID="hiddenComboPadreID" runat="server" Value="" />
                <asp:HiddenField ID="hiddenModalPadreID" runat="server" Value="" />
                <asp:HiddenField ID="hiddenTieneModalPadreID" runat="server" Value="false" />
                <asp:HiddenField ID="hiddenCodigoRegistro" runat="server" Value="0" />
                <br />
            </td></tr>  
            <tr>
                <td style="width: 30px;" rowspan="3"></td>  
                <td style="width: 440px; height: 25px;" valign="middle" align="right" colspan="2">
                    <em>Campos Obligatorios (*)</em>
                </td>
                <td style="width: 30px;" rowspan="3"></td>  
            </tr> 
            <tr>
                <td style="width: 80px; height: 60px;" align="left" valign="middle">
                    <span>Descripción :</span><span class="camposObligatorios">(*)</span>
                </td>
                <td style="width: 360px; height: 60px;" align="left" valign="top">
                    <asp:TextBox ID="tbDescripcion" runat="server" CssClass="miTextBox" Width="350px" Height="50px" Rows="3" TextMode="MultiLine" />
                </td>
            </tr>            
            <tr>
                <td style="width: 80px; height: 25px;" align="left" valign="middle">
                    <span>Tipo :</span><span class="camposObligatorios">(*)</span>
                </td>
                <td style="width: 360px; height: 25px;" align="left" valign="middle">
                     <asp:DropDownList ID="ddlTipo" runat="server" Width="250px" style="font-size: 8pt; font-family: Arial;">
                    </asp:DropDownList>
                </td>
            </tr>       
            <tr><td colspan="4"><br /></td></tr>
            <tr>
                <td colspan="4" align="center" valign="middle"> 
                    <asp:ImageButton ID="btnGrabar_IngresarItem" runat="server" Width="84px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/Botones/btnGrabar_1.png"
                            onmouseover="this.src = '/Presupuestos/App_Themes/Imagenes/Botones/btnGrabar_2.png'" 
                            onmouseout="this.src = '/Presupuestos/App_Themes/Imagenes/Botones/btnGrabar_1.png'" ToolTip="Grabar"
                            onclick="btnGrabar_IngresarItem_Click" />&nbsp;
                    <asp:ImageButton ID="btnCancelar_IngresarItem" runat="server" Width="84px" Height="19px" 
                            ImageUrl="~/App_Themes/Imagenes/Botones/btnCancelar_1.png"
                            onmouseover="this.src = '/Presupuestos/App_Themes/Imagenes/Botones/btnCancelar_2.png'" 
                            onmouseout="this.src = '/Presupuestos/App_Themes/Imagenes/Botones/btnCancelar_1.png'" ToolTip="Cancelar"
                            onclick="btnCerrar_IngresarItem_Click" CausesValidation="False"/>
                </td>
            </tr>   
            <tr><td colspan="4"><br /></td></tr>             
        </table>  
        <div id="controlIngresarItem" style="display:none">
            <input type="button" id="btnVer_IngresarItem" runat="server" />
            <input type="button" id="btnOK_IngresarItem" />
            <input type="button" id="btnCancel_IngresarItem" />
        </div>       
    </asp:panel> 
    </ContentTemplate>        
    </asp:UpdatePanel>   