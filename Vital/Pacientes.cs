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
    public partial class Pacientes : Form
    {
        public Pacientes()
        {
            InitializeComponent();
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
                                if (
                                    mask.MaskCompleted == false)
                                {
                                    lleno = false;
                                    ctrl.Focus();
                                }
                            }
                        }
                    }
                }

                if (lleno == true)
                {
                    bool existe = false;
                    SqlConnection con2 = new SqlConnection();
                    con2.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                    SqlCommand cmd2 = new SqlCommand("select Nombre from Pacientes", con2);
                    con2.Open();

                    SqlDataReader read = cmd2.ExecuteReader();

                    while (read.Read())
                    {
                        if (txtNombre.Text == read["Nombre"].ToString())
                        {
                            existe = true;
                        }
                    
                    }
                    con2.Close();

                    if (existe == true)
                    {
                        MessageBox.Show("El paciente ya existe");
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = @"Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        con.Open();

                        cmd.CommandText = "INSERT INTO Pacientes(Nombre, Direccion, Telefono, Correo, Edad, FechaNac, Sexo, TipoActFisica, ActFisica, Escolaridad, Ocupacion, HorarioTrabajo, EstadoCivil, NumeroHijos, PartoNatural, PartoCesarea, PadresConObesidad, HermanosConObesidad, Fuma, ConsumeBebidas, Medicamento, AntFamiliares, AntPersonales) VALUES(@Nombre, @Direccion, @Telefono, @Correo, @Edad, @FechaNac, @Sexo, @TipoActFisica, @ActFisica, @Escolaridad, @Ocupacion, @HorasTrabajo, @EstadoCivil, @NumeroHijos, @PartoNatural, @PartoCesarea, @PadresObesidad, @HmnosObesidad, @Fuma, @Bebidas, @Medicamento, @AntFamiliares, @AntPersonales)";

                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
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

                        MessageBox.Show("Paciente Guardado");
                        txtNombre.Focus();
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
                    }
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Pacientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(Form form in Application.OpenForms)
            {
                if (form.Name == "BusquedaPacientes" || form.Name == "BusquedaPacientesEmpleados")
                {
                    form.Show();
                }
            }
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(e.KeyChar == (char)Keys.Back || char.IsNumber(e.KeyChar));
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void Pacientes_Load(object sender, EventArgs e)
        {
            dtpFechaNac.MaxDate = DateTime.Today;
            dtpFechaNac.Text = DateTime.Today.ToString();
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
    }
}
