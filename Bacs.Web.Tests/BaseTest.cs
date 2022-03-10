
using ENSEK.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Web.Tests
{
    public class BaseTest
    {
        private SqliteConnection sqliteConnection;
        private ENSEKContext _context;
        public ENSEKContext SetUpContext(bool UseSqlServer)
        {
            sqliteConnection = new SqliteConnection("Filename=:memory:");
            sqliteConnection.Open();
            DbContextOptions<ENSEKContext> _contextOptions = null;
            // These options will be used by the context instances in this test suite, including the connection opened above.
            if (!UseSqlServer)
            {
                _contextOptions = new DbContextOptionsBuilder<ENSEKContext>()
                     .UseSqlite(sqliteConnection)
                     .Options;
            }
            else
            {
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                var config = builder.Build();
                _contextOptions = new DbContextOptionsBuilder<ENSEKContext>()
                     .UseSqlServer(config.GetConnectionString("Default"))
                     .Options;
            }
                
        
             _context = new ENSEKContext(_contextOptions); 
        
            _context.Database.EnsureCreated();
            return _context;
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            sqliteConnection.Close();
            sqliteConnection.Dispose();
        }
    }
}
