using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace proy_estructurado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        conexion conexion_bd = new conexion(); 
        private void button4_Click(object sender, EventArgs e)
        {
            if (conexion_bd.EliminarDatos("persona", "ci = " + txb_ci.Text))
            {
                MessageBox.Show("Datos de persona eliminado");
                MostrarDatos();
            }
            else {
                MessageBox.Show("Datos de persona no eliminado");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            conexion_bd.Conectar();
            MostrarDatos();//invocamos metodo para mostrar datos linea 43 de Form1
            MostrarCombo();//invocamos metodo para mostrar combo linea 51 de Form1
            
        }
        public void MostrarDatos() {
            //metodo para mostrar datos
            conexion_bd.consulta("SELECT * FROM persona INNER JOIN ciudad ON persona.id_ciudad = ciudad.id_ciudad", "persona");
            dataGridView1.DataSource = conexion_bd.ds.Tables["persona"];
            dataGridView1.Columns["id_ciudad"].Visible = false;
            dataGridView1.Columns["id_ciudad1"].Visible = false;
        }

        public void MostrarCombo()
        {
            //metodo para obtener los datos de ciudad en el combo
            conexion_bd.consulta("SELECT * FROM ciudad", "ciudad");
            comboBox1.DataSource = conexion_bd.ds.Tables["ciudad"];
            comboBox1.DisplayMember = "des_ciudad";
            comboBox1.ValueMember = "id_ciudad";
            
        }

        private void boton_nueva_persona_Click(object sender, EventArgs e)
        {
            //boton para agregar persona
            string consulta_agregar = "INSERT INTO persona VALUES ("+Convert.ToInt32(txb_ci.Text)+",'"+ txb_nombre.Text + "','" + txb_apellido.Text + "'," + Convert.ToInt32(txb_telefono.Text) + ",'" + Convert.ToInt32(comboBox1.SelectedValue) + "')";
            if(conexion_bd.InsertarDatos(consulta_agregar)){
           MessageBox.Show("Datos insertados");
           MostrarDatos();
            }else{
                MessageBox.Show("Datos no insertados");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //forma para obtener los registros en los campos al dar click sobre un registro
            DataGridViewRow registro = dataGridView1.Rows[e.RowIndex];
            txb_ci.Text = registro.Cells["ci"].Value.ToString();
            txb_nombre.Text = registro.Cells["nombres"].Value.ToString();
            txb_apellido.Text = registro.Cells["apellidos"].Value.ToString();
            txb_telefono.Text = registro.Cells["telefono"].Value.ToString();
            comboBox1.Text = registro.Cells["des_ciudad"].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //boton para actualizar datos
           string actualizar = "ci='" + Convert.ToInt32(txb_ci.Text) + "', nombres= '" + txb_nombre.Text + "',apellidos='" + txb_apellido.Text + "', telefono= '" + Convert.ToInt32(txb_telefono.Text) + "',id_ciudad='" + Convert.ToInt32(comboBox1.SelectedValue) + "'";
           if (conexion_bd.ActualizarDatos("persona", actualizar, "ci=" + txb_ci.Text.Trim()))
            {
                MessageBox.Show("Datos actualizados");
                MostrarDatos();
            }
            else
            {
                MessageBox.Show("Datos no actualizados");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //boton para buscar a traves del CI
            conexion_bd.Buscar("SELECT * FROM persona INNER JOIN ciudad ON persona.id_ciudad = ciudad.id_ciudad WHERE ci=" + txb_ci.Text, "persona");
            dataGridView1.DataSource = conexion_bd.ds.Tables["persona"];
            dataGridView1.Columns["id_ciudad"].Visible = false;
            dataGridView1.Columns["id_ciudad1"].Visible = false;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarDatos();
        }

    
    }
}
