// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace systemeGABWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager = null!;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        // POST api/<AuthController>
        [HttpPost]
        [ProducesResponseType(typeof(TokenModel), 201)]
        public async Task<ActionResult<TokenModel>> Login([FromBody] ConnectCarteRequest request)
        {
            if (ModelState.IsValid)
            {
                var token = await _authManager.ConnectCarte(request);
                if (!token.success)
                {
                    return Ok(token.message);
                }

                return Ok(token.tokenResponse);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
