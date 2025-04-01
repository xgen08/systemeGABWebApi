namespace systemeGAB.DataClass.RequestModels.Update
{
    public class UpdateCompteBancaireRequest
    {
        [Required]
        public string? numeroCompte { get; set; }
        [Required]
        public double? solde { get; set; }
        [Required]
        public string? typeCompte { get; set; }
    }
}
