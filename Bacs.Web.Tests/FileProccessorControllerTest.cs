using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using ENSEK.Web.Controllers;
using ENSEK.Services.Interfaces;
using ENSEK.Services;
using ENSEK.Services.Service;
using ENSEK.Services.Constants;
using ENSEK.Models.Models;
using System.Linq;
using ENSEK.Models.Interfaces;
using ENSEK.Data.Seed;

namespace ENSEK.Web.Tests
{
    public class FileProccessorControllerTest: BaseTest
    {
        private FileProccessorController _fileProccessorController;
        private SqliteConnection sqliteConnection;
        private IFileProccessor _IFileProccessor;
        IAccountService _accountService;
        IMeterReadingService _meterReadingService;

        [SetUp]
        public void Setup()
        {
            var context = SetUpContext(false);
            AccountsSeed.SeedAccounts(Constants.AccountsSeedFile, context);
            _meterReadingService = new MeterReadingService(new Data.Repository.MeterReadingRepository(context));
            _accountService = new AccountService(new Data.Repository.AccountRepository(context), _meterReadingService);
            _IFileProccessor = new FileProccessor(_accountService, _meterReadingService);
            _fileProccessorController = new FileProccessorController(_IFileProccessor);
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        [Test]
        public void FileProccessorController_NullArgs_ReturnsOk()
        {
            var result = _fileProccessorController.OnPostUpload(null) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400,result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(Constants.NoFiles,(result.Value as FileResponseBadValidation).ResponseMessage);
        }

        [Test]
        public void FileProccessorController_MockFile_ReturnsOk()
        {
            FormFile formFiles = default;
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.MeterReadingFileName);
            
            if (File.Exists(path))
            {
                var fileStream = File.Open(path, FileMode.Open);
                formFiles = new FormFile(fileStream, 0, fileStream.Length, "someName", Constants.MeterReadingFileName);
            }

            var result =  _fileProccessorController.OnPostUpload(formFiles) as OkObjectResult;
            var allMeterReadings =_meterReadingService.GetAllMeterReadings();
            
            Assert.IsNotNull(result);
           Assert.AreEqual(200, result.StatusCode);
           Assert.IsNotNull(result.Value);
           Assert.IsNotNull(allMeterReadings);
           Assert.IsNotEmpty(allMeterReadings);
           Assert.IsNotEmpty(result.Value as List<IFileResponse>);
           Assert.AreEqual(35,(result.Value as List<IFileResponse>).Count);
           Assert.AreEqual(26, allMeterReadings.Count);
           Assert.Less(allMeterReadings.Count, (result.Value as List<IFileResponse>).Count);
        }
    }
}