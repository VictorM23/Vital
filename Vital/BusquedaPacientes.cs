using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Vital
{
    public partial class BusquedaPacientes : Form
    {
        public BusquedaPacientes()
        {
            InitializeComponent();
        }

        private void BusquedaPacientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.Name == "Login")
            //    {
            //        form.Show();
            //    }
            //}
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Pacientes Agregar = new Pacientes();
            Agregar.Show();
            this.Close();
        }

        private void BusquedaPacientes_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Pacientes", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();

            con.Close();
            dgvPacientes.DataSource = dt;
        }

        private void btnTodo_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Pacientes", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvPacientes.DataSource = dt;
            con.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Pacientes where Nombre LIKE '%" + txtBuscar.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvPacientes.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Modificar Modificar = new Modificar();
            Modificar.Show();
            this.Close();
        }
    }
}
