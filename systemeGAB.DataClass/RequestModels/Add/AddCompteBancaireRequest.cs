namespace systemeGAB.DataClass.RequestModels.Add
{
    public class AddCompteBancaireRequest
    {
        [Required]
        public int idClient { get; set; }
        [Required]
        public string? numeroCompte { get; set; }
        [Required]
        public double? solde { get; set; }
        [Required]
        public string? typeCompte { get; set; }
    }
}
