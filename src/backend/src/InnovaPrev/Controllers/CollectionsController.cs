using DomainModel.Services.CollectionsProvider;
using Microsoft.AspNetCore.Mvc;

namespace InnovaPrev.Controllers
{
    [Route("api/collections")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionProvider collectionProvider;

        public CollectionsController(ICollectionProvider collectionProvider)
        {
            this.collectionProvider = collectionProvider ?? throw new ArgumentNullException(nameof(collectionProvider));
        }

        // GET: api/<InternalColor>
        [HttpGet]
        public Collections Get()
        {
            return collectionProvider.Get();
        }
    }
}
