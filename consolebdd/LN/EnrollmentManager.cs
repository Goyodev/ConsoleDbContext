using consolebdd.Data;
using consolebdd.Models;
using Microsoft.EntityFrameworkCore;

namespace consolebdd.LN
{
    public static class EnrollmentManager
    {

        public static void ManageEnrollments(SchoolContext context)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Dar de alta un enrollment");
            Console.WriteLine("2. Consultar enrollments");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddEnrollment(context);
                    break;
                case "2":
                    ListEnrollments(context);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private static void AddEnrollment(SchoolContext context)
        {
            Console.WriteLine("Ingrese el ID del curso:");
            if (!int.TryParse(Console.ReadLine(), out var courseId))
            {
                Console.WriteLine("ID de curso no válido.");
                return;
            }

            Console.WriteLine("Ingrese el ID del estudiante:");
            if (!int.TryParse(Console.ReadLine(), out var studentId))
            {
                Console.WriteLine("ID de estudiante no válido.");
                return;
            }
            try
            {
                var enrollment = new Enrollment { CourseId = courseId, StudentId = studentId };
                context.Enrollments.Add(enrollment);
                context.SaveChanges();
                Console.WriteLine("Enrollment agregado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error: " + ex);

            }

        }

        private static void ListEnrollments(SchoolContext context)
        {
            var enrollments = context.Enrollments.Include(e => e.Course).Include(e => e.Student).ToList();
            foreach (var enrollment in enrollments)
            {
                Console.WriteLine($"{enrollment.Id}: Curso - {enrollment.Course?.Title}, Estudiante - {enrollment.Student?.FullName}, Nota - {enrollment.Grade}");
            }
        }

    }
}
