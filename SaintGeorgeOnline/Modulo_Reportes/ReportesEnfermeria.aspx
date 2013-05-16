<%@ Page Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ReportesEnfermeria.aspx.vb" Inherits="ModuloReportes_ReportesEnfermeria" title="Página sin título" %>

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

    function MostrarImpresionFichaMedica_html() {

        window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FichaAtencionHistorialClinico_html.aspx', '_blank', '');
    }
      
</script>

<div id="miPaginaMantenimiento">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>    
        <asp:PostBackTrigger ControlID="btnReporteExportar" />
    </Triggers>
<ContentTemplate>

    <div style="border: solid 0px blue; width: 860px; height: 1248px;">
                
    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
    <table style="width: 795px; height: 1424px;" cellpadding="0" cellspacing="0" 
            border="0">
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
                                
<asp:Panel ID="pnlReporte1" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
   
   
   
   <%--tipo sede--%>
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Sede :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="dpSede" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
               >
            </asp:DropDownList>                                       
        </td>
    </tr>
   <%--fin sede --%>
   
   
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Tipo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_TipoPersona" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_TipoPersona_SelectedIndexChanged">
            </asp:DropDownList>                                       
        </td>
    </tr>
    
    
    <tr id="col_FecInic" runat="server" >
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Inicio :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                       
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px; height: 25px;">    
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
    <tr id="col_FecFin" runat="server">
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
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Nivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_Nivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>SubNivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_SubNivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_SubNivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>     
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_Grado_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Aula" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_Aula_SelectedIndexChanged">
            </asp:DropDownList>     
        </td>
    </tr>     
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Persona :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Persona" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>     
        </td>
    </tr>        
    </table>                            
</asp:Panel>   
<asp:Panel ID="pnlReporte2" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    
<tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Sede :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="cmbSede2" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
               >
            </asp:DropDownList>                                       
        </td>
    </tr>
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Tipo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep2_TipoPersona" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
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
        <asp:TextBox ID="tbRep2_FechaInicio" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="tbRep2_FechaInicio"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep2_FechaInicio" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbRep2_FechaInicio"
            PopupButtonID="imgRep2_FechaInicio" Format="dd/MM/yyyy" CssClass="MyCalendar" />
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
    </table>                            
</asp:Panel> 
<asp:Panel ID="pnlReporte3" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Tipo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep3_TipoPersona" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr> 
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep3_PeriodoAcademico" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>      
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep3_Nivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep3_Nivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>SubNivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep3_SubNivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep3_SubNivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>     
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep3_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep3_Grado_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep3_Aula" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>     
        </td>
    </tr>    
    </table>                            
</asp:Panel> 
<asp:Panel ID="pnlReporte4" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    
     <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAula" runat="server" Width="190px" style="font-size: 8pt; font-family: Courier New;"
                OnSelectedIndexChanged="ddlAula_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </td> 
    </tr>  
       
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Alumno :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAlumno" runat="server" style="width: 190px; font-size: 8pt; font-family: Courier New;" >
            </asp:DropDownList>  
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Rango de fechas :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
           <asp:RadioButtonList ID="rbTipoFecha" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="0" Text="No"  Selected="True"/>
                <asp:ListItem Value="1" Text="Si"/>
            </asp:RadioButtonList>  
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
        <asp:TextBox ID="tbRep4_FechaInicio" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender9" runat="server" TargetControlID="tbRep4_FechaInicio"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep4_FechaInicio" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="tbRep4_FechaInicio"
            PopupButtonID="imgRep4_FechaInicio" Format="dd/MM/yyyy" CssClass="MyCalendar" />
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
        <asp:TextBox ID="tbRep4_FechaFin" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="tbRep4_FechaFin"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep4_FechaFin" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="tbRep4_FechaFin"
            PopupButtonID="imgRep4_FechaFin" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>  
        </td>
    </tr>
    </table>                            
</asp:Panel>    
<asp:Panel ID="PnlReporte5" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="Table1">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Tipo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep5_TipoPersona" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr> 
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep5_PeriodoAcademico" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;" AutoPostBack="true" OnSelectedIndexChanged ="ddlRep5_PeriodoAcademico_SelectedIndexChanged">
            </asp:DropDownList>                                       
        </td>
    </tr>      
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep5_Nivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged ="ddlRep5_Nivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>SubNivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep5_SubNivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddlRep5_SubNivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>     
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep5_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddlRep5_Grado_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep5_Aula" runat="server" 
               style="width: 190px; font-size: 8pt; font-family: Arial;"
               AutoPostBack ="true" 
               OnSelectedIndexChanged="ddlRep5_Aula_SelectedIndexChanged">           
            </asp:DropDownList>     
        </td>
    </tr>   
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Persona :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep5_Persona" runat="server" 
                style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>     
        </td>
    </tr> 
    </table>                            
</asp:Panel> 


<asp:Panel ID="pnlReporte6" runat="server" BackColor="White" 
                    style="width: 300px; height: 200px;" Height="210px">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
     <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Sede :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep6_Sede" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>     
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Tipo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep6_TipoPersona" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_TipoPersona_SelectedIndexChanged">
            </asp:DropDownList>                                       
        </td>
    </tr>
    <tr id="col_FecInicPnl6" runat="server" >
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Inicio :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                       
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px; height: 25px;">    
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">
        <asp:TextBox ID="tbRep6_FechaInicio" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="tbRep6_FechaInicio"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep6_FechaInicio" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="tbRep6_FechaInicio"
            PopupButtonID="imgRep6_FechaInicio" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>                                     
        </td>
    </tr>   
    <tr id="col_FecFinPnl6" runat="server">
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Fin :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px">
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">
        <asp:TextBox ID="tbRep6_FechaFin" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="tbRep6_FechaFin"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="imgRep6_FechaFin" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="tbRep6_FechaFin"
            PopupButtonID="imgRep6_FechaFin" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>  
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep6_Nivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_Nivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>SubNivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep6_SubNivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_SubNivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>     
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep6_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_Grado_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep6_Aula" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_Aula_SelectedIndexChanged">
            </asp:DropDownList>     
        </td>
    </tr>     
      <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span># Mayor a :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
           <asp:DropDownList ID="ddlRep6_NumeroPintar" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
               >
            </asp:DropDownList>     
        </td>
    </tr>     
    </table>                            
</asp:Panel>

<%-- INICIO REPORTE ENFERMERIA POR HORAS --%>
<%--<asp:Panel ID="pnlReporteLast" runat="server" BackColor="White" 
                    style="width: 300px; height: 200px;" Height="210px">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="Table2">
     <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Sede :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="cmbSedeLast" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>     
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Tipo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="cmbTipoLast" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_TipoPersona_SelectedIndexChanged">
            </asp:DropDownList>                                       
        </td>
    </tr>
    <tr id="Tr1" runat="server" >
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Inicio :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                       
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px; height: 25px;">    
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">
        <asp:TextBox ID="txtFechaInicioLast" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtFechaInicioLast"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtFechaInicioLast"
            PopupButtonID="imgRep6_FechaInicio" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>                                     
        </td>
    </tr>   
    <tr id="Tr2" runat="server">
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Fecha Fin :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle"> 
    <table cellpadding="0" cellspacing="0" border="0" style="width: 150px">
    <tr>
    <td style="width: 110px; height: 25px;" align="left" valign="middle">
        <asp:TextBox ID="txtFechaFinLast" runat="server" CssClass="miTextBoxCalendar" style="width: 100px; font-size: 8pt; font-family: Arial;" />
        <atk:MaskedEditExtender ID="MaskedEditExtender10" runat="server" TargetControlID="txtFechaFinLast"
            UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date" PromptCharacter="-">
        </atk:MaskedEditExtender>
    </td>
    <td style="width: 40px; height: 25px;" align="left" valign="middle">
        <asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/App_Themes/Imagenes/calendar_icon.png"
            AlternateText="Elija una fecha del calendario" />
        <atk:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtFechaFinLast"
            PopupButtonID="imgRep6_FechaFin" Format="dd/MM/yyyy" CssClass="MyCalendar" />
    </td>
    </tr>
    </table>  
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="cmbNivelLast" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_Nivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>SubNivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="cmbSubNivelLast" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_SubNivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>     
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="cmbGradoLast" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_Grado_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>    
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="cmbAulaLast" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep6_Aula_SelectedIndexChanged">
            </asp:DropDownList>     
        </td>
    </tr>     
      <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span># Mayor a :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
           <asp:DropDownList ID="cmbMayorLast" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
               >
            </asp:DropDownList>     
        </td>
    </tr>     
    </table>                            
</asp:Panel>--%>
<%--FIN DE REPORTE  DE ENFERMERIA --%>



        
   

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

    });
    
</script>

</asp:Content>

