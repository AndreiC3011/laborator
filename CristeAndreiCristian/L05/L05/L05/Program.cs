using L05.Models;
using L05.Repository;
using System;
using System.Collections.Generic;

namespace L05
{
    class Program
    {
        private static IStudentsRepository _studentsRepository;
        private static IMetricRepository _metricRepository;

        static void Main(string[] args)
        {
            _studentsRepository = new StudentsRepository();
            _metricRepository = new MetricRepository(_studentsRepository.GetAllStudents().Result);
            _metricRepository.GenerateMetric();

        }
    }
}