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
    public partial class FormProducto : Form
    {
        public FormProducto()
        {
            InitializeComponent();
        }

        private void FormProducto_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.");
                return;
            }

            ConexionBD conexion = new ConexionBD();
            var conn = conexion.Conectar();

            string query = "INSERT INTO Productos (Nombre, Descripcion) VALUES (@nombre, @desc)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@desc", txtDescripcion.Text);
            cmd.ExecuteNonQuery();

            conexion.Desconectar();
            MessageBox.Show("Producto agregado correctamente.");
            this.Close();
        }
    }
}
