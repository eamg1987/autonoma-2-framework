using System;
using MySql.Data.MySqlClient;

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
                string query = "SELECT * FROM alumnos";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["nombres"].ToString());
                        Console.WriteLine(reader["apellidos"].ToString());
                        Console.WriteLine(reader["edad"].ToString());
                        Console.WriteLine(reader["sexo"].ToString());
                        Console.WriteLine(reader["email"].ToString());
                        Console.WriteLine(reader["telefono"].ToString());
                    }

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de MySQL: " + ex.Message);
            }
        }
    }
}
