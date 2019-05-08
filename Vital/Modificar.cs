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
            con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Pacientes", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                cbNombre.Items.Add(DR[0]);
            }

            con.Close();
        }

        private void cbNombre_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
            SqlCommand cmd = new SqlCommand("select * from Pacientes WHERE Nombre='" + cbNombre.Text + "'", con);
            con.Open();

            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                txtDireccion.Text = (read["Direccion"].ToString());
                txtTelefono.Text = (read["Telefono"].ToString());
                txtCorreo.Text = (read["Correo"].ToString());
                txtEdad.Text = (read["Edad"].ToString());
                mtbFechaNac.Text = (read["FechaNac"].ToString());
                cbActFisica.Text = (read["TipoActFisica"].ToString());
                txtActFisica.Text = (read["ActFisica"].ToString());
                txtEscolaridad.Text = (read["Escolaridad"].ToString());
                txtOcupacion.Text = (read["Ocupacion"].ToString());
                txtHorasTrabajo.Text = (read["HorarioTrabajo"].ToString());
                txtEstadoCivil.Text = (read["EstadoCivil"].ToString());
                txtNumeroHijos.Text = (read["NumeroHijos"].ToString());
                txtPartoNatural.Text = (read["PartoNatural"].ToString());
                txtPartoCesarea.Text = (read["PartoCesarea"].ToString());
                txtFuma.Text = (read["Fuma"].ToString());
                txtBebidas.Text = (read["ConsumeBebidas"].ToString());
                txtMedicamento.Text = (read["Medicamento"].ToString());
                txtAntFamiliares.Text = (read["AntFamiliares"].ToString());
                txtAntPersonales.Text = (read["AntPersonales"].ToString());

                if (read["Sexo"].ToString() == "M")
                {
                    rbSexoM.Checked = true;
                }
                else
                {
                    rbSexoF.Checked = true;
                }

                if (read["PadresConObesidad"].ToString() == "SI")
                {
                    rbPadresSi.Checked = true;
                }
                else
                {
                    rbPadresNo.Checked = true;
                }

                if (read["HermanosConObesidad"].ToString() == "SI")
                {
                    rbHermanosSi.Checked = true;
                }
                else
                {
                    rbHermanosNo.Checked = true;
                }
            }
            read.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Pacientes SET Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo, Edad = @Edad, FechaNac = @FechaNac, Sexo = @Sexo, TipoActFisica = @TipoActFisica, ActFisica = @ActFisica, Escolaridad = @Escolaridad, Ocupacion = @Ocupacion, HorarioTrabajo = @HorasTrabajo, EstadoCivil = @EstadoCivil, NumeroHijos = @NumeroHijos, PartoNatural = @PartoNatural, PartoCesarea = @PartoCesarea, PadresConObesidad = @PadresObesidad, HermanosConObesidad = @HmnosObesidad, Fuma = @Fuma, ConsumeBebidas = @Bebidas, Medicamento = @Medicamento, AntFamiliares = @AntFamiliares, AntPersonales = @AntPersonales WHERE Nombre='" + cbNombre.Text + "'";

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

            MessageBox.Show("Paciente Modificado");
            con.Close();
        }

        private void Modificar_FormClosing(object sender, FormClosingEventArgs e)
        {
            BusquedaPacientes Pacientes = new BusquedaPacientes();
            Pacientes.Show();
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
                con.ConnectionString = "Data Source=LABSISINF;Initial Catalog=Vital;Integrated Security=True";
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
                    if (c is RadioButton)
                    {
                        RadioButton rb = (RadioButton)c;
                        rb.Checked = false;
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
                }
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
