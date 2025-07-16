using ControlProduccionApp.Conexion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlProduccionApp.View
{
    public partial class FormMaquina : Form
    {
        public FormMaquina()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            ConexionBD conexion = new ConexionBD();
            var conn = conexion.Conectar();

            string query = "INSERT INTO Maquinas (Nombre, Tipo) VALUES (@nombre, @tipo)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
            cmd.ExecuteNonQuery();

            conexion.Desconectar();
            MessageBox.Show("Máquina agregada correctamente.");
            this.Close();
        }
    }
}
