using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Models;

namespace AccountManager.Services.Interfaces
{
    internal interface IInfoService
    {
        IEnumerable<Information> GetAllForUser(string userName);
        bool AddInformation(Information information);
        string GetInformationStringForUser(string userName);
    }
}
