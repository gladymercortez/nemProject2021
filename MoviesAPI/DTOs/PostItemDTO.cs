using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs
{
    public class PostItemDTO
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Title { get; set; }
        public string ItemDescription { get; set; }
        public int Condition { get; set; }
        public string MeetingLocation { get; set; }
        public string UserId { get; set; }
    }
}
