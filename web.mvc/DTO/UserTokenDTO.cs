using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.mvc.DTO
{
    public class UserTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
