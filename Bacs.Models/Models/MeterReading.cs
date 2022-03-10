using ENSEK.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Models.Models
{
    public class MeterReading: IFileResponse
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string MeterReadValue { get; set; }
        public Account Account { get; set; } 
        public DateTime MeterReadingDateTime { get; set; }
        public string ResponseMessage { get; set; }
    }
}
