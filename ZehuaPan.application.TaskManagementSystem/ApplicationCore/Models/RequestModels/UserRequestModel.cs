using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.RequestModels
{
    public class UserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Mobileno { get; set; }
    }
}
