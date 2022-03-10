
using ENSEK.Models;
using ENSEK.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENSEK.Services.Interfaces
{
    public interface IMeterReadingService
    {
        void Insert(MeterReading meterReading); 
        List<MeterReading> GetAllMeterReadings(); 
    }
}
