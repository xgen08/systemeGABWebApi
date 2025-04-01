// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace systemeGABWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarteBancaireController : ControllerBase
    {
        private readonly ICarteBancaireManager _carteBancaireManager = null!;

        public CarteBancaireController(ICarteBancaireManager carteBancaireManager)
        {
            _carteBancaireManager = carteBancaireManager;
        }

        // GET: api/<CarteBancaireController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarteBancaire>), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<List<CarteBancaire>>> Get()
        {
            try
            {
                var carte = await _carteBancaireManager.GetCarteBancaires();

                if (carte == null || carte.Count == 0)
                {
                    return NoContent();
                }

                return Ok(carte.Select(c => c.carteBancaireResponse).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new List<EntityResponse>
                {
                    new EntityResponse { success = false, message = $"Erreur interne du serveur : {ex.Message}" }
                });
            }
        }

        // GET api/<CarteBancaireController>/5
        [HttpGet("{idCarte}")]
        [ProducesResponseType(typeof(CarteBancaire), 201)]
        [Produces("application/json")]
        public async Task<ActionResult<CarteBancaire>> Get(int idCarte)
        {
            try
            {
                var response = await _carteBancaireManager.GetCarteBancaireById(idCarte);

                if (!response.success)
                {
                    return NotFound(response.message);
                }

                return Ok(response.carteBancaireResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EntityResponse { success = false, message = "Erreur interne : " + ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<CarteBancaire>> Post([FromBody] AddCarteBancaireRequest request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _carteBancaireManager.AddCarteBancaire(request);
        //        return Ok(response.carteBancaireResponse);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}
    }
}
