
using ENSEK.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ENSEK.Data.Repository
{
    public class MeterReadingRepository : GenericRepository<MeterReading>
    {
        public MeterReadingRepository(ENSEKContext context) : base(context)
        {
        }

    }
}

