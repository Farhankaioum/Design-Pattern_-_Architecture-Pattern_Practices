using Microsoft.AspNetCore.Mvc;
using Swagger.Versioning.Models;
using Swagger.Versioning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger.Versioning.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepository _repository;

        public StudentController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<TeacherController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _repository.GetAllStudent();
        }

        // GET api/<TeacherController>/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _repository.GetStudent(id);
        }

        // POST api/<TeacherController>
        [HttpPost]
        public void Post([FromBody] Student student)
        {
            _repository.AddStudent(student);
        }

        // PUT api/<TeacherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Student student)
        {
            _repository.UpdateStudent(student);
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.RemoveStudent(id);
        }
    }
}
