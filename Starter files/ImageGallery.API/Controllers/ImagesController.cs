using AutoMapper;
using ImageGallery.API.Services;
using ImageGallery.Authorization;
using ImageGallery.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Text;

namespace ImageGallery.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        //private readonly ILogger<ImagesController> _logger;

        public ImagesController(
            IGalleryRepository galleryRepository,
            IWebHostEnvironment hostingEnvironment,
            IMapper mapper
//            ,ILogger<ImagesController> logger
            )
        {
            _galleryRepository = galleryRepository ?? 
                throw new ArgumentNullException(nameof(galleryRepository));
            _hostingEnvironment = hostingEnvironment ?? 
                throw new ArgumentNullException(nameof(hostingEnvironment));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            var ownerId = GetOwnerId();
            // get from repo
            var imagesFromRepo = await _galleryRepository.GetImagesAsync(ownerId);

            // map to model
            var imagesToReturn = _mapper.Map<IEnumerable<Image>>(imagesFromRepo);

            // return
            return Ok(imagesToReturn);
        }

        [HttpGet("{id}", Name = "GetImage")]
        public async Task<ActionResult<Image>> GetImage(Guid id)
        {
//            var ownerId = GetOwnerId();
            var imageFromRepo = await _galleryRepository.GetImageAsync(id);

            if (imageFromRepo == null)
            {
                return NotFound();
            }

            var imageToReturn = _mapper.Map<Image>(imageFromRepo);

            return Ok(imageToReturn);
        }

        [HttpPost()]
        [Authorize(Policy= Names.ImageAdd)]
        //[Authorize(Roles = "PayingUser")]
        [Authorize(Policy = "ClientApplicationCanWrite")]
        public async Task<ActionResult<Image>> CreateImage([FromBody] ImageForCreation imageForCreation)
        {
            // Automapper maps only the Title in our configuration
            var imageEntity = _mapper.Map<Entities.Image>(imageForCreation);

            // Create an image from the passed-in bytes (Base64), and 
            // set the filename on the image

            // get this environment's web root path (the path
            // from which static content, like an image, is served)
            var webRootPath = _hostingEnvironment.WebRootPath;

            // create the filename
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            
            // the full file path
            var filePath = Path.Combine($"{webRootPath}/images/{fileName}");

            // write bytes and auto-close stream
            await System.IO.File.WriteAllBytesAsync(filePath, imageForCreation.Bytes);

            // fill out the filename
            imageEntity.FileName = fileName;

            // ownerId should be set - can't save image in starter solution, will
            // be fixed during the course
            imageEntity.OwnerId = GetOwnerId();

            // add and save.  
            _galleryRepository.AddImage(imageEntity);

            await _galleryRepository.SaveChangesAsync();

            var imageToReturn = _mapper.Map<Image>(imageEntity);

            return CreatedAtRoute("GetImage",
                new { id = imageToReturn.Id },
                imageToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize(Names.ImageOwner)]
        public async Task<IActionResult> DeleteImage(Guid id)
        {            
            var imageFromRepo = await _galleryRepository.GetImageAsync(id);

            if (imageFromRepo == null)
            {
                return NotFound();
            }

            _galleryRepository.DeleteImage(imageFromRepo);

            await _galleryRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Names.ImageOwner)]
        public async Task<IActionResult> UpdateImage(Guid id, 
            [FromBody] ImageForUpdate imageForUpdate)
        {
            var imageFromRepo = await _galleryRepository.GetImageAsync(id);
            if (imageFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(imageForUpdate, imageFromRepo);

            _galleryRepository.UpdateImage(imageFromRepo);

            await _galleryRepository.SaveChangesAsync();

            return NoContent();
        }
        //public async Task LogAccessInformation()
        //{
        //    // get the saved access token.  
        //    var accessToken = await HttpContext
        //        .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        //    var userClaimsStringBuilder = new StringBuilder();
        //    foreach (var claim in User.Claims)
        //    {
        //        userClaimsStringBuilder.AppendLine(
        //            $"Claim type: {claim.Type} - Claim value: {claim.Value}");
        //    }

        //    // log tokens & claims
        //    _logger.LogInformation($"Access token: " +
        //        $"\n{accessToken}");
        //    //_logger.LogInformation($"Refresh token: " +
        //    //    $"\n{refreshToken}");
        //}

        private string GetOwnerId ()
        {
            var result = User.Claims
                .FirstOrDefault(c => c.Type == "sub")?.Value;
            if (result == null)
            {
                //await LogAccessInformation();
                throw new NotSupportedException($"OwnerId Missing from user information");
            }
            return result;
        }

    }
}