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
    public class AppRequestTest
    {
        [Test]
        public void AppRequestsGetAll_ReturnList()
        {
            List<AppRequest> appRequests = AppRequestManager.AppRequestsGetAll(true);

            Assert.IsTrue(appRequests.Count > 0);
        }

        [TestCase("status", true)]
        [TestCase("", false)]
        public void AppRequestUpdate_ReturnString(string status, bool expected)
        {
            AppRequest request = new AppRequest() { DevStatus = status };
            string errorMessage = string.Empty;
            bool isNullString = false;

            AppRequestManager.AppRequestUpdate(request, out errorMessage, true);
            isNullString = string.IsNullOrEmpty(errorMessage);

            Assert.IsTrue(isNullString == expected);
        }
    }
}
