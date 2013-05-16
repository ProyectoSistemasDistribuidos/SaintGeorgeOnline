<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="ReportesAcademicos.aspx.vb" Inherits="Modulo_Reportes_ReportesAcademicos" %>

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
        .style1
        {
            height: 25px;
            width: 127px;
        }
        .style2
        {
            height: 25px;
            width: 102px;
        }
        </style>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript" >

     function MostrarImpresionFotoAlumno_html() {

         window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FotosAlumnos_html.aspx', '_blank', '');
     }

    function ShowMyModalPopup() {
        var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
        modal.show();
    }

    function MostrarImpresionFichaMedica_html() {
        window.open('/SaintGeorgeOnline/Plantillas/Exportaciones/Plantilla_Rep_FichaAtencionHistorialClinico_html.aspx', '_blank', '');
    }
    function listar(url) {
        $("#link").html("<a href='" + url + "'>descargar</a>")
    }
      /*
      
      listar


 <td id="link">
             
            </td>
      */
</script>

<div id="miPaginaMantenimiento">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>    
        <asp:PostBackTrigger ControlID="btnReporteExportar" />
    </Triggers>
<ContentTemplate>

    <div style="border: solid 0px blue; width: 860px;">
                
    <div id="miBusquedaActualizacion_Ficha"><!-- 650px -->
    <table style="width: 990px;" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#113a6e; 
                width: 200px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
            <span style="padding-left: 30px;">Reporte</span>
            </td>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#113a6e; 
                width: 200px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
            <span style="padding-left: 30px;">Presentación</span>
            </td>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#113a6e; 
                width: 440px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="left" valign="middle">
            <span style="padding-left: 30px;">Filtros</span>
            </td>
            <td style="border-style:solid; border-width:1px;  border-color:#a6a3a3; background-color:#113a6e; 
                width: 150px; height: 26px; color:White; font-size:10px; font-family: Arial;" align="center" valign="middle">                                    
            </td>
        </tr>
        
        <tr>
            <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
    <asp:ListBox ID="lstReportes" runat="server" style="Height: 250px; Width: 300px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;" 
        OnSelectedIndexChanged="lstReportes_SelectedIndexChanged"  AutoPostBack="True">                 
    </asp:ListBox>   
            </td>
            
            <td style="border: solid 1px #a6a3a3; width: 200px; height: auto; font-size:10px;" valign="top" align="left">
    <asp:ListBox ID="lstPresentacion" runat="server" style="Height: 250px; Width: 350px; border: 0; font-weight: bold; font-size:11px; font-family: Arial;" 
        OnSelectedIndexChanged="lstPresentacion_SelectedIndexChanged"  AutoPostBack="True">                            
    </asp:ListBox>   
            </td>            
            
            <td style="border-style :solid; border-width:1px; border-color:#a6a3a3; width: 440px; height: auto; " valign="top" align="left">
                                
<asp:Panel ID="pnlReporte1" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Año Académico :</span></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnioAcademico1" runat="server" Width="100px">
                <asp:ListItem Value="0"> 2010 </asp:ListItem>
                <asp:ListItem Value="1"> 2009 </asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Nivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Nivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_Nivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>SubNivel :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_SubNivel" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_SubNivel_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>     
    <tr>
        <td align="left" valign="middle" class="style2" >                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep1_Grado_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>    
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep1_Aula" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true">
            </asp:DropDownList>  
        </td>
    </tr>     
    </table>                            
</asp:Panel> 
<asp:Panel ID="pnlReporte2" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="miTablaFiltros0" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" class="style2">                                
            <span>Año Académico :</span></td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlAnioAcademico2" runat="server" Width="100px">
                    <asp:ListItem Value="0"> 2010 </asp:ListItem>
                    <asp:ListItem Value="1"> 2009 </asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style1">
                <span>Mes :</span>
            </td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlMes" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style2">                                
            <span>Nivel :</span>  
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlRep1_Nivel1" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlRep1_Nivel1_SelectedIndexChanged"
                    style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style2">                                
            <span>SubNivel :</span>                            
        </td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlRep1_SubNivel1" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlRep1_SubNivel1_SelectedIndexChanged"
                    style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style2" >                                
            <span>Grado :</span>                            
        </td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlRep1_Grado1" runat="server" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlRep1_Grado1_SelectedIndexChanged" 
                    style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" class="style2">                                
            <span>Aula :</span>                            
        </td>
            <td align="left" style="width: 200px; height: 25px;" valign="middle">
                <asp:DropDownList ID="ddlRep1_Aula1" runat="server" AutoPostBack="true" 
                    style="width: 190px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Panel> 
<asp:Panel ID="pnlReporte3" runat="server" BackColor="White" style="width: 300px; height: 200px;">

    <table ID="Table6" border="0" cellpadding="0" cellspacing="0" style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">                                
            <span>Periodo:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="ddlPeriodoRepo3" runat="server" Width="220px" style="font-size: 8pt; font-family: Courier New;"
    OnSelectedIndexChanged="ddlPeriodoRepo3_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList> 
            </td>
        </tr>        
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">                                
            <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="ddlAsignacionAula3" runat="server" Width="220px" style="font-size: 8pt; font-family: Courier New;">
</asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="ddlBimestre3" runat="server" style="width: 220px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 
            </td>
        </tr>
        
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Tipo Nota:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="ddlTipoNotas" runat="server" style="width: 220px; font-size: 8pt; font-family: Arial;">
    <asp:ListItem Text="Notas Bimestrales" Value="1"></asp:ListItem>
    <asp:ListItem Text="Notas de Examenes" Value="0"></asp:ListItem>
</asp:DropDownList> 
            </td>
        </tr>           
        
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Tipo Asignatura:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="ddlTipoReporte" runat="server" style="width: 220px; font-size: 8pt; font-family: Arial;">
    <asp:ListItem Text="Asignaturas Totales" Value="1"></asp:ListItem>
    <asp:ListItem Text="Asignaturas Oficiales" Value="2"></asp:ListItem>
    <asp:ListItem Text="Asignaturas Internas" Value="3"></asp:ListItem>
</asp:DropDownList> 
            </td>
        </tr>            
    </table>

</asp:Panel> 
<asp:Panel Visible="false" ID="pnlReporteComparacion" runat="server" BackColor="White" style="width: 300px; height: 200px;">

    <table ID="Table3" border="0" cellpadding="0" cellspacing="0" style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">                                
                Grado<span>:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbPimariaComparacionBimestre" runat="server" Width="220px" style="font-size: 8pt; font-family: Courier New;">
</asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbBimestreA" runat="server" style="width: 125px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 

              
            </td>
        </tr>
        
         <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbBimestreB" runat="server" style="width: 125px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 

              
            </td>
        </tr>
    </table>

</asp:Panel> 
<asp:Panel ID="pnlReporteGradoPrimariaBimestre" runat="server" BackColor="White" style="width: 300px; height: 200px;">

    <table ID="Table4" border="0" cellpadding="0" cellspacing="0" style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">                                
                Grado<span>:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbReportePrimariaGradoBimestre" runat="server" Width="220px" style="font-size: 8pt; font-family: Courier New;">
</asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">      
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="cmbBimestreReportePrimariaGradoBimestre" runat="server" style="width: 125px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList> 

              
            </td>
        </tr>
        
         
    </table>

</asp:Panel>
<asp:Panel ID="pnlReporte4" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="miTablaFiltros1" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">   
                <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="ddlAsignacionAula4" runat="server" 
                    style="font-size: 8pt; font-family: Courier New;" Width="220px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="ddlBimestre4" runat="server" 
                    style="width: 125px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
          <td id="link">
            </td>
            
            
            
        </tr>
    </table>
</asp:Panel>  
<asp:Panel ID="pnlReporteAsistencia" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltrosAsistencia">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Año :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnioAcademico_Asist" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Bimestre :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlBimestre_Asist" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" >
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAula_Asist" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"
             AutoPostBack="true" OnSelectedIndexChanged="ddlAula_Asist_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>  
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Alumno :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAlumno_Asist" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
               >
            </asp:DropDownList>                             
        </td>
    </tr>    
    </table>                            
</asp:Panel>
 <asp:Panel ID="pnlReporteRetiro" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltrosRetiro">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Año :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnio_ret" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>   
    </table>                            
</asp:Panel>
<asp:Panel ID="pnlReporteFotos" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="Table5">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Año :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlBuscarAnioAcademicoFoto" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>   
     <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlBuscarGradoFoto" runat="server" OnSelectedIndexChanged="ddlBuscarGradoFoto_SelectedIndexChanged" AutoPostBack="true" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>
     <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Aula :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlBuscarAulaFoto" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;"  >
            </asp:DropDownList>                                       
        </td>
    </tr>
    </table>                            
</asp:Panel>
<asp:Panel ID="pnlPrimaria" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="miTablaFiltros1" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        
        <tr>
            <td align="left" valign="middle" style="width: 60px; height: 25px;">                                
            <span>Periodo:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
<asp:DropDownList ID="ddlPeriodoRepo4" runat="server" Width="220px" style="font-size: 8pt; font-family: Courier New;"
    OnSelectedIndexChanged="ddlPeriodoRepo4_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList> 
            </td>
        </tr>        
        
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="ddlSalonRepPrimaria" runat="server" 
                    style="font-size: 8pt; font-family: Courier New;" Width="220px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="cmbBimestrePrimaria" runat="server" 
                    style="width: 125px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
          <td id="Td1">
            </td>
            
            
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlDescriptores" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="Table1" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="dpsSalonDescriptores" runat="server" 
                    style="width: 220px; font-size: 8pt; font-family: Courier New;"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
<tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Curso:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="dpdCursosDescriptores" runat="server" 
                    style="width: 220px; font-size: 8pt; font-family: Courier New;">
                </asp:DropDownList>
          <td id="Td3">
            </td>
            
            
            
        </tr>
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="dpdBimestreDescriptores" runat="server" 
                    style="width: 125px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
          <td id="Td2">
            </td>
            
            
            
        </tr>
    </table>
</asp:Panel>
 <asp:Panel ID="pnlExportarRegistroNotas" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table ID="Table2" border="0" cellpadding="0" cellspacing="0" 
        style="border: solid 0x red; width: 280px; margin-left: 20px;">
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Salón:</span></td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="cmbSalonPrimariaInicial" runat="server" 
                    style="width: 220px; font-size: 8pt; font-family: Courier New;"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
<tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Curso:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="cmbCurso" runat="server" 
                    style="width: 220px; font-size: 8pt; font-family: Courier New;">
                </asp:DropDownList>
          <td id="Td4">
            </td>
            
            
            
        </tr>
        <tr>
            <td align="left" style="width: 60px; height: 25px;" valign="middle">
                <span>Bimestre:</span>
            </td>
            <td align="left" valign="middle" style="width: 240px; height: 25px;">
                <asp:DropDownList ID="DropDownList3" runat="server" 
                    style="width: 125px; font-size: 8pt; font-family: Arial;">
                </asp:DropDownList>
          <td id="Td5">
            </td>
            
            
            
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlReporteMatricula" runat="server" BackColor="White" style="width: 300px; height: 200px;">

    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Año Académico :</span></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnioAcademico_RepMatricula" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>    
    
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Estado:</span></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlTipoMatricula_RepMatricula" runat="server" Width="100px">
                <asp:ListItem Text="No Oficial" Selected="true" Value="2" ></asp:ListItem>
                <asp:ListItem Text="Oficial" Value="0" ></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    </table>
    
</asp:Panel>    
<asp:Panel ID="pnlReporteLibretas" runat="server" BackColor="White" style="width: 430px; height: 200px;">

<table cellpadding="0" cellspacing="0" border="0" style="width: 400px; border: solid 0px red; margin: 0 0 0 20px; padding: 0; font-size: 11px; font-family: Arial;">    
    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Periodo:</td>
        <td style="width: 330px; height: 25px;" align="left" valign="middle">
            <asp:DropDownList  ID="ddlAnioAcademicoLibretas" runat="server" 
                style="width: 90px; font-size: 8pt; font-family: Arial;" AutoPostBack="True">
            </asp:DropDownList>                  
        </td>    
    </tr>  

    <tr>
        <td align="left" style="width: 70px; height: 25px;" valign="middle">
            Fecha</td>
        <td align="left" style="width: 330px; height: 25px;" valign="middle">
            <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Bimestre:</td>
        <td style="width: 330px; height: 25px;" align="left" valign="middle">
<asp:DropDownList ID="ddlBimestreLibretas" runat="server" style="width: 320px; font-size: 8pt; font-family: Arial;">
</asp:DropDownList>                      
        </td>
    </tr>

    <tr>
        <td style="width: 70px; height: 25px;" align="left" valign="middle">Salon:</td>
        <td style="width: 330px; height: 25px;" align="left" valign="middle">
<asp:DropDownList ID="ddlSalonLibretas" runat="server" style="width: 320px; font-size: 8pt; font-family: Courier New;"
    OnSelectedIndexChanged="ddlSalonLibretas_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>      
        </td>
    </tr>
   
    <tr>
        <td style="width: 400px; height: 25px;" align="left" valign="top" colspan="2">    
    
    

    <div id="miGridviewMantActualizacion_Ficha" style="width: 400px; height: 26px; margin: 0; padding: 0; border: 0;">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 400px; height: 26px; color:White; background-color: #0a0f14; 
               font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;" class="miGVBusquedaFicha_Header">
            <tr>
                <td style="width: 20px; height: 26px;" align="center" valign="middle">
                    <span>&nbsp;&nbsp;</span>
                </td>
                <td style="width: 70px; height: 26px;" align="center" valign="middle">
                    <span>Codigo</span>
                </td>
                <td style="width: 260px; height: 26px;" align="center" valign="middle">  
                    <span>Apellidos y Nombres</span>
                </td>
                <td style="width:  30px; height: 26px;" align="center" valign="middle"> 
                    <asp:CheckBox ID="chkAll1" runat="server" /><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </td>   
                <td style="width:  20px; height: 26px;" align="center" valign="middle"> 
                    <span>&nbsp;&nbsp;&nbsp;</span>
                </td>                    
            </tr>
        </table>      
    </div>     
              
        </td> 
    </tr>
    <tr>
        <td style="width: 400px; height: 25px;" align="left" valign="top" colspan="2">       

    <div style="overflow-y: scroll; overflow-x: hidden; width:397px; height: 146px; margin: 0; padding: 0; border: solid 1px #a6a3a3; ">   
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="miGridviewBusqueda" 
            Width="380px"
            GridLines="None" 
            ShowFooter="false"
            ShowHeader="false"
            AutoGenerateColumns="False"
            EmptyDataText=" - No se encontraron resultados - "                         
            OnRowDataBound="GridView1_RowDataBound">
        <HeaderStyle CssClass="miGridviewBusqueda_Header" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataRowStyle ForeColor="#A51515" HorizontalAlign="Center" />                                                                                
            <Columns>
<asp:TemplateField HeaderText="idx" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" />
    </ItemTemplate>   
</asp:TemplateField>         
            
<asp:TemplateField HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
    <ItemTemplate>
        <asp:Label ID="lblCodigoAlumno" runat="server" Text='<%# Bind("CodigoAlumno") %>' />
    </ItemTemplate>
</asp:TemplateField>                
<asp:TemplateField HeaderText="Nombre de Alumno" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" ItemStyle-Width="260px">
    <ItemTemplate>
        <asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>' />
    </ItemTemplate>
</asp:TemplateField>                 
<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="left" ItemStyle-Width="30px">
    <ItemTemplate>
        <asp:CheckBox ID="chk" runat="server" />
    </ItemTemplate>
</asp:TemplateField>     
            </Columns>
        </asp:GridView>     
    </div>            
                
        </td> 
    </tr>
</table>

</asp:Panel>  
<asp:Panel ID="pnlReporteTaller1" runat="server" BackColor="White" style="width: 280px; height: 150px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 260px; margin-left: 20px;" id="miTablaFiltros">
    
    <tr>
        <td style="width: 50px; height: 25px;" align="left" valign="middle">
            <span>Año:</span>   
        </td>
        <td style="width: 210px; height: 25px;" align="left" valign="middle">
            <asp:DropDownList ID="ddlRep_Taller1_PeriodoAcademico" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" >
            </asp:DropDownList> 
        </td>
    </tr>   
    <tr>
            <td style="width: 50px; height: 25px;" align="left" valign="middle">
                <span>Bimestre:</span>   
            </td>
            <td style="width: 210px; height: 25px;" align="left" valign="middle">
                <asp:DropDownList ID="ddlRep_Taller1_Bimestre" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" >
                </asp:DropDownList> 
            </td>
    </tr>   
    <tr>
        <td style="width: 50px; height: 25px;" align="left" valign="middle">                                
            <span>Taller:</span>                            
        </td>
        <td style="width: 210px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep_Taller1" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>
    <tr>
        <td style="width: 50px; height: 25px;" align="left" valign="middle">                                
            <span>Grado:</span>                            
        </td>
        <td style="width: 210px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep_Taller1_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlRep_Taller1_Grado_SelectedIndexChanged">
            </asp:DropDownList>                             
        </td>
    </tr>    
    <tr>
        <td style="width: 50px; height: 25px;" align="left" valign="middle">                                
            <span>Aula:</span>                            
        </td>
        <td style="width: 210px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRep_Taller1_Aula" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>     
        </td>
    </tr>     
    </table>                            
</asp:Panel>

<asp:Panel ID="panelSubjectProgress" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 300px; margin: 0; padding: 0;" id="Table7">
    <tr>
        <td style="width: 20px; height: 25px;" align="left" valign="middle">
        <td style="width: 60px; height: 25px;" align="left" valign="middle">                                
<span>Periodo:</span>                            
        </td>
        <td style="width: 220px; height: 25px;" align="left" valign="middle">         
    <asp:DropDownList ID="ddlPeriodoAcademico_spr" runat="server" style="width: 120px; font-size: 8pt; font-family: Arial;" 
        OnSelectedIndexChanged="ddlPeriodoAcademico_spr_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>                                                 
        </td>
    </tr>
    <tr>
        <td style="width: 20px; height: 25px;" align="left" valign="middle">
        <td style="width: 60px; height: 25px;" align="left" valign="middle">                                
<span>Bimestre:</span>                            
        </td>
        <td style="width: 220px; height: 25px;" align="left" valign="middle"> 
    <asp:DropDownList ID="ddlBimestre_spr" runat="server" style="width: 120px; font-size: 8pt; font-family: Arial;" >
    </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 20px; height: 25px;" align="left" valign="middle">
        <td style="width: 60px; height: 25px;" align="left" valign="middle">                                
<span>Salon:</span>                            
        </td>
        <td style="width: 220px; height: 25px;" align="left" valign="middle">  
    <asp:DropDownList ID="ddlAsignacionAula_spr" runat="server" Width="210px" style="font-size: 8pt; font-family: Courier New;" 
        OnSelectedIndexChanged="ddlAsignacionAula_spr_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>   
        </td>
    </tr>       
    <tr>
        <td style="width: 20px; height: 25px;" align="left" valign="middle">
        <td style="width: 60px; height: 25px;" align="left" valign="middle">                                
<span>Curso:</span>                            
        </td>
        <td style="width: 220px; height: 25px;" align="left" valign="middle">  
    <asp:DropDownList ID="ddlAsignacionCursosAnio_spr" runat="server" style="width: 210px; font-size: 8pt; font-family: Courier New;" 
        OnSelectedIndexChanged="ddlAsignacionCursosAnio_spr_SelectedIndexChanged" AutoPostBack="true"> 
    </asp:DropDownList> 
        </td>
    </tr>
    <tr>
        <td style="width: 20px; height: 25px;" align="left" valign="middle">
        <td style="width: 60px; height: 25px;" align="left" valign="middle">                                
<span>Grupo:</span>                            
        </td>
        <td style="width: 220px; height: 25px;" align="left" valign="middle">  
    <asp:DropDownList ID="ddlAsignacionGruposCursos_spr" runat="server" style="width: 210px; font-size: 8pt; font-family: Courier New;" 
        OnSelectedIndexChanged="ddlAsignacionGruposCursos_spr_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>  
        </td>
    </tr>   
    <tr>
        <td style="width: 20px; height: 25px;" align="left" valign="middle">
        <td style="width: 60px; height: 25px;" align="left" valign="middle">                                
<span>Alumno:</span>                            
        </td>
        <td style="width: 220px; height: 25px;" align="left" valign="middle">  
        </td>
    </tr>  
    <tr>
        <td style="width: 300px;" align="left" valign="top" colspan="3">  
<table cellpadding="0" cellspacing="0" border="0" 
    style="width: 302px; height: 25px; border: solid 1px #a6a3a3;
           color: #23527e; background-color: #a1b5cd; margin: 0; padding: 0;
           font-size: 10px; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;">
    <tr>
            <td style="width:  20px; height: 25px;" align="center" valign="middle">
            </td>
            <td style="width:  60px; height: 25px;" align="center" valign="middle">
                <span>Código</span>                                                                 
            </td>
            <td style="width:  200px; height: 25px;" align="center" valign="middle">
                <span>Nombre Completo</span>                                                                 
            </td>
            <td style="width:  22px; height: 25px;" align="center" valign="middle">   
                <asp:CheckBox ID="chkAll" runat="server" />                                                
            </td>                    
    </tr>
</table>    
<div style=" width:300px; height: auto; margin: 0; padding: 0; border: solid 1px #a6a3a3;">   
<asp:GridView ID="GridView1_spr" runat="server" 
    Width="300px" 
    CssClass="miGridviewBusqueda" 
    GridLines="None" 
    AutoGenerateColumns="false"
    AllowPaging="false" 
    AllowSorting="false"
    ShowFooter="false"
    ShowHeader="false"
    EmptyDataText=" - No se encontraron resultados - "
    OnRowDataBound="GridView1_spr_RowDataBound">
<Columns>
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblidx" runat="server" /> 
    </ItemTemplate>
</asp:TemplateField>
 
<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblcAlumno" runat="server" Text='<%# Bind("cAlumno") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 

<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">                                                                                 
    <ItemTemplate>
        <asp:Label ID="lblnAlumno" runat="server" Text='<%# Bind("nAlumno") %>' /> 
    </ItemTemplate>
</asp:TemplateField> 

<asp:TemplateField ItemStyle-CssClass="miGridviewBusqueda_Rows" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">                                                                                 
    <ItemTemplate>
        <asp:CheckBox ID="chkG" runat="server" />
        <asp:Label ID="lblcodAnioAcademico" runat="server" Text='<%# Bind("cPeriodo") %>' style="display: none;" /> 
        <asp:Label ID="lblcodBimestre" runat="server" Text='<%# Bind("cBimestre") %>' style="display: none;" /> 
        <asp:Label ID="lblcodAsignacionGrupo" runat="server" Text='<%# Bind("cAsigGrupo") %>' style="display: none;" /> 
    </ItemTemplate>
</asp:TemplateField> 

</Columns>
</asp:GridView> 
</div>            
        </td>
    </tr>                 
    
    </table>                            
</asp:Panel>  
<asp:Panel ID="PanelSeguro" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Periodo :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlSeguro_Periodo" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                                       
        </td>
    </tr>
    </table>                            
</asp:Panel>    
<asp:Panel ID="PanelNotasLibretaSenior" runat="server" BackColor="White" style="width: 300px; height: 300px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Bimestre:</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
<asp:DropDownList ID="ddlBimestre" runat="server" style="width: 190px; font-size: 8pt; font-family: Courier New;">
</asp:DropDownList>                                       
        </td>
    </tr>
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Salon:</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">      
<asp:DropDownList ID="ddlAsignacionAula" runat="server" Width="190px" style="font-size: 8pt; font-family: Courier New;" 
    OnSelectedIndexChanged="ddlAsignacionAula_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>  
        </td>
    </tr>
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Curso:</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">  
<asp:DropDownList ID="ddlAsignacionCursosAnio" runat="server" style="width: 190px; font-size: 8pt; font-family: Courier New;" 
    OnSelectedIndexChanged="ddlAsignacionCursosAnio_SelectedIndexChanged" AutoPostBack="true"> 
</asp:DropDownList>     
        </td>
    </tr>
    <tr>
        <td style="width: 80px; height: 25px;" align="left" valign="middle">                                
            <span>Grupo:</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">   
<asp:DropDownList ID="ddlAsignacionGruposCursos" runat="server" style="width: 190px; font-size: 8pt; font-family: Courier New;">
</asp:DropDownList>    
        </td>
    </tr>        
    
</table>   
</asp:Panel>
<asp:Panel ID="pnlReporte_Tercio" runat="server" BackColor="White" style="width: 300px; height: 200px;">
    <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Año Académico :</span></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlAnioAcademico_Tercio" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>
        </td>
    </tr>  
    <tr>
        <td align="left" valign="middle" class="style2" >                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlGrado_Tercio" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
            </asp:DropDownList>                             
        </td>
    </tr>      
    </table>                            
</asp:Panel> 

      
<asp:Panel ID="pnlMidtermReport" runat="server" style="background-color:#FFFFFF; width:250px;">
   
    
    
   
    
           
            <div style="border: solid 0px blue; width: 860px;">            
                <div id="Div2">
                    <!-- 650px -->
                    
                        <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 820px;">
                            <tr>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Mes :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 450px; height: 25px;" align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="ddlAsignacion" runat="server" Width="300px" style="font-size: 8pt; font-family: Courier New;">
                                    </asp:DropDownList>
                                </td>                                                
                                <td style="width: 320px; height: 25px;" align="right" valign="middle" colspan="5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Grado :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 450px; height: 25px;" align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="ddlGrado" runat="server" Width="196px" style="font-size: 8pt; font-family: Courier New;"
                                        OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>                                                
                                <td style="width: 320px; height: 25px;" align="right" valign="middle" colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Aula :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 450px; height: 25px;" align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="ddlAula" runat="server" Width="198px" style="font-size: 8pt; font-family: Courier New;"
                                        OnSelectedIndexChanged="ddlAula_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>                                                
                                <td style="width: 320px; height: 25px;" align="right" valign="middle" colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px; height: 25px;" align="left" valign="middle">
                                    <span>Alumno :&nbsp;</span>
                                </td>                                                  
                                <td style="width: 450px; height: 25px;" align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="ddlAlumno" runat="server" Width="197px" 
                                        style="font-size: 8pt; font-family: Courier New;">
                                    </asp:DropDownList>
                                </td>                                                
                                <td style="width: 320px; height: 25px;" align="right" valign="middle" colspan="5">
                                </td>
                            </tr>
                            
                        </table>
                     
                </div>                                                                          
                <div class="miEspacio"></div>   
            </div>            
      
     
    
   
  
 
      
        
        <asp:Panel ID="Panel1" runat="server" style="background-color:#FFFFFF;display:none;width:250px">
            <div style="margin: auto;">
            <table cellpadding="0" cellspacing="0" border="0" style="width:250px; border: solid 1px #000000">
                <tr><td colspan="3"></td>
                    <td style="width: 20px;" align="center" valign="middle">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/Imagenes/cross_icon_normal.png"   />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" align="left" valign="middle"></td>
                    <td style="width: 80px;" align="left" valign="middle">
                        <img alt="Procesando..." src="../App_Themes/Imagenes/ajax-loader.gif" />  
                    </td>
                    <td style="width: 130px;" align="left" valign="middle">
                        <span style="color: #6684b7; font-family: Arial; font-size: 9pt; font-weight: bold;">Procesando...<br />Espere un momento.</span>
                    </td>
                    <td style="width: 20px;" align="left" valign="middle">
                        
                    </td>
                </tr>
                <tr><td colspan="4"><br /></td></tr>
            </table>                              
            </div>
        </asp:Panel>        
    
        <asp:Label ID="Label1" runat="server" Text="MiModalHandler" style="display:none;"/> 
      
  

     </asp:Panel>
<asp:Panel ID="pnlReporteMeritoDemerito" runat="server" style="background-color:#FFFFFF; width:250px;">
     <table>
     <tr>
     <td>
         Anio academico</td>
      <td>
      <asp:DropDownList ID="cmbPrimariaInicialAnioAcademico0" runat="server">
                                        </asp:DropDownList>                                              
      </td>
     </tr>
         <tr>
             <td>
                 Bimestre
             </td>
             <td>
                 <asp:DropDownList ID="ddlBuscarBimestre" runat="server" AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlBuscarBimestre_SelectedIndexChanged" 
                     style="font-size: 8pt; font-family: Arial;" Width="200px">
                 </asp:DropDownList>
             </td>
         </tr>
      <tr>
     <td>Salon </td>
      <td> <asp:DropDownList ID="ddlBuscarSalon" runat="server" Width="250px" style="font-size: 8pt; font-family: Arial;"
                                         OnSelectedIndexChanged="ddlBuscarSalon_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList> </td>
     </tr>
     <tr>
     <td>Alumnos</td>
     <td><asp:DropDownList ID="ddlAlumnos" runat="server" Width="250px" style="font-size: 8pt; font-family: Arial;"
                                          AutoPostBack="true">
                                        </asp:DropDownList></td>
     </tr>
     </table>
     
      </asp:Panel>
<asp:Panel ID="pnlConductaTutoria" runat="server" style="background-color:#FFFFFF; width:250px;">
       <table>
            <tr>
               <td>Anio Academico</td>
               <td>
                   <asp:DropDownList ID="cmbPrimariaInicialAnioAcademico" runat="server">
                   </asp:DropDownList>
                </td
            </tr>
            <tr>
                <td>Grado</td>
                <td>
                    <asp:DropDownList ID="cmbGradoReportePrimaria" runat="server">
                    </asp:DropDownList>
                </td
            </tr>
             <tr>
                 <td></td>
                 <td></td
             </tr>
           
       </table>
        </asp:Panel>
<asp:Panel ID="pnlTutorReport" runat="server">
        <table>
        <tr>
        <td>
        Bimestre
        </td>
   
   
        <td>
        
            <asp:DropDownList ID="cmbBimestreTutor" runat="server" Height="27px" Width="156px">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
            </asp:DropDownList>
        
        </td>
        </tr>
        
         <tr>
        <td>
        
            salon</td>
        
        <td>
        
            <asp:DropDownList ID="cmbSalonTutor" runat="server" Height="16px" Width="147px">
            </asp:DropDownList>
        
        </td>
        </tr>
        </table>
        
          </asp:Panel>


<asp:Panel ID="pnlReporte_GradoPronostico" runat="server" BackColor="White" style="width: 300px; height: 200px;">          
 <table cellpadding="0" cellspacing="0" border="0" style="border: solid 0x red; width: 280px; margin-left: 20px;" id="miTablaFiltros">
    <tr>
        <td align="left" valign="middle" class="style2">                                
            <span>Año Académico :</span></td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRepGradoPro_Periodo" runat="server" style="width: 100px; font-size: 8pt; font-family: Arial;" >
            </asp:DropDownList>
        </td>
    </tr>
    
    <tr>
        <td align="left" valign="middle" class="style2" >                                
            <span>Grado :</span>                            
        </td>
        <td style="width: 200px; height: 25px;" align="left" valign="middle">                                
            <asp:DropDownList ID="ddlRepGradoPro_Grado" runat="server" style="width: 190px; font-size: 8pt; font-family: Arial;">
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
        PopupControlID="pnlImpresion"
        TargetControlID="lblAccionExportar">
    </atk:ModalPopupExtender>
    <asp:Panel ID="pnlImpresion" runat="server" BackColor="White" Height="71px" Width="388px" Style="display: none">
        <table style="width: 100%; border: solid 1px #000000; background-color: #ffffff;" border="0" cellpadding="0" cellspacing="0">  
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
<div>
 
<div id="link">
<span style="display: none;">ghjghjghj</span>
</div>
</div>
</div>

<script type="text/javascript" src="../App_Themes/Javascript/jquery-1.4.1.min.js" ></script> 
<script type="text/javascript">

    $(document).ready(function() {

        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);

        var grid1 = document.getElementById("<%= GridView1.ClientID %>");
        $('input:checkbox[id$=chkAll1]').click(function(e) {
            if (grid1 != undefined) {
                if (grid1.rows.length > 0) {
                    var estadoCheck = $(this).is(':checked');
                    $(grid1).find("input:checkbox").each(function(e, checkbox) {
                        if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                        else { $(checkbox).removeAttr('checked'); }
                    });
                }
            }
        });

        var gridAulas = document.getElementById("<%= GridView1_spr.ClientID %>");
        $('input:checkbox[id$=chkAll]').click(function(e) {
            if (gridAulas != undefined) {
                if (gridAulas.rows.length > 0) {
                    var estadoCheck = $(this).attr('checked');
                    $(gridAulas).find("input:checkbox").each(function(e, checkbox) {
                        if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                        else { $(checkbox).removeAttr('checked'); }
                    });
                }
            }
        });   

    });

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            var grid1 = document.getElementById("<%= GridView1.ClientID %>");
            $('input:checkbox[id$=chkAll1]').click(function(e) {
                if (grid1 != undefined) {
                    if (grid1.rows.length > 0) {
                        var estadoCheck = $(this).is(':checked');
                        $(grid1).find("input:checkbox").each(function(e, checkbox) {
                            if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                            else { $(checkbox).removeAttr('checked'); }
                        });
                    }
                }
            });

            var gridAulas = document.getElementById("<%= GridView1_spr.ClientID %>");
            $('input:checkbox[id$=chkAll]').click(function(e) {
                if (gridAulas != undefined) {
                    if (gridAulas.rows.length > 0) {
                        var estadoCheck = $(this).attr('checked');
                        $(gridAulas).find("input:checkbox").each(function(e, checkbox) {
                            if (estadoCheck == true) { $(checkbox).attr('checked', true); }
                            else { $(checkbox).removeAttr('checked'); }
                        });
                    }
                }
            });
            
        }
    }        
    
</script>

</asp:Content>

