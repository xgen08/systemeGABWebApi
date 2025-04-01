// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace systemeGABWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientManager _clientManager = null!;

        public ClientController(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        // GET api/<ClientController>/5
        [HttpGet("{idClient}")]
        [ProducesResponseType(typeof(Client), 201)]
        [Produces("application/json")]
        public async Task<ActionResult<EntityResponse>> Get(int idClient)
        {
            try
            {
                var response = await _clientManager.GetClientById(idClient);

                if (!response.success)
                {
                    return NotFound(response.message);
                }

                return Ok(response.clientResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EntityResponse { success = false, message = "Erreur interne : " + ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<Client>> Post([FromBody] AddClientRequest request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _clientManager.AddClient(request);
        //        return Ok(response.clientResponse);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}
    }
}
