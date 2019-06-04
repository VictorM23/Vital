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
    public partial class ModificarSeguimiento : Form
    {
        public ModificarSeguimiento()
        {
            InitializeComponent();
        }

        private void cbNombre_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Seguimiento where Nombre = '" +cbNombre.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataReader DR = cmd.ExecuteReader();
            cmb_Fecha.Items.Clear();

            while (DR.Read())
            {
                cmb_Fecha.Items.Add(DR[1]);
            }

            con.Close();
        }

        private void cmb_Fecha_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
            SqlCommand cmd = new SqlCommand("select * from Seguimiento WHERE Fecha='" + cmb_Fecha.Text + "'", con);
            con.Open();

            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                txt_PesoActual.Text = (read["PesoActual"].ToString());
                txt_Talla.Text = (read["Talla"].ToString());
                txt_IMC.Text = (read["IMC"].ToString());
                txt_PorcentajeGrasa.Text = (read["PorcGrasa"].ToString());
                txt_PesoIdeal.Text = (read["PesoIdeal"].ToString());
                txt_PesoDeseado.Text = (read["PesoDeseado"].ToString());
                txt_KCal.Text = (read["KcalPorDia"].ToString());
                txt_Tratamiento.Text = (read["TratamientoActual"].ToString());
                txt_Laboratorio.Text = (read["ResultadosLab"].ToString());
                txt_Alimentos.Text = (read["RecordatorioAlimentos"].ToString());
                txt_Observaciones.Text = (read["Observaciones"].ToString());

            }
            read.Close();
        }

        private void ModificarSeguimiento_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModificarSeguimiento_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "VistaSeguimiento")
                {
                    form.Show();
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool lleno = true;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Text == string.Empty)
                {
                    lleno = false;
                    ctrl.Focus();
                }
            }

            if (lleno == true)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=VICTOR-PC;Initial Catalog=Vital;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE Seguimiento SET PesoActual = @PesoActual, Talla = @Talla, IMC = @IMC, PorcGrasa = @PorcGrasa, PesoIdeal = @PesoIdeal, PesoDeseado = @PesoDeseado, KcalPorDia = @KcalPorDia, TratamientoActual = @TratamientoActual, ResultadosLab = @ResultadosLab, RecordatorioAlimentos = @RecordatorioAlimentos, Observaciones = @Observaciones WHERE Nombre='" + cbNombre.Text + "'" + " AND Fecha ='" + cmb_Fecha.Text + "'";

                cmd.Parameters.AddWithValue("@PesoActual", txt_PesoActual.Text);
                cmd.Parameters.AddWithValue("@Talla", txt_Talla.Text);
                cmd.Parameters.AddWithValue("@IMC", txt_IMC.Text);
                cmd.Parameters.AddWithValue("@PorcGrasa", txt_PorcentajeGrasa.Text);
                cmd.Parameters.AddWithValue("@PesoIdeal", txt_PesoIdeal.Text);
                cmd.Parameters.AddWithValue("@PesoDeseado", txt_PesoDeseado.Text);
                cmd.Parameters.AddWithValue("@KcalPorDia", txt_KCal.Text);
                cmd.Parameters.AddWithValue("@TratamientoActual", txt_Tratamiento.Text);
                cmd.Parameters.AddWithValue("@ResultadosLab", txt_Laboratorio.Text);
                cmd.Parameters.AddWithValue("@RecordatorioAlimentos", txt_Alimentos.Text);
                cmd.Parameters.AddWithValue("@Observaciones", txt_Observaciones.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Seguimiento Modificado");
                con.Close();
            }
            else
            {
                MessageBox.Show("Campos Vacíos");
            }
        }
    }
}
