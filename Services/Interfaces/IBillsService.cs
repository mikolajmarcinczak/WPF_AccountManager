using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Models;

namespace AccountManager.Services.Interfaces
{
    public interface IBillsService
    {
        IEnumerable<Bill> GetBillsForUser(string email);
        bool Add(Bill bill);
        bool Edit(Bill bill);
    }
}
