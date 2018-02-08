using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.WindowsAzure.Storage.File; // Namespace for Azure Files
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Mars.Components;
using Newtonsoft.Json;

namespace MarsWebSite.Components
{
    public class StorageService
    {
        FileStorageService _service;
        public StorageService() {
            string coureseContentEndPoint = AppSettings.CoureseContentEndPoint;
            string couserSASToken = AppSettings.CourseSASToken;            
            _service = new FileStorageService(coureseContentEndPoint, couserSASToken);    
        }

        public string ListFilesOrDirectories(string sharefolder)
        {
            var list = _service.ListFilesOrDirectories(sharefolder);
            string resultJson = _service.ConvertToJson(list);
            return resultJson;
        }
    }
}
