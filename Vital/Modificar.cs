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
    public partial class Modificar : Form
    {
        public Modificar()
        {
            InitializeComponent();
        }

        private void Modificar_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Pacientes", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                cbNombre.Items.Add(DR[0]);
            }

            con.Close();

            dtpFechaNac.MaxDate = DateTime.Today;
            dtpFechaNac.Text = DateTime.Today.ToString();
        }

        private void cbNombre_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            SqlCommand cmd = new SqlCommand("select * from Pacientes WHERE Nombre='" + cbNombre.Text + "'", con);
            con.Open();

            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                txtDireccion.Text = (read["Direccion"].ToString());
                txtTelefono.Text = (read["Telefono"].ToString());
                txtCorreo.Text = (read["Correo"].ToString());
                txtEdad.Text = (read["Edad"].ToString());
                dtpFechaNac.Text = (read["FechaNac"].ToString());
                cbActFisica.Text = (read["TipoActFisica"].ToString());
                txtActFisica.Text = (read["ActFisica"].ToString());
                txtEscolaridad.Text = (read["Escolaridad"].ToString());
                txtOcupacion.Text = (read["Ocupacion"].ToString());

                string data = read["HorarioTrabajo"].ToString();
                string[] words = data.Split('-');
                string hora1 = words[0];
                string hora2 = words[1];

                hora1 = hora1.Trim();
                hora2 = hora2.Trim();
                dtpHora1.Text = hora1;
                dtpHora2.Text = hora2;
                txtEstadoCivil.Text = (read["EstadoCivil"].ToString());
                txtNumeroHijos.Text = (read["NumeroHijos"].ToString());
                txtPartoNatural.Text = (read["PartoNatural"].ToString());
                txtPartoCesarea.Text = (read["PartoCesarea"].ToString());
                cbFuma.Text = (read["Fuma"].ToString());
                cbBebidas.Text = (read["ConsumeBebidas"].ToString());
                txtMedicamento.Text = (read["Medicamento"].ToString());
                txtAntFamiliares.Text = (read["AntFamiliares"].ToString());
                txtAntPersonales.Text = (read["AntPersonales"].ToString());
                cbSexo.Text = (read["Sexo"].ToString());
                cbPadres.Text = (read["PadresConObesidad"].ToString());
                cbHermanos.Text = (read["HermanosConObesidad"].ToString());
            }
            read.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (EmailValido(txtCorreo.Text) == true)
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
                        else
                        {
                            if (ctrl is MaskedTextBox)
                            {
                                MaskedTextBox mask = (MaskedTextBox)ctrl;
                                if (mask.MaskCompleted == false)
                                {
                                    lleno = false;
                                    mask.Focus();
                                }
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
                    cmd.CommandText = "UPDATE Pacientes SET Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo, Edad = @Edad, FechaNac = @FechaNac, Sexo = @Sexo, TipoActFisica = @TipoActFisica, ActFisica = @ActFisica, Escolaridad = @Escolaridad, Ocupacion = @Ocupacion, HorarioTrabajo = @HorasTrabajo, EstadoCivil = @EstadoCivil, NumeroHijos = @NumeroHijos, PartoNatural = @PartoNatural, PartoCesarea = @PartoCesarea, PadresConObesidad = @PadresObesidad, HermanosConObesidad = @HmnosObesidad, Fuma = @Fuma, ConsumeBebidas = @Bebidas, Medicamento = @Medicamento, AntFamiliares = @AntFamiliares, AntPersonales = @AntPersonales WHERE Nombre='" + cbNombre.Text + "'";

                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Edad", txtEdad.Text);
                    cmd.Parameters.AddWithValue("@FechaNac", dtpFechaNac.Text);
                    cmd.Parameters.AddWithValue("@Sexo", cbSexo.Text);
                    cmd.Parameters.AddWithValue("@TipoActFisica", cbActFisica.Text);
                    cmd.Parameters.AddWithValue("@ActFisica", txtActFisica.Text);
                    cmd.Parameters.AddWithValue("@Escolaridad", txtEscolaridad.Text);
                    cmd.Parameters.AddWithValue("@Ocupacion", txtOcupacion.Text);
                    cmd.Parameters.AddWithValue("@HorasTrabajo", dtpHora1.Text + " - " + dtpHora2.Text);
                    cmd.Parameters.AddWithValue("@EstadoCivil", txtEstadoCivil.Text);
                    cmd.Parameters.AddWithValue("@NumeroHijos", txtNumeroHijos.Text);
                    cmd.Parameters.AddWithValue("@PartoNatural", txtPartoNatural.Text);
                    cmd.Parameters.AddWithValue("@PartoCesarea", txtPartoCesarea.Text);
                    cmd.Parameters.AddWithValue("@PadresObesidad", cbPadres.Text);
                    cmd.Parameters.AddWithValue("@HmnosObesidad", cbHermanos.Text);
                    cmd.Parameters.AddWithValue("@Fuma", cbFuma.Text);
                    cmd.Parameters.AddWithValue("@Bebidas", cbBebidas.Text);
                    cmd.Parameters.AddWithValue("@Medicamento", txtMedicamento.Text);
                    cmd.Parameters.AddWithValue("@AntFamiliares", txtAntFamiliares.Text);
                    cmd.Parameters.AddWithValue("@AntPersonales", txtAntPersonales.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Paciente Modificado");
                    cbNombre.Focus();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Campos Incompletos");
                }
            }
            else
            {
                MessageBox.Show("Formato de Correo Electronico Incorrecto");
                txtCorreo.Focus();
            }
        }
        bool EmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void Modificar_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "BusquedaPacientes")
                {
                    form.Show();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el paciente?", "Eliminar Paciente", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Delete from Pacientes Where Nombre='" + cbNombre.Text + "'";
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("select * from Pacientes", con);
                SqlDataReader DR = cmd.ExecuteReader();
                cbNombre.Items.Clear();

                while (DR.Read())
                {
                    cbNombre.Items.Add(DR[0]);
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
                    if (c is MaskedTextBox)
                    {
                        MaskedTextBox mtb = (MaskedTextBox)c;
                        mtb.Text = string.Empty;
                    }
                    if (c is DateTimePicker)
                    {
                        DateTimePicker dtp = (DateTimePicker)c;
                        dtp.Text = DateTime.Today.ToString();
                    }
                }

                MessageBox.Show("Paciente Eliminado");
                cbNombre.Focus();
            }
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));
        }

        private void txtNumeroHijos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));
        }

        private void txtPartoNatural_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));
        }

        private void txtPartoCesarea_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
    }
}
