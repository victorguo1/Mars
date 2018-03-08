using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars.Components
{
    public class TableStorageService
    {
        CloudTableClient _sasClient;
        public TableStorageService(string endPoint, string sasToken)
        {
            string courseContentEndPoint = endPoint;
            string courseSASToken = sasToken;

            StorageCredentials creds = new StorageCredentials(courseSASToken);
            _sasClient = new CloudTableClient(new Uri(courseContentEndPoint), creds);
        }

        public string GetCourses() {
            CloudTable table = _sasClient.GetTableReference(StorageAppSettings.CourseTable);

            var entities = table.ExecuteQuery(new TableQuery<TableEntity>()).ToList();
            var resultJson = JsonConvert.SerializeObject(entities);
            return resultJson;
        }
    }
}
