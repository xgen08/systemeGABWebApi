namespace systemeGAB.DataClass.EntityModels.Responses
{
    public class EntityResponse : BaseResponse
    {
        public List<Client>? listClientResponse { get; set; }
        public Client? clientResponse { get; set; }
        public List<CarteBancaire>? listCarteBancaireResponse { get; set; }
        public CarteBancaire? carteBancaireResponse { get; set; }
        public TokenModel? tokenResponse { get; set; }
        public List<Transaction>? listTransactionResponse { get; set; }
        public Transaction? transactionResponse { get; set; }
        public List<CompteBancaire>? listCompteBancaireResponse { get; set; }
        public CompteBancaire? compteBancaireResponse { get; set; }
    }
}
