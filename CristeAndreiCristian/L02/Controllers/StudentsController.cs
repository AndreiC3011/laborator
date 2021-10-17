using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Repositories;

namespace L02.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return StudentsRepo.Students;
        }
        
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            foreach (var i in StudentsRepo.Students)
            {
                if (i.Id == id)
                    return i;
            }
            return null;
        }
        [HttpPost]
        public IEnumerable<Student> Post([FromBody] Student student)
        {
            System.Diagnostics.Debug.WriteLine("pula");
            StudentsRepo.Students.Add(student);
            return StudentsRepo.Students.ToArray();
        }
        [HttpPut]
        public IEnumerable<Student> Put([FromBody] Update update)
        {
            foreach (var i in StudentsRepo.Students)
            {
                if (i.Id == update.Id)
                {
                    switch (update.Body.ToLower())
                    {
                        case "id":
                            i.Id = Convert.ToInt32(update.Value);
                            break;
                        case "name":
                            i.Name = update.Value;
                            break;
                        case "faculty":
                            i.Faculty = update.Value;
                            break;
                        case "year":
                            i.Year = Convert.ToInt32(update.Value);
                            break;
                    }
                    break;
                }
            }
            return StudentsRepo.Students.ToArray();
        }
        [HttpDelete("{id}")]
        public IEnumerable<Student> Delete(int id)
        {
            foreach (var i in StudentsRepo.Students)
            {
                if (i.Id == id)
                {
                    StudentsRepo.Students.Remove(i);
                    break;
                }
            }
            return StudentsRepo.Students.ToArray();
        }
    }
}