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
    public class FeatureManagerTest
    {
        [Test]
        public void GetAllFeatures_ReturnList()
        {
            List<Features> allFeatures = FeatureManager.GetAllFeatures(true);

            Assert.IsTrue(allFeatures.Count >= 1);
        }

        [TestCase("Name", true)]
        [TestCase("", false)]
        public void FeatureInsert_ReturnInt(string name, bool expected)
        {
            Features feature = new Features();
            feature.FeatureName = name;
            string errorMessage = string.Empty;
            bool isEmptyString = true;

            FeatureManager.FeatureInsert(feature, out errorMessage, true);
            isEmptyString = string.IsNullOrEmpty(errorMessage);

            Assert.IsTrue(isEmptyString == expected);
        }

        [TestCase("Name", true)]
        [TestCase("", false)]
        public void FeatureUpdate_ReturnString(string name, bool expected)
        {
            Features feature = new Features();
            feature.FeatureName = name;
            string errorMessage = string.Empty;
            bool isEmptyString = true;

            FeatureManager.FeatureUpdate(feature, out errorMessage, true);
            isEmptyString = string.IsNullOrEmpty(errorMessage);

            Assert.IsTrue(isEmptyString == expected);
        }

        [TestCase(1, true)]
        [TestCase(0, false)]
        public void FeatureDelete_ReturnString(int id, bool expected)
        {
            int featureID = id;
            string errorMessage = string.Empty;
            bool isEmptyString = true;

            FeatureManager.FeatureDelete(featureID, out errorMessage, true);
            isEmptyString = string.IsNullOrEmpty(errorMessage);

            Assert.IsTrue(isEmptyString == expected);
        }
    }
}
