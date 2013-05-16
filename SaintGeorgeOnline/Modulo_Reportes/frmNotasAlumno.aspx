<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNotasAlumno.aspx.vb" Inherits="Modulo_Reportes_frmNotasAlumno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     
    <link href="../jQuery%20BlockUI%20Plugin%20(v2)_archivos/block.css" rel="stylesheet"
        type="text/css" />
        
      <!--Archivos de Javascripts -->
       <script src="/SaintGeorgeOnline/App_Themes/Javascript/jquery-1.7.1.min.js" type="text/javascript"></script>
  <%--  <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery-1.4.1.min.js"></script>--%>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/sexyalertbox.v1.2.js"></script>
  <%--  <script type="text/javascript" src="/SaintGeorgeOnline/App_Themes/Javascript/jquery.blockUI.js"></script> --%>      
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
    <link href="../App_Themes/Estilos/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/Javascript/jquery.alerts.js" type="text/javascript"></script>
    <!--Scripts Internos -->  
   <%--mis estilos para jquery  --%>
    <link href="../App_Themes/Estilos/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
   <%--fin estilos jqueyr --%>
   <%--mis script inicio--%>
    <script src="../App_Themes/Javascript/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <script src="../App_Themes/Javascript/json2.js" type="text/javascript"></script>
    <script src="../jQuery%20BlockUI%20Plugin%20(v2)_archivos/jquery.blockUI.js" type="text/javascript"></script>
   <%--fin mis script --%>
    
     
    
    
    
    <title>
    
    </title>
    <style>
        
        .nombreCurso
        {
            
            
        color:#5798E9;
          
        }
        
       .tabla th {
 padding: 5px;
 font-size: 16px;
 background-color: #83aec0;
  background-repeat: repeat-x;
 color: #FFFFFF;
 border-right-width: 1px;
 border-bottom-width: 1px;
 border-right-style: solid;
 border-bottom-style: solid;
 border-right-color: #558FA6;
 border-bottom-color: #558FA6;
  text-transform: uppercase;
 }
 
 .tabla .modo1 {
 font-size: 12px;
 font-weight:bold;
 background-color: #e2ebef;
  background-repeat: repeat-x;
 color: #34484E;
 
 }
 .tabla .modo1 td {
 padding: 5px;
 border-right-width: 1px;
 border-bottom-width: 1px;
 border-right-style: solid;
 border-bottom-style: solid;
 border-right-color: #A4C4D0;
 border-bottom-color: #A4C4D0;
 text-align:right;
 } 
 .tabla .modo1 th {
  background-position: left top;
 font-size: 12px;
 font-weight:bold;
 text-align: left;
 background-color: #e2ebef;
 background-repeat: repeat-x;
 color: #34484E;
  border-right-width: 1px;
 border-bottom-width: 1px;
 border-right-style: solid;
 border-bottom-style: solid;
 border-right-color: #A4C4D0;
 border-bottom-color: #A4C4D0;
 }
 .tabla .modo2 {
 font-size: 12px;
 font-weight:bold;
 background-color: #fdfdf1;
  background-repeat: repeat-x;
 color: #990000;
  text-align:left;
 }
 .tabla .modo2 td {
 padding: 5px;
 border-right-width: 1px;
 border-bottom-width: 1px;
 border-right-style: solid;
 border-bottom-style: solid;
 border-right-color: #EBE9BC;
 border-bottom-color: #EBE9BC;
 }
 .tabla .modo2 th {
  background-position: left top;
 font-size: 12px;
 font-weight:bold;
 background-color: #fdfdf1;
 background-repeat: repeat-x;
 color: #990000;
 
 text-align:left;
 border-right-width: 1px;
 border-bottom-width: 1px;
 border-right-style: solid;
 border-bottom-style: solid;
 border-right-color: #EBE9BC;
 border-bottom-color: #EBE9BC;
 }
        </style>
    
    <script>
    
     
    
        var lstJson =<%=listaJsonLibreta %>
 var lstJsAsistencias =<%=jsAsistensias %>
      
      
        function tablaNotas(lstLibreta,lstAsistencias) {
        
        
        
        
            var html = "<table class='tabla'>";
            html += "<thead style='background-color:#629EDB'>";
             html += "<tr>"
            html += "<th>Curso/Area</th>";
            html += "<th>1</th>";
            html += "<th>2</th>";
            html += "<th>3</th>";
            html += "<th>4</th>";
            html += "<th>Anual</th>";
              html += "</tr>"
            html += "</thead>";
              html += "<tbody>";

            for (var indice = 0; indice < lstLibreta.length; indice++) {
                html += "<tr class='modo1'>"
                html += "<td class='nombreCurso'><a>" + lstLibreta[indice].nombreCurso + "</a></td>"
                html += "<td>" + lstLibreta[indice].notaAnual + "</td>"
                for (var subInd = 0; subInd < lstLibreta[indice].lstNotaCurso.length; subInd++) {
                    html += "<td>" + lstLibreta[indice].lstNotaCurso[subInd].notaCurso + "</td>"
                }
                html += "</tr>"
            }
          
            
            /**/
            html += "<tr class='modo1'>"
html +="<td style='background-color:#629EDB'>Comportamiento</td>"
html +="<td style='background-color:#629EDB'>I</td>"
html +="<td style='background-color:#629EDB'>II</td>"
html +="<td style='background-color:#629EDB'>III</td>"
html +="<td style='background-color:#629EDB'>IV</td>"
html +="<td style='background-color:#629EDB'>Anual</td>"
html +="</tr>"
html += "<tr class='modo1'>"
html +="<td>Inasistencias Justificadas</td>"
html +="<td>"+lstAsistencias[0].FaltaJustificada1+"</td>"
html +="<td>"+lstAsistencias[0].FaltaJustificada2+"</td>"
html +="<td>"+lstAsistencias[0].FaltaJustificada3+"</td>"
html +="<td>"+lstAsistencias[0].FaltaJustificada4+"</td>"
html +="<td> </td>"
html += "</tr>"
            /**/
html += "<tr class='modo1'>"
html +="<td>Inasistencias Injustificadas</td>"
html +="<td>"+lstAsistencias[0].FaltaSinJustificar1+"</td>"
html +="<td>"+lstAsistencias[0].FaltaSinJustificar2+"</td>"
html +="<td>"+lstAsistencias[0].FaltaSinJustificar3+"</td>"
html +="<td>"+lstAsistencias[0].FaltaSinJustificar4+"</td>"
html +="<td> </td>"
html += "</tr>"
            /**/
                  /**/
html += "<tr class='modo1'>"
html +="<td>Tardanza Injustificadas</td>"
html +="<td>"+lstAsistencias[0].TardanzaSinJustificar1+"</td>"
html +="<td>"+lstAsistencias[0].TardanzaSinJustificar2+"</td>"
html +="<td>"+lstAsistencias[0].TardanzaSinJustificar3+"</td>"
html +="<td>"+lstAsistencias[0].TardanzaSinJustificar4+"</td>"
html +="<td> </td>"
html += "</tr>"
            /**/
      
                     /**/
html += "<tr class='modo1'>"
html +="<td>Tardanza Justificadas</td>"
html +="<td>"+lstAsistencias[0].TardanzaJustificada1+"</td>"
html +="<td>"+lstAsistencias[0].TardanzaJustificada2+"</td>"
html +="<td>"+lstAsistencias[0].TardanzaJustificada3+"</td>"
html +="<td>"+lstAsistencias[0].TardanzaJustificada4+"</td>"
html +="<td> </td>"
html += "</tr>"
            
            
            
             html += "</tbody>";
            html += "</table>"
 

            return html
        }
        
        function tablaAsistencias()
        {
        var tbAsistencias="";
        
        
 


 
 
        tbAsistencias += "</table>"
        }
         
         $(document).ready(function (){
         var html="";
         html=tablaNotas(lstJson,lstJsAsistencias);
         $("#tabla").html(html);
         
         /*
           var lstJson =<%=listaJsonLibreta %>
 var lstJsAsistencias =<%=jsAsistensias %>
         */
         
         });
         
         
    </script>
    <style type="text/css">
        .style1
        {
            width: 114px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tabla">
    
  
    
    </div>
    </form>
</body>
</html>
