

using ENSEK.Data.Repository;
using ENSEK.Models.Models;
using ENSEK.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Services.Service
{
    public class MeterReadingService : IMeterReadingService { 
       
        private readonly MeterReadingRepository _meterReadingRepository;
        public MeterReadingService(MeterReadingRepository meterReadingRepository) => _meterReadingRepository = meterReadingRepository;

        public List<MeterReading> GetAllMeterReadings()
        {
            return _meterReadingRepository.GetAll().ToList();
        }

        public List<MeterReading> GetAllMeterReadings(MeterReading meterReading)
        {
            throw new NotImplementedException();
        }

        public MeterReading GetByID(int Id)
        {
           return _meterReadingRepository.GetByID(Id);
        }

        public void Insert(MeterReading meterReading)
        {
            if(!_meterReadingRepository.GetAll(x=>x.AccountId == meterReading.AccountId && x.MeterReadingDateTime < meterReading.MeterReadingDateTime && x.MeterReadValue == meterReading.MeterReadValue).Any())
            {
                _meterReadingRepository.Insert(meterReading);
                Save();
            }
            
        }

        public void Save()
        {
            _meterReadingRepository.Save();
        }
    }
}
