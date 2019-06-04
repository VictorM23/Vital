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
    public partial class Seguimiento : Form
    {
        public Seguimiento()
        {
            InitializeComponent();
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

                string date = DateTime.Now.ToString("dd/MM/yyyy");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO Seguimiento(Nombre, Fecha, PesoActual, Talla, IMC, PorcGrasa, PesoIdeal, PesoDeseado, KcalPorDia, TratamientoActual, ResultadosLab, RecordatorioAlimentos, Observaciones) " +
                                  "VALUES(@Nombre, @Fecha, @PesoActual, @Talla, @IMC, @PorcGrasa, @PesoIdeal, @PesoDeseado, @KcalPorDia, @TratamientoActual, @ResultadosLab, @RecordatorioAlimentos, @Observaciones)";

                cmd.Parameters.AddWithValue("@Nombre", cbNombre.Text);
                cmd.Parameters.AddWithValue("@Fecha", date);
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
                MessageBox.Show("Seguimiento Guardado");
                con.Close();
            }
            else
            {
                MessageBox.Show("Campos Vacíos");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Seguimiento_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "VistaSeguimiento")
                {
                    form.Show();
                }
            }
        }

        private void Seguimiento_Load(object sender, EventArgs e)
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
    }
}
