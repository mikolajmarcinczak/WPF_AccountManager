using AccountManager.Models;
using AccountManager.Repository;
using AccountManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Services.Implementations
{
    public class InfoService : IInfoService
    {
        private readonly AccountManagerDBFirstContext context;
        public InfoService(AccountManagerDBFirstContext context)
        {
            this.context = context;
        }

        public bool AddInformation(Information information)
        {
            try
            {
                context.Informations.Add(information);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Information> GetAllForUser(string userName)
        {
            return context.Informations.Where(i => i.User.UserName == userName);
        }

        public string GetInformationStringForUser(string userName)
        {
            var informations = GetAllForUser(userName);
            string result = "";

            foreach(var info in informations)
            {
                result = $"{result} {info.InformationName} \n";
                result = $"{result} {info.Content} \n\n";
            }

            return result;
        }
    }
}
