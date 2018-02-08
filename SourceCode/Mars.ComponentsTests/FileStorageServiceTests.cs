﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mars.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Mars.Components.Tests
{
    [TestClass()]
    public class FileStorageServiceTests
    {
        private string _endPoint ;
        private string _sasToken ;
        private string _shareFolder ;

        public FileStorageServiceTests() {
            _endPoint = ConfigurationManager.AppSettings["EndPoint"];
            _sasToken = ConfigurationManager.AppSettings["SasToken"];
            _shareFolder = ConfigurationManager.AppSettings["ShareFolder"];
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            FileStorageService service = new FileStorageService(_endPoint, _sasToken);
            var list = service.ListFilesOrDirectories(_shareFolder);
            var resultJson = service.ConvertToJson(list);
        }

        [TestMethod()]
        public void ListFilesOrDirectoriesTest()
        {
            FileStorageService service = new FileStorageService(_endPoint,_sasToken);
            var list = service.ListFilesOrDirectories(_shareFolder);
        }
    }
}