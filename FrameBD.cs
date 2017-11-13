using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
namespace ClaseBDCSharp
{
   
  class FrameBD
    {
        string ser;
        string por;
        string pas;
        string use;
        string db;
        MySqlConnection conex = new MySqlConnection();
        /* public void FrameBD(string server, string port, string pass, string user, string bd)
        {
            server = ser;
            port = por;
            pass = pas;
            user = use;
            bd = db;
        }
         */



        public void conectar() 
        {
            conex.Close(); 
                MySqlConnectionStringBuilder conexs = new MySqlConnectionStringBuilder();
                conexs.Server = "localhost";
                conexs.Port = 3309;//Convert.ToUInt32(por);
                conexs.Password = "admin"; //pas;
                conexs.UserID = "root"; //use;
                conexs.Database = "seutc";
            
                //MySqlConnection conex = new MySqlConnection(conexs.ToString() );
                conex.ConnectionString = conexs.ConnectionString;   
                //MySqlConnection conn = new MySqlConnection(builder.ToString());
                

                conex.Open(); 
            
                
                
            
            

        }


      public  DataSet SQLSEL(string sql)
      {
          conectar();

          MySqlDataAdapter Adaptador = new MySqlDataAdapter(sql, conex);
          DataSet RsDatos = new DataSet();
          Adaptador.Fill(RsDatos, "datos");
          //SQLSEL = RsDatos;
          return (RsDatos);
          conex.Close(); 
      }


      /*
       Public Function SQLSEL(ByVal sql As String) As DataSet
        Try
            conectar()

            Dim Adaptador As New MySqlDataAdapter(sql, conex)
            Dim RsDatos As New DataSet
            Adaptador.Fill(RsDatos, "datos")
            Return (RsDatos)
            conex.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
      */

    }



}
