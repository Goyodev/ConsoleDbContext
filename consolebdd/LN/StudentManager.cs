using consolebdd.Data;
using consolebdd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolebdd.LN
{
    public static class StudentManager
    {
        public static void ManageStudents(SchoolContext context)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Dar de alta un estudiante");
            Console.WriteLine("2. Consultar estudiantes");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddStudent(context);
                    break;
                case "2":
                    ListStudents(context);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private static void AddStudent(SchoolContext context)
        {
            Console.WriteLine("Ingrese el nombre del estudiante:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido del estudiante:");
            var lastName = Console.ReadLine();

            try
            {
                var student = new Student { FirstMidName = firstName, LastName = lastName, EnrollmentDate = DateTime.Now };
                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Estudiante agregado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error: " + ex);

            }
        }

        private static void ListStudents(SchoolContext context)
        {
            var students = context.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id}: {student.FullName}");
            }
        }

    }
}
