using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Model
{
    public class FeatureRequested
    {
        public int FeatureRequestedID { get; set; }
        public int AppRequestID { get; set; }
        public Features RequestedFeature { get; set; }
        public DateTime? DateRequested { get; set; }
        public string DevStatus { get; set; }
        public UserInfo AssignedTo { get; set; }
        public bool Active { get; set; }
    }
}
