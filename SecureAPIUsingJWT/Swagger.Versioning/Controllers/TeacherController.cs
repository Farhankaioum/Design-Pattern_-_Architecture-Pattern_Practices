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
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IRepository _repository;

        public TeacherController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<TeacherController>
        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            return _repository.GetAllTeacher();
        }

        // GET api/<TeacherController>/5
        [HttpGet("{id}")]
        public Teacher Get(int id)
        {
            return _repository.GetTeacher(id);
        }

        // POST api/<TeacherController>
        [HttpPost]
        public void Post([FromBody] Teacher teacher)
        {
            _repository.AddTeacher(teacher);
        }

        // PUT api/<TeacherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Teacher teacher)
        {
            _repository.UpdateTeacher(teacher);
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.RemoveTeacher(id);
        }
    }
}
