using AccountManager.Models;
using AccountManager.Repository;
using AccountManager.Services.Interfaces;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Services.Implementations
{
    public class BillsService : IBillsService
    {
        private readonly AccountManagerDBFirstContext context;
        public BillsService(AccountManagerDBFirstContext context)
        {
            this.context = context;
        }

        public bool Add(Bill bill)
        {
            try
            {
                context.Add(bill);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Edit(Bill bill)
        {
            var billdb = context.Bills.SingleOrDefault(b => b.Id == bill.Id);

            if (billdb == null)
            {
                return false;
            }

            PropertyInfo[] billProps = typeof(Bill).GetProperties();

            try
            {
                foreach (PropertyInfo property in billProps)
                {
                    property.SetValue(billdb, property.GetValue(bill, null));
                }
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Bill> GetBillsForUser(string userName)
        {
            return context.Bills.Where(b => b.User.UserName == userName);
        }
    }
}
