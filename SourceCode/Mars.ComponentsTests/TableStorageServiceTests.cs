using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mars.Components;
using System.Configuration;

namespace Mars.Components.Tests
{
    [TestClass()]
    public class TableStorageServiceTests
    {
        private string _endPoint;
        private string _sasToken;

        public TableStorageServiceTests()
        {
            string storageAccount = ConfigurationManager.AppSettings["CourseStorageAccount"];
            _sasToken = ConfigurationManager.AppSettings["CourseSASToken"];

            var settings = new StorageAppSettings(storageAccount);
            _endPoint = settings.TableEndPoint;

        }

        [TestMethod()]
        public void GetCoursesTest()
        {
            TableStorageService service = new TableStorageService(_endPoint,_sasToken);
            string json = service.GetCourses();
        }
    }
}