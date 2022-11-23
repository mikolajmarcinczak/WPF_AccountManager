using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Models;

namespace AccountManager.Services.Interfaces
{
    public interface IInfoService
    {
        IEnumerable<Information> GetAllForUser(string email);
        bool AddInformation(Information information);
        string GetInformationStringForUser(string email);
    }
}
