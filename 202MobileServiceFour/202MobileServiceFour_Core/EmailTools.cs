using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Core
{
    internal class EmailTools : IEmailTools
    {
        public bool SendEmail(string to, string body)
        {
            throw new NotImplementedException();
        }


        public bool SendToAll(string body)
        {
            List<UserInfo> allUsers = UserManager.GetAllUsers();
            bool allSent = true;

            foreach (UserInfo user in allUsers)
            {
                bool result = SendEmail(user.Email, body);
                allSent = (allSent) ? result : false;
            }

            return allSent;
        }
    }

    internal class FakeEmailTools : IEmailTools
    {
        public bool SendEmail(string to, string body)
        {
            return true;
        }


        public bool SendToAll(string body)
        {
            return true;
        }
    }

    internal interface IEmailTools
    {
        bool SendEmail(string to, string body);
        bool SendToAll(string body);
    }
}
