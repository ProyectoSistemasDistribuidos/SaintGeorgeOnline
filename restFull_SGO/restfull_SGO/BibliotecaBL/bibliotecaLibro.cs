using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bean = BeanBiblioteca.BibliotecaLibros;
using dao = BibliotecaDAO.BibliotecaLibro;

namespace BibliotecaBL
{
    public class bibliotecaLibro
    {
        public static bean getLibros( String str_codigo,int int_anio) {
            dao vobjdao = new dao();
            return vobjdao.getPrestamo(str_codigo,int_anio);
        }

    }
}
