namespace systemeGAB.DataClass.RequestModels.Add
{
    public class AddTransactionRequest
    {
        [Required]
        public int idCompte { get; set; }
        [Required]
        public string? typeTransaction { get; set; } //Retrait, Dépôt, Consultation solde
        [Required]
        public double? montant { get; set; }
    }
}
