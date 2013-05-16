<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ReportesMatricula.aspx.vb" Inherits="Modulo_Reportes_ReportesMatricula" title="Página sin título" %>

<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .FondoAplicacion{
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }      
    #miTablaFiltros span{
        margin: 0;
        padding: 0;
        font-size: 11px;
        font-family: Arial;    
    }      
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" >

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }
      
</script>

<div id="miPaginaMantenimiento">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>    
        <asp:PostBackTrigger ControlID="btnReporteExportar" />
    </Triggers>
<ContentTemplate>

    <div style="border: solid 0px blue; width: 860px;">
                
    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
    <table style="width: 850px;" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                width: 200px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
            <span style="padding-left: 30px;">Reporte</span>
            </td>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                width: 200px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
            <span style="padding-left: 30px;">Presentación</span>
            </td>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                width: 300px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
            <span style="padding-left: 30px;">Filtros</span>
            </td>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#0a0f14; 
                width: 150px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="center" valign="middle">                                    
            </td>
        </tr>
        
        <tr>
            <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
    <asp:ListBox ID="lstReportes" runat="server" 
        style="Height: 250px; Width: 200px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;"
        OnSelectedIndexChanged="lstReportes_SelectedIndexChanged"  AutoPostBack="True">                 
    </asp:ListBox>   
            </td>
            
            <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
    <asp:ListBox ID="lstPresentacion" runat="server" 
        style="Height: 250px; Width: 200px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;">                            
    </asp:ListBox>   
            </td>            
            
            <td style="border-style :solid; border-width:1px; border-color:#a6a3a3; width: 300px; height: auto; " valign="top" align="left">
                                
<asp:Panel ID="pnlReporte1" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Periodo" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Inicio :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                       
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px">    
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">
        <asp:TextBox ID="tbRep1_FechaInicio" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="tbRep1_FechaInicio"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep1_FechaInicio" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbRep1_FechaInicio"
            PopupButtonID="imgRep1_FechaInicio" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>                                     
        </td>
    </tr>   
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Fin :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px">
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">
        <asp:TextBox ID="tbRep1_FechaFin" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="tbRep1_FechaFin"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep1_FechaFin" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbRep1_FechaFin"
            PopupButtonID="imgRep1_FechaFin" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>  
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep1_Grados" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>          
        </td> 
    </tr>              
    </table>                            
</asp:Panel>      

<asp:Panel ID="pnlReporte2" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep2_Periodo" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>   
    </table>
</asp:Panel>                                
            </td>
            <td style="border-style :solid; border-width:1px; border-color:#a6a3a3; width: 150px; height: auto; " valign="top" align="center">                                                                
            <table cellpadding="0" cellspacing="0" border="0" style="width: 150px; height: 26px;">
            <tr>
                <td style="width: 150px; height: 26px;" valign="middle" align="center">
                    <asp:ImageButton ID="btnReporteExportar" runat="server" Width="84" Height="19" 
                        ImageUrl="~/App_Themes/Imagenes/btnExportar_1.png"
                        onmouseover="this.src = '../App_Themes/Imagenes/btnExportar_2.png'" 
                        onmouseout="this.src = '../App_Themes/Imagenes/btnExportar_1.png'" ToolTip="Exportar"
                        onclick="btnReporteExportar_Click" />                                               
                </td>
            </tr>
            </table>
            </td>
        </tr>                                                     
    </table>                        
    </div>             
                
    </div>

<br />

    <atk:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
        DynamicServicePath=""
        Enabled="True" 
        BackgroundCssClass="FondoAplicacion" 
        DropShadow="True" 
        PopupControlID="pnlImpresion"
        TargetControlID="lblAccionExportar">
    </atk:ModalPopupExtender>
    <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="71px" Width="388px" Style="display: none">
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">  
            <tr>
                <td style="text-align: right;">
                    <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png" />
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: 10px; color: #000080;
                    text-align: center;">
                    <img alt="" src="../App_Themes/Imagenes/bigrotation2.gif" style="width: 32px; height: 32px" />
                    <br />
                    Exportando, espere unos segundos ...
                </td>
            </tr>
        </table>
    </asp:Panel>    
    <asp:Label ID="lblAccionExportar" runat="server" ForeColor="White" Text="..." />
    
</ContentTemplate>
</asp:UpdatePanel>

</div>

<script type="text/javascript">

    $(document).ready(function() {

        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);

        $('#ctl00_ContentPlaceHolder1_ddlRep1_chkAll').click(function() {
            $('#divRep1_listaConceptos').find(':checkbox').attr('checked', this.checked);
        });

        $('#ctl00_ContentPlaceHolder1_ddlRep2_chkAll').click(function() {
            $('#divRep2_listaConceptos').find(':checkbox').attr('checked', this.checked);
        });        
           

    });

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            $('#ctl00_ContentPlaceHolder1_ddlRep1_chkAll').click(function() {
                $('#divRep1_listaConceptos').find(':checkbox').attr('checked', this.checked);
            });

            $('#ctl00_ContentPlaceHolder1_ddlRep2_chkAll').click(function() {
                $('#divRep2_listaConceptos').find(':checkbox').attr('checked', this.checked);
            });

        }
    }      
    
</script>

</asp:Content>