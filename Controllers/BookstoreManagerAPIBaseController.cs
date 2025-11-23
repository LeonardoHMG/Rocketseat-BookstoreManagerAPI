using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManagerAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public abstract class BookstoreManagerAPIBaseController : ControllerBase
{
}
