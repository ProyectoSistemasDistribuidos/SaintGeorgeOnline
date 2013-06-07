using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using bean = BeanBiblioteca.BibliotecaLibros;

namespace BibliotecaService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "Ibiblioteca" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface Ibiblioteca
    {
        [OperationContract]
        [WebInvoke(
           Method = "POST",
           UriTemplate = "/getLibros",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare
           )]
        bean getLibros(ClsLibro objLibro);
    }

    
      [DataContract]
    public class ClsLibro
    {
        [DataMember]
        public String codigo { get; set; }
        [DataMember]
        public String anio { get; set; }
     }
     
}
