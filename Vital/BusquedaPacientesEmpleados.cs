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
    public partial class BusquedaPacientesEmpleados : Form
    {
        public BusquedaPacientesEmpleados()
        {
            InitializeComponent();
        }

        private void BusquedaPacientesEmpleados_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "Login")
                {
                    form.Show();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
                con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Pacientes where Nombre LIKE '%" + txtBuscar.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPacientes.DataSource = dt;
                con.Close();
                dgvPacientes.Sort(dgvPacientes.Columns["Nombre"], ListSortDirection.Descending);
                dgvPacientes.ReadOnly = true;
            }
        }

        private void btnTodo_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Pacientes", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvPacientes.DataSource = dt;
            con.Close();
            dgvPacientes.Sort(dgvPacientes.Columns["Nombre"], ListSortDirection.Descending);
            txtBuscar.Text = string.Empty;
            dgvPacientes.ReadOnly = true;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Pacientes Agregar = new Pacientes();
            Agregar.Show();
            this.Hide();
        }

        private void BusquedaPacientesEmpleados_VisibleChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Pacientes", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();

            con.Close();
            dgvPacientes.DataSource = dt;
            dgvPacientes.ReadOnly = true;
        }
    }
}
