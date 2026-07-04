using System;

namespace MVC_ConsoleAPP
{
    // Model
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentView
    {
        public void DisplayStudent(Student student)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
        }
    }

    public class StudentController
    {
        private Student _student;
        private StudentView _view;

        public StudentController(Student student, StudentView view)
        {
            _student = student;
            _view = view;
        }

        public void SetStudentName(string name)
        {
            _student.Name = name;
        }

        public void UpdateView()
        {
            _view.DisplayStudent(_student);
        }
    }

    internal class StudentMVC
    {
        public static void Application()
        {
            Student student = new Student { Id = 1, Name = "Ram" };
            StudentView view = new StudentView();
            StudentController controller = new StudentController(student, view);

            controller.UpdateView();
            controller.SetStudentName("Krushna");
            controller.UpdateView();
        }
    }
}

// Entry point
class Program
{
    static void Main()
    {
        MVC_ConsoleAPP.StudentMVC.Application();
    }
}
