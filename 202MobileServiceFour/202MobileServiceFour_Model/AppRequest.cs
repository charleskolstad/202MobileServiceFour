using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Model
{
    public class AppRequest
    {
        public int AppRequestID { get; set; }
        public DateTime DateRequested { get; set; }
        public Business AppBusiness { get; set; }
        public string DevStatus { get; set; }
        public bool Active { get; set; }
        List<FeatureRequested> RequestedFeatures { get; set; }
    }
}
