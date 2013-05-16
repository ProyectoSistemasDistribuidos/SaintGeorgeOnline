function devolver(control,destino) {
    $(destino).append("<li onclick='seleccionar(this)' ondblclick='mandar(this)'>" + $(control).text() + "</li>");
    $(destino).append("<li style='display:none'>" + $(control).next().text() + "</li>");
    $(control).next().remove()
    $(control).remove()
}

function mandar(control,destino) {
    $(destino).append("<li onclick='seleccionar(this)' ondblclick='devolver(this)'>" + $(control).text() + "</li>");
    $(destino).append("<li style='display:none'>" + $(control).next().text() + "</li>");
            $(control).next().remove()
            $(control).remove()
  }
   