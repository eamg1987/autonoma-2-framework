
using System.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

public class Alumno
{
    [Key]
    public int id_alumno { get; set; }
    public string nombres { get; set; }
    public string apellidos { get; set; }
    public int edad { get; set; }
    public string sexo { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
}
public class AppDbContext : DbContext
{
    public DbSet<Alumno> Alumnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Reemplaza "Server=localhost;Database=autonoma2;Uid=root;Pwd='';" con tu cadena de conexión correcta
        optionsBuilder.UseMySQL("Server=localhost;Database=autonoma2;Uid=root;Pwd='';");
    }
}

class Program
{
    static void Main()
    {
        using (var context = new AppDbContext())
        {
            try
            {
                // Consulta numero 1 - Todos los datos
                var consulta1 = context.Alumnos.ToList();

                Console.WriteLine("Consulta numero 1 - Todos los datos");
                foreach (var alumno in consulta1)
                {
                    Console.WriteLine($"Nombres: {alumno.nombres}");
                    Console.WriteLine($"Apellidos: {alumno.apellidos}");
                    Console.WriteLine($"Edad: {alumno.edad}");
                    Console.WriteLine($"Sexo: {alumno.sexo}");
                    Console.WriteLine($"Email: {alumno.email}");
                    Console.WriteLine($"Telefono: {alumno.telefono}");
                }
                // Consulta numero 2 - solo dos columnas
                var resultadoConsulta = context.Alumnos
                    .Where(a => a.edad >= 18) // Filtro con WHERE
                    .GroupBy(a => a.sexo)      // Agrupar por Sexo
                    .Select(grupo => new
                    {
                        Sexo = grupo.Key,
                        Cantidad = grupo.Count()
                        // Puedes agregar más propiedades según tus necesidades
                    })
                    .ToList();

                // Imprimir resultados
                foreach (var resultado in resultadoConsulta)
                {
                    Console.WriteLine($"Sexo: {resultado.Sexo}, Cantidad: {resultado.Cantidad}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
