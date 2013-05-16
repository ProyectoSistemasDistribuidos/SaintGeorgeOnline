<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AlertaStocksMedicamentos.ascx.vb" Inherits="Modulo_Alertas_AlertaStocksMedicamentos" %>

<atk:DragPanelExtender ID="DragPanelExtender_AlertaStockMedicamento" runat="server" 
                        Enabled="True" 
                        BehaviorID="Drag_AlertaStockMedicamento"                         
                        DragHandleID="pnlContainer_AlertaStockMedicamento" 
                        TargetControlID="pnlContainer_AlertaStockMedicamento">
</atk:DragPanelExtender>

<asp:Panel ID="pnlContainer_AlertaStockMedicamento" runat="server">
<div id="divMedicamento" runat="server" style="position: relative;width: 310px; height: 250px;">

    <table cellpadding="0" cellspacing="0" border="0" width="310px">    
        <tr>
            <td style="width: 310px; height: 250px; background: url('/SaintGeorgeOnline/App_Themes/imagenes/fondoAlerta2.png') no-repeat;" valign="middle" align="center" colspan="3">   
                        
            <table cellpadding="0" cellspacing="0" border="0" width="310px">
            
                <tr>
                    <td style="width: 150px; height: 32px;" align="right" valign="bottom">
                        <img src="/SaintGeorgeOnline/App_Themes/Imagenes/first_aid_kit.png" width="50" height="32" alt="Lista de Stock" border="0" style="border:0px"/>
                    </td>
                    <td style="width: 160px; height: 32px;" align="left" valign="middle">
                        <asp:Label ID="lbltitulo" runat="server" style="padding-left:0px; font-family:Arial; font-size:14px; font-weight:bold;" />
                    </td>                    
                </tr>
                
                <tr>
                    <td style="width: 310px; height: 160px; border: solid 0px blue" align="center" valign="top" colspan="2">
                <asp:Chart id="Chart1" runat="server" BackColor="Transparent" Width="310px" Height="142px" BackImage="/SaintGeorgeOnline/App_Themes/imagenes/fondoAlerta2_chart.png" BackImageAlignment="left">                
                    <Series>                        
                        <asp:series ChartArea="ChartArea1" Name="Series2" LegendText="Stock Minimo" BorderColor="#444444" Color="#3782ee"></asp:series>
                        <asp:series ChartArea="ChartArea1" Name="Series1" LegendText="Stock Actual" BorderColor="#444444" Color="#ff0000"></asp:series>                        
                    </Series>
                    <ChartAreas>                        
                        <asp:ChartArea Name="ChartArea1" BackColor="64, 165, 191, 228" ShadowColor="Transparent">
                            <Area3DStyle Rotation="0" Perspective="0" Inclination="0" IsRightAngleAxes="False" WallWidth="0" IsClustered="False"></Area3DStyle>                                                                                                                     
                            <AxisY LabelAutoFitMaxFontSize="8">                                
                                <LabelStyle Font="Arial, 7px" ForeColor="Black"/>
                                <MajorGrid LineColor="64, 64, 64, 64"  />
                            </AxisY>
                            <AxisX  LabelAutoFitMaxFontSize="8">
                                <LabelStyle Font="Arial, 7px" ForeColor="Black"/>
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisX>                            
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>                       
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 310px; height: 10px;" align="left" valign="top" colspan="2">

<table cellpadding="0" cellspacing="0" border="0" style="width:310px; font-family: Arial; font-size: 12px; font-weight: bold;">
                            <tr>
                                <td style="width:85px; height:12px;" align="right" valign="middle">
<span style="display:block; height:12px; width:85px; margin:0px; font-family: Arial; font-size:10px; color: black; font-weight: bold">Legenda :</span>                                                                                      
                                </td>
                                <td style="width:5px; height:12px;" align="center" valign="middle">                                
                                </td>
                                <td style="width:100px; height:12px;" align="center" valign="middle">
<span style="display:block; height:12px; width:100px; border: solid 1px black; margin:0px; font-family: Arial; font-size:10px; background-color: #ff0000; color: white; font-weight: bold">Stock Actual</span>                                   
                                    
                                </td>
                                <td style="width:5px; height:12px;" align="center" valign="middle">
                                
                                </td>
                                <td style="width:100px; height:12px;" align="center" valign="middle">
<span style="display:block; height:12px; width:100px; border: solid 1px black; margin:0px; font-family: Arial; font-size:10px; background-color: #2d78ec; color: white; font-weight: bold">Stock Mínimo</span>                                                                   
                                </td>
                                <td style="width:20px; height:12px;" align="center" valign="middle">
                                
                                </td>
                            </tr>
</table>                                                            
                        
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 310px; height: 30px;" align="left" valign="top" colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:310px; font-family: Arial; font-size: 12px; font-weight: bold;" id="misAlertas">
                            <tr>
                                <td style="width:50px; height:30px;" align="right" valign="middle">
                                    <asp:ImageButton ID="btnVerMas" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/book-icon.png" 
                                        ToolTip="Ver más" Width="25" Height="25" 
                                        OnClick="btnVerMas_Click"/>
                                    
                                </td>
                                
                                <td style="width:60px; height:30px;" align="left" valign="middle">
                                    <asp:LinkButton ID="linkVerMas" runat="server" 
                                        OnClick="btnVerMas_Click">Ver más</asp:LinkButton>
                                </td>
                                
                                <td style="width:40px; height:30px;" align="right" valign="middle">
                                    <asp:ImageButton ID="btnRegularizarStock" runat="server" 
                                        ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/alert.png" 
                                        ToolTip="Ir a Regularización de Stock" Width="25" Height="25" 
                                        PostBackUrl="~/Modulo_Enfermeria/KardexMedicamentos.aspx" CausesValidation="false" />
                                </td>
                                
                                <td style="width:160px; height:30px;" align="left" valign="middle">
                                    <asp:LinkButton ID="linkRegularizarStock" runat="server" 
                                        PostBackUrl="~/Modulo_Enfermeria/KardexMedicamentos.aspx" 
                                        CausesValidation="false">Ir a regularizar Stock</asp:LinkButton>   
                                </td>
                            </tr>
                        </table>                   
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 310px; height: 16px;" align="left" valign="top" colspan="2">
                        &nbsp;
                    </td>
                </tr> 
                   
            </table>
                                                      
            </td>            
        </tr>              
    </table>  
   
</div>       
</asp:Panel> 

<div id="ModalPopUP_AlertaStockMedicamento">
    <atk:ModalPopupExtender ID="pnModalListaAlertaStock" runat="server"
        TargetControlID="VerListaAlertaStock"
        PopupControlID="miPanelAlertaStock"
        BackgroundCssClass="MiModalBackground" 
        DropShadow="false" 
        OkControlID="OKListaAlertaStock" 
        CancelControlID="CancelListaAlertaStock"
        Drag="true" 
        PopupDragHandleControlID="miDragControl" />           

    <asp:panel id="miPanelAlertaStock" runat="server" BackColor="#4c4c4c" style="background:url('/SaintGeorgeOnline/App_Themes/Imagenes/fondoAlerta3.png') no-repeat; width:400px; height:400px; display: none">  
        <table cellpadding="0" cellspacing="0" border="0" width="400px">    
        <tr>
            <td style="width: 400px; height: 40px" valign="middle" align="center">   
                <table cellpadding="0" cellspacing="0" border="0" style="width:400px; height: 40px">
                    <tr>
                        <td style="width:360px; height: 40px;" align="left" valign="middle">
                            &nbsp;&nbsp;&nbsp;&nbsp;<span id="miDragControl" style="color: #FFFFFF; font-weight:bold; font-size:14px; font-family:Arial; cursor: pointer;">Alerta</span>&nbsp;&nbsp;
                        </td>
                        <td style="width:40px; height: 40px;" align="left" valign="middle">
                            <asp:ImageButton ID="btnCerraListaAlertaStock" runat="server" Width="25" Height="29"
                                ImageUrl="/SaintGeorgeOnline/App_Themes/Imagenes/x.png" CssClass="miPaddingBottom"
                                onclick="btnCerraListaAlertaStock_Click" ToolTip="Cerrar Panel"/>
                        </td>
                    </tr>
                </table> 
            </td>            
        </tr>
        <tr>
            <td style="width: 400px; height: 300px; border: solid 0px green" valign="top" align="center">   
             
                <div style="margin: 0px 10px 0px 10px; padding: 0; width: 380px; border: solid 1px #a6a3a3;">           
                <asp:GridView ID="miGridviewStockTotal" runat="server"
                    Width="380"
                    CssClass="miGridviewBusquedaPersona" 
                    GridLines="none" 
                    AutoGenerateColumns="False"                      
                    ShowFooter="false"
                    ShowHeader="true"                   
                    EmptyDataText=" - No se encontraron resultados - "
                    EmptyDataRowStyle-ForeColor="#a51515"
                    EmptyDataRowStyle-HorizontalAlign="Center"
                    OnRowDataBound="miGridviewStockTotal_RowDataBound">    
                    <HeaderStyle CssClass="miGridviewBusquedaPersona_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />            
                     <Columns>            
                        <asp:BoundField DataField="DescMedicamento"  HeaderText="Medicamento">
                            <HeaderStyle HorizontalAlign="Center" Width="200px" />
                            <ItemStyle HorizontalAlign="Center" Width="200px" CssClass="miGridviewBusqueda_Rows" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Stock" HeaderText="Stock Actual">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" CssClass="miGridviewBusqueda_Rows" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" Width="90px" CssClass="miGridviewBusqueda_Rows" />
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
    <div id="controlListaAlertaStock" style="display:none">
            <input type="button" id="VerListaAlertaStock" runat="server" />
            <input type="button" id="OKListaAlertaStock" />
            <input type="button" id="CancelListaAlertaStock" />
    </div>       
    </asp:panel>       
</div>  
 

