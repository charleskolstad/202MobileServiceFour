using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Model
{
    public class Business
    {
        public int BusinessID { get; set; }
        public string BusinessName { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessHoursStart { get; set; }
        public string BusinessHoursEnd { get; set; }
        public string WebsiteUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string ImageGalleryUrl { get; set; }
        public string Other { get; set; }
        public string TypeOfBusiness { get; set; }
        public string AppLink { get; set; }
        public bool IsPublic { get; set; }
        public UserInfo user { get; set; }
        public string AppStatus { get; set; }
    }
}
