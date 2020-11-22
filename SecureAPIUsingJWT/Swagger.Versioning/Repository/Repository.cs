using Swagger.Versioning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger.Versioning.Repository
{
    public class Repository : IRepository
    {
        public List<Teacher> Teachers = new List<Teacher>();
        public List<Student> Students = new List<Student>();

        public Repository()
        {
            Teachers.Add(new Teacher { TeacherId = 1, Name = "A", Subject = "A"});
            Teachers.Add(new Teacher { TeacherId = 2, Name = "B", Subject = "B"});
            Teachers.Add(new Teacher { TeacherId = 3, Name = "B", Subject = "B"});
            Teachers.Add(new Teacher { TeacherId = 4, Name = "B", Subject = "B"});
            Teachers.Add(new Teacher { TeacherId = 5, Name = "B", Subject = "B"});

            Students.Add(new Student { StudentId = 1, Name = "S1"});
            Students.Add(new Student { StudentId = 2, Name = "S2"});
            Students.Add(new Student { StudentId = 3, Name = "S3"});
            Students.Add(new Student { StudentId = 4, Name = "S4"});
            Students.Add(new Student { StudentId = 5, Name = "S5"});
        }

        public void AddStudent(Student student)
        {
            student.StudentId = Students.Count() + 1;
            Students.Add(student);
        }

        public void AddTeacher(Teacher teacher)
        {
            teacher.TeacherId = Teachers.Count() + 1;
            Teachers.Add(teacher);
        }

        public List<Student> GetAllStudent()
        {
            return Students;
        }

        public List<Teacher> GetAllTeacher()
        {
            return Teachers;
        }

        public Student GetStudent(int id)
        {
            return Students.Find(s => s.StudentId == id);
        }

        public Teacher GetTeacher(int id)
        {
            return Teachers.Find(t => t.TeacherId == id);
        }

        public void RemoveStudent(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveTeacher(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(Student updateStudent)
        {
            throw new NotImplementedException();
        }

        public void UpdateTeacher(Teacher updateTeacher)
        {
            throw new NotImplementedException();
        }
    }
}
