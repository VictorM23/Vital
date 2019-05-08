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
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Pacientes(Nombre, Direccion, Telefono, Correo, Edad, FechaNac, Sexo, TipoActFisica, ActFisica, Escolaridad, Ocupacion, HorarioTrabajo, EstadoCivil, NumeroHijos, PartoNatural, PartoCesarea, PadresConObesidad, HermanosConObesidad, Fuma, ConsumeBebidas, Medicamento, AntFamiliares, AntPersonales) VALUES(@Nombre, @Direccion, @Telefono, @Correo, @Edad, @FechaNac, @Sexo, @TipoActFisica, @ActFisica, @Escolaridad, @Ocupacion, @HorasTrabajo, @EstadoCivil, @NumeroHijos, @PartoNatural, @PartoCesarea, @PadresObesidad, @HmnosObesidad, @Fuma, @Bebidas, @Medicamento, @AntFamiliares, @AntPersonales)";

            string Sexo = "";
            string PadresObesidad = "";
            string HmnosObesidad = "";
            foreach (RadioButton rb in pnlSexo.Controls)
            {
                if (rb.Checked == true)
                {
                    Sexo = rb.Text;
                }
            }
            foreach (RadioButton rb in pnlPadresObesidad.Controls)
            {
                if (rb.Checked == true)
                {
                    PadresObesidad = rb.Text;
                }
            }
            foreach (RadioButton rb in pnlHmnosObesidad.Controls)
            {
                if (rb.Checked == true)
                {
                    HmnosObesidad = rb.Text;
                }
            }

            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
            cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
            cmd.Parameters.AddWithValue("@Edad", txtEdad.Text);
            cmd.Parameters.AddWithValue("@FechaNac", mtbFechaNac.Text);
            cmd.Parameters.AddWithValue("@Sexo", Sexo);
            cmd.Parameters.AddWithValue("@TipoActFisica", cbActFisica.Text);
            cmd.Parameters.AddWithValue("@ActFisica", txtActFisica.Text);
            cmd.Parameters.AddWithValue("@Escolaridad", txtEscolaridad.Text);
            cmd.Parameters.AddWithValue("@Ocupacion", txtOcupacion.Text);
            cmd.Parameters.AddWithValue("@HorasTrabajo", txtHorasTrabajo.Text);
            cmd.Parameters.AddWithValue("@EstadoCivil", txtEstadoCivil.Text);
            cmd.Parameters.AddWithValue("@NumeroHijos", txtNumeroHijos.Text);
            cmd.Parameters.AddWithValue("@PartoNatural", txtPartoNatural.Text);
            cmd.Parameters.AddWithValue("@PartoCesarea", txtPartoCesarea.Text);
            cmd.Parameters.AddWithValue("@PadresObesidad", PadresObesidad);
            cmd.Parameters.AddWithValue("@HmnosObesidad", HmnosObesidad);
            cmd.Parameters.AddWithValue("@Fuma", txtFuma.Text);
            cmd.Parameters.AddWithValue("@Bebidas", txtBebidas.Text);
            cmd.Parameters.AddWithValue("@Medicamento", txtMedicamento.Text);
            cmd.Parameters.AddWithValue("@AntFamiliares", txtAntFamiliares.Text);
            cmd.Parameters.AddWithValue("@AntPersonales", txtAntPersonales.Text);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Paciente Guardado");
            foreach (Control c2 in this.Controls)
            {
                if (c2 is TextBox)
                {
                    TextBox textbox = (TextBox)c2;
                    textbox.Text = string.Empty;
                }
                if (c2 is RadioButton)
                {
                    RadioButton rb = (RadioButton)c2;
                    rb.Checked = false;
                }
                if (c2 is ComboBox)
                {
                    ComboBox cb = (ComboBox)c2;
                    cb.SelectedIndex = -1;
                }
                if (c2 is MaskedTextBox)
                {
                    MaskedTextBox mtb = (MaskedTextBox)c2;
                    mtb.Text = string.Empty;
                }
            }
            con.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Pacientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            BusquedaPacientes Pacientes = new BusquedaPacientes();
            Pacientes.Show();
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
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

        private void mtbFechaNac_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNumeroHijos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPartoNatural_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPartoCesarea_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
