using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ConexionBDConsultas
{
    public class Estadisticas
    {
        string SQL;
        Conexion con = new Conexion();

        //case 4
        public decimal CalcularPromedioConsumoEnergiaGeneral()
        {
            decimal promedioGeneral = 0;
            int cantidadClientes = 0;
            decimal sumaConsumoEnergia = 0;

            using (SqlConnection connection = new SqlConnection(con.conexion))
            {
                string SQL = @"SELECT ConsumoActualEnergia FROM ConsumoEnergia";

                SqlCommand command = new SqlCommand(SQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("ConsumoActualEnergia")))
                        {
                            decimal consumoEnergia = reader.GetDecimal(reader.GetOrdinal("ConsumoActualEnergia"));
                            sumaConsumoEnergia += consumoEnergia;
                            cantidadClientes++;
                        }
                    }

                    reader.Close();

                    if (cantidadClientes > 0)
                    {
                        promedioGeneral = sumaConsumoEnergia / cantidadClientes;
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron registros de consumo de energía.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al calcular el promedio general de consumo de energía: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return promedioGeneral;
        }
        //case 5
        public static double CalcularTotalDescuentos()
        {
            double totalDescuentos = 0;
            Conexion con = new Conexion();

            using (SqlConnection connection = new SqlConnection(con.conexion))
            {
                string query = "SELECT MetaAhorroEnergia, ConsumoActualEnergia, TarifaEnergia FROM Clientes";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        double metaAhorro = Convert.ToDouble(reader["MetaAhorroEnergia"]);
                        double consumoActual = Convert.ToDouble(reader["ConsumoActualEnergia"]);
                        double tarifaEnergia = Convert.ToDouble(reader["TarifaEnergia"]);

                        double descuentoCliente = (metaAhorro - consumoActual) * tarifaEnergia;
                        if (descuentoCliente > 0)
                            totalDescuentos += descuentoCliente;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return totalDescuentos;
        }

        // case 6
        public static double CalcularTotalExcesoAgua()
        {
            double totalExcesoAgua = 0;
        
            Conexion con = new Conexion();
            using (SqlConnection connection = new SqlConnection(con.conexion))
            {
                connection.Open();
                string query = "SELECT ConsumoActualAgua, PromedioConsumoAgua FROM Clientes";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    double consumoActual = Convert.ToDouble(reader["ConsumoActualAgua"]);
                    double promedioConsumo = Convert.ToDouble(reader["PromedioConsumoAgua"]);
                    totalExcesoAgua += Math.Max(0, consumoActual - promedioConsumo);
                }

                reader.Close();
            }

            return totalExcesoAgua;
        }

        //case 7

        // case 8

        // case 9
        

        //case 10
        public decimal CalcularTotalClientesConConsumoMayorPromedio()
        {
            decimal totalclientesConConsumoMayorPromedio = 0;
            Tarifa tarifa = new Tarifa();

            using (SqlConnection connection = new SqlConnection(con.conexion))
            {
                string SQL = @"SELECT COUNT(*) AS ClientesConConsumoMayorPromedio
               FROM Clientes
               WHERE ConsumoActualAgua > PromedioConsumoAgua";
                SqlCommand command = new SqlCommand(SQL, connection);

                try
                {
                    connection.Open();
                    totalclientesConConsumoMayorPromedio = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al contar clientes con consumo mayor al promedio: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return totalclientesConConsumoMayorPromedio;
        }

        //case 11
        public decimal CalcularTotalPagadoEnergiaGeneral()
        {
            decimal totalPagadoEnergia = 0;
            Tarifa tarifa = new Tarifa(); // Instanciamos la clase Tarifa

            using (SqlConnection connection = new SqlConnection(con.conexion))
            {
                string SQL = @"SELECT ConsumoActualEnergia FROM ConsumoEnergia";

                SqlCommand command = new SqlCommand(SQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("ConsumoActualEnergia")))
                        {
                            decimal consumoEnergia = reader.GetDecimal(reader.GetOrdinal("ConsumoActualEnergia"));
                            // Obtenemos el valor de la tarifa de agua
                            decimal tarifaEnergia = tarifa.ObtenerValorTarifaPorTipo("Energia");
                            // Calculamos el total pagado por agua
                            totalPagadoEnergia += consumoEnergia * tarifaEnergia;
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al calcular el total pagado por agua: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return totalPagadoEnergia;
        }

        //case 12
        public decimal CalcularTotalPagadoAguaGeneral()
        {
            decimal totalPagadoAgua = 0;
            Tarifa tarifa = new Tarifa(); // Instanciamos la clase Tarifa

            using (SqlConnection connection = new SqlConnection(con.conexion))
            {
                string SQL = @"SELECT ConsumoActualAgua FROM ConsumoAgua";

                SqlCommand command = new SqlCommand(SQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("ConsumoActualAgua")))
                        {
                            decimal consumoAgua = reader.GetDecimal(reader.GetOrdinal("ConsumoActualAgua"));
                            // Obtenemos el valor de la tarifa de agua
                            decimal tarifaAgua = tarifa.ObtenerValorTarifaPorTipo("Agua");
                            // Calculamos el total pagado por agua
                            totalPagadoAgua += consumoAgua * tarifaAgua;
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al calcular el total pagado por agua: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return totalPagadoAgua;
        }

    }
}
