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
                    var course = GetCourse();
                    AddCourse(course, context);
                    break;
                case "2":
                    ListCourses(context);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private static Course GetCourse()
        {
            Console.WriteLine("Ingrese el título del curso:");
            var title = Console.ReadLine();

            Console.WriteLine("Ingrese los créditos del curso:");
            if (!int.TryParse(Console.ReadLine(), out var credits))
            {
                Console.WriteLine("Créditos no válidos.");
                return null;
            }

            Console.WriteLine("Ingrese el Id de Departamento:");
            if (!int.TryParse(Console.ReadLine(), out var departmentId))
            {
                Console.WriteLine("Id de departamento no valido.");
                return null;
            }

            return new Course { Title = title, Credits = credits, DepartmentId = departmentId };
        }

        private static void AddCourse(Course? course, SchoolContext context)
        {
            try
            {
                if (course is null) throw new Exception("Su curso no es válido.");

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
            foreach (var course in context.Courses)
            {
                Console.WriteLine($"{course.Id}: {course.Title} - {course.Credits} créditos");
            }
        }

    }
}
