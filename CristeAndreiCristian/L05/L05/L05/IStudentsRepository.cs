using L05.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L05
{
    public interface IStudentsRepository
    {
        Task<List<StudentEntity>> GetAllStudents();
    }
}