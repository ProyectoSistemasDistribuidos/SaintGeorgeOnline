using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using bean = BeanBiblioteca.BibliotecaLibros;
using bl = BibliotecaBL.bibliotecaLibro;

namespace BibliotecaService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "biblioteca" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione biblioteca.svc o biblioteca.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class biblioteca : Ibiblioteca
    {
 

        public BeanBiblioteca.BibliotecaLibros getLibros(ClsLibro objLibro)
        {
            String codigo = objLibro.codigo;
            int anio = int.Parse(objLibro.anio ?? "0");
            return bl.getLibros(codigo,anio);
        }
    }
}
