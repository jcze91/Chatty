using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice.Providers
{
    public class ContactProvider : Utils.BaseProvider<int, Dbo.Contact, DataAccess.ContactDao, Services.ContactService>
    {
        protected override string getClass() { return "contact"; }

        protected override int GetFieldCount() { return 3; }

        protected override dynamic Insert(string[] args)
        {
            try
            {
                return service.Insert(new Dbo.Contact()
                {
                    UserId = int.Parse(args[1]),
                    ContactId = int.Parse(args[2])
                });
            }
            catch { return null; }
        }

        protected override dynamic Update(string[] args)
        {
            try
            {
                return service.Update(new Dbo.Contact()
                {
                    Id = int.Parse(args[1]),
                    UserId = int.Parse(args[2]),
                    ContactId = int.Parse(args[3])
                });
            }
            catch { return null; }
        }
    }
}