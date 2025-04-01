namespace systemeGAB.DataClass.RequestModels.Update
{
    public class UpdateCarteBancaireRequest
    {
        [Required]
        public string? numeroCarte { get; set; }
        [Required]
        public DateTime? dateExpiration { get; set; }
        [Required]
        public string? codePin { get; set; }
        [Required]
        public string? statut { get; set; }
    }
}
