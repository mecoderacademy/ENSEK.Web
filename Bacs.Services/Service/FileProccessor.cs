using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Linq;
using System.IO;

using System.Reflection;
using System.Collections.Generic;
using ENSEK.Services.Constants;
using ENSEK.Models.Models;
using ENSEK.Services.Interfaces;
using ENSEK.Models.Interfaces;
using System.Globalization;
using ENSEK.Models;

namespace ENSEK.Services
{
    public class FileProccessor : IFileProccessor
    {
        private readonly IMeterReadingService _meterReadingService;
        private readonly IAccountService _accountService;
        public FileProccessor(IAccountService accountService, IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
            _accountService = accountService;
        }
        public List<IFileResponse> ReadFile(IFormFile formFile)
        {
            var fileResponses = new List<IFileResponse>();

            if (formFile == null)
            {
                fileResponses.Add(new FileResponseBadValidation { ResponseMessage= Constants.Constants.NoFileProvided});
                return fileResponses;
            }

            try
            {
                using (var reader = new StreamReader(formFile.OpenReadStream()))
                {
                    int readerCount = 0;
                    while (!reader.EndOfStream)
                    {
                        readerCount++;
                        var line = reader.ReadLine();
                        if (line != null && readerCount > 1)
                        {
                           
                            var data = line.Split(',');  // could validate using regular expression
                            bool isValidAccount = int.TryParse(data[0], out int accountId) && _accountService.GetByAccountId(accountId)?.AccountId > 0;
                            bool isValidDate = DateTime.TryParse(data[1], out DateTime meterReadingDateTime);
                            bool isValidMeterReading = int.TryParse(data[2], out int meterReadValue);
                            bool isValid = isValidAccount && isValidDate && isValidMeterReading;

                            if (isValid)
                            {
                                _meterReadingService.Insert(new MeterReading
                                {
                                    AccountId = _accountService.GetByAccountId(accountId).Id,
                                    MeterReadingDateTime = meterReadingDateTime,
                                    MeterReadValue = meterReadValue.ToString("D5", CultureInfo.CurrentCulture),
                                });
                            }

                            fileResponses.Add(new MeterReading
                            {
                                AccountId = accountId,
                                MeterReadingDateTime = meterReadingDateTime,
                                MeterReadValue = meterReadValue.ToString("D5", CultureInfo.CurrentCulture),
                                ResponseMessage = isValid ? Constants.Constants.SuccessfulUpload : Constants.Constants.UnSuccessfulUpload,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                fileResponses.Add(new FileResponseBadValidation() { ResponseMessage = ex.Message });
                return fileResponses;
            }

            return fileResponses;
        }
    }
}
