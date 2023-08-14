using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authorization;
namespace ImageGallery.API.Authorization
{
    public class MustOwnImageHandler : AuthorizationHandler<MustOwnImageRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGalleryRepository _galleryRepository;   

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnImageRequirement requirement)
        {
            var imageId = _httpContextAccessor.HttpContext?
                .GetRouteValue("id")?.ToString();
            if(!Guid.TryParse(imageId,out Guid imageIdASGuid))
            {
                context.Fail();
                return;
            }
            var ownerId = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (ownerId == null)
            {
                context.Fail();
                return;
            }
            if (!await _galleryRepository.IsImageOwnerAsync(imageIdASGuid,ownerId) )
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }

        public MustOwnImageHandler(IHttpContextAccessor contextAccessor, IGalleryRepository galleryRepository) 
        { 
            _httpContextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _galleryRepository = galleryRepository ?? throw new ArgumentNullException(nameof(galleryRepository));
        }
    }
}
