using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SMSDataLayer
{
    public class DbSeeder : DropCreateDatabaseIfModelChanges<SMSDBContext>
    {
        protected override void Seed(SMSDBContext context)
        {
            base.Seed(context);

            Admin ad = new Admin()
            {
                AdminId = 1,
                AdminName = "Admin",
                AdminEmail = "admin@sms.com",
                AdminPhone = "01712345678",
                AdminAddress = "Nikunja-2",
                AdminDOB = "24/12/2016",
                AdminPassword = "admin123"
            };
            context.Admins.Add(ad);
            context.SaveChanges();
        }
    }
}