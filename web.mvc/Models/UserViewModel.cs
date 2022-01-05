using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web.mvc.Models
{
    public class UserViewModel
    {
        public string EmailAddress { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
