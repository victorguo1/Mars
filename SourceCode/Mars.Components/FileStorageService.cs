using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.File; // Namespace for Azure Files
using Microsoft.WindowsAzure.Storage.Auth;
using Newtonsoft.Json;

namespace Mars.Components
{
    public class FileStorageService
    {
        CloudFileClient _sasClient;
        
        public FileStorageService(string endPoint,string sasToken)
        {
            string courseContentEndPoint = endPoint;
            string courseSASToken = sasToken;

            StorageCredentials creds = new StorageCredentials(courseSASToken);
            _sasClient = new CloudFileClient(new Uri(courseContentEndPoint), creds);
        }

        public object[] ListFilesOrDirectories(string shareFolder)
        {           
            var share = _sasClient.GetShareReference(shareFolder);
            var rootDirectory = share.GetRootDirectoryReference();
            var dirs = rootDirectory.ListFilesAndDirectories();           
            var list = dirs.ToList<IListFileItem>();

            string type = string.Empty;
            string size = string.Empty;
            string name = string.Empty;
            string path = string.Empty;

            string lastModifiedDate = string.Empty;

            object[] content = new object[list.Count];
            int i = 0;
            foreach (var item in list)
            {
                if (item is CloudFileDirectory)
                {
                    type = "directory";
                    name =  ((CloudFileDirectory)item).Name;
                    path = shareFolder + "-" + name;
                }
                else {
                    type = "file";
                    name = ((CloudFile)item).Name;
                    path = shareFolder;
                    size = ((CloudFile)item).Properties.Length.ToString();

                }
                lastModifiedDate = item.Share.Properties.LastModified.HasValue ? item.Share.Properties.LastModified.Value.DateTime.ToString() :
                    string.Empty;

                content[i++] = new
                {
                    Name = name,
                    Link = item.Uri.ToString(),
                    Type = type,
                    LastModifiedDate = lastModifiedDate,
                    Size = size,
                    Path = path
                };
            }
            return content;
        }

        public CloudFile GetFile(string shareFolder, string fileName) {
           var share = _sasClient.GetShareReference(shareFolder);
           var dir = share.GetRootDirectoryReference();
           var file = dir.GetFileReference(fileName);
            return file;
        }

        public string ConvertToJson(object objects) {
            var resultJson = JsonConvert.SerializeObject(objects);
            return resultJson;
        }
    }
}
