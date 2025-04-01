// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace systemeGABWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionManager _transactionManager = null!;

        public TransactionController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        // GET: api/<TransactionController>/compte/{idCompte}
        [HttpGet("compte/{idCompte}")]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<List<Transaction>>> GetTransactionsByCompte(int idCompte)
        {
            try
            {
                var transactions = await _transactionManager.GetTransactions(idCompte);

                if (transactions == null || transactions.Count == 0)
                {
                    return NoContent();
                }

                if (transactions.Any(c => c.message == "Aucune transaction trouvée!") || (transactions.Any(c => c.message == "CompteBancaire non trouvé")))
                {
                    return Ok(transactions.Select(c => c.message));
                }

                return Ok(transactions.Select(c => c.transactionResponse).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new List<EntityResponse>
                {
                    new EntityResponse { success = false, message = $"Erreur interne du serveur : {ex.Message}" }
                });
            }
        }

        // GET api/<TransactionController>/transaction/{idTransaction}
        [HttpGet("transaction/{idTransaction}")]
        [ProducesResponseType(typeof(Transaction), 201)]
        [Produces("application/json")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int idTransaction)
        {
            try
            {
                var response = await _transactionManager.GetTransactionById(idTransaction);

                if (!response.success)
                {
                    return NotFound(response.message);
                }

                return Ok(response.transactionResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EntityResponse { success = false, message = "Erreur interne : " + ex.Message });
            }
        }

        // POST api/<TransactionController>
        [HttpPost]
        [ProducesResponseType(typeof(Transaction), 201)]
        [Produces("application/json")]
        public async Task<ActionResult<Transaction>> Post([FromBody] AddTransactionRequest request)
        {
            try
            {
                var response = await _transactionManager.AddTransaction(request);
                if (!response.success)
                {
                    return Ok(response.message);
                }
                return Ok(response.transactionResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EntityResponse { success = false, message = "Erreur interne : " + ex.Message });
            }
        }
    }
}
