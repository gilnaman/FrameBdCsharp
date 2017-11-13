using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClaseBDCSharp
{
    public partial class Form1 : Form
    {
        FrameBD ControlEscBD = new FrameBD("localhost", 3309, "root", "admin", "seutc");
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            //ControlEscBD.conectar();

            /*double suma;
            double  num1,num2;

            num1 = Convert.ToDouble(textBox1.Text);
            num2 = Convert.ToDouble(textBox2.Text);
            suma = num1 + num2;

            */

            //DataSet Auxdatos = new DataSet(); 
            //Auxdatos = ControlEscBD.SQLSEL("Select * from Alumnos");
            //MessageBox.Show("Conectado");
            


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            FiltroAlumnos();

        }

        private void FiltroAlumnos() {
            dataGridView1.DataSource = ControlEscBD.SQLSEL("Select Matricula,Concat(ApellidoP,' ',ApellidoM,' ',Nombre) AS 'Alumno' from Alumnos Where ApellidoP Like '" + textBox2.Text  + "%'" );//Auxdatos;
            dataGridView1.DataMember = "datos";
            dataGridView1.Columns["Alumno"].Width = 300;

        }

        public void GetCarreras() 
        {
            String carrs = "Select IdCarrera,Nombre FROM Carreras";

            CmbCarreras.DataSource = ControlEscBD.SQLCOMBO(carrs);
            CmbCarreras.ValueMember = "IdCarrera";
            CmbCarreras.DisplayMember = "Nombre";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FiltroAlumnos();
            GetCarreras();
        }

        private void CmbCarrera_Click(object sender, EventArgs e)
        {
            String savecarr = "INSERT INTO Carreras(IdCarrera,Nombre)" +
                              " VALUES('" + TxtIdCarrera.Text + "','" + TxtNombre.Text + "')";

            ControlEscBD.SQLIDU(savecarr);
            MessageBox.Show("datos almacenados");
            GetCarreras();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TxtIdCarrera.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value);
            
        }



    }
}
