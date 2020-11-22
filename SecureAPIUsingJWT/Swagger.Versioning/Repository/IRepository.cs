using Swagger.Versioning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger.Versioning.Repository
{
    public interface IRepository
    {
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher updateTeacher);
        void RemoveTeacher(int id);
        Teacher GetTeacher(int id);
        List<Teacher> GetAllTeacher();

        void AddStudent(Student student);
        void UpdateStudent(Student updateStudent);
        void RemoveStudent(int id);
        Student GetStudent(int id);
        List<Student> GetAllStudent();
    }
}
