<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ReportesPensiones.aspx.vb" Inherits="ModuloReportes_ReportesPensiones" title="Página sin título" %>

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
    <asp:ListBox ID="lstReportes" runat="server" style="Height: 250px; Width: 200px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;"
        OnSelectedIndexChanged="lstReportes_SelectedIndexChanged"  AutoPostBack="True">                 
    </asp:ListBox>   
            </td>
            
            <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
    <asp:ListBox ID="lstPresentacion" runat="server" style="Height: 250px; Width: 200px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;"
    OnSelectedIndexChanged="lstPresentacion_SelectedIndexChanged"  AutoPostBack="True">                            
    
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
            <span>Conceptos :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
<asp:CheckBox ID="ddlRep1_chkAll" runat="server" Text="Todos" style="margin: 0; padding: 0; font-size: 8pt; font-family: Arial;" />
        </td> 
    </tr>        
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="top">   
        </td>  
        <td style="width: 200px; height: 150px;" align="left" valign="top">    
<div id="divRep1_listaConceptos" style="overflow-y: scroll; overflow-x: hidden; width: 200px; height: 138px; margin: 0; padding: 0;">  
<asp:CheckBoxList ID="ddlRep1_rbConceptosCobro" runat="server" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table" 
    Width="200px" CellPadding="0" CellSpacing="0" BorderWidth="0">
</asp:CheckBoxList>  
</div>              
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep1_Grados" runat="server" OnSelectedIndexChanged="ddlRep1_Grados_SelectedIndexChanged" AutoPostBack="true"
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>          
        </td> 
    </tr>      
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep1_Aulas" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
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
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Fin :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px">
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">
        <asp:TextBox ID="tbRep2_FechaFin" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="tbRep2_FechaFin"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep2_FechaFin" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbRep2_FechaFin"
            PopupButtonID="imgRep2_FechaFin" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>  
        </td>
    </tr>   
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Conceptos :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
<asp:CheckBox ID="ddlRep2_chkAll" runat="server" Text="Todos" style="margin: 0; padding: 0; font-size: 8pt; font-family: Arial;" />
        </td> 
    </tr>        
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="top">   
        </td>  
        <td style="width: 200px; height: 150px;" align="left" valign="top">    
<div id="divRep2_listaConceptos" style="overflow-y: scroll; overflow-x: hidden; width: 200px; height: 138px; margin: 0; padding: 0;">  
<asp:CheckBoxList ID="ddlRep2_rbConceptosCobro" runat="server" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table" 
    Width="200px" CellPadding="0" CellSpacing="0" BorderWidth="0">
</asp:CheckBoxList>  
</div>              
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep2_Grados" runat="server" OnSelectedIndexChanged="ddlRep2_Grados_SelectedIndexChanged" AutoPostBack="true"
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>          
        </td> 
    </tr>      
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep2_Aulas" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>          
        </td> 
    </tr>                
    </table>                            
</asp:Panel>      

<asp:Panel ID="pnlReporte3" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep3_Periodo" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>   
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
             <asp:DropDownList ID="ddlRep3_NivelMinisterio" runat="server" OnSelectedIndexChanged="ddlRep3_NivelMinisterio_SelectedIndexChanged" AutoPostBack="true"
              style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>   
        </td>
    </tr>   
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep3_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList> 
        </td> 
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Motivo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep3_MotivoBeca" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>          
        </td> 
    </tr>      
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>% Beca :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep3_TipoBeca" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>          
        </td> 
    </tr>                
    </table>                            
</asp:Panel>    

<asp:Panel ID="pnlReporte4" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="Table1">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep4_Periodo" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>   
    <tr id= "tr_Beca" runat="server" >
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>% Beca :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep4_TipoBeca" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>          
        </td> 
    </tr>
     <tr id="tr_Mes" runat="server" >
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Mes :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep4_Mes" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr> 
    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
             <asp:DropDownList ID="ddlRep4_Nivel" runat="server" AutoPostBack="true"
              style="width: 190px; font-size: 8pt; font-family: Arial;" OnSelectedIndexChanged ="ddlRep4_Nivel_SelectedIndexChanged">
            </asp:DropDownList>   
        </td>
    </tr>   
      <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>SubNivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
             <asp:DropDownList ID="ddlRep4_SubNivel" runat="server" AutoPostBack="true"
              style="width: 190px; font-size: 8pt; font-family: Arial;" OnSelectedIndexChanged="ddlRep4_SubNivel_SelectedIndexChanged">
            </asp:DropDownList>   
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep4_Grado" runat="server" AutoPostBack ="true" 
                style="width: 190px; font-size: 8pt; font-family: Arial;" OnSelectedIndexChanged ="ddlRep4_Grado_SelectedIndexChanged">
            </asp:DropDownList> 
        </td> 
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
            <asp:DropDownList ID="ddlRep4_Aula" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>          
        </td> 
    </tr>      
                    
    </table>                            
</asp:Panel>  

<asp:Panel ID="pnlReporte5" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Inicio :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">          
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px">    
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">                                      
            <asp:TextBox ID="tbRep5_FechaInicio" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
            <atk:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="tbRep5_FechaInicio"
                UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
            </atk:MaskedEditExtender>   
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep5_FechaInicio" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbRep5_FechaInicio"
            PopupButtonID="imgRep5_FechaInicio" Format="dd/MM/yyyy" CssClass="MyCalendar" />
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
            <asp:TextBox ID="tbRep5_FechaFin" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
            <atk:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="tbRep5_FechaFin"
                UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
            </atk:MaskedEditExtender>   
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep5_FechaFin" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="tbRep5_FechaFin"
            PopupButtonID="imgRep5_FechaFin" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>                                                                      
        </td>
    </tr>              
    </table>                            
</asp:Panel>   
               
<asp:Panel ID="pnlReporte6" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep6_Periodo" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>       
    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Inicio:</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">      
        
<table cellpadding="0" cellspacing="0" border="0" style="width: 150px">    
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">                                      
            <asp:TextBox ID="tbRep6_FechaInicio" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
            <atk:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="tbRep6_FechaInicio"
                UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
            </atk:MaskedEditExtender>   
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep6_FechaInicio" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="tbRep6_FechaInicio"
            PopupButtonID="imgRep6_FechaInicio" Format="dd/MM/yyyy" CssClass="MyCalendar" />
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
            <asp:TextBox ID="tbRep6_FechaFin" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
            <atk:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="tbRep6_FechaFin"
                UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
            </atk:MaskedEditExtender>   
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep6_FechaFin" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="tbRep6_FechaFin"
            PopupButtonID="imgRep6_FechaFin" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>                                                                      
        </td>
    </tr>  

    </table>                          
</asp:Panel>      
         
<asp:Panel ID="pnlReporte7" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep7_Periodo" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha 1:</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">     
<table cellpadding="0" cellspacing="0" border="0" style="width: 150px">    
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">  
        <asp:TextBox ID="tbRep7_Fecha1" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="tbRep7_Fecha1"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>  
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">  
        <asp:ImageButton runat="server" ID="imgRep7_Fecha1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="tbRep7_Fecha1"
            PopupButtonID="imgRep7_Fecha1" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
</table>         
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha 2:</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">             
<table cellpadding="0" cellspacing="0" border="0" style="width: 150px">    
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">  
        <asp:TextBox ID="tbRep7_Fecha2" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender9" runat="server" TargetControlID="tbRep7_Fecha2"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>  
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">  
        <asp:ImageButton runat="server" ID="imgRep7_Fecha2" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="tbRep7_Fecha2"
            PopupButtonID="imgRep7_Fecha2" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
</table> 
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
    <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Width="388px" Style="display: none">
        <table style="width: 100%; border: solid 1px #000000;" border="0" cellpadding="0" cellspacing="0">  
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
                    <br />
                    <asp:Button ID="btnCerrarModal" runat="server" Text="Cerrar" />
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

