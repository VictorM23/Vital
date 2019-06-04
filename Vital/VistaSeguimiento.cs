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
    public partial class VistaSeguimiento : Form
    {
        public VistaSeguimiento()
        {
            InitializeComponent();
        }
        private void VistaSeguimiento_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Seguimiento", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();

            con.Close();
            dgvPacientes.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Seguimiento seguimiento = new Seguimiento();
            seguimiento.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            ModificarSeguimiento modificarSeguimiento = new ModificarSeguimiento();
            modificarSeguimiento.Show();
            this.Hide();
        }

        private void VistaSeguimiento_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "BusquedaPacientes")
                {
                    form.Show();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == string.Empty)
            {
                MessageBox.Show("Campo Vacío");
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Seguimiento where Nombre LIKE '%" + txtBuscar.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPacientes.DataSource = dt;
                con.Close();
            }
        }

        private void btnTodo_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Pacientes", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvPacientes.DataSource = dt;
            con.Close();
            txtBuscar.Text = string.Empty;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }
    }
}
