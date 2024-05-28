using consolebdd.Data;
using consolebdd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolebdd.LN
{
    public static class CourseManager
    {

        public static void ManageCourses(SchoolContext context)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Dar de alta un curso");
            Console.WriteLine("2. Consultar cursos");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddCourse(context);
                    break;
                case "2":
                    ListCourses(context);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private static void AddCourse(SchoolContext context)
        {
            Console.WriteLine("Ingrese el título del curso:");
            var title = Console.ReadLine();

            Console.WriteLine("Ingrese los créditos del curso:");
            if (!int.TryParse(Console.ReadLine(), out var credits))
            {
                Console.WriteLine("Créditos no válidos.");
                return;
            }

            Console.WriteLine("Ingrese el Id de Departamento:");
            if (!int.TryParse(Console.ReadLine(), out var departmentId))
            {
                Console.WriteLine("Id de departamento no valido.");
                return;
            }

            try
            {
                var course = new Course { Title = title, Credits = credits, DepartmentId = departmentId };
                context.Courses.Add(course);
                context.SaveChanges();
                Console.WriteLine("Curso agregado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error: " + ex);

            }
        }

        private static void ListCourses(SchoolContext context)
        {
            var courses = context.Courses.ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Id}: {course.Title} - {course.Credits} créditos");
            }
        }

    }
}
