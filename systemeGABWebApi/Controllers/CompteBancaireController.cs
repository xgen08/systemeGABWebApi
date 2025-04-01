// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace systemeGABWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompteBancaireController : ControllerBase
    {
        private readonly ICompteBancaireManager _compteBancaireManager = null!;
        public CompteBancaireController(ICompteBancaireManager compteBancaireManager)
        {
            _compteBancaireManager = compteBancaireManager;
        }

        // GET: api/<CompteBancaireController>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CompteBancaire>), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<List<CompteBancaire>>> Get()
        {
            try
            {
                var comptes = await _compteBancaireManager.GetCompte();

                if (comptes == null || comptes.Count == 0)
                {
                    return NoContent();
                }

                return Ok(comptes.Select(c => c.compteBancaireResponse).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new List<EntityResponse>
                {
                    new EntityResponse { success = false, message = $"Erreur interne du serveur : {ex.Message}" }
                });
            }
        }

        // GET api/<CompteBancaireController>/5
        [HttpGet("{idCompte}")]
        [ProducesResponseType(typeof(CompteBancaire), 201)]
        [Produces("application/json")]
        public async Task<ActionResult<CompteBancaire>> Get(int idCompte)
        {
            try
            {
                var response = await _compteBancaireManager.GetCompteById(idCompte);

                if (!response.success)
                {
                    return NotFound(response.message);
                }

                return Ok(response.compteBancaireResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EntityResponse { success = false, message = "Erreur interne : " + ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<CompteBancaire>> Post([FromBody] AddCompteBancaireRequest request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _compteBancaireManager.AddCompte(request);
        //        return Ok(response.compteBancaireResponse);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}
    }
}
