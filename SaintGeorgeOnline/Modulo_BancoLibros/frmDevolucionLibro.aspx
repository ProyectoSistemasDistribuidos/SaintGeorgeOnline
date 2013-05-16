<%@ Page Title="" Language="VB" MasterPageFile="~/PaginaPrincipal.master" AutoEventWireup="false" CodeFile="frmDevolucionLibro.aspx.vb" Inherits="Modulo_BancoLibros_frmDevolucionLibro" %>
<%@ MasterType VirtualPath="~/PaginaPrincipal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
    .container 
    {
        margin: 0; 
        padding: 0; 
        width: 1002px; 
        height: auto;
        border: solid 1px #ffffff;
    }
    .drow
    {
        margin: 0; 
        padding: 0; 
        width: 1000px; 
        height: 25px;
        }
    .wrap
    {
        line-height: 25px; 
        float: left; 
        text-align: left; 
        vertical-align: middle;          
    }
    .text
        {
        width:140px;
        height:23px;
        font-size:8pt;
        text-align: left;         	 
    }
    .miboton
    {
        text-decoration: none; 
        background-color: #4b8efa; 
        color: #ffffff; 
        border: 0;
        font-size: 12px;  
        display: block; line-height: 20px; float: left;
        padding: 0 5px; 
        margin: 0 5px 0 0;
        width: 80px;
        vertical-align: middle; text-align: center;   
        border: solid 1px #3079ed;   
    }
    .miboton:hover
    {
        text-decoration: none; 
        background-color: #0072bb;
        color: #ffffff; 
        border: 0;font-size: 12px; 
        display: block; line-height: 20px; float: left; 
        padding: 0 5px; 
        margin: 0 5px 0 0;
        width: 80px;
        vertical-align: middle; text-align: center;   
        border: solid 1px #2f5bb7; 
    } 
    
    #lstPrestamo th
    {
        font-size:8pt; height:25px; text-align:center; color: #ffffff; background-color: #555555; font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif;                
    } 
    #lstPrestamo td
    {
        font-size:8pt; height:25px; font-family: Verdana, Arial, Helvetica, sans-serif; border-bottom: solid 1px #a6a3a3; border-left: solid 1px #a6a3a3;     
    }       
    
</style>
    
     <script type="text/javascript">
         $(document).ready(function() {

            $('#aspnetForm').submit(function() {
                 return false;
             });
                  
         })
     </script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="main" class="container" style="margin: 0 0 0 10px;">
      
<div class="drow">  
    <div class="wrap" style="width: 100px;">
        <span>Periodo:</span>
    </div>    
    <div class="wrap" style="width: 150px;">
        <select id="cmbPeriodo" name="D1" class="text">
        <option value="0">--Seleccione--</option>
            <%For Each flAnioAcademico As Data.DataRow In dtAnio.Rows%>        
            <%If Date.Now.Year = flAnioAcademico("Descripcion") Then%>
        <option selected=selected value="<%=flAnioAcademico("Codigo") %>"><%=flAnioAcademico("Descripcion")%></option>
            <%Else%>
        <option value="<%=flAnioAcademico("Codigo") %>"><%=flAnioAcademico("Descripcion")%></option>
            <%End If%>
            <%Next%>
        </select>
    </div>
    <div class="wrap" style="width: 150px;">     
    </div>
    <div class="wrap" style="width: 600px;">
    </div>
    
</div>   
<div class="drow">  
    <div class="wrap" style="width: 100px;">
        <span>Código Barra:</span>
    </div>
    <div class="wrap" style="width: 150px;">
        <input id="tbCodigoBarra" type="text" class="text" maxlength="20" />
    </div>
    <div class="wrap" style="width: 750px;">    
        <a id="lnkbuscar" class="miboton" href="#">Buscar</a>   
        &nbsp;
        <a id="lnkdevolver" class="miboton" href="#">Devolver</a>    
    </div>
</div>    
    <br />
<div class="drow">  
    <div id="pnlResultado" style="height:auto; overflow:hidden; width:1000px;; margin:0; padding: 0;">
    </div>    
</div>    
    
    
</div>

<script type="text/javascript" src="../App_Themes/Javascript/jquery-1.4.1.min.js" ></script> 
<script type="text/javascript" src="../App_Themes/Javascript/jquery.blockUI.js"></script>   
<script type="text/javascript" src="../App_Themes/Javascript/jquery.textchange.min.js" ></script> 
<link rel="stylesheet" type="text/css" media="all" href="../App_Themes/Estilos/blockUI.css" /> 


<script type="text/javascript">

    $(document).ready(function() {

    $('#aspnetForm').submit(function() {
        return false;
    });
    
        $("#imgControl").attr("src", '/SaintGeorgeOnline/App_Themes/Imagenes/menuShow.png');
        $("#menu").hide('fast');
        $("#menu").width(0);
        $("#contenido").width(893);

        $('#tbCodigoBarra').bind('textchange', function(event, previousText) {
            if ($(this).val().length == 11) {
                consultarPrestamo();
            }
        });
        $("#lnkbuscar").click(function() {
            if ($('#tbCodigoBarra').val().length == 11) {
                consultarPrestamo();
            } else {
                alert('Debe ingresar un codigo de barra con formato correcto.')
            }
        });
        $("#lnkdevolver").click(function() {
            if ($('#tbCodigoBarra').val().length == 11) {
                registrarDevolucion();
            }
        });

    });

    function registrarDevolucion() {
        var pCodDetalle = 0;
        if ($('#lstPrestamo').length > 0) {
            var rowCount = $('#lstPrestamo tr').length;
            if (rowCount > 1) {
                pCodDetalle = $('#lstPrestamo tr:eq(1) td:eq(1)').html();
            }             
        }        
        if (!(pCodDetalle > 0)){
            return false;
        }
        $.blockUI({
            message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" />Buscando ...</h4>'
        });
        $.ajax({
            url: "frmDevolucionLibro.aspx/devolverLibro",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                codDetalle: pCodDetalle
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {
                $.unblockUI();              
                $("#pnlResultado").html("");
                if (res.d[0] > 0) {
                    Sexy.info(res.d[1]);
                } else {
                    Sexy.alert(res.d[1]);
                }
            }
            , error: function(xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
            }
        });  
        
    }

    function consultarPrestamo() {
        var codigoBarra = $('#tbCodigoBarra').val();        
        $.blockUI({
            message: '<h4><img src="../App_Themes/Imagenes/barrita.gif" />Buscando ...</h4>'
        });
        var pCodAnio = 0;
        var pCodBarra = "";
        pCodAnio = parseInt($("#cmbPeriodo option:selected").val());
        pCodBarra = $.trim($("#tbCodigoBarra").val());
        $.ajax({
            url: "frmDevolucionLibro.aspx/buscarPrestamo",
            async: false,
            cache: false,
            type: "post",
            data: JSON.stringify({
                codAnio: pCodAnio,
                codBarra: pCodBarra
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(res, textStatus, jqXHR) {
                $("#pnlResultado").html(res.d.html);
                $.unblockUI();
                //1PL0365-001
                e.preventdefault();
                //return false;   
            }
            , error: function(xhr, ajaxOptions, thrownError) {
                alert(xhr.status); alert(thrownError);
            }
        });

    }
    
    function Over(ctr) {
        $(ctr).css("backgroundColor", "#DEE8F5");
    }
    function Out(ctr) {
        $(ctr).css("backgroundColor", "#FFFFFF");
    }


</script>

</asp:Content>

