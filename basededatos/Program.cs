using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=autonoma2;Uid=root;Pwd='';";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Aquí puedes ejecutar tus consultas
                //consulta numero 1 todos los datos
                string query = "SELECT * FROM alumnos";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Consulta numero 1 todos los datos");
                    while (reader.Read())
                    {
                        Console.WriteLine("Nombres:  " + reader["nombres"].ToString());
                        Console.WriteLine("Apellidos:  " + reader["apellidos"].ToString());
                        Console.WriteLine("Edad:  " + reader["edad"].ToString());
                        Console.WriteLine("Sexo:  " + reader["sexo"].ToString());
                        Console.WriteLine("Email:  " + reader["email"].ToString());
                        Console.WriteLine("Telefono:  " + reader["telefono"].ToString());
                    }
                }
                //consulta numero 2 todos los datos
                string query2 = "SELECT nombres, apellidos FROM alumnos where edad >10";
                MySqlCommand cmd2 = new MySqlCommand(query2, connection);

                using (MySqlDataReader reader = cmd2.ExecuteReader())
                {
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Consulta numero 2 seleccionando solo dos columnas ");
                    while (reader.Read())
                    {
                        Console.WriteLine("Nombres:  " + reader["nombres"].ToString());
                        Console.WriteLine("Apellidos:  " + reader["apellidos"].ToString());

                    }

                }
                //consulta numero 3 usando order by
                string query3 = "SELECT * FROM alumnos order by edad";
                MySqlCommand cmd3 = new MySqlCommand(query3, connection);

                using (MySqlDataReader reader = cmd3.ExecuteReader())
                {
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Consulta numero 3 usando order by por edad");
                    while (reader.Read())
                    {
                        Console.WriteLine("Nombres:  " + reader["nombres"].ToString());
                        Console.WriteLine("Apellidos:  "+ reader["apellidos"].ToString());
                        Console.WriteLine("Edad:  "+ reader["edad"].ToString());
                        Console.WriteLine("Sexo:  " + reader["sexo"].ToString());
                        Console.WriteLine("Email:  " + reader["email"].ToString());
                        Console.WriteLine("Telefono:  " + reader["telefono"].ToString());

                    }

                }
                //consulta numero 4 usando GROUP BY
                string query4 = "SELECT sexo, COUNT(*) as total FROM alumnos group by sexo";
                MySqlCommand cmd4 = new MySqlCommand(query4, connection);

                using (MySqlDataReader reader = cmd4.ExecuteReader())
                {
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Consulta numero 4 usando Group by");
                    while (reader.Read())
                    {                
                        Console.WriteLine("Sexo:  " + reader["sexo"].ToString());
                        Console.WriteLine("Total:  " + reader["total"].ToString());
                    }

                }
                //Una consulta con WHERE, GROUP BY Y ORDER BY
                string query5 = "SELECT edad, COUNT(*) as cantidad_alumnos FROM alumnos where edad>20 GROUP BY edad ORDER BY edad;";
                MySqlCommand cmd5 = new MySqlCommand(query5, connection);

                using (MySqlDataReader reader = cmd5.ExecuteReader())
                {
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Consulta numero 5  con WHERE, GROUP BY Y ORDER BY");
                    while (reader.Read())
                    {
                        Console.WriteLine("Edad:  " + reader["edad"].ToString());
                        Console.WriteLine("Cantidad de alumnos:  " + reader["cantidad_alumnos"].ToString());
                    }

                }
                
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de MySQL: " + ex.Message);
            }
            finally
            {
                // Asegúrate de cerrar la conexión en el bloque finally
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
    }
}
