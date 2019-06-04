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
    public partial class AgregarUsuario : Form
    {
        public AgregarUsuario()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool lleno = true;

            foreach(Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textbox = (TextBox)c;
                    if(textbox.Text.Trim() == string.Empty)
                    {
                        lleno = false;
                        textbox.Focus();
                    }
                }
            }

            if(lleno == true)
            {
                bool existe = false;
                SqlConnection con2 = new SqlConnection();
                con2.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                SqlCommand cmd2 = new SqlCommand("select Usuario from Usuarios", con2);
                con2.Open();

                SqlDataReader read = cmd2.ExecuteReader();

                while (read.Read())
                {
                    if (txtUsuario.Text == read["Usuario"].ToString())
                    {
                        existe = true;
                    }

                }
                con2.Close();

                if (existe == true)
                {
                    MessageBox.Show("El usuario ya existe");
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "INSERT INTO Usuarios(Usuario, Pass, Rol) VALUES(@Usuario, @Pass, @Rol)";

                    cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@Pass", txtPass.Text);

                    if(chkAdmin.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Rol", "Admin");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Rol", "Empleado");
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuario Guardado");
                    txtUsuario.Focus();
                    con.Close();

                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            TextBox textbox = (TextBox)c;
                            textbox.Text = string.Empty;
                        }
                        if (c is CheckBox)
                        {
                            CheckBox chk = (CheckBox)c;
                            chk.Checked = false;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Campos Incompletos");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AgregarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(Form form in Application.OpenForms)
            {
                if(form.Name == "BusquedaPacientes")
                {
                    form.Show();
                }
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Space);
        }

        private void txtPass2_KeyPress(object sender, KeyPressEventArgs e)
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
