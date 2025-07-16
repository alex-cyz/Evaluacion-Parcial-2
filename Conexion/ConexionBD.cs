using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlProduccionApp.Conexion
{
    public class ConexionBD
    {
        private MySqlConnection conexion;

        public MySqlConnection Conectar()
        {
            string cadenaConexion = "server=localhost;database=controlproduccion;uid=root;pwd=;";
            conexion = new MySqlConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }

        public void Desconectar()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}

