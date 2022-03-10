using ENSEK.Data.Seed;
using ENSEK.Services;
using ENSEK.Services.Constants;
using ENSEK.Services.Interfaces;
using ENSEK.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace ENSEK.Web.Tests
{
    public class FileProccessorServiceTests:BaseTest
    {
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
        }
         [TearDown]
        public void TearDown()
        {
            Dispose();
        }
        [Test]
        public  void ReadFile_NullArgs_ReturnsCorrectErrorMessege()
        {
            var response=_IFileProccessor.ReadFile(null);
            
            Assert.IsNotEmpty(response);
            Assert.AreEqual(Constants.NoFileProvided, response[0].ResponseMessage);
            Assert.IsTrue(response.TrueForAll(x=> x.ResponseMessage == Constants.NoFileProvided));
        }

        [Test]
        public void ReadFile_MockFile_CorrectResult()
        {
            string fileName = Constants.MeterReadingFileName;
            FormFile formFile = null;
            var formFiles = new List<IFormFile>();
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.MeterReadingFileName);
            
            if (File.Exists(path))
            {
                var fileStream = File.Open(path, FileMode.Open);
                 formFile=new FormFile(fileStream, 0, fileStream.Length, fileName, Constants.MeterReadingFileName);
            }
          
            var result =_IFileProccessor.ReadFile(formFile);
            var allMeterReadings= _meterReadingService.GetAllMeterReadings();
            
            Assert.IsNotNull(result);
            Assert.IsNotNull(allMeterReadings);
            Assert.AreEqual(26,allMeterReadings.Count);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(35,result.Count);
            Assert.AreEqual(8,result.Count(x=>x.ResponseMessage==Constants.UnSuccessfulUpload));
           
        }

    }
}