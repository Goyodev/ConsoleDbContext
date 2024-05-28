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
                    var student = GetStudent();
                    AddStudent(student, context);
                    break;
                case "2":
                    ListStudents(context);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
        private static Student GetStudent()
        {
            Console.WriteLine("Ingrese el nombre del estudiante:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido del estudiante:");
            var lastName = Console.ReadLine();

            return new Student { FirstMidName = firstName, LastName = lastName, EnrollmentDate = DateTime.Now };
        }

        private static void AddStudent(Student? student, SchoolContext context)
        {
            try
            {
                if (student is null) throw new Exception("Su estudiante no es válido.");

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
            foreach (var student in context.Students)
            {
                Console.WriteLine($"{student.Id}: {student.FullName}");
            }
        }

    }
}
