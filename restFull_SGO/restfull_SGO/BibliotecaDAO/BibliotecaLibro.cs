using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

using bean= BeanBiblioteca;

namespace BibliotecaDAO
{
    public class BibliotecaLibro
    {
        public SqlConnection con = new SqlConnection("Data Source=FANNY\\SA;Initial Catalog=BANCO_LIBROS;User Id=sa;Password=sql;");

        public bean.BibliotecaLibros getPrestamo( String pstr_codigo, int pint_anio){
            
            var obj_biblioteca = new bean.BibliotecaLibros();
            
            SqlCommand cmd=new SqlCommand();

            cmd.Connection=con;
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText= "MA_USP_GET_LIBROS";
            cmd.Parameters.Add( "@codigo",SqlDbType.VarChar,8).Value=pstr_codigo;
            cmd.Parameters.Add("@anio",SqlDbType.Int).Value= pint_anio;

            try{
             if(con.State==  ConnectionState.Closed){
            con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();

                while(dr.Read()){
                obj_biblioteca.str_mensaje= dr[0].ToString();
                    obj_biblioteca.int_valor = int.Parse( dr[1].ToString() );

                    }

            }finally{
            con.Close();
            }
           
            return obj_biblioteca;
        }

        

        /*
         
 ALTER proc MA_USP_GET_LIBROS
 (
 @codigo varchar(8),
 @anio int
 )
 as
 BEGIN
 SELECT 'DEBE LIBROS' AS MENSAJE,0 AS VALOR
         */
        
    }
}
