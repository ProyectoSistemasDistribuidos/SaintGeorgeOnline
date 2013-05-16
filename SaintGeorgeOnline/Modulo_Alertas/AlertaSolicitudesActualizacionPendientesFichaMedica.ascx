<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AlertaSolicitudesActualizacionPendientesFichaMedica.ascx.vb" Inherits="Modulo_Alertas_AlertaSolicitudesActualizacionPendientesFichaMedica" %>

<atk:DragPanelExtender ID="DragPanelExtender_AlertaSolicitudesActualizacionPendientesFichaMedica" runat="server" 
                        Enabled="True" 
                        BehaviorID="Drag_AlertaSolicitudesActualizacionPendientesFichaMedica"                         
                        DragHandleID="pnlContainer_AlertaSolicitudesActualizacionPendientesFichaMedica" 
                        TargetControlID="pnlContainer_AlertaSolicitudesActualizacionPendientesFichaMedica">
</atk:DragPanelExtender>

<asp:Panel ID="pnlContainer_AlertaSolicitudesActualizacionPendientesFichaMedica" runat="server">
<div id="divSolicitudesActualizacionPendientesFichaMedica" runat="server" style="position: relative;width: 230px; height: 190px;">

    <table cellpadding="0" cellspacing="0" border="0" width="230px">    
        <tr>
            <td style="width: 230px; height: 190px; background: url('/SaintGeorgeOnline/App_Themes/imagenes/fondoAlerta5.png') no-repeat;" valign="middle" align="center" colspan="3">   
                        
            <table cellpadding="0" cellspacing="0" border="0" width="230px">
            
                <tr>
                    <td style="width: 230px; height: 55px;" align="left" valign="top" colspan="2">
                        &nbsp;
                    </td>
                </tr> 
                
                <tr>
                    <td style="width: 230px; height: 40px;" align="center" valign="middle" colspan="2">
                        <asp:Label ID="lbltitulo" runat="server" style="padding-left:0px; font-family:Arial; font-size:14px; font-weight:bold;" />
                    </td>                    
                </tr>
                  
                <tr>
                    <td colspan="2" style="width: 230px; height: 20px;" align="center" valign="middle">
                    
<table cellpadding="0" cellspacing="0" border="0" style="width:230px; font-family: Arial; font-size:12px; color: #ff0000; font-weight: bold">
    <tr>
        <td style="width:310px; height:20px;" align="center" valign="middle">
            <span style="padding-left:30px"># Fichas Pendientes :&nbsp;</span><asp:Label ID="lblCantidadFichas" runat="server" />
        </td>
    </tr>
</table>
                            
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 230px; height: 50px;" align="left" valign="top" colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:230px; font-family: Arial; font-size: 12px; font-weight: bold;" id="misAlertas">
                            <tr>
                                <td style="width:60px; height:30px;" align="right" valign="middle">
                                    <asp:ImageButton ID="btnVerMas" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/book-icon.png" 
                                        ToolTip="Ver más" Width="25" Height="25" OnClick="btnVerMas_Click"/>
                                    
                                </td>                                
                                <td style="width:170px; height:30px;" align="left" valign="middle">
                                    <asp:LinkButton ID="linkVerMas" runat="server" OnClick="btnVerMas_Click">Ver más</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>                                 
                                <td style="width:60px; height:30px;" align="right" valign="middle">
                                    <asp:ImageButton ID="btnRegularizarStock" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/alert.png" 
                                        ToolTip="Ir a Regularización de Stock" Width="25" Height="25" 
                                        PostBackUrl="~/Modulo_Enfermeria/ValidarDatosActualizadosFichaMedica.aspx" CausesValidation="false" />
                                </td>                                
                                <td style="width:170px; height:30px;" align="left" valign="middle">
                                    <asp:LinkButton ID="linkRegularizarStock" runat="server" 
                                        PostBackUrl="~/Modulo_Enfermeria/ValidarDatosActualizadosFichaMedica.aspx"
                                        CausesValidation="false">Ir a Regularizar Fichas</asp:LinkButton>   
                                </td>
                            </tr>
                        </table>                   
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 250px; height: 25px;" align="left" valign="top" colspan="2">
                        &nbsp;
                    </td>
                </tr> 
                   
            </table>
                                                      
            </td>            
        </tr>              
    </table>  
   
</div>       
</asp:Panel> 

<div id="ModalPopUP_AlertaSolicitudesActualizacionPendientesFichaMedica">
    <atk:ModalPopupExtender ID="pnModalListaAlertaSolicitudesActualizacionPendientesFichaMedica" runat="server"
        TargetControlID="VerListaAlertaSolicitudesActualizacionPendientesFichaMedica"
        PopupControlID="miPanelAlertaSolicitudesActualizacionPendientesFichaMedica"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="false" 
        OkControlID="OKListaAlertaSolicitudesActualizacionPendientesFichaMedica" 
        CancelControlID="CancelListaAlertaSolicitudesActualizacionPendientesFichaMedica"
        Drag="true" 
        PopupDragHandleControlID="miDragControl" />           

    <asp:panel id="miPanelAlertaSolicitudesActualizacionPendientesFichaMedica" runat="server" BackColor="#4c4c4c" style="background:url('/SaintGeorgeOnline/App_Themes/Imagenes/fondoAlerta3.png') no-repeat; width:400px; height:400px; display: none">  
        <table cellpadding="0" cellspacing="0" border="0" width="400px">    
        <tr>
            <td style="width: 400px; height: 40px" valign="middle" align="center">   
                <table cellpadding="0" cellspacing="0" border="0" style="width:400px; height: 40px">
                    <tr>
                        <td style="width:360px; height: 40px;" align="left" valign="middle">
                            &nbsp;&nbsp;&nbsp;&nbsp;<span id="miDragControl" style="color: #FFFFFF; font-weight:bold; font-size:14px; font-family:Arial; cursor: pointer;">Alerta</span>&nbsp;&nbsp;
                        </td>
                        <td style="width:40px; height: 40px;" align="left" valign="middle">
                            <asp:ImageButton ID="btnCerraListaAlertaSolicitudesActualizacionPendientesFichaMedica" runat="server" Width="25" Height="29"
                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/x.png" CssClass="miPaddingBottom"
                                onclick="btnCerraListaAlertaSolicitudesActualizacionPendientesFichaMedica_Click" ToolTip="Cerrar Panel"/>
                        </td>
                    </tr>
                </table> 
            </td>            
        </tr>
        <tr>
            <td style="width: 400px; height: 300px; border: solid 0px green" valign="top" align="center">   
             
                <div style="margin: 0px 10px 0px 10px; padding: 0; width: 380px; border: solid 1px #a6a3a3;">           
                <asp:GridView ID="miGridviewSolicitudesActualizacionPendientesFichaMedica" runat="server"
                    Width="380"
                    CssClass="miGridviewBusquedaPersona" 
                    GridLines="none" 
                    AutoGenerateColumns="False"                      
                    ShowFooter="false"
                    ShowHeader="true"                   
                    EmptyDataText=" - No se encontraron resultados - "
                    EmptyDataRowStyle-ForeColor="#a51515"
                    EmptyDataRowStyle-HorizontalAlign="Center"
                    OnRowDataBound="miGridviewSolicitudesActualizacionPendientesFichaMedica_RowDataBound">    
                    <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />            
                     <Columns>            
                        <asp:BoundField DataField="FechaRegistroStr"  HeaderText="Fecha Registro">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="miGridviewBusqueda_Rows" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombreCompletoSolicitante" HeaderText="Nombre del Paciente">
                            <HeaderStyle HorizontalAlign="Center" Width="205px" />
                            <ItemStyle HorizontalAlign="Left" Width="155px" CssClass="miGridviewBusqueda_Rows" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CodigoSolicitud" HeaderText="Cod. Solicitud">
                            <HeaderStyle HorizontalAlign="Center" Width="75px" />
                            <ItemStyle HorizontalAlign="Center" Width="75px" CssClass="miGridviewBusqueda_Rows" />
                        </asp:BoundField>
                    </Columns>  
                </asp:GridView>   
                </div>    
                   
            </td>
        </tr>
        <tr>
            <td style="width: 400px; height: 60px" valign="middle" align="center">                   
            </td>            
        </tr>              
    </table>     
    <div id="controlListaAlertaSolicitudesActualizacionPendientesFichaMedica" style="display:none">
            <input type="button" id="VerListaAlertaSolicitudesActualizacionPendientesFichaMedica" runat="server" />
            <input type="button" id="OKListaAlertaSolicitudesActualizacionPendientesFichaMedica" />
            <input type="button" id="CancelListaAlertaSolicitudesActualizacionPendientesFichaMedica" />
    </div>       
    </asp:panel>       
</div>  