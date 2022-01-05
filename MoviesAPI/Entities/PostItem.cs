using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Entities
{
    public class PostItem : IId
    {

        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Picture { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(300)]
        public string ItemDescription { get; set; }

        [Required]
        public int Condition { get; set; }

        [Required]
        [StringLength(200)]
        public string MeetingLocation { get; set; }

        [Required]
        [StringLength(500)]
        public string UserId { get; set; }
    }
}
