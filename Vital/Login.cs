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
            if (txtUsuario.Text.Trim() == string.Empty || txtPass.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Campos Vacíos");
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                con.Open();
                string Usuario = txtUsuario.Text;
                string Pass = txtPass.Text;
                SqlCommand cmd = new SqlCommand("select Usuario,Pass from Usuarios where Usuario='" + Usuario + "'and Pass='" + Pass + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    con.Close();

                    SqlConnection con2 = new SqlConnection();
                    con2.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                    con2.Open();
                    using (SqlCommand cmd2 = new SqlCommand("select * from Usuarios where Usuario='" + txtUsuario.Text + "'", con2))
                    {
                        using (SqlDataReader dr = cmd2.ExecuteReader())
                        {
                            txtUsuario.Text = string.Empty;
                            txtPass.Text = string.Empty;
                            txtUsuario.Focus();

                            if (dr.Read())
                            {
                                if(dr[2].ToString() == "Admin")
                                {
                                    this.Hide();
                                    BusquedaPacientes Busqueda = new BusquedaPacientes();
                                    Busqueda.Show();
                                }
                                else
                                {
                                    this.Hide();
                                    BusquedaPacientesEmpleados BusquedaEmpleados = new BusquedaPacientesEmpleados();
                                    BusquedaEmpleados.Show();
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Datos Incorrectos");
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Space);
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrar.Checked == true)
            {
                txtPass.PasswordChar = '\0';
            }
            else
            {
                txtPass.PasswordChar = '*';
            }
        }
    }
}
