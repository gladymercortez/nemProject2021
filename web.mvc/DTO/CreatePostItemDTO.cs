using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using web.mvc.Validations;

namespace web.mvc.DTO
{
    public class CreatePostItemDTO
    {

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

        [StringLength(500)]
        public string UserId { get; set; }

        //[FileSizeValidator(MaxFileSizeInMbs: 4)]
        //[ContentTypeValidator(ContentTypeGroup.Image)]
        public IFormFile Picture { get; set; }

    }
}
