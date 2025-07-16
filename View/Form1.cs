using ControlProduccionApp.Conexion;
using ControlProduccionApp.View;
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

namespace ControlProduccionApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
            private void CargarProductos()
        {
            ConexionBD conexion = new ConexionBD();
            var conn = conexion.Conectar();
            string query = "SELECT ProductoID, Nombre FROM Productos";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboProducto.DataSource = dt;
            comboProducto.DisplayMember = "Nombre";
            comboProducto.ValueMember = "ProductoID";
            conexion.Desconectar();
        }
        private void CargarEmpleados()
        {
            ConexionBD conexion = new ConexionBD();
            var conn = conexion.Conectar();
            string query = "SELECT EmpleadoID, Nombre FROM Empleados";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboEmpleado.DataSource = dt;
            comboEmpleado.DisplayMember = "Nombre";
            comboEmpleado.ValueMember = "EmpleadoID";
            conexion.Desconectar();
        }

        private void CargarMaquinas()
        {
            ConexionBD conexion = new ConexionBD();
            var conn = conexion.Conectar();
            string query = "SELECT MaquinaID, Nombre FROM Maquinas";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboMaquina.DataSource = dt;
            comboMaquina.DisplayMember = "Nombre";
            comboMaquina.ValueMember = "MaquinaID";
            conexion.Desconectar();
        }

        private void CargarOrdenes()
        {
            ConexionBD conexion = new ConexionBD();
            var conn = conexion.Conectar();
            string query = @"SELECT o.OrdenID, p.Nombre AS Producto, e.Nombre AS Empleado, 
                     m.Nombre AS Maquina, o.FechaInicio, o.FechaFin, o.Estado
                     FROM OrdenesProduccion o
                     JOIN Productos p ON o.ProductoID = p.ProductoID
                     JOIN Empleados e ON o.EmpleadoID = e.EmpleadoID
                     JOIN Maquinas m ON o.MaquinaID = m.MaquinaID";

            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridViewOrdenes.DataSource = dt;
            conexion.Desconectar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarProductos();
            CargarEmpleados();
            CargarMaquinas();
            CargarOrdenes();
        }

        private void btnCrearOrden_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            var conn = conexion.Conectar();

            string query = @"INSERT INTO OrdenesProduccion 
                    (ProductoID, EmpleadoID, MaquinaID, FechaInicio, FechaFin, Estado)
                    VALUES (@producto, @empleado, @maquina, @inicio, @fin, @estado)";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@producto", comboProducto.SelectedValue);
            cmd.Parameters.AddWithValue("@empleado", comboEmpleado.SelectedValue);
            cmd.Parameters.AddWithValue("@maquina", comboMaquina.SelectedValue);
            cmd.Parameters.AddWithValue("@inicio", dtInicio.Value.Date);
            cmd.Parameters.AddWithValue("@fin", dtFin.Value.Date);
            cmd.Parameters.AddWithValue("@estado", "En proceso");

            cmd.ExecuteNonQuery();
            conexion.Desconectar();
            MessageBox.Show("Orden creada correctamente");
            CargarOrdenes();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarOrdenes();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrdenes.SelectedRows.Count > 0)
            {
                int ordenID = Convert.ToInt32(dataGridViewOrdenes.SelectedRows[0].Cells["OrdenID"].Value);
                ConexionBD conexion = new ConexionBD();
                var conn = conexion.Conectar();

                string query = "DELETE FROM OrdenesProduccion WHERE OrdenID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", ordenID);
                cmd.ExecuteNonQuery();

                conexion.Desconectar();
                MessageBox.Show("Orden eliminada.");
                CargarOrdenes();
            }
            else
            {
                MessageBox.Show("Selecciona una fila primero.");
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            FormProducto form = new FormProducto();
            form.ShowDialog();
            CargarProductos();
        }

        private void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            FormEmpleado form = new FormEmpleado();
            form.ShowDialog();
            CargarEmpleados();
        }

        private void btnAgregarMaquina_Click(object sender, EventArgs e)
        {
            FormMaquina form = new FormMaquina();
            form.ShowDialog();
            CargarMaquinas();
        }
    }
}
