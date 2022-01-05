using Microsoft.AspNetCore.Http;
using MoviesAPI.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs
{
    public class PostItemCreationDTO
    {
        public string Title { get; set; }
        public string ItemDescription { get; set; }
        public int Condition { get; set; }
        public string MeetingLocation { get; set; }
        public string UserId { get; set; }

        [FileSizeValidator(MaxFileSizeInMbs: 4)]
        [ContentTypeValidator(ContentTypeGroup.Image)]
        public IFormFile Picture { get; set; }

    }
}
