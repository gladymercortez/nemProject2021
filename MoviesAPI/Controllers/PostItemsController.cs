using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/postitems")]
    public class PostItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;
        private readonly IMapper mapper;
        private readonly string containerName = "postitem";

        public PostItemsController(ApplicationDbContext context,
            IFileStorageService fileStorageService,
            IMapper mapper)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<PostItemDTO>>> Get()
        {
            var postitem = await context.PostItems.ToListAsync();
            return mapper.Map<List<PostItemDTO>>(postitem);
        }


        [HttpGet("GetItemByUserID/{userId}", Name = "getItemByUserID")]
        public async Task<ActionResult<List<PostItemDTO>>> GetItemByUserID(string userId)
        {
            var postitem = await context.PostItems
                .Where(x => x.UserId == userId)             
                .ToListAsync();
            return mapper.Map<List<PostItemDTO>>(postitem);
        }

        [HttpGet("{id}", Name = "getItem")]
        public async Task<ActionResult<PostItemDTO>> Get(int id)
        {
            var postItem = await context.PostItems.FirstOrDefaultAsync(x => x.Id == id);

            if (postItem == null)
            {
                return NotFound();
            }

            return mapper.Map<PostItemDTO>(postItem);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromForm] PostItemCreationDTO postItemCreationDTO)
        {
            var postItem = mapper.Map<PostItem>(postItemCreationDTO);

            if (postItemCreationDTO.Picture != null)
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    await postItemCreationDTO.Picture.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(postItemCreationDTO.Picture.FileName);
                    postItem.Picture =
                        await fileStorageService.SaveFile(content, extension, containerName,
                                                            postItemCreationDTO.Picture.ContentType);
                }
            }

            context.Add(postItem);
            await context.SaveChangesAsync();
            var postItemDTO = mapper.Map<PostItemDTO>(postItem);
            return new CreatedAtRouteResult("getItem", new { id = postItem.Id }, postItemDTO);

        }

    }
}