using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yansi_KalmTraining.Model.Viewmodel
{
    public class UserTokenModel
    {
        public int UserId { get; set; }
        public DateTime TokenValidTo { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
    }
}
