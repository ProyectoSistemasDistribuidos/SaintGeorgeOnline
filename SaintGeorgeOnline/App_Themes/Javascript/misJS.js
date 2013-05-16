        window.moveTo(0, 0);
	    window.resizeTo(screen.width, screen.height);

	    document.write("<link REL='stylesheet' HREF='../App_Themes/Estilos/miCalendario.css' TYPE='text/css'>");

        if ($.browser.msie) {
            document.write("<link REL='stylesheet' HREF='../App_Themes/Estilos/misEstilos.css' TYPE='text/css'>");
        } else if ($.browser.mozilla) {
            document.write("<link REL='stylesheet' HREF='../App_Themes/Estilos/misEstilosFF.css' TYPE='text/css'>");
        } else if ($.browser.safari) {
            document.write("<link REL='stylesheet' HREF='../App_Themes/Estilos/misEstilosSafari.css' TYPE='text/css'>");
        } else { //Google Chrone
            document.write("<link REL='stylesheet' HREF='../App_Themes/Estilos/misEstilosChrone.css' TYPE='text/css'>");
        }

        function imagenGlow(miID) {
            document.images(miID).src = 'Imagenes/' + miID + '_2.png';
        }
        function imagenNormal(miID) {
            document.images(miID).src = 'Imagenes/' + miID + '_1.png';
        }

        function ValidarLength(textareaControl, maxlength) {
            if (textareaControl.value.length > maxlength) {
                textareaControl.value = textareaControl.value.substring(0, maxlength);
            }
        }    