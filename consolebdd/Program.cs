// See https://aka.ms/new-console-template for more information
using consolebdd.Data;
using consolebdd.LN;
using consolebdd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace consolebdd.App;

public class Program
{
    public static IConfigurationRoot? Configuration { get; set; }

    public static void Main(string[] args)
    {
        ReadConfiguration();

        using (var db = new SchoolContext())
        {
            Console.WriteLine("Creating database...\n");
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine("Seeding database...\n");
            SchoolInitializer.SeedData(db);
            Console.WriteLine("End seeding database...\n");

            Console.WriteLine("Start LINQ queries...\n");

            while (true)
            {
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Cursos");
                Console.WriteLine("2. Estudiantes");
                Console.WriteLine("3. Enrollments");
                Console.WriteLine("4. Salir");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CourseManager.ManageCourses(db);
                        break;
                    case "2":
                        StudentManager.ManageStudents(db);
                        break;
                    case "3":
                        EnrollmentManager.ManageEnrollments(db);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }

   












    //Example 1
    //var custQuery = from stu in db.Students
    //                where stu.FirstMidName == "Peggy"
    //                select stu.FullName;

    //foreach (var stu in custQuery)
    //    Console.WriteLine(stu);

    ////Example 2
    //var custQuery2 = db.Courses.Where(c => c.Title == "Macroeconomics").ToList();

    //foreach (var stu in custQuery2)
    //    Console.WriteLine(stu);

    ////Listar todos los students que se inscribieron entre 2002-01-01 y 2003-12-31 

    //var custQuery3 = from stu in db.Students
    //                 where stu.EnrollmentDate >= new DateTime(2002, 01, 01) && stu.EnrollmentDate <= new DateTime(2003, 12, 31)
    //                 select stu.FullName;
    //foreach (var stu in custQuery3)
    //    Console.WriteLine(stu);

    ////Listar todos los students que se inscribieron entre 2002-01-01 y 2003-12-31 y sus Enrollments 
    //var custQuery4 = from stu in db.Students
    //                 where stu.EnrollmentDate >= new DateTime(2002, 01, 01) && stu.EnrollmentDate <= new DateTime(2003, 12, 31)
    //                 select stu.Enrollments;
    //foreach (var stu in custQuery4)
    //{
    //    foreach (var s in stu)
    //    Console.WriteLine(s.Course.Title);
    //}
    //};



    private static void ReadConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        Configuration = builder.Build();

        ConnectionStrings.DefaultConnection = Configuration["DefaultConnection"];

        Console.WriteLine("Configuration\n");
        Console.WriteLine($@"connectionString (defaultConnection) = ""{ConnectionStrings.DefaultConnection}""");
        Console.WriteLine();
    }

}