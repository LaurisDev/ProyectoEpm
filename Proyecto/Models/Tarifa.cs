using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace ConexionBDConsultas

{
    public class Conexion
    {
        public string ObtenerCadenaConexion()
        {
            // Nombre de la cadena de conexión definida en el archivo de configuración
            string nombreCadenaConexion = "EPMEntities";

            // Obtener la cadena de conexión desde el archivo de configuración
            string connectionString = ConfigurationManager.ConnectionStrings[nombreCadenaConexion].ConnectionString;

            return connectionString;
        }
    }

    public class Tarifa
    {
        private string connectionString;

        public Tarifa()
        {
            // Obtener la cadena de conexión desde la clase de conexión
            connectionString = new Conexion().ObtenerCadenaConexion();
        }

        public decimal ObtenerValorTarifaPorTipo(string tipo)
        {
            decimal valorTarifa = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Consulta SQL para obtener el valor de la tarifa por tipo
                    string query = "SELECT Valor FROM Tarifas WHERE Tipo = @Tipo";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Tipo", tipo);

                    connection.Open();

                    // Ejecutar la consulta y obtener el resultado
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        valorTarifa = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener el valor de la tarifa: " + ex.Message);
                // Puedes lanzar una excepción o manejar el error de acuerdo a tus requerimientos
            }

            return valorTarifa;
        }
    }
}
