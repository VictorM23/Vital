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
    public partial class ModificarEliminarUsuario : Form
    {
        public ModificarEliminarUsuario()
        {
            InitializeComponent();
        }

        private void cbUsuario_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            SqlCommand cmd = new SqlCommand("select * from Usuarios WHERE Usuario='" + cbUsuario.Text + "'", con);
            con.Open();

            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                txtPass.Text = (read["Pass"].ToString());

                if(read["Rol"].ToString() == "Admin")
                {
                    chkAdmin.Checked = true;
                }
                else
                {
                    chkAdmin.Checked = false;
                }
            }
            read.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool lleno = true;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox textbox = (TextBox)ctrl;
                    if (textbox.Text.Trim() == string.Empty)
                    {
                        lleno = false;
                        textbox.Focus();
                    }
                }
                else
                {
                    if (ctrl is ComboBox)
                    {
                        ComboBox cb = (ComboBox)ctrl;
                        if (cb.SelectedIndex == -1)
                        {
                            lleno = false;
                            cb.Focus();
                        }
                    }
                }
            }
            if (lleno == true)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE Usuarios SET Pass= @Pass, Rol = @Rol WHERE Usuario='" + cbUsuario.Text + "'";

                cmd.Parameters.AddWithValue("@Pass", txtPass.Text);

                if (chkAdmin.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Rol", "Admin");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Rol", "Empleado");
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("Usuario Modificado");
                cbUsuario.Focus();
                con.Close();
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

        private void ModificarEliminarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(Form form in Application.OpenForms)
            {
                if(form.Name == "BusquedaPacientes")
                {
                    form.Show();
                }
            }
        }

        private void ModificarEliminarUsuario_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Usuarios", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                cbUsuario.Items.Add(DR[0]);
            }

            con.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el usuario?", "Eliminar Usuario", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Delete from Usuarios Where Usuario='" + cbUsuario.Text + "'";
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("select * from Usuarios", con);
                SqlDataReader DR = cmd.ExecuteReader();
                cbUsuario.Items.Clear();

                while (DR.Read())
                {
                    cbUsuario.Items.Add(DR[0]);
                }

                con.Close();

                foreach (Control c in this.Controls)
                {
                    if (c is TextBox)
                    {
                        TextBox textbox = (TextBox)c;
                        textbox.Text = string.Empty;
                    }
                    if (c is ComboBox)
                    {
                        ComboBox cb = (ComboBox)c;
                        cb.SelectedIndex = -1;
                    }
                    if (c is CheckBox)
                    {
                        CheckBox chk = (CheckBox)c;
                        chk.Checked = false;
                    }
                }

                MessageBox.Show("Usuario Eliminado");
                cbUsuario.Focus();
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Space);
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if(chkMostrar.Checked == true)
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
