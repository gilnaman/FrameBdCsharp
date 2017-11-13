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
        //string ser;
        //string por;
        //string pas;
        //string use;
        //string db;
        MySqlConnection conex = new MySqlConnection();
       
        private string servidor;
        private uint puerto;
        private string usuario;
        private string password;
        private string bd;

        public FrameBD(string servidor, uint puerto, string usuario, string password, string bd)
        {
            // TODO: Complete member initialization
            this.servidor = servidor;
            this.puerto= puerto;
            this.usuario= usuario;
            this.password= password;
            this.bd= bd;


            conex.Close();
            MySqlConnectionStringBuilder conexs = new MySqlConnectionStringBuilder();
            conexs.Server = servidor;
            conexs.Port = puerto;//Convert.ToUInt32(por);
            conexs.Password = password; //pas;
            conexs.UserID = usuario; //use;
            conexs.Database = bd;

            //MySqlConnection conex = new MySqlConnection(conexs.ToString() );
            conex.ConnectionString = conexs.ConnectionString;
            //MySqlConnection conn = new MySqlConnection(builder.ToString());


            conex.Open(); 
            
        }
         


        
      



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
          //conex.Close(); 
      }


      public DataTable SQLCOMBO(string sql) 
      {
          conectar();
          MySqlDataAdapter Adap = new MySqlDataAdapter(sql,conex);
          DataTable DT = new DataTable();
          Adap.Fill(DT);
          return DT;
 
      }

      public void SQLIDU(string sql) 
      {
          try
          {
              conectar();
              MySqlCommand comando = new MySqlCommand(sql, conex);
              comando.ExecuteNonQuery();
              //conex.Close();

          }
          catch (MySqlException) 
          {
              
              throw;
          }
          
      }

        /*
         Public Sub SQLIDU(ByVal Sql As String)
          'Try
          conectar()
          Dim comando As New MySqlCommand(Sql, conex)
          comando.ExecuteNonQuery()
          conex.Close()
          'Catch ex As MySqlException




          'Select Case ex.Number
          '  Case 1062
          'MsgBox("El elemento que pretende crear ya existe", vbInformation, "ATENCIÓN")
          '   Case Else
          'MsgBox(ex.Message & " " & ex.Number)
          'End Select

          'End Try


      End Sub
       
         */



        /*
             * Public Function SQLCOMBO(ByVal sql As String) As DataTable

           Try
               conectar()
               Dim Adap As New MySqlDataAdapter(sql, conex)
               Dim DT As New DataTable
               Adap.Fill(DT)
               Return DT
           Catch ex As Exception
               MsgBox(ex.Message)
           End Try
       End Function
          */

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
