
using ENSEK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Data.Seed
{
    public class AccountsSeed
    {

        public static List<Account> SeedAccounts(string fileName, ENSEKContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            List<Account> accounts = new List<Account>();
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            if (File.Exists(path))
            {

                try
                {
                    using (var reader = new StreamReader(File.Open(path, FileMode.Open)))
                    {
                        int readerCount = 0;
                        while (!reader.EndOfStream)
                        {
                           
                            var line = reader.ReadLine();
                            if (line != null && readerCount > 0)
                            {
                                var data = line.Split(','); 
                                int.TryParse(data[0], out var accountId);

                                accounts.Add(new Account
                                {
                                    AccountId = accountId,
                                    FirstName = data[1],
                                    LastName = data[2],
                                });
                            }
                            readerCount++;
                        }
                        
                    }
                    context.AddRange(accounts);
                    context.SaveChanges();
                }

                catch (Exception ex)
                {

                }
            }
            
            return accounts;
        }
    }
}


