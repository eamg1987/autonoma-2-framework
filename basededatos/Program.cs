
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
                // Consulta numero 2 - solo 2 columnas
                var consulta2 = context.Alumnos
                    .Where(a => a.edad >= 20) // Filtro con WHERE
                    .OrderBy(a => a.nombres)
                    .Select(grupo => new
                    {
                        Nombres = grupo.nombres,
                        Apellidos = grupo.apellidos
                        // Puedes agregar más propiedades según tus necesidades
                    })
                    .ToList();

                // Imprimir resultados
                Console.WriteLine("===============================================");
                Console.WriteLine("Consulta numero 2 - solo 2 columnas");
                foreach (var resultado in consulta2)
                {
                    Console.WriteLine($"Nombres: {resultado.Nombres}, Apellidos: {resultado.Apellidos}");
                }

                // Consulta numero 3 -  con ORDER BY
                var consulta3 = context.Alumnos
                    .Where(a => a.sexo == "Masculino") // Filtro con WHERE
                    .OrderBy(a => a.edad)
                    .ToList();

                // Imprimir resultados
                Console.WriteLine("===============================================");
                Console.WriteLine("Consulta numero 3 - con ORDER BY");
                foreach (var resultado in consulta3)
                {
                    Console.WriteLine($"Nombres: {resultado.nombres}");
                    Console.WriteLine($"Apellidos: {resultado.apellidos}");
                    Console.WriteLine($"Edad: {resultado.edad}");
                    Console.WriteLine($"Sexo: {resultado.sexo}");
                    Console.WriteLine($"Email: {resultado.email}");
                    Console.WriteLine($"Telefono: {resultado.telefono}");
                }

                // Consulta numero 4 -  con Groupby
                var consulta4 = context.Alumnos
                    .Where(a => a.sexo == "femenino") // Filtro con WHERE
                    .GroupBy(a => a.edad)
                    .Select( group =>new
                    {
                        Edad = group.Key,
                        Total= group.Count()
                    }
                    )
                    .ToList();

                // Imprimir resultados
                Console.WriteLine("===============================================");
                Console.WriteLine("Consulta numero 4 - con GROUP BY");
                foreach (var resultado in consulta4)
                {
                    Console.WriteLine($"Edad: {resultado.Edad}");
                    Console.WriteLine($"Total: {resultado.Total}");
                }


                // Consulta numero 5 -  con WHERE,  GROUP BY  Y ORDER BY
                var resultadoConsulta = context.Alumnos
                    .Where(a => a.edad >= 18) // Filtro con WHERE
                    .GroupBy(a => a.sexo)    // Agrupar por Sexo
                    .OrderBy(a => a.Key)
                    .Select(grupo => new
                    {
                        Sexo = grupo.Key,
                        Cantidad = grupo.Count()
                        
                    })
                    .ToList();

                // Imprimir resultados
                Console.WriteLine("===============================================");
                Console.WriteLine("Consulta numero 5 - con WHERE,  GROUP BY  Y ORDER BY");
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
