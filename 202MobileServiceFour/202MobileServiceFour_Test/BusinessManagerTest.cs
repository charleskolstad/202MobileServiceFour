using _202MobileServiceFour_Core;
using _202MobileServiceFour_Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Test
{
    [TestFixture]
    public class BusinessManagerTest
    {
        [TestCase("user", true)]
        [TestCase("", false)]
        public void BusinessGetByUser_ReturnBusiness(string name, bool expected)
        {
            string userName = name;
            bool isNotNull;

            Business business = BusinessManager.BusinessGetByUser(userName, true);
            isNotNull = (business != null);

            Assert.IsTrue(isNotNull == expected);
        }

        [TestCase("Business", "test@test.com", "(111)111-1111", true)]
        [TestCase("", "test@test.com", "(111)111-1111", false)]
        [TestCase("Business", "invalid", "(111)111-1111", false)]
        [TestCase("Business", "test@test.com", "invalid", false)]
        public void BusinessUpdate_ReturnString(string name, string email, string number, bool expected)
        {
            Business business = new Business();
            business.BusinessName = name;
            business.BusinessEmail = email;
            business.BusinessPhone = number;
            string errorMessage;
            bool isNullString = true;

            BusinessManager.BusinessUpdate(business, out errorMessage, true);
            isNullString = string.IsNullOrEmpty(errorMessage);

            Assert.IsTrue(isNullString == expected);            
        }

        public void BusinessGetAll_ReturnList()
        {
            List<Business> businesses = BusinessManager.BusinessGetAll(true);

            Assert.IsTrue(businesses.Count() > 0);
        }
    }
}
