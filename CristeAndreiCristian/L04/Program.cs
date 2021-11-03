using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace L04
{
    class Program
    {
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;
        static void Main(string[] args)
        {
            Task.Run(async()=>{await Initialize();})
            .GetAwaiter()
            .GetResult();
        }

        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;" + "AccountName=cristeandrei3datc" + ";AccountKey=potrsshuSBxJW+ve3iiZFNNlyKozjtcteLoqAElq1wXDp9mNNBNK9c7IdzdckfK+/nNAL8I2YtATHwvAWcqo7A==" + ";EndpointSuffix=core.windows.net";
            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();
            studentsTable = tableClient.GetTableReference("studenti");
            await studentsTable.CreateIfNotExistsAsync();   
            await AddNewStudent();
            //await GetAllStudents();
            //await UpdateStudent();
            //await DeleteStudent();
            
        }
        private static async Task GetAllStudents()
        {
            Console.WriteLine("Universitate\tCNP\tNume\tEmail\tNumar telefon\tAn");
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>();

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (StudentEntity entity in resultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2} {3}\t{4}\t{5}\t{6}", entity.PartitionKey , entity.RowKey,
                    entity.FirstName, entity.LastName,
                    entity.Email, entity.PhoneNumber,
                    entity.Year);
                }
            }while (token != null);
        }

        private static async Task AddNewStudent()
        {
            var student = new StudentEntity ("UPT","1981130305234");
            student.FirstName = "Criste";
            student.LastName = "Andrei-Cristian";
            student.Email = "criste.andrei30@yahoo.com";
            student.Year = 4;
            student.PhoneNumber = "0748036174";
            student.Faculty= "AC";

            var insertOperation = TableOperation.Insert(student);
            await studentsTable.ExecuteAsync(insertOperation);
        }

        private static async Task UpdateStudent()
        {
            string fisrtName, lastName, email, phoneNumber, faculty, university, cnp;
            int year;

            Console.WriteLine("Enter student first-name:");
            fisrtName = Console.ReadLine();
            Console.WriteLine("Enter student last-name:");
            lastName = Console.ReadLine();
            Console.WriteLine("Enter student e-mail:");
            email = Console.ReadLine();
            Console.WriteLine("Enter student phone number:");
            phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter student faculty:");
            faculty = Console.ReadLine();
            Console.WriteLine("Enter student university:");
            university = Console.ReadLine();
            Console.WriteLine("Enter student cnp:");
            cnp = Console.ReadLine();
            Console.WriteLine("Enter student year:");
            year = Convert.ToInt32(Console.ReadLine());

            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>().Where(TableQuery.CombineFilters(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, cnp), TableOperators.And, TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, university)));
            TableContinuationToken token = null;
            TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
            token = resultSegment.ContinuationToken;
            
            resultSegment.Results[0].FirstName = fisrtName;
            resultSegment.Results[0].LastName = lastName;
            resultSegment.Results[0].Email = email;
            resultSegment.Results[0].PhoneNumber = phoneNumber;
            resultSegment.Results[0].Faculty = faculty;
            resultSegment.Results[0].Year = year;

            var updateOperation = TableOperation.Replace(resultSegment.Results[0]);
            await studentsTable.ExecuteAsync(updateOperation);
        }
        private static async Task DeleteStudent()
        {   
            string university, cnp;

            Console.WriteLine("Enter student university:");
            university = Console.ReadLine();
            Console.WriteLine("Enter student cnp:");
            cnp = Console.ReadLine();

            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>().Where(TableQuery.CombineFilters(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, cnp), TableOperators.And, TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, university)));
            TableContinuationToken token = null;
            TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
            token = resultSegment.ContinuationToken;      

            var updateOperation = TableOperation.Delete(resultSegment.Results[0]);
            await studentsTable.ExecuteAsync(updateOperation);
        }
    }
}
