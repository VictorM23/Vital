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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            string Usuario = txtUsuario.Text;
            string Pass = txtPass.Text;
            SqlCommand cmd = new SqlCommand("select Usuario,Pass from Usuarios where Usuario='" + Usuario + "'and Pass='" + Pass + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                this.Hide();
                BusquedaPacientes Busqueda = new BusquedaPacientes();
                Busqueda.Show();
                txtUsuario.Text = string.Empty;
                txtPass.Text = string.Empty;
                txtUsuario.Focus();
            }
            else
            {
                MessageBox.Show("Datos Incorrectos");
            }
            con.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
