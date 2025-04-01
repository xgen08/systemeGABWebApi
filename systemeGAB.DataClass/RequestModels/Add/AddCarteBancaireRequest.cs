namespace systemeGAB.DataClass.RequestModels.Add
{
    public class AddCarteBancaireRequest
    {
        [Required]
        public int idCompte { get; set; }
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
