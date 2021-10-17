using System.Collections.Generic;
using Models;

namespace Repositories
{
    public static class StudentsRepo
    {
        public static List<Student> Students = new List<Student>
        {
            new Student() {Id=1, Faculty= "AC", Name="Andrei", Year=4},
            new Student() {Id=2, Faculty= "AC", Name="Mircea", Year=4},
            new Student() {Id=3, Faculty= "AC", Name="Dragos", Year=4},
            new Student() {Id=4, Faculty= "ETC", Name="Emil", Year=4}
        };
    }
}